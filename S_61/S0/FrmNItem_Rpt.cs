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
    public partial class FrmNItem_Rpt : Formbase
    {
        class 成本計算
        {
            bool sale = false;
            bool rsale = false;
            string ymonth = "";
            string ymonth1 = "";
            string itno = "";
            string itno1 = "";

            public 成本計算(bool s, bool r, string y, string y1, string c, string c1)
            {
                this.sale = s;
                this.rsale = r;
                this.ymonth = y;
                this.ymonth1 = y1;
                this.itno = c;
                this.itno1 = c1;
            }

            /// <summary>
            /// 組合品取子件, 計算成本
            /// </summary>
            public void 月平均成本(out DataTable dtResult)
            {
                DataTable dtSource = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合
                DataTable saleSandR = new DataTable();

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
                    cmd.Parameters.AddWithValue("itno", this.itno);
                    cmd.Parameters.AddWithValue("itno1", this.itno1);
                    var sQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.itno.Trim().Length > 0) sQuery += "  And saled.itno>=@itno";
                    if (this.itno1.Trim().Length > 0) sQuery += " And saled.itno<=@itno1";
                    var rQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.itno.Trim().Length > 0) rQuery += "  And rsaled.itno>=@itno";
                    if (this.itno1.Trim().Length > 0) rQuery += " And rsaled.itno<=@itno1";

                    var mny = "(saled.qty)*(saled.itpkgqty)*(saled.prs)*(saled.priceb)";
                    var rmny = "(rsaled.qty)*(rsaled.itpkgqty)*(rsaled.prs)*(rsaled.priceb)";
                    var back = FrmNItem_Rpt.BackEnd ? "" : " And saled.bracket='前台' ";
                    var rback = FrmNItem_Rpt.BackEnd ? "" : " And rsaled.bracket='前台' ";

                    cmd.CommandText = @"
                    Select 序號='', 單據日期=sadate, 單據='銷貨', cust.cuname1
                    ,sano, saID, sadate, sadate1, sadateac1, sadateac, saled.cuno, orno, itno, itname, ittrait, itunit, itpkgqty
                    ,qty=ISNULL(qty,0), price, prs, taxprice, mny=ISNULL((" + mny + @"),0), memo, bomid, bomrec, recordno, Pqty, Punit 
                    from saled 
                    left join cust on saled.cuno = cust.cuno
                    where 0=0 " + back + sQuery;
                    da.Fill(saleSandR);

                    cmd.CommandText = @"
                    Select 序號='', 單據日期=sadate, 單據='銷退', cust.cuname1
                    ,sano, saID, sadate, sadate1, sadateac1, sadateac, rsaled.cuno, orno, itno, itname, ittrait, itunit, itpkgqty
                    ,qty=(-1)*ISNULL(qty,0), price, prs, taxprice, mny=(-1)*ISNULL((" + rmny + @"),0), memo, bomid, bomrec, recordno, Pqty, Punit 
                    from rsaled 
                    left join cust on rsaled.cuno = cust.cuno
                    where 0=0 " + rback + rQuery;
                    da.Fill(saleSandR);

                    cmd.CommandText = "Select 單據='',bomid='',銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

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

                    if (sale)
                    {
                        cmd.CommandText = "Select 單據='銷貨',saled.bomid,substring(sadate,1,5)月份,itno,mny=ISNULL((" + mny + @"),0),母數量=qty*itpkgqty from saled where ittrait <> 1 " + back + sQuery;
                        da.Fill(saled23);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = "Select 單據='銷退',rsaled.bomid,substring(sadate,1,5)月份,itno,mny=(-1)*ISNULL((" + rmny + @"),0),母數量=(-1)*qty*itpkgqty from rsaled where ittrait <> 1 " + rback + rQuery;
                        da.Fill(rsaled23);
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,單據='銷貨',saled.bomid,substring(sadate,1,5)月份,saled.itno sitno,bom.itno,mny=ISNULL((" + mny + @"),0),母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from salebom bom
                        right join saled on bom.bomid = saled.bomid 
                        where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + sQuery;
                        da.Fill(saled1);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,單據='銷退',rsaled.bomid,substring(sadate,1,5)月份,rsaled.itno sitno,bom.itno,mny=(-1)*ISNULL((" + rmny + @"),0),母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from rsalebom bom
                        right join rsaled on bom.bomid = rsaled.bomid 
                        where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + rQuery;
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

                var tTemp = saleSandR.Clone();
                tTemp.Columns.Add("銷貨成本", typeof(Decimal));
                tTemp.Columns.Add("銷貨毛利", typeof(Decimal));
                tTemp.Columns.Add("毛利率", typeof(Decimal));

                saleSandR.AsEnumerable()
                    .GroupJoin(
                        dtSource.AsEnumerable(),
                        sr => new { 單據 = sr["單據"].ToString(), bomid = sr["bomid"].ToString() },
                        s => new { 單據 = s["單據"].ToString(), bomid = s["bomid"].ToString() },
                        (sr, s) => new
                        {
                            obj = sr.ItemArray,
                            銷貨成本 = s.First()["銷貨成本"],
                            銷貨毛利 = s.First()["銷貨毛利"],
                            毛利率 = s.First()["毛利率"],
                        })
                    .AsParallel()
                    .ForAll(srs =>
                    {
                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        foreach (var o in srs.obj)
                        {
                            cq.Enqueue(o);
                        }
                        cq.Enqueue(srs.銷貨成本);
                        cq.Enqueue(srs.銷貨毛利);
                        cq.Enqueue(srs.毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                if (tTemp.Rows.Count > 0)
                    dtResult = tTemp.AsEnumerable().OrderBy(r => r["itno"].ToString()).ThenBy(r => r["單據日期"].ToString()).CopyToDataTable();

                dtSource.Clear();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();
                saleSandR.Clear();
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
                    .Select(sc => JoinAvg(sc.sale, sc.cost));

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            object[] JoinAvg(DataRow sale, IEnumerable<DataRow> cost)
            {
                var 銷貨淨額 = sale["mny"].ToDecimal();
                var 銷貨成本 = 0M;
                var 銷貨毛利 = 0M;
                var 毛利率 = 0M;

                var row = cost.FirstOrDefault();
                if (row != null)
                    銷貨成本 = sale["母數量"].ToDecimal() * row["itcost"].ToDecimal();

                銷貨毛利 = 銷貨淨額 - 銷貨成本;
                if (銷貨淨額 != 0)
                    毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                return new object[] { sale["單據"], sale["bomid"], 銷貨成本, 銷貨毛利, 毛利率 };
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
                    .Select(bc => new tempClass
                    {
                        母數量 = bc.bom["母數量"],
                        單據 = bc.bom["單據"],
                        bomid = bc.bom["bomid"],
                        銷貨淨額 = bc.bom["mny"],
                        銷貨成本 = BomJoinAvg(bc.bom, bc.cost)
                    })
                    .GroupBy(o => o.bomid)
                    .Select(g => BomGroupAvg(g));

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

            object[] BomGroupAvg(IGrouping<object, tempClass> g)
            {
                var 銷貨淨額 = g.First().銷貨淨額.ToDecimal();
                var 銷貨成本 = g.First().母數量.ToDecimal() * g.Sum(gw => gw.銷貨成本);
                var 銷貨毛利 = 0M;
                var 毛利率 = 0M;

                銷貨毛利 = 銷貨淨額 - 銷貨成本;
                if (銷貨淨額 != 0)
                    毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                return new object[] { g.First().單據, g.First().bomid, 銷貨成本, 銷貨毛利, 毛利率 };
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
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合
                DataTable saleSandR = new DataTable();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day0", Date.ToTWDate(Common.Sys_StkYear1 - 1 + "0101"));
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1));
                    cmd.Parameters.AddWithValue("itno", this.itno);
                    cmd.Parameters.AddWithValue("itno1", this.itno1);
                    var sQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.itno.Trim().Length > 0) sQuery += "  And saled.itno>=@itno";
                    if (this.itno1.Trim().Length > 0) sQuery += " And saled.itno<=@itno1";
                    var rQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.itno.Trim().Length > 0) rQuery += "  And rsaled.itno>=@itno";
                    if (this.itno1.Trim().Length > 0) rQuery += " And rsaled.itno<=@itno1";

                    var mny = "(saled.qty)*(saled.itpkgqty)*(saled.prs)*(saled.priceb)";
                    var rmny = "(rsaled.qty)*(rsaled.itpkgqty)*(rsaled.prs)*(rsaled.priceb)";
                    var back = FrmNItem_Rpt.BackEnd ? "" : " And saled.bracket='前台' ";
                    var rback = FrmNItem_Rpt.BackEnd ? "" : " And rsaled.bracket='前台' ";

                    cmd.CommandText = @"
                    Select 序號='', 單據日期=sadate, 單據='銷貨', cust.cuname1
                    ,sano, saID, sadate, sadate1, sadateac1, sadateac, saled.cuno, orno, itno, itname, ittrait, itunit, itpkgqty
                    ,qty=ISNULL(qty,0), price, prs, taxprice, mny=ISNULL((" + mny + @"),0), memo, bomid, bomrec, recordno, Pqty, Punit 
                    from saled 
                    left join cust on saled.cuno = cust.cuno
                    where 0=0 " + back + sQuery;
                    da.Fill(saleSandR);

                    cmd.CommandText = @"
                    Select 序號='', 單據日期=sadate, 單據='銷退', cust.cuname1
                    ,sano, saID, sadate, sadate1, sadateac1, sadateac, rsaled.cuno, orno, itno, itname, ittrait, itunit, itpkgqty
                    ,qty=(-1)*ISNULL(qty,0), price, prs, taxprice, mny=(-1)*ISNULL((" + rmny + @"),0), memo, bomid, bomrec, recordno, Pqty, Punit 
                    from rsaled 
                    left join cust on rsaled.cuno = cust.cuno
                    where 0=0 " + rback + rQuery;
                    da.Fill(saleSandR);

                    cmd.CommandText = "Select 單據='',bomid='',銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = @"Select itno,bsdate,itunit,itpkgqty,realcost from bshopd Where bsdate >=@day0 and bsdate<=@day1 order by itno asc,bsdate desc,bsno desc";
                    da.Fill(itemcost);

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select  單據='銷貨',saled.bomid,saled.sadate,saled.itno,mny=ISNULL((" + mny + @"),0),母數量=saled.qty,saled.itunit,saled.itpkgqty,item.itbuypri
                        From saled 
                        Left join item on saled.itno = item.itno
                        where saled.ittrait <> 1 " + back + sQuery;
                        da.Fill(saled23);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select  單據='銷退',rsaled.bomid,rsaled.sadate,rsaled.itno,mny=(-1)*ISNULL((" + rmny + @"),0),母數量=(-1)*rsaled.qty,rsaled.itunit,rsaled.itpkgqty,item.itbuypri
                        From rsaled 
                        Left join item on rsaled.itno = item.itno
                        where rsaled.ittrait <> 1 " + rback + rQuery;
                        da.Fill(rsaled23);
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,單據='銷貨',saled.bomid,saled.sadate,saled.itno sitno,bom.itno,mny=ISNULL((" + mny + @"),0),母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
                        From saled 
                        Left join item on saled.itno = item.itno
                        Left join salebom bom on bom.bomid = saled.bomid 
                        Where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + back + sQuery;
                        da.Fill(saled1);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,單據='銷退',rsaled.bomid,rsaled.sadate,rsaled.itno sitno,bom.itno,mny=(-1)*ISNULL((" + rmny + @"),0),母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
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

                var tTemp = saleSandR.Clone();
                tTemp.Columns.Add("銷貨成本", typeof(Decimal));
                tTemp.Columns.Add("銷貨毛利", typeof(Decimal));
                tTemp.Columns.Add("毛利率", typeof(Decimal));

                saleSandR.AsEnumerable()
                    .GroupJoin(
                        dtSource.AsEnumerable(),
                        sr => new { 單據 = sr["單據"].ToString(), bomid = sr["bomid"].ToString() },
                        s => new { 單據 = s["單據"].ToString(), bomid = s["bomid"].ToString() },
                        (sr, s) => new
                        {
                            obj = sr.ItemArray,
                            銷貨成本 = s.First()["銷貨成本"],
                            銷貨毛利 = s.First()["銷貨毛利"],
                            毛利率 = s.First()["毛利率"],
                        })
                    .AsParallel()
                    .ForAll(srs =>
                    {
                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        foreach (var o in srs.obj)
                        {
                            cq.Enqueue(o);
                        }
                        cq.Enqueue(srs.銷貨成本);
                        cq.Enqueue(srs.銷貨毛利);
                        cq.Enqueue(srs.毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                if (tTemp.Rows.Count > 0)
                    dtResult = tTemp.AsEnumerable().OrderBy(r => r["itno"].ToString()).ThenBy(r => r["單據日期"].ToString()).CopyToDataTable();

                dtSource.Clear();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();
                saleSandR.Clear();
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
                            var 銷貨淨額 = r["mny"].ToDecimal();
                            var 銷貨成本 = r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * r["itbuypri"].ToDecimal();
                            var 銷貨毛利 = 0M;
                            var 毛利率 = 0M;

                            銷貨毛利 = 銷貨淨額 - 銷貨成本;
                            if (銷貨淨額 != 0)
                                毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                            ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                            cq.Enqueue(r["單據"]);
                            cq.Enqueue(r["bomid"]);
                            cq.Enqueue(銷貨成本);
                            cq.Enqueue(銷貨毛利);
                            cq.Enqueue(毛利率);

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
                                var 銷貨淨額 = r["mny"].ToDecimal();
                                var 銷貨成本 = r["母數量"].ToDecimal() * row["realcost"].ToDecimal();
                                var 銷貨毛利 = 0M;
                                var 毛利率 = 0M;

                                銷貨毛利 = 銷貨淨額 - 銷貨成本;
                                if (銷貨淨額 != 0)
                                    毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["單據"]);
                                cq.Enqueue(r["bomid"]);
                                cq.Enqueue(銷貨成本);
                                cq.Enqueue(銷貨毛利);
                                cq.Enqueue(毛利率);

                                lock (dtSource.Rows.SyncRoot)
                                {
                                    dtSource.Rows.Add(cq.ToArray());
                                }
                            }
                            else
                            {
                                var 銷貨淨額 = r["mny"].ToDecimal();
                                var 銷貨成本 = 0M;
                                var 銷貨毛利 = 0M;
                                var 毛利率 = 0M;

                                if (bshoppkgqty != 0)
                                    銷貨成本 = r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * (row["realcost"].ToDecimal() / bshoppkgqty);

                                銷貨毛利 = 銷貨淨額 - 銷貨成本;
                                if (銷貨淨額 != 0)
                                    毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["單據"]);
                                cq.Enqueue(r["bomid"]);
                                cq.Enqueue(銷貨成本);
                                cq.Enqueue(銷貨毛利);
                                cq.Enqueue(毛利率);

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
                    .Select(g => BomGroupLastCost(g));

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            object[] BomGroupLastCost(IGrouping<string, DataRow> g)
            {
                var 銷貨淨額 = g.First()["mny"].ToDecimal();
                var 銷貨成本 = g.First()["母數量"].ToDecimal() * g.Sum(gw => gw["銷貨成本"].ToDecimal());
                var 銷貨毛利 = 0M;
                var 毛利率 = 0M;

                銷貨毛利 = 銷貨淨額 - 銷貨成本;
                if (銷貨淨額 != 0)
                    毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                return new object[] { g.First()["單據"], g.First()["bomid"], 銷貨成本, 銷貨毛利, 毛利率 };
            }

            /// <summary>
            /// 組合品取子件, 計算成本
            /// </summary> 
            public void 標準成本(out DataTable dtResult)
            {
                DataTable dtSource = new DataTable();
                DataTable itemcost = new DataTable();
                DataTable saled23 = new DataTable();//組裝單一
                DataTable rsaled23 = new DataTable();//組裝單一
                DataTable saled1 = new DataTable();//組合
                DataTable rsaled1 = new DataTable();//組合
                DataTable saleSandR = new DataTable();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("day", Date.ToTWDate(ymonth));
                    cmd.Parameters.AddWithValue("day1", Date.ToTWDate(ymonth1));
                    cmd.Parameters.AddWithValue("itno", this.itno);
                    cmd.Parameters.AddWithValue("itno1", this.itno1);
                    var sQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.itno.Trim().Length > 0) sQuery += "  And saled.itno>=@itno";
                    if (this.itno1.Trim().Length > 0) sQuery += " And saled.itno<=@itno1";
                    var rQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.itno.Trim().Length > 0) rQuery += "  And rsaled.itno>=@itno";
                    if (this.itno1.Trim().Length > 0) rQuery += " And rsaled.itno<=@itno1";

                    var mny = "(saled.qty)*(saled.itpkgqty)*(saled.prs)*(saled.priceb)";
                    var rmny = "(rsaled.qty)*(rsaled.itpkgqty)*(rsaled.prs)*(rsaled.priceb)";
                    var back = FrmNItem_Rpt.BackEnd ? "" : " And saled.bracket='前台' ";
                    var rback = FrmNItem_Rpt.BackEnd ? "" : " And rsaled.bracket='前台' ";

                    cmd.CommandText = @"
                    Select 序號='', 單據日期=sadate, 單據='銷貨', cust.cuname1
                    ,sano, saID, sadate, sadate1, sadateac1, sadateac, saled.cuno, orno, itno, itname, ittrait, itunit, itpkgqty
                    ,qty=ISNULL(qty,0), price, prs, taxprice, mny=ISNULL((" + mny + @"),0), memo, bomid, bomrec, recordno, Pqty, Punit 
                    from saled 
                    left join cust on saled.cuno = cust.cuno
                    where 0=0 " + back + sQuery;
                    da.Fill(saleSandR);

                    cmd.CommandText = @"
                    Select 序號='', 單據日期=sadate, 單據='銷退', cust.cuname1
                    ,sano, saID, sadate, sadate1, sadateac1, sadateac, rsaled.cuno, orno, itno, itname, ittrait, itunit, itpkgqty
                    ,qty=(-1)*ISNULL(qty,0), price, prs, taxprice, mny=(-1)*ISNULL((" + rmny + @"),0), memo, bomid, bomrec, recordno, Pqty, Punit 
                    from rsaled 
                    left join cust on rsaled.cuno = cust.cuno
                    where 0=0 " + rback + rQuery;
                    da.Fill(saleSandR);

                    cmd.CommandText = "Select 單據='',bomid='',銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = @"Select itno,itpkgqty,itcost,itcostp from item";
                    da.Fill(itemcost);

                    if (sale)
                    {
                        cmd.CommandText = "Select 單據='銷貨',saled.bomid,itno,mny=ISNULL((" + mny + @"),0),母數量=qty,itpkgqty from saled where ittrait <> 1 " + back + sQuery;
                        da.Fill(saled23);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = "Select 單據='銷退',rsaled.bomid,itno,mny=(-1)*ISNULL((" + rmny + @"),0),母數量=(-1)*qty,itpkgqty from rsaled where ittrait <> 1 " + rback + rQuery;
                        da.Fill(rsaled23);
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,單據='銷貨',saled.bomid,saled.itno sitno,bom.itno,mny=ISNULL((" + mny + @"),0),母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from salebom bom
                        right join saled on bom.bomid = saled.bomid 
                        where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + back + sQuery;
                        da.Fill(saled1);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 銷貨成本=0.0,單據='銷退',rsaled.bomid,rsaled.itno sitno,bom.itno,mny=(-1)*ISNULL((" + rmny + @"),0),母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from rsalebom bom
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

                var tTemp = saleSandR.Clone();
                tTemp.Columns.Add("銷貨成本", typeof(Decimal));
                tTemp.Columns.Add("銷貨毛利", typeof(Decimal));
                tTemp.Columns.Add("毛利率", typeof(Decimal));

                saleSandR.AsEnumerable()
                    .GroupJoin(
                        dtSource.AsEnumerable(),
                        sr => new { 單據 = sr["單據"].ToString(), bomid = sr["bomid"].ToString() },
                        s => new { 單據 = s["單據"].ToString(), bomid = s["bomid"].ToString() },
                        (sr, s) => new
                        {
                            obj = sr.ItemArray,
                            銷貨成本 = s.First()["銷貨成本"],
                            銷貨毛利 = s.First()["銷貨毛利"],
                            毛利率 = s.First()["毛利率"],
                        })
                    .AsParallel()
                    .ForAll(srs =>
                    {
                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        foreach (var o in srs.obj)
                        {
                            cq.Enqueue(o);
                        }
                        cq.Enqueue(srs.銷貨成本);
                        cq.Enqueue(srs.銷貨毛利);
                        cq.Enqueue(srs.毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                if (tTemp.Rows.Count > 0)
                    dtResult = tTemp.AsEnumerable().OrderBy(r => r["itno"].ToString()).ThenBy(r => r["單據日期"].ToString()).CopyToDataTable();

                dtSource.Clear();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();
                saleSandR.Clear();
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
                    .Select(sc => ItemCostOrCostP(sc.sale, sc.cost));

                if (temp.Count() > 0)
                {
                    foreach (object[] obj in temp)
                        dtSource.Rows.Add(obj);
                }
            }

            object[] ItemCostOrCostP(DataRow sale, IEnumerable<DataRow> cost)
            {
                var 銷貨淨額 = sale["mny"].ToDecimal();
                var 銷貨成本 = 0M;
                var 銷貨毛利 = 0M;
                var 毛利率 = 0M;

                var row = cost.FirstOrDefault();
                if (row != null)
                {
                    if (sale["itpkgqty"].ToDecimal() == row["itpkgqty"].ToDecimal())
                        銷貨成本 = sale["母數量"].ToDecimal() * row["itcostp"].ToDecimal();
                    else
                        銷貨成本 = sale["母數量"].ToDecimal() * sale["itpkgqty"].ToDecimal() * row["itcost"].ToDecimal();
                }

                銷貨毛利 = 銷貨淨額 - 銷貨成本;
                if (銷貨淨額 != 0)
                    毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                return new object[] { sale["單據"], sale["bomid"], 銷貨成本, 銷貨毛利, 毛利率 };
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
                    .Select((bc) => new tempClass
                    {
                        母數量 = bc.bom["母數量"],
                        單據 = bc.bom["單據"],
                        bomid = bc.bom["bomid"],
                        銷貨淨額 = bc.bom["mny"],
                        銷貨成本 = BomItemCostOrCostP(bc.bom, bc.cost)
                    })
                    .GroupBy(o => o.bomid)
                    .Select(g => BomGroupItemCost(g));

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

            object[] BomGroupItemCost(IGrouping<object, tempClass> g)
            {
                var 銷貨淨額 = g.First().銷貨淨額.ToDecimal();
                var 銷貨成本 = g.First().母數量.ToDecimal() * g.Sum(gw => gw.銷貨成本);
                var 銷貨毛利 = 0M;
                var 毛利率 = 0M;

                銷貨毛利 = 銷貨淨額 - 銷貨成本;
                if (銷貨淨額 != 0)
                    毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                return new object[] { g.First().單據, g.First().bomid, 銷貨成本, 銷貨毛利, 毛利率 };
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
                    cmd.Parameters.AddWithValue("itno", this.itno);
                    cmd.Parameters.AddWithValue("itno1", this.itno1);
                    var sQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.itno.Trim().Length > 0) sQuery += "  And saled.itno>=@itno";
                    if (this.itno1.Trim().Length > 0) sQuery += " And saled.itno<=@itno1";
                    var rQuery = " And sadate>=@day and sadate<=@day1 ";
                    if (this.itno.Trim().Length > 0) rQuery += "  And rsaled.itno>=@itno";
                    if (this.itno1.Trim().Length > 0) rQuery += " And rsaled.itno<=@itno1";
                     
                    var back = FrmNItem_Rpt.BackEnd ? "" : " And saled.bracket='前台' ";
                    var rback = FrmNItem_Rpt.BackEnd ? "" : " And rsaled.bracket='前台' ";

                    dtResult = new DataTable();
                    cmd.CommandText = @"
                    Select 序號='', 單據日期=sadate, 單據='銷貨', cust.cuname1
                    ,sano, saID, sadate, sadate1, sadateac1, sadateac, saled.cuno, orno, itno, itname, ittrait, itunit, itpkgqty
                    ,qty=ISNULL(qty,0), price, prs, taxprice, mny=ISNULL(mnyb,0), memo, bomid, bomrec, recordno, Pqty, Punit 
                    ,銷貨成本=0.0,銷貨毛利=0.0,毛利率=0.0
                    from saled 
                    left join cust on saled.cuno = cust.cuno
                    where 1=0 ";
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
                    dtResult = stack.OrderBy(r => r["itno"].ToString()).ThenBy(r => r["單據日期"].ToString()).CopyToDataTable();

                t1.Clear();
                t2.Clear();
                t3.Clear();
            }

            void Alert(decimal i)
            {
                System.Windows.Forms.MessageBox.Show(i.ToString());
            }

            class tempClass
            {
                public object 母數量 { get; set; }
                public object 單據 { get; set; }
                public object bomid { get; set; }
                public object 銷貨淨額 { get; set; }
                public decimal 銷貨成本 { get; set; }
            }
        }

        JBS.JS.xEvents xe;

        public static bool BackEnd { get; set; }

        public FrmNItem_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            groupBoxT4.Enabled = false;
        }

        private void FrmNItem_Rpt_Load(object sender, EventArgs e)
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

        private void FrmNItem_Rpt_Shown(object sender, EventArgs e)
        {
            SaDate.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rdrank_CheckedChanged(object sender, EventArgs e)
        {
            if (sender.Equals(rdSimple))
            {
                Rank.Clear();
                Rank.ReadOnly = true;
                groupBoxT4.Enabled = false;
            }
            else
            {
                Rank.ReadOnly = false;
                groupBoxT4.Enabled = true;
            }
        }

        private void SaDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void INo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void INo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tx = sender as TextBox;
            if (tx.Text.Trim().Length == 0)
                return;

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                tx.Text = row["itno"].ToString().Trim();
            });
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
            FrmNItem_Rpt.BackEnd = ckBackend.Checked;

            if (!Compare(SaDate, SaDate1)) return;
            if (!Compare(ItNo, ItNo1)) return;
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
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, ItNo.Text, ItNo1.Text).標準成本(out dtResult);
            }
            else if (rdAvg.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, ItNo.Text, ItNo1.Text).月平均成本(out dtResult);
            }
            else if (rdLast.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, ItNo.Text, ItNo1.Text).最後一次進貨成本(out dtResult);
            }
            else if (rdCost.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, ItNo.Text, ItNo1.Text).標準成本(out dtResult);
            }
            else if (rdItem.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, SaDate.Text, SaDate1.Text, ItNo.Text, ItNo1.Text).建檔(out dtResult);
            }

            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            //if (rdSimple.Checked)
            //{
            //    #region rdSimple
            //    using (var frm = new FrmItemSale_Rptb())
            //    {
            //        frm.TResult = dtResult;
            //        frm.dateRange = SaDate.Text + "～" + SaDate1.Text;
            //        frm.ShowDialog();

            //        dtResult.Clear();
            //    }
            //    #endregion
            //    return;
            //}
            //else
            {
                #region 將所有資料依產品群組
                var tTemp = dtResult.Clone();
                dtResult.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["itno"].ToString().Trim())
                    .ForAll(g =>
                    {
                        var obj = g.First().ItemArray;
                        var tQty = 0M;
                        var tMny = 0M;
                        var 銷貨成本 = 0M;

                        foreach (var gw in g)
                        {
                            tQty += gw["qty"].ToDecimal();
                            tMny += gw["mny"].ToDecimal();
                            銷貨成本 += gw["銷貨成本"].ToDecimal();
                        }

                        var 銷貨毛利 = tMny - 銷貨成本;
                        var 毛利率 = 0M;
                        if (tMny != 0)
                            毛利率 = ((銷貨毛利 / tMny) * 100);

                        ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                        var xQty = dtResult.Columns["qty"].Ordinal;
                        var xMny = dtResult.Columns["mny"].Ordinal;
                        for (int i = 0; i < obj.Length - 3; i++)
                        {
                            if (i == xQty)
                                cq.Enqueue(tQty);
                            else if (i == xMny)
                                cq.Enqueue(tMny);
                            else
                                cq.Enqueue(obj.ElementAt(i));
                        }
                        cq.Enqueue(銷貨成本);
                        cq.Enqueue(銷貨毛利);
                        cq.Enqueue(毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });
                #endregion

                using (var frm = new S_61.subMenuFm_4.FrmItemSale_Rptc(true))
                {
                    var rk = 0;
                    if (Rank.Text.ToDecimal() > 0)
                        rk = Rank.Text.ToInteger();

                    if (radioT1.Checked)
                        frm.choosemode = 1;
                    else if (radioT2.Checked)
                        frm.choosemode = 2;
                    else
                        frm.choosemode = 3;

                    tTemp.DefaultView.Sort = "銷貨毛利 desc";
                    frm.TResult = tTemp.DefaultView.ToTable();
                    tTemp.Clear();

                    frm.rk = rk;
                    frm.dateRange = SaDate.Text + "～" + SaDate1.Text;
                    frm.ShowDialog();
                }
                return;
            }
        }

    }
}
