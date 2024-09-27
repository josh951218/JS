using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using System.Data.SqlClient;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class 指送地址 : Formbase
    {
        public string cuno = ""; //客戶編號
        public string addr = ""; //指送地址
        public string tel = "";  //電話
        public string per = "";  //聯絡人
        public string name = ""; //公司名稱
        int 客戶基本檔Count = 0;
        DataTable 指送地址dt = new DataTable();       //指送地址DataSource(新增用)
        string BeforeText_指送地址 = "";
        List<string> Update指送地址 = new List<string>();
        List<string> Insert指送地址 = new List<string>();

        public 指送地址()
        {
            InitializeComponent();
        }

        private void 指送地址_Load(object sender, EventArgs e)
        {
            function("Load");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            地址.Width = (int)((13 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            公司名稱.Width = (int)((13 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            聯絡人.Width =  (int)((13 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            電話.Width = (int)((13 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
        }
        
        private void dataGridViewT2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);  

            if (dataGridViewT1.ReadOnly) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if (CurrentColumnName == "預設列印")
            {
                if (指送地址dt.Rows[e.RowIndex]["DefaultPrint"].ToString() == "X") return;
                if (指送地址dt.Rows[e.RowIndex]["DefaultPrint"].ToString().Trim().ToUpper() == "V")
                {
                    指送地址dt.Rows[e.RowIndex]["DefaultPrint"] = "";
                }
                else
                {
                    for (int i = 0; i < 指送地址dt.Rows.Count; i++)
                    {
                        if (指送地址dt.Rows[i]["DefaultPrint"].ToString() == "X" || 指送地址dt.Rows[i]["Dano"].ToString() == "X") continue; //////////
                        指送地址dt.Rows[i]["DefaultPrint"] = "";
                    }
                    指送地址dt.Rows[e.RowIndex]["DefaultPrint"] = "V";
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            function("Search");
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.ReadOnly == false) return;

            if (指送地址dt.Rows.Count <= 0) this.DialogResult = DialogResult.OK;
            else
            {
                int i = dataGridViewT1.CurrentCell.RowIndex;
                this.addr = 指送地址dt.Rows[i]["addr"].ToString();
                this.per = 指送地址dt.Rows[i]["per1"].ToString();
                this.tel = 指送地址dt.Rows[i]["Tel"].ToString();
                this.addr = 指送地址dt.Rows[i]["addr"].ToString();
                this.name = 指送地址dt.Rows[i]["name"].ToString();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void AddDataGridView1Row()
        {
            int max = 指送地址dt.Rows.Count, now = 0;
            if (dataGridViewT1.CurrentCell.RowIndex < max - 1) return;
            for (int i = 0; i < 指送地址dt.Rows.Count; i++)
            {
                if (指送地址dt.Rows[i]["dano"].ToString().Trim().Length > 0) now += 1; ;
            }
            if (dataGridViewT1["指送編碼", dataGridViewT1.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim().Length > 0) now += 1;
            if (now >= max)
            {
                DataRow dr = 指送地址dt.NewRow();
                dr["序號"] = (指送地址dt.Rows.Count + 1).ToString();
                指送地址dt.Rows.Add(dr);
                dataGridViewT1.InvalidateRow(dataGridViewT1.CurrentCell.RowIndex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            控制指送地址Column編輯狀態(true);
            編輯指送地址Btn狀態(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            控制指送地址Column編輯狀態(false);
            編輯指送地址Btn狀態(true);
            function("Duplicate_Save");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            控制指送地址Column編輯狀態(false);
            編輯指送地址Btn狀態(true);
            function("load");
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            BeforeText_指送地址 = dataGridViewT1.CurrentCell.EditedFormattedValue.ToString().Trim();
        }

        private void dataGridViewT1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string 序號 = 指送地址dt.Rows[dataGridViewT1.CurrentCell.RowIndex]["序號"].ToString();
            string LeastText_指送地址 = dataGridViewT1.CurrentCell.EditedFormattedValue.ToString().Trim();
            if (BeforeText_指送地址 != LeastText_指送地址 && Update指送地址.IndexOf(序號) == -1 && Insert指送地址.IndexOf(序號) == -1)
            {
                if (指送地址dt.Rows[dataGridViewT1.CurrentCell.RowIndex]["id"].ToString() != "")
                    Update指送地址.Add(序號);
                else
                    Insert指送地址.Add(序號);
            }
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
        }

        private void dataGridViewT1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                string str = dataGridViewT1.CurrentCell.OwningColumn.Name;
                TextBox t = (TextBox)e.Control;
                t.TextChanged -= new EventHandler(t_TextChanged);
                if (str == "郵遞區號")
                {
                    t.TextChanged += new EventHandler(t_TextChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void function(string state)
        {
            if (state.ToLower() == "load")
            {
                #region Load
                Insert指送地址.Clear();
                Update指送地址.Clear();
                指送地址dt.Clear();
                using (SqlConnection cn = new SqlConnection(S_61.Basic.Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("cuno", cuno);
                    #region load 客戶基本檔地址
                    cmd.CommandText = @"
select ROW_NUMBER() OVER(ORDER BY UnionDt.cuno) as 序號 ,* from(
                    select cuno,cuaddr1 as addr,cur1 as zip,cuper1 as per1,cutel1 as tel,dano='公司地址',cuname2 as name ,defaultPrint='X' from cust where cuno =@cuno and  cuaddr1 !=''
                    union all
                    select cuno,cuaddr2 as addr,cur2 as zip,cuper1 as per1,cutel1 as tel,dano='發票地址',cuname2 as name ,defaultPrint='X' from cust where cuno =@cuno and  cuaddr2 !=''
                    union all
                    select cuno,cuaddr3 as addr,cur3 as zip,cuper1 as per1,cutel1 as tel,dano='送貨地址',cuname2 as name ,defaultPrint='X' from cust where cuno =@cuno and  cuaddr3 !=''
) UnionDt";
                    da.Fill(指送地址dt);
                    客戶基本檔Count = 指送地址dt.Rows.Count;
                    #endregion
                    #region load 指送地址
                    cmd.CommandText = "Select ROW_NUMBER() OVER(ORDER BY id) +" + 客戶基本檔Count.ToString() + " AS 序號,* from DeliveryAddress where cuno = @cuno ";
                    da.Fill(指送地址dt);
                    cmd.CommandText = "Select 序號='" + (指送地址dt.Rows.Count + 1).ToString() + "', addr='', zip='', per1='', tel='',dano='', name='' ,defaultPrint='' ";
                    da.Fill(指送地址dt);
                    #endregion
                }
                dataGridViewT1.DataSource = 指送地址dt;
                #endregion
            }
            else if (state.ToLower() == "search")
            {
                #region Search
                using (SqlConnection cn = new SqlConnection(S_61.Basic.Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    dataGridViewT1.DataSource = null;
                    指送地址dt.Clear();
                    string sql = @"Select ROW_NUMBER() OVER(ORDER BY id) AS 序號,* from DeliveryAddress where cuno = @cuno ";

                    cmd.Parameters.AddWithValue("cuno", cuno);
                    if (Addr.Text.Trim().Length > 0)
                    {
                        cmd.Parameters.AddWithValue("Addr", Addr.Text.Trim());
                        sql += " and Addr like @Addr+'%'";
                    }
                    if (Tel.Text.Trim().Length > 0)
                    {
                        cmd.Parameters.AddWithValue("Tel", Tel.Text.Trim());
                        sql += " and Tel  like @Tel+'%'";
                    }
                    if (per1.Text.Trim().Length > 0)
                    {
                        cmd.Parameters.AddWithValue("per1", per1.Text.Trim());
                        sql += " and per1 like @per1+'%'";
                    }
                    if (zip.Text.Trim().Length > 0)
                    {
                        cmd.Parameters.AddWithValue("zip", zip.Text.Trim());
                        sql += " and zip like @zip+'%' ";
                    }
                    if (DaNo.Text.Trim().Length > 0)
                    {
                        cmd.Parameters.AddWithValue("DaNo", DaNo.Text.Trim());
                        sql += " and DaNo like @DaNo+'%'";
                    }

                    cmd.CommandText = sql;
                    da.Fill(指送地址dt);
                }
                dataGridViewT1.DataSource = 指送地址dt;
                #endregion
            }
            else if (state.ToLower() == "duplicate_save")
            {
                #region  duplicate_save
                using (SqlConnection cn = new SqlConnection(S_61.Basic.Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    #region update
                    cn.Open();
                    for (int i = 0; i < Update指送地址.Count; i++)
                    {
                        int index = int.Parse(Update指送地址[i]) - 1;
                        cmd.Parameters.Clear();
                        if (指送地址dt.Rows[index]["dano"].ToString().Trim() == "") continue;
                        cmd.Parameters.AddWithValue("dano", 指送地址dt.Rows[index]["dano"].ToString().Trim().GetUTF8(10));
                        cmd.Parameters.AddWithValue("id", 指送地址dt.Rows[index]["id"].ToString().Trim().GetUTF8(10));
                        cmd.Parameters.AddWithValue("name", 指送地址dt.Rows[index]["name"].ToString().Trim().GetUTF8(50));
                        cmd.Parameters.AddWithValue("cuno", cuno);
                        cmd.Parameters.AddWithValue("addr", 指送地址dt.Rows[index]["addr"].ToString().Trim().GetUTF8(60));
                        cmd.Parameters.AddWithValue("zip", 指送地址dt.Rows[index]["zip"].ToString().Trim().GetUTF8(3));
                        cmd.Parameters.AddWithValue("tel", 指送地址dt.Rows[index]["tel"].ToString().Trim().GetUTF8(10));
                        cmd.Parameters.AddWithValue("per1", 指送地址dt.Rows[index]["per1"].ToString().Trim().GetUTF8(10));
                        cmd.Parameters.AddWithValue("defaultPrint", 指送地址dt.Rows[index]["defaultPrint"].ToString().Trim().GetUTF8(1));
                        cmd.CommandText = @"UPDATE DeliveryAddress SET dano=@dano,cuno=@cuno,addr=@addr,zip=@zip,name=@name,tel=@tel,per1=@per1,defaultPrint= @defaultPrint WHERE id = @id ";
                        var influent = cmd.ExecuteNonQuery();
                        if (cmd.ExecuteNonQuery() <= 0)
                        #region  insert
                        {
                            cmd.CommandText = "insert into DeliveryAddress(dano,cuno,addr,zip,tel,per1,defaultPrint,name) values(@dano,@cuno,@addr,@zip,@tel,@per1,@defaultPrint,@name)";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion
                    }
                    #endregion
                    #region  insert
                    for (int i = 0; i < Insert指送地址.Count; i++)
                    {
                        int index = int.Parse(Insert指送地址[i]) - 1;
                        var dano = 指送地址dt.Rows[index]["dano"].ToString().Trim();
                        if (指送地址dt.Rows[index]["dano"].ToString().Trim() == "" ) continue;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("dano", 指送地址dt.Rows[index]["dano"].ToString().Trim().GetUTF8(10));
                        cmd.Parameters.AddWithValue("cuno", cuno);
                        cmd.Parameters.AddWithValue("addr", 指送地址dt.Rows[index]["addr"].ToString().Trim().GetUTF8(60));
                        cmd.Parameters.AddWithValue("zip", 指送地址dt.Rows[index]["zip"].ToString().Trim().GetUTF8(3));
                        cmd.Parameters.AddWithValue("tel", 指送地址dt.Rows[index]["tel"].ToString().Trim().GetUTF8(10));
                        cmd.Parameters.AddWithValue("per1", 指送地址dt.Rows[index]["per1"].ToString().Trim().GetUTF8(10));
                        cmd.Parameters.AddWithValue("name", 指送地址dt.Rows[index]["name"].ToString().Trim().GetUTF8(50));
                        cmd.Parameters.AddWithValue("defaultPrint", 指送地址dt.Rows[index]["defaultPrint"].ToString().Trim().GetUTF8(1));
                        cmd.CommandText = "insert into DeliveryAddress(dano,cuno,addr,zip,tel,per1,defaultPrint,name) values(@dano,@cuno,@addr,@zip,@tel,@per1,@defaultPrint,@name)";
                        cmd.ExecuteNonQuery();
                    }
                    #endregion
                    #region  Delete
                    string DefaultId = "";
                    for (int i = 0; i < 指送地址dt.Rows.Count; i++)
                    {
                        if (指送地址dt.Rows[i]["defaultPrint"].ToString().Trim() == "V")
                            DefaultId = 指送地址dt.Rows[i]["id"].ToString().Trim();

                        var dano = 指送地址dt.Rows[i]["dano"].ToString().Trim();
                        var id = 指送地址dt.Rows[i]["id"].ToString().Trim();
                        if (指送地址dt.Rows[i]["dano"].ToString().Trim() != "")
                            continue;
                        if (指送地址dt.Rows[i]["id"].ToString().Trim() == "")
                            continue;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.CommandText = "delete from DeliveryAddress where id = @id";
                        cmd.ExecuteNonQuery();
                    }
                    #endregion
                    #region  UpDate預設列印
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("cuno", cuno);
                    cmd.Parameters.AddWithValue("id", DefaultId);
                    cmd.CommandText = @"UPDATE DeliveryAddress SET defaultPrint= ''  WHERE cuno=@cuno and defaultPrint = 'V' ";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"UPDATE DeliveryAddress SET defaultPrint= 'V' WHERE id=@id ";
                    cmd.ExecuteNonQuery();
                    #endregion
                    function("load");
                }
                #endregion
            }
        }

        void t_TextChanged(object sender, EventArgs e)
        {
            string zip = dataGridViewT1["郵遞區號", dataGridViewT1.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim();
            if (zip.Length == 3)
            {

                if (指送地址dt.Rows[dataGridViewT1.CurrentCell.RowIndex]["addr"].ToString().IndexOf(get地址(zip)) != -1)
                    return;
                指送地址dt.Rows[dataGridViewT1.CurrentCell.RowIndex]["zip"] = zip;
                指送地址dt.Rows[dataGridViewT1.CurrentCell.RowIndex]["addr"] = get地址(zip);
                //指送地址dt.AcceptChanges();
                dataGridViewT1.InvalidateRow(dataGridViewT1.CurrentCell.RowIndex);
                //dataGridViewT2.CurrentRow.Selected = true;
                //dataGridViewT2.Focus();
            }
        }

        private string get地址(string zip)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("zip", zip);
                cmd.CommandText = "select * from saddr2 where zip=@zip";
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    return dr["city"].ToString().Trim() + dr["area"].ToString().Trim();
                }
            }
            return "";
        }

        private void 編輯指送地址Btn狀態(bool state)
        {
            foreach (var item in new List<JE.MyControl.ButtonSmallT> { btnEdit, btnExit, btnQuery, btnGet })
            {
                item.Enabled = state;
            }
            foreach (var item in new List<JE.MyControl.ButtonSmallT> { btnSave, btnCancel })
            {
                item.Enabled = !state;
            }
        }

        private void 控制指送地址Column編輯狀態(bool state)
        {
            dataGridViewT1.ReadOnly = !state;
            foreach (var item in new List<DataGridViewTextBoxColumn> { 序號, 預設列印 })
            {
                item.ReadOnly = true;
            }
            for (int i = 0; i < 客戶基本檔Count; i++)
            {
                dataGridViewT1.Rows[i].ReadOnly = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                if (!dataGridViewT1.ReadOnly)
                    AddDataGridView1Row();
            }
            else if (keyData == Keys.F2)
            {
                btnEdit_Click(null, null);
            }
            else if (keyData == Keys.F3)
            {
                btnSave_Click(null, null);
            }
            else if (keyData == Keys.F4)
            {
                btnCancel_Click(null, null);
            }
            else if (keyData == Keys.F6)
            {
                btnQuery_Click(null, null);
            }
            else if (keyData == Keys.F9)
            {
                btnGet_Click(null, null);
            }
            else if (keyData == Keys.F10)
            {
                btnExit_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }













    }
}
