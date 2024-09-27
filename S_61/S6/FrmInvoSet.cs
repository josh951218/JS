using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JBS;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S6
{
    /// <summary>
    /// 20151006-作誠修改
    /// 修改內容為:隱藏欄位(蓋電章/發票機埠/錢櫃連接埠/客顯連接埠)
    /// 修改原因:因為多工作站環境下，不可能用一共用設定，符合所有工作站環境，如:發票機PORT
    /// 再者目前列印[2/3聯發票]僅在銷貨單列印有功能，而銷貨單列印[2/3聯發票]也未使用上述隱藏欄位資訊
    /// </summary>
    public partial class FrmInvoSet : Formbase
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        

        public FrmInvoSet()
        {
            InitializeComponent(); 

            cn1.ConnectionString = Common.sqlConnString;

            lblMsg.Text += " 一列可打印24個字元，即12個中文字" + Environment.NewLine;
            lblMsg1.Text += Environment.NewLine + "N為參數(設定長度)" + Environment.NewLine;
            lblMsg1.Text += "Line(二聯)/Line(三聯): 列印一橫線" + Environment.NewLine;
            lblMsg1.Text += "西元DateTime(): 西元日期與時間" + Environment.NewLine;
            lblMsg1.Text += "民國DateTime(): 民國日期與時間" + Environment.NewLine;
            lblMsg1.Text += "ItNo(N): 產品編號" + Environment.NewLine;
            lblMsg1.Text += "ItName(N): 品名規格" + Environment.NewLine;
            lblMsg1.Text += "Qty(N): 數量" + Environment.NewLine;
            lblMsg1.Text += "Price(N): 單價" + Environment.NewLine;
            lblMsg1.Text += "TaxPrice(N): 稅前單價" + Environment.NewLine;
            lblMsg1.Text += "Mny(N): 單筆金額" + Environment.NewLine;
            lblMsg1.Text += "Space(N): 空白字元" + Environment.NewLine;
            lblMsg2.Text += Environment.NewLine;
            lblMsg2.Text += "iTitle(): 公司抬頭" + Environment.NewLine;
            lblMsg2.Text += "iStore(): 店號" + Environment.NewLine;
            lblMsg2.Text += "iUnno(): 統一編號" + Environment.NewLine;
            lblMsg2.Text += "iTaxNo(): 稅籍編號" + Environment.NewLine;
            lblMsg2.Text += "iTel(): 電話" + Environment.NewLine;
            lblMsg2.Text += "iAddress(): 地址" + Environment.NewLine;
            lblMsg2.Text += "iMemo1(): 備註一" + Environment.NewLine;
            lblMsg2.Text += "iMemo2(): 備註二" + Environment.NewLine;
            lblMsg2.Text += "Machine(): 機台號碼" + Environment.NewLine;
            lblMsg2.Text += "Page(): 顯示頁碼" + Environment.NewLine;
            lblMsg2.Text += "CuUnno(): 客戶統一編號" + Environment.NewLine;
            lblMsg2.Text += "GetCount(N): 銷售總筆數" + Environment.NewLine;
            lblMsg2.Text += "GetTotal(N): 銷售總金額" + Environment.NewLine;
            lblMsg2.Text += "Cash(N): 收現金額" + Environment.NewLine;
            lblMsg2.Text += "CardMny(N): 刷卡金額" + Environment.NewLine;
            lblMsg2.Text += "GetPrvAcc(N): 扣儲值" + Environment.NewLine;
            lblMsg2.Text += "Change(N): 找零金額" + Environment.NewLine;
            lblMsg2.Text += "KiTax1(N): 未稅" + Environment.NewLine;
            lblMsg2.Text += "KiTax2(N): 免稅" + Environment.NewLine;
            lblMsg2.Text += "Tax(N): 稅額" + Environment.NewLine;

            //選單與主要表單
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = MainForm.menu.Size;
        }

        private void FrmInvoSet_Load(object sender, EventArgs e)
        {
            this.Location = new Point(1, 1);
            btnSave.Enabled = btnCancel.Enabled = false;

            LoadDB();
            btnModify.Focus();
        }

        void LoadDB()
        {
            dt.Clear();
            da1.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dr = dt.AsEnumerable().ToList().Find(r => r["inno"].ToString() == "N1");
                WriteToText(dr);
            }
            else
            {
                WriteToText(null);
            }
        }

        void WriteToText(DataRow row)
        {
            if (row != null)
            {
                row = dt.AsEnumerable().ToList().Find(r => r["inno"].ToString() == "N1");
                TextBox1.Text = row["inhead"].ToString();
                Detail1.Text = row["indital1"].ToString();
                Detail2.Text = row["indital21"].ToString();
                TextBox4.Text = row["infoot"].ToString();
                t1.Text = row["inheadnum"].ToString();
                t2.Text = row["jump"].ToString();
                t3.Text = row["inditalnum"].ToString();
                t4.Text = row["infootnum"].ToString();
                radio1.Checked = row["insignet"].ToDecimal() == 1;
                radio2.Checked = row["insignet"].ToDecimal() == 2;
                if (Common.User_ScInvDev == 1) radio3.Checked = true;
                else if (Common.User_ScInvDev == 2) radio4.Checked = true;
                else radio5.Checked = true;

                if (Common.User_ScInvDevs == 1) radio6.Checked = true;
                else if (Common.User_ScInvDevs == 2) radio7.Checked = true;
                else if (Common.User_ScInvDevs == 3) radio8.Checked = true;
                else radio9.Checked = true;

                if (Common.User_ScPriDev == 1) radio10.Checked = true;
                else if (Common.User_ScPriDev == 2) radio11.Checked = true;
                else radio12.Checked = true;
            }
            else
            {
                radio5.Checked = true;
                radio9.Checked = true;
                radio12.Checked = true;
                TextBox1.Text = "";
                Detail1.Text = "";
                Detail2.Text = "";
                TextBox4.Text = "";
                t1.Text = t2.Text = t3.Text = t4.Text = "0";
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            t1.Text = TextBox1.Lines.Length.ToString();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            t4.Text = TextBox4.Lines.Length.ToString();
        }

        private void t2_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            TextBox tx = sender as TextBox;
            decimal d = 0;
            if (!decimal.TryParse(tx.Text, out d))
            {
                e.Cancel = true;
                MessageBox.Show("只能輸入數字", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (tx.Text.ToDecimal() < 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("數字必須大於等於0", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            if (TextBox1.ReadOnly) return;
            ((Control)sender).BackColor = Color.GreenYellow;
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            if (TextBox1.ReadOnly) return;
            ((RichTextBox)sender).BackColor = Color.FromArgb(243, 243, 243);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            TextBox1.ReadOnly = false;
            Detail1.ReadOnly = false;
            Detail2.ReadOnly = false;
            TextBox4.ReadOnly = false;
            t2.ReadOnly = t3.ReadOnly = false;

            btnSave.Enabled = btnCancel.Enabled = true;
            btnModify.Enabled = btnExit.Enabled = false;

            TextBox1.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            dr["inheadnum"] = t1.Text.ToDecimal();
            dr["jump"] = t2.Text.ToDecimal();
            dr["inditalnum"] = t3.Text.ToDecimal();
            dr["infootnum"] = t4.Text.ToDecimal();

            dr["inhead"] = TextBox1.Text;
            dr["indital1"] = Detail1.Text;
            dr["indital21"] = Detail2.Text;
            dr["infoot"] = TextBox4.Text;

            dr["insignet"] = radio1.Checked ? "1" : "2";
            dr.EndEdit();

            da1.Update(dt);
            btnCancel_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            WriteToText(dr);
            btnModify.Enabled = btnExit.Enabled = true;
            TextBox1.ReadOnly = true;
            Detail1.ReadOnly = true;
            Detail2.ReadOnly = true;
            TextBox4.ReadOnly = true;
            t2.ReadOnly = t3.ReadOnly = true;
            btnModify.Focus();
            btnSave.Enabled = btnCancel.Enabled = false;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btntPrnt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要測試列印?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }


            string sql = "select TOP 1 SaNo from sale order by SaNo DESC";
            DataTable table = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn1))
            {
                table.Clear();
                da.Fill(table);
            }
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("請先新增一筆銷貨單據，才能測試列印！");
                return;
            }
            else
            {
                var p = table.Rows[0][0].ToString();
                sql = "select * from saled left join sale on saled.sano=sale.sano and sale.sano='" + p.Trim() + "'";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn1))
                {
                    table = new DataTable();
                    da.Fill(table);
                }
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("銷貨單據無明細無法測試列印！");
                    return;
                }
                else
                {
                    InvoPrint inv = new InvoPrint();
                    inv.dt = table.Copy();
                    inv.doPrint();
                }
            }





            //decimal d = 14938240;
            //string ivno = "FJ14938240";
            //try
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        ivno = "FJ" + (d).ToString("f0");
            //        MessageBox.Show(ivno);
            //        string sql = "select sale.invno,sale.invtaxno,sale.appdate,saled.* from saled left join sale on saled.sano = sale.sano where 0=0 and invno = '" + ivno + "' order by invno";

            //        DataTable dt = new DataTable();
            //        using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            //        {
            //            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            //            {
            //                da.Fill(dt);
            //            }
            //        }
            //        if (dt.Rows.Count > 0)
            //        {
            //            var machine = dt.Rows[0]["seno"].ToString().Trim();
            //            var unno = dt.Rows[0]["invtaxno"].ToString().Trim();
            //            InvoPrint inv = new InvoPrint();
            //            inv.dt = dt.Copy();
            //            inv.Machine = machine;
            //            inv.CuUnno = unno;
            //            Common.iMemo1 = dt.Rows[0]["appdate"].ToString().Trim();
            //            inv.doPrint();
            //        }
            //        d = d + 1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}








            //DataTable dt = new DataTable();
            //string ivno = "FJ14944292";
            //string sql = "select sale.invno,sale.invtaxno,sale.appdate,saled.* from saled left join sale on saled.sano = sale.sano where 0=0 and invno = '" + ivno + "' order by invno";
            //try
            //{
            //    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            //    {
            //        using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            //        {
            //            da.Fill(dt);
            //        }
            //    }
            //    if (dt.Rows.Count > 0)
            //    {
            //        var machine = dt.Rows[0]["seno"].ToString().Trim();
            //        var unno = dt.Rows[0]["invtaxno"].ToString().Trim();
            //        InvoPrint inv = new InvoPrint();
            //        inv.dt = dt.Copy();
            //        inv.Machine = machine;
            //        inv.CuUnno = unno;
            //        Common.iMemo1 = dt.Rows[0]["appdate"].ToString().Trim();
            //        inv.doPrint();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void Recover1_Click(object sender, EventArgs e)
        {
            DataRow row = dt.AsEnumerable().ToList().Find(r => r["inno"].ToString() == "N2");
            if (row != null)
            {
                dr["inheadnum"] = row["inheadnum"];
                dr["inditalnum"] = row["inditalnum"];
                dr["infootnum"] = row["infootnum"];
                dr["intotnum"] = row["intotnum"];
                dr["inhead"] = row["inhead"];
                dr["indital1"] = row["indital1"];
                dr["indital21"] = row["indital21"];
                dr["infoot"] = row["infoot"];
                dr["insignet"] = radio1.Checked ? "1" : "2";
            }
            da1.Update(dt);
            LoadDB();
        }

        private void Recover2_Click(object sender, EventArgs e)
        {
            DataRow row = dt.AsEnumerable().ToList().Find(r => r["inno"].ToString() == "N3");
            if (row != null)
            {
                dr["inheadnum"] = row["inheadnum"];
                dr["inditalnum"] = row["inditalnum"];
                dr["infootnum"] = row["infootnum"];
                dr["intotnum"] = row["intotnum"];
                dr["inhead"] = row["inhead"];
                dr["indital1"] = row["indital1"];
                dr["indital21"] = row["indital21"];
                dr["infoot"] = row["infoot"];
                dr["insignet"] = radio1.Checked ? "1" : "2";
            }
            da1.Update(dt);
            LoadDB();
        }

        private void TextBox1_ReadOnlyChanged(object sender, EventArgs e)
        {
            Recover1.Enabled = Recover2.Enabled = !TextBox1.ReadOnly;
            radio1.Enabled = radio2.Enabled = radio3.Enabled = radio4.Enabled = radio5.Enabled = radio6.Enabled = radio7.Enabled = radio8.Enabled = radio9.Enabled = radio10.Enabled = radio11.Enabled = radio12.Enabled = !TextBox1.ReadOnly;
        }

        private void radio1_CheckedChanged(object sender, EventArgs e)
        {
            lbl1.BackColor = radio1.Checked ? Color.LightBlue : Color.Transparent;
            lbl2.BackColor = radio2.Checked ? Color.LightBlue : Color.Transparent;
        }

        private void radio3_CheckedChanged(object sender, EventArgs e)
        {
            lbl3.BackColor = radio3.Checked ? Color.LightBlue : Color.Transparent;
            lbl4.BackColor = radio4.Checked ? Color.LightBlue : Color.Transparent;
            lbl5.BackColor = radio5.Checked ? Color.LightBlue : Color.Transparent;

        }

        private void radio6_CheckedChanged(object sender, EventArgs e)
        {
            lbl6.BackColor = radio6.Checked ? Color.LightBlue : Color.Transparent;
            lbl7.BackColor = radio7.Checked ? Color.LightBlue : Color.Transparent;
            lbl8.BackColor = radio8.Checked ? Color.LightBlue : Color.Transparent;
            lbl9.BackColor = radio9.Checked ? Color.LightBlue : Color.Transparent;
        }

        private void radio10_CheckedChanged(object sender, EventArgs e)
        {
            lbl10.BackColor = radio10.Checked ? Color.LightBlue : Color.Transparent;
            lbl11.BackColor = radio11.Checked ? Color.LightBlue : Color.Transparent;
            lbl12.BackColor = radio12.Checked ? Color.LightBlue : Color.Transparent;
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            radio1.Checked = lbl1.Name == lbl.Name;
            radio2.Checked = lbl2.Name == lbl.Name;
        }

        private void lbl3_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            radio3.Checked = lbl3.Name == lbl.Name;
            radio4.Checked = lbl4.Name == lbl.Name;
            radio5.Checked = lbl5.Name == lbl.Name;
        }

        private void lbl6_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            radio6.Checked = lbl6.Name == lbl.Name;
            radio7.Checked = lbl7.Name == lbl.Name;
            radio8.Checked = lbl8.Name == lbl.Name;
            radio9.Checked = lbl9.Name == lbl.Name;
        }

        private void lbl10_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            radio10.Checked = lbl10.Name == lbl.Name;
            radio11.Checked = lbl11.Name == lbl.Name;
            radio12.Checked = lbl12.Name == lbl.Name;
        }

        

        





















    }
}
