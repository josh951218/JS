using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmItemInvBrow : Formbase
    {
        JBS.JS.xEvents xe;

        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        DataTable dt = new DataTable();
        EnumerableRowCollection<DataRow> rows;
        string No = "";
        bool IsSearching = false;

        public FrmItemInvBrow()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmItemInvBrow_Load(object sender, EventArgs e)
        {
            this.TResult = "";
             
            this.日期.DataPropertyName = Common.User_DateTime == 1 ? "ivdate" : "ivdate1";
            IvDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
        
            LoadDB();
            if (dt.Rows.Count > 0)
            {
                btnIvNo_Click(null, null);
                dataGridViewT1.Search("盤點憑證", this.TSeekNo.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            dataGridViewT1.DataSource = dt;
        }

        void LoadDB()
        {
            dt.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = " select ivd.*,iv.memo as ivmemo,iv.sum,iv.edtscno,定序='',產品組成='',盤點日期='',日期='' from ivd left join iv on ivd.ivno=iv.ivno ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i]["定序"] = (i + 1).ToString();
                                dt.Rows[i]["盤點日期"] = Common.User_DateTime == 1 ? Date.AddLine(dt.Rows[i]["ivdate"].ToString().Trim()) : Date.AddLine(dt.Rows[i]["ivdate1"].ToString().Trim());

                                if (dt.Rows[i]["ittrait"].ToDecimal() == 1)
                                {
                                    dt.Rows[i]["產品組成"] = "組合品";
                                }
                                else if (dt.Rows[i]["ittrait"].ToDecimal() == 2)
                                {
                                    dt.Rows[i]["產品組成"] = "組裝品";
                                }
                                else if (dt.Rows[i]["ittrait"].ToDecimal() == 3)
                                {
                                    dt.Rows[i]["產品組成"] = "單一商品";
                                }
                            }

                            rows = dt.AsEnumerable();
                        }
                        else
                        {
                            rows = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void WriteToText(DataRow row)
        {
            if (row != null)
            {
                textBoxT1.Text = row["stno"].ToString().Trim();
                textBoxT2.Text = row["stname"].ToString().Trim();
                textBoxT3.Text = row["stkqty"].ToDecimal().ToString("f" + Common.Q);
                textBoxT4.Text = row["sum"].ToDecimal().ToString("f" + Common.MS);
                textBoxT5.Text = row["ivmemo"].ToString().Trim();
                textBoxT6.Text = row["edtscno"].ToString().Trim();
            }
            else
            {
                foreach (var item in this.Controls.OfType<TextBox>())
                {
                    item.Clear();
                }
            }
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
        }

        void btnColor(Button btn)
        {
            foreach (var item in this.Controls.OfType<Button>())
            {
                item.ForeColor = Color.Black;
            }
            btn.ForeColor = Color.Red;
            var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            if (p == null) return;
            else
            {
                No = p.Cells["定序"].Value.ToString();
            }
        }

        private void btnIvNo_Click(object sender, EventArgs e)
        {
            btnColor(btnIvNo);
            dt = rows.OrderBy(r => r["IvNo"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = dt;
            if (!IsSearching)
            {
                dataGridViewT1.Search("定序", No.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
        }

        private void btnIvDate_Click(object sender, EventArgs e)
        {
            btnColor(btnIvDate);
            dt = rows.OrderBy(r => r["IvDate"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = dt;
            if (!IsSearching)
            {
                dataGridViewT1.Search("定序", No.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
        }

        private void btnStnoItem_Click(object sender, EventArgs e)
        {
            btnColor(btnStnoItem);
            dt = rows.OrderBy(r => r["StNo"].ToString()).ThenBy(r => r["ItNo"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = dt;
            if (!IsSearching)
            {
                dataGridViewT1.Search("定序", No.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            btnColor(btnItem);
            dt = rows.OrderBy(r => r["ItNo"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = dt;
            if (!IsSearching)
            {
                dataGridViewT1.Search("定序", No.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
        }

        private void btnEmplDate_Click(object sender, EventArgs e)
        {
            btnColor(btnEmplDate);
            dt = rows.OrderBy(r => r["EmNo"].ToString()).ThenBy(r => r["IvDate"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = dt;
            if (!IsSearching)
            {
                dataGridViewT1.Search("定序", No.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
        }

        private void btnMemo_Click(object sender, EventArgs e)
        {
            btnColor(btnMemo);
            dt = rows.OrderBy(r => r["Memo"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = dt;
            if (!IsSearching)
            {
                dataGridViewT1.Search("定序", No.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0) return;
            IsSearching = true;
            if (StNo.Text.Trim().Length > 0 && ItNo.Text.Trim().Length > 0)
            {
                btnStnoItem_Click(null, null);
                dataGridViewT1.Search("盤點倉庫", StNo.Text.Trim(), "產品編號", ItNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (EmNo.Text.Trim().Length > 0 && IvDate.Text.Trim().Length > 0)
            {
                btnEmplDate_Click(null, null);
                dataGridViewT1.Search("盤點人員", EmNo.Text.Trim(), "日期", IvDate.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (IvNo.Text.Trim().Length > 0)
            {
                btnIvNo_Click(null, null);
                dataGridViewT1.Search("盤點憑證", IvNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (ItNo.Text.Trim().Length > 0)
            {
                btnItem_Click(null, null);
                dataGridViewT1.Search("產品編號", ItNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (IvDate.Text.Trim().Length > 0)
            {
                btnIvDate_Click(null, null);
                dataGridViewT1.Search("日期", IvDate.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (Memo.Text.Trim().Length > 0)
            {
                btnMemo_Click(null, null);
                dataGridViewT1.Search("備註", Memo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (EmNo.Text.Trim().Length > 0)
            {
                btnMemo_Click(null, null);
                dataGridViewT1.Search("盤點人員", EmNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (StNo.Text.Trim().Length > 0)
            {
                btnMemo_Click(null, null);
                dataGridViewT1.Search("盤點倉庫", StNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            IsSearching = false;
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void IvDate_Validating(object sender, CancelEventArgs e)
        {
            if (bExit.Focused) 
                return;

            xe.DateValidate(sender, e, true);

        }

        private void bPicture_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1.SelectedRows.Count > 0)
                {
                    pVar.PictureOpenForm(dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString());
                    dataGridViewT1.Focus();
                }
            }
        }

        private void bStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                bStock.Focus();
                FrmSale_Stock frm = new FrmSale_Stock();
                frm.ItNo = dt.Rows[dataGridViewT1.CurrentRow.Index]["ItNo"].ToString();
                frm.ShowDialog();
                dataGridViewT1.Focus();
            }
        }

        private void bItDesp_Click(object sender, EventArgs e)
        {

            bItDesp.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            using (var frm = new JE.SOther.FrmDesp(false, FormStyle.Mini))
            {
                frm.dr = dt.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            var row = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            var index = row.Index;
            this.TResult = dt.Rows[index]["ivno"].ToString().Trim();
            this.Dispose();
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (e.StateChanged == DataGridViewElementStates.Selected && IsSearching == false)
                {
                    var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                    if (p == null) return;
                    var index = p.Index == -1 ? 0 : p.Index;
                    WriteToText(dt.Rows[index]);
                }
            }
        }































    }
}
