using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using S_61.Basic;


namespace JBS.JS
{
    public abstract class xDocuments
    {
        protected string Current { get; set; }
        protected abstract string MasterName { get; }
        protected abstract string KeyName { get; }

        private KeyMan keymam = new KeyMan();
        public KeyMan keyMan
        {
            get
            {
                return this.keymam;
            }
        }

        public string Top()
        {
            using (var db = new xSQL())
            {
                this.Current = db.Top(this.MasterName, this.KeyName);
                return this.Current;
            }
        }

        public string Prior()
        {
            using (var db = new xSQL())
            {
                var temp = db.Prior(this.MasterName, this.KeyName, this.Current);

                if (temp.Length == 0)
                {
                    MessageBox.Show("已最上一筆");
                    temp = db.CPrior(this.MasterName, this.KeyName, this.Current);
                }

                return this.Current = temp;
            }
        }

        public string Next()
        {
            using (var db = new xSQL())
            {
                var temp = db.Next(this.MasterName, this.KeyName, this.Current);

                if (temp.Length == 0)
                {
                    MessageBox.Show("已最下一筆");
                    temp = db.CNext(this.MasterName, this.KeyName, this.Current);
                }

                return this.Current = temp;
            }
        }

        public string Bottom()
        {
            using (var db = new xSQL())
            {
                this.Current = db.Bottom(this.MasterName, this.KeyName);
                return this.Current;
            }
        }

        public string Cancel()
        {
            using (var db = new xSQL())
            {
                var Current = db.Cancel(this.MasterName, this.KeyName, this.Current);
                return (this.Current.Length == 0) ? Next() : this.Current;
            }
        }

        public void Save(string key)
        {
            this.Current = key;
        }

        public string GetCurrent()
        {
            return this.Current ?? "";
        }

        public bool LoadData(string pk, Action<SqlDataReader> TLoad)
        {
            var load = false;
            using (var db = new xSQL())
            {
                var TSQL = " Select top 1 * from " + this.MasterName + " where " + this.KeyName + " = @key ";
                var result = db.ExecuteReader(TSQL, spc => spc.AddWithValue("key", pk), reader =>
                {
                    load = true;
                    TLoad.Invoke(reader);

                    this.Current = pk;
                });

                if (load == false)
                {
                    result = false;
                    this.Current = "";
                }

                return result;
            }
        }

        public void GetItemBom(string itno, string rec, ref DataTable dtBom)
        {
            using (SqlConnection cn = new SqlConnection(xSQL.xSqlConnectionString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("no", itno);
                cmd.CommandText = "Select * from bomd where boitno = @no";

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows == false)
                        return;

                    while (reader.Read())
                    {
                        DataRow row = dtBom.NewRow();
                        row["bomrec"] = rec;
                        row["itno"] = reader["itno"];
                        row["itname"] = reader["itname"];
                        row["itunit"] = reader["itunit"];
                        row["itqty"] = reader["itqty"];
                        row["itpareprs"] = reader["itpareprs"];
                        row["itpkgqty"] = reader["itpkgqty"];
                        row["itrec"] = reader["itrec"];
                        row["itprice"] = reader["itprice"];
                        row["itprs"] = reader["itprs"];
                        row["itmny"] = reader["itmny"];
                        row["itnote"] = reader["itnote"];
                        dtBom.Rows.Add(row);
                    }
                }
            }
        }

