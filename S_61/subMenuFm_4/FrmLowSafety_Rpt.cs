using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmLowSafety_Rpt : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmLowSafety_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmLowSafety_Rpt_Load(object sender, EventArgs e)
        {

        }

        private void itno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "")
            {
                tb.Clear();
                return;
            }

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                tb.Text = row["itno"].ToString().Trim();
            });
        }

        private void _itno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void _stno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void _stno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.TrimTextLenth() == 0)
            {
                tb.Clear();
                return;
            }

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                tb.Text = row["stno"].ToString().Trim();
            });
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmLowSafety_Rpt_Shown(object sender, EventArgs e)
        {
            ItNo.Focus();
        }

        bool Compare(TextBox tx, TextBox tx1)
        {
            var Isflag = true;
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
            if (!Compare(ItNo, ItNo1)) return;
            if (!Compare(StNo, StNo1)) return;

            try
            {
                DataTable dtD = new DataTable();
                DataTable dtBShopD = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    if (ItNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("itno", ItNo.Text.ToString().Trim());
                    if (ItNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("itno1", ItNo1.Text.ToString().Trim());
                    if (StNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("stno", StNo.Text.ToString().Trim());
                    if (StNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("stno1", StNo1.Text.ToString().Trim());

                    var iQuery = "";
                    if (ItNo.Text.ToString().Length > 0) iQuery += " And itno >= @itno";
                    if (ItNo1.Text.ToString().Length > 0) iQuery += " And itno <= @itno1";

                    var sQuery = "";
                    if (ItNo.Text.ToString().Length > 0) sQuery += " And itno >= @itno";
                    if (ItNo1.Text.ToString().Length > 0) sQuery += " And itno <= @itno1";
                    if (StNo.Text.ToString().Length > 0) sQuery += " And stock.stno >= @stno";
                    if (StNo1.Text.ToString().Length > 0) sQuery += " And stock.stno <= @stno1";
                    if (!ck1.Checked) sQuery += " And Stkroom.Sttrait <> 1";
                    if (!ck2.Checked) sQuery += " And Stkroom.Sttrait <> 2";
                    if (!ck3.Checked) sQuery += " And Stkroom.Sttrait <> 3";
                    if (!ck4.Checked) sQuery += " And Stkroom.Sttrait <> 4";

                    var isQuery = "";
                    if (ItNo.Text.ToString().Length > 0) isQuery += " And itno >= @itno";
                    if (ItNo1.Text.ToString().Length > 0) isQuery += " And itno <= @itno1";
                    if (StNo.Text.ToString().Length > 0) isQuery += " And stno >= @stno";
                    if (StNo1.Text.ToString().Length > 0) isQuery += " And stno <= @stno1";


                    cmd.CommandText = ""
                    + " select A.*,A.itno 產品編號,A.itname 品名規格,A.itunit 單位,A.itsafeqty 安全存量,ISNULL(庫存倉數量,0)庫存倉數量,ISNULL(借出倉數量,0)借出倉數量,ISNULL(加工倉數量,0)加工倉數量,ISNULL(借入倉數量,0)借入倉數量,ISNULL(庫存總數量,0)庫存總數量,採購建議量=0.0,實際採購量=0.0,廠商編號='',廠商簡稱=''"
                    + " from  ( select * from item where 0=0 And ittrait<>'1' And itsafeqty<>0 " + iQuery + ")A"
                    + " left join (select itno,SUM(itqty)庫存倉數量 from stock left join stkroom on stkroom.stno = stock.stno where 0=0 " + sQuery + " And stkroom.sttrait = 1 group by itno) AA on A.itno = AA.itno"
                    + " left join (select itno,SUM(itqty)借出倉數量 from stock left join stkroom on stkroom.stno = stock.stno where 0=0 " + sQuery + " And stkroom.sttrait = 2 group by itno) BB on A.itno = BB.itno"
                    + " left join (select itno,SUM(itqty)加工倉數量 from stock left join stkroom on stkroom.stno = stock.stno where 0=0 " + sQuery + " And stkroom.sttrait = 3 group by itno) CC on A.itno = CC.itno"
                    + " left join (select itno,SUM(itqty)借入倉數量 from stock left join stkroom on stkroom.stno = stock.stno where 0=0 " + sQuery + " And stkroom.sttrait = 4 group by itno) DD on A.itno = DD.itno"
                    + " left join (select itno,SUM(itqty)庫存總數量 from stock left join stkroom on stkroom.stno = stock.stno where 0=0 " + sQuery + " group by itno) EE on A.itno = EE.itno ";
                    da.Fill(dtD);

                    cmd.CommandText = "select 產品編號=BS.itno,廠商編號=BS.fano,廠商簡稱=FA.faname1 from (select * from bshopd where 0=0 " + isQuery + " ) BS left join fact FA on BS.fano=FA.fano order by bsdate desc";
                    da.Fill(dtBShopD);
                }
                if (dtD.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (DataRow row in dtD.Rows)
                {
                    row["採購建議量"] = row["安全存量"].ToDecimal() - row["庫存總數量"].ToDecimal();
                    row["實際採購量"] = row["採購建議量"].ToDecimal();

                    var havefa = dtBShopD.AsEnumerable().Where(r => r["產品編號"].ToString().Trim() == row["產品編號"].ToString().Trim());
                    if (havefa.Count() > 0)
                    {
                        row["廠商編號"] = havefa.ElementAt(0)["廠商編號"].ToString().Trim();
                        row["廠商簡稱"] = havefa.ElementAt(0)["廠商簡稱"].ToString().Trim();
                    }
                }

                var temp = dtD.AsEnumerable().Where(r => r["採購建議量"].ToDecimal() > 0);
                if (temp.Count() == 0)
                {
                    MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //using (FrmLowSafetyb_Rpt frm = new FrmLowSafetyb_Rpt())
                //{
                //    frm.dtD = temp.CopyToDataTable();
                //    frm.ShowDialog();
                //}
                this.OpemInfoFrom<FrmLowSafetyb_Rpt>(() =>
                            {
                                FrmLowSafetyb_Rpt frm = new FrmLowSafetyb_Rpt();
                                frm.dtD = temp.CopyToDataTable();
                                return frm;
                            });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

        }
    }
}
