using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using S_61.Basic;

namespace S_61.S0
{
    public partial class FrmNEmpl_Rpt : JE.MyControl.Formbase
    {
        JBS.JS.xEvents xe;

        public FrmNEmpl_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            Day.SetDateLength();
            Day1.SetDateLength();
            Day.Text = Date.GetDateTime(Common.User_DateTime);
            Day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmNEmpl_Rpt_Load(object sender, EventArgs e)
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
                EmNo.Text = row["emno"].ToString().Trim();
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

        void OutReport(RptMode mode)
        {
            string columns = @"
            sano, sadate, sadate1, sadate2, sadateac, sadateac1, sadateac2, quno, cono, cuno, emno, spno, stno, xa1no, xa1par, seno, sename, x4no, x4name, orno, itno, 
            itname, ittrait, itunit, itpkgqty, qty, price, prs, rate, taxprice, mny, priceb, taxpriceb, mnyb, memo, lowzero, bomid, bomrec, recordno, sltflag, extflag, bracket, itdesp1, 
            itdesp2, itdesp3, itdesp4, itdesp5, itdesp6, itdesp7, itdesp8, itdesp9, itdesp10, stName, RecordNo_D, orid, KiTax, IsTrans, mqty, munit, mlong, mwidth1, mwidth2, 
            mwidth3, mwidth4, mformula, Point, Punit, Pqty, Pformula ";

            DataTable temp = new DataTable();
            using (var db = new JBS.xSQL())
            {
                var Query = " and bracket=@bracket";
                if (machine.TrimTextLenth() > 0)
                    Query += " and seno=@machine";
                if (ShNo.TrimTextLenth() > 0)
                    Query += " and spno=@shno";
                if (EmNo.TrimTextLenth() > 0)
                    Query += " and emno=@emno";

                var tsql = @"
                Select * from (
                    Select 單據='銷貨',數量=qty,單價=price,折數=prs,小計=(qty*price*prs), " + columns + @" from saled  where sadate >= @sadate and sadate <= @sadate1 " + Query + @"
                    union all
                    Select 單據='銷退',數量=(-1)*qty,單價=price,折數=prs,小計=(-1)*(qty*price*prs), " + columns + @" from rsaled where sadate >= @sadate and sadate <= @sadate1" + Query + @"
                ) A order by sadate,sano,單據 ";

                db.Fill(tsql, spc =>
                {
                    spc.AddWithValue("sadate", Date.ToTWDate(Day.Text));
                    spc.AddWithValue("sadate1", Date.ToTWDate(Day1.Text));
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

            for (int i = 0; i < temp.Rows.Count; i++)
            {
                temp.Rows[i]["數量"] = temp.Rows[i]["數量"].ToDecimal("f" + Common.Q);
                temp.Rows[i]["單價"] = temp.Rows[i]["單價"].ToDecimal("f" + Common.MS);
                temp.Rows[i]["小計"] = temp.Rows[i]["小計"].ToDecimal("f0");
            }

            var path = Common.reportaddress + @"ReportF\收銀人員業績表.frx";

            using (var fs = new JBS.FReport())
            {
                fs.dy.Add("DateRange", Day.Text + "～" + Day1.Text);

                fs.OutReport(mode, temp, "Table1", path);
            }
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
    }
}
