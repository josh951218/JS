using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmItemLevel : Formbase
    {
        List<TextBoxbase> list;
        List<TextBoxbase> listHead = new List<TextBoxbase>();
        DataTable dt = new DataTable();

        //小數位數
        int nFirst = 1;
        int MS = 0;
        int MST = 0;
        int TS = 0;
        int M = 0;
        int MF = 0;
        int MFT = 0;
        int TF = 0;
        int Q = 0;

        public FrmItemLevel()
        {
            InitializeComponent();
            this.list = this.getEnumMember();
            cn1.ConnectionString = Common.sqlConnString;

            //小數位數設定
            nFirst = Common.nFirst;
            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciS"].ToString(), out MS);//銷貨單價小數
            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciSt"].ToString(), out MST);//銷貨單據小數
            int.TryParse(Common.dtSysSettings.Rows[0]["TaxDeciS"].ToString(), out TS);//銷項稅額小數
            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciBS"].ToString(), out M);//本幣金額小數

            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciF"].ToString(), out MF);//進貨單價小數
            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciFt"].ToString(), out MFT);//進貨單據小數
            int.TryParse(Common.dtSysSettings.Rows[0]["TaxDeciF"].ToString(), out TF);//進項稅額小數
            int.TryParse(Common.dtSysSettings.Rows[0]["QtyDeci"].ToString(), out Q);//庫存數量小數


            this.售價1.Set銷貨單價小數();
            this.售價一1.Set銷貨單價小數();
            this.售價二1.Set銷貨單價小數();
            this.售價三1.Set銷貨單價小數();
            this.售價四1.Set銷貨單價小數();
            this.售價五1.Set銷貨單價小數();
            this.包裝售價1.Set銷貨單價小數();
            this.包裝售價一1.Set銷貨單價小數();
            this.包裝售價二1.Set銷貨單價小數();
            this.包裝售價三1.Set銷貨單價小數();
            this.包裝售價四1.Set銷貨單價小數();
            this.包裝售價五1.Set銷貨單價小數();
            this.包裝數量1.Set銷貨單價小數();

            this.售價1.Visible = Common.User_SalePrice;
            this.售價一1.Visible = Common.User_SalePrice;
            this.售價二1.Visible = Common.User_SalePrice;
            this.售價三1.Visible = Common.User_SalePrice;
            this.售價四1.Visible = Common.User_SalePrice;
            this.售價五1.Visible = Common.User_SalePrice;

            this.包裝售價1.Visible = Common.User_SalePrice;
            this.包裝售價一1.Visible = Common.User_SalePrice;
            this.包裝售價二1.Visible = Common.User_SalePrice;
            this.包裝售價三1.Visible = Common.User_SalePrice;
            this.包裝售價四1.Visible = Common.User_SalePrice;
            this.包裝售價五1.Visible = Common.User_SalePrice;

            ItPrice.Visible = Common.User_SalePrice;
            ItPrice1.Visible = Common.User_SalePrice;
            ItPrice2.Visible = Common.User_SalePrice;
            ItPrice3.Visible = Common.User_SalePrice;
            ItPrice4.Visible = Common.User_SalePrice;
            ItPrice5.Visible = Common.User_SalePrice;

            ItPriceP.Visible = Common.User_SalePrice;
            ItPriceP1.Visible = Common.User_SalePrice;
            ItPriceP2.Visible = Common.User_SalePrice;
            ItPriceP3.Visible = Common.User_SalePrice;
            ItPriceP4.Visible = Common.User_SalePrice;
            ItPriceP5.Visible = Common.User_SalePrice;

            foreach (Control a in this.Controls)
            {
                if (a is TextBoxNumberT)
                {
                    (a as TextBoxNumberT).FirstNum = nFirst;
                    (a as TextBoxNumberT).LastNum = MS;

                }
            }
            ItPkgqty.LastNum = Q;

            this.listHead.Clear();
            this.listHead.AddRange(list.AsEnumerable());

            if (listHead.Contains(ItNos))
                listHead.Remove(ItNos);

            if (listHead.Contains(ItNoUdfs))
                listHead.Remove(ItNoUdfs);

            if (listHead.Contains(KiNos))
                listHead.Remove(KiNos);
        }

        private void FrmItemLevel_Load(object sender, EventArgs e)
        {
            loadDB();
            if (dt.Rows.Count > 0) txtboxchange();

            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            ItNos.Focus();
        }

        public void loadDB()
        {
            dataGridViewT1.DataSource = null;
            dt.Clear();
            dt.Dispose();
            dt = new DataTable();
            da1.Fill(dt);
            dataGridViewT1.DataSource = dt;
        }

        public void txtboxchange()
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            foreach (TextBoxbase tb in listHead)
            {
                if (index == -1)
                    tb.Clear();
                else
                {
                    if (tb is TextBoxNumberT)
                    {
                        tb.Text = dt.Rows[index][tb.Name].ToDecimal().ToString("f" + Common.MS);
                    }
                    else
                    {
                        tb.Text = dt.Rows[index][tb.Name].ToString();
                    }
                }
            }
        }

        private void dataGridViewT1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            txtboxchange();
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            txtboxchange();
        }

        private void btnModify_EnabledChanged(object sender, EventArgs e)
        {
            btnUpdateBat.Enabled = !btnModify.Enabled;
            btnTempPrice.Enabled = btnModify.Enabled;
        }

        private void btnUpdateBat_Click(object sender, EventArgs e)
        {
            using (FrmItemLVbat frm = new FrmItemLVbat())
            {
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    loadDB();
                    if (dt.Rows.Count > 0) txtboxchange();
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.Modify, ref list);

            dataGridViewT1.ReadOnly = false;
            dataGridViewT1.Columns["產品編號1"].ReadOnly = true;
            dataGridViewT1.Columns["自訂編號1"].ReadOnly = true;

            ItNo.ReadOnly = true;
            ItNoUdf.ReadOnly = true;
            btnModify.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.None, ref list);
            btnModify.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            dataGridViewT1.ReadOnly = true;
            loadDB();
            txtboxchange();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Search(ref DataTable dt, ref DataGridViewT grid, string TColumn, string TValue)
        {
            if (String.IsNullOrEmpty(TValue)) return;
            if (!dt.Columns.Contains(TColumn)) return;
            if (dt.Rows.Count == 0) return;

            var list = dt.AsEnumerable().ToList();
            var index = list.FindIndex(r => r[TColumn].ToString() == TValue);
            if (index == -1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (String.CompareOrdinal(list[i][TColumn].ToString(), TValue) > 0)
                    {
                        index = i;
                        break;
                    }
                    if (i == list.Count - 1)
                        index = i;
                }
            }

            if (index == -1) index = 0;
            if (index >= dt.Rows.Count) index = dt.Rows.Count - 1;

            grid.FirstDisplayedScrollingRowIndex = index;
            grid.CurrentCell = grid[0, index];
            grid.Rows[index].Selected = true;
        }

        private void ItNo_TextChanged(object sender, EventArgs e)
        {
            if (ItNos.TrimTextLenth() > 0)
            {
                Search(ref dt, ref dataGridViewT1, "itno", ItNos.Text.Trim());
            }
            else if (ItNoUdfs.TrimTextLenth() > 0)
            {
                Search(ref dt, ref dataGridViewT1, "itnoudf", ItNoUdfs.Text.Trim());
            }
            else if (KiNos.TrimTextLenth() > 0)
            {
                Search(ref dt, ref dataGridViewT1, "kino", KiNos.Text.Trim());
            }

            txtboxchange();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D2:
                case Keys.NumPad2:
                    if (btnModify.Enabled)
                        btnModify.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    if (btnExit.Enabled)
                        btnExit.PerformClick();
                    break;
                case Keys.F9:
                    if (btnSave.Enabled)
                        btnSave.PerformClick();
                    break;
                case Keys.F4:
                    if (btnCancel.Enabled)
                        btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void 表頭欄位_Leave(object sender, EventArgs e)
        {
            int index = dt.AsEnumerable().ToList().FindIndex(r => r["itno"].ToString().Trim() == ItNo.Text.Trim());
            if (index == -1)
                return;

            if (sender is TextBoxNumberT)
                dt.Rows[index][((TextBox)sender).Name] = ((TextBox)sender).Text.ToDecimal();
            else
                dt.Rows[index][((TextBox)sender).Name] = ((TextBox)sender).Text.Trim();
            dataGridViewT1.InvalidateRow(index);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                da1.Update(dt);
            }
            catch(DBConcurrencyException ex)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            btnCancel_Click(null, null);
        }

        private void btnTempPrice_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmItemTempPrice())
            {
                frm.ShowDialog();
            }
            this.Dispose(); 
        }
    }
}
