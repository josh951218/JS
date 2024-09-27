using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;
using System.IO;
using JBS;

namespace S_61.Report
{
    public partial class Frmreport : Formbase
    {
        public short PCS = 1;

        string reportname = "";

        public Frmreport()
        {
            InitializeComponent();
            Common.FrmReport = this;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Frmreport_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = true;

            string str = rpt1.FileName.ToUpper();
            if (str.Contains("REPORT"))
            {
                int i = str.IndexOf("REPORT");
                this.Text = str.Substring(i + 7);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Frmreport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rpt1 != null)
                rpt1.Close();

            if (cview != null)
                cview.Dispose();

            if (rpt1 != null)
                rpt1.Dispose();

            Common.FrmReport = null;
            this.Dispose();

            GC.Collect();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string str = rpt1.FileName.ToUpper();
            if (str.Contains("REPORT"))
            {
                int i = str.IndexOf("REPORT");
                reportname = str.Substring(i + 7);
            }

            PrintDialog print = new PrintDialog();
            print.AllowSelection = true;
            print.AllowSomePages = true;
            print.PrinterSettings.Collate = true;
            print.PrinterSettings.Copies = PCS;

            try
            {
                if (print.ShowDialog() != DialogResult.OK)
                    return;

                if (print.PrinterSettings.SupportsColor)
                {
                    #region 事務機
                    rpt1.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

                    if (print.PrinterSettings.DefaultPageSettings.PaperSource.Kind == PaperSourceKind.AutomaticFeed)
                        rpt1.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                    else if (print.PrinterSettings.DefaultPageSettings.PaperSource.Kind == PaperSourceKind.Upper)
                        rpt1.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Upper;
                    else if (print.PrinterSettings.DefaultPageSettings.PaperSource.Kind == PaperSourceKind.Middle)
                        rpt1.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Middle;
                    else if (print.PrinterSettings.DefaultPageSettings.PaperSource.Kind == PaperSourceKind.Manual)
                        rpt1.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Manual;

                    PrinterSettings printerSettings = new PrinterSettings();
                    PageSettings pageSettings = new PageSettings();
                    rpt1.PrintOptions.CopyTo(printerSettings, pageSettings);
                    printerSettings.PrinterName = print.PrinterSettings.PrinterName;
                    printerSettings.PrintRange = print.PrinterSettings.PrintRange;
                    printerSettings.Collate = print.PrinterSettings.Collate;
                    printerSettings.Copies = print.PrinterSettings.Copies;
                    printerSettings.FromPage = print.PrinterSettings.FromPage;
                    printerSettings.ToPage = print.PrinterSettings.ToPage;

                    //新修改:不管letter 或是 122, 印事務機都轉成A4
                    var pA4 = print.PrinterSettings.PaperSizes
                        .OfType<System.Drawing.Printing.PaperSize>()
                        .Where(p => p.PaperName.Trim().ToUpper().StartsWith("A4"))
                        .FirstOrDefault();

                    if (pA4 != null)
                    {
                        //rpt1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)pA4.RawKind;
                        pageSettings.PaperSize = pA4;
                    }

                    pageSettings.PaperSource = print.PrinterSettings.DefaultPageSettings.PaperSource;
                    pageSettings.PrinterSettings = printerSettings;

                    CrystalDecisions.Shared.PageMargins margins = new CrystalDecisions.Shared.PageMargins();
                    margins.leftMargin = 240;
                    margins.rightMargin = 0;
                    margins.topMargin = 144;
                    margins.bottomMargin = 144;
                    rpt1.PrintOptions.ApplyPageMargins(margins);

                    if (pageSettings.PaperSize.Width > 840)
                    {
                        //若報表格式是latter/latter small,表寬等於850
                        //表寬較大,所以左邊界設100就好,剛好置中
                        pageSettings.Margins.Left = 100;
                        pageSettings.Margins.Right = 10;
                        pageSettings.Margins.Top = 10;
                        pageSettings.Margins.Bottom = 10;

                    }
                    else
                    {
                        //若報表格式是122,轉印到印表機會變成A4,表寬等於827
                        pageSettings.Margins.Left = 150;
                        pageSettings.Margins.Right = 10;
                        pageSettings.Margins.Top = 10;
                        pageSettings.Margins.Bottom = 10;

                    }

                    rpt1.PrintToPrinter(printerSettings, pageSettings, false);
                    #endregion
                }
                else
                {
                    #region 點陣機
                    if (reportname.Contains("自定"))//是否為自定表
                    {
                        rpt1.PrintOptions.PrinterName = print.PrinterSettings.PrinterName;
                        rpt1.PrintToPrinter(print.PrinterSettings.Copies, print.PrinterSettings.Collate, print.PrinterSettings.FromPage, print.PrinterSettings.ToPage);
                    }
                    else if (reportname.Contains("自訂"))//是否為自定表
                    {
                        rpt1.PrintOptions.PrinterName = print.PrinterSettings.PrinterName;
                        rpt1.PrintToPrinter(print.PrinterSettings.Copies, print.PrinterSettings.Collate, print.PrinterSettings.FromPage, print.PrinterSettings.ToPage);
                    }
                    else if (reportname.Contains("應收分組表"))//是否為自定表
                    {
                        rpt1.PrintOptions.PrinterName = print.PrinterSettings.PrinterName;
                        rpt1.PrintToPrinter(print.PrinterSettings.Copies, print.PrinterSettings.Collate, print.PrinterSettings.FromPage, print.PrinterSettings.ToPage);
                    }
                    else if (reportname.ToList().TrueForAll(c => char.IsUpper(c) || char.IsDigit(c) || c == '.'))//是否為自定表
                    {
                        rpt1.PrintOptions.PrinterName = print.PrinterSettings.PrinterName;
                        rpt1.PrintToPrinter(print.PrinterSettings.Copies, print.PrinterSettings.Collate, print.PrinterSettings.FromPage, print.PrinterSettings.ToPage);
                    }
                    else
                    {
                        //點陣印表機,不管報表邊界如何設定,接套用以下格式,均會置中
                        //122印出來寬度比較大一點,latter small印出來會比122的寬度小一點點
                        //250:360=100:X    =>   X=144;
                        CrystalDecisions.Shared.PageMargins mg = new CrystalDecisions.Shared.PageMargins();
                        mg.leftMargin = 144;
                        mg.rightMargin = 0;
                        mg.topMargin = 144;
                        mg.bottomMargin = 144;
                        rpt1.PrintOptions.ApplyPageMargins(mg);


                        if (Common.Sys_LendToSaleMode != 2)
                        {
                            //以報表為主
                        }
                        else if (reportname.Contains("A") || reportname.Contains("B"))
                        {
                            //報表已經是半頁/全頁, 就以報表為主
                        }
                        else if (rpt1.PrintOptions.PaperSize == CrystalDecisions.Shared.PaperSize.PaperLetterSmall || rpt1.PrintOptions.PaperSize == CrystalDecisions.Shared.PaperSize.PaperLetter)
                        {
                            //若報表沒有半頁/全頁之分, 就以列印時所選的紙張格式為主
                            PageSettings ps = new PageSettings();
                            ps.PrinterSettings = print.PrinterSettings;
                            if (ps.PaperSize.Height == 550 && ps.PaperSize.Width == 850)
                            {
                                var p122 = print.PrinterSettings.PaperSizes
                                    .OfType<System.Drawing.Printing.PaperSize>()
                                    .Where(p => p.PaperName == "122")
                                    .FirstOrDefault();

                                if (p122 != null)
                                {
                                    rpt1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)p122.RawKind;
                                }
                            }
                        }


                        rpt1.PrintOptions.PrinterName = print.PrinterSettings.PrinterName;
                        rpt1.PrintToPrinter(print.PrinterSettings.Copies, print.PrinterSettings.Collate, print.PrinterSettings.FromPage, print.PrinterSettings.ToPage);
                    }
                    #endregion
                }
            }
            catch
            {
                #region 事務機
                rpt1.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

                if (print.PrinterSettings.DefaultPageSettings.PaperSource.Kind == PaperSourceKind.AutomaticFeed)
                    rpt1.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                else if (print.PrinterSettings.DefaultPageSettings.PaperSource.Kind == PaperSourceKind.Upper)
                    rpt1.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Upper;
                else if (print.PrinterSettings.DefaultPageSettings.PaperSource.Kind == PaperSourceKind.Middle)
                    rpt1.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Middle;
                else if (print.PrinterSettings.DefaultPageSettings.PaperSource.Kind == PaperSourceKind.Manual)
                    rpt1.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Manual;

                PrinterSettings printerSettings = new PrinterSettings();
                PageSettings pageSettings = new PageSettings();
                rpt1.PrintOptions.CopyTo(printerSettings, pageSettings);
                printerSettings.PrinterName = print.PrinterSettings.PrinterName;
                printerSettings.PrintRange = print.PrinterSettings.PrintRange;
                printerSettings.Collate = print.PrinterSettings.Collate;
                printerSettings.Copies = print.PrinterSettings.Copies;
                printerSettings.FromPage = print.PrinterSettings.FromPage;
                printerSettings.ToPage = print.PrinterSettings.ToPage;

                //新修改:不管letter 或是 122, 印事務機都轉成A4
                var pA4 = print.PrinterSettings.PaperSizes
                    .OfType<System.Drawing.Printing.PaperSize>()
                    .Where(p => p.PaperName.Trim().ToUpper().StartsWith("A4"))
                    .FirstOrDefault();

                if (pA4 != null)
                {
                    //rpt1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)pA4.RawKind;
                    pageSettings.PaperSize = pA4;
                }

                pageSettings.PaperSource = print.PrinterSettings.DefaultPageSettings.PaperSource;
                pageSettings.PrinterSettings = printerSettings;

                CrystalDecisions.Shared.PageMargins margins = new CrystalDecisions.Shared.PageMargins();
                margins.leftMargin = 240;
                margins.rightMargin = 0;
                margins.topMargin = 144;
                margins.bottomMargin = 144;
                rpt1.PrintOptions.ApplyPageMargins(margins);

                if (pageSettings.PaperSize.Width > 840)
                {
                    //若報表格式是latter/latter small,表寬等於850
                    //表寬較大,所以左邊界設100就好,剛好置中
                    pageSettings.Margins.Left = 100;
                    pageSettings.Margins.Right = 10;
                    pageSettings.Margins.Top = 10;
                    pageSettings.Margins.Bottom = 10;

                }
                else
                {
                    //若報表格式是122,轉印到印表機會變成A4,表寬等於827
                    pageSettings.Margins.Left = 150;
                    pageSettings.Margins.Right = 10;
                    pageSettings.Margins.Top = 10;
                    pageSettings.Margins.Bottom = 10;

                }

                rpt1.PrintToPrinter(printerSettings, pageSettings, false);
                #endregion
            }
            finally
            {
                print.Dispose();

                //if (rpt1 != null)
                //    rpt1.Close();

                //if (cview != null)
                //    cview.Dispose();

                //if (rpt1 != null)
                //    rpt1.Dispose();

                //Common.FrmReport = null;
                //this.Dispose();

                //GC.Collect();
            }
        }

