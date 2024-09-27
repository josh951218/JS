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
    public partial class FrmFact_Acc : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmFact_Acc()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            SaDate.SetDateLength();
            SaDate1.SetDateLength();
        }

        private void FrmFact_Acc_Load(object sender, EventArgs e)
        {
            SaDate.Text = Date.GetDateTime(Common.User_DateTime);
            SaDate.Text = SaDate.Text.takeString(SaDate.Text.Length - 2) + "01";
            SaDate1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmFact_Acc_Shown(object sender, EventArgs e)
        {
            SaDate.Focus();
        }

        private void X4No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX04>(sender, row =>
            {
                X4No.Text = row["X4No"].ToString().Trim();
                X4Name.Text = row["X4Name"].ToString().Trim();
            });
        }
        private void X4No_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if (X4No.TrimTextLenth() == 0)
            {
                X4No.Clear();
                X4Name.Clear();
                return;
            }

            xe.ValidateOpen<JBS.JS.XX04>(sender, e, row =>
            {
                X4No.Text = row["X4No"].ToString().Trim();
                X4Name.Text = row["X4Name"].ToString().Trim();
            });
        }

        private void SaDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }
        private void SaDate1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }
        private void FaNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Spec>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (SaDate.Text.BigThen(SaDate1.Text))
            {
                MessageBox.Show("起始日期大於終止日期！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SaDate1.Focus();
                SaDate1.SelectAll();
                return;
            }

            if (FaNo.Text.BigThen(FaNo1.Text))
            {
                MessageBox.Show("起始廠商編號大於終止廠商編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                FaNo.SelectAll();
                return;
            }

            if (radioT2.Checked)
            {
                本幣總額表();
                return;
            }

            //抓專案名稱
            string spname = "";
            if (SpNo.Text.ToString().Trim() != "")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());

                    cn.Open();
                    cmd.CommandText = "select spname from spec where 0=0 and spno=@spno";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                            return;

                        reader.Read();
                        spname = reader["spname"].ToString().Trim();
                    }
                }
            }


            DataTable tTitle = new DataTable();
            DataTable tMaster = new DataTable();
            DataTable tBefore = new DataTable();
            DataTable tTemp = new DataTable();

            try
            {
                string columns = " 前期總金額=0.0, "
                               + " 交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, "
                               + " 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0  ";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdateAc", Date.ToTWDate(SaDate.Text));
                        cmd.Parameters.AddWithValue("bsdateAc1", Date.ToTWDate(SaDate1.Text));
                        if (FaNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("faNo", FaNo.Text.Trim());
                        if (FaNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("faNo1", FaNo1.Text.Trim());
                        if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("fax4no", X4No.Text.Trim());
                        if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                        //表頭
                        string sql = " select top 1 *," + columns + " from bshop where 0=0 ";
                        using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                        {
                            da.Fill(tTitle);
                            tTitle.Clear();
                        }

                        //本期進貨(總)
                        sql = " select 單據='進貨',bshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,fact.FaPayAmt,"
                            + " bshop.TaxMny,bshop.Tax,bshop.TotMny,bshop.invno,bshop.Discount,bshop.recordno as rdno,bshop.CollectMny,bshop.GetPrvAcc,bshop.AcctMny,bshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                            + " from bshopd "
                            + " left join bshop on bshopd.bsno = bshop.bsno "
                            + " left join fact on bshopd.fano = fact.fano "
                            + " where 0=0 "
                            + " and bshop.bsdateAc >=@bsdateAc"
                            + " and bshop.bsdateAc <=@bsdateAc1";
                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (SpNo.Text.Trim().Length > 0) sql += " and bshop.SpNo =@SpNo";
                        if (radioT6.Checked) sql += " and bshop.AcctMny !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tMaster);
                            foreach (var g in tMaster.AsEnumerable().GroupBy(r => r["bsno"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();
                                    rw["前期總金額"] = 0;
                                    rw["交易總筆數"] = 1;
                                    rw["稅前總金額"] = row["TaxMny"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["營業稅總額"] = row["Tax"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["應付總金額"] = row["TotMny"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["折扣總金額"] = row["Discount"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["已付加預付"] = (row["CollectMny"].ToDecimal() + row["GetPrvAcc"].ToDecimal()).ToString("f" + Common.MFT);
                                    rw["本期總金額"] = row["AcctMny"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["交易總筆數"] = (int)p["交易總筆數"].ToDecimal() + 1;
                                    p["稅前總金額"] = (p["稅前總金額"].ToDecimal() + row["TaxMny"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["營業稅總額"] = (p["營業稅總額"].ToDecimal() + row["Tax"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["應付總金額"] = (p["應付總金額"].ToDecimal() + row["TotMny"].ToDecimal()).ToString("f" + Common.MFT);

                                    p["折扣總金額"] = (p["折扣總金額"].ToDecimal() + row["Discount"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["已付加預付"] = (p["已付加預付"].ToDecimal() + (row["CollectMny"].ToDecimal() + row["GetPrvAcc"].ToDecimal())).ToString("f" + Common.MFT);
                                    p["本期總金額"] = (p["本期總金額"].ToDecimal() + row["AcctMny"].ToDecimal()).ToString("f" + Common.MFT);
                                }
                            }
                        }

                        //本期進退(總)
                        sql = " select 單據='進退',rshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,fact.FaPayAmt,"
                            + " rshop.TaxMny,rshop.Tax,rshop.TotMny,rshop.invno,rshop.Discount,rshop.recordno as rdno,rshop.CollectMny,rshop.GetPrvAcc,rshop.AcctMny,rshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0 ,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                            + " from rshopd "
                            + " left join rshop on rshopd.bsno = rshop.bsno "
                            + " left join fact on rshopd.fano = fact.fano "
                            + " where 0=0 "
                            + " and rshop.bsdateAc >=@bsdateAc"
                            + " and rshop.bsdateAc <=@bsdateAc1";
                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (SpNo.Text.Trim().Length > 0) sql += " and rshop.SpNo =@SpNo";
                        if (radioT6.Checked) sql += " and rshop.AcctMny !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            tTemp = new DataTable();
                            da.Fill(tTemp);
                            //進退金額變負數
                            for (int i = 0; i < tTemp.Rows.Count; i++)
                            {
                                tTemp.Rows[i]["price"] = -1 * tTemp.Rows[i]["price"].ToDecimal();
                                tTemp.Rows[i]["Mny"] = -1 * tTemp.Rows[i]["Mny"].ToDecimal("f" + Common.TPF);
                                tTemp.Rows[i]["TaxMny"] = -1 * tTemp.Rows[i]["TaxMny"].ToDecimal();
                                tTemp.Rows[i]["Tax"] = -1 * tTemp.Rows[i]["Tax"].ToDecimal();
                                tTemp.Rows[i]["TotMny"] = -1 * tTemp.Rows[i]["TotMny"].ToDecimal();
                                tTemp.Rows[i]["CollectMny"] = -1 * tTemp.Rows[i]["CollectMny"].ToDecimal();
                                tTemp.Rows[i]["GetPrvAcc"] = -1 * tTemp.Rows[i]["GetPrvAcc"].ToDecimal();
                                tTemp.Rows[i]["Discount"] = -1 * tTemp.Rows[i]["Discount"].ToDecimal();
                                tTemp.Rows[i]["AcctMny"] = -1 * tTemp.Rows[i]["AcctMny"].ToDecimal();
                            }

                            foreach (var g in tTemp.AsEnumerable().GroupBy(r => r["bsno"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();
                                    rw["前期總金額"] = 0;
                                    rw["交易總筆數"] = 1;
                                    rw["稅前總金額"] = row["TaxMny"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["營業稅總額"] = row["Tax"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["應付總金額"] = row["TotMny"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["折扣總金額"] = row["Discount"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["已付加預付"] = (row["CollectMny"].ToDecimal() + row["GetPrvAcc"].ToDecimal()).ToString("f" + Common.MFT);
                                    rw["本期總金額"] = row["AcctMny"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["交易總筆數"] = (int)p["交易總筆數"].ToDecimal() + 1;
                                    p["稅前總金額"] = (p["稅前總金額"].ToDecimal() + row["TaxMny"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["營業稅總額"] = (p["營業稅總額"].ToDecimal() + row["Tax"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["應付總金額"] = (p["應付總金額"].ToDecimal() + row["TotMny"].ToDecimal()).ToString("f" + Common.MFT);

                                    p["折扣總金額"] = (p["折扣總金額"].ToDecimal() + row["Discount"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["已付加預付"] = (p["已付加預付"].ToDecimal() + (row["CollectMny"].ToDecimal() + row["GetPrvAcc"].ToDecimal())).ToString("f" + Common.MFT);
                                    p["本期總金額"] = (p["本期總金額"].ToDecimal() + row["AcctMny"].ToDecimal()).ToString("f" + Common.MFT);
                                }
                            }
                        }
                        tMaster.Merge(tTemp);
                        tMaster.AcceptChanges();

                        //前期進貨(未付總額)
                        sql = " select 單據='進貨',bshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,fact.FaPayAmt,"
                            + " bshop.TaxMny,bshop.Tax,bshop.TotMny,bshop.invno,bshop.Discount,bshop.recordno as rdno,bshop.CollectMny,bshop.GetPrvAcc,bshop.AcctMny,bshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0 ,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                            + " from bshopd "
                            + " left join bshop on bshopd.bsno = bshop.bsno "
                            + " left join fact on bshopd.fano = fact.fano "
                            + " where 0=0 "
                            + " and bshop.bsdateAc <@bsdateAc";
                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (SpNo.Text.Trim().Length > 0) sql += " and bshop.SpNo =@SpNo";
                        if (radioT6.Checked) sql += " and bshop.AcctMny !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tBefore);
                            foreach (var g in tBefore.AsEnumerable().GroupBy(r => r["bsno"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();

                                    rw["交易總筆數"] = 0;
                                    rw["前期總金額"] = row["AcctMny"].ToDecimal().ToString("f" + Common.MFT);

                                    rw["稅前總金額"] = 0;
                                    rw["營業稅總額"] = 0;
                                    rw["應付總金額"] = 0;
                                    rw["折扣總金額"] = 0;
                                    rw["已付加預付"] = 0;
                                    rw["本期總金額"] = 0;
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["AcctMny"].ToDecimal()).ToString("f" + Common.MFT);
                                }
                            }
                        }

                        //前期進退(未付總額)
                        sql = " select 單據='進退',rshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,fact.FaPayAmt,"
                            + " rshop.TaxMny,rshop.Tax,rshop.TotMny,rshop.invno,rshop.Discount,rshop.recordno as rdno,rshop.CollectMny,rshop.GetPrvAcc,rshop.AcctMny,rshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0 "
                            + " from rshopd "
                            + " left join rshop on rshopd.bsno = rshop.bsno "
                            + " left join fact on rshopd.fano = fact.fano "
                            + " where 0=0 "
                            + " and rshop.bsdateAc <@bsdateAc";

                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (SpNo.Text.Trim().Length > 0) sql += " and rshop.SpNo =@SpNo";
                        if (radioT6.Checked) sql += " and rshop.AcctMny !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            tTemp = new DataTable();
                            da.Fill(tTemp);
                            //進退金額變負數
                            for (int i = 0; i < tTemp.Rows.Count; i++)
                            {
                                tTemp.Rows[i]["price"] = -1 * tTemp.Rows[i]["price"].ToDecimal();
                                tTemp.Rows[i]["Mny"] = -1 * tTemp.Rows[i]["Mny"].ToDecimal("f" + Common.TPF);
                                tTemp.Rows[i]["TaxMny"] = -1 * tTemp.Rows[i]["TaxMny"].ToDecimal();
                                tTemp.Rows[i]["Tax"] = -1 * tTemp.Rows[i]["Tax"].ToDecimal();
                                tTemp.Rows[i]["TotMny"] = -1 * tTemp.Rows[i]["TotMny"].ToDecimal();
                                tTemp.Rows[i]["CollectMny"] = -1 * tTemp.Rows[i]["CollectMny"].ToDecimal();
                                tTemp.Rows[i]["GetPrvAcc"] = -1 * tTemp.Rows[i]["GetPrvAcc"].ToDecimal();
                                tTemp.Rows[i]["Discount"] = -1 * tTemp.Rows[i]["Discount"].ToDecimal();
                                tTemp.Rows[i]["AcctMny"] = -1 * tTemp.Rows[i]["AcctMny"].ToDecimal();
                            }
                            foreach (var g in tTemp.AsEnumerable().GroupBy(r => r["bsno"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();

                                    rw["交易總筆數"] = 0;
                                    rw["前期總金額"] = row["AcctMny"].ToDecimal().ToString("f" + Common.MFT);

                                    rw["稅前總金額"] = 0;
                                    rw["營業稅總額"] = 0;
                                    rw["應付總金額"] = 0;
                                    rw["折扣總金額"] = 0;
                                    rw["已付加預付"] = 0;
                                    rw["本期總金額"] = 0;
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["AcctMny"].ToDecimal()).ToString("f" + Common.MFT);
                                }
                            }
                        }
                        tBefore.Merge(tTemp);
                        tBefore.AcceptChanges();

                        //期初
                        sql = " select " + columns + ",fact.*,Xa1Name from fact "
                            + " left join Xa01 on fact.faXa1No = Xa01.Xa1No where 0=0 ";

                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (radioT6.Checked) sql += " and fact.FaSparePay !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            tTemp = new DataTable();
                            da.Fill(tTemp);
                            foreach (DataRow row in tTemp.Rows)
                            {
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();

                                    rw["交易總筆數"] = 0;
                                    rw["前期總金額"] = row["FaSparePay"].ToDecimal().ToString("f" + Common.MFT);

                                    rw["稅前總金額"] = 0;
                                    rw["營業稅總額"] = 0;
                                    rw["應付總金額"] = 0;
                                    rw["折扣總金額"] = 0;
                                    rw["已付加預付"] = 0;
                                    rw["本期總金額"] = 0;
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["FaSparePay"].ToDecimal()).ToString("f" + Common.MFT);
                                }
                            }
                        }

                        if (radioT4.Checked)
                        {
                            for (int i = 0; i < tTemp.Rows.Count; i++)
                            {
                                DataRow row = tMaster.NewRow();
                                row["bsdate"] = "   /  /  ";
                                row["bsdate1"] = "    /  /  ";
                                row["fano"] = tTemp.Rows[i]["fano"].ToString().Trim();
                                row["faname1"] = tTemp.Rows[i]["faname1"].ToString().Trim();
                                row["faname2"] = tTemp.Rows[i]["faname2"].ToString().Trim();
                                row["Xa1Name"] = tTemp.Rows[i]["Xa1Name"].ToString().Trim();
                                row["faaddr1"] = tTemp.Rows[i]["faaddr1"].ToString().Trim();
                                row["fatel1"] = tTemp.Rows[i]["fatel1"].ToString().Trim();
                                row["fafax1"] = tTemp.Rows[i]["fafax1"].ToString().Trim();
                                row["fauno"] = tTemp.Rows[i]["fauno"].ToString().Trim();
                                row["faper1"] = tTemp.Rows[i]["faper1"].ToString().Trim();

                                row["單據"] = "期初";
                                row["bsno"] = "上期餘額";
                                row["qty"] = "0".ToDecimal().ToString("f" + Common.Q);
                                row["Mny"] = "0".ToDecimal().ToString("f" + Common.TPF);
                                row["price"] = "0".ToDecimal().ToString("f" + Common.MFT);
                                row["TaxMny"] = "0".ToDecimal().ToString("f" + Common.MFT);
                                row["Tax"] = "0".ToDecimal().ToString("f" + Common.MFT);
                                row["TotMny"] = tTemp.Rows[i]["FaSparePay"].ToDecimal().ToString("f" + Common.MFT);
                                row["Discount"] = "0".ToDecimal().ToString("f" + Common.MFT);
                                row["CollectMny"] = "0".ToDecimal().ToString("f" + Common.MFT);
                                row["GetPrvAcc"] = "0".ToDecimal().ToString("f" + Common.MFT);
                                row["AcctMny"] = tTemp.Rows[i]["FaSparePay"].ToDecimal().ToString("f" + Common.MFT);
                                row["FaPayAmt"] = tTemp.Rows[i]["FaPayAmt"].ToDecimal().ToString("f" + Common.MFT);
                                tMaster.Rows.Add(row);
                                tMaster.AcceptChanges();
                            }
                            tMaster.Merge(tBefore);
                        }

                        if (tTitle.Rows.Count == 0)
                        {
                            MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            foreach (DataRow row in tTitle.Rows)
                            {
                                row["前期加本期"] = (row["前期總金額"].ToDecimal() + row["本期總金額"].ToDecimal()).ToString("f" + Common.MFT);
                            }
                            if (radioT1.Checked)
                            {
                                this.OpemInfoFrom<FrmFact_Accb>(() =>
                                {
                                    FrmFact_Accb frm = new FrmFact_Accb();
                                    frm.tTitle = tTitle.AsEnumerable().OrderBy(r => r["fano"].ToString()).CopyToDataTable();
                                    if (tMaster.Rows.Count > 0)
                                        frm.dt = tMaster.AsEnumerable().OrderBy(r => r["fano"].ToString()).ThenBy(r => r["bsdate"].ToString()).CopyToDataTable();
                                    else
                                        frm.dt = tMaster.Clone();
                                    frm.DateRange = Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                    frm.spname = spname;
                                    return frm;
                                });
                            }
                            else if (radioT2.Checked)
                            {
                                this.OpemInfoFrom<FrmFact_AccBrow>(() =>
                                {
                                    FrmFact_AccBrow frm = new FrmFact_AccBrow();
                                    frm.dt = tTitle.AsEnumerable().OrderBy(r => r["fano"].ToString()).CopyToDataTable();

                                    frm.DateRange = "帳款區間：" + Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                    frm.spname = spname;
                                    return frm;
                                });
                            }
                            else if (radioT3.Checked)
                            {
                                this.OpemInfoFrom<FrmFact_AccBrowx>(() =>
                                {
                                    FrmFact_AccBrowx frm = new FrmFact_AccBrowx();
                                    frm.dt = tTitle.AsEnumerable().OrderBy(r => r["fano"].ToString()).CopyToDataTable();

                                    frm.DateRange = "帳款區間：" + Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                    frm.spname = spname;
                                    return frm;
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        void 本幣總額表()
        {
            //抓專案名稱
            string spname = "";
            if (SpNo.Text.ToString().Trim() != "")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());

                    cn.Open();
                    cmd.CommandText = "select spname from spec where 0=0 and spno=@spno";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                            return;

                        reader.Read();
                        spname = reader["spname"].ToString().Trim();
                    }
                }
            }


            DataTable tTitle = new DataTable();
            DataTable tMaster = new DataTable();
            DataTable tBefore = new DataTable();
            DataTable tTemp = new DataTable();

            try
            {
                string columns = " 前期總金額=0.0, "
                               + " 交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, "
                               + " 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0,FaPayAmt=0.0  ";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bsdateAc", Date.ToTWDate(SaDate.Text));
                        cmd.Parameters.AddWithValue("bsdateAc1", Date.ToTWDate(SaDate1.Text));
                        if (FaNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("faNo", FaNo.Text.Trim());
                        if (FaNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("faNo1", FaNo1.Text.Trim());
                        if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("fax4no", X4No.Text.Trim());
                        if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                        //表頭
                        string sql = " select top 1 *," + columns + " from bshop where 1=0 ";
                        using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                        {
                            da.Fill(tTitle);
                        }

                        //本期進貨(總)
                        sql = " select 單據='進貨',bshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,fact.FaPayAmt,"
                            + " bshop.TaxMnyB,bshop.TaxB,bshop.TotMnyB,bshop.invno,bshop.Discount,bshop.recordno as rdno,bshop.CollectMny,bshop.GetPrvAcc,bshop.AcctMny,bshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                            + " from bshopd "
                            + " left join bshop on bshopd.bsno = bshop.bsno "
                            + " left join fact on bshopd.fano = fact.fano "
                            + " where 0=0 "
                            + " and bshop.bsdateAc >=@bsdateAc"
                            + " and bshop.bsdateAc <=@bsdateAc1";
                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (SpNo.Text.Trim().Length > 0) sql += " and bshop.SpNo =@SpNo";
                        if (radioT6.Checked) sql += " and bshop.AcctMny !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tMaster);
                            foreach (var g in tMaster.AsEnumerable().GroupBy(r => r["bsno"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());

                                var xa1par = row["xa1par"].ToDecimal();
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();
                                    rw["前期總金額"] = 0;
                                    rw["交易總筆數"] = 1;
                                    rw["稅前總金額"] = row["TaxMnyB"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["營業稅總額"] = row["TaxB"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["應付總金額"] = row["TotMnyB"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["折扣總金額"] = (row["Discount"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                    rw["已付加預付"] = (row["CollectMny"].ToDecimal() * xa1par + row["GetPrvAcc"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                    rw["本期總金額"] = (row["AcctMny"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                    rw["前期加本期"] = 0;
                                    rw["FaPayAmt"] = row["FaPayAmt"].ToDecimal();

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["交易總筆數"] = (int)p["交易總筆數"].ToDecimal() + 1;
                                    p["稅前總金額"] = (p["稅前總金額"].ToDecimal() + row["TaxMnyB"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["營業稅總額"] = (p["營業稅總額"].ToDecimal() + row["TaxB"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["應付總金額"] = (p["應付總金額"].ToDecimal() + row["TotMnyB"].ToDecimal()).ToString("f" + Common.MFT);

                                    p["折扣總金額"] = (p["折扣總金額"].ToDecimal() + row["Discount"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                    p["已付加預付"] = (p["已付加預付"].ToDecimal() + (row["CollectMny"].ToDecimal() * xa1par + row["GetPrvAcc"].ToDecimal() * xa1par)).ToString("f" + Common.MFT);
                                    p["本期總金額"] = (p["本期總金額"].ToDecimal() + row["AcctMny"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                }
                            }
                        }

                        //本期進退(總)
                        sql = " select 單據='進退',rshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,fact.FaPayAmt,"
                            + " rshop.TaxMnyB,rshop.TaxB,rshop.TotMnyB,rshop.invno,rshop.Discount,rshop.recordno as rdno,rshop.CollectMny,rshop.GetPrvAcc,rshop.AcctMny,rshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0 ,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                            + " from rshopd "
                            + " left join rshop on rshopd.bsno = rshop.bsno "
                            + " left join fact on rshopd.fano = fact.fano "
                            + " where 0=0 "
                            + " and rshop.bsdateAc >=@bsdateAc"
                            + " and rshop.bsdateAc <=@bsdateAc1";
                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (SpNo.Text.Trim().Length > 0) sql += " and rshop.SpNo =@SpNo";
                        if (radioT6.Checked) sql += " and rshop.AcctMny !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            tTemp = new DataTable();
                            da.Fill(tTemp);
                            //進退金額變負數
                            for (int i = 0; i < tTemp.Rows.Count; i++)
                            {
                                tTemp.Rows[i]["price"] = -1 * tTemp.Rows[i]["price"].ToDecimal();
                                tTemp.Rows[i]["Mny"] = -1 * tTemp.Rows[i]["Mny"].ToDecimal("f" + Common.TPF);

                                tTemp.Rows[i]["TaxMnyB"] = -1 * tTemp.Rows[i]["TaxMnyB"].ToDecimal();
                                tTemp.Rows[i]["TaxB"] = -1 * tTemp.Rows[i]["TaxB"].ToDecimal();
                                tTemp.Rows[i]["TotMnyB"] = -1 * tTemp.Rows[i]["TotMnyB"].ToDecimal();
                                tTemp.Rows[i]["CollectMny"] = -1 * tTemp.Rows[i]["CollectMny"].ToDecimal();
                                tTemp.Rows[i]["GetPrvAcc"] = -1 * tTemp.Rows[i]["GetPrvAcc"].ToDecimal();
                                tTemp.Rows[i]["Discount"] = -1 * tTemp.Rows[i]["Discount"].ToDecimal();
                                tTemp.Rows[i]["AcctMny"] = -1 * tTemp.Rows[i]["AcctMny"].ToDecimal();
                            }

                            foreach (var g in tTemp.AsEnumerable().GroupBy(r => r["bsno"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());

                                var xa1par = row["xa1par"].ToDecimal();
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();
                                    rw["前期總金額"] = 0;
                                    rw["交易總筆數"] = 1;
                                    rw["稅前總金額"] = row["TaxMnyb"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["營業稅總額"] = row["Taxb"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["應付總金額"] = row["TotMnyb"].ToDecimal().ToString("f" + Common.MFT);
                                    rw["折扣總金額"] = (row["Discount"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                    rw["已付加預付"] = (row["CollectMny"].ToDecimal() * xa1par + row["GetPrvAcc"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                    rw["本期總金額"] = (row["AcctMny"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                    rw["前期加本期"] = 0;
                                    rw["FaPayAmt"] = row["FaPayAmt"].ToDecimal();

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["交易總筆數"] = (int)p["交易總筆數"].ToDecimal() + 1;
                                    p["稅前總金額"] = (p["稅前總金額"].ToDecimal() + row["TaxMnyb"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["營業稅總額"] = (p["營業稅總額"].ToDecimal() + row["Taxb"].ToDecimal()).ToString("f" + Common.MFT);
                                    p["應付總金額"] = (p["應付總金額"].ToDecimal() + row["TotMnyb"].ToDecimal()).ToString("f" + Common.MFT);

                                    p["折扣總金額"] = (p["折扣總金額"].ToDecimal() + row["Discount"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                    p["已付加預付"] = (p["已付加預付"].ToDecimal() + (row["CollectMny"].ToDecimal() * xa1par + row["GetPrvAcc"].ToDecimal() * xa1par)).ToString("f" + Common.MFT);
                                    p["本期總金額"] = (p["本期總金額"].ToDecimal() + row["AcctMny"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                }
                            }
                        }
                        tMaster.Merge(tTemp);
                        tMaster.AcceptChanges();

                        //前期進貨(未付總額)
                        sql = " select 單據='進貨',bshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,fact.FaPayAmt,"
                            + " bshop.TaxMnyb,bshop.Taxb,bshop.TotMnyb,bshop.invno,bshop.Discount,bshop.recordno as rdno,bshop.CollectMny,bshop.GetPrvAcc,bshop.AcctMny,bshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0 ,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                            + " from bshopd "
                            + " left join bshop on bshopd.bsno = bshop.bsno "
                            + " left join fact on bshopd.fano = fact.fano "
                            + " where 0=0 "
                            + " and bshop.bsdateAc <@bsdateAc";
                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (SpNo.Text.Trim().Length > 0) sql += " and bshop.SpNo =@SpNo";
                        if (radioT6.Checked) sql += " and bshop.AcctMny !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tBefore);
                            foreach (var g in tBefore.AsEnumerable().GroupBy(r => r["bsno"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());
                                var xa1par = row["xa1par"].ToDecimal();
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();

                                    rw["交易總筆數"] = 0;
                                    rw["前期總金額"] = (row["AcctMny"].ToDecimal() * xa1par).ToString("f" + Common.MFT);

                                    rw["稅前總金額"] = 0;
                                    rw["營業稅總額"] = 0;
                                    rw["應付總金額"] = 0;
                                    rw["折扣總金額"] = 0;
                                    rw["已付加預付"] = 0;
                                    rw["本期總金額"] = 0;
                                    rw["前期加本期"] = 0;
                                    rw["FaPayAmt"] = row["FaPayAmt"].ToDecimal();

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["AcctMny"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                }
                            }
                        }

                        //前期進退(未付總額)
                        sql = " select 單據='進退',rshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,fact.FaPayAmt,"
                            + " rshop.TaxMnyb,rshop.Taxb,rshop.TotMnyb,rshop.invno,rshop.Discount,rshop.recordno as rdno,rshop.CollectMny,rshop.GetPrvAcc,rshop.AcctMny,rshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0 "
                            + " from rshopd "
                            + " left join rshop on rshopd.bsno = rshop.bsno "
                            + " left join fact on rshopd.fano = fact.fano "
                            + " where 0=0 "
                            + " and rshop.bsdateAc <@bsdateAc";

                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (SpNo.Text.Trim().Length > 0) sql += " and rshop.SpNo =@SpNo";
                        if (radioT6.Checked) sql += " and rshop.AcctMny !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            tTemp = new DataTable();
                            da.Fill(tTemp);
                            //進退金額變負數
                            for (int i = 0; i < tTemp.Rows.Count; i++)
                            {
                                tTemp.Rows[i]["price"] = -1 * tTemp.Rows[i]["price"].ToDecimal();
                                tTemp.Rows[i]["Mny"] = -1 * tTemp.Rows[i]["Mny"].ToDecimal("f" + Common.TPF);

                                tTemp.Rows[i]["TaxMnyb"] = -1 * tTemp.Rows[i]["TaxMnyb"].ToDecimal();
                                tTemp.Rows[i]["Taxb"] = -1 * tTemp.Rows[i]["Taxb"].ToDecimal();
                                tTemp.Rows[i]["TotMnyb"] = -1 * tTemp.Rows[i]["TotMnyb"].ToDecimal();
                                tTemp.Rows[i]["CollectMny"] = -1 * tTemp.Rows[i]["CollectMny"].ToDecimal();
                                tTemp.Rows[i]["GetPrvAcc"] = -1 * tTemp.Rows[i]["GetPrvAcc"].ToDecimal();
                                tTemp.Rows[i]["Discount"] = -1 * tTemp.Rows[i]["Discount"].ToDecimal();
                                tTemp.Rows[i]["AcctMny"] = -1 * tTemp.Rows[i]["AcctMny"].ToDecimal();
                            }
                            foreach (var g in tTemp.AsEnumerable().GroupBy(r => r["bsno"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());
                                var xa1par = row["xa1par"].ToDecimal();
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();

                                    rw["交易總筆數"] = 0;
                                    rw["前期總金額"] = (row["AcctMny"].ToDecimal() * xa1par).ToString("f" + Common.MFT);

                                    rw["稅前總金額"] = 0;
                                    rw["營業稅總額"] = 0;
                                    rw["應付總金額"] = 0;
                                    rw["折扣總金額"] = 0;
                                    rw["已付加預付"] = 0;
                                    rw["本期總金額"] = 0;
                                    rw["前期加本期"] = 0;
                                    rw["FaPayAmt"] = row["FaPayAmt"].ToDecimal();

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["AcctMny"].ToDecimal() * xa1par).ToString("f" + Common.MFT);
                                }
                            }
                        }
                        tBefore.Merge(tTemp);
                        tBefore.AcceptChanges();

                        //期初
                        sql = " select " + columns + ",FaPayAmt1=fact.FaPayAmt,fact.*,Xa1Name from fact "
                            + " left join Xa01 on fact.faXa1No = Xa01.Xa1No where 0=0 ";

                        if (FaNo.Text.Trim().Length > 0) sql += " and fact.faNo >=@faNo";
                        if (FaNo1.Text.Trim().Length > 0) sql += " and fact.faNo <=@faNo1";
                        if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                        if (radioT6.Checked) sql += " and fact.FaSparePay !=0 ";

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            tTemp = new DataTable();
                            da.Fill(tTemp);
                            foreach (DataRow row in tTemp.Rows)
                            {
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["fano"] = row["fano"].ToString().Trim();
                                    rw["faname1"] = row["faname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();

                                    rw["交易總筆數"] = 0;
                                    rw["前期總金額"] = (row["FaSparePay"].ToDecimal() * row["FaFirPayPar"].ToDecimal()).ToString("f" + Common.MFT);

                                    rw["稅前總金額"] = 0;
                                    rw["營業稅總額"] = 0;
                                    rw["應付總金額"] = 0;
                                    rw["折扣總金額"] = 0;
                                    rw["已付加預付"] = 0;
                                    rw["本期總金額"] = 0;
                                    rw["前期加本期"] = 0;
                                    rw["FaPayAmt"] = row["FaPayAmt1"].ToDecimal();

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["FaSparePay"].ToDecimal() * row["FaFirPayPar"].ToDecimal()).ToString("f" + Common.MFT);
                                }
                            }
                        }

                        if (tTitle.Rows.Count == 0)
                        {
                            MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            foreach (DataRow row in tTitle.Rows)
                            {
                                row["前期加本期"] = (row["前期總金額"].ToDecimal() + row["本期總金額"].ToDecimal()).ToString("f" + Common.MFT);
                            }

                            //using (var frm = new FrmFact_AccBrow())
                            //{ 
                            //    frm.dt = tTitle.AsEnumerable().OrderBy(r => r["fano"].ToString()).CopyToDataTable(); 
                            //    frm.DateRange = "帳款區間：" + SaDate.Text.Trim() + " ～ " + SaDate1.Text.Trim();
                            //    frm.spname = spname;
                            //    frm.ShowDialog();
                            //}
                            this.OpemInfoFrom<FrmFact_AccBrow>(() =>
                            {
                                FrmFact_AccBrow frm = new FrmFact_AccBrow();
                                frm.dt = tTitle.AsEnumerable().OrderBy(r => r["fano"].ToString()).CopyToDataTable();
                                frm.DateRange = "帳款區間：" + Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                frm.spname = spname;
                                return frm;
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
    }
}
