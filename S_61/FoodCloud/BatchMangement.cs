using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.SOther;
using S_61.Basic;
using System.Data.SqlClient;

namespace S_61
{
    public partial class BatchMangement : Formbase
    {
        JBS.JS.Item jItem;
        DataTable dt = new DataTable(); //批次資訊
        List<TextBoxbase> list = new List<TextBoxbase>();
        string pk = "";
        public BatchMangement()
        {
            InitializeComponent();
        }

        private void BatchMangement_Load(object sender, EventArgs e)
        {
            this.list = this.getEnumMember();
            總庫存量.Set庫存數量小數();
            總庫存量.Set庫存數量小數();
            刪除.Visible = false;
            if (Common.User_DateTime == 1)
            {
                製造日期.MaxInputLength = 7;
                有效日期.MaxInputLength = 7;
            }
            else
            {
                製造日期.MaxInputLength = 8;
                有效日期.MaxInputLength = 8;
            }
            this.jItem = new JBS.JS.Item();
            pk = jItem.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            pk = jItem.Prior();
            writeToTxt(pk);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            pk = jItem.Top();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jItem.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            pk = jItem.Bottom();
            writeToTxt(pk);
        }

        private void writeToTxt(string pk,SqlCommand cmd = null)
        {
            dataGridViewT1.DataSource = null;
            dt.Clear();
            string sql_批次 = "select ROW_NUMBER() OVER( ORDER BY a.fano,a.BatchNo) AS 序號,*,Faname = (select top 1 faname1 from fact where fano = a.fano),總庫存量 = isnull((select sum(StnoQty) from BatchStock where Bno =a.bno ),0),分倉庫存 ='分倉庫存',異動過程='異動過程',原料來源='原料來源',追蹤 = '追蹤',追溯 = '追溯',刪除 = '刪除'  from BatchInformation as a where itno = @itno Order By a.fano,a.BatchNo";
            parameters par = new parameters("itno", pk);
            SQL.ExecuteReader("select top 1 * from item where itno = @itno", par, 
                r =>
                {
                    ItNo.Text   = r["itno"].ToString();
                    ItName.Text = r["ItName"].ToString();
                    string ittrait = r["ItTrait"].ToString();

                    switch (ittrait) 
                    {
                        case "1" :
                            ItTrait.Text = "組合品";
                            原料來源.Visible = false;
                            break;
                        case "2":
                            ItTrait.Text = "組裝品";
                            原料來源.Visible = true;
                            break;
                        case "3":
                            ItTrait.Text = "單一商品";
                            原料來源.Visible = false;
                            break;

                    }
                    追溯.Visible = true;
                    追蹤.Visible = true;
                    異動過程.Visible = true;
                    分倉庫存.Visible = true;
                    dataGridViewT1.ReadOnly = true;

                    SQL.ExecuteNonQuery(sql_批次, par, dt,cmd);
                },cmd);
            dataGridViewT1.DataSource = dt;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (ItNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new FrmItemb())
            {
                frm.TSeekNo = ItNo.Text.Trim();
                var dl = frm.ShowDialog(this);

                if (dl == DialogResult.OK)
                {
                    writeToTxt(frm.TResult);
                }
                else if (dl == DialogResult.Yes)
                {
                    btnTop_Click(null, null);
                }
            }
        }




