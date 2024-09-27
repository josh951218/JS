using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmCust_DateRecievd : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmCust_DateRecievd()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmCust_DateRecievd_Load(object sender, EventArgs e)
        {
            ReDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            ReDate1.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            ReDate.Text = Date.GetDateTime(Common.User_DateTime);
            ReDate.Text = ReDate.Text.takeString(ReDate.Text.Length - 2) + "01";
            ReDate1.Text = Date.GetDateTime(Common.User_DateTime);
            radioT1.Checked = true;
        }

        private void ReDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void ReDate1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Spec>(sender, row =>
            {
                SpNo.Text = row["SpNo"].ToString().Trim();
                SpName.Text = row["spname"].ToString().Trim();
            });
        }

        private void SpNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Spec>(sender, e, row =>
            {
                SpNo.Text = row["SpNo"].ToString().Trim();
                SpName.Text = row["spname"].ToString().Trim();
            }, true);

            if (SpNo.TrimTextLenth() == 0)
                SpName.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (ReDate.Text.BigThen(ReDate1.Text))
            {
                MessageBox.Show("起始日期大於終止日期！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ReDate1.Focus();
                ReDate1.SelectAll();
                return;
            }
            DataTable t = new DataTable();
            DataTable rt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("redate", Date.ToTWDate(ReDate.Text));
                        cmd.Parameters.AddWithValue("redate1", Date.ToTWDate(ReDate1.Text));
                        if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());

                        string sql = "select "
                        + " ISNULL((select xa1par1 from receivd where receivd.reno = receiv.reno and receivd.recordno=1),1) xa1par,"
                        + " 折讓總=0.0,現金總=0.0,刷卡總=0.0,禮卷總=0.0,支票總=0.0, "
                        + " 匯入總=0.0,其它總=0.0,取用總=0.0,沖帳總=0.0,累入總=0.0,沖抵總=0.0, "
                        + " * from receiv where 0=0"
                        + " and redate >=@redate"
                        + " and redate <=@redate1";
                        if (SpNo.Text.Trim().Length > 0)
                        {
                            sql += " and receiv.spno =@spno";
                        }
                        sql += " order by redate,reno ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(t);
                        }

                        sql = "select "
                        + " 折讓總=0.0,現金總=0.0,刷卡總=0.0,禮卷總=0.0,支票總=0.0, "
                        + " 匯入總=0.0,其它總=0.0,取用總=0.0,沖帳總=0.0,累入總=0.0,沖抵總=0.0, "
                        + " receivd.bracket as 單據,receivd.Discount,receivd.Reverse,receivd.recordno as rdno,receivd.xa1par1,"
                        + " receiv.* from receivd"
                        + " left join receiv on receivd.reno=receiv.reno"
                        + " where 0=0"
                        + " and receiv.redate >=@redate"
                        + " and receiv.redate <=@redate1";
                        if (SpNo.Text.Trim().Length > 0)
                        {
                            sql += " and receiv.spno =@spno";
                        }
                        sql += " order by receiv.reno,receiv.redate ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(rt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (t.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (radioT1.Checked)
            {
                var p = t.AsEnumerable();
                for (int i = 0; i < t.Rows.Count; i++)
                {

                    t.Rows[i]["redate"] = Date.AddLine(t.Rows[i]["redate"].ToString());
                    t.Rows[i]["redate1"] = Date.AddLine(t.Rows[i]["redate1"].ToString());

                    t.Rows[i]["TotDisc"] = t.Rows[i]["TotDisc"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["CashMny"] = t.Rows[i]["CashMny"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["CardMny"] = t.Rows[i]["CardMny"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["Ticket"] = t.Rows[i]["Ticket"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["CheckMny"] = t.Rows[i]["CheckMny"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["RemitMny"] = t.Rows[i]["RemitMny"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["OtherMny"] = t.Rows[i]["OtherMny"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["GetPrvAcc"] = t.Rows[i]["GetPrvAcc"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["TotReve"] = t.Rows[i]["TotReve"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["AddPrvAcc"] = t.Rows[i]["AddPrvAcc"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                    t.Rows[i]["TotMny"] = t.Rows[i]["TotMny"].ToDecimal() * t.Rows[i]["xa1par"].ToDecimal();
                }
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    t.Rows[i]["折讓總"] = p.Sum(r => r["TotDisc"].ToDecimal());
                    t.Rows[i]["現金總"] = p.Sum(r => r["CashMny"].ToDecimal());
                    t.Rows[i]["刷卡總"] = p.Sum(r => r["CardMny"].ToDecimal());
                    t.Rows[i]["禮卷總"] = p.Sum(r => r["Ticket"].ToDecimal());
                    t.Rows[i]["支票總"] = p.Sum(r => r["CheckMny"].ToDecimal());
                    t.Rows[i]["匯入總"] = p.Sum(r => r["RemitMny"].ToDecimal());
                    t.Rows[i]["其它總"] = p.Sum(r => r["OtherMny"].ToDecimal());
                    t.Rows[i]["取用總"] = p.Sum(r => r["GetPrvAcc"].ToDecimal());
                    t.Rows[i]["沖帳總"] = p.Sum(r => r["TotReve"].ToDecimal());
                    t.Rows[i]["累入總"] = p.Sum(r => r["AddPrvAcc"].ToDecimal());
                    t.Rows[i]["沖抵總"] = p.Sum(r => r["TotMny"].ToDecimal());
                }
                for (int i = 0; i < rt.Rows.Count; i++)
                {
                    rt.Rows[i]["redate"] = Date.AddLine(rt.Rows[i]["redate"].ToString());
                    rt.Rows[i]["redate1"] = Date.AddLine(rt.Rows[i]["redate1"].ToString());

                    rt.Rows[i]["cashmny"] = rt.Rows[i]["cashmny"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["remitmny"] = rt.Rows[i]["remitmny"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["othermny"] = rt.Rows[i]["othermny"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["getprvacc"] = rt.Rows[i]["getprvacc"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["addprvacc"] = rt.Rows[i]["addprvacc"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["totmny"] = rt.Rows[i]["totmny"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["totdisc"] = rt.Rows[i]["totdisc"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["totreve"] = rt.Rows[i]["totreve"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["ticket"] = rt.Rows[i]["ticket"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["checkmny"] = rt.Rows[i]["checkmny"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["cardmny"] = rt.Rows[i]["cardmny"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["Discount"] = rt.Rows[i]["Discount"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();
                    rt.Rows[i]["Reverse"] = rt.Rows[i]["Reverse"].ToDecimal() * rt.Rows[i]["xa1par1"].ToDecimal();

                    if (rt.Rows[i]["recordno"].ToDecimal() == 1) continue;
                    else
                    {
                        if (rt.Rows[i]["rdno"].ToDecimal() == rt.Rows[i]["recordno"].ToDecimal()) continue;
                        rt.Rows[i]["TotReve"] = 0;
                        rt.Rows[i]["AddPrvAcc"] = 0;
                        rt.Rows[i]["TotMny"] = 0;
                    }
                }
                this.OpemInfoFrom<FrmCust_DateRecb>(() =>
                            {
                                FrmCust_DateRecb frm = new FrmCust_DateRecb();
                                frm.dt = t.Copy();
                                frm.rt = rt.Copy();
                                frm.DateRange = "收款區間：" + ReDate.Text.Trim() + " ～ " + ReDate1.Text.Trim();
                                return frm;
                            });
            }
            else if (radioT2.Checked)
            {
                DataTable temp = t.Copy();
                temp.Clear();
                temp.Columns.Add("筆數總");
                temp.Columns.Add("沖帳筆數");

                var GroupbyDate = from r in t.AsEnumerable()
                                  group r by r["redate"].ToString();

                foreach (var g in GroupbyDate)
                {
                    DataRow row = temp.NewRow();
                    row["redate"] = g.FirstOrDefault()["redate"];
                    row["redate1"] = g.FirstOrDefault()["redate1"];

                    row["沖帳筆數"] = g.Count();
                    row["TotDisc"] = g.Sum(r => r["TotDisc"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["CashMny"] = g.Sum(r => r["CashMny"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["CardMny"] = g.Sum(r => r["CardMny"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["Ticket"] = g.Sum(r => r["Ticket"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["CheckMny"] = g.Sum(r => r["CheckMny"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["RemitMny"] = g.Sum(r => r["RemitMny"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["OtherMny"] = g.Sum(r => r["OtherMny"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["GetPrvAcc"] = g.Sum(r => r["GetPrvAcc"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["TotReve"] = g.Sum(r => r["TotReve"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["AddPrvAcc"] = g.Sum(r => r["AddPrvAcc"].ToDecimal() * r["xa1par"].ToDecimal());
                    row["TotMny"] = g.Sum(r => r["TotMny"].ToDecimal() * r["xa1par"].ToDecimal());
                    temp.Rows.Add(row);
                }
                temp.AcceptChanges();

                var tp = temp.AsEnumerable();
                foreach (var item in tp)
                {
                    item["redate"] = Date.AddLine(item["redate"].ToString());
                    item["redate1"] = Date.AddLine(item["redate1"].ToString());

                    item["筆數總"] = tp.Sum(r => r["沖帳筆數"].ToDecimal()).ToString("f0");
                    item["折讓總"] = tp.Sum(r => r["TotDisc"].ToDecimal());
                    item["現金總"] = tp.Sum(r => r["CashMny"].ToDecimal());
                    item["刷卡總"] = tp.Sum(r => r["CardMny"].ToDecimal());
                    item["禮卷總"] = tp.Sum(r => r["Ticket"].ToDecimal());
                    item["支票總"] = tp.Sum(r => r["CheckMny"].ToDecimal());
                    item["匯入總"] = tp.Sum(r => r["RemitMny"].ToDecimal());
                    item["其它總"] = tp.Sum(r => r["OtherMny"].ToDecimal());
                    item["取用總"] = tp.Sum(r => r["GetPrvAcc"].ToDecimal());
                    item["沖帳總"] = tp.Sum(r => r["TotReve"].ToDecimal());
                    item["累入總"] = tp.Sum(r => r["AddPrvAcc"].ToDecimal());
                    item["沖抵總"] = tp.Sum(r => r["TotMny"].ToDecimal());
                }
                this.OpemInfoFrom<FrmCust_DateRecBrow>(() =>
                            {
                                FrmCust_DateRecBrow frm = new FrmCust_DateRecBrow();
                                frm.dt = temp.Copy();
                                frm.DateRange = "收款區間：" + ReDate.Text.Trim() + " ～ " + ReDate1.Text.Trim();
                                return frm;
                            });
            }
        }















































































































    }
}
