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
    public partial class FrmLend_PrintNew : Formbase
    {
        JBS.JS.xEvents xe;
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        public string PK = "";
        string ReportFileName = "";
        string ReportPath = "";
        string sql;
     

        DataSet ds = new DataSet();

        public FrmLend_PrintNew()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmLend_PrintNew_Load(object sender, EventArgs e)
        {
            ReportFileName = "倉庫借出作業系統";
            rpt3.SetUserDefineRpt("倉庫借出作業系統_簡要自定.rpt");
            rpt4.SetUserDefineRpt("倉庫借出作業系統_組件自定.rpt");

            LeNo.Focus();
            LeNo.Text = this.PK;
            LeNo1.Text = this.PK;

            SetRdUdf();
        }

        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(LeNo1.Text, LeNo.Text) < 0)
            {
                MessageBox.Show("終止借出單號不可小於起始借出單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ds.Clear();
            try
            {
                setsql();
                sql += " where '0'='0'";
                if (LeNo.Text != "")
                    sql += " and lendd.leno >=@leno ";
                if (LeNo1.Text != "")
                    sql += " and lendd.leno <=@leno1 ";
                 

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cn.Open();

                    if (LeNo.Text != "") cmd.Parameters.AddWithValue("leno", LeNo.Text);
                    if (LeNo1.Text != "") cmd.Parameters.AddWithValue("leno1", LeNo1.Text);

                    cmd.CommandText = sql + " order by lendd.leno,lendd.recordno ";
                    dd.Fill(ds, "lend");

                    cmd.CommandText = @"
                        SELECT 
                        itno, itnoudf, itname, kino, itime, ittrait, itunit, itunitp, itpkgqty, itnw, itnwunit, itdesp1, itdesp2, itdesp3, itdesp4, 
                        itdesp5, itdesp6, itdesp7, itdesp8, itdesp9, itdesp10, itnote, itudf1, itudf2, itudf3, itudf4, itudf5, fano, Punit, ScNo
                        FROM item ";
                    dd.Fill(ds, "item");

                    cmd.CommandText = " SELECT cuno, cuemno1, cuname2, cuname1,cur1,cuaddr1,cur2,cuaddr2,cur3,cuaddr3 FROM cust ";
                    dd.Fill(ds, "cust");

                    cmd.CommandText = "SELECT emno, emname FROM empl";
                    dd.Fill(ds, "empl");
                }

                if (ds.Tables["lend"].Rows.Count == 0)
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
                        var ph = Common.reportaddress + "Report\\LendRpt1.rpt";
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
            xe.Open<JBS.JS.Lend>(sender); 
        }

        private void LeNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) 
                return;

            xe.ValidateOpen<JBS.JS.Lend>(sender, e, row => { }); 
        }

        void setsql()
        {
            sql = "";
            sql += " SELECT lend.leno AS lend_leno, lend.ledate AS lend_ledate, lend.ledate1 AS lend_ledate1, lend.ledate2 AS lend_ledate2, ";
            sql += " lend.cono AS lend_cono, lend.coname1 AS lend_coname1, lend.coname2 AS lend_coname2, lend.cuno AS lend_cuno, ";
            sql += " lend.cuname1 AS lend_cuname1, lend.cuname2 AS lend_cuname2, lend.cutel AS lend_cutel, ";
            sql += " lend.cuper1 AS lend_cuper1, lend.stno AS lend_stno, lend.stname AS lend_stname, lend.emno AS lend_emno, ";
            sql += " lend.emname AS lend_emname, lend.xa1no AS lend_xa1no, lend.xa1name AS lend_xa1name, ";
            sql += " lend.xa1par AS lend_xa1par, lend.taxmnyf AS lend_taxmnyf, lend.taxmnyb AS lend_taxmnyb, ";
            sql += " lend.taxmny AS lend_taxmny, lend.x3no AS lend_x3no, lend.rate AS lend_rate, lend.tax AS lend_tax, ";
            sql += " lend.totmny AS lend_totmny, lend.taxb AS lend_taxb, lend.totmnyb AS lend_totmnyb, lend.lememo AS lend_lememo, lend.lememo1 AS lend_lememo1, ";
            sql += " lend.recordno AS lend_recordno,  lendd.ledate AS lendd_ledate, lendd.leno AS lendd_leno, ";
            sql += " lendd.ledate1 AS lendd_ledate1, lendd.ledate2 AS lendd_ledate2, lendd.cono AS lendd_cono, ";
            sql += " lendd.cuno AS lendd_cuno, lendd.stno AS lendd_stno, lendd.emno AS lendd_emno, lendd.xa1no AS lendd_xa1no, ";
            sql += " lendd.xa1par AS lendd_xa1par, lendd.itno AS lendd_itno, lendd.itname AS lendd_itname, lendd.ittrait AS lendd_ittrait, ";
            sql += " lendd.itunit AS lendd_itunit, lendd.itpkgqty AS lendd_itpkgqty, lendd.qty AS lendd_qty, lendd.price AS lendd_price, ";
            sql += " lendd.prs AS lendd_prs, lendd.rate AS lendd_rate, lendd.taxprice AS lendd_taxprice, lendd.mny AS lendd_mny, ";
            sql += " lendd.priceb AS lendd_priceb, lendd.taxpriceb AS lendd_taxpriceb, lendd.mnyb AS lendd_mnyb, ";
            sql += " lendd.memo AS lendd_memo, lendd.lowzero AS lendd_lowzero, lendd.bomid AS lendd_bomid, ";
            sql += " lendd.bomrec AS lendd_bomrec, lendd.recordno AS lendd_recordno, lendd.sltflag AS lendd_sltflag, ";
            sql += " lendd.extflag AS lendd_extflag, lendd.itdesp1 AS lendd_itdesp1, lendd.itdesp2 AS lendd_itdesp2, ";
            sql += " lendd.itdesp3 AS lendd_itdesp3, lendd.itdesp4 AS lendd_itdesp4, lendd.itdesp5 AS lendd_itdesp5, ";
            sql += " lendd.itdesp6 AS lendd_itdesp6, lendd.itdesp7 AS lendd_itdesp7, lendd.itdesp8 AS lendd_itdesp8, ";
            sql += " lendd.itdesp9 AS lendd_itdesp9, lendd.itdesp10 AS lendd_itdesp10, cust.cuname2 AS cust_cuname2, ";
            sql += " cust.cuaddr1 AS cust_cuaddr1, cust.cuuno AS cust_cuuno, cust.cufax1 AS cust_cufax1,scrit.scname,scrit.scname1 ";
            if (rpt2.Checked || rpt4.Checked)
            {
                sql += " ,lendbom.leno AS lendbom_leno, lendbom.bomid AS lendbom_bomid, lendbom.BomRec AS lendbom_BomRec, ";
                sql += " lendbom.itno AS lendbom_itno, lendbom.itname AS lendbom_itname, lendbom.itunit AS lendbom_itunit, ";
                sql += " lendbom.itqty AS lendbom_itqty, lendbom.itpareprs AS lendbom_itpareprs, lendbom.itpkgqty AS lendbom_itpkgqty, ";
                sql += " lendbom.itrec AS lendbom_itrec, lendbom.itprice AS lendbom_itprice, lendbom.itprs AS lendbom_itprs, ";
                sql += " lendbom.itmny AS lendbom_itmny, lendbom.itnote AS lendbom_itnote, lendbom.ItSource AS lendbom_ItSource, ";
                sql += " lendbom.ItBuyPri AS lendbom_ItBuyPri, lendbom.ItBuyMny AS lendbom_ItBuyMny";
            }
            sql += " FROM lendd ";
            sql += " LEFT  JOIN lend ON lendd.leno = lend.leno";
            sql += " LEFT  JOIN cust ON lend.cuno = cust.cuno "; 
            sql += " LEFT  JOIN scrit  ON lend.appscno = scrit.scname "; 
            if (rpt2.Checked || rpt4.Checked)
            {
                sql += " LEFT  JOIN lendbom ON lendd.bomid = lendbom.bomid";
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(Threetxtend);
            pnlist.Add(Conostart);
            pVar.SaveRadioUdf(pnlist, "Lend");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(Threetxtend);
            pnlist.Add(Conostart);
            pVar.SetRadioUdf(pnlist, "Lend");
        }
    }
}
