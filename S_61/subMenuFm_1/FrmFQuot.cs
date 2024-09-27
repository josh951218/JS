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
using S_61.subMenuFm_2;
using System.IO;
using System.Diagnostics;
using S_61.SOther;

namespace S_61.subMenuFm_1
{
    public partial class FrmFQuot : Formbase
    {
        JBS.JS.FQuot jFQuot;

        DataTable FqD = new DataTable();
        DataTable FqBom = new DataTable();

        List<TextBoxbase> list;

        DataTable T = new DataTable();//製單人員
        DataTable DaFile = new DataTable();
        SqlTransaction tn;

        string tempNo = "";
        string btnState = "";
        string ItNoBegin = "";
        string UdfNoBegin = "";
        decimal BomRec = 0;

        string FaName2 = "";
        string TextBefore = "";
        string memo1 = "";//詳細備註
        string sql = "";
        string PhotoPath = "";// 13.7c  

        public FrmFQuot()
        {
            InitializeComponent();
            this.jFQuot = new JBS.JS.FQuot();
            this.list = this.getEnumMember();
            this.dataGridViewT1.tableName = "Fquotd";

            pVar.SetMemoUdf(this.說明);

            this.詢價數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();

            this.進價.Set銷貨單價小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前進價.FirstNum = 9;
            this.稅前進價.LastNum = 6;
            this.稅前進價.DefaultCellStyle.Format = "f6";
            this.稅前金額.Set進項金額小數();
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
            TaxMny.LastNum = Common.MFT;

            Tax.FirstNum = Common.nFirst;
            Tax.LastNum = Common.TF;

            TotMny.FirstNum = Common.nFirst;
            TotMny.LastNum = Common.MFT;

            Rate.FirstNum = 1;
            Rate.LastNum = 0;

            FqDate.SetDateLength();
            FqDateS.SetDateLength();

            if (Common.Series == "74")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;

                EmNo.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);

            }
            else if (Common.Series == "73")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = false;
            }
            else if (Common.Series == "72")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = true;

                EmNo.Validating += new CancelEventHandler(Xa1Par_Validating);
                Xa1Par.Validating -= new CancelEventHandler(Xa1Par_Validating);

            }
            else if (Common.Series == "71")
            {
                Xa1No.Enabled = false;
                Xa1Name.Enabled = false;
                Xa1Par.Enabled = true;
            }

            //金額權限
            TaxMnyB.Visible = Common.User_ShopPrice;
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
            this.本幣稅前金額.Visible = Common.User_ShopPrice;
            this.本幣稅前單價.Visible = Common.User_ShopPrice;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        private void FrmFQuot_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlDataAdapter da = new SqlDataAdapter("select Top(1)* from [fquot] where 1=0", cn))
            {
                da.Fill(T);//製單人員
            }

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = @"
                    select 產品組成=
                    case
                    when ittrait=1 then '組合品'
                    when ittrait=2 then '組裝品'
                    when ittrait=3 then '單一商品'
                    end,*,ItNoUdf='' from fquotd where 1=0 ";

                da.Fill(FqD);
                dataGridViewT1.DataSource = FqD;
            }
            FqD.Clear();

            WriteToTxt(Common.load("Bottom", "fquot", "fqno"));
           
        }

        private void FrmFQuot_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        void WriteToTxt(DataRow row)
        {
            T.Clear();
            FqD.Clear();
            tempNo = memo1 = "";

            if (row == null)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);

                FqD.Clear();
                FqBom.Clone();
            }
            else
            {
                memo1 = row["fqmemo1"].ToString();//詳細備註
                T.ImportRow(row);
                T.AcceptChanges();

                FqNo.Text = row["FqNo"].ToString().Trim();
                tempNo = FqNo.Text.Trim();

                if (Common.User_DateTime == 1)
                {
                    FqDate.Text = row["FqDate"].ToString();
                    FqDateS.Text = row["FqDateS"].ToString();
                }
                else
                {
                    FqDate.Text = row["FqDate1"].ToString();
                    FqDateS.Text = row["FqDateS1"].ToString();
                }

                EmNo.Text = row["emno"].ToString();
                EmName.Text = row["emname"].ToString();
                FaNo.Text = row["FaNo"].ToString();
                FaName1.Text = row["FaName1"].ToString();
                FaPer1.Text = row["FaPer1"].ToString();
                FaTel1.Text = row["FaTel1"].ToString();
                Xa1No.Text = row["xa1no"].ToString();
                Xa1Name.Text = row["xa1name"].ToString();
                Xa1Par.Text = row["xa1par"].ToDecimal().ToString("f4");
                TaxMny.Text = row["taxmny"].ToDecimal().ToString("f" + Common.MFT);
                TaxMnyB.Text = row["taxmnyb"].ToDecimal().ToString("f" + Common.M);
                X3No.Text = row["x3no"].ToString();

                Rate.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");
                FqPayment.Text = row["FqPayment"].ToString();
                Fqperiod.Text = row["Fqperiod"].ToString();
                FqMemo.Text = row["FqMemo"].ToString();
                Tax.Text = row["tax"].ToDecimal().ToString("f" + Common.TF);
                TotMny.Text = row["totmny"].ToDecimal().ToString("f" + Common.MFT);
                PhotoPath = row["PhotoPath"].ToString();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
                    cmd.CommandText = "select x3name from xx03 where x3no=@x3no";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                        X3Name.Text = cmd.ExecuteScalar().ToString();
                    cmd.Dispose();
                }
                loadD();
            }
        }

        void loadD()
        {
            FqD.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                cmd.CommandText = @"
                    select 產品組成=
                    case
                    when ittrait=1 then '組合品'
                    when ittrait=2 then '組裝品'
                    when ittrait=3 then '單一商品'
                    end,ItNoUdf= (select top 1 itnoudf from item where item.itno = fquotd.itno) ,* from fquotd where  fqno=@fqno ORDER BY recordno";

                da.Fill(FqD);
                dataGridViewT1.DataSource = FqD;
            }
        }

        void loadBom()
        {
            FqBom.Clear();

            if (btnState == "Append")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = "select * from fquotbom where 1=0 ";
                    da.Fill(FqBom);
                }
            }

            if (btnState == "Modify")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("fqno", this.tempNo);
                    cmd.CommandText = "select * from fquotbom where fqno=@fqno";
                    da.Fill(FqBom);
                }
            }

            if (btnState == "Duplicate")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("fqno", this.tempNo);
                    cmd.CommandText = "select * from fquotbom where fqno=@fqno";
                    da.Fill(FqBom);
                }
            }
        }

        void AddRow()
        {
            DataRow dr = FqD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 0;
            dr["price"] = 0;
            dr["prs"] = 1;
            dr["taxprice"] = 0;
            dr["mny"] = 0;
            dr["itpkgqty"] = 1;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["priceb"] = 0;
            dr["taxpriceb"] = 0;
            dr["mnyb"] = 0;
            dr["BomRec"] = GetBomRec();

            FqD.Rows.Add(dr);
            FqD.AcceptChanges();
        }

        void AddRow(int index)
        {
            DataRow dr = FqD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 0;
            dr["price"] = 0;
            dr["prs"] = 1;
            dr["taxprice"] = 0;
            dr["mny"] = 0;
            dr["itpkgqty"] = 1;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["priceb"] = 0;
            dr["taxpriceb"] = 0;
            dr["mnyb"] = 0;
            dr["BomRec"] = GetBomRec();

            FqD.Rows.InsertAt(dr, index);
            FqD.AcceptChanges();
        }

        decimal GetBomRec()
        {
            if (btnState == "Append")
            {
                return ++BomRec;
            }
            else
            {
                decimal d = 0;
                for (int i = 0; i < FqD.Rows.Count; i++)
                {
                    if (Convert.ToInt32(FqD.Rows[i]["BomRec"].ToString()) > d)
                        d = Convert.ToInt32(FqD.Rows[i]["BomRec"].ToString());
                }
                BomRec = ++d;
                return BomRec;
            }
        }

        void DeleteRow(int index)
        {
            string rec = dataGridViewT1["組件編號", index].Value.ToString();
            int rowcount = FqBom.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                if (FqBom.Rows[i]["BomRec"].ToString() == rec)
                    FqBom.Rows[i].Delete();
            }
            FqBom.AcceptChanges();
            FqD.Rows[index].Delete();
            FqD.AcceptChanges();
            SetAllMny();
        }

        bool SetFqNo()
        {
            string strFqNo = "";
            if (FqNo.Text != "")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                    cmd.CommandText = "select fqno from fquot where fqno=@fqno";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        MessageBox.Show("詢價單號重複", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FqNo.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                try
                {
                    string strDate = "";
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();

                        strDate = Date.ChangeDateForSN(FqDate.Text);

                        string sql = "select fqno from fquot where fqno like @fqdate + '%' order by fqno desc";
                        DataTable qunotable = new DataTable();
                        List<DataRow> listno = new List<DataRow>();

                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fqdate", strDate);
                        cmd.CommandText = sql;
                        SqlDataAdapter dd = new SqlDataAdapter(cmd);
                        string Countgano = "";
                        dd.Fill(qunotable);
                        if (qunotable.Rows.Count > 0)
                        {
                            listno = qunotable.AsEnumerable().ToList();
                            Countgano = qunotable.Rows[0]["fqno"].ToString();
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
                                strFqNo = strDate + Countgano;
                                if (listno.Find(r => r.Field<string>("FqNo") == strFqNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                            else
                            {
                                Countgano = C.ToString();
                                Countgano = Countgano.PadLeft(4, '0');
                                strFqNo = strDate + Countgano;
                                if (listno.Find(r => r.Field<string>("FqNo") == strFqNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                        } while (isRepeat);
                        FqNo.Text = strFqNo;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    FqNo.Text = "";
                    return false;
                }

            }
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            if (FaNo.Text.Trim() == "")
            {
                FaNo.Focus();
                MessageBox.Show("請先輸入廠商編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaName1.Text = FaPer1.Text = FaTel1.Text = EmNo.Text = EmName.Text = "";
                return;
            }
            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                AddRow();
                dataGridViewT1.CurrentCell = dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Cells["產品編號"];
                dataGridViewT1.CurrentRow.Selected = true;
            }
            dataGridViewT1.Focus();
        }

        private void gridInsert_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridAppend.Focus();
                if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    AddRow(index);
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells["產品編號"];
                    dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
            }
        }

        private void gridPicture_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1.SelectedRows.Count > 0)
                    pVar.PictureOpenForm(dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString());
            }
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].Value.ToString().Trim();
            for (int i = 0; i < FqBom.Rows.Count; i++)
            {
                if (FqBom.Rows[i]["BomRec"].ToString().Trim() == rec)
                    FqBom.Rows.RemoveAt(i--);
            }
            FqBom.AcceptChanges();
            FqD.Rows[dataGridViewT1.SelectedRows[0].Index].Delete();
            FqD.AcceptChanges();

            SetAllMny();
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
                frm.dr = FqD.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridBomD_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows[0].Cells["產品組成"].Value.ToString() == "單一商品")
            {
                MessageBox.Show("只有組合品與組裝品可以編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].Value.ToString();
            DataTable table = FqBom.Clone();

            for (int i = 0; i < FqBom.Rows.Count; i++)
            {
                if (FqBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                {
                    table.ImportRow(FqBom.Rows[i]);
                    FqBom.Rows.RemoveAt(i--);
                }
            }

            FqBom.AcceptChanges();
            table.AcceptChanges();

            using (var frm = new subMenuFm_2.FrmDraw_Bom())
            { 
                frm.table = table.Copy();
                frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                frm.BomRec = rec;
                frm.grid = dataGridViewT1;
                frm.上層Row = FqD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        if (frm.CallBack == "Bom")
                        {
                            FqBom.Merge(frm.table);
                            FqBom.AcceptChanges();
                            dataGridViewT1.Focus();
                            table.Clear();
                        }
                        break;
                    case DialogResult.Cancel:
                        FqBom.Merge(table);
                        FqBom.AcceptChanges();
                        dataGridViewT1.Focus();
                        table.Clear();
                        break;

                }
            }
        }

        private void gridStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                FrmSale_Stock frm = new FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
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
            var itno = FqD.Rows[index]["itno"].ToString().Trim();
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
            var itno = FqD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有廠商此產品交易 frm = new S2.Frm所有廠商此產品交易())
            {
                frm.fano = FaNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridFactBShop_Click(object sender, EventArgs e)
        {
            gridFactBShop.Focus();
            if (FaNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("廠商編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                return;
            }
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            var itno = (index == -1) ? "" : FqD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該廠商歷史交易 frm = new S2.Frm該廠商歷史交易())
            {
                frm.fano = FaNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void Text_Enter(object sender, EventArgs e)
        {
            //FqNo,FaNo,X3No
            TextBefore = (sender as TextBox).Text;
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToTxt(Common.load("Top", "fquot", "fqno"));
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var row = Common.load("Prior", "fquot", "fqno", FqNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CPrior", "fquot", "fqno", FqNo.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

            }
            WriteToTxt(row);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var row = Common.load("Next", "fquot", "fqno", FqNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "fquot", "fqno", FqNo.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

            }
            WriteToTxt(row);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToTxt(Common.load("Bottom", "fquot", "fqno", FqNo.Text.Trim()));


        }

        void WhenAppend()
        {
            if (Common.User_DateTime == 1)
                FqDate.Text = FqDateS.Text = Date.GetDateTime(1, false);
            else
                FqDate.Text = FqDateS.Text = Date.GetDateTime(2, false);

            Xa1No.Text = "TWD";
            Xa1Name.Text = "新臺幣";
            Xa1Par.Text = "1.0000";
            X3No.Text = "1";
            X3Name.Text = "外加稅";
            Rate.Text = (Common.Sys_Rate * 100).ToString("f0");
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            btnState = "Append";
            tempNo = FqNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            Xa1Par.ReadOnly = (Common.Series == "74" || Common.Series == "73");

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前進價.ReadOnly = true;
            this.序號.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            this.BomRec = 0;

            FqD.Clear();
            loadBom();

            T.Clear();
            DataRow tr = T.NewRow();
            T.Rows.Add(tr);
            T.AcceptChanges();
            memo1 = "";

            WhenAppend();
            FqDate.Focus();            
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnState = "Duplicate";
            tempNo = FqNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            Xa1Par.ReadOnly = (Common.Series == "74" || Common.Series == "73");
            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前進價.ReadOnly = true;
            this.序號.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.自定編號.ReadOnly = true;

            loadBom();

            FqNo.Text = "";
            FqDate.Text = Date.GetDateTime(Common.User_DateTime, false);
            FqDate.Focus();

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jFQuot.IsExistDocument<JBS.JS.FQuot>(FqNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jFQuot.IsModify<JBS.JS.FQuot>(FqNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jFQuot.upModify1<JBS.JS.FQuot>(FqNo.Text.Trim());//更新修改狀態1
                WriteToTxt(Common.load("Cancel", "fquot", "fqno", FqNo.Text.Trim()));
            }
            btnState = "Modify";
            tempNo = FqNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前進價.ReadOnly = true;
            this.序號.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            loadBom();
            FqNo.Focus();
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jFQuot.IsExistDocument<JBS.JS.FQuot>(FqNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jFQuot.IsModify<JBS.JS.FQuot>(FqNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();

                    tn = cn.BeginTransaction();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Transaction = tn;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                    cmd.CommandText = "delete from fquot where fqno=@fqno";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from fquotd where fqno=@fqno";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from fquotbom where fqno=@fqno";

                    FrmAffixFile.FileDelete_單據刪除(cmd, FqNo.Text.Trim(), "詢價單");

                    cmd.ExecuteNonQuery();

                    tn.Commit();
                    tn.Dispose();
                    cmd.Dispose();
                    btnNext_Click(null, null);
                }
                catch (Exception ex)
                {
                    tn.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (FrmFQuot_Print frm = new FrmFQuot_Print())
            {
                frm.PK = FqNo.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new S1.FrmFQuotBrowNew())
            {
                frm.TSeekNo = FqNo.Text.Trim();
                frm.ShowDialog();
                WriteToTxt(Common.load("Check", "fquot", "fqno", frm.TResult.Trim()));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            bool falg = false;
            if (FqD.AsEnumerable().Count(r => r["itno"].ToString().Trim().Length == 0) > 0)
            {
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    if (dataGridViewT1["產品編號", i].Value.ToString() == "")
                    {
                        if (dataGridViewT1["詢價數量", i].Value.ToString() == "0")
                        {
                            DeleteRow(i);
                            i--;
                        }
                        else
                            falg = true;
                    }
                    if (falg)
                    {
                        MessageBox.Show("產品編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridViewT1.Focus();
                        dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", i];
                        dataGridViewT1.CurrentRow.Selected = true;
                        return;
                    }
                }
                SetAllMny();
            }
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("詢價明細不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnState == "Append" || btnState == "Duplicate")
            {
                if (!SetFqNo()) return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Transaction = tn;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                        cmd.Parameters.AddWithValue("fqdate", Date.ToTWDate(FqDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("fqdate1", Date.ToUSDate(FqDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("fqdates", Date.ToTWDate(FqDateS.Text.Trim()));
                        cmd.Parameters.AddWithValue("fqdates1", Date.ToUSDate(FqDateS.Text.Trim()));
                        cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("faname1", FaName1.Text.Trim());
                        cmd.Parameters.AddWithValue("faname2", FaName2);
                        cmd.Parameters.AddWithValue("fatel1", FaTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("faper1", FaPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                        cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
                        cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
                        cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
                        cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                        cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
                        cmd.Parameters.AddWithValue("taxb", Math.Round(Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
                        cmd.Parameters.AddWithValue("totmnyb", Math.Round(TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("fqpayment", FqPayment.Text.Trim());
                        cmd.Parameters.AddWithValue("fqperiod", Fqperiod.Text.Trim());
                        cmd.Parameters.AddWithValue("fqmemo", FqMemo.Text.Trim());
                        cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("appscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("fqmemo1", memo1);
                        cmd.Parameters.AddWithValue("recordno", FqD.Rows.Count);
                        cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.GetUTF8(100));
   
                        cmd.CommandText = "insert into fquot (fqno,fqdate,fqdate1,fqdates,fqdates1,fano,faname1,faname2,"
                            + " fatel1,faper1,emno,emname,xa1no,xa1name,xa1par,taxmny,taxmnyb,x3no,rate,tax,taxb,"
                            + " totmny,totmnyb,fqpayment,fqperiod,fqmemo,appdate,edtdate,appscno,edtscno,fqmemo1,recordno,PhotoPath )"
                            + " values( "
                            + "@fqno,@fqdate,@fqdate1,@fqdates,@fqdates1,@fano,@faname1,@faname2,"
                            + " @fatel1,@faper1,@emno,@emname,@xa1no,@xa1name,@xa1par,@taxmny,@taxmnyb,@x3no,@rate,@tax,@taxb,"
                            + " @totmny,@totmnyb,@fqpayment,@fqperiod,@fqmemo,@appdate,@edtdate,@appscno,@edtscno,@fqmemo1,@recordno,@PhotoPath )";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < FqD.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                            cmd.Parameters.AddWithValue("fqdate", Date.ToTWDate(FqDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("fqdate1", Date.ToUSDate(FqDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("fqdates", Date.ToTWDate(FqDateS.Text.Trim()));
                            cmd.Parameters.AddWithValue("fqdates1", Date.ToUSDate(FqDateS.Text.Trim()));
                            cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                            cmd.Parameters.AddWithValue("itno", FqD.Rows[i]["itno"]);
                            cmd.Parameters.AddWithValue("itname", FqD.Rows[i]["itname"]);
                            cmd.Parameters.AddWithValue("ittrait", FqD.Rows[i]["ittrait"]);
                            cmd.Parameters.AddWithValue("itunit", FqD.Rows[i]["itunit"]);
                            cmd.Parameters.AddWithValue("itpkgqty", FqD.Rows[i]["itpkgqty"]);
                            cmd.Parameters.AddWithValue("qty", FqD.Rows[i]["qty"]);
                            cmd.Parameters.AddWithValue("price", FqD.Rows[i]["price"]);
                            cmd.Parameters.AddWithValue("priceb", FqD.Rows[i]["priceb"]);
                            cmd.Parameters.AddWithValue("prs", FqD.Rows[i]["prs"]);
                            cmd.Parameters.AddWithValue("rate", Rate.Text.Trim());
                            cmd.Parameters.AddWithValue("taxprice", FqD.Rows[i]["taxprice"]);
                            cmd.Parameters.AddWithValue("taxpriceb", FqD.Rows[i]["taxpriceb"]);
                            cmd.Parameters.AddWithValue("mny", FqD.Rows[i]["mny"]);
                            cmd.Parameters.AddWithValue("mnyb", FqD.Rows[i]["mnyb"]);
                            cmd.Parameters.AddWithValue("memo", FqD.Rows[i]["memo"]);
                            cmd.Parameters.AddWithValue("bomid", FqNo.Text.Trim() + FqD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", FqD.Rows[i]["BomRec"]);
                            cmd.Parameters.AddWithValue("recordno", (i + 1));
                            cmd.Parameters.AddWithValue("itdesp1", FqD.Rows[i]["itdesp1"]);
                            cmd.Parameters.AddWithValue("itdesp2", FqD.Rows[i]["itdesp2"]);
                            cmd.Parameters.AddWithValue("itdesp3", FqD.Rows[i]["itdesp3"]);
                            cmd.Parameters.AddWithValue("itdesp4", FqD.Rows[i]["itdesp4"]);
                            cmd.Parameters.AddWithValue("itdesp5", FqD.Rows[i]["itdesp5"]);
                            cmd.Parameters.AddWithValue("itdesp6", FqD.Rows[i]["itdesp6"]);
                            cmd.Parameters.AddWithValue("itdesp7", FqD.Rows[i]["itdesp7"]);
                            cmd.Parameters.AddWithValue("itdesp8", FqD.Rows[i]["itdesp8"]);
                            cmd.Parameters.AddWithValue("itdesp9", FqD.Rows[i]["itdesp9"]);
                            cmd.Parameters.AddWithValue("itdesp10", FqD.Rows[i]["itdesp10"]);
                            
                            cmd.CommandText = "insert into fquotd ("
                                + " fqno,fqdate,fqdate1,fqdates,fqdates1,fano,emno,xa1no,xa1par,itno,itname,ittrait,"
                                + " itunit,itpkgqty,qty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo,"
                                + " bomid,bomrec,recordno,"
                                + " itdesp1,itdesp2,itdesp3,itdesp4,itdesp5,"
                                + " itdesp6,itdesp7,itdesp8,itdesp9,itdesp10) "
                                + " values( "
                                + " @fqno,@fqdate,@fqdate1,@fqdates,@fqdates1,@fano,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,"
                                + " @itunit,@itpkgqty,@qty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo,"
                                + " @bomid,@bomrec,@recordno,"
                                + " @itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5,"
                                + " @itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10)";
                            cmd.ExecuteNonQuery();

                        }

                        for (int i = 0; i < FqBom.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fqno", FqNo.Text);
                            cmd.Parameters.AddWithValue("bomid", FqNo.Text.Trim() + FqBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", FqBom.Rows[i]["BomRec"].ToString());
                            cmd.Parameters.AddWithValue("itno", FqBom.Rows[i]["itno"].ToString());
                            cmd.Parameters.AddWithValue("itname", FqBom.Rows[i]["itname"].ToString());
                            cmd.Parameters.AddWithValue("itunit", FqBom.Rows[i]["itunit"].ToString());
                            cmd.Parameters.AddWithValue("itqty", FqBom.Rows[i]["itqty"].ToString());
                            cmd.Parameters.AddWithValue("itpareprs", FqBom.Rows[i]["itpareprs"].ToString());
                            cmd.Parameters.AddWithValue("itpkgqty", FqBom.Rows[i]["itpkgqty"].ToString());
                            cmd.Parameters.AddWithValue("itrec", (i + 1));
                            cmd.Parameters.AddWithValue("itprice", FqBom.Rows[i]["itprice"].ToString());
                            cmd.Parameters.AddWithValue("itprs", 1);
                            cmd.Parameters.AddWithValue("itmny", FqBom.Rows[i]["itmny"].ToString());
                            cmd.Parameters.AddWithValue("itnote", FqBom.Rows[i]["itnote"].ToString());
                            cmd.CommandText = "insert into fquotbom("
                                + " fqno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,itprice,"
                                + " itprs,itmny,itnote)"
                                + " values("
                                + " @fqno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,"
                                + " @itprs,@itmny,@itnote)";
                            cmd.ExecuteNonQuery();
                        }
                        FrmAffixFile.FileSave_單據存檔(DaFile, cmd, FqNo.Text.Trim(), "詢價單");
                        tn.Commit();
                        tn.Dispose();
                        cmd.Dispose();
                        tempNo = FqNo.Text.Trim();

                        if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            using (FrmFQuot_Print frm = new FrmFQuot_Print())
                            {
                                frm.PK = FqNo.Text.Trim();
                                frm.ShowDialog();
                            }
                        }
                        BomRec = 0;
                        btnAppend_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            if (btnState == "Modify")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Transaction = tn;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fqno", tempNo);
                        cmd.CommandText = "delete from fquot where fqno=@fqno";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                        cmd.Parameters.AddWithValue("fqdate", Date.ToTWDate(FqDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("fqdate1", Date.ToUSDate(FqDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("fqdates", Date.ToTWDate(FqDateS.Text.Trim()));
                        cmd.Parameters.AddWithValue("fqdates1", Date.ToUSDate(FqDateS.Text.Trim()));
                        cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("faname1", FaName1.Text.Trim());
                        cmd.Parameters.AddWithValue("faname2", FaName2);
                        cmd.Parameters.AddWithValue("fatel1", FaTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("faper1", FaPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                        cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
                        cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
                        cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
                        cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                        cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
                        cmd.Parameters.AddWithValue("taxb", Math.Round(Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
                        cmd.Parameters.AddWithValue("totmnyb", Math.Round(TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("fqpayment", FqPayment.Text.Trim());
                        cmd.Parameters.AddWithValue("fqperiod", Fqperiod.Text.Trim());
                        cmd.Parameters.AddWithValue("fqmemo", FqMemo.Text.Trim());
                        cmd.Parameters.AddWithValue("appdate", T.Rows[0]["AppDate"].ToString());
                        cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("appscno", T.Rows[0]["AppScNo"].ToString());
                        cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("fqmemo1", memo1);
                        cmd.Parameters.AddWithValue("recordno", FqD.Rows.Count);
                        cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.GetUTF8(100));
                        cmd.CommandText = "insert into fquot (fqno,fqdate,fqdate1,fqdates,fqdates1,fano,faname1,faname2,"
                            + " fatel1,faper1,emno,emname,xa1no,xa1name,xa1par,taxmny,taxmnyb,x3no,rate,tax,taxb,"
                            + " totmny,totmnyb,fqpayment,fqperiod,fqmemo,appdate,edtdate,appscno,edtscno,fqmemo1,recordno,PhotoPath )"
                            + " values( "
                            + " @fqno,@fqdate,@fqdate1,@fqdates,@fqdates1,@fano,@faname1,@faname2,"
                            + " @fatel1,@faper1,@emno,@emname,@xa1no,@xa1name,@xa1par,@taxmny,@taxmnyb,@x3no,@rate,@tax,@taxb,"
                            + " @totmny,@totmnyb,@fqpayment,@fqperiod,@fqmemo,@appdate,@edtdate,@appscno,@edtscno,@fqmemo1,@recordno,@PhotoPath )";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fqno", tempNo);
                        cmd.CommandText = "delete from fquotd where fqno=@fqno";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < FqD.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                            cmd.Parameters.AddWithValue("fqdate", Date.ToTWDate(FqDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("fqdate1", Date.ToUSDate(FqDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("fqdates", Date.ToTWDate(FqDateS.Text.Trim()));
                            cmd.Parameters.AddWithValue("fqdates1", Date.ToUSDate(FqDateS.Text.Trim()));
                            cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                            cmd.Parameters.AddWithValue("itno", FqD.Rows[i]["itno"]);
                            cmd.Parameters.AddWithValue("itname", FqD.Rows[i]["itname"]);
                            cmd.Parameters.AddWithValue("ittrait", FqD.Rows[i]["ittrait"]);
                            cmd.Parameters.AddWithValue("itunit", FqD.Rows[i]["itunit"]);
                            cmd.Parameters.AddWithValue("itpkgqty", FqD.Rows[i]["itpkgqty"]);
                            cmd.Parameters.AddWithValue("qty", FqD.Rows[i]["qty"]);
                            cmd.Parameters.AddWithValue("price", FqD.Rows[i]["price"]);
                            cmd.Parameters.AddWithValue("priceb", FqD.Rows[i]["priceb"]);
                            cmd.Parameters.AddWithValue("prs", FqD.Rows[i]["prs"]);
                            cmd.Parameters.AddWithValue("rate", Rate.Text.Trim());
                            cmd.Parameters.AddWithValue("taxprice", FqD.Rows[i]["taxprice"]);
                            cmd.Parameters.AddWithValue("taxpriceb", FqD.Rows[i]["taxpriceb"]);
                            cmd.Parameters.AddWithValue("mny", FqD.Rows[i]["mny"]);
                            cmd.Parameters.AddWithValue("mnyb", FqD.Rows[i]["mnyb"]);
                            cmd.Parameters.AddWithValue("memo", FqD.Rows[i]["memo"]);
                            cmd.Parameters.AddWithValue("bomid", FqNo.Text.Trim() + FqD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", FqD.Rows[i]["BomRec"]);
                            cmd.Parameters.AddWithValue("recordno", (i + 1));
                            cmd.Parameters.AddWithValue("itdesp1", FqD.Rows[i]["itdesp1"]);
                            cmd.Parameters.AddWithValue("itdesp2", FqD.Rows[i]["itdesp2"]);
                            cmd.Parameters.AddWithValue("itdesp3", FqD.Rows[i]["itdesp3"]);
                            cmd.Parameters.AddWithValue("itdesp4", FqD.Rows[i]["itdesp4"]);
                            cmd.Parameters.AddWithValue("itdesp5", FqD.Rows[i]["itdesp5"]);
                            cmd.Parameters.AddWithValue("itdesp6", FqD.Rows[i]["itdesp6"]);
                            cmd.Parameters.AddWithValue("itdesp7", FqD.Rows[i]["itdesp7"]);
                            cmd.Parameters.AddWithValue("itdesp8", FqD.Rows[i]["itdesp8"]);
                            cmd.Parameters.AddWithValue("itdesp9", FqD.Rows[i]["itdesp9"]);
                            cmd.Parameters.AddWithValue("itdesp10", FqD.Rows[i]["itdesp10"]);
                            cmd.CommandText = "insert into fquotd ("
                                + " fqno,fqdate,fqdate1,fqdates,fqdates1,fano,emno,xa1no,xa1par,itno,itname,ittrait,"
                                + " itunit,itpkgqty,qty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo,"
                                + " bomid,bomrec,recordno,"
                                + " itdesp1,itdesp2,itdesp3,itdesp4,itdesp5,"
                                + " itdesp6,itdesp7,itdesp8,itdesp9,itdesp10) "
                                + " values( "
                                + " @fqno,@fqdate,@fqdate1,@fqdates,@fqdates1,@fano,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,"
                                + " @itunit,@itpkgqty,@qty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo,"
                                + " @bomid,@bomrec,@recordno,"
                                + " @itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5,"
                                + " @itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10)";
                            cmd.ExecuteNonQuery();

                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fqno", tempNo);
                        cmd.CommandText = "delete from fquotbom where fqno=@fqno";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < FqBom.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fqno", FqNo.Text);
                            cmd.Parameters.AddWithValue("bomid", FqNo.Text.Trim() + FqBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", FqBom.Rows[i]["BomRec"].ToString());
                            cmd.Parameters.AddWithValue("itno", FqBom.Rows[i]["itno"].ToString());
                            cmd.Parameters.AddWithValue("itname", FqBom.Rows[i]["itname"].ToString());
                            cmd.Parameters.AddWithValue("itunit", FqBom.Rows[i]["itunit"].ToString());
                            cmd.Parameters.AddWithValue("itqty", FqBom.Rows[i]["itqty"].ToString());
                            cmd.Parameters.AddWithValue("itpareprs", FqBom.Rows[i]["itpareprs"].ToString());
                            cmd.Parameters.AddWithValue("itpkgqty", FqBom.Rows[i]["itpkgqty"].ToString());
                            cmd.Parameters.AddWithValue("itrec", (i + 1));
                            cmd.Parameters.AddWithValue("itprice", FqBom.Rows[i]["itprice"].ToString());
                            cmd.Parameters.AddWithValue("itprs", 1);
                            cmd.Parameters.AddWithValue("itmny", FqBom.Rows[i]["itmny"].ToString());
                            cmd.Parameters.AddWithValue("itnote", FqBom.Rows[i]["itnote"].ToString());
                            cmd.CommandText = "insert into fquotbom("
                                + " fqno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,itprice,"
                                + " itprs,itmny,itnote)"
                                + " values("
                                + " @fqno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,"
                                + " @itprs,@itmny,@itnote)";
                            cmd.ExecuteNonQuery();
                        }
                        tn.Commit();
                        tn.Dispose();
                        cmd.Dispose();
                        tempNo = FqNo.Text.Trim();
                        if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            using (FrmFQuot_Print frm = new FrmFQuot_Print())
                            {
                                frm.PK = FqNo.Text.Trim();
                                frm.ShowDialog();
                            }
                        }
                        jFQuot.upModify0<JBS.JS.FQuot>(FqNo.Text.Trim());//更新修改狀態0    
                        BomRec = 0;
                        btnAppend_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = string.Empty;
            WriteToTxt(Common.load("Cancel", "fquot", "fqno", tempNo));
            Common.SetTextState(FormState = FormEditState.None, ref list);
            dataGridViewT1.ReadOnly = true;
            btnAppend.Focus();
            jFQuot.upModify0<JBS.JS.FQuot>(FqNo.Text.Trim());//更新修改狀態0      
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }







        private void FqDate_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.ReadOnly || btnCancel.Focused) return;
            if (FqDate.Text == "")
            {
                MessageBox.Show("日期不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            if (!tb.IsDateTime())
            {
                if (tb.Text.Trim() == "") return;
                e.Cancel = true;
                MessageBox.Show("輸入日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tb.Name == "FqDate")
            {
                if (FqDateS.Text != "")
                {
                    if (string.CompareOrdinal(FqDate.Text.ToString().Trim(), FqDateS.Text.ToString().Trim()) > 0)
                        FqDateS.Text = FqDate.Text;
                }
            }

        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            jFQuot.Open<JBS.JS.Fact>(sender, row =>
            {
                FaNo.Text = row["fano"].ToString().Trim();
                FaName1.Text = row["faname1"].ToString();
                FaName2 = row["faname2"].ToString();
                FaPer1.Text = row["faper1"].ToString().GetUTF8(20);
                FaTel1.Text = row["fatel1"].ToString();
                X3No.Text = row["fax3no"].ToString();
                EmNo.Text = row["faemno1"].ToString();
                Xa1No.Text = row["faxa1no"].ToString();

                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);
                FaNo.SelectAll();

                for (int i = 0; i < FqD.Rows.Count; i++)
                {
                    GetSystemPrice(FqD.Rows[i], i);
                    SetRow_TaxPrice(FqD.Rows[i]);
                    SetRow_Mny(FqD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["fano"].ToString().Trim();
            });

        }

        private void FaNo_Validating(object sender, CancelEventArgs e)
        {
            if (FaNo.ReadOnly || btnCancel.Focused) return;
            if (FaNo.Text.Trim() == "")
            {
                FaNo.Focus();
                MessageBox.Show("請先輸入廠商編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaName1.Text = FaPer1.Text = FaTel1.Text = EmNo.Text = EmName.Text = "";
                e.Cancel = true;
                return;
            }

            jFQuot.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                if (FaNo.Text.Trim() == TextBefore)
                    return;

                FaNo.Text = row["fano"].ToString().Trim();
                FaName1.Text = row["faname1"].ToString();
                FaPer1.Text = row["faper1"].ToString().GetUTF8(10);
                FaTel1.Text = row["fatel1"].ToString();
                FaName2 = row["faname2"].ToString();
                X3No.Text = row["fax3no"].ToString();
                EmNo.Text = row["faemno1"].ToString();
                Xa1No.Text = row["faxa1no"].ToString();
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);

                for (int i = 0; i < FqD.Rows.Count; i++)
                {
                    GetSystemPrice(FqD.Rows[i], i);
                    SetRow_TaxPrice(FqD.Rows[i]);
                    SetRow_Mny(FqD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["fano"].ToString().Trim();
            });

        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jFQuot.Open<JBS.JS.Empl>(sender, row =>
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

            jFQuot.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        private void FqPayment_DoubleClick(object sender, EventArgs e)
        { 
            using (var frm = new SOther.FrmPayNoteBrow())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        FqPayment.Text = frm.TResult;
                        break;
                }
            }
        }

        private void Fqperiod_DoubleClick(object sender, EventArgs e)
        { 
            using (var frm = new SOther.FrmPerNote())
            { 
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        Fqperiod.Text = frm.TResult;
                        break; 
                }
            }
        }

        private void FqMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(FqMemo, FqMemo.MaxLength);
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jFQuot.Open<JBS.JS.XX03>(sender, row =>
            {
                X3No.Text = row["x3no"].ToString().Trim();
                X3Name.Text = row["x3name"].ToString().Trim();
                Rate.Text = (row["x3rate"].ToDecimal() * 100).ToString("f0");

                for (int i = 0; i < FqD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(FqD.Rows[i]);
                    SetRow_Mny(FqD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["x3no"].ToString().Trim();
            });
        }

        private void X3No_Validating(object sender, CancelEventArgs e)
        {
            if (X3No.ReadOnly) return;
            if (btnCancel.Focused) return;

            TextBox tx = sender as TextBox;
            if (tx.Text.Trim() == "" || tx.Text.Trim() == "0")
            {
                e.Cancel = true;
                tx.SelectAll();
                MessageBox.Show("稅別編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jFQuot.ValidateOpen<JBS.JS.XX03>(sender, e, row =>
            {
                if (X3No.Text.Trim() == TextBefore)
                    return;

                X3No.Text = row["x3no"].ToString().Trim();
                X3Name.Text = row["x3name"].ToString().Trim();
                Rate.Text = (row["x3rate"].ToDecimal() * 100).ToString("f0");

                for (int i = 0; i < FqD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(FqD.Rows[i]);
                    SetRow_Mny(FqD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["x3no"].ToString().Trim();
            });
        }

        private void Tax_Validating(object sender, CancelEventArgs e)
        {
            if (Tax.ReadOnly) return;

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


        }

        private void FqNo_DoubleClick(object sender, EventArgs e)
        {
            if (FqNo.ReadOnly)
                return;

            using (var frm = new FrmFQuot_Print_FqNo())
            { 
                frm.TSeekNo = FqNo.Text.Trim();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    FqNo.Text = frm.TResult.Trim();
                }
            }
        }

        private void FqNo_Validating(object sender, CancelEventArgs e)
        {
            if (FqNo.ReadOnly || btnCancel.Focused) return;

            if (FqNo.Text.Length > 0 && FqNo.Text.Trim() == "")
            {
                e.Cancel = true;
                FqNo.Text = "";
                FqNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnState == "Append")
            {
                if (jFQuot.IsExistDocument<JBS.JS.FQuot>(FqNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnState == "Duplicate")
            {
                if (jFQuot.IsExistDocument<JBS.JS.FQuot>(FqNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnState == "Modify")
            {
                var dr = Common.load("Check", "fquot", "fqno", FqNo.Text.Trim());
                if (dr != null)
                {
                    if (TextBefore != FqNo.Text.Trim())
                    {
                        WriteToTxt(dr);
                        loadBom();
                    }
                }
                else
                {
                    e.Cancel = true;
                    FqNo.SelectAll();

                    using (var frm = new FrmFQuot_Print_FqNo())
                    { 
                        frm.TSeekNo = FqNo.Text.Trim();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            FqNo.Text = frm.TResult.Trim();
                            WriteToTxt(Common.load("Check", "fquot", "fqno", FqNo.Text.Trim()));
                            loadBom();
                        }
                    }
                }
            }
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (FaNo.Text != "")
                if (dataGridViewT1.Rows.Count == 0)
                    if (!dataGridViewT1.ReadOnly) gridAppend_Click(null, null);
        }

        private void dataGridViewT1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewT1["序號", e.RowIndex].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.ReadOnly)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                TextBefore = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jFQuot.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                jFQuot.DataGridViewOpen<JBS.JS.Item>(sender, e, FqD, row => FillItem(row, e.RowIndex));
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.單位))
            {
                var itno = dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var row = Common.load("Check", "item", "itno", itno);
                if (row != null)
                {
                    if (row != null && unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        FqD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            FqD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            FqD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = unit;
                    FqD.Rows[e.RowIndex]["itunit"] = unit;

                    GetSystemPrice(FqD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(FqD.Rows[e.RowIndex]);
                    SetRow_Mny(FqD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.說明))
            {
                using (var frm = new FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                        FqD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly || btnCancel.Focused) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.詢價數量))
            {
                FqD.Rows[e.RowIndex]["qty"] = dataGridViewT1["詢價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                SetRow_Mny(FqD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                string itnonow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
                if (itnonow == "")
                {
                    FqD.Rows[e.RowIndex]["itno"] = "";
                    FqD.Rows[e.RowIndex]["itname"] = "";
                    FqD.Rows[e.RowIndex]["qty"] = 0;
                    FqD.Rows[e.RowIndex]["itunit"] = "";
                    FqD.Rows[e.RowIndex]["price"] = 0;
                    FqD.Rows[e.RowIndex]["prs"] = 1;
                    FqD.Rows[e.RowIndex]["taxprice"] = 0;
                    FqD.Rows[e.RowIndex]["mny"] = 0;
                    FqD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    FqD.Rows[e.RowIndex]["產品組成"] = "";
                    FqD.Rows[e.RowIndex]["memo"] = "";
                    FqD.Rows[e.RowIndex]["priceb"] = 0;
                    FqD.Rows[e.RowIndex]["taxpriceb"] = 0;
                    FqD.Rows[e.RowIndex]["mnyb"] = 0;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();

                    var rec = FqD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jFQuot.RemoveBom(rec, ref FqBom);
                    return;
                }
                if (ItNoBegin == itnonow) return;
                if (itnonow == UdfNoBegin)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    FqD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jFQuot.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, FqD, row => FillItem(row, e.RowIndex));

            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.進價))
            {
                FqD.Rows[e.RowIndex]["price"] = dataGridViewT1["進價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MF);
                SetRow_TaxPrice(FqD.Rows[e.RowIndex]);
                SetRow_Mny(FqD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.稅前金額))
            {
                decimal beforeMny = FqD.Rows[e.RowIndex]["mny"].ToDecimal("f" + Common.TPS);
                decimal nowMny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.TPS);
                if (beforeMny == nowMny) return;
                decimal price = 0;
                decimal qty = dataGridViewT1["詢價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                decimal taxprice = dataGridViewT1["稅前進價", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f6");
                decimal mny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.TPF);
                decimal prs = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");
                qty = (qty == 0) ? 1 : qty;

                FqD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / qty) / prs).ToDecimal("f" + Common.MF);
                        FqD.Rows[e.RowIndex]["Price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / qty) / prs).ToDecimal("f" + Common.MF);
                        FqD.Rows[e.RowIndex]["Price"] = price;
                        break;
                }
                SetRow_TaxPrice(FqD.Rows[e.RowIndex]);

                taxprice = FqD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                FqD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                FqD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                FqD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.折數))
            {
                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                FqD.Rows[e.RowIndex]["prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");
                SetRow_TaxPrice(FqD.Rows[e.RowIndex]);
                SetRow_Mny(FqD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.單位))
            {

                string itno = FqD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (unit == TextBefore)
                    return;

                jFQuot.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        FqD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        FqD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                FqD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                GetSystemPrice(FqD.Rows[e.RowIndex], e.RowIndex);
                SetRow_TaxPrice(FqD.Rows[e.RowIndex]);
                SetRow_Mny(FqD.Rows[e.RowIndex]);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
        }

        private void FillItem(SqlDataReader row, int index)
        {
            this.ItNoBegin = row["itno"].ToString();

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = row["itno"].ToString();

            FqD.Rows[index]["itno"] = row["itno"].ToString();
            FqD.Rows[index]["ItNoUdf"] = row["ItNoUdf"].ToString();
            FqD.Rows[index]["itname"] = row["itname"].ToString();
            FqD.Rows[index]["ittrait"] = row["ittrait"].ToString();
            if (row["ittrait"].ToString().Trim() == "1")
                FqD.Rows[index]["產品組成"] = "組合品";
            else if (row["ittrait"].ToString().Trim() == "2")
                FqD.Rows[index]["產品組成"] = "組裝品";
            else
                FqD.Rows[index]["產品組成"] = "單一商品";

            if (row["itbuyunit"].ToString().Trim() == "1")
            {
                FqD.Rows[index]["itunit"] = row["itunitp"].ToString();

                var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0)
                    itpkgqty = 1;
                FqD.Rows[index]["itpkgqty"] = itpkgqty;
            }
            else
            {
                FqD.Rows[index]["itunit"] = row["itunit"].ToString();
                FqD.Rows[index]["itpkgqty"] = 1;
            }
            GetSystemPrice(FqD.Rows[index], index);
            SetRow_TaxPrice(FqD.Rows[index]);
            SetRow_Mny(FqD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();
            for (int i = 1; i <= 10; i++)
            {
                FqD.Rows[index]["itdesp" + i] = row["itdesp" + i].ToString();
            }
            var rec = FqD.Rows[index]["BomRec"].ToString().Trim();
            jFQuot.RemoveBom(rec, ref FqBom);

            jFQuot.GetItemBom(row["itno"].ToString().Trim(), rec, ref FqBom);
        }

        //系統進價取用方式
        void GetSystemPrice(DataRow row, int index)
        {
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
        //帳款計算
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
            var qty = row["qty"].ToDecimal("f" + Common.Q);
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
            var sum = FqD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f"+Common.TPF)).ToDecimal("f" + Common.MFT);

            if (X3No.Text.ToInteger() == 1)
            {
                tax = (sum * Common.Sys_Rate).ToDecimal("f" + Common.TF);
                TaxMny.Text = sum.ToString("f" + Common.MFT);
                TaxMnyB.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = tax.ToString("f" + Common.TF);
                TotMny.Text = (sum + tax).ToString("f" + Common.MFT);
            }
            else if (X3No.Text.ToInteger() == 2)
            {
                var totmny = FqD.AsEnumerable().Sum(r => r["qty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MF)).ToDecimal("f" + Common.MFT);
                tax = (totmny / (1 + Common.Sys_Rate)) * Common.Sys_Rate;

                TotMny.Text = totmny.ToString("f" + Common.MFT);
                tax = tax.ToDecimal("f" + Common.TF);
                Tax.Text = tax.ToString();
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MFT);
                TaxMnyB.Text = (TaxMny.Text.ToDecimal() * par).ToString("f" + Common.M);
            }
            else if (X3No.Text.ToInteger() == 3 || X3No.Text.ToInteger() == 4)
            {
                TaxMny.Text = sum.ToString("f" + Common.MFT);
                TaxMnyB.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = tax.ToString("f" + Common.TF);
                TotMny.Text = sum.ToString("f" + Common.MFT);
            }
        }

        ToolTip tip = new ToolTip();
        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "說明")
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
            if (FqNo.Text.Trim() == "") return;
            FrmSale_AppScNo frm = new FrmSale_AppScNo();
            //新增人員
            frm.AName = T.Rows[0]["AppScNo"].ToString();//製單人員
            frm.ATime = T.Rows[0]["AppDate"].ToString();
            //修改人員
            frm.EName = T.Rows[0]["EdtScNo"].ToString();
            frm.ETime = T.Rows[0]["EdtDate"].ToString();
            frm.ShowDialog();
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = FaNo.ReadOnly ? false : true;
                frm.memo1 = memo1;

                if (frm.ShowDialog() == DialogResult.OK) memo1 = frm.memo1;
            }
        }

        private void Xa1Par_Validating(object sender, CancelEventArgs e)
        {
            if (Xa1Par.ReadOnly || btnCancel.Focused) return;
            if (Xa1Par.Text.ToDecimal() == 0)
            {
                MessageBox.Show("匯率不可為零", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            if (dataGridViewT1.Rows.Count > 0)
            {
                //離開匯率設定，重新計算本幣金額
                for (int i = 0; i < FqD.Rows.Count; i++)
                {
                    SetRow_Mny(FqD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();
            }
            dataGridViewT1_Click(null, null);
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            gridAppend.Enabled = gridDelete.Enabled = gridPicture.Enabled = gridInsert.Enabled = gridItDesp.Enabled =gridBomD.Enabled  = !btnAppend.Enabled;
            gridStock.Enabled = gridTran.Enabled = gridAllTrans.Enabled = gridFactBShop.Enabled = true;
        }

        //選擇檔頭檔案路徑 13.7C
        private void ShowORModify_PhotoPath_Click(object sender, EventArgs e)
        {
            using (FrmAffixFile frm = new FrmAffixFile())
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {

                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Datano", FqNo.Text.Trim());
                    cmd.CommandText = "select ROW_NUMBER() OVER(ORDER BY id) AS 序號 , * from AffixFile where DaType ='詢價單' and Datano=@Datano";
                    DaFile.Clear();
                    da.Fill(DaFile);
                    frm.DtFile = DaFile;
                    frm.CMD = cmd;
                    frm.Datano = FqNo.Text.Trim();
                    frm.DaType = "詢價單";
                    if (this.FormState == FormEditState.Append || this.FormState == FormEditState.Duplicate)
                    {
                        frm.編輯狀態 = "新增";
                    }
                    else
                    {
                        frm.編輯狀態 = "修改";
                    } 
                    frm.ShowDialog();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.ToString() == "F9, Shift")
            {
                if (gridTran.Enabled) gridTran.PerformClick();
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else if (keyData.ToString() == "F10, Shift")
            {
                if (gridAllTrans.Enabled) gridAllTrans.PerformClick();
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else if (keyData.ToString() == "F4, Shift")
            {
                if (gridFactBShop.Enabled) gridFactBShop.PerformClick();
                return base.ProcessCmdKey(ref msg, keyData);
            }

            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    if (btnAppend.Enabled)
                    {
                        btnAppend.Focus();
                        btnAppend.PerformClick();
                    }
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    if (btnModify.Enabled)
                    {
                        btnModify.Focus();
                        btnModify.PerformClick();
                    }
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (btnDelete.Enabled) btnDelete.PerformClick();
                    break;
                case Keys.D4:
                    if (btnBrow.Enabled) btnBrow.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    if (btnExit.Enabled) btnExit.PerformClick();
                    break;
                case Keys.Home:
                    if (btnTop.Enabled) btnTop.PerformClick();
                    break;
                case Keys.PageUp:
                    if (btnPrior.Enabled) btnPrior.PerformClick();
                    break;
                case Keys.PageDown:
                    if (btnNext.Enabled) btnNext.PerformClick();
                    break;
                case Keys.End:
                    if (btnBottom.Enabled) btnBottom.PerformClick();
                    break;
                case Keys.F9:
                    if (btnSave.Enabled)
                    {
                        btnSave.Focus();
                        btnSave.PerformClick();
                    }
                    break;
                case Keys.F4:
                    if (btnCancel.Enabled)
                    {
                        btnCancel.Focus();
                        btnCancel.PerformClick();
                    }
                    break;
                case Keys.F2:
                    if (gridAppend.Enabled) gridAppend.PerformClick();
                    break;
                case Keys.F5:
                    if (gridInsert.Enabled) gridInsert.PerformClick();
                    break;
                case Keys.F6:
                    if (gridItDesp.Enabled) gridItDesp.PerformClick();
                    break;
                case Keys.F7:
                    if (gridBomD.Enabled) gridBomD.PerformClick();
                    break;
                case Keys.F8:
                    if (gridStock.Enabled) gridStock.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }












    }
}
