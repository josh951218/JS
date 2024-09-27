using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.subMenuFm_2;
using S_61.SOther;

namespace S_61.subMenuFm_1
{
    public partial class FrmOrderToFordb : Formbase
    {
        JBS.JS.xEvents xe;
        public DataTable table = new DataTable();
        public DataTable ordertable = new DataTable();
        string ReportFileName;
        string ReportPath;
        SqlTransaction tran;

        public FrmOrderToFordb()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmOrderToFordb_Load(object sender, EventArgs e)
        {
            ReportFileName = "客戶訂單採購明細表";

            r2.SetUserDefineRpt("客戶訂單採購明細表_自定一.rpt");
            r3.SetUserDefineRpt("客戶訂單採購明細表_自定二.rpt");

            table.DefaultView.Sort = "產品編號";
            dataGridViewT1.DataSource = table;
            dataGridViewT1.ReadOnly = false;
            rd1.Checked = r1.Checked = true;
            this.品名規格.ReadOnly = this.單位.ReadOnly = this.現有庫存量.ReadOnly = true;
            this.訂單需求量.ReadOnly = this.採購未交量.ReadOnly = this.已採購訂單.ReadOnly = true;
            this.入庫數量.ReadOnly = this.不足數量.ReadOnly = this.採購建議數量.ReadOnly = true;
            this.安全存量.ReadOnly = this.廠商簡稱.ReadOnly = this.產品編號.ReadOnly = true;
            this.現有庫存量.DefaultCellStyle.Format = "f" + Common.Q;
            this.訂單需求量.DefaultCellStyle.Format = "f" + Common.Q;
            this.採購未交量.DefaultCellStyle.Format = "f" + Common.Q;
            this.已採購訂單.DefaultCellStyle.Format = "f" + Common.Q;
            this.入庫數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.不足數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.安全存量.DefaultCellStyle.Format = "f" + Common.Q;
            this.採購建議數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.採購數量.Set庫存數量小數();
        }

        RPT paramsInit()
        {
            if (r1.Checked)
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + "_標準表.rpt";
            else if (r2.Checked)
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + "_自定一.rpt";
            else
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + "_自定二.rpt";

            RPT rp = new RPT(table, ReportPath);
            rp.office = "類別銷售報表_明細表";
            //制表日期
            rp.lobj.Add(new string[] { "制表日期", Date.GetDateTime(Common.User_DateTime, true) });

            //單行註腳
            if (this.FindControl("groupBoxT2") != null)
            {
                string txtend;
                if (rd1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (rd2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (rd3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (rd4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (rd5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else txtend = "";
                rp.lobj.Add(new string[] { "txtend", txtend });
            }
            return rp;
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            ItName.Text = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
            ItNo.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                i.Cells["採購數量"].Value = "0";
            }
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                i.Cells["採購數量"].Value = i.Cells["訂單需求量"].Value.ToDecimal() - i.Cells["現有庫存量"].Value.ToDecimal();
            }
        }

        private void btnBrowT3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                i.Cells["採購數量"].Value = i.Cells["訂單需求量"].Value.ToDecimal();
            }
        }

        private void btnBrowT4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                i.Cells["採購數量"].Value = i.Cells["不足數量"].Value.ToDecimal();
            }
        }

