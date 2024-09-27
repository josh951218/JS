using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using S_61.Basic;

namespace JBS.JS
{
    class Instk : xDocuments
    {
        protected override string MasterName
        {
            get { return "Instk"; }
        }

        protected override string KeyName
        {
            get { return "Inno"; }
        }

        public override bool 加庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "inqty")
        {
            try
            {
                var column = stname == "" ? "stno" : stname;
                if (dt.Columns.Contains(column) == false)
                    throw new ArgumentNullException();

                var stno = "";
                var itno = "";
                var inqty = 0M;
                var itpkgqty = 0M;
                var TotalQty = 0M;
                foreach (DataRow row in dt.Select("ItTrait = '2' OR ItTrait = '3'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    itno = row["itno"].ToString().Trim();
                    inqty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();
                    TotalQty = (inqty * itpkgqty).ToDecimal("f" + Common.Q);

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

                    inqty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();

                    var rec = row["BomRec"].ToString().Trim();
                    foreach (DataRow rw in dtBom.Select("BomRec = '" + rec + "'"))
                    {
                        itno = rw["itno"].ToString().Trim();
                        TotalQty = (inqty * itpkgqty * (rw["itqty"].ToDecimal() * rw["itpkgqty"].ToDecimal() / rw["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q);

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

        public override bool 扣庫存(SqlCommand cmd, DataTable dt, DataTable dtBom, string stname = "stno", string qtyname = "inqty")
        {
            try
            {
                var column = stname == "" ? "stno" : stname;
                if (dt.Columns.Contains(column) == false)
                    throw new ArgumentNullException();

                var stno = "";
                var itno = "";
                var inqty = 0M;
                var itpkgqty = 0M;
                var TotalQty = 0M;
                foreach (DataRow row in dt.Select("ItTrait = '2' OR ItTrait = '3'"))
                {
                    stno = row[column].ToString().Trim();
                    if (stno.Trim().Length == 0) continue;

                    itno = row["itno"].ToString().Trim();
                    inqty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();
                    TotalQty = (inqty * itpkgqty).ToDecimal("f" + Common.Q);

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

                    inqty = row[qtyname].ToDecimal();
                    itpkgqty = row["itpkgqty"].ToDecimal();

                    var rec = row["BomRec"].ToString().Trim();
                    foreach (DataRow rw in dtBom.Select("BomRec = '" + rec + "'"))
                    {
                        itno = rw["itno"].ToString().Trim();
                        TotalQty = (inqty * itpkgqty * (rw["itqty"].ToDecimal() * rw["itpkgqty"].ToDecimal() / rw["itpareprs"].ToDecimal())).ToDecimal("f" + Common.Q);

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

    }
}
