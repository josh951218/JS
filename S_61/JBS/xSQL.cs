using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;

namespace JBS
{
    public class xSQL : IDisposable
    {
        #region 建構解構
        private bool disposed = false;
        public xSQL()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
            }

            // Free any unmanaged objects here.
            //

            disposed = true;
        }

        ~xSQL()
        {
            Dispose(false);
        }
        #endregion

        //資料庫連線字串
        public static string xSqlConnectionString = "";

        //資料庫結構異動
        public int GetTableCount(string TTable)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'" + TTable + "';";

                    cn.Open();
                    return cmd.ExecuteScalar().ToInteger();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //單據用
        public string Top(string TTable, string TColumn)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "select top(1) " + TColumn + " from " + TTable + " order by " + TColumn + "";

                    cn.Open();
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                        return obj.ToString().Trim();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Prior(string TTable, string TColumn, string TValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("TValue", TValue.Trim());
                    cmd.CommandText = "select top(1) " + TColumn + " from " + TTable + " where " + TColumn + " < @TValue order by " + TColumn + " desc";

                    cn.Open();
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                        return obj.ToString().Trim();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CPrior(string TTable, string TColumn, string TValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("TValue", TValue.Trim());
                    cmd.CommandText = "select top(1) " + TColumn + " from " + TTable + " where " + TColumn + " >= @TValue order by " + TColumn + "";

                    cn.Open();
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                        return obj.ToString().Trim();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Next(string TTable, string TColumn, string TValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("TValue", TValue.Trim());
                    cmd.CommandText = "select top(1) " + TColumn + " from " + TTable + " where " + TColumn + " > @TValue order by " + TColumn + "";

                    cn.Open();
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                        return obj.ToString().Trim();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CNext(string TTable, string TColumn, string TValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("TValue", TValue.Trim());
                    cmd.CommandText = "select top(1) " + TColumn + " from " + TTable + " where " + TColumn + " <= @TValue order by " + TColumn + " desc";

                    cn.Open();
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                        return obj.ToString().Trim();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Bottom(string TTable, string TColumn)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "select top(1) " + TColumn + " from " + TTable + " order by " + TColumn + " desc";

                    cn.Open();
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                        return obj.ToString().Trim();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改時更新單據用以及取消時更新用
        /// </summary> 
        public string Cancel(string TTable, string TColumn, string TValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("TValue", TValue.Trim());
                    cmd.CommandText = "select top(1) " + TColumn + " from " + TTable + " where " + TColumn + " = @TValue";

                    cn.Open();
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                        return obj.ToString().Trim();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Count(string Table, string TColumn, string TValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("TValue", TValue.Trim());
                    cmd.CommandText = " Select COUNT(*) from " + Table + " where " + TColumn + " = @TValue ";

                    cn.Open();
                    return cmd.ExecuteScalar().ToInteger();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Fill
        /// </summary>
        public bool Fill(string TSQL, Action<SqlParameterCollection> spc, ref DataTable dt)
        {
            try
            {
                dt.Clear();
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    spc(cmd.Parameters);
                    cmd.CommandText = TSQL;
                    da.Fill(dt);
                }
                return true;
            }
            catch (Exception ex)
            {
                dt.Clear();
                throw ex; 
            } 
        }
        /// <summary>
        /// ExecuteReader
        /// </summary>
        public bool ExecuteReader(string TSQL, Action<SqlParameterCollection> spc, Action<SqlDataReader> action)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    spc(cmd.Parameters);
                    cmd.CommandText = TSQL;
                     
                    cn.Open(); 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            action.Invoke(reader);
                            return true;
                        }
                    } 
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            } 
        }
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        public object ExecuteScalar(string TSQL, Action<SqlParameterCollection> spc)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    spc(cmd.Parameters);
                    cmd.CommandText = TSQL;

                    cn.Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            } 
        } 
        /// <summary>
        /// ExecuteNonQuery
        /// </summary> 
        public int ExecuteNonQuery(string TSQL, Action<SqlParameterCollection> spc)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(xSqlConnectionString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    spc(cmd.Parameters);
                    cmd.CommandText = TSQL;

                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;  
            } 
        } 
    }
}
