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
    public partial class FrmAllotBrow : Formbase
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

        public FrmAllotBrow()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            pVar.SetMemoUdf(this.備註說明);
            lblT14.Text = Common.Sys_MemoUdf;

            query8.Text = "f8:" + Common.Sys_MemoUdf;

            query = new List<Button> { query2, query3, query4, query5, query6, query7, query8 };
            Txt = new List<TextBox> { ItNo, StNoI, StNameI, StNoO, StNameO, EmNo, EmName, AlMemo, StockQty };

            this.調撥數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
             
        }

        private void FrmAllotBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            if (Common.User_DateTime == 1)
                qDate.MaxLength = 7;
            else
                qDate.MaxLength = 8;
             
            dataGridViewT1.ReadOnly = false;
            this.調撥單號.ReadOnly = true;
            this.調撥日期.ReadOnly = true;
            this.撥入倉庫.ReadOnly = true;
            this.撥出倉庫.ReadOnly = true;
            this.品名規格.ReadOnly = true;
            this.單位.ReadOnly = true;
            this.調撥數量.ReadOnly = true;
            this.包裝數量.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.調撥人員.ReadOnly = true;
            this.產品編號.ReadOnly = true;

            sql = " 調撥日期='',產品組成='',序號='',"
            + " a.*,b.almemo,b.emname from allotd as a left join allot as b"
            + " on a.alno = b.alno where '0'='0'";
            table = Common.Bload(sql, "a.alno", this.TSeekNo).AsEnumerable().OrderBy(r => r["alno"].ToString()).CopyToDataTable();
            intTable();
            if (list.Count > 0)
            {
                dr = list.Find(r => r["alno"].ToString() == this.TSeekNo);
                WriteToTxt(dr);
                NO = dr["序號"].ToString();
            }
            else
            {
                WriteToTxt(null);
            }
            SetButtonColor();
            SetSelectRow(NO);
            query2.Focus();
            query2.ForeColor = Color.Red;

            qAlNo.Focus();
        }

        void intTable()
        {
            dataGridViewT1.DataSource = null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i]["序號"] = i;
                if (Common.User_DateTime == 1)
                    table.Rows[i]["調撥日期"] = Date.AddLine(table.Rows[i]["aldate"].ToString());
                else
                    table.Rows[i]["調撥日期"] = Date.AddLine(table.Rows[i]["aldate1"].ToString());

                if (table.Rows[i]["ittrait"].ToDecimal() == 1) table.Rows[i]["產品組成"] = "組合品";
                else if (table.Rows[i]["ittrait"].ToDecimal() == 2) table.Rows[i]["產品組成"] = "組裝品";
                else if (table.Rows[i]["ittrait"].ToDecimal() == 3) table.Rows[i]["產品組成"] = "單一商品";
            }
            dataGridViewT1.DataSource = table;
            list.Clear();
            if (table.Rows.Count > 0) list = table.AsEnumerable().ToList();
        }

        void WriteToTxt(DataRow dr)
        {
            if (dr == null) return;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dr["itno"].ToString());
                    cmd.CommandText = "select itstockqty from item where itno=@itno";
                    StockQty.Text = cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.Q);

                    ItNo.Text = dr["itno"].ToString();

                    StNoI.Text = dr["stnoi"].ToString();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("stno", StNoI.Text.ToString());
                    cmd.CommandText = "select stname from stkroom where stno=@stno";
                    StNameI.Text = cmd.ExecuteScalar().ToString();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("stno", StNoO.Text.ToString());
                    StNoO.Text = dr["stnoo"].ToString();
                    cmd.CommandText = "select stname from stkroom where stno='" + StNoO.Text.ToString() + "'";
                    StNameO.Text = cmd.ExecuteScalar().ToString();

                    EmNo.Text = dr["emno"].ToString();
                    EmName.Text = dr["emname"].ToString();
                    AlMemo.Text = dr["almemo"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            table = table.AsEnumerable().OrderBy(r => r["alno"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetButtonColor();
            query2.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void query3_Click(object sender, EventArgs e)
        {
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderByDescending(r => r["調撥日期"].ToString()).CopyToDataTable();
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
            table = table.AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenByDescending(r => r["調撥日期"].ToString()).CopyToDataTable();
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            btnQuery.Enabled = false;
            SetButtonColor();
            if (qStNoO.Text != "" && qItNo.Text != "")
            {
                table = Common.Bload(sql, "a.stnoo", qStNoO.Text.Trim(), "a.itno", qItNo.Text.Trim()).AsEnumerable().OrderBy(r => r["stnoo"].ToString()).ThenBy(r => r["itno"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("撥出倉庫", qStNoO.Text, "產品編號", qItNo.Text);
                query5.ForeColor = Color.Red;
            }
            else if (qStNoI.Text != "" && qItNo.Text != "")
            {
                table = Common.Bload(sql, "a.stnoi", qStNoI.Text.Trim(), "a.itno", qItNo.Text.Trim()).AsEnumerable().OrderBy(r => r["stnoi"].ToString()).ThenBy(r => r["itno"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("撥入倉庫", qStNoI.Text, "產品編號", qItNo.Text);
                query6.ForeColor = Color.Red;
            }
            else if (qEmNo.Text != "" && qDate.Text != "")
            {
                string str = Date.ToTWDate(qDate.Text.Trim());
                table = Common.Bload(sql, "a.emno", qEmNo.Text.Trim(), "a.aldate", str).AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenByDescending(r => r["aldate"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.SearchForDate("調撥人員", qEmNo.Text, "aldate", str);
                query7.ForeColor = Color.Red;
            }
            else if (qAlNo.Text != "")
            {
                table = Common.Bload(sql, "a.alno", qAlNo.Text.Trim()).AsEnumerable().OrderBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("調撥單號", qAlNo.Text);
                query2.ForeColor = Color.Red;
            }
            else if (qDate.Text != "")
            {
                string str = Date.ToTWDate(qDate.Text.Trim());
                table = Common.Bload(sql, "a.aldate", str).AsEnumerable().OrderByDescending(r => r["aldate"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.SearchForDate("aldate", str);
                query3.ForeColor = Color.Red;
            }
            else if (qItNo.Text != "")
            {
                table = Common.Bload(sql, "a.itno", qItNo.Text.Trim()).AsEnumerable().OrderBy(r => r["itno"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("產品編號", qItNo.Text);
                query4.ForeColor = Color.Red;
            }
            else if (qMemo.Text != "")
            {
                table = Common.Bload(sql, "a.memo", qMemo.Text.Trim()).AsEnumerable().OrderBy(r => r["memo"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("備註說明", qMemo.Text);
                query8.ForeColor = Color.Red;
            }
            else if (qUnit.Text != "")
            {
                table = Common.Bload(sql, "a.itunit", qUnit.Text.Trim()).AsEnumerable().OrderBy(r => r["itunit"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("單位", qUnit.Text);
            }
            else if (qStNoI.Text != "")
            {
                table = Common.Bload(sql, "a.stnoi", qStNoI.Text.Trim()).AsEnumerable().OrderBy(r => r["stnoi"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("撥入倉庫", qStNoI.Text);
                query6.ForeColor = Color.Red;
            }
            else if (qStNoO.Text != "")
            {
                table = Common.Bload(sql, "a.stnoo", qStNoO.Text.Trim()).AsEnumerable().OrderBy(r => r["stnoo"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("撥出倉庫", qStNoO.Text);
                query5.ForeColor = Color.Red;
            }
            else if (qEmNo.Text != "")
            {
                string str = Date.GetDateTime(1);
                table = Common.Bload(sql, "a.emno", qEmNo.Text.Trim(), "a.aldate", str).AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenByDescending(r => r["aldate"].ToString()).ThenBy(r => r["alno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.SearchForDate("調撥人員", qEmNo.Text, "aldate", str);
                query7.ForeColor = Color.Red;
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
                    frm.DrNo = dataGridViewT1.SelectedRows[0].Cells["調撥單號"].Value.ToString();
                    frm.BomID = table.Rows[dataGridViewT1.SelectedRows[0].Index]["bomid"].ToString();
                    frm.together = true;
                    frm.str = new string[] { "AlloBom", "AlNo" };
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
            if (dataGridViewT1.Rows.Count == 0) TResult = "";
            else TResult = dataGridViewT1.SelectedRows[0].Cells["調撥單號"].Value.ToString().Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            string drno = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            dr = list.Find(r => r["序號"].ToString() == drno);
            WriteToTxt(dr);
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name != "備註說明") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        string alno = dataGridViewT1["調撥單號", e.RowIndex].Value.ToString().Trim();
                        string memo = dataGridViewT1["備註說明", e.RowIndex].Value.ToString();
                        string bomrec = table.Rows[dataGridViewT1.SelectedRows[0].Index]["bomrec"].ToString();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("memo", memo);
                        cmd.Parameters.AddWithValue("alno", alno);
                        cmd.Parameters.AddWithValue("bomrec", bomrec);
                        cmd.CommandText = "update allotd set memo=@memo where alno=@alno "
                        + " and bomrec=@bomrec";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void AlMemo_Validating(object sender, CancelEventArgs e)
        {
            if (AlMemo.Text == "") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        string alno = dataGridViewT1.SelectedRows[0].Cells["調撥單號"].Value.ToString();
                        string memo = AlMemo.Text.Trim();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("almemo", memo);
                        cmd.Parameters.AddWithValue("alno", alno);
                        cmd.CommandText = "update allot set almemo=@almemo where alno=@alno";
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
         
        private void dataGridViewT1_DoubleClick(object sender, EventArgs e)
        {
            btnExit_Click(null, null);
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
            else if (keyData == Keys.F3)
            {
                btnQuery.Focus();
                btnQuery.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                query4.Focus();
                query4.PerformClick();
            }
            else if (keyData == Keys.F4)
            {
                btnPicture.Focus();
                btnPicture.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F5") && keyData.ToString().EndsWith("Shift"))
            {
                query5.Focus();
                query5.PerformClick();
            }
            else if (keyData == Keys.F5)
            {
                btnDesp.Focus();
                btnDesp.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F6") && keyData.ToString().EndsWith("Shift"))
            {
                query6.Focus();
                query6.PerformClick();
            }
            else if (keyData == Keys.F6)
            {
                btnBom.Focus();
                btnBom.PerformClick();
            } 
            else if (keyData.ToString().StartsWith("F7") && keyData.ToString().EndsWith("Shift"))
            {
                query7.Focus();
                query7.PerformClick();
            }
            else if (keyData == Keys.F8)
            {
                btnStock.Focus();
                btnStock.PerformClick(); 
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

    }
}
