using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmCust_Acc : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable tempcust = new DataTable();
        public FrmCust_Acc()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmCust_Acc_Load(object sender, EventArgs e)
        {
            radioT1.Checked = radioT5.Checked = radioT6.Checked = true;
            SaDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            SaDate1.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            SaDate.Text = Date.GetDateTime(Common.User_DateTime);
            SaDate.Text = SaDate.Text.takeString(SaDate.Text.Length - 2) + "01";
            SaDate1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmCust_Acc_Shown(object sender, EventArgs e)
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

        private void X4No_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar)) e.Handled = true;
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

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }
        private void CuNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Spec>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        class MyClass : AppData.DSReportTableAdapters.本幣總額表銷貨TableAdapter
        {
            public string GetCommomText()
            {
                try
                {
                    return base.CommandCollection.FirstOrDefault().CommandText;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.Dispose();
                }
            }
        }
        class MyClass1 : AppData.DSReportTableAdapters.本幣總額表銷退TableAdapter
        {
            public string GetCommomText()
            {
                try
                {
                    return base.CommandCollection.FirstOrDefault().CommandText;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.Dispose();
                }
            }
        }

        class danju
        {
            public string cuno { get; set; }
            public string payerno { get; set; }
            public decimal 本期銷項金額 { get; set; }
            public decimal 前期總金額 { get; set; }
            public int 交易總筆數 { get; set; }

            public decimal 本期稅前金額 { get; set; }
            public decimal 本期營業稅額 { get; set; }
            public decimal 本期單據總額 { get; set; }

            public decimal 本期折扣金額 { get; set; }
            public decimal 本期已收預收 { get; set; }
            public decimal 本期應收總額 { get; set; }
        }

        void work1(ref DataTable tNow, string tSale)
        {
            tNow.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("sadateAc", Date.ToTWDate(SaDate.Text));
                cmd.Parameters.AddWithValue("sadateAc1", Date.ToTWDate(SaDate1.Text));
                cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());
                cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                cmd.CommandText = tSale;
                da.Fill(tNow);
            }
            var day = (Common.User_DateTime == 1) ? "sadateac" : "sadateac1";
            for (int i = 0; i < tNow.Rows.Count; i++)
            {
                tNow.Rows[i]["帳款日期"] = Date.AddLine(tNow.Rows[i][day].ToString().Trim());
                tNow.Rows[i]["Mny"] = tNow.Rows[i]["Mny"].ToDecimal("f" + Common.TPS);
                if (tNow.Rows[i]["payerno"].ToString().Trim() != "")
                {
                    ///tNow.Rows[i]["出貨客戶"] = tNow.Rows[i]["cuname1"];
                    tNow.Rows[i]["cuno"] = tNow.Rows[i]["payerno"].ToString();
                }
            }
        }
        void work2(ref DataTable tNtemp, string tRsale)
        {
            tNtemp.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("sadateAc", Date.ToTWDate(SaDate.Text));
                cmd.Parameters.AddWithValue("sadateAc1", Date.ToTWDate(SaDate1.Text));
                cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());
                cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                cmd.CommandText = tRsale;
                da.Fill(tNtemp);
            }
            //銷退金額變負數
            var day = (Common.User_DateTime == 1) ? "sadateac" : "sadateac1";
            for (int i = 0; i < tNtemp.Rows.Count; i++)
            {
                tNtemp.Rows[i]["帳款日期"] = Date.AddLine(tNtemp.Rows[i][day].ToString().Trim());
                tNtemp.Rows[i]["已收預收"] = -1 * tNtemp.Rows[i]["已收預收"].ToDecimal();

                tNtemp.Rows[i]["price"] = -1 * tNtemp.Rows[i]["price"].ToDecimal();
                tNtemp.Rows[i]["Mny"] = -1 * tNtemp.Rows[i]["Mny"].ToDecimal("f" + Common.TPS);
                tNtemp.Rows[i]["TaxMny"] = -1 * tNtemp.Rows[i]["TaxMny"].ToDecimal();
                tNtemp.Rows[i]["Tax"] = -1 * tNtemp.Rows[i]["Tax"].ToDecimal();
                tNtemp.Rows[i]["TotMny"] = -1 * tNtemp.Rows[i]["TotMny"].ToDecimal();
                tNtemp.Rows[i]["CollectMny"] = -1 * tNtemp.Rows[i]["CollectMny"].ToDecimal();
                tNtemp.Rows[i]["GetPrvAcc"] = -1 * tNtemp.Rows[i]["GetPrvAcc"].ToDecimal();
                tNtemp.Rows[i]["Discount"] = -1 * tNtemp.Rows[i]["Discount"].ToDecimal();
                tNtemp.Rows[i]["AcctMny"] = -1 * tNtemp.Rows[i]["AcctMny"].ToDecimal();
                if (tNtemp.Rows[i]["payerno"].ToString().Trim() != "")
                {
                    //tNtemp.Rows[i]["出貨客戶"] = tNtemp.Rows[i]["cuname1"];
                    tNtemp.Rows[i]["cuno"] = tNtemp.Rows[i]["payerno"].ToString();
                }
            }
        }
        void work3(ref DataTable tBefore, string bSale)
        {
            tBefore.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("sadateAc", Date.ToTWDate(SaDate.Text));
                cmd.Parameters.AddWithValue("sadateAc1", Date.ToTWDate(SaDate1.Text));
                cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());
                cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                cmd.CommandText = bSale;
                da.Fill(tBefore);
            }
            for (int i = 0; i < tBefore.Rows.Count; i++)
            {
                if (tBefore.Rows[i]["payerno"].ToString().Trim() != "")
                {
                    ///tNow.Rows[i]["出貨客戶"] = tNow.Rows[i]["cuname1"];
                    tBefore.Rows[i]["cuno"] = tBefore.Rows[i]["payerno"].ToString();
                }
            }

            //顯示前期明細
            if (radioT4.Checked)
            {
                var day = (Common.User_DateTime == 1) ? "sadateac" : "sadateac1";
                for (int i = 0; i < tBefore.Rows.Count; i++)
                {
                    tBefore.Rows[i]["帳款日期"] = Date.AddLine(tBefore.Rows[i][day].ToString().Trim());
                    tBefore.Rows[i]["Mny"] = tBefore.Rows[i]["Mny"].ToDecimal("f" + Common.TPS);
                }
            }
        }
        void work4(ref DataTable tBtemp, string bRsale)
        {
            tBtemp.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("sadateAc", Date.ToTWDate(SaDate.Text));
                cmd.Parameters.AddWithValue("sadateAc1", Date.ToTWDate(SaDate1.Text));
                cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());
                cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                cmd.CommandText = bRsale;
                da.Fill(tBtemp);
            }
            for (int i = 0; i < tBtemp.Rows.Count; i++)
            {
                if (tBtemp.Rows[i]["payerno"].ToString().Trim() != "")
                {
                    //tBtemp.Rows[i]["出貨客戶"] = tBtemp.Rows[i]["cuname1"];
                    tBtemp.Rows[i]["cuno"] = tBtemp.Rows[i]["payerno"].ToString();
                }
            }

            //顯示前期明細
            if (radioT4.Checked)
            {
                var day = (Common.User_DateTime == 1) ? "sadateac" : "sadateac1";
                for (int i = 0; i < tBtemp.Rows.Count; i++)
                {
                    tBtemp.Rows[i]["帳款日期"] = Date.AddLine(tBtemp.Rows[i][day].ToString().Trim());
                    tBtemp.Rows[i]["已收預收"] = -1 * tBtemp.Rows[i]["已收預收"].ToDecimal();

                    tBtemp.Rows[i]["price"] = -1 * tBtemp.Rows[i]["price"].ToDecimal();
                    tBtemp.Rows[i]["Mny"] = -1 * tBtemp.Rows[i]["Mny"].ToDecimal("f" + Common.TPS);
                    tBtemp.Rows[i]["TaxMny"] = -1 * tBtemp.Rows[i]["TaxMny"].ToDecimal();
                    tBtemp.Rows[i]["Tax"] = -1 * tBtemp.Rows[i]["Tax"].ToDecimal();
                    tBtemp.Rows[i]["TotMny"] = -1 * tBtemp.Rows[i]["TotMny"].ToDecimal();
                    tBtemp.Rows[i]["CollectMny"] = -1 * tBtemp.Rows[i]["CollectMny"].ToDecimal();
                    tBtemp.Rows[i]["GetPrvAcc"] = -1 * tBtemp.Rows[i]["GetPrvAcc"].ToDecimal();
                    tBtemp.Rows[i]["Discount"] = -1 * tBtemp.Rows[i]["Discount"].ToDecimal();
                    tBtemp.Rows[i]["AcctMny"] = -1 * tBtemp.Rows[i]["AcctMny"].ToDecimal();

                }
            }
            else
            {
                //前期不顯示,只要計算前期未收金額,銷退*-1
                for (int i = 0; i < tBtemp.Rows.Count; i++)
                {
                    tBtemp.Rows[i]["AcctMny"] = -1 * tBtemp.Rows[i]["AcctMny"].ToDecimal();
                }
            }
        }
        void work5(ref DataTable tCust, string bCust)
        {
            tCust.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());

                cmd.CommandText = bCust;
                da.Fill(tCust);
            }
        }

        void 明細簡要表()
        {
            DataTable tNow = new DataTable();
            DataTable tNtemp = new DataTable();
            DataTable tBefore = new DataTable();
            DataTable tBtemp = new DataTable();
            DataTable tCust = new DataTable();
            ConcurrentStack<danju> stack = new ConcurrentStack<danju>();

            var columns1 = @"
                帳款日期='',(CollectMny+GetPrvAcc) as 已收預收,本期銷項金額=0.0, 
                前期總金額=0.0,交易總筆數=0.0,
                本期稅前金額=0.0,本期營業稅額=0.0,本期單據總額=0.0,
                本期折扣金額=0.0,本期已收預收=0.0,本期應收總額=0.0 ";
            var columnscu = @"
               cust.cuname2,cust.cuper1,cust.cutel1,cufax1,CuAtel1,cur1,cuaddr1,cuuno,CuAdvamt,";

            //全部或非零只針對本期,前期一律抓非零
            #region T-SQL-SALE
            var tSale = @"
                select sale.adper1 as sale_adper1,sale.adtel as sale_adtel ,sale.adaddr as sale_adaddr,單據='銷貨',saled.*," + columnscu + @"
                sale.emname,sale.TaxMny,sale.Tax,sale.TotMny,sale.invno,sale.Discount,sale.recordno as rdno,sale.CollectMny,sale.GetPrvAcc,sale.AcctMny,sale.Xa1Name,sale.samemo,sale.payerno,出貨客戶=sale.cuname1,cust.cuname1," + columns1 + @"
                from saled 
                left join sale on saled.sano = sale.sano 
                left join cust on sale.payerno = cust.cuno 
                where 0=0 
                and sale.sadateAc >=@sadateAc
                and sale.sadateAc <=@sadateAc1 ";
            if (CuNo.Text.Trim().Length > 0) tSale += " and sale.payerno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) tSale += " and sale.payerno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) tSale += " and cust.cux4no =@cux4no";
            if (SpNo.Text.Trim().Length > 0) tSale += " and sale.SpNo =@SpNo";
            if (radioT6.Checked) tSale += " and sale.AcctMny !=0 ";
            #endregion

            #region T-SQL-RSALE
            var tRsale = @"
                select  sale_adper1 ='' ,sale_adtel ='',sale_adaddr='',單據='銷退',rsaled.*," + columnscu + @"
                rsale.emname,rsale.TaxMny,rsale.Tax,rsale.TotMny,rsale.invno,rsale.Discount,rsale.recordno as rdno,rsale.CollectMny,rsale.GetPrvAcc,rsale.AcctMny,rsale.Xa1Name,rsale.samemo,rsale.payerno,出貨客戶=rsale.cuname1,cust.cuname1," + columns1 + @"
                from rsaled 
                left join rsale on rsaled.sano = rsale.sano 
                left join cust on rsale.payerno = cust.cuno
                where 0=0 
                and rsale.sadateAc >=@sadateAc
                and rsale.sadateAc <=@sadateAc1 ";
            if (CuNo.Text.Trim().Length > 0) tRsale += " and rsale.payerno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) tRsale += " and rsale.payerno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) tRsale += " and cust.cux4no =@cux4no";
            if (SpNo.Text.Trim().Length > 0) tRsale += " and rsale.SpNo =@SpNo";
            if (radioT6.Checked) tRsale += " and rsale.AcctMny !=0  ";
            #endregion

            #region T-SQL-B-SALE
            var bSale = @"
                select sale.adper1 as sale_adper1,sale.adtel as sale_adtel,sale.adaddr as sale_adaddr,單據='銷貨',saled.*," + columnscu + @"
                sale.emname,sale.TaxMny,sale.Tax,sale.TotMny,sale.Discount,sale.invno,sale.recordno as rdno,sale.CollectMny,sale.GetPrvAcc,sale.AcctMny,sale.Xa1Name,sale.payerno,出貨客戶=sale.cuname1,cust.cuname1," + columns1 + @"
                from saled 
                left join sale on saled.sano = sale.sano 
                left join cust on sale.payerno = cust.cuno
                where 0=0 
                and saled.sadateAc < @sadateAc ";
            if (CuNo.Text.Trim().Length > 0) bSale += " and sale.payerno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) bSale += " and sale.payerno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) bSale += " and cust.cux4no =@cux4no";
            if (SpNo.Text.Trim().Length > 0) bSale += " and sale.SpNo =@SpNo";
            bSale += " and sale.AcctMny !=0 ";
            #endregion

            #region T-SQL-B-RSALE
            var bRsale = @"
                select  sale_adper1 ='' ,sale_adtel ='',sale_adaddr='',單據='銷退',rsaled.*," + columnscu + @"
