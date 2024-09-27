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

namespace S_61.S2
{
    public partial class FrmLendNew_Infob : Formbase
    {
        public DataTable dtD = new DataTable();
        public int LendCount { get; set; }
        public int RLendCount { get; set; }
        public decimal LendMny { get; set; }
        public decimal RLendMny { get; set; }
        public string DateRange { get; set; }

        string ReportFileName = "";
        string ReportPath = "";
        string SortState;
        string No;

        List<TextBox> CostControl;
        List<Button> SortBtn;

        public FrmLendNew_Infob()
        {
            InitializeComponent();

            CostControl = new List<TextBox> { LMny, RMny, TotMny };
            SortBtn = new List<Button> { sort1, sort2, sort3, sort4 };

            this.說明.HeaderText = Common.Sys_MemoUdf;
            this.數量.Set庫存數量小數();
            this.本幣售價.Set本幣金額小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.本幣稅前售價.Set本幣金額小數();
            this.本幣稅前金額.Set銷貨單價小數();
            this.售價.Set銷貨單價小數();
            this.稅前售價.DefaultCellStyle.Format = "f6";
            this.稅前金額.Set銷貨單價小數();

            radioT2.SetUserDefineRpt("借出還入資料瀏覽_自定一");
            radioT3.SetUserDefineRpt("借出還入資料瀏覽_自定二");
            radioT4.SetUserDefineRpt("借出還入資料瀏覽_自定三");

            this.本幣售價.Visible = Common.User_SalePrice;
            this.本幣稅前售價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;
            this.售價.Visible = Common.User_SalePrice;
            this.稅前售價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
            LMny.Visible = RMny.Visible = TotMny.Visible = Common.User_SalePrice;
        }

        private void FrmLendNew_Infob_Load(object sender, EventArgs e)
        {
            ReportFileName = "借出還入資料瀏覽";

            //權限設定
            if (!Common.User_ShopPrice)
            {
                CostControl.ForEach(t =>
                {
                    Panel pen = new Panel();
                    pen.BackColor = Color.Silver;
                    pen.Dock = DockStyle.Fill;
                    t.Controls.Add(pen);
                    t.TabStop = false;
                });

                dataGridViewT1.Columns["本幣售價"].Visible = false;
                dataGridViewT1.Columns["本幣稅前售價"].Visible = false;
                dataGridViewT1.Columns["本幣稅前金額"].Visible = false;
                dataGridViewT1.Columns["售價"].Visible = false;
                dataGridViewT1.Columns["稅前售價"].Visible = false;
                dataGridViewT1.Columns["稅前金額"].Visible = false;
            }

            dataGridViewT1.DataSource = dtD;
            this.單據日期.DataPropertyName = Common.User_DateTime == 1 ? "ledate" : "ledate1";

            LCount.Text = LendCount.ToString("f0");
            RCount.Text = RLendCount.ToString("f0");
            TotQty.Text = (LendCount + RLendCount).ToString("f0");

            RLendMny = -1 * RLendMny;
            LMny.Text = LendMny.ToString("f" + Common.TPF);
            RMny.Text = RLendMny.ToString("f" + Common.TPF);

            TotMny.Text = (LendMny + RLendMny).ToString("f" + Common.TPF);
            sort1_Click(null, null);
        }

        void SetSortBtn()
        {
            dataGridViewT1.SuspendLayout();
            No = dataGridViewT1["序號", dataGridViewT1.SelectedRows[0].Index].Value.ToString();
            //dataGridViewT1.DataSource = null;
            SortBtn.ForEach(t => t.ForeColor = SystemColors.ControlText);
        }

        void SetSelectRow(string No)
        {
            //dataGridViewT1.DataSource = dtD;
            dataGridViewT1.ResumeLayout(true);
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

        private void sort1_Click(object sender, EventArgs e)
        {
            SortState = "F2";
            SetSortBtn();
            sort1.ForeColor = Color.Red;
            dtD.DefaultView.Sort = "ledate,leno";
            SetSelectRow(No);
        }

        private void sort2_Click(object sender, EventArgs e)
        {
            SortState = "F3";
            SetSortBtn();
            sort2.ForeColor = Color.Red;
            dtD.DefaultView.Sort = "CuName1,ledate";
            SetSelectRow(No);
        }

        private void sort3_Click(object sender, EventArgs e)
        {
            SortState = "F4";
            SetSortBtn();
            sort3.ForeColor = Color.Red;
            dtD.DefaultView.Sort = "ItNo,ItUnit,ledate";
            SetSelectRow(No);
        }

        private void sort4_Click(object sender, EventArgs e)
        {
            SortState = "F5";
            SetSortBtn();
            sort4.ForeColor = Color.Red;
            dtD.DefaultView.Sort = "Emno,ledate";
            SetSelectRow(No);
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //if (e.StateChanged == DataGridViewElementStates.Selected)
            //{
            //    if (dataGridViewT1.SelectedRows.Count == 0) return;
            //    CuNo.Text = dataGridViewT1.SelectedRows[0].Cells["客戶編號"].Value.ToString();
            //    ItNo.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
            //    EmNo.Text = dataGridViewT1.SelectedRows[0].Cells["人員編號"].Value.ToString();
            //}
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            if (dataGridViewT1.SelectedRows == null)
                return;

            if (dataGridViewT1.SelectedRows[0].Index == index)
            {
                CuNo.Text = dataGridViewT1.SelectedRows[0].Cells["客戶編號"].Value.ToString();
                ItNo.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                EmNo.Text = dataGridViewT1.SelectedRows[0].Cells["人員編號"].Value.ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dtD.Clear();

            this.Close();
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dtD.Excel匯出並開啟(this.Text);
            }
            else if (keyData == Keys.F11)
            {
                btnExit_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F2") && keyData.ToString().EndsWith("Shift"))
            {
                sort1_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F3") && keyData.ToString().EndsWith("Shift"))
            {
                sort2_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                sort3_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F5") && keyData.ToString().EndsWith("Shift"))
            {
                sort4_Click(null, null);
            }
            else if (keyData == Keys.Escape)
            {
                btnExit_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void dataintodocument(RptMode mode)
        {
            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\" + ReportFileName + SortState;
            string udfPath = Common.reportaddress + "Report\\" + ReportFileName;

            try
            {
                if (radioT1.Checked)
                {
                    //if (Common.Sys_DBqty == 1)
                    //{
                        ReportPath += "_標準表.rpt";
                        if (File.Exists(ReportPath))
                        {
                            oRpt.Load(ReportPath);
                        }
                        else
                        {
                            MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    //}

                }
                if (radioT2.Checked)
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
                if (radioT3.Checked)
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
                if (radioT4.Checked)
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


                oRpt.SetDataSource(dtD);

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


    }
}
