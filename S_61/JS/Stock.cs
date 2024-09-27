using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using S_61.Basic;
using System.Windows.Forms;

namespace JBS.JS
{
    public class Stock
    {
        class dtDStNo
        {
            public string dtD { get; set; }
            public string dtBom { get; set; }
            public string StNo { get; set; }
            public string day { get; set; }
            public string qty { get; set; }
        }

        int StartYear { get; set; }
        int EndYear { get; set; }

        public Stock(int sy, int ey)
        {
            this.StartYear = sy;
            this.EndYear = ey;
        }

        private string group(string stno, string dt, string ittrait, string day, string qty = "qty")
        {
            var str = " (select itno," + stno + ",SUM(" + qty + "*itpkgqty)數量 from " + dt
                + " where " + day + " >= @day and " + day + " <= @day1 and " + ittrait
                + " group by itno," + stno + ") ";
            return str;
        }

        private void NewMethod(SqlCommand cmd)
        {
            //期初                      //只保留有期初庫存的產品，其他重算
            cmd.CommandText = @"
                    Delete from stock where itqtyf = 0 or itqtyf is null ;
                    Update stock set itqty = itqtyf ;";
            cmd.ExecuteNonQuery();

            //入庫入料 stnoi            //入組裝 入單一
            cmd.CommandText = @"
                    update stock set itqty = ISNULL(itqty,0) + (dt.數量) 
                    from stock inner join " + group("stnoi", "garnerd", "ittrait <> 1", "gadate") + @" dt on (stock.itno = dt.itno and stock.stno = dt.stnoi) 
                    where LEN(dt.stnoi)>0;

                    insert into stock (stno,itno,itqty)
                    Select stnoi,itno,itqty from (
                        Select dt.stnoi,dt.itno,SUM((dt.qty*dt.itpkgqty))itqty from garnerd dt left join stock on dt.stnoi = stock.stno and dt.itno = stock.itno 
                        where gadate >= @day and gadate <= @day1 and LEN(dt.stnoi)>0 and ittrait <> 1 and stock.stno is null and stock.itno is null
                        group by dt.stnoi,dt.itno
                    )A ;";
            cmd.ExecuteNonQuery();

            //入庫發料 stnoo            //扣組裝子件 扣單一
            cmd.CommandText = @"
                    update stock set itqty = ISNULL(itqty,0) - (dt.數量) 
                    from stock inner join " + group("stnoo", "garnerd", "ittrait = 3", "gadate") + @" dt on (stock.itno = dt.itno and stock.stno = dt.stnoo) 
                    where LEN(dt.stnoo)>0;

                    insert into stock (stno,itno,itqty)
                    Select stnoo,itno,itqty from (
                        Select dt.stnoo,dt.itno,(-1)*SUM((dt.qty*dt.itpkgqty))itqty from garnerd dt left join stock on dt.stnoo = stock.stno and dt.itno = stock.itno 
                        where gadate >= @day and gadate <= @day1 and LEN(dt.stnoo)>0 and ittrait = 3 and stock.stno is null and stock.itno is null
                        group by dt.stnoo,dt.itno
                    )A ;

                    update stock set itqty = ISNULL(itqty,0) - (aaa.數量) 
                    from stock 
                    inner join(
                        select X.stnoo,Y.itno,SUM((X.qty*X.itpkgqty*Y.itqty*Y.itpkgqty)/Y.itpareprs)數量 from garnerd X left join GarnBom Y on X.bomid = Y.bomid 
                        where gadate >= @day and gadate <= @day1 and LEN(X.stnoo)>0 and ittrait = 2
                        group by X.stnoo,Y.itno
                    )aaa
                    on (stock.itno = aaa.itno and stock.stno = aaa.stnoo) ;

                    insert into stock (stno,itno,itqty)
                    Select stnoo,itno,itqty from (
                        Select XY.stnoo,XY.itno,(-1)*SUM(XY.數量)itqty from(
                        select X.stnoo,Y.itno,((X.qty*X.itpkgqty*Y.itqty*Y.itpkgqty)/Y.itpareprs)數量 from garnerd X left join GarnBom Y on X.bomid = Y.BomID 
                        where gadate >= @day and gadate <= @day1 and LEN(X.stnoo)>0 and X.ittrait = 2 and Y.itno is not null
                        )XY 
                    left join stock on (XY.stnoo = stock.stno and XY.itno = stock.itno)
                    where stock.itno is null group by XY.stnoo,XY.itno
                    )A ;";
            cmd.ExecuteNonQuery();


            //單一組裝 ++
            List<dtDStNo> listIn = new List<dtDStNo>();
            listIn.Add(new dtDStNo { qty = "qty", day = "sadate", dtD = "RSaleD", StNo = "stno" });
            listIn.Add(new dtDStNo { qty = "qty", day = "bsdate", dtD = "BShopD", StNo = "stno" });//進貨
            listIn.Add(new dtDStNo { qty = "inqty", day = "indate", dtD = "InStkD", StNo = "stno" });
            listIn.Add(new dtDStNo { qty = "qty", day = "aldate", dtD = "allotd", StNo = "stnoi" });//調撥
            listIn.Add(new dtDStNo { qty = "qty", day = "addate", dtD = "adjustd", StNo = "stno" });
            listIn.Add(new dtDStNo { qty = "qty", day = "ledate", dtD = "lendd", StNo = "stnoi" });
            listIn.Add(new dtDStNo { qty = "qty", day = "ledate", dtD = "rlendd", StNo = "stno" });
            listIn.Add(new dtDStNo { qty = "qty", day = "bodate", dtD = "borrd", StNo = "stno" });
            listIn.Add(new dtDStNo { qty = "qty", day = "bodate", dtD = "rborrd", StNo = "stnoo" });
            listIn.Add(new dtDStNo { qty = "qty", day = "drdate", dtD = "drawd", StNo = "stnoi" });
            foreach (dtDStNo dm in listIn)
            {
                cmd.CommandText = @"
                        Update stock set itqty = ISNULL(itqty,0) + (dt.數量) 
                        From stock inner join " + group(dm.StNo, dm.dtD, "ittrait <> 1", dm.day, dm.qty) + @" dt on (stock.itno = dt.itno and stock.stno = dt." + dm.StNo + @") 
                        Where LEN(dt." + dm.StNo + @")>0;

                        Insert into stock (stno,itno,itqty)
                        Select " + dm.StNo + @",itno,itqty from (
                            Select dt." + dm.StNo + @",dt.itno,SUM((dt." + dm.qty + "*dt.itpkgqty))itqty from " + dm.dtD + @" dt left join stock on dt." + dm.StNo + @" = stock.stno and dt.itno = stock.itno 
                            Where " + dm.day + " >= @day and " + dm.day + " <= @day1 And LEN(dt." + dm.StNo + @")>0 and ittrait <> 1 and stock.stno is null and stock.itno is null
                            Group by dt." + dm.StNo + @",dt.itno
                        )A ;";
                cmd.ExecuteNonQuery();
            }

            //單一組裝 --
            List<dtDStNo> listOut = new List<dtDStNo>();
            listOut.Add(new dtDStNo { qty = "qty", day = "sadate", dtD = "SaleD", StNo = "stno" });//銷貨
            listOut.Add(new dtDStNo { qty = "qty", day = "bsdate", dtD = "RShopD", StNo = "stno" });
            listOut.Add(new dtDStNo { qty = "ouqty", day = "oudate", dtD = "OuStkD", StNo = "stno" });
            listOut.Add(new dtDStNo { qty = "qty", day = "aldate", dtD = "allotd", StNo = "stnoo" });//調撥
            listOut.Add(new dtDStNo { qty = "qty", day = "ledate", dtD = "lendd", StNo = "stno" });
            listOut.Add(new dtDStNo { qty = "qty", day = "ledate", dtD = "rlendd", StNo = "stnoi" });
            listOut.Add(new dtDStNo { qty = "qty", day = "bodate", dtD = "borrd", StNo = "stnoo" });
            listOut.Add(new dtDStNo { qty = "qty", day = "bodate", dtD = "rborrd", StNo = "stno" });
            listOut.Add(new dtDStNo { qty = "qty", day = "drdate", dtD = "drawd", StNo = "stnoo" });
            foreach (dtDStNo dm in listOut)
            {
                cmd.CommandText = @"
                        Update stock set itqty = ISNULL(itqty,0) - (dt.數量) 
                        From stock inner join " + group(dm.StNo, dm.dtD, "ittrait <> 1", dm.day, dm.qty) + @" dt on (stock.itno = dt.itno and stock.stno = dt." + dm.StNo + @") 
                        Where LEN(dt." + dm.StNo + @")>0 ;

                        Insert into stock (stno,itno,itqty)
                        Select " + dm.StNo + @",itno,itqty from (
                            Select dt." + dm.StNo + @",dt.itno,(-1)*SUM((dt." + dm.qty + "*dt.itpkgqty))itqty from " + dm.dtD + @" dt left join stock on dt." + dm.StNo + @" = stock.stno and dt.itno = stock.itno 
                            Where " + dm.day + " >= @day and " + dm.day + " <= @day1 And LEN(dt." + dm.StNo + @")>0 and ittrait <> 1 and stock.stno is null and stock.itno is null
                            Group by dt." + dm.StNo + @",dt.itno
                        )A ;";
                cmd.ExecuteNonQuery();
            }

            //組合品 ++
            List<dtDStNo> listIn2 = new List<dtDStNo>();
            listIn2.Add(new dtDStNo { qty = "qty", day = "sadate", dtD = "RSaleD", dtBom = "RSalebom", StNo = "stno" });
            listIn2.Add(new dtDStNo { qty = "inqty", day = "indate", dtD = "InStkD", dtBom = "InStkBOM", StNo = "stno" });
            listIn2.Add(new dtDStNo { qty = "qty", day = "ledate", dtD = "lendd", dtBom = "lendbom", StNo = "stnoi" });
            listIn2.Add(new dtDStNo { qty = "qty", day = "ledate", dtD = "rlendd", dtBom = "rlendbom", StNo = "stno" });
            listIn2.Add(new dtDStNo { qty = "qty", day = "bodate", dtD = "borrd", dtBom = "borrbom", StNo = "stno" });
            listIn2.Add(new dtDStNo { qty = "qty", day = "bodate", dtD = "rborrd", dtBom = "RBorrBom", StNo = "stnoo" });
            listIn2.Add(new dtDStNo { qty = "qty", day = "drdate", dtD = "drawd", dtBom = "DrawBom", StNo = "stnoi" });
            foreach (dtDStNo dm in listIn2)
            {
                cmd.CommandText = @"
                        update stock set itqty = ISNULL(itqty,0) + (aaa.數量) 
                        from stock 
                        inner join(
                            Select X." + dm.StNo + ",Y.itno,SUM((X." + dm.qty + @"*X.itpkgqty*Y.itqty*Y.itpkgqty)/Y.itpareprs)數量 
                            from " + dm.dtD + " X left join " + dm.dtBom + @" Y on X.bomid = Y.bomid 
                            Where " + dm.day + " >= @day and " + dm.day + " <= @day1 And LEN(X." + dm.StNo + @")>0 and ittrait = 1
                            group by X." + dm.StNo + @",Y.itno
                        )aaa
                        on (stock.itno = aaa.itno and stock.stno = aaa." + dm.StNo + @") ;

                        insert into stock (stno,itno,itqty)
                        Select " + dm.StNo + @",itno,itqty from (
                            Select XY." + dm.StNo + @",XY.itno,SUM(XY.數量)itqty from(
                            select X." + dm.StNo + ",Y.itno,((X." + dm.qty + @"*X.itpkgqty*Y.itqty*Y.itpkgqty)/Y.itpareprs)數量 from " + dm.dtD + " X left join " + dm.dtBom + @" Y on X.bomid = Y.BomID 
                            where " + dm.day + " >= @day and " + dm.day + " <= @day1 and LEN(X." + dm.StNo + @")>0 and X.ittrait = 1 and Y.itno is not null
                            )XY 
                            left join stock on (XY." + dm.StNo + @" = stock.stno and XY.itno = stock.itno)
                            where stock.itno is null group by XY." + dm.StNo + @",XY.itno
                        )A ;";
                cmd.ExecuteNonQuery();
            }

            //組合品 --
            List<dtDStNo> listOut2 = new List<dtDStNo>();
            listOut2.Add(new dtDStNo { qty = "qty", day = "sadate", dtD = "SaleD", dtBom = "SaleBom", StNo = "stno" });
            listOut2.Add(new dtDStNo { qty = "ouqty", day = "oudate", dtD = "OuStkD", dtBom = "OuStkBOM", StNo = "stno" });
            listOut2.Add(new dtDStNo { qty = "qty", day = "ledate", dtD = "lendd", dtBom = "lendbom", StNo = "stno" });
            listOut2.Add(new dtDStNo { qty = "qty", day = "ledate", dtD = "rlendd", dtBom = "rlendbom", StNo = "stnoi" });
            listOut2.Add(new dtDStNo { qty = "qty", day = "bodate", dtD = "borrd", dtBom = "borrbom", StNo = "stnoo" });
            listOut2.Add(new dtDStNo { qty = "qty", day = "bodate", dtD = "rborrd", dtBom = "RBorrBom", StNo = "stno" });
            listOut2.Add(new dtDStNo { qty = "qty", day = "drdate", dtD = "drawd", dtBom = "DrawBom", StNo = "stnoo" });
            foreach (dtDStNo dm in listOut2)
            {
                cmd.CommandText = @"
                        update stock set itqty = ISNULL(itqty,0) - (aaa.數量) 
                        from stock 
                        inner join(
                            select X." + dm.StNo + ",Y.itno,SUM((X." + dm.qty + @"*X.itpkgqty*Y.itqty*Y.itpkgqty)/Y.itpareprs)數量 
                            from " + dm.dtD + " X left join " + dm.dtBom + @" Y on X.bomid = Y.bomid 
                            where " + dm.day + " >= @day and " + dm.day + " <= @day1 And LEN(X." + dm.StNo + @")>0 and ittrait = 1
                            group by X." + dm.StNo + @",Y.itno
                        )aaa
                        on (stock.itno = aaa.itno and stock.stno = aaa." + dm.StNo + @") ;

                        insert into stock (stno,itno,itqty)
                        Select " + dm.StNo + @",itno,itqty from (
                            Select XY." + dm.StNo + @",XY.itno,(-1)*SUM(XY.數量)itqty from(
                            select X." + dm.StNo + ",Y.itno,((X." + dm.qty + @"*X.itpkgqty*Y.itqty*Y.itpkgqty)/Y.itpareprs)數量 from " + dm.dtD + " X left join " + dm.dtBom + @" Y on X.bomid = Y.BomID 
                            where " + dm.day + " >= @day and " + dm.day + " <= @day1 And LEN(X." + dm.StNo + @")>0 and X.ittrait = 1 and Y.itno is not null
                            )XY 
                            left join stock on (XY." + dm.StNo + @" = stock.stno and XY.itno = stock.itno)
                            where stock.itno is null group by XY." + dm.StNo + @",XY.itno
                        )A ;";
                cmd.ExecuteNonQuery();
            }
        }

