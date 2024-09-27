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
    public partial class FrmReceivdPrint : Formbase
    {
        DataTable tt = new DataTable();
        DataTable dt = new DataTable(); 
        string reportfilename = "";
        bool notdata = false;
        public string spname="";

        public FrmReceivdPrint()
        {
            InitializeComponent();
        }

        private void FrmReceivdPrint_Load(object sender, EventArgs e)
        {
            reportfilename = "應收帳款沖帳"; 

            radio12.SetUserDefineRpt("ReceivUdf1.rpt");
            radio13.SetUserDefineRpt("ReceivUdf2.rpt");

            SetRdUdf();
        }

        void dataintodocument()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string str = "select "
                    + " receiv.reno as reno,"
                    + " receiv.redate as redate,"
                    + " receiv.redate1 as redate1,"
                    + " receiv.redate2 as redate2,"
                    + " receiv.redateacs as redateacs,"
                    + " receiv.redateacs1 as redateacs1,"
                    + " receiv.redateacs2 as redateacs2,"
                    + " receiv.redateace as redateace,"
                    + " receiv.redateace1 as redateace1,"
                    + " receiv.redateace2 as redateace2,"
                    + " receiv.cono as cono,"
                    + " receiv.coname1 as coname1,"
                    + " receiv.coname2 as coname2,"
                    + " receiv.cuno as cuno,"
                    + " receiv.cuname2 as cuname2,"
                    + " receiv.cuname1 as cuname1,"
                    + " receiv.cutel1 as cutel1,"
                    + " receiv.emno as emno,"
                    + " receiv.emname as emname,"
                    + " receiv.cashmny as cashmny,"
                    + " receiv.cardmny as cardmny,"
                    + " receiv.cardno as cardno,"
                    + " receiv.ticket as ticket,"
                    + " receiv.checkmny as checkmny,"
                    + " receiv.remitmny as remitmny,"
                    + " receiv.othermny as othermny,"
                    + " receiv.getprvacc as getprvacc,"
                    + " receiv.addprvacc as addprvacc,"
                    + " receiv.totmny as totmny,"
                    + " receiv.actslt as actslt,"
                    + " receiv.totdisc as totdisc,"
                    + " receiv.totreve as totreve,"
                    + " receiv.totmny1 as totmny1,"
                    + " receiv.totexgdiff as totexgdiff,"
                    + " receiv.memo1 as 沒用到,"
                    + " receiv.memo2 as memo2,"
                    + " receiv.acno as acno,"
                    + " receiv.sano as sano,"
                    + " receiv.bracket as bracket,"
                    + " receiv.recordno as recordno,"
                    + " receiv.cloflag as cloflag,"
                    + " receiv.ExtFlag as ExtFlag,"
                    + " receivd.reno as reno,"
                    + " receivd.redate as redate,"
                    + " receivd.redate1 as redate1,"
                    + " receivd.redate2 as redate2,"
                    + " receivd.redateacs as redateacs,"
                    + " receivd.redateacs1 as redateacs1,"
                    + " receivd.redateacs2 as redateacs2,"
                    + " receivd.redateace as redateace,"
                    + " receivd.redateace1 as redateace1,"
                    + " receivd.redateace2 as redateace2,"
                    + " receivd.cono as cono,"
                    + " receivd.coname1 as coname1,"
                    + " receivd.coname2 as coname2,"
                    + " receivd.cuno as cuno,"
                    + " receivd.cuname2 as cuname2,"
                    + " receivd.cuname1 as cuname1,"
                    + " receivd.cutel1 as cutel1,"
                    + " receivd.emno as emno,"
                    + " receivd.emname as emname,"
                    + " receivd.recordno as recordno,"
                    + " receivd.sadateac as sadateac,"
                    + " receivd.sadateac1 as sadateac1,"
                    + " receivd.sadateac2 as sadateac2,"
                    + " receivd.sano as sano1,"
                    + " receivd.bracket as bracket1,"
                    + " receivd.xa1no as xa1no,"
                    + " receivd.xa1name as xa1name,"
                    + " receivd.xa1par as xa1par,"
                    + " receivd.totmny as totmny2,"
                    + " receivd.acctmny as acctmny,"
                    + " receivd.invno as invno,"
                    + " receivd.discount as discount,"
                    + " receivd.reverse as reverse,"
                    + " receivd.xa1par1 as xa1par1,"
                    + " receivd.reverseb as reverseb,"
                    + " receivd.exgstat as exgstat,"
                    + " receivd.exgdiff as exgdiff,"
                    + " receivd.memo1 as memo1,"
                    + " receivd.extflag as extflag "
                    + " from receiv "
                    + " right join receivd on receiv.reno = receivd.reno "
                    + " where receivd.reno >=@reno";

                    if (ReNo_1.Text.Trim() != "")
                        str += " and receivd.reno <= @reno1";
                    str += " order by receivd.reno,receivd.recordno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("reno", ReNo.Text.Trim());
                        if (ReNo_1.Text.Trim() != "") cmd.Parameters.AddWithValue("reno1", ReNo_1.Text.Trim());
                        cmd.CommandText = str;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        tt.Clear();
                        da.Fill(tt);

                        da.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (tt.Rows.Count <= 0)
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
                rptPath = Common.reportaddress + "Report\\ReceivRpt1.rpt";
                if (File.Exists(rptPath)) oRpt.Load(rptPath);
                else
                    oRpt.Load(Common.reportaddress + "Report\\應收帳款沖帳_簡要表.rpt");
            }
            else if (radio12.Checked)
            {
                rptPath = Common.reportaddress + "Report\\ReceivUdf1.rpt";
                if (File.Exists(rptPath)) oRpt.Load(rptPath);
            }
            else if (radio13.Checked)
            {
                rptPath = Common.reportaddress + "Report\\ReceivUdf2.rpt";
                if (File.Exists(rptPath)) oRpt.Load(rptPath);
            }

            oRpt.SetDataSource(tt);
            oRpt.Database.Tables[0].ApplyLogOnInfo(Common.logOnInfo);
            TextObject myFieldTitleName;
            try
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                if (radio6.Checked) myFieldTitleName.Text =Common.dtstart.Rows[0]["pnname"].ToString();
                else if (radio7.Checked) myFieldTitleName.Text =Common.dtstart.Rows[1]["pnname"].ToString();
                else if (radio8.Checked) myFieldTitleName.Text =Common.dtstart.Rows[2]["pnname"].ToString();
                else if (radio9.Checked) myFieldTitleName.Text =Common.dtstart.Rows[3]["pnname"].ToString();
                else if (radio10.Checked) myFieldTitleName.Text =Common.dtstart.Rows[4]["pnname"].ToString();
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
                oRpt.SetParameterValue("MST",  Common.MST);
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
         
        private void ReNo_DoubleClick_1(object sender, EventArgs e)
        {
            using (var frm = new FrmReceivdb())
            { 
                frm.tb = ((TextBox)sender);
                if (((TextBox)sender).Name == "ReNo")
                {
                    frm.callType = "PrintReNo";
                }
                else
                {
                    frm.callType = "PrintReNo_1";
                }
                frm.ShowDialog();
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(group1);
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox2);
            pVar.SaveRadioUdf(pnlist, "receiv");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(group1);
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox2);
            pVar.SetRadioUdf(pnlist, "receiv");
        }
    }
}
