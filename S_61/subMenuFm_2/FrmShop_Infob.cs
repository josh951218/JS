using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmShop_Infob : Formbase
    {
        public DataTable table = new DataTable();
        public int ShopCount { get; set; }
        public int RShopCount { get; set; }
        public decimal ShopMny { get; set; }
        public decimal RShopMny { get; set; }
        public string DateRange { get; set; }

        bool No_Data = false;
        string ReportFileName = "";
        string ReportPath = "";
        string SortState;
        string No;
        public string money;

        List<TextBox> CostControl;
        List<Button> SortBtn;

        public FrmShop_Infob()
        {
            InitializeComponent();

            this.說明.HeaderText = Common.Sys_MemoUdf;
            groupBox4.Text = Common.Sys_MemoUdf;

            CostControl = new List<TextBox> { SMny, RMny, TotMny };
            SortBtn = new List<Button> { sort1, sort2, sort3, sort4, sort5 };

            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.本幣進價.DefaultCellStyle.Format = "f" + Common.M;
            this.折數.DefaultCellStyle.Format = "f" + Common.Q;
            this.本幣稅前進價.DefaultCellStyle.Format = "f" + Common.M;
            this.本幣稅前金額.DefaultCellStyle.Format = "f" + Common.M;
            this.進價.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前進價.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPF;

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = this.計位.Visible = false;
            }

            SMny.Visible = RMny.Visible = TotMny.Visible = Common.User_ShopPrice;
            this.本幣進價.Visible = Common.User_ShopPrice;
            this.本幣稅前進價.Visible = Common.User_ShopPrice;
            this.本幣稅前金額.Visible = Common.User_ShopPrice;
            this.進價.Visible = Common.User_ShopPrice;
            this.稅前進價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;

        }

        private void FrmShop_Infob_Load(object sender, EventArgs e)
        {
            SearchUserReport("F2");

            dataGridViewT1.AutoGenerateColumns = false;

            dataGridViewT1.DataSource = table;
            this.單據日期.DataPropertyName = Common.User_DateTime == 1 ? "bsdate" : "bsdate1";

            SCount.Text = ShopCount.ToString("f0");
            RCount.Text = RShopCount.ToString("f0");
            SMny.Text = ShopMny.ToString("f" + Common.TPF);
            RMny.Text = RShopMny.ToString("f" + Common.TPF);
            TotMny.Text = (ShopMny - RShopMny).ToString("f" + Common.TPF);
            TotQty.Text = (ShopCount + RShopCount).ToString("f0");

            sort1_Click(null, null);
            if (money == "money")//判斷資料要顯示本幣還外幣
            {
                dataGridViewT1.Columns["本幣進價"].DataPropertyName = "price";
                dataGridViewT1.Columns["本幣稅前進價"].DataPropertyName = "taxprice";
                dataGridViewT1.Columns["本幣稅前金額"].DataPropertyName = "mny";

            }
            else
            {
                dataGridViewT1.Columns["本幣進價"].DataPropertyName = "priceb";
                dataGridViewT1.Columns["本幣稅前進價"].DataPropertyName = "taxpriceb";
                dataGridViewT1.Columns["本幣稅前金額"].DataPropertyName = "mnyb";
            }
        }

        void dataintodocument()
        {

            if (table.Rows.Count == 0)
            {
                No_Data = true;
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (money == "money")//如果是外幣跑外幣值
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    table.Rows[i]["Taxpriceb"] = table.Rows[i]["taxprice"].ToDecimal();
                    table.Rows[i]["mnyb"] = table.Rows[i]["mny"].ToDecimal();
                }
            }

            Common.FrmReport = new Report.Frmreport();
            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\" + ReportFileName + SortState;
            string udfPath = Common.reportaddress + "Report\\" + ReportFileName;

            bool isUserUdf = false;
            if (radio2.Checked) isUserUdf = true;
            else if (radio3.Checked) isUserUdf = true;
            else if (radio4.Checked) isUserUdf = true;
            else if (radio5.Checked) isUserUdf = true;

            if (!isUserUdf)
            {
                if (radio6.Checked) ReportPath += "O";
                if (radio10.Checked) ReportPath += "M";
            }
            try
            {
                if (radio1.Checked)
                {
                    if (Common.Sys_DBqty == 1)
                    {
                        ReportPath += "_內定報表.rpt";
                        if (File.Exists(ReportPath))
                        {
                            oRpt.Load(ReportPath);
                        }
                        else
                        {
                            MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else if (Common.Sys_DBqty == 2)
                    {
                        ReportPath += "_內定報表P.rpt";
                        if (File.Exists(ReportPath))
                        {
                            oRpt.Load(ReportPath);
                        }
                        else
                        {
                            MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                if (radio2.Checked)
                {
                    ReportPath += "_自定一.rpt";
                    if (File.Exists(ReportPath))
                    {
                        oRpt.Load(ReportPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (radio3.Checked)
                {
                    ReportPath += "_自定二.rpt";
                    if (File.Exists(ReportPath))
                    {
                        oRpt.Load(ReportPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (radio4.Checked)
                {
                    ReportPath += "_自定三.rpt";
                    if (File.Exists(ReportPath))
                    {
                        oRpt.Load(ReportPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (radio5.Checked)
                {
                    ReportPath += "_自定四.rpt";
                    if (File.Exists(ReportPath))
                    {
                        oRpt.Load(ReportPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                oRpt.SetDataSource(table);

                if (Common.Sql_LogMod == 2)//混合驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table dt in oRpt.Database.Tables)
                    {
                        dt.ApplyLogOnInfo(Common.logOnInfo);
                    }
                }
                else if (Common.Sql_LogMod == 1)//SQL驗證
                {
                    Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table dt in oRpt.Database.Tables)
                    {
                        dt.ApplyLogOnInfo(Common.logOnInfo);
                    }
                }
                oRpt.Refresh();

                TextObject myFieldTitleName = null;
                List<TextObject> Txt = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();
                //公司抬頭
                if (Txt.Find(t => t.Name == "txtstart") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                    myFieldTitleName.Text = Common.Sys_StcPnName;
                }

                //單行註腳
                if (Txt.Find(t => t.Name == "txtend") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                    if (rdFooter1.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                    else if (rdFooter2.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                    else if (rdFooter3.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                    else if (rdFooter4.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                    else if (rdFooter5.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
                //日期區間
                if (Txt.Find(t => t.Name == "DateRange") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateRange"];
                    myFieldTitleName.Text = DateRange;
                }
                //製表日期
                if (Txt.Find(t => t.Name == "DateCreated") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateCreated"];
                }

                List<ParameterField> Num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
                //備註說明
                if (Num.Find(p => p.Name == "備註說明") != null)
                    oRpt.SetParameterValue("備註說明", Common.Sys_MemoUdf);
                //日期格式


                if (Num.Find(p => p.Name == "date") != null)
                {
                    switch (Common.User_DateTime)
                    {
                        case 1:
                            oRpt.SetParameterValue("date", "民國");
                            myFieldTitleName.Text = "製表日期：" + Date.GetDateTime(1, true);
                            break;
                        case 2:
                            oRpt.SetParameterValue("date", "西元");
                            myFieldTitleName.Text = "製表日期：" + Date.GetDateTime(2, true);
                            break;
                    }
                }

                //報表參數設定
                if (Num.Find(p => p.Name == "顯示千分位") != null)
                {
                    if (pVar.TRS != "")
                        pVar.ShowTRS = true;
                    oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
                }
                if (Num.Find(p => p.Name == "千分位") != null)
                    oRpt.SetParameterValue("千分位", pVar.TRS);
                if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                    oRpt.SetParameterValue("銷貨單價小數", Common.MS);
                if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                    oRpt.SetParameterValue("銷貨單據小數", Common.MST);
                if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                    oRpt.SetParameterValue("銷項稅額小數", Common.TS);
                if (Num.Find(p => p.Name == "本幣金額小數") != null)
                    oRpt.SetParameterValue("本幣金額小數", Common.M);
                if (Num.Find(p => p.Name == "庫存數量小數") != null)
                    oRpt.SetParameterValue("庫存數量小數", Common.Q);
                if (Num.Find(p => p.Name == "是否顯示金額") != null)
                    oRpt.SetParameterValue("是否顯示金額", Common.User_SalePrice);

                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "進項金額小數") != null)
                    oRpt.SetParameterValue("進項金額小數", Common.TPF);


                Common.FrmReport.cview.ReportSource = oRpt;
                Common.FrmReport.rpt1 = oRpt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (!No_Data)
            {
                Common.FrmReport.button1_Click(null, null);
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (!No_Data)
                Common.FrmReport.ShowDialog();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (No_Data) return;
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.WordForWindows, Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".doc");
            Process.Start(Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".doc");
            Common.FrmReport.Dispose();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument();
            if (No_Data) return;
            Random Rn = new Random();
            int intRn = Rn.Next(1000);
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\temp"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            Common.FrmReport.rpt1.ExportToDisk(ExportFormatType.Excel, Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".xls");
            Process.Start(Application.StartupPath + "\\temp\\" + ReportFileName + intRn + ".xls");
            Common.FrmReport.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        void SetSortBtn()
        {
            No = dataGridViewT1["序號", dataGridViewT1.SelectedRows[0].Index].Value.ToString();
            dataGridViewT1.DataSource = null;
            SortBtn.ForEach(t => t.ForeColor = SystemColors.ControlText);
        }

        void SetSelectRow(string No)
        {
            dataGridViewT1.DataSource = table;
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["序號", i].Value.ToString() == No)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                    break;
                }
            }
        }

        ToolTip _Tip = new ToolTip();
        void SearchUserReport(string sort)
        {
            radio1.Checked = true;
            ReportFileName = "進退貨資料瀏覽";

            radio2.SetUserDefineRpt("進退貨資料瀏覽" + sort + "_自定一.rpt");
            radio3.SetUserDefineRpt("進退貨資料瀏覽" + sort + "_自定二.rpt");
            radio4.SetUserDefineRpt("進退貨資料瀏覽" + sort + "_自定三.rpt");
            radio5.SetUserDefineRpt("進退貨資料瀏覽" + sort + "_自定四.rpt");
             
        }

        private void sort1_Click(object sender, EventArgs e)
        {
            SearchUserReport("F2");
            SortState = "F2";
            SetSortBtn();
            sort1.ForeColor = Color.Red;
            table.DefaultView.Sort = "bsdate,bsno";
            SetSelectRow(No);

        }

        private void sort2_Click(object sender, EventArgs e)
        {
            SearchUserReport("F3");
            SortState = "F3";
            SetSortBtn();
            sort2.ForeColor = Color.Red;
            table.DefaultView.Sort = "fano,bsdate";
            SetSelectRow(No);
        }

        private void sort3_Click(object sender, EventArgs e)
        {
            SearchUserReport("F4");
            SortState = "F4";
            SetSortBtn();
            sort3.ForeColor = Color.Red;
            table.DefaultView.Sort = "ItNo,ItUnit,bsDate";
            SetSelectRow(No);
        }

        private void sort4_Click(object sender, EventArgs e)
        {
            SearchUserReport("F5");
            SortState = "F5";
            SetSortBtn();
            sort4.ForeColor = Color.Red;
            table.DefaultView.Sort = "EmNo,bsDate";
            SetSelectRow(No);
        }

        private void sort5_Click(object sender, EventArgs e)
        {
            SearchUserReport("F6");
            SortState = "F6";
            SetSortBtn();
            sort5.ForeColor = Color.Red;
            table.DefaultView.Sort = "SpNo,bsDate";
            SetSelectRow(No);
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridViewT1.SelectedRows.Count == 0) return;
                FaNo.Text = dataGridViewT1.SelectedRows[0].Cells["客戶編號"].Value.ToString();
                ItNo.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                EmNo.Text = dataGridViewT1.SelectedRows[0].Cells["員工編號"].Value.ToString();
                dataGridViewT1.Columns[10].HeaderText = "進價(" + dataGridViewT1.SelectedRows[0].Cells["Xa1Name"].Value.ToString() + ")";
                dataGridViewT1.Columns[12].HeaderText = "稅前進價(" + dataGridViewT1.SelectedRows[0].Cells["Xa1Name"].Value.ToString() + ")";
                dataGridViewT1.Columns[13].HeaderText = "稅前金額(" + dataGridViewT1.SelectedRows[0].Cells["Xa1Name"].Value.ToString() + ")";
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                table.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 



    }
}
