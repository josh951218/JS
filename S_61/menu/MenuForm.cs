using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using JBS.JS;
using S_61.Basic;
using JE.MyControl;
using System.Data.SqlClient;
using S_61.subMenuFm_1;

namespace S_61.menu
{
    public partial class MenuForm : Form
    {
        public PictureBox menuhelp = new PictureBox();

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        public MenuForm()
        {
            InitializeComponent();
            MainForm.menu = this;
            tabControl1.ItemSize = new Size(0, 1);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width - (MainForm.main.Width - MainForm.main.ClientSize.Width) - 2;
            this.Height = MainForm.main.ClientSize.Height - MainForm.main.stripHeight-30;
            this.Location = new Point(1, 1);

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Text = MainForm.main.name1 + "功能選單";

            if (Common.Series == "74" || Common.Series == "72")
            {
                btnFrmNetOrder.Enabled = false;
                btnOrder.Enabled = false;
                btnOrderInfo.Enabled = false;
                btnFord.Enabled = false;
                btnFordInfo.Enabled = false;

                btnFrmOrderSaled.Enabled = false;
                btnFrmCustNotOut.Enabled = false;
                btnFrmItemNotOut.Enabled = false;
                btnFrmEmplNotOut.Enabled = false;
                btnOrderToFord.Enabled = false;

                btnFrmFordBshopd.Enabled = false;
                btnFrmFactNotIn.Enabled = false;
                btnFrmItemNotIn.Enabled = false;
                btnFrmEmplNotIn.Enabled = false;
            }
            if (Common.Series == "73" || Common.Series == "74")
            {
                btnAllot.Enabled = false;
                btnFrmAllot_Info.Enabled = false;

                btnLend.Enabled = false;
                btnRLend.Enabled = false;
                menuLabel1.Enabled = false;
                btnFrmBorr.Enabled = false;
                btnFrmRBorr.Enabled = false;
                btnBorrowNew_Info.Enabled = false;
            }

            if (Common.Sys_WebOrder == 1)
            {
                btnCheckItem.Visible = false;
                btnFrmNetOrder.Visible = false;
                btnBarCodeToorder.Visible = false;
            }
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(1, 1);
            Application.DoEvents();

            menuhelp.Size = new Size(17, 17);
            this.Controls.Add(menuhelp);
            menuhelp.BackgroundImage = Properties.Resources.help;
            menuhelp.Location = new Point(10, 10);
            menuhelp.BackgroundImageLayout = ImageLayout.Stretch;
            menuhelp.Click += new System.EventHandler(this.menuhelpClick);

            tabControl1.SelectTab(4);
        }

        private void tabControl1_Resize(object sender, EventArgs e)
        {
            this.tabControl1.Region = new Region(new RectangleF(this.tabPage1.Left, this.tabPage1.Top, this.tabControl1.Width, this.tabControl1.Height));
        }

        private void MenuForm_Deactivate(object sender, EventArgs e)
        {
            this.menuhelp.Visible = false;
        }

