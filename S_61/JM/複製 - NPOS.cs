using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.IO;
using System.Text;

namespace JBS.JM
{
    public class NPOS
    {
        //public static FrmPosSale FrmPosSale;
        //public static FrmShowChange FrmShowChange;
        public static SuperInput SuperInput;
        public static bool IsSuperAdministrator = false;
        public static string CurrentMachine = "";//目前機台號碼
        public static string CuUno = "";//消費者統一編號
        public static decimal TotMny = 0;//消費總額
        public static decimal Cash = 0;//發票列印現金(消費者支付現金)(找零前的現金)
        public static decimal CashMny = 0;//單據已收現金
        public static decimal Change = 0;//應找零錢
        public static decimal TaxMny = 0;//單據稅前金額
        public static decimal Tax = 0;//單據稅額
        public static decimal TaxMnyb = 0;//單據稅前金額(本幣)
        public static decimal Discount = 0;//單據折扣金額
        public static string CardNo = "";//信用卡卡號
        public static decimal CardMny = 0;//信用卡金額
        public static decimal Ticket = 0;//支票金額
        public static decimal GetPrvAcc = 0;//此次消費已取用儲值金
        public static decimal GetPrvPoint = 0;//此次消費已取用的紅利點
        public static decimal CollectMny = 0;//此次消費已收金額
        public static decimal AcctMny = 0;//此次消費未收金額 
        public static decimal ForFree = 0;//招待
        public static int RecordNo = 0;//此次銷費產品筆數

        public static string SaNo = "";//銷貨單號
        public static string SaDate = "";//銷貨日期民國
        public static string SaDate1 = "";//銷貨日期西元
        public static string CuNo = "";//客戶編號
        public static string CuName2 = "";//客戶名稱
        public static string CuName1 = "";//客戶簡稱
        public static string CuTel1 = "";//客戶電話一
        public static string CuPer1 = "";//客戶聯絡人一
        public static decimal CuDisc = 1M;//客戶折數
        public static decimal CuPoint = 0;//客戶現有紅利點
        public static decimal CuAdvamt = 0;//客戶現有儲值金
        public static string X3No = "";//客戶稅別
        public static string Rate = "";//稅率
        public static string X5No = "";//發票模式
        public static string X5Name = "";//發票模式
        public static string Xa1No = "TWD";
        public static string Xa1Name = "新臺幣";
        public static decimal Xa1Par = 1M;//台幣匯率
        public static string StNo = "";//使用者倉庫
        public static string StName = "";//使用者倉庫
        public static string EmNo = "";//登入員工
        public static string EmName = "";//登入員工
        public static string EmPass = "";//登入員工密碼
        public static string ShNo = "";//班別
        public static string ShName = "";//班別
        public static decimal MorningCash = 0M;//錢櫃零用金 

        public static string Online = "";//交班起始時間
        public static string OffLine = "";//交班終止時間

