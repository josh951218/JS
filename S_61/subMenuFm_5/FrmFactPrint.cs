using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmfactPrint : Formbase
    {
        JBS.JS.xEvents xe;

        DataTable dt = new DataTable();
        string path = "";
        string txtend = "";

        public FrmfactPrint()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmfactPrint_Load(object sender, EventArgs e)
        {
            loadDB();
        }

        void loadDB()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@fano", fano.Text.Trim());
                    cmd.Parameters.AddWithValue("@fano_1", fano_1.Text.Trim());

                    string str = "select * from fact where '0'='0'";
                    if (fano.Text.Trim() != "")
                        str += " and fano >= @fano";
                    if (fano_1.Text.Trim() != "")
                        str += " and fano <= @fano_1";
                    cmd.CommandText = str;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt.Clear();
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadDBError:\n" + ex.ToString());
            }
        }

        void paramsInit()
        {
            loadDB();
            path = Common.reportaddress + "Report\\廠商資料瀏覽_內定報表.rpt";

            if (radioButton1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioButton2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioButton3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioButton4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioButton5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            paramsInit();
            RPT rp = new RPT(dt, path);
            rp.txtend = txtend;
            rp.Print();
        }

        private void btnpreview_Click(object sender, EventArgs e)
        {
            paramsInit();
            RPT rp = new RPT(dt, path);
            rp.txtend = txtend;
            rp.PreView();
        }

        private void btnword_Click(object sender, EventArgs e)
        {
            paramsInit();
            RPT rp = new RPT(dt, path);
            rp.txtend = txtend;
            rp.office = "廠商基本資料";
            rp.Word();
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            paramsInit();
            RPT rp = new RPT(dt, path);
            rp.txtend = txtend;
            rp.office = "廠商基本資料";
            rp.Excel();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }
    }
}
