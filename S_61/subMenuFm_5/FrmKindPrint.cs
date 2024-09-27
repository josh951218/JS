using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmKindPrint : Formbase
    {
        JBS.JS.xEvents xe;

        DataTable dt = new DataTable();
        RPT rp;

        public FrmKindPrint()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmKindPrint_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }

        private void KiNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }

        void LoadDB()
        {
            dt.Clear();
            if (KiNo.Text.Trim().Length > 0 && KiNo1.Text.Trim().Length > 0)
            {
                if (KiNo.Text.Trim().BigThen(KiNo1.Text.Trim()))
                {
                    MessageBox.Show("起始類別編號大於終止類別編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@kino", KiNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@kino1", KiNo1.Text.Trim());

                    string sql = "select *,稅='' from kind where 0=0 ";
                    if (KiNo.Text.Trim().Length > 0) sql += " and kino>=@kino";
                    if (KiNo1.Text.Trim().Length > 0) sql += " and kino<=@kino1";
                    cmd.CommandText = sql;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["稅"] = dt.Rows[i]["KiTax"].ToDecimal() == 1 ? "應稅" : "免稅";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void paramsInit()
        {
            LoadDB();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = Common.reportaddress + "Report\\產品類別簡要表.rpt";
            string txtend = "";
            if (radioT1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            var p = Common.User_DateTime == 1 ? Date.GetDateTime(1, true) : Date.GetDateTime(2, true);
            p = "製表日期：" + p;

            rp = new RPT(dt, path);
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateCreated", p });
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Print();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.PreView();
        }
    }
}
