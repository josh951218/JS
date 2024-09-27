using System;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;
using JBS.JM;
using S_61.subMenuFm_2;
using System.Globalization;

namespace JBS
{
    public class InvoPrint
    {
        
        delegate void myDelegate();
        public string InvPort { get; set; }
        string machine = "";

        DataTable dtTemp = new DataTable();
        DataTable machinertable = new DataTable();
        RichTextBox TextBox1 = new RichTextBox();
        RichTextBox Detail1 = new RichTextBox();
        RichTextBox Detail2 = new RichTextBox();
        RichTextBox TextBox2 = new RichTextBox();

        SerialPort sp = new SerialPort();
        //string 初始化 = ((char)27).ToString() + "@";
        string LeftOnly = ((char)27).ToString() + ((char)99).ToString() + ((char)48).ToString() + ((char)2).ToString();
        string RightOnly = ((char)27).ToString() + ((char)99).ToString() + ((char)48).ToString() + ((char)1).ToString();
        string LeftRedStop = ((char)27).ToString() + ((char)99).ToString() + ((char)52).ToString() + ((char)2).ToString();
        string MoveToFront = ((char)13).ToString();
        string 換頁切斷 = ((char)12).ToString();
        string 跳行 = "";
        public static readonly byte[] 斷 = new byte[] { 12 };
        public static readonly byte[] 初始化 = new byte[] { 27, 64 };
        public static readonly byte[] 可印中文字 = new byte[] { 28, 38 };
        public static readonly byte[] 開錢櫃 = new byte[] { 27, 112, 0, 50, 250 };

        int Recorded = 0;
        int PerPage = 0;//明細幾筆
        int PageCode = 0;//頁碼

        //呼叫打印發票時，必要傳入參數
        public string X5No = "";
        public string X5Name = "";
        public DataTable dt = new DataTable();//列印明細
        public string Machine = "";           //機台號碼
        public string InvNoS = "";            //起始發票號碼
        public string InvNoE = "";            //終止發票號碼
        public decimal Cash = 0;              //現金
        public decimal CardMny =0;            //刷卡金額
        public decimal GetPrvAcc = 0;         //取用預收
        public decimal Change = 0;            //找零 
        public decimal KiTax1 = 0;            //未稅
        public decimal KiTax2 = 0;            //免稅
        public decimal Tax = 0;               //稅額
        public string CuUnno = "";            //客戶統編
        public bool IsTestPrint = false;      //是否為測試列印
        public string iTitle = "";
        public string iStore = "";
        public string iUnno = "";
        public string iTaxNo = "";
        public string iTel = "";
        public string iAddress = "";
        public string iMemo1 = "";
        public string iMemo2 = "";


        string itno = "";
        string itname = "";
        decimal qty = 0;
        decimal price = 0;
        decimal taxprice = 0;
        decimal mny = 0;
        decimal total = 0;
        decimal kitax = 0;

        //一般變數
        string tempStr;
        string format;
        int len = 0;
        int index = 0;
        bool IsLast = true;
        //string BBB = "";

        void Nothing()
        {
            if (IsLast) IsLast = true;
        }

        public InvoPrint()
        {
            sp.BaudRate = 9600;
            sp.Parity = Parity.None;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Encoding = Encoding.GetEncoding(950);
            LoadDB();
            takemachine();
            GetUserSeting();
        }

        void takemachine() 
        {
            Machine = Common.sc_MachineSet;
        }

