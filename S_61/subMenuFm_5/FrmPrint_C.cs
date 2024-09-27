using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmPrint_C : Formbase
    {
        JBS.JS.xEvents xe; 

        public FrmPrint_C()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmPrint_C_Load(object sender, EventArgs e)
        {
            if (this.Tag.ToString() == "客戶資料瀏覽")
            {
                this.Text = "客戶資料瀏覽";
            }
            else
            {
                this.Text = "客戶郵遞標簽";
            }

            if (Common.listSysSettings.Count > 0)
            {
                var v = Common.listSysSettings.First().Field<string>("cuudfc1");
                if (v != "" && v != null) lblCuUdf1.Text = v.Trim();
            }
            CuNo.Focus();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (CuNo.Text.Trim() != "" && CuNo_1.Text.Trim() != "")
            {
                if (CuNo.Text.Trim()[0] > CuNo_1.Text.Trim()[0])
                {
                    MessageBox.Show("起始客戶不可大於終止客戶，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CuNo.Focus();
                    return;
                }
            }
            if (CuX1No.Text.Trim() != "" && CuX1No_1.Text.Trim() != "")
            {
                if (CuX1No.Text.Trim()[0] > CuX1No_1.Text.Trim()[0])
                {
                    MessageBox.Show("起始客戶類別編號不可大於終止客戶類別編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CuX1No.Focus();
                    return;
                }
            }
            if (CuEmNo1.Text.Trim() != "" && CuEmNo1_1.Text.Trim() != "")
            {
                if (CuEmNo1.Text.Trim()[0] > CuEmNo1_1.Text.Trim()[0])
                {
                    MessageBox.Show("起始業務人員編號不可大於終止業務人員編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CuEmNo1.Focus();
                    return;
                }
            }
            if (CuX4No.Text.Trim() != "" && CuX4No_1.Text.Trim() != "")
            {
                if (CuX4No.Text.Trim()[0] > CuX4No_1.Text.Trim()[0])
                {
                    MessageBox.Show("起始結帳類別編號不可大於終止結帳類別編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CuX4No.Focus();
                    return;
                }
            }

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CuNo", CuNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuNo_1", CuNo_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuX1No", CuX1No.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuX1No_1", CuX1No_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuEmNo1", CuEmNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuEmNo1_1", CuEmNo1_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuR1", CuR1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuR1_1", CuR1_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuX4No", CuX4No.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuX4No_1", CuX4No_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuName1", CuName1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuIme", CuIme.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuTel1", CuTel1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuAddr1", CuAddr1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuMemo1", CuMemo1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuUdf1", CuUdf1.Text.Trim());

                    string str = "select 列印='N',* from cust where '0'='0'";
                    if (CuNo.Text.Trim() != "")
                        str += " and CuNo >=@CuNo";
                    if (CuNo_1.Text.Trim() != "")
                        str += " and CuNo <=@CuNo_1";
                    if (CuX1No.Text.Trim() != "")
                        str += " and CuX1No >=@CuX1No";
                    if (CuX1No_1.Text.Trim() != "")
                        str += " and CuX1No <=@CuX1No_1";
                    if (CuEmNo1.Text.Trim() != "")
                        str += " and CuEmNo1 >=@CuEmNo1";
                    if (CuEmNo1_1.Text.Trim() != "")
                        str += " and CuEmNo1 <=@CuEmNo1_1";
                    if (CuR1.Text.Trim() != "")
                        str += " and CuR1 >=@CuR1";
                    if (CuR1_1.Text.Trim() != "")
                        str += " and CuR1 <=@CuR1_1";
                    if (CuX4No.Text.Trim() != "")
                        str += " and CuX4No >=@CuX4No";
                    if (CuX4No_1.Text.Trim() != "")
                        str += " and CuX4No <=@CuX4No_1";
                    if (CuName1.Text.Trim() != "")
                        str += " and CuName1 like @CuName1+'%'";
                    if (CuIme.Text.Trim() != "")
                        str += " and CuIme like @CuIme+'%'";
                    if (CuTel1.Text.Trim() != "")
                        str += " and CuTel1 like @CuTel1+'%'";
                    if (CuAddr1.Text.Trim() != "")
                        str += " and CuAddr1 like '%'+@CuAddr1+'%'";
                    if (CuMemo1.Text.Trim() != "")
                        str += " and CuMemo1 like '%'+@CuMemo1+'%'";
                    if (CuUdf1.Text.Trim() != "")
                        str += " and CuUdf1 like '%'+@CuUdf1+'%'";
                    cmd.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.Clear();
                    da.Fill(dt);
                    da.Dispose();
                }

                if (dt.Rows.Count > 0)
                {
                    if (this.Tag.ToString() == "客戶資料瀏覽")
                    {
                        //using (var frm = new FrmCustInfob())
                        //{ 
                        //    frm.dt = dt;
                        //    frm.ShowDialog();
                        //}
                        this.OpemInfoFrom<FrmCustInfob>(() =>
                            {
                                FrmCustInfob frm = new FrmCustInfob();
                                frm.dt = dt;
                                return frm;
                            });
                    }
                    else if (this.Tag.ToString() == "客戶郵遞標簽")
                    {
                        using (var frm = new FrmPrint_Cb())
                        {
                            frm.table = dt;
                            frm.ShowDialog();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void CuX1No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX01>(sender);
        }

        private void CuEmNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void CuX4No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX04>(sender);
        }
    }
}
