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
    public partial class FrmFQuot_Print : Formbase
    {
        DataTable dt = new DataTable();
        public string PK { get; set; }
        string ReportFileName = "";
        string ReportPath = "";
        string sql;

        public FrmFQuot_Print()
        {
            InitializeComponent();
        }

        private void FrmFQuot_Print_Load(object sender, EventArgs e)
        {
            ReportFileName = "詢價作業系統";

            radio5.判斷有無CF或RF("詢價作業系統_簡要自定一");
            radio6.判斷有無CF或RF("詢價作業系統_簡要自定二");
            radio7.判斷有無CF或RF("詢價作業系統_組合自定一");
            radio8.判斷有無CF或RF("詢價作業系統_組合自定二");

            FqNo.Focus();
            FqNo.Text = PK;
            FqNo1.Text = PK;

            SetRdUdf();
        }

        void setsql()
        {
            sql = "";
            sql += "SELECT a.fqno AS fquot_fqno, a.fqdate AS fquot_fqdate, a.fqdate1 AS fquot_fqdate1, a.fqdate2 AS fquot_fqdate2, ";
            sql += " a.fqdates AS fquot_fqdates, a.fqdates1 AS fquot_fqdates1, a.fqdates2 AS fquot_fqdates2, a.cono AS fquot_cono, ";
            sql += " a.coname1 AS fquot_coname1, a.coname2 AS fquot_coname2, a.fano AS fquot_fano, a.faname2 AS fquot_faname2,";
            sql += " a.faname1 AS fquot_faname1, a.fatel1 AS fquot_fatel1, a.faper1 AS fquot_faper1, a.emno AS fquot_emno,";
            sql += " a.emname AS fquot_emname, a.xa1no AS fquot_xa1no, a.xa1name AS fquot_xa1name, a.xa1par AS fquot_xa1par,";
            sql += " a.taxmnyf AS fquot_taxmnyf, a.taxmnyb AS fquot_taxmnyb, a.taxmny AS fquot_taxmny, a.x3no AS fquot_x3no,";
            sql += " a.rate AS fquot_rate, a.tax AS fquot_tax, a.totmny AS fquot_totmny, a.taxb AS fquot_taxb, a.totmnyb AS fquot_totmnyb,";
            sql += " a.fqpayment AS fquot_fqpayment, a.fqperiod AS fquot_fqperiod, a.fqmemo AS fquot_fqmemo,";
            sql += " a.recordno AS fquot_recordno, a.UsrNo AS fquot_UsrNo,a.fqmemo1 AS fquot_fqmemo1, ";
            sql += " a.AppDate AS fquot_AppDate, a.EdtDate AS fquot_EdtDate,a.AppScNo AS fquot_AppScNo, a.EdtScNo AS fquot_EdtScNo, ";
            if (radio3.Checked || radio4.Checked)
            {
                sql += " c.FqNo AS fquotbom_FqNo,c.BomID AS fquotbom_BomID, c.BomRec AS fquotbom_BomRec, c.itno AS fquotbom_itno, c.itname AS fquotbom_itname, c.itunit AS fquotbom_itunit,";
                sql += " c.itqty AS fquotbom_itqty, c.itpareprs AS fquotbom_itpareprs, c.itpkgqty AS fquotbom_itpkgqty, ";
                sql += " c.itrec AS fquotbom_itrec, c.itprice AS fquotbom_itprice, c.itprs AS fquotbom_itprs, c.itmny AS fquotbom_itmny,";
                sql += " c.itnote AS fquotbom_itnote, c.ItSource AS fquotbom_ItSource, c.ItBuyPri AS fquotbom_ItBuyPri, ";
                sql += " c.ItBuyMny AS fquotbom_ItBuyMny, ";
            }
            sql += " b.fqno AS fquotd_fqno, b.fqdate AS fquotd_fqdate, b.fqdate1 AS fquotd_fqdate1, b.fqdate2 AS fquotd_fqdate2, b.fqdates AS fquotd_fqdates, b.fqdates1 AS fquotd_fqdates1, ";
            sql += " b.fqdates2 AS fquotd_fqdates2, b.fano AS fquotd_fano, b.emno AS fquotd_emno, b.xa1no AS fquotd_xa1no, ";
            sql += " b.xa1par AS fquotd_xa1par, b.itno AS fquotd_itno, b.itname AS fquotd_itname, b.ittrait AS fquotd_ittrait, ";
            sql += " b.itunit AS fquotd_itunit, b.itpkgqty AS fquotd_itpkgqty, b.qty AS fquotd_qty, b.price AS fquotd_price, ";
            sql += " b.prs AS fquotd_prs, b.rate AS fquotd_rate, b.taxprice AS fquotd_taxprice, b.mny AS fquotd_mny, ";
            sql += " b.priceb AS fquotd_priceb, b.taxpriceb AS fquotd_taxpriceb, b.mnyb AS fquotd_mnyb, b.memo AS fquotd_memo,";
            sql += " b.lowzero AS fquotd_lowzero, b.bomid AS fquotd_bomid, b.bomrec AS fquotd_bomrec, b.recordno AS fquotd_recordno,";
            sql += " b.sltflag AS fquotd_sltflag, b.extflag AS fquotd_extflag, b.itdesp1 AS fquotd_itdesp1, b.itdesp2 AS fquotd_itdesp2,";
            sql += " b.itdesp3 AS fquotd_itdesp3, b.itdesp4 AS fquotd_itdesp4, b.itdesp5 AS fquotd_itdesp5, b.itdesp6 AS fquotd_itdesp6,";
            sql += " b.itdesp7 AS fquotd_itdesp7, b.itdesp8 AS fquotd_itdesp8, b.itdesp9 AS fquotd_itdesp9, ";
            sql += " b.itdesp10 AS fquotd_itdesp10, b.stName AS fquotd_stName, f.fano AS fact_fano, f.fafax1 AS fact_fafax1,";
            sql += " f.faaddr1 AS fact_faaddr1, f.fauno AS fact_fauno, f.faname2 AS fact_faname2 , i.itpicture AS item_itpicture , i.pic AS item_pic,DATALENGTH(i.pic) as pic長度, ";
            sql += " scrit.scname as scname,scrit.scname1 as scname1";
            sql += " FROM fquotd AS b LEFT JOIN fquot AS a ON a.fqno = b.fqno";
            sql += " left join fact as f on f.fano=a.fano";
            sql += " left join item as i on i.itno=b.itno";
            sql += " LEFT   JOIN scrit ON a.appscno = scrit.scname ";
            if (radio3.Checked || radio4.Checked)
            {
                sql += " LEFT  JOIN FQuotBom AS c ON c.BomID = b.bomid";
            }
        }
        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(FqNo1.Text, FqNo.Text) < 0)
            {
                MessageBox.Show("終止詢價憑證不可大於起始報價憑證", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (radio1.Checked)
            {
                ReportPath += "_簡要表";
            }
            else if (radio2.Checked)
            {
                ReportPath += "_規格說明";
            }
            else if (radio3.Checked)
            {
                ReportPath += "_組件明細";
            }
            else if (radio4.Checked)
            {
                ReportPath += "_組件明細_規格";
            }
            else if (radio5.Checked)
            {
                ReportPath += "_簡要自定一";
            }
            else if (radio6.Checked)
            {
                ReportPath += "_簡要自定二";
            }
            else if (radio7.Checked)
            {
                ReportPath += "_組合自定一";
            }
            else
            {
                ReportPath += "_組合自定二";
            }

            return Common.判斷開啟報表類型(ReportPath);
        }
        void 水晶列印(string path, RptMode mode)
        {
            try
            {
                setsql();
                sql += " where '0'='0'";
                if (FqNo.Text != "")
                    sql += " and a.fqno >=@fqno ";
                if (FqNo1.Text != "")
                    sql += " and a.fqno <=@fqno1 ";


                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fqno", FqNo.Text);
                    cmd.Parameters.AddWithValue("fqno1", FqNo1.Text);
                    cmd.CommandText = sql;
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        dt.Clear();
                        dd.Fill(dt);
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    FqNo.Focus();
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReportDocument oRpt = new ReportDocument();
                oRpt.Load(path);
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
            printTB.TableName = "FQUOT_";
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                sql = "";
                sql += "SELECT a.fqno AS fquot_詢價單號, a.fqdate AS fquot_詢價日期_民國, a.fqdate1 AS fquot_詢價日期_西元, a.fqdate2 AS fquot_詢價日期_保留, ";
                sql += " a.fqdates AS fquot_預計交期_民國, a.fqdates1 AS fquot_預計交期_西元, a.fqdates2 AS fquot_預計交期_保留, a.cono AS fquot_公司編號, ";
                sql += " a.coname1 AS fquot_公司簡稱, a.coname2 AS fquot_公司名稱, a.fano AS fquot_廠商編號, a.faname2 AS fquot_廠商名稱,";
                sql += " a.faname1 AS fquot_廠商簡稱, a.fatel1 AS fquot_廠商電話, a.faper1 AS fquot_廠商聯絡人, a.emno AS fquot_採購人員編號,";
                sql += " a.emname AS fquot_採購人員姓名, a.xa1no AS fquot_幣別編號, a.xa1name AS fquot_幣別名稱, a.xa1par AS fquot_匯率,";
                sql += " a.taxmnyf AS fquot_taxmnyf, a.taxmnyb AS fquot_本幣合計, a.taxmny AS fquot_外幣合計, a.x3no AS fquot_稅別編號,";
                sql += " a.rate AS fquot_稅率, a.tax AS fquot_外幣營業稅額, a.totmny AS fquot_外幣詢價總額, a.taxb AS fquot_本幣營業稅額, a.totmnyb AS fquot_本幣詢價總額,";
                sql += " a.fqpayment AS fquot_付款條件, a.fqperiod AS fquot_有效期限, a.fqmemo AS fquot_備註,";
                sql += " a.recordno AS fquot_recordno, a.UsrNo AS fquot_UsrNo,a.fqmemo1 AS fquot_fqmemo1, ";
                sql += " a.AppDate AS fquot_AppDate, a.EdtDate AS fquot_EdtDate,a.AppScNo AS fquot_AppScNo, a.EdtScNo AS fquot_EdtScNo, ";
                if (radio3.Checked || radio4.Checked)
                {
                    sql += " c.FqNo AS fquotbom_詢價單號,c.BomID AS fquotbom_BomID, c.BomRec AS fquotbom_BomRec, c.itno AS fquotbom_產品編號, c.itname AS fquotbom_品名規格, c.itunit AS fquotbom_單位,";
                    sql += " c.itqty AS fquotbom_標準用量, c.itpareprs AS fquotbom_母件比率, c.itpkgqty AS fquotbom_包裝數量, ";
                    sql += " c.itrec AS fquotbom_itrec, c.itprice AS fquotbom_單價, c.itprs AS fquotbom_折數, c.itmny AS fquotbom_金額,";
                    sql += " c.itnote AS fquotbom_說明, c.ItSource AS fquotbom_ItSource, c.ItBuyPri AS fquotbom_ItBuyPri, ";
                    sql += " c.ItBuyMny AS fquotbom_ItBuyMny,";
                }
                sql += " b.fqno AS fquotd_詢價單號, b.fqdate AS fquotd_詢價日期_民國, b.fqdate1 AS fquotd_詢價日期_西元, b.fqdate2 AS fquotd_詢價日期_保留, b.fqdates AS fquotd_預計交期_民國, b.fqdates1 AS fquotd_預計交期_西元, ";
                sql += " b.fqdates2 AS fquotd_預計交期_保留, b.fano AS fquotd_廠商編號, b.emno AS fquotd_業務編號, b.xa1no AS fquotd_幣別編號, ";
                sql += " b.xa1par AS fquotd_匯率, b.itno AS fquotd_產品編號, b.itname AS fquotd_品名規格, b.ittrait AS fquotd_產品組成, ";
                sql += " b.itunit AS fquotd_單位, b.itpkgqty AS fquotd_包裝數量, b.qty AS fquotd_數量, b.price AS fquotd_外幣單價, ";
                sql += " b.prs AS fquotd_折數, b.rate AS fquotd_稅率, b.taxprice AS fquotd_外幣稅前單價, b.mny AS fquotd_外幣稅前金額, ";
                sql += " b.priceb AS fquotd_本幣單價, b.taxpriceb AS fquotd_本幣稅前單價, b.mnyb AS fquotd_本幣稅前金額, b.memo AS fquotd_備註,";
                sql += " b.lowzero AS fquotd_lowzero, b.bomid AS fquotd_bomid, b.bomrec AS fquotd_bomrec, b.recordno AS fquotd_recordno,";
                sql += " b.sltflag AS fquotd_sltflag, b.extflag AS fquotd_extflag, b.itdesp1 AS fquotd_規格說明1, b.itdesp2 AS fquotd_規格說明2,";
                sql += " b.itdesp3 AS fquotd_規格說明3, b.itdesp4 AS fquotd_規格說明4, b.itdesp5 AS fquotd_規格說明5, b.itdesp6 AS fquotd_規格說明6,";
                sql += " b.itdesp7 AS fquotd_規格說明7, b.itdesp8 AS fquotd_規格說明8, b.itdesp9 AS fquotd_規格說明9, ";
                sql += " b.itdesp10 AS fquotd_規格說明10, b.stName AS fquotd_stName, f.fano AS fact_廠商編號, f.fafax1 AS fact_廠商傳真,";
                sql += " f.faaddr1 AS fact_廠商地址, f.fauno AS fact_廠商統一編號, f.faname2 AS fact_廠商名稱 , i.itpicture AS item_itpicture , i.pic AS item_pic,DATALENGTH(i.pic) as pic長度, ";
                sql += " scrit.scname as 使用者編號,scrit.scname1 as 使用者名稱";
                sql += " FROM fquotd AS b LEFT JOIN fquot AS a ON a.fqno = b.fqno";
                sql += " left join fact as f on f.fano=a.fano";
                sql += " left join item as i on i.itno=b.itno";
                sql += " LEFT   JOIN scrit ON a.appscno = scrit.scname ";
                if (radio3.Checked || radio4.Checked)
                {
                    sql += " LEFT  JOIN FQuotBom AS c ON c.BomID = b.bomid";
                }

                sql += " where '0'='0'";
                if (FqNo.Text != "")
                    sql += " and a.fqno >=@fqno ";
                if (FqNo1.Text != "")
                    sql += " and a.fqno <=@fqno1 ";
                sql += " order by b.itno ";

                cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                cmd.Parameters.AddWithValue("fqno1", FqNo1.Text.Trim());
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

                FastReport.PreView(path, printTB, "FQUOT_", null, null, mode, ReportFileName);
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

        private void FqNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fqno", ((TextBox)sender).Text.Trim());
                    cmd.CommandText = "select count(fqno) from fquot where fqno=@fqno";
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        e.Cancel = true;
                        using (var frm = new FrmFQuot_Print_FqNo())
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
        private void FqNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmFQuot_Print_FqNo())
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



        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SaveRadioUdf(pnlist, "FQuot");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SetRadioUdf(pnlist, "FQuot");
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
