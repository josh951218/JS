using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmItemNotIn : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable Dt = new DataTable();
        string DateRange = "";

        bool Isflag;

        public FrmItemNotIn()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmItemNotIn_Load(object sender, EventArgs e)
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

        bool Compare(TextBox tx, TextBox tx1)
        {
            Isflag = true;
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

        private void EsDate1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void ItNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "")
                return;

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                tb.Text = row["itno"].ToString().Trim();
            });
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            DateRange = Date.AddLine(EsDate.Text.ToString().Trim()) + "～" + Date.AddLine(EsDate1.Text.ToString().Trim());

            if (!Compare(EsDate, EsDate1)) return;
            if (!Compare(ItNo, ItNo1)) return;

            string sql = "";

            try
            {
                Dt.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    //不含組件
                    cn.Open();
                    sql = "SELECT fdd.pqty,fdd.punit,fdd.esdate,fdd.esdate1,fdd.fano,fdd.itno,fdd.emno,"
                         + "交貨日期='',單據日期='',序號='',fdd.fono,"
                         + "fdd.itname,fdd.qty,fdd.itunit,fdd.qtyNotIn,fdd.price,"
                         + "fdd.prs,fdd.taxprice,fdd.mny,fdd.fodate,fdd.fodate1,"
                         + "ft.faname1,ft.faper1,ft.fatel1,ft.fafax1,em.emname "
                         + "FROM fordd fdd "
                         + "left join ford fd on fdd.fono=fd.fono "
                         + "LEFT JOIN fact ft ON fdd.fano=ft.fano "
                         + "LEFT JOIN empl em ON fdd.emno=em.emno "
                         + "WHERE fd.fooverflag <> 1 and fdd.qtyNotIn>0 ";
                    if (EsDate.Text.ToString().Trim().Length > 0) sql += " and fdd.esdate >=@date ";
                    if (EsDate1.Text.ToString().Trim().Length > 0) sql += " and fdd.esdate <=@date1 ";
                    if (ItNo.Text.ToString().Trim().Length > 0) sql += " and fdd.itno >=@itno ";
                    if (ItNo1.Text.ToString().Trim().Length > 0) sql += " and fdd.itno <=@itno1 ";
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    if (EsDate.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("date", Date.ToTWDate(EsDate.Text.ToString().Trim()));
                    if (EsDate1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("date1", Date.ToTWDate(EsDate1.Text.ToString().Trim()));
                    if (ItNo.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("itno", ItNo.Text.ToString().Trim());
                    if (ItNo1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("itno1", ItNo1.Text.ToString().Trim());
                    cmd.CommandText = sql;
                    SqlDataAdapter dd;
                    dd = new SqlDataAdapter(cmd);
                    dd.Fill(Dt);

                    foreach (DataRow i in Dt.Rows)
                    {
                        if (Common.User_DateTime == 1) i["單據日期"] = Date.AddLine(i["fodate"].ToString().Trim());
                        else i["單據日期"] = Date.AddLine(i["fodate1"].ToString().Trim());
                    }

                    foreach (DataRow i in Dt.Rows)
                    {
                        if (Common.User_DateTime == 1) i["交貨日期"] = Date.AddLine(i["esdate"].ToString().Trim());
                        else i["交貨日期"] = Date.AddLine(i["esdate1"].ToString().Trim());
                    }

                    if (Dt.Rows.Count > 0)
                    {
                        //using (var frm = new FrmItemNotInb())
                        //{ 
                        //    frm.Dt = Dt.Copy();
                        //    frm.DateRange = DateRange;
                        //    frm.ShowDialog();
                        //}
                        this.OpemInfoFrom<FrmItemNotInb>(() =>
                            {
                                FrmItemNotInb frm = new FrmItemNotInb();
                                frm.Dt = Dt.Copy();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




    }
}
