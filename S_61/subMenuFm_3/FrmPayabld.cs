using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmPayabld : Formbase
    {
        JBS.JS.xEvents xe;
        List<TextBoxbase> list;
        string FaNoTEMP = "";
        string PaDateAcsTemp = "";
        string PaDateAceTemp = "";

        Boolean AppendCheck = false;
        SqlTransaction tran;
        List<DataRow> li = new List<DataRow>();
        DataTable dt = new DataTable();
        DataTable dtPayabld = new DataTable();
        DataTable dtItem = new DataTable();
        DataRow dr;
        string tempNo = "";
        int temp = 0;

        DataTable beginning = new DataTable();//期初
        DataTable BShop = new DataTable();//進貨
        DataTable RShop = new DataTable();//進貨

        List<Control> TXTs;
        List<Control> TXTReadOnly;
        List<Control> Writes;
        public string Browtemp = "";


        DataTable CHKuser = new DataTable();
        public DataTable CHKo = new DataTable();
        public DataTable RemitO = new DataTable();
        DataTable temptb = new DataTable();
        bool 票據錯誤 = false;
        string 票據錯誤文字 = "";
        string 票據連線字串 = "";
        DataRow chksys;
        DataRow scrit;
        DataTable IsModify = new DataTable();//儲存時比對用是否有修改中得單據

        public FrmPayabld()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            pVar.FrmPayabld = this;
            this.list = this.getEnumMember();

            #region 票據部分
            lblFee.Visible = FeeMny.Visible = 支票按鈕.Visible = 匯入按鈕.Visible = Common.pathC.Trim() == "" ? false : true;

            支票按鈕.Enabled = 匯入按鈕.Enabled = false;
            if (Common.pathC != "")
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString.Replace("Initial Catalog=" + Common.logOnInfo.ConnectionInfo.DatabaseName, "Initial Catalog=" + Common.pathC.Trim())))
                    {
                        cn.Open();
                        SqlDataAdapter dd = new SqlDataAdapter("select * from scrit where scname=N'" + Common.User_Name + "'", cn);
                        dd.Fill(CHKuser);
                        if (CHKuser.Rows.Count == 0)
                        {
                            票據錯誤 = true;
                            票據錯誤文字 = "查無票據系統內使用者名稱『" + Common.User_Name + "』，請檢查";
                        }
                        else if (Common.CoNo == "")
                        {
                            票據錯誤 = true;
                            票據錯誤文字 = "進銷存代碼不可為空，請檢查\n於 系統維護->系統參數設定->票據參數";
                        }
                        else
                        {
                            scrit = CHKuser.Rows[0];
                            dd = new SqlDataAdapter("select * from chko", cn);
                            dd.Fill(CHKo);
                            CHKo.Clear();//取應收票據結構
                            dd = new SqlDataAdapter("select * from remito", cn);
                            dd.Fill(RemitO);
                            RemitO.Clear();//取匯入結構
                            dd = new SqlDataAdapter("select * from chksys", cn);
                            dd.Fill(temptb);//取票據系統參數
                            chksys = temptb.Rows[0];
                            票據連線字串 = Common.sqlConnString.Replace("Initial Catalog=" + Common.logOnInfo.ConnectionInfo.DatabaseName, "Initial Catalog=" + Common.pathC.Trim());
                        }
                    }
                }
                catch (Exception)
                {
                    票據錯誤 = true;
                    票據錯誤文字 = "票據資料庫名稱錯誤，請檢查\n於 系統維護->系統參數設定->票據參數";
                }
            }
            #endregion

            TXTs = new List<Control> { PaNo, PaDate, PaDateAcs, Ticket, CheckMny, RemitMny, OtherMny, PaDateAce, FaNo, CashMny, CardMny, CardNo, GetPrvAcc, AddPrvAcc, ActSlt, SpNo, EmNo, DeNo, Xa1Par, FeeMny };
            TXTReadOnly = new List<Control> { FaPayable, FaName1, FaPayAmt, TotMny };
            Writes = new List<Control> { PaNo, PaDate, PaDateAcs, Ticket, CheckMny, RemitMny, OtherMny, PaDateAce, FaNo, FaName1, FaPayable, CashMny, CardMny, CardNo, FaPayAmt, GetPrvAcc, AddPrvAcc, TotMny, ActSlt, SpNo, EmName, EmNo, DeNo, DeName, Xa1Par, FeeMny };


            this.單據總計.Set進貨單據小數();
            this.未付金額.Set進貨單據小數();
            this.折讓金額.Set進貨單據小數();
            this.沖帳金額.Set進貨單據小數();

            this.匯率.FirstNum = 12;
            this.匯率.LastNum = 4;
            this.沖款匯率.FirstNum = 12;
            this.沖款匯率.LastNum = 4;

            this.本幣金額.Set本幣金額小數();
            this.匯差金額.Set本幣金額小數();

            this.單據總計.DefaultCellStyle.Format = "f" + Common.MFT;
            this.未付金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.折讓金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.沖帳金額.DefaultCellStyle.Format = "f" + Common.MFT;
            this.匯率.DefaultCellStyle.Format = "f4";
            this.沖款匯率.DefaultCellStyle.Format = "f4";
            this.本幣金額.DefaultCellStyle.Format = "f" + Common.M;
            this.匯差金額.DefaultCellStyle.Format = "f" + Common.M;

            foreach (Control a in list)
            {
                if (a is JE.MyControl.TextBoxNumberT)
                {
                    (a as JE.MyControl.TextBoxNumberT).FirstNum = Common.nFirst;
                    (a as JE.MyControl.TextBoxNumberT).LastNum = Common.MFT;
                }
            }

            TotExgDiff.FirstNum = Common.nFirst;
            TotExgDiff.LastNum = Common.M;
            Xa1Par.FirstNum = 4;
            Xa1Par.LastNum = 4;

            TotDisc.Text = (0M).ToString("f" + Common.MST);
            TotReve.Text = (0M).ToString("f" + Common.MST);
            TotMny1.Text = (0M).ToString("f" + Common.MST);
            TotExgDiff.Text = (0M).ToString("f" + Common.M);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radio1.Enabled = false;
            radio2.Enabled = false;
            radio1.Checked = true;

            Writes.ForEach(r => { (r as TextBox).ReadOnly = true; });
            單據明細.Enabled = true;

            PaDate.SetDateLength();
            PaDateAcs.SetDateLength();
            PaDateAce.SetDateLength();

            //載入資料庫，預設顯示第一筆
            loadDB();
            if (dt.Rows.Count > 0)
            {
                dr = li.Last();
                writeToTxt(dr);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private DataRow getCurrentDataRow()
        {
            return li.Find(r => r.Field<string>("PaNo") == (PaNo.Text.Trim()));
        }

        private DataRow getCurrentDataRow(string s)
        {
            return li.Find(r => r.Field<string>("PaNo") == (s));
        }

        public void loadDB()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();
                string str = @"
                select Payabl.Xa1par,Payabl.cono,Payabl.accono,Payabl.acno,Payabl.deno,Payabl.dename,Payabl.emname,Payabl.emno,SpNo,PaNo,Ticket,CheckMny,RemitMny,OtherMny,PaDate,PaDate1,PaDateAcs,PaDateAcs1,PaDateAce,PaDateAce1,Payabl.FaNo,Payabl.FaName1,TotMny1,CashMny,CardMny,CardNo,FaPayAmt,GetPrvAcc,AddPrvAcc,TotMny,ActSlt,memo2,TotDisc,TotReve,TotMny1,TotExgDiff,Fact.FaPayable,Payabl.ACCONO,Payabl.FeeMny 
                from Payabl 
                left join Fact on Fact.FaNo=Payabl.FaNo
                order by payabl.pano";
                SqlDataAdapter da = new SqlDataAdapter(str, conn);
                dt.Clear();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    li.Clear();
                    li = dt.AsEnumerable().ToList();
                }
                else
                {
                    li.Clear();
                }
                da.Dispose();
                conn.Close(); conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void writeToTxt(DataRow row)
        {
            if (row != null)
            {
                decimal d = 0;
                Writes.ForEach(r =>
                {
                    try
                    {
                        if (row[r.Name].ToString() != null && row[r.Name].ToString() != "")
                            r.Text = row[r.Name].ToString();
                        else
                            r.Text = "";

                        if (r.Name == "PaDate" || r.Name == "PaDateAcs" || r.Name == "PaDateAce")
                        {
                            switch (Common.User_DateTime)
                            {
                                case 1:
                                    if (row[r.Name].ToString() != null && row[r.Name].ToString() != "")
                                        r.Text = row[r.Name].ToString();
                                    else
                                        r.Text = "";
                                    break;
                                case 2:
                                    if (row[r.Name + "1"].ToString() != null && row[r.Name + "1"].ToString() != "")
                                        r.Text = row[r.Name + "1"].ToString();
                                    else
                                        r.Text = "";
                                    break;
                            }
                        }

                        if (r is TextBoxNumberT)
                        {
                            d = r.Text.ToDecimal(); 
                            if (r.Name == "FaPayable") d = 0;
                            r.Text = string.Format("{0:F" + (r as TextBoxNumberT).LastNum + "}", d);
                        }
                    }
                    catch { }
                });
                TotDisc.Text = row["TotDisc"].ToDecimal().ToString("f" + Common.MFT);
                TotReve.Text = row["TotReve"].ToDecimal().ToString("f" + Common.MFT);
                TotMny1.Text = row["TotMny1"].ToDecimal().ToString("f" + Common.MFT);
                TotExgDiff.Text = row["TotExgDiff"].ToDecimal().ToString("f" + Common.M);
                Memo2.Text = row["Memo2"].ToString();

                if (ActSlt.Text == "1") radio1.Checked = true;
                if (ActSlt.Text == "2") radio2.Checked = true;

                if (PaNo.Text.Trim() != "")
                {
                    loadDbPayabld();
                }
            }
            else
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                dtPayabld.Clear();
            }
        }

        public void loadDbPayabld()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();
                string str = "";

                switch (Common.User_DateTime)
                {
                    case 1:
                        str = "select RecordNo as 序號,BsDateAc as 帳款日期,Bracket as 單據,BsNo as 單據號碼,TotMny as 單據總計,AcctMny as 未付金額,Discount as 折讓金額,Reverse as 沖帳金額,InvNo as 發票號碼,Memo1 as 說明,Xa1Name as 幣別,Xa1Par as 匯率,Xa1Par1 as 沖款匯率,ReverseB as 本幣金額,ExgStat as 匯兌狀況,ExgDiff as 匯差金額,ExtFlag as 是否為外部轉入單 from Payabld where PaNo=@PaNo";
                        break;
                    case 2:
                        str = "select RecordNo as 序號,BsDateAc1 as 帳款日期,Bracket as 單據,BsNo as 單據號碼,TotMny as 單據總計,AcctMny as 未付金額,Discount as 折讓金額,Reverse as 沖帳金額,InvNo as 發票號碼,Memo1 as 說明,Xa1Name as 幣別,Xa1Par as 匯率,Xa1Par1 as 沖款匯率,ReverseB as 本幣金額,ExgStat as 匯兌狀況,ExgDiff as 匯差金額,ExtFlag as 是否為外部轉入單 from Payabld where PaNo=@PaNo";
                        break;
                }
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("PaNo", PaNo.Text.Trim());
                    cmd.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dtPayabld.Clear();
                    da.Fill(dtPayabld);
                    if (dtPayabld.Rows.Count > 0)
                    {
                        dataGridViewT1.DataSource = dtPayabld;
                        CashMny_TextChanged(null, null);
                    }
                    else
                    {
                        dataGridViewT1.DataSource = null;
                    }
                    da.Dispose();
                }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CashMny_TextChanged(object sender, EventArgs e)
        {
            if (!AppendCheck) return;
            if (sender is TextBox)
                if ((sender as TextBox).Text == "-" || (sender as TextBox).Text == ".") return;

            decimal d = 0, total = 0;
            decimal.TryParse(CashMny.Text, out d); total += d;
            decimal.TryParse(CardMny.Text, out d); total += d;
            decimal.TryParse(Ticket.Text, out d); total += d;
            decimal.TryParse(CheckMny.Text, out d); total += d;
            decimal.TryParse(RemitMny.Text, out d); total += d;
            decimal.TryParse(OtherMny.Text, out d); total += d;
            decimal.TryParse(GetPrvAcc.Text, out d); total += d;
            decimal.TryParse(AddPrvAcc.Text, out d); total -= d;
            TotMny.Text = total.ToString();
            TotMny1.Text = TotMny.Text;


            if (dataGridViewT1.RowCount > 0)
            {
                TotDisc.Text = "0";
                TotReve.Text = "0";
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    TotDisc.Text = (Convert.ToDecimal(TotDisc.Text) + Convert.ToDecimal(dataGridViewT1["折讓金額", i].EditedFormattedValue.ToString())).ToString();
                    TotReve.Text = (Convert.ToDecimal(TotReve.Text) + Convert.ToDecimal(dataGridViewT1["沖帳金額", i].EditedFormattedValue.ToString())).ToString();
                }
                TotMny1.Text = (Convert.ToDecimal(TotMny1.Text) - Convert.ToDecimal(TotReve.Text)).ToString();

                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["本幣金額", i].Value = (
                        Convert.ToDecimal(dataGridViewT1["沖款匯率", i].EditedFormattedValue.ToString())
                        * Convert.ToDecimal(dataGridViewT1["沖帳金額", i].EditedFormattedValue.ToString())).ToString();
                    dataGridViewT1["匯差金額", i].Value = (
                        (Convert.ToDecimal(dataGridViewT1["沖款匯率", i].Value.ToString()) - Convert.ToDecimal(dataGridViewT1["匯率", i].Value.ToString()))
                        * Convert.ToDecimal(dataGridViewT1["沖帳金額", i].EditedFormattedValue.ToString())).ToString();
                    if (Convert.ToDecimal(dataGridViewT1["匯差金額", i].Value.ToString()) >= 0)
                        dataGridViewT1["匯兌狀況", i].Value = "匯兌收益";
                    else
                        dataGridViewT1["匯兌狀況", i].Value = "匯兌損失";

                    dataGridViewT1["本幣金額", i].Value = string.Format("{0:" + "F" + Common.M + "}", Convert.ToDecimal(dataGridViewT1["本幣金額", i].Value.ToString()));
                    dataGridViewT1["匯差金額", i].Value = string.Format("{0:" + "F" + Common.M + "}", Convert.ToDecimal(dataGridViewT1["匯差金額", i].Value.ToString()));
                    dataGridViewT1["匯率", i].Value = string.Format("{0:" + "F" + "4" + "}", Convert.ToDecimal(dataGridViewT1["匯率", i].Value.ToString()));
                }

                TotExgDiff.Text = "0";
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    TotExgDiff.Text = (Convert.ToDecimal(TotExgDiff.Text) + Convert.ToDecimal(dataGridViewT1["匯差金額", i].Value.ToString())).ToString();
                }
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            loadDB();
            if (li.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
            btnTop.Enabled = false;
            btnPrior.Enabled = false;
            btnNext.Enabled = true;
            btnBottom.Enabled = true;
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            loadDB();
            tempNo = PaNo.Text.Trim();
            dr = getCurrentDataRow();
            temp = li.IndexOf(dr);
            if (li.Count > 0)
            {
                dr = getCurrentDataRow(tempNo);
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    if (temp == 0)
                    {
                        dr = li.First();
                        writeToTxt(dr);
                        MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnTop.Enabled = false;
                        btnPrior.Enabled = false;
                        btnNext.Enabled = true;
                        btnBottom.Enabled = true;
                        return;
                    }
                    else
                    {
                        dr = li[--temp];
                        writeToTxt(dr);
                        btnNext.Enabled = true;
                        btnBottom.Enabled = true;
                        return;
                    }
                }
                if (i > 0)
                {
                    dr = li[--i];
                    writeToTxt(dr);
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                }
                else
                {
                    MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnTop.Enabled = false;
                    btnPrior.Enabled = false;
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            loadDB();
            tempNo = PaNo.Text.Trim();
            dr = getCurrentDataRow();
            temp = li.IndexOf(dr);
            if (li.Count > 0)
            {
                dr = getCurrentDataRow(tempNo);
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    if (temp >= li.Count)
                    {
                        dr = li.Last();
                        writeToTxt(dr);
                        MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnTop.Enabled = true;
                        btnPrior.Enabled = true;
                        btnNext.Enabled = false;
                        btnBottom.Enabled = false;
                        return;
                    }
                    else
                    {
                        dr = li[++i];
                        writeToTxt(dr);
                        btnTop.Enabled = true;
                        btnPrior.Enabled = true;
                        return;
                    }
                }
                if (i < li.Count - 1)
                {
                    dr = li[++i];
                    writeToTxt(dr);
                    btnTop.Enabled = true;
                    btnPrior.Enabled = true;
                }
                else
                {
                    MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnTop.Enabled = true;
                    btnPrior.Enabled = true;
                    btnNext.Enabled = false;
                    btnBottom.Enabled = false;
                }
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            loadDB();
            if (li.Count > 0)
            {
                dr = li.Last();
                writeToTxt(dr);
            }
            btnTop.Enabled = true;
            btnPrior.Enabled = true;
            btnNext.Enabled = false;
            btnBottom.Enabled = false;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            tempNo = PaNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            AppendCheck = true;
            dtPayabld.Clear();
            dataGridViewT1.DataSource = null;

            #region 票據部分
            FeeMny.ReadOnly = CheckMny.ReadOnly = RemitMny.ReadOnly = Common.pathC == "" ? false : true;
            支票按鈕.Enabled = 匯入按鈕.Enabled = true;
            #endregion

            PaNo.ReadOnly = false;
            PaDate.Focus();
            PaDate.Text = Date.GetDateTime(Common.User_DateTime);

            //數值欄位填上0
            Writes.ForEach(r =>
            {
                try
                {
                    if (r is TextBoxNumberT)
                    {
                        if (r.Text == "")
                            r.Text = string.Format("{0:F" + (r as TextBoxNumberT).LastNum + "}", 0);
                    }
                }
                catch { }
            });
            EmName.Text = EmNo.Text = DeNo.Text = DeName.Text = "";

            Xa1Par.Text = "1.0000";

            radio1.Enabled = true;
            radio2.Enabled = true;
            radio1.Checked = true;
            Memo2.ReadOnly = false;

            dataGridViewT1.ReadOnly = false;
            dataGridViewT1.Columns["序號"].ReadOnly = true;
            dataGridViewT1.Columns["帳款日期"].ReadOnly = true;
            dataGridViewT1.Columns["單據"].ReadOnly = true;
            dataGridViewT1.Columns["單據號碼"].ReadOnly = true;
            dataGridViewT1.Columns["單據總計"].ReadOnly = true;
            dataGridViewT1.Columns["未付金額"].ReadOnly = true;
            dataGridViewT1.Columns["幣別"].ReadOnly = true;
            dataGridViewT1.Columns["匯率"].ReadOnly = true;
            dataGridViewT1.Columns["本幣金額"].ReadOnly = true;
            dataGridViewT1.Columns["匯兌狀況"].ReadOnly = true;
            dataGridViewT1.Columns["匯差金額"].ReadOnly = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (PaNo.Text == string.Empty) return;//BoItNo沒有值，就不執行下面的指令
            DataRow ac = Common.load("Check", "payabl", "PaNo", PaNo.Text.Trim());

            if (ac != null && ac["acno"].ToString().Trim() != "")
            {
                MessageBox.Show("此單據已傳輸至會計傳票，無法異動！\n公司編號:" + ac["accono"].ToString().Trim() + " 傳票編號:" + ac["acno"].ToString().Trim(), "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                cmd.Parameters.AddWithValue("pano", PaNo.Text.Trim());

                cmd.CommandText = "select ExtFlag from Payabl where PaNo =@pano COLLATE Chinese_Taiwan_Stroke_BIN";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (reader["ExtFlag"].ToString() == "進貨" || reader["ExtFlag"].ToString() == "退貨")
                        {
                            MessageBox.Show("此沖款單為" + reader["ExtFlag"].ToString() + "單轉入，無法刪除。", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    }
                }
                cmd.Dispose();
                conn.Close(); conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadDBError:\n" + ex.ToString());
            }

            {
                //執行刪除指令前，檢查此筆資料是否已被別人刪除
                loadDB();
                dr = getCurrentDataRow();
                if (li.IndexOf(dr) == -1)
                {
                    if (li.Count > 0)
                    {
                        dr = li.Last();
                        writeToTxt(dr);
                    }
                    else
                    {
                        dr = null;
                        writeToTxt(dr);
                    }
                    return;//資料已被刪除，以下程式碼不執行
                }
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = Common.sqlConnString;
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    tran = conn.BeginTransaction();
                    cmd.Transaction = tran;
                    for (int i = 0; i < dataGridViewT1.RowCount; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("pano", PaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("i", (i + 1).ToString());
                        cmd.CommandText = "delete from Payabld where PaNo=@pano and recordno=@i";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("discount", dataGridViewT1["折讓金額", i].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("mny", dataGridViewT1["沖帳金額", i].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("bsno", dataGridViewT1["單據號碼", i].Value.ToString());
                        cmd.Parameters.AddWithValue("invno", dataGridViewT1["發票號碼", i].Value.ToString());

                        //現有應付帳款餘額=應付餘額+折讓金額+沖款金額
                        cmd.CommandText = "update Fact set Fact.FaPayable=isnull(Fact.FaPayable,0)+(@discount)+(@mny) where Fact.FaNo=@fano";
                        cmd.ExecuteNonQuery();

                        if (dataGridViewT1["單據", i].Value.ToString() == "期初")
                        {
                            //--(期初帳款餘額=期初帳款餘額)+折讓金額+沖款金額
                            cmd.CommandText = "update Fact set Fact.FaSparePay=isnull(Fact.FaSparePay,0)+@discount+@mny" +
                            " where Fact.FaNo=@fano";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "Update Payabld Set Payabld.acctmny=(select FaSparePay from Fact where FaNo=@fano)" +
                            " where Payabld.FaNo=@fano  and bracket = '期初'";
                            cmd.ExecuteNonQuery();
                        }
                        else if (dataGridViewT1["單據", i].Value.ToString() == "進貨")
                        {
                            cmd.CommandText = "Update BShop Set BShop.AcctMny=BShop.AcctMny+@mny+@discount" +
                            ",BShop.CollectMny=IsNull(BShop.CollectMny,0)-@mny" +
                            ",BShop.Discount=BShop.Discount-@discount" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "Update Payabld Set Payabld.acctmny=(select acctmny from BShop where BsNo=@bsno)" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();
                        }
                        else if (dataGridViewT1["單據", i].Value.ToString() == "退貨")
                        {
                            cmd.CommandText = "Update RShop Set RShop.AcctMny=RShop.AcctMny-(@mny)-(@discount)" +
                            ",RShop.CollectMny=IsNull(RShop.CollectMny,0)+(@mny)" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Update Payabld Set Payabld.acctmny=(-1)*(select acctmny from RShop where BsNo=@bsno)" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Update RShop Set RShop.InvNo=@invno" +
                            " ,RShop.Discount=" + "(-1)*(select ISNULL(sum(Discount),0) from Payabld where BsNo=@bsno and Bracket='退貨')" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (cmd.Parameters.IndexOf("pano") == -1) cmd.Parameters.AddWithValue("pano", PaNo.Text.Trim());
                    cmd.CommandText = "delete from Payabl where PaNo=@pano";
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters.IndexOf("fano") == -1) cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("famny", (GetPrvAcc.Text.ToDecimal() - AddPrvAcc.Text.ToDecimal()));
                    cmd.CommandText = "update Fact set Fact.FaPayAmt=Fact.FaPayAmt+@famny where Fact.FaNo=@fano";
                    cmd.ExecuteNonQuery();
                    if (!票據系統刪除())//票據部分
                    {
                        tran.Rollback();
                    }
                    else
                    {
                        tran.Commit();
                    }
                    tran.Dispose();
                    cmd.Dispose();
                    conn.Close(); conn.Dispose();

                    //刪除完成重載DB
                    //如果list還有值,秀出最後一筆
                    //如果沒有值,清空當前欄位
                    //載入資料庫，預設顯示第一筆
                    loadDB();
                    if (li.Count > 0)
                    {
                        dr = li.Last();
                        writeToTxt(dr);
                    }
                    else
                    {
                        dr = null;
                        writeToTxt(dr);
                        dataGridViewT1.DataSource = null;
                        dataGridViewT1.RowCount = 0;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("DelError:\n" + ex.ToString());
                    tran.Rollback();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (xe.IsRegisted("payabl") == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！\n";
                msg += "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Boolean SaveCheck = false;
            //進銷存年度，日期的西元民國轉換
            string PaDateAD = "", PaDateTW = "";//PaDate西元和民國
            string PaDateYearAD = "", PaDateYearTW = "";//PaDate西元和民國的年度
            string PaDateAcsAD = "", PaDateAcsTW = "";
            string PaDateAceAD = "", PaDateAceTW = "";

            switch (Common.User_DateTime)
            {
                case 1:
                    PaDateTW = PaDate.Text;
                    PaDateAD = (Convert.ToInt32(PaDate.Text.Substring(0, 3)) + 1911) + PaDate.Text.Substring(3, 4);
                    PaDateYearTW = PaDate.Text.Substring(0, 3);
                    PaDateYearAD = (Convert.ToInt32(PaDate.Text.Substring(0, 3)) + 1911).ToString();
                    PaDateAcsTW = PaDateAcs.Text;
                    if (PaDateAcs.Text != "")
                        PaDateAcsAD = (Convert.ToInt32(PaDateAcs.Text.Substring(0, 3)) + 1911) + PaDateAcs.Text.Substring(3, 4);
                    PaDateAceTW = PaDateAce.Text;
                    if (PaDateAce.Text != "")
                        PaDateAceAD = (Convert.ToInt32(PaDateAce.Text.Substring(0, 3)) + 1911) + PaDateAce.Text.Substring(3, 4);
                    break;
                case 2:
                    PaDateAD = PaDate.Text;
                    PaDateTW = (Convert.ToInt32(PaDate.Text.Substring(0, 4)) - 1911) + PaDate.Text.Substring(4, 4);
                    PaDateYearAD = PaDate.Text.Substring(0, 4);
                    PaDateYearTW = (Convert.ToInt32(PaDate.Text.Substring(0, 4)) - 1911).ToString();
                    PaDateAcsAD = PaDateAcs.Text;
                    if (PaDateAcs.Text != "")
                        PaDateAcsTW = (Convert.ToInt32(PaDateAcs.Text.Substring(0, 4)) - 1911) + PaDateAcs.Text.Substring(4, 4);
                    PaDateAceTW = PaDateAce.Text;
                    if (PaDateAce.Text != "")
                        PaDateAceTW = (Convert.ToInt32(PaDateAce.Text.Substring(0, 4)) - 1911) + PaDateAce.Text.Substring(4, 4);
                    break;
            }

            if ((Convert.ToInt32(PaDateYearAD) < Common.Sys_StkYear2) || (Convert.ToInt32(PaDateYearAD) > (Common.Sys_StkYear2 + 2)))
            {
                MessageBox.Show("單據日期不能超過進銷存年度兩年或小於進銷存年度，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PaDateAcs.Focus();
                return;
            }
            if (FaNo.Text.Trim() == "")
            {
                MessageBox.Show("廠商編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                return;
            }
            if (PaDateAcsAD != "" && PaDateAceAD != "")
                if (Convert.ToDecimal(PaDateAcsAD) > Convert.ToDecimal(PaDateAceAD))
                {
                    MessageBox.Show("帳款起始日期不能大於帳款終止日期，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PaDateAcs.Focus();
                    return;
                }
            if (Convert.ToDecimal(TotMny1.Text.Trim()) != 0)
            {
                MessageBox.Show("沖帳金額不符，請輸入後再儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (TotReve.Text.Trim().ToDecimal() == 0 && TotDisc.Text.Trim().ToDecimal() == 0)
            {
                bool havevalue = false;
                foreach (DataGridViewRow dr in dataGridViewT1.Rows)
                {
                    if (dr.Cells["沖帳金額"].EditedFormattedValue.ToDecimal() != 0) havevalue = true;
                }
                foreach (DataGridViewRow dr in dataGridViewT1.Rows)
                {
                    if (dr.Cells["折讓金額"].EditedFormattedValue.ToDecimal() != 0) havevalue = true;
                }
                if (GetPrvAcc.Text.Trim().ToDecimal() != 0 || AddPrvAcc.Text.Trim().ToDecimal() != 0) havevalue = true;
                if (!havevalue)
                {
                    MessageBox.Show("尚未輸入沖款資料，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //沖款若有人使用單據
            #region 沖款VS修改狀態

            IsModify.Clear();
            string str = "select * from bshop where IsModify = 1 and fano=@fano";
            if (PaDateAcs.Text.Trim() != "") str += " and bsDateAc>=@date";
            if (PaDateAce.Text.Trim() != "") str += " and bsDateAc<=@date1";
            if (SpNo.Text.Trim() != "") str += " and spno =@spno";

            parameters p = new parameters();
            p.AddWithValue("fano", FaNo.Text.Trim());
            p.AddWithValue("date", PaDateAcs.Text.Trim());
            p.AddWithValue("date1", PaDateAce.Text.Trim());
            p.AddWithValue("spno", SpNo.Text.Trim());


            SQL.ExecuteNonQuery(str, p, IsModify);
            foreach (DataRow dr in IsModify.Rows)
            {
                foreach (DataGridViewRow dTr in dataGridViewT1.Rows)
                {
                    if (dTr.Cells["單據號碼"].EditedFormattedValue.ToString() == dr["bsno"].ToString() && (dTr.Cells["沖帳金額"].EditedFormattedValue.ToDecimal() > 0 || dTr.Cells["折讓金額"].EditedFormattedValue.ToDecimal() > 0))
                    {
                        MessageBox.Show("進貨單據有其他使用者正在修改，無法儲存，單據：" + dTr.Cells["單據號碼"].EditedFormattedValue.ToString(), "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            IsModify.Clear();
            str = "select * from rshop where IsModify = 1 and fano=@fano";
            if (PaDateAcs.Text.Trim() != "") str += " and bsDateAc>=@date";
            if (PaDateAce.Text.Trim() != "") str += " and bsDateAc<=@date1";
            if (SpNo.Text.Trim() != "") str += " and spno =@spno";
            SQL.ExecuteNonQuery(str, p, IsModify);
            foreach (DataRow dr in IsModify.Rows)
            {
                foreach (DataGridViewRow dTr in dataGridViewT1.Rows)
                {
                    if (dTr.Cells["單據號碼"].EditedFormattedValue.ToString() == dr["sano"].ToString() && (dTr.Cells["沖帳金額"].EditedFormattedValue.ToDecimal() > 0 || dTr.Cells["折讓金額"].EditedFormattedValue.ToDecimal() > 0))
                    {
                        MessageBox.Show("進退單據有其他使用者正在修改，無法儲存，單據：" + dTr.Cells["單據號碼"].EditedFormattedValue.ToString(), "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            IsModify.Clear();

            #endregion
            //沖款匯率不同無法儲存
            decimal d = 0, totle = 0;
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                totle = i.Cells["折讓金額"].Value.ToDecimal() + i.Cells["沖帳金額"].Value.ToDecimal();
                if (d == 0 && totle != 0) d = i.Cells["沖款匯率"].Value.ToDecimal();
                if (totle != 0 && i.Cells["沖款匯率"].Value.ToDecimal() != d)
                {
                    MessageBox.Show("沖款明細的沖款匯率不同，無法儲存", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Focus();
                    dataGridViewT1.Rows[i.Index].Selected = true;
                    dataGridViewT1.CurrentCell = i.Cells["沖款匯率"];
                    dataGridViewT1.Focus();
                    return;
                }
            }

            if (!SetPaNo()) return;
            PaDateAcs.Clear();
            PaDateAce.Clear();
            PaDateAcsAD = PaDateAcsTW = "";
            PaDateAceAD = PaDateAceTW = "";
            //新增主檔
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                tran = conn.BeginTransaction();
                int count = 0;
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dataGridViewT1["折讓金額", i].Value.ToString()) != 0 || Convert.ToDecimal(dataGridViewT1["沖帳金額", i].Value.ToString()) != 0)
                        ++count;
                }
                cmd.Transaction = tran;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("PaNo", PaNo.Text.Trim());
                cmd.Parameters.AddWithValue("PaDate", PaDateTW);
                cmd.Parameters.AddWithValue("PaDate1", PaDateAD);
                cmd.Parameters.AddWithValue("PaDate2", PaDateAD);
                cmd.Parameters.AddWithValue("PaDateacs", PaDateAcsTW);
                cmd.Parameters.AddWithValue("PaDateacs1", PaDateAcsAD);
                cmd.Parameters.AddWithValue("PaDateacs2", PaDateAcsAD);
                cmd.Parameters.AddWithValue("PaDateace", PaDateAceTW);
                cmd.Parameters.AddWithValue("PaDateace1", PaDateAceAD);
                cmd.Parameters.AddWithValue("PaDateace2", PaDateAceAD);
                cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
                cmd.Parameters.AddWithValue("FaName1", FaName1.Text);
                cmd.Parameters.AddWithValue("cashmny", CashMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("cardmny", CardMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("cardno", CardNo.Text);
                cmd.Parameters.AddWithValue("ticket", Ticket.Text.ToDecimal());
                cmd.Parameters.AddWithValue("checkmny", CheckMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("remitmny", RemitMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("othermny", OtherMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("getprvacc", GetPrvAcc.Text.ToDecimal());
                cmd.Parameters.AddWithValue("addprvacc", AddPrvAcc.Text.ToDecimal());
                cmd.Parameters.AddWithValue("totmny", TotMny.Text.ToDecimal());
                cmd.Parameters.AddWithValue("actslt", ActSlt.Text.ToDecimal());
                cmd.Parameters.AddWithValue("totdisc", TotDisc.Text.ToDecimal());
                cmd.Parameters.AddWithValue("totreve", TotReve.Text.ToDecimal());
                cmd.Parameters.AddWithValue("totmny1", TotMny1.Text.ToDecimal());
                cmd.Parameters.AddWithValue("totexgdiff", TotExgDiff.Text.ToDecimal());
                cmd.Parameters.AddWithValue("Xa1Par", Xa1Par.Text.Trim().ToDecimal("f4"));
                cmd.Parameters.AddWithValue("memo2", Memo2.Text);
                cmd.Parameters.AddWithValue("SpNo", SpNo.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("emno", EmNo.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("emname", EmName.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("deno", DeNo.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("dename", DeName.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("recordno", count);
                cmd.Parameters.AddWithValue("ExtFlag", "沖帳");
                if (CHKo.Rows.Count > 0 || RemitO.Rows.Count > 0)//票據部分
                    cmd.Parameters.AddWithValue("CoNo", "Y");
                else
                    cmd.Parameters.AddWithValue("CoNo", "");
                cmd.Parameters.AddWithValue("FeeMny", FeeMny.Text.ToDecimal());

                cmd.CommandText = "INSERT INTO Payabl "
                + "(PaNo,PaDate,PaDate1,PaDate2"
                + ",PaDateacs,PaDateacs1,PaDateacs2,PaDateace"
                + ",PaDateace1,PaDateace2,FaNo,FaName1"
                + ",cashmny,cardmny,cardno,ticket"
                + ",checkmny,remitmny,othermny,getprvacc"
                + ",addprvacc,totmny,actslt,totdisc"
                + ",totreve,totmny1,totexgdiff,memo2,SpNo,emno,emname,deno,dename,recordno"
                + ",ExtFlag,CoNo,Xa1Par,FeeMny"
                + ") VALUES "
                + "(@PaNo,@PaDate,@PaDate1,@PaDate2"
                + ",@PaDateacs,@PaDateacs1,@PaDateacs2,@PaDateace"
                + ",@PaDateace1,@PaDateace2,@FaNo,@FaName1"
                + ",@cashmny,@cardmny,@cardno,@ticket"
                + ",@checkmny,@remitmny,@othermny,@getprvacc"
                + ",@addprvacc,@totmny,@actslt,@totdisc"
                + ",@totreve,@totmny1,@totexgdiff,@memo2,@SpNo,@emno,@emname,@deno,@dename,@recordno"
                + ",@ExtFlag,@CoNo,@Xa1Par,@FeeMny) ";
                cmd.ExecuteNonQuery();

                //(預付金額=預付金額)-取用預付+累入預付
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("mny", (AddPrvAcc.Text.ToDecimal() - GetPrvAcc.Text.ToDecimal()));
                cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                cmd.CommandText = "update Fact set Fact.FaPayAmt=Fact.FaPayAmt + @mny where Fact.FaNo=@fano";
                cmd.ExecuteNonQuery();

                int RecordNo = 0;
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    Boolean loopcheck = true;
                    if (Convert.ToDecimal(dataGridViewT1["折讓金額", i].Value.ToString()) == 0 && Convert.ToDecimal(dataGridViewT1["沖帳金額", i].Value.ToString()) == 0)
                        loopcheck = false;
                    if (loopcheck)
                    {
                        RecordNo++;
                        string BsDateAcTW = "";
                        string BsDateAcAD = "";
                        if (!dataGridViewT1["帳款日期", i].Value.IsNullOrEmpty())
                        {
                            switch (Common.User_DateTime)
                            {
                                case 1:
                                    if (dataGridViewT1["帳款日期", i].Value.ToString() != "")
                                    {
                                        BsDateAcTW = dataGridViewT1["帳款日期", i].Value.ToString();
                                        BsDateAcAD = (Convert.ToInt32(dataGridViewT1["帳款日期", i].Value.ToString().Substring(0, 3)) + 1911) + dataGridViewT1["帳款日期", i].Value.ToString().Substring(3, 4);
                                    }
                                    break;
                                case 2:
                                    if (dataGridViewT1["帳款日期", i].Value.ToString() != "")
                                    {
                                        BsDateAcAD = dataGridViewT1["帳款日期", i].Value.ToString();
                                        BsDateAcTW = (Convert.ToInt32(dataGridViewT1["帳款日期", i].Value.ToString().Substring(0, 4)) - 1911) + dataGridViewT1["帳款日期", i].Value.ToString().Substring(4, 4);
                                    }
                                    break;
                            }
                        }

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("PaNo", PaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("PaDate", PaDateTW);
                        cmd.Parameters.AddWithValue("PaDate1", PaDateAD);
                        cmd.Parameters.AddWithValue("PaDate2", PaDateAD);
                        cmd.Parameters.AddWithValue("PaDateacs", PaDateAcsTW);
                        cmd.Parameters.AddWithValue("PaDateacs1", PaDateAcsAD);
                        cmd.Parameters.AddWithValue("PaDateacs2", PaDateAcsAD);
                        cmd.Parameters.AddWithValue("PaDateace", PaDateAceTW);
                        cmd.Parameters.AddWithValue("PaDateace1", PaDateAceAD);
                        cmd.Parameters.AddWithValue("PaDateace2", PaDateAceAD);
                        cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("FaName1", FaName1.Text);
                        cmd.Parameters.AddWithValue("RecordNo", RecordNo.ToString());
                        cmd.Parameters.AddWithValue("BsDateAc", BsDateAcTW);
                        cmd.Parameters.AddWithValue("BsDateAc1", BsDateAcAD);
                        cmd.Parameters.AddWithValue("BsDateAc2", BsDateAcAD);
                        cmd.Parameters.AddWithValue("BsNo", dataGridViewT1["單據號碼", i].Value == null ? "" : dataGridViewT1["單據號碼", i].Value.ToString());
                        cmd.Parameters.AddWithValue("Bracket", dataGridViewT1["單據", i].Value == null ? "" : dataGridViewT1["單據", i].Value.ToString());
                        cmd.Parameters.AddWithValue("Xa1Name", dataGridViewT1["幣別", i].Value == null ? "" : dataGridViewT1["幣別", i].Value.ToString());
                        cmd.Parameters.AddWithValue("TotMny", dataGridViewT1["單據總計", i].Value == null ? 0 : dataGridViewT1["單據總計", i].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("AcctMny", dataGridViewT1["未付金額", i].Value == null ? 0 : dataGridViewT1["未付金額", i].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("InvNo", dataGridViewT1["發票號碼", i].Value == null ? "" : dataGridViewT1["發票號碼", i].Value.ToString());
                        cmd.Parameters.AddWithValue("Discount", dataGridViewT1["折讓金額", i].Value == null ? 0 : dataGridViewT1["折讓金額", i].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("Reverse", dataGridViewT1["沖帳金額", i].Value == null ? 0 : dataGridViewT1["沖帳金額", i].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("Xa1Par1", dataGridViewT1["沖款匯率", i].Value == null ? 1 : dataGridViewT1["沖款匯率", i].Value.ToDecimal("f4"));
                        cmd.Parameters.AddWithValue("ReverseB", dataGridViewT1["本幣金額", i].Value == null ? 0 : dataGridViewT1["本幣金額", i].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("ExgStat", dataGridViewT1["匯兌狀況", i].Value == null ? "" : dataGridViewT1["匯兌狀況", i].Value.ToString());
                        cmd.Parameters.AddWithValue("ExgDiff", dataGridViewT1["匯差金額", i].Value == null ? 0 : dataGridViewT1["匯差金額", i].Value.ToDecimal());
                        if (dataGridViewT1["說明", i].Value != null)
                            cmd.Parameters.AddWithValue("Memo1", dataGridViewT1["說明", i].Value == null ? "" : dataGridViewT1["說明", i].Value.ToString());
                        else
                            cmd.Parameters.AddWithValue("Memo1", "");
                        cmd.Parameters.AddWithValue("Xa1Par", dataGridViewT1["匯率", i].Value == null ? 1 : dataGridViewT1["匯率", i].Value.ToDecimal("f4"));
                        cmd.Parameters.AddWithValue("ExtFlag", "沖帳");
                        cmd.Parameters.AddWithValue("emno", dataGridViewT1["業務編號", i].Value == null ? "" : dataGridViewT1["業務編號", i].Value.ToString());
                        cmd.Parameters.AddWithValue("emname", dataGridViewT1["業務名稱", i].Value == null ? "" : dataGridViewT1["業務名稱", i].Value.ToString());
                        cmd.Parameters.AddWithValue("xa1no", dataGridViewT1["幣別編號", i].Value == null ? "" : dataGridViewT1["幣別編號", i].Value.ToString());

                        cmd.CommandText = "INSERT INTO Payabld "
                        + "(PaNo,PaDate,PaDate1,PaDate2"
                        + ",PaDateacs,PaDateacs1,PaDateacs2,PaDateace"
                        + ",PaDateace1,PaDateace2,FaNo,FaName1"
                        + ",RecordNo,BsDateAc,BsDateAc1,BsDateAc2"
                        + ",BsNo,Bracket,Xa1Name,TotMny"
                        + ",AcctMny,InvNo,Discount,Reverse"
                        + ",Xa1Par1,ReverseB,ExgStat,ExgDiff"
                        + ",Memo1,Xa1Par,ExtFlag,emno,emname,xa1no"
                        + " ) VALUES "
                        + "(@PaNo,@PaDate,@PaDate1,@PaDate2"
                        + ",@PaDateacs,@PaDateacs1,@PaDateacs2,@PaDateace"
                        + ",@PaDateace1,@PaDateace2,@FaNo,@FaName1"
                        + ",@RecordNo,@BsDateAc,@BsDateAc1,@BsDateAc2"
                        + ",@BsNo,@Bracket,@Xa1Name,@TotMny"
                        + ",@AcctMny,@InvNo,@Discount,@Reverse"
                        + ",@Xa1Par1,@ReverseB,@ExgStat,@ExgDiff"
                        + ",@Memo1,@Xa1Par,@ExtFlag,@emno,@emname,@xa1no)";
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("discount", dataGridViewT1["折讓金額", i].Value == null ? 0 : dataGridViewT1["折讓金額", i].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("mny", dataGridViewT1["沖帳金額", i].Value == null ? 0 : dataGridViewT1["沖帳金額", i].Value.ToDecimal());
                        cmd.Parameters.AddWithValue("bsno", dataGridViewT1["單據號碼", i].Value == null ? "" : dataGridViewT1["單據號碼", i].Value.ToString());
                        cmd.Parameters.AddWithValue("invno", dataGridViewT1["發票號碼", i].Value == null ? "" : dataGridViewT1["發票號碼", i].Value.ToString());

                        //--現有應付帳款餘額=應付餘額-折讓金額-沖款金額
                        cmd.CommandText = "update Fact set Fact.FaPayable=isnull(Fact.FaPayable,0)-(@discount)-(@mny) where Fact.FaNo=@fano";
                        cmd.ExecuteNonQuery();
                        if (dataGridViewT1["單據", i].Value.ToString() == "期初")
                        {
                            //--(期初帳款餘額=期初帳款餘額)-折讓金額-沖款金額
                            cmd.CommandText = "update Fact set  Fact.FaSparePay=isnull(Fact.FaSparePay,0)-@discount-@mny" +
                            " where Fact.FaNo=@fano";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Update Payabld Set Payabld.acctmny=(select FaSparePay from Fact where FaNo=@fano)" +
                            " where Payabld.FaNo=@fano and bracket = '期初'";
                            cmd.ExecuteNonQuery();
                        }
                        else if (dataGridViewT1["單據", i].Value.ToString() == "進貨")
                        {
                            cmd.CommandText = "Update BShop Set BShop.AcctMny=BShop.AcctMny-@discount-@mny" +
                            ",BShop.CollectMny=IsNull(BShop.CollectMny,0)+@mny" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Update Payabld Set Payabld.acctmny=(select acctmny from BShop where BsNo=@bsno)" +
                            ",Payabld.InvNo=@invno" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Update BShop Set BShop.InvNo=@invno" +
                            ",BShop.Discount= (select ISNULL(sum(Discount),0) from Payabld where BsNo=@bsno and Bracket='進貨')" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();
                        }
                        else if (dataGridViewT1["單據", i].Value.ToString() == "退貨")
                        {
                            cmd.CommandText = "Update RShop Set RShop.AcctMny=RShop.AcctMny+@mny+@discount " +
                             ",RShop.CollectMny=IsNull(RShop.CollectMny,0)-(@mny)" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Update Payabld Set Payabld.acctmny=(-1)*(select acctmny from RShop where BsNo=@bsno)" +
                            ",Payabld.InvNo=@invno" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Update RShop Set RShop.InvNo=@invno" +
                            ",RShop.Discount= (-1)*(select ISNULL(sum(Discount),0) from Payabld where BsNo=@bsno and Bracket='退貨')" +
                            " where BsNo=@bsno";
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                if (!票據系統儲存())//票據部分
                {
                    tran.Rollback();
                }
                else
                {
                    tran.Commit();
                }
                tran.Dispose();
                cmd.Dispose();
                conn.Close(); conn.Dispose();
                SaveCheck = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show("AppendError:\n" + ex.ToString());
            }
            if (!SaveCheck) return;
            tempNo = PaNo.Text.Trim();
            #region 票據部分
            CHKo.Clear();
            RemitO.Clear();
            #endregion
            dtPayabld.Clear();
            dataGridViewT1.DataSource = null;
            dataGridViewT1.RowCount = 0;
            writeToTxt(null);
            PaNo.Focus();
            setTxtWhenAppend();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.None, ref list);

            AppendCheck = false;

            #region 票據部分
            CHKo.Clear();
            RemitO.Clear();
            支票按鈕.Enabled = 匯入按鈕.Enabled = false;
            #endregion
            dataGridViewT1.ReadOnly = true;

            radio1.Enabled = false;
            radio2.Enabled = false;

            單據明細.Enabled = true;
            EmName.Text = EmNo.Text = DeNo.Text = DeName.Text = "";

            loadDB();
            dr = getCurrentDataRow(tempNo);
            if (li.IndexOf(dr) == -1)
            {
                if (li.Count > 0)
                {
                    dr = li.Last();
                    writeToTxt(dr);
                }
                else
                {
                    dr = null;
                    writeToTxt(dr);
                    dataGridViewT1.RowCount = 0;
                }
            }
            else
            {
                writeToTxt(dr);
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (PaNo.ReadOnly)
            {
                FrmPayabldBrow frm = new FrmPayabldBrow();

                frm.TSeekNo = PaNo.Text.Trim();

                frm.票據錯誤 = 票據錯誤;
                frm.票據錯誤文字 = 票據錯誤文字;
                frm.票據連線字串 = 票據連線字串;
                frm.chksys = chksys;
                frm.scrit = scrit;

                frm.ShowDialog();
                dr = getCurrentDataRow(frm.TResult.ToString());
                if (li.IndexOf(dr) == -1)
                {
                    if (li.Count > 0)
                    {
                        dr = li.Last();
                        writeToTxt(dr);
                    }
                    else
                    {
                        dr = null;
                        writeToTxt(dr);
                        dataGridViewT1.RowCount = 0;
                    }
                }
                else
                {
                    writeToTxt(dr);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //抓專案名稱
            string spname = "";
            if (SpNo.Text.ToString().Trim() != "")
            {
                DataTable spnamedt = new DataTable();
                string spnamesql = "select spname from spec where 0=0 and spno=@spno";
                using (SqlConnection spnamecn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = spnamecn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());
                        cmd.CommandText = spnamesql;
                        using (SqlDataAdapter spnameda = new SqlDataAdapter(cmd))
                        {
                            spnameda.Fill(spnamedt);
                            spname = spnamedt.Rows[0][0].ToString().Trim();
                        }
                    }
                }
            }
            using (var frm = new FrmPayabldPrint())
            {
                frm.spname = spname;
                frm.PaNo.Text = PaNo.Text;
                frm.PaNo_1.Text = PaNo.Text;
                frm.ShowDialog();
            }
        }

        void setTxtWhenAppend()
        {
            #region 票據部分
            CheckMny.ReadOnly = RemitMny.ReadOnly = Common.pathC == "" ? false : true;
            #endregion

            PaDate.Text = Date.GetDateTime(Common.User_DateTime);
            //數值欄位填上0
            Writes.ForEach(r =>
            {
                try
                {
                    if (r is TextBoxNumberT)
                    {
                        r.Text = string.Format("{0:F" + (r as TextBoxNumberT).LastNum + "}", 0);
                    }
                }
                catch { }
            });

            Xa1Par.Text = "1.0000";
            radio1.Enabled = true;
            radio2.Enabled = true;
            radio1.Checked = true;
            Memo2.ReadOnly = false;
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (btnAppend.Enabled) return;

            if (dataGridViewT1.Columns[e.ColumnIndex] is DataGridViewTextNumberT)
            {
                var lastN = ((DataGridViewTextNumberT)dataGridViewT1.Columns[e.ColumnIndex]).LastNum;
                dataGridViewT1[e.ColumnIndex, e.RowIndex].Value = dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToDecimal("f" + lastN);
            }
            CashMny_TextChanged(null, null);
        }

        private void Memo2_DoubleClick(object sender, EventArgs e)
        {
            if (Memo2.ReadOnly)
                return;

            using (var frm = new S_61.subMenuFm_2.FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    Memo2.Text = frm.Memo.GetUTF8(60);
                Memo2.SelectAll();
            } 
        }

        bool SetPaNo()
        {
            string strPaNo = "";
            if (PaNo.Text.Trim() != "")
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = Common.sqlConnString;
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    tran = conn.BeginTransaction();
                    cmd.Transaction = tran;

                    cmd.Parameters.AddWithValue("pano", PaNo.Text.Trim());
                    cmd.CommandText = "select COUNT(PaDate) from Payabl where PaNo =@pano COLLATE Chinese_Taiwan_Stroke_BIN";
                    bool isRepeat = true;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            if (reader[0].ToString() == "0")
                                isRepeat = false;
                            else
                                isRepeat = true;
                        }
                    }
                    tran.Commit(); tran.Dispose();
                    cmd.Dispose();
                    conn.Close(); conn.Dispose();
                    if (isRepeat)
                    {
                        MessageBox.Show("進貨單號重複", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else
                        return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("LoadDBError:\n" + ex.ToString());
                    return false;
                }
            }

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Common.sqlConnString;
                conn.Open();

                string strDate = "";
                strDate = Date.ChangeDateForSN(PaDate.Text);


                string str = "select PaNo from Payabl where PaNo like @pano + '%' Order by PaNo COLLATE Chinese_Taiwan_Stroke_BIN Desc";
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("pano", strDate);
                    cmd.CommandText = str;
                    using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                    {
                        DataTable dtPaNo = new DataTable();
                        List<DataRow> li;
                        string CountPaNo = "";
                        dtPaNo.Clear();
                        adr.Fill(dtPaNo);
                        li = dtPaNo.AsEnumerable().ToList();
                        if (dtPaNo.Rows.Count > 0)
                            CountPaNo = dtPaNo.Rows[0]["PaNo"].ToString();
                        else
                            CountPaNo = strDate;
                        int C = 0;
                        switch (Common.Sys_NoAdd)
                        {
                            case 1:
                                CountPaNo = CountPaNo.PadRight(11, '0');
                                int.TryParse(CountPaNo.Substring(7), out C);
                                break;
                            case 2:
                                CountPaNo = CountPaNo.PadRight(11, '0');
                                int.TryParse(CountPaNo.Substring(7), out C);
                                break;
                            case 3:
                                CountPaNo = CountPaNo.PadRight(12, '0');
                                int.TryParse(CountPaNo.Substring(8), out C);
                                break;
                            case 4:
                                CountPaNo = CountPaNo.PadRight(12, '0');
                                int.TryParse(CountPaNo.Substring(8), out C);
                                break;
                        }
                        bool isRepeat = true;
                        do
                        {
                            C++;
                            if (C == 10000)
                            {
                                C = 1;
                                CountPaNo = C.ToString();
                                CountPaNo = CountPaNo.PadLeft(4, '0');
                                strPaNo = strDate + CountPaNo;
                                if (li.Find(r => r.Field<string>("PaNo") == strPaNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                            else
                            {
                                CountPaNo = C.ToString();
                                CountPaNo = CountPaNo.PadLeft(4, '0');
                                strPaNo = strDate + CountPaNo;
                                if (li.Find(r => r.Field<string>("PaNo") == strPaNo) != null)
                                    isRepeat = true;
                                else
                                    isRepeat = false;
                            }
                        } while (isRepeat);
                    }
                }
                PaNo.Text = strPaNo;
                conn.Close(); conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                PaNo.Text = "";
                return false;
            }
        }

        private void 單據明細_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.CurrentCell != null)
            {
                using (var frm = new subMenuFm_3.FrmPayabld_BShopb())
                {
                    if (dataGridViewT1["單據", dataGridViewT1.CurrentCell.RowIndex].Value.ToString() == "期初") return;
                    else if (dataGridViewT1["單據", dataGridViewT1.CurrentCell.RowIndex].Value.ToString() == "進貨")
                        frm.callType = "BShopd where BsNo='" + dataGridViewT1["單據號碼", dataGridViewT1.CurrentCell.RowIndex].Value.ToString() + "' order by RecordNo";
                    else if (dataGridViewT1["單據", dataGridViewT1.CurrentCell.RowIndex].Value.ToString() == "退貨")
                        frm.callType = "RShopd where BsNo='" + dataGridViewT1["單據號碼", dataGridViewT1.CurrentCell.RowIndex].Value.ToString() + "' order by RecordNo";
                    frm.ShowDialog();
                }
            }
        }

        public void Payabld_Brow()
        {
            loadDB();
            tempNo = Browtemp.Trim();
            dr = getCurrentDataRow(tempNo);
            temp = li.IndexOf(dr);
            if (li.Count > 0)
            {
                dr = getCurrentDataRow(tempNo);
                int i = li.IndexOf(dr);
                writeToTxt(dr);
                if (i == 0)
                {
                    btnTop.Enabled = false;
                    btnPrior.Enabled = false;
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                }
                else if (i == li.Count - 1)
                {
                    btnTop.Enabled = true;
                    btnPrior.Enabled = true;
                    btnNext.Enabled = false;
                    btnBottom.Enabled = false;
                }
                else
                {
                    btnTop.Enabled = true;
                    btnPrior.Enabled = true;
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    if (btnAppend.Enabled)
                    {
                        btnAppend.Focus();
                        btnAppend.PerformClick();
                    }
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (btnDelete.Enabled)
                    {
                        btnDelete.PerformClick();
                    }
                    break;
                case Keys.D4:
                    if (btnBrow.Enabled)
                    {
                        btnBrow.PerformClick();
                    }
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    if (btnExit.Enabled)
                    {
                        btnExit.PerformClick();
                    }
                    break;
                case Keys.Home:
                    if (btnTop.Enabled) btnTop.PerformClick();
                    break;
                case Keys.PageUp:
                    if (btnPrior.Enabled) btnPrior.PerformClick();
                    break;
                case Keys.PageDown:
                    if (btnNext.Enabled) btnNext.PerformClick();
                    break;
                case Keys.End:
                    if (btnBottom.Enabled) btnBottom.PerformClick();
                    break;
                case Keys.F9:
                    if (btnSave.Enabled)
                    {
                        btnSave.Focus();
                        btnSave.PerformClick();
                    }
                    break;
                case Keys.F4:
                    if (btnCancel.Enabled)
                    {
                        btnCancel.Focus();
                        btnCancel.PerformClick();
                    }
                    break;
                case Keys.F2:
                    GridDBClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender, row =>
             {
                 EmNo.Text = row["EmNo"].ToString().Trim();
                 EmName.Text = row["EmName"].ToString().Trim();
             });
            GEtDeno();
        }
        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (EmNo.ReadOnly) return;

            if (EmNo.Text.Trim() == "")
            {
                EmNo.Text = "";
                EmName.Text = "";
                GEtDeno();
                return;
            }

            xe.ValidateOpen<JBS.JS.Empl>(sender, e, row =>
            {
                EmNo.Text = row["EmNo"].ToString().Trim();
                EmName.Text = row["EmName"].ToString().Trim();
            });
            GEtDeno();
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (dataGridViewT1.RowCount > 0)
            {
                if (dataGridViewT1.Columns[e.ColumnIndex].Name == "沖帳金額")
                {
                    if (dataGridViewT1.EditingControl != null)
                        dataGridViewT1.EditingControl.Text = dataGridViewT1["未付金額", e.RowIndex].Value.ToString();
                    dataGridViewT1[e.ColumnIndex, e.RowIndex].Value = dataGridViewT1["未付金額", e.RowIndex].Value.ToString();
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
                else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "說明")
                {
                    using (subMenuFm_2.FrmSale_Memo frm = new subMenuFm_2.FrmSale_Memo())
                    {
                        var tb = (TextBox)dataGridViewT1.EditingControl;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            if (dataGridViewT1.EditingControl != null)
                                dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);
                            dataGridViewT1["說明", e.RowIndex].Value = frm.Memo.GetUTF8(20);
                        }
                    }
                }
            }
        }

        private void GridDBClick()
        {
            if (dataGridViewT1.ReadOnly)
                return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            if (dataGridViewT1.EditingControl == null)
                return;

            if (dataGridViewT1.CurrentCell.ColumnIndex != this.沖帳金額.Index)
                return;

            dataGridViewT1.EditingControl.Text = dataGridViewT1["未付金額", index].Value.ToString();
            dataGridViewT1["沖帳金額", index].Value = dataGridViewT1["未付金額", index].Value.ToString();
        }

        private void SpNo_DoubleClick(object sender, EventArgs e)
        {
            if (SpNo.ReadOnly)
                return;

            if (Common.Sys_StockKind == 2)
            {
                xe.Open<JBS.JS.Shift>(sender);
            }
            else
            {
                xe.Open<JBS.JS.Spec>(sender);
            }
        }

        private void SpNo_Validating(object sender, CancelEventArgs e)
        {
            if (SpNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (SpNo.TrimTextLenth() == 0)
            {
                datagridchange();
                return;
            }

            try
            {
                if (Common.Sys_StockKind == 2)
                {
                    xe.ValidateOpen<JBS.JS.Shift>(sender, e, row =>
                    {
                        SpNo.Text = row["ShNo"].ToString().Trim();
                    });
                }
                else
                {
                    xe.ValidateOpen<JBS.JS.Spec>(sender, e, row =>
                    {
                        SpNo.Text = row["SpNo"].ToString().Trim();
                    });
                }
                datagridchange();
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(ex.ToString());
            }
        }

        void GetEmname()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                cmd.Parameters.AddWithValue("deno", DeNo.Text.Trim());

                cmd.CommandText = "select emname,emdeno from empl where emno=@emno";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    EmName.Text = reader["emname"].ToString();
                    DeNo.Text = reader["emdeno"].ToString();
                }
                reader.Dispose(); reader.Close();
                cmd.CommandText = "select dename1 from dept where deno=@deno";
                if (!cmd.ExecuteScalar().IsNullOrEmpty())
                    DeName.Text = cmd.ExecuteScalar().ToString().Trim();
            }
        }

        void GEtDeno()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                cmd.Parameters.AddWithValue("deno", DeNo.Text.Trim());

                cmd.CommandText = "select emdeno from empl where emno=@emno";
                if (!cmd.ExecuteScalar().IsNullOrEmpty())
                    DeNo.Text = cmd.ExecuteScalar().ToString();
                else
                    DeNo.Text = "";

                cmd.CommandText = "select dename1 from dept where deno=@deno";
                if (!cmd.ExecuteScalar().IsNullOrEmpty())
                    DeName.Text = cmd.ExecuteScalar().ToString().Trim();
                else
                    DeName.Text = "";
            }
        }

        private void DeNo_DoubleClick(object sender, EventArgs e)
        {
            if (DeNo.ReadOnly)
                return;

            xe.Open<JBS.JS.Dept>(sender, row =>
            {
                DeNo.Text = row["deno"].ToString();
                DeName.Text = row["dename1"].ToString();
            });
        }

        private void DeNo_Validating(object sender, CancelEventArgs e)
        {
            if (DeNo.ReadOnly || btnCancel.Focused) return;
            if (DeNo.TrimTextLenth() == 0)
            {
                DeNo.Clear();
                DeName.Clear();
                return;
            }
            xe.ValidateOpen<JBS.JS.Dept>(sender, e, row =>
            {
                DeNo.Text = row["deno"].ToString();
                DeName.Text = row["dename1"].ToString();
            });
        }

        #region 票據部分
        private void 支票按鈕_Click(object sender, EventArgs e)
        {
            if (票據錯誤)
            {
                MessageBox.Show(票據錯誤文字, "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (FaNo.Text.Trim() == "")
            {
                MessageBox.Show("請先輸入廠商編號", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                return;
            }
            using (var frm = new 應付票據建檔())
            {
                frm.tbM = CHKo.Copy();
                frm.chksys = chksys;
                frm.scrit = scrit;
                frm.廠商編號 = FaNo.Text.Trim();
                frm.票據連線字串 = 票據連線字串;
                frm.ShowDialog();
            }
        }
        private void 匯入按鈕_Click(object sender, EventArgs e)
        {
            if (票據錯誤)
            {
                MessageBox.Show(票據錯誤文字, "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (FaNo.Text.Trim() == "")
            {
                MessageBox.Show("請先輸入廠商編號", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FaNo.Focus();
                return;
            }
            using (var frm = new 帳款匯出作業())
            {
                frm.tbM = RemitO.Copy();
                frm.chksys = chksys;
                frm.scrit = scrit;
                frm.廠商編號 = FaNo.Text.Trim();
                frm.票據連線字串 = 票據連線字串;
                frm.ShowDialog();
            }
        }
        bool 票據系統儲存()
        {
            if (票據錯誤)
            {
                MessageBox.Show(票據錯誤文字, "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (票據連線字串.Trim() == "") return true;
            try
            {
                #region 皇翼與票據系統連接，常常有沖帳後支票與匯入沒寫入票據系統
                //驗證暫存table與畫面金額是否一樣
                //1.沒匯入票據可能是table在程式中被誤刪
                if (CheckMny.Text.Trim().ToDecimal() > 0 && CHKo.Rows.Count == 0)
                {
                    MessageBox.Show("[支票金額]有誤，請連絡華越資訊工程師");
                    return false;
                }
                if (RemitMny.Text.Trim().ToDecimal() > 0 && RemitO.Rows.Count == 0)
                {
                    MessageBox.Show("[匯入金額]有誤，請連絡華越資訊工程師");
                    return false;
                }

                #endregion
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    SqlTransaction tran = cn.BeginTransaction();
                    cmd.Transaction = tran;

                    if (CHKo.Rows.Count > 0)
                    {
                        for (int i = 0; i < CHKo.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("chstno", PaNo.Text.Trim());
                            cmd.Parameters.AddWithValue("chstnum", Common.CoNo);
                            cmd.Parameters.AddWithValue("chno", CHKo.Rows[i]["chno"].ToString().Trim());
                            cmd.Parameters.AddWithValue("cono", CHKo.Rows[i]["cono"].ToString().Trim());
                            cmd.Parameters.AddWithValue("coname1", CHKo.Rows[i]["coname1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("fano", CHKo.Rows[i]["fano"].ToString().Trim());
                            cmd.Parameters.AddWithValue("faname1", CHKo.Rows[i]["faname1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("faname2", CHKo.Rows[i]["faname2"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chline", CHKo.Rows[i]["chline"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chdis", CHKo.Rows[i]["chdis"].ToString().Trim());
                            cmd.Parameters.AddWithValue("acno", CHKo.Rows[i]["acno"].ToString().Trim());
                            cmd.Parameters.AddWithValue("acname", CHKo.Rows[i]["acname"].ToString().Trim());
                            cmd.Parameters.AddWithValue("acname1", CHKo.Rows[i]["acname1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chdate", CHKo.Rows[i]["chdate"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chdate_1", CHKo.Rows[i]["chdate_1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chdate1", CHKo.Rows[i]["chdate1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chdate1_1", CHKo.Rows[i]["chdate1_1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chdate2", CHKo.Rows[i]["chdate2"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chdate2_1", CHKo.Rows[i]["chdate2_1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chdate3", CHKo.Rows[i]["chdate3"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chdate3_1", CHKo.Rows[i]["chdate3_1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chmny", CHKo.Rows[i]["chmny"].ToDecimal());
                            cmd.Parameters.AddWithValue("chstatus", CHKo.Rows[i]["chstatus"].ToDecimal());
                            cmd.Parameters.AddWithValue("chstname", CHKo.Rows[i]["chstname"].ToString().Trim());
                            cmd.Parameters.AddWithValue("chmemo", "進銷存轉入 進銷存代碼:" + Common.CoNo + " 沖款單號:" + PaNo.Text.Trim());
                            cmd.CommandText = "insert into chko"
                                + " (chstno,chstnum,chno,cono,coname1,fano,faname1,faname2,chline,chdis,"
                                + "chdate,chdate_1,chdate1,chdate1_1,chdate2,chdate2_1,chdate3,chdate3_1,chmny,chstatus,chstname,chmemo,acno,acname,acname1) values "
                                + " (@chstno,@chstnum,@chno,@cono,@coname1,@fano,@faname1,@faname2,@chline,@chdis,@"
                                + "chdate,@chdate_1,@chdate1,@chdate1_1,@chdate2,@chdate2_1,@chdate3,@chdate3_1,@chmny,@chstatus,@chstname,@chmemo,@acno,@acname,@acname1); ";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    if (RemitO.Rows.Count > 0)
                    {
                        for (int i = 0; i < RemitO.Rows.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("reno", RemitO.Rows[i]["reno"].ToString().Trim());
                            cmd.Parameters.AddWithValue("cono", RemitO.Rows[i]["cono"].ToString().Trim());
                            cmd.Parameters.AddWithValue("coname1", RemitO.Rows[i]["coname1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("coname2", RemitO.Rows[i]["coname2"].ToString().Trim());
                            cmd.Parameters.AddWithValue("redate", RemitO.Rows[i]["redate"].ToString().Trim());
                            cmd.Parameters.AddWithValue("redate_1", RemitO.Rows[i]["redate_1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("fano", RemitO.Rows[i]["fano"].ToString().Trim());
                            cmd.Parameters.AddWithValue("faname1", RemitO.Rows[i]["faname1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("faname2", RemitO.Rows[i]["faname2"].ToString().Trim());
                            cmd.Parameters.AddWithValue("acno", RemitO.Rows[i]["acno"].ToString().Trim());
                            cmd.Parameters.AddWithValue("acname", RemitO.Rows[i]["acname"].ToString().Trim());
                            cmd.Parameters.AddWithValue("acname1", RemitO.Rows[i]["acname1"].ToString().Trim());
                            cmd.Parameters.AddWithValue("remny", RemitO.Rows[i]["remny"].ToString().Trim());
                            cmd.Parameters.AddWithValue("xa1par", Xa1Par.Text.ToDecimal("f4"));
                            cmd.Parameters.AddWithValue("xa1name", RemitO.Rows[i]["xa1name"].ToString().Trim());
                            cmd.Parameters.AddWithValue("restno", PaNo.Text.Trim());
                            cmd.Parameters.AddWithValue("chstnum", Common.CoNo);
                            cmd.Parameters.AddWithValue("chargemny", RemitO.Rows[i]["chargemny"].ToString().Trim());
                            cmd.Parameters.AddWithValue("surcharge", RemitO.Rows[i]["surcharge"].ToString().Trim());
                            cmd.Parameters.AddWithValue("rememo", "進銷存轉入 進銷存代碼:" + Common.CoNo + " 沖款單號:" + PaNo.Text.Trim());

                            cmd.CommandText = "insert into remito "
                                + "(reno,cono,coname1,coname2,redate,redate_1,fano,faname1,faname2"
                                + ",acno,acname,acname1,remny,xa1name,restno,chstnum,chargemny,surcharge,rememo,xa1par) values "
                                + "(@reno,@cono,@coname1,@coname2,@redate,@redate_1,@fano,@faname1,@faname2"
                                + ",@acno,@acname,@acname1,@remny,@xa1name,@restno,@chstnum,@chargemny,@surcharge,@rememo,@xa1par);";

                            cmd.Parameters.AddWithValue("mny", (RemitO.Rows[i]["remny"].ToDecimal() + RemitO.Rows[i]["chargemny"].ToDecimal()));

                            cmd.CommandText += "update acct set acmny1=acmny1-@mny where acno=@acno;";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    #region 驗證資料有無存入票據系統
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("chstno", PaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("chstnum", Common.CoNo);
                    cmd.CommandText = "select count(chno) from chko where chstno=@chstno and chstnum=@chstnum";
                    if (CHKo.Rows.Count.ToDecimal() != cmd.ExecuteScalar().ToDecimal())
                    {
                        MessageBox.Show("沖款[支票筆數]與票據系統不符，請連絡華越資訊工程師");
                        tran.Rollback();
                        return false;
                    }
                    cmd.CommandText = "select count(reno) from remito where restno=@chstno and chstnum=@chstnum";
                    if (RemitO.Rows.Count.ToDecimal() != cmd.ExecuteScalar().ToDecimal())
                    {
                        MessageBox.Show("沖款[匯出筆數]與票據系統不符，請連絡華越資訊工程師");
                        tran.Rollback();
                        return false;
                    }
                    #endregion
                    tran.Commit();
                    tran.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        bool 票據系統刪除()
        {
            if (票據錯誤)
            {
                MessageBox.Show(票據錯誤文字, "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (票據連線字串.Trim() == "") return true;
            try
            {
                #region 由沖帳產生之支票與匯出，不可由票據系統內刪除
                using (SqlConnection cn = new SqlConnection(票據連線字串))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    SqlTransaction tran = cn.BeginTransaction();
                    cmd.Transaction = tran;
                    cmd.Parameters.AddWithValue("pano", PaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("chstnum", Common.CoNo);

                    cmd.Parameters.AddWithValue("reno", "");
                    cmd.Parameters.AddWithValue("mny", "");
                    cmd.Parameters.AddWithValue("acno", "");

                    //cmd.CommandText = "select sum(chmny) from chko where chstno=@pano and chstnum=@chstnum;";
                    //if (cmd.ExecuteScalar().ToDecimal() != CheckMny.Text.ToDecimal())
                    //{
                    //    MessageBox.Show("沖款[支票金額]與票據系統不符，請連絡華越資訊工程師");
                    //    return false;
                    //}
                    //cmd.CommandText = "select sum(REMNY) from remito where restno=@pano and chstnum=@chstnum";
                    //if (RemitMny.Text.ToDecimal() != cmd.ExecuteScalar().ToDecimal())
                    //{
                    //    MessageBox.Show("沖款[匯出金額]與票據系統不符，請連絡華越資訊工程師");
                    //    return false;
                    //}
                    //cmd.CommandText = "select sum(chargemny) from remito where restno=@pano and chstnum=@chstnum";
                    //if (FeeMny.Text.ToDecimal() != cmd.ExecuteScalar().ToDecimal())
                    //{
                    //    MessageBox.Show("沖款[手續費]與票據系統不符，請連絡華越資訊工程師");
                    //    return false;
                    //}

                    if (li.Find(r => r["pano"].ToString().Trim() == PaNo.Text.Trim())["cono"].ToString().Trim() != "")
                    {
                        DataTable tempchko = new DataTable();
                        cmd.CommandText = "select * from chko where chstno=@pano and chstnum=@chstnum;";
                        SqlDataAdapter dd = new SqlDataAdapter(cmd);
                        dd.Fill(tempchko);
                        cmd.CommandText = "delete chko where chstno=@pano and chstnum=@chstnum;";
                        cmd.ExecuteNonQuery();

                        DataTable temp = new DataTable();
                        cmd.CommandText = "select * from remito where restno=@pano and chstnum=@chstnum;";
                        dd.Fill(temp);
                        for (int i = 0; i < temp.Rows.Count; i++)
                        {
                            cmd.Parameters["reno"].Value = temp.Rows[i]["reno"].ToString().Trim();
                            cmd.Parameters["mny"].Value = (temp.Rows[i]["remny"].ToDecimal() + temp.Rows[i]["chargemny"].ToDecimal());
                            cmd.Parameters["acno"].Value = temp.Rows[i]["acno"].ToString().Trim();

                            cmd.CommandText = "delete remito where reno=@reno;";
                            cmd.CommandText += "update acct set acmny1=acmny1+@mny where acno=@acno;";
                            cmd.ExecuteNonQuery();
                        }
                        for (int i = 0; i < tempchko.Rows.Count; i++)
                        {
                            if (tempchko.Rows[i]["chstatus"].ToDecimal() == 3)
                            {
                                cmd.Parameters["mny"].Value = tempchko.Rows[i]["ChMny"].ToDecimal();
                                cmd.Parameters["acno"].Value = tempchko.Rows[i]["AcNo"].ToString().Trim();
                                cmd.CommandText = "update acct set acmny1+=@mny where acno=@acno";
                                cmd.ExecuteNonQuery();
                            }
                        }
                        tempchko.Clear();
                        temp.Clear();
                    }

                    cmd.CommandText = "select count(chno) from chko where chstno=@pano and chstnum=@chstnum;";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("票據系統相關支票尚未刪除，請連絡華越資訊工程師");
                        tran.Rollback();
                        return false;
                    }
                    cmd.CommandText = "select count(reno) from remito where restno=@pano and chstnum=@chstnum";
                    if (cmd.ExecuteScalar().ToDecimal() > 0)
                    {
                        MessageBox.Show("票據系統相關匯出尚未刪除，請連絡華越資訊工程師");
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
        #endregion

        private void PaDateAcs_Enter(object sender, EventArgs e)
        {
            PaDateAcsTemp = PaDateAcs.Text.Trim();

        }

        private void PaDateAce_Enter(object sender, EventArgs e)
        {
            PaDateAceTemp = PaDateAce.Text.Trim();
        }

        private void FaNo_Enter(object sender, EventArgs e)
        {
            FaNoTEMP = FaNo.Text;
        }

        private void PaDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (PaDate.ReadOnly) return;

            if (!PaDate.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("日期格式錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PaDate.Text = Date.GetDateTime(Common.User_DateTime);
            }
        }

        private void PaDateAcs_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (PaDateAcs.ReadOnly) return;
            if (PaDateAcs.Text.Trim() == PaDateAcsTemp) return;
            if (PaDateAcs.TrimTextLenth() == 0)
            {
                datagridchange();
                return;
            }

            if (!PaDateAcs.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("日期格式錯誤,請重新輸入！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PaDateAcs.SelectAll();
                return;
            }
            else
            {
                if (FaNo.TrimTextLenth() == 0) return;
                if (PaDateAcs.Text.Trim() == PaDateAcsTemp) return;
                datagridchange();
            }
        }

        private void PaDateAce_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (PaDateAce.ReadOnly) return;
            if (PaDateAce.Text.Trim() == PaDateAceTemp) return;
            if (PaDateAce.TrimTextLenth() == 0)
            {
                datagridchange();
                return;
            }

            if (!PaDateAce.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("日期格式錯誤,請重新輸入！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PaDateAce.SelectAll();
                return;
            }
            else
            {
                if (FaNo.TrimTextLenth() == 0) return;
                if (PaDateAce.Text.Trim() == PaDateAceTemp) return;
                datagridchange();
            }
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender, reader =>
            {
                if (FaNoTEMP == reader["FaNo"].ToString())
                    return;

                FaNoTEMP = reader["FaNo"].ToString();
                FaNo.Text = reader["FaNo"].ToString();
                FaName1.Text = reader["FaName1"].ToString();
                FaPayAmt.Text = reader["FaPayAmt"].ToDecimal().ToString("f" + Common.MFT);
                EmNo.Text = reader["faemno1"].ToString();
                ActSlt.Text = "1";

                xe.Validate<JBS.JS.Empl>(EmNo.Text, row =>
                {
                    EmNo.Text = row["EmNo"].ToString().Trim();
                    EmName.Text = row["EmName"].ToString().Trim();
                    DeNo.Text = row["Emdeno"].ToString().Trim();
                }, () =>
                {
                    EmNo.Text = "";
                    EmName.Text = "";
                    DeNo.Text = "";
                });
                xe.Validate<JBS.JS.Dept>(DeNo.Text, r => DeName.Text = r["DeName1"].ToString(), () => DeName.Clear());

                radio1.Enabled = true;
                radio2.Enabled = true;
                radio1.Checked = true;

                datagridchange();
            });
        }

        private void FaNo_Validating(object sender, CancelEventArgs e)
        {
            if (FaNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (FaNo.Text.Trim() == "")
            {
                FaNo.Text = "";
                FaName1.Text = "";
                datagridchange();
                return;
            }

            xe.ValidateOpen<JBS.JS.Fact>(sender, e, reader =>
            {
                if (reader["FaNo"].ToString().Trim() == FaNoTEMP)
                    return;

                FaNo.Text = reader["FaNo"].ToString().Trim();
                FaName1.Text = reader["FaName1"].ToString().Trim();
                FaPayAmt.Text = reader["FaPayAmt"].ToDecimal().ToString("f" + Common.MST);
                EmNo.Text = reader["FaEmNo1"].ToString().Trim();

                xe.Validate<JBS.JS.Empl>(EmNo.Text, row =>
                {
                    EmNo.Text = row["EmNo"].ToString().Trim();
                    EmName.Text = row["EmName"].ToString().Trim();
                    DeNo.Text = row["Emdeno"].ToString().Trim();
                }, () =>
                {
                    EmNo.Text = "";
                    EmName.Text = "";
                    DeNo.Text = "";
                });
                xe.Validate<JBS.JS.Dept>(DeNo.Text, r => DeName.Text = r["DeName1"].ToString(), () => DeName.Clear());

                radio1.Enabled = true;
                radio2.Enabled = true;
                radio1.Checked = true;
                ActSlt.Text = "1";

                datagridchange();
                CashMny_TextChanged(null, null);

            }, true);

        }

        public void datagridchange()
        {
            CashMny.Text = (0M).ToString("f" + CashMny.LastNum);
            CardMny.Text = (0M).ToString("f" + CardMny.LastNum);
            Ticket.Text = (0M).ToString("f" + Ticket.LastNum);
            CheckMny.Text = (0M).ToString("f" + CheckMny.LastNum);
            RemitMny.Text = (0M).ToString("f" + RemitMny.LastNum);
            OtherMny.Text = (0M).ToString("f" + OtherMny.LastNum);
            GetPrvAcc.Text = (0M).ToString("f" + GetPrvAcc.LastNum);
            AddPrvAcc.Text = (0M).ToString("f" + AddPrvAcc.LastNum);
            TotMny.Text = (0M).ToString("f" + TotMny.LastNum);
            CardNo.Text = "";

            int num = 0;
            //期初
            try
            {
                //填入明細
                dataGridViewT1.Rows.Clear();
                string str = "SELECT FaFirPayabl as 期初應付帳款金額,FaSparePay as 期初應付帳款餘額,FaFirPayPar as 期初應付帳款匯率,Xa1Name 幣別,faxa1no 幣別編號 FROM Fact left join Xa01 on Xa01.Xa1No=Fact.FaXa1no "
                    + " WHERE FaNo=@fano COLLATE Chinese_Taiwan_Stroke_BIN ";

                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                    cmd.CommandText = str;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["期初應付帳款餘額"].ToString() != null || dr["期初應付帳款餘額"].ToString() != "" || dr["期初應付帳款餘額"].ToString() != "0")
                            if (dr["期初應付帳款餘額"].ToDecimal() != 0)
                            {
                                dataGridViewT1.Rows.Add(1);

                                dataGridViewT1["帳款日期", num].Value = "";
                                dataGridViewT1["單據號碼", num].Value = "";

                                dataGridViewT1["序號", num].Value = (num + 1).ToString();
                                dataGridViewT1["單據", num].Value = "期初";
                                dataGridViewT1["單據總計", num].Value = dr["期初應付帳款金額"].ToDecimal("f" + Common.MFT);
                                dataGridViewT1["未付金額", num].Value = dr["期初應付帳款餘額"].ToDecimal("f" + Common.MFT);
                                dataGridViewT1["幣別", num].Value = dr["幣別"].ToString();
                                dataGridViewT1["匯率", num].Value = dr["期初應付帳款匯率"].ToDecimal("f4");
                                dataGridViewT1["幣別編號", num].Value = dr["幣別編號"].ToString();
                                dataGridViewT1["沖款匯率", num].Value = Xa1Par.Text.ToDecimal("f4");
                                dataGridViewT1["本幣金額", num].Value = "0";
                                dataGridViewT1["匯兌狀況", num].Value = "匯兌收益";
                                dataGridViewT1["匯差金額", num].Value = "0";
                                dataGridViewT1["沖帳金額", num].Value = "0";
                                dataGridViewT1["折讓金額", num].Value = "0";
                                num++;
                            }
                    }

                    dr.Dispose(); cmd.Dispose();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); };


            //進貨
            try
            {
                //填入明細
                string str;
                if (Common.User_DateTime == 1)
                {
                    str = "SELECT emno as 業務編號,emname as 業務名稱,BsNo as 進貨單號,BsDateAc as 帳款日期,TotMny as 外幣總額,AcctMny 未付金額,Xa1Name 幣別,Xa1Par 匯率,InvNo 發票號碼,xa1no 幣別編號 FROM BShop WHERE FaNo=@fano";
                    if (PaDateAcs.Text != "") str += " and BsDateAc>=@date";
                    if (PaDateAce.Text != "") str += " and BsDateAc<=@date1";
                    if (SpNo.Text != "") str += " and spno =@spno";
                    str += " COLLATE Chinese_Taiwan_Stroke_BIN order by 帳款日期,進貨單號";
                }
                else
                {
                    str = "SELECT emno as 業務編號,emname as 業務名稱,BsNo as 進貨單號,BsDateAc1 as 帳款日期,TotMny as 外幣總額,AcctMny 未付金額,Xa1Name 幣別,Xa1Par 匯率,InvNo 發票號碼,xa1no 幣別編號 FROM BShop WHERE FaNo=@fano";
                    if (PaDateAcs.Text != "") str += " and BsDateAc1>=@date";
                    if (PaDateAce.Text != "") str += " and BsDateAc1<=@date1";
                    if (SpNo.Text != "") str += " and spno =@spno";
                    str += " COLLATE Chinese_Taiwan_Stroke_BIN order by 帳款日期,進貨單號";
                }
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                    if (PaDateAcs.Text != "") cmd.Parameters.AddWithValue("date", PaDateAcs.Text.Trim());
                    if (PaDateAce.Text != "") cmd.Parameters.AddWithValue("date1", PaDateAce.Text.Trim());
                    if (SpNo.Text != "") cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["未付金額"].ToString() != null || dr["未付金額"].ToString() != "" || dr["未付金額"].ToString() != "0")
                            if (dr["未付金額"].ToDecimal() != 0)
                            {
                                dataGridViewT1.Rows.Add(1);
                                dataGridViewT1["序號", num].Value = (num + 1).ToString();
                                dataGridViewT1["帳款日期", num].Value = dr["帳款日期"].ToString();
                                dataGridViewT1["單據", num].Value = "進貨";
                                dataGridViewT1["單據號碼", num].Value = dr["進貨單號"].ToString();
                                dataGridViewT1["單據總計", num].Value = dr["外幣總額"].ToDecimal("f" + Common.MFT);
                                dataGridViewT1["未付金額", num].Value = dr["未付金額"].ToDecimal("f" + Common.MFT);
                                dataGridViewT1["幣別", num].Value = dr["幣別"].ToString();
                                dataGridViewT1["匯率", num].Value = dr["匯率"].ToDecimal("f4");
                                dataGridViewT1["幣別編號", num].Value = dr["幣別編號"].ToString();
                                dataGridViewT1["沖款匯率", num].Value = Xa1Par.Text.ToDecimal("f4");
                                dataGridViewT1["本幣金額", num].Value = "0";
                                dataGridViewT1["匯兌狀況", num].Value = "匯兌收益";
                                dataGridViewT1["匯差金額", num].Value = "0";
                                dataGridViewT1["沖帳金額", num].Value = "0";
                                dataGridViewT1["折讓金額", num].Value = "0";
                                dataGridViewT1["發票號碼", num].Value = dr["發票號碼"].ToString();
                                dataGridViewT1["業務編號", num].Value = dr["業務編號"].ToString();
                                dataGridViewT1["業務名稱", num].Value = dr["業務名稱"].ToString();
                                num++;
                            }
                    }
                    dr.Dispose();
                    cmd.Dispose();
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); };

            //進貨退回
            try
            {
                //填入明細
                string str;
                if (Common.User_DateTime == 1)
                {
                    str = "SELECT emno as 業務編號,emname as 業務名稱,BsNo as 進貨單號,BsDateAc as 帳款日期,TotMny as 外幣總額,AcctMny 未付金額,Xa1Name 幣別,Xa1Par 匯率,InvNo 發票號碼,xa1no 幣別編號 FROM RShop WHERE FaNo=@fano";
                    if (PaDateAcs.Text != "") str += " and BsDateAc>=@date";
                    if (PaDateAce.Text != "") str += " and BsDateAc<=@date1";
                    if (SpNo.Text != "") str += " and spno =@spno";
                    str += " COLLATE Chinese_Taiwan_Stroke_BIN order by 帳款日期,進貨單號";
                }
                else
                {
                    str = "SELECT emno as 業務編號,emname as 業務名稱,BsNo as 進貨單號,BsDateAc1 as 帳款日期,TotMny as 外幣總額,AcctMny 未付金額,Xa1Name 幣別,Xa1Par 匯率,InvNo 發票號碼,xa1no 幣別編號 FROM RShop WHERE FaNo=@fano";
                    if (PaDateAcs.Text != "") str += " and BsDateAc1>=@date";
                    if (PaDateAce.Text != "") str += " and BsDateAc1<=@date1";
                    if (SpNo.Text != "") str += " and spno =@spno";
                    str += " COLLATE Chinese_Taiwan_Stroke_BIN order by 帳款日期,進貨單號";
                }
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("fano", FaNo.Text.Trim());
                    if (PaDateAcs.Text != "") cmd.Parameters.AddWithValue("date", PaDateAcs.Text.Trim());
                    if (PaDateAce.Text != "") cmd.Parameters.AddWithValue("date1", PaDateAce.Text.Trim());
                    if (SpNo.Text != "") cmd.Parameters.AddWithValue("spno", SpNo.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["未付金額"].ToString() != null || dr["未付金額"].ToString() != "" || dr["未付金額"].ToString() != "0")
                            if (dr["未付金額"].ToDecimal() != 0)
                            {
                                dataGridViewT1.Rows.Add(1);
                                dataGridViewT1["序號", num].Value = (num + 1).ToString();
                                dataGridViewT1["帳款日期", num].Value = dr["帳款日期"].ToString();
                                dataGridViewT1["單據", num].Value = "退貨";
                                dataGridViewT1["單據號碼", num].Value = dr["進貨單號"].ToString();
                                dataGridViewT1["單據總計", num].Value = (-1) * dr["外幣總額"].ToDecimal("f" + Common.MFT);
                                dataGridViewT1["未付金額", num].Value = (-1) * dr["未付金額"].ToDecimal("f" + Common.MFT);
                                dataGridViewT1["幣別", num].Value = dr["幣別"].ToString();
                                dataGridViewT1["匯率", num].Value = dr["匯率"].ToDecimal("f4");
                                dataGridViewT1["幣別編號", num].Value = dr["幣別編號"].ToString();
                                dataGridViewT1["沖款匯率", num].Value = Xa1Par.Text.ToDecimal("f4");
                                dataGridViewT1["本幣金額", num].Value = "0";
                                dataGridViewT1["匯兌狀況", num].Value = "匯兌收益";
                                dataGridViewT1["匯差金額", num].Value = "0";
                                dataGridViewT1["沖帳金額", num].Value = "0";
                                dataGridViewT1["折讓金額", num].Value = "0";
                                dataGridViewT1["發票號碼", num].Value = dr["發票號碼"].ToString();
                                dataGridViewT1["業務編號", num].Value = dr["業務編號"].ToString();
                                dataGridViewT1["業務名稱", num].Value = dr["業務名稱"].ToString();
                                num++;
                            }
                    }

                    dr.Dispose(); cmd.Dispose();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); };

            var rows = dataGridViewT1.Rows.OfType<DataGridViewRow>()
                .OrderBy(r => r.Cells["帳款日期"].Value.ToString().Trim())
                .ThenBy(r => r.Cells["單據號碼"].Value.ToString().Trim())
                .ThenBy(r => r.Cells["單據"].Value.ToString().Trim())
                .ToArray();
            dataGridViewT1.Rows.Clear();
            dataGridViewT1.Rows.AddRange(rows);



            decimal FaPayable_Num = 0;
            if (dataGridViewT1.RowCount > 0)
                for (int i = 0; i < dataGridViewT1.RowCount; i++)
                {
                    dataGridViewT1["序號", i].Value = (i + 1).ToString();
                    FaPayable_Num += decimal.Parse(dataGridViewT1["未付金額", i].Value.ToString());
                }
            FaPayable.Text = FaPayable_Num.ToString("f" + Common.MFT);
        }

        private void radios_CheckedChanged(object sender, EventArgs e)
        {
            if (PaNo.ReadOnly) return;
            if (radio1.Checked)
            {
                ActSlt.Text = "1";
                radio1.Enabled = true;
                radio2.Enabled = true;
            }
            else if (radio2.Checked)
            {
                ActSlt.Text = "2";
                radio1.Enabled = true;
                radio2.Enabled = true;

                decimal TotMny1Temp = decimal.Parse(TotMny1.Text);
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["沖帳金額", i].Value = "0";
                }
                CashMny_TextChanged(null, null);

                decimal sum沖帳金額 = 0;
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                    sum沖帳金額 += decimal.Parse(dataGridViewT1["未付金額", i].Value.ToString());

                if (sum沖帳金額 != decimal.Parse(TotMny1.Text))
                {
                    for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                    {
                        if (decimal.Parse(TotMny1.Text) > 0)
                        {
                            dataGridViewT1["沖帳金額", i].Value = "0";
                            if (decimal.Parse(TotMny1.Text) > decimal.Parse(dataGridViewT1["未付金額", i].Value.ToString()))
                            {
                                dataGridViewT1["沖帳金額", i].Value = decimal.Parse(dataGridViewT1["未付金額", i].Value.ToString());
                                if (dataGridViewT1.EditingControl != null)
                                    dataGridViewT1.EditingControl.Text = dataGridViewT1["未付金額", i].Value.ToString();
                            }
                            else
                            {
                                dataGridViewT1["沖帳金額", i].Value = decimal.Parse(TotMny1.Text);
                                if (dataGridViewT1.EditingControl != null)
                                    dataGridViewT1.EditingControl.Text = TotMny1.Text;
                            }
                            CashMny_TextChanged(null, null);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                    {
                        dataGridViewT1["沖帳金額", i].Value = decimal.Parse(dataGridViewT1["未付金額", i].Value.ToString());
                        CashMny_TextChanged(null, null);
                    }
                }
            }
        }

        private void Xa1Par_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            if (Xa1Par.ReadOnly) return;

            if (Xa1Par.Text.ToDecimal() <= 0)
            {
                e.Cancel = true;
                MessageBox.Show("沖款匯率不可小於或等於零！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["沖款匯率", i].Value = Xa1Par.Text.ToDecimal("f4");
                }
                CashMny_TextChanged(null, null);
            }
        }

        private void FrmPayabld_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmPayabldQuery())
            {
                frm.TSeekNo = PaNo.Text.Trim();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    writeToTxt(Common.load("Check", "payabl", "pano", frm.TResult));
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            支票按鈕.Location = new Point(CheckMny.Right + 5, 支票按鈕.Location.Y);
            匯入按鈕.Location = new Point(RemitMny.Right + 5, 匯入按鈕.Location.Y);
            lblFee.Location = new Point(匯入按鈕.Right + 5, lblFee.Location.Y);
            FeeMny.Location = new Point(OtherMny.Right + 5, FeeMny.Location.Y);
        }
    }
}
