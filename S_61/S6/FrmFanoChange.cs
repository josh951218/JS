using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace JBS.S6
{
    public partial class FrmFanoChange : Formbase
    {
        JBS.JS.xEvents xe;
        SqlTransaction tran;

        public FrmFanoChange()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BFaNo.Text.Trim() == "" || NFaNo.Text.Trim() == "")
            {
                MessageBox.Show("廠商編號不可為空", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("請確定是否更新編號", "訊息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) return;
            BFaNo.Enabled = NFaNo.Enabled = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        tran = cn.BeginTransaction();
                        cmd.Transaction = tran;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@BFaNo", BFaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@NFaNo", NFaNo.Text.Trim());
                        cmd.CommandText = "";

                        cmd.CommandText += "Update  Bdiscount         set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  Bdiscountd        set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  borr              set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  borrd             set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  bshop             set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  bshopd            set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  CylinderOut       set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  CylinderOutd      set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  fact              set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  ford              set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  fordd             set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  fquot             set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  fquotd            set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  InOut             set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  item              set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  LSFact            set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  payabl            set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  payabld           set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  RBorr             set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  RBorrD            set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  rshop             set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  rshopd            set fano=@NFaNo where fano=@BFaNo;";
                        cmd.CommandText += "Update  TempOrderDetail   set fano=@NFaNo where fano=@BFaNo;";


                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        MessageBox.Show("編號修改完成", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        BFaNo.Text = FaName2.Text = FaPer1.Text = FaTel1.Text = FaAddr1.Text = NFaNo.Text = "";
                        BFaNo.Focus();

                    }

                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show(ex.ToString());
            }
            BFaNo.Enabled = NFaNo.Enabled = true;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void BFaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender, row =>
            {
                BFaNo.Text = row["FaNo"].ToString();
                FaName2.Text = row["FaName2"].ToString();
                FaPer1.Text = row["FaPer1"].ToString();
                FaTel1.Text = row["FaTel1"].ToString();
                FaAddr1.Text = row["FaAddr1"].ToString();
            }); 
        }

        private void BFaNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (BFaNo.Text.Trim() == "")
            {
                BFaNo.Text = "";
                FaName2.Text = "";
                FaPer1.Text = "";
                FaTel1.Text = "";
                FaAddr1.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                BFaNo.Text = row["FaNo"].ToString();
                FaName2.Text = row["FaName2"].ToString();
                FaPer1.Text = row["FaPer1"].ToString();
                FaTel1.Text = row["FaTel1"].ToString();
                FaAddr1.Text = row["FaAddr1"].ToString();
            });
             
        }

        private void NFaNo_Validating(object sender, CancelEventArgs e)
        {
            if (NFaNo.Text.Trim() == "" || btnCancel.Focused) return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@fano", NFaNo.Text.Trim());
                    cmd.CommandText = "Select fano,faname2 from fact where fano=@fano";

                    if (cmd.ExecuteScalar().IsNotNull())
                    {
                        e.Cancel = true;
                        MessageBox.Show("此廠商編號已經存在", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }



                }
            }
        }

        private void FrmFanoChange_Load(object sender, EventArgs e)
        {

        }

    }
}
