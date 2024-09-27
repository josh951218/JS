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

namespace S_61.S2
{
    public partial class FrmLendNew : Formbase
    {
        JBS.JS.Lend jLend;

        DataTable LenD = new DataTable(); //明細檔
        DataTable LenDTemp = new DataTable(); //明細暫存檔

        DataTable LenBom = new DataTable(); //組件檔
        DataTable LenBomTemp = new DataTable();//組件暫存檔

        List<TextBoxbase> list;
        List<ButtonSmallT> grid;

        string ItNoBegin = "";
        string UdfNoBegin = "";
        string CuName2 = "";
        string TextBefore = "";
        string Memo1 = "";
        decimal BomRec = 0;

        public FrmLendNew()
        {
            InitializeComponent();
            this.jLend = new JBS.JS.Lend();
            this.list = this.getEnumMember();

            grid = new List<ButtonSmallT> { gridAppend, gridBomD, gridDelete, gridInsert, gridItDesp, gridPicture };
            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            labelT13.Text = Common.Sys_MemoUdf;
            this.借出數量.Set庫存數量小數();
            this.售價.Set銷貨單價小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 4;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前售價.FirstNum = 12;
            this.稅前售價.LastNum = 6;
            this.稅前售價.DefaultCellStyle.Format = "f6";
            this.稅前金額.Set銷項金額小數();
            this.包裝數量.Set庫存數量小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.Set銷貨單價小數();
            this.借出未還量.Set庫存數量小數();

            LeDate.SetDateLength();

            Tax.FirstNum = 12;
            Tax.LastNum = Common.TS;
            TaxMny.FirstNum = 12;
            TaxMny.LastNum = Common.MST;
            TotMny.FirstNum = 12;
            TotMny.LastNum = Common.MST;
            TaxMnyB.FirstNum = 12;
            TaxMnyB.LastNum = Common.M;
            Rate.FirstNum = 1;
            Rate.LastNum = 0;
            Xa1Par.LastNum = 4;
            Xa1Par.FirstNum = 4;

            if (!LenD.Columns.Contains("序號")) LenD.Columns.Add("序號", typeof(String));
            if (!LenD.Columns.Contains("產品組成")) LenD.Columns.Add("產品組成", typeof(String));

            LenD.RowChanged += new DataRowChangeEventHandler(LenD_RowChanged);

            dataGridViewT1.DataSource = LenD;
            LenBom.Clear();
            LenBomTemp = LenBom.Clone();

            if (Common.Series == "74")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                EmNo.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);
                this.訂單憑證.Visible = false;
            }
            else if (Common.Series == "73")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                this.訂單憑證.Visible = true;
            }
            else if (Common.Series == "72")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = true;
                EmNo.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);
                this.訂單憑證.Visible = false;
            }
            else if (Common.Series == "71")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = true;
                this.訂單憑證.Visible = true;
            }

            //金額權限
            TaxMnyB.Visible = Common.User_SalePrice;
            TaxMny.Visible = Common.User_SalePrice;
            Tax.Visible = Common.User_SalePrice;
            TotMny.Visible = Common.User_SalePrice;
            X3No.Visible = Common.User_SalePrice;
            X3Name.Visible = Common.User_SalePrice;
            Rate.Visible = Common.User_SalePrice;

            this.售價.Visible = Common.User_SalePrice;
            this.稅前售價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
            this.本幣單價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;
            this.本幣稅前單價.Visible = Common.User_SalePrice;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        void LenD_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                e.Row["序號"] = e.Row.Table.Rows.Count;
            }
        }

        private void LeNo_Load(object sender, EventArgs e)
        {
            Action InitdtD = () =>
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = @"
                    Select 產品組成=
                    case
                    when ittrait=1 then '組合品'
                    when ittrait=2 then '組裝品'
                    when ittrait=3 then '單一商品'
                    end,ItNoUdf='',*
                    from Lendd where 1=0 ";

                    da.Fill(LenD);
                    da.Fill(LenDTemp);
                }
            };
            InitdtD.Invoke();

            var pk = jLend.Bottom();
            writeToTxt(pk);
        }

        private void FrmLendNew_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string leno)
        {
            var result = jLend.LoadData(leno, row =>
            {
                LeNo.Text = row["leno"].ToString().Trim();
                LeDate.Text = Common.User_DateTime == 1 ? row["ledate"].ToString() : row["ledate1"].ToString();
                CuNo.Text = row["cuno"].ToString().Trim();
                CuName1.Text = row["cuname1"].ToString().Trim();
                StNo.Text = row["stno"].ToString().Trim(); ;
                StName.Text = row["stname"].ToString().Trim();
                EmNo.Text = row["emno"].ToString().Trim();
                EmName.Text = row["emname"].ToString().Trim();
                Xa1No.Text = row["xa1no"].ToString().Trim();
                Xa1Name.Text = row["xa1name"].ToString().Trim();
                Xa1Par.Text = row["xa1par"].ToDecimal().ToString("f4").Trim();
                TaxMny.Text = row["taxmny"].ToDecimal().ToString("f" + Common.MST).Trim();
                TaxMnyB.Text = row["taxmnyb"].ToDecimal().ToString("f" + Common.M).Trim();
                Tax.Text = row["tax"].ToDecimal().ToString("f" + Common.TS).Trim();
                TotMny.Text = row["totmny"].ToDecimal().ToString("f" + Common.MST).Trim();
                LeMemo.Text = row["lememo"].ToString().Trim();
                StNoi.Text = row["stnoi"].ToString().Trim();
                StNamei.Text = row["stnamei"].ToString().Trim();

                X3No.Text = row["x3no"].ToString();
                Rate.Text = (row["rate"].ToDecimal() * 100).ToString("f0").Trim();
                jLend.Validate<JBS.JS.XX03>(X3No.Text, r => X3Name.Text = r["x3name"].ToString(), () => X3Name.Clear());

                loadLenD();

                leoverflag.Checked = (row["leoverflag"].ToString() == "True");

                this.CuName2 = row["cuname2"].ToString();
                this.Memo1 = row["lememo1"].ToString();
                jLend.keyMan.Set(row);

            });

            if (!result)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                LenD.Clear();
                LenDTemp.Clear();
                LenBom.Clear();
                LenBomTemp.Clear();

                leoverflag.Checked = false;

                this.CuName2 = "";
                this.Memo1 = "";
                jLend.keyMan.Clear();
            }
        }

        void loadLenD()
        {
            LenD.Clear();
            LenDTemp.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("LeNo", LeNo.Text.Trim());

                    cmd.CommandText = @"
                    Select 產品組成=
                    case
                    when ittrait=1 then '組合品'
                    when ittrait=2 then '組裝品'
                    when ittrait=3 then '單一商品'
                    end,ItNoUdf= (select top 1 itnoudf from item where item.itno = lendd.itno),*
                    from Lendd where LeNo=@LeNo 
                    order by  lendd.recordno ";

                    da.Fill(LenD);
                    da.Fill(LenDTemp);

                    dataGridViewT1.DataSource = LenD;
                    if (LenD.Rows.Count > 0) BomRec = LenD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadLenBom()
        {
            LenBom.Clear();
            LenBomTemp.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LeNo", jLend.GetCurrent());

                    string sql = "";
                    if (this.FormState == FormEditState.Append) sql = "select top 1 * from LendBom where 1=0";
                    else if (this.FormState == FormEditState.Duplicate) sql = "select * from LendBom where LeNo=@LeNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    else if (this.FormState == FormEditState.Modify) sql = "select * from LendBom where LeNo=@LeNo COLLATE Chinese_Taiwan_Stroke_BIN";

                    cmd.CommandText = sql;

                    da.Fill(LenBom);
                    da.Fill(LenBomTemp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            var pk = jLend.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jLend.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jLend.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jLend.Bottom();
            writeToTxt(pk);
        }

        decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
        }

        void WorkInit()
        {
            if (FormState == FormEditState.Append)
            {
                LenD.Clear();
                loadLenBom();

                this.CuName2 = "";
                this.Memo1 = "";
                this.BomRec = 0;

                LeDate.Text = Date.GetDateTime(Common.User_DateTime, false);

                StNo.Text = Common.User_StkNo;
                pVar.StkValidate(Common.User_StkNo, StNo, StName);

                Xa1No.Text = "TWD";
                pVar.Xa01Validate("TWD", Xa1No, Xa1Name);
                Xa1Par.Text = "1.0000";
                X3No.Text = "1";
                pVar.XX03Validate("1", X3No, X3Name, Rate);

                StNoi.Text = "BOUT";
                StNamei.Text = "借出倉庫";

                var d = 0;
                TaxMny.Text = d.ToString("f" + TaxMny.LastNum);
                Tax.Text = d.ToString("f" + Tax.LastNum);
                TotMny.Text = d.ToString("f" + TotMny.LastNum);
                TaxMnyB.Text = d.ToString("f" + TaxMnyB.LastNum);

                leoverflag.Checked = false;

                LeDate.Focus();
            }
            else if (FormState == FormEditState.Duplicate)
            {
                loadLenBom();

                LeNo.Clear();
                LeDate.Text = Date.GetDateTime(Common.User_DateTime, false);

                for (int i = 0; i < LenD.Rows.Count; i++)
                {
                    LenD.Rows[i]["qtynotout"] = LenD.Rows[i]["qty"].ToString();
                    LenD.Rows[i]["orno"] = "";
                }

                leoverflag.Checked = false;

                LeDate.Focus();
            }
            else if (FormState == FormEditState.Modify)
            {
                loadLenBom();
                LeNo.Focus();
            }
            this.自定編號.ReadOnly = true;
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            bool enable = btnAppend.Enabled;
            dataGridViewT1.ReadOnly = enable;

            if (Common.Sys_KeyPrs == 2)
                this.折數.ReadOnly = true;

            this.序號.ReadOnly = true;
            this.倉庫名稱.ReadOnly = true;
            this.稅前售價.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.借出未還量.ReadOnly = true;

            this.稅前金額.ReadOnly = !Common.User_SalePrice;

            gridAppend.Enabled = gridBomD.Enabled = gridDelete.Enabled = gridInsert.Enabled = gridItDesp.Enabled = leoverflag.Enabled = !enable;


            //74.73版單倉
            if (Common.Series == "74")
            {
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = true;
            }
            else if (Common.Series == "73")
            {
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = false;
            }
            else if (Common.Series == "72")
            {
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = true;
            }
            else if (Common.Series == "71")
            {
                dataGridViewT1.Columns["訂單憑證"].ReadOnly = false;
            }
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            WorkInit();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (LeNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "提示視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            WorkInit();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (LeNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "提示視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jLend.IsExistDocument<JBS.JS.Lend>(LeNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                var pk = jLend.Next();
                writeToTxt(pk);
                return;
            }

            if (jLend.IsEditInCloseDay(LeDate.Text) == false)
                return;

            if (jLend.IsModify<JBS.JS.Lend>(LeNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jLend.upModify1<JBS.JS.Lend>(LeNo.Text.Trim());//更新修改狀態1
                var pk = jLend.Renew();//刷新資料
                writeToTxt(pk);
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            WorkInit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LeNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "提示視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jLend.IsExistDocument<JBS.JS.Lend>(LeNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                var pk = jLend.Next();
                writeToTxt(pk);
                return;
            }
            if (jLend.IsModify<JBS.JS.Lend>(LeNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jLend.IsEditInCloseDay(LeDate.Text) == false)
                return;

            jLend.GetTempBomOnDeleting("lendbom", LeNo.Text.Trim(), ref LenBomTemp);

            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@leno", LeNo.Text.Trim());
                    cmd.CommandText = @"
                        Delete from lend    where LeNo=@leno;
                        Delete from lendd   where LeNo=@leno;
                        Delete from lendbom where LeNo=@leno; 
                        ";
                    cmd.ExecuteNonQuery();

                    jLend.加庫存(cmd, LenDTemp, LenBomTemp, "stno");
                    jLend.扣庫存(cmd, LenDTemp, LenBomTemp, "stnoi");

                    tn.Commit();

                    jLend.UpdateItemItStockQty(LenDTemp, LenBomTemp);

                    btnNext_Click(null, null);

                    MessageBox.Show("刪除完成!");
                }
                catch (Exception ex)
                {
                    if (tn != null)
                        tn.Rollback();

                    throw ex;
                }
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (LeNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "提示視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new FrmLendBrowNew())
            {
                frm.TSeekNo = LeNo.Text.Trim();
                frm.ShowDialog();

                writeToTxt(frm.TResult);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (LeNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "提示視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new FrmLend_PrintNew())
            {
                frm.PK = LeNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (jLend.IsEditInCloseDay(LeDate.Text) == false)
                return;

            for (int i = 0; i < LenD.Rows.Count; i++)
            {
                LenD.Rows[i]["stnoi"] = StNoi.Text.Trim();
                LenD.Rows[i]["stnamei"] = StNamei.Text.Trim();

                if (LenD.Rows[i]["stno"].ToString() == LenD.Rows[i]["stnoi"].ToString() && LenD.Rows[i]["stname"].ToString() == LenD.Rows[i]["stnamei"].ToString())
                {
                    MessageBox.Show("借出倉庫和客戶倉庫不可為一樣", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
                    dataGridViewT1.CurrentCell = dataGridViewT1["借出倉庫", i];
                    dataGridViewT1.CurrentRow.Selected = true;
                    dataGridViewT1.Focus();
                    SetAllMny();
                    return;
                }
                if (LenD.Rows[i]["stno"].ToString() == "" || LenD.Rows[i]["stname"].ToString() == "")
                {
                    MessageBox.Show("借出倉庫不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
                    dataGridViewT1.CurrentCell = dataGridViewT1["借出倉庫", i];
                    dataGridViewT1.CurrentRow.Selected = true;
                    dataGridViewT1.Focus();
                    SetAllMny();
                    return;
                }

                if (LenD.Rows[i]["itno"].ToString().Trim().Length == 0)
                {
                    if (LenD.Rows[i]["qty"].ToDecimal() > 0)
                    {
                        var rec = LenD.Rows[i]["BomRec"].ToString().Trim();
                        jLend.RemoveBom(rec, ref LenBom);

                        MessageBox.Show("產品編號不可為空!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", i];
                        dataGridViewT1.CurrentRow.Selected = true;
                        dataGridViewT1.Focus();
                        SetAllMny();
                        return;
                    }
                    else
                    {
                        var rec = LenD.Rows[i]["BomRec"].ToString().Trim();
                        jLend.RemoveBom(rec, ref LenBom);

                        LenD.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (LenD.Rows.Count == 0)
            {
                SetAllMny();
                MessageBox.Show("借出明細不可為空！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (FormState == FormEditState.Append || FormState == FormEditState.Duplicate)
            {
                //前置作業
                string cuname2 = "";
                string cutel = "";
                string cuper1 = "";
                jLend.Validate<JBS.JS.Cust>(CuNo.Text.Trim(), reader =>
                {
                    cuname2 = reader["cuname2"].ToString().Trim();
                    cutel = reader["cutel1"].ToString().Trim();
                    cuper1 = reader["cuper1"].ToString().Trim().GetUTF8(10);
                });

                System.Threading.Tasks.Task tk;
                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        if (jLend.GetPkNumber<JBS.JS.Lend>(cmd, LeDate.Text, ref LeNo) == false)
                        {
                            tn.Rollback();

                            MessageBox.Show("單據號碼取得失敗!");
                            return;
                        }

                        //主檔
                        AppendMasterOnSaving(cmd, cuname2, cutel, cuper1);

                        for (int i = 0; i < LenD.Rows.Count; i++)
                        {
                            //儲存明細
                            AppendDetailOnSaving(cmd, i);
                        }

                        //儲存組件
                        AppendBomOnSaving(cmd);

                        //處理庫存
                        jLend.加庫存(cmd, LenD, LenBom, "stnoi");
                        jLend.扣庫存(cmd, LenD, LenBom, "stno");

                        //完成重要資料存檔, 確認交易
                        tn.Commit();

                        //儲存完成
                        jLend.Save(LeNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新產品檔庫存量
                            jLend.UpdateItemItStockQty(LenD, LenBom);
                        });
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                }

                var dl = MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl == DialogResult.Yes)
                {
                    using (var frm = new FrmLend_PrintNew())
                    {
                        frm.PK = jLend.GetCurrent();
                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                btnAppend_Click(null, null);
            }

            if (FormState == FormEditState.Modify)
            {
                if (jLend.IsExistDocument<JBS.JS.Lend>(jLend.GetCurrent()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
                    return;
                }

                //前置作業
                string cuname2 = "";
                string cutel = "";
                string cuper1 = "";
                jLend.Validate<JBS.JS.Cust>(CuNo.Text.Trim(), reader =>
                {
                    cuname2 = reader["cuname2"].ToString().Trim();
                    cutel = reader["cutel1"].ToString().Trim();
                    cuper1 = reader["cuper1"].ToString().Trim().GetUTF8(10);
                });

                System.Threading.Tasks.Task tk;
                SqlTransaction tn = null;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        //儲存主檔
                        UpdateMasterOnSaving(cmd, cuname2, cutel, cuper1);

                        //刪除舊檔
                        DelteOldOnSaving(cmd);

                        for (int i = 0; i < LenD.Rows.Count; i++)
                        {
                            //儲存明細
                            this.AppendDetailOnSaving(cmd, i);
                        }

                        //儲存組件
                        this.AppendBomOnSaving(cmd);

                        //處理庫存
                        jLend.加庫存(cmd, LenDTemp, LenBomTemp, "stno");
                        jLend.扣庫存(cmd, LenDTemp, LenBomTemp, "stnoi");
                        jLend.加庫存(cmd, LenD, LenBom, "stnoi");
                        jLend.扣庫存(cmd, LenD, LenBom, "stno");

                        //完成重要資料存檔, 確認交易
                        tn.Commit();

                        //儲存完成
                        jLend.Save(LeNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            //更新產品檔庫存量
                            jLend.UpdateItemItStockQty(LenDTemp, LenBomTemp, LenD, LenBom);
                        });
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        throw ex;
                    }
                }

                var dl = MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl == DialogResult.Yes)
                {
                    using (var frm = new FrmLend_PrintNew())
                    {
                        frm.PK = jLend.GetCurrent();
                        frm.ShowDialog();
                    }
                }
                jLend.upModify0<JBS.JS.Lend>(LeNo.Text.Trim());//改回0為無修改狀態

                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);
            }
        }
        private void AppendMasterOnSaving(SqlCommand cmd, string cuname2, string cutel, string cuper1)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("leno", LeNo.Text.Trim());
            cmd.Parameters.AddWithValue("ledate", Date.ToTWDate(LeDate.Text.Trim()));
            cmd.Parameters.AddWithValue("ledate1", Date.ToUSDate(LeDate.Text.Trim()));
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("stnoi", StNoi.Text.Trim());
            cmd.Parameters.AddWithValue("stnamei", StNamei.Text.Trim());
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuname1", CuName1.Text.Trim());
            cmd.Parameters.AddWithValue("cuname2", cuname2);
            cmd.Parameters.AddWithValue("cutel", cutel);
            cmd.Parameters.AddWithValue("cuper1", cuper1);
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
            cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
            cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
            cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
            cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
            cmd.Parameters.AddWithValue("taxb", Math.Round(Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
            cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
            cmd.Parameters.AddWithValue("totmnyb", Math.Round(TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
            cmd.Parameters.AddWithValue("lememo", LeMemo.Text.Trim());
            cmd.Parameters.AddWithValue("lememo1", Memo1);
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("recordno", LenD.Rows.Count);

            cmd.Parameters.AddWithValue("leoverflag", leoverflag.Checked.ToString());

            cmd.CommandText = "insert into lend ("
            + " leno,ledate,ledate1,stno,stname,stnoi,stnamei"
            + " ,cuno,cuname1,cuname2,cutel,cuper1,emno,emname,xa1no,xa1name,xa1par"
            + " ,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,lememo,lememo1,appdate,edtdate,appscno,edtscno"
            + " ,recordno,leoverflag) values "
            + " (@leno,@ledate,@ledate1,@stno,@stname,@stnoi,@stnamei"
            + " ,@cuno,@cuname1,@cuname2,@cutel,@cuper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
            + " ,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@lememo,@lememo1,@appdate,@edtdate,@appscno,@edtscno"
            + " ,@recordno,@leoverflag) ";

            cmd.ExecuteNonQuery();
        }
        private void AppendDetailOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("leno", LeNo.Text.Trim());
            cmd.Parameters.AddWithValue("ledate", Date.ToTWDate(LeDate.Text.Trim()));
            cmd.Parameters.AddWithValue("ledate1", Date.ToUSDate(LeDate.Text.Trim()));
            cmd.Parameters.AddWithValue("stno", LenD.Rows[i]["stno"].ToString().Trim());
            cmd.Parameters.AddWithValue("stnoi", StNoi.Text.Trim());
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
            cmd.Parameters.AddWithValue("itno", LenD.Rows[i]["itno"].ToString().Trim());
            cmd.Parameters.AddWithValue("itname", LenD.Rows[i]["itname"].ToString());
            cmd.Parameters.AddWithValue("ittrait", LenD.Rows[i]["ittrait"].ToString().Trim());
            cmd.Parameters.AddWithValue("itunit", LenD.Rows[i]["itunit"].ToString().Trim());
            cmd.Parameters.AddWithValue("itpkgqty", LenD.Rows[i]["itpkgqty"].ToDecimal());
            cmd.Parameters.AddWithValue("qty", LenD.Rows[i]["qty"].ToDecimal());
            cmd.Parameters.AddWithValue("price", LenD.Rows[i]["price"].ToDecimal());
            cmd.Parameters.AddWithValue("priceb", LenD.Rows[i]["priceb"].ToDecimal());
            cmd.Parameters.AddWithValue("prs", LenD.Rows[i]["prs"].ToDecimal());
            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
            cmd.Parameters.AddWithValue("taxprice", LenD.Rows[i]["taxprice"].ToDecimal());
            cmd.Parameters.AddWithValue("taxpriceb", LenD.Rows[i]["taxpriceb"].ToDecimal());
            cmd.Parameters.AddWithValue("mny", LenD.Rows[i]["mny"].ToDecimal());
            cmd.Parameters.AddWithValue("mnyb", LenD.Rows[i]["mnyb"].ToDecimal());
            cmd.Parameters.AddWithValue("memo", LenD.Rows[i]["memo"]);
            cmd.Parameters.AddWithValue("bomid", LeNo.Text.ToString().Trim() + LenD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", LenD.Rows[i]["BomRec"].ToString().Trim());
            cmd.Parameters.AddWithValue("recordno", (i + 1));
            cmd.Parameters.AddWithValue("itdesp1", LenD.Rows[i]["itdesp1"]);
            cmd.Parameters.AddWithValue("itdesp2", LenD.Rows[i]["itdesp2"]);
            cmd.Parameters.AddWithValue("itdesp3", LenD.Rows[i]["itdesp3"]);
            cmd.Parameters.AddWithValue("itdesp4", LenD.Rows[i]["itdesp4"]);
            cmd.Parameters.AddWithValue("itdesp5", LenD.Rows[i]["itdesp5"]);
            cmd.Parameters.AddWithValue("itdesp6", LenD.Rows[i]["itdesp6"]);
            cmd.Parameters.AddWithValue("itdesp7", LenD.Rows[i]["itdesp7"]);
            cmd.Parameters.AddWithValue("itdesp8", LenD.Rows[i]["itdesp8"]);
            cmd.Parameters.AddWithValue("itdesp9", LenD.Rows[i]["itdesp9"]);
            cmd.Parameters.AddWithValue("itdesp10", LenD.Rows[i]["itdesp10"]);
            cmd.Parameters.AddWithValue("stname", LenD.Rows[i]["stname"].ToString().Trim());
            cmd.Parameters.AddWithValue("stnamei", StNamei.Text.Trim());
            cmd.Parameters.AddWithValue("orno", LenD.Rows[i]["orno"].ToString().Trim());
            cmd.Parameters.AddWithValue("qtynotout", LenD.Rows[i]["qtynotout"].ToDecimal());

            cmd.CommandText = "insert into lendd("
                + " leno,ledate,ledate1,stno,stnoi"
                + " ,cuno,emno,xa1no,xa1par,itno,itname,ittrait,itunit,itpkgqty"
                + " ,qty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo"
                + " ,bomid,bomrec,recordno"
                + " ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                + " ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10,stname,stnamei,orno,qtynotout) values "
                + " (@leno,@ledate,@ledate1,@stno,@stnoi"
                + " ,@cuno,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@itpkgqty"
                + " ,@qty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo"
                + " ,@bomid,@bomrec,@recordno"
                + " ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                + " ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@stname,@stnamei,@orno,@qtynotout) ";

            cmd.ExecuteNonQuery();
        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < LenBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("leno", LeNo.Text);
                cmd.Parameters.AddWithValue("bomid", LeNo.Text + LenBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("bomrec", LenBom.Rows[i]["BomRec"].ToString().Trim());
                cmd.Parameters.AddWithValue("itno", LenBom.Rows[i]["itno"].ToString().Trim());
                cmd.Parameters.AddWithValue("itname", LenBom.Rows[i]["itname"].ToString());
                cmd.Parameters.AddWithValue("itunit", LenBom.Rows[i]["itunit"].ToString().Trim());
                cmd.Parameters.AddWithValue("itqty", LenBom.Rows[i]["itqty"].ToString().Trim());
                cmd.Parameters.AddWithValue("itpareprs", LenBom.Rows[i]["itpareprs"].ToString().Trim());
                cmd.Parameters.AddWithValue("itpkgqty", LenBom.Rows[i]["itpkgqty"].ToString().Trim());
                cmd.Parameters.AddWithValue("itrec", (i + 1));
                cmd.Parameters.AddWithValue("itprice", LenBom.Rows[i]["itprice"].ToString().Trim());
                cmd.Parameters.AddWithValue("itprs", "1");
                cmd.Parameters.AddWithValue("itmny", LenBom.Rows[i]["itmny"].ToString().Trim());
                cmd.Parameters.AddWithValue("itnote", LenBom.Rows[i]["itnote"].ToString().Trim());
                cmd.CommandText = "insert into lendbom ("
                    + "leno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                    + "itprice,itprs,itmny,itnote) values "
                    + "(@leno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@"
                    + "itprice,@itprs,@itmny,@itnote) ";

                cmd.ExecuteNonQuery();

            }
        }
        private void UpdateMasterOnSaving(SqlCommand cmd, string cuname2, string cutel, string cuper1)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("leno", LeNo.Text.Trim());
            cmd.Parameters.AddWithValue("ledate", Date.ToTWDate(LeDate.Text.Trim()));
            cmd.Parameters.AddWithValue("ledate1", Date.ToUSDate(LeDate.Text.Trim()));
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("stnoi", StNoi.Text.Trim());
            cmd.Parameters.AddWithValue("stnamei", StNamei.Text.Trim());
            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
            cmd.Parameters.AddWithValue("cuname1", CuName1.Text.Trim());
            cmd.Parameters.AddWithValue("cuname2", cuname2);
            cmd.Parameters.AddWithValue("cutel", cutel);
            cmd.Parameters.AddWithValue("cuper1", cuper1);
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
            cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
            cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
            cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
            cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
            cmd.Parameters.AddWithValue("taxb", Math.Round(Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
            cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
            cmd.Parameters.AddWithValue("totmnyb", Math.Round(TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
            cmd.Parameters.AddWithValue("lememo", LeMemo.Text.Trim());
            cmd.Parameters.AddWithValue("lememo1", Memo1);
            cmd.Parameters.AddWithValue("appdate", jLend.keyMan.AppendTime);
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", jLend.keyMan.AppendMan);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("recordno", LenD.Rows.Count);
            cmd.Parameters.AddWithValue("leoverflag", leoverflag.Checked.ToString());

            cmd.CommandText = @"
            update lend set 
                ledate=@ledate,ledate1=@ledate1,stno=@stno,stname=@stname,stnoi=@stnoi,stnamei=@stnamei
                ,cuno=@cuno,cuname1=@cuname1,cuname2=@cuname2,cutel=@cutel,cuper1=@cuper1,emno=@emno,emname=@emname
                ,xa1no=@xa1no,xa1name=@xa1name,xa1par=@xa1par,taxmnyb=@taxmnyb,taxmny=@taxmny,x3no=@x3no,rate=@rate
                ,tax=@tax,taxb=@taxb,totmny=@totmny,totmnyb=@totmnyb,lememo=@lememo,lememo1=@lememo1,appdate=@appdate
                ,edtdate=@edtdate,appscno=@appscno,edtscno=@edtscno,recordno=@recordno,leoverflag=@leoverflag
            where leno = @leno ";

            cmd.ExecuteNonQuery();
        }
        private void DelteOldOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("leno", LeNo.Text.Trim());
            cmd.CommandText = @"
                delete from lendd   where leno=@leno;
                delete from lendbom where leno=@leno;";
            cmd.ExecuteNonQuery();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jLend.Cancel();
            writeToTxt(pk);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
            jLend.upModify0<JBS.JS.Lend>(LeNo.Text.Trim());//改回0為無修改狀態
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }





        private void gridAppend_Click(object sender, EventArgs e)
        {
            if (CuNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("請先輸入客戶編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuName1.Text = EmNo.Text = EmName.Text = "";
                CuNo.Focus();
                return;
            }
            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();

            if (!LenD.AsEnumerable().Any(r => r["itno"].ToString().Trim().Length == 0))
            {
                DataRow row = LenD.NewRow();
                row["itno"] = "";
                row["itname"] = "";
                row["qty"] = 0;
                row["qtynotout"] = 0;
                row["itunit"] = "";
                row["price"] = 0;
                row["prs"] = 1;
                row["taxprice"] = 0;
                row["mny"] = 0;
                row["itpkgqty"] = 1;
                row["priceb"] = 0;
                row["mnyb"] = 0;
                row["產品組成"] = "";
                row["memo"] = "";
                row["BomRec"] = GetBomRec();
                row["stno"] = StNo.Text.Trim();
                row["stname"] = StName.Text.Trim();
                row["stnoi"] = StNoi.Text.Trim();
                row["stnamei"] = StNamei.Text.Trim();

                LenD.Rows.Add(row);

                var index = LenD.Rows.Count - 1;
                dataGridViewT1.InvalidateRow(index);
                if (Common.Series == "74" || Common.Series == "72")
                {
                    dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, index];
                }
                else
                {
                    if (this.訂單憑證.Visible == false)
                        dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, index];
                    else
                        dataGridViewT1.CurrentCell = dataGridViewT1[this.訂單憑證.Name, index];
                }
                dataGridViewT1.CurrentRow.Selected = true;
            }
            dataGridViewT1.Focus();
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows[0].Cells["借出數量"].Value.ToDecimal() != dataGridViewT1.SelectedRows[0].Cells["借出未還量"].Value.ToDecimal())
            {
                MessageBox.Show("此產品已有還入，無法刪除!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            gridDelete.Focus();
            if (dataGridViewT1.Rows.Count > 0)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                {
                    dataGridViewT1.Focus();
                    return;
                }

                var rec = LenD.Rows[index]["Bomrec"].ToString().Trim();
                jLend.RemoveBom(rec, ref LenBom);

                LenD.Rows.RemoveAt(index);
                LenD.AcceptChanges();

                for (int i = 0; i < LenD.Rows.Count; i++)
                {
                    LenD.Rows[i]["序號"] = (i + 1).ToString();
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                if (dataGridViewT1.Rows.Count > 0)
                {
                    index = index > dataGridViewT1.Rows.Count - 1 ? dataGridViewT1.Rows.Count - 1 : index;
                    if (Common.Series == "74" || Common.Series == "72")
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, index];
                    }
                    else
                    {
                        if (this.訂單憑證.Visible == false)
                            dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, index];
                        else
                            dataGridViewT1.CurrentCell = dataGridViewT1[this.訂單憑證.Name, index];
                    }
                    dataGridViewT1.Rows[index].Selected = true;
                }
            }
            dataGridViewT1.Focus();
        }

        private void gridPicture_Click(object sender, EventArgs e)
        {

            gridPicture.Focus();
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1.SelectedRows.Count > 0)
                {
                    pVar.PictureOpenForm(dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString());
                    dataGridViewT1.Focus();
                }
            }
        }

        private void gridInsert_Click(object sender, EventArgs e)
        {
            if (CuNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("請先輸入客戶編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuName1.Text = StNo.Text = StName.Text = EmNo.Text = EmName.Text = "";
                CuNo.Focus();
                return;
            }
            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridInsert.Focus();
            if (LenD.Rows.Count > 0)
            {
                if (!LenD.AsEnumerable().Any(r => r["itno"].ToString().Trim().Length == 0))
                {
                    var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (index == -1)
                    {
                        dataGridViewT1.Focus();
                        return;
                    }

                    DataRow row = LenD.NewRow();
                    row["itno"] = "";
                    row["itname"] = "";
                    row["qty"] = 0;
                    row["qtynotout"] = 0;
                    row["itunit"] = "";
                    row["price"] = 0;
                    row["prs"] = 1;
                    row["taxprice"] = 0;
                    row["mny"] = 0;
                    row["itpkgqty"] = 1;
                    row["priceb"] = 0;
                    row["mnyb"] = 0;
                    row["產品組成"] = "";
                    row["memo"] = "";
                    row["BomRec"] = GetBomRec();
                    row["stno"] = StNo.Text.Trim();
                    row["stname"] = StName.Text.Trim();
                    row["stnoi"] = StNoi.Text.Trim();
                    row["stnamei"] = StNamei.Text.Trim();
                    LenD.Rows.InsertAt(row, index);

                    for (int i = index; i < LenD.Rows.Count; i++)
                    {
                        LenD.Rows[i]["序號"] = (i + 1).ToString();
                        dataGridViewT1.InvalidateRow(i);
                    }
                    if (Common.Series == "74" || Common.Series == "72")
                    {
                        dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, index];
                    }
                    else
                    {
                        if (this.訂單憑證.Visible == false)
                            dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, index];
                        else
                            dataGridViewT1.CurrentCell = dataGridViewT1[this.訂單憑證.Name, index];
                    }
                    dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridItDesp_Click(object sender, EventArgs e)
        {
            gridItDesp.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus(); return;
            }
            using (var frm = new JE.SOther.FrmDesp(true, FormStyle.Mini))
            {
                frm.dr = LenD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridBomD_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridBomD.Focus();

                if (dataGridViewT1.SelectedRows != null && Common.Sys_LendToSaleMode == 2)
                {
                    if (dataGridViewT1.SelectedRows[0].Cells["借出數量"].Value.ToDecimal() != dataGridViewT1.SelectedRows[0].Cells["借出未還量"].Value.ToDecimal())
                    {
                        MessageBox.Show("此產品已有還出，無法編輯組件!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridViewT1.Focus();
                        return;
                    }
                }


                string _trait = dataGridViewT1["產品組成", dataGridViewT1.CurrentRow.Index].Value.ToString();
                if (_trait != "組合品" && _trait != "組裝品")
                {
                    MessageBox.Show("只有組合品或組裝品可以編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridViewT1.Focus();
                    return;
                }

                using (FrmSale_Bom frm = new FrmSale_Bom())
                {
                    string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].EditedFormattedValue.ToString();
                    DataTable table = LenBom.Clone();

                    for (int i = 0; i < LenBom.Rows.Count; i++)
                    {
                        if (LenBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                        {
                            table.ImportRow(LenBom.Rows[i]);
                            LenBom.Rows.RemoveAt(i--);
                        }
                    }

                    table.AcceptChanges();
                    LenBom.AcceptChanges();
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    frm.dtD = table.Copy();
                    frm.BomRec = rec;
                    frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.grid = dataGridViewT1;
                    frm.上層Row = LenD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (frm.CallBack == "Money")
                            {
                                LenBom.Merge(frm.dtD);
                                LenD.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MS);
                                dataGridViewT1.InvalidateRow(dataGridViewT1.SelectedRows[0].Index);
                                dataGridViewT1.Focus();
                                SetRow_TaxPrice(LenD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetRow_Mny(LenD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetAllMny();
                                break;
                            }
                            else
                            {
                                LenBom.Merge(frm.dtD);
                                LenBom.AcceptChanges();
                                dataGridViewT1.Focus();
                                break;
                            }
                        case DialogResult.Cancel:
                            LenBom.Merge(table);
                            LenBom.AcceptChanges();
                            dataGridViewT1.Focus();
                            break;
                    }
                }
            }
        }

        private void gridStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridStock.Focus();
                subMenuFm_2.FrmSale_Stock frm = new subMenuFm_2.FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
                dataGridViewT1.Focus();
            }
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
            var itno = LenD.Rows[index]["itno"].ToString().Trim();
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
            var itno = LenD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有客戶此產品交易 frm = new S2.Frm所有客戶此產品交易())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridItBuyPrice_Click(object sender, EventArgs e)
        {
            gridItBuyPrice.Focus();

            if (jLend.EnableBShopPrice() == false)
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
            var itno = LenD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();

        }


        void GetSystemPrice(DataRow row, int index)
        {
            string sql = "";
            var itno = row["itno"].ToString().Trim();
            var unit = row["itunit"].ToString().Trim();
            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
            string itunit = "";
            string itunitp = "";
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
                                itunit = reader["itunit"].ToString().Trim();
                                itunitp = reader["itunitp"].ToString().Trim();
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
                        if (itunitp.Trim().Length > 0 && itunitp.Trim() == itunit)
                        {
                            sql = " select s.*,i.itno,售價="
                                + " case"
                                + " when s.regrade = 6 and i.itpkgqty = " + itpkgqty + " then i.itpricep"
                                + " when s.regrade = 1 and i.itpkgqty = " + itpkgqty + " then i.itpricep1"
                                + " when s.regrade = 2 and i.itpkgqty = " + itpkgqty + " then i.itpricep2"
                                + " when s.regrade = 3 and i.itpkgqty = " + itpkgqty + " then i.itpricep3"
                                + " when s.regrade = 4 and i.itpkgqty = " + itpkgqty + " then i.itpricep4"
                                + " when s.regrade = 5 and i.itpkgqty = " + itpkgqty + " then i.itpricep5"
                                + " end,c.cuno from salgrad as s"
                                + " inner join item as i on s.kino=i.scno and itno=(@itno)"
                                + " inner join cust as c on s.x1no=c.cux1no and cuno=(@cuno)";
                        }
                        else
                        {
                            sql = " select s.*,i.itno,售價="
                                + " case"
                                + " when s.regrade = 6  then i.itprice"
                                + " when s.regrade = 1  then i.itprice1"
                                + " when s.regrade = 2  then i.itprice2"
                                + " when s.regrade = 3  then i.itprice3"
                                + " when s.regrade = 4  then i.itprice4"
                                + " when s.regrade = 5  then i.itprice5"
                                + " end,c.cuno from salgrad as s"
                                + " inner join item as i on s.kino=i.scno and itno=(@itno)"
                                + " inner join cust as c on s.x1no=c.cux1no and cuno=(@cuno)";
                        }
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
        void SetRow_TaxPrice(DataRow row)
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
        void SetRow_Mny(DataRow row)
        {
            //稅前金額 = 銷貨數量 * 稅前售價
            var qty = row["qty"].ToDecimal("f" + Common.Q);
            var price = row["price"].ToDecimal("f" + Common.MS);
            var taxprice = row["taxprice"].ToDecimal("f6");

            var mny = qty * taxprice;
            row["mny"] = mny.ToDecimal("f" + Common.TPS);

            var par = Xa1Par.Text.Trim().ToDecimal();
            row["priceb"] = (price * par).ToDecimal("f" + Common.M);
            row["taxpriceb"] = (taxprice * par).ToDecimal("f6");
            row["mnyb"] = (mny * par).ToDecimal("f" + Common.TPS).ToDecimal("f" + Common.M);
        }
        void SetAllMny()
        {
            var tax = 0M;
            var par = Xa1Par.Text.ToDecimal();
            var sum = LenD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f"+Common.TPS)).ToDecimal("f" + Common.MST);

            if (X3No.Text.ToInteger() == 1)
            {
                tax = (sum * Common.Sys_Rate).ToDecimal("f" + Common.TS);
                TaxMny.Text = sum.ToString("f" + Common.MST);
                TaxMnyB.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = tax.ToString("f" + Common.TS);
                TotMny.Text = (sum + tax).ToString("f" + Common.MST);
            }
            else if (X3No.Text.ToInteger() == 2)
            {
                var totmny = LenD.AsEnumerable().Sum(r => r["qty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MS)).ToDecimal("f" + Common.MST);
                tax = (totmny / (1 + Common.Sys_Rate)) * Common.Sys_Rate;

                TotMny.Text = totmny.ToString("f" + Common.MST);
                tax = tax.ToDecimal("f" + Common.TS);
                Tax.Text = tax.ToString();
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MST);
                TaxMnyB.Text = (TaxMny.Text.ToDecimal() * par).ToString("f" + Common.M);
            }
            else if (X3No.Text.ToDecimal() == 3 || X3No.Text.ToDecimal() == 4)
            {
                Tax.Text = tax.ToString("f" + Common.TS);
                TotMny.Text = sum.ToString("f" + Common.MST);
                TaxMny.Text = sum.ToString("f" + Common.MST);
                TaxMnyB.Text = (sum * par).ToString("f" + Common.M);
            }

        }

        private void Text_Enter(object sender, EventArgs e)
        {
            LeNo.Tag = LeNo.Text.Trim();
            CuNo.Tag = CuNo.Text.Trim();
            StNo.Tag = StNo.Text.Trim();
            X3No.Tag = X3No.Text.Trim();
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            if (CuNo.ReadOnly)
                return;

            if (this.FormState == FormEditState.Modify)
            {
                if (LenD.AsEnumerable().Any(r => r["qtynotout"].ToDecimal() != r["qty"].ToDecimal()))
                {
                    MessageBox.Show("此單據已有轉單紀錄, 無法修改客戶!");
                    return;
                }
            }

            jLend.Open<JBS.JS.Cust>(sender, row =>
            {
                CuNo.Text = row["CuNo"].ToString().Trim();
                CuName1.Text = row["cuname1"].ToString();
                X3No.Text = row["cux3no"].ToString();
                EmNo.Text = row["cuemno1"].ToString();
                Xa1No.Text = row["cuxa1no"].ToString();
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);

                for (int i = 0; i < LenD.Rows.Count; i++)
                {
                    GetSystemPrice(LenD.Rows[i], i);
                    SetRow_TaxPrice(LenD.Rows[i]);
                    SetRow_Mny(LenD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                CuNo.Tag = row["CuNo"].ToString().Trim();
            });
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused || CuNo.ReadOnly) return;
            if (CuNo.Text.Trim() == "")
            {
                e.Cancel = true;
                CuName1.Text = EmNo.Text = EmName.Text = "";
                MessageBox.Show("請先輸入客戶編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.FormState == FormEditState.Modify)
            {
                if (CuNo.Text.Trim() == (CuNo.Tag ?? "").ToString())
                    return;

                if (LenD.AsEnumerable().Any(r => r["qtynotout"].ToDecimal() != r["qty"].ToDecimal()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據已有轉單紀錄, 無法修改客戶!");
                    return;
                }
            }

            jLend.ValidateOpen<JBS.JS.Cust>(sender, e, row =>
            {
                if (CuNo.Text.Trim() == (CuNo.Tag ?? "").ToString())
                    return;

                CuNo.Text = row["CuNo"].ToString();
                CuName1.Text = row["cuname1"].ToString();
                X3No.Text = row["cux3no"].ToString();
                EmNo.Text = row["cuemno1"].ToString();
                Xa1No.Text = row["cuxa1no"].ToString();
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);

                for (int i = 0; i < LenD.Rows.Count; i++)
                {
                    GetSystemPrice(LenD.Rows[i], i);
                    SetRow_TaxPrice(LenD.Rows[i]);
                    SetRow_Mny(LenD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                CuNo.Tag = row["CuNo"].ToString().Trim();
            });
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            jLend.Open<JBS.JS.Stkroom>(sender, reader =>
            {
                StNo.Text = reader["StNo"].ToString().Trim();
                StName.Text = reader["StName"].ToString().Trim();

                if (Common.Sys_StNoMode == 1)
                {
                    for (int i = 0; i < LenD.Rows.Count; i++)
                    {
                        LenD.Rows[i]["stno"] = StNo.Text;
                        LenD.Rows[i]["StName"] = StName.Text;
                        dataGridViewT1.InvalidateRow(i);
                    }
                }

                StNo.Tag = reader["StNo"].ToString().Trim();
            });
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly || btnCancel.Focused) return;

            if (StNo.TrimTextLenth() == 0)
            {
                StNo.Clear();
                StName.Clear();
                e.Cancel = true;
                MessageBox.Show("借出倉庫不能為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jLend.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                if (StNo.Text.Trim() == (StNo.Tag ?? "").ToString())
                    return;

                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();

                if (Common.Sys_StNoMode == 1)
                {
                    for (int i = 0; i < LenD.Rows.Count; i++)
                    {
                        LenD.Rows[i]["stno"] = StNo.Text;
                        LenD.Rows[i]["StName"] = StName.Text;
                        dataGridViewT1.InvalidateRow(i);
                    }
                }

                StNo.Tag = row["StNo"].ToString().Trim();
            });
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jLend.Open<JBS.JS.Empl>(sender, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
            });
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            if (EmNo.TrimTextLenth() == 0)
            {
                EmNo.Clear();
                EmName.Clear();
                return;
            }

            jLend.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
            }, true);
        }

        private void LeDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            jLend.DateValidate(sender, e);
        }

        private void LeMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(LeMemo, LeMemo.MaxLength);
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jLend.Open<JBS.JS.XX03>(sender, reader =>
            {
                X3No.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = reader["X3Name"].ToString();

                var rate = reader["X3Rate"].ToDecimal() * 100;
                Rate.Text = rate.ToString("f0");

                //完成稅別設定，重新計算金額
                for (int i = 0; i < LenD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(LenD.Rows[i]);
                    SetRow_Mny(LenD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                X3No.Tag = reader["X3No"].ToString().Trim();
            });
        }

        private void X3No_Validating(object sender, CancelEventArgs e)
        {
            if (X3No.ReadOnly || btnCancel.Focused)
                return;

            if (X3No.Text.Trim() == "")
            {
                X3No.Text = X3Name.Text = Rate.Text = "";
                e.Cancel = true;
                MessageBox.Show("營業稅別不能為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                X3No.SelectAll();
                return;
            }

            jLend.ValidateOpen<JBS.JS.XX03>(sender, e, row =>
            {
                if (X3No.Text == (X3No.Tag ?? "").ToString())
                    return;

                X3No.Text = row["X3No"].ToString();
                X3Name.Text = row["X3Name"].ToString();

                var rate = row["X3Rate"].ToDecimal() * 100;
                Rate.Text = rate.ToString("f0");

                for (int i = 0; i < LenD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(LenD.Rows[i]);
                    SetRow_Mny(LenD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                X3No.Tag = row["X3No"].ToString().Trim();
            });
        }

        private void Tax_Validating(object sender, CancelEventArgs e)
        {
            if (Tax.ReadOnly || btnCancel.Focused) return;

            decimal taxmny = TaxMny.Text.ToDecimal();
            decimal tax = Tax.Text.ToDecimal();
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

        private void LeNo_Validating(object sender, CancelEventArgs e)
        {
            if (LeNo.ReadOnly || btnCancel.Focused) return;

            if (LeNo.Text.Length > 0 && LeNo.Text.Trim() == "")
            {
                e.Cancel = true;
                LeNo.Text = "";
                MessageBox.Show("資料不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (FormState == FormEditState.Append || FormState == FormEditState.Duplicate)
            {
                if (jLend.IsExistDocument<JBS.JS.Lend>(LeNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (FormState == FormEditState.Modify)
            {
                if (jLend.IsExistDocument<JBS.JS.Lend>(LeNo.Text.Trim()))
                {
                    if (LeNo.Text.Trim() == (LeNo.Tag ?? "").ToString())
                        return;

                    writeToTxt(LeNo.Text.Trim());
                    loadLenBom();
                }
                else
                {
                    e.Cancel = true;
                    jLend.Open<JBS.JS.Lend>(sender);
                    LeNo.SelectAll();

                    if (jLend.IsExistDocument<JBS.JS.Lend>(LeNo.Text.Trim()) == true)
                    {
                        writeToTxt(LeNo.Text.Trim());
                        loadLenBom();
                    }
                }
            }
        }

        private void Xa1Par_Validating(object sender, CancelEventArgs e)
        {
            if (Xa1Par.ReadOnly || btnCancel.Focused) return;
            SetAllMny();

            if (Common.keyData != Keys.Up)
            {
                if (CuNo.Text.Trim() != "")
                    if (dataGridViewT1.Rows.Count == 0)
                        if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
            }
        }

        private void gridKeyMan_Click(object sender, EventArgs e)
        {
            if (LeNo.Text.Trim() == "")
                return;

            using (FrmSale_AppScNo frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jLend.keyMan.AppendMan;
                frm.ATime = jLend.keyMan.AppendTime;
                //修改人員
                frm.EName = jLend.keyMan.EditMan;
                frm.ETime = jLend.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = CuNo.ReadOnly ? false : true;
                frm.memo1 = Memo1;

                if (frm.ShowDialog() == DialogResult.OK) Memo1 = frm.memo1;
            }
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0 && dataGridViewT1.ReadOnly == false) gridAppend_Click(null, null);
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.ReadOnly)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "訂單憑證")
            {
                TextBefore = dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jLend.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                TextBefore = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "包裝數量")
            {
                TextBefore = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "借出數量")
            {
                TextBefore = dataGridViewT1["借出數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
        }

        private void FillItem(SqlDataReader reader, int index)
        {
            var itno = reader["itno"].ToString().Trim();

            this.ItNoBegin = itno;
            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;
            LenD.Rows[index]["itno"] = itno;
            LenD.Rows[index]["itname"] = reader["itname"].ToString();
            LenD.Rows[index]["itunit"] = reader["itunit"].ToString().Trim();
            LenD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            if (reader["itsalunit"].ToString() == "1")
            {
                LenD.Rows[index]["itunit"] = reader["itunitp"].ToString().Trim();
                LenD.Rows[index]["price"] = reader["itpricep"].ToDecimal();
                var itpkgqty = reader["itpkgqty"].ToDecimal();
                if (itpkgqty == 0)
                    itpkgqty = 1;
                LenD.Rows[index]["itpkgqty"] = itpkgqty;
            }
            else
            {
                LenD.Rows[index]["itunit"] = reader["itunit"].ToString().Trim();
                LenD.Rows[index]["price"] = reader["itprice"].ToDecimal();
                LenD.Rows[index]["itpkgqty"] = 1;
            }

            GetSystemPrice(LenD.Rows[index], index);
            SetRow_TaxPrice(LenD.Rows[index]);
            SetRow_Mny(LenD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();

            var trait = reader["ItTrait"].ToString();
            LenD.Rows[index]["ItTrait"] = trait;

            if (trait == "1") trait = "組合品";
            else if (trait == "2") trait = "組裝品";
            else if (trait == "3") trait = "單一商品";
            else trait = "";
            LenD.Rows[index]["產品組成"] = trait;

            for (int i = 1; i <= 10; i++)
            {
                LenD.Rows[index]["itdesp" + i] = reader["itdesp" + i];
            }

            var rec = LenD.Rows[index]["BomRec"].ToString().Trim();
            jLend.RemoveBom(rec, ref LenBom);
            jLend.GetItemBom(itno, rec, ref LenBom);
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.訂單憑證.Name)
            {
                if (LenD.Rows[e.RowIndex]["qty"].ToDecimal() != LenD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有還入，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (var frm = new FrmOrderToLend())
                {
                    frm.TSeekNo = dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim();
                    frm.CuNo = CuNo.Text.Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.MasterRow == null && frm.dtDetail.Rows.Count == 0) return;

                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.OrNo.Trim();
                        Memo1 = frm.MasterRow["ormemo1"].ToString();
                        Xa1No.Text = frm.MasterRow["xa1no"].ToString();
                        Xa1Name.Text = frm.MasterRow["xa1name"].ToString();
                        Xa1Par.Text = frm.MasterRow["xa1par"].ToString();
                        X3No.Text = frm.MasterRow["x3no"].ToString();
                        LeMemo.Text = frm.MasterRow["ormemo"].ToString();
                        EmNo.Text = frm.MasterRow["emno"].ToString();
                        EmName.Text = frm.MasterRow["emname"].ToString();
                        pVar.XX03Validate(X3No.Text, X3No, X3Name);

                        jLend.RemoveBom(LenD.Rows[e.RowIndex]["bomrec"].ToString().Trim(), ref LenBom);
                        LenD.Rows[e.RowIndex].Delete();
                        LenD.AcceptChanges();

                        //明細部分&組件
                        DataRow row1, row2;

                        for (int i = 0; i < frm.dtDetail.Rows.Count; i++)
                        {
                            row1 = LenD.NewRow();
                            row2 = frm.dtDetail.Rows[i];
                            if (row2["ittrait"].ToString() == "2")
                            {
                                row1["ittrait"] = row2["ittrait"].ToString();
                                row1["產品組成"] = "組裝品";
                            }
                            else if (row2["ittrait"].ToString() == "3")
                            {
                                row1["ittrait"] = row2["ittrait"].ToString();
                                row1["產品組成"] = "單一商品";
                            }
                            else
                            {
                                row1["ittrait"] = row2["ittrait"].ToString();
                                row1["產品組成"] = "組合品";
                            }
                            row1["orno"] = row2["orno"].ToString();
                            row1["itno"] = row2["itno"].ToString();
                            row1["itname"] = row2["itname"].ToString();
                            row1["itunit"] = row2["itunit"].ToString();
                            row1["qty"] = row2["qty"].ToDecimal("f" + Common.Q);
                            row1["price"] = row2["price"].ToDecimal("f" + Common.MS);
                            row1["prs"] = row2["prs"].ToDecimal("f3");
                            row1["taxprice"] = row2["taxprice"].ToDecimal("f6");
                            row1["memo"] = row2["memo"].ToString();
                            row1["priceb"] = row2["priceb"].ToDecimal("f" + Common.M);
                            row1["taxpriceb"] = row2["taxpriceb"].ToDecimal("f6");
                            row1["itpkgqty"] = row2["itpkgqty"].ToDecimal("f" + Common.Q);
                            row1["qtynotout"] = row2["qty"].ToDecimal("f" + Common.Q);
                            row1["StNo"] = StNo.Text.Trim();
                            row1["StName"] = StName.Text;
                            for (int j = 1; j <= 10; j++)
                            {
                                row1["itdesp" + j] = row2["itdesp" + j].ToString();
                            }
                            row1["BomRec"] = GetBomRec();
                            SetRow_Mny(row1);
                            LenD.Rows.InsertAt(row1, LenD.Rows.Count);
                            LenD.AcceptChanges();

                            //組件部分
                            if (row2["ittrait"].ToDecimal() == 3)
                                continue;

                            var bomid = row2["bomid"].ToString().Trim();
                            var rec = row1["BomRec"].ToString().Trim();

                            jLend.GetTBom<JBS.JS.Order>(bomid, rec, ref LenBom);
                            dataGridViewT1.InvalidateRow(i);
                        }

                        if (this.訂單憑證.Visible == false)
                            dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, e.RowIndex];
                        else
                            dataGridViewT1.CurrentCell = dataGridViewT1[this.訂單憑證.Name, e.RowIndex];

                        dataGridViewT1.CurrentRow.Selected = true;
                        SetAllMny();

                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.產品編號.Name)
            {
                #region 產品編號
                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    MessageBox.Show("此筆資料由訂單轉出，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (LenD.Rows[e.RowIndex]["qty"].ToDecimal() != LenD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有還入，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jLend.DataGridViewOpen<JBS.JS.Item>(sender, e, LenD, row => FillItem(row, e.RowIndex));
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.單位.Name)
            {
                if (LenD.Rows[e.RowIndex]["qty"].ToDecimal() != LenD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有還入，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var itno = LenD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                jLend.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        LenD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            LenD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();
                            var itpkgqty = row["itpkgqty"].ToDecimal();
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            LenD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;
                LenD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                GetSystemPrice(LenD.Rows[e.RowIndex], e.RowIndex);
                SetRow_TaxPrice(LenD.Rows[e.RowIndex]);
                SetRow_Mny(LenD.Rows[e.RowIndex]);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.備註說明.Name)
            {
                if (LeMemo.ReadOnly) return;
                using (var frm = new FrmSale_Memo())
                {
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                            LenD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            break;
                        case DialogResult.Cancel: break;
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.借出倉庫.Name)
            {
                if (dataGridViewT1.Columns["借出倉庫"].ReadOnly)
                    return;

                if (LenD.Rows[e.RowIndex]["qty"].ToDecimal() != LenD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有還入，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jLend.DataGridViewOpen<JBS.JS.Stkroom>(sender, e, LenD, row =>
                {
                    LenD.Rows[e.RowIndex]["stno"] = row["StNo"].ToString();
                    LenD.Rows[e.RowIndex]["StName"] = row["StName"].ToString();
                });
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (gridDelete.Focused || btnCancel.Focused) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.訂單憑證.Name)
            {
                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() == TextBefore)
                    return;

                if (LenD.Rows[e.RowIndex]["qty"].ToDecimal() != LenD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有還入，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    LenD.Rows[e.RowIndex]["orno"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() == "")
                {
                    LenD.Rows[e.RowIndex]["orno"] = "";
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                using (var frm = new FrmOrderToLend())
                {
                    frm.TSeekNo = dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim();
                    frm.CuNo = CuNo.Text.Trim();
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (frm.MasterRow == null && frm.dtDetail.Rows.Count == 0) return;

                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.OrNo.Trim();
                            Memo1 = frm.MasterRow["ormemo1"].ToString();
                            Xa1No.Text = frm.MasterRow["xa1no"].ToString();
                            Xa1Name.Text = frm.MasterRow["xa1name"].ToString();
                            Xa1Par.Text = frm.MasterRow["xa1par"].ToString();
                            X3No.Text = frm.MasterRow["x3no"].ToString();
                            LeMemo.Text = frm.MasterRow["ormemo"].ToString();
                            EmNo.Text = frm.MasterRow["emno"].ToString();
                            EmName.Text = frm.MasterRow["emname"].ToString();
                            pVar.XX03Validate(X3No.Text, X3No, X3Name);

                            jLend.RemoveBom(LenD.Rows[e.RowIndex]["bomrec"].ToString().Trim(), ref LenBom);
                            LenD.Rows[e.RowIndex].Delete();
                            LenD.AcceptChanges();

                            //明細部分&組件
                            DataRow row1, row2;

                            for (int i = 0; i < frm.dtDetail.Rows.Count; i++)
                            {
                                row1 = LenD.NewRow();
                                row2 = frm.dtDetail.Rows[i];
                                if (row2["ittrait"].ToString() == "2")
                                {
                                    row1["ittrait"] = row2["ittrait"].ToString();
                                    row1["產品組成"] = "組裝品";
                                }
                                else if (row2["ittrait"].ToString() == "3")
                                {
                                    row1["ittrait"] = row2["ittrait"].ToString();
                                    row1["產品組成"] = "單一商品";
                                }
                                else
                                {
                                    row1["ittrait"] = row2["ittrait"].ToString();
                                    row1["產品組成"] = "組合品";
                                }
                                row1["orno"] = row2["orno"].ToString();
                                row1["itno"] = row2["itno"].ToString();
                                row1["itname"] = row2["itname"].ToString();
                                row1["itunit"] = row2["itunit"].ToString();
                                row1["qty"] = row2["qty"].ToDecimal("f" + Common.Q);
                                row1["price"] = row2["price"].ToDecimal("f" + Common.MS);
                                row1["prs"] = row2["prs"].ToDecimal("f3");
                                row1["taxprice"] = row2["taxprice"].ToDecimal("f6");
                                row1["memo"] = row2["memo"].ToString();
                                row1["priceb"] = row2["priceb"].ToDecimal("f" + Common.M);
                                row1["taxpriceb"] = row2["taxpriceb"].ToDecimal("f6");
                                row1["itpkgqty"] = row2["itpkgqty"].ToDecimal("f" + Common.Q);
                                row1["qtynotout"] = row2["qty"].ToDecimal("f" + Common.Q);
                                row1["StNo"] = StNo.Text.Trim();
                                row1["StName"] = StName.Text;
                                for (int j = 1; j <= 10; j++)
                                {
                                    row1["itdesp" + j] = row2["itdesp" + j].ToString();
                                }
                                row1["BomRec"] = GetBomRec();
                                SetRow_Mny(row1);
                                LenD.Rows.InsertAt(row1, LenD.Rows.Count);
                                LenD.AcceptChanges();

                                //組件部分
                                if (row2["ittrait"].ToDecimal() == 3)
                                    continue;

                                var bomid = row2["bomid"].ToString().Trim();
                                var rec = row1["BomRec"].ToString().Trim();

                                jLend.GetTBom<JBS.JS.Order>(bomid, rec, ref LenBom);
                                dataGridViewT1.InvalidateRow(i);
                            }

                            if (this.訂單憑證.Visible == false)
                                dataGridViewT1.CurrentCell = dataGridViewT1[this.產品編號.Name, e.RowIndex];
                            else
                                dataGridViewT1.CurrentCell = dataGridViewT1[this.訂單憑證.Name, e.RowIndex];

                            dataGridViewT1.CurrentRow.Selected = true;
                            SetAllMny();

                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
                }


            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.產品編號.Name)
            {
                #region 產品編號
                string itnoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                if (itnoNow.Trim().Length == 0)
                {
                    LenD.Rows[e.RowIndex]["itno"] = "";
                    LenD.Rows[e.RowIndex]["itname"] = "";
                    LenD.Rows[e.RowIndex]["qty"] = 0;
                    LenD.Rows[e.RowIndex]["itunit"] = "";
                    LenD.Rows[e.RowIndex]["price"] = 0;
                    LenD.Rows[e.RowIndex]["prs"] = 1;
                    LenD.Rows[e.RowIndex]["taxprice"] = 0;
                    LenD.Rows[e.RowIndex]["mny"] = 0;
                    LenD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    dataGridViewT1["產品組成", e.RowIndex].Value = "";
                    LenD.Rows[e.RowIndex]["memo"] = "";
                    LenD.Rows[e.RowIndex]["priceb"] = 0;
                    LenD.Rows[e.RowIndex]["taxpriceb"] = 0;
                    LenD.Rows[e.RowIndex]["mnyb"] = 0;
                    LenD.Rows[e.RowIndex]["qtynotout"] = 0;

                    for (int i = 1; i <= 10; i++)
                    {
                        LenD.Rows[e.RowIndex]["itdesp" + i] = "";
                    }
                    var rec = LenD.Rows[e.RowIndex]["BomRec"].ToString().Trim();
                    jLend.RemoveBom(rec, ref LenBom);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                    return;
                }

                if (ItNoBegin == itnoNow)
                    return;

                if (dataGridViewT1["訂單憑證", e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    e.Cancel = true;
                    MessageBox.Show("此筆資料由訂單轉出，無法修改", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    LenD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                if (LenD.Rows[e.RowIndex]["qty"].ToDecimal() != LenD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有還入，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    LenD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                if (UdfNoBegin == itnoNow)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;
                    LenD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jLend.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, LenD, row => FillItem(row, e.RowIndex));
                #endregion
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.借出倉庫.Name)
            {
                jLend.DataGridViewValidateOpen<JBS.JS.Stkroom>(sender, e, LenD, reader =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = reader["stno"].ToString().Trim();

                    LenD.Rows[e.RowIndex]["stno"] = reader["stno"].ToString().Trim();
                    LenD.Rows[e.RowIndex]["StName"] = reader["StName"].ToString().Trim();
                });
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.單位.Name)
            {
                string itno = LenD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (TextBefore == unit)
                    return;

                if (LenD.Rows[e.RowIndex]["qty"].ToDecimal() != LenD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有還入，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    LenD.Rows[e.RowIndex]["itunit"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jLend.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        LenD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        LenD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                LenD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                GetSystemPrice(LenD.Rows[e.RowIndex], e.RowIndex);
                SetRow_TaxPrice(LenD.Rows[e.RowIndex]);
                SetRow_Mny(LenD.Rows[e.RowIndex]);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.借出數量.Name)
            {
                if (dataGridViewT1["借出數量", e.RowIndex].EditedFormattedValue.ToString().Trim() == TextBefore) return;
                if (LenD.Rows[e.RowIndex]["qty"].ToDecimal() != LenD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有還入，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    LenD.Rows[e.RowIndex]["qty"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                var qtyB = LenD.Rows[e.RowIndex]["qty"].ToDecimal();
                var qtyN = dataGridViewT1["借出數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                LenD.Rows[e.RowIndex]["qtynotout"] = qtyN.ToDecimal("f" + Common.Q);
                if (qtyB != qtyN)
                {
                    LenD.Rows[e.RowIndex]["qty"] = qtyN.ToDecimal("f" + Common.Q);
                    SetRow_Mny(LenD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.售價.Name)
            {
                LenD.Rows[e.RowIndex]["Price"] = dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS);

                SetRow_TaxPrice(LenD.Rows[e.RowIndex]);
                SetRow_Mny(LenD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.稅前金額.Name)
            {
                var mnyB = LenD.Rows[e.RowIndex]["mny"].ToDecimal();
                var mnyN = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal();
                if (mnyB != mnyN)
                {
                    decimal qty = LenD.Rows[e.RowIndex]["qty"].ToDecimal();
                    if (qty == 0) qty = 1;
                    switch (X3No.Text.Trim())
                    {
                        case "1":
                        case "4":
                        case "3":
                            LenD.Rows[e.RowIndex]["price"] = (mnyN / qty).ToDecimal("f" + Common.MS);
                            break;
                        case "2":
                            LenD.Rows[e.RowIndex]["price"] = ((mnyN * (1 + Common.Sys_Rate)) / qty).ToDecimal("f" + Common.MS);
                            break;
                    }
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.折數.Name)
            {
                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                LenD.Rows[e.RowIndex]["Prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");

                SetRow_TaxPrice(LenD.Rows[e.RowIndex]);
                SetRow_Mny(LenD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.包裝數量.Name)
            {
                if (dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal() == TextBefore.ToDecimal()) return;
                if (LenD.Rows[e.RowIndex]["qty"].ToDecimal() != LenD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有還入，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    LenD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                LenD.Rows[e.RowIndex]["itpkgqty"] = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                dataGridViewT1.InvalidateRow(e.RowIndex);
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
            else if (keyData == Keys.F2)
            {
                if (gridAppend.Enabled) gridAppend_Click(null, null);
            }
            else if (keyData == Keys.F3)
            {
                if (gridDelete.Enabled) gridDelete_Click(null, null);
            }
            else if (keyData == Keys.F5)
            {
                if (gridInsert.Enabled) gridInsert_Click(null, null);
            }
            else if (keyData == Keys.F6)
            {
                if (gridItDesp.Enabled) gridItDesp_Click(null, null);
            }
            else if (keyData == Keys.F7)
            {
                if (gridBomD.Enabled) gridBomD_Click(null, null);
            }
            else if (keyData == Keys.F8)
            {
                if (gridStock.Enabled) gridStock_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift"))
            {
                if (gridTran.Enabled) gridTran_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F10") && keyData.ToString().EndsWith("Shift"))
            {
                if (gridAllTrans.Enabled) gridAllTrans_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F11") && keyData.ToString().EndsWith("Shift"))
            {
                if (gridItBuyPrice.Enabled) gridItBuyPrice_Click(null, null);
            }
            else if (keyData == Keys.Escape)
            {
                if (btnExit.Enabled) btnExit_Click(null, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "借出數量" || dataGridViewT1.Columns[e.ColumnIndex].Name == "售價" || dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
                toolStripStatusLabel1.Text = "1.新增 2.修改 3.刪除 4.瀏覽 0.結束";
        }

        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ToolTip tip = new ToolTip();
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "借出倉庫" || str == "備註說明" || str == "售價")
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

        void t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F12)
            {
                dataGridViewT1_CellDoubleClick(dataGridViewT1, new DataGridViewCellEventArgs(dataGridViewT1.CurrentCell.ColumnIndex, dataGridViewT1.CurrentCell.RowIndex));
            }
        }

    }
}
