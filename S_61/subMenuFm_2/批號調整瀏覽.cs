using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.subMenuFm_1;
using S_61.SOther;
using System.Drawing;

namespace S_61.subMenuFm_2
{
    public partial class 批號調整瀏覽 : Formbase
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

        public 批號調整瀏覽()
        {
            InitializeComponent();
            this.帳上庫存量.Set庫存數量小數();
            this.盤點數量.Set庫存數量小數();
            this.盤盈虧數量.Set庫存數量小數();
            this.xe = new JBS.JS.xEvents();
        }

        private void 批號調整瀏覽_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            date.SetDateLength();
            try
            {
                LoadDB();
                if (dt.Rows.Count > 0)
                {
                    btnAdNo_Click(null, null);
                    dataGridViewT1.Search("調整憑證", this.TSeekNo.Trim());
                    var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                    var index = p.Index == -1 ? 0 : p.Index;
                    WriteToText(dt.Rows[index]);
                }
                dataGridViewT1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        void LoadDB()
        {
            dt.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = @" select d.*
                    ,定序='',調整日期='',faname1=(select faname1 from fact where fano=d.Fano)
                    ,m.stname,m.emname,m.admemo,m.edtscno,m.emno
                    ,b.Batchno,b.Date,b.Date1
                    from BatchProcess_adjustd as d 
                    left join BatchProcess_adjust as m on d.adno=m.adno 
                    left join BatchInformation as b on d.Bno  = b.Bno
                    order by d.adno";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            var datetype = Common.User_DateTime == 1 ? "" : "1";
                            int i = 0;
                            foreach (DataRow item in dt.Rows)
                            {
                                item["定序"] = ++i;
                                item["調整日期"] = Date.AddLine(item["addate" + datetype].ToString().Trim());
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
                textBoxT5.Text = row["admemo"].ToString().Trim();
                textBoxT6.Text = row["edtscno"].ToString().Trim();
            }
            else
            {
                foreach (var item in this.Controls.OfType<TextBox>())
                {
                    item.Clear();
                }
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


        private void btnAdNo_Click(object sender, EventArgs e)
        {
            btnColor(btnAdNo);
            dt = rows.OrderBy(r => r["AdNo"].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = dt;
            if (!IsSearching)
            {
                dataGridViewT1.Search("定序", No.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
        }
        private void btnAdDate_Click(object sender, EventArgs e)
        {
            btnColor(btnAdDate);
            dt = rows.OrderBy(r => r["adDate"].ToString()).CopyToDataTable();
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
            dt = rows.OrderBy(r => r["EmNo"].ToString()).ThenBy(r => r["adDate"].ToString()).CopyToDataTable();
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




        private void bSearch_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0) return;
            IsSearching = true;
            if (qStNo.Text.Trim().Length > 0 && qItNo.Text.Trim().Length > 0)
            {
                btnStnoItem_Click(null, null);
                dataGridViewT1.Search("調整倉庫", qStNo.Text.Trim(), "產品編號", qItNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (qEmNo.Text.Trim().Length > 0 && date.Text.Trim().Length > 0)
            {
                btnEmplDate_Click(null, null);
                dataGridViewT1.Search("emno", qEmNo.Text.Trim(), "addate", Date.ToTWDate(date.Text.Trim()));
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (qAdNo.Text.Trim().Length > 0)
            {
                btnAdNo_Click(null, null);
                dataGridViewT1.Search("調整憑證", qAdNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (qItNo.Text.Trim().Length > 0)
            {
                btnItem_Click(null, null);
                dataGridViewT1.Search("產品編號", qItNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (date.Text.Trim().Length > 0)
            {
                btnAdDate_Click(null, null);
                dataGridViewT1.Search("addate", Date.ToTWDate(date.Text.Trim()));
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (qMemo.Text.Trim().Length > 0)
            {
                btnMemo_Click(null, null);
                dataGridViewT1.Search("備註說明", qMemo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (qEmNo.Text.Trim().Length > 0)
            {
                btnMemo_Click(null, null);
                dataGridViewT1.Search("emno", qEmNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            else if (qStNo.Text.Trim().Length > 0)
            {
                btnMemo_Click(null, null);
                dataGridViewT1.Search("調整倉庫", qStNo.Text.Trim());
                var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p.Index == -1 ? 0 : p.Index;
                WriteToText(dt.Rows[index]);
            }
            IsSearching = false;
        }
        private void bExit_Click(object sender, EventArgs e)
        {
            var row = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            var index = row.Index;
            this.TResult = dt.Rows[index]["adno"].ToString().Trim();
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
        private void Date_Validating(object sender, CancelEventArgs e)
        {
            if (bExit.Focused)
                return;

            xe.DateValidate(sender, e, true);

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                bSearch_Click(null, null);
            }
            else if (keyData == Keys.F3)
            {
                bExit_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
