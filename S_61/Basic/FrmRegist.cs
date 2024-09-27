using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;

namespace S_61.Basic
{
    public partial class FrmRegist : Formbase
    {
        string No { get; set; }
        string M { get; set; }

        public FrmRegist()
        {
            InitializeComponent();
        }

        private void FrmRegist_Load(object sender, EventArgs e)
        {
            loadTime();

            this.No = "";
            this.M = "";
        }

        void loadTime()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "select time from systemset where usrno='T01'";
                        textBoxT1.Text = cmd.ExecuteScalar().ToDecimal().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        string GetServerMacAddress()
        {
            var ip_OR_name = "";
            var server_name = Common.logOnInfo.ConnectionInfo.ServerName.Trim().ToUpper().Replace(@"\SQLEXPRESS", "");

            //判斷是主機名稱還是IP
            if (server_name == "." || server_name == "LOCALHOST" || server_name == "127.0.0.1")
            {
                ip_OR_name = "本機";
            }
            else
            {
                var isIP = server_name.ToList().TrueForAll(c => Char.IsDigit(c) || c == '.');
                if (isIP)
                {
                    ip_OR_name = server_name;

                    //如果是ip的話,要判斷是否為外部ip,外部ip要轉為內部ip
                    if (ip_OR_name.StartsWith("10.") || ip_OR_name.StartsWith("192.168.")) { /* 內部ip */ }
                    else
                    {
                        //172.16 - 172.31
                        var two = ip_OR_name.Split('.').ElementAt(1).ToDecimal();
                        bool iptwo = (16 <= two && two <= 31);
                        if (ip_OR_name.StartsWith("172.") && iptwo) { /* 內部ip,172.16 - 172.31 */ }
                        else
                        {
                            //外部ip轉內部ip
                            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                            using (SqlCommand cmd = cn.CreateCommand())
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable tempt = new DataTable();
                                cmd.CommandText = " SELECT [local_net_address],[local_tcp_port] FROM [master].[sys].[dm_exec_connections] where [local_net_address] is not null ";
                                da.Fill(tempt);
                                if (tempt.Rows.Count > 0) ip_OR_name = tempt.Rows[0]["local_net_address"].ToString().Trim();
                            }
                        }
                    }
                }
                else
                {
                    ip_OR_name = "hostname";
                }
            }

            var mac = "";
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();

                var line = "";
                if (ip_OR_name == "本機") //若是本機的話,必須先轉成本機名稱
                {
                    proc.StandardInput.WriteLine("hostname");
                    while ((line = proc.StandardOutput.ReadLine()) != null)
                    {
                        if (line.EndsWith("hostname"))
                        {
                            server_name = proc.StandardOutput.ReadLine();
                            ip_OR_name = "hostname";
                            break;
                        }
                    }
                    if (server_name.Length == 0)
                    {
                        MessageBox.Show("本機名稱轉換失敗!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return "";
                    }
                }
                //取得MAC指令
                if (ip_OR_name == "hostname")
                {
                    proc.StandardInput.WriteLine("nbtstat -a " + server_name);//name
                }
                else
                {
                    proc.StandardInput.WriteLine("nbtstat -A " + ip_OR_name);//ip
                }

                while ((line = proc.StandardOutput.ReadLine()) != null)
                {
                    if (line.Trim().Length == 0) continue;
                    if (line.ToUpper().Contains("MAC") && line.Contains("位址"))
                    {
                        var index = line.IndexOf("=") + 1;
                        if (index != -1)
                        {
                            mac = line.skipString(index).Trim();
                            break;
                        }
                    }
                }
                //結束CMD
                //proc.StandardInput.WriteLine("exit");
            }
            return mac.ToUpper();
        }

