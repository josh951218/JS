using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmCustPrint : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable dt = new DataTable();
        string path = "";
        string txtend = "";

        public FrmCustPrint()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmCustPrint_Load(object sender, EventArgs e)
        {
            CuNo.Focus();
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
                    cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@cuno_1", CuNo_1.Text.Trim());

                    cmd.CommandText = "select * from cust where '0'='0'";
                    if (CuNo.Text.Trim() != "")
                        cmd.CommandText += " and cuno >=@cuno";
                    if (CuNo_1.Text.Trim() != "")
                        cmd.CommandText += " and cuno <=@cuno_1 ";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt.Clear();
                        da.Fill(dt);
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void paramsInit()
        {
            loadDB();
            path = Common.reportaddress + "Report\\客戶資料瀏覽_內定報表.rpt";

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
            rp.office = "客戶基本資料";
            rp.Word();
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            paramsInit();
            RPT rp = new RPT(dt, path);
            rp.txtend = txtend;
            rp.office = "客戶基本資料";
            rp.Excel();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }
    }
}
