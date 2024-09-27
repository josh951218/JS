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
using S_61.SOther;

namespace S_61.subMenuFm_2
{
    public partial class FrmItemInv : Formbase
    {
        JBS.JS.Iv jIv=new JBS.JS.Iv();
        JBS.JS.xEvents xe;
        List<TextBoxbase> list;
        DataTable dtM = new DataTable();
        DataTable dtD = new DataTable();
        DataTable dtB = new DataTable();
        DataRow dr;
        List<DataRow> li = new List<DataRow>();

        string btnState = "";
        string TextBefore = "";
        string ItNoBegin = "";
        string UdfNoBegin = "";
        int PageTemp = 0;
        string tempNo = "";
        decimal BomRec = 0; 

        public FrmItemInv()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();

            this.帳上庫存量.Set庫存數量小數();
            this.盤點數量.Set庫存數量小數();
            this.盤盈虧數量.Set庫存數量小數();
            this.平均成本.Set進貨單價小數();
            this.金額.Set進貨單價小數();

            this.平均成本.Visible = Common.User_ShopPrice;
            this.金額.Visible = Common.User_ShopPrice;
            IvSum.Visible = Common.User_ShopPrice;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        private void FrmItemInv_Load(object sender, EventArgs e)
        {
            IvDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;


            LoadDB();
            if (dtM.Rows.Count > 0)
            {
                dr = li.LastOrDefault();
                writeToTxt(dr);
            }
            else
            {
                writeToTxt(null);
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        string sql = "select *,序號='',產品組成='',ItNoUdf = (select top 1 itnoudf from item where item.itno = IvD.itno  COLLATE  Chinese_Taiwan_Stroke_BIN ) from IvD where 1=0";
                        using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                        {
                            da.Fill(dtD);
                        }
                        sql = "select * from IvBom where 1=0";
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
            }

            dataGridViewT1.DataSource = dtD;
            btnAppend.Focus();
        }