        //設定快速產品按鈕
        static internal void SetFast(Dictionary<string, ButtonFast> dy)
        {
            DataTable fast = new DataTable();

            using (var db = new xSQL())
            {
                var tsql = "Select * from posfastitem ";
                db.Fill(tsql, spc => spc.AddWithValue("x", "x"), ref fast);
            }

            for (int i = 0; i < fast.Rows.Count; i++)
            {
                var key = "btnIt" + fast.Rows[i]["pNo"].ToString().Trim();
                if (dy.ContainsKey(key))
                {
                    dy[key].ItNo = fast.Rows[i]["ItNo"].ToString().Trim();
                    dy[key].Text = fast.Rows[i]["pName"].ToString().Trim();
                }
            }
        }
        //設定快速類別按鈕
        static internal void SetKind(Dictionary<string, ButtonKind> dy)
        {
            DataTable kind = new DataTable();

            using (var db = new xSQL())
            {
                var tsql = "Select * from poskind ";
                db.Fill(tsql, spc => spc.AddWithValue("x", "x"), ref kind);
            }

            for (int i = 0; i < kind.Rows.Count; i++)
            {
                var key = "ki" + kind.Rows[i]["pNo"].ToString().Trim();
                if (dy.ContainsKey(key))
                {
                    dy[key].KiNo = kind.Rows[i]["kino"].ToString().Trim();
                    dy[key].Text = kind.Rows[i]["pName"].ToString().Trim();
                }
            }
        }
        //低於成本警告
        static internal void LowerCost(string itno, string itunit, decimal price)
        {
            if (Common.Sys_LowCost == 3)
                return;

            if (itno.Trim().Length == 0)
                return;

            using (var db = new JBS.xSQL())
            {
                var tsql = "Select * from item where itno = @itno ";

                db.ExecuteReader(tsql, spc => spc.AddWithValue("itno", itno), row =>
                {
                    if (row["itunitp"].ToString().Trim() == itunit)
                    {
                        if (Common.Sys_LowCost == 1 && row["itbuyprip"].ToDecimal() > price)
                        {
                            var msg = "此產品售價已低於進價，請確認!!!\n此產品包裝進價為:" + row["itbuyprip"].ToDecimal().ToString("f" + Common.MS);
                            using (var frm = new JBS.JM.Msg(msg))
                            {
                                frm.ShowDialog();
                            }
                        }

                        if (Common.Sys_LowCost == 2 && row["itcostp"].ToDecimal() > price)
                        {
                            var msg = "此產品售價已低於標準成本，請確認!!!\n此產品包裝標準成本為:" + row["itcostp"].ToDecimal().ToString("f" + Common.MS);
                            using (var frm = new JBS.JM.Msg(msg))
                            {
                                frm.ShowDialog();
                            }
                        }

                    }
                    else
                    {
                        if (Common.Sys_LowCost == 1 && row["itbuypri"].ToDecimal() > price)
                        {
                            var msg = "此產品售價已低於進價，請確認!!!\n此產品單位進價為:" + row["itbuypri"].ToDecimal().ToString("f" + Common.MS);
                            using (var frm = new JBS.JM.Msg(msg))
                            {
                                frm.ShowDialog();
                            }
                        }

                        if (Common.Sys_LowCost == 2 && row["itcost"].ToDecimal() > price)
                        {
                            var msg = "此產品售價已低於標準成本，請確認!!!\n此產品包裝標準成本為:" + row["itcost"].ToDecimal().ToString("f" + Common.MS);
                            using (var frm = new JBS.JM.Msg(msg))
                            {
                                frm.ShowDialog();
                            }
                        }
                    }
                });
            }
        }
        //安全存量or庫存零 
        static internal int IsLowStock(string itno, ref DataTable dtSaleD)
        {
            //庫存量不控管
            if (Common.Sys_LowStock != 1)
                return 1;

            if (itno.Trim().Length == 0)
                return 1;

            var itname = "";
            var itsafeqty = 0M;
            var stockOne = 0M;
            var stockAll = 0M;

            using (var db = new JBS.xSQL())
            {
                var tsql = "Select * from item where itno = @itno ";
                db.ExecuteReader(tsql, spc => spc.AddWithValue("itno", itno), row =>
                {
                    itname = row["itname"].ToString().Trim();
                    itsafeqty = row["itsafeqty"].ToDecimal();
                });

                tsql = "Select itqty from stock where itno = @itno and stno = @stno";
                stockOne = db.ExecuteScalar(tsql, spc =>
                {
                    spc.AddWithValue("itno", itno);
                    spc.AddWithValue("stno", NPOS.StNo);
                }).ToDecimal();

                tsql = "Select Sum(itqty) from stock where itno = @itno";
                stockAll = db.ExecuteScalar(tsql, spc => spc.AddWithValue("itno", itno)).ToDecimal();
            }

            var qty = dtSaleD.AsEnumerable()
                .Where(r => r["itno"].ToString().Trim() == itno)
                .Sum(r => r["qty"].ToDecimal() * r["itpkgqty"].ToDecimal());

            //總倉or分倉
            var tQty = (Common.Sys_LowStockMode == 1) ? stockAll : stockOne;

            //安全存量
            if (Common.Sys_LowStkSlt == 1)
            {
                if ((tQty - qty) < itsafeqty)
                {
                    Msg.Show("產品編號：" + itno + "\n品名規格：" + itname + "\n出貨數量已超出安全數量，請確認...");
                    return 0;
                }
            }
            //庫存量
            else
            {
                if ((tQty - qty) < 0)
                {
                    Msg.Show("產品編號：" + itno + "\n品名規格：" + itname + "\n出貨數量已大於現有庫存量，請確認...");
                    return 0;
                }
            }

            return 1;
        }
        //取得產品資料
        static internal void InputItem(string itno, Action<SqlDataReader> action)
        {
            var result = false;

            using (var db = new JBS.xSQL())
            {
                var tsql = @"
                    Select top 1 item.*,ISNULL(KiTax,1) KiTax 
                    From item 
                    Left join kind on item.kino=kind.kino 
                    Where item.IsEnable='1' and (itno=@itno or itnoudf=@itno) ";

                db.ExecuteReader(tsql, spc => spc.AddWithValue("itno", itno), reader =>
                {
                    result = true;
                    action.Invoke(reader);
                });
            }

            if (result == false)
            {
                Console.Beep();
                Console.Beep();
                Msg.Show("找不到此產品編號!");
            }
        }
        //刪除過時的保留單據 
        static internal void DeleteOldSaveTemp()
        {
            using (var db = new xSQL())
            {
                DataTable temp = new DataTable();
                var sql = @" Select sano from saletemp where sadate <> @day";
                db.Fill(sql, spc => spc.AddWithValue("day", Date.GetDateTime(1)), ref temp);

                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    var tsql = @" 
                        Delete from saletemp where sano = @sano;
                        Delete from saledtemp where sano = @sano;
                        Delete from salebomtemp where sano = @sano; ";
                    db.ExecuteNonQuery(tsql, spc => spc.AddWithValue("sano", temp.Rows[i]["sano"].ToString().Trim()));
                }
            }
        }
         
