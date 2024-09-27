using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmShop_Info : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable Shop = new DataTable();
        DataTable ShopD = new DataTable();
        DataTable RShop = new DataTable();
        DataTable RShopD = new DataTable();
        DataTable Result = new DataTable();
        decimal b, db;
        //bshop
        string Taxmny = "Taxmny";
        string TotMny = "TotMny";
        string tax = "tax";
        //bshopd 
        string price = "price";
        string taxprice = "taxprice";
        //string mny = "mny";


        public FrmShop_Info()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            lblT11.Text = Common.Sys_MemoUdf;
        }

        private void FrmShop_Info_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                BsDate.MaxLength = 7;
                BsDate1.MaxLength = 7;
                BsDate.Text = Date.GetDateTime(1, false);
                BsDate1.Text = Date.GetDateTime(1, false);
                BsDate.Text = BsDate.Text.Remove(5) + "01";
            }
            else
            {
                BsDate.MaxLength = 8;
                BsDate1.MaxLength = 8;
                BsDate.Text = Date.GetDateTime(2, false);
                BsDate1.Text = Date.GetDateTime(2, false);
                BsDate.Text = BsDate.Text.Remove(6) + "01";
            }
      
            BsDate.Focus();
        }

        bool Compare(TextBox tx, TextBox tx1)
        {
            bool flag = true;
            if (tx.Text.Trim() != "" && tx1.Text.Trim() != "")
            {
                if (string.CompareOrdinal(tx.Text, tx1.Text) > 0)
                {
                    flag = false;
                    FaNo.Focus();
                    MessageBox.Show("起始" + tx.Tag + "不可大於終止" + tx1.Tag + "，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return flag;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            Shop.Clear();
            ShopD.Clear();
            RShop.Clear();
            RShopD.Clear();

            if (!Compare(BsDate, BsDate1)) return;
            if (!Compare(FaNo, FaNo1)) return;
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
                if (BsDate.Text.Trim() != "")
                    str += " and  bsdate >=@bsdate";
                if (BsDate1.Text.Trim() != "")
                    str += " and  bsdate <=@bsdate1";
            }
            else
            {
                if (BsDate.Text.Trim() != "")
                    str += " and  bsdate1 >=@bsdate";
                if (BsDate1.Text.Trim() != "")
                    str += " and  bsdate1 <=@bsdate1";
            }

            if (FaNo.Text.Trim() != "")
                str += " and  fano >=@fano COLLATE Chinese_Taiwan_Stroke_BIN";
            if (FaNo1.Text.Trim() != "")
                str += " and  fano <=@fano1 COLLATE Chinese_Taiwan_Stroke_BIN";

            if (EmNo.Text.Trim() != "")
                str += " and emno >=@emno COLLATE Chinese_Taiwan_Stroke_BIN";
            if (EmNo1.Text.Trim() != "")
                str += " and emno <=@emno1 COLLATE Chinese_Taiwan_Stroke_BIN";

            if (X4No.Text.Trim() != "")
                str += " and X4No >=@X4No COLLATE Chinese_Taiwan_Stroke_BIN";
            if (X4No1.Text.Trim() != "")
                str += " and X4No <=@X4No1 COLLATE Chinese_Taiwan_Stroke_BIN";

            if (SpNo.Text.Trim() != "")
                str += " and SpNo >=@SpNo COLLATE Chinese_Taiwan_Stroke_BIN";
            if (SpNo1.Text.Trim() != "")
                str += " and SpNo <=@SpNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

            if (SeNo.Text.Trim() != "")
                str += " and SeNo >=@SeNo COLLATE Chinese_Taiwan_Stroke_BIN";
            if (SeNo1.Text.Trim() != "")
                str += " and SeNo <=@SeNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (BsDate.Text.Trim() != "") cmd.Parameters.AddWithValue("bsdate", BsDate.Text.Trim());
                        if (BsDate1.Text.Trim() != "") cmd.Parameters.AddWithValue("bsdate1", BsDate1.Text.Trim());
                        if (FaNo.Text.Trim() != "") cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                        if (FaNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("fano1", FaNo1.Text.Trim());
                        if (EmNo.Text.Trim() != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        if (EmNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                        if (X4No.Text.Trim() != "") cmd.Parameters.AddWithValue("X4No", X4No.Text.Trim());
                        if (X4No1.Text.Trim() != "") cmd.Parameters.AddWithValue("X4No1", X4No1.Text.Trim());
                        if (SpNo.Text.Trim() != "") cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());
                        if (SpNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("SpNo1", SpNo1.Text.Trim());
                        if (SeNo.Text.Trim() != "") cmd.Parameters.AddWithValue("SeNo", SeNo.Text.Trim());
                        if (SeNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("SeNo1", SeNo1.Text.Trim());

                        if (ck1.Checked == true)
                        {
                            sql = "select * from bshop where '0'='0'";
                            sql += str;
                            cmd.CommandText = sql; ;
                            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                            {
                                    dd.Fill(Shop);
                            }
                        }
                        if (ck2.Checked == true)
                        {
                            sql = "select * from rshop where '0'='0'";
                            sql += str;
                            cmd.CommandText = sql;
                            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                            {
                                dd.Fill(RShop);
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
                decimal shopmny = 0, rshopmny = 0;
                Result.Clear();
                b = 0;
                db = 0;
                if (ck1.Checked == true)
                {
                    str = "";
                    if (Common.User_DateTime == 1)
                    {
                        if (BsDate.Text.Trim() != "")
                            str += " and  s.bsdate >=@bsdate";
                        if (BsDate1.Text.Trim() != "")
                            str += " and  s.bsdate <=@bsdate1";
                    }
                    else
                    {
                        if (BsDate.Text.Trim() != "")
                            str += " and  s.bsdate1 >=@bsdate";
                        if (BsDate1.Text.Trim() != "")
                            str += " and  s.bsdate1 <=@bsdate1";
                    }

                    if (FaNo.Text.Trim() != "")
                        str += " and  s.fano >=@fano COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (FaNo1.Text.Trim() != "")
                        str += " and  s.fano <=@fano1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (EmNo.Text.Trim() != "")
                        str += " and s.emno >=@emno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (EmNo1.Text.Trim() != "")
                        str += " and s.emno <=@emno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (X4No.Text.Trim() != "")
                        str += " and s.X4No >=@X4No COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (X4No1.Text.Trim() != "")
                        str += " and s.X4No <=@X4No1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (SpNo.Text.Trim() != "")
                        str += " and s.SpNo >=@SpNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SpNo1.Text.Trim() != "")
                        str += " and s.SpNo <=@SpNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (SeNo.Text.Trim() != "")
                        str += " and s.SeNo >=@SeNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SeNo1.Text.Trim() != "")
                        str += " and s.SeNo <=@SeNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (ItNo.Text.Trim() != "")
                        str += " and BShopD.itno >=@itno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (ItNo1.Text.Trim() != "")
                        str += " and BShopD.itno <=@itno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (StNo.Text.Trim() != "")
                        str += " and BShopD.StNo >=@StNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (StNo1.Text.Trim() != "")
                        str += " and BShopD.StNo <=@StNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (KiNo.Text.Trim() != "")
                        str += " and i.KiNo >=@KiNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (KiNo1.Text.Trim() != "")
                        str += " and i.KiNo <=@KiNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (Memo.Text.Trim() != "")
                        str += " and BShopD.memo like '%' + @memo + '%' ";

                    try
                    {
                        string sql = "select i.kino,k.kiname,s.faname1,s.emname,s.spname,BShopD.*,xa.Xa1Name,單據='進貨' from BShopD"
                            + " inner join bshop as s on BShopD.bsno=s.bsno COLLATE Chinese_Taiwan_Stroke_BIN"
                            + " left join item as i on BShopD.itno=i.itno"
                            + " left join kind as k on i.kino=k.kino"
                            + " left join xa01 as xa on BshopD.xa1no=xa.xa1no"
                            + " where '0'='0'"
                            + str;
                        using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                        {
                            cn.Open();
                            using (SqlCommand cmd = cn.CreateCommand())
                            {
                                cmd.Parameters.Clear();
                                if (BsDate.Text.Trim() != "") cmd.Parameters.AddWithValue("bsdate", BsDate.Text.Trim());
                                if (BsDate1.Text.Trim() != "") cmd.Parameters.AddWithValue("bsdate1", BsDate1.Text.Trim());
                                if (FaNo.Text.Trim() != "") cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                                if (FaNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("fano1", FaNo1.Text.Trim());
                                if (EmNo.Text.Trim() != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                                if (EmNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                                if (X4No.Text.Trim() != "") cmd.Parameters.AddWithValue("X4No", X4No.Text.Trim());
                                if (X4No1.Text.Trim() != "") cmd.Parameters.AddWithValue("X4No1", X4No1.Text.Trim());
                                if (SpNo.Text.Trim() != "") cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());
                                if (SpNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("SpNo1", SpNo1.Text.Trim());
                                if (SeNo.Text.Trim() != "") cmd.Parameters.AddWithValue("SeNo", SeNo.Text.Trim());
                                if (SeNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("SeNo1", SeNo1.Text.Trim());
                                if (ItNo.Text.Trim() != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                                if (ItNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                                if (StNo.Text.Trim() != "") cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                                if (StNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                                if (KiNo.Text.Trim() != "") cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                                if (KiNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());
                                if (Memo.Text.Trim() != "") cmd.Parameters.AddWithValue("memo", Memo.Text.Trim());
                                cmd.CommandText = sql;
                                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                                {
                                    dd.Fill(ShopD);
                                    for (int i = 0; i < ShopD.Rows.Count; i++)
                                    {
                                        if (money.Checked)
                                        {
                                            b = ShopD.Rows[i]["mny"].ToDecimal("f" + Common.TPF);
                                            shopmny += b;
                                        }
                                        else
                                        {
                                            db = ShopD.Rows[i]["Mnyb"].ToDecimal("f" + Common.TPS);
                                            shopmny += db;
                                            ShopD.Rows[i]["xa1no"] = "本幣";
                                            ShopD.Rows[i]["Xa1Name"] = "本幣";
                                        } 

                                        ShopD.Rows[i]["mny"] = b;
                                        ShopD.Rows[i]["Mnyb"] = db;
                                        ShopD.Rows[i]["bsdate"] = Date.AddLine(ShopD.Rows[i]["bsdate"].ToString());
                                        ShopD.Rows[i]["bsdate1"] = Date.AddLine(ShopD.Rows[i]["bsdate1"].ToString());
                                        ShopD.Rows[i]["bracket"] = "r" + i.ToString();
                                    }
                                    if (Result.Rows.Count == 0)
                                        Result = ShopD.Copy();
                                    else
                                        Result.Merge(ShopD);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (ck2.Checked == true)
                {
                    str = "";
                    if (Common.User_DateTime == 1)
                    {
                        if (BsDate.Text.Trim() != "")
                            str += " and  s.bsdate >=@bsdate";
                        if (BsDate1.Text.Trim() != "")
                            str += " and  s.bsdate <=@bsdate1";
                    }
                    else
                    {
                        if (BsDate.Text.Trim() != "")
                            str += " and  s.bsdate1 >=@bsdate";
                        if (BsDate1.Text.Trim() != "")
                            str += " and  s.bsdate1 <=@bsdate1";
                    }

                    if (FaNo.Text.Trim() != "")
                        str += " and  s.fano >=@fano COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (FaNo1.Text.Trim() != "")
                        str += " and  s.fano <=@fano1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (EmNo.Text.Trim() != "")
                        str += " and s.emno >=@emno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (EmNo1.Text.Trim() != "")
                        str += " and s.emno <=@emno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (X4No.Text.Trim() != "")
                        str += " and s.X4No >=@X4No COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (X4No1.Text.Trim() != "")
                        str += " and s.X4No <=@X4No1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (SpNo.Text.Trim() != "")
                        str += " and s.SpNo >=@SpNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SpNo1.Text.Trim() != "")
                        str += " and s.SpNo <=@SpNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (SeNo.Text.Trim() != "")
                        str += " and s.SeNo >=@SeNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (SeNo1.Text.Trim() != "")
                        str += " and s.SeNo <=@SeNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (ItNo.Text.Trim() != "")
                        str += " and RShopD.itno >=@itno COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (ItNo1.Text.Trim() != "")
                        str += " and RShopD.itno <=@itno1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (StNo.Text.Trim() != "")
                        str += " and RShopD.StNo >=@StNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (StNo1.Text.Trim() != "")
                        str += " and RShopD.StNo <=@StNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (KiNo.Text.Trim() != "")
                        str += " and i.KiNo >=@KiNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    if (KiNo1.Text.Trim() != "")
                        str += " and i.KiNo <=@KiNo1 COLLATE Chinese_Taiwan_Stroke_BIN";

                    if (Memo.Text.Trim() != "")
                        str += " and RShopD.memo like '%' + @memo + '%' ";

                    try
                    {
                        string sql = "select i.kino,k.kiname,s.faname1,s.emname,s.spname,RShopD.*,xa.Xa1Name,單據='進退' from RShopD"
                            + " inner join rshop as s on RShopD.bsno=s.bsno COLLATE Chinese_Taiwan_Stroke_BIN"
                            + " left join item as i on RShopD.itno=i.itno"
                            + " left join kind as k on i.kino=k.kino"
                            + " left join xa01 as xa on RShopD.xa1no=xa.xa1no"
                            + "  where '0'='0'"
                            + str;
                        using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                        {
                            cn.Open();
                            using (SqlCommand cmd = cn.CreateCommand())
                            {
                                cmd.Parameters.Clear();
                                if (BsDate.Text.Trim() != "") cmd.Parameters.AddWithValue("bsdate", BsDate.Text.Trim());
                                if (BsDate1.Text.Trim() != "") cmd.Parameters.AddWithValue("bsdate1", BsDate1.Text.Trim());
                                if (FaNo.Text.Trim() != "") cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                                if (FaNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("fano1", FaNo1.Text.Trim());
                                if (EmNo.Text.Trim() != "") cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                                if (EmNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("emno1", EmNo1.Text.Trim());
                                if (X4No.Text.Trim() != "") cmd.Parameters.AddWithValue("X4No", X4No.Text.Trim());
                                if (X4No1.Text.Trim() != "") cmd.Parameters.AddWithValue("X4No1", X4No1.Text.Trim());
                                if (SpNo.Text.Trim() != "") cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());
                                if (SpNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("SpNo1", SpNo1.Text.Trim());
                                if (SeNo.Text.Trim() != "") cmd.Parameters.AddWithValue("SeNo", SeNo.Text.Trim());
                                if (SeNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("SeNo1", SeNo1.Text.Trim());
                                if (ItNo.Text.Trim() != "") cmd.Parameters.AddWithValue("itno", ItNo.Text.Trim());
                                if (ItNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("itno1", ItNo1.Text.Trim());
                                if (StNo.Text.Trim() != "") cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                                if (StNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                                if (KiNo.Text.Trim() != "") cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                                if (KiNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());
                                if (Memo.Text.Trim() != "") cmd.Parameters.AddWithValue("memo", Memo.Text.Trim());
                                cmd.CommandText = sql;
                                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                                {
                                    dd.Fill(RShopD);
                                    for (int i = 0; i < RShopD.Rows.Count; i++)
                                    {
                                        b = 0;
                                        decimal.TryParse(RShopD.Rows[i]["qty"].ToString(), out b);
                                        RShopD.Rows[i]["qty"] = (b * (-1)).ToDecimal("f" + Common.Q);

                                        b = 0;
                                        decimal.TryParse(RShopD.Rows[i]["mnyb"].ToString(), out b);
                                        RShopD.Rows[i]["mnyb"] = (b * (-1)).ToDecimal("f" + Common.M);

                                        b = 0;
                                        decimal.TryParse(RShopD.Rows[i]["mny"].ToString(), out b);
                                        RShopD.Rows[i]["mny"] = (b * (-1)).ToDecimal("f" + Common.TPF);
                                        rshopmny += b;

                                        RShopD.Rows[i]["bsdate"] = Date.AddLine(RShopD.Rows[i]["bsdate"].ToString());
                                        RShopD.Rows[i]["bsdate1"] = Date.AddLine(RShopD.Rows[i]["bsdate"].ToString());
                                        RShopD.Rows[i]["bracket"] = "r" + i.ToString();

                                        if (!money.Checked)
                                        {
                                            RShopD.Rows[i]["xa1no"] = "本幣";
                                            RShopD.Rows[i]["Xa1Name"] = "本幣";
                                        }
                                    }
                                }
                                if (Result.Rows.Count == 0)
                                    Result = RShopD.Copy();
                                else
                                    Result.Merge(RShopD);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                if (Result.Rows.Count == 0)
                {
                    MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //using (var frm = new FrmShop_Infob())
                    //{ 
                    //    frm.table = Result;
                    //    frm.ShopCount = ShopD.Rows.Count;
                    //    frm.RShopCount = RShopD.Rows.Count;
                    //    frm.ShopMny = shopmny;
                    //    frm.RShopMny = rshopmny;
                    //    string date = "日期區間:";
                    //    date += Date.AddLine(BsDate.Text.ToString()) + "～" + Date.AddLine(BsDate1.Text.ToString());
                    //    frm.DateRange = date;
                    //    frm.ShowDialog();
                    //}
                    this.OpemInfoFrom<FrmShop_Infob>(() =>
                            {
                                FrmShop_Infob frm = new FrmShop_Infob();
                                frm.table = Result;
                                frm.ShopCount = ShopD.Rows.Count;
                                frm.RShopCount = RShopD.Rows.Count;
                                frm.ShopMny = shopmny;
                                frm.RShopMny = rshopmny;
                                string date = "日期區間:";
                                date += Date.AddLine(BsDate.Text.ToString()) + "～" + Date.AddLine(BsDate1.Text.ToString());
                                frm.DateRange = date;
                                if (money.Checked)
                                    frm.money = "money";
                                else
                                    frm.money = "moneyb";
                                return frm;
                            });
                }
            }


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
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

        private void BsDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tx = sender as TextBox;
            if (!tx.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Common.User_DateTime == 1)
                {
                    BsDate.Text = Date.GetDateTime(1, false);
                    BsDate1.Text = Date.GetDateTime(1, false);
                    BsDate.Text = BsDate.Text.Remove(5) + "01";
                }
                else
                {
                    BsDate.Text = Date.GetDateTime(2, false);
                    BsDate1.Text = Date.GetDateTime(2, false);
                    BsDate.Text = BsDate.Text.Remove(6) + "01";
                }
            }
        }

        private void Memo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(Memo, 20);
        }

        private void money_CheckedChanged(object sender, EventArgs e)
        {
            
        }









    }
}
