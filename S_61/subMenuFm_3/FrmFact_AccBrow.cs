using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using JE.MyControl;
using S_61.Basic;
using System.Windows.Forms;

namespace S_61.subMenuFm_3
{
    public partial class FrmFact_AccBrow : Formbase
    {
        public DataTable dt = new DataTable();
        public string DateRange = "";
        public string spname = "";
        RPT rp;

        public FrmFact_AccBrow()
        {
            InitializeComponent();
        }

        private void FrmFact_AccBrow_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("自定一", typeof(String));//for 報表自定一
            radioT1.Checked = radioT6.Checked = true;

            radioT3.SetUserDefineRpt("廠商別應付帳款_本幣總額表_自定一.rpt");
            radioT4.SetUserDefineRpt("廠商別應付帳款_本幣總額表_自定二.rpt");
            radioT5.SetUserDefineRpt("廠商別應付帳款_本幣總額表_標籤自定一.rpt");
             
            dataGridViewT1.DataSource = dt;

            this.交易筆數.DefaultCellStyle.Format = "f0";
            this.前期帳款.DefaultCellStyle.Format = "f" + Common.M;
            this.稅前金額.DefaultCellStyle.Format = "f" + Common.M;
            this.營業稅額.DefaultCellStyle.Format = "f" + Common.M;
            this.應付總計.DefaultCellStyle.Format = "f" + Common.M;
            this.折扣金額.DefaultCellStyle.Format = "f" + Common.M;
            this.已付加預付.DefaultCellStyle.Format = "f" + Common.M;
            this.本期應付.DefaultCellStyle.Format = "f" + Common.M;
            this.前期加本期.DefaultCellStyle.Format = "f" + Common.M;

            var tRows = dt.AsEnumerable();
            textBoxT1.Text = tRows.Sum(r => r["前期總金額"].ToDecimal()).ToString("f" + Common.M);
            textBoxT2.Text = tRows.Sum(r => r["交易總筆數"].ToDecimal()).ToString("f0");
            textBoxT3.Text = tRows.Sum(r => r["稅前總金額"].ToDecimal()).ToString("f" + Common.M);

            textBoxT4.Text = tRows.Sum(r => r["營業稅總額"].ToDecimal()).ToString("f" + Common.M);
            textBoxT5.Text = tRows.Sum(r => r["應付總金額"].ToDecimal()).ToString("f" + Common.M);
            textBoxT6.Text = tRows.Sum(r => r["折扣總金額"].ToDecimal()).ToString("f" + Common.M);

            textBoxT7.Text = tRows.Sum(r => r["已付加預付"].ToDecimal()).ToString("f" + Common.M);
            textBoxT8.Text = tRows.Sum(r => r["本期總金額"].ToDecimal()).ToString("f" + Common.M);
            textBoxT9.Text = tRows.Sum(r => r["前期加本期"].ToDecimal()).ToString("f" + Common.M);
        }

        void paramsInit()
        {
            string path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\廠商別應付帳款_本幣總額表_稅前報表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\廠商別應付帳款_本幣總額表_稅後報表.rpt";
            }
            else if (radioT3.Checked)
            {
                path = Common.reportaddress + "Report\\廠商別應付帳款_本幣總額表_自定一.rpt";
                using (SqlConnection cn  = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        string sql = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sql = "select FaUdf1 from fact where fano=@fano";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fano", dt.Rows[i]["fano"].ToString().Trim());
                            cmd.CommandText = sql;
                            dt.Rows[i]["自定一"] = cmd.ExecuteScalar().ToString();
                        }
                    }
                }
            }

            string txtend = "";
            if (radioT4.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            else txtend = "";

            dt = dt.AsEnumerable().OrderBy(r => r["fano"].ToString()).CopyToDataTable();
            rp = new RPT(dt, path);
            rp.office = "廠商別應付帳款";
            if (spname.Trim().Length > 0)
            {
                rp.lobj.Add(new string[] { "txtstart", spname });
            }
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
             
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
