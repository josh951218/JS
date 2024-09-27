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
    public partial class FrmPOSSale : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmPOSSale()
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

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (day.TrimTextLenth() == 0 || day1.TrimTextLenth() == 0)
            {
                MessageBox.Show("日期不可為空白！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (StNo.TrimTextLenth() == 0)
            {
                StNo.Clear();
                StName.Clear();
                MessageBox.Show("請輸入倉庫(門市)編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radioT1.Checked) Report1();
            else if (radioT2.Checked) Report2();
        }

        //簡要表
        void Report1()
        {
            DataTable dt = new DataTable();
            DataTable dtD = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@d", day.Text);
                    cmd.Parameters.AddWithValue("@d1", day1.Text);
                    cmd.Parameters.AddWithValue("@StNo", StNo.Text);
                    cmd.Parameters.AddWithValue("@SeNo", SeNo.Text);

                    cmd.CommandText = ""
                        + " select 銷貨日期='',* from Rsale "
                        + " Where sadate >= (@d) And sadate <= (@d1)";
                    if (StNo.TrimTextLenth() > 0) cmd.CommandText += " And StNo = (@StNo)";
                    if (SeNo.TrimTextLenth() > 0) cmd.CommandText += " And SeNo = (@SeNo)";
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["taxmny"] = (-1) * dt.Rows[i]["taxmny"].ToDecimal();
                        dt.Rows[i]["taxmnyb"] = (-1) * dt.Rows[i]["taxmnyb"].ToDecimal();
                        dt.Rows[i]["tax"] = (-1) * dt.Rows[i]["tax"].ToDecimal();
                        dt.Rows[i]["taxb"] = (-1) * dt.Rows[i]["taxb"].ToDecimal();
                        dt.Rows[i]["totmny"] = (-1) * dt.Rows[i]["totmny"].ToDecimal();
                        dt.Rows[i]["totmnyb"] = (-1) * dt.Rows[i]["totmnyb"].ToDecimal();
                        dt.Rows[i]["cashmny"] = (-1) * dt.Rows[i]["cashmny"].ToDecimal();
                        dt.Rows[i]["cardmny"] = (-1) * dt.Rows[i]["cardmny"].ToDecimal();

                        dt.Rows[i]["discount"] = (-1) * dt.Rows[i]["discount"].ToDecimal();
                        dt.Rows[i]["ticket"] = (-1) * dt.Rows[i]["ticket"].ToDecimal();
                        dt.Rows[i]["collectmny"] = (-1) * dt.Rows[i]["collectmny"].ToDecimal();
                        dt.Rows[i]["getprvacc"] = (-1) * dt.Rows[i]["getprvacc"].ToDecimal();
                        dt.Rows[i]["acctmny"] = (-1) * dt.Rows[i]["acctmny"].ToDecimal();
                    }

                    cmd.CommandText = ""
                    + " select 單據='銷貨',銷貨日期='',ISNULL(作廢,'')作廢,sale.* "
                    + " from  sale left join "
                    + " ("
                    + "	    select sale.sano,ISNULL(posinv.Memo,'')作廢 "
                    + "	    from sale "
                    + "	    left join posinv on sale.sano = posinv.sano "
                    + "	    where posinv.totmny > 0 "
                    + " ) A on sale.sano = A.sano "
                    + " where sadate >= (@d) And sadate <= (@d1)";
                    if (StNo.TrimTextLenth() > 0) cmd.CommandText += " And StNo = (@StNo)";
                    if (SeNo.TrimTextLenth() > 0) cmd.CommandText += " And SeNo = (@SeNo)";
                    da.Fill(dt);

                    cmd.CommandText = ""
                    + " select 數量比=0.0,金額比=0.0,rsaled.*,item.kino,kind.kiname from rsaled "
                    + " left join item on rsaled.itno = item.itno "
                    + " left join kind on item.kino = kind.kino "
                    + " where sadate >= (@d) And sadate <= (@d1)";
                    if (StNo.TrimTextLenth() > 0) cmd.CommandText += " And rsaled.StNo = (@StNo)";
                    if (SeNo.TrimTextLenth() > 0) cmd.CommandText += " And rsaled.SeNo = (@SeNo)";
                    da.Fill(dtD);

                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        dtD.Rows[i]["qty"] = (-1) * dtD.Rows[i]["qty"].ToDecimal();
                        dtD.Rows[i]["pqty"] = (-1) * dtD.Rows[i]["pqty"].ToDecimal();
                        dtD.Rows[i]["mny"] = (-1) * dtD.Rows[i]["mny"].ToDecimal();
                        dtD.Rows[i]["mnyb"] = (-1) * dtD.Rows[i]["mnyb"].ToDecimal();
                    }

                    cmd.CommandText = ""
                    + " select 數量比=0.0,金額比=0.0,saled.*,item.kino,kind.kiname from saled "
                    + " left join item on saled.itno = item.itno "
                    + " left join kind on item.kino = kind.kino "
                    + " where sadate >= (@d) And sadate <= (@d1)";
                    if (StNo.TrimTextLenth() > 0) cmd.CommandText += " And saled.StNo = (@StNo)";
                    if (SeNo.TrimTextLenth() > 0) cmd.CommandText += " And saled.SeNo = (@SeNo)";
                    da.Fill(dtD);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var list = from r in dt.AsEnumerable()
                           group r by new
                           {
                               date = r["sadate"].ToString().Trim()
                           } into g
                           select new
                               {
                                   day = g.Key.date
                               };
                list = list.OrderBy(s => s.day).ToList();

                var temp = dt.Clone();
                var date = (Common.User_DateTime == 1) ? "sadate" : "sadate1";

                for (int i = 0; i < list.Count(); i++)
                {
                    var day = list.ElementAt(i).day;
                    var rows = dt.AsEnumerable().Where(r => r["sadate"].ToString() == day);
                    DataRow row = rows.First();

                    row["銷貨日期"] = Date.AddLine(row[date].ToString());

                    row["taxmny"] = rows.Sum(r => r["taxmny"].ToDecimal());
                    row["taxmnyb"] = rows.Sum(r => r["taxmnyb"].ToDecimal());
                    row["tax"] = rows.Sum(r => r["tax"].ToDecimal());
                    row["taxb"] = rows.Sum(r => r["taxb"].ToDecimal());
                    row["totmny"] = rows.Sum(r => r["totmny"].ToDecimal());
                    row["totmnyb"] = rows.Sum(r => r["totmnyb"].ToDecimal());

                    row["discount"] = rows.Sum(r => r["discount"].ToDecimal());
                    row["cashmny"] = rows.Sum(r => r["cashmny"].ToDecimal());
                    row["cardmny"] = rows.Sum(r => r["cardmny"].ToDecimal());
                    row["ticket"] = rows.Sum(r => r["ticket"].ToDecimal());
                    row["collectmny"] = rows.Sum(r => r["collectmny"].ToDecimal());
                    row["getprvacc"] = rows.Sum(r => r["getprvacc"].ToDecimal());
                    row["acctmny"] = rows.Sum(r => r["acctmny"].ToDecimal());
                    temp.ImportRow(row);
                }

                var list1 = from r in dtD.AsEnumerable()
                            group r by new
                            {
                                date = r["sadate"].ToString().Trim(),
                                kino = r["kino"].ToString().Trim()
                            } into g
                            select new
                            {
                                day = g.Key.date,
                                kino = g.Key.kino
                            };
                var temp1 = dtD.Clone();
                for (int i = 0; i < list1.Count(); i++)
                {
                    var day = list1.ElementAt(i).day;
                    var kino = list1.ElementAt(i).kino;
                    var total = dtD.AsEnumerable().Where(r => r["sadate"].ToString() == day).Sum(r => r["PQty"].ToDecimal());
                    var mny = dtD.AsEnumerable().Where(r => r["sadate"].ToString() == day).Sum(r => r["mny"].ToDecimal());

                    var rows = dtD.AsEnumerable().Where(r => r["sadate"].ToString() == day && r["kino"].ToString() == kino);
                    temp1.ImportRow(rows.First());
                    temp1.AcceptChanges();

                    DataRow row = temp1.AsEnumerable().Last();

                    if (total == 0) row["數量比"] = 0;
                    else
                        row["數量比"] = (rows.Sum(r => r["PQty"].ToDecimal()) / total) * 100.ToDecimal("f2");
                    if (mny == 0) row["金額比"] = 0;
                    else
                        row["金額比"] = (rows.Sum(r => r["mny"].ToDecimal()) / mny) * 100.ToDecimal("f2");

                    row["PQty"] = rows.Sum(r => r["PQty"].ToDecimal());
                    row["Qty"] = rows.Sum(r => r["Qty"].ToDecimal());

                    row["mny"] = rows.Sum(r => r["mny"].ToDecimal());
                    row["mnyb"] = rows.Sum(r => r["mnyb"].ToDecimal());


                    if (total == 0) row["數量比"] = 0;
                    else
                        row["數量比"] = (row["PQty"].ToDecimal() / total) * 100.ToDecimal("f2");
                    if (mny == 0) row["金額比"] = 0;
                    else
                        row["金額比"] = (row["mny"].ToDecimal() / mny) * 100.ToDecimal("f2");

                }
                using (FrmPOSSaleb1 frm = new FrmPOSSaleb1())
                {
                    frm.dt = temp.Copy();
                    frm.dtD = temp1.Copy();
                    frm.ShowDialog();
                }
            }
        }
        //明細表
        void Report2()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@d", day.Text);
                    cmd.Parameters.AddWithValue("@d1", day1.Text);
                    cmd.Parameters.AddWithValue("@StNo", StNo.Text);
                    cmd.Parameters.AddWithValue("@SeNo", SeNo.Text);

                    cmd.CommandText = ""
                        + " select 單據='銷退',銷貨日期='',作廢='',* from Rsale "
                        + " Where sadate >= (@d) And sadate <= (@d1)";
                    if (StNo.TrimTextLenth() > 0) cmd.CommandText += " And StNo = (@StNo)";
                    if (SeNo.TrimTextLenth() > 0) cmd.CommandText += " And SeNo = (@SeNo)";
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["taxmny"] = (-1) * dt.Rows[i]["taxmny"].ToDecimal();
                        dt.Rows[i]["taxmnyb"] = (-1) * dt.Rows[i]["taxmnyb"].ToDecimal();
                        dt.Rows[i]["tax"] = (-1) * dt.Rows[i]["tax"].ToDecimal();
                        dt.Rows[i]["taxb"] = (-1) * dt.Rows[i]["taxb"].ToDecimal();
                        dt.Rows[i]["totmny"] = (-1) * dt.Rows[i]["totmny"].ToDecimal();
                        dt.Rows[i]["totmnyb"] = (-1) * dt.Rows[i]["totmnyb"].ToDecimal();
                        dt.Rows[i]["cashmny"] = (-1) * dt.Rows[i]["cashmny"].ToDecimal();
                        dt.Rows[i]["cardmny"] = (-1) * dt.Rows[i]["cardmny"].ToDecimal();

                        dt.Rows[i]["discount"] = (-1) * dt.Rows[i]["discount"].ToDecimal();
                        dt.Rows[i]["ticket"] = (-1) * dt.Rows[i]["ticket"].ToDecimal();
                        dt.Rows[i]["collectmny"] = (-1) * dt.Rows[i]["collectmny"].ToDecimal();
                        dt.Rows[i]["getprvacc"] = (-1) * dt.Rows[i]["getprvacc"].ToDecimal();
                        dt.Rows[i]["acctmny"] = (-1) * dt.Rows[i]["acctmny"].ToDecimal();
                    }

                    cmd.CommandText = ""
                    + " select 單據='銷貨',銷貨日期='',ISNULL(作廢,'')作廢,sale.* "
                    + " from  sale left join "
                    + " ("
                    + "	    select sale.sano,ISNULL(posinv.Memo,'')作廢 "
                    + "	    from sale "
                    + "	    left join posinv on sale.sano = posinv.sano "
                    + "	    where posinv.totmny > 0 "
                    + " ) A on sale.sano = A.sano "
                    + " where sadate >= (@d) And sadate <= (@d1)";
                    if (StNo.TrimTextLenth() > 0) cmd.CommandText += " And StNo = (@StNo)";
                    if (SeNo.TrimTextLenth() > 0) cmd.CommandText += " And SeNo = (@SeNo)";
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Common.User_DateTime == 1)
                    {
                        dt.Rows[i]["銷貨日期"] = Date.AddLine(dt.Rows[i]["sadate"].ToString().Trim());
                    }
                    else
                    {
                        dt.Rows[i]["銷貨日期"] = Date.AddLine(dt.Rows[i]["sadate1"].ToString().Trim());
                    }
                }
                using (FrmPOSSaleb2 frm = new FrmPOSSaleb2())
                {
                    frm.dt = dt.Copy();
                    frm.ShowDialog();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();

            MainForm.menu.OpenForm<FrmPOSReport>("");
        }
    }
}
