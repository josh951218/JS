using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

namespace S_61.subMenuFm_3
{
    public partial class FrmCust_Accb : Formbase
    {
        JBS.JS.xEvents xe;
        public Dictionary<string, decimal> dy = new Dictionary<string, decimal>();
        public DataTable tResult = new DataTable();
        public string DateRange = "";
        public string spname = "";
        DataTable tView = new DataTable();

        public FrmCust_Accb()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            this.稅前金額.DefaultCellStyle.Format = "f" + Common.MST;//格式沒設定,重整會出錯(EX:歸0)
            this.營業稅額.DefaultCellStyle.Format = "f" + Common.TS;
            this.應收總計.DefaultCellStyle.Format = "f" + Common.MST;
            this.折扣金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.已收加預收.DefaultCellStyle.Format = "f" + Common.MST;
            this.本期應收.DefaultCellStyle.Format = "f" + Common.MST;
        }

        private void FrmCust_Accb_Load(object sender, EventArgs e)
        {
            radio5.判斷有無CF或RF("客戶別應收帳款_明細簡要表_明細自定");
            radio6.判斷有無CF或RF("客戶別應收帳款_明細簡要表_組合自定");
            radio7.判斷有無CF或RF("客戶別應收帳款_明細簡要表_應收分組表");
            radio8.判斷有無CF或RF("客戶別應收帳款_明細簡要表_簡要自定");

            ToolTip _Tip = new ToolTip();
            _Tip.SetToolTip(radio1, "CustAccRpt1.rpt");
            this.dataGridViewT1.tableName = "CustAcc";
            groupBoxT3.Visible = false;
            SetRdUdf();

            tView = tResult.AsEnumerable()
               .GroupBy(r => new
               {
                   k1 = r["cuno"].ToString(),
                   k2 = r["sano"].ToString(),
                   k3 = r["單據"].ToString()
               })
               .Select(g => g.First())
               .CopyToDataTable();

            tView.DefaultView.Sort = "cuno,帳款日期,sano";
           // dataGridViewT1.DataSource = tView.DefaultView;
            dataGridViewT1.DataSource = tView;
            writeToTitle(0);
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        void writeToTitle(int index)
        {
            try
            {
                var cuno = dy.ElementAt(index).Key;
                string A = cuno.Replace("'", "''");
                tView.DefaultView.RowFilter = "CuNo = '" + A + "'";

                textBoxT1.Text = cuno;
                xe.Validate<JBS.JS.Cust>(cuno, r => textBoxT2.Text = r["cuname1"].ToString(), () => textBoxT2.Clear());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            if (dataGridViewT1.Rows.Count > 0)
            {
                var a = tView.DefaultView[0]["前期總金額"].ToDecimal();
                var b = tView.DefaultView[0]["本期應收總額"].ToDecimal();

                textBoxT3.Text = a.ToString();
                textBoxT4.Text = tView.DefaultView[0]["交易總筆數"].ToString();
                textBoxT5.Text = tView.DefaultView[0]["本期稅前金額"].ToString();
                textBoxT6.Text = tView.DefaultView[0]["本期營業稅額"].ToString();
                textBoxT7.Text = tView.DefaultView[0]["本期單據總額"].ToString();

                textBoxT8.Text = tView.DefaultView[0]["本期折扣金額"].ToString();
                textBoxT9.Text = tView.DefaultView[0]["本期已收預收"].ToString();
                textBoxT10.Text = tView.DefaultView[0]["本期應收總額"].ToString();
                textBoxT11.Text = (a + b).ToString();
            }
            else
            {
                textBoxT3.Text = dy.ElementAt(index).Value.ToString("f" + Common.MST);
                textBoxT4.Text = "0";
                textBoxT5.Text = "0";
                textBoxT6.Text = "0";
                textBoxT7.Text = "0";

                textBoxT8.Text = "0";
                textBoxT9.Text = "0";
                textBoxT10.Text = "0";
                textBoxT11.Text = dy.ElementAt(index).Value.ToString("f" + Common.MST);
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToTitle(0);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var cuno = textBoxT1.Text.Trim();
            var index = -1;
            Parallel.For(0, dy.Count, (i, loopstate) =>
            {
                if (dy.ElementAt(i).Key == cuno)
                {
                    index = i;
                    loopstate.Stop();
                }
            });

            if (--index <= 0)
                index = 0;
            writeToTitle(index);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var cuno = textBoxT1.Text.Trim();
            var index = -1;
            Parallel.For(0, dy.Count, (i, loopstate) =>
            {
                if (dy.ElementAt(i).Key == cuno)
                {
                    index = i;
                    loopstate.Stop();
                }
            });

            if (++index >= dy.Count - 1)
                index = dy.Count - 1;
            writeToTitle(index);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToTitle(dy.Count - 1);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dy.Clear();
            tView.Clear();
            tResult.Clear();

            this.Dispose();
        }

        string 判斷開啟報表()
        {
            string str = "";
            if (!radio4.Checked) str += radioT1.Checked ? "O" : "";
            str += radioT5.Checked ? "A" : "B";
            string path = "";
            if (Common.Sys_DBqty == 1)
            {
                if (radio1.Checked)
                {
                    if (File.Exists(Common.reportaddress + "Report\\CustAccRpt1.rpt") || File.Exists(Common.reportaddress + "Report\\CustAccRpt1.frx"))
                        path = Common.reportaddress + "Report\\CustAccRpt1";
                    else
                        path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表" + str + "_明細表一";
                }
                else if (radio2.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表" + str + "_明細表二";
                }
                else if (radio3.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表" + str + "_明細表三";
                }
                else if (radio4.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表" + str + "_簡要表";
                }
                else if (radio5.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表_明細自定";
                }
                else if (radio6.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表_組合自定";
                }
                else if (radio7.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表_應收分組表";
                }
                else if (radio8.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表_簡要自定";
                }
            }
            else if (Common.Sys_DBqty == 2)
            {
                if (radio1.Checked)
                {
                    if (File.Exists(Common.reportaddress + "Report\\CustAccRpt1.rpt") || File.Exists(Common.reportaddress + "Report\\CustAccRpt1.frx"))
                        path = Common.reportaddress + "Report\\CustAccRpt1";
                    else
                        path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表" + str + "_明細表一P";
                }
                else if (radio2.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表" + str + "_明細表二P";
                }
                else if (radio3.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表" + str + "_明細表三P";
                }
                else if (radio4.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表" + str + "_簡要表";
                }
                else if (radio5.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表_明細自定";
                }
                else if (radio6.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表_組合自定";
                }
                else if (radio7.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表_應收分組表";
                }
                else if (radio8.Checked)
                {
                    path = Common.reportaddress + "Report\\客戶別應收帳款_明細簡要表_簡要自定";
                }
            }
            return path;
        }
        void OutReport(RptMode mode)
        {
            
            
            ReportDocument oRpt = new ReportDocument();

            var getPath = 確認CF或FF(判斷開啟報表());
            if (getPath.EndsWith("frx"))
            {
                oRpt.Dispose();
                FastReport列印(getPath, mode);
                return;
            }

            string txtend = "";
            if (radioT7.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
            else if (radioT9.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
            else if (radioT10.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
            else if (radioT11.Checked) txtend = Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[19]["tamemo"].ToString();
            else txtend = "";

            RPT rp = new RPT(tResult, getPath);
            oRpt.Load(getPath);
            List<TextObject> Txt = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();
            string address="",tel="";
            //公司抬頭
            #region
            if (Txt.Find(t => t.Name == "txtstart") != null)
            {
                if (rdHeader1.Checked)
                { 
                    rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[0]["pnname"].ToString() });
                    address = Common.dtstart.Rows[0]["pnaddr"].ToString();
                    tel = "TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
                }
                else if (rdHeader2.Checked)
                { 
                    rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[1]["pnname"].ToString() });
                    address = Common.dtstart.Rows[1]["pnaddr"].ToString();
                    tel = "TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
                }
                else if (rdHeader3.Checked)
                {
                    rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[2]["pnname"].ToString() });
                    address = Common.dtstart.Rows[2]["pnaddr"].ToString();
                    tel = "TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
                }
                else if (rdHeader4.Checked)
                {
                    rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[3]["pnname"].ToString() });
                    address = Common.dtstart.Rows[3]["pnaddr"].ToString();
                    tel = "TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
                }
                else if (rdHeader5.Checked)
                {
                    rp.lobj.Add(new string[] { "txtstart", Common.dtstart.Rows[4]["pnname"].ToString() });
                    address = Common.dtstart.Rows[4]["pnaddr"].ToString();
                    tel = "TEL："+Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
                }
                else
                {
                    rp.lobj.Add(new string[] { "txtstart", "" });
                    address = "";
                    tel = "";
                }
            }
                       
            rp.office = "客戶別應收帳款";
            rp.lobj.Add(new string[] { "txttel", tel });
            rp.lobj.Add(new string[] { "txtaddress", address });
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "txtRange", DateRange });
            rp.lobj.Add(new string[] { "txttoday", Date.GetDateTime(Common.User_DateTime, true) });
            #endregion

            if (mode == RptMode.Print)
                rp.Print();
            else if (mode == RptMode.PreView)
                rp.PreView();
            else if (mode == RptMode.Word)
                rp.Word();
            else if (mode == RptMode.Excel)
                rp.Excel();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT4);
            pnlist.Add(groupBoxT5);
            pVar.SaveRadioUdf(pnlist, "FrmCust_Accb");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT4);
            pnlist.Add(groupBoxT5);
            pVar.SetRadioUdf(pnlist, "FrmCust_Accb");
        }

        private void radio4_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxT2.Enabled = !radio4.Checked;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                tView.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        string 確認CF或FF(string path)
        {
            var testPath = path.Replace("Report", "ReportNew") + ".frx";
            if (File.Exists(testPath))
            {
                return testPath;
            }

            testPath = path + ".rpt";
            if (File.Exists(testPath))
            {
                return testPath;
            }

            return null;
        }

        void FastReport列印(string path, RptMode mode)
        {
            //三行註腳
            string txtend = "";
            if (radioT7.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
            else if (radioT9.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
            else if (radioT10.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
            else if (radioT11.Checked) txtend = Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[19]["tamemo"].ToString();
            else txtend = "";

            //公司抬頭
            #region
            string address = "";
            string tel = "";
            string txtstart = "";
            if (rdHeader1.Checked)
            {
                txtstart = Common.dtstart.Rows[0]["pnname"].ToString();
                address = Common.dtstart.Rows[0]["pnaddr"].ToString();
                tel = "TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
            }
            else if (rdHeader2.Checked)
            {
                txtstart = Common.dtstart.Rows[1]["pnname"].ToString();
                address = Common.dtstart.Rows[1]["pnaddr"].ToString();
                tel = "TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
            }
            else if (rdHeader3.Checked)
            {
                txtstart = Common.dtstart.Rows[2]["pnname"].ToString();
                address = Common.dtstart.Rows[2]["pnaddr"].ToString();
                tel = "TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
            }
            else if (rdHeader4.Checked)
            {
                txtstart =  Common.dtstart.Rows[3]["pnname"].ToString();
                address = Common.dtstart.Rows[3]["pnaddr"].ToString();
                tel = "TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
            }
            else if (rdHeader5.Checked)
            {
                txtstart =  Common.dtstart.Rows[4]["pnname"].ToString();
                address = Common.dtstart.Rows[4]["pnaddr"].ToString();
                tel = "TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
            }
            else
            {
                txtstart = "";
                address = "";
                tel = "";
            }
            #endregion

            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                FastReport.dy.Add("txtstart", txtstart);
                FastReport.dy.Add("txttel", tel );
                FastReport.dy.Add("txtaddress", address );
                FastReport.dy.Add("txtend", txtend );
                FastReport.dy.Add("txtRange", DateRange );
                FastReport.dy.Add("txttoday", Date.GetDateTime(Common.User_DateTime, true));
                FastReport.PreView(path, tResult, "Cust_receiv", null, null, mode, "客戶別應收帳款");
            }
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var path = 確認CF或FF(判斷開啟報表());
                if (path.EndsWith(".frx") == false) return;

                var dl = MessageBox.Show("是否要修改報表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl != DialogResult.Yes) return;

                JBS.FReport.Design(path);
            }
        }
    }
}
