using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using JE.SOther;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmGarnerBrow : Formbase
    {
        JBS.JS.xEvents xe; 
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        DataTable table = new DataTable();
        List<DataRow> list = new List<DataRow>();
        DataRow dr;
        List<TextBox> Txt;
        List<Button> query;
        string sql = "";
        string NO = "";

        public FrmGarnerBrow()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            pVar.SetMemoUdf(this.說明);
            lblT14.Text = Common.Sys_MemoUdf;

            query8.Text = "f8:" + Common.Sys_MemoUdf;

            query = new List<Button> { query2, query3, query4, query5, query6, query7, query8 };
            Txt = new List<TextBox> { ItNo, StNoI, StNameI, StNoO, StNameO, EmNo, EmName, GaMemo, StockQty };

            this.入庫數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.入庫成本.DefaultCellStyle.Format = "f" + Common.M;
  
            this.入庫成本.Visible = Common.User_ShopPrice;
        }

        private void FrmGarnerBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            if (Common.User_DateTime == 1) qDate.MaxLength = 7;
            else qDate.MaxLength = 8;
            
            dataGridViewT1.ReadOnly = false;
            this.入庫單號.ReadOnly = true;
            this.入庫日期.ReadOnly = true;
            this.入庫倉庫.ReadOnly = true;
            this.扣料倉庫.ReadOnly = true;
            this.品名規格.ReadOnly = true;
            this.單位.ReadOnly = true;
            this.入庫數量.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.入庫人員.ReadOnly = true;
            this.產品編號.ReadOnly = true;
            this.入庫成本.ReadOnly = true;

            sql = " 入庫日期='',產品組成='',序號='',";
            sql += " b.*,a.stnamei,a.stnameo,a.gamemo,a.emname";
            sql += " from garner as a right join garnerd as b on a.gano=b.gano where '0'='0'";
            table = Common.Bload(sql, "b.gano", this.TSeekNo).AsEnumerable().OrderBy(r => r["gano"].ToString()).CopyToDataTable();
            intTable();
            if (list.Count > 0)
            {
                dr = list.Find(r => r["gano"].ToString() == this.TSeekNo);
                WriteToTxt(dr);
                NO = dr["序號"].ToString();
            }
            else
            {
                WriteToTxt(null);
            }
            SetButtonColor();
            SetSelectRow(NO);

            query2.ForeColor = Color.Red;
            qGaNo.Focus();
        }

        void WriteToTxt(DataRow dr)
        {
            if (dr == null) return;
            ItNo.Text = dr["itno"].ToString();
            StNoI.Text = dr["stnoi"].ToString();
            StNameI.Text = dr["stnamei"].ToString();
            StNoO.Text = dr["stnoo"].ToString();
            StNameO.Text = dr["stnameo"].ToString();
            EmNo.Text = dr["emno"].ToString();
            EmName.Text = dr["emname"].ToString();
            GaMemo.Text = dr["gamemo"].ToString();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                    cmd.CommandText = "select itstockqty from item where itno=@itno";
                    StockQty.Text = cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.Q);
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void intTable()
        {
            dataGridViewT1.DataSource = null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i]["序號"] = i;
                if (Common.User_DateTime == 1)
                    table.Rows[i]["入庫日期"] = Date.AddLine(table.Rows[i]["gadate"].ToString());
                else
                    table.Rows[i]["入庫日期"] = Date.AddLine(table.Rows[i]["gadate1"].ToString());

                if (table.Rows[i]["ittrait"].ToDecimal() == 1) table.Rows[i]["產品組成"] = "組合品";
                else if (table.Rows[i]["ittrait"].ToDecimal() == 2) table.Rows[i]["產品組成"] = "組裝品";
                else if (table.Rows[i]["ittrait"].ToDecimal() == 3) table.Rows[i]["產品組成"] = "單一商品";
            }
            dataGridViewT1.DataSource = table;
            list.Clear();
            if (table.Rows.Count > 0) list = table.AsEnumerable().ToList();
        }

        void SetButtonColor()
        {
            query.ForEach(r => r.ForeColor = SystemColors.ControlText);
        }

        void SetSelectRow(string NO)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["序號", i].Value.ToString() == NO)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void query2_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["gano"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query3_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderByDescending(r => r["gadate"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query3.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query4_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["itno"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query4.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query5_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["stnoo"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query5.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query6_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["stnoi"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query6.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query7_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenByDescending(r => r["gadate"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query7.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query8_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["memo"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query8.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            string num = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dr = list.Find(r => r["序號"].ToString() == num);
            WriteToTxt(dr);
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name != "說明") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    string gano = dataGridViewT1["入庫單號", e.RowIndex].Value.ToString().Trim();
                    string memo = dataGridViewT1["說明", e.RowIndex].Value.ToString();
                    string rec = table.Rows[e.RowIndex]["bomrec"].ToString();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("memo", memo);
                    cmd.Parameters.AddWithValue("gano", gano);
                    cmd.Parameters.AddWithValue("bomrec", rec);
                    cmd.CommandText = "update garnerd set memo=@memo where gano=@gano and bomrec=@bomrec ";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GaMemo_Validating(object sender, CancelEventArgs e)
        {
            if (GaMemo.Text == "") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        string gano = dataGridViewT1.SelectedRows[0].Cells["入庫單號"].Value.ToString();
                        string memo = GaMemo.Text.Trim();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("memo", memo);
                        cmd.Parameters.AddWithValue("gano", gano);
                        cmd.CommandText = "update garner set gamemo=@memo where gano=@gano ";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void qDate_Validating(object sender, CancelEventArgs e)
        { 
            if (btnExit.Focused) 
                return;

            xe.DateValidate(sender, e, true); 
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            btnQuery.Enabled = false;
            SetButtonColor();
            if (qStNoO.Text != "" && qItNo.Text != "")
            {
                table = Common.Bload(sql, "b.stnoo", qStNoO.Text.Trim(), "b.itno", qItNo.Text.Trim()).AsEnumerable().OrderBy(r => r["stnoo"].ToString()).ThenBy(r => r["itno"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("扣料倉庫", qStNoO.Text, "產品編號", qItNo.Text);
                query5.ForeColor = Color.Red;
            }
            else if (qStNoI.Text != "" && qItNo.Text != "")
            {
                table = Common.Bload(sql, "b.stnoi", qStNoI.Text.Trim(), "b.itno", qItNo.Text.Trim()).AsEnumerable().OrderBy(r => r["stnoi"].ToString()).ThenBy(r => r["itno"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("入庫倉庫", qStNoI.Text, "產品編號", qItNo.Text);
                query6.ForeColor = Color.Red;
            }
            else if (qEmNo.Text != "" && qDate.Text != "")
            {
                string str = Date.ToTWDate(qDate.Text.Trim());
                table = Common.Bload(sql, "b.emno", qEmNo.Text.Trim(), "b.gadate", str).AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenByDescending(r => r["gadate"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.SearchForDate("入庫人員", qEmNo.Text, "gadate", str);
                query7.ForeColor = Color.Red;
            }
            else if (qEmNo.Text != "")
            {
                //table = Common.Bload(sql, "b.emno", qEmNo.Text.Trim()).AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                //intTable();
                //dataGridViewT1.Search("入庫人員", qEmNo.Text);
                string str = Date.GetDateTime(1);
                table = Common.Bload(sql, "b.emno", qEmNo.Text.Trim(), "b.gadate", str).AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenByDescending(r => r["gadate"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.SearchForDate("入庫人員", qEmNo.Text, "gadate", str);
                query7.ForeColor = Color.Red;
            }
            else if (qGaNo.Text != "")
            {
                table = Common.Bload(sql, "b.gano", qGaNo.Text.Trim()).AsEnumerable().OrderBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("入庫單號", qGaNo.Text);
                query2.ForeColor = Color.Red;
            }
            else if (qDate.Text != "")
            {
                string str = Date.ToTWDate(qDate.Text.Trim());
                table = Common.Bload(sql, "b.gadate", str).AsEnumerable().OrderByDescending(r => r["gadate"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.SearchForDate("gadate", str);
                query3.ForeColor = Color.Red;
            }
            else if (qItNo.Text != "")
            {
                table = Common.Bload(sql, "b.itno", qItNo.Text.Trim()).AsEnumerable().OrderBy(r => r["itno"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("產品編號", qItNo.Text);
                query4.ForeColor = Color.Red;
            }
            else if (qMemo.Text != "")
            {
                table = Common.Bload(sql, "b.memo", qMemo.Text.Trim()).AsEnumerable().OrderBy(r => r["memo"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("說明", qMemo.Text);
                query8.ForeColor = Color.Red;
            }
            else if (qUnit.Text != "")
            {
                table = Common.Bload(sql, "b.itunit", qUnit.Text.Trim()).AsEnumerable().OrderBy(r => r["itunit"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("單位", qUnit.Text);
            }
            else if (qStNoI.Text != "")
            {
                table = Common.Bload(sql, "b.stnoi", qStNoI.Text.Trim()).AsEnumerable().OrderBy(r => r["stnoi"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("入庫倉庫", qStNoI.Text);
                query6.ForeColor = Color.Red;
            }
            else if (qStNoO.Text != "")
            {
                table = Common.Bload(sql, "b.stnoo", qStNoO.Text.Trim()).AsEnumerable().OrderBy(r => r["stnoo"].ToString()).ThenBy(r => r["gano"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("扣料倉庫", qStNoO.Text);
                query5.ForeColor = Color.Red;
            }
            btnQuery.Enabled = true;
            btnQuery.Focus();
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1.SelectedRows.Count > 0)
                    pVar.PictureOpenForm(dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString());
            }
        }

        private void btnDesp_Click(object sender, EventArgs e)
        {
            //string bomid = table.Rows[dataGridViewT1.SelectedRows[0].Index]["bomid"].ToString();

            //FrmSale_ItDesp frm = new FrmSale_ItDesp();
            //frm.SetParaeter(ViewMode.Normal);
            //frm.JustForBrow = true;
            //frm.BomID = bomid;
            //frm.CallBack = "garnerd";
            //frm.ShowDialog();
            btnDesp.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            using (var frm = new FrmDesp(false, FormStyle.Mini))
            {
                frm.dr = table.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void btnBom_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows[0].Cells["產品組成"].Value.ToString() == "單一商品")
            {
                MessageBox.Show("只有組合品或組裝品有組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                using (var frm = new FrmDraw_Bom())
                { 
                    frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.DrNo = dataGridViewT1.SelectedRows[0].Cells["入庫單號"].Value.ToString();
                    frm.BomID = table.Rows[dataGridViewT1.SelectedRows[0].Index]["bomid"].ToString();
                    frm.FormGarnerBrow = true;
                    frm.ShowDialog();
                }
            }
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                FrmSale_Stock frm = new FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) this.TResult = "";
            else this.TResult = dataGridViewT1.SelectedRows[0].Cells["入庫單號"].Value.ToString().Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void qItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void qEmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void qMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(qMemo, 20);
        }

        private void qStNoI_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }
         
         protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.ToString().StartsWith("F2") && keyData.ToString().EndsWith("Shift"))
            {
                query2.Focus();
                query2.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F3") && keyData.ToString().EndsWith("Shift"))
            {
                query3.Focus();
                query3.PerformClick();
            } 
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                query4.Focus();
                query4.PerformClick();
            } 
            else if (keyData.ToString().StartsWith("F5") && keyData.ToString().EndsWith("Shift"))
            {
                query5.Focus();
                query5.PerformClick();
            } 
            else if (keyData.ToString().StartsWith("F6") && keyData.ToString().EndsWith("Shift"))
            {
                query6.Focus();
                query6.PerformClick();
            } 
            else if (keyData.ToString().StartsWith("F7") && keyData.ToString().EndsWith("Shift"))
            {
                query7.Focus();
                query7.PerformClick();
            } 
            else if (keyData.ToString().StartsWith("F8") && keyData.ToString().EndsWith("Shift"))
            {
                query8.Focus();
                query8.PerformClick();
            }
            else if (keyData == Keys.F9)
            {
                btnExit.Focus();
                btnExit.PerformClick();
            }
             
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void dataGridViewT1_DoubleClick(object sender, EventArgs e)
        {
            btnExit_Click(null, null);
        }

        private void dataGridViewT1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



    }
}
