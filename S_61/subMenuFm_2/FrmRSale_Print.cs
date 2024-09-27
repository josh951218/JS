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
    public partial class FrmRSale_Print : Formbase
    {
        RPT rp;
        DataSet ds = new DataSet();
        string path = "";

        string txtstart = "";
        string txtend = "";
        string txtadress = "";
        string txttel = "";
        string 列印地址 = "";

        public string PK;
        public string CuNo;
        string sql;

        string ReportFileName = "";
        string ReportPath = "";

        public FrmRSale_Print()
        {
            InitializeComponent();
        }

        private void FrmRSale_Print_Load(object sender, EventArgs e)
        {
            SaNo.Text = PK;
            SaNo1.Text = PK;

            ReportFileName = "銷貨退回作業系統";

            string kind = Common.Sys_DBqty == 1 ? "" : "P";
            radio5.判斷有無CF或RF("銷貨退回作業系統_簡要自定一" + kind);
            radio7.判斷有無CF或RF("銷貨退回作業系統_組件自定一" + kind);
            ToolTip _Tip = new ToolTip();
            _Tip.SetToolTip(radio1, "RSaleRpt1"+kind);
            _Tip.SetToolTip(radio2, "RSaleRpt2"+kind);

            SetRdUdf();
          
        }

        void loadDB()
        {
            ds.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    setSql();
                    sql += " where '0'='0'";
                    if (SaNo.Text != "")
                        sql += " and rsale.sano >=@sano";

                    if (SaNo1.Text.Trim() != "")
                        sql += " and rsale.sano <=@sano1";

                    sql += " Order by rsaled.said ";

                    cmd.Parameters.Clear();
                    if (SaNo.Text != "") cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                    if (SaNo1.Text != "") cmd.Parameters.AddWithValue("sano1", SaNo1.Text.Trim());


                    cmd.CommandText = sql;
                    da.Fill(ds, "Data");

                    if (ds.Tables["Data"].Rows.Count == 0)
                    {
                        SaNo.Focus();
                        MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cmd.CommandText = @"
                    Select 
                    item.itno, itnoudf, itname, kino, itime, ittrait, itunit, itunitp, itpkgqty, itnw, itnwunit, itdesp1, itdesp2, itdesp3, itdesp4, 
                    itdesp5, itdesp6, itdesp7, itdesp8, itdesp9, itdesp10, itnote, itudf1, itudf2, itudf3, itudf4, itudf5, fano, Punit, ScNo,pic,DATALENGTH(pic) as pic長度
                    from (
	                    Select distinct itno from rsaled where sano >= @sano and sano <= @sano1
                    )A
                    left join item on A.itno = item.itno ";
                    da.Fill(ds, "item");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void paramsInit(RptMode mode)
        {
            if (string.CompareOrdinal(SaNo1.Text, SaNo.Text) < 0)
            {
                MessageBox.Show("終止銷貨編號不可小於起始銷貨編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                otherRpt = Common.reportaddress + "Report\\RSaleRpt1";
            else if (radio2.Checked)
                otherRpt = Common.reportaddress + "Report\\RSaleRpt2";

            var hasreport = File.Exists(otherRpt + ".rpt");
            if (hasreport)
                return otherRpt + ".rpt";

            hasreport = File.Exists(otherRpt.Replace("Report", "ReportNew") + ".frx");
            if (hasreport)
                return otherRpt + ".frx";

            if (radio1.Checked)
                ReportPath += "_簡要表" + kind;
            else if (radio2.Checked)
                ReportPath += "_組件明細" + kind;
            else if (radio5.Checked)
                ReportPath += "_簡要自定一" + kind;
            else if (radio7.Checked)
                ReportPath += "_組件自定一" + kind;


            return Common.判斷開啟報表類型(ReportPath);
        }
        void 水晶列印(string path, RptMode mode)
        {
            loadDB();

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

            using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("cuno", CuNo.Trim());
                    cmd.CommandText = "select cuAddr1 from cust where cuno=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                            列印地址 = reader["cuAddr1"].ToString();
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
            rp.lval.Add(new string[] { "使用者", Common.User_Name1 });

            if (mode == RptMode.Print)
                rp.Print(1);
            else if (mode == RptMode.PreView)
                rp.PreView(1);
            else if (mode == RptMode.Word)
                rp.Word(1);
            else if (mode == RptMode.Excel)
                rp.Excel(1);
        }
        void FastReport列印(string path, RptMode mode)
        {
            DataTable printTB = new DataTable();
            printTB.TableName = "RSALE_";
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                sql = "";
                sql += " SELECT rsale.fromsale AS rsale_銷貨憑證,rsale.sano AS rsale_銷退單號, rsale.sadate AS rsale_銷退日期_民國, rsale.sadate1 AS rsale_銷退日期_西元, ";
                sql += " rsale.sadate2 AS rsale_銷退日期_保留, rsale.sadateac AS rsale_帳款日期_民國, rsale.sadateac1 AS rsale_帳款日期_西元, ";
                sql += " rsale.sadateac2 AS rsale_帳款日期_保留, rsale.quno AS rsale_報價憑證, rsale.cono AS rsale_公司編號, ";
                sql += " rsale.coname1 AS rsale_公司簡稱, rsale.coname2 AS rsale_公司名稱, rsale.cuno AS rsale_客戶編號, ";
                sql += " rsale.cuname2 AS rsale_客戶名稱, rsale.cuname1 AS rsale_客戶簡稱, rsale.cutel1 AS rsale_客戶電話, ";
                sql += " rsale.cuper1 AS rsale_客戶聯絡人, rsale.emno AS rsale_業務編號, rsale.emname AS rsale_業務姓名, ";
                sql += " rsale.spno AS rsale_專案編號, rsale.spname AS rsale_專案名稱, rsale.stno AS rsale_退貨倉庫編號, rsale.stname AS rsale_退貨倉庫名稱, ";
                sql += " rsale.xa1no AS rsale_幣別編號, rsale.xa1name AS rsale_幣別名稱, rsale.xa1par AS rsale_匯率, ";
                sql += " rsale.taxmnyf AS rsale_taxmnyf, rsale.taxmnyb AS rsale_本幣合計, rsale.taxmny AS rsale_外幣合計, ";
                sql += " rsale.x3no AS rsale_稅別編號, rsale.rate AS rsale_稅率, rsale.x5no AS rsale_發票種類, rsale.seno AS rsale_送貨編號, ";
                sql += " rsale.sename AS rsale_送貨名稱, rsale.x4no AS rsale_結帳類別編號, rsale.x4name AS rsale_結帳類別名稱, rsale.tax AS rsale_外幣營業稅額, ";
                sql += " rsale.totmny AS rsale_外幣總額, rsale.taxb AS rsale_本幣營業稅額, rsale.totmnyb AS rsale_本幣總額, ";
                sql += " rsale.discount AS rsale_折扣金額, rsale.cashmny AS rsale_收現金額, rsale.cardmny AS rsale_刷卡金額, ";
                sql += " rsale.cardno AS rsale_刷卡卡號, rsale.ticket AS rsale_禮卷金額, rsale.collectmny AS rsale_已退金額, ";
                sql += " rsale.getprvacc AS rsale_取用預收, rsale.acctmny AS rsale_未退金額, rsale.samemo AS rsale_備註, ";
                sql += " rsale.samemo1 AS rsale_詳細備註, rsale.bracket AS rsale_表單類型, rsale.recordno AS rsale_recordno, ";
                sql += " rsale.invno AS rsale_發票編號, rsale.invdate AS rsale_發票日期_民國, rsale.invdate1 AS rsale_發票日期_西元, ";
                sql += " rsale.invname AS rsale_發票抬頭, rsale.invtaxno AS rsale_統一編號, rsale.invaddr1 AS rsale_發票地址, ";
                sql += " rsale.invbatch AS rsale_發票批開選定, rsale.invbatflg AS rsale_批開發票標記, rsale.appdate AS rsale_新增日期時間, ";
                sql += " rsale.edtdate AS rsale_修改日期時間, rsale.appscno AS rsale_登錄人員, rsale.edtscno AS rsale_最後修改人員,rsale.SaPayment as rsale_傳票編號, ";
                sql += " rsale.acno AS rsale_傳票編號, rsale.cloflag AS rsale_年結標示, rsale.UsrNo AS rsale_usrno, rsaled.saID AS rsaled_said, ";
                sql += " rsaled.sano AS rsaled_銷退單號, rsaled.sadate AS rsaled_銷退日期_民國, rsaled.sadate1 AS rsaled_銷退日期_西元, ";
                sql += " rsaled.sadate2 AS rsaled_銷退日期_保留, rsaled.sadateac AS rsaled_帳款日期, rsaled.sadateac1 AS rsaled_帳款日期_西元, ";
                sql += " rsaled.sadateac2 AS rsaled_帳款日期_保留, rsaled.quno AS rsaled_報價憑證, rsaled.cono AS rsaled_公司編號, ";
                sql += " rsaled.cuno AS rsaled_客戶編號, rsaled.emno AS rsaled_業務編號, rsaled.spno AS rsaled_專案編號, rsaled.stno AS rsaled_出貨倉庫編號, ";
                sql += " rsaled.xa1no AS rsaled_幣別編號, rsaled.xa1par AS rsaled_匯率, rsaled.seno AS rsaled_送貨編號, ";
                sql += " rsaled.sename AS rsaled_送貨名稱, rsaled.x4no AS rsaled_結帳類別編號, rsaled.x4name AS rsaled_結帳類別名稱, ";
                sql += " rsaled.orno AS rsaled_訂單單號, rsaled.itno AS rsaled_產品編號, rsaled.itname AS rsaled_品名規格, rsaled.ittrait AS rsaled_產品組成,";
                sql += " rsaled.itunit AS rsaled_單位, rsaled.itpkgqty AS rsaled_包裝數量, rsaled.qty AS rsaled_數量, ";
                sql += " rsaled.price AS rsaled_外幣單價, rsaled.prs AS rsaled_折數, rsaled.rate AS rsaled_稅率, rsaled.taxprice AS rsaled_外幣稅前單價,";
                sql += " rsaled.mny AS rsaled_外幣稅前金額, rsaled.priceb AS rsaled_本幣單價, rsaled.taxpriceb AS rsaled_本幣稅前單價, ";
                sql += " rsaled.mnyb AS rsaled_本幣稅前金額, rsaled.memo AS rsaled_說明, rsaled.lowzero AS rsaled_庫存量不足, ";
                sql += " rsaled.bomid AS rsaled_bomid, rsaled.bomrec AS rsaled_bomrec, rsaled.recordno AS rsaled_recordno, ";
                sql += " rsaled.sltflag AS rsaled_sltflag, rsaled.extflag AS rsaled_extflag, rsaled.bracket AS rsaled_表單類型, ";
                sql += " rsaled.itdesp1 AS rsaled_規格說明1, rsaled.itdesp2 AS rsaled_規格說明2, rsaled.itdesp3 AS rsaled_規格說明3, ";
                sql += " rsaled.itdesp4 AS rsaled_規格說明4, rsaled.itdesp5 AS rsaled_規格說明5, rsaled.itdesp6 AS rsaled_規格說明6, ";
                sql += " rsaled.itdesp7 AS rsaled_規格說明7, rsaled.itdesp8 AS rsaled_規格說明8, rsaled.itdesp9 AS rsaled_規格說明9, ";
                sql += " rsaled.itdesp10 AS rsaled_規格說明10, rsaled.stName AS rsaled_stname, rsaled.RecordNo_D AS rsaled_recordno_d, ";
                sql += " rsaled.Punit AS rsaled_Punit, rsaled.Pqty AS rsaled_Pqty, rsaled.Point AS rsaled_Point, ";
                sql += " rsaled.mformula AS rsaled_mformula, rsaled.mwidth4 AS rsaled_mwidth4, rsaled.mwidth3 AS rsaled_mwidth3, ";
                sql += " rsaled.mwidth2 AS rsaled_mwidth2, rsaled.mwidth1 AS rsaled_mwidth1, rsaled.mlong AS rsaled_mlong, ";
                sql += " rsaled.munit AS rsaled_munit, rsaled.mqty AS rsaled_mqty, rsaled.IsTrans AS rsaled_IsTrans, ";
                sql += " rsaled.KiTax AS rsaled_KiTax, rsaled.orid AS rsaled_orid,";
                if (radio2.Checked || radio7.Checked)
                {
                    sql += " RSaleBom.SaNo AS rsalebom_銷退單號, RSaleBom.BomID AS rsalebom_bomid, ";
                    sql += " RSaleBom.BomRec AS rsalebom_bomrec, RSaleBom.itno AS rsalebom_產品編號, RSaleBom.itname AS rsalebom_品名規格, ";
                    sql += " RSaleBom.itunit AS rsalebom_單位, RSaleBom.itqty AS rsalebom_標準用量, RSaleBom.itpareprs AS rsalebom_母件比率, ";
                    sql += " RSaleBom.itpkgqty AS rsalebom_包裝數量, RSaleBom.itrec AS rsalebom_itrec, RSaleBom.itprice AS rsalebom_單價, ";
                    sql += " RSaleBom.itprs AS rsalebom_折數, RSaleBom.itmny AS rsalebom_金額, RSaleBom.itnote AS rsalebom_說明, ";
                    sql += " RSaleBom.ItSource AS rsalebom_itsource, RSaleBom.ItBuyPri AS rsalebom_itbuypri, ";
                    sql += " RSaleBom.ItBuyMny AS rsalebom_itbuymny, ";
                }
                sql += " cust.cuno AS cust_客戶編號, cust.cuname2 AS cust_客戶名稱, ";
                sql += " cust.cuname1 AS cust_客戶簡稱, cust.cuinvoname AS cust_發票抬頭, cust.cuchkname AS cust_支票抬頭, ";
                sql += " cust.cuxa1no AS cust_交易幣別, cust.cupareno AS cust_隸屬經銷, cust.cucono AS cust_隸屬公司, ";
                sql += " cust.cuime AS cust_注音速查, cust.cux1no AS cust_客戶類別, cust.cuemno1 AS cust_業務人員, ";
                sql += " cust.cuper1 AS cust_客戶聯絡人1, cust.cuper2 AS cust_客戶聯絡人2, cust.cuper AS cust_客戶負責人, cust.cutel1 AS cust_客戶電話1, ";
                sql += " cust.cutel2 AS cust_客戶電話2, cust.cufax1 AS cust_客戶傳真機, cust.cuatel1 AS cust_客戶行動電話1, cust.cuatel2 AS cust_客戶行動電話2, ";
                sql += " cust.cuatel3 AS cust_客戶行動電話3, cust.cubbc AS cust_呼叫器, cust.cuaddr1 AS cust_公司地址, cust.cur1 AS cust_公司地址郵遞區號, ";
                sql += " cust.cuaddr2 AS cust_發票地址2, cust.cur2 AS cust_發票地址郵遞區號, cust.cuaddr3 AS cust_送貨地址, cust.cur3 AS cust_送貨地址郵遞區號, ";
                sql += " cust.cuslevel AS cust_售價等級, cust.cudisc AS cust_折數, cust.cuemail AS cust_電子信箱, ";
                sql += " cust.cuwww AS cust_網址, cust.cux2no AS cust_區域名稱, cust.cuuno AS cust_統一編號, cust.cux3no AS cust_營業稅, ";
                sql += " cust.cux4no AS cust_結帳類別, cust.cucredit AS cust_信用額度, cust.cuengname AS cust_英文抬頭, ";
                sql += " cust.cuengaddr AS cust_英文住址, cust.cuengr1 AS cust_英文住址郵遞區號, cust.cumemo1 AS cust_備註1, ";
                sql += " cust.cumemo2 AS cust_備註2, cust.cux5no AS cust_發票模式, cust.cuarea AS cust_地區類別, ";
                sql += " cust.cuudf1 AS cust_自定1, cust.cuudf2 AS cust_自定2, cust.cuudf3 AS cust_自定3, cust.cuudf4 AS cust_自定4, ";
                sql += " cust.cuudf5 AS cust_自定5_網頁使用者密碼, cust.cuudf6 AS cust_自定6, cust.cudate AS cust_建檔日期_民國, cust.cudate1 AS cust_建檔日期_西元, ";
                sql += " cust.cudate2 AS cust_建檔日期_保留, cust.culastday AS cust_最近交易_民國, cust.culastday1 AS cust_最近交易_西元, ";
                sql += " cust.culastday2 AS cust_最近交易_保留, cust.cufirreceiv AS cust_期初應收帳款金額, cust.cusparercv AS cust_期初應收帳款餘額, ";
                sql += " cust.cureceiv AS cust_現有應收帳款金額, cust.cufirrcvpar AS cust_期初應收帳款匯率, cust.cufiradvamt AS cust_期初預收餘額, ";
                sql += " cust.cuadvamt AS cust_預收餘額, cust.cunote AS cust_備忘錄, cust.cubirth AS cust_出生日期_民國, ";
                sql += " cust.cubirth1 AS cust_出生日期_西元, cust.cubirth2 AS cust_出生日期_保留, cust.cusex AS cust_性別, ";
                sql += " cust.cublood AS cust_血型, cust.cuidno AS cust_身份證字號, cust.ServDate AS cust_servdate, ";
                sql += " cust.BetwDates AS cust_betwdates, cust.MsgFlag AS cust_msgflag, cust.CuPause AS cust_cupause,scrit.scname,scrit.scname1, ";
                sql += " [order].Adaddr as order_Adaddr,[order].orno as order_訂單憑證,[order].cuper1 as order_聯絡人,[order].AdAddr as order_AdAddr,[order].orpayment as order_付款條件,[order].ormemo as order_備註,[order].CardNo as order_CardNo  "; //寶元
                sql += " FROM rsale ";
                sql += " LEFT OUTER JOIN scrit  ON rsale.appscno = scrit.scname ";
                sql += " LEFT OUTER JOIN rsaled ON rsale.sano = rsaled.sano ";
                sql += " LEFT JOIN [order] ON [order].orno = rsaled.orno "; //寶元
                if (radio2.Checked || radio7.Checked)
                {
                    sql += " LEFT OUTER JOIN RSaleBom ON rsaled.bomid = RSaleBom.BomID ";
                }
                sql += " LEFT OUTER JOIN cust ON rsale.cuno = cust.cuno";
                sql += " where '0'='0'";
                if (SaNo.Text != "")
                    sql += " and rsale.sano >=@sano";

                if (SaNo1.Text.Trim() != "")
                    sql += " and rsale.sano <=@sano1";

                sql += " Order by rsaled.said ";


                cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                cmd.Parameters.AddWithValue("sano1", SaNo1.Text.Trim());
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

                FastReport.PreView(path, printTB, "RSALE_", null, null, mode, ReportFileName);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            paramsInit(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit(RptMode.Excel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SaNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmRSale_Print_SaNo())
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

        private void SaNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            bool isHaveRow = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sano", (sender as TextBoxT).Text.Trim());
                        cmd.CommandText = "select Count(sano) from RSale where sano=@sano COLLATE Chinese_Taiwan_Stroke_BIN";
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
                using (var frm = new FrmRSale_Print_SaNo())
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
        }



        private void btnUdf_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT6);
            pnlist.Add(groupBoxT8);
            pVar.SaveRadioUdf(pnlist, "RSale");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pVar.ResetRadioUdf("RSale");
            radio1.Checked = true;
            rdFooter1.Checked = true;
            rdHeader1.Checked = true;
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT6);
            pnlist.Add(groupBoxT8);
            pVar.SetRadioUdf(pnlist, "RSale");
        }

        void setSql()
        {
            sql = "";
            sql += " SELECT rsale.fromsale AS rsale_fromsale,rsale.sano AS rsale_sano, rsale.sadate AS rsale_sadate, rsale.sadate1 AS rsale_sadate1, ";
            sql += " rsale.sadate2 AS rsale_sadate2, rsale.sadateac AS rsale_sadateac, rsale.sadateac1 AS rsale_sadateac1, ";
            sql += " rsale.sadateac2 AS rsale_sadateac2, rsale.quno AS rsale_quno, rsale.cono AS rsale_cono, ";
            sql += " rsale.coname1 AS rsale_coname1, rsale.coname2 AS rsale_coname2, rsale.cuno AS rsale_cuno, ";
            sql += " rsale.cuname2 AS rsale_cuname2, rsale.cuname1 AS rsale_cuname1, rsale.cutel1 AS rsale_cutel1, ";
            sql += " rsale.cuper1 AS rsale_cuper1, rsale.emno AS rsale_emno, rsale.emname AS rsale_emname, ";
            sql += " rsale.spno AS rsale_spno, rsale.spname AS rsale_spname, rsale.stno AS rsale_stno, rsale.stname AS rsale_stname, ";
            sql += " rsale.xa1no AS rsale_xa1no, rsale.xa1name AS rsale_xa1name, rsale.xa1par AS rsale_xa1par, ";
            sql += " rsale.taxmnyf AS rsale_taxmnyf, rsale.taxmnyb AS rsale_taxmnyb, rsale.taxmny AS rsale_taxmny, ";
            sql += " rsale.x3no AS rsale_x3no, rsale.rate AS rsale_rate, rsale.x5no AS rsale_x5no, rsale.seno AS rsale_seno, ";
            sql += " rsale.sename AS rsale_sename, rsale.x4no AS rsale_x4no, rsale.x4name AS rsale_x4name, rsale.tax AS rsale_tax, ";
            sql += " rsale.totmny AS rsale_totmny, rsale.taxb AS rsale_taxb, rsale.totmnyb AS rsale_totmnyb, ";
            sql += " rsale.discount AS rsale_discount, rsale.cashmny AS rsale_cashmny, rsale.cardmny AS rsale_cardmny, ";
            sql += " rsale.cardno AS rsale_cardno, rsale.ticket AS rsale_ticket, rsale.collectmny AS rsale_collectmny, ";
            sql += " rsale.getprvacc AS rsale_getprvacc, rsale.acctmny AS rsale_acctmny, rsale.samemo AS rsale_samemo, ";
            sql += " rsale.samemo1 AS rsale_samemo1, rsale.bracket AS rsale_bracket, rsale.recordno AS rsale_recordno, ";
            sql += " rsale.invno AS rsale_invno, rsale.invdate AS rsale_invdate, rsale.invdate1 AS rsale_invdate1, ";
            sql += " rsale.invname AS rsale_invname, rsale.invtaxno AS rsale_invtaxno, rsale.invaddr1 AS rsale_invaddr1, ";
            sql += " rsale.invbatch AS rsale_invbatch, rsale.invbatflg AS rsale_invbatflg, rsale.appdate AS rsale_appdate, ";
            sql += " rsale.edtdate AS rsale_edtdate, rsale.appscno AS rsale_appscno, rsale.edtscno AS rsale_edtscno,rsale.SaPayment as rsale_SaPayment, ";
            sql += " rsale.acno AS rsale_acno, rsale.cloflag AS rsale_cloflag, rsale.UsrNo AS rsale_usrno, rsaled.saID AS rsaled_said, ";
            sql += " rsaled.sano AS rsaled_sano, rsaled.sadate AS rsaled_sadate, rsaled.sadate1 AS rsaled_sadate1, ";
            sql += " rsaled.sadate2 AS rsaled_sadate2, rsaled.sadateac AS rsaled_sadateac, rsaled.sadateac1 AS rsaled_sadateac1, ";
            sql += " rsaled.sadateac2 AS rsaled_sadateac2, rsaled.quno AS rsaled_quno, rsaled.cono AS rsaled_cono, ";
            sql += " rsaled.cuno AS rsaled_cuno, rsaled.emno AS rsaled_emno, rsaled.spno AS rsaled_spno, rsaled.stno AS rsaled_stno, ";
            sql += " rsaled.xa1no AS rsaled_xa1no, rsaled.xa1par AS rsaled_xa1par, rsaled.seno AS rsaled_seno, ";
            sql += " rsaled.sename AS rsaled_sename, rsaled.x4no AS rsaled_x4no, rsaled.x4name AS rsaled_x4name, ";
            sql += " rsaled.orno AS rsaled_orno, rsaled.itno AS rsaled_itno, rsaled.itname AS rsaled_itname, rsaled.ittrait AS rsaled_ittrait,";
            sql += " rsaled.itunit AS rsaled_itunit, rsaled.itpkgqty AS rsaled_itpkgqty, rsaled.qty AS rsaled_qty, ";
            sql += " rsaled.price AS rsaled_price, rsaled.prs AS rsaled_prs, rsaled.rate AS rsaled_rate, rsaled.taxprice AS rsaled_taxprice,";
            sql += " rsaled.mny AS rsaled_mny, rsaled.priceb AS rsaled_priceb, rsaled.taxpriceb AS rsaled_taxpriceb, ";
            sql += " rsaled.mnyb AS rsaled_mnyb, rsaled.memo AS rsaled_memo, rsaled.lowzero AS rsaled_lowzero, ";
            sql += " rsaled.bomid AS rsaled_bomid, rsaled.bomrec AS rsaled_bomrec, rsaled.recordno AS rsaled_recordno, ";
            sql += " rsaled.sltflag AS rsaled_sltflag, rsaled.extflag AS rsaled_extflag, rsaled.bracket AS rsaled_bracket, ";
            sql += " rsaled.itdesp1 AS rsaled_itdesp1, rsaled.itdesp2 AS rsaled_itdesp2, rsaled.itdesp3 AS rsaled_itdesp3, ";
            sql += " rsaled.itdesp4 AS rsaled_itdesp4, rsaled.itdesp5 AS rsaled_itdesp5, rsaled.itdesp6 AS rsaled_itdesp6, ";
            sql += " rsaled.itdesp7 AS rsaled_itdesp7, rsaled.itdesp8 AS rsaled_itdesp8, rsaled.itdesp9 AS rsaled_itdesp9, ";
            sql += " rsaled.itdesp10 AS rsaled_itdesp10, rsaled.stName AS rsaled_stname, rsaled.RecordNo_D AS rsaled_recordno_d, ";
            sql += " rsaled.Punit AS rsaled_Punit, rsaled.Pqty AS rsaled_Pqty, rsaled.Point AS rsaled_Point, ";
            sql += " rsaled.mformula AS rsaled_mformula, rsaled.mwidth4 AS rsaled_mwidth4, rsaled.mwidth3 AS rsaled_mwidth3, ";
            sql += " rsaled.mwidth2 AS rsaled_mwidth2, rsaled.mwidth1 AS rsaled_mwidth1, rsaled.mlong AS rsaled_mlong, ";
            sql += " rsaled.munit AS rsaled_munit, rsaled.mqty AS rsaled_mqty, rsaled.IsTrans AS rsaled_IsTrans, ";
            sql += " rsaled.KiTax AS rsaled_KiTax, rsaled.orid AS rsaled_orid,";
            if (radio2.Checked || radio7.Checked)
            {
                sql += " RSaleBom.SaNo AS rsalebom_sano, RSaleBom.BomID AS rsalebom_bomid, ";
                sql += " RSaleBom.BomRec AS rsalebom_bomrec, RSaleBom.itno AS rsalebom_itno, RSaleBom.itname AS rsalebom_itname, ";
                sql += " RSaleBom.itunit AS rsalebom_itunit, RSaleBom.itqty AS rsalebom_itqty, RSaleBom.itpareprs AS rsalebom_itpareprs, ";
                sql += " RSaleBom.itpkgqty AS rsalebom_itpkgqty, RSaleBom.itrec AS rsalebom_itrec, RSaleBom.itprice AS rsalebom_itprice, ";
                sql += " RSaleBom.itprs AS rsalebom_itprs, RSaleBom.itmny AS rsalebom_itmny, RSaleBom.itnote AS rsalebom_itnote, ";
                sql += " RSaleBom.ItSource AS rsalebom_itsource, RSaleBom.ItBuyPri AS rsalebom_itbuypri, ";
                sql += " RSaleBom.ItBuyMny AS rsalebom_itbuymny, ";
            }
            sql += " cust.cuno AS cust_cuno, cust.cuname2 AS cust_cuname2, ";
            sql += " cust.cuname1 AS cust_cuname1, cust.cuinvoname AS cust_cuinvoname, cust.cuchkname AS cust_cuchkname, ";
            sql += " cust.cuxa1no AS cust_cuxa1no, cust.cupareno AS cust_cupareno, cust.cucono AS cust_cucono, ";
            sql += " cust.cuime AS cust_cuime, cust.cux1no AS cust_cux1no, cust.cuemno1 AS cust_cuemno1, ";
            sql += " cust.cuper1 AS cust_cuper1, cust.cuper2 AS cust_cuper2, cust.cuper AS cust_cuper, cust.cutel1 AS cust_cutel1, ";
            sql += " cust.cutel2 AS cust_cutel2, cust.cufax1 AS cust_cufax1, cust.cuatel1 AS cust_cuatel1, cust.cuatel2 AS cust_cuatel2, ";
            sql += " cust.cuatel3 AS cust_cuatel3, cust.cubbc AS cust_cubbc, cust.cuaddr1 AS cust_cuaddr1, cust.cur1 AS cust_cur1, ";
            sql += " cust.cuaddr2 AS cust_cuaddr2, cust.cur2 AS cust_cur2, cust.cuaddr3 AS cust_cuaddr3, cust.cur3 AS cust_cur3, ";
            sql += " cust.cuslevel AS cust_cuslevel, cust.cudisc AS cust_cudisc, cust.cuemail AS cust_cuemail, ";
            sql += " cust.cuwww AS cust_cuwww, cust.cux2no AS cust_cux2no, cust.cuuno AS cust_cuuno, cust.cux3no AS cust_cux3no, ";
            sql += " cust.cux4no AS cust_cux4no, cust.cucredit AS cust_cucredit, cust.cuengname AS cust_cuengname, ";
            sql += " cust.cuengaddr AS cust_cuengaddr, cust.cuengr1 AS cust_cuengr1, cust.cumemo1 AS cust_cumemo1, ";
            sql += " cust.cumemo2 AS cust_cumemo2, cust.cux5no AS cust_cux5no, cust.cuarea AS cust_cuarea, ";
            sql += " cust.cuudf1 AS cust_cuudf1, cust.cuudf2 AS cust_cuudf2, cust.cuudf3 AS cust_cuudf3, cust.cuudf4 AS cust_cuudf4, ";
            sql += " cust.cuudf5 AS cust_cuudf5, cust.cuudf6 AS cust_cuudf6, cust.cudate AS cust_cudate, cust.cudate1 AS cust_cudate1, ";
            sql += " cust.cudate2 AS cust_cudate2, cust.culastday AS cust_culastday, cust.culastday1 AS cust_culastday1, ";
            sql += " cust.culastday2 AS cust_culastday2, cust.cufirreceiv AS cust_cufirreceiv, cust.cusparercv AS cust_cusparercv, ";
            sql += " cust.cureceiv AS cust_cureceiv, cust.cufirrcvpar AS cust_cufirrcvpar, cust.cufiradvamt AS cust_cufiradvamt, ";
            sql += " cust.cuadvamt AS cust_cuadvamt, cust.cunote AS cust_cunote, cust.cubirth AS cust_cubirth, ";
            sql += " cust.cubirth1 AS cust_cubirth1, cust.cubirth2 AS cust_cubirth2, cust.cusex AS cust_cusex, ";
            sql += " cust.cublood AS cust_cublood, cust.cuidno AS cust_cuidno, cust.ServDate AS cust_servdate, ";
            sql += " cust.BetwDates AS cust_betwdates, cust.MsgFlag AS cust_msgflag, cust.CuPause AS cust_cupause,scrit.scname,scrit.scname1, ";
            sql += " [order].Adaddr as order_Adaddr,[order].orno as order_orno,[order].cuper1 as order_cuper1,[order].AdAddr as order_AdAddr,[order].orpayment as order_orpayment,[order].ormemo as order_ormemo,[order].CardNo as order_CardNo  "; //寶元
            sql += " FROM rsale ";
            sql += " LEFT OUTER JOIN scrit  ON rsale.appscno = scrit.scname ";
            sql += " LEFT OUTER JOIN rsaled ON rsale.sano = rsaled.sano ";
            sql += " LEFT JOIN [order] ON [order].orno = rsaled.orno "; //寶元
            if (radio2.Checked || radio7.Checked)
            {
                sql += " LEFT OUTER JOIN RSaleBom ON rsaled.bomid = RSaleBom.BomID ";
            }
            sql += " LEFT OUTER JOIN cust ON rsale.cuno = cust.cuno";
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
