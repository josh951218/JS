using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmInStkb : Formbase
    {
        public string SaNo = "";
        DataTable dtM = new DataTable();
        DataTable dtD = new DataTable();
        DataTable dtB = new DataTable();
        DataTable tempM = new DataTable();
        DataTable tempD = new DataTable();
        DataTable tempB = new DataTable();
        string InNo = "";
        string btnState = "";

        public FrmInStkb()
        {
            InitializeComponent();
            cn1.ConnectionString = Common.sqlConnString;

            this.寄庫數量.Set庫存數量小數();
            this.銷貨數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.未領數量.DefaultCellStyle.Format = "f" + Common.Q;
        }

        private void FrmInStkb_Load(object sender, EventArgs e)
        {
            #region 狀態為修改時，先將本次新增之寄庫資料加入，方可避免在DataGridView遺漏顯示，本次新增之資料(寄庫資料)。
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using(SqlCommand cm = cn.CreateCommand())
            using(SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_InsertIntoInStkD = new DataTable())
            {
                cn.Open();
                cm.Parameters.Clear();
                cm.Parameters.AddWithValue("sano", SaNo);
                cm.CommandText = " SELECT  count(*) FROM InStkD where sano  = @sano ";
                if (cm.ExecuteScalar().ToInteger() > 0) //如果大於０為Modify
                {
                    cm.CommandText = " SELECT  inno FROM InStkD where sano  = @sano ";
                    string inno_ = cm.ExecuteScalar().ToString();

                    cm.CommandText =@"SELECT InStkD.inno,* FROM  (saleD  left join  InStkD on saleD.sano = InStkD.sano AND saleD.bomid = InStkD.bomid) where saleD.sano =@sano AND InStkD.inno IS NULL";
                    da.Fill(dt_InsertIntoInStkD);
                    foreach (DataRow dr in dt_InsertIntoInStkD.Rows)
                    {
                        SqlTransaction transaction;
                        transaction = cn.BeginTransaction("Transaction");
                        cm.Transaction = transaction;
                        try
                        {
                            #region Insert D
                            cm.Parameters.Clear();
                            cm.Parameters.AddWithValue("inno", inno_);
                            cm.Parameters.AddWithValue("sano", dr["sano"].ToString().Trim());
                            cm.Parameters.AddWithValue("cuno", dr["cuno"].ToString().Trim());
                            cm.Parameters.AddWithValue("indate", Date.ToTWDate(dr["Sadate"].ToString().Trim()));
                            cm.Parameters.AddWithValue("indate1", Date.ToUSDate(dr["Sadate"].ToString().Trim()));
                            cm.Parameters.AddWithValue("stno", dr["stno"].ToString().Trim());
                            cm.Parameters.AddWithValue("stname", dr["stname"].ToString().Trim());
                            cm.Parameters.AddWithValue("ItNo", dr["ItNo"].ToString().Trim());
                            cm.Parameters.AddWithValue("ItName", dr["ItName"].ToString().Trim());
                            cm.Parameters.AddWithValue("ittrait", dr["ittrait"].ToString().Trim());
                            cm.Parameters.AddWithValue("ItUnit", dr["ItUnit"].ToString().Trim());
                            cm.Parameters.AddWithValue("itpkgqty", dr["itpkgqty"]);
                            cm.Parameters.AddWithValue("inqty", "0");
                            cm.Parameters.AddWithValue("qty", dr["qty"]);
                            cm.Parameters.AddWithValue("nonqty", "0");
                            cm.Parameters.AddWithValue("BomId", dr["BomId"].ToString().Trim());
                            cm.Parameters.AddWithValue("bomrec", dr["bomrec"].ToString().Trim());
                            cm.Parameters.AddWithValue("recordno", dr["recordno"].ToString().Trim());
                            cm.Parameters.AddWithValue("IsTrans", "");
                            cm.CommandText =
    @"INSERT INTO InStkD(inno,sano,cuno,indate,indate1,stno,stname,ItNo,ItName,ittrait,ItUnit,itpkgqty,inqty,qty,nonqty,BomId,bomrec,recordno,IsTrans)
values (@inno,@sano,@cuno,@indate,@indate1,@stno,@stname,@ItNo,@ItName,@ittrait,@ItUnit,@itpkgqty,@inqty,@qty,@nonqty,@BomId,@bomrec,@recordno,@IsTrans)";
                            cm.ExecuteNonQuery();
                            #endregion
                            #region Insert Bom
                            using (DataTable dt_InsertIntoInStkBom = new DataTable())
                            {
                                cm.CommandText = "select * from SaleBom where BomID = @BomId";
                                da.Fill(dt_InsertIntoInStkBom);
                                foreach (DataRow dr_ in dt_InsertIntoInStkBom.Rows)
                                {
                                    cm.Parameters.Clear();
                                    cm.Parameters.AddWithValue("inno", inno_);
                                    cm.Parameters.AddWithValue("BomID", dr["BomID"].ToString().Trim());
                                    cm.Parameters.AddWithValue("BomRec", dr["BomRec"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itno", dr["itno"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itname", dr["itname"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itunit", dr["itunit"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itqty", dr["itqty"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itpareprs", dr["itpareprs"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itpkgqty", dr["itpkgqty"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itrec", dr["itrec"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itprice", dr["itprice"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itprs", dr["itprs"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itmny", dr["itmny"].ToString().Trim());
                                    cm.Parameters.AddWithValue("itnote", dr["itnote"].ToString().Trim());
                                    cm.Parameters.AddWithValue("IsTrans", "");

                                    cm.CommandText = @"   
INSERT INTO InStkBOM(inno,BomID,BomRec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,itprice,itprs,itmny,itnote,IsTrans) 
values (@inno,@BomID,@BomRec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote,@IsTrans)";
                                    cm.ExecuteNonQuery();
                                }
                            }
                            #endregion
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            transaction.Rollback();
                        }
                    }
                }
            }
            #endregion

            LoadM();

            btnState = dtM.Rows.Count > 0 ? "Modify" : "Append";
           

            if (btnState == "Append")
            {
                LoadFromSale();
                btnDelete.Enabled = false;
            }
            else if (btnState == "Modify")
            {
                //LoadFromSale();
                btnDelete.Enabled = true;
            }
            WriteToTitle();

            if (!dtD.Columns.Contains("序號")) dtD.Columns.Add("序號", typeof(String));
            if (!dtD.Columns.Contains("inqty")) dtD.Columns.Add("inqty", typeof(Decimal));
            if (!dtD.Columns.Contains("nonqty")) dtD.Columns.Add("nonqty", typeof(Decimal));
            if (!dtD.Columns.Contains("產品組成")) dtD.Columns.Add("產品組成", typeof(String));

            dataGridViewT1.DataSource = dtD;
            dataGridViewT1.ReadOnly = false;
            foreach (DataGridViewColumn c in dataGridViewT1.Columns)
            {
                if(c.Name == "寄庫數量" || c.Name == "備註說明")
                    c.ReadOnly =false ;
                else
                    c.ReadOnly = true;
            }
            foreach (DataGridViewRow r in dataGridViewT1.Rows)
            {
                if (r.Cells["寄庫數量"].EditedFormattedValue.ToDecimal() != r.Cells["未領數量"].EditedFormattedValue.ToDecimal())
                {
                    r.ReadOnly = true;
                }
            }

            if (dtD.Rows.Count > 0)
            {
                dataGridViewT1.CurrentCell = dataGridViewT1.Rows[0].Cells["寄庫數量"];
                dataGridViewT1.CurrentRow.Selected = true;
                dataGridViewT1.Focus();
            }
        }

        void LoadM()
        {
            dtM.Clear();
            dtD.Clear();
            dtB.Clear();
            tempM.Clear();
            tempD.Clear();
            tempB.Clear();

            da1.SelectCommand.Parameters["@sano"].Value = SaNo.Trim();
            da1.Fill(dtM);
            tempM = dtM.Copy();
            if (dtM.Rows.Count > 0)
            {
                LoadD();
                LoadBom();
            }
        }

        void LoadD()
        {
            InNo = dtM.Rows[0]["inno"].ToString().Trim();
            da2.SelectCommand.Parameters["@inno"].Value = InNo.Trim();
            da2.Fill(dtD);
            tempD = dtD.Copy();
        }

        void LoadBom()
        {
            InNo = dtM.Rows[0]["inno"].ToString().Trim();
            da3.SelectCommand.Parameters["@inno"].Value = InNo.Trim();
            da3.Fill(dtB);
            tempB = dtB.Copy();
        }

        void LoadFromSale()
        {
            dtM.Clear();
            dtD.Clear();
            dtB.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sano", SaNo);
                        string sql = "select * from sale where sano=@sano";
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtM);
                        }
                        sql = "select * from saled where sano=@sano";
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtD);
                        }
                        sql = "select * from salebom where sano=@sano";
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtB);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void WriteToTitle()
        {
            if (dtM.Rows.Count > 0)
            {
                var date = "";
                if (btnState == "Append") date = Common.User_DateTime == 1 ? dtM.Rows[0]["sadate"].ToString() : dtM.Rows[0]["sadate1"].ToString();
                else if (btnState == "Modify") date = Common.User_DateTime == 1 ? dtM.Rows[0]["indate"].ToString() : dtM.Rows[0]["indate1"].ToString();

                textBoxT1.Text = dtM.Rows[0]["sano"].ToString();
                Sadate.Text = date.Trim();
                textBoxT3.Text = dtM.Rows[0]["cuno"].ToString();
                textBoxT4.Text = dtM.Rows[0]["cuname1"].ToString();
                textBoxT5.Text = dtM.Rows[0]["cuper1"].ToString();
            }
            else
            {
                foreach (var item in this.Controls.OfType<TextBox>())
                {
                    item.Clear();
                }
            }
        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dtD.Rows[i]["序號"] = (i + 1).ToString();
                if (btnState == "Append") dtD.Rows[i]["inqty"] = 0;
                if (dtD.Rows[i]["ItTrait"].ToDecimal() == 1) dtD.Rows[i]["產品組成"] = "組合品";
                else if (dtD.Rows[i]["ItTrait"].ToDecimal() == 2) dtD.Rows[i]["產品組成"] = "組裝品";
                else if (dtD.Rows[i]["ItTrait"].ToDecimal() == 3) dtD.Rows[i]["產品組成"] = "單一商品";
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (dataGridViewT1.Rows.Count == 0) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "寄庫數量")
            {
                dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (dataGridViewT1.EditingControl.IsNotNull())
                    dtD.Rows[e.RowIndex]["inqty"] = dataGridViewT1.EditingControl.Text.Trim();
                var inqty = dtD.Rows[e.RowIndex]["inqty"].ToDecimal();
                var qty = dtD.Rows[e.RowIndex]["qty"].ToDecimal();
                if (inqty > qty)
                {
                    e.Cancel = true;
                    if (dataGridViewT1.EditingControl.IsNotNull()) (dataGridViewT1.EditingControl as TextBox).SelectAll();
                    MessageBox.Show("寄庫數量大於銷貨數量！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    dtD.Rows[e.RowIndex]["nonqty"] = inqty;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var machine = "";

            if (btnState == "Append")
            {
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    dtD.Rows[i]["nonqty"] = dtD.Rows[i]["inqty"].ToDecimal();
                }
            }

            SqlTransaction tn = null;
            DataTable t = new DataTable();
            DataTable t1 = new DataTable();
            DataTable t2 = new DataTable();
            DataTable t3 = new DataTable();
            string ip = "";
            string inno = "";

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {

                    string sql = "select ip from stkroom where stno=@stno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("stno", Common.User_StkNo);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            t = new DataTable();
                            da.Fill(t);
                            if (t.Rows.Count > 0) ip = t.Rows[0]["ip"].ToString().Trim();
                        }
                    }
                    var dd = "";
                    if (Common.Sys_NoAdd >= 3)
                    {
                        dd = Date.ToUSDate(Sadate.Text.Trim());
                    }
                    else
                    {
                        dd = Date.ToTWDate(Sadate.Text.Trim());
                    }

                    if (cn.State != ConnectionState.Open) cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("inno", ip + dd);
                        cmd.CommandText = "select inno from instk where inno like @inno + '%' order by inno desc";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            t = new DataTable();
                            if (reader.HasRows) t.Load(reader);
                        }
                        decimal d = 1;
                        var collection = t.AsEnumerable();
                        if (Common.Sys_NoAdd == 1)
                        {
                            inno = ip + dd + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 2)
                        {
                            inno = ip + dd.takeString(5) + (d.ToString().PadLeft(6, '0'));
                        }
                        else if (Common.Sys_NoAdd == 3)
                        {
                            inno = ip + dd + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 4)
                        {
                            inno = ip + dd.takeString(6) + (d.ToString().PadLeft(6, '0'));
                        }

                        while (collection.Count(r => r["inno"].ToString().Trim() == inno) > 0)
                        {
                            d++;
                            if (Common.Sys_NoAdd == 1)
                            {
                                inno = ip + dd + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 2)
                            {
                                inno = ip + dd.takeString(5) + (d.ToString().PadLeft(6, '0'));
                            }
                            else if (Common.Sys_NoAdd == 3)
                            {
                                inno = ip + dd + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 4)
                            {
                                inno = ip + dd.takeString(6) + (d.ToString().PadLeft(6, '0'));
                            }
                        }
                    }

                    sql = "select top 1 * from instk where 1=0";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(t1);
                    }
                    sql = "select top 1 * from instkd where 1=0";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(t2);
                    }
                    sql = "select top 1 * from instkbom where 1=0";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(t3);
                    }



                    bool IsTransOK = true;
                    if (btnState == "Append")
                    {
                        tn = cn.BeginTransaction();
                        foreach (DataRow dr in dtM.Rows)
                        {
                            DataRow row = t1.NewRow();
                            row["inno"] = inno;
                            row["indate"] = Date.ToTWDate(Sadate.Text.Trim());
                            row["indate1"] = Date.ToUSDate(Sadate.Text.Trim());
                            row["cuno"] = dr["cuno"].ToString().Trim();
                            row["cuname1"] = dr["cuname1"].ToString().Trim();
                            row["cutel1"] = dr["cutel1"].ToString().Trim();
                            row["cuper1"] = dr["cuper1"].ToString().Trim();
                            row["xa1no"] = dr["xa1no"].ToString().Trim();
                            row["Xa1Name"] = dr["Xa1Name"].ToString().Trim();
                            row["xa1par"] = dr["xa1par"].ToString().Trim();
                            row["invno"] = dr["invno"].ToString().Trim();
                            row["recordno"] = dr["recordno"].ToString().Trim();
                            row["sano"] = dr["sano"].ToString().Trim();
                            row["IsTrans"] = machine;
                            row["stno"] = dr["stno"].ToString().Trim();
                            t1.Rows.Add(row);
                        }
                        t1.AcceptChanges();
                        foreach (DataRow dr in t1.Rows) dr.SetAdded();
                        da1.InsertCommand.Connection = cn;
                        da1.InsertCommand.Transaction = tn;
                        da1.Update(t1);

                        foreach (DataRow dr in dtD.Rows)
                        {
                            DataRow row = t2.NewRow();
                            row["inno"] = inno;
                            row["sano"] = textBoxT1.Text.Trim();
                            row["cuno"] = textBoxT3.Text.Trim();
                            row["indate"] = Date.ToTWDate(Sadate.Text.Trim());
                            row["indate1"] = Date.ToUSDate(Sadate.Text.Trim());
                            row["stno"] = dr["stno"].ToString().Trim();
                            row["stname"] = dr["stname"].ToString().Trim();
                            row["ItNo"] = dr["ItNo"].ToString().Trim();
                            row["ItName"] = dr["ItName"].ToString().Trim();
                            row["ittrait"] = dr["ittrait"].ToString().Trim();
                            row["ItUnit"] = dr["ItUnit"].ToString().Trim();
                            row["itpkgqty"] = dr["itpkgqty"];
                            row["inqty"] = dr["inqty"];
                            row["qty"] = dr["qty"];
                            row["nonqty"] = dr["nonqty"];
                            row["BomId"] = dr["BomId"].ToString().Trim();
                            row["bomrec"] = dr["bomrec"].ToString().Trim();
                            row["recordno"] = dr["recordno"].ToString().Trim();
                            row["memo"] = dr["memo"].ToString().Trim();
                            row["IsTrans"] = machine;
                            t2.Rows.Add(row);
                        }
                        t2.AcceptChanges();
                        foreach (DataRow dr in t2.Rows) dr.SetAdded();
                        da2.InsertCommand.Connection = cn;
                        da2.InsertCommand.Transaction = tn;
                        da2.Update(t2);

                        foreach (DataRow dr in dtB.Rows)
                        {
                            DataRow row = t3.NewRow();
                            row["inno"] = inno;
                            row["BomID"] = dr["BomID"].ToString().Trim();
                            row["BomRec"] = dr["BomRec"].ToString().Trim();
                            row["itno"] = dr["itno"].ToString().Trim();
                            row["itname"] = dr["itname"].ToString().Trim();
                            row["itunit"] = dr["itunit"].ToString().Trim();
                            row["itqty"] = dr["itqty"].ToString().Trim();
                            row["itpareprs"] = dr["itpareprs"].ToString().Trim();
                            row["itpkgqty"] = dr["itpkgqty"].ToString().Trim();
                            row["itrec"] = dr["itrec"].ToString().Trim();
                            row["itprice"] = dr["itprice"].ToString().Trim();
                            //row["memo"] = dr["memo"].ToString().Trim();//
                            row["itprs"] = dr["itprs"].ToString().Trim();
                            row["itmny"] = dr["itmny"].ToString().Trim();
                            row["itnote"] = dr["itnote"].ToString().Trim();
                            row["IsTrans"] = machine;
                            t3.Rows.Add(row);
                        }
                        t3.AcceptChanges();
                        foreach (DataRow dr in t3.Rows) dr.SetAdded();
                        da3.InsertCommand.Connection = cn;
                        da3.InsertCommand.Transaction = tn;
                        da3.Update(t3);

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = tn;
                            if (Common.加庫存(cmd, dtD, dtB, "stno", "inqty") == false) IsTransOK = false;
                        }
                    }
                    else if (btnState == "Modify")
                    {
                        tn = cn.BeginTransaction();

                        //扣tempD庫存，再加回dtD庫存
                        foreach (DataRow dr in dtD.Rows)
                        {
                            dr["IsTrans"] = machine;
                        }

                        da2.UpdateCommand.Connection = cn;
                        da2.UpdateCommand.Transaction = tn;
                        da2.Update(dtD);

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = tn;
                            if (Common.扣庫存(cmd, tempD, tempB, "stno", "inqty") == false) IsTransOK = false;
                            if (Common.加庫存(cmd, dtD, dtB, "stno", "inqty") == false) IsTransOK = false;
                        }
                    }

                    if (IsTransOK)
                    {
                        tn.Commit();
                        MessageBox.Show("儲存成功！");
                        this.Dispose();
                    }
                    else
                    {
                        tn.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnState == "Modify")
            {
                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        //1.檢查是否有領出記錄
                        //2.扣庫存
                        //3.刪除資料

                        cn.Open();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("inno", InNo);
                            cmd.CommandText = "select Count(*) from oustkd where inno=@inno";
                            if (cmd.ExecuteScalar().ToDecimal() > 0)
                            {
                                MessageBox.Show("此單據已有寄庫領出記錄，無法刪除！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        bool IsTransOK = true;
                        tn = cn.BeginTransaction();

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = tn;
                            if (Common.扣庫存(cmd, tempD, tempB, "stno", "inqty") == false) IsTransOK = false;
                        }

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = tn;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("inno", InNo.Trim());
                            cmd.CommandText = "  delete from instk    where inno=@inno;";
                            cmd.CommandText += " delete from instkd   where inno=@inno;";
                            cmd.CommandText += " delete from instkbom where inno=@inno;";
                            cmd.ExecuteNonQuery();
                        }

                        if (IsTransOK)
                        {
                            tn.Commit();
                            MessageBox.Show("刪除成功！");
                            this.Dispose();
                        }
                        else
                        {
                            tn.Rollback();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (tn.IsNotNull()) tn.Rollback();
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F9:
                    btnSave.PerformClick();
                    break;
                case Keys.F4:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dtM.Clear();
            dtD.Clear();
            dtB.Clear();
            tempM.Clear();
            tempD.Clear();
            tempB.Clear();
            base.OnFormClosing(e);
        }
    }
}
