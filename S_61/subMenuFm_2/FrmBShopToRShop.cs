﻿using System;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmBShopToRShop : Formbase
    {
        public string BsNo = "";
        public string FaNo = "";
        public DataRow MasterRow = null;
        public DataTable dtDetail = new DataTable();
        DataTable dtM = new DataTable();
        DataTable dtD = new DataTable();

        public FrmBShopToRShop()
        {
            InitializeComponent();
            cn.ConnectionString = Common.sqlConnString;
            this.進貨總額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前進價.DefaultCellStyle.Format = "f6";
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPF;

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = false;
                this.計位.Visible = false;
            }

            this.進貨總額.Visible = Common.User_ShopPrice;
            this.進價.Visible = Common.User_ShopPrice;
            this.稅前進價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;
        }

        private void FrmBSopToRShop_Load(object sender, EventArgs e)
        {
            this.進貨日期.DataPropertyName = (Common.User_DateTime == 1) ? "bsdate" : "bsdate1";
        
            LoadMaster();
            qNo.Text = SeekNo;
            qNo_TextChanged(null, null);
        }

        void LoadMaster()
        {
            dtM.Clear();
            dataGridViewT1.DataSource = dtM;
            daM.SelectCommand.Parameters["@FaNo"].Value = FaNo;
            if (FaNo.Trim() == "")
                daM.SelectCommand.CommandText = @"
                        Select x3name = 
                        case 
                        when x3no=1 then '外加稅'
                        when x3no=2 then '內含稅'
                        when x3no=3 then '零稅'
                        when x3no=4 then '免稅'
                        end,*  from bshop order by bsno desc";
            daM.Fill(dtM);
        }

        void LoadDetail(object bsno)
        {
            dtD.Clear();
            dataGridViewT2.DataSource = dtD;
            daD.SelectCommand.CommandText = @"select *,ItNoUdf = (select top 1 ItNoUdf from item where item.itno = bshopd.itno )  
                                                from bshopd where bsno=@No order by recordno";
            daD.SelectCommand.Parameters["@No"].Value = bsno;
            daD.Fill(dtD);
            cn.Close();
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dataGridViewT2["點選", i].Value = "V";
            }
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
                if (dataGridViewT1.SelectedRows != null)
                    if (dataGridViewT1.SelectedRows.Count > 0)
                        if (e.StateChanged == DataGridViewElementStates.Selected)
                        {
                            cn.Close();
                            LoadDetail(dataGridViewT1["進貨單號", e.Row.Index].Value);
                        }
        }

        private void btnCKall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewT2.Rows.Count; i++)
            {
                dataGridViewT2["點選", i].Value = "V";
                dataGridViewT2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnCKnull_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewT2.Rows.Count; i++)
            {
                dataGridViewT2["點選", i].Value = "";
                dataGridViewT2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            dtDetail = dtD.Copy();
            dtDetail.Clear();
            for (int i = 0; i < dataGridViewT2.Rows.Count; i++)
            {
                if (dataGridViewT2["點選", i].Value.ToString() == "V")
                    dtDetail.ImportRow(dtD.Rows[i]);
            }
            if (dtDetail.Rows.Count > 0)
            {
                if (dataGridViewT1.Rows.Count > 0)
                    if (dataGridViewT1.SelectedRows != null)
                        if (dataGridViewT1.SelectedRows.Count > 0)
                        {
                            MasterRow = dtM.Rows[dataGridViewT1.SelectedRows[0].Index];
                            BsNo = dataGridViewT1.SelectedRows[0].Cells["進貨單號"].Value.ToString();
                        }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel; break;
                case Keys.F2:
                    btnCKall.PerformClick(); break;
                case Keys.F3:
                    btnCKnull.PerformClick(); break;
                case Keys.F4:
                    this.DialogResult = DialogResult.Cancel; break;
                case Keys.F9:
                    btnGet.PerformClick();
                    this.DialogResult = DialogResult.OK; break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT2.Rows.Count == 0) return;
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            if (dataGridViewT2.Columns[e.ColumnIndex].Name != "點選") return;
            DataGridViewCell cell = dataGridViewT2["點選", e.RowIndex];
            cell.Value = (cell.Value.ToString() == "V") ? "" : "V";
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            btnGet_Click(null, null);
        }

        private void qNo_TextChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
                dataGridViewT1.Search("進貨單號", qNo.Text);
        }

    }
}