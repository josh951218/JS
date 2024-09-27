using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using S_61.Basic;

namespace JBS.JS
{
    public class Garner : xDocuments
    {
        protected override string MasterName
        {
            get { return "Garner"; }
        }

        protected override string KeyName
        {
            get { return "gano"; }
        }

        public override bool 加庫存(System.Data.SqlClient.SqlCommand cmd, System.Data.DataTable dt, System.Data.DataTable dtBom, string stname = "stno", string qtyname = "qty")
        {
            throw new NotImplementedException();
        }

        public override bool 扣庫存(System.Data.SqlClient.SqlCommand cmd, System.Data.DataTable dt, System.Data.DataTable dtBom, string stname = "stno", string qtyname = "qty")
        {
            throw new NotImplementedException();
        }

        public bool 新增加庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
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

                    cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                    TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                    cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                    cmd.ExecuteNonQuery();
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

                        cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                        TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                        cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
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

        public bool 新增扣庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
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
                foreach (DataRow row in dt.Select("ItTrait = '3'"))
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

                    cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                    TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                    cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                    cmd.ExecuteNonQuery();
                }
                foreach (DataRow row in dt.Select("ItTrait = '1' OR ItTrait = '2'"))
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

                        cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                        TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                        cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
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
        //
        public bool 刪除扣庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
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

                    cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                    TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                    cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                    cmd.ExecuteNonQuery();
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

                        cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                        TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                        cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
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

        public bool 刪除加庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "qty")
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
                foreach (DataRow row in dt.Select("ItTrait = '3'"))
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

                    cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                    TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                    cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
                    cmd.ExecuteNonQuery();
                }
                foreach (DataRow row in dt.Select("ItTrait = '1' OR ItTrait = '2'"))
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

                        cmd.CommandText = " Select SUM(ISNULL(ItQty,0)) From Stock Where ItNo = @ItNo;";
                        TotalQty = cmd.ExecuteScalar().ToDecimal("f" + Common.Q);

                        cmd.CommandText = " Update Item Set ItStockQty = " + TotalQty + " Where ItNo = @ItNo;";
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
    }
}
