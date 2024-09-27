using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.subMenuFm_2;
using S_61.Basic;
namespace S_61
{
    public partial class Batch : Formbase
    {
        JBS.JS.xEvents xe;
        public DataTable dt ; //傳回之table
        DataTable dt_GridView = new DataTable();//DataGridView1之source
        public string _Fano = "", _Faneme = "", _itno = "", _itname = "", _Qty = "", _date = "", _date2 = "", _stno = "", _stname = "", _cuno = "", _cuname = "", _bomid = "", _rec = "", _BomRec;//上一層明細資訊
        string frm;
        bool isBom; 
        /// <summary>
        /// _frm  為單據名稱
        /// _isBom 如果為true載入的批次資料不會過濾總庫量為0的明細；如果為false則會過濾。
        /// _isBom 為true  的作業包含[領料、入庫、所有BOM]
        /// _isBom 為false 的作業包含[銷貨、銷退、進貨、進退]
        /// 可以取回  為控制[結構取回按鈕]enable得屬性，在單據非編輯狀態下是false；而編輯下開窗是true
        /// </summary>
        public Batch(string _frm,DataTable _dt,bool _isBom = false,bool 可以取回 = true)
        {
            InitializeComponent();
            frm = _frm;
            isBom = _isBom;
            dt = _dt;
            dataGridViewT2.DataSource = _dt;
            gridGetBomD.Enabled = 可以取回;
        }
        private void Batch_Load(object sender, EventArgs e)
        {
            this.xe = new JBS.JS.xEvents();
            Date.SetDateLength();
            Date1.SetDateLength();
            Qty.Set庫存數量小數();
            製造數量.Set庫存數量小數();
            庫存數量.Set庫存數量小數();
            庫存數量1.Set庫存數量小數();
            數量.Set庫存數量小數();
            if (Common.User_DateTime == 1)
            {
                製造日期1.MaxInputLength = 7;
                有效日期1.MaxInputLength = 7;
                製造日期.MaxInputLength = 7;
                有效日期.MaxInputLength = 7;
            }
            else
            {
                製造日期1.MaxInputLength = 8;
                有效日期1.MaxInputLength = 8;
                製造日期.MaxInputLength = 8;
                有效日期.MaxInputLength = 8;
            }



            StNo.Text = _stno;
            StName.Text = _stname;
            if (StName.TrimTextLenth() == 0)
            {
                var row = Common.load("Check", "stkroom", "stno", StNo.Text.Trim());
                if(row != null)
                    StName.Text = row["stname"].ToString().Trim();
            }
            ItNo.Text = _itno;
            Itname.Text = _itname;
            Qty.Text = _Qty.ToDecimal("f" + Common.Q).ToString();

            if (gridGetBomD.Enabled)
            {
                dataGridViewT2.ReadOnly = false;
                for (int i = 0; i < dataGridViewT2.ColumnCount; i++)
                {
                    dataGridViewT2.Columns[i].ReadOnly = true;
                }
                if (frm == "FrmBShop")  //進貨明細 或者是 入庫明細時才可以打批次資訊
                {
                    批次號碼1.ReadOnly = 廠商編號1.ReadOnly = 製造日期1.ReadOnly = 有效日期1.ReadOnly = 數量.ReadOnly = false;
                }
                else if (frm == "FrmGarner")
                {
                    批次號碼1.ReadOnly = 廠商編號1.ReadOnly = 製造日期1.ReadOnly = 有效日期1.ReadOnly = 數量.ReadOnly = false;
                }
                else
                {
                    數量.ReadOnly = false;
                }
            }

            Load_dt_GridView(dt_GridView);
        }
        private void Load_dt_GridView(DataTable dt_GridView)
        {
            dataGridViewT1.DataSource = null;
            string CmdStr ="";
            if (isBom)//領料、入庫、Bom開窗時，不會考慮庫存量
            {
                CmdStr =
@"
select  ROW_NUMBER() OVER( ORDER BY a.fano,BatchNo) AS 序號,a.*,b.stno,b.StnoQty,c.Itname,faname = (select faname1 from fact where fano = a.fano),stname = (select stname from stkroom where stno = b.Stno),修改批號 = '加入' from 
	(select * from batchInformation where itno = @itno) as a
inner join 
	(select * from batchstock where stno = @stno  )as b 
on  a.bno = b.Bno
left join 
	(select * from item where itno = @itno) as c
on  a.itno = c.itno
order by a.fano,BatchNo
";
            }
            else//其它開窗時，會過濾總庫存量為0的
            {
                CmdStr =
@"
select  ROW_NUMBER() OVER( ORDER BY a.fano,BatchNo) AS 序號,a.*,b.stno,b.StnoQty,c.Itname,faname = (select faname1 from fact where fano = a.fano),stname = (select stname from stkroom where stno = b.Stno),修改批號 = '加入' from 
	(select * from batchInformation where itno = @itno) as a
inner join 
	(select * from batchstock where stno = @stno and StnoQty <> 0  )as b 
on  a.bno = b.Bno
left join 
	(select * from item where itno = @itno) as c
on  a.itno = c.itno
order by a.fano,BatchNo
";
            }
            parameters par = new parameters();
            par.AddWithValue("itno", _itno);
            par.AddWithValue("stno", _stno);
            SQL.ExecuteNonQuery(CmdStr, par, dt_GridView);
            dataGridViewT1.DataSource = dt_GridView;
        }


