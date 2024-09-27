using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmOrderSaled : Formbase
    {
        DataTable Dt = new DataTable();
        DataTable DtD = new DataTable();
        bool Isflag;

        public FrmOrderSaled()
        {
            InitializeComponent();
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

        private void OrNo_DoubleClick(object sender, EventArgs e)
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

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (!Compare(OrNo, OrNo1))
            {
                MessageBox.Show("起始編號不能大於終止編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radioT2.Checked)//已交未交
            {
                try
                {
                    Dt.Clear();
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        string sql = "select odd.orno,odd.ordate,odd.ordate1,odd.xa1no,od.xa1name,od.xa1par,odd.cuno,od.cuname1,odd.itname,odd.qty,odd.itunit,odd.pqty,odd.punit,"
                                   + "產品類別='',odd.itpkgqty,odd.ittrait,odd.qtyout,odd.qtynotout,odd.itno from orderd odd left join [order] od on odd.orno=od.orno where 0=0 and od.oroverflag <> 1 ";

                        if (OrNo.Text.ToString().Trim().Length > 0) sql += " and odd.orno >=@orno ";
                        if (OrNo1.Text.ToString().Trim().Length > 0) sql += " and odd.orno <=@orno1 ";
                        
                        if (OrNo.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno", OrNo.Text.ToString().Trim());
                        if (OrNo1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno1", OrNo1.Text.ToString().Trim());

                        cmd.CommandText = sql;
                        da.Fill(Dt);
                    }

                    if (Dt.Rows.Count > 0)
                    {
                        //using (FrmOrderSaledb frm = new FrmOrderSaledb())
                        //{
                        //    frm.Dt = Dt.Copy();
                        //    frm.ShowDialog();
                        //}
                        this.OpemInfoFrom<FrmOrderSaledb>(() =>
                        {
                            FrmOrderSaledb frm = new FrmOrderSaledb();
                            frm.Dt = Dt.Copy();
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
            else
            {
                try
                {
                    Dt.Clear();
                    DtD.Clear();
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        //含組件
                        cn.Open();
                        string sql = "SELECT sad.orno, sad.cuno, sa.cuname1, sad.pqty,sad.punit,sad.itno,sad.itname,sad.itunit,sad.xa1no,sa.xa1name,sad.xa1par,sad.sadate,sad.sadate1,單據='銷貨'"
                                    + ",sad.sano,sad.qty,sad.price,sad.prs,sad.taxprice,sad.mny,sad.itpkgqty,sad.ittrait,產品類別='',序號='',單據日期=''"
                                    + ",訂單數量=odd.qty,odd.qtyout,odd.qtynotout,sa.cuname2,ct.cuaddr1,sa.cutel1,ct.cufax1,ct.cuper1,組件產品=oddb.itname,組件數量=oddb.itqty,組件單位=oddb.itunit,組件名細=odd.bomid "
                                    + "FROM saled sad left join sale sa on sad.sano=sa.sano left join orderd odd on odd.BOMID=sad.ORID left join orderbom oddb on odd.bomid=oddb.bomid left join cust ct on sad.cuno=ct.cuno where sad.orno <> '' ";
                        if (OrNo.Text.ToString().Trim().Length > 0) sql += " and sad.orno >=@orno ";
                        if (OrNo1.Text.ToString().Trim().Length > 0) sql += " and sad.orno <=@orno1 ";

                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.Clear();
                        if (OrNo.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno", OrNo.Text.ToString().Trim());
                        if (OrNo1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno1", OrNo1.Text.ToString().Trim());
                        cmd.CommandText = sql;
                        SqlDataAdapter dd;
                        dd = new SqlDataAdapter(cmd);
                        dd.Fill(DtD);

                        DataTable temp = new DataTable();
                        sql = "SELECT sad.orno, sad.cuno, sa.cuname1, sad.pqty,sad.punit, sad.itno,sad.itname,sad.itunit,sad.xa1no,sa.xa1name,sad.xa1par,sad.sadate,sad.sadate1,單據='退貨',"
                            + "sad.sano,sad.qty,sad.price,sad.prs,sad.taxprice,sad.mny,sad.itpkgqty,sad.ittrait,產品類別='',序號='',單據日期='',"
                            + "訂單數量=odd.qty,odd.qtyout,odd.qtynotout,sa.cuname2,ct.cuaddr1,sa.cutel1,ct.cufax1,ct.cuper1,組件產品=oddb.itname,組件數量=oddb.itqty,組件單位=oddb.itunit,組件名細=odd.bomid "
                            + "FROM rsaled sad left join rsale sa on sad.sano=sa.sano left join orderd odd on odd.BOMID=sad.ORID left join orderbom oddb on odd.bomid=oddb.bomid left join cust ct on sad.cuno=ct.cuno where sad.orno <> '' ";
                        if (OrNo.Text.ToString().Trim().Length > 0) sql += " and sad.orno >=@orno ";
                        if (OrNo1.Text.ToString().Trim().Length > 0) sql += " and sad.orno <=@orno1 ";

                        cmd.Parameters.Clear();
                        if (OrNo.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno", OrNo.Text.ToString().Trim());
                        if (OrNo1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno1", OrNo1.Text.ToString().Trim());
                        cmd.CommandText = sql;
                        dd = new SqlDataAdapter(cmd);
                        dd.Fill(temp);

                        DtD.Merge(temp);

                        //不含組件
                        temp.Clear();
                        sql = "SELECT sad.pqty,sad.punit,sad.orno, sad.cuno, sa.cuname1, sad.itno,sad.itname,sad.itunit,sad.xa1no,sa.xa1name,sad.xa1par,sad.sadate,sad.sadate1,單據='銷貨',"
                            + "sad.sano,sad.qty,sad.price,sad.prs,sad.taxprice,sad.mny,sad.itpkgqty,sad.ittrait,產品類別='',序號='',單據日期='',"
                            + "訂單數量=odd.qty,odd.qtyout,odd.qtynotout,sa.cuname2,ct.cuaddr1,sa.cutel1,ct.cufax1,ct.cuper1 "
                            + "FROM saled sad left join sale sa on sad.sano=sa.sano left join orderd odd on odd.BOMID=sad.ORID left join cust ct on sad.cuno=ct.cuno where sad.orno <> '' ";
                        if (OrNo.Text.ToString().Trim().Length > 0) sql += " and sad.orno >=@orno ";
                        if (OrNo1.Text.ToString().Trim().Length > 0) sql += " and sad.orno <=@orno1 ";
                        cmd.Parameters.Clear();
                        if (OrNo.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno", OrNo.Text.ToString().Trim());
                        if (OrNo1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno1", OrNo1.Text.ToString().Trim());
                        cmd.CommandText = sql;
                        dd = new SqlDataAdapter(cmd);
                        dd.Fill(Dt);

                        sql = "SELECT sad.pqty,sad.punit,sad.orno, sad.cuno, sa.cuname1, sad.itno,sad.itname,sad.itunit,sad.xa1no,sa.xa1name,sad.xa1par,sad.sadate,sad.sadate1,單據='退貨',"
                            + "sad.sano,sad.qty,sad.price,sad.prs,sad.taxprice,sad.mny,sad.itpkgqty,sad.ittrait,產品類別='',序號='',單據日期='',"
                            + "訂單數量=odd.qty,odd.qtyout,odd.qtynotout,sa.cuname2,ct.cuaddr1,sa.cutel1,ct.cufax1,ct.cuper1 "
                            + "FROM rsaled sad left join rsale sa on sad.sano=sa.sano left join orderd odd on odd.BOMID=sad.ORID left join cust ct on sad.cuno=ct.cuno where sad.orno <> '' ";

                        if (OrNo.Text.ToString().Trim().Length > 0) sql += " and sad.orno >=@orno ";
                        if (OrNo1.Text.ToString().Trim().Length > 0) sql += " and sad.orno <=@orno1 ";
                        cmd.Parameters.Clear();
                        if (OrNo.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno", OrNo.Text.ToString().Trim());
                        if (OrNo1.Text.ToString().Trim().Length > 0) cmd.Parameters.AddWithValue("orno1", OrNo1.Text.ToString().Trim());
                        cmd.CommandText = sql;

                        dd = new SqlDataAdapter(cmd);
                        dd.Fill(temp);

                        Dt.Merge(temp);


                        foreach (DataRow i in Dt.Rows)
                        {
                            if (Common.User_DateTime == 1) i["單據日期"] = Date.AddLine(i["sadate"].ToString().Trim());
                            else i["單據日期"] = Date.AddLine(i["sadate1"].ToString().Trim());
                        }
                        foreach (DataRow i in DtD.Rows)
                        {
                            if (Common.User_DateTime == 1) i["單據日期"] = Date.AddLine(i["sadate"].ToString().Trim());
                            else i["單據日期"] = Date.AddLine(i["sadate1"].ToString().Trim());
                        }

                        if (Dt.Rows.Count > 0)
                        {
                            //using (FrmOrderSaleda frm = new FrmOrderSaleda())
                            //{
                            //    frm.Dt = Dt.Copy();
                            //    frm.DtD = DtD.Copy();
                            //    frm.ShowDialog();
                            //}
                            this.OpemInfoFrom<FrmOrderSaleda>(() =>
                            {
                                FrmOrderSaleda frm = new FrmOrderSaleda();
                                frm.Dt = Dt.Copy();
                                frm.DtD = DtD.Copy();
                                return frm;
                            });
                        }
                        else
                        {
                            MessageBox.Show("查詢不到資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void FrmOrderSaled_Load(object sender, EventArgs e)
        {

        }

    }


}
