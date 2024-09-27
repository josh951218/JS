using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmFord_Payabld : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmFord_Payabld()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmFord_Payabld_Load(object sender, EventArgs e)
        {
            PaDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            PaDate1.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
        
            PaDate.Text = Date.GetDateTime(Common.User_DateTime);
            PaDate.Text = PaDate.Text.takeString(PaDate.Text.Length - 2) + "01";
            PaDate1.Text = Date.GetDateTime(Common.User_DateTime);
            PaDate.Focus();
        }

        private void PaDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void PaDate1_Validating(object sender, CancelEventArgs e)
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
            if (PaDate.Text.BigThen(PaDate1.Text))
            {
                MessageBox.Show("起始日期大於終止日期！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PaDate1.Focus();
                PaDate1.SelectAll();
                return;
            }

            DataTable t = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("padate", Date.ToTWDate(PaDate.Text));
                        cmd.Parameters.AddWithValue("padate1", Date.ToTWDate(PaDate1.Text));
                        if (EmNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        if (EmNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());

                        string sql = "select "
                        + " 折讓總=0.0,現金總=0.0,刷卡總=0.0,禮卷總=0.0,支票總=0.0, "
                        + " 匯入總=0.0,其它總=0.0,取用總=0.0,沖帳總=0.0,累入總=0.0,沖抵總=0.0, "
                        + " * from Payabl where 0=0"
                        + " and padate >=@padate"
                        + " and padate <=@padate1";
                        if (EmNo.Text.Trim().Length > 0)
                        {
                            sql += " and Payabl.EmNo >=@emno";
                        }
                        if (EmNo1.Text.Trim().Length > 0)
                        {
                            sql += " and Payabl.EmNo <=@emno1";
                        }
                        sql += " order by padate,pano ";

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

            //using (var frm = new FrmFord_Payabldb())
            //{  
            //    frm.dt = t.Copy();
            //    frm.DateRange = "收款區間：" + PaDate.Text.Trim() + " ～ " + PaDate1.Text.Trim();
            //    frm.ShowDialog();
            //}
            this.OpemInfoFrom<FrmFord_Payabldb>(() =>
                            {
                                FrmFord_Payabldb frm = new FrmFord_Payabldb();
                                frm.dt = t.Copy();
                                frm.DateRange = "收款區間：" + PaDate.Text.Trim() + " ～ " + PaDate1.Text.Trim();
                                return frm;
                            });
        }
    }
}
