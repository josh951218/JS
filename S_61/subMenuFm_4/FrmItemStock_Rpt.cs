using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Threading.Tasks;

namespace S_61.subMenuFm_4
{
    public partial class FrmItemStock_Rpt : Formbase
    {
        JBS.JS.xEvents xe;

        DataTable tb1 = new DataTable();
        DataTable tb2 = new DataTable();
        DataTable Alltb1 = new DataTable();
        DataTable Alltb2 = new DataTable();
        DataTable tbtemp = new DataTable();
        DataRow dr;
        List<string> list;
        SqlDataAdapter dd;

        bool Error = false;

        public FrmItemStock_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

        }

        private void FrmItemStock_Rpt_Load(object sender, EventArgs e)
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

            date.Focus();
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
            Alltb1.Clear();
            Alltb2.Clear();
            string sql = "";
            if (compare(date, date1)) return;
            if (compare(ItNo, ItNo1)) return;

           
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        #region 進貨單據 區間內  成本=rshopd.realcos
                        sql = "select a.itno,a.bsno as 單號,a.itname,a.itunit,a.bsdate as 日期1,a.bsdate1 as 日期2,b.faname1 as 客戶";
                        sql += ",單據='進貨+',a.realcost as 成本,a.qty*a.itpkgqty as 數量,a.qty*a.itpkgqty*a.realcost as 累進成本,a.qty*a.itpkgqty as 累進數量,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from bshopd a left join bshop b on a.bsno=b.bsno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.bsdate >=@bsdate";
                            sql += " and a.bsdate <=@bsdate1";
                        }
                        else
                        {
                            sql += " and a.bsdate1 >=@bsdate";
                            sql += " and a.bsdate1 <=@bsdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bsdate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        /********************************/
                        //alltb1 alltb2 先取結構 以免後面沒結構 
                        Alltb1 = tb1.Copy();
                        Alltb2 = tb1.Copy();
                        Alltb1.Clear();
                        Alltb2.Clear();
                        if (tb1.Rows.Count > 0)
                        {
                            Alltb1.Merge(tb1);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 進貨單據 區間外  成本=rshopd.realcos
                        sql = "select a.itno,a.bsno as 單號,a.itname,a.itunit,a.bsdate as 日期1,a.bsdate1 as 日期2,b.faname1 as 客戶";
                        sql += ",單據='進貨+',a.realcost as 成本,a.qty*a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from bshopd a left join bshop b on a.bsno=b.bsno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.bsdate >= @bsdate";
                            sql += " and a.bsdate < @bsdate1";
                        }
                        else
                        {
                            sql += " and a.bsdate1 >= @bsdate";
                            sql += " and a.bsdate1 < @bsdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";

                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdate", GetStartDay());
                        cmd.Parameters.AddWithValue("bsdate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            Alltb2.Merge(tb2);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 進退單據 區間內  成本=rshopd.realcos
                        sql = "select a.itno,a.bsno as 單號,a.itname,a.itunit,a.bsdate as 日期1,a.bsdate1 as 日期2,b.faname1 as 客戶";
                        sql += ",單據='進退-',a.realcost as 成本,a.qty*-1*a.itpkgqty as 數量,-1*a.qty*a.itpkgqty*a.realcost as 累進成本,a.qty*-1*a.itpkgqty as 累進數量,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from rshopd a left join rshop b on a.bsno=b.bsno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.bsdate >=@bsdate";
                            sql += " and a.bsdate <=@bsdate1";
                        }
                        else
                        {
                            sql += " and a.bsdate1 >=@bsdate";
                            sql += " and a.bsdate1 <=@bsdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bsdate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            Alltb1.Merge(tb1);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 進退單據 區間外  成本=rshopd.realcos
                        sql = "select a.itno,a.bsno as 單號,a.itname,a.itunit,a.bsdate as 日期1,a.bsdate1 as 日期2,b.faname1 as 客戶";
                        sql += ",單據='進退-',a.realcost as 成本,a.qty*-1*a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from rshopd a left join rshop b on a.bsno=b.bsno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.bsdate >= @bsdate";
                            sql += " and a.bsdate < @bsdate1";
                        }
                        else
                        {
                            sql += " and a.bsdate1 >= @bsdate";
                            sql += " and a.bsdate1 < @bsdate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdate", GetStartDay());
                        cmd.Parameters.AddWithValue("bsdate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            Alltb2.Merge(tb2);
                            Alltb2.AcceptChanges();
                        }
                        #endregion




                        #region 銷貨單據 區間內 單一商品&組裝品 union all 組合品子件 成本=0.0
                        sql = "select a.itno,a.sano as 單號,a.itname,a.itunit,a.sadate as 日期1,a.sadate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='銷貨-',成本=0.0,a.qty*-1*a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from saled as a left join sale as b on a.sano=b.sano where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.sadate >=@sadate";
                            sql += " and a.sadate <=@sadate1";
                        }
                        else
                        {
                            sql += " and a.sadate1 >=@sadate";
                            sql += " and a.sadate1 <=@sadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        sql += " union all ";

                        sql += "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * -1 * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量 ,累進成本=0.0,累進數量=0.0,q.倉庫1,q.倉庫編1,q.結餘數量";
                        sql += " from (select a.sano as 單號,a.sadate as 日期1,a.sadate1 as 日期2,b.cuname1 as 客戶,單據='銷貨組-',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0,a.bomid";
                        sql += " from saled a left join sale b on a.sano=b.sano  where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.sadate >=@sadate";
                            sql += " and a.sadate <=@sadate1";
                        }
                        else
                        {
                            sql += " and a.sadate1 >=@sadate";
                            sql += " and a.sadate1 <=@sadate1";
                        }
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join SaleBom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("sadate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            Alltb1.Merge(tb1);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 銷貨單據 區間外 單一商品&組裝品 union all 組合品子件 成本=0.0
                        sql = "select a.itno,a.sano as 單號,a.itname,a.itunit,a.sadate as 日期1,a.sadate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='銷貨-',成本=0.0,a.qty*-1*a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from saled as a left join sale as b on a.sano=b.sano where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.sadate >= @sadate";
                            sql += " and a.sadate < @sadate1";
                        }
                        else
                        {
                            sql += " and a.sadate1 >= @sadate";
                            sql += " and a.sadate1 < @sadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        sql += " union all ";

                        sql += "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * -1 * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進成本=0.0,累進數量=0.0 ,q.倉庫1,q.倉庫編1,q.結餘數量";
                        sql += " from (select a.sano as 單號,a.sadate as 日期1,a.sadate1 as 日期2,b.cuname1 as 客戶,單據='銷貨組-',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0,a.bomid";
                        sql += " from saled a left join sale b on a.sano=b.sano where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.sadate >= @sadate";
                            sql += " and a.sadate < @sadate1";
                        }
                        else
                        {
                            sql += " and a.sadate1 >= @sadate";
                            sql += " and a.sadate1 < @sadate1";
                        }
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join SaleBom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", GetStartDay());
                        cmd.Parameters.AddWithValue("sadate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            Alltb2.Merge(tb2);
                            Alltb2.AcceptChanges();
                        }
                        #endregion



                        #region 寄庫單據 區間內 單一商品&組裝品 union all 組合品子件 成本=0.0
                        sql = "select a.itno,a.inno as 單號,a.itname,a.itunit,a.indate as 日期1,a.indate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='寄庫+',成本=0.0,a.inqty*a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from instkd as a left join instk as b on a.inno=b.inno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.indate >=@indate";
                            sql += " and a.indate <=@indate1";
                        }
                        else
                        {
                            sql += " and a.indate1 >=@indate";
                            sql += " and a.indate1 <=@indate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        sql += " union all ";

                        sql += "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.inqty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進成本=0.0,累進數量=0.0 ,q.倉庫1,q.倉庫編1,q.結餘數量";
                        sql += " from (select a.inno as 單號,a.indate as 日期1,a.indate1 as 日期2,b.cuname1 as 客戶,單據='寄庫組+',成本=0.0,a.inqty,a.itpkgqty,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0,a.bomid";
                        sql += " from instkd a left join instk b on a.inno=b.inno  where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.indate >=@indate";
                            sql += " and a.indate <=@indate1";
                        }
                        else
                        {
                            sql += " and a.indate1 >=@indate";
                            sql += " and a.indate1 <=@indate1";
                        }
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join instkBom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("indate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("indate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            Alltb1.Merge(tb1);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 寄庫單據 區間外 單一商品&組裝品 union all 組合品子件 成本=0.0
                        sql = "select a.itno,a.inno as 單號,a.itname,a.itunit,a.indate as 日期1,a.indate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='寄庫+',成本=0.0,a.inqty*a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from instkd as a left join instk as b on a.inno=b.inno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.indate >= @indate";
                            sql += " and a.indate < @indate1";
                        }
                        else
                        {
                            sql += " and a.indate1 >= @indate";
                            sql += " and a.indate1 < @indate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        sql += " union all ";

                        sql += "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.inqty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進成本=0.0,累進數量=0.0 ,q.倉庫1,q.倉庫編1,q.結餘數量";
                        sql += " from (select a.inno as 單號,a.indate as 日期1,a.indate1 as 日期2,b.cuname1 as 客戶,單據='寄庫組+',成本=0.0,a.inqty,a.itpkgqty,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0,a.bomid";
                        sql += " from instkd a left join instk b on a.inno=b.inno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.indate >= @indate";
                            sql += " and a.indate < @indate1";
                        }
                        else
                        {
                            sql += " and a.indate1 >= @indate";
                            sql += " and a.indate1 < @indate1";
                        }
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join instkBom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("indate", GetStartDay());
                        cmd.Parameters.AddWithValue("indate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            Alltb2.Merge(tb2);
                            Alltb2.AcceptChanges();
                        }
                        #endregion



                        #region 領庫單據 區間內 單一商品&組裝品 union all 組合品子件 成本=0.0
                        sql = "select a.itno,a.ouno as 單號,a.itname,a.itunit,a.oudate as 日期1,a.oudate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='領庫-',成本=0.0,a.ouqty*-1*a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from oustkd as a left join oustk as b on a.ouno=b.ouno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.oudate >=@oudate";
                            sql += " and a.oudate <=@oudate1";
                        }
                        else
                        {
                            sql += " and a.oudate1 >=@oudate";
                            sql += " and a.oudate1 <=@oudate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        sql += " union all ";

                        sql += "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.ouqty * -1 * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進成本=0.0,累進數量=0.0 ,q.倉庫1,q.倉庫編1,q.結餘數量";
                        sql += " from (select a.ouno as 單號,a.oudate as 日期1,a.oudate1 as 日期2,b.cuname1 as 客戶,單據='領庫組-',成本=0.0,a.ouqty,a.itpkgqty,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0,a.bomid";
                        sql += " from oustkd a left join oustk b on a.ouno=b.ouno  where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.oudate >=@oudate";
                            sql += " and a.oudate <=@oudate1";
                        }
                        else
                        {
                            sql += " and a.oudate1 >=@oudate";
                            sql += " and a.oudate1 <=@oudate1";
                        }
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join oustkbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("oudate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("oudate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            Alltb1.Merge(tb1);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 領庫單據 區間外 單一商品&組裝品 union all 組合品子件 成本=0.0
                        sql = "select a.itno,a.ouno as 單號,a.itname,a.itunit,a.oudate as 日期1,a.oudate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='領庫-',成本=0.0,a.ouqty*-1*a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from oustkd as a left join oustk as b on a.ouno=b.ouno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.oudate >= @oudate";
                            sql += " and a.oudate < @oudate1";
                        }
                        else
                        {
                            sql += " and a.oudate1 >= @oudate";
                            sql += " and a.oudate1 < @oudate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        sql += " union all ";

                        sql += "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.ouqty * -1 * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進成本=0.0,累進數量=0.0 ,q.倉庫1,q.倉庫編1,q.結餘數量";
                        sql += " from (select a.ouno as 單號,a.oudate as 日期1,a.oudate1 as 日期2,b.cuname1 as 客戶,單據='領庫組-',成本=0.0,a.ouqty,a.itpkgqty,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0,a.bomid";
                        sql += " from oustkd a left join oustk b on a.ouno=b.ouno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.oudate >= @oudate";
                            sql += " and a.oudate < @oudate1";
                        }
                        else
                        {
                            sql += " and a.oudate1 >= @oudate";
                            sql += " and a.oudate1 < @oudate1";
                        }
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join oustkbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("oudate", GetStartDay());
                        cmd.Parameters.AddWithValue("oudate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            Alltb2.Merge(tb2);
                            Alltb2.AcceptChanges();
                        }
                        #endregion




                        #region 銷退單據 區間內 單一商品&組裝品 union all 組合品子件 成本=0.0
                        sql = "select a.itno,a.sano as 單號,a.itname,a.itunit,a.sadate as 日期1,a.sadate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='銷退+',成本=0.0,a.qty*a.itpkgqty as 數量,累進成本=0.0,a.qty*a.itpkgqty as 累進數量,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0,a.qty,a.itpkgqty";
                        sql += " from rsaled as a left join rsale as b on a.sano=b.sano where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.sadate >=@sadate";
                            sql += " and a.sadate <=@sadate1";
                        }
                        else
                        {
                            sql += " and a.sadate1 >=@sadate";
                            sql += " and a.sadate1 <=@sadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        sql += " union all ";

                        sql += "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty  * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進成本=0.0,q.qty  * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 累進數量 ,q.倉庫1,q.倉庫編1,q.結餘數量,q.qty,q.itpkgqty";
                        sql += " from (select a.sano as 單號,a.sadate as 日期1,a.sadate1 as 日期2,b.cuname1 as 客戶,單據='銷退組+',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0,a.bomid";
                        sql += " from rsaled a left join rsale b on a.sano=b.sano where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.sadate >=@sadate";
                            sql += " and a.sadate <=@sadate1";
                        }
                        else
                        {
                            sql += " and a.sadate1 >=@sadate";
                            sql += " and a.sadate1 <=@sadate1";
                        }

                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join RSaleBom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("sadate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            銷退取得累進成本(ref tb1);
                            Alltb1.Merge(tb1);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 銷退單據 區間外 單一商品&組裝品 union all 組合品子件 成本=0.0
                        sql = "select a.itno,a.sano as 單號,a.itname,a.itunit,a.sadate as 日期1,a.sadate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='銷退+',成本=0.0,a.qty*a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0";
                        sql += " from rsaled as a left join rsale as b on a.sano=b.sano where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.sadate >= @sadate";
                            sql += " and a.sadate < @sadate1";
                        }
                        else
                        {
                            sql += " and a.sadate1 >= @sadate";
                            sql += " and a.sadate1 < @sadate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        sql += " union all ";

                        sql += "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進成本=0.0,累進數量=0.0 ,q.倉庫1,q.倉庫編1,q.結餘數量";
                        sql += " from (select a.sano as 單號,a.sadate as 日期1,a.sadate1 as 日期2,b.cuname1 as 客戶,單據='銷退組+',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno 倉庫編1,結餘數量=0.0,a.bomid";
                        sql += " from rsaled a left join rsale b on a.sano=b.sano  where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.sadate >= @sadate";
                            sql += " and a.sadate < @sadate1";
                        }
                        else
                        {
                            sql += " and a.sadate1 >= @sadate";
                            sql += " and a.sadate1 < @sadate1";
                        }

                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join RSaleBom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sadate", GetStartDay());
                        cmd.Parameters.AddWithValue("sadate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            Alltb2.Merge(tb2);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 調整單據 區間內  成本 = adjustd.costb
                        sql = "select a.itno,a.adno as 單號,a.itname,a.itunit,a.addate as 日期1,a.addate1 as 日期2";
                        sql += ",單據='調整+',a.costb as 成本,a.qty * a.itpkgqty as 數量,a.qty * a.itpkgqty * a.costb as 累進成本,a.qty * a.itpkgqty as 累進數量,b.stname as 倉庫1,b.stno 倉庫編1,結餘數量=0.0";
                        sql += " from adjustd a left join adjust b on a.adno=b.adno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.addate >=@addate";
                            sql += " and a.addate <=@addate1";
                        }
                        else
                        {
                            sql += " and a.addate1 >=@addate";
                            sql += " and a.addate1 <=@addate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("addate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("addate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            Alltb1.Merge(tb1);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 調整單據 區間外  成本 = adjustd.costb
                        sql = "select a.itno,a.adno as 單號,a.itname,a.itunit,a.addate as 日期1,a.addate1 as 日期2";
                        sql += ",單據='調整+',a.costb as 成本,a.qty * a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,b.stname as 倉庫1,b.stno 倉庫編1,結餘數量=0.0";
                        sql += " from adjustd a left join adjust b on a.adno=b.adno where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.addate >= @addate";
                            sql += " and a.addate < @addate1";
                        }
                        else
                        {
                            sql += " and a.addate1 >= @addate";
                            sql += " and a.addate1 < @addate1";
                        }
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and b.stno=@stno";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("addate", GetStartDay());
                        cmd.Parameters.AddWithValue("addate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            Alltb2.Merge(tb2);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 領料單據 區間內 單一商品&組裝品   成本 = 0.0
                        sql = "select a.itno,a.drno as 單號,a.itname,a.itunit,a.drdate as 日期1,a.drdate1 as 日期2";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,b.stnameo as 倉庫1,b.stnoo 倉庫編1,b.stnamei as 倉庫2,b.stnoi 倉庫編2,結餘數量=0.0";
                        sql += " from drawd a left join draw b on a.drno=b.drno where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drdate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("drdate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckDraw("draw", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 領料單據 區間內 組合品子件 成本 = 0.0
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,累進成本=0.0,累進數量=0.0,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量 ,q.倉庫1,q.倉庫編1,q.倉庫2,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.drno as 單號,a.drdate as 日期1,a.drdate1 as 日期2,客戶='',單據='',成本=0.0,a.qty,a.itpkgqty,b.stnameo as 倉庫1,b.stnoo 倉庫編1,b.stnamei as 倉庫2,b.stnoi 倉庫編2,結餘數量=0.0,a.bomid";
                        sql += " from drawd a left join draw b on a.drno=b.drno  where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join drawbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drdate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("drdate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckDraw("drawbom", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 領料單據 區間外 單一商品&組裝品 成本 = 0.0
                        sql = "select a.itno,a.drno as 單號,a.itname,a.itunit,a.drdate as 日期1,a.drdate1 as 日期2";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,b.stnameo as 倉庫1,b.stnoo 倉庫編1,b.stnamei as 倉庫2,b.stnoi 倉庫編2,結餘數量=0.0";
                        sql += " from drawd a left join draw b on a.drno=b.drno where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drdate", GetStartDay());
                        cmd.Parameters.AddWithValue("drdate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            CkeckDraw("draw", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 領料單據 區間外 組合品子件 成本 = 0.0
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量 ,q.倉庫1,q.倉庫2,q.倉庫編1,q.倉庫編2,q.結餘數量,累進成本=0.0,累進數量=0.0";
                        sql += " from (select a.drno as 單號,a.drdate as 日期1,a.drdate1 as 日期2,客戶='',單據='',成本=0.0,a.qty,a.itpkgqty,b.stnameo as 倉庫1,b.stnoo 倉庫編1,b.stnamei as 倉庫2,b.stnoi 倉庫編2,結餘數量=0.0,a.bomid";
                        sql += " from drawd a left join draw b on a.drno=b.drno where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join drawbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("drdate", GetStartDay());
                        cmd.Parameters.AddWithValue("drdate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            CkeckDraw("drawbom", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion





                        #region 入庫單據 區間內 單一商品&組裝品(不含子件) 成本 = garnerd.costb 
                        sql = "select a.itno,a.gano as 單號,a.itname,a.itunit,a.gadate as 日期1,a.gadate1 as 日期2";
                        sql += ",單據='',a.costb as 成本,a.qty * a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,b.stnamei as 倉庫1,b.stnoi as 倉庫編1,b.stnameo as 倉庫2,b.stnoo as 倉庫編2,a.ittrait,結餘數量=0.0";
                        sql += " from garnerd a left join garner b on a.gano=b.gano where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("gadate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckGarner(tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 入庫單據 組裝品子件 區間內 成本 = garnerd.costb
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進成本=0.0,累進數量=0.0 ,q.倉庫1,q.倉庫2,q.結餘數量";
                        sql += " from (select a.gano as 單號,a.gadate as 日期1,a.gadate1 as 日期2,客戶='',單據='',成本=0.0,a.qty,a.itpkgqty,b.stnamei as 倉庫1,b.stnoi as 倉庫編1,b.stnameo as 倉庫2,b.stnoo as 倉庫編2,結餘數量=0.0,a.bomid";
                        sql += " from garnerd a left join garner b on a.gano=b.gano  where '0'='0'";
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

                        sql += " and a.ittrait in (2))";
                        sql += " as q left join garnbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gadate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("gadate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckGarnbom(tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion



                        #region 入庫單據 單一商品&組裝品(不含子件) 區間外 成本 = garnerd.costb
                        sql = "select a.itno,a.gano as 單號,a.itname,a.itunit,a.gadate as 日期1,a.gadate1 as 日期2";
                        sql += ",單據='',a.costb as 成本,a.qty * a.itpkgqty as 數量,累進成本=0.0,累進數量=0.0,b.stnamei as 倉庫1,b.stnoi as 倉庫編1,b.stnameo as 倉庫2,b.stnoo as 倉庫編2,a.ittrait,結餘數量=0.0";
                        sql += " from garnerd a left join garner b on a.gano=b.gano where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gadate", GetStartDay());
                        cmd.Parameters.AddWithValue("gadate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            CkeckGarner(tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 入庫單據 組裝品子件 區間外 成本 = 0.0
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,累進成本=0.0,累進數量=0.0,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量 ,q.倉庫1,q.倉庫2,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.gano as 單號,a.gadate as 日期1,a.gadate1 as 日期2,客戶='',單據='',成本=0.0,a.qty,a.itpkgqty,b.stnamei as 倉庫1,b.stnoi as 倉庫編1,b.stnameo as 倉庫2,b.stnoo as 倉庫編2,結餘數量=0.0,a.bomid";
                        sql += " from garnerd a left join garner b on a.gano=b.gano  where '0'='0'";
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

                        sql += " and a.ittrait in (2))";
                        sql += " as q left join garnbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gadate", GetStartDay());
                        cmd.Parameters.AddWithValue("gadate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            CkeckGarnbom(tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion




                        #region 調撥單據 區間內 單一商品&組裝品 成本=0.0
                        sql = "select a.itno,a.alno as 單號,a.itname,a.itunit,a.aldate as 日期1,a.aldate1 as 日期2";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,b.stnameo as 倉庫1,b.stnoo as 倉庫編1,b.stnamei as 倉庫2,b.stnoi as 倉庫編2,結餘數量=0.0";
                        sql += " from allotd a left join allot b on a.alno=b.alno where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("aldate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("aldate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckAllot(tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 調撥單據 區間內 組合品子件 成本=0.0
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進數量=0.0,累進成本=0.0 ,q.倉庫1,q.倉庫2,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.alno as 單號,a.aldate as 日期1,a.aldate1 as 日期2,單據='',成本=0.0,a.qty,a.itpkgqty,倉庫1=stnameo,倉庫編1=a.stnoo,倉庫2=stnamei,倉庫編2=a.stnoi,結餘數量=0.0,a.bomid";
                        sql += " from allotd a left join allot b on a.alno=b.alno  where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join AlloBom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("aldate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("aldate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckAllot(tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion 

                        #region 調撥單據 區間外 單一商品&組裝品 成本=0.0
                        sql = "select a.itno,a.alno as 單號,a.itname,a.itunit,a.aldate as 日期1,a.aldate1 as 日期2";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,b.stnameo as 倉庫1,b.stnoo as 倉庫編1,b.stnamei as 倉庫2,b.stnoi as 倉庫編2,結餘數量=0.0";
                        sql += " from allotd a left join allot b on a.ALno=b.ALno where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("aldate", GetStartDay());
                        cmd.Parameters.AddWithValue("aldate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            CkeckAllot(tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 調撥單據 區間外 組合品子件 成本=0.0
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進數量=0.0,累進成本=0.0 ,q.倉庫1,q.倉庫2,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.alno as 單號,a.aldate as 日期1,a.aldate1 as 日期2,單據='',成本=0.0,a.qty,a.itpkgqty,倉庫1=stnameo,倉庫編1=a.stnoo,倉庫2=stnamei,倉庫編2=a.stnoi,結餘數量=0.0,a.bomid";
                        sql += " from allotd a left join allot b on a.alno=b.alno  where '0'='0'";
                        if (Common.User_DateTime == 1)
                        {
                            sql += " and a.aldate >=@aldate";
                            sql += " and a.aldate < @aldate1";
                        }
                        else
                        {
                            sql += " and a.aldate1 >=@aldate";
                            sql += " and a.aldate1 < @aldate1";
                        }
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join AlloBom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("aldate", GetStartDay());
                        cmd.Parameters.AddWithValue("aldate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckAllot(tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion 






                        #region 借出單據 區間內 單一商品&組裝品
                        sql = "select a.itno,a.leno as 單號,a.itname,a.itunit,a.ledate as 日期1,a.ledate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,倉庫1=a.stname,倉庫編1=a.stno,倉庫2='借出倉庫',倉庫編2=a.stnoi,結餘數量=0.0";
                        sql += " from lendd a left join lend b on a.leno=b.leno where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("ledate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckLend("lend", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 借出單據 區間內 組合品子件
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,累進數量=0.0,累進成本=0.0,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量 ,q.倉庫1,q.倉庫2,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.leno as 單號,a.ledate as 日期1,a.ledate1 as 日期2,客戶=b.cuname1,單據='',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借出倉庫',倉庫編2=a.stnoi,結餘數量=0.0,a.bomid";
                        sql += " from lendd a left join lend b on a.leno=b.leno  where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join lendbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("ledate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckLend("lendbom", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 借出單據 區間外 單一商品&組裝品
                        sql = "select a.itno,a.leno as 單號,a.itname,a.itunit,a.ledate as 日期1,a.ledate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借出倉庫',倉庫編2=a.stnoi,結餘數量=0.0";
                        sql += " from lendd a left join lend b on a.leno=b.leno where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", GetStartDay());
                        cmd.Parameters.AddWithValue("ledate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            CkeckLend("lend", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 借出單據 區間外 組合品子件
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進數量=0.0,累進成本=0.0 ,q.倉庫1,q.倉庫2,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.leno as 單號,a.ledate as 日期1,a.ledate1 as 日期2,客戶=b.cuname1,單據='',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借入倉庫',倉庫編2=a.stnoi,結餘數量=0.0,a.bomid";
                        sql += " from lendd a left join lend b on a.leno=b.leno where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join lendbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", GetStartDay());
                        cmd.Parameters.AddWithValue("ledate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            CkeckLend("lendbom", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion



                        //
                        #region 借出還入單據 區間內 單一商品&組裝品
                        sql = "select a.itno,a.leno as 單號,a.itname,a.itunit,a.ledate as 日期1,a.ledate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,倉庫1=a.stname,倉庫編1=a.stno,倉庫2='借出倉庫',倉庫編2=a.stnoi,結餘數量=0.0";
                        sql += " from rlendd a left join rlend b on a.leno=b.leno where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("ledate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            rrrrrCkeckLend("rlend", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 借出還入單據 區間內 組合品子件
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進數量=0.0,累進成本=0.0 ,q.倉庫1,q.倉庫2 ,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.leno as 單號,a.ledate as 日期1,a.ledate1 as 日期2,客戶=b.cuname1,單據='',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借出倉庫',倉庫編2=a.stnoi,結餘數量=0.0,a.bomid";
                        sql += " from rlendd a left join rlend b on a.leno=b.leno  where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join rlendbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("ledate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            rrrrrCkeckLend("rlendbom", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 借出還入單據 區間外 單一商品&組裝品
                        sql = "select a.itno,a.leno as 單號,a.itname,a.itunit,a.ledate as 日期1,a.ledate1 as 日期2,b.cuname1 as 客戶";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借出倉庫',倉庫編2=a.stnoi,結餘數量=0.0";
                        sql += " from rlendd a left join rlend b on a.leno=b.leno where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", GetStartDay());
                        cmd.Parameters.AddWithValue("ledate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            rrrrrCkeckLend("rlend", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 借出還入單據 區間外 組合品子件
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進數量=0.0,累進成本=0.0 ,q.倉庫1,q.倉庫2,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.leno as 單號,a.ledate as 日期1,a.ledate1 as 日期2,客戶=b.cuname1,單據='',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借入倉庫',倉庫編2=a.stnoi,結餘數量=0.0,a.bomid";
                        sql += " from rlendd a left join rlend b on a.leno=b.leno where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join rlendbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ledate", GetStartDay());
                        cmd.Parameters.AddWithValue("ledate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            rrrrrCkeckLend("rlendbom", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion
                        //



                        #region 借入單據 區間內 單一商品&組裝品
                        sql = "select a.itno,a.bono as 單號,a.itname,a.itunit,a.bodate as 日期1,a.bodate1 as 日期2,b.faname1 as 客戶";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,倉庫1=a.stname,倉庫編1=a.stno,倉庫2='借入倉庫',倉庫編2=a.stnoo,結餘數量=0.0";
                        sql += " from borrd a left join borr b on a.bono=b.bono where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckBorr("borr", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 借入單據 區間內 組合品子件
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進數量=0.0,累進成本=0.0 ,q.倉庫1,q.倉庫2,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.bono as 單號,a.bodate as 日期1,a.bodate1 as 日期2,客戶=b.faname1,單據='',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借入倉庫',倉庫編2=a.stnoo,結餘數量=0.0,a.bomid";
                        sql += " from borrd a left join borr b on a.bono=b.bono  where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join borrbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            CkeckBorr("borrbom", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 借入單據 區間外 單一商品&組裝品
                        sql = "select a.itno,a.bono as 單號,a.itname,a.itunit,a.bodate as 日期1,a.bodate1 as 日期2,b.faname1 as 客戶";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借入倉庫',倉庫編2=a.stnoo,結餘數量=0.0";
                        sql += " from borrd a left join borr b on a.bono=b.bono where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", GetStartDay());
                        cmd.Parameters.AddWithValue("bodate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            CkeckBorr("borr", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 借入單據 區間外 組合品子件
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進數量=0.0,累進成本=0.0 ,q.倉庫1,q.倉庫2,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.bono as 單號,a.bodate as 日期1,a.bodate1 as 日期2,客戶=b.faname1,單據='',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借入倉庫',倉庫編2=a.stnoo,結餘數量=0.0,a.bomid";
                        sql += " from borrd a left join borr b on a.bono=b.bono where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join borrbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", GetStartDay());
                        cmd.Parameters.AddWithValue("bodate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            CkeckBorr("borrbom", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion



                        //
                        #region 借入還出單據 區間內 單一商品&組裝品
                        sql = "select a.itno,a.bono as 單號,a.itname,a.itunit,a.bodate as 日期1,a.bodate1 as 日期2,b.faname1 as 客戶";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,倉庫1=a.stname,倉庫編1=a.stno,倉庫2='借入倉庫',倉庫編2=a.stnoo,結餘數量=0.0";
                        sql += " from rborrd a left join rborr b on a.bono=b.bono where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            rrrrrCkeckBorr("rborr", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 借入還出單據 區間內 組合品子件
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進數量=0.0,累進成本=0.0 ,q.倉庫1,q.倉庫2 ,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.bono as 單號,a.bodate as 日期1,a.bodate1 as 日期2,客戶=b.faname1,單據='',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借入倉庫',倉庫編2=a.stnoo,結餘數量=0.0,a.bomid";
                        sql += " from rborrd a left join rborr b on a.bono=b.bono  where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join rborrbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", date.Text.Trim());
                        cmd.Parameters.AddWithValue("bodate1", date1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb1.Clear();
                        dd.Fill(tb1);
                        dd.Dispose();
                        if (tb1.Rows.Count > 0)
                        {
                            rrrrrCkeckBorr("rborrbom", tb1);
                            Alltb1.Merge(tbtemp);
                            Alltb1.AcceptChanges();
                        }
                        #endregion

                        #region 借入還出單據 區間外 單一商品&組裝品
                        sql = "select a.itno,a.bono as 單號,a.itname,a.itunit,a.bodate as 日期1,a.bodate1 as 日期2,b.faname1 as 客戶";
                        sql += ",單據='',成本=0.0,a.qty * a.itpkgqty as 數量,累進數量=0.0,累進成本=0.0,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借入倉庫',倉庫編2=a.stnoo,結餘數量=0.0";
                        sql += " from rborrd a left join rborr b on a.bono=b.bono where '0'='0'";
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
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        sql += " and a.ittrait not in (1)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", GetStartDay());
                        cmd.Parameters.AddWithValue("bodate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            rrrrrCkeckBorr("rborr", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion

                        #region 借入還出單據 區間外 組合品子件
                        sql = "select c.itno,q.單號,c.itname,c.itunit,q.日期1,q.日期2,q.客戶,q.單據,q.成本,";
                        sql += " q.qty * q.itpkgqty * (c.itqty*c.itpkgqty/c.itpareprs) as 數量,累進數量=0.0,累進成本=0.0 ,q.倉庫1,q.倉庫2 ,q.倉庫編1,q.倉庫編2,q.結餘數量";
                        sql += " from (select a.bono as 單號,a.bodate as 日期1,a.bodate1 as 日期2,客戶=b.faname1,單據='',成本=0.0,a.qty,a.itpkgqty,a.stname as 倉庫1,a.stno as 倉庫編1,倉庫2='借入倉庫',倉庫編2=a.stnoo,結餘數量=0.0,a.bomid";
                        sql += " from rborrd a left join rborr b on a.bono=b.bono where '0'='0'";
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
                        sql += " and a.ittrait in (1))";
                        sql += " as q left join rborrbom as c on q.bomid = c.BomID where q.bomid=c.bomid";
                        if (ItNo.Text != "")
                            sql += " and c.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and c.itno <=@itno1";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bodate", GetStartDay());
                        cmd.Parameters.AddWithValue("bodate1", date.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            rrrrrCkeckBorr("rborrbom", tb2);
                            Alltb2.Merge(tbtemp);
                            Alltb2.AcceptChanges();
                        }
                        #endregion
                        //



                        #region 期初單據
                        sql = "select a.itqtyf as 數量,a.itno,b.itname from stock a left join item b on a.itno=b.itno where '0'='0'";
                        if (ItNo.Text != "")
                            sql += " and a.itno >=@itno";
                        if (ItNo1.Text != "")
                            sql += " and a.itno <=@itno1";
                        if (StNo.Text != "")
                            sql += " and a.stno=@stno";
                        sql += " and a.itqtyf not in (0)";

                        cmd.Parameters.Clear();
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (StNo.Text != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        tb2.Clear();
                        dd.Fill(tb2);
                        dd.Dispose();
                        if (tb2.Rows.Count > 0)
                        {
                            Alltb2.Merge(tb2);
                            Alltb2.AcceptChanges();
                        }
                        #endregion
                    }
                }

                if (Alltb2.Rows.Count == 0 && Alltb1.Rows.Count == 0)
                {
                    MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    date.Focus();
                    return;
                }

                //計算期初數量
                if (Alltb2.Rows.Count != 0)
                {
                    decimal totqty = 0;
                    var group = Alltb2.AsEnumerable().GroupBy(r => r["itno"].ToString());
                    for (int i = 0; i < group.Count(); i++)
                    {
                        var p = group.ElementAt(i).FirstOrDefault();

                        totqty = Alltb2.AsEnumerable().Where(r => r["itno"].ToString() == p["itno"].ToString()).Sum(r => r["數量"].ToDecimal());
                        dr = Alltb1.NewRow();
                        dr["日期1"] = "   /  /  ";
                        dr["日期2"] = "    /  /  ";
                        dr["itno"] = p["itno"].ToString();
                        dr["itname"] = p["itname"].ToString();
                        dr["結餘數量"] = 0;
                        dr["成本"] = 0;
                        dr["數量"] = totqty.ToString();
                        dr["單據"] = "期初";
                        dr["倉庫1"] = StName.Text.ToString();
                        Alltb1.Rows.Add(dr);
                    }
                    Alltb1.AcceptChanges();
                }
                if (Alltb1.Rows.Count != 0)
                {
                    var num = Alltb1.AsEnumerable().OrderBy(r => r["itno"].ToString()).Select(r => r["itno"].ToString()).Distinct().ToList();
                    list = num.ToList();
                }

                #region 填入成本
                object thisLock = new object();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cn.Open();
                    cm.Parameters.AddWithValue("itno", "");
                    cm.Parameters.AddWithValue("stno", "");
                    string AvgcostNum = "";
                    List<string> 不需帶月平均成本單據 = new List<string>() { "進貨+", "進退-", "扣料-", "入庫+", "調整+", "銷退+", "銷退組+" };

                    for (int i = 0; i < Alltb1.Rows.Count; i++)
                    {
                        if (不需帶月平均成本單據.Count(tt => tt == Alltb1.Rows[i]["單據"].ToString()) == 0)
                        {
                            decimal 成本 = 0M;
                            cm.Parameters["itno"].Value = Alltb1.Rows[i]["itno"].ToString();
                            cm.Parameters["stno"].Value = Alltb1.Rows[i]["倉庫編1"].ToString();

                            //取得成本並填入
                            AvgcostNum = SelectAvgcost(new string(Alltb1.Rows[i]["日期1"].ToString().Take(5).ToArray()));// range 01~24
                            if (AvgcostNum.Length == 0)
                            {
                                #region 區間外:date_資料值為 "  /  /  "
                                //期初成本帶起始日在減1日
                                string M = SelectAvgcost(Date.ToTWDate(date.Text.Trim()).Substring(0, 5));
                                string D = Date.ToTWDate(date.Text.Trim()).Substring(5, 2);
                                if (D.ToDecimal() - 1 == 0)
                                {
                                    if (M.ToDecimal() <= 1)
                                    {
                                        //等於1，代表為庫存年度第一個月，E.g.系統年度103，使用者起始日下1030101，若在減一日就會出問題,所以帶開帳期初
                                        cm.CommandText = "select ItFirCost from item where itno = @itno";
                                    }
                                    else
                                    {
                                        M = (M.ToDecimal() - 1).ToString().PadLeft(2, '0');
                                        cm.CommandText = "select avgcost" + M + " from itemcost where itno = @itno";
                                    }
                                    成本 = cm.ExecuteScalar().ToDecimal("f" + Common.M);
                                }
                                #endregion
                            }
                            else
                            {
                                #region //區間內帶入當約平均成本
                                if (rdAvgByAllStk.Checked)
                                {
                                    cm.CommandText = "select avgcost" + AvgcostNum + " from itemcost where itno = @itno";
                                    成本 = cm.ExecuteScalar().ToDecimal();
                                }
                                else
                                {
                                    cm.CommandText = "select avgcost" + AvgcostNum + " from stkcost where itno=@itno and stno=@stno";
                                    成本 = cm.ExecuteScalar().ToDecimal("f" + Common.M);
                                }
                                #endregion
                            }
                            Alltb1.Rows[i]["成本"] = 成本;
                        }
                    }




                }
                #endregion

                this.OpemInfoFrom<FrmItemStock_Rptb>(() =>
                {
                    FrmItemStock_Rptb frm = new FrmItemStock_Rptb();
                    frm.table = Alltb1;
                    frm.list = list;
                    string DateRang = Date.AddLine(date.Text.ToString()) + "～" + Date.AddLine(date1.Text.ToString());
                    frm.DateRange = DateRang;
                    frm.stno = StNo.Text;
                    frm.stname = StName.Text;
                    return frm;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
               
            }
        }

        private string SelectAvgcost(string olddate) //偉任加
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void date_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);

            TextBox tb = sender as TextBox;
            if (tb.TrimTextLenth() == 0)
                return;

            if (Date.ToTWDate(tb.Text).takeString(3).ToDecimal() < Common.Sys_StkYear1)
            {
                MessageBox.Show("輸入的年度不可以小於系統庫存年度", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                e.Cancel = true;
                return;
            }
            if (!tb.IsDateTime())
            {
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                e.Cancel = true;
                if (Common.User_DateTime == 1)
                {
                    if (tb.Name == "date")
                    {
                        tb.Text = Date.GetDateTime(1, false);
                        tb.Text = date.Text.Remove(5) + "01";
                    }
                    else
                    {
                        tb.Text = Date.GetDateTime(1, false);
                    }
                }
                else
                {
                    if (tb.Name == "date1")
                    {
                        tb.Text = Date.GetDateTime(2, false);
                        tb.Text = date1.Text.Remove(6) + "01";
                    }
                    else
                    {
                        tb.Text = Date.GetDateTime(2, false);
                    }
                }
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

            TextBox tb = sender as TextBox;
            if (tb.Text == "")
                return;

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                tb.Text = row["itno"].ToString().Trim();
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
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["累進數量"] = "0".ToDecimal();
                        dr["累進成本"] = "0".ToDecimal();
                        dr["結餘數量"] = 0;
                        tbtemp.Rows.Add(dr);

                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo1 + "+";
                        dr["數量"] = tb.Rows[i]["數量"].ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["累進數量"] = "0".ToDecimal();
                        dr["累進成本"] = "0".ToDecimal();
                        dr["結餘數量"] = "0".ToDecimal();
                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                    else
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["累進數量"] = "0".ToDecimal();
                        dr["累進成本"] = "0".ToDecimal();
                        dr["結餘數量"] = "0".ToDecimal();
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
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["累進數量"] = "0".ToDecimal();
                        dr["累進成本"] = "0".ToDecimal();
                        dr["結餘數量"] = "0".ToDecimal();
                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo1 + "+";
                        dr["數量"] = tb.Rows[i]["數量"].ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["累進數量"] = "0".ToDecimal();
                        dr["累進成本"] = "0".ToDecimal();
                        dr["結餘數量"] = "0".ToDecimal();
                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
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
                        dr = tbtemp.NewRow();
                        dr["成本"] = tb.Rows[i]["成本"].ToString();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo1;
                        dr["數量"] = tb.Rows[i]["數量"].ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = tb.Rows[i]["數量"].ToString();
                        dr["累進成本"] = dr["累進數量"].ToDecimal() * dr["成本"].ToDecimal();

                        tbtemp.Rows.Add(dr);

                        if (tb.Rows[i]["ittrait"].ToString() == "3")
                        {
                            dr = tbtemp.NewRow();
                            dr["成本"] = tb.Rows[i]["成本"].ToString();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                            dr["單據"] = memo;
                            dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                            dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                            dr["倉庫2"] = "";
                            dr["倉庫編2"] = "";
                            dr["結餘數量"] = "0".ToDecimal();

                            dr["累進數量"] = -1* tb.Rows[i]["數量"].ToDecimal();
                            dr["累進成本"] = dr["累進數量"].ToDecimal() * dr["成本"].ToDecimal();

                            tbtemp.Rows.Add(dr);
                        }

                        tbtemp.AcceptChanges();
                    }
                    else
                    {
                        dr = tbtemp.NewRow();
                        dr["成本"] = tb.Rows[i]["成本"].ToString();

                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo1;
                        dr["數量"] = tb.Rows[i]["數量"].ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = tb.Rows[i]["數量"].ToString();
                        dr["累進成本"] = dr["累進數量"].ToDecimal() * dr["成本"].ToDecimal();

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
                    if (tb.Rows[i]["倉庫1"].ToString() != "" && tb.Rows[i]["倉庫2"].ToString() != "")
                    {
                        if (tb.Rows[i]["倉庫1"].ToString().Trim() == StName.Text.Trim())
                        {
                            dr = tbtemp.NewRow();
                            dr["成本"] = tb.Rows[i]["成本"].ToString();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                            dr["單據"] = memo1;
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                            dr["倉庫2"] = "";
                            dr["倉庫編2"] = "";
                            dr["結餘數量"] = "0".ToDecimal();

                            dr["累進數量"] = tb.Rows[i]["數量"].ToString();
                            dr["累進成本"] = dr["累進數量"].ToDecimal() * dr["成本"].ToDecimal();

                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
                        }
                        if (tb.Rows[i]["倉庫2"].ToString().Trim() == StName.Text.Trim())
                        {
                            if (tb.Rows[i]["ittrait"].ToString() == "3")
                            {
                                dr = tbtemp.NewRow();
                                dr["成本"] = tb.Rows[i]["成本"].ToString();
                                dr["單號"] = tb.Rows[i]["單號"].ToString();
                                dr["itno"] = tb.Rows[i]["itno"].ToString();
                                dr["itname"] = tb.Rows[i]["itname"].ToString();
                                dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                                dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                                dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                                dr["單據"] = memo;
                                dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                                dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                                dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                                dr["倉庫2"] = "";
                                dr["倉庫編2"] = "";
                                dr["結餘數量"] = "0".ToDecimal();

                                dr["累進數量"] = -1*tb.Rows[i]["數量"].ToDecimal();
                                dr["累進成本"] = dr["累進數量"].ToDecimal() * dr["成本"].ToDecimal();

                                tbtemp.Rows.Add(dr);
                                tbtemp.AcceptChanges();
                            }
                        }
                    }
                    else
                    {
                        if (tb.Rows[i]["倉庫1"].ToString().Trim() == StName.Text.Trim())
                        {
                            dr = tbtemp.NewRow();
                            dr["成本"] = tb.Rows[i]["成本"].ToString();
                            dr["單號"] = tb.Rows[i]["單號"].ToString();
                            dr["itno"] = tb.Rows[i]["itno"].ToString();
                            dr["itname"] = tb.Rows[i]["itname"].ToString();
                            dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                            dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                            dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                            dr["單據"] = memo1;
                            dr["數量"] = tb.Rows[i]["數量"].ToString();
                            dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                            dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                            dr["倉庫2"] = "";
                            dr["倉庫編2"] = "";
                            dr["結餘數量"] = "0".ToDecimal();

                            dr["累進數量"] = tb.Rows[i]["數量"].ToString();
                            dr["累進成本"] = dr["累進數量"].ToDecimal() * dr["成本"].ToDecimal();

                            tbtemp.Rows.Add(dr);
                            tbtemp.AcceptChanges();
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
                        dr = tbtemp.NewRow();
                        dr["成本"] = tb.Rows[i]["成本"].ToString();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo;
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                }
                if (StNo.Text != "")
                {
                    if (StName.Text == tb.Rows[i]["倉庫2"].ToString())
                    {
                        dr = tbtemp.NewRow();
                        dr["成本"] = tb.Rows[i]["成本"].ToString();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo;
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
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
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["結餘數量"] = 0;

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);

                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo1 + "+";
                        dr["數量"] = tb.Rows[i]["數量"].ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                    else
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

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
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo1 + "+";
                        dr["數量"] = tb.Rows[i]["數量"].ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
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
                    dr = tbtemp.NewRow();
                    dr["單號"] = tb.Rows[i]["單號"].ToString();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["itname"] = tb.Rows[i]["itname"].ToString();
                    dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                    dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                    dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                    dr["單據"] = memo + "+";
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                    dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                    dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                    dr["倉庫2"] = "";
                    dr["倉庫編2"] = "";
                    dr["成本"] = "0".ToDecimal();
                    dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                    dr["結餘數量"] = 0;

                    dr["累進數量"] = 0;
                    dr["累進成本"] = 0;

                    tbtemp.Rows.Add(dr);

                    dr = tbtemp.NewRow();
                    dr["單號"] = tb.Rows[i]["單號"].ToString();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["itname"] = tb.Rows[i]["itname"].ToString();
                    dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                    dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                    dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                    dr["單據"] = memo + "-";
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                    dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                    dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                    dr["倉庫2"] = "";
                    dr["倉庫編2"] = "";
                    dr["成本"] = "0".ToDecimal();
                    dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                    dr["結餘數量"] = "0".ToDecimal();

                    dr["累進數量"] = 0;
                    dr["累進成本"] = 0;

                    tbtemp.Rows.Add(dr);
                    tbtemp.AcceptChanges();
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "+";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
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
                    dr = tbtemp.NewRow();
                    dr["單號"] = tb.Rows[i]["單號"].ToString();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["itname"] = tb.Rows[i]["itname"].ToString();
                    dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                    dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                    dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                    dr["單據"] = memo + "-";
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                    dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                    dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                    dr["倉庫2"] = "";
                    dr["倉庫編2"] = "";
                    dr["成本"] = "0".ToDecimal();
                    dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                    dr["結餘數量"] = 0;

                    dr["累進數量"] = 0;
                    dr["累進成本"] = 0;

                    tbtemp.Rows.Add(dr);

                    dr = tbtemp.NewRow();
                    dr["單號"] = tb.Rows[i]["單號"].ToString();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["itname"] = tb.Rows[i]["itname"].ToString();
                    dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                    dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                    dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                    dr["單據"] = memo + "+";
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                    dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                    dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                    dr["倉庫2"] = "";
                    dr["倉庫編2"] = "";
                    dr["成本"] = "0".ToDecimal();
                    dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                    dr["結餘數量"] = "0".ToDecimal();

                    dr["累進數量"] = 0;
                    dr["累進成本"] = 0;

                    tbtemp.Rows.Add(dr);
                    tbtemp.AcceptChanges();
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "+";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
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
                    dr = tbtemp.NewRow();
                    dr["單號"] = tb.Rows[i]["單號"].ToString();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["itname"] = tb.Rows[i]["itname"].ToString();
                    dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                    dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                    dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                    dr["單據"] = memo + "-";
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * -1).ToString();
                    dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                    dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                    dr["倉庫2"] = "";
                    dr["倉庫編2"] = "";
                    dr["成本"] = "0".ToDecimal();
                    dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                    dr["結餘數量"] = 0;

                    dr["累進數量"] = 0;
                    dr["累進成本"] = 0;

                    tbtemp.Rows.Add(dr);

                    dr = tbtemp.NewRow();
                    dr["單號"] = tb.Rows[i]["單號"].ToString();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["itname"] = tb.Rows[i]["itname"].ToString();
                    dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                    dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                    dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                    dr["單據"] = memo + "+";
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                    dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                    dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                    dr["倉庫2"] = "";
                    dr["倉庫編2"] = "";
                    dr["成本"] = "0".ToDecimal();
                    dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                    dr["結餘數量"] = "0".ToDecimal();

                    dr["累進數量"] = 0;
                    dr["累進成本"] = 0;

                    tbtemp.Rows.Add(dr);
                    tbtemp.AcceptChanges();
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * -1).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "+";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
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
                    dr = tbtemp.NewRow();
                    dr["單號"] = tb.Rows[i]["單號"].ToString();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["itname"] = tb.Rows[i]["itname"].ToString();
                    dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                    dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                    dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                    dr["單據"] = memo + "+";
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                    dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                    dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                    dr["倉庫2"] = "";
                    dr["倉庫編2"] = "";
                    dr["成本"] = "0".ToDecimal();
                    dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                    dr["結餘數量"] = 0;

                    dr["累進數量"] = 0;
                    dr["累進成本"] = 0;

                    tbtemp.Rows.Add(dr);

                    dr = tbtemp.NewRow();
                    dr["單號"] = tb.Rows[i]["單號"].ToString();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["itname"] = tb.Rows[i]["itname"].ToString();
                    dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                    dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                    dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                    dr["單據"] = memo + "-";
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                    dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                    dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                    dr["倉庫2"] = "";
                    dr["倉庫編2"] = "";
                    dr["成本"] = "0".ToDecimal();
                    dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                    dr["結餘數量"] = "0".ToDecimal();

                    dr["累進數量"] = 0;
                    dr["累進成本"] = 0;

                    tbtemp.Rows.Add(dr);
                    tbtemp.AcceptChanges();
                }
            }
            if (StNo.Text != "")
            {
                tbtemp = tb.Copy();
                tbtemp.Clear();
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (tb.Rows[i]["倉庫1"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "+";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal()).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫1"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編1"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                    if (tb.Rows[i]["倉庫2"].ToString().Trim() == StName.Text.Trim())
                    {
                        dr = tbtemp.NewRow();
                        dr["單號"] = tb.Rows[i]["單號"].ToString();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["itname"] = tb.Rows[i]["itname"].ToString();
                        dr["itunit"] = tb.Rows[i]["itunit"].ToString();
                        dr["日期1"] = tb.Rows[i]["日期1"].ToString();
                        dr["日期2"] = tb.Rows[i]["日期2"].ToString();
                        dr["單據"] = memo + "-";
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["倉庫1"] = tb.Rows[i]["倉庫2"].ToString();
                        dr["倉庫編1"] = tb.Rows[i]["倉庫編2"].ToString();
                        dr["倉庫2"] = "";
                        dr["倉庫編2"] = "";
                        dr["成本"] = "0".ToDecimal();
                        dr["客戶"] = tb.Rows[i]["客戶"].ToString();
                        dr["結餘數量"] = "0".ToDecimal();

                        dr["累進數量"] = 0;
                        dr["累進成本"] = 0;

                        tbtemp.Rows.Add(dr);
                        tbtemp.AcceptChanges();
                    }
                }
            }
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
                    var qty = tb.Rows[i]["qty"].ToDecimal();
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
                        cmd.CommandText = "select ItFirCost from item where itno=@itno";
                        var rw = cmd.ExecuteScalar().ToDecimal();
                        realcost = itpkgqty * rw;
                    }

                    tb.Rows[i]["累進成本"] = (qty * realcost);
                    tb.Rows[i]["成本"] = realcost;
                    temprsale.Clear();
                }
            }
        }

    }
}
