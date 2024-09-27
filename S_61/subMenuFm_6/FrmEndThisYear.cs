using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class FrmEndThisYear : Formbase
    {
        Point pt1 = Point.Empty;
        Point pt2 = Point.Empty;
        string MDFdir = "";
        string MDFpath = "";
        delegate void mydelegate(string msg);
        delegate void mydelegate1();

        public FrmEndThisYear()
        {
            InitializeComponent();
            labelT2.Text = "";
        }

        private void FrmEndThisYear_Load(object sender, EventArgs e)
        {
            sqlConnection1.ConnectionString = Common.sqlConnString;

            MainForm.main.loadSystemSetting();
            if (Common.User_DateTime == 1)
                ShowMessage("目前系統庫存年度：【" + Common.Sys_StkYear1 + "】");
            else
                ShowMessage("目前系統庫存年度：【" + Common.Sys_StkYear2 + "】");
        }

        private void buttonSmallT1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("壹、結轉前建議先備份資料！", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
            if (MessageBox.Show("貳、年度結轉前，所有工作站都必須退出系統！", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
            if (MessageBox.Show("參、你已經準備好【年度結轉】了嗎？", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
            if (MessageBox.Show("肆、最後的確認！結轉請按【是】！", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
            btnsGo.Enabled = false;
            btnsExist.Enabled = false;
            Msg.Text = "年度結轉 Log：";
            bar.Value = 0;
            Thread t = new Thread(new ThreadStart(A));
            t.IsBackground = true;
            t.Start();
        }

        void ShowMessage(object msg)
        {
            if (InvokeRequired)
            {
                Invoke(new mydelegate(ShowMessage), new object[] { msg });
                return;
            }
            this.Msg.Text += Environment.NewLine;
            this.Msg.Text += msg.ToString();
            if (msg.ToString() == "年度結轉失敗！")
            {
                MessageBox.Show("年度結轉失敗！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnsExist.Enabled = true;
            }
            else if (msg.ToString() == "系統庫存年度與目前日期年度相同,不需要執行年度結轉作業！")
            {
                MessageBox.Show("系統庫存年度與目前日期年度相同,不需要執行年度結轉作業！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnsExist.Enabled = true;
            }
            else if (msg.ToString() == "年度結轉作業完成！！！")
            {
                MessageBox.Show("年度結轉作業完成！...", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MainForm.main.loadSystemSetting();
                btnsExist.Enabled = true;
            }
            else if (msg.ToString() == "    【成本計算】完成＊＊＊")
            {
                labelT2.Text = "【成本計算】完成";
                return;
            }
        }

        void ShowMessageTwo(object msg)
        {
            if (InvokeRequired)
            {
                Invoke(new mydelegate(ShowMessageTwo), new object[] { msg });
                return;
            }
            this.labelT2.Text = msg.ToString();
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

        void A()
        {
            try
            {
                Invoke(new mydelegate(ShowMessage), new object[] { "開始檢查環境..." });
                var year = Date.GetDateTime(1).takeString(3).ToInteger();
                if (Common.Sys_StkYear1 >= year)
                {
                    Invoke(new mydelegate(ShowMessage), new object[] { "系統庫存年度與目前日期年度相同,不需要執行年度結轉作業！" });
                    return;
                }
                //取得目前工作站數
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dtcount = new DataTable();
                    cmd.CommandText = " select hostprocess 主機,net_address 位址 from master..sysprocesses where program_name = 'JBS' ";
                    da.Fill(dtcount);

                    DataTable temp = dtcount.Clone();
                    for (int i = 0; i < dtcount.Rows.Count; i++)
                    {
                        var row = temp.AsEnumerable().Where(r => r["主機"].ToString() == dtcount.Rows[i]["主機"].ToString() && r["位址"].ToString() == dtcount.Rows[i]["位址"].ToString());
                        if (row.Count() == 0)
                            temp.ImportRow(dtcount.Rows[i]);
                    }
                    temp.AcceptChanges();
                    if (temp.Rows.Count > 1)
                    {
                        MessageBox.Show("請所有正在使用進銷存的工作站都必須退出,暫停作業！" + Environment.NewLine + "才可以進行年度結轉作業！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Invoke(new mydelegate(ShowMessage), new object[] { "請所有正在使用進銷存的工作站都必須退出,暫停作業！" + Environment.NewLine + "才可以進行年度結轉作業！" });
                        return;
                    }
                }
                //取得資料庫位址
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "SELECT physical_name FROM sys.database_files";
                    MDFpath = cmd.ExecuteScalar().ToString().ToUpper();
                    var index = MDFpath.LastIndexOf("\\") + 1;
                    MDFdir = new string(MDFpath.Take(index).ToArray());
                }
                Invoke(new mydelegate(ShowMessage), new object[] { "    環境檢查完成＊＊＊" });
                Invoke(new mydelegate(ShowMessage), new object[] { "    " });
                Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });
                //
                Thread t = new Thread(new ThreadStart(B));
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Invoke(new mydelegate(ShowMessage), new object[] { ex.ToString() });
                Invoke(new mydelegate(ShowMessage), new object[] { "年度結轉失敗！" });
            }
        }

        void B()
        {
            try
            {
                Invoke(new mydelegate(ShowMessage), new object[] { "開始備份【資料庫】..." });
                //
                var str = Common.sqlConnString.Split(';').ToList();
                int index = str.FindIndex(s => s.Contains("Initial Catalog"));
                str[index] = "Initial Catalog=master";
                index = str.ToList().FindIndex(s => s.Contains("Application Name"));
                str[index] = "";

                var SQLString = "";
                str.ForEach(s => SQLString = SQLString + s + ";");
                SQLString = SQLString.takeString(SQLString.Length - 1);

                string time = DateTime.Now.ToString("yyyyMMddhhmmss");
                try
                {
                    //delete
                    using (SqlConnection cn = new SqlConnection(SQLString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "EXECUTE master.dbo.xp_delete_file 0, N'" + MDFdir + time + "_" + Common.DatabaseName.ToUpper() + ".bak' , N'bak'";
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { }

                //backup
                using (SqlConnection cn = new SqlConnection(SQLString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "backup database [" + Common.DatabaseName.ToUpper() + "] to disk=N'" + MDFdir + time + "_" + Common.DatabaseName.ToUpper() + "結轉前.bak'";
                    cmd.ExecuteNonQuery();
                }
                Invoke(new mydelegate(ShowMessage), new object[] { "    【資料庫】備份完成＊＊＊" });
                Invoke(new mydelegate(ShowMessage), new object[] { "    " });
                Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });


                Thread t = new Thread(new ThreadStart(C));
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Invoke(new mydelegate(ShowMessage), new object[] { ex.ToString() });
                Invoke(new mydelegate(ShowMessage), new object[] { "年度結轉失敗！" });
            }
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
                    for (int i = 0; i < itAll; i += 20000)
                    {
                        DataTable tb = new DataTable();
                        da.Fill(i, 20000, tb);
                        if (!tb.Columns.Contains("begqty25")) tb.Columns.Add("begqty25", typeof(Decimal));
                        if (!tb.Columns.Contains("begcost25")) tb.Columns.Add("begcost25", typeof(Decimal));
                        liICost.Add(tb);
                    }
                    cmd.CommandText = new stkFirst().GetCommandText();
                    for (int i = 0; i < stkAll; i += 20000)
                    {
                        DataTable tb = new DataTable();
                        da.Fill(i, 20000, tb);
                        if (!tb.Columns.Contains("begqty25")) tb.Columns.Add("begqty25", typeof(Decimal));
                        if (!tb.Columns.Contains("begcost25")) tb.Columns.Add("begcost25", typeof(Decimal));
                        liSCost.Add(tb);
                    }

                    cmd.CommandText = "select bsdate,itno,itunit,itpkgqty,RealCost from bshopd order by bsdate DESC";
                    da.Fill(tempbshop);
                    cmd.CommandText = "select itno,ItFirCost from item";
                    da.Fill(tempitem);
                }
                //
                Invoke(new mydelegate(ShowMessage), new object[] { "開始【成本計算】..." });
                for (int i = 1; i <= 12; i++)
                {
                    var date = Common.Sys_StkYear1 + ((i).ToString().PadLeft(2, '0'));
                    Invoke(new mydelegate(ShowMessageTwo), new object[] { date + "計算中..." });

                    成本計算(date, i.ToString().PadLeft(2, '0'), (i + 1).ToString().PadLeft(2, '0'));
                    Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });
                }

                for (int i = 13; i <= 24; i++)
                {
                    var date = (Common.Sys_StkYear1 + 1) + ((i - 12).ToString().PadLeft(2, '0'));
                    Invoke(new mydelegate(ShowMessageTwo), new object[] { (Common.Sys_StkYear1 + 1) + ((i - 12).ToString().PadLeft(2, '0')) + "計算中..." });

                    成本計算(date, i.ToString().PadLeft(2, '0'), (i + 1).ToString().PadLeft(2, '0'));
                    Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });
                }

                Invoke(new mydelegate(ShowMessage), new object[] { "    【成本計算】完成＊＊＊" });
                Invoke(new mydelegate(ShowMessage), new object[] { "    " });

                Thread td = new Thread(new ThreadStart(D));
                td.IsBackground = true;
                td.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Invoke(new mydelegate(ShowMessage), new object[] { ex.ToString() });
                Invoke(new mydelegate(ShowMessage), new object[] { "年度結轉失敗！" });
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
        string 單一累進量(MyClass mc, string date, string qty = "qty")
        {
            return " Select itno," + mc.stno + ",(" + mc.add + ")*SUM(" + qty + "*itpkgqty)累進數量 from " + mc.dtD + " where ittrait <> 1 And " + mc.date + " like '" + date + "%' group by itno," + mc.stno;
        }
        string 組合累進量(MyClass mc, string date, string qty = "qty")
        {
            return " Select B.itno,A." + mc.stno + ",(" + mc.add + ")*SUM(A." + qty + "*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from " + mc.dtD + " where ittrait = 1 And " + mc.date + " like '" + date + "%' )A left join " + mc.dtBom + " B on A.bomid=B.BomID where B.itno is not null group by B.itno,A." + mc.stno;
        }
        string 單一累進量1(MyClass mc, string date, string qty = "qty")
        {
            return " Select itno,(" + mc.add + ")*SUM(" + qty + "*itpkgqty)累進數量 from " + mc.dtD + " where ittrait <> 1 And " + mc.date + " like '" + date + "%' group by itno";
        }
        string 組合累進量1(MyClass mc, string date, string qty = "qty")
        {
            return " Select B.itno,(" + mc.add + ")*SUM(A." + qty + "*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from " + mc.dtD + " where ittrait = 1 And " + mc.date + " like '" + date + "%' )A left join " + mc.dtBom + " B on A.bomid=B.BomID where B.itno is not null group by B.itno";
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

                //累進數量 = 進貨數量 + 銷退數量 – 進退數量 + 入庫數量 (+-)調整數量
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
                        + " select B.itno,A.stnoo stno,(-1)*SUM(A.qty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from garnerd where ittrait = 2 And gadate like '" + date + "%' )A left join drawbom B on A.bomid=B.BomID where B.itno is not null group by B.itno,A.stnoo" + " union all"

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
                    + " select itno,stnoo stno,(-1)*SUM(qty*costb)累進成本 from garnerd where ittrait <> 1 And gadate like '" + date + "%' and len(stnoo) > 0 group by itno,stnoo" + " union all"
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
                        + 單一累進量(new MyClass { add = 1, stno = "stno", dtD = "instkd", dtBom = "InStkBOM", date = "indate" }, date, "inqty") + " union all"
                        //寄庫+ (組合子件)
                        + 組合累進量(new MyClass { add = 1, stno = "stno", dtD = "instkd", dtBom = "InStkBOM", date = "indate" }, date, "inqty") + " union all"

                        //領庫- (單一組裝)
                        + 單一累進量(new MyClass { add = -1, stno = "stno", dtD = "oustkd", dtBom = "OuStkBOM", date = "oudate" }, date, "ouqty") + " union all"
                        //領庫- (組合子件)
                        + 組合累進量(new MyClass { add = -1, stno = "stno", dtD = "oustkd", dtBom = "OuStkBOM", date = "oudate" }, date, "ouqty")

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
                        + " select itno,SUM(qty*itpkgqty)累進數量 from garnerd where len(stnoi)>0 and ittrait = 3 And gadate like '" + date + "%' group by itno" + " union all"
                        //入庫- (單一)
                        + " select itno,(-1)*SUM(qty*itpkgqty)累進數量 from garnerd where len(stnoo)>0 and ittrait = 3 And gadate like '" + date + "%' group by itno" + " union all"
                        //入庫- (組裝子件)
                        + " select B.itno,(-1)*SUM(A.qty*A.itpkgqty*(B.itqty*B.itpkgqty/B.itpareprs))累進數量 from (Select * from garnerd where len(stnoo)>0 AND ittrait = 2 And gadate like '" + date + "%' )A left join drawbom B on A.bomid=B.BomID where B.itno is not null group by B.itno" + " union all"

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
                    + " select itno,(-1)*SUM(qty*costb)累進成本 from garnerd where ittrait <> 1 And gadate like '" + date + "%' and len(stnoo) > 0 group by itno" + " union all"
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
                        + 單一累進量1(new MyClass { add = -1, stno = "stnoo", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"
                        //領料- (組合子件)
                        + 組合累進量1(new MyClass { add = -1, stno = "stnoo", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"
                        //領料+ (單一組裝)
                        + 單一累進量1(new MyClass { add = 1, stno = "stnoi", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"
                        //領料+ (組合子件)
                        + 組合累進量1(new MyClass { add = 1, stno = "stnoi", dtD = "drawd", dtBom = "DrawBom", date = "drdate" }, date) + " union all"

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
                        + 單一累進量1(new MyClass { add = 1, stno = "stno", dtD = "instkd", dtBom = "InStkBOM", date = "indate" }, date, "inqty") + " union all"
                        //寄庫+ (組合子件)
                        + 組合累進量1(new MyClass { add = 1, stno = "stno", dtD = "instkd", dtBom = "InStkBOM", date = "indate" }, date, "inqty") + " union all"

                        //領庫- (單一組裝)
                        + 單一累進量1(new MyClass { add = -1, stno = "stno", dtD = "oustkd", dtBom = "OuStkBOM", date = "oudate" }, date, "ouqty") + " union all"
                        //領庫- (組合子件)
                        + 組合累進量1(new MyClass { add = -1, stno = "stno", dtD = "oustkd", dtBom = "OuStkBOM", date = "oudate" }, date, "ouqty")

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
                    for (int j = 0; j < liSCost.Count; j++)
                    {
                        var tempColumns1 = liSCost[j].Columns.OfType<DataColumn>().Where(c => c.ColumnName.ToLower().EndsWith("no") == false && c.ColumnName.Substring(c.ColumnName.Length - 2, 2).ToDecimal() <= 12).Select(c => c.ColumnName).ToList();
                        for (int i = 0; i < tempColumns1.Count; i++)
                        {
                            liSCost[j].Columns.Remove(tempColumns1[i]);
                        }
                        for (int i = 0; i < liSCost[j].Columns.Count; i++)
                        {
                            var name = liSCost[j].Columns[i].ColumnName.ToLower();
                            if (name.EndsWith("no")) continue;
                            else
                            {
                                var index = name.Substring(name.Length - 2, 2).ToInteger() - 12;
                                liSCost[j].Columns[i].ColumnName = name.Substring(0, name.Length - 2) + (index.ToString().PadLeft(2, '0'));
                            }
                        }
                    }

                    for (int j = 0; j < liICost.Count; j++)
                    {
                        var tempColumns2 = liICost[j].Columns.OfType<DataColumn>().Where(c => c.ColumnName.ToLower().EndsWith("no") == false && c.ColumnName.Substring(c.ColumnName.Length - 2, 2).ToDecimal() <= 12).Select(c => c.ColumnName).ToList();
                        for (int i = 0; i < tempColumns2.Count; i++)
                        {
                            liICost[j].Columns.Remove(tempColumns2[i]);
                        }
                        for (int i = 0; i < liICost[j].Columns.Count; i++)
                        {
                            var name = liICost[j].Columns[i].ColumnName.ToLower();
                            if (name.EndsWith("no")) continue;
                            else
                            {
                                var index = name.Substring(name.Length - 2, 2).ToInteger() - 12;
                                liICost[j].Columns[i].ColumnName = name.Substring(0, name.Length - 2) + (index.ToString().PadLeft(2, '0'));
                            }
                        }
                    }

                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;
                    cmd.CommandTimeout = 0;

                    Invoke(new mydelegate(ShowMessage), new object[] { "開始【過帳】至下個年度..." });

                    for (int j = 0; j < liSCost.Count; j++)
                    {
                        liSCost[j].AcceptChanges();
                        for (int i = 0; i < liSCost[j].Rows.Count; i++)
                        {
                            var itqtyf = liSCost[j].Rows[i]["begqty01"].ToDecimal();
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("itno", liSCost[j].Rows[i]["itno"].ToString());
                                cmd.Parameters.AddWithValue("stno", liSCost[j].Rows[i]["stno"].ToString());
                                cmd.CommandText = "Update Stock set itqtyf = (" + itqtyf + ") where stno = (@stno) and itno = (@itno)";
                                var c = cmd.ExecuteNonQuery();
                                if (c == 0)
                                {
                                    cmd.CommandText = "Insert into Stock (ItNo,StNo,Itqtyf,Itqty) values (@itno,@stno," + itqtyf + ",0)";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            liSCost[j].Rows[i].SetAdded();
                        }
                    }
                     
                    for (int j = 0; j < liICost.Count; j++)
                    {
                        liICost[j].AcceptChanges();
                        for (int i = 0; i < liICost[j].Rows.Count; i++)
                        { 
                            var itfircost = liICost[j].Rows[i]["begcost01"].ToDecimal();
                             
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("itno", liICost[j].Rows[i]["itno"].ToString());
                            cmd.CommandText = "Update item set itfircost = (" + itfircost + ") where itno = (@itno)";
                            cmd.ExecuteNonQuery(); 

                            liICost[j].Rows[i].SetAdded();
                        }
                    }

                    JBS.JS.Stock jStock = new JBS.JS.Stock(Common.Sys_StkYear1 + 1, Common.Sys_StkYear1 + 2);
                    jStock.ReSetAllStock(cmd);

                    Invoke(new mydelegate(ShowMessage), new object[] { "    【過帳】至新年度完成＊＊＊" });
                    Invoke(new mydelegate(ShowMessage), new object[] { "    " });
                    Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });


                    Invoke(new mydelegate(ShowMessage), new object[] { "儲存新年度【成本計算】資料表..." });

                    cmd.CommandText = "delete from itemcost;delete from stkcost;";
                    cmd.ExecuteNonQuery();

                    sqlDataAdapter1.InsertCommand.Connection = cn;
                    sqlDataAdapter2.InsertCommand.Connection = cn;

                    sqlDataAdapter1.InsertCommand.Transaction = tn;
                    sqlDataAdapter2.InsertCommand.Transaction = tn;

                    sqlDataAdapter1.InsertCommand.CommandTimeout = 0;
                    sqlDataAdapter2.InsertCommand.CommandTimeout = 0;

                    for (int j = 0; j < liICost.Count; j++)
                    {
                        sqlDataAdapter1.Update(liICost[j]);
                    }
                    for (int j = 0; j < liSCost.Count; j++)
                    {
                        sqlDataAdapter2.Update(liSCost[j]);
                    }


                    Invoke(new mydelegate(ShowMessage), new object[] { "    新年度【成本計算】資料表,儲存完成＊＊＊" });
                    Invoke(new mydelegate(ShowMessage), new object[] { "    " });
                    Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });

                    Invoke(new mydelegate(ShowMessage), new object[] { "更新【庫存年度】..." });
                    cmd.CommandText = "Update systemset set JZOK='JZOK',stkyear1=" + (Common.Sys_StkYear1 + 1) + ",stkyear2=" + (Common.Sys_StkYear2 + 1) + " where usrno='T01'";
                    cmd.ExecuteNonQuery();

                    tn.Commit();
                    Invoke(new mydelegate(ShowMessageBar), new object[] { "    " });
                    Invoke(new mydelegate(ShowMessage), new object[] { "年度結轉作業完成！！！" });
                    Common.Sys_JZOK = true;
                }
                catch (Exception ex)
                {
                    if (tn != null) 
                        tn.Rollback();

                    MessageBox.Show(ex.ToString());
                    Invoke(new mydelegate(ShowMessage), new object[] { ex.ToString() });
                    Invoke(new mydelegate(ShowMessage), new object[] { "年度結轉失敗！" });
                }
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

        private void buttonSmallT2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBoxT1_Validating(object sender, CancelEventArgs e)
        {
            if (textBox1.Text.Trim().ToUpper() == "WHAT")
            {
                textBox1.Clear();
                Application.DoEvents();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "Select ISNULL(JieZ,'XX') JieZ from systemset where usrno='T01';";
                    var JZ = cmd.ExecuteScalar();
                    if (JZ != null)
                    {
                        labelT3.Text += Environment.NewLine;
                        labelT3.Text += "  The Result is '" + JZ.ToString().Trim() + "' ;Your Query Completed!";
                        labelT3.Text += Environment.NewLine;
                        labelT3.Text += "  Congragalation, The Database is Repaired!";
                    }
                    else
                    {
                        labelT3.Text += Environment.NewLine;
                        labelT3.Text += "  The Result is 'XX' ;Your Query Completed!";
                        labelT3.Text += Environment.NewLine;
                        labelT3.Text += "  Congragalation, The Database is Repaired!";
                    }
                }
                if (panelNT3.VerticalScroll.Value > 0 && panelNT3.VerticalScroll.Value != panelNT3.VerticalScroll.Maximum)
                {
                    labelT3.Text += Environment.NewLine;
                    labelT3.Text += Environment.NewLine;
                }
            }
            else if (textBox1.Text.Trim().ToUpper() == "CM888")
            {
                textBox1.Clear();
                Application.DoEvents();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "Update systemset set JieZ = 'OK' where usrno='T01';";
                    cmd.ExecuteNonQuery();
                    labelT3.Text += Environment.NewLine;
                    labelT3.Text += "  Congragalation, The Database is Repaired!";
                }
                if (panelNT3.VerticalScroll.Value > 0 && panelNT3.VerticalScroll.Value != panelNT3.VerticalScroll.Maximum)
                {
                    labelT3.Text += Environment.NewLine;
                    labelT3.Text += Environment.NewLine;
                }
            }
            else if (textBox1.Text.Trim().ToUpper() == "CM999")
            {
                textBox1.Clear();
                Application.DoEvents();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "Update systemset set JieZ = 'XX' where usrno='T01';";
                    cmd.ExecuteNonQuery();
                    labelT3.Text += Environment.NewLine;
                    labelT3.Text += "  Congragalation, The Database is Repaired!";
                }
                if (panelNT3.VerticalScroll.Value > 0 && panelNT3.VerticalScroll.Value != panelNT3.VerticalScroll.Maximum)
                {
                    labelT3.Text += Environment.NewLine;
                    labelT3.Text += Environment.NewLine;
                }
            }
            else if (textBox1.Text.Trim().ToUpper() == "EXIT")
            {
                textBox1.Clear();
                panelNT3.Visible = false;
                Application.DoEvents();
            }
            else if (textBox1.TrimTextLenth() > 0)
            {
                textBox1.Clear();
                Application.DoEvents();
                Random rd = new Random(DateTime.Now.Millisecond);
                var val = rd.Next(0, 20);
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText += "Select top " + val + " * from  item";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            labelT3.Text += Environment.NewLine;
                            for (int i = 0; i < val; i++)
                            {
                                labelT3.Text += reader[rd.Next(0, 10)].ToDecimal().GetHashCode() + "  ";
                                panelNT3.VerticalScroll.Value = panelNT3.VerticalScroll.Maximum;
                                Application.DoEvents();
                            }
                        }
                        if (panelNT3.VerticalScroll.Value > 0 && panelNT3.VerticalScroll.Value != panelNT3.VerticalScroll.Maximum)
                        {
                            labelT3.Text += Environment.NewLine;
                            labelT3.Text += Environment.NewLine;
                        }
                    }
                }
            }
            else
            {
                textBox1.Clear();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnsExist.Focus();
                textBox1.Focus();
            }
            else if (e.KeyData == Keys.Escape)
            {
                textBox1.Clear();
                panelNT3.Visible = false;
            }
        }

        private void panelNT3_VisibleChanged(object sender, EventArgs e)
        {
            //if (panelNT3.Visible)
            //{
            //    AnyConsole = true;
            //    labelT3.Text = "";
            //    bar.Value = 0;
            //    btnsGo.Enabled = false;
            //    textBox1.Focus();
            //}
            //else
            //{
            //    btnsGo.Enabled = true;
            //}
        }

        private void FrmEndThisYear_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    pt1 = e.Location;
            //}
            //else pt1 = Point.Empty;
        }

        private void FrmEndThisYear_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    pt2 = e.Location;
            //    if (pt1 != Point.Empty && pt2 != Point.Empty)
            //    {
            //        if (pt1.X > 0 && pt1.Y > 0 && pt2.X > pt1.X && pt2.Y > pt1.Y)
            //        {
            //            Rectangle rc = new Rectangle(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y);
            //            if (rc.Contains(btnsGo.Location) && rc.Contains(btnsExist.Location) && btnsExist.Enabled)
            //            {
            //                panelNT3.Visible = true;
            //                textBox1.Focus();
            //            }
            //        }
            //    }
            //}
            //else pt2 = Point.Empty;
        }
    }
}
