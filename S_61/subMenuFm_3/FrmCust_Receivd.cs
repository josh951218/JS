using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_3
{
    public partial class FrmCust_Receivd : Formbase
    {
        JBS.JS.xEvents xe;
        public DataTable dt = new DataTable();
        public DataTable dt_Main = new DataTable();
        public DataTable dt_Detail = new DataTable();

        public FrmCust_Receivd()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            pVar.FrmCust_Receivd = this;

            switch (Common.User_DateTime)
            {
                case 1:
                    ReDateAcs.MaxLength = 7;
                    ReDateAcs_1.MaxLength = 7;
                    break;
                case 2:
                    ReDateAcs.MaxLength = 8;
                    ReDateAcs_1.MaxLength = 8;
                    break;
            }
            if (Common.Series == "74" || Common.Series == "73")
            {
                radio3.Enabled = false;
              
            }
        }

        private void FrmCust_Receivd_Load(object sender, EventArgs e)
        {
            ReDateAcs.Focus();
            radio1.Checked = true;
            radio4.Checked = true;
            switch (Common.User_DateTime)
            {
                case 1:
                    ReDateAcs.MaxLength = 7;
                    ReDateAcs_1.MaxLength = 7;
                    ReDateAcs.Text = Date.GetDateTime(1, false).Substring(0, 5) + "01";
                    ReDateAcs_1.Text = Date.GetDateTime(1, false);
                    break;
                case 2:
                    ReDateAcs.MaxLength = 8;
                    ReDateAcs_1.MaxLength = 8;
                    ReDateAcs.Text = Date.GetDateTime(2, false).Substring(0, 6) + "01";
                    ReDateAcs_1.Text = Date.GetDateTime(2, false);
                    break;
            }
         
           
        }
         
        private void CuNo_Click(object sender, EventArgs e)
        { 
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void ReDateAcs_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tx = sender as TextBox;
            if (!tx.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("日期輸入錯誤，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (ReDateAcs.Text.Trim() != "" && ReDateAcs_1.Text.Trim() != "")
            {
                if (string.CompareOrdinal(ReDateAcs.Text.Trim(), ReDateAcs_1.Text.Trim()) > 0)
                {
                    MessageBox.Show("起始帳款日期不可大於終止帳款日期，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ReDateAcs.Focus();
                    return;
                }
            }
            if (CuNo.Text.Trim() != "" && CuNo_1.Text.Trim() != "")
            {
                if (string.CompareOrdinal(CuNo.Text.Trim(), CuNo_1.Text.Trim()) > 0)
                {
                    MessageBox.Show("起始客戶編號不可大於終止客戶編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CuNo.Focus();
                    return;
                }
            }


            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = conn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    if (CuNo.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno", CuNo.Text.Trim());
                    if (CuNo_1.Text.Trim() != "") cmd.Parameters.AddWithValue("cuno1", CuNo_1.Text.Trim());
                    
                    string str = "";
                    string str_Main = "";

                    if (radio1.Checked)
                    {
                        //明細表 簡要表
                        str = "select ReNo 收款憑證,SUBSTRING(ReDate,1,3)+'/'+SUBSTRING(ReDate,4,2)+'/'+SUBSTRING(ReDate,6,2) 收款日期民國,SUBSTRING(ReDate1,1,4)+'/'+SUBSTRING(ReDate1,5,2)+'/'+SUBSTRING(ReDate1,7,2) 收款日期西元,CuNo 客戶編號,CuName1 客戶簡稱,TotDisc 折讓金額,CashMny 現金金額,CardMny 刷卡金額,Ticket 禮卷金額,CheckMny 支票金額,RemitMny 匯入金額,OtherMny 其他金額,GetPrvAcc 取用預收,TotReve 沖帳合計,AddPrvAcc 累入預收,TotMny 沖抵帳款 from Receiv where '0'='0'";
                        str += " and ReDate >='" + Date.ToTWDate(ReDateAcs.Text.Trim()) + "' and ReDate <='" + Date.ToTWDate(ReDateAcs_1.Text.Trim()) + "'";
                        if (CuNo.Text.Trim() != "")
                            str += " and cuno>=@cuno";
                        if (CuNo_1.Text.Trim() != "")
                            str += " and cuno<=@cuno1";
                        cmd.CommandText = str;
                        dt_Detail.Clear();
                        dd.Fill(dt_Detail);


                        str_Main = "select *,cuname1 客戶簡稱,cuname1 客戶名稱,CuAddr1 地址,CuTel1 電話,CuFax1 傳真,(select xa1name from Xa01 where Xa1No=cust.cuxa1no) 幣別,cust.CuPer1 聯絡人,cust.CuUno 統一編號 from(";
                        str_Main += "select CuNo 客戶編號,count(*) 沖帳總筆數,sum(TotDisc) 折讓總金額,sum(CashMny) 現金總金額,sum(CardMny) 刷卡總金額,sum(Ticket) 禮券總金額,sum(CheckMny) 支票總金額,sum(RemitMny) 匯入總金額,sum(OtherMny) 其他總金額,sum(GetPrvAcc) 取用總金額,sum(TotReve) 沖帳總合計,sum(AddPrvAcc) 累入總金額,sum(TotMny) 沖抵總金額 from Receiv where '0'='0'";
                        str_Main += " and ReDate >='" + Date.ToTWDate(ReDateAcs.Text.Trim()) + "' and ReDate <='" + Date.ToTWDate(ReDateAcs_1.Text.Trim()) + "'";
                        str_Main += " group by CuNo) 沖帳總計 left join cust on 沖帳總計.客戶編號=cust.cuno where '0'='0'";
                        if (CuNo.Text.Trim() != "")
                            str_Main += " and 沖帳總計.客戶編號>=@cuno";
                        if (CuNo_1.Text.Trim() != "")
                            str_Main += " and 沖帳總計.客戶編號<=@cuno1";
                        cmd.CommandText = str_Main;
                        dt_Main.Clear();
                        dd.Fill(dt_Main);


                        string str_all;
                        str_all = "select * from (" + str_Main + ") Main right join (" + str + ") Detail on Main.客戶編號=Detail.客戶編號 left join receivd on Detail.收款憑證=receivd.reno";
                        cmd.CommandText = str_all;
                        dt.Clear();
                        dd.Fill(dt);
                    }
                    else if (radio2.Checked)
                    {
                        str = "select *,cuname1 客戶簡稱,cuname1 客戶名稱,(select xa1name from Xa01 where Xa1No=cust.cuxa1no) 幣別 from(";
                        str += "select CuNo 客戶編號,count(*) 沖帳筆數,sum(TotDisc * xa1par) 折讓金額,sum(CashMny * xa1par) 現金金額,sum(CardMny * xa1par) 刷卡金額,sum(Ticket * xa1par) 禮卷金額,sum(CheckMny * xa1par) 支票金額,sum(RemitMny * xa1par) 匯入金額,sum(OtherMny * xa1par) 其他金額,sum(GetPrvAcc * xa1par) 取用預收,sum(TotReve * xa1par) 沖帳合計,sum(AddPrvAcc * xa1par) 累入預收,sum(TotMny * xa1par) 沖抵帳款 from ";
                        str +=" Receiv where '0'='0'";
                        str += " and ReDate >='" + Date.ToTWDate(ReDateAcs.Text.Trim()) + "' and ReDate <='" + Date.ToTWDate(ReDateAcs_1.Text.Trim()) + "'";
                        if (CuNo.Text.Trim() != "")
                            str += " and cuno>=@cuno";
                        if (CuNo_1.Text.Trim() != "")
                            str += " and cuno<=@cuno1";
                        str += " group by CuNo) 沖帳總計 left join cust on 沖帳總計.客戶編號=cust.cuno where '0'='0'";

                        str_Main += "select count(*) 沖帳總筆數,sum(TotDisc * xa1par) 折讓總金額,sum(CashMny * xa1par) 現金總金額,sum(CardMny * xa1par) 刷卡總金額,sum(Ticket * xa1par) 禮券總金額,sum(CheckMny * xa1par) 支票總金額,sum(RemitMny * xa1par) 匯入總金額,sum(OtherMny * xa1par) 其他總金額,sum(GetPrvAcc * xa1par) 取用總金額,sum(TotReve * xa1par) 沖帳總合計,sum(AddPrvAcc * xa1par) 累入總金額,sum(TotMny * xa1par) 沖抵總金額 ";
                        str_Main += " from Receiv where '0'='0'";
                        str_Main += " and ReDate >='" + Date.ToTWDate(ReDateAcs.Text.Trim()) + "' and ReDate <='" + Date.ToTWDate(ReDateAcs_1.Text.Trim()) + "'";
                        if (CuNo.Text.Trim() != "")
                            str_Main += " and cuno>=@cuno";
                        if (CuNo_1.Text.Trim() != "")
                            str_Main += " and cuno<=@cuno1";

                        str_Main = "select * from (" + str_Main + ") Main right join (" + str + ") Detail on 0=0";
                        cmd.CommandText = str_Main;
                        dt_Main.Clear();
                        dd.Fill(dt_Main);
                    }
                    else
                    {
                        str = "select *,cuname1 客戶簡稱,cuname1 客戶名稱,(select xa1name from Xa01 where Xa1No=cust.cuxa1no) 幣別 from(";
                        str += "select CuNo 客戶編號,count(*) 沖帳筆數,sum(TotDisc) 折讓金額,sum(CashMny) 現金金額,sum(CardMny) 刷卡金額,sum(Ticket) 禮卷金額,sum(CheckMny) 支票金額,sum(RemitMny) 匯入金額,sum(OtherMny) 其他金額,sum(GetPrvAcc) 取用預收,sum(TotReve) 沖帳合計,sum(AddPrvAcc) 累入預收,sum(TotMny) 沖抵帳款 from Receiv where '0'='0'";
                        str += " and ReDate >='" + Date.ToTWDate(ReDateAcs.Text.Trim()) + "' and ReDate <='" + Date.ToTWDate(ReDateAcs_1.Text.Trim()) + "'";
                        if (CuNo.Text.Trim() != "")
                            str += " and cust.cuno>=@cuno";
                        if (CuNo_1.Text.Trim() != "")
                            str += " and cust.cuno<=@cuno1";
                        str += " group by CuNo) 沖帳總計 left join cust on 沖帳總計.客戶編號=cust.cuno where '0'='0'";

                        str_Main += "select count(*) 沖帳總筆數,sum(TotDisc) 折讓總金額,sum(CashMny) 現金總金額,sum(CardMny) 刷卡總金額,sum(Ticket) 禮券總金額,sum(CheckMny) 支票總金額,sum(RemitMny) 匯入總金額,sum(OtherMny) 其他總金額,sum(GetPrvAcc) 取用總金額,sum(TotReve) 沖帳總合計,sum(AddPrvAcc) 累入總金額,sum(TotMny) 沖抵總金額 from Receiv where '0'='0'";
                        str_Main += " and ReDate >='" + Date.ToTWDate(ReDateAcs.Text.Trim()) + "' and ReDate <='" + Date.ToTWDate(ReDateAcs_1.Text.Trim()) + "'";
                        if (CuNo.Text.Trim() != "")
                            str_Main += " and cuno>=@cuno";
                        if (CuNo_1.Text.Trim() != "")
                            str_Main += " and cuno<=@cuno1";

                        str_Main = "select * from (" + str_Main + ") Main right join (" + str + ") Detail on 0=0";
                        cmd.CommandText = str_Main;
                        dt_Main.Clear();
                        dd.Fill(dt_Main);
                    }
                }

                if (dt_Main.Rows.Count > 0)
                {
                    //開啟瀏覽視窗
                    this.OpemInfoFrom<FrmCust_Receivdb>(() =>
                            {
                                FrmCust_Receivdb frm = new FrmCust_Receivdb();
                                if (radio1.Checked)
                                {
                                    frm.tabControl1.SelectTab(0);
                                    frm.Text = "客戶別已收帳款(明細簡要表)";
                                }
                                else if (radio2.Checked)
                                {
                                    frm.tabControl1.SelectTab(1);
                                    frm.Text = "客戶別已收帳款(本幣總額表)";
                                }
                                else if (radio3.Checked)
                                {
                                    frm.tabControl1.SelectTab(2);
                                    frm.Text = "客戶別已收帳款(幣別總額表)";
                                }
                                return frm;
                            });
                }
                else
                {
                    MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pVar.SaveRadioUdf(pnlist, "FrmCust_Receivd");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT2);
            pVar.SetRadioUdf(pnlist, "FrmCust_Receivd");
        }

        private void FrmCust_Receivd_Shown(object sender, EventArgs e)
        {
            ReDateAcs.Focus();
        }
    }
}
