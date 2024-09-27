using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmEmpl_Accb : Formbase
    {
        public DataTable tTitle = new DataTable();
        public DataTable dt = new DataTable();
        public string DateRange = "";
        RPT rp;
        List<DataRow> list = new List<DataRow>();
        DataRow dr = null;
        public string spname = "";

        public FrmEmpl_Accb()
        {
            InitializeComponent();
        }

        private void FrmEmpl_Accb_Load(object sender, EventArgs e)
        {
            radioT3.Enabled = radioT4.Enabled = false;
            rd1.Checked = radioT1.Checked = radioT5.Checked = radioT7.Checked = true; 

            if (Common.Sys_DBqty == 1)
            {
                rd5.SetUserDefineRpt("採購員應付帳款_明細簡要表_明細自定.rpt");
                rd6.SetUserDefineRpt("採購員應付帳款_明細簡要表_組合自定.rpt");
                rd7.SetUserDefineRpt("採購員應付帳款_明細簡要表_應付分組.rpt");
                radio8.SetUserDefineRpt("採購員應付帳款_明細簡要表_簡要自定.rpt");
            }
            else
            {
                rd5.SetUserDefineRpt("採購員應付帳款_明細簡要表_明細自定P.rpt");
                rd6.SetUserDefineRpt("採購員應付帳款_明細簡要表_組合自定P.rpt");
                rd7.SetUserDefineRpt("採購員應付帳款_明細簡要表_應付分組P.rpt");
                radio8.SetUserDefineRpt("採購員應付帳款_明細簡要表_簡要自定P.rpt");
            }
      
            if (!dt.Columns.Contains("帳款日期")) dt.Columns.Add("帳款日期", typeof(string));
            if (!dt.Columns.Contains("已付預付")) dt.Columns.Add("已付預付", typeof(string));
            dt.AcceptChanges();

            this.稅前金額.DefaultCellStyle.Format = "f" + Common.TPF;
            this.營業稅額.DefaultCellStyle.Format = "f" + Common.TF;
            this.應付總計.DefaultCellStyle.Format = "f" + Common.MFT;
            this.折扣金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.已付加預付.DefaultCellStyle.Format = "f" + Common.MFT;
            this.本期應付.DefaultCellStyle.Format = "f" + Common.MFT;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Common.User_DateTime == 1)
                {
                    dt.Rows[i]["帳款日期"] = Date.AddLine(dt.Rows[i]["bsdateac"].ToString());
                }
                else if (Common.User_DateTime == 2)
                {
                    dt.Rows[i]["帳款日期"] = Date.AddLine(dt.Rows[i]["bsdateac1"].ToString());
                }
                dt.Rows[i]["已付預付"] = dt.Rows[i]["CollectMny"].ToDecimal() + dt.Rows[i]["GetPrvAcc"].ToDecimal();

            }

            tTitle = tTitle.AsEnumerable().OrderBy(r => r["emno"].ToString()).CopyToDataTable();
            list = tTitle.AsEnumerable().ToList();
            dr = list.FirstOrDefault();
            WriteToTitle(dr);
            DataTable beforemny;
            //寫入前期總金額
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                beforemny = tTitle.AsEnumerable().ToList().Where(r => r["fano"].ToString() == dt.Rows[i]["fano"].ToString()).CopyToDataTable();
                dt.Rows[i]["前期總金額"] = beforemny.Rows[0]["前期總金額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["交易總筆數"] = beforemny.Rows[0]["交易總筆數"].ToDecimal().ToString("f" + Common.Q);
                dt.Rows[i]["稅前總金額"] = beforemny.Rows[0]["稅前總金額"].ToDecimal().ToString("f" + Common.TPF);
                dt.Rows[i]["營業稅總額"] = beforemny.Rows[0]["營業稅總額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["應付總金額"] = beforemny.Rows[0]["應付總金額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["折扣總金額"] = beforemny.Rows[0]["折扣總金額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["已付加預付"] = beforemny.Rows[0]["已付加預付"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["本期總金額"] = beforemny.Rows[0]["本期總金額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["前期加本期"] = beforemny.Rows[0]["前期加本期"].ToDecimal().ToString("f" + Common.MFT);

            }

            groupBoxT3.Visible = false;//************************************************************
        }

        void WriteToTitle(DataRow dr)
        {
            textBoxT1.Text = dr["fano"].ToString();
            textBoxT2.Text = dr["faname1"].ToString();
            textBoxT12.Text = dr["emno"].ToString();
            textBoxT13.Text = dr["emname"].ToString();

            textBoxT3.Text = dr["前期總金額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT4.Text = dr["交易總筆數"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT5.Text = dr["稅前總金額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT6.Text = dr["營業稅總額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT7.Text = dr["應付總金額"].ToDecimal().ToString("f" + Common.MFT);

            textBoxT8.Text = dr["折扣總金額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT9.Text = dr["已付加預付"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT10.Text = dr["本期總金額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT11.Text = dr["前期加本期"].ToDecimal().ToString("f" + Common.MFT);

            var rows = dt.AsEnumerable().Where(r => r["fano"].ToString().Trim() == dr["fano"].ToString().Trim() && r["emno"].ToString().Trim() == dr["emno"].ToString().Trim());
            if (rows.Count() > 0)
            {
                DataTable temp = dt.Clone();
                string sano = "", mode = "";
                for (int i = 0; i < rows.Count(); i++)
                {
                    sano = rows.ElementAt(i)["bsno"].ToString().Trim();
                    mode = rows.ElementAt(i)["單據"].ToString().Trim();
                    if (temp.AsEnumerable().Count(r => r["bsno"].ToString().Trim() == sano && r["單據"].ToString().Trim() == mode) > 0) continue;
                    else
                    {
                        temp.ImportRow(rows.ElementAt(i));
                    }
                }
                temp = temp.AsEnumerable().OrderBy(r => r["bsdateac"].ToString()).CopyToDataTable();

                dataGridViewT1.DataSource = temp;
            }
            else dataGridViewT1.DataSource = null;
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            dr = list.FirstOrDefault();
            WriteToTitle(dr);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = list.FindIndex(r => r["fano"].ToString().Trim() == dr["fano"].ToString().Trim() && r["emno"].ToString().Trim() == dr["emno"].ToString().Trim());
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
            var index = list.FindIndex(r => r["fano"].ToString().Trim() == dr["fano"].ToString().Trim() && r["emno"].ToString().Trim() == dr["emno"].ToString().Trim());
            if (index == list.Count() - 1)
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
            if (!rd4.Checked) str += radioT1.Checked ? "O" : "";
            str += radioT5.Checked ? "A" : "B";

            if (Common.Sys_DBqty == 1)
            {
                if (rd1.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_明細表一.rpt";
                }
                else if (rd2.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_明細表二.rpt";
                }
                else if (rd3.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_明細表三.rpt";
                }
                else if (rd4.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_簡要表.rpt";
                }
                else if (rd5.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_明細自定.rpt";
                }
                else if (rd6.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_組合自定.rpt";
                }
                else if (rd7.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_應付分組.rpt";
                }
                else if (radio8.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_簡要自定.rpt";
                }
            }
            else if (Common.Sys_DBqty == 2)
            {
                if (rd1.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_明細表一P.rpt";
                }
                else if (rd2.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_明細表二P.rpt";
                }
                else if (rd3.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_明細表三P.rpt";
                }
                else if (rd4.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_簡要表.rpt";
                }
                else if (rd5.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_明細自定.rpt";
                }
                else if (rd6.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_組合自定.rpt";
                }
                else if (rd7.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_應付分組.rpt";
                }
                else if (radio8.Checked)
                {
                    path = Common.reportaddress + "Report\\採購員應付帳款_明細簡要表" + str + "_簡要自定.rpt";
                }
            }

            string address = Common.dtstart.Rows[0]["pnaddr"].ToString();
            string tel = Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();

            string txtend = "";
            if (radioT7.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString();
            else if (radioT9.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString();
            else if (radioT10.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString();
            else if (radioT11.Checked) txtend = Common.dtEnd.Rows[17]["tamemo"].ToString();
            else txtend = "";
             
             
            rp = new RPT(dt, path);

            rp.office = "廠商別應付帳款";
            if (spname.Trim().Length > 0)
            {
                rp.lobj.Add(new string[] { "txtstart", spname });
            }
            rp.lobj.Add(new string[] { "txttel", "TEL：" + tel });
            rp.lobj.Add(new string[] { "txtaddress", address });
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "txtRange", DateRange });
            rp.lobj.Add(new string[] { "txttoday", Date.GetDateTime(Common.User_DateTime, true) });
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

        private void rd4_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxT2.Enabled = !rd4.Checked;
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
