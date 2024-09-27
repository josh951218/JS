using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmSale_AppScNo : Formbase
    {
        SqlTransaction tran;
        public string AName;
        public string ATime;
        public string EName;
        public string ETime;
        string ScName1;

        public FrmSale_AppScNo()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
        }

        private void FrmSale_AppScNo_Load(object sender, EventArgs e)
        {
            foreach (var item in pnlBoxT1.Controls)
            {
                if (item is TextBox)
                    (item as TextBox).Text = "";
            }

            //新增人員
            LoadDB(AName);
            AppName.Text = AName;
            AppName1.Text = ScName1;
            AppTime.Text = ATime;

            //修改人員
            LoadDB(EName);
            EditName.Text = EName;
            EditName1.Text = ScName1;
            EditTime.Text = ETime;
        }

        void LoadDB(string str)
        {
            ScName1 = "";
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ScName", str);
                cmd.CommandText = "select ScName,ScName1 from Scrit where ScName=@ScName COLLATE Chinese_Taiwan_Stroke_BIN";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ScName1 = reader["ScName1"].ToString();
                    }
                }

                tran.Commit(); tran.Dispose();
                cmd.Dispose();
                conn.Close(); conn.Dispose();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show("LoadDBError:\n" + ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
