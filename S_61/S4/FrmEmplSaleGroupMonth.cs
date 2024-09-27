using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Collections.Generic;

namespace S_61.S4
{
    public partial class FrmEmplSaleGroupMonth : Formbase
    {
        JBS.JS.xEvents xe;
        public List<DataTable> list = new List<DataTable>();

        DataTable dtD = new DataTable();
        string reportName = "業務銷售月份統計表.rpt";
        string EmName = "";

        public FrmEmplSaleGroupMonth()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            dataGridViewT1.DefaultCellStyle.Format = "f2";
        }

        private void FrmSaleGroupMonth_Load(object sender, EventArgs e)
        {
            btnTop_Click(null, null);

            //dataGridViewT1.DataSource = dtD;

            //var 銷退金額 = 0M;
            //var 銷貨金額 = 0M;
            //var 銷貨淨額 = 0M;
            //var 銷貨成本 = 0M;
            //var 銷貨毛利 = 0M;
            //var 毛利率 = 0M;

            //foreach (DataRow row in dtD.Rows)
            //{
            //    銷退金額 += row["銷退金額"].ToDecimal();
            //    銷貨金額 += row["銷貨金額"].ToDecimal();

            //    銷貨成本 += row["銷貨成本"].ToDecimal();
            //}
            //銷貨淨額 = 銷退金額 + 銷貨金額;
            //銷貨毛利 = 銷貨淨額 - 銷貨成本;

            //if (銷貨淨額 != 0)
            //    毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

            //textBoxT1.Text = 銷退金額.ToString("f2");
            //textBoxT2.Text = 銷貨金額.ToString("f2");

            //textBoxT3.Text = 銷貨淨額.ToString("f2");
            //textBoxT4.Text = 銷貨成本.ToString("f2");

            //textBoxT5.Text = 銷貨毛利.ToString("f2");
            //textBoxT6.Text = 毛利率.ToString("f2");

            //ChartInitialize();
            //ShowChart();
        }

        void ChartInitialize()
        {
            chart1.SuspendLayout();

            var area = chart1.ChartAreas.First();
            var legend1 = chart1.Legends.First();
            var series1 = chart1.Series.First();
            var series2 = chart1.Series.Last();

            series1.LegendText = "銷貨淨額";
            series2.LegendText = "銷貨毛利";
            area.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount;
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = 13;
            area.AxisX.Interval = 1;

            area.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount;
            var max = dtD.AsEnumerable().Max(r => r["銷貨淨額"].ToDecimal("f0"));
            var min = dtD.AsEnumerable().Min(r => r["銷貨淨額"].ToDecimal("f0"));

            var span = 0M;
            if (max >= 0 && min >= 0)
                span = max;
            else if (max >= 0 && min < 0)
                span = max - min;
            else if (max < 0 && min < 0)
                span = min;

            var len = Math.Abs(span).ToString().Length;
            var per = Math.Pow(10, len - 1);

            area.AxisY.Interval = per;
            var maxY = max + per.ToDecimal();
            var maxValue = (maxY / per.ToDecimal()).ToInteger() * per;

            var minY = min - per.ToDecimal();
            var minValue = (minY / per.ToDecimal()).ToInteger() * per;
            if (min == 0)
                minValue = 0;

            area.AxisY.Maximum = maxValue;
            area.AxisY.Minimum = minValue;

            var temp = (maxValue - minValue) / per;
            if (temp <= 5)
                area.AxisY.Interval = per / 5;

            chart1.ResumeLayout(true);
        }

        void ShowChart()
        {
            chart1.SuspendLayout();

            #region Y 軸重畫
            var series1 = chart1.Series.First();
            var series2 = chart1.Series.Last();

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                index = 0;

            series1.Points.Clear();
            series1.Points.AddXY(0, 0);
            series1.Points[0].AxisLabel = "0";
            for (int i = 1; i <= 12; i++)
            {
                if (index++ < dtD.Rows.Count)
                {
                    series1.Points.AddXY(i, dtD.Rows[index - 1]["銷貨淨額"].ToDecimal());
                    series1.Points[i].AxisLabel = dtD.Rows[index - 1]["月份"].ToString().Trim();
                }
                else
                {
                    series1.Points.AddXY(i, 0);
                    series1.Points[i].AxisLabel = " ";
                }
            }

            index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                index = 0;

            series2.Points.Clear();
            series2.Points.AddXY(0, 0);
            series2.Points[0].AxisLabel = "0";
            for (int i = 1; i <= 12; i++)
            {
                if (index++ < dtD.Rows.Count)
                {
                    series2.Points.AddXY(i, dtD.Rows[index - 1]["銷貨毛利"].ToDecimal());
                    series2.Points[i].AxisLabel = dtD.Rows[index - 1]["月份"].ToString().Trim();
                }
                else
                {
                    series2.Points.AddXY(i, 0);
                    series2.Points[i].AxisLabel = " ";
                }
            }
            #endregion

            chart1.ResumeLayout(true);
        }

