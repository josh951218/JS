using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmAdjust_print : Formbase
    {
        JBS.JS.Adjust jAdjust;

        RPT rp;
        DataTable dt = new DataTable();
        string path = "";

        string txtstart = "";
        string txtend = "";
        string txtadress = "";
        string txttel = "";

        public string PK;
        public string CuNo; 
        string sql;

        public FrmAdjust_print()
        {
            InitializeComponent();
            this.jAdjust = new JBS.JS.Adjust();
        }

        private void FrmAdjust_print_Load(object sender, EventArgs e)
        {
            AdNo.Text = PK;
            AdNo1.Text = PK;
             
            radio5.SetUserDefineRpt("庫存調整作業系統_簡要自定一.rpt");
            radio7.SetUserDefineRpt("庫存調整作業系統_組件自定一.rpt");

            AdNo.Focus();
            loadDB();

            SetRdUdf();
        }

        void loadDB()
        {
            try
            {
                setsql();
                sql += "where '0'='0'";
                if (AdNo.Text != "")
                    sql += " and a.adno >=@adno ";
                if (AdNo1.Text != "")
                    sql += " and a.adno <=@adno1 ";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (AdNo.Text != "") cmd.Parameters.AddWithValue("adno", AdNo.Text.Trim());
                        if (AdNo1.Text != "") cmd.Parameters.AddWithValue("adno1", AdNo1.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void paramsInit()
        {
            loadDB();
            path = Common.reportaddress + "Report\\" + "庫存調整作業系統";
            if (radio1.Checked)
            {
                path += "_簡要表.rpt";
            }
            else if (radio2.Checked)
            {
                path += "_規格說明.rpt";
            }
            else if (radio3.Checked)
            {
                path += "_組件明細.rpt";
            }
            else if (radio5.Checked)
            {
                path += "_簡要自定一.rpt";
            }
            else
            {
                path += "_組件自定一.rpt";
            }

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

            rp = new RPT(dt, path);
            rp.txtstart = txtstart;
            rp.txtend = txtend;
            rp.txtadress = txtadress;
            rp.txttel = txttel;
            rp.office = "庫存調整作業系統";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Print();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.PreView();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Word();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Excel();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AdNo_DoubleClick(object sender, EventArgs e)
        {
            jAdjust.Open<JBS.JS.Adjust>(sender);
        }

        private void AdNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jAdjust.ValidateOpen<JBS.JS.Adjust>(sender, e, row =>
            {
                AdNo.Text = row["AdNo"].ToString().Trim();
            }, true);
        }

        void setsql()
        {
            sql = "";
            sql += "SELECT a.adno AS adjust_adno, a.addate AS adjust_addate, a.addate1 AS adjust_addate1, a.addate2 AS adjust_addate2, ";
            sql += "a.cono AS adjust_cono, a.coname1 AS adjust_coname1, a.coname2 AS adjust_coname2, a.stno AS adjust_stno, ";
            sql += "a.stname AS adjust_stname, a.emno AS adjust_emno, a.emname AS adjust_emname, a.totmnyb AS adjust_totmnyb, ";
            sql += "a.AppDate ASadjust_AppDate,a.EdtDate AS adjust_EdtDate, a.AppScNo AS adjust_AppScNo, a.EdtScNo AS adjust_EdtScNo,a.admemo1 AS adjust_admemo1,";//製單人員+詳細備註
            sql += "a.costflag AS adjust_costflag, a.admemo AS adjust_admemo, a.bracket AS adjust_bracket,";
            sql += "a.recordno AS adjust_recordno, a.UsrNo AS adjust_usrno, b.adno AS adjustd_adno, b.addate AS adjustd_addate,";
            sql += "b.addate1 AS adjustd_addate1, b.addate2 AS adjustd_addate2, b.cono AS adjustd_cono, b.stno AS adjustd_stno,";
            sql += "b.emno AS adjustd_emno, b.costflag AS adjustd_costflag, b.itno AS adjustd_itno, b.itname AS adjustd_itname,";
            sql += "b.ittrait AS adjustd_ittrait, b.itunit AS adjustd_itunit, b.itpkgqty AS adjustd_itpkgqty, b.qty AS adjustd_qty,";
            sql += "b.costb AS adjustd_costb, b.costb1 AS adjustd_costb1, b.mnyb AS adjustd_mnyb, b.memo AS adjustd_memo,";
            sql += "b.lowzero AS adjustd_lowzro, b.bomid AS adjustd_bomid, b.bomrec AS adjustd_bomrec,";
            sql += "b.recordno AS adjustd_recordno, b.sltflag AS adjustd_sltflag, b.extflag AS adjustd_extflag, ";
            sql += "b.bracket AS adjustd_bracket, b.itdesp1 AS adjustd_itdesp1, b.itdesp2 AS adjustd_itdesp2,";
            sql += "b.itdesp3 AS adjustd_itdesp3, b.itdesp4 AS adjustd_itdesp4, b.itdesp5 AS adjustd_itdesp5,";
            sql += "b.itdesp6 AS adjustd_itdesp6, b.itdesp7 AS adjustd_itdesp7, b.itdesp8 AS adjustd_itdesp8,";
            sql += "b.itdesp9 AS adjustd_itdesp9, b.itdesp10 AS adjustd_itdesp310, b.stname AS adjustd_stname,scrit.scname,scrit.scname1 ";
            if (radio3.Checked == true)
            {
                sql += ",c.AdNo AS adjuBom_AdNo, c.BomID AS adjuBom_BomID, c.BomRec AS adjuBom_BomRec, c.itno AS adjuBom_itno,";
                sql += "c.itname AS adjuBom_itname, c.itunit AS adjuBom_itunit, c.itqty AS adjuBom_itqty, c.itpareprs AS adjuBom_tpareprs,";
                sql += "c.itpkgqty AS adjuBom_itpkgqty, c.itrec AS adjuBom_itrec, c.itprice AS adjuBom_itprice, c.itprs AS adjuBom_itprs,";
                sql += "c.itmny AS adjuBom_itmny, c.itnote AS adjuBom_itnote, c.ItSource AS adjuBom_itsource, ";
                sql += "c.ItBuyPri AS adjuBom_itbuypri, c.ItBuyMny AS adjuBom_itbuymny";
            }
            sql += @" FROM adjust AS a LEFT  JOIN adjustd AS b ON a.adno = b.adno 
                                       Left join scrit on a.appscno = scrit.scname ";

            if (radio3.Checked == true)
            {
                sql += " LEFT  JOIN ";
                sql += " adjuBom AS c ON c.bomid = b.bomid ";
            }
        }
         
        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SaveRadioUdf(pnlist, "Adjust");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT3);
            pVar.SetRadioUdf(pnlist, "Adjust");
        }
    }
}
