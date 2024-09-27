using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.subMenuFm_2; 

namespace S_61.subMenuFm_1
{
    public partial class FrmFord_Info : Formbase
    {
        JBS.JS.Ford jFord;

        bool Error = false;
        DataTable table = new DataTable();

        public FrmFord_Info()
        {
            InitializeComponent();
            this.jFord = new JBS.JS.Ford();

            lblT11.Text = Common.Sys_MemoUdf;
        }

        private void FrmFord_Info_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                fodate.MaxLength = fodate1.MaxLength = 7;
                esdate.MaxLength = esdate1.MaxLength = 7;
                fodate.Text = Date.GetDateTime(1, false).Remove(5) + "01";
                fodate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                fodate.MaxLength = fodate1.MaxLength = 8;
                esdate.MaxLength = esdate1.MaxLength = 8;
                fodate.Text = Date.GetDateTime(2, false).Remove(6) + "01";
                fodate1.Text = Date.GetDateTime(2, false);
            }
            
            fodate.Focus();
        }

        bool Compare(TextBox tb, TextBox tb1)
        {
            Error = false;
            if (tb.Text == "" || tb1.Text == "")
                return Error;
            if (string.CompareOrdinal(tb.Text.Trim(), tb1.Text.Trim()) > 0)
            {
                MessageBox.Show("起迄" + tb.Tag + "不可大於終止" + tb.Tag + "", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                Error = true;
            }
            return Error;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (Compare(fodate, fodate1)) return;
            if (Compare(esdate, esdate1)) return;
            if (Compare(fano, fano1)) return;
            if (Compare(emno, emno1)) return;
            if (Compare(itno, itno1)) return;
            if (Compare(spno, spno1)) return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                try
                {
                    string sql = "SELECT 交貨日期="
                               + " case"
                               + " when 1=" + Common.User_DateTime + " then d.esdate"
                               + " when 2=" + Common.User_DateTime + " then d.esdate1"
                               + " end,採購日期="
                               + " case"
                               + " when 1=" + Common.User_DateTime + " then d.fodate"
                               + " when 2=" + Common.User_DateTime + " then d.fodate1"
                               + " end,序號='',d.*,a.faname1,a.emname,c.faper1,c.fatel1,c.fafax1,a.spno"
                               + " from ford a left join fordd d on a.fono=d.fono left join fact c on d.fano=c.fano where '0'='0'"
                               + " and d.fodate >=@fodate"
                               + " and d.fodate <=@fodate1";
                    if (esdate.Text.Trim() != "")
                        sql += " and d.esdate >=@esdate";
                    if (esdate1.Text.Trim() != "")
                        sql += " and d.esdate <=@esdate1";
                    if (emno.Text.Trim() != "")
                        sql += " and d.emno >=@emno";
                    if (emno1.Text.Trim() != "")
                        sql += " and d.emno <=@emno1";
                    if (fano.Text.Trim() != "")
                        sql += " and d.fano >=@fano";
                    if (fano1.Text.Trim() != "")
                        sql += " and d.fano <=@fano1";
                    if (itno.Text.Trim() != "")
                        sql += " and d.itno >=@itno";
                    if (itno1.Text.Trim() != "")
                        sql += " and d.itno <=@itno1";
                    if (spno.Text.Trim() != "")
                        sql += " and a.spno >=@spno";
                    if (spno1.Text.Trim() != "")
                        sql += " and a.spno <=@spno1";
                    if (memo.Text.Trim() != "")
                        sql += " and d.memo like '%' + @memo + '%'";
                    if (rd2.Checked)
                    {
                        sql += " and d.qtynotin > 0";
                    }
                    if (rd4.Checked)
                    {
                        sql += " and a.fooverflag = 0";
                    }
                    sql += " order by d.esdate,d.fono";
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fodate", Date.ToTWDate(fodate.Text.Trim()));
                    cmd.Parameters.AddWithValue("fodate1", Date.ToTWDate(fodate1.Text.Trim()));
                    cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(esdate.Text.Trim()));
                    cmd.Parameters.AddWithValue("esdate1", Date.ToTWDate(esdate1.Text.Trim()));
                    cmd.Parameters.AddWithValue("emno", emno.Text.Trim());
                    cmd.Parameters.AddWithValue("emno1", emno1.Text.Trim());
                    cmd.Parameters.AddWithValue("fano", fano.Text.Trim());
                    cmd.Parameters.AddWithValue("fano1", fano1.Text.Trim());
                    cmd.Parameters.AddWithValue("itno", itno.Text.Trim());
                    cmd.Parameters.AddWithValue("itno1", itno1.Text.Trim());
                    cmd.Parameters.AddWithValue("spno", spno.Text.Trim());
                    cmd.Parameters.AddWithValue("spno1", spno1.Text.Trim());
                    cmd.Parameters.AddWithValue("memo", memo.Text.Trim());
                    cmd.CommandText = sql;
                    SqlDataAdapter dd = new SqlDataAdapter(cmd);
                    table.Clear();
                    dd.Fill(table);

                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    int i = 0;
                    table.AsEnumerable().ToList().ForEach(r =>
                    {
                        r["序號"] = i;
                        r["交貨日期"] = Date.AddLine(r["交貨日期"].ToString());
                        r["採購日期"] = Date.AddLine(r["採購日期"].ToString());
                        ++i;
                    });
                    //using (var frm = new FrmFord_Infob())
                    //{
                    //    frm.table = table;
                    //    frm.DateRange = Date.AddLine(fodate.Text) + "～" + Date.AddLine(fodate1.Text);
                    //    frm.ShowDialog();
                    //}
                    this.OpemInfoFrom<FrmFord_Infob>(() =>
                            {
                                FrmFord_Infob frm = new FrmFord_Infob();
                                frm.table = table.Copy();
                                frm.DateRange = Date.AddLine(fodate.Text) + "～" + Date.AddLine(fodate1.Text);
                                return frm;
                            });
                    table.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void fodate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jFord.DateValidate(sender, e);
        }

        private void esdate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jFord.DateValidate(sender, e, true);
        }

        private void fano_DoubleClick(object sender, EventArgs e)
        {
            jFord.Open<JBS.JS.Fact>(sender);
        }

        private void fano_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jFord.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                (sender as TextBox).Text = row["fano"].ToString().Trim();
            }, true);
        }

        private void emno_DoubleClick(object sender, EventArgs e)
        {
            jFord.Open<JBS.JS.Empl>(sender);
        }

        private void emno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jFord.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                (sender as TextBox).Text = row["emno"].ToString().Trim();
            }, true);
        }

        private void itno_DoubleClick(object sender, EventArgs e)
        {
            jFord.Open<JBS.JS.Item>(sender);
        }

        private void itno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jFord.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                (sender as TextBox).Text = row["itno"].ToString().Trim();
            }, true);
        }

        private void memo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    memo.Text = frm.Memo.GetUTF8(20);
                }
            }
        }

        private void spno_DoubleClick(object sender, EventArgs e)
        {
            jFord.Open<JBS.JS.Spec>(sender);
        }

        private void spno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jFord.ValidateOpen<JBS.JS.Spec>(sender, e, row =>
            {
                (sender as TextBox).Text = row["spno"].ToString().Trim();
            }, true);
        }


    }
}
