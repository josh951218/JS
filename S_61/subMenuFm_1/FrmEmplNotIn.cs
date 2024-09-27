using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmEmplNotIn : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmEmplNotIn()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmEmplNotIn_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                EsDate.MaxLength = 7;
                EsDate1.MaxLength = 7;
                EsDate.Text = Date.GetDateTime(1, false);
                EsDate.Text = EsDate.Text.Remove(5) + "01";
                EsDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                EsDate.MaxLength = 8;
                EsDate1.MaxLength = 8;
                EsDate.Text = Date.GetDateTime(2, false);
                EsDate.Text = EsDate.Text.Remove(6) + "01";
                EsDate1.Text = Date.GetDateTime(2, false);
            }
            EsDate.Focus();
            EsDate.Select();
        }

        private void EsDate1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                ((TextBox)sender).Text = reader["emno"].ToString().Trim();
            }, true);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool Compare(TextBox tx, TextBox tx1)
        {
            bool Isflag = true;
            if (tx.Text != "" && tx1.Text != "")
            {
                if (string.CompareOrdinal(tx.Text, tx1.Text) > 0)
                {
                    Isflag = false;
                    MessageBox.Show("起始" + tx.Tag + "不可大於終止" + tx1.Tag + "，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx.Focus();
                }
            }
            return Isflag;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            string DateRange = Date.AddLine(EsDate.Text.ToString().Trim()) + "～" + Date.AddLine(EsDate1.Text.ToString().Trim());

            if (!Compare(EsDate, EsDate1)) return;
            if (!Compare(EmNo, EmNo1)) return;

            try
            {
                DataTable dtD = new DataTable();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    //不含組件
                    cn.Open();
                    string sql = "SELECT fdd.pqty,fdd.punit,fdd.esdate,fdd.esdate1,fdd.fano,fdd.itno,fdd.emno,"
                         + "交貨日期='',單據日期='',序號='',fdd.fono,"
                         + "fdd.itname,fdd.qty,fdd.itunit,fdd.qtyNotIn,fdd.price,"
                         + "fdd.prs,fdd.taxprice,fdd.mny,fdd.fodate,fdd.fodate1,"
                         + "ft.faname1,ft.faper1,ft.fatel1,ft.fafax1,em.emname "
                         + "FROM fordd fdd "
                         + "left join ford fd on fdd.fono=fd.fono "
                         + "LEFT JOIN fact ft ON fdd.fano=ft.fano "
                         + "LEFT JOIN empl em ON fdd.emno=em.emno "
                         + "WHERE fd.fooverflag <> 1 and fdd.qtyNotIn>0 and fdd.emno<>'' ";
                    if (EsDate.Text.ToString().Trim().Length > 0) sql += "and fdd.esdate >=@esdate ";
                    if (EsDate1.Text.ToString().Trim().Length > 0) sql += "and fdd.esdate <=@esdate1 ";
                    if (EmNo.Text.ToString().Trim().Length > 0) sql += "and fdd.emno >=@emno ";
                    if (EmNo1.Text.ToString().Trim().Length > 0) sql += "and fdd.emno <=@emno1 ";

                    cmd.Parameters.Clear();
                    if (EsDate.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(EsDate.Text.ToString().Trim()));
                    if (EsDate1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("esdate1", Date.ToTWDate(EsDate1.Text.ToString().Trim()));
                    if (EmNo.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("emno", EmNo.Text.ToString().Trim());
                    if (EmNo1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("emno1", EmNo1.Text.ToString().Trim());

                    cmd.CommandText = sql;
                    dd.Fill(dtD);

                    foreach (DataRow i in dtD.Rows)
                    {
                        if (Common.User_DateTime == 1) i["單據日期"] = Date.AddLine(i["fodate"].ToString().Trim());
                        else i["單據日期"] = Date.AddLine(i["fodate1"].ToString().Trim());
                    }

                    foreach (DataRow i in dtD.Rows)
                    {
                        if (Common.User_DateTime == 1) i["交貨日期"] = Date.AddLine(i["esdate"].ToString().Trim());
                        else i["交貨日期"] = Date.AddLine(i["esdate1"].ToString().Trim());
                    }
                }

                if (dtD.Rows.Count > 0)
                {
                    //using (var frm = new FrmEmplNotInb())
                    //{ 
                    //    frm.Dt = dtD.Copy();
                    //    frm.DateRange = DateRange;
                    //    frm.ShowDialog();

                    //    dtD.Clear();
                    //}
                    this.OpemInfoFrom<FrmEmplNotInb>(() =>
                            {
                                FrmEmplNotInb frm = new FrmEmplNotInb();
                                frm.Dt = dtD.Copy();
                                frm.DateRange = DateRange;
                                return frm;
                            });
                }
                else
                {
                    MessageBox.Show("查詢不到資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
