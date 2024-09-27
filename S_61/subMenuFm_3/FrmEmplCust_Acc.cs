using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.App_Data.EmplAccNewTableAdapters;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmEmplCust_Acc : Formbase
    {
        JBS.JS.xEvents xe;

        string SQL = "", RSQL = "";

        public FrmEmplCust_Acc()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            SQL = ""
                + " SELECT          sale_adaddr = sale.AdAddr,sale.sano, sale.sadate, sale.sadateac, sale.sadate1, sale.sadate2, sale.sadateac1, sale.sadateac2, sale.quno, "
                + " sale.cono, sale.coname1, sale.coname2, sale.emno, sale.emname, sale.spno, sale.spname, sale.stno, sale.stname, "
                + " sale.xa1no, sale.Xa1Name, sale.xa1par, sale.taxmnyf, sale.taxmnyb, sale.taxmny, sale.x3no, sale.rate, sale.x5no, "
                + " sale.seno, sale.sename, sale.x4no, sale.x4name, sale.tax, sale.totmny, sale.taxb, sale.totmnyb, "
                + " sale.discount AS 折扣金額, sale.cashmny, sale.cardmny, sale.cardno, sale.ticket, sale.collectmny, sale.getprvacc, "
                + " sale.acctmny, sale.samemo, sale.samemo1, sale.bracket, sale.recordno, sale.invno, sale.invdate, sale.invdate1, "
                + " sale.invname, sale.invtaxno, sale.invaddr1, sale.invbatch, sale.invbatflg, sale.appdate, sale.edtdate, sale.appscno, "
                + " sale.edtscno, sale.acno, sale.cloflag, sale.SendAddr, sale.UsrNo, sale.DeNo, sale.DeName, sale.accono, sale.X2No, "
                + " sale.STime, '' AS rdno, sale.collectmny + sale.getprvacc AS 已收預收, 0 AS 前期總金額, 0 AS 稅前總金額, "
                + " 0 AS 營業稅總額, 0 AS 應收總金額, 0 AS 折扣總金額, 0 AS 已收加預收, 0 AS 本期總金額, 0 AS 前期加本期, '銷貨' AS 單據,"
                + " '' AS orno, '' AS itno, '' AS itname, '' AS itunit, '' AS punit, 0 AS qty, 0 AS pqty, 0 AS price, 0 AS mny, cust.* , sale.payerno,出貨客戶=sale.cuname1 "
                + " FROM              sale LEFT OUTER JOIN"
                + " cust ON sale.payerno = cust.cuno"
                + " where 0=0 ";
            RSQL = ""
                + " SELECT          sale_adaddr='',rsale.sano, rsale.sadate, rsale.sadateac, rsale.sadate1, rsale.sadate2, rsale.sadateac1, rsale.sadateac2, rsale.quno, "
                + " rsale.cono, rsale.coname1, rsale.coname2, rsale.emno, rsale.emname, rsale.spno, rsale.spname, rsale.stno, rsale.stname, "
                + " rsale.xa1no, rsale.Xa1Name, rsale.xa1par, rsale.taxmnyf, rsale.taxmnyb, rsale.taxmny, rsale.x3no, rsale.rate, rsale.x5no, "
                + " rsale.seno, rsale.sename, rsale.x4no, rsale.x4name, rsale.tax, rsale.totmny, rsale.taxb, rsale.totmnyb, "
                + " rsale.discount AS 折扣金額, rsale.cashmny, rsale.cardmny, rsale.cardno, rsale.ticket, rsale.collectmny, rsale.getprvacc, "
                + " rsale.acctmny, rsale.samemo, rsale.samemo1, rsale.bracket, rsale.recordno, rsale.invno, rsale.invdate, rsale.invdate1, "
                + " rsale.invname, rsale.invtaxno, rsale.invaddr1, rsale.invbatch, rsale.invbatflg, rsale.appdate, rsale.edtdate, rsale.appscno, "
                + " rsale.edtscno, rsale.acno, rsale.cloflag, rsale.UsrNo, rsale.DeNo, rsale.DeName, rsale.accono, "
                + "  '' AS rdno, rsale.collectmny + rsale.getprvacc AS 已收預收, 0 AS 前期總金額, 0 AS 稅前總金額, "
                + " 0 AS 營業稅總額, 0 AS 應收總金額, 0 AS 折扣總金額, 0 AS 已收加預收, 0 AS 本期總金額, 0 AS 前期加本期, '銷退' AS 單據,"
                + " '' AS orno, '' AS itno, '' AS itname, '' AS itunit, '' AS punit, 0 AS qty, 0 AS pqty, 0 AS price, 0 AS mny, cust.* , rsale.payerno,出貨客戶=rsale.cuname1 "
                + " FROM              rsale LEFT OUTER JOIN"
                + " cust ON rsale.payerno = cust.cuno"
                + " where 0=0 ";
        }

        private void FrmEmplCust_Acc_Load(object sender, EventArgs e)
        {
 
            SaDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            SaDate1.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
          
            SaDate.Text = Date.GetDateTime(Common.User_DateTime);
            SaDate.Text = SaDate.Text.takeString(SaDate.Text.Length - 2) + "01";
            SaDate1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmEmplCust_Acc_Shown(object sender, EventArgs e)
        {
            SaDate.Focus();
        }

        private void X4No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX04>(sender, row =>
            {
                X4No.Text = row["X4No"].ToString().Trim();
                X4Name.Text = row["X4Name"].ToString().Trim();
            });
        }
        private void X4No_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;

            if (X4No.TrimTextLenth() == 0)
            {
                X4No.Clear();
                X4Name.Clear();
                return;
            }

            xe.ValidateOpen<JBS.JS.XX04>(sender, e, row =>
            {
                X4No.Text = row["X4No"].ToString().Trim();
                X4Name.Text = row["X4Name"].ToString().Trim();
            });
        }

        private void ReDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }
        private void ReDate1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }
        private void EmNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (SaDate.Text.BigThen(SaDate1.Text))
            {
                MessageBox.Show("起始日期大於終止日期！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SaDate1.Focus();
                SaDate1.SelectAll();
                return;
            }
            if (EmNo.Text.BigThen(EmNo1.Text))
            {
                MessageBox.Show("起始業務編號大於終止業務編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EmNo.Focus();
                EmNo.SelectAll();
                return;
            }

            if (radioT2.Checked)
            {
                RadioT2Report();
                return;
            }

            List<string[]> list = new List<string[]>();
            DataTable dtTitle = new DataTable();//表頭用
            //
            DataTable dtD = new DataTable();//本期明細
            DataTable dtDBefore = new DataTable();//前期明細
            DataTable dtCust = new DataTable();

            try
            {

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();

                    
                    if (EmNo.TrimTextLenth() == 0)
                    {
                        cmd.CommandText = " Select TOP 1 emno from empl order by emno asc";
                        var p = cmd.ExecuteScalar();
                        if (p != null) EmNo.Text = p.ToString().Trim();
                    }
                    if (EmNo1.TrimTextLenth() == 0)
                    {
                        cmd.CommandText = " Select TOP 1 emno from empl order by emno desc";
                        var p = cmd.ExecuteScalar();
                        if (p != null) EmNo1.Text = p.ToString().Trim();
                    }

                    cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
                    cmd.Parameters.AddWithValue("sadate1", Date.ToTWDate(SaDate1.Text));
                    if (EmNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    if (EmNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                    if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());

                    //先撈本期
                    #region 本期銷貨 dtD
                    cmd.CommandText = SQL
                                    + " and sale.sadate >=@sadate"
                                    + " and sale.sadate <=@sadate1"
                                    + " and len(sale.emno) > 0";
                    if (EmNo.Text.Trim().Length > 0) cmd.CommandText += " and sale.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) cmd.CommandText += " and sale.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) cmd.CommandText += " and cust.cux4no =@cux4no";
                    if (radioT6.Checked) cmd.CommandText += " and sale.AcctMny !=0 ";
                    dtD.Clear();
                    da.Fill(dtD);
                    #endregion

                    #region 本期銷退 dtD
                    cmd.CommandText = RSQL
                                    + " and rsale.sadate >=@sadate"
                                    + " and rsale.sadate <=@sadate1"
                                    + " and len(Rsale.emno) > 0";
                    if (EmNo.Text.Trim().Length > 0) cmd.CommandText += " and rsale.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) cmd.CommandText += " and rsale.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) cmd.CommandText += " and cust.cux4no =@cux4no";
                    if (radioT6.Checked) cmd.CommandText += " and rsale.AcctMny !=0 ";
                    DataTable t = new DataTable();
                    da.Fill(t);
                    //銷退金額變負數
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        t.Rows[i]["TaxMny"] = -1 * t.Rows[i]["TaxMny"].ToDecimal();
                        t.Rows[i]["Tax"] = -1 * t.Rows[i]["Tax"].ToDecimal();
                        t.Rows[i]["TotMny"] = -1 * t.Rows[i]["TotMny"].ToDecimal();
                        t.Rows[i]["CollectMny"] = -1 * t.Rows[i]["CollectMny"].ToDecimal();
                        t.Rows[i]["GetPrvAcc"] = -1 * t.Rows[i]["GetPrvAcc"].ToDecimal();
                        t.Rows[i]["折扣金額"] = -1 * t.Rows[i]["折扣金額"].ToDecimal();
                        t.Rows[i]["AcctMny"] = -1 * t.Rows[i]["AcctMny"].ToDecimal();
                        t.Rows[i]["已收預收"] = -1 * t.Rows[i]["已收預收"].ToDecimal();
                    }
                    dtD.Merge(t);
                    dtD.AcceptChanges();
                    t.Clear();
                    t.Dispose();
                    #endregion

                    #region 表頭 dtTitle
                    string sql = " select sale.*,前期總金額=0.0,筆數=0,稅前總金額=0.0,營業稅總額=0.0,應收總金額=0.0, "
                                + " 折扣總金額=0.0,已收加預收=0.0,本期總金額=0.0,前期加本期=0.0 "
                                + " from sale where 1=0 ";
                    cmd.CommandText = sql;
                    da.Fill(dtTitle);
                    dtTitle.Clear();
                    #endregion

                    foreach (DataRow rw in dtD.Rows)
                    {
                        //帳款歸屬給請款客戶
                        if (rw["payerno1"].ToString().Trim() == "")
                            rw["cuno"] = rw["cuno"].ToString();
                        else
                            rw["cuno"] = rw["payerno1"].ToString();

                        rw["sadate"] = Date.AddLine(rw["sadate"].ToString().Trim());
                        rw["sadate1"] = Date.AddLine(rw["sadate1"].ToString().Trim());

                        var emno = rw["emno"].ToString().Trim();
                        var cuno = rw["cuno"].ToString().Trim();
                        var p = dtTitle.AsEnumerable().FirstOrDefault(r => r["emno"].ToString() == emno && r["cuno"].ToString() == cuno);
                        if (p == null)
                        {
                            list.Add(new string[] { emno, cuno });
                            DataRow row = dtTitle.NewRow();

                            row["Xa1No"] = rw["Xa1No"];
                            row["Xa1Name"] = "";
                            row["emno"] = emno;
                            row["emname"] = "";
                            row["cuno"] = cuno;
                            row["cuname1"] = "";
                            row["前期總金額"] = 0;
                            row["前期加本期"] = rw["AcctMny"].ToDecimal().ToString("f" + Common.MST);

                            row["筆數"] = 1;
                            row["稅前總金額"] = rw["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                            row["營業稅總額"] = rw["Tax"].ToDecimal().ToString("f" + Common.MST);
                            row["應收總金額"] = rw["TotMny"].ToDecimal().ToString("f" + Common.MST);

                            row["折扣總金額"] = rw["折扣金額"].ToDecimal().ToString("f" + Common.MST);
                            row["已收加預收"] = rw["已收預收"].ToDecimal().ToString("f" + Common.MST);
                            row["本期總金額"] = rw["AcctMny"].ToDecimal().ToString("f" + Common.MST);

                            dtTitle.Rows.Add(row);
                        }
                        else
                        {
                            p["筆數"] = (int)p["筆數"].ToDecimal() + 1;
                            p["稅前總金額"] = (p["稅前總金額"].ToDecimal() + rw["TaxMny"].ToDecimal()).ToString("f" + Common.MST);
                            p["營業稅總額"] = (p["營業稅總額"].ToDecimal() + rw["Tax"].ToDecimal()).ToString("f" + Common.MST);
                            p["應收總金額"] = (p["應收總金額"].ToDecimal() + rw["TotMny"].ToDecimal()).ToString("f" + Common.MST);

                            p["折扣總金額"] = (p["折扣總金額"].ToDecimal() + rw["折扣金額"].ToDecimal()).ToString("f" + Common.MST);
                            p["已收加預收"] = (p["已收加預收"].ToDecimal() + rw["已收預收"].ToDecimal()).ToString("f" + Common.MST);
                            p["本期總金額"] = (p["本期總金額"].ToDecimal() + rw["AcctMny"].ToDecimal()).ToString("f" + Common.MST);

                            p["前期加本期"] = (p["前期加本期"].ToDecimal() + rw["AcctMny"].ToDecimal()).ToString("f" + Common.MST);
                        }
                    }

                    //客戶期初開帳
                    cmd.CommandText = "Select * from cust where Len(cuemno1) > 0 ";
                    if (EmNo.Text.Trim().Length > 0) cmd.CommandText += " and cust.cuemno1 >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) cmd.CommandText += " and cust.cuemno1 <=@emno1";
                    cmd.CommandText += " and cust.CuSpareRcv != 0";
                    dtCust.Clear();
                    da.Fill(dtCust);

                    //前期
                    cmd.CommandText = SQL
                                    + " and sale.sadate < @sadate"
                                    + " and len(sale.emno) > 0";
                    if (EmNo.Text.Trim().Length > 0) cmd.CommandText += " and sale.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) cmd.CommandText += " and sale.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) cmd.CommandText += " and cust.cux4no =@cux4no";
                    if (radioT6.Checked) cmd.CommandText += " and sale.AcctMny !=0 ";
                    dtDBefore.Clear();
                    da.Fill(dtDBefore);

                    cmd.CommandText = RSQL
                                    + " and rsale.sadate < @sadate"
                                    + " and len(Rsale.emno) > 0";
                    if (EmNo.Text.Trim().Length > 0) cmd.CommandText += " and rsale.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) cmd.CommandText += " and rsale.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) cmd.CommandText += " and cust.cux4no =@cux4no";
                    if (radioT6.Checked) cmd.CommandText += " and rsale.AcctMny !=0 ";
                    t = new DataTable();
                    da.Fill(t);
                    t.Clear();
                    t.Dispose();
                    //銷退金額變負數
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        t.Rows[i]["TaxMny"] = -1 * t.Rows[i]["TaxMny"].ToDecimal();
                        t.Rows[i]["Tax"] = -1 * t.Rows[i]["Tax"].ToDecimal();
                        t.Rows[i]["TotMny"] = -1 * t.Rows[i]["TotMny"].ToDecimal();
                        t.Rows[i]["CollectMny"] = -1 * t.Rows[i]["CollectMny"].ToDecimal();
                        t.Rows[i]["GetPrvAcc"] = -1 * t.Rows[i]["GetPrvAcc"].ToDecimal();
                        t.Rows[i]["折扣金額"] = -1 * t.Rows[i]["折扣金額"].ToDecimal();
                        t.Rows[i]["AcctMny"] = -1 * t.Rows[i]["AcctMny"].ToDecimal();
                        t.Rows[i]["已收預收"] = -1 * t.Rows[i]["已收預收"].ToDecimal();
                    }
                    dtDBefore.Merge(t);
                    dtDBefore.AcceptChanges();
                    t.Dispose();

                    foreach (DataRow rw in dtCust.Rows)
                    {
                        var emno = rw["cuemno1"].ToString().Trim();
                        var cuno = rw["cuno"].ToString().Trim();

                        DataRow row = dtDBefore.NewRow();
                        row["單據"] = "期初";
                        row["sano"] = "上期餘額";

                        row["Xa1No"] = rw["CuXa1No"];
                        row["Xa1Name"] = "";
                        row["emno"] = emno;
                        row["emname"] = "";
                        row["cuno"] = cuno;
                        row["cuname1"] = rw["cuname1"];
                        row["cuname2"] = rw["cuname2"];
                        row["cutel1"] = rw["cutel1"];
                        row["cuper1"] = rw["cuper1"];
                        row["cuaddr1"] = rw["cuaddr1"];
                        row["cur1"] = rw["cur1"];
                        row["cufax1"] = rw["cufax1"];
                        row["cuuno"] = rw["cuuno"];

                        row["TaxMny"] = 0;
                        row["Tax"] = 0;
                        row["TotMny"] = rw["CuSpareRcv"].ToDecimal("f" + Common.MST);
                        row["CollectMny"] = 0;
                        row["GetPrvAcc"] = 0;
                        row["折扣金額"] = 0;
                        row["AcctMny"] = rw["CuSpareRcv"].ToDecimal("f" + Common.MST);
                        row["已收預收"] = 0;

                        dtDBefore.Rows.Add(row);
                    }

                    foreach (DataRow rw in dtDBefore.Rows)
                    {
                        rw["sadate"] = Date.AddLine(rw["sadate"].ToString().Trim());
                        rw["sadate1"] = Date.AddLine(rw["sadate1"].ToString().Trim());

                        var emno = rw["emno"].ToString().Trim();
                        var cuno = rw["cuno"].ToString().Trim();
                        var p = dtTitle.AsEnumerable().FirstOrDefault(r => r["emno"].ToString() == emno && r["cuno"].ToString() == cuno);
                        if (p == null)
                        {
                            list.Add(new string[] { emno, cuno });
                            DataRow row = dtTitle.NewRow();

                            row["Xa1No"] = rw["Xa1No"];
                            row["Xa1Name"] = "";
                            row["emno"] = emno;
                            row["emname"] = "";
                            row["cuno"] = cuno;
                            row["cuname1"] = "";
                            row["前期總金額"] = rw["AcctMny"].ToDecimal("f" + Common.MST);
                            row["前期加本期"] = rw["AcctMny"].ToDecimal("f" + Common.MST);

                            row["筆數"] = 0;
                            row["稅前總金額"] = 0;
                            row["營業稅總額"] = 0;
                            row["應收總金額"] = 0;

                            row["折扣總金額"] = 0;
                            row["已收加預收"] = 0;
                            row["本期總金額"] = 0;

                            dtTitle.Rows.Add(row);
                        }
                        else
                        {
                            p["前期總金額"] = (p["前期總金額"].ToDecimal() + rw["AcctMny"].ToDecimal()).ToString("f" + Common.MST);
                            p["前期加本期"] = (p["前期加本期"].ToDecimal() + rw["AcctMny"].ToDecimal()).ToString("f" + Common.MST);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }


            if (dtTitle.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (radioT4.Checked)
                {
                    dtD.Merge(dtDBefore);
                }
                DataTable empl = new DataTable();
                DataTable cust = new DataTable();
                DataTable xa01 = new DataTable();
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        string sql = "select emno,emname from empl";
                        using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                        {
                            da.Fill(empl);
                        }
                        sql = "select cuno,cuname1,cuname2,cutel1,cufax1,cuxa1no,cuuno,cuper1,cuaddr1 from cust";
                        using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                        {
                            da.Fill(cust);
                        }
                        sql = "select * from xa01";
                        using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                        {
                            da.Fill(xa01);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                foreach (DataRow rw in dtTitle.Rows)
                {
                    var emno = rw["emno"].ToString().Trim();
                    var cuno = rw["cuno"].ToString().Trim();
                    
                    var p = empl.AsEnumerable().FirstOrDefault(r => r["emno"].ToString().Trim() == emno);
                    if (p.IsNotNull()) rw["emname"] = p["emname"].ToString().Trim();
                    p = cust.AsEnumerable().FirstOrDefault(r => r["cuno"].ToString().Trim() == cuno);
                    if (p.IsNotNull()) rw["cuname1"] = p["cuname1"].ToString().Trim();
                    var xa1no = rw["Xa1No"].ToString().Trim();
                    p = xa01.AsEnumerable().FirstOrDefault(r => r["Xa1No"].ToString().Trim() == xa1no);
                    if (p.IsNotNull()) rw["Xa1Name"] = p["Xa1Name"].ToString().Trim();
                }
                foreach (DataRow rw in dtD.Rows)
                {
                    var emno = rw["emno"].ToString().Trim();
                    var cuno = rw["cuno"].ToString().Trim();
                    var p = empl.AsEnumerable().FirstOrDefault(r => r["emno"].ToString().Trim() == emno);
                    if (p.IsNotNull()) rw["emname"] = p["emname"].ToString().Trim();
                    p = cust.AsEnumerable().FirstOrDefault(r => r["cuno"].ToString().Trim() == cuno);
                    if (p.IsNotNull()) { 填客戶資料(rw, p); }
                    //if (p.IsNotNull()) rw["cuname1"] = p["cuname1"].ToString().Trim();
                    var xa1no = rw["Xa1No"].ToString().Trim();
                    p = xa01.AsEnumerable().FirstOrDefault(r => r["Xa1No"].ToString().Trim() == xa1no);
                    if (p.IsNotNull()) rw["Xa1Name"] = p["Xa1Name"].ToString().Trim();
                }
            }

            if (radioT1.Checked)
            {
                this.OpemInfoFrom<FrmEmplCust_Accb>(() =>
                            {
                                FrmEmplCust_Accb frm = new FrmEmplCust_Accb();
                                frm.list = list.OrderBy(r => r[0]).ThenBy(r => r[1]).ToList();
                                frm.dtTitle = dtTitle.Copy();
                                DataTable d = CreateDetail(ref dtD, ref dtTitle);
                                frm.dt = dtD.Copy();
                                frm.dtDetail = d.Copy();
                                frm.DateRange = "帳款區間：" + Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                return frm;
                            });
            }
            else if (radioT2.Checked)
            {
                this.OpemInfoFrom<FrmEmplCust_AccBrow>(() =>
                            {
                                FrmEmplCust_AccBrow frm = new FrmEmplCust_AccBrow();
                                frm.IsPrintBefore = radioT4.Checked;
                                dtTitle.DefaultView.RowFilter = "筆數 > 0 or 前期總金額 > 0";
                                frm.dt = dtTitle.DefaultView.ToTable();
                                frm.DateRange = "帳款區間：" + Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                return frm;
                            });
            }
            else if (radioT3.Checked)
            {
                this.OpemInfoFrom<FrmEmplCust_AccBrowx>(() =>
                            {
                                FrmEmplCust_AccBrowx frm = new FrmEmplCust_AccBrowx();
                                frm.dt = dtTitle.Copy();
                                frm.DateRange = "帳款區間：" + Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                return frm;
                            });
            }
        }

        private void 填客戶資料(DataRow rw, DataRow p)
        {
            rw["cuname1"] = p["cuname1"].ToString().Trim();
            rw["cuname2"] = p["cuname2"].ToString().Trim();
            rw["cuaddr1"] = p["cuaddr1"].ToString().Trim();
            rw["cutel1"] = p["cutel1"].ToString().Trim();
            rw["cufax1"] = p["cufax1"].ToString().Trim();
            rw["Xa1No"] = p["cuxa1no"].ToString().Trim();
            rw["cuuno"] = p["cuuno"].ToString().Trim();
            rw["cuper1"] = p["cuper1"].ToString().Trim();
        }

        DataTable CreateDetail(ref DataTable t, ref DataTable title)
        {
            DataTable temp = new DataTable();
            DataTable temp1 = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
                        cmd.Parameters.AddWithValue("sadate1", Date.ToTWDate(SaDate1.Text));

                        cmd.CommandText = "select *,單據='銷貨' from saled where 0=0";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(temp);
                        }

                        cmd.CommandText = "select *,單據='銷退' from rsaled where 0=0";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(temp1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            var p = t.AsEnumerable().ToList();
            string sano = "";
            string kind = "";

            DataTable detail = t.Clone();
            var rows = t.AsEnumerable().Where(r => r["單據"].ToString().Trim() == "期初");
            if (rows.Count() > 0) detail = rows.CopyToDataTable();

            foreach (DataRow row in temp.Rows)
            {
                sano = row["sano"].ToString().Trim();
                kind = row["單據"].ToString().Trim();
                var w = p.Find(r => r["sano"].ToString().Trim() == sano && r["單據"].ToString().Trim() == kind);
                if (w.IsNotNull())
                {
                    w["rdno"] = row["recordno"];
                    w["orno"] = row["orno"];
                    w["itno"] = row["itno"];
                    w["itname"] = row["itname"];
                    w["itunit"] = row["itunit"];
                    w["punit"] = row["punit"];
                    w["qty"] = row["qty"].ToDecimal("f" + Common.Q);
                    w["pqty"] = row["pqty"].ToDecimal("f" + Common.Q);
                    w["price"] = row["price"].ToDecimal("f" + Common.MS);
                    w["mny"] = row["mny"].ToDecimal("f" + Common.TPS);
                    detail.ImportRow(w);
                }
            }
            detail.AcceptChanges();
            foreach (DataRow row in temp1.Rows)
            {
                sano = row["sano"].ToString().Trim();
                kind = row["單據"].ToString().Trim();
                var w = p.Find(r => r["sano"].ToString().Trim() == sano && r["單據"].ToString().Trim() == kind);
                if (w.IsNotNull())
                {
                    w["rdno"] = row["recordno"];
                    w["orno"] = row["orno"];
                    w["itno"] = row["itno"];
                    w["itname"] = row["itname"];
                    w["itunit"] = row["itunit"];
                    w["punit"] = row["punit"];
                    w["qty"] = row["qty"].ToDecimal("f" + Common.Q) * -1;
                    w["pqty"] = row["pqty"].ToDecimal("f" + Common.Q);
                    w["price"] = row["price"].ToDecimal("f" + Common.MS) * -1;
                    w["mny"] = row["mny"].ToDecimal("f" + Common.TPS) * -1;
                    detail.ImportRow(w);
                }
            }
            detail.AcceptChanges();

            string emno = "";
            string cuno = "";
            foreach (DataRow rw in t.Rows)
            {
                emno = rw["emno"].ToString().Trim();
                cuno = rw["cuno"].ToString().Trim();

                var row = title.AsEnumerable().FirstOrDefault(r => r["emno"].ToString() == emno && r["cuno"].ToString() == cuno);
                if (row != null)
                {
                    rw["稅前總金額"] = row["稅前總金額"].ToDecimal("f" + Common.MST);
                    rw["營業稅總額"] = row["營業稅總額"].ToDecimal("f" + Common.MST);
                    rw["應收總金額"] = row["應收總金額"].ToDecimal("f" + Common.MST);

                    rw["折扣總金額"] = row["折扣總金額"].ToDecimal("f" + Common.MST);
                    rw["已收加預收"] = row["已收加預收"].ToDecimal("f" + Common.MST);
                    rw["本期總金額"] = row["本期總金額"].ToDecimal("f" + Common.MST);

                    rw["前期加本期"] = row["前期加本期"].ToDecimal("f" + Common.MST);
                    rw["前期總金額"] = row["前期總金額"].ToDecimal("f" + Common.MST);
                }
                else
                {
                    rw["稅前總金額"] = 0;
                    rw["營業稅總額"] = 0;
                    rw["應收總金額"] = 0;

                    rw["折扣總金額"] = 0;
                    rw["已收加預收"] = 0;
                    rw["本期總金額"] = 0;

                    rw["前期加本期"] = 0;
                    rw["前期總金額"] = 0;
                }
            }
            foreach (DataRow rw in detail.Rows)
            {
                emno = rw["emno"].ToString().Trim();
                cuno = rw["cuno"].ToString().Trim();

                var row = title.AsEnumerable().FirstOrDefault(r => r["emno"].ToString() == emno && r["cuno"].ToString() == cuno);
                if (row != null)
                {
                    rw["稅前總金額"] = row["稅前總金額"].ToDecimal("f" + Common.MST);
                    rw["營業稅總額"] = row["營業稅總額"].ToDecimal("f" + Common.MST);
                    rw["應收總金額"] = row["應收總金額"].ToDecimal("f" + Common.MST);

                    rw["折扣總金額"] = row["折扣總金額"].ToDecimal("f" + Common.MST);
                    rw["已收加預收"] = row["已收加預收"].ToDecimal("f" + Common.MST);
                    rw["本期總金額"] = row["本期總金額"].ToDecimal("f" + Common.MST);

                    rw["前期加本期"] = row["前期加本期"].ToDecimal("f" + Common.MST);
                    rw["前期總金額"] = row["前期總金額"].ToDecimal("f" + Common.MST);
                }
                else
                {
                    rw["稅前總金額"] = 0;
                    rw["營業稅總額"] = 0;
                    rw["應收總金額"] = 0;

                    rw["折扣總金額"] = 0;
                    rw["已收加預收"] = 0;
                    rw["本期總金額"] = 0;

                    rw["前期加本期"] = 0;
                    rw["前期總金額"] = 0;
                }
            }
            return detail;
        }

        void RadioT2Report()
        {
            try
            {
                DataTable dtCur = new DataTable();
                DataTable dtBef = new DataTable();
                DataTable dtCust = new DataTable();
                using (GetSaleTableAdapter da = new GetSaleTableAdapter())
                {
                    da.Connection.ConnectionString = Common.sqlConnString;
                    da.GetData();

                    da.Adapter.SelectCommand.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
                    da.Adapter.SelectCommand.Parameters.AddWithValue("sadate1", Date.ToTWDate(SaDate1.Text));
                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("cux4no", X4No.Text.Trim());

                    da.Adapter.SelectCommand.CommandText += " and sale.sadate >=@sadate";
                    da.Adapter.SelectCommand.CommandText += " and sale.sadate <=@sadate1";
                    da.Adapter.SelectCommand.CommandText += " and Len(sale.emno) > 0";
                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and sale.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and sale.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and cust.cux4no =@cux4no";
                    if (radioT6.Checked) da.Adapter.SelectCommand.CommandText += " and sale.AcctMny !=0 ";

                    dtCur = da.GetData();
                }
                using (GetRaleTableAdapter da = new GetRaleTableAdapter())
                {
                    da.Connection.ConnectionString = Common.sqlConnString;
                    da.GetData();

                    da.Adapter.SelectCommand.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
                    da.Adapter.SelectCommand.Parameters.AddWithValue("sadate1", Date.ToTWDate(SaDate1.Text));
                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("cux4no", X4No.Text.Trim());

                    da.Adapter.SelectCommand.CommandText += " and rsale.sadate >=@sadate";
                    da.Adapter.SelectCommand.CommandText += " and rsale.sadate <=@sadate1";
                    da.Adapter.SelectCommand.CommandText += " and Len(rsale.emno) > 0";
                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and rsale.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and rsale.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and cust.cux4no =@cux4no";
                    if (radioT6.Checked) da.Adapter.SelectCommand.CommandText += " and rsale.AcctMny !=0 ";

                    dtCur.Merge(da.GetData());
                }
                using (GetSaleTableAdapter da = new GetSaleTableAdapter())
                {
                    da.Connection.ConnectionString = Common.sqlConnString;
                    da.GetData();

                    da.Adapter.SelectCommand.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
                    da.Adapter.SelectCommand.Parameters.AddWithValue("sadate1", Date.ToTWDate(SaDate1.Text));
                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("cux4no", X4No.Text.Trim());

                    da.Adapter.SelectCommand.CommandText += " and sale.sadate < @sadate";
                    da.Adapter.SelectCommand.CommandText += " and Len(sale.emno) > 0";
                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and sale.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and sale.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and cust.cux4no =@cux4no";
                    da.Adapter.SelectCommand.CommandText += " and sale.AcctMny !=0 ";

                    dtBef = da.GetData();
                }
                using (GetRaleTableAdapter da = new GetRaleTableAdapter())
                {
                    da.Connection.ConnectionString = Common.sqlConnString;
                    da.GetData();

                    da.Adapter.SelectCommand.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
                    da.Adapter.SelectCommand.Parameters.AddWithValue("sadate1", Date.ToTWDate(SaDate1.Text));
                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("cux4no", X4No.Text.Trim());

                    da.Adapter.SelectCommand.CommandText += " and rsale.sadate < @sadate";
                    da.Adapter.SelectCommand.CommandText += " and Len(rsale.emno) > 0";
                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and rsale.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and rsale.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and cust.cux4no =@cux4no";
                    da.Adapter.SelectCommand.CommandText += " and rsale.AcctMny !=0 ";

                    dtBef.Merge(da.GetData());
                }
                using (getCustTableAdapter da = new getCustTableAdapter())
                {
                    da.Connection.ConnectionString = Common.sqlConnString;
                    da.GetData();

                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.Parameters.AddWithValue("cux4no", X4No.Text.Trim());

                    if (EmNo.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and empl.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and empl.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) da.Adapter.SelectCommand.CommandText += " and cust.cux4no =@cux4no";
                    da.Adapter.SelectCommand.CommandText += " and ISNULL(CuSpareRcv,0) != 0 ";
                    da.Adapter.SelectCommand.CommandText += " and len(cuemno1) > 0 ";

                    dtCust = da.GetData();
                }

                //前期 AND 本期 客戶編號轉換為請款客戶
                foreach (DataRow item in dtCur.Rows)
                {
                    item["cuno"] = item["payerno"].ToString().Trim();
                }
                foreach (DataRow item in dtBef.Rows)
                {
                    item["cuno"] = item["payerno"].ToString().Trim();
                }

                //本期
                var list = from r in dtCur.AsEnumerable()
                           group r by new
                           {
                               emno = r["EmNo"].ToString().Trim(),
                               cuno = r["CuNo"].ToString().Trim()
                           } into g
                           select new { g.Key.emno, g.Key.cuno };

                DataTable dtResult = dtCur.Clone();
                foreach (var li in list)
                {
                    var rows = dtCur.AsEnumerable().Where(r => r["emno"].ToString().Trim() == li.emno && r["cuno"].ToString().Trim() == li.cuno);
                    if (rows.Count() == 1)
                    {
                        DataRow row = rows.FirstOrDefault();
                        row["前期帳款"] = 0;
                        row["交易筆數"] = 1;
                        row["已收加預收"] = row["CollectMny"].ToDecimal("f" + Common.MST) * row["Xa1par"].ToDecimal("f4") + 
                                            row["GetPrvAcc"].ToDecimal("f" + Common.MST) * row["Xa1par"].ToDecimal("f4");
                        row["前期加本期"] = 0 + row["Acctmny"].ToDecimal("f" + Common.MST) * row["Xa1par"].ToDecimal("f4");

                        row["Discount"] = rows.Sum(r => r["Discount"].ToDecimal("f" + Common.MST) * r["Xa1par"].ToDecimal("f4"));
                        row["Acctmny"] = rows.Sum(r => r["Acctmny"].ToDecimal("f" + Common.MST) * r["Xa1par"].ToDecimal("f4"));

                        dtResult.ImportRow(row);
                    }
                    else
                    {
                        DataRow row = rows.FirstOrDefault();

                        row["前期帳款"] = 0;
                        row["交易筆數"] = rows.Count();
                        row["已收加預收"] = rows.Sum(r => r["CollectMny"].ToDecimal("f" + Common.MST) * r["Xa1par"].ToDecimal("f4") + r["GetPrvAcc"].ToDecimal("f" + Common.MST) * r["Xa1par"].ToDecimal("f4"));
                        row["前期加本期"] = rows.Sum(r => r["Acctmny"].ToDecimal("f" + Common.MST) * r["Xa1par"].ToDecimal("f4"));

                        row["TaxMny"] = rows.Sum(r => r["TaxMny"].ToDecimal());
                        row["TaxMnyb"] = rows.Sum(r => r["TaxMnyb"].ToDecimal());
                        row["Tax"] = rows.Sum(r => r["Tax"].ToDecimal());
                        row["Taxb"] = rows.Sum(r => r["Taxb"].ToDecimal());
                        row["TotMny"] = rows.Sum(r => r["TotMny"].ToDecimal());
                        row["TotMnyb"] = rows.Sum(r => r["TotMnyb"].ToDecimal());
                        row["Discount"] = rows.Sum(r => r["Discount"].ToDecimal("f" + Common.MST) * r["Xa1par"].ToDecimal("f4"));
                        row["Acctmny"] = rows.Sum(r => r["Acctmny"].ToDecimal("f" + Common.MST) * r["Xa1par"].ToDecimal("f4"));
                        dtResult.ImportRow(row);
                    }
                }
                //前期
                list = from r in dtBef.AsEnumerable()
                       group r by new
                       {
                           emno = r["EmNo"].ToString(),
                           cuno = r["CuNo"].ToString()
                       } into g
                       select new { g.Key.emno, g.Key.cuno };

                foreach (var li in list)
                {
                    var row = dtResult.AsEnumerable().Where(r => r["emno"].ToString().Trim() == li.emno && r["cuno"].ToString().Trim() == li.cuno).FirstOrDefault();
                    if (row == null)
                    {
                        var rows = dtBef.AsEnumerable().Where(r => r["emno"].ToString().Trim() == li.emno && r["cuno"].ToString().Trim() == li.cuno);
                        if (rows.Count() == 1) //結果集裡面沒有這筆資料
                        {
                            var rw = rows.FirstOrDefault();

                            rw["前期帳款"] = rw["Acctmny"].ToDecimal("f" + Common.MST) * rw["Xa1par"].ToDecimal("f4");
                            rw["交易筆數"] = 0;
                            rw["已收加預收"] = 0;
                            rw["前期加本期"] = rw["Acctmny"].ToDecimal("f" + Common.MST) * rw["Xa1par"].ToDecimal("f4");

                            rw["Taxmny"] = rw["Taxmnyb"] = rw["Tax"] = rw["Taxb"] = rw["Totmny"] = rw["Totmnyb"] = rw["discount"] = rw["acctmny"] = 0;
                            dtResult.ImportRow(rw);
                        }
                        else//結果集裡面沒有這個資料集合
                        {
                            var rw = rows.FirstOrDefault();

                            rw["前期帳款"] = rows.Sum(r => r["Acctmny"].ToDecimal("f" + Common.MST) * rw["Xa1par"].ToDecimal("f4"));
                            rw["交易筆數"] = 0;
                            rw["已收加預收"] = 0;
                            rw["前期加本期"] = rows.Sum(r => r["Acctmny"].ToDecimal("f" + Common.MST) * rw["Xa1par"].ToDecimal("f4"));

                            rw["Taxmny"] = rw["Taxmnyb"] = rw["Tax"] = rw["Taxb"] = rw["Totmny"] = rw["Totmnyb"] = rw["discount"] = rw["acctmny"] = 0;
                            dtResult.ImportRow(rw);
                        }
                    }
                    else
                    {
                        //結果集裡已經有相同群組了(row),將前期合併至(row)
                        var rows = dtBef.AsEnumerable().Where(r => r["emno"].ToString().Trim() == li.emno && r["cuno"].ToString().Trim() == li.cuno);

                        row["前期帳款"] = row["前期帳款"].ToDecimal() + rows.Sum(r => r["Acctmny"].ToDecimal("f" + Common.MST) * r["Xa1par"].ToDecimal("f4"));
                        row["前期加本期"] = row["前期加本期"].ToDecimal() + rows.Sum(r => r["Acctmny"].ToDecimal("f" + Common.MST) * r["Xa1par"].ToDecimal("f4"));
                    }
                }
                //期初
                foreach (DataRow rw in dtCust.Rows)
                {
                    var emno = rw["emno"].ToString().Trim();
                    var cuno = rw["cuno"].ToString().Trim();
                    var row = dtResult.AsEnumerable().Where(r => r["emno"].ToString().Trim() == emno && r["cuno"].ToString().Trim() == cuno).FirstOrDefault();
                    //var row = dtResult.Select("EmNo ='" + emno + "' And CuNo ='" + cuno + "'").FirstOrDefault();
                    if (row == null)//結果集沒有期初
                    {
                        row = dtResult.NewRow();
                        row["Taxmny"] = row["Taxmnyb"] = row["Tax"] = row["Taxb"] = row["Totmny"] = row["Totmnyb"] = row["discount"] = row["acctmny"] = 0;
                        row["cashmny"] = row["cardmny"] = row["ticket"] = row["collectmny"] = row["getprvacc"] = 0;

                        row["前期帳款"] = rw["CuSpareRcv"].ToDecimal() * rw["CuFirRcvPar"].ToDecimal();
                        row["交易筆數"] = 0;
                        row["已收加預收"] = 0;
                        row["前期加本期"] = rw["CuSpareRcv"].ToDecimal() * rw["CuFirRcvPar"].ToDecimal();

                        row["sano"] = cuno + emno;
                        row["cuno"] = cuno;
                        row["cuname2"] = rw["cuname2"].ToString().Trim();
                        row["cuname1"] = rw["cuname1"].ToString().Trim();
                        row["cutel1"] = rw["cutel1"].ToString().Trim();
                        row["cuper1"] = rw["cuper1"].ToString().Trim();
                        row["cufax1"] = rw["cufax1"].ToString().Trim();
                        row["cur1"] = rw["cur1"].ToString().Trim();
                        row["cuaddr1"] = rw["cuaddr1"].ToString().Trim();
                        row["cuuno"] = rw["cuuno"].ToString().Trim();
                        row["cuper"] = rw["cuper"].ToString().Trim();
                        row["emno"] = emno;
                        row["emname"] = rw["emname"].ToString().Trim();
                        row["xa1no"] = rw["xa1no"].ToString().Trim();
                        row["xa1Name"] = rw["xa1Name"].ToString().Trim();

                        dtResult.Rows.Add(row);
                    }
                    else//合併期初
                    {
                        row["前期帳款"] = row["前期帳款"].ToDecimal() + rw["CuSpareRcv"].ToDecimal() * rw["CuFirRcvPar"].ToDecimal(); ;
                        row["前期加本期"] = row["前期加本期"].ToDecimal() + rw["CuSpareRcv"].ToDecimal() * rw["CuFirRcvPar"].ToDecimal(); ;
                    }
                }
                if (dtResult.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料");
                    return;
                }
                this.OpemInfoFrom<FrmEmplCust_AccBrow>(() =>
                            {
                                FrmEmplCust_AccBrow frm = new FrmEmplCust_AccBrow();
                                frm.dt = dtResult.Copy();
                                frm.DateRange = "帳款區間：" + SaDate.Text.Trim() + " ～ " + SaDate1.Text.Trim();
                                return frm;
                            });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
