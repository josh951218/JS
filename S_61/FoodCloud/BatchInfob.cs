using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.IO;

namespace S_61.FoodCloud
{
    public partial class BatchInfob : Formbase
    {
        private TextBoxT date;
        private TextBoxT date1;
        private TextBoxT itno;
        private TextBoxT itno1;
        private TextBoxT fano;
        private TextBoxT fano1;
        private TextBoxT BatchNo;
        private TextBoxT BatchNo1;
        private TextBoxT Manufacturingdate;
        private TextBoxT Manufacturingdate1;
        private TextBoxT ExpiryDate;
        private TextBoxT ExpiryDate1;
        private TextBoxT StNo;
        private TextBoxT StNo1;
        List<Button> qury;
        int sort;
        DataTable dt = new DataTable();
        parameters par = new parameters();
         
        
        #region 前期Sql
        string 前期Sql_上半部 = @"
						SELECT BatchInformation.Bno,民國年='   /  /  ',西元年='    /  /  ',單據='前期',異動數量 = 前期數量,結餘數量 = 前期數量,單據編號='',明細序號=0,bom序=0,客戶或廠商編號='',客戶或廠商簡稱='',異動倉庫='',倉庫名稱='',產品編號=Itno,品名規格 = (select top 1 itname from item where item.itno = BatchInformation.Itno),製造商編號=Fano,製造商簡稱 = (select top 1 faname1 from fact where fact.fano = BatchInformation.Fano),批次號碼 = Batchno,製造日期 = [Date],有效日期 = Date1 FROM BatchInformation
						INNER JOIN 
						(
							SELECT bno,前期數量 = SUM(異動數量) FROM
							 (
										SELECT Bno,民國年,西元年,單據,異動數量,單據編號,明細序號 = 序,bom序,客戶或廠商編號,客戶或廠商簡稱,異動倉庫,倉庫名稱 FROM 
											(
												SELECT 單據 = '+入庫明細',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoi,序=b.recordno, bom序='',民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
												FROM 
												(select * from BatchProcess_Garnerd ) as a																    
												inner join 																				    
												(select * from Garnerd where len(stnoi) > 0 and ittrait != '1') as b 	--									    
												on 																						    
												a.bomid = b.bomid															    
											UNION ALL																					    
												SELECT 單據 = '-入庫明細',異動數量 = -1 * a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序='',民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
												FROM 
												(select * from BatchProcess_Garnerd ) as a	
												inner join 
												(select * from Garnerd where len(stnoo) > 0 and ittrait = '3') as b   --
												on 
												a.bomid = b.bomid
											UNION ALL																						    		
												SELECT 單據 = '+領料明細',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoi,序=b.recordno, bom序='',民國年  = b.drdate ,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
												FROM 
												(select * from  BatchProcess_Drawd ) as a	
												inner join 
												(select * from Drawd where len(stnoi) > 0 and ittrait != '1') as b  --
												on 
												a.bomid = b.bomid
											UNION ALL																					    		
												SELECT 單據 = '-領料明細',異動數量 = -1*a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoo,序=b.recordno, bom序='',民國年  = b.drdate ,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
												FROM 
												(select * from  BatchProcess_Drawd ) as a	
												inner join 
												(select * from Drawd where len(stnoo) > 0 and ittrait != '1' ) as b -- 
												on 
												a.bomid = b.bomid
		
											UNION ALL																					    		
												SELECT 單據 = '+進貨明細',異動數量  = a.Qty,單據編號 = b.bsno,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.bsdate ,西元年 = b.bsdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = bShop.fano,客戶或廠商簡稱=bShop.faname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
												FROM 
												(select * from BatchProcess_bShopd ) as a	
												inner join 
												(select * from bShopd where  ittrait != '1' ) as b  --
												on 
												a.bomid = b.bomid
												left join bShop
												on
												b.bsno = bShop.bsno
											UNION ALL																					    		
												SELECT 單據 = '-銷貨明細',異動數量  = -1*a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 =  Sale.cuno,客戶或廠商簡稱= Sale.cuname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
												FROM                                                 
												(select * from BatchProcess_Saled ) as a	
												inner join 
												(select * from Saled where ittrait != '1') as b 
												on 
												a.bomid = b.bomid
												left join Sale
												on
												b.sano = Sale.sano
											UNION ALL																						    		
												SELECT 單據 = '+銷退明細',異動數量  = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = rSale.cuno,客戶或廠商簡稱=rSale.cuname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
												FROM 
												(select * from BatchProcess_rSaleD ) as a	
												inner join 
												(select * from rSaleD where ittrait != '1') as b 
												on 
												a.bomid = b.bomid
												left join rSale
												on
												b.sano = rSale.sano
											UNION ALL																						    		
												SELECT 單據 = '-進退明細',異動數量  = -1*a.Qty,單據編號 = b.bsno,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.bsdate ,西元年 = b.bsdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = rShop.fano,客戶或廠商簡稱=rShop.faname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
												FROM 
												(select * from BatchProcess_rShopD ) as a	
												inner join 
												(select * from rShopD where  ittrait != '1' ) as b  --
												on 
												a.bomid = b.bomid
												left join rShop
												on
												b.bsno = rShop.bsno
											UNION ALL
											-- SELECT 單據 = '+入庫子階',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoi,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
											--  FROM BatchProcess_GarnerBom as a															    
											-- inner join 																				    
											-- (select * from Garnerd where len(stnoi) > 0) as b 													
											-- on 																						    
											-- a.bomid = b.bomid	
											--UNION ALL	
												SELECT 單據 = '-入庫子階',異動數量 = -1*a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
												FROM 
												(select * from BatchProcess_GarnerBom  ) as a															    
												inner join 																				    
												(select * from Garnerd where len(stnoo) > 0 and ittrait = '2') as b 											    
												on 																						    
												a.bomid = b.bomid		
											UNION ALL
												SELECT 單據 = '+領料子階',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoi,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.drdate,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
												FROM 
												(select * from BatchProcess_Drawbom ) as a															    
												inner join 																				    
												(select * from Drawd where len(stnoi) > 0 and ittrait = '1') as b 		--									    
												on 																						    
												a.bomid = b.bomid		
											UNION ALL
												SELECT 單據 = '-領料子階',異動數量 = -1*a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.drdate,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
												FROM 
												(select * from BatchProcess_Drawbom ) as a																    
												inner join 																				    
												(select * from Drawd where len(stnoo) > 0 and ittrait = '1') as b 		--									    
												on 																						    
												a.bomid = b.bomid		
											UNION ALL
												SELECT 單據 = '+銷退子階',異動數量 = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = rSale.cuno,客戶或廠商簡稱=rSale.cuname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
												FROM  
												(select * from BatchProcess_rSaleBom  ) as a																    
												inner join 																				    
												(select * from rSaled  where ittrait = '1') as b 											    
												on 																						    
												a.bomid = b.bomid	
												left join rSale
												on
												b.sano = rSale.sano
											UNION ALL
												SELECT 單據 = '-銷貨子階',異動數量 = -1*a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = sale.cuno,客戶或廠商簡稱 = sale.cuname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
												FROM 
												(select * from BatchProcess_SaleBom  ) as a															    
												inner join 																				    
												(select * from Saled where ittrait = '1') as b 											    
												on 																						    
												a.bomid = b.bomid	
												left join sale
												on
												b.sano = sale.sano


                                            UNION ALL
                                                SELECT 單據 = '+調整',異動數量 = a.Qty,單據編號 = a.adno,異動倉庫 = a.stno,序=a.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = a.addate,西元年 = a.addate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱 = '',倉庫名稱 = (select top 1 stname from stkroom where stno =  a.stno )
                                                FROM 
                                                BatchProcess_adjustd as a 



												) Z			
												where 1 = 1
";
        string 前期Sql_下半部 = @"
							)前期 
							GROUP BY Bno
						) 前期數量表
						ON 前期數量表.Bno = BatchInformation.Bno WHERE 前期數量 > 0 
";
        #endregion
        #region 本期Sql
        string 本期Sql = @"
--本期過程	
						SELECT BatchInformation.Bno, 民國年,西元年,單據,異動數量,結餘數量 = -99999999999,單據編號,明細序號 = 序,bom序,客戶或廠商編號,客戶或廠商簡稱,異動倉庫,倉庫名稱,產品編號 = Itno,品名規格 = (select top 1 itname from item where item.itno = BatchInformation.Itno),製造商編號 = Fano,製造商簡稱 = (select top 1 faname1 from fact where fact.fano = BatchInformation.Fano),批次號碼 = Batchno,製造日期 = [Date],有效日期 = Date1 FROM 
							(
								SELECT 單據 = '+入庫明細',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoi,序=b.recordno, bom序='',民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
								FROM 
								(select * from BatchProcess_Garnerd ) as a																    
								inner join 																				    
								(select * from Garnerd where len(stnoi) > 0 and ittrait != '1') as b 	--									    
								on 																						    
								a.bomid = b.bomid		
																								    
							UNION ALL																					    
								SELECT 單據 = '-入庫明細',異動數量 = -1 * a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序='',民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
								FROM 
								(select * from BatchProcess_Garnerd ) as a	
								inner join 
								(select * from Garnerd where len(stnoo) > 0 and ittrait = '3') as b   --
								on 
								a.bomid = b.bomid
							UNION ALL																						    		
								SELECT 單據 = '+領料明細',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoi,序=b.recordno, bom序='',民國年  = b.drdate ,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
								FROM 
								(select * from  BatchProcess_Drawd ) as a	
								inner join 
								(select * from Drawd where len(stnoi) > 0 and ittrait != '1') as b  --
								on 
								a.bomid = b.bomid
							UNION ALL																					    		
								SELECT 單據 = '-領料明細',異動數量 = -1*a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoo,序=b.recordno, bom序='',民國年  = b.drdate ,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
								FROM 
								(select * from  BatchProcess_Drawd ) as a	
								inner join 
								(select * from Drawd where len(stnoo) > 0 and ittrait != '1' ) as b -- 
								on 
								a.bomid = b.bomid
								
							UNION ALL																					    		
								SELECT 單據 = '+進貨明細',異動數量  = a.Qty,單據編號 = b.bsno,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.bsdate ,西元年 = b.bsdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = bShop.fano,客戶或廠商簡稱=bShop.faname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
								FROM 
								(select * from BatchProcess_bShopd ) as a	
								inner join 
								(select * from bShopd where  ittrait != '1' ) as b  --
								on 
								a.bomid = b.bomid
								left join bShop
								on
								b.bsno = bShop.bsno
								
							UNION ALL																					    		
								SELECT 單據 = '-銷貨明細',異動數量  = -1*a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 =  Sale.cuno,客戶或廠商簡稱= Sale.cuname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
								FROM 
								(select * from BatchProcess_Saled ) as a	
								inner join 
								(select * from Saled where ittrait != '1') as b 
								on 
								a.bomid = b.bomid
								left join Sale
								on
								b.sano = Sale.sano
							UNION ALL																						    		
								SELECT 單據 = '+銷退明細',異動數量  = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = rSale.cuno,客戶或廠商簡稱=rSale.cuname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
								FROM 
								(select * from BatchProcess_rSaleD ) as a	
								inner join 
								(select * from rSaleD where ittrait != '1') as b 
								on 
								a.bomid = b.bomid
								left join rSale
								on
								b.sano = rSale.sano
							UNION ALL																						    		
								SELECT 單據 = '-進退明細',異動數量  = -1*a.Qty,單據編號 = b.bsno,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.bsdate ,西元年 = b.bsdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = rShop.fano,客戶或廠商簡稱=rShop.faname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
								FROM 
								(select * from BatchProcess_rShopD ) as a	
								inner join 
								(select * from rShopD where  ittrait != '1' ) as b  --
								on 
								a.bomid = b.bomid
								left join rShop
								on
								b.bsno = rShop.bsno
							UNION ALL
							-- SELECT 單據 = '+入庫子階',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoi,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
							--  FROM BatchProcess_GarnerBom as a															    
							-- inner join 																				    
							-- (select * from Garnerd where len(stnoi) > 0) as b 													
							-- on 																						    
							-- a.bomid = b.bomid	
							--UNION ALL	
								SELECT 單據 = '-入庫子階',異動數量 = -1*a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
								FROM 
								(select * from BatchProcess_GarnerBom  ) as a															    
								inner join 																				    
								(select * from Garnerd where len(stnoo) > 0 and ittrait = '2') as b 											    
								on 																						    
								a.bomid = b.bomid		
							UNION ALL
								SELECT 單據 = '+領料子階',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoi,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.drdate,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
								FROM 
								(select * from BatchProcess_Drawbom ) as a															    
								inner join 																				    
								(select * from Drawd where len(stnoi) > 0 and ittrait = '1') as b 		--									    
								on 																						    
								a.bomid = b.bomid		
							UNION ALL
								SELECT 單據 = '-領料子階',異動數量 = -1*a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.drdate,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
								FROM 
								(select * from BatchProcess_Drawbom ) as a																    
								inner join 																				    
								(select * from Drawd where len(stnoo) > 0 and ittrait = '1') as b 		--									    
								on 																						    
								a.bomid = b.bomid		
							UNION ALL
								SELECT 單據 = '+銷退子階',異動數量 = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = rSale.cuno,客戶或廠商簡稱=rSale.cuname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
								FROM  
								(select * from BatchProcess_rSaleBom  ) as a																    
								inner join 																				    
								(select * from rSaled  where ittrait = '1') as b 											    
								on 																						    
								a.bomid = b.bomid	
								left join rSale
								on
								b.sano = rSale.sano
							UNION ALL
								SELECT 單據 = '-銷貨子階',異動數量 = -1*a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = sale.cuno,客戶或廠商簡稱 = sale.cuname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
								FROM 
								(select * from BatchProcess_SaleBom  ) as a															    
								inner join 																				    
								(select * from Saled where ittrait = '1') as b 											    
								on 																						    
								a.bomid = b.bomid	
								left join sale
								on
								b.sano = sale.sano



                            UNION ALL
                                SELECT 單據 = '+調整',異動數量 = a.Qty,單據編號 = a.adno,異動倉庫 = a.stno,序=a.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = a.addate,西元年 = a.addate1,a.Bno,a.Bomid,a.Rec,客戶或廠商編號 = '',客戶或廠商簡稱 = '',倉庫名稱 = (select top 1 stname from stkroom where stno =  a.stno )
                                FROM 
                                BatchProcess_adjustd as a 



								) Z
						
