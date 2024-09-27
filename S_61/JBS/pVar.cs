using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
 
namespace S_61.Basic
{
    public class pVar
    {
        public static subMenuFm_3.FrmCust_Receivd FrmCust_Receivd;
        public static subMenuFm_3.FrmReceivd FrmReceivd;
        public static subMenuFm_3.FrmPayabld FrmPayabld;
        public static subMenuFm_3.FrmFact_Receivd FrmFact_Receivd;
        public static SOther.FrmItem_Inventory FrmItem_Inventory;

        public static string TRS = "";
        public static bool ShowTRS = false;

        public static bool Xa01Validate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from xa01 where xa1no =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["xa1no"].ToString();
                                TxName.Text = reader["xa1name"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool XX01Validate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from xx01 where x1no =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["x1no"].ToString();
                                TxName.Text = reader["x1name"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool XX02Validate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from xx02 where x2no =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["x2no"].ToString();
                                TxName.Text = reader["x2name"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool XX03Validate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from xx03 where x3no =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["x3no"].ToString();
                                TxName.Text = reader["x3name"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool XX04Validate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from xx04 where x4no =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["x4no"].ToString();
                                TxName.Text = reader["x4name"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool XX05Validate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from xx05 where x5no =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["x5no"].ToString();
                                TxName.Text = reader["x5name"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool XX06Validate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from xx06 where x6no =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["x6no"].ToString();
                                TxName.Text = reader["x6name"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool XX12Validate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from xx12 where x12no =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["x12no"].ToString();
                                TxName.Text = reader["x12name"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool EmplValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select emno,emname from empl where emno =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["emno"].ToString();
                                TxName.Text = reader["emname"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool CuPareValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select cuno,cuname1 from cust where cuno =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["cuno"].ToString();
                                TxName.Text = reader["cuname1"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool StkValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select stno,stname from stkroom where stno =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["stno"].ToString();
                                TxName.Text = reader["stname"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool SendValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select seno,sename from send where seno =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["seno"].ToString();
                                TxName.Text = reader["sename"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool SpecValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select spno,spname from spec where spno =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["spno"].ToString();
                                TxName.Text = reader["spname"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool DeptValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select deno,dename1 from dept where deno =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["deno"].ToString();
                                TxName.Text = reader["dename1"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool KindValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select kino,kiname from kind where kino =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["kino"].ToString();
                                TxName.Text = reader["kiname"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool ItemValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select itno,itname from item where itno =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["itno"].ToString();
                                TxName.Text = reader["itname"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool ItemValidate(string TKey)
        {
            if (TKey.IsNullOrEmpty()) return false;
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select COUNT(*) from item where itno =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        if (cmd.ExecuteScalar().ToDecimal() > 0) flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool faPareValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select fano,faname1 from fact where fano =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["fano"].ToString();
                                TxName.Text = reader["faname1"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }
        public static bool InvModeValidate(string TKey, TextBox TxNo, TextBox TxName)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from InvMode where ImNo =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["ImNo"].ToString();
                                TxName.Text = reader["ImName"].ToString();
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }

        public static void MemoMemoOpenForm(TextBox TxNo, int Len)
        {
            if (TxNo.ReadOnly) return;
            using (subMenuFm_2.FrmSale_Memo frm = new subMenuFm_2.FrmSale_Memo())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        TxNo.Text = frm.Memo.GetUTF8(Len);
                        break;
                    case DialogResult.Cancel: break;
                }
            }
        }
        public static void PictureOpenForm(string no)
        {
            using (SOther.FrmPicture frm = new SOther.FrmPicture())
            {
                frm.ItNo = no;
                frm.ShowDialog();
            }
        }

        public static bool XX03Validate(string TKey, TextBox TxNo, TextBox TxName, TextBox TxRate)
        {
            if (TKey.Trim() == "")
            {
                TxNo.Text = "";
                TxName.Text = "";
                return false;
            }
            bool flag = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("TKey", TKey.Trim());
                        cmd.CommandText = "select * from xx03 where x3no =@TKey COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                flag = true;
                                TxNo.Text = reader["x3no"].ToString();
                                TxName.Text = reader["x3name"].ToString();
                                decimal rate = 0;
                                decimal.TryParse(reader["x3rate"].ToString(), out rate);
                                rate = rate * 100;
                                TxRate.Text = rate.ToString("f0");
                                return flag;
                            }
                            else
                            {
                                TxNo.Text = TKey;
                                TxName.Text = "";
                                TxRate.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return flag;
            }
            return flag;
        }

        public static void CheckLowCost(string itno, string itunit, decimal price)
        {
            try
            {
                bool lowcost = false;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("itno", itno);
                    cmd.CommandText = "select * from item where itno=@itno";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            if (reader["itunitp"].ToString().Trim() == itunit)
                            {
                                if (Common.Sys_LowCost == 1)
                                {
                                    lowcost = reader["itbuyprip"].ToDecimal() > price ? true : false;
                                    if (lowcost) MessageBox.Show("產品編號：" + itno + "\n品名規格：" + reader["itname"] + "\n售價: " + price + " 已低於進價，請確認!!!\n此產品包裝進價為:" + reader["itbuyprip"].ToDecimal().ToString("f" + Common.MS));
                                }
                                if (Common.Sys_LowCost == 2)
                                {
                                    lowcost = reader["itcostp"].ToDecimal() > price ? true : false;
                                    if (lowcost) MessageBox.Show("產品編號：" + itno + "\n品名規格：" + reader["itname"] + "\n售價: " + price + " 已低於標準成本，請確認!!!\n此產品包裝標準成本為:" + reader["itcostp"].ToDecimal().ToString("f" + Common.MS));
                                }
                            }
                            else
                            {
                                if (Common.Sys_LowCost == 1)
                                {
                                    lowcost = reader["itbuypri"].ToDecimal() > price ? true : false;
                                    if (lowcost) MessageBox.Show("產品編號：" + itno + "\n品名規格：" + reader["itname"] + "\n售價: " + price + " 已低於進價，請確認!!!\n此產品單位進價為:" + reader["itbuypri"].ToDecimal().ToString("f" + Common.MS));
                                }
                                if (Common.Sys_LowCost == 2)
                                {
                                    lowcost = reader["itcost"].ToDecimal() > price ? true : false;
                                    if (lowcost) MessageBox.Show("產品編號：" + itno + "\n品名規格：" + reader["itname"] + "\n售價: " + price + " 已低於標準成本，請確認!!!\n此產品單位標準成本為:" + reader["itcost"].ToDecimal().ToString("f" + Common.MS));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void SetMemoUdf(DataGridViewColumn TColumn)
        {
            TColumn.HeaderText = Common.Sys_MemoUdf;
        }
        public static void SetRadioUdf(List<Control> pnlist, string TFormName)
        {
            List<DataRow> rdlist = new List<DataRow>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from usrcapt where formname =@formname";
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("formname", TFormName);
                    cmd.CommandText = sql;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        if (table.Rows.Count > 0)
                            rdlist = table.AsEnumerable().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                DataRow row = null;
                RadioButton rd = null;
                if (rdlist.Count > 0)
                {
                    foreach (Control pnl in pnlist)
                    {
                        row = rdlist.Find(r => r["classname"].ToString() == pnl.Name);
                        if (row != null)
                        {
                            rd = pnl.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Name == row["usrvaluen"].ToString());
                            if (rd != null)
                                rd.Checked = true;
                            for (int i = 5; i < row.Table.Columns.Count; i++)
                            {
                                var nv = row[i].ToString().Trim();
                                if (nv.Length == 0) continue;
                                else
                                {
                                    var name = nv.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries).First();
                                    var value = nv.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries).Last();
                                    if (name.Length > 0)
                                    {
                                        var temp = pnl.Controls.Find(name, false).FirstOrDefault();
                                        if (temp != null) temp.Text = value.Length > 0 ? value : temp.Tag.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void SaveRadioUdf(List<Control> pnlist, string TFormName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("formname", TFormName);
                        string sudf = "delete from usrcapt where formname =@formname ";
                        cmd.CommandText = sudf;
                        cmd.ExecuteNonQuery();


                        foreach (Control cl in pnlist)
                        {
                            cmd.Parameters.Clear();

                            var li = cl.Controls.OfType<RadioButton>().Take(10).ToList();
                            sudf = "insert into usrcapt ([formname],[classname],[usrvaluen]";
                            for (int i = 0; i < li.Count; i++)
                            {
                                sudf += ",[udf" + (i + 1).ToString().PadLeft(2, '0') + "]";
                            }
                            sudf += ") values (@formname,@classname,@usrvaluen";
                            for (int i = 0; i < li.Count; i++)
                            {
                                cmd.Parameters.AddWithValue("udf" + (i + 1).ToString().PadLeft(2, '0'), li[i].Name + "::" + li[i].Text);
                                sudf += ",@udf" + (i + 1).ToString().PadLeft(2, '0');
                            }
                            sudf += ")";

                            string rdname = cl.Controls.OfType<RadioButton>().Where(r => r.Checked).Select(r => r.Name).FirstOrDefault();
                            cmd.Parameters.AddWithValue("formname", TFormName);
                            cmd.Parameters.AddWithValue("classname", cl.Name);
                            cmd.Parameters.AddWithValue("usrvaluen", rdname);
                            cmd.CommandText = sudf;
                            cmd.ExecuteNonQuery();

                            //string rdname = cl.Controls.OfType<RadioButton>().Where(r => r.Checked).Select(r => r.Name).FirstOrDefault();
                            //sudf = "insert into usrcapt ([formname],[classname],[usrvaluen]) values (@formname,@classname,@usrvaluen);";
                            //cmd.CommandText = sudf;
                            //cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("儲存成功", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void ResetRadioUdf(string TFormName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("formname", TFormName);
                        cmd.CommandText = "delete from usrcapt where formname =@formname";
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("重置完成", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}

