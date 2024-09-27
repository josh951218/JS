using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JBS.JS
{
    public class SaleInv : xDocuments, IxValidate
    {
        protected override string MasterName
        {
            get { return "SaleInv"; }
        }

        protected override string KeyName
        {
            get { return "inno"; }
        }

        public string ValiTable
        {
            get { return "SaleInv"; }
        }

        public string ValiKey
        {
            get { return "inno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new S_61.S2.FrmInvNoOpen(1);
        }

        /// <summary>
        /// 驗證發票號碼格式
        /// </summary>
        internal bool IsInNoFormat(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var InNo = sender as TextBox;
            if (InNo.Text.Trim().Length != 10)
            {
                e.Cancel = true;
                MessageBox.Show(
                    "發票號碼輸入錯誤!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                InNo.SelectAll();
                return false;
            }
            else
            {
                var str = InNo.Text.Trim().ToUpper();
                if (char.IsUpper(str[0]) == false || char.IsUpper(str[1]) == false)
                {
                    e.Cancel = true;
                    MessageBox.Show(
                        "發票號碼輸入錯誤!",
                        "訊息視窗",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    InNo.SelectAll();
                    return false;
                }

                for (int i = 2; i < 10; i++)
                {
                    if (char.IsDigit(str[i]) == false)
                    {
                        e.Cancel = true;
                        MessageBox.Show(
                            "發票號碼輸入錯誤!",
                            "訊息視窗",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        InNo.SelectAll();
                        return false;
                    }
                }
                InNo.Text = str;
            }
            return true;
        }
    }
}
