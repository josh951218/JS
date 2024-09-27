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

namespace S_61.subMenuFm_1
{
    public partial class FrmFord_Infob : Formbase
    {
        public DataTable table;
        public string DateRange;
        List<DataRow> list;
        string ReportFileName = "";
        string ReportPath = "";
        bool No_Data = false;
        string NO = "";
        string SortState = "F2";
        List<Button> qury;
        DataRow dr;

        public FrmFord_Infob()
        {
            InitializeComponent();
            this.說明.HeaderText = Common.Sys_MemoUdf;
            qury = new List<Button> { qury1, qury3, qury4, qury5, qury6, qury7, qury8 };

            this.本幣進價.Visible = Common.User_ShopPrice;
            this.本幣稅前進價.Visible = Common.User_ShopPrice;
            this.本幣稅前金額.Visible = Common.User_ShopPrice;
            this.外幣進價.Visible = Common.User_ShopPrice;
            this.外幣稅前進價.Visible = Common.User_ShopPrice;
            this.外幣稅前金額.Visible = Common.User_ShopPrice;
        }

        private void FrmFord_Infob_Load(object sender, EventArgs e)
        {
            list = table.AsEnumerable().ToList();
            SearchUserReport("F2");
             
            this.採購數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.計價數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.未交量.DefaultCellStyle.Format = "f" + Common.Q;
            this.折數.DefaultCellStyle.Format = "f3";
            this.本幣進價.DefaultCellStyle.Format = "f" + Common.M;
            this.本幣稅前進價.DefaultCellStyle.Format = "f" + Common.M;
            this.本幣稅前金額.DefaultCellStyle.Format = "f" + Common.M;
            this.外幣進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.外幣稅前進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.外幣稅前金額.DefaultCellStyle.Format = "f" + Common.TPF;
            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = this.計位.Visible = false;
            }

            dataGridViewT1.DataSource = table;

            qury1.ForeColor = Color.Red;
        }

