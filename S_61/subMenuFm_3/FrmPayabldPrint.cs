using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmPayabldPrint : Formbase
    {
        DataTable dt = new DataTable();
        string reportfilename = "";
        public string spname;
        bool notdata = false;

        public FrmPayabldPrint()
        {
            InitializeComponent();
        }

        private void FrmPayabldPrint_Load(object sender, EventArgs e)
        { 
            reportfilename = "應付帳款沖帳"; 

            radio12.SetUserDefineRpt("PayabldUdf1.rpt");
            radio13.SetUserDefineRpt("PayabldUdf2.rpt");
             
            SetRdUdf();
        }

        void dataintodocument()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd=cn.CreateCommand())
                    {
                        string str = "select Payabl.PaNo as PaNo,Payabl.PaDate as PaDate,Payabl.PaDate1 as PaDate1,Payabl.PaDate2 as PaDate2,Payabl.PaDateacs as PaDateacs"
                            +",Payabl.PaDateacs1 as PaDateacs1,Payabl.PaDateacs2 as PaDateacs2,Payabl.PaDateace as PaDateace,Payabl.PaDateace1 as PaDateace1"
                            +",Payabl.PaDateace2 as PaDateace2,Payabl.cono as cono,Payabl.coname1 as coname1,Payabl.coname2 as coname2,Payabl.FaNo as FaNo"
                            +",Payabl.FaName2 as FaName2,Payabl.FaName1 as FaName1,Payabl.FaTel1 as FaTel1,Payabl.emno as emno,Payabl.emname as emname"
                            +",Payabl.cashmny as cashmny,Payabl.cardmny as cardmny,Payabl.cardno as cardno,Payabl.ticket as ticket,Payabl.checkmny as checkmny"
                            +",Payabl.remitmny as remitmny,Payabl.othermny as othermny,Payabl.getprvacc as getprvacc,Payabl.addprvacc as addprvacc,Payabl.totmny as totmny"
                            +",Payabl.actslt as actslt,Payabl.totdisc as totdisc,Payabl.totreve as totreve,Payabl.totmny1 as totmny1,Payabl.totexgdiff as totexgdiff"
                            +",Payabl.memo1 as 沒用到,Payabl.memo2 as memo2,Payabl.acno as acno,Payabl.BsNo as BsNo,Payabl.bracket as bracket,Payabl.recordno as recordno"
                            +",Payabl.cloflag as cloflag,Payabl.ExtFlag as ExtFlag,Payabld.PaNo as PaNo,Payabld.PaDate as PaDate,Payabld.PaDate1 as PaDate1"
                            +",Payabld.PaDate2 as PaDate2,Payabld.PaDateacs as PaDateacs,Payabld.PaDateacs1 as PaDateacs1,Payabld.PaDateacs2 as PaDateacs2"
                            +",Payabld.PaDateace as PaDateace,Payabld.PaDateace1 as PaDateace1,Payabld.PaDateace2 as PaDateace2,Payabld.cono as cono"
                            +",Payabld.coname1 as coname1,Payabld.coname2 as coname2,Payabld.FaNo as FaNo,Payabld.FaName2 as FaName2,Payabld.FaName1 as FaName1"
                            +",Payabld.FaTel1 as FaTel1,Payabld.emno as emno,Payabld.emname as emname,Payabld.recordno as recordno,Payabld.BsDateac as BsDateac"
                            +",Payabld.BsDateac1 as BsDateac1,Payabld.BsDateac2 as BsDateac2,Payabld.BsNo as BsNo1,Payabld.bracket as bracket1,Payabld.xa1no as xa1no"
                            +",Payabld.xa1name as xa1name,Payabld.xa1par as xa1par,Payabld.totmny as totmny2,Payabld.acctmny as acctmny,Payabld.invno as invno"
                            +",Payabld.discount as discount,Payabld.reverse as reverse,Payabld.xa1par1 as xa1par1,Payabld.reverseb as reverseb,Payabld.exgstat as exgstat"
                            +",Payabld.exgdiff as exgdiff,Payabld.memo1 as memo1,Payabld.extflag as extflag from Payabl right join Payabld on Payabl.PaNo=Payabld.PaNo"
                            + " where Payabld.PaNo >=@pano ";

                        if (PaNo_1.Text.Trim() != "")
                            str += " and Payabld.PaNo <=@pano1 ";
                        str += " order by Payabld.PaNo,Payabld.recordno";

                        cmd.Parameters.AddWithValue("pano", PaNo.Text.Trim());
                        if (PaNo_1.Text.Trim() != "") cmd.Parameters.AddWithValue("pano1", PaNo_1.Text.Trim());

                        cmd.CommandText = str;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        dt.Clear();
                        da.Fill(dt);

                        da.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                notdata = true;
                return;
            }

            Common.FrmReport = new Report.Frmreport();
            ReportDocument oRpt = new ReportDocument();
            string rptPath = "";

            if (radio11.Checked)
            {
                rptPath = Common.reportaddress + "Report\\PayabldRpt1.rpt";
                if (File.Exists(rptPath)) oRpt.Load(rptPath);
                else
                    oRpt.Load(Common.reportaddress + "Report\\應付帳款沖帳_簡要表.rpt");
            }
            else if (radio12.Checked)
            {
                rptPath = Common.reportaddress + "Report\\PayabldUdf1.rpt";
                if (File.Exists(rptPath)) oRpt.Load(rptPath);
            }
            else if (radio13.Checked)
            {
                rptPath = Common.reportaddress + "Report\\PayabldUdf2.rpt";
                if (File.Exists(rptPath)) oRpt.Load(rptPath);
            }

            oRpt.SetDataSource(dt);
            oRpt.Database.Tables[0].ApplyLogOnInfo(Common.logOnInfo);
            TextObject myFieldTitleName;

            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                if (radio6.Checked) myFieldTitleName.Text = Common.dtstart.Rows[0]["pnname"].ToString();
                else if (radio7.Checked) myFieldTitleName.Text = Common.dtstart.Rows[1]["pnname"].ToString();
                else if (radio8.Checked) myFieldTitleName.Text = Common.dtstart.Rows[2]["pnname"].ToString();
                else if (radio9.Checked) myFieldTitleName.Text = Common.dtstart.Rows[3]["pnname"].ToString();
                else if (radio10.Checked) myFieldTitleName.Text = Common.dtstart.Rows[4]["pnname"].ToString();
                else myFieldTitleName.Text = "";

                if (spname.Trim().Length > 0)
                {
                    myFieldTitleName.Text = spname;
                }
            }
            catch { }
            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                if (radio1.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                else if (radio2.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                else if (radio3.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                else if (radio4.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                else if (radio5.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                else myFieldTitleName.Text = "";
            }
            catch { }

            List<ParameterField> num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
            if (num.Find(p => p.Name == "顯示千分位") != null)
            {
                if (pVar.TRS != "")
                    pVar.ShowTRS = true;
                oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
            }
            if (num.Find(p => p.Name == "千分位") != null)
                oRpt.SetParameterValue("千分位", pVar.TRS);
            if (num.Find(p => p.Name == "MST") != null)
            {
                oRpt.SetParameterValue("MST", Common.MST);
            }
            if (num.Find(p => p.Name == "M") != null)
            {
                oRpt.SetParameterValue("M", Common.M);
            }
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

            Common.FrmReport.cview.ReportSource = oRpt;
            Common.FrmReport.rpt1 = oRpt;

        }

        private void btnpreview_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (notdata) return;
            Common.FrmReport.ShowDialog();

        }

        private void btnword_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (notdata) return;
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".doc");
            Process.Start(Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".doc");
            Common.FrmReport.Dispose();

        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (notdata) return;
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".xls");
            Process.Start(Application.StartupPath + "\\temp\\" + reportfilename + intRn + ".xls");
            Common.FrmReport.Dispose();
        }
         
        private void PaNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmPayabldb())
            {
                frm.tb = ((TextBox)sender);
                if (((TextBox)sender).Name == "PaNo")
                {
                    frm.callType = "PrintPaNo";
                }
                else
                {
                    frm.callType = "PrintPaNo_1";
                }
                frm.ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (notdata) return;
            Common.FrmReport.button1_Click(null, null);
            Common.FrmReport.Dispose();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(group1);
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox2);
            pVar.SaveRadioUdf(pnlist, "Payabl");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(group1);
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox2);
            pVar.SetRadioUdf(pnlist, "Payabl");
        }
    }
}