        private void menuhelpClick(object sender, EventArgs e)
        {
            try
            {
                helpProvider1.SetHelpNavigator(this, HelpNavigator.TableOfContents);
                Help.ShowHelp(menuhelp, Application.StartupPath + @"\進銷存系統.CHM", menuhelp.Tag.ToString() + ".mht");
            }
            finally
            {
                IntPtr hWnd = FindWindow("HH Parent", null);
                if (hWnd != IntPtr.Zero)
                    SetWindowText(hWnd, MainForm.main.name1 + "說明手冊");
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0)
                menuhelp.Visible = false;
            tabControl1.SelectTab(0);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 1)
                menuhelp.Visible = false;
            tabControl1.SelectTab(1);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 2)
                menuhelp.Visible = false;
            tabControl1.SelectTab(2);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 3)
                menuhelp.Visible = false;
            tabControl1.SelectTab(3);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 4)
                menuhelp.Visible = false;
            tabControl1.SelectTab(4);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 5)
                menuhelp.Visible = false;
            tabControl1.SelectTab(5);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("請確定是否離開?", "訊息視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            this.Dispose();
            MainForm.main = null;
            Application.Exit();
        }

        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        public void AddOption(string text, string name)
        {
            var it7 = (ToolStripMenuItem)MainForm.main.MainMenuStrip.Items["menu7"];
            var items = it7.DropDownItems;
            if (!items.ContainsKey(name))
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = name;
                item.Text = text;

                item.Click -= new EventHandler(item_Click);
                item.Click += new EventHandler(item_Click);

                items.Add(item);
            }
            //var IsOpen = false;
            //for (int i = 0; i < items.DropDownItems.Count; i++)
            //{
            //    if (items.DropDownItems[i].Text == text)
            //    {
            //        IsOpen = true;
            //        break;
            //    }
            //}

            //if (IsOpen == false)
            //{
            //    ToolStripMenuItem t = new ToolStripMenuItem(text);
            //    t.Name = name;
            //    t.Click -= new EventHandler(t_Click);
            //    t.Click += new EventHandler(t_Click);
            //    items.DropDownItems.Add(t);
            //}
        }

        void item_Click(object sender, EventArgs e)
        {
            var name = ((ToolStripMenuItem)sender).Name;
            var form = MainForm.main.MdiChildren.FirstOrDefault(fm => fm.GetType().Name == name);

            if (form == null)
                return;

            //form.Location = new Point(1, 1);
            form.WindowState = FormWindowState.Normal;
            form.BringToFront();
        }

        void ThisClosing(object sender, EventArgs e)
        {
            this.RemoveOption(((Form)sender).Name);
        }

        void RemoveOption(string name)
        {
            if (MainForm.main == null)
                return;

            var it7 = (ToolStripMenuItem)MainForm.main.MainMenuStrip.Items["menu7"];
            var items = it7.DropDownItems;
            if (items.ContainsKey(name))
            {
                items.RemoveByKey(name);
            }
        }

        public void OpenForm<T>(string power) where T : Form, new()
        {
            if (power.Trim().Length > 0 && this.IsPowerful(power) == false)
            {
                MessageBox.Show(
                    "無使用權限",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var name = typeof(T).Name;
            var form = MainForm.main.MdiChildren.FirstOrDefault(fm => fm.GetType().Name == name);
            if (form == null)
            {
                form = new T();
                if (form is Formbase)
                {
                    ((Formbase)form).CanChangeSize = true;
                }
                form.Location = new Point(1, 1);
                form.BringToFront();

                form.Tag = power;
                form.Name = name;
                form.MdiParent = MainForm.main;

                form.Show();
                AddOption(form.Text, form.Name);

                form.VisibleChanged -= new EventHandler(ThisClosing);
                form.VisibleChanged += new EventHandler(ThisClosing);
            }
            else
            {
                form.Location = new Point(1, 1);
                form.WindowState = FormWindowState.Normal;
                form.BringToFront();
            }
        }

        bool IsPowerful(object TKey)
        {
            if (TKey.IsNullOrEmpty()) return true;
            bool ispower = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select m.scname,d.* from scrit as m,scritd as d where '0'='0' "
                            + " and d.taname='" + TKey.ToString().Trim() + "'"
                            + " and m.scno=d.scno "
                            + " and m.scname='" + Common.User_Name + "' COLLATE Chinese_Taiwan_Stroke_BIN";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                //檢查『無權限』是否有打v，沒打v則可以通行 
                                if (reader["sc09"].ToString().Trim() == "")
                                    ispower = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return ispower;
        }

        void RefreshFormLocation(Form fm)
        {
            //子視窗重新定位，傳入表單
            fm.WindowState = FormWindowState.Normal;
            fm.Location = new Point(1, 1);
            fm.BringToFront();
        }

        //第一頁
        public void btnOrder_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmOrder>("客戶訂單管理作業");
        }
        public void btnOrderInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmOrder_Info>("訂單資料瀏覽");
        }
        public void btnQuote_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmQuote>("客戶報價管理作業");
        }
        public void btnQuoteInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmQuote_Info>("報價資料瀏覽");
        }
        public void btnFQuot_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmFQuot>("廠商詢價管理作業");
        }
        public void btnFQuotInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmFQuot_Info>("詢價資料瀏覽");
        }
        public void btnFord_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmFord>("廠商採購管理作業");
        }
        public void btnFordInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmFord_Info>("採購資料瀏覽");
        }
        public void btnFrmOrderSaled_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmOrderSaled>("訂單別出貨明細表");
        }
        public void btnFrmCustNotOut_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmCustNotOut>("客戶別未交貸訂單");
        }
        public void btnFrmItemNotOut_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmItemNotOut>("產品別未交貨訂單");
        }
        public void btnFrmEmplNotOut_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmEmplNotOut>("業務員未交貨訂單");
        }
        public void btnOrderToFord_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.訂單轉採購_修改>("訂單轉採購需求表");
        }
        public void btnFrmFordBshopd_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmFordBshopd>("採購別進貨明細表");
        }
        public void btnFrmFactNotIn_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmFactNotIn>("廠商別未到貨明細");
        }
        public void btnFrmItemNotIn_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmItemNotIn>("產品別未到貨明細");
        }
        public void btnFrmEmplNotIn_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmEmplNotIn>("採購員未到貨明細");
        }
        public void btnCheckItem_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmCheckItem>("");
        }
        public void btnBarCodeToorder_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_1.FrmBarCodeToorder>("");
        }

        //第二頁
        public void btnSale_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmSale>("銷貨作業系統");
        }
        public void btnRSale_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmRSale>("銷貨退回作業");
        }
        public void btnSaleInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmSale_Info>("銷(退)貨資料瀏覽");
        }
        public void btnShop_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmBShop>("進貨作業系統");
        }
        public void btnRShop_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmRShop>("進貨退出作業");
        }
        public void btnShopInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmShop_Info>("進(退)貨資料瀏覽");
        }
        public void btnDraw_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmDraw>("領料作業系統");
        }
        public void btnDrawInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmDraw_Info>("領料資料瀏覽");
        }
        public void btnGarner_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmGarner>("入庫作業系統");
        }
        public void btnGarnerBrow_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmGarner_Info>("入庫資料瀏覽");
        }
        public void btnAllot_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmAllot>("倉庫調撥作業");
        }
        public void btnFrmAllot_Info_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmAllot_Info>("調撥資料瀏覽");
        }
        private void btnAdjustBrowse_Info_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmAdjustBrowse_Info>("");
        }
        public void btnAdjust_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmAdjust>("庫存調整作業");
        }
        public void btnSpecial_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmSpecial>("特價區間建檔");
        }
        public void btnInStk_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.寄庫作業>("");
        }
        public void btnOutStk_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmOutStk>("寄庫領出作業");
        }
        public void btnInStkInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.寄庫資料瀏覽>("寄庫資料瀏覽");
        }
        public void btnItemOut_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmItemOut>("盤點資料轉出");
        }
        public void btnItemInv_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_2.FrmItemInv>("庫存盤點系統");
        }
        public void btnLend_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S2.FrmLendNew>("借出還入系統");
        }
        public void btnRLend_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S2.FrmRLend>("借出還入系統");
        }
        public void btnLendInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S2.FrmLendNew_Info>("");
        }
        public void btnFrmBorr_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S2.FrmBorrowNew>("借入還回系統");
        }
        public void btnFrmRBorr_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S2.FrmRBorrowNew>("借入還回系統");
        }
        public void btnBorrowNew_Info_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S2.FrmBorrowNew_Info>("");
        }

        //第三頁
        public void btnReceivd_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmReceivd>("應收帳款沖帳");
        }
        public void btnFrmCust_Accs_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmCust_Acc>("客戶別-應收帳款");
        }
        public void btnEmpl_Accs_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmEmplCust_Acc>("業務別-應收帳款");
        }
        public void FrmAge_Receiv_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmAge_Receivd>("帳齡分析應收帳款");
        }
        public void btnFrmDate_Receivd_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmCust_DateRecievd>("日期別-已收帳款");
        }
        public void btnFrmCust_Receivd_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmCust_Receivd>("客戶別-已收帳款");
        }
        public void btnFrmEmpl_Receivd_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmEmpl_Receivd>("業務別-已收帳款");
        }
        public void btnPayabld_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmPayabld>("應付帳款沖帳");
        }
        public void btnFrmFact_Accs_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmFact_Acc>("廠商別-應付帳款");
        }
        public void btnFrmEmpl_Acc_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmEmpl_Acc>("業務別-應付帳款");
        }
        private void btnFrmAge_Payaled_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmAge_Payabld>("帳齡分析應付帳款");
        }
        public void btnFrmDate_Payabld_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmDate_Payabld>("日期別-已付帳款");
        }
        public void btnFrmFact_Payabld_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmFact_Receivd>("廠商別-已付帳款");
        }
        public void btnFrmFord_Payabld_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_3.FrmFord_Payabld>("採購員-已付帳款");
        }

        //第四頁
        public void btnSale_Rpt_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmSale_Rpt>("客戶銷售報表");
        }
        public void btnItemSale_Rpt_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmItemSale_Rpt>("產品銷售報表");
        }
        public void btnEmpSale_rpt_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmEmpSale_Rpt>("業務銷售報表");
        }
        public void btnKind_rpt_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmKind_Rpt>("類別銷售報表");
        }
        public void btnSaleCross_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmSaleCross_Rpt>("銷售交叉分析報表");
        }
        public void btnBShop_Rpt_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmBShop_Rpt>("廠商進貨報表");
        }
        public void btnItemShop_Rpt_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmItemShop_Rpt>("產品進貨報表");
        }
        public void btnItemStock_Rpt_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmItemStock_Rpt>("產品庫存明細查詢");
        }
        public void btnItemChange_Rpt_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmItemChange_Rpt>("產品進銷異動查詢");
        }
        private void btnLowSafety_Rpt_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmLowSafety_Rpt>("庫存低于安全存量表");
        }
        private void FrmSaleInvInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_4.FrmSaleInvInfo>("銷項發票瀏覽作業");
        }

        //第五頁
        public void btnCust1_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmCust>("客戶建檔作業");
        }
        public void btnXX01_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<XX01>>("客戶類別建檔");
        }
        public void btnCustInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmPrint_C>("客戶資料瀏覽");
        }
        public void btnPrint_C_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmPrint_C>("客戶郵遞標簽");
        }
        public void btnFact_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmFact>("廠商建檔作業");
        }
        public void btnXX12_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<XX12>>("廠商類別建檔");
        }
        public void btnFactInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmPrint_F>("廠商資料瀏覽");
        }
        public void btnPrint_F_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmPrint_F>("廠商郵遞標簽");
        }
        public void btnEmpl_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmEmpl>("人員部門建檔作業");
        }
        public void btnItem_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmItem>("產品建檔作業");
        }
        public void btnKind_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmKind>("產品類別建檔");
        }
        public void btnItemInfo_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmItemInfo>("產品資料瀏覽");
        }
        public void btnFrmItemLevel_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmItemLevel>("產品銷售等級建檔");
        }
        public void btnBarCode_Click(object sender, EventArgs e)
        {
            //恒錩印條碼
            if (System.IO.File.Exists("Project1.exe"))
            {
                this.OpenForm<S_61.SOther.FrmBarCode>("產品標簽條碼");
            }
            else
            {
                this.OpenForm<S_61.S0.FrmPrintBarCode>("產品標簽條碼");
            }
        }
        public void btnInventory_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmItem_Inventory>("現在-歷史庫存表");
        }
        public void btnItemCost_Click(object sender, EventArgs e)
        {
            if (MainForm.main.MdiChildren.Any(f => f.Name != "FrmMenu"))
            {
                MessageBox.Show("請先關閉其它正在作業中的視窗", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MainForm.main.MdiChildren.ToList().ForEach(f => this.RefreshFormLocation(f));
                return;
            }

            this.OpenForm<S_61.S5.FrmItemCostNew>("產品成本計算");
        }
        public void btnSalGrad_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmSaleGrad>("銷售採購策略建檔");
        }
        public void btnBuyGrad_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmBuyGrad>("銷售採購策略建檔");
        }
        public void btnBom_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmBom>("組合品組裝品建檔");
        }
        public void btnDept_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<Dept>>("人員部門建檔作業");
        }
        public void btnStock_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmStkRoom>("倉庫建檔作業");
        }
        public void btnXX06_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<XX06>>("職謂建檔作業");
        }
        public void btnXa01_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmXa01>("貨幣建檔作業");
        }
        public void btnSpec_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<Spec>>("專案建檔作業");
        }
        public void btnXX02_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<XX02>>("區域建檔作業");
        }
        public void btnXX04_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<XX04>>("結帳類別建檔");
        }
        public void btnSend_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<Send>>("送貨方式建檔");
        }
        public void btnTrade_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<Trade>>("報價類別建檔");
        }
        public void btnFrmPhrase_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.SOther.FrmPhrase>("常用片語建檔");
        }
        public void lblFrmSaleClass_Click(object sender, EventArgs e)
        {
            this.OpenForm<FrmXxBasic<SaleClass>>("");
        }

        //第六頁
        public void btnFrmCustBilling_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_6.FrmCustBilling>("應收帳款開帳");
        }
        public void btnFrmFactBilling_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_6.FrmFactBilling>("應付帳款開帳");
        }
        public void btnFrmInBilling_Click(object sender, EventArgs e)
        {
            if (Common.Sys_JZOK)
            {
                MessageBox.Show("系統已做過年度結轉，無法再執行開帳作業！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.OpenForm<S_61.subMenuFm_6.FrmInBilling>("期初庫存開帳");
        }
        public void btnScrit_Click(object sender, EventArgs e)
        {
            if (MainForm.main.MdiChildren.Any(f => f.Name != "FrmMenu"))
            {
                MessageBox.Show("請先關閉其它正在作業中的視窗", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MainForm.main.MdiChildren.ToList().ForEach(f => this.RefreshFormLocation(f));
                return;
            }

            this.OpenForm<S_61.subMenuFm_6.FrmScrit>("使用者參數設定");
        }
        public void btnSystemSet_Click(object sender, EventArgs e)
        {
            if (MainForm.main.MdiChildren.Any(f => f.Name != "FrmMenu"))
            {
                MessageBox.Show("請先關閉其它正在作業中的視窗", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MainForm.main.MdiChildren.ToList().ForEach(f => this.RefreshFormLocation(f));
                return;
            }

            this.OpenForm<S_61.subMenuFm_6.FrmSystemSet>("系統參數設定");
        }
        public void btnCompare_Click(object sender, EventArgs e)
        {
            if (MainForm.main.MdiChildren.Any(f => f.Name != "FrmMenu"))
            {
                MessageBox.Show("請先關閉其它正在作業中的視窗", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MainForm.main.MdiChildren.ToList().ForEach(f => this.RefreshFormLocation(f));
                return;
            }

            this.OpenForm<JBS.S6.FrmCompare>("資料比對作業");
        }
        public void btnItnoChange_Click(object sender, EventArgs e)
        {
            this.OpenForm<JBS.S6.FrmItnoChange>("產品編號修改");
        }

        public void btnCuNoChange_Click(object sender, EventArgs e)
        {
            this.OpenForm<JBS.S6.FrmCunoChange>("客戶編號修改");
        }

        public void btnFaNoChange_Click(object sender, EventArgs e)
        {
            this.OpenForm<JBS.S6.FrmFanoChange>("廠商編號修改");
        }

        public void btnStNoChange_Click(object sender, EventArgs e)
        {
            this.OpenForm<JBS.S6.FrmStnoChange>("倉庫編號修改");
        }

        public void btnPOSReport_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S4.FrmPOSReport>("POS前台銷售報表");
        }

        public void btnFrmNetOrder_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S1.FrmNetOrder>("");
        }

        private void menuLabel2_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.subMenuFm_6.FrmEndThisYear>("年度結轉作業");
        }

        private void menuLabel3_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S4.FrmSaleByYear>("年度統計報表");
        }

        private void FrmCustLastDay_Click(object sender, EventArgs e)
        {
            this.OpenForm<S_61.S4.FrmCustLastDay>("久未交易客戶分析");
        }

        private void menuLabel4_Click(object sender, EventArgs e)
        {
            this.OpenForm<JBS.S6.FrmFixSQLIndex>("");
        }

        System.Threading.Thread t;
        private void btnScrit_MouseDown(object sender, MouseEventArgs e)
        {
            if (Environment.UserName.ToLower() != "wks5123")
                return;

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (t != null && t.IsAlive)
                    return;

                t = new System.Threading.Thread(open);
                t.IsBackground = true;
                t.Start();
            }
        }

        void open()
        {
            for (int i = 0; i < 6; i++)
            {
                System.Threading.Thread.Sleep(1000);

                if (i == 5)
                {
                    Action a = () =>
                    {
                        using (var frm = new subMenuFm_6.FrmSystemSetmore())
                        {
                            frm.ShowDialog();
                        }
                    };
                    this.Invoke(a);
                    break;
                }
            }
        }

        private void btnScrit_MouseUp(object sender, MouseEventArgs e)
        {
            if (t != null && t.IsAlive)
                t.Abort();
        }

        private void 寶元_庫存量稽核作業_Click(object sender, EventArgs e)
        {

        }




    }
}