﻿using System;
using System.Data;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.subMenuFm_1
{
    public partial class FrmItemNotOutb : Formbase
    {
        public DataTable Dt = new DataTable();
        public DataTable DtD = new DataTable();
        public string DateRange = "";

        RPT rp;
        string txtmiddle = "";

        public FrmItemNotOutb()
        {
            InitializeComponent();

            this.售價本幣.Visible = Common.User_SalePrice;
            this.稅前售價本幣.Visible = Common.User_SalePrice;
            this.稅前金額本幣.Visible = Common.User_SalePrice;
            this.售價.Visible = Common.User_SalePrice;
            this.稅前售價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
        }

        private void FrmItemNotOutb_Load(object sender, EventArgs e)
        { 
            this.訂單數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.未交量.DefaultCellStyle.Format = "f" + Common.Q;
            this.售價本幣.DefaultCellStyle.Format = "f" + Common.M;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前售價本幣.DefaultCellStyle.Format = "f" + Common.M;
            this.稅前金額本幣.DefaultCellStyle.Format = "f" + Common.M;
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前售價.DefaultCellStyle.Format = "f6";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.MST;
            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = this.計位.Visible = false;
            }

            //依產品編號交貨日期排序
            Dt = Dt.AsEnumerable().OrderBy(r => r["esdate"].ToString().Trim()).CopyToDataTable();
            Dt = Dt.AsEnumerable().OrderBy(r => r["itno"].ToString().Trim()).CopyToDataTable();

            //產品類別轉為文字
            int num = 0;
            foreach (DataRow i in Dt.Rows)
            {
                string itt = i["ittrait"].ToString().Trim();
                if (itt == "3") i["產品分類"] = "單一商品";
                if (itt == "2") i["產品分類"] = "組裝品";
                if (itt == "1") i["產品分類"] = "組合品";
                i["序號"] = num.ToString();
                num++;
            }
            num = 0;
            foreach (DataRow i in DtD.Rows)
            {
                string itt = i["ittrait"].ToString().Trim();
                if (itt == "3") i["產品分類"] = "單一商品";
                if (itt == "2") i["產品分類"] = "組裝品";
                if (itt == "1") i["產品分類"] = "組合品";
                i["序號"] = num.ToString();
                num++;
            }

            WriteToTitle();
        }

        void WriteToTitle()
        {
            textBoxT1.Text = Dt.Rows[0]["cuno"].ToString();
            textBoxT2.Text = Dt.Rows[0]["itno"].ToString();
            dataGridViewT1.DataSource = Dt;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void paramsInit()
        {
            string path = "";
            string HaveO = "";
            string HaveBom = "";

            if (radioT7.Checked) HaveO = "O";
            if (radioT9.Checked) HaveBom = "BOM";

            if(Common.Sys_DBqty == 1)
                path = Common.reportaddress + "Report\\產品別未交貨訂單" + HaveO + HaveBom + ".rpt";
            else if (Common.Sys_DBqty == 2)
                path = Common.reportaddress + "Report\\產品別未交貨訂單" + HaveO + HaveBom + "P.rpt";

            string txtend = "";
            if (radioT1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            var p = Common.User_DateTime == 1 ? Date.GetDateTime(1, true) : Date.GetDateTime(2, true);
            p = "製表日期：" + p;

            rp = new RPT(DtD, path);
            rp.office = txtmiddle + "產品別未交貨訂單";
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "txtmiddle", "產品別-未交貨訂單" });
            rp.lobj.Add(new string[] { "DateCreated", p });
            rp.lobj.Add(new string[] { "DateRange", DateRange });

        }



        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Print();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.PreView();
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            textBoxT1.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
            textBoxT2.Text = dataGridViewT1.SelectedRows[0].Cells["客戶編號"].Value.ToString();

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                Dt.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 

    }
}