        private void ItNo_Validating(object sender, CancelEventArgs e)
        {

            if (btnCancel.Focused) return;

            if (!jItem.IsExist(ItNo.Text.Trim()))
            {
                e.Cancel = true;

                using (var frm = new FrmItemb())
                {
                    frm.TSeekNo = ItNo.Text.Trim();
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            pk = frm.TResult;
                            writeToTxt(pk);
                            break;
                    }
                }
            }
            else 
            {
                writeToTxt(ItNo.Text.Trim());
            }
            ThisFormState();
        }
        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmItemb())
            {
                frm.TSeekNo = ItNo.Text.Trim();
                if (DialogResult.OK == frm.ShowDialog())
                    writeToTxt(frm.TResult);
            }
        }




        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].HeaderText == "分倉庫存")
            {
                using (BatchStock frm = new BatchStock())
                {
                    frm._Bno = dt.Rows[e.RowIndex]["bno"].ToString();
                    frm._BatchNo = dt.Rows[e.RowIndex]["BatchNo"].ToString();
                    frm._FaName = dt.Rows[e.RowIndex]["faname"].ToString();
                    frm._ItName = ItName.Text;
                    frm.ShowDialog();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].HeaderText == "異動過程")
            {
                using (BatchTransactionProcess frm = new BatchTransactionProcess())
                {
                    frm._Bno = dt.Rows[e.RowIndex]["bno"].ToString();
                    frm._BatchNo = dt.Rows[e.RowIndex]["BatchNo"].ToString();
                    frm._FaName = dt.Rows[e.RowIndex]["faname"].ToString();
                    frm._ItName = ItName.Text;
                    frm.ShowDialog();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].HeaderText == "原料來源")
            {
                using (BatchProductSource frm = new BatchProductSource())
                {
                    frm._Bno = dt.Rows[e.RowIndex]["bno"].ToString();
                    frm._BatchNo = dt.Rows[e.RowIndex]["BatchNo"].ToString();
                    frm._FaName = dt.Rows[e.RowIndex]["faname"].ToString();
                    frm._ItName = ItName.Text;
                    frm.ShowDialog();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].HeaderText == "追蹤")
            {
                using (BatchTrace frm = new BatchTrace("追蹤"))
                {
                    frm._Bno = dt.Rows[e.RowIndex]["bno"].ToString();
                    frm._BatchNo = dt.Rows[e.RowIndex]["BatchNo"].ToString();
                    frm._FaName = dt.Rows[e.RowIndex]["faname"].ToString();
                    frm._ItName = ItName.Text;
                    frm.dt = GetBatchTraceHistory_Dt("追蹤", dt.Rows[e.RowIndex]["bno"].ToString());
                    frm.ShowDialog();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].HeaderText == "追溯")
            {
                using(BatchTrace frm = new BatchTrace("追溯"))
                {
                    frm._Bno = dt.Rows[e.RowIndex]["bno"].ToString();
                    frm._BatchNo = dt.Rows[e.RowIndex]["BatchNo"].ToString();
                    frm._FaName = dt.Rows[e.RowIndex]["faname"].ToString();
                    frm._ItName = ItName.Text;
                    frm.dt = GetBatchTraceHistory_Dt("追溯", dt.Rows[e.RowIndex]["bno"].ToString());
                    frm.ShowDialog();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].HeaderText == "刪除")
            {
                if (SQL.UsingTransction(Delete, dt.Rows[e.RowIndex]["bno"].ToString()))
                {
                    writeToTxt(dt.Rows[e.RowIndex]["itno"].ToString());
                    ThisFormState();
                    MessageBox.Show("刪除成功!!");
                }
            }
        }

        private bool Delete(SqlCommand cmd,string bno)
        {
            string SrtSql_批次異動過程 = new BatchTransactionProcess().SrtSql_批次異動過程;
            DataTable dt_異動過程 = new DataTable();
            SQL.ExecuteNonQuery(SrtSql_批次異動過程, new parameters("Bno", bno), dt_異動過程,cmd);

            if (dt_異動過程.Rows.Count == 0) //如果沒有批次異動過程才能刪除
            {
                SQL.ExecuteNonQuery(
                    @"delete BatchInformation where bno = @bno 
                      delete BatchStock where bno = @bno "          
                    , new parameters("Bno", bno), dt_異動過程, cmd);
                return true; //sql commit
            }
            else
            {
                MessageBox.Show("存在批次異動資料，不得刪除!");
                return false;//sql Rollback
            }
        }

        private DataTable GetBatchTraceHistory_Dt(string str,string bno)
        {
            DataTable dt = new DataTable();
            #region strSql_追溯
            string strSql_追溯 =
    @"
                    SELECT * FROM 
(															    		
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
 ) Z
order by 民國年 desc,單據 desc,單據編號 desc
                    ";
            #endregion
            #region strSql_追蹤
            string strSql_追蹤 = @"SELECT * FROM 
(																				    		
 SELECT 成品編號 ='',成品名稱 = '',單據 = '-銷貨明細',異動數量  = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = Sale.cuno,客戶簡稱= Sale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
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
 SELECT 成品編號 ='',成品名稱 = '',單據 = '+銷退明細',異動數量  = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = rSale.cuno,客戶簡稱 = rSale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
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
 SELECT 成品編號 ='',成品名稱 = '',單據 = '+銷退子階',異動數量 = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = rSale.cuno,客戶簡稱 = rSale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
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
 SELECT 成品編號 ='',成品名稱 = '',單據 = '-銷貨子階',異動數量 = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = sale.cuno,客戶簡稱 = sale.cuname1,廠商編號 ='',廠商簡稱 ='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
 FROM 
 (select * from BatchProcess_SaleBom where bno = @Bno ) as a															    
 inner join 																				    
 (select * from Saled where ittrait = '1') as b 											    
 on 																						    
 a.bomid = b.bomid	
 left join sale
  on
 b.sano = sale.sano
 ) Z
order by 民國年 desc,單據 desc,單據編號 desc";

            #endregion

            if (str == "追溯")
            {
                SQL.ExecuteNonQuery(strSql_追溯, new parameters("Bno", bno), dt);
            }
            else 
            {
                SQL.ExecuteNonQuery(strSql_追蹤, new parameters("Bno", bno), dt);

                if (ItTrait.Text == "組裝品" || ItTrait.Text == "單一商品")
                {
                    #region 組裝品 or 單一商品時，關聯出成品的出貨紀錄。
                    string strSql_原料相關的成品 = @"			    
SELECT distinct(入庫_加.Bno) FROM 
(
 SELECT 單據 = '-入庫子階',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
 FROM 
 (select * from BatchProcess_GarnerBom where bno = @bno ) as a			--海苔												    
 inner join 																				    
 (select * from Garnerd where len(stnoo) > 0 and ittrait = '2') as b 											    
 on 																						    
 a.bomid = b.bomid		
) AS 入庫bom_減
INNER JOIN
(
 SELECT 單據 = '+入庫明細',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoi,序=b.recordno, bom序='',民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
 FROM 
 (select * from BatchProcess_Garnerd ) as a																    
 inner join 																				    
 (select * from Garnerd where len(stnoi) > 0 and ittrait != '1') as b 	--									    
 on 																						    
 a.bomid = b.bomid	
 )AS 入庫_加
 on 入庫bom_減.Bomid = 入庫_加.Bomid";
                    DataTable dt_原料相關的成品批次Key = new DataTable();
                    SQL.ExecuteNonQuery(strSql_原料相關的成品, new parameters("bno", bno), dt_原料相關的成品批次Key);
                    string strSql_原料相關成品批次銷貨紀錄 =
                        @"

	 SELECT 成品編號 = BF.itno,成品名稱 = (select top 1 itname from item where itno = BF.itno),* FROM 
	(																			    		
	 SELECT 成品編號 ='',單據 = '-銷貨明細',異動數量  = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = Sale.cuno,客戶簡稱= Sale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
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
	 SELECT 成品編號 ='',單據 = '+銷退明細',異動數量  = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = rSale.cuno,客戶簡稱 = rSale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
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
	 SELECT 成品編號 ='',單據 = '+銷退子階',異動數量 = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = rSale.cuno,客戶簡稱 = rSale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
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
	 SELECT 成品編號 ='',單據 = '-銷貨子階',異動數量 = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = sale.cuno,客戶簡稱 = sale.cuname1,廠商編號 ='',廠商簡稱 ='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
	 FROM 
	 (select * from BatchProcess_SaleBom where bno = @Bno ) as a															    
	 inner join 																				    
	 (select * from Saled where ittrait = '1') as b 											    
	 on 																						    
	 a.bomid = b.bomid	
	 left join sale
	  on
	 b.sano = sale.sano
	 ) Z
	 inner join
	 (select * from BatchInformation where  bno = @Bno) as  Bf
	 ON 
	 Z.BNO = Bf.BNO
";
                    DataTable _dt = new DataTable();
                    parameters _par = new parameters();
                    for (int i = 0; i < dt_原料相關的成品批次Key.Rows.Count; i++)
                    {
                        _par.Clear();
                        _par.AddWithValue("bno", dt_原料相關的成品批次Key.Rows[i]["bno"].ToString());
                        SQL.ExecuteNonQuery(strSql_原料相關成品批次銷貨紀錄, _par, _dt);
                        dt.Merge(_dt); //將材料的成品資料加入
                    }
                    #endregion
                }
            }
            return dt;
               
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0) return;
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            ThisFormState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BatchUpdate(dt, 1000) == false)//如果存檔失敗
                return;
            else   //如果存檔成功
            {
                Common.SetTextState(this.FormState = FormEditState.None, ref list);
                writeToTxt(pk);
                ItNo.ReadOnly = false;
                刪除.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridViewT1.ReadOnly = true;
            Common.SetTextState(FormState = FormEditState.None, ref list);
            writeToTxt(pk);
            ItNo.ReadOnly = false;
            刪除.Visible = false;
        }

        void ThisFormState()
        {
            #region//修改時,值不變
            if (this.FormState == FormEditState.Modify)
            {
                刪除.Visible = true;
                追溯.Visible = false;
                追蹤.Visible = false;
                原料來源.Visible = false;
                異動過程.Visible = false;
                分倉庫存.Visible = false;
                
                dataGridViewT1.ReadOnly = false;
                dataGridViewT1.Columns["序號"].ReadOnly = true;
                dataGridViewT1.Columns["廠商編號"].ReadOnly = true;
                dataGridViewT1.Columns["製造廠商"].ReadOnly = true;
                dataGridViewT1.Columns["總庫存量"].ReadOnly = true;
            }
            #endregion
        }

        public bool BatchUpdate(DataTable dt, Int32 batchSize)
        {
            //限制:在修改批次編號時，不能夠將a批號 改成b批號，b批號改成c批號(a -> b -> c) 這是因為primary key的限制
            //不修正原因:因為這部分需要檢查很多地方，且發生狀況很少，所以不冒險修改
            // Connect to the AdventureWorks database.
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter ad = new SqlDataAdapter())
            {
                cn.Open();
                using (SqlTransaction tran = cn.BeginTransaction("Transaction"))
                {
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    try
                    {
                        #region 判斷是否可以存檔

                        //1.避免批號空值
                        if (dt.MultiThreadFindIndex("batchno","") >-1)
                        {
                            MessageBox.Show("批次號碼存在空值，請先確認。");
                            return false;
                        }
                        //2.避免同時存在兩個批號
                        DataTable dt_比對 = new DataTable();
                        SQL.ExecuteNonQuery("select * from BatchInformation where itno = @itno", new parameters("itno", dt.Rows[0]["itno"].ToString()), dt_比對, cmd);
                        for (int i = 0; i < dt_比對.Rows.Count; i++)
                        {
                            int count = 0;
                            for (int K = 0; K < dt.Rows.Count; K++)
                            {
                                if (dt_比對.Rows[i]["itno"].ToString() == dt.Rows[K]["itno"].ToString() && dt_比對.Rows[i]["fano"].ToString() == dt.Rows[K]["fano"].ToString() && dt_比對.Rows[i]["batchno"].ToString() == dt.Rows[K]["batchno"].ToString())
                                    count += 1;
                            }
                            if(count > 1)
                            {
                                MessageBox.Show("批次號碼:" + dt.Rows[i]["batchno"].ToString() + "已存在，不得同時存在兩個批號，請先確認。");
                                return false;
                            }
                        }
                        #endregion

                        cmd.CommandText = "update BatchInformation set [Date] = @Date ,[Date1] = @Date1,Batchno = @Batchno where Bno = @Bno";
                        ad.UpdateCommand = cmd;
                        ad.UpdateCommand.Parameters.Add("@Batchno", SqlDbType.NVarChar, 30, "Batchno");
                        ad.UpdateCommand.Parameters.Add("@Date", SqlDbType.NVarChar, 8, "Date");
                        ad.UpdateCommand.Parameters.Add("@Date1", SqlDbType.NVarChar, 8, "Date1");
                        ad.UpdateCommand.Parameters.Add("@Bno", SqlDbType.Int, 10, "Bno");
                        ad.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;

                        // Set the batch size.
                        ad.UpdateBatchSize = batchSize;

                        // Execute the update.
                        ad.Update(dt);

                        // commit
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (btnCancel.Focused ) return;

            var CurrentColumn = dataGridViewT1.Columns[e.ColumnIndex];
            var CurrentEditValue = dataGridViewT1.CurrentCell.EditedFormattedValue.ToString().Trim();
            var CurrentCell = dataGridViewT1.CurrentCell;
            var CuurenRow = dt.Rows[e.RowIndex];

            if (CurrentColumn.Name == "批次號碼")
            {
                if (CurrentEditValue.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("批次號碼不得為空!");
                    e.Cancel = true;
                }
            }
            else if (CurrentColumn.Name == "有效日期" || CurrentColumn.Name == "製造日期")
            {
                if (CurrentColumn.ReadOnly == true) return;
                TextBox tb = new TextBox();
                tb.Text = CurrentEditValue;
                if (Common.User_DateTime == 1 && CurrentEditValue.Length == 8)
                {
                    int year = CurrentEditValue.Substring(0, 4).ToInteger() - 1911;
                    tb.Text = year + CurrentEditValue.Substring(4);
                }
                else if (Common.User_DateTime == 2 && CurrentEditValue.Length == 7)
                {
                    int year = CurrentEditValue.Substring(0, 3).ToInteger() + 1911;
                    tb.Text = year + CurrentEditValue.Substring(4);
                }
                if (!(tb.IsDateTime()))
                {
                    MessageBox.Show("您輸入的日期格是不正確!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }




    }
}
