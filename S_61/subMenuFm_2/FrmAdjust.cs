using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using JE.SOther;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmAdjust : Formbase
    {
        JBS.JS.Adjust jAdjust;
        List<TextBoxbase> list = new List<TextBoxbase>();

        DataTable dtD = new DataTable();
        DataTable dtBom = new DataTable();
        DataTable tempdtD = new DataTable();
        List<Button> GridButton;

        decimal BomRec = 0;
        string ItNoBegin = "";
        string UdfNoBegin = "";
        string TextBefore = "";
        string Memo1 = "";

        public FrmAdjust()
        {
            InitializeComponent();
            this.jAdjust = new JBS.JS.Adjust();
            this.list = this.getEnumMember();

            pVar.SetMemoUdf(this.說明);

            GridButton = new List<Button> { gridAppend, gridBomD, gridDelete, gridInsert, gridItDesp, gridPicture };

            this.調整數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.成本.Set本幣金額小數();
            this.金額.Set本幣金額小數();

            this.成本.Visible = Common.User_ShopPrice;
            this.金額.Visible = Common.User_ShopPrice;
            TotMnyB.Visible = Common.User_ShopPrice;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
            dataGridViewT1.DataSource = dtD;
        }

        private void FrmTuShop_Load(object sender, EventArgs e)
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
                    from adjustd where 1=0";
                da.Fill(dtD);
                da.Fill(tempdtD);

                cmd.CommandText = @"select * from adjubom where 1=0 ";
                da.Fill(dtBom);
            }

            AdDate.SetDateLength();

            var pk = jAdjust.Bottom();
            writeToTxt(pk);
        }

        private void FrmAdjust_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void writeToTxt(string adno)
        {
            BomRec = 0;

            var result = jAdjust.LoadData(adno, reader =>
            {
                AdNo.Text = reader["adno"].ToString();
                AdDate.Text = Common.User_DateTime == 1 ? reader["addate"].ToString() : reader["addate1"].ToString();
                StNo.Text = reader["stno"].ToString();
                StName.Text = reader["stname"].ToString();
                EmNo.Text = reader["emno"].ToString();
                EmName.Text = reader["emname"].ToString();
                TotMnyB.Text = reader["totmnyb"].ToDecimal().ToString("f" + Common.M);
                AdMemo.Text = reader["admemo"].ToString();

                Memo1 = reader["admemo1"].ToString();
                bracket.Text = reader["bracket"].ToString().Trim();

                loadD();
                jAdjust.keyMan.Set(reader);
            });

            if (!result)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                dtD.Clear();
                dtBom.Clear();
                tempdtD.Clear();

                Memo1 = "";
                bracket.Text = "";

                jAdjust.keyMan.Clear();
            }
        }

        void loadD()
        {
            dtD.Clear();
            tempdtD.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    string sql = "select 產品組成="
                                 + " case"
                                 + " when ittrait=1 then '組合品'"
                                 + " when ittrait=2 then '組裝品'"
                                 + " when ittrait=3 then '單一商品'"
                                 + " end,ItNoUdf= (select top 1 itnoudf from item where item.itno = adjustd.itno),*"
                                 + " from adjustd where adno=@adno ORDER BY recordno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("adno", AdNo.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                        {
                            dd.Fill(dtD);
                            dd.Fill(tempdtD);
                            dataGridViewT1.DataSource = dtD;

                            if (dtD.Rows.Count > 0) BomRec = dtD.AsEnumerable().Max(r => r["BomRec"].ToDecimal());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadBomD()
        {
            dtBom.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    string sql = "";
                    if (this.FormState == FormEditState.Append) sql = "select * from adjubom where 1=0 ";
                    else if (this.FormState == FormEditState.Duplicate) sql = "select * from adjubom where adno=@adno";
                    else if (this.FormState == FormEditState.Modify) sql = "select * from adjubom where adno=@adno";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("adno", jAdjust.GetCurrent());
                    cmd.CommandText = sql;
                     
                    da.Fill(dtBom);
                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void GridAdjustDAddRows()
        {
            DataRow dr = dtD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 0;
            dr["itunit"] = "";
            dr["costb"] = 0;
            dr["mnyb"] = 0;
            dr["itpkgqty"] = 1;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["BomRec"] = GetBomRec();
            dtD.Rows.Add(dr);
            dtD.AcceptChanges();
        }

        void GridAdjustDAddRows(int index)
        {
            DataRow dr = dtD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 0;
            dr["itunit"] = "";
            dr["costb"] = 0;
            dr["mnyb"] = 0;
            dr["itpkgqty"] = 1;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["BomRec"] = GetBomRec();
            dtD.Rows.InsertAt(dr, index);
            dtD.AcceptChanges();
        }

        decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
        }

        void CheckMny(int index)
        {
            decimal _costb = dtD.Rows[index]["costb"].ToDecimal();
            decimal _qty = dtD.Rows[index]["qty"].ToDecimal();

            dtD.Rows[index]["mnyb"] = _costb * _qty;
            dataGridViewT1.InvalidateRow(index);

            Summary();
        }

        void Summary()
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                decimal d = 0, total = 0;

                for (int t = 0; t < dataGridViewT1.Rows.Count; t++)
                {
                    decimal.TryParse(dataGridViewT1["金額", t].Value.ToString(), out d);
                    d = Math.Round(d, Common.M, MidpointRounding.AwayFromZero);
                    total += d;
                }
                TotMnyB.Text = total.ToString("f" + Common.M);
            }
            else
            {
                TotMnyB.Text = "0";
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            var pk = jAdjust.Top();
            writeToTxt(pk);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var pk = jAdjust.Prior();
            writeToTxt(pk);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pk = jAdjust.Next();
            writeToTxt(pk);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var pk = jAdjust.Bottom();
            writeToTxt(pk);
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            GridButton.ForEach(t => t.Enabled = !btnAppend.Enabled);
        }

        void SetStName()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        string sql = "select stname from stkroom where stno=@stno";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                        cmd.CommandText = sql;
                        if (cmd.ExecuteScalar().ToString() != "")
                            StName.Text = cmd.ExecuteScalar().ToString();
                        else
                            StName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            switch (Common.User_DateTime)
            {
                case 1:
                    AdDate.Text = Date.GetDateTime(1, false);
                    break;
                case 2:
                    AdDate.Text = Date.GetDateTime(2, false);
                    break;
            }

        }

        void ThisFormState()
        {
            #region//新增時,各種歸零,然後設定某些預設值
            if (this.FormState == FormEditState.Append)
            {
                dtD.Clear();
                loadBomD();

                this.Memo1 = "";
                this.BomRec = 0;

                StNo.Text = Common.User_StkNo;
                TotMnyB.Text = "0";
                SetStName();
                dataGridViewT1.ReadOnly = false;
                dataGridViewT1.Columns["序號"].ReadOnly = true;
                dataGridViewT1.Columns["金額"].ReadOnly = true;
                dataGridViewT1.Columns["包裝數量"].ReadOnly = true;
                dataGridViewT1.Columns["產品組成"].ReadOnly = true;

                AdDate.Focus();
            }
            #endregion

            #region//複製時,值不變  ,但要設定某些預設值
            if (this.FormState == FormEditState.Duplicate)
            {
                loadBomD();

                AdNo.Text = "";

                if (bracket.Text.Trim() == "")
                    AdMemo.Clear();

                dataGridViewT1.ReadOnly = false;
                dataGridViewT1.Columns["序號"].ReadOnly = true;
                dataGridViewT1.Columns["金額"].ReadOnly = true;
                dataGridViewT1.Columns["包裝數量"].ReadOnly = true;
                dataGridViewT1.Columns["產品組成"].ReadOnly = true;

                AdDate.Text = Date.GetDateTime(Common.User_DateTime, false);

                AdDate.Focus();
            }
            #endregion

            #region//修改時,值不變
            if (this.FormState == FormEditState.Modify)
            {
                loadBomD();

                dataGridViewT1.ReadOnly = false;
                dataGridViewT1.Columns["序號"].ReadOnly = true;
                dataGridViewT1.Columns["金額"].ReadOnly = true;
                dataGridViewT1.Columns["包裝數量"].ReadOnly = true;
                dataGridViewT1.Columns["產品組成"].ReadOnly = true;

                AdDate.Focus();
            }
            #endregion
            this.自定編號.ReadOnly = true;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            ThisFormState();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (AdNo.Text == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jAdjust.IsExistDocument<JBS.JS.Adjust>(AdNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AdNo.Text = "";
                AdNo.Focus();
                return;
            }

            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            ThisFormState();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (AdNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jAdjust.IsExistDocument<JBS.JS.Adjust>(AdNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }

            if (jAdjust.IsEditInCloseDay(AdDate.Text) == false)
                return;

            if (bracket.Text.Trim() == "盤點")
            {
                if (MessageBox.Show("此單據由盤點單轉入,請確定是否修改?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    bracket.Text = "";
                }
            }

            if (jAdjust.IsModify<JBS.JS.Adjust>(AdNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jAdjust.upModify1<JBS.JS.Adjust>(AdNo.Text.Trim());//更新修改狀態1
                var pk = jAdjust.Renew();//刷新資料
                writeToTxt(pk);
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            ThisFormState();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (AdNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jAdjust.IsExistDocument<JBS.JS.Adjust>(AdNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jAdjust.IsModify<JBS.JS.Adjust>(AdNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jAdjust.IsEditInCloseDay(AdDate.Text) == false)
                return;

            if (bracket.Text.Trim() == "盤點")
            {
                if (MessageBox.Show("此單據由盤點單轉入,請確定是否刪除?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    bracket.Text = "";
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

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("adno", AdNo.Text.Trim());
                    //刪除主檔
                    cmd.CommandText = @"
                        delete from adjust    where adno=@adno;
                        delete from adjustd   where adno=@adno;
                        delete from adjuBom   where adno=@adno;
                        update iv set adno='' where adno=@adno; ";
                    cmd.ExecuteNonQuery();

                    //庫存調整
                    var tempd = tempdtD.Copy();
                    var tempbom = dtBom.Clone();
                    for (int i = 0; i < tempd.Rows.Count; i++)
                    {
                        tempd.Rows[i]["Qty"] = (-1) * tempd.Rows[i]["Qty"].ToDecimal();
                    }

                    jAdjust.加庫存(cmd, tempd, tempbom, "stno");

                    tn.Commit();

                    jAdjust.UpdateItemItStockQty(tempd, tempbom);

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
            if (AdNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new FrmAdjust_print())
            { 
                frm.PK = AdNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (AdNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new FrmAdjustBrow())
            { 
                frm.TSeekNo = AdNo.Text.Trim();
                frm.ShowDialog();

                writeToTxt(frm.TResult);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (jAdjust.IsEditInCloseDay(AdDate.Text) == false)
                return;

            jAdjust.RemoveEmptyRowOnSaving(dataGridViewT1, ref dtD, ref dtBom, () => { });

            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("調整明細不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dtD.Rows[i]["StNo"] = StNo.Text.Trim();

                CheckMny(i);
            }

            if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
            {
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

                        jAdjust.GetPkNumber<JBS.JS.Adjust>(cmd, AdDate.Text, ref AdNo);

                        AppendMasterOnSaving(cmd);

                        for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                        {
                            AppendDetailOnSaving(cmd, i);
                        }

                        AppendBomOnSaving(cmd);

                        var tempbom = dtBom.Clone();
                        jAdjust.加庫存(cmd, dtD, tempbom, "stno");

                        tn.Commit();

                        jAdjust.Save(AdNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jAdjust.UpdateItemItStockQty(dtD, tempbom);
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
                    using (var frm = new FrmAdjust_print())
                    { 
                        frm.PK = jAdjust.GetCurrent();
                        frm.ShowDialog();
                    }
                }

                if (tk != null)
                    tk.Wait();

                Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                ThisFormState();
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jAdjust.IsExistDocument<JBS.JS.Adjust>(AdNo.Text.Trim()) == false)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnNext_Click(null, null);
                    return;
                }

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

                        UpdateMasterOnSaving(cmd);

                        DelteOldOnSaving(cmd);

                        for (int k = 0; k < dataGridViewT1.Rows.Count; k++)
                        {
                            AppendDetailOnSaving(cmd, k);
                        }

                        AppendBomOnSaving(cmd);

                        var tempd = tempdtD.Copy();
                        var tempbom = dtBom.Clone();
                        for (int i = 0; i < tempd.Rows.Count; i++)
                        {
                            tempd.Rows[i]["Qty"] = (-1) * tempd.Rows[i]["Qty"].ToDecimal();
                        }

                        jAdjust.加庫存(cmd, tempd, tempbom, "stno");
                        jAdjust.加庫存(cmd, dtD, tempbom, "stno");

                        tn.Commit();

                        jAdjust.Save(AdNo.Text.Trim());

                        tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            jAdjust.UpdateItemItStockQty(tempd, tempbom, dtD, tempbom);
                        });
                    }
                    catch (Exception ex)
                    {
                        if (tn != null)
                            tn.Rollback();

                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }

                var dl = MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl == DialogResult.Yes)
                {
                    using (var frm = new FrmAdjust_print())
                    { 
                        frm.PK = jAdjust.GetCurrent();
                        frm.ShowDialog();
                    }
                }
                jAdjust.upModify0<JBS.JS.Adjust>(AdNo.Text.Trim());//改回0為無修改狀態
                if (tk != null)
                    tk.Wait();

                Common.SetTextState(this.FormState = FormEditState.Append, ref list);
                ThisFormState();
            }
        }
        private void AppendMasterOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("AdNo", AdNo.Text.Trim());
            cmd.Parameters.AddWithValue("AdDate", Date.ToTWDate(AdDate.Text).Trim());
            cmd.Parameters.AddWithValue("AdDate1", Date.ToUSDate(AdDate.Text).Trim());
            cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("totmnyb", TotMnyB.Text.ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("admemo", AdMemo.Text);
            cmd.Parameters.AddWithValue("admemo1", Memo1);
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("IsTrans", "");

            cmd.CommandText = "insert into adjust"
                + "(AdNo,AdDate,AdDate1"
                + ",StNo,stname,emno,emname,totmnyb"
                + ",admemo"
                + ",admemo1,appdate,edtdate,appscno,edtscno,IsTrans"
                + ") values "
                + " (@AdNo,@AdDate,@AdDate1"
                + ",@StNo,@stname,@emno,@emname,@totmnyb"
                + ",@admemo"
                + ",@admemo1,@appdate,@edtdate,@appscno,@edtscno,@IsTrans) ";
            cmd.ExecuteNonQuery();
        }
        private void AppendDetailOnSaving(SqlCommand cmd, int i)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("adno", AdNo.Text.Trim());
            cmd.Parameters.AddWithValue("addate", Date.ToTWDate(AdDate.Text).Trim());
            cmd.Parameters.AddWithValue("addate1", Date.ToUSDate(AdDate.Text).Trim());
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("itno", dataGridViewT1["產品編號", i].Value.ToString().Trim());
            cmd.Parameters.AddWithValue("itname", dataGridViewT1["品名規格", i].Value.ToString());
            cmd.Parameters.AddWithValue("ittrait", dataGridViewT1["ittrait", i].Value.ToDecimal());
            cmd.Parameters.AddWithValue("itunit", dataGridViewT1["單位", i].Value.ToString().Trim());
            cmd.Parameters.AddWithValue("itpkgqty", dataGridViewT1["包裝數量", i].Value.ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qty", dataGridViewT1["調整數量", i].Value.ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("costb", dataGridViewT1["成本", i].Value.ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("mnyb", dataGridViewT1["金額", i].Value.ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("memo", dataGridViewT1["說明", i].Value);
            cmd.Parameters.AddWithValue("bomid", AdNo.Text + dtD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", dtD.Rows[i]["BomRec"]);
            cmd.Parameters.AddWithValue("recordno", (i + 1));
            cmd.Parameters.AddWithValue("itdesp1", dtD.Rows[i]["itdesp1"]);
            cmd.Parameters.AddWithValue("itdesp2", dtD.Rows[i]["itdesp2"]);
            cmd.Parameters.AddWithValue("itdesp3", dtD.Rows[i]["itdesp3"]);
            cmd.Parameters.AddWithValue("itdesp4", dtD.Rows[i]["itdesp4"]);
            cmd.Parameters.AddWithValue("itdesp5", dtD.Rows[i]["itdesp5"]);
            cmd.Parameters.AddWithValue("itdesp6", dtD.Rows[i]["itdesp6"]);
            cmd.Parameters.AddWithValue("itdesp7", dtD.Rows[i]["itdesp7"]);
            cmd.Parameters.AddWithValue("itdesp8", dtD.Rows[i]["itdesp8"]);
            cmd.Parameters.AddWithValue("itdesp9", dtD.Rows[i]["itdesp9"]);
            cmd.Parameters.AddWithValue("itdesp10", dtD.Rows[i]["itdesp10"]);
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("IsTrans", "");
            cmd.CommandText = "insert into adjustd"
                + "(adno,addate,addate1"
                + ",stno,emno"
                + ",itno,itname,ittrait,itunit,itpkgqty,qty"
                + ",costb"
                + ",mnyb,memo"
                + ",bomid,bomrec"
                + ",recordno"
                + ",itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                + ",itdesp6,itdesp7,itdesp8,itdesp9,itdesp10"
                + ",stname,IsTrans) values "
                + "(@adno,@addate,@addate1"
                + ",@stno,@emno"
                + ",@itno,@itname,@ittrait,@itunit,@itpkgqty,@qty"
                + ",@costb"
                + ",@mnyb,@memo"
                + ",@bomid,@bomrec"
                + ",@recordno"
                + ",@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                + ",@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10"
                + ",@stname,@IsTrans) ";
            cmd.ExecuteNonQuery();
        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int j = 0; j < dtBom.Rows.Count; j++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("adno", AdNo.Text.Trim());
                cmd.Parameters.AddWithValue("bomid", AdNo.Text + dtBom.Rows[j]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("bomrec", dtBom.Rows[j]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", dtBom.Rows[j]["itno"]);
                cmd.Parameters.AddWithValue("itname", dtBom.Rows[j]["itname"]);
                cmd.Parameters.AddWithValue("itunit", dtBom.Rows[j]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", dtBom.Rows[j]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", dtBom.Rows[j]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", dtBom.Rows[j]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itprice", dtBom.Rows[j]["itprice"]);
                cmd.Parameters.AddWithValue("itmny", dtBom.Rows[j]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", dtBom.Rows[j]["itnote"]);
                cmd.Parameters.AddWithValue("IsTrans", "");
                cmd.Parameters.AddWithValue("itrec", (j + 1));
                cmd.Parameters.AddWithValue("itprs", 1);
                cmd.CommandText = @"
                                insert into adjubom
                                (adno,bomid,bomrec,itno,itname,itunit,itqty
                                ,itpareprs,itpkgqty,itprice,itmny,itnote,IsTrans,itrec,itprs
                                ) values 
                                (@adno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty
                                ,@itpareprs,@itpkgqty,@itprice,@itmny,@itnote,@IsTrans,@itrec,@itprs) ";

                cmd.ExecuteNonQuery();
            }
        }
        private void DelteOldOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("adno", AdNo.Text.Trim());
            cmd.CommandText = "delete from adjustd where adno=@adno COLLATE Chinese_Taiwan_Stroke_CS_AS";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "update iv set adno='' where adno=@adno";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "delete from adjubom where adno=@adno COLLATE Chinese_Taiwan_Stroke_CS_AS";
            cmd.ExecuteNonQuery();
        }
        private void UpdateMasterOnSaving(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("addate", Date.ToTWDate(AdDate.Text));
            cmd.Parameters.AddWithValue("addate1", Date.ToUSDate(AdDate.Text));
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
            cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
            cmd.Parameters.AddWithValue("totmnyb", TotMnyB.Text.ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("admemo1", Memo1);
            cmd.Parameters.AddWithValue("admemo", AdMemo.Text);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("bracket", "");
            cmd.Parameters.AddWithValue("adno", AdNo.Text.Trim());
            cmd.CommandText = "update adjust set "
                + " addate=@addate"
                + ",addate1=@addate1"
                + ",stno=@stno"
                + ",stname=@stname"
                + ",emno=@emno"
                + ",emname=@emname"
                + ",bracket=@bracket"
                + ",totmnyb=@totmnyb"
                + ",admemo1=@admemo1"
                + ",admemo=@admemo"
                + ",edtscno=@edtscno"
                + ",edtdate=@edtdate"
                + " where adno=@adno COLLATE Chinese_Taiwan_Stroke_CS_AS";
            cmd.ExecuteNonQuery();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var pk = jAdjust.Cancel();
            writeToTxt(pk);

            dataGridViewT1.ReadOnly = true;

            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
            jAdjust.upModify0<JBS.JS.Adjust>(AdNo.Text.Trim());//改回0為無修改狀態
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }





        private void gridAppend_Click(object sender, EventArgs e)
        {
            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                GridAdjustDAddRows();
                dataGridViewT1.CurrentCell = dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Cells["產品編號"];
                dataGridViewT1.CurrentRow.Selected = true;
            }
            dataGridViewT1.Focus();
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            gridDelete.Focus();
            if (dataGridViewT1.Rows.Count > 0)
            {
                string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].EditedFormattedValue.ToString().Trim();
                jAdjust.RemoveBom(rec, ref dtBom);

                int index = dataGridViewT1.CurrentRow.Index;
                dtD.Rows.RemoveAt(index);
                Summary();

                if (dataGridViewT1.Rows.Count > 0)
                {
                    index = (index > dataGridViewT1.Rows.Count - 1) ? dataGridViewT1.Rows.Count - 1 : index;
                    dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", index];
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
                gridAppend.Focus();
                if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    GridAdjustDAddRows(index);
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells["產品編號"];
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
                dataGridViewT1.Focus();
                return;
            }
            using (var frm = new FrmDesp(true, FormStyle.Mini))
            {
                frm.dr = dtD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridBomD_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridBomD.Focus();
                if (dataGridViewT1.SelectedRows[0].Cells["產品組成"].Value.ToString() != "組裝品")
                {
                    MessageBox.Show("只有組裝品或組合品可以編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridViewT1.Focus();
                    return;
                }
                else
                {
                    using (var frm = new FrmAdjust_Bom())
                    { 
                        string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].Value.ToString().Trim();
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

                        frm.FromADBrow = false;
                        frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                        frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                        frm.table = table.Copy();
                        frm.grid = dataGridViewT1;
                        frm.pkey = rec;
                        frm.上層Row = dtD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                        switch (frm.ShowDialog())
                        {
                            case DialogResult.OK:
                                if (frm.CallBack == "Money")
                                {
                                    dtBom.Merge(frm.table);
                                    dtBom.AcceptChanges();
                                    dtD.Rows[dataGridViewT1.SelectedRows[0].Index]["costb"] = frm.Money;
                                    dataGridViewT1.Focus();
                                    dataGridViewT1.InvalidateRow(dataGridViewT1.SelectedRows[0].Index);
                                    break;
                                }
                                else
                                {
                                    dtBom.Merge(frm.table);
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
                dataGridViewT1.Focus();
            }
        }

        private void gridStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridStock.Focus();
                using (FrmSale_Stock frm = new FrmSale_Stock())
                {
                    frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                    frm.ShowDialog();
                    dataGridViewT1.Focus();
                }
            }
        }

        private void gridBshopHistory_Click(object sender, EventArgs e)
        {
            gridBshopHistory.Focus();

            if (jAdjust.EnableBShopPrice() == false)
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

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jAdjust.Open<JBS.JS.Empl>(sender, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
            });
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused)
                return;

            if (EmNo.ReadOnly)
                return;

            if (EmNo.TrimTextLenth() == 0)
            {
                EmNo.Clear();
                EmName.Clear();
                return;
            }

            jAdjust.ValidateOpen<JBS.JS.Empl>(sender, e, reader =>
            {
                EmNo.Text = reader["emno"].ToString().Trim();
                EmName.Text = reader["emname"].ToString().Trim();
            }, true);
        }

        private void AdMemo_DoubleClick(object sender, EventArgs e)
        {
            if (AdMemo.ReadOnly)
                return;

            using (var frm = new FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    AdMemo.Text = frm.Memo.GetUTF8(60);
                AdMemo.SelectAll();
            }
        }

        private void StNo_Enter(object sender, EventArgs e)
        {
            if (StNo.ReadOnly)
                return;

            StNo.Tag = StNo.Text.Trim();
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            jAdjust.Open<JBS.JS.Stkroom>(sender, reader =>
            {
                StNo.Text = reader["StNo"].ToString().Trim();
                StName.Text = reader["StName"].ToString().Trim();

                StNo.Tag = reader["StNo"].ToString().Trim();
            });
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (StNo.Text == "")
            {
                e.Cancel = true;
                MessageBox.Show("倉庫編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jAdjust.ValidateOpen<JBS.JS.Stkroom>(sender, e, reader =>
            {
                if (StNo.Text.Trim() == (StNo.Tag ?? "").ToString())
                    return;

                StNo.Text = reader["StNo"].ToString().Trim();
                StName.Text = reader["StName"].ToString().Trim();

                StNo.Tag = reader["StNo"].ToString().Trim();
            });

            if (Common.keyData != Keys.Up)
            {
                if (dataGridViewT1.Rows.Count == 0)
                    if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
            }
        }

        private void AdNo_Enter(object sender, EventArgs e)
        {
            if (AdNo.ReadOnly)
                return;

            AdNo.Tag = AdNo.Text.Trim();
        }

        private void AdNo_DoubleClick(object sender, EventArgs e)
        {
            jAdjust.Open<JBS.JS.Adjust>(sender); 
        }

        private void AdNo_Validating(object sender, CancelEventArgs e)
        {
            if (AdNo.ReadOnly || btnCancel.Focused) return;

            if (AdNo.Text.Length > 0 && AdNo.Text.Trim() == "")
            {
                e.Cancel = true;
                AdNo.Text = "";
                AdNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (jAdjust.IsExistDocument<JBS.JS.Adjust>(AdNo.Text.Trim()) == true)
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Duplicate)
            {
                if (jAdjust.IsExistDocument<JBS.JS.Adjust>(AdNo.Text.Trim()) == true)
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (jAdjust.IsExistDocument<JBS.JS.Adjust>(AdNo.Text.Trim()) == true)
                {
                    if (AdNo.Text.Trim() == (AdNo.Tag ?? "").ToString())
                        return;

                    writeToTxt(AdNo.Text.Trim());
                    loadBomD(); 
                }
                else
                {
                    e.Cancel = true;
                    AdNo.SelectAll();

                    jAdjust.Open<JBS.JS.Adjust>(sender);

                    if (jAdjust.IsExistDocument<JBS.JS.Adjust>(AdNo.Text.Trim()) == true)
                    {
                        writeToTxt(AdNo.Text.Trim());
                        loadBomD(); 
                    }
                }
            }
        }

        private void AdDate_Validating(object sender, CancelEventArgs e)
        {
            if (AdDate.ReadOnly) return;
            if (btnCancel.Focused) return;

            jAdjust.DateValidate(sender, e);
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
                if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
        }

        private void dataGridViewT1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "序號")
            {
                dataGridViewT1["序號", e.RowIndex].Value = (e.RowIndex + 1).ToString();
            }
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name.ToString() == "單位")
            {
                TextBefore = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name.ToString() == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jAdjust.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (AdNo.ReadOnly == true) return;
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                jAdjust.DataGridViewOpen<JBS.JS.Item>(sender, e, dtD, row => FillItem(row, e.RowIndex));
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.說明))
            {
                using (var frm = new FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                        dtD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.單位))
            {
                var itno = dtD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                jAdjust.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
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

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            dtD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;

                dtD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                dtD.Rows[e.RowIndex]["costb"] = GetLastBuyPrice(itno, unit);

                CheckMny(e.RowIndex);

            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly == true) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.成本))
            {
                var format = this.成本.DefaultCellStyle.Format;
                var costb = dataGridViewT1["成本", e.RowIndex].EditedFormattedValue.ToDecimal(format);

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = costb.ToString();

                dtD.Rows[e.RowIndex]["costb"] = costb;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                CheckMny(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.調整數量))
            {
                var format = this.調整數量.DefaultCellStyle.Format;
                var qty = dataGridViewT1["調整數量", e.RowIndex].EditedFormattedValue.ToDecimal(format);

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = qty.ToString();

                dtD.Rows[e.RowIndex]["qty"] = qty;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                CheckMny(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                string itnow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (itnow.Length == 0)
                {
                    dtD.Rows[e.RowIndex]["itno"] = "";
                    dtD.Rows[e.RowIndex]["itname"] = "";
                    dtD.Rows[e.RowIndex]["qty"] = 0;
                    dtD.Rows[e.RowIndex]["itunit"] = "";
                    dtD.Rows[e.RowIndex]["costb"] = 0;
                    dtD.Rows[e.RowIndex]["mnyb"] = 0;
                    dtD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    dtD.Rows[e.RowIndex]["產品組成"] = "";
                    dtD.Rows[e.RowIndex]["ittrait"] = 0;
                    dtD.Rows[e.RowIndex]["memo"] = "";
                    dataGridViewT1.InvalidateRow(e.RowIndex);

                    ItNoBegin = UdfNoBegin = "";
                    CheckMny(e.RowIndex);

                    var rec = dtD.Rows[e.RowIndex]["bomrec"].ToString();
                    jAdjust.RemoveBom(rec, ref dtBom);
                    return;
                }

                if (itnow == ItNoBegin)
                    return;

                if (itnow == UdfNoBegin && itnow.Length > 0)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    dtD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                if (itnow != ItNoBegin && itnow != UdfNoBegin)
                {
                    jAdjust.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, dtD, row => FillItem(row, e.RowIndex, e));
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.單位))
            {
                var itno = dtD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (TextBefore == unit)
                    return;

                jAdjust.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        dtD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        dtD.Rows[e.RowIndex]["costb"] = row["itbuyprip"].ToDecimal();

                    }
                    else
                    {
                        dtD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        dtD.Rows[e.RowIndex]["costb"] = row["itbuypri"].ToDecimal();
                    }
                });

                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = unit;

                dtD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                dtD.Rows[e.RowIndex]["costb"] = GetLastBuyPrice(itno, unit);

                CheckMny(e.RowIndex);
            }
        }

        private void FillItem(SqlDataReader row, int index, DataGridViewCellValidatingEventArgs e = null)
        {
            string ItTrait = "";
            switch (row["ittrait"].ToString())
            {
                case "1":
                    ItTrait = "組合品";
                    break;
                case "2":
                    ItTrait = "組裝品";
                    break;
                case "3":
                    ItTrait = "單一商品";
                    break;
            }
            if (ItTrait == "組合品")
            {
                if (e != null)
                    e.Cancel = true;

                MessageBox.Show("組合品無法調整", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (dataGridViewT1.EditingControl != null)
                    ((TextBox)dataGridViewT1.EditingControl).SelectAll();

                return;
            }

            this.ItNoBegin = row["itno"].ToString().Trim();

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = row["itno"].ToString().Trim();

            dtD.Rows[index]["itno"] = row["itno"].ToString().Trim();
            dtD.Rows[index]["itname"] = row["itname"].ToString();
            dtD.Rows[index]["itunit"] = row["itunit"].ToString();
            dtD.Rows[index]["itpkgqty"] = 1;
            dtD.Rows[index]["產品組成"] = ItTrait;
            dtD.Rows[index]["ittrait"] = row["ittrait"].ToString();
            dtD.Rows[index]["ItNoUdf"] = row["ItNoUdf"].ToString();
            for (int i = 1; i <= 10; i++)
            {
                dtD.Rows[index]["itdesp" + i] = row["itdesp" + i].ToString();
            }

            string rec = dtD.Rows[index]["bomrec"].ToString().Trim();
            jAdjust.RemoveBom(rec, ref dtBom);

            if (ItTrait == "組裝品")
            {
                jAdjust.GetItemBom(row["itno"].ToString().Trim(), rec, ref dtBom);
            }

            dtD.Rows[index]["costb"] = GetLastBuyPrice(row["itno"].ToString().Trim(), row["itunit"].ToString());

            CheckMny(index);
        }

        private decimal GetLastBuyPrice(string itno, string unit)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("itno", itno);
                cmd.Parameters.AddWithValue("itunit", unit);
                cmd.Parameters.AddWithValue("day", AdDate.Text.Trim());
                cmd.CommandText = @"
                    Select top 1 price from bshopd 
                    where bsdate >= @day
                    And itno = @itno 
                    And itunit = @itunit
                    Order by bsdate desc,bsno desc ";
                var obj = cmd.ExecuteScalar();
                if (obj != null)
                    return obj.ToDecimal("f" + Common.M);

                cmd.CommandText = @"
                    Select top 1 itbuyprip from item where itno = @itno and itunitp = @itunit and Len(itunitp) > 0 ";
                obj = cmd.ExecuteScalar();
                if (obj != null)
                    return obj.ToDecimal("f" + Common.M);

                cmd.CommandText = @"
                    Select top 1 itbuypri from item where itno = @itno";
                obj = cmd.ExecuteScalar();
                if (obj != null)
                    return obj.ToDecimal("f" + Common.M);
            }
            return 0;
        }


        ToolTip tip = new ToolTip();
        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "備註說明")
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

        private void gridKeyMan_Click(object sender, EventArgs e)
        {
            if (AdNo.Text.Trim() == "")
                return;

            using (FrmSale_AppScNo frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = jAdjust.keyMan.AppendMan;
                frm.ATime = jAdjust.keyMan.AppendTime;
                //修改人員
                frm.EName = jAdjust.keyMan.EditMan;
                frm.ETime = jAdjust.keyMan.EditTime;
                frm.ShowDialog();
            }
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = AdNo.ReadOnly ? false : true;
                frm.memo1 = Memo1;

                if (frm.ShowDialog() == DialogResult.OK) Memo1 = frm.memo1;
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
            else if (keyData == Keys.F6 && gridItDesp.Enabled)
            {
                gridItDesp_Click(null, null);
            }
            else if (keyData == Keys.F7 && gridBomD.Enabled)
            {
                gridBomD_Click(null, null);
            }
            else if (keyData == Keys.F8 && gridStock.Enabled)
            {
                gridStock_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift") && gridBshopHistory.Enabled)
            {
                gridBshopHistory_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}