        void LoadDB()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from invoiceset where InNo=N'N1'";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        dtTemp.Clear();
                        da.Fill(dtTemp);
                    }
                }
                if (dtTemp.Rows.Count > 0)
                {
                    TextBox1.Clear();
                    Detail1.Clear();
                    Detail2.Clear();
                    TextBox2.Clear();
                    TextBox1.Text = dtTemp.Rows[0]["InHead"].ToString();//發票抬頭
                    Detail1.Text = dtTemp.Rows[0]["InDital1"].ToString();//銷售數量為1，列印方式
                    Detail2.Text = dtTemp.Rows[0]["InDital21"].ToString();//銷售數量>1，列印方式
                    TextBox2.Text = dtTemp.Rows[0]["InFoot"].ToString();//發票註腳
                    PerPage = (int)dtTemp.Rows[0]["inditalnum"].ToDecimal();//明細行數
                    int j = (int)dtTemp.Rows[0]["jump"].ToDecimal();//抬頭跳行
                    跳行 = ((char)27).ToString() + "d" + ((char)j).ToString();
                }
                else
                {
                    MessageBox.Show("發票設定檔錯誤！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void GetUserSeting()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("scname",Common.User_Name);
                cmd.CommandText = "select * from scrit where scname=@scname";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        iTitle = reader["iTitle"].ToString().Trim();
                        iStore = reader["iStore"].ToString().Trim();
                        iUnno = reader["iUnno"].ToString().Trim();
                        iTaxNo = reader["iTaxNo"].ToString().Trim();
                        iTel = reader["iTel"].ToString().Trim();
                        iAddress = reader["iAddress"].ToString().Trim();
                        iMemo1 = reader["iMemo1"].ToString().Trim();
                        iMemo2 = reader["iMemo2"].ToString().Trim();
                    }
                }
            }
        }

        public void doPrint()
        {
            try
            {
                if (Common.User_ScInvDev == 1) sp.PortName = "COM1";
                else if (Common.User_ScInvDev == 2) sp.PortName = "COM2";
                if (sp.IsOpen) sp.Close();
                sp.Open();
                sp.Write(初始化, 0, 初始化.Length);
                sp.Write(可印中文字, 0, 可印中文字.Length);
                //sp.Write(LeftOnly);
                //sp.Write(RightOnly);
                //sp.Write(LeftRedStop);
                sp.Write(開錢櫃, 0, 開錢櫃.Length);
                Recorded = 0;
                total = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //抬頭
                    if (Recorded % PerPage == 0)
                    {
                        PrintTitle(dt.Rows[i]);
                    }
                    //明細
                    PrintDetail(dt.Rows[i]);
                    //註腳
                    if (Recorded >= PerPage)
                    {
                        //換頁，並判斷是否為最後一筆，最後一筆的話，接著印註腳，存發票號碼與金額
                        //否則，換頁繼續印明細，存發票號碼 金額0(配合發票系統)
                        if (i == dt.Rows.Count - 1)
                        {
                            PrintFloot();
                            sp.Write(斷, 0, 斷.Length);
                            //存當前號，使用者參數做累加
                            IsLast = true;
                            break;
                        }
                        else
                        {
                            sp.Write(斷, 0, 斷.Length);
                            sp.Write(MoveToFront);
                            //存當前號，使用者參數做累加
                            IsLast = false;
                        }
                        Recorded = 0;
                    }
                    //明細不須換頁就印完了，印註腳，存發票號碼與金額
                    if (i == (dt.Rows.Count - 1))
                    {
                        PrintFloot();
                        sp.Write(斷, 0, 斷.Length);
                        //存當前號，使用者參數做累加
                        IsLast = true;

                        break;
                    }
                }
                sp.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void PrintTitle(DataRow row)
        {
            PageCode++;
            CuUnno = row["invtaxno"].ToString();
            sp.Write(跳行);
            for (int i = 0; i < TextBox1.Lines.Length; i++)
            {
                tempStr = TextBox1.Lines[i].Trim();
                if (tempStr.Length == 0)
                {
                    sp.WriteLine(sp.NewLine);
                }
                tempStr = ckf(tempStr);
                sp.WriteLine(tempStr);
                //BBB += tempStr;
                //BBB += Environment.NewLine;
            }
        }

        void PrintDetail(DataRow row)
        {
            itno = row["itno"].ToString();
            itname = row["itname"].ToString();
            qty = row["qty"].ToDecimal();
            price = row["price"].ToDecimal();
            taxprice = row["taxprice"].ToDecimal();
            kitax = row["kitax"].ToDecimal();


            string p = price.ToString("f0");
            string q = qty.ToString("f0");
            mny = p.ToDecimal() * q.ToDecimal();
            total += mny;



            if (qty == 1)
            {
                for (int i = 0; i < Detail1.Lines.Length; i++)
                {
                    tempStr = Detail1.Lines[i].Trim();
                    if (tempStr.Length == 0)
                    {
                        sp.WriteLine(sp.NewLine);
                    }
                    tempStr = CheckFunction(tempStr);

                    tempStr = kitax == 1 ? tempStr : tempStr.takeString(tempStr.Length);
                    sp.WriteLine(tempStr);
                    //BBB += tempStr;
                    //BBB += Environment.NewLine;
                }
                Recorded += (Detail1.Lines.Length);
            }
            else if (qty > 1)
            {
                for (int i = 0; i < Detail2.Lines.Length; i++)
                {
                    tempStr = Detail2.Lines[i].Trim();
                    if (tempStr.Length == 0)
                    {
                        sp.WriteLine(sp.NewLine);
                    }
                    tempStr = CheckFunction(tempStr);

                    tempStr = kitax == 1 ? tempStr : tempStr.takeString(tempStr.Length );
                    sp.WriteLine(tempStr);
                    //BBB += tempStr;
                    //BBB += Environment.NewLine;
                }
                Recorded += (Detail2.Lines.Length);
            }
        }

        void PrintFloot()
        {
            for (int i = 0; i < TextBox2.Lines.Length; i++)
            {
                tempStr = TextBox2.Lines[i].Trim();
                if (tempStr.Length == 0)
                {
                    sp.WriteLine(sp.NewLine);
                }
                tempStr = ckf(tempStr);
                sp.WriteLine(tempStr);
                //BBB += tempStr;
                //BBB += Environment.NewLine;
            }
            //using (StreamWriter ww = new StreamWriter(@"C:\\123.txt", false, Encoding.GetEncoding(950)))
            //{
            //    ww.Write(BBB);
            //}
        }

        string ckf(string line)
        {
            if (line.Contains("iTitle()"))
            {
                line = line.Replace("iTitle()", iTitle.Trim());
            }
            if (line.Contains("iStore()"))
            {
                line = line.Replace("iStore()", iStore.Trim());
            }
            if (line.Contains("iUnno()"))
            {
                line = line.Replace("iUnno()", iUnno.Trim());
            }
            if (line.Contains("iTaxNo()"))
            {
                line = line.Replace("iTaxNo()", iTaxNo.Trim());
            }
            if (line.Contains("iTel()"))
            {
                line = line.Replace("iTel()", iTel.Trim());
            }
            if (line.Contains("iAddress()"))
            {
                line = line.Replace("iAddress()", iAddress.Trim());
            }
            if (line.Contains("iMemo1()"))
            {
                line = line.Replace("iMemo1()", iMemo1.Trim());
            }
            if (line.Contains("iMemo2()"))
            {
                line = line.Replace("iMemo2()", iMemo2.Trim());
            }
            if (line.Contains("CuUnno()"))
            {
                line = line.Replace("CuUnno()", CuUnno.Trim());
            }



            if (line.Contains("Machine()"))
            {
                line = line.Replace("Machine()", Machine.Trim());
            }
            if (line.Contains("Page()"))
            {
                line = line.Replace("Page()", PageCode.ToString());
            }
            if (line.Contains("西元DateTime()"))
            {
                line = line.Replace("西元DateTime()", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
            }
            if (line.Contains("民國DateTime()"))
            {
                    var date = DateTime.Now;
                    TaiwanCalendar taiwanCalendar = new TaiwanCalendar();
                    line = line.Replace("民國DateTime()", string.Format("{0}{1}",
                                 taiwanCalendar.GetYear(date),
                                 DateTime.Now.ToString("MMdd  HH:mm:ss")
                                 ));
            }
            if (line.Contains("GetCount("))
            {
                format = "GetCount(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", dt.Rows.Count.ToString());
                line = line.Replace("GetCount(" + len + ")", format);
            }
            if (line.Contains("GetTotal("))
            {
                format = "GetTotal(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + total.ToString("f0"));
                line = line.Replace("GetTotal(" + len + ")", format);
            }
            if (line.Contains("Cash("))
            {
                format = "Cash(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + Cash.ToString("f0"));
                line = line.Replace("Cash(" + len + ")", format);
            }
            if (line.Contains("CardMny("))
            {
                format = "CardMny(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + CardMny.ToString("f0"));
                line = line.Replace("CardMny(" + len + ")", format);
            }
            if (line.Contains("Change("))
            {
                format = "Change(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + Change.ToString("f0"));
                line = line.Replace("Change(" + len + ")", format);
            }
            if (line.Contains("GetPrvAcc("))
            {
                format = "GetPrvAcc(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + GetPrvAcc.ToString("f0"));
                line = line.Replace("GetPrvAcc(" + len + ")", format);
            }
            if (line.Contains("KiTax1("))
            {
                format = "KiTax1(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + KiTax1.ToString("f0"));
                line = line.Replace("KiTax1(" + len + ")", format);
            }
            if (line.Contains("KiTax2("))
            {
                format = "KiTax2(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + KiTax2.ToString("f0"));
                line = line.Replace("KiTax2(" + len + ")", format);
            }
            if (line.Contains("Tax("))
            {
                format = "Tax(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + Tax.ToString("f0"));
                line = line.Replace("Tax(" + len + ")", format);
            }
        flag:
            if (line.Contains("Space("))
            {
                format = "Space(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "");
                line = line.Replace("Space(" + len + ")", format);
                if (line.Contains("Space(")) goto flag;
            }
            if (line.Contains("Line(二聯)"))
            {
                line = line.Replace("Line(二聯)", "------------------------");
            }
            if (line.Contains("Line(三聯)"))
            {
                line = line.Replace("Line(三聯)", "--------------------------------");
            }
            return line;
        }

        string CheckFunction(string line)
        {
            if (line.Contains("ItNo("))
            {
                format = "ItNo(";
                len = GetLen(format, line);
                int i = Encoding.GetEncoding(950).GetByteCount(itno);
                format = itno.PadRight(24, ' ');
                line = line.Replace("ItNo(" + len + ")", format.GetUTF8(len));
            }
            if (line.Contains("ItName("))
            {
                format = "ItName(";
                len = GetLen(format, line);
                int i = Encoding.GetEncoding(950).GetByteCount(itname);
                format = itname.PadRight(24, ' ');
                line = line.Replace("ItName(" + len + ")", format.GetUTF8(len));
            }
            if (line.Contains("TaxPrice("))
            {
                format = "TaxPrice(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + taxprice.ToString("f0"));
                line = line.Replace("TaxPrice(" + len + ")", format);
            }
            if (line.Contains("Price("))
            {
                format = "Price(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "$" + price.ToString("f0"));
                line = line.Replace("Price(" + len + ")", format);
            }
            if (line.Contains("Mny("))
            {
                format = "Mny(";
                len = GetLen(format, line);
                if (mny < 0)
                {
                    mny = (-1) * mny;
                    int ln = len - 1;
                    format = string.Format("{0," + ln + "}", "$-" + mny.ToString("f0"));
                }
                else
                {
                    format = string.Format("{0," + len + "}", "$" + mny.ToString("f0"));
                }
                line = line.Replace("Mny(" + len + ")", format);
            }
            if (line.Contains("Qty("))
            {
                format = "Qty(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", qty.ToString("f0"));
                line = line.Replace("Qty(" + len + ")", format);
            }
        flag:
            if (line.Contains("Space("))
            {
                format = "Space(";
                len = GetLen(format, line);
                format = string.Format("{0," + len + "}", "");
                line = line.Replace("Space(" + len + ")", format);
                if (line.Contains("Space(")) goto flag;
            }
            if (line.Contains("Line(二聯)"))
            {
                line = line.Replace("Line(二聯)", "------------------------");
            }
            if (line.Contains("Line(三聯)"))
            {
                line = line.Replace("Line(三聯)", "--------------------------------");
            }
            return line;
        }

        int GetLen(string s, string l)
        {
            index = l.IndexOf(s);
            l = new string(l.Skip(index + s.Length).ToArray());
            index = l.IndexOf(")");
            l = new string(l.Take(index).ToArray());
            len = 0;
            int.TryParse(l, out len);
            return len;
        }
        
    }
}
