using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using S_61.Basic;
using System.Linq;

namespace S_61.S0
{
    public partial class FrmNTime_Rpt : JE.MyControl.Formbase
    {
        JBS.JS.xEvents xe;

        public FrmNTime_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            Day.SetDateLength();
            Day1.SetDateLength();
            Day.Text = Date.GetDateTime(Common.User_DateTime);
            //Day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmNTime_Rpt_Load(object sender, EventArgs e)
        {

        }

        private void Day_Validating(object sender, CancelEventArgs e)
        {
            xe.DateValidate(sender, e);
        }

        private void ShNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Shift>(sender, row =>
            {
                ShNo.Text = row["ShNo"].ToString().Trim();
                ShName.Text = row["ShName"].ToString().Trim();
            });
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        private void ShNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Shift>(sender, e, row => ShName.Text = row["ShName"].ToString().Trim(), true);

            if (ShNo.TrimTextLenth() == 0)
                ShName.Clear();
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row => EmName.Text = row["EmName"].ToString().Trim(), true);

            if (EmNo.TrimTextLenth() == 0)
                EmName.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("是否要編輯報表?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    OutReport(RptMode.Design);
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        void OutReport(RptMode mode)
        {
            DataTable temp = new DataTable();
            using (var db = new JBS.xSQL())
            {
                var Query = "";
                if (machine.TrimTextLenth() > 0)
                    Query += " and seno=@machine";
                if (ShNo.TrimTextLenth() > 0)
                    Query += " and spno=@shno";
                if (EmNo.TrimTextLenth() > 0)
                    Query += " and emno=@emno";

                var back = ckBackend.Checked ? "" : " And saled.bracket=@bracket ";
                var rback = ckBackend.Checked ? "" : " And rsaled.bracket=@bracket ";

                //總銷費金額
                var tsql = @"
Select C.timei,ct=COUNT(*),money=SUM(C.money) from (
    Select B.*,timei=SUBSTRING(appdate,10,2) from (
        Select A.sano,money=SUM(A.money) from (
            Select sano,money=(qty*prs*price)      from saled  where sadate=@sadate " + back + Query + @"
            union all
            Select sano,money=(-1)*(qty*prs*price) from rsaled where sadate=@sadate " + rback + Query + @"
        )A
        left join sale on A.sano=sale.sano
        group by A.sano
    )B
    left join sale on B.sano=sale.sano
)C
group by C.timei order by timei ";
                db.Fill(tsql, spc =>
                {
                    spc.AddWithValue("sadate", Date.ToTWDate(Day.Text));
                    //spc.AddWithValue("sadate1", Date.ToTWDate(Day1.Text));
                    spc.AddWithValue("bracket", "前台");
                    spc.AddWithValue("machine", machine.Text.Trim());
                    spc.AddWithValue("shno", ShNo.Text.Trim());
                    spc.AddWithValue("emno", EmNo.Text.Trim());
                }, ref temp);
            }

            if (temp.Rows.Count == 0 && mode != RptMode.Design)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            string[] timei = new string[] { 
                "00","01","02","03","04","05","06","07",
                "08","09","10","11","12","13","14","15",
                "16","17","18","19","20","21","22","23"
            };
            for (int i = 0; i < timei.Length; i++)
            {
                if (temp.AsEnumerable().Count(r => r["timei"].ToString().Trim() == timei[i]) == 0)
                    temp.Rows.Add(new object[] { timei[i], 0, 0 });
            }
            var dt = temp.AsEnumerable().OrderBy(r => r["timei"].ToString().Trim()).CopyToDataTable();
            temp.Clear();
            temp = null;

            var path = Common.reportaddress + @"ReportF\時段銷售排行表.frx";

            using (var fs = new JBS.FReport())
            {
                fs.dy.Add("DateRange", Day.Text);
                fs.dy.Add("機台", machine.Text);
                fs.dy.Add("班別", "[" + ShNo.Text + "]" + ShName.Text);

                fs.OutReport(mode, dt, "Table", path);
            }
        }

    }
}
