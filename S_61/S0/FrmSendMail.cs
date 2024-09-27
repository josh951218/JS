using System;
using System.Data.SqlClient;
using S_61.Basic;

namespace S_61.S0
{
    public partial class FrmSendMail : JE.MyControl.Formbase
    {
        public FrmSendMail()
        {
            InitializeComponent();
        }

        private void FrmSendMail_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = "Select top 1 * from SendMail";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SendID.Text = reader["sendid"].ToString().Trim();
                        SendPW.Text = reader["sendpw"].ToString().Trim();
                        Geter.Text = reader["geter"].ToString().Trim();
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("SendID", SendID.Text.Trim());
                cmd.Parameters.AddWithValue("SendPW", SendPW.Text.Trim());
                cmd.Parameters.AddWithValue("Geter", Geter.Text.Trim());
                cmd.CommandText = "Update SendMail Set SendID=@SendID, SendPW=@SendPW, Geter=@Geter;";

                cn.Open();
                var count = cmd.ExecuteNonQuery();
                if (count == 0)
                {
                    cmd.CommandText = "Insert into SendMail (SendID,SendPW,Geter) values (@SendID,@SendPW,@Geter)";
                    cmd.ExecuteNonQuery();
                }
            }

            this.Dispose();
        }
    }
}
