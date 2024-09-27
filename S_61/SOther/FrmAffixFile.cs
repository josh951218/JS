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
using System.IO;
using System.Diagnostics;

namespace S_61.SOther
{
    public partial class FrmAffixFile : Formbase
    {
        string FilePath = "";
        public string Datano, DaType, giveupno, 編輯狀態 = "";
        public DataTable DtFile = new DataTable();//DtFile DataSource(新增用) 
        List<string> UpdateDtFile = new List<string>();
        List<string> InsertDtFile = new List<string>();
        public SqlCommand CMD =new SqlCommand();
      
        public FrmAffixFile()
        {
            InitializeComponent();
        }

        private void FrmAffixFile_Load(object sender, EventArgs e)
        {          
            按鈕狀態("LOAD");
        }

        private void 選擇檔案路徑()
        {
            using (OpenFileDialog OpenFileDialog = new OpenFileDialog())
            {
                OpenFileDialog.Title = "請選擇檔案";
                OpenFileDialog.ShowDialog();
                if (OpenFileDialog.FileName != "")
                    FilePath = OpenFileDialog.FileName;
            }
            if (File.Exists(FilePath))
            {
                Process.Start(FilePath);
            }
        }

        private void btnopen_Click(object sender, EventArgs e)//開啟檔案
        {
            FilePath = DtFile.Rows[dataGridViewT1.CurrentCell.RowIndex]["DaAdd"].ToString();

            if (FilePath != "")
            {
                if (File.Exists(FilePath))
                {
                    Process.Start(FilePath);
                }
                else
                {
                    var result = MessageBox.Show("此單據檔案路徑不存在:" + FilePath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                var result = MessageBox.Show("此單據未填入檔案路徑!!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonT2_Click(object sender, EventArgs e)
        {
            FilePath = DtFile.Rows[dataGridViewT1.CurrentCell.RowIndex]["DaAdd"].ToString();
            if (FilePath != "")
            {
                var result = MessageBox.Show("此單據檔案路徑目前為:" + FilePath + "\n是否修改!?", "訊息視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    選擇檔案路徑();
                DtFile.Rows[dataGridViewT1.CurrentCell.RowIndex]["DaAdd"] = FilePath;
                dataGridViewT1.InvalidateRow(dataGridViewT1.CurrentCell.RowIndex);
                FileSave(DtFile);
            }
        }

        public void FileSave(DataTable DtFile)
        {
            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;
                    for (int i = 0; i < DtFile.Rows.Count; i++)
                    {
                        if (編輯狀態 == "新增") //如果還沒有產生憑證編號時，不能存此單據資料，只能儲存相關連單據資料
                        {
                            if (DtFile.Rows[i]["DaType"].ToString().Trim() == DaType) continue;
                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("id", DtFile.Rows[i]["id"]);
                        cmd.Parameters.AddWithValue("Type", DtFile.Rows[i]["DaType"].ToString().Trim().GetUTF8(10));
                        cmd.Parameters.AddWithValue("Datano", DtFile.Rows[i]["Datano"].ToString().Trim().GetUTF8(20));
                        cmd.Parameters.AddWithValue("FilePath", DtFile.Rows[i]["DaAdd"].ToString().Trim().GetUTF8(100));
                        cmd.Parameters.AddWithValue("Dadetail", DtFile.Rows[i]["Dadetail"].ToString().Trim().GetUTF8(100));
                        if (DtFile.Rows[i]["DaAdd"].ToString().Trim().Length == 0 || DtFile.Rows[i]["Datano"].ToString().Trim().Length == 0) //刪除沒有路徑為空的資料
                        {
                            cmd.CommandText = @"DELETE AffixFile WHERE id = @id ";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = @"UPDATE AffixFile SET DaType=@Type,Datano=@Datano,DaAdd=@FilePath,Dadetail=@Dadetail WHERE id = @id ";
                            var influent = cmd.ExecuteNonQuery();
                            if (cmd.ExecuteNonQuery() <= 0 && DtFile.Rows[i]["DaAdd"].ToString().Trim() != "")
                                cmd.CommandText = "insert into AffixFile(DaType,Datano,DaAdd,Dadetail) values(@Type,@Datano,@FilePath,@Dadetail)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tn.Commit();
                }
                catch (Exception ex)
                {
                    if (tn != null)
                        tn.Rollback();

                    throw ex;
                }
            }
        }

        public static void FileSave_單據存檔(DataTable DtFile, SqlCommand cmd, string Datano, string 單據類型)
        {
            for (int i = 0; i < DtFile.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", DtFile.Rows[i]["id"]);
                cmd.Parameters.AddWithValue("Type", DtFile.Rows[i]["DaType"].ToString().Trim().GetUTF8(10));
                cmd.Parameters.AddWithValue("FilePath", DtFile.Rows[i]["DaAdd"].ToString().Trim().GetUTF8(100));
                cmd.Parameters.AddWithValue("Dadetail", DtFile.Rows[i]["Dadetail"].ToString().Trim().GetUTF8(100));
                if (DtFile.Rows[i]["Datano"].ToString().Trim().Length == 0 )
                {
                    cmd.Parameters.AddWithValue("Datano", Datano);
                }
                else
                {
                    cmd.Parameters.AddWithValue("Datano", DtFile.Rows[i]["Datano"].ToString().Trim().GetUTF8(20));
                }
                if (DtFile.Rows[i]["DaAdd"].ToString().Trim().Length > 0 && DtFile.Rows[i]["DaType"].ToString().Trim() == 單據類型) //刪除沒有路徑為空的資料
                {
                    cmd.CommandText = "insert into AffixFile(DaType,Datano,DaAdd,Dadetail) values(@Type,@Datano,@FilePath,@Dadetail)";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void FileDelete_單據刪除(SqlCommand cmd, string Datano, string 單據類型)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("Datype", 單據類型);
            cmd.Parameters.AddWithValue("Datano", Datano);
            cmd.CommandText = "delete AffixFile where Datype =@Datype and Datano = @Datano";
            cmd.ExecuteNonQuery();
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly == true) return;
            FilePath = "";
            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;
            if ( CurrentColumnName == "DaAdd" )
            {
                選擇檔案路徑();
                if (FilePath.ToString().Trim() == "")
                    return;
            }
            else
            {
                return;
            }
            int max = DtFile.Rows.Count, now = 0;
            //if (dataGridViewT1.CurrentCell.RowIndex < max - 1) return;
            for (int i = 0; i < DtFile.Rows.Count; i++)
            {
                if (DtFile.Rows[i]["datano"].ToString().Trim().Length > 0) now += 1; ;
            }
            if (dataGridViewT1["序號", dataGridViewT1.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim().Length > 0) now += 1;
            //if (now >= max)
            //{
                DtFile.Rows[dataGridViewT1.CurrentCell.RowIndex]["序號"] = (DtFile.Rows.Count).ToString();
                //DtFile.Rows[dataGridViewT1.CurrentCell.RowIndex]["DaType"] = DaType;
                //DtFile.Rows[dataGridViewT1.CurrentCell.RowIndex]["Datano"] = Datano;
                DtFile.Rows[dataGridViewT1.CurrentCell.RowIndex]["DaAdd"] = FilePath;
                dataGridViewT1.InvalidateRow(dataGridViewT1.CurrentCell.RowIndex);
                AddDataGridViewRow();
            //}
            //else
            //{
            //    DtFile.Rows[dataGridViewT1.CurrentCell.RowIndex]["DaType"] = DaType;
            //    DtFile.Rows[dataGridViewT1.CurrentCell.RowIndex]["DaAdd"] = FilePath;
            //    dataGridViewT1.InvalidateRow(dataGridViewT1.CurrentCell.RowIndex);
            //}

        }

        private void loadState()
        {
            dataGridViewT1.DataSource = null;
            DtFile.Clear();
            InsertDtFile.Clear();
            UpdateDtFile.Clear();

            using (SqlDataAdapter da = new SqlDataAdapter(CMD))
            {
                da.Fill(DtFile);
            }

            for (int i = 0; i < 1; i++)
            {
                DataRow dr = DtFile.NewRow();
                dr["序號"] = (DtFile.Rows.Count + 1).ToString();
                dr["DaType"] = DaType;
                dr["Datano"] = Datano;
                DtFile.Rows.Add(dr);
            }
            dataGridViewT1.DataSource = DtFile;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Dispose();
            //DtFile.Clear();
            this.Close();
        }

        public void GiveupData()//放棄時要連同單據一起刪掉檔案路徑
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Datano", giveupno.ToString().Trim());
                cmd.CommandText = "delete from AffixFile WHERE Datano = @Datano COLLATE Chinese_Taiwan_Stroke_BIN";
                cmd.ExecuteNonQuery();
            }
        }

        public void Updatasalesave()//從外面再存一次單號
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("Datano", Datano);
                for (int i = 0; i < DtFile.Rows.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("id", DtFile.Rows[i]["id"]);
                    cmd.Parameters.AddWithValue("Datano", Datano);
                    cmd.CommandText = @"UPDATE AffixFile SET Datano=@Datano WHERE id = @id ";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            按鈕狀態("儲存");
            FileSave(DtFile);
            if (編輯狀態 != "新增") //如果還沒有產生憑證編號時，不會刷新劃面
            {
                loadState();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            按鈕狀態("編輯");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            按鈕狀態("放棄");
            loadState();
        }

        private void 按鈕狀態(string str)
        {
            if (str == "編輯")
            {
                btnSave.Enabled = btnCancel.Enabled = true;
                btnModify.Enabled = false;
                dataGridViewT1.ReadOnly = false;
                foreach (var item in new List<DataGridViewTextBoxColumn> { 序號, Type, 單號 })
                {
                    item.ReadOnly = true;
                }
            }
            else
            {
                if (str == "LOAD" )
                {
                    dataGridViewT1.DataSource = DtFile;
                    DataRow dr = DtFile.NewRow();
                    dr["序號"] = (DtFile.Rows.Count + 1).ToString();
                    dr["DaType"] = DaType;
                    dr["Datano"] = Datano;
                    DtFile.Rows.Add(dr);
                    dataGridViewT1.InvalidateRow(dataGridViewT1.CurrentCell.RowIndex);
                }
                btnSave.Enabled = btnCancel.Enabled = false;
                btnModify.Enabled = true;
                dataGridViewT1.ReadOnly = true;
            }
        }

        private void AddDataGridViewRow()
        {
            int max = DtFile.Rows.Count, now = 0;
            if (dataGridViewT1.CurrentCell.RowIndex < max - 1) return;
            for (int i = 0; i < DtFile.Rows.Count; i++)
            {
                if (DtFile.Rows[i]["Dadetail"].ToString().Trim().Length > 0 || DtFile.Rows[i]["DaAdd"].ToString().Trim().Length > 0) now += 1; ;
            }
            if (dataGridViewT1["Dadetail", dataGridViewT1.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim().Length > 0 ||  dataGridViewT1["DaAdd", dataGridViewT1.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim().Length > 0) now += 1;
            if (now >= max)
            {
                DataRow dr = DtFile.NewRow();
                dr["序號"] = (DtFile.Rows.Count + 1).ToString();
                dr["DaType"] = DaType;
                dr["Datano"] = Datano;
                DtFile.Rows.Add(dr);
                dataGridViewT1.InvalidateRow(dataGridViewT1.CurrentCell.RowIndex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                    if (!dataGridViewT1.ReadOnly)
                        AddDataGridViewRow();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}
