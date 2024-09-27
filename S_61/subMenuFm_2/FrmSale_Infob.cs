using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class FrmSale_Infob : Formbase
    {
        public DataTable table0 = new DataTable(); //無組件
        public DataTable table = new DataTable();  //有組件
        public int SaleCount { get; set; }
        public int RSaleCount { get; set; }
        public decimal SaleMny { get; set; }
        public decimal RSaleMny { get; set; }
        public string DateRange { get; set; }

        string ReportFileName = "";
        string ReportPath = "";
        string SortState;
        string No;

        public string money;

        List<TextBox> CostControl;

        public FrmSale_Infob()
        {
            InitializeComponent();

            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.本幣售價.DefaultCellStyle.Format = "f" + Common.M;
            this.折數.DefaultCellStyle.Format = "f3";
            this.本幣稅前售價.DefaultCellStyle.Format = "f" + Common.M;
            this.本幣稅前金額.DefaultCellStyle.Format = "f" + Common.M;
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.稅前售價.DefaultCellStyle.Format = "f" + Common.MS;

            CostControl = new List<TextBox> { SMny, RMny, TotMny };

            if (Common.Sys_StockKind == 2)
            {
                this.專案名稱.HeaderText = "班別名稱";
                this.送貨方式.HeaderText = "機台名稱";
            }

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = this.計位.Visible = false;
            }
        }

        private void FrmSale_Infob_Load(object sender, EventArgs e)
        {
            SearchUserReport("F2");

            //權限設定
            if (!Common.User_SalePrice)
            {
                CostControl.ForEach(t =>
                {
                    Panel pnl = new Panel();
                    pnl.Dock = DockStyle.Fill;
                    pnl.BackColor = Color.Silver;
                    t.Controls.Add(pnl);
                    t.TabStop = false;
                });
                dataGridViewT1.Columns["本幣售價"].Visible = false;
                dataGridViewT1.Columns["本幣稅前售價"].Visible = false;
                dataGridViewT1.Columns["本幣稅前金額"].Visible = false;
                dataGridViewT1.Columns["售價"].Visible = false;
                dataGridViewT1.Columns["稅前售價"].Visible = false;
                dataGridViewT1.Columns["幣別"].Visible = false;

            }

            this.單據日期.DataPropertyName = Common.User_DateTime == 1 ? "SaDate" : "SaDate1";

            SCount.Text = SaleCount.ToString("f0");
            RCount.Text = RSaleCount.ToString("f0");
            SMny.Text = SaleMny.ToString("f" + Common.TPS);
            RMny.Text = RSaleMny.ToString("f" + Common.TPS);
            TotQty.Text = (SaleCount + RSaleCount).ToString("f0");
            TotMny.Text = (SaleMny + RSaleMny).ToString("f" + Common.TPS);

            sort1_Click(null, null);
            if (money == "money")//判斷資料要顯示本幣還外幣
            {
                dataGridViewT1.Columns["本幣售價"].DataPropertyName = "price";
                dataGridViewT1.Columns["本幣稅前售價"].DataPropertyName = "taxprice";
                dataGridViewT1.Columns["本幣稅前金額"].DataPropertyName = "mny";
            }
            else
            {
                dataGridViewT1.Columns["本幣售價"].DataPropertyName = "priceb";
                dataGridViewT1.Columns["本幣稅前售價"].DataPropertyName = "taxpriceb";
                dataGridViewT1.Columns["本幣稅前金額"].DataPropertyName = "mnyb";
            }
        }

        void dataintodocument(RptMode mode)
        {
            if (table0.Rows.Count <= 0)
            {
                MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (money == "money")//如果是外幣跑外幣值
                {
                    for (int i = 0; i < table0.Rows.Count; i++)
                    {
                        table0.Rows[i]["Taxpriceb"] = table0.Rows[i]["taxprice"].ToDecimal();
                        table0.Rows[i]["mnyb"] = table0.Rows[i]["mny"].ToDecimal();
                    }
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        table.Rows[i]["Taxpriceb"] = table.Rows[i]["taxprice"].ToDecimal();
                        table.Rows[i]["mnyb"] = table.Rows[i]["mny"].ToDecimal();
                    }
                }
            
            }

            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\" + ReportFileName + SortState;
            string udfPath = Common.reportaddress + "Report\\" + ReportFileName + SortState;

            bool IsUserDefine = false;//是否為自定報表
            if (radio2.Checked) IsUserDefine = true;
            else if (radio3.Checked) IsUserDefine = true;
            else if (radio4.Checked) IsUserDefine = true;
            else if (radio5.Checked) IsUserDefine = true;

            if (!IsUserDefine)
            {
                if (radio6.Checked) ReportPath += "O";
                if (radio8.Checked) ReportPath += "M";
                if (radio11.Checked) ReportPath += "B";
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
                else if (radio2.Checked)
                {
                    udfPath += "_明細自定.rpt";
                    if (File.Exists(udfPath))
                    {
                        oRpt.Load(udfPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + udfPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (radio3.Checked)
                {
                    udfPath += "_組合自定.rpt";
                    if (File.Exists(udfPath))
                    {
                        oRpt.Load(udfPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + udfPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (radio4.Checked)
                {
                    udfPath += "_自定一.rpt";
                    if (File.Exists(udfPath))
                    {
                        oRpt.Load(udfPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + udfPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (radio5.Checked)
                {
                    udfPath += "_自定二.rpt";
                    if (File.Exists(udfPath))
                    {
                        oRpt.Load(udfPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + udfPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (radio10.Checked || IsUserDefine)
                    oRpt.SetDataSource(table0);
                else
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
                //銷貨單標題
                if (Txt.Find(t => t.Name == "title") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["title"];
                    myFieldTitleName.Text = Common.Sys_SaleHead;
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
                    myFieldTitleName.Text = "製表日期：" + Date.GetDateTime(Common.User_DateTime, true);
                }
                //日期格式
                List<ParameterField> Num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
                if (Num.Find(p => p.Name == "date") != null)
                {
                    oRpt.SetParameterValue("date", Common.User_DateTime == 1 ? "民國" : "西元");
                }
                //備註說明
                if (Num.Find(p => p.Name == "備註說明") != null)
                    oRpt.SetParameterValue("備註說明", Common.Sys_MemoUdf);
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

                Common.FrmReport = new Report.Frmreport();
                Common.FrmReport.cview.ReportSource = oRpt;
                Common.FrmReport.rpt1 = oRpt;

                if (mode == RptMode.Print)
                    Common.FrmReport.button1_Click(null, null);
                else if (mode == RptMode.PreView)
                    Common.FrmReport.ShowDialog();
                else if (mode == RptMode.Word)
                    Common.FrmReport.word(ReportFileName);
                else if (mode == RptMode.Excel)
                    Common.FrmReport.excel(ReportFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Excel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }





        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (dataGridViewT1.SelectedRows.Count == 0) return;
                CuNo.Text = dataGridViewT1.SelectedRows[0].Cells["客戶編號"].Value.ToString();
                ItNo.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                EmNo.Text = dataGridViewT1.SelectedRows[0].Cells["員工編號"].Value.ToString();
                dataGridViewT1.Columns[10].HeaderText = "售價(" + dataGridViewT1.SelectedRows[0].Cells["Xa1Name"].Value.ToString() + ")";
                dataGridViewT1.Columns[12].HeaderText = "稅前售價(" + dataGridViewT1.SelectedRows[0].Cells["Xa1Name"].Value.ToString() + ")";
                dataGridViewT1.Columns[13].HeaderText = "稅前金額(" + dataGridViewT1.SelectedRows[0].Cells["Xa1Name"].Value.ToString() + ")";
            }
        }

        void SetButtonsBlackColor()
        {
            var p = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (p == -1) No = "";
            else No = dataGridViewT1["序號", p].Value.ToString();
            dataGridViewT1.DataSource = null;

            sort1.ForeColor = sort2.ForeColor = sort3.ForeColor = sort4.ForeColor = SystemColors.ControlText;
        }

        void SeekRowHeightLight(string no)
        {
            dataGridViewT1.DataSource = table0;
            if (dataGridViewT1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    if (dataGridViewT1["序號", i].Value.ToString() == no)
                    {
                        dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                        dataGridViewT1.Rows[i].Selected = true;
                        break;
                    }
                }
            }
        }

        void SearchUserReport(string sort)
        {
            radio1.Checked = true;

            ReportFileName = "銷退貨資料瀏覽";

            radio2.SetUserDefineRpt("銷退貨資料瀏覽" + sort + "_明細自定.rpt");
            radio3.SetUserDefineRpt("銷退貨資料瀏覽" + sort + "_組合自定.rpt");
            radio4.SetUserDefineRpt("銷退貨資料瀏覽" + sort + "_自定一.rpt");
            radio5.SetUserDefineRpt("銷退貨資料瀏覽" + sort + "_自定二.rpt");
        }

        private void sort1_Click(object sender, EventArgs e)
        {
            SearchUserReport("F2");
            SortState = "F2";
            SetButtonsBlackColor();
            sort1.ForeColor = Color.Red;
            table0.DefaultView.Sort = "saDate,saNo";
            SeekRowHeightLight(No);
        }

        private void sort2_Click(object sender, EventArgs e)
        {
            SearchUserReport("F3");
            SortState = "F3";
            SetButtonsBlackColor();
            sort2.ForeColor = Color.Red;
            table0.DefaultView.Sort = "cuNo,saDate";
            SeekRowHeightLight(No);
        }

        private void sort3_Click(object sender, EventArgs e)
        {
            SearchUserReport("F4");
            SortState = "F4";
            SetButtonsBlackColor();
            sort3.ForeColor = Color.Red;
            table0.DefaultView.Sort = "ItNo,ItUnit,saDate";
            SeekRowHeightLight(No);
        }

        private void sort4_Click(object sender, EventArgs e)
        {
            SearchUserReport("F5");
            SortState = "F5";
            SetButtonsBlackColor();
            sort4.ForeColor = Color.Red;
            table0.DefaultView.Sort = "EmNo,saDate";
            SeekRowHeightLight(No);
        }

        private void FrmSale_Infob_Shown(object sender, EventArgs e)
        {
            sort1.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                table0.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 































    }
}
