using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S5
{
    public partial class FrmItemCostNew : Formbase
    {
        string DateBegin = "";
        string DateEnd = "";
        delegate void mydelegate(string msg);

        public FrmItemCostNew()
        {
            InitializeComponent();
            labelT2.Text = "Message:";

            var d = Date.GetDateTime(Common.User_DateTime);
            mDate.MaxLength = Common.User_DateTime == 1 ? 5 : 6;
            mDate.Text = Common.User_DateTime == 1 ? d.takeString(5) : d.takeString(6);

            if (Common.User_DateTime == 1)
            {
                DateBegin = Common.Sys_StkYear1 + "0101";
                DateEnd = Common.Sys_StkYear1 + 1 + "1231";
            }
            else
            {
                DateBegin = Common.Sys_StkYear2 + "0101";
                DateEnd = Common.Sys_StkYear2 + 1 + "1231";
            }
        }

        private void FrmItemCostNew_Load(object sender, EventArgs e)
        {
            sqlConnection1.ConnectionString = Common.sqlConnString;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void mDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;

            TextBox tx = new TextBox();
            tx.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            tx.Text = mDate.Text + "01";

            if (!tx.IsDateTime())
            {
                e.Cancel = true;
                mDate.SelectAll();
                MessageBox.Show("日期格式錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.CompareOrdinal(tx.Text, DateBegin) < 0 || string.CompareOrdinal(tx.Text, DateEnd) > 0)
            {
                e.Cancel = true;
                mDate.SelectAll();
                MessageBox.Show("日期超過庫存年度！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonSmallT1_Click(object sender, EventArgs e)
        {
            var my = mDate.Text.Substring(0, mDate.Text.Length - 2);
            var mm = mDate.Text.Substring(mDate.Text.Length - 2, 2);
            if (my.ToInteger() == Common.Sys_StkYear1) bar.Maximum = mm.ToInteger() + 1;
            else
            {
                bar.Maximum = 13 + mm.ToInteger();
            }

            btnBrow.Enabled = false;
            btnExit.Enabled = false;
            buttonSmallT1.Enabled = false;
            buttonSmallT2.Enabled = false;

            bar.Value = 0;
            labelT2.Text = "Message:";
            Thread t = new Thread(new ThreadStart(B));
            t.IsBackground = true;
            t.Start();
        }

        private void buttonSmallT2_Click(object sender, EventArgs e)
        {
            btnBrow.Enabled = false;
            btnExit.Enabled = false;
            buttonSmallT1.Enabled = false;
            buttonSmallT2.Enabled = false;

            bar.Maximum = 25;

            bar.Value = 0;
            labelT2.Text = "Message:";
            Thread t = new Thread(new ThreadStart(C));
            t.IsBackground = true;
            t.Start();
        }


        DataTable tempbshop = new DataTable();//取得銷退成本用
        DataTable tempitem = new DataTable(); //取得銷退成本用
        DataTable temprsale = new DataTable();//取得銷退成本用
        DataTable temprsaleall = new DataTable();//取得銷退成本用

        
        List<DataTable> liICost = new List<DataTable>();
        List<DataTable> liSCost = new List<DataTable>();

        DataTable ADDQty = new DataTable();
        DataTable AddCost = new DataTable();
        DataTable OtherQty = new DataTable();

        DataTable AddQtyAll = new DataTable();
        DataTable AddCostAll = new DataTable();
        DataTable OtherQtyAll = new DataTable();

        void ShowMessageTwo(object msg)
        {
            if (InvokeRequired)
            {
                Invoke(new mydelegate(ShowMessageTwo), new object[] { msg });
                return;
            }
            this.labelT2.Text = "Message: " + msg.ToString();
            if (msg.ToString().Trim() == "【成本計算】完成")
            {
                btnBrow.Enabled = btnExit.Enabled = true;
                buttonSmallT1.Enabled = true;
                buttonSmallT2.Enabled = true;
            }
        }

        void ShowMessageBar(object msg)
        {
            if (InvokeRequired)
            {
                Invoke(new mydelegate(ShowMessageBar), new object[] { msg });
                return;
            }
            if (bar.Value <= (bar.Maximum - 1)) this.bar.Value += 1;
        }

        void buttonOK(object msg)
        {
            if (InvokeRequired)
            {
                Invoke(new mydelegate(buttonOK), new object[] { msg });
                return;
            }
            btnBrow.Enabled = btnExit.Enabled = true;
            buttonSmallT1.Enabled = true;
            buttonSmallT2.Enabled = true;
        }

        void B()
        {
            bool IsBreak = false;
            try
            {
                tempbshop.Clear();//取得銷退成本用
                tempitem.Clear();//取得銷退成本用

                liICost.ForEach(t => t.Dispose());
                liICost.Clear();
                liSCost.ForEach(t => t.Dispose());
                liSCost.Clear();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    if (cn.State != ConnectionState.Open) cn.Open();
                    cmd.CommandText = " select COUNT(*) from item ";
                    var itAll = cmd.ExecuteScalar().ToInteger();
                    cmd.CommandText = "select (select COUNT(*) from item)*(select COUNT(*) from stkroom) ";
                    var stkAll = cmd.ExecuteScalar().ToInteger();

                    cmd.CommandText = new itemFirst().GetCommandText();
                    for (int i = 0; i < itAll; i += 30000)
                    {
                        DataTable tb = new DataTable();
                        da.Fill(i, 30000, tb);
                        if (!tb.Columns.Contains("begqty25")) tb.Columns.Add("begqty25", typeof(Decimal));
                        if (!tb.Columns.Contains("begcost25")) tb.Columns.Add("begcost25", typeof(Decimal));
                        liICost.Add(tb);
                    }
                    cmd.CommandText = new stkFirst().GetCommandText();
                    for (int i = 0; i < stkAll; i += 30000)
                    {
                        DataTable tb = new DataTable();
                        da.Fill(i, 30000, tb);
                        if (!tb.Columns.Contains("begqty25")) tb.Columns.Add("begqty25", typeof(Decimal));
                        if (!tb.Columns.Contains("begcost25")) tb.Columns.Add("begcost25", typeof(Decimal));
                        liSCost.Add(tb);
                    }

                    cmd.CommandText = "select bsdate,itno,itunit,itpkgqty,RealCost from bshopd order by bsdate DESC";
                    da.Fill(tempbshop);
                    cmd.CommandText = "select itno,ItFirCost from item";
                    da.Fill(tempitem);
                }
                Invoke(new mydelegate(ShowMessageTwo), new object[] { " 成本計算中..." });
                //
                var dd = Common.User_DateTime == 1 ? Common.Sys_StkYear1.ToString() : Common.Sys_StkYear2.ToString();
                for (int i = 1; i <= 12; i++)
                {
                    var date = Common.Sys_StkYear1 + ((i).ToString().PadLeft(2, '0'));

                    成本計算(date, i.ToString().PadLeft(2, '0'), (i + 1).ToString().PadLeft(2, '0'));
                    Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });

                    if (date == mDate.Text)
                    {
                        IsBreak = true; break;
                    }
                }

                dd = Common.User_DateTime == 1 ? (Common.Sys_StkYear1 + 1).ToString() : (Common.Sys_StkYear2 + 1).ToString();
                if (IsBreak == false)
                {
                    for (int i = 13; i <= 24; i++)
                    {
                        var date = (Common.Sys_StkYear1 + 1) + ((i - 12).ToString().PadLeft(2, '0'));

                        成本計算(date, (i).ToString().PadLeft(2, '0'), (i + 1).ToString().PadLeft(2, '0'));
                        Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });

                        if (date == mDate.Text)
                        {
                            IsBreak = true; break;
                        }
                    }
                }

                Thread td = new Thread(new ThreadStart(D));
                td.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Invoke(new mydelegate(buttonOK), new object[] { "    " });
            }
        }

        void C()
        {
            try
            {
                tempbshop.Clear();//取得銷退成本用
                tempitem.Clear();//取得銷退成本用

                liICost.ForEach(t => t.Dispose());
                liICost.Clear();
                liSCost.ForEach(t => t.Dispose());
                liSCost.Clear();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    if (cn.State != ConnectionState.Open) cn.Open();
                    cmd.CommandText = " select COUNT(*) from item ";
                    var itAll = cmd.ExecuteScalar().ToInteger();
                    cmd.CommandText = "select (select COUNT(*) from item)*(select COUNT(*) from stkroom) ";
                    var stkAll = cmd.ExecuteScalar().ToInteger();

                    cmd.CommandText = new itemFirst().GetCommandText();
                    for (int i = 0; i < itAll; i += 30000)
                    {
                        DataTable tb = new DataTable();
                        da.Fill(i, 30000, tb);
                        if (!tb.Columns.Contains("begqty25")) tb.Columns.Add("begqty25", typeof(Decimal));
                        if (!tb.Columns.Contains("begcost25")) tb.Columns.Add("begcost25", typeof(Decimal));
                        liICost.Add(tb);
                    }
                    cmd.CommandText = new stkFirst().GetCommandText();
                    for (int i = 0; i < stkAll; i += 30000)
                    {
                        DataTable tb = new DataTable();
                        da.Fill(i, 30000, tb);
                        if (!tb.Columns.Contains("begqty25")) tb.Columns.Add("begqty25", typeof(Decimal));
                        if (!tb.Columns.Contains("begcost25")) tb.Columns.Add("begcost25", typeof(Decimal));
                        liSCost.Add(tb);
                    }

                    cmd.CommandText = "select bsdate,itno,itunit,itpkgqty,RealCost from bshopd order by bsdate DESC";
                    da.Fill(tempbshop);
                    cmd.CommandText = "select itno,ItFirCost from item";
                    da.Fill(tempitem);
                }
                Invoke(new mydelegate(ShowMessageTwo), new object[] { " 成本計算中..." });
                //
                for (int i = 1; i <= 12; i++)
                {
                    var date = Common.Sys_StkYear1 + ((i).ToString().PadLeft(2, '0'));

                    成本計算(date, i.ToString().PadLeft(2, '0'), (i + 1).ToString().PadLeft(2, '0'));
                    Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });
                }

                for (int i = 13; i <= 24; i++)
                {
                    var date = (Common.Sys_StkYear1 + 1) + ((i - 12).ToString().PadLeft(2, '0'));

                    成本計算(date, (i).ToString().PadLeft(2, '0'), (i + 1).ToString().PadLeft(2, '0'));
                    Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });
                }

                Thread td = new Thread(new ThreadStart(D));
                td.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Invoke(new mydelegate(buttonOK), new object[] { "    " });
            }
        }

        void D()
        {
            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    Invoke(new mydelegate(ShowMessageTwo), new object[] { " 資料儲存中..." });

                    for (int j = 0; j < liSCost.Count; j++)
                    {
                        liSCost[j].AcceptChanges();
                        for (int i = 0; i < liSCost[j].Rows.Count; i++)
                        {
                            liSCost[j].Rows[i].SetAdded();
                        }
                    }

                    for (int j = 0; j < liICost.Count; j++)
                    {
                        liICost[j].AcceptChanges();
                        for (int i = 0; i < liICost[j].Rows.Count; i++)
                        {
                            liICost[j].Rows[i].SetAdded();
                        }
                    }

                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.CommandText = "delete from itemcost;delete from stkcost;";
                    cmd.ExecuteNonQuery();

                    sqlDataAdapter1.InsertCommand.Connection = cn;
                    sqlDataAdapter2.InsertCommand.Connection = cn;
                    sqlDataAdapter1.InsertCommand.Transaction = tn;
                    sqlDataAdapter2.InsertCommand.Transaction = tn;



                    for (int j = 0; j < liICost.Count; j++)
                    {
                        sqlDataAdapter1.Update(liICost[j]);
                    }
                    for (int j = 0; j < liSCost.Count; j++)
                    {
                        sqlDataAdapter2.Update(liSCost[j]);
                    }



                    tn.Commit();
                    Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });
                    Invoke(new mydelegate(ShowMessageTwo), new object[] { "【成本計算】完成" });
                }
                catch (Exception ex)
                {
                    if (tn != null) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    Invoke(new mydelegate(buttonOK), new object[] { "    " });
                }
            }
        }

        class MyClass
        {
            public string stno { get; set; }
            public int add { get; set; }
            public string dtD { get; set; }
            public string dtBom { get; set; }
            public string date { get; set; }
        }
        string 單一累進量(MyClass mc, string date)
        {
            return " Select itno," + mc.stno + ",(" + mc.add + ")*SUM(qty*itpkgqty)累進數量 from " + mc.dtD + " where ittrait <> 1 And " + mc.date + " like '" + date + "%' group by itno," + mc.stno;
        }
        string 組合累進量(MyClass mc, string date)
        {
            return " Select B.itno,A." + mc.stno + ",(" + mc.add + ")*SUM(A.qty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from " + mc.dtD + " where ittrait = 1 And " + mc.date + " like '" + date + "%' )A left join " + mc.dtBom + " B on A.bomid=B.BomID where B.itno is not null group by B.itno,A." + mc.stno;
        }

        string 單一累進量1(MyClass mc, string date)
        {
            return " Select itno,(" + mc.add + ")*SUM(qty*itpkgqty)累進數量 from " + mc.dtD + " where ittrait <> 1 And " + mc.date + " like '" + date + "%' group by itno";
        }
        string 組合累進量1(MyClass mc, string date)
        {
            return " Select B.itno,(" + mc.add + ")*SUM(A.qty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from " + mc.dtD + " where ittrait = 1 And " + mc.date + " like '" + date + "%' )A left join " + mc.dtBom + " B on A.bomid=B.BomID where B.itno is not null group by B.itno";
        }

        string 單一累進量2(MyClass mc, string date) //WeiRen
        {
            return " Select itno,(" + mc.add + ")*SUM(qty*itpkgqty)累進數量 from " + mc.dtD + " where ittrait <> 1 And " + mc.date + " like '" + date + "%' and len(" + mc.stno + ") > 0 group by itno";
        }
        string 組合累進量2(MyClass mc, string date) //WeiRen
        {
            return " Select B.itno,(" + mc.add + ")*SUM(A.qty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from " + mc.dtD + " where ittrait = 1 And " + mc.date + " like '" + date + "%' )A left join " + mc.dtBom + " B on A.bomid=B.BomID where B.itno is not null  and len(" + mc.stno + ") > 0  group by B.itno";
        }

        string 寄庫單一累進量(MyClass mc, string date)
        {
            return " Select itno," + mc.stno + ",(" + mc.add + ")*SUM(inqty*itpkgqty)累進數量 from " + mc.dtD + " where ittrait <> 1 And " + mc.date + " like '" + date + "%' group by itno," + mc.stno;
        }
        string 寄庫組合累進量(MyClass mc, string date)
        {
            return " Select B.itno,A." + mc.stno + ",(" + mc.add + ")*SUM(A.inqty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from " + mc.dtD + " where ittrait = 1 And " + mc.date + " like '" + date + "%' )A left join " + mc.dtBom + " B on A.bomid=B.BomID where B.itno is not null group by B.itno,A." + mc.stno;
        }
        string 領庫單一累進量(MyClass mc, string date)
        {
            return " Select itno," + mc.stno + ",(" + mc.add + ")*SUM(ouqty*itpkgqty)累進數量 from " + mc.dtD + " where ittrait <> 1 And " + mc.date + " like '" + date + "%' group by itno," + mc.stno;
        }
        string 領庫組合累進量(MyClass mc, string date)
        {
            return " Select B.itno,A." + mc.stno + ",(" + mc.add + ")*SUM(A.ouqty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from " + mc.dtD + " where ittrait = 1 And " + mc.date + " like '" + date + "%' )A left join " + mc.dtBom + " B on A.bomid=B.BomID where B.itno is not null group by B.itno,A." + mc.stno;
        }

        string 寄庫單一累進量1(MyClass mc, string date)
        {
            return " Select itno,(" + mc.add + ")*SUM(inqty*itpkgqty)累進數量 from " + mc.dtD + " where ittrait <> 1 And " + mc.date + " like '" + date + "%' group by itno";
        }
        string 寄庫組合累進量1(MyClass mc, string date)
        {
            return " Select B.itno,(" + mc.add + ")*SUM(A.inqty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from " + mc.dtD + " where ittrait = 1 And " + mc.date + " like '" + date + "%' )A left join " + mc.dtBom + " B on A.bomid=B.BomID where B.itno is not null group by B.itno";
        }
        string 領庫單一累進量1(MyClass mc, string date)
        {
            return " Select itno,(" + mc.add + ")*SUM(ouqty*itpkgqty)累進數量 from " + mc.dtD + " where ittrait <> 1 And " + mc.date + " like '" + date + "%' group by itno";
        }
        string 領庫組合累進量1(MyClass mc, string date)
        {
            return " Select B.itno,(" + mc.add + ")*SUM(A.ouqty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from " + mc.dtD + " where ittrait = 1 And " + mc.date + " like '" + date + "%' )A left join " + mc.dtBom + " B on A.bomid=B.BomID where B.itno is not null group by B.itno";
        }

        void 成本計算(string date, string index, string nindex)
        {
            try
            {
                ADDQty.Clear();
                AddCost.Clear();
                OtherQty.Clear();
                temprsale.Clear();

                AddQtyAll.Clear();
                AddCostAll.Clear();
                OtherQtyAll.Clear();
                temprsaleall.Clear();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    #region 分倉
                    cmd.CommandText = ""
                    + " select C.itno,C.stno,SUM(累進數量)累進數量 from "
                    + " ("
                        //進貨+ (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stno", dtD = "bshopd", dtBom = "", date = "bsdate" }, date) + " union all"
                        //進退- (單一組裝)
                        + 單一累進量(new MyClass { add = -1, stno = "stno", dtD = "rshopd", dtBom = "", date = "bsdate" }, date) + " union all"

                        //銷退+ (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stno", dtD = "rsaled", dtBom = "RSaleBom", date = "sadate" }, date) + " union all"
                        //銷退+ (組合子件)
                        + 組合累進量(new MyClass { add = 1, stno = "stno", dtD = "rsaled", dtBom = "RSaleBom", date = "sadate" }, date) + " union all"

                        //入庫+ (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stnoi", dtD = "garnerd", dtBom = "GarnBom", date = "gadate" }, date) + " union all"
                        //入庫- (單一)
                        + " select itno,stnoo stno,(-1)*SUM(qty*itpkgqty)累進數量 from garnerd where ittrait = 3 And gadate like '" + date + "%' group by itno,stnoo" + " union all"
                        //入庫- (組裝組合子件)
                        //+ " select B.itno,A.stnoo stno,(-1)*SUM(A.qty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from garnerd where ittrait = 2 And gadate like '" + date + "%' )A left join garnbom B on A.bomid=B.BomID where B.itno is not null group by B.itno,A.stnoo" + " union all"

                        //調整± (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stno", dtD = "adjustd", dtBom = "", date = "addate" }, date)

                    + " )C group by C.itno,C.stno";
                    da.Fill(ADDQty);

                    cmd.CommandText = ""
                    + " select C.itno,C.stno,SUM(累進成本)累進成本 from"
                    + " ("
                    + " select itno,stno,SUM(qty*realcost)累進成本 from bshopd where ittrait <> 1 And bsdate like '" + date + "%' group by itno,stno" + " union all"
                    + " select itno,stno,(-1)*SUM(qty*realcost)累進成本 from rshopd where ittrait <> 1 And bsdate like '" + date + "%' group by itno,stno" + " union all"
                    + " select itno,stnoi stno,SUM(qty*costb)累進成本 from garnerd where ittrait <> 1 And gadate like '" + date + "%' and len(stnoi) > 0 group by itno,stnoi" + " union all"
                    + " select itno,stnoo stno,(-1)*SUM(qty*costb)累進成本 from garnerd where ittrait = 3  And gadate like '" + date + "%' and len(stnoo) > 0 group by itno,stnoo" + " union all"
                    + " select itno,stno,SUM(qty*costb)累進成本 from adjustd where ittrait <> 1 And addate like '" + date + "%' group by itno,stno"
                    + " )C group by C.itno,C.stno";
                    da.Fill(AddCost);

                    //取得銷退單一組裝and組合的子件的實際成本,並計算累計成本
                    cmd.CommandText = ""
                    + " select itno,itunit,qty,itpkgqty,realcost=0.0,stno from rsaled where ittrait <> 1 And sadate like '" + date + "%' "
                    + " union all"
                    + " select B.itno,B.itunit,(A.qty*A.itpkgqty*((B.itqty*B.itpkgqty)/B.itpareprs)) qty,B.itpkgqty,realcost=0.0,A.stno from (select * from rsaled where ittrait = 1 And sadate like '" + date + "%')A left join RSaleBom B on A.bomid = B.bomid";
                    da.Fill(temprsale);
                    for (int i = 0; i < temprsale.Rows.Count; i++)
                    {
                        var itno = temprsale.Rows[i]["itno"].ToString().Trim();
                        var unit = temprsale.Rows[i]["itunit"].ToString().Trim();
                        var qty = temprsale.Rows[i]["qty"].ToDecimal();
                        var itpkgqty = temprsale.Rows[i]["itpkgqty"].ToDecimal();
                        var stno = temprsale.Rows[i]["stno"].ToString().Trim();
                        var realcost = 0M;
                        var row = tempbshop.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno);
                        if (row != null)
                        {
                            if (row["itunit"].ToString().Trim() == unit && row["itpkgqty"].ToDecimal() == itpkgqty)
                            {
                                realcost = row["realcost"].ToDecimal();
                            }
                            else
                            {
                                if (row["itpkgqty"].ToDecimal() == 0) row["itpkgqty"] = 1;
                                realcost = itpkgqty * (row["realcost"].ToDecimal() / row["itpkgqty"].ToDecimal());
                            }
                        }
                        else
                        {
                            var rw = tempitem.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno);
                            if (rw == null) realcost = 0;
                            else
                            {
                                realcost = itpkgqty * rw["ItFirCost"].ToDecimal();
                            }
                        }

                        var w = AddCost.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno && r["stno"].ToString().Trim() == stno);
                        if (w != null)
                        {
                            w["累進成本"] = w["累進成本"].ToDecimal() + (qty * realcost);
                        }
                        else
                        {
                            var datarow = AddCost.NewRow();
                            datarow["itno"] = itno;
                            datarow["stno"] = stno;
                            datarow["累進成本"] = qty * realcost;
                            AddCost.Rows.Add(datarow);
                        }
                    }

                    for (int j = 0; j < liSCost.Count; j++)
                    {
                        for (int i = 0; i < liSCost[j].Rows.Count; i++)
                        {
                            var itno = liSCost[j].Rows[i]["itno"].ToString().Trim();
                            var stno = liSCost[j].Rows[i]["stno"].ToString().Trim();

                            if (ADDQty.Rows.Count > 0)
                            {
                                var row = ADDQty.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno && r["stno"].ToString().Trim() == stno);
                                if (row == null)
                                {
                                    liSCost[j].Rows[i]["addqty" + index] = 0;
                                }
                                else
                                {
                                    liSCost[j].Rows[i]["addqty" + index] = row["累進數量"].ToDecimal("f2");
                                }
                            }
                            else
                            {
                                liSCost[j].Rows[i]["addqty" + index] = 0;
                            }

                            if (AddCost.Rows.Count > 0)
                            {
                                var row = AddCost.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno && r["stno"].ToString().Trim() == stno);
                                if (row == null)
                                {

                                    liSCost[j].Rows[i]["addcost" + index] = 0;
                                }
                                else
                                {
                                    liSCost[j].Rows[i]["addcost" + index] = row["累進成本"].ToDecimal("f6");
                                }
                            }
                            else
                            {
                                liSCost[j].Rows[i]["addcost" + index] = 0;
                            }
                        }
                    }

                    ////1040106註解jbs庫存計算方式(會影響到結轉的結餘量)
                    ////銷貨- = 單一組裝(-) 組合子件(-)
                    ////銷退+ = 單一組裝(+) 組合子件(+)
                    ////進貨+ = 單一組裝(+)
                    ////進退- = 單一組裝(-)
                    ////領料- = 單一組裝(-) 組合子件(-)
                    ////領料+ = 單一組裝(+) 組合子件(+)
                    ////入庫+ = 單一組裝(+) 
                    ////入庫- = 單一(-)     組裝子件(-)
                    ////調撥- = 單一組裝(-) 組合子件(-)
                    ////調撥+ = 單一組裝(+) 組合子件(+)
                    ////借出- = 單一組裝(-) 組合子件(-)
                    ////借出+ = 單一組裝(+) 組合子件(+)
                    ////還入+ = 單一組裝(+) 組合子件(+)
                    ////還入- = 單一組裝(-) 組合子件(-)
                    ////借入+ = 單一組裝(+) 組合子件(+)
                    ////借入- = 單一組裝(-) 組合子件(-)
                    ////還出- = 單一組裝(-) 組合子件(-)
                    ////還出+ = 單一組裝(+) 組合子件(+)
                    ////寄庫+ = 單一組裝(+) 組合子件(+)
                    ////領庫- = 單一組裝(-) 組合子件(-)
                    ////調整+- =單一組裝(+-)

                    //次月期初數量 = 當月期初+當月累進(-銷貨數量-領料數量±調撥數量(含 領料+ 入庫-))
                    cmd.CommandText = ""
                    + " select C.itno,C.stno,SUM(累進數量)累進數量 from "
                    + " ("
                        //銷貨- (單一組裝)
                        + 單一累進量(new MyClass { add = -1, stno = "stno", dtD = "saled", dtBom = "salebom", date = "sadate" }, date) + " union all"
                        //銷貨- (組合子件)
                        + 組合累進量(new MyClass { add = -1, stno = "stno", dtD = "saled", dtBom = "salebom", date = "sadate" }, date) + " union all"

                        //領料- (單一組裝)
                        + 單一累進量(new MyClass { add = -1, stno = "stnoo", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"
                        //領料- (組合子件)
                        + 組合累進量(new MyClass { add = -1, stno = "stnoo", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"
                        //領料+ (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stnoi", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"
                        //領料+ (組合子件)
                        + 組合累進量(new MyClass { add = 1, stno = "stnoi", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"

                        //調撥- (單一組裝)
                        + 單一累進量(new MyClass { add = -1, stno = "stnoo", dtD = "allotd", dtBom = "       ", date = "aldate" }, date) + " union all"
                        //調撥- (組合子件)
                        + 組合累進量(new MyClass { add = -1, stno = "stnoo", dtD = "allotd", dtBom = "AlloBom", date = "aldate" }, date) + " union all"
                        //調撥+ (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stnoi", dtD = "allotd", dtBom = "       ", date = "aldate" }, date) + " union all"
                        //調撥+ (組合子件)
                        + 組合累進量(new MyClass { add = 1, stno = "stnoi", dtD = "allotd", dtBom = "AlloBom", date = "aldate" }, date) + " union all"

                        //借出- (單一組裝)
                        + 單一累進量(new MyClass { add = -1, stno = "stno", dtD = "lendd", dtBom = "lendbom", date = "ledate" }, date) + " union all"
                        //借出- (組合子件)
                        + 組合累進量(new MyClass { add = -1, stno = "stno", dtD = "lendd", dtBom = "lendbom", date = "ledate" }, date) + " union all"
                        //借出+ (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stnoi", dtD = "lendd", dtBom = "lendbom", date = "ledate" }, date) + " union all"
                        //借出+ (組合子件)
                        + 組合累進量(new MyClass { add = 1, stno = "stnoi", dtD = "lendd", dtBom = "lendbom", date = "ledate" }, date) + " union all"

                        //還入+ (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stno", dtD = "rlendd", dtBom = "rlendbom", date = "ledate" }, date) + " union all"
                        //還入+ (組合子件)
                        + 組合累進量(new MyClass { add = 1, stno = "stno", dtD = "rlendd", dtBom = "rlendbom", date = "ledate" }, date) + " union all"
                        //還入- (單一組裝)
                        + 單一累進量(new MyClass { add = -1, stno = "stnoi", dtD = "rlendd", dtBom = "rlendbom", date = "ledate" }, date) + " union all"
                        //還入- (組合子件)
                        + 組合累進量(new MyClass { add = -1, stno = "stnoi", dtD = "rlendd", dtBom = "rlendbom", date = "ledate" }, date) + " union all"

                        //借入+ (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stno", dtD = "BorrD", dtBom = "BorrBom", date = "bodate" }, date) + " union all"
                        //借入+ (組合子件)
                        + 組合累進量(new MyClass { add = 1, stno = "stno", dtD = "BorrD", dtBom = "BorrBom", date = "bodate" }, date) + " union all"
                        //借入- (單一組裝)
                        + 單一累進量(new MyClass { add = -1, stno = "stnoo", dtD = "BorrD", dtBom = "BorrBom", date = "bodate" }, date) + " union all"
                        //借入- (組合子件)
                        + 組合累進量(new MyClass { add = -1, stno = "stnoo", dtD = "BorrD", dtBom = "BorrBom", date = "bodate" }, date) + " union all"

                        //還出- (單一組裝)
                        + 單一累進量(new MyClass { add = -1, stno = "stno", dtD = "rBorrD", dtBom = "rBorrBom", date = "bodate" }, date) + " union all"
                        //還出- (組合子件)
                        + 組合累進量(new MyClass { add = -1, stno = "stno", dtD = "rBorrD", dtBom = "rBorrBom", date = "bodate" }, date) + " union all"
                        //還出+ (單一組裝)
                        + 單一累進量(new MyClass { add = 1, stno = "stnoo", dtD = "rBorrD", dtBom = "rBorrBom", date = "bodate" }, date) + " union all"
                        //還出+ (組合子件)
                        + 組合累進量(new MyClass { add = 1, stno = "stnoo", dtD = "rBorrD", dtBom = "rBorrBom", date = "bodate" }, date) + " union all"

                        //寄庫+ (單一組裝)
                        + 寄庫單一累進量(new MyClass { add = 1, stno = "stno", dtD = "instkd", dtBom = "InStkBOM", date = "indate" }, date) + " union all"
                        //寄庫+ (組合子件)
                        + 寄庫組合累進量(new MyClass { add = 1, stno = "stno", dtD = "instkd", dtBom = "InStkBOM", date = "indate" }, date) + " union all"

                        //領庫- (單一組裝)
                        + 領庫單一累進量(new MyClass { add = -1, stno = "stno", dtD = "oustkd", dtBom = "OuStkBOM", date = "oudate" }, date) + " union all"
                        //領庫- (組合子件)
                        + 領庫組合累進量(new MyClass { add = -1, stno = "stno", dtD = "oustkd", dtBom = "OuStkBOM", date = "oudate" }, date)
                        //入庫- (組裝組合子件)
                        + "  union all select B.itno,A.stnoo stno,(-1)*SUM(A.qty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from garnerd where ittrait = 2 And gadate like '" + date + "%' )A left join garnbom B on A.bomid=B.BomID where B.itno is not null group by B.itno,A.stnoo " 
                    + " )C group by C.itno,C.stno";
                    da.Fill(OtherQty);
                    //
                    var bqty = 0M;
                    var aqty = 0M;
                    var bcost = 0M;
                    var acost = 0M;

                    for (int j = 0; j < liSCost.Count; j++)
                    {
                        for (int i = 0; i < liSCost[j].Rows.Count; i++)
                        {
                            bqty = liSCost[j].Rows[i]["begqty" + index].ToDecimal();
                            bcost = liSCost[j].Rows[i]["begcost" + index].ToDecimal();
                            aqty = liSCost[j].Rows[i]["addqty" + index].ToDecimal();
                            acost = liSCost[j].Rows[i]["addcost" + index].ToDecimal();
                            if ((bqty + aqty) == 0)
                            {
                                liSCost[j].Rows[i]["avgcost" + index] = bcost.ToDecimal("f6");
                                liSCost[j].Rows[i]["begcost" + nindex] = bcost.ToDecimal("f6");
                            }
                            else
                            {
                                liSCost[j].Rows[i]["begcost" + nindex] = liSCost[j].Rows[i]["avgcost" + index] = (((bqty * bcost) + acost) / (bqty + aqty)).ToDecimal("f6");
                            }

                            var itno = liSCost[j].Rows[i]["itno"].ToString().Trim();
                            var stno = liSCost[j].Rows[i]["stno"].ToString().Trim();
                            var row = OtherQty.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno && r["stno"].ToString().Trim() == stno);
                            if (row == null)
                            {
                                liSCost[j].Rows[i]["begqty" + nindex] = bqty + aqty;
                            }
                            else
                            {
                                liSCost[j].Rows[i]["begqty" + nindex] = bqty + aqty + row["累進數量"].ToDecimal();
                            }
                        }
                    }
                    #endregion


                    #region 不分倉
                    cmd.CommandText = ""
                    + " select C.itno,SUM(累進數量)累進數量 from "
                    + " ("

                        //進貨+ (單一組裝)
                        + 單一累進量1(new MyClass { add = 1, stno = "stno", dtD = "bshopd", dtBom = "", date = "bsdate" }, date) + " union all"
                        //進退- (單一組裝)
                        + 單一累進量1(new MyClass { add = -1, stno = "stno", dtD = "rshopd", dtBom = "", date = "bsdate" }, date) + " union all"

                        //銷退+ (單一組裝)
                        + 單一累進量1(new MyClass { add = 1, stno = "stno", dtD = "rsaled", dtBom = "RSaleBom", date = "sadate" }, date) + " union all"
                        //銷退+ (組合子件)
                        + 組合累進量1(new MyClass { add = 1, stno = "stno", dtD = "rsaled", dtBom = "RSaleBom", date = "sadate" }, date) + " union all"

                        //入庫+ (單一組裝) 
                        + " select itno,SUM(qty*itpkgqty)累進數量 from garnerd where len(stnoi)>0 and ittrait <> 1 And gadate like '" + date + "%' group by itno" + " union all"
                        //入庫- (單一)
                        + " select itno,(-1)*SUM(qty*itpkgqty)累進數量 from garnerd where len(stnoo)>0 and ittrait = 3 And gadate like '" + date + "%' group by itno" + " union all"
                        //入庫- (組裝子件)
                        //+ " select B.itno,(-1)*SUM(A.qty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from garnerd where len(stnoo)>0 AND ittrait = 2 And gadate like '" + date + "%' )A left join garnbom B on A.bomid=B.BomID where B.itno is not null group by B.itno" + " union all"

                        //調整± (單一組裝)
                        + 單一累進量1(new MyClass { add = 1, stno = "stno", dtD = "adjustd", dtBom = "", date = "addate" }, date)

                    + " )C group by C.itno";
                    da.Fill(AddQtyAll);

                    cmd.CommandText = ""
                    + " select C.itno,SUM(累進成本)累進成本 from"
                    + " ("
                    + " select itno,SUM(qty*realcost)累進成本 from bshopd where ittrait <> 1 And bsdate like '" + date + "%' group by itno" + " union all"
                    + " select itno,(-1)*SUM(qty*realcost)累進成本 from rshopd where ittrait <> 1 And bsdate like '" + date + "%' group by itno" + " union all"
                    + " select itno,SUM(qty*costb)累進成本 from      garnerd where ittrait <> 1 And gadate like '" + date + "%' and len(stnoi) > 0 group by itno" + " union all"
                    + " select itno,(-1)*SUM(qty*costb)累進成本 from garnerd where ittrait = 3 And gadate like '" + date + "%' and len(stnoo) > 0 group by itno" + " union all"
                    + " select itno,SUM(qty*costb)累進成本 from adjustd where ittrait <> 1 And addate like '" + date + "%' group by itno"
                    + " )C group by C.itno";
                    da.Fill(AddCostAll);

                    //取得銷退單一組裝and組合的子件的實際成本,並計算累計成本
                    cmd.CommandText = ""
                    + " select itno,itunit,qty,itpkgqty,realcost=0.0 from rsaled where ittrait <> 1 And sadate like '" + date + "%' "
                    + " union all"
                    + " select B.itno,B.itunit,(A.qty*A.itpkgqty*((B.itqty*B.itpkgqty)/B.itpareprs)) qty,B.itpkgqty,realcost=0.0 from (select * from rsaled where ittrait = 1 And sadate like '" + date + "%')A left join RSaleBom B on A.bomid = B.bomid";
                    da.Fill(temprsaleall);
                    for (int i = 0; i < temprsaleall.Rows.Count; i++)
                    {
                        var itno = temprsaleall.Rows[i]["itno"].ToString().Trim();
                        var unit = temprsaleall.Rows[i]["itunit"].ToString().Trim();
                        var qty = temprsaleall.Rows[i]["qty"].ToDecimal();
                        var itpkgqty = temprsaleall.Rows[i]["itpkgqty"].ToDecimal();
                        var realcost = 0M;
                        var row = tempbshop.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno);//要取得最小單位成本
                        if (row != null)//如果單位=Unit  包裝數量=itpkgqty 
                        {
                            if (row["itunit"].ToString().Trim() == unit && row["itpkgqty"].ToDecimal() == itpkgqty)
                            {
                                realcost = row["realcost"].ToDecimal();
                            }
                            else
                            {
                                if (row["itpkgqty"].ToDecimal() == 0) row["itpkgqty"] = 1;
                                realcost = itpkgqty * (row["realcost"].ToDecimal() / row["itpkgqty"].ToDecimal());
                            }
                        }
                        else
                        {
                            var rw = tempitem.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno);
                            if (rw == null) realcost = 0;
                            else
                            {
                                realcost = itpkgqty * rw["ItFirCost"].ToDecimal();
                            }
                        }

                        var w = AddCostAll.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno);
                        if (w != null)
                        {
                            w["累進成本"] = w["累進成本"].ToDecimal() + (qty * realcost);
                        }
                        else
                        {
                            var datarow = AddCostAll.NewRow();
                            datarow["itno"] = itno;
                            datarow["累進成本"] = qty * realcost;
                            AddCostAll.Rows.Add(datarow);
                        }
                    }

                    for (int j = 0; j < liICost.Count; j++)
                    {
                        for (int i = 0; i < liICost[j].Rows.Count; i++)
                        {
                            var itno = liICost[j].Rows[i]["itno"].ToString().Trim();

                            var row = AddQtyAll.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno);
                            if (row == null)
                            {
                                liICost[j].Rows[i]["addqty" + index] = 0;
                            }
                            else
                            {
                                liICost[j].Rows[i]["addqty" + index] = row["累進數量"].ToDecimal();
                            }

                            row = AddCostAll.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno);
                            if (row == null)
                            {
                                liICost[j].Rows[i]["addcost" + index] = 0;
                            }
                            else
                            {
                                liICost[j].Rows[i]["addcost" + index] = row["累進成本"].ToDecimal();
                            }
                        }
                    }


                    cmd.CommandText = ""
                    + " select C.itno,SUM(累進數量)累進數量 from "
                    + " ("
                        //銷貨- (單一組裝)
                        + 單一累進量1(new MyClass { add = -1, stno = "stno", dtD = "saled", dtBom = "salebom", date = "sadate" }, date) + " union all"
                        //銷貨- (組合子件)
                        + 組合累進量1(new MyClass { add = -1, stno = "stno", dtD = "saled", dtBom = "salebom", date = "sadate" }, date) + " union all"

                        //領料- (單一組裝)
                        + 單一累進量2(new MyClass { add = -1, stno = "stnoo", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"
                        //領料- (組合子件)
                        + 組合累進量2(new MyClass { add = -1, stno = "stnoo", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"
                        //領料+ (單一組裝)
                        + 單一累進量2(new MyClass { add = 1, stno = "stnoi", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"
                        //領料+ (組合子件)
                        + 組合累進量2(new MyClass { add = 1, stno = "stnoi", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"

                        //調撥- (單一組裝)
                        + 單一累進量1(new MyClass { add = -1, stno = "stnoo", dtD = "allotd", dtBom = "       ", date = "aldate" }, date) + " union all"
                        //調撥- (組合子件)
                        + 組合累進量1(new MyClass { add = -1, stno = "stnoo", dtD = "allotd", dtBom = "AlloBom", date = "aldate" }, date) + " union all"
                        //調撥+ (單一組裝)
                        + 單一累進量1(new MyClass { add = 1, stno = "stnoi", dtD = "allotd", dtBom = "       ", date = "aldate" }, date) + " union all"
                        //調撥+ (組合子件)
                        + 組合累進量1(new MyClass { add = 1, stno = "stnoi", dtD = "allotd", dtBom = "AlloBom", date = "aldate" }, date) + " union all"

                        //借出- (單一組裝)
                        + 單一累進量1(new MyClass { add = -1, stno = "stno", dtD = "lendd", dtBom = "lendbom", date = "ledate" }, date) + " union all"
                        //借出- (組合子件)
                        + 組合累進量1(new MyClass { add = -1, stno = "stno", dtD = "lendd", dtBom = "lendbom", date = "ledate" }, date) + " union all"
                        //借出+ (單一組裝)
                        + 單一累進量1(new MyClass { add = 1, stno = "stnoi", dtD = "lendd", dtBom = "lendbom", date = "ledate" }, date) + " union all"
                        //借出+ (組合子件)
                        + 組合累進量1(new MyClass { add = 1, stno = "stnoi", dtD = "lendd", dtBom = "lendbom", date = "ledate" }, date) + " union all"

                        //還入+ (單一組裝)
                        + 單一累進量1(new MyClass { add = 1, stno = "stno", dtD = "rlendd", dtBom = "rlendbom", date = "ledate" }, date) + " union all"
                        //還入+ (組合子件)
                        + 組合累進量1(new MyClass { add = 1, stno = "stno", dtD = "rlendd", dtBom = "rlendbom", date = "ledate" }, date) + " union all"
                        //還入- (單一組裝)
                        + 單一累進量1(new MyClass { add = -1, stno = "stnoi", dtD = "rlendd", dtBom = "rlendbom", date = "ledate" }, date) + " union all"
                        //還入- (組合子件)
                        + 組合累進量1(new MyClass { add = -1, stno = "stnoi", dtD = "rlendd", dtBom = "rlendbom", date = "ledate" }, date) + " union all"

                        //借入+ (單一組裝)
                        + 單一累進量1(new MyClass { add = 1, stno = "stno", dtD = "BorrD", dtBom = "BorrBom", date = "bodate" }, date) + " union all"
                        //借入+ (組合子件)
                        + 組合累進量1(new MyClass { add = 1, stno = "stno", dtD = "BorrD", dtBom = "BorrBom", date = "bodate" }, date) + " union all"
                        //借入- (單一組裝)
                        + 單一累進量1(new MyClass { add = -1, stno = "stnoo", dtD = "BorrD", dtBom = "BorrBom", date = "bodate" }, date) + " union all"
                        //借入- (組合子件)
                        + 組合累進量1(new MyClass { add = -1, stno = "stnoo", dtD = "BorrD", dtBom = "BorrBom", date = "bodate" }, date) + " union all"

                        //還出- (單一組裝)
                        + 單一累進量1(new MyClass { add = -1, stno = "stno", dtD = "rBorrD", dtBom = "rBorrBom", date = "bodate" }, date) + " union all"
                        //還出- (組合子件)
                        + 組合累進量1(new MyClass { add = -1, stno = "stno", dtD = "rBorrD", dtBom = "rBorrBom", date = "bodate" }, date) + " union all"
                        //還出+ (單一組裝)
                        + 單一累進量1(new MyClass { add = 1, stno = "stnoo", dtD = "rBorrD", dtBom = "rBorrBom", date = "bodate" }, date) + " union all"
                        //還出+ (組合子件)
                        + 組合累進量1(new MyClass { add = 1, stno = "stnoo", dtD = "rBorrD", dtBom = "rBorrBom", date = "bodate" }, date) + " union all"

                        //寄庫+ (單一組裝)
                        + 寄庫單一累進量1(new MyClass { add = 1, stno = "stno", dtD = "instkd", dtBom = "InStkBOM", date = "indate" }, date) + " union all"
                        //寄庫+ (組合子件)
                        + 寄庫組合累進量1(new MyClass { add = 1, stno = "stno", dtD = "instkd", dtBom = "InStkBOM", date = "indate" }, date) + " union all"

                        //領庫- (單一組裝)
                        + 領庫單一累進量1(new MyClass { add = -1, stno = "stno", dtD = "oustkd", dtBom = "OuStkBOM", date = "oudate" }, date) + " union all"
                        //領庫- (組合子件)
                        + 領庫組合累進量1(new MyClass { add = -1, stno = "stno", dtD = "oustkd", dtBom = "OuStkBOM", date = "oudate" }, date)
                        //入庫- (組裝子件)
                        + "  union all select B.itno,(-1)*SUM(A.qty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from garnerd where len(stnoo)>0 AND ittrait = 2 And gadate like '" + date + "%' )A left join garnbom B on A.bomid=B.BomID where B.itno is not null group by B.itno " 
                    + " )C group by C.itno";
                    da.Fill(OtherQtyAll);

                    //
                    for (int j = 0; j < liICost.Count; j++)
                    {
                        for (int i = 0; i < liICost[j].Rows.Count; i++)
                        {
                            bqty = liICost[j].Rows[i]["begqty" + index].ToDecimal();
                            bcost = liICost[j].Rows[i]["begcost" + index].ToDecimal();
                            aqty = liICost[j].Rows[i]["addqty" + index].ToDecimal();
                            acost = liICost[j].Rows[i]["addcost" + index].ToDecimal();
                            if ((bqty + aqty) == 0) 
                            {
                                liICost[j].Rows[i]["avgcost" + index] = bcost.ToDecimal("f6");
                                liICost[j].Rows[i]["begcost" + nindex] = bcost.ToDecimal("f6");
                            }
                            else
                            {
                                liICost[j].Rows[i]["begcost" + nindex] = liICost[j].Rows[i]["avgcost" + index] = (((bqty * bcost) + acost) / (bqty + aqty)).ToDecimal("f6");
                            }

                            var itno = liICost[j].Rows[i]["itno"].ToString().Trim();
                            var row = OtherQtyAll.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno);
                            if (row == null)
                            {
                                liICost[j].Rows[i]["begqty" + nindex] = bqty + aqty;
                            }
                            else
                            {
                                liICost[j].Rows[i]["begqty" + nindex] = bqty + aqty + row["累進數量"].ToDecimal();
                            }
                        }
                    }
                    #endregion
                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Invoke(new mydelegate(buttonOK), new object[] { "    " });
            }

        }

        class itemFirst : AppData.DSCodingTableAdapters.成本計算首期TableAdapter
        {
            public string GetCommandText()
            {
                try
                {
                    return base.CommandCollection.FirstOrDefault().CommandText;
                }
                catch { return ""; }
            }
        }

        class stkFirst : AppData.DSCodingTableAdapters.分倉計算首期TableAdapter
        {
            public string GetCommandText()
            {
                try
                {
                    return base.CommandCollection.FirstOrDefault().CommandText;
                }
                catch { return ""; }
            }
        }

        class ic : AppData.DSCodingTableAdapters.ItemCostViewTableAdapter
        {
            public string GetCommandText()
            {
                try
                {
                    return base.CommandCollection.FirstOrDefault().CommandText;
                }
                catch { return ""; }
            }
        }

        class sc : AppData.DSCodingTableAdapters.StkCostViewTableAdapter
        {
            public string GetCommandText()
            {
                try
                {
                    return base.CommandCollection.FirstOrDefault().CommandText;
                }
                catch { return ""; }
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            try
            {
                liICost.ForEach(t => t.Dispose());
                liICost.Clear();
                liSCost.ForEach(t => t.Dispose());
                liSCost.Clear();

                using (S5.FrmItemCostbNew frm = new S5.FrmItemCostbNew())
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandText = new ic().GetCommandText();
                        frm.iCost.Clear();
                        da.Fill(frm.iCost);
                        cmd.CommandText = new sc().GetCommandText();

                        frm.sCost.Clear();
                        da.Fill(frm.sCost);
                    }
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }


    }
}
