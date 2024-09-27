using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmRLend_Print : Formbase
    {
        JBS.JS.xEvents xe;
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        public string PK;
        string ReportFileName = "";
        string ReportPath = "";
        string sql; 

        DataSet ds = new DataSet();

        public FrmRLend_Print()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmLend_PrintNew_Load(object sender, EventArgs e)
        {
            ReportFileName = "倉庫借出還入作業系統";
            rpt3.SetUserDefineRpt("倉庫借出還入作業系統_簡要自定.rpt");
            rpt4.SetUserDefineRpt("倉庫借出還入作業系統_組件自定.rpt");

            LeNo.Focus();
            LeNo.Text = this.PK;
            LeNo1.Text = this.PK;

            SetRdUdf();
        }

        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(LeNo1.Text, LeNo.Text) < 0)
            {
                MessageBox.Show("終止還入單號不可小於起始還入單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ds.Clear();
            try
            {
                setsql();
                sql += " where '0'='0'";
                if (LeNo.Text != "")
                    sql += " and rlendd.leno >=@leno ";
                if (LeNo1.Text != "")
                    sql += " and rlendd.leno <=@leno1 ";
                 
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    if (LeNo.Text != "") cmd.Parameters.AddWithValue("leno", LeNo.Text);
                    if (LeNo1.Text != "") cmd.Parameters.AddWithValue("leno1", LeNo1.Text);

                    sql += " order by rlendd.leno,rlendd.recordno ";

                    cmd.CommandText = sql;
                    dd.Fill(ds, "rlend");

                    cmd.CommandText = @"
                        SELECT 
                        itno, itnoudf, itname, kino, itime, ittrait, itunit, itunitp, itpkgqty, itnw, itnwunit, itdesp1, itdesp2, itdesp3, itdesp4, 
                        itdesp5, itdesp6, itdesp7, itdesp8, itdesp9, itdesp10, itnote, itudf1, itudf2, itudf3, itudf4, itudf5, fano, Punit, ScNo
                        FROM item ";
                    dd.Fill(ds, "item");

                    cmd.CommandText = " SELECT cuno, cuemno1, cuname2, cuname1 FROM cust ";
                    dd.Fill(ds, "rlend-cust");

                    cmd.CommandText = "SELECT emno, emname FROM empl";
                    dd.Fill(ds, "rlend-empl");
                }

                if (ds.Tables["rlend"].Rows.Count == 0)
                {
                    LeNo.Focus();
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReportDocument oRpt = new ReportDocument();
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName;

                if (rpt1.Checked)
                {
                    ReportPath += "_簡要表.rpt";
                    if (File.Exists(ReportPath))
                    {
                        var ph = Common.reportaddress + "Report\\RLendRpt1.rpt";
                        if (File.Exists(ph))
                        {
                            oRpt.Load(ph);
                        }
                        else
                        {
                            oRpt.Load(ReportPath);
                        }
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (rpt2.Checked)
                {
                    ReportPath += "_組件明細.rpt";
                    if (File.Exists(ReportPath))
                        oRpt.Load(ReportPath);
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (rpt3.Checked)
                {
                    ReportPath += "_簡要自定.rpt";
                    if (File.Exists(ReportPath))
                        oRpt.Load(ReportPath);
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    ReportPath += "_組件自定.rpt";
                    if (File.Exists(ReportPath))
                        oRpt.Load(ReportPath);
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


                oRpt.SetDataSource(ds);
                if (Common.Sql_LogMod == 2)//混合驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in oRpt.Database.Tables)
                    {
                        table.ApplyLogOnInfo(Common.logOnInfo);
                    }
                }
                else if (Common.Sql_LogMod == 1)//SQL驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in oRpt.Database.Tables)
                    {
                        table.ApplyLogOnInfo(Common.logOnInfo);
                    }
                }
                oRpt.Refresh();

                TextObject myFieldTitleName;

                List<TextObject> Txt = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    //公司抬頭
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                    if (Conostart_1.Checked) myFieldTitleName.Text = Common.dtstart.Rows[0]["pnname"].ToString();
                    else if (Conostart_2.Checked) myFieldTitleName.Text = Common.dtstart.Rows[1]["pnname"].ToString();
                    else if (Conostart_3.Checked) myFieldTitleName.Text = Common.dtstart.Rows[2]["pnname"].ToString();
                    else if (Conostart_4.Checked) myFieldTitleName.Text = Common.dtstart.Rows[3]["pnname"].ToString();
                    else if (Conostart_5.Checked) myFieldTitleName.Text = Common.dtstart.Rows[4]["pnname"].ToString();
                    else myFieldTitleName.Text = "";
                }
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    //三行註腳
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (Threetxtend_1.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                    else if (Threetxtend_2.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                    else if (Threetxtend_3.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                    else if (Threetxtend_4.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                    else if (Threetxtend_5.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
                if (Txt.Find(t => t.Name == "txtadress") != null)
                {
                    //表頭-公司住址
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtadress"];
                    if (Conostart_1.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[0]["pnaddr"].ToString();
                    else if (Conostart_2.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[1]["pnaddr"].ToString();
                    else if (Conostart_3.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[2]["pnaddr"].ToString();
                    else if (Conostart_4.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[3]["pnaddr"].ToString();
                    else if (Conostart_5.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[4]["pnaddr"].ToString();
                    else myFieldTitleName.Text = "";
                }
                if (Txt.Find(t => t.Name == "txttel") != null)
                {
                    //表頭-電話、傳真
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txttel"];
                    if (Conostart_1.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
                    else if (Conostart_2.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
                    else if (Conostart_3.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
                    else if (Conostart_4.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
                    else if (Conostart_5.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
                    else myFieldTitleName.Text = "";
                }

                List<ParameterField> num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
                //日期格式

                if (num.Find(p => p.Name == "date") != null)
                {
                    switch (Common.User_DateTime)
                    {
                        case 1:
                            oRpt.SetParameterValue("date", "民國");
                            break;
                        case 2:
                            oRpt.SetParameterValue("date", "西元");
                            break;
                    }
                }
                if (num.Find(p => p.Name == "備註說明") != null)
                {
                    oRpt.SetParameterValue("備註說明", Common.Sys_MemoUdf);
                }
                if (num.Find(p => p.Name == "使用者") != null)
                {
                    oRpt.SetParameterValue("使用者", Common.User_Name1);
                }

                //報表參數設定
                if (num.Find(p => p.Name == "顯示千分位") != null)
                {
                    if (pVar.TRS != "")
                        pVar.ShowTRS = true;
                    oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
                }
                if (num.Find(p => p.Name == "千分位") != null)
                    oRpt.SetParameterValue("千分位", pVar.TRS);
                if (num.Find(p => p.Name == "銷貨單價小數") != null)
                    oRpt.SetParameterValue("銷貨單價小數", Common.MS);
                if (num.Find(p => p.Name == "銷貨單據小數") != null)
                    oRpt.SetParameterValue("銷貨單據小數", Common.MST);

                if (num.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);

                if (num.Find(p => p.Name == "銷項稅額小數") != null)
                    oRpt.SetParameterValue("銷項稅額小數", Common.TS);
                if (num.Find(p => p.Name == "本幣金額小數") != null)
                    oRpt.SetParameterValue("本幣金額小數", Common.M);
                if (num.Find(p => p.Name == "庫存數量小數") != null)
                    oRpt.SetParameterValue("庫存數量小數", Common.Q);
                if (num.Find(p => p.Name == "是否顯示金額") != null)
                    oRpt.SetParameterValue("是否顯示金額", Common.User_SalePrice);


                Common.FrmReport = new Report.Frmreport();
                Common.FrmReport.cview.ReportSource = oRpt;
                Common.FrmReport.rpt1 = oRpt;

                if (mode == RptMode.Print)
                    Common.FrmReport.button1_Click(null, null);
                else if (mode == RptMode.PreView)
                    Common.FrmReport.ShowDialog();
                else if (mode == RptMode.Word)
                    Common.FrmReport.word(ReportFileName);
                else if (mode == RptMode.Excel)
                    Common.FrmReport.excel(ReportFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Excel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void LeNo_DoubleClick(object sender, EventArgs e)
        { 
            xe.Open<JBS.JS.RLend>(sender);
        }

        private void LeNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.RLend>(sender, e, row => { });
        }

        void setsql()
        {
            sql = "";
            sql += " SELECT rlend.leno AS rlend_leno, rlend.ledate AS rlend_ledate, rlend.ledate1 AS rlend_ledate1, rlend.ledate2 AS rlend_ledate2, ";
            sql += " rlend.cono AS rlend_cono, rlend.coname1 AS rlend_coname1, rlend.coname2 AS rlend_coname2, rlend.cuno AS rlend_cuno, ";
            sql += " rlend.cuname1 AS rlend_cuname1, rlend.cuname2 AS rlend_cuname2, rlend.cutel AS rlend_cutel, ";
            sql += " rlend.cuper1 AS rlend_cuper1, rlend.stno AS rlend_stno, rlend.stname AS rlend_stname, rlend.emno AS rlend_emno, ";
            sql += " rlend.emname AS rlend_emname, rlend.xa1no AS rlend_xa1no, rlend.xa1name AS rlend_xa1name, ";
            sql += " rlend.xa1par AS rlend_xa1par, rlend.taxmnyf AS rlend_taxmnyf, rlend.taxmnyb AS rlend_taxmnyb, ";
            sql += " rlend.taxmny AS rlend_taxmny, rlend.x3no AS rlend_x3no, rlend.rate AS rlend_rate, rlend.tax AS rlend_tax, ";
            sql += " rlend.totmny AS rlend_totmny, rlend.taxb AS rlend_taxb, rlend.totmnyb AS rlend_totmnyb, rlend.lememo AS rlend_lememo, rlend.lememo1 AS rlend_lememo1,";
            sql += " rlend.recordno AS rlend_recordno,  rlendd.ledate AS rlendd_ledate, rlendd.leno AS rlendd_leno, ";
            sql += " rlendd.ledate1 AS rlendd_ledate1, rlendd.ledate2 AS rlendd_ledate2, rlendd.cono AS rlendd_cono, ";
            sql += " rlendd.cuno AS rlendd_cuno, rlendd.stno AS rlendd_stno, rlendd.emno AS rlendd_emno, rlendd.xa1no AS rlendd_xa1no, ";
            sql += " rlendd.xa1par AS rlendd_xa1par, rlendd.itno AS rlendd_itno, rlendd.itname AS rlendd_itname, rlendd.ittrait AS rlendd_ittrait, ";
            sql += " rlendd.itunit AS rlendd_itunit, rlendd.itpkgqty AS rlendd_itpkgqty, rlendd.qty AS rlendd_qty, rlendd.price AS rlendd_price, ";
            sql += " rlendd.prs AS rlendd_prs, rlendd.rate AS rlendd_rate, rlendd.taxprice AS rlendd_taxprice, rlendd.mny AS rlendd_mny, ";
            sql += " rlendd.priceb AS rlendd_priceb, rlendd.taxpriceb AS rlendd_taxpriceb, rlendd.mnyb AS rlendd_mnyb, ";
            sql += " rlendd.memo AS rlendd_memo, rlendd.lowzero AS rlendd_lowzero, rlendd.bomid AS rlendd_bomid, ";
            sql += " rlendd.bomrec AS rlendd_bomrec, rlendd.recordno AS rlendd_recordno, rlendd.sltflag AS rlendd_sltflag, ";
            sql += " rlendd.extflag AS rlendd_extflag, rlendd.itdesp1 AS rlendd_itdesp1, rlendd.itdesp2 AS rlendd_itdesp2, ";
            sql += " rlendd.itdesp3 AS rlendd_itdesp3, rlendd.itdesp4 AS rlendd_itdesp4, rlendd.itdesp5 AS rlendd_itdesp5, ";
            sql += " rlendd.itdesp6 AS rlendd_itdesp6, rlendd.itdesp7 AS rlendd_itdesp7, rlendd.itdesp8 AS rlendd_itdesp8, ";
            sql += " rlendd.itdesp9 AS rlendd_itdesp9, rlendd.itdesp10 AS rlendd_itdesp10, cust.cuname2 AS cust_cuname2, ";
            sql += " cust.cuaddr1 AS cust_cuaddr1, cust.cuuno AS cust_cuuno, cust.cufax1 AS cust_cufax1,scrit.scname,scrit.scname1 ";
            if (rpt2.Checked || rpt4.Checked)
            {
                sql += " ,rlendbom.leno AS rlendbom_leno, rlendbom.bomid AS rlendbom_bomid, rlendbom.BomRec AS rlendbom_BomRec, ";
                sql += " rlendbom.itno AS rlendbom_itno, rlendbom.itname AS rlendbom_itname, rlendbom.itunit AS rlendbom_itunit, ";
                sql += " rlendbom.itqty AS rlendbom_itqty, rlendbom.itpareprs AS rlendbom_itpareprs, rlendbom.itpkgqty AS rlendbom_itpkgqty, ";
                sql += " rlendbom.itrec AS rlendbom_itrec, rlendbom.itprice AS rlendbom_itprice, rlendbom.itprs AS rlendbom_itprs, ";
                sql += " rlendbom.itmny AS rlendbom_itmny, rlendbom.itnote AS rlendbom_itnote, rlendbom.ItSource AS rlendbom_ItSource, ";
                sql += " rlendbom.ItBuyPri AS rlendbom_ItBuyPri, rlendbom.ItBuyMny AS rlendbom_ItBuyMny";
            }
            sql += " FROM rlendd ";
            sql += " LEFT  JOIN rlend ON rlendd.leno = rlend.leno";
            sql += " LEFT  JOIN cust ON rlend.cuno = cust.cuno "; 
            sql += " LEFT  JOIN scrit ON rlend.appscno = scrit.scname "; 
            if (rpt2.Checked || rpt4.Checked)
            {
                sql += " LEFT  JOIN rlendbom ON rlendd.bomid = rlendbom.bomid";
            }

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(Threetxtend);
            pnlist.Add(Conostart);
            pVar.SaveRadioUdf(pnlist, "RLend");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(Threetxtend);
            pnlist.Add(Conostart);
            pVar.SetRadioUdf(pnlist, "RLend");
        }
    }
}
