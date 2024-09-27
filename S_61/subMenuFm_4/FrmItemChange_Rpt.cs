using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmItemChange_Rpt : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable tItem = new DataTable();
        DataTable table = new DataTable();
        DataTable Intable = new DataTable();//區間內
        DataTable Outtable = new DataTable();//區間外
        DataTable tbtemp = new DataTable();
        List<string> list;
        List<DataRow> stkroom;
        DataRow dr;
        SqlDataAdapter dd;
        string sql = "";
        bool Error = false;
        bool AllSel = true, NoSel = false;
        decimal sttrait;
        bool RD7 = false;//紀錄 成本計算是否勾選 銷貨金額

        public FrmItemChange_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmItemChange_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                date.MaxLength = date1.MaxLength = 7;
                date.Text = Date.GetDateTime(1, false);
                date.Text = date.Text.Remove(5) + "01";
                date1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                date.MaxLength = date1.MaxLength = 8;
                date.Text = Date.GetDateTime(2, false);
                date.Text = date.Text.Remove(6) + "01";
                date1.Text = Date.GetDateTime(2, false);
            }
            //date.Focus();
            this.ActiveControl = date;
        }

        bool compare(TextBox tb, TextBox tb1)
        {
            Error = false;
            if (tb.Text == "" || tb1.Text == "")
                return Error;
            if (string.CompareOrdinal(tb.Text.Trim(), tb1.Text.Trim()) > 0)
            {
                MessageBox.Show("起迄" + tb.Tag + "不可大於終止" + tb.Tag + "", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                Error = true;
            }
            return Error;
        }

        string GetStartDay()
        {
            if (Common.User_DateTime == 1)
                return Common.Sys_StkYear1 + "0101";
            else
                return Common.Sys_StkYear2 + "0101";
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (compare(date, date1)) return;
            if (compare(ItNo, ItNo1)) return;
            AllSel = true; NoSel = false;
            if (!ch1.Checked) AllSel = false;
            if (!ch2.Checked) AllSel = false;
            if (!ch3.Checked) AllSel = false;
            if (!ch4.Checked) AllSel = false;
            if (!ch1.Checked && !ch2.Checked && !ch3.Checked && !ch4.Checked) NoSel = true;
            if (NoSel)
            {
                MessageBox.Show("必須選擇倉庫類別", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

             

            btnBrow.Enabled = false;
            Intable.Clear();
            Outtable.Clear();
            sql = "";
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        string 倉庫類別 = " in(";
                        if (ch1.Checked) 倉庫類別 += "1,";
                        if (ch2.Checked) 倉庫類別 += "2,";
                        if (ch3.Checked) 倉庫類別 += "3,";
                        if (ch4.Checked) 倉庫類別 += "4,";
                        倉庫類別 = 倉庫類別.Substring(0, 倉庫類別.Length - 1) + ")";

                        dd = new SqlDataAdapter("select * from stkroom", cn);
                        DataTable stk = new DataTable();
                        dd.Fill(stk);
                        stkroom = stk.AsEnumerable().ToList();

                        #region 進貨區間內
                        sql = "select b.bsdate as 日期,b.bsdate1 as 日期1,b.bsno as 單號,b.itno,b.itname,a.faname1 as 廠商,單據='進貨+',b.qty as 數量,b.itunit,"
                            + " b.taxpriceb as 稅前價格,b.mnyb as 稅前金額,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=b.qty*b.itpkgqty*b.realcost,結餘總數=0.0,"
                            + " 結餘=b.qty*b.itpkgqty,b.stno as 倉庫1"
                            + " from bshopd as b left join bshop as a on a.bsno=b.bsno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where  s.sttrait" + 倉庫類別 + " and b.ittrait !=1";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.bsdate >=@bsdate";
                            sql += " and b.bsdate <=@bsdate1";
                        }
                        else
                        {
                            sql += " and b.bsdate1 >=@bsdate";
                            sql += " and b.bsdate1 <=@bsdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bsdate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 進貨區間外
                        sql = "select b.bsdate as 日期,b.bsdate1 as 日期1,b.bsno as 單號,b.itno,b.itname,a.faname1 as 廠商,單據='進貨+',b.qty as 數量,b.itunit,"
                            + " b.taxpriceb as 稅前價格,b.mnyb as 稅前金額,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘總數=0.0,"
                            + " 結餘= b.qty*b.itpkgqty"
                            + " from bshopd as b left join bshop as a on a.bsno=b.bsno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where s.sttrait" + 倉庫類別+" and b.ittrait not in (1)";//進貨區間外 組合品不算期初 
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.bsdate >= @bsdate";
                            sql += " and b.bsdate < @bsdate1";
                        }
                        else
                        {
                            sql += " and b.bsdate1 >= @bsdate";
                            sql += " and b.bsdate1 < @bsdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdate", GetStartDay());
                        cmd.Parameters.AddWithValue("bsdate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion


                        #region 進退區間內
                        sql = "select b.bsdate as 日期,b.bsdate1 as 日期1,b.bsno as 單號,b.itno,b.itname,a.faname1 as 廠商,單據='進退-',b.qty*(-1) as 數量,b.itunit,"
                            + " b.taxpriceb as 稅前價格,b.mnyb*(-1) as 稅前金額,b.itpkgqty,總數量=b.qty*b.itpkgqty*(-1),成本=b.qty*b.itpkgqty*(-1)*b.realcost,結餘總數=0.0,"
                            + " 結餘=b.qty*b.itpkgqty*(-1),b.stno as 倉庫1"
                            + " from rshopd as b left join rshop as a on a.bsno=b.bsno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where s.sttrait" + 倉庫類別 + " and b.ittrait !=1";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.bsdate >=@bsdate";
                            sql += " and b.bsdate <=@bsdate1";
                        }
                        else
                        {
                            sql += " and b.bsdate1 >=@bsdate";
                            sql += " and b.bsdate1 <=@bsdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bsdate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 進退區間外
                        sql = "select b.bsdate as 日期,b.bsdate1 as 日期1,b.bsno as 單號,b.itno,b.itname,a.faname1 as 廠商,單據='進退-',b.qty*(-1) as 數量,b.itunit,"
                            + " b.taxpriceb as 稅前價格,b.mnyb*(-1) as 稅前金額,b.itpkgqty,總數量=b.qty*b.itpkgqty*(-1),成本=0.0,結餘總數=0.0,"
                            + " 結餘=b.qty*b.itpkgqty*(-1)"
                            + " from rshopd as b left join rshop as a on a.bsno=b.bsno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where s.sttrait" + 倉庫類別+ " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.bsdate >= @bsdate";
                            sql += " and b.bsdate < @bsdate1";
                        }
                        else
                        {
                            sql += " and b.bsdate1 >= @bsdate";
                            sql += " and b.bsdate1 < @bsdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdate", GetStartDay());
                        cmd.Parameters.AddWithValue("bsdate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion


                        #region 銷貨區間內 組合品(不含子件)
                        //sql = "select b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='銷貨-',b.itunit,b.itpkgqty,"
                        //    + " 數量=sum(c.itqty/c.itpareprs*c.itpkgqty*b.itpkgqty*b.qty),總數量=0.0,結餘=0.0,結餘總數=0.0,"
                        //    + " b.taxpriceb as 稅前價格,b.mnyb as 稅前金額,成本=0.0"
                        //    + " from saled b left join sale a on a.sano=b.sano left join SaleBom as c on b.bomid=c.BomID left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                        //    + " where s.sttrait" + 倉庫類別+ " and b.ittrait=1";
                        //if (Common.User_DateTime == 1)
                        //{
                        //    sql += " and b.sadate >=@sadate";
                        //    sql += " and b.sadate <=@sadate1";
                        //}
                        //else
                        //{
                        //    sql += " and b.sadate1 >=@sadate";
                        //    sql += " and b.sadate1 <=@sadate1";
                        //}
                        //if (ItNo.Text != "")
                        //    sql += " and b.itno >=@itno";
                        //if (ItNo1.Text != "")
                        //    sql += " and b.itno <=@itno1";
                        //if (StNo.Text != "")
                        //    sql += " and b.stno =@stno";
                        //if (KiNo.Text != "")
                        //    sql += " and i.kino =@kino";
                        //sql += " group by b.sadate,b.sadate1,b.itno,b.itname,b.sano,a.cuname1,b.itunit,b.itpkgqty,b.taxpriceb,b.mnyb";

                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("sadate", date.Text.Trim());
                        //cmd.Parameters.AddWithValue("sadate1", date1.Text.Trim());
                        //if (ItNo.Text != "")
                        //    cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        //if (ItNo1.Text != "")
                        //    cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        //if (StNo.Text != "")
                        //    cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        //if (KiNo.Text != "")
                        //    cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        //cmd.CommandText = sql;

                        //dd = new SqlDataAdapter(cmd);
                        //table.Clear();
                        //dd.Fill(table);
                        //dd.Dispose();
                        //if (table.Rows.Count != 0)
                        //{
                        //    SetCost(table);
                        //    for (int i = 0; i < table.Rows.Count; i++)
                        //    {
                        //        table.Rows[i]["數量"] = 0.0;
                        //        table.Rows[i]["itpkgqty"] = 0.0;
                        //    }
                        //    Intable.Merge(table);
                        //    Intable.AcceptChanges();
                        //}
                        #endregion

                        #region 銷貨單據 區間內 組裝品&單一商品
                        sql = "select b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='銷貨-',b.itunit,結餘總數=0.0,"
                           + " b.qty*(-1) as 數量,b.itpkgqty,結餘=b.qty*(-1)*b.itpkgqty,b.ittrait,"
                           + " b.taxpriceb as 稅前價格,b.mnyb as 稅前金額,總數量=b.qty*b.itpkgqty*(-1),成本=0.0,b.stno as 倉庫1"
                           + " from saled as b left join sale as a on a.sano=b.sano left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                           + " where s.sttrait" + 倉庫類別+ " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.sadate >=@sadate";
                            sql += " and b.sadate <=@sadate1";
                        }
                        else
                        {
                            sql += " and b.sadate1 >=@sadate";
                            sql += " and b.sadate1 <=@sadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("sadate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 銷貨區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='銷貨組-',c.itunit,結餘總數=0.0,"
                            + " c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.qty as 數量,"
                            + " c.itpkgqty,"
                            + " 結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.qty,q.日期,q.日期1,q.單號,q.廠商,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.qty,成本=0.0,q.stno as 倉庫1 from "
                            + "(select b.qty,b.itpkgqty,b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.bomid,a.cuname1 as 廠商,b.stno "
                            + " from saled as b left join sale as a on a.sano=b.sano left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where s.sttrait" + 倉庫類別;
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.sadate >=@sadate";
                            sql += " and b.sadate <=@sadate1";
                        }
                        else
                        {
                            sql += " and b.sadate1 >=@sadate";
                            sql += " and b.sadate1 <=@sadate1";
                        }
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join SaleBom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("sadate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 銷貨單據 區間外 組裝品&單一商品
                        sql = "select b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='銷貨-',b.itunit,結餘總數=0.0,"
                           + " b.qty*(-1) as 數量,b.itpkgqty,結餘=b.qty*(-1)*b.itpkgqty,b.ittrait,"
                           + " b.taxpriceb as 稅前價格,b.mnyb as 稅前金額,總數量=b.qty*b.itpkgqty*(-1),成本=0.0"
                           + " from saled as b left join sale as a on a.sano=b.sano left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                           + " where s.sttrait" + 倉庫類別+ " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.sadate >= @sadate";
                            sql += " and b.sadate < @sadate1";
                        }
                        else
                        {
                            sql += " and b.sadate1 >= @sadate";
                            sql += " and b.sadate1 < @sadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", GetStartDay());
                        cmd.Parameters.AddWithValue("sadate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 銷貨區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='銷貨組-',c.itunit,結餘總數=0.0,"
                            + " c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.qty as 數量,"
                            + " c.itpkgqty,"
                            + " 結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.qty,q.日期,q.日期1,q.單號,q.廠商,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.bomid,a.cuname1 as 廠商 "
                            + " from saled as b left join sale as a on a.sano=b.sano left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where '0'='0'"
                            + " and s.sttrait" + 倉庫類別;
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.sadate >= @sadate";
                            sql += " and b.sadate < @sadate1";
                        }
                        else
                        {
                            sql += " and b.sadate1 >= @sadate";
                            sql += " and b.sadate1 < @sadate1";
                        }
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join SaleBom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", GetStartDay());
                        cmd.Parameters.AddWithValue("sadate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion


                        #region 銷退單據 區間內 組合品(不含子件)
                        //sql = "select b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='銷退+',b.itunit,b.itpkgqty,結餘總數=0.0,"
                        //    + " 數量=sum(c.itqty/c.itpareprs*c.itpkgqty*b.itpkgqty*b.qty),總數量=0.0,結餘=0.0,"
                        //    + " b.taxpriceb as 稅前價格,(-1)*b.mnyb as 稅前金額,成本=0.0"
                        //    + " from rsaled b left join rsale a on a.sano=b.sano left join RSaleBom as c on b.bomid=c.BomID left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                        //    + " where '0'='0'"
                        //    + " and s.sttrait" + 倉庫類別
                        //    + " and b.ittrait=1";
                        //if (Common.User_DateTime == 1)
                        //{
                        //    sql += " and b.sadate >=@sadate";
                        //    sql += " and b.sadate <=@sadate1";
                        //}
                        //else
                        //{
                        //    sql += " and b.sadate1 >=@sadate";
                        //    sql += " and b.sadate1 <=@sadate1";
                        //}
                        //if (ItNo.Text != "")
                        //    sql += " and b.itno >=@itno";
                        //if (ItNo1.Text != "")
                        //    sql += " and b.itno <=@itno1";
                        //if (StNo.Text != "")
                        //    sql += " and b.stno =@stno";
                        //if (KiNo.Text != "")
                        //    sql += " and i.kino =@kino";
                        //sql += " group by b.sadate,b.sadate1,b.itno,b.itname,b.sano,a.cuname1,b.itunit,b.itpkgqty,b.taxpriceb,b.mnyb";

                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("sadate", date.Text.Trim());
                        //cmd.Parameters.AddWithValue("sadate1", date1.Text.Trim());
                        //if (ItNo.Text != "")
                        //    cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        //if (ItNo1.Text != "")
                        //    cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        //if (StNo.Text != "")
                        //    cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        //if (KiNo.Text != "")
                        //    cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        //cmd.CommandText = sql;

                        //dd = new SqlDataAdapter(cmd);
                        //table.Clear();
                        //dd.Fill(table);
                        //dd.Dispose();
                        //if (table.Rows.Count != 0)
                        //{
                        //    SetCost(table);
                        //    for (int i = 0; i < table.Rows.Count; i++)
                        //    {
                        //        table.Rows[i]["數量"] = 0.0;
                        //        table.Rows[i]["itpkgqty"] = 0.0;
                        //    }
                        //    Intable.Merge(table);
                        //    Intable.AcceptChanges();
                        //}
                        #endregion

                        #region 銷退單據 區間內 組裝品&單一商品
                        sql = "select b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='銷退+',b.itunit,結餘總數=0.0,"
                           + " b.qty as 數量,b.itpkgqty,結餘=b.qty*b.itpkgqty,b.ittrait,"
                           + " b.taxpriceb as 稅前價格,b.mnyb*(-1) as 稅前金額,總數量=b.qty*b.itpkgqty,成本=0.0,b.stno as 倉庫1"
                           + " from rsaled as b left join rsale as a on a.sano=b.sano left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                           + " where s.sttrait" + 倉庫類別+ " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.sadate >=@sadate";
                            sql += " and b.sadate <=@sadate1";
                        }
                        else
                        {
                            sql += " and b.sadate1 >=@sadate";
                            sql += " and b.sadate1 <=@sadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("sadate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            銷退取得累進成本(ref table);
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 銷退單據 區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='銷退組+',c.itunit,結餘總數=0.0,"
                            + " c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,"
                            + " c.itpkgqty,"
                            + " 結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.廠商,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0,q.stno as 倉庫1 from "
                            + "(select b.qty,b.itpkgqty,b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.bomid,a.cuname1 as 廠商,b.stno "
                            + " from rsaled as b left join rsale as a on a.sano=b.sano left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where s.sttrait" + 倉庫類別;
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.sadate >=@sadate";
                            sql += " and b.sadate <=@sadate1";
                        }
                        else
                        {
                            sql += " and b.sadate1 >=@sadate";
                            sql += " and b.sadate1 <=@sadate1";
                        }
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join RSaleBom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("sadate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            銷退取得累進成本(ref table);
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 銷貨單據 區間外 組裝品&單一商品
                        sql = "select b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='銷退+',b.itunit,結餘總數=0.0,"
                           + " b.qty as 數量,b.itpkgqty,結餘=b.qty*b.itpkgqty,b.ittrait,"
                           + " b.taxpriceb as 稅前價格,(-1)*b.mnyb as 稅前金額,總數量=b.qty*b.itpkgqty,成本=0.0"
                           + " from rsaled as b left join rsale as a on a.sano=b.sano left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                           + " where s.sttrait" + 倉庫類別+ " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.sadate >= @sadate";
                            sql += " and b.sadate < @sadate1";
                        }
                        else
                        {
                            sql += " and b.sadate1 >= @sadate";
                            sql += " and b.sadate1 < @sadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", GetStartDay());
                        cmd.Parameters.AddWithValue("sadate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //etCost(table);
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 銷貨區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='銷貨退-',c.itunit,結餘總數=0.0,"
                            + " c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,"
                            + " c.itpkgqty,"
                            + " 結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.廠商,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.sadate as 日期,b.sadate1 as 日期1,b.sano as 單號,b.bomid,a.cuname1 as 廠商 "
                            + " from rsaled as b left join rsale as a on a.sano=b.sano left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where s.sttrait" + 倉庫類別;
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.sadate >= @sadate";
                            sql += " and b.sadate < @sadate1";
                        }
                        else
                        {
                            sql += " and b.sadate1 >= @sadate";
                            sql += " and b.sadate1 < @sadate1";
                        }
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join RSaleBom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", GetStartDay());
                        cmd.Parameters.AddWithValue("sadate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion




                        #region 調整單據 區間內 組裝&單一商品
                        sql = "select b.itno,b.itname,b.addate as 日期,b.addate1 as 日期1,b.adno as 單號,單據='調整+',b.qty as 數量,b.itunit,b.costb as 稅前價格,結餘總數=0.0,"
                            + " b.mnyb as 稅前金額,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=b.qty*b.itpkgqty*b.costb,結餘=b.qty*b.itpkgqty,b.stno as 倉庫1"
                            + " from adjustd as b left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where s.sttrait" + 倉庫類別+ " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.addate >=@addate";
                            sql += " and b.addate <=@addate1";
                        }
                        else
                        {
                            sql += " and b.addate1 >=@addate";
                            sql += " and b.addate1 <=@addate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("addate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("addate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 調整單據 區間外 組裝&單一商品
                        sql = "select b.itno,b.itname,b.addate as 日期,b.addate1 as 日期1,b.adno as 單號,單據='調整+',b.qty as 數量,b.itunit,b.costb as 稅前價格,結餘總數=0.0,"
                            + " b.mnyb as 稅前金額,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty"
                            + " from adjustd as b left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where s.sttrait" + 倉庫類別+ " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.addate >= @addate";
                            sql += " and b.addate < @addate1";
                        }
                        else
                        {
                            sql += " and b.addate1 >= @addate";
                            sql += " and b.addate1 < @addate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("addate", GetStartDay());
                        cmd.Parameters.AddWithValue("addate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 領料單據 區間內 組裝&單一商品
                        sql = "select b.itno,b.itname,b.drdate as 日期,b.drdate1 as 日期1,b.drno as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                           + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoo as 倉庫1,b.stnoi as 倉庫2"
                           + " from drawd as b left join draw as a on a.drno=b.drno left join item as i on i.itno=b.itno"
                           + " where b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.drdate >=@drdate";
                            sql += " and a.drdate <=@drdate1";
                        }
                        else
                        {
                            sql += " and a.drdate1 >=@drdate";
                            sql += " and a.drdate1 <=@drdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drdate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("drdate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckDraw("draw", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 領料單據 區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.drdate as 日期,b.drdate1 as 日期1,b.drno as 單號,b.bomid,b.stnoo as 倉庫1,b.stnoi as 倉庫2 "
                            + " from drawd as b left join draw as a on a.drno=b.drno left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.drdate >=@drdate";
                            sql += " and b.drdate <=@drdate1";
                        }
                        else
                        {
                            sql += " and b.drdate1 >=@drdate";
                            sql += " and b.drdate1 <=@drdate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join drawbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drdate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("drdate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckDraw("drawbom", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 領料單據 區間外 組裝&單一商品
                        sql = "select b.itno,b.itname,b.drdate as 日期,b.drdate1 as 日期1,b.drno as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                            + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoo as 倉庫1,b.stnoi as 倉庫2"
                            + " from drawd as b left join draw as a on a.drno=b.drno left join item as i on i.itno=b.itno "
                            + " where '0'='0'"
                            + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.drdate >= @drdate";
                            sql += " and a.drdate < @drdate1";
                        }
                        else
                        {
                            sql += " and a.drdate1 >= @drdate";
                            sql += " and a.drdate1 < @drdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drdate", GetStartDay());
                        cmd.Parameters.AddWithValue("drdate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckDraw("draw", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 領料單據 區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.drdate as 日期,b.drdate1 as 日期1,b.drno as 單號,b.bomid,b.stnoo as 倉庫1,b.stnoi as 倉庫2 "
                            + " from drawd as b left join draw as a on a.drno=b.drno left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.drdate >= @drdate";
                            sql += " and b.drdate < @drdate1";
                        }
                        else
                        {
                            sql += " and b.drdate1 >= @drdate";
                            sql += " and b.drdate1 < @drdate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join drawbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drdate", GetStartDay());
                        cmd.Parameters.AddWithValue("drdate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckDraw("drawbom", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 入庫單據 區間內 單一商品&組裝品(不含子件)
                        sql = "select b.itno,b.itname,b.gadate as 日期,b.gadate1 as 日期1,b.gano as 單號,單據='',b.qty as 數量,b.itunit,稅前價格=0.0,結餘總數=0.0,"
                           + " 稅前金額=0.0,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=b.qty*b.itpkgqty*b.costb,結餘=b.qty*b.itpkgqty,b.stnoi as 倉庫1,b.stnoo as 倉庫2,b.ittrait"
                           + " from garnerd as b left join garner as a on a.gano=b.gano left join item as i on i.itno=b.itno "
                           + " where '0'='0'"
                           + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.gadate >=@gadate";
                            sql += " and a.gadate <=@gadate1";
                        }
                        else
                        {
                            sql += " and a.gadate1 >=@gadate";
                            sql += " and a.gadate1 <=@gadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("gadate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckGarner(table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 入庫單據 區間內 組裝品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.gadate as 日期,b.gadate1 as 日期1,b.gano as 單號,b.bomid,b.stnoi as 倉庫1,b.stnoo as 倉庫2 "
                            + " from garnerd as b left join garner as a on a.gano=b.gano left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.gadate >=@gadate";
                            sql += " and b.gadate <=@gadate1";
                        }
                        else
                        {
                            sql += " and b.gadate1 >=@gadate";
                            sql += " and b.gadate1 <=@gadate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 2 )";
                        sql += " as q left join garnbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("gadate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckGarnbom(table);
                            for (int i = 0; i < tbtemp.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 入庫單據 區間外 單一商品&組裝品(不含子件)
                        sql = "select b.itno,b.itname,b.gadate as 日期,b.gadate1 as 日期1,b.gano as 單號,單據='',b.qty as 數量,b.itunit,稅前價格=0.0,結餘總數=0.0,"
                           + " 稅前金額=0.0,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoi as 倉庫1,b.stnoo as 倉庫2,b.ittrait"
                           + " from garnerd as b left join garner as a on a.gano=b.gano left join item as i on i.itno=b.itno "
                           + " where '0'='0'"
                           + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.gadate >= @gadate";
                            sql += " and a.gadate < @gadate1";
                        }
                        else
                        {
                            sql += " and a.gadate1 >= @gadate";
                            sql += " and a.gadate1 < @gadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gadate", GetStartDay());
                        cmd.Parameters.AddWithValue("gadate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckGarner(table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 入庫單據 區間外 組裝品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.gadate as 日期,b.gadate1 as 日期1,b.gano as 單號,b.bomid,b.stnoi as 倉庫1,b.stnoo as 倉庫2 "
                            + " from garnerd as b left join garner as a on a.gano=b.gano left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.gadate >= @gadate";
                            sql += " and b.gadate < @gadate1";
                        }
                        else
                        {
                            sql += " and b.gadate1 >= @gadate";
                            sql += " and b.gadate1 < @gadate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 2 )";
                        sql += " as q left join garnbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gadate", GetStartDay());
                        cmd.Parameters.AddWithValue("gadate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckGarnbom(table);
                            for (int i = 0; i < tbtemp.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion





                        #region 調撥單據 區間內 組裝&單一商品
                        sql = "select b.itno,b.itname,b.aldate as 日期,b.aldate1 as 日期1,b.alno as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                           + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoo as 倉庫1,b.stnoi as 倉庫2"
                           + " from allotd as b left join allot as a on a.alno=b.alno left join item as i on i.itno=b.itno "
                           + " where b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.aldate >=@aldate";
                            sql += " and a.aldate <=@aldate1";
                        }
                        else
                        {
                            sql += " and a.aldate1 >=@aldate";
                            sql += " and a.aldate1 <=@aldate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("aldate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("aldate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckAllot(table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 調撥單據 區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.aldate as 日期,b.aldate1 as 日期1,b.alno as 單號,b.bomid,b.stnoo as 倉庫1,b.stnoi as 倉庫2 "
                            + " from allotd as b left join allot as a on a.alno=b.alno left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.aldate >=@aldate";
                            sql += " and b.aldate <=@aldate1";
                        }
                        else
                        {
                            sql += " and b.aldate1 >=@aldate";
                            sql += " and b.aldate1 <=@aldate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino = @kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join AlloBom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("aldate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("aldate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckAllot(table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 調撥單據 區間外 組裝&單一商品
                        sql = @"select b.itno,b.itname,b.aldate as 日期,b.aldate1 as 日期1,b.alno as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,
                            稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoo as 倉庫1,b.stnoi as 倉庫2
                            from allotd as b left join allot as a on a.alno=b.alno left join item as i on i.itno=b.itno 
                            where  b.ittrait not in (1)";

                        //Note:原本此sql的where 變數是用參數，但不知道為什麼，相同的字串，丟參數跟直接寫值撈出來的結果就是會不一樣，且直接給值的是正確的解果，所以這邊將WHERE條件直接給值。  [版本:138e8]
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.aldate >= @aldate";
                            sql += " and a.aldate < @aldate1";
                        }
                        else
                        {
                            sql += " and a.aldate1 >= @aldate";
                            sql += " and a.aldate1 < @aldate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";


                        //if (Common.User_DateTime == 1)
                        //{
                        //    sql += " and a.aldate >= '" + GetStartDay() +"' ";
                        //    sql += " and a.aldate < '" + date.Text.Trim() + "' ";
                        //}
                        //else
                        //{
                        //    sql += " and a.aldate1 >= '" + GetStartDay() + "' ";
                        //    sql += " and a.aldate1 < '" + date.Text.Trim() + "' ";
                        //}
                        //if (ItNo.Text != "")
                        //    sql += " and b.itno >= '" + ItNo.Text.Trim() + "' ";
                        //if (ItNo1.Text != "")
                        //    sql += " and b.itno <= '" + ItNo1.Text.Trim() + "' ";
                        //if (KiNo.Text != "")
                        //    sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("aldate", GetStartDay());
                        cmd.Parameters.AddWithValue("aldate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());

                        cmd.CommandText = sql;
                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckAllot(table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 調撥單據 區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.aldate as 日期,b.aldate1 as 日期1,b.alno as 單號,b.bomid,b.stnoo as 倉庫1,b.stnoi as 倉庫2 "
                            + " from allotd as b left join allot as a on a.alno=b.alno left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.aldate >=@aldate";
                            sql += " and b.aldate < @aldate1";
                        }
                        else
                        {
                            sql += " and b.aldate1 >=@aldate";
                            sql += " and b.aldate1 < @aldate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino = @kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join AlloBom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("aldate", GetStartDay());
                        cmd.Parameters.AddWithValue("aldate1", date.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckAllot(table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion



                        //
                        #region 寄庫單據 區間內 組裝品&單一商品
                        sql = "select b.indate as 日期,b.indate1 as 日期1,b.inno as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='寄庫+',b.itunit,結餘總數=0.0,"
                           + " b.inqty as 數量,b.itpkgqty,結餘=b.inqty*b.itpkgqty,b.ittrait,"
                           + " b.taxpriceb as 稅前價格,b.mnyb as 稅前金額,總數量=b.inqty*b.itpkgqty,成本=0.0,b.stno as 倉庫1"
                           + " from instkd as b left join instk as a on a.inno=b.inno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                           + " where s.sttrait" + 倉庫類別+ " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.indate >=@indate";
                            sql += " and b.indate <=@indate1";
                        }
                        else
                        {
                            sql += " and b.indate1 >=@indate";
                            sql += " and b.indate1 <=@indate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("indate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("indate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 寄庫區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='寄庫組+',c.itunit,結餘總數=0.0,"
                            + " c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.inqty as 數量,"
                            + " c.itpkgqty,"
                            + " 結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.inqty,q.日期,q.日期1,q.單號,q.廠商,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.inqty,成本=0.0,q.stno as 倉庫1 from "
                            + "(select b.inqty,b.itpkgqty,b.indate as 日期,b.indate1 as 日期1,b.inno as 單號,b.bomid,a.cuname1 as 廠商,b.stno "
                            + " from instkd as b left join instk as a on a.inno=b.inno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where '0'='0'"
                            + " and s.sttrait" + 倉庫類別;
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.indate >=@indate";
                            sql += " and b.indate <=@indate1";
                        }
                        else
                        {
                            sql += " and b.indate1 >=@indate";
                            sql += " and b.indate1 <=@indate1";
                        }
                        if (StNo.Text != "")
                            sql += " and b.stno = @stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino = @kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join instkbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("indate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("indate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 寄庫單據 區間外 組裝品&單一商品
                        sql = "select b.indate as 日期,b.indate1 as 日期1,b.inno as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='寄庫+',b.itunit,結餘總數=0.0,"
                           + " b.inqty as 數量,b.itpkgqty,結餘=b.inqty*b.itpkgqty,b.ittrait,"
                           + " b.taxpriceb as 稅前價格,b.mnyb as 稅前金額,總數量=b.inqty*b.itpkgqty,成本=0.0"
                           + " from instkd as b left join instk as a on a.inno=b.inno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                           + " where '0'='0'"
                           + " and s.sttrait" + 倉庫類別
                           + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.indate >= @indate";
                            sql += " and b.indate < @indate1";
                        }
                        else
                        {
                            sql += " and b.indate1 >= @indate";
                            sql += " and b.indate1 < @indate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("indate", GetStartDay());
                        cmd.Parameters.AddWithValue("indate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 寄庫區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='寄庫組+',c.itunit,結餘總數=0.0,"
                            + " c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.inqty as 數量,"
                            + " c.itpkgqty,"
                            + " 結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.inqty,q.日期,q.日期1,q.單號,q.廠商,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.inqty,成本=0.0 from "
                            + "(select b.inqty,b.itpkgqty,b.indate as 日期,b.indate1 as 日期1,b.inno as 單號,b.bomid,a.cuname1 as 廠商 "
                            + " from instkd as b left join instk as a on a.inno=b.inno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where '0'='0'"
                            + " and s.sttrait" + 倉庫類別;
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.indate >= @indate";
                            sql += " and b.indate < @indate1";
                        }
                        else
                        {
                            sql += " and b.indate1 >= @indate";
                            sql += " and b.indate1 < @indate1";
                        }
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join instkbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("indate", GetStartDay());
                        cmd.Parameters.AddWithValue("indate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion


                        //
                        #region 領庫單據 區間內 組裝品&單一商品
                        sql = "select b.oudate as 日期,b.oudate1 as 日期1,b.ouno as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='領庫-',b.itunit,結餘總數=0.0,"
                           + " b.ouqty*(-1) as 數量,b.itpkgqty,結餘=b.ouqty*(-1)*b.itpkgqty,b.ittrait,"
                           + " b.taxpriceb as 稅前價格,b.mnyb as 稅前金額,總數量=b.ouqty*b.itpkgqty*(-1),成本=0.0,b.stno as 倉庫1"
                           + " from oustkd as b left join oustk as a on a.ouno=b.ouno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                           + " where s.sttrait" + 倉庫類別+ " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.oudate >=@oudate";
                            sql += " and b.oudate <=@oudate1";
                        }
                        else
                        {
                            sql += " and b.oudate1 >=@oudate";
                            sql += " and b.oudate1 <=@oudate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("oudate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("oudate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 領庫區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='領庫組-',c.itunit,結餘總數=0.0,"
                            + " c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.ouqty as 數量,"
                            + " c.itpkgqty,"
                            + " 結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.ouqty,q.日期,q.日期1,q.單號,q.廠商,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.ouqty,成本=0.0,q.stno as 倉庫1 from "
                            + "(select b.ouqty,b.itpkgqty,b.oudate as 日期,b.oudate1 as 日期1,b.ouno as 單號,b.bomid,a.cuname1 as 廠商,b.stno "
                            + " from oustkd as b left join oustk as a on a.ouno=b.ouno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where s.sttrait" + 倉庫類別;
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.oudate >=@oudate";
                            sql += " and b.oudate <=@oudate1";
                        }
                        else
                        {
                            sql += " and b.oudate1 >=@oudate";
                            sql += " and b.oudate1 <=@oudate1";
                        }
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join oustkbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("oudate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("oudate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Intable.Merge(table);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 領庫單據 區間外 組裝品&單一商品
                        sql = "select b.oudate as 日期,b.oudate1 as 日期1,b.ouno as 單號,b.itno,b.itname,a.cuname1 as 廠商,單據='領庫-',b.itunit,結餘總數=0.0,"
                           + " b.ouqty*(-1) as 數量,b.itpkgqty,結餘=b.ouqty*(-1)*b.itpkgqty,b.ittrait,"
                           + " b.taxpriceb as 稅前價格,b.mnyb as 稅前金額,總數量=b.ouqty*b.itpkgqty*(-1),成本=0.0"
                           + " from oustkd as b left join oustk as a on a.ouno=b.ouno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                           + " where '0'='0'"
                           + " and s.sttrait" + 倉庫類別
                           + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.oudate >= @oudate";
                            sql += " and b.oudate < @oudate1";
                        }
                        else
                        {
                            sql += " and b.oudate1 >= @oudate";
                            sql += " and b.oudate1 < @oudate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno = @stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino = @kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("oudate", GetStartDay());
                        cmd.Parameters.AddWithValue("oudate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 領庫區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='領庫組-',c.itunit,結餘總數=0.0,"
                            + " c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.ouqty as 數量,"
                            + " c.itpkgqty,"
                            + " 結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.ouqty,q.日期,q.日期1,q.單號,q.廠商,"
                            + "稅前價格=0.0,稅前金額=0.0,"
                            + "總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*(-1)*q.ouqty,成本=0.0 from "
                            + "(select b.ouqty,b.itpkgqty,b.oudate as 日期,b.oudate1 as 日期1,b.ouno as 單號,b.bomid,a.cuname1 as 廠商 "
                            + " from oustkd as b left join oustk as a on a.ouno=b.ouno left join item as i on i.itno=b.itno left join stkroom as s on b.stno=s.stno"
                            + " where '0'='0'"
                            + " and s.sttrait" + 倉庫類別;
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.oudate >= @oudate";
                            sql += " and b.oudate < @oudate1";
                        }
                        else
                        {
                            sql += " and b.oudate1 >= @oudate";
                            sql += " and b.oudate1 < @oudate1";
                        }
                        if (StNo.Text != "")
                            sql += " and b.stno =@stno";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join oustkbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("oudate", GetStartDay());
                        cmd.Parameters.AddWithValue("oudate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            //SetCost(table);
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                table.Rows[i]["itpkgqty"] = 1;
                            }
                            Outtable.Merge(table);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        //
                        #region 借出單據 區間內 組裝&單一商品
                        sql = "select b.itno,b.itname,b.ledate as 日期,b.ledate1 as 日期1,b.leno as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                           + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoi as 倉庫1,b.stno as 倉庫2"
                           + " from lendd as b left join lend as a on a.leno=b.leno left join item as i on i.itno=b.itno"
                           + " where '0'='0'"
                           + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.ledate >=@ledate";
                            sql += " and a.ledate <=@ledate1";
                        }
                        else
                        {
                            sql += " and a.ledate1 >=@ledate";
                            sql += " and a.ledate1 <=@ledate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("ledate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckLend("lend", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 借出單據 區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.ledate as 日期,b.ledate1 as 日期1,b.leno as 單號,b.bomid,b.stnoi as 倉庫1,b.stno as 倉庫2 "
                            + " from lendd as b left join lend as a on a.leno=b.leno left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.ledate >=@ledate";
                            sql += " and b.ledate <=@ledate1";
                        }
                        else
                        {
                            sql += " and b.ledate1 >=@ledate";
                            sql += " and b.ledate1 <=@ledate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino = @kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join lendbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("ledate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckLend("lendbom", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 借出單據 區間外 組裝&單一商品
                        sql = "select b.itno,b.itname,b.ledate as 日期,b.ledate1 as 日期1,b.leno as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                            + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoi as 倉庫1,b.stno as 倉庫2"
                            + " from lendd as b left join lend as a on a.leno=b.leno left join item as i on i.itno=b.itno "
                            + " where '0'='0'"
                            + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.ledate >= @ledate";
                            sql += " and a.ledate < @ledate1";
                        }
                        else
                        {
                            sql += " and a.ledate1 >= @ledate";
                            sql += " and a.ledate1 < @ledate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", GetStartDay());
                        cmd.Parameters.AddWithValue("ledate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckLend("lend", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 借出單據 區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.ledate as 日期,b.ledate1 as 日期1,b.leno as 單號,b.bomid,b.stnoi as 倉庫1,b.stno as 倉庫2 "
                            + " from lendd as b left join lend as a on a.leno=b.leno left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.ledate >= @ledate";
                            sql += " and b.ledate < @ledate1";
                        }
                        else
                        {
                            sql += " and b.ledate1 >= @ledate";
                            sql += " and b.ledate1 < @ledate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join lendbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", GetStartDay());
                        cmd.Parameters.AddWithValue("ledate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckLend("lendbom", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion



                        //
                        #region 借出還入單據 區間內 組裝&單一商品
                        sql = "select b.itno,b.itname,b.ledate as 日期,b.ledate1 as 日期1,b.leno as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                           + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoi as 倉庫1,b.stno as 倉庫2"
                           + " from rlendd as b left join rlend as a on a.leno=b.leno left join item as i on i.itno=b.itno"
                           + " where '0'='0'"
                           + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.ledate >=@ledate";
                            sql += " and a.ledate <=@ledate1";
                        }
                        else
                        {
                            sql += " and a.ledate1 >=@ledate";
                            sql += " and a.ledate1 <=@ledate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("ledate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            rrrrrCkeckLend("rlend", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 借出還入單據 區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.ledate as 日期,b.ledate1 as 日期1,b.leno as 單號,b.bomid,b.stnoi as 倉庫1,b.stno as 倉庫2 "
                            + " from rlendd as b left join rlend as a on a.leno=b.leno left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.ledate >=@ledate";
                            sql += " and b.ledate <=@ledate1";
                        }
                        else
                        {
                            sql += " and b.ledate1 >=@ledate";
                            sql += " and b.ledate1 <=@ledate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino = @kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join rlendbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("ledate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            rrrrrCkeckLend("rlendbom", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 借出還入單據 區間外 組裝&單一商品
                        sql = "select b.itno,b.itname,b.ledate as 日期,b.ledate1 as 日期1,b.leno as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                            + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoi as 倉庫1,b.stno as 倉庫2"
                            + " from rlendd as b left join rlend as a on a.leno=b.leno left join item as i on i.itno=b.itno "
                            + " where '0'='0'"
                            + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.ledate >= @ledate";
                            sql += " and a.ledate < @ledate1";
                        }
                        else
                        {
                            sql += " and a.ledate1 >= @ledate";
                            sql += " and a.ledate1 < @ledate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", GetStartDay());
                        cmd.Parameters.AddWithValue("ledate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            rrrrrCkeckLend("rlend", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 借出還入單據 區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.ledate as 日期,b.ledate1 as 日期1,b.leno as 單號,b.bomid,b.stnoi as 倉庫1,b.stno as 倉庫2 "
                            + " from rlendd as b left join rlend as a on a.leno=b.leno left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.ledate >= @ledate";
                            sql += " and b.ledate < @ledate1";
                        }
                        else
                        {
                            sql += " and b.ledate1 >= @ledate";
                            sql += " and b.ledate1 < @ledate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join rlendbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", GetStartDay());
                        cmd.Parameters.AddWithValue("ledate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            rrrrrCkeckLend("rlendbom", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion
                        //


                        //
                        #region 借入單據 區間內 組裝&單一商品
                        sql = "select b.itno,b.itname,b.bodate as 日期,b.bodate1 as 日期1,b.bono as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                           + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoo as 倉庫1,b.stno as 倉庫2"
                           + " from borrd as b left join borr as a on a.bono=b.bono left join item as i on i.itno=b.itno"
                           + " where '0'='0'"
                           + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.bodate >=@bodate";
                            sql += " and a.bodate <=@bodate1";
                        }
                        else
                        {
                            sql += " and a.bodate1 >=@bodate";
                            sql += " and a.bodate1 <=@bodate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckBorr("borr", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 借入單據 區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.bodate as 日期,b.bodate1 as 日期1,b.bono as 單號,b.bomid,b.stnoo as 倉庫1,b.stno as 倉庫2 "
                            + " from borrd as b left join borr as a on a.bono=b.bono left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.bodate >=@bodate";
                            sql += " and b.bodate <=@bodate1";
                        }
                        else
                        {
                            sql += " and b.bodate1 >=@bodate";
                            sql += " and b.bodate1 <=@bodate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join borrbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckBorr("borrbom", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 借入單據 區間外 組裝&單一商品
                        sql = "select b.itno,b.itname,b.bodate as 日期,b.bodate1 as 日期1,b.bono as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                            + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoo as 倉庫1,b.stno as 倉庫2"
                            + " from borrd as b left join borr as a on a.bono=b.bono left join item as i on i.itno=b.itno "
                            + " where '0'='0'"
                            + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.bodate >= @bodate";
                            sql += " and a.bodate < @bodate1";
                        }
                        else
                        {
                            sql += " and a.bodate1 >= @bodate";
                            sql += " and a.bodate1 < @bodate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", GetStartDay());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckBorr("borr", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 借入單據 區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.bodate as 日期,b.bodate1 as 日期1,b.bono as 單號,b.bomid,b.stnoo as 倉庫1,b.stno as 倉庫2 "
                            + " from borrd as b left join borr as a on a.bono=b.bono left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.bodate >= @bodate";
                            sql += " and b.bodate < @bodate1";
                        }
                        else
                        {
                            sql += " and b.bodate1 >= @bodate";
                            sql += " and b.bodate1 < @bodate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join borrbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", GetStartDay());
                        cmd.Parameters.AddWithValue("bodate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            CkeckBorr("borrbom", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion


                        //
                        #region 借入還出單據 區間內 組裝&單一商品
                        sql = "select b.itno,b.itname,b.bodate as 日期,b.bodate1 as 日期1,b.bono as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                           + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoo as 倉庫1,b.stno as 倉庫2"
                           + " from rborrd as b left join rborr as a on a.bono=b.bono left join item as i on i.itno=b.itno"
                           + " where '0'='0'"
                           + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.bodate >=@bodate";
                            sql += " and a.bodate <=@bodate1";
                        }
                        else
                        {
                            sql += " and a.bodate1 >=@bodate";
                            sql += " and a.bodate1 <=@bodate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "")
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            rrrrrCkeckBorr("rborr", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 借入還出單據 區間內 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.bodate as 日期,b.bodate1 as 日期1,b.bono as 單號,b.bomid,b.stnoo as 倉庫1,b.stno as 倉庫2 "
                            + " from rborrd as b left join rborr as a on a.bono=b.bono left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.bodate >=@bodate";
                            sql += " and b.bodate <=@bodate1";
                        }
                        else
                        {
                            sql += " and b.bodate1 >=@bodate";
                            sql += " and b.bodate1 <=@bodate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join rborrbom as c on q.bomid = c.BomID where q.bomid=c.bomid ";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());
                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            rrrrrCkeckBorr("rborrbom", table);
                            Intable.Merge(tbtemp);
                            Intable.AcceptChanges();
                        }
                        #endregion

                        #region 借入還出單據 區間外 組裝&單一商品
                        sql = "select b.itno,b.itname,b.bodate as 日期,b.bodate1 as 日期1,b.bono as 單號,單據='',b.qty as 數量,b.itunit,i.itbuypri as 稅前價格,結餘總數=0.0,"
                            + " 稅前金額=i.itbuypri*b.qty,b.itpkgqty,總數量=b.qty*b.itpkgqty,成本=0.0,結餘=b.qty*b.itpkgqty,b.stnoo as 倉庫1,b.stno as 倉庫2"
                            + " from rborrd as b left join rborr as a on a.bono=b.bono left join item as i on i.itno=b.itno "
                            + " where '0'='0'"
                            + " and b.ittrait not in (1)";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.bodate >= @bodate";
                            sql += " and a.bodate < @bodate1";
                        }
                        else
                        {
                            sql += " and a.bodate1 >= @bodate";
                            sql += " and a.bodate1 < @bodate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and b.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and b.itno <=@itno1";
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", GetStartDay());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            rrrrrCkeckBorr("rborr", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion

                        #region 借入還出單據 區間外 組合品子件
                        sql = "select c.itno,c.itname,單據='',c.itunit, c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty as 數量,結餘總數=0.0,"
                            + "c.itpkgqty,結餘=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,q.日期,q.日期1,q.單號,q.倉庫1,q.倉庫2,"
                            + "稅前價格=0.0,稅前金額=0.0,總數量=c.itqty/c.itpareprs*c.itpkgqty*q.itpkgqty*q.qty,成本=0.0 from "
                            + "(select b.qty,b.itpkgqty,b.bodate as 日期,b.bodate1 as 日期1,b.bono as 單號,b.bomid,b.stnoo as 倉庫1,b.stno as 倉庫2 "
                            + " from rborrd as b left join rborr as a on a.bono=b.bono left join item as i on i.itno=b.itno "
                            + " where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and b.bodate >= @bodate";
                            sql += " and b.bodate < @bodate1";
                        }
                        else
                        {
                            sql += " and b.bodate1 >= @bodate";
                            sql += " and b.bodate1 < @bodate1";
                        }
                        if (KiNo.Text != "")
                            sql += " and i.kino =@kino";
                        sql += " and b.ittrait = 1 )";
                        sql += " as q left join rborrbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", GetStartDay());
                        cmd.Parameters.AddWithValue("bodate1", date.Text.Trim());

                        if (ItNo.Text != "")
                            cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "")
                            cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (KiNo.Text != "")
                            cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        table.Clear();
                        dd.Fill(table);
                        dd.Dispose();
                        if (table.Rows.Count != 0)
                        {
                            rrrrrCkeckBorr("rborrbom", table);
                            Outtable.Merge(tbtemp);
                            Outtable.AcceptChanges();
                        }
                        #endregion


                        //期初
                        //先判斷表單類型
                        if (rd1.Checked)
                        {
                            #region rd1
                            //表單為匯總表
                            //期初
                            sql = "select s.itno,s.itname,s.itqtyf as 結餘,i.itunit,期初數量=0.0,期初成本=0.0,期初金額=0.0,進貨數量=0.0,進貨平均單價=0.0,"
                               + " 進貨金額=0.0,銷貨數量=0.0,銷貨成本=0.0,領料數量=0.0,入料數量=0.0,入庫數量=0.0,扣料數量=0.0,調整數量=0.0,撥入數量=0.0,撥出數量=0.0,借出數量=0.0,還入數量=0.0,借入數量=0.0,還出數量=0.0,結餘數量=0.0,結餘單位成本=0.0,結餘金額=0.0,寄庫數量=0.0,寄庫成本=0.0,領庫數量=0.0,領庫成本=0.0,廠商='',進價=0.0"
                               + " from stock as s left join item as i on i.itno=s.itno "
                               + " where '0'='0'"
                               + " and s.sttrait" + 倉庫類別;
                            if (ItNo.Text != "")
                                sql += " and s.itno >=@itno";
                            if (ItNo1.Text != "")
                                sql += " and s.itno <=@itno1";
                            if (StNo.Text != "")
                                sql += " and s.stno =@stno";
                            if (KiNo.Text != "")
                                sql += " and i.kino =@kino";
                            if (rd8.Checked)
                                sql += " and s.itqtyf not in (0)";

                            cmd.Parameters.Clear();
                            if (ItNo.Text != "")
                                cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                            if (ItNo1.Text != "")
                                cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                            if (StNo.Text != "")
                                cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                            if (KiNo.Text != "")
                                cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                            cmd.CommandText = sql;

                            dd = new SqlDataAdapter(cmd);
                            table.Clear();
                            dd.Fill(table);
                            dd.Dispose();
                            if (table.Rows.Count != 0)
                            {
                                Outtable.Merge(table);
                                Outtable.AcceptChanges();
                            }

                            if (Intable.Rows.Count == 0 && Outtable.Rows.Count == 0)
                            {
                                MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                date.Focus();
                                btnBrow.Enabled = true;
                                return;
                            }

                            if (Outtable.Rows.Count != 0)
                            {
                                DataTable temp = table.Copy(); //暫存temp取結構
                                temp.Clear();
                                decimal totqty = 0;
                                DataRow dr1;
                                var itno = Outtable.AsEnumerable().OrderBy(r => r["itno"].ToString()).Select(r => r["itno"].ToString()).Distinct().ToList();
                                
                                for (int i = 0; i < itno.Count; i++)
                                {
                                    //有異動才顯示再報表
                                    if (Intable.AsEnumerable().Count(r => r["itno"].ToString().Trim() == itno[i]) == 0) continue;
                                    // totqty 區間外表示期初數量(偉任理解)
                                    totqty = Outtable.AsEnumerable().Where(r => r["itno"].ToString() == itno[i]).Sum(r => r["結餘"].ToDecimal());
                                    dr1 = Outtable.AsEnumerable().Where(r => r["itno"].ToString() == itno[i]).First();
                                    dr = temp.NewRow();
                                    dr["itno"] = itno[i].ToString();
                                    dr["itname"] = dr1["itname"].ToString();
                                    dr["itunit"] = dr1["itunit"].ToString();
                                    dr["期初數量"] = totqty;
                                    dr["期初成本"] = 減一日期初成本(dr["itno"].ToString());
                                    dr["期初金額"] = (dr["期初成本"].ToDecimal() * totqty).ToDecimal();
                                    dr["單據"] = "期初";
                                    temp.Rows.Add(dr);
                                    temp.AcceptChanges();
                                }

                                Intable.Merge(temp);
                                Intable.AcceptChanges();
                            }
                            if (Intable.Rows.Count != 0)
                            {
                                DataTable temp = table.Copy();
                                temp.Clear();
                                decimal tot = 0;
                                DataRow dr1;
                                var itno = Intable.AsEnumerable().OrderBy(r => r["itno"].ToString()).Select(r => r["itno"].ToString()).Distinct().ToList();
                                for (int i = 0; i < itno.Count; i++)
                                {
                                    dr = temp.NewRow();
                                    dr1 = Intable.AsEnumerable().Where(r => r["itno"].ToString() == itno[i]).First();
                                    dr["itno"] = dr1["itno"].ToString();
                                    dr["itname"] = dr1["itname"].ToString();
                                    dr["itunit"] = dr1["itunit"].ToString();

                                    //期初寫入
                                    dr1 = Intable.AsEnumerable().ToList().Find(r => r["itno"].ToString() == itno[i] && r["單據"].ToString() == "期初");
                                    if (dr1 != null)
                                    {
                                        dr["期初數量"] = dr1["期初數量"].ToString();
                                        dr["期初成本"] = dr1["期初成本"].ToString();
                                        dr["期初金額"] = dr1["期初金額"].ToString();
                                    }
                                    else
                                    {
                                        dr["期初數量"] = 0;
                                        dr["期初成本"] = 0;
                                        dr["期初金額"] = 0;
                                    }

                                    //進貨
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "進貨"
                                        ).Sum(r => r["總數量"].ToDecimal());
                                    dr["進貨數量"] = tot;
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "進退"
                                        ).Sum(r => r["總數量"].ToDecimal());
                                    dr["進貨數量"] = dr["進貨數量"].ToDecimal() + tot;

                                    if (dr["進貨數量"].ToDecimal().ToString("f0") != "0")
                                    {
                                        tot = Intable.AsEnumerable().Where
                                            (
                                            r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "進貨"
                                            ).Sum(r => r["稅前金額"].ToDecimal());
                                        dr["進貨金額"] = tot;
                                        tot = Intable.AsEnumerable().Where
                                            (
                                            r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "進退"
                                            ).Sum(r => r["稅前金額"].ToDecimal());
                                        dr["進貨金額"] = dr["進貨金額"].ToDecimal() + tot;

                                        if (dr["進貨金額"].ToDecimal().ToString("f0") != "0")
                                            dr["進貨平均單價"] = dr["進貨金額"].ToDecimal() / dr["進貨數量"].ToDecimal();
                                        else
                                            dr["進貨平均單價"] = 0;
                                    }
                                    else
                                    {
                                        dr["進貨金額"] = 0;
                                        dr["進貨平均單價"] = 0;
                                    }

                                    //銷貨
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "銷貨"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["銷貨數量"] = tot;
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "銷退"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["銷貨數量"] = dr["銷貨數量"].ToDecimal() + tot;

                                    if (dr["銷貨數量"].ToDecimal().ToString("f0") != "0")
                                    {
                                        dr["銷貨成本"] = GetCost(itno[i], "", SelectAvgcost(Date.ToTWDate(date1.Text.Trim()).Substring(0,5))) * dr["銷貨數量"].ToDecimal() * (-1);
                                    }
                                    else
                                    {
                                        dr["銷貨成本"] = 0;
                                    }

                                    //領料
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "領料"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["領料數量"] = tot;

                                    //入料
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "入料"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["入料數量"] = tot;

                                    //入庫
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "入庫"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["入庫數量"] = tot;

                                    //扣料
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "扣料"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["扣料數量"] = tot;

                                    //調整
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "調整"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["調整數量"] = tot;

                                    //撥出
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "撥出"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["撥出數量"] = tot;

                                    //撥入
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "撥入"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["撥入數量"] = tot;

                                    //寄庫
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "寄庫"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["寄庫數量"] = tot;

                                    //if (dr["寄庫數量"].ToDecimal().ToString("f0") != "0")
                                    //{
                                    //    dr["寄庫成本"] = SetCost(dr) * dr["寄庫數量"].ToDecimal();
                                    //}
                                    //else
                                    //{
                                    //    dr["寄庫成本"] = 0;
                                    //}

                                    //領庫
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "領庫"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["領庫數量"] = tot;

                                    //if (dr["領庫數量"].ToDecimal().ToString("f0") != "0")
                                    //{
                                    //    dr["領庫成本"] = SetCost(dr) * dr["領庫數量"].ToDecimal() * (-1);
                                    //}
                                    //else
                                    //{
                                    //    dr["領庫成本"] = 0;
                                    //}

                                    //借出
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "借出"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["借出數量"] = tot;

                                    //借出還入
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "還入"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["還入數量"] = tot;

                                    //借入
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "借入"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["借入數量"] = tot;

                                    //借入還出
                                    tot = Intable.AsEnumerable().Where
                                        (
                                        r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Substring(0, 2) == "還出"
                                        ).Sum(r => r["結餘"].ToDecimal());
                                    dr["還出數量"] = tot;
                                    

                                    //結餘數量
                                    tot = Intable.AsEnumerable().Where(r => r["itno"].ToString() == itno[i]).Sum(r => r["結餘"].ToDecimal());
                                    tot += Intable.AsEnumerable().Where(r => r["itno"].ToString() == itno[i] && r["單據"].ToString().Trim() == "期初").Sum(r => r["期初數量"].ToDecimal());
                                    dr["結餘數量"] = tot;

                                    //結餘單位成本
                                    dr["結餘單位成本"] = GetCost(itno[i], "", SelectAvgcost(Date.ToTWDate(date1.Text.Trim()).Substring(0, 5)));//SetCost(dr);

                                    //結餘金額
                                    dr["結餘金額"] = dr["結餘數量"].ToDecimal() * dr["結餘單位成本"].ToDecimal();

                                    temp.Rows.Add(dr);
                                }
                                temp.AcceptChanges();

                                using (SqlDataAdapter da = new SqlDataAdapter("select itno,faname1,ItBuyPri from item left join fact on item.fano=fact.fano", cn))
                                {
                                    tItem.Clear();
                                    da.Fill(tItem);
                                }
                                var litem = tItem.AsEnumerable().ToList();
                                for (int j = 0; j < temp.Rows.Count; j++)
                                {
                                    var rw = litem.Find(r => r["itno"].ToString().Trim() == temp.Rows[j]["itno"].ToString().Trim());
                                    if (rw != null)
                                    {
                                        temp.Rows[j]["廠商"] = rw["faname1"].ToString().Trim();
                                        temp.Rows[j]["進價"] = rw["ItBuyPri"].ToDecimal();
                                    }
                                }

                                this.OpemInfoFrom<FrmItemChange_Rptb>(() =>
                                {
                                    FrmItemChange_Rptb frm = new FrmItemChange_Rptb();
                                    frm.stno = StNo.Text.Trim();
                                    frm.stname = StName.Text.Trim();
                                    frm.dtD = temp;
                                    string range = Date.AddLine(date.Text.Trim()) + "～" + Date.AddLine(date1.Text.Trim());
                                    frm.DateRange = range;
                                    return frm;
                                });


                            }
                            else
                            {
                                MessageBox.Show("查無資料!");
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            #region rd2
                            //表單為明細細表
                            #region 加入期初之Row 以及 填入成本[SetCost]
                            sql = "select s.itno,s.itname,s.itqtyf as 結餘,i.itunit,結餘總數=0.0"
                                + " from stock as s left join item as i on i.itno=s.itno where '0'='0'";
                            if (ItNo.Text != "")
                                sql += " and s.itno >=@itno";
                            if (ItNo1.Text != "")
                                sql += " and s.itno <=@itno1";
                            if (StNo.Text != "")
                                sql += " and s.stno =@stno";
                            if (KiNo.Text != "")
                                sql += " and i.kino =@kino";
                            sql += " and s.itqtyf not in (0)";

                            cmd.Parameters.Clear();
                            if (ItNo.Text != "")
                                cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                            if (ItNo1.Text != "")
                                cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                            if (StNo.Text != "")
                                cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                            if (KiNo.Text != "")
                                cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                            cmd.CommandText = sql;

                            dd = new SqlDataAdapter(cmd);
                            table.Clear();
                            dd.Fill(table);
                            dd.Dispose();

                            if (table.Rows.Count != 0)
                            {
                                Outtable.Merge(table);
                                Outtable.AcceptChanges();
                            }

                            if (Intable.Rows.Count == 0 && Outtable.Rows.Count == 0)
                            {
                                MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                date.Focus();
                                btnBrow.Enabled = true;
                                return;
                            }

                            if (Outtable.Rows.Count != 0)
                            {
                                DataTable temp = table.Copy(); //暫存temp取結構
                                temp.Clear();
                                decimal totqty = 0;
                                DataRow dr1;
                                var itno = Outtable.AsEnumerable().Select(r => r["itno"].ToString()).Distinct().ToList();
                                for (int i = 0; i < itno.Count; i++)
                                {
                                    totqty = Outtable.AsEnumerable().Where(r => r["itno"].ToString() == itno[i]).Sum(r => r["結餘"].ToDecimal());
                                    if (totqty == 0) continue;
                                    dr1 = Outtable.AsEnumerable().Where(r => r["itno"].ToString() == itno[i]).First();
                                    dr = temp.NewRow();
                                    dr["日期"] = "";
                                    dr["日期1"] = "";
                                    dr["itno"] = itno[i].ToString();
                                    dr["itname"] = dr1["itname"].ToString();
                                    dr["itunit"] = dr1["itunit"].ToString();
                                    dr["itpkgqty"] = 1;
                                    dr["稅前價格"] = 0.0;
                                    dr["稅前金額"] = 0.0;
                                    dr["結餘"] = totqty;
                                    dr["數量"] = totqty;
                                    dr["總數量"] = totqty;
                                    dr["單據"] = "期初";
                                    temp.Rows.Add(dr);
                                }
                                temp.AcceptChanges();
                                //SetCost(temp);
                                Intable.Merge(temp);
                                Intable.AcceptChanges();
                            }
                            #endregion
                            if (Intable.Rows.Count != 0)
                            {
                                var num = Intable.AsEnumerable().OrderBy(r => r["itno"].ToString()).Select(r => r["itno"].ToString()).Distinct().ToList();
                                list = num.ToList();
                            }

                            if (Intable.Rows.Count != 0)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("itno", "");
                                cmd.Parameters.AddWithValue("stno", "");
                                if (Intable.Columns.Contains("倉庫名稱") == false)
                                {
                                    Intable.Columns.Add("倉庫名稱", typeof(string));
                                }
                                string AvgcostNum = "";
                                List<string> 不需帶月平均成本單據 = new List<string>() { "進貨+", "進退-", "扣料-", "入庫+", "調整+", "銷退+", "銷退組+" };


                                for (int i = 0; i < Intable.Rows.Count; i++)
                                {
                                    cmd.Parameters["stno"].Value = Intable.Rows[i]["倉庫1"].ToString();
                                    cmd.CommandText = "select stname from stkroom where stno=@stno";
                                    Intable.Rows[i]["倉庫名稱"] = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();
                                    if (不需帶月平均成本單據.Count(tt => tt == Intable.Rows[i]["單據"].ToString()) == 0)
                                    {
                                        decimal 成本 = 0M;
                                        cmd.Parameters["itno"].Value = Intable.Rows[i]["itno"].ToString();
                                        

                                        //取得成本並填入
                                        if (Intable.Rows[i]["單據"].ToString() == "期初")
                                        {
                                            #region 區間外:date_資料值為 "  /  /  "，帶標準成本
                                            //期初成本帶起始日在減1日
                                            string M = SelectAvgcost(Date.ToTWDate(date.Text.Trim()).Substring(0, 5));
                                            string D = Date.ToTWDate(date.Text.Trim()).Substring(5, 2);
                                            if (D.ToDecimal() - 1 == 0)
                                            {
                                                if (M.ToDecimal() == 1)
                                                {
                                                    //等於1，代表為庫存年度第一個月，E.g.系統年度103，使用者起始日下1030101，若在減一日就會出問題,所以帶開帳期初
                                                    cmd.CommandText = "select ItFirCost from item where itno = @itno";
                                                }
                                                else
                                                {
                                                    M = (M.ToDecimal() - 1).ToString().PadLeft(2, '0');
                                                    cmd.CommandText = "select avgcost" + M + " from itemcost where itno = @itno";
                                                }
                                            }
                                            成本 = cmd.ExecuteScalar().ToDecimal("f"+Common.M);
                                            #endregion
                                        }
                                        else
                                        {
                                            AvgcostNum = SelectAvgcost(new string(Intable.Rows[i]["日期"].ToString().Take(5).ToArray()));// range 01~24
                                            #region //區間內帶入當約平均成本
                                            if (rdAvgByAllStk.Checked)
                                            {
                                                cmd.CommandText = "select avgcost" + AvgcostNum + " from itemcost where itno = @itno";
                                            }
                                            else if (rdAvgByOneStk.Checked)
                                            {
                                                cmd.CommandText = "select avgcost" + AvgcostNum + " from stkcost where itno=@itno and stno=@stno";
                                            }
                                            else if (rd5.Checked)
                                            {
                                                cmd.CommandText = "select itcost from item where itno=@itno";
                                            }
                                            成本 = cmd.ExecuteScalar().ToDecimal("f" + Common.M);
                                            #endregion
                                        }
                                        Intable.Rows[i]["成本"] = (Intable.Rows[i]["總數量"].ToDecimal() * 成本).ToDecimal("f" + Common.M);
                                    }
                                }
                            }

                            this.OpemInfoFrom<FrmItemChange_Rptc>(() =>
                            {
                                FrmItemChange_Rptc frm = new FrmItemChange_Rptc();
                                if (rd7.Checked)
                                    RD7 = true;
                                else
                                    RD7 = false;
                                frm.RD7 = RD7;
                                frm.table = Intable;
                                frm.list = list;
                                string daterange = Date.AddLine(date.Text.Trim()) + "～" + Date.AddLine(date1.Text.Trim());
                                frm.DateRange = daterange;
                                return frm;
                            });


                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        btnBrow.Enabled = true;
 
                    }
                }
            }

            btnBrow.Enabled = true;
        }

        void 銷退取得累進成本(ref DataTable tb)
        {
            DataTable temprsale = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("itno", "");
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    var itno = tb.Rows[i]["itno"].ToString().Trim();
                    cmd.Parameters["itno"].Value = itno;
                    var unit = tb.Rows[i]["itunit"].ToString().Trim();
                    var qty = tb.Rows[i]["數量"].ToDecimal();
                    var itpkgqty = tb.Rows[i]["itpkgqty"].ToDecimal();
                    var realcost = 0M;


                    cmd.CommandText = "select top(1)itno,itunit,itpkgqty,RealCost from bshopd where itno=@itno order by bsdate DESC";
                    dd.Fill(temprsale);

                    if (temprsale.Rows.Count > 0)
                    {
                        if (temprsale.Rows[0]["itunit"].ToString().Trim() == unit && temprsale.Rows[0]["itpkgqty"].ToDecimal() == itpkgqty)
                        {
                            realcost = temprsale.Rows[0]["realcost"].ToDecimal();
                        }
                        else
                        {
                            if (temprsale.Rows[0]["itpkgqty"].ToDecimal() == 0) temprsale.Rows[0]["itpkgqty"] = 1;
                            realcost = itpkgqty * (temprsale.Rows[0]["realcost"].ToDecimal() / temprsale.Rows[0]["itpkgqty"].ToDecimal());
                        }
                    }
                    else
                    {
                        cmd.CommandText = "select itno,ItFirCost from item where itno=@itno";
                        var rw = cmd.ExecuteScalar().ToDecimal();
                        realcost = itpkgqty * rw;
                    }

                    tb.Rows[i]["成本"] = (qty * realcost);
                    temprsale.Clear();
                }
            }
        }
        string SelectAvgcost(string olddate) //偉任加
        {
            string 庫存年度 = (Common.Sys_StkYear1).ToString();
            string 庫存年度後一年 = (Common.Sys_StkYear1 + 1).ToString();

            for (int i = 1; i <= 12; i++)
            {
                if (i < 10)
                {
                    if (庫存年度 + "0" + i.ToString() == olddate)
                    {
                        return "0" + i.ToString();
                    }
                }
                else
                {
                    if (庫存年度 + i.ToString() == olddate)
                    {
                        return i.ToString();
                    }
                }
            }
            for (int i = 1; i <= 12; i++)
            {
                if (i < 10)
                {
                    if (庫存年度後一年 + "0" + i.ToString() == olddate)
                    {
                        return (12 + i).ToString();
                    }
                }
                else
                {
                    if (庫存年度後一年 + i.ToString() == olddate)
                    {
                        return (12 + i).ToString();
                    }
                }
            }
            return "";
        }
        decimal 減一日期初成本(string itno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("itno", itno);
                if (rd5.Checked)
                {
                    cmd.CommandText = "select itcost from item where itno=@itno";
                    
                }
                else
                {
                    string M = SelectAvgcost(Date.ToTWDate(date.Text.Trim()).Substring(0, 5));
                    string D = Date.ToTWDate(date.Text.Trim()).Substring(5, 2);
                    if (D.ToDecimal() - 1 == 0)
                    {
                        if (M.ToDecimal() == 1)
                        {
                            //等於1，代表為庫存年度第一個月，E.g.系統年度103，使用者起始日下1030101，若在減一日就會出問題,所以帶開帳期初
                            cmd.CommandText = "select ItFirCost from item where itno = @itno";
                        }
                        else
                        {
                            M = (M.ToDecimal() - 1).ToString().PadLeft(2, '0');
                            cmd.CommandText = "select avgcost" + M + " from itemcost where itno = @itno";
                        }
                    }
                }
                return cmd.ExecuteScalar().ToDecimal("f" + Common.M);
            }
        }
        decimal GetCost(string itno,string stno,string Momth)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("itno", itno);
                cmd.Parameters.AddWithValue("stno", stno);

                if (rd5.Checked)
                {
                    cmd.CommandText = "select itcost from item where itno=@itno";
                }
                else if (rdAvgByAllStk.Checked)
                {
                    cmd.CommandText = "select avgcost" + Momth + " from itemcost where itno = @itno";
                }
                else if (rdAvgByOneStk.Checked)
                {
                    cmd.CommandText = "select avgcost" + Momth + " from stkcost where itno=@itno and stno=@stno";
                }
                return cmd.ExecuteScalar().ToDecimal("f" + Common.M);
            }
        }

        private string GetMonth(decimal sqldate)
        {
            string Month = "";
            if (sqldate < 10)
                Month = "0" + sqldate;
            else
                Month = sqldate.ToString();
            return Month;
        }

        void CkeckDraw(string str, DataTable tb)
        {
            string memo = "", memo1 = "";
            if (str == "draw")
            {
                memo = "領料";
                memo1 = "入料";
            }
            if (str == "drawbom")
            {
                memo = "領料組";
                memo1 = "入料組";
            }
            if (StNo.Text == "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString() != "" && tb.Rows[i]["倉庫2"].ToString() != "")
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                        }
                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo1 + "+";
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                    else
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo1 + "+";
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
        }

        void CkeckGarner(DataTable tb)
        {
            string memo = "扣料-", memo1 = "入庫+";
            if (StNo.Text == "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString() != "" && tb.Rows[i]["倉庫2"].ToString() != "")
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo1;
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = tb.Rows[i]["成本"].ToString();
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                        }

                        if (tb.Rows[i]["ittrait"].ToString() == "3")
                        {
                            if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                            {
                                dr = tbtemp.NewRow();
                                dr["單號"] = tb.Rows[i]["單號"].ToString();
                                dr["itno"] = tb.Rows[i]["itno"].ToString();
                                dr["itname"] = tb.Rows[i]["itname"].ToString();
                                dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                                dr["日期"] = tb.Rows[i]["日期"].ToString();
                                dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                                dr["單據"] = memo;
                                dr["數量"] = tb.Rows[i]["數量"].ToDecimal() * (-1);
                                dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                                dr["倉庫2"] = "";
                                dr["成本"] = tb.Rows[i]["成本"].ToString();
                                dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                                dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                                dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                                dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                                dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                                tbtemp.Rows.Add(dr);
                            }
                        }

                        tbtemp.AcceptChanges();
                    }
                    else
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo1;
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = tb.Rows[i]["成本"].ToString();
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString() != "" && tb.Rows[i]["倉庫2"].ToString() != "")
                    {
                        if (tb.Rows[i]["倉庫1"].ToString().Trim() == StNo.Text.Trim())
                        {
                            if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                            {
                                dr = tbtemp.NewRow();
                                dr["單號"] = tb.Rows[i]["單號"].ToString();
                                dr["itno"] = tb.Rows[i]["itno"].ToString();
                                dr["itname"] = tb.Rows[i]["itname"].ToString();
                                dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                                dr["日期"] = tb.Rows[i]["日期"].ToString();
                                dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                                dr["單據"] = memo1;
                                dr["數量"] = tb.Rows[i]["數量"].ToString();
                                dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                                dr["倉庫2"] = "";
                                dr["成本"] = tb.Rows[i]["成本"].ToString();
                                dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                                dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                                dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                                dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                                dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                                tbtemp.Rows.Add(dr);
                                tbtemp.AcceptChanges();
                            }
                        }
                        if (tb.Rows[i]["倉庫2"].ToString().Trim() == StNo.Text.Trim())
                        {
                            if (tb.Rows[i]["ittrait"].ToString() == "3")
                            {
                                if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                                {
                                    dr = tbtemp.NewRow();
                                    dr["單號"] = tb.Rows[i]["單號"].ToString();
                                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                                    dr["itname"] = tb.Rows[i]["itname"].ToString();
                                    dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                                    dr["日期"] = tb.Rows[i]["日期"].ToString();
                                    dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                                    dr["單據"] = memo1;
                                    dr["數量"] = tb.Rows[i]["數量"].ToDecimal() * (-1);
                                    dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                                    dr["倉庫2"] = "";
                                    dr["成本"] = tb.Rows[i]["成本"].ToString();
                                    dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                                    dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                                    dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                                    dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                                    dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                                    tbtemp.Rows.Add(dr);
                                    tbtemp.AcceptChanges();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tb.Rows[i]["倉庫1"].ToString().Trim() == StNo.Text.Trim())
                        {
                            if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                            {
                                dr = tbtemp.NewRow();
                                dr["單號"] = tb.Rows[i]["單號"].ToString();
                                dr["itno"] = tb.Rows[i]["itno"].ToString();
                                dr["itname"] = tb.Rows[i]["itname"].ToString();
                                dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                                dr["日期"] = tb.Rows[i]["日期"].ToString();
                                dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                                dr["單據"] = memo1;
                                dr["數量"] = tb.Rows[i]["數量"].ToString();
                                dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                                dr["倉庫2"] = "";
                                dr["成本"] = tb.Rows[i]["成本"].ToString();
                                dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                                dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                                dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                                dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                                dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                                tbtemp.Rows.Add(dr);
                                tbtemp.AcceptChanges();
                            }
                        }
                    }
                }

            }



        }

        void CkeckGarnbom(DataTable tb)
        {
            string memo = "扣料組-";
            tbtemp = tb.Copy();
            tbtemp.Clear();
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (StNo.Text == "")
                {
                    if (tb.Rows[i]["倉庫2"].ToString() != "")
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo;
                            dr["數量"] = tb.Rows[i]["數量"].ToDecimal() * (-1);
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = tb.Rows[i]["成本"].ToString();
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
                if (StNo.Text != "")
                {
                    if (StNo.Text == tb.Rows[i]["倉庫2"].ToString())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo;
                            dr["數量"] = tb.Rows[i]["數量"].ToDecimal() * (-1);
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = tb.Rows[i]["成本"].ToString();
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
        }

        void CkeckAllot(DataTable tb)
        {
            string memo = "", memo1 = "";
            memo = "撥出";
            memo1 = "撥入";

            if (StNo.Text == "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString() != "" && tb.Rows[i]["倉庫2"].ToString() != "")
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                        }

                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo1 + "+";
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                    else
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo1 + "+";
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
        }

        void CkeckLend(string str, DataTable tb)
        {
            string memo = "";
            if (str == "lend")
            {
                memo = "借出";
            }
            if (str == "lendbom")
            {
                memo = "借出組";
            }
            if (StNo.Text == "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期"] = tb.Rows[i]["日期"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["單據"] = memo + "+";
                        dr["數量"] = tb.Rows[i]["數量"].ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫2"] = "";
                        dr["成本"] = 0.0;
                        dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                        dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                        dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                        dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                        dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                        tbtemp.Rows.Add(dr);
                    }
                    if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期"] = tb.Rows[i]["日期"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫2"] = "";
                        dr["成本"] = 0.0;
                        dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                        dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                        dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                        dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                        dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "+";
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
        }

        void rrrrrCkeckLend(string str, DataTable tb)
        {
            string memo = "";
            if (str == "rlend")
            {
                memo = "還入";
            }
            if (str == "rlendbom")
            {
                memo = "還入組";
            }
            if (StNo.Text == "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期"] = tb.Rows[i]["日期"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫2"] = "";
                        dr["成本"] = 0.0;
                        dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                        dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                        dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                        dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                        dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                        tbtemp.Rows.Add(dr);
                    }
                    if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期"] = tb.Rows[i]["日期"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["單據"] = memo + "+";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫2"] = "";
                        dr["成本"] = 0.0;
                        dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                        dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                        dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                        dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                        dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "+";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
        }

        void CkeckBorr(string str, DataTable tb)
        {
            string memo = "";
            if (str == "borr")
            {
                memo = "借入";
            }
            if (str == "borrbom")
            {
                memo = "借入組";
            }
            if (StNo.Text == "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期"] = tb.Rows[i]["日期"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫2"] = "";
                        dr["成本"] = 0.0;
                        dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                        dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                        dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                        dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                        dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                        tbtemp.Rows.Add(dr);
                    }
                    if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期"] = tb.Rows[i]["日期"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["單據"] = memo + "+";
                        dr["數量"] = tb.Rows[i]["數量"].ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫2"] = "";
                        dr["成本"] = 0.0;
                        dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                        dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                        dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                        dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                        dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "+";
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
        }

        void rrrrrCkeckBorr(string str, DataTable tb)
        {
            string memo = "";
            if (str == "rborr")
            {
                memo = "還出";
            }
            if (str == "rborrbom")
            {
                memo = "還出組";
            }
            if (StNo.Text == "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期"] = tb.Rows[i]["日期"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["單據"] = memo + "+";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫2"] = "";
                        dr["成本"] = 0.0;
                        dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                        dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                        dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                        dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                        dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                        tbtemp.Rows.Add(dr);
                    }
                    if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期"] = tb.Rows[i]["日期"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫2"] = "";
                        dr["成本"] = 0.0;
                        dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                        dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                        dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                        dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                        dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫1"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "+";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal();
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal();
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StNo.Text.Trim())
                    {
                        if (CkeckSttrait(tb.Rows[i]["倉庫2"].ToString()))
                        {
                            dr = tbtemp.NewRow();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期"] = tb.Rows[i]["日期"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["單據"] = memo + "-";
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫2"] = "";
                            dr["成本"] = 0.0;
                            dr["結餘"] = tb.Rows[i]["結餘"].ToDecimal() * (-1);
                            dr["總數量"] = tb.Rows[i]["總數量"].ToDecimal() * (-1);
                            dr["稅前價格"] = tb.Rows[i]["稅前價格"].ToDecimal();
                            dr["稅前金額"] = tb.Rows[i]["稅前金額"].ToDecimal();
                            dr["itpkgqty"] = tb.Rows[i]["itpkgqty"].ToDecimal();
                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                    }
                }
            }
        }

        bool CkeckSttrait(string stno)
        {
            if (AllSel) return true;
            sttrait = stkroom.Find(r => r["stno"].ToString().Trim() == stno.Trim())["sttrait"].ToDecimal();
            if (sttrait == 1 && ch1.Checked) return true;
            if (sttrait == 2 && ch2.Checked) return true;
            if (sttrait == 3 && ch3.Checked) return true;
            if (sttrait == 4 && ch4.Checked) return true;
            return false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void date_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;

            xe.DateValidate(sender, e);

            TextBox tb = sender as TextBox;

            if (Date.ToTWDate(tb.Text).Substring(0, 3).ToDecimal() < Common.Sys_StkYear1)
            {
                MessageBox.Show("輸入的年度不可以小於系統庫存年度", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                e.Cancel = true;
                return;
            }
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }
        private void ItNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            var tb = sender as TextBox;
            if (tb.TrimTextLenth() == 0)
            {
                tb.Clear();
                return;
            }

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                tb.Text = row["itno"].ToString().Trim();
            });
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }
        private void KiNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;

            if (KiNo.Text.Trim() == "")
            {
                KiNo.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Kind>(sender, e, row =>
            {
                KiNo.Text = row["KiNo"].ToString().Trim();
            });

        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }
        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if (StNo.TrimTextLenth() == 0)
            {
                StNo.Clear();
                StName.Clear();
                return;
            }

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }

        private void rd1_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1.Checked)
            {
                rd5.Checked = true;
                rdAvgByOneStk.Enabled = false;
            }
            else if (rd2.Checked)
            {
                rd5.Checked = true;
                rdAvgByOneStk.Enabled = true;
            }
        }
    }
}
