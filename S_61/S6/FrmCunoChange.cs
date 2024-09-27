using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace JBS.S6
{
    public partial class FrmCunoChange : Formbase
    {
        JBS.JS.xEvents xe;
        SqlTransaction tran;
        public FrmCunoChange()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BCuNo.Text.Trim() == "" || NCuNo.Text.Trim() == "")
            {
                MessageBox.Show("客戶編號不能為空", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("請確定是否更新編號", "訊息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) return;
            BCuNo.Enabled = NCuNo.Enabled = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        tran = cn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.Parameters.AddWithValue("@BCuNo", BCuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@NCuNo", NCuNo.Text.Trim());
                        cmd.CommandText = "";
                        cmd.CommandText += " Update  cust            set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  CylinderIn      set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  CylinderInd     set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  discount        set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  discountd       set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  InStk           set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  InStkD          set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  lend            set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  lendd           set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  LSCust          set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  nullify         set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  dbo.[order]     set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  orderd          set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  OuStk           set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  OuStkD          set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  OutIn           set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  quote           set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  quoted          set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  receiv          set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  receivd         set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  rlend           set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  rlendd          set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  rsale           set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  rsaled          set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  sale            set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  saled           set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  saledTemp       set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  saleTemp        set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  SaveStock       set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  SaveStockD      set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  scrit           set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  TempOrder       set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  TempOrderD      set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  TempQuoteD      set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  TempRsaleD      set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  TempSaleD       set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  turshop         set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  webcust         set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  weborder        set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  weborderd       set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  DeliveryAddress set cuno=@NCuNo where cuno=@BCuNo;";
                        cmd.CommandText += " Update  sale            set payerno=@NCuNo where payerno=@BCuNo;";
                        cmd.CommandText += " Update  rsale           set payerno=@NCuNo where payerno=@BCuNo;";
                        cmd.CommandText += " Update  receivd         set payerno=@NCuNo where payerno=@BCuNo;";



                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        MessageBox.Show("編號修改完畢", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        BCuNo.Text = CuName2.Text = CuTel1.Text = CuPer1.Text = CuAddr1.Text = NCuNo.Text = "";
                        BCuNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ex.ToString();
            }
            BCuNo.Enabled = NCuNo.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void BCuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender, reader =>
            {
                BCuNo.Text = reader["CuNo"].ToString();
                CuName2.Text = reader["CuName2"].ToString();
                CuTel1.Text = reader["CuTel1"].ToString();
                CuPer1.Text = reader["CuPer1"].ToString();
                CuAddr1.Text = reader["CuAddr1"].ToString();
            });
        }

        private void BCuNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            if (BCuNo.TrimTextLenth() == 0)
            {
                BCuNo.Text = "";
                CuName2.Text = "";
                CuTel1.Text = "";
                CuPer1.Text = "";
                CuAddr1.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Cust>(sender, e, reader =>
            {
                BCuNo.Text = reader["CuNo"].ToString();
                CuName2.Text = reader["CuName2"].ToString();
                CuTel1.Text = reader["CuTel1"].ToString();
                CuPer1.Text = reader["CuPer1"].ToString();
                CuAddr1.Text = reader["CuAddr1"].ToString();
            }, true); 
        }

        private void NCuNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused || NCuNo.Text.Trim() == "")
                return;
            
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@NCuNo", NCuNo.Text.Trim());
                    cmd.CommandText = "select cuno,cuname2 from cust where cuno=@NCuNo";

                    if (cmd.ExecuteScalar().IsNotNull())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此客戶編號已經存在", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                }
            }
        }

        private void FrmCunoChange_Load(object sender, EventArgs e)
        {

        }




    }
}
