using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;

namespace JBS.JS
{
    public class RSale : xDocuments
    {
        protected override string MasterName
        {
            get { return "RSale"; }
        }

        protected override string KeyName
        {
            get { return "sano"; }
        }

        /// <summary>
        /// 是否已經有沖款
        /// </summary> 
        public bool IsPassToReceiv(string key)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select COUNT(*) from receivd where " + this.KeyName + " = @key and extflag ='沖帳' and bracket ='銷退'";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("key", key));

                if (count.ToDecimal() > 0)
                    MessageBox.Show("此單據已經有沖款單沖帳，無法異動", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return count.ToDecimal() > 0;
            }
        }

        /// <summary>
        /// 新增, 銷退貨帳款
        /// </summary> 
        public bool UpdateNewCustReceiv(string newcuno, string acctmny, string getprvacc)
        {
            using (var db = new xSQL())
            {
                var tsql = @"
                Update  cust set CuReceiv = CuReceiv - @acctmny   where cuno = @cuno;
                Update  cust set CuAdvamt = CuAdvamt + @getprvacc where cuno = @cuno;";

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
        /// 回覆, 銷退貨帳款
        /// </summary> 
        public bool BackOldCustReceiv(string oldcuno, decimal acctmny, decimal getprvacc)
        {
            using (var db = new xSQL())
            {
                var tsql = @"
                Update  cust set CuReceiv = CuReceiv + (@acctmny)   where cuno = @cuno;
                Update  cust set CuAdvamt = CuAdvamt - (@getprvacc) where cuno = @cuno;";

                db.ExecuteNonQuery(tsql, spc =>
                {
                    spc.AddWithValue("cuno", oldcuno);
                    spc.AddWithValue("acctmny", acctmny.ToDecimal());
                    spc.AddWithValue("getprvacc", getprvacc.ToDecimal());
                });
            }
            return true;
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
                    spc.AddWithValue("單據", "銷退");
                    spc.AddWithValue("keyname", key);
                });
                return obj == null ? "" : obj.ToString().Trim();
            }
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
                if (count != null)
                {
                    if (count.ToString() == "已上傳")
                    {
                        MessageBox.Show("此發票已上傳平台，無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
