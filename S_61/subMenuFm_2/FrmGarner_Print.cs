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
    public partial class FrmGarner_Print : Formbase
    { 
        DataTable dt = new DataTable();
        public string PK;
        string ReportFileName = "";
        string ReportPath = "";
        string sql;

        public FrmGarner_Print()
        {
            InitializeComponent();
        }

        private void FrmGarner_Print_Load(object sender, EventArgs e)
        {
            ReportFileName = "倉庫入庫作業系統";
            radio5.SetUserDefineRpt("倉庫入庫作業系統_簡要自定一.rpt");
            radio7.SetUserDefineRpt("倉庫入庫作業系統_組件自定一.rpt"); 

            GaNo.Text = PK;
            GaNo1.Text = PK;
            SetRdUdf();
        }

        private void FrmGarner_Print_Shown(object sender, EventArgs e)
        {
            GaNo.Focus(); 
        }

        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(GaNo1.Text, GaNo.Text) < 0)
            {
                MessageBox.Show("終止入庫單號不可大於起始入庫單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                setsql();
                sql += " where '0'='0'";
                if (GaNo.Text != "")
                    sql += " and a.gano >=@gano";
                if (GaNo1.Text != "")
                    sql += " and a.gano <=@gano1";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (GaNo.Text != "") cmd.Parameters.AddWithValue("gano", GaNo.Text.Trim());
                        if (GaNo1.Text != "") cmd.Parameters.AddWithValue("gano1", GaNo1.Text.Trim());
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
                    GaNo.Focus();
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
                else
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

        private void GaNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmGarner_Print_GaNo())
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

        private void GaNo_Validating(object sender, CancelEventArgs e)
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
                    cmd.CommandText = "select count(gano) from garner where gano=@key";
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        e.Cancel = true;
                        using (var frm = new FrmGarner_Print_GaNo())
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
            sql += " SELECT ";
            sql += " a.gano AS Garner_gano, a.gadate AS Garner_gadate, a.gadate1 AS Garner_gadate1, a.gadate2 AS Garner_gadate2,";
            sql += " a.cono AS Garner_cono, a.coname1 AS Garner_coname1, a.coname2 AS Garner_coname2, a.stnoo AS Garner_stnoo,";
            sql += " a.stnameo AS Garner_stnameo, a.stnoi AS Garner_stnoi, a.stnamei AS Garner_stnamei, a.orno AS Garner_orno, ";
            sql += " a.emno AS Garner_emno, a.emname AS Garner_emname, a.totmnyb AS Garner_totmnyb, ";
            sql += " a.AppDate AS Garner_AppDate,a.EdtDate AS Garner_EdtDate, a.AppScNo AS Garner_AppScNo, a.EdtScNo AS Garner_EdtScNo,";//製單人員
            sql += " a.costflag AS Garner_costflag, a.gamemo AS Garner_gamemo, a.gamemo1 AS Garner_gamemo1, a.bracket AS Garner_bracket,";//詳細備註
            sql += " a.recordno AS Garner_recordno, a.UsrNo AS Garner_UsrNo, b.gano AS Garnerd_gano, b.gadate AS Garnerd_gadate,";
            sql += " b.gadate2 AS Garnerd_gadate2, b.gadate1 AS Garnerd_gadate1, b.cono AS Garnerd_cono, b.stnoo AS Garnerd_stnoo,";
            sql += " b.stnoi AS Garnerd_stnoi, b.orno AS Garnerd_orno, b.emno AS Garnerd_emno, b.costflag AS Garnerd_costflag,";
            sql += " b.itno AS Garnerd_itno, b.itname AS Garnerd_itname, b.ittrait AS Garnerd_ittrait, b.itunit AS Garnerd_itunit,";
            sql += " b.itpkgqty AS Garnerd_itpkgqty, b.qty AS Garnerd_qty, b.costb AS Garnerd_costb, b.costb1 AS Garnerd_costb1,";
            sql += " b.mnyb AS Garnerd_mnyb, b.memo AS Garnerd_memo, b.lowzero AS Garnerd_lowzero, b.bomid AS Garnerd_bomid, ";
            sql += " b.bomrec AS Garnerd_bomrec, b.recordno AS Garnerd_recordno, b.sltflag AS Garnerd_sltflag, ";
            sql += " b.extflag AS Garnerd_extflag, b.bracket AS Garnerd_bracket, b.itdesp1 AS Garnerd_itdesp1,";
            sql += " b.itdesp2 AS Garnerd_itdesp2, b.itdesp3 AS Garnerd_itdesp3, b.itdesp4 AS Garnerd_itdesp4,";
            sql += " b.itdesp5 AS Garnerd_itdesp5, b.itdesp6 AS Garnerd_itdesp6, b.itdesp7 AS Garnerd_itdesp7, ";
            sql += " b.itdesp8 AS Garnerd_itdesp8, b.itdesp9 AS Garnerd_itdesp9, b.itdesp10 AS Garnerd_itdesp10,b.stName AS Garnerd_stName,scrit.scname,scrit.scname1 ";
            if (radio3.Checked)
            {
                sql += " ,c.GaNo AS GarnBom_GaNo, c.BomID AS GarnBom_BomID, ";
                sql += " c.BomRec AS GarnBom_BomRec, c.itno AS GarnBom_itno, c.itname AS GarnBom_itname, c.itunit AS GarnBom_itunit, ";
                sql += " c.itqty AS GarnBom_itqty, c.itpareprs AS GarnBom_itpareprs, c.itpkgqty AS GarnBom_itpkgqty, ";
                sql += " c.itrec AS GarnBom_itrec, c.itprice AS GarnBom_itprice, c.itprs AS GarnBom_itprs, c.itmny AS GarnBom_itmny,";
                sql += " c.itnote AS GarnBom_itnote, c.ItSource AS GarnBom_ItSource, c.ItBuyPri AS GarnBom_ItBuyPri, ";
                sql += " c.ItBuyMny AS GarnBom_ItBuyMny";
            }
            sql += @" from garner AS a LEFT JOIN garnerd AS b ON a.gano = b.gano
                 Left join scrit on a.appscno = scrit.scname ";
            if (radio3.Checked)
            {
                sql += " LEFT JOIN GarnBom AS c ON b.BomID = c.BomID";
            }
        }
         
        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SaveRadioUdf(pnlist, "Garner");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SetRadioUdf(pnlist, "Garner");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            this.Close();
            this.Dispose();
        }

        private void btnBar_Click(object sender, EventArgs e)
        {
            //FastReport 印條碼
            DataTable t = new DataTable();
            string str_where ="";
            parameters par = new parameters();
            if (GaNo.Text.Trim().Length > 0)
            {
                str_where += "  and gano >=@gano1";
                par.AddWithValue("gano1",GaNo.Text);
            }
            if (GaNo1.Text.Trim().Length > 0)
            {
                str_where += " and gano <=@gano2";
                par.AddWithValue("gano2", GaNo1.Text);
            }
            string str = @"select 列印 = 'V',製造商編號 = c.Fano,製造商簡稱 = (select top 1 faname1 from fact where fano = c.Fano),批次號碼 = c.Batchno,a.itno,item.itnoudf, a.itname, a.itunit ,張數 = b.qty
,有效日期 = c.[Date]
,製造日期 = c.Date1
,kind.kino,類別名稱 = kind.kiname from 
(select * from garnerd  where  0=0  " + str_where + @" ) as a
inner join 
BatchProcess_Garnerd as b on a.bomid =  b.bomid inner join
BatchInformation as c on b.Bno = c.Bno
left join item on a.itno = item.itno
left join kind on item.kino = kind.kino
order by a.bomid";
            SQL.ExecuteNonQuery(str, par, t);

            using (Batch F = new Batch("a", new DataTable()))
            {
               for (int i = 0; i < t.Rows.Count; i++)
               {
                   t.Rows[i]["有效日期"] = Date.AddLine(F.轉換日期(t.Rows[i]["有效日期"].ToString()));
                   t.Rows[i]["製造日期"] = Date.AddLine(F.轉換日期(t.Rows[i]["製造日期"].ToString()));
               }
            }

            if (t.Rows.Count > 0)
            {
                using (var frm = new S0.FrmPrintBarCodeb(BarCodePrintMode.BatchNo))
                {
                    frm.dt = t.Copy();
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}
