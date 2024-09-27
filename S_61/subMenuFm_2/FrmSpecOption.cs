using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmSpecOption : Formbase
    {
        public decimal qty = 0;
        public decimal prs = 1;
        public decimal price = 0;
        public decimal point = 0;

        public string reason = "無";
        public string groupid = "";
        public decimal singleprice = 0;

        public string SpTrait = "";
        public string SpTraitName = "";
        public List<TextBox> list;

        List<String> BigNumber = new List<string>() { "", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

        public FrmSpecOption(string itno, string itname, decimal itprice)
        {
            InitializeComponent();
            list = new List<TextBox>() { t1, t2, t3, t3p, t4, t4p, t5, t5p, t6, t6p, t7, t7p, t8, t8p };

            ItNo.Text = itno;
            ItName.Text = itname;
            ItPrice.Text = itprice.ToString("f" + Common.MS);
        }

        private void FrmSpecOption_Load(object sender, EventArgs e)
        {
        }

        private void FrmSpecOption_Shown(object sender, EventArgs e)
        {
            radioT1.Checked = true;
            t1.Focus();
        }

        private void RADIO_CheckedChanged(object sender, EventArgs e)
        {
            if (sender.Equals(radioT1))
            {
                SpTrait = "1";
                SpTraitName = "特價";
                list.ForEach(t => { t.ReadOnly = true; t.Clear(); });
                t1.ReadOnly = false;
                t1.Focus();
            }
            else if (sender.Equals(radioT2))
            {
                SpTrait = "2";
                SpTraitName = "折數";
                list.ForEach(t => { t.ReadOnly = true; t.Clear(); });
                t2.ReadOnly = false;
                t2.Focus();
            }
            else if (sender.Equals(radioT3))
            {
                SpTrait = "3";
                SpTraitName = "第N件打折";
                list.ForEach(t => { t.ReadOnly = true; t.Clear(); });
                t3.ReadOnly = t3p.ReadOnly = false;
                t3.Focus();
            }
            else if (sender.Equals(radioT4))
            {
                SpTrait = "4";
                SpTraitName = "買N件打折";
                list.ForEach(t => { t.ReadOnly = true; t.Clear(); });
                t4.ReadOnly = t4p.ReadOnly = false;
                t4.Focus();
            }
            else if (sender.Equals(radioT5))
            {
                //SpTrait = "5";
                //SpTraitName = "特價.扣紅利";
                //list.ForEach(t => { t.ReadOnly = true; t.Clear(); });
                //t5.ReadOnly = t5p.ReadOnly = false;
                //t5.Focus();
            }
            else if (sender.Equals(radioT6))
            {
                SpTrait = "6";
                SpTraitName = "加購商品";
                list.ForEach(t => { t.ReadOnly = true; t.Clear(); });
                t6.ReadOnly = t6p.ReadOnly = false;
                t6.Focus();
            }
            else if (sender.Equals(radioT7))
            {
                SpTrait = "7";
                SpTraitName = "買N送N";
                list.ForEach(t => { t.ReadOnly = true; t.Clear(); });
                t7.ReadOnly = t7p.ReadOnly = false;
                t7.Focus();
            }
            else if (sender.Equals(radioT8))
            {
                SpTrait = "8";
                SpTraitName = "混搭商品";
                list.ForEach(t => { t.ReadOnly = true; t.Clear(); });
                t8.ReadOnly = t8p.ReadOnly = true;
                t8.Focus();
            }
        }

        private void t8_DoubleClick(object sender, EventArgs e)
        {
            if (radioT8.Checked)
            {
                using (SOther.FrmSpecialGroup frm = new SOther.FrmSpecialGroup(true))
                {
                    if (DialogResult.OK == frm.ShowDialog())
                    {
                        t8.Text = ((decimal)frm.qty).ToString();
                        t8p.Text = ((decimal)frm.price).ToString();
                        GroupID.Text = frm.groupid;
                        SinglePrice.Text = ((decimal)frm.singleprice).ToString();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (radioT1.Checked)
            {
                price = t1.Text.ToDecimal();
                if (price <= 0)
                {
                    MessageBox.Show("特價不可為零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t1.Focus();
                    return;
                }
            }
            else if (radioT2.Checked)
            {
                prs = t2.Text.ToDecimal();
                if (prs <= 0)
                {
                    MessageBox.Show("折數不可小於零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t2.Focus();
                    return;
                }
                if (prs > 1)
                {
                    MessageBox.Show("折數不可大於1！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t2.Focus();
                    return;
                }
            }
            else if (radioT3.Checked)
            {
                qty = t3.Text.ToDecimal();
                prs = t3p.Text.ToDecimal();
                if (prs <= 0)
                {
                    MessageBox.Show("折數不可小於零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t3p.Focus();
                    return;
                }
                if (prs > 1)
                {
                    MessageBox.Show("折數不可大於1！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t3p.Focus();
                    return;
                }
            }
            else if (radioT4.Checked)
            {
                qty = t4.Text.ToDecimal();
                prs = t4p.Text.ToDecimal();
                if (prs <= 0)
                {
                    MessageBox.Show("折數不可小於零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t4p.Focus();
                    return;
                }
                if (prs > 1)
                {
                    MessageBox.Show("折數不可大於1！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t4p.Focus();
                    return;
                }
            }
            else if (radioT5.Checked)
            {
                price = t5.Text.ToDecimal();
                point = t5p.Text.ToDecimal("f0");
            }
            else if (radioT6.Checked)
            {
                if (t6.Text.Trim().ToDecimal() == 0 || t6p.Text.Trim().ToDecimal() == 0)
                {
                    MessageBox.Show("價格不可為零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t6.Focus();
                    return;
                }
                reason = t6.Text.Trim();
                price = t6p.Text.ToDecimal("f0");

                SpTraitName += "(滿" + reason + "元)";
            }
            else if (radioT7.Checked)
            {
                if (t7.Text.Trim().ToInteger() == 0 || t7p.Text.Trim().ToInteger() == 0)
                {
                    MessageBox.Show("商品件數不可為零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t7.Focus();
                    return;
                }
                reason = t7.Text.Trim();
                qty = t7p.Text.ToInteger();

                var totqty = reason.ToInteger();
                var str1 = "";
                var str2 = "";

                if (totqty == 10)
                {
                    str1 = "十";
                }
                else if (totqty > 0 && totqty % 10 == 0)
                {
                    str1 = BigNumber.ElementAt(totqty / 10) + "十";
                }
                else if (10 < totqty && totqty < 20)
                {
                    str1 = "十" + BigNumber.ElementAt(totqty % 10);
                }
                else if (totqty > 20)
                {
                    str1 = BigNumber.ElementAt(totqty / 10) + "十" + BigNumber.ElementAt(totqty % 10);
                }
                else
                {
                    str1 = BigNumber.ElementAt(totqty);
                }



                if (qty == 10)
                {
                    str2 = "十";
                }
                else if (qty > 0 && qty % 10 == 0)
                {
                    str2 = BigNumber.ElementAt((int)qty / 10) + "十";
                }
                else if (10 < qty && qty < 20)
                {
                    str2 = "十" + BigNumber.ElementAt((int)qty % 10);
                }
                else if (qty > 20)
                {
                    str2 = BigNumber.ElementAt((int)qty / 10) + "十" + BigNumber.ElementAt((int)qty % 10);
                }
                else
                {
                    str2 = BigNumber.ElementAt((int)qty);
                }

                SpTraitName = "買" + str1 + "送" + str2;
            }
            else if (radioT8.Checked)
            {
                if (t8.Text.Trim().ToInteger() == 0 || t8p.Text.Trim().ToDecimal() == 0)
                {
                    MessageBox.Show("商品件數或價格不可為零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t8.Focus();
                    return;
                }

                reason = t8.Text.Trim();
                qty = t8.Text.Trim().ToDecimal("f0");
                price = t8p.Text.Trim().ToDecimal("f0");
                groupid = GroupID.Text.Trim();
                singleprice = SinglePrice.Text.ToDecimal("f0");

                SpTraitName += "(" + groupid + ")";
            }
            this.DialogResult = DialogResult.OK;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                btnSave_Click(btnGet, null);
                return true;
            }
            else if (keyData == Keys.F4)
            {
                btnCancel_Click(btnExit, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