        public void RemoveBom(string rec, ref DataTable dtBom)
        {
            for (int i = 0; i < dtBom.Rows.Count; i++)
            {
                if (dtBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                {
                    dtBom.Rows.RemoveAt(i--);
                }
            }
            dtBom.AcceptChanges();
        }

        /// <summary>
        /// 是否有瀏覽進價的權限
        /// </summary>
        public bool EnableBShopPrice()
        {
            if (!Common.User_ShopPrice)
                MessageBox.Show("沒有權限!");

            return Common.User_ShopPrice;
        }

        /// <summary>
        /// 是否有瀏覽售價的權限
        /// </summary>
        public bool EnableSalePrice()
        {
            if (!Common.User_SalePrice)
                MessageBox.Show("沒有權限!");

            return Common.User_SalePrice;
        }

        /// <summary>
        /// 轉單時, 取得轉單來源的Bom資料
        /// </summary>
        public void GetTBom<T>(string bomid, string rec, ref DataTable dtBom) where T : IxBom, new()
        {
            using (SqlConnection cn = new SqlConnection(xSQL.xSqlConnectionString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("bomid", bomid);
                cmd.CommandText = "Select * from " + new T().TBom + " where bomid = @bomid ";

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows == false)
                        return;

                    while (reader.Read())
                    {
                        DataRow row = dtBom.NewRow();
                        row["bomrec"] = rec;
                        row["itno"] = reader["itno"];
                        row["itname"] = reader["itname"];
                        row["itunit"] = reader["itunit"];
                        row["itqty"] = reader["itqty"];
                        row["itpareprs"] = reader["itpareprs"];
                        row["itpkgqty"] = reader["itpkgqty"];
                        row["itrec"] = reader["itrec"];
                        row["itprice"] = reader["itprice"];
                        row["itprs"] = reader["itprs"];
                        row["itmny"] = reader["itmny"];
                        row["itnote"] = reader["itnote"];
                        dtBom.Rows.Add(row);
                    }
                }
            }
        }

