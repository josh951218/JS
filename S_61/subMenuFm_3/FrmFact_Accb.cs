using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.IO;

namespace S_61.subMenuFm_3
{
    public partial class FrmFact_Accb : Formbase
    {
        DataTable temp = new DataTable();

        List<string> list = new List<string>();
        public DataTable tTitle = new DataTable();
        public DataTable dt = new DataTable();
        public string DateRange = "";
        RPT rp;
        List<DataRow> li = new List<DataRow>();
        public string spname = "";

        public FrmFact_Accb()
        {
            InitializeComponent();

            radio5.SetUserDefineRpt("廠商別應付帳款_明細簡要表_明細自定.rpt");
            radio6.SetUserDefineRpt("廠商別應付帳款_明細簡要表_組合自定.rpt");
            radio7.SetUserDefineRpt("廠商別應付帳款_明細簡要表_應付分組表.rpt");
            radio8.SetUserDefineRpt("廠商別應付帳款_明細簡要表_簡要自訂.rpt");

            this.稅前金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.營業稅額.DefaultCellStyle.Format = "f" + Common.TF;
            this.應付總計.DefaultCellStyle.Format = "f" + Common.MFT;
            this.折扣金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.已付加預付.DefaultCellStyle.Format = "f" + Common.MFT;
            this.本期應付.DefaultCellStyle.Format = "f" + Common.MFT;

        }

