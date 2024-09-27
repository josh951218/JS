using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using S_61.Basic;
using System.Collections.Generic;
using S_61;

namespace JBS.JS
{
    public class Sale : xDocuments, IxValidate, IxBom
    {
        protected override string MasterName
        {
            get { return "Sale"; }
        }

        protected override string KeyName
        {
            get { return "sano"; }
        }

        public string ValiTable
        {
            get { return "Sale"; }
        }

        public string ValiKey
        {
            get { return "sano"; }
        }

        public Form TOpen()
        {
            return new S_61.subMenuFm_2.FrmSale_Print_SaNo();
        }

        public string TBom
        {
            get { return "SaleBom"; }
        }

        /// <summary>
        /// 是否已經有沖款
        /// </summary> 
        public bool IsPassToReceiv(string key)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select COUNT(*) from receivd where " + this.KeyName + " = @key and extflag ='沖帳' and bracket ='銷貨'";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("key", key));

                if (count.ToDecimal() > 0)
                    MessageBox.Show("此單據已經有沖款單沖帳，無法異動", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return count.ToDecimal() > 0;
            }
        }

        /// <summary>
        /// 是否已轉寄庫
        /// </summary> 
        public bool IsPassToInStk(string sano, bool msg = true)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select COUNT(*) from instk Where " + this.KeyName + " = @sano ";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("sano", sano)).ToDecimal();
                if (count > 0 && msg)
                    MessageBox.Show("此單據已經有寄庫資料，無法異動！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return count > 0;
            }
        }

        /// <summary>
        /// 是否有轉還入單據
        /// </summary>
        public bool IsPassToRLend(DataTable tempdtD)
        {
            using (var db = new xSQL())
            {
                for (int i = 0; i < tempdtD.Rows.Count; i++)
                {
                    if (tempdtD.Rows[i]["leno"].ToString().Trim() == "批借轉銷")
                        return true;

                    var tsql = "Select Count(*) from rlendd where LendNo=@leno and lenoid=@leid and Len(lenoid)>0";
                    var exist = db.ExecuteScalar(tsql, spc =>
                    {
                        spc.AddWithValue("leno", tempdtD.Rows[i]["leno"].ToString().Trim());
                        spc.AddWithValue("leid", tempdtD.Rows[i]["leid"].ToString().Trim());
                    });

                    if (exist.ToDecimal() > 0)
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 新增, 銷貨新帳款
        /// </summary> 
        public bool UpdateNewCustReceiv(string newcuno, string acctmny, string getprvacc)
        {
            using (var db = new xSQL())
            {
                var tsql = @"
                Update  cust set CuReceiv = CuReceiv + (@acctmny)   where cuno = @cuno;
                Update  cust set CuAdvamt = CuAdvamt - (@getprvacc) where cuno = @cuno;";

                db.ExecuteNonQuery(tsql, spc =>
                {
                    spc.AddWithValue("cuno", newcuno);
                    spc.AddWithValue("acctmny", acctmny.ToDecimal());
                    spc.AddWithValue("getprvacc", getprvacc.ToDecimal());
                });
            }
            return true;
        }

        /// <summary>
        /// 回覆, 銷貨舊帳款
        /// </summary> 
        public bool BackOldCustReceiv(string oldcuno, decimal acctmny, decimal getprvacc)
        {
            using (var db = new xSQL())
            {
                var tsql = @"
                Update  cust set CuReceiv = CuReceiv - @acctmny   where cuno = @cuno;
                Update  cust set CuAdvamt = CuAdvamt + @getprvacc where cuno = @cuno;";

                db.ExecuteNonQuery(tsql, spc =>
                {
                    spc.AddWithValue("cuno", oldcuno);
                    spc.AddWithValue("acctmny", acctmny);
                    spc.AddWithValue("getprvacc", getprvacc);
                });
            }
            return true;
        }

        /// <summary>
        /// 更新客戶交易日
        /// </summary>
        public void UpdateCustLastDay(string day, string cuno)
        {
            using (var db = new xSQL())
            {
                var tsql = " Update Cust Set CuLastday =(@day),CuLastday1 =(@day1) Where CuNo =(@cuno) And CuLastday < (@day);";
                db.ExecuteNonQuery(tsql, spc =>
                {
                    spc.AddWithValue("cuno", cuno);
                    spc.AddWithValue("day", Date.ToTWDate(day));
                    spc.AddWithValue("day1", Date.ToUSDate(day));
                });
            }
        }

        /// <summary>
        /// 更新產品交易日
        /// </summary>
        public void UpdateItemDate(string day, ref DataTable dtD)
        {
            using (var db = new xSQL())
            {
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    var itno = dtD.Rows[i]["itno"].ToString();
                    var tsql = " Update Item Set itsaldate =(@day),itsaldate1 =(@day1) Where ItNo =(@itno) And (itsaldate is null or itsaldate < (@day));";

                    db.ExecuteNonQuery(tsql, spc =>
                    {
                        spc.AddWithValue("itno", itno);
                        spc.AddWithValue("day", Date.ToTWDate(day));
                        spc.AddWithValue("day1", Date.ToUSDate(day));
                    });
                }
            }
        }

        /// <summary>
        /// 取得立沖單號
        /// </summary> 
        public string GetThisPassToReceiv(string key)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select reno from receivd where ExtFlag = @單據 and " + this.KeyName + " = @keyname COLLATE Chinese_Taiwan_Stroke_BIN";
                var obj = db.ExecuteScalar(tsql, spc =>
                {
                    spc.AddWithValue("單據", "銷貨");
                    spc.AddWithValue("keyname", key);
                });
                return obj == null ? "" : obj.ToString().Trim();
            }
        }

        /// <summary>
        /// 超過信用額度：1限制出貨 2銷貨警告 3銷貨不警告
        /// </summary>
        public bool IsOverCredit(string cuno, decimal acctmny, decimal getprvacc)
        {
            if (Common.Sys_UpCredit != 1 && Common.Sys_UpCredit != 2)
                return true;

            try
            {
                using (var db = new xSQL())
                {
                    var tsql = "Select COUNT(*) from cust where cuno = @cuno and CuCredit <> 0 and CuCredit-(cureceiv+@getprvacc-@acctmny) <= 0 ";

                    var count = db.ExecuteScalar(tsql, spc =>{
                        spc.AddWithValue("cuno", cuno);
                        spc.AddWithValue("acctmny", acctmny);
                        spc.AddWithValue("getprvacc", getprvacc);
                    });
                    if (count.ToDecimal() > 0)
                    {
                        //限制出貨
                        if (Common.Sys_UpCredit == 1)
                        {
                            MessageBox.Show("信用額度不足,禁止出貨!");
                            return false;
                        }

                        //出貨警告
                        if (Common.Sys_UpCredit == 2)
                        {
                            MessageBox.Show("信用額度不足或為0!");
                            return true;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNextInvNo(string X5No)
        {
            var end = "";
            if (X5No == "7" || X5No == "8") return "";
            if (X5No == "2") end = "ScInvoic2";
            if (X5No == "1") end = "ScInvoic3";
            if (X5No == "3") end = "ScInvoicA";
            if (X5No == "4") end = "ScInvoicA3";
            if (X5No != "5")
            {
                using (var db = new xSQL())
                {

                    var tsql = "Select " + end + " from scrit where scname = @scname ";
                    var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("scname", Common.User_Name));
                    if (obj != null)
                        return obj.ToString().Trim();
                }
            }
            return "";
        }
        public string GetNextInvNo78(string X5No, string User_Einv)
        {
            var end = "";
            if (X5No == "7") end = "ScInvoic7";
            if (X5No == "8") end = "ScInvoic8";
            using (var db = new xSQL())
            {
                var tsql = "Select " + end + " from Einvsetup where Einvid = @User_Einv";
                var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("User_Einv", User_Einv));
                if (obj != null)
                    return obj.ToString().Trim();
            }
            return "";
        }

        public bool IsPassToBatch(string key)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select COUNT(*) from batch where invno=@key";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("key", key));

                if (count.ToDecimal() > 0)
                    MessageBox.Show("此單據在發票系統已有批開發票，\n請刪除『已改明細發票』內中該筆發票，才可修改刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return count.ToDecimal() > 0;
            }
        }

        public bool IsSendToEINV(string key)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select einvstate from sale where sano=@key";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("key", key));

                if (count.ToString() == "已上傳")
                {
                    MessageBox.Show("此發票已上傳平台，無法修改與刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 獲得目前子階庫存量
        /// </summary>
        /// <param name="itno"></param>
        /// <param name="stno"></param>
        /// <param name="dtSaleD"></param>
        /// <param name="dtSaleBom"></param>
        internal DataTable GetBomQtyTable(DataTable InDtD, DataTable InDtbom) 
        {
            if (InDtD.Columns.Contains("stno"))//銷貨開窗
            {
                var BomQty =
                from d in InDtD.AsEnumerable()
                join bom in InDtbom.AsEnumerable()
                on d.Field<decimal>("BomRec") equals bom.Field<string>("BomRec").ToDecimal()
                where d.Field<decimal>("ItTrait") == 1
                select new
                {
                    itno = bom.Field<string>("itno"),
                    stno = d.Field<string>("stno"),
                    Qty = (d.Field<decimal>("qty") * d.Field<decimal>("itpkgqty") * (bom.Field<decimal>("itqty") * bom.Field<decimal>("itpkgqty") / bom.Field<decimal>("itpareprs"))).ToDecimal("f" + Common.Q)
                }
                ;
                DataTable OutBom = new DataTable();
                SQL.ExecuteNonQuery("select itno='',stno='',qty='' from item where 1 = 0", null, OutBom);
                foreach (var r in BomQty)
                {
                    DataRow dr = OutBom.NewRow();
                    dr["itno"] = r.itno;
                    dr["stno"] = r.stno;
                    dr["qty"] = r.Qty;
                    OutBom.Rows.Add(dr);
                }
                return OutBom;
            }
            else//領料開窗，明細無倉庫
            {
                var BomQty =
                               from d in InDtD.AsEnumerable()
                               join bom in InDtbom.AsEnumerable()
                               on d.Field<decimal>("BomRec") equals bom.Field<string>("BomRec").ToDecimal()
                               where d.Field<decimal>("ItTrait") == 1
                               select new
                               {
                                   itno = bom.Field<string>("itno"),
                                   Qty = (d.Field<decimal>("qty") * d.Field<decimal>("itpkgqty") * (bom.Field<decimal>("itqty") * bom.Field<decimal>("itpkgqty") / bom.Field<decimal>("itpareprs"))).ToDecimal("f" + Common.Q)
                               }
                               ;
                DataTable OutBom = new DataTable();
                SQL.ExecuteNonQuery("select itno='',qty='' from item where 1 = 0", null, OutBom);
                foreach (var r in BomQty)
                {
                    DataRow dr = OutBom.NewRow();
                    dr["itno"] = r.itno;
                    dr["qty"] = r.Qty;
                    OutBom.Rows.Add(dr);
                }
                return OutBom; 
            }

        }

        /// <summary>
        /// 計算是否低於庫存量
        /// </summary>
        internal int IsLowStock(string itno, string stno, string ItTrait, string BomRec,  DataTable dtD,  DataTable dtBom, DataTable BomQty, DataTable TempDtD, DataTable TempBomQty, FormEditState FormState)
        {
            //庫存量不控管
            if (Common.Sys_LowStock != 1)
                return 1;
            //產品編號是否為空
            if (itno.Trim().Length == 0)
                return 1;

            List<string> ListItno = new List<string>();
            if (ItTrait == "1")//組合品
            {
                foreach (var r in dtBom.Select("BomRec = '" + BomRec + "'"))
                {
                    ListItno.Add(r["itno"].ToString());
                }
            }
            else//組裝品、單一商品
            {
                ListItno.Add(itno);
            }
            for (int i = 0; i < ListItno.Count; i++)
            {
                #region 求現有庫存量  tQty [總倉or分倉]
                itno = ListItno[i];
                var itname = "";
                var itsafeqty = 0M;
                var stockOne = 0M;
                var stockAll = 0M;
                using (var db = new JBS.xSQL())
                {
                    var tsql = "Select * from item where itno = @itno ";
                    db.ExecuteReader(tsql, spc => spc.AddWithValue("itno", itno), row =>
                    {
                        itname = row["itname"].ToString().Trim();
                        itsafeqty = row["itsafeqty"].ToDecimal();
                    });

                    tsql = "Select itqty from stock where itno = @itno and stno = @stno";
                    stockOne = db.ExecuteScalar(tsql, spc =>
                    {
                        spc.AddWithValue("itno", itno);
                        spc.AddWithValue("stno", stno);
                    }).ToDecimal();

                    tsql = "Select Sum(itqty) from stock where itno = @itno";
                    stockAll = db.ExecuteScalar(tsql, spc => spc.AddWithValue("itno", itno)).ToDecimal();
                }
                //總倉or分倉
                var tQty = (Common.Sys_LowStockMode == 1) ? stockAll : stockOne;
                #endregion
                #region 求扣量 qty ，如果是1.修改狀態要扣掉暫存量 2.扣量qty [總倉or分倉]

                var TempQty = 0M;//暫存扣量
                var qty = 0M;   //扣量
                //1.如果是領料開窗的話，它的扣量跟著表頭跑，所以在求購量時不必分倉算。 2.如果是銷貨作業開窗時，因為它的扣量是根據明細的倉庫跑，所以會根據系統參數去判分倉或者不分倉
                if (Common.Sys_LowStockMode == 1 || !dtD.Columns.Contains("stno"))
                {
                    #region //求總倉扣量 : 領料開倉 or 銷貨開窗，且Sys_LowStockMode設定不分倉
                    if (FormState == FormEditState.Modify)
                    {
                        //修改狀態的明細
                        TempQty = TempDtD.AsEnumerable()
                            .Where(r => r["itno"].ToString().Trim() == itno && r["ItTrait"].ToString() != "1")
                            .Sum(r => r["qty"].ToDecimal() * r["itpkgqty"].ToDecimal());
                        //修改狀態的Bom
                        TempQty += TempBomQty.AsEnumerable()
                          .Where(r => r["itno"].ToString().Trim() == itno)
                          .Sum(r => r["qty"].ToDecimal());
                    }

                    //明細
                    qty = dtD.AsEnumerable()
                        .Where(r => r["itno"].ToString().Trim() == itno && r["ItTrait"].ToString() != "1")
                        .Sum(r => r["qty"].ToDecimal() * r["itpkgqty"].ToDecimal());
                    //Bom
                    qty += BomQty.AsEnumerable()
                        .Where(r => r["itno"].ToString().Trim() == itno)
                        .Sum(r => r["qty"].ToDecimal());
                    #endregion
                }
                else
                {
                    #region //求分倉扣量 :　銷貨開窗，且Sys_LowStockMode設定為分倉
                    if (FormState == FormEditState.Modify)
                    {
                        //修改狀態的明細
                        TempQty = TempDtD.AsEnumerable()
                            .Where(r => r["itno"].ToString().Trim() == itno && r["stno"].ToString().Trim() == stno && r["ItTrait"].ToString() != "1")
                            .Sum(r => r["qty"].ToDecimal() * r["itpkgqty"].ToDecimal());
                        //修改狀態的Bom
                        TempQty += TempBomQty.AsEnumerable()
                          .Where(r => r["itno"].ToString().Trim() == itno && r["stno"].ToString().Trim() == stno)
                          .Sum(r => r["qty"].ToDecimal());
                    }

                    //明細
                    qty = dtD.AsEnumerable()
                        .Where(r => r["itno"].ToString().Trim() == itno && r["stno"].ToString().Trim() == stno && r["ItTrait"].ToString() != "1")
                        .Sum(r => r["qty"].ToDecimal() * r["itpkgqty"].ToDecimal());
                    //Bom
                    qty += BomQty.AsEnumerable()
                        .Where(r => r["itno"].ToString().Trim() == itno && r["stno"].ToString().Trim() == stno)
                        .Sum(r => r["qty"].ToDecimal());
                    #endregion
                }
                
                //扣量 == 本次扣量  -暫存扣量
                qty = qty - TempQty;
                #endregion
                //安全存量
                if (Common.Sys_LowStkSlt == 1)
                {
                    if ((tQty - qty) < itsafeqty)
                    {
                        var 超出量 = ((tQty - qty - itsafeqty) < 0) ? (tQty - qty - itsafeqty) * -1 : (tQty - qty - itsafeqty);
                        MessageBox.Show("產品編號：" + itno + "\n品名規格：" + itname + "\n出貨數量已超出安全數量:" + 超出量.ToDecimal("f" + Common.Q) + "，請確認...");
                    }
                }
                //庫存量
                else
                {
                    if ((tQty - qty) < 0)
                    {
                        var 超出量 = ((tQty - qty < 0) ? (tQty - qty) * -1 : tQty - qty);
                        MessageBox.Show("產品編號：" + itno + "\n品名規格：" + itname + "\n出貨數量已大於現有庫存量:" + 超出量.ToDecimal("f" + Common.Q) + "，請確認...");
                    }
                }
            } 
            return 1;
        }

        
    }
}
