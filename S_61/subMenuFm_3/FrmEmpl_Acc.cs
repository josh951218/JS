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
    public partial class FrmEmpl_Acc : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmEmpl_Acc()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmEmpl_Acc_Load(object sender, EventArgs e)
        { 
            SaDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            SaDate1.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            
            SaDate.Text = Date.GetDateTime(Common.User_DateTime);
            SaDate.Text = SaDate.Text.takeString(SaDate.Text.Length - 2) + "01";
            SaDate1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmEmpl_Acc_Shown(object sender, EventArgs e)
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
            if (btnExit.Focused) return;

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

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void EmNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
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
            if (EmNo.Text.BigThen(EmNo1.Text))
            {
                MessageBox.Show("起始廠商編號大於終止廠商編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EmNo.Focus();
                EmNo1.SelectAll();
                return;
            }

            //抓專案名稱
            string spname = "";
            if (SpNo.Text.ToString().Trim() != "")
            {
                DataTable spnamedt = new DataTable();
                string spnamesql = "select spname from spec where 0=0 and spno=@spno";
                using (SqlConnection spnamecn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = spnamecn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                        cmd.CommandText = spnamesql;
                        using (SqlDataAdapter spnameda = new SqlDataAdapter(cmd))
                        {
                            spnameda.Fill(spnamedt);
                            if (spnamedt.Rows.Count > 0)
                                spname = spnamedt.Rows[0][0].ToString().Trim();
                        }
                    }
                }
            }


            if (radioT2.Checked)
            {
                本幣總和表(spname);
                return;
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
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("bsdateAc", Date.ToTWDate(SaDate.Text));
                    cmd.Parameters.AddWithValue("bsdateAc1", Date.ToTWDate(SaDate1.Text));
                    if (EmNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    if (EmNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno1", EmNo.Text.Trim());
                    if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("fax4no", X4No.Text.Trim());
                    if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                    //表頭
                    string sql = " select *," + columns + " from bshop where 1=0 ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(tTitle);
                        tTitle.Clear();
                    }

                    //本期進貨(總)
                    sql = " select 單據='進貨',bshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,bshop.emno,bshop.emname,"
                        + " bshop.TaxMny,bshop.Tax,bshop.TotMny,bshop.invno,bshop.Discount,bshop.recordno as rdno,bshop.CollectMny,bshop.GetPrvAcc,bshop.AcctMny,bshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                        + " from bshopd "
                        + " left join bshop on bshopd.bsno = bshop.bsno "
                        + " left join fact on bshopd.fano = fact.fano "
                        + " where 0=0 "
                        + " and bshop.bsdateAc >=@bsdateAc"
                        + " and bshop.bsdateAc <=@bsdateAc1 and bshop.emno <> ''";
                    if (EmNo.Text.Trim().Length > 0) sql += " and bshop.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) sql += " and bshop.emno <=@emno1";
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
                            var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim() && r["emno"].ToString().Trim() == row["emno"].ToString().Trim());
                            if (p == null)
                            {
                                DataRow rw = tTitle.NewRow();
                                rw["fano"] = row["fano"].ToString().Trim();
                                rw["faname1"] = row["faname1"].ToString().Trim();
                                rw["emno"] = row["emno"].ToString().Trim();
                                rw["emname"] = row["emname"].ToString().Trim();
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
                    sql = " select 單據='進退',rshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,rshop.emno,rshop.emname,"
                        + " rshop.TaxMny,rshop.Tax,rshop.TotMny,rshop.invno,rshop.Discount,rshop.recordno as rdno,rshop.CollectMny,rshop.GetPrvAcc,rshop.AcctMny,rshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0 ,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                        + " from rshopd "
                        + " left join rshop on rshopd.bsno = rshop.bsno "
                        + " left join fact on rshopd.fano = fact.fano "
                        + " where 0=0 "
                        + " and rshop.bsdateAc >=@bsdateAc"
                        + " and rshop.bsdateAc <=@bsdateAc1 and rshop.emno <> ''";
                    if (EmNo.Text.Trim().Length > 0) sql += " and rshop.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) sql += " and rshop.emno <=@emno";
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
                            var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim() && r["emno"].ToString().Trim() == row["emno"].ToString().Trim());
                            if (p == null)
                            {
                                DataRow rw = tTitle.NewRow();
                                rw["fano"] = row["fano"].ToString().Trim();
                                rw["faname1"] = row["faname1"].ToString().Trim();
                                rw["emno"] = row["emno"].ToString().Trim();
                                rw["emname"] = row["emname"].ToString().Trim();
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
                    sql = " select 單據='進貨',bshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,bshop.emno,bshop.emname,"
                        + " bshop.TaxMny,bshop.Tax,bshop.TotMny,bshop.invno,bshop.Discount,bshop.recordno as rdno,bshop.CollectMny,bshop.GetPrvAcc,bshop.AcctMny,bshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0 ,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                        + " from bshopd "
                        + " left join bshop on bshopd.bsno = bshop.bsno "
                        + " left join fact on bshopd.fano = fact.fano "
                        + " where 0=0 "
                        + " and bshop.bsdateAc <@bsdateAc and bshop.emno <> ''";
                    if (EmNo.Text.Trim().Length > 0) sql += " and bshop.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) sql += " and bshop.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                    if (SpNo.Text.Trim().Length > 0) sql += " and bshop.SpNo =@SpNo";
                    if (radioT6.Checked) sql += " and bshop.AcctMny !=0 ";

                    cmd.CommandText = sql;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(tBefore);
                    }

                    //前期進退(未付總額)
                    sql = " select 單據='進退',rshopd.*,fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno,rshop.emno,rshop.emname,"
                        + " rshop.TaxMny,rshop.Tax,rshop.TotMny,rshop.invno,rshop.Discount,rshop.recordno as rdno,rshop.CollectMny,rshop.GetPrvAcc,rshop.AcctMny,rshop.Xa1Name,(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0 "
                        + " from rshopd "
                        + " left join rshop on rshopd.bsno = rshop.bsno "
                        + " left join fact on rshopd.fano = fact.fano "
                        + " where 0=0 "
                        + " and rshop.bsdateAc <@bsdateAc and rshop.emno <> ''";
                    if (EmNo.Text.Trim().Length > 0) sql += " and rshop.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) sql += " and rshop.emno <=@emno1";
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
                    }
                    tBefore.Merge(tTemp);
                    tBefore.AcceptChanges();

                    //期初開帳
                    cmd.CommandText = "Select * from fact where Len(faemno1) > 0 ";
                    if (EmNo.Text.Trim().Length > 0) cmd.CommandText += " and fact.faemno1 >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) cmd.CommandText += " and fact.faemno1 <=@emno1";
                    cmd.CommandText += " and fact.FaSparePay != 0";
                    DataTable dtFact = new DataTable();
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        dd.Fill(dtFact);
                    }

                    foreach (DataRow rw in dtFact.Rows)
                    {
                        var emno = rw["faemno1"].ToString().Trim();
                        var fano = rw["fano"].ToString().Trim();

                        DataRow row = tBefore.NewRow();
                        row["單據"] = "期初";
                        row["bsno"] = "上期餘額";

                        row["Xa1No"] = rw["FaXa1No"];
                        row["Xa1Name"] = "";
                        row["emno"] = emno;
                        row["emname"] = "";
                        row["fano"] = fano;
                        row["faname1"] = rw["faname1"];
                        row["faname2"] = rw["faname2"];
                        row["fatel1"] = rw["fatel1"];
                        row["faper1"] = rw["faper1"];
                        row["faaddr1"] = rw["faaddr1"];
                        row["fafax1"] = rw["fafax1"];
                        row["fauno"] = rw["fauno"];

                        row["TaxMny"] = 0;
                        row["Tax"] = 0;
                        row["TotMny"] = rw["FaSparePay"].ToDecimal("f" + Common.MFT);
                        row["Discount"] = 0;
                        row["CollectMny"] = 0;
                        row["GetPrvAcc"] = 0;
                        row["AcctMny"] = rw["FaSparePay"].ToDecimal("f" + Common.MFT);
                        row["已付預付"] = 0;

                        tBefore.Rows.Add(row);
                    }

                    foreach (var g in tBefore.AsEnumerable().GroupBy(r => r["bsno"].ToString().Trim()))
                    {
                        var row = g.FirstOrDefault();
                        var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim() && r["emno"].ToString().Trim() == row["emno"].ToString().Trim());
                        if (p == null)
                        {
                            DataRow rw = tTitle.NewRow();
                            rw["fano"] = row["fano"].ToString().Trim();
                            rw["faname1"] = row["faname1"].ToString().Trim();
                            rw["emno"] = row["emno"].ToString().Trim();
                            rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();
                            rw["emname"] = row["emname"].ToString().Trim();

                            rw["交易總筆數"] = 0;
                            rw["稅前總金額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["營業稅總額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["應付總金額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["折扣總金額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["已付加預付"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["本期總金額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["前期加本期"] = "0".ToDecimal().ToString("f" + Common.MFT);

                            rw["前期總金額"] = row["AcctMny"].ToDecimal().ToString("f" + Common.MFT);

                            tTitle.Rows.Add(rw);
                            tTitle.AcceptChanges();
                        }
                        else
                        {
                            p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["AcctMny"].ToDecimal()).ToString("f" + Common.MFT);
                        }
                    }


                    if (radioT4.Checked)
                    {
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
                            this.OpemInfoFrom<FrmEmpl_Accb>(() =>
                            {
                                FrmEmpl_Accb frm = new FrmEmpl_Accb();
                                frm.tTitle = tTitle.Copy();
                                frm.dt = tMaster.Copy();
                                frm.DateRange = SaDate.Text.Trim() + " ～ " + SaDate1.Text.Trim();
                                frm.spname = spname;
                                return frm;
                            });
                        }
                        else if (radioT2.Checked)
                        {
                            this.OpemInfoFrom<FrmEmpl_AccBrow>(() =>
                            {
                                FrmEmpl_AccBrow frm = new FrmEmpl_AccBrow();
                                frm.dt = tTitle.Copy();
                                frm.dt = frm.dt.AsEnumerable().OrderBy(r => r["fano"].ToString()).CopyToDataTable();
                                frm.DateRange = "帳款區間：" + SaDate.Text.Trim() + " ～ " + SaDate1.Text.Trim();
                                frm.spname = spname;
                                return frm;
                            });
                        }
                        else if (radioT3.Checked)
                        {
                            this.OpemInfoFrom<FrmEmpl_AccBrowx>(() =>
                            {
                                FrmEmpl_AccBrowx frm = new FrmEmpl_AccBrowx();
                                frm.dt = tTitle.Copy();
                                frm.dt = frm.dt.AsEnumerable().OrderBy(r => r["fano"].ToString()).CopyToDataTable();
                                frm.DateRange = "帳款區間：" + SaDate.Text.Trim() + " ～ " + SaDate1.Text.Trim();
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

        void 本幣總和表(string spname)
        {
            string columns = " 前期總金額=0.0, "
                               + " 交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, "
                               + " 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0  ";

            DataTable tTitle = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("bsdateAc", Date.ToTWDate(SaDate.Text));
                    cmd.Parameters.AddWithValue("bsdateAc1", Date.ToTWDate(SaDate1.Text));
                    if (EmNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    if (EmNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("emno1", EmNo.Text.Trim());
                    if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("fax4no", X4No.Text.Trim());
                    if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                    //表頭
                    string sql = " select *," + columns + " from bshop where 1=0 ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(tTitle);
                        tTitle.Clear();
                    }

                    #region //本期進貨(總)
                    sql = " select 單據='進貨',bshop.*," + columns
                        + ",(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                        + ",fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno"
                        + " from bshop "
                        + " left join fact on bshop.fano = fact.fano "
                        + " where bshop.bsdateAc >=@bsdateAc"
                        + " and bshop.bsdateAc <=@bsdateAc1 and bshop.emno <> ''";
                    if (EmNo.Text.Trim().Length > 0) sql += " and bshop.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) sql += " and bshop.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                    if (SpNo.Text.Trim().Length > 0) sql += " and bshop.SpNo =@SpNo";
                    if (radioT6.Checked) sql += " and bshop.AcctMny !=0 ";
                    cmd.CommandText = sql;
                    DataTable bshop = new DataTable();
                    dd.Fill(bshop);
                    #endregion

                    #region //本期進退(總)
                    sql = " select 單據='進退',rshop.*," + columns
                        + ",(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                        + ",fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno"
                        + " from rshop "
                        + " left join fact on rshop.fano = fact.fano "
                        + " where rshop.bsdateAc >=@bsdateAc"
                        + " and rshop.bsdateAc <=@bsdateAc1 and rshop.emno <> ''";
                    if (EmNo.Text.Trim().Length > 0) sql += " and rshop.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) sql += " and rshop.emno <=@emno";
                    if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                    if (SpNo.Text.Trim().Length > 0) sql += " and rshop.SpNo =@SpNo";
                    if (radioT6.Checked) sql += " and rshop.AcctMny !=0 ";
                    cmd.CommandText = sql;
                    DataTable btemp = new DataTable();
                    dd.Fill(btemp);
                    foreach (DataRow item in btemp.Rows)
                    {
                        item["TaxMnyB"] = -1 * item["TaxMnyB"].ToDecimal();
                        item["TaxB"] = -1 * item["TaxB"].ToDecimal();
                        item["TotMnyB"] = -1 * item["TotMnyB"].ToDecimal();
                        item["CollectMny"] = -1 * item["CollectMny"].ToDecimal();
                        item["GetPrvAcc"] = -1 * item["GetPrvAcc"].ToDecimal();
                        item["Discount"] = -1 * item["Discount"].ToDecimal();
                        item["AcctMny"] = -1 * item["AcctMny"].ToDecimal();
                    }
                    bshop.Merge(btemp);
                    btemp.Clear();
                    #endregion

                    foreach (DataRow row in bshop.Rows)
                    {
                        var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim() && r["emno"].ToString().Trim() == row["emno"].ToString().Trim());
                        if (p == null)
                        {
                            DataRow rw = tTitle.NewRow();
                            rw["fano"] = row["fano"].ToString().Trim();
                            rw["faname1"] = row["faname1"].ToString().Trim();
                            rw["emno"] = row["emno"].ToString().Trim();
                            rw["emname"] = row["emname"].ToString().Trim();
                            rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();
                            rw["前期總金額"] = 0;
                            rw["交易總筆數"] = 1;
                            rw["稅前總金額"] = row["TaxMnyB"].ToDecimal().ToString("f" + Common.MFT);
                            rw["營業稅總額"] = row["TaxB"].ToDecimal().ToString("f" + Common.MFT);
                            rw["應付總金額"] = row["TotMnyB"].ToDecimal().ToString("f" + Common.MFT);
                            rw["折扣總金額"] = row["Discount"].ToDecimal("f" + Common.MFT) * row["xa1par"].ToDecimal("f4");
                            rw["已付加預付"] = row["CollectMny"].ToDecimal("f" + Common.MFT) * row["Xa1par"].ToDecimal("f4")
                                               + row["GetPrvAcc"].ToDecimal("f" + Common.MFT) * row["Xa1par"].ToDecimal("f4");
                            rw["本期總金額"] = row["AcctMny"].ToDecimal("f" + Common.MFT) * row["Xa1par"].ToDecimal("f4");
                            rw["前期加本期"] = 0;

                            tTitle.Rows.Add(rw);
                            tTitle.AcceptChanges();
                        }
                        else
                        {
                            p["交易總筆數"] = (int)p["交易總筆數"].ToDecimal() + 1;
                            p["稅前總金額"] = p["稅前總金額"].ToDecimal() + row["TaxMnyB"].ToDecimal();
                            p["營業稅總額"] = p["營業稅總額"].ToDecimal() + row["TaxB"].ToDecimal();
                            p["應付總金額"] = p["應付總金額"].ToDecimal() + row["TotMnyB"].ToDecimal();

                            p["折扣總金額"] = p["折扣總金額"].ToDecimal() + (row["Discount"].ToDecimal("f" + Common.MFT) * row["Xa1par"].ToDecimal("f4"));
                            p["已付加預付"] = p["已付加預付"].ToDecimal() + (row["CollectMny"].ToDecimal("f" + Common.MFT) * row["Xa1par"].ToDecimal("f4")
                                                                            + row["GetPrvAcc"].ToDecimal("f" + Common.MFT) * row["Xa1par"].ToDecimal("f4"));
                            p["本期總金額"] = p["本期總金額"].ToDecimal() + (row["AcctMny"].ToDecimal("f" + Common.MFT) * row["Xa1par"].ToDecimal("f4"));
                        }
                    }

                    #region //前期進貨(總)
                    sql = " select 單據='進貨',bshop.*," + columns
                        + ",(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                        + ",fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno"
                        + " from bshop "
                        + " left join fact on bshop.fano = fact.fano "
                        + " where bshop.bsdateAc <@bsdateAc and bshop.emno <> ''";
                    if (EmNo.Text.Trim().Length > 0) sql += " and bshop.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) sql += " and bshop.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                    if (SpNo.Text.Trim().Length > 0) sql += " and bshop.SpNo =@SpNo";
                    if (radioT6.Checked) sql += " and bshop.AcctMny !=0 ";
                    cmd.CommandText = sql;
                    DataTable rshop = new DataTable();
                    dd.Fill(rshop);
                    #endregion

                    #region //前期進退(未付總額)
                    sql = " select 單據='進退',rshop.*," + columns
                        + ",(CollectMny+GetPrvAcc) as 已付預付,前期總金額=0.0,交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應付總金額=0.0, 折扣總金額=0.0,已付加預付=0.0,本期總金額=0.0,前期加本期=0.0"
                        + ",fact.faper1,fact.faname1,fact.faname2,折已取=0, fact.fano,fact.faper1,fact.fatel1,fact.fafax1,fact.faaddr1,fact.fauno"
                        + " from rshop "
                        + " left join fact on rshop.fano = fact.fano "
                        + " where rshop.bsdateAc <@bsdateAc and rshop.emno <> ''";
                    if (EmNo.Text.Trim().Length > 0) sql += " and rshop.emno >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) sql += " and rshop.emno <=@emno1";
                    if (X4No.Text.Trim().Length > 0) sql += " and fact.fax4no =@fax4no";
                    if (SpNo.Text.Trim().Length > 0) sql += " and rshop.SpNo =@SpNo";
                    if (radioT6.Checked) sql += " and rshop.AcctMny !=0 ";
                    cmd.CommandText = sql;
                    DataTable rtemp = new DataTable();
                    dd.Fill(rtemp);
                    foreach (DataRow item in rtemp.Rows)
                    {
                        item["TaxMnyB"] = -1 * item["TaxMnyB"].ToDecimal();
                        item["TaxB"] = -1 * item["TaxB"].ToDecimal();
                        item["TotMnyB"] = -1 * item["TotMnyB"].ToDecimal();
                        item["CollectMny"] = -1 * item["CollectMny"].ToDecimal();
                        item["GetPrvAcc"] = -1 * item["GetPrvAcc"].ToDecimal();
                        item["Discount"] = -1 * item["Discount"].ToDecimal();
                        item["AcctMny"] = -1 * item["AcctMny"].ToDecimal();
                    }
                    rshop.Merge(rtemp);
                    rtemp.Clear();
                    #endregion

                    #region //期初開帳
                    cmd.CommandText = "Select * from fact where Len(faemno1) > 0 ";
                    if (EmNo.Text.Trim().Length > 0) cmd.CommandText += " and fact.faemno1 >=@emno";
                    if (EmNo1.Text.Trim().Length > 0) cmd.CommandText += " and fact.faemno1 <=@emno1";
                    cmd.CommandText += " and fact.FaSparePay != 0";
                    DataTable dtFact = new DataTable();
                    dd.Fill(dtFact);

                    foreach (DataRow rw in dtFact.Rows)
                    {
                        var emno = rw["faemno1"].ToString().Trim();
                        var fano = rw["fano"].ToString().Trim();

                        DataRow row = rshop.NewRow();
                        row["單據"] = "期初";
                        row["bsno"] = "上期餘額";

                        row["Xa1No"] = rw["FaXa1No"];
                        row["Xa1Name"] = "";
                        row["emno"] = emno;
                        row["emname"] = "";
                        row["fano"] = fano;
                        row["faname1"] = rw["faname1"];
                        row["faname2"] = rw["faname2"];
                        row["fatel1"] = rw["fatel1"];
                        row["faper1"] = rw["faper1"];
                        row["faaddr1"] = rw["faaddr1"];
                        row["fafax1"] = rw["fafax1"];
                        row["fauno"] = rw["fauno"];

                        row["TaxMny"] = 0;
                        row["Tax"] = 0;
                        row["TotMny"] = rw["FaSparePay"].ToDecimal().ToDecimal("f"+Common.MFT);
                        row["Discount"] = 0;
                        row["CollectMny"] = 0;
                        row["GetPrvAcc"] = 0;
                        row["AcctMny"] = rw["FaSparePay"].ToDecimal().ToDecimal("f" + Common.MFT);
                        row["已付預付"] = 0;
                        row["xa1par"] = rw["FaFirPayPar"].ToDecimal();

                        rshop.Rows.Add(row);
                    }
                    #endregion

                    foreach (DataRow row in rshop.Rows)
                    {
                        var p = tTitle.AsEnumerable().ToList().Find(r => r["fano"].ToString().Trim() == row["fano"].ToString().Trim() && r["emno"].ToString().Trim() == row["emno"].ToString().Trim());
                        if (p == null)
                        {
                            DataRow rw = tTitle.NewRow();
                            rw["fano"] = row["fano"].ToString().Trim();
                            rw["faname1"] = row["faname1"].ToString().Trim();
                            rw["emno"] = row["emno"].ToString().Trim();
                            rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();
                            rw["emname"] = row["emname"].ToString().Trim();

                            rw["交易總筆數"] = 0;
                            rw["稅前總金額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["營業稅總額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["應付總金額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["折扣總金額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["已付加預付"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["本期總金額"] = "0".ToDecimal().ToString("f" + Common.MFT);
                            rw["前期加本期"] = "0".ToDecimal().ToString("f" + Common.MFT);

                            rw["前期總金額"] = row["AcctMny"].ToDecimal("f" + Common.MFT) * row["xa1par"].ToDecimal();

                            tTitle.Rows.Add(rw);
                            tTitle.AcceptChanges();
                        }
                        else
                        {
                            p["前期總金額"] = p["前期總金額"].ToDecimal() + (row["AcctMny"].ToDecimal("f" + Common.MFT) * row["xa1par"].ToDecimal());
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

                        this.OpemInfoFrom<FrmEmpl_AccBrow>(() =>
                        {
                            FrmEmpl_AccBrow frm = new FrmEmpl_AccBrow();
                            frm.dt = tTitle.Copy();
                            frm.dt = frm.dt.AsEnumerable().OrderBy(r => r["fano"].ToString()).CopyToDataTable();
                            frm.DateRange = "帳款區間：" + SaDate.Text.Trim() + " ～ " + SaDate1.Text.Trim();
                            frm.spname = spname;
                            return frm;
                        });
                    }
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