        void LoadDB()
        {
            dtM.Clear();
            dtD.Clear();
            dtB.Clear();
            li.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from Iv order by ivno ";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(dtM);
                        if (dtM.Rows.Count > 0) li = dtM.AsEnumerable().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void LoadD(DataRow row)
        {
            var ivno = row["ivno"].ToString().Trim();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        string sql = "select *,序號='',產品組成='', ItNoUdf= (select top 1 itnoudf from item where item.itno = ivD.itno  COLLATE  Chinese_Taiwan_Stroke_BIN  ) from ivD where ivno=@ivno";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ivno", ivno);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtD);
                        }
                        for (int i = 0; i < dtD.Rows.Count; i++)
                        {
                            dtD.Rows[i]["序號"] = (i + 1).ToString();
                            dtD.Rows[i]["IvDate"] = Date.AddLine(dtD.Rows[i]["IvDate"].ToString().Trim());
                            dtD.Rows[i]["IvDate1"] = Date.AddLine(dtD.Rows[i]["IvDate1"].ToString().Trim());

                            if (dtD.Rows[i]["ItTrait"].ToDecimal() == 1) dtD.Rows[i]["產品組成"] = "組合品";
                            else if (dtD.Rows[i]["ItTrait"].ToDecimal() == 2) dtD.Rows[i]["產品組成"] = "組裝品";
                            else if (dtD.Rows[i]["ItTrait"].ToDecimal() == 3) dtD.Rows[i]["產品組成"] = "單一商品";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void LoadB(DataRow row)
        {
            var ivno = row["ivno"].ToString().Trim();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        string sql = "select * from IvBom where ivno=@ivno";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ivno", ivno);
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

        void writeToTxt(DataRow row)
        {
            if (row != null)
            {
                IvNo.Text = row["IvNo"].ToString().Trim();
                var date = Common.User_DateTime == 1 ? row["IvDate"].ToString().Trim() : row["IvDate1"].ToString().Trim();
                IvDate.Text = date;
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
                AdNo.Text = row["AdNo"].ToString().Trim();
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
                IvMemo.Text = row["Memo"].ToString().Trim();
                IvSum.Text = row["sum"].ToDecimal().ToString("f" + Common.MF);
                LoadD(row);
                LoadB(row);
            }
            else
            {
                //清空所有欄位
                IvNo.Text = AdNo.Text = StNo.Text = StName.Text = EmNo.Text = EmName.Text = IvMemo.Text = "";
                IvDate.Text = StNo.Text = StName.Text = "";
            }
        }

        DataRow getCurrentDataRow()
        {
            return li.Find(r => r.Field<string>("IvNo") == (IvNo.Text.Trim()));
        }

        DataRow getCurrentDataRow(string no)
        {
            return li.Find(r => r.Field<string>("IvNo") == (no.Trim()));
        }

        private void IvNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            IvDate.ReadOnly = StNo.ReadOnly = EmNo.ReadOnly = IvMemo.ReadOnly = IvNo.ReadOnly;
            btnToAdjust.Enabled = IvToThis.Enabled = IvNo.ReadOnly;
            if (Common.Series == "74" || Common.Series == "73") StNo.ReadOnly = true;

            foreach (var item in new List<ButtonSmallT> { gridAppend, gridInsert, gridDelete, gridPicture, gridItDesp })
            {
                item.Enabled = !IvNo.ReadOnly;
            }

            dataGridViewT1.ReadOnly = IvNo.ReadOnly;
            if (dataGridViewT1.ReadOnly)
            {
                foreach (DataGridViewColumn c in dataGridViewT1.Columns) c.ReadOnly = true;
            }
            else
            {
                dataGridViewT1.Columns["序號"].ReadOnly = true;
                dataGridViewT1.Columns["帳上庫存量"].ReadOnly = true;
                dataGridViewT1.Columns["盤盈虧數量"].ReadOnly = true;
                dataGridViewT1.Columns["產品組成"].ReadOnly = true;
                dataGridViewT1.Columns["單位"].ReadOnly = true;
                dataGridViewT1.Columns["金額"].ReadOnly = true;
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            LoadDB();
            if (li.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            tempNo = IvNo.Text.Trim();
            dr = getCurrentDataRow();
            PageTemp = li.IndexOf(dr);
            LoadDB();
            if (li.Count > 0)
            {
                dr = getCurrentDataRow(tempNo);
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    if (PageTemp == 0)
                    {
                        dr = li.First();
                        writeToTxt(dr);
                        MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        dr = li[--PageTemp];
                        writeToTxt(dr);
                        return;
                    }
                }
                if (i > 0)
                {
                    dr = li[--i];
                    writeToTxt(dr);
                }
                else
                {
                    dr = li.FirstOrDefault();
                    writeToTxt(dr);
                    MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tempNo = IvNo.Text.Trim();
            dr = getCurrentDataRow();
            PageTemp = li.IndexOf(dr);
            LoadDB();
            if (li.Count > 0)
            {
                dr = getCurrentDataRow(tempNo);
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    if (PageTemp >= li.Count)
                    {
                        dr = li.Last();
                        writeToTxt(dr);
                        MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        dr = li[++i];
                        writeToTxt(dr);
                        return;
                    }
                }
                if (i < li.Count - 1)
                {
                    dr = li[++i];
                    writeToTxt(dr);
                }
                else
                {
                    dr = li.LastOrDefault();
                    writeToTxt(dr);
                    MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            LoadDB();
            if (li.Count > 0)
            {
                dr = li.Last();
                writeToTxt(dr);
            }
        }

        void SetTextWhenAppend()
        {
            IvNo.Text = AdNo.Text = IvDate.Text = StNo.Text = StName.Text = EmNo.Text = EmName.Text = IvMemo.Text = "";
            dtD.Clear();
            IvDate.Text = Date.GetDateTime(Common.User_DateTime);
            AdNo.Clear();
            StNo.Text = Common.User_StkNo;
            pVar.StkValidate(StNo.Text.Trim(), StNo, StName);
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.Append, ref list);
            btnState = "Append";

            IvNo.ReadOnly = false;

            SetTextWhenAppend();
            dtD.Clear();
            dtB.Clear();

            IvDate.Focus();
            this.自定編號.ReadOnly = true;
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (IvNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(this.FormState = FormEditState.Duplicate, ref list);
            btnState = "Duplicate";

            IvNo.ReadOnly = false;

            IvNo.Clear();
            AdNo.Clear();
            IvDate.Text = Date.GetDateTime(Common.User_DateTime);
            StNo.Text = Common.User_StkNo;
            pVar.StkValidate(StNo.Text.Trim(), StNo, StName);

            var stno = StNo.Text.Trim();
            var itno = "";

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                itno = dtD.Rows[i]["itno"].ToString().Trim();
                dtD.Rows[i]["stno"] = stno;
                dtD.Rows[i]["stkqty"] = GetItemStock(itno, stno);
                dtD.Rows[i]["qty"] = 0;
                dtD.Rows[i]["invqty"] = 0;
                dtD.Rows[i]["mny"] = 0;
                ToCountRowMny(i);
            }
            ToCountAllMny();
            IvSum.Clear();
            IvDate.Focus();
            this.自定編號.ReadOnly = true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (IvNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jIv.IsExistDocument<JBS.JS.Iv>(IvNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }

            if (xe.IsEditInCloseDay(IvDate.Text) == false)
                return;

            if (AdNo.Text.Trim().Length > 0)
            {
                MessageBox.Show("此單據已轉調整單，無法修改！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jIv.IsModify<JBS.JS.Iv>(IvNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jIv.upModify1<JBS.JS.Iv>(IvNo.Text.Trim());//更新修改狀態1
                LoadDB();
                dr = getCurrentDataRow(IvNo.Text.Trim());
                if (li.IndexOf(dr) == -1)
                {
                    if (li.Count > 0)
                    {
                        dr = li.Last();
                        writeToTxt(dr);
                    }
                    else
                    {
                        dr = null;
                        writeToTxt(dr);
                    }
                }
                else
                {
                    writeToTxt(dr);
                }
            }            

            Common.SetTextState(this.FormState = FormEditState.Modify, ref list);
            btnState = "Modify";

            IvNo.ReadOnly = false;

            IvDate.Focus();
            this.自定編號.ReadOnly = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (IvNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jIv.IsExistDocument<JBS.JS.Iv>(IvNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jIv.IsModify<JBS.JS.Iv>(IvNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (xe.IsEditInCloseDay(IvDate.Text) == false)
                return;

            if (AdNo.Text.Trim().Length > 0)
            {
                MessageBox.Show("此單據已轉調整單，無法修改！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnState = "Delete";
            bool IsDeleted = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("ivno", IvNo.Text.Trim());
                        cmd.CommandText = "  delete from iv  where ivno=@ivno;";
                        cmd.CommandText += " delete from ivD where ivno=@ivno;";
                        cmd.ExecuteNonQuery();
                    }
                }
                IsDeleted = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            if (IsDeleted)
            {
                LoadDB();
                if (li.Count > 0)
                {
                    dr = li.LastOrDefault();
                    writeToTxt(dr);
                }
                else
                {
                    dr = null;
                    writeToTxt(dr);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (IvNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new FrmItemInvPrint())
            {
                frm.No = IvNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        void AjaxLoad(string no)
        {
            LoadDB();
            if (li.Count > 0)
            {
                dr = getCurrentDataRow(no);
                writeToTxt(dr);
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (IvNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("空資料庫，請先新增！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new FrmItemInvBrow())
            {
                frm.TSeekNo = IvNo.Text.Trim();
                frm.ShowDialog();
                AjaxLoad(frm.TResult);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (xe.IsEditInCloseDay(IvDate.Text) == false)
                return;

            if (StNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("盤點倉庫不可為空白！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNo.Focus();
                return;
            }

            var machine = "";
            //if (Common.Sys_StockKind == 1)
            //{
            //    machine = "";
            //}
            //else
            //{
            //    if (File.Exists(Common.StartUpIniFileName))
            //    {
            //        //唯有pos系統，且是多點情況下，機台號碼存1號機做傳輸用
            //        var SIP = Common.GetPrivateProfileString("POS設定", "SERVER");
            //        machine = "1";
            //        if (SIP.Trim() == "-1") machine = "";
            //        if (Common.Series.ToDecimal() == 71) machine = "";
            //    }
            //    else
            //    {
            //        machine = "";
            //        MessageBox.Show("設定檔遺失！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}

            DataTable t;
            string ip = "";
            string ivno = "";
            string sql = "";
            SqlTransaction trans = null;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    trans = cn.BeginTransaction();

                    if (btnState == "Append" || btnState == "Duplicate")
                    {
                        sql = "select ip from stkroom where stno=@stno";
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("stno", Common.User_StkNo);
                            cmd.Transaction = trans;
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
                            dd = Date.ToUSDate(IvDate.Text.Trim());
                        }
                        else
                        {
                            dd = Date.ToTWDate(IvDate.Text.Trim());
                        }

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.Parameters.AddWithValue("ivno", ip + dd);
                            cmd.CommandText = "select ivno from iv where ivno like @ivno + '%' order by ivno desc";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                t = new DataTable();
                                if (reader.HasRows) t.Load(reader);
                            }
                        }
                        decimal d = 1;
                        var collection = t.AsEnumerable();

                        if (Common.Sys_NoAdd == 1)
                        {
                            ivno = ip + dd + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 2)
                        {
                            ivno = ip + dd.takeString(5) + (d.ToString().PadLeft(6, '0'));
                        }
                        else if (Common.Sys_NoAdd == 3)
                        {
                            ivno = ip + dd + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 4)
                        {
                            ivno = ip + dd.takeString(6) + (d.ToString().PadLeft(6, '0'));
                        }
                        while (collection.Count(r => r["ivno"].ToString().Trim() == ivno) > 0)
                        {
                            d++;
                            if (Common.Sys_NoAdd == 1)
                            {
                                ivno = ip + dd + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 2)
                            {
                                ivno = ip + dd.takeString(5) + (d.ToString().PadLeft(6, '0'));
                            }
                            else if (Common.Sys_NoAdd == 3)
                            {
                                ivno = ip + dd + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 4)
                            {
                                ivno = ip + dd.takeString(6) + (d.ToString().PadLeft(6, '0'));
                            }
                        }

                        //主檔
                        dtM.Clear();
                        var row = dtM.NewRow();
                        row["ivno"] = ivno.Trim();
                        row["ivdate"] = Date.ToTWDate(IvDate.Text);
                        row["ivdate1"] = Date.ToUSDate(IvDate.Text);
                        row["stno"] = StNo.Text.Trim();
                        row["stname"] = StName.Text.Trim();
                        row["emno"] = EmNo.Text.Trim();
                        row["emname"] = EmName.Text.Trim();
                        row["memo"] = IvMemo.Text.Trim();
                        row["sum"] = IvSum.Text.ToDecimal().ToString("f" + Common.MF);
                        row["bracket"] = "盤點";
                        row["recordno"] = dtD.Rows.Count;
                        row["appscno"] = Common.User_Name;
                        row["appdate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                        row["edtscno"] = Common.User_Name;
                        row["edtdate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                        row["IsTrans"] = machine;
                        dtM.Rows.Add(row);
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = "select * from iv where 1=0";
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                                {
                                    da.InsertCommand = builder.GetInsertCommand();
                                    da.InsertCommand.Transaction = trans;
                                    da.Update(dtM);
                                }
                            }
                        }

                        //明細
                        dataGridViewT1.EndEdit();
                        for (int i = 0; i < dtD.Rows.Count; i++)
                        {
                            if (dtD.Rows[i]["itno"].ToString().Trim().Length == 0)
                            {
                                dtD.Rows[i].AcceptChanges();
                                continue;
                            }
                            dtD.Rows[i]["ivno"] = ivno.Trim();
                            dtD.Rows[i]["ivdate"] = Date.ToTWDate(IvDate.Text);
                            dtD.Rows[i]["ivdate1"] = Date.ToUSDate(IvDate.Text);
                            dtD.Rows[i]["stno"] = StNo.Text.Trim();
                            dtD.Rows[i]["emno"] = EmNo.Text.Trim();
                            dtD.Rows[i]["emname"] = EmName.Text.Trim();
                            dtD.Rows[i]["bracket"] = "盤點";
                            dtD.Rows[i]["recordno"] = (i + 1).ToString();
                            dtD.Rows[i]["IsTrans"] = machine;
                            dtD.Rows[i].EndEdit();
                            dtD.Rows[i].AcceptChanges();
                            dtD.Rows[i].SetAdded();
                        }
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = "select * from IvD where 1=0";
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                                {
                                    da.InsertCommand = builder.GetInsertCommand();
                                    da.InsertCommand.Transaction = trans;
                                    da.Update(dtD);
                                }
                            }
                        }
                    }
                    else if (btnState == "Modify")
                    {
                        //主檔
                        //dr["ivdate"] = Date.ToTWDate(IvDate.Text);
                        //dr["ivdate1"] = Date.ToUSDate(IvDate.Text);
                        //dr["stno"] = StNo.Text.Trim();
                        //dr["stname"] = StName.Text.Trim();
                        //dr["emno"] = EmNo.Text.Trim();
                        //dr["emname"] = EmName.Text.Trim();
                        //dr["memo"] = IvMemo.Text.Trim();
                        //dr["sum"] = IvSum.Text.ToDecimal().ToString("f" + Common.MF);
                        //dr["bracket"] = "盤點";
                        //dr["recordno"] = dtD.Rows.Count;
                        //dr["edtscno"] = Common.User_Name;
                        //dr["edtdate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                        //dr["IsTrans"] = machine;

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("IvNo", dr["ivno"].ToString().Trim());
                            cmd.Parameters.AddWithValue("ivdate", Date.ToTWDate(IvDate.Text));
                            cmd.Parameters.AddWithValue("ivdate1", Date.ToUSDate(IvDate.Text));
                            cmd.Parameters.AddWithValue("stno",StNo.Text.Trim());
                            cmd.Parameters.AddWithValue("stname",StName.Text.Trim());
                            cmd.Parameters.AddWithValue("emno",EmNo.Text.Trim());
                            cmd.Parameters.AddWithValue("emname",EmName.Text.Trim());
                            cmd.Parameters.AddWithValue("memo",IvMemo.Text.Trim());
                            cmd.Parameters.AddWithValue("sum",IvSum.Text.ToDecimal().ToString("f" + Common.MF));
                            cmd.Parameters.AddWithValue("bracket","盤點");
                            cmd.Parameters.AddWithValue("recordno", dtD.Rows.Count);
                            cmd.Parameters.AddWithValue("edtscno",Common.User_Name);
                            cmd.Parameters.AddWithValue("edtdate",Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                            cmd.Parameters.AddWithValue("IsTrans",machine);

                            cmd.CommandText = @"UPDATE [dbo].[Iv]
                               SET [ivdate] = @ivdate
                                  ,[ivdate1] = @ivdate1
                                  ,[stno] = @stno
                                  ,[stname] = @stname
                                  ,[emno] = @emno
                                  ,[emname] = @emname
                                  ,[sum] = @sum
                                  ,[memo] = @memo
                                  ,[bracket] =@bracket
                                  ,[edtscno] = @edtscno
                                  ,[edtdate] = @edtdate
                                  ,[IsTrans] = @IsTrans
                                  ,[recordno] = @recordno
                             WHERE ivno=@IvNo";
                            cmd.ExecuteNonQuery();

                            //cmd.CommandText = "select * from iv where 1=0";
                            //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            //{
                            //    using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                            //    {
                            //        da.UpdateCommand = builder.GetUpdateCommand();
                            //        da.UpdateCommand.Transaction = trans;
                            //        da.Update(dtM);
                            //    }
                            //}
                        }
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("ivno", dr["ivno"].ToString().Trim());
                            cmd.CommandText = " delete from ivd where ivno=@ivno";
                            cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                        }
                        //明細
                        dataGridViewT1.EndEdit();
                        for (int i = 0; i < dtD.Rows.Count; i++)
                        {
                            if (dtD.Rows[i]["itno"].ToString().Trim().Length == 0)
                            {
                                dtD.Rows[i].AcceptChanges();
                                continue;
                            }
                            dtD.Rows[i]["ivno"] = dr["ivno"].ToString().Trim();
                            dtD.Rows[i]["ivdate"] = Date.ToTWDate(IvDate.Text);
                            dtD.Rows[i]["ivdate1"] = Date.ToUSDate(IvDate.Text);
                            dtD.Rows[i]["stno"] = StNo.Text.Trim();
                            dtD.Rows[i]["emno"] = EmNo.Text.Trim();
                            dtD.Rows[i]["emname"] = EmName.Text.Trim();
                            dtD.Rows[i]["bracket"] = "盤點";
                            dtD.Rows[i]["recordno"] = (i + 1).ToString();
                            dtD.Rows[i]["IsTrans"] = machine;
                            dtD.Rows[i].EndEdit();
                            dtD.Rows[i].AcceptChanges();
                            dtD.Rows[i].SetAdded();
                        }

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = "select * from IvD where 1=0";
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                                {
                                    da.InsertCommand = builder.GetInsertCommand();
                                    da.InsertCommand.Transaction = trans;
                                    da.Update(dtD);
                                }
                            }
                        }
                    }




                    if (btnState == "Append" || btnState == "Duplicate")
                    {
                        tempNo = ivno;
                        SetTextWhenAppend();
                        trans.Commit();

                        dtD.Clear();
                        dtB.Clear();
                        IvDate.Focus();
                    }

                    if (btnState == "Modify")
                    {
                        tempNo = dr["ivno"].ToString().Trim();
                        trans.Commit();
                        jIv.upModify0<JBS.JS.Iv>(IvNo.Text.Trim());//更新修改狀態0
                        dtD.Clear();
                        dtB.Clear();
                        IvDate.Focus();
                        btnState = "Append";
                    }
                }

                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(this.FormState = FormEditState.None, ref list);
            btnState = "";
            IvNo.ReadOnly = true;

            LoadDB();
            dr = getCurrentDataRow(tempNo);
            if (li.IndexOf(dr) == -1)
            {
                if (li.Count > 0)
                {
                    dr = li.Last();
                    writeToTxt(dr);
                }
                else
                {
                    dr = null;
                    writeToTxt(dr);
                }
            }
            else
            {
                writeToTxt(dr);
            }
            jIv.upModify0<JBS.JS.Iv>(IvNo.Text.Trim());//更新修改狀態0
            btnAppend.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridViewT1_Enter(object sender, EventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (dataGridViewT1.Rows.Count == 0)
            {
                gridAppend_Click(null, null);
            }
        }

        private void IvDate_Validating(object sender, CancelEventArgs e)
        {
            if (IvDate.ReadOnly) return;
            if (btnCancel.Focused) return;

            xe.DateValidate(sender, e);
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();

                var stno = StNo.Text.Trim();
                var itno = "";

                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    dtD.Rows[i]["stno"] = StNo.Text.Trim();
                    dtD.Rows[i]["stname"] = StName.Text.Trim();
                    itno = dtD.Rows[i]["itno"].ToString().Trim();
                    dtD.Rows[i]["stkqty"] = GetItemStock(itno, stno);
                    ToCountRowMny(i);
                }
                ToCountAllMny();

                this.TextBefore = row["StNo"].ToString().Trim();
            });
        }

        private void StNo_Enter(object sender, EventArgs e)
        {
            this.TextBefore = StNo.Text.Trim();
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (StNo.Text.Trim().Length == 0)
            {
                e.Cancel = true;
                StNo.Text = StName.Text = "";
                MessageBox.Show("倉庫編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                if (this.TextBefore == row["StNo"].ToString().Trim())
                    return;

                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();

                var stno = StNo.Text.Trim();
                var itno = "";

                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    dtD.Rows[i]["stno"] = StNo.Text.Trim();
                    dtD.Rows[i]["stname"] = StName.Text.Trim();
                    itno = dtD.Rows[i]["itno"].ToString().Trim();
                    dtD.Rows[i]["stkqty"] = GetItemStock(itno, stno);
                    ToCountRowMny(i);
                }
                ToCountAllMny();

                this.TextBefore = row["StNo"].ToString().Trim();
            });
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender, row =>
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
                EmNo.Text = EmName.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        void GridSaleDAddRows()
        {
            DataRow dRow = dtD.NewRow();
            dRow["itno"] = "";
            dRow["ItNoUdf"] = "";
            dRow["itname"] = "";
            dRow["stkqty"] = 0;
            dRow["Qty"] = 0;
            dRow["invqty"] = 0;
            dRow["itunit"] = "";
            dRow["Price"] = 0;
            dRow["Mny"] = 0;
            dRow["ItPkgQty"] = 0;
            dRow["ItTrait"] = 0;
            dRow["產品組成"] = "";
            dRow["Memo"] = "";
            //結構編號
            dRow["BomRec"] = GetBomRec();

            TextBox tno = new TextBox();
            TextBox tname = new TextBox();
            pVar.StkValidate(StNo.Text.Trim(), tno, tname);
            dRow["StNo"] = StNo.Text.Trim();
            dRow["StName"] = tname.Text;

            dtD.Rows.Add(dRow);
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dtD.Rows[i]["序號"] = (i + 1).ToString();
            }
            dtD.AcceptChanges();
        }

        void DeleteEmptyRow(int index)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                //刪除明細前，先刪除明細的『組件明細』
                string rec = dataGridViewT1.Rows[index].Cells["結構編號"].Value.ToString();

                var rows = dtB.AsEnumerable().Where(r => r["BomRec"].ToString() != rec);
                if (rows.Count() == 0) dtB.Clear();
                else
                {
                    dtB = rows.CopyToDataTable();
                }

                dtB.AcceptChanges();

                //刪除明細
                dataGridViewT1.Rows.Remove(dataGridViewT1.Rows[index]);
                dtD.AcceptChanges();
                dataGridViewT1.Focus();
                if (dataGridViewT1.Rows.Count > 0)
                {
                    dataGridViewT1.CurrentRow.Selected = true;
                    for (int i = 0; i < dtD.Rows.Count; i++)
                    {
                        dtD.Rows[i]["序號"] = (i + 1).ToString();
                    }
                }
                //刪除列後，重新計算帳款
                //SetSaleDMnyAll();
            }
        }

        void GridSaleDInsertRows(int index)
        {
            DataRow dRow = dtD.NewRow();
            dRow["itno"] = "";
            dRow["ItNoUdf"] = "";
            dRow["itname"] = "";
            dRow["stkqty"] = 0;
            dRow["Qty"] = 0;
            dRow["invqty"] = 0;
            dRow["itunit"] = "";
            dRow["Price"] = 0;
            dRow["Mny"] = 0;
            dRow["ItPkgQty"] = 0;
            dRow["ItTrait"] = 0;
            dRow["產品組成"] = "";
            dRow["Memo"] = "";
            //結構編號
            dRow["BomRec"] = GetBomRec();

            TextBox tno = new TextBox();
            TextBox tname = new TextBox();
            pVar.StkValidate(StNo.Text.Trim(), tno, tname);
            dRow["StNo"] = StNo.Text.Trim();
            dRow["StName"] = tname.Text;

            dtD.Rows.InsertAt(dRow, index);
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                dtD.Rows[i]["序號"] = (i + 1).ToString();
            }
            dtD.AcceptChanges();
        }



        decimal GetBomRec()
        {
            if (btnState == "Append" || btnState == "Duplicate")
            {
                BomRec++;
                return BomRec;
            }
            if (btnState == "Modify")
            {
                dtD.Rows.OfType<DataRow>().ToList().ForEach(r =>
                {
                    decimal d = 0;
                    decimal.TryParse(r["BomRec"].ToString(), out d);
                    if (d > BomRec)
                        BomRec = d;
                });
                BomRec++;
                return BomRec;
            }
            return 0;
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            if (StNo.Text.Trim() == "")
            {
                StNo.Focus();
                MessageBox.Show("倉庫編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;

            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                GridSaleDAddRows();
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
                //刪除明細前，先刪除明細的『組件明細』
                string rec = dataGridViewT1.CurrentRow.Cells["ItTrait"].Value.ToString();

                var rows = dtB.AsEnumerable().Where(r => r["BomRec"].ToString() != rec);
                if (rows.Count() == 0) dtB.Clear();
                else
                {
                    dtB = rows.CopyToDataTable();
                }
                dtB.AcceptChanges();

                //刪除明細
                int index = dataGridViewT1.SelectedRows[0].Index;
                dtD.Rows.RemoveAt(index);
                dtD.AcceptChanges();

                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    dtD.Rows[i]["序號"] = (i + 1).ToString();
                }
                ToCountAllMny();

                if (dtD.Rows.Count > 0)
                {
                    index = (index > dtD.Rows.Count - 1) ? dtD.Rows.Count - 1 : index;
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
                gridInsert.Focus();
                if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    GridSaleDInsertRows(index);
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells["產品編號"];
                    dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridItDesp_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridItDesp.Focus();
                if (dataGridViewT1["產品編號", dataGridViewT1.CurrentRow.Index].EditedFormattedValue.ToString().Trim() == "")
                {
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridViewT1.Focus();
                    return;
                }

                //載入此產品規格說明
                //using (FrmSale_ItDesp frm = new FrmSale_ItDesp())
                //{
                //    frm.SetParaeter(ViewMode.Normal);
                //    int index = dataGridViewT1.SelectedRows[0].Index;

                //    for (int x = 1; x <= 10; x++)
                //    {
                //        frm.desp[x - 1] = dtD.Rows[index]["ItDesp" + x].ToString();
                //    }
                //    frm.ShowDialog();

                //    for (int x = 1; x <= 10; x++)
                //    {
                //        dtD.Rows[index]["ItDesp" + x] = frm.desp[x - 1];
                //    }
                //    dtD.AcceptChanges();
                //    dataGridViewT1.Focus();
                //}

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
        }

        private void gridStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridStock.Focus();
                FrmSale_Stock frm = new FrmSale_Stock();
                frm.ItNo = dtD.Rows[dataGridViewT1.CurrentRow.Index]["ItNo"].ToString();
                frm.ShowDialog();
                dataGridViewT1.Focus();
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IvNo.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            var name = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (name == "產品編號")
            {
                using (var frm = new FrmItemb("NotShowStrait1"))
                {
                    frm.TSeekNo = dataGridViewT1.EditingControl.Text;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        xe.Validate<JBS.JS.Item>(frm.TResult, row => { FillItem(row, e.RowIndex); });
                    }
                }
            }
            else if (name == "盤點數量")
            {

            }
            else if (name == "")
            {

            }
        }

        void ToCountRowMny(int index)
        {
            decimal invqty = 0;
            decimal mny = 0;

            var qty = dataGridViewT1["盤點數量", index].EditedFormattedValue.ToDecimal();
            var stkqty = dataGridViewT1["帳上庫存量", index].EditedFormattedValue.ToDecimal();
            var price = dataGridViewT1["平均成本", index].EditedFormattedValue.ToDecimal();

            invqty = qty - stkqty;
            mny = qty * price;

            dtD.Rows[index]["invqty"] = invqty.ToString("f" + Common.Q);
            dtD.Rows[index]["mny"] = mny.ToString("f" + Common.MF);
        }

        void ToCountAllMny()
        {
            decimal d = 0;
            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                d += dtD.Rows[i]["mny"].ToDecimal();
            }
            IvSum.Text = d.ToString("f" + Common.MF);
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        { 
            if (dataGridViewT1.ReadOnly) return;
            if (gridDelete.Focused || btnCancel.Focused) return;

            var name = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (name == "產品編號")
            {
                string ItNoNow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoNow.Trim().Length == 0)
                {
                    dtD.Rows[e.RowIndex]["itno"] = "";
                    dtD.Rows[e.RowIndex]["ItNoUdf"] = "";
                    dtD.Rows[e.RowIndex]["itname"] = "";
                    dtD.Rows[e.RowIndex]["stkqty"] = 0;
                    dtD.Rows[e.RowIndex]["Qty"] = 0;
                    dtD.Rows[e.RowIndex]["invqty"] = 0;
                    dtD.Rows[e.RowIndex]["itunit"] = "";
                    dtD.Rows[e.RowIndex]["Price"] = 0;
                    dtD.Rows[e.RowIndex]["Mny"] = 0;
                    dtD.Rows[e.RowIndex]["ItPkgQty"] = 1;
                    dtD.Rows[e.RowIndex]["ItTrait"] = 0;
                    dtD.Rows[e.RowIndex]["產品組成"] = "";
                    dtD.Rows[e.RowIndex]["Memo"] = "";

                    TextBox tno = new TextBox();
                    TextBox tname = new TextBox();
                    pVar.StkValidate(StNo.Text.Trim(), tno, tname);
                    dtD.Rows[e.RowIndex]["StNo"] = StNo.Text.Trim();
                    dtD.Rows[e.RowIndex]["StName"] = tname.Text;

                    var rec = dtD.Rows[e.RowIndex]["BomRec"].ToString().Trim();
                    xe.RemoveBom(rec, ref dtB);

                    dataGridViewT1.InvalidateRow(e.RowIndex);

                    ToCountRowMny(e.RowIndex);
                    ToCountAllMny();
                }
                else if (ItNoNow == ItNoBegin) return;
                else if (ItNoNow != ItNoBegin && ItNoNow == UdfNoBegin)
                {
                    dtD.Rows[e.RowIndex]["ItNo"] = ItNoBegin;
                }
                else if (ItNoNow != ItNoBegin && ItNoNow != UdfNoBegin)
                {
                    using (var db = new JBS.xSQL())
                    {
                        var tsql = "Select ItNo from item where itno=@itno or itnoudf=@itno";
                        var itno = db.ExecuteScalar(tsql, spc => spc.AddWithValue("itno", ItNoNow));
                        if (itno != null && itno.ToString().Trim().Length > 0)
                        {
                            ItNoNow = itno.ToString().Trim();

                            dtD.Rows[e.RowIndex]["ItNo"] = ItNoNow;
                        }
                    }

                    var count = dtD.AsEnumerable().Count(r => r["itno"].ToString().Trim() == ItNoNow);
                    if (count > 1)
                    {
                        e.Cancel = true;
                        MessageBox.Show("此盤點單據已有相同產品!"); 
                        return;
                    }

                    var result = false;
                    xe.Validate<JBS.JS.Item>(ItNoNow, row =>
                    {
                        result = true;
                        FillItem(row, e.RowIndex);
                    });

               

                    if (result == false)
                    {
                        e.Cancel = true;
                        using (var frm = new FrmItemb("NotShowStrait1"))
                        {
                            frm.TSeekNo = ItNoNow;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                xe.Validate<JBS.JS.Item>(frm.TResult, row => { FillItem(row, e.RowIndex); });
                            }
                        }
                    }
                }
            }
            else if (name == "盤點數量")
            {
                var qty = dataGridViewT1["盤點數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                dtD.Rows[e.RowIndex]["qty"] = qty;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                ToCountRowMny(e.RowIndex);
                ToCountAllMny();
            }
            else if (name == "平均成本")
            {
                var price = dataGridViewT1["平均成本", e.RowIndex].EditedFormattedValue.ToDecimal();
                dtD.Rows[e.RowIndex]["price"] = price;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                ToCountRowMny(e.RowIndex);
                ToCountAllMny();
            }
        }

        private void FillItem(SqlDataReader row, int index)
        {
            var itno = row["itno"].ToString().Trim();
            var stno = StNo.Text.Trim();

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = itno;

            dtD.Rows[index]["itno"] = itno;
            dtD.Rows[index]["ItNoUdf"] = row["itnoudf"].ToString().Trim();
            dtD.Rows[index]["itname"] = row["itname"].ToString();
            dtD.Rows[index]["ItNoUdf"] = row["ItNoUdf"].ToString();
            dtD.Rows[index]["stkqty"] = GetItemStock(itno, stno);
            dtD.Rows[index]["Qty"] = 0;
            dtD.Rows[index]["invqty"] = 0;
            dtD.Rows[index]["itunit"] = row["ItUnit"].ToString().Trim();
            dtD.Rows[index]["ItPkgQty"] = 1;
            dtD.Rows[index]["Price"] = row["ItBuyPri"].ToString().Trim();
            dtD.Rows[index]["Mny"] = 0;

            var trait = row["ItTrait"].ToDecimal();
            if (trait == 1) dtD.Rows[index]["產品組成"] = "組合品";
            else if (trait == 2) dtD.Rows[index]["產品組成"] = "組裝品";
            else if (trait == 3) dtD.Rows[index]["產品組成"] = "單一商品";
            dtD.Rows[index]["ItTrait"] = trait;

            for (int x = 1; x <= 10; x++)
            {
                dtD.Rows[index]["ItDesp" + x] = row["ItDesp" + x];
            }

            var rec = dtD.Rows[index]["BomRec"].ToString().Trim();
            xe.RemoveBom(rec, ref dtB);
            xe.GetItemBom(itno, rec, ref dtB);

            ToCountRowMny(index);
            ToCountAllMny();
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                xe.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        decimal GetItemStock(string itno, string stno)
        {
            decimal qty = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = " select stock.itqty from item left join stock on item.itno=stock.itno where 0=0 "
                               + " and item.itno=@itno"
                               + " and stock.stno=@stno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("itno", itno);
                        cmd.Parameters.AddWithValue("stno", stno);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable t = new DataTable();
                            da.Fill(t);
                            if (t.Rows.Count > 0) qty = t.Rows[0]["itqty"].ToDecimal();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return qty;
        }

        void GetItemBom(string itno, string rec)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from bomd where boitno=@itno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("itno", itno);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable t = new DataTable())
                            {
                                da.Fill(t);
                                for (int i = 0; i < t.Rows.Count; i++)
                                {
                                    DataRow row = dtB.NewRow();
                                    row["bomrec"] = rec;
                                    row["itno"] = t.Rows[i]["itno"].ToString().Trim();
                                    row["itname"] = t.Rows[i]["itname"];
                                    row["itunit"] = t.Rows[i]["itunit"];
                                    row["itqty"] = t.Rows[i]["itqty"];
                                    row["itpareprs"] = t.Rows[i]["itpareprs"];
                                    row["itpkgqty"] = t.Rows[i]["itpkgqty"];
                                    row["itrec"] = t.Rows[i]["itrec"];
                                    row["itprice"] = t.Rows[i]["itprice"];
                                    row["itprs"] = t.Rows[i]["itprs"];
                                    row["itmny"] = t.Rows[i]["itmny"];
                                    dtB.Rows.Add(row);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                case Keys.D2:
                case Keys.NumPad2:
                    btnModify.Focus();
                    btnModify.PerformClick();
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
                case Keys.F6:
                    gridItDesp.PerformClick();
                    break;
                case Keys.F8:
                    gridStock.PerformClick();
                    break;
                case Keys.F2:
                    if (gridAppend.Enabled)
                        gridAppend.PerformClick();
                    break;
                case Keys.F3:
                    if (gridDelete.Enabled)
                        gridDelete.PerformClick();
                    break;
                case Keys.F5:
                    if (gridInsert.Enabled)
                        gridInsert.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            if (IvNo.Text.Trim() == "")
                return;

            using (var frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = dr["AppScNo"].ToString();
                frm.ATime = dr["AppDate"].ToString();
                //修改人員
                frm.EName = dr["EdtScNo"].ToString();
                frm.ETime = dr["EdtDate"].ToString();
                frm.ShowDialog();
            }
        }

        private void IvToThis_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmIvToThis())
            {
                frm.dtM = dtM.Clone();
                frm.dtD = dtD.Clone();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    tempNo = frm.invno;
                    btnCancel_Click(null, null);
                }
            }
        }

        bool IsPowerful(object TKey)
        {
            if (TKey.IsNullOrEmpty()) return true;
            bool ispower = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select m.scname,d.* from scrit as m,scritd as d where '0'='0' "
                            + " and d.taname='" + TKey.ToString().Trim() + "'"
                            + " and m.scno=d.scno "
                            + " and m.scname='" + Common.User_Name + "' COLLATE Chinese_Taiwan_Stroke_BIN";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                //檢查『無權限』是否有打v，沒打v則可以通行 
                                if (reader["sc09"].ToString().Trim() == "")
                                    ispower = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return ispower;
        }

        private void btnToAdjust_Click(object sender, EventArgs e)
        {
            string count = SQL.ExecuteScalar(@"
 select count(*) from scrit as m,scritd as d where
 d.taname='庫存調整作業' and m.scno=d.scno and m.scname='BM' and sc01 ='V' COLLATE Chinese_Taiwan_Stroke_BIN", null);

            if (!this.IsPowerful("庫存調整作業") || count =="0")
            {
                MessageBox.Show("無使用權限", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (AdNo.Text.Trim().Length > 0)
            {
                MessageBox.Show("此單據已轉調整單！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (IvNo.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jIv.IsModify<JBS.JS.Iv>(IvNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法進行轉入作業", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                LoadDB();
                dr = getCurrentDataRow(IvNo.Text.Trim());
                if (li.IndexOf(dr) == -1)
                {
                    if (li.Count > 0)
                    {
                        dr = li.Last();
                        writeToTxt(dr);
                    }
                    else
                    {
                        dr = null;
                        writeToTxt(dr);
                    }
                }
                else
                {
                    writeToTxt(dr);
                }
            }

            var machine = "";
            //if (Common.Sys_StockKind == 1)
            //{
            //    machine = "";
            //}
            //else
            //{
            //    if (File.Exists(Common.StartUpIniFileName))
            //    {
            //        //唯有pos系統，且是多點情況下，機台號碼存1號機做傳輸用
            //        var SIP = Common.GetPrivateProfileString("POS設定", "SERVER");
            //        machine = "1";
            //        if (SIP.Trim() == "-1") machine = "";
            //        if (Common.Series.ToDecimal() == 71) machine = "";
            //    }
            //    else
            //    {
            //        machine = "";
            //        MessageBox.Show("設定檔遺失！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            SqlTransaction tn = null;
            DataTable t;
            string ip = "";
            string adno = "";
            string sql = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    sql = "select ip from stkroom where stno=@stno";
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

                    var date = "";
                    if (Common.Sys_NoAdd >= 3)
                    {
                        date = Date.ToUSDate(IvDate.Text.Trim());
                    }
                    else
                    {
                        date = Date.ToTWDate(IvDate.Text.Trim());
                    }

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("adno", ip + date);
                        cmd.CommandText = "select adno from adjust where adno like @adno + '%' order by adno desc";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            t = new DataTable();
                            if (reader.HasRows) t.Load(reader);
                        }
                    }
                    decimal d = 1;
                    var collection = t.AsEnumerable();

                    if (Common.Sys_NoAdd == 1)
                    {
                        adno = ip + date + (d.ToString().PadLeft(4, '0'));
                    }
                    else if (Common.Sys_NoAdd == 2)
                    {
                        adno = ip + date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                    }
                    else if (Common.Sys_NoAdd == 3)
                    {
                        adno = ip + date + (d.ToString().PadLeft(4, '0'));
                    }
                    else if (Common.Sys_NoAdd == 4)
                    {
                        adno = ip + date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                    }
                    while (collection.Count(r => r["adno"].ToString().Trim() == adno) > 0)
                    {
                        d++;
                        if (Common.Sys_NoAdd == 1)
                        {
                            adno = ip + date + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 2)
                        {
                            adno = ip + date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                        }
                        else if (Common.Sys_NoAdd == 3)
                        {
                            adno = ip + date + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 4)
                        {
                            adno = ip + date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                        }
                    }

                    tn = cn.BeginTransaction();

                    AdNo.Text = adno.Trim();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Transaction = tn;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("adno", adno.Trim());
                        cmd.Parameters.AddWithValue("ivno", dr["ivno"].ToString().Trim());
                        cmd.CommandText = "update iv set adno=@adno where ivno=@ivno";
                        cmd.ExecuteNonQuery();
                    }

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Transaction = tn;
                        cmd.CommandText = "select * from Adjustd where 1=0";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt2);
                            for (int i = 0; i < dtD.Rows.Count; i++)
                            {
                                if (dtD.Rows[i]["invqty"].ToDecimal() == 0) continue;
                                DataRow row = dt2.NewRow();
                                row["adno"] = adno;
                                row["addate"] = dr["ivdate"];
                                row["addate1"] = dr["ivdate1"];
                                row["stno"] = dr["stno"];
                                row["stname"] = dr["stname"];
                                row["emno"] = dr["emno"];
                                row["itno"] = dtD.Rows[i]["itno"];
                                row["itname"] = dtD.Rows[i]["itname"];
                                row["ittrait"] = dtD.Rows[i]["ittrait"];
                                row["itunit"] = dtD.Rows[i]["itunit"];
                                var itpkgqty = 1;
                                row["itpkgqty"] = itpkgqty.ToString("f" + Common.Q);
                                row["qty"] = dtD.Rows[i]["invqty"].ToDecimal().ToString("f" + Common.Q);
                                row["costb"] = dtD.Rows[i]["price"].ToDecimal().ToString("f" + Common.MF);
                                var mny = row["qty"].ToDecimal() * row["costb"].ToDecimal();
                                row["mnyb"] = mny.ToString("f" + Common.MF);
                                row["bomid"] = adno + (i + 1).ToString().PadLeft(10, '0');
                                row["bomrec"] = (i + 1).ToString();
                                row["recordno"] = (i + 1).ToString();
                                row["bracket"] = "盤點";
                                row["itdesp1"] = dtD.Rows[i]["itdesp1"];
                                row["itdesp2"] = dtD.Rows[i]["itdesp2"];
                                row["itdesp3"] = dtD.Rows[i]["itdesp3"];
                                row["itdesp4"] = dtD.Rows[i]["itdesp4"];
                                row["itdesp5"] = dtD.Rows[i]["itdesp5"];
                                row["itdesp6"] = dtD.Rows[i]["itdesp6"];
                                row["itdesp7"] = dtD.Rows[i]["itdesp7"];
                                row["itdesp8"] = dtD.Rows[i]["itdesp8"];
                                row["itdesp9"] = dtD.Rows[i]["itdesp9"];
                                row["itdesp10"] = dtD.Rows[i]["itdesp10"];
                                row["IsTrans"] = machine;

                                dt2.Rows.Add(row);
                            }
                            using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                            {
                                da.InsertCommand = builder.GetInsertCommand();
                                da.InsertCommand.Transaction = tn;
                                da.Update(dt2);
                            }
                        }
                    }
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Transaction = tn;
                        cmd.CommandText = "select * from Adjust where 1=0";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt1);
                            DataRow row = dt1.NewRow();
                            row["adno"] = adno;
                            row["addate"] = dr["ivdate"];
                            row["addate1"] = dr["ivdate1"];
                            row["stno"] = dr["stno"];
                            row["stname"] = dr["stname"];
                            row["emno"] = dr["emno"];
                            row["emname"] = dr["emname"];
                            row["admemo"] = "盤點單轉入";
                            row["totmnyb"] = dt2.AsEnumerable().Sum(r => r["mnyb"].ToDecimal());
                            row["recordno"] = dtD.Rows.Count;
                            row["bracket"] = "盤點";
                            row["appscno"] = Common.User_Name;
                            row["appdate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                            row["edtscno"] = Common.User_Name;
                            row["edtdate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                            row["IsTrans"] = machine;
                            row.EndEdit();
                            dt1.Rows.Add(row);
                            using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                            {
                                da.InsertCommand = builder.GetInsertCommand();
                                da.InsertCommand.Transaction = tn;
                                da.Update(dt1);
                            }
                        }
                    }
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Transaction = tn;
                        DataTable tbom = new DataTable();
                        cmd.CommandText = "Select top 1 * from AdjuBom where 1=0";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tbom);
                        }
                        Common.加庫存(cmd, dt2, tbom, "stno", "qty");
                    }
                    if (dt2.Rows.Count == 0)
                    {
                        AdNo.Clear();
                        tn.Rollback();
                        MessageBox.Show("找不到需要調整的產品！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        tn.Commit();
                        MessageBox.Show("盤點單資料更新及轉入調整單，完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }






































































































    }
}
