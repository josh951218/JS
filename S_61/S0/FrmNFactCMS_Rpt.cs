using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using S_61.Basic;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;

namespace S_61.S0
{
    public partial class FrmNFactCMS_Rpt : JE.MyControl.Formbase
    {
        JBS.JS.xEvents xe;

        public FrmNFactCMS_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            Day.SetDateLength();
            Day1.SetDateLength();
            Day.Text = Date.GetDateTime(Common.User_DateTime);
            var len = Day.Text.Length - 2;
            Day.Text = Day.Text.takeString(len) + "01";
            Day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmNFactCMS_Rpt_Load(object sender, EventArgs e)
        {

        }

        private void FaNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Fact>(sender, e, row => FaName1.Text = row["faname1"].ToString().Trim());

            if (FaNo.TrimTextLenth() == 0)
                FaName1.Clear();
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void Day_Validating(object sender, CancelEventArgs e)
        {
            xe.DateValidate(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void OutReport(RptMode mode)
        {
            try
            {
                this.panelBtnT1.Enabled = false;

                if (FaNo.TrimTextLenth() == 0)
                {
                    MessageBox.Show("請輸入廠商編號!");
                    FaNo.Focus();
                    return;
                }

                DataTable temp = new DataTable();
                using (var db = new JBS.xSQL())
                {
                    var Query = "";
                    if (FaNo.TrimTextLenth() > 0)
                        Query = " and Item.fano = @fano";
                    var TQuery = "";
                    if (FaNo.TrimTextLenth() > 0)
                        TQuery = " and T.fano = @fano";
                    var SQuery = "  and Saled.Sadate  >= @day and Saled.Sadate  <= @day1";
                    var RSQuery = " and RSaled.Sadate >= @day and RSaled.Sadate <= @day1";

                    var tsql = @"
Select itno,itname,itprice,itdisc
,後台出單=(銷貨數量後+銷退數量後)
,前台出單=(銷貨數量前+銷退數量前)
,區間交易量=(銷貨數量後+銷退數量後+銷貨數量前+銷退數量前)
,應收金額=(銷貨數量後+銷退數量後+銷貨數量前+銷退數量前)*itprice
,實售金額=(銷貨金額+銷退金額)
,期初庫存=0.0
,進貨數量=0.0
,本月庫存=0.0
,抽成金額=((銷貨金額+銷退金額)*(itdisc/100))
,fact.* from (
    Select T.fano,T.itno,T.itname,T.itprice,T.itdisc
    ,銷貨數量後=ISNULL(銷貨數量後,0) 
    ,銷退數量後=ISNULL(銷退數量後,0)
    ,銷貨數量前=ISNULL(銷貨數量前,0) 
    ,銷退數量前=ISNULL(銷退數量前,0)
    ,銷貨金額=ISNULL(銷貨金額,0) 
    ,銷退金額=ISNULL(銷退金額,0)  
    from item T
    left join (
			    Select S.itno,銷貨數量後=SUM(銷貨數量後) from (
			    Select Saled.itno,銷貨數量後=(Saled.qty*Saled.itpkgqty) from Saled 
			    left join item on saled.itno = item.itno 
			    where bracket <> @前台 and item.ittrait <>1 " + SQuery + Query + @"
			    union all
			    Select SaleBom.itno,銷貨數量後=(saled.qty*saled.itpkgqty*SaleBom.itqty*SaleBom.itpkgqty/SaleBom.itpareprs) from SaleBom 
			    left join saled on SaleBom.BomID = saled.BomID
			    left join item  on SaleBom.itno = item.itno
			    where bracket <> @前台 and saled.ittrait = 1 " + SQuery + Query + @" )S group by S.itno
    ) A on T.itno = A.itno 
    left join (
			    Select S.itno,銷退數量後=(-1)*SUM(銷退數量後) from (
			    Select rsaled.itno,銷退數量後=(rsaled.qty*rsaled.itpkgqty) from rsaled 
			    left join item on rsaled.itno = item.itno 
			    where bracket <> @前台 and item.ittrait <>1 " + RSQuery + Query + @"
			    union all
			    Select rsalebom.itno,銷退數量後=(rsaled.qty*rsaled.itpkgqty*rsalebom.itqty*rsalebom.itpkgqty/rsalebom.itpareprs) from rsalebom 
			    left join rsaled on rsalebom.BomID = rsaled.BomID
			    left join item  on rsalebom.itno = item.itno
			    where bracket <> @前台 and rsaled.ittrait = 1 " + RSQuery + Query + @" )S group by S.itno
    ) B on T.itno = B.itno
    left join (
			    Select S.itno,銷貨數量前=SUM(銷貨數量前) from (
			    Select Saled.itno,銷貨數量前=(Saled.qty*Saled.itpkgqty) from Saled 
			    left join item on saled.itno = item.itno 
			    where bracket = @前台 and item.ittrait <>1 " + SQuery + Query + @"
			    union all
			    Select SaleBom.itno,銷貨數量前=(saled.qty*saled.itpkgqty*SaleBom.itqty*SaleBom.itpkgqty/SaleBom.itpareprs) from SaleBom 
			    left join saled on SaleBom.BomID = saled.BomID
			    left join item  on SaleBom.itno = item.itno
			    where bracket = @前台 and saled.ittrait = 1 " + SQuery + Query + @" )S group by S.itno
    ) C on T.itno = C.itno 
    left join (
			    Select S.itno,銷退數量前=(-1)*SUM(銷退數量前) from (
			    Select rsaled.itno,銷退數量前=(rsaled.qty*rsaled.itpkgqty) from rsaled 
			    left join item on rsaled.itno = item.itno 
			    where bracket = @前台 and item.ittrait <>1 " + RSQuery + Query + @"
			    union all
			    Select rsalebom.itno,銷退數量前=(rsaled.qty*rsaled.itpkgqty*rsalebom.itqty*rsalebom.itpkgqty/rsalebom.itpareprs) from rsalebom 
			    left join rsaled on rsalebom.BomID = rsaled.BomID
			    left join item  on rsalebom.itno = item.itno
			    where bracket = @前台 and rsaled.ittrait = 1 " + RSQuery + Query + @" )S group by S.itno
    ) D on T.itno = D.itno
    left join (
			    Select S.itno,銷貨金額=SUM(銷貨金額) from (
			    Select Saled.itno,銷貨金額=ROUND((Saled.qty*Saled.price*Saled.prs),0) from Saled 
			    left join item on saled.itno = item.itno 
			    where 0=0 " + SQuery + Query + @" )S group by S.itno
    ) E on T.itno = E.itno 
    left join (
			    Select S.itno,銷退金額=(-1)*SUM(銷退金額) from (
			    Select rsaled.itno,銷退金額=ROUND((rsaled.qty*rsaled.price*rsaled.prs),0) from rsaled 
			    left join item on rsaled.itno = item.itno 
			    where 0=0 " + RSQuery + Query + @" )S group by S.itno
    ) F on T.itno = F.itno
    Where 0=0 " + TQuery + @"
)Result left join fact on Result.fano = fact.fano ORDER BY itno asc";
                    db.Fill(tsql, spc =>
                    {
                        spc.AddWithValue("day", Date.ToTWDate(Day.Text));
                        spc.AddWithValue("day1", Date.ToTWDate(Day1.Text));
                        spc.AddWithValue("fano", FaNo.Text.Trim());
                        spc.AddWithValue("前台", "前台");
                    }, ref temp);
                }

                if (temp.Rows.Count == 0 && mode != RptMode.Design)
                {
                    MessageBox.Show("查無資料!");
                    return;
                }

                DateTime t1;
                DateTime.TryParseExact(
                    Date.ToUSDate(Day.Text),
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out t1);
                var t2 = t1.AddDays(-1).ToString("yyyyMMdd");

                AllStock ak = new AllStock();
                var startday = Common.Sys_StkYear1 + "0101";
                var endday = Date.ToTWDate(t2);

                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    var qty = ak.getQty(temp.Rows[i]["itno"].ToString().Trim(), startday, endday);
                    temp.Rows[i]["期初庫存"] = qty;

                    var last = ak.getQty(temp.Rows[i]["itno"].ToString().Trim(), startday, Day1.Text);
                    temp.Rows[i]["本月庫存"] = last;

                    var sale = temp.Rows[i]["區間交易量"].ToDecimal();
                    var X = last + sale - qty;
                    temp.Rows[i]["進貨數量"] = X;
                }

                var path = Common.reportaddress + @"ReportF\寄售廠商對帳表.frx";

                using (var fs = new JBS.FReport())
                {
                    fs.dy.Add("DateRange", Day.Text + "～" + Day1.Text);

                    fs.OutReport(mode, temp, "Table1", path);
                }
            }
            finally
            {
                this.panelBtnT1.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("是否要編輯報表?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    OutReport(RptMode.Design);
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        public struct table
        {
            public string tableName;
            public string bomName;
            public string qtyName;
            public string stnoName;
            public string dateName;

            public table(string tn, string bn, string qn, string sn, string dn)
            {
                this.tableName = tn;
                this.bomName = bn;
                this.qtyName = qn;
                this.stnoName = sn;
                this.dateName = dn;
            }
        }

        public class AllStock
        {
            List<table> listA = new List<table>();
            List<table> listB = new List<table>();

            public AllStock()
            {
                //單一組裝減項，組合子件減項
                listA.Add(new table(" saled", " salebom", "  qty", " stno", "sadate"));
                listA.Add(new table("rshopd", "        ", "  qty", " stno", "bsdate"));
                listA.Add(new table("ouStkd", "oustkbom", "ouqty", " stno", "oudate"));
                listA.Add(new table("allotd", "        ", "  qty", "stnoo", "aldate"));
                listA.Add(new table(" lendd", " lendbom", "  qty", " stno", "ledate"));
                listA.Add(new table("rlendd", "rlendbom", "  qty", "stnoi", "ledate"));
                listA.Add(new table(" borrd", " borrbom", "  qty", "stnoo", "bodate"));
                listA.Add(new table("rborrd", "rborrbom", "  qty", " stno", "bodate"));
                listA.Add(new table(" drawd", " drawbom", "  qty", "stnoo", "drdate"));

                //單一組裝加項，組合子件減項
                listB.Add(new table(" rsaled", "rsalebom", "  qty", " stno", "sadate"));
                listB.Add(new table(" bshopd", "        ", "  qty", " stno", "bsdate"));
                listB.Add(new table(" InStkD", "instkbom", "inqty", " stno", "indate"));
                listB.Add(new table(" allotd", "        ", "  qty", "stnoi", "aldate"));
                listB.Add(new table("adjustd", "        ", "  qty", " stno", "addate"));
                listB.Add(new table("  lendd", " lendbom", "  qty", "stnoi", "ledate"));
                listB.Add(new table(" rlendd", "rlendbom", "  qty", " stno", "ledate"));
                listB.Add(new table("  borrd", " borrbom", "  qty", " stno", "bodate"));
                listB.Add(new table(" rborrd", "rborrbom", "  qty", "stnoo", "bodate"));
                listB.Add(new table("  drawd", " drawbom", "  qty", "stnoi", "drdate"));
            }

            public decimal getQty(string itno, string startDay, string endDay)
            {
                decimal Qty = 0M;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("itno", itno);
                    cmd.Parameters.AddWithValue("day", startDay);
                    cmd.Parameters.AddWithValue("day1", endDay);

                    cmd.CommandText = "Select ittrait from item where itno = @itno ";
                    var ittrait = cmd.ExecuteScalar();
                    if (ittrait.ToDecimal() < 2)
                        return 0;

                    //庫存年度的期初
                    cmd.CommandText = "Select qty=SUM(itqtyf) from stock where itno = @itno group by itno";
                    Qty += cmd.ExecuteScalar().ToDecimal();

                    //減項
                    for (int i = 0; i < listA.Count; i++)
                    {
                        var t = listA.ElementAt(i);

                        cmd.CommandText = "Select qty=(-1)*SUM(" + t.qtyName + ") from " + t.tableName + " where len(" + t.stnoName + ")>0 and itno = @itno and " + t.dateName + " >= @day and " + t.dateName + " <= @day1 group by itno";
                        Qty += cmd.ExecuteScalar().ToDecimal();

                        if (t.bomName.Trim().Length == 0)
                            continue;

                        cmd.CommandText = ""
                            + "Select qty=(-1)*SUM(" + t.tableName + "." + t.qtyName + "*" + t.tableName + ".itpkgqty*" + t.bomName + ".itqty*" + t.bomName + ".itpkgqty/" + t.bomName + ".itpareprs) from " + t.tableName + " left join " + t.bomName + " on " + t.tableName + ".bomid = " + t.bomName + ".bomid "
                            + "Where " + t.tableName + ".ittrait=1 and len(" + t.tableName + "." + t.qtyName + ")>0 and " + t.bomName + ".itno=@itno and " + t.tableName + "." + t.dateName + " >= @day and " + t.tableName + "." + t.dateName + " <= @day1 group by " + t.bomName + ".itno ";
                        Qty += cmd.ExecuteScalar().ToDecimal();
                    }

                    //加項
                    for (int i = 0; i < listB.Count; i++)
                    {
                        var t = listB.ElementAt(i);

                        cmd.CommandText = "Select qty=SUM(" + t.qtyName + ") from " + t.tableName + " where len(" + t.stnoName + ")>0 and itno = @itno and " + t.dateName + " >= @day and " + t.dateName + " <= @day1 group by itno";
                        Qty += cmd.ExecuteScalar().ToDecimal();

                        if (t.bomName.Trim().Length == 0)
                            continue;

                        cmd.CommandText = ""
                            + "Select qty=SUM(" + t.tableName + "." + t.qtyName + "*" + t.tableName + ".itpkgqty*" + t.bomName + ".itqty*" + t.bomName + ".itpkgqty/" + t.bomName + ".itpareprs) from " + t.tableName + " left join " + t.bomName + " on " + t.tableName + ".bomid = " + t.bomName + ".bomid "
                             + "Where " + t.tableName + ".ittrait=1 and len(" + t.tableName + "." + t.qtyName + ")>0 and " + t.bomName + ".itno=@itno and " + t.tableName + "." + t.dateName + " >= @day and " + t.tableName + "." + t.dateName + " <= @day1 group by " + t.bomName + ".itno ";
                        Qty += cmd.ExecuteScalar().ToDecimal();
                    }

                    //入庫+單一組裝
                    cmd.CommandText = "Select qty=SUM(qty) from garnerd where ittrait<>1 and len(stnoi)>0 and itno = @itno and gadate >= @day and gadate <= @day1 group by itno";
                    Qty += cmd.ExecuteScalar().ToDecimal();

                    //入庫-單一
                    cmd.CommandText = "Select qty=(-1)*SUM(qty) from garnerd where ittrait=3 and len(stnoo)>0 and itno = @itno and gadate >= @day and gadate <= @day1 group by itno";
                    Qty += cmd.ExecuteScalar().ToDecimal();
                    //入庫-組裝子件
                    cmd.CommandText = @"Select qty=(-1)*SUM(garnerd.qty*garnerd.itpkgqty*GarnBom.itqty*GarnBom.itpkgqty/GarnBom.itpareprs) from garnerd left join GarnBom on garnerd.bomid = GarnBom.bomid
                                        Where garnerd.ittrait=2 and len(garnerd.stnoo)>0 and GarnBom.itno=@itno and garnerd.gadate >= @day and garnerd.gadate <= @day1 group by GarnBom.itno ";
                    Qty += cmd.ExecuteScalar().ToDecimal();

                }

                return Qty;
            }
        }
    }
}

