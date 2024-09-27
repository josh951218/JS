using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using S_61.Basic;

namespace JBS.JS
{
    public class RShop : xDocuments
    {
        protected override string MasterName
        {
            get { return "RShop"; }
        }

        protected override string KeyName
        {
            get { return "bsno"; }
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
                var tsql = "Select COUNT(*) from payabld where " + this.KeyName + " = @key and extflag ='沖帳' and bracket ='退貨'";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("key", key));

                if (count.ToDecimal() > 0)
                    MessageBox.Show("此單據已經有沖款單沖帳，無法異動", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return count.ToDecimal() > 0;
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
                Update  fact set FaPayable = FaPayable - (@acctmny)   where fano = @fano;
                Update  fact set FaPayamt =  FaPayamt  + (@getprvacc) where fano = @fano;";

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
                Update  fact set FaPayable = FaPayable + @acctmny   where fano = @fano;
                Update  fact set FaPayamt  = FaPayamt  - @getprvacc where fano = @fano;";

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
        /// 取得立沖單號
        /// </summary> 
        public string GetThisPassToPayabl(string key)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select pano from payabld where ExtFlag = @單據 and " + this.KeyName + " = @keyname COLLATE Chinese_Taiwan_Stroke_BIN";
                var obj = db.ExecuteScalar(tsql, spc =>
                {
                    spc.AddWithValue("單據", "退貨");
                    spc.AddWithValue("keyname", key);
                });
                return obj == null ? "" : obj.ToString().Trim();
            }
        }
    }
}
