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
using System.Data.SqlClient;

namespace S_61.電子發票
{
    public partial class ErrorInvoice : Formbase
    {
        public DataTable tb;
        public ErrorInvoice()
        {
            InitializeComponent();
        }

        private void ErrorInvoice_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = tb;
        }

        private void buttonSmallT1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
