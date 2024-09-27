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
using S_61.SOther;
using System.IO;
using System.Diagnostics;

namespace S_61.subMenuFm_1
{
    public partial class FrmOrder : Formbase
    {
        JBS.JS.Order jOrder;

        DataTable OrD = new DataTable();
        DataTable OrBom = new DataTable();
        DataTable DaFile = new DataTable();
        DataTable dtstandard = new DataTable();
        List<TextBoxbase> list;
        DataTable T = new DataTable();
        SqlTransaction tn;

        string tempNo = "";
        string btnState = "";
        string ItNoBegin = "";
        string UdfNoBegin = "";
        decimal BomRec = 0; 
        string cuname2 = "";
        string trname = "";
        string spname = "";
        string TextBefore = "";
        string ormemo1 = "";
        string memo1 = "";
        decimal Disc = 1;
        //13.6a
        string  AdName = "";//AdName = 指送公司名稱
        string NetNo = "";
        // 13.7c
        string PhotoPath = "";  

        public FrmOrder()
        {
            InitializeComponent();
            this.jOrder = new JBS.JS.Order();
            this.list = this.getEnumMember();
            this.dataGridViewT1.tableName = "orderd";
 

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

            OrDate.MaxLength = EsDate.MaxLength = (Common.User_DateTime == 1) ? 7 : 8;
            this.交貨日期.DataPropertyName = (Common.User_DateTime == 1) ? "esdate" : "esdate1";

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

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlDataAdapter da = new SqlDataAdapter("Select Top(1)* from [order] where 1=0", cn))
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
                    end,存量預估=
                    case
                    when stkqtyflag=1 then '展開'
                    when stkqtyflag=2 then '不展開'
                    end,ItNoUdf = '',*
                    from orderd where 1=0 ";
                da.Fill(OrD);
                dataGridViewT1.DataSource = OrD;
            }
            OrD.Clear();

