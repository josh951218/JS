using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Drawing;
using System.Threading;
using System.Linq;

namespace S_61.subMenuFm_1
{
    public partial class FrmBarCodeToorder : Formbase
    {
        DataTable tb = new DataTable();
        DataTable tbD = new DataTable();
        DataTable tbBom = new DataTable();
        Label lbShow = new Label();
        Thread showTD;

        JBS.JS.Sale jSale = new JBS.JS.Sale();

        public FrmBarCodeToorder()
        {
            InitializeComponent();
            SaDate.SetDateLength();
            tbD.TableName = "cudt";
            this.tabControlT1.ItemSize = new Size(0, 1);
            this.訂單數量.Set庫存數量小數();
            this.已撿貨量.Set庫存數量小數();
            dataGridViewT1.DataSource = tbD;

        }

        private void FrmBarCodeToorder_Load(object sender, EventArgs e)
        {
            init();
        }

        void init()
        {
            ItNo.Clear();
            OrNo.Clear();
            Qty.Text = (0M).ToString("f" + Common.Q);

            tb.Clear();
            tbD.Clear();
            tbBom.Clear();

            //銷貨單&帳款&發票
            payerno.Clear();
            InvNo.Clear();
            SaNo.Clear();
            X5No.Clear();
            User_Einv.Text = Common.User_Einv;
            iTitle.Text = Common.iTitle;
            SaDate.Text = Date.GetDateTime(Common.User_DateTime);
            StNo.Text = Common.User_StkNo;
            StName.Clear();
            InvName.Clear();
            InvTaxNo.Clear();
            InvAddr1.Clear();
            CuAddr1.Clear();
           
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.Parameters.AddWithValue("key", "");

                cmd.Parameters["key"].Value = StNo.Text.Trim();
                cmd.CommandText = "select stname from stkroom where stno=@key";
                StName.Text = ResultSTR(cmd.ExecuteScalar());
            }
 
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnPreView.Enabled = false;

            OrNo.Focus();
        }