        public void ReSetAllStock()
        {
            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.Parameters.AddWithValue("day", this.StartYear + "0101");
                    cmd.Parameters.AddWithValue("day1", this.EndYear + "1231");

                    NewMethod(cmd);

                    cmd.CommandText = @"
                        update stock 
                        set itqtyf = 0 
                        where itqtyf is null ;

                        update stock
                        set sttrait = stk.sttrait ,stock.stname = stk.stname
                        from stock
                        inner join stkroom stk on stock.stno = stk.stno ;

                        update stock
                        set itname = item.itname,stock.unit = item.itunit
                        from stock
                        inner join item on stock.itno = item.itno ;
 
                        update item
                        set item.itfirtqty = dt.itqtyf,item.itstockqty = dt.itqty
                        from item
                        inner join (select item.itno,SUM(ISNULL(itqtyf,0))itqtyf,SUM(ISNULL(itqty,0))itqty from item left join stock on item.itno = stock.itno group by item.itno)dt on item.itno = dt.itno ;

                        update item set Itfirtcost=ISNULL(Itfircost,0)*ISNULL(Itfirtqty,0) ;";
                    cmd.ExecuteNonQuery();

                    tn.Commit();

                    MessageBox.Show("計算完成!");
                }
                catch (Exception)
                {
                    if (tn != null)
                        tn.Rollback();

                    throw;
                }
            }
        }

        public void ReSetAllStock(SqlCommand cmd)
        {
            try
            {
                cmd.Parameters.AddWithValue("day", this.StartYear + "0101");
                cmd.Parameters.AddWithValue("day1", this.EndYear + "1231");

                NewMethod(cmd);

                cmd.CommandText = @"
                        update stock 
                        set itqtyf = 0 
                        where itqtyf is null ;

                        update stock
                        set sttrait = stk.sttrait ,stock.stname = stk.stname
                        from stock
                        inner join stkroom stk on stock.stno = stk.stno ;

                        update stock
                        set itname = item.itname,stock.unit = item.itunit
                        from stock
                        inner join item on stock.itno = item.itno ;

                        update item
                        set item.itfirtqty = dt.itqtyf,item.itstockqty = dt.itqty
                        from item
                        inner join (select item.itno,SUM(ISNULL(itqtyf,0))itqtyf,SUM(ISNULL(itqty,0))itqty from item left join stock on item.itno = stock.itno group by item.itno)dt on item.itno = dt.itno ;

                        update item set Itfirtcost=ISNULL(Itfircost,0)*ISNULL(Itfirtqty,0) ;";
                cmd.ExecuteNonQuery(); 
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public void EndThisYear()
        {
            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.Parameters.AddWithValue("day", this.StartYear + "0101");
                    cmd.Parameters.AddWithValue("day1", this.StartYear + "1231");

                    NewMethod(cmd);

                    cmd.CommandText = @"
                        update stock
                        set itqtyf = itqty ;

                        update stock
                        set itqty = 0 ;

                        update stock 
                        set itqtyf = 0 
                        where itqtyf is null ;

                        update stock
                        set sttrait = stk.sttrait ,stock.stname = stk.stname
                        from stock
                        inner join stkroom stk on stock.stno = stk.stno ;

                        update stock
                        set itname = item.itname,stock.unit = item.itunit
                        from stock
                        inner join item on stock.itno = item.itno ;

                        update item
                        set item.itfirtqty = dt.itqtyf,item.itstockqty = dt.itqty
                        from item
                        inner join (select itno,SUM(itqtyf)itqtyf,SUM(itqty)itqty from stock group by itno)dt on item.itno = dt.itno ;

                        update item set Itfirtcost=ISNULL(Itfircost,0)*ISNULL(Itfirtqty,0) ;";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "Update systemset set JZOK='JZOK',stkyear1=" + (Common.Sys_StkYear1 + 1) + ",stkyear2=" + (Common.Sys_StkYear2 + 1) + " where usrno='T01'";
                    cmd.ExecuteNonQuery();

                    tn.Commit();

                    MessageBox.Show("計算完成!");

                    S_61.MainForm.main.loadSystemSetting();
                }
                catch (Exception)
                {
                    if (tn != null)
                        tn.Rollback();

                    throw;
                }
            }
        }

    }
}
