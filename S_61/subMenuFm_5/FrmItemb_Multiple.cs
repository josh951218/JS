using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmItemb_Multiple : Formbase, JBS.JS.IxOpen
    {
        JBS.JS.Item jItem;
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo; 

        List<TextBoxbase> list;
        DataTable dt = new DataTable();

        public List<string> MultipleItNo;
       
        public FrmItemb_Multiple()
        {
            InitializeComponent();
            this.jItem = new JBS.JS.Item();
            this.Style = FormStyle.Mini;
            this.list = new List<TextBoxbase>() { ItNo, ItName, ItNoUdf, KiNo, ItIme, ItDesp1 };
            ItNo.ReadOnly = ItNoUdf.ReadOnly = ItName.ReadOnly = KiNo.ReadOnly = ItIme.ReadOnly = false;

            this.dataGridViewT1.tableName = "item";
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);

            //權限設定
            this.售價.Visible = (Common.User_SalePrice || Common.User_ShopPrice);
            this.包裝售價.Visible = (Common.User_SalePrice || Common.User_ShopPrice);

            this.售價.DefaultCellStyle.Format = "f6";
            this.包裝售價.DefaultCellStyle.Format = "f6";

            this.包裝數量.Set庫存數量小數();
            this.安全存量.Set庫存數量小數();
            this.現有存量.Set庫存數量小數();

            var ime = Common.dtSysSettings.Rows[0]["ItIME"].ToString();
            if (ime.Trim().Length > 0)
            {
                this.注音速查.HeaderText = ime;
                labelT1.Text = ime;
            }

            jItem.TSQL = @"
                Select
                點選='', 產品組成= case when ittrait=1 then '組合品' when ittrait=2 then '組裝品' when ittrait=3 then '單一商品' end,
                itno, itnoudf, itname, kino, itime, ittrait, itunit, itunitp, itpkgqty, itbuypri, itprice, itcost, itbuyprip, itpricep,  
                itcostp, itdesp1, itstockqty, IsEnable, item.fano, Punit, ScNo
                ,fact.faname1 
                from item 
                left join fact on item.fano=fact.fano where 0=0 ";
        }

        public FrmItemb_Multiple(string NotShowStrait1)
        {
            InitializeComponent();
            this.jItem = new JBS.JS.Item();
            this.Style = FormStyle.Mini;
            this.list = new List<TextBoxbase>() { ItNo, ItName, ItNoUdf, KiNo, ItIme };
            ItNo.ReadOnly = ItNoUdf.ReadOnly = ItName.ReadOnly = KiNo.ReadOnly = ItIme.ReadOnly = false;

            this.dataGridViewT1.tableName = "item";
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);

            //權限設定
            this.售價.Visible = (Common.User_SalePrice || Common.User_ShopPrice);
            this.包裝售價.Visible = (Common.User_SalePrice || Common.User_ShopPrice);

            this.售價.DefaultCellStyle.Format = "f6";
            this.包裝售價.DefaultCellStyle.Format = "f6";

            this.包裝數量.Set庫存數量小數();
            this.安全存量.Set庫存數量小數();
            this.現有存量.Set庫存數量小數();

            var ime = Common.dtSysSettings.Rows[0]["ItIME"].ToString();
            if (ime.Trim().Length > 0)
            {
                this.注音速查.HeaderText = ime;
                labelT1.Text = ime;
            }

            jItem.TSQL = @"
                Select
                點選='', 產品組成= case when ittrait=1 then '組合品' when ittrait=2 then '組裝品' when ittrait=3 then '單一商品' end,
                itno, itnoudf, itname, kino, itime, ittrait, itunit, itunitp, itpkgqty, itbuypri, itprice, itcost, itbuyprip, itpricep,  
                itcostp, itdesp1, itstockqty, IsEnable, item.fano, Punit, ScNo
                ,fact.faname1 
                from item 
                left join fact on item.fano=fact.fano where ittrait <> 1 ";
        }

        private void FrmItemb_Load(object sender, EventArgs e)
        {
            dt.RowChanged += new DataRowChangeEventHandler(dt_RowChanged);

            this.點選.Visible = true;
            this.Style = FormStyle.Max;

            MultipleItNo = new List<string>();

            dataGridViewT1.DataSource = dt;

            jItem.Search(this.TSeekNo, ref dt);
            jItem.SeekCurrent(this.TSeekNo, dt, dataGridViewT1);
        }

        void dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action != DataRowAction.Add) return;
            var hasrow = MultipleItNo.FirstOrDefault(r=>r.Equals(e.Row["itno"].ToString().Trim()));
            if (hasrow != null)
                e.Row["點選"] = "V";
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            jItem.Search(this.TSeekNo, ref dt);
            jItem.SeekCurrent(this.TSeekNo, dt, dataGridViewT1);
            dataGridViewT1.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmItem())
            {
                var itno = "";
                frm.ShowDialog(out itno);
                jItem.Search(itno, ref dt);
                jItem.SeekCurrent(itno, dt, dataGridViewT1);
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "點選")
                return;

            //雙擊欄位之產品編號 若不再MultipleItNo中，才加入
            var t = MultipleItNo.Find(r=>r.Equals(dataGridViewT1["產品編號", e.RowIndex].Value.ToString().Trim()));
            if (t == null)
                this.MultipleItNo.Add(dataGridViewT1["產品編號",e.RowIndex].Value.ToString().Trim());

            btnGet_Click(null, null);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            this.TResult = "";
            if (dataGridViewT1.Rows.Count > 0)
            {
                //如果多選MultipleItNo為空，才帶回光棒那一筆
                if (MultipleItNo.Count == 0)
                {
                    string TempID = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();

                    var row = Common.load("Check", "item", "itno", TempID);

                    if (row == null)
                    {
                        MessageBox.Show("您選取的資料已被刪除");

                        jItem.Search("", ref dt);
                        jItem.SeekCurrent("", dt, dataGridViewT1);
                        return;
                    }

                    this.MultipleItNo.Add(row["itno"].ToString().Trim());
                    this.TResult = row["itno"].ToString().Trim();
                }
                MultipleItNo.Sort();
                this.DialogResult = DialogResult.OK;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.Tag != null && this.Tag.ToString() == "delete")
                this.DialogResult = DialogResult.Yes;

            base.OnFormClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2) btnAppend.PerformClick();
            else if (keyData == Keys.F6) btnQuery.PerformClick();
            else if (keyData == Keys.F9) btnGet.PerformClick();
            else if (keyData == Keys.F11) btnExit.PerformClick();
            else if (keyData.ToString() == "Tab")
            {
                if (!dataGridViewT1.Focused)
                {
                    if (dataGridViewT1.Rows.Count == 0) return true;
                    dataGridViewT1.CurrentCell = dataGridViewT1[0, dataGridViewT1.CurrentCell.RowIndex];
                    dataGridViewT1.Focus();
                    return true;
                }
                else
                {
                    ItNo.Focus();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dt.Rows.Count == 0) return;
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == this.點選.Name)
            {
                dt.Rows[e.RowIndex]["點選"] = dt.Rows[e.RowIndex]["點選"].ToString().Trim() == "V" ? "" : "V";
                dataGridViewT1.InvalidateRow(e.RowIndex);

                if (dt.Rows[e.RowIndex]["點選"].ToString().Trim() == "V")
                {
                    MultipleItNo.Add(dt.Rows[e.RowIndex]["itno"].ToString().Trim());
                }
                else
                {
                    MultipleItNo.Remove(dt.Rows[e.RowIndex]["itno"].ToString().Trim());
                }
            }
        }

        private void ItNo_TextChanged(object sender, EventArgs e)
        {
            if (ItNo.Text.Trim() == ""
                && ItName.Text.Trim() == ""
                && KiNo.Text.Trim() == ""
                && ItIme.Text.Trim() == ""
                && ItNoUdf.Text.Trim() == ""
                && ItDesp1.Text.Trim() =="")
            {
                jItem.Search("", ref dt);
                jItem.SeekCurrent("", dt, dataGridViewT1);
                return;
            }

            if (ItNo.TrimTextLenth() > 0)
            {
                jItem.Search(ItNo.Text.Trim(), ref dt);
                jItem.SeekCurrent(ItNo.Text.Trim(), dt, dataGridViewT1);
            }
            else
            {
                OtherQuery();
            }
        }

        private void OtherQuery()
        {
            if (list.All(t => t.TrimTextLenth() == 0))
                return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = jItem.TSQL;
                foreach (TextBoxT tb in list)
                {
                    if (tb.TrimTextLenth() > 0)
                    {
                        if (tb.Name != "ItDesp1")
                        {
                            cmd.Parameters.AddWithValue(tb.Name, tb.Text);
                            cmd.CommandText += " and " + tb.Name + " like '%'+@" + tb.Name + "+'%'";
                        }
                        else 
                        {
                            cmd.Parameters.AddWithValue("ItDesp", tb.Text);
                            cmd.CommandText += @"
                            and
                            ( 
                                ItDesp1 like '%'+@ItDesp+'%' or
                                ItDesp2 like '%'+@ItDesp+'%' or
                                ItDesp3 like '%'+@ItDesp+'%' or
                                ItDesp4 like '%'+@ItDesp+'%' or
                                ItDesp5 like '%'+@ItDesp+'%' or
                                ItDesp6 like '%'+@ItDesp+'%' or
                                ItDesp7 like '%'+@ItDesp+'%' or
                                ItDesp8 like '%'+@ItDesp+'%' or
                                ItDesp9 like '%'+@ItDesp+'%' or
                                ItDesp10 like '%'+@ItDesp+'%' 
                            )
                            ";
                        }
                    }
                }

                var ob = string.Join(",", list.Where(t => t.TrimTextLenth() > 0).Select(t => t.Name));
                cmd.CommandText += " order by ";
                cmd.CommandText += ob;

                dt.Clear();
                da.Fill(dt);
            }

            if (ItNo.TrimTextLenth() > 0)
                jItem.SeekCurrent(ItNo.Text, dt, dataGridViewT1, ItNo.Name);
            else if (ItNoUdf.TrimTextLenth() > 0)
                jItem.SeekCurrent(ItNoUdf.Text, dt, dataGridViewT1, ItNoUdf.Name);
            else if (ItName.TrimTextLenth() > 0)
                jItem.SeekCurrent(ItName.Text, dt, dataGridViewT1, ItName.Name);
            else if (ItIme.TrimTextLenth() > 0)
                jItem.SeekCurrent(ItIme.Text, dt, dataGridViewT1, ItIme.Name);
            else if (KiNo.TrimTextLenth() > 0)
                jItem.SeekCurrent(KiNo.Text, dt, dataGridViewT1, KiNo.Name);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (list.All(t => t.TrimTextLenth() == 0))
                return;


            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = jItem.TSQL;
                foreach (TextBoxT tb in list)
                {
                    if (tb.TrimTextLenth() > 0)
                    {
                        if (tb.Name != "ItDesp1")
                        {
                            cmd.Parameters.AddWithValue(tb.Name, tb.Text);
                            cmd.CommandText += " and " + tb.Name + " like '%'+@" + tb.Name + "+'%'";
                        }
                        else 
                        {
                            cmd.Parameters.AddWithValue("ItDesp", tb.Text);
                            cmd.CommandText += @"
                            and
                            ( 
                                ItDesp1 like '%'+@ItDesp+'%' or
                                ItDesp2 like '%'+@ItDesp+'%' or
                                ItDesp3 like '%'+@ItDesp+'%' or
                                ItDesp4 like '%'+@ItDesp+'%' or
                                ItDesp5 like '%'+@ItDesp+'%' or
                                ItDesp6 like '%'+@ItDesp+'%' or
                                ItDesp7 like '%'+@ItDesp+'%' or
                                ItDesp8 like '%'+@ItDesp+'%' or
                                ItDesp9 like '%'+@ItDesp+'%' or
                                ItDesp10 like '%'+@ItDesp+'%' 
                            )
                            ";
                        }
                    }
                    tb.Enter -= new EventHandler(Text_OnEnter);
                    tb.Enter += new EventHandler(Text_OnEnter);
                }
                var ob = string.Join(",", list.Where(t => t.TrimTextLenth() > 0).Select(t => t.Name));
                cmd.CommandText += " order by ";
                cmd.CommandText += ob;

                dt.Clear();
                da.Fill(dt);
            }
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            foreach (TextBoxT tb in list)
            {
                tb.Clear();
                tb.Enter -= new EventHandler(Text_OnEnter);
            }

            jItem.Search("", ref dt);
            jItem.SeekCurrent("", dt, dataGridViewT1);
        }

        private void btnShowSelected_Click(object sender, EventArgs e)
        {
            if (MultipleItNo.Count == 0)
            {
                MessageBox.Show("目前無多選紀錄");
                return;
            }
            string values = string.Join("','", MultipleItNo.ToArray());
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = jItem.TSQL + " and itno in('" + values + "')";
                dt.Clear();
                da.Fill(dt);
            }
        }
    }
}
