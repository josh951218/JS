using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.FoodCloud
{
    public partial class BatchInfo : Formbase
    {
        BatchDocument BatchD = new BatchDocument();
        public BatchInfo()
        {
            InitializeComponent();
            if (Common.User_DateTime == 1)
            {
                date.MaxLength = date1.MaxLength = 7;
                date.Text = Date.GetDateTime(1, false).Remove(5) + "01";
                date1.Text = Date.GetDateTime(1, false);
                Manufacturingdate.MaxLength = 7;
                Manufacturingdate1.MaxLength = 7;
                ExpiryDate.MaxLength = 7;
                ExpiryDate1.MaxLength = 7;
            }
            else
            {
                date.MaxLength = date1.MaxLength = 8;
                date.Text = Date.GetDateTime(2, false).Remove(6) + "01";
                date1.Text = Date.GetDateTime(2, false);
                Manufacturingdate.MaxLength = 8;
                Manufacturingdate1.MaxLength = 8;
                ExpiryDate.MaxLength = 8;
                ExpiryDate1.MaxLength = 8;
            }
        }

        private void date_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            BatchD.DateValidate(sender, e);
        }

        private void itno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            BatchD.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                (sender as TextBox).Text = row["itno"].ToString().Trim();
            }, true);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (date.Text.ToDecimal() > date1.Text.ToDecimal())
            {
                MessageBox.Show("起始日期不得大於終止日期!");
                date.Focus();
                return;
            }
            if (itno.Text.BigThen(itno1.Text))
            {
                MessageBox.Show("起始產品編號不可大於終止產品編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                itno.Focus();
                return;
            }
            if (fano.Text.BigThen(fano1.Text))
            {
                MessageBox.Show("起始廠商編號不可大於終止產品編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fano.Focus();
                return;
            }
            if (BatchNo.Text.BigThen(BatchNo1.Text))
            {
                MessageBox.Show("起始批次編號不可大於終止批次編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BatchNo.Focus();
                return;
            }
            if (Manufacturingdate.Text.BigThen(Manufacturingdate1.Text))
            {
                MessageBox.Show("起始製造日期不可大於終止製造日期，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Manufacturingdate.Focus();
                return;
            }
            if (ExpiryDate.Text.BigThen(ExpiryDate1.Text))
            {
                MessageBox.Show("起始有效日期不可大於終止有效日期，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ExpiryDate.Focus();
                return;
            }
            if (Stno.Text.BigThen(Stno1.Text))
            {
                MessageBox.Show("起始倉庫編號不可大於終止倉庫編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Stno.Focus();
                return;
            }

            using (BatchInfob frm = new BatchInfob(date, date1, itno, itno1, fano, fano1, BatchNo, BatchNo1, Manufacturingdate, Manufacturingdate1, ExpiryDate, ExpiryDate1, Stno ,Stno1))
            {
                frm.ShowDialog();
            }
        }

        private void itno_DoubleClick(object sender, EventArgs e)
        {
            BatchD.Open<JBS.JS.Item>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Stno_DoubleClick(object sender, EventArgs e)
        {
            BatchD.Open<JBS.JS.Stkroom>(sender);
        }

        private void Stno_Validating(object sender, CancelEventArgs e)
        {
            BatchD.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                (sender as TextBox).Text = row["stno"].ToString().Trim();
            }, true);
        }

        private void BatchNo_DoubleClick(object sender, EventArgs e)
        {
            BatchD.Open<JBS.JS.BatchInformation>(sender);
        }

        private void fano_DoubleClick(object sender, EventArgs e)
        {
            BatchD.Open<JBS.JS.Fact>(sender);
        }

        private void fano_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            BatchD.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                (sender as TextBox).Text = row["fano"].ToString().Trim();
            }, true);
        }

        private void BatchNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            BatchD.ValidateOpen<JBS.JS.BatchInformation>(sender, e, row =>
            {
                (sender as TextBox).Text = row["Batchno"].ToString().Trim();
            }, true);
        }

        private void Manufacturingdate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if ((sender as TextBox).Text !="")
            BatchD.DateValidate(sender, e);
        }
    }
}
