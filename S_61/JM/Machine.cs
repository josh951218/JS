using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;
using System.Diagnostics;
using System.ComponentModel;
using S_61.subMenuFm_1;
using S_61.ecr;
using System.IO;
using System.Text.RegularExpressions;

namespace JBS.JM
{
    public class Machine
    {
        public string TicketPort { get; set; }
        public int TicketPortBox { get; set; }
        public string InvPort { get; set; }
        public int InvPortBox { get; set; }
        public string X5No { get; set; }//3:二聯 4:三聯
        public string VideoPort { get; set; }
        public string MoneyPort { get; set; }
        public string OffLinePort { get; set; }
        public string HalfPort { get; set; }
        public string PaperPort { get; set; }
        public string cardPort { get; set; }//刷卡機
        public string InvT1 { get; set; }
        public string InvT2 { get; set; }
        public string InvT3 { get; set; }
        public string InvT4 { get; set; }
        public string InvT5 { get; set; }
        public string X3No { get; set; }
        public int InvNoWarning { get; set; }

        public string EmNo { get; set; }
        public string CuInvNo { get; set; }
        string dir = System.Windows.Forms.Application.StartupPath;
        string[] read = new string[9];
        bool exist = false;
        
        DataTable dtHalf;
        DataTable dtSaleD;
        DataTable dtTicket;
        string machine = "";
        int perpage = 10;

