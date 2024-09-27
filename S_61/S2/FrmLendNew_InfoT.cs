using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmLendNew_InfobT : Formbase
    {
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        public DataTable dtD = new DataTable();
        public string DateRange { get; set; }

        string DateCreated;
        List<string> list;

        public FrmLendNew_InfobT()
        {
            InitializeComponent();

        }

        private void FrmLendNew_InfobT_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dt.DefaultView;
            list = dt.AsEnumerable().GroupBy(r => r["cuno"].ToString()).Select(g => g.Key).ToList();
            btnTop_Click(null, null);

            radioT3.SetUserDefineRpt("借出還入資料瀏覽_明細自定一.rpt");
            SetRdUdf();
        }

        void writeToText(string cuno)
        {
            dt.DefaultView.RowFilter = "CuNo = '" + cuno + "'";

            if (dataGridViewT1.Rows.Count == 0) CuNo.Text = CuName1.Text = "";
            else
            {
                CuNo.Text = dt.DefaultView[0]["CuNo"].ToString();
                CuName1.Text = dt.DefaultView[0]["CuName1"].ToString();
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToText(list.First());
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(CuNo.Text.Trim()) - 1;
            if (index <= -1) index = 0;
            writeToText(list.ElementAt(index));
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = list.IndexOf(CuNo.Text.Trim()) + 1;
            if (index >= list.Count - 1) index = list.Count - 1;
            writeToText(list.ElementAt(index));
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToText(list.Last());
        }

        void OutReport(RptMode mode)
        {
            switch (Common.User_DateTime)
            {
                case 1:
                    DateCreated = Date.GetDateTime(1, true);
                    break;
                case 2:
                    DateCreated = Date.GetDateTime(2, true);
                    break;
            }

            RPT rp;
            var path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\借出還入資料瀏覽_總表.rpt";
                rp = new RPT(dt, path);

                rp.lobj.Add(new string[] { "DateRange", DateRange });
                rp.lobj.Add(new string[] { "DateCreated", "製表日期：" + DateCreated });

                if (mode == RptMode.Print)
                {
                    rp.Print();
                }
                else if (mode == RptMode.PreView)
                {
                    rp.PreView();
                }
                else if (mode == RptMode.Word)
                {
                    rp.Word();
                }
                else if (mode == RptMode.Excel)
                {
                    rp.Excel();
                }
            }
            else if (radioT2.Checked)
            {
                var count = dt.AsEnumerable().GroupBy(r => r["cuno"].ToString().Trim()).Count();
                if (count != 1)
                {
                    MessageBox.Show("此報表為子母報表，一次只能查詢一個客戶資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                path = Common.reportaddress + "Report\\借出還入資料瀏覽_明細表.rpt";
                rp = new RPT(dtD, path);

                rp.lobj.Add(new string[] { "DateRange", DateRange });
                rp.lobj.Add(new string[] { "DateCreated", "製表日期：" + DateCreated });

                var name = "借出還入資料瀏覽_明細表_總表.rpt";
                if (mode == RptMode.Print)
                {
                    rp.Print(name, dt);
                }
                else if (mode == RptMode.PreView)
                {
                    rp.PreView(name, dt);
                }
                else if (mode == RptMode.Word)
                {
                    rp.Word(name, dt);
                }
                else if (mode == RptMode.Excel)
                {
                    rp.Excel(name, dt);
                }
            }
            else
            {
                if (Common.Sys_LendToSaleMode == 2)
                {
                    //弘恩
                    var count = dt.AsEnumerable().GroupBy(r => r["cuno"].ToString().Trim()).Count();
                    if (count != 1)
                    {
                        MessageBox.Show("此報表為子母報表，一次只能查詢一個客戶資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (ds.Tables.Count == 0)
                    {
                        dt.TableName = "LendInfo";
                        ds.Tables.Add(dt);

                        using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                        using (SqlCommand cmd = cn.CreateCommand())
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            cn.Open();
                            cmd.CommandText = @"
                                SELECT 
                                itno, itnoudf, itname FROM item ";
                            da.Fill(ds, "item");
                        }
                    }

                    path = Common.reportaddress + "Report\\借出還入資料瀏覽_明細自定一.rpt";
                    rp = new RPT(ds, path);

                    rp.lobj.Add(new string[] { "DateRange", DateRange });
                    rp.lobj.Add(new string[] { "DateCreated", "製表日期：" + DateCreated });
                     
                    if (mode == RptMode.Print)
                    {
                        rp.Print(1);
                    }
                    else if (mode == RptMode.PreView)
                    {
                        rp.PreView(1);
                    }
                    else if (mode == RptMode.Word)
                    {
                        rp.Word(1);
                    }
                    else if (mode == RptMode.Excel)
                    {
                        rp.Excel(1);
                    }
                }
                else
                {
                    var count = dt.AsEnumerable().GroupBy(r => r["cuno"].ToString().Trim()).Count();
                    if (count != 1)
                    {
                        MessageBox.Show("此報表為子母報表，一次只能查詢一個客戶資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    List<string> list = dt.AsEnumerable().Where(r => r["前期加本期"].ToDecimal() == 0).Select(r => r["itno"].ToString().Trim()).Distinct().ToList();

                    var InTotal = dt.Clone();

                    var no = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        no = dt.Rows[i]["itno"].ToString().Trim();
                        if (list.IndexOf(no) != -1)
                        {
                            continue;
                        }
                        else
                        {
                            InTotal.ImportRow(dt.Rows[i]);
                        }
                    }

                    path = Common.reportaddress + "Report\\借出還入資料瀏覽_明細自定一.rpt";
                    rp = new RPT(dtD, path);

                    rp.lobj.Add(new string[] { "DateRange", DateRange });
                    rp.lobj.Add(new string[] { "DateCreated", "製表日期：" + DateCreated });

                    var name = "借出還入資料瀏覽_明細表_總表.rpt";
                    if (mode == RptMode.Print)
                    {
                        rp.Print(name, InTotal);
                    }
                    else if (mode == RptMode.PreView)
                    {
                        rp.PreView(name, InTotal);
                    }
                    else if (mode == RptMode.Word)
                    {
                        rp.Word(name, InTotal);
                    }
                    else if (mode == RptMode.Excel)
                    {
                        rp.Excel(name, InTotal);
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
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
            else if (keyData == Keys.Escape)
            {
                btnExit_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pVar.SaveRadioUdf(pnlist, this.Name);
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pVar.SetRadioUdf(pnlist, this.Name);
        }


    }
}
