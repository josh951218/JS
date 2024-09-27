using System;
using System.Collections.Concurrent;
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
    public partial class FrmItemShop_Rpt : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmItemShop_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmItemShop_Rtp_Load(object sender, EventArgs e)
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

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (!Compare(ItNo, ItNo1)) return;

            if (ck1.Checked == false && ck2.Checked == false)
            {
                MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string bshopd = @"單據='進貨'
            ,[bsno],[bsdate],[bsdate1],[bsdate2],[bsdateac],[bsdateac1],[bsdateac2],[fqno],[cono],[fano],[emno],[spno],[stno],[xa1no],[xa1par]
            ,[seno],[sename],[x4no],[x4name],[fono],[itno],[itname],[ittrait],[itunit],[itpkgqty],[qty],[price],[prs],[rate],[taxprice],[mny]
            ,[priceb],[taxpriceb],[mnyb],[memo],[lowzero],[bomid],[bomrec],[recordno],[sltflag],[extflag],[bracket],[itdesp1],[itdesp2],[itdesp3]
            ,[itdesp4],[itdesp5],[itdesp6],[itdesp7],[itdesp8],[itdesp9],[itdesp10] ,[StName] ,[RecordNo_D],[RealCost],[foid],[mqty],[munit]
            ,[mlong],[mwidth1],[mwidth2],[mwidth3],[mwidth4],[mformula],[Punit],[Pqty],[Pformula] ";

            string rshopd = @"單據='退貨'
            ,[bsno],[bsdate],[bsdate1],[bsdate2],[bsdateac],[bsdateac1],[bsdateac2],[fqno],[cono],[fano],[emno],[spno],[stno],[xa1no],[xa1par]
            ,[seno],[sename],[x4no],[x4name],[fono],[itno],[itname],[ittrait],[itunit],[itpkgqty],qty=(-1)*[qty],[price],[prs],[rate],[taxprice]
            ,mny=(-1)*[mny]
            ,[priceb],[taxpriceb],mnyb=(-1)*[mnyb],[memo],[lowzero],[bomid],[bomrec],[recordno],[sltflag],[extflag],[bracket],[itdesp1],[itdesp2],[itdesp3]
            ,[itdesp4],[itdesp5],[itdesp6],[itdesp7],[itdesp8],[itdesp9],[itdesp10] ,[StName] ,[RecordNo_D],[RealCost],[foid],[mqty],[munit]
            ,[mlong],[mwidth1],[mwidth2],[mwidth3],[mwidth4],[mformula],[Punit],pqty=(-1)*[pqty],[Pformula] ";

            var Query = "";
            if (Bsdate.TrimTextLenth() > 0) Query += " And bsdate >= @bsdate ";
            if (Bsdate1.TrimTextLenth() > 0) Query += " And bsdate <= @bsdate1 ";
            if (ItNo.TrimTextLenth() > 0) Query += " And itno >= @itno ";
            if (ItNo1.TrimTextLenth() > 0) Query += " And itno <= @itno1 ";

            StringBuilder sb = new StringBuilder();
            sb.Append(" Select A.*,fact.faname1 from (");
            if (ck1.Checked && ck2.Checked)
            {
                sb.Append("Select " + bshopd + " from bshopd where 0=0 " + Query);
                sb.Append(" union ");
                sb.Append("Select " + rshopd + " from rshopd where 0=0 " + Query);
            }
            else if (ck1.Checked)
            {
                sb.Append("Select " + bshopd + " from bshopd where 0=0 " + Query);
            }
            else if (ck2.Checked)
            {
                sb.Append("Select " + rshopd + " from rshopd where 0=0 " + Query);
            }
            sb.Append(" )A left join fact on A.fano = fact.fano order by A.itno,A.bsno");

            DataTable dtResult = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("bsdate", Date.ToTWDate(Bsdate.Text));
                cmd.Parameters.AddWithValue("bsdate1", Date.ToTWDate(Bsdate1.Text));
                cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());

                cmd.CommandText = sb.ToString();
                da.Fill(dtResult);
            }

            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("找不到任何資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rd1.Checked)
            {
                //using (var frm = new FrmItemShop_Rptb())
                //{
                //    frm.tb = dtResult;
                //    frm.DateRange = "日期區間:" + Date.AddLine(Bsdate.Text.ToString()) + "～" + Date.AddLine(Bsdate1.Text.ToString());
                //    frm.ShowDialog();

                //    dtResult.Clear();
                //}
                this.OpemInfoFrom<FrmItemShop_Rptb>(() =>
                            {
                                FrmItemShop_Rptb frm = new FrmItemShop_Rptb();
                                frm.tb = dtResult.Copy();
                                frm.DateRange = "日期區間:" + Date.AddLine(Bsdate.Text.ToString()) + "～" + Date.AddLine(Bsdate1.Text.ToString());
                                return frm;
                            });
                dtResult.Clear();
            }
            else
            {
                var tTemp = dtResult.Clone();
                tTemp.Columns.Add("進貨數量", typeof(Decimal));
                tTemp.Columns.Add("進貨淨額", typeof(Decimal));

                dtResult.AsEnumerable()
                    .AsParallel()
                    .GroupBy(r => r["itno"].ToString().Trim())
                    .ForAll(g =>
                    {
                        var obj = g.First().ItemArray;
                        var qty = 0M;
                        var mny = 0M;
                        foreach (var gw in g)
                        {
                            qty += gw["qty"].ToDecimal();
                            mny += gw["mnyb"].ToDecimal();
                        }

                        ConcurrentQueue<object> queue = new ConcurrentQueue<object>();
                        foreach (var o in obj)
                        {
                            queue.Enqueue(o);
                        }
                        queue.Enqueue(qty);
                        queue.Enqueue(mny);

                        lock (tTemp.Rows.SyncRoot)
                        {
                            tTemp.Rows.Add(queue.ToArray());
                        }
                    });

                //using (FrmItemShop_Rptc frm = new FrmItemShop_Rptc())
                //{
                //    frm.tb = tTemp.AsEnumerable().OrderBy(r => r["itno"].ToString()).CopyToDataTable();
                //    frm.DateRange = "日期區間:" + Date.AddLine(Bsdate.Text.ToString()) + "～" + Date.AddLine(Bsdate1.Text.ToString());
                //    frm.ShowDialog();

                //    dtResult.Clear();
                //    tTemp.Clear();
                //}
                this.OpemInfoFrom<FrmItemShop_Rptc>(() =>
                            {
                                FrmItemShop_Rptc frm = new FrmItemShop_Rptc();
                                frm.tb = tTemp.AsEnumerable().OrderBy(r => r["itno"].ToString()).CopyToDataTable();
                                frm.DateRange = "日期區間:" + Date.AddLine(Bsdate.Text.ToString()) + "～" + Date.AddLine(Bsdate1.Text.ToString());
                                return frm;
                            });
                dtResult.Clear();
                tTemp.Clear();
            }
        }

    }
}
