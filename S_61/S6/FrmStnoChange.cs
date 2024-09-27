using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace JBS.S6
{
    public partial class FrmStnoChange : Formbase
    {
        JBS.JS.xEvents xe;

        SqlTransaction tran;

        public FrmStnoChange()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BStNo.Text.Trim() == "" || NStNo.Text.Trim() == "")
            {
                MessageBox.Show("倉庫編號不能為空", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("請確定是否更新編號", "訊息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                return; BStNo.Enabled = NStNo.Enabled = false;
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
                        cmd.Parameters.AddWithValue("@NStNo", NStNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@BStNo", BStNo.Text.Trim());
                        cmd.CommandText = "";

                        cmd.CommandText += "Update adjust            set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update adjustd           set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update bestock           set stno=@NStNo where stno=@BStNo;";//?
                        cmd.CommandText += "Update borr              set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update borrd             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update bshop             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update bshopd            set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update CylinderIn        set stno=@NStNo where stno=@BStNo;";//?
                        cmd.CommandText += "Update CylinderInd       set stno=@NStNo where stno=@BStNo;";//?
                        cmd.CommandText += "Update CylinderOut       set stno=@NStNo where stno=@BStNo;";//?
                        cmd.CommandText += "Update CylinderOutd      set stno=@NStNo where stno=@BStNo;";//?
                        cmd.CommandText += "Update InOutD            set stno=@NStNo where stno=@BStNo;";
                        cmd.CommandText += "Update InStk             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update InStkD            set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update Iv                set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update IvD               set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update lend              set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update lendd             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update OuStk             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update OuStkD            set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update OutInD            set stno=@NStNo where stno=@BStNo;";
                        cmd.CommandText += "Update RBorr             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update RBorrD            set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update rlend             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update rlendd            set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update rsale             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update rsaled            set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update rshop             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update rshopd            set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update sale              set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update saled             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update saledTemp         set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update saleTemp          set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update SaveStock         set stno=@NStNo where stno=@BStNo;";//?
                        cmd.CommandText += "Update SaveStockD        set stno=@NStNo where stno=@BStNo;";//?
                        cmd.CommandText += "Update scrit             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update stkcost           set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update StkDetail         set stno=@NStNo where stno=@BStNo;";//?
                        cmd.CommandText += "Update stkroom           set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update stock             set stno=@NStNo where stno=@BStNo;";//
                        cmd.CommandText += "Update stock_D           set stno=@NStNo where stno=@BStNo;";
                        cmd.CommandText += "Update Stocktaking       set stno=@NStNo where stno=@BStNo;";
                        cmd.CommandText += "Update Stocktakingd      set stno=@NStNo where stno=@BStNo;";
                        cmd.CommandText += "Update TempRsaleD        set stno=@NStNo where stno=@BStNo;";
                        cmd.CommandText += "Update TempSaleD         set stno=@NStNo where stno=@BStNo;";
                        cmd.CommandText += "Update Tempstock         set stno=@NStNo where stno=@BStNo;";
                        cmd.CommandText += "Update turshop           set stno=@NStNo where stno=@BStNo;";

                        cmd.CommandText += "Update allot             set stnoi=@NStNo where stnoi=@BStNo;";
                        cmd.CommandText += "Update allot             set stnoo=@NStNo where stnoo=@BStNo;";
                        cmd.CommandText += "Update allotd            set stnoi=@NStNo where stnoi=@BStNo;";
                        cmd.CommandText += "Update allotd            set stnoo=@NStNo where stnoo=@BStNo;";
                        cmd.CommandText += "Update draw              set stnoi=@NStNo where stnoi=@BStNo;";
                        cmd.CommandText += "Update draw              set stnoo=@NStNo where stnoo=@BStNo;";
                        cmd.CommandText += "Update drawd             set stnoi=@NStNo where stnoi=@BStNo;";
                        cmd.CommandText += "Update drawd             set stnoo=@NStNo where stnoo=@BStNo;";
                        cmd.CommandText += "Update garner            set stnoi=@NStNo where stnoi=@BStNo;";
                        cmd.CommandText += "Update garner            set stnoo=@NStNo where stnoo=@BStNo;";
                        cmd.CommandText += "Update garnerd           set stnoi=@NStNo where stnoi=@BStNo;";
                        cmd.CommandText += "Update garnerd           set stnoo=@NStNo where stnoo=@BStNo;";


                        cmd.ExecuteNonQuery();
                        tran.Commit(); tran.Dispose();
                        MessageBox.Show("編號修改成功", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        BStNo.Text = StName.Text = NStNo.Text = "";
                        BStNo.Focus();

                    }

                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ex.ToString();
            }
            BStNo.Enabled = NStNo.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void BStNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender, row =>
            {
                BStNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }

        private void BStNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused || BStNo.Text.Trim() == "")
                return;

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                BStNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
             
        }

        private void NStNo_Validating(object sender, CancelEventArgs e)
        {
            if (NStNo.Text.Trim() == "" || btnCancel.Focused) return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@NStNo", NStNo.Text.Trim());
                    cmd.CommandText = "Select stno,stname from stkroom where stno=@NStNo";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            e.Cancel = true;
                            MessageBox.Show("此倉庫編號已經存在", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                }
            }

        }

        private void FrmStnoChange_Load(object sender, EventArgs e)
        {

        }
    }
}