						inner join 
							    BatchInformation on Z.Bno = BatchInformation.Bno		

								where 1 = 1
";
        #endregion
        string Orderbyitno = " ORDER BY 產品編號,製造商編號,批次號碼,民國年,單據編號";
        string Orderbybatchno = " ORDER BY 批次號碼,製造商編號,產品編號,民國年,單據編號";
        string 前期where = "";
        string 本期where = "";
        string AllWhere = " where 1 = 1";
        public BatchInfob()
        {
            InitializeComponent();
        }

        public BatchInfob(TextBoxT date, TextBoxT date1, TextBoxT itno, TextBoxT itno1, TextBoxT fano, TextBoxT fano1, TextBoxT BatchNo, TextBoxT BatchNo1, TextBoxT Manufacturingdate, TextBoxT Manufacturingdate1, TextBoxT ExpiryDate, TextBoxT ExpiryDate1, TextBoxT StNo, TextBoxT StNo1)
        {
            InitializeComponent();
            #region set where string and set SQL paramaters
            this.date = date;
            this.date1 = date1;
            this.itno = itno;
            this.itno1 = itno1;
            this.fano = fano;
            this.fano1 = fano1;
            this.BatchNo = BatchNo;
            this.BatchNo1 = BatchNo1;
            this.Manufacturingdate = Manufacturingdate;
            this.Manufacturingdate1 = Manufacturingdate1;
            this.ExpiryDate = ExpiryDate;
            this.ExpiryDate1 = ExpiryDate1;
            this.StNo = StNo;
            this.StNo1 = StNo1;
            異動數量.Set庫存數量小數();
            結餘數量.Set庫存數量小數();
            qury = new List<Button> { buttonitno, buttonbatchno };
            if(date.Text.Trim().Length > 0 )
            {
                if (Common.User_DateTime == 1)
                {
                    本期where += " and 民國年 >= @date1 ";
                    前期where += " and 民國年 <  @date1 ";
                }
                else 
                {
                    本期where += " and 西元年 >= @date1 ";
                    前期where += " and 西元年 <  @date1 ";
                }
                par.AddWithValue("date1", date.Text.Trim());
            }

            if (date1.Text.Trim().Length > 0)
            {
                if (Common.User_DateTime == 1)
                {
                    本期where += " and 民國年 <= @date2 ";
                    if (date.Text.Trim().Length == 0 && date1.Text.Trim().Length > 0)
                    {
                        前期where += " and 民國年 < @date2 ";
                    }
                }
                else
                {
                    本期where += " and 西元年 <= @date2 ";
                    if (date.Text.Trim().Length == 0 && date1.Text.Trim().Length > 0)
                    {
                        前期where += " and 西元年 < @date2 ";
                    }
                }
                par.AddWithValue("date2", date1.Text.Trim());
            }

            if (StNo.Text.Trim().Length > 0)
            {
                //本期where += " and 異動倉庫 = @StNo ";
                //前期where += " and 異動倉庫 = @StNo ";
                AllWhere += " and 異動倉庫 >= @StNo ";
                par.AddWithValue("StNo", StNo.Text.Trim());
            }

            if (StNo1.Text.Trim().Length > 0)
            {
                //本期where += " and 異動倉庫 = @StNo ";
                //前期where += " and 異動倉庫 = @StNo ";
                AllWhere += " and 異動倉庫 <= @StNo1 ";
                par.AddWithValue("StNo1", StNo1.Text.Trim());
            }

            if (itno.Text.Trim().Length > 0)
            {
                AllWhere += " and 產品編號 >= @itno ";

                par.AddWithValue("itno", itno.Text.Trim());
            }
            if (itno1.Text.Trim().Length > 0)
            {
                AllWhere += " and 產品編號 <= @itno1 ";
                par.AddWithValue("itno1", itno1.Text.Trim());
            }

            if (fano.Text.Trim().Length > 0)
            {
                AllWhere += " and 製造商編號 >= @fano ";

                par.AddWithValue("fano", fano.Text.Trim());
            }
            if (fano1.Text.Trim().Length > 0)
            {
                AllWhere += " and 製造商編號 <= @fano1 ";
                par.AddWithValue("fano1", fano1.Text.Trim());
            }

            if (BatchNo.Text.Trim().Length > 0)
            {
                AllWhere += " and 批次號碼 >= @BatchNo ";
                par.AddWithValue("BatchNo", BatchNo.Text.Trim());
            }
            if (BatchNo1.Text.Trim().Length > 0)
            {
                AllWhere += " and 批次號碼 <= @BatchNo1 ";
                par.AddWithValue("BatchNo1", BatchNo1.Text.Trim());
            }
            if (Manufacturingdate.Text.Trim().Length > 0)
            {
                AllWhere += " and 製造日期 >= @Manufacturingdate ";
                par.AddWithValue("Manufacturingdate", Manufacturingdate.Text.Trim());
            }
            if (Manufacturingdate1.Text.Trim().Length > 0)
            {
                AllWhere += " and 製造日期 <= @Manufacturingdate1 ";
                par.AddWithValue("Manufacturingdate1", Manufacturingdate1.Text.Trim());
            }

            if (ExpiryDate.Text.Trim().Length > 0)
            {
                AllWhere += " and 有效日期 >= @ExpiryDate ";
                par.AddWithValue("ExpiryDate", ExpiryDate.Text.Trim());
            }
            if (ExpiryDate1.Text.Trim().Length > 0)
            {
                AllWhere += " and 有效日期 <= @ExpiryDate1 ";
                par.AddWithValue("ExpiryDate1", ExpiryDate1.Text.Trim());
            }

            #endregion
            dt.Clear();
            SetButtonColor();
            buttonitno.ForeColor = Color.Red;
            SearchUserReport();
            //buttonitno_Click(null, null);
        }

