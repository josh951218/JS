using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmFact_Receivd : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable dt = new DataTable();
        DataTable dt_Main = new DataTable();
        DataTable dt_Detail = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        public FrmFact_Receivd()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            pVar.FrmFact_Receivd = this;

            switch (Common.User_DateTime)
            {
                case 1:
                    PaDateAcs.MaxLength = 7;
                    PaDateAcs_1.MaxLength = 7;
                    break;
                case 2:
                    PaDateAcs.MaxLength = 8;
                    PaDateAcs_1.MaxLength = 8;
                    break;
            }
            if (Common.Series == "74" || Common.Series == "73")
            {
                radio3.Enabled = false;
            }
        }

        private void FrmFact_Receivd_Load(object sender, EventArgs e)
        {
            switch (Common.User_DateTime)
            {
                case 1:
                    PaDateAcs.MaxLength = 7;
                    PaDateAcs_1.MaxLength = 7;
                    PaDateAcs.Text = Date.GetDateTime(1, false).Substring(0, 5) + "01";
                    PaDateAcs_1.Text = Date.GetDateTime(1, false);
                    break;
                case 2:
                    PaDateAcs.MaxLength = 8;
                    PaDateAcs_1.MaxLength = 8;
                    PaDateAcs.Text = Date.GetDateTime(2, false).Substring(0, 6) + "01";
                    PaDateAcs_1.Text = Date.GetDateTime(2, false);
                    break;
            }
        }

        private void FrmFact_Receivd_Shown(object sender, EventArgs e)
        {
            PaDateAcs.Focus();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            #region 限制
            if (PaDateAcs.Text.Trim() != "" && PaDateAcs_1.Text.Trim() != "")
            {
                if (string.CompareOrdinal(PaDateAcs.Text.Trim(), PaDateAcs_1.Text.Trim()) > 0)
                {
                    MessageBox.Show("起始帳款日期不可大於終止帳款日期，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PaDateAcs.Focus();
                    return;
                }
            }
            if (FaNo.Text.Trim() != "" && FaNo_1.Text.Trim() != "")
            {
                if (string.CompareOrdinal(FaNo.Text.Trim(), FaNo_1.Text.Trim()) > 0)
                {
                    MessageBox.Show("起始廠商編號不可大於終止廠商編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FaNo.Focus();
                    return;
                }
            }
            #endregion

            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.Clear();
                    if (FaNo.Text.Trim() != "") cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
                    if (FaNo_1.Text.Trim() != "") cmd.Parameters.AddWithValue("FaNo1", FaNo_1.Text.Trim());

                    //明細部分
                    string str = "";
                    //明細表 簡要表
                    if (radio1.Checked)
                    {
                        #region when radio1 is checked then set value to str(明細表)
                        str += "select PaNo 付款憑證,SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2) 付款日期民國,SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2) 付款日期西元,FaNo 廠商編號,FaName1 廠商簡稱,TotDisc 折讓金額,CashMny 現金金額,CardMny 刷卡金額,Ticket 禮卷金額,CheckMny 支票金額,RemitMny 匯出金額,OtherMny 其他金額,GetPrvAcc 取用預付,TotReve 沖帳合計,AddPrvAcc 累入預付,TotMny 沖抵帳款 from Payabl where '0'='0'";
                        //起訖帳款日期
                        if (PaDateAcs.Text != "" || PaDateAcs_1.Text != "")
                        {
                            switch (Common.User_DateTime)
                            {
                                case 1:
                                    str += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                    if (PaDateAcs_1.Text != "")
                                        str += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                    break;
                                case 2:
                                    str += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                    if (PaDateAcs_1.Text != "")
                                        str += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                    break;
                            }
                        }
                        //起訖廠商編號
                        if (FaNo.Text.Trim() != "" || FaNo_1.Text.Trim() != "")
                        {
                            str += " and FaNo>=@FaNo";
                            if (FaNo_1.Text.Trim() != "")
                                str += " and FaNo<=@FaNo1";
                        }
                        #endregion
                    }
                    else if (radio2.Checked)
                    {
                        str = "select *,FaName1 廠商簡稱,FaName1 廠商名稱,(select xa1name from Xa01 where Xa1No=Fact.FaXa1no) 幣別 from(";
                        str += "select FaNo 廠商編號,count(*) 沖帳筆數,sum(TotDisc * xa1par) 折讓金額,sum(CashMny * xa1par) 現金金額,sum(CardMny * xa1par) 刷卡金額,sum(Ticket * xa1par) 禮卷金額,sum(CheckMny * xa1par) 支票金額,sum(RemitMny * xa1par) 匯出金額,sum(OtherMny * xa1par) 其他金額,sum(GetPrvAcc * xa1par) 取用預付,sum(TotReve * xa1par) 沖帳合計,sum(AddPrvAcc * xa1par) 累入預付,sum(TotMny * xa1par) 沖抵帳款 from Payabl where '0'='0'";
                        switch (Common.User_DateTime)
                        {
                            case 1:
                                if (PaDateAcs.Text != "")
                                    str += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                            case 2:
                                if (PaDateAcs.Text != "")
                                    str += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                        }
                        str += " group by FaNo) 沖帳總計 left join Fact on 沖帳總計.廠商編號=Fact.FaNo where '0'='0'";

                        //起訖廠商編號
                        if (FaNo.Text != "" || FaNo_1.Text != "")
                        {
                            str += " and Fact.FaNo>=@FaNo";
                            if (FaNo_1.Text != "")
                                str += " and Fact.FaNo<=@FaNo1";
                        }
                    }
                    else
                    {
                        #region  when radio2 or radio3 is checked then set value to str(明細表)
                        str = "select *,FaName1 廠商簡稱,FaName1 廠商名稱,(select xa1name from Xa01 where Xa1No=Fact.FaXa1no) 幣別 from(";
                        str += "select FaNo 廠商編號,count(*) 沖帳筆數,sum(TotDisc) 折讓金額,sum(CashMny) 現金金額,sum(CardMny) 刷卡金額,sum(Ticket) 禮卷金額,sum(CheckMny) 支票金額,sum(RemitMny) 匯出金額,sum(OtherMny) 其他金額,sum(GetPrvAcc) 取用預付,sum(TotReve) 沖帳合計,sum(AddPrvAcc) 累入預付,sum(TotMny) 沖抵帳款 from Payabl where '0'='0'";
                        switch (Common.User_DateTime)
                        {
                            case 1:
                                if (PaDateAcs.Text != "")
                                    str += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                            case 2:
                                if (PaDateAcs.Text != "")
                                    str += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                        }
                        str += " group by FaNo) 沖帳總計 left join Fact on 沖帳總計.廠商編號=Fact.FaNo where '0'='0'";

                        //起訖廠商編號
                        if (FaNo.Text != "" || FaNo_1.Text != "")
                        {
                            str += " and Fact.FaNo>=@FaNo";
                            if (FaNo_1.Text != "")
                                str += " and Fact.FaNo<=@FaNo1";
                        }
                        #endregion
                    }

                    if (radio1.Checked)
                    {
                        cmd.CommandText = str;
                        da = new SqlDataAdapter(cmd);
                        dt_Detail.Clear();
                        da.Fill(dt_Detail);
                        da.Dispose();
                    }

                    //主檔部分
                    string str_Main = "";
                    if (radio1.Checked)
                    {
                        #region  when radio1 is checked then set value to str_Main(主檔)
                        str_Main = "select *,FaName1 廠商簡稱,FaName1 廠商名稱,FaAddr1 地址,FaTel1 電話,FaFax1 傳真,(select xa1name from Xa01 where Xa1No=Fact.FaXa1no) 幣別,Fact.FaPer1 聯絡人,Fact.FaUno 統一編號 from(";
                        str_Main += "select FaNo 廠商編號,count(*) 沖帳總筆數,sum(TotDisc) 折讓總金額,sum(CashMny) 現金總金額,sum(CardMny) 刷卡總金額,sum(Ticket) 禮券總金額,sum(CheckMny) 支票總金額,sum(RemitMny) 匯出總金額,sum(OtherMny) 其他總金額,sum(GetPrvAcc) 取用總金額,sum(TotReve) 沖帳總合計,sum(AddPrvAcc) 累入總金額,sum(TotMny) 沖抵總金額 from Payabl where '0'='0'";
                        switch (Common.User_DateTime)
                        {
                            case 1:
                                if (PaDateAcs.Text != "")
                                    str_Main += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str_Main += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                            case 2:
                                if (PaDateAcs.Text != "")
                                    str_Main += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str_Main += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                        }
                        str_Main += " group by FaNo) 沖帳總計 left join Fact on 沖帳總計.廠商編號=Fact.FaNo where '0'='0'";
                        #endregion
                    }
                    else if (radio2.Checked)
                    {
                        #region  when radio2 or radio3 is checked then set value to str_Main(主檔)
                        str_Main += "select count(*) 沖帳總筆數,sum(TotDisc * xa1par) 折讓總金額,sum(CashMny * xa1par) 現金總金額,sum(CardMny * xa1par) 刷卡總金額,sum(Ticket * xa1par) 禮券總金額,sum(CheckMny * xa1par) 支票總金額,sum(RemitMny * xa1par) 匯出總金額,sum(OtherMny * xa1par) 其他總金額,sum(GetPrvAcc * xa1par) 取用總金額,sum(TotReve * xa1par) 沖帳總合計,sum(AddPrvAcc * xa1par) 累入總金額,sum(TotMny * xa1par) 沖抵總金額 from Payabl where '0'='0'";
                        switch (Common.User_DateTime)
                        {
                            case 1:
                                if (PaDateAcs.Text != "")
                                    str_Main += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str_Main += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                            case 2:
                                if (PaDateAcs.Text != "")
                                    str_Main += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str_Main += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        #region  when radio2 or radio3 is checked then set value to str_Main(主檔)
                        str_Main += "select count(*) 沖帳總筆數,sum(TotDisc) 折讓總金額,sum(CashMny) 現金總金額,sum(CardMny) 刷卡總金額,sum(Ticket) 禮券總金額,sum(CheckMny) 支票總金額,sum(RemitMny) 匯出總金額,sum(OtherMny) 其他總金額,sum(GetPrvAcc) 取用總金額,sum(TotReve) 沖帳總合計,sum(AddPrvAcc) 累入總金額,sum(TotMny) 沖抵總金額 from Payabl where '0'='0'";
                        switch (Common.User_DateTime)
                        {
                            case 1:
                                if (PaDateAcs.Text != "")
                                    str_Main += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str_Main += " and SUBSTRING(PaDate,1,3)+'/'+SUBSTRING(PaDate,4,2)+'/'+SUBSTRING(PaDate,6,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                            case 2:
                                if (PaDateAcs.Text != "")
                                    str_Main += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)>='" + Date.AddLine(PaDateAcs.Text) + "'";
                                if (PaDateAcs_1.Text != "")
                                    str_Main += " and SUBSTRING(PaDate1,1,4)+'/'+SUBSTRING(PaDate1,5,2)+'/'+SUBSTRING(PaDate1,7,2)<='" + Date.AddLine(PaDateAcs_1.Text) + "'";
                                break;
                        }
                        #endregion
                    }
                    //起訖廠商編號
                    if (radio1.Checked)
                    {
                        if (FaNo.Text != "" || FaNo_1.Text != "")
                        {
                            str_Main += " and 沖帳總計.廠商編號>=@FaNo";
                            if (FaNo_1.Text != "")
                                str_Main += " and 沖帳總計.廠商編號<=@FaNo1";
                        }
                    }
                    if (radio2.Checked || radio3.Checked)
                    {
                        str_Main = "select * from (" + str_Main + ") Main right join (" + str + ") Detail on 0=0";
                    }

                    cmd.CommandText = str_Main;
                    da = new SqlDataAdapter(cmd);
                    dt_Main.Clear();
                    da.Fill(dt_Main);
                    da.Dispose();

                    if (radio1.Checked)
                    {
                        string str_all;
                        str_all = "select * from (" + str_Main + ") Main right join (" + str + ") Detail on Main.廠商編號=Detail.廠商編號 left join Payabld on Detail.付款憑證=Payabld.PaNo";
                        cmd.CommandText = str_all;
                        da = new SqlDataAdapter(cmd);
                        dt.Clear();
                        da.Fill(dt);
                        da.Dispose();
                    }
                }

                if (radio1.Checked)
                {
                    #region OpenForm radio1
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //using (FrmFact_Receivd_N1 frm = new FrmFact_Receivd_N1())
                    //{
                    //    frm.dtM = dt;
                    //    frm.dtD = dt_Detail;
                    //    frm.Text = "廠商別已付帳款(明細簡要表)";
                    //    frm.ShowDialog();
                    //}
                    this.OpemInfoFrom<FrmFact_Receivd_N1>(() =>
                            {
                                FrmFact_Receivd_N1 frm = new FrmFact_Receivd_N1();
                                frm.dtM = dt;
                                frm.dtD = dt_Detail;
                                frm.Text = "廠商別已付帳款(明細簡要表)";
                                return frm;
                            });
                    #endregion
                }
                else
                {
                    #region OpenForm radio2 or radio3
                    if (dt_Main.Rows.Count == 0)
                    {
                        MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (radio2.Checked)
                    {
                        //using (FrmFact_Receivd_N2 frm = new FrmFact_Receivd_N2())
                        //{
                        //    frm.dtM = dt_Main;
                        //    frm.Text = "廠商別已付帳款(本幣總額表)";
                        //    frm.ShowDialog();
                        //}
                        this.OpemInfoFrom<FrmFact_Receivd_N2>(() =>
                            {
                                FrmFact_Receivd_N2 frm = new FrmFact_Receivd_N2();
                                frm.dtM = dt_Main;
                                frm.Text = "廠商別已付帳款(本幣總額表)";
                                return frm;
                            });
                    }
                    else if (radio3.Checked)
                    {
                        //using (FrmFact_Receivd_N3 frm = new FrmFact_Receivd_N3())
                        //{
                        //    frm.dtM = dt_Main;
                        //    frm.Text = "廠商別已付帳款(幣別總額表)";
                        //    frm.ShowDialog();
                        //}
                        this.OpemInfoFrom<FrmFact_Receivd_N3>(() =>
                            {
                                FrmFact_Receivd_N3 frm = new FrmFact_Receivd_N3();
                                frm.dtM = dt_Main;
                                frm.Text = "廠商別已付帳款(幣別總額表)";
                                return frm;
                            });
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //finally
            //{
            //    dt.Clear();
            //    dt_Main.Clear();
            //    dt_Detail.Clear();
            //}
        }

        private void FaNo_Click(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            dt_Main.Clear();
            dt_Detail.Clear();

            dt.Dispose();
            dt_Main.Dispose();
            dt_Detail.Dispose();

            this.Close();
            this.Dispose();
        }

        private void PaDateAcs_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }



    }
}