        //public void excel(string ReportFileName)
        //{
        //    try
        //    {
        //        //Random Rn = new Random();
        //        //int intRn = Rn.Next(1000);
        //        //if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
        //        //{
        //        //    System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
        //        //}
        //        //Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".xls");
        //        //Process.Start(Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".xls");

        //        Random Rn = new Random();
        //        int intRn = Rn.Next(1000);
        //        if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
        //        {
        //            System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
        //        }
        //        Environment.SetEnvironmentVariable("TEMP", Application.StartupPath + "\\temp", EnvironmentVariableTarget.User);

        //        Common.FrmReport.rpt1.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
        //        CrystalDecisions.Shared.PageMargins margins = new CrystalDecisions.Shared.PageMargins();
        //        margins.leftMargin = 0;
        //        margins.rightMargin = 0;
        //        margins.topMargin = 0;
        //        margins.bottomMargin = 0;
        //        Common.FrmReport.rpt1.PrintOptions.ApplyPageMargins(margins);

        //        // 宣告變數並取得匯出選項。  
        //        ExportOptions exportOpts = new ExportOptions();
        //        ExcelFormatOptions excelFormatOpts = new ExcelFormatOptions();
        //        DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
        //        exportOpts = Common.FrmReport.rpt1.ExportOptions;

        //        // 設定 Excel 格式選項。 
        //        excelFormatOpts.ExcelConstantColumnWidth = 10;
        //        //excelFormatOpts.ExcelAreaType = AreaSectionKind.WholeReport;
        //        excelFormatOpts.ConvertDateValuesToString = true;
        //        //excelFormatOpts.ExcelUseConstantColumnWidth = true;
        //        excelFormatOpts.ExportPageBreaksForEachPage = false;
        //        excelFormatOpts.ExportPageHeadersAndFooters = ExportPageAreaKind.OnEachPage;
        //        excelFormatOpts.ShowGridLines = true;

