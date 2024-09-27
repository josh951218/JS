using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S0
{
    public partial class FrmNSale_Rpt : Formbase
    {
        class 成本計算
        {
            bool sale = false;
            bool rsale = false;
            string ymonth = "";
            string ymonth1 = "";
            string cuno = "";
            string cuno1 = "";

            public 成本計算(bool s, bool r, string y, string y1, string c, string c1)
            {
                this.sale = s;
                this.rsale = r;
                this.ymonth = y;
                this.ymonth1 = y1;
                this.cuno = c;
                this.cuno1 = c1;
            }

            /// <summary>
            /// 組合品取子件, 計算成本
            /// </summary>
            public void 月平均成本(out DataTable dtResult)
            {
                DataTable dtSource = new DataTable();
                DataTable dtCust = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合

                itemcost.Columns.Add("itno", typeof(String));
                itemcost.Columns.Add("月份", typeof(String));
                itemcost.Columns.Add("itcost", typeof(String));

                var ym = Common.Sys_StkYear1;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1));
                    cmd.Parameters.AddWithValue("cuno", this.cuno);
                    cmd.Parameters.AddWithValue("cuno1", this.cuno1);
                    var sQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.cuno.Trim().Length > 0) sQuery += "  And saled.cuno>=@cuno";
                    if (this.cuno1.Trim().Length > 0) sQuery += " And saled.cuno<=@cuno1";
                    var rQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.cuno.Trim().Length > 0) rQuery += "  And rsaled.cuno>=@cuno";
                    if (this.cuno1.Trim().Length > 0) rQuery += " And rsaled.cuno<=@cuno1";

                    cmd.CommandText = "Select cuno='',cuname1='',itno='',itname='',銷退數量=0.0,銷退金額=0.0,銷貨數量=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = "Select cuno,cuname1 from cust";
                    da.Fill(dtCust);

                    cmd.CommandText = @"Select itno,
                    avgcost01,avgcost02,avgcost03,avgcost04,avgcost05,avgcost06,avgcost07,avgcost08,avgcost09,avgcost10,avgcost11,avgcost12,
                    avgcost13,avgcost14,avgcost15,avgcost16,avgcost17,avgcost18,avgcost19,avgcost20,avgcost21,avgcost22,avgcost23,avgcost24 
                    from itemcost";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int j = 1; j < 13; j++)
                            {
                                var 月份 = ym + j.ToString().PadLeft(2, '0');
                                itemcost.Rows.Add(new object[] { reader["itno"], 月份, reader[j] });
                            }

                            for (int j = 13; j < 25; j++)
                            {
                                var 月份 = (ym + 1) + (j - 12).ToString().PadLeft(2, '0');
                                itemcost.Rows.Add(new object[] { reader["itno"], 月份, reader[j] });
                            }
                        }
                    }

                    var mny = "(saled.qty)*(saled.itpkgqty)*(saled.prs)*(saled.price)";
                    var rmny = "(rsaled.qty)*(rsaled.itpkgqty)*(rsaled.prs)*(rsaled.price)";
                    var back = FrmNSale_Rpt.BackEnd ? "" : " And saled.bracket='前台' ";
                    var rback = FrmNSale_Rpt.BackEnd ? "" : " And rsaled.bracket='前台' ";

                    if (sale)
                    {
                        cmd.CommandText = "Select substring(sadate,1,5)月份,cuno,itno,itname,銷退金額=0.0,銷貨金額=(" + mny + "),銷退數量=0.0,銷貨數量=qty*itpkgqty,母數量=qty*itpkgqty from saled where ittrait <> 1 " + back + sQuery;
                        da.Fill(saled23);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = "Select substring(sadate,1,5)月份,cuno,itno,itname,銷退金額=(-1)*(" + rmny + "),銷貨金額=0.0,銷貨數量=0.0,銷退數量=(-1)*qty*itpkgqty,母數量=(-1)*qty*itpkgqty from rsaled where ittrait <> 1 " + rback + rQuery;
                        da.Fill(rsaled23);
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,saled.bomid,substring(sadate,1,5)月份,saled.cuno,saled.itno sitno,saled.itname,bom.itno,銷退金額=0.0,銷貨金額=(" + mny + @"),銷退數量=0.0,銷貨數量=saled.qty*saled.itpkgqty,母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from salebom bom
                        right join saled on bom.bomid = saled.bomid 
                        where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + back + sQuery;
                        da.Fill(saled1);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,rsaled.bomid,substring(sadate,1,5)月份,rsaled.cuno,rsaled.itno sitno,rsaled.itname,bom.itno,銷退金額=(-1)*(" + rmny + @"),銷貨金額=0.0,銷貨數量=0.0,銷退數量=(-1)*rsaled.qty*rsaled.itpkgqty,母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from rsalebom bom
                        right join rsaled on bom.bomid = rsaled.bomid 
                        where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + rback + rQuery;
                        da.Fill(rsaled1);
                    }
                }
                //單一組裝成本
                if (sale)
                    AvgCost(saled23, itemcost, ref dtSource);

                if (rsale)
                    AvgCost(rsaled23, itemcost, ref dtSource);

                //組合品成本
                if (sale)
                    BomAvgCost(saled1, itemcost, ref dtSource);
                if (rsale)
                    BomAvgCost(rsaled1, itemcost, ref dtSource);

                //結果: 計算毛利
                dtResult = dtSource.Clone();
                if (dtSource.Rows.Count == 0)
                    return;

                var tTemp = dtSource.Clone();
                dtSource.AsEnumerable()
                    .GroupJoin(
                        dtCust.AsEnumerable(),
                        s => s["cuno"].ToString().Trim(),
                        c => c["cuno"].ToString().Trim(),
                        (s, c) => new
                        {
                            cuno = s["cuno"],
                            cuname1 = c.FirstOrDefault() == null ? "" : c.First()["cuname1"],
                            itno = s["itno"],
                            itname = s["itname"],
                            銷退數量 = s["銷退數量"],
                            銷退金額 = s["銷退金額"],
                            銷貨數量 = s["銷貨數量"],
                            銷貨金額 = s["銷貨金額"],
                            銷貨淨額 = s["銷貨淨額"],
                            銷貨成本 = s["銷貨成本"],
                        })
                    .AsParallel()
                    .GroupBy(gj => new
                    {
                        cuno = gj.cuno,
                        itno = gj.itno
                    })
                    .ForAll(g =>
                    {
                        var 銷退數量 = 0M;
                        var 銷退金額 = 0M;
                        var 銷貨數量 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退數量 += gw.銷退數量.ToDecimal();
                            銷退金額 += gw.銷退金額.ToDecimal();

                            銷貨數量 += gw.銷貨數量.ToDecimal();
                            銷貨金額 += gw.銷貨金額.ToDecimal();

                            銷貨成本 += gw.銷貨成本.ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key.cuno);
                        cq.Enqueue(g.First().cuname1);
                        cq.Enqueue(g.Key.itno);
                        cq.Enqueue(g.First().itname);
                        cq.Enqueue(銷退數量);
                        cq.Enqueue(銷退金額);

                        cq.Enqueue(銷貨數量);
                        cq.Enqueue(銷貨金額);

                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                if (tTemp.Rows.Count > 0)
                    dtResult = tTemp.AsEnumerable().OrderBy(r => r["cuno"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();

                dtSource.Clear();
                dtCust.Clone();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();
                tTemp.Clear();
            }

            void AvgCost(DataTable tSale, DataTable itemCost, ref DataTable dtSource)
            {
                if (tSale.Rows.Count == 0)
                    return;

                var temp = tSale.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        s => new { ym = s["月份"].ToString(), itno = s["itno"].ToString() },
                        c => new { ym = c["月份"].ToString(), itno = c["itno"].ToString() },
                        (s, c) => new { sale = s, cost = c.DefaultIfEmpty() })
                    .Select(sc => new object[]
                        { 
                            sc.sale["cuno"],
                            "",
                            sc.sale["itno"], 
                            sc.sale["itname"], 
                            sc.sale["銷退數量"], 
                            sc.sale["銷退金額"], 
                            sc.sale["銷貨數量"], 
                            sc.sale["銷貨金額"], 
                            0, 
                            JoinAvg(sc.sale, sc.cost)
                        });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal JoinAvg(DataRow sale, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                return sale["母數量"].ToDecimal() * row["itcost"].ToDecimal();
            }

            void BomAvgCost(DataTable tBom, DataTable itemCost, ref DataTable dtSource)
            {
                if (tBom.Rows.Count == 0)
                    return;

                var temp = tBom.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        b => new { ym = b["月份"].ToString(), itno = b["itno"].ToString() },
                        c => new { ym = c["月份"].ToString(), itno = c["itno"].ToString() },
                        (b, c) => new { bom = b, cost = c.DefaultIfEmpty() })
                    .Select(bc => new
                        {
                            bomid = bc.bom["bomid"],
                            母數量 = bc.bom["母數量"],
                            cuno = bc.bom["cuno"],
                            sitno = bc.bom["sitno"],
                            itname = bc.bom["itname"],
                            銷退數量 = bc.bom["銷退數量"],
                            銷退金額 = bc.bom["銷退金額"],
                            銷貨數量 = bc.bom["銷貨數量"],
                            銷貨金額 = bc.bom["銷貨金額"],
                            銷貨成本 = BomJoinAvg(bc.bom, bc.cost)
                        })
                    .GroupBy(o => o.bomid)
                    .Select(g => new object[]     
                        { 
                            g.First().cuno, 
                            "",
                            g.First().sitno,
                            g.First().itname,
                            g.First().銷退數量,
                            g.First().銷退金額, 
                            g.First().銷貨數量,
                            g.First().銷貨金額, 
                            0, 
                            g.First().母數量.ToDecimal() * g.Sum(gw => gw.銷貨成本) 
                        });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal BomJoinAvg(DataRow bom, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                return bom["子數量"].ToDecimal() * row["itcost"].ToDecimal();
            }

            /// <summary>
            /// 1 沒進貨紀錄, 則取產品進價 (itbuypri)
            /// 2 有進貨紀錄
            ///     包裝數量相同, 取實際成本(realcost)
            ///     包裝數量不同, 取平均成本(realcost/itpkgqty)
            /// </summary>
            public void 最後一次進貨成本(out DataTable dtResult)
            {
                DataTable dtSource = new DataTable();
                DataTable dtCust = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day0", Date.ToTWDate(Common.Sys_StkYear1 - 1 + "0101"));
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1));
                    cmd.Parameters.AddWithValue("cuno", this.cuno);
                    cmd.Parameters.AddWithValue("cuno1", this.cuno1);
                    var sQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.cuno.Trim().Length > 0) sQuery += "  And saled.cuno>=@cuno";
                    if (this.cuno1.Trim().Length > 0) sQuery += " And saled.cuno<=@cuno1";
                    var rQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.cuno.Trim().Length > 0) rQuery += "  And rsaled.cuno>=@cuno";
                    if (this.cuno1.Trim().Length > 0) rQuery += " And rsaled.cuno<=@cuno1";

                    cmd.CommandText = "Select cuno='',cuname1='',itno='',itname='',銷退數量=0.0,銷退金額=0.0,銷貨數量=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = "Select cuno,cuname1 from cust";
                    da.Fill(dtCust);

                    cmd.CommandText = @"Select itno,bsdate,itunit,itpkgqty,realcost from bshopd Where bsdate >=@day0 and bsdate<=@day1 order by itno asc,bsdate desc,bsno desc";
                    da.Fill(itemcost);

                    var mny = "(saled.qty)*(saled.itpkgqty)*(saled.prs)*(saled.price)";
                    var rmny = "(rsaled.qty)*(rsaled.itpkgqty)*(rsaled.prs)*(rsaled.price)";
                    var back = FrmNSale_Rpt.BackEnd ? "" : " And saled.bracket='前台' ";
                    var rback = FrmNSale_Rpt.BackEnd ? "" : " And rsaled.bracket='前台' ";

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select saled.sadate,saled.cuno,saled.itno,saled.itname,銷退金額=0.0,銷貨金額=(" + mny + @"),銷退數量=0.0,銷貨數量=saled.qty*saled.itpkgqty,母數量=saled.qty,saled.itunit,saled.itpkgqty,item.itbuypri
                        From saled 
                        Left join item on saled.itno = item.itno
                        where saled.ittrait <> 1 " + back + sQuery;
                        da.Fill(saled23);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select rsaled.sadate,rsaled.cuno,rsaled.itno,rsaled.itname,銷退金額=(-1)*(" + rmny + @"),銷貨金額=0.0,銷貨數量=0.0,銷退數量=(-1)*rsaled.qty*rsaled.itpkgqty,母數量=(-1)*rsaled.qty,rsaled.itunit,rsaled.itpkgqty,item.itbuypri
                        From rsaled 
                        Left join item on rsaled.itno = item.itno
                        where rsaled.ittrait <> 1 " + rback + rQuery;
                        da.Fill(rsaled23);
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,saled.bomid,saled.sadate,saled.cuno,saled.itno sitno,saled.itname,bom.itno,銷退金額=0.0,銷貨金額=(" + mny + @"),銷退數量=0.0,銷貨數量=saled.qty*saled.itpkgqty,母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
                        From saled 
                        Left join item on saled.itno = item.itno
                        Left join salebom bom on bom.bomid = saled.bomid 
                        Where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + back + sQuery;
                        da.Fill(saled1);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,rsaled.bomid,rsaled.sadate,rsaled.cuno,rsaled.itno sitno,rsaled.itname,bom.itno,銷退金額=(-1)*(" + rmny + @"),銷貨金額=0.0,銷貨數量=0.0,銷退數量=(-1)*rsaled.qty*rsaled.itpkgqty,母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
                        From rsaled 
                        Left join item on rsaled.itno = item.itno
                        Left join rsalebom bom on bom.bomid = rsaled.bomid 
                        Where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + rback + rQuery;
                        da.Fill(rsaled1);
                    }
                }
                //單一組裝成本
                if (sale)
                    LastCost(saled23, itemcost, dtSource);
                if (rsale)
                    LastCost(rsaled23, itemcost, dtSource);

                //組合品成本
                if (sale)
                    BomJoinLast(saled1, itemcost);
                if (rsale)
                    BomJoinLast(rsaled1, itemcost);

                if (sale)
                    BomLastCost(saled1, ref dtSource);
                if (rsale)
                    BomLastCost(rsaled1, ref dtSource);

                //結果: 計算毛利
                dtResult = dtSource.Clone();
                if (dtSource.Rows.Count == 0)
                    return;

                var tTemp = dtSource.Clone();
                dtSource.AsEnumerable()
                    .GroupJoin(
                        dtCust.AsEnumerable(),
                        s => s["cuno"].ToString(),
                        c => c["cuno"].ToString(),
                        (s, c) => new
                        {
                            cuno = s["cuno"],
                            cuname1 = c.FirstOrDefault() == null ? "" : c.First()["cuname1"],
                            itno = s["itno"],
                            itname = s["itname"],
                            銷退數量 = s["銷退數量"],
                            銷退金額 = s["銷退金額"],
                            銷貨數量 = s["銷貨數量"],
                            銷貨金額 = s["銷貨金額"],
                            銷貨淨額 = s["銷貨淨額"],
                            銷貨成本 = s["銷貨成本"],
                        })
                    .AsParallel()
                    .GroupBy(gj => new
                    {
                        cuno = gj.cuno,
                        itno = gj.itno
                    })
                    .ForAll(g =>
                    {
                        var 銷退數量 = 0M;
                        var 銷退金額 = 0M;
                        var 銷貨數量 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退數量 += gw.銷退數量.ToDecimal();
                            銷退金額 += gw.銷退金額.ToDecimal();

                            銷貨數量 += gw.銷貨數量.ToDecimal();
                            銷貨金額 += gw.銷貨金額.ToDecimal();

                            銷貨成本 += gw.銷貨成本.ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key.cuno);
                        cq.Enqueue(g.First().cuname1);
                        cq.Enqueue(g.Key.itno);
                        cq.Enqueue(g.First().itname);
                        cq.Enqueue(銷退數量);
                        cq.Enqueue(銷退金額);

                        cq.Enqueue(銷貨數量);
                        cq.Enqueue(銷貨金額);

                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                if (tTemp.Rows.Count > 0)
                    dtResult = tTemp.AsEnumerable().OrderBy(r => r["cuno"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();

                dtSource.Clear();
                dtCust.Clone();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();
                tTemp.Clear();
            }

            void LastCost(DataTable tSale, DataTable itemcost, DataTable dtSource)
            {
                tSale.AsEnumerable()
                    .AsParallel()
                    .ForAll(r =>
                    {
                        var itno = r["itno"].ToString().Trim();
                        var day = r["sadate"].ToString();
                        var row = itemcost.AsEnumerable().Where(c => c["itno"].ToString().Trim() == itno && string.CompareOrdinal(day, c["bsdate"].ToString()) >= 0)
                            .FirstOrDefault();

                        if (row == null)
                        {
                            ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                            cq.Enqueue(r["cuno"]);
                            cq.Enqueue("");
                            cq.Enqueue(r["itno"]);
                            cq.Enqueue(r["itname"]);
                            cq.Enqueue(r["銷退數量"]);
                            cq.Enqueue(r["銷退金額"]);
                            cq.Enqueue(r["銷貨數量"]);
                            cq.Enqueue(r["銷貨金額"]);
                            cq.Enqueue(0);
                            cq.Enqueue(r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * r["itbuypri"].ToDecimal());

                            lock (dtSource.Rows.SyncRoot)
                            {
                                dtSource.Rows.Add(cq.ToArray());
                            }
                        }
                        else
                        {
                            var saleunit = r["itunit"].ToString();
                            var salepkgqty = r["itpkgqty"].ToDecimal();

                            var bshopunit = row["itunit"].ToString();
                            var bshoppkgqty = row["itpkgqty"].ToDecimal();

                            if (saleunit == bshopunit && salepkgqty == bshoppkgqty)
                            {
                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["cuno"]);
                                cq.Enqueue("");
                                cq.Enqueue(r["itno"]);
                                cq.Enqueue(r["itname"]);
                                cq.Enqueue(r["銷退數量"]);
                                cq.Enqueue(r["銷退金額"]);
                                cq.Enqueue(r["銷貨數量"]);
                                cq.Enqueue(r["銷貨金額"]);
                                cq.Enqueue(0);
                                cq.Enqueue(r["母數量"].ToDecimal() * row["realcost"].ToDecimal());

                                lock (dtSource.Rows.SyncRoot)
                                {
                                    dtSource.Rows.Add(cq.ToArray());
                                }
                            }
                            else
                            {
                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["cuno"]);
                                cq.Enqueue("");
                                cq.Enqueue(r["itno"]);
                                cq.Enqueue(r["itname"]);
                                cq.Enqueue(r["銷退數量"]);
                                cq.Enqueue(r["銷退金額"]);
                                cq.Enqueue(r["銷貨數量"]);
                                cq.Enqueue(r["銷貨金額"]);
                                cq.Enqueue(0);

                                var cost = 0M;
                                if (bshoppkgqty != 0)
                                    cost = r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * (row["realcost"].ToDecimal() / bshoppkgqty);

                                cq.Enqueue(cost);

                                lock (dtSource.Rows.SyncRoot)
                                {
                                    dtSource.Rows.Add(cq.ToArray());
                                }
                            }
                        }
                    });
            }

            void BomJoinLast(DataTable tBom, DataTable itemcost)
            {
                foreach (DataRow r in tBom.AsEnumerable())
                {
                    var itno = r["itno"].ToString().Trim();
                    var day = r["sadate"].ToString();
                    var row = itemcost.AsEnumerable().Where(c => c["itno"].ToString().Trim() == itno && string.CompareOrdinal(day, c["bsdate"].ToString()) >= 0)
                        .FirstOrDefault();

                    if (row == null)
                    {
                        r["銷貨成本"] = r["子數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * r["itbuypri"].ToDecimal();
                    }
                    else
                    {
                        var saleunit = r["itunit"].ToString();
                        var salepkgqty = r["itpkgqty"].ToDecimal();

                        var bshopunit = row["itunit"].ToString();
                        var bshoppkgqty = row["itpkgqty"].ToDecimal();

                        if (saleunit == bshopunit && salepkgqty == bshoppkgqty)
                        {
                            r["銷貨成本"] = r["子數量"].ToDecimal() * row["realcost"].ToDecimal();
                        }
                        else
                        {
                            var cost = 0M;
                            if (bshoppkgqty != 0)
                                cost = r["子數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * (row["realcost"].ToDecimal() / bshoppkgqty);

                            r["銷貨成本"] = cost;
                        }
                    }
                }
            }

            void BomLastCost(DataTable tBom, ref DataTable dtSource)
            {
                var temp = tBom.AsEnumerable()
                    .GroupBy(r => r["bomid"].ToString())
                    .Select(g => new object[] 
                    {
                        g.First()["cuno"], 
                        "", 
                        g.First()["sitno"], 
                        g.First()["itname"],
                        g.First()["銷退數量"],
                        g.First()["銷退金額"],
                        g.First()["銷貨數量"],
                        g.First()["銷貨金額"],
                        0,
                        g.First()["母數量"].ToDecimal() * g.Sum(gw => gw["銷貨成本"].ToDecimal())
                    });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            /// <summary>
            /// 組合品取子件, 計算成本
            /// </summary> 
            public void 標準成本(out DataTable dtResult)
            {
                DataTable dtSource = new DataTable();
                DataTable dtCust = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1));
                    cmd.Parameters.AddWithValue("cuno", this.cuno);
                    cmd.Parameters.AddWithValue("cuno1", this.cuno1);
                    var sQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.cuno.Trim().Length > 0) sQuery += "  And saled.cuno>=@cuno";
                    if (this.cuno1.Trim().Length > 0) sQuery += " And saled.cuno<=@cuno1";
                    var rQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.cuno.Trim().Length > 0) rQuery += "  And rsaled.cuno>=@cuno";
                    if (this.cuno1.Trim().Length > 0) rQuery += " And rsaled.cuno<=@cuno1";

                    cmd.CommandText = "Select cuno='',cuname1='',itno='',itname='',銷退數量=0.0,銷退金額=0.0,銷貨數量=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = "Select cuno,cuname1 from cust";
                    da.Fill(dtCust);

                    cmd.CommandText = @"Select itno,itpkgqty,itcost,itcostp from item";
                    da.Fill(itemcost);

                    var mny = "(saled.qty)*(saled.itpkgqty)*(saled.prs)*(saled.price)";
                    var rmny = "(rsaled.qty)*(rsaled.itpkgqty)*(rsaled.prs)*(rsaled.price)";
                    var back = FrmNSale_Rpt.BackEnd ? "" : " And saled.bracket='前台' ";
                    var rback = FrmNSale_Rpt.BackEnd ? "" : " And rsaled.bracket='前台' ";

                    if (sale)
                    {
                        cmd.CommandText = "Select cuno,itno,itname,銷退金額=0.0,銷貨金額=(" + mny + "),銷退數量=0.0,銷貨數量=qty*itpkgqty,母數量=qty,itpkgqty from saled where ittrait <> 1 " + back + sQuery;
                        da.Fill(saled23);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = "Select cuno,itno,itname,銷退金額=(-1)*(" + rmny + "),銷貨金額=0.0,銷貨數量=0.0,銷退數量=(-1)*qty*itpkgqty,母數量=(-1)*qty,itpkgqty from rsaled where ittrait <> 1 " + rback + rQuery;
                        da.Fill(rsaled23);
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,saled.bomid,saled.cuno,saled.itno sitno,saled.itname,bom.itno,銷退金額=0.0,銷貨金額=(" + mny + @"),銷退數量=0.0,銷貨數量=saled.qty*saled.itpkgqty,母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from salebom bom
                        right join saled on bom.bomid = saled.bomid 
                        where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + back + sQuery;
                        da.Fill(saled1);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,rsaled.bomid,rsaled.cuno,rsaled.itno sitno,rsaled.itname,bom.itno,銷退金額=(-1)*(" + rmny + @"),銷貨金額=0.0,銷貨數量=0.0,銷退數量=(-1)*rsaled.qty*rsaled.itpkgqty,母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from rsalebom bom
                        right join rsaled on bom.bomid = rsaled.bomid 
                        where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + rback + rQuery;
                        da.Fill(rsaled1);
                    }
                }
                //單一組裝成本
                if (sale)
                    ItemCost(saled23, itemcost, ref dtSource);
                if (rsale)
                    ItemCost(rsaled23, itemcost, ref dtSource);

                //組合品成本
                if (sale)
                    BomItemCost(saled1, itemcost, ref dtSource);
                if (rsale)
                    BomItemCost(rsaled1, itemcost, ref dtSource);

                //結果: 計算毛利
                dtResult = dtSource.Clone();
                if (dtSource.Rows.Count == 0)
                    return;

                var tTemp = dtSource.Clone();
                dtSource.AsEnumerable()
                    .GroupJoin(
                        dtCust.AsEnumerable(),
                        s => s["cuno"].ToString(),
                        c => c["cuno"].ToString(),
                        (s, c) => new
                        {
                            cuno = s["cuno"],
                            cuname1 = c.FirstOrDefault() == null ? "" : c.First()["cuname1"],
                            itno = s["itno"],
                            itname = s["itname"],
                            銷退數量 = s["銷退數量"],
                            銷退金額 = s["銷退金額"],
                            銷貨數量 = s["銷貨數量"],
                            銷貨金額 = s["銷貨金額"],
                            銷貨淨額 = s["銷貨淨額"],
                            銷貨成本 = s["銷貨成本"],
                        })
                    .AsParallel()
                    .GroupBy(gj => new
                    {
                        cuno = gj.cuno,
                        itno = gj.itno
                    })
                    .ForAll(g =>
                    {
                        var 銷退數量 = 0M;
                        var 銷退金額 = 0M;
                        var 銷貨數量 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退數量 += gw.銷退數量.ToDecimal();
                            銷退金額 += gw.銷退金額.ToDecimal();

                            銷貨數量 += gw.銷貨數量.ToDecimal();
                            銷貨金額 += gw.銷貨金額.ToDecimal();

                            銷貨成本 += gw.銷貨成本.ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key.cuno);
                        cq.Enqueue(g.First().cuname1);
                        cq.Enqueue(g.Key.itno);
                        cq.Enqueue(g.First().itname);
                        cq.Enqueue(銷退數量);
                        cq.Enqueue(銷退金額);

                        cq.Enqueue(銷貨數量);
                        cq.Enqueue(銷貨金額);

                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                if (tTemp.Rows.Count > 0)
                    dtResult = tTemp.AsEnumerable().OrderBy(r => r["cuno"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();

                dtSource.Clear();
                dtCust.Clone();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();
                tTemp.Clear();
            }

            void ItemCost(DataTable tSale, DataTable itemCost, ref DataTable dtSource)
            {
                if (tSale.Rows.Count == 0)
                    return;

                var temp = tSale.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        s => s["itno"].ToString(),
                        c => c["itno"].ToString(),
                        (s, c) => new { sale = s, cost = c.DefaultIfEmpty() })
                    .Select(sc => new object[]
                        {
                            sc.sale["cuno"],
                            "",
                            sc.sale["itno"], 
                            sc.sale["itname"], 
                            sc.sale["銷退數量"], 
                            sc.sale["銷退金額"], 
                            sc.sale["銷貨數量"], 
                            sc.sale["銷貨金額"], 
                            0, 
                            ItemCostOrCostP(sc.sale, sc.cost) 
                        });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal ItemCostOrCostP(DataRow sale, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                if (sale["itpkgqty"].ToDecimal() == row["itpkgqty"].ToDecimal())
                    return sale["母數量"].ToDecimal() * row["itcostp"].ToDecimal();
                else
                    return sale["母數量"].ToDecimal() * sale["itpkgqty"].ToDecimal() * row["itcost"].ToDecimal();
            }

            void BomItemCost(DataTable tBom, DataTable itemCost, ref DataTable dtSource)
            {
                if (tBom.Rows.Count == 0)
                    return;

                var temp = tBom.AsEnumerable()
                    .GroupJoin(
                        itemCost.AsEnumerable(),
                        b => b["itno"].ToString(),
                        c => c["itno"].ToString(),
                        (b, c) => new { bom = b, cost = c.DefaultIfEmpty() })
                    .Select((bc) => new
                        {
                            bomid = bc.bom["bomid"],
                            母數量 = bc.bom["母數量"],
                            cuno = bc.bom["cuno"],
                            itno = bc.bom["sitno"],
                            itname = bc.bom["itname"],
                            銷退數量 = bc.bom["銷退數量"],
                            銷退金額 = bc.bom["銷退金額"],
                            銷貨數量 = bc.bom["銷貨數量"],
                            銷貨金額 = bc.bom["銷貨金額"],
                            銷貨淨額 = 0,
                            銷貨成本 = BomItemCostOrCostP(bc.bom, bc.cost)
                        })
                    .GroupBy(o => o.bomid)
                    .Select(g => new object[] 
                        {  
                            g.First().cuno,
                            "",
                            g.First().itno,
                            g.First().itname,
                            g.First().銷退數量,
                            g.First().銷退金額,
                            g.First().銷貨數量,
                            g.First().銷貨金額,
                            g.First().銷貨淨額,
                            g.First().母數量.ToDecimal() * g.Sum(gw => gw.銷貨成本)
                        });

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            decimal BomItemCostOrCostP(DataRow bom, IEnumerable<DataRow> cost)
            {
                var row = cost.FirstOrDefault();
                if (row == null)
                    return 0M;

                if (bom["itpkgqty"].ToDecimal() == row["itpkgqty"].ToDecimal())
                    return bom["子數量"].ToDecimal() * row["itcostp"].ToDecimal();
                else
                    return bom["子數量"].ToDecimal() * bom["itpkgqty"].ToDecimal() * row["itcost"].ToDecimal();
            }

            public void 建檔(out DataTable dtResult)
            {
                DataTable items = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1));
                    cmd.Parameters.AddWithValue("cuno", this.cuno);
                    cmd.Parameters.AddWithValue("cuno1", this.cuno1);
                    var sQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.cuno.Trim().Length > 0) sQuery += "  And saled.cuno>=@cuno";
                    if (this.cuno1.Trim().Length > 0) sQuery += " And saled.cuno<=@cuno1";
                    var rQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.cuno.Trim().Length > 0) rQuery += "  And rsaled.cuno>=@cuno";
                    if (this.cuno1.Trim().Length > 0) rQuery += " And rsaled.cuno<=@cuno1";

                    var back = FrmNSale_Rpt.BackEnd ? "" : " And saled.bracket='前台' ";
                    var rback = FrmNSale_Rpt.BackEnd ? "" : " And rsaled.bracket='前台' ";

                    dtResult = new DataTable();
                    cmd.CommandText = "Select cuno='',cuname1='',itno='',itname='',銷退數量=0.0,銷退金額=0.0,銷貨數量=0.0,銷貨金額=0.0,銷貨淨額=0.0,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtResult);

                    cmd.CommandText = @"
                    Select A.itno,ItCostSlt from 
                    (
                        Select itno from saled  where 0=0 " + back + sQuery + @"
                        union all 
                        Select itno from rsaled where 0=0 " + rback + rQuery + @"
                    ) A left join item on A.itno = item.itno
                    group by A.itno,ItCostSlt ";
                    da.Fill(items);
                }

                if (items.Rows.Count == 0)
                    return;

                DataTable t1, t2, t3;
                月平均成本(out t1);
                最後一次進貨成本(out t2);
                標準成本(out t3);

                ConcurrentStack<DataRow> stack = new ConcurrentStack<DataRow>();
                items.AsEnumerable()
                    .AsParallel()
                    .ForAll(r =>
                    {
                        var itno = r["itno"].ToString().Trim();
                        var slt = r["ItCostSlt"].ToString().Trim();
                        if (slt == "1")
                        {
                            var rows = t1.AsEnumerable().Where(rw => rw["itno"].ToString().Trim() == itno);
                            foreach (var row in rows)
                                stack.Push(row);
                        }
                        else if (slt == "2")
                        {
                            var rows = t2.AsEnumerable().Where(rw => rw["itno"].ToString().Trim() == itno);
                            foreach (var row in rows)
                                stack.Push(row);
                        }
                        else if (slt == "3")
                        {
                            var rows = t3.AsEnumerable().Where(rw => rw["itno"].ToString().Trim() == itno);
                            foreach (var row in rows)
                                stack.Push(row);
                        }
                    });

                //結果
                if (stack.Count > 0)
                    dtResult = stack.OrderBy(r => r["cuno"].ToString()).ThenBy(r => r["itno"].ToString()).CopyToDataTable();

                t1.Clear();
                t2.Clear();
                t3.Clear();
            }


        }

        JBS.JS.xEvents xe;

        public static bool BackEnd { get; set; }

        public FrmNSale_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmNSale_Rpt_Load(object sender, EventArgs e)
        {
            SaDate.SetDateLength();
            SaDate1.SetDateLength();
            if (Common.User_DateTime == 1)
            {
                SaDate.Text = Date.GetDateTime(1, false);
                SaDate.Text = SaDate.Text.Remove(5) + "01";
                SaDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                SaDate.Text = Date.GetDateTime(2, false);
                SaDate.Text = SaDate.Text.Remove(6) + "01";
                SaDate1.Text = Date.GetDateTime(2, false);
            }
        }

        private void FrmNSale_Rpt_Shown(object sender, EventArgs e)
        {
            SaDate.Focus();
        }

        private void SaDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tx = sender as TextBox;
            if (tx.Text.Trim().Length > 0)
            {
                if (!tx.IsDateTime())
                {
                    e.Cancel = true;
                    tx.SelectAll();
                    MessageBox.Show("日期格式錯誤!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                tx.Clear();
                e.Cancel = true;
                MessageBox.Show("請輸入日期!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tx.SelectAll();
            }
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tx = sender as TextBox;
            if (tx.TrimTextLenth() == 0)
            {
                tx.Clear();
                return;
            }

            xe.ValidateOpen<JBS.JS.Cust>(sender, e, row => tx.Text = row["cuno"].ToString(), true);
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool Compare(TextBox tx, TextBox tx1)
        {
            bool Isflag = true;
            if (tx.Text != "" && tx1.Text != "")
            {
                if (string.CompareOrdinal(tx.Text, tx1.Text) > 0)
                {
                    Isflag = false;
                    MessageBox.Show("起始" + tx.Tag + "不可大於終止" + tx1.Tag + "，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx.Focus();
                }
            }
            return Isflag;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            FrmNSale_Rpt.BackEnd = ckBackend.Checked;

            if (!Compare(SaDate, SaDate1)) return;
            if (!Compare(CuNo, CuNo1)) return;
            if (ckSale.Checked == false && ckRsale.Checked == false)
            {
                MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtResult = new DataTable();

            var out1 = Date.ToTWDate(SaDate.Text).takeString(3).ToInteger() < Common.Sys_StkYear1;
            var out2 = Date.ToTWDate(SaDate1.Text).takeString(3).ToInteger() < Common.Sys_StkYear1;

            if (out1 && (out2 == false))
            {
                MessageBox.Show("查詢日期, 請勿橫跨庫存年度!");
                return;
            }

            if (out1 && out2)
            {
                rdCost.Checked = true;
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, CuNo.Text, CuNo1.Text).標準成本(out dtResult);
            }
            else if (rdAvg.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, CuNo.Text, CuNo1.Text).月平均成本(out dtResult);
            }
            else if (rdLast.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, CuNo.Text, CuNo1.Text).最後一次進貨成本(out dtResult);
            }
            else if (rdCost.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, CuNo.Text, CuNo1.Text).標準成本(out dtResult);
            }
            else if (rdItem.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, CuNo.Text, CuNo1.Text).建檔(out dtResult);
            }

            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            //if (rdSimple.Checked)
            //{
            //    #region rdSimple
            //    using (var frm = new S_61.subMenuFm_4.FrmSale_Rptb())
            //    {
            //        frm.dt = dtResult;
            //        frm.dateRange = SaDate.Text + "～" + SaDate1.Text;
            //        frm.ShowDialog();

            //        dtResult.Clear();
            //    }
            //    #endregion
            //    return;
            //}
            //else
            {
                #region 合併成客戶群組
                var tTemp = dtResult.Clone();
                dtResult.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["cuno"].ToString().Trim())
                    .ForAll(g =>
                    {
                        var 銷退金額 = 0M;
                        var 銷貨金額 = 0M;
                        var 銷貨淨額 = 0M;
                        var 銷貨成本 = 0M;
                        var 銷貨毛利 = 0M;
                        var 毛利率 = 0M;
                        foreach (var gw in g)
                        {
                            銷退金額 += gw["銷退金額"].ToDecimal();
                            銷貨金額 += gw["銷貨金額"].ToDecimal();
                            銷貨成本 += gw["銷貨成本"].ToDecimal();
                        }
                        銷貨淨額 = 銷退金額 + 銷貨金額;
                        銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        cq.Enqueue(g.Key);
                        cq.Enqueue(g.First()["cuname1"]);
                        cq.Enqueue("");
                        cq.Enqueue("");
                        cq.Enqueue(0);
                        cq.Enqueue(銷退金額);

                        cq.Enqueue(0);
                        cq.Enqueue(銷貨金額);

                        cq.Enqueue(銷貨淨額);
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                using (var frm = new S_61.subMenuFm_4.FrmSale_Rptc(true))
                {
                    frm.dt = tTemp.AsEnumerable().OrderBy(r => r["cuno"].ToString()).CopyToDataTable();
                    frm.dateRange = SaDate.Text + "～" + SaDate1.Text;
                    frm.ShowDialog();

                    tTemp.Clone();
                    dtResult.Clear();
                }
                #endregion
                return;
            }
        }

    }
}
