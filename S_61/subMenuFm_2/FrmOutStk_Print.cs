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
    public partial class FrmOutStk_Print : Formbase
    {
        RPT rp;
        DataTable dt = new DataTable();
        string path = "";

        string txtstart = "";
        string txtend = "";
        string txtadress = "";
        string txttel = "";
        string 列印地址 = "";

        public string PK;
        public string CuNo;
        //string ReportFileName = "";
        string sql;

        public FrmOutStk_Print()
        {
            InitializeComponent();
        }

        private void FrmOutStk_Print_Load(object sender, EventArgs e)
        {
            OuNo.Text = PK;
            OuNo1.Text = PK;

            //ReportFileName = "寄庫領出作業系統";
            //if (Common.Sys_DBqty == 1)
            //{
            radio5.SetUserDefineRpt("寄庫領出作業系統_簡要自定一.rpt");
            radio7.SetUserDefineRpt("寄庫領出作業系統_組件自定一.rpt");
              
            ToolTip _Tip = new ToolTip();
            _Tip.SetToolTip(radio1, "OuStkRpt1");
            _Tip.SetToolTip(radio2, "OuStkRpt2");
               
            //}
            //else if (Common.Sys_DBqty == 2)
            //{
            //    radio5.SetUserDefineRpt("寄庫領出作業系統_簡要自定一P.rpt");
            //    radio7.SetUserDefineRpt("寄庫領出作業系統_組件自定一P.rpt");

            //    ToolTip _Tip = new ToolTip();
            //    _Tip.SetToolTip(radio1, "OuStkRpt1P");
            //    _Tip.SetToolTip(radio2, "OuStkRpt2P");
           
            //}

            SetRdUdf();
            loadDB();
        }

        void loadDB()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    setSql();
                    sql += " where '0'='0'";
                    if (OuNo.Text != "")
                        sql += " and oustk.ouno >=@ouno";

                    if (OuNo1.Text.Trim() != "")
                        sql += " and oustk.ouno <=@ouno1";

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (OuNo.Text != "") cmd.Parameters.AddWithValue("ouno", OuNo.Text.Trim());
                        if (OuNo1.Text != "") cmd.Parameters.AddWithValue("ouno1", OuNo1.Text.Trim());
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
            string udfPath = Common.reportaddress + "Report\\";
            path = Common.reportaddress + "Report\\" + "寄庫領出作業系統";

            //if (Common.Sys_DBqty == 1)
            //{
                if (radio1.Checked)
                {
                    if (File.Exists(udfPath + "OuStkRpt1.rpt"))
                    {
                        path = udfPath + "OuStkRpt1.rpt";
                    }
                    else
                    {
                        path += "_簡要表.rpt";
                    }
                }
                else if (radio2.Checked)
                {
                    if (File.Exists(udfPath + "OuStkRpt2.rpt"))
                    {
                        path = udfPath + "OuStkRpt2.rpt";
                    }
                    else
                    {
                        path += "_組件明細.rpt";
                    }
                }
                else if (radio5.Checked)
                {
                    path += "_簡要自定一.rpt";
                }
                else if (radio7.Checked)
                {
                    path += "_組件自定一.rpt";
                }
            //}
            //else
            //{
            //    if (radio1.Checked)
            //    {
            //        if (File.Exists(udfPath + "OuStkRpt1P.rpt"))
            //        {
            //            path = udfPath + "OuStkRpt1P.rpt";
            //        }
            //        else
            //        {
            //            path += "_簡要表P.rpt";
            //        }
            //    }
            //    else if (radio2.Checked)
            //    {
            //        if (File.Exists(udfPath + "OuStkRpt2P.rpt"))
            //        {
            //            path = udfPath + "OuStkRpt2P.rpt";
            //        }
            //        else
            //        {
            //            path += "_組件明細P.rpt";
            //        }
            //    }
            //    else if (radio5.Checked)
            //    {
            //        path += "_簡要自定一P.rpt";
            //    }
            //    else if (radio7.Checked)
            //    {
            //        path += "_組件自定一P.rpt";
            //    }
            //}

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
            rp = new RPT(dt, path);
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "txtstart", txtstart });
            rp.lobj.Add(new string[] { "txtadress", txtadress });
            rp.lobj.Add(new string[] { "txttel", txttel });
            rp.lobj.Add(new string[] { "列印地址", 列印地址 });
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

        private void OuNo_DoubleClick(object sender, EventArgs e)
        {
            using (FrmOutStk_Print_OuNo frm = new FrmOutStk_Print_OuNo())
            {
                frm.SeekNo = (sender as TextBoxT).Text;
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        (sender as TextBoxT).Text = frm.SeekNo;
                        break;
                    case DialogResult.Cancel: break;
                }
            }
        }

        private void OuNo_Validating(object sender, CancelEventArgs e)
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
                        cmd.Parameters.AddWithValue("ouno", (sender as TextBoxT).Text.Trim());
                        cmd.CommandText = "select Count(ouno) from OuStk where ouno=@ouno COLLATE Chinese_Taiwan_Stroke_BIN";
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
                using (FrmOutStk_Print_OuNo frm = new FrmOutStk_Print_OuNo())
                {
                    
                    frm.SeekNo = (sender as TextBoxT).Text;
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            (sender as TextBoxT).Text = frm.SeekNo;
                            break;
                        case DialogResult.Cancel: break;
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
            pVar.SaveRadioUdf(pnlist, "OuStk");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pVar.ResetRadioUdf("OuStk");
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
            pVar.SetRadioUdf(pnlist, "OuStk");
        }

        void setSql()
        {
            sql = "";
            sql += " SELECT OuStk.ouno AS oustk_ouno,oustk.sano AS oustk_sano, oustk.oudate AS oustk_oudate, oustk.oudate1 AS oustk_oudate11, ";
            sql+=" oustk.oudate2 AS oustk_oudate2, oustk.oudateac AS oustk_oudateac, oustk.oudateac1 AS oustk_oudateac1, ";
            sql+=" oustk.oudateac2 AS oustk_sadateac2, oustk.quno AS oustk_quno, oustk.cono AS oustk_cono, ";
            sql+=" oustk.coname1 AS oustk_coname1, oustk.coname2 AS oustk_coname2, oustk.cuno AS oustk_cuno, ";
            sql+=" oustk.cuname2 AS oustk_cuname2, oustk.cuname1 AS oustk_cuname1, oustk.cutel1 AS oustk_cutel1, ";
            sql+=" oustk.cuper1 AS oustk_cuper1, oustk.emno AS oustk_emno, oustk.emname AS oustk_emname, ";
            sql+=" oustk.spno AS oustk_spno, oustk.spname AS oustk_spname, oustk.stno AS oustk_stno, oustk.stname AS oustk_stname, ";
            sql+=" oustk.xa1no AS oustk_xa1no, oustk.xa1name AS oustk_xa1name, oustk.xa1par AS oustk_xa1par, ";
            sql+=" oustk.taxmnyf AS oustk_taxmnyf, oustk.taxmnyb AS oustk_taxmnyb, oustk.taxmny AS oustk_taxmny, ";
            sql+=" oustk.x3no AS oustk_x3no, oustk.rate AS oustk_rate, oustk.x5no AS oustk_x5no, oustk.seno AS oustk_seno, ";
            sql+=" oustk.sename AS oustk_sename, oustk.x4no AS oustk_x4no, oustk.x4name AS oustk_x4name, oustk.tax AS oustk_tax, ";
            sql+=" oustk.totmny AS oustk_totmny, oustk.taxb AS oustk_taxb, oustk.totmnyb AS oustk_totmnyb, ";
            sql+=" oustk.discount AS oustk_discount, oustk.cashmny AS oustk_cashmny, oustk.cardmny AS oustk_cardmny, ";
            sql+=" oustk.cardno AS oustk_cardno, oustk.ticket AS oustk_ticket, oustk.collectmny AS oustk_collectmny, ";
            sql+=" oustk.getprvacc AS oustk_getprvacc, oustk.acctmny AS oustk_acctmny, oustk.oumemo AS oustk_oumemo, ";
            sql+=" oustk.oumemo1 AS oustk_oumemo1, oustk.bracket AS oustk_bracket, oustk.recordno AS oustk_recordno, ";
            sql+=" oustk.invno AS oustk_invno, oustk.invdate AS oustk_invdate, oustk.invdate1 AS oustk_invdate1, ";
            sql+=" oustk.invname AS oustk_invname, oustk.invtaxno AS oustk_invtaxno, oustk.invaddr1 AS oustk_invaddr1, ";
            sql+=" oustk.invbatch AS oustk_invbatch, oustk.invbatflg AS oustk_invbatflg, oustk.appdate AS oustk_appdate, ";
            sql+=" oustk.edtdate AS oustk_edtdate, oustk.appscno AS oustk_appscno, oustk.edtscno AS oustk_edtscno, ";
            sql+=" oustk.acno AS oustk_acno, oustk.cloflag AS oustk_cloflag, oustk.UsrNo AS oustk_usrno, oustkd.ouID AS oustkd_ouID, ";
            sql+=" OuStkD.ouno AS oustkd_ouno,OuStkD.inno AS oustkd_inno,oustkd.sano AS oustkd_sano, oustkd.oudate AS oustkd_oudate, oustkd.oudate1 AS oustkd_oudate1, ";
            sql+=" oustkd.oudate2 AS oustkd_oudate2, oustkd.oudateac AS oustkd_oudateac, oustkd.oudateac1 AS oustkd_oudateac1, ";
            sql+=" oustkd.oudateac2 AS oustkd_oudateac2, oustkd.quno AS oustkd_quno, oustkd.cono AS oustkd_cono, ";
            sql+=" oustkd.cuno AS oustkd_cuno, oustkd.emno AS oustkd_emno, oustkd.spno AS oustkd_spno, oustkd.stno AS oustkd_stno, ";
            sql+=" oustkd.xa1no AS oustkd_xa1no, oustkd.xa1par AS oustkd_xa1par, oustkd.seno AS oustkd_seno, ";
            sql+=" oustkd.sename AS oustkd_sename, oustkd.x4no AS oustkd_x4no, oustkd.x4name AS oustkd_x4name, ";
            sql+=" oustkd.orno AS oustkd_orno, oustkd.itno AS oustkd_itno, oustkd.itname AS oustkd_itname, oustkd.ittrait AS oustkd_ittrait, ";
            sql += " oustkd.itunit AS oustkd_itunit, oustkd.itpkgqty AS oustkd_itpkgqty, oustkd.ouqty AS oustkd_ouqty, oustkd.qty AS oustkd_qty, ";
            sql+=" oustkd.price AS oustkd_price, oustkd.prs AS oustkd_prs, oustkd.rate AS oustkd_rate, oustkd.taxprice AS oustkd_taxprice, ";
            sql+=" oustkd.mny AS oustkd_mny, oustkd.priceb AS oustkd_priceb, oustkd.taxpriceb AS oustkd_taxpriceb, ";
            sql+=" oustkd.mnyb AS oustkd_mnyb, oustkd.memo AS oustkd_memo, oustkd.lowzero AS oustkd_lowzero, ";
            sql+=" oustkd.bomid AS oustkd_bomid, oustkd.bomrec AS oustkd_bomrec, oustkd.recordno AS oustkd_recordno, ";
            sql+=" oustkd.sltflag AS oustkd_sltflag, oustkd.extflag AS oustkd_extflag, oustkd.bracket AS oustkd_bracket, ";
            sql+=" oustkd.itdesp1 AS oustkd_itdesp1, oustkd.itdesp2 AS oustkd_itdesp2, oustkd.itdesp3 AS oustkd_itdesp3, ";
            sql+=" oustkd.itdesp4 AS oustkd_itdesp4, oustkd.itdesp5 AS oustkd_itdesp5, oustkd.itdesp6 AS oustkd_itdesp6, ";
            sql+=" oustkd.itdesp7 AS oustkd_itdesp7, oustkd.itdesp8 AS oustkd_itdesp8, oustkd.itdesp9 AS oustkd_itdesp9, ";
            sql+=" oustkd.itdesp10 AS oustkd_itdesp10, oustkd.stName AS oustkd_stname, oustkd.RecordNo_D AS oustkd_recordno_d, ";
            sql+=" oustkd.istrans AS oustkd_istrans,oustkd.indate AS oustkd_indate,oustkd.indate1 AS oustkd_indate1,";
            if (radio2.Checked || radio7.Checked)
            {
                sql += " OuStkBOM.OuNo AS oustkbom_ouno, OuStkBOM.BomID AS oustkbom_bomid, ";
                sql += " OuStkBOM.BomRec AS oustkbom_bomrec, OuStkBOM.itno AS oustkbom_itno, OuStkBOM.itname AS oustkbom_itname, ";
                sql += " OuStkBOM.itunit AS oustkbom_itunit, OuStkBOM.itqty AS oustkbom_itqty, OuStkBOM.itpareprs AS oustkbom_itpareprs, ";
                sql += " OuStkBOM.itpkgqty AS oustkbom_itpkgqty, OuStkBOM.itrec AS oustkbom_itrec, OuStkBOM.itprice AS oustkbom_itprice, ";
                sql += " OuStkBOM.itprs AS oustkbom_itprs, OuStkBOM.itmny AS oustkbom_itmny, OuStkBOM.itnote AS oustkbom_itnote, ";
                sql += " OuStkBOM.ItSource AS oustkbom_itsource, OuStkBOM.ItBuyPri AS oustkbom_itbuypri, ";
                sql += " OuStkBOM.ItBuyMny AS oustkbom_itbuymny, ";
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
            sql += " cust.BetwDates AS cust_betwdates, cust.MsgFlag AS cust_msgflag, cust.CuPause AS cust_cupause";
            sql += " FROM oustk LEFT OUTER JOIN";
            sql += " oustkD ON oustk.ouno = oustkD.ouno LEFT OUTER JOIN";
            if (radio2.Checked || radio7.Checked)
            {
                sql += " OuStkBOM ON OuStkD.bomid = OuStkBOM.BomID LEFT OUTER JOIN";
            }
            sql += " cust ON oustk.cuno = cust.cuno";
        }




    }
}
