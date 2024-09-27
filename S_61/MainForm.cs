using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using S_61.Basic;
using S_61.menu;
using System.Xml.Linq;

namespace S_61
{
    public partial class MainForm : Form
    {
        public static MainForm main;
        public static MenuForm menu;

        public Dictionary<string, string> dyCust;
        public Dictionary<string, string> dyFact;
        public Dictionary<string, string> dyItem;

        public int stripHeight = 1;
        public string name1 = "進銷存系統";
        public string name2 = "JS";
        //public string version = "15.9";
        public string version = "16.1";
        public string subver = "";

        public MainForm()
        {
            Common.SwStart();
            InitializeComponent();
            main = this;

            this.CheckDLL();

            using (LoginForm loginForm = new LoginForm())
            {
                Application.DoEvents();
                loginForm.ShowDialog();
                if (!Common.sqlConnString.Contains("Application Name=JBS")) Common.sqlConnString += ";Application Name=JBS";
            }

            if (main.Tag != null && main.Tag.ToString() == "XX")
                return;
            InitializeReportAddress();


            JEInitialize.UpdateDataBase(version.ToDecimal("f1"), Common.GetDBVers());
            this.loadUserSetting();
            JEInitialize.SetControlFontSize();
        }

        private void CheckDLL()
        {
            try
            {
                var config = Environment.CurrentDirectory + @"\JS.exe.config";
                if (System.IO.File.Exists(config) == false)
                {
                    MessageBox.Show("找不到config檔");
                }

                var xml = XDocument.Load(config);
                var runtime = xml.Descendants("runtime").FirstOrDefault();
                if (runtime == null)
                {
                    XNamespace name = "urn:schemas-microsoft-com:asm.v1";

                    var probing = new XElement(name + "probing", new XAttribute("privatePath", "DLL"));
                    var assemblyBinding = new XElement(name + "assemblyBinding", probing);

                    runtime = new XElement("runtime", assemblyBinding);

                    var configuration = xml.Descendants("configuration").FirstOrDefault();
                    configuration.Add(runtime);

                    xml.Save(config);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void InitializeReportAddress()
        {
            if (Directory.Exists(Application.StartupPath + "\\Report"))
            {
                Common.reportaddress = Application.StartupPath + "\\";
            }
            else
            {
                Common.reportaddress = "..\\..\\";
            }
            using (Report.Frmreport frm = new Report.Frmreport()) { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Common.dtUsSettings.Rows.Count > 0)  //登入成功
            {
                JBS.xSQL.xSqlConnectionString = Common.sqlConnString;

                loadSystemSetting();                //系統參數
                if (!Common.CheckVer(version))      //版本不符
                {
                    var isConnOK = ServerCommonFolderConn(Common.Update_IP, Common.Update_Folder, Common.Update_Account, Common.Update_Pwd);
                    if (Common.Update_IP.Trim().Length > 0 && isConnOK && File.Exists(Application.StartupPath + @"\Ujs.exe"))
                    {
                        if (MessageBox.Show("有新版程式,是否下載更新程式?", "訊息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo.FileName = Application.StartupPath + @"\Ujs.exe";
                            var args = Common.Update_IP + ";" + Common.Update_Folder + ";" + Common.Update_Account + ";" + Common.Update_Pwd;
                            p.StartInfo.Arguments = args;
                            p.Start();
                            System.Threading.Thread.Sleep(100);
                            this.Dispose();
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show("版本不符！" + Environment.NewLine + "資料庫  版本 V " + Common.Vers + " 版" + Environment.NewLine + "JBS系統版本 V " + version + " 版", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Dispose();
                            Application.Exit();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("版本不符！" + Environment.NewLine + "資料庫  版本 V " + Common.Vers + " 版" + Environment.NewLine + "JBS系統版本 V " + version + " 版", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Dispose();
                        Application.Exit();
                        return;
                    }
                }

                Task.Factory.StartNew(() =>
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    using (DataTable ctemp = new DataTable())
                    {
                        cmd.CommandText = "Select ROW_NUMBER() OVER(ORDER BY cuno) AS ROWID,CuNo from cust";
                        da.Fill(ctemp);

                        if (ctemp.Rows.Count > 0)
                            dyCust = ctemp.AsEnumerable().ToDictionary(row => row["ROWID"].ToString(), row => row["CuNo"].ToString());
                        else
                            dyCust = new Dictionary<string, string>();

                        ctemp.Clear();
                    }
                });

                Task.Factory.StartNew(() =>
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    using (DataTable ftemp = new DataTable())
                    {
                        cmd.CommandText = "Select ROW_NUMBER() OVER(ORDER BY fano) AS ROWID,FaNo from fact";
                        da.Fill(ftemp);

                        if (ftemp.Rows.Count > 0)
                            dyFact = ftemp.AsEnumerable().ToDictionary(row => row["ROWID"].ToString(), row => row["FaNo"].ToString());
                        else
                            dyFact = new Dictionary<string, string>();

                        ftemp.Clear();
                    }
                });

                Task.Factory.StartNew(() =>
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    using (DataTable itemp = new DataTable())
                    {
                        cmd.CommandText = "Select ROW_NUMBER() OVER(ORDER BY ItNo) AS ROWID,ItNo from item";
                        da.Fill(itemp);

                        if (itemp.Rows.Count > 0)
                            dyItem = itemp.AsEnumerable().ToDictionary(row => row["ROWID"].ToString(), row => row["ItNo"].ToString());
                        else
                            dyItem = new Dictionary<string, string>();
                        itemp.Clear();
                    }
                });

                JS.JSStrip strip = new JS.JSStrip();
                this.Controls.Add(strip.menuStrip1);
                this.MainMenuStrip = strip.menuStrip1;
                stripHeight = strip.menuStrip1.Height;

                //是否超出工作台數
                Common.Regist = Common.CheckWKS() ? "[正式版]" : "[教育版]";
                Common.Series = Common.GetSeries();
                //載入片語 
                doConnection();
                loadPhrase();
                loadComp();
                SetTitle();
                Application.DoEvents();
                OpenMenuForm();
            }
            else
            {
                Application.Exit();
            }
        }

        public void loadPhrase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    //讀報表尾
                    string str = "select * from tail";
                    using (SqlDataAdapter da = new SqlDataAdapter(str, conn))
                    {
                        Common.dtEnd.Clear();
                        da.Fill(Common.dtEnd);
                    }
                    //讀報表頭
                    str = "select * from pnthead";
                    using (SqlDataAdapter da = new SqlDataAdapter(str, conn))
                    {
                        Common.dtstart.Clear();
                        da.Fill(Common.dtstart);
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void loadUserSetting()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "select * from scrit left join einvsetup on scrit.User_Einv=Einvsetup.Einvid collate Chinese_Taiwan_Stroke_BIN where scname=N'" + Common.User_Name + "' COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Common.User_Name1 = reader["scname1"].ToString().Trim();
                            Common.User_DateTime = Common.ToInt32(reader["ScDatepic"]);
                            Common.User_StkNo = reader["StNo"].ToString();
                            Common.User_SalePrice = Common.ToBool(reader["saleprice"]);
                            Common.User_ShopPrice = Common.ToBool(reader["ScBshopMny"]);
                            Common.User_CuNo = reader["CuNo"].ToString();
                            Common.User_X5No = reader["X5No"].ToString();
                            Common.User_ScInvDev = Common.ToInt32(reader["ScInvDev"]);
                            Common.User_ScInvDevs = Common.ToInt32(reader["ScInvDevs"]);
                            Common.User_ScPriDev = Common.ToInt32(reader["ScPriDev"]);
                            Common.User_ShowKeyBoard = Common.ToInt32(reader["IsShowKeyBoard"]);
                            Common.User_StopInvPrint = Common.ToInt32(reader["IsStopInvPrint"]);
                            Common.SearchCount = reader["SearchCount"].ToString().Trim() == "" ? 500 : Convert.ToInt32(reader["SearchCount"].ToString());
                            Common.User_Formula = reader["Formula"].ToString().Trim();
                            Common.User_ScInvSlt = reader["ScInvSlt"].ToInteger();
                            Common.User_ScInvBat = reader["ScInvBat"].ToInteger();
                            Common.User_StopInvMode = reader["StopInvMode"].ToInteger();
                            Common.User_CloseMgr = reader["CloseMgr"].ToInteger();
                            Common.User_CanEditPOS = reader["CanEditPOS"].ToInteger();
                            Common.User_CanCelPrompt = reader["CanCelPrompt"].ToInteger();
                            Common.User_InvSalePort = reader["InvSalePort"].ToString();
                            Common.User_Einv = reader["User_Einv"].ToString();//改這
                            Common.sc_MachineSet = reader["sc_MachineSet"].ToString();
                            Common.isCheck = reader["isCheck"].ToString().Trim();
                            if (reader["User_Einv"].ToString().Trim().Length > 0)
                            {
                                Common.iTitle = reader["EinvTitle"].ToString().Trim();
                                Common.iStore = reader["EinvStore"].ToString().Trim();
                                Common.iUnno = reader["EinvUnno"].ToString().Trim();
                                Common.iTaxNo = reader["EinvTaxNo"].ToString().Trim();
                                Common.iTel = reader["EinvTel"].ToString().Trim();
                                Common.iAddress = reader["EinvAddress"].ToString().Trim();
                                Common.iMemo1 = reader["EinvMemo1"].ToString().Trim();
                                Common.iMemo2 = reader["EinvMemo2"].ToString().Trim();
                            }
                            else
                            {
                                Common.iTitle = reader["iTitle"].ToString().Trim();
                                Common.iStore = reader["iStore"].ToString().Trim();
                                Common.iUnno = reader["iUnno"].ToString().Trim();
                                Common.iTaxNo = reader["iTaxNo"].ToString().Trim();
                                Common.iTel = reader["iTel"].ToString().Trim();
                                Common.iAddress = reader["iAddress"].ToString().Trim();
                                Common.iMemo1 = reader["iMemo1"].ToString().Trim();
                                Common.iMemo2 = reader["iMemo2"].ToString().Trim();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料庫未更新，請更新資料庫", "訊息視窗\n" + ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Dispose();
                Application.Exit();
                return;
            }
        }

        public void loadSystemSetting()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    //讀系統參數設定
                    string str = "select * from systemset";
                    using (SqlDataAdapter da = new SqlDataAdapter(str, conn))
                    {
                        Common.dtSysSettings.Clear();
                        da.Fill(Common.dtSysSettings);
                        if (Common.dtSysSettings.Rows.Count > 0)
                        {
                            Common.listSysSettings.Clear();
                            Common.listSysSettings = Common.dtSysSettings.AsEnumerable().ToList();

                            Common.Sys_StockKind = Common.ToInt32(Common.dtSysSettings.Rows[0]["StockKind"]);
                            Common.Sys_NoAdd = Common.ToInt32(Common.dtSysSettings.Rows[0]["NoAdd"]);
                            Common.Sys_KeyPrs = Common.ToInt32(Common.dtSysSettings.Rows[0]["KeyPrs"]);
                            Common.Sys_LowStock = Common.ToInt32(Common.dtSysSettings.Rows[0]["LowStock"]);
                            Common.Sys_LowStockMode = Common.ToInt32(Common.dtSysSettings.Rows[0]["LowStockMode"]);
                            Common.Sys_LowStkSlt = Common.ToInt32(Common.dtSysSettings.Rows[0]["LowStkSlt"]);
                            Common.Sys_LowCost = Common.ToInt32(Common.dtSysSettings.Rows[0]["LowCost"]);
                            Common.Sys_UpCredit = Common.ToInt32(Common.dtSysSettings.Rows[0]["UpCredit"]);
                            Common.Sys_SalePrice = Common.ToInt32(Common.dtSysSettings.Rows[0]["SalePrice"]);
                            Common.Sys_StkYear1 = Common.ToInt32(Common.dtSysSettings.Rows[0]["StkYear1"]);
                            Common.Sys_StkYear2 = Common.ToInt32(Common.dtSysSettings.Rows[0]["StkYear2"]);
                            Common.Sys_StNoMode = Common.ToInt32(Common.dtSysSettings.Rows[0]["StNoMode"]);
                            Common.Sys_CuSaleMny = Common.ToInt32(Common.dtSysSettings.Rows[0]["CuSaleMny"]);
                            Common.Sys_CuPoint = Common.ToInt32(Common.dtSysSettings.Rows[0]["CuPoint"]);

                            Common.Sys_BuyPrice = Common.ToInt32(Common.dtSysSettings.Rows[0]["BuyPrice"]);
                            Common.Sys_AutoBuyp = Common.ToInt32(Common.dtSysSettings.Rows[0]["AutoBuyp"]);

                            Common.Sys_Stcname = Common.dtSysSettings.Rows[0]["Stcname"].ToString();
                            Common.Sys_Stctaxno = Common.dtSysSettings.Rows[0]["Stctaxno"].ToString();
                            Common.Sys_Stctaxno1 = Common.dtSysSettings.Rows[0]["Stctaxno1"].ToString();

                            Common.Sys_StcPnName = Common.dtSysSettings.Rows[0]["StcPnName"].ToString();
                            Common.Sys_SaleHead = Common.dtSysSettings.Rows[0]["SaleHead"].ToString();
                            Common.Sys_MemoUdf = Common.dtSysSettings.Rows[0]["MemoUdf"].ToString();
                            Common.CompanyName = Common.dtSysSettings.Rows[0]["StcCo"].ToString();
                            Common.Sys_PosMPW = Common.dtSysSettings.Rows[0]["PosMPW"].ToString();
                            var cDate = Common.User_DateTime == 1 ? "CloseDate" : "CloseDate1";
                            Common.Sys_CloseDate = Common.dtSysSettings.Rows[0][cDate].ToString();
                            Common.Sys_JZOK = (Common.dtSysSettings.Rows[0]["JZOK"].ToString().Trim() == "JZOK");//是否作過年度結轉

                            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciS"].ToString(), out  Common.MS);//銷貨單價小數
                            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciSt"].ToString(), out  Common.MST);//銷貨單據小數
                            int.TryParse(Common.dtSysSettings.Rows[0]["TaxDeciS"].ToString(), out Common.TS);//銷項稅額小數
                            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciBS"].ToString(), out Common.M);//本幣金額小數

                            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciF"].ToString(), out Common.MF);//進貨單價小數
                            int.TryParse(Common.dtSysSettings.Rows[0]["MnyDeciFt"].ToString(), out Common.MFT);//進貨單據小數
                            int.TryParse(Common.dtSysSettings.Rows[0]["TaxDeciF"].ToString(), out Common.TF);//進項稅額小數
                            int.TryParse(Common.dtSysSettings.Rows[0]["QtyDeci"].ToString(), out Common.Q);//庫存數量小數

                            int.TryParse(Common.dtSysSettings.Rows[0]["TaxPriceS"].ToString(), out Common.TPS);//銷項金額小數
                            int.TryParse(Common.dtSysSettings.Rows[0]["TaxPriceF"].ToString(), out Common.TPF);//進項金額小數

                            int.TryParse(Common.dtSysSettings.Rows[0]["DBqty"].ToString(), out Common.Sys_DBqty);//計量計價
                            int.TryParse(Common.dtSysSettings.Rows[0]["UsingBatch"].ToString(), out Common.Sys_UsingBatch);//批號管理
                            int.TryParse(Common.dtSysSettings.Rows[0]["weborder"].ToString(), out Common.Sys_WebOrder);//批號管理
                            int.TryParse(Common.dtSysSettings.Rows[0]["ItNameLenth"].ToString(), out Common.Sys_ItNameLenth); //資料庫結構 : 品名規格長度
                            int.TryParse(Common.dtSysSettings.Rows[0]["ItNoUdfLenth"].ToString(), out Common.Sys_ItNoUdfLenth); //資料庫結構 : 自訂編號長度
                            Common.pathC = Common.dtSysSettings.Rows[0]["pathC"].ToString().Trim();

                            Common.Update_IP = Common.dtSysSettings.Rows[0]["Update_IP"].ToString().Trim();
                            Common.Update_Folder = Common.dtSysSettings.Rows[0]["Update_Folder"].ToString().Trim();
                            Common.Update_Account = Common.dtSysSettings.Rows[0]["Update_Account"].ToString().Trim();
                            Common.Update_Pwd = Common.dtSysSettings.Rows[0]["Update_Pwd"].ToString().Trim();
                            Common.Sys_LendToSaleMode = Common.dtSysSettings.Rows[0]["LendToSaleMode"].ToInteger();
                            Common.Sys_X3Forward = Common.dtSysSettings.Rows[0]["X3Forward"].ToInteger();
                            Common.Sys_InvUsed = Common.dtSysSettings.Rows[0]["InvUsed"].ToInteger();
                            Common.Sys_DefaultAddr = Common.dtSysSettings.Rows[0]["DefaultAddr"].ToInteger();
                            Common.Sys_BookNo = Common.dtSysSettings.Rows[0]["BookNo"].ToString().Trim();
                            Common.Sys_Einvusen = Common.dtSysSettings.Rows[0]["Einvusen"].ToInteger();//電子發票使用家數
                            SetTitle();
                        }
                        else
                        {
                            Common.listSysSettings.Clear();
                        }
                    }
                }

                //var tool = this.MainMenuStrip.Items.OfType<ToolStripMenuItem>().FirstOrDefault(r => r.Name == "toolStripMenuItem10");
                //if (tool != null)
                //{
                //    tool.Visible = Common.Sys_StockKind != 1; 
                //} 
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料庫未更新，請更新資料庫", "訊息視窗\n" + ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Dispose();
                Application.Exit();
                return;
            }
        }

        public void loadComp()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = "select cono from comp";
                if (!cmd.ExecuteScalar().IsNullOrEmpty())
                    Common.CoNo = cmd.ExecuteScalar().ToString().Trim();
                else
                    Common.CoNo = "";

                cmd.CommandText = @"update sale set IsModify ='0' 
                        update rsale set IsModify ='0' 
                        update bshop set IsModify ='0' 
                        update rshop set IsModify ='0'
                        update draw set IsModify ='0' 
                        update allot set IsModify ='0' 
                        update adjust set IsModify ='0' 
                        update garner set IsModify ='0'
                        update InStk set IsModify ='0' 
                        update rlend set IsModify ='0' 
                        update lend set IsModify ='0' 
                        update Borr set IsModify ='0'
                        update RBorr set IsModify ='0'
                        update quote set IsModify ='0' 
                        update ford set IsModify ='0' 
                        update fquot set IsModify ='0' 
                        update [order] set IsModify ='0'
                        update Iv set IsModify ='0'  
                        update BatchProcess_adjust set IsModify ='0' 
                        ";
                cmd.ExecuteNonQuery();
            }
        }