        void WriteToText(string name)
        {
            this.EmName = name;
            textBoxT7.Text = this.EmName;
            textBoxT8.Clear();
            xe.Validate<JBS.JS.Empl>(this.EmName, row => { textBoxT8.Text = row["emname"].ToString().Trim(); });

            var index = list.FindIndex(t => t.TableName == name);
            this.dtD = list[index];
            dataGridViewT1.DataSource = dtD;

            var 銷退金額 = 0M;
            var 銷貨金額 = 0M;
            var 銷貨淨額 = 0M;
            var 銷貨成本 = 0M;
            var 銷貨毛利 = 0M;
            var 毛利率 = 0M;

            foreach (DataRow row in dtD.Rows)
            {
                銷退金額 += row["銷退金額"].ToDecimal();
                銷貨金額 += row["銷貨金額"].ToDecimal();

                銷貨成本 += row["銷貨成本"].ToDecimal();
            }
            銷貨淨額 = 銷退金額 + 銷貨金額;
            銷貨毛利 = 銷貨淨額 - 銷貨成本;

            if (銷貨淨額 != 0)
                毛利率 = (銷貨毛利 / 銷貨淨額) * 100;

            textBoxT1.Text = 銷退金額.ToString("f2");
            textBoxT2.Text = 銷貨金額.ToString("f2");

            textBoxT3.Text = 銷貨淨額.ToString("f2");
            textBoxT4.Text = 銷貨成本.ToString("f2");

            textBoxT5.Text = 銷貨毛利.ToString("f2");
            textBoxT6.Text = 毛利率.ToString("f2");

            ChartInitialize();
            ShowChart();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToText(list.First().TableName);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = list.FindIndex(t => t.TableName == this.EmName) - 1;
            if (index <= 0)
                index = 0;

            WriteToText(list.ElementAt(index).TableName);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = list.FindIndex(t => t.TableName == this.EmName) + 1;
            if (index >= list.Count - 1)
                index = list.Count - 1;

            WriteToText(list.ElementAt(index).TableName);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToText(list.Last().TableName);
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            if (dataGridViewT1.SelectedRows == null)
                return;

            if (dataGridViewT1.SelectedRows[0].Index == index)
                ShowChart();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dtD.Excel匯出並開啟(this.Text);
            }
            else if (keyData == (Keys.Escape))
            {
                dtD.Clear();
                chart1.Dispose();

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
            var tall = dtD.Clone();
            if (tall.Columns.Contains("emno") == false)
                tall.Columns.Add("emno", typeof(String));

            if (tall.Columns.Contains("emname") == false)
                tall.Columns.Add("emname", typeof(String));


            for (int i = 0; i < list.Count; i++)
            {
                var tb = list.ElementAt(i);

                if (tb.Columns.Contains("emno") == false)
                    tb.Columns.Add("emno", typeof(String));

                if (tb.Columns.Contains("emname") == false)
                    tb.Columns.Add("emname", typeof(String));

                var emno = tb.TableName;
                var emname = "";
                xe.Validate<JBS.JS.Empl>(emno, row => emname = row["emname"].ToString().Trim());

                for (int j = 0; j < tb.Rows.Count; j++)
                {
                    tb.Rows[j]["emno"] = emno;
                    tb.Rows[j]["emname"] = emname;

                    tall.Rows.Add(tb.Rows[j].ItemArray);
                }
            }

            var path = Common.reportaddress + @"Report\" + reportName;
            RPT rp = new RPT(tall, path);

            var txtend = "";
            if (radioT1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
            else if (radioT2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
            else if (radioT3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
            else if (radioT4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
            rp.lobj.Add(new string[] { "txtend", txtend });

            //if (EmNo.Length > 0)
            //{
            //    rp.lobj.Add(new string[] { "EmNo", EmNo });

            //    var EmName = "";
            //    new JBS.JS.xEvents().Validate<JBS.JS.Empl>(EmNo, row =>
            //    {
            //        EmName = row["EmName"].ToString().Trim();
            //    });
            //    rp.lobj.Add(new string[] { "EmName", EmName });
            //}

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
