using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmCheckItem : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable CuDt = new DataTable();
        DataTable ItDt = new DataTable();
        string DateRange = "";

        bool Isflag;

        public FrmCheckItem()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();


        }

        private void FrmCheckItem_Load(object sender, EventArgs e)
        {
            OrDate.SetDateLength();
            OrDate1.SetDateLength();

            if (Common.User_DateTime == 1)
            {
                OrDate.Text = Date.GetDateTime(1, false);
                OrDate.Text = OrDate.Text.Remove(5) + "01";
                OrDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                OrDate.Text = Date.GetDateTime(2, false);
                OrDate.Text = OrDate.Text.Remove(6) + "01";
                OrDate1.Text = Date.GetDateTime(2, false);
            }
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

        private void btnBrow_Click(object sender, EventArgs e)
        {
            DateRange = Date.AddLine(OrDate.Text.ToString().Trim()) + "～" + Date.AddLine(OrDate1.Text.ToString().Trim());

            if (!Compare(OrDate, OrDate1)) return;

            string sql = "";

            try
            {
                CuDt.Clear();
                ItDt.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(OrDate.Text.ToString().Trim()));
                    cmd.Parameters.AddWithValue("esdate1", Date.ToTWDate(OrDate1.Text.ToString().Trim()));

                    cn.Open();
                    sql = @"select odd.*,
                            od.cuname1,ct.cuname2,ct.cuper1,ct.cutel1,ct.cufax1,產品分類='',單據日期='',交貨日期='',em.emname,序號='' from 
                            (
                            select esdate,esdate1,orno,itno,itname,qty,itunit,
	                            qtynotout,priceb,prs,taxpriceb,mnyb,
	                            price,taxprice,mny,ordate,ordate1,cuno,emno,bomid,AdAddr,Adper1,Adtel,recordno from orderd WHERE qtyNotOut>0 and esdate >=@esdate and esdate <=@esdate1
                            )odd
                            left join dbo.[order] od on odd.orno = od.orno
                            left join cust ct ON odd.cuno=ct.cuno 
                            left join empl em ON odd.emno=em.emno";
                    sql += " WHERE od.oroverflag <> 1";
                    if(ck1.Checked)
                        sql += " and od.orpickingflag <> 1";
                    sql += " order by odd.orno,odd.recordno";
                    cmd.CommandText = sql;
                    dd.Fill(CuDt);

                    if (CuDt.Rows.Count == 0)
                    {
                        MessageBox.Show("查詢不到資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        OrDate.Focus();
                        return;
                    }

                    sql = @"select orderd.itno,orderd.itname,SUM(orderd.qtyNotOut)total,orderd.itunit from 
                            (select orno,itno,itname,qtyNotOut,itunit from orderd where qtyNotOut>0 and esdate >=@esdate and esdate <=@esdate1) orderd
                            left join [order] on orderd.orno = [order].orno
                            where oroverflag <> 1";
                    if(ck1.Checked)
                        sql += " and orpickingflag <> 1";
                    sql += " group by orderd.itno,orderd.itname,orderd.itunit order by orderd.itno";
                    cmd.CommandText = sql;
                    dd.Fill(ItDt);
                    var datatype = Common.User_DateTime == 1 ? "" : "1";

                    foreach (DataRow i in CuDt.Rows)
                    {
                        i["單據日期"] = Date.AddLine(i["ordate" + datatype].ToString().Trim());
                        i["交貨日期"] = Date.AddLine(i["esdate" + datatype].ToString().Trim());
                    }
                }

                this.OpemInfoFrom<FrmCheckItemb>(() =>
                {
                    FrmCheckItemb frm = new FrmCheckItemb();
                    frm.CuDt = CuDt.Copy();
                    frm.ItDt = ItDt.Copy();
                    frm.DateRange = DateRange;
                    return frm;
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Date_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
