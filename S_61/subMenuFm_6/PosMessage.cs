using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class PosMessage : Formbase
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        string btnState = "";

        public PosMessage()
        {
            InitializeComponent();
            //pVar.PosMessage = this;
            cn1.ConnectionString = Common.sqlConnString;
        }

        private void PosMessage_Load(object sender, EventArgs e)
        {
            //this.SetLocation();
            //dataGridViewT1.SetWidthByPixel();
            btnSave.Enabled = btnCancel.Enabled = false;
            this.起始日期.DataPropertyName = Common.User_DateTime == 1 ? "mgsdate" : "mgsdate1";
            this.終止日期.DataPropertyName = Common.User_DateTime == 1 ? "mgedate" : "mgedate1";
            MgsDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            MgeDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            //MgsDate.Init();
            //MgeDate.Init();
            dataGridViewT1.DataSource = dt;
            LoadDB();
        }

        void LoadDB()
        {
            dt.Clear();
            da1.Fill(dt);
        }

        void WriteText(DataRow row)
        {
            if (row.IsNotNull())
            {
                var s = Common.User_DateTime == 1 ? "mgsdate" : "mgsdate1";
                MgsDate.Text = row[s].ToString().Trim();
                var e = Common.User_DateTime == 1 ? "mgedate" : "mgedate1";
                MgeDate.Text = row[e].ToString().Trim();
                MgTitle.Text = row["mgtitle"].ToString().Trim();
                Msg.Text = row["message"].ToString().Trim();
            }
            else
            {
                dr = null;
                MgsDate.Clear();
                MgeDate.Clear();
                MgTitle.Clear();
                Msg.Clear();
            }
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            btnState = "Append";
            MgsDate.ReadOnly = MgeDate.ReadOnly = MgTitle.ReadOnly = Msg.ReadOnly = false;
            btnSave.Enabled = btnCancel.Enabled = true;
            btnAppend.Enabled = btnModify.Enabled = btnDelete.Enabled = false;
            MgsDate.Text = Date.GetDateTime(Common.User_DateTime);
            MgeDate.Text = Date.GetDateTime(Common.User_DateTime);
            MgTitle.Clear();
            Msg.Clear();
            MgsDate.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                btnState = "Modify";
                MgsDate.ReadOnly = MgeDate.ReadOnly = MgTitle.ReadOnly = Msg.ReadOnly = false;
                btnSave.Enabled = btnCancel.Enabled = true;
                btnAppend.Enabled = btnModify.Enabled = btnDelete.Enabled = false;
                MgsDate.Focus();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (MessageBox.Show("請確定是否刪除此筆記錄?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                btnState = "Delete";
                dt.AcceptChanges();
                dr.Delete();
                da1.Update(dt);
                if (dt.Rows.Count > 0)
                {
                    btnState = "";
                    var p = dt.AsEnumerable().Max(r => r["mgID"]);
                    dataGridViewT1.Search("mgID", p.ToString());
                }
                else WriteText(null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MgsDate.Text.Trim().Length == 0)
            {
                MgsDate.Focus();
                return;
            }
            if (MgeDate.Text.Trim().Length == 0)
            {
                MgeDate.Focus();
                return;
            }
            if (MgTitle.Text.Trim().Length == 0)
            {
                MgTitle.Focus();
                return;
            }
            if (MgsDate.Text.BigThen(MgeDate.Text))
            {
                MessageBox.Show("起始日期大於終止日期", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MgsDate.Focus();
                return;
            }
            if (btnState == "Append")
            {
                dr = dt.NewRow();
                dr["mgsdate"] = Date.ToTWDate(MgsDate.Text.Trim());
                dr["mgsdate1"] = Date.ToUSDate(MgsDate.Text.Trim());
                dr["mgedate"] = Date.ToTWDate(MgeDate.Text.Trim());
                dr["mgedate1"] = Date.ToUSDate(MgeDate.Text.Trim());
                dr["mgTitle"] = MgTitle.Text.Trim();
                dr["message"] = Msg.Text.Trim();
                dt.Rows.Add(dr);
                da1.Update(dt);

                MgsDate.Clear();
                MgeDate.Clear();
                MgTitle.Clear();
                Msg.Clear();

                var p = dt.AsEnumerable().Max(r => r["mgID"]);
                dataGridViewT1.Search("mgID", p.ToString());
            }
            else if (btnState == "Modify")
            {
                dr["mgsdate"] = Date.ToTWDate(MgsDate.Text.Trim());
                dr["mgsdate1"] = Date.ToUSDate(MgsDate.Text.Trim());
                dr["mgedate"] = Date.ToTWDate(MgeDate.Text.Trim());
                dr["mgedate1"] = Date.ToUSDate(MgeDate.Text.Trim());
                dr["mgTitle"] = MgTitle.Text.Trim();
                dr["message"] = Msg.Text.Trim();
                da1.Update(dt);

                MgsDate.Clear();
                MgeDate.Clear();
                MgTitle.Clear();
                Msg.Clear();
                dataGridViewT1.Search("mgID", dr["mgID"].ToString());
            }
            MgsDate.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAppend.Enabled = btnModify.Enabled = btnDelete.Enabled = true;
            btnSave.Enabled = btnCancel.Enabled = false;
            MgsDate.ReadOnly = MgeDate.ReadOnly = MgTitle.ReadOnly = Msg.ReadOnly = true;

            var p = dr == null ? "" : dr["mgID"].ToString();
            LoadDB();
            btnState = "";
            if (dt.Rows.Count > 0)
            {
                dataGridViewT1.Search("mgID", p);
                dr = dt.AsEnumerable().ToList().Find(r => r["mgID"].ToString() == p);
            }
            else WriteText(null);
            btnAppend.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (btnState == "")
                {
                    var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                    if (p == null) return;
                    dr = dt.Rows[p.Index];
                    WriteText(dr);
                }
                else if (btnState == "Modify")
                {
                    var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                    if (p == null) return;
                    dr = dt.Rows[p.Index];
                    WriteText(dr);
                }
            }
        }

        private void MgsDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            if (btnCancel.Focused) return;
            if (MgsDate.Text.Trim().Length == 0) return;
            if (!MgsDate.IsDateTime())
            {
                e.Cancel = true;
                MgsDate.SelectAll();
                MessageBox.Show("日期格式錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MgeDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            if (btnCancel.Focused) return;
            if (MgeDate.Text.Trim().Length == 0) return;
            if (!MgeDate.IsDateTime())
            {
                e.Cancel = true;
                MgeDate.SelectAll();
                MessageBox.Show("日期格式錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