        private void bookNo_Validating(object sender, CancelEventArgs e)
        {
            if (BookNo.ReadOnly) return;
            if (btnExit.Focused) return;

            if (BookNo.Text.ToUpper().EndsWith("M"))
                this.M = "M";
            else
                this.M = "";

            this.No = BookNo.Text.ToUpper().takeString(17).Trim();

            if (this.No.Length != 17)
            {
                e.Cancel = true;
                MessageBox.Show("序號輸入錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var JSJM = this.No.Substring(9, 2).ToUpper();
            if (JSJM != "JS" && JSJM != "JM")
            {
                e.Cancel = true;
                MessageBox.Show("序號輸入錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Code.Focus();
                //產生AutoX1
                AutoX1.Text = "";
                Wks.Text = new string(this.No.Skip(15).ToArray());

                Random rd = new Random((int)DateTime.Now.Ticks);
                for (int i = 0; i < 4; i++)
                {
                    AutoX1.Text += rd.Next(1, 9).ToString();
                }

                Security Sec = new Security();
                AutoX1.Text += Sec.數字加密(this.No.Substring(11, 2), AutoX1.Text.Substring(0, 2));
                AutoX1.Text += Sec.數字加密(textBoxT1.Text.PadLeft(2, '0').Substring(0, 2), AutoX1.Text.Substring(0, 2));
                AutoX1.Text += Sec.數字加密(this.No.Substring(15, 2), AutoX1.Text.Substring(2, 2));
            }

            BookNo.ReadOnly = true;
            
        }

        private void Wks_Validating(object sender, CancelEventArgs e)
        {
            if (Wks.Text.ToDecimal() == 0)
            {
                e.Cancel = true;
                MessageBox.Show("工作台數輸入錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            Security Sec = new Security();

            string 特徵 = new string(AutoX1.Text.ToCharArray().Reverse().ToArray());
            bool 驗證 = true;

            string textBoxT1temp = "0";
            try
            {
                string 解密 = Sec.數字解密(Code.Text, 特徵);
                if (解密.Substring(0, 4) != AutoX1.Text.Substring(0, 4)) 驗證 = false;
                if (解密.Substring(6, 2) != AutoX1.Text.Substring(0, 2)) 驗證 = false;
                textBoxT1temp = 解密.Substring(4, 2);
            }
            catch
            {
                驗證 = false;
            }
            if (驗證)
            {
                string 版本次 = "";
                string 版本次temp = this.No.Substring(11, 2);
                Random rd = new Random((int)DateTime.Now.Ticks);
                版本次 += rd.Next(1, 8).ToString();
                for (int i = 1; i < 10; i++)
                {
                    if (i == 版本次[0].ToDecimal())
                        版本次 += 版本次temp;
                    if (i != 版本次[0].ToDecimal() && i != 版本次[0].ToDecimal() + 1)
                        版本次 += rd.Next(0, 9).ToString();
                }

                string 註冊台數 = "";
                string 註冊台數temp = this.No.Substring(15, 2) + "78";

                註冊台數 += rd.Next(1, 6).ToString();
                for (int i = 1; i < 10; i++)
                {
                    if (i == 註冊台數[0].ToDecimal())
                        註冊台數 += 註冊台數temp;
                    if (i < 註冊台數[0].ToDecimal() || i > 註冊台數[0].ToDecimal() + 3)
                        註冊台數 += rd.Next(0, 9).ToString();
                }


                try
                {
                    using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT CONVERT(nvarchar(30),create_date,114) FROM sys.databases where name ='" + Common.DatabaseName + "'";
                            string str = "";
                            str = cmd.ExecuteScalar().ToString().Trim().Replace(":", "");

                            cmd.Parameters.AddWithValue("@bookNo", this.No + this.M);
                            cmd.CommandText = "update systemset set uomgp='" + Sec.數字加密(註冊台數, str + "0")
                                + "' ,CT ='" + str
                                + "' ,vera ='" + Sec.數字加密(版本次, str + "0")
                                + "' ,time =" + textBoxT1temp
                                + "  ,bookNo=@bookNo";
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }

                Wks.ReadOnly = true;
                Code.ReadOnly = true;
                textBoxT1.Text = textBoxT1temp;
                MessageBox.Show("註冊成功！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Code錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Code.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnRegist_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("註冊", tMAC.Font, Brushes.Black, new Point((btnRegist.Width / 6).ToInteger(), (btnRegist.Height / 3).ToInteger()));
        }
    }
}