        void FastReport列印(string path, RptMode mode)
        {
            //string sql = "";
            DataTable printTB = new DataTable();
            DataTable tableB = new DataTable();
            printTB.TableName = "FORD_";

            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                //銷貨單標題
                var title = Common.Sys_SaleHead;
                FastReport.dy.Add("title", title);
                //三行註腳
                string txtend = "";
                if (radio8.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (radio9.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (radio10.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (radio11.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (radio12.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else txtend = "";
                FastReport.dy.Add("txtend", txtend);
                FastReport.dy.Add("txtRange", DateRange);
                FastReport.dy.Add("txtstart", Common.Sys_StcPnName);
                FastReport.dy.Add("price", Common.User_SalePrice);
                FastReport.PreView(path, table, "FORD_", null, null, mode, ReportFileName);
            }
        }

        string 確認列印報表()
        {
            string path = "";

            path = "採購資料瀏覽" + SortState;

            if (!(rd2.Checked || rd3.Checked))
            {
                if (rd6.Checked)
                    path += "P";
            }
            if (rd1.Checked)
            {
                path += "_內定報表";
                if (Common.Sys_DBqty == 2)
                {
                    path += "P";
                }
            }
            if (rd2.Checked)
                path += "_明細自定一";
            if (rd3.Checked)
                path += "_明細自定二";

            return Common.判斷開啟報表類型(path);
        }

        void dataintodocument()
        {
            No_Data = false;

            Common.FrmReport = new Report.Frmreport();
            ReportDocument oRpt = new ReportDocument();
            ReportPath = Common.reportaddress + "Report\\" + ReportFileName + SortState;
            string udfPath = Common.reportaddress + "Report\\" + ReportFileName + SortState;
            bool isUserUdf = false;
            if (rd1.Checked)
                isUserUdf = false;
            else
                isUserUdf = true;

            if (!isUserUdf)
            {
                if (rd6.Checked)
                    ReportPath += "P";
            }


            try
            {
                if (rd1.Checked)
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
                            No_Data = true;
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
                            No_Data = true;
                            return;
                        }
                    }
                }
                if (rd2.Checked)
                {
                    udfPath += "_明細自定一.rpt";
                    if (File.Exists(udfPath))
                    {
                        oRpt.Load(udfPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + udfPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        No_Data = true;
                        return;
                    }
                }
                if (rd3.Checked)
                {
                    udfPath += "_明細自定二.rpt";
                    if (File.Exists(udfPath))
                    {
                        oRpt.Load(udfPath);
                    }
                    else
                    {
                        MessageBox.Show("報表檔案不存在\n" + udfPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        No_Data = true;
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
                    if (radio8.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                    else if (radio9.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                    else if (radio10.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                    else if (radio11.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                    else if (radio12.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                    else myFieldTitleName.Text = "";
                }
                //日期區間
                if (Txt.Find(t => t.Name == "DateRange") != null)
                {
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateRange"];
                    myFieldTitleName.Text = "日期區間：" + DateRange;
                }
                //製表日期
                if (Txt.Find(t => t.Name == "DateCreated") != null)
                {
                    string datetime = "";
                    myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateCreated"];
                    if (Common.User_DateTime == 1)
                    {
                        datetime = Date.GetDateTime(1, false);
                        datetime = Date.AddLine(datetime);
                    }
                    else
                    {
                        datetime = Date.GetDateTime(2, false);
                        datetime = Date.AddLine(datetime);
                    }
                    myFieldTitleName.Text = datetime;
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
                if (Num.Find(p => p.Name == "進貨單價小數") != null)
                    oRpt.SetParameterValue("進貨單價小數", Common.MF);
                if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                    oRpt.SetParameterValue("銷貨單價小數", Common.MS);
                if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                    oRpt.SetParameterValue("銷貨單據小數", Common.MST);
                if (Num.Find(p => p.Name == "進貨單據小數") != null)
                    oRpt.SetParameterValue("進貨單據小數", Common.MFT);
                if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                    oRpt.SetParameterValue("銷項稅額小數", Common.TS);
                if (Num.Find(p => p.Name == "進項稅額小數") != null)
                    oRpt.SetParameterValue("進項稅額小數", Common.TF);
                if (Num.Find(p => p.Name == "進項金額小數") != null)
                    oRpt.SetParameterValue("進項金額小數", Common.TPF);
                if (Num.Find(p => p.Name == "銷項金額小數") != null)
                    oRpt.SetParameterValue("銷項金額小數", Common.TPS);
                if (Num.Find(p => p.Name == "本幣金額小數") != null)
                    oRpt.SetParameterValue("本幣金額小數", Common.M);
                if (Num.Find(p => p.Name == "庫存數量小數") != null)
                    oRpt.SetParameterValue("庫存數量小數", Common.Q);
                if (Num.Find(p => p.Name == "是否顯示金額") != null)
                    oRpt.SetParameterValue("是否顯示金額", Common.User_SalePrice);

                Common.FrmReport.cview.ReportSource = oRpt;
                Common.FrmReport.rpt1 = oRpt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void WriteToTxt(DataRow dr)
        {
            if (dr == null) return;
            FaNo.Text = dr["fano"].ToString();
            ItNo.Text = dr["itno"].ToString();
            EmNo.Text = dr["emno"].ToString();
        }

        void SetButtonColor()
        {
            qury.ForEach(r => r.ForeColor = SystemColors.ControlText);
        }

        void SetSelectRow(string NO)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (dataGridViewT1["序號", i].Value.ToString() == NO)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Selected = true;
                    break;
                }
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var path = 確認列印報表();
            if (File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (path.EndsWith(".frx"))
            {
                FastReport列印(path, RptMode.Print);
            }
            else
            {
                dataintodocument();
                if (!No_Data)
                {
                    Common.FrmReport.button1_Click(null, null);
                }
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            var path = 確認列印報表();
            if (File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (path.EndsWith(".frx"))
            {
                FastReport列印(path, RptMode.PreView);
            }
            else
            {
                dataintodocument();
                if (!No_Data)
                    Common.FrmReport.ShowDialog();
            }
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            var path = 確認列印報表();
            if (File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (path.EndsWith(".frx"))
            {
                FastReport列印(path, RptMode.Word);
            }
            else
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
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var path = 確認列印報表();
            if (File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (path.EndsWith(".frx"))
            {
                FastReport列印(path, RptMode.Excel);
            }
            else
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
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
         
        void SearchUserReport(string sort)
        {
            rd1.Checked = true;
            ReportFileName = "採購資料瀏覽";

            rd2.判斷有無CF或RF("採購資料瀏覽" + sort + "_明細自定一");
            rd3.判斷有無CF或RF("採購資料瀏覽" + sort + "_明細自定二");
              
        }

        private void qury1_Click(object sender, EventArgs e)
        {
            SearchUserReport("F2");
            SortState = "F2";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "fodate,fono";
            SetButtonColor();
            qury1.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury3_Click(object sender, EventArgs e)
        {
            SearchUserReport("F3");
            SortState = "F3";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "fano,esdate";
            SetButtonColor();
            qury3.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury4_Click(object sender, EventArgs e)
        {
            SearchUserReport("F4");
            SortState = "F4";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "itno,itunit,esdate";
            SetButtonColor();
            qury4.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury5_Click(object sender, EventArgs e)
        {
            SearchUserReport("F5");
            SortState = "F5";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "emno,esdate";
            SetButtonColor();
            qury5.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury6_Click(object sender, EventArgs e)
        {
            SearchUserReport("F6");
            SortState = "F6";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "esdate,itno,itunit,fono";
            SetButtonColor();
            qury6.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury7_Click(object sender, EventArgs e)
        {
            SearchUserReport("F7");
            SortState = "F7";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "fano,itno,itunit,esdate";
            SetButtonColor();
            qury7.ForeColor = Color.Red;
            SetSelectRow(NO);
        }

        private void qury8_Click(object sender, EventArgs e)
        {
            SearchUserReport("F8");
            SortState = "F8";
            NO = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            table.DefaultView.Sort = "emno,itno,itunit,esdate";
            SetButtonColor();
            qury8.ForeColor = Color.Red;
            SetSelectRow(NO);
        }
         
        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            dr = list.Find(r => r["序號"].ToString().Trim() == dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim());
            WriteToTxt(dr);
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

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var path = 確認列印報表();
                if (path.EndsWith(".frx") == false) return;

                var dl = MessageBox.Show("是否要修改報表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl != DialogResult.Yes) return;

                JBS.FReport.Design(path);
            }
        }

    }
}
