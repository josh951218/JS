using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmGarner_Info : Formbase
    {
        JBS.JS.xEvents xe;

        List<DataRow> list = new List<DataRow>();
        DataTable table = new DataTable();

        bool Error = false;

        public FrmGarner_Info()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            lblT10.Text = Common.Sys_MemoUdf;
        }

        private void FrmGarner_Info_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                GaDate.MaxLength = GaDate1.MaxLength = 7;
                GaDate.Text = Date.GetDateTime(1, false);
                GaDate.Text = GaDate.Text.Remove(5) + "01";
                GaDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                GaDate.MaxLength = GaDate1.MaxLength = 8;
                GaDate.Text = Date.GetDateTime(2, false);
                GaDate.Text = GaDate.Text.Remove(6) + "01";
                GaDate1.Text = Date.GetDateTime(2, false);
            }
          

            GaDate.Focus();
        }

        bool compare(TextBox tb, TextBox tb1)
        {
            Error = false;
            if (tb.Text == "" || tb1.Text == "")
                return Error;
            if (string.CompareOrdinal(tb.Text.Trim(), tb1.Text.Trim()) > 0)
            {
                MessageBox.Show("起迄" + tb.Tag + "不可大於終止" + tb.Tag + "", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                Error = true;
            }
            return Error;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (compare(GaDate, GaDate1)) return;
            if (compare(StNoO, StNoO1)) return;
            if (compare(StNoI, StNoI1)) return;
            if (compare(EmNo, EmNo1)) return;
            if (compare(ItNo, ItNo1)) return;
            string sql = "select 產品組成=";
            sql += " case ";
            sql += " when ittrait=1 then '組合品' ";
            sql += " when ittrait=2 then '組裝品' ";
            sql += " when ittrait=3 then '單一商品' ";
            sql += " end,A.gadate as 序號,A.emname,A.stnameo,A.stnamei,B.*";
            sql += " from garner as A left join garnerd as B on A.gano=B.gano where '0'='0'";
            if (GaDate.Text != "")
            {
                if (Common.User_DateTime == 1)
                    sql += " and B.gadate >=@gadate ";
                else
                    sql += " and B.gadate1 >=@gadate";
            }
            if (GaDate1.Text != "")
            {
                if (Common.User_DateTime == 1)
                    sql += " and B.gadate <=@gadate1";
                else
                    sql += " and B.gadate1 <=@gadate1";
            }
            if (StNoO.Text != "")
            {
                sql += " and B.stnoo >=@stnoo ";
            }
            if (StNoO1.Text != "")
            {
                sql += " and B.stnoo <=@stnoo1 ";
            }
            if (StNoI.Text != "")
            {
                sql += " and B.stnoi >=@stnoi ";
            }
            if (StNoI1.Text != "")
            {
                sql += " and B.stnoi <=@stnoi1 ";
            }
            if (EmNo.Text != "")
            {
                sql += " and B.emno >=@emno ";
            }
            if (EmNo1.Text != "")
            {
                sql += " and B.emno <=@emno1 ";
            }
            if (ItNo.Text != "")
            {
                sql += " and B.itno >=@itno ";
            }
            if (ItNo1.Text != "")
            {
                sql += " and B.itno <=@itno1 ";
            }
            if (Memo.Text != "")
            {
                sql += " and B.memo like '%' + @memo + '%'";
            }
            sql += " order by B.gadate,B.gano";

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("gadate", GaDate.Text.Trim());
                        cmd.Parameters.AddWithValue("gadate1", GaDate1.Text.Trim());
                        if (StNoO.Text != "") cmd.Parameters.AddWithValue("stnoo", StNoO.Text.Trim());
                        if (StNoO1.Text != "") cmd.Parameters.AddWithValue("stnoo1", StNoO1.Text.Trim());
                        if (StNoI.Text != "") cmd.Parameters.AddWithValue("stnoi", StNoI.Text.Trim());
                        if (StNoI1.Text != "") cmd.Parameters.AddWithValue("stnoi1", StNoI1.Text.Trim());
                        if (EmNo.Text != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        if (EmNo1.Text != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                        if (ItNo.Text != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                        if (ItNo1.Text != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                        if (Memo.Text != "") cmd.Parameters.AddWithValue("memo", Memo.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            table.Clear();
                            list.Clear();
                            dd.Fill(table);
                            if (table.Rows.Count == 0)
                            {
                                MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                table.Rows[i]["序號"] = i.ToString();
                                if (Common.User_DateTime == 1)
                                    table.Rows[i]["gadate"] = Date.AddLine(table.Rows[i]["gadate"].ToString());
                                else
                                    table.Rows[i]["gadate1"] = Date.AddLine(table.Rows[i]["gadate1"].ToString());
                            }
                            list = table.AsEnumerable().ToList();

                            //using (var frm = new FrmGarner_Infob())
                            //{ 
                            //    frm.table = table;
                            //    frm.list = list;
                            //    string date = Date.AddLine(GaDate.Text.Trim()) + "～" + Date.AddLine(GaDate1.Text.Trim());
                            //    frm.DateRange = date;
                            //    frm.date = GaDate.Text.Trim();
                            //    frm.date1 = GaDate1.Text.Trim();
                            //    frm.stnoi = StNoI.Text.Trim();
                            //    frm.stnoi1 = StNoI1.Text.Trim();
                            //    frm.stnoo = StNoO.Text.Trim();
                            //    frm.stnoo1 = StNoO1.Text.Trim();
                            //    frm.emno = EmNo.Text.Trim();
                            //    frm.emno1 = EmNo1.Text.Trim();
                            //    frm.itno = ItNo.Text.Trim();
                            //    frm.itno1 = ItNo1.Text.Trim();
                            //    frm.memo = Memo.Text.Trim();

                            //    frm.ShowDialog();
                            //}
                            this.OpemInfoFrom<FrmGarner_Infob>(() =>
                            {
                                FrmGarner_Infob frm = new FrmGarner_Infob();
                                frm.table = table;
                                frm.list = list;
                                string date = Date.AddLine(GaDate.Text.Trim()) + "～" + Date.AddLine(GaDate1.Text.Trim());
                                frm.DateRange = date;
                                frm.date = GaDate.Text.Trim();
                                frm.date1 = GaDate1.Text.Trim();
                                frm.stnoi = StNoI.Text.Trim();
                                frm.stnoi1 = StNoI1.Text.Trim();
                                frm.stnoo = StNoO.Text.Trim();
                                frm.stnoo1 = StNoO1.Text.Trim();
                                frm.emno = EmNo.Text.Trim();
                                frm.emno1 = EmNo1.Text.Trim();
                                frm.itno = ItNo.Text.Trim();
                                frm.itno1 = ItNo1.Text.Trim();
                                frm.memo = Memo.Text.Trim();
                                return frm;
                            });
                            GaDate.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void GaDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tb = sender as TextBox;
            if (tb.Text == "")
            {
                MessageBox.Show("輸入的日期不可以有空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                e.Cancel = true;
                return;
            }
            if (!tb.IsDateTime())
            {
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                e.Cancel = true;
                if (Common.User_DateTime == 1)
                {
                    if (tb.Name == "GaDate")
                    {
                        tb.Text = Date.GetDateTime(1, false);
                        tb.Text = GaDate.Text.Remove(5) + "01";
                    }
                    else
                    {
                        tb.Text = Date.GetDateTime(1, false);
                    }
                }
                else
                {
                    if (tb.Name == "GaDate")
                    {
                        tb.Text = Date.GetDateTime(2, false);
                        tb.Text = GaDate.Text.Remove(6) + "01";
                    }
                    else
                    {
                        tb.Text = Date.GetDateTime(2, false);
                    }
                }
                return;
            }
        }

        private void StNoO_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void StNoO_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text == "")
                return;

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                tb.Text = row["stno"].ToString().Trim();
            });
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text == "")
                return;

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                tb.Text = row["emno"].ToString().Trim();
            });
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void ItNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            TextBox tb = sender as TextBox;
            if (tb.Text == "")
                return;

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                tb.Text = row["itno"].ToString().Trim();
            });
        }

        private void Memo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(Memo, 20);
        }


    }
}
