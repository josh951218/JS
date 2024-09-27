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
    public partial class FrmDraw_Print : Formbase
    {
        DataTable dt = new DataTable();
        public string PK;
        string ReportFileName = "";
        string ReportPath = "";
        string sql;

        public FrmDraw_Print()
        {
            InitializeComponent();
        }

        private void FrmDraw_Print_Load(object sender, EventArgs e)
        {
            ReportFileName = "倉庫領料作業系統"; 

            radio5.SetUserDefineRpt("倉庫領料作業系統_簡要自定一.rpt");
            radio7.SetUserDefineRpt("倉庫領料作業系統_組件自定一.rpt");
             
            DrNo.Focus();
            DrNo.Text = PK;
            DrNo1.Text = PK;
            SetRdUdf();
        }

        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(DrNo1.Text, DrNo.Text) < 0)
            {
                MessageBox.Show("終止領料單號不可大於起始領料單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                setsql();
                sql += " where 0=0 ";
                if (DrNo.Text != "")
                    sql += " and a.drno >=@drno ";
                if (DrNo1.Text != "")
                    sql += " and a.drno <=@drno1 ";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (DrNo.Text != "") cmd.Parameters.AddWithValue("drno", DrNo.Text.Trim());
                        if (DrNo1.Text != "") cmd.Parameters.AddWithValue("drno1", DrNo1.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            dd.Fill(dt);
                        }
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    DrNo.Focus();
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
                else if (radio7.Checked)
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


                oRpt.SetDataSource(dt);
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

        private void DrNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("key", ((TextBox)sender).Text.Trim());
                    cmd.CommandText = "select count(drno) from draw where drno=@key";
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        e.Cancel = true;
                        using (var frm = new FrmDraw_Print_DrNo())
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

        private void DrNo_DoubleClick(object sender, EventArgs e)
        {

            using (var frm = new FrmDraw_Print_DrNo())
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

        void setsql()
        {
            sql = "";
            sql += "SELECT";
            sql += " a.drno AS Draw_drno, a.drdate1 AS Draw_drdate1, a.drdate2 AS Draw_drdate2, a.cono AS Draw_cono,";
            sql += " a.coname1 AS Draw_coname1, a.coname2 AS Draw_coname2, a.stnoo AS Draw_stnoo, a.stnameo AS Draw_stnameo,";
            sql += " a.stnoi AS Draw_stnoi, a.stnamei AS Draw_stnamei, a.emno AS Draw_emno, a.emname AS Draw_emname,";
            sql += " a.drmemo AS Draw_drmemo, a.bracket AS Draw_bracket, a.recordno AS Draw_recordno, a.UsrNo AS Draw_UsrNo,";
            sql += " a.AppDate AS Draw_AppDate,a.EdtDate AS Draw_EdtDate, a.AppScNo AS Draw_AppScNo, a.EdtScNo AS Draw_EdtScNo,a.drmemo1 AS Draw_drmemo1,";//製單人員+詳細備註
            sql += " a.DrDate AS Draw_DrDate,a.totmnyb AS Draw_totmnyb, b.costb AS Drawd_costb,";
            if (radio3.Checked)
            {
                sql += " c.DrNo AS DrawBom_DrNo,c.BomID AS DrawBom_BomID,c.BomRec AS DrawBom_BomRec, c.itno AS DrawBom_itno, c.itname AS DrawBom_itname,";
                sql += " c.itunit AS DrawBom_itunit, c.itqty AS DrawBom_itqty, c.itpareprs AS DrawBom_itpareprs,";
                sql += " c.itpkgqty AS DrawBom_itpkgqty, c.itrec AS DrawBom_itrec, c.itprice AS DrawBom_itprice, c.itprs AS DrawBom_itprs,";
                sql += " c.itmny AS DrawBom_itmny, c.itnote AS DrawBom_itnote, c.ItSource AS DrawBom_ItSource,";
                sql += " c.ItBuyPri AS DrawBom_ItBuyPri, c.ItBuyMny AS DrawBom_ItBuyMny,";
            }
            sql += " b.mnyb AS Drawd_mnyb,b.drno AS Drawd_drno,b.drdate1 AS Drawd_drdate1, b.drdate2 AS Drawd_drdate2, b.cono AS Drawd_cono, b.stnoo AS Drawd_stnoo,";
            sql += " b.stnoi AS Drawd_stnoi, b.emno AS Drawd_emno, b.itno AS Drawd_itno, b.itname AS Drawd_itname,";
            sql += " b.ittrait AS Drawd_ittrait, b.itunit AS Drawd_itunit, b.itpkgqty AS Drawd_itpkgqty, b.qty AS Drawd_qty,";
            sql += " b.memo AS Drawd_memo, b.lowzero AS Drawd_lowzero, b.bomid AS Drawd_bomid, b.bomrec AS Drawd_bomrec,";
            sql += " b.recordno AS Drawd_recordno, b.sltflag AS Drawd_sltflag, b.extflag AS Drawd_extflag, b.bracket AS Drawd_bracket,";
            sql += " b.itdesp1 AS Drawd_itdesp1, b.itdesp2 AS Drawd_itdesp2, b.itdesp3 AS Drawd_itdesp3, b.itdesp4 AS Drawd_itdesp4,";
            sql += " b.itdesp5 AS Drawd_itdesp5, b.itdesp6 AS Drawd_itdesp6, b.itdesp7 AS Drawd_itdesp7, b.itdesp8 AS Drawd_itdesp8,";
            sql += " b.itdesp9 AS Drawd_itdesp9, b.itdesp10 AS Drawd_itdesp10, b.stNameo AS Drawd_stNameo,";
            sql += " b.stNamei AS Drawd_stNamei, b.DrDate AS Drawd_DrDate,scrit.scname,scrit.scname1 ";
            sql += " FROM";
            sql += " draw AS a LEFT  JOIN";
            sql += @" drawd AS b ON b.drno = a.drno 
                        Left join scrit on a.appscno = scrit.scname ";
            if (radio3.Checked)
            {
                sql += " LEFT  JOIN DrawBom AS c ON c.bomid = b.bomid ";
            }
        }
  
        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SaveRadioUdf(pnlist, "Draw");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SetRadioUdf(pnlist, "Draw");
        }




    }
}
