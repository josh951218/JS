using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S1
{
    public partial class FrmNetOrder : Formbase
    {
        JBS.JS.xEvents xe;

        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
      
        DataTable OrM = new DataTable();
        DataTable OrD = new DataTable();
        DataTable OrBom = new DataTable();
        List<TextBoxbase> list;

        //詳細備註
        string memo1 = "";
        int Current序號 = 0;
        public FrmNetOrder()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();

            pVar.SetMemoUdf(this.說明);

            this.訂單數量.Set庫存數量小數();
            this.計價數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.訂單未交量.Set庫存數量小數();
            this.未入庫數量.Set庫存數量小數();

            this.售價.Set銷貨單價小數();
            this.稅前金額.Set銷項金額小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前售價.FirstNum = 9;
            this.稅前售價.LastNum = 6;
            this.稅前售價.DefaultCellStyle.Format = "f6";

            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.FirstNum = 9;
            this.本幣稅前單價.LastNum = 6;
            this.本幣稅前單價.DefaultCellStyle.Format = "f6";

            Xa1Par.FirstNum = 4;
            Xa1Par.LastNum = 4;

            TaxMnyB.FirstNum = 16;
            TaxMnyB.LastNum = Common.M;

            TaxMny.FirstNum = Common.nFirst;
            TaxMny.LastNum = Common.MST;

            Tax.FirstNum = Common.nFirst;
            Tax.LastNum = Common.TS;

            TotMny.FirstNum = Common.nFirst;
            TotMny.LastNum = Common.MST;

            Rate.FirstNum = 1;
            Rate.LastNum = 0;

            OrDate.MaxLength = EsDate.MaxLength = (Common.User_DateTime == 1) ? 7 : 8;
            this.交貨日期.DataPropertyName = (Common.User_DateTime == 1) ? "esdate" : "esdate1";
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            this.TResult = "";
            var row = dataGridView_changeAndShow("top");
        }

        private void FrmOrder_Shown(object sender, EventArgs e)
        {
            btnBrow.Focus();
        }

        void WriteToTxt(SqlDataReader row)
        {
            OrD.Clear();
            memo1 = "";
            if (row == null)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
            }
            else
            {
                memo1 = MyToString(row["ormemo1"]);
                OrNo.Text = MyToString(row["orno"]);

                if (Common.User_DateTime == 1)
                    OrDate.Text = MyToString(row["ordate"]);
                else
                    OrDate.Text = MyToString(row["ordate1"]);

                if (MyToString(row["esdate"]) != "")
                    EsDate.Text = Common.User_DateTime == 1 ? MyToString(row["esdate"]) : MyToString(row["esdate1"]);
                else
                    EsDate.Text = OrDate.Text;

                EmNo.Text    = MyToString(row["emno"]);
                EmName.Text  = MyToString(row["EmName"]);
                CuNo.Text    = MyToString(row["CuNo"]);
                CuName1.Text = MyToString(row["CuName1"]);
                CuPer1.Text  = MyToString(row["CuPer1"]);
                CuTel1.Text  = MyToString(row["CuTel1"]);
                CuAddr1.Text = MyToString(row["CuAddr1"]);
                Xa1No.Text   = MyToString(row["Xa1No"]);
                Xa1Name.Text = MyToString(row["Xa1Name"]);
                Xa1Par.Text  = MyToString(row["Xa1Par"]);

                TaxMny.Text = row["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                TaxMnyB.Text = row["TaxMnyB"].ToDecimal().ToString("f" + Common.M);

                X3No.Text = MyToString(row["X3No"]);
                Rate.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");
                Tax.Text = row["Tax"].ToDecimal().ToString("f" + Common.TS);
                TotMny.Text = row["TotMny"].ToDecimal().ToString("f" + Common.MST);

                OrPayment.Text = MyToString(row["OrPayment"]);
                Orperiod.Text = MyToString(row["Orperiod"]);
                OrMemo.Text = MyToString(row["OrMemo"]);
                CardNo.Text = MyToString(row["CardNo"]);

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@x3no", X3No.Text.Trim());
                    cmd.Parameters.AddWithValue("@CuNo", X3No.Text.Trim());
                    cmd.CommandText = "select x3name from xx03 where x3no=@x3no";
                    var p = cmd.ExecuteScalar();
                    if (p != null) X3Name.Text = p.ToString().Trim();
                }

                SysNo.Text = MyToString(row["SysOrNo"]);
                var state = row["orderState"].ToDecimal();
                if (state == 0) OrState.Text = "未受理";
                else if (state == 1) OrState.Text = "已受理";
                else if (state == 2) OrState.Text = "已出貨";

                loadD();
            }
        }

        private string MyToString(object object1)
        {
            if (object1 == null) return "";
            else
                return object1.ToString();
        }

        void loadD()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                cmd.CommandText = @" select 序號 = ROW_NUMBER() OVER(order by orno) ,
                                   產品組成="
                                + " case" 
                                + " when ittrait=1 then '組合品'"
                                + " when ittrait=2 then '組裝品'"
                                + " when ittrait=3 then '單一商品'"
                                + " end,存量預估="
                                + " case"
                                + " when stkqtyflag=1 then '展開'"
                                + " when stkqtyflag=2 then '不展開'"
                                + " end,*"
                                + " from weborderd where orno=@orno";
                dd.Fill(OrD);
                dataGridViewT1.DataSource = OrD;
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            dataGridView_changeAndShow("Top");
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var row = dataGridView_changeAndShow("previous");
            if (row == false)
            {
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool dataGridView_changeAndShow(string state,string orno="")
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            {
                cn.Open();
                cm.CommandText = "select count(*) from weborder";
                int max序號 = cm.ExecuteScalar().ToInteger();
                if (max序號 == 0) return false;
                switch (state.ToLower())
                {
                    case "get_browse":
                        #region 資料瀏覽取回時，然後return true
                        cm.Parameters.AddWithValue("orno", orno);
                        cm.CommandText = @"
select top 1 * from(
select top 999999 序號 = ROW_NUMBER() OVER(order by orderState asc,ordate desc,esdate desc)
,* from weborder order by orderState asc,ordate desc,esdate desc
) a where orno=@orno
";
                        SqlDataReader dr_ = cm.ExecuteReader();
                        if (dr_.HasRows)
                        {
                            while (dr_.Read())
                            {
                                Current序號 = dr_["序號"].ToString().ToInteger();
                                WriteToTxt(dr_);
                            }
                        }

                        return true;
                        #endregion
                    case "last":       Current序號 = max序號; break;
                    case "top":        Current序號 =  1;      break;
                    case "next":       Current序號 += 1;      break;
                    case "previous":   Current序號 -= 1;      break;
                }
                if (Current序號 > max序號)
                {
                    Current序號 = max序號;
                   // return false;
                }
                if (Current序號 <= 0)
                {
                    Current序號 = 1;
                   // return false;
                }
                cm.Parameters.AddWithValue("序號", Current序號.ToString());
                string sqlStr = @"
select top 1 * from(
select top 999999 序號 = ROW_NUMBER() OVER(order by orderState asc,ordate desc,esdate desc)
,* from weborder order by orderState asc,ordate desc,esdate desc
) a where 序號=@序號
";
                cm.CommandText = sqlStr;
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        WriteToTxt(dr);
                    }
                }
                return true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var row =dataGridView_changeAndShow("Next");
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            var row = dataGridView_changeAndShow("last");
            //btnTop.Enabled = true;
            //btnPrior.Enabled = true;
            //btnNext.Enabled = false;
            //btnBottom.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增！", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (SysNo.TrimTextLenth() > 0)
            {
                MessageBox.Show("此筆網路訂單已轉正式訂單，無法刪除！", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var delete = false;
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
                    cmd.Parameters.AddWithValue("OrNo", OrNo.Text.Trim());

                    cmd.CommandText = " delete from weborder    where OrNo = (@OrNo);";
                    cmd.CommandText += "delete from weborderd   where OrNo = (@OrNo);";
                    cmd.CommandText += "delete from weborderbom where OrNo = (@OrNo);";
                    cmd.ExecuteNonQuery();

                    tn.Commit();
                    delete = true;
                }
                catch (Exception ex)
                {
                    if (tn != null) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
            if (delete) btnNext_Click(null, null);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (OrNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new S1.FrmNetOrderBrow())
            {
                frm.TSeekNo = OrNo.Text.Trim();
                frm.ShowDialog();
                var orno = frm.TResult; //text
                dataGridView_changeAndShow("get_browse", orno);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = false;
                frm.memo1 = memo1;

                if (frm.ShowDialog() == DialogResult.OK) memo1 = frm.memo1;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D4:
                    if (btnBrow.Enabled) btnBrow.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    if (btnExit.Enabled) btnExit.PerformClick();
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
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnToOrder_Click(object sender, EventArgs e)
        {
            if (OrNo.TrimTextLenth() == 0) return;
             
            if (xe.IsEditInCloseDay(this.OrDate.Text) == false)
                return; 

            if (SysNo.TrimTextLenth() > 0)
            {
                MessageBox.Show("此筆資料已轉為系統訂單，請勿重複轉單！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("請確定是否將此筆資料轉為系統訂單?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;

            TextBoxT OrDate = new TextBoxT();
            OrDate.Name = "";
            OrDate.Text = Date.GetDateTime(Common.User_DateTime);
            OrDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

            TextBoxT orno = new TextBoxT();
            orno.Name = "orno";
            orno.MaxLength = 16;

            if (Common.JESetSSID("dbo.[Order]", ref OrDate, ref orno) == false)
            {
                MessageBox.Show("單據號碼計算錯誤，轉單失敗！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlTransaction tn = null;
            DataTable dt = new DataTable();
            DataTable dtD = new DataTable();
            DataTable dtBom = new DataTable();
            DataTable temp = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = "Select * from dbo.[order] where 1 = 0";
                da.Fill(dt);
                cmd.CommandText = "Select * from dbo.[orderd] where 1 = 0";
                da.Fill(dtD);
                cmd.CommandText = "Select * from dbo.[orderbom] where 1 = 0";
                da.Fill(dtBom);

                cmd.Parameters.AddWithValue("OrNo", OrNo.Text);
                cmd.CommandText = "Select * from weborder where OrNo = (@OrNo)";
                da.Fill(dt);
                cmd.CommandText = "Select * from weborderd where OrNo = (@OrNo)";
                da.Fill(dtD);
                cmd.CommandText = "Select * from weborderbom where OrNo = (@OrNo)";
                da.Fill(dtBom);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["OrNo"] = OrNo.Text.Trim();
                    dt.Rows[i]["NetNo"] = OrNo.Text.Trim();
                    dt.Rows[i]["recordno"] = dtD.Rows.Count;
                    dt.Rows[i]["AppDate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                    dt.Rows[i]["EdtDate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                    dt.Rows[i]["AppScNo"] = Common.User_Name;
                    dt.Rows[i]["EdtScNo"] = Common.User_Name;
                    dt.Rows[i]["cuper1"]  = CuPer1.Text;
                    dt.Rows[i]["AdAddr"] = CuAddr1.Text;
                    dt.Rows[i]["cutel1"] = CuTel1.Text;
                    dt.Rows[i].AcceptChanges();
                    dt.Rows[i].SetAdded();
                }
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    dtD.Rows[i]["OrNo"] = OrNo.Text.Trim();
                    dtD.Rows[i]["bomid"] = OrNo.Text.Trim() + (i + 1).ToString().PadLeft(10, '0');
                    dtD.Rows[i]["bomrec"] = (i + 1).ToString().PadLeft(10, '0');
                    dtD.Rows[i]["qtynotout"] = dtD.Rows[i]["qty"].ToDecimal();
                    dtD.Rows[i]["qtynotinStk"] = dtD.Rows[i]["qty"].ToDecimal();
                    dtD.Rows[i]["PQty"] = dtD.Rows[i]["qty"].ToDecimal();
                    dtD.Rows[i]["mwidth1"] = 0;
                    dtD.Rows[i]["mwidth2"] = 0;
                    dtD.Rows[i]["mwidth3"] = 0;
                    dtD.Rows[i]["mwidth4"] = 0;
                    dtD.Rows[i]["pformula"] = "";
                    dtD.Rows[i]["AdPer1"] = CuPer1.Text;
                    dtD.Rows[i]["AdAddr"] = CuAddr1.Text;
                    dtD.Rows[i]["AdTel"] = CuTel1.Text;
                    dtD.Rows[i].AcceptChanges();
                    dtD.Rows[i].SetAdded();
                }
                //for (int i = 0; i < dtBom.Rows.Count; i++)
                //{
                //    dtBom.Rows[i]["OrNo"] = orno.Text.Trim();
                //    dtBom.Rows[i]["bomid"] = orno.Text.Trim() + dtBom.Rows[i]["bomrec"].ToString().PadLeft(10, '0');
                //    dtBom.Rows[i].AcceptChanges();
                //    dtBom.Rows[i].SetAdded();
                //}

                try
                {
                    if (cn.State != ConnectionState.Open) cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.CommandText = "Select * from dbo.[order] where 1=0";
                    da.Fill(temp);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.InsertCommand = builder.GetInsertCommand();
                    da.Update(dt);

                    cmd.CommandText = "Select * from dbo.[orderd] where 1=0";
                    da.Fill(temp);
                    builder = new SqlCommandBuilder(da);
                    da.InsertCommand = builder.GetInsertCommand();
                    da.Update(dtD);

                    cmd.CommandText = "Select * from dbo.[orderbom] where 1=0";
                    da.Fill(temp);
                    builder = new SqlCommandBuilder(da);
                    da.InsertCommand = builder.GetInsertCommand();
                    da.Update(dtBom);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("OrNo", OrNo.Text.Trim());
                    cmd.Parameters.AddWithValue("SysOrNo", OrNo.Text.Trim());
                    cmd.CommandText = " Update weborder set SysOrNo = (@SysOrNo), orderState = '1' where OrNo = (@OrNo);";
                    cmd.ExecuteNonQuery();

                    tn.Commit();
                    MessageBox.Show("傳輸成功！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dt.Clear();

                    cmd.CommandText = "Select top 1 * from weborder where OrNo = (@OrNo)";
                    da.Fill(dt);
                    var row = dataGridView_changeAndShow("this");
                }
                catch (Exception ex)
                {
                    if (tn != null) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void 批次轉訂單Btn_Click(object sender, EventArgs e)
        {
            using (網路訂單管理系統_批次轉單 frm = new 網路訂單管理系統_批次轉單())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                    case DialogResult.Cancel: break;
                }
            }
        }



    }
}
