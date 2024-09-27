using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S4
{
    public partial class FrmPOSItemPrs : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmPOSItemPrs()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            day.SetDateLength();
            day1.SetDateLength();
            var d = Date.GetDateTime(Common.User_DateTime);
            day.Text = d.Substring(0, d.Length - 2) + "01";
            day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmPOSSale_Load(object sender, EventArgs e)
        {

        }

        private void day_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void day1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void ItNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (day.TrimTextLenth() == 0 || day1.TrimTextLenth() == 0)
            {
                MessageBox.Show("日期不可為空白！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radioT1.Checked) Report1();
            else if (radioT2.Checked) Report2();
        }

        //簡要表
        void Report1()
        {
            DataTable dtD = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@d", day.Text);
                    cmd.Parameters.AddWithValue("@d1", day1.Text);
                    cmd.Parameters.AddWithValue("@ItNo", ItNo.Text);
                    cmd.Parameters.AddWithValue("@ItNo1", ItNo1.Text);

                    cmd.CommandText = ""
                    + " select rsaled.* from rsaled "
                        //+ " left join rsale on rsaled.sano = rsale.sano "
                    + " where  rsaled.sadate >= (@d) and rsaled.sadate <= (@d1) and itno != 'Z$1'";
                    if (ItNo.TrimTextLenth() > 0) cmd.CommandText += " And ItNo >= (@ItNo)";
                    if (ItNo1.TrimTextLenth() > 0) cmd.CommandText += " And ItNo <= (@ItNo1)";
                    da.Fill(dtD);

                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        dtD.Rows[i]["pqty"] = (-1) * dtD.Rows[i]["pqty"].ToDecimal();
                        dtD.Rows[i]["qty"] = (-1) * dtD.Rows[i]["qty"].ToDecimal();
                        dtD.Rows[i]["mny"] = (-1) * dtD.Rows[i]["mny"].ToDecimal();
                        dtD.Rows[i]["mnyb"] = (-1) * dtD.Rows[i]["mnyb"].ToDecimal();
                    }

                    cmd.CommandText = ""
                    + " select saled.* from saled "
                        //+ " left join sale on saled.sano = sale.sano "
                    + " where  saled.sadate >= (@d) and saled.sadate <= (@d1) and itno != 'Z$1'";
                    if (ItNo.TrimTextLenth() > 0) cmd.CommandText += " And ItNo >= (@ItNo)";
                    if (ItNo1.TrimTextLenth() > 0) cmd.CommandText += " And ItNo <= (@ItNo1)";
                    da.Fill(dtD);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (dtD.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var list = from r in dtD.AsEnumerable()
                           group r by new
                           {
                               no = r["itno"].ToString().Trim(),
                               slt = r["prs"].ToDecimal(),
                               p = r["price"].ToDecimal(),
                               q = r["itpkgqty"].ToDecimal()
                           } into g
                           select new
                               {
                                   itno = g.Key.no,
                                   prs = g.Key.slt,
                                   price = g.Key.p,
                                   itpkgqty = g.Key.q
                               };

                var temp = dtD.Clone();
                for (int i = 0; i < list.Count(); i++)
                {
                    var itno = list.ElementAt(i).itno;
                    var prs = list.ElementAt(i).prs;
                    var price = list.ElementAt(i).price;
                    var itpkgqty = list.ElementAt(i).itpkgqty;

                    var rows = dtD.AsEnumerable().Where(r => r["itno"].ToString() == itno && r["prs"].ToDecimal() == prs && r["price"].ToDecimal() == price && r["itpkgqty"].ToDecimal() == itpkgqty);
                    DataRow row = rows.First();

                    row["qty"] = rows.Sum(r => r["qty"].ToDecimal());
                    row["Pqty"] = rows.Sum(r => r["Pqty"].ToDecimal());
                    row["mny"] = rows.Sum(r => r["mny"].ToDecimal());
                    row["mnyb"] = rows.Sum(r => r["mnyb"].ToDecimal());

                    temp.ImportRow(row);
                }

                using (FrmPOSItemPrsb1 frm = new FrmPOSItemPrsb1())
                {
                    frm.CreateDay = Date.AddLine(day.Text) + " ～ " + Date.AddLine(day1.Text);
                    frm.dtD = temp.Copy();
                    frm.ShowDialog();
                }
            }
        }
        //明細表
        void Report2()
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();

            MainForm.menu.OpenForm<FrmPOSReport>("");
        }
    }
}
