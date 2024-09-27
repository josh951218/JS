using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmBomPrint : Formbase
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        //DataTable tt = new DataTable();
        List<ButtonT> bt = new List<ButtonT>();
        string reportfilename = "";
        bool notdata = false;

        public FrmBomPrint()
        {
            InitializeComponent();
            cn.ConnectionString = Common.sqlConnString;
        }

        private void FrmBomPrint_Load(object sender, EventArgs e)
        {
            bt.Add(btnexcel);
            bt.Add(btnword);
            bt.Add(btnprint);
            bt.Add(btnpreview);
            bt.Add(btnExit);

            reportfilename = "組合組裝品建檔"; 
            radio13.SetUserDefineRpt("組合組裝品建檔_自訂一.rpt");
            radio14.SetUserDefineRpt("組合組裝品建檔_自訂二.rpt");
             
        }

        void dataintodocument()
        {
            try
            {
                ds.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@BoItNo", BoItNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@BoItNo_1", BoItNo_1.Text.Trim());

                    string str = "select * from Bom right join Bomd on Bom.BoItNo=Bomd.BoItNo where '0'='0'";
                    if (BoItNo.Text.Trim() != "")
                        str += " and Bom.BoItNo >= @BoItNo";
                    if (BoItNo_1.Text.Trim() != "")
                        str += " and Bom.BoItNo <= @BoItNo_1";
                    str += " order by Bom.BoRec,Bomd.ItRec";
                    cmd.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //da.Fill(tt);
                    da.Fill(ds, "命令");

                    cmd.CommandText = @"
                            Select item.itno, itnoudf, itname, kino, itime, ittrait, itunit, itunitp, itpkgqty, itbuypri, itprice, itprice1, itprice2, itprice3, itprice4, 
                                    itprice5, itcost, itbuyprip, itpricep, itpricep1, itpricep2, itpricep3, itpricep4, itpricep5, itcostp, itbuyunit, itsalunit, 
                                    itsafeqty, itlastqty, itnw, itnwunit, itcostslt, itcodeslt, itcodeno, itdesp1, itdesp2, itdesp3, itdesp4, itdesp5, itdesp6, 
                                    itdesp7, itdesp8, itdesp9, itdesp10, itdate, itdate1, itdate2, itbuydate, itbuydate1, itbuydate2, itsaldate, itsaldate1, 
                                    itSaldate2, itfircost, itfirtqty, itfirtcost, itstockqty, itnote, itudf1, itudf2, itudf3, itudf4, itudf5, itweblist, itwebpic, itwebctl1, 
                                    itwebctl2, IsUse, ItSource, itpicture, IsEnable, fano, Punit, ScNo,Pic
                            from (
		                            Select itno from Bomd where Bomd.BoItNo>=(@BoItNo) and Bomd.BoItNo <=(@BoItNo_1) group by itno
	                                )A
                            left join item on A.itno = item.itno
                                                ";
                    da.Fill(ds, "item");

                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadDBError:\n" + ex.ToString());
            }
            if (ds.Tables["命令"].Rows.Count <= 0)
            {
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                notdata = true;
                return;
            }
            Common.FrmReport = new Report.Frmreport();

            ReportDocument oRpt = new ReportDocument();
            if (radio11.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_標準表.rpt");
            else if (radio12.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_成本表.rpt");
            else if (radio13.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂一.rpt");
            else if (radio14.Checked) oRpt.Load(Common.reportaddress + "Report\\" + reportfilename + "_自訂二.rpt");

            oRpt.SetDataSource(ds);
            oRpt.Database.Tables[0].ApplyLogOnInfo(Common.logOnInfo);

            TextObject myFieldTitleName;
            List<TextObject> Txt = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();
            try
            {
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                    if (radio6.Checked) myFieldTitleName.Text =Common.dtstart.Rows[0]["pnname"].ToString();
                    else if (radio7.Checked) myFieldTitleName.Text =Common.dtstart.Rows[1]["pnname"].ToString();
                    else if (radio8.Checked) myFieldTitleName.Text =Common.dtstart.Rows[2]["pnname"].ToString();
                    else if (radio9.Checked) myFieldTitleName.Text =Common.dtstart.Rows[3]["pnname"].ToString();
                    else if (radio10.Checked) myFieldTitleName.Text =Common.dtstart.Rows[4]["pnname"].ToString();
                    else myFieldTitleName.Text = "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            try
            {
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (radio1.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                    else if (radio2.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                    else if (radio3.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                    else if (radio4.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                    else if (radio5.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
            }
            catch { }
            try
            {
                if (Txt.Find(t => t.Name == "txtadress") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtadress"];
                    myFieldTitleName.Text = Common.dtSysSettings.Rows[0]["StcAddr1"].ToString();
                }
            }
            catch { }
            try
            {
                if (Txt.Find(t => t.Name == "txttel") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txttel"];
                    myFieldTitleName.Text = "TEL：" + Common.dtSysSettings.Rows[0]["StcTel"].ToString()
                       + " FAX：" + Common.dtSysSettings.Rows[0]["StcTel"].ToString();
                }
            }
            catch { }

            List<ParameterField> Num = oRpt.ParameterFields.OfType<ParameterField>().ToList();

            if (Num.Find(p => p.Name == "備註說明") != null)
            {
                oRpt.SetParameterValue("備註說明", Common.Sys_MemoUdf);
            }
            if (Num.Find(p => p.Name == "Q") != null)
                oRpt.SetParameterValue("Q", Common.Q);
            if (Num.Find(p => p.Name == "M") != null)
                oRpt.SetParameterValue("M", Common.M);
            if (Num.Find(p => p.Name == "MFT") != null)
                oRpt.SetParameterValue("MFT", Common.dtSysSettings.Rows[0]["MnyDeciFt"].ToString());//進貨單據小數
            if (Num.Find(p => p.Name == "MF") != null)
                oRpt.SetParameterValue("MF", Common.MF);
            if (Num.Find(p => p.Name == "date") != null)
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
            PrintDialog printDialog1 = new PrintDialog();
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                Common.FrmReport.rpt1.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                Common.FrmReport.rpt1.PrintToPrinter(1, true, 0, 0);
            }
            printDialog1.Dispose();
            Common.FrmReport.Dispose();
        }

        private void btnpreview_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (notdata) return;
            Common.FrmReport.ShowDialog();

        }

        private void BoItNo_DoubleClick(object sender, EventArgs e)
        {
            using (FrmBomBrow frm = new FrmBomBrow())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    ((TextBox)sender).Text = frm.Result.Trim();
                else ((TextBox)sender).SelectAll();
            }
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

        private void BoItNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tx = ((TextBox)sender);
            da.SelectCommand.Parameters["@no"].Value = tx.Text.Trim();
            dt.Clear();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                e.Cancel = true;
                tx.SelectAll();

                using (FrmBomBrow frm = new FrmBomBrow())
                {
                    if (frm.ShowDialog() == DialogResult.OK) tx.Text = frm.Result.Trim();
                }
            }
        }













































    }
}
