using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmBShop_Rpt : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmBShop_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmBShop_Rpt_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                Bsdate.MaxLength = 7;
                Bsdate1.MaxLength = 7;
                Bsdate.Text = Date.GetDateTime(1, false);
                Bsdate.Text = Bsdate.Text.Remove(5) + "01";
                Bsdate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                Bsdate.MaxLength = 8;
                Bsdate1.MaxLength = 8;
                Bsdate.Text = Date.GetDateTime(2, false);
                Bsdate.Text = Bsdate.Text.Remove(6) + "01";
                Bsdate1.Text = Date.GetDateTime(2, false);
            }
            Bsdate.Focus();
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void Bsdate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tx = sender as TextBox;
            if (!tx.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Common.User_DateTime == 1)
                {
                    Bsdate.Text = Date.GetDateTime(1, false);
                    Bsdate1.Text = Date.GetDateTime(1, false);
                    Bsdate.Text = Bsdate.Text.Remove(5) + "01";
                }
                else
                {
                    Bsdate.Text = Date.GetDateTime(2, false);
                    Bsdate1.Text = Date.GetDateTime(2, false);
                    Bsdate.Text = Bsdate.Text.Remove(6) + "01";
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool Compare(TextBox tx, TextBox tx1)
        {
            bool Isflag = true;
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
            if (!Compare(Bsdate, Bsdate1)) return;
            if (!Compare(FaNo, FaNo1)) return;

            if (ck1.Checked == false && ck2.Checked == false)
            {
                MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string Query = "";
            if (Bsdate.TrimTextLenth() > 0) Query += " And bsdate>=@bsdate";
            if (Bsdate1.TrimTextLenth() > 0) Query += " And bsdate<=@bsdate1";
            if (FaNo.TrimTextLenth() > 0) Query += " And fano>=@fano";
            if (FaNo1.TrimTextLenth() > 0) Query += " And fano<=@fano1";

            StringBuilder sb = new StringBuilder();
            sb.Append("Select B.*,fact.faname1,item.itname from (");
            sb.Append("Select 退貨數量=SUM(退貨數量),退貨金額=SUM(退貨金額),進貨數量=SUM(進貨數量),進貨金額=SUM(進貨金額),進貨淨額=SUM(退貨金額+進貨金額),fano,itno from (");
            if (ck1.Checked && ck2.Checked)
            {
                sb.Append("Select 退貨數量=0.0,退貨金額=0.0,進貨數量=SUM(qty),進貨金額=SUM(mnyb),fano,itno from bshopd where 0=0 " + Query + " group by fano,itno ");
                sb.Append(" union ");
                sb.Append("Select 退貨數量=(-1)*SUM(qty),退貨金額=(-1)*SUM(mnyb),進貨數量=0.0,進貨金額=0.0,fano,itno from rshopd where 0=0 " + Query + " group by fano,itno ");
            }
            else if (ck1.Checked)
            {
                sb.Append("Select 退貨數量=0.0,退貨金額=0.0,進貨數量=SUM(qty),進貨金額=SUM(mnyb),fano,itno from bshopd where 0=0 " + Query + " group by fano,itno ");
            }
            else if (ck2.Checked)
            {
                sb.Append("Select 退貨數量=(-1)*SUM(qty),退貨金額=(-1)*SUM(mnyb),進貨數量=0.0,進貨金額=0.0,fano,itno from rshopd where 0=0 " + Query + " group by fano,itno ");
            }
            sb.Append(" )A group by A.fano,A.itno ");
            sb.Append(" )B left join fact on B.fano=fact.fano left join item on b.itno = item.itno order by B.fano,B.itno");

            DataTable dtResult = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("bsdate", Date.ToTWDate(Bsdate.Text));
                cmd.Parameters.AddWithValue("bsdate1", Date.ToTWDate(Bsdate1.Text));
                cmd.Parameters.AddWithValue("fano", FaNo.Text);
                cmd.Parameters.AddWithValue("fano1", FaNo1.Text);

                cmd.CommandText = sb.ToString();
                da.Fill(dtResult);
            }

            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            if (rd1.Checked)
            {
                //using (FrmBShop_Rptb frm = new FrmBShop_Rptb())
                //{
                //    frm.dtResult = dtResult;
                //    frm.DataRange = "日期區間:" + Date.AddLine(Bsdate.Text.ToString()) + "～" + Date.AddLine(Bsdate1.Text.ToString());
                //    frm.ShowDialog();

                //    dtResult.Clear();
                //}
                this.OpemInfoFrom<FrmBShop_Rptb>(() =>
                            {
                                FrmBShop_Rptb frm = new FrmBShop_Rptb();
                                frm.dtResult = dtResult.Copy();
                                frm.DataRange = "日期區間:" + Date.AddLine(Bsdate.Text.ToString()) + "～" + Date.AddLine(Bsdate1.Text.ToString());
                                return frm;
                            });
                dtResult.Clear();
            }
            else
            {
                var tTemp = dtResult.Clone();
                dtResult.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["fano"].ToString().Trim())
                    .ForAll(g =>
                    {
                        var obj = g.First().ItemArray;
                        var 退貨金額 = 0M;
                        var 進貨金額 = 0M;
                        var 進貨淨額 = 0M;
                        foreach (var gw in g)
                        {
                            退貨金額 += gw["退貨金額"].ToDecimal();
                            進貨金額 += gw["進貨金額"].ToDecimal();
                            進貨淨額 += gw["進貨淨額"].ToDecimal();
                        }

                        lock (tTemp.Rows.SyncRoot)
                        {
                            var x退貨金額 = tTemp.Columns["退貨金額"].Ordinal;
                            var x進貨金額 = tTemp.Columns["進貨金額"].Ordinal;
                            var x進貨淨額 = tTemp.Columns["進貨淨額"].Ordinal;

                            obj[x退貨金額] = 退貨金額;
                            obj[x進貨金額] = 進貨金額;
                            obj[x進貨淨額] = 進貨淨額;

                            tTemp.Rows.Add(obj);
                        }
                    });


                //using (FrmBShop_Rptc frm = new FrmBShop_Rptc())
                //{
                //    frm.dtResult = tTemp.AsEnumerable().OrderBy(r => r["fano"].ToString().Trim()).CopyToDataTable();
                //    frm.DataRange = "日期區間:" + Date.AddLine(Bsdate.Text.ToString()) + "～" + Date.AddLine(Bsdate1.Text.ToString());
                //    frm.ShowDialog();

                //    dtResult.Clear();
                //    tTemp.Clear();
                //}
                this.OpemInfoFrom<FrmBShop_Rptc>(() =>
                            {
                                FrmBShop_Rptc frm = new FrmBShop_Rptc();
                                frm.dtResult = tTemp.AsEnumerable().OrderBy(r => r["fano"].ToString().Trim()).CopyToDataTable();
                                frm.DataRange = "日期區間:" + Date.AddLine(Bsdate.Text.ToString()) + "～" + Date.AddLine(Bsdate1.Text.ToString());
                                return frm;
                            });
                dtResult.Clear();
                tTemp.Clear();
            }
        }
    }
}
