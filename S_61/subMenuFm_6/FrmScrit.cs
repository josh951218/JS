using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class FrmScrit : Formbase
    {
        JBS.JS.xEvents xe;
        SqlTransaction tran;
        DataRow dr;
        DataTable dt = new DataTable();
        List<DataRow> li = new List<DataRow>();

        DataTable dtScritd = new DataTable();
        DataTable dtDefault = new DataTable();

        string btnState;
        string tempNo;
        string beforeScName;
        int temp = 0;

        List<TextBoxbase> list;

        public FrmScrit()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            list = this.getEnumMember();

            if (Common.Series == "74")
            {
                StNo.Enabled = false;
                StName.Enabled = false;
            }
            if (Common.Series == "73")
            {
                StNo.Enabled = false;
                StName.Enabled = false;
            }

            if (Common.Sys_BookNo.EndsWith("M") == false)
            {
                //不是註冊百貨，不顯示百貨功能
                tabControl1.TabPages.RemoveAt(1);
            }
        }

        private void FrmScrit_Load(object sender, EventArgs e)
        {
            loadDB();
            if (dt.Rows.Count > 0)
            {
                dr = li.Find(r => r.Field<string>("ScName") == Common.User_Name);
                writeToTxt(dr);
            }
            btnAppend.Focus();
        }

        void loadDB()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from scrit", cn))
                {
                    dt.Clear();
                    li.Clear();

                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        li.Clear();
                        li = dt.AsEnumerable().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadScritD(string scno)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@scno", scno.Trim());
                    cmd.CommandText = "select * from scritd where scno =@scno";

                    dtScritd.Clear();
                    da.Fill(dtScritd);

                    dataGridViewT1.DataSource = dtScritd;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadStock(string stno)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StNo", stno.Trim());
                    cmd.CommandText = "select StName from StkRoom where StNo =@StNo";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            StName.Text = reader["StName"].ToString();
                        else
                        {
                            StNo.Text = "";
                            StName.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadXX05(string x5no)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@X5No", x5no.Trim());
                    cmd.CommandText = "select X5Name from XX05 where X5No =@X5No";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            X5Name.Text = reader["X5Name"].ToString();
                        else
                        {
                            X5No.Text = "";
                            X5Name.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void loadCust(string cuno)
        {
            if (cuno.Trim().Length == 0) CuNo.Text = CuName1.Text = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CuNo", cuno.Trim());
                    cmd.CommandText = "select CuName1 from Cust where CuNo =@CuNo";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            CuName1.Text = reader["CuName1"].ToString();
                        else
                        {
                            CuNo.Text = "";
                            CuName1.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void writeToTxt(DataRow row)
        {
            if (row != null)
            {
                ScNo.Text = row["ScNo"].ToString();
                ScName.Text = row["ScName"].ToString();
                ScPass.Text = row["ScPass"].ToString();
                ScName1.Text = row["ScName1"].ToString();
                CuNo.Text = row["CuNo"].ToString();
                StNo.Text = row["StNo"].ToString();
                X5No.Text = row["X5No"].ToString();
                loadStock(row["StNo"].ToString());
                loadXX05(row["X5No"].ToString());

                ScInvoic2.Text = row["ScInvoic2"].ToString();
                ScInvoic2e.Text = row["ScInvoic2e"].ToString();
                ScInvoic3.Text = row["ScInvoic3"].ToString();
                ScInvoic3e.Text = row["ScInvoic3e"].ToString();

                ScInvoicA.Text = row["ScInvoicA"].ToString();
                ScInvoicAe.Text = row["ScInvoicAe"].ToString();
                ScInvoicA3.Text = row["ScInvoicA3"].ToString();
                ScInvoicA3e.Text = row["ScInvoicA3e"].ToString();
                
                SearchCount.Text = row["SearchCount"].ToString();
                InvSalePort.Text = row["InvSalePort"].ToString();
                User_Einv.Text = row["User_Einv"].ToString();
                sc_MachineSet.Text = row["sc_MachineSet"].ToString();
                //日期格式
                switch (row["ScDatepic"].ToString())
                {
                    case "1": Date1.Checked = true; break;
                    case "2": Date2.Checked = true; break;
                    default: break;
                }
                //分頁一(左)
                switch (row["ScInvSlt"].ToString())
                {
                    case "1": ScInvSlt_1.Checked = true; break;
                    case "2": ScInvSlt_2.Checked = true; break;
                    default: break;
                }
                switch (row["ScInvBat"].ToString())
                {
                    case "1": ScInvBat_1.Checked = true; break;
                    case "2": ScInvBat_2.Checked = true; break;
                    default: break;
                }
                //分頁一(右)
                switch (row["ScInvDev"].ToString())
                {
                    case "1": ScInvDev_1.Checked = true; break;
                    case "2": ScInvDev_2.Checked = true; break;
                    case "3": ScInvDev_3.Checked = true; break;
                    default: break;
                }
                switch (row["ScInvDevs"].ToString())
                {
                    case "1": ScInvDevs_1.Checked = true; break;
                    case "2": ScInvDevs_2.Checked = true; break;
                    case "3": ScInvDevs_3.Checked = true; break;
                    case "4": ScInvDevs_4.Checked = true; break;
                    default: break;
                }
                switch (row["ScPriDev"].ToString())
                {
                    case "1": ScPriDev_1.Checked = true; break;
                    case "2": ScPriDev_2.Checked = true; break;
                    case "3": ScPriDev_3.Checked = true; break;
                    default: break;
                }
                switch (row["CloseMgr"].ToString())
                {
                    case "1": CloseMgr_1.Checked = true; break;
                    case "2": CloseMgr_2.Checked = true; break;
                    default: break;
                }
                switch (row["ScCost"].ToString())
                {
                    case "1": ScCost_1.Checked = true; break;
                    case "2": ScCost_2.Checked = true; break;
                    default: break;
                }
                switch (row["ScBshopMny"].ToString())
                {
                    case "1": ScBshopMny_1.Checked = true; break;
                    case "2": ScBshopMny_2.Checked = true; break;
                    default: break;
                }
                switch (row["language"].ToString())
                {
                    case "0": cmbLanguage.SelectedItem = cmbLanguage.Items[0]; break;
                    case "1": cmbLanguage.SelectedItem = cmbLanguage.Items[0]; break;
                    default: break;
                }
                switch (row["saleprice"].ToString())
                {
                    case "1": salePrice1.Checked = true; break;
                    case "2": salePrice2.Checked = true; break;
                    default: break;
                }
                switch (row["IsShowKeyBoard"].ToString())
                {
                    case "1": keyBoard1.Checked = true; break;
                    case "2": keyBoard2.Checked = true; break;
                    default: break;
                }
                switch (row["IsStopInvPrint"].ToString())
                {
                    case "1": StopInvPrint1.Checked = true; break;
                    case "2": StopInvPrint2.Checked = true; break;
                    default: break;
                }
                switch (row["StopInvMode"].ToString())
                {
                    case "1": stop1.Checked = true; break;
                    case "2": stop2.Checked = true; break;
                    default: break;
                }
                switch (row["CanEditPOS"].ToString())
                {
                    case "1": CanEditPOS1.Checked = true; break;
                    case "2": CanEditPOS2.Checked = true; break;
                    default: break;
                }
                switch (row["CanEditX3No"].ToString())
                {
                    case "1": CanEditX3No1.Checked = true; break;
                    case "2": CanEditX3No2.Checked = true; break;
                    default: break;
                }
                switch (row["CanCelPrompt"].ToString())
                {
                    case "1": CanCelPrompt1.Checked = true; break;
                    case "2": CanCelPrompt2.Checked = true; break;
                    default: break;
                }
                switch (row["einvuse"].ToString())//是否使用電子發票
                {
                    case "1": EInvUse1.Checked = true; break;
                    case "2": EInvUse2.Checked = true; break;
                    default: break;
                }
                switch (row["ischeck"].ToString())//主管覆核
                {
                    case "1": IsCheck1.Checked = true; break;
                    case "2": IsCheck2.Checked = true; break;
                    default: break;
                }

                loadScritD(ScNo.Text.Trim());
                loadCust(CuNo.Text.Trim());
                if (User_Einv.Text.Length > 0)
                {
                    iTitle.Text = Common.iTitle;
                    iStore.Text = Common.iStore;
                    iUnno.Text = Common.iUnno;
                    iTaxNo.Text = Common.iTaxNo;
                    iTel.Text = Common.iTel;
                    iAddress.Text = Common.iAddress;
                    iMemo1.Text = Common.iMemo1;
                    iMemo2.Text = Common.iMemo2;
                }
                else
                {
                    iTitle.Text = row["iTitle"].ToString();
                    iStore.Text = row["iStore"].ToString();
                    iUnno.Text = row["iUnno"].ToString();
                    iTaxNo.Text = row["iTaxNo"].ToString();
                    iTel.Text = row["iTel"].ToString();
                    iAddress.Text = row["iAddress"].ToString();
                    iMemo1.Text = row["iMemo1"].ToString();
                    iMemo2.Text = row["iMemo2"].ToString();
                }
            }
            else
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                dtScritd.Clear();
            }
        }

        DataRow getCurrentDataRow()
        {
            return li.Find(o => o.Field<string>("ScName") == (ScName.Text.Trim()));
        }

        DataRow getCurrentDataRow(string name)
        {
            return li.Find(o => o.Field<string>("ScName") == (name));
        }





        //功能按鈕
        private void btnTop_Click(object sender, EventArgs e)
        {
            loadDB();
            if (li.Count > 0)
            {
                dr = li.First();
                writeToTxt(dr);
            }
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            tempNo = ScName.Text.Trim();
            dr = getCurrentDataRow();
            temp = li.IndexOf(dr);
            loadDB();
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
                        return;
                    }
                    else
                    {
                        dr = li[--temp];
                        writeToTxt(dr);
                        return;
                    }
                }
                if (i > 0)
                {
                    dr = li[--i];
                    writeToTxt(dr);
                }
                else
                {
                    dr = li.First();
                    writeToTxt(dr);
                    MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tempNo = ScName.Text.Trim();
            dr = getCurrentDataRow();
            temp = li.IndexOf(dr);
            loadDB();
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
                        return;
                    }
                    else
                    {
                        dr = li[++i];
                        writeToTxt(dr);
                        return;
                    }
                }
                if (i < li.Count - 1)
                {
                    dr = li[++i];
                    writeToTxt(dr);
                }
                else
                {
                    dr = li.Last();
                    writeToTxt(dr);
                    MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            tempNo = ScName.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);

            btnState = "Append";
            ScName.Focus();

            dataGridViewT1.Tag = "T";
            SearchCount.Text = "500";
            setTxtWhenAppend();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            tempNo = ScName.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);

            btnState = ((Button)sender).Name.Substring(3);

            ScInvoic2.Clear();
            ScInvoic2e.Clear();
            ScInvoic3.Clear();
            ScInvoic3e.Clear();

            ScInvoicA.Clear();
            ScInvoicAe.Clear();
            ScInvoicA3.Clear();
            ScInvoicA3e.Clear();

            ScName.Text = "";
            ScPass.Text = "";
            ScName1.Text = "";
            ScName.Focus();

            dataGridViewT1.Tag = "T";
            SearchCount.Text = "500";
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (ScName.Text == "") return;
            loadDB();
            if (dt.Rows.Count > 0)
            {
                dr = li.Find(r => r.Field<string>("ScName") == ScName.Text);
                writeToTxt(dr);
            }

            tempNo = ScName.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);

            btnState = "Modify";
            ScName.Focus();
            ScName.SelectAll();

            dataGridViewT1.Tag = "T";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ScName.Text == "") return;
            if (ScName.Text == Common.User_Name)
            {
                MessageBox.Show("不能刪除自己帳號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ScName.Text == "BM")
            {
                MessageBox.Show("此帳號為系統管理員，禁止刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnState = "Delete";
            var isDelete = false;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cn.Open();
                    tran = cn.BeginTransaction();
                    cmd.Transaction = tran;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ScNo", ScNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@ScName", ScName.Text.Trim());

                    cmd.CommandText = "delete from scritd where scno=@ScNo";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "delete from scrit where ScName=@ScName";
                    cmd.ExecuteNonQuery();

                    tran.Commit();
                    tran.Dispose();
                    isDelete = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }
            if (isDelete == false) return;

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
            }
        }

        void btnok()
        {
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            if (ScName.Text.Trim() == "" || ScPass.Text.Trim() == "")//儲存或修改時，ScName不能為空值
            {
                MessageBox.Show("『帳號』、『密碼』不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ScName.Focus();
                btnok();
                return;
            }
            if (StNo.Text.Trim() == "")
            {
                MessageBox.Show("倉庫編號不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNo.Focus();
                btnok();
                return;
            }

            if (ScInvDev_1.Checked == true && ScPriDev_1.Checked == true)
            {
                MessageBox.Show("發票機和價格顯示器都是COM1，請修改！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnok();
                return;
            }
            if (ScInvDev_2.Checked == true && ScPriDev_2.Checked == true)
            {
                MessageBox.Show("發票機和價格顯示器都是COM2，請修改！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnok();
                return;
            }

            if (btnState == "Append" || btnState == "Duplicate")
            {
                loadDB();
                dr = getCurrentDataRow();
                int index = li.IndexOf(dr);
                if (index != -1)
                {
                    MessageBox.Show("此帳號已經重複,請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ScName.Text = "";
                    ScName.Focus();
                    btnok();
                    return;
                }

                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    try
                    {

                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        tran = conn.BeginTransaction();
                        cmd.Transaction = tran;
                        //get ScNo
                        cmd.Parameters.AddWithValue("FilterScno",Date.GetDateTime(1, false));
                        cmd.CommandText = "select lastScno = isnull(substring(max(scno),len(max(scno))-3,4),'0000') from scrit where scno like @FilterScno+'%'";
                        string ScNo = Date.GetDateTime(1, false) + (cmd.ExecuteScalar().ToInteger() + 1).ToString().PadLeft(4,'0');

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@scno", ScNo.Trim());
                        cmd.Parameters.AddWithValue("@scname", ScName.Text.Trim());
                        cmd.Parameters.AddWithValue("@scpass", ScPass.Text.Trim());
                        cmd.Parameters.AddWithValue("@scname1", ScName1.Text.Trim());
                        cmd.Parameters.AddWithValue("@scsys", "0");
                        cmd.Parameters.AddWithValue("@scdatepic", getRadioNumber(pDate));
                        cmd.Parameters.AddWithValue("@cono", "");
                        cmd.Parameters.AddWithValue("@stno", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@scmain", "0");
                        cmd.Parameters.AddWithValue("@scsubp", "0");
                        cmd.Parameters.AddWithValue("@scsuchk", "0");
                        cmd.Parameters.AddWithValue("@scsalmny", "0");
                        cmd.Parameters.AddWithValue("@scbshopmny", getRadioNumber(pBuyPrice));
                        cmd.Parameters.AddWithValue("@sccost", getRadioNumber(pCost));
                        cmd.Parameters.AddWithValue("@scinvdev", getRadioNumber(pInvDev));
                        cmd.Parameters.AddWithValue("@scinvdevs", getRadioNumber(pInvDevs));
                        cmd.Parameters.AddWithValue("@scpridev", getRadioNumber(pPriceDev));
                        cmd.Parameters.AddWithValue("@scinvoic2", ScInvoic2.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoic2e", ScInvoic2e.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoic3", ScInvoic3.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoic3e", ScInvoic3e.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoica", ScInvoicA.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoicae", ScInvoicAe.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoica3", ScInvoicA3.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoica3e", ScInvoicA3e.Text.Trim());
                        cmd.Parameters.AddWithValue("@User_Einv", User_Einv.Text.Trim());//電子發票設定
                        cmd.Parameters.AddWithValue("@einvuse", getRadioNumber(pScuseInv));//是否有使用電子發票
                        cmd.Parameters.AddWithValue("@scinvslt", getRadioNumber(pInvSlt));
                        cmd.Parameters.AddWithValue("@scinvbat", getRadioNumber(pInvBat));
                        cmd.Parameters.AddWithValue("@scpath1", "0");
                        cmd.Parameters.AddWithValue("@scpath2", "0");
                        cmd.Parameters.AddWithValue("@scpath3", "0");
                        cmd.Parameters.AddWithValue("@scpath4", "0");
                        cmd.Parameters.AddWithValue("@scsetcno", "0");
                        cmd.Parameters.AddWithValue("@searchcount", SearchCount.Text.Trim());
                        cmd.Parameters.AddWithValue("@x5no", X5No.Text.Trim());
                        cmd.Parameters.AddWithValue("@CuNo", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@saleprice", getRadioNumber(pSalePrice));
                        cmd.Parameters.AddWithValue("@language", getCmboxNumber());
                        cmd.Parameters.AddWithValue("@CloseMgr", getRadioNumber(pCloseMgr));
                        cmd.Parameters.AddWithValue("@iTitle", iTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@iStore", iStore.Text.Trim());
                        cmd.Parameters.AddWithValue("@iUnno", iUnno.Text.Trim());
                        cmd.Parameters.AddWithValue("@iTaxNo", iTaxNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@iTel", iTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@iAddress", iAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@iMemo1", iMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@iMemo2", iMemo2.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsShowKeyBoard", getRadioNumber(pKeyBoard));
                        cmd.Parameters.AddWithValue("@IsStopInvPrint", getRadioNumber(pInvStop));
                        cmd.Parameters.AddWithValue("@StopInvMode", getRadioNumber(pWord));
                        cmd.Parameters.AddWithValue("@CanEditPOS", getRadioNumber(pCanEditPOS));
                        cmd.Parameters.AddWithValue("@CanCelPrompt", getRadioNumber(pCanCelPrompt));
                        cmd.Parameters.AddWithValue("@InvSalePort", InvSalePort.Text.Trim());
                        cmd.Parameters.AddWithValue("@CanEditX3No", getRadioNumber(pCanEditX3No));
                        cmd.Parameters.AddWithValue("@ischeck", getRadioNumber(pIsCheck));
                        cmd.Parameters.AddWithValue("@sc_MachineSet", sc_MachineSet.Text.Trim());//收銀機號

                        cmd.CommandText = @"
                                    INSERT INTO scrit
                                     ([scno],[scname],[scpass],[scname1],[scsys]  
                                     ,[scdatepic],[cono],[stno],[scmain],[scsubp]  
                                     ,[scsuchk],[scsalmny],[scbshopmny],[sccost]  
                                     ,[scinvdev],[scinvdevs],[scpridev],[scinvoic2],[scinvoic2e]  
                                     ,[scinvoic3],[scinvoic3e],[scinvoica],[scinvoicae]  
                                     ,[scinvoica3],[scinvoica3e]  
                                     ,[scinvslt]  
                                     ,[scinvbat],[scpath1],[scpath2],[scpath3],[scpath4]  
                                     ,[scsetcno],[searchcount],[x5no],[CuNo],[saleprice],[language],[CloseMgr]  
                                     ,[iTitle],[iStore],[iUnno],[iTaxNo],[iTel],[iAddress],[iMemo1],[iMemo2],[IsShowKeyBoard]  
                                     ,[IsStopInvPrint],[StopInvMode],[CanEditPOS],[CanCelPrompt],[InvSalePort],[CanEditX3No]  
                                     ,[User_Einv],[einvuse],[ischeck],[sc_MachineSet]) 
                                    VALUES   
                                     ((@scno),(@scname),(@scpass),(@scname1),(@scsys)  
                                     ,(@scdatepic),(@cono),(@stno),(@scmain),(@scsubp)  
                                     ,(@scsuchk),(@scsalmny),(@scbshopmny),(@sccost)  
                                     ,(@scinvdev),(@scinvdevs),(@scpridev)  
                                     ,(@scinvoic2),(@scinvoic2e)  
                                     ,(@scinvoic3),(@scinvoic3e)  
                                     ,(@scinvoica),(@scinvoicae)  
                                     ,(@scinvoica3),(@scinvoica3e)  
                                     ,(@scinvslt)  
                                     ,(@scinvbat),(@scpath1),(@scpath2),(@scpath3),(@scpath4)  
                                     ,(@scsetcno),(@searchcount),(@x5no),(@CuNo),(@saleprice),(@language),(@CloseMgr)  
                                     ,(@iTitle),(@iStore),(@iUnno)  
                                     ,(@iTaxNo),(@iTel),(@iAddress),(@iMemo1),(@iMemo2),(@IsShowKeyBoard)  
                                     ,(@IsStopInvPrint),(@StopInvMode),(@CanEditPOS),(@CanCelPrompt),(@InvSalePort),(@CanEditX3No)  
                                     ,(@User_Einv),(@einvuse),(@ischeck),(@sc_MachineSet)) ;";
                        cmd.ExecuteNonQuery();

                        if (btnState == "Append")
                        {
                            for (int i = 0; i < dtDefault.Rows.Count; i++)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@scno", ScNo.Trim());
                                cmd.Parameters.AddWithValue("@tano", dtDefault.Rows[i]["tano"]);
                                cmd.Parameters.AddWithValue("@taname", dtDefault.Rows[i]["taname"]);
                                cmd.Parameters.AddWithValue("@taform", dtDefault.Rows[i]["taform"]);
                                cmd.Parameters.AddWithValue("@sc01", dataGridViewT1.Rows[i].Cells["SC01"].Value);
                                cmd.Parameters.AddWithValue("@sc02", dataGridViewT1.Rows[i].Cells["SC02"].Value);
                                cmd.Parameters.AddWithValue("@sc03", dataGridViewT1.Rows[i].Cells["SC03"].Value);
                                cmd.Parameters.AddWithValue("@sc04", dataGridViewT1.Rows[i].Cells["SC04"].Value);
                                cmd.Parameters.AddWithValue("@sc05", dataGridViewT1.Rows[i].Cells["SC05"].Value);
                                cmd.Parameters.AddWithValue("@sc06", dataGridViewT1.Rows[i].Cells["SC06"].Value);
                                cmd.Parameters.AddWithValue("@sc07", dataGridViewT1.Rows[i].Cells["SC07"].Value);
                                cmd.Parameters.AddWithValue("@sc08", dataGridViewT1.Rows[i].Cells["SC08"].Value);
                                cmd.Parameters.AddWithValue("@sc09", dataGridViewT1.Rows[i].Cells["SC09"].Value);

                                cmd.CommandText = "insert into scritd(scno,tano,taname,taform,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09)values("
                                    + "(@scno),"
                                    + "(@tano),(@taname),(@taform),"
                                    + "(@sc01),(@sc02),(@sc03),(@sc04),(@sc05),(@sc06),(@sc07),(@sc08),(@sc09))";
                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (btnState == "Duplicate")
                        {
                            for (int i = 0; i < dtScritd.Rows.Count; i++)
                            {

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@scno", ScNo.Trim());
                                cmd.Parameters.AddWithValue("@tano", dtScritd.Rows[i]["tano"]);
                                cmd.Parameters.AddWithValue("@taname", dtScritd.Rows[i]["taname"]);
                                cmd.Parameters.AddWithValue("@taform", dtScritd.Rows[i]["taform"]);
                                cmd.Parameters.AddWithValue("@sc01", dataGridViewT1.Rows[i].Cells["SC01"].Value);
                                cmd.Parameters.AddWithValue("@sc02", dataGridViewT1.Rows[i].Cells["SC02"].Value);
                                cmd.Parameters.AddWithValue("@sc03", dataGridViewT1.Rows[i].Cells["SC03"].Value);
                                cmd.Parameters.AddWithValue("@sc04", dataGridViewT1.Rows[i].Cells["SC04"].Value);
                                cmd.Parameters.AddWithValue("@sc05", dataGridViewT1.Rows[i].Cells["SC05"].Value);
                                cmd.Parameters.AddWithValue("@sc06", dataGridViewT1.Rows[i].Cells["SC06"].Value);
                                cmd.Parameters.AddWithValue("@sc07", dataGridViewT1.Rows[i].Cells["SC07"].Value);
                                cmd.Parameters.AddWithValue("@sc08", dataGridViewT1.Rows[i].Cells["SC08"].Value);
                                cmd.Parameters.AddWithValue("@sc09", dataGridViewT1.Rows[i].Cells["SC09"].Value);

                                cmd.CommandText = "insert into scritd(scno,tano,taname,taform,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09)values("
                                    + "(@scno),"
                                    + "(@tano),(@taname),(@taform),"
                                    + "(@sc01),(@sc02),(@sc03),(@sc04),(@sc05),(@sc06),(@sc07),(@sc08),(@sc09))";
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();

                        tempNo = ScName.Text.Trim();
                        foreach (TextBox tb in this.Controls.OfType<TextBox>())
                        {
                            tb.Clear();
                        }
                        ScName.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        tran.Rollback();
                    }
                    finally
                    {
                        btnSave.Enabled = true;
                        btnCancel.Enabled = true;
                    }
                }
            }
            if (btnState == "Modify")
            {
                loadDB();
                dr = getCurrentDataRow();
                int i = li.IndexOf(dr);
                if (i == -1)
                {
                    MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ScName.Text = "";
                    ScName.Focus();
                    btnok();
                    return;
                }
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        tran = cn.BeginTransaction();
                        cmd.Transaction = tran;
                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@scname", ScName.Text.Trim());
                        cmd.Parameters.AddWithValue("@scpass", ScPass.Text.Trim());
                        cmd.Parameters.AddWithValue("@scname1", ScName1.Text.Trim());
                        cmd.Parameters.AddWithValue("@scdatepic", getRadioNumber(pDate));
                        cmd.Parameters.AddWithValue("@stno", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@scbshopmny", getRadioNumber(pBuyPrice));
                        cmd.Parameters.AddWithValue("@sccost", getRadioNumber(pCost));
                        cmd.Parameters.AddWithValue("@scinvdev", getRadioNumber(pInvDev));
                        cmd.Parameters.AddWithValue("@scinvdevs", getRadioNumber(pInvDevs));
                        cmd.Parameters.AddWithValue("@scpridev", getRadioNumber(pPriceDev));
                        cmd.Parameters.AddWithValue("@scinvoic2", ScInvoic2.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoic2e", ScInvoic2e.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoic3", ScInvoic3.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoic3e", ScInvoic3e.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoica", ScInvoicA.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoicae", ScInvoicAe.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoica3", ScInvoicA3.Text.Trim());
                        cmd.Parameters.AddWithValue("@scinvoica3e", ScInvoicA3e.Text.Trim());
                        cmd.Parameters.AddWithValue("@User_Einv", User_Einv.Text.Trim());//電子發票設定
                        cmd.Parameters.AddWithValue("@einvuse", getRadioNumber(pScuseInv));//是否有使用電子發票
                        cmd.Parameters.AddWithValue("@scinvslt", getRadioNumber(pInvSlt));
                        cmd.Parameters.AddWithValue("@scinvbat", getRadioNumber(pInvBat));
                        cmd.Parameters.AddWithValue("@searchcount", SearchCount.Text.Trim());
                        cmd.Parameters.AddWithValue("@x5no", X5No.Text.Trim());
                        cmd.Parameters.AddWithValue("@CuNo", CuNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@saleprice", getRadioNumber(pSalePrice));
                        cmd.Parameters.AddWithValue("@language", getCmboxNumber());
                        cmd.Parameters.AddWithValue("@CloseMgr", getRadioNumber(pCloseMgr));
                        cmd.Parameters.AddWithValue("@iTitle", iTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@iStore", iStore.Text.Trim());
                        cmd.Parameters.AddWithValue("@iUnno", iUnno.Text.Trim());
                        cmd.Parameters.AddWithValue("@iTaxNo", iTaxNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@iTel", iTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@iAddress", iAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@iMemo1", iMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@iMemo2", iMemo2.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsShowKeyBoard", getRadioNumber(pKeyBoard));
                        cmd.Parameters.AddWithValue("@IsStopInvPrint", getRadioNumber(pInvStop));
                        cmd.Parameters.AddWithValue("@StopInvMode", getRadioNumber(pWord));
                        cmd.Parameters.AddWithValue("@CanEditPOS", getRadioNumber(pCanEditPOS));
                        cmd.Parameters.AddWithValue("@CanCelPrompt", getRadioNumber(pCanCelPrompt));
                        cmd.Parameters.AddWithValue("@InvSalePort", InvSalePort.Text.Trim());
                        cmd.Parameters.AddWithValue("@CanEditX3No", getRadioNumber(pCanEditX3No));
                        cmd.Parameters.AddWithValue("@ischeck", getRadioNumber(pIsCheck));
                        cmd.Parameters.AddWithValue("@sc_MachineSet", sc_MachineSet.Text.Trim());//收銀機號

                        cmd.CommandText = "UPDATE scrit SET "
                            + "scpass = @scpass"
                            + ",scname1 = @scname1"
                            + ",scdatepic = @scdatepic"
                            + ",x5no = @x5no"
                            + ",CuNo = @CuNo"
                            + ",stno = @stno"
                            + ",searchcount = @searchcount"
                            + ",scbshopmny = @scbshopmny"
                            + ",sccost = @sccost"
                            + ",scinvdev = @scinvdev"
                            + ",scinvdevs = @scinvdevs"
                            + ",scpridev = @scpridev"
                            + ",scinvoic2 = @scinvoic2"
                            + ",scinvoic2e = @scinvoic2e"
                            + ",scinvoic3 = @scinvoic3"
                            + ",scinvoic3e = @scinvoic3e"
                            + ",scinvoica = @scinvoica"
                            + ",scinvoicae = @scinvoicae"
                            + ",scinvoica3 = @scinvoica3"
                            + ",scinvoica3e = @scinvoica3e"
                            + ",iTitle = @iTitle"
                            + ",iStore = @iStore"
                            + ",iUnno = @iUnno"
                            + ",iTaxNo = @iTaxNo"
                            + ",iTel = @iTel"
                            + ",iAddress = @iAddress"
                            + ",iMemo1 = @iMemo1"
                            + ",iMemo2 = @iMemo2"
                            + ",scinvslt = @scinvslt"
                            + ",scinvbat = @scinvbat"
                            + ",saleprice = @saleprice"
                            + ",language = @language"
                            + ",CloseMgr = @CloseMgr"
                            + ",IsShowKeyBoard = @IsShowKeyBoard"
                            + ",IsStopInvPrint = @IsStopInvPrint"
                            + ",StopInvMode = @StopInvMode"
                            + ",CanEditPOS = @CanEditPOS"
                            + ",CanCelPrompt = @CanCelPrompt"
                            + ",InvSalePort = @InvSalePort"
                            + ",CanEditX3No = @CanEditX3No"
                            + ",User_Einv = @User_Einv"
                            + ",einvuse = @einvuse"//是否有使用電子發票
                            + ",ischeck = @ischeck"
                            + ",sc_MachineSet = @sc_MachineSet"
                            + " where ScName=@scname";
                        cmd.ExecuteNonQuery();

                        for (int k = 0; k < dtScritd.Rows.Count; k++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@scno", dtScritd.Rows[0]["ScNo"].ToString());
                            cmd.Parameters.AddWithValue("@taname", dataGridViewT1.Rows[k].Cells["taname"].Value);
                            cmd.Parameters.AddWithValue("@sc01", dataGridViewT1.Rows[k].Cells["SC01"].Value);
                            cmd.Parameters.AddWithValue("@sc02", dataGridViewT1.Rows[k].Cells["SC02"].Value);
                            cmd.Parameters.AddWithValue("@sc03", dataGridViewT1.Rows[k].Cells["SC03"].Value);
                            cmd.Parameters.AddWithValue("@sc04", dataGridViewT1.Rows[k].Cells["SC04"].Value);
                            cmd.Parameters.AddWithValue("@sc05", dataGridViewT1.Rows[k].Cells["SC05"].Value);
                            cmd.Parameters.AddWithValue("@sc06", dataGridViewT1.Rows[k].Cells["SC06"].Value);
                            cmd.Parameters.AddWithValue("@sc07", dataGridViewT1.Rows[k].Cells["SC07"].Value);
                            cmd.Parameters.AddWithValue("@sc08", dataGridViewT1.Rows[k].Cells["SC08"].Value);
                            cmd.Parameters.AddWithValue("@sc09", dataGridViewT1.Rows[k].Cells["SC09"].Value);

                            cmd.CommandText = "update scritd set "
                                + "sc01 =@sc01"
                                + ",sc02 =@sc02"
                                + ",sc03 =@sc03"
                                + ",sc04 =@sc04"
                                + ",sc05 =@sc05"
                                + ",sc06 =@sc06"
                                + ",sc07 =@sc07"
                                + ",sc08 =@sc08"
                                + ",sc09 =@sc09"
                                + " where scno=@scno"
                                + "  and taname =@taname";
                            cmd.ExecuteNonQuery();
                        }
                        tran.Commit(); tran.Dispose();
                        cmd.Dispose();

                        tempNo = ScName.Text.Trim();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    btnok();
                    MainForm.main.loadUserSetting();
                }
            }

            btnCancel_Click(null, null);
            btnExit.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = string.Empty;
            Common.SetTextState(FormState = FormEditState.None, ref list);
            dataGridViewT1.Tag = "F";

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
                }
            }
            else
            {
                writeToTxt(dr);
            }
            btnAppend.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }




        private string changeDataGridView(object obj)
        {
            return obj.ToString() == "V" ? "" : "V";
        }

        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (dataGridViewT1.Tag.ToString() == ("F")) return;
            if (dataGridViewT1.CurrentCell.OwningColumn.HeaderText.Trim() == "作業名稱") return;
            if (ScName.Text.Trim() == "BM" && e.RowIndex != -1 && e.RowIndex < dataGridViewT1.Rows.Count)
            {
                if (dataGridViewT1["taname", e.RowIndex].Value.ToString() == "使用者參數設定")
                {
                    MessageBox.Show("此項目為系統管理員權限，無法異動", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            switch (dataGridViewT1.CurrentCell.OwningColumn.Name)
            {
                case "SC01":
                case "SC02":
                case "SC03":
                case "SC04":
                case "SC05":
                case "SC06":
                case "SC07":
                    if (dataGridViewT1.CurrentCell.Value.Equals("V"))
                        dataGridViewT1.CurrentCell.Value = "";
                    else
                        dataGridViewT1.CurrentCell.Value = "V";
                    if (dataGridViewT1["SC01", dataGridViewT1.CurrentRow.Index].Value.ToString() == "V" &&
                        dataGridViewT1["SC02", dataGridViewT1.CurrentRow.Index].Value.ToString() == "V" &&
                        dataGridViewT1["SC03", dataGridViewT1.CurrentRow.Index].Value.ToString() == "V" &&
                        dataGridViewT1["SC04", dataGridViewT1.CurrentRow.Index].Value.ToString() == "V" &&
                        dataGridViewT1["SC05", dataGridViewT1.CurrentRow.Index].Value.ToString() == "V" &&
                        dataGridViewT1["SC06", dataGridViewT1.CurrentRow.Index].Value.ToString() == "V" &&
                        dataGridViewT1["SC07", dataGridViewT1.CurrentRow.Index].Value.ToString() == "V")
                    {
                        dataGridViewT1["SC08", dataGridViewT1.CurrentRow.Index].Value = "V";
                        dataGridViewT1["SC09", dataGridViewT1.CurrentRow.Index].Value = "";
                    }
                    else if (dataGridViewT1["SC01", dataGridViewT1.CurrentRow.Index].Value.ToString() == "" &&
                        dataGridViewT1["SC02", dataGridViewT1.CurrentRow.Index].Value.ToString() == "" &&
                        dataGridViewT1["SC03", dataGridViewT1.CurrentRow.Index].Value.ToString() == "" &&
                        dataGridViewT1["SC04", dataGridViewT1.CurrentRow.Index].Value.ToString() == "" &&
                        dataGridViewT1["SC05", dataGridViewT1.CurrentRow.Index].Value.ToString() == "" &&
                        dataGridViewT1["SC06", dataGridViewT1.CurrentRow.Index].Value.ToString() == "" &&
                        dataGridViewT1["SC07", dataGridViewT1.CurrentRow.Index].Value.ToString() == "")
                    {
                        dataGridViewT1["SC08", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC09", dataGridViewT1.CurrentRow.Index].Value = "V";
                    }
                    else
                    {
                        dataGridViewT1["SC08", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC09", dataGridViewT1.CurrentRow.Index].Value = "";
                    }
                    break;
                case "SC08":
                    if (dataGridViewT1.CurrentCell.Value.Equals(""))
                    {
                        dataGridViewT1["SC01", dataGridViewT1.CurrentRow.Index].Value = "V";
                        dataGridViewT1["SC02", dataGridViewT1.CurrentRow.Index].Value = "V";
                        dataGridViewT1["SC03", dataGridViewT1.CurrentRow.Index].Value = "V";
                        dataGridViewT1["SC04", dataGridViewT1.CurrentRow.Index].Value = "V";
                        dataGridViewT1["SC05", dataGridViewT1.CurrentRow.Index].Value = "V";
                        dataGridViewT1["SC06", dataGridViewT1.CurrentRow.Index].Value = "V";
                        dataGridViewT1["SC07", dataGridViewT1.CurrentRow.Index].Value = "V";
                        dataGridViewT1["SC08", dataGridViewT1.CurrentRow.Index].Value = "V";
                        dataGridViewT1["SC09", dataGridViewT1.CurrentRow.Index].Value = "";
                    }
                    break;
                case "SC09":
                    if (dataGridViewT1.CurrentCell.Value.Equals(""))
                    {
                        dataGridViewT1["SC01", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC02", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC03", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC04", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC05", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC06", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC07", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC08", dataGridViewT1.CurrentRow.Index].Value = "";
                        dataGridViewT1["SC09", dataGridViewT1.CurrentRow.Index].Value = "V";
                    }
                    break;
            }
        }

        private void setTxtWhenAppend()
        {
            //新增一筆資料時,載入欄位預設值
            StNo.Text = Common.User_StkNo;
            loadStock(Common.User_StkNo);

            ScInvSlt_1.Checked = true;
            ScInvBat_1.Checked = true;
            ScInvDev_3.Checked = true;
            ScInvDevs_4.Checked = true;
            ScPriDev_3.Checked = true;
            CloseMgr_2.Checked = true;
            ScCost_2.Checked = true;
            ScBshopMny_1.Checked = true;
            salePrice1.Checked = true;
            stop2.Checked = true;
            IsCheck2.Checked = true;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from scritd where scno =N'default'", cn))
                {
                    dtDefault.Clear();
                    da.Fill(dtDefault);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dataGridViewT1.DataSource = null;
            dataGridViewT1.DataSource = dtDefault;
        }

        private string getRadioNumber(PanelNT pnl)
        {
            var rd = pnl.Controls.OfType<RadioT>().FirstOrDefault(r => r.Checked);
            return rd == null ? "0" : rd.Name.Last().ToString();
        }

        private string getCmboxNumber()
        {
            string str = "";
            switch (cmbLanguage.SelectedText)
            {
                case "繁體中文": str = "0"; break;
                case "簡体中文": str = "1"; break;
                default: str = "0"; break;
            }
            return str;
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    btnAppend.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    btnModify.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    btnDelete.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    btnExit.PerformClick();
                    break;
                case Keys.Home:
                    btnTop.PerformClick();
                    break;
                case Keys.PageUp:
                    btnPrior.PerformClick();
                    break;
                case Keys.PageDown:
                    btnNext.PerformClick();
                    break;
                case Keys.End:
                    btnBottom.PerformClick();
                    break;
                case Keys.F9:
                    btnSave.PerformClick();
                    break;
                case Keys.F4:
                    btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (StNo.Text.Trim() == "")
            {
                e.Cancel = true;
                StNo.Text = StName.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }

        private void ScName_Validating(object sender, CancelEventArgs e)
        {
            if (ScName.ReadOnly) return;
            if (btnCancel.Focused) return;
            if (ScName.Text.Trim().ToLower() == "default")
            {
                e.Cancel = true;
                ScName.SelectAll();
                MessageBox.Show("此帳號為預設用，請重新設定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                bool flag = false;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ScName", ScName.Text.Trim());
                    cmd.CommandText = "select scname from scrit where scname=@ScName";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        flag = reader.Read();
                    }
                }
                if (flag)
                {
                    if (btnState == "Append" || btnState == "Duplicate")
                    {
                        e.Cancel = true;
                        ScName.SelectAll();
                        MessageBox.Show("此帳號重複，請重新設定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (btnState == "Modify")
                    {
                        if (beforeScName != ScName.Text.Trim())
                        {
                            dr = getCurrentDataRow(ScName.Text.Trim());
                            writeToTxt(dr);
                        }
                    }
                }
                else
                {
                    if (btnState == "Modify")
                    {
                        e.Cancel = true;
                        ScName.SelectAll();
                        MessageBox.Show("查無此使用者帳號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ScName_Enter(object sender, EventArgs e)
        {
            beforeScName = ScName.Text.Trim();
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender, reader =>
            {
                CuNo.Text = reader["cuno"].ToString();
                CuName1.Text = reader["cuname1"].ToString();
            });
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (CuNo.Text.Trim().Length == 0)
            {
                CuNo.Text = CuName1.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Cust>(sender, e, reader =>
            {
                CuNo.Text = reader["cuno"].ToString();
                CuName1.Text = reader["cuname1"].ToString();
            }, true);
        }

        private void X5No_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX05>(sender, row =>
            {
                X5No.Text = row["X5No"].ToString().Trim();
                X5Name.Text = row["X5Name"].ToString().Trim();
            });
        }

        private void X5No_Validating(object sender, CancelEventArgs e)
        {
            if (X5No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (X5No.Text.Trim().Length == 0)
            {
                X5No.Text = X5Name.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.XX05>(sender, e, row =>
            {
                X5No.Text = row["X5No"].ToString().Trim();
                X5Name.Text = row["X5Name"].ToString().Trim();
            });

            if (X5No.Text.Trim().ToDecimal() != 3 && X5No.Text.Trim().ToDecimal() != 4 && X5No.Text.Trim().ToDecimal() != 7 && X5No.Text.Trim().ToDecimal() != 8)
            {
                e.Cancel = true;
                X5No.Focus();
                X5No.SelectAll();
                MessageBox.Show("發票模式只能選:" + Environment.NewLine + "3-二聯收銀機" + Environment.NewLine + "4-三聯收銀機" + Environment.NewLine + "7-一般電子發票" + Environment.NewLine + "8-特種電子發", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SearchCount_Validating(object sender, CancelEventArgs e)
        {
            if (SearchCount.ReadOnly) return;
            if (btnCancel.Focused) return;
            if (SearchCount.Text.ToDecimal() <= 1)
            {
                e.Cancel = true;
                MessageBox.Show("瀏覽筆數不可小於1", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (SearchCount.Text.ToDecimal() > 100000000)
            {
                e.Cancel = true;
                MessageBox.Show("瀏覽筆數不可大於100000000", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void ScInvoic2_Validating(object sender, CancelEventArgs e)
        {
            TextBoxT tb = ((TextBoxT)sender);
            if (tb.ReadOnly || btnCancel.Focused) return;
            if (tb.TrimTextLenth() == 0)
            {
                tb.Clear();
                return;
            }

            if (tb.TrimTextLenth() != 10)
            {
                e.Cancel = true;
                MessageBox.Show("發票號碼輸入錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.SelectAll();
                return;
            }
            else
            {
                var str = tb.Text.Trim().ToUpper();
                for (int i = 0; i < 10; i++)
                {
                    if (i < 2)
                    {
                        if (!char.IsUpper(str[i]))
                        {
                            e.Cancel = true;
                            MessageBox.Show("發票號碼輸入錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    }
                    else
                    {
                        if (!char.IsDigit(str[i]))
                        {
                            e.Cancel = true;
                            MessageBox.Show("發票號碼輸入錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    }
                }
                tb.Text = str;
            }
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            pIsCheck.Enabled = pDate.Enabled = pCloseMgr.Enabled = pCost.Enabled = pBuyPrice.Enabled = pSalePrice.Enabled = pInvBat.Enabled = pInvSlt.Enabled = !btnAppend.Enabled;
            pInvDev.Enabled = pInvDevs.Enabled = pPriceDev.Enabled = pKeyBoard.Enabled = pInvStop.Enabled = !btnAppend.Enabled;
            pWord.Enabled = pCanEditPOS.Enabled = pCanEditX3No.Enabled = pCanCelPrompt.Enabled = !btnAppend.Enabled;
        }

        private void InvSalePort_DoubleClick(object sender, EventArgs e)
        {
            if (InvSalePort.ReadOnly)
                return;

            using (var frm = new S_61.S0.FrmPrintb())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    (sender as JE.MyControl.TextBoxT).Text = frm.TResult;
            }
        }

        private void User_Einv_DoubleClick(object sender, EventArgs e)
        {
            if (User_Einv.ReadOnly || btnCancel.Focused)
                return;
            xe.Open<JBS.JS.EINV>(sender, reader =>
            {
                User_Einv.Text = reader["Einvid"].ToString();
            });

        }

        private void User_Einv_Validating(object sender, CancelEventArgs e)
        {
            DataTable Einvdt = new DataTable();
            if (User_Einv.Text.Trim().Length > 0)
            {
                if (User_Einv.ReadOnly) return;
                if (btnCancel.Focused) return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.CommandText = "select * from Einvsetup where Einvid='" + User_Einv.Text + "'";
                    if (cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        User_Einv_DoubleClick(sender, null);
                        e.Cancel = true;
                    }
                    else
                    {
                        Einvdt.Clear();
                        da.Fill(Einvdt);
                        da.Dispose();
                    }
                }
            }
        }

        private void buttonSmallT1_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.S6.FrmInvoSet>("");
        }

        private void buttonSmallT2_Click(object sender, EventArgs e)
        {
            MainForm.menu.OpenForm<S_61.SOther.Einvsetup>("");
        }
    }
}
