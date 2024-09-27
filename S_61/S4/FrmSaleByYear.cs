using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S4
{
    public partial class FrmSaleByYear : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmSaleByYear()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            if (Common.User_DateTime == 1)
            {
                xYear.MaxLength = 3;
                gYear.MaxLength = 3;
                ymonth.MaxLength = 5;
                ymonth1.MaxLength = 5;
                emonth.MaxLength = 5;
                emonth1.MaxLength = 5;
                SaleYear.MaxLength = 3;
                xYear.Text = Date.GetDateTime(1).takeString(3);
                SaleYear.Text = Date.GetDateTime(1).takeString(3);
            }
            else
            {
                xYear.MaxLength = 4;
                gYear.MaxLength = 4;
                ymonth.MaxLength = 6;
                ymonth1.MaxLength = 6;
                emonth.MaxLength = 6;
                emonth1.MaxLength = 6;
                SaleYear.MaxLength = 4;
                xYear.Text = Date.GetDateTime(2).takeString(4);
                SaleYear.Text = Date.GetDateTime(2).takeString(4);
            }

            gYear.Text = xYear.Text;
            ymonth.Text = xYear.Text + "01";
            ymonth1.Text = xYear.Text + "12";
            emonth.Text = xYear.Text + "01";
            emonth1.Text = xYear.Text + "12";
        }

        private void FrmSaleByYear_Load(object sender, EventArgs e)
        {

        }

        private void tabControlT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlT1.SelectedTab.Equals(tabPage1))
                xYear.Focus();
            else if (tabControlT1.SelectedTab.Equals(tabPage2))
                gYear.Focus();
            else if (tabControlT1.SelectedTab.Equals(tabPage3))
                ymonth.Focus();
            else if (tabControlT1.SelectedTab.Equals(tabPage4))
                emonth.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void xX1No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX01>(sender);
        }
        private void xX1No_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if (xX1No.TrimTextLenth() > 0)
            {
                xe.ValidateOpen<JBS.JS.XX01>(sender, e, row =>
                {
                    xX1No.Text = row["X1No"].ToString();
                });
            }
            else xX1No.Clear();
        }
        private void xCuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }
        private void xItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }
        private void xItNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }
        private void xKiNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }
        private void xKiNo1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;

            if (xKiNo.TrimTextLenth() > 0)
            {
                xe.ValidateOpen<JBS.JS.Kind>(sender, e, row =>
                {
                    xKiNo.Text = row["KiNo"].ToString();
                });
            }
            else xKiNo.Clear();
        }

        private void gCuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }
        private void gCuNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void ymonth_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            using (TextBox tb = new TextBox())
            {
                tb.Text = ((TextBox)sender).Text + "01";
                if (tb.IsDateTime() == false)
                {
                    if (sender.Equals(ymonth))
                    {
                        e.Cancel = true;
                        MessageBox.Show(
                            "日期格式錯誤！",
                            "訊息視窗",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        ymonth.SelectAll();
                    }
                    else
                    {
                        e.Cancel = true;
                        MessageBox.Show(
                            "日期格式錯誤！",
                            "訊息視窗",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        ymonth.SelectAll();
                    }
                    return;
                }
                else
                {

                    string y = "";
                    string y1 = "";
                    if (Common.User_DateTime == 1)
                    {
                        y = Common.Sys_StkYear1 + "01";
                        y1 = (Common.Sys_StkYear1 + 1) + "12";
                    }
                    else
                    {
                        y = Common.Sys_StkYear2 + "01";
                        y1 = (Common.Sys_StkYear2 + 1) + "12";
                    }

                    if (sender.Equals(ymonth) && y.BigThen(tb.Text))
                    {
                        e.Cancel = true;
                        MessageBox.Show("超出庫存年度!");
                        ymonth.SelectAll();
                        return;
                    }

                    if (sender.Equals(ymonth1) && tb.Text.BigThen(y1 + "31"))
                    {
                        e.Cancel = true;
                        MessageBox.Show("超出庫存年度!");
                        ymonth1.SelectAll();
                        return;
                    }
                }
            }

            var day = ymonth.Text;
            var day1 = ymonth1.Text;

            if (day.BigThen(day1))
            {
                e.Cancel = true;
                MessageBox.Show(
                    "起始日期大於終止日期",
                    "訊息視窗",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
                ymonth.SelectAll();
                return;
            }
        }

        private void emonth_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            using (TextBox tb = new TextBox())
            {
                tb.Text = ((TextBox)sender).Text + "01";
                if (tb.IsDateTime() == false)
                {
                    if (sender.Equals(emonth))
                    {
                        e.Cancel = true;
                        MessageBox.Show(
                            "日期格式錯誤！",
                            "訊息視窗",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        emonth.SelectAll();
                    }
                    else
                    {
                        e.Cancel = true;
                        MessageBox.Show(
                            "日期格式錯誤！",
                            "訊息視窗",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        emonth.SelectAll();
                    }
                    return;
                }
                else
                {
                    var y = Common.Sys_StkYear1 + "01";
                    var y1 = (Common.Sys_StkYear1 + 1) + "12";

                    if (sender.Equals(emonth) && y.BigThen(tb.Text))
                    {
                        e.Cancel = true;
                        MessageBox.Show("超出庫存年度!");
                        emonth.SelectAll();
                        return;
                    }

                    if (sender.Equals(emonth1) && tb.Text.BigThen(y1 + "31"))
                    {
                        e.Cancel = true;
                        MessageBox.Show("超出庫存年度!");
                        emonth1.SelectAll();
                        return;
                    }
                }
            }

            var day = emonth.Text;
            var day1 = emonth1.Text;

            if (day.BigThen(day1))
            {
                e.Cancel = true;
                MessageBox.Show(
                    "起始日期大於終止日期",
                    "訊息視窗",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
                emonth.SelectAll();
                return;
            }

            if (radioT6.Checked)
            {
                if (radioT5.Focused)
                    return;

                var len = Common.User_DateTime == 1 ? 3 : 4;
                var year = day.takeString(len);
                var year1 = day1.takeString(len);

                if (year == year1)
                    return;

                e.Cancel = true;
                MessageBox.Show("查詢匯總表, 起始年月請勿跨年度查詢!");
                return;
            }
        }
        private void eEmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }
        private void eEmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                (sender as TextBoxT).Text = row["emno"].ToString().Trim();
            }, true);
        }

        private void xNext_Click(object sender, EventArgs e)
        {
            if (tabControlT1.SelectedTab.Equals(tabPage1))
            {
                if (xYear.TrimTextLenth() == 0)
                {
                    MessageBox.Show("請輸入查詢年度！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    xYear.Focus();
                    return;
                }
                if (xYear.Text.ToDecimal() == 0)
                {
                    MessageBox.Show("請輸入正確年度！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    xYear.Focus();
                    return;
                }
                DataTable temp = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    var xxx = "";
                    var yyy = "";
                    cmd.Parameters.AddWithValue("@year", xYear.Text);
                    if (xX1No.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@X1No", xX1No.Text.Trim());
                        xxx = " And CuX1No=(@X1No) ";
                    }
                    if (xKiNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@KiNo", xKiNo.Text.Trim());
                        yyy += " And KiNo=(@KiNo)";
                    }
                    if (xCuNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("CuNo", xCuNo.Text.Trim());
                        xxx += " And cust.CuNo >=(@CuNo) ";
                    }
                    if (xCuNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("CuNo1", xCuNo1.Text.Trim());
                        xxx += " And cust.CuNo <=(@CuNo1) ";
                    }
                    if (xItNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ItNo", xItNo.Text.Trim());
                        xxx += " And ItNo >=(@ItNo) ";
                        yyy += " And ItNo >=(@ItNo) ";
                    }
                    if (xItNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@eItNo", xItNo1.Text.Trim());
                        xxx += " And ItNo <=(@eItNo) ";
                        yyy += " And ItNo <=(@eItNo) ";
                    }
                    cmd.CommandText = @"
                    Select Good.*,itname,ISNULL(kiname,'未歸類')kiname 
                    from 
                    (
                        Select A.*
                        ,ISNULL(m01,0)m01,ISNULL(m02,0)m02,ISNULL(m03,0)m03,ISNULL(m04,0)m04,ISNULL(m05,0)m05,ISNULL(m06,0)m06,ISNULL(m07,0)m07,ISNULL(m08,0)m08,ISNULL(m09,0)m09,ISNULL(m10,0)m10,ISNULL(m11,0)m11,ISNULL(m12,0)m12,ISNULL(SA,0)SA,ISNULL(SB,0)SB,ISNULL(ST,0)ST 
                        ,ISNULL(mny01,0)mny01,ISNULL(mny02,0)mny02,ISNULL(mny03,0)mny03,ISNULL(mny04,0)mny04,ISNULL(mny05,0)mny05,ISNULL(mny06,0)mny06,ISNULL(mny07,0)mny07,ISNULL(mny08,0)mny08,ISNULL(mny09,0)mny09,ISNULL(mny10,0)mny10,ISNULL(mny11,0)mny11,ISNULL(mny12,0)mny12,ISNULL(TA,0)TA,ISNULL(TB,0)TB,ISNULL(TT,0)TT 
                        from 
                        (
                            select ISNULL(case when LEN(kino)=0 then '未歸類' else kino end,'未歸類')kino,itno 
                            from item
                            where 0=0 " + yyy + @"
                        )A
                        left join (select itno,sum(m01)m01 from (select itno,sum(qty*itpkgqty)m01 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'01%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m01 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'01%' " + xxx + @" group by itno)WW group by itno
                                                                )B on A.itno=B.itno 
                        left join (select itno,sum(m02)m02 from (select itno,sum(qty*itpkgqty)m02 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'02%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m02 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'02%' " + xxx + @" group by itno)WW group by itno
                                                                )C on A.itno=C.itno  
                        left join (select itno,sum(m03)m03 from (select itno,sum(qty*itpkgqty)m03 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'03%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m03 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'03%' " + xxx + @" group by itno)WW group by itno
                                                                )D on A.itno=D.itno 
                        left join (select itno,sum(m04)m04 from (select itno,sum(qty*itpkgqty)m04 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'04%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m04 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'04%' " + xxx + @" group by itno)WW group by itno
                                                                )E on A.itno=E.itno 
                        left join (select itno,sum(m05)m05 from (select itno,sum(qty*itpkgqty)m05 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'05%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m05 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'05%' " + xxx + @" group by itno)WW group by itno
                                                                )F on A.itno=F.itno  
                        left join (select itno,sum(m06)m06 from (select itno,sum(qty*itpkgqty)m06 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'06%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m06 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'06%' " + xxx + @" group by itno)WW group by itno
                                                                )G on A.itno=G.itno  
                        left join (select itno,sum(m07)m07 from (select itno,sum(qty*itpkgqty)m07 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'07%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m07 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'07%' " + xxx + @" group by itno)WW group by itno
                                                                )H on A.itno=H.itno  
                        left join (select itno,sum(m08)m08 from (select itno,sum(qty*itpkgqty)m08 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'08%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m08 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'08%' " + xxx + @" group by itno)WW group by itno
                                                                )I on A.itno=I.itno  
                        left join (select itno,sum(m09)m09 from (select itno,sum(qty*itpkgqty)m09 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'09%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m09 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'09%' " + xxx + @" group by itno)WW group by itno
                                                                )J on A.itno=J.itno  
                        left join (select itno,sum(m10)m10 from (select itno,sum(qty*itpkgqty)m10 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'10%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m10 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'10%' " + xxx + @" group by itno)WW group by itno
                                                                )K on A.itno=K.itno  
                        left join (select itno,sum(m11)m11 from (select itno,sum(qty*itpkgqty)m11 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'11%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m11 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'11%' " + xxx + @" group by itno)WW group by itno
                                                                )L on A.itno=L.itno  
                        left join (select itno,sum(m12)m12 from (select itno,sum(qty*itpkgqty)m12 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'12%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)m12 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'12%' " + xxx + @" group by itno)WW group by itno
                                                                )M on A.itno=M.itno  
                        left join (select itno,sum(SA)SA from (select itno,sum(qty*itpkgqty)SA  from saled left join cust on saled.cuno=cust.cuno where sadate >=  (@year)+'0101' And sadate  < (@year)+'0701' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)SA  from rsaled left join cust on rsaled.cuno=cust.cuno where sadate >=  (@year)+'0101' And sadate  < (@year)+'0701' " + xxx + @" group by itno)WW group by itno
                                                                )N on A.itno=N.itno  
                        left join (select itno,sum(SB)SB from (select itno,sum(qty*itpkgqty)SB  from saled left join cust on saled.cuno=cust.cuno where sadate >=  (@year)+'0701' And sadate <= (@year)+'1231' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)SB  from rsaled left join cust on rsaled.cuno=cust.cuno where sadate >=  (@year)+'0701' And sadate <= (@year)+'1231' " + xxx + @" group by itno)WW group by itno
                                                                )O on A.itno=O.itno  
                        left join (select itno,sum(ST)ST from (select itno,sum(qty*itpkgqty)ST  from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'%'   " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(qty*itpkgqty)ST  from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'%'   " + xxx + @" group by itno)WW group by itno
                                                                )P on A.itno=P.itno  

                        left join (select itno,sum(mny01)mny01 from (select itno,sum(mnyb)mny01 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'01%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny01 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'01%' " + xxx + @" group by itno)WW group by itno
                                                                )BB on A.itno=BB.itno
                        left join (select itno,sum(mny02)mny02 from (select itno,sum(mnyb)mny02 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'02%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny02 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'02%' " + xxx + @" group by itno)WW group by itno
                                                                )BC on A.itno=BC.itno
                        left join (select itno,sum(mny03)mny03 from (select itno,sum(mnyb)mny03 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'03%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny03 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'03%' " + xxx + @" group by itno)WW group by itno
                                                                )BD on A.itno=BD.itno
                        left join (select itno,sum(mny04)mny04 from (select itno,sum(mnyb)mny04 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'04%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny04 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'04%' " + xxx + @" group by itno)WW group by itno
                                                                )BE on A.itno=BE.itno
                        left join (select itno,sum(mny05)mny05 from (select itno,sum(mnyb)mny05 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'05%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny05 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'05%' " + xxx + @" group by itno)WW group by itno
                                                                )BF on A.itno=BF.itno
                        left join (select itno,sum(mny06)mny06 from (select itno,sum(mnyb)mny06 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'06%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny06 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'06%' " + xxx + @" group by itno)WW group by itno
                                                                )BG on A.itno=BG.itno
                        left join (select itno,sum(mny07)mny07 from (select itno,sum(mnyb)mny07 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'07%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny07 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'07%' " + xxx + @" group by itno)WW group by itno
                                                                )BH on A.itno=BH.itno
                        left join (select itno,sum(mny08)mny08 from (select itno,sum(mnyb)mny08 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'08%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny08 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'08%' " + xxx + @" group by itno)WW group by itno
                                                                )BI on A.itno=BI.itno
                        left join (select itno,sum(mny09)mny09 from (select itno,sum(mnyb)mny09 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'09%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny09 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'09%' " + xxx + @" group by itno)WW group by itno
                                                                )BJ on A.itno=BJ.itno
                        left join (select itno,sum(mny10)mny10 from (select itno,sum(mnyb)mny10 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'10%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny10 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'10%' " + xxx + @" group by itno)WW group by itno
                                                                )BK on A.itno=BK.itno
                        left join (select itno,sum(mny11)mny11 from (select itno,sum(mnyb)mny11 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'11%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny11 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'11%' " + xxx + @" group by itno)WW group by itno
                                                                )BL on A.itno=BL.itno
                        left join (select itno,sum(mny12)mny12 from (select itno,sum(mnyb)mny12 from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'12%' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)mny12 from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'12%' " + xxx + @" group by itno)WW group by itno
                                                                )BM on A.itno=BM.itno
                        left join (select itno,sum(TA)TA from (select itno,sum(mnyb)TA  from saled left join cust on saled.cuno=cust.cuno where sadate >=  (@year)+'0101' And sadate  < (@year)+'0701' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)TA  from rsaled left join cust on rsaled.cuno=cust.cuno where sadate >=  (@year)+'0101' And sadate  < (@year)+'0701' " + xxx + @" group by itno)WW group by itno
                                                                )BN on A.itno=BN.itno
                        left join (select itno,sum(TB)TB from (select itno,sum(mnyb)TB  from saled left join cust on saled.cuno=cust.cuno where sadate >=  (@year)+'0701' And sadate <= (@year)+'1231' " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)TB  from rsaled left join cust on rsaled.cuno=cust.cuno where sadate >=  (@year)+'0701' And sadate <= (@year)+'1231' " + xxx + @" group by itno)WW group by itno
                                                                )BO on A.itno=BO.itno
                        left join (select itno,sum(TT)TT from (select itno,sum(mnyb)TT  from saled left join cust on saled.cuno=cust.cuno where sadate like (@year)+'%'   " + xxx + @" group by itno
                                                                union all
                                                                select itno,(-1)*sum(mnyb)TT  from rsaled left join cust on rsaled.cuno=cust.cuno where sadate like (@year)+'%'   " + xxx + @" group by itno)WW group by itno
                                                                )BP on A.itno=BP.itno
                    )Good
                    left join item on Good.itno=item.itno
                    left join kind on Good.kino=kind.kino
                    order by Good.kino,Good.itno";
                    da.Fill(temp);
                }

                if (temp.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (var frm = new FrmSaleTarget())
                {
                    if (xCuNo.TrimTextLenth() > 0 && xCuNo1.TrimTextLenth() > 0 && xCuNo.Text == xCuNo1.Text)
                    {
                        xe.Validate<JBS.JS.Cust>(xCuNo.Text, row =>
                        {
                            frm.TCNo = xCuNo.Text.Trim();
                            frm.TCName = row["cuname1"].ToString().Trim();
                        });
                    }

                    frm.year = xYear.Text.Trim();
                    if (xX1No.TrimTextLenth() > 0) frm.X1No = xX1No.Text.Trim();
                    if (radioT3.Checked)
                    {
                        temp.DefaultView.RowFilter = "ST <> 0";
                    }
                    frm.dtD = temp.DefaultView.ToTable();
                    frm.ShowDialog();

                    temp.Clear();
                }
            }
        }
        private void gNext_Click(object sender, EventArgs e)
        {
            if (tabControlT1.SelectedTab.Equals(tabPage2) == false)
                return;

            if (gYear.TrimTextLenth() == 0)
            {
                MessageBox.Show("請輸入查詢年度！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                xYear.Focus();
                return;
            }
            if (gYear.Text.ToDecimal() == 0)
            {
                MessageBox.Show("請輸入正確年度！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                xYear.Focus();
                return;
            }

            DataTable temp = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                var yyy = "";
                cmd.Parameters.AddWithValue("@year", xYear.Text);
                if (gCuNo.TrimTextLenth() > 0)
                {
                    cmd.Parameters.AddWithValue("@CuNo", gCuNo.Text.Trim());
                    yyy += " And CuNo >=(@CuNo) ";
                }
                if (gCuNo1.TrimTextLenth() > 0)
                {
                    cmd.Parameters.AddWithValue("@tCuNo1", gCuNo1.Text.Trim());
                    yyy += " And CuNo <=(@tCuNo1) ";
                }

                cmd.CommandText = @"
                Select A.*
                ,ISNULL(mny01,0)mny01,ISNULL(mny02,0)mny02,ISNULL(mny03,0)mny03,ISNULL(mny04,0)mny04,ISNULL(mny05,0)mny05,ISNULL(mny06,0)mny06,ISNULL(mny07,0)mny07,ISNULL(mny08,0)mny08,ISNULL(mny09,0)mny09,ISNULL(mny10,0)mny10,ISNULL(mny11,0)mny11,ISNULL(mny12,0)mny12,ISNULL(TA,0)TA,ISNULL(TB,0)TB,ISNULL(TT,0)TT 
                from 
                (
                    select cuno,cuname1,cuname2
                    from cust
                    where 0=0 " + yyy + @"
                )A
                left join (select cuno,sum(mny01)mny01 from (select cuno,sum(mnyb)mny01 from saled where sadate like (@year)+'01%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny01 from rsaled where sadate like (@year)+'01%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BB on A.cuno=BB.cuno
                left join (select cuno,sum(mny02)mny02 from (select cuno,sum(mnyb)mny02 from saled where sadate like (@year)+'02%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny02 from rsaled where sadate like (@year)+'02%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BC on A.cuno=BC.cuno
                left join (select cuno,sum(mny03)mny03 from (select cuno,sum(mnyb)mny03 from saled where sadate like (@year)+'03%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny03 from rsaled where sadate like (@year)+'03%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BD on A.cuno=BD.cuno
                left join (select cuno,sum(mny04)mny04 from (select cuno,sum(mnyb)mny04 from saled where sadate like (@year)+'04%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny04 from rsaled where sadate like (@year)+'04%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BE on A.cuno=BE.cuno
                left join (select cuno,sum(mny05)mny05 from (select cuno,sum(mnyb)mny05 from saled where sadate like (@year)+'05%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny05 from rsaled where sadate like (@year)+'05%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BF on A.cuno=BF.cuno
                left join (select cuno,sum(mny06)mny06 from (select cuno,sum(mnyb)mny06 from saled where sadate like (@year)+'06%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny06 from rsaled where sadate like (@year)+'06%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BG on A.cuno=BG.cuno
                left join (select cuno,sum(mny07)mny07 from (select cuno,sum(mnyb)mny07 from saled where sadate like (@year)+'07%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny07 from rsaled where sadate like (@year)+'07%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BH on A.cuno=BH.cuno
                left join (select cuno,sum(mny08)mny08 from (select cuno,sum(mnyb)mny08 from saled where sadate like (@year)+'08%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny08 from rsaled where sadate like (@year)+'08%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BI on A.cuno=BI.cuno
                left join (select cuno,sum(mny09)mny09 from (select cuno,sum(mnyb)mny09 from saled where sadate like (@year)+'09%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny09 from rsaled where sadate like (@year)+'09%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BJ on A.cuno=BJ.cuno
                left join (select cuno,sum(mny10)mny10 from (select cuno,sum(mnyb)mny10 from saled where sadate like (@year)+'10%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny10 from rsaled where sadate like (@year)+'10%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BK on A.cuno=BK.cuno
                left join (select cuno,sum(mny11)mny11 from (select cuno,sum(mnyb)mny11 from saled where sadate like (@year)+'11%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny11 from rsaled where sadate like (@year)+'11%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BL on A.cuno=BL.cuno
                left join (select cuno,sum(mny12)mny12 from (select cuno,sum(mnyb)mny12 from saled where sadate like (@year)+'12%' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)mny12 from rsaled where sadate like (@year)+'12%' " + yyy + @" group by cuno)WW group by cuno
                                                        )BM on A.cuno=BM.cuno
                left join (select cuno,sum(TA)TA from (select cuno,sum(mnyb)TA  from saled where sadate >=  (@year)+'0101' And sadate  < (@year)+'0701' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)TA  from rsaled where sadate >=  (@year)+'0101' And sadate  < (@year)+'0701' " + yyy + @" group by cuno)WW group by cuno
                                                        )BN on A.cuno=BN.cuno
                left join (select cuno,sum(TB)TB from (select cuno,sum(mnyb)TB  from saled where sadate >=  (@year)+'0701' And sadate <= (@year)+'1231' " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)TB  from rsaled where sadate >=  (@year)+'0701' And sadate <= (@year)+'1231' " + yyy + @" group by cuno)WW group by cuno
                                                        )BO on A.cuno=BO.cuno
                left join (select cuno,sum(TT)TT from (select cuno,sum(mnyb)TT  from saled where sadate like (@year)+'%'   " + yyy + @" group by cuno
                                                        union all
                                                        select cuno,(-1)*sum(mnyb)TT  from rsaled where sadate like (@year)+'%'   " + yyy + @" group by cuno)WW group by cuno
                                                        )BP on A.cuno=BP.cuno
                order by A.cuno ";
                da.Fill(temp);
            }

            if (temp.Rows.Count == 0)
            {
                MessageBox.Show(
                    "查無資料！",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new FrmSaleGroupCust())
            {
                frm.year = xYear.Text.Trim();
                if (radioT1.Checked)
                {
                    temp.DefaultView.RowFilter = "TT <> 0";
                }
                frm.dtD = temp.DefaultView.ToTable();
                frm.ShowDialog();

                temp.Clear();
            }
        }
        private void yNext_Click(object sender, EventArgs e)
        {
            if (tabControlT1.SelectedTab.Equals(tabPage3) == false)
                return;

            if (ymonth.TrimTextLenth() == 0)
            {
                MessageBox.Show("請輸入查詢年月！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ymonth.Focus();
                return;
            }
            if (ymonth1.TrimTextLenth() == 0)
            {
                MessageBox.Show("請輸入查詢年月！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ymonth1.Focus();
                return;
            }

            if (yCost.Checked)
            {
                new 月份統計(ymonth.Text, ymonth1.Text).標準成本();
                return;
            }

            if (rdAvgByAllStk.Checked)
            {
                new 月份統計(ymonth.Text, ymonth1.Text,false).月平均成本();
                return;
            }

            if (rdAvgByOneStk.Checked)
            {
                new 月份統計(ymonth.Text, ymonth1.Text, true).月平均成本();
                return;
            }

            if (yLast.Checked)
            {
                new 月份統計(ymonth.Text, ymonth1.Text).最後一次進貨成本();
                return;
            }
        }
        private void eNext_Click(object sender, EventArgs e)
        {
            if (tabControlT1.SelectedTab.Equals(tabPage4) == false)
                return;

            if (emonth.TrimTextLenth() == 0)
            {
                MessageBox.Show("請輸入查詢年月！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ymonth.Focus();
                return;
            }

            if (emonth1.TrimTextLenth() == 0)
            {
                MessageBox.Show("請輸入查詢年月！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ymonth1.Focus();
                return;
            }

            if (eEmNo.TrimTextLenth() == 0)
            {
                using (var db = new JBS.xSQL())
                {
                    var tsql = " Select Top 1 emno from empl order by emno asc";
                    var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("x", "x"));

                    if (obj != null)
                        eEmNo.Text = obj.ToString().Trim();
                }
            }

            if (eEmNo1.TrimTextLenth() == 0)
            {
                using (var db = new JBS.xSQL())
                {
                    var tsql = " Select Top 1 emno from empl order by emno desc";
                    var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("x", "x"));

                    if (obj != null)
                        eEmNo1.Text = obj.ToString().Trim();
                }
            }

            if (eEmNo.Text.BigThen(eEmNo1.Text))
            {
                MessageBox.Show("起始業務編號大於終止業務編號!");
                eEmNo.Focus();
                return;
            }

            if (eCost.Checked)
            {
                new 業務統計(emonth.Text, emonth1.Text, eEmNo.Text.Trim(), eEmNo1.Text.Trim()).開窗("標準成本", radioT5.Checked);
                return;
            }

            if (eAvg.Checked)
            {
                new 業務統計(emonth.Text, emonth1.Text, eEmNo.Text.Trim(), eEmNo1.Text.Trim()).開窗("月平均成本", radioT5.Checked);
                return;
            }

            if (eLast.Checked)
            {
                new 業務統計(emonth.Text, emonth1.Text, eEmNo.Text.Trim(), eEmNo1.Text.Trim()).開窗("最後一次進貨成本", radioT5.Checked);
                return;
            }
        }
        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (tabControlT1.SelectedTab.Equals(tabPage1))
                xNext_Click(null, null);
            else if (tabControlT1.SelectedTab.Equals(tabPage2))
                gNext_Click(null, null);
            else if (tabControlT1.SelectedTab.Equals(tabPage3))
                yNext_Click(null, null);
            else if (tabControlT1.SelectedTab.Equals(tabPage4))
                eNext_Click(null, null);
            else if (tabControlT1.SelectedTab.Equals(tabPage5))
                年度銷售報表_產品瀏覽_Click(null, null);
                //MessageBox.Show("此版本未開放此功能....");

        }

        class 月份統計
        {
            string ymonth = "";
            string ymonth1 = "";
            bool 各倉成本 = false;

            public 月份統計(string y, string y1, bool 各倉成本 = false)
            {
                ymonth = y;
                ymonth1 = y1;
                this.各倉成本 = 各倉成本;
            }

            /// <summary>
            /// 組合品取子件, 計算成本
            /// </summary>
            public void 月平均成本()
            {
                DataTable dtSource = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合

                itemcost.Columns.Add("itno", typeof(String));
                itemcost.Columns.Add("月份", typeof(String));
                itemcost.Columns.Add("itcost", typeof(String));

                var ym = Common.Sys_StkYear1;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth + "01"));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1 + "31"));

                    cmd.CommandText = "Select 月份='',銷退金額=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    if (this.各倉成本 == false)
                    {
                        cmd.CommandText = @"Select itno,
                        avgcost01,avgcost02,avgcost03,avgcost04,avgcost05,avgcost06,avgcost07,avgcost08,avgcost09,avgcost10,avgcost11,avgcost12,
                        avgcost13,avgcost14,avgcost15,avgcost16,avgcost17,avgcost18,avgcost19,avgcost20,avgcost21,avgcost22,avgcost23,avgcost24 
                        from itemcost";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int j = 1; j < 13; j++)
                                {
                                    var 月份 = ym + j.ToString().PadLeft(2, '0');
                                    itemcost.Rows.Add(new object[] { reader["itno"], 月份, reader[j] });
                                }

                                for (int j = 13; j < 25; j++)
                                {
                                    var 月份 = (ym + 1) + (j - 12).ToString().PadLeft(2, '0');
                                    itemcost.Rows.Add(new object[] { reader["itno"], 月份, reader[j] });
                                }
                            }
                        }
                    }

                    cmd.CommandText = "Select substring(sadate,1,5)月份,itno,stno,銷退金額=0.0,銷貨金額=mnyb,母數量=qty*itpkgqty from saled where ittrait <> 1 and sadate>=@day and sadate<=@day1 ";
                    da.Fill(saled23);

                    cmd.CommandText = "Select substring(sadate,1,5)月份,itno,stno,銷退金額=(-1)*mnyb,銷貨金額=0.0,母數量=(-1)*qty*itpkgqty from rsaled where ittrait <> 1 and sadate>=@day and sadate<=@day1";
                    da.Fill(rsaled23);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,saled.bomid,substring(sadate,1,5)月份,bom.itno,saled.stno,銷退金額=0.0,銷貨金額=saled.mnyb,母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from salebom bom
                    right join saled on bom.bomid = saled.bomid 
                    where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1";
                    da.Fill(saled1);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,rsaled.bomid,substring(sadate,1,5)月份,bom.itno,rsaled.stno,銷退金額=(-1)*rsaled.mnyb,銷貨金額=0.0,母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from rsalebom bom
                    right join rsaled on bom.bomid = rsaled.bomid 
                    where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1 ";
                    da.Fill(rsaled1);
                }
                //單一組裝成本
                if (this.各倉成本 == false)
                {
                    AvgCost(saled23, itemcost, ref dtSource);
                    AvgCost(rsaled23, itemcost, ref dtSource);
                }
                else
                {
                    AvgCostByOneStk(saled23, itemcost, dtSource);
                    AvgCostByOneStk(rsaled23, itemcost, dtSource);
                }

                //組合品成本
                if (this.各倉成本 == false)
                {
                    BomAvgCost(saled1, itemcost, ref dtSource);
                    BomAvgCost(rsaled1, itemcost, ref dtSource);
                }
                else
                {
                    BomAvgCostByOneStk(saled1, itemcost, dtSource);
                    BomAvgCostByOneStk(rsaled1, itemcost, dtSource);
                }

                //結果: 依月份別計算毛利
                var tResult = dtSource.Clone();
                dtSource.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["月份"].ToString())
                    .ForAll(g =>
                    {
                        var 銷退金額 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退金額 += gw["銷退金額"].ToDecimal();
                            銷貨金額 += gw["銷貨金額"].ToDecimal();
                            銷貨成本 += gw["銷貨成本"].ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key);
                        cq.Enqueue(銷退金額);
                        cq.Enqueue(銷貨金額);
                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tResult.Rows.SyncRoot)
                        {
                            tResult.Rows.Add(cq.ToArray());
                        }
                    });

                dtSource.Clear();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();

                if (tResult.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料");
                    return;
                }

                using (var frm = new FrmSaleGroupMonth())
                {
                    frm.dtD = tResult.AsEnumerable().OrderBy(r => r["月份"].ToString()).CopyToDataTable();
                    frm.ShowDialog();
                }
                tResult.Clear();
            }

            void AvgCostByOneStk(DataTable tSale, DataTable itemCost, DataTable dtSource)
            {
                if (tSale.Rows.Count == 0)
                    return;

                tSale.AsEnumerable().GroupBy(r => r["itno"].ToString().Trim()).AsParallel().ForAll(r =>
                {
                    DataTable temp = new DataTable();
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.AddWithValue("itno", r.Key);
                        cmd.CommandText = @"Select itno,stno,
                                        avgcost01,avgcost02,avgcost03,avgcost04,avgcost05,avgcost06,avgcost07,avgcost08,avgcost09,avgcost10,avgcost11,avgcost12,
                                        avgcost13,avgcost14,avgcost15,avgcost16,avgcost17,avgcost18,avgcost19,avgcost20,avgcost21,avgcost22,avgcost23,avgcost24 
                                        from stkcost where itno=@itno";
                        dd.Fill(temp);
                    }

                    foreach (DataRow item in r)
                    {
                        var cost = temp.AsEnumerable().FirstOrDefault(s => s["stno"].ToString().Trim() == item["stno"].ToString().Trim());
                        object[] addrow;

                        string 查詢月份 = "";
                        if (item["月份"].ToString().Substring(0, 3).ToInteger() == Common.Sys_StkYear1)
                            查詢月份 = item["月份"].ToString().Substring(3, 2);
                        else
                            查詢月份 = (item["月份"].ToString().Substring(3, 2).ToInteger() + 12).ToString().PadLeft(2, '0');

                        decimal 銷貨成本 = 0M;
                        if (cost != null)
                            銷貨成本 = item["母數量"].ToDecimal() * cost["avgcost" + 查詢月份].ToDecimal();

                        addrow = new object[]{
                                item["月份"],
                                item["銷退金額"], 
                                item["銷貨金額"], 
                                0, 
                                銷貨成本
                            };

                        lock (dtSource.Rows.SyncRoot)
                            dtSource.Rows.Add(addrow);
                    }
                    temp.Clear();
                });
            }
            void BomAvgCostByOneStk(DataTable tBom, DataTable itemCost, DataTable dtSource)
            {
                if (tBom.Rows.Count == 0)
                    return;

                DataTable itnoGroupTB = new DataTable();
                itnoGroupTB = tBom.Clone();
                tBom.AsEnumerable().GroupBy(r => r["itno"].ToString().Trim()).AsParallel().ForAll(r =>
                {
                    DataTable temp = new DataTable();
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.AddWithValue("itno", r.Key);
                        cmd.CommandText = @"Select itno,stno,
                                        avgcost01,avgcost02,avgcost03,avgcost04,avgcost05,avgcost06,avgcost07,avgcost08,avgcost09,avgcost10,avgcost11,avgcost12,
                                        avgcost13,avgcost14,avgcost15,avgcost16,avgcost17,avgcost18,avgcost19,avgcost20,avgcost21,avgcost22,avgcost23,avgcost24 
                                        from stkcost where itno=@itno";
                        dd.Fill(temp);
                    }

                    foreach (DataRow item in r)
                    {
                        var cost = temp.AsEnumerable().FirstOrDefault(s => s["stno"].ToString().Trim() == item["stno"].ToString().Trim());
                        DataRow addrow = itnoGroupTB.NewRow();

                        string 查詢月份 = "";
                        if (item["月份"].ToString().Substring(0, 3).ToInteger() == Common.Sys_StkYear1)
                            查詢月份 = item["月份"].ToString().Substring(3, 2);
                        else
                            查詢月份 = (item["月份"].ToString().Substring(3, 2).ToInteger() + 12).ToString().PadLeft(2, '0');

                        decimal 銷貨成本 = 0M;
                        if (cost != null)
                            銷貨成本 = item["子數量"].ToDecimal() * cost["avgcost" + 查詢月份].ToDecimal();

                        addrow["bomid"] = item["bomid"];
                        addrow["母數量"] = item["母數量"];
                        addrow["月份"] = item["月份"];
                        addrow["銷退金額"] = item["銷退金額"];
                        addrow["銷貨金額"] = item["銷貨金額"];
                        addrow["銷貨成本"] = 銷貨成本;

                        lock (itnoGroupTB.Rows.SyncRoot)
                            itnoGroupTB.Rows.Add(addrow);
                    }
                    temp.Clear();
                });

                itnoGroupTB.AsEnumerable().GroupBy(r => r["bomid"].ToString().Trim()).AsParallel().ForAll(r =>
                {
                    object[] row = new object[]{
                        r.First()["月份"], 
                        r.First()["銷退金額"],
                        r.First()["銷貨金額"],
                        0, 
                        r.First()["母數量"].ToDecimal() * r.Sum(gw => gw["銷貨成本"].ToDecimal()) 
                    };

                    lock (dtSource.Rows.SyncRoot)
                        dtSource.Rows.Add(row);
                });

                itnoGroupTB.Clear();
            }

            void AvgCost(DataTable tSale, DataTable itemCost, ref DataTable dtSource)
            {
                if (tSale.Rows.Count == 0)
                    return;

                var temp = tSale.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        s => new { ym = s["月份"].ToString(), itno = s["itno"].ToString() },
                        c => new { ym = c["月份"].ToString(), itno = c["itno"].ToString() },
                        (s, c) => new { sale = s, cost = c.DefaultIfEmpty() })
                    .Select((sc) => new object[] 
                    { 
                        sc.sale["月份"], 
                        sc.sale["銷退金額"], 
                        sc.sale["銷貨金額"], 
                        0, 
                        JoinAvg(sc.sale, sc.cost) 
                    });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }
            decimal JoinAvg(DataRow sale, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                return sale["母數量"].ToDecimal() * row["itcost"].ToDecimal();
            }
            void BomAvgCost(DataTable tBom, DataTable itemCost, ref DataTable dtSource)
            {
                if (tBom.Rows.Count == 0)
                    return;

                var temp = tBom.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        b => new { ym = b["月份"].ToString(), itno = b["itno"].ToString() },
                        c => new { ym = c["月份"].ToString(), itno = c["itno"].ToString() },
                        (b, c) => new { bom = b, cost = c.DefaultIfEmpty() })
                    .Select((bc) => new
                        {
                            bomid = bc.bom["bomid"],
                            母數量 = bc.bom["母數量"],
                            月份 = bc.bom["月份"],
                            銷退金額 = bc.bom["銷退金額"],
                            銷貨金額 = bc.bom["銷貨金額"],
                            銷貨成本 = BomJoinAvg(bc.bom, bc.cost)
                        })
                    .GroupBy(o => o.bomid)
                    .Select(g => new object[] 
                    {
                        g.First().月份, 
                        g.First().銷退金額, 
                        g.First().銷貨金額, 
                        0, 
                        g.First().母數量.ToDecimal() * g.Sum(gw => gw.銷貨成本) 
                    });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }
            decimal BomJoinAvg(DataRow bom, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                return bom["子數量"].ToDecimal() * row["itcost"].ToDecimal();
            }

            /// <summary>
            /// 1 沒進貨紀錄, 則取產品進價 (itbuypri)
            /// 2 有進貨紀錄
            ///     包裝數量相同, 取實際成本(realcost)
            ///     包裝數量不同, 取平均成本(realcost/itpkgqty)
            /// </summary>
            public void 最後一次進貨成本()
            {
                DataTable dtSource = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day0", Common.Sys_StkYear1 - 1 + "0101");
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth + "01"));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1 + "31"));

                    cmd.CommandText = "Select 月份='',銷退金額=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = @"Select itno,bsdate,itunit,itpkgqty,realcost from bshopd Where bsdate>=@day0 and bsdate<=@day1 order by itno asc,bsdate desc,bsno desc";
                    da.Fill(itemcost);

                    cmd.CommandText = @"
                    Select substring(sadate,1,5)月份,saled.sadate,saled.itno,銷退金額=0.0,銷貨金額=saled.mnyb,母數量=saled.qty,saled.itunit,saled.itpkgqty,item.itbuypri
                    From saled 
                    Left join item on saled.itno = item.itno
                    Where saled.ittrait <> 1 and sadate>=@day and sadate<=@day1 ";
                    da.Fill(saled23);

                    cmd.CommandText = @"
                    Select substring(sadate,1,5)月份,rsaled.sadate,rsaled.itno,銷退金額=(-1)*rsaled.mnyb,銷貨金額=0.0,母數量=(-1)*rsaled.qty,rsaled.itunit,rsaled.itpkgqty,item.itbuypri
                    From rsaled 
                    Left join item on rsaled.itno = item.itno
                    Where rsaled.ittrait <> 1 and sadate>=@day and sadate<=@day1";
                    da.Fill(rsaled23);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,saled.bomid,substring(sadate,1,5)月份,saled.sadate,bom.itno,銷退金額=0.0,銷貨金額=saled.mnyb,母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
                    From saled 
                    Left join item on saled.itno = item.itno
                    Left join salebom bom on bom.bomid = saled.bomid 
                    Where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1";
                    da.Fill(saled1);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,rsaled.bomid,substring(sadate,1,5)月份,rsaled.sadate,bom.itno,銷退金額=(-1)*rsaled.mnyb,銷貨金額=0.0,母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
                    From rsaled 
                    Left join item on rsaled.itno = item.itno
                    Left join rsalebom bom on bom.bomid = rsaled.bomid 
                    Where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1 ";
                    da.Fill(rsaled1);
                }
                //單一組裝成本
                LastCost(saled23, itemcost, dtSource);
                LastCost(rsaled23, itemcost, dtSource);

                //組合品成本
                BomJoinCost(saled1, itemcost);
                BomJoinCost(rsaled1, itemcost);

                BomLastCost(saled1, ref dtSource);
                BomLastCost(rsaled1, ref dtSource);

                //結果: 依月份別計算毛利
                var tResult = dtSource.Clone();
                dtSource.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["月份"].ToString())
                    .ForAll(g =>
                    {
                        var 銷退金額 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退金額 += gw["銷退金額"].ToDecimal();
                            銷貨金額 += gw["銷貨金額"].ToDecimal();
                            銷貨成本 += gw["銷貨成本"].ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key);
                        cq.Enqueue(銷退金額);
                        cq.Enqueue(銷貨金額);
                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tResult.Rows.SyncRoot)
                        {
                            tResult.Rows.Add(cq.ToArray());
                        }
                    });

                dtSource.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();

                if (tResult.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料");
                    return;
                }

                using (var frm = new FrmSaleGroupMonth())
                {
                    frm.dtD = tResult.AsEnumerable().OrderBy(r => r["月份"].ToString()).CopyToDataTable();
                    frm.ShowDialog();
                }
                tResult.Clear();
            }

            void LastCost(DataTable tSale, DataTable itemcost, DataTable dtSource)
            {
                tSale.AsEnumerable()
                    .AsParallel()
                    .ForAll(r =>
                    {
                        var itno = r["itno"].ToString().Trim();
                        var day = r["sadate"].ToString();
                        var row = itemcost.AsEnumerable().Where(c => c["itno"].ToString().Trim() == itno && string.CompareOrdinal(day, c["bsdate"].ToString()) >= 0)
                            .FirstOrDefault();

                        if (row == null)
                        {
                            ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                            cq.Enqueue(r["月份"]);
                            cq.Enqueue(r["銷退金額"]);
                            cq.Enqueue(r["銷貨金額"]);
                            cq.Enqueue(0);
                            cq.Enqueue(r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * r["itbuypri"].ToDecimal());

                            lock (dtSource.Rows.SyncRoot)
                            {
                                dtSource.Rows.Add(cq.ToArray());
                            }
                        }
                        else
                        {
                            var saleunit = r["itunit"].ToString();
                            var salepkgqty = r["itpkgqty"].ToDecimal();

                            var bshopunit = row["itunit"].ToString();
                            var bshoppkgqty = row["itpkgqty"].ToDecimal();

                            if (saleunit == bshopunit && salepkgqty == bshoppkgqty)
                            {
                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["月份"]);
                                cq.Enqueue(r["銷退金額"]);
                                cq.Enqueue(r["銷貨金額"]);
                                cq.Enqueue(0);
                                cq.Enqueue(r["母數量"].ToDecimal() * row["realcost"].ToDecimal());

                                lock (dtSource.Rows.SyncRoot)
                                {
                                    dtSource.Rows.Add(cq.ToArray());
                                }
                            }
                            else
                            {
                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["月份"]);
                                cq.Enqueue(r["銷退金額"]);
                                cq.Enqueue(r["銷貨金額"]);
                                cq.Enqueue(0);

                                var cost = 0M;
                                if (bshoppkgqty != 0)
                                    cost = r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * (row["realcost"].ToDecimal() / bshoppkgqty);

                                cq.Enqueue(cost);

                                lock (dtSource.Rows.SyncRoot)
                                {
                                    dtSource.Rows.Add(cq.ToArray());
                                }
                            }
                        }
                    });
            }

            void BomJoinCost(DataTable tBom, DataTable itemcost)
            {
                foreach (DataRow r in tBom.AsEnumerable())
                {
                    var itno = r["itno"].ToString().Trim();
                    var day = r["sadate"].ToString();
                    var row = itemcost.AsEnumerable().Where(c => c["itno"].ToString().Trim() == itno && string.CompareOrdinal(day, c["bsdate"].ToString()) >= 0)
                        .FirstOrDefault();

                    if (row == null)
                    {
                        r["銷貨成本"] = r["子數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * r["itbuypri"].ToDecimal();
                    }
                    else
                    {
                        var saleunit = r["itunit"].ToString();
                        var salepkgqty = r["itpkgqty"].ToDecimal();

                        var bshopunit = row["itunit"].ToString();
                        var bshoppkgqty = row["itpkgqty"].ToDecimal();

                        if (saleunit == bshopunit && salepkgqty == bshoppkgqty)
                        {
                            r["銷貨成本"] = r["子數量"].ToDecimal() * row["realcost"].ToDecimal();
                        }
                        else
                        {
                            var cost = 0M;
                            if (bshoppkgqty != 0)
                                cost = r["子數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * (row["realcost"].ToDecimal() / bshoppkgqty);

                            r["銷貨成本"] = cost;
                        }
                    }
                }
            }

            void BomLastCost(DataTable tBom, ref DataTable dtSource)
            {
                var temp = tBom.AsEnumerable()
                    .GroupBy(r => r["bomid"].ToString())
                    .Select(g => new object[]
                    { 
                        g.First()["月份"], 
                        g.First()["銷退金額"], 
                        g.First()["銷貨金額"], 
                        0, 
                        g.First()["母數量"].ToDecimal() * g.Sum(gw => gw["銷貨成本"].ToDecimal()) 
                    });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            /// <summary>
            /// 組合品取子件, 計算成本
            /// </summary> 
            public void 標準成本()
            {
                DataTable dtSource = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth + "01"));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1 + "31"));

                    cmd.CommandText = "Select 月份='',銷退金額=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = @"Select itno,itpkgqty,itcost,itcostp from item";
                    da.Fill(itemcost);

                    cmd.CommandText = "Select substring(sadate,1,5)月份,itno,銷退金額=0.0,銷貨金額=mnyb,母數量=qty,itpkgqty from saled where ittrait <> 1 and sadate>=@day and sadate<=@day1 ";
                    da.Fill(saled23);

                    cmd.CommandText = "Select substring(sadate,1,5)月份,itno,銷退金額=(-1)*mnyb,銷貨金額=0.0,母數量=(-1)*qty,itpkgqty from rsaled where ittrait <> 1 and sadate>=@day and sadate<=@day1";
                    da.Fill(rsaled23);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,saled.bomid,substring(sadate,1,5)月份,bom.itno,銷退金額=0.0,銷貨金額=saled.mnyb,母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from salebom bom
                        right join saled on bom.bomid = saled.bomid 
                        where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1";
                    da.Fill(saled1);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,rsaled.bomid,substring(sadate,1,5)月份,bom.itno,銷退金額=(-1)*rsaled.mnyb,銷貨金額=0.0,母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from rsalebom bom
                        right join rsaled on bom.bomid = rsaled.bomid 
                        where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1 ";
                    da.Fill(rsaled1);
                }
                //單一組裝成本
                ItemCost(saled23, itemcost, ref dtSource);
                ItemCost(rsaled23, itemcost, ref dtSource);

                //組合品成本
                BomItemCost(saled1, itemcost, ref dtSource);
                BomItemCost(rsaled1, itemcost, ref dtSource);

                //結果: 依月份別計算毛利
                var tResult = dtSource.Clone();
                dtSource.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["月份"].ToString())
                    .ForAll(g =>
                    {
                        var 銷退金額 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退金額 += gw["銷退金額"].ToDecimal();
                            銷貨金額 += gw["銷貨金額"].ToDecimal();
                            銷貨成本 += gw["銷貨成本"].ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key);
                        cq.Enqueue(銷退金額);
                        cq.Enqueue(銷貨金額);
                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tResult.Rows.SyncRoot)
                        {
                            tResult.Rows.Add(cq.ToArray());
                        }
                    });

                dtSource.Clear();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();

                if (tResult.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料");
                    return;
                }

                using (var frm = new FrmSaleGroupMonth())
                {
                    frm.dtD = tResult.AsEnumerable().OrderBy(r => r["月份"].ToString()).CopyToDataTable();
                    frm.ShowDialog();
                }
                tResult.Clear();
            }

            void ItemCost(DataTable tSale, DataTable itemCost, ref DataTable dtSource)
            {
                if (tSale.Rows.Count == 0)
                    return;

                var temp = tSale.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        s => new { itno = s["itno"].ToString() },
                        c => new { itno = c["itno"].ToString() },
                        (s, c) => new { sale = s, cost = c.DefaultIfEmpty() })
                    .Select(sc => new object[] 
                        { 
                            sc.sale["月份"], 
                            sc.sale["銷退金額"], 
                            sc.sale["銷貨金額"], 
                            0, 
                            ItemCostOrCostP(sc.sale, sc.cost) 
                        });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal ItemCostOrCostP(DataRow sale, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                if (sale["itpkgqty"].ToDecimal() == row["itpkgqty"].ToDecimal())
                    return sale["母數量"].ToDecimal() * row["itcostp"].ToDecimal();
                else
                    return sale["母數量"].ToDecimal() * sale["itpkgqty"].ToDecimal() * row["itcost"].ToDecimal();
            }

            void BomItemCost(DataTable tBom, DataTable itemCost, ref DataTable dtSource)
            {
                if (tBom.Rows.Count == 0)
                    return;

                var temp = tBom.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        b => new { itno = b["itno"].ToString() },
                        c => new { itno = c["itno"].ToString() },
                        (b, c) => new { bom = b, cost = c.DefaultIfEmpty() })
                    .Select((sc) => new
                        {
                            bomid = sc.bom["bomid"],
                            母數量 = sc.bom["母數量"],
                            月份 = sc.bom["月份"],
                            銷退金額 = sc.bom["銷退金額"],
                            銷貨金額 = sc.bom["銷貨金額"],
                            銷貨成本 = BomItemCostOrCostP(sc.bom, sc.cost)
                        })
                    .GroupBy(o => o.bomid)
                    .Select(g => new object[] 
                        { 
                            g.First().月份, 
                            g.First().銷退金額, 
                            g.First().銷貨金額, 
                            0, 
                            g.First().母數量.ToDecimal() * g.Sum(gw => gw.銷貨成本) 
                        });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal BomItemCostOrCostP(DataRow bom, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                if (bom["itpkgqty"].ToDecimal() == row["itpkgqty"].ToDecimal())
                    return bom["子數量"].ToDecimal() * row["itcostp"].ToDecimal();
                else
                    return bom["子數量"].ToDecimal() * bom["itpkgqty"].ToDecimal() * row["itcost"].ToDecimal();
            }
        }

        class 業務統計
        {
            List<DataTable> list;
            string emno = "";
            string emno1 = "";
            string ymonth = "";
            string ymonth1 = "";

            public 業務統計(string y, string y1, string em, string em1)
            {
                this.list = new List<DataTable>();
                this.ymonth = y;
                this.ymonth1 = y1;
                this.emno = em;
                this.emno1 = em1;
            }

            public void 開窗(string cost, bool radio5 = true)
            {
                List<string> liempl;
                DataTable dtempl = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("emno", emno);
                    cmd.Parameters.AddWithValue("emno1", emno1);
                    cmd.CommandText = " Select * from empl where emno >= @emno And emno <= @emno1 order by emno";
                    da.Fill(dtempl);
                }

                if (dtempl.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料!");
                    return;
                }

                liempl = dtempl.AsEnumerable().Select(r => r["emno"].ToString().Trim()).ToList();

                if (cost == "月平均成本")
                {
                    for (int i = 0; i < liempl.Count; i++)
                    {
                        this.emno = liempl[i];
                        月平均成本();
                    }
                }
                else if (cost == "最後一次進貨成本")
                {
                    for (int i = 0; i < liempl.Count; i++)
                    {
                        this.emno = liempl[i];
                        最後一次進貨成本();
                    }
                }
                else if (cost == "標準成本")
                {
                    for (int i = 0; i < liempl.Count; i++)
                    {
                        this.emno = liempl[i];
                        標準成本();
                    }
                }

                if (list.Count == 0)
                {
                    MessageBox.Show("查無資料!");
                    return;
                }

                if (radio5)
                {
                    using (var frm = new FrmEmplSaleGroupMonth())
                    {
                        frm.list = this.list;

                        frm.ShowDialog();
                    }
                }
                else
                {
                    using (var frm = new FrmEmplSaleGroupMonth2())
                    {
                        frm.list = this.list;

                        frm.ShowDialog();
                    }
                }

                list.Clear();
                list = null;
            }

            /// <summary>
            /// 組合品取子件, 計算成本
            /// </summary>
            public void 月平均成本()
            {
                DataTable dtSource = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合

                itemcost.Columns.Add("itno", typeof(String));
                itemcost.Columns.Add("月份", typeof(String));
                itemcost.Columns.Add("itcost", typeof(String));

                var ym = Common.Sys_StkYear1;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth + "01"));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1 + "31"));
                    cmd.Parameters.AddWithValue("emno", emno);

                    cmd.CommandText = "Select 月份='',銷退金額=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = @"Select itno,
                    avgcost01,avgcost02,avgcost03,avgcost04,avgcost05,avgcost06,avgcost07,avgcost08,avgcost09,avgcost10,avgcost11,avgcost12,
                    avgcost13,avgcost14,avgcost15,avgcost16,avgcost17,avgcost18,avgcost19,avgcost20,avgcost21,avgcost22,avgcost23,avgcost24 
                    from itemcost";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int j = 1; j < 13; j++)
                            {
                                var 月份 = ym + j.ToString().PadLeft(2, '0');
                                itemcost.Rows.Add(new object[] { reader["itno"], 月份, reader[j] });
                            }

                            for (int j = 13; j < 25; j++)
                            {
                                var 月份 = (ym + 1) + (j - 12).ToString().PadLeft(2, '0');
                                itemcost.Rows.Add(new object[] { reader["itno"], 月份, reader[j] });
                            }
                        }
                    }
                    cmd.CommandText = "Select substring(sadate,1,5)月份,itno,銷退金額=0.0,銷貨金額=mnyb,母數量=qty*itpkgqty from saled where ittrait <> 1 and sadate>=@day and sadate<=@day1 and saled.emno = @emno ";
                    da.Fill(saled23);

                    cmd.CommandText = "Select substring(sadate,1,5)月份,itno,銷退金額=(-1)*mnyb,銷貨金額=0.0,母數量=(-1)*qty*itpkgqty from rsaled where ittrait <> 1 and sadate>=@day and sadate<=@day1 and rsaled.emno = @emno";
                    da.Fill(rsaled23);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,saled.bomid,substring(sadate,1,5)月份,bom.itno,銷退金額=0.0,銷貨金額=saled.mnyb,母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from salebom bom
                    right join saled on bom.bomid = saled.bomid 
                    where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1 and saled.emno = @emno ";
                    da.Fill(saled1);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,rsaled.bomid,substring(sadate,1,5)月份,bom.itno,銷退金額=(-1)*rsaled.mnyb,銷貨金額=0.0,母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from rsalebom bom
                    right join rsaled on bom.bomid = rsaled.bomid 
                    where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1 and rsaled.emno = @emno ";
                    da.Fill(rsaled1);
                }
                //單一組裝成本
                AvgCost(saled23, itemcost, ref dtSource);
                AvgCost(rsaled23, itemcost, ref dtSource);

                //組合品成本
                BomAvgCost(saled1, itemcost, ref dtSource);
                BomAvgCost(rsaled1, itemcost, ref dtSource);

                //結果: 依月份別計算毛利
                var tResult = dtSource.Clone();
                dtSource.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["月份"].ToString())
                    .ForAll(g =>
                    {
                        var 銷退金額 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退金額 += gw["銷退金額"].ToDecimal();
                            銷貨金額 += gw["銷貨金額"].ToDecimal();
                            銷貨成本 += gw["銷貨成本"].ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key);
                        cq.Enqueue(銷退金額);
                        cq.Enqueue(銷貨金額);
                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tResult.Rows.SyncRoot)
                        {
                            tResult.Rows.Add(cq.ToArray());
                        }
                    });

                dtSource.Clear();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();

                if (tResult.Rows.Count == 0)
                {
                    //MessageBox.Show("查無資料");
                    return;
                }
                else
                {
                    var year = ymonth.takeString(3).ToInteger();
                    for (int i = 1; i <= 12; i++)
                    {
                        var mon = year + (i.ToString().PadLeft(2, '0'));

                        if (mon.BigThen(ymonth1))
                            break;

                        if (tResult.AsEnumerable().Any(r => r["月份"].ToString().Trim() == mon) == false)
                            tResult.Rows.Add(new object[] { mon, 0, 0, 0, 0, 0, 0 });
                    }
                    year += 1;
                    for (int i = 1; i <= 12; i++)
                    {
                        var mon = year + (i.ToString().PadLeft(2, '0'));
                        if (mon.BigThen(ymonth1))
                            break;

                        if (tResult.AsEnumerable().Any(r => r["月份"].ToString().Trim() == mon) == false)
                            tResult.Rows.Add(new object[] { mon, 0, 0, 0, 0, 0, 0 });
                    }
                }

                //using (var frm = new FrmSaleGroupMonth())
                //{
                //    frm.EmNo = this.emno;
                //    frm.reportName = "業務銷售月份統計表.rpt";
                //    frm.dtD = tResult.AsEnumerable().OrderBy(r => r["月份"].ToString()).CopyToDataTable();
                //    frm.ShowDialog();
                //}
                var tb = tResult.AsEnumerable().OrderBy(r => r["月份"].ToString()).CopyToDataTable();
                tb.TableName = this.emno;
                list.Add(tb);
                tResult.Clear();
            }

            void AvgCost(DataTable tSale, DataTable itemCost, ref DataTable dtSource)
            {
                if (tSale.Rows.Count == 0)
                    return;

                var temp = tSale.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        s => new { ym = s["月份"].ToString(), itno = s["itno"].ToString() },
                        c => new { ym = c["月份"].ToString(), itno = c["itno"].ToString() },
                        (s, c) => new { sale = s, cost = c.DefaultIfEmpty() })
                    .Select((sc) => new object[] 
                    { 
                        sc.sale["月份"], 
                        sc.sale["銷退金額"], 
                        sc.sale["銷貨金額"], 
                        0, 
                        JoinAvg(sc.sale, sc.cost) 
                    });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal JoinAvg(DataRow sale, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                return sale["母數量"].ToDecimal() * row["itcost"].ToDecimal();
            }

            void BomAvgCost(DataTable tBom, DataTable itemCost, ref DataTable dtSource)
            {
                if (tBom.Rows.Count == 0)
                    return;

                var temp = tBom.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        b => new { ym = b["月份"].ToString(), itno = b["itno"].ToString() },
                        c => new { ym = c["月份"].ToString(), itno = c["itno"].ToString() },
                        (b, c) => new { bom = b, cost = c.DefaultIfEmpty() })
                    .Select((bc) => new
                    {
                        bomid = bc.bom["bomid"],
                        母數量 = bc.bom["母數量"],
                        月份 = bc.bom["月份"],
                        銷退金額 = bc.bom["銷退金額"],
                        銷貨金額 = bc.bom["銷貨金額"],
                        銷貨成本 = BomJoinAvg(bc.bom, bc.cost)
                    })
                    .GroupBy(o => o.bomid)
                    .Select(g => new object[] 
                    {
                        g.First().月份, 
                        g.First().銷退金額, 
                        g.First().銷貨金額, 
                        0, 
                        g.First().母數量.ToDecimal() * g.Sum(gw => gw.銷貨成本) 
                    });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal BomJoinAvg(DataRow bom, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                return bom["子數量"].ToDecimal() * row["itcost"].ToDecimal();
            }

            /// <summary>
            /// 1 沒進貨紀錄, 則取產品進價 (itbuypri)
            /// 2 有進貨紀錄
            ///     包裝數量相同, 取實際成本(realcost)
            ///     包裝數量不同, 取平均成本(realcost/itpkgqty)
            /// </summary>
            public void 最後一次進貨成本()
            {
                DataTable dtSource = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day0", Common.Sys_StkYear1 - 1 + "0101");
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth + "01"));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1 + "31"));
                    cmd.Parameters.AddWithValue("emno", emno);

                    cmd.CommandText = "Select 月份='',銷退金額=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = @"Select itno,bsdate,itunit,itpkgqty,realcost from bshopd Where bsdate>=@day0 and bsdate<=@day1 order by itno asc,bsdate desc,bsno desc";
                    da.Fill(itemcost);

                    cmd.CommandText = @"
                    Select substring(sadate,1,5)月份,saled.sadate,saled.itno,銷退金額=0.0,銷貨金額=saled.mnyb,母數量=saled.qty,saled.itunit,saled.itpkgqty,item.itbuypri
                    From saled 
                    Left join item on saled.itno = item.itno
                    Where saled.ittrait <> 1 and sadate>=@day and sadate<=@day1 and saled.emno = @emno ";
                    da.Fill(saled23);

                    cmd.CommandText = @"
                    Select substring(sadate,1,5)月份,rsaled.sadate,rsaled.itno,銷退金額=(-1)*rsaled.mnyb,銷貨金額=0.0,母數量=(-1)*rsaled.qty,rsaled.itunit,rsaled.itpkgqty,item.itbuypri
                    From rsaled 
                    Left join item on rsaled.itno = item.itno
                    Where rsaled.ittrait <> 1 and sadate>=@day and sadate<=@day1 and rsaled.emno = @emno ";
                    da.Fill(rsaled23);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,saled.bomid,substring(sadate,1,5)月份,saled.sadate,bom.itno,銷退金額=0.0,銷貨金額=saled.mnyb,母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
                    From saled 
                    Left join item on saled.itno = item.itno
                    Left join salebom bom on bom.bomid = saled.bomid 
                    Where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1 and saled.emno = @emno ";
                    da.Fill(saled1);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,rsaled.bomid,substring(sadate,1,5)月份,rsaled.sadate,bom.itno,銷退金額=(-1)*rsaled.mnyb,銷貨金額=0.0,母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
                    From rsaled 
                    Left join item on rsaled.itno = item.itno
                    Left join rsalebom bom on bom.bomid = rsaled.bomid 
                    Where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1 and rsaled.emno = @emno ";
                    da.Fill(rsaled1);
                }
                //單一組裝成本
                LastCost(saled23, itemcost, dtSource);
                LastCost(rsaled23, itemcost, dtSource);

                //組合品成本
                BomJoinCost(saled1, itemcost);
                BomJoinCost(rsaled1, itemcost);

                BomLastCost(saled1, ref dtSource);
                BomLastCost(rsaled1, ref dtSource);

                //結果: 依月份別計算毛利
                var tResult = dtSource.Clone();
                dtSource.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["月份"].ToString())
                    .ForAll(g =>
                    {
                        var 銷退金額 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退金額 += gw["銷退金額"].ToDecimal();
                            銷貨金額 += gw["銷貨金額"].ToDecimal();
                            銷貨成本 += gw["銷貨成本"].ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key);
                        cq.Enqueue(銷退金額);
                        cq.Enqueue(銷貨金額);
                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tResult.Rows.SyncRoot)
                        {
                            tResult.Rows.Add(cq.ToArray());
                        }
                    });

                dtSource.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();

                if (tResult.Rows.Count == 0)
                {
                    //MessageBox.Show("查無資料");
                    return;
                }
                else
                {
                    var year = ymonth.takeString(3).ToInteger();
                    for (int i = 1; i <= 12; i++)
                    {
                        var mon = year + (i.ToString().PadLeft(2, '0'));

                        if (mon.BigThen(ymonth1))
                            break;

                        if (tResult.AsEnumerable().Any(r => r["月份"].ToString().Trim() == mon) == false)
                            tResult.Rows.Add(new object[] { mon, 0, 0, 0, 0, 0, 0 });
                    }
                    year += 1;
                    for (int i = 1; i <= 12; i++)
                    {
                        var mon = year + (i.ToString().PadLeft(2, '0'));
                        if (mon.BigThen(ymonth1))
                            break;

                        if (tResult.AsEnumerable().Any(r => r["月份"].ToString().Trim() == mon) == false)
                            tResult.Rows.Add(new object[] { mon, 0, 0, 0, 0, 0, 0 });
                    }
                }

                //using (var frm = new FrmSaleGroupMonth())
                //{
                //    frm.EmNo = this.emno;
                //    frm.reportName = "業務銷售月份統計表.rpt";
                //    frm.dtD = tResult.AsEnumerable().OrderBy(r => r["月份"].ToString()).CopyToDataTable();
                //    frm.ShowDialog();
                //}
                var tb = tResult.AsEnumerable().OrderBy(r => r["月份"].ToString()).CopyToDataTable();
                tb.TableName = this.emno;
                list.Add(tb);
                tResult.Clear();
            }

            void LastCost(DataTable tSale, DataTable itemcost, DataTable dtSource)
            {
                tSale.AsEnumerable()
                    .AsParallel()
                    .ForAll(r =>
                    {
                        var itno = r["itno"].ToString().Trim();
                        var day = r["sadate"].ToString();
                        var row = itemcost.AsEnumerable().Where(c => c["itno"].ToString().Trim() == itno && string.CompareOrdinal(day, c["bsdate"].ToString()) >= 0)
                            .FirstOrDefault();

                        if (row == null)
                        {
                            ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                            cq.Enqueue(r["月份"]);
                            cq.Enqueue(r["銷退金額"]);
                            cq.Enqueue(r["銷貨金額"]);
                            cq.Enqueue(0);
                            cq.Enqueue(r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * r["itbuypri"].ToDecimal());

                            lock (dtSource.Rows.SyncRoot)
                            {
                                dtSource.Rows.Add(cq.ToArray());
                            }
                        }
                        else
                        {
                            var saleunit = r["itunit"].ToString();
                            var salepkgqty = r["itpkgqty"].ToDecimal();

                            var bshopunit = row["itunit"].ToString();
                            var bshoppkgqty = row["itpkgqty"].ToDecimal();

                            if (saleunit == bshopunit && salepkgqty == bshoppkgqty)
                            {
                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["月份"]);
                                cq.Enqueue(r["銷退金額"]);
                                cq.Enqueue(r["銷貨金額"]);
                                cq.Enqueue(0);
                                cq.Enqueue(r["母數量"].ToDecimal() * row["realcost"].ToDecimal());

                                lock (dtSource.Rows.SyncRoot)
                                {
                                    dtSource.Rows.Add(cq.ToArray());
                                }
                            }
                            else
                            {
                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["月份"]);
                                cq.Enqueue(r["銷退金額"]);
                                cq.Enqueue(r["銷貨金額"]);
                                cq.Enqueue(0);

                                var cost = 0M;
                                if (bshoppkgqty != 0)
                                    cost = r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * (row["realcost"].ToDecimal() / bshoppkgqty);

                                cq.Enqueue(cost);

                                lock (dtSource.Rows.SyncRoot)
                                {
                                    dtSource.Rows.Add(cq.ToArray());
                                }
                            }
                        }
                    });
            }

            void BomJoinCost(DataTable tBom, DataTable itemcost)
            {
                foreach (DataRow r in tBom.AsEnumerable())
                {
                    var itno = r["itno"].ToString().Trim();
                    var day = r["sadate"].ToString();
                    var row = itemcost.AsEnumerable().Where(c => c["itno"].ToString().Trim() == itno && string.CompareOrdinal(day, c["bsdate"].ToString()) >= 0)
                        .FirstOrDefault();

                    if (row == null)
                    {
                        r["銷貨成本"] = r["子數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * r["itbuypri"].ToDecimal();
                    }
                    else
                    {
                        var saleunit = r["itunit"].ToString();
                        var salepkgqty = r["itpkgqty"].ToDecimal();

                        var bshopunit = row["itunit"].ToString();
                        var bshoppkgqty = row["itpkgqty"].ToDecimal();

                        if (saleunit == bshopunit && salepkgqty == bshoppkgqty)
                        {
                            r["銷貨成本"] = r["子數量"].ToDecimal() * row["realcost"].ToDecimal();
                        }
                        else
                        {
                            var cost = 0M;
                            if (bshoppkgqty != 0)
                                cost = r["子數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * (row["realcost"].ToDecimal() / bshoppkgqty);

                            r["銷貨成本"] = cost;
                        }
                    }
                }
            }

            void BomLastCost(DataTable tBom, ref DataTable dtSource)
            {
                var temp = tBom.AsEnumerable()
                    .GroupBy(r => r["bomid"].ToString())
                    .Select(g => new object[]
                    { 
                        g.First()["月份"], 
                        g.First()["銷退金額"], 
                        g.First()["銷貨金額"], 
                        0, 
                        g.First()["母數量"].ToDecimal() * g.Sum(gw => gw["銷貨成本"].ToDecimal()) 
                    });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            /// <summary>
            /// 組合品取子件, 計算成本
            /// </summary> 
            public void 標準成本()
            {
                DataTable dtSource = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth + "01"));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1 + "31"));
                    cmd.Parameters.AddWithValue("emno", emno);

                    cmd.CommandText = "Select 月份='',銷退金額=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = @"Select itno,itpkgqty,itcost,itcostp from item";
                    da.Fill(itemcost);

                    cmd.CommandText = "Select substring(sadate,1,5)月份,itno,銷退金額=0.0,銷貨金額=mnyb,母數量=qty,itpkgqty from saled where ittrait <> 1 and sadate>=@day and sadate<=@day1 and saled.emno = @emno ";
                    da.Fill(saled23);

                    cmd.CommandText = "Select substring(sadate,1,5)月份,itno,銷退金額=(-1)*mnyb,銷貨金額=0.0,母數量=(-1)*qty,itpkgqty from rsaled where ittrait <> 1 and sadate>=@day and sadate<=@day1 and rsaled.emno = @emno ";
                    da.Fill(rsaled23);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,saled.bomid,substring(sadate,1,5)月份,bom.itno,銷退金額=0.0,銷貨金額=saled.mnyb,母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from salebom bom
                        right join saled on bom.bomid = saled.bomid 
                        where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1 and saled.emno = @emno ";
                    da.Fill(saled1);

                    cmd.CommandText = @"
                    Select 銷貨成本=0.0,rsaled.bomid,substring(sadate,1,5)月份,bom.itno,銷退金額=(-1)*rsaled.mnyb,銷貨金額=0.0,母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from rsalebom bom
                        right join rsaled on bom.bomid = rsaled.bomid 
                        where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) and sadate>=@day and sadate<=@day1 and rsaled.emno = @emno ";
                    da.Fill(rsaled1);
                }
                //單一組裝成本
                ItemCost(saled23, itemcost, ref dtSource);
                ItemCost(rsaled23, itemcost, ref dtSource);

                //組合品成本
                BomItemCost(saled1, itemcost, ref dtSource);
                BomItemCost(rsaled1, itemcost, ref dtSource);

                //結果: 依月份別計算毛利
                var tResult = dtSource.Clone();
                dtSource.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["月份"].ToString())
                    .ForAll(g =>
                    {
                        var 銷退金額 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退金額 += gw["銷退金額"].ToDecimal();
                            銷貨金額 += gw["銷貨金額"].ToDecimal();
                            銷貨成本 += gw["銷貨成本"].ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key);
                        cq.Enqueue(銷退金額);
                        cq.Enqueue(銷貨金額);
                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tResult.Rows.SyncRoot)
                        {
                            tResult.Rows.Add(cq.ToArray());
                        }
                    });

                dtSource.Clear();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();

                if (tResult.Rows.Count == 0)
                {
                    //MessageBox.Show("查無資料");
                    return;
                }
                else
                {
                    var year = ymonth.takeString(3).ToInteger();
                    for (int i = 1; i <= 12; i++)
                    {
                        var mon = year + (i.ToString().PadLeft(2, '0'));

                        if (mon.BigThen(ymonth1))
                            break;

                        if (tResult.AsEnumerable().Any(r => r["月份"].ToString().Trim() == mon) == false)
                            tResult.Rows.Add(new object[] { mon, 0, 0, 0, 0, 0, 0 });
                    }
                    year += 1;
                    for (int i = 1; i <= 12; i++)
                    {
                        var mon = year + (i.ToString().PadLeft(2, '0'));
                        if (mon.BigThen(ymonth1))
                            break;

                        if (tResult.AsEnumerable().Any(r => r["月份"].ToString().Trim() == mon) == false)
                            tResult.Rows.Add(new object[] { mon, 0, 0, 0, 0, 0, 0 });
                    }
                }

                //using (var frm = new FrmSaleGroupMonth())
                //{
                //    frm.EmNo = this.emno;
                //    frm.reportName = "業務銷售月份統計表.rpt";
                //    frm.dtD = tResult.AsEnumerable().OrderBy(r => r["月份"].ToString()).CopyToDataTable();
                //    frm.ShowDialog();
                //}
                var tb = tResult.AsEnumerable().OrderBy(r => r["月份"].ToString()).CopyToDataTable();
                tb.TableName = this.emno;
                list.Add(tb);
                tResult.Clear();
            }

            void ItemCost(DataTable tSale, DataTable itemCost, ref DataTable dtSource)
            {
                if (tSale.Rows.Count == 0)
                    return;

                var temp = tSale.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        s => new { itno = s["itno"].ToString() },
                        c => new { itno = c["itno"].ToString() },
                        (s, c) => new { sale = s, cost = c.DefaultIfEmpty() })
                    .Select(sc => new object[] 
                        { 
                            sc.sale["月份"], 
                            sc.sale["銷退金額"], 
                            sc.sale["銷貨金額"], 
                            0, 
                            ItemCostOrCostP(sc.sale, sc.cost) 
                        });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal ItemCostOrCostP(DataRow sale, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                if (sale["itpkgqty"].ToDecimal() == row["itpkgqty"].ToDecimal())
                    return sale["母數量"].ToDecimal() * row["itcostp"].ToDecimal();
                else
                    return sale["母數量"].ToDecimal() * sale["itpkgqty"].ToDecimal() * row["itcost"].ToDecimal();
            }

            void BomItemCost(DataTable tBom, DataTable itemCost, ref DataTable dtSource)
            {
                if (tBom.Rows.Count == 0)
                    return;

                var temp = tBom.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        b => new { itno = b["itno"].ToString() },
                        c => new { itno = c["itno"].ToString() },
                        (b, c) => new { bom = b, cost = c.DefaultIfEmpty() })
                    .Select((sc) => new
                    {
                        bomid = sc.bom["bomid"],
                        母數量 = sc.bom["母數量"],
                        月份 = sc.bom["月份"],
                        銷退金額 = sc.bom["銷退金額"],
                        銷貨金額 = sc.bom["銷貨金額"],
                        銷貨成本 = BomItemCostOrCostP(sc.bom, sc.cost)
                    })
                    .GroupBy(o => o.bomid)
                    .Select(g => new object[] 
                        { 
                            g.First().月份, 
                            g.First().銷退金額, 
                            g.First().銷貨金額, 
                            0, 
                            g.First().母數量.ToDecimal() * g.Sum(gw => gw.銷貨成本) 
                        });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal BomItemCostOrCostP(DataRow bom, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                if (bom["itpkgqty"].ToDecimal() == row["itpkgqty"].ToDecimal())
                    return bom["子數量"].ToDecimal() * row["itcostp"].ToDecimal();
                else
                    return bom["子數量"].ToDecimal() * bom["itpkgqty"].ToDecimal() * row["itcost"].ToDecimal();
            }
        }

        private void 年度銷售報表_產品瀏覽_Click(object sender, EventArgs e)
        {

            if (tabControlT1.SelectedTab.Equals(tabPage5) == false)
                return;

            if (this.SaleYear.TrimTextLenth() == 0)
            {
                MessageBox.Show("請輸入銷售年度！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.SaleYear.Focus();
                return;
            }
            TextBoxT SaleYear = new TextBoxT();
            SaleYear.Text = this.SaleYear.Text;
            using (var frm = new Frm年度銷售報表(SaleYear, CuNo, CuNo1, ItNo, ItNo1, CheckBox_Sale.Checked, CheckBox_rSale.Checked, radioT_Cust))
            {
                frm.ShowDialog();
            }

        }

        private void SaleYear_Validating(object sender, CancelEventArgs e)
        {
            TextBoxT tb_ = new TextBoxT();
            tb_.Text = ((TextBoxT)sender).Text+"0101";
            if (tb_.IsDateTime() == false)
            {
                e.Cancel = true;
                MessageBox.Show(
                    "日期格式錯誤！",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                ((TextBoxT)sender).SelectAll();
            }
        }

        private void buttonSmallT2_Click(object sender, EventArgs e)
        {
            TextBoxT tb_ = new TextBoxT();
            tb_.Text = ((TextBoxT)sender).Text + "0101";
            if (tb_.IsDateTime() == false)
            {
                //e.Cancel = true;
                MessageBox.Show(
                    "日期格式錯誤！",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                ((TextBoxT)sender).SelectAll();
            }
        }




    }
}
