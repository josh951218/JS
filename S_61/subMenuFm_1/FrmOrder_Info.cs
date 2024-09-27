using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.subMenuFm_2;

namespace S_61.subMenuFm_1
{
    public partial class FrmOrder_Info : Formbase
    {
        JBS.JS.Order jOrder;

        bool Error = false;
        DataTable table = new DataTable();

        public FrmOrder_Info()
        {
            InitializeComponent();
            this.jOrder = new JBS.JS.Order();
            lblT11.Text = Common.Sys_MemoUdf;
        }

        private void FrmOrderInfo_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                ordate.MaxLength = ordate1.MaxLength = 7;
                esdate.MaxLength = esdate1.MaxLength = 7;
                ordate.Text = Date.GetDateTime(1, false).Remove(5) + "01";
                ordate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                ordate.MaxLength = ordate1.MaxLength = 8;
                esdate.MaxLength = esdate1.MaxLength = 8;
                ordate.Text = Date.GetDateTime(2, false).Remove(6) + "01";
                ordate1.Text = Date.GetDateTime(2, false);
            }

            ordate.SetDateLength();
            ordate1.SetDateLength();
            esdate.SetDateLength();
            esdate1.SetDateLength();


        }

        private void FrmOrder_Info_Shown(object sender, EventArgs e)
        {
            ordate.Focus();
        }

        bool Compare(TextBox tb, TextBox tb1)
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
            if (Compare(ordate, ordate1)) return;
            if (Compare(esdate, esdate1)) return;
            if (Compare(orno, orno1)) return;
            if (Compare(cuno, cuno1)) return;
            if (Compare(emno, emno1)) return;
            if (Compare(itno, itno1)) return;
            if (Compare(spno, spno1)) return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                //cn.Open();
                try
                {
                    //string sql = "SELECT 交貨日期="
                    //           + " case"
                    //           + " when 1=" + Common.User_DateTime + " then d.esdate"
                    //           + " when 2=" + Common.User_DateTime + " then d.esdate1"
                    //           + " end,訂單日期="
                    //           + " case"
                    //           + " when 1=" + Common.User_DateTime + " then d.ordate"
                    //           + " when 2=" + Common.User_DateTime + " then d.ordate1"
                    //           + " end,序號='',d.*,a.cuname1,a.emname,c.cuper1,c.cutel1,c.cufax1,a.spno"
                    //           + " from [order] a left join orderd d on a.orno=d.orno left join cust c on d.cuno=c.cuno where '0'='0'"
                    //           + " and d.ordate >=@ordate"
                    //           + " and d.ordate <=@ordate1";
                    string sql = "SELECT 交貨日期="
                    + " case"
                    + " when 1=" + Common.User_DateTime + " then d.esdate"
                    + " when 2=" + Common.User_DateTime + " then d.esdate1"
                    + " end,訂單日期="
                    + " case"
                    + " when 1=" + Common.User_DateTime + " then d.ordate"
                    + " when 2=" + Common.User_DateTime + " then d.ordate1"
                    + " end,序號='',ISNULL((select top 1 sano from rsaled where orno = a.orno), '') as 銷退單號,(select top 1 sano from saled where orno = a.orno) as 銷貨單號,a.orpayment as 付款條件,weborder.orno as 網路訂單號,a.cuper1  as 收件人,a.totmny ,d.*,a.ormemo,a.cuname1,a.emname,c.cuper1,c.cutel1,c.cufax1,a.spno"
                    + ",item.itdesp1 as item_itdesp1,item.itdesp2 as item_itdesp2,item.itdesp3 as item_itdesp3,item.itdesp4 as item_itdesp4,item.itdesp5 as item_itdesp5,item.itdesp6 as item_itdesp6,item.itdesp7 as item_itdesp7,item.itdesp8 as item_itdesp8,item.itdesp9 as item_itdesp9,item.itdesp10 as item_itdesp10,item.itnoudf as item_itnoudf,a.CardNo as 卡號"
                    + " from [order] a left join orderd d on a.orno=d.orno left join cust c on d.cuno=c.cuno left join item on d.itno = item.itno left join weborder  on  a.orno = weborder.SysOrNo where '0'='0' "
                    + " and d.ordate >=@ordate"
                    + " and d.ordate <=@ordate1";

                    if (esdate.Text.Trim() != "")
                        sql += " and d.esdate >=@esdate";
                    if (esdate1.Text.Trim() != "")
                        sql += " and d.esdate <=@esdate1";
                    if (orno.Text.Trim() != "")
                        sql += " and d.orno >=@orno";
                    if (orno1.Text.Trim() != "")
                        sql += " and d.orno <=@orno1";
                    if (emno.Text.Trim() != "")
                        sql += " and d.emno >=@emno";
                    if (emno1.Text.Trim() != "")
                        sql += " and d.emno <=@emno1";
                    if (cuno.Text.Trim() != "")
                        sql += " and d.cuno >=@cuno";
                    if (cuno1.Text.Trim() != "")
                        sql += " and d.cuno <=@cuno1";
                    if (itno.Text.Trim() != "")
                        sql += " and d.itno >=@itno";
                    if (itno1.Text.Trim() != "")
                        sql += " and d.itno <=@itno1";
                    if (spno.Text.Trim() != "")
                        sql += " and a.spno >=@spno";
                    if (spno1.Text.Trim() != "")
                        sql += " and a.spno <=@spno1";
                    if (memo.Text.Trim() != "")
                        sql += " and d.memo like '%' + @memo + '%'";
                    if (rd2.Checked)
                    {
                        sql += " and d.qtynotout > 0";
                    }
                    if (rd4.Checked)
                    {
                        sql += " and a.oroverflag = 0";
                    }
                    sql += " order by d.esdate,d.orno,d.recordno";
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("ordate", Date.ToTWDate(ordate.Text.Trim()));
                    cmd.Parameters.AddWithValue("ordate1", Date.ToTWDate(ordate1.Text.Trim()));
                    if (esdate.Text.Trim() != "") cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(esdate.Text.Trim()));
                    if (esdate1.Text.Trim() != "") cmd.Parameters.AddWithValue("esdate1", Date.ToTWDate(esdate1.Text.Trim()));
                    if (orno.Text.Trim() != "") cmd.Parameters.AddWithValue("orno", orno.Text.Trim());
                    if (orno1.Text.Trim() != "") cmd.Parameters.AddWithValue("orno1", orno1.Text.Trim());
                    if (emno.Text.Trim() != "") cmd.Parameters.AddWithValue("emno", emno.Text.Trim());
                    if (emno1.Text.Trim() != "") cmd.Parameters.AddWithValue("emno1", emno1.Text.Trim());
                    if (cuno.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno", cuno.Text.Trim());
                    if (cuno1.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno1", cuno1.Text.Trim());
                    if (itno.Text.Trim() != "") cmd.Parameters.AddWithValue("itno", itno.Text.Trim());
                    if (itno1.Text.Trim() != "") cmd.Parameters.AddWithValue("itno1", itno1.Text.Trim());
                    if (spno.Text.Trim() != "") cmd.Parameters.AddWithValue("spno", spno.Text.Trim());
                    if (spno1.Text.Trim() != "") cmd.Parameters.AddWithValue("spno1", spno1.Text.Trim());
                    if (memo.Text.Trim() != "") cmd.Parameters.AddWithValue("memo", memo.Text.Trim());
                    cmd.CommandText = sql;
                    SqlDataAdapter dd = new SqlDataAdapter(cmd);
                    table.Clear();
                    dd.Fill(table);

                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    int i = 0;
                    table.AsEnumerable().ToList().ForEach(r =>
                    {
                        r["序號"] = i;
                        r["交貨日期"] = Date.AddLine(r["交貨日期"].ToString());
                        r["訂單日期"] = Date.AddLine(r["訂單日期"].ToString());
                        ++i;
                    });
                    //FrmOrder_Infob frm = new FrmOrder_Infob();
                    //frm.table = table;
                    //frm.DateRange = Date.AddLine(ordate.Text) + "～" + Date.AddLine(ordate1.Text);
                    //frm.ShowDialog();
                    this.OpemInfoFrom<FrmOrder_Infob>(() =>
                    {
                        FrmOrder_Infob frm = new FrmOrder_Infob();
                        frm.table = table.Copy();
                        frm.DateRange = Date.AddLine(ordate.Text) + "～" + Date.AddLine(ordate1.Text);
                        return frm;
                    });
                    table.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void ordate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jOrder.DateValidate(sender, e);
        }

        private void esdate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jOrder.DateValidate(sender, e, true);
        }

        private void orno_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmOrder_Print_OrNo())
            {
                frm.TSeekNo = ((TextBox)sender).Text;
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        ((TextBox)sender).Text = frm.TResult;
                        break;
                }
            }
        }

        private void orno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused || ((TextBox)sender).Text == "") return;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("orno", ((TextBox)sender).Text.Trim());
                    cmd.CommandText = "select count(orno) from [" + "order" + "] where orno=@orno";
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        using (FrmOrder_Print_OrNo frm = new FrmOrder_Print_OrNo())
                        {
                            frm.TSeekNo = ((TextBox)sender).Text;
                            switch (frm.ShowDialog())
                            {
                                case DialogResult.OK:
                                    ((TextBox)sender).Text = frm.TResult;
                                    break;
                            }
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cuno_DoubleClick(object sender, EventArgs e)
        {
            jOrder.Open<JBS.JS.Cust>(sender);
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

            jOrder.ValidateOpen<JBS.JS.Cust>(sender, e, row => tb.Text = row["cuno"].ToString().Trim(), true);

        }

        private void emno_DoubleClick(object sender, EventArgs e)
        {
            jOrder.Open<JBS.JS.Empl>(sender);
        }

        private void emno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jOrder.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                (sender as TextBox).Text = row["emno"].ToString().Trim();
            }, true);
        }

        private void itno_DoubleClick(object sender, EventArgs e)
        {
            jOrder.Open<JBS.JS.Item>(sender);
        }

        private void itno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jOrder.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                (sender as TextBox).Text = row["itno"].ToString().Trim();
            }, true);
        }

        private void ormemo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    memo.Text = frm.Memo.GetUTF8(20);
                }
            }
        }

        private void spno_DoubleClick(object sender, EventArgs e)
        {
            jOrder.Open<JBS.JS.Spec>(sender);
        }

        private void spno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jOrder.ValidateOpen<JBS.JS.Spec>(sender, e, row =>
            {
                (sender as TextBox).Text = row["spno"].ToString().Trim();
            }, true);
        }




    }
}
