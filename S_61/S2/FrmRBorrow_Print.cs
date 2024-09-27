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
    public partial class FrmRBorrow_Print : Formbase
    {
        DataSet ds = new DataSet();
        public string PK = "";
        string ReportFileName = "";
        string ReportPath = "";
        string sql;
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        public FrmRBorrow_Print()
        {
            InitializeComponent();
        }

        private void FrmBorrow_Print_Load(object sender, EventArgs e)
        {
            ReportFileName = "倉庫借入還出作業系統";
            rpt3.SetUserDefineRpt("倉庫借入還出作業系統_簡要自定.rpt");
            rpt4.SetUserDefineRpt("倉庫借入還出作業系統_組件自定.rpt");

            BoNo.Focus();
            BoNo.Text = this.PK;
            BoNo1.Text = this.PK;

            SetRdUdf();
        }

        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(BoNo1.Text, BoNo.Text) < 0)
            {
                MessageBox.Show("終止還出單號不可小於起始還出單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                setsql();
                sql += " where '0'='0'";
                if (BoNo.Text != "")
                    sql += " and rborrd.bono >=@bono ";
                if (BoNo1.Text != "")
                    sql += " and rborrd.bono <=@bono1 ";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        if (BoNo.Text != "") cmd.Parameters.AddWithValue("bono", BoNo.Text);
                        if (BoNo1.Text != "") cmd.Parameters.AddWithValue("bono1", BoNo1.Text);

                        sql += " order by rborrd.bono,rborrd.recordno ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            ds.Clear();
                            dd.Fill(ds);
                        }
                    }
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    BoNo.Focus();
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReportDocument oRpt = new ReportDocument();
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName;

                if (rpt1.Checked)
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


                oRpt.SetDataSource(ds.Tables[0]);
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
            this.Dispose();
            this.Close();
        }

        private void LeNo_DoubleClick(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            using (var frm = new FrmBorrow_Print_BoNo())
            {
                frm.TSeekNo = tb.Text.Trim();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    tb.Text = frm.TResult;
                    tb.SelectAll();
                }
            }
        }

        private void LeNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("bono", ((TextBox)sender).Text.Trim());
                    cmd.CommandText = "select count(bono) from rborr where bono=@bono";
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        e.Cancel = true;
                        using (var frm = new FrmBorrow_Print_BoNo())
                        {
                            frm.TSeekNo = ((TextBox)sender).Text;
                            switch (frm.ShowDialog())
                            {
                                case DialogResult.OK:
                                    ((TextBox)sender).Text = frm.TResult;
                                    break;
                                case DialogResult.Cancel:
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
            sql += " SELECT rborr.bono AS rborr_bono, rborr.bodate AS rborr_bodate, rborr.bodate1 AS rborr_bodate1, rborr.bodate2 AS rborr_bodate2, ";
            sql += " rborr.cono AS rborr_cono, rborr.coname1 AS rborr_coname1, rborr.coname2 AS rborr_coname2, rborr.fano AS rborr_fano, ";
            sql += " rborr.faname1 AS rborr_faname1, rborr.faname2 AS rborr_faname2, rborr.fatel1 AS rborr_fatel1, ";
            sql += " rborr.faper1 AS rborr_faper1, rborr.stno AS rborr_stno, rborr.stname AS rborr_stname, rborr.emno AS rborr_emno, ";
            sql += " rborr.emname AS rborr_emname, rborr.xa1no AS rborr_xa1no, rborr.xa1name AS rborr_xa1name, ";
            sql += " rborr.xa1par AS rborr_xa1par, rborr.taxmnyf AS rborr_taxmnyf, rborr.taxmnyb AS rborr_taxmnyb, ";
            sql += " rborr.taxmny AS rborr_taxmny, rborr.x3no AS rborr_x3no, rborr.rate AS rborr_rate, rborr.tax AS rborr_tax, ";
            sql += " rborr.totmny AS rborr_totmny, rborr.taxb AS rborr_taxb, rborr.totmnyb AS rborr_totmnyb, rborr.bomemo AS rborr_bomemo, rborr.bomemo1 AS rborr_bomemo1,";
            sql += " rborr.recordno AS rborr_recordno,  rborrd.bodate AS rborrd_bodate, rborrd.bono AS rborrd_bono, ";
            sql += " rborrd.bodate1 AS rborrd_bodate1, rborrd.bodate2 AS rborrd_bodate2, rborrd.cono AS rborrd_cono, ";
            sql += " rborrd.fano AS rborrd_fano, rborrd.stno AS rborrd_stno, rborrd.emno AS rborrd_emno, rborrd.xa1no AS rborrd_xa1no, ";
            sql += " rborrd.xa1par AS rborrd_xa1par, rborrd.itno AS rborrd_itno, rborrd.itname AS rborrd_itname, rborrd.ittrait AS rborrd_ittrait, ";
            sql += " rborrd.itunit AS rborrd_itunit, rborrd.itpkgqty AS rborrd_itpkgqty, rborrd.qty AS rborrd_qty, rborrd.price AS rborrd_price, ";
            sql += " rborrd.prs AS rborrd_prs, rborrd.rate AS rborrd_rate, rborrd.taxprice AS rborrd_taxprice, rborrd.mny AS rborrd_mny, ";
            sql += " rborrd.priceb AS rborrd_priceb, rborrd.taxpriceb AS rborrd_taxpriceb, rborrd.mnyb AS rborrd_mnyb, ";
            sql += " rborrd.memo AS rborrd_memo, rborrd.lowzero AS rborrd_lowzero, rborrd.bomid AS rborrd_bomid, ";
            sql += " rborrd.bomrec AS rborrd_bomrec, rborrd.recordno AS rborrd_recordno, rborrd.sltflag AS rborrd_sltflag, ";
            sql += " rborrd.extflag AS rborrd_extflag, rborrd.itdesp1 AS rborrd_itdesp1, rborrd.itdesp2 AS rborrd_itdesp2, ";
            sql += " rborrd.itdesp3 AS rborrd_itdesp3, rborrd.itdesp4 AS rborrd_itdesp4, rborrd.itdesp5 AS rborrd_itdesp5, ";
            sql += " rborrd.itdesp6 AS rborrd_itdesp6, rborrd.itdesp7 AS rborrd_itdesp7, rborrd.itdesp8 AS rborrd_itdesp8, ";
            sql += " rborrd.itdesp9 AS rborrd_itdesp9, rborrd.itdesp10 AS rborrd_itdesp10, fact.faname2 AS fact_faname2, ";
            sql += " fact.faaddr1 AS fact_faaddr1, fact.fauno AS fact_fauno, fact.fafax1 AS fact_fafax1,fact.fatel1 AS fact_fatel1 ";
            if (rpt2.Checked || rpt4.Checked)
            {
                sql += " ,rborrbom.bono AS rborrbom_bono, rborrbom.bomid AS rborrbom_bomid, rborrbom.BomRec AS rborrbom_BomRec, ";
                sql += " rborrbom.itno AS rborrbom_itno, rborrbom.itname AS rborrbom_itname, rborrbom.itunit AS rborrbom_itunit, ";
                sql += " rborrbom.itqty AS rborrbom_itqty, rborrbom.itpareprs AS rborrbom_itpareprs, rborrbom.itpkgqty AS rborrbom_itpkgqty, ";
                sql += " rborrbom.itrec AS rborrbom_itrec, rborrbom.itprice AS rborrbom_itprice, rborrbom.itprs AS rborrbom_itprs, ";
                sql += " rborrbom.itmny AS rborrbom_itmny, rborrbom.itnote AS rborrbom_itnote, rborrbom.ItSource AS rborrbom_ItSource, ";
                sql += " rborrbom.ItBuyPri AS rborrbom_ItBuyPri, rborrbom.ItBuyMny AS rborrbom_ItBuyMny";
            }
            sql += " FROM rborr LEFT  JOIN";
            sql += " fact ON rborr.fano = fact.fano LEFT  JOIN ";
            sql += " rborrd ON rborr.bono = rborrd.bono";
            if (rpt2.Checked || rpt4.Checked)
            {
                sql += " LEFT  JOIN rborrbom ON rborrd.bomid = rborrbom.bomid";
            }

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(Threetxtend);
            pnlist.Add(Conostart);
            pVar.SaveRadioUdf(pnlist, "RBorr");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(Threetxtend);
            pnlist.Add(Conostart);
            pVar.SetRadioUdf(pnlist, "RBorr");
        }

    }
}