            WriteToTxt(Common.load("Bottom", "[order]", "orno"));
        }

        private void FrmOrder_Shown(object sender, EventArgs e)
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
            OrD.Clear();
            tempNo = memo1 = spname = NetNo = "";

            if (row == null)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                orpickingflag.Checked = oroverflag.Checked = ortrnflag.Checked = false;

                OrD.Clear();
                OrBom.Clear();
            }
            else
            {
                memo1 = row["ormemo1"].ToString();
                T.ImportRow(row);
                T.AcceptChanges();
                NetNo = row["NetNo"].ToString().Trim();
                OrNo.Text = row["orno"].ToString().Trim();
                QuNo.Text = row["quno"].ToString().Trim();
                tempNo = OrNo.Text.Trim();

                if (Common.User_DateTime == 1)
                    OrDate.Text = row["ordate"].ToString().Trim();
                else
                    OrDate.Text = row["ordate1"].ToString().Trim();

                if (row["esdate"].ToString().Trim() != "")
                    EsDate.Text = Common.User_DateTime == 1 ? row["esdate"].ToString() : row["esdate1"].ToString();
                else
                    EsDate.Text = OrDate.Text;

                EmNo.Text = row["emno"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
                CuNo.Text = row["cuno"].ToString();
                CuName1.Text = row["CuName1"].ToString();
                AdAddr.Text = row["AdAddr"].ToString().GetUTF8(60);
                AdPer1.Text = row["CuPer1"].ToString().GetUTF8(10);
                AdTel.Text = row["CuTel1"].ToString().GetUTF8(20);
                Xa1No.Text = row["Xa1No"].ToString();
                Xa1Name.Text = row["Xa1Name"].ToString();
                Xa1Par.Text = row["Xa1Par"].ToString();
                TrNo.Text = row["TrNo"].ToString();
                SpNo.Text = row["SpNo"].ToString();
                CardNo.Text = row["CardNo"].ToString();
                PhotoPath = row["PhotoPath"].ToString();//13.7c
                if (SpNo.Text.Trim() != "")
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                        cmd.CommandText = "select spname from spec where spno=@spno";
                        var p = cmd.ExecuteScalar();
                        if (p != null) spname = p.ToString().Trim();
                    }
                }

                TaxMny.Text = row["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                TaxMnyB.Text = row["TaxMnyB"].ToDecimal().ToString("f" + Common.M);

                X3No.Text = row["X3No"].ToString();
                Rate.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");
                Tax.Text = row["Tax"].ToDecimal().ToString("f" + Common.TS);
                TotMny.Text = row["TotMny"].ToDecimal().ToString("f" + Common.MST);

                OrPayment.Text = row["OrPayment"].ToString();
                Orperiod.Text = row["Orperiod"].ToString();
                OrMemo.Text = row["OrMemo"].ToString();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@x3no", X3No.Text.Trim());
                    cmd.CommandText = "select x3name from xx03 where x3no=@x3no";
                    var p = cmd.ExecuteScalar();
                    if (p != null) X3Name.Text = p.ToString().Trim();
                }

                ortrnflag.Checked = (row["ortrnflag"].ToString() == "True");
                oroverflag.Checked = (row["oroverflag"].ToString() == "True");
                orpickingflag.Checked = (row["orpickingflag"].ToString() == "True");

                loadD();
            }
            #region  帶入公司名稱變數值
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                AdName = "";
                try
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                    cmd.CommandText = "select ISNULL(name, '') as 公司名稱  from DeliveryAddress where cuno=@cuno and  DefaultPrint='V'";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            AdName = dr["公司名稱"].ToString();       //指送公司名稱
                        }
                    }
                    if (AdName == "")
                    {
                        cmd.CommandText = "select cuname2 from cust where cuno = @cuno ";
                        object 公司名稱 = cmd.ExecuteScalar();
                        if (公司名稱 != null)
                            AdName = 公司名稱.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
            }
            #endregion
        }

        void loadD()
        {
            OrD.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                cmd.CommandText = @" 
                    select 產品組成=
                    case
                    when ittrait=1 then '組合品'
                    when ittrait=2 then '組裝品'
                    when ittrait=3 then '單一商品'
                    end,存量預估=
                    case
                    when stkqtyflag=1 then '展開'
                    when stkqtyflag=2 then '不展開'
                    end,ItNoUdf= (select top 1 itnoudf from item where item.itno = orderD.itno),*
                    from orderd where orno=@orno ORDER BY recordno";

                dd.Fill(OrD);
                dataGridViewT1.DataSource = OrD;
            }
        }

        void loadBom()
        {
            OrBom.Clear();

            if (btnState == "Append")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = "select * from orderbom where 1=0 ";

                    dd.Fill(OrBom);
                }
            }

            if (btnState == "Modify")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("orno", this.tempNo);
                    cmd.CommandText = "select * from orderbom where orno=@orno";
                    dd.Fill(OrBom);
                }
            }

            if (btnState == "Duplicate")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("orno", this.tempNo);
                    cmd.CommandText = "select * from orderbom where orno=@orno";
                    dd.Fill(OrBom);
                }
            }
        }

        void AddRow()
        {
            DataRow dr = OrD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 0;
            dr["pqty"] = 0;
            dr["itunit"] = "";
            dr["punit"] = "";
            dr["price"] = 0;
            dr["mny"] = 0;
            dr["itpkgqty"] = 1;
            dr["taxpriceb"] = 0;
            dr["priceb"] = 0;
            dr["mnyb"] = 0;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["qtynotout"] = 0;
            dr["qtynotinstk"] = 0;
            dr["pformula"] = "";
            dr["esdate"] = Date.ToTWDate(EsDate.Text);
            dr["esdate1"] = Date.ToUSDate(EsDate.Text);
            dr["qtyout"] = 0;
            dr["qtyin"] = 0;
            dr["stkqtyflag"] = 2;
            dr["BomRec"] = GetBomRec();
            dr["存量預估"] = "不展開";
            if (Common.Sys_SalePrice == 2)
                dr["Prs"] = Disc;
            else dr["Prs"] = "1.00";
            填入OrD指送地址欄位資料(dr);
            OrD.Rows.Add(dr);
            OrD.AcceptChanges();
        }

        void AddRow(int index)
        {
            DataRow dr = OrD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 0;
            dr["pqty"] = 0;
            dr["itunit"] = "";
            dr["punit"] = "";
            dr["price"] = 0;
            dr["mny"] = 0;
            dr["itpkgqty"] = 1;
            dr["taxpriceb"] = 0;
            dr["priceb"] = 0;
            dr["mnyb"] = 0;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["qtynotout"] = 0;
            dr["qtynotinstk"] = 0;
            dr["pformula"] = "";
            dr["esdate"] = Date.ToTWDate(EsDate.Text);
            dr["esdate1"] = Date.ToUSDate(EsDate.Text);
            dr["qtyout"] = 0;
            dr["qtyin"] = 0;
            dr["stkqtyflag"] = 2;
            dr["BomRec"] = GetBomRec();
            dr["存量預估"] = "不展開";
            if (Common.Sys_SalePrice == 2)
                dr["Prs"] = Disc;
            else dr["Prs"] = "1.00";
            

            填入OrD指送地址欄位資料(dr);
            OrD.Rows.InsertAt(dr, index);
            OrD.AcceptChanges();
        }

        private void 填入OrD指送地址欄位資料(DataRow dRow) //小按鈕(新增，或插入)明細時
        {
            dRow["AdAddr"] = AdAddr.Text.Trim();
            dRow["Adper1"] = AdPer1.Text.Trim();
            dRow["Adtel"] = AdTel.Text.Trim();
            dRow["AdName"] = AdName;                //指送公司名稱
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
                for (int i = 0; i < OrD.Rows.Count; i++)
                {
                    if (Convert.ToInt32(OrD.Rows[i]["BomRec"].ToString()) > d)
                        d = Convert.ToInt32(OrD.Rows[i]["BomRec"].ToString());
                }
                BomRec = ++d;
                return BomRec;
            }
        }

        void DeleteRow(int index)
        {
            string rec = dataGridViewT1["組件編號", index].Value.ToString();
            int rowcount = OrBom.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                if (OrBom.Rows[i]["BomRec"].ToString() == rec)
                    OrBom.Rows[i].Delete();
            }
            OrBom.AcceptChanges();
            OrD.Rows[index].Delete();
            OrD.AcceptChanges();
            SetAllMny();
        }

        bool SetOrNo()
        {
            string strOrNo = "";
            if (OrNo.Text != "")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                    cmd.CommandText = "select OrNo from [" + "order" + "] where OrNo=@orno";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        MessageBox.Show("訂單單號重複", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        strDate = Date.ChangeDateForSN(OrDate.Text);
                        string sql = "select orno from [" + "order" + "] where orno like @date + '%' order by orno desc";
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
                            Countgano = qunotable.Rows[0]["orno"].ToString();
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
                                strOrNo = strDate + Countgano;
                                if (listno.Find(r => r.Field<string>("OrNo") == strOrNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                            else
                            {
                                Countgano = C.ToString();
                                Countgano = Countgano.PadLeft(4, '0');
                                strOrNo = strDate + Countgano;
                                if (listno.Find(r => r.Field<string>("OrNo") == strOrNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                        } while (isRepeat);
                        OrNo.Text = strOrNo;
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
                CuName1.Text = AdPer1.Text = AdTel.Text = EmNo.Text = EmName.Text = "";
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

        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows[0].Cells["訂單數量"].Value.ToDecimal() != dataGridViewT1.SelectedRows[0].Cells["訂單未交量"].Value.ToDecimal())
            {
                MessageBox.Show("此產品已有交貨，無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            gridDelete.Focus();
            int index = 0;
            if (dataGridViewT1.Rows.Count > 0)
            {
                string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].Value.ToString();
                for (int x = 0; x < OrBom.Rows.Count; x++)
                {
                    if (OrBom.Rows[x]["BomRec"].ToString() == rec)
                    {
                        OrBom.Rows.RemoveAt(x--);
                    }
                }
                OrBom.AcceptChanges();

                index = dataGridViewT1.CurrentRow.Index;
                OrD.Rows.RemoveAt(index);
                SetAllMny();

                if (dataGridViewT1.Rows.Count > 0)
                {
                    index = (index > dataGridViewT1.Rows.Count - 1) ? dataGridViewT1.Rows.Count - 1 : index;
                    dataGridViewT1.CurrentCell = dataGridViewT1["產品編號", index];
                    dataGridViewT1.Rows[index].Selected = true;
                }
            }
            dataGridViewT1.Focus();
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1["訂單數量", index].Value.ToDecimal() != dataGridViewT1["訂單未交量", index].Value.ToDecimal())
                {
                    SendKeys.Send("{tab}");
                }
            }
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
                if (OrD.AsEnumerable().All(r => r["itno"].ToString().Trim().Length >= 0))
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    AddRow(index);
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
                frm.dr = OrD.Rows[index];
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
            DataTable table = OrBom.Clone();

            for (int i = 0; i < OrBom.Rows.Count; i++)
            {
                if (OrBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                {
                    table.ImportRow(OrBom.Rows[i]);
                    OrBom.Rows.RemoveAt(i--);
                }
            }

            table.AcceptChanges();
            OrBom.AcceptChanges();

            using (var frm = new subMenuFm_2.FrmAdjust_Bom())
            { 
                frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                frm.table = table.Copy();
                frm.pkey = rec;
                frm.grid = dataGridViewT1;
                frm.FromQuote = true;
                frm.上層Row = OrD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        if (frm.CallBack.ToString() == "Money")
                        {
                            OrD.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MS);
                            dataGridViewT1.InvalidateRow(dataGridViewT1.SelectedRows[0].Index);
                            dataGridViewT1.Focus();
                            OrBom.Merge(frm.table);
                            OrBom.AcceptChanges();
                            SetRow_TaxPrice(OrD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                            SetRow_Mny(OrD.Rows[dataGridViewT1.SelectedRows[0].Index]);
                            SetAllMny();
                        }
                        else
                        {
                            OrBom.Merge(frm.table);
                            OrBom.AcceptChanges();
                        }
                        break;
                    case DialogResult.Cancel:
                        OrBom.Merge(table);
                        OrBom.AcceptChanges();
                        break;
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
            var itno = OrD.Rows[index]["itno"].ToString().Trim();
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
            var itno = OrD.Rows[index]["itno"].ToString().Trim();
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

            if (jOrder.EnableBShopPrice() == false)
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
            var itno = OrD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm進價查詢 frm = new S2.Frm進價查詢())
            {
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
            var itno = OrD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶歷史報價 frm = new S2.Frm該客戶歷史報價())
            {
                frm.cuno = CuNo.Text.Trim();
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
            var itno = (index == -1) ? "" : OrD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm該客戶歷史交易 frm = new S2.Frm該客戶歷史交易())
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
            var itno = OrD.Rows[index]["itno"].ToString().Trim();
            using (S2.Frm所有客戶所有產品 frm = new S2.Frm所有客戶所有產品())
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
            if (OrD.AsEnumerable().Any(r => r["itno"].ToString().Trim() == ""))
            {
                MessageBox.Show("明細尚有產品編號未輸入，請登打完畢後再進行查詢");
                return;
            }

            if (btnAppend.Enabled)
            {
                OrBom.Clear();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@orno", OrNo.Text.Trim());
                    cmd.CommandText = "select * from orderBom where orno=@orno COLLATE Chinese_Taiwan_Stroke_BIN";

                    da.Fill(OrBom);
                }
            }

            gridCost.Focus();
            if (dataGridViewT1.SelectedRows.Count > 0)
            {
                using (S2.Frm成本與毛利分析 frm = new S2.Frm成本與毛利分析(OrDate.Text))
                {
                    frm.dtD = OrD.Copy();
                    frm.bom = OrBom.Copy();
                    frm.date = Date.ToTWDate(OrDate.Text);
                    frm.ShowDialog();
                }
            }
            dataGridViewT1.Focus();
        }

        private void Text_Enter(object sender, EventArgs e)
        {
            //OrNo,FaNo,X3No
            TextBefore = (sender as TextBox).Text;
        }



        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToTxt(Common.load("Top", "[order]", "orno"));

        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var row = Common.load("Prior", "[order]", "orno", OrNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CPrior", "[order]", "orno", OrNo.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

            }
            WriteToTxt(row);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var row = Common.load("Next", "[order]", "orno", OrNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "[order]", "orno", OrNo.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

            }
            WriteToTxt(row);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToTxt(Common.load("Bottom", "[order]", "orno", OrNo.Text.Trim()));
        }

        void WhenAppend()
        {
            EsDate.Text = OrDate.Text = Date.GetDateTime(Common.User_DateTime);

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
            tempNo = OrNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            Xa1Par.ReadOnly = (Common.Series == "74" || Common.Series == "73");
            orpickingflag.Checked = ortrnflag.Checked = oroverflag.Checked = false;

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前售價.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.訂單未交量.ReadOnly = true;
            this.未入庫數量.ReadOnly = true;
            this.存量預估.ReadOnly = true;
            this.型號.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            this.BomRec = 0;

            OrD.Clear();
            loadBom();

            T.Clear();
            DataRow tr = T.NewRow();
            T.Rows.Add(tr);
            T.AcceptChanges();
            memo1 = "";
            WhenAppend();
            OrDate.Focus();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            btnState = "Duplicate";
            tempNo = OrNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            Xa1Par.ReadOnly = (Common.Series == "74" || Common.Series == "73");
            orpickingflag.Checked = ortrnflag.Checked = oroverflag.Checked = false;

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前售價.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.訂單未交量.ReadOnly = true;
            this.未入庫數量.ReadOnly = true;
            this.存量預估.ReadOnly = true;
            this.型號.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            loadBom();

            for (int i = 0; i < OrD.Rows.Count; i++)
            {
                OrD.Rows[i]["qtynotout"] = OrD.Rows[i]["qty"].ToString();
                OrD.Rows[i]["qtynotinstk"] = OrD.Rows[i]["qty"].ToString();
                OrD.Rows[i]["qtyin"] = 0;
                OrD.Rows[i]["qtyout"] = 0;
                OrD.Rows[i]["esdate"] = Date.GetDateTime(1, false);
                OrD.Rows[i]["esdate1"] = Date.GetDateTime(2, false);
            }
            OrNo.Text = "";
            QuNo.Text = "";
            EsDate.Text = OrDate.Text = Date.GetDateTime(Common.User_DateTime, false);
            OrDate.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jOrder.IsExistDocument<JBS.JS.Order>(OrNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jOrder.IsModify<JBS.JS.Order>(OrNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jOrder.upModify1<JBS.JS.Order>(OrNo.Text.Trim());//更新修改狀態1
                WriteToTxt(Common.load("Cancel", "[order]", "orno", OrNo.Text.Trim()));
            }
            btnState = "Modify";
            tempNo = OrNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前售價.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.訂單未交量.ReadOnly = true;
            this.未入庫數量.ReadOnly = true;
            this.存量預估.ReadOnly = true;
            this.型號.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            loadBom();
            OrNo.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jOrder.IsExistDocument<JBS.JS.Order>(OrNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jOrder.IsModify<JBS.JS.Order>(OrNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < OrD.Rows.Count; i++)
            {
                if (OrD.Rows[i]["qtyout"].ToDecimal() != 0)
                {
                    MessageBox.Show("此張訂單已有出貨資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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
                    cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());

                    cmd.CommandText = "delete from [" + "order" + "] where OrNo=@orno";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from orderd where OrNo=@orno";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from orderbom where OrNo=@orno";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"update weborder SET SysOrNo = '',orderState='0' WHERE SysOrNo = @orno";
                    cmd.ExecuteNonQuery();

                    FrmAffixFile.FileDelete_單據刪除(cmd, OrNo.Text.Trim(), "訂貨單");

                    tn.Commit();
                    tn.Dispose();
                    cmd.Dispose();
                    btnNext_Click(null, null);
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
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FrmOrder_Print frm = new FrmOrder_Print();
            frm.PK = OrNo.Text.Trim();
            frm.ShowDialog();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            
            if (OrNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new S1.FrmOrderBrowNew())
            {
                frm.TSeekNo = OrNo.Text.Trim();
                frm.ShowDialog();
                WriteToTxt(Common.load("Check", "[order]", "orno", frm.TResult));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (CuNo.Text.Trim() == "")
            {
                MessageBox.Show("客戶編號不可為空", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CuNo.Focus();
                return;
            }

            bool falg = false;
            if (OrD.AsEnumerable().Count(r => r["itno"].ToString().Trim().Length == 0) > 0)
            {
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    if (dataGridViewT1["產品編號", i].Value.ToString() == "")
                    {
                        if (dataGridViewT1["訂單數量", i].Value.ToString() == "0")
                        {
                            DeleteRow(i);
                            i--;
                        }
                        else
                        {
                            falg = true;
                        }
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
                MessageBox.Show("訂單明細不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnState == "Append" || btnState == "Duplicate")
            {
                #region Append 、 Duplicate
                if (!SetOrNo()) return;
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        SqlCommand cmd = cn.CreateCommand();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                        cmd.Parameters.AddWithValue("ordate", Date.ToTWDate(OrDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("ordate1", Date.ToUSDate(OrDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(EsDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("esdate1", Date.ToUSDate(EsDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuname1", CuName1.Text.Trim());
                        cmd.Parameters.AddWithValue("cuname2", cuname2);
                        cmd.Parameters.AddWithValue("cutel1", AdTel.Text.Trim());
                        cmd.Parameters.AddWithValue("cuper1", AdPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                        cmd.Parameters.AddWithValue("trno", TrNo.Text.Trim());
                        cmd.Parameters.AddWithValue("trname", trname);
                        cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
                        cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
                        cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
                        cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                        cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
                        cmd.Parameters.AddWithValue("taxb", Math.Round(Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
                        cmd.Parameters.AddWithValue("totmnyb", Math.Round(TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("orpayment", OrPayment.Text.Trim());
                        cmd.Parameters.AddWithValue("orperiod", Orperiod.Text.Trim());
                        cmd.Parameters.AddWithValue("ormemo", OrMemo.Text);
                        cmd.Parameters.AddWithValue("ormemo1", memo1);
                        cmd.Parameters.AddWithValue("recordno", OrD.Rows.Count);
                        cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                        cmd.Parameters.AddWithValue("spname", spname);
                        cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("appscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("MeMain", T.Rows[0]["MeMain"].ToString());
                        cmd.Parameters.AddWithValue("MeOther", T.Rows[0]["MeOther"].ToString());
                        cmd.Parameters.AddWithValue("MePrint", T.Rows[0]["MePrint"].ToString());
                        cmd.Parameters.AddWithValue("MeSize", T.Rows[0]["MeSize"].ToString());
                        cmd.Parameters.AddWithValue("MeSize2", T.Rows[0]["MeSize2"].ToString());
                        
                        cmd.Parameters.AddWithValue("AdAddr", AdAddr.Text.ToString().Trim().GetUTF8(60));//指送地址
                        cmd.Parameters.AddWithValue("CardNo", CardNo.Text.Trim().GetUTF8(20));
                        cmd.Parameters.AddWithValue("PhotoPath", PhotoPath);//13.7c
                        
                        if (ortrnflag.Checked)
                            cmd.Parameters.AddWithValue("ortrnflag", 1);
                        else
                            cmd.Parameters.AddWithValue("ortrnflag", 0);
                        if (oroverflag.Checked)
                            cmd.Parameters.AddWithValue("oroverflag", 1);
                        else
                            cmd.Parameters.AddWithValue("oroverflag", 0);
                        if (orpickingflag.Checked)
                            cmd.Parameters.AddWithValue("orpickingflag", 1);
                        else
                            cmd.Parameters.AddWithValue("orpickingflag", 0);

                        cmd.Transaction = tn;
                        cmd.CommandText = "insert into [" + "order" + "] ("
                        + " orno,ordate,ordate1,esdate,esdate1,quno"
                            //+",cono,coname1,coname2,taxmnyf,usrno"
                        + " ,cuno,cuname1,cuname2,cutel1,cuper1,emno,emname,xa1no,xa1name,xa1par"
                        + " ,trno,trname,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,orpayment,orperiod,ormemo,ormemo1"
                        + " ,recordno,spno,spname,appdate,edtdate,appscno,edtscno,MeMain,MeOther,MePrint,MeSize,MeSize2,ortrnflag,oroverflag,AdAddr,CardNo,PhotoPath,orpickingflag)values("
                        + " @orno,@ordate,@ordate1,@esdate,@esdate1,@quno"
                        + " ,@cuno,@cuname1,@cuname2,@cutel1,@cuper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
                        + " ,@trno,@trname,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@orpayment,@orperiod,@ormemo,@ormemo1"
                        + " ,@recordno,@spno,@spname,@appdate,@edtdate,@appscno,@edtscno,@MeMain,@MeOther,@MePrint,@MeSize,@MeSize2,@ortrnflag,@oroverflag,@AdAddr,@CardNo,@PhotoPath,@orpickingflag)";

                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < OrD.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                            cmd.Parameters.AddWithValue("ordate", Date.ToTWDate(OrDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("ordate1", Date.ToUSDate(OrDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(dataGridViewT1["交貨日期", i].Value.ToString()));
                            cmd.Parameters.AddWithValue("esdate1", Date.ToUSDate(dataGridViewT1["交貨日期", i].Value.ToString()));
                            cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("qtynotout", OrD.Rows[i]["qtynotout"].ToString());
                            cmd.Parameters.AddWithValue("qtynotinstk", OrD.Rows[i]["qtynotinstk"].ToString());
                            cmd.Parameters.AddWithValue("qtyout", OrD.Rows[i]["qtyout"].ToString());
                            cmd.Parameters.AddWithValue("qtyin", OrD.Rows[i]["qtyin"].ToString());
                            cmd.Parameters.AddWithValue("stkqtyflag", OrD.Rows[i]["stkqtyflag"].ToString());
                            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                            cmd.Parameters.AddWithValue("trno", TrNo.Text.Trim());
                            cmd.Parameters.AddWithValue("itno", OrD.Rows[i]["itno"]);
                            cmd.Parameters.AddWithValue("itname", OrD.Rows[i]["itname"]);
                            cmd.Parameters.AddWithValue("ittrait", OrD.Rows[i]["ittrait"]);
                            cmd.Parameters.AddWithValue("itunit", OrD.Rows[i]["itunit"]);
                            cmd.Parameters.AddWithValue("punit", OrD.Rows[i]["punit"]);
                            cmd.Parameters.AddWithValue("itpkgqty", OrD.Rows[i]["itpkgqty"]);
                            cmd.Parameters.AddWithValue("qty", OrD.Rows[i]["qty"].ToDecimal());
                            cmd.Parameters.AddWithValue("pqty", OrD.Rows[i]["pqty"].ToDecimal());
                            cmd.Parameters.AddWithValue("price", OrD.Rows[i]["price"].ToDecimal());
                            cmd.Parameters.AddWithValue("priceb", OrD.Rows[i]["priceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("prs", OrD.Rows[i]["prs"].ToDecimal());
                            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                            cmd.Parameters.AddWithValue("taxprice", OrD.Rows[i]["taxprice"].ToDecimal());
                            cmd.Parameters.AddWithValue("taxpriceb", OrD.Rows[i]["taxpriceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("mny", OrD.Rows[i]["mny"].ToDecimal());
                            cmd.Parameters.AddWithValue("mnyb", OrD.Rows[i]["mnyb"].ToDecimal());
                            cmd.Parameters.AddWithValue("memo", OrD.Rows[i]["memo"]);
                            cmd.Parameters.AddWithValue("bomid", OrNo.Text.ToString().Trim() + OrD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", OrD.Rows[i]["BomRec"]);
                            cmd.Parameters.AddWithValue("recordno", (i + 1));
                            cmd.Parameters.AddWithValue("mwidth1", OrD.Rows[i]["mwidth1"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth2", OrD.Rows[i]["mwidth2"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth3", OrD.Rows[i]["mwidth3"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth4", OrD.Rows[i]["mwidth4"].ToDecimal());
                            cmd.Parameters.AddWithValue("pformula", OrD.Rows[i]["pformula"]);
                            cmd.Parameters.AddWithValue("itdesp1", OrD.Rows[i]["itdesp1"]);
                            cmd.Parameters.AddWithValue("itdesp2", OrD.Rows[i]["itdesp2"]);
                            cmd.Parameters.AddWithValue("itdesp3", OrD.Rows[i]["itdesp3"]);
                            cmd.Parameters.AddWithValue("itdesp4", OrD.Rows[i]["itdesp4"]);
                            cmd.Parameters.AddWithValue("itdesp5", OrD.Rows[i]["itdesp5"]);
                            cmd.Parameters.AddWithValue("itdesp6", OrD.Rows[i]["itdesp6"]);
                            cmd.Parameters.AddWithValue("itdesp7", OrD.Rows[i]["itdesp7"]);
                            cmd.Parameters.AddWithValue("itdesp8", OrD.Rows[i]["itdesp8"]);
                            cmd.Parameters.AddWithValue("itdesp9", OrD.Rows[i]["itdesp9"]);
                            cmd.Parameters.AddWithValue("itdesp10", OrD.Rows[i]["itdesp10"]);
                            cmd.Parameters.AddWithValue("standard", OrD.Rows[i]["standard"]);
                            if (ortrnflag.Checked)
                                cmd.Parameters.AddWithValue("ortrnflag", 1);
                            else
                                cmd.Parameters.AddWithValue("ortrnflag", 0);

                            cmd.Parameters.AddWithValue("Adper1", OrD.Rows[i]["Adper1"].ToString().Trim().GetUTF8(10));//指送負責人
                            cmd.Parameters.AddWithValue("Adtel", OrD.Rows[i]["Adtel"].ToString().Trim().GetUTF8(20));//指送電話
                            if (OrD.Rows[i]["AdAddr"].ToString().Trim().GetUTF8(60) == "")
                                cmd.Parameters.AddWithValue("AdAddr", AdAddr.Text.Trim().GetUTF8(60));//指送地址
                            else
                                cmd.Parameters.AddWithValue("AdAddr", OrD.Rows[i]["AdAddr"].ToString().Trim().GetUTF8(60));//指送地址
                            cmd.Parameters.AddWithValue("AdName", OrD.Rows[i]["AdName"].ToString().Trim().GetUTF8(50));

                            cmd.CommandText = "insert into orderd("
                                + " orno,ordate,ordate1,esdate,esdate1,quno"
                                + " ,qtynotout,qtynotinstk,qtyout,qtyin,stkqtyflag"
                                //+" ,lowzero,sltflag,extflag,stName"
                                + " ,cuno,emno,xa1no,xa1par,trno,itno,itname,ittrait,itunit,punit,itpkgqty"
                                + " ,qty,pqty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo"
                                + " ,bomid,bomrec,recordno,mwidth1,mwidth2,mwidth3,mwidth4,pformula"
                                + " ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5,standard"
                                + " ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10,ortrnflag,Adper1,Adtel,AdAddr,AdName)values("
                                + " @orno,@ordate,@ordate1,@esdate,@esdate1,@quno"
                                + " ,@qtynotout,@qtynotinstk,@qtyout,@qtyin,@stkqtyflag"
                                + " ,@cuno,@emno,@xa1no,@xa1par,@trno,@itno,@itname,@ittrait,@itunit,@punit,@itpkgqty"
                                + " ,@qty,@pqty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo"
                                + " ,@bomid,@bomrec,@recordno,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@pformula"
                                + " ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5,@standard"
                                + " ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@ortrnflag,@Adper1,@Adtel,@AdAddr,@AdName)";

                            cmd.ExecuteNonQuery();
                        }

                        for (int i = 0; i < OrBom.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("orno", OrNo.Text);
                            cmd.Parameters.AddWithValue("bomid", OrNo.Text + OrBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", OrBom.Rows[i]["BomRec"].ToString());
                            cmd.Parameters.AddWithValue("itno", OrBom.Rows[i]["itno"].ToString());
                            cmd.Parameters.AddWithValue("itname", OrBom.Rows[i]["itname"].ToString());
                            cmd.Parameters.AddWithValue("itunit", OrBom.Rows[i]["itunit"].ToString());
                            cmd.Parameters.AddWithValue("itqty", OrBom.Rows[i]["itqty"].ToString());
                            cmd.Parameters.AddWithValue("itpareprs", OrBom.Rows[i]["itpareprs"].ToString());
                            cmd.Parameters.AddWithValue("itpkgqty", OrBom.Rows[i]["itpkgqty"].ToString());
                            cmd.Parameters.AddWithValue("itrec", (i + 1));
                            cmd.Parameters.AddWithValue("itprice", OrBom.Rows[i]["itprice"].ToString());
                            cmd.Parameters.AddWithValue("itprs", 1);
                            cmd.Parameters.AddWithValue("itmny", OrBom.Rows[i]["itmny"].ToString());
                            cmd.Parameters.AddWithValue("itnote", OrBom.Rows[i]["itnote"].ToString());
                            cmd.CommandText = "insert into orderbom ("
                                + "orno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                                //+ "itsource,itbuypri,itbuymny,"
                                + "itprice,itprs,itmny,itnote)values("
                                + "@orno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,"
                                + "@itprice,@itprs,@itmny,@itnote)";

                            cmd.ExecuteNonQuery();
                        }
                        FrmAffixFile.FileSave_單據存檔(DaFile, cmd, OrNo.Text, "訂貨單");
                        tn.Commit();
                        tn.Dispose();
                        cmd.Dispose();
                        tempNo = OrNo.Text.Trim();
                        if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            FrmOrder_Print frm = new FrmOrder_Print();
                            frm.PK = OrNo.Text.Trim();
                            frm.ShowDialog();

                        }
                        BomRec = 0;
                        btnAppend_Click(null, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    tn.Rollback();

                }
#endregion
            }

            if (btnState == "Modify")
            {
                #region Modify
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Transaction = tn;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("orno", tempNo);
                        cmd.CommandText = "delete from [" + "order" + "] where orno=@orno";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                        cmd.Parameters.AddWithValue("ordate", Date.ToTWDate(OrDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("ordate1", Date.ToUSDate(OrDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(EsDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("esdate1", Date.ToUSDate(EsDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("cuname1", CuName1.Text.Trim());
                        cmd.Parameters.AddWithValue("cuname2", cuname2);
                        cmd.Parameters.AddWithValue("cutel1", AdTel.Text.Trim());
                        cmd.Parameters.AddWithValue("cuper1", AdPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("emname", EmName.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1name", Xa1Name.Text.Trim());
                        cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                        cmd.Parameters.AddWithValue("trno", TrNo.Text.Trim());
                        cmd.Parameters.AddWithValue("trname", trname);
                        cmd.Parameters.AddWithValue("taxmnyb", TaxMnyB.Text.Trim());
                        cmd.Parameters.AddWithValue("taxmny", TaxMny.Text.Trim());
                        cmd.Parameters.AddWithValue("x3no", X3No.Text.Trim());
                        cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                        cmd.Parameters.AddWithValue("tax", Tax.Text.Trim());
                        cmd.Parameters.AddWithValue("taxb", Math.Round(Tax.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("totmny", TotMny.Text.Trim());
                        cmd.Parameters.AddWithValue("totmnyb", Math.Round(TotMny.Text.ToDecimal() * Xa1Par.Text.ToDecimal(), Common.M, MidpointRounding.AwayFromZero));
                        cmd.Parameters.AddWithValue("orpayment", OrPayment.Text.Trim());
                        cmd.Parameters.AddWithValue("orperiod", Orperiod.Text.Trim());
                        cmd.Parameters.AddWithValue("ormemo", OrMemo.Text);
                        cmd.Parameters.AddWithValue("ormemo1", memo1);
                        cmd.Parameters.AddWithValue("recordno", OrD.Rows.Count);
                        cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                        cmd.Parameters.AddWithValue("spname", spname);
                        cmd.Parameters.AddWithValue("appdate", T.Rows[0]["AppDate"].ToString());
                        cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("appscno", T.Rows[0]["AppScNo"].ToString());
                        cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("MeMain", T.Rows[0]["MeMain"].ToString());
                        cmd.Parameters.AddWithValue("MeOther", T.Rows[0]["MeOther"].ToString());
                        cmd.Parameters.AddWithValue("MePrint", T.Rows[0]["MePrint"].ToString());
                        cmd.Parameters.AddWithValue("MeSize", T.Rows[0]["MeSize"].ToString());
                        cmd.Parameters.AddWithValue("MeSize2", T.Rows[0]["MeSize2"].ToString());
                        cmd.Parameters.AddWithValue("NetNo", NetNo);
                        cmd.Parameters.AddWithValue("AdAddr", AdAddr.Text.ToString().Trim().GetUTF8(60));//指送地址
                        cmd.Parameters.AddWithValue("CardNo", CardNo.Text.Trim().GetUTF8(20));
                        cmd.Parameters.AddWithValue("PhotoPath", PhotoPath);//13.7c
                        if (ortrnflag.Checked)
                            cmd.Parameters.AddWithValue("ortrnflag", 1);
                        else
                            cmd.Parameters.AddWithValue("ortrnflag", 0);
                        if (oroverflag.Checked)
                            cmd.Parameters.AddWithValue("oroverflag", 1);
                        else
                            cmd.Parameters.AddWithValue("oroverflag", 0);
                        if (orpickingflag.Checked)
                            cmd.Parameters.AddWithValue("orpickingflag", 1);
                        else
                            cmd.Parameters.AddWithValue("orpickingflag", 0);

                        cmd.Transaction = tn;
                        cmd.CommandText = "insert into [" + "order" + "] ("
                        + " orno,ordate,ordate1,esdate,esdate1,quno"
                            //+",cono,coname1,coname2,taxmnyf,usrno"
                        + " ,cuno,cuname1,cuname2,cutel1,cuper1,emno,emname,xa1no,xa1name,xa1par"
                        + " ,trno,trname,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,orpayment,orperiod,ormemo,ormemo1"
                        + " ,recordno,spno,spname,appdate,edtdate,appscno,edtscno,MeMain,MeOther,MePrint,MeSize,MeSize2,ortrnflag,oroverflag,AdAddr,NetNo,CardNo,PhotoPath,orpickingflag)values("
                        + " @orno,@ordate,@ordate1,@esdate,@esdate1,@quno"
                        + " ,@cuno,@cuname1,@cuname2,@cutel1,@cuper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
                        + " ,@trno,@trname,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@orpayment,@orperiod,@ormemo,@ormemo1"
                        + " ,@recordno,@spno,@spname,@appdate,@edtdate,@appscno,@edtscno,@MeMain,@MeOther,@MePrint,@MeSize,@MeSize2,@ortrnflag,@oroverflag,@AdAddr,@NetNo,@CardNo,@PhotoPath,@orpickingflag)";

                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("orno", tempNo);
                        cmd.CommandText = "delete from orderd where orno=@orno";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < OrD.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                            cmd.Parameters.AddWithValue("ordate", Date.ToTWDate(OrDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("ordate1", Date.ToUSDate(OrDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(dataGridViewT1["交貨日期", i].Value.ToString()));
                            cmd.Parameters.AddWithValue("esdate1", Date.ToUSDate(dataGridViewT1["交貨日期", i].Value.ToString()));
                            cmd.Parameters.AddWithValue("quno", QuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("qtynotout", OrD.Rows[i]["qtynotout"].ToDecimal());
                            cmd.Parameters.AddWithValue("qtynotinstk", OrD.Rows[i]["qtynotinstk"].ToDecimal());
                            cmd.Parameters.AddWithValue("qtyout", OrD.Rows[i]["qtyout"].ToDecimal());
                            cmd.Parameters.AddWithValue("qtyin", OrD.Rows[i]["qtyin"].ToDecimal());
                            cmd.Parameters.AddWithValue("stkqtyflag", OrD.Rows[i]["stkqtyflag"].ToDecimal());
                            cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.ToDecimal());
                            cmd.Parameters.AddWithValue("trno", TrNo.Text.Trim());
                            cmd.Parameters.AddWithValue("itno", OrD.Rows[i]["itno"]);
                            cmd.Parameters.AddWithValue("itname", OrD.Rows[i]["itname"]);
                            cmd.Parameters.AddWithValue("ittrait", OrD.Rows[i]["ittrait"]);
                            cmd.Parameters.AddWithValue("itunit", OrD.Rows[i]["itunit"]);
                            cmd.Parameters.AddWithValue("punit", OrD.Rows[i]["punit"]);
                            cmd.Parameters.AddWithValue("itpkgqty", OrD.Rows[i]["itpkgqty"].ToDecimal());
                            cmd.Parameters.AddWithValue("qty", OrD.Rows[i]["qty"].ToDecimal());
                            cmd.Parameters.AddWithValue("pqty", OrD.Rows[i]["pqty"].ToDecimal());
                            cmd.Parameters.AddWithValue("price", OrD.Rows[i]["price"].ToDecimal());
                            cmd.Parameters.AddWithValue("priceb", OrD.Rows[i]["priceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("prs", OrD.Rows[i]["prs"].ToDecimal());
                            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3").ToDecimal());
                            cmd.Parameters.AddWithValue("taxprice", OrD.Rows[i]["taxprice"].ToDecimal());
                            cmd.Parameters.AddWithValue("taxpriceb", OrD.Rows[i]["taxpriceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("mny", OrD.Rows[i]["mny"].ToDecimal());
                            cmd.Parameters.AddWithValue("mnyb", OrD.Rows[i]["mnyb"].ToDecimal());
                            cmd.Parameters.AddWithValue("memo", OrD.Rows[i]["memo"]);
                            cmd.Parameters.AddWithValue("bomid", OrNo.Text.ToString().Trim() + OrD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", OrD.Rows[i]["BomRec"].ToDecimal());
                            cmd.Parameters.AddWithValue("recordno", (i + 1).ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth1", OrD.Rows[i]["mwidth1"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth2", OrD.Rows[i]["mwidth2"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth3", OrD.Rows[i]["mwidth3"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth4", OrD.Rows[i]["mwidth4"].ToDecimal());
                            cmd.Parameters.AddWithValue("pformula", OrD.Rows[i]["pformula"].ToDecimal());
                            cmd.Parameters.AddWithValue("itdesp1", OrD.Rows[i]["itdesp1"].ToString());
                            cmd.Parameters.AddWithValue("itdesp2", OrD.Rows[i]["itdesp2"].ToString());
                            cmd.Parameters.AddWithValue("itdesp3", OrD.Rows[i]["itdesp3"].ToString());
                            cmd.Parameters.AddWithValue("itdesp4", OrD.Rows[i]["itdesp4"].ToString());
                            cmd.Parameters.AddWithValue("itdesp5", OrD.Rows[i]["itdesp5"].ToString());
                            cmd.Parameters.AddWithValue("itdesp6", OrD.Rows[i]["itdesp6"].ToString());
                            cmd.Parameters.AddWithValue("itdesp7", OrD.Rows[i]["itdesp7"].ToString());
                            cmd.Parameters.AddWithValue("itdesp8", OrD.Rows[i]["itdesp8"].ToString());
                            cmd.Parameters.AddWithValue("itdesp9", OrD.Rows[i]["itdesp9"].ToString());
                            cmd.Parameters.AddWithValue("itdesp10", OrD.Rows[i]["itdesp10"].ToString());
                            cmd.Parameters.AddWithValue("standard", OrD.Rows[i]["standard"].ToString());
                            if (ortrnflag.Checked)
                                cmd.Parameters.AddWithValue("ortrnflag", 1);
                            else
                                cmd.Parameters.AddWithValue("ortrnflag", 0);

                            cmd.Parameters.AddWithValue("Adper1", OrD.Rows[i]["Adper1"].ToString().Trim().GetUTF8(10));//指送負責人
                            cmd.Parameters.AddWithValue("Adtel", OrD.Rows[i]["Adtel"].ToString().Trim().GetUTF8(20));//指送電話
                            if (OrD.Rows[i]["AdAddr"].ToString().Trim().GetUTF8(60) == "")
                                cmd.Parameters.AddWithValue("AdAddr", AdAddr.Text.Trim().GetUTF8(60));//指送地址
                            else
                                cmd.Parameters.AddWithValue("AdAddr", OrD.Rows[i]["AdAddr"].ToString().Trim().GetUTF8(60));//指送地址
                            cmd.Parameters.AddWithValue("AdName", OrD.Rows[i]["AdName"].ToString().Trim().GetUTF8(50));

                            cmd.CommandText = "insert into orderd("
                                + " orno,ordate,ordate1,esdate,esdate1,quno"
                                + " ,qtynotout,qtynotinstk,qtyout,qtyin,stkqtyflag"
                                //+" ,lowzero,sltflag,extflag,stName"
                                + " ,cuno,emno,xa1no,xa1par,trno,itno,itname,ittrait,itunit,punit,itpkgqty"
                                + " ,qty,pqty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo"
                                + " ,bomid,bomrec,recordno,mwidth1,mwidth2,mwidth3,mwidth4,pformula"
                                + " ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                                + " ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10,ortrnflag,Adper1,Adtel,AdAddr,AdName,standard)values("
                                + " @orno,@ordate,@ordate1,@esdate,@esdate1,@quno"
                                + " ,@qtynotout,@qtynotinstk,@qtyout,@qtyin,@stkqtyflag"
                                + " ,@cuno,@emno,@xa1no,@xa1par,@trno,@itno,@itname,@ittrait,@itunit,@punit,@itpkgqty"
                                + " ,@qty,@pqty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo"
                                + " ,@bomid,@bomrec,@recordno,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@pformula"
                                + " ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                                + " ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10,@ortrnflag,@Adper1,@Adtel,@AdAddr,@AdName,@standard)";

                            cmd.ExecuteNonQuery();
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("orno", tempNo);
                        cmd.CommandText = "delete from orderbom where orno=@orno";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < OrBom.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("orno", OrNo.Text);
                            cmd.Parameters.AddWithValue("bomid", OrNo.Text + OrBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", OrBom.Rows[i]["BomRec"].ToString());
                            cmd.Parameters.AddWithValue("itno", OrBom.Rows[i]["itno"].ToString());
                            cmd.Parameters.AddWithValue("itname", OrBom.Rows[i]["itname"].ToString());
                            cmd.Parameters.AddWithValue("itunit", OrBom.Rows[i]["itunit"].ToString());
                            cmd.Parameters.AddWithValue("itqty", OrBom.Rows[i]["itqty"].ToString());
                            cmd.Parameters.AddWithValue("itpareprs", OrBom.Rows[i]["itpareprs"].ToString());
                            cmd.Parameters.AddWithValue("itpkgqty", OrBom.Rows[i]["itpkgqty"].ToString());
                            cmd.Parameters.AddWithValue("itrec", (i + 1));
                            cmd.Parameters.AddWithValue("itprice", OrBom.Rows[i]["itprice"].ToString());
                            cmd.Parameters.AddWithValue("itprs", 1);
                            cmd.Parameters.AddWithValue("itmny", OrBom.Rows[i]["itmny"].ToString());
                            cmd.Parameters.AddWithValue("itnote", OrBom.Rows[i]["itnote"].ToString());
                            cmd.CommandText = "insert into orderbom ("
                                + "orno,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                                //+ "itsource,itbuypri,itbuymny,"
                                + "itprice,itprs,itmny,itnote)values("
                                + "@orno,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,"
                                + "@itprice,@itprs,@itmny,@itnote)";

                            cmd.ExecuteNonQuery();
                        }
                        tn.Commit();
                        tn.Dispose();
                        cmd.Dispose();
                        tempNo = OrNo.Text.Trim();
                        if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            using (FrmOrder_Print frm = new FrmOrder_Print())
                            {
                                frm.PK = OrNo.Text.Trim();
                                frm.ShowDialog();
                            }
                        }
                        jOrder.upModify0<JBS.JS.Order>(OrNo.Text.Trim());//更新修改狀態0
                        BomRec = 0;
                        btnAppend_Click(null, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    tn.Rollback();

                }
                #endregion
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = string.Empty;
            WriteToTxt(Common.load("Cancel", "[order]", "orno", tempNo));
            Common.SetTextState(FormState = FormEditState.None, ref list);
            dataGridViewT1.ReadOnly = true;
            btnAppend.Focus();
            jOrder.upModify0<JBS.JS.Order>(OrNo.Text.Trim());//更新修改狀態0
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        private void bt2_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmOrder_Memo())
            {
                frm.ormemo1 = ormemo1;
                frm.orno = OrNo.Text;
                frm.quno = QuNo.Text;
                frm.ordate = OrDate.Text;
                frm.cuno = CuNo.Text;
                frm.cuname1 = CuName1.Text;
                frm.cuper1 = AdPer1.Text;
                frm.cutel1 = AdTel.Text;
                frm.emno = EmNo.Text;
                frm.emname = EmName.Text;
                frm.xa1no = Xa1No.Text;
                frm.xa1name = Xa1Name.Text;
                frm.xa1par = Xa1Par.Text;
                frm.trno = TrNo.Text;

                frm.MeMain = T.Rows[0]["MeMain"].ToString();
                frm.MeOther = T.Rows[0]["MeOther"].ToString();
                frm.MePrint = T.Rows[0]["MePrint"].ToString();
                frm.MeSize = T.Rows[0]["MeSize"].ToString();
                frm.MeSize2 = T.Rows[0]["MeSize2"].ToString();

                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        T.Rows[0]["MeMain"] = frm.MeMain;
                        T.Rows[0]["MeOther"] = frm.MeOther;
                        T.Rows[0]["MePrint"] = frm.MePrint;
                        T.Rows[0]["MeSize"] = frm.MeSize;
                        T.Rows[0]["MeSize2"] = frm.MeSize2;
                        break;
                }
            }
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = CuNo.ReadOnly ? false : true;
                frm.memo1 = memo1;

                if (frm.ShowDialog() == DialogResult.OK) memo1 = frm.memo1;
            }
        }



        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            jOrder.Open<JBS.JS.Cust>(sender, reader =>
            {
                CuNo.Text = reader["cuno"].ToString().Trim();
                CuName1.Text = reader["cuname1"].ToString();
                cuname2 = reader["cuname2"].ToString();//0606
                AdPer1.Text = reader["cuper1"].ToString().GetUTF8(10);
                AdTel.Text = reader["cutel1"].ToString();
                X3No.Text = reader["cux3no"].ToString();
                EmNo.Text = reader["cuemno1"].ToString();
                Xa1No.Text = reader["cuxa1no"].ToString();
                this.Disc = reader["CuDisc"].ToDecimal();
                SpNo.Text = reader["spno"].ToString();
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);

                if (this.FormState != FormEditState.Modify)
                {
                    for (int i = 0; i < OrD.Rows.Count; i++)
                    {
                        Common.GetSpecialPrice(OrD.Rows[i], i, CuNo, OrDate, dataGridViewT1, GetSystemPrice);
                        SetRow_TaxPrice(OrD.Rows[i]);
                        SetRow_Mny(OrD.Rows[i]);
                        dataGridViewT1.InvalidateRow(i);
                    }
                    SetAllMny();
                }
                this.TextBefore = reader["cuno"].ToString().Trim();

                填入指送地址資訊(reader);
            });
            rowstandard(null, 0);
        }

        private void 填入指送地址資訊(SqlDataReader row)//如果有預設列印，即列印指派地址;否則列印CUST之資料
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("cuno", row["cuno"]);
                cmd.CommandText = "select * from DeliveryAddress where cuno=@cuno and DefaultPrint = 'V'";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        AdPer1.Text = dr["per1"].ToString().GetUTF8(10);
                        AdTel.Text = dr["Tel"].ToString();
                        AdAddr.Text = dr["Addr"].ToString();
                        AdName = dr["name"].ToString();       //指送公司名稱
                        //labelT1.Text = "指送地址";
                    }
                    else
                    {
                        AdPer1.Text = row["CuPer1"].ToString().GetUTF8(10);
                        AdTel.Text = row["CuTel1"].ToString();
                        AdAddr.Text = row["cuaddr1"].ToString();
                        AdName = row["cuname2"].ToString();
                        //labelT1.Text = "公司地址";
                    }
                }
            }
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly || btnCancel.Focused)
                return;

            if (CuNo.Text == "")
            {
                e.Cancel = true;
                CuName1.Text = AdPer1.Text = AdTel.Text = EmNo.Text = EmName.Text = "";
                MessageBox.Show("請先輸入客戶編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            jOrder.ValidateOpen<JBS.JS.Cust>(sender, e, reader =>
            {
                if (CuNo.Text.Trim() == TextBefore)
                    return;

                CuNo.Text = reader["cuno"].ToString().Trim();
                CuName1.Text = reader["cuname1"].ToString();
                AdPer1.Text = reader["cuper1"].ToString().GetUTF8(10);
                AdTel.Text = reader["cutel1"].ToString();
                cuname2 = reader["cuname2"].ToString();
                X3No.Text = reader["CUX3No"].ToString();
                EmNo.Text = reader["cuemno1"].ToString();
                Xa1No.Text = reader["cuxa1no"].ToString();
                SpNo.Text = reader["spno"].ToString();
                this.Disc = reader["CuDisc"].ToDecimal();
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);
                if (this.FormState != FormEditState.Modify)
                {
                    for (int i = 0; i < OrD.Rows.Count; i++)
                    {
                        Common.GetSpecialPrice(OrD.Rows[i], i, CuNo, OrDate, dataGridViewT1, GetSystemPrice);
                        SetRow_TaxPrice(OrD.Rows[i]);
                        SetRow_Mny(OrD.Rows[i]);
                        dataGridViewT1.InvalidateRow(i);
                    }
                    SetAllMny();
                }

                this.TextBefore = reader["cuno"].ToString().Trim();

                填入指送地址資訊(reader);
            });
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jOrder.Open<JBS.JS.Empl>(sender, row =>
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

            jOrder.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        private void TrNo_DoubleClick(object sender, EventArgs e)
        {
            jOrder.Open<JBS.JS.Trade>(sender, row =>
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

            jOrder.ValidateOpen<JBS.JS.Trade>(sender, e, row =>
            {
                TrNo.Text = row["TrNo"].ToString().Trim();
                this.trname = row["TrName"].ToString().Trim();
            });
        }


        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            jOrder.Open<JBS.JS.Spec>(sender, row =>
            {
                SpNo.Text = row["SpNo"].ToString().Trim();
                this.spname = row["spname"].ToString().Trim();
            });
        }
        private void SpNo_Validating(object sender, CancelEventArgs e)
        {
            if (SpNo.ReadOnly || btnCancel.Focused)
                return;

            if (SpNo.TrimTextLenth() == 0)
            {
                SpNo.Clear();
                this.spname = "";
                return;
            }

            jOrder.ValidateOpen<JBS.JS.Spec>(sender, e, row =>
            {
                SpNo.Text = row["SpNo"].ToString().Trim();
                this.spname = row["spname"].ToString().Trim();
            });
        }


        private void OrPayment_DoubleClick(object sender, EventArgs e)
        { 
            using (var frm = new SOther.FrmPayNoteBrow())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        OrPayment.Text = frm.TResult;
                        break;
                }
            }
        }

        private void Orperiod_DoubleClick(object sender, EventArgs e)
        { 
            using (var frm = new SOther.FrmPerNote())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        Orperiod.Text = frm.TResult;
                        break;
                }
            }
        }

        private void OrMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(OrMemo, 60);
        }

        private void QuNo_DoubleClick(object sender, EventArgs e)
        {
            if (QuNo.ReadOnly)
                return;

            QuNo.CausesValidation = false;

            using (var frm = new FrmQuoteToSale())
            { 
                frm.CuNo = CuNo.Text.Trim();
                frm.SeekNo = QuNo.Text.Trim();

                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                jOrder.RemoveEmptyRowOnSaving(dataGridViewT1, ref OrD, ref OrBom, SetAllMny);

                this.PassQuoteM(frm);

                this.PassQuoteD(frm);
            }

            QuNo.Tag = QuNo.Text.Trim();
            SetAllMny();

            QuNo.CausesValidation = true;
        }
        private void QuNo_Validating(object sender, CancelEventArgs e)
        {
            if (QuNo.ReadOnly)
                return;

            if (btnCancel.Focused)
                return;

            if (QuNo.TrimTextLenth() == 0)
            {
                QuNo.Tag = "";
                return;
            }

            if (QuNo.Tag == null)
                QuNo.Tag = "";

            if (jOrder.IsExistDocument<JBS.JS.Quote>(QuNo.Text.Trim()) == true)
            {
                //已經開過窗了, 之後就不開
                if (QuNo.Tag.ToString() == QuNo.Text.Trim())
                    return;

                using (var frm = new FrmQuoteToSale())
                { 
                    frm.CuNo = CuNo.Text.Trim();
                    frm.SeekNo = QuNo.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jOrder.RemoveEmptyRowOnSaving(dataGridViewT1, ref OrD, ref OrBom, SetAllMny);

                    this.PassQuoteM(frm);

                    this.PassQuoteD(frm);
                }
                QuNo.Tag = QuNo.Text.Trim();
                SetAllMny();
            }
            else
            {
                e.Cancel = true;
                using (var frm = new FrmQuoteToSale())
                { 
                    frm.CuNo = CuNo.Text.Trim();
                    frm.SeekNo = QuNo.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jOrder.RemoveEmptyRowOnSaving(dataGridViewT1, ref OrD, ref OrBom, SetAllMny);

                    this.PassQuoteM(frm);

                    this.PassQuoteD(frm);
                }
                QuNo.Tag = QuNo.Text.Trim();
                SetAllMny();
            }
        }
        private void PassQuoteM(FrmQuoteToSale frm)
        {
            QuNo.Text = frm.QuNo.Trim();
            //EsDate.Text = Common.User_DateTime == 1 ? frm.MasterRow["qudates"].ToString().Trim() : frm.MasterRow["qudates1"].ToString().Trim();
            EmNo.Text = frm.MasterRow["emno"].ToString().Trim();
            EmName.Text = frm.MasterRow["EmName"].ToString().Trim();
            CuNo.Text = frm.MasterRow["cuno"].ToString();
            CuName1.Text = frm.MasterRow["CuName1"].ToString();
            AdPer1.Text = frm.MasterRow["CuPer1"].ToString().GetUTF8(10);
            AdTel.Text = frm.MasterRow["CuTel1"].ToString();
            Xa1No.Text = frm.MasterRow["Xa1No"].ToString();
            Xa1Name.Text = frm.MasterRow["Xa1Name"].ToString();
            Xa1Par.Text = frm.MasterRow["Xa1Par"].ToString();
            TaxMny.Text = frm.MasterRow["TaxMny"].ToDecimal().ToString("f" + Common.MST);
            TaxMnyB.Text = frm.MasterRow["TaxMnyB"].ToDecimal().ToString("f" + Common.M);
            TrNo.Text = frm.MasterRow["TrNo"].ToString();
            trname = frm.MasterRow["trname"].ToString();
            X3No.Text = frm.MasterRow["X3No"].ToString();
            Rate.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
            OrPayment.Text = frm.MasterRow["quPayment"].ToString();
            Orperiod.Text = frm.MasterRow["quperiod"].ToString();
            OrMemo.Text = frm.MasterRow["quMemo"].ToString();
            Tax.Text = frm.MasterRow["Tax"].ToDecimal().ToString("f" + Common.TS);
            TotMny.Text = frm.MasterRow["TotMny"].ToDecimal().ToString("f" + Common.MST);
            PhotoPath = frm.MasterRow["PhotoPath"].ToString(); //13.7c
            this.memo1 = frm.MasterRow["qumemo1"].ToString();//製單人員
            AdAddr.Text = SQL.ExecuteScalar("select cuaddr" + Common.Sys_DefaultAddr + "  from cust where cuno = @cuno ", new parameters("cuno", CuNo.Text));
            jOrder.Validate<JBS.JS.Cust>(CuNo.Text, row =>
            {
                this.Disc = row["CuDisc"].ToDecimal("f4");
            });

            jOrder.Validate<JBS.JS.XX03>(X3No.Text, row =>
            {
                X3No.Text = row["x3no"].ToString();
                X3Name.Text = row["x3name"].ToString();
            });

        }
        private void PassQuoteD(FrmQuoteToSale frm)
        {
            OrD.Clear();
            OrBom.Clear();

            DataRow row = null;
            DataTable dtD = frm.dtDetail;

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                var rec = GetBomRec();
                row = OrD.NewRow();
                row["quno"] = frm.QuNo.Trim();
                row["itno"] = dtD.Rows[i]["itno"].ToString();
                row["itname"] = dtD.Rows[i]["itname"].ToString();
                row["ittrait"] = dtD.Rows[i]["ittrait"].ToString();
                row["itnoudf"] = dtD.Rows[i]["itnoudf"].ToString();
                if (row["ittrait"].ToString().Trim() == "1")
                    row["產品組成"] = "組合品";
                if (row["ittrait"].ToString().Trim() == "2")
                    row["產品組成"] = "組裝品";
                if (row["ittrait"].ToString().Trim() == "3")
                    row["產品組成"] = "單一商品";
                row["itunit"] = dtD.Rows[i]["itunit"].ToString();
                row["punit"] = getPUnit(dtD.Rows[i]["itno"].ToString().Trim());
                row["itpkgqty"] = dtD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q);
                row["qty"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                row["pqty"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                row["price"] = dtD.Rows[i]["price"].ToDecimal("f" + Common.MS);
                row["prs"] = dtD.Rows[i]["prs"].ToDecimal("f3");
                row["taxprice"] = dtD.Rows[i]["taxprice"].ToDecimal("f6");
                row["mny"] = dtD.Rows[i]["mny"].ToDecimal("f" + Common.TPS);
                row["priceb"] = dtD.Rows[i]["priceb"].ToDecimal("f" + Common.M);
                row["taxpriceb"] = dtD.Rows[i]["taxpriceb"].ToDecimal("f6");
                row["mnyb"] = dtD.Rows[i]["mnyb"].ToDecimal("f" + Common.M);
                row["qtynotout"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q); ;//訂單未交量
                row["qtynotinstk"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q); ;//訂單未入庫
                row["qtyout"] = 0;
                row["qtyin"] = 0;
                
                row["esdate"] = Date.ToTWDate(EsDate.Text.Trim());
                row["esdate1"] = Date.ToUSDate(EsDate.Text.Trim());
                row["standard"] = dtD.Rows[i]["standard"].ToString();
                if (row["ittrait"].ToString().Trim() != "3")
                {
                    row["stkqtyflag"] = 1;
                    row["存量預估"] = "展開";
                }
                else
                {
                    row["stkqtyflag"] = 2;
                    row["存量預估"] = "不展開";
                }
                row["BomRec"] = rec;
                row["memo"] = dtD.Rows[i]["memo"].ToString();
                for (int j = 1; j <= 10; j++)
                {
                    row["itdesp" + j] = dtD.Rows[i]["itdesp" + j].ToString();
                }
                OrD.Rows.Add(row);
                OrD.AcceptChanges();
                dataGridViewT1.InvalidateRow(OrD.Rows.Count - 1);

                if (row["ittrait"].ToString().Trim() == "3")
                    continue;

                var qubomid = dtD.Rows[i]["bomid"].ToString().Trim();
                jOrder.GetTBom<JBS.JS.Quote>(qubomid, rec.ToString(), ref OrBom);
            }
        }

        string getPUnit(string itno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("itno", itno);
                cmd.CommandText = "select punit from item where itno=@itno";
                if (cmd.ExecuteScalar().IsNullOrEmpty())
                    return "";
                else
                    return cmd.ExecuteScalar().ToString();
            }
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jOrder.Open<JBS.JS.XX03>(sender, row =>
            {
                X3No.Text = row["X3No"].ToString().Trim();
                X3Name.Text = row["X3Name"].ToString();
                decimal _rate = 0;
                decimal.TryParse(row["X3Rate"].ToString(), out _rate);
                _rate = _rate * 100;
                Rate.Text = _rate.ToString("f0");

                for (int i = 0; i < OrD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(OrD.Rows[i]);
                    SetRow_Mny(OrD.Rows[i]);
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

            jOrder.ValidateOpen<JBS.JS.XX03>(sender, e, row =>
            {
                if (X3No.Text.Trim() == TextBefore)
                    return;

                X3No.Text = row["X3No"].ToString().Trim();
                X3Name.Text = row["X3Name"].ToString();
                decimal rate = row["X3Rate"].ToString().ToDecimal() * 100;
                Rate.Text = rate.ToString("f" + Rate.LastNum);

                for (int i = 0; i < OrD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(OrD.Rows[i]);
                    SetRow_Mny(OrD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["X3No"].ToString().Trim();
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

        private void OrNo_DoubleClick(object sender, EventArgs e)
        {
            if (OrNo.ReadOnly)
                return;

            using (var frm = new FrmOrder_Print_OrNo())
            {
                frm.TSeekNo = OrNo.Text.Trim();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    OrNo.Text = frm.TResult;
                }
            }
        }

        private void OrNo_Validating(object sender, CancelEventArgs e)
        {
            if (OrNo.ReadOnly || btnCancel.Focused) return;

            if (OrNo.Text.Length > 0 && OrNo.Text.Trim() == "")
            {
                e.Cancel = true;
                OrNo.Text = "";
                OrNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnState == "Append")
            {
                if (jOrder.IsExistDocument<JBS.JS.Order>(OrNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnState == "Duplicate")
            {
                if (jOrder.IsExistDocument<JBS.JS.Order>(OrNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnState == "Modify")
            {
                var dr = Common.load("Check", "[order]", "orno", OrNo.Text.Trim());
                if (dr != null)
                {
                    if (TextBefore != OrNo.Text.Trim())
                    {
                        WriteToTxt(dr);
                        loadBom();
                    }
                }
                else
                {
                    e.Cancel = true;
                    OrNo.SelectAll();

                    using (var frm = new FrmOrder_Print_OrNo())
                    {
                        frm.TSeekNo = OrNo.Text.Trim();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            OrNo.Text = frm.TResult.Trim();
                            WriteToTxt(Common.load("Check", "[order]", "orno", frm.TResult.Trim()));
                            loadBom();

                        }
                    }
                }
            }
        }

        private void OrDate_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.ReadOnly == true) return;
            if (btnCancel.Focused) return;

            if (tb.Text.Trim() == "")
            {
                e.Cancel = true;
                MessageBox.Show("日期不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!tb.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("輸入日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (tb.Name == OrDate.Name)
            //{
            //    if (this.FormState != FormEditState.Modify)
            //    {
            //        if (CuNo.Text.Trim().Length > 0)
            //        {
            //            for (int i = 0; i < OrD.Rows.Count; i++)
            //            {
            //                Common.GetSpecialPrice(OrD.Rows[i], i, CuNo, OrDate, dataGridViewT1, GetSystemPrice);
            //                SetRow_TaxPrice(OrD.Rows[i]);
            //                SetRow_Mny(OrD.Rows[i]);
            //                dataGridViewT1.InvalidateRow(i);
            //            }
            //            SetAllMny();
            //        }
            //    }
            //}
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (CuNo.Text == "") return;
            if (dataGridViewT1.ReadOnly) return;
            if (dataGridViewT1.Rows.Count > 0) return;
            gridAppend_Click(null, null);
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                TextBefore = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "包裝數量")
            {
                TextBefore = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "訂單數量")
            {
                TextBefore = dataGridViewT1["訂單數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jOrder.Validate<JBS.JS.Item>(ItNoBegin, reader =>
                {
                    ItNoBegin = reader["itno"].ToString().Trim();
                    UdfNoBegin = reader["itnoudf"].ToString().Trim();
                });
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) 
                return;
            if (e.RowIndex == -1)
                return;
            string column = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (dataGridViewT1.ReadOnly) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                if (OrD.Rows[e.RowIndex]["qty"].ToDecimal() != OrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jOrder.DataGridViewOpen<JBS.JS.Item>(sender, e, OrD, row => FillItem(row, e.RowIndex));

            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                if (OrD.Rows[e.RowIndex]["qty"].ToDecimal() != OrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var itno = dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var row = Common.load("Check", "item", "itno", itno);
                if (row != null)
                {
                    if (row != null && unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        OrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            OrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            OrD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = unit;

                    OrD.Rows[e.RowIndex]["itunit"] = unit;
                    dataGridViewT1.InvalidateRow(e.RowIndex);

                    //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                    if (Common.Sys_DBqty == 1)
                    {
                        Common.GetSpecialPrice(OrD.Rows[e.RowIndex], e.RowIndex, CuNo, OrDate, dataGridViewT1, GetSystemPrice);
                        SetRow_TaxPrice(OrD.Rows[e.RowIndex]);
                        SetRow_Mny(OrD.Rows[e.RowIndex]);

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        SetAllMny();
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "送貨地址")
            {
                using (指送地址 frm = new 指送地址())
                {
                    frm.cuno = CuNo.Text.Trim();
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            OrD.Rows[dataGridViewT1.CurrentCell.RowIndex]["AdAddr"] = frm.addr.ToString().Trim().GetUTF8(60);
                            OrD.Rows[dataGridViewT1.CurrentCell.RowIndex]["Adper1"] = frm.per.ToString().Trim().GetUTF8(10);
                            OrD.Rows[dataGridViewT1.CurrentCell.RowIndex]["Adtel"] = frm.tel.ToString().Trim().GetUTF8(20);
                            OrD.Rows[dataGridViewT1.CurrentCell.RowIndex]["AdName"] = frm.name.ToString().Trim().GetUTF8(50);
                            dataGridViewT1.InvalidateRow(dataGridViewT1.CurrentCell.RowIndex);
                            break;
                        case DialogResult.Cancel: break;
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "說明")
            {
                using (var frm = new subMenuFm_2.FrmSale_Memo())
                {
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                            OrD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                            break;
                        case DialogResult.Cancel: break;
                    }
                    dataGridViewT1.InvalidateRow(e.RowIndex);

                    if (dataGridViewT1.EditingControl != null)
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "存量預估")
            {
                if (OrD.Rows[e.RowIndex]["ittrait"].ToDecimal() == 3) return;
                if (OrD.Rows[e.RowIndex]["存量預估"].ToString() == "展開")
                {
                    OrD.Rows[e.RowIndex]["存量預估"] = "不展開";
                    OrD.Rows[e.RowIndex]["stkqtyflag"] = 2;
                }
                else
                {
                    OrD.Rows[e.RowIndex]["存量預估"] = "展開";
                    OrD.Rows[e.RowIndex]["stkqtyflag"] = 1;
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "訂單數量")
            {
                if (OrD.Rows[e.RowIndex]["qty"].ToDecimal() != OrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Common.Sys_DBqty == 1)
                {
                    using (FrmComputer frm = new FrmComputer())
                    {
                        frm.w1 = OrD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = OrD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = OrD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = OrD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = OrD.Rows[e.RowIndex]["Pformula"].ToString();
                        switch (frm.ShowDialog())
                        {
                            case DialogResult.OK:
                                OrD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                                OrD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                                OrD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                                OrD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                                OrD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;

                                if (dataGridViewT1.EditingControl != null)
                                    dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);

                                OrD.Rows[e.RowIndex]["Qty"] = frm.resultCount.ToString("f" + Common.Q);
                                OrD.Rows[e.RowIndex]["PQty"] = frm.resultCount.ToString("f" + Common.Q);
                                OrD.Rows[e.RowIndex]["qtynotout"] = frm.resultCount.ToString("f" + Common.Q);
                                OrD.Rows[e.RowIndex]["qtynotinstk"] = frm.resultCount.ToString("f" + Common.Q);
                                SetRow_Mny(OrD.Rows[e.RowIndex]);
                                dataGridViewT1.InvalidateRow(e.RowIndex);
                                SetAllMny();
                                break;
                            case DialogResult.Cancel: break;
                        }
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);

                if (dataGridViewT1.EditingControl != null)
                    ((TextBox)dataGridViewT1.EditingControl).SelectAll();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "計價數量")
            {
                if (Common.Sys_DBqty == 2)
                {
                    using (FrmComputer frm = new FrmComputer())
                    {
                        frm.w1 = OrD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = OrD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = OrD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = OrD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = OrD.Rows[e.RowIndex]["Pformula"].ToString();
                        frm.qty = OrD.Rows[e.RowIndex]["qty"].ToDecimal();
                        frm.lbTxt = "訂單數量";
                        switch (frm.ShowDialog())
                        {
                            case DialogResult.OK:
                                OrD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                                OrD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                                OrD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                                OrD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                                OrD.Rows[e.RowIndex]["qty"] = frm.qty;
                                OrD.Rows[e.RowIndex]["pformula"] = frm.Pformula;
                                OrD.Rows[e.RowIndex]["qtynotout"] = frm.qty;
                                OrD.Rows[e.RowIndex]["qtynotinstk"] = frm.qty;

                                if (dataGridViewT1.EditingControl != null)
                                    dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);

                                OrD.Rows[e.RowIndex]["Pqty"] = frm.resultCount.ToString("f" + Common.Q);

                                SetRow_Mny(OrD.Rows[e.RowIndex]);
                                dataGridViewT1.InvalidateRow(e.RowIndex);
                                SetAllMny();
                                break;
                            case DialogResult.Cancel: break;
                        }
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);

                if (dataGridViewT1.EditingControl != null)
                    ((TextBox)dataGridViewT1.EditingControl).SelectAll();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "售價")
            {
                if (OrD.Rows[e.RowIndex]["itno"].ToString().Trim() == "") return;
                using (var frm = new FrmItemLevelb())
                {
                    frm.TSeekNo = OrD.Rows[e.RowIndex]["itno"].ToString().Trim();
                    frm.itunit = OrD.Rows[e.RowIndex]["itunit"].ToString().Trim();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Result.ToDecimal().ToString("f" + Common.MS);

                        OrD.Rows[e.RowIndex]["price"] = frm.Result.ToDecimal("f" + Common.MS);
                        SetRow_TaxPrice(OrD.Rows[e.RowIndex]);
                        SetRow_Mny(OrD.Rows[e.RowIndex]);

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        SetAllMny();
                    }
                    if (dataGridViewT1.EditingControl != null)
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
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
                            OrD.Rows[e.RowIndex]["punit"] = frm.Result;
                    }
                }
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly || btnCancel.Focused) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                string itnonow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (itnonow == ItNoBegin)
                    return;

                if (OrD.Rows[e.RowIndex]["qty"].ToDecimal() != OrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    OrD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                if (itnonow == "")
                {
                    OrD.Rows[e.RowIndex]["itno"] = "";
                    OrD.Rows[e.RowIndex]["itname"] = "";
                    OrD.Rows[e.RowIndex]["qty"] = 0;
                    OrD.Rows[e.RowIndex]["pqty"] = 0;
                    OrD.Rows[e.RowIndex]["itunit"] = "";
                    OrD.Rows[e.RowIndex]["punit"] = "";
                    OrD.Rows[e.RowIndex]["price"] = 0;
                    OrD.Rows[e.RowIndex]["prs"] = 1;
                    OrD.Rows[e.RowIndex]["taxprice"] = 0;
                    OrD.Rows[e.RowIndex]["mny"] = 0;
                    OrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    OrD.Rows[e.RowIndex]["產品組成"] = "";
                    OrD.Rows[e.RowIndex]["memo"] = "";
                    OrD.Rows[e.RowIndex]["priceb"] = 0;
                    OrD.Rows[e.RowIndex]["taxpriceb"] = 0;
                    OrD.Rows[e.RowIndex]["mnyb"] = 0;
                    OrD.Rows[e.RowIndex]["qtynotout"] = 0;
                    OrD.Rows[e.RowIndex]["qtynotinstk"] = 0;
                    OrD.Rows[e.RowIndex]["存量預估"] = "";
                    OrD.Rows[e.RowIndex]["esdate"] = Date.ToTWDate(EsDate.Text);
                    OrD.Rows[e.RowIndex]["esdate1"] = Date.ToUSDate(EsDate.Text);
                    OrD.Rows[e.RowIndex]["mwidth1"] = 0;
                    OrD.Rows[e.RowIndex]["mwidth2"] = 0;
                    OrD.Rows[e.RowIndex]["mwidth3"] = 0;
                    OrD.Rows[e.RowIndex]["mwidth4"] = 0;
                    OrD.Rows[e.RowIndex]["pformula"] = "";
                    OrD.Rows[e.RowIndex]["Prs"] = (Common.Sys_SalePrice == 2) ? Disc : 1;

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();

                    var rec = OrD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jOrder.RemoveBom(rec, ref OrBom);
                    return;
                }
                if (itnonow == UdfNoBegin)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    OrD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jOrder.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, OrD, row => FillItem(row, e.RowIndex));

            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "單位")
            {
                string itno = OrD.Rows[e.RowIndex]["ItNo"].ToString().Trim();
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (unit == TextBefore)
                    return;

                if (OrD.Rows[e.RowIndex]["qty"].ToDecimal() != OrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    OrD.Rows[e.RowIndex]["itunit"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jOrder.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        OrD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        OrD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                OrD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)
                {
                    Common.GetSpecialPrice(OrD.Rows[e.RowIndex], e.RowIndex, CuNo, OrDate, dataGridViewT1, GetSystemPrice);
                    SetRow_TaxPrice(OrD.Rows[e.RowIndex]);
                    SetRow_Mny(OrD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "售價")
            {
                if (Common.Sys_LowCost != 3 && dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim() != "")
                    pVar.CheckLowCost(dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["單位", e.RowIndex].Value.ToString().Trim(), dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS));
                OrD.Rows[e.RowIndex]["price"] = dataGridViewT1["售價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MS);
                SetRow_TaxPrice(OrD.Rows[e.RowIndex]);
                SetRow_Mny(OrD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "折數")
            {
                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                OrD.Rows[e.RowIndex]["prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToString();
                SetRow_TaxPrice(OrD.Rows[e.RowIndex]);
                SetRow_Mny(OrD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "稅前金額")
            {
                //正常情形『稅前金額』是由『售價』帶出來的
                //下面處理的情形是手動打上『稅前金額』
                //所以須往前推算『售價』金額。
                decimal beforeMny = OrD.Rows[e.RowIndex]["mny"].ToDecimal("f" + Common.TPS);
                decimal nowMny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.TPS);
                if (beforeMny == nowMny) return;

                decimal price = 0;
                decimal qty = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f" + Common.Q);
                decimal taxprice = dataGridViewT1["稅前售價", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f6");
                decimal mny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f" + Common.TPS);
                decimal prs = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToString().ToDecimal();
                qty = (qty == 0) ? 1 : qty;

                OrD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / qty) / prs).ToDecimal("f" + Common.MS);
                        OrD.Rows[e.RowIndex]["Price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / qty) / prs).ToDecimal("f" + Common.MS);
                        OrD.Rows[e.RowIndex]["Price"] = price;
                        break;
                }
                SetRow_TaxPrice(OrD.Rows[e.RowIndex]);

                taxprice = OrD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                OrD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                OrD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                OrD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "交貨日期")
            {
                TextBox tday = new TextBox();
                tday.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

                tday.Text = dataGridViewT1["交貨日期", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (tday.IsDateTime() == false)
                {
                    e.Cancel = true;
                    MessageBox.Show("日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (dataGridViewT1.EditingControl != null)
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();

                    return;
                }
                OrD.Rows[e.RowIndex]["esdate"] = Date.ToTWDate(tday.Text.Trim());
                OrD.Rows[e.RowIndex]["esdate1"] = Date.ToUSDate(tday.Text.Trim());
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "訂單數量")
            {
                var qNow = dataGridViewT1["訂單數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                if (qNow == TextBefore.ToDecimal())
                    return;

                if (OrD.Rows[e.RowIndex]["qty"].ToDecimal() != OrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    OrD.Rows[e.RowIndex]["qty"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                var qty = dataGridViewT1["訂單數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                if (Common.Sys_DBqty == 1)
                {
                    OrD.Rows[e.RowIndex]["Qty"] = qty;
                    OrD.Rows[e.RowIndex]["PQty"] = qty;
                    OrD.Rows[e.RowIndex]["qtynotout"] = qty;
                    OrD.Rows[e.RowIndex]["qtynotinstk"] = qty - (TextBefore.ToInteger() - OrD.Rows[e.RowIndex]["qtynotinstk"].ToInteger());//扣掉原有入庫數
                }
                else if (Common.Sys_DBqty == 2)
                {
                    OrD.Rows[e.RowIndex]["Qty"] = qty;
                    OrD.Rows[e.RowIndex]["qtynotout"] = qty;
                    OrD.Rows[e.RowIndex]["qtynotinstk"] = qty - (TextBefore.ToInteger() - OrD.Rows[e.RowIndex]["qtynotinstk"].ToInteger());//扣掉原有入庫數
                }
                SetRow_Mny(OrD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "計價數量")
            {
                OrD.Rows[e.RowIndex]["PQty"] = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);

                SetRow_Mny(OrD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "包裝數量")
            {
                var qNow = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                if (qNow == TextBefore.ToDecimal())
                    return;

                if (OrD.Rows[e.RowIndex]["qty"].ToDecimal() != OrD.Rows[e.RowIndex]["qtynotout"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    OrD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                OrD.Rows[e.RowIndex]["itpkgqty"] = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }

        }

        private void FillItem(SqlDataReader row, int index)
        {
            this.ItNoBegin = row["itno"].ToString().Trim();

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = row["itno"].ToString().Trim();

            OrD.Rows[index]["itno"] = row["itno"].ToString().Trim();
            OrD.Rows[index]["itname"] = row["itname"].ToString();
            OrD.Rows[index]["punit"] = row["punit"].ToString().Trim();
            OrD.Rows[index]["ItNoUdf"] = row["ItNoUdf"].ToString();
            //銷貨常用單位
            string unitset = row["ItSalUnit"].ToString().Trim();
            string unit;
            //預設帶包裝單位或是單位
            if (unitset == "1")
            {
                unit = row["ItUnitp"].ToString();
                OrD.Rows[index]["ItUnit"] = unit;

                var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0)
                    itpkgqty = 1;
                OrD.Rows[index]["itpkgqty"] = itpkgqty;
            }
            else
            {
                unit = row["ItUnit"].ToString();
                OrD.Rows[index]["ItUnit"] = unit;
                OrD.Rows[index]["itpkgqty"] = 1;
            }

            Common.GetSpecialPrice(OrD.Rows[index], index, CuNo, OrDate, dataGridViewT1, GetSystemPrice);
            SetRow_TaxPrice(OrD.Rows[index]);
            SetRow_Mny(OrD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();

            if (row["ittrait"].ToString() == "1")
            {
                OrD.Rows[index]["產品組成"] = "組合品";
                OrD.Rows[index]["ittrait"] = 1;
                OrD.Rows[index]["存量預估"] = "展開";
                OrD.Rows[index]["stkqtyflag"] = 1;
            }
            else if (row["ittrait"].ToString() == "2")
            {
                OrD.Rows[index]["產品組成"] = "組裝品";
                OrD.Rows[index]["ittrait"] = 2;
                OrD.Rows[index]["存量預估"] = "展開";
                OrD.Rows[index]["stkqtyflag"] = 1;
            }
            else
            {
                OrD.Rows[index]["產品組成"] = "單一商品";
                OrD.Rows[index]["ittrait"] = 3;
                OrD.Rows[index]["存量預估"] = "不展開";
                OrD.Rows[index]["stkqtyflag"] = 2;
            }

            for (int i = 1; i <= 10; i++)
            {
                OrD.Rows[index]["itdesp" + i] = row["itdesp" + i].ToString();
            }

            string rec = OrD.Rows[index]["BomRec"].ToString();
            jOrder.RemoveBom(rec, ref OrBom);

            jOrder.GetItemBom(row["itno"].ToString().Trim(), rec, ref OrBom);

            rowstandard(row["itno"].ToString().Trim(), index);//回寫對應的客戶型號
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
                                OrD.Rows[i]["standard"] = dtstandard.Rows[0]["standard"].ToString();
                            else
                                OrD.Rows[i]["standard"] = "";
                        }
                    }
                    else
                    {
                        cmd.CommandText = " select * from standard where itno=@itno and cfno=@cuno";
                        da.Fill(dtstandard);
                        if (dtstandard.Rows.Count != 0)
                            OrD.Rows[index]["standard"] = dtstandard.Rows[0]["standard"].ToString();
                        else
                            OrD.Rows[index]["standard"] = "";
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
            var qty = row["Pqty"].ToDecimal("f" + Common.Q);
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
            var sum = OrD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f" + Common.TPS)).ToDecimal("f" + Common.MST);

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
                var totmny = OrD.AsEnumerable().Sum(r => r["Pqty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MS)).ToDecimal("f" + Common.MST);
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
            if (keyData.ToString() == "F11, Shift" && gridItBuyPrice.Enabled)
            {
                gridItBuyPrice.PerformClick();
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else if (keyData.ToString() == "F9, Shift" && gridTran.Enabled)
            {
                gridTran.PerformClick();
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else if (keyData.ToString() == "F10, Shift" && gridAllTrans.Enabled)
            {
                gridAllTrans.PerformClick();
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
                case Keys.F6:
                    if (gridItDesp.Enabled) gridItDesp.PerformClick();
                    break;
                case Keys.F7:
                    if (gridBomD.Enabled) gridBomD.PerformClick();
                    break;
                case Keys.F8:
                    if (gridStock.Enabled) gridStock.PerformClick();
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
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
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
            else if (str == "訂單數量")
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
            if (OrNo.Text.Trim() == "") return;
            FrmSale_AppScNo frm = new FrmSale_AppScNo();
            //新增人員
            frm.AName = T.Rows[0]["AppScNo"].ToString();
            frm.ATime = T.Rows[0]["AppDate"].ToString();
            //修改人員
            frm.EName = T.Rows[0]["EdtScNo"].ToString();
            frm.ETime = T.Rows[0]["EdtDate"].ToString();
            frm.ShowDialog();
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
                for (int i = 0; i < OrD.Rows.Count; i++)
                {
                    SetRow_Mny(OrD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();
            }
            dataGridViewT1_Click(null, null);
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            gridAppend.Enabled = gridDelete.Enabled = gridPicture.Enabled = gridInsert.Enabled = gridItDesp.Enabled = gridBomD.Enabled = oroverflag.Enabled = orpickingflag.Enabled =ortrnflag.Enabled = !btnAppend.Enabled; 
            gridStock.Enabled = gridTran.Enabled = gridAllTrans.Enabled = gridItBuyPrice.Enabled = gridItemQuote.Enabled = gridCost.Enabled = gridCustSale.Enabled = true;
            gridAllTransTrans.Enabled = bt2.Enabled =true;
        }

        private void AdAddr_DoubleClick(object sender, EventArgs e)
        {
            if (((TextBox)sender).ReadOnly) return;

            using (指送地址 frm = new 指送地址())
            {
                frm.cuno = CuNo.Text.Trim();
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        AdAddr.Text = frm.addr; //公司住址
                        AdPer1.Text = frm.per;  //公司連絡人
                        AdTel.Text = frm.tel;   //公司電話
                        AdName = frm.Name;      //公司名稱
                        break;
                    case DialogResult.Cancel: break;
                }
            }
        }

        private void ShowORModify_PhotoPath_Click(object sender, EventArgs e)
        {
            using (FrmAffixFile frm = new FrmAffixFile())
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DaFile.Clear();
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Datano", OrNo.Text.Trim());
                    cmd.Parameters.AddWithValue("Datano2", QuNo.Text.Trim());
                    cmd.CommandText = "select ROW_NUMBER() OVER(ORDER BY id) AS 序號, * from AffixFile where (DaType ='訂貨單' and Datano=@Datano) or (Datano=@Datano2 and DaType='報價單')";
                    da.Fill(DaFile);

                    frm.DtFile = DaFile;
                    frm.CMD = cmd;
                    frm.Datano = OrNo.Text.Trim();
                    frm.DaType = "訂貨單";
                    if (btnState == "Append" || btnState == "Duplicate")
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
      
    }
}
