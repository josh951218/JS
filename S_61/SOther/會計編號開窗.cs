using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.subMenuFm_1;
using S_61.SOther;
using System.IO;
using System.Diagnostics;

namespace S_61.SOther
{
    public partial class 會計編號開窗 : Formbase
    {
        public string acno { get; set; }
        public string accono { get; set; }
        public 會計編號開窗()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
        }

        private void 會計編號開窗_Load(object sender, EventArgs e)
        {
            ACNO.Text = acno;
            ACCoNo.Text = accono;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
