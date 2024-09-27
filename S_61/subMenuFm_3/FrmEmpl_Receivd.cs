using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmEmpl_Receivd : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmEmpl_Receivd()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmEmpl_Receivd_Load(object sender, EventArgs e)
        {
            ReDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            ReDate1.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
       
            ReDate.Text = Date.GetDateTime(Common.User_DateTime);
            ReDate.Text = ReDate.Text.takeString(ReDate.Text.Length - 2) + "01";
            ReDate1.Text = Date.GetDateTime(Common.User_DateTime);
            ReDate.Focus();
        }

        private void ReDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) 
                return;

            xe.DateValidate(sender, e);
        }

        private void ReDate1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) 
                return;

            xe.DateValidate(sender, e);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void EmNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (ReDate.Text.BigThen(ReDate1.Text))
            {
                MessageBox.Show("起始日期大於終止日期！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ReDate1.Focus();
                ReDate1.SelectAll();
                return;
            }

            DataTable t = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("redate", Date.ToTWDate(ReDate.Text));
                        cmd.Parameters.AddWithValue("redate1", Date.ToTWDate(ReDate1.Text));
                        if (EmNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        if (EmNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());

                        string sql = "select "
                        + " 折讓總=0.0,現金總=0.0,刷卡總=0.0,禮卷總=0.0,支票總=0.0, "
                        + " 匯入總=0.0,其它總=0.0,取用總=0.0,沖帳總=0.0,累入總=0.0,沖抵總=0.0, "
                        + " * from receiv where 0=0"
                        + " and redate >=@redate"
                        + " and redate <=@redate1";
                        if (EmNo.Text.Trim().Length > 0)
                        {
                            sql += " and receiv.EmNo >=@emno";
                        }
                        if (EmNo1.Text.Trim().Length > 0)
                        {
                            sql += " and receiv.EmNo <=@emno1";
                        }
                        sql += " order by redate,reno ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (t.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.OpemInfoFrom<FrmEmpl_Receivdb>(() =>
                            {
                                FrmEmpl_Receivdb frm = new FrmEmpl_Receivdb();
                                frm.dt = t.Copy();
                                frm.DateRange = "收款區間：" + ReDate.Text.Trim() + " ～ " + ReDate1.Text.Trim();
                                return frm;
                            });
        }
    }
}
