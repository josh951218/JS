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
using System.IO;
using System.Diagnostics;

namespace S_61.subMenuFm_1
{
    public partial class FrmFord : Formbase
    {
        JBS.JS.Ford jFord;

        DataTable FoD = new DataTable();
        DataTable FoBom = new DataTable();
        DataTable DaFile = new DataTable();
        List<TextBoxbase> list;
        DataTable T = new DataTable();//製單人員
        SqlTransaction tn;

        string tempNo = "";
        string btnState = "";
        string ItNoBegin = "";
        string UdfNoBegin = "";
        decimal BomRec = 0;
        string sql = "";
        string faname2 = "";
        string spname = "";
        string TextBefore = "";

        string memo1 = "";//詳細備註
        bool order2ford = false;
        string order2fordFoNo = "";
        string PhotoPath = "";// 13.7c

        public FrmFord()
        {
            InitializeComponent();
            this.jFord = new JBS.JS.Ford();
            this.list = this.getEnumMember();
            this.dataGridViewT1.tableName = "Fordd";

            order2ford = false;
            pVar.SetMemoUdf(this.說明);


            this.採購數量.Set庫存數量小數();
            this.計價數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.採購未交量.Set庫存數量小數();

            this.進價.Set進貨單價小數();
            this.稅前金額.Set進項金額小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前進價.FirstNum = 9;
            this.稅前進價.LastNum = 6;
            this.稅前進價.DefaultCellStyle.Format = "f6";

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
            TaxMny.LastNum = Common.MFT;

            Tax.FirstNum = Common.nFirst;
            Tax.LastNum = Common.TF;

            TotMny.FirstNum = Common.nFirst;
            TotMny.LastNum = Common.MFT;

            Rate.FirstNum = 1;
            Rate.LastNum = 0;

            FoDate.MaxLength = (Common.User_DateTime == 1) ? 7 : 8;
            this.交貨日期.DataPropertyName = (Common.User_DateTime == 1) ? "esdate" : "esdate1";

            this.Shown -= new System.EventHandler(this.FrmFord_Shown_1);
            this.Shown += new System.EventHandler(this.FrmFord_Shown_1);

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

        public FrmFord(string nn)
        {
            InitializeComponent();
            this.jFord = new JBS.JS.Ford();
            this.list = this.getEnumMember();

            order2ford = true;
            order2fordFoNo = nn;

            pVar.SetMemoUdf(this.說明);

            this.採購數量.Set庫存數量小數();
            this.計價數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.採購未交量.Set庫存數量小數();

            this.進價.Set進貨單價小數();
            this.稅前金額.Set進項金額小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前進價.FirstNum = 9;
            this.稅前進價.LastNum = 6;
            this.稅前進價.DefaultCellStyle.Format = "f6";

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
            TaxMny.LastNum = Common.MFT;

            Tax.FirstNum = Common.nFirst;
            Tax.LastNum = Common.TF;

            TotMny.FirstNum = Common.nFirst;
            TotMny.LastNum = Common.MFT;

            Rate.FirstNum = 1;
            Rate.LastNum = 0;

            FoDate.MaxLength = (Common.User_DateTime == 1) ? 7 : 8;
            this.交貨日期.DataPropertyName = (Common.User_DateTime == 1) ? "esdate" : "esdate1";

            this.Shown -= new System.EventHandler(this.FrmFord_Shown);
            this.Shown += new System.EventHandler(this.FrmFord_Shown);


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
        }

        private void FrmFord_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlDataAdapter da = new SqlDataAdapter("Select Top(1)* from ford where 1=0", cn))
            {
                da.Fill(T);//製單人員
            }

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
                    end,ItNoUdf = '',*
                    from fordd where 1=0 ";

                da.Fill(FoD);
                dataGridViewT1.DataSource = FoD;
            }
            FoD.Clear();

