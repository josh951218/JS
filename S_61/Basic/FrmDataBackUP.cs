using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace S_61.Basic
{
    public partial class FrmDataBackUP : Form
    {
        string MDFdir = "";
        string MDFpath = "";
        string IsServer = "Integrated Security=True";


        public FrmDataBackUP()
        {
            InitializeComponent();
            //Common.FrmDataBackUP = this;
        }

        private void FrmDataBackUP_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = "SELECT physical_name FROM sys.database_files";
                MDFpath = cmd.ExecuteScalar().ToString().ToUpper();
                var index = MDFpath.LastIndexOf("\\") + 1;
                MDFdir = new string(MDFpath.Take(index).ToArray());
            }
            ISSERVER();
        }

        void ISSERVER()
        {
            try
            {
                var str = Common.sqlConnString.Split(';').ToList();
                var a = str.Find(s => s.Contains("Data Source")) + ";";
                var b = str.Find(s => s.Contains("Initial Catalog")) + ";";
                var path = a + b + IsServer;
                using (SqlConnection cn = new SqlConnection(path))
                {
                    cn.Open();
                }
                btnRestore.Enabled = true;
            }
            catch
            {
                btnRestore.Enabled = false;
            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            btnBackUp.Enabled = false;
            btnRestore.Enabled = false;
            Common.ckTime.Enabled = false;

            var str = Common.sqlConnString.Split(';').ToList();
            int index = str.FindIndex(s => s.Contains("Initial Catalog"));
            str[index] = "Initial Catalog=master";
            index = str.ToList().FindIndex(s => s.Contains("Application Name"));
            str[index] = "";

            var SQLString = "";
            str.ForEach(s => SQLString = SQLString + s + ";");
            SQLString = SQLString.takeString(SQLString.Length - 1);

            string time = DateTime.Now.ToString("yyyyMMdd");
            try
            {
                //delete
                using (SqlConnection cn = new SqlConnection(SQLString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "EXECUTE master.dbo.xp_delete_file 0, N'" + MDFdir + time + "_" + Common.DatabaseName.ToUpper() + ".bak' , N'bak'";
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
            try
            {
                //backup
                using (SqlConnection cn = new SqlConnection(SQLString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "backup database [" + Common.DatabaseName.ToUpper() + "] to disk=N'" + MDFdir + time + "_" + Common.DatabaseName.ToUpper() + ".bak'";
                    cmd.ExecuteNonQuery();
                }
                if (MessageBox.Show("資料庫備份成功！  是否開啟備份資較夾?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    string file = @"C:\Windows\explorer.exe";
                    string argument = "/select,"+MDFdir + time + "_" + Common.DatabaseName.ToUpper() + ".bak";
                    System.Diagnostics.Process.Start(file, argument);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Common.ckTime.Enabled = true;
                btnBackUp.Enabled = true;
                ISSERVER();
            }
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "select TOP 1 * from CUST where 1=0";
                    cmd.ExecuteReader();
                }

            }
            catch { }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            btnBackUp.Enabled = false;
            btnRestore.Enabled = false;

            string msg = "您將使用資料庫還原功能，資料庫將會回複至稍早的時間點！";
            if (MessageBox.Show(msg, "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                int index = MDFpath.LastIndexOf("\\") + 1;
                var dir = new string(MDFpath.Take(index).ToArray());
                DirectoryInfo directory = new DirectoryInfo(dir);
                FileInfo[] infos = directory.GetFiles();
                List<string> list = new List<string>();
                for (int i = 0; i < infos.Length; i++)
                {
                    if (infos[i].Name.ToLower().EndsWith("stock.bak"))
                    {
                        list.Add(infos[i].Name + ",備份時間:" + infos[i].LastWriteTime);
                    }
                }
                string fileNfame = "";
                using (FrmBackUpList frm = new FrmBackUpList())
                {
                    frm.list = list;
                    DialogResult rt = frm.ShowDialog();
                    if (rt != System.Windows.Forms.DialogResult.OK)
                    {
                        btnBackUp.Enabled = true;
                        btnRestore.Enabled = true;
                        return;
                    }
                    fileNfame = frm.fileName;
                }

                FileInfo info = new FileInfo(dir + fileNfame);
                msg = "您將使用" + info.LastWriteTime.ToString() + "所建立的備份檔！\n";
                msg += "確定請按    是(Y)，\n";
                msg += "如要使用最新的備份檔案，請選擇    否(N)，\n";
                msg += "並點擊『備份-資料庫』按鈕，備份最新的資料庫";
                if (MessageBox.Show(msg, "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    var str = Common.sqlConnString.Split(';').ToList();
                    index = str.FindIndex(s => s.Contains("Initial Catalog"));
                    str[index] = "Initial Catalog=master";
                    index = str.ToList().FindIndex(s => s.Contains("Application Name"));
                    str[index] = "";

                    var SQLString = "";
                    str.ForEach(s => SQLString = SQLString + s + ";");
                    SQLString = SQLString.takeString(SQLString.Length - 1);

                    try
                    {
                        Common.ckTime.Enabled = false;
                        //restore
                        using (SqlConnection cn = new SqlConnection(SQLString))
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cn.Open();
                            cmd.CommandText = " Alter database [" + Common.DatabaseName + "] Set Offline With Rollback immediate;";
                            cmd.CommandText += " restore database [" + Common.DatabaseName + "] from disk=N'" + dir + fileNfame + "' with replace;";
                            cmd.CommandText += " Alter database [" + Common.DatabaseName + "] Set Online With Rollback immediate;";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        btnBackUp.Enabled = true;
                        btnRestore.Enabled = true;
                        Common.ckTime.Enabled = true;
                    }
                }
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "select TOP 1 * from CUST where 1=0";
                    cmd.ExecuteReader();
                }
            }
            catch { }



        }









































    }
}
