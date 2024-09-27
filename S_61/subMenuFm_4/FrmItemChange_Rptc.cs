using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmItemChange_Rptc : Formbase
    {
        public DataTable table;
        public List<string> list;
        public bool RD7 { get; set; }
        public string DateRange { get; set; }


        DataTable ShowTb = new DataTable();
        string CurrentRow = "";
        string itno = "";
        string ReportFileName = "";
        string ReportPath = "";

        public FrmItemChange_Rptc()
        {
            InitializeComponent();

            this.異動數量.Set庫存數量小數();
            this.成本.Set本幣金額小數();
            this.包裝數量.Set庫存數量小數();
            this.總數量.Set庫存數量小數();
            this.稅前進價.Set本幣金額小數();
            this.稅前金額.Set本幣金額小數();

            Totle.FirstNum = 15;
            Totle.LastNum = Common.Q;

            rd2.SetUserDefineRpt("產品進銷異動表_明細表_自定一");
            rd3.SetUserDefineRpt("產品進銷異動表_明細表_自定二");

            this.成本.Visible = Common.User_ShopPrice;
            this.稅前進價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;
        }

        private void FrmItemChange_Rptc_Load(object sender, EventArgs e)
        {
            this.異動日期.DataPropertyName = (Common.User_DateTime == 1) ? "日期" : "日期1";
            if (RD7) this.成本.Visible = false;
            WriteToTxt(list.First().Trim());
        }

        void WriteToTxt(string itno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("itno", itno);
                cmd.CommandText = "select itno,itname,itunit from item where itno=@itno";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    ItNo.Text = reader["itno"].ToString();
                    ItName.Text = reader["itname"].ToString();
                    Itunit.Text = reader["itunit"].ToString();
                }
                reader.Dispose();
                reader.Close();
                cmd.Dispose();
            }
            decimal totqty = 0;
            ShowTb.Clear();
            ShowTb = table.Copy();
            for (int i = 0; i < ShowTb.Rows.Count; i++)
            {
                if (ShowTb.Rows[i]["itno"].ToString() == itno)
                {
                    if (ShowTb.Rows[i]["單據"].ToString() == "期初")
                    {

                        if (ShowTb.Rows[i]["數量"].ToDecimal().ToString("f0") == "0")
                        {
                            ShowTb.Rows[i].Delete();
                            continue;
                        }
                    }
                    if (Common.User_DateTime == 1)
                    {
                        if (ShowTb.Rows[i]["日期"].ToString() == "")
                            ShowTb.Rows[i]["日期"] = "    /  /  ";
                        else
                            ShowTb.Rows[i]["日期"] = Date.AddLine(ShowTb.Rows[i]["日期"].ToString().Trim());
                    }
                    else
                    {
                        if (ShowTb.Rows[i]["日期1"].ToString() == "")
                            ShowTb.Rows[i]["日期"] = "     /  /  ";
                        else
                            ShowTb.Rows[i]["日期1"] = Date.AddLine(ShowTb.Rows[i]["日期1"].ToString().Trim());
                    }

                    totqty += ShowTb.Rows[i]["結餘"].ToDecimal();

                }
                else
                {
                    ShowTb.Rows[i].Delete();
                }
            }
            ShowTb.AcceptChanges();
            ShowTb.DefaultView.Sort = "日期,單號";
            dataGridViewT1.DataSource = ShowTb;
            Totle.Text = totqty.ToDecimal().ToString("f" + Common.Q);
            CurrentRow = itno;
        }

        string GetDataRow(string str)
        {
            return list.Find(r => r.ToString() == str);
        }

        void dataintodocument(RptMode mode)
        {
            ReportDocument oRpt = new ReportDocument();
            ReportFileName = "產品進銷異動表_明細表";
            ReportPath = Common.reportaddress + "Report\\" + ReportFileName;

            if (rd1.Checked)
            {
                ReportPath += "_標準報表.rpt";
                if (File.Exists(ReportPath))
                    oRpt.Load(ReportPath);
                else
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (rd2.Checked)
            {
                ReportPath += "_自定一.rpt";
                if (File.Exists(ReportPath))
                    oRpt.Load(ReportPath);
                else
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                ReportPath += "_自定二.rpt";
                if (File.Exists(ReportPath))
                    oRpt.Load(ReportPath);
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
                if (rd6.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (rd7.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (rd8.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (rd9.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (rd10.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else myFieldTitleName.Text = "";
            }

            if (Txt.Find(t => t.Name == "txtadress") != null)
            {
                //表頭-公司住址
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtadress"];
                myFieldTitleName.Text = "    " + Common.dtSysSettings.Rows[0]["StcAddr1"].ToString();
            }
            //日期區間
            if (Txt.Find(t => t.Name == "DateRange") != null)
            {
                myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["DateRange"];
                myFieldTitleName.Text = "日期區間:" + DateRange;
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

                        break;
                    case 2:
                        oRpt.SetParameterValue("date", "西元");

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
            if (Num.Find(p => p.Name == "是否顯示成本") != null)
                oRpt.SetParameterValue("是否顯示成本", Common.User_ShopPrice);

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

        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToTxt(list.First());
            btnTop.Enabled = btnPrior.Enabled = false;
            btnNext.Enabled = btnBottom.Enabled = true;
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            itno = GetDataRow(CurrentRow);
            int index = list.IndexOf(itno);
            if (index == 0)
            {
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WriteToTxt(list.First());
                btnTop.Enabled = btnPrior.Enabled = false;
                btnNext.Enabled = btnBottom.Enabled = true;
            }
            else
            {
                WriteToTxt(list[--index]);
                btnNext.Enabled = btnBottom.Enabled = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            itno = GetDataRow(CurrentRow);
            int index = list.LastIndexOf(itno);
            if (index == list.Count - 1)
            {
                MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WriteToTxt(list.Last());
                btnTop.Enabled = btnPrior.Enabled = true;
                btnNext.Enabled = btnBottom.Enabled = false;
            }
            else
            {
                WriteToTxt(list[++index]);
                btnTop.Enabled = btnPrior.Enabled = true;
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToTxt(list.Last());
            btnNext.Enabled = btnBottom.Enabled = false;
            btnTop.Enabled = btnPrior.Enabled = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Print);
        }

        private void btnPreview_Click(object sender, EventArgs e)
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
            this.Dispose();
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                ShowTb.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }






    }
}
