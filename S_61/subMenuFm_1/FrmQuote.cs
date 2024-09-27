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
using S_61.SOther;
using System.Diagnostics;
using System.IO;

namespace S_61.subMenuFm_1
{
    public partial class FrmQuote : Formbase
    {
        JBS.JS.Quote jQuote;

        DataTable QuD = new DataTable();
        DataTable QuBom = new DataTable();
        DataTable dtstandard = new DataTable();
        List<TextBoxbase> list;
        DataTable DaFile = new DataTable(); //檔案附件
        DataTable T = new DataTable();//製單人員
        SqlTransaction tn;

        string tempNo = "";
        string btnState = "";
        string ItNoBegin = "";
        string UdfNoBegin = "";
        decimal BomRec = 0; 
        string cuname2 = "";
        string trname = "";
        string TextBefore = "";
        string memo1 = "";//詳細備註
        decimal Disc = 1; //折數
        string PhotoPath = "";  // 13.7c

        public FrmQuote()
        {
            InitializeComponent();
            this.jQuote = new JBS.JS.Quote();
            this.list = this.getEnumMember();//TEXBOX
            this.dataGridViewT1.tableName = "quoted";

            pVar.SetMemoUdf(this.說明);

            this.計價數量.Set庫存數量小數();
            this.報價數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();

            this.售價.Set銷貨單價小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前售價.FirstNum = 9;
            this.稅前售價.LastNum = 6;
            this.稅前售價.DefaultCellStyle.Format = "f6";
            this.稅前金額.Set銷項金額小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.FirstNum = 9;
            this.本幣稅前單價.LastNum = 6;
            this.本幣稅前單價.DefaultCellStyle.Format = "f6";



            if (Common.Sys_DBqty == 1)
            {
                this.計價數量.Visible = false;
                this.計位.Visible = false;
            }

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

            QuDate.SetDateLength();
            QuDateS.SetDateLength();

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
            this.本幣稅前單價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        private void FrmQuote_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlDataAdapter da = new SqlDataAdapter("select Top(1)* from [quote] where 1=0", cn))
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
                    end,ItNoUdf = '',*
                    from quoted where 1=0 ";
                da.Fill(QuD);
                dataGridViewT1.DataSource = QuD;
            }
            QuD.Clear();

