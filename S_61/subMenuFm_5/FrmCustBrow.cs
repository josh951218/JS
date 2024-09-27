using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using JE.MyControl;
using S_61.Basic;
using S_61.SOther;

namespace JE.S5
{
    public partial class FrmCustBrow : Formbase
    {
        public DataRow Result { get; set; }

        List<TextBoxbase> list;
        DataTable table = new DataTable();
        string SearchItem = "CuNo,CuName1,CuPer1,CuTel1,CuAtel1,CuFax1,CuPer,CuR1,CuAddr1,CuName2,CuXa1No,CuIme,CuX1No,CuEmno1,Cuudf1";

        public FrmCustBrow(bool AllowAppend,FormStyle style = FormStyle.Max,bool WorkForBrow = true)
        {
            this.Style = style;
            InitializeComponent();
            list = this.getEnumMember();
            btnAppend.Enabled = AllowAppend;

            if (!WorkForBrow)
            {
                this.行動電話一.Visible = false;
                this.傳真.Visible = false;
                this.負責人.Visible = false;
                this.交易幣別.Visible = false;
                this.注音速查.Visible = false;
                this.客戶類別.Visible = false;
                this.業務人員.Visible = false;
            }
        }

        private void FrmCustBrow_Load(object sender, EventArgs e)
        {
            LoadDB(this.SeekNo);
            CuNo.Focus();
        }

        void LoadDB(string str)
        {
            try
            {
                table.Clear();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("key", str);

                    if (str.Trim() == "")
                    {
                        cmd.CommandText = "select top(" + Common.SearchCount + ")" + SearchItem + " from cust";
                        dd.Fill(table);
                    }
                    else
                    {
                        cmd.CommandText = "select top(" + Common.SearchCount / 2 + ")" + SearchItem + " from cust where cuno <=@key order by cuno desc";
                        dd.Fill(table);

                        cmd.CommandText = "select top(" + Common.SearchCount / 2 + ")" + SearchItem + " from cust where cuno >@key";
                        dd.Fill(table);
                    }

                }

                dataGridViewT1.SuspendLayout();
                dataGridViewT1.DataSource = null;

                dataGridViewT1.DataSource = table;
                dataGridViewT1.ResumeLayout();

                GridViewSeekRow(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
            }
        }

