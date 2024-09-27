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
    public partial class FrmOrder_Print : Formbase
    {
        public string PK = "";
        string ReportFileName = "";
        string ReportPath = "";
        string sql;
        DataTable rpt = new DataTable();
        DataTable dt = new DataTable();

        public FrmOrder_Print()
        {
            InitializeComponent();
        }

        private void FrmOrder_print_Load(object sender, EventArgs e)
        {
            ReportFileName = "客戶訂單管理";
            string kind = Common.Sys_DBqty == 1 ? "" : "P";

            radio5.判斷有無CF或RF("客戶訂單管理_簡要自定一" + kind);
            radio6.判斷有無CF或RF("客戶訂單管理_簡要自定二" + kind);
            radio7.判斷有無CF或RF("客戶訂單管理_組合自定一" + kind);
            radio8.判斷有無CF或RF("客戶訂單管理_組合自定二" + kind);

            ToolTip tip = new ToolTip();
            tip.SetToolTip(radio1, "OrderRpt1");
            tip.SetToolTip(radio2, "OrderRpt2");
            tip.SetToolTip(radio3, "OrderRpt3");
            tip.SetToolTip(radio4, "OrderRpt4");
            tip.SetToolTip(radio9, "OrderRpt9");
            OrNo.Focus();
            OrNo.Text = PK;
            OrNo1.Text = PK;

            SetRdUdf();
        }

        void dataintodocument(RptMode mode)
        {
            if (string.CompareOrdinal(OrNo1.Text, OrNo.Text) < 0)
            {
                MessageBox.Show("終止訂單編號不可小於起始訂單編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string otherRpt = "";
            string kind = Common.Sys_DBqty == 1 ? "" : "P";

            //先判斷額外報表有沒有
            if (radio1.Checked)
                otherRpt = Common.reportaddress + "Report\\OrderRpt1";
            else if (radio2.Checked)
                otherRpt = Common.reportaddress + "Report\\OrderRpt2";
            else if (radio3.Checked)
                otherRpt = Common.reportaddress + "Report\\OrderRpt3";
            else if (radio4.Checked)
                otherRpt = Common.reportaddress + "Report\\OrderRpt4";
            else if (radio9.Checked)
                otherRpt = Common.reportaddress + "Report\\OrderRpt9";

            var hasreport = File.Exists(otherRpt + ".rpt");
            if (hasreport)
                return otherRpt + ".rpt";

            hasreport = File.Exists(otherRpt.Replace("Report", "ReportNew") + ".frx");
            if (hasreport)
                return otherRpt + ".frx";

            if (radio1.Checked)
                ReportPath += "_簡要表" + kind;
            else if (radio2.Checked)
                ReportPath += "_規格說明" + kind;
            else if (radio3.Checked)
                ReportPath += "_組件明細" + kind;
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
            else if (radio9.Checked)
                ReportPath += "_生產單" + kind;

            return Common.判斷開啟報表類型(ReportPath);
        }
        void setsql()
        {
            sql = "";
            sql += "SELECT ";
            sql += "a.AdAddr as order_AdAddr,a.orno AS order_orno, a.ordate AS order_ordate, a.ordate1 AS order_ordate1, a.ordate2 AS order_ordate2, ";
            sql += "a.quno AS order_quno, a.ortrnflag AS order_ortrnflag, a.oroverflag AS order_oroverflag, a.cono AS order_cono,";
            sql += "a.coname1 AS order_coname1, a.coname2 AS order_coname2, a.cuno AS order_cuno, a.cuname2 AS order_cuname2, ";
            sql += "a.cuname1 AS order_cuname1, a.cutel1 AS order_cutel1, a.cuper1 AS order_cuper1, a.emno AS order_emno, ";
            sql += "a.emname AS order_emname, a.xa1no AS order_xa1no, a.xa1name AS order_xa1name, a.xa1par AS order_xa1par, ";
            sql += "a.trno AS order_trno, a.trname AS order_trname, a.taxmnyf AS order_taxmnyf, a.taxmnyb AS order_taxmnyb,";
            sql += "a.taxmny AS order_taxmny, a.x3no AS order_x3no, a.rate AS order_rate, a.tax AS order_tax, ";
            sql += "a.totmny AS order_totmny, a.taxb AS order_taxb, a.totmnyb AS order_totmnyb, a.orpayment AS order_orpayment, ";
            sql += "a.orperiod AS order_orperiod, a.ormemo AS order_ormemo, a.ormemo1 AS order_ormemo1, ";
            sql += "a.recordno AS order_recordno, a.RevMoney AS order_RevMoney, a.UsrNo AS order_UsrNo, ";
            sql += "a.MeMain AS order_MeMain, a.MeOther AS order_MeOther, a.MePrint AS order_MePrint, a.MeSize AS order_MeSize, ";
            sql += "a.MeSize2 AS order_MeSize2, a.AppDate AS order_AppDate, a.EdtDate AS order_EdtDate, ";
            sql += "a.AppScNo AS order_AppScNo, a.EdtScNo AS order_EdtScNo,a.ormemo1 AS order_ormemo1, ";
            if (radio3.Checked || radio4.Checked || radio9.Checked)
            {
                sql += "c.BomID AS orderbom_BomID, c.BomRec AS orderbom_BomRec, c.itno AS orderbom_itno,c.OrNo AS orderbom_OrNo, ";
                sql += "c.itname AS orderbom_itname, c.itunit AS orderbom_itunit, c.itqty AS orderbom_itqty, ";
                sql += "c.itpareprs AS orderbom_itpareprs, c.itpkgqty AS orderbom_itpkgqty, c.itrec AS orderbom_itrec,";
                sql += "c.itprice AS orderbom_itprice, c.itprs AS orderbom_itprs, c.itmny AS orderbom_itmny, c.itnote AS orderbom_itnote, ";
                sql += "c.ItSource AS orderbom_ItSource, c.ItBuyPri AS orderbom_ItBuyPri, c.ItBuyMny AS orderbom_ItBuyMny, ";
            }
            sql += "b.OrID AS orderd_OrID, b.orno AS orderd_orno, b.ordate AS orderd_ordate, b.ordate1 AS orderd_ordate1, ";
            sql += "b.ordate2 AS orderd_ordate2, b.quno AS orderd_quno, b.ortrnflag AS orderd_ortrnflag, b.cuno AS orderd_cuno,";
            sql += "b.emno AS orderd_emno, b.xa1no AS orderd_xa1no, b.xa1par AS orderd_xa1par, b.trno AS orderd_trno, ";
            sql += "b.itno AS orderd_itno, b.itname AS orderd_itname, b.ittrait AS orderd_ittrait, b.itunit AS orderd_itunit, ";
            sql += "b.itpkgqty AS orderd_itpkgqty, b.qty AS orderd_qty, b.price AS orderd_price, b.prs AS orderd_prs, ";
            sql += "b.rate AS orderd_rate, b.taxprice AS orderd_taxprice, b.mny AS orderd_mny, b.priceb AS orderd_priceb, ";
            sql += "b.taxpriceb AS orderd_taxpriceb, b.mnyb AS orderd_mnyb, b.qtyout AS orderd_qtyout, b.qtyin AS orderd_qtyin, ";
            sql += "b.esdate AS orderd_esdate, b.esdate1 AS orderd_esdate1, b.esdate2 AS orderd_esdate2, ";
            sql += "b.stkqtyflag AS orderd_stkqtyflag, b.memo AS orderd_memo, b.lowzero AS orderd_lowzero, b.bomid AS orderd_bomid,";
            sql += "b.bomrec AS orderd_bomrec, b.recordno AS orderd_recordno, b.sltflag AS orderd_sltflag, b.extflag AS orderd_extflag, ";
            sql += "b.itdesp1 AS orderd_itdesp1, b.itdesp2 AS orderd_itdesp2, b.itdesp3 AS orderd_itdesp3, b.itdesp4 AS orderd_itdesp4,";
            sql += "b.itdesp5 AS orderd_itdesp5, b.itdesp6 AS orderd_itdesp6, b.itdesp7 AS orderd_itdesp7, b.itdesp8 AS orderd_itdesp8, ";
            sql += "b.itdesp9 AS orderd_itdesp9, b.itdesp10 AS orderd_itdesp10, b.stName AS orderd_stName, ";
            sql += "b.Punit AS orderd_Punit, b.Pqty AS orderd_Pqty, b.mqty AS orderd_mqty, b.munit AS orderd_munit, ";
            sql += "b.mlong AS orderd_mlong, b.mwidth1 AS orderd_mwidth1, b.mwidth2 AS orderd_mwidth2, ";
            sql += "b.mwidth3 AS orderd_mwidth3, b.mwidth4 AS orderd_mwidth4, b.mformula AS orderd_mformula, b.standard AS orderd_standard, ";
            sql += "b.qtyNotOut AS orderd_qtyNotOut, b.qtyNotInStk AS orderd_qtyNotInStk, d.cuno AS cust_cuno, ";
            sql += "d.cuname2 AS cust_cuname2, d.cutel1 AS cust_cutel1, d.cufax1 AS cust_cufax1, d.cuatel1 AS cust_cuatel1 ,d.cuaddr1 AS cust_cuaddr1,d.cuaddr2 AS cust_cuaddr2,d.cuaddr3 AS cust_cuaddr3,d.cuuno AS cust_cuuno,t.itpicture AS item_itpicture,t.pic AS item_pic,  DATALENGTH(t.pic) as pic長度,";
            sql += "scrit.scname as scname,scrit.scname1 as scname1,x4.X4No,x4.X4Name,x4.X4CoNo  ";

            sql += " FROM orderd AS b LEFT JOIN ";
            sql += " [order] AS a ON a.orno = b.orno LEFT  JOIN ";
            sql += " cust AS d ON d.cuno = a.cuno ";
            sql += " LEFT JOIN item AS t on t.itno=b.itno ";
            sql += " LEFT JOIN scrit ON a.appscno = scrit.scname ";
            sql += " LEFT JOIN XX04 as x4 ON x4.X4No = d.CUX4NO ";

            if (radio3.Checked || radio4.Checked || radio9.Checked)
            {

                sql += "LEFT JOIN OrderBom AS c ON b.bomid = c.BomID";
            }

        }
        void 水晶列印(string path, RptMode mode)
        {
            try
            {
                setsql();
                sql += " where '0'='0'";
                if (OrNo.Text != "")
                    sql += " and a.orno >=@orno";
                if (OrNo1.Text != "")
                    sql += " and a.orno <=@orno1";

                sql += " order by orderd_OrID ";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    if (OrNo.Text != "") cmd.Parameters.AddWithValue("orno", OrNo.Text);
                    if (OrNo.Text != "") cmd.Parameters.AddWithValue("orno1", OrNo1.Text);
                    cmd.CommandText = sql;
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        dt.Clear();
                        dd.Fill(dt);
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    OrNo.Focus();
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
                if (Txt.Find(t => t.Name == "tCoName") != null)
                {
                    //公司抬頭
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["tCoName"];
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
            printTB.TableName = "ORDER_";
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                sql = "";
                sql += "SELECT ";
                sql += "a.AdAddr as order_送貨地址,a.orno AS order_訂單單號, a.ordate AS order_訂單日期, a.ordate1 AS order_訂單日期_西元, a.ordate2 AS order_訂單日期_保留, ";
                sql += "a.quno AS order_報價憑證, a.ortrnflag AS order_是否已轉採購單, a.oroverflag AS order_結案標示, a.cono AS order_公司編號,";
                sql += "a.coname1 AS order_公司簡稱, a.coname2 AS order_公司名稱, a.cuno AS order_客戶編號, a.cuname2 AS order_客戶名稱, ";
                sql += "a.cuname1 AS order_客戶簡稱, a.cutel1 AS order_客戶電話, a.cuper1 AS order_客戶聯絡人, a.emno AS order_業務編號, ";
                sql += "a.emname AS order_業務姓名, a.xa1no AS order_幣別編號, a.xa1name AS order_幣別名稱, a.xa1par AS order_匯率, ";
                sql += "a.trno AS order_報價類別編號, a.trname AS order_報價類別名稱, a.taxmnyf AS order_taxmnyf, a.taxmnyb AS order_本幣合計,";
                sql += "a.taxmny AS order_外幣合計, a.x3no AS order_稅別編號, a.rate AS order_稅率, a.tax AS order_外幣營業稅額, ";
                sql += "a.totmny AS order_外幣訂單總額, a.taxb AS order_本幣營業稅額, a.totmnyb AS order_本幣訂單總額, a.orpayment AS order_付款條件, ";
                sql += "a.orperiod AS order_有效期限, a.ormemo AS order_備註, a.ormemo1 AS order_詳細備註, ";
                sql += "a.recordno AS order_recordno, a.RevMoney AS order_RevMoney, a.UsrNo AS order_UsrNo, ";
                sql += "a.MeMain AS order_MeMain, a.MeOther AS order_MeOther, a.MePrint AS order_MePrint, a.MeSize AS order_MeSize, ";
                sql += "a.MeSize2 AS order_MeSize2, a.AppDate AS order_AppDate, a.EdtDate AS order_EdtDate, ";
                sql += "a.AppScNo AS order_AppScNo, a.EdtScNo AS order_EdtScNo, ";
                if (radio3.Checked || radio4.Checked || radio9.Checked)
                {
                    sql += "c.BomID AS orderbom_BomID, c.BomRec AS orderbom_BomRec, c.itno AS orderbom_產品編號,c.OrNo AS orderbom_訂單編號, ";
                    sql += "c.itname AS orderbom_品名規格, c.itunit AS orderbom_單位, c.itqty AS orderbom_標準用量, ";
                    sql += "c.itpareprs AS orderbom_母件比率, c.itpkgqty AS orderbom_包裝數量, c.itrec AS orderbom_itrec,";
                    sql += "c.itprice AS orderbom_單價, c.itprs AS orderbom_折數, c.itmny AS orderbom_金額, c.itnote AS orderbom_說明, ";
                    sql += "c.ItSource AS orderbom_ItSource, c.ItBuyPri AS orderbom_ItBuyPri, c.ItBuyMny AS orderbom_ItBuyMny, ";
                }
                sql += "b.OrID AS orderd_OrID, b.orno AS orderd_訂單單號, b.ordate AS orderd_訂單日期, b.ordate1 AS orderd_訂單日期_西元, ";
                sql += "b.ordate2 AS orderd_訂單日期_保留, b.quno AS orderd_報價憑證, b.ortrnflag AS orderd_是否已轉採購單, b.cuno AS orderd_客戶編號,";
                sql += "b.emno AS orderd_業務編號, b.xa1no AS orderd_幣別編號, b.xa1par AS orderd_匯率, b.trno AS orderd_報價類別編號, ";
                sql += "b.itno AS orderd_產品編號, b.itname AS orderd_品名規格, b.ittrait AS orderd_產品組成, b.itunit AS orderd_單位, ";
                sql += "b.itpkgqty AS orderd_包裝數量, b.qty AS orderd_數量, b.price AS orderd_單價, b.prs AS orderd_折數, ";
                sql += "b.rate AS orderd_稅率, b.taxprice AS orderd_外幣稅前單價, b.mny AS orderd_外幣稅前金額, b.priceb AS orderd_本幣單價, ";
                sql += "b.taxpriceb AS orderd_本幣稅前單價, b.mnyb AS orderd_本幣稅前金額, b.qtyout AS orderd_訂單已交量, b.qtyin AS orderd_已入庫數量, ";
                sql += "b.esdate AS orderd_交貨日_民國, b.esdate1 AS orderd_交貨日_西元, b.esdate2 AS orderd_交貨日_保留, ";
                sql += "b.stkqtyflag AS orderd_stkqtyflag, b.memo AS orderd_備註, b.lowzero AS orderd_lowzero, b.bomid AS orderd_bomid,";
                sql += "b.bomrec AS orderd_bomrec, b.recordno AS orderd_recordno, b.sltflag AS orderd_sltflag, b.extflag AS orderd_extflag, ";
                sql += "b.itdesp1 AS orderd_規格說明1, b.itdesp2 AS orderd_規格說明2, b.itdesp3 AS orderd_規格說明3, b.itdesp4 AS orderd_規格說明4,";
                sql += "b.itdesp5 AS orderd_規格說明5, b.itdesp6 AS orderd_規格說明6, b.itdesp7 AS orderd_規格說明7, b.itdesp8 AS orderd_規格說明8, ";
                sql += "b.itdesp9 AS orderd_規格說明9, b.itdesp10 AS orderd_規格說明10, b.stName AS orderd_stName, ";
                sql += "b.Punit AS orderd_Punit, b.Pqty AS orderd_Pqty, b.mqty AS orderd_mqty, b.munit AS orderd_munit, ";
                sql += "b.mlong AS orderd_mlong, b.mwidth1 AS orderd_mwidth1, b.mwidth2 AS orderd_mwidth2, ";
                sql += "b.mwidth3 AS orderd_mwidth3, b.mwidth4 AS orderd_mwidth4, b.mformula AS orderd_mformula, b.standard AS orderd_standard, ";
                sql += "b.qtyNotOut AS orderd_訂單未交量, b.qtyNotInStk AS orderd_未入庫數量, d.cuno AS cust_客戶編號, ";
                sql += "d.cuname2 AS cust_客戶名稱, d.cutel1 AS cust_客戶電話, d.cufax1 AS cust_客戶傳真, d.cuatel1 AS cust_行動電話 ,d.cuaddr1 AS cust_公司地址,d.cuaddr2 AS cust_發票地址,d.cuaddr3 AS cust_送貨地址,d.cuuno AS cust_統一編號,t.itpicture AS item_itpicture,t.pic AS item_pic,  DATALENGTH(t.pic) as pic長度,";
                sql += "scrit.scname as 使用者編號,scrit.scname1 as 使用者名稱,x4.X4No,x4.X4Name,x4.X4CoNo  ";

                sql += " FROM orderd AS b LEFT JOIN ";
                sql += " [order] AS a ON a.orno = b.orno LEFT  JOIN ";
                sql += " cust AS d ON d.cuno = a.cuno ";
                sql += " LEFT JOIN item AS t on t.itno=b.itno ";
                sql += " LEFT JOIN scrit ON a.appscno = scrit.scname ";
                sql += " LEFT JOIN XX04 as x4 ON x4.X4No = d.CUX4NO ";

                if (radio3.Checked || radio4.Checked || radio9.Checked)
                {

                    sql += "LEFT JOIN OrderBom AS c ON b.bomid = c.BomID";
                }

                sql += " where '0'='0'";
                if (OrNo.Text != "")
                    sql += " and a.orno >=@orno";
                if (OrNo1.Text != "")
                    sql += " and a.orno <=@orno1";

                sql += " order by orderd_OrID ";

                cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                cmd.Parameters.AddWithValue("orno1", OrNo1.Text.Trim());
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

                FastReport.PreView(path, printTB, "ORDER_", null, null, mode, ReportFileName);
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



        private void OrNo_DoubleClick(object sender, EventArgs e)
        {
            using (FrmOrder_Print_OrNo frm = new FrmOrder_Print_OrNo())
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
        private void OrNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("orno", ((TextBox)sender).Text.Trim());
                    cmd.CommandText = "select count(orno) from [" + "order" + "] where orno=@orno";
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        e.Cancel = true;
                        using (var frm = new FrmOrder_Print_OrNo())
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
            pVar.SaveRadioUdf(pnlist, "[Order]");
        }
        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SetRadioUdf(pnlist, "[Order]");
        }

        private void FrmOrder_Print_Shown(object sender, EventArgs e)
        {
            OrNo.Focus();
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
