using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace S_61.S4
{
    public partial class FrmEmplSaleGroupMonth2 : Formbase
    {
        JBS.JS.xEvents xe;
        public List<DataTable> list = new List<DataTable>();

        DataTable dtD = new DataTable();
        string reportName = "業務銷售月份統計表_總表.rpt";

        public FrmEmplSaleGroupMonth2()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            dataGridViewT1.DefaultCellStyle.Format = "f2";
        }

        private void FrmSaleGroupMonth_Load(object sender, EventArgs e)
        {
            dtD.Columns.Add("emno", typeof(String));
            dtD.Columns.Add("emname", typeof(String));
            for (int i = 1; i <= 12; i++)
            {
                dtD.Columns.Add("m" + i + "月淨額", typeof(Decimal));
                dtD.Columns.Add("m" + i + "月成本", typeof(Decimal));
            }
            dtD.Columns.Add("小計", typeof(Decimal));

            var temp = dtD.Clone();

            list.AsParallel()
                .ForAll(tb =>
                {
                    var emno = tb.TableName;

                    ConcurrentQueue<object> cq = new ConcurrentQueue<object>();
                    cq.Enqueue(emno);
                    cq.Enqueue("");

                    var sum = 0M;
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        sum += tb.Rows[i]["銷貨淨額"].ToDecimal();
                        cq.Enqueue(tb.Rows[i]["銷貨淨額"]);
                        cq.Enqueue(tb.Rows[i]["銷貨成本"]);
                    }
                    cq.Enqueue(sum);

                    lock (dtD.Rows.SyncRoot)
                    {
                        temp.Rows.Add(cq.ToArray());
                    }
                });

            foreach (var row in temp.AsEnumerable())
            {
                var emno = row["emno"].ToString();
                xe.Validate<JBS.JS.Empl>(emno, r => row["emname"] = r["emname"]);
            }

            dtD = temp.AsEnumerable().OrderBy(r => r["emno"].ToString().Trim()).CopyToDataTable();
            dataGridViewT1.DataSource = dtD;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();
            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dtD.Excel匯出並開啟(this.Text);
            }
            if (keyData == (Keys.Escape))
            {
                dtD.Clear();
                //chart1.Dispose();

                this.Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void OutReport(RptMode mode)
        {
            var path = Common.reportaddress + @"Report\" + reportName;
            RPT rp = new RPT(dtD, path);

            var txtend = "";
            if (radioT1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            rp.lobj.Add(new string[] { "txtend", txtend });

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

        private void btnWord_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

    }
}