        void GridViewSeekRow(string value)
        {
            int index = 0;
            if (value.Trim() != "")
            {
                index = table.Rows.Count - 1;
                System.Threading.Tasks.Parallel.For(0, table.Rows.Count, r =>
                {
                    //找到一樣一筆，直接離開
                    if (table.Rows[r]["cuno"].ToString().Trim() == value.Trim())
                    {
                        index = r;
                        return;
                    }
                    else if (table.Rows[r]["cuno"].ToString().Trim().StartsWith(value.Trim()))
                    {
                        if (r < index)
                            index = r;
                    }
                    //若找到相近一筆，要比r，比較小的給index

                });
                dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
                dataGridViewT1.Rows[index].Selected = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Result = null;
            this.Dispose();
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            using (FrmCust frm = new FrmCust())
            {
                this.SeekNo = frm.ShowDialogCallBack();
                LoadDB(SeekNo);
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index >= 0)
                {
                    Result = Common.load("Check", "cust", "cuno", table.DefaultView[index]["cuno"].ToString());
                    this.SeekNo = dataGridViewT1["客戶編號", index].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }



        //int findindex(string name, string value)
        //{
        //    int index = -1;
        //    if (value.Trim() != "")
        //    {
        //        System.Threading.Tasks.Parallel.For(0, table.Rows.Count, r =>
        //        {
        //            //找到一樣一筆，直接離開
        //            if (table.Rows[r][name].ToString().Trim() == value.Trim())
        //            {
        //                index = r;
        //                return;
        //            }
        //        });
        //    }
        //    return index;
        //}
        ////搜尋規則:1先找已顯示的筆數是否有否合2若沒有則去資料庫拉值3再鎖定最接近一筆
        //private void textBoxT1_TextChanged(object sender, EventArgs e)
        //{
        //    dataGridViewT1.SuspendLayout();
        //    if (CuNo.TrimTextLenth() > 0)
        //    {
        //        if (findindex("CuNo", CuNo.Text.Trim()) == -1)
        //        {
        //            Common.Nload(ref table, "CuNo", sql, "CuNo", CuNo.Text.Trim());
        //            GridViewSeekRow("CuNo", CuNo.Text.Trim());
        //        }
        //    }
        //    else if (CuName1.TrimTextLenth() > 0)
        //    {
        //        if (findindex("CuName1", CuName1.Text.Trim()) == -1)
        //        {
        //            Common.Nload(ref table, "CuName1", sql, "CuName1", CuName1.Text.Trim());
        //            GridViewSeekRow("CuName1", CuName1.Text.Trim());
        //        }
        //    }
        //    else if (CuPer1.TrimTextLenth() > 0)
        //    {
        //        if (findindex("CuPer1", CuPer1.Text.Trim()) == -1)
        //        {
        //            Common.Nload(ref table, "CuPer1", sql, "CuPer1", CuPer1.Text.Trim());
        //            GridViewSeekRow("CuPer1", CuPer1.Text.Trim());
        //        }
        //    }
        //    else if (CuTel1.TrimTextLenth() > 0)
        //    {
        //        if (findindex("CuTel1", CuTel1.Text.Trim()) == -1)
        //        {
        //            Common.Nload(ref table, "CuTel1", sql, "CuTel1", CuTel1.Text.Trim());
        //            GridViewSeekRow("CuTel1", CuTel1.Text.Trim());
        //        }
        //    }
        //    else if (CuAtel1.TrimTextLenth() > 0)
        //    {
        //        if (findindex("CuAtel1", CuAtel1.Text.Trim()) == -1)
        //        {
        //            Common.Nload(ref table, "CuAtel1", sql, "CuAtel1", CuAtel1.Text.Trim());
        //            GridViewSeekRow("CuAtel1", CuAtel1.Text.Trim());
        //        }
        //    }
        //    else if (CuIme.TrimTextLenth() > 0)
        //    {
        //        if (findindex("CuIme", CuIme.Text.Trim()) == -1)
        //        {
        //            Common.Nload(ref table, "CuIme", sql, "CuIme", CuIme.Text.Trim());
        //            GridViewSeekRow("CuIme", CuIme.Text.Trim());
        //        }
        //    }
        //    else if (CuUdf1.TrimTextLenth() > 0)
        //    {
        //        if (findindex("CuUdf1", CuUdf1.Text.Trim()) == -1)
        //        {
        //            Common.Nload(ref table, "CuUdf1", sql, "CuUdf1", CuUdf1.Text.Trim());
        //            GridViewSeekRow("CuUdf1", CuUdf1.Text.Trim());
        //        }
        //    }
        //    dataGridViewT1.ResumeLayout();
        //}

        private void btnQuery_Click(object sender, EventArgs e)
        {
            var HasSearchCode = list.Any(r => r.TrimTextLenth() > 0);
            if (!HasSearchCode) return;
            try
            {
                table.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.CommandText = "Select " + SearchItem + " From cust where 0=0 ";

                    if (CuNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@CuNo", CuNo.Text.Trim());
                        cmd.CommandText += " And CuNo like '%'+@CuNo+'%' ";
                    }
                    if (CuName1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@CuName1", CuName1.Text.Trim());
                        cmd.CommandText += " And CuName1 like '%'+@CuName1+'%' ";
                    }
                    if (CuPer1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@CuPer1", CuPer1.Text.Trim());
                        cmd.CommandText += " And CuPer1 like '%'+@CuPer1+'%' ";
                    }
                    if (CuTel1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@CuTel1", CuTel1.Text.Trim());
                        cmd.CommandText += " And CuTel1 like '%'+@CuTel1+'%' ";
                    }
                    if (CuIme.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@CuIme", CuIme.Text.Trim());
                        cmd.CommandText += " And CuIme like '%'+@CuIme+'%' ";
                    }
                    if (CuUdf1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@CuUdf1", CuUdf1.Text.Trim());
                        cmd.CommandText += " And CuUdf1 like '%'+@CuUdf1+'%' ";
                    }
                    if (CuAtel1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@CuAtel1", CuAtel1.Text.Trim());
                        cmd.CommandText += " And CuAtel1 like '%'+@CuAtel1+'%' ";
                    }
                    dataGridViewT1.SuspendLayout();
                    dataGridViewT1.DataSource = null;
                    da.Fill(table);
                }
                CuNo.Enter += new EventHandler(Text_OnEnter);
                CuName1.Enter += new EventHandler(Text_OnEnter);
                CuPer1.Enter += new EventHandler(Text_OnEnter);
                CuTel1.Enter += new EventHandler(Text_OnEnter);
                CuIme.Enter += new EventHandler(Text_OnEnter);
                CuUdf1.Enter += new EventHandler(Text_OnEnter);
                CuAtel1.Enter += new EventHandler(Text_OnEnter);

                dataGridViewT1.DataSource = table;
                dataGridViewT1.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            LoadDB(SeekNo);
            foreach (Control tb in (sender as Control).Parent.Controls)
            {
                if (tb is TextBox)
                {
                    (tb as TextBox).Text = "";
                    (tb as TextBox).Enter -= Text_OnEnter;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                btnAppend.Focus();
                btnAppend.PerformClick();
            }
            else if (keyData == Keys.F6)
            {
                btnQuery.Focus();
                btnQuery.PerformClick();
            }
            else if (keyData == Keys.F9)
            {
                dataGridViewT1.Focus();
                btnGet_Click(null, null);
            }
            else if (keyData == Keys.F11)
            {
                this.Dispose();
            }
            else if (keyData == Keys.Escape)
            {
                this.Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
