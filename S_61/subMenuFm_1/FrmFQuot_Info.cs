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
    public partial class FrmFQuot_Info : Formbase
    {
        JBS.JS.xEvents xe;

        bool Error = false;
        DataTable table = new DataTable();
        List<DataRow> list = new List<DataRow>();

        public FrmFQuot_Info()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            lblT10.Text = Common.Sys_MemoUdf;
        }

        private void FrmFQuot_Info_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                FqDate.MaxLength = FqDate1.MaxLength = 7;
                FqDateS.MaxLength = FqDateS1.MaxLength = 7;
                FqDate.Text = Date.GetDateTime(1, false).Remove(5) + "01";
                FqDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                FqDate.MaxLength = FqDate1.MaxLength = 8;
                FqDateS.MaxLength = FqDateS1.MaxLength = 8;
                FqDate.Text = Date.GetDateTime(2, false).Remove(6) + "01";
                FqDate1.Text = Date.GetDateTime(2, false);
            }
 

            FqDate.Focus();
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
                if (Compare(FqDate, FqDate1)) return;
                if (Compare(FqDateS, FqDateS1)) return;
                if (Compare(FaNo, FaNo1)) return;
                if (Compare(EmNo, EmNo1)) return;
                if (Compare(ItNo, ItNo1)) return;

                string sql = "";
                sql = "select fa.fafax1,b.*,a.faname1,a.emname,a.fano,a.emno,a.faper1,a.fatel1,詢價日期='',預交日期='',序號=''"
                    + " from fquotd as b left join fquot as a on b.fqno=a.fqno left join fact as fa on fa.fano = b.fano where '0'='0' ";
                if (Common.User_DateTime == 1)
                {
                    sql += " and a.fqdate >=@fqdate";
                    sql += " and a.fqdate <=@fqdate1";
                    if (FqDateS.Text != "")
                        sql += " and a.fqdates >=@fqdates";
                    if (FqDateS1.Text != "")
                        sql += " and a.fqdates <=@fqdates1";
                }
                else
                {
                    sql += " and a.fqdate1 >=@fqdate";
                    sql += " and a.fqdate1 <=@fqdate1";
                    if (FqDateS.Text != "")
                        sql += " and a.fqdates1 >=@fqdates";
                    if (FqDateS1.Text != "")
                        sql += " and a.fqdates1 <=@fqdates1";
                }
                if (FaNo.Text != "")
                    sql += " and a.fano >=@fano";
                if (FaNo1.Text != "")
                    sql += " and a.fano <=@fano1";
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
                sql += " order by b.fqdate,b.fqno";
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    //cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fqdate", FqDate.Text.Trim());
                    cmd.Parameters.AddWithValue("fqdate1", FqDate1.Text.Trim());
                    cmd.Parameters.AddWithValue("fqdates", FqDateS.Text.Trim());
                    cmd.Parameters.AddWithValue("fqdates1", FqDateS1.Text.Trim());
                    cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("fano1", FaNo1.Text.Trim());
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
                            table.Rows[i]["詢價日期"] = Date.AddLine(table.Rows[i]["fqdate"].ToString());
                            if (table.Rows[i]["fqdates"].ToString() == "")
                            {
                                table.Rows[i]["預交日期"] = "   /  /";
                            }
                            else
                            {
                                table.Rows[i]["預交日期"] = Date.AddLine(table.Rows[i]["fqdates"].ToString());
                            }
                        }
                        else
                        {
                            table.Rows[i]["詢價日期"] = Date.AddLine(table.Rows[i]["fqdate1"].ToString());
                            if (table.Rows[i]["fqdates1"].ToString() == "")
                            {
                                table.Rows[i]["預交日期"] = "    /  /";
                            }
                            else
                            {
                                table.Rows[i]["預交日期"] = Date.AddLine(table.Rows[i]["fqdates1"].ToString());
                            }
                        }
                    }
                    //using (var frm = new FrmFQuot_Infob())
                    //{ 
                    //    frm.table = table;
                    //    frm.list = list;
                    //    string date = Date.AddLine(FqDate.Text.Trim()) + "～" + Date.AddLine(FqDate1.Text.Trim());
                    //    frm.DateRange = date;
                         
                    //    frm.ShowDialog();
                    //}
                    this.OpemInfoFrom<FrmFQuot_Infob>(() =>
                    {
                        FrmFQuot_Infob frm = new FrmFQuot_Infob();
                        frm.table = table;
                        frm.list = list;
                        string date = Date.AddLine(FqDate.Text.Trim()) + "～" + Date.AddLine(FqDate1.Text.Trim());
                        return frm;
                    });
                    FqDate.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void FqDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;
            TextBox tb = sender as TextBox;
            if (tb.Text == "")
            {
                if (tb.Name == "FqDateS" || tb.Name == "FqDateS1") return;
                MessageBox.Show("輸入的日期不可以有空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                e.Cancel = true;
                return;
            }
            if (!tb.IsDateTime())
            {
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                if (tb.Name == "FqDateS" || tb.Name == "FqDateS1")
                {
                    tb.Text = "";
                }
                else
                {
                    if (tb.Name == "FqDate")
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

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void FaNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text == "")
                return;

            xe.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                tb.Text = row["fano"].ToString().Trim();
            });
        }



    }
}
