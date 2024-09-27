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
    public partial class FrmBorrow_Print : Formbase
    {
        DataSet ds = new DataSet();
        public string PK = "";
        string ReportFileName = "";
        string ReportPath = "";
        string sql;
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        public FrmBorrow_Print()
        {
            InitializeComponent();
        }

        private void FrmBorrow_Print_Load(object sender, EventArgs e)
        {
            ReportFileName = "倉庫借入作業系統";
            rpt3.SetUserDefineRpt("倉庫借入作業系統_簡要自定.rpt");
            rpt4.SetUserDefineRpt("倉庫借入作業系統_組件自定.rpt");

            BoNo.Focus();
            BoNo.Text = this.PK;
            BoNo1.Text = this.PK;

            SetRdUdf();
        }

        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(BoNo1.Text, BoNo.Text) < 0)
            {
                MessageBox.Show("終止借入單號不可小於起始借入單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                setsql();
                sql += " where '0'='0'";
                if (BoNo.Text != "")
                    sql += " and borrd.bono >=@bono ";
                if (BoNo1.Text != "")
                    sql += " and borrd.bono <=@bono1 ";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        if (BoNo.Text != "") cmd.Parameters.AddWithValue("bono", BoNo.Text);
                        if (BoNo1.Text != "") cmd.Parameters.AddWithValue("bono1", BoNo1.Text);

                        sql += " order by borrd.bono,borrd.recordno ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds.Clear();
                            da.Fill(ds);
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
                    cmd.CommandText = "select count(bono) from borr where bono=@bono";
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
            sql += " SELECT borr.bono AS borr_bono, borr.bodate AS borr_bodate, borr.bodate1 AS borr_bodate1, borr.bodate2 AS borr_bodate2, ";
            sql += " borr.cono AS borr_cono, borr.coname1 AS borr_coname1, borr.coname2 AS borr_coname2, borr.fano AS borr_fano, ";
            sql += " borr.faname1 AS borr_faname1, borr.faname2 AS borr_faname2, borr.fatel1 AS borr_fatel1, ";
            sql += " borr.faper1 AS borr_faper1, borr.stno AS borr_stno, borr.stname AS borr_stname, borr.emno AS borr_emno, ";
            sql += " borr.emname AS borr_emname, borr.xa1no AS borr_xa1no, borr.xa1name AS borr_xa1name, ";
            sql += " borr.xa1par AS borr_xa1par, borr.taxmnyf AS borr_taxmnyf, borr.taxmnyb AS borr_taxmnyb, ";
            sql += " borr.taxmny AS borr_taxmny, borr.x3no AS borr_x3no, borr.rate AS borr_rate, borr.tax AS borr_tax, ";
            sql += " borr.totmny AS borr_totmny, borr.taxb AS borr_taxb, borr.totmnyb AS borr_totmnyb, borr.bomemo AS borr_bomemo, borr.bomemo1 AS borr_bomemo1, ";
            sql += " borr.recordno AS borr_recordno,  borrd.bodate AS borrd_bodate, borrd.bono AS borrd_bono, ";
            sql += " borrd.bodate1 AS borrd_bodate1, borrd.bodate2 AS borrd_bodate2, borrd.cono AS borrd_cono, ";
            sql += " borrd.fano AS borrd_fano, borrd.stno AS borrd_stno, borrd.emno AS borrd_emno, borrd.xa1no AS borrd_xa1no, ";
            sql += " borrd.xa1par AS borrd_xa1par, borrd.itno AS borrd_itno, borrd.itname AS borrd_itname, borrd.ittrait AS borrd_ittrait, ";
            sql += " borrd.itunit AS borrd_itunit, borrd.itpkgqty AS borrd_itpkgqty, borrd.qty AS borrd_qty, borrd.price AS borrd_price, ";
            sql += " borrd.prs AS borrd_prs, borrd.rate AS borrd_rate, borrd.taxprice AS borrd_taxprice, borrd.mny AS borrd_mny, ";
            sql += " borrd.priceb AS borrd_priceb, borrd.taxpriceb AS borrd_taxpriceb, borrd.mnyb AS borrd_mnyb, ";
            sql += " borrd.memo AS borrd_memo, borrd.lowzero AS borrd_lowzero, borrd.bomid AS borrd_bomid, ";
            sql += " borrd.bomrec AS borrd_bomrec, borrd.recordno AS borrd_recordno, borrd.sltflag AS borrd_sltflag, ";
            sql += " borrd.extflag AS borrd_extflag, borrd.itdesp1 AS borrd_itdesp1, borrd.itdesp2 AS borrd_itdesp2, ";
            sql += " borrd.itdesp3 AS borrd_itdesp3, borrd.itdesp4 AS borrd_itdesp4, borrd.itdesp5 AS borrd_itdesp5, ";
            sql += " borrd.itdesp6 AS borrd_itdesp6, borrd.itdesp7 AS borrd_itdesp7, borrd.itdesp8 AS borrd_itdesp8, ";
            sql += " borrd.itdesp9 AS borrd_itdesp9, borrd.itdesp10 AS borrd_itdesp10, fact.faname2 AS fact_faname2, ";
            sql += " fact.faaddr1 AS fact_faaddr1, fact.fauno AS fact_fauno, fact.fafax1 AS fact_fafax1,fact.fatel1 AS fact_fatel1 ";
            if (rpt2.Checked || rpt4.Checked)
            {
                sql += " ,borrbom.bono AS borrbom_bono, borrbom.bomid AS borrbom_bomid, borrbom.BomRec AS borrbom_BomRec, ";
                sql += " borrbom.itno AS borrbom_itno, borrbom.itname AS borrbom_itname, borrbom.itunit AS borrbom_itunit, ";
                sql += " borrbom.itqty AS borrbom_itqty, borrbom.itpareprs AS borrbom_itpareprs, borrbom.itpkgqty AS borrbom_itpkgqty, ";
                sql += " borrbom.itrec AS borrbom_itrec, borrbom.itprice AS borrbom_itprice, borrbom.itprs AS borrbom_itprs, ";
                sql += " borrbom.itmny AS borrbom_itmny, borrbom.itnote AS borrbom_itnote, borrbom.ItSource AS borrbom_ItSource, ";
                sql += " borrbom.ItBuyPri AS borrbom_ItBuyPri, borrbom.ItBuyMny AS borrbom_ItBuyMny";
            }
            sql += " FROM borr  ";
            sql += " LEFT  JOIN fact ON borr.fano = fact.fano ";
            sql += " LEFT  JOIN borrd ON borr.bono = borrd.bono";
            if (rpt2.Checked || rpt4.Checked)
            {
                sql += " LEFT  JOIN borrbom ON borrd.bomid = borrbom.bomid";
            }

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(Threetxtend);
            pnlist.Add(Conostart);
            pVar.SaveRadioUdf(pnlist, "Borr");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(Threetxtend);
            pnlist.Add(Conostart);
            pVar.SetRadioUdf(pnlist, "Borr");
        }

    }
}
