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
    public partial class FrmQuote_Print : Formbase
    {
        DataTable dt = new DataTable();
        public string PK { get; set; }
        string ReportFileName = "";
        string ReportPath = "";
        string sql;
        DataTable rpt = new DataTable();

        public FrmQuote_Print()
        {
            InitializeComponent();
        }

        private void FrmQuote_Print_Load(object sender, EventArgs e)
        {
            ReportFileName = "報價作業系統";
            var p = Common.Sys_DBqty == 1 ? "" : "P";
            radio5.判斷有無CF或RF("報價作業系統_簡要自定一" + p );
            radio6.判斷有無CF或RF("報價作業系統_簡要自定二" + p );
            radio7.判斷有無CF或RF("報價作業系統_組合自定一" + p );
            radio8.判斷有無CF或RF("報價作業系統_組合自定二" + p );

            QuNo.Focus();
            QuNo.Text = PK;
            QuNo1.Text = PK;

            SetRdUdf();
        }

        void setsql()
        {
            sql = "";
            sql += "SELECT a.quno AS quote_quno, a.qudate1 AS quote_qudate1, a.qudates1 AS quote_qudates1, a.qudates2 AS quote_qudates2, ";
            sql += " a.cono AS quote_cono, a.coname1 AS quote_coname1, a.coname2 AS quote_coname2, a.cuno AS quote_cuno,";
            sql += " a.cuname2 AS quote_cuname2, a.cuname1 AS quote_cuname1, a.cutel1 AS quote_cutel1, a.cuper1 AS quote_cuper1, ";
            sql += " a.emno AS quote_emno, a.emname AS quote_emname, a.xa1no AS quote_xa1no, a.xa1name AS quote_xa1name, ";
            sql += " a.xa1par AS quote_xa1par, a.trno AS quote_trno, a.trname AS quote_trname, a.taxmnyf AS quote_taxmnyf, ";
            sql += " a.taxmnyb AS quote_taxmnyb, a.taxmny AS quote_taxmny, a.x3no AS quote_x3no, a.rate AS quote_rate, ";
            sql += " a.tax AS quote_tax, a.totmny AS quote_totmny, a.taxb AS quote_taxb, a.totmnyb AS quote_totmnyb, ";
            sql += " a.qupayment AS quote_qupayment, a.quperiod AS quote_quperiod, a.qumemo AS quote_qumemo,";
            sql += " a.recordno AS quote_recordno, a.UsrNo AS quote_UsrNo, a.QuDate AS quote_QuDate, a.QuDates AS quote_QuDates,a.qumemo1 AS quote_qumemo1,a.ischeck AS quote_ischeck,";
            if (radio3.Checked || radio4.Checked)
            {
                sql += " c.QuNo AS quotebom_QuNo, c.BomID AS quotebom_BomID, c.BomRec AS quotebom_BomRec,";
                sql += " c.itno AS quotebom_itno, c.itname AS quotebom_itname, c.itunit AS quotebom_itunit, c.itqty AS quotebom_itqty, ";
                sql += " c.itpareprs AS quotebom_itpareprs, c.itpkgqty AS quotebom_itpkgqty, c.itrec AS quotebom_itrec, ";
                sql += " c.itprice AS quotebom_itprice, c.itprs AS quotebom_itprs, c.itmny AS quotebom_itmny, c.itnote AS quotebom_itnote,";
                sql += " c.ItSource AS quotebom_ItSource, c.ItBuyPri AS quotebom_ItBuyPri, c.ItBuyMny AS quotebom_ItBuyMny,";
            }
            sql += " b.quid AS quoted_quid, b.quno AS quoted_quno, b.qudate1 AS quoted_qudate1, b.qudate2 AS quoted_qudate2,";
            sql += " b.qudates1 AS quoted_qudates1, b.qudates2 AS quoted_qudates2, b.cuno AS quoted_cuno, b.emno AS quoted_emno,";
            sql += " b.xa1no AS quoted_xa1no, b.xa1par AS quoted_xa1par, b.trno AS quoted_trno, b.itno AS quoted_itno,";
            sql += " b.itname AS quoted_itname, b.ittrait AS quoted_ittrait, b.itunit AS quoted_itunit, b.itpkgqty AS quoted_itpkgqty, ";
            sql += " b.qty AS quoted_qty, b.price AS quoted_price, b.prs AS quoted_prs, b.rate AS quoted_rate, ";
            sql += " b.taxprice AS quoted_taxprice, b.mny AS quoted_mny, b.priceb AS quoted_priceb, b.taxpriceb AS quoted_taxpriceb,";
            sql += " b.mnyb AS quoted_mnyb, b.memo AS quoted_memo, b.lowzero AS quoted_lowzero, b.bomid AS quoted_bomid,";
            sql += " b.bomrec AS quoted_bomrec, b.recordno AS quoted_recordno, b.sltflag AS quoted_sltflag, b.extflag AS quoted_extflag,";
            sql += " b.itdesp1 AS quoted_itdesp1, b.itdesp2 AS quoted_itdesp2, b.itdesp3 AS quoted_itdesp3, ";
            sql += " b.itdesp4 AS quoted_itdesp4, b.itdesp5 AS quoted_itdesp5, b.itdesp6 AS quoted_itdesp6, ";
            sql += " b.itdesp7 AS quoted_itdesp7, b.itdesp8 AS quoted_itdesp8, b.itdesp9 AS quoted_itdesp9,";
            sql += " b.itdesp10 AS quoted_itdesp10,b.pqty,b.punit, b.stName AS quoted_stName, b.QuDate AS quoted_QuDate,";
            sql += " b.QuDates AS quoted_QuDates,b.standard AS quoted_standard , ";//20151210補型號standard
            sql += " d.cufax1 AS cust_cufax1, d.cuaddr1 AS cust_cuaddr1, d.cuuno AS cust_cuuno,d.cuname2 AS cust_cuname2,d.cuatel1 AS cust_cuatel1,";
            sql += " i.itpicture AS item_itpicture , i.pic AS item_pic , i.itnote AS item_itnote ,i.itnoudf ,DATALENGTH(pic)長度 ,i.kino ,kind.kiname ,";
            sql += " scrit.scname as scname,scrit.scname1 as scname1,  ";
            sql += " empl.EmAtel1,empl.EmEmail";
            sql += " FROM quoted AS b LEFT JOIN quote AS a ON a.quno = b.quno";
            sql += " left join cust as d on d.cuno=b.cuno";
            sql += " left join item as i on b.itno=i.itno";
            sql += " left join kind on i.kino = kind.kino";
            sql += " left join scrit ON a.appscno = scrit.scname";
            sql += " left join empl on a.emno = empl.emno";
            if (radio3.Checked || radio4.Checked)
            {
                sql += " LEFT JOIN QuoteBom AS c ON c.BomID = b.bomid";
            }
        }
        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(QuNo1.Text, QuNo.Text) < 0)
            {
                MessageBox.Show("終止報價憑證不可小於起始報價憑證", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                FastReport列印(path,mode);
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
                if (Common.Sys_DBqty == 1) ReportPath += "_簡要表";
                else ReportPath += "_簡要表P";
            }
            else if (radio2.Checked)
            {
                if (Common.Sys_DBqty == 1) ReportPath += "_規格說明";
                else ReportPath += "_規格說明P";
            }
            else if (radio3.Checked)
            {
                if (Common.Sys_DBqty == 1) ReportPath += "_組件明細";
                else ReportPath += "_組件明細P";
            }
            else if (radio4.Checked)
            {
                if (Common.Sys_DBqty == 1) ReportPath += "_組件明細_規格";
                else ReportPath += "_組件明細_規格P";
            }
            else if (radio5.Checked)
            {
                if (Common.Sys_DBqty == 1) ReportPath += "_簡要自定一";
                else ReportPath += "_簡要自定一P";
            }
            else if (radio6.Checked)
            {
                if (Common.Sys_DBqty == 1) ReportPath += "_簡要自定二";
                else ReportPath += "_簡要自定二P";
            }
            else if (radio7.Checked)
            {
                if (Common.Sys_DBqty == 1) ReportPath += "_組合自定一";
                else ReportPath += "_組合自定一P";
            }
            else if (radio8.Checked)
            {
                if (Common.Sys_DBqty == 1) ReportPath += "_組合自定二";
                else ReportPath += "_組合自定二P";
            }

            return Common.判斷開啟報表類型(ReportPath);
        }
        void 水晶列印(string path,RptMode mode)
        {
            try
            {
                setsql();
                sql += " where '0'='0'";
                if (QuNo.Text != "")
                    sql += " and a.quno >=@quno ";
                if (QuNo1.Text != "")
                    sql += " and a.quno <=@quno1 ";

                sql += " order by quoted_quid ";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("quno", QuNo.Text);
                    cmd.Parameters.AddWithValue("quno1", QuNo1.Text);
                    cmd.CommandText = sql;
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        dt.Clear();
                        dd.Fill(dt);
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    QuNo.Focus();
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
                if (num.Find(p => p.Name == "列印日期") != null)
                {
                    oRpt.SetParameterValue("列印日期", Date.AddLine(Date.GetDateTime(Common.User_DateTime)));
                }

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "select Top(1)stctel,stcfax,stcaddr1 from systemset";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            if (num.Find(p => p.Name == "公司電話") != null)
                            {
                                oRpt.SetParameterValue("公司電話", reader["stctel"].ToString());
                            }
                            if (num.Find(p => p.Name == "公司傳真") != null)
                            {
                                oRpt.SetParameterValue("公司傳真", reader["stcfax"].ToString());
                            }
                            if (num.Find(p => p.Name == "公司地址") != null)
                            {
                                oRpt.SetParameterValue("公司地址", reader["stcaddr1"].ToString());
                            }
                        }
                    }
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
            printTB.TableName = "QUOTE_";
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                sql = "";
                sql += "SELECT a.quno AS quote_報價單號, a.qudate1 AS quote_報價日期_西元, a.qudates1 AS quote_預計交期_西元, a.qudates2 AS quote_預計交期_保留, ";
                sql += " a.cono AS quote_公司編號, a.coname1 AS quote_公司簡稱, a.coname2 AS quote_公司名稱, a.cuno AS quote_客戶編號,";
                sql += " a.cuname2 AS quote_客戶名稱, a.cuname1 AS quote_客戶簡稱, a.cutel1 AS quote_客戶電話, a.cuper1 AS quote_客戶聯絡人, ";
                sql += " a.emno AS quote_業務編號, a.emname AS quote_業務姓名, a.xa1no AS quote_幣別編號, a.xa1name AS quote_幣別名稱, ";
                sql += " a.xa1par AS quote_匯率, a.trno AS quote_報價類別編號, a.trname AS quote_報價類別名稱, a.taxmnyf AS quote_taxmnyf, ";
                sql += " a.taxmnyb AS quote_本幣合計, a.taxmny AS quote_外幣合計, a.x3no AS quote_稅別編號, a.rate AS quote_稅率, ";
                sql += " a.tax AS quote_外幣營業稅額, a.totmny AS quote_外幣報價總額, a.taxb AS quote_本幣營業稅額, a.totmnyb AS quote_本幣報價總額, ";
                sql += " a.qupayment AS quote_付款條件, a.quperiod AS quote_有效期限, a.qumemo AS quote_備註,";
                sql += " a.recordno AS quote_recordno, a.UsrNo AS quote_UsrNo, a.QuDate AS quote_QuDate, a.QuDates AS quote_QuDates,a.qumemo1 AS quote_詳細備註,a.ischeck AS quote_ischeck,";
                if (radio3.Checked || radio4.Checked)
                {
                    sql += " c.QuNo AS quotebom_報價單號, c.BomID AS quotebom_BomID, c.BomRec AS quotebom_BomRec,";
                    sql += " c.itno AS quotebom_產品編號, c.itname AS quotebom_品名規格, c.itunit AS quotebom_單位, c.itqty AS quotebom_標準用量, ";
                    sql += " c.itpareprs AS quotebom_母件比率, c.itpkgqty AS quotebom_包裝數量, c.itrec AS quotebom_itrec, ";
                    sql += " c.itprice AS quotebom_單價, c.itprs AS quotebom_折數, c.itmny AS quotebom_金額, c.itnote AS quotebom_說明,";
                    sql += " c.ItSource AS quotebom_ItSource, c.ItBuyPri AS quotebom_ItBuyPri, c.ItBuyMny AS quotebom_ItBuyMny,";
                }
                sql += " b.quid AS quoted_quid, b.quno AS quoted_報價單號, b.qudate1 AS quoted_報價日期_西元, b.qudate2 AS quoted_報價日期_保留,";
                sql += " b.qudates1 AS quoted_預計交期_西元, b.qudates2 AS quoted_預計交期_保留, b.cuno AS quoted_客戶編號, b.emno AS quoted_業務編號,";
                sql += " b.xa1no AS quoted_幣別編號, b.xa1par AS quoted_匯率, b.trno AS quoted_報價類別編號, b.itno AS quoted_產品編號,";
                sql += " b.itname AS quoted_品名規格, b.ittrait AS quoted_產品組成, b.itunit AS quoted_單位, b.itpkgqty AS quoted_包裝數量, ";
                sql += " b.qty AS quoted_數量, b.price AS quoted_外幣單價, b.prs AS quoted_折數, b.rate AS quoted_稅率, ";
                sql += " b.taxprice AS quoted_外幣稅前單價, b.mny AS quoted_外幣稅前金額, b.priceb AS quoted_本幣單價, b.taxpriceb AS quoted_本幣稅前單價,";
                sql += " b.mnyb AS quoted_本幣稅前金額, b.memo AS quoted_說明, b.lowzero AS quoted_庫存量不足, b.bomid AS quoted_bomid,";
                sql += " b.bomrec AS quoted_bomrec, b.recordno AS quoted_recordno, b.sltflag AS quoted_sltflag, b.extflag AS quoted_extflag,";
                sql += " b.itdesp1 AS quoted_規格說明1, b.itdesp2 AS quoted_規格說明2, b.itdesp3 AS quoted_規格說明3, ";
                sql += " b.itdesp4 AS quoted_規格說明4, b.itdesp5 AS quoted_規格說明5, b.itdesp6 AS quoted_規格說明6, ";
                sql += " b.itdesp7 AS quoted_規格說明7, b.itdesp8 AS quoted_規格說明8, b.itdesp9 AS quoted_規格說明9,";
                sql += " b.itdesp10 AS quoted_規格說明10,b.pqty,b.punit, b.stName AS quoted_stName, b.QuDate AS quoted_QuDate,";
                sql += " b.QuDates AS quoted_QuDates,b.standard AS quoted_standard , ";//20151210補型號standard
                sql += " d.cufax1 AS cust_客戶傳真, d.cuaddr1 AS cust_公司地址, d.cuuno AS cust_統一編號,d.cuname2 AS cust_客戶名稱,d.cuatel1 AS cust_客戶行動電話,";
                sql += " i.itpicture AS item_itpicture , i.pic AS item_pic , i.itnote AS item_備註 ,i.itnoudf ,DATALENGTH(pic)長度 ,i.kino ,kind.kiname ,";
                sql += " scrit.scname as 使用者編號,scrit.scname1 as 使用者名稱,  ";
                sql += " empl.EmAtel1,empl.EmEmail";
                sql += " FROM quoted AS b LEFT JOIN quote AS a ON a.quno = b.quno";
                sql += " left join cust as d on d.cuno=b.cuno";
                sql += " left join item as i on b.itno=i.itno";
                sql += " left join kind on i.kino = kind.kino";
                sql += " left join scrit ON a.appscno = scrit.scname";
                sql += " left join empl on a.emno = empl.emno";
                if (radio3.Checked || radio4.Checked)
                {
                    sql += " LEFT JOIN QuoteBom AS c ON c.BomID = b.bomid";
                }

                sql += " where '0'='0'";
                if (QuNo.Text != "")
                    sql += " and a.quno >=@quno ";
                if (QuNo1.Text != "")
                    sql += " and a.quno <=@quno1 ";

                sql += " order by quoted_quid ";

                cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                cmd.Parameters.AddWithValue("quno1", QuNo1.Text.Trim());
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

                FastReport.PreView(path, printTB, "QUOTE_", null, null, mode, ReportFileName);
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



        private void QuNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("quno", ((TextBox)sender).Text.Trim());
                    cmd.CommandText = "select count(quno) from quote where quno=@quno";
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        e.Cancel = true;
                        using (var frm = new FrmQuote_Print_QuNo())
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
        private void QuNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmQuote_Print_QuNo())
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

            pVar.SaveRadioUdf(pnlist, "Quote");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SetRadioUdf(pnlist, "Quote");
        }

        private void FrmQuote_Print_Shown(object sender, EventArgs e)
        {
            QuNo.Focus();
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
