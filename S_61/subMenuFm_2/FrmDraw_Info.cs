using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmDraw_Info : Formbase
    {
        JBS.JS.xEvents xe;

        bool error = false;
        DataTable table = new DataTable();
        List<DataRow> list = new List<DataRow>();

        public FrmDraw_Info()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            lblT10.Text = Common.Sys_MemoUdf;
        }

        private void FrmDraw_Info_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                DrDate.MaxLength = 7;
                DrDate.Text = Date.GetDateTime(1, false);
                DrDate.Text = DrDate.Text.Remove(5) + "01";
                DrDate1.MaxLength = 7;
                DrDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                DrDate.MaxLength = 8;
                DrDate.Text = Date.GetDateTime(2, false);
                DrDate.Text = DrDate.Text.Remove(6) + "01";
                DrDate1.MaxLength = 8;
                DrDate1.Text = Date.GetDateTime(2, false);
            }
            //DrDate.Init();
            //DrDate1.Init();
        }

        bool compare(TextBox tb, TextBox tb1)
        {
            if (tb.Text == "" || tb1.Text == "")
                return error = false;
            error = false;
            if (string.CompareOrdinal(tb.Text, tb1.Text) > 0)
            {
                MessageBox.Show("起迄" + tb.Tag + "不可大於終止" + tb.Tag + "", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                error = true;
            }
            return error;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (compare(StNoI, StNoI1)) return;
            if (compare(StNoO, StNoO1)) return;
            if (compare(ItNo, ItNo1)) return;
            if (compare(EmNo, EmNo1)) return;

            try
            {
                string sql = "";
                sql += "select 序號=0.0,日期='',A.emname,B.* from drawd as B left join draw as A on B.drno=A.drno where '0'='0'";
                if (Common.User_DateTime == 1)
                {
                    sql += " and B.drdate >=@drdate";
                    sql += " and B.drdate <=@drdate1";
                }
                else
                {
                    sql += " and B.drdate1 >=@drdate";
                    sql += " and B.drdate1 <=@drdate1";
                }

                if (StNoI.Text != "")
                {
                    sql += " and B.stnoi >=@stnoi ";
                }
                if (StNoI1.Text != "")
                {
                    sql += " and B.stnoi <=@stnoi1 ";
                }
                if (StNoO.Text != "")
                {
                    sql += " and B.stnoo >=@stnoo ";
                }
                if (StNoO1.Text != "")
                {
                    sql += " and B.stnoo <=@stnoo1 ";
                }
                if (EmNo.Text != "")
                {
                    sql += " and B.emno >=@emno ";
                }
                if (EmNo1.Text != "")
                {
                    sql += " and B.emno <=@emno1 ";
                }
                if (ItNo.Text != "")
                {
                    sql += " and B.itno >=@itno ";
                }
                if (ItNo1.Text != "")
                {
                    sql += " and B.itno <=@itno1 ";
                }
                if (Memo.Text != "")
                {
                    sql += " and B.memo like '%' + @memo + '%' ";
                }
                sql += " order by B.drdate,B.drno";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drdate", DrDate.Text.Trim());
                        cmd.Parameters.AddWithValue("drdate1", DrDate1.Text.Trim());
                        if (StNoI.Text != "") cmd.Parameters.AddWithValue("stnoi", StNoI.Text.Trim());
                        if (StNoI1.Text != "") cmd.Parameters.AddWithValue("stnoi1", StNoI1.Text.Trim());
                        if (StNoO.Text != "") cmd.Parameters.AddWithValue("stnoo", StNoO.Text.Trim());
                        if (StNoO1.Text != "") cmd.Parameters.AddWithValue("stnoo1", StNoO1.Text.Trim());
                        if (EmNo.Text != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        if (EmNo1.Text != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (Memo.Text != "") cmd.Parameters.AddWithValue("memo", Memo.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            table.Clear();
                            dd.Fill(table);
                            if (table.Rows.Count == 0)
                            {
                                MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            list = table.AsEnumerable().ToList();
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                if (Common.User_DateTime == 1)
                                {
                                    table.Rows[i]["日期"] = Date.AddLine(table.Rows[i]["drdate"].ToString());
                                    table.Rows[i]["序號"] = i;
                                }
                                else
                                {
                                    table.Rows[i]["日期"] = Date.AddLine(table.Rows[i]["drdate1"].ToString());
                                    table.Rows[i]["序號"] = i;
                                }
                            }
                            //using (var frm = new FrmDraw_Infob())
                            //{ 
                            //    frm.table = table;
                            //    string date = "日期區間：" + Date.AddLine(DrDate.Text) + "～" + Date.AddLine(DrDate1.Text);
                            //    frm.DateRange = date;
                            //    frm.list = list;
                            //    frm.stnoi = StNoI.Text;
                            //    frm.stnoi1 = StNoI1.Text;
                            //    frm.stnoo = StNoO.Text;
                            //    frm.stnoo1 = StNoO.Text;
                            //    frm.emno = EmNo.Text;
                            //    frm.emno1 = EmNo1.Text;
                            //    frm.itno = ItNo.Text;
                            //    frm.itno1 = ItNo1.Text;
                            //    frm.memo = Memo.Text;
                            //    frm.date = DrDate.Text;
                            //    frm.date1 = DrDate1.Text;
                            //    frm.ShowDialog();
                            //}
                            this.OpemInfoFrom<FrmDraw_Infob>(() =>
                            {
                                FrmDraw_Infob frm = new FrmDraw_Infob();
                                frm.table = table.Copy(); ;
                                string date = "日期區間：" + Date.AddLine(DrDate.Text) + "～" + Date.AddLine(DrDate1.Text);
                                frm.DateRange = date;
                                frm.list = list;
                                frm.stnoi = StNoI.Text;
                                frm.stnoi1 = StNoI1.Text;
                                frm.stnoo = StNoO.Text;
                                frm.stnoo1 = StNoO.Text;
                                frm.emno = EmNo.Text;
                                frm.emno1 = EmNo1.Text;
                                frm.itno = ItNo.Text;
                                frm.itno1 = ItNo1.Text;
                                frm.memo = Memo.Text;
                                frm.date = DrDate.Text;
                                frm.date1 = DrDate1.Text;
                                return frm;
                            });
                            table.Clear();
                            DrDate.Focus();
                        }
                    }

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

        private void DrNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tb = sender as TextBox;
            if (tb.Text == "")
            {
                MessageBox.Show("輸入日期不可以有空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                e.Cancel = true;
                return;
            }
            if (!tb.IsDateTime())
            {
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                e.Cancel = true;
                if (Common.User_DateTime == 1)
                {
                    if (tb.Name == "DrNo")
                    {
                        tb.Text = Date.GetDateTime(1, false);
                        tb.Text = DrDate.Text.Remove(5) + "01";
                    }
                    else
                    {
                        tb.Text = Date.GetDateTime(1, false);
                    }
                }
                else
                {
                    if (tb.Name == "DrNo")
                    {
                        tb.Text = Date.GetDateTime(2, false);
                        tb.Text = DrDate.Text.Remove(6) + "01";
                    }
                    else
                    {
                        tb.Text = Date.GetDateTime(2, false);
                    }
                }
                return;
            }
        }

        private void StNoO_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void StNoO_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text == "")
                return;

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                tb.Text = row["StNo"].ToString().Trim();
            });
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
    }
}