        //開錢櫃記錄檔
        internal static void LogMoneyBox()
        {
            using (var db = new xSQL())
            {
                var tsql = @"
                Insert into moneyboxlog 
                ( machine, shno, shname, emno, emname, opdate, opdate1, memo) values 
                (@machine,@shno,@shname,@emno,@emname,@opdate,@opdate1,@memo)";

                db.ExecuteNonQuery(tsql, spc =>
                {
                    spc.AddWithValue("machine", NPOS.CurrentMachine);
                    spc.AddWithValue("shno", NPOS.ShNo);
                    spc.AddWithValue("shname", NPOS.ShName);
                    spc.AddWithValue("emno", NPOS.EmNo);
                    spc.AddWithValue("emname", NPOS.EmName);
                    spc.AddWithValue("opdate", Date.GetDateTime(1));
                    spc.AddWithValue("opdate1", Date.GetDateTime(2));
                    spc.AddWithValue("memo", Date.GetDateTime(2, true) + " " + DateTime.Now.ToString("HH:mm:ss"));
                });
            }
        }
        //驗證發票格式是否正確 
        internal static string IsInvNoFormat(string invno)
        {
            var str = invno.ToUpper();
            if (char.IsUpper(str[0]) == false || char.IsUpper(str[1]) == false)
            {
                //Msg.Show("發票字軌輸入錯誤!");
                return "iZZ";
            }
            if (invno.Length != 10)
            {
                // Msg.Show("發票號碼輸入錯誤!");
                return "iStart";
            }
            for (int i = 2; i < 10; i++)
            {
                if (char.IsDigit(str[i]) == false)
                {
                    Msg.Show("發票號碼輸入錯誤!");
                    return "iStart";
                }
            }
            return "OK";
        }
        //檢查發票是否有開立記錄
        internal static bool ISInvNoUsed(string invno)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select Count(*) from posinv where invno = @inno ";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("inno", invno));
                if (count.ToDecimal() > 0)
                {
                    Msg.Show("此發票號碼已有開立記錄!");
                    return true;
                }
            }
            return false;
        }
        //取得當班開始時間
        internal static void GetOnline()
        {
            //using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            //using (SqlCommand cmd = cn.CreateCommand())
            //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            //{
            //    cn.Open();
            //    cmd.Parameters.AddWithValue("offline", "x");
            //    cmd.Parameters.AddWithValue("bracket", "前台");
            //    cmd.Parameters.AddWithValue("seno", NPOS.CurrentMachine);
            //    cmd.Parameters.AddWithValue("online", "");

            //    cmd.CommandText = "Select Max(ISNULL(offline,'')) from sale where bracket=@bracket and seno=@seno and offline != @offline";
            //    NPOS.Online = cmd.ExecuteScalar().ToString().Trim();

            //    if (NPOS.Online.Length == 0)
            //    {
            //        NPOS.Online = Date.GetDateTime(2, true) + " " + DateTime.Now.ToString("HH:mm:ss");
            //    }
            //}
        }


 
    }
}
