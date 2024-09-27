using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using S_61.Basic;

namespace JBS.JS
{
    public class BShop : xDocuments, IxValidate, IxBom
    {
        protected override string MasterName
        {
            get { return "BShop"; }
        }

        protected override string KeyName
        {
            get { return "bsno"; }
        }

        public string ValiTable
        {
            get { return "BShop"; }
        }

        public string ValiKey
        {
            get { return "bsno"; }
        }

        public Form TOpen()
        {
            return new S_61.subMenuFm_2.FrmBShop_Print_BsNo();
        }

        public string TBom
        {
            get { return "BShopBom"; }
        }
         
        public override bool 加庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
        {
            return 加庫存(cmd, dt, stname, qtyname);
        }

        public override bool 扣庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
        {
            return 扣庫存(cmd, dt, stname, qtyname);
        }

        private bool 加庫存(SqlCommand cmd, DataTable dt, string stname = "stno", string qtyname = "qty")
        {
            try
            {
                var column = stname == "" ? "stno" : stname;
                if (dt.Columns.Contains(column) == false)
                    throw new ArgumentNullException();

                var stno = "";
                var itno = "";
                var qty = 0M;
                var itpkgqty = 0M;
                var TotalQty = 0M;
                foreach (DataRow row in dt.Select("ItTrait = '2' OR ItTrait = '3'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    itno = row["itno"].ToString().Trim();
                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();
                    TotalQty = (qty * itpkgqty).ToDecimal("f" + Common.Q);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StNo", stno);
                    cmd.Parameters.AddWithValue("@ItNo", itno);
                    cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) + (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + TotalQty + ");";
                        cmd.ExecuteNonQuery();
                    }
                }
               
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool 扣庫存(SqlCommand cmd, DataTable dt, string stname = "stno", string qtyname = "qty")
        {
            try
            {
                var column = stname == "" ? "stno" : stname;
                if (dt.Columns.Contains(column) == false)
                    throw new ArgumentNullException();

                var stno = "";
                var itno = "";
                var qty = 0M;
                var itpkgqty = 0M;
                var TotalQty = 0M;
                foreach (DataRow row in dt.Select("ItTrait = '2' OR ItTrait = '3'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    itno = row["itno"].ToString().Trim();
                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();
                    TotalQty = (qty * itpkgqty).ToDecimal("f" + Common.Q);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StNo", stno);
                    cmd.Parameters.AddWithValue("@ItNo", itno);
                    cmd.CommandText = " Update Stock Set ItQty = IsNull(ItQty,0) - (" + TotalQty + ") Where StNo = @StNo And ItNo = @ItNo;";
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        cmd.CommandText = " Insert Into Stock (StNo,ItNo,ItQty) Values (@StNo,@ItNo," + (-1 * TotalQty) + ");";
                        cmd.ExecuteNonQuery();
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 是否已經有沖款
        /// </summary>
        public bool IsPassToPayabl(string key)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select COUNT(*) from payabld where " + this.KeyName + " = @key and extflag ='沖帳' and bracket ='進貨'";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("key", key));

                if (count.ToDecimal() > 0)
                    MessageBox.Show("此單據已經有沖款單沖帳，無法異動", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return count.ToDecimal() > 0;
            }
        }

        /// <summary>
        /// 是否有轉還出單據
        /// </summary>
        public bool IsPassToRBorrow(DataTable tempdtD)
        {
            using (var db = new xSQL())
            {
                for (int i = 0; i < tempdtD.Rows.Count; i++)
                {
                    if (tempdtD.Rows[i]["bono"].ToString().Trim() == "批借轉進")
                        return true;

                    var tsql = "Select Count(*) from RBorrD where BorrNo=@bono and borrid=@boid and Len(borrid)>0";
                    var exist = db.ExecuteScalar(tsql, spc =>
                    {
                        spc.AddWithValue("bono", tempdtD.Rows[i]["bono"].ToString().Trim());
                        spc.AddWithValue("boid", tempdtD.Rows[i]["boid"].ToString().Trim());
                    });

                    if (exist.ToDecimal() > 0)
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 新增, 進貨新帳款
        /// </summary> 
        public bool UpdateNewFactPayabl(string newfano, string acctmny, string getprvacc)
        {
            using (var db = new xSQL())
            {
                var tsql = @"
                Update  fact set FaPayable = FaPayable + (@acctmny)   where fano = @fano;
                Update  fact set FaPayamt =  FaPayamt  - (@getprvacc) where fano = @fano;";

                db.ExecuteNonQuery(tsql, spc =>
                {
                    spc.AddWithValue("fano", newfano);
                    spc.AddWithValue("acctmny", acctmny.ToDecimal());
                    spc.AddWithValue("getprvacc", getprvacc.ToDecimal());
                });
            }
            return true;
        }

        /// <summary>
        /// 回覆, 進貨舊帳款
        /// </summary> 
        public bool BackOldFactPayabl(string oldfano, decimal acctmny, decimal getprvacc)
        {
            using (var db = new xSQL())
            {
                var tsql = @"
                Update  fact set FaPayable = FaPayable - @acctmny   where fano = @fano;
                Update  fact set FaPayamt  = FaPayamt  + @getprvacc where fano = @fano;";

                db.ExecuteNonQuery(tsql, spc =>
                {
                    spc.AddWithValue("fano", oldfano);
                    spc.AddWithValue("acctmny", acctmny);
                    spc.AddWithValue("getprvacc", getprvacc);
                });
            }
            return true;
        }
         
        /// <summary>
        /// 更新廠商交易日
        /// </summary>
        public void UpdateFactLastDay(string day, string fano)
        {
            using (var db = new xSQL())
            {
                var tsql = " Update Fact Set FaLastday =(@day),FaLastday1 =(@day1) Where FaNo =(@fano) And FaLastday < (@day);";
                db.ExecuteNonQuery(tsql, spc =>
                {
                    spc.AddWithValue("fano", fano);
                    spc.AddWithValue("day", Date.ToTWDate(day));
                    spc.AddWithValue("day1", Date.ToUSDate(day));
                });
            }
        }

        /// <summary>
        /// 更新產品交易日與產品進價
        /// </summary>
        public void UpdateItemDatePrice(string day, ref DataTable dtD)
        {
            using (var db = new xSQL())
            {
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    var itno = dtD.Rows[i]["itno"].ToString();
                    var tsql = " Update Item Set itbuydate =(@day),itbuydate1 =(@day1) Where ItNo =(@itno) And (itbuydate is null or itbuydate < (@day));";

                    db.ExecuteNonQuery(tsql, spc =>
                    {
                        spc.AddWithValue("itno", itno);
                        spc.AddWithValue("day", Date.ToTWDate(day));
                        spc.AddWithValue("day1", Date.ToUSDate(day));
                    });

                    if (Common.Sys_AutoBuyp != 2)
                        continue;
                     
                    var sqlforAutoBuyp = "";
                    var unit = dtD.Rows[i]["itunit"].ToString().Trim();
                    var itpkgqty = dtD.Rows[i]["itpkgqty"].ToDecimal();
                    var realcost = dtD.Rows[i]["realcost"].ToDecimal("f" + Common.MF);
                    var avgcost = itpkgqty == 0 ? 0M : (realcost / itpkgqty).ToDecimal("f" + Common.MF);

                    var iUnit = "";
                    var iUnitp = "";

                    tsql = "Select itunit,itunitp from item where itno=@itno";
                    db.ExecuteReader(tsql, spc => spc.AddWithValue("itno", itno), reader =>
                    {
                        iUnit = reader["itunit"].ToString().Trim();
                        iUnitp = reader["itunitp"].ToString().Trim();

                        if (iUnit == unit)
                        {
                            sqlforAutoBuyp = "update item set itbuypri=@itbuypri where itno=@itno ";
                        }
                        else if (iUnitp == unit)
                        {
                            sqlforAutoBuyp = "update item set itbuypri=@price,itbuyprip=@itbuypri where itno=@itno ";
                        }
                    });

                    if (sqlforAutoBuyp.Trim().Length > 0)
                    {
                        db.ExecuteNonQuery(sqlforAutoBuyp, spc =>
                        {
                            spc.AddWithValue("itno", itno);
                            spc.AddWithValue("price", avgcost);
                            spc.AddWithValue("itbuypri", realcost);
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 取得立沖單號
        /// </summary> 
        public string GetThisPassToPayabl(string key)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select pano from payabld where ExtFlag = @單據 and " + this.KeyName + " = @keyname COLLATE Chinese_Taiwan_Stroke_BIN";
                var obj = db.ExecuteScalar(tsql, spc =>
                {
                    spc.AddWithValue("單據", "進貨");
                    spc.AddWithValue("keyname", key);
                });
                return obj == null ? "" : obj.ToString().Trim();
            }
        }

       
        
    }
}