        /// <summary>
        /// 是否在關帳與庫存年度內
        /// </summary>
        public bool IsEditInCloseDay(string day)
        {
            try
            {
                if (day.Trim().Length == 0)
                    return true;

                var edate = Date.ToTWDate(day);
                var year = edate.takeString(3).ToInteger();
                if (Common.Sys_StkYear1 > year)
                {
                    MessageBox.Show("單據日期不可超過進銷存年度兩年或小於進銷存年度！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (year >= (Common.Sys_StkYear1 + 2))
                {
                    MessageBox.Show("單據日期不可超過進銷存年度兩年或小於進銷存年度！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //代表無視限制
                if (Common.User_CloseMgr == 2)
                    return true;

                //代表沒有設定關帳日期
                if (Common.Sys_CloseDate.Trim().Length == 0)
                    return true;

                var sdate = Date.ToTWDate(Common.Sys_CloseDate);
                if ((String.CompareOrdinal(sdate, edate) >= 0))
                {
                    MessageBox.Show("此單據已經關帳,或是單據日期小於關帳日期,無法異動！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 是否傳輸至會計
        /// </summary> 
        public bool IsPassToAcc(string key)
        {
            using (var db = new xSQL())
            {
                var obj = "";
                var tsql = "Select acno,accono from " + this.MasterName + " where " + this.KeyName + " = @key ";
                db.ExecuteReader(
                    tsql,
                    spc => spc.AddWithValue("key", key),
                    reader =>
                    {
                        obj = reader["acno"].ToString().Trim();
                        if (obj.Length > 0)
                            MessageBox.Show("此單據已傳輸至會計傳票，無法異動！\n公司編號:" + reader["accono"].ToString().Trim() + " 傳票編號:" + reader["acno"].ToString().Trim(), "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    });
                return obj.Length > 0;
            }
        }

        /// <summary>
        /// 檢查單據是否存在
        /// </summary>
        public bool IsExistDocument<T>(string key) where T : xDocuments, new()
        {
            using (var db = new xSQL())
            {
                T t = new T();
                var tsql = "Select Count(*) from " + t.MasterName + " Where " + t.KeyName + " = @keyname";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("keyname", key)).ToDecimal();
                return (count > 0);
            }
        }

        /// <summary>
        /// 刪除時, 取得最新的bom, 用來扣庫存
        /// </summary>
        public void GetTempBomOnDeleting(string bomname, string keyname, ref DataTable tempbom)
        {
            tempbom.Clear();

            using (var db = new xSQL())
            {
                var tsql = "Select * from " + bomname + " where " + this.KeyName + " = @keyname ";
                db.Fill(tsql, spc => spc.AddWithValue("keyname", keyname), ref tempbom);
            }
        }

        /// <summary>
        /// 取得, 單據舊帳款
        /// </summary>
        public void GetOldAcctMnyOnDeleting(string key, out decimal acctmny, out decimal getprvacc)
        {
            using (var db = new xSQL())
            {
                var acc = 0M;
                var prvacc = 0M;

                var tsql = "Select acctmny,getprvacc from " + this.MasterName + " where " + this.KeyName + " = @key ";
                db.ExecuteReader(tsql, spc => spc.AddWithValue("key", key), reader =>
                {
                    acc = reader["acctmny"].ToDecimal();
                    prvacc = reader["getprvacc"].ToDecimal();
                });

                acctmny = acc;
                getprvacc = prvacc;
            }
        }

        public virtual bool 加庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
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
                foreach (DataRow row in dt.Select("ItTrait = '1'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();

                    var rec = row["BomRec"].ToString().Trim();
                    foreach (DataRow rw in dtBom.Select("BomRec = '" + rec + "'"))
                    {
                        itno = rw["itno"].ToString().Trim();
                        TotalQty = (qty * itpkgqty * (rw["itqty"].ToDecimal() * rw["itpkgqty"].ToDecimal() / rw["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q);

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
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool 扣庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
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
                foreach (DataRow row in dt.Select("ItTrait = '1'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    qty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();

                    var rec = row["BomRec"].ToString().Trim();
                    foreach (DataRow rw in dtBom.Select("BomRec = '" + rec + "'"))
                    {
                        itno = rw["itno"].ToString().Trim();
                        TotalQty = (qty * itpkgqty * (rw["itqty"].ToDecimal() * rw["itpkgqty"].ToDecimal() / rw["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q);

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
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新產品總庫存
        /// </summary>
        public virtual bool UpdateItemItStockQty(DataTable tempdtD, DataTable tempdtBom, DataTable dtD = null, DataTable dtBom = null)
        {
            if (dtD == null)
                dtD = tempdtD.Clone();

            if (dtBom == null)
                dtBom = tempdtBom.Clone();

            System.Collections.Concurrent.ConcurrentDictionary<string, string> dy = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            foreach (DataRow row in tempdtD.Rows)
            {
                var itno = row["itno"].ToString();
                if (!dy.ContainsKey(itno))
                    dy.TryAdd(itno, itno);
            }
            foreach (DataRow row in tempdtBom.Rows)
            {
                var itno = row["itno"].ToString();
                if (!dy.ContainsKey(itno))
                    dy.TryAdd(itno, itno);
            }
            foreach (DataRow row in dtD.Rows)
            {
                var itno = row["itno"].ToString();
                if (!dy.ContainsKey(itno))
                    dy.TryAdd(itno, itno);
            }
            foreach (DataRow row in dtBom.Rows)
            {
                var itno = row["itno"].ToString();
                if (!dy.ContainsKey(itno))
                    dy.TryAdd(itno, itno);
            }

            using (var db = new xSQL())
            {
                foreach (var k in dy)
                {
                    var itno = k.Key;
                    var tsql = " Select SUM(ISNULL(ItQty,0)) From Stock Where itno = @itno;";
                    var TotalQty = db.ExecuteScalar(tsql, spc => spc.AddWithValue("itno", itno)).ToDecimal("f" + Common.Q);

                    var tsql2 = " Update Item Set ItStockQty = " + TotalQty + " Where itno = @itno;";
                    db.ExecuteNonQuery(tsql2, spc => spc.AddWithValue("itno", itno));
                }
            }
            return true;
        }

        /// <summary>
        /// 檢查是否有註冊
        /// </summary>
        public bool IsRegisted(string master = "")
        {
            if (Common.Regist.Contains("正式版"))
                return true;

            using (var db = new xSQL())
            {
                var table = (master.Trim().Length > 0) ? master : this.MasterName;

                var tsql = "Select COUNT(*) from " + table;
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("NoNeed", "")).ToDecimal();
                return count < 50;
            }
        }

        public void RemoveEmptyRowOnSaving(DataGridView grid, ref DataTable dtD, ref DataTable dtBom, Action callback)
        {
            var result = grid.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].EditedFormattedValue.ToString().Trim().Length == 0);
            if (result == false)
                return;

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                if (dtD.Rows[i]["itno"].ToString().Trim().Length > 0)
                    continue;

                //刪組件
                var rec = dtD.Rows[i]["bomrec"].ToString().Trim();
                this.RemoveBom(rec, ref dtBom);

                //刪明細
                dtD.Rows.RemoveAt(i);
                dtD.AcceptChanges();
                i--;
            }
            callback.Invoke();
        }

        DialogResult Open<T>(string tIn, out string tOut) where T : IxValidate, new()
        {
            tOut = tIn;

            T t = new T();
            using (var frm = t.TOpen())
            {
                ((IxOpen)frm).TSeekNo = tIn;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    tOut = ((IxOpen)frm).TResult.Trim();
                    return DialogResult.OK;
                }
                return DialogResult.Cancel;
            }
        }

        public void Open<T>(object sender) where T : IxValidate, new()
        {
            var tb = sender as TextBox;

            if (tb.ReadOnly)
                return;

            if (tb.TrimTextLenth() == 0)
            {
                tb.Clear();
            }

            T t = new T();
            using (var frm = t.TOpen())
            {
                ((IxOpen)frm).TSeekNo = tb.Text;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    tb.Text = ((IxOpen)frm).TResult.Trim();
                }
            }
        }
        public void Open<T>(object sender, Action<SqlDataReader> callback) where T : IxValidate, new()
        {
            var tb = sender as TextBox;

            if (tb == null)
                throw new ArgumentNullException();

            if (tb.ReadOnly)
                return;

            if (tb.TrimTextLenth() == 0)
            {
                tb.Clear();
            }

            T t = new T();
            using (var frm = t.TOpen())
            {
                ((IxOpen)frm).TSeekNo = tb.Text;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var tOut = ((IxOpen)frm).TResult.Trim();

                    this.Validate<T>(tOut, reader =>
                    {
                        callback.Invoke(reader);
                    });

                    tb.Tag = tOut;
                }

                tb.SelectAll();
            }
        }
        public void ValidateOpen<T>(object sender, CancelEventArgs e, Action<SqlDataReader> TSuccessTIn, bool allowEmpty = false) where T : IxValidate, new()
        {
            var tb = sender as TextBox;

            if (tb.ReadOnly)
                return;

            if (tb.TrimTextLenth() == 0)
            {
                if (allowEmpty)
                {
                    tb.Clear();
                    return;
                }
                else
                {
                    e.Cancel = true;

                    tb.Clear();
                    MessageBox.Show("不可為空值!");
                    tb.SelectAll();
                    return;
                }
            }

            T t = new T();
            var tIn = tb.Text.Trim();
            using (var db = new xSQL())
            {
                var success = false;
                var tsql = "Select * from " + t.ValiTable + " where " + t.ValiKey + " = @tIn";
                db.ExecuteReader(tsql, spc => spc.AddWithValue("tIn", tIn), reader =>
                {
                    success = true;
                    tb.Text = tIn.Trim();
                    TSuccessTIn.Invoke(reader);
                });

                if (success == false)
                {
                    string tOut;

                    e.Cancel = true;
                    var dl = this.Open<T>(tIn, out tOut);
                    if (dl == DialogResult.OK)
                    {
                        db.ExecuteReader(tsql, spc => spc.AddWithValue("tIn", tOut), reader =>
                        {
                            tb.Text = tOut.Trim();
                            TSuccessTIn.Invoke(reader);
                        });
                    }
                }
            }
            tb.SelectAll();
        }
        public void Validate<T>(string tIn, Action<SqlDataReader> TSuccess, Action TFail = null) where T : IxValidate, new()
        {
            T t = new T();
            using (var db = new xSQL())
            {
                var success = false;
                var tsql = "Select * from " + t.ValiTable + " where " + t.ValiKey + " = @tIn";
                var result = db.ExecuteReader(tsql, spc => spc.AddWithValue("tIn", tIn), reader =>
                {
                    success = true;
                    TSuccess.Invoke(reader);
                });

                if (success == false)
                {
                    if (TFail != null)
                        TFail.Invoke();
                }
            }
        }

        public void DataGridViewOpen<T>(object sender, DataGridViewCellEventArgs e, DataTable dtD, Action<SqlDataReader> callback) where T : IxValidate, new()
        {
            var grid = sender as DataGridView;

            if (grid == null)
                throw new ArgumentNullException();

            if (grid.Columns[e.ColumnIndex].ReadOnly)
                return;

            var value = grid[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim();
            var name = grid.Columns[e.ColumnIndex].DataPropertyName;

            if (value.Length == 0)
            {
                dtD.Rows[e.RowIndex][name] = "";

                if (grid.EditingControl != null)
                    grid.EditingControl.Text = "";
            }

            T t = new T();
            using (var frm = t.TOpen())
            {
                ((IxOpen)frm).TSeekNo = value;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var tOut = ((IxOpen)frm).TResult.Trim();
                    this.Validate<T>(tOut, reader =>
                    {
                        dtD.Rows[e.RowIndex][name] = tOut.Trim();

                        if (grid.EditingControl != null)
                            grid.EditingControl.Text = tOut;

                        callback.Invoke(reader);
                    });
                }
            }

            if (grid.EditingControl != null)
                ((TextBox)grid.EditingControl).SelectAll();

            grid.InvalidateRow(e.RowIndex);
        }
        public void DataGridViewValidateOpen<T>(object sender, DataGridViewCellValidatingEventArgs e, DataTable dtD, Action<SqlDataReader> TSuccessTIn, bool allowEmpty = false) where T : IxValidate, new()
        {
            var grid = sender as DataGridView;

            if (grid == null)
                throw new ArgumentNullException();

            if (grid.Columns[e.ColumnIndex].ReadOnly)
                return;

            var value = grid[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim();
            var name = grid.Columns[e.ColumnIndex].DataPropertyName;

            if (value.Length == 0)
            {
                if (allowEmpty)
                {
                    dtD.Rows[e.RowIndex][name] = "";

                    if (grid.EditingControl != null)
                        grid.EditingControl.Text = "";

                    grid.InvalidateRow(e.RowIndex);
                    return;
                }
                else
                {
                    e.Cancel = true;

                    dtD.Rows[e.RowIndex][name] = "";

                    if (grid.EditingControl != null)
                        grid.EditingControl.Text = "";

                    grid.InvalidateRow(e.RowIndex);

                    MessageBox.Show("不可為空值!");
                    return;
                }
            }

            T t = new T();
            var tIn = value;
            using (var db = new xSQL())
            {
                var success = false;
                try
                {
                    Action<SqlDataReader> doAction = reader =>
                    {
                        success = true;

                        dtD.Rows[e.RowIndex][name] = tIn.Trim();

                        if (grid.EditingControl != null)
                            grid.EditingControl.Text = tIn;

                        TSuccessTIn.Invoke(reader);

                        grid.InvalidateRow(e.RowIndex);
                    };

                    var tsql = "";
                    if (t is Item)
                    {
                        tsql = "Select * from " + t.ValiTable + " where Len(@tIn) > 0 and (itno = @tIn or itnoudf = @tIn) ";
                        db.ExecuteReader(tsql, spc => spc.AddWithValue("tIn", tIn), doAction);
                    }
                    else
                    {
                        tsql = "Select * from " + t.ValiTable + " where " + t.ValiKey + " = @tIn";
                        db.ExecuteReader(tsql, spc => spc.AddWithValue("tIn", tIn), doAction);
                    }

                    if (success == false)
                    {
                        e.Cancel = true;


                        if (t is Item && grid.Columns[e.ColumnIndex].Name.Trim().Equals("產品編號"))
                        {
                            if (MessageBox.Show("請確定是否新增產品？", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                            {
                                using (var frm = new S_61.SOther.FrmItem())
                                {
                                    frm.newItno = tIn;
                                    frm.ShowDialog();
                                }
                                return;
                            }
                        }

                        string tOut;

                        
                        var dl = this.Open<T>(tIn, out tOut);
                        if (dl == DialogResult.OK)
                        {
                            db.ExecuteReader(tsql, spc => spc.AddWithValue("tIn", tOut), reader =>
                            {
                                dtD.Rows[e.RowIndex][name] = tOut.Trim();

                                if (grid.EditingControl != null)
                                    grid.EditingControl.Text = tOut;

                                TSuccessTIn.Invoke(reader);

                                grid.InvalidateRow(e.RowIndex);
                            });
                        }

                        if (grid.EditingControl != null)
                            ((TextBox)grid.EditingControl).SelectAll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public void DataGridView_ItNoMultipleOpen(object sender, int beginInsertIndex, Action DeleteRowMethod, Action<int> AddRowMethod, Action<SqlDataReader, int> callback)
        {
            var grid = sender as DataGridView;

            if (grid == null)
                throw new ArgumentNullException();

            string edivalue = "";
            if (grid.EditingControl != null)
                edivalue = grid.EditingControl.Text.Trim();

            using (S_61.SOther.FrmItemb_Multiple frm = new S_61.SOther.FrmItemb_Multiple())
            {
                frm.TSeekNo = edivalue;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DeleteRowMethod();
                    int index = beginInsertIndex;
                    foreach (string item in frm.MultipleItNo)
                    {
                        AddRowMethod(index);
                        this.Validate<Item>(item, reader =>
                        {
                            callback.Invoke(reader, index);
                        });
                        index++;
                    }
                    grid.CurrentCell = grid["產品編號", beginInsertIndex];
                    grid.Rows[beginInsertIndex].Selected = true;
                    grid.Focus();
                }
            }
        }

        public void DateValidate(object sender, CancelEventArgs e, bool allowEmpty = false)
        {
            var tb = sender as TextBox;

            if (tb == null)
                throw new ArgumentNullException();

            if (tb.ReadOnly)
                return;

            if (tb.TrimTextLenth() == 0)
            {
                if (allowEmpty)
                {
                    tb.Clear();
                    return;
                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("日期格式錯誤!");
                    tb.SelectAll();
                    return;
                }
            }

            if (tb.IsDateTime() == false)
            {
                e.Cancel = true;
                MessageBox.Show("日期格式錯誤!");
                tb.SelectAll();
                return;
            }
        }

        
        public bool GetPkNumber<T>(SqlCommand cmd, string date, ref JE.MyControl.TextBoxT tbNo) where T : xDocuments, new()
        {
            TextBox tb = new TextBox();
            try
            {
                tb.Text = tbNo.Text;

                var result = GetPkNumber<T>(ref cmd, date, ref tb);
                if (result)
                {
                    tbNo.Text = tb.Text;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tb.Dispose();
                tb = null;
            }
        }
        private bool GetPkNumber<T>(ref SqlCommand cmd, string date, ref TextBox tbNo) where T : xDocuments, new()
        {
            try
            {
                T t = new T();
                if (tbNo.TrimTextLenth() > 0)
                {
                    var key = tbNo.Text.Trim();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("key", key);
                    cmd.CommandText = "Select Count(*) from " + t.MasterName + " where " + t.KeyName + " = @key ";
                    var count = cmd.ExecuteScalar().ToDecimal();

                    if (count > 0)
                    {
                        MessageBox.Show("單據編號重複！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                DataTable temp = new DataTable();
                 
                cmd.CommandText = PkNumber1(t, ref date);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("date", date);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(temp);
                }

                if (temp.Rows.Count == 0)
                {
                    tbNo.Text = PkNumber2(date);
                }
                else
                {
                    var no = temp.AsEnumerable().Max(r => r[t.KeyName].ToString().Trim().skipString(r[t.KeyName].ToString().Trim().Length - 4).ToDecimal());
                    if (0 < no && no < 9999)
                    {
                        tbNo.Text = PkNumber3(date, no);
                    }
                    else
                    {
                        tbNo.Text = PkNumber4(t, date, temp);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string PkNumber1(xDocuments t, ref string date)
        {
            var tsql = "";
            if (Common.Sys_NoAdd == 1)
            {
                date = Date.ToTWDate(date);
                tsql = " Select " + t.KeyName + " from " + t.MasterName + " where " + t.KeyName + " like @date+'%' and Len(" + t.KeyName + ")=11";
            }
            else if (Common.Sys_NoAdd == 2)
            {
                date = Date.ToTWDate(date);
                date = date.takeString(5) + "00";
                tsql = " Select " + t.KeyName + " from " + t.MasterName + " where " + t.KeyName + " like @date+'%' and Len(" + t.KeyName + ")=11";
            }
            else if (Common.Sys_NoAdd == 3)
            {
                date = Date.ToUSDate(date);
                tsql = " Select " + t.KeyName + " from " + t.MasterName + " where " + t.KeyName + " like @date+'%' and Len(" + t.KeyName + ")=12";
            }
            else if (Common.Sys_NoAdd == 4)
            {
                date = Date.ToUSDate(date);
                date = date.takeString(6) + "00";
                tsql = " Select " + t.KeyName + " from " + t.MasterName + " where " + t.KeyName + " like @date+'%' and Len(" + t.KeyName + ")=12";
            }
            return tsql;
        }
        private string PkNumber2(string date)
        {
            var d = 1M;
            if (Common.Sys_NoAdd == 1)
            {
                return date + (d.ToString().PadLeft(4, '0'));
            }
            else if (Common.Sys_NoAdd == 2)
            {
                return date.takeString(5) + (d.ToString().PadLeft(6, '0'));
            }
            else if (Common.Sys_NoAdd == 3)
            {
                return date + (d.ToString().PadLeft(4, '0'));
            }
            else if (Common.Sys_NoAdd == 4)
            {
                return date.takeString(6) + (d.ToString().PadLeft(6, '0'));
            }
            else
            {
                return "";
            }
        }
        private string PkNumber3(string date, decimal d)
        {
            d++;
            if (Common.Sys_NoAdd == 1)
            {
                return date + (d.ToString().PadLeft(4, '0'));
            }
            else if (Common.Sys_NoAdd == 2)
            {
                return date.takeString(5) + (d.ToString().PadLeft(6, '0'));
            }
            else if (Common.Sys_NoAdd == 3)
            {
                return date + (d.ToString().PadLeft(4, '0'));
            }
            else if (Common.Sys_NoAdd == 4)
            {
                return date.takeString(6) + (d.ToString().PadLeft(6, '0'));
            }
            else return "";
        }
        private string PkNumber4(xDocuments t, string date, DataTable temp)
        {
            var d = 1M;
            var pkid = "";
            while (true)
            {
                if (Common.Sys_NoAdd == 1)
                {
                    pkid = date + (d.ToString().PadLeft(4, '0'));
                }
                else if (Common.Sys_NoAdd == 2)
                {
                    pkid = date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                }
                else if (Common.Sys_NoAdd == 3)
                {
                    pkid = date + (d.ToString().PadLeft(4, '0'));
                }
                else if (Common.Sys_NoAdd == 4)
                {
                    pkid = date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                }

                if (temp.AsEnumerable().Count(r => r[t.KeyName].ToString().Trim() == pkid) > 0)
                {
                    d++;
                    continue;
                }
                else
                {
                    break;
                }
            }
            return pkid;
        }
        public string GetInvoiceRandom()
        {
            string str = "";
            Random r = new Random();
            while (str.Length < 4)
            {
                str += r.Next(10);
            }
            return str;
        }

        /// <summary>
        /// 檢查單據是否修改中
        /// </summary>
        public bool IsModify<T>(string key) where T : xDocuments, new()
        {
            using (var db = new xSQL())
            {
                T t = new T();
                var tsql = "Select IsModify from " + t.MasterName + " Where " + t.KeyName + " = @keyname";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("keyname", key)).ToDecimal();

                return (count > 0);
            }
        }
        /// <summary>
        /// 更新單據狀態->修改狀態(1)，不能給其他使用者修改
        /// </summary>
        public bool upModify1<T>(string key) where T : xDocuments, new()
        {
            using (var db = new xSQL())
            {
                T t = new T();
                var tsql = "update " + t.MasterName + " set IsModify ='1' Where " + t.KeyName + " = @keyname";
                db.ExecuteScalar(tsql, spc => spc.AddWithValue("keyname", key)).ToDecimal();
                return (true);
            }
        }
        /// <summary>
        /// 更新單據狀態->修改完畢(0)，開放給其他使用者修改
        /// </summary>
        public bool upModify0<T>(string key) where T : xDocuments, new()
        {
            using (var db = new xSQL())
            {
                T t = new T();
                var tsql = "update " + t.MasterName + " set IsModify ='0' Where " + t.KeyName + " = @keyname";
                db.ExecuteScalar(tsql, spc => spc.AddWithValue("keyname", key)).ToDecimal();
                return (true);
            }
        }
        /// <summary>
        /// 修改前刷新單據內容
        /// </summary>
        public string Renew()
        {
            using (var db = new xSQL())
            {
                var Current = db.Cancel (this.MasterName, this.KeyName, this.Current);//跟取消功能一樣刷新單據內容
                return (this.Current.Length == 0) ? Next() : this.Current;
            }
        }
    
    }
}
