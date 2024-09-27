using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmSale_Info : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable dtSale = new DataTable();
        DataTable dtSaleD = new DataTable();
        DataTable RSale = new DataTable();
        DataTable RSaleD = new DataTable();
        DataTable Result = new DataTable();
        DataTable dtSD = new DataTable();
        DataTable dtRD = new DataTable();
        DataTable Result0 = new DataTable();
        //sale
        string Taxmny = "Taxmny";
        string TotMny = "TotMny";
        string tax = "tax";
        //saled 
        string price = "price";
        string taxprice = "taxprice";
        string mny = "mny";

        decimal d, db;

        public FrmSale_Info()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            lblT11.Text = Common.Sys_MemoUdf;
            //if (Common.Sys_StockKind == 2)
            //{
            //    lblT9.Text = "起迄收銀人員";
            //    lblT9.Text = "起迄班別編號";
            //    lblT10.Text = "起迄機台號碼";
            //}
        }

        private void FrmSale_Info_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                SaDate.MaxLength = 7;
                SaDate1.MaxLength = 7;
                SaDate.Text = Date.GetDateTime(1, false);
                SaDate1.Text = Date.GetDateTime(1, false);
                SaDate.Text = SaDate.Text.Remove(5) + "01";
            }
            else
            {
                SaDate.MaxLength = 8;
                SaDate1.MaxLength = 8;
                SaDate.Text = Date.GetDateTime(2, false);
                SaDate1.Text = Date.GetDateTime(2, false);
                SaDate.Text = SaDate.Text.Remove(6) + "01";
            }
            SaDate.SetDateLength();
            SaDate1.SetDateLength();

        }

        private void FrmSale_Info_Shown(object sender, EventArgs e)
        {
            SaDate.Focus();
        }

        bool Compare(TextBoxT tx, TextBoxT tx1)
        {
            bool flag = true;
            if (tx.Text.Trim() != "" && tx1.Text.Trim() != "")
            {
                if (string.CompareOrdinal(tx.Text.Trim(), tx1.Text.Trim()) > 0)
                {
                    flag = false;
                    tx.Focus();
                    MessageBox.Show("起始" + tx.Tag + "不可大於終止" + tx.Tag + "，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return flag;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            dtSale.Clear();
            dtSaleD.Clear();
            RSale.Clear();
            RSaleD.Clear();
            Result.Clear();
            Result0.Clear();
            dtSD.Clear();
            dtRD.Clear();

            if (!Compare(SaDate, SaDate1)) return;
            if (!Compare(CuNo, CuNo1)) return;
            if (!Compare(EmNo, EmNo1)) return;
            if (!Compare(ItNo, ItNo1)) return;
            if (!Compare(StNo, StNo1)) return;
            if (!Compare(KiNo, KiNo1)) return;
            if (!Compare(X4No, X4No1)) return;
            if (!Compare(SpNo, SpNo1)) return;
            if (!Compare(SeNo, SeNo1)) return;
            if (ck1.Checked == false)
                if (ck2.Checked == false)
                {
                    MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            string str = "";
            if (Common.User_DateTime == 1)
            {
                if (SaDate.Text.Trim() != "")
                    str += " and SaDate >=@SaDate";
                if (SaDate1.Text.Trim() != "")
                    str += " and SaDate <=@SaDate1";
            }
            else
            {
                if (SaDate.Text.Trim() != "")
                    str += " and SaDate1 >=@SaDate";
                if (SaDate1.Text.Trim() != "")
                    str += " and SaDate1 <=@SaDate1";
            }
            if (CuNo.Text.Trim() != "")
                str += " and cuno >=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
            if (CuNo1.Text.Trim() != "")
                str += " and cuno <=@cuno1 COLLATE Chinese_Taiwan_Stroke_BIN";
            if (EmNo.Text.Trim() != "")
                str += " and EmNo >=@emno COLLATE Chinese_Taiwan_Stroke_BIN";
            if (EmNo1.Text.Trim() != "")
                str += " and EmNo <=@emno1 COLLATE Chinese_Taiwan_Stroke_BIN";
            if (X4No.Text.Trim() != "")
                str += " and X4No >=@x4no COLLATE Chinese_Taiwan_Stroke_BIN";
            if (X4No1.Text.Trim() != "")
                str += " and X4No <=@x4no1 COLLATE Chinese_Taiwan_Stroke_BIN";
            if (SpNo.Text.Trim() != "")
                str += " and SpNo >=@spno COLLATE Chinese_Taiwan_Stroke_BIN";
            if (SpNo1.Text.Trim() != "")
                str += " and SpNo <=@spno1 COLLATE Chinese_Taiwan_Stroke_BIN";
            if (SeNo.Text.Trim() != "")
                str += " and SeNo >=@seno COLLATE Chinese_Taiwan_Stroke_BIN";
            if (SeNo1.Text.Trim() != "")
                str += " and SeNo <=@seno1 COLLATE Chinese_Taiwan_Stroke_BIN";
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("SaDate", SaDate.Text.Trim());
                        cmd.Parameters.AddWithValue("SaDate1", SaDate1.Text.Trim());
                        if (CuNo.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        if (CuNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                        if (EmNo.Text.Trim() != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        if (EmNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                        if (X4No.Text.Trim() != "") cmd.Parameters.AddWithValue("x4no", X4No.Text.Trim());
                        if (X4No1.Text.Trim() != "") cmd.Parameters.AddWithValue("x4no1", X4No1.Text.Trim());
                        if (SpNo.Text.Trim() != "") cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                        if (SpNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("spno1", SpNo1.Text.Trim());
                        if (SeNo.Text.Trim() != "") cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
                        if (SeNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("seno1", SeNo1.Text.Trim());

                        string sql = "";
                        if (ck1.Checked)
                        {
                            sql = "select sano from Sale where '0'='0' " + str;
                            cmd.CommandText = sql;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dtSale);
                            }
                        }
                        if (ck2.Checked)
                        {
                            sql = "select sano from RSale where '0'='0' " + str;
                            cmd.CommandText = sql;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(RSale);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                decimal salemny = 0, rsalemny = 0;
                Result0.Clear();
                Result.Clear();
                d = 0;
                db = 0;
                if (ck1.Checked)
                {
                    str = "";
                    if (Common.User_DateTime == 1)
                    {
                        if (SaDate.Text.Trim() != "")
                            str += " and sa.SaDate >=@SaDate";
                        if (SaDate1.Text.Trim() != "")
                            str += " and sa.SaDate <=@SaDate1";
                    }
                    else
                    {
                        if (SaDate.Text.Trim() != "")
                            str += " and sa.SaDate1 >=@SaDate";
                        if (SaDate1.Text.Trim() != "")
                            str += " and sa.SaDate1 <=@SaDate1";
                    }
                    if (CuNo.Text.Trim() != "")
                        str += " and sa.cuno >=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (CuNo1.Text.Trim() != "")
                        str += " and sa.cuno <=@cuno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (EmNo.Text.Trim() != "")
                        str += " and sa.EmNo >=@emno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (EmNo1.Text.Trim() != "")
                        str += " and sa.EmNo <=@emno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (X4No.Text.Trim() != "")
                        str += " and sa.X4No >=@x4no COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (X4No1.Text.Trim() != "")
                        str += " and sa.X4No <=@x4no1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SpNo.Text.Trim() != "")
                        str += " and sa.SpNo >=@spno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SpNo1.Text.Trim() != "")
                        str += " and sa.SpNo <=@spno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SeNo.Text.Trim() != "")
                        str += " and sa.SeNo >=@seno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SeNo1.Text.Trim() != "")
                        str += " and sa.SeNo <=@seno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (ItNo.Text.Trim() != "")
                        str += " and SaleD.ItNo >=@itno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (ItNo1.Text.Trim() != "")
                        str += " and SaleD.ItNo <=@itno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (StNo.Text.Trim() != "")
                        str += " and SaleD.StNo >=@stno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (StNo1.Text.Trim() != "")
                        str += " and SaleD.StNo <=@stno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (KiNo.Text.Trim() != "")
                        str += " and i.kiNo >=@kino COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (KiNo1.Text.Trim() != "")
                        str += " and i.kiNo <=@kino1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (Memo.Text.Trim() != "")
                        str += " and SaleD.Memo like '%' + @memo + '%'";

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("SaDate", SaDate.Text.Trim());
                                cmd.Parameters.AddWithValue("SaDate1", SaDate1.Text.Trim());
                                if (CuNo.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                                if (CuNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                                if (EmNo.Text.Trim() != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                                if (EmNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                                if (X4No.Text.Trim() != "") cmd.Parameters.AddWithValue("x4no", X4No.Text.Trim());
                                if (X4No1.Text.Trim() != "") cmd.Parameters.AddWithValue("x4no1", X4No1.Text.Trim());
                                if (SpNo.Text.Trim() != "") cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                                if (SpNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("spno1", SpNo1.Text.Trim());
                                if (SeNo.Text.Trim() != "") cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
                                if (SeNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("seno1", SeNo1.Text.Trim());
                                if (ItNo.Text.Trim() != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                                if (ItNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                                if (StNo.Text.Trim() != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                                if (StNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("stno1", StNo1.Text.Trim());
                                if (KiNo.Text.Trim() != "") cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                                if (KiNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("kino1", KiNo1.Text.Trim());
                                if (Memo.Text.Trim() != "") cmd.Parameters.AddWithValue("memo", Memo.Text.Trim());


                                string sql0 = @"
Select sa.cardno,sa.SaPayment,sa.invno,sa." + Taxmny + @",sa." + tax + @",sa." + TotMny + @",d.dename1,s.spname,i.kino,k.kiname, i.itnw,sa.Cuname1,sa.emname,sa.spname,SaleD.*,單據='銷貨'
,c.cuper1,c.cutel1,c.cuatel1,c.cufax1,c.cuaddr1,c.cur1,c.cux1no,x.x1name ,xa.Xa1Name
from SaleD
inner join sale as sa on SaleD.sano=sa.sano 
left join item as i on SaleD.itno=i.itno
left join kind as k on i.kino=k.kino
left join dept as d on sa.deno=d.deno
left join spec as s on sa.spno=s.spno
left join cust as c on sa.cuno=c.cuno
left join xx01 as x on c.cux1no=x.x1no
left join xa01 as xa on saleD.xa1no= xa.xa1no
where 0=0 " + str;

                                string sql = @"
                                Select b.itname as boitname,b.itqty as boitqty,b.itunit as boitunit,i.kino,k.kiname, i.itnw,sa.Cuname1,sa.emname,sa.spname,SaleD.*,單據='銷貨',xa.Xa1Name 
                                from SaleD
                                inner join sale as sa on SaleD.sano=sa.sano 
                                left join item as i on SaleD.itno=i.itno
                                left join kind as k on i.kino=k.kino
                                left join xa01 as xa on saleD.xa1no= xa.xa1no
                                left join SaleBom as b on SaleD.BomID = b.BomID where 0=0 " + str;


                                cmd.CommandText = sql0;
                                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                {
                                    da.Fill(dtSD);
                                    for (int i = 0; i < dtSD.Rows.Count; i++)
                                    {
                                        if (money.Checked)
                                        {
                                            d = dtSD.Rows[i]["Mny"].ToDecimal("f" + Common.TPS);
                                            salemny += d;
                                        }
                                        else
                                        {
                                            db = dtSD.Rows[i]["Mnyb"].ToDecimal("f" + Common.TPS);
                                            salemny += db;
                                            dtSD.Rows[i]["xa1no"] = "本幣";
                                            dtSD.Rows[i]["Xa1Name"] = "本幣";
                                        }
                                        dtSD.Rows[i]["Mny"] = d;
                                        dtSD.Rows[i]["Mnyb"] = db;
                                        dtSD.Rows[i]["bracket"] = "s" + i.ToString();
                                        dtSD.Rows[i]["SaDate"] = Date.AddLine(dtSD.Rows[i]["SaDate"].ToString());
                                        dtSD.Rows[i]["SaDate1"] = Date.AddLine(dtSD.Rows[i]["SaDate1"].ToString());
                                    }
                                    if (Result0.Rows.Count == 0)
                                        Result0 = dtSD.Copy();
                                    else
                                        Result0.Merge(dtSD);
                                }

                                cmd.CommandText = sql;
                                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                {
                                    da.Fill(dtSaleD);
                                    for (int i = 0; i < dtSaleD.Rows.Count; i++)
                                    {
                                        dtSaleD.Rows[i]["Mny"] = dtSaleD.Rows[i]["Mny"].ToDecimal("f" + Common.TPS);
                                        dtSaleD.Rows[i]["Mnyb"] = dtSaleD.Rows[i]["Mnyb"].ToDecimal("f" + Common.TPS);
                                        dtSaleD.Rows[i]["bracket"] = "s" + i.ToString();
                                        dtSaleD.Rows[i]["SaDate"] = Date.AddLine(dtSaleD.Rows[i]["SaDate"].ToString());
                                        dtSaleD.Rows[i]["SaDate1"] = Date.AddLine(dtSaleD.Rows[i]["SaDate1"].ToString());
                                        if (!money.Checked)
                                        {
                                            dtSaleD.Rows[i]["xa1no"] = "本幣";
                                            dtSaleD.Rows[i]["Xa1Name"] = "本幣";
                                        }
                                    }
                                    if (Result.Rows.Count == 0)
                                        Result = dtSaleD.Copy();
                                    else
                                        Result.Merge(dtSaleD);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (ck2.Checked)
                {
                    str = "";
                    if (Common.User_DateTime == 1)
                    {
                        if (SaDate.Text.Trim() != "")
                            str += " and sa.SaDate >=@SaDate";
                        if (SaDate1.Text.Trim() != "")
                            str += " and sa.SaDate <=@SaDate1";
                    }
                    else
                    {
                        if (SaDate.Text.Trim() != "")
                            str += " and sa.SaDate1 >=@SaDate";
                        if (SaDate1.Text.Trim() != "")
                            str += " and sa.SaDate1 <=@SaDate1";
                    }
                    if (CuNo.Text.Trim() != "")
                        str += " and sa.cuno >=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (CuNo1.Text.Trim() != "")
                        str += " and sa.cuno <=@cuno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (EmNo.Text.Trim() != "")
                        str += " and sa.EmNo >=@emno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (EmNo1.Text.Trim() != "")
                        str += " and sa.EmNo <=@emno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (X4No.Text.Trim() != "")
                        str += " and sa.X4No >=@x4no COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (X4No1.Text.Trim() != "")
                        str += " and sa.X4No <=@x4no1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SpNo.Text.Trim() != "")
                        str += " and sa.SpNo >=@spno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SpNo1.Text.Trim() != "")
                        str += " and sa.SpNo <=@spno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SeNo.Text.Trim() != "")
                        str += " and sa.SeNo >=@seno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SeNo1.Text.Trim() != "")
                        str += " and sa.SeNo <=@seno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (ItNo.Text.Trim() != "")
                        str += " and RSaleD.ItNo >=@itno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (ItNo1.Text.Trim() != "")
                        str += " and RSaleD.ItNo <=@itno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (StNo.Text.Trim() != "")
                        str += " and RSaleD.StNo >=@stno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (StNo1.Text.Trim() != "")
                        str += " and RSaleD.StNo <=@stno1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (KiNo.Text.Trim() != "")
                        str += " and i.kiNo >=@kiNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (KiNo1.Text.Trim() != "")
                        str += " and i.kiNo <=@kiNo1 COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (Memo.Text.Trim() != "")
                        str += " and RSaleD.Memo like '%' + @memo + '%'";

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                        {
                            string sql0 = @"
Select sa.cardno,sa.SaPayment,sa.invno,sa." + Taxmny + @",sa." + tax + @",sa." + TotMny + @",d.dename1,s.spname,i.kino,k.kiname, i.itnw,sa.Cuname1,sa.emname,sa.spname,RSaleD.*,單據='銷退'
,c.cuper1,c.cutel1,c.cuatel1,c.cufax1,c.cuaddr1,c.cur1,c.cux1no,x.x1name,xa.Xa1Name
from RSaleD
inner join RSale as sa on RSaleD.sano=sa.sano 
left join item as i on RSaleD.itno=i.itno
left join kind as k on i.kino=k.kino
left join dept as d on sa.deno=d.deno
left join spec as s on sa.spno=s.spno
left join cust as c on sa.cuno=c.cuno
left join xx01 as x on c.cux1no=x.x1no
left join xa01 as xa on RSaleD.xa1no= xa.xa1no
where 0=0 "
                                + str;

                            string sql = @"
Select b.itname as boitname,b.itqty as boitqty,b.itunit as boitunit,i.kino, i.itnw,k.kiname,sa.Cuname1,sa.emname,sa.spname,RSaleD.*,單據='銷退',xa.Xa1Name  
from RSaleD
inner join RSale as sa on RSaleD.sano=sa.sano 
left join item as i on RSaleD.itno=i.itno
left join kind as k on i.kino=k.kino
left join xa01 as xa on RSaleD.xa1no= xa.xa1no
left join RSaleBom as b on RSaleD.BomID = b.BomID where 0=0 "
                                + str;
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("SaDate", SaDate.Text.Trim());
                                cmd.Parameters.AddWithValue("SaDate1", SaDate1.Text.Trim());
                                if (CuNo.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                                if (CuNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno1", CuNo1.Text.Trim());
                                if (EmNo.Text.Trim() != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                                if (EmNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                                if (X4No.Text.Trim() != "") cmd.Parameters.AddWithValue("x4no", X4No.Text.Trim());
                                if (X4No1.Text.Trim() != "") cmd.Parameters.AddWithValue("x4no1", X4No1.Text.Trim());
                                if (SpNo.Text.Trim() != "") cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                                if (SpNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("spno1", SpNo1.Text.Trim());
                                if (SeNo.Text.Trim() != "") cmd.Parameters.AddWithValue("seno", SeNo.Text.Trim());
                                if (SeNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("seno1", SeNo1.Text.Trim());
                                if (ItNo.Text.Trim() != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                                if (ItNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                                if (StNo.Text.Trim() != "") cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                                if (StNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("stno1", StNo1.Text.Trim());
                                if (KiNo.Text.Trim() != "") cmd.Parameters.AddWithValue("kino", KiNo.Text.Trim());
                                if (KiNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("kino1", KiNo1.Text.Trim());
                                if (Memo.Text.Trim() != "") cmd.Parameters.AddWithValue("memo", Memo.Text.Trim());

                                cmd.CommandText = sql0;
                                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                {
                                    da.Fill(dtRD);
                                    for (int i = 0; i < dtRD.Rows.Count; i++)
                                    {
                                        d = 0;
                                        if (money.Checked)
                                        {
                                            decimal.TryParse(dtRD.Rows[i]["Mny"].ToString(), out d);
                                            d *= -1;
                                            rsalemny += d;
                                            dtRD.Rows[i]["Mny"] = d.ToDecimal("f" + Common.TPS);
                                        }
                                        else
                                        {
                                            decimal.TryParse(dtRD.Rows[i]["MnyB"].ToString(), out d);
                                            d *= -1;
                                            rsalemny += d;
                                            dtRD.Rows[i]["MnyB"] = d.ToDecimal("f" + Common.M);
                                        }
                                        //decimal.TryParse(dtRD.Rows[i]["Mny"].ToString(), out d);
                                        //d *= -1;
                                        //rsalemny += d;
                                        //dtRD.Rows[i]["Mny"] = d.ToDecimal("f" + Common.TPS);
                                        //d = 0;
                                        //decimal.TryParse(dtRD.Rows[i]["MnyB"].ToString(), out d);
                                        //d *= -1;
                                        //dtRD.Rows[i]["MnyB"] = d.ToDecimal("f" + Common.M);
                                        d = 0;
                                        decimal.TryParse(dtRD.Rows[i]["Qty"].ToString(), out d);
                                        d *= -1;
                                        dtRD.Rows[i]["Qty"] = d.ToDecimal("f" + Common.Q);

                                        dtRD.Rows[i]["bracket"] = "r" + i.ToString();
                                        dtRD.Rows[i]["SaDate"] = Date.AddLine(dtRD.Rows[i]["SaDate"].ToString());
                                        dtRD.Rows[i]["SaDate1"] = Date.AddLine(dtRD.Rows[i]["SaDate1"].ToString());

                                        if (!money.Checked)
                                        {
                                            dtRD.Rows[i]["xa1no"] = "本幣";
                                            dtRD.Rows[i]["Xa1Name"] = "本幣";
                                        }
                                    }
                                    if (Result0.Rows.Count == 0)
                                        Result0 = dtRD.Copy();
                                    else
                                        Result0.Merge(dtRD);
                                }

                                cmd.CommandText = sql;
                                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                {
                                    da.Fill(RSaleD);
                                    for (int i = 0; i < RSaleD.Rows.Count; i++)
                                    {
                                        d = 0;
                                        decimal.TryParse(RSaleD.Rows[i]["Mny"].ToString(), out d);
                                        d *= -1;
                                        RSaleD.Rows[i]["Mny"] = d.ToDecimal("f" + Common.TPS);
                                        d = 0;
                                        decimal.TryParse(RSaleD.Rows[i]["MnyB"].ToString(), out d);
                                        d *= -1;
                                        RSaleD.Rows[i]["MnyB"] = d.ToDecimal("f" + Common.M);
                                        d = 0;
                                        decimal.TryParse(RSaleD.Rows[i]["Qty"].ToString(), out d);
                                        d *= -1;
                                        RSaleD.Rows[i]["Qty"] = d.ToDecimal("f" + Common.Q);

                                        RSaleD.Rows[i]["bracket"] = "r" + i.ToString();
                                        RSaleD.Rows[i]["SaDate"] = Date.AddLine(RSaleD.Rows[i]["SaDate"].ToString());
                                        RSaleD.Rows[i]["SaDate1"] = Date.AddLine(RSaleD.Rows[i]["SaDate1"].ToString());

                                        if (!money.Checked)
                                        {
                                            RSaleD.Rows[i]["xa1no"] = "本幣";
                                            RSaleD.Rows[i]["Xa1Name"] = "本幣";
                                        }
                                    }
                                    if (Result.Rows.Count == 0)
                                        Result = RSaleD.Copy();
                                    else
                                        Result.Merge(RSaleD);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (Result.Rows.Count == 0)
                    MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    this.OpemInfoFrom<FrmSale_Infob>(() =>
                    {
                        FrmSale_Infob fm = new FrmSale_Infob();
                        fm.table0 = Result0;
                        fm.table = Result;
                        fm.SaleCount = dtSD.Rows.Count;
                        fm.RSaleCount = dtRD.Rows.Count;
                        fm.SaleMny = salemny;
                        fm.RSaleMny = rsalemny;
                        string date = "日期區間： ";
                        date += Date.AddLine(SaDate.Text) + "～" + Date.AddLine(SaDate1.Text);
                        fm.DateRange = date;
						if (money.Checked)
                            fm.money = "money";
                        else
                            fm.money = "moneyb";

                        return fm;
                    });
                    //using (FrmSale_Infob frm = new FrmSale_Infob())
                    //{
                    //    frm.table0 = Result0;
                    //    frm.table = Result;
                    //    frm.SaleCount = dtSD.Rows.Count;
                    //    frm.RSaleCount = dtRD.Rows.Count;
                    //    frm.SaleMny = salemny;
                    //    frm.RSaleMny = rsalemny;
                    //    string date = "日期區間： ";
                    //    date += Date.AddLine(SaDate.Text) + "～" + Date.AddLine(SaDate1.Text);
                    //    frm.DateRange = date;
                    //    frm.ShowDialog();
                    //}
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }

        private void X4No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX04>(sender);
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Spec>(sender);
        }

        private void SeNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Send>(sender);
        }

        private void SaDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tx = sender as TextBox;
            if (!tx.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Common.User_DateTime == 1)
                {
                    SaDate.Text = Date.GetDateTime(1, false);
                    SaDate1.Text = Date.GetDateTime(1, false);
                    SaDate.Text = SaDate.Text.Remove(5) + "01";
                }
                else
                {
                    SaDate.Text = Date.GetDateTime(2, false);
                    SaDate1.Text = Date.GetDateTime(2, false);
                    SaDate.Text = SaDate.Text.Remove(6) + "01";
                }
            }
        }

        private void Memo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(Memo, 20);
        }

        private void money_CheckedChanged(object sender, EventArgs e)
        {
            if (money.Checked)//外幣
            {
                //sale
                Taxmny = "Taxmny";
                TotMny = "TotMny";
                tax = "tax";
                //saled 
                price = "price";
                taxprice = "taxprice";
                mny = "mny";
            }
            else//本幣
            {
                //sale
                Taxmny = "Taxmnyb";
                TotMny = "TotMnyb";
                tax = "taxb";
                //saled 
                price = "priceb";
                taxprice = "taxpriceb";
                mny = "mnyb";
            }
        }

    }
}
