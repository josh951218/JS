using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmLendToRlendBAT : Formbase
    {
        JBS.JS.xEvents xe;
        public string cuno = "";
        public decimal disc = 1;
        public string sadate = "";
        public string x3no = "1";
        public decimal xa1par = 1;
        public int maxrec = 0;
        public string stno = "";
        public string stname = "";
        public DataTable dtSaleD = new DataTable();
        public DataTable lendtemp = new DataTable();
        public DataTable lendtempNOedit = new DataTable();


        StringFormat mat = new StringFormat();
        DataTable ttt = new DataTable();
        DataRow rowItem = null;

        public FrmLendToRlendBAT()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            this.序號.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.勾選.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mat.Alignment = StringAlignment.Center;
            mat.LineAlignment = StringAlignment.Center;
            this.借出日期.DataPropertyName = Common.User_DateTime == 1 ? "ledate" : "ledate1";

            this.數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.借出未還量.Set庫存數量小數();
            tbQty.Set庫存數量小數();
            tbItPkgQty.FirstNum = 3;
            tbItPkgQty.LastNum = Common.Q;

            this.序號.Visible = false;
        }

        private void frmLendToSaleBAT_Load(object sender, EventArgs e)
        {
            if (lendtemp.Rows.Count == 0) LoadMaster();

            dataGridViewT1.DataSource = lendtemp.DefaultView;
            dataGridViewT2.DataSource = dtSaleD;
        }

        private void LoadMaster()
        {
            lendtemp.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("cuno", cuno);
                cmd.CommandText = @"
                    select 勾選='',tempQty=lendd.qtynotout,lend.cuname1,lend.lememo1,lend.lememo,lend.xa1no,lend.xa1name,lend.xa1par,lend.x3no,lend.rate,lend.emno,lend.emname,lend.leoverflag,lendd.* 
                    from lend 
                    left join lendd on lend.leno=lendd.leno 
                    where lendd.qtynotout > 0 And lendd.cuno=(@cuno) 
                    order by lendd.itno asc, lendd.ledate asc, lendd.leno asc ";

                da.Fill(lendtemp);
                lendtempNOedit = lendtemp.Copy();
            }
            if (lendtemp.Rows.Count == 0)
            {
                this.Visible = false;
                this.Shown -= new EventHandler(frmLendToSaleBAT_Shown);
                this.Shown += new EventHandler(frmLendToSaleBAT_Shown);
                MessageBox.Show("查無資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmLendToSaleBAT_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Dispose();
        }

        protected override void OnShown(EventArgs e)
        {
            DefaultText();
            base.OnShown(e);
        }

        void GridSaleDAddRows(string unit)
        {
            DataRow row = dtSaleD.NewRow();
            row["itno"] = rowItem["itno"].ToString().Trim();
            row["ItNoUdf"] = rowItem["ItNoUdf"].ToString().Trim();
            row["itname"] = rowItem["itname"].ToString().Trim();

            row["PQty"] = tbQty.Text.ToDecimal("f" + Common.Q);
            row["Qty"] = tbQty.Text.ToDecimal("f" + Common.Q);
            row["Punit"] = rowItem["Punit"].ToString().Trim();
            row["ItPkgQty"] = tbItPkgQty.Text.ToDecimal("f" + Common.Q);
            //帶借出單據的單位
            row["itunit"] = unit;

            GetSpecialPrice(row);
            SetRow_TaxPrice(row);
            SetRow_Mny(row);

            row["leno"] = "批借轉銷";
            row["lenoid"] = "V";
            row["StNo"] = stno;
            row["StName"] = stname;

            if (Common.Sys_SalePrice == 2) row["Prs"] = disc;
            else row["Prs"] = "1.00";

            var ittrait = rowItem["ittrait"].ToDecimal();
            row["ittrait"] = ittrait;

            if (ittrait == 1) row["產品組成"] = "組合品";
            else if (ittrait == 2) row["產品組成"] = "組裝品";
            else row["產品組成"] = "單一商品";

            for (int x = 1; x <= 10; x++)
            {
                row["ItDesp" + x] = rowItem["ItDesp" + x];
            }
            row["BomRec"] = ++maxrec;

            dtSaleD.Rows.Add(row);
            dtSaleD.AcceptChanges();
        }

        void GetSpecialPrice(DataRow row)
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
                                          + " and EDate >= '" + Date.ToTWDate(sadate) + "'"
                                          + " and SDate <= '" + Date.ToTWDate(sadate) + "'"
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
                }
                else if (SpTrait == 2)
                {
                    xe.Validate<JBS.JS.Item>(itno, rw =>
                    {
                        price = rw["itprice"].ToDecimal("f" + Common.MS);
                    });

                    row["price"] = price;
                    row["prs"] = prs;
                }
                else
                {
                    GetSystemPrice(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void GetSystemPrice(DataRow row)
        {
            var sql = "";
            var itno = row["itno"].ToString().Trim();
            var unit = row["itunit"].ToString().Trim();
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
                            cmd.Parameters.AddWithValue("@cuno", cuno);
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
                            cmd.Parameters.AddWithValue("@cuno", cuno);
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
                            cmd.Parameters.AddWithValue("@cuno", cuno);
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
                            cmd.Parameters.AddWithValue("@cuno", cuno);
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
                            cmd.Parameters.AddWithValue("@cuno", cuno);
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

        void SetRow_TaxPrice(DataRow row)
        {
            var taxprice = row["price"].ToDecimal() * row["prs"].ToDecimal();
            switch (x3no)
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

        void SetRow_Mny(DataRow row)
        {
            var qty = row["Pqty"].ToDecimal("f" + Common.Q);
            var price = row["price"].ToDecimal("f" + Common.MS);
            var taxprice = row["taxprice"].ToDecimal("f6");

            var mny = qty * taxprice;
            row["mny"] = mny.ToDecimal("f" + Common.TPS);

            var par = xa1par;
            row["priceb"] = (price * par).ToDecimal("f" + Common.M);
            row["taxpriceb"] = (taxprice * par).ToDecimal("f6");
            row["mnyb"] = (mny * par).ToDecimal("f" + Common.TPS).ToDecimal("f" + Common.M);
        }

        private void tbSort_Validating(object sender, CancelEventArgs e)
        {
            if (tbSort.ReadOnly) return;
            if (gridCancel.Focused) return;

            if (tbSort.Text != "1" && tbSort.Text != "2")
            {
                e.Cancel = true;
                MessageBox.Show("只能輸入 1,或是 2,請確認!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbSort.SelectAll();
                return;
            }
        }

        private void tbAuto_Validating(object sender, CancelEventArgs e)
        {
            if (tbAuto.ReadOnly) return;
            if (gridCancel.Focused) return;

            if (tbAuto.Text != "1" && tbAuto.Text != "2")
            {
                e.Cancel = true;
                MessageBox.Show("只能輸入 1,或是 2,請確認!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbAuto.SelectAll();
                return;
            }
        }

        private void tbItPkgQty_Validating(object sender, CancelEventArgs e)
        {
            if (tbItPkgQty.ReadOnly) return;
            if (gridCancel.Focused) return;

            if (tbItPkgQty.Text.ToDecimal() <= 0)
            {
                e.Cancel = true;
                MessageBox.Show("您輸入的數量必須大於0, 請確定後重新輸入!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbItPkgQty.SelectAll();
                return;
            }
        }

        private void tbItNo_Validating(object sender, CancelEventArgs e)
        {
            if (tbItNo.ReadOnly) return;
            if (gridCancel.Focused) return;

            if (tbItNo.TrimTextLenth() == 0)
            {
                tbItNo.Clear();
                lendtemp.DefaultView.RowFilter = "1=1";
                mode1();
                return;
            }

            var itno = tbItNo.Text.Trim();

            ttt.Clear();
            rowItem = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("itno", itno);
                cmd.CommandText = " Select * from item where itno=(@itno) or itnoudf=(@itno)";
                da.Fill(ttt);
                if (ttt.Rows.Count == 0) itno = "";
                else
                {
                    rowItem = ttt.Rows[0];
                }
            }

            if (rowItem == null)
            {
                e.Cancel = true;
                MessageBox.Show("產品編號輸入錯誤!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbItNo.SelectAll();
                return;
            }

            //將產品編號或是自定編號,置換成產品編號
            tbItNo.Text = itno = rowItem["itno"].ToString().Trim();
            var itpkgqty = tbItPkgQty.Text.ToDecimal("f" + Common.Q);

            if (lendtemp.AsEnumerable().Any(r => r["itno"].ToString().Trim() == itno && r["itpkgqty"].ToDecimal() == itpkgqty) == false)
            {
                e.Cancel = true;
                var msg = "產品編號:" + itno + Environment.NewLine;
                msg += "包裝數量:" + itno + Environment.NewLine;
                MessageBox.Show(msg + "借出單據找不到此筆資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbItNo.SelectAll();
                return;
            }

            lendtemp.DefaultView.Sort = (tbSort.Text == "1") ? "ledate ASC,leno ASC" : "ledate DESC,leno DESC";
            lendtemp.DefaultView.RowFilter = "ItNo = '" + itno + "' And Itpkgqty = " + itpkgqty;
            if (tbAuto.Text == "1")
            {
                mode2();
            }
            else
            {
                //手動
            }
        }

        private void tbQty_Validating(object sender, CancelEventArgs e)
        {
            if (tbQty.ReadOnly) return;
            if (gridCancel.Focused) return;

            if (tbQty.Text.ToDecimal() < 0)
            {
                e.Cancel = true;
                MessageBox.Show("借出轉銷貨數量不可小於零!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbQty.SelectAll();
                return;
            }

            if (tbQty.Text.ToDecimal() == 0)
            {
                tbItNo.Focus();
                return;
            }
            //
            var qty = tbQty.Text.ToDecimal();
            var lendqty = lendtemp.DefaultView.OfType<DataRowView>().Sum(v => v["qtynotout"].ToDecimal());
            if (qty > lendqty)
            {
                e.Cancel = true;
                MessageBox.Show("您輸入的數量大於借出總量( " + lendqty.ToDecimal("f" + Common.Q) + " ), 請確定後重新輸入!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbQty.SelectAll();
                return;
            }

            var itno = tbItNo.Text.Trim();
            var itpkgqty = tbItPkgQty.Text.ToDecimal();
            var qtynotout = 0M;

            var rows = lendtemp.DefaultView.OfType<DataRowView>().ToList();
            for (int i = 0; i < rows.Count; i++)
            {
                qtynotout = rows[i]["qtynotout"].ToDecimal("f" + Common.Q);
                if (qty == 0)
                {
                    rows[i]["tempQty"] = qtynotout;
                    rows[i]["勾選"] = "";
                    continue;
                }

                if (qty - qtynotout >= 0)
                {
                    rows[i]["tempQty"] = 0;
                    rows[i]["勾選"] = "V";
                    qty -= qtynotout;

                }
                else
                {
                    rows[i]["tempQty"] = (qtynotout - qty).ToDecimal();
                    rows[i]["勾選"] = "V";
                    qty = 0;
                }
                dataGridViewT1.InvalidateRow(i);

            }
            //判斷產品編號是否重複
            var rw = dtSaleD.AsEnumerable().FirstOrDefault(r => r["itno"].ToString().Trim() == itno && r["itpkgqty"].ToDecimal() == itpkgqty);
            if (rw == null)
            {
                var unit = lendtemp.DefaultView[0]["itunit"].ToString().Trim();
                GridSaleDAddRows(unit);
                dataGridViewT2.Invalidate();
            }
            else
            {
                rw["pqty"] = tbQty.Text.ToDecimal("f" + Common.Q);
                rw["qty"] = tbQty.Text.ToDecimal("f" + Common.Q);
                dataGridViewT2.Invalidate();
            }

            lendtemp.DefaultView.RowFilter = "1=0";
            mode1();
            DefaultText();
        }

        void DefaultText()
        {
            tbSort.Text = "1";
            tbAuto.Text = "1";
            tbItPkgQty.Text = (1M).ToString("f" + Common.Q);
            tbQty.Text = (0M).ToString("f" + Common.Q);
            tbItNo.Clear();
            tbItNo.Focus();
        }

        void mode1()
        {
            tbSort.AllowGrayBackColor = tbAuto.AllowGrayBackColor = tbItPkgQty.AllowGrayBackColor = false;
            Application.DoEvents();
            tbSort.ReadOnly = tbAuto.ReadOnly = tbItPkgQty.ReadOnly = false;
            gridDelete.Enabled = true;
        }

        void mode2()
        {
            tbSort.AllowGrayBackColor = tbAuto.AllowGrayBackColor = tbItPkgQty.AllowGrayBackColor = true;
            Application.DoEvents();
            tbSort.ReadOnly = tbAuto.ReadOnly = tbItPkgQty.ReadOnly = true;
            gridDelete.Enabled = false;
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dtSaleD.Rows.Count == 0) return;
            var index = dataGridViewT2.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            var itno = dtSaleD.Rows[index]["itno"].ToString().Trim();
            var itpkgqty = dtSaleD.Rows[index]["itpkgqty"].ToDecimal();
            lendtemp.AsEnumerable().Where(r =>
                   r["itno"].ToString().Trim() == itno
                && r["itpkgqty"].ToDecimal() == itpkgqty
                && r["勾選"].ToString().Length > 0).ToList().ForEach(rw =>
                {
                    rw["勾選"] = "";
                    rw["tempQty"] = rw["qtynotout"];
                });

            dtSaleD.Rows.RemoveAt(index);
            if (dtSaleD.Rows.Count > 0)
            {
                index = dtSaleD.Rows.Count - 1;
                dataGridViewT2.CurrentCell = dataGridViewT2[this.產品編號.Name, index];
                dataGridViewT2.Rows[index].Selected = true;
            }
            dataGridViewT2.Invalidate();

        }

        private void gridCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gridGet_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void tbItNo_DoubleClick(object sender, EventArgs e)
        {
           xe.Open<JBS.JS.Item>(sender);
        }







    }
}
