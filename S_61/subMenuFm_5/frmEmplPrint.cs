using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmEmplPrint : Formbase
    {
        JBS.JS.xEvents xe;

        DataTable dt = new DataTable();
        string path = "";
        string txtend = "";

        public FrmEmplPrint()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmEmplPrint_Load(object sender, EventArgs e)
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
                    cmd.Parameters.AddWithValue("@emno", EmNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@emno_1", EmNo_1.Text.Trim());

                    string str = "select * from empl left join dept on empl.emdeno=dept.deno where '0'='0'";
                    if (EmNo.Text.Trim() != "") str += " and EmNo >=@emno";
                    if (EmNo_1.Text.Trim() != "") str += " and EmNo <=@emno_1";
                    if (radioT2.Checked) str += " and Len(LTRIM(RTRIM(EmOutday)))=0 ";

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
            path = Common.reportaddress + "Report\\人員基本資料.rpt";

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
            rp.office = "員工基本資料";
            rp.Word();
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            paramsInit();
            RPT rp = new RPT(dt, path);
            rp.txtend = txtend;
            rp.office = "員工基本資料";
            rp.Excel();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }
    }
}
