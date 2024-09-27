using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class 銀行帳號建檔_瀏覽 : Formbase
    {
        public DataRow TResult; 
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        List<TextBoxbase> list;
        DataTable tbM = new DataTable();
        public DataRow chksys;
        public DataRow scrit;
        public string 票據連線字串 = "";
        List<DataRow> li = new List<DataRow>();
         
        public bool 開窗模式 = false;
        public bool 去除外幣帳戶 = false;
        public string CoNo = "";

        public 銀行帳號建檔_瀏覽()
        {
            InitializeComponent();
            this.list = this.getEnumMember();
        }

        private void 銀行帳號建檔_瀏覽_Load(object sender, EventArgs e)
        {
            this.現行餘額.DefaultCellStyle.Format = "N" + chksys["deci"].ToString();
             
            loadM();
            if (li.Count > 0)
            {
                dataGridViewT1.Search("帳戶編號", this.TSeekNo);
            }
            AcNo.Focus();
        }

        void loadM()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                {
                    cn.Open();
                    tbM.Clear();
                    li.Clear();
                    string sql = "select 帳戶類別=case when ackind=1 then '本幣帳戶'  when ackind=2 then '外幣帳戶' end,* from acct where '0'='0'";
                    if (去除外幣帳戶) sql += " and ackind=1 ";
                    if (CoNo != "")
                        sql += " and cono=@cono ";
                    sql += " order by acno";

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        if (CoNo != "") cmd.Parameters.AddWithValue("cono", CoNo);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            dd.Fill(tbM);
                            if (tbM.Rows.Count > 0) li = tbM.AsEnumerable().ToList();
                            dataGridViewT1.DataSource = tbM; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F11:
                    Exit.PerformClick(); 
                    break;
                case Keys.F9:
                    Get.PerformClick();
                    break;
                case Keys.Escape:
                    Exit.PerformClick();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
         
        private void Get_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count > 0)
            {
                //result = dataGridViewT1.SelectedRows[0].Cells["帳戶編號"].Value.ToString().Trim();
                TResult = tbM.Rows[dataGridViewT1.SelectedRows[0].Index];
            }
            this.DialogResult = DialogResult.OK;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //result = dataGridViewT1.SelectedRows[0].Cells["帳戶編號"].Value.ToString().Trim();
            this.Dispose();
            this.Close();
        }
         
        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0 || !開窗模式) return;
            TResult = tbM.Rows[dataGridViewT1.SelectedRows[0].Index];
            this.DialogResult = DialogResult.OK;
        }

        private void AcNo_TextChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0 || AcNo.Text.Trim() == "") return;
            dataGridViewT1.Search("帳戶編號", AcNo.Text);
        } 
    }
}
