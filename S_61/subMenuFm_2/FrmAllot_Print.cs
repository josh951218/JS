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

namespace S_61.subMenuFm_2
{
    public partial class FrmAllot_Print : Formbase
    {
        DataSet ds = new DataSet();
        public string PK;
        string ReportFileName = "";
        string ReportPath = "";
        string sql;

        public FrmAllot_Print()
        {
            InitializeComponent();
        }

        private void FrmAllot_Print_Load(object sender, EventArgs e)
        {
            ReportFileName = "倉庫調撥作業系統";

            radio5.SetUserDefineRpt("倉庫調撥作業系統_簡要自定一.rpt");
            radio7.SetUserDefineRpt("倉庫調撥作業系統_組件自定一.rpt");

            AlNo.Focus();
            AlNo.Text = PK;
            AlNo1.Text = PK;
            SetRdUdf();
        }

        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(AlNo1.Text, AlNo.Text) < 0)
            {
                MessageBox.Show("終止調撥單號不可大於起始調撥單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ds.Clear();
            try
            {
                setsql();
                sql += " where '0'='0'";
                if (AlNo.Text != "")
                    sql += " and allot.alno >=@alno";
                if (AlNo1.Text != "")
                    sql += " and allot.alno <=@alno1";

                sql += " order by allotd.alno,allotd.recordno";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    if (AlNo.Text != "") cmd.Parameters.AddWithValue("alno", AlNo.Text.Trim());
                    if (AlNo1.Text != "") cmd.Parameters.AddWithValue("alno1", AlNo1.Text.Trim());

                    cmd.CommandText = sql;
                    dd.Fill(ds, "allot");

                    cmd.CommandText = @"
                            SELECT 
                            itno, itnoudf, itname, kino, itime, ittrait, itunit, itunitp, itpkgqty, itnw, itnwunit, itdesp1, itdesp2, itdesp3, itdesp4, 
                            itdesp5, itdesp6, itdesp7, itdesp8, itdesp9, itdesp10, itnote, itudf1, itudf2, itudf3, itudf4, itudf5, fano, Punit, ScNo
                            FROM item ";
                    dd.Fill(ds, "item");
                }

                if (ds.Tables["allot"].Rows.Count == 0)
                {
                    AlNo.Focus();
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReportDocument oRpt = new ReportDocument();
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName;

                if (radio1.Checked)
                {
                    ReportPath += "_簡要表.rpt";
                    if (File.Exists(ReportPath))
                        oRpt.Load(ReportPath);
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (radio2.Checked)
                {
                    ReportPath += "_規格說明.rpt";
                    if (File.Exists(ReportPath))
                        oRpt.Load(ReportPath);
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (radio3.Checked)
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
                else if (radio5.Checked)
                {
                    ReportPath += "_簡要自定一.rpt";
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
                    ReportPath += "_組件自定一.rpt";
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
                    if (rdHeader1.Checked) myFieldTitleName.Text = Common.dtstart.Rows[0]["pnname"].ToString();
                    else if (rdHeader2.Checked) myFieldTitleName.Text = Common.dtstart.Rows[1]["pnname"].ToString();
                    else if (rdHeader3.Checked) myFieldTitleName.Text = Common.dtstart.Rows[2]["pnname"].ToString();
                    else if (rdHeader4.Checked) myFieldTitleName.Text = Common.dtstart.Rows[3]["pnname"].ToString();
                    else if (rdHeader5.Checked) myFieldTitleName.Text = Common.dtstart.Rows[4]["pnname"].ToString();
                    else myFieldTitleName.Text = "";
                }
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    //三行註腳
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (rdFooter1.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                    else if (rdFooter2.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                    else if (rdFooter3.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                    else if (rdFooter4.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                    else if (rdFooter5.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
                if (Txt.Find(t => t.Name == "txtadress") != null)
                {
                    //表頭-公司住址
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtadress"];
                    if (rdHeader1.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[0]["pnaddr"].ToString();
                    else if (rdHeader2.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[1]["pnaddr"].ToString();
                    else if (rdHeader3.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[2]["pnaddr"].ToString();
                    else if (rdHeader4.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[3]["pnaddr"].ToString();
                    else if (rdHeader5.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[4]["pnaddr"].ToString();
                    else myFieldTitleName.Text = "";
                }
                if (Txt.Find(t => t.Name == "txttel") != null)
                {
                    //表頭-電話、傳真
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txttel"];
                    if (rdHeader1.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
                    else if (rdHeader2.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
                    else if (rdHeader3.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
                    else if (rdHeader4.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
                    else if (rdHeader5.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
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

                //報表參數設定
                if (num.Find(p => p.Name == "顯示千分位") != null)
                {
                    if (pVar.TRS != "")
                        pVar.ShowTRS = true;
                    oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
                }
                if (num.Find(p => p.Name == "千分位") != null)
                    oRpt.SetParameterValue("千分位", pVar.TRS);
                if (num.Find(p => p.Name == "進貨單價小數") != null)
                    oRpt.SetParameterValue("進貨單價小數", Common.MF);
                if (num.Find(p => p.Name == "銷貨單價小數") != null)
                    oRpt.SetParameterValue("銷貨單價小數", Common.MS);
                if (num.Find(p => p.Name == "銷貨單據小數") != null)
                    oRpt.SetParameterValue("銷貨單據小數", Common.MST);
                if (num.Find(p => p.Name == "進貨單據小數") != null)
                    oRpt.SetParameterValue("進貨單據小數", Common.MFT);
                if (num.Find(p => p.Name == "銷項稅額小數") != null)
                    oRpt.SetParameterValue("銷項稅額小數", Common.TS);
                if (num.Find(p => p.Name == "進項稅額小數") != null)
                    oRpt.SetParameterValue("進項稅額小數", Common.TF);
                if (num.Find(p => p.Name == "進項金額小數") != null)
                    oRpt.SetParameterValue("進項金額小數", Common.TPF);
                if (num.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);
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
            this.Close();
            this.Dispose();
        }

        private void AlNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmAllot_Print_AlNo())
            { 
                frm.TSeekNo = ((TextBox)sender).Text;

                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        ((TextBox)sender).Text = frm.TResult;
                        break; 
                }
            }
        }

        private void AlNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("alno", ((TextBox)sender).Text.Trim());
                    cmd.CommandText = "select count(alno) from allot where alno=@alno";
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        e.Cancel = true;
                        using (var frm = new FrmAllot_Print_AlNo())
                        { 
                            frm.TSeekNo = ((TextBox)sender).Text;

                            switch (frm.ShowDialog())
                            {
                                case DialogResult.OK:
                                    ((TextBox)sender).Text = frm.TResult;
                                    break; 
                            }
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void setsql()
        {
            sql = "";
            sql += "SELECT allot.alno AS allot_alno, allot.aldate AS allot_aldate, allot.aldate1 AS allot_aldate1, allot.aldate2 AS allot_aldate2, ";
            sql += "allot.cono AS allot_cono, allot.coname1 AS allot_coname1, allot.coname2 AS allot_coname2, ";
            sql += "allot.stnoo AS allot_stnoo, allot.stnameo AS allot_stnameo, allot.stnoi AS allot_stnoi, allot.stnamei AS allot_stnamei, ";
            sql += "allot.emno AS allot_emno, allot.emname AS allot_emname, allot.almemo AS allot_almemo, ";
            sql += "allot.bracket AS allot_bracket, allot.recordno AS allot_recordno, allot.stname AS allot_stname, ";
            sql += "allot.AppDate AS allot_AppDate,allot.EdtDate AS allot_EdtDate, allot.AppScNo AS allot_AppScNo, allot.EdtScNo AS allot_EdtScNo,allot.almemo1 AS allot_almemo1,";//製單人員+詳細備註
            sql += "allot.UsrNo AS allot_UsrNo, ";
            if (radio3.Checked)
            {
                sql += "AlloBom.AlNo AS AlloBom_AlNo, AlloBom.BomID AS AlloBom_BomID, ";
                sql += "AlloBom.BomRec AS AlloBom_BomRec, AlloBom.itno AS AlloBom_itno, AlloBom.itname AS AlloBom_itname, ";
                sql += "AlloBom.itunit AS AlloBom_itunit, AlloBom.itqty AS AlloBom_itqty, AlloBom.itpareprs AS AlloBom_itpareprs, ";
                sql += "AlloBom.itpkgqty AS AlloBom_itpkgqty, AlloBom.itrec AS AlloBom_itrec, AlloBom.itprice AS AlloBom_itprice, ";
                sql += "AlloBom.itprs AS AlloBom_itprs, AlloBom.itmny AS AlloBom_itmny, AlloBom.itnote AS AlloBom_itnote, ";
                sql += "AlloBom.ItSource AS AlloBom_ItSource, AlloBom.ItBuyPri AS AlloBom_ItBuyPri, ";
                sql += "AlloBom.ItBuyMny AS AlloBom_ItBuyMny,";
            }
            sql += "allotd.alno AS allotd_alno, allotd.aldate AS allotd_aldate, ";
            sql += "allotd.aldate1 AS allotd_aldate1, allotd.aldate2 AS allotd_aldate2, allotd.cono AS allotd_cono, ";
            sql += "allotd.stnoo AS allotd_stnoo, allotd.stnoi AS allotd_stnoi, allotd.emno AS allotd_emno, allotd.itno AS allotd_itno, ";
            sql += "allotd.itname AS allotd_itname, allotd.ittrait AS allotd_ittrait, allotd.itunit AS allotd_itunit, ";
            sql += "allotd.itpkgqty AS allotd_itpkgqty, allotd.qty AS allotd_qty, allotd.memo AS allotd_memo, ";
            sql += "allotd.lowzero AS allotd_lowzero, allotd.bomid AS allotd_bomid, allotd.bomrec AS allotd_bomrec, ";
            sql += "allotd.recordno AS allotd_recordno, allotd.sltflag AS allotd_sltflag, allotd.extflag AS allotd_extflag, ";
            sql += "allotd.bracket AS allotd_bracket, allotd.itdesp1 AS allotd_itdesp1, allotd.itdesp2 AS allotd_itdesp2, ";
            sql += "allotd.itdesp3 AS allotd_itdesp3, allotd.itdesp4 AS allotd_itdesp4, allotd.itdesp5 AS allotd_itdesp5, ";
            sql += "allotd.itdesp6 AS allotd_itdesp6, allotd.itdesp7 AS allotd_itdesp7, allotd.itdesp8 AS allotd_itdesp8, ";
            sql += "allotd.itdesp9 AS allotd_itdesp9, allotd.itdesp10 AS allotd_itdesp10,scrit.scname,scrit.scname1 ";
            sql += "FROM allotd LEFT OUTER JOIN ";
            if (radio3.Checked)
            {
                sql += "AlloBom ON allotd.bomid = AlloBom.bomid LEFT OUTER JOIN ";
            }
            sql += @" allot ON allotd.alno = allot.alno
                   Left join scrit on allot.appscno = scrit.scname";
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SaveRadioUdf(pnlist, "Allot");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SetRadioUdf(pnlist, "Allot");
        }
    }
}