        private void FrmFact_Accb_Load(object sender, EventArgs e)
        {
            if (!dt.Columns.Contains("帳款日期")) dt.Columns.Add("帳款日期", typeof(string));
            if (!dt.Columns.Contains("已付預付")) dt.Columns.Add("已付預付", typeof(string));
            dt.AcceptChanges();

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

            //寫入前期總金額
            //DataTable beforemny;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = tTitle.AsEnumerable().Where(r => r["fano"].ToString() == dt.Rows[i]["fano"].ToString()).First();

                dt.Rows[i]["前期總金額"] = row["前期總金額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["交易總筆數"] = row["交易總筆數"].ToDecimal().ToString("f" + Common.Q);
                dt.Rows[i]["稅前總金額"] = row["稅前總金額"].ToDecimal().ToString("f" + Common.TPF);
                dt.Rows[i]["營業稅總額"] = row["營業稅總額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["應付總金額"] = row["應付總金額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["折扣總金額"] = row["折扣總金額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["已付加預付"] = row["已付加預付"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["本期總金額"] = row["本期總金額"].ToDecimal().ToString("f" + Common.MFT);
                dt.Rows[i]["前期加本期"] = row["前期加本期"].ToDecimal().ToString("f" + Common.MFT);
            }

            temp = dt.Clone();
            dt.DefaultView.Sort = " bsdateac ASC";

            list = tTitle.AsEnumerable().Select(r => r["fano"].ToString().Trim()).Distinct().ToList();
            WriteToTitle(list.First());

            SetRdUdf();
        }

        void WriteToTitle(string fano)
        {
            var row = tTitle.AsEnumerable().Where(r => r["fano"].ToString().Trim() == fano).First();

            textBoxT1.Text = row["fano"].ToString().Trim();
            textBoxT2.Text = row["faname1"].ToString();

            textBoxT3.Text = row["前期總金額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT4.Text = row["交易總筆數"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT5.Text = row["稅前總金額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT6.Text = row["營業稅總額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT7.Text = row["應付總金額"].ToDecimal().ToString("f" + Common.MFT);

            textBoxT8.Text = row["折扣總金額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT9.Text = row["已付加預付"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT10.Text = row["本期總金額"].ToDecimal().ToString("f" + Common.MFT);
            textBoxT11.Text = row["前期加本期"].ToDecimal().ToString("f" + Common.MFT);

            List<MyClass> lt = new List<MyClass>();

            temp.Clear();
            var rows = dt.AsEnumerable().Where(r => r["fano"].ToString().Trim() == fano);
            for (int i = 0; i < rows.Count(); i++)
            {
                var bsno = rows.ElementAt(i)["bsno"].ToString().Trim();
                var 單據 = rows.ElementAt(i)["單據"].ToString().Trim();
                if (lt.Any(l => l.bsno == bsno && l.單據 == 單據) == false)
                {
                    MyClass mc = new MyClass();
                    mc.bsno = bsno;
                    mc.單據 = 單據;
                    lt.Add(mc);

                    temp.ImportRow(rows.ElementAt(i));
                }
            }
            dataGridViewT1.DataSource = temp;
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToTitle(list.First());
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(textBoxT1.Text.Trim()) - 1;

            if (index <= 0)
                WriteToTitle(list.First());
            else
                WriteToTitle(list.ElementAt(index));
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(textBoxT1.Text.Trim()) + 1;

            if (index >= list.Count - 1)
                WriteToTitle(list.Last());
            else
                WriteToTitle(list.ElementAt(index));
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToTitle(list.Last());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string 判斷開啟報表()
        {
            string path = "";
            string str = "";
            if (!radio4.Checked) str += radioT1.Checked ? "O" : "";
            str += radioT5.Checked ? "A" : "B";
            if (Common.Sys_DBqty == 1)
            {
                if (radio1.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表" + str + "_明細表一";
                }
                else if (radio2.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表" + str + "_明細表二";
                }
                else if (radio3.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表" + str + "_明細表三";
                }
                else if (radio4.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表" + str + "_簡要表";
                }
                else if (radio5.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表_明細自定";
                }
                else if (radio6.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表_組合自定";
                }
                else if (radio7.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表_應付分組";
                }
                else if (radio8.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表_簡要自定";
                }
            }
            else if (Common.Sys_DBqty == 2)
            {
                if (radio1.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表" + str + "_明細表一P";
                }
                else if (radio2.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表" + str + "_明細表二P";
                }
                else if (radio3.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表" + str + "_明細表三P";
                }
                else if (radio4.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表" + str + "_簡要表";
                }
                else if (radio5.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表_明細自定";
                }
                else if (radio6.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表_組合自定";
                }
                else if (radio7.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表_應付分組";
                }
                else if (radio8.Checked)
                {
                    path = Common.reportaddress + "Report\\廠商別應付帳款_明細簡要表_簡要自定";
                }
            }
            return path;
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
        void paramsInit(RptMode mode)
        {
            var getPath = 確認CF或FF(判斷開啟報表());
            if (getPath.EndsWith("frx"))
            {
                FastReport列印(getPath, mode);
                return;
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

            //rp = new RPT(dt, path);
            dt.TableName = "bshop";
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = " Select * from fact ";
                da.Fill(ds, "fact");
            }
            rp = new RPT(ds, getPath);

            rp.office = "廠商別應付帳款";
            if (spname.Trim().Length > 0)
            {
                rp.lobj.Add(new string[] { "txtstart", spname });
            }
            rp.lobj.Add(new string[] { "txttel", "TEL："+tel });
            rp.lobj.Add(new string[] { "txtaddress", address });
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "txtRange", DateRange });
            rp.lobj.Add(new string[] { "txttoday", Date.GetDateTime(Common.User_DateTime, true) });

            if (mode == RptMode.Print) rp.Print(1);
            else if (mode == RptMode.PreView) rp.PreView(1);
            else if (mode == RptMode.Word) rp.Word(1);
            else if (mode == RptMode.Excel) rp.Excel(1);

            ds.Tables.Clear();
        }
        void FastReport列印(string path, RptMode mode)
        {
            string address = Common.dtstart.Rows[0]["pnaddr"].ToString();
            string tel = Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();

            //三行註腳
            string txtend = "";
            if (radioT7.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
            else if (radioT9.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
            else if (radioT10.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
            else if (radioT11.Checked) txtend = Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[19]["tamemo"].ToString();
            else txtend = "";

            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                if (spname.Trim().Length > 0)
                    FastReport.dy.Add("txtstart", spname);
                else
                    FastReport.dy.Add("txtstart", Common.Sys_StcPnName);
                FastReport.dy.Add( "txttel", "TEL：" + tel );
                FastReport.dy.Add("txtaddress", address );
                FastReport.dy.Add( "txtend", txtend );
                FastReport.dy.Add( "txtRange", DateRange );
                FastReport.dy.Add( "txttoday", Date.GetDateTime(Common.User_DateTime, true) );
                FastReport.dy["是否顯示金額"] = Common.User_ShopPrice;
                FastReport.PreView(path, dt, "bshop", null, null, mode, "廠商別應付帳款");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            paramsInit(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit(RptMode.Excel);
        }
         
        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT4);
            pnlist.Add(groupBoxT5);
            pVar.SaveRadioUdf(pnlist, "FrmFact_Accb");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pnlist.Add(groupBoxT4);
            pnlist.Add(groupBoxT5);
            pVar.SetRadioUdf(pnlist, "FrmFact_Accb");
        }

        class MyClass
        {
            public string bsno { get; set; }
            public string 單據 { get; set; }
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
                temp.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