        private void BatchInfob_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1) 民國年.Visible = true; else 西元年.Visible = true;
            string srtSQL =
@" select 序號1 = '', 序號 = ROW_NUMBER() OVER(PARTITION BY Bno ORDER BY 產品編號,製造商編號,批次號碼,民國年,單據編號 asc),* from ( "
+ 前期Sql_上半部
+ 前期where
+ 前期Sql_下半部
+ @" union all "
+ 本期Sql
+ 本期where
+ @" ) as 異動過程表 "
+ AllWhere;
            if (buttonitno.ForeColor == Color.Red)
            { 
                srtSQL += Orderbyitno; 
            }
            if (buttonbatchno.ForeColor == Color.Red)
            {
                srtSQL += Orderbybatchno;
            } 
            SQL.ExecuteNonQuery(srtSQL, par, dt);
            計算結餘數量(dt);
            dataGridViewT1.DataSource = dt;
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Excel);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Print);
        }

        private void dataintodocument(RptMode rptMode)
        {
            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                //報表參數
                List<string> Paramaters = new List<string>() { "User_DateTime", Common.User_DateTime.ToString(),"日期區間",Date.AddLine(date.Text) + "～"+ Date.AddLine(date1.Text)};
                FastReport.Paramaters = Paramaters;
                string 報表名稱 = "";
                if (rd1.Checked)
                {
                    if (buttonitno.ForeColor == Color.Red)
                    {
                        報表名稱 = "產品批號庫存明細表_產品排序";
                    }
                    else
                    {
                        報表名稱 = "產品批號庫存明細表_批次排序";
                    }
                }
                else
                {
                    if (buttonitno.ForeColor == Color.Red)
                    {
                        報表名稱 = "產品批號庫存明細表_產品排序_自定一";
                    }
                    else
                    {
                        報表名稱 = "產品批號庫存明細表_批次排序_自定一";
                    }
                }

                string ReportPath = Common.reportaddress + "ReportG\\" + 報表名稱 + ".frx";

                if (File.Exists(ReportPath) == false)
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //三行註腳
                string txtend = "";
                if (rd6.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (rd7.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (rd8.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (rd9.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (rd10.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else txtend = "";
                FastReport.dy.Add("txtend", txtend);
                FastReport.dy.Add("txtstart", Common.Sys_StcPnName);
                FastReport.PreView(ReportPath, dt, "Data_", null, "", rptMode, 報表名稱);
            }
        }

        private void 計算結餘數量(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["異動數量"] = dt.Rows[i]["異動數量"].ToDecimal("f" + Common.Q);
                dt.Rows[i]["結餘數量"] = dt.Rows[i]["結餘數量"].ToDecimal("f" + Common.Q);
                dt.Rows[i]["民國年"] = Date.AddLine(dt.Rows[i]["民國年"].ToString());
                dt.Rows[i]["西元年"] = Date.AddLine(dt.Rows[i]["西元年"].ToString());
                dt.Rows[i]["序號1"] = dt.Rows[i]["序號"].ToString().Trim();
                if (dt.Rows[i]["序號"].ToString().Trim() == "1")  //第一筆
                {
                    dt.Rows[i]["結餘數量"] = dt.Rows[i]["異動數量"].ToString();
                }
                else // dt.Rows[i]["序號"].ToString() > "1"
                {
                    dt.Rows[i]["結餘數量"] = (dt.Rows[i-1]["結餘數量"].ToDecimal()) + (dt.Rows[i]["異動數量"].ToDecimal());
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt.Excel匯出並開啟(this.Text);
            }
            else if (keyData == Keys.F11)
                this.Dispose();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("是否要編輯報表?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                    return;
                dataintodocument(RptMode.Design);
            }
        }

        private void buttonitno_Click(object sender, EventArgs e)
        {
            dt.Clear();
            SetButtonColor();
            buttonitno.ForeColor = Color.Red;
            SearchUserReport();
            BatchInfob_Load(null, null);
        }

        private void buttonbatchno_Click(object sender, EventArgs e)
        {
            dt.Clear();
            SetButtonColor();
            buttonbatchno.ForeColor = Color.Red;
            SearchUserReport(); 
            BatchInfob_Load(null, null);
        }

        void SearchUserReport()
        {
            //rd1.Checked = true;
            if (buttonitno.ForeColor == Color.Red)
                radioT1.SetUserDefineRpt("產品批號庫存明細表_產品排序_自定一.frx", @"ReportG\");
            if (buttonbatchno.ForeColor == Color.Red)
                radioT1.SetUserDefineRpt("產品批號庫存明細表_批次排序_自定一.frx", @"ReportG\");
        }

        void SetButtonColor()
        {
            qury.ForEach(r => r.ForeColor = SystemColors.ControlText);
        }
    }
}
