using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmRShop_Print : Formbase
    {
        RPT rp;
        DataSet ds = new DataSet();
        string path = "";

        public string PK;
        public string FaNo;
        string ReportFileName = "";
        string ReportPath = "";
        string sql;

        public FrmRShop_Print()
        {
            InitializeComponent();
        }

        private void FrmRShop_Print_Load(object sender, EventArgs e)
        {
            BsNo.Text = PK;
            BsNo1.Text = PK;
            ReportFileName = "退貨作業系統";
            string kind = Common.Sys_DBqty == 1 ? "" : "P";
            ToolTip _Tip = new ToolTip();
            _Tip.SetToolTip(radio1, "RShopRpt1" + kind);

            radio5.判斷有無CF或RF("退貨作業系統_簡要自定一" + kind);
            radio7.判斷有無CF或RF("退貨作業系統_簡要自定二" + kind);

            SetRdUdf();
        }


        void dataintodocument(RptMode mode)
        {

            if (string.CompareOrdinal(BsNo1.Text, BsNo.Text) < 0)
            {
                MessageBox.Show("終止退貨單號不可小於起始退貨單號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                otherRpt = Common.reportaddress + "Report\\RShopRpt1" + kind;

            var hasreport = File.Exists(otherRpt + ".rpt");
            if (hasreport)
                return otherRpt + ".rpt";

            hasreport = File.Exists(otherRpt.Replace("Report", "ReportNew") + ".frx");
            if (hasreport)
                return otherRpt + ".frx";

            if (radio1.Checked)
                ReportPath += "_簡要表" + kind;
            else if (radio5.Checked)
                ReportPath += "_簡要自定一" + kind;
            else if (radio7.Checked)
                ReportPath += "_簡要自定二" + kind;

            return Common.判斷開啟報表類型(ReportPath);
        }
        void 水晶列印(string path, RptMode mode)
        {
            ds.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    setSql();
                    sql += " where '0'='0'";
                    if (BsNo.Text != "")
                        sql += " and RShop.BsNo >=@bsno";

                    if (BsNo1.Text.Trim() != "")
                        sql += " and RShop.BsNo <=@bsno1";

                    sql += " order by RShopd.bsid";

                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsno", BsNo.Text.Trim());
                        cmd.Parameters.AddWithValue("bsno1", BsNo1.Text.Trim());
                        cmd.CommandText = sql;
                        da.Fill(ds, "Data");


                        cmd.CommandText = @"
                    Select 
                    *,DATALENGTH(pic) as pic長度
                    from (
	                    Select distinct itno from RShopd where BsNo >= @bsno and BsNo <= @bsno1
                    )A
                    left join item on A.itno = item.itno ";
                        da.Fill(ds, "item");
                    }
                }

                string txtstart = "";
                string txtend = "";
                string txtadress = "";
                string txttel = "";
                string 列印地址 = "";

                if (rdHeader1.Checked) txtstart = Common.dtstart.Rows[0]["pnname"].ToString();
                else if (rdHeader2.Checked) txtstart = Common.dtstart.Rows[1]["pnname"].ToString();
                else if (rdHeader3.Checked) txtstart = Common.dtstart.Rows[2]["pnname"].ToString();
                else if (rdHeader4.Checked) txtstart = Common.dtstart.Rows[3]["pnname"].ToString();
                else if (rdHeader5.Checked) txtstart = Common.dtstart.Rows[4]["pnname"].ToString();
                else txtstart = "";

                if (rdFooter1.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                else if (rdFooter2.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                else if (rdFooter3.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                else if (rdFooter4.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                else if (rdFooter5.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                else txtend = "";

                if (rdHeader1.Checked) txtadress = "    " + Common.dtstart.Rows[0]["pnaddr"].ToString();
                else if (rdHeader2.Checked) txtadress = "    " + Common.dtstart.Rows[1]["pnaddr"].ToString();
                else if (rdHeader3.Checked) txtadress = "    " + Common.dtstart.Rows[2]["pnaddr"].ToString();
                else if (rdHeader4.Checked) txtadress = "    " + Common.dtstart.Rows[3]["pnaddr"].ToString();
                else if (rdHeader5.Checked) txtadress = "    " + Common.dtstart.Rows[4]["pnaddr"].ToString();
                else txtadress = "";

                if (rdHeader1.Checked) txttel = "    TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
                else if (rdHeader2.Checked) txttel = "    TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
                else if (rdHeader3.Checked) txttel = "    TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
                else if (rdHeader4.Checked) txttel = "    TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
                else if (rdHeader5.Checked) txttel = "    TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
                else txttel = "";

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
                                列印地址 = reader["faAddr1"].ToString();
                            else
                                列印地址 = "";
                        }
                    }
                }

                rp = new RPT(ds, path);
                rp.lobj.Add(new string[] { "txtend", txtend });
                rp.lobj.Add(new string[] { "txtstart", txtstart });
                rp.lobj.Add(new string[] { "txtadress", txtadress });
                rp.lobj.Add(new string[] { "txttel", txttel });
                rp.lobj.Add(new string[] { "列印地址", 列印地址 });

                if (mode == RptMode.Print)
                { rp.Print(1); }
                else if (mode == RptMode.PreView)
                { rp.PreView(1); }
                else if (mode == RptMode.Excel)
                { rp.Excel(1); }
                else if (mode == RptMode.Word)
                { rp.Word(1); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void FastReport列印(string path, RptMode mode)
        {
            DataTable printTB = new DataTable();
            printTB.TableName = "RSHOP_";
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                sql = @"SELECT 
                    rshop.bsno AS rshop_退貨單號, rshop.bsdate AS rshop_退貨日期_民國, rshop.bsdate1 AS rshop_退貨日期_西元,  rshop.bsdate2 AS rshop_退貨日期_保留, rshop.bsdateac AS rshop_帳款日期, rshop.bsdateac1 AS rshop_帳款日期_西元,  rshop.bsdateac2 AS rshop_帳款日期_保留, rshop.fqno AS rshop_詢價憑證, rshop.cono AS rshop_公司編號, rshop.coname1 AS rshop_公司簡稱, rshop.coname2 AS rshop_公司名稱, rshop.fano AS rshop_廠商編號,  rshop.faname2 AS rshop_廠商名稱, rshop.faname1 AS rshop_廠商簡稱, 
                    rshop.fatel1 AS rshop_廠商電話,  rshop.faper1 AS rshop_廠商聯絡人, rshop.emno AS rshop_業務編號, rshop.emname AS rshop_業務姓名,  rshop.spno AS rshop_專案編號, rshop.spname AS rshop_專案名稱, rshop.stno AS rshop_退貨倉庫編號, rshop.stname AS rshop_退貨倉庫名稱, rshop.xa1no AS rshop_幣別編號, rshop.xa1name AS rshop_幣別名稱, rshop.xa1par AS rshop_匯率, rshop.taxmnyf AS rshop_taxmnyf, rshop.taxmnyb AS rshop_本幣合計,  rshop.taxmny AS rshop_外幣合計, rshop.x3no AS rshop_稅別編號, 
                    rshop.rate AS rshop_稅率, rshop.x5no AS rshop_發票種類,  rshop.seno AS rshop_送貨編號, rshop.sename AS rshop_送貨名稱, rshop.x4no AS rshop_結帳類別編號,  rshop.x4name AS rshop_結帳類別名稱, rshop.tax AS rshop_外幣營業稅額, rshop.totmny AS rshop_外幣總額, rshop.taxb AS rshop_本幣營業稅額,  rshop.totmnyb AS rshop_本幣總額, rshop.discount AS rshop_折扣金額, rshop.cashmny AS rshop_現金金額,  rshop.cardmny AS rshop_刷卡金額, rshop.cardno AS rshop_刷卡卡號, rshop.ticket AS rshop_禮卷金額,  
                    rshop.collectmny AS rshop_已付金額, rshop.getprvacc AS rshop_取用預付, rshop.acctmny AS rshop_未付金額,  rshop.bsmemo AS rshop_備註, rshop.bsmemo1 AS rshop_詳細備註, rshop.bracket AS rshop_表單類型,  rshop.recordno AS rshop_recordno, rshop.invno AS rshop_發票編號, rshop.invdate AS rshop_發票日期_民國,  rshop.invdate1 AS rshop_發票日期_西元, rshop.invname AS rshop_發票抬頭, rshop.invtaxno AS rshop_統一編號, rshop.invaddr1 AS rshop_發票地址, 
                    rshop.transport AS rshop_運費, rshop.tranpar AS rshop_運費匯率,  rshop.transportb AS rshop_運費本幣, rshop.premium AS rshop_保險費外幣, rshop.prempar AS rshop_保險費匯率,  rshop.premiumb AS rshop_保險費本幣, rshop.tariff AS rshop_關稅, rshop.taripar AS rshop_關稅匯率,  rshop.tariffb AS rshop_關稅本幣, rshop.postal AS rshop_郵電費, rshop.procfee AS rshop_手續費,  rshop.certif AS rshop_簽證費, rshop.clearance AS rshop_報關費, rshop.otherfee AS rshop_其他費用,  rshop.totfee AS rshop_費用合計, rshop.allot AS rshop_費用分配, 
                    rshop.appdate AS rshop_新增日期時間,  rshop.edtdate AS rshop_修改日期時間, rshop.appscno AS rshop_登錄人員, rshop.edtscno AS rshop_最後修改人員,  rshop.acno AS rshop_傳票編號, rshop.cloflag AS rshop_年結標示, rshop.UsrNo AS rshop_UsrNo,  rshop.BsShare AS rshop_費用分攤, rshop.BsShsel AS rshop_分攤方式, rshopd.bsid AS rshopd_bsid,  

                    rshopd.bsno AS rshopd_退貨單號, rshopd.bsdate AS rshopd_退貨日期_民國, rshopd.bsdate1 AS rshopd_退貨日期_西元,  rshopd.bsdate2 AS rshopd_退貨日期_保留, rshopd.bsdateac AS rshopd_帳款日期_民國, rshopd.bsdateac1 AS rshopd_帳款日期_西元,  rshopd.bsdateac2 AS rshopd_帳款日期_保留, rshopd.fqno AS rshopd_詢價憑證, rshopd.cono AS rshopd_公司編號,  rshopd.fano AS rshopd_廠商編號, rshopd.emno AS rshopd_業務編號, rshopd.spno AS rshopd_專案編號,  rshopd.stno AS rshopd_進貨倉庫編號, rshopd.xa1no AS rshopd_幣別編號, 
                    rshopd.xa1par AS rshopd_匯率,  rshopd.seno AS rshopd_送貨編號, rshopd.sename AS rshopd_送貨名稱, rshopd.x4no AS rshopd_結帳類別編號,  rshopd.x4name AS rshopd_結帳類別名稱, rshopd.fono AS rshopd_採購單號, rshopd.itno AS rshopd_產品編號,  rshopd.itname AS rshopd_品名規格, rshopd.ittrait AS rshopd_產品組成, rshopd.itunit AS rshopd_單位,  rshopd.itpkgqty AS rshopd_包裝數量, rshopd.qty AS rshopd_數量, rshopd.price AS rshopd_外幣單價,  rshopd.prs AS rshopd_折數, rshopd.rate AS rshopd_稅率, rshopd.taxprice AS rshopd_外幣稅前單價,  
                    rshopd.mny AS rshopd_外幣稅前金額, rshopd.priceb AS rshopd_本幣單價, rshopd.taxpriceb AS rshopd_本幣稅前單價,  rshopd.mnyb AS rshopd_本幣稅前金額, rshopd.memo AS rshopd_說明, rshopd.lowzero AS rshopd_低於庫存量,  rshopd.bomid AS rshopd_bomid, rshopd.bomrec AS rshopd_bomrec, rshopd.recordno AS rshopd_recordno,  rshopd.sltflag AS rshopd_sltflag, rshopd.extflag AS rshopd_extflag, rshopd.bracket AS rshopd_表單類型,  
                    rshopd.itdesp1 AS rshopd_規格說明1, rshopd.itdesp2 AS rshopd_規格說明2, rshopd.itdesp3 AS rshopd_規格說明3,  rshopd.itdesp4 AS rshopd_規格說明4, rshopd.itdesp5 AS rshopd_規格說明5, 
                    rshopd.itdesp6 AS rshopd_規格說明6,  rshopd.itdesp7 AS rshopd_規格說明7, rshopd.itdesp8 AS rshopd_規格說明8, rshopd.itdesp9 AS rshopd_規格說明9,  rshopd.itdesp10 AS rshopd_規格說明10, 
                    rshopd.stName AS rshopd_stName,  rshopd.RecordNo_D AS rshopd_RecordNo_D, rshopd.RealCost AS rshopd_RealCost, rshopd.Pqty AS rshopd_Pqty,  rshopd.Punit AS rshopd_Punit, rshopd.mformula AS rshopd_mformula, 
                    rshopd.mwidth4 AS rshopd_mwidth4,  rshopd.mwidth3 AS rshopd_mwidth3, rshopd.mwidth2 AS rshopd_mwidth2, rshopd.mwidth1 AS rshopd_mwidth1,  rshopd.mlong AS rshopd_mlong, rshopd.munit AS rshopd_munit, rshopd.mqty AS rshopd_mqty,  rshopd.foid AS rshopd_foid, 

                    RShopBom.BsNo AS RShopBom_退貨單號, RShopBom.BomID AS RShopBom_BomID,  RShopBom.BomRec AS RShopBom_BomRec, RShopBom.itno AS RShopBom_產品編號,  RShopBom.itname AS RShopBom_品名規格, RShopBom.itunit AS RShopBom_單位,  RShopBom.itqty AS RShopBom_標準用量, RShopBom.itpareprs AS RShopBom_母件比率,  RShopBom.itpkgqty AS RShopBom_包裝數量, RShopBom.itrec AS RShopBom_itrec,  RShopBom.itprice AS RShopBom_單價, RShopBom.itprs AS RShopBom_折數,  RShopBom.itmny AS RShopBom_金額, RShopBom.itnote AS RShopBom_說明,  RShopBom.ItSource AS RShopBom_ItSource, RShopBom.ItBuyPri AS RShopBom_ItBuyPri,  RShopBom.ItBuyMny AS RShopBom_ItBuyMny, 

                    fact.fano AS fact_廠商編號, fact.faname2 AS fact_廠商名稱,  fact.faname1 AS fact_廠商簡稱, fact.faxa1no AS fact_幣別編號, fact.facono AS fact_隸屬公司, fact.faime AS fact_注音速查,  fact.fax12no AS fact_廠商類別, fact.faemno1 AS fact_業務人員, fact.faper1 AS fact_廠商聯絡人1, fact.faper2 AS fact_廠商聯絡人2,  fact.faper AS fact_廠商負責人, fact.fatel1 AS fact_廠商電話1, fact.fatel2 AS fact_廠商電話2, fact.fatel3 AS fact_廠商電話3,  fact.fafax1 AS fact_廠商傳真, fact.faatel1 AS fact_廠商行動電話1, fact.faatel2 AS fact_廠商行動電話2, fact.fabbc AS fact_呼叫器,  
                    fact.faaddr1 AS fact_公司地址, fact.far1 AS fact_公司郵遞區號, fact.faaddr2 AS fact_發票地址, fact.far2 AS fact_發票郵遞區號,  fact.faaddr3 AS fact_工廠地址, fact.far3 AS fact_工廠郵遞區號, fact.fawork AS fact_營業項目, fact.faemail AS fact_電子信箱,  fact.fawww AS fact_網址, fact.facredit AS fact_信用額度, fact.fauno AS fact_統一編號, fact.fax3no AS fact_營業稅,  fact.fax4no AS fact_結帳類別, fact.fax5no AS fact_發票模式, fact.faengname AS fact_英文抬頭,  fact.faengaddr AS fact_英文住址, fact.faengr1 AS fact_英文住址郵遞區號, fact.famemo1 AS fact_備註1,  fact.famemo2 AS fact_備註2, 
                    fact.faarea AS fact_地區類別, fact.fax2no AS fact_區域名稱, fact.faudf1 AS fact_自訂1,  fact.faudf2 AS fact_自訂2, fact.faudf3 AS fact_自訂3, fact.faudf4 AS fact_自訂4, fact.faudf5 AS fact_自訂5,  fact.faudf6 AS fact_自訂6, fact.falastday AS fact_最近交易_民國, fact.falastday1 AS fact_最近交易_西元,  fact.falastday2 AS fact_最近交易_保留, fact.fadate AS fact_建檔日期_民國, fact.fadate1 AS fact_建檔日期_西元,  fact.fadate2 AS fact_建檔日期_保留, fact.fafirpayabl AS fact_期初應付帳款金額, fact.fasparepay AS fact_期初應付帳款餘額,  fact.fapayable AS fact_現有應付帳款金額, fact.fafirpaypar AS fact_期初應付帳款匯率, fact.fafirpayamt AS fact_期初預付金額,  fact.fapayamt AS fact_預付餘額, fact.faChkName AS fact_faChkName,

                    scrit.scname,scrit.scname1  

                    FROM  rshop  
                    LEFT  JOIN  rshopd ON rshop.bsno = rshopd.bsno  
                    LEFT  JOIN  RShopBom ON rshopd.bomid = RShopBom.BomID  
                    LEFT  JOIN  fact ON rshop.fano = fact.fano  
                    Left join scrit on rshop.appscno = scrit.scname  

                    WHERE rshop.bsno>=@bsno and rshop.bsno<=@bsno1

                    order by rshop.bsno,rshopd.recordno";


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

                FastReport.PreView(path, printTB, "RSHOP_", null, null, mode, ReportFileName);
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
            using (var frm = new FrmRShop_Print_BsNo())
            { 
                frm.TSeekNo = (sender as TextBoxT).Text;

                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        (sender as TextBoxT).Text = frm.TResult;
                        break;
                }
            }
        }

        private void BsNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            bool isHaveRow = true;
            TextBox tb = sender as TextBox;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("BsNo", tb.Text.Trim());
                        cmd.CommandText = "select Count(BsNo) from RShop where BsNo=@BsNo COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                if (reader[0].ToString() == "0")
                                    isHaveRow = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (!isHaveRow)
            {
                e.Cancel = true;
                tb.SelectAll();
                using (var frm = new FrmRShop_Print_BsNo())
                { 
                    frm.TSeekNo = tb.Text;

                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            tb.Text = frm.TResult;
                            break;
                    }
                }
            }
        }

        private void btnUdf_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT6);
            pnlist.Add(groupBoxT8);
            pVar.SaveRadioUdf(pnlist, "RShop");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT6);
            pnlist.Add(groupBoxT8);
            pVar.SetRadioUdf(pnlist, "RShop");
        }

        void setSql()
        {
            sql = "";
            sql += " SELECT rshop.bsno AS rshop_bsno, rshop.bsdate AS rshop_bsdate, rshop.bsdate1 AS rshop_bsdate1, ";
            sql += " rshop.bsdate2 AS rshop_bsdate2, rshop.bsdateac AS rshop_bsdateac, rshop.bsdateac1 AS rshop_bsdateac1, ";
            sql += " rshop.bsdateac2 AS rshop_bsdateac2, rshop.fqno AS rshop_fqno, rshop.cono AS rshop_cono,";
            sql += " rshop.coname1 AS rshop_coname1, rshop.coname2 AS rshop_coname2, rshop.fano AS rshop_fano, ";
            sql += " rshop.faname2 AS rshop_faname2, rshop.faname1 AS rshop_faname1, rshop.fatel1 AS rshop_fatel1, ";
            sql += " rshop.faper1 AS rshop_faper1, rshop.emno AS rshop_emno, rshop.emname AS rshop_emname, ";
            sql += " rshop.spno AS rshop_spno, rshop.spname AS rshop_spname, rshop.stno AS rshop_stno,";
            sql += " rshop.stname AS rshop_stname, rshop.xa1no AS rshop_xa1no, rshop.xa1name AS rshop_xa1name,";
            sql += " rshop.xa1par AS rshop_xa1par, rshop.taxmnyf AS rshop_taxmnyf, rshop.taxmnyb AS rshop_taxmnyb, ";
            sql += " rshop.taxmny AS rshop_taxmny, rshop.x3no AS rshop_x3no, rshop.rate AS rshop_rate, rshop.x5no AS rshop_x5no, ";
            sql += " rshop.seno AS rshop_seno, rshop.sename AS rshop_sename, rshop.x4no AS rshop_x4no, ";
            sql += " rshop.x4name AS rshop_x4name, rshop.tax AS rshop_tax, rshop.totmny AS rshop_totmny, rshop.taxb AS rshop_taxb, ";
            sql += " rshop.totmnyb AS rshop_totmnyb, rshop.discount AS rshop_discount, rshop.cashmny AS rshop_cashmny, ";
            sql += " rshop.cardmny AS rshop_cardmny, rshop.cardno AS rshop_cardno, rshop.ticket AS rshop_ticket, ";
            sql += " rshop.collectmny AS rshop_collectmny, rshop.getprvacc AS rshop_getprvacc, rshop.acctmny AS rshop_acctmny, ";
            sql += " rshop.bsmemo AS rshop_bsmemo, rshop.bsmemo1 AS rshop_bsmemo1, rshop.bracket AS rshop_bracket, ";
            sql += " rshop.recordno AS rshop_recordno, rshop.invno AS rshop_invno, rshop.invdate AS rshop_invdate, ";
            sql += " rshop.invdate1 AS rshop_invdate1, rshop.invname AS rshop_invname, rshop.invtaxno AS rshop_invtaxno,";
            sql += " rshop.invaddr1 AS rshop_invaddr1, rshop.transport AS rshop_transport, rshop.tranpar AS rshop_tranpar, ";
            sql += " rshop.transportb AS rshop_transportb, rshop.premium AS rshop_premium, rshop.prempar AS rshop_prempar, ";
            sql += " rshop.premiumb AS rshop_premiumb, rshop.tariff AS rshop_tariff, rshop.taripar AS rshop_taripar, ";
            sql += " rshop.tariffb AS rshop_tariffb, rshop.postal AS rshop_postal, rshop.procfee AS rshop_procfee, ";
            sql += " rshop.certif AS rshop_certif, rshop.clearance AS rshop_clearance, rshop.otherfee AS rshop_otherfee, ";
            sql += " rshop.totfee AS rshop_totfee, rshop.allot AS rshop_allot, rshop.appdate AS rshop_appdate, ";
            sql += " rshop.edtdate AS rshop_edtdate, rshop.appscno AS rshop_appscno, rshop.edtscno AS rshop_edtscno, ";
            sql += " rshop.acno AS rshop_acno, rshop.cloflag AS rshop_cloflag, rshop.UsrNo AS rshop_UsrNo, ";
            sql += " rshop.BsShare AS rshop_BsShare, rshop.BsShsel AS rshop_BsShsel, rshopd.bsid AS rshopd_bsid, ";
            sql += " rshopd.bsno AS rshopd_bsno, rshopd.bsdate AS rshopd_bsdate, rshopd.bsdate1 AS rshopd_bsdate1, ";
            sql += " rshopd.bsdate2 AS rshopd_bsdate2, rshopd.bsdateac AS rshopd_bsdateac, rshopd.bsdateac1 AS rshopd_bsdateac1, ";
            sql += " rshopd.bsdateac2 AS rshopd_bsdateac2, rshopd.fqno AS rshopd_fqno, rshopd.cono AS rshopd_cono, ";
            sql += " rshopd.fano AS rshopd_fano, rshopd.emno AS rshopd_emno, rshopd.spno AS rshopd_spno, ";
            sql += " rshopd.stno AS rshopd_stno, rshopd.xa1no AS rshopd_xa1no, rshopd.xa1par AS rshopd_xa1par, ";
            sql += " rshopd.seno AS rshopd_seno, rshopd.sename AS rshopd_sename, rshopd.x4no AS rshopd_x4no, ";
            sql += " rshopd.x4name AS rshopd_x4name, rshopd.fono AS rshopd_fono, rshopd.itno AS rshopd_itno, ";
            sql += " rshopd.itname AS rshopd_itname, rshopd.ittrait AS rshopd_ittrait, rshopd.itunit AS rshopd_itunit, ";
            sql += " rshopd.itpkgqty AS rshopd_itpkgqty, rshopd.qty AS rshopd_qty, rshopd.price AS rshopd_price, ";
            sql += " rshopd.prs AS rshopd_prs, rshopd.rate AS rshopd_rate, rshopd.taxprice AS rshopd_taxprice, ";
            sql += " rshopd.mny AS rshopd_mny, rshopd.priceb AS rshopd_priceb, rshopd.taxpriceb AS rshopd_taxpriceb, ";
            sql += " rshopd.mnyb AS rshopd_mnyb, rshopd.memo AS rshopd_memo, rshopd.lowzero AS rshopd_lowzero, ";
            sql += " rshopd.bomid AS rshopd_bomid, rshopd.bomrec AS rshopd_bomrec, rshopd.recordno AS rshopd_recordno, ";
            sql += " rshopd.sltflag AS rshopd_sltflag, rshopd.extflag AS rshopd_extflag, rshopd.bracket AS rshopd_bracket, ";
            sql += " rshopd.itdesp1 AS rshopd_itdesp1, rshopd.itdesp2 AS rshopd_itdesp2, rshopd.itdesp3 AS rshopd_itdesp3, ";
            sql += " rshopd.itdesp4 AS rshopd_itdesp4, rshopd.itdesp5 AS rshopd_itdesp5, rshopd.itdesp6 AS rshopd_itdesp6, ";
            sql += " rshopd.itdesp7 AS rshopd_itdesp7, rshopd.itdesp8 AS rshopd_itdesp8, rshopd.itdesp9 AS rshopd_itdesp9, ";
            sql += " rshopd.itdesp10 AS rshopd_itdesp10, rshopd.stName AS rshopd_stName, ";
            sql += " rshopd.RecordNo_D AS rshopd_RecordNo_D, rshopd.RealCost AS rshopd_RealCost,";
            sql += " rshopd.Pqty AS rshopd_Pqty, ";
            sql += " rshopd.Punit AS rshopd_Punit, rshopd.mformula AS rshopd_mformula, rshopd.mwidth4 AS rshopd_mwidth4, ";
            sql += " rshopd.mwidth3 AS rshopd_mwidth3, rshopd.mwidth2 AS rshopd_mwidth2, rshopd.mwidth1 AS rshopd_mwidth1, ";
            sql += " rshopd.mlong AS rshopd_mlong, rshopd.munit AS rshopd_munit, rshopd.mqty AS rshopd_mqty, ";
            sql += " rshopd.foid AS rshopd_foid,";
            sql += " RShopBom.BsNo AS RShopBom_BsNo, RShopBom.BomID AS RShopBom_BomID, ";
            sql += " RShopBom.BomRec AS RShopBom_BomRec, RShopBom.itno AS RShopBom_itno, ";
            sql += " RShopBom.itname AS RShopBom_itname, RShopBom.itunit AS RShopBom_itunit, ";
            sql += " RShopBom.itqty AS RShopBom_itqty, RShopBom.itpareprs AS RShopBom_itpareprs, ";
            sql += " RShopBom.itpkgqty AS RShopBom_itpkgqty, RShopBom.itrec AS RShopBom_itrec, ";
            sql += " RShopBom.itprice AS RShopBom_itprice, RShopBom.itprs AS RShopBom_itprs, ";
            sql += " RShopBom.itmny AS RShopBom_itmny, RShopBom.itnote AS RShopBom_itnote, ";
            sql += " RShopBom.ItSource AS RShopBom_ItSource, RShopBom.ItBuyPri AS RShopBom_ItBuyPri, ";
            sql += " RShopBom.ItBuyMny AS RShopBom_ItBuyMny, fact.fano AS fact_fano, fact.faname2 AS fact_faname2, ";
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
            sql += " FROM  rshop ";
            sql += " LEFT  JOIN  rshopd ON rshop.bsno = rshopd.bsno ";
            sql += " LEFT  JOIN  RShopBom ON rshopd.bomid = RShopBom.BomID ";
            sql += " LEFT  JOIN  fact ON rshop.fano = fact.fano  Left join scrit on rshop.appscno = scrit.scname";
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