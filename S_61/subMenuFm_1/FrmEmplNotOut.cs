using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmEmplNotOut : Formbase
    {
        JBS.JS.xEvents xe;

        DataTable Dt = new DataTable();
        DataTable DtD = new DataTable();
        string DateRange = "";

        bool Isflag;

        public FrmEmplNotOut()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmEmplNotOut_Load(object sender, EventArgs e)
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

        private void EmNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "")
                return;

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                tb.Text = row["emno"].ToString().Trim();
            });
        }

        private void Date_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            DateRange = Date.AddLine(EsDate.Text.ToString().Trim()) + "～" + Date.AddLine(EsDate1.Text.ToString().Trim());

            if (!Compare(EsDate, EsDate1)) return;
            if (!Compare(EmNo, EmNo1)) return;

            string sql = "";

            try
            {
                Dt.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    //不含組件
                    cn.Open();
                    sql = "SELECT odd.esdate,odd.esdate1,odd.orno,odd.itno,odd.itname,odd.qty,odd.itunit,odd.pqty,odd.punit,"
                        + "odd.qtynotout,odd.priceb,odd.prs,odd.taxpriceb,odd.mnyb,"
                        + "odd.price,odd.taxprice,odd.mny,odd.ordate,odd.ordate1,"
                        + "odd.cuno,ct.cuname1,ct.cuname2,ct.cuper1,ct.cutel1,ct.cufax1,"
                        + "odd.ittrait,產品分類='',單據日期='',交貨日期='',odd.emno,em.emname,序號='' "
                        + "FROM orderd odd "
                        + "left join dbo.[order] od on odd.orno = od.orno "
                        + "LEFT JOIN cust ct ON odd.cuno=ct.cuno "
                        + "LEFT JOIN empl em ON odd.emno=em.emno "
                        + "WHERE od.oroverflag <> 1 and odd.qtyNotOut>0 and odd.emno<>'' ";
                    if (EsDate.Text.ToString().Trim().Length > 0) sql += "and odd.esdate >=@esdate ";
                    if (EsDate1.Text.ToString().Trim().Length > 0) sql += "and odd.esdate <=@esdate1 ";
                    if (EmNo.Text.ToString().Trim().Length > 0) sql += "and odd.emno >=@emno ";
                    if (EmNo1.Text.ToString().Trim().Length > 0) sql += "and odd.emno <=@emno1 ";
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    if (EsDate.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(EsDate.Text.ToString().Trim()));
                    if (EsDate1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("esdate1", Date.ToTWDate(EsDate1.Text.ToString().Trim()));
                    if (EmNo.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("emno", EmNo.Text.ToString().Trim());
                    if (EmNo1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("emno1", EmNo1.Text.ToString().Trim());
                    cmd.CommandText = sql;
                    SqlDataAdapter dd;
                    dd = new SqlDataAdapter(cmd);
                    dd.Fill(Dt);

                    //含組件
                    sql = "SELECT odd.esdate,odd.esdate1,odd.orno,odd.itno,odd.itname,odd.qty,odd.itunit,odd.pqty,odd.punit,"
                        + "odd.qtynotout,odd.priceb,odd.prs,odd.taxpriceb,odd.mnyb,"
                        + "odd.price,odd.taxprice,odd.mny,odd.ordate,odd.ordate1,"
                        + "odd.cuno,ct.cuname1,ct.cuname2,ct.cuper1,ct.cutel1,ct.cufax1,"
                        + "odd.ittrait,產品分類='',單據日期='',交貨日期='',odd.emno,em.emname,序號='',"
                        + "組件產品=odb.itname,組件數量=odb.itqty,組件單位=odb.itunit,組件名細=odd.bomid "
                        + "FROM orderd odd "
                        + "left join dbo.[order] od on odd.orno = od.orno "
                        + "LEFT JOIN cust ct ON odd.cuno=ct.cuno "
                        + "LEFT JOIN empl em ON odd.emno=em.emno "
                        + "LEFT JOIN orderbom odb ON odd.bomid=odb.bomid "
                        + "WHERE od.oroverflag <> 1 and odd.qtyNotOut>0 and odd.emno<>'' ";
                    if (EsDate.Text.ToString().Trim().Length > 0) sql += "and odd.esdate >=@esdate ";
                    if (EsDate1.Text.ToString().Trim().Length > 0) sql += "and odd.esdate <=@esdate1 ";
                    if (EmNo.Text.ToString().Trim().Length > 0) sql += "and odd.emno >=@emno ";
                    if (EmNo1.Text.ToString().Trim().Length > 0) sql += "and odd.emno <=@emno1 ";
                    cmd.CommandText = sql;
                    dd = new SqlDataAdapter(cmd);
                    dd.Fill(DtD);
                    dd.Dispose();
                    cmd.Dispose();

                    foreach (DataRow i in Dt.Rows)
                    {
                        if (Common.User_DateTime == 1) i["單據日期"] = Date.AddLine(i["ordate"].ToString().Trim());
                        else i["單據日期"] = Date.AddLine(i["ordate1"].ToString().Trim());
                    }
                    foreach (DataRow i in DtD.Rows)
                    {
                        if (Common.User_DateTime == 1) i["單據日期"] = Date.AddLine(i["ordate"].ToString().Trim());
                        else i["單據日期"] = Date.AddLine(i["ordate1"].ToString().Trim());
                    }

                    foreach (DataRow i in Dt.Rows)
                    {
                        if (Common.User_DateTime == 1) i["交貨日期"] = Date.AddLine(i["esdate"].ToString().Trim());
                        else i["交貨日期"] = Date.AddLine(i["esdate1"].ToString().Trim());
                    }
                    foreach (DataRow i in DtD.Rows)
                    {
                        if (Common.User_DateTime == 1) i["交貨日期"] = Date.AddLine(i["esdate"].ToString().Trim());
                        else i["交貨日期"] = Date.AddLine(i["esdate1"].ToString().Trim());
                    } 
                }

                if (Dt.Rows.Count > 0)
                {
                    //using (var frm = new FrmEmplNotOutb())
                    //{
                    //    frm.Dt = Dt.Copy();
                    //    frm.DtD = DtD.Copy();
                    //    frm.DateRange = DateRange;
                    //    frm.ShowDialog();
                    //}
                    this.OpemInfoFrom<FrmEmplNotOutb>(() =>
                            {
                                FrmEmplNotOutb frm = new FrmEmplNotOutb();
                                frm.Dt = Dt.Copy();
                                frm.DtD = DtD.Copy();
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
