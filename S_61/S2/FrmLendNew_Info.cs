using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;


namespace S_61.S2
{
    public partial class FrmLendNew_Info : Formbase
    {
        JBS.JS.Lend jLend;
        bool Error = false;

        DataTable Lend = new DataTable();
        DataTable LendD = new DataTable();
        DataTable RLend = new DataTable();
        DataTable RLendD = new DataTable();

        DataTable dt = new DataTable();
        DataTable dtD = new DataTable();

        public FrmLendNew_Info()
        {
            InitializeComponent();
            this.jLend = new JBS.JS.Lend();
            LeDate.SetDateLength();
            LeDate1.SetDateLength();

            if (Common.User_DateTime == 1)
            {
                LeDate.Text = Date.GetDateTime(1, false).Remove(5) + "01";
                LeDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                LeDate.Text = Date.GetDateTime(2, false).Remove(6) + "01";
                LeDate1.Text = Date.GetDateTime(2, false);
            }
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
            Lend.Clear();
            LendD.Clear();
            RLend.Clear();
            RLendD.Clear();

            if (Compare(LeDate, LeDate1)) return;
            if (Compare(CuNo, CuNo1)) return;
            if (Compare(EmNo, EmNo1)) return;
            if (Compare(StNo, StNo1)) return;
            if (Compare(ItNo, ItNo1)) return;
            if (Compare(KiNo, KiNo1)) return;
            if (ck1.Checked == false)
            {
                if (ck2.Checked == false)
                {
                    MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string str = "";

            decimal b, lendmny = 0, rlendmny = 0;
            dtD.Clear();

            if (ck1.Checked == true)
            {
                str = "";
                if (Common.User_DateTime == 1)
                {
                    if (LeDate.Text.Trim() != "")
                        str += " and  s.ledate >=@ledate";
                    if (LeDate1.Text.Trim() != "")
                        str += " and  s.ledate <=@ledate1";
                }
                else
                {
                    if (LeDate.Text.Trim() != "")
                        str += " and  s.ledate1 >=@ledate";
                    if (LeDate1.Text.Trim() != "")
                        str += " and  s.ledate1 <=@ledate1";
                }

                if (CuNo.Text.Trim() != "")
                    str += " and  s.cuno >=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                if (CuNo.Text.Trim() != "")
                    str += " and  s.cuno <=@cuno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                if (EmNo.Text.Trim() != "")
                    str += " and s.emno >=@emno COLLATE Chinese_Taiwan_Stroke_BIN";
                if (EmNo1.Text.Trim() != "")
                    str += " and s.emno <=@emno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                if (StNo.Text.Trim() != "")
                    str += " and LendD.StNo >=@StNo COLLATE Chinese_Taiwan_Stroke_BIN";
                if (StNo1.Text.Trim() != "")
                    str += " and LendD.StNo <=@StNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                if (ItNo.Text.Trim() != "")
                    str += " and LendD.itno >=@itno COLLATE Chinese_Taiwan_Stroke_BIN";
                if (ItNo1.Text.Trim() != "")
                    str += " and LendD.itno <=@itno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                if (KiNo.Text.Trim() != "")
                    str += " and i.KiNo >=@KiNo COLLATE Chinese_Taiwan_Stroke_BIN";
                if (KiNo1.Text.Trim() != "")
                    str += " and i.KiNo <=@KiNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                try
                {
                    string sql = "select i.kino,k.kiname,s.emname,s.cuname1,LendD.*,單據='借出',序號='' from LendD"
                        + " inner join lend as s on LendD.leno=s.leno COLLATE Chinese_Taiwan_Stroke_BIN"
                        + " left join item as i on LendD.itno=i.itno"
                        + " left join kind as k on i.kino=k.kino"
                        + " where '0'='0'"
                        + str;
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            if (LeDate.Text.Trim() != "") cmd.Parameters.AddWithValue("ledate", LeDate.Text.Trim());
                            if (LeDate1.Text.Trim() != "") cmd.Parameters.AddWithValue("ledate1", LeDate1.Text.Trim());
                            if (CuNo.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                            if (CuNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                            if (EmNo.Text.Trim() != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            if (EmNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                            if (StNo.Text.Trim() != "") cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                            if (StNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                            if (ItNo.Text.Trim() != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                            if (ItNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                            if (KiNo.Text.Trim() != "") cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                            if (KiNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());

                            cmd.CommandText = sql;
                            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                            {
                                dd.Fill(LendD);
                                for (int i = 0; i < LendD.Rows.Count; i++)
                                {
                                    b = LendD.Rows[i]["mny"].ToDecimal("f" + Common.TPF);
                                    lendmny += b;

                                    LendD.Rows[i]["mny"] = b;
                                    LendD.Rows[i]["ledate"] = Date.AddLine(LendD.Rows[i]["ledate"].ToString());
                                    LendD.Rows[i]["ledate1"] = Date.AddLine(LendD.Rows[i]["ledate1"].ToString());
                                    LendD.Rows[i]["序號"] = "a" + i.ToString();
                                }
                                if (dtD.Rows.Count == 0)
                                    dtD = LendD.Copy();
                                else
                                    dtD.Merge(LendD);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (ck2.Checked == true)
            {
                str = "";
                if (Common.User_DateTime == 1)
                {
                    if (LeDate.Text.Trim() != "")
                        str += " and  s.ledate >=@ledate";
                    if (LeDate1.Text.Trim() != "")
                        str += " and  s.ledate <=@ledate1";
                }
                else
                {
                    if (LeDate.Text.Trim() != "")
                        str += " and  s.ledate1 >=@ledate";
                    if (LeDate1.Text.Trim() != "")
                        str += " and  s.ledate1 <=@ledate1";
                }

                if (CuNo.Text.Trim() != "")
                    str += " and  s.cuno >=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                if (CuNo.Text.Trim() != "")
                    str += " and  s.cuno <=@cuno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                if (EmNo.Text.Trim() != "")
                    str += " and s.emno >=@emno COLLATE Chinese_Taiwan_Stroke_BIN";
                if (EmNo1.Text.Trim() != "")
                    str += " and s.emno <=@emno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                if (StNo.Text.Trim() != "")
                    str += " and RLendD.StNo >=@StNo COLLATE Chinese_Taiwan_Stroke_BIN";
                if (StNo1.Text.Trim() != "")
                    str += " and RLendD.StNo <=@StNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                if (ItNo.Text.Trim() != "")
                    str += " and RLendD.itno >=@itno COLLATE Chinese_Taiwan_Stroke_BIN";
                if (ItNo1.Text.Trim() != "")
                    str += " and RLendD.itno <=@itno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                if (KiNo.Text.Trim() != "")
                    str += " and i.KiNo >=@KiNo COLLATE Chinese_Taiwan_Stroke_BIN";
                if (KiNo1.Text.Trim() != "")
                    str += " and i.KiNo <=@KiNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                try
                {
                    string sql = "select i.kino,k.kiname,s.emname,s.cuname1,RLendD.*,單據='還入',序號='' from RLendD"
                        + " inner join rlend as s on RLendD.leno=s.leno COLLATE Chinese_Taiwan_Stroke_BIN"
                        + " left join item as i on RLendD.itno=i.itno"
                        + " left join kind as k on i.kino=k.kino"
                        + " where '0'='0'"
                        + str;
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            if (LeDate.Text.Trim() != "") cmd.Parameters.AddWithValue("ledate", LeDate.Text.Trim());
                            if (LeDate1.Text.Trim() != "") cmd.Parameters.AddWithValue("ledate1", LeDate1.Text.Trim());
                            if (CuNo.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                            if (CuNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                            if (EmNo.Text.Trim() != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            if (EmNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                            if (StNo.Text.Trim() != "") cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                            if (StNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                            if (ItNo.Text.Trim() != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                            if (ItNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                            if (KiNo.Text.Trim() != "") cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                            if (KiNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());

                            cmd.CommandText = sql;
                            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                            {
                                dd.Fill(RLendD);
                                for (int i = 0; i < RLendD.Rows.Count; i++)
                                {
                                    b = 0;
                                    decimal.TryParse(RLendD.Rows[i]["qty"].ToString(), out b);
                                    RLendD.Rows[i]["qty"] = (b * (-1)).ToDecimal("f" + Common.Q);

                                    b = 0;
                                    decimal.TryParse(RLendD.Rows[i]["mnyb"].ToString(), out b);
                                    RLendD.Rows[i]["mnyb"] = (b * (-1)).ToDecimal("f" + Common.M);

                                    b = 0;
                                    decimal.TryParse(RLendD.Rows[i]["mny"].ToString(), out b);
                                    RLendD.Rows[i]["mny"] = (b * (-1)).ToDecimal("f" + Common.TPF);
                                    rlendmny += b;

                                    RLendD.Rows[i]["ledate"] = Date.AddLine(RLendD.Rows[i]["ledate"].ToString());
                                    RLendD.Rows[i]["ledate1"] = Date.AddLine(RLendD.Rows[i]["ledate1"].ToString());
                                    RLendD.Rows[i]["序號"] = "b" + i.ToString();

                                }
                            }
                            if (dtD.Rows.Count == 0)
                                dtD = RLendD.Copy();
                            else
                                dtD.Merge(RLendD);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (dtD.Rows.Count == 0)
            {
                MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //using (FrmLendNew_Infob frm = new FrmLendNew_Infob())
                //{
                //    frm.dtD = dtD;
                //    frm.LendCount = LendD.Rows.Count;
                //    frm.RLendCount = RLendD.Rows.Count;
                //    frm.LendMny = lendmny;
                //    frm.RLendMny = rlendmny;
                //    string date = "日期區間:";
                //    date += Date.AddLine(LeDate.Text.ToString()) + "～" + Date.AddLine(LeDate1.Text.ToString());
                //    frm.DateRange = date;
                //    frm.ShowDialog();
                //}
                this.OpemInfoFrom<FrmLendNew_Infob>(() =>
                            {
                                FrmLendNew_Infob frm = new FrmLendNew_Infob();
                                frm.dtD = dtD;
                                frm.LendCount = LendD.Rows.Count;
                                frm.RLendCount = RLendD.Rows.Count;
                                frm.LendMny = lendmny;
                                frm.RLendMny = rlendmny;
                                string date = "日期區間:";
                                date += Date.AddLine(LeDate.Text.ToString()) + "～" + Date.AddLine(LeDate1.Text.ToString());
                                frm.DateRange = date;
                                return frm;
                            });
            }
            //}

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void LeDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tx = sender as TextBox;
            if (!tx.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Common.User_DateTime == 1)
                {
                    LeDate.Text = Date.GetDateTime(1, false);
                    LeDate1.Text = Date.GetDateTime(1, false);
                    LeDate.Text = LeDate.Text.Remove(5) + "01";
                }
                else
                {
                    LeDate.Text = Date.GetDateTime(2, false);
                    LeDate1.Text = Date.GetDateTime(2, false);
                    LeDate.Text = LeDate.Text.Remove(6) + "01";
                }
            }
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            jLend.Open<JBS.JS.Cust>(sender);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jLend.Open<JBS.JS.Empl>(sender);
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            jLend.Open<JBS.JS.Stkroom>(sender);
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            jLend.Open<JBS.JS.Item>(sender);
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            jLend.Open<JBS.JS.Kind>(sender);
        }

        private void FrmLendNew_Info_Load(object sender, EventArgs e)
        {

        }

        private void btnTotal_Click(object sender, EventArgs e)
        {
            dt.Clear();
            dtD.Clear();
            Lend.Clear();
            LendD.Clear();
            RLend.Clear();
            RLendD.Clear();

            if (Compare(LeDate, LeDate1)) return;
            if (Compare(CuNo, CuNo1)) return;
            if (Compare(EmNo, EmNo1)) return;
            if (Compare(StNo, StNo1)) return;
            if (Compare(ItNo, ItNo1)) return;
            if (Compare(KiNo, KiNo1)) return;

            if (ck1.Checked == false && ck2.Checked == false)
            {
                MessageBox.Show(
                    "找不到任何資料，請重新輸入!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            //明細表-借出
            var str = "";
            var str1 = "";

            if (ck1.Checked == true)
            {
                if (LeDate.Text.Trim() != "")
                    str += " and  LendD.ledate >=@day";
                if (LeDate1.Text.Trim() != "")
                    str += " and  LendD.ledate <=@day1";

                if (CuNo.Text.Trim() != "")
                    str += " and  LendD.cuno >=@cuno ";
                if (CuNo.Text.Trim() != "")
                    str += " and  LendD.cuno <=@cuno1 ";

                if (EmNo.Text.Trim() != "")
                    str += " and LendD.emno >=@emno ";
                if (EmNo1.Text.Trim() != "")
                    str += " and LendD.emno <=@emno1 ";

                if (StNo.Text.Trim() != "")
                    str += " and LendD.StNo >=@StNo ";
                if (StNo1.Text.Trim() != "")
                    str += " and LendD.StNo <=@StNo1 ";

                if (ItNo.Text.Trim() != "")
                    str += " and LendD.itno >=@itno ";
                if (ItNo1.Text.Trim() != "")
                    str += " and LendD.itno <=@itno1 ";

                if (KiNo.Text.Trim() != "")
                    str += " and i.KiNo >=@KiNo ";
                if (KiNo1.Text.Trim() != "")
                    str += " and i.KiNo <=@KiNo1 ";

                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("day", Date.ToTWDate(LeDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("day1", Date.ToTWDate(LeDate1.Text.Trim()));
                        cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());

                        cmd.CommandText = @"
                            select i.kino,k.kiname,s.emname,s.cuname1,LendD.*,單據='借出',序號='',期初數量=0.0
                            from LendD 
                            left join lend as s on LendD.leno=s.leno
                            left join item as i on LendD.itno=i.itno 
                            left join kind as k on i.kino=k.kino 
                            where '0'='0' " + str;

                        dd.Fill(LendD);
                        for (int i = 0; i < LendD.Rows.Count; i++)
                        {
                            LendD.Rows[i]["mny"] = LendD.Rows[i]["mny"].ToDecimal("f" + Common.TPF);
                            LendD.Rows[i]["ledate"] = Date.AddLine(LendD.Rows[i]["ledate"].ToString());
                            LendD.Rows[i]["ledate1"] = Date.AddLine(LendD.Rows[i]["ledate1"].ToString());
                            LendD.Rows[i]["序號"] = "c" + i.ToString();
                        }
                    }

                    if (dtD.Rows.Count == 0)
                        dtD = LendD.Clone();

                    dtD.Merge(LendD);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            //明細表-還入
            str = "";
            str1 = "";

            if (ck2.Checked == true)
            {
                if (LeDate.Text.Trim() != "")
                    str += " and  RLendD.ledate >=@day";
                if (LeDate1.Text.Trim() != "")
                    str += " and  RLendD.ledate <=@day1";

                if (CuNo.Text.Trim() != "")
                    str += " and  RLendD.cuno >=@cuno ";
                if (CuNo.Text.Trim() != "")
                    str += " and  RLendD.cuno <=@cuno1 ";

                if (EmNo.Text.Trim() != "")
                    str += " and RLendD.emno >=@emno ";
                if (EmNo1.Text.Trim() != "")
                    str += " and RLendD.emno <=@emno1 ";

                if (StNo.Text.Trim() != "")
                    str += " and RLendD.StNo >=@StNo ";
                if (StNo1.Text.Trim() != "")
                    str += " and RLendD.StNo <=@StNo1 ";

                if (ItNo.Text.Trim() != "")
                    str += " and RLendD.itno >=@itno ";
                if (ItNo1.Text.Trim() != "")
                    str += " and RLendD.itno <=@itno1 ";

                if (KiNo.Text.Trim() != "")
                    str += " and i.KiNo >=@KiNo ";
                if (KiNo1.Text.Trim() != "")
                    str += " and i.KiNo <=@KiNo1 ";

                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("day", Date.ToTWDate(LeDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("day1", Date.ToTWDate(LeDate1.Text.Trim()));
                        cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());

                        cmd.CommandText = @"
                            select i.kino,k.kiname,s.emname,s.cuname1,RLendD.*,單據='還入',序號='',期初數量=0.0 
                            from RLendD
                            left join rlend as s on RLendD.leno=s.leno
                            left join item as i on RLendD.itno=i.itno
                            left join kind as k on i.kino=k.kino
                            where '0'='0' " + str;

                        dd.Fill(RLendD);

                        for (int i = 0; i < RLendD.Rows.Count; i++)
                        {
                            RLendD.Rows[i]["qty"] = RLendD.Rows[i]["qty"].ToDecimal("f" + Common.Q) * -1;
                            RLendD.Rows[i]["mnyb"] = RLendD.Rows[i]["mnyb"].ToDecimal("f" + Common.M) * -1;
                            RLendD.Rows[i]["mny"] = RLendD.Rows[i]["mny"].ToDecimal("f" + Common.TPF) * -1;

                            RLendD.Rows[i]["ledate"] = Date.AddLine(RLendD.Rows[i]["ledate"].ToString());
                            RLendD.Rows[i]["ledate1"] = Date.AddLine(RLendD.Rows[i]["ledate1"].ToString());
                            RLendD.Rows[i]["序號"] = "d" + i.ToString();

                            if (RLendD.Rows[i]["isFromSale"].ToString().Trim() == "賣出")
                                RLendD.Rows[i]["單據"] = "已賣";
                        }
                    }

                    if (dtD.Rows.Count == 0)
                        dtD = RLendD.Clone();

                    dtD.Merge(RLendD);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            //總表-期初
            str = "";
            str1 = "";

            if (LeDate.Text.Trim() != "") str += " and  LendD.ledate <@day";
            if (LeDate1.Text.Trim() != "") str += " and  LendD.ledate <@day1";

            if (CuNo.Text.Trim() != "") str += " and  LendD.cuno >=@cuno ";
            if (CuNo.Text.Trim() != "") str += " and  LendD.cuno <=@cuno1 ";

            if (EmNo.Text.Trim() != "") str += " and LendD.emno >=@emno ";
            if (EmNo1.Text.Trim() != "") str += " and LendD.emno <=@emno1 ";

            if (StNo.Text.Trim() != "") str += " and LendD.StNo >=@StNo ";
            if (StNo1.Text.Trim() != "") str += " and LendD.StNo <=@StNo1 ";

            if (ItNo.Text.Trim() != "") str += " and LendD.itno >=@itno ";
            if (ItNo1.Text.Trim() != "") str += " and LendD.itno <=@itno1 ";

            if (KiNo.Text.Trim() != "") str += " and i.KiNo >=@KiNo ";
            if (KiNo1.Text.Trim() != "") str += " and i.KiNo <=@KiNo1 ";
            //
            //
            if (LeDate.Text.Trim() != "") str1 += " and  rLendD.ledate <@day";
            if (LeDate1.Text.Trim() != "") str1 += " and  rLendD.ledate <@day1";

            if (CuNo.Text.Trim() != "") str1 += " and  rLendD.cuno >=@cuno ";
            if (CuNo.Text.Trim() != "") str1 += " and  rLendD.cuno <=@cuno1 ";

            if (EmNo.Text.Trim() != "") str1 += " and rLendD.emno >=@emno ";
            if (EmNo1.Text.Trim() != "") str1 += " and rLendD.emno <=@emno1 ";

            if (StNo.Text.Trim() != "") str1 += " and rLendD.StNo >=@StNo ";
            if (StNo1.Text.Trim() != "") str1 += " and rLendD.StNo <=@StNo1 ";

            if (ItNo.Text.Trim() != "") str1 += " and rLendD.itno >=@itno ";
            if (ItNo1.Text.Trim() != "") str1 += " and rLendD.itno <=@itno1 ";

            if (KiNo.Text.Trim() != "") str1 += " and i.KiNo >=@KiNo ";
            if (KiNo1.Text.Trim() != "") str1 += " and i.KiNo <=@KiNo1 ";

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(LeDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(LeDate1.Text.Trim()));
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                    cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                    cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                    cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                    cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());

                    cmd.CommandText = @"
                        select C.cuno,C.itno,SUM(期初數量)期初數量,leqty=0.0,rleqty=0.0
                        from 
                        (
	                        select lendd.cuno,lendd.itno,(lendd.qty*lendd.itpkgqty)期初數量 
	                        from lendd 
	                        left join item as i on LendD.itno=i.itno 
	                        left join kind as k on i.kino=k.kino 
	                        where '0'='0' " + str + @"
	                        union all 
	                        select rLendD.cuno,rLendD.itno,(rLendD.qty*rLendD.itpkgqty*-1)期初數量 
	                        from rlendd  
	                        left join item as i on rLendD.itno=i.itno 
	                        left join kind as k on i.kino=k.kino 
	                        where '0'='0' " + str1 + @"
                        )C 
                        group by C.cuno,C.itno ";

                    dd.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //總表-借出
            str = "";
            str1 = "";

            if (ck1.Checked)
            {
                if (LeDate.Text.Trim() != "")
                    str += " and  LendD.ledate >=@day";
                if (LeDate1.Text.Trim() != "")
                    str += " and  LendD.ledate <=@day1";

                if (CuNo.Text.Trim() != "")
                    str += " and  LendD.cuno >=@cuno ";
                if (CuNo.Text.Trim() != "")
                    str += " and  LendD.cuno <=@cuno1 ";

                if (EmNo.Text.Trim() != "")
                    str += " and LendD.emno >=@emno ";
                if (EmNo1.Text.Trim() != "")
                    str += " and LendD.emno <=@emno1 ";

                if (StNo.Text.Trim() != "")
                    str += " and LendD.StNo >=@StNo ";
                if (StNo1.Text.Trim() != "")
                    str += " and LendD.StNo <=@StNo1 ";

                if (ItNo.Text.Trim() != "")
                    str += " and LendD.itno >=@itno ";
                if (ItNo1.Text.Trim() != "")
                    str += " and LendD.itno <=@itno1 ";

                if (KiNo.Text.Trim() != "")
                    str += " and i.KiNo >=@KiNo ";
                if (KiNo1.Text.Trim() != "")
                    str += " and i.KiNo <=@KiNo1 ";

                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("day", Date.ToTWDate(LeDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("day1", Date.ToTWDate(LeDate1.Text.Trim()));
                        cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());

                        cmd.CommandText = @"
                            select lendd.cuno,lendd.itno,期初數量=0.0,SUM(lendd.qty*lendd.itpkgqty)leqty,rleqty=0.0
                            from lendd 
                            left join item as i on lendd.itno=i.itno
                            left join kind as k on i.kino=k.kino
                            where '0'='0' " + str + @"
                            group by lendd.cuno,lendd.itno";

                        dd.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            //總表-還入
            str = "";
            str1 = "";

            if (ck2.Checked == true)
            {
                if (LeDate.Text.Trim() != "")
                    str += " and  RLendD.ledate >=@day";
                if (LeDate1.Text.Trim() != "")
                    str += " and  RLendD.ledate <=@day1";

                if (CuNo.Text.Trim() != "")
                    str += " and  RLendD.cuno >=@cuno ";
                if (CuNo.Text.Trim() != "")
                    str += " and  RLendD.cuno <=@cuno1 ";

                if (EmNo.Text.Trim() != "")
                    str += " and RLendD.emno >=@emno ";
                if (EmNo1.Text.Trim() != "")
                    str += " and RLendD.emno <=@emno1 ";

                if (StNo.Text.Trim() != "")
                    str += " and RLendD.StNo >=@StNo ";
                if (StNo1.Text.Trim() != "")
                    str += " and RLendD.StNo <=@StNo1 ";

                if (ItNo.Text.Trim() != "")
                    str += " and RLendD.itno >=@itno ";
                if (ItNo1.Text.Trim() != "")
                    str += " and RLendD.itno <=@itno1 ";

                if (KiNo.Text.Trim() != "")
                    str += " and i.KiNo >=@KiNo ";
                if (KiNo1.Text.Trim() != "")
                    str += " and i.KiNo <=@KiNo1 ";

                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("day", Date.ToTWDate(LeDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("day1", Date.ToTWDate(LeDate1.Text.Trim()));
                        cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());

                        cmd.CommandText = @"
                            select rlendd.cuno,rlendd.itno,期初數量=0.0,leqty=0.0,SUM(rlendd.qty*rlendd.itpkgqty*-1)rleqty
                            from rlendd 
                            left join item as i on rlendd.itno=i.itno
                            left join kind as k on i.kino=k.kino
                            where '0'='0' " + str + @"
                            group by rlendd.cuno,rlendd.itno";

                        dd.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }


            if (dtD.Rows.Count == 0)
            {
                MessageBox.Show(
                    "找不到任何資料，請重新輸入!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (dt.Columns.Contains("前期加本期") == false) dt.Columns.Add("前期加本期", typeof(Decimal));
                if (dt.Columns.Contains("itname") == false) dt.Columns.Add("itname", typeof(String));
                if (dt.Columns.Contains("itunit") == false) dt.Columns.Add("itunit", typeof(String));
                if (dt.Columns.Contains("cuname1") == false) dt.Columns.Add("cuname1", typeof(String));
                if (dt.Columns.Contains("qty") == false) dt.Columns.Add("qty", typeof(Decimal));

                List<CI> list = new List<CI>();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("cuno", "");
                    cmd.Parameters.AddWithValue("itno", "");
                    cmd.CommandText = "select top 1 itno,itname,itunit,cuno,cuname1 from item,cust where itno = @itno and cuno = @cuno ";
                    cn.Open();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var cuno = dt.Rows[i]["CuNo"].ToString().Trim();
                        var itno = dt.Rows[i]["ItNo"].ToString().Trim();

                        if (list.FindIndex(l => l.CuNo == cuno && l.ItNo == itno) == -1)
                        {
                            CI ci = new CI(cuno, itno);
                            var row = dtD.AsEnumerable().FirstOrDefault(r => r["cuno"].ToString().Trim() == cuno && r["itno"].ToString().Trim() == itno);
                            if (row != null)
                            {
                                ci.CuName1 = row["cuname1"].ToString().Trim();
                                ci.ItName = row["itname"].ToString();
                                ci.ItUnit = row["itunit"].ToString().Trim();
                            }
                            else
                            {
                                cmd.Parameters["cuno"].Value = cuno;
                                cmd.Parameters["itno"].Value = itno;
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        ci.CuName1 = reader["cuname1"].ToString().Trim();
                                        ci.ItName = reader["itname"].ToString();
                                        ci.ItUnit = reader["itunit"].ToString().Trim();
                                    }
                                }
                            }
                            list.Add(ci);
                        }
                    }
                }

                var tAll = dt.Clone();
                for (int i = 0; i < list.Count; i++)
                {
                    var ci = list.ElementAt(i);
                    DataRow row = tAll.NewRow();

                    var rows = dt.AsEnumerable().Where(r => r["cuno"].ToString().Trim() == ci.CuNo && r["itno"].ToString().Trim() == ci.ItNo);
                    var inqty = rows.Sum(r => r["期初數量"].ToDecimal());
                    var leqty = rows.Sum(r => r["leqty"].ToDecimal());
                    var rleqty = rows.Sum(r => r["rleqty"].ToDecimal());
                    var 前期加本期 = inqty + leqty + rleqty;

                    //宏恩借轉銷 前期/本期為零的話,不出現
                    if (Common.Sys_LendToSaleMode == 2 && inqty == 0 && 前期加本期 == 0)
                        continue;

                    row["itno"] = ci.ItNo.ToString().Trim();
                    row["itname"] = ci.ItName.ToString();
                    row["itunit"] = ci.ItUnit.ToString().Trim();
                    row["cuno"] = ci.CuNo.ToString().Trim();
                    row["cuname1"] = ci.CuName1.ToString().Trim();

                    row["期初數量"] = inqty.ToDecimal("f" + Common.Q);
                    row["leqty"] = leqty.ToDecimal("f" + Common.Q);
                    row["rleqty"] = rleqty.ToDecimal("f" + Common.Q);
                    row["qty"] = leqty.ToDecimal("f" + Common.Q) + rleqty.ToDecimal("f" + Common.Q);
                    row["前期加本期"] = 前期加本期.ToDecimal("f" + Common.Q);

                    tAll.Rows.Add(row);
                }

                if (tAll.Rows.Count == 0)
                {
                    MessageBox.Show("前期+本期的總結餘量為零!");
                    return;
                }

                if (tAll.AsEnumerable().Count(r => r["qty"].ToDecimal() != 0) == 0 && checkBoxT1_過濾總結數為0.Checked)
                {
                    MessageBox.Show("沒有總結餘量大於零的資料");
                    return;
                }
                //using (FrmLendNew_InfobT frm = new FrmLendNew_InfobT())
                //{
                //    if(checkBoxT1_過濾總結數為0.Checked)
                //        frm.dt = tAll.AsEnumerable().Where(r => r["QTY"].ToString().ToInteger() > 0).OrderBy(r => r["cuno"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();
                //    else
                //        frm.dt = tAll.AsEnumerable().OrderBy(r => r["cuno"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();
                //    frm.dtD = dtD.Copy();
                //    frm.DateRange = "日期區間:" + Date.AddLine(LeDate.Text.ToString()) + "～" + Date.AddLine(LeDate1.Text.ToString());

                //    frm.ShowDialog();
                //}
                this.OpemInfoFrom<FrmLendNew_InfobT>(() =>
                            {
                                FrmLendNew_InfobT frm = new FrmLendNew_InfobT();
                                if (checkBoxT1_過濾總結數為0.Checked)
                                    frm.dt = tAll.AsEnumerable().Where(r => r["QTY"].ToString().ToInteger() > 0).OrderBy(r => r["cuno"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();
                                else
                                    frm.dt = tAll.AsEnumerable().OrderBy(r => r["cuno"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();
                                frm.dtD = dtD.Copy();
                                frm.DateRange = "日期區間:" + Date.AddLine(LeDate.Text.ToString()) + "～" + Date.AddLine(LeDate1.Text.ToString());
                                return frm;
                            });
            }
        }
    }

    public class CI
    {
        public string CuNo { get; set; }
        public string CuName1 { get; set; }
        public string ItNo { get; set; }
        public string ItName { get; set; }
        public string ItUnit { get; set; }

        public CI(string co, string io)
        {
            CuNo = co;
            CuName1 = "";
            ItNo = io;
            ItName = "";
            ItUnit = "";
        }
    }
}
