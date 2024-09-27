using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61.subMenuFm_2;

namespace S_61.SOther
{
    public partial class FrmBom : Formbase
    {
        JBS.JS.Bom jBom;
        List<TextBoxbase> list;
        DataTable dtD = new DataTable();
        List<Control> Writes;

        public FrmBom()
        {
            InitializeComponent();
            this.jBom = new JBS.JS.Bom();
            this.list = this.getEnumMember();

            Writes = new List<Control> { BoItNo, BoItName, BoTotQty, BoTotMny, BoMemo };

            //數量
            BoTotQty.FirstNum = Common.nFirst;
            BoTotQty.LastNum = Common.Q;
            //金額
            BoTotMny.FirstNum = Common.nFirst;
            BoTotMny.LastNum = Common.MF;

            this.標準用量.Set庫存數量小數();
            this.母件比例.FirstNum = 11;
            this.母件比例.LastNum = 4;
            this.母件比例.DefaultCellStyle.Format = "f4";
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.單價.Set進貨單價小數();
            this.金額.Set進貨單價小數();
            this.包裝數量.Set庫存數量小數();

            this.單價.Visible = this.折數.Visible = this.金額.Visible = Common.User_ShopPrice;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            this.BoItName.MaxLength = Common.Sys_ItNameLenth;
            BoTotMny.Visible = Common.User_ShopPrice;
        }

        private void FrmBom_Load(object sender, EventArgs e)
        {
            using (var db = new JBS.xSQL())
            {
                var tsql = "Select * from Bomd where 1=0 ";
                db.Fill(tsql, spc => spc.AddWithValue("boitno", 1), ref dtD);
            }

            dataGridViewT1.DataSource = dtD;

            var pk = jBom.Bottom();
            writeToTxt(pk);
        }