            WriteToTxt(Common.load("Bottom", "Quote", "Quno"));

        }

        private void FrmQuote_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
            gridItBuyPrice.Text = "進價查詢";
            gridItemQuote.Text = "該客戶歷史報價";
            gridCustSale.Text = "該客戶歷史交易";
            gridAllTransTrans.Text = "所有客戶所有產品";
            gridCost.Text = "成本與毛利分析";
        }

        void WriteToTxt(DataRow row)
        {
            T.Clear();
            QuD.Clear();
            tempNo = memo1 = "";

            if (row == null)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);

                QuD.Clear();
                QuBom.Clear();
            }
            else
            {
                memo1 = row["qumemo1"].ToString();
                T.ImportRow(row);
                T.AcceptChanges();

                QuNo.Text = row["quno"].ToString().Trim();
                tempNo = QuNo.Text.Trim();

                if (Common.User_DateTime == 1)
                {
                    QuDate.Text = row["qudate"].ToString().Trim();
                    QuDateS.Text = row["qudates"].ToString().Trim();
                }
                else
                {
                    QuDate.Text = row["qudate1"].ToString().Trim();
                    QuDateS.Text = row["qudates1"].ToString().Trim();
                }

                EmNo.Text = row["emno"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
                CuNo.Text = row["cuno"].ToString();
                CuName1.Text = row["CuName1"].ToString();
                CuPer1.Text = row["CuPer1"].ToString();
                CuTel1.Text = row["CuTel1"].ToString();
                Xa1No.Text = row["Xa1No"].ToString();
                Xa1Name.Text = row["Xa1Name"].ToString();
                Xa1Par.Text = row["Xa1Par"].ToString();
                TaxMny.Text = row["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                TaxMnyB.Text = row["TaxMnyB"].ToDecimal().ToString("f" + Common.M);
                X3No.Text = row["X3No"].ToString();

                Rate.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");
                QuPayment.Text = row["QuPayment"].ToString();
                Quperiod.Text = row["Quperiod"].ToString();
                QuMemo.Text = row["QuMemo"].ToString();
                Tax.Text = row["Tax"].ToDecimal().ToString("f" + Common.TS);
                TotMny.Text = row["TotMny"].ToDecimal().ToString("f" + Common.MST);
                PhotoPath = row["PhotoPath"].ToString();//13.7c
                IsCheck.Checked = row["ischeck"].ToString() == "0" ? false : true;
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
                TrNo.Text = row["TrNo"].ToString();

                loadD();
            }
        }

        void loadD()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                cmd.CommandText = @" 
                    select 產品組成=
                    case
                    when ittrait=1 then '組合品'
                    when ittrait=2 then '組裝品'
                    when ittrait=3 then '單一商品'
                    end,ItNoUdf= (select top 1 itnoudf from item where item.itno = quoted.itno),*
                    from quoted where quno=@quno ORDER BY recordno";

                dd.Fill(QuD);
                dataGridViewT1.DataSource = QuD;
            }
        }

        void loadBom()
        {
            QuBom.Clear();

            if (btnState == "Append")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = "select * from quotebom where 1=0 ";

                    da.Fill(QuBom);
                }
            }

            if (btnState == "Modify")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("quno", this.tempNo);
                    cmd.CommandText = "select * from quotebom where quno=@quno";
                    da.Fill(QuBom);
                }
            }

            if (btnState == "Duplicate")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("quno", this.tempNo);
                    cmd.CommandText = "select * from quotebom where quno=@quno";
                    da.Fill(QuBom);
                }
            }
        }

        void AddRow()
        {
            DataRow dr = QuD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["pqty"] = 0;
            dr["punit"] = "";
            dr["qty"] = 0;
            dr["itunit"] = "";
            dr["price"] = 0;
            dr["mny"] = 0;
            dr["itpkgqty"] = 1;
            dr["priceb"] = 0;
            dr["mnyb"] = 0;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["BomRec"] = GetBomRec();
            if (Common.Sys_SalePrice == 2)
                dr["Prs"] = Disc;
            else dr["Prs"] = "1.00";

            QuD.Rows.Add(dr);
            QuD.AcceptChanges();

        }

        void AddRow(int index)
        {
            var dr = QuD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["pqty"] = 0;
            dr["punit"] = "";
            dr["qty"] = 0;
            dr["itunit"] = "";
            dr["price"] = 0;
            dr["mny"] = 0;
            dr["itpkgqty"] = 1;
            dr["priceb"] = 0;
            dr["mnyb"] = 0;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["BomRec"] = GetBomRec();
            if (Common.Sys_SalePrice == 2)
                dr["Prs"] = Disc;
            else dr["Prs"] = "1.00";

            QuD.Rows.InsertAt(dr, index);
            QuD.AcceptChanges();

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
                for (int i = 0; i < QuD.Rows.Count; i++)
                {
                    if (Convert.ToInt32(QuD.Rows[i]["BomRec"].ToString()) > d)
                        d = Convert.ToInt32(QuD.Rows[i]["BomRec"].ToString());
                }
                BomRec = ++d;
                return BomRec;
            }
        }

        void DeleteRow(int index)
        {
            string rec = dataGridViewT1["組件編號", index].Value.ToString();
            int rowcount = QuBom.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                if (QuBom.Rows[i]["BomRec"].ToString() == rec)
                    QuBom.Rows[i].Delete();
            }
            QuBom.AcceptChanges();
            QuD.Rows[index].Delete();
            QuD.AcceptChanges();
            SetAllMny();
        }

        bool SetQuNo()
        {
            string strQuNo = "";
            if (QuNo.Text != "")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                    cmd.CommandText = "select quno from Quote where quno=@quno";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        MessageBox.Show("報價單號重複", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        QuNo.Focus();
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
                        strDate = Date.ChangeDateForSN(QuDate.Text);
                        string sql = "select quno from quote where quno like @date + '%' order by quno desc";
                        DataTable qunotable = new DataTable();
                        List<DataRow> listno = new List<DataRow>();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("date", strDate);
                        cmd.CommandText = sql;
                        SqlDataAdapter dd = new SqlDataAdapter(cmd);
                        string Countgano = "";
                        dd.Fill(qunotable);
                        if (qunotable.Rows.Count > 0)
                        {
                            listno = qunotable.AsEnumerable().ToList();
                            Countgano = qunotable.Rows[0]["quno"].ToString();
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
                                strQuNo = strDate + Countgano;
                                if (listno.Find(r => r.Field<string>("QuNo") == strQuNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                            else
                            {
                                Countgano = C.ToString();
                                Countgano = Countgano.PadLeft(4, '0');
                                strQuNo = strDate + Countgano;
                                if (listno.Find(r => r.Field<string>("QuNo") == strQuNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                        } while (isRepeat);
                        QuNo.Text = strQuNo;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    QuNo.Text = "";
                    return false;
                }

            }
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            if (CuNo.Text.Trim() == "")
            {
                CuNo.Focus();
                MessageBox.Show("請先輸入客戶編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuName1.Text = CuPer1.Text = CuTel1.Text = EmNo.Text = EmName.Text = "";
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

        private void gridStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                FrmSale_Stock frm = new FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
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
                frm.dr = QuD.Rows[index];
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
            DataTable table = QuBom.Clone();

            for (int i = 0; i < QuBom.Rows.Count; i++)
            {
                if (QuBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                {
                    table.ImportRow(QuBom.Rows[i]);
                    QuBom.Rows.RemoveAt(i--);
                }
            }

            table.AcceptChanges();
            QuBom.AcceptChanges();

            using (var frm = new subMenuFm_2.FrmAdjust_Bom())
            { 
                frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                frm.table = table.Copy();
                frm.pkey = rec;
                frm.grid = dataGridViewT1;
                frm.FromQuote = true;
                frm.上層Row = QuD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        if (frm.CallBack.ToString() == "Money")
                        {

                            QuD.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MS);
                            dataGridViewT1.InvalidateRow(dataGridViewT1.SelectedRows[0].Index);
                            dataGridViewT1.Focus();
                            QuBom.Merge(frm.table);
                            QuBom.AcceptChanges();
                            SetRow_TaxPrice(QuD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                            SetRow_Mny(QuD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                            SetAllMny();
                        }
                        else
                        {
                            QuBom.Merge(frm.table);
                            QuBom.AcceptChanges();
                        }
                        break;
                    case DialogResult.Cancel:
                        QuBom.Merge(table);
                        QuBom.AcceptChanges();
                        break;
                }
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
            var itno = QuD.Rows[index]["itno"].ToString().Trim();
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
            var itno = QuD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有客戶此產品交易 frm = new S2.Frm所有客戶此產品交易())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].Value.ToString().Trim();
            for (int i = 0; i < QuBom.Rows.Count; i++)
            {
                if (QuBom.Rows[i]["BomRec"].ToString().Trim() == rec)
                    QuBom.Rows.RemoveAt(i--);
            }
            QuBom.AcceptChanges();
            QuD.Rows[dataGridViewT1.SelectedRows[0].Index].Delete();
            QuD.AcceptChanges();

            SetAllMny();
        }

        private void gridItBuyPrice_Click(object sender, EventArgs e)
        {
            gridItBuyPrice.Focus();

            if (jQuote.EnableBShopPrice() == false)
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
            var itno = QuD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridCustSale_Click(object sender, EventArgs e)
        {
            gridCustSale.Focus();
            if (CuNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("客戶編號不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuNo.Focus();
                return;
            }
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            var itno = (index == -1) ? "" : QuD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶歷史交易 frm = new S2.Frm該客戶歷史交易())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridItemQuote_Click(object sender, EventArgs e)
        {
            gridItemQuote.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = QuD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶歷史報價 frm = new S2.Frm該客戶歷史報價())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridAllTransTrans_Click(object sender, EventArgs e)
        {
            gridAllTransTrans.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            var itno = QuD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有客戶此產品交易 frm = new S2.Frm所有客戶此產品交易())
            {
                frm.cuno = CuNo.Text.Trim();
                frm.itno = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridCost_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (QuD.AsEnumerable().Any(r => r["itno"].ToString().Trim() == ""))
            {
                MessageBox.Show("明細尚有產品編號未輸入，請登打完畢後再進行查詢");
                return;
            }

            if (btnAppend.Enabled)
            {
                QuBom.Clear();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@quno", QuNo.Text.Trim());
                    cmd.CommandText = "select * from quoteBom where quno=@quno COLLATE Chinese_Taiwan_Stroke_BIN";

                    da.Fill(QuBom);
                }
            }

            gridCost.Focus();
            if (dataGridViewT1.SelectedRows.Count > 0)
            {
                using (S2.Frm成本與毛利分析 frm = new S2.Frm成本與毛利分析(QuDate.Text))
                {
                    frm.dtD = QuD.Copy();
                    frm.bom = QuBom.Copy();
                    frm.date = Date.ToTWDate(QuDate.Text);
                    frm.ShowDialog();
                }
            }
            dataGridViewT1.Focus();
        }

        private void Text_Enter(object sender, EventArgs e)
        {
            //QuNo,CuNo,X3No
            TextBefore = (sender as TextBox).Text;
        }




        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToTxt(Common.load("Top", "quote", "quno"));

        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var row = Common.load("Prior", "quote", "quno", QuNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CPrior", "quote", "quno", QuNo.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

            }
            WriteToTxt(row);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var row = Common.load("Next", "quote", "quno", QuNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "quote", "quno", QuNo.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

            }
            WriteToTxt(row);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToTxt(Common.load("Bottom", "quote", "quno", QuNo.Text.Trim()));
        }


        void WhenAppend()
        {
            if (Common.User_DateTime == 1)
                QuDate.Text = QuDateS.Text = Date.GetDateTime(1, false);
            else
                QuDate.Text = QuDateS.Text = Date.GetDateTime(2, false);

            IsCheck.Enabled = Common.isCheck == "1";
            IsCheck.Checked = false;


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
            tempNo = QuNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            Xa1Par.ReadOnly = (Common.Series == "74" || Common.Series == "73");

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前售價.ReadOnly = true;
            this.序號.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.型號.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            this.BomRec = 0;

            QuD.Clear();
            loadBom();

            T.Clear();
            DataRow tr = T.NewRow();
            T.Rows.Add(tr);
            T.AcceptChanges();
            memo1 = "";

            WhenAppend();
            QuDate.Focus();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnState = "Duplicate";
            tempNo = QuNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            Xa1Par.ReadOnly = (Common.Series == "74" || Common.Series == "73");

            if (Common.isCheck == "2")
            {
                IsCheck.Enabled = false;
                IsCheck.Checked = false;
            }
            else
            {
                IsCheck.Enabled = true;
            }

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前售價.ReadOnly = true;
            this.序號.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.型號.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            loadBom();

            QuNo.Text = "";
            QuDateS.Text = QuDate.Text = Date.GetDateTime(Common.User_DateTime, false);
            QuDate.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jQuote.IsExistDocument<JBS.JS.Quote>(QuNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jQuote.IsModify<JBS.JS.Quote>(QuNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jQuote.upModify1<JBS.JS.Quote>(QuNo.Text.Trim());//更新修改狀態1
                WriteToTxt(Common.load("Cancel", "quote", "quno", QuNo.Text.Trim()));
            }
            btnState = "Modify";
            tempNo = QuNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            if (Common.isCheck == "2")
            {
                IsCheck.Enabled = false;
                IsCheck.Checked = false;
            }
            else
            {
                IsCheck.Enabled = true;
            }

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前售價.ReadOnly = true;
            this.序號.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.型號.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            loadBom();
            QuNo.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jQuote.IsExistDocument<JBS.JS.Quote>(QuNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jQuote.IsModify<JBS.JS.Quote>(QuNo.Text.Trim()) != false)
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
                    cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());

                    cmd.CommandText = "delete from quote where QuNo=@quno";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from quoted where QuNo=@quno";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from quotebom where QuNo=@quno";
                    cmd.ExecuteNonQuery();

                    FrmAffixFile.FileDelete_單據刪除(cmd, QuNo.Text.Trim(), "報價單");

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
            using (FrmQuote_Print frm = new FrmQuote_Print())
            {
                frm.PK = QuNo.Text.Trim();
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

            using (var frm = new S1.FrmQuoteBrowNew())
            {
                frm.TSeekNo = QuNo.Text.Trim();
                frm.ShowDialog();

                WriteToTxt(Common.load("Check", "quote", "quno", frm.TResult.Trim()));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            bool falg = false;
            if (QuD.AsEnumerable().Count(r => r["itno"].ToString().Trim().Length == 0) > 0)
            {
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    if (dataGridViewT1["產品編號", i].Value.ToString() == "")
                    {
                        if (dataGridViewT1["報價數量", i].Value.ToString() == "0")
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
                MessageBox.Show("報價明細不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnState == "Append" || btnState == "Duplicate")
            {
                if (!SetQuNo()) return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Transaction = tn;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("qudate", Date.ToTWDate(QuDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("qudate1", Date.ToUSDate(QuDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("qudates", Date.ToTWDate(QuDateS.Text.Trim()));
                        cmd.Parameters.AddWithValue("qudates1", Date.ToUSDate(QuDateS.Text.Trim()));
                        cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuname1", CuName1.Text.Trim());
                        cmd.Parameters.AddWithValue("cuname2", cuname2);
                        cmd.Parameters.AddWithValue("cutel1", CuTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("cuper1", CuPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                        cmd.Parameters.AddWithValue("trno", TrNo.Text.ToString());
                        cmd.Parameters.AddWithValue("trname", trname);
                        cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
                        cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
                        cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
                        cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                        cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
                        cmd.Parameters.AddWithValue("taxb", Math.Round(Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
                        cmd.Parameters.AddWithValue("totmnyb", Math.Round(TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("qupayment", QuPayment.Text.Trim());
                        cmd.Parameters.AddWithValue("quperiod", Quperiod.Text.Trim());
                        cmd.Parameters.AddWithValue("qumemo", QuMemo.Text.Trim());
                        cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("appscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("qumemo1", memo1);
                        cmd.Parameters.AddWithValue("recordno", QuD.Rows.Count);
                        cmd.Parameters.AddWithValue("PhotoPath", PhotoPath);//13.7c
                        cmd.Parameters.AddWithValue("ischeck", IsCheck.Checked ? "1" : "0");
                        cmd.CommandText = "insert into quote ("
                        + " quno,qudate,qudate1,qudates,qudates1"
                            //+",cono,coname1,coname2,taxmnyf,usrno"
                        + " ,cuno,cuname1,cuname2,cutel1,cuper1,emno,emname,xa1no,xa1name,xa1par"
                        + " ,trno,trname,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,qupayment,quperiod,qumemo,appdate,edtdate,appscno,edtscno,qumemo1"
                        + " ,recordno,PhotoPath,ischeck)values("
                        + " @quno,@qudate,@qudate1,@qudates,@qudates1"
                        + " ,@cuno,@cuname1,@cuname2,@cutel1,@cuper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
                        + " ,@trno,@trname,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@qupayment,@quperiod,@qumemo,@appdate,@edtdate,@appscno,@edtscno,@qumemo1"
                        + " ,@recordno,@PhotoPath,@ischeck)";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < QuD.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("qudate", Date.ToTWDate(QuDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("qudate1", Date.ToUSDate(QuDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("qudates", Date.ToTWDate(QuDateS.Text.Trim()));
                            cmd.Parameters.AddWithValue("qudates1", Date.ToUSDate(QuDateS.Text.Trim()));
                            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                            cmd.Parameters.AddWithValue("trno", TrNo.Text.Trim());
                            cmd.Parameters.AddWithValue("itno", QuD.Rows[i]["itno"]);
                            cmd.Parameters.AddWithValue("itname", QuD.Rows[i]["itname"]);
                            cmd.Parameters.AddWithValue("ittrait", QuD.Rows[i]["ittrait"]);
                            cmd.Parameters.AddWithValue("itunit", QuD.Rows[i]["itunit"]);
                            cmd.Parameters.AddWithValue("itpkgqty", QuD.Rows[i]["itpkgqty"]);
                            cmd.Parameters.AddWithValue("qty", QuD.Rows[i]["qty"].ToDecimal());
                            cmd.Parameters.AddWithValue("price", QuD.Rows[i]["price"].ToDecimal());
                            cmd.Parameters.AddWithValue("priceb", QuD.Rows[i]["priceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("prs", QuD.Rows[i]["prs"].ToDecimal());
                            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                            cmd.Parameters.AddWithValue("taxprice", QuD.Rows[i]["taxprice"].ToDecimal());
                            cmd.Parameters.AddWithValue("taxpriceb", QuD.Rows[i]["taxpriceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("mny", QuD.Rows[i]["mny"].ToDecimal());
                            cmd.Parameters.AddWithValue("mnyb", QuD.Rows[i]["mnyb"].ToDecimal());
                            cmd.Parameters.AddWithValue("memo", QuD.Rows[i]["memo"]);
                            cmd.Parameters.AddWithValue("bomid", QuNo.Text.ToString().Trim() + QuD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", QuD.Rows[i]["BomRec"]);
                            cmd.Parameters.AddWithValue("recordno", (i + 1));
                            cmd.Parameters.AddWithValue("itdesp1", QuD.Rows[i]["itdesp1"]);
                            cmd.Parameters.AddWithValue("itdesp2", QuD.Rows[i]["itdesp2"]);
                            cmd.Parameters.AddWithValue("itdesp3", QuD.Rows[i]["itdesp3"]);
                            cmd.Parameters.AddWithValue("itdesp4", QuD.Rows[i]["itdesp4"]);
                            cmd.Parameters.AddWithValue("itdesp5", QuD.Rows[i]["itdesp5"]);
                            cmd.Parameters.AddWithValue("itdesp6", QuD.Rows[i]["itdesp6"]);
                            cmd.Parameters.AddWithValue("itdesp7", QuD.Rows[i]["itdesp7"]);
                            cmd.Parameters.AddWithValue("itdesp8", QuD.Rows[i]["itdesp8"]);
                            cmd.Parameters.AddWithValue("itdesp9", QuD.Rows[i]["itdesp9"]);
                            cmd.Parameters.AddWithValue("itdesp10", QuD.Rows[i]["itdesp10"]);
                            cmd.Parameters.AddWithValue("mwidth1", QuD.Rows[i]["mwidth1"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth2", QuD.Rows[i]["mwidth2"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth3", QuD.Rows[i]["mwidth3"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth4", QuD.Rows[i]["mwidth4"].ToDecimal());
                            cmd.Parameters.AddWithValue("pqty", QuD.Rows[i]["pqty"].ToDecimal());
                            cmd.Parameters.AddWithValue("punit", QuD.Rows[i]["punit"].ToString());
                            cmd.Parameters.AddWithValue("pformula", QuD.Rows[i]["pformula"].ToString());
                            cmd.Parameters.AddWithValue("standard",QuD.Rows[i]["standard"].ToString());

                            cmd.CommandText = "insert into quoted("
                                + " quno,qudate,qudate1,qudates,qudates1"
                                //+" ,lowzero,sltflag,extflag,stName"
                                + " ,cuno,emno,xa1no,xa1par,trno,itno,itname,ittrait,itunit,itpkgqty"
                                + " ,qty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo"
                                + " ,bomid,bomrec,recordno,standard"
                                + " ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                                + " ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10,mwidth1,mwidth2,mwidth3,mwidth4,pqty,punit,pformula)values("
                                + " @quno,@qudate,@qudate1,@qudates,@qudates1"
                                + " ,@cuno,@emno,@xa1no,@xa1par,@trno,@itno,@itname,@ittrait,@itunit,@itpkgqty"
                                + " ,@qty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo"
                                + " ,@bomid,@bomrec,@recordno,@standard"
                                + " ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                                + " ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@pqty,@punit,@pformula)";

                            cmd.ExecuteNonQuery();
                        }

                        for (int i = 0; i < QuBom.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("quno", QuNo.Text);
                            cmd.Parameters.AddWithValue("bomid", QuNo.Text + QuBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", QuBom.Rows[i]["BomRec"].ToString());
                            cmd.Parameters.AddWithValue("itno", QuBom.Rows[i]["itno"].ToString());
                            cmd.Parameters.AddWithValue("itname", QuBom.Rows[i]["itname"].ToString());
                            cmd.Parameters.AddWithValue("itunit", QuBom.Rows[i]["itunit"].ToString());
                            cmd.Parameters.AddWithValue("itqty", QuBom.Rows[i]["itqty"].ToString());
                            cmd.Parameters.AddWithValue("itpareprs", QuBom.Rows[i]["itpareprs"].ToString());
                            cmd.Parameters.AddWithValue("itpkgqty", QuBom.Rows[i]["itpkgqty"].ToString());
                            cmd.Parameters.AddWithValue("itrec", (i + 1));
                            cmd.Parameters.AddWithValue("itprice", QuBom.Rows[i]["itprice"].ToString());
                            cmd.Parameters.AddWithValue("itprs", 1);
                            cmd.Parameters.AddWithValue("itmny", QuBom.Rows[i]["itmny"].ToString());
                            cmd.Parameters.AddWithValue("itnote", QuBom.Rows[i]["itnote"].ToString());
                            cmd.CommandText = "insert into quotebom ("
                                + "quno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                                //+ "itsource,itbuypri,itbuymny,"
                                + "itprice,itprs,itmny,itnote)values("
                                + "@quno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,"
                                + "@itprice,@itprs,@itmny,@itnote)";

                            cmd.ExecuteNonQuery();
                        }

                        FrmAffixFile.FileSave_單據存檔(DaFile, cmd, QuNo.Text.Trim(), "報價單");

                        tn.Commit();
                        tn.Dispose();
                        cmd.Dispose();
                        tempNo = QuNo.Text.Trim();
                        
                        if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            using (FrmQuote_Print frm = new FrmQuote_Print())
                            {
                                frm.PK = QuNo.Text.Trim();
                                frm.ShowDialog();
                            }
                        }
                        BomRec = 0;
                        btnAppend_Click(null, null);

                    }
                    catch (Exception ex)
                    {
                        tn.Rollback();
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
                        cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                        cmd.CommandText = "delete from quote where quno=@quno";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("qudate", Date.ToTWDate(QuDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("qudate1", Date.ToUSDate(QuDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("qudates", Date.ToTWDate(QuDateS.Text.Trim()));
                        cmd.Parameters.AddWithValue("qudates1", Date.ToUSDate(QuDateS.Text.Trim()));
                        cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuname1", CuName1.Text.Trim());
                        cmd.Parameters.AddWithValue("cuname2", cuname2);
                        cmd.Parameters.AddWithValue("cutel1", CuTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("cuper1", CuPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                        cmd.Parameters.AddWithValue("trno", TrNo.Text.ToString());
                        cmd.Parameters.AddWithValue("trname", trname);
                        cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
                        cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
                        cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
                        cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                        cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
                        cmd.Parameters.AddWithValue("taxb", Math.Round(Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
                        cmd.Parameters.AddWithValue("totmnyb", Math.Round(TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("qupayment", QuPayment.Text.Trim());
                        cmd.Parameters.AddWithValue("quperiod", Quperiod.Text.Trim());
                        cmd.Parameters.AddWithValue("qumemo", QuMemo.Text.Trim());
                        cmd.Parameters.AddWithValue("appdate", T.Rows[0]["AppDate"].ToString());
                        cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("appscno", T.Rows[0]["AppScNo"].ToString());
                        cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("qumemo1", memo1);
                        cmd.Parameters.AddWithValue("recordno", QuD.Rows.Count);
                        cmd.Parameters.AddWithValue("PhotoPath", PhotoPath);//13.7c
                        cmd.Parameters.AddWithValue("ischeck", IsCheck.Checked ? "1" : "0");
                        cmd.CommandText = "insert into quote ("
                        + " quno,qudate,qudate1,qudates,qudates1"
                            //+",cono,coname1,coname2,taxmnyf,usrno"
                        + " ,cuno,cuname1,cuname2,cutel1,cuper1,emno,emname,xa1no,xa1name,xa1par"
                        + " ,trno,trname,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,qupayment,quperiod,qumemo,appdate,edtdate,appscno,edtscno,qumemo1"
                        + " ,recordno,PhotoPath,ischeck)values("
                        + " @quno,@qudate,@qudate1,@qudates,@qudates1"
                        + " ,@cuno,@cuname1,@cuname2,@cutel1,@cuper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
                        + " ,@trno,@trname,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@qupayment,@quperiod,@qumemo,@appdate,@edtdate,@appscno,@edtscno,@qumemo1"
                        + " ,@recordno,@PhotoPath,@ischeck)";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                        cmd.CommandText = "delete from quoted where quno=@quno";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < QuD.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("qudate", Date.ToTWDate(QuDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("qudate1", Date.ToUSDate(QuDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("qudates", Date.ToTWDate(QuDateS.Text.Trim()));
                            cmd.Parameters.AddWithValue("qudates1", Date.ToUSDate(QuDateS.Text.Trim()));
                            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                            cmd.Parameters.AddWithValue("trno", TrNo.Text.Trim());
                            cmd.Parameters.AddWithValue("itno", QuD.Rows[i]["itno"]);
                            cmd.Parameters.AddWithValue("itname", QuD.Rows[i]["itname"]);
                            cmd.Parameters.AddWithValue("ittrait", QuD.Rows[i]["ittrait"]);
                            cmd.Parameters.AddWithValue("itunit", QuD.Rows[i]["itunit"]);
                            cmd.Parameters.AddWithValue("itpkgqty", QuD.Rows[i]["itpkgqty"]);
                            cmd.Parameters.AddWithValue("qty", QuD.Rows[i]["qty"].ToDecimal());
                            cmd.Parameters.AddWithValue("price", QuD.Rows[i]["price"].ToDecimal());
                            cmd.Parameters.AddWithValue("priceb", QuD.Rows[i]["priceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("prs", QuD.Rows[i]["prs"].ToDecimal());
                            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                            cmd.Parameters.AddWithValue("taxprice", QuD.Rows[i]["taxprice"].ToDecimal());
                            cmd.Parameters.AddWithValue("taxpriceb", QuD.Rows[i]["taxpriceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("mny", QuD.Rows[i]["mny"].ToDecimal());
                            cmd.Parameters.AddWithValue("mnyb", QuD.Rows[i]["mnyb"].ToDecimal());
                            cmd.Parameters.AddWithValue("memo", QuD.Rows[i]["memo"]);
                            cmd.Parameters.AddWithValue("bomid", QuNo.Text.ToString().Trim() + QuD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", QuD.Rows[i]["BomRec"]);
                            cmd.Parameters.AddWithValue("recordno", (i + 1));
                            cmd.Parameters.AddWithValue("itdesp1", QuD.Rows[i]["itdesp1"]);
                            cmd.Parameters.AddWithValue("itdesp2", QuD.Rows[i]["itdesp2"]);
                            cmd.Parameters.AddWithValue("itdesp3", QuD.Rows[i]["itdesp3"]);
                            cmd.Parameters.AddWithValue("itdesp4", QuD.Rows[i]["itdesp4"]);
                            cmd.Parameters.AddWithValue("itdesp5", QuD.Rows[i]["itdesp5"]);
                            cmd.Parameters.AddWithValue("itdesp6", QuD.Rows[i]["itdesp6"]);
                            cmd.Parameters.AddWithValue("itdesp7", QuD.Rows[i]["itdesp7"]);
                            cmd.Parameters.AddWithValue("itdesp8", QuD.Rows[i]["itdesp8"]);
                            cmd.Parameters.AddWithValue("itdesp9", QuD.Rows[i]["itdesp9"]);
                            cmd.Parameters.AddWithValue("itdesp10", QuD.Rows[i]["itdesp10"]);
                            cmd.Parameters.AddWithValue("mwidth1", QuD.Rows[i]["mwidth1"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth2", QuD.Rows[i]["mwidth2"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth3", QuD.Rows[i]["mwidth3"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth4", QuD.Rows[i]["mwidth4"].ToDecimal());
                            cmd.Parameters.AddWithValue("pqty", QuD.Rows[i]["pqty"].ToDecimal());
                            cmd.Parameters.AddWithValue("punit", QuD.Rows[i]["punit"].ToString());
                            cmd.Parameters.AddWithValue("pformula", QuD.Rows[i]["pformula"].ToString());
                            cmd.Parameters.AddWithValue("standard", QuD.Rows[i]["standard"].ToString());

                            cmd.CommandText = "insert into quoted("
                                + " quno,qudate,qudate1,qudates,qudates1"
                                //+" ,lowzero,sltflag,extflag,stName"
                                + " ,cuno,emno,xa1no,xa1par,trno,itno,itname,ittrait,itunit,itpkgqty"
                                + " ,qty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo"
                                + " ,bomid,bomrec,recordno,standard"
                                + " ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                                + " ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10,mwidth1,mwidth2,mwidth3,mwidth4,pqty,punit,pformula)values("
                                + " @quno,@qudate,@qudate1,@qudates,@qudates1"
                                + " ,@cuno,@emno,@xa1no,@xa1par,@trno,@itno,@itname,@ittrait,@itunit,@itpkgqty"
                                + " ,@qty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo"
                                + " ,@bomid,@bomrec,@recordno,@standard"
                                + " ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                                + " ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@pqty,@punit,@pformula)";

                            cmd.ExecuteNonQuery();
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                        cmd.CommandText = "delete from quotebom where quno=@quno";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < QuBom.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("quno", QuNo.Text);
                            cmd.Parameters.AddWithValue("bomid", QuNo.Text + QuBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", QuBom.Rows[i]["BomRec"].ToString());
                            cmd.Parameters.AddWithValue("itno", QuBom.Rows[i]["itno"].ToString());
                            cmd.Parameters.AddWithValue("itname", QuBom.Rows[i]["itname"].ToString());
                            cmd.Parameters.AddWithValue("itunit", QuBom.Rows[i]["itunit"].ToString());
                            cmd.Parameters.AddWithValue("itqty", QuBom.Rows[i]["itqty"].ToString());
                            cmd.Parameters.AddWithValue("itpareprs", QuBom.Rows[i]["itpareprs"].ToString());
                            cmd.Parameters.AddWithValue("itpkgqty", QuBom.Rows[i]["itpkgqty"].ToString());
                            cmd.Parameters.AddWithValue("itrec", (i + 1));
                            cmd.Parameters.AddWithValue("itprice", QuBom.Rows[i]["itprice"].ToString());
                            cmd.Parameters.AddWithValue("itprs", 1);
                            cmd.Parameters.AddWithValue("itmny", QuBom.Rows[i]["itmny"].ToString());
                            cmd.Parameters.AddWithValue("itnote", QuBom.Rows[i]["itnote"].ToString());
                            cmd.CommandText = "insert into quotebom ("
                                + "quno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                                //+ "itsource,itbuypri,itbuymny,"
                                + "itprice,itprs,itmny,itnote)values("
                                + "@quno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,"
                                + "@itprice,@itprs,@itmny,@itnote)";

                            cmd.ExecuteNonQuery();
                        }
                        tn.Commit();
                        tn.Dispose();
                        cmd.Dispose();
                        tempNo = QuNo.Text.Trim();
                        if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            using (FrmQuote_Print frm = new FrmQuote_Print())
                            {
                                frm.PK = QuNo.Text.Trim();
                                frm.ShowDialog();
                            }
                        }
                        jQuote.upModify0<JBS.JS.Quote>(QuNo.Text.Trim());//更新修改狀態0
                        BomRec = 0;
                        btnAppend_Click(null, null);
                    }

                    catch (Exception ex)
                    {
                        tn.Rollback();
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = string.Empty;
            WriteToTxt(Common.load("Cancel", "quote", "quno", tempNo));
            Common.SetTextState(FormState = FormEditState.None, ref list);
            dataGridViewT1.ReadOnly = true;
            IsCheck.Enabled = false;
            btnAppend.Focus();
            jQuote.upModify0<JBS.JS.Quote>(QuNo.Text.Trim());//更新修改狀態0
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }





        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            jQuote.Open<JBS.JS.Cust>(sender, reader =>
            {
                CuNo.Text = reader["cuno"].ToString().Trim();
                CuName1.Text = reader["cuname1"].ToString();
                CuPer1.Text = reader["cuper1"].ToString().GetUTF8(10);
                CuTel1.Text = reader["cutel1"].ToString();
                //X3No.Text = reader["cux3no"].ToString();
                EmNo.Text = reader["cuemno1"].ToString();
                Xa1No.Text = reader["cuxa1no"].ToString();
                this.Disc = reader["CuDisc"].ToDecimal();
                //pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);
                CuNo.SelectAll();

                this.TextBefore = reader["cuno"].ToString().Trim();

                X3No.Text = reader["cux3no"].ToString();
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                //if (this.FormState != FormEditState.Modify)
                //{
                //    for (int i = 0; i < QuD.Rows.Count; i++)
                //    {
                //        Common.GetSpecialPrice(QuD.Rows[i], i, CuNo, QuDate, dataGridViewT1, GetSystemPrice);
                //        SetRow_TaxPrice(QuD.Rows[i]);
                //        SetRow_Mny(QuD.Rows[i]);
                //        dataGridViewT1.InvalidateRow(i);
                //    }
                //    SetAllMny();
                //}
            });
            rowstandard(null, 0);
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly || btnCancel.Focused) return;
            if (CuNo.Text.Trim() == "")
            {
                e.Cancel = true;
                CuName1.Text = CuPer1.Text = CuTel1.Text = EmNo.Text = EmName.Text = "";
                MessageBox.Show("請先輸入客戶編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jQuote.ValidateOpen<JBS.JS.Cust>(sender, e, row =>
            {
                if (CuNo.Text.Trim() == TextBefore)
                    return;

                CuNo.Text = row["cuno"].ToString();
                CuName1.Text = row["cuname1"].ToString();
                CuPer1.Text = row["cuper1"].ToString().GetUTF8(10);
                CuTel1.Text = row["cutel1"].ToString();
                cuname2 = row["cuname2"].ToString();
                EmNo.Text = row["cuemno1"].ToString();
                Xa1No.Text = row["cuxa1no"].ToString();
                this.Disc = row["CuDisc"].ToDecimal(); 
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);

                this.TextBefore = row["cuno"].ToString().Trim();

                //規則:複製時金額不變，所以也不可以帶稅別，不然總額會不對
                X3No.Text = row["CUX3No"].ToString();
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);

                //if (FormState != FormEditState.Duplicate)
                //{
                //    if (this.FormState != FormEditState.Modify)
                //    {
                //        for (int i = 0; i < QuD.Rows.Count; i++)
                //        {
                //            Common.GetSpecialPrice(QuD.Rows[i], i, CuNo, QuDate, dataGridViewT1, GetSystemPrice);
                //            SetRow_TaxPrice(QuD.Rows[i]);
                //            SetRow_Mny(QuD.Rows[i]);
                //            dataGridViewT1.InvalidateRow(i);
                //        }
                //        SetAllMny();
                //    }
                //}
            });
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jQuote.Open<JBS.JS.Empl>(sender, row =>
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

            jQuote.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        private void TrNo_DoubleClick(object sender, EventArgs e)
        {
            jQuote.Open<JBS.JS.Trade>(sender, row =>
            {
                TrNo.Text = row["TrNo"].ToString().Trim();
                this.trname = row["TrName"].ToString().Trim();
            });
        }
        private void TrNo_Validating(object sender, CancelEventArgs e)
        {
            if (TrNo.ReadOnly || btnCancel.Focused)
                return;

            if (TrNo.TrimTextLenth() == 0)
            {
                TrNo.Clear();
                this.trname = "";
                return;
            }

            jQuote.ValidateOpen<JBS.JS.Trade>(sender, e, row =>
            {
                TrNo.Text = row["TrNo"].ToString().Trim();
                this.trname = row["TrName"].ToString().Trim();
            });
        }

        private void QuPayment_DoubleClick(object sender, EventArgs e)
        { 
            using (var frm = new SOther.FrmPayNoteBrow())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        QuPayment.Text = frm.TResult;
                        break;
                }
            }
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jQuote.Open<JBS.JS.XX03>(sender, row =>
            {
                X3No.Text = row["X3No"].ToString().Trim();
                X3Name.Text = row["X3Name"].ToString();
                decimal _rate = 0;
                decimal.TryParse(row["X3Rate"].ToString(), out _rate);
                _rate = _rate * 100;
                Rate.Text = _rate.ToString("f0");

                for (int i = 0; i < QuD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(QuD.Rows[i]);
                    SetRow_Mny(QuD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["X3No"].ToString().Trim();
            });
        }

        private void X3No_Validating(object sender, CancelEventArgs e)
        {
            if (X3No.ReadOnly || btnCancel.Focused) return;
            if (X3No.Text == "")
            {
                X3No.Text = X3Name.Text = Rate.Text = "";
                e.Cancel = true;
                X3No.SelectAll();
                MessageBox.Show("稅別編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jQuote.ValidateOpen<JBS.JS.XX03>(sender, e, row =>
            {
                if (X3No.Text.Trim() == TextBefore)
                    return;

                X3No.Text = row["X3No"].ToString().Trim();
                X3Name.Text = row["X3Name"].ToString();
                decimal rate = row["X3Rate"].ToString().ToDecimal() * 100;
                Rate.Text = rate.ToString("f" + Rate.LastNum);

                for (int i = 0; i < QuD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(QuD.Rows[i]);
                    SetRow_Mny(QuD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["X3No"].ToString().Trim();
            });
        }

        private void Quperiod_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new SOther.FrmPerNote())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        Quperiod.Text = frm.TResult;
                        break;
                }
            } 
        }

        private void QuMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(QuMemo, QuMemo.MaxLength);
        }

        private void QuDate_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.ReadOnly || btnCancel.Focused) return;
            if (QuDate.Text == "")
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

            if (tb.Name == "QuDate")
            {
                //if (this.FormState != FormEditState.Modify)
                //{
                //    if (QuDateS.Text != "")
                //    {
                //        if (string.CompareOrdinal(QuDate.Text.ToString().Trim(), QuDateS.Text.ToString().Trim()) > 0)
                //            QuDateS.Text = QuDate.Text;
                //        if (CuNo.Text.Trim().Length > 0)
                //        {
                //            for (int i = 0; i < QuD.Rows.Count; i++)
                //            {
                //                Common.GetSpecialPrice(QuD.Rows[i], i, CuNo, QuDate, dataGridViewT1, GetSystemPrice);
                //                SetRow_TaxPrice(QuD.Rows[i]);
                //                SetRow_Mny(QuD.Rows[i]);
                //                dataGridViewT1.InvalidateRow(i);
                //            }
                //            SetAllMny();
                //        }
                //    }
                //}
            }


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
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MST);
            }
            else
            {
                TotMny.Text = (taxmny + tax).ToString("f" + Common.MST);
            }

            //decimal tax = Tax.Text.ToDecimal();
            //decimal taxmny = TaxMny.Text.ToDecimal();
            //TotMny.Text = Math.Round(tax + taxmny, Common.MST, MidpointRounding.AwayFromZero).ToString();
        }



        private void QuNo_DoubleClick(object sender, EventArgs e)
        {
            if (QuNo.ReadOnly)
                return;

            using (var frm = new FrmQuote_Print_QuNo())
            { 
                frm.TSeekNo = QuNo.Text.Trim();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    QuNo.Text = frm.TResult.Trim();
                }
            }
        }

        private void QuNo_Validating(object sender, CancelEventArgs e)
        {
            if (QuNo.ReadOnly || btnCancel.Focused) return;

            if (QuNo.Text.Length > 0 && QuNo.Text.Trim() == "")
            {
                e.Cancel = true;
                QuNo.Text = "";
                QuNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnState == "Append")
            {
                if (jQuote.IsExistDocument<JBS.JS.Quote>(QuNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnState == "Duplicate")
            {
                if (jQuote.IsExistDocument<JBS.JS.Quote>(QuNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnState == "Modify")
            {
                var dr = Common.load("Check", "quote", "quno", QuNo.Text.Trim());
                if (dr != null)
                {
                    if (TextBefore != QuNo.Text.Trim())
                    {
                        WriteToTxt(dr);
                        loadBom();
                    }
                }
                else
                {
                    e.Cancel = true;
                    QuNo.SelectAll();

                    using (var frm = new FrmQuote_Print_QuNo())
                    { 
                        frm.TSeekNo = QuNo.Text.Trim();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            QuNo.Text = frm.TResult.Trim();
                            WriteToTxt(Common.load("Check", "quote", "quno", QuNo.Text));
                            loadBom();
                        }
                    }
                }
            }
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (CuNo.Text == "") return;
            if (dataGridViewT1.ReadOnly) return;
            if (dataGridViewT1.Rows.Count > 0) return;
            gridAppend_Click(null, null);
        }

        private void dataGridViewT1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                dataGridViewT1["序號", e.RowIndex].Value = (e.RowIndex + 1).ToString();
            }
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.ReadOnly)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                TextBefore = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "包裝數量")
            {
                TextBefore = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "報價數量")
            {
                TextBefore = dataGridViewT1["報價數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jQuote.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                jQuote.DataGridViewOpen<JBS.JS.Item>(sender, e, QuD, row => FillItem(row, e.RowIndex));


            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                var itno = dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var row = Common.load("Check", "item", "itno", itno);
                if (row != null)
                {
                    if (row != null && unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        QuD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            QuD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            QuD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = unit;

                    QuD.Rows[e.RowIndex]["itunit"] = unit;
                    dataGridViewT1.InvalidateRow(e.RowIndex);

                    //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                    if (Common.Sys_DBqty == 1)//1代表一般進銷存
                    {
                        Common.GetSpecialPrice(QuD.Rows[e.RowIndex], e.RowIndex, CuNo, QuDate, dataGridViewT1, GetSystemPrice);
                        SetRow_TaxPrice(QuD.Rows[e.RowIndex]);
                        SetRow_Mny(QuD.Rows[e.RowIndex]);

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        SetAllMny();
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "說明")
            {
                using (FrmSale_Memo frm = new FrmSale_Memo())
                {
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                            QuD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            break;
                        case DialogResult.Cancel: break;
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "報價數量")
            {
                if (Common.Sys_DBqty == 1)
                {
                    using (FrmComputer frm = new FrmComputer())
                    {
                        frm.w1 = QuD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = QuD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = QuD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = QuD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = QuD.Rows[e.RowIndex]["Pformula"].ToString();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            QuD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            QuD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            QuD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            QuD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            QuD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;

                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);

                            QuD.Rows[e.RowIndex]["Qty"] = frm.resultCount.ToString("f" + Common.Q);
                            QuD.Rows[e.RowIndex]["PQty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(QuD.Rows[e.RowIndex]);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            SetAllMny();
                        }
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "計價數量")
            {
                if (Common.Sys_DBqty == 2)
                {
                    using (FrmComputer frm = new FrmComputer())
                    {
                        frm.w1 = QuD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = QuD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = QuD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = QuD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = QuD.Rows[e.RowIndex]["Pformula"].ToString();
                        frm.qty = QuD.Rows[e.RowIndex]["qty"].ToDecimal();
                        frm.lbTxt = "報價數量";

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            QuD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                            QuD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                            QuD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                            QuD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                            QuD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;
                            QuD.Rows[e.RowIndex]["qty"] = frm.qty;

                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);

                            QuD.Rows[e.RowIndex]["Pqty"] = frm.resultCount.ToString("f" + Common.Q);

                            SetRow_Mny(QuD.Rows[e.RowIndex]);
                            dataGridViewT1.InvalidateRow(e.RowIndex);
                            SetAllMny();
                        }
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "計位")
            {
                if (Common.Sys_DBqty == 2)
                {
                    using (var frm = new FrmUnit())
                    { 
                        frm.Kid = 1;
                        if (DialogResult.OK == frm.ShowDialog())
                        {
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.Result;

                            QuD.Rows[e.RowIndex]["punit"] = frm.Result;
                        }
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "售價")
            {
                if (QuD.Rows[e.RowIndex]["itno"].ToString().Trim() == "") return;
                using (var frm = new FrmItemLevelb())
                {
                    frm.TSeekNo = QuD.Rows[e.RowIndex]["itno"].ToString().Trim();
                    frm.itunit = QuD.Rows[e.RowIndex]["itunit"].ToString().Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Result.ToDecimal().ToString("f" + Common.MS);

                        QuD.Rows[e.RowIndex]["price"] = frm.Result.ToDecimal("f" + Common.MS);
                        SetRow_TaxPrice(QuD.Rows[e.RowIndex]);
                        SetRow_Mny(QuD.Rows[e.RowIndex]);
                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        SetAllMny();
                    }
                    if (dataGridViewT1.EditingControl != null)
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                }
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly || btnCancel.Focused) return;
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                string itnonow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (itnonow == "")
                {
                    QuD.Rows[e.RowIndex]["itno"] = "";
                    QuD.Rows[e.RowIndex]["itname"] = "";
                    QuD.Rows[e.RowIndex]["punit"] = "";
                    QuD.Rows[e.RowIndex]["pqty"] = 0;
                    QuD.Rows[e.RowIndex]["qty"] = 0;
                    QuD.Rows[e.RowIndex]["itunit"] = "";
                    QuD.Rows[e.RowIndex]["price"] = 0;
                    QuD.Rows[e.RowIndex]["prs"] = 1;
                    QuD.Rows[e.RowIndex]["taxprice"] = 0;
                    QuD.Rows[e.RowIndex]["mny"] = 0;
                    QuD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    QuD.Rows[e.RowIndex]["產品組成"] = "";
                    QuD.Rows[e.RowIndex]["memo"] = "";
                    QuD.Rows[e.RowIndex]["priceb"] = 0;
                    QuD.Rows[e.RowIndex]["taxpriceb"] = 0;
                    QuD.Rows[e.RowIndex]["mnyb"] = 0;
                    QuD.Rows[e.RowIndex]["mwidth1"] = 0;
                    QuD.Rows[e.RowIndex]["mwidth2"] = 0;
                    QuD.Rows[e.RowIndex]["mwidth3"] = 0;
                    QuD.Rows[e.RowIndex]["mwidth4"] = 0;
                    QuD.Rows[e.RowIndex]["Pformula"] = "";
                    QuD.Rows[e.RowIndex]["standard"] = "";
                    //折數
                    QuD.Rows[e.RowIndex]["Prs"] = (Common.Sys_SalePrice == 2) ? Disc : 1;

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();

                    var rec = QuD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jQuote.RemoveBom(rec, ref QuBom);
                    return;
                }

                if (itnonow == ItNoBegin)
                    return;

                if (itnonow == UdfNoBegin)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    QuD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jQuote.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, QuD, row => FillItem(row, e.RowIndex));

            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                string itno = QuD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (unit == TextBefore)
                    return;

                jQuote.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        QuD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        QuD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                QuD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)
                {
                    Common.GetSpecialPrice(QuD.Rows[e.RowIndex], e.RowIndex, CuNo, QuDate, dataGridViewT1, GetSystemPrice);
                    SetRow_TaxPrice(QuD.Rows[e.RowIndex]);
                    SetRow_Mny(QuD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "報價數量")
            {
                var qty = dataGridViewT1["報價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                if (Common.Sys_DBqty == 1)
                {
                    QuD.Rows[e.RowIndex]["Qty"] = qty;
                    QuD.Rows[e.RowIndex]["PQty"] = qty;
                }
                else if (Common.Sys_DBqty == 2)
                {
                    QuD.Rows[e.RowIndex]["Qty"] = qty;
                }

                SetRow_Mny(QuD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "計價數量")
            {
                QuD.Rows[e.RowIndex]["PQty"] = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                SetRow_Mny(QuD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "售價")
            {
                if (Common.Sys_LowCost != 3 && dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() != "")
                    pVar.CheckLowCost(dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["單位", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS));
                QuD.Rows[e.RowIndex]["price"] = dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS);
                SetRow_TaxPrice(QuD.Rows[e.RowIndex]);
                SetRow_Mny(QuD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "稅前金額")
            {
                //正常情形『稅前金額』是由『售價』帶出來的
                //下面處理的情形是手動打上『稅前金額』
                //所以須往前推算『售價』金額。
                //decimal beforeMny = QuD.Rows[e.RowIndex]["mny"].ToDecimal("f" + Common.TPS);
                //decimal nowMny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.TPS);
                //if (beforeMny == nowMny) return;
                decimal price = 0;
                decimal qty = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f" + Common.Q);
                decimal taxprice = dataGridViewT1["稅前售價", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f6");
                decimal mny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.TPS);
                decimal prs = QuD.Rows[e.RowIndex]["prs"].ToDecimal("f3");
                if (qty == 0) qty = 1;
                QuD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / qty) / prs).ToDecimal("f" + Common.MS);
                        QuD.Rows[e.RowIndex]["price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / qty) / prs).ToDecimal("f" + Common.MS);
                        QuD.Rows[e.RowIndex]["price"] = price;
                        break;
                }
                SetRow_TaxPrice(QuD.Rows[e.RowIndex]);

                taxprice = QuD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                QuD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                QuD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                QuD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "折數")
            {
                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                QuD.Rows[e.RowIndex]["prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");
                SetRow_TaxPrice(QuD.Rows[e.RowIndex]);
                SetRow_Mny(QuD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
        }

        private void FillItem(SqlDataReader row, int index)
        {
            this.ItNoBegin = row["itno"].ToString().Trim();

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = row["itno"].ToString().Trim();

            QuD.Rows[index]["itno"] = row["itno"].ToString().Trim();
            QuD.Rows[index]["itname"] = row["itname"].ToString();
            QuD.Rows[index]["ItNoUdf"] = row["ItNoUdf"].ToString();
            //銷貨常用單位
            string unitset = row["ItSalUnit"].ToString().Trim();
            string unit;
            //預設帶包裝單位或是單位
            if (unitset == "1")
            {
                unit = row["ItUnitp"].ToString();
                QuD.Rows[index]["ItUnit"] = unit;

                var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0)
                    itpkgqty = 1;
                QuD.Rows[index]["itpkgqty"] = itpkgqty;
            }
            else
            {
                unit = row["ItUnit"].ToString();
                QuD.Rows[index]["ItUnit"] = unit;
                QuD.Rows[index]["itpkgqty"] = 1;
            }
            Common.GetSpecialPrice(QuD.Rows[index], index, CuNo, QuDate, dataGridViewT1, GetSystemPrice);
            SetRow_TaxPrice(QuD.Rows[index]);
            SetRow_Mny(QuD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();

            if (row["ittrait"].ToString() == "1")
            {
                QuD.Rows[index]["產品組成"] = "組合品";
                QuD.Rows[index]["ittrait"] = 1;
            }
            else if (row["ittrait"].ToString() == "2")
            {
                QuD.Rows[index]["產品組成"] = "組裝品";
                QuD.Rows[index]["ittrait"] = 2;
            }
            else
            {
                QuD.Rows[index]["產品組成"] = "單一商品";
                QuD.Rows[index]["ittrait"] = 3;
            }
            for (int i = 1; i <= 10; i++)
            {
                QuD.Rows[index]["itdesp" + i] = row["itdesp" + i].ToString();
            }

            var rec = QuD.Rows[index]["BomRec"].ToString();
            jQuote.RemoveBom(rec, ref QuBom);

            jQuote.GetItemBom(row["itno"].ToString().Trim(), rec, ref QuBom);
            rowstandard(row["itno"].ToString().Trim(),index);//回寫對應的客戶型號
        }

        private void rowstandard(string p, int index)
        {
            try
            {
                dtstandard.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", p);
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim().ToString());
                    if (p == null)
                    {
                        for (int i = 0; i < dataGridViewT1.RowCount; i++)
                        {
                            dtstandard.Clear();
                            cmd.Parameters["itno"].Value = dataGridViewT1.Rows[i].Cells["產品編號"].Value.ToString();
                            cmd.CommandText = " select * from standard where itno=@itno and cfno=@cuno";
                            da.Fill(dtstandard);
                            if (dtstandard.Rows.Count != 0)
                                QuD.Rows[i]["standard"] = dtstandard.Rows[0]["standard"].ToString();
                            else
                                QuD.Rows[i]["standard"] = "";
                        }
                    }
                    else
                    {
                        cmd.CommandText = " select * from standard where itno=@itno and cfno=@cuno";
                        da.Fill(dtstandard);
                        if (dtstandard.Rows.Count != 0)
                            QuD.Rows[index]["standard"] = dtstandard.Rows[0]["standard"].ToString();
                        else
                            QuD.Rows[index]["standard"] = "";
                    }
                    
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

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
                        if (itunitp.Trim().Length > 0 && itunitp.Trim() == unit)
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
            var qty = row["pqty"].ToDecimal("f" + Common.Q);
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
            var sum = QuD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f" + Common.TPS)).ToDecimal("f" + Common.MST);

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
                var totmny = QuD.AsEnumerable().Sum(r => r["pqty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MS)).ToDecimal("f" + Common.MST);
                tax = (totmny / (1 + Common.Sys_Rate)) * Common.Sys_Rate;

                TotMny.Text = totmny.ToString("f" + Common.MST);
                tax = tax.ToDecimal("f" + Common.TS);
                Tax.Text = tax.ToString();
                TaxMny.Text = (totmny - tax).ToString("f" + Common.MST);
                TaxMnyB.Text = (TaxMny.Text.ToDecimal() * par).ToString("f" + Common.M);
            }
            else if (X3No.Text.ToInteger() == 3 || X3No.Text.ToInteger() == 4)
            {
                TaxMny.Text = sum.ToString("f" + Common.MST);
                TaxMnyB.Text = (sum * par).ToString("f" + Common.M);
                Tax.Text = tax.ToString("f" + Common.TS);
                TotMny.Text = sum.ToString("f" + Common.MST);
            }
        }




        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.ToString() == "F11, Shift")
            {
                if (gridItBuyPrice.Enabled) gridItBuyPrice.PerformClick();
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else if (keyData.ToString() == "F9, Shift")
            {
                if (gridTran.Enabled) gridTran.PerformClick();
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else if (keyData.ToString() == "F10, Shift")
            {
                if (gridAllTrans.Enabled) gridAllTrans.PerformClick();
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
                case Keys.F3:
                    if (gridDelete.Enabled) gridDelete.PerformClick();
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

        ToolTip tip = new ToolTip();
        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
            TextBox t = (TextBox)e.Control;
            if (str == "產品編號" || str == "說明" || str == "售價")
            {
                t.KeyDown -= new KeyEventHandler(t_KeyDown);
                t.KeyDown += new KeyEventHandler(t_KeyDown);
                tip.SetToolTip(t, "雙擊滑鼠左鍵二下或按[F12]開窗查詢");
            }
            else if (str == "報價數量")
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
            else if (str == "計價數量")
            {
                if (Common.Sys_DBqty == 2)
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
            if (QuNo.Text.Trim() == "") return;
            FrmSale_AppScNo frm = new FrmSale_AppScNo();
            //新增人員
            frm.AName = T.Rows[0]["AppScNo"].ToString();//製單人員
            frm.ATime = T.Rows[0]["AppDate"].ToString();//製單人員
            //修改人員
            frm.EName = T.Rows[0]["EdtScNo"].ToString();//製單人員
            frm.ETime = T.Rows[0]["EdtDate"].ToString();//製單人員
            frm.ShowDialog();
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = CuNo.ReadOnly ? false : true;
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
                for (int i = 0; i < QuD.Rows.Count; i++)
                {
                    SetRow_Mny(QuD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();
            }
            dataGridViewT1_Click(null, null);
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            gridAppend.Enabled = gridDelete.Enabled = gridPicture.Enabled = gridInsert.Enabled = gridItDesp.Enabled = !btnAppend.Enabled;
            gridBomD.Enabled = !btnAppend.Enabled;

            gridStock.Enabled = gridTran.Enabled = gridAllTrans.Enabled = gridItBuyPrice.Enabled = gridAllTransTrans.Enabled = gridCost.Enabled = gridCustSale.Enabled = gridItemQuote.Enabled = true;
        }

        //13.7c
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
                    cmd.Parameters.AddWithValue("Datano", QuNo.Text.Trim());
                    cmd.CommandText = "select ROW_NUMBER() OVER(ORDER BY id) AS 序號, * from AffixFile where DaType ='報價單' and Datano=@Datano";
                    DaFile.Clear();
                    da.Fill(DaFile);

                    frm.DtFile = DaFile;
                    frm.Datano = QuNo.Text.Trim();
                    frm.CMD = cmd;
                    frm.DaType = "報價單";

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

        void test()
        {

        }
    }
}
