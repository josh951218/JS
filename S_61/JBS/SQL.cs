using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace S_61
{
    
    static class SQL
    {
        public static void ExecuteNonQuery(string Sqlcmd, parameters par = null, DataTable dt = null, SqlCommand cmd_ = null)
        {
            if (cmd_ == null)
            {
                using (SqlConnection cn = new SqlConnection(S_61.Basic.Common.sqlConnString))
                using (SqlCommand cmd = new SqlCommand("", cn))
                using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.SetParameters(par);
                    cmd.CommandText = Sqlcmd;

                    if (dt == null)
                        cmd.ExecuteNonQuery();
                    else if (dt != null)
                        SqlDataAdapter.Fill(dt);
                }
            }
            else
            {
                using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter(cmd_))
                {
                    cmd_.SetParameters(par);
                    cmd_.CommandText = Sqlcmd;

                    if (dt == null)
                        cmd_.ExecuteNonQuery();
                    else if (dt != null)
                        SqlDataAdapter.Fill(dt);
                }
            }
    
        }

        public static bool ExecuteReader(string Sqlcmd, parameters par, Action<SqlDataReader> Action, SqlCommand cmd_ = null)
        {
            if (cmd_ == null)
            {
                using (SqlConnection SqlConnection = new SqlConnection(S_61.Basic.Common.sqlConnString))
                using (SqlCommand cmd = SqlConnection.CreateCommand())
                {
                    bool HaveRow = false;
                    SqlConnection.Open();
                    cmd.SetParameters(par);
                    cmd.CommandText = Sqlcmd;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Action.Invoke(reader);
                            HaveRow = true;
                        }
                    }
                    return HaveRow;
                }
            }
            else
            {
                bool HaveRow = false;
                cmd_.SetParameters(par);
                cmd_.CommandText = Sqlcmd;
                SqlDataReader reader = cmd_.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Action.Invoke(reader);
                        HaveRow = true;
                    }
                }
                return HaveRow;
            }
        }

        public static string ExecuteScalar(string Sqlcmd=null, parameters par = null, SqlCommand cmd_ = null)
        {
            if (cmd_ == null)
            {
                using (SqlConnection SqlConnection = new SqlConnection(S_61.Basic.Common.sqlConnString))
                using (SqlCommand cmd = SqlConnection.CreateCommand())
                {
                    SqlConnection.Open();
                    cmd.SetParameters(par);
                    if (Sqlcmd!=null)
                        cmd.CommandText = Sqlcmd;

                    object obj = cmd.ExecuteScalar();
                    if (obj == null)
                    {
                        return "";
                    }
                    return cmd.ExecuteScalar().ToString();
                }
            }
            else 
            {
                cmd_.SetParameters(par);
                if (Sqlcmd != null)
                    cmd_.CommandText = Sqlcmd;
                object obj = cmd_.ExecuteScalar();
                if (obj == null)
                {
                    return "";
                }
                return cmd_.ExecuteScalar().ToString();
            }
        }

        public static void SetParameters(this SqlCommand cmd, parameters par)
        {
            if (par != null)
            {
                cmd.Parameters.Clear();
                for (int i = 0; i < par.list.Count; i++)
                {
                    cmd.Parameters.AddWithValue(par.list[i].paramater, par.list[i].value);
                }
            }
        }

        #region UsingTransction 多載區
        public static bool UsingTransction(Func<SqlCommand, bool> Event)
        {
            using (SqlConnection cn = new SqlConnection(S_61.Basic.Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                using (SqlTransaction tran = cn.BeginTransaction("Transaction"))
                {
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    try
                    {
                        if (Event(cmd))
                        {
                            tran.Commit();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return false;
                        throw ex;
                    }
                }

            }
        }
        //多載1
        public static bool UsingTransction(Func<SqlCommand, string, bool> Event, string str1)
        {
            using (SqlConnection cn = new SqlConnection(S_61.Basic.Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                using (SqlTransaction tran = cn.BeginTransaction("Transaction"))
                {
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    try
                    {
                        if (Event(cmd, str1))
                        {
                            tran.Commit();
                            return true;
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return false;
                        throw ex;
                    }
                }

            }
        }  
        #endregion
    }

    public class parameters
    {
        public struct parameter_struct
        {
            public string paramater;
            public string value;
            public parameter_struct(string p1, string p2)
            {
                paramater = p1;
                value = p2;
            }
        }

        public List<parameter_struct> list = new List<parameter_struct>();

        public parameters()
        { }

        public parameters(string paramater_, string value_)
        { 
            list.Add(new parameter_struct(paramater_, value_)); 
        }

        public void AddWithValue(string paramater_,string value_) 
        {
            list.Add(new parameter_struct(paramater_, value_));
        }

        public void Clear()
        {
            list.Clear();
        }
    }

}
