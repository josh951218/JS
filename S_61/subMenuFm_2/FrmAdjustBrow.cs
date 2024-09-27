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
    public partial class FrmAdjustBrow : Formbase, JBS.JS.IxOpen
    {
        JBS.JS.xEvents xe;

        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        DataTable table = new DataTable();
        List<TextBox> Txt;
        List<DataRow> list = new List<DataRow>();
        List<Button> query;
        List<DataRow> tb = new List<DataRow>();
        string sql = "";
        string NO;

        public FrmAdjustBrow()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            pVar.SetMemoUdf(this.說明);
            lblT12.Text = Common.Sys_MemoUdf;

            query = new List<Button> { query2, query3, query4, query5, query6, query7 };
            Txt = new List<TextBox> { ItNo, StNo, StName, TotQty, StNo, StName, AdMemo };

            this.入庫成本.DefaultCellStyle.Format = "f" + Common.M;
            this.入庫數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;

            this.入庫成本.Visible = Common.User_ShopPrice;
        }

        private void FrmAdjustBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            if (Common.User_DateTime == 1)
                qAdDate.MaxLength = 7;
            else
                qAdDate.MaxLength = 8;

            dataGridViewT1.ReadOnly = false;
            dataGridViewT1.Columns["調整單號"].ReadOnly = true;
            dataGridViewT1.Columns["調整日期"].ReadOnly = true;
            dataGridViewT1.Columns["調整倉庫"].ReadOnly = true;
            dataGridViewT1.Columns["品名規格"].ReadOnly = true;
            dataGridViewT1.Columns["單位"].ReadOnly = true;
            dataGridViewT1.Columns["入庫數量"].ReadOnly = true;
            dataGridViewT1.Columns["包裝數量"].ReadOnly = true;
            dataGridViewT1.Columns["產品組成"].ReadOnly = true;
            dataGridViewT1.Columns["調整人員"].ReadOnly = true;
            dataGridViewT1.Columns["產品編號"].ReadOnly = true;
            dataGridViewT1.Columns["入庫成本"].ReadOnly = true;

            sql = " 調整日期='',產品組成='',序號='',"
                + " a.*,b.emname,b.admemo from adjustd AS a left join adjust AS b "
                + " on a.adno = b.adno where '0'='0'";
            table = Common.Bload(sql, "a.adno", this.TSeekNo ?? "").AsEnumerable().OrderBy(r => r["adno"].ToString()).CopyToDataTable();
            intTable();
            if (list.Count > 0)
            {
                var dr = list.Find(r => r["adno"].ToString() == this.TSeekNo.Trim());
                WriteToTxt(dr);
                NO = dr["序號"].ToString();
            }
            else
            {
                WriteToTxt(null);
            }
            SetBtnColor();
            SetSelect(NO);
            query2.ForeColor = Color.Red;
            qAdNo.Focus();
        }

        void intTable()
        {
            dataGridViewT1.DataSource = null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i]["序號"] = i;
                if (Common.User_DateTime == 1)
                    table.Rows[i]["調整日期"] = Date.AddLine(table.Rows[i]["addate"].ToString());
                else
                    table.Rows[i]["調整日期"] = Date.AddLine(table.Rows[i]["addate1"].ToString());

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
            if (dr == null)
            {
                Txt.ForEach(t => t.Text = "");
            }
            else
            {
                ItNo.Text = dr["itno"].ToString();
                StNo.Text = dr["stno"].ToString();
                StName.Text = dr["stname"].ToString();
                EmNo.Text = dr["emno"].ToString();
                EmName.Text = dr["emname"].ToString();
                AdMemo.Text = dr["admemo"].ToString();
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        string sql = "select sum(itqty) from stock where itno=@itno COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("itno", dr["itno"].ToString().Trim());
                            TotQty.Text = cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.Q);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }


        private void btnQuery_Click(object sender, EventArgs e)
        {
            btnQuery.Enabled = false;
            SetBtnColor();

            if (qStNo.Text != "" && qItNo.Text != "")
            {
                table = Common.Bload(sql, "a.stno", qStNo.Text.Trim(), "a.itno", qItNo.Text.Trim()).AsEnumerable().OrderBy(r => r["stno"].ToString()).ThenBy(r => r["itno"].ToString()).ThenBy(r => r["adno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("調整倉庫", qStNo.Text, "產品編號", qItNo.Text);
                query5.ForeColor = Color.Red;
            }
            else if (qEmNo.Text != "" && qAdDate.Text != "")
            {
                string str = Date.ToTWDate(qAdDate.Text.Trim());
                table = Common.Bload(sql, "a.emno", qEmNo.Text.Trim(), "a.addate", str).AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenByDescending(r => r["addate"].ToString()).ThenBy(r => r["adno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.SearchForDate("調整人員", qEmNo.Text, "addate", str);
                query6.ForeColor = Color.Red;
            }
            else if (qAdNo.Text.Trim() != "")
            {
                table = Common.Bload(sql, "a.adno", qAdNo.Text.Trim()).AsEnumerable().OrderBy(r => r["adno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("調整單號", qAdNo.Text);
                query2.ForeColor = Color.Red;
            }
            else if (qItNo.Text != "")
            {
                table = Common.Bload(sql, "a.itno", qItNo.Text.Trim()).AsEnumerable().OrderBy(r => r["itno"].ToString()).ThenBy(r => r["adno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("產品編號", qItNo.Text);
                query4.ForeColor = Color.Red;
            }
            else if (qAdDate.Text != "")
            {
                string str = Date.ToTWDate(qAdDate.Text.Trim());
                table = Common.Bload(sql, "a.addate", str).AsEnumerable().OrderByDescending(r => r["addate"].ToString()).ThenBy(r => r["adno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.SearchForDate("addate", str);
                query3.ForeColor = Color.Red;
            }
            else if (qItUnit.Text != "")
            {
                table = Common.Bload(sql, "a.itunit", qItUnit.Text.Trim()).AsEnumerable().OrderBy(r => r["itunit"].ToString()).ThenBy(r => r["adno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("單位", qItUnit.Text);
            }
            else if (qAdMemo.Text != "")
            {
                table = Common.Bload(sql, "a.memo", qAdMemo.Text.Trim()).AsEnumerable().OrderBy(r => r["memo"].ToString()).ThenBy(r => r["adno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("說明", qAdMemo.Text);
                query7.ForeColor = Color.Red;
            }
            else if (qStNo.Text != "")
            {
                table = Common.Bload(sql, "a.stno", qStNo.Text.Trim()).AsEnumerable().OrderBy(r => r["stno"].ToString()).ThenBy(r => r["adno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.Search("調整倉庫", qStNo.Text);
                query5.ForeColor = Color.Red;
            }
            else if (qEmNo.Text != "")
            {
                string str = Date.GetDateTime(1);
                table = Common.Bload(sql, "a.emno", qEmNo.Text.Trim(), "a.addate", str).AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenByDescending(r => r["addate"].ToString()).ThenBy(r => r["adno"].ToString()).CopyToDataTable();
                intTable();
                dataGridViewT1.SearchForDate("調整人員", qEmNo.Text, "addate", str);
                query6.ForeColor = Color.Red;
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
            if (dataGridViewT1.Rows.Count < 1) return;
            if (dataGridViewT1.SelectedRows[0].Cells["產品組成"].Value.ToString().Trim() != "組裝品")
            {
                MessageBox.Show("只有組合品或組裝品有組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                using (var frm = new FrmAdjust_Bom())
                { 
                    frm.BoItNo1 = ItNo.Text;
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.adno = dataGridViewT1.SelectedRows[0].Cells["調整單號"].Value.ToString().Trim();
                    frm.rec = dataGridViewT1.SelectedRows[0].Cells["bomrec"].Value.ToString().Trim();
                    frm.FromADBrow = true;
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
            else this.TResult = dataGridViewT1.SelectedRows[0].Cells["調整單號"].Value.ToString().Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void query2_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count < 1) return;
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["adno"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetBtnColor();
            query2.ForeColor = Color.Red;
            SetSelect(NO);

        }

        private void query3_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count < 1) return;
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderByDescending(r => r["addate"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetBtnColor();
            query3.ForeColor = Color.Red;
            SetSelect(NO);

        }

        private void query4_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count < 1) return;
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["itno"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetBtnColor();
            query4.ForeColor = Color.Red;
            SetSelect(NO);

        }

        private void query5_Click(object sender, EventArgs e)
        {

            if (dataGridViewT1.Rows.Count < 1) return;
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["stno"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetBtnColor();
            query5.ForeColor = Color.Red;
            SetSelect(NO);

        }

        private void query6_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count < 1) return;
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["emno"].ToString()).ThenByDescending(r => r["addate"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetBtnColor();
            query6.ForeColor = Color.Red;
            SetSelect(NO);

        }

        private void query7_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count < 1) return;
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString();
            table = table.AsEnumerable().OrderBy(r => r["memo"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = table;
            SetBtnColor();
            query7.ForeColor = Color.Red;
            SetSelect(NO);

        }

        void SetBtnColor()
        {
            query.ForEach(t => t.ForeColor = SystemColors.ControlText);

        }

        void SetSelect(string NO)
        {
            if (dataGridViewT1.Rows.Count > 0)
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
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.Rows.Count < 1) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name != "說明") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        string adno = dataGridViewT1["調整單號", e.RowIndex].Value.ToString().Trim();
                        string memo = dataGridViewT1["說明", e.RowIndex].Value.ToString();
                        string bomrec = dataGridViewT1["bomrec", e.RowIndex].Value.ToString().Trim();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("memo", memo);
                        cmd.Parameters.AddWithValue("adno", adno);
                        cmd.Parameters.AddWithValue("bomrec", bomrec);
                        cmd.CommandText = "update adjustd set memo=@memo where adno=@adno"
                        + " and bomrec=@bomrec ";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count < 1) return;
            if (dataGridViewT1.SelectedRows.Count < 1) return;
            string adno = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            var dr = list.Find(r => r["序號"].ToString() == adno);
            WriteToTxt(dr);
        }

        private void qAdDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e, true);
        }

        private void qStNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void qItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void qEmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void qAdMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(qAdMemo, 20);
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



    }
}