        private void OrNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmOrder_Print_OrNo())
            {
                frm.TSeekNo = OrNo.Text.Trim();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    OrNo.Text = frm.TResult;
                }
            }
        }
        private void OrNo_Validating(object sender, CancelEventArgs e)
        {
            if (OrNo.TrimTextLenth() == 0) 
            {
                OrNo.Clear();
                return;
            }
            if (btnExit.Focused) return;

            tb.Clear();
            tbD.Clear();
            tbBom.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                cmd.CommandText = "select * from [order] where orno=@orno";
                dd.Fill(tb);

                if (tb.Rows.Count == 0)
                {
                    ShowMessage("查無訂單憑證" + OrNo.Text.Trim() + "!!!", true);
                    e.Cancel = true;
                    OrNo.Clear();
                    return;
                }

                if (tb.Rows[0]["orpickingflag"].ToString() == "True")
                {
                    ShowMessage("訂單憑證" + OrNo.Text.Trim() + "，已撿貨完畢!!!", true);
                    e.Cancel = true;
                    OrNo.Clear();
                    return;
                }

                cmd.CommandText = "select *,已撿貨量=0,stno='"+Common.User_StkNo+"',單據日期='' from [orderd] where orno=@orno";
                dd.Fill(tbD);
                var datatype = Common.User_DateTime == 1 ? "" : "1";
                foreach (DataRow item in tbD.Rows)
                {
                    item["單據日期"] = Date.AddLine(item["ordate" + datatype].ToString());
                }

                cmd.CommandText = "select * from [orderbom] where orno=@orno";
                dd.Fill(tbBom);

                GetOrNoInfo(cmd);
            }
            
        }
        void GetOrNoInfo(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("key", tb.Rows[0]["cuno"].ToString().Trim());
            cmd.CommandText = "select * from cust where cuno=@key";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows && reader.Read())
                {
                    payerno.Text = reader["payerno"].ToString().Trim() == "" ? reader["cuno"].ToString().Trim() : reader["payerno"].ToString().Trim();
                    X5No.Text = reader["cux5no"].ToString().Trim();
                    InvName.Text = reader["CuInvoName"].ToString().Trim();
                    if (InvName.Text.Trim() == "")
                        InvName.Text = reader["CuName2"].ToString();
                    InvTaxNo.Text = reader["CuUno"].ToString().Trim();
                    InvAddr1.Text = reader["Cuaddr2"].ToString();
                    CuAddr1.Text = reader["Cuaddr1"].ToString();

                    if (reader["cux5no"].ToDecimal() == 7 || reader["cux5no"].ToDecimal() == 8)//使用
                        EInv1.Checked = true;
                    else
                        EInv2.Checked = true;
                }
            }


        }
        private void ItNo_Validating(object sender, CancelEventArgs e)
        {
            if (ItNo.TrimTextLenth() == 0)
            {
                ItNo.Clear();
                return;
            }
            if (btnExit.Focused) return;

            var hasrow = tbD.AsEnumerable().Where(r => r["itno"].ToString().Trim() == ItNo.Text.Trim());
            if (hasrow.Count() == 0)
            {
                ShowMessage("產品編號:" + ItNo.Text.Trim() + "\n不再訂單明細內", true);
                e.Cancel = true;
                ItNo.Clear();
                return;
            }

            if (!hasrow.Any(r => r["qtynotout"].ToDecimal() != r["已撿貨量"].ToDecimal()))
            {
                ShowMessage("產品編號:" + ItNo.Text.Trim() + "\n已撿貨完畢", true);
                e.Cancel = true;
                ItNo.Clear();
                return;
            }

            var fristrow = hasrow.First(r => r["itno"].ToString().Trim() == ItNo.Text.Trim() && r["qty"].ToDecimal() > r["已撿貨量"].ToDecimal());
            fristrow["已撿貨量"] = fristrow["已撿貨量"].ToDecimal() + 1;

            if (fristrow["qtynotout"].ToDecimal() == fristrow["已撿貨量"].ToDecimal())
            {
                ShowMessage("產品編號:" + ItNo.Text.Trim() + "\n已撿貨完畢", false);
            }

            ItNo.Clear();
            e.Cancel = true;
            CheckAllReady();
        }
        private void btnQty_Click(object sender, EventArgs e)
        {
            if (Qty.Text.ToDecimal() == 0) return;
            if (dataGridViewT1.Rows.Count == 0) return;

            var selectrow = tbD.Rows[dataGridViewT1.SelectedRows[0].Index];
            var 剩餘數量 = selectrow["qtynotout"].ToDecimal() - selectrow["已撿貨量"].ToDecimal();
            if (Qty.Text.ToDecimal() > 剩餘數量)
            {
                ShowMessage("產品編號:" + selectrow["itno"].ToString() + "\n批次數量大於剩餘撿貨量", true);
                Qty.Text = (0M).ToString("f" + Common.Q);
                return;
            }

            selectrow["已撿貨量"] = selectrow["已撿貨量"].ToDecimal() + Qty.Text.ToDecimal();

            if (selectrow["qtynotout"].ToDecimal() == selectrow["已撿貨量"].ToDecimal())
            {
                ShowMessage("產品編號:" + ItNo.Text.Trim() + "\n已撿貨完畢", false);
            }
            Qty.Text = (0M).ToString("f" + Common.Q);
            ItNo.Focus();
            CheckAllReady();
        }
        private void Qty_Validating(object sender, CancelEventArgs e)
        {
            btnQty_Click(null, null);
        }
        void CheckAllReady()
        {
            if (tbD.AsEnumerable().Any(r => r["qtynotout"].ToDecimal() != r["已撿貨量"].ToDecimal()))
            {
                return;
            }
            else
            {
                //showTD.Join();
                ShowMessage("訂單貨品已全數撿貨完畢\n按儲存鍵可轉銷貨單", false);
                btnSave.Enabled = true;
                btnPrint.Enabled = true;
                btnPreView.Enabled = true;
            }
        }
        void ShowMessage(string message,bool isEroor)
        {
            try
            {
                if (showTD != null && showTD.IsAlive)
                    showTD.Abort();
                if (this.Controls.ContainsKey("lbShow"))
                    this.Controls.RemoveByKey(lbShow.Name);

                lbShow.Text = message;
                lbShow.Font = new System.Drawing.Font("細明體", 50F, FontStyle.Bold);

                if (isEroor)
                {
                    lbShow.BackColor = Color.HotPink;
                    Console.Beep(12000, 500);
                }
                else
                {
                    lbShow.BackColor = Color.DeepSkyBlue;
                }
                this.Controls.Add(lbShow);
                lbShow.BringToFront();

                showTD = new Thread(() =>
                {
                    Thread.Sleep(1000);
                    Invoke((MethodInvoker)delegate
                    {
                        this.Controls.RemoveByKey(lbShow.Name);
                    });
                });
                showTD.IsBackground = true;
                showTD.Start();
            }
            catch (Exception ex)
            {
            }
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            btnQty.Location = new System.Drawing.Point(Qty.Location.X + Qty.Width + 10, btnQty.Location.Y);

            lbShow.Name = "lbShow";

            lbShow.Width = dataGridViewT1.Width;
            lbShow.Height = this.Height / 3;
            lbShow.Location = new System.Drawing.Point(dataGridViewT1.Location.X, this.Height / 3);

        }


        void dataintodocument(RptMode mode)
        {
            string path = Common.reportaddress;
            path += "ReportF\\訂單撿貨報表_標籤.frx";

            if (System.IO.File.Exists(path) == false)
            {
                MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                FastReport.dy.Add("txtstart", Common.Sys_StcPnName);

                FastReport.PreView(path, tbD, tbD.TableName, null, null, mode, path);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Print);
        }
        private void btnPreView_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.PreView);
        }
        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string path = Common.reportaddress;
                path += "ReportF\\訂單撿貨報表_標籤.frx";

                var dl = MessageBox.Show("是否要修改報表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dl != DialogResult.Yes) return;

                JBS.FReport.Design(path);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (jSale.IsEditInCloseDay(Date.GetDateTime(Common.User_DateTime)) == false)
                return;

            if (jSale.IsRegisted() == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！" + Environment.NewLine + "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();
                cmd.Transaction = tran;
                try
                {


                    System.Threading.Tasks.Task tk;

                    //銷貨單號
                    var result = true;
                    result &= jSale.GetPkNumber<JBS.JS.Sale>(cmd, SaDate.Text, ref SaNo);
                    if (!result)
                    {
                        MessageBox.Show("單號取得失敗!");
                        return;
                    }

                    //自動取得發票號碼
                    bool repeat;
                    this.AutoGetInvNo(cmd, out repeat);
                    if (repeat)
                        return ;
                    //檢查發票
                    if (!InvNoCheck(cmd)) return ;

                    //訂單標註已撿貨
                    cmd.Parameters.AddWithValue("orno", OrNo.Text.Trim());
                    cmd.CommandText = "update [order] set orpickingflag = '1',oroverflag='1' where orno = @orno;";//檢貨及結案標記
                    cmd.CommandText += "update orderd set qtyNotOut ='0' where orno = @orno";
                    cmd.ExecuteNonQuery();

                    this.AppendMasterOnSaving(cmd);

                    for (int i = 0; i < tbD.Rows.Count; i++)
                    {
                        //儲存明細
                        this.AppendDetailOnSaving(cmd, i);
                    }

                    //儲存組件
                    this.AppendBomOnSaving(cmd);

                    //處理沖款
                    this.PassToReceivOnSaving(cmd);

                    //處理庫存
                    jSale.扣庫存(cmd, tbD, tbBom,"stno","qtynotout");

                    tran.Commit();

                    //儲存完成
                    jSale.Save(SaNo.Text.Trim());
                    string tepayerno = payerno.Text.ToString().Trim();
                    tk = System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        //更新客戶應收帳款
                        jSale.UpdateNewCustReceiv(tepayerno, tb.Rows[0]["totmny"].ToString().Trim(), "0");

                        //更新客戶交易日期
                        jSale.UpdateCustLastDay(SaDate.Text.Trim(), tb.Rows[0]["CuNo"].ToString().Trim());

                        //更新最近進貨日期
                        jSale.UpdateItemDate(SaDate.Text, ref tbD);

                        //更新產品檔庫存量
                        jSale.UpdateItemItStockQty(tbD, tbBom);
                    });

                    if (tk != null)
                        tk.Wait();

                    init();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    tran.Dispose();
                }
            }
            
        }
        private void AppendMasterOnSaving(SqlCommand cmd)
        {
            var row = tb.Rows[0];
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.Parameters.AddWithValue("sadate1", Date.ToUSDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadate2", SaDate.Text.Trim());
            cmd.Parameters.AddWithValue("sadateac1", Date.ToUSDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac2", SaDate.Text.Trim());
            cmd.Parameters.AddWithValue("quno", "");
            cmd.Parameters.AddWithValue("cuno", row["CuNo"].ToString().Trim());
            cmd.Parameters.AddWithValue("cuname1", row["CuName1"].ToString().Trim());
            cmd.Parameters.AddWithValue("emno", row["EmNo"].ToString().Trim() );
            cmd.Parameters.AddWithValue("emname", row["EmName"].ToString().Trim());
            cmd.Parameters.AddWithValue("spno", row["SpNo"].ToString().Trim());
            cmd.Parameters.AddWithValue("spname", row["SpName"].ToString().Trim());
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", row["Xa1No"].ToString().Trim());
            cmd.Parameters.AddWithValue("xa1name", row["Xa1Name"].ToString().Trim());
            cmd.Parameters.AddWithValue("xa1par", row["Xa1Par"].ToDecimal());
            cmd.Parameters.AddWithValue("taxmnyb", row["TaxMnyB"].ToString().Trim());
            cmd.Parameters.AddWithValue("taxmny", row["TaxMny"].ToString().Trim());
            cmd.Parameters.AddWithValue("x3no", row["X3No"].ToString().Trim());
            cmd.Parameters.AddWithValue("rate", "0.050");
            cmd.Parameters.AddWithValue("x5no", X5No.Text.Trim());
            cmd.Parameters.AddWithValue("seno", "");
            cmd.Parameters.AddWithValue("sename", "");
            cmd.Parameters.AddWithValue("x4no", "");
            cmd.Parameters.AddWithValue("x4name", "");
            cmd.Parameters.AddWithValue("tax", row["Tax"].ToString().Trim());
            cmd.Parameters.AddWithValue("totmny", row["TotMny"].ToString().Trim());
            cmd.Parameters.AddWithValue("taxb", row["Taxb"].ToString().Trim());//本幣營業稅額 = 外幣營業稅額*匯率
            cmd.Parameters.AddWithValue("totmnyb", row["totmnyb"].ToString().Trim());//本幣總額 = 外幣總額*匯率
            cmd.Parameters.AddWithValue("discount", 0);
            cmd.Parameters.AddWithValue("cashmny", 0);
            cmd.Parameters.AddWithValue("cardmny", 0);
            cmd.Parameters.AddWithValue("cardno", "");
            cmd.Parameters.AddWithValue("ticket", 0);
            cmd.Parameters.AddWithValue("collectmny", 0);
            cmd.Parameters.AddWithValue("getprvacc",0);
            cmd.Parameters.AddWithValue("acctmny", row["TotMny"].ToString().Trim());
            cmd.Parameters.AddWithValue("samemo", row["ormemo"].ToString().Trim());
            cmd.Parameters.AddWithValue("bracket", "");
            cmd.Parameters.AddWithValue("recordno", row["recordno"].ToString().Trim());
            cmd.Parameters.AddWithValue("invno", InvNo.Text.Trim());
            cmd.Parameters.AddWithValue("invdate", Date.GetDateTime(1));
            cmd.Parameters.AddWithValue("invdate1", Date.GetDateTime(2));
            cmd.Parameters.AddWithValue("invname", InvName.Text.Trim());
            cmd.Parameters.AddWithValue("invtaxno", InvTaxNo.Text.Trim());
            cmd.Parameters.AddWithValue("invaddr1", InvAddr1.Text.Trim());
            cmd.Parameters.AddWithValue("invbatch", 2);//發票批開選定
            cmd.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("appscno", Common.User_Name);
            cmd.Parameters.AddWithValue("edtscno", Common.User_Name);
            cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("samemo1", row["ormemo1"]);
            cmd.Parameters.AddWithValue("deno", "");
            cmd.Parameters.AddWithValue("dename", "");
            cmd.Parameters.AddWithValue("cutel1", row["CuTel1"].ToString().Trim());
            cmd.Parameters.AddWithValue("cuper1", row["CuPer1"].ToString().Trim());
            cmd.Parameters.AddWithValue("CuAddr", CuAddr1.Text.Trim());
            cmd.Parameters.AddWithValue("Adper1", row["cuper1"].ToString().Trim());//指送負責人
            cmd.Parameters.AddWithValue("Adtel", row["cutel1"].ToString().Trim());//指送電話
            cmd.Parameters.AddWithValue("AdAddr", row["adaddr"].ToString().Trim());//指送地址
            cmd.Parameters.AddWithValue("SaPayment", "");
            cmd.Parameters.AddWithValue("PhotoPath", ""); //13.7a
            cmd.Parameters.AddWithValue("einv", EInv1.Checked ? "1" : "2");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvstate", EInv1.Checked ? "未上傳" : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("einvchange", EInv1.Checked ? "存證" : "");//發票狀態 使用電子發票
            cmd.Parameters.AddWithValue("invrandom", jSale.GetInvoiceRandom());//發票隨機防伪碼 4碼
            cmd.Parameters.AddWithValue("payerno", payerno.Text.ToString());
            cmd.Parameters.AddWithValue("User_Einv", User_Einv.Text);//電子發票設定
            cmd.Parameters.AddWithValue("iTitle", iTitle.Text);//電子發票設定

            //媒體申報
            string invkind = "", passmode = "", specialtax="";
            if (InvNo.Text.Trim() != "")
            {
                if (invkind == "")
                {
                    if (X5No.Text == "1")
                    {
                        invkind = "31 銷項三聯式".GetUTF8(20);
                    }
                    else if (X5No.Text == "2")
                    {
                        invkind = "32 銷項二聯式".GetUTF8(20);
                    }
                    else if (X5No.Text == "3")
                    {
                        invkind = "32 銷項二聯式收銀機統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "4")
                    {
                        invkind = "35 銷項三聯式收銀機統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "5")
                    {
                        invkind = "36 銷項免用統一發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "7")
                    {
                        invkind = "35 銷項一般稅額計算之電子發票".GetUTF8(20);
                    }
                    else if (X5No.Text == "8")
                    {
                        invkind = "37 特種稅額之銷項憑證(含特種稅額計算之電子發票)".GetUTF8(20);
                    }
                }

                if (tb.Rows[0]["X3No"].ToDecimal() != 3)
                    passmode = "";
                else
                {
                    if (passmode == "")
                        passmode = "1";
                }

                if (invkind.Substring(0, 2) != "37")
                    specialtax = "";
            }
            else
            {
                if (X5No.Text.Trim() == "5")
                {
                    if (invkind == "")
                        invkind = "36 銷項免用統一發票".GetUTF8(20);

                    specialtax = "";

                    if (tb.Rows[0]["X3No"].ToDecimal() != 3)
                        passmode = "";
                    else
                    {
                        if (passmode == "")
                            passmode = "1";
                    }
                }
                else
                {
                    invkind = "";
                    specialtax = "";
                    passmode = "";
                }
            }

            cmd.Parameters.AddWithValue("invkind", invkind);
            cmd.Parameters.AddWithValue("passmode", passmode);
            cmd.Parameters.AddWithValue("specialtax", specialtax);

            cmd.CommandText = @"
            INSERT INTO Sale (
            sano,sadate1,sadate2,sadateac1,sadateac2,quno,cuno,cuname1,cutel1,cuper1,CuAddr
            ,emno,emname,spno,spname,stno,stname,xa1no,xa1name,xa1par
            ,taxmnyb,taxmny,x3no,rate,x5no,seno,sename,x4no,x4name,tax,totmny
            ,taxb,totmnyb,discount,cashmny,cardmny,cardno,ticket,collectmny
            ,getprvacc,acctmny,samemo,bracket,recordno,invno
            ,invdate,invdate1,invname,invtaxno,invaddr1,invbatch
            ,appdate,edtdate,appscno,edtscno,sadate,sadateac,samemo1,deno,dename,Adper1,Adtel,AdAddr,SaPayment,PhotoPath
            ,invkind,passmode,specialtax,einvstate,einvchange,einv,invrandom,payerno,User_Einv,iTitle 
            ) VALUES (
            @sano,@sadate1,@sadate2,@sadateac1,@sadateac2,@quno,@cuno,@cuname1,@cutel1,@cuper1,@CuAddr
            ,@emno,@emname,@spno,@spname,@stno,@stname,@xa1no,@xa1name,@xa1par
            ,@taxmnyb,@taxmny,@x3no,@rate,@x5no,@seno,@sename,@x4no,@x4name,@tax,@totmny
            ,@taxb,@totmnyb,@discount,@cashmny,@cardmny,@cardno,@ticket,@collectmny
            ,@getprvacc,@acctmny,@samemo,@bracket,@recordno,@invno
            ,@invdate,@invdate1,@invname,@invtaxno,@invaddr1,@invbatch
            ,@appdate,@edtdate,@appscno,@edtscno,@sadate,@sadateac,@samemo1,@deno,@dename,@Adper1,@Adtel,@AdAddr,@SaPayment,@PhotoPath
            ,@invkind,@passmode,@specialtax,@einvstate,@einvchange,@einv,@invrandom,@payerno,@User_Einv,@iTitle)";
            cmd.ExecuteNonQuery();
        }
        private void AutoGetInvNo(SqlCommand cmd, out bool repeat)
        {
            //自動取得發票，純自動取得發票
            //檢查部分，由另一函式執行
            repeat = false;

            //使用者設定為自動，才繼續設定發票
            if (Common.User_ScInvSlt == 1)
                return;

            //自動，使用電子發票，但使用者沒有設定[電子發票編號]
            if (User_Einv.TrimTextLenth() == 0)
            {
                MessageBox.Show("開立電子發票，請設定[使用者參數設定]->[電子發票設定]");
                return;
            }

            //單據金額0，不自動取得發票
            if (tb.Rows[0]["TotMny"].ToDecimal() == 0)
                return;

            //若為true，代表自動，手打發票
            bool IsUserInput = false;

            var scvno = "";
            var invno = "";
            var inv = "";
            var no = 0M;
            object obj;
            bool exist = false;

            if (IsUserInput)
                invno = InvNo.Text;
            else
                InvNo.Clear();

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@InvNo", "");
            cmd.Parameters.AddWithValue("@User", Common.User_Name);
            cmd.Parameters.AddWithValue("@Einvid", User_Einv.Text.ToString().Trim());//電子發票 改這
            if (X5No.Text.Trim().ToInteger() == 2)//二聯
            {
                #region 自動二聯式發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoic2 from Scrit where ScName = (@User)";
                    obj = cmd.ExecuteScalar();
                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)
                    {
                        MessageBox.Show("沒有設定二聯式發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                //檢查發票號碼是否大於 終止號碼
                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = " Select Count(*) from scrit where ScName = (@User) And (@InvNo) > ScInvoic2e ";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Scrit Set ScInvoic2 = (@InvNo) where ScName = (@User)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToInteger() == 1)//三聯
            {
                #region 自動三聯式發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoic3 from Scrit where ScName = (@User)";
                    obj = cmd.ExecuteScalar();

                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)
                    {
                        MessageBox.Show("沒有設定三聯式發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                //檢查發票號碼是否大於 終止號碼
                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = " Select Count(*) from scrit where ScName = (@User) And (@InvNo) > ScInvoic3e ";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Scrit Set ScInvoic3 = (@InvNo) where ScName = (@User)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToInteger() == 7)//電子發票取號
            {
                #region 電子發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoic7 from Einvsetup where Einvid = (@Einvid)";
                    obj = cmd.ExecuteScalar();

                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)//取得發票不足10碼，可能電子發票設定有誤
                    {
                        MessageBox.Show("沒有設定正確的電子發票號碼!(電子發票自動取號)");
                        repeat = true;
                        return;
                    }
                }

                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = "Select Count(*) from Einvsetup where Einvid = (@Einvid) And (@InvNo) > ScInvoic7e";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Einvsetup Set ScInvoic7 = (@InvNo) where Einvid = (@Einvid)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToInteger() == 8)//電子發票取號
            {
                #region 電子發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoic8 from Einvsetup where Einvid = (@Einvid)";
                    obj = cmd.ExecuteScalar();

                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)//取得發票不足10碼，可能電子發票設定有誤
                    {
                        MessageBox.Show("沒有設定正確的電子發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = "Select Count(*) from Einvsetup where Einvid = (@Einvid) And (@InvNo) > ScInvoic8e";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Einvsetup Set ScInvoic8 = (@InvNo) where Einvid = (@Einvid)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToDecimal() == 3)//二聯收銀機
            {
                #region 自動二聯收銀機發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoicA from Scrit where ScName = (@User)";
                    obj = cmd.ExecuteScalar();
                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)
                    {
                        MessageBox.Show("沒有設定二聯收銀機發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                //檢查發票號碼是否大於 終止號碼
                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = " Select Count(*) from scrit where ScName = (@User) And (@InvNo) > ScInvoicAe ";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Scrit Set ScInvoicA = (@InvNo) where ScName = (@User)";
                cmd.ExecuteNonQuery();
                #endregion
            }
            else if (X5No.Text.Trim().ToDecimal() == 4)//三聯收銀機
            {
                #region 自動三聯收銀機發票取號
                if (!IsUserInput)
                {
                    cmd.CommandText = " Select ScInvoicA3 from Scrit where ScName = (@User)";
                    obj = cmd.ExecuteScalar();
                    if (obj != null)
                        invno = obj.ToString();

                    if (invno.Trim().Length != 10)
                    {
                        MessageBox.Show("沒有設定三聯收銀機發票號碼!");
                        repeat = true;
                        return;
                    }
                }

                //檢查發票號碼是否大於 終止號碼
                cmd.Parameters["@InvNo"].Value = invno.Trim();
                cmd.CommandText = " Select Count(*) from scrit where ScName = (@User) And (@InvNo) > ScInvoicA3e ";
                if (cmd.ExecuteScalar().ToDecimal() > 0)
                {
                    if (IsUserInput)
                        MessageBox.Show("發票號碼超過終止號碼，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("發票號碼已使用完畢，無法存檔！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    repeat = true;
                    return;
                }

                InvNo.Text = invno;

                //發票 + 1 寫回使用者參數
                inv = invno.Trim().takeString(2);
                no = invno.Trim().skipString(2).ToDecimal() + 1;
                scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                do
                {
                    cmd.Parameters["@InvNo"].Value = scvno.Trim();
                    cmd.CommandText = "Select Count(*) from Sale where InvNo = (@InvNo)";
                    exist = cmd.ExecuteScalar().ToDecimal() == 1;
                    if (exist)
                    {
                        inv = scvno.Trim().takeString(2);
                        no = scvno.Trim().skipString(2).ToDecimal() + 1;
                        scvno = inv + (no.ToString("f0").PadLeft(8, '0'));
                    }
                } while (exist);

                if (no > 99999999)
                    scvno = "";

                cmd.Parameters["@InvNo"].Value = scvno.Trim();
                cmd.CommandText = "Update Scrit Set ScInvoicA3 = (@InvNo) where ScName = (@User)";
                cmd.ExecuteNonQuery();
                #endregion
            }
        }
        private bool InvNoCheck(SqlCommand cmd)
        {
            if (InvNo.TrimTextLenth() == 0) return true;
            //新增狀態 或 修改狀態發票號碼異動下，檢查發票號碼是否重複
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@InvNo", InvNo.Text.Trim());
            cmd.CommandText = @"Select count(*) from
                (
                 (select invno from Sale )
                 union all	
                 (select invno from posinv)
                 union all	
                 (select invno from sinv)
                ) POS與進銷存銷貨單以及銷項發票登入
                 where InvNo = @InvNo";

            if (cmd.ExecuteScalar().ToDecimal() >= 1)
            {
                MessageBox.Show("此發票編號已存在，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InvNo.SelectAll();
                return false;
            }
            //作廢
            cmd.CommandText = "select invno from nullify where invno = @InvNo";
            if (!cmd.ExecuteScalar().IsNullOrEmpty())
            {
                MessageBox.Show("此發票編號已有作廢紀錄，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InvNo.SelectAll();
                return false;
            }
            return true;
        }
        private void AppendDetailOnSaving(SqlCommand cmd, int i)
        {
            var row = tb.Rows[0];
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.Parameters.AddWithValue("sadate1", Date.ToUSDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadate2", SaDate.Text.Trim());
            cmd.Parameters.AddWithValue("sadateac1", Date.ToUSDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac2", SaDate.Text.Trim());
            cmd.Parameters.AddWithValue("quno", "");
            cmd.Parameters.AddWithValue("cuno", row["CuNo"].ToString().Trim());
            cmd.Parameters.AddWithValue("cuname1", row["CuName1"].ToString().Trim());
            cmd.Parameters.AddWithValue("emno", row["EmNo"].ToString().Trim());
            cmd.Parameters.AddWithValue("emname", row["EmName"].ToString().Trim());
            cmd.Parameters.AddWithValue("spno", row["SpNo"].ToString().Trim());
            cmd.Parameters.AddWithValue("spname", row["SpName"].ToString().Trim());
            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
            cmd.Parameters.AddWithValue("stname", StName.Text.Trim());
            cmd.Parameters.AddWithValue("xa1no", row["Xa1No"].ToString().Trim());
            cmd.Parameters.AddWithValue("xa1par", row["Xa1Par"].ToDecimal());
            cmd.Parameters.AddWithValue("seno", "");
            cmd.Parameters.AddWithValue("sename", "");
            cmd.Parameters.AddWithValue("x4no", "");
            cmd.Parameters.AddWithValue("x4name", "");

            cmd.Parameters.AddWithValue("itno", tbD.Rows[i]["itno"].ToString().Trim());
            cmd.Parameters.AddWithValue("itname", tbD.Rows[i]["itname"].ToString());
            cmd.Parameters.AddWithValue("ittrait", tbD.Rows[i]["ittrait"].ToString().Trim());
            cmd.Parameters.AddWithValue("itunit", tbD.Rows[i]["itunit"].ToString().Trim());
            cmd.Parameters.AddWithValue("itpkgqty", tbD.Rows[i]["itpkgqty"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("qty", tbD.Rows[i]["qtynotout"].ToDecimal("f" + Common.Q));
            cmd.Parameters.AddWithValue("price", tbD.Rows[i]["price"].ToDecimal("f" + Common.MS));
            cmd.Parameters.AddWithValue("prs", tbD.Rows[i]["prs"].ToDecimal("f3"));
            cmd.Parameters.AddWithValue("rate", "0.050");
            cmd.Parameters.AddWithValue("taxprice", tbD.Rows[i]["taxprice"].ToDecimal("f6"));
            cmd.Parameters.AddWithValue("mny", tbD.Rows[i]["mny"].ToDecimal("f" + Common.TPS));
            cmd.Parameters.AddWithValue("priceb", tbD.Rows[i]["priceb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("taxpriceb", tbD.Rows[i]["taxpriceb"].ToDecimal("f6"));
            cmd.Parameters.AddWithValue("mnyb", tbD.Rows[i]["mnyb"].ToDecimal("f" + Common.M));
            cmd.Parameters.AddWithValue("memo", tbD.Rows[i]["Memo"].ToString().Trim().GetUTF8(20));
            cmd.Parameters.AddWithValue("bomid", SaNo.Text + tbD.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
            cmd.Parameters.AddWithValue("bomrec", tbD.Rows[i]["BomRec"]);
            cmd.Parameters.AddWithValue("recordno", (i + 1).ToString());
            cmd.Parameters.AddWithValue("bracket", "");
            cmd.Parameters.AddWithValue("itdesp1", tbD.Rows[i]["ItDesp1"]);
            cmd.Parameters.AddWithValue("itdesp2", tbD.Rows[i]["ItDesp2"]);
            cmd.Parameters.AddWithValue("itdesp3", tbD.Rows[i]["ItDesp3"]);
            cmd.Parameters.AddWithValue("itdesp4", tbD.Rows[i]["ItDesp4"]);
            cmd.Parameters.AddWithValue("itdesp5", tbD.Rows[i]["ItDesp5"]);
            cmd.Parameters.AddWithValue("itdesp6", tbD.Rows[i]["ItDesp6"]);
            cmd.Parameters.AddWithValue("itdesp7", tbD.Rows[i]["ItDesp7"]);
            cmd.Parameters.AddWithValue("itdesp8", tbD.Rows[i]["ItDesp8"]);
            cmd.Parameters.AddWithValue("itdesp9", tbD.Rows[i]["ItDesp9"]);
            cmd.Parameters.AddWithValue("itdesp10", tbD.Rows[i]["ItDesp10"]);
            cmd.Parameters.AddWithValue("orid", tbD.Rows[i]["bomid"].ToString());
            cmd.Parameters.AddWithValue("orno", tbD.Rows[i]["orno"].ToString());
            cmd.Parameters.AddWithValue("sadate", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("sadateac", Date.ToTWDate(SaDate.Text));
            cmd.Parameters.AddWithValue("mwidth1", 0);
            cmd.Parameters.AddWithValue("mwidth2", 0);
            cmd.Parameters.AddWithValue("mwidth3", 0);
            cmd.Parameters.AddWithValue("mwidth4", 0);
            cmd.Parameters.AddWithValue("pqty", tbD.Rows[i]["qtynotout"].ToDecimal());
            cmd.Parameters.AddWithValue("punit", tbD.Rows[i]["itunit"].ToString());
            cmd.Parameters.AddWithValue("pformula", "");
            cmd.Parameters.AddWithValue("leno", "");
            cmd.Parameters.AddWithValue("leid", "");
            cmd.Parameters.AddWithValue("NetNo", row["NetNo"].ToString().Trim());
            cmd.Parameters.AddWithValue("standard", tbD.Rows[i]["standard"]);

            cmd.Parameters.AddWithValue("cyno", "");

            cmd.Parameters.AddWithValue("Adper1", tbD.Rows[i]["Adper1"].ToString().Trim().GetUTF8(10));//指送負責人
            cmd.Parameters.AddWithValue("Adtel", tbD.Rows[i]["Adtel"].ToString().Trim().GetUTF8(20));//指送電話
            cmd.Parameters.AddWithValue("AdAddr", tbD.Rows[i]["AdAddr"].ToString().Trim().GetUTF8(60));//指送地址
            cmd.Parameters.AddWithValue("AdName", tbD.Rows[i]["AdName"].ToString().Trim().GetUTF8(50));

            cmd.CommandText = @"
            INSERT INTO Saled (
            sano,sadate1,sadate2,sadateac1,sadateac2,quno,cuno,emno,spno
            ,stno,xa1no,xa1par,seno,sename,x4no,x4name,itno,itname
            ,ittrait,itunit,itpkgqty,qty,price,prs,rate,taxprice,mny,priceb
            ,taxpriceb,mnyb,memo,bomid,bomrec,recordno,bracket
            ,itdesp1,itdesp2,itdesp3,itdesp4,itdesp5
            ,itdesp6,itdesp7,itdesp8,itdesp9,itdesp10
            ,stName,orid,orno,sadate,sadateac,mwidth1,mwidth2,mwidth3,mwidth4
            ,pqty,punit,pformula,leno,leid,cyno,Adper1,Adtel,AdAddr,AdName,NetNo,standard
            )  VALUES (
            @sano,@sadate1,@sadate2,@sadateac1,@sadateac2,@quno,@cuno,@emno,@spno
            ,@stno,@xa1no,@xa1par,@seno,@sename,@x4no,@x4name,@itno,@itname
            ,@ittrait,@itunit,@itpkgqty,@qty,@price,@prs,@rate,@taxprice,@mny,@priceb
            ,@taxpriceb,@mnyb,@memo,@bomid,@bomrec,@recordno,@bracket
            ,@itdesp1,@itdesp2,@itdesp3,@itdesp4,@itdesp5
            ,@itdesp6,@itdesp7,@itdesp8,@itdesp9,@itdesp10
            ,@stName,@orid,@orno,@sadate,@sadateac,@mwidth1,@mwidth2,@mwidth3,@mwidth4,@pqty,@punit
            ,@pformula,@leno,@leid,@cyno,@Adper1,@Adtel,@AdAddr,@AdName,@NetNo,@standard) ";
            cmd.ExecuteNonQuery();

            if (row["NetNo"].ToString().Length > 0)
            {
                cmd.CommandText = @"update weborder SET orderState='2' WHERE orno = @NetNo";
                cmd.ExecuteNonQuery();
            }

        }
        private void AppendBomOnSaving(SqlCommand cmd)
        {
            for (int i = 0; i < tbBom.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("SaNo", SaNo.Text.Trim());
                cmd.Parameters.AddWithValue("BomID", SaNo.Text + tbBom.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("BomRec", tbBom.Rows[i]["BomRec"]);
                cmd.Parameters.AddWithValue("itno", tbBom.Rows[i]["itno"]);
                cmd.Parameters.AddWithValue("itname", tbBom.Rows[i]["itname"]);
                cmd.Parameters.AddWithValue("itunit", tbBom.Rows[i]["itunit"]);
                cmd.Parameters.AddWithValue("itqty", tbBom.Rows[i]["itqty"]);
                cmd.Parameters.AddWithValue("itpareprs", tbBom.Rows[i]["itpareprs"]);
                cmd.Parameters.AddWithValue("itpkgqty", tbBom.Rows[i]["itpkgqty"]);
                cmd.Parameters.AddWithValue("itrec", tbBom.Rows[i]["itrec"]);
                cmd.Parameters.AddWithValue("itprice", tbBom.Rows[i]["itprice"]);
                cmd.Parameters.AddWithValue("itprs", tbBom.Rows[i]["itprs"]);
                cmd.Parameters.AddWithValue("itmny", tbBom.Rows[i]["itmny"]);
                cmd.Parameters.AddWithValue("itnote", tbBom.Rows[i]["itnote"]);

                cmd.CommandText = @"
                    INSERT INTO SaleBom
                    (SaNo,BomID,BomRec,itno,itname,itunit,itqty,itpareprs,itpkgqty,itrec,itprice,itprs,itmny,itnote) VALUES 
                    (@SaNo,@BomID,@BomRec,@itno,@itname,@itunit,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote)";
                cmd.ExecuteNonQuery();
            }
        }
        private void PassToReceivOnSaving(SqlCommand cmd)
        {
            var row = tb.Rows[0];
            //儲存時檢查『應收總計』與『未收金額』是否相等
            //若相等時，刪除沖款與沖款明細
            //若不等時，沖款
            decimal totmny = 0;//應收總額
            decimal acctmny = 0;//未收總額
            decimal.TryParse(row["TotMny"].ToString().Trim(), out totmny);
            decimal.TryParse(row["TotMny"].ToString().Trim(), out acctmny);
            decimal collectmny = 0;//已收金額
            decimal getprvacc = 0;//取用預收

            //沖款總額
            decimal _Total = 0;
            _Total = collectmny + getprvacc;//已收金額 + 取用預收
            //本幣總額
            decimal xa1par = 0;
            decimal _TotalB = 0;
            decimal.TryParse(row["Xa1Par"].ToString().Trim(), out xa1par);
            _TotalB = _Total * xa1par;//沖款總額 * 匯率

            string reno = "";

            //刪除沖款
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
            cmd.CommandText = "select reno from receivd where ExtFlag =N'銷貨' and SaNo =@sano COLLATE Chinese_Taiwan_Stroke_BIN";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                    if (reader.Read())
                        reno = reader["reno"].ToString();
            }
            cmd.Parameters.AddWithValue("reno", reno);
            cmd.CommandText = "delete from receiv where ExtFlag =N'銷貨' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();
            //
            cmd.CommandText = "delete from receivd where ExtFlag =N'銷貨' and reno =@reno COLLATE Chinese_Taiwan_Stroke_BIN";
            cmd.ExecuteNonQuery();
           
        }





        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        string ResultSTR(object ob)
        {
            if (ob == null) return "";
            return ob.ToString().Trim();
        }





    }
}
