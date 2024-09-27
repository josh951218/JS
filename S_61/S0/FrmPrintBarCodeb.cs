using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using S_61.Basic;
using System.Collections.Generic;

namespace S_61.S0
{
    public partial class FrmPrintBarCodeb : JE.MyControl.Formbase
    {
        public DataTable dt = new DataTable();
        DataTable temp = new DataTable();
        List<DataGridViewColumn> List_產品介面;
        List<DataGridViewColumn> List_批號介面;
        BarCodePrintMode ViewMode;
        public FrmPrintBarCodeb(BarCodePrintMode Mode = BarCodePrintMode.Item)
        {
            InitializeComponent();

            this.包裝售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.包裝進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.進價.DefaultCellStyle.Format = "f" + Common.MF;

            this.張數下.DefaultCellStyle.Format = "f0";
            this.張數下.FirstNum = 10;
            this.張數下.LastNum = 0;

            this.售價.Visible = this.包裝售價.Visible = Common.User_SalePrice;
            this.進價.Visible = this.包裝進價.Visible = Common.User_ShopPrice;

            dataGridViewT2.ReadOnly = false;
            for (int i = 0; i < dataGridViewT2.Columns.Count; i++)
            {
                if (dataGridViewT2.Columns[i].Name == "張數下") continue;
                else
                {
                    dataGridViewT2.Columns[i].ReadOnly = true;
                }
            }
            #region 畫面調整 
            List_產品介面 = new List<DataGridViewColumn>() {包裝單位,包裝數量,進價,包裝進價,售價,包裝售價};
            List_批號介面 = new List<DataGridViewColumn>() { 製造商編號, 製造商簡稱, 批次號碼, 張數, 有效日期, 製造日期 };
            ViewMode = Mode;
            if (Mode == BarCodePrintMode.BatchNo)//入庫開窗，主要是看批號資料，所以會把畫面調整成入庫介面
            {
                for (int i = 0; i < List_產品介面.Count; i++)  List_產品介面[i].Visible = false;
                for (int i = 0; i < List_產品介面.Count; i++)  List_批號介面[i].Visible = true;
                this.張數.DefaultCellStyle.Format = "f0";
                this.張數.FirstNum = 10;
                this.張數.LastNum = 0;
            }
            else
            {
                for (int i = 0; i < List_產品介面.Count; i++) List_產品介面[i].Visible = true;
                for (int i = 0; i < List_產品介面.Count; i++) List_批號介面[i].Visible = false;
            }
            #endregion

        }

        private void FrmBarCodeBrow_Load(object sender, EventArgs e)
        {
            temp = dt.Clone();

            dataGridViewT1.DataSource = dt;
            dataGridViewT2.DataSource = temp;

            radioT3.SetUserDefineRpt("條碼列印_自定一.frx", @"ReportF\");

            SetRdUdf();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ItNo.Text.Trim().Length > 0 && ItName.Text.Trim().Length > 0)
                dataGridViewT1.Search("產品編號", ItNo.Text.Trim(), "品名規格", ItName.Text.Trim());
            else if (ItNo.Text.Trim().Length > 0)
                dataGridViewT1.Search("產品編號", ItNo.Text.Trim());
            else if (ItName.Text.Trim().Length > 0)
                dataGridViewT1.Search("品名規格", ItName.Text.Trim());
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == -1) return;
            if (dataGridViewT1.Rows.Count == 0) return;

            temp.ImportRow(dt.Rows[e.RowIndex]);
            temp.AcceptChanges();

            dataGridViewT2.FirstDisplayedScrollingRowIndex = temp.Rows.Count - 1;
            dataGridViewT2.Rows[temp.Rows.Count - 1].Selected = true;

            var index = dataGridViewT2.Rows.Count - 1;
            dataGridViewT2.CurrentCell = dataGridViewT2["產品編號下", index];
            dataGridViewT2.Rows[index].Selected = true;
            dataGridViewT2.Focus();
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            btnBrowT1.Focus();
            temp.Merge(dt.Copy());
            temp.AcceptChanges();
            dataGridViewT2.DataSource = temp;

            var index = dataGridViewT2.Rows.Count - 1;
            dataGridViewT2.CurrentCell = dataGridViewT2["產品編號下", index];
            dataGridViewT2.Rows[index].Selected = true;
            dataGridViewT2.Focus();
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            btnBrowT2.Focus();
            temp.Clear();
            dataGridViewT2.DataSource = temp;
        }

        private void btnBrowT3_Click(object sender, EventArgs e)
        {
            btnBrowT3.Focus();
            if (dataGridViewT2.Rows.Count > 0)
            {
                var p = dataGridViewT2.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p == null ? -1 : p.Index;
                if (index == -1) return;
                temp.Rows[index].Delete();
                temp.AcceptChanges();

                if (dataGridViewT2.Rows.Count > 0)
                {
                    index = dataGridViewT2.Rows.Count - 1;
                    dataGridViewT2.CurrentCell = dataGridViewT2["產品編號下", index];
                    dataGridViewT2.Rows[index].Selected = true;
                }
                dataGridViewT2.DataSource = temp;
            }
            dataGridViewT2.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        int OutReport(RptMode mode)
        {
            var path = "";
            if (ViewMode == BarCodePrintMode.Item)
            {
                if (radioT1.Checked)
                    path = Common.reportaddress + @"ReportF\條碼列印Code128.frx";
                if (radioT2.Checked)
                    path = Common.reportaddress + @"ReportF\條碼列印EAN13.frx";
                if (radioT3.Checked)
                    path = Common.reportaddress + @"ReportF\條碼列印_自定一.frx";
            }
            else
            {
                if (radioT1.Checked)
                    path = Common.reportaddress + @"ReportF\批號_條碼列印Code128.frx";
                if (radioT2.Checked)
                    path = Common.reportaddress + @"ReportF\批號_條碼列印EAN13.frx";
                if (radioT3.Checked)
                    path = Common.reportaddress + @"ReportF\批號_條碼列印_自定一.frx";
            }


            var TResult = temp.Clone();
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                if (temp.Rows[i]["ItCodeSlt"].ToInteger() == 2)
                    temp.Rows[i]["itno"] = temp.Rows[i]["itnoudf"].ToString();
                var pcs = temp.Rows[i]["張數"].ToInteger();
                for (int j = 0; j < pcs; j++)
                {
                    TResult.ImportRow(temp.Rows[i]);
                }
            }

            if (TResult.Rows.Count == 0)
            {
                MessageBox.Show("列印張數為0");
                return 0;
            }

            using (var fs = new JBS.FReport())
            {
                return fs.OutReport(mode, TResult, "Item", path);
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("是否要編輯報表?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                    return;

                var path = Common.reportaddress + @"ReportF\條碼列印_自定一.frx";
                if (System.IO.File.Exists(path) == false)
                {
                    var dl = MessageBox.Show("找不到自定表，\n是否由系統產生條碼列印_自定一.frx?", "", MessageBoxButtons.YesNo) == DialogResult.Yes;
                    if (dl == false)
                        return;

                    var path1 = Common.reportaddress + @"ReportF\條碼列印Code128.frx";
                    System.IO.File.Copy(path1, path);
                    System.Threading.Thread.Sleep(3000);
                }

                var ans = OutReport(RptMode.Design);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(panelNT1);
            pVar.SaveRadioUdf(pnlist, "NBarCode");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(panelNT1);
            pVar.SetRadioUdf(pnlist, "NBarCode");
        }


























































    }
}
