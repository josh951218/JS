using System.Data.SqlClient;
using S_61.Basic;

namespace S_61.AppData {
    
    
    public partial class DSCoding 
    {
        public static int DeleteRow(SqlTable table, string pk, string pkno)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@pkno", pkno == null ? "" : pkno);
                        cmd.CommandText = " delete from " + table.ToString() + " where " + pk + " = @pkno ";
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        public static object GetValue(SqlTable table, string only_a_Result_youWant, string pkColumn, string pkValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@pkno", pkValue == null ? "" : pkValue);
                        cmd.CommandText = " select " + only_a_Result_youWant + " from " + table.ToString() + " where " + pkColumn + " = @pkno ";
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
