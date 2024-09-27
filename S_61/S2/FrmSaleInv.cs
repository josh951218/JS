using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmSaleInv : Formbase
    {
        JBS.JS.SaleInv jSaleInv;
        List<TextBoxbase> list;

        DataTable dtSaleD = new DataTable();  //明細檔
        DataTable tempSaleD = new DataTable();//明細暫存檔

        DataTable dtSaleBom = new DataTable(); //組件檔
        DataTable tempBom = new DataTable();   //所有組件暫存檔

        string DuplicateInDay = "";
        string DuplicatePrDay = "";
        string TextBefore;
        string ItNoBegin;
        string UdfNoBegin;

        decimal BomRec = 0;
        decimal Disc = 1;

        public FrmSaleInv()
        {
            InitializeComponent();
            this.jSaleInv = new JBS.JS.SaleInv();
            this.list = this.getEnumMember();
            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            InDate.SetDateLength();
            PrDate.SetDateLength();

            this.計價數量.Set庫存數量小數();
            this.數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.售價.Set銷貨單價小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前售價.FirstNum = 9;
            this.稅前售價.LastNum = 6;
            this.稅前售價.DefaultCellStyle.Format = "f6";
            this.稅前金額.Set銷項金額小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前單價.FirstNum = 9;
            this.本幣稅前單價.LastNum = 6;
            this.本幣稅前單價.DefaultCellStyle.Format = "f6";
            this.本幣稅前金額.Set本幣金額小數();

            TaxMny.FirstNum = 12;
            TaxMny.LastNum = 0;
            TotMny.FirstNum = 12;
            TotMny.LastNum = 0;
            Tax.FirstNum = 12;
            Tax.LastNum = 0;
            Rate.FirstNum = 1;
            Rate.LastNum = 0;
            Xa1Par.FirstNum = 4;
            Xa1Par.LastNum = 3;

            TaxMny.Visible = Common.User_SalePrice;
            Tax.Visible = Common.User_SalePrice;
            TotMny.Visible = Common.User_SalePrice;
            X3No.Visible = Common.User_SalePrice;
            X3Name.Visible = Common.User_SalePrice;
            Rate.Visible = Common.User_SalePrice;
            X5No.Visible = Common.User_SalePrice;
            X5Name.Visible = Common.User_SalePrice;
            this.售價.Visible = Common.User_SalePrice;
            this.稅前售價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
            this.本幣單價.Visible = Common.User_SalePrice;
            this.本幣稅前單價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;

            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = false;
                this.計位.Visible = false;
            }

            CuNo.Enter -= new EventHandler(TxtEnter);
            CuNo.Enter += new EventHandler(TxtEnter);
            X3No.Enter -= new EventHandler(TxtEnter);
            X3No.Enter += new EventHandler(TxtEnter);
            InNo.Enter -= new EventHandler(TxtEnter);
            InNo.Enter += new EventHandler(TxtEnter);

            dataGridViewT1.DataSource = dtSaleD;
        }

        private void FrmSaleInv_Load(object sender, EventArgs e)
        {
            loadSaleD();
            dtSaleD.Clear();
            dtSaleBom.Clear();

            var pk = jSaleInv.Bottom();
            writeToTxt(pk);
        }

        private void FrmSale_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        private void writeToTxt(string inno)
        {
            BomRec = 0;

            var result = jSaleInv.LoadData(inno, reader =>
            {
                InNo.Text = reader["InNo"].ToString().Trim();
                CoNo.Text = reader["CoNo"].ToString().Trim();
                CoName1.Text = reader["CoName1"].ToString().Trim();
                CuNo.Text = reader["CuNo"].ToString().Trim();
                CuName1.Text = reader["CuName1"].ToString().Trim();
                InvTaxNo.Text = reader["InvTaxNo"].ToString().Trim();
                InvName.Text = reader["InvName"].ToString().Trim();
                InDate.Text = Common.User_DateTime == 1 ? reader["InDate"].ToString().Trim() : reader["InDate1"].ToString().Trim();
                PrDate.Text = Common.User_DateTime == 1 ? reader["PrDate"].ToString().Trim() : reader["PrDate1"].ToString().Trim();
                InvAddr1.Text = reader["invaddr1"].ToString().Trim();

                TaxMny.Text = reader["TaxMny"].ToDecimal().ToString("f" + TaxMny.LastNum);
                Tax.Text = reader["Tax"].ToDecimal().ToString("f" + Tax.LastNum);
                TotMny.Text = reader["TotMny"].ToDecimal().ToString("f" + TotMny.LastNum);

                X3No.Text = reader["X3No"].ToString().Trim();
                X5No.Text = reader["X5No"].ToString().Trim();
                Rate.Text = (reader["Rate"].ToDecimal() * 100).ToString("f0");

                jSaleInv.Validate<JBS.JS.XX03>(X3No.Text, r =>
                {
                    X3No.Text = r["X3No"].ToString();
                    X3Name.Text = r["X3Name"].ToString();
                }, () =>
                {
                    X3No.Clear();
                    X3Name.Clear();
                });

                jSaleInv.Validate<JBS.JS.XX05>(X5No.Text, r =>
                {
                    X5No.Text = r["X5No"].ToString();
                    X5Name.Text = r["X5Name"].ToString();
                }, () =>
                {
                    X5No.Clear();
                    X5Name.Clear();
                });

                Xa1Par.Text = reader["Xa1par"].ToDecimal().ToString("f3");
                InMemo.Text = reader["InMemo"].ToString().Trim();
                InValid.Checked = reader["invalid"].ToDecimal() == 1 ? true : false;
                Xa1Par.Text = reader["Xa1par"].ToDecimal().ToString("f3");

                //載入明細與暫存檔
                loadSaleD();
            });

            if (result == false)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);

                dtSaleD.Clear();
                tempSaleD.Clear();

                dtSaleBom.Clear();
                tempBom.Clear();
            }
        }

        private void loadSaleD()
        {
            dtSaleD.Clear();
            tempSaleD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("InNo", InNo.Text.Trim());

                    cmd.CommandText = @"
                        Select 
                            產品組成 = case
                            when ittrait=1 then '組合品'
                            when ittrait=2 then '組裝品'
                            when ittrait=3 then '單一商品'
                            end,
                            ItNoUdf = '', saleinvd.*
                        From saleinvd 
                        Where InNo=@InNo 
                        Order by recordno";

                    da.Fill(dtSaleD);
                    da.Fill(tempSaleD);
                }
                dataGridViewT1.DataSource = dtSaleD;

                if (dtSaleD.Rows.Count > 0)
                    BomRec = dtSaleD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void loadSaleBom()
        {
            dtSaleBom.Clear();
            tempBom.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@InNo", jSaleInv.GetCurrent());

                    if (FormState == FormEditState.Append)
                        cmd.CommandText = "select top 1 * from saleinvbom where 1=0";
                    else if (FormState == FormEditState.Duplicate)
                        cmd.CommandText = "select * from saleinvbom where InNo=@InNo ";
                    else if (FormState == FormEditState.Modify)
                        cmd.CommandText = "select * from saleinvbom where InNo=@InNo ";

                    da.Fill(dtSaleBom);
                    da.Fill(tempBom);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            var pk = jSaleInv.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jSaleInv.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jSaleInv.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jSaleInv.Bottom();
            writeToTxt(pk);
        }

        private void WorkInit()
        {
            if (FormState == FormEditState.Append)
            {
                this.BomRec = 0;

                dtSaleD.Clear();

                InDate.Text = Date.GetDateTime(Common.User_DateTime, false);
                PrDate.Text = Date.GetDateTime(Common.User_DateTime, false);
                InValid.Checked = false;

                decimal d = 1;
                Xa1Par.Text = d.ToString("f3");
                X3No.Text = "1";
                X3Name.Text = "外加稅";
                d = 5;
                Rate.Text = d.ToString("f0");

                d = 0;
                TaxMny.Text = d.ToString("f" + TaxMny.LastNum);
                Tax.Text = d.ToString("f" + Tax.LastNum);
                TotMny.Text = d.ToString("f" + TotMny.LastNum);

                InNo.Focus();
            }
            else if (FormState == FormEditState.Duplicate)
            {
                loadSaleBom();

                InNo.Text = "";
                InDate.Text = this.DuplicateInDay;
                PrDate.Text = this.DuplicatePrDay;
                InValid.Checked = false;

                InNo.Focus();
            }
            else if (FormState == FormEditState.Modify)
            {
                InDate.Focus();
                InDate.SelectAll();
            }
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            WorkInit();
            loadSaleBom();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (InNo.TrimTextLenth() == 0)
            {
                MessageBox.Show(
                    "空資料庫，請先新增!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            this.DuplicateInDay = InDate.Text.Trim();
            this.DuplicatePrDay = PrDate.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            WorkInit();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (InNo.TrimTextLenth() == 0)
            {
                MessageBox.Show(
                    "空資料庫，請先新增!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            WorkInit();
            loadSaleBom();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (InNo.TrimTextLenth() == 0)
            {
                MessageBox.Show(
                    "空資料庫，請先新增!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var deleteFlag = false;
            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cmd.Parameters.AddWithValue("inno", InNo.Text.Trim());

                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.CommandText = "delete saleinv where inno=@inno";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete saleinvd where inno=@inno";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete saleinvbom where inno=@inno";
                    cmd.ExecuteNonQuery();

                    tn.Commit();
                    deleteFlag = true;
                }
                catch (Exception ex)
                {
                    if (tn != null) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }

            if (deleteFlag)
                btnBottom_Click(null, null);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (InNo.TrimTextLenth() == 0)
                return;

            using (var frm = new FrmSaleInvBrow())
            {
                frm.TSeekNo = InNo.Text.Trim();
                frm.ShowDialog();

                writeToTxt(frm.TResult);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jSaleInv.Cancel();
            writeToTxt(pk);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// 計算金額函式
        /// </summary>
        /// <param name="row"></param>
        /// <param name="index"></param>
        private void GetSpecialPrice(DataRow row, int index)
        {
            var prs = 1M;
            var price = 0M;
            var SpTrait = 0M;

            var itno = row["itno"].ToString().Trim();
            var itname = row["itname"].ToString().Trim();
            var unit = row["itunit"].ToString().Trim();
            var qty = row["qty"].ToDecimal("f" + Common.Q);
            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@itno", itno);
                        cmd.Parameters.AddWithValue("@unit", unit);
                        cmd.Parameters.AddWithValue("@itpkgqty", itpkgqty);

                        cmd.CommandText = " select top 1 * from Speciald "
                                          + " where 0=0"
                                          + " and ItNo=(@itno)"
                                          + " and ItUnit=(@unit)"
                                          + " and ItPkgqty=(@itpkgqty)"
                                          + " and EDate >= '" + Date.ToTWDate(InDate.Text) + "'"
                                          + " and SDate <= '" + Date.ToTWDate(InDate.Text) + "'"
                                          + " order by SDate DESC,RecordNo DESC";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                SpTrait = reader["SpTrait"].ToDecimal();
                                prs = reader["prs"].ToDecimal("f3");
                                price = reader["price"].ToDecimal("f" + Common.MS);
                            }
                        }
                    }
                }
                if (SpTrait == 1)
                {
                    row["price"] = price;
                    row["prs"] = 1.000;
                    dataGridViewT1.InvalidateRow(index);
                }
                else if (SpTrait == 2)
                {
                    jSaleInv.Validate<JBS.JS.Item>(itno, rw =>
                    {
                        price = rw["itprice"].ToDecimal("f" + Common.MS);
                    });

                    row["price"] = price;
                    row["prs"] = prs;
                    dataGridViewT1.InvalidateRow(index);
                }
                else
                {
                    GetSystemPrice(row, index);
                    dataGridViewT1.InvalidateRow(index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetSystemPrice(DataRow row, int index)
        {
            var itno = row["itno"].ToString().Trim();
            var unit = row["itunit"].ToString().Trim();
            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
            var sql = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@itno", itno);
                        cmd.CommandText = "select * from item where itno=(@itno)";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string itunit = reader["itunit"].ToString().Trim();
                                string itunitp = reader["itunitp"].ToString().Trim();
                                if (unit == itunitp && unit != "")
                                {
                                    row["price"] = reader["itpricep"].ToDecimal("f" + Common.MS);
                                }
                                else
                                {
                                    row["price"] = reader["itprice"].ToDecimal("f" + Common.MS);
                                }
                                row["prs"] = "1.000";
                            }
                        }
                    }
                    if (Common.Sys_SalePrice == 2)
                    {
                        sql = " select 售價 = "
                            + " case "
                            + " when c.cuslevel = 1 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep1"
                            + " when c.cuslevel = 2 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep2"
                            + " when c.cuslevel = 3 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep3"
                            + " when c.cuslevel = 4 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep4"
                            + " when c.cuslevel = 5 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep5"
                            + " when c.cuslevel = 6 and i.itunitp=(@itunit) and Len(RTrim(Ltrim(i.itunitp)))>0 then i.itpricep"
                            + " when c.cuslevel = 1  then i.itprice1"
                            + " when c.cuslevel = 2  then i.itprice2"
                            + " when c.cuslevel = 3  then i.itprice3"
                            + " when c.cuslevel = 4  then i.itprice4"
                            + " when c.cuslevel = 5  then i.itprice5"
                            + " when c.cuslevel = 6  then i.itprice"
                            + " end ,c.cudisc 折數"
                            + " from cust as c,item as i"
                            + " where itno=(@itno)"
                            + " and cuno=(@cuno)";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("@itunit", unit.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["折數"].ToDecimal("f3");
                                    row["price"] = reader["售價"].ToDecimal("f" + Common.MS);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 3)
                    {
                        //類別折數(取產品建檔售價/包裝售價，折數取售價等級建檔裡的折數)
                        sql = " select s.*,i.itno,c.cuno from salgrad as s "
                            + " inner join item as i on s.kino=i.scno and itno=(@itno) "
                            + " inner join cust as c on s.x1no=c.cux1no and cuno=(@cuno) ";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) row["prs"] = reader["reprs"].ToDecimal("f3");
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 4)
                    {
                        //歷史售價(最後一次交易售價)
                        sql = " select * from saled where itno=(@itno)"
                            + " and itunit=(@itunit)"
                            + " and itpkgqty=(@itpkgqty)"
                            + " and cuno=(@cuno)"
                            + " order by sadate desc,said desc";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@itunit", unit);
                            cmd.Parameters.AddWithValue("@itpkgqty", itpkgqty);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["prs"].ToDecimal("f3");
                                    row["price"] = reader["price"].ToDecimal("f" + Common.MS);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 5)
                    {
                        sql = " select s.*,i.itno,售價="
                            + " case"
                            + " when s.regrade = 6 and i.itpkgqty = " + itpkgqty + " then i.itpricep"
                            + " when s.regrade = 1 and i.itpkgqty = " + itpkgqty + " then i.itpricep1"
                            + " when s.regrade = 2 and i.itpkgqty = " + itpkgqty + " then i.itpricep2"
                            + " when s.regrade = 3 and i.itpkgqty = " + itpkgqty + " then i.itpricep3"
                            + " when s.regrade = 4 and i.itpkgqty = " + itpkgqty + " then i.itpricep4"
                            + " when s.regrade = 5 and i.itpkgqty = " + itpkgqty + " then i.itpricep5"
                            + " when s.regrade = 6  then i.itprice"
                            + " when s.regrade = 1  then i.itprice1"
                            + " when s.regrade = 2  then i.itprice2"
                            + " when s.regrade = 3  then i.itprice3"
                            + " when s.regrade = 4  then i.itprice4"
                            + " when s.regrade = 5  then i.itprice5"
                            + " end,c.cuno from salgrad as s"
                            + " inner join item as i on s.kino=i.scno and itno=(@itno)"
                            + " inner join cust as c on s.x1no=c.cux1no and cuno=(@cuno)";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["reprs"].ToDecimal("f3");
                                    row["price"] = reader["售價"].ToDecimal("f" + Common.MS);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 7)
                    {
                        //歷史報價
                        sql = " select * from quoted where itno=(@itno)"
                            + " and itunit=(@itunit)"
                            + " and itpkgqty=(@itpkgqty)"
                            + " and cuno=(@cuno)"
                            + " order by qudate desc,quid desc";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@itunit", unit);
                            cmd.Parameters.AddWithValue("@itpkgqty", itpkgqty);
                            cmd.Parameters.AddWithValue("@cuno", CuNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["prs"].ToDecimal("f3");
                                    row["price"] = reader["price"].ToDecimal("f" + Common.MS);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_SalePrice == 6)
                    {
                        row["price"] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetRow_TaxPrice(DataRow row)
        {
            var taxprice = row["price"].ToDecimal() * row["prs"].ToDecimal();
            switch (X3No.Text.Trim())
            {
                case "1":
                case "3":
                case "4":
                    row["taxprice"] = taxprice.ToDecimal("f6");
                    break;
                case "2":
                    row["taxprice"] = (taxprice / (1 + Common.Sys_Rate)).ToDecimal("f6");
                    break;
            }
        }

        private void SetRow_Mny(DataRow row)
        {
            var qty = row["Pqty"].ToDecimal("f" + Common.Q);
            var price = row["price"].ToDecimal("f" + Common.MS);
            var taxprice = row["taxprice"].ToDecimal("f6");

            var mny = qty * taxprice;
            row["mny"] = mny.ToDecimal("f" + Common.TPS);

            var par = Xa1Par.Text.Trim().ToDecimal();
            row["priceb"] = (price * par).ToDecimal("f" + Common.M);
            row["taxpriceb"] = (taxprice * par).ToDecimal("f6");
            row["mnyb"] = (mny * par).ToDecimal("f" + Common.TPS).ToDecimal("f" + Common.M);
        }

        private void SetAllMny()
        {
            var tax = 0M;
            var par = Xa1Par.Text.ToDecimal();
            var sum = dtSaleD.AsEnumerable().Sum(r => r["mny"].ToDecimal()).ToDecimal("f" + Common.TPS);

            if (X3No.Text.ToInteger() == 1)
            {
                tax = (sum * Common.Sys_Rate).ToDecimal("f" + Common.TS);
                TaxMny.Text = sum.ToString("f" + Common.MST);
                Tax.Text = tax.ToString("f" + Common.TS);
                TotMny.Text = (sum + tax).ToString("f" + Common.MST);
            }
            else if (X3No.Text.ToInteger() == 2)
            {
                var totmny = dtSaleD.AsEnumerable().Sum(r => r["Pqty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MS));
                tax = ((totmny / (1 + Common.Sys_Rate)).ToDecimal("f" + Common.MST)) * Common.Sys_Rate;

                TotMny.Text = totmny.ToString("f" + Common.MST);
                tax = tax.ToDecimal("f" + Common.TS);
                Tax.Text = tax.ToString();
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MST);

            }
            else if (X3No.Text.ToInteger() == 3 || X3No.Text.ToInteger() == 4)
            {
                TaxMny.Text = sum.ToString("f" + Common.MST);
                Tax.Text = tax.ToString("f" + Common.TS);
                TotMny.Text = sum.ToString("f" + Common.MST);
            }
        }

        /// <summary>
        /// 明細操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridSaleDAddRows()
        {
            DataRow row = dtSaleD.NewRow();
            row["itno"] = "";
            row["ItNoUdf"] = "";
            row["itname"] = "";
            row["PQty"] = 0;
            row["Punit"] = "";
            row["Qty"] = 0;
            row["itunit"] = "";
            row["Price"] = 0;
            row["TaxPrice"] = 0;
            row["Mny"] = 0;
            row["ItPkgQty"] = 1;
            row["ItTrait"] = 0;
            row["產品組成"] = "";
            row["Memo"] = "";
            row["PriceB"] = 0;
            row["TaxPriceB"] = 0;
            row["MnyB"] = 0;
            row["BomRec"] = GetBomRec();
            if (Common.Sys_SalePrice == 2)
                row["Prs"] = Disc;
            else
                row["Prs"] = "1.00";

            dtSaleD.Rows.Add(row);
            dtSaleD.AcceptChanges();
        }

        private void GridSaleDInsertRows(int index)
        {
            DataRow row = dtSaleD.NewRow();
            row["itno"] = "";
            row["ItNoUdf"] = "";
            row["itname"] = "";
            row["PQty"] = 0;
            row["Punit"] = "";
            row["Qty"] = 0;
            row["itunit"] = "";
            row["Price"] = 0;
            row["TaxPrice"] = 0;
            row["Mny"] = 0;
            row["ItPkgQty"] = 1;
            row["ItTrait"] = 0;
            row["產品組成"] = "";
            row["Memo"] = "";
            row["PriceB"] = 0;
            row["TaxPriceB"] = 0;
            row["MnyB"] = 0;
            row["BomRec"] = GetBomRec();
            if (Common.Sys_SalePrice == 2)
                row["Prs"] = Disc;
            else
                row["Prs"] = "1.00";

            dtSaleD.Rows.InsertAt(row, index);
            dtSaleD.AcceptChanges();
        }

        private decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
        }

        private void InNo_Validating(object sender, CancelEventArgs e)
        {
            if (InNo.ReadOnly || btnCancel.Focused)
                return;

            if (jSaleInv.IsInNoFormat(sender, e) == false)
                return;

            if (FormState != FormEditState.Modify)
            {
                if (jSaleInv.IsExistDocument<JBS.JS.SaleInv>(InNo.Text.Trim()) == true)
                {
                    e.Cancel = true;
                    MessageBox.Show(
                        "此發票號碼已重複,請檢查",
                        "訊息視窗",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                if (TextBefore == InNo.Text.Trim())
                    return;

                if (jSaleInv.IsExistDocument<JBS.JS.SaleInv>(InNo.Text.Trim()) == true)
                {
                    e.Cancel = true;
                    MessageBox.Show(
                        "此發票號碼已重複,請檢查!",
                        "訊息視窗",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void InDate_Validating(object sender, CancelEventArgs e)
        {
            if (InDate.ReadOnly || btnCancel.Focused)
                return;

            jSaleInv.DateValidate(sender, e);
        }

        private void PrDate_Validating(object sender, CancelEventArgs e)
        {
            if (PrDate.ReadOnly || btnCancel.Focused)
                return;

            jSaleInv.DateValidate(sender, e);
        }

        private void TxtEnter(object sender, EventArgs e)
        {
            TextBefore = ((TextBox)sender).Text.Trim();
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            jSaleInv.Open<JBS.JS.Cust>(sender, reader =>
            {
                CuNo.Text = reader["CuNo"].ToString().Trim();
                CuName1.Text = reader["CuName1"].ToString();
                InvTaxNo.Text = reader["CuUno"].ToString();
                X3No.Text = reader["CuX3no"].ToString();
                X5No.Text = reader["CuX5no"].ToString();
                pVar.XX05Validate(reader["CuX5No"].ToString(), X5No, X5Name);
                pVar.XX03Validate(reader["CuX3No"].ToString(), X3No, X3Name);
                InvName.Text = reader["CuInvoName"].ToString();
                InvAddr1.Text = reader["CuAddr2"].ToString();

                this.TextBefore = reader["CuNo"].ToString().Trim();
            });
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly || btnCancel.Focused)
                return;

            if (CuNo.TrimTextLenth() == 0)
            {
                e.Cancel = true;
                MessageBox.Show(
                    "請先輸入客戶編號",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CuNo.Clear();
                return;
            }

            jSaleInv.ValidateOpen<JBS.JS.Cust>(sender, e, reader =>
            {
                if (TextBefore == CuNo.Text.Trim())
                    return;

                CuNo.Text = reader["CuNo"].ToString().Trim();
                CuName1.Text = reader["CuName1"].ToString();
                InvTaxNo.Text = reader["CuUno"].ToString();
                X3No.Text = reader["CuX3no"].ToString();
                X5No.Text = reader["CuX5no"].ToString();
                pVar.XX05Validate(reader["CuX5no"].ToString(), X5No, X5Name);
                pVar.XX03Validate(reader["CuX3no"].ToString(), X3No, X3Name);
                InvName.Text = reader["CuInvoName"].ToString();
                InvAddr1.Text = reader["CuAddr2"].ToString();

                this.TextBefore = reader["CuNo"].ToString().Trim();
            });
        }

        private void X5No_DoubleClick(object sender, EventArgs e)
        {
            jSaleInv.Open<JBS.JS.XX05>(sender, row =>
            {
                X5No.Text = row["X5No"].ToString().Trim();
                X5Name.Text = row["X5Name"].ToString().Trim();
            });
        }

        private void X5No_Validating(object sender, CancelEventArgs e)
        {
            if (X5No.ReadOnly || btnCancel.Focused)
                return;

            if (X5No.TrimTextLenth() == 0)
            {
                X5No.Clear();
                X5Name.Clear();

                e.Cancel = true;
                MessageBox.Show(
                    "發票類別編號不可為空白",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!pVar.XX05Validate(X5No.Text, X5No, X5Name))
            {
                e.Cancel = true;
                X5No_DoubleClick(sender, null);
            }
        }

        private void InvAddr1_Validating(object sender, CancelEventArgs e)
        {
            if (InNo.ReadOnly)
                return;

            if (dataGridViewT1.ReadOnly)
                return;

            if (Common.keyData == Keys.Up)
                return;

            if (CuNo.TrimTextLenth() > 0 && dataGridViewT1.Rows.Count == 0)
                gridAppend_Click(null, null);
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            if (X3No.ReadOnly)
                return;

            jSaleInv.Open<JBS.JS.XX03>(sender, reader =>
            {
                X3No.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = reader["X3Name"].ToString();

                var rate = reader["X3Rate"].ToString().ToDecimal() * 100;
                Rate.Text = rate.ToString("f" + Rate.LastNum);

                for (int i = 0; i < dtSaleD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtSaleD.Rows[i]);
                    SetRow_Mny(dtSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = reader["X3No"].ToString().Trim();
            });
        }

        private void X3No_Validating(object sender, CancelEventArgs e)
        {
            if (X3No.ReadOnly || btnCancel.Focused)
                return;

            if (X3No.TrimTextLenth() == 0 || X3No.Text.ToDecimal() == 0)
            {
                e.Cancel = true;
                MessageBox.Show(
                    "稅別編號不可為空白!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                X3No.SelectAll();
                return;
            }

            jSaleInv.ValidateOpen<JBS.JS.XX03>(sender, e, reader =>
            {
                if (TextBefore == X3No.Text.Trim())
                    return;

                X3No.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = reader["X3Name"].ToString();

                var rate = reader["X3Rate"].ToString().ToDecimal() * 100;
                Rate.Text = rate.ToString("f" + Rate.LastNum);

                for (int i = 0; i < dtSaleD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtSaleD.Rows[i]);
                    SetRow_Mny(dtSaleD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = reader["X3No"].ToString().Trim();
            });
        }

        private void Tax_Validating(object sender, CancelEventArgs e)
        {
            if (Tax.ReadOnly || btnCancel.Focused)
                return;

            decimal tax = Tax.Text.ToDecimal();
            decimal taxmny = TaxMny.Text.ToDecimal();
            decimal totmny = TotMny.Text.ToDecimal();

            if (Common.Sys_X3Forward == 1 && X3No.Text.Trim() == "2")
            {
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MST);
            }
            else
            {
                TotMny.Text = (taxmny + tax).ToString("f" + Common.MST);
            }
        }

        private void InMemo_DoubleClick(object sender, EventArgs e)
        {
            if (InMemo.ReadOnly)
                return;

            using (var frm = new subMenuFm_2.FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    InMemo.Text = frm.Memo.GetUTF8(60);

                InMemo.SelectAll();
            }
        }

        private void Xa1par_Validating(object sender, CancelEventArgs e)
        {
            if (Xa1Par.ReadOnly || btnCancel.Focused)
                return;

            if (Xa1Par.Text.ToDecimal() == 0)
            {
                e.Cancel = true;
                MessageBox.Show(
                    "匯率不可為零!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < dtSaleD.Rows.Count; i++)
            {
                SetRow_Mny(dtSaleD.Rows[i]);
                dataGridViewT1.InvalidateRow(i);
            }
            SetAllMny();
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            if (CuNo.TrimTextLenth() == 0)
            {
                MessageBox.Show(
                    "客戶編號不可為空白!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                CuNo.Focus();
                return;
            }

            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;

            gridAppend.Focus();
            if (dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells[this.產品編號.Name].EditedFormattedValue.IsNullOrEmpty()) == false)
            {
                GridSaleDAddRows();
                dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, dataGridViewT1.Rows.Count - 1];
                dataGridViewT1.CurrentRow.Selected = true;
            }
            dataGridViewT1.Focus();
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            try
            {
                gridDelete.Focus();

                if (dataGridViewT1.Rows.Count == 0)
                    return;

                //刪除明細前，先刪除明細的『組件明細』
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                    return;

                var rec = dtSaleD.Rows[index]["BomRec"].ToString().Trim();
                var rows = dtSaleBom.AsEnumerable().Where(r => r["BomRec"].ToString() != rec);

                if (rows.Count() > 0)
                    dtSaleBom = rows.CopyToDataTable();
                else
                    dtSaleBom.Clear();

                //刪除明細
                dtSaleD.Rows.RemoveAt(index);
                dtSaleD.AcceptChanges();

                if (dataGridViewT1.Rows.Count == 0)
                    return;

                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["序號", i].Value = (i + 1).ToString();
                }
                SetAllMny();

                if (index > dataGridViewT1.Rows.Count - 1)
                    index = dataGridViewT1.Rows.Count - 1;

                dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, index];
                dataGridViewT1.Rows[index].Selected = true;
            }
            finally
            {
                dataGridViewT1.Focus();
            }
        }

        private void gridInsert_Click(object sender, EventArgs e)
        {
            try
            {
                gridInsert.Focus();

                if (dataGridViewT1.Rows.Count == 0)
                    return;

                if (dtSaleD.AsEnumerable().Any(r => r["itno"].ToString().Trim().Length == 0))
                    return;

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                    return;

                GridSaleDInsertRows(index);
                dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, index];
                dataGridViewT1.CurrentRow.Selected = true;
            }
            finally
            {
                dataGridViewT1.Focus();
            }
        }

        private void gridBom_Click(object sender, EventArgs e)
        {
            try
            {
                gridBom.Focus();

                if (dataGridViewT1.Rows.Count == 0)
                    return;

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                    return;

                var trait = dtSaleD.Rows[index]["產品組成"].ToString().Trim();
                if (trait != "組合品" && trait != "組裝品")
                {
                    MessageBox.Show(
                        "只有組合品或組裝品可以編修組件明細!",
                        "訊息視窗",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                using (var frm = new subMenuFm_2.FrmSale_Bom())
                {
                    var table = dtSaleBom.Clone();
                    var bomrec = dtSaleD.Rows[index]["BomRec"].ToString().Trim();

                    var rows = table.AsEnumerable().Where(r => r["BomRec"].ToString().Trim() == bomrec);
                    if (rows.Count() > 0)
                        table = rows.CopyToDataTable();
                    else
                        table.Clear();

                    var _rows = dtSaleBom.AsEnumerable().Where(r => r["BomRec"].ToString().Trim() != bomrec);
                    if (_rows.Count() > 0)
                        dtSaleBom = _rows.CopyToDataTable();
                    else
                        dtSaleBom.Clear();

                    table.AcceptChanges();
                    dtSaleBom.AcceptChanges();


                    frm.dtD = table.Copy();
                    frm.BomRec = bomrec;
                    frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.grid = dataGridViewT1;

                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (frm.CallBack == "Money")
                            {
                                dtSaleBom.Merge(frm.dtD);
                                dtSaleD.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MS);
                                dataGridViewT1.InvalidateRow(dataGridViewT1.SelectedRows[0].Index);
                                dataGridViewT1.Focus();
                                SetRow_TaxPrice(dtSaleD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetRow_Mny(dtSaleD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetAllMny();
                                break;
                            }
                            else
                            {
                                dtSaleBom.Merge(frm.dtD);
                                dtSaleBom.AcceptChanges();
                                dataGridViewT1.Focus();
                                break;
                            }
                        case DialogResult.Cancel:
                            dtSaleBom.Merge(table);
                            dtSaleBom.AcceptChanges();
                            dataGridViewT1.Focus();
                            break;
                    }
                }
            }
            finally
            {
                dataGridViewT1.Focus();
            }
        }

        private void gridCustSale_Click(object sender, EventArgs e)
        {
            if (CuNo.TrimTextLenth() == 0)
            {
                MessageBox.Show(
                    "客戶編號不可為空",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CuNo.Focus();
                return;
            }

            gridCustSale.Focus();

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            var itno = (index == -1) ? "" : dtSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶歷史交易 frm = new S2.Frm該客戶歷史交易())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridTran_Click(object sender, EventArgs e)
        {
            gridTran.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = dtSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶此產品交易 frm = new S2.Frm該客戶此產品交易())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridAllTrans_Click(object sender, EventArgs e)
        {
            gridAllTrans.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = dtSaleD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有客戶此產品交易 frm = new S2.Frm所有客戶此產品交易())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.ReadOnly)
                return;

            if (CuNo.TrimTextLenth() > 0 && dataGridViewT1.Rows.Count == 0)
                gridAppend_Click(null, null);
        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (InNo.ReadOnly)
                return;

            if (dataGridViewT1.Rows.Count == 0)
                return;

            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count)
                return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (CurrentColumnName == "產品編號")
            {
                jSaleInv.DataGridViewOpen<JBS.JS.Item>(sender, e, dtSaleD, row => FillItem(row, e.RowIndex));
            }
            else if (CurrentColumnName == "單位")
            {
                var itno = dtSaleD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                jSaleInv.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        dtSaleD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            dtSaleD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();
                            dtSaleD.Rows[e.RowIndex]["itpkgqty"] = row["itpkgqty"];
                        }
                    }

                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;
                dtSaleD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)//1代表一般進銷存
                {
                    GetSpecialPrice(dtSaleD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                    SetRow_Mny(dtSaleD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
            }
            else if (CurrentColumnName == "備註說明")
            {
                using (var frm = new subMenuFm_2.FrmSale_Memo())
                {
                    var tb = (TextBox)dataGridViewT1.EditingControl;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        dtSaleD.Rows[e.RowIndex]["memo"] = tb.Text = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
            else if (CurrentColumnName == "數量")
            {
                if (Common.Sys_DBqty == 1)
                {
                    using (var frm = new subMenuFm_2.FrmComputer())
                    {
                        frm.w1 = dtSaleD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtSaleD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtSaleD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtSaleD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtSaleD.Rows[e.RowIndex]["Pformula"].ToString();

                        var tb = (TextBox)dataGridViewT1.EditingControl;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtSaleD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtSaleD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtSaleD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtSaleD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtSaleD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;

                            tb.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtSaleD.Rows[e.RowIndex]["Qty"] = frm.resultCount.ToString("f" + Common.Q);
                            dtSaleD.Rows[e.RowIndex]["PQty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            SetAllMny();
                        }
                    }
                }
            }
            else if (CurrentColumnName == "計價數量")
            {
                if (Common.Sys_DBqty == 2)
                {
                    using (var frm = new subMenuFm_2.FrmComputer())
                    {
                        frm.w1 = dtSaleD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = dtSaleD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = dtSaleD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = dtSaleD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = dtSaleD.Rows[e.RowIndex]["Pformula"].ToString();
                        frm.qty = dtSaleD.Rows[e.RowIndex]["qty"].ToDecimal();
                        frm.lbTxt = "數量";

                        var tb = (TextBox)dataGridViewT1.EditingControl;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            dtSaleD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            dtSaleD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            dtSaleD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            dtSaleD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            dtSaleD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;
                            dtSaleD.Rows[e.RowIndex]["qty"] = frm.qty;

                            tb.Text = frm.resultCount.ToString("f" + Common.Q);
                            dtSaleD.Rows[e.RowIndex]["Pqty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            SetAllMny();
                        }
                    }
                }
            }
            else if (CurrentColumnName == "售價")
            {
                if (dtSaleD.Rows[e.RowIndex]["itno"].ToString().Trim() == "")
                    return;

                using (var frm = new SOther.FrmItemLevelb())
                {
                    frm.TSeekNo = dtSaleD.Rows[e.RowIndex]["itno"].ToString().Trim();
                    frm.itunit = dtSaleD.Rows[e.RowIndex]["itunit"].ToString().Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Result.ToDecimal().ToString("f" + Common.MS);
                        dtSaleD.Rows[e.RowIndex]["price"] = frm.Result.ToDecimal("f" + Common.MS);
                        SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                        SetRow_Mny(dtSaleD.Rows[e.RowIndex]);

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        SetAllMny();
                    }
                    ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                }
            }
            else if (CurrentColumnName == "計位")
            {
                if (Common.Sys_DBqty == 2)
                {
                    using (var frm = new SOther.FrmUnit())
                    {
                        frm.Kid = 1;
                        if (DialogResult.OK == frm.ShowDialog())
                            dtSaleD.Rows[e.RowIndex]["punit"] = frm.Result;
                    }
                }
            }
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                TextBefore = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "折數")
            {
                TextBefore = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "售價")
            {
                TextBefore = dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "稅前金額")
            {
                TextBefore = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "計價數量")
            {
                TextBefore = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "數量")
            {
                TextBefore = dataGridViewT1["數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "包裝數量")
            {
                TextBefore = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                TextBefore = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                ItNoBegin = UdfNoBegin = "";
                ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].Value.ToString();

                if (ItNoBegin == "")
                    return;

                jSaleInv.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (CurrentColumnName == "產品編號")
            {
                string ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString();

                //空值->清空
                if (ItNoNow == "" || ItNoNow.Trim().Length == 0)
                {
                    if (btnSave.Focused) return;

                    dtSaleD.Rows[e.RowIndex]["itno"] = "";
                    dtSaleD.Rows[e.RowIndex]["ItNoUdf"] = "";
                    dtSaleD.Rows[e.RowIndex]["itname"] = "";
                    dtSaleD.Rows[e.RowIndex]["itunit"] = "";
                    dtSaleD.Rows[e.RowIndex]["Punit"] = "";
                    dtSaleD.Rows[e.RowIndex]["qty"] = 0;
                    dtSaleD.Rows[e.RowIndex]["Pqty"] = 0;
                    dtSaleD.Rows[e.RowIndex]["Price"] = 0;
                    dtSaleD.Rows[e.RowIndex]["TaxPrice"] = 0;
                    dtSaleD.Rows[e.RowIndex]["Mny"] = 0;
                    dtSaleD.Rows[e.RowIndex]["ItPkgQty"] = 1;
                    dtSaleD.Rows[e.RowIndex]["ItTrait"] = 0;
                    dtSaleD.Rows[e.RowIndex]["產品組成"] = "";
                    dtSaleD.Rows[e.RowIndex]["Memo"] = "";
                    dtSaleD.Rows[e.RowIndex]["PriceB"] = 0;
                    dtSaleD.Rows[e.RowIndex]["TaxPriceB"] = 0;
                    dtSaleD.Rows[e.RowIndex]["MnyB"] = 0;

                    //折數
                    dtSaleD.Rows[e.RowIndex]["Prs"] = (Common.Sys_SalePrice == 2) ? Disc : 1;

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();

                    var rec = dtSaleD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jSaleInv.RemoveBom(rec, ref dtSaleBom);
                    return;
                }
                //值沒變->離開
                if (ItNoNow == ItNoBegin)
                    return;

                //值有變，但是跟自定編號一樣，視同沒變->離開
                //把『自定編號』改成『產品編號』
                if (ItNoNow != ItNoBegin)
                {
                    if (ItNoNow == UdfNoBegin)
                    {
                        dataGridViewT1.EditingControl.Text = ItNoBegin;
                        dtSaleD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }
                //值變了，跟產品編號和自定編號都不一樣,帶值出來
                //若找不到這筆資料則開窗
                if (ItNoNow != ItNoBegin && ItNoNow != UdfNoBegin)
                {
                    jSaleInv.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtSaleD, row => FillItem(row, e.RowIndex));
                }
            }
            else if (CurrentColumnName == "單位")
            {
                string itno = dtSaleD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (TextBefore == unit)
                    return;

                jSaleInv.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        dtSaleD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        dtSaleD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                dtSaleD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)
                {
                    GetSpecialPrice(dtSaleD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                    SetRow_Mny(dtSaleD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
            }
            else if (CurrentColumnName == "數量")
            {
                var qty = dataGridViewT1["數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                if (qty == TextBefore.ToDecimal())
                    return;

                if (Common.Sys_DBqty == 1)
                {
                    dtSaleD.Rows[e.RowIndex]["Qty"] = qty;
                    dtSaleD.Rows[e.RowIndex]["PQty"] = qty;
                    dtSaleD.Rows[e.RowIndex]["punit"] = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
                }
                else if (Common.Sys_DBqty == 2)
                {
                    dtSaleD.Rows[e.RowIndex]["Qty"] = qty;
                }

                SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (CurrentColumnName == "計價數量")
            {
                var pqty = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                if (pqty == TextBefore.ToDecimal())
                    return;

                dtSaleD.Rows[e.RowIndex]["PQty"] = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (CurrentColumnName == "售價")
            {
                var price = dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS);
                if (price == TextBefore.ToDecimal())
                    return;

                dtSaleD.Rows[e.RowIndex]["Price"] = dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS);
                if (Common.Sys_LowCost != 3 && dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() != "")
                    pVar.CheckLowCost(dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["單位", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS));

                SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (CurrentColumnName == "折數")
            {
                var prs = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal();
                if (prs == TextBefore.ToDecimal())
                    return;

                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                dtSaleD.Rows[e.RowIndex]["Prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue;

                SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);
                SetRow_Mny(dtSaleD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (CurrentColumnName == "稅前金額")
            {
                //正常情形『稅前金額』是由『售價』帶出來的
                //下面處理的情形是手動打上『稅前金額』
                //所以須往前推算『售價』金額。
                decimal price = 0;
                decimal qty = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f" + Common.Q);
                decimal taxprice = dataGridViewT1["稅前售價", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f6");
                decimal mny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f" + Common.TPS);
                decimal prs = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToString().ToDecimal();
                qty = (qty == 0) ? 1 : qty;
                if (TextBefore.ToDecimal() == mny) return;

                dtSaleD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / qty) / prs).ToDecimal("f" + Common.MS);
                        dtSaleD.Rows[e.RowIndex]["Price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / qty) / prs).ToDecimal("f" + Common.MS);
                        dtSaleD.Rows[e.RowIndex]["Price"] = price;
                        break;
                }
                SetRow_TaxPrice(dtSaleD.Rows[e.RowIndex]);

                taxprice = dtSaleD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                dtSaleD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                dtSaleD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                dtSaleD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (CurrentColumnName == "包裝數量")
            {
                var itpkgqty = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                if (itpkgqty == TextBefore.ToDecimal())
                    return;

                if (itpkgqty <= 0)
                {
                    e.Cancel = true;
                    MessageBox.Show(
                        "包裝數量不可小於零!",
                        "訊息視窗",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    if (dataGridViewT1.EditingControl != null)
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                    return;
                }

                dtSaleD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }
        private void FillItem(SqlDataReader row, int index)
        {
            this.ItNoBegin = row["ItNo"].ToString();

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = row["ItNo"].ToString();

            dtSaleD.Rows[index]["ItNo"] = row["ItNo"];
            dtSaleD.Rows[index]["ItName"] = row["ItName"];
            dtSaleD.Rows[index]["ItNoUdf"] = row["ItNoUdf"];
            dtSaleD.Rows[index]["Punit"] = row["Punit"];

            //銷貨常用單位
            var utype = row["ItSalUnit"].ToString().Trim();
            var unit = "";

            //預設帶包裝單位或是單位
            if (utype == "1")
            {
                unit = row["ItUnitp"].ToString();
                dtSaleD.Rows[index]["ItUnit"] = unit;
                dtSaleD.Rows[index]["itpkgqty"] = row["itpkgqty"].ToDecimal("f" + Common.Q);
            }
            else
            {
                unit = row["ItUnit"].ToString();
                dtSaleD.Rows[index]["ItUnit"] = unit;
                dtSaleD.Rows[index]["itpkgqty"] = 1;
            }

            GetSpecialPrice(dtSaleD.Rows[index], index);
            SetRow_TaxPrice(dtSaleD.Rows[index]);
            SetRow_Mny(dtSaleD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();

            //組合組裝品
            var trait = row["ItTrait"].ToString().Trim();
            dtSaleD.Rows[index]["ItTrait"] = trait;

            if (trait == "1")
                trait = "組合品";
            else if (trait == "2")
                trait = "組裝品";
            else
                trait = "單一商品";
            dtSaleD.Rows[index]["產品組成"] = trait;

            //BOM
            var rec = dtSaleD.Rows[index]["BomRec"].ToString().Trim();
            jSaleInv.RemoveBom(rec, ref dtSaleBom);
            jSaleInv.GetItemBom(row["itno"].ToString().Trim(), rec, ref dtSaleBom);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.D1 | keyData == Keys.NumPad1 && btnAppend.Enabled)
            {
                btnAppend.PerformClick();
                return true;
            }
            else if (keyData == Keys.D2 | keyData == Keys.NumPad2 && btnModify.Enabled)
            {
                btnModify.PerformClick();
            }
            else if (keyData == Keys.D3 | keyData == Keys.NumPad3 && btnDelete.Enabled)
            {
                btnDelete.PerformClick();
            }
            else if (keyData == Keys.D4 | keyData == Keys.NumPad4 && btnBrow.Enabled)
            {
                btnBrow.PerformClick();
            }
            else if (keyData == Keys.D0 | keyData == Keys.NumPad0 | keyData == Keys.F11 && btnBrow.Enabled)
            {
                btnExit.PerformClick();
            }
            else if (keyData == Keys.Home && btnTop.Enabled)
            {
                btnTop.PerformClick();
            }
            else if (keyData == Keys.PageUp && btnPrior.Enabled)
            {
                btnPrior.PerformClick();
            }
            else if (keyData == Keys.PageDown && btnNext.Enabled)
            {
                btnNext.PerformClick();
            }
            else if (keyData == Keys.End && btnBottom.Enabled)
            {
                btnBottom.PerformClick();
            }
            else if (keyData == Keys.F9 && btnSave.Enabled)
            {
                btnSave.PerformClick();
            }
            else if (keyData == Keys.F4 && btnCancel.Enabled)
            {
                btnCancel.Focus();
                btnCancel.PerformClick();
            }
            else if (keyData == Keys.F2 && gridAppend.Enabled)
            {
                gridAppend_Click(null, null);
            }
            else if (keyData == Keys.F3 && gridDelete.Enabled)
            {
                gridDelete_Click(null, null);
            }
            else if (keyData == Keys.F5 && gridInsert.Enabled)
            {
                gridInsert_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift") && gridTran.Enabled)
            {
                gridTran_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F10") && keyData.ToString().EndsWith("Shift") && gridAllTrans.Enabled)
            {
                gridAllTrans_Click(null, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ToolTip tip = new ToolTip();
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == this.產品編號.Name || str == this.備註說明.Name || str == this.售價.Name)
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                t.KeyDown += new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
            }
            else if (str == this.數量.Name)
            {
                if (Common.Sys_DBqty == 1)
                {
                    t.KeyDown -= new KeyEventHandler(t_KeyDown);
                    t.KeyDown += new KeyEventHandler(t_KeyDown);
                    tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
                }
                else
                {
                    t.KeyDown -= new KeyEventHandler(t_KeyDown);
                    tip.SetToolTip(t, "");
                }
            }
            else if (str == this.計價數量.Name)
            {
                if (Common.Sys_DBqty == 2)
                {
                    t.KeyDown -= new KeyEventHandler(t_KeyDown);
                    t.KeyDown += new KeyEventHandler(t_KeyDown);
                    tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
                }
                else
                {
                    t.KeyDown -= new KeyEventHandler(t_KeyDown);
                    tip.SetToolTip(t, "");
                }
            }
            else
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "");
            }
        }

        private void t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F12)
            {
                dataGridViewT1_CellDoubleClick(dataGridViewT1, new DataGridViewCellEventArgs(dataGridViewT1.CurrentCell.ColumnIndex, dataGridViewT1.CurrentCell.RowIndex));
            }
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            InValid.Enabled = gridAppend.Enabled = gridDelete.Enabled = gridCustSale.Enabled = !btnAppend.Enabled;
            gridInsert.Enabled = gridBom.Enabled = gridTran.Enabled = gridAllTrans.Enabled = !btnAppend.Enabled;


            dataGridViewT1.ReadOnly = btnAppend.Enabled;
            this.序號.ReadOnly = true;
            this.稅前售價.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.折數.ReadOnly = (Common.Sys_KeyPrs != 1);
        }

        private void CoNo_DoubleClick(object sender, EventArgs e)
        {
            jSaleInv.Open<JBS.JS.Comp>(sender, row =>
            {
                CoNo.Text = row["CoNo"].ToString().Trim();
                CoName1.Text = row["coname1"].ToString().Trim();
            });
        }

        private void CoNo_Validating(object sender, CancelEventArgs e)
        {
            if (CoNo.ReadOnly)
                return;

            if (btnCancel.Focused)
                return;

            if (CoNo.TrimTextLenth() == 0)
            {
                CoNo.Clear();
                CoName1.Clear();
                return;
            }

            jSaleInv.ValidateOpen<JBS.JS.Comp>(sender, e, row =>
            {
                CoNo.Text = row["CoNo"].ToString().Trim();
                CoName1.Text = row["coname1"].ToString().Trim();
            });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (CuNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("客戶編號不可為空白!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                CuNo.Focus();
                return;
            }

            jSaleInv.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtSaleD, ref dtSaleBom, SetAllMny);

            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("無產品明細,無法存檔!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (Common.TPS == Common.MST && X3No.Text.ToDecimal() != 2)
            {
                var checktaxmny = dtSaleD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f" + Common.TPS));
                if (TaxMny.Text.ToDecimal() != checktaxmny)
                {
                    MessageBox.Show(
                        "稅前合計金額有誤！無法存檔！",
                        "訊息視窗",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("inno", InNo.Text.Trim());
                cmd.Parameters.AddWithValue("indate", Date.ToTWDate(InDate.Text));
                cmd.Parameters.AddWithValue("indate1", Date.ToUSDate(InDate.Text));
                cmd.Parameters.AddWithValue("prdate", Date.ToTWDate(PrDate.Text));
                cmd.Parameters.AddWithValue("prdate1", Date.ToUSDate(PrDate.Text));
                cmd.Parameters.AddWithValue("cono", CoNo.Text.Trim());
                cmd.Parameters.AddWithValue("coname1", CoName1.Text);
                cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                cmd.Parameters.AddWithValue("cuname1", CuName1.Text);
                cmd.Parameters.AddWithValue("invtaxno", InvTaxNo.Text);
                cmd.Parameters.AddWithValue("x5no", X5No.Text);
                cmd.Parameters.AddWithValue("x3no", X3No.Text);
                cmd.Parameters.AddWithValue("invname", InvName.Text.Trim());
                cmd.Parameters.AddWithValue("invaddr1", InvAddr1.Text.Trim());
                cmd.Parameters.AddWithValue("tax", Tax.Text);
                cmd.Parameters.AddWithValue("taxmny", TaxMny.Text);
                cmd.Parameters.AddWithValue("totmny", TotMny.Text);
                cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                cmd.Parameters.AddWithValue("inmemo", InMemo.Text.Trim());
                cmd.Parameters.AddWithValue("recordno", dtSaleD.Rows.Count);
                cmd.Parameters.AddWithValue("invalid", InValid.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text);
                cmd.Parameters.AddWithValue("itno", "");
                cmd.Parameters.AddWithValue("itname", "");
                cmd.Parameters.AddWithValue("ittrait", "");
                cmd.Parameters.AddWithValue("itunit", "");
                cmd.Parameters.AddWithValue("punit", "");
                cmd.Parameters.AddWithValue("itpkgqty", 0.0);
                cmd.Parameters.AddWithValue("qty", 0.0);
                cmd.Parameters.AddWithValue("pqty", 0.0);
                cmd.Parameters.AddWithValue("price", 0.0);
                cmd.Parameters.AddWithValue("prs", 0.0);
                cmd.Parameters.AddWithValue("taxprice", 0.0);
                cmd.Parameters.AddWithValue("mny", 0.0);
                cmd.Parameters.AddWithValue("memo", "");
                cmd.Parameters.AddWithValue("bomid", "");
                cmd.Parameters.AddWithValue("bomrec", "");
                cmd.Parameters.AddWithValue("priceb", 0.0);
                cmd.Parameters.AddWithValue("taxpriceb", 0.0);
                cmd.Parameters.AddWithValue("mnyb", 0.0);
                cmd.Parameters.AddWithValue("boitno", "");
                cmd.Parameters.AddWithValue("itqty", 0.0);
                cmd.Parameters.AddWithValue("itpareprs", 0.0);
                cmd.Parameters.AddWithValue("itprice", 0.0);
                cmd.Parameters.AddWithValue("itmny", 0.0);
                cmd.Parameters.AddWithValue("itprs", 0.0);
                cmd.Parameters.AddWithValue("itnote", "");
                cmd.Parameters.AddWithValue("itrec", "");
                var inputday = Date.GetDateTime(2) + DateTime.Now.ToString("HHmmss");
                cmd.Parameters.AddWithValue("inputday", inputday);

                try
                {
                    if (FormState == FormEditState.Append || FormState == FormEditState.Duplicate)
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = @"
                            insert into saleinv 
                            (inno,indate,indate1,prdate,prdate1,cono,coname1,cuno,cuname1,invtaxno,x5no,x3no,invname,invaddr1,tax,taxmny,totmny,rate,inmemo,recordno,invalid,xa1par,inputday)
                            values
                            (@inno,@indate,@indate1,@prdate,@prdate1,@cono,@coname1,@cuno,@cuname1,@invtaxno,@x5no,@x3no,@invname,@invaddr1,@tax,@taxmny,@totmny,@rate,@inmemo,@recordno,@invalid,@xa1par,@inputday)";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < dtSaleD.Rows.Count; i++)
                        {
                            cmd.Parameters["itno"].Value = dtSaleD.Rows[i]["itno"].ToString().Trim();
                            cmd.Parameters["itname"].Value = dtSaleD.Rows[i]["itname"].ToString().Trim();
                            cmd.Parameters["itunit"].Value = dtSaleD.Rows[i]["itunit"].ToString().Trim();
                            cmd.Parameters["punit"].Value = dtSaleD.Rows[i]["punit"].ToString().Trim();
                            cmd.Parameters["qty"].Value = dtSaleD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                            cmd.Parameters["pqty"].Value = dtSaleD.Rows[i]["pqty"].ToDecimal("f" + Common.Q);
                            cmd.Parameters["itpkgqty"].Value = dtSaleD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q);
                            cmd.Parameters["price"].Value = dtSaleD.Rows[i]["price"].ToDecimal("f" + Common.MS);
                            cmd.Parameters["prs"].Value = dtSaleD.Rows[i]["prs"].ToDecimal("f3");
                            cmd.Parameters["taxprice"].Value = dtSaleD.Rows[i]["taxprice"].ToDecimal("f6");
                            cmd.Parameters["mny"].Value = dtSaleD.Rows[i]["mny"].ToDecimal("f" + Common.TPS);
                            cmd.Parameters["memo"].Value = dtSaleD.Rows[i]["memo"].ToString().Trim();
                            cmd.Parameters["recordno"].Value = (i + 1);
                            var bomrec = dtSaleD.Rows[i]["bomrec"].ToString().Trim();
                            cmd.Parameters["bomid"].Value = InNo.Text + bomrec.PadLeft(10, '0');
                            cmd.Parameters["bomrec"].Value = bomrec;
                            cmd.Parameters["ittrait"].Value = dtSaleD.Rows[i]["ittrait"].ToString().Trim();
                            var xa1apr = Xa1Par.Text.ToDecimal();
                            cmd.Parameters["priceb"].Value = (dtSaleD.Rows[i]["mny"].ToDecimal() * xa1apr).ToDecimal("f" + Common.M);
                            cmd.Parameters["taxpriceb"].Value = (dtSaleD.Rows[i]["mny"].ToDecimal() * xa1apr).ToDecimal("f6");
                            cmd.Parameters["mnyb"].Value = (dtSaleD.Rows[i]["mny"].ToDecimal() * xa1apr).ToDecimal("f" + Common.M);

                            cmd.CommandText = @"
                                insert into saleinvd 
                                (inno,indate,indate1,prdate,prdate1,cono,cuno,x5no,itno,itname,itunit,punit,pqty,qty,price,rate,prs,taxprice,mny,memo,recordno,bomid,bomrec,ittrait,xa1par,priceb,taxpriceb,mnyb,itpkgqty)
                                values
                                (@inno,@indate,@indate1,@prdate,@prdate1,@cono,@cuno,@x5no,@itno,@itname,@itunit,@punit,@pqty,@qty,@price,@rate,@prs,@taxprice,@mny,@memo,@recordno,@bomid,@bomrec,@ittrait,@xa1par,@priceb,@taxpriceb,@mnyb,@itpkgqty)";
                            cmd.ExecuteNonQuery();

                            var rows = dtSaleBom.AsEnumerable().Where(r => r["bomrec"].ToString().Trim() == bomrec);
                            for (int j = 0; j < rows.Count(); j++)
                            {
                                cmd.Parameters["bomid"].Value = InNo.Text + bomrec.PadLeft(10, '0');
                                cmd.Parameters["bomrec"].Value = bomrec;
                                cmd.Parameters["itno"].Value = rows.ElementAt(j)["itno"];
                                cmd.Parameters["itname"].Value = rows.ElementAt(j)["itname"];
                                cmd.Parameters["itunit"].Value = rows.ElementAt(j)["itunit"];
                                cmd.Parameters["itqty"].Value = rows.ElementAt(j)["itqty"];
                                cmd.Parameters["itpareprs"].Value = rows.ElementAt(j)["itpareprs"];
                                cmd.Parameters["itpkgqty"].Value = rows.ElementAt(j)["itpkgqty"];
                                cmd.Parameters["itrec"].Value = (j + 1);
                                cmd.Parameters["itprice"].Value = rows.ElementAt(j)["itprice"];
                                cmd.Parameters["itprs"].Value = rows.ElementAt(j)["itprs"];
                                cmd.Parameters["itmny"].Value = rows.ElementAt(j)["itmny"];
                                cmd.Parameters["itnote"].Value = rows.ElementAt(j)["itnote"];
                                cmd.CommandText = @"
                                    insert into saleinvbom (
                                    bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,itprice,itprs,itmny,itnote,inno) 
                                    VALUES (
                                    @bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote,@inno)";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else if (FormState == FormEditState.Modify)
                    {
                        if (jSaleInv.IsExistDocument<JBS.JS.SaleInv>(jSaleInv.GetCurrent()) == false)
                        {
                            MessageBox.Show("此單據以被刪除!");
                            btnNext_Click(null, null);
                            return;
                        }

                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters["inno"].Value = InNo.Text.Trim();
                        cmd.CommandText = @"
                            Delete from saleinvd where inno=(@inno);
                            Delete from saleinvbom where inno=(@inno);";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                        update saleinv set
                            inno = @inno,
                            indate = @indate,
                            indate1 = @indate1,
                            prdate = @prdate,
                            prdate1 = @prdate1,
                            cono = @cono,
                            coname1 = @coname1,
                            cuno = @cuno,
                            cuname1 = @cuname1,
                            invtaxno = @invtaxno,
                            x5no = @x5no,
                            x3no = @x3no,
                            invname = @invname,
                            invaddr1 = @invaddr1,
                            tax = @tax,
                            taxmny = @taxmny,
                            totmny = @totmny,
                            rate = @rate,
                            inmemo = @inmemo,
                            recordno = @recordno,
                            invalid = @invalid,
                            xa1par = @xa1par
                        where inno= @inno";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < dtSaleD.Rows.Count; i++)
                        {
                            cmd.Parameters["itno"].Value = dtSaleD.Rows[i]["itno"].ToString().Trim();
                            cmd.Parameters["itname"].Value = dtSaleD.Rows[i]["itname"].ToString().Trim();
                            cmd.Parameters["itunit"].Value = dtSaleD.Rows[i]["itunit"].ToString().Trim();
                            cmd.Parameters["punit"].Value = dtSaleD.Rows[i]["punit"].ToString().Trim();
                            cmd.Parameters["qty"].Value = dtSaleD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                            cmd.Parameters["pqty"].Value = dtSaleD.Rows[i]["pqty"].ToDecimal("f" + Common.Q);
                            cmd.Parameters["itpkgqty"].Value = dtSaleD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q);
                            cmd.Parameters["price"].Value = dtSaleD.Rows[i]["price"].ToDecimal("f" + Common.MS);
                            cmd.Parameters["prs"].Value = dtSaleD.Rows[i]["prs"].ToDecimal("f3");
                            cmd.Parameters["taxprice"].Value = dtSaleD.Rows[i]["taxprice"].ToDecimal("f6");
                            cmd.Parameters["mny"].Value = dtSaleD.Rows[i]["mny"].ToDecimal("f" + Common.TPS);
                            cmd.Parameters["memo"].Value = dtSaleD.Rows[i]["memo"].ToString().Trim();
                            cmd.Parameters["recordno"].Value = (i + 1);
                            var bomrec = dtSaleD.Rows[i]["bomrec"].ToString().Trim();
                            cmd.Parameters["bomid"].Value = InNo.Text + bomrec.PadLeft(10, '0');
                            cmd.Parameters["bomrec"].Value = bomrec;
                            cmd.Parameters["ittrait"].Value = dtSaleD.Rows[i]["ittrait"].ToString().Trim();
                            var xa1apr = Xa1Par.Text.ToDecimal();
                            cmd.Parameters["priceb"].Value = (dtSaleD.Rows[i]["mny"].ToDecimal() * xa1apr).ToDecimal("f" + Common.M);
                            cmd.Parameters["taxpriceb"].Value = (dtSaleD.Rows[i]["mny"].ToDecimal() * xa1apr).ToDecimal("f6");
                            cmd.Parameters["mnyb"].Value = (dtSaleD.Rows[i]["mny"].ToDecimal() * xa1apr).ToDecimal("f" + Common.M);

                            cmd.CommandText = @"
                                insert into saleinvd 
                                (inno,indate,indate1,prdate,prdate1,cono,cuno,x5no,itno,itname,itunit,punit,pqty,qty,price,rate,prs,taxprice,mny,memo,recordno,bomid,bomrec,ittrait,xa1par,priceb,taxpriceb,mnyb,itpkgqty)
                                values
                                (@inno,@indate,@indate1,@prdate,@prdate1,@cono,@cuno,@x5no,@itno,@itname,@itunit,@punit,@pqty,@qty,@price,@rate,@prs,@taxprice,@mny,@memo,@recordno,@bomid,@bomrec,@ittrait,@xa1par,@priceb,@taxpriceb,@mnyb,@itpkgqty)";
                            cmd.ExecuteNonQuery();

                            var rows = dtSaleBom.AsEnumerable().Where(r => r["bomrec"].ToString().Trim() == bomrec);
                            for (int j = 0; j < rows.Count(); j++)
                            {
                                cmd.Parameters["bomid"].Value = InNo.Text + bomrec.PadLeft(10, '0');
                                cmd.Parameters["bomrec"].Value = bomrec;
                                cmd.Parameters["itno"].Value = rows.ElementAt(j)["itno"];
                                cmd.Parameters["itname"].Value = rows.ElementAt(j)["itname"];
                                cmd.Parameters["itunit"].Value = rows.ElementAt(j)["itunit"];
                                cmd.Parameters["itqty"].Value = rows.ElementAt(j)["itqty"];
                                cmd.Parameters["itpareprs"].Value = rows.ElementAt(j)["itpareprs"];
                                cmd.Parameters["itpkgqty"].Value = rows.ElementAt(j)["itpkgqty"];
                                cmd.Parameters["itrec"].Value = (j + 1);
                                cmd.Parameters["itprice"].Value = rows.ElementAt(j)["itprice"];
                                cmd.Parameters["itprs"].Value = rows.ElementAt(j)["itprs"];
                                cmd.Parameters["itmny"].Value = rows.ElementAt(j)["itmny"];
                                cmd.Parameters["itnote"].Value = rows.ElementAt(j)["itnote"];
                                cmd.CommandText = @"
                                    insert into saleinvbom (
                                    bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,itprice,itprs,itmny,itnote,inno) 
                                    VALUES (
                                    @bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote,@inno)";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    tn.Commit();

                    jSaleInv.Save(InNo.Text.Trim());
                }
                catch (Exception ex)
                {
                    if (tn != null) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
            btnAppend_Click(null, null);
        }
         
       

        
    }
}
