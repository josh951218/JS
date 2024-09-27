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
    public partial class FrmBorrowNew : Formbase
    {
        JBS.JS.Borr jBorr;

        DataTable dtBorrD = new DataTable();
        DataTable tempBorrD = new DataTable();
        DataTable dtBom = new DataTable();
        DataTable tempBom = new DataTable();

        List<TextBoxbase> list;
        List<ButtonGridT> AllGridButtons;

        string TextBefore = "";
        string ItNoBegin = "";
        string UdfNoBegin = "";
        string Memo1 = "";
        decimal BomRec = 0;

        public FrmBorrowNew()
        {
            InitializeComponent();
            this.jBorr = new JBS.JS.Borr();
            this.list = this.getEnumMember();

            BoDate.SetDateLength();
            pVar.SetMemoUdf(this.備註說明);

            AllGridButtons = new List<ButtonGridT> { gridAppend, gridDelete, gridPicture, gridInsert, gridDesp, gridBom, btnKeyMan, btnMemo1 };

            //金額權限
            Xa1Par.Visible = Common.User_ShopPrice;
            TaxMnyb.Visible = Common.User_ShopPrice;
            TaxMny.Visible = Common.User_ShopPrice;
            Tax.Visible = Common.User_ShopPrice;
            TotMny.Visible = Common.User_ShopPrice;
            X3No.Visible = Common.User_ShopPrice;
            X3Name.Visible = Common.User_ShopPrice;
            Rate.Visible = Common.User_ShopPrice;
            this.進價.Visible = Common.User_ShopPrice;
            this.稅前進價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;
            this.本幣單價.Visible = Common.User_ShopPrice;
            this.本幣稅前單價.Visible = Common.User_ShopPrice;
            this.本幣稅前金額.Visible = Common.User_ShopPrice;

            Xa1Par.LastNum = 4;
            Xa1Par.FirstNum = 4;
            TaxMnyb.FirstNum = 19 - Common.M;
            TaxMnyb.LastNum = Common.M;
            TaxMny.Set進貨單據小數();
            Tax.Set進項稅額小數();
            TotMny.Set進貨單據小數();

            //this.計價數量.Set庫存數量小數();
            this.借入數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.進價.Set銷貨單價小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前進價.FirstNum = 9;
            this.稅前進價.LastNum = 6;
            this.稅前進價.DefaultCellStyle.Format = "f6";
            this.稅前金額.Set銷項金額小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前單價.FirstNum = 9;
            this.本幣稅前單價.LastNum = 6;
            this.本幣稅前單價.DefaultCellStyle.Format = "f6";
            this.本幣稅前金額.Set本幣金額小數();
            this.借入未還量.Set庫存數量小數();

            if (Common.Sys_DBqty == 1)
            {
                //this.計價數量.Visible = false;
                //this.計位.Visible = false;
            }

            if (Common.Series == "74")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                StNo.Enabled = false;
                StName.Enabled = false;
                EmNo.Validating += new CancelEventHandler(Xa1Par_Validating);
                //this.訂單憑證.Visible = false;
            }
            if (Common.Series == "73")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
                StNo.Enabled = false;
                StName.Enabled = false;
                EmNo.Validating += new CancelEventHandler(Xa1Par_Validating);
            }
            dtBorrD.RowChanged += new DataRowChangeEventHandler(dtBorrD_RowChanged);
            dataGridViewT1.DataSource = dtBorrD;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        void dtBorrD_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                e.Row["序號"] = e.Row.Table.Rows.Count;
            }
        }

        private void Xa1Par_Validating(object sender, CancelEventArgs e)
        {
            if (((TextBox)sender).ReadOnly) return;
            if (Xa1Par.ReadOnly != true && Xa1Par.Text.Trim() != "" && dataGridViewT1.Rows.Count > 0)
            {
                //離開匯率設定，重新計算本幣金額
                for (int i = 0; i < dtBorrD.Rows.Count; i++)
                {
                    SetRow_Mny(dtBorrD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();
            }

            if (Common.keyData != Keys.Up)
            {
                if (FaNo.Text.Trim() != "")
                    if (dataGridViewT1.Rows.Count == 0)
                        if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
            }
        }

        private void FrmBorrowNew_Load(object sender, EventArgs e)
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
                    end,自定編號='',序號='',ItNoUdf = '',*
                    from BorrD where 1=0 ";

                da.Fill(dtBorrD);
                da.Fill(tempBorrD);
            }

            var pk = jBorr.Bottom();
            writeToTxt(pk);
        }

        private void FrmBorrowNew_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string bono)
        {
            BomRec = 0;

            var result = jBorr.LoadData(bono, row =>
            {
                BoNo.Text = row["BoNo"].ToString().Trim();
                var date = (Common.User_DateTime == 1) ? "" : "1";
                BoDate.Text = row["BoDate" + date].ToString();

                FaNo.Text = row["FaNo"].ToString().Trim();
                FaName1.Text = row["FaName1"].ToString().Trim();
                StNoo.Text = "BIN";
                StNameo.Text = "BIN";
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
                Xa1No.Text = row["Xa1No"].ToString().Trim();
                Xa1Name.Text = row["Xa1Name"].ToString().Trim();
                Xa1Par.Text = row["Xa1Par"].ToDecimal().ToString("f4");

                BoMemo.Text = row["BoMemo"].ToString().Trim();
                TaxMnyb.Text = row["TaxMnyb"].ToDecimal().ToString("f" + TaxMnyb.LastNum);
                TaxMny.Text = row["TaxMny"].ToDecimal().ToString("f" + TaxMny.LastNum);
                Tax.Text = row["Tax"].ToDecimal().ToString("f" + Tax.LastNum);
                TotMny.Text = row["TotMny"].ToDecimal().ToString("f" + TotMny.LastNum);
                Rate.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");
                pVar.XX03Validate(row["X3No"].ToString().Trim(), X3No, X3Name);

                loadBorrD();

                booverflag.Checked = (row["booverflag"].ToString() == "True");

                this.Memo1 = row["bomemo1"].ToString();
                jBorr.keyMan.Set(row);
            });

            if (!result)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                dtBorrD.Clear();
                tempBorrD.Clear();
                dtBom.Clear();
                tempBom.Clear();

                booverflag.Checked = false;

                this.Memo1 = "";
                jBorr.keyMan.Clear();
            }
        }

        void loadBorrD()
        {
            dtBorrD.Clear();
            tempBorrD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("BoNo", BoNo.Text.Trim());
                    cmd.CommandText = @"
                        Select 產品組成=
                        case
                        when ittrait=1 then '組合品'
                        when ittrait=2 then '組裝品'
                        when ittrait=3 then '單一商品'
                        end,自定編號='',序號='',ItNoUdf = (select top 1 itnoudf from item where item.itno = BorrD.itno),*
                        from BorrD where BoNo=(@BoNo)
                        order by borrd.recordno ";

                    da.Fill(dtBorrD);
                    da.Fill(tempBorrD);
                }
                if (dtBorrD.Rows.Count > 0) BomRec = dtBorrD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadBorrBom()
        {
            dtBom.Clear();
            tempBom.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = conn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    string sql = "";
                    if (FormState == FormEditState.Append) sql = "select top 1 * from BorrBom where 1=0";
                    else if (FormState == FormEditState.Duplicate) sql = "select * from BorrBom where BoNo=@BoNo ";
                    else if (FormState == FormEditState.Modify) sql = "select * from BorrBom where BoNo=@BoNo ";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@BoNo", jBorr.GetCurrent());
                    cmd.CommandText = sql;

                    da.Fill(dtBom);
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
            var pk = jBorr.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jBorr.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jBorr.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jBorr.Bottom();
            writeToTxt(pk);
        }

        decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
        }

        void setTxtWhenAppend()
        {
            this.BomRec = 0;
            Memo1 = "";
            TaxMnyb.Text = (0M).ToString("f" + TaxMnyb.LastNum);
            TaxMny.Text = (0M).ToString("f" + TaxMny.LastNum);
            Tax.Text = (0M).ToString("f" + Tax.LastNum);
            TotMny.Text = (0M).ToString("f" + TotMny.LastNum);

            StNo.Text = Common.User_StkNo;
            pVar.StkValidate(Common.User_StkNo, StNo, StName);
            StNoo.Text = "BIN";
            pVar.StkValidate("BIN", StNoo, StNameo);

            Xa1No.Text = "TWD";
            pVar.Xa01Validate("TWD", Xa1No, Xa1Name);
            Xa1Par.Text = "1.000";

            X3No.Text = "1";
            pVar.XX03Validate("1", X3No, X3Name, Rate);
            BoDate.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            booverflag.Checked = false;

            dtBorrD.Clear();
            loadBorrBom();

            setTxtWhenAppend();
            BoDate.Focus();
            this.自定編號.ReadOnly = true;
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (BoNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            booverflag.Checked = false;

            BoNo.Text = "";
            BoDate.Text = Date.GetDateTime(Common.User_DateTime);

            loadBorrBom();
            for (int i = 0; i < dtBorrD.Rows.Count; i++)
            {
                dtBorrD.Rows[i]["qtynotout"] = dtBorrD.Rows[i]["qty"].ToString();
            }
            BoDate.Focus();
            BoDate.SelectAll();
            this.自定編號.ReadOnly = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (BoNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jBorr.IsExistDocument<JBS.JS.Borr>(BoNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }

            if (jBorr.IsEditInCloseDay(BoDate.Text) == false)
                return;

            if (jBorr.IsModify<JBS.JS.Borr>(BoNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jBorr.upModify1<JBS.JS.Borr>(BoNo.Text.Trim());//更新修改狀態1
                var pk = jBorr.Renew();//刷新資料
                writeToTxt(pk);
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            loadBorrBom();
            BoNo.Focus();
            BoNo.SelectAll();
            this.自定編號.ReadOnly = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (BoNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jBorr.IsExistDocument<JBS.JS.Borr>(BoNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jBorr.IsModify<JBS.JS.Borr>(BoNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jBorr.IsEditInCloseDay(BoDate.Text) == false)
                return;

            jBorr.GetTempBomOnDeleting("BorrBom", BoNo.Text.Trim(), ref tempBom);

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
                    cmd.Parameters.AddWithValue("BoNo", BoNo.Text.Trim());
                    cmd.CommandText = @"
                        delete from BorrBom where BoNo=(@BoNo);
                        delete from BorrD   where BoNo=(@BoNo);
                        delete from Borr    where BoNo=(@BoNo); ";
                    cmd.ExecuteNonQuery();

                    jBorr.加庫存(cmd, tempBorrD, tempBom, "stnoo");
                    jBorr.扣庫存(cmd, tempBorrD, tempBom, "stno");

                    tn.Commit();

                    jBorr.UpdateItemItStockQty(tempBorrD, tempBom);

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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (BoNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new FrmBorrow_Print())
            {
                frm.PK = BoNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (BoNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new S2.FrmBorrowBrowNew())
            {
                frm.TSeekNo = BoNo.Text.Trim();
                frm.ShowDialog();

                writeToTxt(frm.TResult);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (jBorr.IsEditInCloseDay(BoDate.Text) == false)
                return;

            if (jBorr.IsRegisted() == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！" + Environment.NewLine + "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (FaNo.TrimTextLenth() == 0)
            {
                FaNo.Focus();
                MessageBox.Show("廠商編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (StNo.TrimTextLenth() == 0)
            {
                StNo.Focus();
                MessageBox.Show("倉庫編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //移除空行
            jBorr.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtBorrD, ref dtBom, SetAllMny);

            if (Common.TPF == Common.MFT && X3No.Text.ToDecimal() != 2)
            {
                var checktaxmny = dtBorrD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f" + Common.TPF));
                if (TaxMny.Text.ToDecimal() != checktaxmny)
                {
                    MessageBox.Show("稅前合計金額有誤！無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (FormState == FormEditState.Append || FormState == FormEditState.Duplicate)
            {
                string faname2 = "";
                string fatel = "";
                string faper1 = "";
                jBorr.Validate<JBS.JS.Fact>(FaNo.Text.Trim(), reader =>
                {
                    faname2 = reader["faname2"].ToString().Trim();
                    fatel = reader["fatel1"].ToString().Trim();
                    faper1 = reader["faper1"].ToString().Trim().GetUTF8(10);
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

                        if (jBorr.GetPkNumber<JBS.JS.Borr>(cmd, BoDate.Text, ref BoNo) == false)
                        {
                            tn.Rollback();

                            MessageBox.Show("單據號碼取得失敗!");
                            return;
                        }

                        AppendMasterOnSaving(cmd, faname2, fatel, faper1);

                        for (int i = 0; i < dtBorrD.Rows.Count; i++)
                        {
                            AppendDetailOnSaving(cmd, i);
                        }

                        AppendBomOnSaving(cmd);

                        jBorr.加庫存(cmd, dtBorrD, dtBom, "stno");
                        jBorr.扣庫存(cmd, dtBorrD, dtBom, "stnoo");

                        tn.Commit();

                        jBorr.Save(BoNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jBorr.UpdateItemItStockQty(dtBorrD, dtBom);
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
                    using (var frm = new FrmBorrow_Print())
                    {
                        frm.PK = jBorr.GetCurrent();
                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);
            }
            else if (FormState == FormEditState.Modify)
            {
                if (jBorr.IsExistDocument<JBS.JS.Borr>(BoNo.Text.Trim()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
                    return;
                }

                string faname2 = "";
                string fatel = "";
                string faper1 = "";
                jBorr.Validate<JBS.JS.Fact>(FaNo.Text.Trim(), reader =>
                {
                    faname2 = reader["faname2"].ToString().Trim();
                    fatel = reader["fatel1"].ToString().Trim();
                    faper1 = reader["faper1"].ToString().Trim().GetUTF8(10);
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

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("bono", BoNo.Text.Trim());
                        cmd.CommandText = "Delete from BorrD where bono = (@bono);Delete from BorrBom where bono = (@bono);";
                        cmd.ExecuteNonQuery();

                        UpdateMaster(cmd, faname2, fatel, faper1);

                        for (int i = 0; i < dtBorrD.Rows.Count; i++)
                        {
                            this.AppendDetailOnSaving(cmd, i);
                        }

                        this.AppendBomOnSaving(cmd);

                        jBorr.加庫存(cmd, tempBorrD, tempBom, "stnoo");
                        jBorr.扣庫存(cmd, tempBorrD, tempBom, "stno");

                        jBorr.加庫存(cmd, dtBorrD, dtBom, "stno");
                        jBorr.扣庫存(cmd, dtBorrD, dtBom, "stnoo");

                        tn.Commit();

                        jBorr.Save(BoNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jBorr.UpdateItemItStockQty(tempBorrD, tempBom, dtBorrD, dtBom);
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
                    using (var frm = new FrmBorrow_Print())
                    {
                        frm.PK = jBorr.GetCurrent();
                        frm.ShowDialog();
                    }
                }
                jBorr.upModify0<JBS.JS.Borr>(BoNo.Text.Trim());//更新修改狀態0
                if (tk != null)
                    tk.Wait();

                btnAppend_Click(null, null);
            }
        }
        private void AppendMasterOnSaving(SqlCommand cmd, string faname2, string fatel, string faper1)
        {
            cmd.Parameters.AddWithValue("bono", BoNo.Text.Trim());
            cmd.Parameters.AddWithValue("bodate", Date.ToTWDate(BoDate.Text));
            cmd.Parameters.AddWithValue("bodate1", Date.ToUSDate(BoDate.Text));
            cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
            cmd.Parameters.AddWithValue("faname1", FaName1.Text.Trim());
            cmd.Parameters.AddWithValue("faname2", faname2);
            cmd.Parameters.AddWithValue("fatel1", fatel);
            cmd.Parameters.AddWithValue("faper1", faper1);
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.ToDecimal());
            cmd.Parameters.AddWithValue("taxmnyb", TaxMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToDecimal("f2"));
            cmd.Parameters.AddWithValue("tax", Tax.Text.ToDecimal());
            cmd.Parameters.AddWithValue("totmny", TotMny.Text.ToDecimal());
            cmd.Parameters.AddWithValue("taxb", Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("totmnyb", TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("recordno", dtBorrD.Rows.Count);
            cmd.Parameters.AddWithValue("bomemo", BoMemo.Text.Trim());
            cmd.Parameters.AddWithValue("bomemo1", Memo1.Trim());
            cmd.Parameters.AddWithValue("AppScNo", Common.User_Name);
            cmd.Parameters.AddWithValue("AppDate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("EdtScNo", Common.User_Name);
            cmd.Parameters.AddWithValue("EdtDate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("stnoo", StNoo.Text.Trim());
            cmd.Parameters.AddWithValue("stnameo", StNameo.Text.Trim());
            //
            cmd.Parameters.AddWithValue("itno", "");
            cmd.Parameters.AddWithValue("itname", "");
            cmd.Parameters.AddWithValue("ittrait", "");
            cmd.Parameters.AddWithValue("itunit", "");
            cmd.Parameters.AddWithValue("itpkgqty", 1);
            cmd.Parameters.AddWithValue("qty", 0);
            cmd.Parameters.AddWithValue("price", 0);
            cmd.Parameters.AddWithValue("prs", 1);
            cmd.Parameters.AddWithValue("taxprice", 0);
            cmd.Parameters.AddWithValue("mny", 0);
            cmd.Parameters.AddWithValue("priceb", 0);
            cmd.Parameters.AddWithValue("taxpriceb", 0);
            cmd.Parameters.AddWithValue("mnyb", 0);
            cmd.Parameters.AddWithValue("memo", "");
            cmd.Parameters.AddWithValue("bomid", "");
            cmd.Parameters.AddWithValue("bomrec", "");
            cmd.Parameters.AddWithValue("recordno1", 1);
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
            //
            cmd.Parameters.AddWithValue("itqty", 0);
            cmd.Parameters.AddWithValue("itpareprs", 1);
            cmd.Parameters.AddWithValue("itrec", 0);
            cmd.Parameters.AddWithValue("itprice", 0);
            cmd.Parameters.AddWithValue("itprs", 1);
            cmd.Parameters.AddWithValue("itmny", 0);
            cmd.Parameters.AddWithValue("itnote", "");
            cmd.Parameters.AddWithValue("booverflag", booverflag.Checked.ToString());
            cmd.Parameters.AddWithValue("qtynotout", 0);



            cmd.CommandText = " INSERT INTO [dbo].[borr]"
            + " ([bono],[bodate],[bodate1],[fano],[faname1],[faname2],[fatel1],[faper1],[stno],[stname],[emno],[emname],[xa1no],[xa1name],[xa1par]"
            + " ,[taxmny],[taxmnyb],[x3no],[rate],[tax],[totmny],[taxb],[totmnyb],[recordno],[bomemo],[bomemo1],[AppScNo],[AppDate],[EdtScNo],[EdtDate],[stnoo],[stnameo],[booverflag])"
            + " VALUES"
            + " (@bono,@bodate,@bodate1,@fano,@faname1,@faname2,@fatel1,@faper1,@stno,@stname,@emno,@emname,@xa1no,@xa1name,@xa1par"
            + " ,@taxmny,@taxmnyb,@x3no,@rate,@tax,@totmny,@taxb,@totmnyb,@recordno,@bomemo,@bomemo1,@AppScNo,@AppDate,@EdtScNo,@EdtDate,@stnoo,@stnameo,@booverflag)";
            cmd.ExecuteNonQuery();
        }
        private void AppendDetailOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters["itno"].Value = dtBorrD.Rows[i]["itno"].ToString().Trim();
            cmd.Parameters["itname"].Value = dtBorrD.Rows[i]["itname"].ToString();
            cmd.Parameters["ittrait"].Value = dtBorrD.Rows[i]["ittrait"].ToString().Trim();
            cmd.Parameters["itunit"].Value = dtBorrD.Rows[i]["itunit"].ToString().Trim();
            cmd.Parameters["itpkgqty"].Value = dtBorrD.Rows[i]["itpkgqty"].ToDecimal();
            cmd.Parameters["qty"].Value = dtBorrD.Rows[i]["qty"].ToDecimal();
            cmd.Parameters["price"].Value = dtBorrD.Rows[i]["price"].ToDecimal();
            cmd.Parameters["prs"].Value = dtBorrD.Rows[i]["prs"].ToDecimal();
            cmd.Parameters["stno"].Value = dtBorrD.Rows[i]["stno"].ToString().Trim();
            cmd.Parameters["stname"].Value = dtBorrD.Rows[i]["stname"].ToString().Trim();
            cmd.Parameters["stnoo"].Value = dtBorrD.Rows[i]["stnoo"].ToString().Trim();
            cmd.Parameters["stnameo"].Value = dtBorrD.Rows[i]["stnameo"].ToString().Trim();
            cmd.Parameters["taxprice"].Value = dtBorrD.Rows[i]["taxprice"].ToDecimal();
            cmd.Parameters["mny"].Value = dtBorrD.Rows[i]["mny"].ToDecimal();
            cmd.Parameters["priceb"].Value = dtBorrD.Rows[i]["priceb"].ToDecimal();
            cmd.Parameters["taxpriceb"].Value = dtBorrD.Rows[i]["taxpriceb"].ToDecimal();
            cmd.Parameters["mnyb"].Value = dtBorrD.Rows[i]["mnyb"].ToDecimal();
            cmd.Parameters["memo"].Value = dtBorrD.Rows[i]["memo"].ToString().Trim();

            var rec = dtBorrD.Rows[i]["bomrec"].ToString().Trim();
            var bomid = BoNo.Text.Trim() + rec.PadLeft(10, '0');

            cmd.Parameters["bomid"].Value = bomid;
            cmd.Parameters["bomrec"].Value = rec;
            cmd.Parameters["recordno1"].Value = (i + 1);
            cmd.Parameters["itdesp1"].Value = dtBorrD.Rows[i]["itdesp1"].ToString();
            cmd.Parameters["itdesp2"].Value = dtBorrD.Rows[i]["itdesp2"].ToString();
            cmd.Parameters["itdesp3"].Value = dtBorrD.Rows[i]["itdesp3"].ToString();
            cmd.Parameters["itdesp4"].Value = dtBorrD.Rows[i]["itdesp4"].ToString();
            cmd.Parameters["itdesp5"].Value = dtBorrD.Rows[i]["itdesp5"].ToString();
            cmd.Parameters["itdesp6"].Value = dtBorrD.Rows[i]["itdesp6"].ToString();
            cmd.Parameters["itdesp7"].Value = dtBorrD.Rows[i]["itdesp7"].ToString();
            cmd.Parameters["itdesp8"].Value = dtBorrD.Rows[i]["itdesp8"].ToString();
            cmd.Parameters["itdesp9"].Value = dtBorrD.Rows[i]["itdesp9"].ToString();
            cmd.Parameters["itdesp10"].Value = dtBorrD.Rows[i]["itdesp10"].ToString();
            cmd.Parameters["qtynotout"].Value = dtBorrD.Rows[i]["qtynotout"].ToDecimal();

            cmd.CommandText = " INSERT INTO [dbo].[borrd]"
            + " ([bono],[bodate],[bodate1],[fano],[stno],[stname],[emno],[xa1no],[xa1par],[itno],[itname],[ittrait],[itunit],[itpkgqty],[qty],[price],[prs],[rate]"
            + " ,[taxprice],[mny],[priceb],[taxpriceb],[mnyb],[memo],[bomid],[bomrec],[recordno],[itdesp1],[itdesp2],[itdesp3],[itdesp4],[itdesp5],[itdesp6],[itdesp7],[itdesp8],[itdesp9],[itdesp10],[stnoo],[stnameo],[qtynotout])"
            + " VALUES"
            + " (@bono,@bodate,@bodate1,@fano,@stno,@stname,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@itpkgqty,@qty,@price,@prs,@rate"
            + " ,@taxprice,@mny,@priceb,@taxpriceb,@mnyb,@memo,@bomid,@bomrec,@recordno1,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@stnoo,@stnameo,@qtynotout)";
            cmd.ExecuteNonQuery();
        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < dtBom.Rows.Count; i++)
            {
                var rec = dtBom.Rows[i]["bomrec"].ToString().Trim();
                var bomid = BoNo.Text.Trim() + rec.PadLeft(10, '0');

                cmd.Parameters["bomid"].Value = bomid;
                cmd.Parameters["bomrec"].Value = rec;
                cmd.Parameters["itno"].Value = dtBom.Rows[i]["itno"].ToString().Trim();
                cmd.Parameters["itname"].Value = dtBom.Rows[i]["itname"].ToString();
                cmd.Parameters["itunit"].Value = dtBom.Rows[i]["itunit"].ToString().Trim();
                cmd.Parameters["itqty"].Value = dtBom.Rows[i]["itqty"].ToDecimal();
                cmd.Parameters["itpareprs"].Value = dtBom.Rows[i]["itpareprs"].ToDecimal();
                cmd.Parameters["itpkgqty"].Value = dtBom.Rows[i]["itpkgqty"].ToDecimal();
                cmd.Parameters["itrec"].Value = (i + 1).ToString();
                cmd.Parameters["itprice"].Value = dtBom.Rows[i]["itprice"].ToDecimal();
                cmd.Parameters["itprs"].Value = dtBom.Rows[i]["itprs"].ToDecimal();
                cmd.Parameters["itmny"].Value = dtBom.Rows[i]["itmny"].ToDecimal();
                cmd.Parameters["itnote"].Value = dtBom.Rows[i]["itnote"].ToString().Trim();

                cmd.CommandText = "INSERT INTO [dbo].[borrbom]"
                + " ([bono],[bomid],[bomrec],[itno],[itname],[itunit],[itqty],[itpareprs],[itpkgqty],[itrec],[itprice],[itprs],[itmny],[itnote])"
                + " VALUES"
                + " (@bono,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote)";
                cmd.ExecuteNonQuery();
            }
        }
        private void UpdateMaster(SqlCommand cmd, string faname2, string fatel, string faper1)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("bono", BoNo.Text.Trim());
            cmd.Parameters.AddWithValue("bodate", Date.ToTWDate(BoDate.Text));
            cmd.Parameters.AddWithValue("bodate1", Date.ToUSDate(BoDate.Text));
            cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
            cmd.Parameters.AddWithValue("faname1", FaName1.Text.Trim());
            cmd.Parameters.AddWithValue("faname2", faname2);
            cmd.Parameters.AddWithValue("fatel1", fatel);
            cmd.Parameters.AddWithValue("faper1", faper1);
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
            cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.ToDecimal());
            cmd.Parameters.AddWithValue("taxmnyb", TaxMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToDecimal("f2"));
            cmd.Parameters.AddWithValue("tax", Tax.Text.ToDecimal());
            cmd.Parameters.AddWithValue("totmny", TotMny.Text.ToDecimal());
            cmd.Parameters.AddWithValue("taxb", Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("totmnyb", TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal());
            cmd.Parameters.AddWithValue("recordno", dtBorrD.Rows.Count);
            cmd.Parameters.AddWithValue("bomemo", BoMemo.Text.Trim());
            cmd.Parameters.AddWithValue("bomemo1", Memo1.Trim());
            cmd.Parameters.AddWithValue("EdtScNo", Common.User_Name);
            cmd.Parameters.AddWithValue("EdtDate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("stnoo", StNoo.Text.Trim());
            cmd.Parameters.AddWithValue("stnameo", StNameo.Text.Trim());
            //
            cmd.Parameters.AddWithValue("itno", "");
            cmd.Parameters.AddWithValue("itname", "");
            cmd.Parameters.AddWithValue("ittrait", "");
            cmd.Parameters.AddWithValue("itunit", "");
            cmd.Parameters.AddWithValue("itpkgqty", 1);
            cmd.Parameters.AddWithValue("qty", 0);
            cmd.Parameters.AddWithValue("price", 0);
            cmd.Parameters.AddWithValue("prs", 1);
            cmd.Parameters.AddWithValue("taxprice", 0);
            cmd.Parameters.AddWithValue("mny", 0);
            cmd.Parameters.AddWithValue("priceb", 0);
            cmd.Parameters.AddWithValue("taxpriceb", 0);
            cmd.Parameters.AddWithValue("mnyb", 0);
            cmd.Parameters.AddWithValue("memo", "");
            cmd.Parameters.AddWithValue("bomid", "");
            cmd.Parameters.AddWithValue("bomrec", "");
            cmd.Parameters.AddWithValue("recordno1", 1);
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
            //
            cmd.Parameters.AddWithValue("itqty", 0);
            cmd.Parameters.AddWithValue("itpareprs", 1);
            cmd.Parameters.AddWithValue("itrec", 0);
            cmd.Parameters.AddWithValue("itprice", 0);
            cmd.Parameters.AddWithValue("itprs", 1);
            cmd.Parameters.AddWithValue("itmny", 0);
            cmd.Parameters.AddWithValue("itnote", "");
            cmd.Parameters.AddWithValue("booverflag", booverflag.Checked.ToString());
            cmd.Parameters.AddWithValue("qtynotout", 0);

            cmd.CommandText = @"
            UPDATE [dbo].[borr] SET 
                bodate = (@bodate),bodate1 = (@bodate1),fano = (@fano),faname1 = (@faname1),faname2 = (@faname2)
                ,fatel1 = (@fatel1),faper1 = (@faper1),stno = (@stno),stname = (@stname),emno = (@emno)
                ,emname = (@emname),xa1no = (@xa1no),xa1name = (@xa1name),xa1par = (@xa1par),taxmny = (@taxmny)
                ,taxmnyb = (@taxmnyb),x3no = (@x3no),rate = (@rate),tax = (@tax),totmny = (@totmny)
                ,taxb = (@taxb),totmnyb = (@totmnyb),recordno = (@recordno),bomemo = (@bomemo)
                ,bomemo1 = (@bomemo1),EdtScNo = (@EdtScNo),EdtDate = (@EdtDate),stnoo = (@stnoo)
                ,stnameo = (@stnameo),booverflag=(@booverflag)
            where bono = (@bono)";
            cmd.ExecuteNonQuery();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jBorr.Cancel();
            writeToTxt(pk);

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();

            btnKeyMan.Enabled = true;
            btnMemo1.Enabled = true;
            jBorr.upModify0<JBS.JS.Borr>(BoNo.Text.Trim());//更新修改狀態0
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void GetSystemPrice(DataRow row, int index)
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
                                    row["price"] = reader["itbuyprip"].ToDecimal("f" + Common.MF);
                                }
                                else
                                {
                                    row["price"] = reader["itbuypri"].ToDecimal("f" + Common.MF);
                                }
                                row["prs"] = "1.000";
                            }
                        }
                    }
                    if (Common.Sys_BuyPrice == 2)
                    {
                        //歷史進價(最後一次交易進價)
                        sql = " select * from bshopd where itno=(@itno)"
                            + " and itunit=(@itunit)"
                            + " and itpkgqty=(@itpkgqty)"
                            + " and fano=(@fano)"
                            + " order by bsdate desc";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@itunit", unit);
                            cmd.Parameters.AddWithValue("@itpkgqty", itpkgqty);
                            cmd.Parameters.AddWithValue("@fano", FaNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    row["prs"] = reader["prs"].ToDecimal("f3");
                                    row["price"] = reader["price"].ToDecimal("f" + Common.MF);
                                }
                            }
                        }
                    }
                    else if (Common.Sys_BuyPrice == 3)
                    {
                        //類別折數(取產品建檔進價/包裝進價，折數取進價等級建檔裡的折數)
                        sql = " select b.*,i.itno,f.fano from buygrad as b "
                            + " inner join item as i on b.kino=i.kino and itno=(@itno) "
                            + " inner join fact as f on b.x12no=f.fax12no and f.fano=(@fano) ";
                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@itno", itno);
                            cmd.Parameters.AddWithValue("@fano", FaNo.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) row["prs"] = reader["buprs"].ToDecimal("f3");
                            }
                        }
                    }
                    else if (Common.Sys_BuyPrice == 4)
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
            //var qty = row["PQty"].ToDecimal("f" + Common.Q);
            var qty = row["Qty"].ToDecimal("f" + Common.Q);
            var price = row["price"].ToDecimal("f" + Common.MF);
            var taxprice = row["taxprice"].ToDecimal("f6");

            var mny = qty * taxprice;
            row["mny"] = mny.ToDecimal("f" + Common.TPF);

            var par = Xa1Par.Text.Trim().ToDecimal();
            row["priceb"] = (price * par).ToDecimal("f" + Common.M);
            row["taxpriceb"] = (taxprice * par).ToDecimal("f6");
            row["mnyb"] = (mny * par).ToDecimal("f" + Common.TPF).ToDecimal("f" + Common.M);
        }

        void SetAllMny()
        {
            var tax = 0M;
            var par = Xa1Par.Text.ToDecimal();
            var sum = dtBorrD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f"+Common.TPF)).ToDecimal("f" + Common.MFT);

            if (X3No.Text.ToInteger() == 1)
            {
                tax = (sum * Common.Sys_Rate).ToDecimal("f" + Common.TF);
                TaxMny.Text = sum.ToString("f" + Common.MFT);
                TaxMnyb.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = tax.ToString("f" + Common.TF);
                TotMny.Text = (sum + tax).ToString("f" + Common.MFT);
            }
            else if (X3No.Text.ToInteger() == 2)
            {
                var totmny = dtBorrD.AsEnumerable().Sum(r => r["qty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MF)).ToDecimal("f" + Common.MFT);
                tax = (totmny / (1 + Common.Sys_Rate)) * Common.Sys_Rate;

                TotMny.Text = totmny.ToString("f" + Common.MFT);
                tax = tax.ToDecimal("f" + Common.TF);
                Tax.Text = tax.ToString();
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MFT);
                TaxMnyb.Text = (TaxMny.Text.ToDecimal() * par).ToString("f" + Common.M);
            }
            else if (X3No.Text.ToInteger() == 3 || X3No.Text.ToInteger() == 4)
            {
                TaxMny.Text = sum.ToString("f" + Common.MFT);
                TaxMnyb.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = tax.ToString("f" + Common.TF);
                TotMny.Text = sum.ToString("f" + Common.MFT);
            }
        }

        void GridSaleDAddRows()
        {
            DataRow dRow = dtBorrD.NewRow();
            dRow["itno"] = "";
            dRow["自定編號"] = "";
            dRow["itname"] = "";
            dRow["Qty"] = 0;
            //dRow["PQty"] = 0;
            dRow["itunit"] = "";
            //dRow["Punit"] = "";
            dRow["Price"] = 0;
            dRow["TaxPrice"] = 0;
            dRow["Mny"] = 0;
            dRow["ItPkgQty"] = 1;
            dRow["ItTrait"] = 0;
            dRow["產品組成"] = "";
            dRow["Memo"] = "";
            dRow["PriceB"] = 0;
            dRow["TaxPriceB"] = 0;
            dRow["MnyB"] = 0;
            //折數
            dRow["Prs"] = "1.00";
            //結構編號
            dRow["BomRec"] = GetBomRec();
            //倉庫帶預設倉
            dRow["StNo"] = StNo.Text;
            dRow["StName"] = StName.Text;
            dRow["StNoo"] = StNoo.Text;
            dRow["StNameo"] = StNameo.Text;
            dRow["qtynotout"] = 0;

            dtBorrD.Rows.Add(dRow);
            dtBorrD.AcceptChanges();
        }

        void GridSaleDInsertRows(int index)
        {
            DataRow dRow = dtBorrD.NewRow();
            dRow["itno"] = "";
            dRow["自定編號"] = "";
            dRow["itname"] = "";
            dRow["Qty"] = 0;
            //dRow["PQty"] = 0;
            dRow["itunit"] = "";
            //dRow["Punit"] = "";
            dRow["Price"] = 0;
            dRow["TaxPrice"] = 0;
            dRow["Mny"] = 0;
            dRow["ItPkgQty"] = 1;
            dRow["ItTrait"] = 0;
            dRow["產品組成"] = "";
            dRow["Memo"] = "";
            dRow["PriceB"] = 0;
            dRow["TaxPriceB"] = 0;
            dRow["MnyB"] = 0;
            //折數
            dRow["Prs"] = "1.00";
            //結構編號
            dRow["BomRec"] = GetBomRec();
            //倉庫帶預設倉
            dRow["StNo"] = StNo.Text;
            dRow["StName"] = StName.Text;
            dRow["StNoo"] = StNoo.Text;
            dRow["StNameo"] = StNameo.Text;
            dRow["qtynotout"] = 0;

            dtBorrD.Rows.InsertAt(dRow, index);
            dtBorrD.AcceptChanges();
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            if (FaNo.Text.Trim() == "")
            {
                FaNo.Focus();
                MessageBox.Show("廠商編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                GridSaleDAddRows();
                string str = (Common.Series == "74") ? "產品編號" : "產品編號";
                dataGridViewT1.CurrentCell = dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Cells[str];
                dataGridViewT1.CurrentRow.Selected = true;
            }
            dataGridViewT1.Focus();
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            gridDelete.Focus();

            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1.SelectedRows != null)
                {
                    if (dataGridViewT1.SelectedRows[0].Cells["借入數量"].Value.ToDecimal() != dataGridViewT1.SelectedRows[0].Cells["借入未還量"].Value.ToDecimal())
                    {
                        MessageBox.Show("此產品已有還出，無法刪除!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridViewT1.Focus();
                        return;
                    }
                }
                //刪除明細前，先刪除明細的『組件明細』
                var rec = dataGridViewT1.CurrentRow.Cells["結構編號"].Value.ToString().Trim();
                jBorr.RemoveBom(rec, ref dtBom);

                //刪除明細
                int index = dataGridViewT1.SelectedRows[0].Index;
                dtBorrD.Rows.RemoveAt(index);
                dtBorrD.AcceptChanges();

                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["序號", i].Value = (i + 1).ToString();
                }
                SetAllMny();//刪除列後，重新計算帳款

                if (dataGridViewT1.Rows.Count > 0)
                {
                    index = (index > dataGridViewT1.Rows.Count - 1) ? dataGridViewT1.Rows.Count - 1 : index;
                    string str = (Common.Series == "74") ? "產品編號" : "產品編號";
                    dataGridViewT1.CurrentCell = dataGridViewT1[str, index];
                    dataGridViewT1.Rows[index].Selected = true;
                }
            }
            dataGridViewT1.Focus();
        }

        private void gridPicture_Click(object sender, EventArgs e)
        {
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
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridInsert.Focus();
                if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    GridSaleDInsertRows(index);
                    string str = (Common.Series == "74") ? "產品編號" : "產品編號";
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells[str];
                    dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridDesp_Click(object sender, EventArgs e)
        {
            gridDesp.Focus();
            using (JE.SOther.FrmDesp frm = new JE.SOther.FrmDesp(true, FormStyle.Mini))
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                {
                    dataGridViewT1.Focus();
                    return;
                }
                frm.dr = dtBorrD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridBom_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridBom.Focus();

                if (dataGridViewT1.SelectedRows != null && Common.Sys_LendToSaleMode == 2)
                {
                    if (dataGridViewT1.SelectedRows[0].Cells["借入數量"].Value.ToDecimal() != dataGridViewT1.SelectedRows[0].Cells["借入未還量"].Value.ToDecimal())
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

                using (subMenuFm_2.FrmSale_Bom frm = new subMenuFm_2.FrmSale_Bom())
                {
                    var rec = dataGridViewT1.SelectedRows[0].Cells["結構編號"].Value.ToString().Trim();
                    DataTable table = dtBom.Clone();

                    for (int i = 0; i < dtBom.Rows.Count; i++)
                    {
                        if (dtBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                        {
                            table.ImportRow(dtBom.Rows[i]);
                            dtBom.Rows.RemoveAt(i--);
                        }
                    }

                    table.AcceptChanges();
                    dtBom.AcceptChanges();

                    frm.dtD = table.Copy();
                    frm.BomRec = rec;
                    frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.grid = dataGridViewT1;
                    frm.上層Row = dtBorrD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (frm.CallBack == "Money")
                            {
                                dtBom.Merge(frm.dtD);
                                dtBorrD.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MF);
                                dataGridViewT1.Focus();
                                SetRow_TaxPrice(dtBorrD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetRow_Mny(dtBorrD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                                SetAllMny();
                                break;
                            }
                            else
                            {
                                dtBom.Merge(frm.dtD);
                                dtBom.AcceptChanges();
                                dataGridViewT1.Focus();
                                break;
                            }
                        case DialogResult.Cancel:
                            dtBom.Merge(table);
                            dtBom.AcceptChanges();
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
            var itno = dtBorrD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該廠商此產品交易 frm = new S2.Frm該廠商此產品交易())
            {
                frm.fano = FaNo.Text.Trim();
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
            var itno = dtBorrD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有廠商此產品交易 frm = new S2.Frm所有廠商此產品交易())
            {
                frm.fano = FaNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        void ReLoadFact(SqlDataReader row)
        {
            if (row != null)
            {
                FaNo.Text = row["FaNo"].ToString().Trim();
                FaName1.Text = row["FaName1"].ToString().Trim();
                Xa1No.Text = row["FaXa1no"].ToString();
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                X3No.Text = row["FaX3no"].ToString();
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                EmNo.Text = row["FaEmno1"].ToString();
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);

                for (int i = 0; i < dtBorrD.Rows.Count; i++)
                {
                    GetSystemPrice(dtBorrD.Rows[i], i);
                    SetRow_TaxPrice(dtBorrD.Rows[i]);
                    SetRow_Mny(dtBorrD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["FaNo"].ToString().Trim();
            }
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            if (FaNo.ReadOnly)
                return;

            if (this.FormState == FormEditState.Modify)
            {
                if (dtBorrD.AsEnumerable().Any(r => r["qtynotout"].ToDecimal() != r["qty"].ToDecimal()))
                {
                    MessageBox.Show("此單據已有轉單紀錄, 無法修改廠商!");
                    return;
                }
            }

            jBorr.Open<JBS.JS.Fact>(sender, row => ReLoadFact(row));
        }

        private void FaNo_Validating(object sender, CancelEventArgs e)
        {
            if (FaNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FaNo.TrimTextLenth() == 0)
            {
                e.Cancel = true;
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Clear();
                FaName1.Clear();
                return;
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (dtBorrD.AsEnumerable().Any(r => r["qtynotout"].ToDecimal() != r["qty"].ToDecimal()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據已有轉單紀錄, 無法修改廠商!");
                    return;
                }
            }
             
            jBorr.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                if (FaNo.Text.Trim() == TextBefore)
                    return;

                ReLoadFact(row);
            });

        }

        private void No_Enter(object sender, EventArgs e)
        {
            TextBefore = (sender as TextBox).Text;
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            if (X3No.ReadOnly)
                return;

            jBorr.Open<JBS.JS.XX03>(sender, reader =>
            {
                X3No.Text = reader["X3No"].ToString().Trim();
                X3Name.Text = reader["X3Name"].ToString();

                var rate = reader["X3Rate"].ToDecimal() * 100;
                Rate.Text = rate.ToString("f0");

                for (int i = 0; i < dtBorrD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtBorrD.Rows[i]);
                    SetRow_Mny(dtBorrD.Rows[i]);
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

            if (X3No.TrimTextLenth() == 0)
            {
                X3No.Text = X3Name.Text = Rate.Text = "";
                e.Cancel = true;
                MessageBox.Show("營業稅別不能為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                X3No.SelectAll();
                return;
            }

            jBorr.ValidateOpen<JBS.JS.XX03>(sender, e, row =>
            {
                if (X3No.Text == TextBefore)
                    return;

                X3No.Text = row["X3No"].ToString().Trim();
                X3Name.Text = row["X3Name"].ToString();

                var rate = row["X3Rate"].ToDecimal() * 100;
                Rate.Text = rate.ToString("f0");

                for (int i = 0; i < dtBorrD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(dtBorrD.Rows[i]);
                    SetRow_Mny(dtBorrD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                TextBefore = row["X3No"].ToString().Trim();
            });
        }

        private void BoDate_Validating(object sender, CancelEventArgs e)
        {
            if (BoDate.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (BoDate.Text.Trim() == "")
            {
                e.Cancel = true;
                MessageBox.Show("日期不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BoDate.Clear();
                return;
            }

            if (!BoDate.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("輸入日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BoDate.SelectAll();
                return;
            }
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jBorr.Open<JBS.JS.Empl>(sender, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (EmNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (EmNo.Text.Trim() == "")
            {
                EmNo.Text = "";
                EmName.Text = "";
                return;
            }

            jBorr.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        private void Tax_Validating(object sender, CancelEventArgs e)
        {
            if (Tax.ReadOnly)
                return;

            if (btnCancel.Focused)
                return;

            decimal taxmny = TaxMny.Text.ToDecimal();
            decimal tax = Tax.Text.ToDecimal();
            decimal totmny = TotMny.Text.ToDecimal();

            if (Common.Sys_X3Forward == 1 && X3No.Text.Trim() == "2")
            {
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MFT);
            }
            else
            {
                TotMny.Text = (taxmny + tax).ToString("f" + Common.MFT);
            }
            //TotMny.Text = (TaxMny.Text.ToDecimal() + Tax.Text.ToDecimal()).ToString("f" + Common.MFT);
        }

        private void btnKeyMan_Click(object sender, EventArgs e)
        {
            if (BoNo.Text.Trim() == "")
                return;

            using (var frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jBorr.keyMan.AppendMan;
                frm.ATime = jBorr.keyMan.AppendTime;
                //修改人員
                frm.EName = jBorr.keyMan.EditMan;
                frm.ETime = jBorr.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void btnMemo1_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = BoNo.ReadOnly ? false : true;
                frm.memo1 = Memo1;

                if (frm.ShowDialog() == DialogResult.OK) Memo1 = frm.memo1;
            }
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            AllGridButtons.ForEach(b => b.Enabled = !btnAppend.Enabled);
            booverflag.Enabled = !btnAppend.Enabled;
            dataGridViewT1.ReadOnly = btnAppend.Enabled;
            this.序號.ReadOnly = true;
            this.稅前進價.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.倉庫名稱.ReadOnly = true;
            this.借入未還量.ReadOnly = true;
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (FaNo.Text.Trim() != "")
            {
                if (dataGridViewT1.Rows.Count == 0)
                    if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
            }
            else
            {
                FaNo.Focus();
            }
        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //for (int i = 0; i < dtBorrD.Rows.Count; i++)
            //{
            //    dtBorrD.Rows[i]["序號"] = (i + 1).ToString();
            //    dataGridViewT1.InvalidateRow(i);
            //}
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.單位.Name)
            {
                TextBefore = dataGridViewT1[this.單位.Name, e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.稅前金額.Name)
            {
                TextBefore = dataGridViewT1[this.稅前金額.Name, e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.包裝數量.Name)
            {
                TextBefore = dataGridViewT1[this.包裝數量.Name, e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.產品編號.Name)
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jBorr.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.借入數量.Name)
            {
                TextBefore = dataGridViewT1[this.借入數量.Name, e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (BoNo.ReadOnly) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            var cellValue = dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim();

            if (CurrentColumnName == this.產品編號.Name)
            {
                #region 產品編號
                if (dtBorrD.Rows[e.RowIndex]["qty"].ToDecimal() != dtBorrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有還出，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jBorr.DataGridViewOpen<JBS.JS.Item>(sender, e, dtBorrD, row => FillItem(row, e.RowIndex));
                #endregion
            }
            else if (CurrentColumnName == this.借入倉庫.Name)
            {
                if (this.借入倉庫.ReadOnly) return;
                if (dtBorrD.Rows[e.RowIndex]["qty"].ToDecimal() != dtBorrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有還出，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jBorr.DataGridViewOpen<JBS.JS.Stkroom>(sender, e, dtBorrD, row =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = row["StNo"].ToString().Trim();

                    dtBorrD.Rows[e.RowIndex]["StNo"] = row["StNo"].ToString().Trim();
                    dtBorrD.Rows[e.RowIndex]["StName"] = row["StName"].ToString();
                });
            }
            else if (CurrentColumnName == this.單位.Name)
            {
                if (dtBorrD.Rows[e.RowIndex]["qty"].ToDecimal() != dtBorrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有還出，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var itno = dtBorrD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = cellValue;

                jBorr.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        dtBorrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            dtBorrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();
                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0) itpkgqty = 1;
                            dtBorrD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;

                dtBorrD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)//1代表一般進銷存
                {
                    GetSystemPrice(dtBorrD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtBorrD.Rows[e.RowIndex]);
                    SetRow_Mny(dtBorrD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }

            }
            else if (CurrentColumnName == this.備註說明.Name)
            {
                using (var frm = new subMenuFm_2.FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                        dtBorrD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            var cellValue = dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim();

            if (CurrentColumnName == 產品編號.Name)
            {
                #region 產品編號
                string ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoNow == "" || ItNoNow.Trim().Length == 0)
                {
                    if (btnSave.Focused) return;
                    dtBorrD.Rows[e.RowIndex]["itno"] = "";
                    dtBorrD.Rows[e.RowIndex]["自定編號"] = "";
                    dtBorrD.Rows[e.RowIndex]["itname"] = "";
                    dtBorrD.Rows[e.RowIndex]["itunit"] = "";

                    dtBorrD.Rows[e.RowIndex]["qty"] = 0;
                    dtBorrD.Rows[e.RowIndex]["Price"] = 0;
                    dtBorrD.Rows[e.RowIndex]["TaxPrice"] = 0;
                    dtBorrD.Rows[e.RowIndex]["Mny"] = 0;
                    dtBorrD.Rows[e.RowIndex]["ItPkgQty"] = 1;
                    dtBorrD.Rows[e.RowIndex]["ItTrait"] = 0;
                    dtBorrD.Rows[e.RowIndex]["產品組成"] = "";
                    dtBorrD.Rows[e.RowIndex]["Memo"] = "";
                    dtBorrD.Rows[e.RowIndex]["PriceB"] = 0;
                    dtBorrD.Rows[e.RowIndex]["TaxPriceB"] = 0;
                    dtBorrD.Rows[e.RowIndex]["MnyB"] = 0;
                    dtBorrD.Rows[e.RowIndex]["StNo"] = StNo.Text;
                    dtBorrD.Rows[e.RowIndex]["StName"] = StName.Text;
                    dtBorrD.Rows[e.RowIndex]["qtynotout"] = 0;

                    //折數
                    dtBorrD.Rows[e.RowIndex]["Prs"] = "1.00";

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();

                    var rec = dtBorrD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jBorr.RemoveBom(rec, ref dtBom);
                    return;
                }
                //值沒變->離開
                if (ItNoNow == ItNoBegin)
                    return;

                if (dtBorrD.Rows[e.RowIndex]["qty"].ToDecimal() != dtBorrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有還出，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    dtBorrD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                //值有變，但是跟自定編號一樣，視同沒變->離開                //把『自定編號』改成『產品編號』
                if (ItNoNow != ItNoBegin)
                {
                    if (ItNoNow == UdfNoBegin)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = ItNoBegin;
                        dtBorrD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        return;
                    }
                }

                //值變了，跟產品編號和自定編號都不一樣,帶值出來                //若找不到這筆資料則開窗
                if (ItNoNow != ItNoBegin && ItNoNow != UdfNoBegin)
                {
                    jBorr.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtBorrD, row => FillItem(row, e.RowIndex));
                }
                #endregion
            }
            else if (CurrentColumnName == 單位.Name)
            {
                string itno = dtBorrD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string unit = cellValue;

                if (TextBefore == unit)
                    return;

                if (dtBorrD.Rows[e.RowIndex]["qty"].ToDecimal() != dtBorrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有還出，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    dtBorrD.Rows[e.RowIndex]["itunit"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jBorr.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        dtBorrD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        dtBorrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                dtBorrD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)
                {
                    GetSystemPrice(dtBorrD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(dtBorrD.Rows[e.RowIndex]);
                    SetRow_Mny(dtBorrD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
            }
            else if (CurrentColumnName == 借入數量.Name)
            {
                if (dataGridViewT1["借入數量", e.RowIndex].EditedFormattedValue.ToString().Trim() == TextBefore) return;
                if (dtBorrD.Rows[e.RowIndex]["qty"].ToDecimal() != dtBorrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有還出，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    dtBorrD.Rows[e.RowIndex]["qty"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                var qty = dataGridViewT1["借入數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                if (Common.Sys_DBqty == 1)
                {
                    dtBorrD.Rows[e.RowIndex]["Qty"] = qty;
                    dtBorrD.Rows[e.RowIndex]["qtynotout"] = qty;
                }
                else if (Common.Sys_DBqty == 2)
                {
                    dtBorrD.Rows[e.RowIndex]["Qty"] = qty;
                    dtBorrD.Rows[e.RowIndex]["qtynotout"] = qty;
                }

                SetRow_Mny(dtBorrD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (CurrentColumnName == "進價")
            {
                dtBorrD.Rows[e.RowIndex]["Price"] = dataGridViewT1["進價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MF);

                SetRow_TaxPrice(dtBorrD.Rows[e.RowIndex]);
                SetRow_Mny(dtBorrD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (CurrentColumnName == "折數")
            {
                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                dtBorrD.Rows[e.RowIndex]["Prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");

                SetRow_TaxPrice(dtBorrD.Rows[e.RowIndex]);
                SetRow_Mny(dtBorrD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (CurrentColumnName == "稅前金額")
            {
                //正常情形『稅前金額』是由『進價』帶出來的
                //下面處理的情形是手動打上『稅前金額』
                //所以須往前推算『進價』金額。
                decimal price = 0;
                decimal qty = dataGridViewT1[this.借入數量.Name, e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                decimal taxprice = dataGridViewT1["稅前進價", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f6");
                decimal mny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.TPF);
                decimal prs = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");
                qty = (qty == 0) ? 1 : qty;
                if (TextBefore.ToDecimal() == mny) return;

                dtBorrD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / qty) / prs).ToDecimal("f" + Common.MF);
                        dtBorrD.Rows[e.RowIndex]["Price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / qty) / prs).ToDecimal("f" + Common.MF);
                        dtBorrD.Rows[e.RowIndex]["Price"] = price;
                        break;
                }
                SetRow_TaxPrice(dtBorrD.Rows[e.RowIndex]);

                taxprice = dtBorrD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                dtBorrD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                dtBorrD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                dtBorrD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (CurrentColumnName == 借入倉庫.Name)
            {
                if (dataGridViewT1.Columns["借入倉庫"].ReadOnly)
                    return;

                jBorr.DataGridViewValidateOpen<JBS.JS.Stkroom>(sender, e, dtBorrD, row =>
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = row["StNo"].ToString().Trim();

                    dtBorrD.Rows[e.RowIndex]["StNo"] = row["StNo"].ToString().Trim();
                    dtBorrD.Rows[e.RowIndex]["StName"] = row["StName"].ToString();
                });

            }
            else if (CurrentColumnName == 包裝數量.Name)
            {
                if (dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToString().Trim() == TextBefore) return;
                var itpkgqty = cellValue.ToDecimal("f" + Common.Q);
                if (dtBorrD.Rows[e.RowIndex]["qty"].ToDecimal() != dtBorrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有還出，無法更變資料!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    dtBorrD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                if (itpkgqty == 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("包裝數量不可為零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }

        private void FillItem(SqlDataReader reader, int index)
        {
            var itno = reader["ItNo"].ToString().Trim();

            this.ItNoBegin = itno;
            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;
            dtBorrD.Rows[index]["ItNo"] = itno;
            dtBorrD.Rows[index]["自定編號"] = reader["ItNoUdf"].ToString().Trim();
            dtBorrD.Rows[index]["ItName"] = reader["ItName"].ToString();
            dtBorrD.Rows[index]["ItNoUdf"] = reader["ItNoUdf"].ToString();
            //進貨常用單位
            var utype = reader["ItBuyUnit"].ToString().Trim();
            var unit = "";
            //預設帶包裝單位或是單位
            if (utype == "1")
            {
                unit = reader["ItUnitp"].ToString();
                dtBorrD.Rows[index]["ItUnit"] = unit;

                var itpkgqty = reader["ItPkgQty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0) itpkgqty = 1;
                dtBorrD.Rows[index]["ItPkgQty"] = itpkgqty;
            }
            else
            {
                unit = reader["ItUnit"].ToString();
                dtBorrD.Rows[index]["ItUnit"] = unit;
                dtBorrD.Rows[index]["itpkgqty"] = 1;
            }

            GetSystemPrice(dtBorrD.Rows[index], index);
            SetRow_TaxPrice(dtBorrD.Rows[index]);
            SetRow_Mny(dtBorrD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();

            //組合組裝品
            string trait = reader["ItTrait"].ToString();
            dtBorrD.Rows[index]["ItTrait"] = trait;

            if (trait == "1") trait = "組合品";
            else if (trait == "2") trait = "組裝品";
            else if (trait == "3") trait = "單一商品";
            dtBorrD.Rows[index]["產品組成"] = trait;

            //規格說明
            for (int x = 1; x <= 10; x++)
            {
                dtBorrD.Rows[index]["ItDesp" + x] = reader["ItDesp" + x];
            }

            //BOM
            var rec = dtBorrD.Rows[index]["BomRec"].ToString().Trim();
            jBorr.RemoveBom(rec, ref dtBom);
            jBorr.GetItemBom(itno, rec, ref dtBom);
        }

        private void dataGridViewT1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (this.FormState == FormEditState.None) return;
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            if (dtBorrD.Rows[e.RowIndex].RowState == DataRowState.Deleted)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.借入數量.Name)
            {
                if (dtBorrD.Rows[e.RowIndex]["ItNo"].ToString().Trim().Length == 0) return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dtBorrD.Rows[e.RowIndex]["ItNo"].ToString().Trim());
                    cmd.CommandText = "select ISNULL(itstockqty,0)itstockqty from item where itno=@itno";
                    toolStripStatusLabel1.Text = "現有庫存量：" + cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.Q);
                }
            }

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.進價.Name)
            {
                if (FaNo.TrimTextLenth() == 0) return;
                if (dtBorrD.Rows[e.RowIndex]["ItNo"].ToString().Trim().Length == 0) return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dtBorrD.Rows[e.RowIndex]["ItNo"].ToString().Trim());
                    cmd.Parameters.AddWithValue("itunit", dtBorrD.Rows[e.RowIndex]["ItUnit"].ToString().Trim());
                    cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("bsdate", Date.ToTWDate(BoDate.Text));
                    cmd.CommandText = "select top(1)taxprice from bshopd where "
                        + " itno=@itno "
                        + " and itunit=@itunit "
                        + " and fano=@fano "
                        + " and bsdate<=@bsdate "
                        + " order by bsdate desc,bomid desc";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        toolStripStatusLabel1.Text = "最後一次交易稅前進價：" + cmd.ExecuteScalar().ToDecimal().ToString("f" + Common.MF);
                    else
                        toolStripStatusLabel1.Text = "最後一次交易稅前進價：0";
                }
            }
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.單位.Name)
            {
                if (dtBorrD.Rows[e.RowIndex]["ItNo"].ToString().Trim().Length == 0) return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dtBorrD.Rows[e.RowIndex]["ItNo"].ToString().Trim());
                    cmd.CommandText = "select itunit,itunitp,itpkgqty from item where itno=@itno ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            toolStripStatusLabel1.Text = reader["itpkgqty"].ToDecimal() == 0 ? "" : reader["itpkgqty"].ToDecimal().ToString("f" + Common.Q);
                            toolStripStatusLabel1.Text += reader["itunit"].ToString().Trim() == "" ? " / " : reader["itunit"].ToString() + " / ";
                            toolStripStatusLabel1.Text += reader["itunitp"].ToString().Trim() == "" ? "未建檔" : reader["itunitp"].ToString();
                            toolStripStatusLabel1.Text += ",滑鼠左鍵快點兩下切換單位或自行輸入";
                        }
                    }
                }
            }
        }

        private void dataGridViewT1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (this.FormState == FormEditState.None) return;
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.借入數量.Name || dataGridViewT1.Columns[e.ColumnIndex].Name == this.進價.Name || dataGridViewT1.Columns[e.ColumnIndex].Name == this.單位.Name)
                toolStripStatusLabel1.Text = "1.新增 2.修改 3.刪除 4.瀏覽 0.結束";
        }

        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ToolTip tip = new ToolTip();
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == this.產品編號.Name || str == this.借入倉庫.Name || str == this.備註說明.Name)
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                t.KeyDown += new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
            }
            else if (str == this.借入數量.Name)
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
            //else if (str == "計價數量")
            //{
            //    if (Common.Sys_DBqty == 2)
            //    {
            //        t.KeyDown -= new KeyEventHandler(t_KeyDown);
            //        t.KeyDown += new KeyEventHandler(t_KeyDown);
            //        tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
            //    }
            //    else
            //    {
            //        t.KeyDown -= new KeyEventHandler(t_KeyDown);
            //        tip.SetToolTip(t, "");
            //    }
            //}
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
            else if (keyData == Keys.F6 && gridDesp.Enabled)
            {
                gridDesp_Click(null, null);
            }
            else if (keyData == Keys.F7 && gridBom.Enabled)
            {
                gridBom_Click(null, null);
            }
            else if (keyData == Keys.F8 && gridStock.Enabled)
            {
                gridStock_Click(null, null);
            }
            else if (keyData.ToString() == "F9, Shift" && gridTran.Enabled)
            {
                gridTran_Click(null, null);
            }
            else if (keyData.ToString() == "F10, Shift" && gridAllTrans.Enabled)
            {
                gridAllTrans_Click(null, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BoNo_Validating(object sender, CancelEventArgs e)
        {
            if (BoNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (BoNo.Text.Length > 0 && BoNo.Text.Trim() == "")
            {
                e.Cancel = true;
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BoNo.Clear();
                return;
            }

            if (FormState == FormEditState.Append)
            {
                if (jBorr.IsExistDocument<JBS.JS.Borr>(BoNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (FormState == FormEditState.Duplicate)
            {
                if (jBorr.IsExistDocument<JBS.JS.Borr>(BoNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            else if (FormState == FormEditState.Modify)
            {
                if (jBorr.IsExistDocument<JBS.JS.Borr>(BoNo.Text.Trim()))
                {
                    if (TextBefore == BoNo.Text.Trim())
                        return;

                    writeToTxt(BoNo.Text.Trim());
                    loadBorrBom();
                }
                else
                {
                    e.Cancel = true;
                    BoNo.SelectAll();

                    using (var frm = new FrmBorrow_Print_BoNo())
                    {
                        frm.TSeekNo = BoNo.Text.Trim();
                        if (DialogResult.OK == frm.ShowDialog())
                        {
                            writeToTxt(frm.TResult);
                            loadBorrBom();
                        }
                    }
                }
            }
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            if (StNo.Text.Trim() == "BIN")
            {
                MessageBox.Show("借入倉庫編號不可與廠商編號相同！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNo.Focus();
                StNo.SelectAll();
                return;
            }

            jBorr.Open<JBS.JS.Stkroom>(sender, reader =>
            {
                StNo.Text = reader["StNo"].ToString().Trim();
                StName.Text = reader["StName"].ToString().Trim();

                if (Common.Sys_StNoMode == 1)
                {
                    for (int i = 0; i < dtBorrD.Rows.Count; i++)
                    {
                        dtBorrD.Rows[i]["stno"] = StNo.Text;
                        dtBorrD.Rows[i]["StName"] = StName.Text;
                        dataGridViewT1.InvalidateRow(i);
                    }
                }

                this.TextBefore = reader["StNo"].ToString().Trim();
            });
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (StNo.Text.Trim() == "BIN")
            {
                e.Cancel = true;
                MessageBox.Show("借入倉庫編號不可與廠商倉庫相同！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNo.SelectAll();
                return;
            }

            if (StNo.Text.Trim() == "")
            {
                e.Cancel = true;
                MessageBox.Show("倉庫編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNo.Clear();
                StName.Clear();
                return;
            }

            jBorr.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                if (StNo.Text.Trim() == TextBefore)
                    return;

                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();

                if (Common.Sys_StNoMode == 1)
                {
                    for (int i = 0; i < dtBorrD.Rows.Count; i++)
                    {
                        dtBorrD.Rows[i]["stno"] = StNo.Text;
                        dtBorrD.Rows[i]["StName"] = StName.Text;
                        dataGridViewT1.InvalidateRow(i);
                    }
                }

                this.TextBefore = row["StNo"].ToString().Trim();
            });
        }
    }
}
