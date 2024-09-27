using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class 寄庫作業列印 : JE.MyControl.Formbase
    {
        public string InNo;
        public string CuNo;
        string txtstart = "";
        string txtend = "";
        string txtadress = "";
        string txttel = "";
        string 列印地址 = "";
        string 寄庫日期 = "";
        DataTable dt = new DataTable();
        public 寄庫作業列印()
        {
            InitializeComponent();
        }


        private void 寄庫作業列印_Load(object sender, EventArgs e)
        {
            radio5.SetUserDefineRpt("寄庫作業系統_簡要自定一.rpt");
            radio7.SetUserDefineRpt("寄庫作業系統_組件自定一.rpt");
            InNo_TB.Focus();
            InNo_TB.Text = InNo;
            InNo1_TB.Text = InNo;

            寄庫日期 = Common.User_DateTime == 1 ?
            "寄庫日期=SUBSTRING(indate,1,3) + '/'+SUBSTRING(indate,4,2)+ '/'+SUBSTRING(indate,6,2)"
            :
            "寄庫日期= SUBSTRING(indate1,1,4) + '/'+SUBSTRING(indate1,5,2)+ '/'+SUBSTRING(indate1,7,2)";
        }

        void OutReport(RptMode mode)
        {
            if (rdHeader1.Checked) txtstart = Common.dtstart.Rows[0]["pnname"].ToString();
            else if (rdHeader2.Checked) txtstart = Common.dtstart.Rows[1]["pnname"].ToString();
            else if (rdHeader3.Checked) txtstart = Common.dtstart.Rows[2]["pnname"].ToString();
            else if (rdHeader4.Checked) txtstart = Common.dtstart.Rows[3]["pnname"].ToString();
            else if (rdHeader5.Checked) txtstart = Common.dtstart.Rows[4]["pnname"].ToString();
            else txtstart = "";

            if (rdFooter1.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
            else if (rdFooter2.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
            else if (rdFooter3.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
            else if (rdFooter4.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
            else if (rdFooter5.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
            else txtend = "";

            if (rdHeader1.Checked) txtadress = "    " + Common.dtstart.Rows[0]["pnaddr"].ToString();
            else if (rdHeader2.Checked) txtadress = "    " + Common.dtstart.Rows[1]["pnaddr"].ToString();
            else if (rdHeader3.Checked) txtadress = "    " + Common.dtstart.Rows[2]["pnaddr"].ToString();
            else if (rdHeader4.Checked) txtadress = "    " + Common.dtstart.Rows[3]["pnaddr"].ToString();
            else if (rdHeader5.Checked) txtadress = "    " + Common.dtstart.Rows[4]["pnaddr"].ToString();
            else txtadress = "";

            if (rdHeader1.Checked) txttel = "    TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
            else if (rdHeader2.Checked) txttel = "    TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
            else if (rdHeader3.Checked) txttel = "    TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
            else if (rdHeader4.Checked) txttel = "    TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
            else if (rdHeader5.Checked) txttel = "    TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
            else txttel = "";

            using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("cuno", CuNo.Trim());
                    cmd.CommandText = "select cuAddr1 from cust where cuno=@cuno COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                            列印地址 = reader["cuAddr1"].ToString();
                        else
                            列印地址 = "";
                    }
                }
            }
            LoadDt();

            var path = "";
            if (radio1.Checked == true)
                path = Common.reportaddress + @"Report\寄庫作業系統_簡要表.rpt";
            else
                path = Common.reportaddress + @"Report\寄庫作業系統_組件明細.rpt";

            RPT rp = new RPT(dt, path);
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "txtstart", txtstart });
            rp.lobj.Add(new string[] { "txtadress", txtadress });
            rp.lobj.Add(new string[] { "txttel", txttel });
            rp.lobj.Add(new string[] { "列印地址", 列印地址 });

            if (mode == RptMode.Print) rp.Print();
            else if (mode == RptMode.PreView) rp.PreView();
            else if (mode == RptMode.Excel) rp.Excel();
            else if (mode == RptMode.Word) rp.Word();
        }
        private void LoadDt()
        {
            string SelectStr = "", TableJoin = "", WhereStr = "and InStkD.inno >= @inno and InStkD.inno <= @inno1";
            if (radio1.Checked == true)
            {
                TableJoin = "  InStkD inner join cust on InStkD.cuno = cust.cuno  ";
                SelectStr =
@"InStkD.inno as 寄庫單號
, cust.cuno as 客戶編號
, cust.cuname1 as 客戶簡稱
, cust.cutel1 as 電話 
, cust.cuper1 as 聯絡人
, cust.cuname2 as 客戶名稱
, " +寄庫日期+ @"
, InStkD.itno as 產品編號
, InStkD.itname as 品名規格
, InStkD.inqty as 寄庫數量
, InStkD.itunit as 單位 " ;
            }
            else if (radio2.Checked == true)
            {
                TableJoin = " InStkBOM right join InStkD on InStkBOM.BomID = InStkD.bomid inner join cust on cust.cuno = InStkD.cuno ";
                SelectStr =
 @"
 InStkD.inid as inid
,InStkD.inno as 寄庫單號
,InStkD.itno as 產品編號
,InStkD.itname as 品名規格
,InStkD.inqty as 寄庫數量
, " + 寄庫日期 + @"
,cust.cuno as 客戶編號
,cust.cuname1 as 客戶簡稱
,cust.cutel1 as  電話 
,cust.cuper1 as 聯絡人
,cust.cuname2 as 客戶名稱 
,InStkBOM.itno   as Bom產品編號
,InStkBOM.itname as Bom產品規格
,InStkBOM.itqty  as Bom寄庫數量
,InStkBOM.itunit as Bom寄庫單位
,InStkBOM.BomID as BomID  ";
            }


            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter ad = new SqlDataAdapter(cm)) 
            {
                dt.Clear();
                cm.Parameters.AddWithValue("inno", InNo_TB.Text.Trim());
                cm.Parameters.AddWithValue("inno1", InNo1_TB.Text.Trim());
                cm.CommandText = "select " + SelectStr + " from " + TableJoin +  "where 0=0 " + WhereStr;
                ad.Fill(dt);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBoxT1);
            pnlist.Add(groupBoxT6);
            pnlist.Add(groupBoxT8);
            pVar.SaveRadioUdf(pnlist, "OuStk");
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

        private void InNo_TB_DoubleClick(object sender, EventArgs e)
        {
            using (寄庫作業列印_選擇寄庫單號視窗 frm = new 寄庫作業列印_選擇寄庫單號視窗())
            {
                frm.SeekNo = (sender as TextBoxT).Text;
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        (sender as TextBoxT).Text = frm.SeekNo;
                        break;
                    case DialogResult.Cancel: break;
                }
            }
        }

        private void InNo_TB_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            bool isHaveRow = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("inno", (sender as TextBoxT).Text.Trim());
                        cmd.CommandText = "select Count(inno) from InStk where inno=@inno COLLATE Chinese_Taiwan_Stroke_BIN";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                if (reader[0].ToString() == "0")
                                    isHaveRow = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (!isHaveRow)
            {
                e.Cancel = true;
                using (寄庫作業列印_選擇寄庫單號視窗 frm = new 寄庫作業列印_選擇寄庫單號視窗())
                {

                    frm.SeekNo = (sender as TextBoxT).Text;
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            (sender as TextBoxT).Text = frm.SeekNo;
                            break;
                        case DialogResult.Cancel: break;
                    }
                }
            }
        }



    }
}
