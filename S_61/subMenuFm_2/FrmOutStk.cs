using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmOutStk : Formbase
    {
        JBS.JS.OuStk jOuStk;

        DataTable dtM = new DataTable();
        DataTable dtD = new DataTable();
        DataTable dtB = new DataTable();

        List<TextBoxbase> list;
        string OldString = "";

        public FrmOutStk()
        {
            InitializeComponent();
            this.jOuStk = new JBS.JS.OuStk();
            this.list = this.getEnumMember();
            cn3.ConnectionString = Common.sqlConnString;

            this.寄庫數量.Set庫存數量小數();
            this.領出數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        private void FrmOutStk_Load(object sender, EventArgs e)
        {
            OuNo.ReadOnly = CuNo.ReadOnly = OuDate.ReadOnly = true;

            OuDate.SetDateLength();

            this.寄庫日期.DataPropertyName = Common.User_DateTime == 1 ? "indate" : "indate1";


            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from oustk where 1=0 ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(dtM);
                    }
                }

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select *,序號='',產品組成='',ItNoUdf = '' from oustkd where 1=0 ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(dtD);
                    }
                    sql = "select * from oustkbom where 1=0 ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(dtB);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (!dtD.Columns.Contains("nonqty"))
                dtD.Columns.Add("nonqty", typeof(System.Decimal));

            dataGridViewT1.DataSource = dtD;

            var pk = jOuStk.Bottom();
            writeToTxt(pk);
        }

        private void FrmOutStk_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();

            using (DataTable t = new DataTable())
            {
                da4.Fill(t);
            }
            using (DataTable t = new DataTable())
            {
                da5.Fill(t);
            }
            using (DataTable t = new DataTable())
            {
                da6.SelectCommand.Parameters["@cuno"].Value = CuNo.Text.Trim();
                da6.Fill(t);
            }
        }

        void LoadOustkD(string ouno)
        {
            dtD.Clear();

            //var ouno = row["ouno"].ToString().Trim();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    //string sql = "select *,序號='',產品組成='' from oustkd where ouno=@ouno";
                   // string sql = "select *,序號='',產品組成='' from instkd,oustkd where instkd.bomid = oustkd.instkdbomid and ouno=@ouno";
                    string sql = "select 序號='',產品組成='',ItNoUdf = (select top 1 itnoudf from item where item.itno = oustkd.itno) ,oustkd.*,nonqty from instkd right join oustkd on instkd.bomid = oustkd.instkdbomid  where ouno=@ouno";
                    
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("ouno", ouno);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtD);
                        }
                    }
                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        dtD.Rows[i]["序號"] = (i + 1).ToString();
                        dtD.Rows[i]["indate"] = Date.AddLine(dtD.Rows[i]["indate"].ToString().Trim());
                        dtD.Rows[i]["indate1"] = Date.AddLine(dtD.Rows[i]["indate1"].ToString().Trim());
                      //  dtD.Rows[i]["instkdBomid"] = get//////
                        if (this.FormState == FormEditState.Append) dtD.Rows[i]["ouqty"] = 0;
                        if (dtD.Rows[i]["ItTrait"].ToDecimal() == 1) dtD.Rows[i]["產品組成"] = "組合品";
                        else if (dtD.Rows[i]["ItTrait"].ToDecimal() == 2) dtD.Rows[i]["產品組成"] = "組裝品";
                        else if (dtD.Rows[i]["ItTrait"].ToDecimal() == 3) dtD.Rows[i]["產品組成"] = "單一商品";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void LoadOustkBom(string ouno)
        {
            dtB.Clear();
            //var ouno = row["ouno"].ToString().Trim();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from oustkbom where ouno=@ouno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ouno", ouno);
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

        void writeToTxt(string ouno)
        {
            var result = jOuStk.LoadData(ouno, row =>
            {
                OuNo.Text = row["OuNo"].ToString().Trim();
                var date = Common.User_DateTime == 1 ? row["oudate"].ToString().Trim() : row["oudate1"].ToString().Trim();
                OuDate.Text = date;
                CuNo.Text = row["CuNo"].ToString().Trim();
                CuName1.Text = row["CuName1"].ToString().Trim();
                CuPer1.Text = row["CuPer1"].ToString().Trim();
                CuTel1.Text = row["CuTel1"].ToString().Trim();

                LoadOustkD(ouno);
                LoadOustkBom(ouno);
            });

            if (!result)
            {
                //清空所有欄位
                OuNo.Text = OuDate.Text = CuNo.Text = "";
                CuName1.Text = CuPer1.Text = CuTel1.Text = "";
                dataGridViewT1.DataSource = dtD;
                dtD.Clear();
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            var pk = jOuStk.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jOuStk.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jOuStk.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jOuStk.Bottom();
            writeToTxt(pk);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            dataGridViewT1.ReadOnly = false;
            foreach (DataGridViewColumn c in dataGridViewT1.Columns)
            {
                c.ReadOnly = (c.Name == "領出數量" || c.Name == "備註說明") ? false : true; 
            }

            OuDate.Text = Date.GetDateTime(Common.User_DateTime);

            dtD.Clear();
            dataGridViewT1.DataSource = dtD;
            dtB.Clear();
            CuNo.Focus();
            this.自定編號.ReadOnly = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (OuNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("空資料庫，請先新增！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jOuStk.IsEditInCloseDay(OuDate.Text) == false)
                return;

            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            CuNo.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OuNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jOuStk.IsEditInCloseDay(OuDate.Text) == false)
                return;


            bool IsTransOK = true;
            bool IsDeleted = false;


            try
            {
                //1.加回未交量
                //2.加回庫存
                //3.刪除資料
                SqlTransaction tn = null;

                if (cn3.State != ConnectionState.Open) cn3.Open();
                tn = cn3.BeginTransaction();

                using (SqlCommand cmd = cn3.CreateCommand())
                {
                    cmd.Transaction = tn;
                    cmd.CommandText = "";
                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        var bomid = dtD.Rows[i]["InstkDBomid"].ToString().Trim();
                        cmd.CommandText += " Update InStkD Set nonQty = ISNULL(nonQty,0) + (" + dtD.Rows[i]["OuQty"].ToDecimal() + ") Where BomId ='" + bomid + "';";
                    }
                    if (cmd.CommandText.Length > 0)
                        cmd.ExecuteNonQuery();

                    if (Common.加庫存(cmd, dtD, dtB, "stno", "ouqty") == false) IsTransOK = false;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("OuNo", OuNo.Text.Trim());
                    cmd.CommandText = "  delete from oustk where OuNo=@OuNo;";
                    cmd.CommandText += " delete from oustkd where OuNo=@OuNo;";
                    cmd.CommandText += " delete from oustkbom where OuNo=@OuNo;";
                    cmd.ExecuteNonQuery();
                }


                if (IsTransOK) tn.Commit();
                else
                {
                    tn.Rollback();
                }
                IsDeleted = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            if (IsDeleted)
            {
                btnNext_Click(null, null);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (OuNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FrmOutStk_Print frm = new FrmOutStk_Print())
            {
                frm.PK = OuNo.Text.Trim();
                frm.CuNo = CuNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (OuNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new FrmOutStkBrow())
            {
                frm.TSeekNo = OuNo.Text.Trim();
                frm.ShowDialog();

                writeToTxt(frm.TResult);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Focus();

            if (jOuStk.IsEditInCloseDay(OuDate.Text) == false)
                return;

            jOuStk.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtD, ref dtB, () => { });

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                if (dtD.Rows[i]["ouqty"].ToDecimal() == 0)
                {
                    dtD.Rows.RemoveAt(i);
                    i--;
                }
            }

            if (dtD.Rows.Count == 0)
            {
                MessageBox.Show("明細不可為空值!");
                return;
            }

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();

                DataTable check = new DataTable();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                    cmd.CommandText = @"
                        Select 
                        InStkD.*,OuStkD.ouqty from InStkD 
                        left join OuStkD 
                        on InStkD.sano=OuStkD.sano 
                        where 0=0 
                        and InStkD.cuno=@cuno ";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(check);
                    }
                }
                foreach (DataRow rw in dtD.Rows)
                {
                    var bid = rw["BomID"].ToString().Trim();
                    var p = check.AsEnumerable().ToList().Find(r => bid == r["BomID"].ToString().Trim());
                    if (p.IsNotNull())
                    {
                        if (rw["ouqty"].ToDecimal() > p["nonqty"].ToDecimal())
                        {
                            MessageBox.Show(rw["ItName"].ToString() + " 的領出數量大於產品寄庫數量！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            rw["nonqty"] = p["nonqty"];
                            return;
                        }
                    }
                    rw["oudate"] = Date.ToTWDate(OuDate.Text);
                    rw["oudate1"] = Date.ToUSDate(OuDate.Text);
                }
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

                    var result = jOuStk.GetPkNumber<JBS.JS.OuStk>(cmd, OuDate.Text, ref OuNo);

                    if (!result)
                    {
                        if (tn != null)
                            tn.Rollback();

                        MessageBox.Show("單號取得失敗!");
                        return;
                    }

                    var ouno = OuNo.Text.Trim();
                    var date = OuDate.Text;

                    DataRow row = dtM.NewRow();
                    row["ouno"] = ouno;
                    row["oudate"] = Date.ToTWDate(date);
                    row["oudate1"] = Date.ToUSDate(date);
                    row["cuno"] = CuNo.Text.Trim();
                    row["cuname1"] = CuName1.Text.Trim();
                    row["cutel1"] = CuTel1.Text.Trim();
                    row["cuper1"] = CuPer1.Text.Trim().GetUTF8(10);
                    row["recordno"] = dtD.Rows.Count;
                    row["IsTrans"] = "";
                    dtM.Rows.Add(row);

                    cmd.CommandText = "select * from oustk where 1=0";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                    {
                        da.InsertCommand = builder.GetInsertCommand();
                        da.InsertCommand.Transaction = tn;
                        da.Update(dtM);
                    }

                    dtB.Clear();
                    int rec = 1,BomItrec=1;
                    foreach (DataRow r in dtD.Rows)////////////////
                    {

                        r["ouno"] = ouno;
                        r["IsTrans"] = "";
                        r["indate"] = Date.RemoveLine(r["indate"].ToString().Trim());
                        r["indate1"] = Date.RemoveLine(r["indate1"].ToString().Trim());
                        r["InstkdBomid"] = r["Bomid"].ToString();
                        r.AcceptChanges();
                        r.SetAdded();
                         
                        //BOM
                        var bomid = r["BomID"].ToString().Trim();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bomid", bomid);
                        cmd.CommandText = "select * from instkbom where bomid = @bomid ";

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        using (DataTable SelectInstkBom = new DataTable())
                        {
                            da.Fill(SelectInstkBom);
                            for (int i = 0; i < SelectInstkBom.Rows.Count; i++)
                            {
                                SelectInstkBom.Rows[i]["BomId"]  = ouno + (rec).ToString().PadLeft(10, '0');
                                SelectInstkBom.Rows[i]["BomRec"] = (rec).ToString();
                                SelectInstkBom.Rows[i]["ItRec"] = (BomItrec).ToString();
                                BomItrec++;
                            }
                            dtB.Merge(SelectInstkBom);
                        }

                        r["Bomid"]    = ouno + (rec).ToString().PadLeft(10, '0');
                        r["BomRec"]   = (rec).ToString();
                        r["RecordNo"] = (rec).ToString();
                        rec++;
                    }

                    cmd.CommandText = "select * from oustkd where 1=0";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                    {
                        da.InsertCommand = builder.GetInsertCommand();
                        da.InsertCommand.Transaction = tn;

                        //for (int i = 0; i < dtD.Rows.Count; i++)////////////////清除
                        //{
                        //    dtD.Rows[i]["InstkdBomid"] = dtD.Rows[i]["Bomid"].ToString();
                        //}
                        da.Update(dtD);
                    }

                    foreach (DataRow r in dtB.Rows)
                    {
                        r["ouno"] = ouno;
                        r["IsTrans"] = "";
                        r.AcceptChanges();
                        r.SetAdded();
                    }

                    cmd.CommandText = "select * from oustkbom where 1=0";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                    {
                        da.InsertCommand = builder.GetInsertCommand();
                        da.InsertCommand.Transaction = tn;
                        da.Update(dtB);
                    }

                    cmd.Parameters.Clear();
                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        var bomid = dtD.Rows[i]["InstkdBomid"].ToString().Trim();
                        cmd.CommandText = " Update InStkD Set nonQty = ISNULL(nonQty,0) - (" + dtD.Rows[i]["OuQty"].ToDecimal() + ") Where BomId ='" + bomid + "';";
                        cmd.ExecuteNonQuery();
                    }

                    result &= Common.扣庫存(cmd, dtD, dtB, "stno", "ouqty");

                    if (!result)
                    {
                        tn.Rollback();
                    }
                    else
                    {
                        tn.Commit();
                        jOuStk.Save(ouno);
                    }
                }
                catch (Exception ex)
                {
                    if (tn != null)
                        tn.Rollback();

                    throw ex;
                }
            }


            foreach (var item in this.Controls.OfType<TextBoxT>())
            {
                item.Clear();
            }
            OuDate.Text = Date.GetDateTime(Common.User_DateTime);

            dtD.Clear();
            dtB.Clear();
            CuNo.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jOuStk.Cancel();
            writeToTxt(pk);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
            領出數量.ReadOnly = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            if (CuNo.ReadOnly) return;
            DataTable t = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = " select cust.* from InStkD inner join cust on cust.cuno=InStkD.cuno "
                               + " where 0=0 "
                               + " and IsNull(InStkD.nonqty,0) > 0";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(t);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (t.Rows.Count == 0)
            {
                MessageBox.Show("查無會員寄庫資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                IEnumerable<IGrouping<string, DataRow>> GroupbyDate = from r in t.AsEnumerable()
                                                                      group r by r["CuNo"].ToString().Trim();

                DataTable temp = t.Clone();
                foreach (var g in GroupbyDate)
                {
                    temp.ImportRow(g.FirstOrDefault());
                }
                temp.AcceptChanges();
                using (var frm = new FrmOutStkCustb())
                {
                    frm.dt = temp.Copy();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        jOuStk.Validate<JBS.JS.Cust>(frm.TResult, row =>
                        {
                            CuNo.Text = row["CuNo"].ToString().Trim();
                            CuName1.Text = row["CuName1"].ToString().Trim();
                            CuTel1.Text = row["CuTel1"].ToString().Trim();
                            CuPer1.Text = row["CuPer1"].ToString().Trim();
                        }); 

                        LoadCustDetail(CuNo.Text.Trim());
                    }
                }
            }
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;
            if (CuNo.Text.Trim().Length == 0)
            {
                e.Cancel = true;
                MessageBox.Show("客戶編號不可為空值！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable t = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = " select cust.* from InStkD inner join cust on cust.cuno=InStkD.cuno "
                                + " where 0=0 "
                                + " and IsNull(InStkD.nonqty,0) > 0";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(t);
                    }
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(ex.ToString());
            }
            if (t.AsEnumerable().Count(r => r["cuno"].ToString().Trim() == CuNo.Text.Trim()) == 0)
            {
                IEnumerable<IGrouping<string, DataRow>> GroupbyDate = from r in t.AsEnumerable()
                                                                      group r by r["CuNo"].ToString().Trim();

                DataTable temp = t.Clone();
                foreach (var g in GroupbyDate)
                {
                    temp.ImportRow(g.FirstOrDefault());
                }
                temp.AcceptChanges();
                using (var frm = new FrmOutStkCustb())
                { 
                    frm.dt = temp.Copy();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        jOuStk.Validate<JBS.JS.Cust>(frm.TResult, row =>
                        {
                            CuNo.Text = row["CuNo"].ToString().Trim();
                            CuName1.Text = row["CuName1"].ToString().Trim();
                            CuTel1.Text = row["CuTel1"].ToString().Trim();
                            CuPer1.Text = row["CuPer1"].ToString().Trim();
                        }); 
                         
                        LoadCustDetail(CuNo.Text.Trim());
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                DataRow rw = t.AsEnumerable().ToList().Find(r => r["cuno"].ToString().Trim() == CuNo.Text.Trim());
                CuNo.Text = rw["CuNo"].ToString().Trim();
                CuName1.Text = rw["CuName1"].ToString().Trim();
                CuTel1.Text = rw["CuTel1"].ToString().Trim();
                CuPer1.Text = rw["CuPer1"].ToString().Trim();
                LoadCustDetail(CuNo.Text.Trim());
            }
        }

        void LoadCustDetail(string cuno)
        {
            dtD.Clear();
            try
            {
                DataTable t = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = " select *,ItNoUdf = (select top 1 ItNoUdf from item where item.itno = InStkD.itno) from InStkD "
                               + " where 0=0 "
                               + " and Isnull(InStkD.nonqty,0) > 0 "
                               + " and InStkD.cuno=@cuno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("cuno", cuno.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(t);
                        }
                    }
                }
                if (t.Rows.Count == 0) return;
                foreach (DataRow row in t.Rows)
                {
                    DataRow r = dtD.NewRow();
                    r["indate"] = row["indate"].ToString().Trim();
                    r["indate1"] = row["indate1"].ToString().Trim();
                    r["inno"] = row["inno"].ToString().Trim();
                    r["sano"] = row["sano"].ToString().Trim();
                    r["oudate"] = Date.GetDateTime(1);
                    r["oudate1"] = Date.GetDateTime(2);
                    r["cuno"] = row["cuno"].ToString().Trim();
                    r["stno"] = row["stno"].ToString().Trim();
                    r["stname"] = row["stname"];
                    r["itno"] = row["itno"].ToString().Trim();
                    r["itname"] = row["itname"];
                    r["ittrait"] = row["ittrait"];
                    r["itunit"] = row["itunit"];
                    r["itnoudf"] = row["itnoudf"];
                    r["itpkgqty"] = row["itpkgqty"];
                    r["ouqty"] = 0;
                    r["inqty"] = row["inqty"];
                    r["qty"] = row["qty"];
                    r["nonqty"] = row["nonqty"];
                    r["bomid"] = row["bomid"];
                    var bomid = row["bomid"].ToString();
                    r["bomrec"] = row["bomrec"];
                    r["recordno"] = row["recordno"];
                    r["memo"] = row["memo"];
                    dtD.Rows.Add(r);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dataGridViewT1.DataSource = dtD;
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    dtD.Rows[i]["序號"] = (i + 1).ToString();
                    dtD.Rows[i]["indate"] = Date.AddLine(dtD.Rows[i]["indate"].ToString().Trim());
                    dtD.Rows[i]["indate1"] = Date.AddLine(dtD.Rows[i]["indate1"].ToString().Trim());
                    if (this.FormState == FormEditState.Append) dtD.Rows[i]["ouqty"] = 0;
                    if (dtD.Rows[i]["ItTrait"].ToDecimal() == 1) dtD.Rows[i]["產品組成"] = "組合品";
                    else if (dtD.Rows[i]["ItTrait"].ToDecimal() == 2) dtD.Rows[i]["產品組成"] = "組裝品";
                    else if (dtD.Rows[i]["ItTrait"].ToDecimal() == 3) dtD.Rows[i]["產品組成"] = "單一商品";
                }
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;
            if (dataGridViewT1.Rows.Count == 0) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "領出數量")
            {
                var qty = dataGridViewT1["領出數量", e.RowIndex].EditedFormattedValue.ToDecimal("F"+Common.Q);
                dtD.Rows[e.RowIndex]["ouqty"] = qty;
                dataGridViewT1.InvalidateRow(e.RowIndex);
                var ouqty = dtD.Rows[e.RowIndex]["ouqty"].ToDecimal();
                var nonqty = dtD.Rows[e.RowIndex]["nonqty"].ToDecimal();
                if (ouqty > nonqty)
                {
                    e.Cancel = true;
                    if (dataGridViewT1.EditingControl.IsNotNull()) (dataGridViewT1.EditingControl as TextBox).SelectAll();
                    MessageBox.Show("領出數量大於寄庫數量！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void CuNo_Enter(object sender, EventArgs e)
        {
            OldString = CuNo.Text.Trim();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    btnAppend.Focus();
                    btnAppend.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    btnDelete.PerformClick();
                    break;
                case Keys.D4:
                    btnBrow.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    btnExit.PerformClick();
                    break;
                case Keys.Home:
                    btnTop.PerformClick();
                    break;
                case Keys.PageUp:
                    btnPrior.PerformClick();
                    break;
                case Keys.PageDown:
                    btnNext.PerformClick();
                    break;
                case Keys.End:
                    btnBottom.PerformClick();
                    break;
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

        private void btnStock_Click(object sender, EventArgs e)
        {
            btnStock.Focus();

            if (dataGridViewT1.Rows.Count > 0)
            {
                using (var frm = new FrmSale_Stock())
                {
                    frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                    frm.ShowDialog();
                }
            }
            dataGridViewT1.Focus();
        }
    }
}
