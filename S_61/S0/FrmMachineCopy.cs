using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace S_61.S0
{
    public partial class FrmMachineCopy : Form
    {
        public string TResult { get; private set; }

        public FrmMachineCopy()
        {
            InitializeComponent();

            if (this.comboBox1.Items.Count > 0)
                this.comboBox1.SelectedIndex = 0;
        }

        private void FrmMachineCopy_Load(object sender, EventArgs e)
        {
            this.TResult = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var number = this.comboBox1.SelectedIndex + 1;
            if (number <= 0)
            {
                MessageBox.Show("請選擇機台!!!");
                return;
            }

            this.TResult = number.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