        private void btnBrowT5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                i.Cells["採購數量"].Value = i.Cells["採購建議數量"].Value.ToDecimal();
            }
        }

        private void btnBrowT6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                i.Cells["廠商編號"].Value = "";
                i.Cells["廠商簡稱"].Value = "";
            }
        }

        private void btnBrowT7_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.Rows.Count > 0)
            {
                FrmSale_Stock frm = new subMenuFm_2.FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrowT8_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmOrderToFordbrow())
            {
                frm.ordertable = ordertable.Copy();
                frm.ShowDialog();

                switch (frm.DialogResult)
                {
                    case DialogResult.Cancel:
                        ordertable = frm.ordertable.Copy();
                        break;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.ToString().StartsWith("F2") && keyData.ToString().EndsWith("Shift"))
            {
                btnBrowT1.Focus();
                btnBrowT1.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F3") && keyData.ToString().EndsWith("Shift"))
            {
                btnBrowT2.Focus();
                btnBrowT2.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                btnBrowT3.Focus();
                btnBrowT3.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F5") && keyData.ToString().EndsWith("Shift"))
            {
                btnBrowT4.Focus();
                btnBrowT4.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F6") && keyData.ToString().EndsWith("Shift"))
            {
                btnBrowT5.Focus();
                btnBrowT5.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F7") && keyData.ToString().EndsWith("Shift"))
            {
                btnBrowT6.Focus();
                btnBrowT6.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F8") && keyData.ToString().EndsWith("Shift"))
            {
                btnBrowT7.Focus();
                btnBrowT7.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift"))
            {
                btnBrowT8.Focus();
                btnBrowT8.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F10") && keyData.ToString().EndsWith("Shift"))
            {
                btnBrowT9.Focus();
                btnBrowT9.PerformClick();
            }
            else if (keyData == Keys.F11)
            {
                btnExit.Focus();
                btnExit.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            if (dataGridViewT1.CurrentCell.OwningColumn.Name == "廠商編號")
            {
                using (var frm = new FrmFactb())
                {
                    frm.TSeekNo = dataGridViewT1.EditingControl.Text;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        xe.Validate<JBS.JS.Fact>(frm.TResult, row =>
                        {
                            dataGridViewT1.EditingControl.Text = row["fano"].ToString();
                            dataGridViewT1["廠商簡稱", e.RowIndex].Value = row["faname1"].ToString();
                        });
                    }
                }
            }
            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            if (dataGridViewT1.CurrentCell.OwningColumn.Name == "廠商編號")
            {
                try
                {
                    if (dataGridViewT1.EditingControl == null) return;
                    if (dataGridViewT1.EditingControl.Text.ToString() == "")
                    {
                        dataGridViewT1.EditingControl.Text = "";
                        dataGridViewT1["廠商簡稱", e.RowIndex].Value = "";
                        return;
                    }

                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        string str = "SELECT * FROM Fact WHERE FaNo=@fano COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlCommand cmd = new SqlCommand(str, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fano", dataGridViewT1.EditingControl.Text);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    dataGridViewT1["廠商簡稱", e.RowIndex].Value = reader["FaName1"].ToString();
                                }
                                else
                                {
                                    e.Cancel = true;
                                    using (var frm = new FrmFactb())
                                    {
                                        frm.TSeekNo = dataGridViewT1.EditingControl.Text;

                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            xe.Validate<JBS.JS.Fact>(frm.TResult, row =>
                                            {
                                                if (dataGridViewT1.EditingControl != null)
                                                    dataGridViewT1.EditingControl.Text = row["fano"].ToString();
                                                dataGridViewT1["廠商簡稱", e.RowIndex].Value = row["faname1"].ToString();
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    MessageBox.Show(ex.ToString());
                }
            }
            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnBrowT9_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.RowCount <= 0)
            {
                MessageBox.Show("廠商編號不能空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridViewT1["廠商編號", dataGridViewT1.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim() == "")
            {
                MessageBox.Show("廠商編號不能空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var faid = dataGridViewT1["廠商編號", dataGridViewT1.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim();
            var samefa = table.AsEnumerable().Where(r => r["廠商編號"].ToString().Trim() == faid && r["採購數量"].ToDecimal() > 0);

            if (samefa.Count() == 0)
            {
                MessageBox.Show("已選取之採購數量都為0無法轉入採購單！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("是否轉入採購單", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) return;

                string FoNo = "", strFoNo = "";
                string FoDate = Date.GetDateTime(Common.User_DateTime, false);
                try
                {
                    string strDate = "";
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        strDate = Date.ChangeDateForSN(FoDate);
                        string sql = "select fono from ford where fono like @date +'%' order by fono desc";
                        DataTable fanotable = new DataTable();
                        List<DataRow> list = new List<DataRow>();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("date", strDate);
                        cmd.CommandText = sql;
                        SqlDataAdapter dd = new SqlDataAdapter(cmd);
                        string Countgano = "";
                        dd.Fill(fanotable);
                        if (fanotable.Rows.Count > 0)
                        {
                            list = fanotable.AsEnumerable().ToList();
                            Countgano = fanotable.Rows[0]["fono"].ToString();
                        }
                        else
                        {
                            Countgano = strDate;
                        }
                        int C = 0;
                        switch (Common.Sys_NoAdd)
                        {
                            case 1:
                                Countgano = Countgano.PadRight(11, '0');
                                int.TryParse(Countgano.Substring(7), out C);
                                break;
                            case 2:
                                Countgano = Countgano.PadRight(11, '0');
                                int.TryParse(Countgano.Substring(7), out C);
                                break;
                            case 3:
                                Countgano = Countgano.PadRight(12, '0');
                                int.TryParse(Countgano.Substring(8), out C);
                                break;
                            case 4:
                                Countgano = Countgano.PadRight(12, '0');
                                int.TryParse(Countgano.Substring(8), out C);
                                break;
                        }
                        bool isRepeat = true;
                        do
                        {
                            C++;
                            if (C == 10000)
                            {
                                C = 1;
                                Countgano = C.ToString();
                                Countgano = Countgano.PadLeft(4, '0');
                                strFoNo = strDate + Countgano;
                                if (list.Find(r => r.Field<string>("FoNo") == strFoNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                            else
                            {
                                Countgano = C.ToString();
                                Countgano = Countgano.PadLeft(4, '0');
                                strFoNo = strDate + Countgano;
                                if (list.Find(r => r.Field<string>("FoNo") == strFoNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                        } while (isRepeat);
                        FoNo = strFoNo;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    FoNo = "";
                }

                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();

                        DataTable fa = new DataTable();
                        string sqlstr = "select * from fact where fano=@fano";
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fano", dataGridViewT1["廠商編號", dataGridViewT1.CurrentCell.RowIndex].Value.ToString().Trim());
                        cmd.CommandText = sqlstr;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(fa);

                        DataTable xa = new DataTable();
                        sqlstr = "select * from xa01 where xa1no=N'TWD'";
                        sda = new SqlDataAdapter(sqlstr, cn);
                        sda.Fill(xa);

                        var x3no = fa.Rows[0]["fax3no"].ToString().Trim();
                        var rate = 0M;
                        using (var db = new JBS.xSQL())
                        {
                            var sql = "select x3rate from xx03 where x3no=@x3no";
                            var obj = db.ExecuteScalar(sql, spc => spc.AddWithValue("x3no", x3no));
                            rate = obj.ToDecimal();
                        }

                        DataTable itt = new DataTable();
                        sqlstr = "select * from item";
                        sda = new SqlDataAdapter(sqlstr, cn);
                        sda.Fill(itt);

                        DataTable bomt = new DataTable();
                        sqlstr = "select * from bomd";
                        sda = new SqlDataAdapter(sqlstr, cn);
                        sda.Fill(bomt);

                        tran = cn.BeginTransaction();
                        cmd.Transaction = tran;
                        DataTable temp = new DataTable();
                        if (samefa.Count() > 0) temp = samefa.CopyToDataTable();
                        int no = 0;
                        //decimal totmny = 0;
                        foreach (DataRow dr in temp.Rows)
                        {
                            no++;
                            DataTable it = new DataTable();
                            var nowit = itt.AsEnumerable().Where(r => r["itno"].ToString().Trim() == dr["產品編號"].ToString().Trim());
                            if (nowit.Count() > 0) it = nowit.CopyToDataTable();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fono", FoNo);
                            cmd.Parameters.AddWithValue("fodate", Date.ToTWDate(Date.GetDateTime(Common.User_DateTime, false)));
                            cmd.Parameters.AddWithValue("fodate1", Date.ToUSDate(Date.GetDateTime(Common.User_DateTime, false)));
                            cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(Date.GetDateTime(Common.User_DateTime, false)));
                            cmd.Parameters.AddWithValue("esdate1", Date.ToUSDate(Date.GetDateTime(Common.User_DateTime, false)));
                            cmd.Parameters.AddWithValue("fqno", "");
                            cmd.Parameters.AddWithValue("qtynotin", dr["採購數量"].ToDecimal("f" + Common.Q));
                            cmd.Parameters.AddWithValue("qtyin", 0);
                            cmd.Parameters.AddWithValue("fano", dr["廠商編號"].ToString().Trim());
                            cmd.Parameters.AddWithValue("emno", "");
                            cmd.Parameters.AddWithValue("xa1no", xa.Rows[0]["Xa1No"].ToString().Trim());
                            cmd.Parameters.AddWithValue("xa1par", 1.0000);
                            cmd.Parameters.AddWithValue("itno", it.Rows[0]["itno"]);

                            cmd.Parameters.AddWithValue("itname", dr["品名規格"].ToString());//代使用者編輯的品名規格 不去產品建檔重撈
                            cmd.Parameters.AddWithValue("ittrait", it.Rows[0]["ittrait"]);
                            cmd.Parameters.AddWithValue("itunit", it.Rows[0]["itunit"]);
                            cmd.Parameters.AddWithValue("punit", it.Rows[0]["punit"]);
                            cmd.Parameters.AddWithValue("itpkgqty", 1);
                            cmd.Parameters.AddWithValue("pqty", dr["採購數量"].ToDecimal("f" + Common.Q));
                            cmd.Parameters.AddWithValue("qty", dr["採購數量"].ToDecimal("f" + Common.Q));

                            var price = it.Rows[0]["itbuypri"].ToDecimal("f" + Common.MF);
                            var qty = dr["採購數量"].ToDecimal("f" + Common.Q);
                            var taxprice = SetRow_TaxPrice(price, x3no);

                            cmd.Parameters.AddWithValue("price", price);
                            cmd.Parameters.AddWithValue("priceb", price);
                            cmd.Parameters.AddWithValue("prs", 1.000);
                            cmd.Parameters.AddWithValue("rate", rate);
                            cmd.Parameters.AddWithValue("taxprice", taxprice);
                            cmd.Parameters.AddWithValue("taxpriceb", taxprice);
                            cmd.Parameters.AddWithValue("mny", SetRow_Mny(qty, price, taxprice));
                            cmd.Parameters.AddWithValue("mnyb", SetRow_Mny(qty, price, taxprice));
                            cmd.Parameters.AddWithValue("memo", "");
                            cmd.Parameters.AddWithValue("bomid", FoNo + no.ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", no.ToString().Trim());
                            cmd.Parameters.AddWithValue("recordno", no.ToString().Trim());
                            cmd.Parameters.AddWithValue("itdesp1", it.Rows[0]["itdesp1"].ToString());
                            cmd.Parameters.AddWithValue("itdesp2", it.Rows[0]["itdesp2"].ToString());
                            cmd.Parameters.AddWithValue("itdesp3", it.Rows[0]["itdesp3"].ToString());
                            cmd.Parameters.AddWithValue("itdesp4", it.Rows[0]["itdesp4"].ToString());
                            cmd.Parameters.AddWithValue("itdesp5", it.Rows[0]["itdesp5"].ToString());
                            cmd.Parameters.AddWithValue("itdesp6", it.Rows[0]["itdesp6"].ToString());
                            cmd.Parameters.AddWithValue("itdesp7", it.Rows[0]["itdesp7"].ToString());
                            cmd.Parameters.AddWithValue("itdesp8", it.Rows[0]["itdesp8"].ToString());
                            cmd.Parameters.AddWithValue("itdesp9", it.Rows[0]["itdesp9"].ToString());
                            cmd.Parameters.AddWithValue("itdesp10", it.Rows[0]["itdesp10"].ToString());


                            cmd.CommandText = "insert into fordd("
                                + " fono,fodate,fodate1,esdate,esdate1,fqno"
                                + " ,qtynotin,qtyin"

                                + " ,fano,emno,xa1no,xa1par,itno,itname,ittrait,itunit,punit,itpkgqty"
                                + " ,pqty,qty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo"
                                + " ,bomid,bomrec,recordno"
                                + " ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                                + " ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10)values("
                                + " @fono,@fodate,@fodate1,@esdate,@esdate1,@fqno"
                                + " ,@qtynotin,@qtyin"
                                + " ,@fano,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@punit,@itpkgqty"
                                + " ,@pqty,@qty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo"
                                + " ,@bomid,@bomrec,@recordno"
                                + " ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                                + " ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10)";
                            cmd.ExecuteNonQuery();

                            //totmny += (dr["採購數量"].ToDecimal("f" + Common.Q) * it.Rows[0]["itbuypri"].ToDecimal("f" + Common.MF)).ToDecimal("f" + Common.TPF);

                            if (it.Rows[0]["ittrait"].ToString().Trim() != "3")
                            {
                                DataTable bom = new DataTable();
                                var nowbom = bomt.AsEnumerable().Where(r => r["boitno"].ToString().Trim() == dr["產品編號"].ToString().Trim());
                                if (nowbom.Count() > 0)
                                {
                                    bom = nowbom.CopyToDataTable();

                                    for (int i = 0; i < bom.Rows.Count; i++)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.AddWithValue("fono", FoNo);
                                        cmd.Parameters.AddWithValue("bomid", FoNo + no.ToString().PadLeft(10, '0'));
                                        cmd.Parameters.AddWithValue("bomrec", no.ToString().Trim());
                                        cmd.Parameters.AddWithValue("itno", bom.Rows[i]["itno"].ToString());
                                        cmd.Parameters.AddWithValue("itname", bom.Rows[i]["itname"].ToString());
                                        cmd.Parameters.AddWithValue("itunit", bom.Rows[i]["itunit"].ToString());
                                        cmd.Parameters.AddWithValue("itqty", bom.Rows[i]["itqty"].ToString());
                                        cmd.Parameters.AddWithValue("itpareprs", bom.Rows[i]["itpareprs"].ToString());
                                        cmd.Parameters.AddWithValue("itpkgqty", bom.Rows[i]["itpkgqty"].ToString());
                                        cmd.Parameters.AddWithValue("itrec", (i + 1));
                                        cmd.Parameters.AddWithValue("itprice", bom.Rows[i]["itprice"].ToString());
                                        cmd.Parameters.AddWithValue("itprs", 1);
                                        cmd.Parameters.AddWithValue("itmny", bom.Rows[i]["itmny"].ToString());
                                        cmd.Parameters.AddWithValue("itnote", bom.Rows[i]["itnote"].ToString());
                                        cmd.CommandText = "insert into fordbom ("
                                            + "fono,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"

                                            + "itprice,itprs,itmny,itnote)values("
                                            + "@fono,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,"
                                            + "@itprice,@itprs,@itmny,@itnote)";

                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", FoNo);
                        cmd.CommandText = "Select * from fordd where fono = @fono";
                        DataTable tFordd = new DataTable();
                        decimal[] mnys;
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tFordd);
                            mnys = SetAllMny(tFordd, x3no);
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", FoNo);
                        cmd.Parameters.AddWithValue("fodate", Date.ToTWDate(Date.GetDateTime(Common.User_DateTime, false)));
                        cmd.Parameters.AddWithValue("fodate1", Date.ToUSDate(Date.GetDateTime(Common.User_DateTime, false)));
                        cmd.Parameters.AddWithValue("fqno", "");
                        cmd.Parameters.AddWithValue("fano", fa.Rows[0]["FaNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("faname1", fa.Rows[0]["FaName1"].ToString().Trim());
                        cmd.Parameters.AddWithValue("faname2", fa.Rows[0]["FaName2"].ToString().Trim());
                        cmd.Parameters.AddWithValue("fatel1", fa.Rows[0]["FaTel1"].ToString().Trim());
                        cmd.Parameters.AddWithValue("faper1", fa.Rows[0]["FaPer1"].ToString().Trim().GetUTF8(10));
                        cmd.Parameters.AddWithValue("emno", "");
                        cmd.Parameters.AddWithValue("emname", "");
                        cmd.Parameters.AddWithValue("xa1no", xa.Rows[0]["Xa1No"].ToString().Trim());
                        cmd.Parameters.AddWithValue("xa1name", xa.Rows[0]["Xa1Name"].ToString().Trim());
                        cmd.Parameters.AddWithValue("xa1par", 1.0000);
                        cmd.Parameters.AddWithValue("taxmnyb", mnys[0]);
                        cmd.Parameters.AddWithValue("taxmny", mnys[0]);
                        cmd.Parameters.AddWithValue("x3no", x3no);
                        cmd.Parameters.AddWithValue("rate", rate);
                        cmd.Parameters.AddWithValue("tax", mnys[1]);
                        cmd.Parameters.AddWithValue("taxb", mnys[1]);
                        cmd.Parameters.AddWithValue("totmny", mnys[2]);
                        cmd.Parameters.AddWithValue("totmnyb", mnys[2]);
                        cmd.Parameters.AddWithValue("fopayment", "");
                        cmd.Parameters.AddWithValue("fomemo", "");
                        cmd.Parameters.AddWithValue("recordno", samefa.Count());
                        cmd.Parameters.AddWithValue("spno", "");
                        cmd.Parameters.AddWithValue("spname", "");
                        cmd.Parameters.AddWithValue("fooverflag", 0);
                        cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("appscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
                        cmd.CommandText = "insert into ford ("
                        + " fono,fodate,fodate1,fqno"

                        + " ,fano,faname1,faname2,fatel1,faper1,emno,emname,xa1no,xa1name,xa1par"
                        + " ,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,fopayment,fomemo"
                        + " ,recordno,spno,spname,fooverflag,appdate,edtdate,appscno,edtscno)values("
                        + " @fono,@fodate,@fodate1,@fqno"
                        + " ,@fano,@faname1,@faname2,@fatel1,@faper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
                        + " ,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@fopayment,@fomemo"
                        + " ,@recordno,@spno,@spname,@fooverflag,@appdate,@edtdate,@appscno,@edtscno)";
                        cmd.ExecuteNonQuery();
                        //將訂單表示為已轉採購(cmd, ordertable);



                        tran.Commit();
                        tran.Dispose();
                        cmd.Dispose();

                    }

                    var differentfa = table.AsEnumerable().Where(r => r["廠商編號"].ToString().Trim() != dataGridViewT1["廠商編號", dataGridViewT1.CurrentCell.RowIndex].Value.ToString().Trim());
                    if (differentfa.Count() > 0)
                    {
                        table = differentfa.CopyToDataTable();
                        dataGridViewT1.DataSource = table;
                    }
                    else
                    {
                        dataGridViewT1.DataSource = null;
                    }

                    using (FrmFord frm = new FrmFord(FoNo))
                    {
                        frm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        private void 將訂單表示為已轉採購(SqlCommand cmd, DataTable ordertable)
        {
            foreach (DataRow i in ordertable.Rows)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("orno", i["訂單編號"].ToString().Trim());
                cmd.CommandText = "update [order] set ortrnflag='1' where orno=@orno";
                cmd.ExecuteNonQuery();
            }
        }

        //帳款計算
        decimal SetRow_TaxPrice(decimal price, string x3no)
        {
            var taxprice = price * 1;
            if (x3no == "2")
            {
                return (taxprice / (1 + Common.Sys_Rate)).ToDecimal("f6");
            }
            else
            {
                return taxprice.ToDecimal("f6");
            }
        }
        decimal SetRow_Mny(decimal qty, decimal price, decimal taxprice)
        {
            var mny = qty * taxprice;

            return mny.ToDecimal("f" + Common.TPF);
        }
        decimal[] SetAllMny(DataTable tFordd, string x3no)
        {
            var taxmny = 0M;
            var tax = 0M;
            var totmny = 0M;
            var sum = 0M;

            sum = tFordd.AsEnumerable().Sum(r => r["Mny"].ToDecimal("f" + Common.TPS)).ToDecimal("f" + Common.MST);

            if (x3no.ToInteger() == 1)
            {
                tax = (sum * Common.Sys_Rate).ToDecimal("f" + Common.TS);
                taxmny = sum;
                totmny = (sum + tax).ToDecimal("f" + Common.MST);
            }
            else if (x3no.ToInteger() == 2)
            {
                totmny = tFordd.AsEnumerable().Sum(r => r["pqty"].ToDecimal("f" + Common.Q) * r["price"].ToDecimal("f" + Common.MS) * r["prs"].ToDecimal()).ToDecimal("f" + Common.MST);
                tax = ((totmny / (1 + Common.Sys_Rate)) * Common.Sys_Rate).ToDecimal("f" + Common.TS);
                taxmny = (totmny - tax).ToDecimal("f" + Common.MST);
            }
            else if (x3no.ToInteger() == 3 || x3no.ToInteger() == 4)
            {
                tax = 0;
                totmny = taxmny = sum.ToDecimal("f" + Common.MST);
            }
            return new decimal[] { taxmny, tax, totmny };
        }



        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit().Print();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            paramsInit().PreView();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit().Word();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit().Excel();
        }
    }
}