            if (order2ford == false) WriteToTxt(Common.load("Bottom", "ford", "fono"));
            else WriteToTxt(Common.load("Check", "ford", "fono", order2fordFoNo));
        }

        private void FrmFord_Shown_1(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        private void FrmFord_Shown(object sender, EventArgs e)
        {
            btnModify_Click(null, null);
        }

        void WriteToTxt(DataRow row)
        {
            T.Clear();
            FoD.Clear();
            tempNo = memo1 = spname = "";

            if (row == null)
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                FoOverFlag.Checked = false;

                FoD.Clear();
                FoBom.Clear();
            }
            else
            {
                memo1 = row["fomemo1"].ToString();
                T.ImportRow(row);
                T.AcceptChanges();

                FoNo.Text = row["FoNo"].ToString().Trim();
                FqNo.Text = row["FqNo"].ToString().Trim();
                tempNo = FoNo.Text.Trim();

                if (Common.User_DateTime == 1)
                    FoDate.Text = row["FoDate"].ToString().Trim();
                else
                    FoDate.Text = row["FoDate1"].ToString().Trim();

                EmNo.Text = row["emno"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
                FaNo.Text = row["FaNo"].ToString();
                FaName1.Text = row["FaName1"].ToString();
                FaPer1.Text = row["FaPer1"].ToString();
                FaTel1.Text = row["FaTel1"].ToString();
                Xa1No.Text = row["Xa1No"].ToString();
                Xa1Name.Text = row["Xa1Name"].ToString();
                Xa1Par.Text = row["Xa1Par"].ToString();
                SpNo.Text = row["SpNo"].ToString();
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

                TaxMny.Text = row["TaxMny"].ToDecimal().ToString("f" + Common.MFT);
                TaxMnyB.Text = row["TaxMnyB"].ToDecimal().ToString("f" + Common.M);

                X3No.Text = row["X3No"].ToString();
                Rate.Text = (row["Rate"].ToDecimal() * 100).ToString("f0");
                Tax.Text = row["Tax"].ToDecimal().ToString("f" + Common.TF);
                TotMny.Text = row["TotMny"].ToDecimal().ToString("f" + Common.MFT);

                FoPayment.Text = row["FoPayment"].ToString();
                FoMemo.Text = row["FoMemo"].ToString();

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

                FoOverFlag.Checked = (row["fooverflag"].ToString() == "True");

                loadD();
            }
        }

        void loadD()
        {
            FoD.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("fono", FoNo.Text.Trim());
                cmd.CommandText = @"
                    Select 產品組成=
                    case
                    when ittrait=1 then '組合品'
                    when ittrait=2 then '組裝品'
                    when ittrait=3 then '單一商品'
                    end,ItNoUdf= (select top 1 itnoudf from item where item.itno = fordd.itno) ,*
                    from fordd where fono=@fono ORDER BY recordno";

                da.Fill(FoD);
                dataGridViewT1.DataSource = FoD;
            }
        }

        void loadBom()
        {
            FoBom.Clear();

            if (btnState == "Append")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = "select * from fordbom where 1=0 ";

                    da.Fill(FoBom);
                }
            }

            if (btnState == "Modify")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("fono", this.tempNo);
                    cmd.CommandText = "select * from fordbom where fono=@fono";
                    da.Fill(FoBom);
                }
            }

            if (btnState == "Duplicate")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("fono", this.tempNo);
                    cmd.CommandText = "select * from fordbom where fono=@fono";
                    da.Fill(FoBom);
                }
            }
        }

        void AddRow()
        {
            if (FaNo.Text.Trim() == "")
            {
                FaNo.Focus();
                MessageBox.Show("請先輸入廠商編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaName1.Text = FaPer1.Text = FaTel1.Text = EmNo.Text = EmName.Text = "";
                return;
            }
            DataRow dr = FoD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 0;
            dr["pqty"] = 0;
            dr["itunit"] = "";
            dr["punit"] = "";
            dr["price"] = 0;
            dr["taxprice"] = 0;
            dr["prs"] = 1;
            dr["mny"] = 0;
            dr["itpkgqty"] = 1;
            dr["taxpriceb"] = 0;
            dr["priceb"] = 0;
            dr["mnyb"] = 0;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["qtynotin"] = 0;
            dr["Pformula"] = "";
            dr["esdate"] = Date.ToTWDate(FoDate.Text);
            dr["esdate1"] = Date.ToUSDate(FoDate.Text);
            dr["qtyin"] = 0;
            dr["BomRec"] = GetBomRec();

            FoD.Rows.Add(dr);
            FoD.AcceptChanges();
        }

        void AddRow(int index)
        {
            var dr = FoD.NewRow();
            dr["itno"] = "";
            dr["itname"] = "";
            dr["qty"] = 0;
            dr["pqty"] = 0;
            dr["itunit"] = "";
            dr["punit"] = "";
            dr["price"] = 0;
            dr["taxprice"] = 0;
            dr["prs"] = 1;
            dr["mny"] = 0;
            dr["itpkgqty"] = 1;
            dr["taxpriceb"] = 0;
            dr["priceb"] = 0;
            dr["mnyb"] = 0;
            dr["產品組成"] = "";
            dr["memo"] = "";
            dr["qtynotin"] = 0;
            dr["Pformula"] = "";
            dr["esdate"] = Date.ToTWDate(FoDate.Text);
            dr["esdate1"] = Date.ToUSDate(FoDate.Text);
            dr["qtyin"] = 0;
            dr["BomRec"] = GetBomRec();

            FoD.Rows.InsertAt(dr, index);
            FoD.AcceptChanges();
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
                for (int i = 0; i < FoD.Rows.Count; i++)
                {
                    if (Convert.ToInt32(FoD.Rows[i]["BomRec"].ToString()) > d)
                        d = Convert.ToInt32(FoD.Rows[i]["BomRec"].ToString());
                }
                BomRec = ++d;
                return BomRec;
            }
        }

        void DeleteRow(int index)
        {
            string rec = dataGridViewT1["組件編號", index].Value.ToString();
            int rowcount = FoBom.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                if (FoBom.Rows[i]["BomRec"].ToString() == rec)
                    FoBom.Rows[i].Delete();
            }
            FoBom.AcceptChanges();
            FoD.Rows[index].Delete();
            FoD.AcceptChanges();
            SetAllMny();
        }

        bool SetFoNo()
        {
            string strFoNo = "";
            if (FoNo.Text != "")
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("fono", FoNo.Text.Trim());
                    cmd.CommandText = "select FoNo from ford where FoNo=@fono";
                    if (!cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        MessageBox.Show("採購單號重複", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FoNo.Focus();
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
                        strDate = Date.ChangeDateForSN(FoDate.Text);
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", strDate);
                        string sql = "select fono from ford where fono like @fono + '%' order by fono desc";
                        cmd.CommandText = sql;
                        DataTable fanotable = new DataTable();
                        List<DataRow> listno = new List<DataRow>();
                        SqlDataAdapter dd = new SqlDataAdapter(cmd);
                        string Countgano = "";
                        dd.Fill(fanotable);
                        if (fanotable.Rows.Count > 0)
                        {
                            listno = fanotable.AsEnumerable().ToList();
                            Countgano = fanotable.Rows[0]["fono"].ToString();
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
                                strFoNo = strDate + Countgano;
                                if (listno.Find(r => r.Field<string>("FoNo") == strFoNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                            else
                            {
                                Countgano = C.ToString();
                                Countgano = Countgano.PadLeft(4, '0');
                                strFoNo = strDate + Countgano;
                                if (listno.Find(r => r.Field<string>("FoNo") == strFoNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                        } while (isRepeat);
                        FoNo.Text = strFoNo;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    FoNo.Text = "";
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

        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (dataGridViewT1.SelectedRows[0].Cells["採購數量"].Value.ToDecimal() != dataGridViewT1.SelectedRows[0].Cells["採購未交量"].Value.ToDecimal())
            {
                MessageBox.Show("此產品已有交貨，無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            gridDelete.Focus();
            int index = 0;
            if (dataGridViewT1.Rows.Count > 0)
            {
                string rec = dataGridViewT1.SelectedRows[0].Cells["組件編號"].Value.ToString();
                for (int x = 0; x <  FoBom.Rows.Count; x++)
                {
                    if (FoBom.Rows[x]["BomRec"].ToString() == rec)
                    {
                        FoBom.Rows.RemoveAt(x--);
                    }
                }
                FoBom.AcceptChanges();

                index = dataGridViewT1.CurrentRow.Index;
                FoD.Rows.RemoveAt(index);
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
                if (dataGridViewT1["採購數量", index].Value.ToDecimal() != dataGridViewT1["採購未交量", index].Value.ToDecimal())
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

        private void gridItDesp_Click(object sender, EventArgs e)
        {

            gridItDesp.Focus();
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                dataGridViewT1.Focus();
                return;
            }
            using (var frm = new JE.SOther.FrmDesp(true, FormStyle.Mini))
            {
                frm.dr = FoD.Rows[index];
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
            DataTable table = FoBom.Clone();

            for (int i = 0; i < FoBom.Rows.Count; i++)
            {
                if (FoBom.Rows[i]["bomrec"].ToString().Trim() == rec)
                {
                    table.ImportRow(FoBom.Rows[i]);
                    FoBom.Rows.RemoveAt(i--);
                }
            }

            table.AcceptChanges();
            FoBom.AcceptChanges();

            using (var frm = new subMenuFm_2.FrmDraw_Bom())
            { 
                frm.table = table.Copy();
                frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                frm.BomRec = rec;
                frm.grid = dataGridViewT1;
                frm.上層Row = FoD.Rows[dataGridViewT1.CurrentCell.RowIndex];
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        if (frm.CallBack == "Bom")
                        {
                            FoBom.Merge(frm.table);
                            FoBom.AcceptChanges();
                            dataGridViewT1.Focus();
                            table.Clear();
                        }
                        break;
                    case DialogResult.Cancel:
                        FoBom.Merge(table);
                        FoBom.AcceptChanges();
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
            var itno = FoD.Rows[index]["itno"].ToString().Trim();
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
            var itno = FoD.Rows[index]["itno"].ToString().Trim();
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
            var itno = (index == -1) ? "" : FoD.Rows[index]["itno"].ToString().Trim();
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
            //FoNo,FaNo,X3No
            TextBefore = (sender as TextBox).Text;
        }




        private void btnTop_Click(object sender, EventArgs e)
        {
            WriteToTxt(Common.load("Top", "ford", "fono"));
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var row = Common.load("Prior", "ford", "fono", FoNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CPrior", "ford", "fono", FoNo.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

            }
            WriteToTxt(row);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var row = Common.load("Next", "ford", "fono", FoNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "ford", "fono", FoNo.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

            }
            WriteToTxt(row);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            WriteToTxt(Common.load("Bottom", "ford", "fono", FoNo.Text.Trim()));


        }

        void WhenAppend()
        {
            FoDate.Text = Date.GetDateTime(Common.User_DateTime);

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
            tempNo = FoNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            Xa1Par.ReadOnly = (Common.Series == "74" || Common.Series == "73");
            FoOverFlag.Checked = false;

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true; ;
            this.稅前進價.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.採購未交量.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            this.BomRec = 0;

            FoD.Clear();
            loadBom();

            T.Clear();
            DataRow tr = T.NewRow();
            T.Rows.Add(tr);
            T.AcceptChanges();
            memo1 = ""; ;

            WhenAppend();
            FoDate.Focus();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnState = "Duplicate";
            tempNo = FoNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            Xa1Par.ReadOnly = (Common.Series == "74" || Common.Series == "73");
            FoOverFlag.Checked = false;

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true; ;
            this.稅前進價.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.採購未交量.ReadOnly = true;
            this.自定編號.ReadOnly = true;

            loadBom();

            for (int i = 0; i < FoD.Rows.Count; i++)
            {
                FoD.Rows[i]["qtynotin"] = FoD.Rows[i]["qty"].ToString();
                FoD.Rows[i]["qtyin"] = 0;
                FoD.Rows[i]["esdate"] = Date.GetDateTime(1, false);
                FoD.Rows[i]["esdate1"] = Date.GetDateTime(2, false);
            }
            FoNo.Text = "";
            FqNo.Text = "";
            FoDate.Text = Date.GetDateTime(Common.User_DateTime, false);
            FoDate.Focus();
        }

        public void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jFord.IsExistDocument<JBS.JS.Ford>(FoNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jFord.IsModify<JBS.JS.Ford>(FoNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                jFord.upModify1<JBS.JS.Ford>(FoNo.Text.Trim());//更新修改狀態1
                WriteToTxt(Common.load("Cancel", "ford", "fono", FoNo.Text.Trim()));
            }
            btnState = "Modify";
            tempNo = FoNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            dataGridViewT1.ReadOnly = false;
            this.序號.ReadOnly = true;
            if (Common.Sys_KeyPrs == 2) this.折數.ReadOnly = true;
            this.稅前進價.ReadOnly = true;
            this.產品組成.ReadOnly = true;
            this.本幣單價.ReadOnly = true;
            this.本幣稅前單價.ReadOnly = true;
            this.本幣稅前金額.ReadOnly = true;
            this.採購未交量.ReadOnly = true;
            this.自定編號.ReadOnly = true;
            loadBom();
            FoNo.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jFord.IsExistDocument<JBS.JS.Ford>(FoNo.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNext_Click(null, null);
                return;
            }
            if (jFord.IsModify<JBS.JS.Ford>(FoNo.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中,無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < FoD.Rows.Count; i++)
            {
                if (FoD.Rows[i]["qtyin"].ToDecimal() != 0)
                {
                    MessageBox.Show("此張採購單已有出貨資料無法刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    cmd.Parameters.AddWithValue("@FoNo", FoNo.Text.Trim());
                    cmd.CommandText = "delete from ford where FoNo=@FoNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from fordd where FoNo=@FoNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from fordbom where FoNo=@FoNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    cmd.ExecuteNonQuery();

                    FrmAffixFile.FileDelete_單據刪除(cmd, FoNo.Text.Trim(), "採購單");

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
            FrmFord_Print frm = new FrmFord_Print();
            frm.PK = FoNo.Text.Trim();
            frm.ShowDialog();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "視窗訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new S1.FrmFOrdBrowNew())
            {
                frm.TSeekNo = FoNo.Text.Trim();
                frm.ShowDialog();
                WriteToTxt(Common.load("Check", "ford", "fono", frm.TResult.Trim()));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (FaNo.Text.Trim() == "")
            {
                MessageBox.Show("廠商編號不可為空", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                return;
            }

            bool falg = false;
            if (FoD.AsEnumerable().Count(r => r["itno"].ToString().Trim().Length == 0) > 0)
            {
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    if (dataGridViewT1["產品編號", i].Value.ToString() == "")
                    {
                        if (dataGridViewT1["採購數量", i].Value.ToString() == "0")
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
                MessageBox.Show("採購明細不可為空", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btnState == "Append" || btnState == "Duplicate")
            {
                if (!SetFoNo()) return;
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Transaction = tn;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", FoNo.Text.Trim());
                        cmd.Parameters.AddWithValue("fodate", Date.ToTWDate(FoDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("fodate1", Date.ToUSDate(FoDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                        cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("faname1", FaName1.Text.Trim());
                        cmd.Parameters.AddWithValue("faname2", faname2);
                        cmd.Parameters.AddWithValue("fatel1", FaTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("faper1", FaPer1.Text.Trim());
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
                        cmd.Parameters.AddWithValue("fopayment", FoPayment.Text.Trim());
                        cmd.Parameters.AddWithValue("fomemo", FoMemo.Text.Trim());
                        cmd.Parameters.AddWithValue("recordno", FoD.Rows.Count);
                        cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                        cmd.Parameters.AddWithValue("spname", spname);
                        cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("appscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("fomemo1", memo1);
                        cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.GetUTF8(100));
                        if (FoOverFlag.Checked)
                            cmd.Parameters.AddWithValue("fooverflag", 1);
                        else
                            cmd.Parameters.AddWithValue("fooverflag", 0);

                        cmd.CommandText = "insert into ford ("
                        + " fono,fodate,fodate1,fqno"
                            //+",cono,coname1,coname2,taxmnyf,usrno"
                        + " ,fano,faname1,faname2,fatel1,faper1,emno,emname,xa1no,xa1name,xa1par"
                        + " ,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,fopayment,fomemo"
                        + " ,recordno,spno,spname,appdate,edtdate,appscno,edtscno,fomemo1,fooverflag,PhotoPath)values("

                        + " @fono,@fodate,@fodate1,@fqno"
                        + " ,@fano,@faname1,@faname2,@fatel1,@faper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
                        + " ,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@fopayment,@fomemo"
                        + " ,@recordno,@spno,@spname,@appdate,@edtdate,@appscno,@edtscno,@fomemo1,@fooverflag,@PhotoPath)";

                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < FoD.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fono", FoNo.Text.Trim());
                            cmd.Parameters.AddWithValue("fodate", Date.ToTWDate(FoDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("fodate1", Date.ToUSDate(FoDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(dataGridViewT1["交貨日期", i].Value.ToString()));
                            cmd.Parameters.AddWithValue("esdate1", Date.ToUSDate(dataGridViewT1["交貨日期", i].Value.ToString()));
                            cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                            cmd.Parameters.AddWithValue("qtynotin", FoD.Rows[i]["qtynotin"].ToString());
                            cmd.Parameters.AddWithValue("qtyin", FoD.Rows[i]["qtyin"].ToString());
                            cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                            cmd.Parameters.AddWithValue("itno", FoD.Rows[i]["itno"]);
                            cmd.Parameters.AddWithValue("itname", FoD.Rows[i]["itname"]);
                            cmd.Parameters.AddWithValue("ittrait", FoD.Rows[i]["ittrait"]);
                            cmd.Parameters.AddWithValue("itunit", FoD.Rows[i]["itunit"]);
                            cmd.Parameters.AddWithValue("punit", FoD.Rows[i]["punit"]);
                            cmd.Parameters.AddWithValue("itpkgqty", FoD.Rows[i]["itpkgqty"]);
                            cmd.Parameters.AddWithValue("qty", FoD.Rows[i]["qty"].ToDecimal());
                            cmd.Parameters.AddWithValue("pqty", FoD.Rows[i]["pqty"].ToDecimal());
                            cmd.Parameters.AddWithValue("price", FoD.Rows[i]["price"].ToDecimal());
                            cmd.Parameters.AddWithValue("priceb", FoD.Rows[i]["priceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("prs", FoD.Rows[i]["prs"].ToDecimal());
                            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                            cmd.Parameters.AddWithValue("taxprice", FoD.Rows[i]["taxprice"].ToDecimal());
                            cmd.Parameters.AddWithValue("taxpriceb", FoD.Rows[i]["taxpriceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("mny", FoD.Rows[i]["mny"].ToDecimal());
                            cmd.Parameters.AddWithValue("mnyb", FoD.Rows[i]["mnyb"].ToDecimal());
                            cmd.Parameters.AddWithValue("memo", FoD.Rows[i]["memo"]);
                            cmd.Parameters.AddWithValue("bomid", FoNo.Text.ToString().Trim() + FoD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", FoD.Rows[i]["BomRec"]);
                            cmd.Parameters.AddWithValue("recordno", (i + 1));
                            cmd.Parameters.AddWithValue("mwidth1", FoD.Rows[i]["mwidth1"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth2", FoD.Rows[i]["mwidth2"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth3", FoD.Rows[i]["mwidth3"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth4", FoD.Rows[i]["mwidth4"].ToDecimal());
                            cmd.Parameters.AddWithValue("pformula", FoD.Rows[i]["pformula"]);
                            cmd.Parameters.AddWithValue("itdesp1", FoD.Rows[i]["itdesp1"]);
                            cmd.Parameters.AddWithValue("itdesp2", FoD.Rows[i]["itdesp2"]);
                            cmd.Parameters.AddWithValue("itdesp3", FoD.Rows[i]["itdesp3"]);
                            cmd.Parameters.AddWithValue("itdesp4", FoD.Rows[i]["itdesp4"]);
                            cmd.Parameters.AddWithValue("itdesp5", FoD.Rows[i]["itdesp5"]);
                            cmd.Parameters.AddWithValue("itdesp6", FoD.Rows[i]["itdesp6"]);
                            cmd.Parameters.AddWithValue("itdesp7", FoD.Rows[i]["itdesp7"]);
                            cmd.Parameters.AddWithValue("itdesp8", FoD.Rows[i]["itdesp8"]);
                            cmd.Parameters.AddWithValue("itdesp9", FoD.Rows[i]["itdesp9"]);
                            cmd.Parameters.AddWithValue("itdesp10", FoD.Rows[i]["itdesp10"]);

                            cmd.CommandText = "insert into fordd("
                                + " fono,fodate,fodate1,esdate,esdate1,fqno"
                                + " ,qtynotin,qtyin"
                                //+" ,lowzero,sltflag,extflag,stName"
                                + " ,fano,emno,xa1no,xa1par,itno,itname,ittrait,itunit,punit,itpkgqty"
                                + " ,qty,pqty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo"
                                + " ,bomid,bomrec,recordno,mwidth1,mwidth2,mwidth3,mwidth4,pformula"
                                + " ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                                + " ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10)values("

                                + " @fono,@fodate,@fodate1,@esdate,@esdate1,@fqno"
                                + " ,@qtynotin,@qtyin"
                                + " ,@fano,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@punit,@itpkgqty"
                                + " ,@qty,@pqty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo"
                                + " ,@bomid,@bomrec,@recordno,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@pformula"
                                + " ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                                + " ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10)";
                            cmd.ExecuteNonQuery();
                        }


                        for (int i = 0; i < FoBom.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fono", FoNo.Text.Trim());
                            cmd.Parameters.AddWithValue("bomid", FoNo.Text + FoBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", FoBom.Rows[i]["BomRec"].ToString());
                            cmd.Parameters.AddWithValue("itno", FoBom.Rows[i]["itno"].ToString());
                            cmd.Parameters.AddWithValue("itname", FoBom.Rows[i]["itname"].ToString());
                            cmd.Parameters.AddWithValue("itunit", FoBom.Rows[i]["itunit"].ToString());
                            cmd.Parameters.AddWithValue("itqty", FoBom.Rows[i]["itqty"].ToString());
                            cmd.Parameters.AddWithValue("itpareprs", FoBom.Rows[i]["itpareprs"].ToString());
                            cmd.Parameters.AddWithValue("itpkgqty", FoBom.Rows[i]["itpkgqty"].ToString());
                            cmd.Parameters.AddWithValue("itrec", (i + 1));
                            cmd.Parameters.AddWithValue("itprice", FoBom.Rows[i]["itprice"].ToString());
                            cmd.Parameters.AddWithValue("itprs", 1);
                            cmd.Parameters.AddWithValue("itmny", FoBom.Rows[i]["itmny"].ToString());
                            cmd.Parameters.AddWithValue("itnote", FoBom.Rows[i]["itnote"].ToString());
                            cmd.CommandText = "insert into fordbom ("
                                + "fono,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                                //+ "itsource,itbuypri,itbuymny,"
                                + "itprice,itprs,itmny,itnote)values("
                                + "@fono,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,"
                                + "@itprice,@itprs,@itmny,@itnote)";
                            cmd.ExecuteNonQuery();
                        }
                        FrmAffixFile.FileSave_單據存檔(DaFile, cmd, FoNo.Text.Trim(), "採購單");
                        tn.Commit();
                        tn.Dispose();
                        cmd.Dispose();
                        tempNo = FoNo.Text.Trim();
                        
                        if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            FrmFord_Print frm = new FrmFord_Print();
                            frm.PK = FoNo.Text.Trim();
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
            }
            if (btnState == "Modify")
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        tn = cn.BeginTransaction();
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Transaction = tn;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", tempNo);
                        cmd.CommandText = "delete from ford where fono=@fono";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", FoNo.Text.Trim());
                        cmd.Parameters.AddWithValue("fodate", Date.ToTWDate(FoDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("fodate1", Date.ToUSDate(FoDate.Text.Trim()));
                        cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                        cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("faname1", FaName1.Text.Trim());
                        cmd.Parameters.AddWithValue("faname2", faname2);
                        cmd.Parameters.AddWithValue("fatel1", FaTel1.Text.Trim());
                        cmd.Parameters.AddWithValue("faper1", FaPer1.Text.Trim());
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
                        cmd.Parameters.AddWithValue("fopayment", FoPayment.Text.Trim());
                        cmd.Parameters.AddWithValue("fomemo", FoMemo.Text.Trim());
                        cmd.Parameters.AddWithValue("recordno", FoD.Rows.Count);
                        cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                        cmd.Parameters.AddWithValue("spname", spname);
                        cmd.Parameters.AddWithValue("appdate", T.Rows[0]["appdate"].ToString());
                        cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("appscno", T.Rows[0]["appscno"].ToString());
                        cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
                        cmd.Parameters.AddWithValue("fomemo1", memo1);
                        cmd.Parameters.AddWithValue("PhotoPath", PhotoPath.GetUTF8(100));
                        if (FoOverFlag.Checked)
                            cmd.Parameters.AddWithValue("fooverflag", 1);
                        else
                            cmd.Parameters.AddWithValue("fooverflag", 0);
                        cmd.CommandText = "insert into ford ("
                        + " fono,fodate,fodate1,fqno"
                            //+",cono,coname1,coname2,taxmnyf,usrno"
                        + " ,fano,faname1,faname2,fatel1,faper1,emno,emname,xa1no,xa1name,xa1par"
                        + " ,taxmnyb,taxmny,x3no,rate,tax,taxb,totmny,totmnyb,fopayment,fomemo"
                        + " ,recordno,spno,spname,appdate,edtdate,appscno,edtscno,fomemo1,fooverflag,PhotoPath)values("

                        + " @fono,@fodate,@fodate1,@fqno"
                        + " ,@fano,@faname1,@faname2,@fatel1,@faper1,@emno,@emname,@xa1no,@xa1name,@xa1par"
                        + " ,@taxmnyb,@taxmny,@x3no,@rate,@tax,@taxb,@totmny,@totmnyb,@fopayment,@fomemo"
                        + " ,@recordno,@spno,@spname,@appdate,@edtdate,@appscno,@edtscno,@fomemo1,@fooverflag,@PhotoPath)";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", tempNo);
                        cmd.CommandText = "delete from fordd where fono=@fono";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < FoD.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fono", FoNo.Text.Trim());
                            cmd.Parameters.AddWithValue("fodate", Date.ToTWDate(FoDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("fodate1", Date.ToUSDate(FoDate.Text.Trim()));
                            cmd.Parameters.AddWithValue("esdate", Date.ToTWDate(dataGridViewT1["交貨日期", i].Value.ToString()));
                            cmd.Parameters.AddWithValue("esdate1", Date.ToUSDate(dataGridViewT1["交貨日期", i].Value.ToString()));
                            cmd.Parameters.AddWithValue("fqno", FqNo.Text.Trim());
                            cmd.Parameters.AddWithValue("qtynotin", FoD.Rows[i]["qtynotin"].ToString());
                            cmd.Parameters.AddWithValue("qtyin", FoD.Rows[i]["qtyin"].ToString());
                            cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                            cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1no", Xa1No.Text.Trim());
                            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.Trim());
                            cmd.Parameters.AddWithValue("itno", FoD.Rows[i]["itno"]);
                            cmd.Parameters.AddWithValue("itname", FoD.Rows[i]["itname"]);
                            cmd.Parameters.AddWithValue("ittrait", FoD.Rows[i]["ittrait"]);
                            cmd.Parameters.AddWithValue("itunit", FoD.Rows[i]["itunit"]);
                            cmd.Parameters.AddWithValue("punit", FoD.Rows[i]["punit"]);
                            cmd.Parameters.AddWithValue("itpkgqty", FoD.Rows[i]["itpkgqty"]);
                            cmd.Parameters.AddWithValue("qty", FoD.Rows[i]["qty"].ToDecimal());
                            cmd.Parameters.AddWithValue("pqty", FoD.Rows[i]["pqty"].ToDecimal());
                            cmd.Parameters.AddWithValue("price", FoD.Rows[i]["price"].ToDecimal());
                            cmd.Parameters.AddWithValue("priceb", FoD.Rows[i]["priceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("prs", FoD.Rows[i]["prs"].ToDecimal());
                            cmd.Parameters.AddWithValue("rate", (Rate.Text.ToDecimal() / 100).ToString("f3"));
                            cmd.Parameters.AddWithValue("taxprice", FoD.Rows[i]["taxprice"].ToDecimal());
                            cmd.Parameters.AddWithValue("taxpriceb", FoD.Rows[i]["taxpriceb"].ToDecimal());
                            cmd.Parameters.AddWithValue("mny", FoD.Rows[i]["mny"].ToDecimal());
                            cmd.Parameters.AddWithValue("mnyb", FoD.Rows[i]["mnyb"].ToDecimal());
                            cmd.Parameters.AddWithValue("memo", FoD.Rows[i]["memo"]);
                            cmd.Parameters.AddWithValue("bomid", FoNo.Text.ToString().Trim() + FoD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", FoD.Rows[i]["BomRec"]);
                            cmd.Parameters.AddWithValue("recordno", (i + 1));
                            cmd.Parameters.AddWithValue("mwidth1", FoD.Rows[i]["mwidth1"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth2", FoD.Rows[i]["mwidth2"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth3", FoD.Rows[i]["mwidth3"].ToDecimal());
                            cmd.Parameters.AddWithValue("mwidth4", FoD.Rows[i]["mwidth4"].ToDecimal());
                            cmd.Parameters.AddWithValue("pformula", FoD.Rows[i]["pformula"]);
                            cmd.Parameters.AddWithValue("itdesp1", FoD.Rows[i]["itdesp1"]);
                            cmd.Parameters.AddWithValue("itdesp2", FoD.Rows[i]["itdesp2"]);
                            cmd.Parameters.AddWithValue("itdesp3", FoD.Rows[i]["itdesp3"]);
                            cmd.Parameters.AddWithValue("itdesp4", FoD.Rows[i]["itdesp4"]);
                            cmd.Parameters.AddWithValue("itdesp5", FoD.Rows[i]["itdesp5"]);
                            cmd.Parameters.AddWithValue("itdesp6", FoD.Rows[i]["itdesp6"]);
                            cmd.Parameters.AddWithValue("itdesp7", FoD.Rows[i]["itdesp7"]);
                            cmd.Parameters.AddWithValue("itdesp8", FoD.Rows[i]["itdesp8"]);
                            cmd.Parameters.AddWithValue("itdesp9", FoD.Rows[i]["itdesp9"]);
                            cmd.Parameters.AddWithValue("itdesp10", FoD.Rows[i]["itdesp10"]);
                            cmd.CommandText = "insert into fordd("
                               + " fono,fodate,fodate1,esdate,esdate1,fqno"
                               + " ,qtynotin,qtyin"
                                //+" ,lowzero,sltflag,extflag,stName"
                               + " ,fano,emno,xa1no,xa1par,itno,itname,ittrait,itunit,punit,itpkgqty"
                               + " ,qty,pqty,price,priceb,prs,rate,taxprice,taxpriceb,mny,mnyb,memo"
                               + " ,bomid,bomrec,recordno,mwidth1,mwidth2,mwidth3,mwidth4,pformula"
                               + " ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5"
                               + " ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10)values("

                               + " @fono,@fodate,@fodate1,@esdate,@esdate1,@fqno"
                               + " ,@qtynotin,@qtyin"
                               + " ,@fano,@emno,@xa1no,@xa1par,@itno,@itname,@ittrait,@itunit,@punit,@itpkgqty"
                               + " ,@qty,@pqty,@price,@priceb,@prs,@rate,@taxprice,@taxpriceb,@mny,@mnyb,@memo"
                               + " ,@bomid,@bomrec,@recordno,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@pformula"
                               + " ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5"
                               + " ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10)";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", tempNo);
                        cmd.CommandText = "delete from fordbom where fono=@fono";
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < FoBom.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("fono", FoNo.Text.Trim());
                            cmd.Parameters.AddWithValue("bomid", FoNo.Text + FoBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                            cmd.Parameters.AddWithValue("bomrec", FoBom.Rows[i]["BomRec"].ToString());
                            cmd.Parameters.AddWithValue("itno", FoBom.Rows[i]["itno"].ToString());
                            cmd.Parameters.AddWithValue("itname", FoBom.Rows[i]["itname"].ToString());
                            cmd.Parameters.AddWithValue("itunit", FoBom.Rows[i]["itunit"].ToString());
                            cmd.Parameters.AddWithValue("itqty", FoBom.Rows[i]["itqty"].ToString());
                            cmd.Parameters.AddWithValue("itpareprs", FoBom.Rows[i]["itpareprs"].ToString());
                            cmd.Parameters.AddWithValue("itpkgqty", FoBom.Rows[i]["itpkgqty"].ToString());
                            cmd.Parameters.AddWithValue("itrec", (i + 1));
                            cmd.Parameters.AddWithValue("itprice", FoBom.Rows[i]["itprice"].ToString());
                            cmd.Parameters.AddWithValue("itprs", 1);
                            cmd.Parameters.AddWithValue("itmny", FoBom.Rows[i]["itmny"].ToString());
                            cmd.Parameters.AddWithValue("itnote", FoBom.Rows[i]["itnote"].ToString());
                            cmd.CommandText = "insert into fordbom ("
                                + "fono,bomid,bomrec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,"
                                //+ "itsource,itbuypri,itbuymny,"
                                + "itprice,itprs,itmny,itnote)values("
                                + "@fono,@bomid,@bomrec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,"
                                + "@itprice,@itprs,@itmny,@itnote)";

                            cmd.ExecuteNonQuery();
                        }
                        tn.Commit();
                        tn.Dispose();
                        cmd.Dispose();
                        tempNo = FoNo.Text.Trim();
                        if (MessageBox.Show("存檔完成，是否列印?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            FrmFord_Print frm = new FrmFord_Print();
                            frm.PK = FoNo.Text.Trim();
                            frm.ShowDialog();
                        }
                        jFord.upModify0<JBS.JS.Ford>(FoNo.Text.Trim());//更新修改狀態0
                        BomRec = 0;
                        btnAppend_Click(null, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    tn.Rollback();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = string.Empty;
            WriteToTxt(Common.load("Cancel", "ford", "fono", tempNo));
            Common.SetTextState(FormState = FormEditState.None, ref list);
            dataGridViewT1.ReadOnly = true;
            btnAppend.Focus();
            jFord.upModify0<JBS.JS.Ford>(FoNo.Text.Trim());//更新修改狀態0
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            jFord.Open<JBS.JS.Fact>(sender, row =>
            {
                FaNo.Text = row["fano"].ToString().Trim();
                FaName1.Text = row["faname1"].ToString();
                FaPer1.Text = row["faper1"].ToString().GetUTF8(10);
                FaTel1.Text = row["fatel1"].ToString();
                X3No.Text = row["fax3no"].ToString();
                EmNo.Text = row["faemno1"].ToString();
                Xa1No.Text = row["faxa1no"].ToString();

                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);
                FaNo.SelectAll();

                for (int i = 0; i < FoD.Rows.Count; i++)
                {
                    GetSystemPrice(FoD.Rows[i], i);
                    SetRow_TaxPrice(FoD.Rows[i]);
                    SetRow_Mny(FoD.Rows[i]);
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

            jFord.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                if (FaNo.Text.Trim() == TextBefore)
                    return;

                FaNo.Text = row["fano"].ToString().Trim();
                FaName1.Text = row["faname1"].ToString();
                FaPer1.Text = row["faper1"].ToString().GetUTF8(10);
                FaTel1.Text = row["fatel1"].ToString();
                X3No.Text = row["faX3No"].ToString();
                EmNo.Text = row["faemno1"].ToString();
                Xa1No.Text = row["faxa1no"].ToString();
                pVar.XX03Validate(X3No.Text, X3No, X3Name, Rate);
                pVar.Xa01Validate(Xa1No.Text, Xa1No, Xa1Name);
                pVar.EmplValidate(EmNo.Text, EmNo, EmName);

                for (int i = 0; i < FoD.Rows.Count; i++)
                {
                    GetSystemPrice(FoD.Rows[i], i);
                    SetRow_TaxPrice(FoD.Rows[i]);
                    SetRow_Mny(FoD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["fano"].ToString().Trim();
            });

        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            jFord.Open<JBS.JS.Empl>(sender, row =>
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

            jFord.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
        }

        private void FoPayment_DoubleClick(object sender, EventArgs e)
        {
            if (FoPayment.ReadOnly) return;
            using (var frm = new SOther.FrmPayNoteBrow())
            { 
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        FoPayment.Text = frm.TResult;
                        break;
                }
            }
        }

        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jFord.Open<JBS.JS.XX03>(sender, row =>
            {
                X3No.Text = row["X3No"].ToString().Trim();
                X3Name.Text = row["X3Name"].ToString();
                decimal _rate = 0;
                decimal.TryParse(row["X3Rate"].ToString(), out _rate);
                _rate = _rate * 100;
                Rate.Text = _rate.ToString("f0");

                for (int i = 0; i < FoD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(FoD.Rows[i]);
                    SetRow_Mny(FoD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["X3No"].ToString().Trim();
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

            jFord.ValidateOpen<JBS.JS.XX03>(sender, e, row =>
            {
                if (X3No.Text.Trim() == TextBefore)
                    return;

                X3No.Text = row["X3No"].ToString().Trim();
                X3Name.Text = row["X3Name"].ToString();
                decimal _rate = 0;
                decimal.TryParse(row["X3Rate"].ToString(), out _rate);
                _rate = _rate * 100;
                Rate.Text = _rate.ToString("f0");

                for (int i = 0; i < FoD.Rows.Count; i++)
                {
                    SetRow_TaxPrice(FoD.Rows[i]);
                    SetRow_Mny(FoD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();

                this.TextBefore = row["X3No"].ToString().Trim();
            });
        }

        private void FoNo_DoubleClick(object sender, EventArgs e)
        {
            if (FoNo.ReadOnly)
                return;

            using (var frm = new FrmFord_Print_FoNo())
            { 
                frm.TSeekNo = FaNo.Text.Trim();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    FoNo.Text = frm.TResult.Trim();

                    this.TextBefore = FoNo.Text;
                }
            }
        }

        private void FoNo_Validating(object sender, CancelEventArgs e)
        {
            if (FoNo.ReadOnly || btnCancel.Focused) return;

            if (FoNo.Text.Length > 0 && FoNo.Text.Trim() == "")
            {
                e.Cancel = true;
                FoNo.Text = "";
                FoNo.Focus();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnState == "Append")
            {
                if (jFord.IsExistDocument<JBS.JS.Ford>(FoNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnState == "Duplicate")
            {
                if (jFord.IsExistDocument<JBS.JS.Ford>(FoNo.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show("此單據編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnState == "Modify")
            {
                var dr = Common.load("Check", "ford", "fono", FoNo.Text.Trim());
                if (dr != null)
                {
                    if (TextBefore == FoNo.Text.Trim())
                        return;

                    WriteToTxt(dr);
                    loadBom();
                }
                else
                {
                    e.Cancel = true;
                    FoNo.SelectAll();

                    using (var frm = new FrmFord_Print_FoNo())
                    { 
                        frm.TSeekNo = FoNo.Text.Trim();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            FoNo.Text = frm.TResult.Trim();
                            WriteToTxt(Common.load("Check", "ford", "fono", FoNo.Text.Trim()));
                            loadBom();
                        }
                    }
                }
            }
        }

        private void FoDate_Validating(object sender, CancelEventArgs e)
        {
            if (FoDate.ReadOnly == true) return;
            if (btnCancel.Focused) return;
            TextBox tb = sender as TextBox;

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

            //decimal tax = Tax.Text.ToDecimal();
            //decimal taxmny = TaxMny.Text.ToDecimal();
            //TotMny.Text = Math.Round(tax + taxmny, Common.MFT, MidpointRounding.AwayFromZero).ToString();
        }

        private void FoMemo_DoubleClick(object sender, EventArgs e)
        {
            pVar.MemoMemoOpenForm(FoMemo, 60);
        }

        private void FqNo_DoubleClick(object sender, EventArgs e)
        {
            if (FqNo.ReadOnly)
                return;

            FqNo.CausesValidation = false;

            using (var frm = new FrmFQuotToShop())
            {  
                frm.SeekNo = FqNo.Text.Trim();

                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                jFord.RemoveEmptyRowOnSaving(dataGridViewT1, ref FoD, ref FoBom, SetAllMny);

                this.PassFQuotM(frm);

                this.PassFQuotD(frm);

            }

            FqNo.Tag = FqNo.Text.Trim();
            SetAllMny();

            FqNo.CausesValidation = true;
        }
        private void FqNo_Validating(object sender, CancelEventArgs e)
        {
            if (FqNo.ReadOnly)
                return;

            if (btnCancel.Focused)
                return;

            if (FqNo.TrimTextLenth() == 0)
            {
                FqNo.Tag = "";
                return;
            }

            if (FqNo.Tag == null)
                FqNo.Tag = "";

            if (jFord.IsExistDocument<JBS.JS.FQuot>(FqNo.Text.Trim()) == true)
            {
                if (FqNo.Tag.ToString() == FqNo.Text.Trim())
                    return;

                using (var frm = new FrmFQuotToShop())
                { 
                    frm.FaNo = FaNo.Text.Trim();
                    frm.SeekNo = FqNo.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jFord.RemoveEmptyRowOnSaving(dataGridViewT1, ref FoD, ref FoBom, SetAllMny);

                    this.PassFQuotM(frm);

                    this.PassFQuotD(frm);
                }

                FqNo.Tag = FqNo.Text.Trim();
                SetAllMny();
            }
            else
            {
                e.Cancel = true;
                using (var frm = new FrmFQuotToShop())
                {
                    frm.FaNo = FaNo.Text.Trim();
                    frm.SeekNo = FqNo.Text.Trim();

                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    jFord.RemoveEmptyRowOnSaving(dataGridViewT1, ref FoD, ref FoBom, SetAllMny);

                    this.PassFQuotM(frm);

                    this.PassFQuotD(frm);
                }

                FqNo.Tag = FqNo.Text.Trim();
                SetAllMny();
            }
        }
        private void PassFQuotM(FrmFQuotToShop frm)
        {
            if (frm.MasterRow == null)
                return;

            FqNo.Text = frm.FqNo;
            EmNo.Text = frm.MasterRow["emno"].ToString().Trim();
            EmName.Text = frm.MasterRow["EmName"].ToString().Trim();
            FaNo.Text = frm.MasterRow["FaNo"].ToString();
            FaName1.Text = frm.MasterRow["FaName1"].ToString();
            FaPer1.Text = frm.MasterRow["FaPer1"].ToString();
            FaTel1.Text = frm.MasterRow["FaTel1"].ToString();
            Xa1No.Text = frm.MasterRow["Xa1No"].ToString();
            Xa1Name.Text = frm.MasterRow["Xa1Name"].ToString();
            Xa1Par.Text = frm.MasterRow["Xa1Par"].ToString();
            TaxMny.Text = frm.MasterRow["TaxMny"].ToDecimal().ToString("f" + Common.MFT);
            TaxMnyB.Text = frm.MasterRow["TaxMnyB"].ToDecimal().ToString("f" + Common.M);
            X3No.Text = frm.MasterRow["X3No"].ToString();
            Rate.Text = (frm.MasterRow["Rate"].ToDecimal() * 100).ToString("f0");
            FoPayment.Text = frm.MasterRow["fqPayment"].ToString();
            FoMemo.Text = frm.MasterRow["fqMemo"].ToString();
            Tax.Text = frm.MasterRow["Tax"].ToDecimal().ToString("f" + Common.TF);
            TotMny.Text = frm.MasterRow["TotMny"].ToDecimal().ToString("f" + Common.MFT);
            this.memo1 = frm.MasterRow["fqmemo1"].ToString();//詳細備註
            PhotoPath = frm.MasterRow["PhotoPath"].ToString();
            jFord.Validate<JBS.JS.XX03>(X3No.Text, row =>
            {
                X3No.Text = row["x3no"].ToString();
                X3Name.Text = row["x3name"].ToString();
            });
        }
        private void PassFQuotD(FrmFQuotToShop frm)
        {
            FoD.Clear();
            FoBom.Clear();

            DataRow row = null;
            DataTable dtD = frm.dtFQuotD;

            for (int i = 0; i < dtD.Rows.Count; i++)
            {
                var rec = GetBomRec();
                row = FoD.NewRow();
                row["fqno"] = frm.FqNo.Trim();
                row["itno"] = dtD.Rows[i]["itno"].ToString();
                row["itname"] = dtD.Rows[i]["itname"].ToString();
                row["ittrait"] = dtD.Rows[i]["ittrait"].ToString();
                if (row["ittrait"].ToString().Trim() == "1")
                    row["產品組成"] = "組合品";
                if (row["ittrait"].ToString().Trim() == "2")
                    row["產品組成"] = "組裝品";
                if (row["ittrait"].ToString().Trim() == "3")
                    row["產品組成"] = "單一商品";
                row["itunit"] = dtD.Rows[i]["itunit"].ToString();
                row["ItNoUdf"] = dtD.Rows[i]["ItNoUdf"].ToString();
                row["punit"] = getPUnit(dtD.Rows[i]["itno"].ToString().Trim());
                row["itpkgqty"] = dtD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q);
                row["qty"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                row["pqty"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);
                row["price"] = dtD.Rows[i]["price"].ToDecimal("f" + Common.MF);
                row["prs"] = dtD.Rows[i]["prs"].ToDecimal("f3");
                row["taxprice"] = dtD.Rows[i]["taxprice"].ToDecimal("f6");
                row["mny"] = dtD.Rows[i]["mny"].ToDecimal("f" + Common.TPF);
                row["priceb"] = dtD.Rows[i]["priceb"].ToDecimal("f" + Common.M);
                row["taxpriceb"] = dtD.Rows[i]["taxpriceb"].ToDecimal("f6");
                row["mnyb"] = dtD.Rows[i]["mnyb"].ToDecimal("f" + Common.M);
                row["qtynotin"] = dtD.Rows[i]["qty"].ToDecimal("f" + Common.Q);//採購未交量
                row["qtyin"] = 0;
                if (Common.User_DateTime == 1)
                    row["esdate"] = Date.ToTWDate(FoDate.Text.Trim());
                else
                    row["esdate"] = Date.ToUSDate(FoDate.Text.Trim());

                row["BomRec"] = rec;
                row["memo"] = dtD.Rows[i]["memo"].ToString();
                for (int j = 1; j <= 10; j++)
                {
                    row["itdesp" + j] = dtD.Rows[i]["itdesp" + j].ToString();
                }
                FoD.Rows.Add(row);
                FoD.AcceptChanges();
                dataGridViewT1.InvalidateRow(FoD.Rows.Count - 1);

                if (dtD.Rows[i]["ittrait"].ToString().Trim() == "3")
                    continue;

                var fqbomid = dtD.Rows[i]["bomid"].ToString().Trim();
                jFord.GetTBom<JBS.JS.FQuot>(fqbomid, rec.ToString(), ref FoBom);
            }
        }

        string getPUnit(string itno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("itno", itno.ToString().Trim());
                cmd.CommandText = "select punit from item where itno=@itno";
                if (cmd.ExecuteScalar().IsNullOrEmpty())
                    return "";
                else
                    return cmd.ExecuteScalar().ToString();
            }
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            jFord.Open<JBS.JS.Spec>(sender, row =>
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

            jFord.ValidateOpen<JBS.JS.Spec>(sender, e, row =>
            {
                SpNo.Text = row["SpNo"].ToString().Trim();
                this.spname = row["spname"].ToString().Trim();
            });
        }

        private void dataGridViewT1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
            {
                dataGridViewT1["序號", i].Value = (i + 1).ToString();
            }
        }

        private void dataGridViewT1_Click(object sender, EventArgs e)
        {
            if (FaNo.Text == "") return;
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
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "採購數量")
            {
                TextBefore = dataGridViewT1["採購數量", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                ItNoBegin = UdfNoBegin = "";
                TextBefore = ItNoBegin = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == "")
                    return;

                jFord.Validate<JBS.JS.Item>(ItNoBegin, reader =>
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
            if (dataGridViewT1.SelectedRows.Count == 0) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                if (FoD.Rows[e.RowIndex]["qty"].ToDecimal() != FoD.Rows[e.RowIndex]["qtynotin"].ToDecimal())
                {
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jFord.DataGridViewOpen<JBS.JS.Item>(sender, e, FoD, row => FillItem(row, e.RowIndex));

            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.單位))
            {
                if (FoD.Rows[e.RowIndex]["qty"].ToDecimal() != FoD.Rows[e.RowIndex]["qtynotin"].ToDecimal())
                {
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var itno = FoD.Rows[e.RowIndex]["itno"].ToString().Trim();
                var unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
                var row = Common.load("Check", "item", "itno", itno);
                if (row != null)
                {
                    if (row != null && unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunit"].ToString();
                        FoD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                    else
                    {
                        if (row["itunitp"].ToString().Length == 0)
                        {
                            unit = row["itunit"].ToString();
                            FoD.Rows[e.RowIndex]["itpkgqty"] = 1;
                        }
                        else
                        {
                            unit = row["itunitp"].ToString();

                            var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                            if (itpkgqty == 0)
                                itpkgqty = 1;
                            FoD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                        }
                    }

                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = unit;

                    FoD.Rows[e.RowIndex]["itunit"] = unit;
                    dataGridViewT1.InvalidateRow(e.RowIndex);

                    //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                    if (Common.Sys_DBqty == 1)//1代表一般進銷存
                    {
                        GetSystemPrice(FoD.Rows[e.RowIndex], e.RowIndex);
                        SetRow_TaxPrice(FoD.Rows[e.RowIndex]);
                        SetRow_Mny(FoD.Rows[e.RowIndex]);

                        dataGridViewT1.InvalidateRow(e.RowIndex);
                        SetAllMny();
                    }
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

                        FoD.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.採購數量))
            {
                if (FoD.Rows[e.RowIndex]["qty"].ToDecimal() != FoD.Rows[e.RowIndex]["qtynotin"].ToDecimal())
                {
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Common.Sys_DBqty == 1)
                {
                    using (var frm = new FrmComputer())
                    {
                        frm.w1 = FoD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = FoD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = FoD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = FoD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = FoD.Rows[e.RowIndex]["Pformula"].ToString();
                        switch (frm.ShowDialog())
                        {
                            case DialogResult.OK:
                                FoD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                                FoD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                                FoD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                                FoD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                                FoD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;

                                if (dataGridViewT1.EditingControl != null)
                                    dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);

                                FoD.Rows[e.RowIndex]["Qty"] = frm.resultCount.ToString("f" + Common.Q);
                                FoD.Rows[e.RowIndex]["PQty"] = frm.resultCount.ToString("f" + Common.Q);
                                FoD.Rows[e.RowIndex]["qtynotin"] = frm.resultCount.ToString("f" + Common.Q);

                                SetRow_Mny(FoD.Rows[e.RowIndex]);
                                dataGridViewT1.InvalidateRow(e.RowIndex);
                                SetAllMny();
                                break;
                            case DialogResult.Cancel: break;
                        }
                    }
                    if (dataGridViewT1.EditingControl != null)
                        ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.計價數量))
            {
                if (Common.Sys_DBqty == 2)
                {
                    using (FrmComputer frm = new FrmComputer())
                    {
                        frm.w1 = FoD.Rows[e.RowIndex]["mwidth1"].ToDecimal();
                        frm.w2 = FoD.Rows[e.RowIndex]["mwidth2"].ToDecimal();
                        frm.w3 = FoD.Rows[e.RowIndex]["mwidth3"].ToDecimal();
                        frm.w4 = FoD.Rows[e.RowIndex]["mwidth4"].ToDecimal();
                        frm.Pformula = FoD.Rows[e.RowIndex]["Pformula"].ToString();
                        frm.qty = FoD.Rows[e.RowIndex]["qty"].ToDecimal();
                        frm.lbTxt = "採購數量";
                        switch (frm.ShowDialog())
                        {
                            case DialogResult.OK:
                                FoD.Rows[e.RowIndex]["mwidth1"] = frm.w1;
                                FoD.Rows[e.RowIndex]["mwidth2"] = frm.w2;
                                FoD.Rows[e.RowIndex]["mwidth3"] = frm.w3;
                                FoD.Rows[e.RowIndex]["mwidth4"] = frm.w4;
                                FoD.Rows[e.RowIndex]["Pformula"] = frm.Pformula;
                                FoD.Rows[e.RowIndex]["qty"] = frm.qty;
                                FoD.Rows[e.RowIndex]["qtynotin"] = frm.qty;

                                if (dataGridViewT1.EditingControl != null)
                                    dataGridViewT1.EditingControl.Text = frm.resultCount.ToString("f" + Common.Q);

                                FoD.Rows[e.RowIndex]["Pqty"] = frm.resultCount.ToString("f" + Common.Q);

                                SetRow_Mny(FoD.Rows[e.RowIndex]);
                                dataGridViewT1.InvalidateRow(e.RowIndex);
                                SetAllMny();
                                break;
                            case DialogResult.Cancel: break;
                        }
                        if (dataGridViewT1.EditingControl != null)
                            ((TextBox)dataGridViewT1.EditingControl).SelectAll();
                    }
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.計位))
            {
                if (Common.Sys_DBqty == 2)
                {
                    using (var frm = new FrmUnit())
                    { 
                        frm.Kid = 1;
                        if (DialogResult.OK == frm.ShowDialog())
                            FoD.Rows[e.RowIndex]["punit"] = frm.Result;
                    }
                }
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly || btnCancel.Focused) return;
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;
            if (dataGridViewT1.SelectedRows.Count == 0) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.採購數量))
            {
                var qNow = dataGridViewT1["採購數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                if (qNow == TextBefore.ToDecimal())
                    return;

                if (FoD.Rows[e.RowIndex]["qty"].ToDecimal() != FoD.Rows[e.RowIndex]["qtynotin"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    FoD.Rows[e.RowIndex]["qty"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                var qty = dataGridViewT1["採購數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                if (Common.Sys_DBqty == 1)
                {
                    FoD.Rows[e.RowIndex]["qty"] = qty;
                    FoD.Rows[e.RowIndex]["pqty"] = qty;
                    FoD.Rows[e.RowIndex]["qtynotin"] = qty;
                    SetAllMny();
                }
                else if (Common.Sys_DBqty == 2)
                {
                    FoD.Rows[e.RowIndex]["qty"] = qty;
                    FoD.Rows[e.RowIndex]["qtynotin"] = qty;
                }
                SetRow_Mny(FoD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.產品編號))
            {
                string itnonow = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();

                if (ItNoBegin == itnonow)
                    return;

                if (FoD.Rows[e.RowIndex]["qty"].ToDecimal() != FoD.Rows[e.RowIndex]["qtynotin"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    FoD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                if (itnonow == "")
                {
                    FoD.Rows[e.RowIndex]["itno"] = "";
                    FoD.Rows[e.RowIndex]["itname"] = "";
                    FoD.Rows[e.RowIndex]["qty"] = 0;
                    FoD.Rows[e.RowIndex]["pqty"] = 0;
                    FoD.Rows[e.RowIndex]["itunit"] = "";
                    FoD.Rows[e.RowIndex]["punit"] = "";
                    FoD.Rows[e.RowIndex]["price"] = 0;
                    FoD.Rows[e.RowIndex]["prs"] = 0;
                    FoD.Rows[e.RowIndex]["taxprice"] = 0;
                    FoD.Rows[e.RowIndex]["mny"] = 0;
                    FoD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    FoD.Rows[e.RowIndex]["qtynotin"] = 0;
                    FoD.Rows[e.RowIndex]["產品組成"] = "";
                    FoD.Rows[e.RowIndex]["memo"] = "";
                    FoD.Rows[e.RowIndex]["priceb"] = 0;
                    FoD.Rows[e.RowIndex]["taxpriceb"] = 0;
                    FoD.Rows[e.RowIndex]["mnyb"] = 0;
                    FoD.Rows[e.RowIndex]["esdate"] = Date.ToTWDate(FoDate.Text);
                    FoD.Rows[e.RowIndex]["esdate1"] = Date.ToUSDate(FoDate.Text);
                    FoD.Rows[e.RowIndex]["mwidth1"] = 0;
                    FoD.Rows[e.RowIndex]["mwidth2"] = 0;
                    FoD.Rows[e.RowIndex]["mwidth3"] = 0;
                    FoD.Rows[e.RowIndex]["mwidth4"] = 0;
                    FoD.Rows[e.RowIndex]["Pformula"] = "";

                    SetAllMny();
                    dataGridViewT1.InvalidateRow(e.RowIndex);

                    var rec = FoD.Rows[e.RowIndex]["bomrec"].ToString().Trim();
                    jFord.RemoveBom(rec, ref FoBom);
                    return;
                }

                if (itnonow == UdfNoBegin)
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = ItNoBegin;

                    FoD.Rows[e.RowIndex]["itno"] = ItNoBegin;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jFord.DataGridViewValidateOpen<JBS.JS.Item>(sender, e, FoD, row => FillItem(row, e.RowIndex));
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.進價))
            {
                FoD.Rows[e.RowIndex]["price"] = dataGridViewT1["進價", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.MF);
                SetRow_TaxPrice(FoD.Rows[e.RowIndex]);
                SetRow_Mny(FoD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.稅前金額))
            {
                decimal beforeMny = FoD.Rows[e.RowIndex]["mny"].ToDecimal("f" + Common.TPF);
                decimal nowMny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.TPF);
                if (beforeMny == nowMny) return;
                decimal price = 0;
                decimal _qty = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                decimal taxprice = dataGridViewT1["稅前進價", e.RowIndex].EditedFormattedValue.ToString().ToDecimal("f6");
                decimal mny = dataGridViewT1["稅前金額", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.TPF);
                decimal prs = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToDecimal("f3");
                _qty = (_qty == 0) ? 1 : _qty;
                if (TextBefore.ToDecimal() == mny) return;

                FoD.Rows[e.RowIndex]["mny"] = mny;
                switch (X3No.Text)
                {
                    case "1":
                    case "3":
                    case "4":
                        price = ((mny / _qty) / prs).ToDecimal("f" + Common.MF);
                        FoD.Rows[e.RowIndex]["Price"] = price;
                        break;
                    case "2":
                        price = (((mny * (1 + Common.Sys_Rate)) / _qty) / prs).ToDecimal("f" + Common.MF);
                        FoD.Rows[e.RowIndex]["Price"] = price;
                        break;
                }
                SetRow_TaxPrice(FoD.Rows[e.RowIndex]);

                taxprice = FoD.Rows[e.RowIndex]["taxprice"].ToDecimal();
                var par = Xa1Par.Text.Trim().ToDecimal();
                FoD.Rows[e.RowIndex]["priceb"] = (price * par).ToDecimal("f" + Common.M);
                FoD.Rows[e.RowIndex]["taxpriceb"] = (taxprice * par).ToDecimal("f6");
                FoD.Rows[e.RowIndex]["mnyb"] = (mny * par).ToDecimal("f" + Common.M);

                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.單位))
            {
                string unit = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString().Trim();
                string itno = FoD.Rows[e.RowIndex]["itno"].ToString();

                if (unit == TextBefore)
                    return;

                if (FoD.Rows[e.RowIndex]["qty"].ToDecimal() != FoD.Rows[e.RowIndex]["qtynotin"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    FoD.Rows[e.RowIndex]["itunit"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }

                jFord.Validate<JBS.JS.Item>(itno, row =>
                {
                    if (unit.Length > 0 && unit == row["itunitp"].ToString().Trim())
                    {
                        unit = row["itunitp"].ToString();

                        var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                        if (itpkgqty == 0)
                            itpkgqty = 1;
                        FoD.Rows[e.RowIndex]["itpkgqty"] = itpkgqty;
                    }
                    else
                    {
                        FoD.Rows[e.RowIndex]["itpkgqty"] = 1;
                    }
                });

                FoD.Rows[e.RowIndex]["itunit"] = unit;
                dataGridViewT1.InvalidateRow(e.RowIndex);

                //計價系統，單位切換，只異動單位/包裝單位，不異動金額
                if (Common.Sys_DBqty == 1)
                {
                    GetSystemPrice(FoD.Rows[e.RowIndex], e.RowIndex);
                    SetRow_TaxPrice(FoD.Rows[e.RowIndex]);
                    SetRow_Mny(FoD.Rows[e.RowIndex]);

                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    SetAllMny();
                }
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.交貨日期))
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
                FoD.Rows[e.RowIndex]["esdate"] = Date.ToTWDate(tday.Text.Trim());
                FoD.Rows[e.RowIndex]["esdate1"] = Date.ToUSDate(tday.Text.Trim());
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.折數))
            {
                if (dataGridViewT1.Columns["折數"].ReadOnly) return;
                FoD.Rows[e.RowIndex]["prs"] = dataGridViewT1["折數", e.RowIndex].EditedFormattedValue.ToString();
                SetRow_TaxPrice(FoD.Rows[e.RowIndex]);
                SetRow_Mny(FoD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.計價數量))
            {
                FoD.Rows[e.RowIndex]["PQty"] = dataGridViewT1["計價數量", e.RowIndex].EditedFormattedValue.ToDecimal("f" + Common.Q);
                SetRow_Mny(FoD.Rows[e.RowIndex]);
                dataGridViewT1.InvalidateRow(e.RowIndex);
                SetAllMny();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Equals(this.包裝數量))
            {
                if (dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal() == TextBefore.ToDecimal())
                    return;

                if (FoD.Rows[e.RowIndex]["qty"].ToDecimal() != FoD.Rows[e.RowIndex]["qtynotin"].ToDecimal())
                {
                    e.Cancel = true;
                    MessageBox.Show("此產品已有交貨，無法更變資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = TextBefore;

                    FoD.Rows[e.RowIndex]["itpkgqty"] = TextBefore;
                    dataGridViewT1.InvalidateRow(e.RowIndex);
                    return;
                }
                FoD.Rows[e.RowIndex]["itpkgqty"] = dataGridViewT1["包裝數量", e.RowIndex].EditedFormattedValue.ToDecimal();
                dataGridViewT1.InvalidateRow(e.RowIndex);
            }
        }

        private void FillItem(SqlDataReader row, int index)
        {
            this.ItNoBegin = row["itno"].ToString();

            if (dataGridViewT1.EditingControl != null)
                dataGridViewT1.EditingControl.Text = row["itno"].ToString();

            FoD.Rows[index]["itno"] = row["itno"].ToString();
            FoD.Rows[index]["itname"] = row["itname"].ToString();
            FoD.Rows[index]["ittrait"] = row["ittrait"].ToString();
            FoD.Rows[index]["punit"] = row["punit"].ToString();
            FoD.Rows[index]["ItNoUdf"] = row["ItNoUdf"].ToString();
            if (row["ittrait"].ToString().Trim() == "1")
                FoD.Rows[index]["產品組成"] = "組合品";
            else if (row["ittrait"].ToString().Trim() == "2")
                FoD.Rows[index]["產品組成"] = "組裝品";
            else
                FoD.Rows[index]["產品組成"] = "單一商品";

            if (row["itbuyunit"].ToString().Trim() == "1")
            {
                FoD.Rows[index]["itunit"] = row["itunitp"].ToString();

                var itpkgqty = row["itpkgqty"].ToDecimal("f" + Common.Q);
                if (itpkgqty == 0)
                    itpkgqty = 1;
                FoD.Rows[index]["itpkgqty"] = itpkgqty;
            }
            else
            {
                FoD.Rows[index]["itunit"] = row["itunit"].ToString();
                FoD.Rows[index]["itpkgqty"] = 1;
            }
            GetSystemPrice(FoD.Rows[index], index);
            SetRow_TaxPrice(FoD.Rows[index]);
            SetRow_Mny(FoD.Rows[index]);

            dataGridViewT1.InvalidateRow(index);
            SetAllMny();
            for (int i = 1; i <= 10; i++)
            {
                FoD.Rows[index]["itdesp" + i] = row["itdesp" + i].ToString();
            }

            var rec = FoD.Rows[index]["BomRec"].ToString();
            jFord.RemoveBom(rec, ref FoBom);

            jFord.GetItemBom(row["itno"].ToString().Trim(), rec, ref FoBom);
        }


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
            var qty = row["Pqty"].ToDecimal("f" + Common.Q);
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
            var sum = FoD.AsEnumerable().Sum(r => r["mny"].ToDecimal("f" + Common.TPF)).ToDecimal("f" + Common.MFT);

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
                var totmny = FoD.AsEnumerable().Sum(r => r["Pqty"].ToDecimal("f" + Common.Q) * r["prs"].ToDecimal() * r["price"].ToDecimal("f" + Common.MF)).ToDecimal("f" + Common.MFT);
                tax = (totmny / (1 + Common.Sys_Rate))* Common.Sys_Rate;

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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.ToString() == "F11, Shift" && gridFactBShop.Enabled)
            {
                gridFactBShop.PerformClick();
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
            else if (str == "採購數量")
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
         
        private void gridAddr_Click(object sender, EventArgs e)
        {
            if (Enabled)
            {
                using (var frm = new FrmSale_Print_Addr())
                { 
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            FoMemo.Text = frm.TResult;
                            break;
                        case DialogResult.Cancel: break;
                    }
                }
            }
        }

        private void gridKeyMan_Click(object sender, EventArgs e)
        {
            if (FoNo.Text.Trim() == "") return;
            FrmSale_AppScNo frm = new FrmSale_AppScNo();
            //新增人員
            frm.AName = T.Rows[0]["AppScNo"].ToString();
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
                for (int i = 0; i < FoD.Rows.Count; i++)
                {
                    SetRow_Mny(FoD.Rows[i]);
                    dataGridViewT1.InvalidateRow(i);
                }
                SetAllMny();
            }
            dataGridViewT1_Click(null, null);
        }
         
        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            gridAddr.Enabled = FoOverFlag.Enabled = gridAppend.Enabled = gridDelete.Enabled = gridPicture.Enabled = gridInsert.Enabled = gridItDesp.Enabled = gridBomD.Enabled = !btnAppend.Enabled;
            gridStock.Enabled = gridTran.Enabled = gridAllTrans.Enabled = gridFactBShop.Enabled = true;
            
        }

        //選擇附件檔案路徑 13.8
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
                    cmd.Parameters.AddWithValue("Datano", FoNo.Text.Trim());
                    cmd.Parameters.AddWithValue("Datano2", FqNo.Text.Trim());
                    cmd.CommandText = "select ROW_NUMBER() OVER(ORDER BY id) AS 序號 , * from AffixFile where (DaType ='採購單' and Datano=@Datano) or (Datano=@Datano2 and DaType='詢價單')";
                    DaFile.Clear();
                    da.Fill(DaFile);
                
                    frm.DtFile = DaFile;
                    frm.CMD = cmd;
                    frm.Datano = FoNo.Text.Trim();
                    frm.DaType = "採購單";
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
