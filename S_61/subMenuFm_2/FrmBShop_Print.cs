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
    public partial class FrmBShop_Print : Formbase
    {
        JBS.JS.xEvents xe;

        DataSet ds = new DataSet();
        public string PK;
        public string FaNo;
        string ReportFileName = "";
        string ReportPath = "";
        string sql;

        public FrmBShop_Print()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmShop_Print_Load(object sender, EventArgs e)
        {
            BsNo.Text = PK;
            BsNo1.Text = PK;

            ReportFileName = "進貨作業系統";
            string kind = Common.Sys_DBqty == 1 ? "" : "P";
            ToolTip _Tip = new ToolTip();
            _Tip.SetToolTip(radio1, "BShopRpt1" + kind);

            radio2.判斷有無CF或RF("進貨作業系統_序號簡要表" + kind);
            radio5.判斷有無CF或RF("進貨作業系統_簡要自定一" + kind);
            radio7.判斷有無CF或RF("進貨作業系統_簡要自定二" + kind);

            SetRdUdf();
        }

        void dataintodocument(RptMode mode)
        {

            if (string.CompareOrdinal(BsNo1.Text, BsNo.Text) < 0)
            {
                MessageBox.Show("終止進貨單號不可小於起始進貨單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            //先判斷額外報表有沒有
            string otherRpt = "";
            if (radio1.Checked)
                otherRpt = Common.reportaddress + "Report\\BShopRpt1" + kind;

            var hasreport = File.Exists(otherRpt + ".rpt");
            if (hasreport)
                return otherRpt + ".rpt";

            hasreport = File.Exists(otherRpt.Replace("Report", "ReportNew") + ".frx");
            if (hasreport)
                return otherRpt + ".frx";

            if (radio1.Checked)
                ReportPath += "_簡要表" + kind;
            else if (radio2.Checked)
                ReportPath += "_序號簡要表" + kind;
            else if (radio5.Checked)
                ReportPath += "_簡要自定一" + kind;
            else if (radio7.Checked)
                ReportPath += "_簡要自定二" + kind;

            return Common.判斷開啟報表類型(ReportPath);
        }
        void 水晶列印(string path, RptMode mode)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    ds.Clear();
                    setSql();
                    sql += " where '0'='0'";
                    if (BsNo.Text != "")
                        sql += " and bshop.BsNo >=@bsno ";

                    if (BsNo1.Text.Trim() != "")
                        sql += " and bshop.BsNo <=@bsno1 ";

                    sql += " order by bshopd.bsid";

                    cmd.Parameters.Clear();
                    if (BsNo.Text != "") cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                    if (BsNo1.Text != "") cmd.Parameters.AddWithValue("bsno1", BsNo1.Text.Trim());
                    cmd.CommandText = sql;
                    da.Fill(ds, "Data");

                    cmd.CommandText = @"
                    Select 
                    *,DATALENGTH(pic) as pic長度
                    from (
	                    Select distinct itno from bshopd where BsNo >= @bsno and BsNo <= @bsno1
                    )A
                    left join item on A.itno = item.itno ";
                    da.Fill(ds, "item");

                }

                if (ds.Tables["Data"].Rows.Count <= 0)
                {
                    BsNo.Focus();
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
                List<TextObject> tobj = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();

                if (tobj.Find(t => t.Name == "txtstart") != null)
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
                if (tobj.Find(t => t.Name == "txtend") != null)
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
                if (tobj.Find(t => t.Name == "txtadress") != null)
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
                if (tobj.Find(t => t.Name == "txttel") != null)
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
                List<ParameterField> pfld = oRpt.ParameterFields.OfType<ParameterField>().ToList();
                if (pfld.Find(p => p.Name == "列印地址") != null)
                {
                    //列印地址
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fano", FaNo.Trim());
                            cmd.CommandText = "select faAddr1 from fact where fano=@fano COLLATE Chinese_Taiwan_Stroke_BIN";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows && reader.Read())
                                    oRpt.SetParameterValue("列印地址", reader["faAddr1"].ToString());
                                else
                                    oRpt.SetParameterValue("列印地址", "");
                            }
                        }
                    }
                }
                if (pfld.Find(p => p.Name == "date") != null)
                {
                    //日期格式
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

                //報表參數設定
                if (pfld.Find(p => p.Name == "顯示千分位") != null)
                {
                    if (pVar.TRS != "")
                        pVar.ShowTRS = true;
                    oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
                }
                if (pfld.Find(p => p.Name == "千分位") != null)
                    oRpt.SetParameterValue("千分位", pVar.TRS);
                if (pfld.Find(p => p.Name == "進貨單價小數") != null)
                    oRpt.SetParameterValue("進貨單價小數", Common.MF);
                if (pfld.Find(p => p.Name == "進貨單據小數") != null)
                    oRpt.SetParameterValue("進貨單據小數", Common.MFT);
                if (pfld.Find(p => p.Name == "進項稅額小數") != null)
                    oRpt.SetParameterValue("進項稅額小數", Common.TF);

                if (pfld.Find(p => p.Name == "本幣金額小數") != null)
                    oRpt.SetParameterValue("本幣金額小數", Common.M);
                if (pfld.Find(p => p.Name == "庫存數量小數") != null)
                    oRpt.SetParameterValue("庫存數量小數", Common.Q);
                if (pfld.Find(p => p.Name == "是否顯示金額") != null)
                    oRpt.SetParameterValue("是否顯示金額", Common.User_ShopPrice);

                if (pfld.Find(p => p.Name == "備註抬頭") != null)
                    oRpt.SetParameterValue("備註抬頭", Common.Sys_MemoUdf);
                if (pfld.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);
                if (pfld.Find(p => p.Name == "進項金額小數") != null)
                    oRpt.SetParameterValue("進項金額小數", Common.TPF);

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
            printTB.TableName = "BSHOP_";
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                sql = @"SELECT 
                    bshop.bsno AS bshop_進貨單號, bshop.bsdate AS bshop_進貨日期_民國, bshop.bsdate1 AS bshop_進貨日期_西元,  bshop.bsdate2 AS bshop_進貨日期_保留, bshop.bsdateac AS bshop_帳款日期_民國, bshop.bsdateac1 AS bshop_帳款日期_西元,  bshop.bsdateac2 AS bshop_帳款日期_保留, bshop.fqno AS bshop_詢價憑證,
                    bshop.cono AS bshop_公司編號,  bshop.coname1 AS bshop_公司簡稱, bshop.coname2 AS bshop_公司名稱, bshop.fano AS bshop_廠商編號,  bshop.faname2 AS bshop_廠商名稱, bshop.faname1 AS bshop_廠商簡稱,
                    bshop.fatel1 AS bshop_廠商電話,  bshop.faper1 AS bshop_廠商聯絡人, bshop.emno AS bshop_業務編號, bshop.emname AS bshop_業務姓名,  bshop.spno AS bshop_專案編號, bshop.spname AS bshop_專案名稱, 
                    bshop.xa1no AS bshop_幣別編號,  bshop.xa1name AS bshop_幣別名稱, bshop.xa1par AS bshop_匯率, bshop.taxmnyf AS bshop_taxmnyf,  bshop.taxmnyb AS bshop_本幣合計, bshop.taxmny AS bshop_外幣合計, 
                    bshop.x3no AS bshop_稅別編號,  bshop.rate AS bshop_稅率, bshop.x5no AS bshop_發票種類, bshop.seno AS bshop_送貨編號,  bshop.sename AS bshop_送貨名稱, bshop.x4no AS bshop_結帳類別編號, bshop.x4name AS bshop_結帳類別名稱,  
                    bshop.tax AS bshop_外幣營業稅額, bshop.totmny AS bshop_外幣總額, bshop.taxb AS bshop_本幣營業稅額,  bshop.totmnyb AS bshop_本幣總額, bshop.discount AS bshop_折扣金額, bshop.cashmny AS bshop_現金金額,  bshop.cardmny AS bshop_刷卡金額, bshop.cardno AS bshop_刷卡卡號, 
                    bshop.ticket AS bshop_禮卷金額,  bshop.collectmny AS bshop_已付金額, bshop.getprvacc AS bshop_取用預付, bshop.acctmny AS bshop_未付金額,  bshop.bsmemo AS bshop_備註, bshop.bsmemo1 AS bshop_詳細備註, 
                    bshop.bracket AS bshop_表單類型,  bshop.recordno AS bshop_recordno, bshop.invno AS bshop_發票編號, bshop.invdate AS bshop_發票日期_民國,  bshop.invdate1 AS bshop_發票日期_西元, bshop.invname AS bshop_發票抬頭, bshop.invtaxno AS bshop_統一編號,  bshop.invaddr1 AS bshop_發票地址, 
                    bshop.transport AS bshop_運費, bshop.tranpar AS bshop_外幣運費匯率,  bshop.transportb AS bshop_本幣運費匯率, bshop.premium AS bshop_外幣保險費, bshop.prempar AS bshop_保險費匯率,  bshop.premiumb AS bshop_本幣保險費, 
                    bshop.tariff AS bshop_關稅, bshop.taripar AS bshop_外幣關稅匯率,  bshop.tariffb AS bshop_本幣關稅匯率, bshop.postal AS bshop_郵電費, bshop.procfee AS bshop_手續費,  bshop.certif AS bshop_簽證費, bshop.clearance AS bshop_報關費, bshop.otherfee AS bshop_其他費用,  bshop.totfee AS bshop_費用合計, bshop.allot AS bshop_費用分配, 
                    bshop.appdate AS bshop_新增日期時間,  bshop.edtdate AS bshop_修改日期時間, bshop.appscno AS bshop_登錄人員, bshop.edtscno AS bshop_最後修改人員,  
                    bshop.acno AS bshop_傳票編號, bshop.cloflag AS bshop_年結標示, bshop.stno AS bshop_進貨倉庫編號,  bshop.stname AS bshop_進貨倉庫名稱, bshop.AvgKind AS bshop_AvgKind, bshop.AllMny AS bshop_AllMny,  bshop.UsrNo AS bshop_UsrNo, bshop.BsShare AS bshop_費用分攤, bshop.BsShsel AS bshop_分攤方式,  

                    bshopd.bsid AS bshopd_bsid, bshopd.bsno AS bshopd_進貨單號, bshopd.bsdate AS bshopd_進貨日期_民國,  bshopd.bsdate1 AS bshopd_進貨日期_西元, bshopd.bsdate2 AS bshopd_進貨日期_保留, bshopd.bsdateac AS bshopd_帳款日期_民國,  bshopd.bsdateac1 AS bshopd_帳款日期_西元, bshopd.bsdateac2 AS bshopd_帳款日期_保留, bshopd.fqno AS bshopd_詢價憑證,  
                    bshopd.cono AS bshopd_公司編號, bshopd.fano AS bshopd_廠商編號, bshopd.emno AS bshopd_業務編號,  bshopd.spno AS bshopd_專案編號, bshopd.stno AS bshopd_進貨倉庫編號, bshopd.xa1no AS bshopd_幣別編號,  bshopd.xa1par AS bshopd_匯率, bshopd.seno AS bshopd_送貨編號, bshopd.sename AS bshopd_送貨名稱,  bshopd.x4no AS bshopd_結帳類別編號, bshopd.x4name AS bshopd_結帳類別名稱, 
                    bshopd.fono AS bshopd_採購單號,  bshopd.itno AS bshopd_產品編號, bshopd.itname AS bshopd_品名規格, bshopd.ittrait AS bshopd_產品組成,  bshopd.itunit AS bshopd_單位, bshopd.itpkgqty AS bshopd_包裝數量, bshopd.qty AS bshopd_數量,  bshopd.price AS bshopd_外幣單價, bshopd.prs AS bshopd_折數, bshopd.rate AS bshopd_稅率,  bshopd.taxprice AS bshopd_外幣稅前單價, bshopd.mny AS bshopd_外幣稅前金額, 
                    bshopd.priceb AS bshopd_本幣單價,  bshopd.taxpriceb AS bshopd_本幣稅前單價, bshopd.mnyb AS bshopd_本幣稅前金額, bshopd.memo AS bshopd_說明,  bshopd.lowzero AS bshopd_庫存量不足, bshopd.bomid AS bshopd_bomid, 
                    bshopd.bomrec AS bshopd_bomrec,  bshopd.recordno AS bshopd_recordno, bshopd.sltflag AS bshopd_sltflag, bshopd.extflag AS bshopd_extflag,  bshopd.bracket AS bshopd_表單類型, 
                    bshopd.itdesp1 AS bshopd_規格說明1, bshopd.itdesp2 AS bshopd_規格說明2,  bshopd.itdesp3 AS bshopd_規格說明3, bshopd.itdesp4 AS bshopd_規格說明4, bshopd.itdesp5 AS bshopd_規格說明5,  
                    bshopd.itdesp6 AS bshopd_規格說明6, bshopd.itdesp7 AS bshopd_規格說明7, bshopd.itdesp8 AS bshopd_規格說明8,bshopd.itdesp9 AS bshopd_規格說明9, bshopd.itdesp10 AS bshopd_規格說明10, 
                    bshopd.serno AS bshopd_serno,  bshopd.StName AS bshopd_StName, bshopd.PriceF AS bshopd_PriceF,  bshopd.RecordNo_D AS bshopd_RecordNo_D, bshopd.RealCost AS bshopd_RealCost,  bshopd.Pqty AS bshopd_Pqty,  bshopd.Punit AS bshopd_Punit, bshopd.mformula AS bshopd_mformula, bshopd.mwidth4 AS bshopd_mwidth4,  bshopd.mwidth3 AS bshopd_mwidth3, bshopd.mwidth2 AS bshopd_mwidth2, bshopd.mwidth1 AS bshopd_mwidth1,  bshopd.mlong AS bshopd_mlong, bshopd.munit AS bshopd_munit, bshopd.mqty AS bshopd_mqty,  bshopd.foid AS bshopd_foid, 

                    fact.faname1 AS fact_廠商簡稱, fact.faxa1no AS fact_幣別編號, fact.facono AS fact_隸屬公司, fact.faime AS fact_注音速查,  fact.fax12no AS fact_廠商類別, fact.faemno1 AS fact_業務人員, fact.faper1 AS fact_廠商聯絡人1, fact.faper2 AS fact_廠商聯絡人2,  fact.faper AS fact_負責人, fact.fatel1 AS fact_電話1, fact.fatel2 AS fact_電話2, fact.fatel3 AS fact_電話3,  fact.fafax1 AS fact_廠商傳真, fact.faatel1 AS fact_廠商行動電話1, fact.faatel2 AS fact_廠商行動電話2, fact.fabbc AS fact_廠商呼叫器,  
                    fact.faaddr1 AS fact_公司地址, fact.far1 AS fact_公司郵遞區號, fact.faaddr2 AS fact_發票地址, fact.far2 AS fact_發票郵遞區號,  fact.faaddr3 AS fact_工廠地址, fact.far3 AS fact_工廠郵遞區號, fact.fawork AS fact_營業項目, fact.faemail AS fact_電子信箱,  fact.fawww AS fact_網址, fact.facredit AS fact_信用額度, fact.fauno AS fact_統一編號, fact.fax3no AS fact_營業稅,  fact.fax4no AS fact_結帳類別, fact.fax5no AS fact_發票模式, fact.faengname AS fact_英文抬頭,  fact.faengaddr AS fact_英文住址, 
                    fact.faengr1 AS fact_英文住址郵遞區號, fact.famemo1 AS fact_備註1,  fact.famemo2 AS fact_備註2, fact.faarea AS fact_地區類別, fact.fax2no AS fact_區域名稱, fact.faudf1 AS fact_自訂1,  fact.faudf2 AS fact_自訂2, fact.faudf3 AS fact_自訂3, fact.faudf4 AS fact_自訂4, fact.faudf5 AS fact_自訂5,  fact.faudf6 AS fact_自訂6, fact.falastday AS fact_最近交易_民國, fact.falastday1 AS fact_最近交易_西元,  fact.falastday2 AS fact_最近交易_保留, fact.fadate AS fact_建檔日期_民國, fact.fadate1 AS fact_建檔日期_西元,  
                    fact.fadate2 AS fact_建檔日期_保留, fact.fafirpayabl AS fact_期初應付帳款金額, fact.fasparepay AS fact_期初應付帳款餘額,  fact.fapayable AS fact_現有應付帳款金額, fact.fafirpaypar AS fact_期初應付帳款匯率, fact.fafirpayamt AS fact_期初預付金額,  fact.fapayamt AS fact_預付餘額, fact.faChkName AS fact_faChkName,scrit.scname,scrit.scname1  

                    FROM  bshop LEFT  JOIN  bshopd ON bshop.bsno = bshopd.bsno  
                    LEFT  JOIN  fact ON bshop.fano = fact.fano   
                    Left join scrit on bshop.appscno = scrit.scname

                    WHERE bshop.bsno >=@bsno and bshop.bsno <=@bsno1
                    ORDER BY bshop.bsno,bshopd.recordno";
                

                cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                cmd.Parameters.AddWithValue("bsno1", BsNo1.Text.Trim());
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

                //列印地址
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fano", FaNo.Trim());
                    cmd.CommandText = "select faAddr1 from fact where fano=@fano COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                            FastReport.dy.Add("列印地址", reader["faAddr1"].ToString());
                        else
                            FastReport.dy.Add("列印地址", "");
                    }
                }

                FastReport.dy["是否顯示金額"] = Common.User_ShopPrice;

                FastReport.PreView(path, printTB, "BSHOP_", null, null, mode, ReportFileName);
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

        private void BsNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.BShop>(sender);
        }

        private void BsNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.BShop>(sender, e, row => { });
        }

        private void btnUdf_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT6);
            pnlist.Add(groupBoxT8);
            pVar.SaveRadioUdf(pnlist, "BShop");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pVar.ResetRadioUdf("BShop");
            radio1.Checked = true;
            rdHeader1.Checked = true;
            rdFooter1.Checked = true;
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT6);
            pnlist.Add(groupBoxT8);
            pVar.SetRadioUdf(pnlist, "BShop");
        }

        private void 恆錩BaCode()
        {
            DataTable t = new DataTable();
            string str = " select 列印份數=0,列印='V',bshopd.qty as 張數,kiname as 類別名稱,item.* "
                       + " from bshopd"
                       + " left join item on bshopd.itno = item.itno"
                       + " left join kind on item.kino = kind.kino"
                       + " where 0=0";
            if (BsNo.Text.Trim().Length > 0) str += " and bshopd.bsno >=@bsno";
            if (BsNo1.Text.Trim().Length > 0) str += " and bshopd.bsno <=@bsno1";

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        if (BsNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                        if (BsNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("bsno1", BsNo1.Text.Trim());
                        cmd.CommandText = str;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (t.Rows.Count > 0)
            {
                using (var frm = new SOther.FrmBarCodeBrow())
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

        private void btnBar_Click(object sender, EventArgs e)
        {
            //恒錩印條碼
            if (File.Exists("Project1.exe"))
            {
                恆錩BaCode();
                return;
            }

            //FastReport 印條碼
            DataTable t = new DataTable();
            string str = " select 列印份數=0,列印='V',bshopd.qty as 張數,kiname as 類別名稱,item.*,fact.faname1,fact.faname2 "
                       + " ,bshopd.bsdate1 as shopd_bsdate1,bshopd.bsdate2 as shopd_bsdate2,bshopd.qty as shopd_qty,bshopd.itunit as shopd_itunit,bshopd.memo as shopd_memo "
                       + " from bshopd"
                       + " left join item on bshopd.itno = item.itno"
                       + " left join kind on item.kino = kind.kino"
                       + " left join fact on item.fano = fact.fano"
                       + " where 0=0";
            if (BsNo.Text.Trim().Length > 0) str += " and bshopd.bsno >=@bsno";
            if (BsNo1.Text.Trim().Length > 0) str += " and bshopd.bsno <=@bsno1";

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    if (BsNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                    if (BsNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("bsno1", BsNo1.Text.Trim());

                    cmd.CommandText = str;
                    da.Fill(t);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (t.Rows.Count > 0)
            {
                using (var frm = new S0.FrmPrintBarCodeb())
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





































































        void setSql()
        {
            sql = "";
            sql += " SELECT bshop.bsno AS bshop_bsno, bshop.bsdate AS bshop_bsdate, bshop.bsdate1 AS bshop_bsdate1, ";
            sql += " bshop.bsdate2 AS bshop_bsdate2, bshop.bsdateac AS bshop_bsdateac, bshop.bsdateac1 AS bshop_bsdateac1, ";
            sql += " bshop.bsdateac2 AS bshop_bsdateac2, bshop.fqno AS bshop_fqno, bshop.cono AS bshop_cono, ";
            sql += " bshop.coname1 AS bshop_coname1, bshop.coname2 AS bshop_coname2, bshop.fano AS bshop_fano, ";
            sql += " bshop.faname2 AS bshop_faname2, bshop.faname1 AS bshop_faname1, bshop.fatel1 AS bshop_fatel1, ";
            sql += " bshop.faper1 AS bshop_faper1, bshop.emno AS bshop_emno, bshop.emname AS bshop_emname, ";
            sql += " bshop.spno AS bshop_spno, bshop.spname AS bshop_spname, bshop.xa1no AS bshop_xa1no, ";
            sql += " bshop.xa1name AS bshop_xa1name, bshop.xa1par AS bshop_xa1par, bshop.taxmnyf AS bshop_taxmnyf, ";
            sql += " bshop.taxmnyb AS bshop_taxmnyb, bshop.taxmny AS bshop_taxmny, bshop.x3no AS bshop_x3no, ";
            sql += " bshop.rate AS bshop_rate, bshop.x5no AS bshop_x5no, bshop.seno AS bshop_seno, ";
            sql += " bshop.sename AS bshop_sename, bshop.x4no AS bshop_x4no, bshop.x4name AS bshop_x4name, ";
            sql += " bshop.tax AS bshop_tax, bshop.totmny AS bshop_totmny, bshop.taxb AS bshop_taxb, ";
            sql += " bshop.totmnyb AS bshop_totmnyb, bshop.discount AS bshop_discount, bshop.cashmny AS bshop_cashmny, ";
            sql += " bshop.cardmny AS bshop_cardmny, bshop.cardno AS bshop_cardno, bshop.ticket AS bshop_ticket, ";
            sql += " bshop.collectmny AS bshop_collectmny, bshop.getprvacc AS bshop_getprvacc, bshop.acctmny AS bshop_acctmny, ";
            sql += " bshop.bsmemo AS bshop_bsmemo, bshop.bsmemo1 AS bshop_bsmemo1, bshop.bracket AS bshop_bracket, ";
            sql += " bshop.recordno AS bshop_recordno, bshop.invno AS bshop_invno, bshop.invdate AS bshop_invdate, ";
            sql += " bshop.invdate1 AS bshop_invdate1, bshop.invname AS bshop_invname, bshop.invtaxno AS bshop_invtaxno, ";
            sql += " bshop.invaddr1 AS bshop_invaddr1, bshop.transport AS bshop_transport, bshop.tranpar AS bshop_tranpar, ";
            sql += " bshop.transportb AS bshop_transportb, bshop.premium AS bshop_premium, bshop.prempar AS bshop_prempar, ";
            sql += " bshop.premiumb AS bshop_premiumb, bshop.tariff AS bshop_tariff, bshop.taripar AS bshop_taripar, ";
            sql += " bshop.tariffb AS bshop_tariffb, bshop.postal AS bshop_postal, bshop.procfee AS bshop_procfee, ";
            sql += " bshop.certif AS bshop_certif, bshop.clearance AS bshop_clearance, bshop.otherfee AS bshop_otherfee, ";
            sql += " bshop.totfee AS bshop_totfee, bshop.allot AS bshop_allot, bshop.appdate AS bshop_appdate, ";
            sql += " bshop.edtdate AS bshop_edtdate, bshop.appscno AS bshop_appscno, bshop.edtscno AS bshop_edtscno, ";
            sql += " bshop.acno AS bshop_acno, bshop.cloflag AS bshop_cloflag, bshop.stno AS bshop_stno, ";
            sql += " bshop.stname AS bshop_stname, bshop.AvgKind AS bshop_AvgKind, bshop.AllMny AS bshop_AllMny, ";
            sql += " bshop.UsrNo AS bshop_UsrNo, bshop.BsShare AS bshop_BsShare, bshop.BsShsel AS bshop_BsShsel, ";
            sql += " bshopd.bsid AS bshopd_bsid, bshopd.bsno AS bshopd_bsno, bshopd.bsdate AS bshopd_bsdate, ";
            sql += " bshopd.bsdate1 AS bshopd_bsdate1, bshopd.bsdate2 AS bshopd_bsdate2, bshopd.bsdateac AS bshopd_bsdateac, ";
            sql += " bshopd.bsdateac1 AS bshopd_bsdateac1, bshopd.bsdateac2 AS bshopd_bsdateac2, bshopd.fqno AS bshopd_fqno, ";
            sql += " bshopd.cono AS bshopd_cono, bshopd.fano AS bshopd_fano, bshopd.emno AS bshopd_emno, ";
            sql += " bshopd.spno AS bshopd_spno, bshopd.stno AS bshopd_stno, bshopd.xa1no AS bshopd_xa1no, ";
            sql += " bshopd.xa1par AS bshopd_xa1par, bshopd.seno AS bshopd_seno, bshopd.sename AS bshopd_sename, ";
            sql += " bshopd.x4no AS bshopd_x4no, bshopd.x4name AS bshopd_x4name, bshopd.fono AS bshopd_fono, ";
            sql += " bshopd.itno AS bshopd_itno, bshopd.itname AS bshopd_itname, bshopd.ittrait AS bshopd_ittrait, ";
            sql += " bshopd.itunit AS bshopd_itunit, bshopd.itpkgqty AS bshopd_itpkgqty, bshopd.qty AS bshopd_qty, ";
            sql += " bshopd.price AS bshopd_price, bshopd.prs AS bshopd_prs, bshopd.rate AS bshopd_rate, ";
            sql += " bshopd.taxprice AS bshopd_taxprice, bshopd.mny AS bshopd_mny, bshopd.priceb AS bshopd_priceb, ";
            sql += " bshopd.taxpriceb AS bshopd_taxpriceb, bshopd.mnyb AS bshopd_mnyb, bshopd.memo AS bshopd_memo, ";
            sql += " bshopd.lowzero AS bshopd_lowzero, bshopd.bomid AS bshopd_bomid, bshopd.bomrec AS bshopd_bomrec, ";
            sql += " bshopd.recordno AS bshopd_recordno, bshopd.sltflag AS bshopd_sltflag, bshopd.extflag AS bshopd_extflag, ";
            sql += " bshopd.bracket AS bshopd_bracket, bshopd.itdesp1 AS bshopd_itdesp1, bshopd.itdesp2 AS bshopd_itdesp2, ";
            sql += " bshopd.itdesp3 AS bshopd_itdesp3, bshopd.itdesp4 AS bshopd_itdesp4, bshopd.itdesp5 AS bshopd_itdesp5, ";
            sql += " bshopd.itdesp6 AS bshopd_itdesp6, bshopd.itdesp7 AS bshopd_itdesp7, bshopd.itdesp8 AS bshopd_itdesp8, ";
            sql += " bshopd.itdesp9 AS bshopd_itdesp9, bshopd.itdesp10 AS bshopd_itdesp10, bshopd.serno AS bshopd_serno, ";
            sql += " bshopd.StName AS bshopd_StName, bshopd.PriceF AS bshopd_PriceF, ";
            sql += " bshopd.RecordNo_D AS bshopd_RecordNo_D, bshopd.RealCost AS bshopd_RealCost, ";
            sql += " bshopd.Pqty AS bshopd_Pqty, ";
            sql += " bshopd.Punit AS bshopd_Punit, bshopd.mformula AS bshopd_mformula, bshopd.mwidth4 AS bshopd_mwidth4, ";
            sql += " bshopd.mwidth3 AS bshopd_mwidth3, bshopd.mwidth2 AS bshopd_mwidth2, bshopd.mwidth1 AS bshopd_mwidth1, ";
            sql += " bshopd.mlong AS bshopd_mlong, bshopd.munit AS bshopd_munit, bshopd.mqty AS bshopd_mqty, ";
            sql += " bshopd.foid AS bshopd_foid,";

            sql += " fact.faname1 AS fact_faname1, fact.faxa1no AS fact_faxa1no, fact.facono AS fact_facono, fact.faime AS fact_faime, ";
            sql += " fact.fax12no AS fact_fax12no, fact.faemno1 AS fact_faemno1, fact.faper1 AS fact_faper1, fact.faper2 AS fact_faper2, ";
            sql += " fact.faper AS fact_faper, fact.fatel1 AS fact_fatel1, fact.fatel2 AS fact_fatel2, fact.fatel3 AS fact_fatel3, ";
            sql += " fact.fafax1 AS fact_fafax1, fact.faatel1 AS fact_faatel1, fact.faatel2 AS fact_faatel2, fact.fabbc AS fact_fabbc, ";
            sql += " fact.faaddr1 AS fact_faaddr1, fact.far1 AS fact_far1, fact.faaddr2 AS fact_faaddr2, fact.far2 AS fact_far2, ";
            sql += " fact.faaddr3 AS fact_faaddr3, fact.far3 AS fact_far3, fact.fawork AS fact_fawork, fact.faemail AS fact_faemail, ";
            sql += " fact.fawww AS fact_fawww, fact.facredit AS fact_facredit, fact.fauno AS fact_fauno, fact.fax3no AS fact_fax3no, ";
            sql += " fact.fax4no AS fact_fax4no, fact.fax5no AS fact_fax5no, fact.faengname AS fact_faengname, ";
            sql += " fact.faengaddr AS fact_faengaddr, fact.faengr1 AS fact_faengr1, fact.famemo1 AS fact_famemo1, ";
            sql += " fact.famemo2 AS fact_famemo2, fact.faarea AS fact_faarea, fact.fax2no AS fact_fax2no, fact.faudf1 AS fact_faudf1, ";
            sql += " fact.faudf2 AS fact_faudf2, fact.faudf3 AS fact_faudf3, fact.faudf4 AS fact_faudf4, fact.faudf5 AS fact_faudf5, ";
            sql += " fact.faudf6 AS fact_faudf6, fact.falastday AS fact_falastday, fact.falastday1 AS fact_falastday1, ";
            sql += " fact.falastday2 AS fact_falastday2, fact.fadate AS fact_fadate, fact.fadate1 AS fact_fadate1, ";
            sql += " fact.fadate2 AS fact_fadate2, fact.fafirpayabl AS fact_fafirpayabl, fact.fasparepay AS fact_fasparepay, ";
            sql += " fact.fapayable AS fact_fapayable, fact.fafirpaypar AS fact_fafirpaypar, fact.fafirpayamt AS fact_fafirpayamt, ";
            sql += " fact.fapayamt AS fact_fapayamt, fact.faChkName AS fact_faChkName,scrit.scname,scrit.scname1 ";
            sql += " FROM  bshop";
            sql += " LEFT  JOIN  bshopd ON bshop.bsno = bshopd.bsno ";
            sql += " LEFT  JOIN  fact ON bshop.fano = fact.fano ";
            sql += "  Left join scrit on bshop.appscno = scrit.scname ";

        }






















    }
}