        void SetTitle()
        {
            FileInfo info = new FileInfo(Application.ExecutablePath);
            this.Text = Common.CompanyName + name1 + "[ " + name2 + Common.Series + " ][ 軟體版本:V" + version + subver + " ]" + Common.Regist + "授權人數:" + Common.授權數 + "/連線人數:" + Common.連線數;//+ " 軟體更新時間：" + info.LastWriteTime.ToString();
        }

        void OpenMenuForm()
        {
            if (Common.Sys_BookNo.Contains("JM"))
            {
                //簡易POS
                menu = new MenuForm();
                menu.Controls.Clear();
                menu.Controls.Add(new MenuFormJM().tableLayoutPanelbase1);

                menu.Name = "FrmMenu";
                menu.MdiParent = this;  
                menu.Show();
            }
            else
            {
                menu = new MenuForm();
                menu.Name = "FrmMenu";
                menu.MdiParent = this;
                menu.Show();
            }

            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (ReportDocument rd = new ReportDocument())
                    using (CrystalDecisions.Windows.Forms.CrystalReportViewer view = new CrystalDecisions.Windows.Forms.CrystalReportViewer())
                    {
                        rd.Load(Common.reportaddress + "Report\\空報表.rpt");
                        view.ReportSource = rd;
                        view.RefreshReport();
                    }
                }
                catch { }
            });
        }

        void doConnection()
        {
            Common.ckTime = new System.Timers.Timer();
            Common.ckTime.Elapsed += new System.Timers.ElapsedEventHandler(tick);
            Common.ckTime.Interval = 180000;
            Common.ckTime.Enabled = true;
        }

        public void tick(object source, System.Timers.ElapsedEventArgs e)
        {
            //唯持連線用。
            using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
            {
                conn.Open();
                conn.Close();
            }
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Common.ckTime != null)
            {
                Common.ckTime.Enabled = false;
                Common.ckTime.Dispose();

                var p = System.IO.Path.GetTempPath();
                DirectoryInfo dir = new DirectoryInfo(p);
                var ps = dir.GetFiles().OfType<FileInfo>().Where(r => r.Name.ToUpper().EndsWith(".TMP") || r.Name.ToUpper().EndsWith(".RPT"));
                foreach (var item in ps)
                {
                    try
                    {
                        using (FileStream stream = item.OpenRead()) { }
                        item.Delete();
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
        }


        const int HTCAPTION = 2;
        const int HTSYSMENU = 3;
        const int WM_NCMOUSEMOVE = 0x00A0;
        const int WM_NCLBUTTONDOWN = 0x00A1;
        const int WM_NCLBUTTONUP = 0x00A2;
        const int WM_NCLBUTTONDBLCLK = 0x00A3;
        const int WM_NCRBUTTONDOWN = 0x00A4;
        const int WM_NCRBUTTONUP = 0x00A5;
        const int WM_NCRBUTTONDBLCLK = 0x00A6;
        const int WM_NCMBUTTONDOWN = 0x00A7;
        const int WM_NCMBUTTONUP = 0x00A8;
        const int WM_NCMBUTTONDBLCLK = 0x00A9;

        protected override void WndProc(ref Message m)
        {
            try
            {
                //if (m.Msg == WM_NCLBUTTONDBLCLK)//左雙擊
                //{
                //    m.WParam = System.IntPtr.Zero;
                //    return;
                //}
                //else if (m.Msg == WM_NCRBUTTONDOWN)//右選單
                //{
                //    m.WParam = System.IntPtr.Zero;
                //    return;
                //}
                //else if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)//左拖曳
                //{
                //    return;
                //}
                if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTSYSMENU)//左選單
                {
                    return;
                }
                //else if (m.WParam.ToInt32() == 9)
                //{
                //    return;
                //}
                base.WndProc(ref m);
            }
            catch
            {
            }
        }

        public static bool ServerCommonFolderConn(string remoteHost, string shareName, string userName, string passWord)
        {
            try
            {
                var error = "";
                using (Process proc = new Process())
                {
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardInput = true;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.Start();

                    //proc.StandardInput.WriteLine("net use * /d /y");
                    proc.StandardInput.WriteLine(@"net use \\" + remoteHost + @"\" + shareName + "  /User:" + userName + " " + passWord);
                    proc.StandardInput.WriteLine("exit");

                    while (!proc.HasExited)
                    {
                        proc.WaitForExit(1000);
                    }

                    error = proc.StandardError.ReadToEnd();
                    proc.StandardError.Close();
                }
                return String.IsNullOrEmpty(error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
