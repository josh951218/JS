using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using Microsoft.Win32;
using S_61.Basic;
using System.Data;
using System.IO;

namespace S_61.subMenuFm_6
{
    public partial class FrmSystemSetmore : Formbase
    {
        public FrmSystemSetmore()
        {
            InitializeComponent(); 
        }

        private void FrmSystemSetmore_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = @"
                    Select 
                    X3Forward,InvUsed 
                    From Systemset ";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows == false)
                        return;

                    reader.Read();

                    if (reader["X3Forward"].ToDecimal() == 1)
                        X3Forward1.Checked = true;
                    else
                        X3Forward2.Checked = true;

                    if (reader["InvUsed"].ToDecimal() == 1)
                        InvUsed1.Checked = true;
                    else
                        InvUsed2.Checked = true;
                }
            }

            DabaseAddress.Text = GetDabaseAddress();
        }

        private string GetDabaseAddress(string StrAddress="")
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = "SELECT physical_name FROM sys.database_files";
                StrAddress = cmd.ExecuteScalar().ToString();
                return StrAddress;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("X3Forward", X3Forward1.Checked ? "1" : "2");
                    cmd.Parameters.AddWithValue("InvUsed", InvUsed1.Checked ? "1" : "2");

                    cn.Open();
                    cmd.CommandText = @"
                    Update Systemset Set
                     X3Forward = @X3Forward 
                    ,InvUsed   = @InvUsed 
                    ";

                    cmd.ExecuteNonQuery();
                }

                Common.Sys_X3Forward = X3Forward1.Checked ? 1 : 2;
                Common.Sys_InvUsed = InvUsed1.Checked ? 1 : 2;
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnResetReportFormat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Reset Report Format Right Now?",
                "Message Box",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (var reg = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Print\Forms", true))
                {
                    var iterator = reg.GetValueNames().GetEnumerator();
                    while (iterator.MoveNext())
                    {
                        var name = iterator.Current.ToString();
                        reg.DeleteValue(name, false);
                    }

                    var binary = new Dictionary<string, byte[]>()
                    { 
                        {"122" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"123" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"124" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"125" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"126" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x05,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"127" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x06,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"128" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x07,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"129" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x08,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"130" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x09,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"131" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x0a,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"132" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x0b,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"133" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x0c,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"134" ,new byte[]{0x5c,0x4b,0x03,0x00,0x38,0x63,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0x38,0x63,0x00,0x00,0x0d,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"135" ,new byte[]{0x5c,0x4b,0x03,0x00,0xd4,0x94,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xd4,0x94,0x00,0x00,0x0e,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"136" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x0f,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"137" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x10,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"138" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x11,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"139" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x12,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"140" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x13,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"141" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x14,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"142" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x15,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
                        {"143" ,new byte[]{0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x5c,0x4b,0x03,0x00,0xb4,0x21,0x02,0x00,0x16,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
                    };

                    for (int i = 0; i < binary.Count; i++)
                    {
                        reg.SetValue(
                            binary.ElementAt(i).Key,
                            binary.ElementAt(i).Value,
                            RegistryValueKind.Binary);
                    }

                    binary.Clear();
                }

                MessageBox.Show(
                    "Reset Report Format Completed!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        int GetConnectionningCount()
        {
            DataTable tCount = new DataTable();
            DataTable tTemp = new DataTable();

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = " Select hostprocess 主機,net_address 位址 from master..sysprocesses where program_name = 'JBS' ";
                da.Fill(tCount);

                tTemp = tCount.Clone();
                for (int i = 0; i < tCount.Rows.Count; i++)
                {
                    var row = tTemp.AsEnumerable().Where(r => r["主機"].ToString() == tCount.Rows[i]["主機"].ToString() && r["位址"].ToString() == tCount.Rows[i]["位址"].ToString());
                    if (row.Count() == 0)
                        tTemp.ImportRow(tCount.Rows[i]);
                }
                tTemp.AcceptChanges();
            }

            var count = tTemp.Rows.Count;
            tTemp.Clear();
            tCount.Clear();

            return count;
        }

        private void btnResetAdmin_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Reset Admin Right Now?",
                "Message Box",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.CommandText = @"
                    Update scritd set
                        sc01='V',sc02='V',sc03='V',sc04='V',sc05='V'
                       ,sc06='V',sc07='V',sc08='V',sc09=''
                    Where scno = (Select scno from scrit where scname = 'BM');

                    Update scrit set scpass = 'BM' Where scname = 'BM'; ";
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                   "Reset System Admin Completed!",
                   "訊息視窗",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonSmallT1_Click(object sender, EventArgs e)
        {
            buttonSmallT1.Enabled = false;
            buttonSmallT2.Enabled = false;
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
                    cmd.CommandText = "EXECUTE master.dbo.xp_delete_file 0, N'" + DabaseAddress.Text.ToUpper() + time + "_" + Common.DatabaseName.ToUpper() + ".bak' , N'bak'";
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
                    cmd.CommandText = "backup database [" + Common.DatabaseName.ToUpper() + "] to disk=N'" + DabaseAddress.Text.ToUpper() + time + "_" + Common.DatabaseName.ToUpper() + ".bak'";
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("資料庫備份成功！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                buttonSmallT1.Enabled = true;
                buttonSmallT2.Enabled = true;
            }
        }

        private void buttonSmallT2_Click(object sender, EventArgs e)
        {
            #region 檢查
            if (RestoreAddress.Text=="")
            {
                MessageBox.Show("請先選擇需要還原資料庫之路徑！", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }   
            try
            {
                if (RestoreAddress.Text.IndexOf(".bak") == -1)
                {
                    MessageBox.Show("此檔案不是備份檔案(.bak)！", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                FileInfo info = new FileInfo(RestoreAddress.Text);  
	        }
	        catch (Exception )
	        {
	            MessageBox.Show("找不到檔案!");
            }
            #endregion

            buttonSmallT1.Enabled = false;
            buttonSmallT2.Enabled = false;
            string msg = "您將使用資料庫還原功能，資料庫將會回複至稍早的時間點！";
            if (MessageBox.Show(msg, "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                int index = RestoreAddress.Text.LastIndexOf("\\") + 1;
                FileInfo info = new FileInfo(RestoreAddress.Text);      
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
                            cmd.CommandTimeout = 0;
                            cn.Open();
                            cmd.CommandText = " Alter database [" + Common.DatabaseName + "] Set Offline With Rollback immediate;";
                            cmd.CommandText += " restore database [" + Common.DatabaseName + "] from disk=N'" + RestoreAddress.Text+ "' with replace;";
                            cmd.CommandText += " Alter database [" + Common.DatabaseName + "] Set Online With Rollback immediate;";
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("資料庫已還原。", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        buttonSmallT1.Enabled = true;
                        buttonSmallT2.Enabled = true;
                    }
                }
            }

        }

        private void Browse_btn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog()) 
            {
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    RestoreAddress.Text = open.FileName;
                }
            }
            
        }



    }
}
