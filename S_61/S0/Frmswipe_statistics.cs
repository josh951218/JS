using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;

namespace S_61.S0
{
    public partial class Frmswipe_statistics : JE.MyControl.Formbase
    {
        JBS.JS.xEvents xe;
        DataTable temp = new DataTable();
        string DateRange = "";
        bool Isflag;

        public Frmswipe_statistics()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            Day.SetDateLength();
            Day1.SetDateLength();
            Day.Text = Date.GetDateTime(Common.User_DateTime);
            var len = Day.Text.Length - 2;
            Day.Text = Day.Text.takeString(len) + "01";
            Day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void Day_Validating(object sender, CancelEventArgs e)
        {
            xe.DateValidate(sender, e);
        }

        private void cuno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void cuno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.TrimTextLenth() == 0)
            {
                tb.Clear();
                return;
            }
            xe.ValidateOpen<JBS.JS.Cust>(sender, e, row => tb.Text = row["cuno"].ToString().Trim(), true);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool Compare(TextBox tx, TextBox tx1)
        {
            Isflag = true;
            if (tx.Text != "" && tx1.Text != "")
            {
                if (string.CompareOrdinal(tx.Text, tx1.Text) > 0)
                {
                    Isflag = false;
                    MessageBox.Show("起始" + tx.Tag + "不可大於終止" + tx1.Tag + "，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx.Focus();
                }
            }
            return Isflag;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            try
            {
                DateRange = Date.AddLine(Day.Text.ToString().Trim()) + "～" + Date.AddLine(Day1.Text.ToString().Trim());

                if (!Compare(Day, Day1)) return;

                //if (machine.TrimTextLenth() == 0)
                //{
                //    MessageBox.Show("請輸入機台號碼!!!");
                //    machine.Focus();
                //    return;
                //}

                using (var db = new JBS.xSQL())
                {
                    var Query = "";
                    if (machine.TrimTextLenth() > 0)
                        Query += " and seno=@machine";

                    if (cuno.TrimTextLenth() > 0)
                    {
                        Query += " and cuno >= @cuno";
                    }

                    if (cuno1.TrimTextLenth() > 0)
                    {
                        Query += " and cuno <= @cuno1";
                    }

                    var tsql = @"select sano, sadate1, cuname1, cardno, cardmny, samemo , sename , seno from sale where sadate>=@sadate and sadate<=@sadate1 and cardmny>0 " + Query;

                    db.Fill(tsql, spc =>
                    {
                        spc.AddWithValue("sadate", Date.ToTWDate(Day.Text));
                        spc.AddWithValue("sadate1", Date.ToTWDate(Day1.Text));
                        spc.AddWithValue("cuno", cuno.Text.ToString().Trim());
                        spc.AddWithValue("cuno1", cuno1.Text.ToString().Trim());
                        spc.AddWithValue("bracket", "前台");
                        spc.AddWithValue("machine", machine.Text.Trim());
                    }, ref temp);
                }
                if (temp.Rows.Count > 0)
                {
                    this.OpemInfoFrom<Frmswipe_statisticsB>(() =>
                    {
                        Frmswipe_statisticsB frm = new Frmswipe_statisticsB();
                        frm.temp = temp.Copy();
                        frm.DateRange = DateRange;
                        return frm;
                    });
                }
                else
                {
                    MessageBox.Show("查詢不到資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }
    }
}
