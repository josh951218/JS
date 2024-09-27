using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmPrint_F : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmPrint_F()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmPrint_F_Load(object sender, EventArgs e)
        {
            if (this.Tag.ToString() == "廠商資料瀏覽")
            {
                this.Text = "廠商資料瀏覽";
            }
            else
            {
                this.Text = "廠商郵遞標簽";
            }

            if (Common.listSysSettings.Count > 0)
            {
                var v = Common.listSysSettings.First().Field<string>("faudfc1");
                if (v != "" && v != null) lblFaUdf1.Text = v.Trim();
            }
            FaNo.Focus();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (FaNo.Text.Trim() != "" && FaNo_1.Text.Trim() != "")
            {
                if (FaNo.Text.Trim()[0] > FaNo_1.Text.Trim()[0])
                {
                    MessageBox.Show("起始廠商不可大於終止廠商，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FaNo.Focus();
                    return;
                }
            }
            if (FaX12no.Text.Trim() != "" && FaX12no_1.Text.Trim() != "")
            {
                if (FaX12no.Text.Trim()[0] > FaX12no_1.Text.Trim()[0])
                {
                    MessageBox.Show("起始廠商類別編號不可大於終止廠商類別編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FaX12no.Focus();
                    return;
                }
            }
            if (FaEmno1.Text.Trim() != "" && FaEmno1_1.Text.Trim() != "")
            {
                if (FaEmno1.Text.Trim()[0] > FaEmno1_1.Text.Trim()[0])
                {
                    MessageBox.Show("起始業務人員編號不可大於終止業務人員編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FaEmno1.Focus();
                    return;
                }
            }
            if (FaX4no.Text.Trim() != "" && FaX4no_1.Text.Trim() != "")
            {
                if (FaX4no.Text.Trim()[0] > FaX4no_1.Text.Trim()[0])
                {
                    MessageBox.Show("起始結帳類別編號不可大於終止結帳類別編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FaX4no.Focus();
                    return;
                }
            }
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@FaNo", FaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaNo_1", FaNo_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaX12no", FaX12no.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaX12no_1", FaX12no_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaEmno1", FaEmno1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaEmno1_1", FaEmno1_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaR1", FaR1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaR1_1", FaR1_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaX4no", FaX4no.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaX4no_1", FaX4no_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaName1", FaName1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaIme", FaIme.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaTel1", FaTel1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaAddr1", FaAddr1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaMemo1", FaMemo1.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaUdf1", FaUdf1.Text.Trim());

                    string str = "select 列印='N',* from Fact where '0'='0'";
                    if (FaNo.Text.Trim() != "")
                        str += " and FaNo >=@FaNo";
                    if (FaNo_1.Text.Trim() != "")
                        str += " and FaNo <=@FaNo_1";
                    if (FaX12no.Text.Trim() != "")
                        str += " and FaX12no >=@FaX12no";
                    if (FaX12no_1.Text.Trim() != "")
                        str += " and FaX12no <=@FaX12no_1";
                    if (FaEmno1.Text.Trim() != "")
                        str += " and FaEmno1 >=@FaEmno1";
                    if (FaEmno1_1.Text.Trim() != "")
                        str += " and FaEmno1 <=@FaEmno1_1";
                    if (FaR1.Text.Trim() != "")
                        str += " and FaR1 >=@FaR1";
                    if (FaR1_1.Text.Trim() != "")
                        str += " and FaR1 <=@FaR1_1";
                    if (FaX4no.Text.Trim() != "")
                        str += " and FaX4no >=@FaX4no";
                    if (FaX4no_1.Text.Trim() != "")
                        str += " and FaX4no <=@FaX4no_1";
                    if (FaName1.Text.Trim() != "")
                        str += " and FaName1 like  @FaName1+'%'";
                    if (FaIme.Text.Trim() != "")
                        str += " and FaIme like  @FaIme+'%'";
                    if (FaTel1.Text.Trim() != "")
                        str += " and FaTel1 like @FaTel1+'%'";
                    if (FaAddr1.Text.Trim() != "")
                        str += " and FaAddr1 like '%'+@FaAddr1+'%'";
                    if (FaMemo1.Text.Trim() != "")
                        str += " and FaMemo1 like '%'+@FaMemo1+'%'";
                    if (FaUdf1.Text.Trim() != "")
                        str += " and FaUdf1 like '%'+@FaUdf1+'%'";
                    cmd.CommandText = str;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.Clear();
                    da.Fill(dt);
                    da.Dispose();
                }

                if (dt.Rows.Count > 0)
                {
                    if (this.Tag.ToString() == "廠商資料瀏覽")
                    {
                        //using (var frm = new FrmFactInfob())
                        //{ 
                        //    frm.dt = dt;
                        //    frm.ShowDialog();
                        //}
                        this.OpemInfoFrom<FrmFactInfob>(() =>
                            {
                                FrmFactInfob frm = new FrmFactInfob();
                                frm.dt = dt;
                                return frm;
                            });
                    }
                    else if (this.Tag.ToString() == "廠商郵遞標簽")
                    {
                        using (var frm = new FrmPrint_Fb())
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

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void FaX12no_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX12>(sender);
        }

        private void FaEmno1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void FaX4no_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX04>(sender);
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F11) 
                this.Dispose();

            return base.ProcessCmdKey(ref msg, keyData);
        } 
    }
}
