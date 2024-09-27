using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using FastReport;

namespace S_61.subMenuFm_3
{
    public partial class FrmEmplCust_Accb : Formbase
    {
        public DataTable dtTitle = new DataTable();
        public DataTable dt = new DataTable();
        public DataTable dtDetail = new DataTable();
        public List<string[]> list = new List<string[]>();

        string[] dr = null;

        RPT rp;
        public string DateRange = "";

        public FrmEmplCust_Accb()
        {
            InitializeComponent();

            this.稅前金額.DefaultCellStyle.Format = "f" + Common.MST;
            this.營業稅額.DefaultCellStyle.Format = "f" + Common.TS;
            this.應收總計.DefaultCellStyle.Format = "f" + Common.MST;
            this.折扣金額.DefaultCellStyle.Format = "f" + Common.MST; 
            this.已收加預收.DefaultCellStyle.Format = "f" + Common.MST;
            this.本期應收.DefaultCellStyle.Format = "f" + Common.MST;
        }

        private void FrmEmplCust_Accb_Load(object sender, EventArgs e)
        {
            radio1.Checked = true;
            radioT1.Checked = radioT3.Checked = radioT5.Checked = radioT7.Checked = true;

            this.帳款日期.DataPropertyName = Common.User_DateTime == 1 ? "sadate" : "sadate1";
            radio5.SetUserDefineRpt("業務別應付帳款明細自定.rpt");
            radio6.SetUserDefineRpt("業務別應付帳款組合自定.rpt");
            radio7.SetUserDefineRpt("業務別應付帳款簡要自定.rpt");

            dr = list.FirstOrDefault();
            WriteToTitle(dr);

            groupBoxT3.Visible = false;
            dataGridViewT1.DataSource = dt.DefaultView;
        }

        private void WriteToTitle(string[] dr)
        {
            var p = dtTitle.AsEnumerable().ToList().Find(r => r["emno"].ToString().Trim() == dr[0] && r["cuno"].ToString().Trim() == dr[1]);
            var t = dt.AsEnumerable().ToList().Where(r => r["emno"].ToString().Trim() == dr[0] && r["cuno"].ToString().Trim() == dr[1]);
            if (p.IsNotNull())
            {
                textBoxT1.Text = p["emno"].ToString().Trim();
                textBoxT2.Text = p["emname"].ToString().Trim();
                textBoxT3.Text = p["cuno"].ToString().Trim();
                textBoxT4.Text = p["cuname1"].ToString().Trim();
                textBoxT5.Text = p["前期總金額"].ToDecimal().ToString("f" + Common.MST);
                textBoxT6.Text = p["筆數"].ToString();
                textBoxT7.Text = p["稅前總金額"].ToDecimal().ToString("f" + Common.MST);
                textBoxT8.Text = p["營業稅總額"].ToDecimal().ToString("f" + Common.TS);
                textBoxT9.Text = p["應收總金額"].ToDecimal().ToString("f" + Common.MST);
                textBoxT10.Text = p["折扣總金額"].ToDecimal().ToString("f" + Common.MST);
                textBoxT11.Text = p["已收加預收"].ToDecimal().ToString("f" + Common.MST);
                textBoxT12.Text = p["本期總金額"].ToDecimal().ToString("f" + Common.MST);
                textBoxT13.Text = p["前期加本期"].ToDecimal().ToString("f" + Common.MST);

                dt.DefaultView.Sort = "sadate ASC";
                dt.DefaultView.RowFilter = "emno='" + p["emno"].ToString().Trim() + "' AND cuno='" + p["cuno"].ToString().Trim() + "'";
            }
            else
            {
                dt.DefaultView.RowFilter = "1=0";
                foreach (var tb in this.Controls.OfType<TextBox>()) tb.Clear();
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            dr = list.FirstOrDefault();
            WriteToTitle(dr);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = list.FindIndex(r => r[0] == dr[0] && r[1] == dr[1]);
            if (index == 0)
            {
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dr = list[--index];
                WriteToTitle(dr);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = list.FindIndex(r => r[0] == dr[0] && r[1] == dr[1]);
            if (index == list.Count - 1)
            {
                MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dr = list[++index];
                WriteToTitle(dr);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            dr = list.LastOrDefault();
            WriteToTitle(dr);
        }

        void paramsInit()
        {
            string path = "";
            string str = "";
            if (!radio4.Checked) str += radioT1.Checked ? "O" : "";
            str += radioT5.Checked ? "A" : "B";

            if (Common.Sys_DBqty == 1)
            {
                if (radio1.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款明細表一" + str + ".rpt";
                }
                else if (radio2.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款明細表二" + str + ".rpt";
                }
                else if (radio3.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款明細表三" + str + ".rpt";
                }
                else if (radio4.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款簡要表" + str + ".rpt";
                }
                else if (radio5.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款明細自定" + str + ".rpt";
                }
                else if (radio6.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款組合自定" + str + ".rpt";
                }
                else if (radio7.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款簡要自定" + str + ".rpt";
                }
            }
            else if (Common.Sys_DBqty == 2)
            {
                if (radio1.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款明細表一P" + str + ".rpt";
                }
                else if (radio2.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款明細表二P" + str + ".rpt";
                }
                else if (radio3.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款明細表三P" + str + ".rpt";
                }
                else if (radio4.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款簡要表" + str + ".rpt";
                }
                else if (radio5.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款明細自定" + str + ".rpt";
                }
                else if (radio6.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款組合自定" + str + ".rpt";
                }
                else if (radio7.Checked)
                {
                    path = Common.reportaddress + "Report\\業務別應收帳款簡要自定" + str + ".rpt";
                }
            }

            //string address = Common.dtstart.Rows[0]["pnaddr"].ToString();
            // string tel = Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
            string address = "", tel = "";
            string txtend = "";
            if (radioT7.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
            else if (radioT9.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
            else if (radioT10.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
            else if (radioT11.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
            else txtend = "";

            if (radio1.Checked) rp = new RPT(dtDetail, path);
            else if (radio2.Checked) rp = new RPT(dtDetail, path);
            else if (radio3.Checked) rp = new RPT(dtDetail, path);
            else if (radio4.Checked) rp = new RPT(dt, path);
            else if (radio5.Checked) rp = new RPT(dtDetail, path);
            else if (radio6.Checked) rp = new RPT(dtDetail, path);
            else if (radio7.Checked) rp = new RPT(dt, path);

            rp.office = "業務別應收帳款明細簡要表";

            //公司抬頭
            #region
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
                tel = "TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
            }
            else
            {
                rp.lobj.Add(new string[] { "txtstart", "" });
                address = "";
                tel = "";
            }
            #endregion

            rp.lobj.Add(new string[] { "address", address });
            rp.lobj.Add(new string[] { "tel", tel });
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
            rp.lobj.Add(new string[] { "CreateDate", Date.GetDateTime(Common.User_DateTime, true) });
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

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Word();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Excel();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                dt.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
