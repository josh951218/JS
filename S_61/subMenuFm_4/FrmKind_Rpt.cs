using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmKind_Rpt : Formbase
    {
        class 成本計算
        {
            bool ifkind = false;
            bool ifxx01 = false;
            bool sale = false;
            bool rsale = false;
            string sQuery = "";
            string rQuery = "";
            Action<SqlParameterCollection> sParaList;
            bool 各倉成本 = false;

            public 成本計算(bool s, bool r, Action<SqlParameterCollection> spara, string sqstr, string rqstr, bool ifkind, bool ifxx01, bool 各倉成本 = false)
            {
                this.sale = s;
                this.rsale = r;
                this.sParaList = spara;
                this.sQuery = sqstr;
                this.rQuery = rqstr;
                this.ifkind = ifkind;
                this.ifxx01 = ifxx01;
                this.各倉成本 = 各倉成本;
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
                    this.sParaList(cmd.Parameters);

                    string skind = "", rkind = "", sxx01 = "", rxx01 = "";
                    if (ifkind) skind = @"
                        left join item on saled.itno = item.itno
                        left join kind on item.kino = kind.kino ";

                    if (ifkind) rkind = @"
                        left join item on rsaled.itno = item.itno
                        left join kind on item.kino = kind.kino ";

                    if (ifxx01) sxx01 = @"
                        left join cust on saled.cuno = cust.cuno
                        left join xx01 on cust.cux1no = xx01.x1no ";

                    if (ifxx01) rxx01 = @"
                        left join cust on rsaled.cuno = cust.cuno
                        left join xx01 on cust.cux1no = xx01.x1no ";

                    cmd.CommandText = @"
                    Select 
                    單據='銷貨',憑證=saled.sano,單據日期=saled.sadate,訂單憑證=saled.orno,客戶編號=saled.cuno,業務編號=saled.emno,專案編號=saled.spno,送貨編號=saled.seno,倉庫名稱=saled.stname,saled.bomid,saled.ittrait
                    ,產品編號=saled.itno,品名規格=saled.itname,數量=saled.qty,單位=saled.itunit,售價=saled.priceb,折數=saled.prs,稅前售價=saled.taxpriceb,稅前金額=saled.mnyb,包裝數量=saled.itpkgqty,說明1=saled.memo
                    ,業務人員=SA.emname,送貨方式=SA.sename,專案名稱=SA.spname,結帳方式=SA.x4name,說明=SA.samemo
                    ,客戶簡稱=cust.cuname1,客戶名稱=cust.cuper1,客戶電話=cust.cutel1,客戶傳真=cust.cufax1,客戶類別=cust.cux1no
                    ,客戶類別名稱=X1.x1name
                    ,輔助編號=item.itnoudf,產品類別=item.kino
                    ,類別名稱=KD.kiname
                    ,flag=0
                    from saled 
                    left join sale SA on saled.sano = SA.sano
                    left join cust on saled.cuno = cust.cuno
                    left join xx01 X1 on cust.cux1no = X1.x1no
                    left join item item on saled.itno = item.itno
                    left join kind KD on item.kino = KD.kino
                    
                    where 0=0 " + sQuery;
                    if (sale)
                        da.Fill(saleSandR);

                    cmd.CommandText = @"
                    Select
                    單據='銷退',憑證=rsaled.sano,單據日期=rsaled.sadate,訂單憑證=rsaled.orno,客戶編號=rsaled.cuno,業務編號=rsaled.emno,專案編號=rsaled.spno,送貨編號=rsaled.seno,倉庫名稱=rsaled.stname,rsaled.bomid,rsaled.ittrait
                    ,產品編號=rsaled.itno,品名規格=rsaled.itname,數量=(-1)*rsaled.qty,單位=rsaled.itunit,售價=rsaled.priceb,折數=rsaled.prs,稅前售價=rsaled.taxpriceb,稅前金額=(-1)*rsaled.mnyb,包裝數量=rsaled.itpkgqty,說明1=rsaled.memo
                    ,業務人員=SA.emname,送貨方式=SA.sename,專案名稱=SA.spname,結帳方式=SA.x4name,說明=SA.samemo
                    ,客戶簡稱=cust.cuname1,客戶名稱=cust.cuper1,客戶電話=cust.cutel1,客戶傳真=cust.cufax1,客戶類別=cust.cux1no
                    ,客戶類別名稱=X1.x1name
                    ,輔助編號=item.itnoudf,產品類別=item.kino
                    ,類別名稱=KD.kiname
                    ,flag=0
                    from rsaled rsaled
                    left join rsale SA on rsaled.sano = SA.sano
                    left join cust on rsaled.cuno = cust.cuno
                    left join xx01 X1 on cust.cux1no = X1.x1no
                    left join item item on rsaled.itno = item.itno
                    left join kind KD on item.kino = KD.kino
                    where 0=0 " + rQuery;
                    if (rsale)
                        da.Fill(saleSandR);

                    cmd.CommandText = "Select 單據='',bomid='',成本=0.0,毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    if (this.各倉成本 == false)
                    {
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
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 單據='銷貨',saled.bomid,substring(sadate,1,5)月份,saled.itno,saled.stno,mny=ISNULL(mnyb,0),母數量=saled.qty*saled.itpkgqty from saled " + sxx01 + skind + @"
                        where saled.ittrait <> 1 " + sQuery;
                        da.Fill(saled23);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 單據='銷退',rsaled.bomid,substring(sadate,1,5)月份,rsaled.itno,rsaled.stno,mny=(-1)*ISNULL(mnyb,0),母數量=(-1)*rsaled.qty*rsaled.itpkgqty from rsaled " + rxx01 + rkind + @"
                        where rsaled.ittrait <> 1 " + rQuery;
                        da.Fill(rsaled23);
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 成本=0.0,單據='銷貨',saled.bomid,substring(sadate,1,5)月份,saled.itno sitno,bom.itno,saled.stno,mny=ISNULL(mnyb,0),母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from saled " + sxx01 + skind + @"
                        left join salebom bom on bom.bomid = saled.bomid  
                        where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + sQuery;
                        da.Fill(saled1);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 成本=0.0,單據='銷退',rsaled.bomid,substring(sadate,1,5)月份,rsaled.itno sitno,bom.itno,rsaled.stno,mny=(-1)*ISNULL(mnyb,0),母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty*bom.itpkgqty/bom.itpareprs) from rsaled " + rxx01 + rkind + @"
                        left join rsalebom bom on bom.bomid = rsaled.bomid 
                        where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + rQuery;
                        da.Fill(rsaled1);
                    }
                }
                //單一組裝成本
                if (sale)
                {
                    if(this.各倉成本 == false)
                        AvgCost(saled23, itemcost, ref dtSource);
                    else
                        AvgCostByOneStock(saled23, itemcost, dtSource);
                }
                if (rsale)
                {
                    if (this.各倉成本 == false)
                        AvgCost(rsaled23, itemcost, ref dtSource);
                    else
                        AvgCostByOneStock(rsaled23, itemcost, dtSource);
                }

                //組合品成本
                if (sale)
                {
                    if (this.各倉成本 == false)
                        BomAvgCost(saled1, itemcost, ref dtSource);
                    else
                        BomAvgCostByOneStock(saled1, itemcost, dtSource);
                }
                if (rsale)
                {
                    if (this.各倉成本 == false)
                        BomAvgCost(rsaled1, itemcost, ref dtSource);
                    else
                        BomAvgCostByOneStock(rsaled1, itemcost, dtSource);
                }

                //結果: 計算毛利
                dtResult = null;
                if (dtSource.Rows.Count == 0)
                    return;

                var tTemp = saleSandR.Clone();
                tTemp.Columns.Add("成本", typeof(Decimal));
                tTemp.Columns.Add("毛利", typeof(Decimal));
                tTemp.Columns.Add("毛利率", typeof(Decimal));

                saleSandR.AsEnumerable()
                    .GroupJoin(
                        dtSource.AsEnumerable(),
                        sr => new { 單據 = sr["單據"].ToString(), bomid = sr["bomid"].ToString() },
                        s => new { 單據 = s["單據"].ToString(), bomid = s["bomid"].ToString() },
                        (sr, s) => new
                        {
                            obj = sr.ItemArray,
                            成本 = s.First()["成本"],
                            毛利 = s.First()["毛利"],
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
                        cq.Enqueue(srs.成本);
                        cq.Enqueue(srs.毛利);
                        cq.Enqueue(srs.毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                if (tTemp.Rows.Count > 0)
                    dtResult = tTemp.AsEnumerable().OrderBy(r => r["產品類別"].ToString()).ThenBy(r => r["產品編號"].ToString()).CopyToDataTable();

                dtSource.Clear();
                itemcost.Clear();
                saled1.Clear();
                saled23.Clear();
                rsaled1.Clear();
                rsaled23.Clear();
                saleSandR.Clear();
                tTemp.Clear();
            }

            void AvgCostByOneStock(DataTable tSale, DataTable itemCost, DataTable dtSource)
            {
                if (tSale.Rows.Count == 0)
                    return;

                tSale.AsEnumerable().GroupBy(r => r["itno"].ToString().Trim()).AsParallel().ForAll(r =>
                {
                    DataTable temp = new DataTable();
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.AddWithValue("itno", r.Key);
                        cmd.CommandText = @"Select itno,stno,
                                        avgcost01,avgcost02,avgcost03,avgcost04,avgcost05,avgcost06,avgcost07,avgcost08,avgcost09,avgcost10,avgcost11,avgcost12,
                                        avgcost13,avgcost14,avgcost15,avgcost16,avgcost17,avgcost18,avgcost19,avgcost20,avgcost21,avgcost22,avgcost23,avgcost24 
                                        from stkcost where itno=@itno";
                        dd.Fill(temp);
                    }

                    foreach (DataRow item in r)
                    {
                        var cost = temp.AsEnumerable().FirstOrDefault(s => s["stno"].ToString().Trim() == item["stno"].ToString().Trim());

                        string 查詢月份 = "";
                        if (item["月份"].ToString().Substring(0, 3).ToInteger() == Common.Sys_StkYear1)
                            查詢月份 = item["月份"].ToString().Substring(3, 2);
                        else
                            查詢月份 = (item["月份"].ToString().Substring(3, 2).ToInteger() + 12).ToString().PadLeft(2, '0');

                        decimal 銷貨成本 = cost == null ? 0M : item["母數量"].ToDecimal() * cost["avgcost" + 查詢月份].ToDecimal();
                        decimal 銷貨淨額 = item["mny"].ToDecimal();
                        decimal 銷貨毛利 = 銷貨淨額 - 銷貨成本;
                        decimal 毛利率 = 0M;
                        if (銷貨淨額 != 0)
                            毛利率 = (銷貨毛利 / 銷貨淨額) * 100;
                        object[] addrow;
                        addrow = new object[]{
                                item["單據"],
                                item["bomid"],
                                銷貨成本,
                                銷貨毛利,
                                毛利率,
                            };

                        lock (dtSource.Rows.SyncRoot)
                            dtSource.Rows.Add(addrow);
                    }
                    temp.Clear();
                });
            }
            void BomAvgCostByOneStock(DataTable tBom, DataTable itemCost, DataTable dtSource)
            {
                if (tBom.Rows.Count == 0)
                    return;

                DataTable BomTemp = new DataTable();
                BomTemp = tBom.Clone();

                tBom.AsEnumerable().GroupBy(r => r["itno"].ToString().Trim()).AsParallel().ForAll(r =>
                {
                    DataTable temp = new DataTable();
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.AddWithValue("itno", r.Key);
                        cmd.CommandText = @"Select itno,stno,
                                        avgcost01,avgcost02,avgcost03,avgcost04,avgcost05,avgcost06,avgcost07,avgcost08,avgcost09,avgcost10,avgcost11,avgcost12,
                                        avgcost13,avgcost14,avgcost15,avgcost16,avgcost17,avgcost18,avgcost19,avgcost20,avgcost21,avgcost22,avgcost23,avgcost24 
                                        from stkcost where itno=@itno";
                        dd.Fill(temp);
                    }

                    foreach (DataRow item in r)
                    {
                        var cost = temp.AsEnumerable().FirstOrDefault(s => s["stno"].ToString().Trim() == item["stno"].ToString().Trim());

                        string 查詢月份 = "";
                        if (item["月份"].ToString().Substring(0, 3).ToInteger() == Common.Sys_StkYear1)
                            查詢月份 = item["月份"].ToString().Substring(3, 2);
                        else
                            查詢月份 = (item["月份"].ToString().Substring(3, 2).ToInteger() + 12).ToString().PadLeft(2, '0');

                        decimal 銷貨成本 = cost == null ? 0M : item["子數量"].ToDecimal() * cost["avgcost" + 查詢月份].ToDecimal();

                        DataRow row = BomTemp.NewRow();
                        row["母數量"] = item["母數量"];
                        row["單據"] = item["單據"];
                        row["bomid"] = item["bomid"];
                        row["mny"] = item["mny"];
                        row["成本"] = 銷貨成本;

                        lock (BomTemp.Rows.SyncRoot)
                            BomTemp.Rows.Add(row);
                    }
                    temp.Clear();
                });

                BomTemp.AsEnumerable().GroupBy(r => r["bomid"].ToString()).AsParallel().ForAll(g =>
                {
                    var 銷貨淨額 = g.First()["mny"].ToDecimal();
                    var 銷貨成本 = g.First()["母數量"].ToDecimal() * g.Sum(gw => gw["成本"].ToDecimal());
                    var 銷貨毛利 = 0M;
                    var 毛利率 = 0M;

                    銷貨毛利 = 銷貨淨額 - 銷貨成本;
                    if (銷貨淨額 != 0)
                        毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

                    lock (dtSource.Rows.SyncRoot)
                    {
                        dtSource.Rows.Add(new object[]{
                            g.First()["單據"],
                            g.First()["bomid"],
                            銷貨成本,
                            銷貨毛利,
                            毛利率
                        });
                    }
                });
                BomTemp.Clear();
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
                var 成本 = 0M;
                var 毛利 = 0M;
                var 毛利率 = 0M;

                var row = cost.FirstOrDefault();
                if (row != null)
                    成本 = sale["母數量"].ToDecimal() * row["itcost"].ToDecimal();

                毛利 = 銷貨淨額 - 成本;
                if (銷貨淨額 != 0)
                    毛利率 = (毛利 / 銷貨淨額) * 100;

                return new object[] { sale["單據"], sale["bomid"], 成本, 毛利, 毛利率 };
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
                        成本 = BomJoinAvg(bc.bom, bc.cost)
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
                var 成本 = g.First().母數量.ToDecimal() * g.Sum(gw => gw.成本);
                var 毛利 = 0M;
                var 毛利率 = 0M;

                毛利 = 銷貨淨額 - 成本;
                if (銷貨淨額 != 0)
                    毛利率 = (毛利 / 銷貨淨額) * 100;

                return new object[] { g.First().單據, g.First().bomid, 成本, 毛利, 毛利率 };
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
                    this.sParaList(cmd.Parameters);

                    string skind = "", rkind = "", sxx01 = "", rxx01 = "";
                    if (ifkind) skind = @"
                        left join item on saled.itno = item.itno
                        left join kind on item.kino = kind.kino ";
                    else
                        skind = " left join item on saled.itno = item.itno ";

                    if (ifkind) rkind = @"
                        left join item on rsaled.itno = item.itno
                        left join kind on item.kino = kind.kino ";
                    else
                        rkind = " left join item on rsaled.itno = item.itno ";

                    if (ifxx01) sxx01 = @"
                        left join cust on saled.cuno = cust.cuno
                        left join xx01 on cust.cux1no = xx01.x1no ";

                    if (ifxx01) rxx01 = @"
                        left join cust on rsaled.cuno = cust.cuno
                        left join xx01 on cust.cux1no = xx01.x1no ";

                    cmd.CommandText = @"
                    Select 
                    單據='銷貨',憑證=saled.sano,單據日期=saled.sadate,訂單憑證=saled.orno,客戶編號=saled.cuno,業務編號=saled.emno,專案編號=saled.spno,送貨編號=saled.seno,倉庫名稱=saled.stname,saled.bomid,saled.ittrait
                    ,產品編號=saled.itno,品名規格=saled.itname,數量=saled.qty,單位=saled.itunit,售價=saled.priceb,折數=saled.prs,稅前售價=saled.taxpriceb,稅前金額=saled.mnyb,包裝數量=saled.itpkgqty,說明1=saled.memo
                    ,業務人員=SA.emname,送貨方式=SA.sename,專案名稱=SA.spname,結帳方式=SA.x4name,說明=SA.samemo
                    ,客戶簡稱=cust.cuname1,客戶名稱=cust.cuper1,客戶電話=cust.cutel1,客戶傳真=cust.cufax1,客戶類別=cust.cux1no
                    ,客戶類別名稱=X1.x1name
                    ,輔助編號=item.itnoudf,產品類別=item.kino
                    ,類別名稱=KD.kiname
                    ,flag=0
                    from saled 
                    left join sale SA on saled.sano = SA.sano
                    left join cust on saled.cuno = cust.cuno
                    left join xx01 X1 on cust.cux1no = X1.x1no
                    left join item item on saled.itno = item.itno
                    left join kind KD on item.kino = KD.kino
                    where 0=0 " + sQuery;
                    if (sale)
                        da.Fill(saleSandR);

                    cmd.CommandText = @"
                    Select
                    單據='銷退',憑證=rsaled.sano,單據日期=rsaled.sadate,訂單憑證=rsaled.orno,客戶編號=rsaled.cuno,業務編號=rsaled.emno,專案編號=rsaled.spno,送貨編號=rsaled.seno,倉庫名稱=rsaled.stname,rsaled.bomid,rsaled.ittrait
                    ,產品編號=rsaled.itno,品名規格=rsaled.itname,數量=(-1)*rsaled.qty,單位=rsaled.itunit,售價=rsaled.priceb,折數=rsaled.prs,稅前售價=rsaled.taxpriceb,稅前金額=(-1)*rsaled.mnyb,包裝數量=rsaled.itpkgqty,說明1=rsaled.memo
                    ,業務人員=SA.emname,送貨方式=SA.sename,專案名稱=SA.spname,結帳方式=SA.x4name,說明=SA.samemo
                    ,客戶簡稱=cust.cuname1,客戶名稱=cust.cuper1,客戶電話=cust.cutel1,客戶傳真=cust.cufax1,客戶類別=cust.cux1no
                    ,客戶類別名稱=X1.x1name
                    ,輔助編號=item.itnoudf,產品類別=item.kino
                    ,類別名稱=KD.kiname
                    ,flag=0
                    from rsaled rsaled
                    left join rsale SA on rsaled.sano = SA.sano
                    left join cust on rsaled.cuno = cust.cuno
                    left join xx01 X1 on cust.cux1no = X1.x1no
                    left join item item on rsaled.itno = item.itno
                    left join kind KD on item.kino = KD.kino
                    where 0=0 " + rQuery;
                    if (rsale)
                        da.Fill(saleSandR);

                    cmd.CommandText = "Select 單據='',bomid='',成本=0.0,毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = @"Select itno,bsdate,itunit,itpkgqty,realcost from bshopd Where bsdate >=@day0 and bsdate<=@day1 order by itno asc,bsdate desc,bsno desc";
                    da.Fill(itemcost);

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select  單據='銷貨',saled.bomid,saled.sadate,saled.itno,mny=ISNULL(mnyb,0),母數量=saled.qty,saled.itunit,saled.itpkgqty,item.itbuypri
                        From saled " + sxx01 + skind + @"  
                        where saled.ittrait <> 1 " + sQuery;
                        da.Fill(saled23);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select  單據='銷退',rsaled.bomid,rsaled.sadate,rsaled.itno,mny=(-1)*ISNULL(mnyb,0),母數量=(-1)*rsaled.qty,rsaled.itunit,rsaled.itpkgqty,item.itbuypri
                        From rsaled " + rxx01 + rkind + @" 
                        where rsaled.ittrait <> 1 " + rQuery;
                        da.Fill(rsaled23);
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 成本=0.0,單據='銷貨',saled.bomid,saled.sadate,saled.itno sitno,bom.itno,mny=ISNULL(mnyb,0),母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
                        From saled " + sxx01 + skind + @" 
                        Left join salebom bom on bom.bomid = saled.bomid 
                        Where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + sQuery;
                        da.Fill(saled1);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 成本=0.0,單據='銷退',rsaled.bomid,rsaled.sadate,rsaled.itno sitno,bom.itno,mny=(-1)*ISNULL(mnyb,0),母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty,bom.itunit,item.itbuypri
                        From rsaled " + rxx01 + rkind + @" 
                        Left join rsalebom bom on bom.bomid = rsaled.bomid 
                        Where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + rQuery;
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
                dtResult = null;
                if (dtSource.Rows.Count == 0)
                    return;

                var tTemp = saleSandR.Clone();
                tTemp.Columns.Add("成本", typeof(Decimal));
                tTemp.Columns.Add("毛利", typeof(Decimal));
                tTemp.Columns.Add("毛利率", typeof(Decimal));

                saleSandR.AsEnumerable()
                    .GroupJoin(
                        dtSource.AsEnumerable(),
                        sr => new { 單據 = sr["單據"].ToString(), bomid = sr["bomid"].ToString() },
                        s => new { 單據 = s["單據"].ToString(), bomid = s["bomid"].ToString() },
                        (sr, s) => new
                        {
                            obj = sr.ItemArray,
                            成本 = s.First()["成本"],
                            毛利 = s.First()["毛利"],
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
                        cq.Enqueue(srs.成本);
                        cq.Enqueue(srs.毛利);
                        cq.Enqueue(srs.毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                if (tTemp.Rows.Count > 0)
                    dtResult = tTemp.AsEnumerable().OrderBy(r => r["產品類別"].ToString()).ThenBy(r => r["產品編號"].ToString()).CopyToDataTable();

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
                            var 成本 = r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * r["itbuypri"].ToDecimal();
                            var 毛利 = 0M;
                            var 毛利率 = 0M;

                            毛利 = 銷貨淨額 - 成本;
                            if (銷貨淨額 != 0)
                                毛利率 = (毛利 / 銷貨淨額) * 100;

                            ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                            cq.Enqueue(r["單據"]);
                            cq.Enqueue(r["bomid"]);
                            cq.Enqueue(成本);
                            cq.Enqueue(毛利);
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
                                var 成本 = r["母數量"].ToDecimal() * row["realcost"].ToDecimal();
                                var 毛利 = 0M;
                                var 毛利率 = 0M;

                                毛利 = 銷貨淨額 - 成本;
                                if (銷貨淨額 != 0)
                                    毛利率 = (毛利 / 銷貨淨額) * 100;

                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["單據"]);
                                cq.Enqueue(r["bomid"]);
                                cq.Enqueue(成本);
                                cq.Enqueue(毛利);
                                cq.Enqueue(毛利率);

                                lock (dtSource.Rows.SyncRoot)
                                {
                                    dtSource.Rows.Add(cq.ToArray());
                                }
                            }
                            else
                            {
                                var 銷貨淨額 = r["mny"].ToDecimal();
                                var 成本 = 0M;
                                var 毛利 = 0M;
                                var 毛利率 = 0M;

                                if (bshoppkgqty != 0)
                                    成本 = r["母數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * (row["realcost"].ToDecimal() / bshoppkgqty);

                                毛利 = 銷貨淨額 - 成本;
                                if (銷貨淨額 != 0)
                                    毛利率 = (毛利 / 銷貨淨額) * 100;

                                ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                                cq.Enqueue(r["單據"]);
                                cq.Enqueue(r["bomid"]);
                                cq.Enqueue(成本);
                                cq.Enqueue(毛利);
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
                        r["成本"] = r["子數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * r["itbuypri"].ToDecimal();
                    }
                    else
                    {
                        var saleunit = r["itunit"].ToString();
                        var salepkgqty = r["itpkgqty"].ToDecimal();

                        var bshopunit = row["itunit"].ToString();
                        var bshoppkgqty = row["itpkgqty"].ToDecimal();

                        if (saleunit == bshopunit && salepkgqty == bshoppkgqty)
                        {
                            r["成本"] = r["子數量"].ToDecimal() * row["realcost"].ToDecimal();
                        }
                        else
                        {
                            var cost = 0M;
                            if (bshoppkgqty != 0)
                                cost = r["子數量"].ToDecimal() * r["itpkgqty"].ToDecimal() * (row["realcost"].ToDecimal() / bshoppkgqty);

                            r["成本"] = cost;
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
                var 成本 = g.First()["母數量"].ToDecimal() * g.Sum(gw => gw["成本"].ToDecimal());
                var 毛利 = 0M;
                var 毛利率 = 0M;

                毛利 = 銷貨淨額 - 成本;
                if (銷貨淨額 != 0)
                    毛利率 = (毛利 / 銷貨淨額) * 100;

                return new object[] { g.First()["單據"], g.First()["bomid"], 成本, 毛利, 毛利率 };
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
                    this.sParaList(cmd.Parameters);

                    string skind = "", rkind = "", sxx01 = "", rxx01 = "";
                    if (ifkind) skind = @"
                        left join item on saled.itno = item.itno
                        left join kind on item.kino = kind.kino ";

                    if (ifkind) rkind = @"
                        left join item on rsaled.itno = item.itno
                        left join kind on item.kino = kind.kino ";

                    if (ifxx01) sxx01 = @"
                        left join cust on saled.cuno = cust.cuno
                        left join xx01 on cust.cux1no = xx01.x1no ";

                    if (ifxx01) rxx01 = @"
                        left join cust on rsaled.cuno = cust.cuno
                        left join xx01 on cust.cux1no = xx01.x1no ";

                    cmd.CommandText = @"
                    Select 
                    單據='銷貨',憑證=saled.sano,單據日期=saled.sadate,訂單憑證=saled.orno,客戶編號=saled.cuno,業務編號=saled.emno,專案編號=saled.spno,送貨編號=saled.seno,倉庫名稱=saled.stname,saled.bomid,saled.ittrait
                    ,產品編號=saled.itno,品名規格=saled.itname,數量=saled.qty,單位=saled.itunit,售價=saled.priceb,折數=saled.prs,稅前售價=saled.taxpriceb,稅前金額=saled.mnyb,包裝數量=saled.itpkgqty,說明1=saled.memo
                    ,業務人員=SA.emname,送貨方式=SA.sename,專案名稱=SA.spname,結帳方式=SA.x4name,說明=SA.samemo
                    ,客戶簡稱=cust.cuname1,客戶名稱=cust.cuper1,客戶電話=cust.cutel1,客戶傳真=cust.cufax1,客戶類別=cust.cux1no
                    ,客戶類別名稱=X1.x1name
                    ,輔助編號=item.itnoudf,產品類別=item.kino
                    ,類別名稱=KD.kiname
                    ,flag=0
                    from saled 
                    left join sale SA on saled.sano = SA.sano
                    left join cust cust on saled.cuno = cust.cuno
                    left join xx01 X1 on cust.cux1no = X1.x1no
                    left join item item on saled.itno = item.itno
                    left join kind KD on item.kino = KD.kino
                    where 0=0 " + sQuery;
                    if (sale)
                        da.Fill(saleSandR);

                    cmd.CommandText = @"
                    Select
                    單據='銷退',憑證=rsaled.sano,單據日期=rsaled.sadate,訂單憑證=rsaled.orno,客戶編號=rsaled.cuno,業務編號=rsaled.emno,專案編號=rsaled.spno,送貨編號=rsaled.seno,倉庫名稱=rsaled.stname,rsaled.bomid,rsaled.ittrait
                    ,產品編號=rsaled.itno,品名規格=rsaled.itname,數量=(-1)*rsaled.qty,單位=rsaled.itunit,售價=rsaled.priceb,折數=rsaled.prs,稅前售價=rsaled.taxpriceb,稅前金額=(-1)*rsaled.mnyb,包裝數量=rsaled.itpkgqty,說明1=rsaled.memo
                    ,業務人員=SA.emname,送貨方式=SA.sename,專案名稱=SA.spname,結帳方式=SA.x4name,說明=SA.samemo
                    ,客戶簡稱=cust.cuname1,客戶名稱=cust.cuper1,客戶電話=cust.cutel1,客戶傳真=cust.cufax1,客戶類別=cust.cux1no
                    ,客戶類別名稱=X1.x1name
                    ,輔助編號=item.itnoudf,產品類別=item.kino
                    ,類別名稱=KD.kiname
                    ,flag=0
                    from rsaled rsaled
                    left join rsale SA on rsaled.sano = SA.sano
                    left join cust cust on rsaled.cuno = cust.cuno
                    left join xx01 X1 on cust.cux1no = X1.x1no
                    left join item item on rsaled.itno = item.itno
                    left join kind KD on item.kino = KD.kino
                    where 0=0 " + rQuery;
                    if (rsale)
                        da.Fill(saleSandR);

                    cmd.CommandText = "Select 單據='',bomid='',成本=0.0,毛利=0.0,毛利率=0.0 from saled where 1=0 ";
                    da.Fill(dtSource);

                    cmd.CommandText = @"Select itno,itpkgqty,itcost,itcostp from item";
                    da.Fill(itemcost);

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 單據='銷貨',saled.bomid,saled.itno,mny=ISNULL(mnyb,0),母數量=saled.qty,saled.itpkgqty from saled " + sxx01 + skind + @"
                        where saled.ittrait <> 1 " + sQuery;
                        da.Fill(saled23);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 單據='銷退',rsaled.bomid,rsaled.itno,mny=(-1)*ISNULL(mnyb,0),母數量=(-1)*rsaled.qty,rsaled.itpkgqty from rsaled " + rxx01 + rkind + @"
                        where rsaled.ittrait <> 1 " + rQuery;
                        da.Fill(rsaled23);
                    }

                    if (sale)
                    {
                        cmd.CommandText = @"
                        Select 成本=0.0,單據='銷貨',saled.bomid,saled.itno sitno,bom.itno,mny=ISNULL(mnyb,0),母數量=saled.qty*saled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from saled  " + sxx01 + skind + @"
                        left join salebom bom on bom.bomid = saled.bomid 
                        where saled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + sQuery;
                        da.Fill(saled1);
                    }

                    if (rsale)
                    {
                        cmd.CommandText = @"
                        Select 成本=0.0,單據='銷退',rsaled.bomid,rsaled.itno sitno,bom.itno,mny=(-1)*ISNULL(mnyb,0),母數量=(-1)*rsaled.qty*rsaled.itpkgqty,子數量=(bom.itqty/bom.itpareprs),bom.itpkgqty from rsaled " + rxx01 + rkind + @"
                        left join rsalebom bom on bom.bomid = rsaled.bomid 
                        where rsaled.ittrait = 1 and (bom.itpareprs <> 0 or bom.itpareprs is null) " + rQuery;
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
                dtResult = null;
                if (dtSource.Rows.Count == 0)
                    return;

                var tTemp = saleSandR.Clone();
                tTemp.Columns.Add("成本", typeof(Decimal));
                tTemp.Columns.Add("毛利", typeof(Decimal));
                tTemp.Columns.Add("毛利率", typeof(Decimal));

                saleSandR.AsEnumerable()
                    .GroupJoin(
                        dtSource.AsEnumerable(),
                        sr => new { 單據 = sr["單據"].ToString(), bomid = sr["bomid"].ToString() },
                        s => new { 單據 = s["單據"].ToString(), bomid = s["bomid"].ToString() },
                        (sr, s) => new
                        {
                            obj = sr.ItemArray,
                            成本 = s.First()["成本"],
                            毛利 = s.First()["毛利"],
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
                        cq.Enqueue(srs.成本);
                        cq.Enqueue(srs.毛利);
                        cq.Enqueue(srs.毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(cq.ToArray());
                        }
                    });

                if (tTemp.Rows.Count > 0)
                    dtResult = tTemp.AsEnumerable().OrderBy(r => r["產品類別"].ToString()).ThenBy(r => r["產品編號"].ToString()).CopyToDataTable();

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
                var 成本 = 0M;
                var 毛利 = 0M;
                var 毛利率 = 0M;

                var row = cost.FirstOrDefault();
                if (row != null)
                {
                    if (sale["itpkgqty"].ToDecimal() == row["itpkgqty"].ToDecimal())
                        成本 = sale["母數量"].ToDecimal() * row["itcostp"].ToDecimal();
                    else
                        成本 = sale["母數量"].ToDecimal() * sale["itpkgqty"].ToDecimal() * row["itcost"].ToDecimal();
                }

                毛利 = 銷貨淨額 - 成本;
                if (銷貨淨額 != 0)
                    毛利率 = (毛利 / 銷貨淨額) * 100;

                return new object[] { sale["單據"], sale["bomid"], 成本, 毛利, 毛利率 };
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
                        成本 = BomItemCostOrCostP(bc.bom, bc.cost)
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
                var 成本 = g.First().母數量.ToDecimal() * g.Sum(gw => gw.成本);
                var 毛利 = 0M;
                var 毛利率 = 0M;

                毛利 = 銷貨淨額 - 成本;
                if (銷貨淨額 != 0)
                    毛利率 = (毛利 / 銷貨淨額) * 100;

                return new object[] { g.First().單據, g.First().bomid, 成本, 毛利, 毛利率 };
            }

            public void 建檔(out DataTable dtResult)
            {
                DataTable items = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    this.sParaList(cmd.Parameters);

                    string skind = "", rkind = "";
                    if (ifkind) skind = @"
                        left join sale SA on saled.sano = SA.sano
                        left join cust on saled.cuno = cust.cuno
                        left join xx01 X1 on cust.cux1no = X1.x1no
                        left join item item on saled.itno = item.itno
                        left join kind KD on item.kino = KD.kino ";

                    if (ifkind) rkind = @"
                        left join rsale SA on rsaled.sano = SA.sano
                        left join cust on rsaled.cuno = cust.cuno
                        left join xx01 X1 on cust.cux1no = X1.x1no
                        left join item item on rsaled.itno = item.itno
                        left join kind KD on item.kino = KD.kino ";

                    dtResult = new DataTable();
                    cmd.CommandText = @"
                    Select 
                    單據='銷貨',憑證=saled.sano,單據日期=saled.sadate,訂單憑證=saled.orno,客戶編號=saled.cuno,業務編號=saled.emno,專案編號=saled.spno,送貨編號=saled.seno,倉庫名稱=saled.stname,saled.bomid,saled.ittrait
                    ,產品編號=saled.itno,品名規格=saled.itname,數量=saled.qty,單位=saled.itunit,售價=saled.priceb,折數=saled.prs,稅前售價=saled.taxpriceb,稅前金額=saled.mnyb,包裝數量=saled.itpkgqty,說明1=saled.memo
                    ,業務人員=SA.emname,送貨方式=SA.sename,專案名稱=SA.spname,結帳方式=SA.x4name,說明=SA.samemo
                    ,客戶簡稱=cust.cuname1,客戶名稱=cust.cuper1,客戶電話=cust.cutel1,客戶傳真=cust.cufax1,客戶類別=cust.cux1no
                    ,客戶類別名稱=X1.x1name
                    ,輔助編號=item.itnoudf,產品類別=item.kino
                    ,類別名稱=KD.kiname
                    ,flag=0,成本=0.0,毛利=0.0,毛利率=0.0
                    from saled 
                    left join sale SA on saled.sano = SA.sano
                    left join cust on saled.cuno = cust.cuno
                    left join xx01 X1 on cust.cux1no = X1.x1no
                    left join item item on saled.itno = item.itno
                    left join kind KD on item.kino = KD.kino
                    where 1=0 ";
                    da.Fill(dtResult);

                    cmd.CommandText = @"
                    Select A.itno,ItCostSlt from 
                    (
                        Select saled.itno from saled " + skind + @" where 0=0 " + sQuery + @"
                        union all 
                        Select rsaled.itno from rsaled " + rkind + @" where 0=0 " + rQuery + @"
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
                            var rows = t1.AsEnumerable().Where(rw => rw["產品編號"].ToString().Trim() == itno);
                            foreach (var row in rows)
                                stack.Push(row);
                        }
                        else if (slt == "2")
                        {
                            var rows = t2.AsEnumerable().Where(rw => rw["產品編號"].ToString().Trim() == itno);
                            foreach (var row in rows)
                                stack.Push(row);
                        }
                        else if (slt == "3")
                        {
                            var rows = t3.AsEnumerable().Where(rw => rw["產品編號"].ToString().Trim() == itno);
                            foreach (var row in rows)
                                stack.Push(row);
                        }
                    });

                //結果
                if (stack.Count > 0)
                    dtResult = stack.OrderBy(r => r["產品類別"].ToString()).ThenBy(r => r["產品編號"].ToString()).CopyToDataTable();

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
                public decimal 成本 { get; set; }
            }
        }

        JBS.JS.xEvents xe;

        public FrmKind_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmKind_Rpt_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                date.MaxLength = date1.MaxLength = 7;
                date.Text = Date.GetDateTime(1).Substring(0, 5) + "01";
                date1.Text = Date.GetDateTime(1);
            }
            else
            {
                date.MaxLength = date1.MaxLength = 8;
                date.Text = Date.GetDateTime(2).Substring(0, 6) + "01";
                date1.Text = Date.GetDateTime(2);
            }

            date.Focus();
        }

        bool Compare(TextBox tx, TextBox tx1)
        {
            if (tx.Text != "" && tx1.Text != "")
            {
                if (string.CompareOrdinal(tx.Text, tx1.Text) > 0)
                {
                    MessageBox.Show("起始" + tx.Tag + "不可大於終止" + tx1.Tag + "，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx.Focus();
                    return false;
                }
            }
            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (!Compare(date, date1)) return;
            if (!Compare(KiNo, KiNo1)) return;
            if (!Compare(CuX1No, CuX1No1)) return;
            if (!Compare(CuNo, CuNo1)) return;
            if (!Compare(EmNo, EmNo1)) return;
            if (!Compare(ItNo, ItNo1)) return;

            if (ckSale.Checked == false && ckRsale.Checked == false)
            {
                MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Action<SqlParameterCollection> sqlPara = (sp) =>
            {
                sp.AddWithValue("day0", Date.ToTWDate(Common.Sys_StkYear1 - 1 + "0101"));

                if (date.TrimTextLenth() > 0) sp.AddWithValue("day", Date.ToTWDate(date.Text));
                if (CuNo.TrimTextLenth() > 0) sp.AddWithValue("cuno", CuNo.Text);
                if (EmNo.TrimTextLenth() > 0) sp.AddWithValue("emno", EmNo.Text);
                if (ItNo.TrimTextLenth() > 0) sp.AddWithValue("itno", ItNo.Text);
                if (KiNo.TrimTextLenth() > 0) sp.AddWithValue("kino", KiNo.Text);
                if (CuX1No.TrimTextLenth() > 0) sp.AddWithValue("cux1no", CuX1No.Text);
                if (Memo.TrimTextLenth() > 0) sp.AddWithValue("memo", Memo.Text);

                if (date1.TrimTextLenth() > 0) sp.AddWithValue("day1", Date.ToTWDate(date1.Text));
                if (CuNo1.TrimTextLenth() > 0) sp.AddWithValue("cuno1", CuNo1.Text);
                if (EmNo1.TrimTextLenth() > 0) sp.AddWithValue("emno1", EmNo1.Text);
                if (ItNo1.TrimTextLenth() > 0) sp.AddWithValue("itno1", ItNo1.Text);
                if (KiNo1.TrimTextLenth() > 0) sp.AddWithValue("kino1", KiNo1.Text);
                if (CuX1No1.TrimTextLenth() > 0) sp.AddWithValue("cux1no1", CuX1No1.Text);
            };

            #region -sale
            StringBuilder sb = new StringBuilder();
            if (date.TrimTextLenth() > 0) sb.Append("  And saled.sadate >= @day");
            if (date1.TrimTextLenth() > 0) sb.Append(" And saled.sadate <= @day1");

            if (CuNo.TrimTextLenth() > 0) sb.Append("  And saled.cuno >= @cuno");
            if (CuNo1.TrimTextLenth() > 0) sb.Append(" And saled.cuno <= @cuno1");

            if (EmNo.TrimTextLenth() > 0) sb.Append("  And saled.emno >= @emno");
            if (EmNo1.TrimTextLenth() > 0) sb.Append(" And saled.emno <= @emno1");

            if (ItNo.TrimTextLenth() > 0) sb.Append("  And saled.itno >= @itno");
            if (ItNo1.TrimTextLenth() > 0) sb.Append(" And saled.itno <= @itno1");

            if (KiNo.TrimTextLenth() > 0) sb.Append("  And item.kino >= @kino");
            if (KiNo1.TrimTextLenth() > 0) sb.Append(" And item.kino <= @kino1");

            if (CuX1No.TrimTextLenth() > 0) sb.Append("  And cust.cux1no >= @cux1no");
            if (CuX1No1.TrimTextLenth() > 0) sb.Append(" And cust.cux1no <= @cux1no1");

            if (Memo.TrimTextLenth() > 0) sb.Append(" And saled.memo = @memo");
            string sQuery = sb.ToString();
            #endregion

            #region -rsale
            StringBuilder rb = new StringBuilder();
            if (date.TrimTextLenth() > 0) rb.Append("  And rsaled.sadate >= @day");
            if (date1.TrimTextLenth() > 0) rb.Append(" And rsaled.sadate <= @day1");

            if (CuNo.TrimTextLenth() > 0) rb.Append("  And rsaled.cuno >= @cuno");
            if (CuNo1.TrimTextLenth() > 0) rb.Append(" And rsaled.cuno <= @cuno1");

            if (EmNo.TrimTextLenth() > 0) rb.Append("  And rsaled.emno >= @emno");
            if (EmNo1.TrimTextLenth() > 0) rb.Append(" And rsaled.emno <= @emno1");

            if (ItNo.TrimTextLenth() > 0) rb.Append("  And rsaled.itno >= @itno");
            if (ItNo1.TrimTextLenth() > 0) rb.Append(" And rsaled.itno <= @itno1");

            if (KiNo.TrimTextLenth() > 0) rb.Append("  And item.kino >= @kino");
            if (KiNo1.TrimTextLenth() > 0) rb.Append(" And item.kino <= @kino1");

            if (CuX1No.TrimTextLenth() > 0) rb.Append("  And cust.cux1no >= @cux1no");
            if (CuX1No1.TrimTextLenth() > 0) rb.Append(" And cust.cux1no <= @cux1no1");

            if (Memo.TrimTextLenth() > 0) rb.Append(" And rsaled.memo = @memo");
            string rQuery = rb.ToString();
            #endregion


            var ifkind = false;
            if (KiNo.TrimTextLenth() > 0 || KiNo1.TrimTextLenth() > 0)
                ifkind = true;

            var ifxx01 = false;
            if (CuX1No.TrimTextLenth() > 0 || CuX1No1.TrimTextLenth() > 0)
                ifxx01 = true;

            DataTable dtResult = new DataTable();

            var out1 = Date.ToTWDate(date.Text).takeString(3).ToInteger() < Common.Sys_StkYear1;
            var out2 = Date.ToTWDate(date1.Text).takeString(3).ToInteger() < Common.Sys_StkYear1;

            if (out1 && (out2 == false))
            {
                MessageBox.Show("查詢日期, 請勿橫跨庫存年度!");
                return;
            }

            if (out1 && out2)
            {
                rdCost.Checked = true;
                new 成本計算(ckSale.Checked, ckRsale.Checked, sqlPara, sQuery, rQuery, ifkind, ifxx01).標準成本(out dtResult);
            }
            else if (rdAvgByAllStk.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, sqlPara, sQuery, rQuery, ifkind, ifxx01, false).月平均成本(out dtResult);
            }
            else if (rdAvgByOneStk.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, sqlPara, sQuery, rQuery, ifkind, ifxx01, true).月平均成本(out dtResult);
            }
            else if (rdLast.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, sqlPara, sQuery, rQuery, ifkind, ifxx01).最後一次進貨成本(out dtResult);
            }
            else if (rdCost.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, sqlPara, sQuery, rQuery, ifkind, ifxx01).標準成本(out dtResult);
            }
            else if (rdItem.Checked)
            {
                new 成本計算(ckSale.Checked, ckRsale.Checked, sqlPara, sQuery, rQuery, ifkind, ifxx01).建檔(out dtResult);
            }

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            if (rdSimple.Checked)
            {
                //using (var frm = new FrmKind_Rptb())
                //{
                //    frm.table = dtResult;
                //    frm.DateRange = date.Text + "～" + date1.Text;
                //    frm.ShowDialog();

                //    dtResult.Clear();
                //}
                this.OpemInfoFrom<FrmKind_Rptb>(() =>
                            {
                                FrmKind_Rptb frm = new FrmKind_Rptb();
                                frm.table = dtResult.Copy();
                                frm.DateRange = date.Text + "～" + date1.Text;
                                frm.CheckAVG = (rdAvgByAllStk.Checked || rdAvgByOneStk.Checked);
                                return frm;
                            });
                dtResult.Clear();
            }
            else if (radioT1.Checked)
            {
                DataTable tTemp = new DataTable();
                tTemp.Columns.Add("產品類別", typeof(String));
                tTemp.Columns.Add("類別名稱", typeof(String));
                tTemp.Columns.Add("數量", typeof(Decimal));
                tTemp.Columns.Add("稅前金額", typeof(Decimal));
                tTemp.Columns.Add("成本", typeof(Decimal));
                tTemp.Columns.Add("毛利", typeof(Decimal));
                tTemp.Columns.Add("毛利率", typeof(Decimal));

                dtResult.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["產品類別"].ToString().Trim())
                    .ForAll(g =>
                    {
                        var 數量 = 0M;
                        var 稅前金額 = 0M;
                        var 成本 = 0M;
                        var 毛利 = 0M;
                        var 毛利率 = 0M;

                        foreach (var gw in g)
                        {
                            數量 += gw["數量"].ToDecimal();
                            稅前金額 += gw["稅前金額"].ToDecimal();
                            成本 += gw["成本"].ToDecimal();
                        }
                        毛利 = 稅前金額 - 成本;
                        if (稅前金額 != 0)
                            毛利率 = (毛利 / 稅前金額) * 100;

                        ConcurrentQueue<object> queue = new ConcurrentQueue<object>();
                        queue.Enqueue(g.Key);
                        queue.Enqueue(g.First()["類別名稱"]);
                        queue.Enqueue(數量);
                        queue.Enqueue(稅前金額);
                        queue.Enqueue(成本);
                        queue.Enqueue(毛利);
                        queue.Enqueue(毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(queue.ToArray());
                        }
                    });

                //using (FrmKind_Rptc frm = new FrmKind_Rptc())
                //{
                //    frm.flag = "類別排行榜";
                //    frm.table = tTemp.AsEnumerable().OrderByDescending(r => r["毛利"].ToDecimal()).CopyToDataTable();
                //    frm.DateRange = date.Text + "～" + date1.Text;
                //    frm.ShowDialog();

                //    dtResult.Clear();
                //    tTemp.Clear();
                //}
                this.OpemInfoFrom<FrmKind_Rptc>(() =>
                            {
                                FrmKind_Rptc frm = new FrmKind_Rptc();
                                frm.flag = "類別排行榜";
                                frm.table = tTemp.AsEnumerable().OrderByDescending(r => r["毛利"].ToDecimal()).CopyToDataTable();
                                frm.DateRange = date.Text + "～" + date1.Text;
                                return frm;
                            });
                dtResult.Clear();
                tTemp.Clear();
            }
            else if (radioT2.Checked)
            {
                DataTable tTemp = new DataTable();
                tTemp.Columns.Add("產品編號", typeof(String));
                tTemp.Columns.Add("品名規格", typeof(String));
                tTemp.Columns.Add("單位", typeof(String));
                tTemp.Columns.Add("數量", typeof(Decimal));
                tTemp.Columns.Add("稅前金額", typeof(Decimal));
                tTemp.Columns.Add("成本", typeof(Decimal));
                tTemp.Columns.Add("毛利", typeof(Decimal));
                tTemp.Columns.Add("毛利率", typeof(Decimal));

                dtResult.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => new
                    {
                        itno = r["產品編號"].ToString().Trim(),
                        unit = r["單位"].ToString().Trim()
                    })
                    .ForAll(g =>
                    {
                        var 數量 = 0M;
                        var 稅前金額 = 0M;
                        var 成本 = 0M;
                        var 毛利 = 0M;
                        var 毛利率 = 0M;

                        foreach (var gw in g)
                        {
                            數量 += gw["數量"].ToDecimal();
                            稅前金額 += gw["稅前金額"].ToDecimal();
                            成本 += gw["成本"].ToDecimal();
                        }
                        毛利 = 稅前金額 - 成本;
                        if (稅前金額 != 0)
                            毛利率 = (毛利 / 稅前金額) * 100;

                        ConcurrentQueue<object> queue = new ConcurrentQueue<object>();
                        queue.Enqueue(g.Key.itno);
                        queue.Enqueue(g.First()["品名規格"]);
                        queue.Enqueue(g.Key.unit);
                        queue.Enqueue(數量);
                        queue.Enqueue(稅前金額);
                        queue.Enqueue(成本);
                        queue.Enqueue(毛利);
                        queue.Enqueue(毛利率);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(queue.ToArray());
                        }
                    });

                //using (FrmKind_Rptc frm = new FrmKind_Rptc())
                //{
                //    frm.flag = "產品排行榜";
                //    frm.table = tTemp.AsEnumerable().OrderByDescending(r => r["毛利"].ToDecimal()).CopyToDataTable();
                //    frm.DateRange = date.Text + "～" + date1.Text;
                //    frm.ShowDialog();

                //    dtResult.Clear();
                //    tTemp.Clear();
                //}
                this.OpemInfoFrom<FrmKind_Rptc>(() =>
                            {
                                FrmKind_Rptc frm = new FrmKind_Rptc();
                                frm.flag = "產品排行榜";
                                frm.table = tTemp.AsEnumerable().OrderByDescending(r => r["毛利"].ToDecimal()).CopyToDataTable();
                                frm.DateRange = date.Text + "～" + date1.Text;
                                return frm;
                            });
                dtResult.Clear();
                tTemp.Clear();
            }
        }



        private void date_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void KiNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }

        private void KiNo1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Kind>(sender, e, row =>
            {
                ((TextBox)sender).Text = row["kino"].ToString();
            }, true);
        }

        private void CuX1No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX01>(sender);
        }

        private void CuX1No_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.XX01>(sender, e, row =>
            {
                ((TextBox)sender).Text = row["x1no"].ToString();
            }, true);
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Cust>(sender, e, row =>
            {
                ((TextBox)sender).Text = row["cuno"].ToString();
            }, true);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                ((TextBox)sender).Text = row["emno"].ToString();
            }, true);
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void ItNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                ((TextBox)sender).Text = row["itno"].ToString();
            }, true);
        }

        private void Memo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(Memo, 60);
        }
    }
}