rsale.emname,rsale.TaxMny,rsale.Tax,rsale.TotMny,rsale.Discount,rsale.invno,rsaled.recordno as rdno,rsale.CollectMny,rsale.GetPrvAcc,rsale.AcctMny,rsale.Xa1Name,rsale.payerno,出貨客戶=rsale.cuname1,cust.cuname1," + columns1 + @"
                from rsaled 
                left join rsale on rsaled.sano = rsale.sano  
                left join cust on rsale.payerno = cust.cuno 
                where 0=0 
                and rsaled.sadateAc < @sadateAc ";
            if (CuNo.Text.Trim().Length > 0) bRsale += " and rsale.payerno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) bRsale += " and rsale.payerno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) bRsale += " and cust.cux4no =@cux4no";
            if (SpNo.Text.Trim().Length > 0) bRsale += " and rsale.SpNo =@SpNo";
            bRsale += " and rsale.AcctMny !=0 ";
            #endregion

            #region T-SQL-CUST
            var bCust = @"
                select CuNo,CuSpareRcv from Cust 
                where 0=0 ";
            if (CuNo.Text.Trim().Length > 0) bCust += " and cust.cuno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) bCust += " and cust.cuno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) bCust += " and cust.cux4no =@cux4no";
            bCust += " and cust.CuSpareRcv !=0 ";
            #endregion

            #region 撈資料庫
            Parallel.For(0, 5, i =>
            {
                if (i == 0)
                    work1(ref tNow, tSale);
                else if (i == 1)
                    work2(ref tNtemp, tRsale);
                else if (i == 2)
                    work3(ref tBefore, bSale);
                else if (i == 3)
                    work4(ref tBtemp, bRsale);
                else if (i == 4)
                    work5(ref tCust, bCust);
            });
            tNow.Merge(tNtemp);
            tBefore.Merge(tBtemp);
            #endregion

            #region 本期
            tNow.AsEnumerable()
                .AsParallel()
                .GroupBy(r => r["cuno"].ToString().Trim())
                .ForAll(groups =>
                {
                    var 本期銷項金額 = 0M;
                    var 交易總筆數 = 0;

                    var 本期稅前金額 = 0M;
                    var 本期營業稅額 = 0M;
                    var 本期單據總額 = 0M;

                    var 本期折扣金額 = 0M;
                    var 本期已收預收 = 0M;
                    var 本期應收總額 = 0M;

                    groups.GroupBy(g => new
                    {
                        key1 = g["sano"].ToString().Trim(),
                        key2 = g["單據"].ToString()
                    })
                   .Aggregate(0, (x, sano) =>
                   {
                       本期銷項金額 += sano.Sum(sr => sr["mny"].ToDecimal("f" + Common.TPS));
                       交易總筆數++;

                       本期稅前金額 += sano.First()["TaxMny"].ToDecimal("f" + Common.MST);
                       本期營業稅額 += sano.First()["Tax"].ToDecimal("f" + Common.TS);
                       本期單據總額 += sano.First()["TotMny"].ToDecimal("f" + Common.MST);

                       本期折扣金額 += sano.First()["Discount"].ToDecimal("f" + Common.MST);
                       本期已收預收 += sano.First()["已收預收"].ToDecimal("f" + Common.MST);
                       本期應收總額 += sano.First()["AcctMny"].ToDecimal("f" + Common.MST);
                       return x;
                   });

                    stack.Push(new danju
                    {
                        cuno = groups.Key,
                        本期銷項金額 = 本期銷項金額,
                        前期總金額 = 0M,
                        交易總筆數 = 交易總筆數,

                        本期稅前金額 = 本期稅前金額,
                        本期營業稅額 = 本期營業稅額,
                        本期單據總額 = 本期單據總額,

                        本期折扣金額 = 本期折扣金額,
                        本期已收預收 = 本期已收預收,
                        本期應收總額 = 本期應收總額
                    });
                });
            #endregion

            #region 前期
            tBefore.AsEnumerable()
                .AsParallel()
                .GroupBy(r => r["cuno"].ToString().Trim())
                .ForAll(groups =>
                {
                    var 前期總金額 = 0M;

                    groups.GroupBy(g => new
                    {
                        key1 = g["sano"].ToString().Trim(),
                        key2 = g["單據"].ToString()
                    })
                    .Aggregate(0, (x, sano) =>
                    {
                        前期總金額 += sano.First()["AcctMny"].ToDecimal("f" + Common.MST);
                        return x;
                    });

                    stack.Push(new danju
                    {
                        cuno = groups.Key,
                        本期銷項金額 = 0M,
                        前期總金額 = 前期總金額,
                        交易總筆數 = 0,

                        本期稅前金額 = 0M,
                        本期營業稅額 = 0M,
                        本期單據總額 = 0M,

                        本期折扣金額 = 0M,
                        本期已收預收 = 0M,
                        本期應收總額 = 0M
                    });
                });
            #endregion

            #region 期初
            tCust.AsEnumerable()
                .AsParallel()
                .ForAll(cust =>
                {
                    stack.Push(new danju
                    {
                        cuno = cust["cuno"].ToString().Trim(),
                        本期銷項金額 = 0M,
                        前期總金額 = cust["CuSpareRcv"].ToDecimal("f" + Common.MST),
                        交易總筆數 = 0,

                        本期稅前金額 = 0M,
                        本期營業稅額 = 0M,
                        本期單據總額 = 0M,

                        本期折扣金額 = 0M,
                        本期已收預收 = 0M,
                        本期應收總額 = 0M
                    });
                });
            #endregion

            #region 結果
            var tResult = tNow.Clone();
            tNow.AsEnumerable()
                .AsParallel()
                .ForAll(r =>
                {
                    var obj = r.ItemArray;
                    var x本期銷項金額 = tNow.Columns["本期銷項金額"].Ordinal;
                    var x前期總金額 = tNow.Columns["前期總金額"].Ordinal;
                    var x交易總筆數 = tNow.Columns["交易總筆數"].Ordinal;
                    var x本期稅前金額 = tNow.Columns["本期稅前金額"].Ordinal;
                    var x本期營業稅額 = tNow.Columns["本期營業稅額"].Ordinal;
                    var x本期單據總額 = tNow.Columns["本期單據總額"].Ordinal;
                    var x本期折扣金額 = tNow.Columns["本期折扣金額"].Ordinal;
                    var x本期已收預收 = tNow.Columns["本期已收預收"].Ordinal;
                    var x本期應收總額 = tNow.Columns["本期應收總額"].Ordinal;

                    var cuno = r["cuno"].ToString().Trim();
                    var 本期銷項金額 = 0M;
                    var 前期總金額 = 0M;
                    var 交易總筆數 = 0;

                    var 本期稅前金額 = 0M;
                    var 本期營業稅額 = 0M;
                    var 本期單據總額 = 0M;

                    var 本期折扣金額 = 0M;
                    var 本期已收預收 = 0M;
                    var 本期應收總額 = 0M;

                    stack.Where(o => o.cuno == cuno)
                        .Aggregate(0, (x, ow) =>
                        {
                            本期銷項金額 += ow.本期銷項金額;
                            前期總金額 += ow.前期總金額;
                            交易總筆數 += ow.交易總筆數;

                            本期稅前金額 += ow.本期稅前金額;
                            本期營業稅額 += ow.本期營業稅額;
                            本期單據總額 += ow.本期單據總額;

                            本期折扣金額 += ow.本期折扣金額;
                            本期已收預收 += ow.本期已收預收;
                            本期應收總額 += ow.本期應收總額;
                            return x;
                        });

                    obj[x本期銷項金額] = 本期銷項金額;
                    obj[x前期總金額] = 前期總金額;
                    obj[x交易總筆數] = 交易總筆數;

                    obj[x本期稅前金額] = 本期稅前金額;
                    obj[x本期營業稅額] = 本期營業稅額;
                    obj[x本期單據總額] = 本期單據總額;

                    obj[x本期折扣金額] = 本期折扣金額;
                    obj[x本期已收預收] = 本期已收預收;
                    obj[x本期應收總額] = 本期應收總額;

                    lock (tResult.Rows.SyncRoot)
                    {
                        tResult.Rows.Add(obj);
                    }
                });
            #endregion

            if (tResult.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dy = stack.GroupBy(o => o.cuno)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Sum(gw => gw.前期總金額));

            string spname = "";
            if (SpNo.TrimTextLenth() > 0)
            {  
                xe.Validate<JBS.JS.Spec>(SpNo.Text.Trim(), r => spname = r["spname"].ToString());
            }


            tNow.Clear();
            tNtemp.Clear();
            tBefore.Clear();
            tBtemp.Clear();
            tCust.Clear();
            stack.Clear();

            this.OpemInfoFrom<FrmCust_Accb>(() =>
            {
                FrmCust_Accb frm = new FrmCust_Accb();
                tResult.DefaultView.Sort = "cuno,帳款日期,sano,recordno";
                frm.tResult = tResult.DefaultView.ToTable();

                frm.dy = dy;
                frm.DateRange = Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                frm.spname = spname;
                return frm;
            });
        }

        void 明細簡要表含前期()
        {
            DataTable tNow = new DataTable();
            DataTable tNtemp = new DataTable();
            DataTable tBefore = new DataTable();
            DataTable tBtemp = new DataTable();
            DataTable tCust = new DataTable();
            ConcurrentStack<danju> stack = new ConcurrentStack<danju>();
            ConcurrentStack<Task> tks = new ConcurrentStack<Task>();

            var columns1 = @"
                帳款日期='',(CollectMny+GetPrvAcc) as 已收預收,本期銷項金額=0.0, 
                前期總金額=0.0,交易總筆數=0.0,
                本期稅前金額=0.0,本期營業稅額=0.0,本期單據總額=0.0,
                本期折扣金額=0.0,本期已收預收=0.0,本期應收總額=0.0 ";
            var columnscu = @"
               cust.cuname2,cust.cuper1,cust.cutel1,cufax1,CuAtel1,cur1,cuaddr1,cuuno,CuAdvamt,";
            //全部或非零只針對本期,前期一律抓非零
            #region T-SQL-SALE
            var tSale = @"
                select sale.adper1 as sale_adper1,sale.adtel as sale_adtel,sale.adaddr as sale_adaddr,單據='銷貨',saled.*," + columnscu + @"
                sale.emname,sale.TaxMny,sale.Tax,sale.TotMny,sale.invno,sale.Discount,sale.recordno as rdno,sale.CollectMny,sale.GetPrvAcc,sale.AcctMny,sale.Xa1Name,sale.payerno,出貨客戶=sale.cuname1,cust.cuname1," + columns1 + @"
                from saled 
                left join sale on saled.sano = sale.sano 
                left join cust on sale.payerno = cust.cuno 
                where 0=0 
                and sale.sadateAc >=@sadateAc
                and sale.sadateAc <=@sadateAc1 ";
            if (CuNo.Text.Trim().Length > 0) tSale += " and sale.payerno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) tSale += " and sale.payerno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) tSale += " and cust.cux4no =@cux4no";
            if (SpNo.Text.Trim().Length > 0) tSale += " and sale.SpNo =@SpNo";
            if (radioT6.Checked) tSale += " and sale.AcctMny !=0 ";
            #endregion

            #region T-SQL-RSALE
            var tRsale = @"
                select sale_adper1 ='' ,sale_adtel ='',sale_adaddr='',單據='銷退',rsaled.*," + columnscu + @"
                rsale.emname,rsale.TaxMny,rsale.Tax,rsale.TotMny,rsale.invno,rsale.Discount,rsale.recordno as rdno,rsale.CollectMny,rsale.GetPrvAcc,rsale.AcctMny,rsale.Xa1Name,rsale.payerno,出貨客戶=rsale.cuname1,cust.cuname1," + columns1 + @"
                from rsaled 
                left join rsale on rsaled.sano = rsale.sano 
                left join cust on  rsale.payerno = cust.cuno 
                where 0=0 
                and rsale.sadateAc >=@sadateAc
                and rsale.sadateAc <=@sadateAc1 ";
            if (CuNo.Text.Trim().Length > 0) tRsale += " and rsale.payerno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) tRsale += " and rsale.payerno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) tRsale += " and cust.cux4no =@cux4no";
            if (SpNo.Text.Trim().Length > 0) tRsale += " and rsale.SpNo =@SpNo";
            if (radioT6.Checked) tRsale += " and rsale.AcctMny !=0  ";
            #endregion

            #region T-SQL-B-SALE
            var bSale = @"
                select sale.adper1 as sale_adper1,sale.adtel as sale_adtel,sale.adaddr as sale_adaddr,單據='銷貨',saled.*," + columnscu + @"
                sale.emname,sale.TaxMny,sale.Tax,sale.TotMny,sale.Discount,sale.invno,sale.recordno as rdno,sale.CollectMny,sale.GetPrvAcc,sale.AcctMny,sale.Xa1Name,sale.payerno,出貨客戶=sale.cuname1,cust.cuname1," + columns1 + @"
                from saled 
                left join sale on saled.sano = sale.sano 
                left join cust on sale.payerno = cust.cuno 
                where 0=0 
                and sale.sadateAc < @sadateAc ";
            if (CuNo.Text.Trim().Length > 0) bSale += " and sale.payerno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) bSale += " and sale.payerno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) bSale += " and cust.cux4no =@cux4no";
            if (SpNo.Text.Trim().Length > 0) bSale += " and sale.SpNo =@SpNo";
            bSale += " and sale.AcctMny !=0 ";
            #endregion

            #region T-SQL-B-RSALE
            var bRsale = @"
                select sale_adper1 ='' ,sale_adtel ='',sale_adaddr='',單據='銷退',rsaled.*," + columnscu + @"
                rsale.emname,rsale.TaxMny,rsale.Tax,rsale.TotMny,rsale.Discount,rsale.invno,rsaled.recordno as rdno,rsale.CollectMny,rsale.GetPrvAcc,rsale.AcctMny,rsale.Xa1Name,rsale.payerno,出貨客戶=rsale.cuname1,cust.cuname1," + columns1 + @"
                from rsaled 
                left join rsale on rsaled.sano = rsale.sano 
                left join cust on rsale.payerno = cust.cuno 
                where 0=0 
                and rsale.sadateAc < @sadateAc ";
            if (CuNo.Text.Trim().Length > 0) bRsale += " and rsale.payerno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) bRsale += " and rsale.payerno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) bRsale += " and cust.cux4no =@cux4no";
            if (SpNo.Text.Trim().Length > 0) bRsale += " and rsale.SpNo =@SpNo";
            bRsale += " and rsale.AcctMny !=0 ";
            #endregion

            #region T-SQL-CUST
            var qCls = @"
                帳款日期='',已收預收=0.0,本期銷項金額=0.0, 
                前期總金額=0.0,交易總筆數=0.0,
                本期稅前金額=0.0,本期營業稅額=0.0,本期單據總額=0.0,
                本期折扣金額=0.0,本期已收預收=0.0,本期應收總額=0.0 ";

            var qStr = @"
                select c.cuno,emname,xa1name
                from Cust c
                left join empl on c.cuemno1 = empl.emno
                left join xa01 on c.cuxa1no = xa01.xa1no
                where 0=0 ";
            if (CuNo.Text.Trim().Length > 0) qStr += " and c.cuno >=@CuNo";
            if (CuNo1.Text.Trim().Length > 0) qStr += " and c.cuno <=@CuNo1";
            if (X4No.Text.Trim().Length > 0) qStr += " and c.cux4no =@cux4no";
            qStr += " and c.CuSpareRcv !=0 ";

            var bCust = @"
                Select B.emname,B.xa1name,cust.*," + qCls + @"
                from (" + qStr + @")B
                left join cust on B.cuno = cust.cuno";
            #endregion

            #region 撈資料庫
            Parallel.For(0, 5, i =>
            {
                if (i == 0)
                {
                    work1(ref tNow, tSale);
                }
                else if (i == 1)
                {
                    work2(ref tNtemp, tRsale);
                }
                else if (i == 2)
                {
                    work3(ref tBefore, bSale);
                }
                else if (i == 3)
                {
                    work4(ref tBtemp, bRsale);
                }
                else if (i == 4)
                {
                    work5(ref tCust, bCust);
                }
            });

            #region 將期初改為單據明細
            var tlen = tBefore.Columns.Count;
            var tColumns = tBefore.Clone();
            var 帳款日期 = (Common.User_DateTime == 1) ? "   /  /  " : "    /  /  ";
            tCust.AsEnumerable()
                .AsParallel()
                .ForAll(r =>
                {
                    var obj = new object[tlen];

                    var x帳款日期 = tColumns.Columns["帳款日期"].Ordinal;
                    var xSadate = tColumns.Columns["sadate"].Ordinal;
                    var xSadate1 = tColumns.Columns["sadate1"].Ordinal;
                    var xCuno = tColumns.Columns["cuno"].Ordinal;
                    var xCuname1 = tColumns.Columns["cuname1"].Ordinal;
                    var xCuname2 = tColumns.Columns["cuname2"].Ordinal;
                    var xCutel1 = tColumns.Columns["cutel1"].Ordinal;
                    var xCuper1 = tColumns.Columns["cuper1"].Ordinal;
                    var xCufax1 = tColumns.Columns["cufax1"].Ordinal;
                    var xCuaddr1 = tColumns.Columns["cuaddr1"].Ordinal;
                    var xCuUno = tColumns.Columns["CuUno"].Ordinal;
                    var xEmname = tColumns.Columns["emname"].Ordinal;
                    var xXa1name = tColumns.Columns["xa1name"].Ordinal;

                    var x單據 = tColumns.Columns["單據"].Ordinal;
                    var xSano = tColumns.Columns["sano"].Ordinal;
                    var xTaxMny = tColumns.Columns["TaxMny"].Ordinal;
                    var xTax = tColumns.Columns["Tax"].Ordinal;
                    var xTotMny = tColumns.Columns["TotMny"].Ordinal;
                    var xDiscount = tColumns.Columns["Discount"].Ordinal;
                    var xCollectMny = tColumns.Columns["CollectMny"].Ordinal;
                    var xGetPrvAcc = tColumns.Columns["GetPrvAcc"].Ordinal;
                    var xAcctMny = tColumns.Columns["AcctMny"].Ordinal;
                    var xCuAdvamt = tColumns.Columns["CuAdvamt"].Ordinal;

                    obj[x帳款日期] = 帳款日期;
                    obj[xSadate] = "   /  /  ";
                    obj[xSadate1] = "    /  /  ";
                    obj[xCuno] = r["cuno"].ToString().Trim();
                    obj[xCuname1] = r["cuname1"].ToString().Trim();
                    obj[xCuname2] = r["cuname2"].ToString().Trim();
                    obj[xCutel1] = r["cutel1"].ToString().Trim();
                    obj[xCuper1] = r["cuper1"].ToString().Trim();
                    obj[xCufax1] = r["cufax1"].ToString().Trim();
                    obj[xCuaddr1] = r["cuaddr1"].ToString().Trim();
                    obj[xEmname] = r["emname"].ToString().Trim();
                    obj[xXa1name] = r["xa1name"].ToString().Trim();
                    obj[xCuUno] = r["CuUno"].ToString().Trim();

                    obj[x單據] = "期初";
                    obj[xSano] = "上期餘額";
                    obj[xTaxMny] = 0M;
                    obj[xTax] = 0M;
                    obj[xTotMny] = r["CuSpareRcv"].ToDecimal().ToString("f" + Common.MST);
                    obj[xDiscount] = 0M;
                    obj[xCollectMny] = 0M;
                    obj[xGetPrvAcc] = 0M;
                    obj[xAcctMny] = r["CuSpareRcv"].ToDecimal().ToString("f" + Common.MST);
                    obj[xCuAdvamt] = r["CuAdvamt"].ToDecimal("f" + Common.MST);

                    lock (tBefore.Rows.SyncRoot)
                    {
                        tBefore.Rows.Add(obj);
                    }
                });
            #endregion

            tNow.Merge(tNtemp);
            tBefore.Merge(tBtemp);
            #endregion

            #region 本期
            tNow.AsEnumerable()
                .AsParallel()
                .GroupBy(r => r["cuno"].ToString().Trim())
                .ForAll(groups =>
                {
                    var 本期銷項金額 = 0M;
                    var 交易總筆數 = 0;

                    var 本期稅前金額 = 0M;
                    var 本期營業稅額 = 0M;
                    var 本期單據總額 = 0M;

                    var 本期折扣金額 = 0M;
                    var 本期已收預收 = 0M;
                    var 本期應收總額 = 0M;

                    groups.GroupBy(g => new
                    {
                        key1 = g["sano"].ToString().Trim(),
                        key2 = g["單據"].ToString()
                    })
                   .Aggregate(0, (x, sano) =>
                   {
                       本期銷項金額 += sano.Sum(sr => sr["mny"].ToDecimal("f" + Common.TPS));
                       交易總筆數++;

                       本期稅前金額 += sano.First()["TaxMny"].ToDecimal("f" + Common.MST);
                       本期營業稅額 += sano.First()["Tax"].ToDecimal("f" + Common.TS);
                       本期單據總額 += sano.First()["TotMny"].ToDecimal("f" + Common.MST);

                       本期折扣金額 += sano.First()["Discount"].ToDecimal("f" + Common.MST);
                       本期已收預收 += sano.First()["已收預收"].ToDecimal("f" + Common.MST);
                       本期應收總額 += sano.First()["AcctMny"].ToDecimal("f" + Common.MST);
                       return x;
                   });

                    stack.Push(new danju
                    {
                        cuno = groups.Key,
                        本期銷項金額 = 本期銷項金額,
                        前期總金額 = 0M,
                        交易總筆數 = 交易總筆數,

                        本期稅前金額 = 本期稅前金額,
                        本期營業稅額 = 本期營業稅額,
                        本期單據總額 = 本期單據總額,

                        本期折扣金額 = 本期折扣金額,
                        本期已收預收 = 本期已收預收,
                        本期應收總額 = 本期應收總額
                    });
                });
            #endregion

            #region 前期 + 期初
            tBefore.AsEnumerable()
                .AsParallel()
                .GroupBy(r => r["cuno"].ToString().Trim())
                .ForAll(groups =>
                {
                    var 前期總金額 = 0M;

                    groups.GroupBy(g => new
                    {
                        key1 = g["sano"].ToString().Trim(),
                        key2 = g["單據"].ToString()
                    })
                    .Aggregate(0, (x, sano) =>
                    {
                        前期總金額 += sano.First()["AcctMny"].ToDecimal("f" + Common.MST);
                        return x;
                    });

                    stack.Push(new danju
                    {
                        cuno = groups.Key,
                        本期銷項金額 = 0M,
                        前期總金額 = 前期總金額,
                        交易總筆數 = 0,

                        本期稅前金額 = 0M,
                        本期營業稅額 = 0M,
                        本期單據總額 = 0M,

                        本期折扣金額 = 0M,
                        本期已收預收 = 0M,
                        本期應收總額 = 0M
                    });
                });
            #endregion

            #region 結果
            var tResult = tNow.Clone();
            tNow.Merge(tBefore);

            tNow.AsEnumerable()
                .AsParallel()
                .ForAll(r =>
                {
                    var obj = r.ItemArray;
                    var x本期銷項金額 = tNow.Columns["本期銷項金額"].Ordinal;
                    var x前期總金額 = tNow.Columns["前期總金額"].Ordinal;
                    var x交易總筆數 = tNow.Columns["交易總筆數"].Ordinal;
                    var x本期稅前金額 = tNow.Columns["本期稅前金額"].Ordinal;
                    var x本期營業稅額 = tNow.Columns["本期營業稅額"].Ordinal;
                    var x本期單據總額 = tNow.Columns["本期單據總額"].Ordinal;
                    var x本期折扣金額 = tNow.Columns["本期折扣金額"].Ordinal;
                    var x本期已收預收 = tNow.Columns["本期已收預收"].Ordinal;
                    var x本期應收總額 = tNow.Columns["本期應收總額"].Ordinal;

                    var cuno = r["cuno"].ToString().Trim();
                    var 本期銷項金額 = 0M;
                    var 前期總金額 = 0M;
                    var 交易總筆數 = 0;

                    var 本期稅前金額 = 0M;
                    var 本期營業稅額 = 0M;
                    var 本期單據總額 = 0M;

                    var 本期折扣金額 = 0M;
                    var 本期已收預收 = 0M;
                    var 本期應收總額 = 0M;

                    stack.Where(o => o.cuno == cuno)
                        .Aggregate(0, (x, ow) =>
                        {
                            本期銷項金額 += ow.本期銷項金額;
                            前期總金額 += ow.前期總金額;
                            交易總筆數 += ow.交易總筆數;

                            本期稅前金額 += ow.本期稅前金額;
                            本期營業稅額 += ow.本期營業稅額;
                            本期單據總額 += ow.本期單據總額;

                            本期折扣金額 += ow.本期折扣金額;
                            本期已收預收 += ow.本期已收預收;
                            本期應收總額 += ow.本期應收總額;
                            return x;
                        });

                    obj[x本期銷項金額] = 本期銷項金額;
                    obj[x前期總金額] = 前期總金額;
                    obj[x交易總筆數] = 交易總筆數;

                    obj[x本期稅前金額] = 本期稅前金額;
                    obj[x本期營業稅額] = 本期營業稅額;
                    obj[x本期單據總額] = 本期單據總額;

                    obj[x本期折扣金額] = 本期折扣金額;
                    obj[x本期已收預收] = 本期已收預收;
                    obj[x本期應收總額] = 本期應收總額;

                    lock (tResult.Rows.SyncRoot)
                    {
                        tResult.Rows.Add(obj); 
                    }
                });
            #endregion

            if (tResult.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dy = stack.GroupBy(o => o.cuno)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Sum(gw => gw.前期總金額));

            string spname = "";
            if (SpNo.TrimTextLenth() > 0)
            {
                xe.Validate<JBS.JS.Spec>(SpNo.Text.Trim(), r => spname = r["spname"].ToString());
            }

            tNow.Clear();
            tNtemp.Clear();
            tBefore.Clear();
            tBtemp.Clear();
            tCust.Clear();
            stack.Clear();

            this.OpemInfoFrom<FrmCust_Accb>(() =>
                            {
                                FrmCust_Accb frm = new FrmCust_Accb();
                                tResult.DefaultView.Sort = "cuno,帳款日期,sano,recordno";
                                frm.tResult = tResult.DefaultView.ToTable();

                                frm.dy = dy;
                                frm.DateRange = Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                frm.spname = spname;
                                return frm;
                            });
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
            if (CuNo.Text.BigThen(CuNo1.Text))
            {
                MessageBox.Show("起始業務編號大於終止業務編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuNo.Focus();
                CuNo.SelectAll();
                return;
            }

            //抓專案名稱
            string spname = "";
            if (SpNo.TrimTextLenth() > 0)
            {
                xe.Validate<JBS.JS.Spec>(SpNo.Text.Trim(), r => spname = r["spname"].ToString());
            }


            if (radioT2.Checked)
            {
                本幣總額表();
                return;
            }

            if (radioT1.Checked)
            {
                if (radioT4.Checked == false)
                {
                    明細簡要表();
                    return;
                    
                }
                else
                {
                    明細簡要表含前期();
                    return;
                }
            }

            DataTable tTitle = new DataTable();
            DataTable tMaster = new DataTable();
            DataTable tBefore = new DataTable();
            DataTable tTemp = new DataTable();

            try
            {
                string columns = " 前期總金額=0.0, "
                               + " 交易總筆數=0.0,稅前總金額=0.0,營業稅總額=0.0,應收總金額=0.0, "
                               + " 折扣總金額=0.0,已收加預收=0.0,本期總金額=0.0,前期加本期=0.0  ";

                string columns1 = " 帳款日期='',(CollectMny+GetPrvAcc) as 已收預收,本期銷項金額=0.0, "
                                + " 前期總金額=0.0,"
                                + " 交易總筆數=0.0,本期稅前金額=0.0,本期營業稅額=0.0,"
                                + " 本期折扣金額=0.0,本期已收預收=0.0,本期應收總額=0.0 ";


                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    //表頭
                    string sql = " select *," + columns + " from sale where 1=0 ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(tTitle);
                        tTitle.Clear();
                    }

                    #region //本期銷貨(總)
                    sql = " select 單據='銷貨',saled.*,cust.cuname1,cust.cuname2,cust.cuno,cust.cuper1,cust.cutel1,cust.cufax1,cust.CuAtel1,cust.cur1,cust.cuaddr1,cust.cuuno,cust.CuAdvamt,"
                        + " sale.emname,sale.TaxMny,sale.Tax,sale.TotMny,sale.invno,sale.Discount,sale.recordno as rdno,sale.CollectMny,sale.GetPrvAcc,sale.AcctMny,sale.Xa1Name,sale.payerno," + columns1
                        + " from saled "
                        + " left join sale on saled.sano = sale.sano "
                        + " left join cust on sale.payerno = cust.cuno "
                        + " where 0=0 "
                        + " and sale.sadateAc >=@sadateAc"
                        + " and sale.sadateAc <=@sadateAc1";
                    if (CuNo.Text.Trim().Length > 0) sql += " and cust.CuNo >=@CuNo";
                    if (CuNo1.Text.Trim().Length > 0) sql += " and cust.CuNo <=@CuNo1";
                    if (X4No.Text.Trim().Length > 0) sql += " and cust.cux4no =@cux4no";
                    if (SpNo.Text.Trim().Length > 0) sql += " and sale.SpNo =@SpNo";
                    if (radioT6.Checked) sql += " and sale.AcctMny !=0 ";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("sadateAc", Date.ToTWDate(SaDate.Text));
                        cmd.Parameters.AddWithValue("sadateAc1", Date.ToTWDate(SaDate1.Text));
                        if (CuNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                        if (CuNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                        if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());
                        if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tMaster);

                            for (int i = 0; i < tMaster.Rows.Count; i++)//請款客戶
                            {
                                if (tMaster.Rows[i]["payerno"].ToString().Trim() != "")
                                {
                                    tMaster.Rows[i]["cuno"] = tMaster.Rows[i]["payerno"].ToString();
                                    pVar.CuPareValidate(tMaster.Rows[i]["payerno"].ToString(), payerno, payername);
                                    tMaster.Rows[i]["cuname1"] = payername.Text.ToString();
                                }
                            }

                            foreach (var g in tMaster.AsEnumerable().GroupBy(r => r["sano"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["cuno"].ToString().Trim() == row["cuno"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["cuno"] = row["cuno"].ToString().Trim();
                                    rw["cuname1"] = row["cuname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();
                                    rw["前期總金額"] = 0;
                                    rw["交易總筆數"] = 1;
                                    rw["稅前總金額"] = row["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                                    rw["營業稅總額"] = row["Tax"].ToDecimal().ToString("f" + Common.TS);
                                    rw["應收總金額"] = row["TotMny"].ToDecimal().ToString("f" + Common.MST);
                                    rw["折扣總金額"] = row["Discount"].ToDecimal().ToString("f" + Common.MST);
                                    rw["已收加預收"] = (row["CollectMny"].ToDecimal() + row["GetPrvAcc"].ToDecimal()).ToString("f" + Common.MST);
                                    rw["本期總金額"] = row["AcctMny"].ToDecimal().ToString("f" + Common.MST);
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["交易總筆數"] = (int)p["交易總筆數"].ToDecimal() + 1;
                                    p["稅前總金額"] = (p["稅前總金額"].ToDecimal() + row["TaxMny"].ToDecimal()).ToString("f" + Common.MST);
                                    p["營業稅總額"] = (p["營業稅總額"].ToDecimal() + row["Tax"].ToDecimal()).ToString("f" + Common.TS);
                                    p["應收總金額"] = (p["應收總金額"].ToDecimal() + row["TotMny"].ToDecimal()).ToString("f" + Common.MST);

                                    p["折扣總金額"] = (p["折扣總金額"].ToDecimal() + row["Discount"].ToDecimal()).ToString("f" + Common.MST);
                                    p["已收加預收"] = (p["已收加預收"].ToDecimal() + (row["CollectMny"].ToDecimal() + row["GetPrvAcc"].ToDecimal())).ToString("f" + Common.MST);
                                    p["本期總金額"] = (p["本期總金額"].ToDecimal() + row["AcctMny"].ToDecimal()).ToString("f" + Common.MST);
                                }
                            }
                        }
                    }
                    #endregion

                    #region //本期銷退(總)
                    sql = " select 單據='銷退',rsaled.*,cust.cuname1,cust.cuname2, cust.cuno,cust.cuper1,cust.cutel1,cust.cufax1,cust.CuAtel1,cust.cur1,cust.cuaddr1,cust.cuuno,cust.CuAdvamt,"
                        + " rsale.emname,rsale.TaxMny,rsale.Tax,rsale.TotMny,rsale.invno,rsale.Discount,rsale.recordno as rdno,rsale.CollectMny,rsale.GetPrvAcc,rsale.AcctMny,rsale.Xa1Name,rsale.payerno," + columns1
                        + " from rsaled "
                        + " left join rsale on rsaled.sano = rsale.sano "
                        + " left join cust on rsale.payerno = cust.cuno "
                        + " where 0=0 "
                        + " and rsale.sadateAc >=@sadateAc"
                        + " and rsale.sadateAc <=@sadateAc1";
                    if (CuNo.Text.Trim().Length > 0) sql += " and cust.CuNo >=@CuNo";
                    if (CuNo1.Text.Trim().Length > 0) sql += " and cust.CuNo <=@CuNo1";
                    if (X4No.Text.Trim().Length > 0) sql += " and cust.cux4no =@cux4no";
                    if (SpNo.Text.Trim().Length > 0) sql += " and rsale.SpNo =@SpNo";
                    if (radioT6.Checked) sql += " and rsale.AcctMny !=0 ";

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("sadateAc", Date.ToTWDate(SaDate.Text));
                        cmd.Parameters.AddWithValue("sadateAc1", Date.ToTWDate(SaDate1.Text));
                        if (CuNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                        if (CuNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                        if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());
                        if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            tTemp = new DataTable();
                            da.Fill(tTemp);
                            //銷退金額變負數
                            for (int i = 0; i < tTemp.Rows.Count; i++)
                            {
                                tTemp.Rows[i]["price"] = -1 * tTemp.Rows[i]["price"].ToDecimal();
                                tTemp.Rows[i]["Mny"] = -1 * tTemp.Rows[i]["Mny"].ToDecimal();
                                tTemp.Rows[i]["TaxMny"] = -1 * tTemp.Rows[i]["TaxMny"].ToDecimal();
                                tTemp.Rows[i]["Tax"] = -1 * tTemp.Rows[i]["Tax"].ToDecimal();
                                tTemp.Rows[i]["TotMny"] = -1 * tTemp.Rows[i]["TotMny"].ToDecimal();
                                tTemp.Rows[i]["CollectMny"] = -1 * tTemp.Rows[i]["CollectMny"].ToDecimal();
                                tTemp.Rows[i]["GetPrvAcc"] = -1 * tTemp.Rows[i]["GetPrvAcc"].ToDecimal();
                                tTemp.Rows[i]["Discount"] = -1 * tTemp.Rows[i]["Discount"].ToDecimal();
                                tTemp.Rows[i]["AcctMny"] = -1 * tTemp.Rows[i]["AcctMny"].ToDecimal();
                                //請款客戶
                                if (tTemp.Rows[i]["payerno"].ToString().Trim() != "")
                                {
                                    tTemp.Rows[i]["cuno"] = tTemp.Rows[i]["payerno"].ToString();
                                    pVar.CuPareValidate(tTemp.Rows[i]["payerno"].ToString(), payerno, payername);//pVar會回傳值給textbox
                                    tTemp.Rows[i]["cuname1"] = payername.Text.ToString();
                                }

                            }

                            foreach (var g in tTemp.AsEnumerable().GroupBy(r => r["sano"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["cuno"].ToString().Trim() == row["cuno"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["cuno"] = row["cuno"].ToString().Trim();
                                    rw["cuname1"] = row["cuname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();
                                    rw["前期總金額"] = 0;
                                    rw["交易總筆數"] = 1;
                                    rw["稅前總金額"] = row["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                                    rw["營業稅總額"] = row["Tax"].ToDecimal().ToString("f" + Common.TS);
                                    rw["應收總金額"] = row["TotMny"].ToDecimal().ToString("f" + Common.MST);
                                    rw["折扣總金額"] = row["Discount"].ToDecimal().ToString("f" + Common.MST);
                                    rw["已收加預收"] = (row["CollectMny"].ToDecimal() + row["GetPrvAcc"].ToDecimal()).ToString("f" + Common.MST);
                                    rw["本期總金額"] = row["AcctMny"].ToDecimal().ToString("f" + Common.MST);
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["交易總筆數"] = (int)p["交易總筆數"].ToDecimal() + 1;
                                    p["稅前總金額"] = (p["稅前總金額"].ToDecimal() + row["TaxMny"].ToDecimal()).ToString("f" + Common.MST);
                                    p["營業稅總額"] = (p["營業稅總額"].ToDecimal() + row["Tax"].ToDecimal()).ToString("f" + Common.TS);
                                    p["應收總金額"] = (p["應收總金額"].ToDecimal() + row["TotMny"].ToDecimal()).ToString("f" + Common.MST);

                                    p["折扣總金額"] = (p["折扣總金額"].ToDecimal() + row["Discount"].ToDecimal()).ToString("f" + Common.MST);
                                    p["已收加預收"] = (p["已收加預收"].ToDecimal() + (row["CollectMny"].ToDecimal() + row["GetPrvAcc"].ToDecimal())).ToString("f" + Common.MST);
                                    p["本期總金額"] = (p["本期總金額"].ToDecimal() + row["AcctMny"].ToDecimal()).ToString("f" + Common.MST);
                                }
                            }
                        }
                    }
                    tMaster.Merge(tTemp);
                    tMaster.AcceptChanges();
                    #endregion

                    #region //前期銷貨(未收總額)
                    sql = " select 單據='銷貨',saled.*,cust.cuname1,cust.cuname2, cust.cuno,cust.cuper1,cust.cutel1,cust.cufax1,cust.CuAtel1,cust.cur1,cust.cuaddr1,cust.cuuno,cust.CuAdvamt,"
                        + " sale.emname,sale.TaxMny,sale.Tax,sale.TotMny,sale.Discount,sale.invno,sale.recordno as rdno,sale.CollectMny,sale.GetPrvAcc,sale.AcctMny,sale.Xa1Name,sale.payerno," + columns1
                        + " from saled "
                        + " left join sale on saled.sano = sale.sano "
                        + " left join cust on sale.payerno = cust.cuno "
                        + " where 0=0 "
                        + " and sale.sadateAc <@sadateAc";

                    if (CuNo.Text.Trim().Length > 0) sql += " and cust.CuNo >=@CuNo";
                    if (CuNo1.Text.Trim().Length > 0) sql += " and cust.CuNo <=@CuNo1";
                    if (X4No.Text.Trim().Length > 0) sql += " and cust.cux4no =@cux4no";
                    if (SpNo.Text.Trim().Length > 0) sql += " and sale.SpNo =@SpNo";
                    if (radioT6.Checked) sql += " and sale.AcctMny !=0 ";

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("sadateAc", Date.ToTWDate(SaDate.Text));
                        if (CuNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                        if (CuNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                        if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());
                        if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tBefore);
                            for (int i = 0; i < tBefore.Rows.Count; i++)
                            {
                                //請款客戶
                                if (tBefore.Rows[i]["payerno"].ToString().Trim() != "")
                                {
                                    tBefore.Rows[i]["cuno"] = tBefore.Rows[i]["payerno"].ToString();
                                    pVar.CuPareValidate(tBefore.Rows[i]["payerno"].ToString(), payerno, payername);
                                    tBefore.Rows[i]["cuname1"] = payername.Text.ToString();
                                }
                            }

                            foreach (var g in tBefore.AsEnumerable().GroupBy(r => r["sano"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["cuno"].ToString().Trim() == row["cuno"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["cuno"] = row["cuno"].ToString().Trim();
                                    rw["cuname1"] = row["cuname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();

                                    rw["交易總筆數"] = 0;
                                    rw["前期總金額"] = row["AcctMny"].ToDecimal().ToString("f" + Common.MST);

                                    rw["稅前總金額"] = 0;
                                    rw["營業稅總額"] = 0;
                                    rw["應收總金額"] = 0;
                                    rw["折扣總金額"] = 0;
                                    rw["已收加預收"] = 0;
                                    rw["本期總金額"] = 0;
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["AcctMny"].ToDecimal()).ToString("f" + Common.MST);
                                }
                            }
                        }
                    }
                    #endregion

                    #region //前期銷退(未收總額)
                    sql = " select 單據='銷退',rsaled.*,cust.cuname1,cust.cuname2, cust.cuno,cust.cuper1,cust.cutel1,cust.cufax1,cust.CuAtel1,cust.cur1,cust.cuaddr1,cust.cuuno,cust.CuAdvamt,"
                        + " rsale.emname,rsale.TaxMny,rsale.Tax,rsale.TotMny,rsale.Discount,rsale.invno,rsaled.recordno as rdno,rsale.CollectMny,rsale.GetPrvAcc,rsale.AcctMny,rsale.Xa1Name," + columns1
                        + " from rsaled "
                        + " left join rsale on rsaled.sano = rsale.sano "
                        + " left join cust on rsaled.cuno = cust.cuno "
                        + " where 0=0 "
                        + " and rsale.sadateAc <@sadateAc";

                    if (CuNo.Text.Trim().Length > 0) sql += " and cust.CuNo >=@CuNo";
                    if (CuNo1.Text.Trim().Length > 0) sql += " and cust.CuNo <=@CuNo1";
                    if (X4No.Text.Trim().Length > 0) sql += " and cust.cux4no =@cux4no";
                    if (SpNo.Text.Trim().Length > 0) sql += " and rsale.SpNo =@SpNo";
                    if (radioT6.Checked) sql += " and rsale.AcctMny !=0 ";

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("sadateAc", Date.ToTWDate(SaDate.Text));
                        if (CuNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                        if (CuNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                        if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());
                        if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            tTemp = new DataTable();
                            da.Fill(tTemp);
                            //銷退金額變負數
                            for (int i = 0; i < tTemp.Rows.Count; i++)
                            {
                                tTemp.Rows[i]["price"] = -1 * tTemp.Rows[i]["price"].ToDecimal();
                                tTemp.Rows[i]["Mny"] = -1 * tTemp.Rows[i]["Mny"].ToDecimal();
                                tTemp.Rows[i]["TaxMny"] = -1 * tTemp.Rows[i]["TaxMny"].ToDecimal();
                                tTemp.Rows[i]["Tax"] = -1 * tTemp.Rows[i]["Tax"].ToDecimal();
                                tTemp.Rows[i]["TotMny"] = -1 * tTemp.Rows[i]["TotMny"].ToDecimal();
                                tTemp.Rows[i]["CollectMny"] = -1 * tTemp.Rows[i]["CollectMny"].ToDecimal();
                                tTemp.Rows[i]["GetPrvAcc"] = -1 * tTemp.Rows[i]["GetPrvAcc"].ToDecimal();
                                tTemp.Rows[i]["Discount"] = -1 * tTemp.Rows[i]["Discount"].ToDecimal();
                                tTemp.Rows[i]["AcctMny"] = -1 * tTemp.Rows[i]["AcctMny"].ToDecimal();
                            }
                            foreach (var g in tTemp.AsEnumerable().GroupBy(r => r["sano"].ToString().Trim()))
                            {
                                var row = g.FirstOrDefault();
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["cuno"].ToString().Trim() == row["cuno"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["cuno"] = row["cuno"].ToString().Trim();
                                    rw["cuname1"] = row["cuname1"].ToString().Trim();
                                    rw["Xa1Name"] = row["Xa1Name"].ToString().Trim();

                                    rw["交易總筆數"] = 0;
                                    rw["前期總金額"] = row["AcctMny"].ToDecimal().ToString("f" + Common.MST);

                                    rw["稅前總金額"] = 0;
                                    rw["營業稅總額"] = 0;
                                    rw["應收總金額"] = 0;
                                    rw["折扣總金額"] = 0;
                                    rw["已收加預收"] = 0;
                                    rw["本期總金額"] = 0;
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["AcctMny"].ToDecimal()).ToString("f" + Common.MST);
                                }
                            }
                        }
                    }
                    tBefore.Merge(tTemp);
                    tBefore.AcceptChanges();
                    #endregion

                    #region //期初
                    sql = " select " + columns + ",Cust.*,Xa1Name,emname from Cust "
                        + " left join Xa01 on Cust.CuXa1No = Xa01.Xa1No "
                        + " left join empl on cust.cuemno1 = empl.emno "
                        + " where 0=0 ";

                    if (CuNo.Text.Trim().Length > 0) sql += " and cust.CuNo >=@CuNo";
                    if (CuNo1.Text.Trim().Length > 0) sql += " and cust.CuNo <=@CuNo1";
                    if (X4No.Text.Trim().Length > 0) sql += " and cust.cux4no =@cux4no";
                    if (radioT6.Checked) sql += " and cust.CuSpareRcv !=0 ";

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        if (CuNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                        if (CuNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                        if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());

                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            tTemp = new DataTable();
                            da.Fill(tTemp);
                            foreach (DataRow row in tTemp.Rows)
                            {
                                var p = tTitle.AsEnumerable().ToList().Find(r => r["cuno"].ToString().Trim() == row["cuno"].ToString().Trim());
                                if (p == null)
                                {
                                    DataRow rw = tTitle.NewRow();
                                    rw["cuno"] = row["cuno"].ToString().Trim();
                                    rw["cuname1"] = row["cuname1"].ToString().Trim();


                                    rw["交易總筆數"] = 0;
                                    rw["前期總金額"] = row["CuSpareRcv"].ToDecimal().ToString("f" + Common.MST);

                                    rw["稅前總金額"] = 0;
                                    rw["營業稅總額"] = 0;
                                    rw["應收總金額"] = 0;
                                    rw["折扣總金額"] = 0;
                                    rw["已收加預收"] = 0;
                                    rw["本期總金額"] = 0;
                                    rw["前期加本期"] = 0;

                                    tTitle.Rows.Add(rw);
                                    tTitle.AcceptChanges();
                                }
                                else
                                {
                                    p["前期總金額"] = (p["前期總金額"].ToDecimal() + row["CuSpareRcv"].ToDecimal()).ToString("f" + Common.MST);
                                }
                            }
                        }
                    }
                    #endregion

                    if (radioT4.Checked)
                    {
                        //本期銷項金額
                        DataTable tnow = tMaster.Copy();
                        var day = (Common.User_DateTime == 1) ? "sadateac" : "sadateac1";
                        for (int i = 0; i < tMaster.Rows.Count; i++)
                        {
                            tMaster.Rows[i]["帳款日期"] = Date.AddLine(tMaster.Rows[i][day].ToString().Trim());

                            var cuno1 = tMaster.Rows[i]["CuNo"].ToString();
                            tMaster.Rows[i]["本期銷項金額"] = tnow.AsEnumerable().Where(r => r["CuNo"].ToString().Trim() == cuno1).Sum(r => r["mny"].ToDecimal("f" + Common.TPS));
                        }

                        for (int i = 0; i < tTemp.Rows.Count; i++)
                        {
                            DataRow row = tMaster.NewRow();
                            row["sadate"] = "   /  /  ";
                            row["sadate1"] = "    /  /  ";
                            row["cuno"] = tTemp.Rows[i]["cuno"].ToString().Trim();
                            row["cuname1"] = tTemp.Rows[i]["cuname1"].ToString().Trim();
                            row["cuname2"] = tTemp.Rows[i]["cuname2"].ToString().Trim();
                            row["cutel1"] = tTemp.Rows[i]["cutel1"].ToString().Trim();
                            row["cuper1"] = tTemp.Rows[i]["cuper1"].ToString().Trim();
                            row["cufax1"] = tTemp.Rows[i]["cufax1"].ToString().Trim();
                            row["cuaddr1"] = tTemp.Rows[i]["cuaddr1"].ToString().Trim();
                            row["emname"] = tTemp.Rows[i]["emname"].ToString().Trim();
                            row["xa1name"] = tTemp.Rows[i]["xa1name"].ToString().Trim();
                            row["CuUno"] = tTemp.Rows[i]["CuUno"].ToString().Trim();

                            row["單據"] = "期初";
                            row["sano"] = "上期餘額";
                            row["TaxMny"] = "0".ToDecimal().ToString("f" + Common.MST);
                            row["Tax"] = "0".ToDecimal().ToString("f" + Common.TS);
                            row["TotMny"] = tTemp.Rows[i]["CuSpareRcv"].ToDecimal().ToString("f" + Common.MST);
                            row["Discount"] = "0".ToDecimal().ToString("f" + Common.MST);
                            row["CollectMny"] = "0".ToDecimal().ToString("f" + Common.MST);
                            row["GetPrvAcc"] = "0".ToDecimal().ToString("f" + Common.MST);
                            row["AcctMny"] = tTemp.Rows[i]["CuSpareRcv"].ToDecimal().ToString("f" + Common.MST);
                            row["CuAdvamt"] = tTemp.Rows[i]["CuAdvamt"].ToDecimal("f" + Common.MST);
                            tMaster.Rows.Add(row);
                            tMaster.AcceptChanges();
                        }
                        tMaster.Merge(tBefore);
                    }
                    else
                    {
                        //本期銷項金額
                        DataTable tnow = tMaster.Copy();
                        var day = (Common.User_DateTime == 1) ? "sadateac" : "sadateac1";
                        for (int i = 0; i < tMaster.Rows.Count; i++)
                        {
                            tMaster.Rows[i]["帳款日期"] = Date.AddLine(tMaster.Rows[i][day].ToString().Trim());

                            var cuno1 = tMaster.Rows[i]["CuNo"].ToString();
                            tMaster.Rows[i]["本期銷項金額"] = tnow.AsEnumerable().Where(r => r["CuNo"].ToString().Trim() == cuno1).Sum(r => r["mny"].ToDecimal("f" + Common.TPS));
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
                            row["前期加本期"] = (row["前期總金額"].ToDecimal() + row["本期總金額"].ToDecimal()).ToString("f" + Common.MST);
                        }
                        if (radioT1.Checked)
                        {
                            this.OpemInfoFrom<FrmCust_Accb>(() =>
                            {
                                FrmCust_Accb frm = new FrmCust_Accb();
                                //frm.tTitle = tTitle.Copy();
                                frm.tResult = tMaster.Copy();
                                frm.DateRange = Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                frm.spname = spname;
                                return frm;
                            });
                        }
                        else if (radioT3.Checked)
                        {
                            this.OpemInfoFrom<FrmCust_AccBrowx>(() =>
                            {
                                FrmCust_AccBrowx frm = new FrmCust_AccBrowx();
                                frm.dt = tTitle.Copy();
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

        void 本幣總額表()
        {
            var spname = "";

            DataTable Now = new DataTable();
            DataTable temp = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                if (cn.State != ConnectionState.Open) cn.Open();
                if (SpNo.TrimTextLenth() > 0)
                {
                    cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                    cmd.CommandText = "Select ISNULL(spname,'') spname from spec where spno=(@spno)";
                    var pn = cmd.ExecuteScalar();
                    if (pn != null) spname = pn.ToString();
                }

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("sadateAc", Date.ToTWDate(SaDate.Text));
                cmd.Parameters.AddWithValue("sadateAc1", Date.ToTWDate(SaDate1.Text));
                if (CuNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                if (CuNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                if (X4No.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("cux4no", X4No.Text.Trim());
                if (SpNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("SpNo", SpNo.Text.Trim());

                var sqlCust = "";
                if (CuNo.Text.Trim().Length > 0) sqlCust += " and cust.cuno >=@CuNo";
                if (CuNo1.Text.Trim().Length > 0) sqlCust += " and cust.cuno <=@CuNo1";
                if (X4No.Text.Trim().Length > 0) sqlCust += " and cust.cux4no =@cux4no";

                var sqlSale = "";
                sqlSale += " and sale.sadateAc < @sadateAc";
                if (CuNo.Text.Trim().Length > 0) sqlSale += " and sale.payerno >=@CuNo";
                if (CuNo1.Text.Trim().Length > 0) sqlSale += " and sale.payerno <=@CuNo1";
                if (SpNo.Text.Trim().Length > 0) sqlSale += " and sale.SpNo =@SpNo";
                if (radioT6.Checked) sqlSale += " and sale.AcctMny !=0 ";

                var sqlRSale = "";
                sqlRSale += " and rsale.sadateAc < @sadateAc";
                if (CuNo.Text.Trim().Length > 0) sqlRSale += " and rsale.payerno >=@CuNo";
                if (CuNo1.Text.Trim().Length > 0) sqlRSale += " and rsale.payerno <=@CuNo1";
                if (SpNo.Text.Trim().Length > 0) sqlRSale += " and rsale.SpNo =@SpNo";
                if (radioT6.Checked) sqlRSale += " and rsale.AcctMny !=0 ";

                cmd.CommandText = new MyClass().GetCommomText();
                cmd.CommandText += " and 1=0";
                da.Fill(Now);

                cmd.CommandText = ""
                + " select 0 AS 交易筆數,前期金額,'前期' AS 單據, 0.0 AS 前期加本期, 0.0 AS 已收加預收,cust.* from "
                + " ("
                + "     select cuno,SUM(前期金額)前期金額"
                + "     from "
                + "     ("
                + " 	    select cuno,ROUND((cusparercv * CuFirRcvPar)," + Common.MST + ") 前期金額 from cust where cusparercv != 0 " + sqlCust
                + " 	    union all"
                + " 	    select cuno,SUM(ROUND((acctmny * xa1par)," + Common.MST + "))前期金額 from sale where 0=0 " + sqlSale + " group by cuno"
                + "     )A group by cuno"
                + " )B"
                + " left join cust on B.cuno = cust.cuno";
                da.Fill(Now);

                cmd.CommandText = ""
                + " select 0 AS 交易筆數,前期金額,'前期' AS 單據, 0.0 AS 前期加本期, 0.0 AS 已收加預收,cust.* from "
                + " ("
                + "     select cuno,SUM(前期金額)前期金額"
                + "     from "
                + "     ("
                //+ " 	    select cuno,0 前期金額 from cust where cusparercv != 0 " + sqlCust
                //+ " 	    union all"
                + " 	    select cuno,((-1)*SUM(ROUND((acctmny * xa1par)," + Common.MST + ")))前期金額 from rsale where 0=0 " + sqlRSale + " group by cuno"
                + "     )A group by cuno"
                + " )B"
                + " left join cust on B.cuno = cust.cuno";
                da.Fill(Now);

                cmd.CommandText = new MyClass().GetCommomText();
                cmd.CommandText += " and sale.sadateAc >=@sadateAc";
                cmd.CommandText += " and sale.sadateAc <=@sadateAc1";
                if (CuNo.Text.Trim().Length > 0) cmd.CommandText += " and sale.payerno >=@CuNo";
                if (CuNo1.Text.Trim().Length > 0) cmd.CommandText += " and sale.payerno <=@CuNo1";
                if (X4No.Text.Trim().Length > 0) cmd.CommandText += " and cust.cux4no =@cux4no";
                if (SpNo.Text.Trim().Length > 0) cmd.CommandText += " and sale.SpNo =@SpNo";
                if (radioT6.Checked) cmd.CommandText += " and sale.AcctMny !=0 ";
                da.Fill(Now);

                cmd.CommandText = new MyClass1().GetCommomText();
                cmd.CommandText += " and rsale.sadateAc >=@sadateAc";
                cmd.CommandText += " and rsale.sadateAc <=@sadateAc1";
                if (CuNo.Text.Trim().Length > 0) cmd.CommandText += " and rsale.payerno >=@CuNo";
                if (CuNo1.Text.Trim().Length > 0) cmd.CommandText += " and rsale.payerno <=@CuNo1";
                if (X4No.Text.Trim().Length > 0) cmd.CommandText += " and cust.cux4no =@cux4no";
                if (SpNo.Text.Trim().Length > 0) cmd.CommandText += " and rsale.SpNo =@SpNo";
                if (radioT6.Checked) cmd.CommandText += " and rsale.AcctMny !=0 ";
                da.Fill(Now);

                temp = Now.Clone();
                //請款客戶
                for (int i = 0; i < Now.Rows.Count; i++)
                {
                    if (Now.Rows[i]["payerno"].ToString().Trim() != "")
                    {
                        Now.Rows[i]["cuno"] = Now.Rows[i]["payerno"].ToString();
                        pVar.CuPareValidate(Now.Rows[i]["payerno"].ToString(), payerno, payername);
                        Now.Rows[i]["cuname1"] = payername.Text.ToString();
                    }
                    if (Now.Rows[i]["xa1par"].ToString().Trim().Length > 0)
                    {
                        Now.Rows[i]["acctmny"] = Now.Rows[i]["acctmny"].ToDecimal() * Now.Rows[i]["xa1par"].ToDecimal();
                        Now.Rows[i]["collectmny"] = Now.Rows[i]["collectmny"].ToDecimal() * Now.Rows[i]["xa1par"].ToDecimal();
                        Now.Rows[i]["getprvacc"] = Now.Rows[i]["getprvacc"].ToDecimal() * Now.Rows[i]["xa1par"].ToDecimal();

                        Now.Rows[i]["discount"] = Now.Rows[i]["discount"].ToDecimal() * Now.Rows[i]["xa1par"].ToDecimal();
                        Now.Rows[i]["cashmny"] = Now.Rows[i]["cashmny"].ToDecimal() * Now.Rows[i]["xa1par"].ToDecimal();
                        Now.Rows[i]["cardmny"] = Now.Rows[i]["cardmny"].ToDecimal() * Now.Rows[i]["xa1par"].ToDecimal();
                        Now.Rows[i]["ticket"] = Now.Rows[i]["ticket"].ToDecimal() * Now.Rows[i]["xa1par"].ToDecimal();
                    }
                }

                var list = Now.AsEnumerable().GroupBy(r => r["cuno"].ToString().Trim()).Select(g => g.Key).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    var rows = Now.AsEnumerable().Where(r => r["cuno"].ToString() == list[i] && r["單據"].ToString() != "前期");
                    if (rows.Count() == 0) //只有前期沒有本期
                    {
                        rows = Now.AsEnumerable().Where(r => r["cuno"].ToString() == list[i]);
                        temp.ImportRow(rows.First());
                        var row = temp.AsEnumerable().FirstOrDefault(r => r["cuno"].ToString().Trim() == list[i]);

                        row["tax"] = 0;
                        row["taxb"] = 0;
                        row["taxmny"] = 0;
                        row["taxmnyb"] = 0;
                        row["totmny"] = 0;
                        row["totmnyb"] = 0;
                        row["discount"] = 0;
                        row["cashmny"] = 0;
                        row["cardmny"] = 0;
                        row["ticket"] = 0;
                        row["collectmny"] = 0;
                        row["getprvacc"] = 0;
                        row["acctmny"] = 0;
                        row["已收加預收"] = 0;
                        var 前期金額 = rows.Sum(r => r["前期金額"].ToDecimal("f" + Common.MST));
                        row["前期金額"] = 前期金額;
                        row["前期加本期"] = 前期金額;
                    }
                    else
                    {
                        temp.ImportRow(rows.First());
                        var row = temp.AsEnumerable().FirstOrDefault(r => r["cuno"].ToString().Trim() == list[i]);

                        row["tax"] = rows.Sum(r => r["tax"].ToDecimal("f" + Common.TS));
                        row["taxb"] = rows.Sum(r => r["taxb"].ToDecimal("f" + Common.TS));
                        row["taxmny"] = rows.Sum(r => r["taxmny"].ToDecimal("f" + Common.MST));
                        row["taxmnyb"] = rows.Sum(r => r["taxmnyb"].ToDecimal("f" + Common.MST));
                        row["totmny"] = rows.Sum(r => r["totmny"].ToDecimal("f" + Common.MST));
                        row["totmnyb"] = rows.Sum(r => r["totmnyb"].ToDecimal("f" + Common.MST));
                        row["discount"] = rows.Sum(r => r["discount"].ToDecimal("f" + Common.MST));
                        row["cashmny"] = rows.Sum(r => r["cashmny"].ToDecimal("f" + Common.MST));
                        row["cardmny"] = rows.Sum(r => r["cardmny"].ToDecimal("f" + Common.MST));
                        row["ticket"] = rows.Sum(r => r["ticket"].ToDecimal("f" + Common.MST));

                        var 已收 = rows.Sum(r => r["collectmny"].ToDecimal("f" + Common.MST));
                        row["collectmny"] = 已收;

                        var 預收 = rows.Sum(r => r["getprvacc"].ToDecimal("f" + Common.MST));
                        row["getprvacc"] = 預收;

                        var 本期金額 = rows.Sum(r => r["acctmny"].ToDecimal("f" + Common.MST));
                        row["acctmny"] = 本期金額;

                        var 前期金額 = Now.AsEnumerable().Where(t => t["cuno"].ToString() == list[i] && t["單據"].ToString() == "前期").Sum(t => t["前期金額"].ToDecimal("f" + Common.MST));
                        row["前期金額"] = 前期金額;

                        row["前期加本期"] = 前期金額 + 本期金額;
                        row["已收加預收"] = 已收 + 預收;
                        row["交易筆數"] = rows.Count();

                    }
                }
            }
            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.OpemInfoFrom<FrmCust_AccBrow>(() =>
                            {
                                FrmCust_AccBrow frm = new FrmCust_AccBrow();
                                temp.DefaultView.Sort = "CuNo";
                                frm.dt = temp.DefaultView.ToTable();
                                frm.DateRange = "帳款區間：" + Date.AddLine(SaDate.Text.Trim()) + " ～ " + Date.AddLine(SaDate1.Text.Trim());
                                frm.spname = spname;
                                return frm;
                            });
        }
    }
}