        private void gridAppend_Click(object sender, EventArgs e)
        {
            dataGridViewT2.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();
            DataRow dr = dt.NewRow();
            dr["序號"] = dt.Rows.Count + 1;
            dr["StNo"] = _stno;
            dr["StName"] = _stname;
            dr["bomid"] = _bomid;
            dr["rec"] = _rec;
            dr["itno"] = _itno;
            dr["itname"] = _itname;
            if (dt.Rows.Count == 0)
                dr["qty"] = Qty.Text.Trim().ToDecimal("f" + Common.Q);
            else
                dr["qty"] = "0".ToDecimal("f" + Common.Q);
            dr["StnoQty"] = "0".ToDecimal("f" + Common.Q);
            dr["Date"] = S_61.Basic.Date.GetDateTime(Common.User_DateTime);
            dr["Date1"] = S_61.Basic.Date.GetDateTime(Common.User_DateTime);
            dr["BomRec"] = _BomRec;
            dr["修改批號"] = "刪除";
            if (frm == "FrmBShop")
            {
                dr["Fano"] = _Fano;
                dr["faname"] = _Faneme;
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();

            dataGridViewT2.Rows[dataGridViewT2.Rows.Count - 1].Selected = true;
            dataGridViewT2.CurrentCell = dataGridViewT2["批次號碼1", dataGridViewT2.Rows.Count - 1];
            dataGridViewT2.Focus();
        }
        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT2.SelectedRows.Count == 0) return;
            dt.Rows.RemoveAt(dataGridViewT2.SelectedRows[0].Index);
            dt.AcceptChanges();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["序號"] = (i + 1).ToString();
            }
            dataGridViewT1.Invalidate();
        }
        private void gridGetBomD_Click(object sender, EventArgs e)
        {
            decimal SumQty = 0M;

            foreach (DataRow item in dt.Rows)
            {
                if (item["FaNo"].ToString().Trim().Length == 0)
                {
                    MessageBox.Show("批號製造廠商不可為空");
                    return;
                }
            }


            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i]["ItNo"].ToString().Trim().Length == 0 || dt.Rows[i]["FaNo"].ToString().Trim().Length == 0 || dt.Rows[i]["BatchNo"].ToString().Trim().Length == 0 || dt.Rows[i]["Qty"].ToDecimal() == 0)//過濾 產品編號 及 廠商編號 及 批次編號 為空 之 ROW
                {
                    dt.Rows.RemoveAt(i);
                    dt.AcceptChanges();
                }
                else 
                {
                    SumQty += dt.Rows[i]["qty"].ToDecimal();
                }
            }
            if (SumQty > Qty.Text.ToDecimal())//批次勾稽總數不得大於總用量!
            {
                MessageBox.Show("批次勾稽總數不得大於總用量! 大於總用數:" + (SumQty - Qty.Text.ToDecimal()).ToString("f"+Common.Q), "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.DialogResult = DialogResult.Yes;
            }
        }
        private void gridExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        private void 更正Bno(DataRow dataRow)
        {
            parameters par = new parameters();
            par.AddWithValue("FaNo"   , dataRow["Fano"].ToString());
            par.AddWithValue("ItNo", _itno);
            par.AddWithValue("BatchNo", dataRow["Batchno"].ToString());

            bool 有批次資訊 = SQL.ExecuteReader(@"select top 1 *,faname1=(select faname1 from fact where fano=batchInformation.fano) 
                                                from batchInformation where FaNo = @FaNo and ItNo = @ItNo  and BatchNo = @BatchNo", par,
                r => {
                        dataRow["Date"] = 轉換日期(dataRow["Date"].ToString());
                        dataRow["Date1"] = 轉換日期(r["Date1"].ToString());
                        dataRow["FaNo"] = r["FaNo"].ToString();
                        dataRow["faname"] = r["faname1"].ToString();
                        dataRow["BatchNo"] = r["BatchNo"].ToString();
                        dataRow["Bno"] = r["Bno"].ToString();
                     }
                );

            if (!有批次資訊) 
            {
                dataRow["Bno"] = "";  //清空，存檔時判斷否有bno，而去動作
            }
           
        }
        private void dataGridViewT2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= dataGridViewT2.Rows.Count) return;
            if (dataGridViewT2.Rows.Count == 0) return;
            if (dataGridViewT2.EditingControl == null) return;

            var CurrentColumn = dataGridViewT2.Columns[e.ColumnIndex];

            if (CurrentColumn.Name == "廠商編號1")
            {
                #region 廠商編號1
                if (CurrentColumn.ReadOnly == true) return;
                using (var frm = new S_61.SOther.FrmFactb())
                {
                    frm.TSeekNo = dataGridViewT2.EditingControl.Text.Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewT2.EditingControl.Text = frm.TResult;
                        dt.Rows[e.RowIndex]["fano"] = frm.TResult;
                        dt.Rows[e.RowIndex]["faname"] = SQL.ExecuteScalar("select faname1 from fact where fano =@fano", new parameters("fano", frm.TResult));
                        更正Bno(dt.Rows[e.RowIndex]);
                        dataGridViewT2.InvalidateRow(e.RowIndex);
                    }
                }
                #endregion
            }
        }
        private void dataGridViewT2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT2.ReadOnly || gridDelete.Focused || gridExit.Focused) return;
            if (dataGridViewT2.Rows.Count == 0) return;
            if (dataGridViewT2.EditingControl == null) return;

            var CurrentColumn = dataGridViewT2.Columns[e.ColumnIndex];

            if (CurrentColumn.Name == "批次號碼1")
            {
                if (CurrentColumn.ReadOnly == true) return;

                var BatchNo_New = dataGridViewT2.EditingControl.Text.Trim();
                var BatchNo_Before = dt.Rows[e.RowIndex]["Batchno"].ToString().Trim();
                if (BatchNo_New.Equals(BatchNo_Before)) return;

                dt.Rows[e.RowIndex]["Batchno"] = BatchNo_New;
                更正Bno(dt.Rows[e.RowIndex]);
                dataGridViewT2.InvalidateRow(e.RowIndex);
            }
            else if (CurrentColumn.Name == "廠商編號1")
            {
                #region 廠商編號1
                if (CurrentColumn.ReadOnly == true) return;

                var FaNo_New = dataGridViewT2.EditingControl.Text.Trim();
                var FaNo_Before = dt.Rows[e.RowIndex]["fano"].ToString().Trim();
                if (FaNo_New.Equals(FaNo_Before)) return;

                if (SQL.ExecuteScalar("select count(*) from fact where fano =@fano", new parameters("fano", FaNo_New)) == "0")
                {
                    e.Cancel = true;
                    dataGridViewT2_CellDoubleClick(dataGridViewT2, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
                    return;
                }
                else 
                {
                    dt.Rows[e.RowIndex]["fano"] = FaNo_New;
                    dt.Rows[e.RowIndex]["faname"] = SQL.ExecuteScalar("select faname1 from fact where fano =@fano", new parameters("fano", FaNo_New));
                }
                更正Bno(dt.Rows[e.RowIndex]);
                dataGridViewT2.InvalidateRow(e.RowIndex);
                #endregion
            }
            else if (CurrentColumn.Name == "有效日期1" || CurrentColumn.Name == "製造日期1")
            {
                if (CurrentColumn.ReadOnly == true) return;
                TextBox tb = new TextBox();
                tb.Text = dataGridViewT2.EditingControl.Text.Trim(); ;
                if (!(tb.IsDateTime()))
                {
                    MessageBox.Show("日期格式不正確!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }
        private void dataGridViewT2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= dataGridViewT2.Rows.Count) return;
            if (dataGridViewT2.Columns[e.ColumnIndex].HeaderText == "刪除")
            {
                dt.Rows.RemoveAt(e.RowIndex);
                dt.AcceptChanges();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["序號"] = i + 1;
                }
                dataGridViewT2.Invalidate();
            }
        }

        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].HeaderText == "加入")
            {
                if (dt.MultiThreadFindIndex("bno", dt_GridView.Rows[e.RowIndex]["bno"].ToString()) == -1)
                {
                    dt.ImportRow(dt_GridView.Rows[e.RowIndex]);
                    dt.AcceptChanges();
                    dt.Rows[dt.Rows.Count - 1]["序號"] = dt.Rows.Count ;
                    dt.Rows[dt.Rows.Count - 1]["修改批號"] = "刪除";
                    dt.Rows[dt.Rows.Count - 1]["bomid"] = _bomid;
                    dt.Rows[dt.Rows.Count - 1]["rec"] = _rec;
                    dt.Rows[dt.Rows.Count - 1]["BomRec"] = _BomRec;
                    #region 帶入數量
                    if (dt.Rows.Count == 1) 
                    {
                        // if(Qty.Text  > StnoQty ) 直接帶Qty
                        // else if (Qty <  StnoQty) 帶庫存最大量StnoQty
                        var 需求量 = Qty.Text.ToDecimal("f" + Common.Q);
                        var 庫存量 = dt.Rows[dt.Rows.Count - 1]["StnoQty"].ToDecimal("f" + Common.Q);
                        if (is扣倉())
                        {  
                            if (需求量 >= 庫存量)
                                dt.Rows[dt.Rows.Count - 1]["qty"] = 庫存量;
                            else if (需求量 < 庫存量)
                                dt.Rows[dt.Rows.Count - 1]["qty"] = 需求量;
                        }
                        else
                        {
                            dt.Rows[dt.Rows.Count - 1]["qty"] = 需求量;
                        }
                    }
                    else //第2筆之後都是帶0
                        dt.Rows[dt.Rows.Count - 1]["qty"] = 0;
                    #endregion
                    dt.Rows[dt.Rows.Count - 1]["Date"] = 轉換日期(dt_GridView.Rows[e.RowIndex]["Date"].ToString());
                    dt.Rows[dt.Rows.Count - 1]["Date1"] = 轉換日期(dt_GridView.Rows[e.RowIndex]["Date1"].ToString());
                    dataGridViewT2.Invalidate();
                }
                else 
                {
                    MessageBox.Show("該批次資料已經存在於本次勾稽。", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        /// <summary>
        /// 判斷 該作業開窗是否回扣庫存  包含 : 1.FrmRShop  退貨本 2.FrmSale   銷貨本 3.SaleBom   銷貨子 4.FrmDraw   領料本 5.DrawBom   領料子 6.GarnerBom 入庫子 6.FrmGarner 入庫本
        /// </summary>
        /// <returns></returns>
        private bool is扣倉()
        {
            if (this.frm == "FrmRShop" || this.frm == "FrmSale" || this.frm == "SaleBom" || this.frm == "FrmDraw" || this.frm == "DrawBom" || this.frm == "GarnerBom" || this.frm == "FrmGarner")
                return true;
            else
                return false;
        }
        public string 轉換日期(string Date)
        {

            if (Common.User_DateTime == 1 && Date.Length == 8)
            {
                int year = Date.Substring(0, 4).ToInteger() - 1911;
                Date = year + Date.Substring(4);
            }
            else if (Common.User_DateTime == 2 && Date.Length == 7)
            {
                int year = Date.Substring(0, 3).ToInteger() + 1911;
                Date = year + Date.Substring(4);
            }
            return Date;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.F2)
            {
                gridAppend_Click(null, null);
            }
            else if (keyData == Keys.F3)
            {
                gridDelete_Click(null, null);
            }
            else if (keyData == Keys.F4)
            {
                gridExit_Click(null, null);
            }
            else if (keyData == Keys.F8)
            {
                if (gridGetBomD.Enabled == true) 
                {
                    gridGetBomD_Click(null, null);
                }
            }
       
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void NoSearch_TextChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (NoSearch.TrimTextLenth() == 0)
            {
                dataGridViewT1.FirstDisplayedScrollingRowIndex = 0;
                dataGridViewT1.CurrentCell = dataGridViewT1[0, 0];
                dataGridViewT1.Rows[0].Selected = true;
                return;
            }
            dataGridViewT1.Search("批次號碼", NoSearch.Text.Trim());
        }



    }
}
