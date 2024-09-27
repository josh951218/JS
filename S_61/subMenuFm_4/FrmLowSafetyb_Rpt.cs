using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.subMenuFm_1;
using S_61.subMenuFm_2;

namespace S_61.subMenuFm_4
{
    public partial class FrmLowSafetyb_Rpt : Formbase
    {
        JBS.JS.xEvents xe;
        public DataTable dtD = new DataTable();
        string ReportFileName;
        SqlTransaction tran;
        string ReportPath;

        public FrmLowSafetyb_Rpt()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmLowSafetyb_Rpt_Load(object sender, EventArgs e)
        {
            ReportFileName = "庫存低於安全存量表";
            rd2.SetUserDefineRpt("庫存低於安全存量表_自定一.rpt");
            rd3.SetUserDefineRpt("庫存低於安全存量表_自定二.rpt");

            ToolTip tip = new ToolTip();
            tip.SetToolTip(rd1, ReportFileName + "_標準報表.rpt");

            _Date.Text = Date.GetDateTime(Common.User_DateTime, true);
            dataGridViewT1.DataSource = dtD;
            dataGridViewT1.ReadOnly = false;

            this.產品編號.ReadOnly = this.品名規格.ReadOnly = this.單位.ReadOnly = this.庫存倉數量.ReadOnly = true;
            this.借入倉數量.ReadOnly = this.加工倉數量.ReadOnly = this.借出倉數量.ReadOnly = this.庫存總數量.ReadOnly = true;
            this.安全存量.ReadOnly = this.採購建議量.ReadOnly = this.廠商簡稱.ReadOnly = true;

            this.庫存倉數量.Set庫存數量小數();
            this.借入倉數量.Set庫存數量小數();
            this.加工倉數量.Set庫存數量小數();
            this.借出倉數量.Set庫存數量小數();
            this.庫存總數量.Set庫存數量小數();
            this.安全存量.Set庫存數量小數();
            this.採購建議量.Set庫存數量小數();
            this.實際採購量.Set庫存數量小數();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (btnExit.Focused) return;
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;

            if (dataGridViewT1.CurrentCell.OwningColumn.Name == this.廠商編號.Name)
            {
                var fano = dataGridViewT1[this.廠商編號.Name, e.RowIndex].EditedFormattedValue.ToString().Trim();
                if (fano.Length == 0)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = "";

                    dtD.Rows[e.RowIndex]["廠商編號"] = "";
                    dtD.Rows[e.RowIndex]["廠商簡稱"] = "";
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                xe.DataGridViewValidateOpen<JBS.JS.Fact>(sender, e, dtD, row =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = row["fano"].ToString();

                    dtD.Rows[e.RowIndex]["廠商編號"] = row["fano"].ToString();
                    dtD.Rows[e.RowIndex]["廠商簡稱"] = row["faname1"].ToString();
                });

            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnExit.Focused) return;
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;

