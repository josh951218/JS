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

namespace S_61
{
    public partial class BatchTransactionProcess : Formbase
    {
        public string _Bno { get; set; }
        public string _BatchNo { get; set; }
        public string _FaName { get; set; }
        public string _ItName { get; set; }
        DataTable dt = new DataTable();
        #region public string SrtSql_批次異動過程
        public string SrtSql_批次異動過程 =
@"
 SELECT * FROM 
(
 SELECT 單據 = '+入庫明細',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoi,序=b.recordno, bom序='',民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
 FROM 
 (select * from BatchProcess_Garnerd where bno = @Bno ) as a																    
 inner join 																				    
 (select * from Garnerd where len(stnoi) > 0 and ittrait != '1') as b 	--									    
 on 																						    
 a.bomid = b.bomid																			    
UNION ALL																					    
 SELECT 單據 = '-入庫明細',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序='',民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
 FROM 
 (select * from BatchProcess_Garnerd where bno = @Bno ) as a	
 inner join 
 (select * from Garnerd where len(stnoo) > 0 and ittrait = '3') as b   --
 on 
 a.bomid = b.bomid
UNION ALL																						    		
 SELECT 單據 = '+領料明細',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoi,序=b.recordno, bom序='',民國年  = b.drdate ,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
 FROM 
 (select * from  BatchProcess_Drawd where bno = @Bno ) as a	
 inner join 
 (select * from Drawd where len(stnoi) > 0 and ittrait != '1') as b  --
 on 
 a.bomid = b.bomid
UNION ALL																					    		
 SELECT 單據 = '-領料明細',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoo,序=b.recordno, bom序='',民國年  = b.drdate ,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
 FROM 
 (select * from  BatchProcess_Drawd where bno = @Bno ) as a	
 inner join 
 (select * from Drawd where len(stnoo) > 0 and ittrait != '1' ) as b -- 
 on 
 a.bomid = b.bomid

UNION ALL																					    		
 SELECT 單據 = '+進貨明細',異動數量  = a.Qty,單據編號 = b.bsno,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.bsdate ,西元年 = b.bsdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 = bShop.fano,廠商簡稱=bShop.faname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
 FROM 
 (select * from BatchProcess_bShopd where bno = @Bno ) as a	
 inner join 
 (select * from bShopd where  ittrait != '1' ) as b  --
 on 
 a.bomid = b.bomid
 left join bShop
 on
 b.bsno = bShop.bsno

UNION ALL																					    		
 SELECT 單據 = '-銷貨明細',異動數量  = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = Sale.cuno,客戶簡稱= Sale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
 FROM 
 (select * from BatchProcess_Saled where bno = @Bno ) as a	
 inner join 
 (select * from Saled where ittrait != '1') as b 
 on 
 a.bomid = b.bomid
  left join Sale
 on
 b.sano = Sale.sano
UNION ALL																						    		
 SELECT 單據 = '+銷退明細',異動數量  = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = rSale.cuno,客戶簡稱 = rSale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
 FROM 
 (select * from BatchProcess_rSaleD where bno = @Bno ) as a	
 inner join 
 (select * from rSaleD where ittrait != '1') as b 
 on 
 a.bomid = b.bomid
  left join rSale
 on
 b.sano = rSale.sano
UNION ALL																						    		
 SELECT 單據 = '-進退明細',異動數量  = a.Qty,單據編號 = b.bsno,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.bsdate ,西元年 = b.bsdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 = rShop.fano,廠商簡稱=rShop.faname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
 FROM 
 (select * from BatchProcess_rShopD where bno = @Bno ) as a	
 inner join 
 (select * from rShopD where  ittrait != '1' ) as b  --
 on 
 a.bomid = b.bomid
  left join rShop
 on
 b.bsno = rShop.bsno
UNION ALL
-- SELECT 單據 = '+入庫子階',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoi,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
--  FROM BatchProcess_GarnerBom as a															    
-- inner join 																				    
-- (select * from Garnerd where len(stnoi) > 0) as b 													
-- on 																						    
-- a.bomid = b.bomid	
--UNION ALL	
 SELECT 單據 = '-入庫子階',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
 FROM 
 (select * from BatchProcess_GarnerBom where bno = @Bno ) as a															    
 inner join 																				    
 (select * from Garnerd where len(stnoo) > 0 and ittrait = '2') as b 											    
 on 																						    
 a.bomid = b.bomid		
UNION ALL
 SELECT 單據 = '+領料子階',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoi,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.drdate,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
 FROM 
 (select * from BatchProcess_Drawbom where bno = @Bno ) as a															    
 inner join 																				    
 (select * from Drawd where len(stnoi) > 0 and ittrait = '1') as b 		--									    
 on 																						    
 a.bomid = b.bomid		
UNION ALL
 SELECT 單據 = '-領料子階',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.drdate,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
 FROM 
  (select * from BatchProcess_Drawbom where bno = @Bno ) as a																    
 inner join 																				    
 (select * from Drawd where len(stnoo) > 0 and ittrait = '1') as b 		--									    
 on 																						    
 a.bomid = b.bomid		
UNION ALL
 SELECT 單據 = '+銷退子階',異動數量 = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = rSale.cuno,客戶簡稱 = rSale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
 FROM  
 (select * from BatchProcess_rSaleBom where bno = @Bno ) as a																    
 inner join 																				    
 (select * from rSaled  where ittrait = '1') as b 											    
 on 																						    
 a.bomid = b.bomid	
 left join rSale
 on
 b.sano = rSale.sano
  UNION ALL
 SELECT 單據 = '-銷貨子階',異動數量 = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = sale.cuno,客戶簡稱 = sale.cuname1,廠商編號 ='',廠商簡稱 ='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
 FROM 
 (select * from BatchProcess_SaleBom where bno = @Bno ) as a															    
 inner join 																				    
 (select * from Saled where ittrait = '1') as b 											    
 on 																						    
 a.bomid = b.bomid	
 left join sale
  on
 b.sano = sale.sano

UNION ALL
SELECT 單據 = '+調整',異動數量 = a.Qty,單據編號 = a.adno,異動倉庫 = a.stno,序=a.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = a.addate,西元年 = a.addate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱 = '',廠商編號 ='',廠商簡稱 ='',倉庫名稱 = (select top 1 stname from stkroom where stno =  a.stno )
FROM 
BatchProcess_adjustd as a where a.bno = @Bno 


 ) Z
order by 民國年 desc,單據 desc,單據編號 desc";
        #endregion
        public BatchTransactionProcess() 
        {
            InitializeComponent();
        }

        private void BatchTransactionProcess_Load(object sender, EventArgs e)
        {
            異動數量.Set庫存數量小數();
            if (Common.User_DateTime == 1)
                西元年.Visible = false;
            else
                民國年.Visible = false;
            ItName.Text = _ItName;
            FaName.Text = _FaName;
            BatchNo.Text = _BatchNo;
           
            SQL.ExecuteNonQuery(SrtSql_批次異動過程, new parameters("bno", _Bno), dt);
            dataGridViewT1.DataSource = dt;
 
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
        


    }
}
