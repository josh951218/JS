using System;
using System.Globalization;

namespace S_61.Basic
{
    class Date
    {
        public static string GetDateTime(int i, bool showLine = false)
        {
            TaiwanCalendar tw = new TaiwanCalendar();
            switch (i)
            {
                case 1:
                    if (showLine)
                        return tw.GetYear(DateTime.Now) + "/" + DateTime.Now.ToString("MM/dd");
                    else
                        return tw.GetYear(DateTime.Now) + DateTime.Now.ToString("MMdd");
                case 2:
                    if (showLine)
                        return tw.GetYear(DateTime.Now) + 1911 + "/" + DateTime.Now.ToString("MM/dd");
                    else
                        return tw.GetYear(DateTime.Now) + 1911 + DateTime.Now.ToString("MMdd");
                default:
                    return "";
            }
        }

        public static string ChangeDateForSN(string d)
        {
            string strDate = "";
            if (d.Trim().Length == 7)
            {
                switch (Common.Sys_NoAdd)
                {
                    case 1:
                        strDate = d;
                        break;
                    case 2:
                        strDate = d.Substring(0, 5) + "00";
                        break;
                    case 3:
                        strDate = (Int32.Parse(d.Substring(0, 3)) + 1911) + d.Substring(3);
                        break;
                    case 4:
                        strDate = (Int32.Parse(d.Substring(0, 3)) + 1911) + d.Substring(3, 2) + "00";
                        break;
                }
                return strDate;
            }
            else
            {
                switch (Common.Sys_NoAdd)
                {
                    case 1:
                        strDate = (Int32.Parse(d.Substring(0, 4)) - 1911) + d.Substring(4);
                        break;
                    case 2:
                        strDate = (Int32.Parse(d.Substring(0, 4)) - 1911) + d.Substring(4, 2) + "00";
                        break;
                    case 3:
                        strDate = d;
                        break;
                    case 4:
                        strDate = d.Substring(0, 6) + "00";
                        break;
                }
                return strDate;
            }
        }

        public static string AddLine(string d)
        {
            if (d.Trim().Length == 7)
            {
                d = d.Substring(0, 3) + "/" + d.Substring(3, 2) + "/" + d.Substring(5);
                return d;
            }
            else if (d.Trim().Length == 8)
            {
                d = d.Substring(0, 4) + "/" + d.Substring(4, 2) + "/" + d.Substring(6);
                return d;
            }
            else
            {
                return Common.User_DateTime == 1 ? "   /  /  " : "    /  /  ";
            }
        }

        public static string RemoveLine(string d)
        {
            return d.Replace("/", "");
        }

        public static string ToTWDate(string d)
        {
            int Year = 0;
            if (d.Trim().Length == 8)
            {
                if (d.Trim() == "") return d;
                int.TryParse(d.Substring(0, 4), out Year);
                Year -= 1911;
                return Year.ToString().PadLeft(3,'0') + d.Substring(4);
            }
            else return d;
        }

        public static string ToUSDate(string d)
        {
            int Year = 0;
            if (d.Trim().Length == 7)
            {
                if (d.Trim() == "") return d;
                int.TryParse(d.Substring(0, 3), out Year);
                Year += 1911;
                return Year + d.Substring(3);
            }
            else return d;
        }

        public static string GetToday()
        {
            if (Common.User_DateTime == 1)
            {
                return Date.GetDateTime(1, true);
            }
            else
            {
                return Date.GetDateTime(2, true);
            }
        }
    }
}
