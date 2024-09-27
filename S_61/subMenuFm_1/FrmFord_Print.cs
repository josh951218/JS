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

namespace S_61.subMenuFm_1
{
    public partial class FrmFord_Print : Formbase
    {
        public string PK = "";
        string ReportFileName = "";
        string ReportPath = "";
        string sql;
        DataSet ds = new DataSet();

        public FrmFord_Print()
        {
            InitializeComponent();
        }

        private void FrmFord_Print_Load(object sender, EventArgs e)
        {
            ReportFileName = "廠商採購管理";

            string kind = Common.Sys_DBqty == 1 ? "" : "P";
            radio5.判斷有無CF或RF("廠商採購管理_簡要自定一" + kind);
            radio6.判斷有無CF或RF("廠商採購管理_簡要自定二" + kind);
            radio7.判斷有無CF或RF("廠商採購管理_組合自定一" + kind);
            radio8.判斷有無CF或RF("廠商採購管理_組合自定二" + kind);

            FoNo.Focus();
            FoNo.Text = PK;
            FoNo1.Text = PK;

            SetRdUdf();
        }

        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(FoNo1.Text, FoNo.Text) < 0)
            {
                MessageBox.Show("終止採購單號不可大於起始採購單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var path = 確認列印報表();

            if (File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (path.EndsWith(".frx"))
            {
                FastReport列印(path, mode);
            }
            else
            {
                水晶列印(path, mode);
            }
        }
        string 確認列印報表()
        {
            ReportPath = ReportFileName;
            string kind = Common.Sys_DBqty == 1 ? "" : "P";

            if (radio1.Checked)
                ReportPath += "_簡要表" + kind;
            else if (radio2.Checked)
                ReportPath += "_規格說明" + kind;
            else if (radio3.Checked)
                ReportPath += "_委外加工單" + kind;
            else if (radio4.Checked)
                ReportPath += "_組件明細_規格" + kind;
            else if (radio5.Checked)
                ReportPath += "_簡要自定一" + kind;
            else if (radio6.Checked)
                ReportPath += "_簡要自定二" + kind;
            else if (radio7.Checked)
                ReportPath += "_組合自定一" + kind;
            else if (radio8.Checked)
                ReportPath += "_組合自定二" + kind;

            return Common.判斷開啟報表類型(ReportPath);
        }
        void setsql()
        {
            sql = "";
            sql += "SELECT ";
            sql += "ford.fono AS ford_fono, ford.fodate AS ford_fodate, ford.fodate1 AS ford_fodate1, ford.fodate2 AS ford_fodate2, ";
            sql += "ford.fqno AS ford_fqno, ford.fotrnflag AS ford_fotrnflag, ford.fooverflag AS ford_fooverflag, ford.cono AS ford_cono, ";
            sql += "ford.coname1 AS ford_coname1, ford.coname2 AS ford_coname2, ford.fano AS ford_fano, ";
            sql += "ford.faname2 AS ford_faname2, ford.faname1 AS ford_faname1, ford.fatel1 AS ford_fatel1, ford.faper1 AS ford_faper1,";
            sql += "ford.emno AS ford_emno, ford.emname AS ford_emname, ford.xa1no AS ford_xa1no, ford.xa1name AS ford_xa1name, ";
            sql += "ford.xa1par AS ford_xa1par, ford.trno AS ford_trno, ford.trname AS ford_trname, ford.taxmnyf AS ford_taxmnyf, ";
            sql += "ford.taxmnyb AS ford_taxmnyb, ford.taxmny AS ford_taxmny, ford.x3no AS ford_x3no, ford.rate AS ford_rate, ";
            sql += "ford.tax AS ford_tax, ford.totmny AS ford_totmny, ford.taxb AS ford_taxb, ford.totmnyb AS ford_totmnyb, ";
            sql += "ford.fopayment AS ford_fopayment, ford.foperiod AS ford_foperiod, ford.fomemo AS ford_fomemo, ";
            sql += "ford.recordno AS ford_recordno, ford.UsrNo AS ford_UsrNo, ford.AppDate AS ford_AppDate, ";
            sql += "ford.EdtDate AS ford_EdtDate, ford.AppScNo AS ford_AppScNo, ford.EdtScNo AS ford_EdtScNo,ford.fomemo1 AS ford_fomemo1, ";
            if (radio4.Checked)
            {
                sql += "FOrdBom.FoNo AS FOrdBom_FoNo, FOrdBom.BomID AS FOrdBom_BomID, FOrdBom.BomRec AS FOrdBom_BomRec,";
                sql += "FOrdBom.itno AS FOrdBom_itno, FOrdBom.itname AS FOrdBom_itname, FOrdBom.itunit AS FOrdBom_itunit, ";
                sql += "FOrdBom.itqty AS FOrdBom_itqty, FOrdBom.itpareprs AS FOrdBom_itpareprs, ";
                sql += "FOrdBom.itpkgqty AS FOrdBom_itpkgqty, FOrdBom.itrec AS FOrdBom_itrec, FOrdBom.itprice AS FOrdBom_itprice, ";
                sql += "FOrdBom.itprs AS FOrdBom_itprs, FOrdBom.itmny AS FOrdBom_itmny, FOrdBom.itnote AS FOrdBom_itnote, ";
                sql += "FOrdBom.ItSource AS FOrdBom_ItSource, FOrdBom.ItBuyPri AS FOrdBom_ItBuyPri, ";
                sql += "FOrdBom.ItBuyMny AS FOrdBom_ItBuyMny, ";
            }

            sql += "fordd.fono AS fordd_fono, fordd.fodate AS fordd_fodate, ";
            sql += "fordd.fodate1 AS fordd_fodate1, fordd.fodate2 AS fordd_fodate2, fordd.fqno AS fordd_fqno, ";
            sql += "fordd.fotrnflag AS fordd_fotrnflag, fordd.fano AS fordd_fano, fordd.emno AS fordd_emno, fordd.xa1no AS fordd_xa1no, ";
            sql += "fordd.xa1par AS fordd_xa1par, fordd.itno AS fordd_itno, fordd.itname AS fordd_itname, fordd.ittrait AS fordd_ittrait, ";
            sql += "fordd.itunit AS fordd_itunit, fordd.itpkgqty AS fordd_itpkgqty, fordd.qty AS fordd_qty, fordd.prs AS fordd_prs, ";
            sql += "fordd.price AS fordd_price, fordd.rate AS fordd_rate, fordd.taxprice AS fordd_taxprice, fordd.mny AS fordd_mny, ";
            sql += "fordd.priceb AS fordd_priceb, fordd.taxpriceb AS fordd_taxpriceb, fordd.mnyb AS fordd_mnyb, ";
            sql += "fordd.qtyout AS fordd_qtyout, fordd.qtyin AS fordd_qtyin, fordd.esdate AS fordd_esdate, ";
            sql += "fordd.esdate1 AS fordd_esdate1, fordd.esdate2 AS fordd_esdate2, fordd.memo AS fordd_memo, ";
            sql += "fordd.lowzero AS fordd_lowzero, fordd.bomid AS fordd_bomid, fordd.bomrec AS fordd_bomrec, ";
            sql += "fordd.recordno AS fordd_recordno, fordd.sltflag AS fordd_sltflag, fordd.extflag AS fordd_extflag, ";
            sql += "fordd.itdesp1 AS fordd_itdesp1, fordd.itdesp2 AS fordd_itdesp2, fordd.itdesp3 AS fordd_itdesp3, ";
            sql += "fordd.itdesp4 AS fordd_itdesp4, fordd.itdesp5 AS fordd_itdesp5, fordd.itdesp6 AS fordd_itdesp6,";
            sql += "fordd.itdesp7 AS fordd_itdesp7, fordd.itdesp8 AS fordd_itdesp8, fordd.itdesp9 AS fordd_itdesp9, ";
            sql += "fordd.itdesp10 AS fordd_itdesp10, fordd.stName AS fordd_stName, fordd.qtyNotIn AS fordd_qtyNotIn, ";
            sql += "fordd.OrNo AS fordd_OrNo, fordd.OrRno AS fordd_OrRno, ";
            sql += "fordd.mformula AS fordd_mformula, fordd.mwidth4 AS fordd_mwidth4, fordd.mwidth3 AS fordd_mwidth3, ";
            sql += "fordd.mwidth2 AS fordd_mwidth2, fordd.mwidth1 AS fordd_mwidth1, fordd.mlong AS fordd_mlong, ";
            sql += "fordd.munit AS fordd_munit, fordd.mqty AS fordd_mqty, fordd.Pqty AS fordd_Pqty, fordd.Punit AS fordd_Punit, ";
            sql += "fordd.FoID AS fordd_FoID,";
            sql += "fact.faname2 AS fact_faname2, fact.fatel1 AS fact_fatel1, ";
            sql += "fact.fafax1 AS fact_fafax1, fact.faaddr1 AS fact_faaddr1, fact.fauno AS fact_fauno, fact.faname1 AS fact_faname1, ";
            sql += "fact.faper1 AS fact_faper1, ";
            sql += "scrit.scname as scname,scrit.scname1 as scname1 ";
            sql += "FROM ford LEFT JOIN ";
            sql += "fordd ON ford.fono = fordd.fono ";
            sql += "LEFT JOIN fact ON ford.fano = fact.fano  ";
            sql += "LEFT JOIN scrit ON ford.appscno = scrit.scname  ";
            if (radio4.Checked)
            {
                sql += "LEFT JOIN FOrdBom ON fordd.bomid = FOrdBom.bomid ";
            }
        }
        void 水晶列印(string path, RptMode mode)
        {
            try
            {
                setsql();
                sql += " where '0'='0'";
                if (FoNo.Text != "")
                    sql += " and ford.fono >=@fono";
                if (FoNo1.Text != "")
                    sql += " and ford.fono <=@fono1";

                sql += " order by fordd_FoID ";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fono", FoNo.Text);
                    cmd.Parameters.AddWithValue("fono1", FoNo1.Text);

                    cmd.CommandText = sql;

                    ds.Clear();
                    da.Fill(ds, "ford");

                    cmd.CommandText = @"
                    Select 
                            *,DATALENGTH(pic) as pic長度
                    from (
	                    Select distinct itno from fordd where fono >= @fono and fono <= @fono1
                    )A
                    Inner join item on A.itno = item.itno";
                    da.Fill(ds, "item");
                }

                if (ds.Tables["ford"].Rows.Count == 0)
                {
                    FoNo.Focus();
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReportDocument oRpt = new ReportDocument();
                oRpt.Load(path);
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
                //表頭-電話、傳真
                if (Txt.Find(t => t.Name == "txttel") != null)
                {
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
        void FastReport列印(string path, RptMode mode)
        {
            DataTable printTB = new DataTable();
            printTB.TableName = "FORD_";
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                sql = "";
                sql += "SELECT ";
                sql += "ford.fono AS ford_採購單號, ford.fodate AS ford_採購日期_民國, ford.fodate1 AS ford_採購日期_西元, ford.fodate2 AS ford_採購日期_保留, ";
                sql += "ford.fqno AS ford_詢價憑證, ford.fotrnflag AS ford_fotrnflag, ford.fooverflag AS ford_結案標示, ford.cono AS ford_公司編號, ";
                sql += "ford.coname1 AS ford_公司簡稱, ford.coname2 AS ford_公司名稱, ford.fano AS ford_廠商編號, ";
                sql += "ford.faname2 AS ford_廠商名稱, ford.faname1 AS ford_廠商簡稱, ford.fatel1 AS ford_廠商電話, ford.faper1 AS ford_廠商聯絡人,";
                sql += "ford.emno AS ford_採購人員編號, ford.emname AS ford_採購人員姓名, ford.xa1no AS ford_幣別編號, ford.xa1name AS ford_幣別名稱, ";
                sql += "ford.xa1par AS ford_匯率, ford.trno AS ford_trno, ford.trname AS ford_trname, ford.taxmnyf AS ford_taxmnyf, ";
                sql += "ford.taxmnyb AS ford_本幣合計, ford.taxmny AS ford_外幣合計, ford.x3no AS 稅別編號, ford.rate AS ford_稅率, ";
                sql += "ford.tax AS ford_外幣營業稅額, ford.totmny AS ford_外幣採購總額, ford.taxb AS ford_本幣營業稅額, ford.totmnyb AS ford_本幣採購總額, ";
                sql += "ford.fopayment AS ford_付款條件, ford.foperiod AS ford_有效期限, ford.fomemo AS ford_備註, ";
                sql += "ford.recordno AS ford_recordno, ford.UsrNo AS ford_UsrNo, ford.AppDate AS ford_AppDate, ";
                sql += "ford.EdtDate AS ford_EdtDate, ford.AppScNo AS ford_AppScNo, ford.EdtScNo AS ford_EdtScNo,ford.fomemo1 AS ford_詳細備註, ";
                if (radio4.Checked || radio7.Checked || radio8.Checked)
                {
                    sql += "FOrdBom.FoNo AS FOrdBom_採購單號, FOrdBom.BomID AS FOrdBom_BomID, FOrdBom.BomRec AS FOrdBom_BomRec,";
                    sql += "FOrdBom.itno AS FOrdBom_產品編號, FOrdBom.itname AS FOrdBom_品名規格, FOrdBom.itunit AS FOrdBom_單位, ";
                    sql += "FOrdBom.itqty AS FOrdBom_標準用量, FOrdBom.itpareprs AS FOrdBom_母件比率, ";
                    sql += "FOrdBom.itpkgqty AS FOrdBom_包裝數量, FOrdBom.itrec AS FOrdBom_itrec, FOrdBom.itprice AS FOrdBom_單價, ";
                    sql += "FOrdBom.itprs AS FOrdBom_折數, FOrdBom.itmny AS FOrdBom_數量, FOrdBom.itnote AS FOrdBom_備註, ";
                    sql += "FOrdBom.ItSource AS FOrdBom_ItSource, FOrdBom.ItBuyPri AS FOrdBom_ItBuyPri, ";
                    sql += "FOrdBom.ItBuyMny AS FOrdBom_ItBuyMny, ";
                }

                sql += "fordd.fono AS fordd_採購單號, fordd.fodate AS fordd_採購日期_民國, ";
                sql += "fordd.fodate1 AS fordd_採購日期_西元, fordd.fodate2 AS fordd_採購日期_保留, fordd.fqno AS fordd_詢價憑證, ";
                sql += "fordd.fotrnflag AS fordd_fotrnflag, fordd.fano AS fordd_廠商編號, fordd.emno AS fordd_採購人員編號, fordd.xa1no AS fordd_幣別編號, ";
                sql += "fordd.xa1par AS fordd_匯率, fordd.itno AS fordd_產品編號, fordd.itname AS fordd_品名規格, fordd.ittrait AS fordd_產品組成, ";
                sql += "fordd.itunit AS fordd_單位, fordd.itpkgqty AS fordd_包裝數量, fordd.qty AS fordd_數量, fordd.prs AS fordd_折數, ";
                sql += "fordd.price AS fordd_外幣單價, fordd.rate AS fordd_稅率, fordd.taxprice AS fordd_外幣稅前單價, fordd.mny AS fordd_外幣稅前金額, ";
                sql += "fordd.priceb AS fordd_本幣單價, fordd.taxpriceb AS fordd_本幣稅前單價, fordd.mnyb AS fordd_本幣稅前金額, ";
                sql += "fordd.qtyout AS fordd_qtyout, fordd.qtyin AS fordd_採購已交量, fordd.esdate AS fordd_交貨日_民國, ";
                sql += "fordd.esdate1 AS fordd_交貨日_西元, fordd.esdate2 AS fordd_交貨日_保留, fordd.memo AS fordd_說明, ";
                sql += "fordd.lowzero AS fordd_lowzero, fordd.bomid AS fordd_bomid, fordd.bomrec AS fordd_bomrec, ";
                sql += "fordd.recordno AS fordd_recordno, fordd.sltflag AS fordd_sltflag, fordd.extflag AS fordd_extflag, ";
                sql += "fordd.itdesp1 AS fordd_規格說明1, fordd.itdesp2 AS fordd_規格說明2, fordd.itdesp3 AS fordd_規格說明3, ";
                sql += "fordd.itdesp4 AS fordd_規格說明4, fordd.itdesp5 AS fordd_規格說明5, fordd.itdesp6 AS fordd_規格說明6,";
                sql += "fordd.itdesp7 AS fordd_規格說明7, fordd.itdesp8 AS fordd_規格說明8, fordd.itdesp9 AS fordd_規格說明9, ";
                sql += "fordd.itdesp10 AS fordd_規格說明10, fordd.stName AS fordd_stName, fordd.qtyNotIn AS fordd_採購未交量, ";
                sql += "fordd.OrNo AS fordd_OrNo, fordd.OrRno AS fordd_OrRno, ";
                sql += "fordd.mformula AS fordd_mformula, fordd.mwidth4 AS fordd_mwidth4, fordd.mwidth3 AS fordd_mwidth3, ";
                sql += "fordd.mwidth2 AS fordd_mwidth2, fordd.mwidth1 AS fordd_mwidth1, fordd.mlong AS fordd_mlong, ";
                sql += "fordd.munit AS fordd_munit, fordd.mqty AS fordd_mqty, fordd.Pqty AS fordd_Pqty, fordd.Punit AS fordd_Punit, ";
                sql += "fordd.FoID AS fordd_FoID,";
                sql += "fact.faname2 AS fact_廠商名稱, fact.fatel1 AS fact_廠商電話, ";
                sql += "fact.fafax1 AS fact_廠商傳真, fact.faaddr1 AS fact_廠商地址, fact.fauno AS fact_廠商統一編號, fact.faname1 AS fact_廠商簡稱 , i.itpicture AS item_itpicture , i.pic AS item_pic,DATALENGTH(i.pic) as pic長度,";
                sql += "fact.faper1 AS fact_廠商聯絡人, ";
                sql += "scrit.scname as 使用者編號,scrit.scname1 as 使用者名稱 ";
                sql += "FROM ford LEFT JOIN ";
                sql += "fordd ON ford.fono = fordd.fono ";
                sql += "LEFT JOIN fact ON ford.fano = fact.fano  ";
                sql += "LEFT JOIN scrit ON ford.appscno = scrit.scname  ";
                sql += " left join item as i on i.itno=fordd.itno";
                if (radio4.Checked || radio7.Checked || radio8.Checked)
                {
                    sql += " LEFT JOIN FOrdBom ON fordd.bomid = FOrdBom.bomid ";
                }

                sql += " where '0'='0'";
                if (FoNo.Text != "")
                    sql += " and ford.fono >=@fono";
                if (FoNo1.Text != "")
                    sql += " and ford.fono <=@fono1";

                sql += " order by fordd_FoID ";


                cmd.Parameters.AddWithValue("fono", FoNo.Text.Trim());
                cmd.Parameters.AddWithValue("fono1", FoNo1.Text.Trim());
                cmd.CommandText = sql;
                dd.Fill(printTB);
            }

            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                //報表參數

                //公司抬頭
                var txtstart = "";
                if (rdHeader1.Checked) txtstart = Common.dtstart.Rows[0]["pnname"].ToString();
                else if (rdHeader2.Checked) txtstart = Common.dtstart.Rows[1]["pnname"].ToString();
                else if (rdHeader3.Checked) txtstart = Common.dtstart.Rows[2]["pnname"].ToString();
                else if (rdHeader4.Checked) txtstart = Common.dtstart.Rows[3]["pnname"].ToString();
                else if (rdHeader5.Checked) txtstart = Common.dtstart.Rows[4]["pnname"].ToString();
                else txtstart = "";
                FastReport.dy.Add("txtstart", txtstart);
                //銷貨單標題
                var title = Common.Sys_SaleHead;
                FastReport.dy.Add("title", title);
                //三行註腳
                var txtend = "";
                if (rdFooter1.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                else if (rdFooter2.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                else if (rdFooter3.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                else if (rdFooter4.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                else if (rdFooter5.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                else txtend = "";
                FastReport.dy.Add("txtend", txtend);
                //表頭-公司住址
                var txtadress = "";
                if (rdHeader1.Checked) txtadress = "    " + Common.dtstart.Rows[0]["pnaddr"].ToString();
                else if (rdHeader2.Checked) txtadress = "    " + Common.dtstart.Rows[1]["pnaddr"].ToString();
                else if (rdHeader3.Checked) txtadress = "    " + Common.dtstart.Rows[2]["pnaddr"].ToString();
                else if (rdHeader4.Checked) txtadress = "    " + Common.dtstart.Rows[3]["pnaddr"].ToString();
                else if (rdHeader5.Checked) txtadress = "    " + Common.dtstart.Rows[4]["pnaddr"].ToString();
                else txtadress = "";
                FastReport.dy.Add("txtadress", txtadress);
                //表頭-電話、傳真
                var txttel = "";
                if (rdHeader1.Checked) txttel = "    TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
                else if (rdHeader2.Checked) txttel = "    TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
                else if (rdHeader3.Checked) txttel = "    TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
                else if (rdHeader4.Checked) txttel = "    TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
                else if (rdHeader5.Checked) txttel = "    TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
                else txttel = "";
                FastReport.dy.Add("txttel", txttel);

                FastReport.dy["是否顯示金額"] = Common.User_ShopPrice;

                FastReport.PreView(path, printTB, "FORD_", null, null, mode, ReportFileName);
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



        private void FoNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmFord_Print_FoNo())
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
        private void FoNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fono", ((TextBox)sender).Text.Trim());
                    cmd.CommandText = "select count(fono) from ford where fono=@fono";
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        e.Cancel = true;
                        using (var frm = new FrmFord_Print_FoNo())
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

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SaveRadioUdf(pnlist, "Ford");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SetRadioUdf(pnlist, "Ford");
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var path = 確認列印報表();
                if (path.EndsWith(".frx") == false) return;

                var dl = MessageBox.Show("是否要修改報表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl != DialogResult.Yes) return;

                JBS.FReport.Design(path);
            }
        }
    }
}
