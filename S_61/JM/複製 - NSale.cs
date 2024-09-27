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
using System.Threading;
using System.Threading.Tasks;

namespace JBS.JM
{
    public class NSale : JBS.JS.Sale
    {
        public static string[] End = new string[] { "249", "499", "749", "999" };
        public static string iZZ = "";
        public static int iStart = 0;
        public static bool IsZeroMny { get; set; }//結帳時金額是否為零
        public static bool IsStopInv { get; set; }//結帳時是否要停印發票  
        public static List<string> ListInvNo = new List<string>();//結帳時所印的發票(多張)


        //記錄結帳時，金額是否為零，金額零的話=不開發票
        static internal void CheckIsZeroMny(decimal mny)
        {
            NSale.IsZeroMny = (mny == 0);
        }
        //記錄結帳時，是否要停印發票
        static internal void CheckIsStopInv(Color color)
        {
            NSale.IsStopInv = (color == Color.Red);
        }
        //檢查結帳時，發票應開立，但發票欄位卻是空值
        static internal bool CheckIsInvNoNull(string inno)
        {
            if (NSale.IsStopInv == false
                && inno.Trim().Length == 0
                && Common.User_ScInvDev != 3)
            {

                MessageBox.Show("請設定發票號碼!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }
        static internal bool CheckIsNullify(string inno)
        {
            using (var db = new JBS.xSQL())
            {
                var tsql = "Select Count(*) from nullify where invno = @invno";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("invno", inno)).ToDecimal();

                return (count > 0);
            }
        }

        /// <summary>
        /// 零、判斷是否有金額，沒有金額則不開立發票
        /// 一、判斷機台是否設定comport(lpt1)
        /// 二、判斷此次結帳是否停印
        /// 三、取得發票號碼
        /// 四、列印發票
        /// </summary>
        static internal void PrintInvTicket(ref Machine jMachine, ref DataTable tempSaleD)
        {
            try
            {
                if (jMachine.InvPort.Length == 0)
                    return;

                if (NSale.IsZeroMny)
                    return;

                if (NSale.IsStopInv)
                    return;

                jMachine.doInv(tempSaleD);

                NSale.WritePosInv();
                NSale.UpdateSaleReceivInvNo();
            }
            catch
            {
                throw;
            }
            finally
            {
                NSale.ListInvNo.Clear();
            }
        }
        //更新此次開立發票的最後一張號碼至銷貨與沖款單
        static internal void UpdateSaleReceivInvNo()
        {
            using (var db = new xSQL())
            {
                var tsql = @"
                Update Sale       set InvNo  = @InvNo Where SaNo = @SaNo;
                Update Receivd    set InvNo  = @InvNo Where SaNo = @SaNo;";

                db.ExecuteNonQuery(tsql, spc =>
                {
                    spc.AddWithValue("SaNo", NPOS.SaNo);
                    spc.AddWithValue("InvNo", NSale.ListInvNo.Last());
                    spc.AddWithValue("machine", NPOS.CurrentMachine);
                });
            }
        }
        //更新此次開立發票至發票檔
        static internal void WritePosInv()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("sano", NPOS.SaNo);
                cmd.Parameters.AddWithValue("invdate", NPOS.SaDate);
                cmd.Parameters.AddWithValue("invdate1", NPOS.SaDate1);
                cmd.Parameters.AddWithValue("invno", "");
                cmd.Parameters.AddWithValue("invtaxno", NPOS.CuUno);
                cmd.Parameters.AddWithValue("taxmny", NPOS.TaxMny);
                cmd.Parameters.AddWithValue("tax", NPOS.Tax);
                cmd.Parameters.AddWithValue("x5no", NPOS.X5No);
                cmd.Parameters.AddWithValue("totmny", NPOS.TotMny);
                cmd.Parameters.AddWithValue("seno", NPOS.CurrentMachine);
                cmd.Parameters.AddWithValue("iTitle", Common.iTitle);
                cmd.Parameters.AddWithValue("iUnno", Common.iUnno);
                cmd.Parameters.AddWithValue("iTaxNo", Common.iTaxNo);

                for (int i = 0; i < NSale.ListInvNo.Count; i++)
                {
                    if (i == NSale.ListInvNo.Count - 1)
                    {
                        cmd.Parameters["taxmny"].Value = NPOS.TaxMny;
                        cmd.Parameters["tax"].Value = NPOS.Tax;
                        cmd.Parameters["totmny"].Value = NPOS.TotMny;
                    }
                    else
                    {
                        cmd.Parameters["taxmny"].Value = 0;
                        cmd.Parameters["tax"].Value = 0;
                        cmd.Parameters["totmny"].Value = 0;
                    }

                    cmd.Parameters["invno"].Value = NSale.ListInvNo[i];

                    cmd.CommandText = @"
                    Insert Into PosInv (
                     [sano],[invdate],[invdate1],[invno],[invtaxno],[taxmny],[tax]
                    ,[x5no],[totmny],[seno],[iTitle],[iUnno],[iTaxNo])
                    values(
                     @sano,@invdate,@invdate1,@invno,@invtaxno,@taxmny,@tax
                    ,@x5no,@totmny,@seno,@iTitle,@iUnno,@iTaxNo) ";
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        //剩餘發票警示
        static internal string Warning(int countdown)
        {
            if (countdown <= 0)
                return "";

            //if (NSale.iStart.ToString().Length != 8)
            //    return "";

            var now = NSale.iStart % 1000;
            var x = 249 - (now % 250) + 1;
            if (x <= countdown)
                return "剩餘 " + x + " 組號碼";

            return "";
        }
        //列印時，取得即將列印的號碼
        static internal string GetCurrentInvNo()
        {
            using (var db = new xSQL())
            {
                var tsql = "Select ISNULL(InvNoS,'')InvNoS from machineset where machine = @machine";
                var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("machine", NPOS.CurrentMachine));
                var no = obj.ToString().Trim();
                if (no.Length == 0)
                {
                    NSale.iZZ = "";
                    NSale.iStart = 0;
                }
                else
                {
                    NSale.iZZ = no.takeString(2);
                    NSale.iStart = no.skipString(2).ToInteger();
                }

                return obj.ToString().Trim();
            }
        }
        //列印時，發票累加號碼與換捲 
        static internal void PutInNewInvNo(int page)
        {
            var no = NSale.GetCurrentInvNo();
            NSale.ListInvNo.Add(no);

            var end = no.skipString(7);
            if (NSale.End.Contains(end))
            {
                //換捲  
                //using (var frm = new FrmInvNoSet(NPOS.CurrentMachine))
                //{
                //    frm.btnCancel.Visible = false;
                //    frm.ShowDialog();
                //}
            }
            else
            {
                NSale.iStart++;
                using (var db = new xSQL())
                {
                    var tsql = "Update MachineSet set InvNoS = @InvNoS Where machine = @machine";
                    db.ExecuteNonQuery(tsql, spc =>
                    {
                        spc.AddWithValue("InvNoS", NSale.iZZ + NSale.iStart.ToString().PadLeft(8, '0'));
                        spc.AddWithValue("machine", NPOS.CurrentMachine);
                    });
                }
            }

            //if (page == 1)
            //{
            //    no = NSale.GetCurrentInvNo();
            //    NSale.ListInvNo.Add(no);
            //}
            //else
            //{
            //    NSale.iStart++;
            //    no = NSale.iZZ + NSale.iStart.ToString().PadLeft(8, '0');
            //    NSale.ListInvNo.Add(no);
            //}

            //var end = NSale.ListInvNo.Last().skipString(7);
            //if (NSale.End.Contains(end))
            //{
            //    //換捲  
            //    using (var frm = new FrmInvNoSet(NPOS.CurrentMachine))
            //    {
            //        frm.btnCancel.Visible = false;
            //        frm.ShowDialog();
            //    }
            //    //
            //    no = NSale.GetCurrentInvNo();
            //    NSale.iStart--;

            //}
        }
 
    }
}