        private void FrmBom_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string boitno)
        {
            var result = jBom.LoadData(boitno, row =>
            {
                Writes.ForEach(r =>
                {
                    r.Text = row[r.Name].ToString();
                    if (r is TextBoxNumberT)
                    {
                        r.Text = row[r.Name].ToDecimal().ToString("f" + ((TextBoxNumberT)r).LastNum);
                    }
                });

                dtD.Clear();
                using (var db = new JBS.xSQL())
                {
                    var tsql = "Select * from Bomd where boitno = @boitno ";
                    db.Fill(tsql, spc => spc.AddWithValue("boitno", boitno), ref dtD);
                }

                BoItNo.Tag = BoItNo.Text.Trim();
            });

            if (!result)
            {
                dtD.Clear();
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            var pk = jBom.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jBom.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jBom.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jBom.Bottom();
            writeToTxt(pk);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            dtD.Clear();
            BoItNo.Focus();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (BoItNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            BoItNo.Clear();
            BoItNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (BoItNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            BoItNo.Focus();
            BoItNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (BoItNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jBom.IsExistDocument<JBS.JS.Bom>(BoItNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }

            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.Parameters.AddWithValue("BoItNo", BoItNo.Text.Trim());
                    cmd.CommandText = "Delete from Bom where BoItNo = (@BoItNo)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "Delete from Bomd where BoItNo = (@BoItNo)";
                    cmd.ExecuteNonQuery();

                    tn.Commit();

                    btnNext_Click(null, null);

                    MessageBox.Show("刪除完成!");
                }
                catch (Exception ex)
                {
                    if (tn != null) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (BoItNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new FrmBomPrint())
            {
                frm.BoItNo.Text = BoItNo.Text;
                frm.BoItNo_1.Text = BoItNo.Text;
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (BoItNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (FrmBomBrow frm = new FrmBomBrow())
            {
                frm.ShowDialog();

                writeToTxt(frm.Result);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (BoItNo.Text == string.Empty)
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BoItNo.Focus();
                return;
            }

            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dtD.AsEnumerable().Count(r => r["itno"].ToString().Trim().Length == 0) > 0)
                {
                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        if (dtD.Rows[i]["itno"].ToString().Trim().Length == 0)
                        {
                            dtD.Rows.RemoveAt(i--);
                            dtD.AcceptChanges();
                        }
                    }
                }
            }

            if (dtD.Rows.Count == 0)
            {
                MessageBox.Show("明細不可為空值！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
                if (jBom.IsExistDocument<JBS.JS.Bom>(BoItNo.Text.Trim()))
                {
                    MessageBox.Show("此編號已經重複，請重新輸入!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BoItNo.Text = "";
                    BoItNo.Focus();
                    return;
                }

                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.AddWithValue("boitno", BoItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("boitname", BoItName.Text.Trim());
                        cmd.Parameters.AddWithValue("bototmny", BoTotMny.Text.ToDecimal());
                        cmd.Parameters.AddWithValue("bototqty", BoTotQty.Text.ToDecimal());
                        cmd.Parameters.AddWithValue("bomemo", BoMemo.Text.Trim());

                        cmd.CommandText = @"
                            INSERT INTO [bom]
                            (boitno,boitname,bototmny,bototqty,bomemo) VALUES
                            (@boitno, @boitname, @bototmny, @bototqty, @bomemo ) ";
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.AddWithValue("itno", "");
                        cmd.Parameters.AddWithValue("itname", "");
                        cmd.Parameters.AddWithValue("itunit", "");
                        cmd.Parameters.AddWithValue("itqty", 0.0);
                        cmd.Parameters.AddWithValue("itpareprs", 1.0);
                        cmd.Parameters.AddWithValue("itprice", 0.0);
                        cmd.Parameters.AddWithValue("itprs", 1.0);
                        cmd.Parameters.AddWithValue("itmny", 0.0);
                        cmd.Parameters.AddWithValue("itpkgqty", 1.0);
                        cmd.Parameters.AddWithValue("itnote", 0.0);
                        cmd.Parameters.AddWithValue("itrec", 0.0);
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
                        for (int i = 0; i < dtD.Rows.Count; i++)
                        {
                            dtD.Rows[i]["itrec"] = (i + 1);

                            cmd.Parameters["itno"].Value = dtD.Rows[i]["itno"].ToString();
                            cmd.Parameters["itname"].Value = dtD.Rows[i]["itname"].ToString();
                            cmd.Parameters["itunit"].Value = dtD.Rows[i]["itunit"].ToString();
                            cmd.Parameters["itqty"].Value = dtD.Rows[i]["itqty"].ToDecimal();
                            cmd.Parameters["itpareprs"].Value = dtD.Rows[i]["itpareprs"].ToDecimal();
                            cmd.Parameters["itprice"].Value = dtD.Rows[i]["itprice"].ToDecimal();
                            cmd.Parameters["itprs"].Value = dtD.Rows[i]["itprs"].ToDecimal();
                            cmd.Parameters["itmny"].Value = dtD.Rows[i]["itmny"].ToDecimal();
                            cmd.Parameters["itpkgqty"].Value = dtD.Rows[i]["itpkgqty"].ToDecimal();
                            cmd.Parameters["itnote"].Value = dtD.Rows[i]["itnote"].ToString();
                            cmd.Parameters["itrec"].Value = dtD.Rows[i]["itrec"].ToString();
                            cmd.Parameters["itdesp1"].Value = dtD.Rows[i]["ItDesp1"];
                            cmd.Parameters["itdesp2"].Value = dtD.Rows[i]["ItDesp2"];
                            cmd.Parameters["itdesp3"].Value = dtD.Rows[i]["ItDesp3"];
                            cmd.Parameters["itdesp4"].Value = dtD.Rows[i]["ItDesp4"];
                            cmd.Parameters["itdesp5"].Value = dtD.Rows[i]["ItDesp5"];
                            cmd.Parameters["itdesp6"].Value = dtD.Rows[i]["ItDesp6"];
                            cmd.Parameters["itdesp7"].Value = dtD.Rows[i]["ItDesp7"];
                            cmd.Parameters["itdesp8"].Value = dtD.Rows[i]["ItDesp8"];
                            cmd.Parameters["itdesp9"].Value = dtD.Rows[i]["ItDesp9"];
                            cmd.Parameters["itdesp10"].Value = dtD.Rows[i]["ItDesp10"];

                            cmd.CommandText = @"
                                INSERT INTO [bomd]
                                ([boitno],[itno],[itname],[itunit],[itqty],[itpareprs]
                                ,[itprice],[itprs],[itmny],[itpkgqty],[itnote],[itrec]
                                ,[itdesp1],[itdesp2],[itdesp3],[itdesp4],[itdesp5]
                                ,[itdesp6],[itdesp7],[itdesp8],[itdesp9],[itdesp10]
                                ) values
                                (@boitno,@itno,@itname,@itunit,@itqty,@itpareprs
                                ,@itprice,@itprs,@itmny,@itpkgqty,@itnote,@itrec
                                ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5
                                ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10) ";
                            cmd.ExecuteNonQuery();
                        }

                        tn.Commit();

                        jBom.Save(BoItNo.Text.Trim());

                        Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                        dtD.Clear();
                        BoItNo.Focus();
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
            }
            else if (this.FormState == FormEditState.Modify)
            {
                if (jBom.IsExistDocument<JBS.JS.Bom>(BoItNo.Text.Trim()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
                    return;
                }

                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.Parameters.AddWithValue("boitno", BoItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("boitname", BoItName.Text.Trim());
                        cmd.Parameters.AddWithValue("bototmny", BoTotMny.Text.ToDecimal());
                        cmd.Parameters.AddWithValue("bototqty", BoTotQty.Text.ToDecimal());
                        cmd.Parameters.AddWithValue("bomemo", BoMemo.Text.Trim());

                        cmd.CommandText = @"
                            UPDATE bom SET boitname = @boitname,bototmny = @bototmny,bototqty = @bototqty,bomemo = @bomemo
                            WHERE  boitno = @boitno ";
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.AddWithValue("itno", "");
                        cmd.Parameters.AddWithValue("itname", "");
                        cmd.Parameters.AddWithValue("itunit", "");
                        cmd.Parameters.AddWithValue("itqty", 0.0);
                        cmd.Parameters.AddWithValue("itpareprs", 1.0);
                        cmd.Parameters.AddWithValue("itprice", 0.0);
                        cmd.Parameters.AddWithValue("itprs", 1.0);
                        cmd.Parameters.AddWithValue("itmny", 0.0);
                        cmd.Parameters.AddWithValue("itpkgqty", 1.0);
                        cmd.Parameters.AddWithValue("itnote", "");
                        cmd.Parameters.AddWithValue("itrec", "");
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
                        cmd.CommandText = " Delete from bomd where boitno = @boitno ";
                        cmd.ExecuteNonQuery();
                        for (int i = 0; i < dtD.Rows.Count; i++)
                        {
                            dtD.Rows[i]["itrec"] = (i + 1);

                            cmd.Parameters["itno"].Value = dtD.Rows[i]["itno"].ToString();
                            cmd.Parameters["itname"].Value = dtD.Rows[i]["itname"].ToString();
                            cmd.Parameters["itunit"].Value = dtD.Rows[i]["itunit"].ToString();
                            cmd.Parameters["itqty"].Value = dtD.Rows[i]["itqty"].ToDecimal();
                            cmd.Parameters["itpareprs"].Value = dtD.Rows[i]["itpareprs"].ToDecimal();
                            cmd.Parameters["itprice"].Value = dtD.Rows[i]["itprice"].ToDecimal();
                            cmd.Parameters["itprs"].Value = dtD.Rows[i]["itprs"].ToDecimal();
                            cmd.Parameters["itmny"].Value = dtD.Rows[i]["itmny"].ToDecimal();
                            cmd.Parameters["itpkgqty"].Value = dtD.Rows[i]["itpkgqty"].ToDecimal();
                            cmd.Parameters["itnote"].Value = dtD.Rows[i]["itnote"].ToString();
                            cmd.Parameters["itrec"].Value = dtD.Rows[i]["itrec"].ToString();
                            cmd.Parameters["itdesp1"].Value = dtD.Rows[i]["ItDesp1"];
                            cmd.Parameters["itdesp2"].Value = dtD.Rows[i]["ItDesp2"];
                            cmd.Parameters["itdesp3"].Value = dtD.Rows[i]["ItDesp3"];
                            cmd.Parameters["itdesp4"].Value = dtD.Rows[i]["ItDesp4"];
                            cmd.Parameters["itdesp5"].Value = dtD.Rows[i]["ItDesp5"];
                            cmd.Parameters["itdesp6"].Value = dtD.Rows[i]["ItDesp6"];
                            cmd.Parameters["itdesp7"].Value = dtD.Rows[i]["ItDesp7"];
                            cmd.Parameters["itdesp8"].Value = dtD.Rows[i]["ItDesp8"];
                            cmd.Parameters["itdesp9"].Value = dtD.Rows[i]["ItDesp9"];
                            cmd.Parameters["itdesp10"].Value = dtD.Rows[i]["ItDesp10"];



                            cmd.CommandText = @"
                                INSERT INTO [bomd]
                                (boitno,itno,itname,itunit,itqty,itpareprs
                                ,itprice,itprs,itmny,itpkgqty,itnote,itrec
                                ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5
                                ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10) values
                                (@boitno,@itno,@itname,@itunit,@itqty,@itpareprs
                                ,@itprice,@itprs,@itmny,@itpkgqty,@itnote,@itrec
                                ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5
                                ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10) ";
                            cmd.ExecuteNonQuery();
                        }

                        tn.Commit();

                        jBom.Save(BoItNo.Text.Trim());

                        Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                        dtD.Clear();
                        BoItNo.Focus();
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jBom.Cancel();
            writeToTxt(pk);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dtD.Clear();
            this.Dispose();
        }

        private void BoItNo_DoubleClick(object sender, EventArgs e)
        { 
            jBom.Open<JBS.JS.Bom>(sender, row =>
            {
                BoItNo.Text = row["ItNo"].ToString();
                BoItName.Text = row["ItName"].ToString();
            });
        }

        private void BoItName_Leave(object sender, EventArgs e)
        {
            if (BoItNo.Text.Trim() != "" && dataGridViewT1.Rows.Count == 0 && dataGridViewT1.ReadOnly == false)
            {
                gridAppend_Click(null, null);
            }
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (BoItNo.Text.Trim() != "" && dataGridViewT1.Rows.Count == 0 && dataGridViewT1.ReadOnly == false)
            {
                gridAppend_Click(null, null);
            }
        }

        private void GridBomDAddRows()
        {
            DataRow row = dtD.NewRow();
            row["BoItNo"] = BoItNo.Text.Trim();
            row["itno"] = "";
            row["itname"] = "";
            row["itunit"] = "";
            row["itqty"] = 0;
            row["itpareprs"] = 1;
            row["itprice"] = 0;
            row["itprs"] = 1;
            row["itmny"] = 0;
            row["itpkgqty"] = 1;
            row["itnote"] = "";
            dtD.Rows.Add(row);

        }

        private void GridBomDInsertRows(int i)
        {
            DataRow row = dtD.NewRow();
            row["BoItNo"] = BoItNo.Text.Trim();
            row["itno"] = "";
            row["itname"] = "";
            row["itunit"] = "";
            row["itqty"] = 0;
            row["itpareprs"] = 1;
            row["itprice"] = 0;
            row["itprs"] = 1;
            row["itmny"] = 0;
            row["itpkgqty"] = 1;
            row["itnote"] = "";
            dtD.Rows.InsertAt(row, i);

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
            if (BoItNo.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            if ((dataGridViewT1.Columns[e.ColumnIndex].Name) == this.產品編號.Name)
            {
                jBom.DataGridViewOpen<JBS.JS.Item>(sender, e, dtD, reader =>
                {
                    dataGridViewT1.Tag = reader["itno"].ToString().Trim();

                    dtD.Rows[e.RowIndex]["ItName"] = reader["ItName"].ToString();
                    dtD.Rows[e.RowIndex]["ItUnit"] = reader["ItUnit"].ToString();
                    dtD.Rows[e.RowIndex]["ItPrice"] = reader["ItBuyPri"].ToDecimal();

                    dtD.Rows[e.RowIndex]["itdesp1"] = reader["itdesp1"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp2"] = reader["itdesp2"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp3"] = reader["itdesp3"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp4"] = reader["itdesp4"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp5"] = reader["itdesp5"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp6"] = reader["itdesp6"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp7"] = reader["itdesp7"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp8"] = reader["itdesp8"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp9"] = reader["itdesp9"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp10"] = reader["itdesp10"].ToString();

                    dtD.Rows[e.RowIndex]["itpkgqty"] = 1;
                });
            }
            else if ((dataGridViewT1.Columns[e.ColumnIndex].Name) == this.單位.Name)
            {
                var itno = dtD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                var unit = dataGridViewT1[this.單位.Name, e.RowIndex].EditedFormattedValue.ToString().Trim();

                jBom.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (row != null && unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        dtD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            dtD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();
                            dtD.Rows[e.RowIndex]["itpkgqty"] = row["itpkgqty"];
                        }
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;

                dtD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                CheckMny(e.RowIndex);
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            var CellValue = dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim();

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                if (CellValue == BoItNo.Text.Trim() && BoItNo.TrimTextLenth() > 0)
                {
                    e.Cancel = true;
                    if (dataGridViewT1.EditingControl != null)
                    {
                        dataGridViewT1.EditingControl.Text = CellValue;
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                    }
                    dtD.Rows[e.RowIndex]["itno"] = "";
                    dtD.Rows[e.RowIndex]["itname"] = "";
                    dtD.Rows[e.RowIndex]["itunit"] = "";
                    dtD.Rows[e.RowIndex]["itprice"] = 0;
                    dtD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    dtD.Rows[e.RowIndex]["itdesp1"] = "";
                    dtD.Rows[e.RowIndex]["itdesp2"] = "";
                    dtD.Rows[e.RowIndex]["itdesp3"] = "";
                    dtD.Rows[e.RowIndex]["itdesp4"] = "";
                    dtD.Rows[e.RowIndex]["itdesp5"] = "";
                    dtD.Rows[e.RowIndex]["itdesp6"] = "";
                    dtD.Rows[e.RowIndex]["itdesp7"] = "";
                    dtD.Rows[e.RowIndex]["itdesp8"] = "";
                    dtD.Rows[e.RowIndex]["itdesp9"] = "";
                    dtD.Rows[e.RowIndex]["itdesp10"] = "";
                    dataGridViewT1.InvalidateRow(e.RowIndex);

                    MessageBox.Show("組件明細不可與組件編號相同", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (CellValue.Trim() == "")
                {
                    if (dataGridViewT1.EditingControl != null) dataGridViewT1.EditingControl.Text = "";
                    dtD.Rows[e.RowIndex]["itno"] = "";
                    dtD.Rows[e.RowIndex]["itname"] = "";
                    dtD.Rows[e.RowIndex]["itunit"] = "";
                    dtD.Rows[e.RowIndex]["itprice"] = 0;
                    dtD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    dtD.Rows[e.RowIndex]["itdesp1"] = "";
                    dtD.Rows[e.RowIndex]["itdesp2"] = "";
                    dtD.Rows[e.RowIndex]["itdesp3"] = "";
                    dtD.Rows[e.RowIndex]["itdesp4"] = "";
                    dtD.Rows[e.RowIndex]["itdesp5"] = "";
                    dtD.Rows[e.RowIndex]["itdesp6"] = "";
                    dtD.Rows[e.RowIndex]["itdesp7"] = "";
                    dtD.Rows[e.RowIndex]["itdesp8"] = "";
                    dtD.Rows[e.RowIndex]["itdesp9"] = "";
                    dtD.Rows[e.RowIndex]["itdesp10"] = "";
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jBom.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtD, row =>
                {
                    if (dataGridViewT1.Tag != null)
                    {
                        if (dataGridViewT1.Tag.ToString().Trim() == CellValue)
                            return;
                    }

                    if (row["ittrait"].ToString() == "1")
                    {
                        e.Cancel = true;
                        MessageBox.Show("組合品無法成為子件", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = row["itno"].ToString().Trim();

                    dtD.Rows[e.RowIndex]["itno"] = row["itno"].ToString().Trim();
                    dtD.Rows[e.RowIndex]["itname"] = row["itname"].ToString().Trim();
                    dtD.Rows[e.RowIndex]["itunit"] = row["itunit"].ToString().Trim();
                    dtD.Rows[e.RowIndex]["itprice"] = row["ItBuyPri"].ToDecimal();
                    dtD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    dtD.Rows[e.RowIndex]["itdesp1"] = row["itdesp1"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp2"] = row["itdesp2"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp3"] = row["itdesp3"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp4"] = row["itdesp4"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp5"] = row["itdesp5"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp6"] = row["itdesp6"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp7"] = row["itdesp7"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp8"] = row["itdesp8"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp9"] = row["itdesp9"].ToString();
                    dtD.Rows[e.RowIndex]["itdesp10"] = row["itdesp10"].ToString();
                    dataGridViewT1.InvalidateRow(e.RowIndex);

                    CheckMny(e.RowIndex);

                });

            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.標準用量.Name)
            {
                if (dataGridViewT1.EditingControl != null) dataGridViewT1.EditingControl.Text = CellValue;
                dtD.Rows[e.RowIndex]["itqty"] = CellValue;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                CheckMny(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.母件比例.Name)
            {
                if (CellValue.IsZeroOrEmpty())
                {
                    e.Cancel = true;
                    MessageBox.Show("母件比例不可為0或是空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridViewT1.EditingControl != null) dataGridViewT1.EditingControl.Text = CellValue;
                dtD.Rows[e.RowIndex]["itpareprs"] = CellValue;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                CheckMny(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.單價.Name)
            {
                if (dataGridViewT1.EditingControl != null) dataGridViewT1.EditingControl.Text = CellValue;
                dtD.Rows[e.RowIndex]["itprice"] = CellValue;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                CheckMny(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.折數.Name)
            {
                if (dataGridViewT1.EditingControl != null) dataGridViewT1.EditingControl.Text = CellValue;
                dtD.Rows[e.RowIndex]["itprs"] = CellValue;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                CheckMny(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.金額.Name)
            {
                if (dataGridViewT1.EditingControl != null) dataGridViewT1.EditingControl.Text = CellValue;
                dtD.Rows[e.RowIndex]["itmny"] = CellValue;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                CheckMny(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.包裝數量.Name)
            {
                if (CellValue.IsZeroOrEmpty())
                {
                    e.Cancel = true;
                    MessageBox.Show("包裝數量不可為0或是空值", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dataGridViewT1.EditingControl != null) dataGridViewT1.EditingControl.Text = CellValue;
                dtD.Rows[e.RowIndex]["itpkgqty"] = CellValue;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                CheckMny(e.RowIndex);
            }
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;

            if (!dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
                return;

            dataGridViewT1.Tag = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
        }

        void CheckMny(int Index)
        {
            try
            {
                decimal Mny = (Convert.ToDecimal(dataGridViewT1["標準用量", Index].Value)) * (Convert.ToDecimal(dataGridViewT1["單價", Index].Value)) * (Convert.ToDecimal(dataGridViewT1["折數", Index].Value)) / (Convert.ToDecimal(dataGridViewT1["母件比例", Index].Value));
                dataGridViewT1["金額", Index].Value = string.Format("{0:F" + Common.MF + "}", Mny);
                Summary();
            }
            catch { }
        }

        void Summary()
        {
            try
            {
                decimal Qty = 0;
                for (int q = 0; q < dataGridViewT1.Rows.Count; q++)
                {
                    Qty += Convert.ToDecimal(dataGridViewT1["標準用量", q].Value.ToString());
                }
                BoTotQty.Text = string.Format("{0:F" + Common.Q + "}", Qty);

                decimal Total = 0;
                for (int t = 0; t < dataGridViewT1.Rows.Count; t++)
                {
                    Total += Convert.ToDecimal(dataGridViewT1["金額", t].Value);
                }
                BoTotMny.Text = string.Format("{0:F" + Common.MF + "}", Total);
            }
            catch { }
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                GridBomDAddRows();
                dataGridViewT1.CurrentCell = dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Cells["產品編號"];
                dataGridViewT1.CurrentRow.Selected = true;
            }
            dataGridViewT1.Focus();
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                dataGridViewT1.Rows.Remove(dataGridViewT1.SelectedRows[0]);
                dataGridViewT1.Focus();
                if (dataGridViewT1.Rows.Count > 0)
                {
                    dataGridViewT1.CurrentRow.Selected = true;
                    for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                    {
                        dataGridViewT1["序號", i].Value = (i + 1).ToString();
                    }
                }
                //刪除列後，重新計算『用量總計』與『金額合計』
                Summary();
            }
            dtD.AcceptChanges();
        }

        private void gridInsert_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                if (dataGridViewT1["產品編號", i].Value.ToString() == "")
                {
                    dataGridViewT1.Focus();
                    return;
                }
            }
            int InsertIndex = dataGridViewT1.CurrentRow.Index;
            GridBomDInsertRows(InsertIndex);

            dataGridViewT1.Focus();
            dataGridViewT1.CurrentCell = dataGridViewT1.Rows[InsertIndex].Cells["產品編號"];
            dataGridViewT1.CurrentRow.Selected = true;
        }

        private void BoItNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            if (BoItNo.ReadOnly)
            {
                gridAppend.Enabled = false;
                gridDelete.Enabled = false;
                gridPicture.Enabled = false;
                gridInsert.Enabled = false;
                gridStockQuery.Enabled = false;
                gridItDesp.Enabled = false;
                gridItBuyPrice.Enabled = false;
                dataGridViewT1.ReadOnly = true;
            }
            else
            {
                gridAppend.Enabled = true;
                gridDelete.Enabled = true;
                gridPicture.Enabled = true;
                gridInsert.Enabled = true;
                gridStockQuery.Enabled = true;
                gridItDesp.Enabled = true;
                gridItBuyPrice.Enabled = true;
                dataGridViewT1.ReadOnly = false;
                dataGridViewT1.Columns["序號"].ReadOnly = true;
                if (dataGridViewT1.Rows.Count > 0)
                    dataGridViewT1.CurrentCell = dataGridViewT1[0, 0];
            }
        }

        private void gridPicture_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
                if (dataGridViewT1.SelectedRows.Count > 0)
                    pVar.PictureOpenForm(dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString());
        }

        private void gridStockQuery_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridStockQuery.Focus();
                using (FrmSale_Stock frm = new FrmSale_Stock())
                {
                    frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                    frm.ShowDialog();
                }
                dataGridViewT1.Focus();
            }
        }



        private void gridBuyQuery_Click(object sender, EventArgs e)
        {
            gridItBuyPrice.Focus();

            if (jBom.EnableBShopPrice() == false)
            {
                dataGridViewT1.Focus();
                return;
            }

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = dtD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void BoItNo_Enter(object sender, EventArgs e)
        {
            BoItNo.Tag = BoItNo.Text.Trim();
        }

        private void BoItNo_Validating(object sender, CancelEventArgs e)
        {
            if (BoItNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (BoItNo.Text.Trim() == "")
            {
                e.Cancel = true;
                BoItNo.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = true;
            jBom.Validate<JBS.JS.Item>(BoItNo.Text.Trim(), reader =>
            {
                var ittrait = reader["ittrait"].ToString();
                if (ittrait.ToDecimal() == 3)
                {
                    e.Cancel = true;
                    BoItName.Clear();
                    MessageBox.Show("此產品不是組合組裝品，請重新輸入");
                    return;
                }
                else
                {
                    BoItName.Text = reader["itname"].ToString();
                }
            }, () => result = false);

            if (!result)
            {
                e.Cancel = true;
                jBom.Open<JBS.JS.Bom>(sender, row =>
                {
                    BoItNo.Text = row["ItNo"].ToString();
                    BoItName.Text = row["ItName"].ToString();
                }); 
                BoItNo.SelectAll();
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jBom.IsExistDocument<JBS.JS.Bom>(BoItNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BoItName.Clear();
                    BoItNo.SelectAll();
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jBom.IsExistDocument<JBS.JS.Bom>(BoItNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BoItName.Clear();
                    BoItNo.SelectAll();
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jBom.IsExistDocument<JBS.JS.Bom>(BoItNo.Text.Trim()))
                {
                    if (BoItNo.Tag != null && BoItNo.Tag.ToString().Trim() != BoItNo.Text.Trim())
                    {
                        writeToTxt(BoItNo.Text.Trim());
                    }
                }
                else
                {
                    e.Cancel = true;
                    jBom.Open<JBS.JS.Bom>(sender, row =>
                    {
                        BoItNo.Text = row["ItNo"].ToString();
                        BoItName.Text = row["ItName"].ToString();
                    }); 
                    BoItNo.SelectAll();
                }
            }
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
            else if (keyData == Keys.F6 && gridStockQuery.Enabled)
            {
                gridStockQuery_Click(null, null);
            }
            else if (keyData == Keys.F7 && gridItDesp.Enabled)
            {
                gridItDesp_Click(null, null);
            }
            else if (keyData == Keys.F8 && gridItBuyPrice.Enabled)
            {
                gridBuyQuery_Click(null, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ToolTip tip = new ToolTip();
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號")
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                t.KeyDown += new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
            }
        }

        void t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F12)
            {
                dataGridViewT1_CellDoubleClick(dataGridViewT1, new DataGridViewCellEventArgs(dataGridViewT1.CurrentCell.ColumnIndex, dataGridViewT1.CurrentCell.RowIndex));
            }
        }

        private void gridItDesp_Click(object sender, EventArgs e)
        {
            gridItDesp.Focus();
            using (JE.SOther.FrmDesp frm = new JE.SOther.FrmDesp(true, FormStyle.Mini))
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                {
                    dataGridViewT1.Focus();
                    return;
                }
                frm.dr = dtD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }































    }
}