        //        exportOpts.ExportFormatType = ExportFormatType.Excel;
        //        exportOpts.FormatOptions = excelFormatOpts;

        //        // 設定磁碟檔案選項並匯出。  
        //        exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
        //        diskOpts.DiskFileName = Application.StartupPath + "\\temp\\" + reportname + intRn + ".xls";
        //        exportOpts.DestinationOptions = diskOpts;

        //        Common.FrmReport.rpt1.Export();
        //        Process.Start(Application.StartupPath + "\\temp\\" + reportname + intRn + ".xls");
        //    }
        //    finally
        //    {
        //        if (rpt1 != null)
        //            rpt1.Close();

        //        if (cview != null)
        //            cview.Dispose();

        //        if (rpt1 != null)
        //            rpt1.Dispose();

        //        Common.FrmReport = null;
        //        this.Dispose();

        //        GC.Collect();
        //    }
        //}


        public void excel(string ReportFileName, bool 是否解析Excel = false, string 明細區_頭value = "", string 明細區_尾value = "", string 明細區_欄位對應value = "", bool IncludeDetailHeadRow_ = true, bool IncludeDetailTailRow_ = true)
        {
            try
            {
                reportname = ReportFileName;
                Random Rn = new Random();
                int intRn = Rn.Next(1000);
                if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
                {
                    System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
                }
                Environment.SetEnvironmentVariable("TEMP", Application.StartupPath + "\\temp", EnvironmentVariableTarget.User);

                Common.FrmReport.rpt1.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                CrystalDecisions.Shared.PageMargins margins = new CrystalDecisions.Shared.PageMargins();
                margins.leftMargin = 0;
                margins.rightMargin = 0;
                margins.topMargin = 0;
                margins.bottomMargin = 0;
                Common.FrmReport.rpt1.PrintOptions.ApplyPageMargins(margins);

                // 宣告變數並取得匯出選項。  
                ExportOptions exportOpts = new ExportOptions();
                ExcelFormatOptions excelFormatOpts = new ExcelFormatOptions();
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                exportOpts = Common.FrmReport.rpt1.ExportOptions;

                // 設定 Excel 格式選項。 
                excelFormatOpts.ExcelConstantColumnWidth = 10;
                //excelFormatOpts.ExcelAreaType = AreaSectionKind.WholeReport;
                excelFormatOpts.ConvertDateValuesToString = true;
                //excelFormatOpts.ExcelUseConstantColumnWidth = true;
                excelFormatOpts.ExportPageBreaksForEachPage = false;
                excelFormatOpts.ExportPageHeadersAndFooters = ExportPageAreaKind.OnEachPage;
                excelFormatOpts.ShowGridLines = true;

                exportOpts.ExportFormatType = ExportFormatType.Excel;
                exportOpts.FormatOptions = excelFormatOpts;

                // 設定磁碟檔案選項並匯出。  
                exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
                diskOpts.DiskFileName = Application.StartupPath + "\\temp\\" + reportname + intRn + ".xls";
                exportOpts.DestinationOptions = diskOpts;

                Common.FrmReport.rpt1.Export();
                if (是否解析Excel)
                {
                    ResloveCrystalReportExcel ResloveCrystalReportExcel = new ResloveCrystalReportExcel(diskOpts.DiskFileName, diskOpts.DiskFileName + "_解析檔案.xls", 明細區_頭value, 明細區_尾value, 明細區_欄位對應value, IncludeDetailHeadRow_, IncludeDetailTailRow_);
                    bool Exists = File.Exists(diskOpts.DiskFileName + "_解析檔案.xls");
                    while (!Exists)
                    {
                        Exists = File.Exists(diskOpts.DiskFileName + "_解析檔案.xls");
                    }
                    Process.Start(diskOpts.DiskFileName + "_解析檔案.xls");
                }
                else
                {
                    Process.Start(diskOpts.DiskFileName);
                }
            }
            finally
            {
                if (rpt1 != null)
                    rpt1.Close();

                if (cview != null)
                    cview.Dispose();

                if (rpt1 != null)
                    rpt1.Dispose();

                Common.FrmReport = null;
                this.Dispose();

                GC.Collect();
            }
        }



        public void word(string ReportFileName)
        {
            try
            {
                Random Rn = new Random();
                int intRn = Rn.Next(1000);
                if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
                {
                    System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
                }
                Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".doc");
                Process.Start(Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".doc");
            }
            finally
            {
                if (rpt1 != null)
                    rpt1.Close();

                if (cview != null)
                    cview.Dispose();

                if (rpt1 != null)
                    rpt1.Dispose();

                Common.FrmReport = null;
                this.Dispose();

                GC.Collect();
            }
        }

        public void Mail(string pdfname)
        {
            try
            {
                var path = Application.StartupPath + @"\SDMail";
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                this.rpt1.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath + @"\SDMail\" + pdfname + ".pdf");

                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    JBS.SendMail mail = new JBS.SendMail(Common.sqlConnString, pdfname);
                    mail.Send();
                });

                MessageBox.Show("信件已發送!");
            }
            finally
            {
                if (rpt1 != null)
                    rpt1.Close();

                if (cview != null)
                    cview.Dispose();

                if (rpt1 != null)
                    rpt1.Dispose();

                Common.FrmReport = null;
                this.Dispose();

                GC.Collect();
            }
        }
    }
}