            if (dataGridViewT1.CurrentCell.OwningColumn.Name == this.廠商編號.Name)
            {
                var fano = dataGridViewT1[this.廠商編號.Name, e.RowIndex].EditedFormattedValue.ToString().Trim();

                xe.DataGridViewOpen<JBS.JS.Fact>(sender, e, dtD, row =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = row["fano"].ToString();

                    dtD.Rows[e.RowIndex]["廠商編號"] = row["fano"].ToString();
                    dtD.Rows[e.RowIndex]["廠商簡稱"] = row["faname1"].ToString();
                });

            }
        }

        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows.Count == 0) return;
            ItName.Text = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
            ItNo.Text = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
        }

        private void btnBrowT4_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.Rows.Count > 0)
            {
                FrmSale_Stock frm = new subMenuFm_2.FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrowT3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dtD.Rows[i]["廠商編號"] = "";
                dtD.Rows[i]["廠商簡稱"] = "";
                dataGridViewT1.InvalidateRow(i);
            }
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dtD.Rows[i]["實際採購量"] = 0;
                dataGridViewT1.InvalidateRow(i);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dtD.Excel匯出並開啟(this.Text);
            }

            switch (keyData)
            {
                case Keys.F2:
                    btnBrowT1.Focus();
                    btnBrowT1.PerformClick();
                    break;
                case Keys.F3:
                    btnBrowT2.Focus();
                    btnBrowT2.PerformClick();
                    break;
                case Keys.F4:
                    btnBrowT3.Focus();
                    btnBrowT3.PerformClick();
                    break;
                case Keys.F5:
                    btnBrowT4.Focus();
                    btnBrowT4.PerformClick();
                    break;
                case Keys.F6:
                    btnBrowT5.Focus();
                    btnBrowT5.PerformClick();
                    break;
                case Keys.F11:
                    btnExit.Focus();
                    btnExit.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dtD.Rows[i]["實際採購量"] = dtD.Rows[i]["採購建議量"].ToDecimal();
                dataGridViewT1.InvalidateRow(i);
            }
        }

        private void btnBrowT5_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.RowCount <= 0)
            {
                MessageBox.Show("廠商編號不能空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fano = dataGridViewT1["廠商編號", dataGridViewT1.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim();
            if (fano.Length == 0)
            {
                MessageBox.Show("廠商編號不能空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("是否轉入採購單", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) != DialogResult.OK)
                return;

            var samefa = dtD.AsEnumerable().Where(r => r["廠商編號"].ToString().Trim() == fano);
            if (samefa.Count() == 0)
                return;

            //string FoNo = "", strFoNo = "";
            //string FoDate = Date.GetDateTime(Common.User_DateTime, false);
            //try
            //{
            //    string strDate = "";
            //    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            //    {
            //        cn.Open();
            //        strDate = Date.ChangeDateForSN(FoDate);
            //        string sql = "select fono from ford where fono like @strDate + '%' order by fono desc";
            //        DataTable fanotable = new DataTable();
            //        List<DataRow> list = new List<DataRow>();
            //        using (SqlCommand cmd = cn.CreateCommand())
            //        {
            //            cmd.Parameters.AddWithValue("strDate", strDate);
            //            cmd.CommandText = sql;
            //            SqlDataAdapter dd = new SqlDataAdapter(cmd);
            //            string Countgano = "";
            //            dd.Fill(fanotable);
            //            dd.Dispose();
            //            if (fanotable.Rows.Count > 0)
            //            {
            //                list = fanotable.AsEnumerable().ToList();
            //                Countgano = fanotable.Rows[0]["fono"].ToString();
            //            }
            //            else
            //            {
            //                Countgano = strDate;
            //            }
            //            int C = 0;
            //            switch (Common.Sys_NoAdd)
            //            {
            //                case 1:
            //                    Countgano = Countgano.PadRight(11, '0');
            //                    int.TryParse(Countgano.Substring(7), out C);
            //                    break;
            //                case 2:
            //                    Countgano = Countgano.PadRight(11, '0');
            //                    int.TryParse(Countgano.Substring(7), out C);
            //                    break;
            //                case 3:
            //                    Countgano = Countgano.PadRight(12, '0');
            //                    int.TryParse(Countgano.Substring(8), out C);
            //                    break;
            //                case 4:
            //                    Countgano = Countgano.PadRight(12, '0');
            //                    int.TryParse(Countgano.Substring(8), out C);
            //                    break;
            //            }
            //            bool isRepeat = true;
            //            do
            //            {
            //                C++;
            //                if (C == 10000)
            //                {
            //                    C = 1;
            //                    Countgano = C.ToString();
            //                    Countgano = Countgano.PadLeft(4, '0');
            //                    strFoNo = strDate + Countgano;
            //                    if (list.Find(r => r.Field<string>("FoNo") == strFoNo) != null)
            //                        isRepeat = true;
            //                    else
            //                        isRepeat = false;
            //                }
            //                else
            //                {
            //                    Countgano = C.ToString();
            //                    Countgano = Countgano.PadLeft(4, '0');
            //                    strFoNo = strDate + Countgano;
            //                    if (list.Find(r => r.Field<string>("FoNo") == strFoNo) != null)
            //                        isRepeat = true;
            //                    else
            //                        isRepeat = false;
            //                }
            //            } while (isRepeat);
            //            FoNo = strFoNo;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    FoNo = "";
            //}

            TextBoxT FoNo = new TextBoxT();
            FoNo.Name = "FoNo";

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    DataTable fa = new DataTable();
                    string sqlstr = "select * from fact where fano=@fano";

                    SqlCommand scmd = cn.CreateCommand();
                    scmd.Parameters.AddWithValue("fano", fano);
                    scmd.CommandText = sqlstr;
                    SqlDataAdapter sda = new SqlDataAdapter(scmd);
                    sda.Fill(fa);
                    sda.Dispose();
                    scmd.Dispose();

                    DataTable xa = new DataTable();
                    sqlstr = "select * from xa01 where xa1no=N'TWD'";
                    sda = new SqlDataAdapter(sqlstr, cn);
                    sda.Fill(xa);
                    sda.Dispose();

                    DataTable x3 = new DataTable();
                    sqlstr = "select * from xx03 where x3no=N'1'";
                    sda = new SqlDataAdapter(sqlstr, cn);
                    sda.Fill(x3);
                    sda.Dispose();

                    DataTable itt = new DataTable();
                    sqlstr = "select * from item";
                    sda = new SqlDataAdapter(sqlstr, cn);
                    sda.Fill(itt);
                    sda.Dispose();

                    DataTable bomt = new DataTable();
                    sqlstr = "select * from bomd";
                    sda = new SqlDataAdapter(sqlstr, cn);
                    sda.Fill(bomt);
                    sda.Dispose();

                    tran = cn.BeginTransaction();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Transaction = tran;

                    xe.GetPkNumber<JBS.JS.Ford>(cmd, Date.GetDateTime(Common.User_DateTime), ref FoNo);

                    DataTable temp = samefa.CopyToDataTable();
                    int no = 0;
                    decimal totmny = 0;
                    foreach (DataRow dr in temp.Rows)
                    {
                        no++;
                        DataTable it = new DataTable();
                        var nowit = itt.AsEnumerable().Where(r => r["itno"].ToString().Trim() == dr["產品編號"].ToString().Trim());
                        it = nowit.CopyToDataTable();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", FoNo.Text);
                        cmd.Parameters.AddWithValue("fodate", Date.ToTWDate(Date.GetDateTime(Common.User_DateTime, false)));
                        cmd.Parameters.AddWithValue("fodate1", Date.ToUSDate(Date.GetDateTime(Common.User_DateTime, false)));
                        cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(Date.GetDateTime(Common.User_DateTime, false)));
                        cmd.Parameters.AddWithValue("esdate1", Date.ToUSDate(Date.GetDateTime(Common.User_DateTime, false)));
                        cmd.Parameters.AddWithValue("fqno", "");
                        cmd.Parameters.AddWithValue("qtynotin", dr["實際採購量"].ToString().Trim());
                        cmd.Parameters.AddWithValue("qtyin", 0);
                        cmd.Parameters.AddWithValue("fano", dr["廠商編號"].ToString().Trim());
                        cmd.Parameters.AddWithValue("emno", "");
                        cmd.Parameters.AddWithValue("xa1no", xa.Rows[0]["Xa1No"].ToString().Trim());
                        cmd.Parameters.AddWithValue("xa1par", "1.0000");
                        cmd.Parameters.AddWithValue("itno", it.Rows[0]["itno"]);
                        cmd.Parameters.AddWithValue("itname", it.Rows[0]["itname"]);
                        cmd.Parameters.AddWithValue("ittrait", it.Rows[0]["ittrait"]);
                        cmd.Parameters.AddWithValue("itunit", it.Rows[0]["itunit"]);
                        cmd.Parameters.AddWithValue("itpkgqty", "1");
                        cmd.Parameters.AddWithValue("qty", dr["實際採購量"].ToString().Trim());
                        cmd.Parameters.AddWithValue("price", it.Rows[0]["itbuypri"].ToDecimal());
                        cmd.Parameters.AddWithValue("priceb", it.Rows[0]["itbuypri"].ToDecimal());
                        cmd.Parameters.AddWithValue("prs", "1.000");
                        cmd.Parameters.AddWithValue("rate", "0.050");
                        cmd.Parameters.AddWithValue("taxprice", it.Rows[0]["itbuypri"].ToDecimal());
                        cmd.Parameters.AddWithValue("taxpriceb", it.Rows[0]["itbuypri"].ToDecimal());
                        cmd.Parameters.AddWithValue("mny", dr["實際採購量"].ToDecimal() * it.Rows[0]["itbuypri"].ToDecimal());
                        cmd.Parameters.AddWithValue("mnyb", dr["實際採購量"].ToDecimal() * it.Rows[0]["itbuypri"].ToDecimal());
                        cmd.Parameters.AddWithValue("memo", "");
                        cmd.Parameters.AddWithValue("bomid", FoNo.Text + no.ToString().PadLeft(10, '0'));
                        cmd.Parameters.AddWithValue("bomrec", no.ToString().Trim());
                        cmd.Parameters.AddWithValue("recordno", no.ToString().Trim());
                        cmd.Parameters.AddWithValue("itdesp1", "");
                        cmd.Parameters.AddWithValue("itdesp2", "");
                        cmd.Parameters.AddWithValue("itdesp3", "");
                        cmd.Parameters.AddWithValue("itdesp4", "");
                        cmd.Parameters.AddWithValue("itdesp5", "");
                        cmd.Parameters.AddWithValue("itdesp6", "");
                        cmd.Parameters.AddWithValue("itdesp7", "");
                        cmd.Parameters.AddWithValue("itdesp8", "");
                        cmd.Parameters.AddWithValue("itdesp9", "");
                        cmd.Parameters.AddWithValue("itdesp10", "");

                        cmd.CommandText = "insert into fordd("
                            + " fono,fodate,fodate1,esdate,esdate1,fqno"
                            + " ,qtynotin,qtyin"
                            + " ,fano,emno,xa1no,xa1par,itno,itname,ittrait,itunit,itpkgqty"
                            + " ,qty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo"
                            + " ,bomid,bomrec,recordno"
                            + " ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                            + " ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10) values "
                            + " (@fono,@fodate,@fodate1,@esdate,@esdate1,@fqno"
                            + " ,@qtynotin,@qtyin"
                            + " ,@fano,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@itpkgqty"
                            + " ,@qty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo"
                            + " ,@bomid,@bomrec,@recordno"
                            + " ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                            + " ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10) ";
                        cmd.ExecuteNonQuery();

                        totmny += dr["實際採購量"].ToDecimal() * it.Rows[0]["itbuypri"].ToDecimal();

                        if (it.Rows[0]["ittrait"].ToString().Trim() != "3")
                        {
                            DataTable bom = new DataTable();
                            var nowbom = bom.AsEnumerable().Where(r => r["boitno"].ToString().Trim() == dr["產品編號"].ToString().Trim());
                            if (nowbom.Count() > 0)
                            {
                                bom = nowbom.CopyToDataTable();

                                for (int i = 0; i < bom.Rows.Count; i++)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("fono", FoNo.Text);
                                    cmd.Parameters.AddWithValue("bomid", FoNo.Text + no.ToString().PadLeft(10, '0'));
                                    cmd.Parameters.AddWithValue("bomrec", no.ToString().Trim());
                                    cmd.Parameters.AddWithValue("itno", bom.Rows[i]["itno"].ToString());
                                    cmd.Parameters.AddWithValue("itname", bom.Rows[i]["itname"].ToString());
                                    cmd.Parameters.AddWithValue("itunit", bom.Rows[i]["itunit"].ToString());
                                    cmd.Parameters.AddWithValue("itqty", bom.Rows[i]["itqty"].ToString());
                                    cmd.Parameters.AddWithValue("itpareprs", bom.Rows[i]["itpareprs"].ToString());
                                    cmd.Parameters.AddWithValue("itpkgqty", bom.Rows[i]["itpkgqty"].ToString());
                                    cmd.Parameters.AddWithValue("itrec", (i + 1));
                                    cmd.Parameters.AddWithValue("itprice", bom.Rows[i]["itprice"].ToString());
                                    cmd.Parameters.AddWithValue("itprs", "1");
                                    cmd.Parameters.AddWithValue("itmny", bom.Rows[i]["itmny"].ToString());
                                    cmd.Parameters.AddWithValue("itnote", bom.Rows[i]["itnote"].ToString());

                                    cmd.CommandText = "insert into fordbom ("
                                        + "fono,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                                        + "itprice,itprs,itmny,itnote) values "
                                        + "(@fono,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@"
                                        + "itprice,@itprs,@itmny,@itnote)";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fono", FoNo.Text);
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
                    cmd.Parameters.AddWithValue("xa1par", "1.0000");
                    cmd.Parameters.AddWithValue("taxmnyb", totmny);
                    cmd.Parameters.AddWithValue("taxmny", totmny);
                    cmd.Parameters.AddWithValue("x3no", x3.Rows[0]["X3No"].ToString().Trim());
                    cmd.Parameters.AddWithValue("rate", x3.Rows[0]["X3Rate"].ToString().Trim());
                    cmd.Parameters.AddWithValue("tax", totmny * x3.Rows[0]["X3Rate"].ToDecimal());
                    cmd.Parameters.AddWithValue("taxb", totmny * x3.Rows[0]["X3Rate"].ToDecimal());
                    cmd.Parameters.AddWithValue("totmny", (totmny + (totmny * x3.Rows[0]["X3Rate"].ToDecimal())));
                    cmd.Parameters.AddWithValue("totmnyb", (totmny + (totmny * x3.Rows[0]["X3Rate"].ToDecimal())));
                    cmd.Parameters.AddWithValue("fopayment", "");
                    cmd.Parameters.AddWithValue("fomemo", "");
                    cmd.Parameters.AddWithValue("recordno", samefa.Count());
                    cmd.Parameters.AddWithValue("spno", "");
                    cmd.Parameters.AddWithValue("spname", "");
                    cmd.Parameters.AddWithValue("fooverflag", "0");

                    cmd.CommandText = "insert into ford ("
                    + " fono,fodate,fodate1,fqno"
                    + " ,fano,faname1,faname2,fatel1,faper1,emno,emname,xa1no,xa1name,xa1par"
                    + " ,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,fopayment,fomemo"
                    + " ,recordno,spno,spname,fooverflag) values "
                    + " (@fono,@fodate,@fodate1,@fqno"
                    + " ,@fano,@faname1,@faname2,@fatel1,@faper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
                    + " ,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@fopayment,@fomemo"
                    + " ,@recordno,@spno,@spname,@fooverflag)";
                    cmd.ExecuteNonQuery();

                    tran.Commit();
                    tran.Dispose();
                    cmd.Dispose();

                }

                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    if (dtD.Rows[i]["廠商編號"].ToString().Trim() == fano)
                    {
                        dtD.Rows.RemoveAt(i);
                        i--;
                    }
                }

                using (FrmFord frm = new FrmFord(FoNo.Text))
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        RPT paramsInit()
        {
            if (rd1.Checked)
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + "_標準報表.rpt";
            else if (rd2.Checked)
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + "_自定一.rpt";
            else
                ReportPath = Common.reportaddress + "Report\\" + ReportFileName + "_自定二.rpt";

            RPT rp = new RPT(dtD, ReportPath);
            rp.office = "庫存低於安全存量表";
            //制表日期
            rp.lobj.Add(new string[] { "制表日期", Date.GetDateTime(Common.User_DateTime, true) });

            //單行註腳
            if (this.FindControl("groupBoxT2") != null)
            {
                string txtend;
                if (r1.Checked) txtend = Common.dtEnd.Rows[0]["tamemo"].ToString();
                else if (r2.Checked) txtend = Common.dtEnd.Rows[1]["tamemo"].ToString();
                else if (r3.Checked) txtend = Common.dtEnd.Rows[2]["tamemo"].ToString();
                else if (r4.Checked) txtend = Common.dtEnd.Rows[3]["tamemo"].ToString();
                else if (r5.Checked) txtend = Common.dtEnd.Rows[4]["tamemo"].ToString();
                else txtend = "";
                rp.lobj.Add(new string[] { "txtend", txtend });
            }
            return rp;
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit().Word();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit().Excel();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            paramsInit().PreView();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit().Print();
        }

        private void FrmLowSafetyb_Rpt_Shown(object sender, EventArgs e)
        {
            btnBrowT1.Focus();
        }



    }
}
