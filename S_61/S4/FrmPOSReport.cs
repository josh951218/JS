using System;
using JE.MyControl;

namespace S_61.S4
{
    public partial class FrmPOSReport : Formbase
    {
        public FrmPOSReport()
        {
            InitializeComponent();
        }

        private void FrmPOSReport_Load(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (radioT1.Checked)
            {
                MainForm.menu.OpenForm<FrmPOSSale>("");
            }
            else if (radioT2.Checked)
            {
                MainForm.menu.OpenForm<FrmPOSItemPrs>("");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void labelMenuT1_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmMoneyBoxLog>("");
        }

        private void labelMenuT2_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNEmpl_Rpt>("");
        }

        private void labelMenuT3_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNMachineSale_Rpt>("");
        }

        private void labelMenuT4_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNFactCMS_Rpt>("");
        }

        private void labelMenuT5_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNSale_Rpt>("");
        }

        private void labelMenuT6_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNItem_Rpt>("");
        }

        private void labelMenuT7_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNKind_Rpt>("");
        }

        private void labelMenuT8_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNTime_Rpt>("");
        }

        private void labelMenuT9_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmNSaleAvg>("");
        }

        private void labelMenuT10_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S0.FrmInv401_Rpt>("");
        }
    }
}
