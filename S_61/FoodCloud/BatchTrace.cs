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

namespace S_61
{
    public partial class BatchTrace : Formbase
    {

        public string _Bno { get; set; }
        public string _BatchNo { get; set; }
        public string _FaName { get; set; }
        public string _ItName { get; set; }
        public DataTable dt = new DataTable(); //由上層傳入

        public BatchTrace(string strA) // strA = 追蹤 or 追朔
        {
            InitializeComponent();
            if (strA == "追蹤")
            {
                this.Text = "追蹤";
                this.廠商編號.Visible = false;
                this.廠商簡稱.Visible = false;
            }
            else if (strA == "追溯")
            {
                this.Text = "追溯";
                this.客戶編號.Visible = false;
                this.客戶簡稱.Visible = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dt.Dispose();
            this.Dispose();
        }

        private void BatchTrace_Load(object sender, EventArgs e)
        {
            異動數量.Set庫存數量小數();
            if (Common.User_DateTime == 1)
                西元年.Visible = false;
            else
                民國年.Visible = false;
            ItName.Text = _ItName;
            FaName.Text = _FaName;
            BatchNo.Text = _BatchNo;
            dataGridViewT1.DataSource = dt;
        }

    }
}
