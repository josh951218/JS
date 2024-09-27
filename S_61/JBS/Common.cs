using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using System.Linq.Expressions;

namespace S_61.Basic
{
    public enum RptMode : int
    {
        Print, PreView, Word, Excel, Design, Mail
    }
    public enum FormEditState : int
    {
        Append, Duplicate, Modify, Clear, None
    }
    public enum SqlTable : int
    {
        Cust, Fact, Item, Kind, Empl, XX01, XX02, XX03, XX04, XX05, XX06, XX12, Xa01, Dept, Spec, Send, StkRoom, Trade, Addr, PnThead, Tail, Momo01, PayNote, PerNote, Scrit, ScritD, Stock, Sale, SaleD, RSale, RsaleD, Receiv, ReceivD, Payabl, PayablD, BShop, BShopD, RShop, RShopD, Make, Workflow, Workflowd, Workflowfee, Fee, SystemSet, Bom, BomD, Quote, Quoted, QuoteBom, Oder, Fquot, Ford, Special, SaleClass, Lend, RLend, Borr, RBorr
    }
    public enum BarCodePrintMode : int
    {
        Item,BatchNo
    }
    class Common
    {
        public static void NewLoad(ref DataTable dt, string SQL, string TB, string PK, string str, string orderby = "")
        {
            dt.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("@v1", str);
                cmd.CommandText = @"
                    DECLARE @RK BIGINT;

                    SET @RK = (
	                    SELECT top 1 rk 
                        FROM (
                                SELECT ROW_NUMBER()OVER(order by " + PK + " " + orderby + ")rk," + PK + " from " + TB + @" 
                            )A
	                    WHERE A." + PK + " >= (@v1) " + @"
                    );
                    
                    IF @RK IS NULL OR @RK <= 0
                    SET @RK = (
                        SELECT COUNT(*) FROM " + TB + @"
                    );

                    SELECT * 
                    FROM (
	                        SELECT ROW_NUMBER()OVER(order by " + TB + "." + PK + ")rk," + SQL + @" 
                        )A
                    WHERE A.rk > @RK-(" + SearchCount / 2 + ") And A.rk < @RK+(" + SearchCount / 2 + ");";

                da.Fill(dt);
            }
        }
        public static void NewLoad1(ref DataTable dt, string SQL, string TB, string PK, string str, string orderby = "")
        {
            dt.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("@v1", str.Trim());

                if (str.Trim().Length == 0)
                    cmd.CommandText = " Select top (" + SearchCount / 2 + ") " + SQL + " order by " + PK;
                else
                {
                    cmd.CommandText = @"
                        declare @X nvarchar(20);
                        declare @Y nvarchar(20);

                        set @X = (
	                        select top 1 " + PK + @" 
	                        from (
		                        select top (" + SearchCount / 2 + ") " + PK + " from " + TB + " where " + PK + " < @v1 order by " + PK + @" desc
	                        )A order by " + PK + @" asc
                        );

                        if @X is null
                        set @X = (
                            select top 1 " + PK + " from " + TB + " order by " + PK + @"
                        );
 
                        set @Y = (
	                        select top 1 " + PK + @" 
	                        from (
		                        select top (" + SearchCount / 2 + ") " + PK + " from " + TB + " where " + PK + " >= @v1 order by " + PK + @" asc
	                        )A order by " + PK + @" desc
                        );

                        if @Y is null
                        set @Y = (
                            select top 1 " + PK + " from " + TB + " order by " + PK + @" desc
                        );

                        select " + SQL + " And " + PK + " >= @X and " + PK + " <= @Y order by " + PK + " asc;";
                }
                da.Fill(dt);
            }
        }

        public static DataTable ActionTB = new DataTable();
        public static string ActionSql = "";
        public static DataRow load(string Action, string TBName, string Pk, string str = null)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    if (ActionTB != null)
                    {
                        ActionTB.Dispose();
                    }

                    if (ActionTB != null)
                        ActionTB = null;

                    ActionSql = "";
                    switch (Action)
                    {
                        case "Top":
                            ActionSql = "select top(1)* from " + TBName + " order by " + Pk + "";
                            break;
                        case "Prior":
                            ActionSql = "select top(1)* from " + TBName + " where " + Pk + " < @str order by " + Pk + " desc";
                            break;
                        case "Next":
                            ActionSql = "select top(1)* from " + TBName + " where " + Pk + " > @str order by " + Pk + "";
                            break;
                        case "Bottom":
                            ActionSql = "select top(1)* from " + TBName + " order by " + Pk + " desc";
                            break;
                        case "Cancel":
                            ActionSql = "select * from " + TBName + " where " + Pk + " = @str";
                            break;

                        case "Check":
                            ActionSql = "select * from " + TBName + " where " + Pk + " = @str";
                            break;
                        case "CPrior":
                            ActionSql = "select top(1)* from " + TBName + " where " + Pk + " >= @str order by " + Pk + "";
                            break;
                        case "CNext":
                            ActionSql = "select top(1)* from " + TBName + " where " + Pk + " <= @str order by " + Pk + " desc";
                            break;
                    }
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Clear();
                        if (str != null)
                        {
                            cmd.Parameters.AddWithValue("@str", str);
                        }
                        cmd.CommandText = ActionSql;
                        ActionTB = new DataTable();
                        da.Fill(ActionTB);
                    }
                }

                if (ActionTB.Rows.Count > 0)
                {
                    return ActionTB.Rows[0];
                }
                else if (ActionTB.Rows.Count == 0 && Action == "Cancel")
                {
                    if (ActionTB != null)
                    {
                        ActionTB.Dispose();
                    }

                    if (ActionTB != null)
                        ActionTB = null;

                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandText = "select top(1)* from " + TBName + " order by " + Pk + " desc";
                        ActionTB = new DataTable();
                        da.Fill(ActionTB);
                    }

                    if (ActionTB.Rows.Count > 0)
                    {
                        return ActionTB.Rows[0];
                    }
                    else
                    {
                        if (ActionTB != null)
                        {
                            ActionTB.Dispose();
                        }

                        if (ActionTB != null)
                            ActionTB = null;

                        return null;
                    }
                }
                else
                {
                    if (ActionTB != null)
                    {
                        ActionTB.Dispose();
                    }

                    if (ActionTB != null)
                        ActionTB = null;

                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ActionTB != null)
                {
                    ActionTB.Dispose();
                }

                if (ActionTB != null)
                    ActionTB = null;

                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public static int SearchCount;
        public static DataTable SearchTb = new DataTable();
        public static string BeginSql = "";
        public static DataTable Bload(string SQL, string Pk1, string Str1, string Pk2 = null, string Str2 = null, string Pk3 = null, string Str3 = null)
        {
            try
            {
                if (SearchTb != null)
                    SearchTb.Clear();

                BeginSql = "";

                if (Str1.Trim() == "")
                {
                    BeginSql = "select Top(" + SearchCount + ") " + SQL;
                }
                else
                {
                    BeginSql = "select * from (select Top(" + (SearchCount / 2).ToString() + ") " + SQL
                                + " and " + Pk1 + " < @Str1 order by " + Pk1 + " desc) as t1"
                                + " union  all "
                                + "select * from (select Top(" + (SearchCount / 2).ToString() + ") " + SQL
                                + " and " + Pk1 + " >= @Str1 order by " + Pk1 + ") as t2";
                }

                if (Pk2 != null && Str2 != null)
                {
                    BeginSql += " union "
                                + " select * from(select Top(" + (SearchCount / 2).ToString() + ") " + SQL
                                + " and " + Pk2 + " < @Str2 order by " + Pk2 + " desc)as t3"
                                + " union "
                                + " select * from(select Top(" + (SearchCount / 2).ToString() + ") " + SQL
                                + " and " + Pk2 + " >= @Str2 order by " + Pk2 + ") as t4";
                }
                if (Pk3 != null && Str3 != null)
                {
                    BeginSql += " union "
                                + " select * from(select Top(" + (SearchCount / 2).ToString() + ") " + SQL
                                + " and " + Pk3 + " < @Str3 order by " + Pk3 + " desc) as t5"
                                + " union "
                                + " select * from(select Top(" + (SearchCount / 2).ToString() + ") " + SQL
                                + " and " + Pk3 + " >= @Str3 order by " + Pk3 + ") as t6";
                }
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    //cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    if (Str1 != null)
                        cmd.Parameters.AddWithValue("Str1", Str1);
                    if (Str2 != null)
                        cmd.Parameters.AddWithValue("Str2", Str2);
                    if (Str3 != null)
                        cmd.Parameters.AddWithValue("Str3", Str3);
                    cmd.CommandText = BeginSql;
                    SqlDataAdapter dd;
                    dd = new SqlDataAdapter(cmd);
                    SearchTb.Dispose();
                    SearchTb = new DataTable();
                    dd.Fill(SearchTb);
                    if (SearchTb.Rows.Count == 0)
                    {
                        var row = SearchTb.NewRow();
                        SearchTb.Rows.Add(row);
                        SearchTb.AcceptChanges();
                        MessageBox.Show("資料庫忙碌中，請重新輸入", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return SearchTb;
        }


        public static void Cload(DataTable dt, string SQL, string Pk1, string Str1, string Pk2 = null, string Str2 = null, string Pk3 = null, string Str3 = null)
        {
            try
            {
                BeginSql = "";
                string a = "", b = "";

                if (Str1.Trim() == "")
                {
                    a = "select Top(" + SearchCount + ") " + SQL;
                }
                else
                {
                    var T1 = "T1" + Pk1.Substring(Pk1.IndexOf("."));
                    a = " Select * From (Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " < (@Str1) Order by " + Pk1 + " Desc) AS T1 Order by " + T1 + " ASC";
                    b = " Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " >= (@Str1) Order by " + Pk1;
                }

                if (Pk2 != null && Str2 != null)
                {
                    var T1 = "T1" + Pk1.Substring(Pk1.IndexOf("."));
                    var T2 = "T1" + Pk2.Substring(Pk2.IndexOf("."));
                    a = " Select * From (Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " < (@Str1) And " + Pk2 + " < (@Str2) Order by " + Pk1 + " Desc, " + Pk2 + " Desc) AS T1 Order by " + T1 + " ASC, " + T2 + " ASC";
                    b = " Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " >= (@Str1) And " + Pk2 + " >= (@Str2)  Order by " + Pk1 + ", " + Pk2;
                }
                if (Pk3 != null && Str3 != null)
                {
                    var T1 = "T1" + Pk1.Substring(Pk1.IndexOf("."));
                    var T2 = "T1" + Pk2.Substring(Pk2.IndexOf("."));
                    var T3 = "T1" + Pk3.Substring(Pk3.IndexOf("."));
                    a = " Select * From (Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " < (@Str1) And " + Pk2 + " < (@Str2) And " + Pk3 + " < (@Str3) Order by " + Pk1 + " Desc, " + Pk2 + " Desc, " + Pk3 + " Desc ) AS T1 Order by " + T1 + " ASC, " + T2 + " ASC, " + T3 + " ASC";
                    b = " Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " >= (@Str1) And " + Pk2 + " >= (@Str2) And " + Pk3 + " >= (@Str3)  Order by " + Pk1 + ", " + Pk2 + ", " + Pk3;
                }

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    if (Str1 != null) cmd.Parameters.AddWithValue("@Str1", Str1);
                    if (Str2 != null) cmd.Parameters.AddWithValue("@Str2", Str2);
                    if (Str3 != null) cmd.Parameters.AddWithValue("@Str3", Str3);

                    dt.Clear();
                    if (a.Length > 0)
                    {
                        cmd.CommandText = a;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    if (b.Length > 0)
                    {
                        cmd.CommandText = b;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void Dload(DataTable dt, string SQL, string Pk1, string Str1, string Pk2 = null, string Str2 = null, string Pk3 = null, string Str3 = null)
        {
            try
            {
                BeginSql = "";
                string a = "", b = "";

                if (Str1.Trim() == "")
                {
                    a = "select Top(" + SearchCount + ") " + SQL;
                }
                else
                {
                    if (Pk1.ToLower().Contains("date"))
                    {
                        var T1 = "T1" + Pk1.Substring(Pk1.IndexOf("."));
                        a = " Select * From (Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " < (@Str1) Order by " + Pk1 + " Desc) AS T1 Order by " + T1 + " DESC";
                        b = " Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " >= (@Str1) Order by " + Pk1 + " ASC";
                    }
                    else
                    {
                        var T1 = "T1" + Pk1.Substring(Pk1.IndexOf("."));
                        a = " Select * From (Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " < (@Str1) Order by " + Pk1 + " Desc) AS T1 Order by " + T1 + " ASC";
                        b = " Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " >= (@Str1) Order by " + Pk1;
                    }
                }

                if (Pk2 != null && Str2 != null)
                {
                    var T1 = "T1" + Pk1.Substring(Pk1.IndexOf("."));
                    var T2 = "T1" + Pk2.Substring(Pk2.IndexOf("."));
                    if (Pk1.ToLower().Contains("date"))
                    {
                        a = " Select * From (Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " > (@Str1) And " + Pk2 + " < (@Str2) Order by " + Pk1 + " ASC, " + Pk2 + " DESC) AS T1 Order by " + T1 + " DESC, " + T2 + " ASC";
                        b = " Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " <= (@Str1) And " + Pk2 + " >= (@Str2) Order by " + Pk1 + " DESC, " + Pk2;
                    }
                    else
                    {
                        a = " Select * From (Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " < (@Str1) And " + Pk2 + " > (@Str2) Order by " + Pk1 + " DESC, " + Pk2 + " ASC) AS T1 Order by " + T1 + " ASC, " + T2 + " DESC";
                        b = " Select Top (" + (SearchCount / 2).ToString() + ") " + SQL + " And " + Pk1 + " >= (@Str1) And " + Pk2 + " <= (@Str2)  Order by " + Pk1 + ", " + Pk2 + " DESC";
                    }
                }

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    if (Str1 != null) cmd.Parameters.AddWithValue("@Str1", Str1);
                    if (Str2 != null) cmd.Parameters.AddWithValue("@Str2", Str2);
                    if (Str3 != null) cmd.Parameters.AddWithValue("@Str3", Str3);

                    dt.Clear();
                    if (a.Length > 0)
                    {
                        cmd.CommandText = a;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    if (b.Length > 0)
                    {
                        cmd.CommandText = b;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void Eload(DataTable dt, string SQL, string Pk1, string Str1, string SORTdate)
        {
            var searchcount = SearchCount;
            var a = " Select Top(" + searchcount + ") " + SQL + " And " + Pk1 + " =(@Str1) Order By " + SORTdate + " DESC";

            dt.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = a;
                cmd.Parameters.AddWithValue("@Str1", Str1);
                da.Fill(dt);

                searchcount -= dt.Rows.Count;
                if (searchcount > 0)
                {
                    var T1 = "T1" + Pk1.Substring(Pk1.IndexOf("."));
                    a = " Select * From (Select Top (" + (searchcount / 2).ToString() + ") " + SQL + " And " + Pk1 + " < (@Str1) Order by " + Pk1 + " Desc) AS T1 Order by " + T1 + " ASC";
                    a = "Select Top (" + (searchcount / 2).ToString() + ") " + SQL + " And " + Pk1 + " < (@Str1) Order by " + Pk1 + " Desc";
                    var b = " Select Top (" + (searchcount / 2).ToString() + ") " + SQL + " And " + Pk1 + " > (@Str1) Order by " + Pk1 + " ASC";


                    //b = " Select Top (" + (searchcount / 2).ToString() + ") " + SQL + " And " + Pk1 + " > (@Str1) Order by " + Pk1 + " ASC";

                    cmd.CommandText = a;
                    da.Fill(dt);
                    cmd.CommandText = b;
                    da.Fill(dt);


                }
            }
        }
        public static void Fload(DataTable dt, string SQL, string Pk1, string Str1, string Pk2, string Str2, string SORTdate)
        {
            var searchcount = SearchCount;
            var a = " Select Top(" + searchcount + ") " + SQL + " And " + Pk1 + " =(@Str1) And " + Pk2 + " =(@Str2) Order By " + SORTdate + " DESC";

            dt.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = a;
                cmd.Parameters.AddWithValue("@Str1", Str1);
                cmd.Parameters.AddWithValue("@Str2", Str2);
                da.Fill(dt);

                searchcount -= dt.Rows.Count;
                if (searchcount > 0)
                {
                    var T1 = "T1" + Pk1.Substring(Pk1.IndexOf("."));
                    //a = " Select * From (Select Top (" + (searchcount / 2).ToString() + ") " + SQL + " And " + Pk1 + " < (@Str1) Order by " + Pk1 + " Desc) AS T1 Order by " + T1 + " ASC";
                    a = "Select Top (" + (searchcount / 2).ToString() + ") " + SQL + " And " + Pk1 + " <= (@Str1) And " + Pk2 + " < (@Str2) Order by " + Pk1 + " Desc, " + Pk2 + " Desc";
                    var b = " Select Top (" + (searchcount / 2).ToString() + ") " + SQL + " And " + Pk1 + " >= (@Str1) And " + Pk2 + " > (@Str2) Order by " + Pk1 + " ASC, " + Pk2 + " ASC";

                    cmd.CommandText = a;
                    da.Fill(dt);
                    cmd.CommandText = b;
                    da.Fill(dt);


                }
            }
        }

        public static Keys keyData;

        public static int Sql_LogMod;//紀錄登入模式：1SQL驗證 2本機驗證
        public static string DatabaseName = "";

        public static string CompanyName;

        public static string Series;
        public static string Vers;
        public static string Regist;
        public static int 授權數 = 0;
        public static int 連線數 = 0;
        public static System.Timers.Timer ckTime;
        public static string sqlConnString = "";
        public static DataTable dtUsSettings = new DataTable();
        public static DataTable dtSysSettings = new DataTable();
        public static List<DataRow> listSysSettings = new List<DataRow>();
        public static List<DataRow> listUsSettings = new List<DataRow>();


        public static DataTable dtEnd = new DataTable();//報表尾
        public static DataTable dtstart = new DataTable();//報表頭
        public static Report.Frmreport FrmReport;
        public static string reportaddress = "..\\..\\";//報表位置
        public static TableLogOnInfo logOnInfo = new TableLogOnInfo();
        public static decimal InvoMode = 2;
        public static int User_CloseMgr = 1;//1是2否
        public static string Sys_CloseDate = "";//關帳日期








        //使用者參數
        public static string User_Name;//使用者帳號
        public static string User_Name1;//使用者帳號
        public static int User_DateTime;//記錄使用者日期格式：1民國.2西元.
        public static string User_StkNo;//記錄使用者預設倉庫
        public static string User_X5No;//記錄使用者預設發票模式
        public static string User_CuNo;//記錄前台預設客戶
        public static bool User_SalePrice;//記錄使用者是否可以看售價：true/false
        public static bool User_ShopPrice;//紀錄使用者是否可以看成本：true/false
        public static int User_ScInvDev;//發票機埠 1:Com1  2:Com2  3:無
        public static int User_ScInvDevs;//錢櫃連接埠 1:Com1  2:Com2  3:連接發票機 4:無
        public static int User_ScPriDev;//價格顯示器 1:Com1  2:Com2  3:無
        public static int User_ShowKeyBoard = 1;//是否要螢幕小鍵盤 1:要 2:不要
        public static int User_StopInvPrint = 1;//是否可以停印發票 1:可 2:不可
        public static string User_Formula = "";//材積公式
        public static int User_ScInvSlt = 1;//1手工 2自動
        public static int User_ScInvBat = 1;//1是 2否
        public static int User_StopInvMode = 1;//1是 2否
        public static int User_CanEditPOS = 2;//1是 2否
        public static int User_CanCelPrompt = 1;//取消提示 1是 2否 
        public static string User_InvSalePort ="";//銷貨單印發票 

        public static string iTitle;//使用者參數設定-公司抬頭
        public static string iStore;//店號
        public static string iUnno;//統編
        public static string iTaxNo;//稅籍
        public static string iTel;//電話
        public static string iAddress;//地址
        public static string iMemo1;//備註一
        public static string iMemo2;//備註二
        public static string isCheck;//主管覆核 1是2否
        public static string sc_MachineSet;

        //系統參數
        public static int iFirst = 10;
        public static int nFirst = 12;//數值欄位『整數位數』
        public static int MS = 0;//銷貨單價小數
        public static int MST = 0;//銷貨單據小數
        public static int TS = 0;//銷項稅額小數
        public static int M = 0;//本幣金額小數

        public static int MF = 0;//進貨單價小數
        public static int MFT = 0;//進貨單據小數
        public static int TF = 0;//進項稅額小數
        public static int Q = 0;//庫存數量小數
        public static int DQ = 0;//單據數量小數

        public static int TPS = 0;//銷項金額小數
        public static int TPF = 0;//進項金額小數
        public static decimal Sys_Rate = 0.05M;//稅率

        public static int Sys_StockKind;//系統運作模式
        public static int Sys_NoAdd;//單據編碼方式：1民國年月日+流水 2民國年月+流水 3西元年月日+流水 4西元年月+流水
        public static int Sys_KeyPrs;//單據可不可以輸入折數欄位：1可輸入 2不可輸入
        public static int Sys_LowStock;//單據低於庫存量是否警告：1警告 2不警告
        public static int Sys_LowStockMode = 1;//庫存量是否警告1總倉2分倉
        public static int Sys_LowStkSlt;//承上。警告依據：1安全存量 2庫存0
        public static int Sys_LowCost;//單據售價低於成本：1警告 2不警告
        public static int Sys_UpCredit;//超過信用額度：1限制出貨 2銷貨警告 3銷貨不警告
        public static int Sys_SalePrice;//單據售價取用方式：1產品建檔售價 2客戶建檔折數 3類別折數 4歷史售價 5類別等級折數 6均為零 7歷史報價
        public static int Sys_StNoMode;//單據單倉/多倉設定：1單倉 2多倉
        public static int Sys_StkYear1;//庫存年度民國年
        public static int Sys_StkYear2;//庫存年度西元年
        public static int Sys_CuSaleMny;//銷費金額
        public static int Sys_CuPoint;//紅利
        public static string Sys_PosMPW;//管理密碼
        public static int Sys_DBqty;//1進銷存 2進銷存(計價版)
        public static bool Sys_JZOK = false;//是否做過年度結轉
        public static int Sys_BuyPrice;//進價取用方式：1產品建檔售價 2歷史進價 3類別折數 4均為零 5歷史詢價
        public static int Sys_AutoBuyp;//產品進價異動：1手動更新 2自動更新
        public static int Sys_X3Forward = 2;//內含稅計算:1向前 2向後
        public static int Sys_InvUsed = 2; //發票外掛是否使用 1使用 2不使用
        public static int Sys_DefaultAddr = 1;//預設地址
        public static string Sys_BookNo = "";//保證書序號
        public static int Sys_UsingBatch;      //批號管理 : 1不啟用 2啟用
        public static int Sys_WebOrder;        //網路訂單 : 1不啟用 2啟用
        public static int Sys_ItNameLenth  = 0; //資料庫結構 : 品名規格長度
        public static int Sys_ItNoUdfLenth = 0; //資料庫結構 : 自訂編號長度


        public static string Sys_Stcname = "";//公司名稱
        public static string Sys_Stctaxno = "";//統一編號
        public static string Sys_Stctaxno1 = "";//稅籍編號 

        public static string Sys_StcPnName;//報表抬頭
        public static string Sys_SaleHead;//銷貨單標題
        public static string Sys_MemoUdf;//備註抬頭
        public static string pathC = "";//票據路徑
        public static string CoNo = "";//進銷存代號

        public static string Update_IP = "";
        public static string Update_Folder = "";
        public static string Update_Account = "";
        public static string Update_Pwd = "";
        public static int Sys_LendToSaleMode = 1;// 1一般 2宏恩
        public static string User_Einv = "";//電子發票獨立設定
        public static int Sys_Einvusen ;//電子發票使用家數
        

        //特加區間取用
        public static void GetSpecialPrice(DataRow row, int index, JE.MyControl.TextBoxT CuNo, JE.MyControl.TextBoxT date, DataGridView dataGridViewT1, Action<DataRow, int> GetSystemPrice)
        {
            var prs = 1M;
            var price = 0M;
            var SpTrait = 0M;

            var itno = row["itno"].ToString().Trim();
            var itname = row["itname"].ToString();
            var unit = row["itunit"].ToString().Trim();
            var qty = row["qty"].ToDecimal("f" + Common.Q);
            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
            try
            {
                string SqlCom = @" SELECT * FROM (select *
 ,CuNo = (select top 1 cuno from Special where spno = Speciald.SpNo)
 ,Cux1No = (select top 1 Cux1No from Special where spno = Speciald.SpNo)
 from Speciald  where 0=0  
 and ItNo=(@itno)
 and ItUnit=(@unit)
 and ItPkgqty=(@itpkgqty)
 and EDate >= @EDate
 and SDate <= @SDate
 ) Speciald ";
                string OrderByStr = " order by SDate DESC,RecordNo DESC ";
                //取得特價客戶群
                string cux1no = SQL.ExecuteScalar("select top 1 cux1no from cust where cuno = @cuno", new parameters("cuno", CuNo.Text.Trim()));
                parameters par = new parameters();
                par.AddWithValue("itno", itno);
                par.AddWithValue("unit", unit);
                par.AddWithValue("itpkgqty", itpkgqty.ToString());
                par.AddWithValue("cux1no", cux1no);
                par.AddWithValue("cuno", CuNo.Text.Trim());
                par.AddWithValue("EDate", Date.ToTWDate(date.Text.Trim()));
                par.AddWithValue("SDate", Date.ToTWDate(date.Text.Trim()));
                bool 已找出特價資料 = false;
                if (!已找出特價資料) //特價客戶
                {
                    已找出特價資料 = SQL.ExecuteReader(SqlCom + " where cuno=@cuno " + OrderByStr, par, r =>
                    {
                        SpTrait = r["SpTrait"].ToDecimal();
                        prs = r["prs"].ToDecimal("f3");
                        price = r["price"].ToDecimal("f" + Common.MS);
                    });
                }
                if (cux1no.Length > 0)//如果有客戶群組不為空時才撈以免浪費資源
                {
                    if (!已找出特價資料)//特價客群
                    {
                        已找出特價資料 = SQL.ExecuteReader(SqlCom + " where cux1no=@cux1no " + OrderByStr, par, r =>
                        {
                            SpTrait = r["SpTrait"].ToDecimal();
                            prs = r["prs"].ToDecimal("f3");
                            price = r["price"].ToDecimal("f" + Common.MS);
                        });
                    }
                }
                if (!已找出特價資料) //特價產品
                {
                    已找出特價資料 = SQL.ExecuteReader(SqlCom + " where cux1no='' AND cuno='' " + OrderByStr, par, r =>
                    {
                        SpTrait = r["SpTrait"].ToDecimal();
                        prs = r["prs"].ToDecimal("f3");
                        price = r["price"].ToDecimal("f" + Common.MS);
                    });
                }

                if (已找出特價資料)
                {
                    if (SpTrait == 1)
                    {
                        row["price"] = price;
                        row["prs"] = 1.000;
                        dataGridViewT1.InvalidateRow(index);
                    }
                    else if (SpTrait == 2)
                    {
                        string 產品基本檔售價 = SQL.ExecuteScalar("select top 1 itprice from item where itno = @itno ", new parameters("itno", itno));
                        row["price"] = 產品基本檔售價;
                        row["prs"] = prs;
                        dataGridViewT1.InvalidateRow(index);
                    }
                }
                else
                {
                    GetSystemPrice(row, index);
                    dataGridViewT1.InvalidateRow(index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //public static void Excel匯出與開啟(DataTable dt, string FileName)
        //{
        //    try
        //    {
        //        string FilePath = Application.StartupPath + "\\temp\\" + FileName + ".xls";
        //        JBS.ResloveCrystalReportExcel excel解析 = new JBS.ResloveCrystalReportExcel();
        //        excel解析.ExportExcelDt(dt, FilePath, true);
        //        while (!System.IO.File.Exists(FilePath)) { }
        //        Process.Start(FilePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        public static int ToInt32(object obj)
        {
            return (int)obj.ToDecimal();
        }
        public static bool ToBool(object obj)
        {
            bool flag = false;
            if (!obj.IsNullOrEmpty())
            {
                if (obj.ToString() == "1")
                {
                    flag = true;
                }
            }
            return flag;
        }

        //檢查程式版本與資料庫版本是否同步
        public static bool CheckVer(string vers)
        {
            using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select vers from systemset";
                    Common.Vers = cmd.ExecuteScalar().ToDecimal().ToString("f1");
                    if (cmd.ExecuteScalar().ToDecimal() == vers.ToDecimal()) return true;
                }
            }
            return false;
        }

        public static bool CheckWKS()
        {
            if (Environment.UserName.ToUpper().Contains("WKS5")) return true;

            decimal 工作站數 = 0;
            decimal 工作站台數 = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    Security Sec = new Security();
                    DataTable dtsys = new DataTable();
                    DataTable dtmac = new DataTable();
                    DataTable dtcount = new DataTable();

                    string CT = "";
                    string uomgp = "0";
                    string sql = "select * from systemset";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.Fill(dtsys);
                        if (dtsys.Rows.Count > 0)
                        {
                            //註冊版本與程式版本不符
                            CT = dtsys.Rows[0]["CT"].ToString();
                            uomgp = dtsys.Rows[0]["uomgp"].ToString();
                        }
                        else
                            return false;
                    }
                    ////判斷資料庫是否移動過
                    sql = "select CONVERT(nvarchar(30),create_date,114) CT FROM sys.databases where name ='" + Common.DatabaseName + "'";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.Fill(dtmac);
                        if (dtmac.Rows.Count > 0)
                            if (dtmac.Rows[0]["CT"].ToString().Trim().Replace(":", "") != CT) return false;
                    }

                    //取得目前工作站數
                    sql = " SELECT database_id cndbid FROM master.sys.databases where name = '" + Common.DatabaseName + "'";
                    var cndbid = 0;
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dtdbid = new DataTable();
                        da.Fill(dtdbid);
                        if (dtdbid.Rows.Count > 0) cndbid = dtdbid.Rows[0]["cndbid"].ToInteger();
                    }
                    sql = " select hostprocess 主機,net_address 位址 from master..sysprocesses where program_name = 'JBS' and dbid=" + cndbid;
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.Fill(dtcount);
                    }
                    DataTable temp = dtcount.Copy();
                    temp.Clear();
                    for (int i = 0; i < dtcount.Rows.Count; i++)
                    {
                        var row = temp.AsEnumerable().Where(r => r["主機"].ToString() == dtcount.Rows[i]["主機"].ToString() && r["位址"].ToString() == dtcount.Rows[i]["位址"].ToString());
                        if (row.Count() == 0)
                            temp.ImportRow(dtcount.Rows[i]);
                    }
                    temp.AcceptChanges();
                    //目前連線中的數量
                    工作站數 = temp.Rows.Count.ToDecimal();

                    string 工作站台數temp = "";
                    工作站台數temp = Sec.數字解密(uomgp, CT + "0");

                    if (工作站台數temp.Length != 10) return false;
                    if (工作站台數temp[(int)工作站台數temp[0].ToDecimal() + 2] != '7'
                        || 工作站台數temp[(int)工作站台數temp[0].ToDecimal() + 3] != '8') return false;

                    //註冊的台數
                    工作站台數 = (工作站台數temp.Substring((int)工作站台數temp[0].ToDecimal(), 2)).ToDecimal();

                    授權數 = 工作站台數.ToInteger();
                    連線數 = temp.Rows.Count;
                }
                return (工作站台數 >= 工作站數);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static string GetSeries()
        {
            if (Environment.UserName.ToUpper().Contains("WKS5")) return "71";

            string[] temp = new string[] { "71", "72", "73", "74" };
            Security Sec = new Security();
            string ser = "74";
            try
            {
                DataTable t = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from systemset where usrno=N'T01'";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        t.Clear();
                        da.Fill(t);
                    }
                }
                var vera = t.Rows[0]["vera"].ToString().Trim();
                if (Common.Regist == "[教育版]")
                {
                    if (vera.Length > 0)
                    {
                        //有註冊
                        var ct = t.Rows[0]["CT"].ToString().Trim() + "0";
                        ser = Sec.數字解密(vera, ct);
                        if (ser.Length != 10)
                        {
                            ser = "74";
                            return ser;
                        }
                        if (ser.ToDecimal() == 0)
                        {
                            ser = "74";
                            return ser;
                        }
                        ser = ser[(int)ser[0].ToDecimal()].ToString() + ser[(int)ser[0].ToDecimal() + 1].ToString();
                        if (temp.Count(r => r.ToString() == ser) == 0) ser = "74";
                    }
                    else
                    {
                        //讀設定檔
                        //ser = GetPrivateProfileString("JS設定", "Series");
                        //if (temp.Count(r => r.ToString() == ser) == 0) ser = "74";
                        ser = "71";
                    }
                }
                else
                {
                    //正式版亂改版
                    var ct = t.Rows[0]["CT"].ToString().Trim() + "0";
                    ser = Sec.數字解密(vera, ct);
                    if (ser.Length != 10)
                    {
                        ser = "74";
                        return ser;
                    }
                    if (ser.ToDecimal() == 0)
                    {
                        ser = "74";
                        return ser;
                    }
                    ser = ser[(int)ser[0].ToDecimal()].ToString() + ser[(int)ser[0].ToDecimal() + 1].ToString();
                    if (temp.Count(r => r.ToString() == ser) == 0) ser = "74";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return ser;
        }

        public static decimal GetDBVers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select vers from systemset";
                        return cmd.ExecuteScalar().ToDecimal("f1");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0M;
            }
        }

        public static bool 加庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
        {
            try
            {
                var column = stname == "" ? "stno" : stname;
                if (dt.Columns.Contains(column) == false) return true;

                var stno = "";
                var itno = "";
                var qty = 0M;
                var itpkgqty = 0M;
                var TotalQty = 0M;
                foreach (DataRow row in dt.Select("ItTrait = '2' OR ItTrait = '3'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    itno = row["itno"].ToString().Trim();
                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();
                    TotalQty = (qty * itpkgqty).ToDecimal("f" + Common.Q);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StNo", stno);
                    cmd.Parameters.AddWithValue("@ItNo", itno);
                    cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) + (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + TotalQty + ");";
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                    TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                    cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                    cmd.ExecuteNonQuery();
                }
                foreach (DataRow row in dt.Select("ItTrait = '1'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();

                    var rec = row["BomRec"].ToString().Trim();
                    foreach (DataRow rw in dtBom.Select("BomRec = '" + rec + "'"))
                    {
                        itno = rw["itno"].ToString().Trim();
                        TotalQty = (qty * itpkgqty * (rw["itqty"].ToDecimal() * rw["itpkgqty"].ToDecimal() / rw["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q);

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StNo", stno);
                        cmd.Parameters.AddWithValue("@ItNo", itno);
                        cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) + (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + TotalQty + ");";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                        TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                        cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool 扣庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
        {
            try
            {
                var column = stname == "" ? "stno" : stname;
                if (dt.Columns.Contains(column) == false) return true;

                var stno = "";
                var itno = "";
                var qty = 0M;
                var itpkgqty = 0M;
                var TotalQty = 0M;
                foreach (DataRow row in dt.Select("ItTrait = '2' OR ItTrait = '3'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    itno = row["itno"].ToString().Trim();
                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();
                    TotalQty = (qty * itpkgqty).ToDecimal("f" + Common.Q);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StNo", stno);
                    cmd.Parameters.AddWithValue("@ItNo", itno);
                    cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) - (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + (-1 * TotalQty) + ");";
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                    TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                    cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                    cmd.ExecuteNonQuery();
                }
                foreach (DataRow row in dt.Select("ItTrait = '1'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();

                    var rec = row["BomRec"].ToString().Trim();
                    foreach (DataRow rw in dtBom.Select("BomRec = '" + rec + "'"))
                    {
                        itno = rw["itno"].ToString().Trim();
                        TotalQty = (qty * itpkgqty * (rw["itqty"].ToDecimal() * rw["itpkgqty"].ToDecimal() / rw["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q);

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@StNo", stno);
                        cmd.Parameters.AddWithValue("@ItNo", itno);
                        cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) - (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + (-1 * TotalQty) + ");";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                        TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                        cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Form
        public static void SetTextState(FormEditState state, ref List<JE.MyControl.TextBoxbase> list)
        {
            if (state == FormEditState.Append)
            {
                foreach (var p in list)
                {
                    p.Clear();
                    if (!p.AllowGrayBackColor) p.ReadOnly = false;
                }
            }
            else if (state == FormEditState.Duplicate)
            {
                foreach (var p in list)
                {
                    if (!p.AllowGrayBackColor) p.ReadOnly = false;
                }
            }
            else if (state == FormEditState.Modify)
            {
                foreach (var p in list)
                {
                    if (!p.AllowGrayBackColor) p.ReadOnly = false;
                }
            }
            else if (state == FormEditState.Clear)
            {
                foreach (var p in list) p.Clear();
            }
            else if (state == FormEditState.None)
            {
                foreach (var p in list) p.ReadOnly = true;
            }
        }
        public static bool JESetSSID(SqlTable table, ref JE.MyControl.TextBoxT tbDate, ref JE.MyControl.TextBoxT tbNo, SqlCommand cmd = null)
        {
            DataTable t = new DataTable();
            try
            {
                if (tbNo.TrimTextLenth() > 0)
                {
                    var row = Common.load("Check", table.ToString(), tbNo.Name, tbNo.Text.Trim());
                    if (row != null)
                    {
                        MessageBox.Show("單據編號重複！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                var pkColumn = tbNo.Name;
                var date = "";
                var sql = "";
                using (SqlConnection cn = new SqlConnection(sqlConnString))
                {
                    if (Common.Sys_NoAdd == 1)
                    {
                        date = Date.ToTWDate(tbDate.Text.Trim());
                        sql = " select " + pkColumn + " from " + table.ToString() + " where " + pkColumn + " like @date+'%' and Len(" + pkColumn + ")=11";
                    }
                    else if (Common.Sys_NoAdd == 2)
                    {
                        date = Date.ToTWDate(tbDate.Text.Trim());
                        date = date.takeString(5) + "00";
                        sql = " select " + pkColumn + " from " + table.ToString() + " where " + pkColumn + " like @date+'%' and Len(" + pkColumn + ")=11";
                    }
                    else if (Common.Sys_NoAdd == 3)
                    {
                        date = Date.ToUSDate(tbDate.Text.Trim());
                        sql = " select " + pkColumn + " from " + table.ToString() + " where " + pkColumn + " like @date+'%' and Len(" + pkColumn + ")=12";
                    }
                    else if (Common.Sys_NoAdd == 4)
                    {
                        date = Date.ToUSDate(tbDate.Text.Trim());
                        date = date.takeString(6) + "00";
                        sql = " select " + pkColumn + " from " + table.ToString() + " where " + pkColumn + " like @date+'%' and Len(" + pkColumn + ")=12";
                    }

                    if (cmd == null)
                    {
                        using (cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.CommandText = sql;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(t);
                            }
                        }
                    }
                    else
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(t);
                        }
                    }
                }
                decimal d = 1M;
                var ssid = "";
                if (t.Rows.Count == 0)
                {
                    if (Common.Sys_NoAdd == 1)
                    {
                        ssid = date + (d.ToString().PadLeft(4, '0'));
                    }
                    else if (Common.Sys_NoAdd == 2)
                    {
                        ssid = date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                    }
                    else if (Common.Sys_NoAdd == 3)
                    {
                        ssid = date + (d.ToString().PadLeft(4, '0'));
                    }
                    else if (Common.Sys_NoAdd == 4)
                    {
                        ssid = date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                    }
                    tbNo.Text = ssid;
                }
                else
                {
                    d = t.AsEnumerable().Max(r => r[pkColumn].ToString().Trim().skipString(r[pkColumn].ToString().Trim().Length - 4).ToDecimal());
                    if (0 < d && d < 9999)
                    {
                        d++;
                        if (Common.Sys_NoAdd == 1)
                        {
                            ssid = date + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 2)
                        {
                            ssid = date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                        }
                        else if (Common.Sys_NoAdd == 3)
                        {
                            ssid = date + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 4)
                        {
                            ssid = date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                        }
                        tbNo.Text = ssid;
                    }
                    else
                    {
                        d = 1M;
                        while (true)
                        {
                            if (Common.Sys_NoAdd == 1)
                            {
                                ssid = date + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 2)
                            {
                                ssid = date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                            }
                            else if (Common.Sys_NoAdd == 3)
                            {
                                ssid = date + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 4)
                            {
                                ssid = date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                            }
                            if (t.AsEnumerable().Count(r => r[pkColumn].ToString().Trim() == ssid) > 0)
                            {
                                d++;
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        tbNo.Text = ssid;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                t.Dispose();
            }
        }
        public static bool JESetSSID(string table, ref JE.MyControl.TextBoxT tbDate, ref JE.MyControl.TextBoxT tbNo, SqlCommand cmd = null)
        {
            DataTable t = new DataTable();
            try
            {
                if (tbNo.TrimTextLenth() > 0)
                {
                    var row = Common.load("Check", table.ToString(), tbNo.Name, tbNo.Text.Trim());
                    if (row != null)
                    {
                        MessageBox.Show("單據編號重複！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                var pkColumn = tbNo.Name;
                var date = "";
                var sql = "";
                using (SqlConnection cn = new SqlConnection(sqlConnString))
                {
                    if (Common.Sys_NoAdd == 1)
                    {
                        date = Date.ToTWDate(tbDate.Text.Trim());
                        sql = " select " + pkColumn + " from " + table.ToString() + " where " + pkColumn + " like @date+'%' and Len(" + pkColumn + ")=11";
                    }
                    else if (Common.Sys_NoAdd == 2)
                    {
                        date = Date.ToTWDate(tbDate.Text.Trim());
                        date = date.takeString(5) + "00";
                        sql = " select " + pkColumn + " from " + table.ToString() + " where " + pkColumn + " like @date+'%' and Len(" + pkColumn + ")=11";
                    }
                    else if (Common.Sys_NoAdd == 3)
                    {
                        date = Date.ToUSDate(tbDate.Text.Trim());
                        sql = " select " + pkColumn + " from " + table.ToString() + " where " + pkColumn + " like @date+'%' and Len(" + pkColumn + ")=12";
                    }
                    else if (Common.Sys_NoAdd == 4)
                    {
                        date = Date.ToUSDate(tbDate.Text.Trim());
                        date = date.takeString(6) + "00";
                        sql = " select " + pkColumn + " from " + table.ToString() + " where " + pkColumn + " like @date+'%' and Len(" + pkColumn + ")=12";
                    }

                    if (cmd == null)
                    {
                        using (cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.CommandText = sql;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(t);
                            }
                        }
                    }
                    else
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(t);
                        }
                    }
                }
                decimal d = 1M;
                var ssid = "";
                if (t.Rows.Count == 0)
                {
                    if (Common.Sys_NoAdd == 1)
                    {
                        ssid = date + (d.ToString().PadLeft(4, '0'));
                    }
                    else if (Common.Sys_NoAdd == 2)
                    {
                        ssid = date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                    }
                    else if (Common.Sys_NoAdd == 3)
                    {
                        ssid = date + (d.ToString().PadLeft(4, '0'));
                    }
                    else if (Common.Sys_NoAdd == 4)
                    {
                        ssid = date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                    }
                    tbNo.Text = ssid;
                }
                else
                {
                    d = t.AsEnumerable().Max(r => r[pkColumn].ToString().Trim().skipString(r[pkColumn].ToString().Trim().Length - 4).ToDecimal());
                    if (0 < d && d < 9999)
                    {
                        d++;
                        if (Common.Sys_NoAdd == 1)
                        {
                            ssid = date + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 2)
                        {
                            ssid = date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                        }
                        else if (Common.Sys_NoAdd == 3)
                        {
                            ssid = date + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 4)
                        {
                            ssid = date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                        }
                        tbNo.Text = ssid;
                    }
                    else
                    {
                        d = 1M;
                        while (true)
                        {
                            if (Common.Sys_NoAdd == 1)
                            {
                                ssid = date + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 2)
                            {
                                ssid = date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                            }
                            else if (Common.Sys_NoAdd == 3)
                            {
                                ssid = date + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 4)
                            {
                                ssid = date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                            }
                            if (t.AsEnumerable().Count(r => r[pkColumn].ToString().Trim() == ssid) > 0)
                            {
                                d++;
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        tbNo.Text = ssid;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                t.Dispose();
            }
        }

        static Stopwatch sw = null;
        public static void SwStart()
        {
            sw = null;
            sw = new Stopwatch();
            sw.Start();
        }
        public static void SwStop()
        {
            if (sw != null) MessageBox.Show(sw.ElapsedMilliseconds.ToString());
        }

        public static void CheckGridViewUdf(string frmName, ref JE.MyControl.DataGridViewT grid)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("表單名稱", frmName);
                cmd.CommandText = "Select Count(*) from tablestyle where 表單名稱=(@表單名稱)";

                //檢查使用者是否有自定記錄
                if (cmd.ExecuteScalar().ToDecimal() == 0) { return;/*無*/ }
                else
                {
                    //檢查程式設計師是否有新增欄位
                    bool AddColumn = false;
                    cmd.CommandText = " Select * from tablestyle where 表單名稱=(@表單名稱) order by 初始順序";
                    DataTable temp = new DataTable();
                    da.Fill(temp);
                    var list = temp.AsEnumerable().Select(r => r["資料行名稱"].ToString().Trim()).ToList();
                    for (int i = 0; i < grid.Columns.Count; i++)
                    {
                        if (list.IndexOf(grid.Columns[i].Name) != -1) continue;
                        else
                        {
                            AddColumn = true;

                            var index = grid.Columns[i].Index;
                            temp.AsEnumerable().Where(r => r["初始順序"].ToInteger() >= index).ToList().ForEach(c => c["初始順序"] = c["初始順序"].ToInteger() + 1);

                            var displayindex = grid.Columns[i].DisplayIndex;
                            temp.AsEnumerable().Where(r => r["自定順序"].ToInteger() >= displayindex).ToList().ForEach(c => c["自定順序"] = c["自定順序"].ToInteger() + 1);

                            DataRow row = temp.NewRow();
                            row["表單名稱"] = frmName;
                            row["使用者名稱"] = Common.User_Name;
                            row["資料行名稱"] = grid.Columns[i].Name;
                            row["綁定值名稱"] = grid.Columns[i].DataPropertyName.ToLower();
                            row["初始順序"] = grid.Columns[i].Index;
                            row["自定順序"] = grid.Columns[i].DisplayIndex;
                            row["初始文字"] = grid.Columns[i].HeaderText;
                            row["自定文字"] = grid.Columns[i].HeaderText;
                            row["初始寬度"] = ((DataGridViewTextBoxColumn)grid.Columns[i]).MaxInputLength;
                            row["自定寬度"] = ((DataGridViewTextBoxColumn)grid.Columns[i]).MaxInputLength;
                            row["自定顯示"] = grid.Columns[i].Visible.ToString();
                            temp.Rows.InsertAt(row, i);
                        }
                    }
                    if (AddColumn)
                    {
                        SqlTransaction tn = cn.BeginTransaction();
                        cmd.Transaction = tn;
                        try
                        {
                            cmd.CommandText = " delete from tablestyle where 表單名稱=(@表單名稱) ";
                            cmd.ExecuteNonQuery();

                            cmd.Parameters.AddWithValue("使用者名稱", Common.User_Name);
                            cmd.Parameters.AddWithValue("資料行名稱", "");
                            cmd.Parameters.AddWithValue("綁定值名稱", "");
                            cmd.Parameters.AddWithValue("初始順序", 0);
                            cmd.Parameters.AddWithValue("自定順序", 0);
                            cmd.Parameters.AddWithValue("初始文字", "");
                            cmd.Parameters.AddWithValue("自定文字", "");
                            cmd.Parameters.AddWithValue("初始寬度", 0);
                            cmd.Parameters.AddWithValue("自定寬度", 0);
                            cmd.Parameters.AddWithValue("自定顯示", "");
                            //
                            for (int i = 0; i < temp.Rows.Count; i++)
                            {
                                cmd.Parameters["資料行名稱"].Value = temp.Rows[i]["資料行名稱"];
                                cmd.Parameters["綁定值名稱"].Value = temp.Rows[i]["綁定值名稱"];
                                cmd.Parameters["初始順序"].Value = temp.Rows[i]["初始順序"];
                                cmd.Parameters["自定順序"].Value = temp.Rows[i]["自定順序"];
                                cmd.Parameters["初始文字"].Value = temp.Rows[i]["初始文字"];
                                cmd.Parameters["自定文字"].Value = temp.Rows[i]["自定文字"];
                                cmd.Parameters["初始寬度"].Value = temp.Rows[i]["初始寬度"];
                                cmd.Parameters["自定寬度"].Value = temp.Rows[i]["自定寬度"];
                                cmd.Parameters["自定顯示"].Value = temp.Rows[i]["自定顯示"];
                                cmd.CommandText = "Insert into tablestyle (表單名稱,使用者名稱,資料行名稱,綁定值名稱,初始順序,自定順序,初始文字,自定文字,初始寬度,自定寬度,自定顯示) values (@表單名稱,@使用者名稱,@資料行名稱,@綁定值名稱,@初始順序,@自定順序,@初始文字,@自定文字,@初始寬度,@自定寬度,@自定顯示)";
                                cmd.ExecuteNonQuery();
                            }
                            tn.Commit();
                        }
                        catch (Exception ex)
                        {
                            tn.Rollback();
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    //單據的datagrid載入tablestyle的設定
                    cmd.CommandText = " Select * from tablestyle where 表單名稱=(@表單名稱) order by 初始順序";
                    temp.Clear();
                    da.Fill(temp);
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        string[] tag = new string[3];
                        tag[0] = temp.Rows[i]["初始文字"].ToString();
                        tag[1] = temp.Rows[i]["初始寬度"].ToString();
                        tag[2] = temp.Rows[i]["自定顯示"].ToString();

                        var columnname = temp.Rows[i]["資料行名稱"].ToString().Trim();
                        if (grid.Columns[columnname] != null)
                        {
                            //原生欄位
                            grid.Columns[i].DisplayIndex = temp.Rows[i]["初始順序"].ToInteger();
                            grid.Columns[i].HeaderText = temp.Rows[i]["自定文字"].ToString().Trim();
                            ((DataGridViewTextBoxColumn)grid.Columns[i]).MaxInputLength = temp.Rows[i]["自定寬度"].ToInteger();
                            grid.Columns[i].Visible = (temp.Rows[i]["自定顯示"].ToString() == bool.TrueString);
                            grid.Columns[i].Tag = tag;
                        }
                        else
                        {
                            //使用者自定欄位
                            DataGridViewTextBoxColumn t = new DataGridViewTextBoxColumn();
                            t.Name = temp.Rows[i]["資料行名稱"].ToString();
                            t.DataPropertyName = temp.Rows[i]["綁定值名稱"].ToString().ToLower();

                            t.DisplayIndex = temp.Rows[i]["初始順序"].ToInteger();
                            t.HeaderText = temp.Rows[i]["自定文字"].ToString().Trim();
                            t.MaxInputLength = temp.Rows[i]["自定寬度"].ToInteger();
                            t.Visible = (temp.Rows[i]["自定顯示"].ToString() == bool.TrueString);
                            t.Tag = tag;

                            t.SortMode = DataGridViewColumnSortMode.NotSortable;
                            t.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                            grid.Columns.Add(t);
                        }
                    }
                    var temp1 = temp.AsEnumerable().OrderBy(r => r["自定順序"].ToInteger()).CopyToDataTable();
                    for (int i = 0; i < temp1.Rows.Count; i++)
                    {
                        grid.Columns[temp1.Rows[i]["資料行名稱"].ToString()].DisplayIndex = i;
                    }
                    List<string> A = new List<string>();
                    for (int i = 0; i < temp1.Rows.Count; i++)
                    {
                        if (temp1.Rows[i]["自定顯示"].ToString() == bool.FalseString)
                        {
                            grid.Columns[temp1.Rows[i]["資料行名稱"].ToString()].Visible = false;
                            A.Add(temp1.Rows[i]["資料行名稱"].ToString());
                        }
                    }
                }
            }
        }

        public static string 判斷開啟報表類型(string ReportName, string report = @"Report\")
        {
            bool hasReport = false;
            //先判斷有無FastRepot
            var testPath = Common.reportaddress + report.Replace("Report", "ReportNew") + ReportName + ".frx";
            hasReport = System.IO.File.Exists(testPath);
            if (hasReport == false)
            {
                //在判斷有無水晶報表
                return Common.reportaddress + report + ReportName + ".rpt";
            }
            else
            {
                return testPath;
            }
        }


    }
}
