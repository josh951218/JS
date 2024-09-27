using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using S_61.Basic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO.Ports;

namespace JBS.JS
{
    /// <summary>
    /// 此類別似乎是專案設計的發票列印
    /// 發票版面寫死
    /// 發票列印功能寫在銷貨單[三聯式發票列印]
    /// 發票列印PORT抓[使用者參數設定->後台銷貨單據發票列印]
    /// </summary>
    class SalePrintInvNo
    {
        DataTable dtSaleD;
        int perpage = 9;

        private string AddRightLength(string str, int len)
        {
            str = str.GetUTF8(len);
            var count = Encoding.GetEncoding(950).GetByteCount(str);
            count = len - count;

            if (count <= 0)
                return str;

            var space = "";
            for (int i = 0; i < count; i++)
            {
                space += " ";
            }
            return str + space;
        }
        private string AddLeftLength(string str, int len)
        {
            str = str.GetUTF8(len);
            var count = Encoding.GetEncoding(950).GetByteCount(str);
            count = len - count;

            if (count <= 0)
                return str;

            var space = "";
            for (int i = 0; i < count; i++)
            {
                space += " ";
            }

            return space + str;
        }
        private string RepeatLength(char str, int len)
        {
            var list = Enumerable.Repeat(str, len);
            return new string(list.ToArray());
        }
        private void Release()
        {
            if (dtSaleD == null)
                return;

            dtSaleD.Clear();
            dtSaleD = null;
        }
        #region 發票機列印
        public void doInv(DataTable dt)
        {
            if (Common.User_InvSalePort.Trim().Length == 0)
            {
                MessageBox.Show("未設定發票機名稱(或是COM Port), 無法列印!");
                return;
            }

            this.dtSaleD = dt.Copy();

            if (Common.User_InvSalePort.ToUpper().StartsWith("COM"))
            {
                InvCOMPort();
            }
            else
            {
                InvPrint();
            }

            this.Release();
        }
        #endregion
        #region 發票機-驅動
        private void InvPrint()
        {
            try
            {
                //初始化發票機，設定可印中文字
                USB.Write(Common.User_InvSalePort, 指令集.初始化);
                USB.Write(Common.User_InvSalePort, 指令集.可印中文字);

                //if (this.InvPortBox == 1)
                //{
                //    USB.Write(Common.User_InvSalePort, 指令集.開錢櫃);
                //}

                InvPrint_Page();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void InvPrint_Page()
        {
            //計算共需幾頁
            var page = 0;
            if (dtSaleD.Rows.Count % perpage == 0)
                page = dtSaleD.Rows.Count / perpage;
            else
                page = (dtSaleD.Rows.Count / perpage) + 1;

            //每頁印表頭、表身
            for (int i = 1; i <= page; i++)
            {
                InvPrint_Header(i);
                InvPrint_Detail(i);

                //換頁切斷, 最後一頁不切斷, 印完表尾再切斷
                if (i < page)
                {
                    USB.Write(Common.User_InvSalePort, 指令集.換頁切斷);
                    //JBS.JM.NSale.PutInNewInvNo(i);
                }
            }
            //最後一頁印表尾前補空白行
            var c = perpage - (dtSaleD.Rows.Count % perpage);
            for (int i = 0; i < c; i++)
            {
                USB.Write(Common.User_InvSalePort, 指令集.跳行);
            }

            //最後一頁印表尾
            InvPrint_Footer();
            USB.Write(Common.User_InvSalePort, 指令集.換頁切斷);

            //JBS.JM.NSale.PutInNewInvNo(page);
        }
        private void InvPrint_Header(int p)
        {
            //印表頭
            //USB.Write(Common.User_InvSalePort, 指令集.跳行);
            //USB.Write(Common.User_InvSalePort, 指令集.跳行);
            //USB.Write(Common.User_InvSalePort, 指令集.跳行);
            USB.Write(Common.User_InvSalePort, 指令集.跳行);
            USB.Write(Common.User_InvSalePort, 指令集.跳行);
            USB.WriteLine(Common.User_InvSalePort, " " + Common.iTitle);
            USB.WriteLine(Common.User_InvSalePort, " " + Common.iAddress);
            USB.WriteLine(Common.User_InvSalePort, " " + "統編:" + Common.iUnno + "電話:" + Common.iTel);
            USB.Write(Common.User_InvSalePort, 指令集.跳行);
            if (dtSaleD.Rows[0]["sale_cardno"].ToString().Length == 5) 
            {
                var abc = dtSaleD.Rows[0]["sale_cardno"].ToString();
                USB.Write(Common.User_InvSalePort, "信用卡:" + dtSaleD.Rows[0]["sale_cardno"].ToString());
            }
            else if (dtSaleD.Rows[0]["sale_cardno"].ToString().Length == 14)
            {
                string 二至三 = dtSaleD.Rows[0]["sale_cardno"].ToString().Substring(2, 2);
                string 六至七 = dtSaleD.Rows[0]["sale_cardno"].ToString().Substring(6, 2);
                string 十至十三 = dtSaleD.Rows[0]["sale_cardno"].ToString().Substring(10, 4);
                USB.Write(Common.User_InvSalePort, "ATM: **"+二至三 + "**" + 六至七 + "**" + 十至十三);
            }

            var day = dtSaleD.Rows[0]["sale_invdate"].ToString();
            day = Date.ToTWDate(day);
            day = Date.AddLine(day);
            USB.WriteLine(Common.User_InvSalePort, " " + "     " + day + " 頁:" + p);
            USB.WriteLine(Common.User_InvSalePort, " " + "     " + "統一編號:" + dtSaleD.Rows[0]["sale_invtaxno"].ToString());

            //第六行時間，第七行客戶統編
            //USB.WriteLine(Common.User_InvSalePort, "憑證:" + NPOS.SaNo);
            //USB.WriteLine(Common.User_InvSalePort, DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
            //USB.WriteLine(Common.User_InvSalePort, "統一編號:" + NPOS.CuUno);
        }
        private void InvPrint_Detail(int p)
        {
            int max = 30;
            int qlen = 6;
            int plen = 7;

            USB.Write(Common.User_InvSalePort, 指令集.跳行);
            USB.WriteLine(Common.User_InvSalePort, AddRightLength("  " + "品名規格", max + 2 - (qlen + plen)) + AddLeftLength("數量", qlen) + AddLeftLength("金額", plen));
            USB.WriteLine(Common.User_InvSalePort, "  " + RepeatLength('=', max));

            var start = (p - 1) * perpage;
            for (int i = 0; i < perpage; i++)
            {
                if (start + (i + 1) > dtSaleD.Rows.Count)
                    break;

                var item = dtSaleD.Rows[start + i]["saled_itname"].ToString().Trim().GetUTF8(max - (qlen + plen));
                item = AddRightLength(item.Trim(), max - (qlen + plen));
                var qty = dtSaleD.Rows[start + i]["saled_qty"].ToDecimal();
                var prs = dtSaleD.Rows[start + i]["saled_prs"].ToDecimal();
                var price = dtSaleD.Rows[start + i]["saled_price"].ToDecimal();
                var mny = (qty * price * prs).ToDecimal("f" + Common.TPS);

                USB.WriteLine(Common.User_InvSalePort, "  " + item + AddLeftLength(qty.ToString("f0"), qlen) + AddLeftLength(mny.ToString("f0"), plen));
            }
        }
        private void InvPrint_Footer()
        {
            int max = 30;
            USB.Write(Common.User_InvSalePort, 指令集.跳行);
            USB.Write(Common.User_InvSalePort, 指令集.跳行);
            USB.Write(Common.User_InvSalePort, 指令集.跳行);
            USB.Write(Common.User_InvSalePort, 指令集.跳行);
            USB.WriteLine(Common.User_InvSalePort, "  " + RepeatLength('=', max));
            
            //if (NPOS.CardMny > 0)
            //{
            //    USB.WriteLine(Common.User_InvSalePort, "信用卡" + AddLeftLength(NPOS.CardMny.ToString("f0"), 8));
            //}
            USB.WriteLine(Common.User_InvSalePort, "          " + "    稅前金額" + AddLeftLength(dtSaleD.Rows[0]["sale_taxmny"].ToDecimal().ToString("f" + Common.MST), max - 22));
            USB.WriteLine(Common.User_InvSalePort, "          " + "    營業稅額" + AddLeftLength(dtSaleD.Rows[0]["sale_tax"].ToDecimal().ToString("f" + Common.MST), max - 22));
            USB.WriteLine(Common.User_InvSalePort, "          " + "    應收總額" + AddLeftLength(dtSaleD.Rows[0]["sale_totmny"].ToDecimal().ToString("f" + Common.MST), max - 22));
        }
        #endregion
        #region 發票機-COMPORT
        private void InvCOMPort()
        {
            try
            {
                using (var jComPort = new ComPort(Common.User_InvSalePort))
                {
                    jComPort.Ready();
                    jComPort.Write(指令集.初始化);
                    jComPort.Write(指令集.可印中文字);

                    //if (this.InvPortBox == 1)
                    //{
                    //    jComPort.Write(指令集.開錢櫃);
                    //}

                    InvCOMPort_Page(jComPort);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void InvCOMPort_Page(ComPort jComPort)
        {
            //計算共需幾頁
            var page = 0;
            if (dtSaleD.Rows.Count % perpage == 0)
                page = dtSaleD.Rows.Count / perpage;
            else
                page = (dtSaleD.Rows.Count / perpage) + 1;

            //每頁印表頭、表身
            for (int i = 1; i <= page; i++)
            {
                InvCOMPort_Header(jComPort, i);
                InvCOMPort_Detail(jComPort, i);

                //換頁切斷, 最後一頁不切斷, 印完表尾再切斷
                if (i < page)
                {
                    jComPort.Write(指令集.換頁切斷);
                    //NSale.PutInNewInvNo(i);
                }
            }
            //最後一頁印表尾前補空白行
            var c = perpage - (dtSaleD.Rows.Count % perpage);
            for (int i = 0; i < c; i++)
            {
                jComPort.Write(指令集.跳行);
            }

            //最後一頁印表尾
            InvCOMPort_Footer(jComPort);
            jComPort.Write(指令集.換頁切斷);

            //NSale.PutInNewInvNo(page);
        }
        private void InvCOMPort_Header(ComPort jComPort, int p)
        {
            //印表頭
            //jComPort.Write(指令集.跳行);
            //jComPort.Write(指令集.跳行);
            //jComPort.Write(指令集.跳行);
            jComPort.Write(指令集.跳行);
            jComPort.Write(指令集.跳行);
            jComPort.WriteLine(" " + Common.iTitle);
            jComPort.WriteLine(" " + Common.iAddress);
            jComPort.WriteLine(" " + "統編:" + Common.iUnno + "電話:" + Common.iTel);
            jComPort.Write(指令集.跳行);
            if (dtSaleD.Rows[0]["sale_cardno"].ToString().Length == 5)
            {
                var abc = dtSaleD.Rows[0]["sale_cardno"].ToString();
                USB.Write(Common.User_InvSalePort, "信用卡:" + dtSaleD.Rows[0]["sale_cardno"].ToString());
            }
            else if (dtSaleD.Rows[0]["sale_cardno"].ToString().Length == 14)
            {
                string 二至三 = dtSaleD.Rows[0]["sale_cardno"].ToString().Substring(2, 2);
                string 六至七 = dtSaleD.Rows[0]["sale_cardno"].ToString().Substring(6, 2);
                string 十至十三 = dtSaleD.Rows[0]["sale_cardno"].ToString().Substring(10, 4);
                USB.Write(Common.User_InvSalePort, "ATM: **" + 二至三 + "**" + 六至七 + "**" + 十至十三);
            }

            var day = dtSaleD.Rows[0]["sale_invdate"].ToString();
            day = Date.ToTWDate(day);
            day = Date.AddLine(day);
            jComPort.WriteLine(" " + "     " + day + " 頁:" + p);
            jComPort.WriteLine(" " + "     " + "統一編號:" + dtSaleD.Rows[0]["sale_invtaxno"].ToString());

            //第六行時間，第七行客戶統編
            //jComPort.WriteLine("憑證:" + NPOS.SaNo);
            //jComPort.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
            //jComPort.WriteLine("統一編號:" + NPOS.CuUno);
        }
        private void InvCOMPort_Detail(ComPort jComPort, int p)
        {
            int max = 30;
            int qlen = 6;
            int plen = 7;

            jComPort.Write(指令集.跳行);
            jComPort.WriteLine(AddRightLength("  " + "品名規格", max + 2 - (qlen + plen)) + AddLeftLength("數量", qlen) + AddLeftLength("金額", plen));
            jComPort.WriteLine("  " + RepeatLength('=', max));

            var start = (p - 1) * perpage;
            for (int i = 0; i < perpage; i++)
            {
                if (start + (i + 1) > dtSaleD.Rows.Count)
                    break;

                var item = dtSaleD.Rows[start + i]["saled_itname"].ToString().Trim().GetUTF8(max - (qlen + plen));
                item = AddRightLength(item.Trim(), max - (qlen + plen));
                var qty = dtSaleD.Rows[start + i]["saled_qty"].ToDecimal();
                var prs = dtSaleD.Rows[start + i]["saled_prs"].ToDecimal();
                var price = dtSaleD.Rows[start + i]["saled_price"].ToDecimal();
                var mny = (qty * price * prs).ToDecimal("f" + Common.TPS);

                jComPort.WriteLine("  " + item + AddLeftLength(qty.ToString("f0"), qlen) + AddLeftLength(mny.ToString("f0"), plen));
            }
        }
        private void InvCOMPort_Footer(ComPort jComPort)
        {
            int max = 30;
            jComPort.Write(指令集.跳行);
            jComPort.Write(指令集.跳行);
            jComPort.Write(指令集.跳行);
            jComPort.Write(指令集.跳行);
            jComPort.WriteLine("  " + RepeatLength('=', max));
             
            //if (NPOS.CardMny > 0)
            //{
            //    jComPort.WriteLine("信用卡" + AddLeftLength(NPOS.CardMny.ToString("f0"), 8));
            //} 
            jComPort.WriteLine("          " + "    稅前金額" + AddLeftLength(dtSaleD.Rows[0]["sale_taxmny"].ToDecimal().ToString("f" + Common.MST), max - 22));
            jComPort.WriteLine("          " + "    營業稅額" + AddLeftLength(dtSaleD.Rows[0]["sale_tax"].ToDecimal().ToString("f" + Common.MST), max - 22));
            jComPort.WriteLine("          " + "    應收總額" + AddLeftLength(dtSaleD.Rows[0]["sale_totmny"].ToDecimal().ToString("f" + Common.MST), max - 22));
        }
        #endregion

        public static class 指令集
        {
            public static readonly byte[] 初始化 = new byte[] { 27, 64 };
            public static readonly byte[] 開錢櫃 = new byte[] { 27, 112, 0, 50, 250 };
            public static readonly byte[] 跳行 = new byte[] { 27, 100, 1 };
            public static readonly byte[] 換頁切斷 = new byte[] { 12 };
            public static readonly byte[] 移至行首 = new byte[] { 13 };

            public static readonly byte[] 僅印收執聯 = new byte[] { 27, 99, 48, 2 };
            public static readonly byte[] 印存根聯與收銀聯 = new byte[] { 27, 99, 48, 3 };
            public static readonly byte[] 開啟雙向同步列印 = new byte[] { 27, 122, 1 };
            public static readonly byte[] 可印中文字 = new byte[] { 28, 38 };
        }

        public class USB
        {
            public static void Write(string port, byte[] cmd)
            {
                IntPtr pUnmanagedBytes = new IntPtr(0);
                pUnmanagedBytes = Marshal.AllocCoTaskMem(cmd.Length);

                Marshal.Copy(cmd, 0, pUnmanagedBytes, cmd.Length);
                RawPrinterHelper.SendBytesToPrinter(port, pUnmanagedBytes, cmd.Length);
                Marshal.FreeCoTaskMem(pUnmanagedBytes);
            }

            public static void Write(string port, string msg)
            {
                USB.Write(port, Encoding.GetEncoding(950).GetBytes(msg));
            }

            public static void WriteLine(string port, string msg)
            {
                USB.Write(port, Encoding.GetEncoding(950).GetBytes(msg + Environment.NewLine));
            }
        }

        public class ComPort : IDisposable
        {
            #region 建構解構
            private SerialPort sp;
            private bool disposed = false;

            public ComPort(string port)
            {
                this.sp = new SerialPort();

                sp.BaudRate = 9600;
                sp.Parity = Parity.None;
                sp.DataBits = 8;
                sp.StopBits = StopBits.One;
                sp.Encoding = Encoding.GetEncoding(950);
                sp.PortName = port;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                if (disposing)
                {
                    // Free any other managed objects here.
                    if (this.sp.IsOpen)
                        this.sp.Close();

                    this.sp.Dispose();
                }

                // Free any unmanaged objects here.
                //

                disposed = true;
            }

            ~ComPort()
            {
                Dispose(false);
            }
            #endregion

            public void Ready()
            {
                sp.Open();
                sp.Write(指令集.初始化, 0, 指令集.初始化.Length);
            }

            public void Write(byte[] cmd)
            {
                sp.Write(cmd, 0, cmd.Length);
            }

            public void Write(string msg)
            {
                sp.Write(msg);
            }

            public void WriteLine(string msg)
            {
                sp.WriteLine(msg);
            }

            public static void WriteOne(string port, byte[] cmd)
            {
                using (var jComPort = new ComPort(port))
                {
                    jComPort.Ready();
                    jComPort.Write(指令集.初始化);
                    jComPort.Write(指令集.可印中文字);
                    jComPort.Write(cmd);
                }
            }
        }

        public class CashDrawer : IDisposable
        {
            #region 建構解構
            private SerialPort sp;
            private bool disposed = false;

            public CashDrawer(string port)
            {
                this.sp = new SerialPort();

                sp.BaudRate = 9600;
                sp.Parity = Parity.None;
                sp.DataBits = 8;
                sp.StopBits = StopBits.One;
                sp.Encoding = Encoding.GetEncoding(950);
                sp.PortName = port;

                sp.Open();
                sp.Write(指令集.初始化, 0, 指令集.初始化.Length);
                sp.Close();
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                if (disposing)
                {
                    // Free any other managed objects here.
                    this.sp.Dispose();
                }

                // Free any unmanaged objects here.
                //

                disposed = true;
            }

            ~CashDrawer()
            {
                Dispose(false);
            }
            #endregion

            public void Open()
            {
                if (sp.IsOpen)
                    sp.Close();

                sp.Open();
                sp.Write(指令集.初始化, 0, 指令集.初始化.Length);
                sp.Write(指令集.開錢櫃, 0, 指令集.開錢櫃.Length);
                sp.Close();
            }
        }

        public class DSP870A : IDisposable
        {
            #region 建構解構
            private SerialPort sp;
            private bool disposed = false;

            public DSP870A(string port)
            {
                this.sp = new SerialPort();

                sp.BaudRate = 9600;
                sp.Parity = Parity.None;
                sp.DataBits = 8;
                sp.StopBits = StopBits.One;
                sp.Encoding = Encoding.GetEncoding(950);
                sp.PortName = port;

                sp.Open();
                sp.Write(初始化);
                sp.Write(捲動模式);
                sp.Close();

            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                if (disposing)
                {
                    // Free any other managed objects here.
                    this.sp.Dispose();
                }

                // Free any unmanaged objects here.
                //

                disposed = true;
            }

            ~DSP870A()
            {
                Dispose(false);
            }
            #endregion

            string 初始化 = ((char)27).ToString() + ((char)64).ToString();
            string 捲動模式 = ((char)27).ToString() + ((char)18).ToString();
            string MoveToFront = ((char)13).ToString();
            string 定位 = ((char)31).ToString() + ((char)108).ToString() + ((char)32).ToString() + ((char)01).ToString();

            public void Clear()
            {
                if (sp.IsOpen)
                    sp.Close();

                sp.Open();
                sp.Write(初始化);
                sp.Write(捲動模式);
                sp.Close();
            }

            public void Show(string msg)
            {
                if (sp.IsOpen)
                    sp.Close();

                sp.Open();
                sp.WriteLine(msg);
                sp.Write(MoveToFront);
                sp.Close();
            }
        }

        class RawPrinterHelper
        {
            // Structure and API declarions:
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class DOCINFOA
            {
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDocName;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pOutputFile;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDataType;
            }
            [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

            [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool ClosePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

            [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndDocPrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

            // SendBytesToPrinter()
            // When the function is given a printer name and an unmanaged array
            // of bytes, the function sends those bytes to the print queue.
            // Returns true on success, false on failure.
            public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
            {
                Int32 dwError = 0, dwWritten = 0;
                IntPtr hPrinter = new IntPtr(0);
                DOCINFOA di = new DOCINFOA();
                bool bSuccess = false; // Assume failure unless you specifically succeed.

                di.pDocName = "My C#.NET RAW Document";
                di.pDataType = "RAW";

                // Open the printer.
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page.
                        if (StartPagePrinter(hPrinter))
                        {
                            // Write your bytes.
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                // If you did not succeed, GetLastError may give more information
                // about why not.
                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
                return bSuccess;
            }

            //private void openCashDrawer()
            //{
            //    //下面這行請改成錢櫃的指令
            //    byte[] codeOpenCashDrawer = new byte[] { 27, 112, 48, 55, 121 };
            //    IntPtr pUnmanagedBytes = new IntPtr(0);
            //    pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
            //    Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5);
            //    //下面這行的第一個參數"EPSON xxxx" 請改成印表機名稱，名稱哦~~很方便的
            //    RawPrinterHelper.SendBytesToPrinter("jPOS", pUnmanagedBytes, 5);
            //    Marshal.FreeCoTaskMem(pUnmanagedBytes);
            //}
        }

    }
}