        public Machine(string machinenumber)
        {
            this.machine = machinenumber;
            InitializeDatabase();
        }
        public void InitializeDatabase()
        {
            this.TicketPort = "";
            this.TicketPortBox = 2;
            this.InvPort = "";
            this.InvPortBox = 2;
            this.MoneyPort = "";
            this.VideoPort = "";
            this.OffLinePort = "";
            this.HalfPort = "";
            this.PaperPort = "";
            this.cardPort = "";

            this.InvT1 = "";
            this.InvT2 = "";
            this.InvT3 = "";
            this.InvT4 = "";
            this.InvT5 = "";
            this.X3No = "";
            this.InvNoWarning = 0;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("machine", this.machine);
                cmd.CommandText = "Select top 1 * from MachineSet where machine = @machine ";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        this.TicketPort = reader["TicketPort"].ToString().Trim();
                        this.TicketPortBox = reader["TicketPortBox"].ToInteger();

                        this.InvPort = reader["InvPort"].ToString().Trim();
                        this.InvPortBox = reader["InvPortBox"].ToInteger();

                        this.MoneyPort = reader["MoneyPort"].ToString().Trim();
                        this.VideoPort = reader["VideoPort"].ToString().Trim();
                        this.OffLinePort = reader["OffLinePort"].ToString().Trim();
                        this.HalfPort = reader["HalfPort"].ToString().Trim();
                        this.PaperPort = reader["PaperPort"].ToString().Trim();
                        this.cardPort = reader["cardPort"].ToString().Trim();

                        this.InvT1 = reader["InvT1"].ToString();
                        this.InvT2 = reader["InvT2"].ToString();
                        this.InvT3 = reader["InvT3"].ToString();
                        this.InvT4 = reader["InvT4"].ToString();
                        this.InvT5 = reader["InvT5"].ToString();
                        this.InvNoWarning = reader["InvNoWarning"].ToInteger();

                        this.X5No = reader["X5No"].ToString();
                        this.X3No = reader["X3No"].ToString();
                    }
                }
            }

            if (this.TicketPortBox != 1)
                this.TicketPortBox = 2;

            if (this.InvPortBox != 1)
                this.InvPortBox = 2;
        }


        private DataTable GetTestData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("itname", typeof(String));
            dt.Columns.Add("qty", typeof(String));
            dt.Columns.Add("price", typeof(String));
            dt.Columns.Add("prs", typeof(String));
            dt.Columns.Add("mny", typeof(String));

            dt.Rows.Add("火砲藍的香水", "1", "70", "1", "70");
            dt.Rows.Add("兵丁頭飾", "2", "9", "1", "18");

            NPOS.TotMny = 88;
            NPOS.Cash = 100;
            NPOS.Change = 12;

            return dt;
        }
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

        #region 中一刀測試
        public void TryHalf()
        {
            if (this.InvPort.Length > 0)
            {
                MessageBox.Show("已設定發票機列印，無法使用中一刀列印功能!!!");
                return;
            }

            MessageBox.Show("程式開發中...!");
        }
        #endregion
        #region 中一刀列印
        public void doHalf(string halfPort, DataTable dt, decimal totmny, string page, bool islast)
        {
            if (this.InvPort.Length > 0)
            {
                MessageBox.Show("已設定發票機列印，無法使用中一刀列印功能!!!");
                return;
            }

            if (this.HalfPort.Length == 0)
                return;

            this.dtHalf = dt.Copy();

            if (this.HalfPort.ToUpper().StartsWith("COM"))
                HalfCOMPort(false, halfPort, totmny, page, islast);
            else
                HalfPrint(false, halfPort, totmny, page, islast);

            if (dtHalf == null)
                return;

            dtHalf.Clear();
            dtHalf = null;
        }
        #endregion
        #region 中一刀-驅動
        private void HalfPrint(bool testPrint, string halfPort, decimal totmny, string page, bool islast)
        {
            try
            {
                var path = Common.reportaddress + @"Report\百貨中一刀發票_自定一.frx";
                if (System.IO.File.Exists(path) == false)
                {
                    path = Common.reportaddress + @"Report\百貨中一刀發票.frx";
                }

                if (System.IO.File.Exists(path) == false)
                {
                    MessageBox.Show("找不到報表『百貨中一刀發票.frx』，無法列印!");
                    return;
                }

                using (var fs = new JBS.FReport())
                {
                    var dnum = new string[] { "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖" };
                    var list = totmny.ToString("f0").AsEnumerable().Reverse();
                    fs.dy.Add("個", dnum[list.ElementAtOrDefault(0).ToInteger()]);
                    fs.dy.Add("十", dnum[list.ElementAtOrDefault(1).ToInteger()]);
                    fs.dy.Add("百", dnum[list.ElementAtOrDefault(2).ToInteger()]);
                    fs.dy.Add("仟", dnum[list.ElementAtOrDefault(3).ToInteger()]);
                    fs.dy.Add("萬", dnum[list.ElementAtOrDefault(4).ToInteger()]);
                    fs.dy.Add("拾萬", dnum[list.ElementAtOrDefault(5).ToInteger()]);
                    fs.dy.Add("百萬", dnum[list.ElementAtOrDefault(6).ToInteger()]);
                    fs.dy.Add("仟萬", dnum[list.ElementAtOrDefault(7).ToInteger()]);
                    fs.dy.Add("億", dnum[list.ElementAtOrDefault(8).ToInteger()]);

                    fs.dy.Add("page", page);
                    fs.dy.Add("next", islast ? "(以下空白)" : "*** 續下頁 ***");
                    fs.dy.Add("end", islast);

                    fs.ShowDialog = false;
                    fs.Printer = halfPort;
                    fs.OutReport(RptMode.Print, dtHalf, "Table1", path);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 中一刀-COMPORT
        private void HalfCOMPort(bool testPrint, string halfPort, decimal totmny, string page, bool islast)
        {
            MessageBox.Show("中一刀發票列印(報表套表)，不支援COM介面列印!!!");
            return;
        }
        #endregion


        #region 單據機測試
        public void TryTicket()
        {
            if (TicketPort.Trim().Length == 0)
            {
                MessageBox.Show("未設定出單機名稱(或是COM Port), 無法列印!");
                return;
            }

            this.dtTicket = this.GetTestData();
            NPOS.CurrentMachine = this.machine;

            if (this.TicketPort.ToUpper().StartsWith("COM"))
                TicketCOMPort(true);
            else
                TicketPrint(true);

            if (dtTicket == null)
                return;

            dtTicket.Clear();
            dtTicket = null;
        }
        #endregion
        #region 單據機列印
        public void doTicket(DataTable dt)
        {
            if (this.TicketPort.Length == 0)
                return;

            this.dtTicket = dt.Copy();

            if (this.TicketPort.ToUpper().StartsWith("COM"))
                TicketCOMPort(false);
            else
                TicketPrint(false);

            if (dtTicket == null)
                return;

            dtTicket.Clear();
            dtTicket = null;
        }
        #endregion
        #region 單據機-驅動
        private void TicketPrint(bool testPrint)
        {
            try
            {
                //附屬開錢櫃
                if (this.TicketPortBox == 1)
                {
                    USB.Write(this.TicketPort, 指令集.開錢櫃);
                }

                using (PrintDialog pdialog = new PrintDialog())
                using (PrintDocument pdocument = new PrintDocument())
                {
                    PrintController pController = new StandardPrintController();
                    pdocument.PrintController = pController;

                    Margins margins = new Margins(0, 0, 5, 5);
                    pdocument.DefaultPageSettings.Margins = margins;

                    pdialog.Document = pdocument;
                    pdialog.PrinterSettings.PrinterName = this.TicketPort;

                    if (testPrint)
                    {
                        pdocument.PrintPage -= new PrintPageEventHandler(Test_Ticket_PrintPage);
                        pdocument.PrintPage -= new PrintPageEventHandler(Ticket_PrintPage);

                        pdocument.PrintPage += new PrintPageEventHandler(Test_Ticket_PrintPage);
                    }
                    else
                    {
                        pdocument.PrintPage -= new PrintPageEventHandler(Test_Ticket_PrintPage);
                        pdocument.PrintPage -= new PrintPageEventHandler(Ticket_PrintPage);

                        pdocument.PrintPage += new PrintPageEventHandler(Ticket_PrintPage);
                    }

                    pdocument.Print();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Test_Ticket_PrintPage(object sender, PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            int z = 15;
            Font font = new Font("細明體", 10f, FontStyle.Bold);

            //表頭  
            e.Graphics.DrawString(this.InvT1, font, Brushes.Black, x, y);
            e.Graphics.DrawString(this.InvT2, font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(this.InvT3, font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(this.InvT4, font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(this.InvT5, font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString("統一編號:" + NPOS.CuUno, font, Brushes.Black, x, y += z);

            e.Graphics.DrawString(AddRightLength("名稱", 20) + AddLeftLength("數量", 7) + AddLeftLength("mny", 7), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(RepeatLength('=', 34), font, Brushes.Black, x, y += z);

            //明細
            for (int i = 0; i < dtTicket.Rows.Count; i++)
            {
                var itname = dtTicket.Rows[i]["itname"].ToString().Trim().GetUTF8(24);
                var qty = dtTicket.Rows[i]["qty"].ToDecimal().ToString("f0");
                var mny = dtTicket.Rows[i]["mny"].ToDecimal().ToString("f0");

                e.Graphics.DrawString(AddRightLength(itname, 20) + AddLeftLength(qty, 7) + AddLeftLength(mny, 7), font, Brushes.Black, x, y += z);
            }

            e.Graphics.DrawString(RepeatLength('=', 34), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString("合計" + AddLeftLength("88", 10), font, Brushes.Black, x, y += z);

            e.Graphics.DrawString(".", new Font("標楷體", 1f), Brushes.Black, x, y += z);
            e.Graphics.DrawString("收現" + AddLeftLength("100", 10), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString("找零" + AddLeftLength("12", 10), font, Brushes.Black, x, y += z);

            e.Graphics.DrawString(".", new Font("標楷體", 1f), Brushes.Black, x, y += z);
            e.Graphics.DrawString(".", new Font("標楷體", 1f), Brushes.Black, x, y += z);

            e.HasMorePages = false;
        }
        private void Ticket_PrintPage(object sender, PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            int z = 15;
            Font font = new Font("細明體", 10f, FontStyle.Bold);

            //表頭 
            e.Graphics.DrawString(this.InvT1, font, Brushes.Black, x, y);
            e.Graphics.DrawString(this.InvT2, font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(this.InvT3, font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(this.InvT4, font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(this.InvT5, font, Brushes.Black, x, y += z);
            e.Graphics.DrawString("憑證:" + NPOS.SaNo, font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString("統一編號:" + NPOS.CuUno, font, Brushes.Black, x, y += z);

            e.Graphics.DrawString(AddRightLength("名稱", 20) + AddLeftLength("數量", 7) + AddLeftLength("mny", 7), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(RepeatLength('=', 34), font, Brushes.Black, x, y += z);

            //明細
            for (int i = 0; i < dtTicket.Rows.Count; i++)
            {
                var itname = dtTicket.Rows[i]["itname"].ToString().Trim().GetUTF8(24);
                var qty = dtTicket.Rows[i]["qty"].ToDecimal().ToString("f0");
                var mny = dtTicket.Rows[i]["mny"].ToDecimal().ToString("f0");

                e.Graphics.DrawString(AddRightLength(itname, 20) + AddLeftLength(qty, 7) + AddLeftLength(mny, 7), font, Brushes.Black, x, y += z);
            }

            e.Graphics.DrawString(RepeatLength('=', 34), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString("合計" + AddLeftLength(NPOS.TotMny.ToString("f0"), 10), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString(".", new Font("標楷體", 1f), Brushes.Black, x, y += z);

            e.Graphics.DrawString("信用卡" + AddLeftLength(NPOS.CardMny.ToString("f0"), 8), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString("收現" + AddLeftLength(NPOS.Cash.ToString("f0"), 10), font, Brushes.Black, x, y += z);
            e.Graphics.DrawString("找零" + AddLeftLength(NPOS.Change.ToString("f0"), 10), font, Brushes.Black, x, y += z);

            e.Graphics.DrawString(".", new Font("標楷體", 1f), Brushes.Black, x, y += z);
            e.Graphics.DrawString(".", new Font("標楷體", 1f), Brushes.Black, x, y += z);

            e.HasMorePages = false;
        }
        #endregion
        #region 單據機-COMPORT
        private void TicketCOMPort(bool testPrint)
        {
            MessageBox.Show("程式開發中...，出單機請使用USB介面列印。");
            return;
        }
        #endregion


        #region 發票機測試
        public void TryInvNo()
        {
            if (InvPort.Trim().Length == 0)
            {
                MessageBox.Show("未設定發票機名稱(或是COM Port), 無法列印!");
                return;
            }

            this.dtSaleD = this.GetTestData();
            NPOS.CurrentMachine = this.machine;

            if (this.InvPort.ToUpper().StartsWith("COM"))
            {
                InvCOMPort();
            }
            else
            {
                InvPrint();
            }

            if (dtSaleD == null)
                return;

            dtSaleD.Clear();
            dtSaleD = null;
        }
        #endregion
        #region 發票機列印
        public void doInv(DataTable dt)
        {
            if (InvPort.Trim().Length == 0)
            {
                MessageBox.Show("未設定發票機名稱(或是COM Port), 無法列印!");
                return;
            }

            this.dtSaleD = dt.Copy();

            if (this.InvPort.ToUpper().StartsWith("COM"))
            {
                InvCOMPort();
            }
            else
            {
                InvPrint();
            }

            if (dtSaleD == null)
                return;

            dtSaleD.Clear();
            dtSaleD = null;
        }
        #endregion
        #region 發票機-驅動
        private void InvPrint()
        {
            try
            {
                //初始化發票機，設定可印中文字
                USB.Write(this.InvPort, 指令集.初始化);
                USB.Write(this.InvPort, 指令集.可印中文字);

                if (this.InvPortBox == 1)
                {
                    USB.Write(this.InvPort, 指令集.開錢櫃);
                }

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
                    USB.Write(this.InvPort, 指令集.換頁切斷);
                    NSale.PutInNewInvNo(i);
                }
            }

            //最後一頁印表尾前補空白行
            if (dtSaleD.Rows.Count != perpage)
            {
                var c = perpage - (dtSaleD.Rows.Count % perpage);
                for (int i = 0; i < c; i++)
                {
                    USB.Write(this.InvPort, 指令集.跳行);
                }
            }

            //最後一頁印表尾
            InvPrint_Footer();
            USB.Write(this.InvPort, 指令集.換頁切斷);

            NSale.PutInNewInvNo(page);
        }
        private void InvPrint_Header(int p)
        {
            //印表頭
            if (InvT1.Length > 0) USB.WriteLine(this.InvPort, this.InvT1);
            if (InvT2.Length > 0) USB.WriteLine(this.InvPort, this.InvT2);
            if (InvT3.Length > 0) USB.WriteLine(this.InvPort, this.InvT3);
            if (InvT4.Length > 0) USB.WriteLine(this.InvPort, this.InvT4);
            if (InvT5.Length > 0) USB.WriteLine(this.InvPort, this.InvT5);

            //第六行時間，第七行客戶統編
            USB.WriteLine(this.InvPort, "憑證:" + NPOS.SaNo + " 頁:" + p);
            USB.WriteLine(this.InvPort, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            USB.WriteLine(this.InvPort, "統一編號:" + NPOS.CuUno);
        }
        private void InvPrint_Detail(int p)
        {
            int max = this.X5No == "4" ? 32 : 24;
            int qlen = 5;
            int plen = 6;
            USB.WriteLine(this.InvPort, AddRightLength("品名規格", max - (qlen + plen)) + AddLeftLength("數量", qlen) + AddLeftLength("金額", plen));
            USB.WriteLine(this.InvPort, RepeatLength('=', max));

            var start = (p - 1) * perpage;
            for (int i = 0; i < perpage; i++)
            {
                if (start + (i + 1) > dtSaleD.Rows.Count)
                    break;

                var item = dtSaleD.Rows[start + i]["itname"].ToString().Trim().GetUTF8(max - (qlen + plen));
                item = AddRightLength(item.Trim(), max - (qlen + plen));
                var qty = dtSaleD.Rows[start + i]["qty"].ToDecimal();
                var prs = dtSaleD.Rows[start + i]["prs"].ToDecimal();
                var price = dtSaleD.Rows[start + i]["price"].ToDecimal();
                var mny = (qty * price * prs).ToDecimal("f0");

                USB.WriteLine(this.InvPort, item + AddLeftLength(qty.ToString("f0"), qlen) + AddLeftLength(mny.ToString("f0"), plen));
            }
        }
        private void InvPrint_Footer()
        {
            int max = this.X5No == "4" ? 32 : 24;
            USB.WriteLine(this.InvPort, RepeatLength('=', max));

            USB.WriteLine(this.InvPort, "  合計" + AddLeftLength(NPOS.TotMny.ToString("f0"), max - 8));
            USB.Write(this.InvPort, 指令集.跳行);

            if (NPOS.CardMny > 0)
            {
                USB.WriteLine(this.InvPort, "信用卡" + AddLeftLength(NPOS.CardMny.ToString("f0"), max - 8));
            }

            USB.WriteLine(this.InvPort, "  收現" + AddLeftLength(NPOS.Cash.ToString("f0"), max - 8));
            USB.WriteLine(this.InvPort, "  找零" + AddLeftLength(NPOS.Change.ToString("f0"), max - 8));
        }
        #endregion
        #region 發票機-COMPORT
        private void InvCOMPort()
        {
            try
            {
                using (var jComPort = new ComPort(this.InvPort))
                {
                    jComPort.Ready();
                    jComPort.Write(指令集.初始化);
                    //jComPort.Write(指令集.可印中文字);

                    if (this.InvPortBox == 1)
                    {
                        jComPort.Write(指令集.開錢櫃);
                    }

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
                    NSale.PutInNewInvNo(i);
                }
            }

            //最後一頁印表尾前補空白行
            if (dtSaleD.Rows.Count != perpage)
            {
                var c = perpage - (dtSaleD.Rows.Count % perpage);
                for (int i = 0; i < c; i++)
                {
                    jComPort.Write(指令集.跳行);
                }
            }

            //最後一頁印表尾
            InvCOMPort_Footer(jComPort);
            jComPort.Write(指令集.換頁切斷);

            NSale.PutInNewInvNo(page);
        }
        private void InvCOMPort_Header(ComPort jComPort, int p)
        {
            //印表頭
            if (InvT1.Length > 0) jComPort.WriteLine(this.InvT1);
            if (InvT2.Length > 0) jComPort.WriteLine(this.InvT2);
            if (InvT3.Length > 0) jComPort.WriteLine(this.InvT3);
            if (InvT4.Length > 0) jComPort.WriteLine(this.InvT4);
            if (InvT5.Length > 0) jComPort.WriteLine(this.InvT5);

            //第六行時間，第七行客戶統編
            jComPort.WriteLine("憑證:" + NPOS.SaNo + " 頁:" + p);
            jComPort.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
            jComPort.WriteLine("統一編號:" + NPOS.CuUno);
        }
        private void InvCOMPort_Detail(ComPort jComPort, int p)
        {
            int max = this.X5No == "4" ? 32 : 24;
            int qlen = 5;
            int plen = 6;
            jComPort.WriteLine(AddRightLength("品名規格", max - (qlen + plen)) + AddLeftLength("數量", qlen) + AddLeftLength("金額", plen));
            jComPort.WriteLine(RepeatLength('=', max));

            var start = (p - 1) * perpage;
            for (int i = 0; i < perpage; i++)
            {
                if (start + (i + 1) > dtSaleD.Rows.Count)
                    break;

                var item = dtSaleD.Rows[start + i]["itname"].ToString().Trim().GetUTF8(max - (qlen + plen));
                item = AddRightLength(item.Trim(), max - (qlen + plen));
                var qty = dtSaleD.Rows[start + i]["qty"].ToDecimal();
                var prs = dtSaleD.Rows[start + i]["prs"].ToDecimal();
                var price = dtSaleD.Rows[start + i]["price"].ToDecimal();
                var mny = (qty * price * prs).ToDecimal("f0");

                jComPort.WriteLine(item + AddLeftLength(qty.ToString("f0"), qlen) + AddLeftLength(mny.ToString("f0"), plen));
            }
        }
        private void InvCOMPort_Footer(ComPort jComPort)
        {
            int max = this.X5No == "4" ? 32 : 24;
            jComPort.WriteLine(RepeatLength('=', max));

            jComPort.WriteLine("  合計" + AddLeftLength(NPOS.TotMny.ToString("f0"), max - 8));
            jComPort.Write(指令集.跳行);

            if (NPOS.CardMny > 0)
            {
                jComPort.WriteLine("信用卡" + AddLeftLength(NPOS.CardMny.ToString("f0"), max - 8));
            }

            jComPort.WriteLine("  收現" + AddLeftLength(NPOS.Cash.ToString("f0"), max - 8));
            jComPort.WriteLine("  找零" + AddLeftLength(NPOS.Change.ToString("f0"), max - 8));
        }
        #endregion


        #region 錢櫃測試
        public void TryMoneyBox()
        {
            if (this.MoneyPort.Length == 0)
            {
                MessageBox.Show("未設定(COM Port), 無法動作!");
                return;
            }

            OpenMoneyBox(this.MoneyPort);
        }
        #endregion
        #region 錢櫃開箱
        public bool doMoneyBox()
        {
            if (this.MoneyPort.Length > 0)
            {
                OpenMoneyBox(this.MoneyPort);
                return true;
            }
            else if (this.TicketPortBox == 1)
            {
                OpenMoneyBox(this.TicketPort);
                return true;
            }
            else if (this.InvPortBox == 1)
            {
                OpenMoneyBox(this.InvPort);
                return true;
            }
            else
            {
                MessageBox.Show("未設定(COM Port), 無法動作!");
                return false;
            }
        }
        #endregion
        #region 向不同的介面開箱
        private void OpenMoneyBox(string port)
        {
            if (port.ToUpper().StartsWith("COM"))
            {
                ComPort.WriteOne(port, 指令集.開錢櫃);
            }
            else
            {
                USB.Write(port, 指令集.開錢櫃);
            }
        }
        #endregion


        #region 客顯測試
        public void TryVideo()
        {
            if (this.VideoPort.Length == 0)
            {
                MessageBox.Show("未設定(COM Port), 無法動作!");
                return;
            }

            try
            {
                using (var jDSP870A = new DSP870A(this.VideoPort))
                {
                    jDSP870A.Show("一、我是第一行");
                    jDSP870A.Show("二、我是第二行");
                    jDSP870A.Show("三、我是第三行");
                    jDSP870A.Show("四、螢幕客顯測試完成");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        #endregion
        #region 客顯顯示
        public void doVideoShowItem(string itname, decimal price, int count)
        {
            if (this.VideoPort.Length == 0)
                return;

            using (var jDSP870A = new DSP870A(this.VideoPort))
            {
                if (count == 1)
                    jDSP870A.Clear();

                var msg = AddRightLength(itname, 16) + AddLeftLength("$" + price.ToString("f0"), 10) + "元";
                jDSP870A.Show(msg);
            }
        }
        public void doVideoShowResult(decimal totmny, decimal realcash, decimal change)
        {
            if (this.VideoPort.Length == 0)
                return;

            using (var jDSP870A = new DSP870A(this.VideoPort))
            {
                jDSP870A.Show("收現:" + AddLeftLength(totmny.ToString("f0"), 10) + "元");
                jDSP870A.Show("收現:" + AddLeftLength(realcash.ToString("f0"), 10) + "元");
                jDSP870A.Show("找零:" + AddLeftLength(change.ToString("f0"), 10) + "元");
            }
        }
        #endregion
        #region 交班表測試
        public void TryOffLine()
        {

        }
        #endregion

        #region 百貨出貨報表
        internal void doPaper(ref Machine jMachine, DataTable dtPaper)
        { 
            try
            {  
                var path = Common.reportaddress + @"Report\百貨出貨報表_自定一.frx";
                if (System.IO.File.Exists(path) == false)
                {
                    path = Common.reportaddress + @"Report\百貨出貨報表.frx";
                }

                if (System.IO.File.Exists(path) == false)
                {
                    MessageBox.Show("找不到報表『百貨出貨報表.frx』，無法列印!");
                    return;
                }

                using (var fs = new JBS.FReport())
                { 
                    fs.ShowDialog = false;
                    fs.Printer = jMachine.PaperPort;
                    fs.OutReport(RptMode.PreView, dtPaper, "Table1", path);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dtPaper.Clear();
                dtPaper = null;
            }
        }
        #endregion

        #region 刷卡機測試
        public void Trycard()
        {
            if (this.cardPort.Length == 0)
            {
                MessageBox.Show("未設定(COM Port), 無法動作!");
                return;
            }
            try
            {
                #region 把comport寫入ECR.DAT
                int i = 0;
                StreamReader sr = new StreamReader(@"ECR.DAT");
                while (!sr.EndOfStream)
                {               // 每次讀取一行，直到檔尾             
                    read[i] = sr.ReadLine();// 讀取文字到 read 陣列
                    i++;
                }
                sr.Close();
                StreamWriter write = new StreamWriter(@"ECR.DAT");

                write.WriteLine(this.cardPort);//輸入1or2即可
                for (int j = 1; j < i; j++)
                {
                    write.WriteLine(read[j]);
                }
                write.Close();
               // MessageBox.Show("修改成功", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                #endregion

                if (File.Exists(dir + "\\out.txt")) //刪除out.txt
                    File.Delete(dir + "\\out.txt");
                //寫入刷卡金額
                string code = "0100000000000000000000000000000000000000010121050601100016100000000000000000000000000000000000000000000000000000000000000000000000000010000000000";  // in.txt格式
                using (StreamWriter sw = new StreamWriter(dir + "\\in.txt"))   //小寫TXT     
                sw.Write(code);
                try
                {
                    Process notePad = new Process();
                    // FileName 是要執行的檔案
                    if (File.Exists(Common.reportaddress + "\\ecr\\e.bat"))
                    {
                        notePad.StartInfo.FileName = Common.reportaddress + "\\ecr\\e.bat";
                        notePad.Start();
                        Application.DoEvents();  
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                //讀取OUT信用卡卡號 
                exist = false;
                readcard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void readcard()
        {
           
            while (!exist)
            {
                try
                {
                    if (File.Exists(dir + "\\out.txt"))
                    {
                        StreamReader sr = new StreamReader(dir + "\\out.txt");
                        string line = string.Empty;
                        while ((line = sr.ReadLine()) != null)
                        {
                            MessageBox.Show("信用卡卡號:" + line.ToString().Substring(10, 16));
                            exist = !exist;
                        }
                        sr.Close();
                        //Console.ReadLine();
                    }
                }
                catch (Exception )
                {
                    readcard();
                }
                
            }
        }
        #endregion
    }

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




