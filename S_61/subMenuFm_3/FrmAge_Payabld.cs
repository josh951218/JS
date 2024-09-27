using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmAge_Payabld : Formbase
    {
        JBS.JS.xEvents xe;
        List<TextBoxNumberT> Rule;
        DataTable Alltb = new DataTable();
        DataTable tb = new DataTable();
        List<DataRow> list;

        public FrmAge_Payabld()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            Rule = new List<TextBoxNumberT>() { Rule1_1, Rule1_2, Rule2_1, Rule2_2, Rule3_1, Rule3_2, Rule4_1, Rule4_2, Rule5_1 };
        }

        private void FrmAge_Payabld_Load(object sender, EventArgs e)
        {
            qdate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
   
            qdate.Text = Date.GetDateTime(Common.User_DateTime);
            Rule.ForEach(r =>
            {
                if (r.Name != "Rule1_1")
                    r.ReadOnly = false;
            });
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (string.Compare(qfano1.Text, qfano2.Text) > 0)
            {
                MessageBox.Show("起始廠商編號 不可大於終止 廠商編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qfano1.Focus();
                return;
            }
            Rule.ForEach(r =>
            {
                if (r.Text == "")
                {
                    MessageBox.Show("日期區間不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    r.Focus();
                    return;
                }
            });
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        if (qfano1.Text.Trim() != "") cmd.Parameters.AddWithValue("fano", qfano1.Text.Trim());
                        if (qfano2.Text.Trim() != "") cmd.Parameters.AddWithValue("fano1", qfano2.Text.Trim());

                        SqlDataAdapter dd;
                        string sql = "";
                        sql = "select NoN='',RuleLv1=0.0,RuleLv2=0.0,RuleLv3=0.0,RuleLv4=0.0,RuleLv5=0.0,筆數='',fano,faname1 from fact where '0'='0'";
                        if (qfano1.Text.Trim() != "")
                            sql += " and fano >=@fano";
                        if (qfano2.Text.Trim() != "")
                            sql += " and fano <=@fano1";
                        sql += " order by fano";
                        cmd.CommandText = sql;
                        dd = new SqlDataAdapter(cmd);
                        Alltb.Clear();
                        dd.Fill(Alltb);
                        dd.Dispose();

                        sql = "select fano,faname1,bsdate,bsdate1,bsdateac,bsdateac1,acctmny from bshop where '0'='0' and acctmny != 0";
                        if (qfano1.Text.Trim() != "")
                            sql += " and fano >=@fano";
                        if (qfano2.Text.Trim() != "")
                            sql += " and fano <=@fano1";
                        if (rd3.Checked)
                            sql += " and bsdateac <=@qdate";
                        if (rd4.Checked)
                            sql += " and bsdate <=@qdate";
                        if (qspno.Text.Trim() != "")
                            sql += " and spno =@spno";
                        sql += " union all ";
                        sql += "select fano,faname1,bsdate,bsdate1,bsdateac,bsdateac1,acctmny = (-1)*acctmny from rshop where '0'='0' and acctmny != 0";
                        if (qfano1.Text.Trim() != "")
                            sql += " and fano >=@fano";
                        if (qfano2.Text.Trim() != "")
                            sql += " and fano <=@fano1";
                        if (rd3.Checked)
                            sql += " and bsdateac <=@qdate";
                        if (rd4.Checked)
                            sql += " and bsdate <=@qdate";
                        if (qspno.Text.Trim() != "")
                            sql += " and spno =@spno";

                        cmd.Parameters.AddWithValue("qdate", Date.ToTWDate(qdate.Text.Trim()));
                        if (qspno.Text.Trim() != "") cmd.Parameters.AddWithValue("spno", qspno.Text.Trim());

                        cmd.CommandText = sql;
                        dd = new SqlDataAdapter(cmd);
                        tb.Clear();
                        dd.Fill(tb);
                        dd.Dispose();
                    }
                }
                if (tb.Rows.Count == 0)
                {
                    MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    list = tb.AsEnumerable().ToList();
                }
                DateTime t1, t2;
                TimeSpan ts;
                t1 = Convert.ToDateTime(Date.AddLine(Date.ToUSDate(qdate.Text.Trim())));
                DataTable temp = new DataTable();
                for (int i = 0; i < Alltb.Rows.Count; i++)
                {
                    if (list.Where(r => r["fano"].ToString().Trim() == Alltb.Rows[i]["fano"].ToString().Trim()).Count() == 0)
                    {
                        Alltb.Rows[i]["NoN"] = "Y";
                        continue;
                    }
                    temp = list.Where(r => r["fano"].ToString().Trim() == Alltb.Rows[i]["fano"].ToString().Trim()).CopyToDataTable();
                    Alltb.Rows[i]["筆數"] = temp.Rows.Count;
                    for (int j = 0; j < temp.Rows.Count; j++)
                    {
                        t2 = rd3.Checked == true ? Convert.ToDateTime(Date.AddLine(temp.Rows[j]["bsdateac1"].ToString().Trim())) : Convert.ToDateTime(Date.AddLine(temp.Rows[j]["bsdate1"].ToString().Trim()));
                        ts = t1 - t2;
                        if (ts.Days.ToDecimal() >= Rule1_1.Text.ToDecimal() && ts.Days.ToDecimal() <= Rule1_2.Text.ToDecimal())
                            Alltb.Rows[i]["RuleLv1"] = Alltb.Rows[i]["RuleLv1"].ToDecimal() + temp.Rows[j]["acctmny"].ToDecimal();
                        else if (ts.Days.ToDecimal() >= Rule2_1.Text.ToDecimal() && ts.Days.ToDecimal() <= Rule2_2.Text.ToDecimal())
                            Alltb.Rows[i]["RuleLv2"] = Alltb.Rows[i]["RuleLv2"].ToDecimal() + temp.Rows[j]["acctmny"].ToDecimal();
                        else if (ts.Days.ToDecimal() >= Rule3_1.Text.ToDecimal() && ts.Days.ToDecimal() <= Rule3_2.Text.ToDecimal())
                            Alltb.Rows[i]["RuleLv3"] = Alltb.Rows[i]["RuleLv3"].ToDecimal() + temp.Rows[j]["acctmny"].ToDecimal();
                        else if (ts.Days.ToDecimal() >= Rule4_1.Text.ToDecimal() && ts.Days.ToDecimal() <= Rule4_2.Text.ToDecimal())
                            Alltb.Rows[i]["RuleLv4"] = Alltb.Rows[i]["RuleLv4"].ToDecimal() + temp.Rows[j]["acctmny"].ToDecimal();
                        else
                            Alltb.Rows[i]["RuleLv5"] = Alltb.Rows[i]["RuleLv5"].ToDecimal() + temp.Rows[j]["acctmny"].ToDecimal();
                    }
                }
                //using (FrmAge_Payabldb frm = new FrmAge_Payabldb())
                //{
                //    frm.tbM = Alltb.AsEnumerable().Where(r => r["NoN"].ToString().Trim() != "Y").OrderBy(r => r["fano"].ToString()).CopyToDataTable();
                //    frm.date.Text = Date.AddLine(qdate.Text.Trim());
                //    frm.spname = SpName.Text.Trim();
                //    if (rd1.Checked)
                //    {
                //        frm.Rule1_2 = Rule1_2.Text.Trim().PadLeft(3, ' ') + "日以下";
                //        frm.Rule2_1 = Rule2_1.Text.Trim().PadLeft(3, ' ') + "日";
                //        frm.Rule2_2 = Rule2_2.Text.Trim().PadLeft(3, ' ') + "日";
                //        frm.Rule3_1 = Rule3_1.Text.Trim().PadLeft(3, ' ') + "日";
                //        frm.Rule3_2 = Rule3_2.Text.Trim().PadLeft(3, ' ') + "日";
                //        frm.Rule4_1 = Rule4_1.Text.Trim().PadLeft(3, ' ') + "日";
                //        frm.Rule4_2 = Rule4_2.Text.Trim().PadLeft(3, ' ') + "日";
                //        frm.Rule5_1 = Rule5_1.Text.Trim().PadLeft(3, ' ') + "日以上";
                //    }
                //    else
                //    {
                //        ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule1_2.Text.Trim()));
                //        t2 = t1.Add(ts);
                //        frm.Rule1_2 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) + "以後" : t2.ToString("yyyyMMdd") + "以後";

                //        ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule2_1.Text.Trim()));
                //        t2 = t1.Add(ts);
                //        frm.Rule2_1 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                //        ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule2_2.Text.Trim()));
                //        t2 = t1.Add(ts);
                //        frm.Rule2_2 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                //        ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule3_1.Text.Trim()));
                //        t2 = t1.Add(ts);
                //        frm.Rule3_1 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                //        ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule3_2.Text.Trim()));
                //        t2 = t1.Add(ts);
                //        frm.Rule3_2 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                //        ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule4_1.Text.Trim()));
                //        t2 = t1.Add(ts);
                //        frm.Rule4_1 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                //        ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule4_2.Text.Trim()));
                //        t2 = t1.Add(ts);
                //        frm.Rule4_2 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                //        ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule5_1.Text.Trim()));
                //        t2 = t1.Add(ts);
                //        frm.Rule5_1 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) + "以前" : t2.ToString("yyyyMMdd") + "以前";

                //    } 
                //    frm.ShowDialog();
                //}
                this.OpemInfoFrom<FrmAge_Payabldb>(() =>
                            {
                                FrmAge_Payabldb frm = new FrmAge_Payabldb();
                                frm.tbM = Alltb.AsEnumerable().Where(r => r["NoN"].ToString().Trim() != "Y").OrderBy(r => r["fano"].ToString()).CopyToDataTable();
                                frm.date.Text = Date.AddLine(qdate.Text.Trim());
                                frm.spname = SpName.Text.Trim();
                                if (rd1.Checked)
                                {
                                    frm.Rule1_2 = Rule1_2.Text.Trim().PadLeft(3, ' ') + "日以下";
                                    frm.Rule2_1 = Rule2_1.Text.Trim().PadLeft(3, ' ') + "日";
                                    frm.Rule2_2 = Rule2_2.Text.Trim().PadLeft(3, ' ') + "日";
                                    frm.Rule3_1 = Rule3_1.Text.Trim().PadLeft(3, ' ') + "日";
                                    frm.Rule3_2 = Rule3_2.Text.Trim().PadLeft(3, ' ') + "日";
                                    frm.Rule4_1 = Rule4_1.Text.Trim().PadLeft(3, ' ') + "日";
                                    frm.Rule4_2 = Rule4_2.Text.Trim().PadLeft(3, ' ') + "日";
                                    frm.Rule5_1 = Rule5_1.Text.Trim().PadLeft(3, ' ') + "日以上";
                                }
                                else
                                {
                                    ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule1_2.Text.Trim()));
                                    t2 = t1.Add(ts);
                                    frm.Rule1_2 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) + "以後" : t2.ToString("yyyyMMdd") + "以後";

                                    ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule2_1.Text.Trim()));
                                    t2 = t1.Add(ts);
                                    frm.Rule2_1 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                                    ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule2_2.Text.Trim()));
                                    t2 = t1.Add(ts);
                                    frm.Rule2_2 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                                    ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule3_1.Text.Trim()));
                                    t2 = t1.Add(ts);
                                    frm.Rule3_1 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                                    ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule3_2.Text.Trim()));
                                    t2 = t1.Add(ts);
                                    frm.Rule3_2 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                                    ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule4_1.Text.Trim()));
                                    t2 = t1.Add(ts);
                                    frm.Rule4_1 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                                    ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule4_2.Text.Trim()));
                                    t2 = t1.Add(ts);
                                    frm.Rule4_2 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) : t2.ToString("yyyyMMdd");

                                    ts = TimeSpan.FromDays(-1 * Convert.ToDouble(Rule5_1.Text.Trim()));
                                    t2 = t1.Add(ts);
                                    frm.Rule5_1 = Common.User_DateTime == 1 ? DateToTW(t2.ToString("yyyyMMdd")) + "以前" : t2.ToString("yyyyMMdd") + "以前";

                                } 
                                return frm;
                            });

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

        private void qdate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) 
                return;

            xe.DateValidate(sender, e);
        }

        private void qfano1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender); 
        }

        string DateToTW(string d)
        {
            int Year = 0;
            if (d.Trim() == "") return d;
            int.TryParse(d.Substring(0, 4), out Year);
            Year -= 1911;
            return Year + d.Substring(4);
        }

        private void qspno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Spec>(sender, row =>
            {
                qspno.Text = row["SpNo"].ToString().Trim();
                SpName.Text = row["spname"].ToString().Trim();
            });
        }
        private void qspno_Validating(object sender, CancelEventArgs e)
        {
            if (qspno.TrimTextLenth() ==0)
            {
                qspno.Clear();
                SpName.Clear();
                return;
            }

            xe.ValidateOpen<JBS.JS.Spec>(sender, e, row =>
            {
                qspno.Text = row["SpNo"].ToString().Trim();
                SpName.Text = row["spname"].ToString().Trim();
            });
        }
    }
}
