using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using S_61.Basic;
using System.Linq;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace S_61.S0
{
    public partial class FrmInv401_Rpt : JE.MyControl.Formbase
    {
        JBS.JS.xEvents xe;

        public FrmInv401_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            Day.SetDateLength();
            Day1.SetDateLength();
            Day.Text = Date.GetDateTime(Common.User_DateTime);
            Day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmInv401_Rpt_Load(object sender, EventArgs e)
        {

        }

        private void Day_Validating(object sender, CancelEventArgs e)
        {
            xe.DateValidate(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void OutReport(RptMode mode)
        {
            if (machine.TrimTextLenth() == 0)
            {
                MessageBox.Show("請輸入機台號碼!!!");
                machine.Focus();
                return;
            }

            DataTable temp = new DataTable();
            using (var db = new JBS.xSQL())
            {
                var tsql = @"
Select 
 ZZ=SUBSTRING(k1.invno,1,2),XX=SUBSTRING(k1.invno,3,8)
,YYMM=SUBSTRING(k1.invdate,1,5),DD=SUBSTRING(k1.invdate,6,2)
,k1.invno,k1.invdate,k1.應稅,k2.免稅,X1='',X2='',作廢='',作廢筆數=0 from (
	Select p.invno,p.invdate,應稅=ISNULL(a.mny,0) from posinv p 
	left join (
	            Select sale.invno,mny=SUM(ROUND(qty*prs*price,0)) from saled left join sale on saled.sano = sale.sano 
                where sale.bracket=@前台 
                and (sale.die is null or len(sale.die)=0) 
                and saled.KiTax='1' 
                and sale.invdate >= @day and sale.invdate <= @day1
                and sale.seno = @machine 
                group by sale.invno
	)a on p.invno = a.invno
	where p.invdate >= @day and p.invdate <= @day1 and p.seno = @machine  
)k1
inner join (
	Select p.invno,p.invdate,免稅=ISNULL(b.mny,0) from posinv p 
	left join (
	            Select sale.invno,mny=SUM(ROUND(qty*prs*price,0)) from saled left join sale on saled.sano = sale.sano 
                where sale.bracket=@前台 
                and (sale.die is null or len(sale.die)=0) 
                and saled.KiTax='2' 
                and sale.invdate >= @day and sale.invdate <= @day1
                and sale.seno = @machine 
                group by sale.invno
	)b on p.invno = b.invno
	where p.invdate >= @day and p.invdate <= @day1 and p.seno = @machine
)k2 on k1.invno=k2.invno order by k1.invno ";

                db.Fill(tsql, spc =>
                {
                    spc.AddWithValue("day", Date.ToTWDate(Day.Text));
                    spc.AddWithValue("day1", Date.ToTWDate(Day1.Text));
                    spc.AddWithValue("前台", "前台");
                    spc.AddWithValue("machine", machine.Text.Trim());
                }, ref temp);
            }

            if (temp.Rows.Count == 0 && mode != RptMode.Design)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            var TOrder = temp.Clone();
            temp.AsEnumerable()
                .AsParallel()
                .GroupBy(r => new
                {
                    YYMM = r["YYMM"].ToString().Trim(),
                    ZZ = r["ZZ"].ToString().Trim()
                })
                .ForAll(g =>
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        ConcurrentQueue<string> cq = new ConcurrentQueue<string>();
                        var 應稅 = 0M;
                        var 免稅 = 0M;
                        var DD = i.ToString().PadLeft(2, '0');
                        var rows = g.Where(r => r["DD"].ToString().Trim() == DD).OrderBy(r => r["XX"].ToString().Trim());
                        for (int j = 0; j < rows.Count(); j++)
                        { 
                            if (j == 0)
                            {
                                cq.Enqueue(rows.ElementAt(j)["XX"].ToString());

                                應稅 = rows.ElementAt(j)["應稅"].ToDecimal();
                                免稅 = rows.ElementAt(j)["免稅"].ToDecimal();
                            }
                            else if (rows.ElementAt(j - 1)["XX"].ToDecimal() + 1 != rows.ElementAt(j)["XX"].ToDecimal())
                            {
                                cq.Enqueue(rows.ElementAt(j - 1)["XX"].ToString());

                                cq.Enqueue(應稅.ToString());
                                cq.Enqueue(免稅.ToString());
                                應稅 = 0M;
                                免稅 = 0M;
                                 
                                cq.Enqueue(rows.ElementAt(j)["XX"].ToString());

                                應稅 = rows.ElementAt(j)["應稅"].ToDecimal();
                                免稅 = rows.ElementAt(j)["免稅"].ToDecimal();
                            }
                            else
                            {
                                應稅 += rows.ElementAt(j)["應稅"].ToDecimal();
                                免稅 += rows.ElementAt(j)["免稅"].ToDecimal();
                            }
                             
                            if (j == rows.Count() - 1)
                            {
                                cq.Enqueue(rows.ElementAt(j)["XX"].ToString());
                                cq.Enqueue(應稅.ToString());
                                cq.Enqueue(免稅.ToString()); 
                            }
                        }

                        ConcurrentQueue<object> cobj = new ConcurrentQueue<object>();

                        for (int k = 0; k < cq.Count; k += 4)
                        {
                            if (cobj.Count > 0)
                            {
                                cobj = new ConcurrentQueue<object>();
                            }

                            cobj.Enqueue(g.Key.ZZ);
                            cobj.Enqueue(cq.ElementAt(k) + "~" + cq.ElementAt(k + 1));
                            cobj.Enqueue(g.Key.YYMM);
                            cobj.Enqueue(DD);
                            cobj.Enqueue("");
                            cobj.Enqueue(g.Key.YYMM + DD);
                            cobj.Enqueue(cq.ElementAt(k + 2));
                            cobj.Enqueue(cq.ElementAt(k + 3));
                            cobj.Enqueue(g.Key.ZZ + cq.ElementAt(k));
                            cobj.Enqueue(g.Key.ZZ + cq.ElementAt(k + 1));

                            lock (TOrder.Rows.SyncRoot)
                            {
                                TOrder.Rows.Add(cobj.ToArray());
                            }
                        }

                        if (cobj.Count == 0)
                        {
                            cobj.Enqueue(g.Key.ZZ);
                            cobj.Enqueue("");
                            cobj.Enqueue(g.Key.YYMM);
                            cobj.Enqueue(DD);
                            cobj.Enqueue("");
                            cobj.Enqueue(g.Key.YYMM + DD);
                            cobj.Enqueue(0);
                            cobj.Enqueue(0);
                            cobj.Enqueue("");
                            cobj.Enqueue("");

                            lock (TOrder.Rows.SyncRoot)
                            {
                                TOrder.Rows.Add(cobj.ToArray());
                            }
                        }
                    }
                });

            //撈作廢單號 
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("X1", "");
                cmd.Parameters.AddWithValue("X2", "");

                for (int i = 0; i < TOrder.Rows.Count; i++)
                {
                    var count = 0;
                    var ivno = "";
                    cmd.Parameters["X1"].Value = TOrder.Rows[i]["X1"].ToString().Trim();
                    cmd.Parameters["X2"].Value = TOrder.Rows[i]["X2"].ToString().Trim();

                    cmd.CommandText = "Select ivno=SUBSTRING(invno,7,4) from nullify where invno >= @X1 and invno <= @X2";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count++;

                            if (ivno.Length == 0)
                                ivno += reader["ivno"].ToString();
                            else
                                ivno += "," + reader["ivno"].ToString();
                        }
                    }

                    TOrder.Rows[i]["作廢"] = ivno;
                    TOrder.Rows[i]["作廢筆數"] = count;
                }
            }
            var groups = TOrder.AsEnumerable()
                .GroupBy(r => new
                {
                    YYMM = r["YYMM"].ToString().Trim(),
                    ZZ = r["ZZ"].ToString().Trim()
                });

            for (int i = 0; i < groups.Count(); i++)
            {
                var g = groups.ElementAt(i);
                var total = g.Sum(gw => gw["作廢筆數"].ToDecimal());

                for (int j = 0; j < g.Count(); j++)
                {
                    g.ElementAt(j)["作廢筆數"] = total;
                }
            }

            var TResult = TOrder.AsEnumerable().OrderBy(r => r["DD"].ToString()).CopyToDataTable();
            var path = Common.reportaddress + @"ReportF\二聯式收銀機發票明細表.frx";

            using (var fs = new JBS.FReport())
            {
                fs.dy.Add("機台", machine.Text.Trim());
                fs.dy.Add("統一編號", Common.Sys_Stctaxno);
                fs.dy.Add("營業人名稱", Common.Sys_Stcname);
                fs.dy.Add("稅籍編號", Common.Sys_Stctaxno1);

                fs.OutReport(mode, TResult, "Table1", path);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("是否要編輯報表?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    OutReport(RptMode.Design);
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }
    }
}
