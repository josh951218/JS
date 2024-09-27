using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;

namespace JBS.JS
{
    public class SaleDis : xDocuments
    {
        protected override string MasterName
        {
            get { return "SaleDis"; }
        }

        protected override string KeyName
        {
            get { return "dino"; }
        }

        /// <summary>
        /// 檢查發票號碼是否作廢
        /// </summary>
        internal bool IsInvalid(string inno)
        {
            using (var db = new xSQL())
            {
                var tsql = "Select Count(*) from saleinv where invalid = 1 and inno = @inno";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("inno", inno)).ToDecimal();

                return count > 0;
            }
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
