using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmQuote_Info : Formbase
    {
        JBS.JS.xEvents xe;

        bool Error = false;
        DataTable table = new DataTable();
        List<DataRow> list = new List<DataRow>();

        public FrmQuote_Info()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            lblT10.Text = Common.Sys_MemoUdf;
        }

        private void FrmQuote_Info_Load(object sender, EventArgs e)
        {

            if (Common.User_DateTime == 1)
            {
                QuDate.MaxLength = QuDate1.MaxLength = 7;
                QuDateS.MaxLength = QuDateS1.MaxLength = 7;
                QuDate.Text = Date.GetDateTime(1, false).Remove(5) + "01";
                QuDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                QuDate.MaxLength = QuDate1.MaxLength = 8;
                QuDateS.MaxLength = QuDateS1.MaxLength = 8;
                QuDate.Text = Date.GetDateTime(2, false).Remove(6) + "01";
                QuDate1.Text = Date.GetDateTime(2, false);
            }
 
            QuDate.Focus();
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
            try
            {
                if (Compare(QuDate, QuDate1)) return;
                if (Compare(QuDateS, QuDateS1)) return;
                if (Compare(CuNo, CuNo1)) return;
                if (Compare(EmNo, EmNo1)) return;
                if (Compare(ItNo, ItNo1)) return;

                string sql = "";
                sql = "select cu.cufax1,b.*,a.cuname1,a.emname,a.cuno,a.emno,a.cuper1,a.cutel1,報價日期='',預交日期='',序號=''"
                    + " from quoted as b left join quote as a on b.quno=a.quno left join cust as cu on cu.cuno = b.cuno where '0'='0' ";
                if (Common.User_DateTime == 1)
                {
                    sql += " and a.qudate >=@qudate";
                    sql += " and a.qudate <=@qudate1";
                    if (QuDateS.Text != "")
                        sql += " and a.qudates >=@qudates";
                    if (QuDateS1.Text != "")
                        sql += " and a.qudates <=@qudates1";
                }
                else
                {
                    sql += " and a.qudate1 >=@qudate";
                    sql += " and a.qudate1 <=@qudate1";
                    if (QuDateS.Text != "")
                        sql += " and a.qudates1 >=@qudates";
                    if (QuDateS1.Text != "")
                        sql += " and a.qudates1 <=@qudates1";
                }
                if (CuNo.Text != "")
                    sql += " and a.cuno >=@cuno";
                if (CuNo1.Text != "")
                    sql += " and a.cuno <=@cuno1";
                if (EmNo.Text != "")
                    sql += " and a.emno >=@emno";
                if (EmNo1.Text != "")
                    sql += " and a.emno <=@emno1";
                if (ItNo.Text != "")
                    sql += " and b.itno >=@itno";
                if (ItNo1.Text != "")
                    sql += " and b.itno <=@itno1";
                if (Memo.Text != "")
                    sql += " and b.memo like '%' + @memo + '%'";
                sql += " order by b.qudate,b.quno";
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("qudate", QuDate.Text.Trim());
                    cmd.Parameters.AddWithValue("qudate1", QuDate1.Text.Trim());
                    if (QuDateS.Text != "") cmd.Parameters.AddWithValue("qudates", QuDateS.Text.Trim());
                    if (QuDateS1.Text != "") cmd.Parameters.AddWithValue("qudates1", QuDateS1.Text.Trim());
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                    cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                    cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("memo", Memo.Text.Trim());
                    cmd.CommandText = sql;
                    SqlDataAdapter dd = new SqlDataAdapter(cmd);
                    table.Clear();
                    list.Clear();
                    dd.Fill(table);
                    if (table.Rows.Count > 0)
                        list = table.AsEnumerable().ToList();
                    if (list.Count == 0)
                    {
                        MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        table.Rows[i]["序號"] = i;
                        if (Common.User_DateTime == 1)
                        {
                            table.Rows[i]["報價日期"] = Date.AddLine(table.Rows[i]["qudate"].ToString());
                            if (table.Rows[i]["qudates"].ToString() == "")
                            {
                                table.Rows[i]["預交日期"] = "   /  /";
                            }
                            else
                            {
                                table.Rows[i]["預交日期"] = Date.AddLine(table.Rows[i]["qudates"].ToString());
                            }
                        }
                        else
                        {
                            table.Rows[i]["報價日期"] = Date.AddLine(table.Rows[i]["qudate1"].ToString());
                            if (table.Rows[i]["qudates1"].ToString() == "")
                            {
                                table.Rows[i]["預交日期"] = "    /  /";
                            }
                            else
                            {
                                table.Rows[i]["預交日期"] = Date.AddLine(table.Rows[i]["qudates1"].ToString());
                            }
                        }
                    }
                    this.OpemInfoFrom<FrmQuote_Infob>(() =>
                    {
                        FrmQuote_Infob frm = new FrmQuote_Infob();
                        frm.table = table;
                        frm.list = list;
                        string date = Date.AddLine(QuDate.Text.Trim()) + "～" + Date.AddLine(QuDate1.Text.Trim());
                        frm.DateRange = date;
                        frm.date = QuDate.Text.Trim();
                        frm.date1 = QuDate1.Text.Trim();
                        frm.dates = QuDateS.Text.Trim();
                        frm.dates1 = QuDateS1.Text.Trim();
                        frm.cuno = CuNo.Text.Trim();
                        frm.cuno1 = CuNo1.Text.Trim();
                        frm.emno = EmNo.Text.Trim();
                        frm.emno1 = EmNo1.Text.Trim();
                        frm.itno = ItNo.Text.Trim();
                        frm.itno1 = ItNo1.Text.Trim();
                        frm.memo = Memo.Text.Trim();
                        return frm;
                    });
                    QuDate.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void QuDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tb = sender as TextBox;
            if (tb.Text == "")
            {
                if (tb.Name == "QuDateS" || tb.Name == "QuDateS1") return;
                MessageBox.Show("輸入的日期不可以有空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                e.Cancel = true;
                return;
            }
            if (!tb.IsDateTime())
            {
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                if (tb.Name == "QuDateS" || tb.Name == "QuDateS1")
                {
                    tb.Text = "";
                }
                else
                {
                    if (tb.Name == "QuDate")
                    {
                        if (Common.User_DateTime == 1)
                            tb.Text = Date.GetDateTime(Common.User_DateTime, false).Remove(5) + "01";
                        else
                            tb.Text = Date.GetDateTime(Common.User_DateTime, false).Remove(6) + "01";
                    }
                    else
                    {
                        tb.Text = Date.GetDateTime(Common.User_DateTime, false);
                    }
                }
            }
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text == "")
                return;

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                tb.Text = row["emno"].ToString().Trim();
            });
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
            if (tb.Text == "")
                return;

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                tb.Text = row["itno"].ToString().Trim();
            });
        }

        private void Memo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(Memo, 20);
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text == "")
                return;

            xe.ValidateOpen<JBS.JS.Cust>(sender, e, row =>
            {
                tb.Text = row["cuno"].ToString().Trim();
            });
        }
    }
}
