using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastReport.Export.OoXML;
using Gma.QrCodeNet.Encoding;//放檔案後 加入參考
using Gma.QrCodeNet.Encoding.Windows.Render;
using JE.MyControl;
using S_61.Basic;
using S_61.電子發票;
using JBS;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using JBS.JS;
using JBS.JM;
using S_61.S0;

namespace S_61.subMenuFm_2
{
    public partial class FrmSale_Print : Formbase
    {
        JBS.JS.xEvents xe;
        //JBS.JM.NSale jNSale;
        //JBS.JM.Machine jMachine;
        
        DataSet ds = new DataSet();
        public string PK ,qrStr, rand4, detail;
        public string CuNo;
        string ReportFileName = "";
        string ReportPath = "";
        string sql,path;
        DataTable dteinvhead = new DataTable();
        DataTable dteinvbody = new DataTable();
        DataTable dtoutput = new DataTable();
        DataTable Einvtemp = new DataTable();

        //分單參數
        string festreport_txtend = "";

        public FrmSale_Print()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            pnlMemo.Text = Common.Sys_MemoUdf;
            btnBar.Visible = (Common.CompanyName == "秉衡量");
        }

        private void FrmSale_Print_Load(object sender, EventArgs e)
        {
            SaNo.Text = PK;
            SaNo1.Text = PK;
            if (Common.User_SalePrice == true)
            {
                radio12.Enabled = true;
                radio13.Enabled = true;
            }
            else
            {
                radio12.Enabled = false;
                radio13.Enabled = false;
                radio13.Checked = true;
            }
            ReportFileName = "銷貨作業系統";

            SetRdUdf();

            if (radio分單.Checked == false && radioT不分單.Checked == false)
                radioT不分單.Checked = true;

            if (radio22.Checked == false && radio23.Checked == false && radio24.Checked == false)
                radioT不分單.Checked = true;

            radio分單與不分單_CheckedChanged(null, null);
            if (radioEinv.Checked)
            {
                EinvB2CD.Enabled = true;
                Einvsecond.Enabled = true;
            }
        }



        void dataintodocument(RptMode mode)
        {
            try
            {
                ds.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    setSql();
                    sql += " where '0'='0'";
                    if (SaNo.Text != "")
                        sql += " and sale.sano >=@sano";

                    if (SaNo1.Text.Trim() != "")
                        sql += " and sale.sano <=@sano1";

                    sql += " Order by saled_said ";

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                        cmd.Parameters.AddWithValue("sano1", SaNo1.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds, "Data");
                            cmd.CommandText = @"
                            Select item.itno, itnoudf, itname, kino, itime, ittrait, itunit, itunitp, itpkgqty, itbuypri, itprice, itprice1, itprice2, itprice3, itprice4, 
                                    itprice5, itcost, itbuyprip, itpricep, itpricep1, itpricep2, itpricep3, itpricep4, itpricep5, itcostp, itbuyunit, itsalunit, 
                                    itsafeqty, itlastqty, itnw, itnwunit, itcostslt, itcodeslt, itcodeno, itdesp1, itdesp2, itdesp3, itdesp4, itdesp5, itdesp6, 
                                    itdesp7, itdesp8, itdesp9, itdesp10, itdate, itdate1, itdate2, itbuydate, itbuydate1, itbuydate2, itsaldate, itsaldate1, 
                                    itSaldate2, itfircost, itfirtqty, itfirtcost, itstockqty, itnote, itudf1, itudf2, itudf3, itudf4, itudf5, itweblist, itwebpic, itwebctl1, 
                                    itwebctl2, IsUse, ItSource, itpicture, IsEnable, fano, Punit, ScNo
                            from (
		                            Select itno from saled where saled.sano>=(@sano) and saled.sano <=(@sano1) group by itno
	                                )A
                            left join item on A.itno = item.itno
                                                ";
                            da.Fill(ds, "item");
                        }
                    }
                }

                if (ds.Tables["Data"].Rows.Count <= 0)
                {
                    SaNo.Focus();
                    MessageBox.Show("查無資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (radioT不分單.Checked)
                {
                    #region 一般列印
                    ReportDocument oRpt = new ReportDocument();
                    ReportPath = Common.reportaddress + "Report\\" + ReportFileName;

                    bool IsUserDefine = false;//是否為自定報表
                    if (radio4.Checked) IsUserDefine = true;
                    else if (radio5.Checked) IsUserDefine = true;
                    else if (radio6.Checked) IsUserDefine = true;
                    else if (radio7.Checked) IsUserDefine = true;

                    if (!IsUserDefine)
                    {
                        if (radio8.Checked) ReportPath += "B";
                        if (radio9.Checked) ReportPath += "A";
                        if (radio10.Checked) ReportPath += "O";
                        if (radio11.Checked) ReportPath += "";
                        if (radio14.Checked) ReportPath += "M";
                        if (radio15.Checked) ReportPath += "";
                    }

                    string udfPath = Common.reportaddress + "Report\\";
                    if (Common.Sys_DBqty == 1)
                    {
                        #region Common.Sys_DBqty == 1
                        if (radio1.Checked)
                        {
                            var getPath = 確認CF或FF(udfPath + "SaleRpt1");
                            if (getPath != null)
                            {
                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                ReportPath += "_簡要表";
                                getPath = 確認CF或FF(ReportPath);
                                if (getPath != null)
                                {
                                    if (getPath.EndsWith("rpt"))
                                        oRpt.Load(getPath);
                                    else
                                    {
                                        oRpt.Dispose();
                                        FastReport列印(getPath, mode);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else if (radio2.Checked)
                        {
                            var getPath = 確認CF或FF(udfPath + "SaleRpt2");
                            if (getPath != null)
                            {
                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                ReportPath += "_組件明細";
                                getPath = 確認CF或FF(ReportPath);
                                if (getPath != null)
                                {
                                    if (getPath.EndsWith("rpt"))
                                        oRpt.Load(getPath);
                                    else
                                    {
                                        oRpt.Dispose();
                                        FastReport列印(getPath, mode);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else if (radio3.Checked)
                        {
                            var getPath = 確認CF或FF(udfPath + "SaleRpt3");
                            if (getPath != null)
                            {
                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                ReportPath += "_組件明細_規格";
                                getPath = 確認CF或FF(ReportPath);
                                if (getPath != null)
                                {
                                    if (getPath.EndsWith("rpt"))
                                        oRpt.Load(getPath);
                                    else
                                    {
                                        oRpt.Dispose();
                                        FastReport列印(getPath, mode);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else if (radio4.Checked)
                        {
                            var getPath = 確認CF或FF(udfPath + "SaleRpt4");
                            if (getPath != null)
                            {
                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                ReportPath += "_幣別簡要表";
                                getPath = 確認CF或FF(ReportPath);
                                if (getPath != null)
                                {
                                    if (getPath.EndsWith("rpt"))
                                        oRpt.Load(getPath);
                                    else
                                    {
                                        oRpt.Dispose();
                                        FastReport列印(getPath, mode);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else if (radio5.Checked)
                        {
                            ReportPath += "_簡要自定一";
                            var getPath = 確認CF或FF(ReportPath);
                            if (getPath != null)
                            {

                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else if (radio6.Checked)
                        {
                            ReportPath += "_簡要自定二";
                            var getPath = 確認CF或FF(ReportPath);
                            if (getPath != null)
                            {

                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else if (radio7.Checked)
                        {
                            ReportPath += "_組件自定一";
                            var getPath = 確認CF或FF(ReportPath);
                            if (getPath != null)
                            {

                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        #endregion
                    }
                    else if (Common.Sys_DBqty == 2)
                    {
                        #region Sys_DBqty == 2
                        if (radio1.Checked)
                        {
                            var getPath = 確認CF或FF(udfPath + "SaleRpt1P");
                            if (getPath != null)//File.Exists(udfPath + "SaleRpt1.rpt"))
                            {
                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                ReportPath += "_簡要表P";
                                getPath = 確認CF或FF(ReportPath);
                                if (getPath != null)
                                {
                                    if (getPath.EndsWith("rpt"))
                                        oRpt.Load(getPath);
                                    else
                                    {
                                        oRpt.Dispose();
                                        FastReport列印(getPath, mode);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else if (radio2.Checked)
                        {
                            var getPath = 確認CF或FF(udfPath + "SaleRpt2P");
                            if (getPath != null)//File.Exists(udfPath + "SaleRpt1.rpt"))
                            {
                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                ReportPath += "_組件明細P";
                                getPath = 確認CF或FF(ReportPath);
                                if (getPath != null)
                                {
                                    if (getPath.EndsWith("rpt"))
                                        oRpt.Load(getPath);
                                    else
                                    {
                                        oRpt.Dispose();
                                        FastReport列印(getPath, mode);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else if (radio3.Checked)
                        {
                            var getPath = 確認CF或FF(udfPath + "SaleRpt3P");
                            if (getPath != null)//File.Exists(udfPath + "SaleRpt1.rpt"))
                            {
                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                ReportPath += "_組件明細_規格P";
                                getPath = 確認CF或FF(ReportPath);
                                if (getPath != null)
                                {
                                    if (getPath.EndsWith("rpt"))
                                        oRpt.Load(getPath);
                                    else
                                    {
                                        oRpt.Dispose();
                                        FastReport列印(getPath, mode);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else if (radio4.Checked)
                        {
                            ReportPath += "_幣別簡要表P";
                            var getPath = 確認CF或FF(ReportPath);
                            if (getPath != null)
                            {

                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else if (radio5.Checked)
                        {
                            ReportPath += "_簡要自定一P";
                            var getPath = 確認CF或FF(ReportPath);
                            if (getPath != null)
                            {

                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else if (radio6.Checked)
                        {
                            ReportPath += "_簡要自定二P";
                            var getPath = 確認CF或FF(ReportPath);
                            if (getPath != null)
                            {

                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else if (radio7.Checked)
                        {
                            ReportPath += "_組件自定一P";
                            var getPath = 確認CF或FF(ReportPath);
                            if (getPath != null)
                            {

                                if (getPath.EndsWith("rpt"))
                                    oRpt.Load(getPath);
                                else
                                {
                                    oRpt.Dispose();
                                    FastReport列印(getPath, mode);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        #endregion
                    }

                    oRpt.SetDataSource(ds);

                    if (Common.Sql_LogMod == 2)//混合驗證
                    {
                        Common.logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                        foreach (CrystalDecisions.CrystalReports.Engine.Table tt in oRpt.Database.Tables)
                        {
                            tt.ApplyLogOnInfo(Common.logOnInfo);
                        }
                        oRpt.Refresh();
                    }
                    else if (Common.Sql_LogMod == 1)//SQL驗證
                    {
                        Common.logOnInfo.ConnectionInfo.IntegratedSecurity = false;
                        foreach (CrystalDecisions.CrystalReports.Engine.Table tt in oRpt.Database.Tables)
                        {
                            tt.ApplyLogOnInfo(Common.logOnInfo);
                        }
                        oRpt.Refresh();
                    }


                    TextObject myFieldTitleName;
                    List<TextObject> Txt = oRpt.ReportDefinition.ReportObjects.OfType<TextObject>().ToList();

                    //公司抬頭
                    if (Txt.Find(t => t.Name == "txtstart") != null)
                    {
                        myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtstart"];
                        if (rdHeader1.Checked)      myFieldTitleName.Text = Common.dtstart.Rows[0]["pnname"].ToString();
                        else if (rdHeader2.Checked) myFieldTitleName.Text = Common.dtstart.Rows[1]["pnname"].ToString();
                        else if (rdHeader3.Checked) myFieldTitleName.Text = Common.dtstart.Rows[2]["pnname"].ToString();
                        else if (rdHeader4.Checked) myFieldTitleName.Text = Common.dtstart.Rows[3]["pnname"].ToString();
                        else if (rdHeader5.Checked) myFieldTitleName.Text = Common.dtstart.Rows[4]["pnname"].ToString();
                        else myFieldTitleName.Text = "";
                    }
                    //銷貨單標題
                    if (Txt.Find(t => t.Name == "title") != null)
                    {
                        myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["title"];
                        myFieldTitleName.Text = Common.Sys_SaleHead;
                    }
                    //三行註腳
                    if (Txt.Find(t => t.Name == "txtend") != null)
                    {
                        myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtend"];
                        if (rdFooter1.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                        else if (rdFooter2.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                        else if (rdFooter3.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                        else if (rdFooter4.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                        else if (rdFooter5.Checked) myFieldTitleName.Text = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                        else myFieldTitleName.Text = "";
                        festreport_txtend = myFieldTitleName.Text;
                    }
                    //表頭-公司住址
                    if (Txt.Find(t => t.Name == "txtadress") != null)
                    {
                        //表頭-公司住址
                        myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txtadress"];
                        if (rdHeader1.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[0]["pnaddr"].ToString();
                        else if (rdHeader2.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[1]["pnaddr"].ToString();
                        else if (rdHeader3.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[2]["pnaddr"].ToString();
                        else if (rdHeader4.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[3]["pnaddr"].ToString();
                        else if (rdHeader5.Checked) myFieldTitleName.Text = "    " + Common.dtstart.Rows[4]["pnaddr"].ToString();
                        else myFieldTitleName.Text = "";
                    }
                    //表頭-電話、傳真
                    if (Txt.Find(t => t.Name == "txttel") != null)
                    {
                        myFieldTitleName = (TextObject)oRpt.ReportDefinition.ReportObjects["txttel"];
                        if (rdHeader1.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
                        else if (rdHeader2.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
                        else if (rdHeader3.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
                        else if (rdHeader4.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
                        else if (rdHeader5.Checked) myFieldTitleName.Text = "    TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
                        else myFieldTitleName.Text = "";
                    }

                    List<ParameterField> Num = oRpt.ParameterFields.OfType<ParameterField>().ToList();
                    //列印地址
                    if (Num.Find(p => p.Name == "列印地址") != null)
                    {
                        if (radio22.Checked)
                            oRpt.SetParameterValue("列印地址", "1");
                        if (radio23.Checked)
                            oRpt.SetParameterValue("列印地址", "2");
                        if (radio24.Checked)
                            oRpt.SetParameterValue("列印地址", Addr.Text.Trim());
                    }

                    //日期格式
                    if (Num.Find(p => p.Name == "date") != null)
                    {
                        switch (Common.User_DateTime)
                        {
                            case 1:
                                oRpt.SetParameterValue("date", "民國");
                                break;
                            case 2:
                                oRpt.SetParameterValue("date", "西元");
                                break;
                        }
                    }
                    if (Num.Find(p => p.Name == "備註說明") != null)
                    {
                        oRpt.SetParameterValue("備註說明", Common.Sys_MemoUdf);
                    }
                    if (Num.Find(p => p.Name == "使用者") != null)
                    {
                        oRpt.SetParameterValue("使用者", Common.User_Name1);
                    }

                    //報表參數設定
                    if (Num.Find(p => p.Name == "顯示千分位") != null)
                    {
                        if (pVar.TRS != "")
                            pVar.ShowTRS = true;
                        oRpt.SetParameterValue("顯示千分位", pVar.ShowTRS);
                    }
                    if (Num.Find(p => p.Name == "千分位") != null)
                        oRpt.SetParameterValue("千分位", pVar.TRS);
                    if (Num.Find(p => p.Name == "銷貨單價小數") != null)
                        oRpt.SetParameterValue("銷貨單價小數", Common.MS);
                    if (Num.Find(p => p.Name == "銷貨單據小數") != null)
                        oRpt.SetParameterValue("銷貨單據小數", Common.MST);
                    if (Num.Find(p => p.Name == "銷項稅額小數") != null)
                        oRpt.SetParameterValue("銷項稅額小數", Common.TS);
                    if (Num.Find(p => p.Name == "本幣金額小數") != null)
                        oRpt.SetParameterValue("本幣金額小數", Common.M);
                    if (Num.Find(p => p.Name == "庫存數量小數") != null)
                        oRpt.SetParameterValue("庫存數量小數", Common.Q);

                    if (Num.Find(p => p.Name == "銷項金額小數") != null)
                        oRpt.SetParameterValue("銷項金額小數", Common.TPS);
                    if (Num.Find(p => p.Name == "進項金額小數") != null)
                        oRpt.SetParameterValue("進項金額小數", Common.TPF);

                    //是否顯示價格
                    bool showprice = false;
                    if (Common.User_SalePrice)
                    {
                        showprice = (radio12.Checked) ? true : false;
                    }
                    else
                    {
                        showprice = false;
                    }
                    if (Num.Find(p => p.Name == "是否顯示金額") != null)
                    {
                        oRpt.SetParameterValue("是否顯示金額", showprice);
                    }

                    Common.FrmReport = new Report.Frmreport();
                    Common.FrmReport.cview.ReportSource = oRpt;
                    Common.FrmReport.rpt1 = oRpt;

                    if (mode == RptMode.Print)
                        Common.FrmReport.button1_Click(null, null);
                    else if (mode == RptMode.PreView)
                        Common.FrmReport.ShowDialog();
                    else if (mode == RptMode.Word)
                        Common.FrmReport.word(ReportFileName);
                    else if (mode == RptMode.Excel)
                        Common.FrmReport.excel(ReportFileName);
                    #endregion
                }
                else
                {
                    #region 分單列印
                    using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
                    {
                        //報表參數
                        if (rdFooter1.Checked) festreport_txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                        else if (rdFooter2.Checked) festreport_txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                        else if (rdFooter3.Checked) festreport_txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                        else if (rdFooter4.Checked) festreport_txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                        else if (rdFooter5.Checked) festreport_txtend = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                        else festreport_txtend = "";
                        
                        List<string> Paramaters = new List<string>() { "民國或西元", Common.User_DateTime.ToString(), "txtend", festreport_txtend, "備註抬頭", Common.Sys_MemoUdf, "列印抬頭", CheckBox列印抬頭.Checked.ToString(), "列印備註說明", radio14.Checked.ToString() };
                        FastReport.Paramaters = Paramaters;
                        #region 報表選擇
                        string 報表名稱 = "銷貨作業系統";
                        //報表格式
                        if (radio8.Checked) 報表名稱 += "B";
                        else 報表名稱 += "A";
                        //訂單列印
                        if (radio10.Checked) 報表名稱 += "O";
                        //報表內容
                        if (radio1.Checked)
                            報表名稱 += "_簡要表P";
                        else if (radio2.Checked)
                            報表名稱 += "_組件明細P";
                        else if (radio3.Checked)
                            報表名稱 += "_組件明細_規格P";
                        else if (radio4.Checked)
                            報表名稱 = "銷貨作業系統_幣別簡要表";
                        else if (radio5.Checked)
                            報表名稱 = "銷貨作業系統_簡要自定一";
                        else if (radio6.Checked)
                            報表名稱 = "銷貨作業系統_簡要自定二";
                        else if (radio7.Checked)
                            報表名稱 = "銷貨作業系統_組件自定一";
                        #endregion
                        ReportPath = Common.reportaddress + "ReportG\\" + 報表名稱 + ".frx";

                        if (File.Exists(this.ReportPath) == false)
                        {
                            MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        FastReport.PreView(ReportPath, ds.Tables["Data"], "Data_", ds.Tables["item"], "Item_", mode, 報表名稱);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (radioEinv.Checked == true)
            { 電子發票證明聯(RptMode.Print); }
            else
            dataintodocument(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            if (radioEinv.Checked == true)
            { 電子發票證明聯(RptMode.PreView); }
            else
            { dataintodocument(RptMode.PreView); }
        }

       
        private void btnWord_Click(object sender, EventArgs e)
        {
            if (radioEinv.Checked == true)
            { 電子發票證明聯(RptMode.Word); }
            else
            dataintodocument(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (radioEinv.Checked == true)
            { 電子發票證明聯(RptMode.Excel); }
            else
            dataintodocument(RptMode.Excel);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ds.Clear();
            base.OnFormClosing(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void SaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Sale>(sender);
        }

        private void SaNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Sale>(sender, e, row => { });
        }

        private void radio24_CheckedChanged(object sender, EventArgs e)
        {
            if (radio24.Checked)
            {
                Addr.ReadOnly = false;
                btnAddr.Visible = true;
                Addr.Focus();
            }
            else
            {
                Addr.ReadOnly = true;
                btnAddr.Visible = false;
            }
        }

        private void btnAddr_Click(object sender, EventArgs e)
        {
            if (Addr.ReadOnly) return;
            using (var frm = new FrmSale_Print_Addr())
            {
                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        Addr.Text = frm.TResult;
                        break;
                    case DialogResult.Cancel: break;
                }
            }
        }

        private void btnUdf_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(pnlRpt);
            pnlist.Add(pnlPage);
            pnlist.Add(pnlOrder);
            pnlist.Add(pnlPrice);
            pnlist.Add(pnlMemo);
            pnlist.Add(pnlEnd);
            pnlist.Add(pnlAddress);
            pnlist.Add(pnlTitle);
            pVar.SaveRadioUdf(pnlist, "Sale");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pVar.ResetRadioUdf("Sale");
            radio1.Checked = true;
            radio8.Checked = true;
            radio11.Checked = true;
            radio12.Checked = true;
            radio15.Checked = true;
            rdFooter1.Checked = true;
            radio22.Checked = true;
            rdHeader1.Checked = true;
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(pnlRpt);
            pnlist.Add(pnlPage);
            pnlist.Add(pnlOrder);
            pnlist.Add(pnlPrice);
            pnlist.Add(pnlMemo);
            pnlist.Add(pnlEnd);
            pnlist.Add(pnlAddress);
            pnlist.Add(pnlTitle);
            pVar.SetRadioUdf(pnlist, "Sale");
        }

        void setSql()
        {
            sql = "";
            sql += " SELECT sale.sano AS sale_sano, sale.sadate AS sale_sadate, sale.sadateac AS sale_sadateac, ";
            sql += " sale.sadate1 AS sale_sadate1, sale.sadate2 AS sale_sadate2, sale.sadateac1 AS sale_sadateac1,";
            sql += " sale.sadateac2 AS sale_sadateac2, sale.quno AS sale_quno, sale.cono AS sale_cono, ";
            sql += " sale.coname1 AS sale_coname1, sale.coname2 AS sale_coname2, sale.cuno AS sale_cuno, ";
            sql += " sale.cuname2 AS sale_cuname2, sale.cuname1 AS sale_cuname1, sale.cutel1 AS sale_cutel1, ";
            sql += " sale.cuper1 AS sale_cuper1, sale.emno AS sale_emno, sale.emname AS sale_emname, sale.spno AS sale_spno, ";
            sql += " sale.spname AS sale_spname, sale.stno AS sale_stno, sale.stname AS sale_stname, sale.xa1no AS sale_xa1no, ";
            sql += " sale.Xa1Name AS sale_xa1name, sale.xa1par AS sale_xa1par, sale.taxmnyf AS sale_taxmnyf, ";
            sql += " sale.taxmnyb AS sale_taxmnyb, sale.taxmny AS sale_taxmny, sale.x3no AS sale_x3no, sale.rate AS sale_rate, ";
            sql += " sale.x5no AS sale_x5no, sale.seno AS sale_seno, sale.sename AS sale_sename, sale.x4no AS sale_x4no, ";
            sql += " sale.x4name AS sale_x4name, sale.tax AS sale_tax, sale.totmny AS sale_totmny, sale.taxb AS sale_taxb, ";
            sql += " sale.totmnyb AS sale_totmnyb, sale.discount AS sale_discount, sale.cashmny AS sale_cashmny, ";
            sql += " sale.cardmny AS sale_cardmny, sale.cardno AS sale_cardno, sale.ticket AS sale_ticket, ";
            sql += " sale.collectmny AS sale_collectmny, sale.getprvacc AS sale_getprvacc, sale.acctmny AS sale_acctmny, ";
            sql += " sale.samemo AS sale_samemo, sale.samemo1 AS sale_samemo1, sale.bracket AS sale_bracket, ";
            sql += " sale.recordno AS sale_recordno, sale.invno AS sale_invno, sale.invdate AS sale_invdate, ";
            sql += " sale.invdate1 AS sale_invdate1, sale.invname AS sale_invname, sale.invtaxno AS sale_invtaxno, ";
            sql += " sale.invaddr1 AS sale_invaddr1, sale.invbatch AS sale_invbatch, sale.invbatflg AS sale_invbatflg, ";
            sql += " sale.appdate AS sale_appdate, sale.edtdate AS sale_edtdate, sale.appscno AS sale_appscno, ";
            sql += " sale.edtscno AS sale_edtscno, sale.acno AS sale_acno, sale.cloflag AS sale_cloflag,sale.SaPayment as sale_SaPayment, ";
            sql += " sale.SendAddr AS sale_sendaddr, sale.UsrNo AS sale_usrno, saled.saID AS saled_said, saled.sano AS saled_sano, ";


            sql += " saled.sadate AS saled_sadate, saled.sadate1 AS saled_sadate1, saled.sadate2 AS saled_sadate2, ";
            sql += " saled.sadateac AS saled_sadateac, saled.sadateac1 AS saled_sadateac1, saled.sadateac2 AS saled_dateac2, ";
            sql += " saled.quno AS saled_quno, saled.cono AS saled_cono, saled.cuno AS saled_cuno, saled.emno AS saled_emno, ";
            sql += " saled.spno AS saled_spno, saled.stno AS saled_stno, saled.xa1no AS saled_xa1no, ";
            sql += " saled.xa1par AS saled_xa1par, saled.seno AS saled_seno, saled.sename AS saled_sename, ";
            sql += " saled.x4no AS saled_x4no, saled.x4name AS saled_x4name, saled.orno AS saled_orno, saled.itno AS saled_itno, ";
            sql += " saled.itname AS saled_itname, saled.ittrait AS saled_ittrait, saled.itunit AS saled_itunit, ";
            sql += " saled.itpkgqty AS saled_itpkgqty, saled.qty AS saled_qty, saled.price AS saled_price, saled.prs AS saled_prs, ";
            sql += " saled.rate AS saled_rate, saled.taxprice AS saled_taxprice, saled.mny AS saled_mny, ";
            sql += " saled.priceb AS saled_priceb, saled.taxpriceb AS saled_taxpriceb, saled.mnyb AS saled_mnyb, ";
            sql += " saled.memo AS saled_memo, saled.lowzero AS saled_lowzero, saled.bomid AS saled_bomid, ";
            sql += " saled.bomrec AS saled_bomrec, saled.recordno AS saled_recordno, saled.sltflag AS saled_sltflag, ";
            sql += " saled.extflag AS saled_extflag, saled.bracket AS saled_bracket, saled.itdesp1 AS saled_itdesp1, ";
            sql += " saled.itdesp2 AS saled_itdesp2, saled.itdesp3 AS saled_itdesp3, saled.itdesp4 AS saled_itdesp4, ";
            sql += " saled.itdesp5 AS saled_itdesp5, saled.itdesp6 AS saled_itdesp6, saled.itdesp7 AS saled_itdesp7, ";
            sql += " saled.itdesp8 AS saled_itdesp8, saled.itdesp9 AS saled_itdesp9, saled.itdesp10 AS saled_itdesp10, saled.PQty AS saled_PQty,saled.PUnit AS saled_PUnit, ";
            sql += " saled.stName AS saled_stname, saled.RecordNo_D AS saled_recordno_d, cust.cuno AS cust_cuno, ";
            sql += " saled.mwidth1 AS saled_mwidth1,saled.mwidth2 AS saled_mwidth2,saled.mwidth3 AS saled_mwidth3,saled.mwidth4 AS saled_mwidth4, scrit.scname1, saled.Pformula, ";
            //13.6a
            sql += " sale.AdAddr as sale_AdAddr,ISNULL(sale.Adper1, '')  as sale_Adper1,sale.Adtel as sale_Adtel, ";
            sql += " saled.AdAddr as saled_AdAddr,saled.Adper1 as saled_Adper1,saled.Adtel as saled_Adtel,saled.AdName as  saled_AdName, ";
            //型號
            sql += " saled.standard as saled_standard, ";
            sql += " cust.cuname2 AS cust_cuname2, cust.cuname1 AS cust_cuname1, cust.cuinvoname AS cust_cuinvoname, ";
            sql += " cust.cuchkname AS cust_cuchkname, cust.cuxa1no AS cust_cuxa1no, cust.cupareno AS cust_cupareno, ";
            sql += " cust.cucono AS cust_cucono, cust.cuime AS cust_cuime, cust.cux1no AS cust_cux1no, ";
            sql += " cust.cuemno1 AS cust_cuemno1, cust.cuper1 AS cust_cuper1, cust.cuper2 AS cust_cuper2, cust.cuper AS cust_cuper, ";
            sql += " cust.cutel1 AS cust_cutel1, cust.cutel2 AS cust_cutel2, cust.cufax1 AS cust_cufax1, cust.cuatel1 AS cust_cuatel1, ";
            sql += " cust.cuatel2 AS cust_cuatel2, cust.cuatel3 AS cust_cuatel3, cust.cubbc AS cust_cubbc, cust.cuaddr1 AS cust_cuaddr1, ";
            sql += " cust.cur1 AS cust_cur1, cust.cuaddr2 AS cust_cuaddr2, cust.cur2 AS cust_cur2, cust.cuaddr3 AS cust_cuaddr3, ";
            sql += " cust.cur3 AS cust_cur3, cust.cuslevel AS cust_cuslevel, cust.cudisc AS cust_cudisc, cust.cuemail AS cust_cuemail, ";
            sql += " cust.cuwww AS cust_cuwww, cust.cux2no AS cust_cux2no, cust.cuuno AS cust_cuuno, cust.cux3no AS cust_cux3no, ";
            sql += " cust.cux4no AS cust_cux4no, cust.cucredit AS cust_cucredit, cust.cuengname AS cust_cuengname, ";
            sql += " cust.cuengaddr AS cust_cuengaddr, cust.cuengr1 AS cust_cuengr1, cust.cumemo1 AS cust_cumemo1, ";
            sql += " cust.cumemo2 AS cust_cumemo2, cust.cux5no AS cust_cux5no, cust.cuarea AS cust_cuarea, ";
            sql += " cust.cuudf1 AS cust_cuudf1, cust.cuudf2 AS cust_cuudf2, cust.cuudf3 AS cust_cuudf3, cust.cuudf4 AS cust_cuudf4, ";
            sql += " cust.cuudf5 AS cust_cuudf5, cust.cuudf6 AS cust_cuudf6, cust.cudate AS cust_cudate, cust.cudate1 AS cust_cudate1, ";
            sql += " cust.cudate2 AS cust_date2, cust.culastday AS cust_culastday, cust.culastday1 AS cust_culastday1, ";
            sql += " cust.culastday2 AS cust_culastday2, cust.cufirreceiv AS cust_cufirreceiv, cust.cusparercv AS cust_cusparercv, ";
            sql += " cust.cureceiv AS cust_cureceiv, cust.cufirrcvpar AS cust_cufirrcvpar, cust.cufiradvamt AS cust_cufiradvamt, ";
            sql += " cust.cuadvamt AS cust_cuadvamt, cust.cunote AS cust_cunote, cust.cubirth AS cust_cubirth, ";
            sql += " cust.cubirth1 AS cust_cubirth1, cust.cubirth2 AS cust_cubirth2, cust.cusex AS cust_cusex, ";
            sql += " cust.cublood AS cust_cublood, cust.cuidno AS cust_cuidno, cust.ServDate AS cust_servdate, ";
            sql += " cust.BetwDates AS cust_betwdates, cust.MsgFlag AS cust_msgflag, cust.CuPause AS cust_cupause, item.kino, kind.kiname, ";
            if (radio2.Checked || radio3.Checked || radio7.Checked)
            {
                sql += " salebom.SaNo AS salebom_sano, salebom.BomID AS salebom_bomid, salebom.BomRec AS salebom_bomrec, ";
                sql += " salebom.itno AS salebom_itno, salebom.itname AS salebom_itname, salebom.itunit AS salebom_itunit, ";
                sql += " salebom.itqty AS salebom_itqty, salebom.itpareprs AS salebom_itpareprs, salebom.itpkgqty AS salebom_itpkgqty, ";
                sql += " salebom.itrec AS salebom_itrec, salebom.itprice AS salebom_itprice, salebom.itprs AS salebom_itprs, ";
                sql += " salebom.itmny AS salebom_itmny, salebom.itnote AS salebom_itnote, salebom.ItSource AS salebom_itsource, ";
                sql += " salebom.ItBuyPri AS salebom_itbuypri, salebom.ItBuyMny AS salebom_itbuymny, ";
            }
            sql += " item.pic AS item_pic,item.itpicture AS item_showpic,";
            sql += " item.ItUdf1 AS item_ItUdf1 ,";
            sql += " Einvsetup.EinvTitle as iTitle ,Einvsetup.EinvStore as iStore ,Einvsetup.EinvUnno as iUnno ,Einvsetup.EinvTel as iTel ,Einvsetup.EinvAddress as iAddress";

            sql += " FROM sale ";
            sql += " LEFT OUTER JOIN scrit ON sale.appscno = scrit.scname";
            sql += " LEFT OUTER JOIN saled ON sale.sano = saled.sano";
            sql += " LEFT OUTER JOIN cust ON sale.cuno = cust.cuno";
            sql += " LEFT OUTER JOIN item ON saled.itno = item.itno";
            sql += " LEFT OUTER JOIN kind ON item.kino = kind.kino";
            sql += " LEFT OUTER JOIN Einvsetup ON sale.User_Einv = Einvsetup.Einvid";

            if (radio2.Checked || radio3.Checked || radio7.Checked)
            {
                sql += " LEFT OUTER JOIN salebom ON saled.bomid = salebom.BomID";
            }
        }


        private void btnBarCode_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            using (DataTable temp = new DataTable())
            {
                cmd.Parameters.AddWithValue("SaNo", PK);
                cmd.CommandText = "Select SaNo,Sadate,Sadate1 from Sale where SaNo = (@SaNo)";
                da.Fill(temp);
                if (temp.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var date = Common.User_DateTime == 1 ? "sadate" : "sadate1";
                    list.Add(temp.Rows[0]["SaNo"].ToString().Trim(), temp.Rows[0][date].ToString().Trim());
                }
            }

            var value = "1".PadRight(10, ' ') + list.First().Key.PadRight(12, ' ') + Date.AddLine(list.First().Value).PadRight(10, ' ');
            var path = Application.StartupPath + @"\BarCode\BarCode.Txt";
            if (File.Exists(path))
            {
                File.Delete(path);
                File.WriteAllText(path, value);
            }

            path = Application.StartupPath + @"\BarCode\Project1.exe";
            if (File.Exists(path)) Process.Start(path);
            else
            {
                MessageBox.Show("找不到恆錩列印程式，無法列印！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void 三聯式發票列印_Click(object sender, EventArgs e)
        {
            /// 此功能似乎是專案設計的發票列印
            /// 發票版面寫死[SalePrintInvNo]類別
            /// 發票列印PORT抓[使用者參數設定->後台銷貨單據發票列印]
            try
            {
                if (SaNo.Text != SaNo1.Text)
                {
                    MessageBox.Show("列印三聯發票，只能列印一張銷貨單據!");
                    return;
                }

                三聯式發票列印.Enabled = false;

                radio1.Checked = true;
                Application.DoEvents();


                ds.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    setSql();
                    sql += " where '0'='0'";
                    if (SaNo.Text != "")
                        sql += " and sale.sano >=@sano";

                    if (SaNo1.Text.Trim() != "")
                        sql += " and sale.sano <=@sano1";

                    sql += " Order by saled_said ";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("sano1", SaNo1.Text.Trim());
                    cmd.CommandText = sql;


                    da.Fill(ds, "Data");
                    cmd.CommandText = @"
                        Select item.itno, itnoudf, itname, kino, itime, ittrait, itunit, itunitp, itpkgqty, itbuypri, itprice, itprice1, itprice2, itprice3, itprice4, 
                                itprice5, itcost, itbuyprip, itpricep, itpricep1, itpricep2, itpricep3, itpricep4, itpricep5, itcostp, itbuyunit, itsalunit, 
                                itsafeqty, itlastqty, itnw, itnwunit, itcostslt, itcodeslt, itcodeno, itdesp1, itdesp2, itdesp3, itdesp4, itdesp5, itdesp6, 
                                itdesp7, itdesp8, itdesp9, itdesp10, itdate, itdate1, itdate2, itbuydate, itbuydate1, itbuydate2, itsaldate, itsaldate1, 
                                itSaldate2, itfircost, itfirtqty, itfirtcost, itstockqty, itnote, itudf1, itudf2, itudf3, itudf4, itudf5, itweblist, itwebpic, itwebctl1, 
                                itwebctl2, IsUse, ItSource, itpicture, IsEnable, fano, Punit, ScNo
                        from (
		                        Select itno from saled where saled.sano>=(@sano) and saled.sano <=(@sano1) group by itno
	                            )A
                        left join item on A.itno = item.itno ";
                    da.Fill(ds, "item");
                }

                if (ds.Tables["Data"].Rows.Count == 0)
                {
                    MessageBox.Show("查無資料!");
                    return;
                }

                var sp = new JBS.JS.SalePrintInvNo();
                sp.doInv(ds.Tables["Data"]);

                MessageBox.Show("OK!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                三聯式發票列印.Enabled = true;
            }
        }

        private void radio分單與不分單_CheckedChanged(object sender, EventArgs e)
        {
            if (radio分單.Checked)
            {
                CheckBox列印抬頭.Visible = true;
                #region
                radio4.SetUserDefineRpt("銷貨作業系統_幣別簡要表.frx", @"ReportG\");
                radio5.SetUserDefineRpt("銷貨作業系統_簡要自定一.frx", @"ReportG\");
                radio6.SetUserDefineRpt("銷貨作業系統_簡要自定二.frx", @"ReportG\");
                radio7.SetUserDefineRpt("銷貨作業系統_組件自定一.frx", @"ReportG\");
                ToolTip tip = new ToolTip();
                tip.SetToolTip(radio1, "SaleRpt1.frx");
                tip.SetToolTip(radio2, "SaleRpt2.frx");
                tip.SetToolTip(radio3, "SaleRpt3.frx");
                #endregion
            }
            else if (radioT不分單.Checked)
            {
                CheckBox列印抬頭.Visible = false;
                #region
                if (Common.Sys_DBqty == 1)
                {
                    radio4.判斷有無CF或RF("銷貨作業系統_幣別簡要表");
                    radio5.判斷有無CF或RF("銷貨作業系統_簡要自定一");
                    radio6.判斷有無CF或RF("銷貨作業系統_簡要自定二");
                    radio7.判斷有無CF或RF("銷貨作業系統_組件自定一");

                    if (radio4.Controls.Count > 0)
                        radio4.判斷有無CF或RF("SaleRpt4");

                    ToolTip tip = new ToolTip();
                    tip.SetToolTip(radio1, "SaleRpt1");
                    tip.SetToolTip(radio2, "SaleRpt2");
                    tip.SetToolTip(radio3, "SaleRpt3");
                }
                else if (Common.Sys_DBqty == 2)
                {
                    radio4.判斷有無CF或RF("銷貨作業系統_幣別簡要表P");
                    radio5.判斷有無CF或RF("銷貨作業系統_簡要自定一P");
                    radio6.判斷有無CF或RF("銷貨作業系統_簡要自定二P");
                    radio7.判斷有無CF或RF("銷貨作業系統_組件自定一P");

                    ToolTip tip = new ToolTip();
                    tip.SetToolTip(radio1, "SaleRpt1P");
                    tip.SetToolTip(radio2, "SaleRpt2P");
                    tip.SetToolTip(radio3, "SaleRpt3P");
                }
                #endregion
            }
        }
        private void 電子發票證明聯(RptMode mode)
        {
            var path = "";
            dteinvhead.Clear();
            dtoutput.Clear();
            Einvtemp.Clear();
            DataTable sanofindinvo = new DataTable();

            try
            {
                Application.DoEvents();//?
                ds.Clear();
                // path = Common.reportaddress + @"ReportF\電子發票證明聯B2B.frx";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("sano1", SaNo1.Text.Trim());
                    //cmd.Parameters.AddWithValue("coname1", Common.iTitle.ToString());
                    //cmd.Parameters.AddWithValue("couno", Common.iUnno.ToString());

                    string rule = "";
                    if (SaNo.Text != "")
                        rule += " and sano >=@sano";

                    if (SaNo1.Text.Trim() != "")
                        rule += " and sano <=@sano1";
                    cmd.CommandText = "select sano , invno from sale where invno!='' and (x5no = '7' or x5no = '8') " + rule;
                    da.Fill(sanofindinvo);
                    if (sanofindinvo.Rows.Count == 0)
                    {
                        MessageBox.Show("查無資料,你所選的銷貨單沒有可列印的電子發票號碼", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var tempinvno = sanofindinvo.AsEnumerable().ToList().Select(r => r["invno"]).Distinct().ToList();
                    for (int i = 0; i <= tempinvno.Count - 1; i++)
                    {
                        cmd.CommandText = "select"
                        + " EinvUnno as couno,EinvTitle as coname1,itname='',qty=0.0,price=0.0,taxpriceb=0.0,mnyb=0.0,prs=0.0, MAX(einvB2CPrint) einvB2CPrint , "
                        + " invkinds='',x3noname='',InvDateY='',InvDateM='',InvDateSM='',InvDateEM='',detaiPrint ='',qrStr='',detail='',recordno='', "
                        + " invno,invdate,invdate1,invrandom,invtaxno,x3no,SUM(taxmnyb)taxmnyb,SUM(taxb)taxb,SUM(totmnyb)totmnyb,invkind , 錯誤標註='' "
                        + "from"
                        + " ("
                        + "   select EinvUnno,EinvTitle,invno,invdate,invdate1,invrandom,invtaxno,x3no,taxmnyb,taxb,totmnyb,invkind, einvB2CPrint"
                        + "   from sale LEFT JOIN Einvsetup ON sale.User_Einv=Einvsetup.Einvid COLLATE Chinese_Taiwan_Stroke_BIN"
                        + "   where bracket !='前台' and (x5no = '7' or x5no = '8') and invno ='" + tempinvno[i].ToString() + "'"
                        + " union all"
                        + "   select EinvUnno,EinvTitle,invno,invdate,invdate1,invrandom,invtaxno,x3no,-1*taxmnyb as taxmnyb,-1* taxb as taxb ,-1*totmnyb as totmnyb ,invkind, einvB2CPrint"
                        + "   from rsale LEFT JOIN Einvsetup ON rsale.User_Einv=Einvsetup.Einvid COLLATE Chinese_Taiwan_Stroke_BIN"
                        + "   where bracket !='前台' and (x5no = '7' or x5no = '8') and invno ='" + tempinvno[i].ToString() + "'"
                        + " )as tot"
                        + " group by EinvUnno,EinvTitle,invno,invdate,invdate1,invno,invrandom,invtaxno,x3no,invkind";
                        da.Fill(dteinvhead);
                    }
                    DataTable invCount = new DataTable();

                    cmd.Parameters.AddWithValue("invno", "");
                    dtoutput = dteinvhead.Clone();//先複製架構最後丟去印報表的
                    foreach (DataRow row in dteinvhead.Rows)//把該發票檔頭的一些相關資訊補完
                    {
                        invCount = dteinvhead.AsEnumerable().AsParallel().Where(r => r["invno"].ToString().Trim() == row["invno"].ToString().Trim()).CopyToDataTable();
                        if (invCount.Rows.Count > 1)
                        {
                            if (row["錯誤標註"].ToString() != "Y")
                            {
                                MessageBox.Show("不列印發票號碼：" + row["invno"].ToString() + "\n\r ，因有多張單據且發票資訊不一致，請檢查銷貨/銷退作業");
                                dteinvhead.AsEnumerable().Where(r => r["invno"].ToString().Trim() == row["invno"].ToString().Trim()).ToList().ForEach(r => r["錯誤標註"] = "Y");
                            }
                            continue;
                        }
                        if (row["totmnyb"].ToDecimal() < 0)
                        {
                            MessageBox.Show("發票號碼：" + row["invno"].ToString().Trim() + " 發票金額為負數");
                            row["錯誤標註"] = "Y";
                            continue;
                        }
                        if (row["x3no"].ToInteger() == 1 || row["x3no"].ToInteger() == 2)
                        {
                            row["x3noname"] = "應稅";
                        }
                        else if (row["x3no"].ToInteger() == 3)
                        {
                            row["x3noname"] = "零稅";
                        }
                        else if (row["x3no"].ToInteger() == 4)
                        {
                            row["x3noname"] = "免稅";
                        }

                        if (row["invkind"].ToString() == "37")
                        { row["invkinds"] = "不得扣抵"; }
                        else
                        { row["invkinds"] = "25"; }

                        row["InvDateY"] = row["invdate"].ToString().Substring(0, 3);
                        row["InvDateM"] = row["invdate"].ToString().Substring(3, 2);

                        if (row["invdate"].ToString().Substring(3, 2).ToInteger() % 2 == 0)
                        {
                            row["InvDateEM"] = row["invdate"].ToString().Substring(3, 2);
                            row["InvDateSM"] = (row["InvDateEM"].ToInteger() - 1).ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            row["InvDateSM"] = row["invdate"].ToString().Substring(3, 2);
                            row["InvDateEM"] = (row["InvDateSM"].ToInteger() + 1).ToString().PadLeft(2, '0');
                        }
                        if (radioTB2CD.Checked == true)
                        {
                            if (row["invtaxno"].ToString() == "")
                            {
                                row["detaiPrint"] = "V";
                            }
                        }
                        #region 發票明細
                        dteinvbody.Clear();
                        cmd.Parameters["invno"].Value = row["invno"].ToString();
                        //先檢查有沒有(批開)已改發票明細
                        cmd.CommandText = "select count(invno) from batch where invno=@invno";
                        if (cmd.ExecuteScalar().ToDecimal() > 0) //抓已改發票明細itname='',qty='',price='',taxpriceb='',mnyb='',prs=''
                        {
                            cmd.CommandText = @"select invno,itname, qty,price,taxprice as taxpriceb,mny as mnyb, prs from batchd where invno=@invno order by recordno";
                            da.Fill(dteinvbody);
                        }
                        else
                        {
                            //抓銷貨單
                            cmd.CommandText = @"select einvB2CPrint,invno,itname, qty ,price, taxpriceb ,mnyb, prs from 
                                (
                                select sano,invno,einvB2CPrint from sale where invno=@invno
                                )sale inner join saled on sale.sano=saled.sano order by recordno";
                            da.Fill(dteinvbody);

                            //抓銷退單
                            cmd.CommandText = @"select einvB2CPrint,invno,itname,-1*qty  as qty ,price,taxpriceb ,-1*mnyb as mnyb , prs from 
                                (
                                select sano,invno,einvB2CPrint from rsale where invno=@invno
                                )rsale inner join rsaled on rsale.sano=rsaled.sano order by recordno";
                            da.Fill(dteinvbody);
                        }
                        if (dteinvbody.Rows.Count == 0)
                        {
                            MessageBox.Show("發票號碼:" + row["invno"].ToString() + "查無發票明細!!\n查詢作業停止!!", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row["錯誤標註"] = "Y";
                            continue;
                        }
                        else
                        {
                            PrintQRCode(row);
                            row["qrStr"] = qrStr;
                            row["detail"] = detail;
                            row["recordno"] = dteinvbody.Rows.Count;
                            DataRow workRow;
                            Einvtemp = dtoutput.Clone();
                            Einvtemp.Clear();
                            for (int i = 0; i <= dteinvbody.Rows.Count - 1; i++)
                            {
                                workRow = dtoutput.NewRow();
                                workRow["coname1"] = row["coname1"];
                                workRow["couno"] = row["couno"];
                                workRow["itname"] = dteinvbody.Rows[i]["itname"];
                                workRow["qty"] = dteinvbody.Rows[i]["qty"];
                                workRow["price"] = dteinvbody.Rows[i]["price"];
                                workRow["taxpriceb"] = dteinvbody.Rows[i]["taxpriceb"];
                                workRow["mnyb"] = dteinvbody.Rows[i]["mnyb"];
                                workRow["prs"] = dteinvbody.Rows[i]["prs"];
                                workRow["invkinds"] = row["invkinds"];
                                workRow["x3noname"] = row["x3noname"];
                                workRow["InvDateY"] = row["InvDateY"];
                                workRow["InvDateM"] = row["InvDateM"];
                                workRow["InvDateSM"] = row["InvDateSM"];
                                workRow["InvDateEM"] = row["InvDateEM"];
                                workRow["detaiPrint"] = row["detaiPrint"];
                                workRow["qrStr"] = row["qrStr"];
                                workRow["detail"] = row["detail"];
                                workRow["recordno"] = i + 1;
                                workRow["invno"] = row["invno"];
                                workRow["invdate"] = row["invdate"];
                                workRow["invdate1"] = workRow["invdate1"] = row["invdate1"].ToString().Substring(0, 4) + "-" + row["invdate1"].ToString().Substring(4, 2) + "-" + row["invdate1"].ToString().Substring(6, 2);
                                workRow["invrandom"] = row["invrandom"];
                                workRow["invtaxno"] = row["invtaxno"].ToString().Trim().Length == 0 ? "00000000" : row["invtaxno"];
                                workRow["x3no"] = row["x3no"];
                                workRow["taxmnyb"] = row["taxmnyb"];
                                workRow["taxb"] = row["taxb"];
                                workRow["totmnyb"] = row["totmnyb"];
                                if (rdyes.Checked)
                                    workRow["einvB2CPrint"] = "V";
                                else
                                    workRow["einvB2CPrint"] = "";
                                workRow["invkind"] = row["invkind"];
                                workRow["錯誤標註"] = row["錯誤標註"];
                                // workRow["istore"] = Common.iStore;
                                dtoutput.Rows.Add(workRow);
                                Einvtemp.ImportRow(workRow);
                            }
                            if (dtoutput.Rows.Count == 0)
                            {
                                MessageBox.Show("沒有可列印/預覽的發票", "提示訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {

                                if (Einvtemp.Rows[0]["invtaxno"].ToString().Trim() == "" || Einvtemp.Rows[0]["invtaxno"].ToString().Trim() == "00000000")
                                    path = Common.reportaddress + @"ReportF\電子發票證明聯B2C.frx";
                                else
                                    path = Common.reportaddress + @"ReportF\電子發票證明聯B2B.frx";

                                if (System.IO.File.Exists(path) == false)
                                {
                                    MessageBox.Show("找不到發票報表", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                using (var fs = new JBS.FReport())
                                {
                                    fs.ShowDialog = true;
                                    fs.OutReport(mode, Einvtemp, "invprint", path);
                                }

                                //tempinv.Clear();
                                cmd.Parameters.Clear();
                                //sanofindinvo
                                cmd.Parameters.AddWithValue("invno", row["invno"].ToString());
                                cmd.Parameters.AddWithValue("einvB2CPrint", row["einvB2CPrint"].ToInteger() + 1);//B2C 印過?
                                cmd.CommandText = @"update sale SET einvB2CPrint= @einvB2CPrint WHERE invno = @invno";
                                cmd.ExecuteNonQuery();
                                if (radioTB2CD.Checked && (Einvtemp.Rows[0]["invtaxno"].ToString().Trim() == "" || Einvtemp.Rows[0]["invtaxno"].ToString().Trim() == "00000000"))
                                {
                                    path = Common.reportaddress + @"ReportF\電子發票證明聯B2C明細.frx";
                                    using (var fs = new JBS.FReport())
                                    {
                                        fs.ShowDialog = true;
                                        fs.OutReport(mode, Einvtemp, "invprint", path);
                                    }
                                }
                            }

                        }
                        #endregion

                    }

                    if (dteinvhead.AsEnumerable().Count(r => r["錯誤標註"].ToString() == "Y") > 0)
                    {
                        using (ErrorInvoice frm = new ErrorInvoice())
                        {
                            frm.tb = dteinvhead.AsEnumerable().Where(r => r["錯誤標註"].ToString() == "Y").CopyToDataTable();
                            frm.ShowDialog();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
        private void PrintQRCode(DataRow row)//匯出QRCode
        {
            rand4 = row["invrandom"].ToString();
            qrStr = "";
            try
            {
                string invnum = row["invno"].ToString();
                string invdate = row["InvDate"].ToString().Substring(0, 7);
                string invtime = DateTime.Now.ToString("HHmmss");
                string invrand = row["invrandom"].ToString();
                decimal salesamount = Convert.ToString(row["taxmnyb"].ToInteger()).ToString().PadLeft(8, '0').ToDecimal();
                decimal tatalamount = Convert.ToString(row["totmnyb"].ToInteger()).ToString().PadLeft(8, '0').ToDecimal();//Convert.ToInt16(row["totmnyb"].ToDecimal("f" + Common.MST)).ToString().PadLeft(8, '0');
                decimal taxamount = tatalamount - salesamount;
                string buyid = row["invtaxno"].ToString().Trim() == "" ? "00000000" : row["invtaxno"].ToString().Trim();

                string myid = row["couno"].ToString().Trim();
                string AESkey = "FA7F886F80B107E99918187D6C9768C3";//97402894

                com.tradevan.qrutil.QREncrypter qrEncrypter = new com.tradevan.qrutil.QREncrypter();

                string[][] abc = new String[1][];
                String result = qrEncrypter.QRCodeINV(invnum, invdate, invtime, invrand, salesamount, taxamount, tatalamount, buyid, buyid, buyid, myid, AESkey);
                qrStr = result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            //qrStr += row["invno"].ToString();
            //qrStr += row["InvDate"].ToString();
            //qrStr += row["invrandom"].ToString();


            //qrStr += Convert.ToString(row["taxmnyb"].ToInteger(), 16).ToString().PadLeft(8, '0');
            //qrStr += Convert.ToString(row["totmnyb"].ToInteger(), 16).ToString().PadLeft(8, '0');//Convert.ToInt16(row["totmnyb"].ToDecimal("f" + Common.MST)).ToString().PadLeft(8, '0');
            //qrStr += row["invtaxno"].ToString().Trim() == "" ? "00000000" : row["invtaxno"].ToString().Trim();
            //qrStr += Common.Sys_Stctaxno.ToString().Trim();

            //qrStr += getAes(row["invtaxno"].ToString().Trim() == "" ? "00000000" + row["invrandom"].ToString().Trim() : row["invtaxno"].ToString().Trim() + row["invrandom"].ToString().Trim());

            qrStr += ":**********";

            qrStr += ":" + dteinvbody.Rows.Count;//二維條碼記載完整品目筆數
            qrStr += ":" + dteinvbody.Rows.Count;//交易品目總筆數
            qrStr += ":1";//UTF-8編碼//
           
                detail = "**";
                UTF8Encoding utf8 = new UTF8Encoding();
                foreach (DataRow i in dteinvbody.Rows)
                {
                    if (detail.Length + i["itname"].ToString().Length + i["qty"].ToDecimal().ToString("f0").Length + i["taxpriceb"].ToDecimal().ToString("f0").Length >= 105)
                     {
                         break;
                     }
                    detail += ":" + i["itname"].ToString();
                    detail += ":" + i["qty"].ToDecimal().ToString("f0");
                    detail += ":" + i["taxpriceb"].ToDecimal().ToString("f0");     
                }
        }
        string getAes(string InvTaxNo)//加密
        {
            string encrypt = "";
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] key = Encoding.ASCII.GetBytes(Common.iUnno.Trim().ToString());
                byte[] iv = Encoding.ASCII.GetBytes(InvTaxNo.Substring(0,8));//買方統編
                byte[] dataByteArray = Encoding.UTF8.GetBytes(InvTaxNo);
                des.Key = key;
                des.IV = iv;
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return encrypt;
        }
        string setAes(string InvTaxNo)// AES解密 
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(Common.iUnno.Trim().ToString());
            byte[] iv = Encoding.ASCII.GetBytes(InvTaxNo.Substring(0, 8));
            des.Key = key;
            des.IV = iv;
            string str = getAes("UP808600019999");
            byte[] dataByteArray = Convert.FromBase64String(str.Trim());
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        private void radioEinv_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEinv.Checked == true)
            {
                pnlPage.Enabled = false;
                pnlOrder.Enabled = false;
                pnlPrice.Enabled = false;
                pnlMemo.Enabled = false;
                pnlEnd.Enabled = false;
                groupBoxT1.Enabled = false;
                pnlTitle.Enabled = false;
                EinvB2CD.Enabled = true;
                Einvsecond.Enabled = true;
            }
            else
            {
                pnlPage.Enabled = true;
                pnlOrder.Enabled = true;
                pnlPrice.Enabled = true;
                pnlMemo.Enabled = true;
                pnlEnd.Enabled = true;
                groupBoxT1.Enabled = true;
                pnlTitle.Enabled = true;
                EinvB2CD.Enabled = false;
                Einvsecond.Enabled = false;
            }
        }

        private void btnPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (radioEinv.Checked)
                {
                    var dl = MessageBox.Show("是否要修改B2B電子發票表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dl == DialogResult.Yes)
                    {
                        path = Common.reportaddress + @"ReportF\電子發票證明聯B2B.frx";
                        FReport.Design(path);
                    }
                }
                else
                {

                    var dl = MessageBox.Show("是否要修改報表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dl != DialogResult.Yes) return;
                    
                    if (radioT不分單.Checked)
                    {
                        ReportPath = Common.reportaddress + "ReportNew\\" + ReportFileName;
                        bool IsUserDefine = false;//是否為自定報表
                        if (radio4.Checked) IsUserDefine = true;
                        else if (radio5.Checked) IsUserDefine = true;
                        else if (radio6.Checked) IsUserDefine = true;
                        else if (radio7.Checked) IsUserDefine = true;

                        if (!IsUserDefine)
                        {
                            if (radio8.Checked) ReportPath += "B";
                            if (radio9.Checked) ReportPath += "A";
                            if (radio10.Checked) ReportPath += "O";
                            if (radio11.Checked) ReportPath += "";
                            if (radio14.Checked) ReportPath += "M";
                            if (radio15.Checked) ReportPath += "";
                        }

                        string udfPath = Common.reportaddress + "ReportNew\\";
                        if (Common.Sys_DBqty == 1)
                        {
                            #region Common.Sys_DBqty == 1
                            if (radio1.Checked)
                            {
                                if (File.Exists(udfPath + "SaleRpt1.frx"))
                                {
                                    FReport.Design(udfPath + "SaleRpt1.frx");
                                }
                                else
                                {
                                    if (File.Exists(ReportPath + "_簡要表.frx"))
                                    {
                                        FReport.Design(ReportPath + "_簡要表.frx");
                                    }
                                }
                            }
                            else if (radio2.Checked)
                            {
                                if (File.Exists(udfPath + "SaleRpt2.frx"))
                                {
                                    FReport.Design(udfPath + "SaleRpt2.frx");
                                }
                                else
                                {
                                    if (File.Exists(ReportPath + "_組件明細.frx"))
                                    {
                                        FReport.Design(ReportPath + "_組件明細.frx");
                                    }
                                }
                            }
                            else if (radio3.Checked)
                            {
                                if (File.Exists(udfPath + "SaleRpt3.frx"))
                                {
                                    FReport.Design(udfPath + "SaleRpt3.frx");
                                }
                                else
                                {
                                    if (File.Exists(ReportPath + "_組件明細_規格.frx"))
                                    {
                                        FReport.Design(ReportPath + "_組件明細_規格.frx");
                                    }
                                }
                            }
                            else if (radio4.Checked)
                            {
                                if (File.Exists(udfPath + "SaleRpt4.frx"))
                                {
                                    FReport.Design(udfPath + "SaleRpt4.frx");
                                }
                                else
                                {
                                    if (File.Exists(ReportPath + "_幣別簡要表.frx"))
                                    {
                                        FReport.Design(ReportPath + "_幣別簡要表.frx");
                                    }
                                }
                            }
                            else if (radio5.Checked)
                            {
                                if (File.Exists(ReportPath + "_簡要自定一.frx"))
                                {
                                    FReport.Design(ReportPath + "_簡要自定一.frx");
                                }
                            }
                            else if (radio6.Checked)
                            {
                                if (File.Exists(ReportPath + "_簡要自定二.frx"))
                                {
                                    FReport.Design(ReportPath + "_簡要自定二.frx");
                                }
                            }
                            else if (radio7.Checked)
                            {
                                if (File.Exists(ReportPath + "_組件自定一.frx"))
                                {
                                    FReport.Design(ReportPath + "_組件自定一.frx");
                                }
                            }
                            #endregion
                        }
                        else if (Common.Sys_DBqty == 2)
                        {
                            #region Sys_DBqty == 2
                            if (radio1.Checked)
                            {
                                if (File.Exists(udfPath + "SaleRpt1P.frx"))
                                {
                                    FReport.Design(udfPath + "SaleRpt1P.frx");
                                }
                                else
                                {
                                    if (File.Exists(ReportPath + "_簡要表P.frx"))
                                    {
                                        FReport.Design(ReportPath + "_簡要表P.frx");
                                    }
                                }
                            }
                            else if (radio2.Checked)
                            {
                                if (File.Exists(udfPath + "SaleRpt2P.frx"))
                                {
                                    FReport.Design(udfPath + "SaleRpt2P.frx");
                                }
                                else
                                {
                                    if (File.Exists(ReportPath + "_組件明細P.frx"))
                                    {
                                        FReport.Design(ReportPath + "_組件明細P.frx");
                                    }
                                }
                            }
                            else if (radio3.Checked)
                            {
                                if (File.Exists(udfPath + "SaleRpt3P.frx"))
                                {
                                    FReport.Design(udfPath + "SaleRpt3P.frx");
                                }
                                else
                                {
                                    if (File.Exists(ReportPath + "_組件明細_規格P.frx"))
                                    {
                                        FReport.Design(ReportPath + "_組件明細_規格P.frx");
                                    }
                                }
                            }
                            else if (radio4.Checked)
                            {
                                if (File.Exists(ReportPath + "_幣別簡要表P.frx"))
                                {
                                    FReport.Design(ReportPath + "_幣別簡要表P.frx");
                                }
                            }
                            else if (radio5.Checked)
                            {
                                if (File.Exists(ReportPath + "_簡要自定一P.frx"))
                                {
                                    FReport.Design(ReportPath + "_簡要自定一P.frx");
                                }
                            }
                            else if (radio6.Checked)
                            {
                                if (File.Exists(ReportPath + "_簡要自定二P.frx"))
                                {
                                    FReport.Design(ReportPath + "_簡要自定二P.frx");
                                }
                            }
                            else if (radio7.Checked)
                            {
                                if (File.Exists(ReportPath + "_組件自定一P.frx"))
                                {
                                    FReport.Design(ReportPath + "_組件自定一P.frx");
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        ReportPath = Common.reportaddress + "ReportG\\" + ReportFileName;
                        if (radio8.Checked) ReportPath += "B";
                        else ReportPath += "A";
                        //訂單列印
                        if (radio10.Checked) ReportPath += "O";
                        //報表內容
                        if (radio1.Checked)
                            ReportPath += "_簡要表P";
                        else if (radio2.Checked)
                            ReportPath += "_組件明細P";
                        else if (radio3.Checked)
                            ReportPath += "_組件明細_規格P";
                        else if (radio4.Checked)
                            ReportPath = "銷貨作業系統_幣別簡要表";
                        else if (radio5.Checked)
                            ReportPath = "銷貨作業系統_簡要自定一";
                        else if (radio6.Checked)
                            ReportPath = "銷貨作業系統_簡要自定二";
                        else if (radio7.Checked)
                            ReportPath = "銷貨作業系統_組件自定一";
                        if (File.Exists(ReportPath + ".frx"))
                        {
                            FReport.Design(ReportPath + ".frx");
                        }
                    }
                }
            }
        }

        private void btnPreView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//目前只做可以修改電子發票的報表
            {
                if (radioEinv.Checked)
                {
                    var dl = MessageBox.Show("是否要修改B2C電子發票表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dl == DialogResult.Yes)
                    {
                        path = Common.reportaddress + @"ReportF\電子發票證明聯B2C.frx";
                        FReport.Design(path);
                    }
                }
            }
        }
        private void btnWord_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//目前只做可以修改電子發票的報表
            {
                if (radioEinv.Checked)
                {
                    var dl = MessageBox.Show("是否要修改B2C銷項明細表?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dl == DialogResult.Yes)
                    {
                        path = Common.reportaddress + @"ReportF\電子發票證明聯B2C明細.frx";
                        FReport.Design(path);
                    }
                }
            }
        }

        string 確認CF或FF(string path)
        {
            var testPath = path.Replace("Report", "ReportNew") + ".frx";
            if (File.Exists(testPath))
            {
                return testPath;
            }

            testPath = path + ".rpt";
            if (File.Exists(testPath))
            {
                return testPath;
            }

            return null;
        }
        void FastReport列印(string path, RptMode mode)
        {
            DataTable printTB = new DataTable();
            printTB.TableName = "SALE_";
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
            {
                sql = "";
                sql += " SELECT sale.sano AS sale_銷貨單號, sale.sadate AS sale_銷貨日期_民國, sale.sadateac AS sale_帳款日期_民國, ";
                sql += " sale.sadate1 AS sale_銷貨日期_西元, sale.sadate2 AS sale_銷貨日期_保留, sale.sadateac1 AS sale_帳款日期_西元,";
                sql += " sale.sadateac2 AS sale_帳款日期_保留, sale.quno AS sale_報價憑證, sale.cono AS sale_公司編號, ";
                sql += " sale.coname1 AS sale_公司簡稱, sale.coname2 AS sale_公司名稱, sale.cuno AS sale_客戶編號, ";
                sql += " sale.cuname2 AS sale_客戶名稱, sale.cuname1 AS sale_客戶簡稱, sale.cutel1 AS sale_客戶電話, ";
                sql += " sale.cuper1 AS sale_客戶聯絡人, sale.emno AS sale_業務編號, sale.emname AS sale_業務姓名, sale.spno AS sale_專案編號, ";
                sql += " sale.spname AS sale_專案名稱, sale.stno AS sale_出貨倉庫編號, sale.stname AS sale_出貨倉庫名稱, sale.xa1no AS sale_幣別編號, ";
                sql += " sale.Xa1Name AS sale_幣別名稱, sale.xa1par AS sale_匯率, sale.taxmnyf AS sale_taxmnyf, ";
                sql += " sale.taxmnyb AS sale_本幣稅前合計, sale.taxmny AS sale_外幣稅前合計, sale.x3no AS sale_稅別編號, sale.rate AS sale_稅率, ";
                sql += " sale.x5no AS sale_發票種類, sale.seno AS sale_送貨編號, sale.sename AS sale_送貨名稱, sale.x4no AS sale_結帳類別編號, ";
                sql += " sale.x4name AS sale_結帳類別名稱, sale.tax AS sale_外幣營業稅額, sale.totmny AS sale_外幣總額, sale.taxb AS sale_本幣營業稅額, ";
                sql += " sale.totmnyb AS sale_本幣總額, sale.discount AS sale_折扣金額, sale.cashmny AS sale_收現金額, ";
                sql += " sale.cardmny AS sale_刷卡金額, sale.cardno AS sale_刷卡卡號, sale.ticket AS sale_禮卷金額, ";
                sql += " sale.collectmny AS sale_已收金額, sale.getprvacc AS sale_取用預收, sale.acctmny AS sale_未收金額, ";
                sql += " sale.samemo AS sale_備註, sale.samemo1 AS sale_詳細備註, sale.bracket AS sale_表單類型, ";
                sql += " sale.recordno AS sale_recordno, sale.invno AS sale_發票編號, sale.invdate AS sale_發票日期_民國, ";
                sql += " sale.invdate1 AS 發票日期_西元, sale.invname AS 發票抬頭, sale.invtaxno AS sale_統一編號, ";
                sql += " sale.invaddr1 AS sale_發票地址, sale.invbatch AS sale_發票批開選定, sale.invbatflg AS sale_批開發票標記, ";
                sql += " sale.appdate AS sale_新增日期時間, sale.edtdate AS sale_修改日期時間, sale.appscno AS sale_登錄人員, ";
                sql += " sale.edtscno AS sale_最後修改人員, sale.acno AS sale_傳票編號, sale.cloflag AS sale_cloflag,sale.SaPayment as sale_付款條件, ";
                sql += " sale.SendAddr AS sale_sendaddr, sale.UsrNo AS sale_usrno, saled.saID AS saled_said, saled.sano AS saled_銷貨單號, ";
                sql += " saled.sadate AS saled_銷貨日期_民國, saled.sadate1 AS saled_銷貨日期_西元, saled.sadate2 AS saled_銷貨日期_保留, ";
                sql += " saled.sadateac AS saled_帳款日期_民國, saled.sadateac1 AS saled_帳款日期_西元, saled.sadateac2 AS saled_帳款日期_保留, ";
                sql += " saled.quno AS saled_報價憑證, saled.cono AS saled_公司編號, saled.cuno AS saled_客戶編號, saled.emno AS saled_業務編號, ";
                sql += " saled.spno AS saled_專案編號, saled.stno AS saled_出貨倉庫編號, saled.xa1no AS saled_幣別編號, ";
                sql += " saled.xa1par AS saled_匯率, saled.seno AS saled_送貨編號, saled.sename AS saled_送貨名稱, ";
                sql += " saled.x4no AS saled_結帳類別編號, saled.x4name AS saled_結帳類別名稱, saled.orno AS saled_訂單單號, saled.itno AS saled_產品編號, ";
                sql += " saled.itname AS saled_品名規格, saled.ittrait AS saled_產品組成, saled.itunit AS saled_單位, ";
                sql += " saled.itpkgqty AS saled_包裝數量, saled.qty AS saled_數量, saled.price AS saled_外幣單價, saled.prs AS saled_折數, ";
                sql += " saled.rate AS saled_稅率, saled.taxprice AS saled_外幣稅前單價, saled.mny AS saled_外幣稅前金額, ";
                sql += " saled.priceb AS saled_本幣單價, saled.taxpriceb AS saled_本幣稅前單價, saled.mnyb AS 本幣稅前金額, ";
                sql += " saled.memo AS saled_備註說明, saled.lowzero AS saled_lowzero, saled.bomid AS saled_bomid, ";
                sql += " saled.bomrec AS saled_bomrec, saled.recordno AS saled_recordno, saled.sltflag AS saled_sltflag, ";
                sql += " saled.extflag AS saled_extflag, saled.bracket AS saled_表單類型, saled.itdesp1 AS saled_規格說明1, ";
                sql += " saled.itdesp2 AS saled_規格說明2, saled.itdesp3 AS saled_規格說明3, saled.itdesp4 AS saled_規格說明4, ";
                sql += " saled.itdesp5 AS saled_規格說明5, saled.itdesp6 AS saled_規格說明6, saled.itdesp7 AS saled_規格說明7, ";
                sql += " saled.itdesp8 AS saled_規格說明8, saled.itdesp9 AS saled_規格說明9, saled.itdesp10 AS saled_規格說明10, saled.PQty AS saled_PQty,saled.PUnit AS saled_PUnit, ";
                sql += " saled.stName AS saled_出貨倉庫名稱, saled.RecordNo_D AS saled_recordno_d, cust.cuno AS cust_客戶編號, ";
                sql += " saled.mwidth1 AS saled_mwidth1,saled.mwidth2 AS saled_mwidth2,saled.mwidth3 AS saled_mwidth3,saled.mwidth4 AS saled_mwidth4, scrit.scname1, saled.Pformula, ";
                //13.6a
                sql += " sale.AdAddr as sale_AdAddr,ISNULL(sale.Adper1, '')  as sale_客戶收件人,sale.Adtel as sale_Adtel, ";
                sql += " saled.AdAddr as saled_AdAddr,saled.Adper1 as saled_Adper1,saled.Adtel as saled_Adtel,saled.AdName as  saled_AdName, ";
                //型號
                sql += " saled.standard as saled_型號, ";
                sql += " cust.cuname2 AS cust_客戶名稱, cust.cuname1 AS cust_客戶簡稱, cust.cuinvoname AS cust_發票抬頭, ";
                sql += " cust.cuchkname AS cust_支票抬頭, cust.cuxa1no AS cust_交易幣別編號, cust.cupareno AS cust_隸屬經銷編號, ";
                sql += " cust.cucono AS cust_隸屬公司編號, cust.cuime AS cust_注音速查, cust.cux1no AS cust_客戶類別編號, ";
                sql += " cust.cuemno1 AS cust_業務人員編號, cust.cuper1 AS cust_聯絡人1, cust.cuper2 AS cust_聯絡人2, cust.cuper AS cust_負責人, ";
                sql += " cust.cutel1 AS cust_電話1, cust.cutel2 AS cust_電話2, cust.cufax1 AS cust_傳真, cust.cuatel1 AS cust_行動電話1, ";
                sql += " cust.cuatel2 AS cust_行動電話2, cust.cuatel3 AS cust_080電話, cust.cubbc AS cust_cubbc, cust.cuaddr1 AS cust_公司地址, ";
                sql += " cust.cur1 AS cust_公司地址郵遞區號, cust.cuaddr2 AS cust_發票地址, cust.cur2 AS cust_發票地址郵遞區號, cust.cuaddr3 AS cust_送貨地址, ";
                sql += " cust.cur3 AS cust_送貨地址郵遞區號, cust.cuslevel AS cust_售價等級, cust.cudisc AS cust_折數, cust.cuemail AS cust_電子信箱, ";
                sql += " cust.cuwww AS cust_網址, cust.cux2no AS cust_區域名稱編號, cust.cuuno AS cust_統編, cust.cux3no AS cust_營業稅編號, ";
                sql += " cust.cux4no AS cust_結帳類別編號, cust.cucredit AS cust_信用額度, cust.cuengname AS cust_英文抬頭, ";
                sql += " cust.cuengaddr AS cust_英文地址, cust.cuengr1 AS cust_英文郵遞區號, cust.cumemo1 AS cust_備註ㄧ, ";
                sql += " cust.cumemo2 AS cust_備註二, cust.cux5no AS cust_發票模式編號, cust.cuarea AS cust_地區類別, ";
                sql += " cust.cuudf1 AS cust_自定ㄧ, cust.cuudf2 AS cust_自定二, cust.cuudf3 AS cust_自定三, cust.cuudf4 AS cust_自定四, ";
                sql += " cust.cuudf5 AS cust_自定五, cust.cuudf6 AS cust_自定六, cust.cudate AS cust_建檔日期_民國, cust.cudate1 AS cust_建檔日期_西元, ";
                sql += " cust.cudate2 AS cust_建檔日期_保留, cust.culastday AS cust_最近交易_民國, cust.culastday1 AS cust_最近交易_西元, ";
                sql += " cust.culastday2 AS cust_最近交易_保留, cust.cufirreceiv AS cust_期初應收帳款金額, cust.cusparercv AS cust_期初應收帳款餘額, ";
                sql += " cust.cureceiv AS cust_現有應收帳款金額, cust.cufirrcvpar AS cust_期初應收帳款匯率, cust.cufiradvamt AS cust_期初預收餘額, ";
                sql += " cust.cuadvamt AS cust_預收餘額, cust.cunote AS cust_備忘錄, cust.cubirth AS cust_出生日期_民國, ";
                sql += " cust.cubirth1 AS cust_出生日期_西元, cust.cubirth2 AS cust_出生日期_保留, cust.cusex AS cust_性別, ";
                sql += " cust.cublood AS cust_血型, cust.cuidno AS cust_身分證, cust.ServDate AS cust_servdate, ";
                sql += " cust.BetwDates AS cust_betwdates, cust.MsgFlag AS cust_msgflag, cust.CuPause AS cust_cupause, item.kino, kind.kiname, ";
                if (radio2.Checked || radio3.Checked || radio7.Checked)
                {
                    sql += " salebom.SaNo AS salebom_報價單號, salebom.BomID AS salebom_bomid, salebom.BomRec AS salebom_bomrec, ";
                    sql += " salebom.itno AS salebom_產品編號, salebom.itname AS salebom_品名規格, salebom.itunit AS salebom_單位, ";
                    sql += " salebom.itqty AS salebom_標準用量, salebom.itpareprs AS salebom_母件比率, salebom.itpkgqty AS salebom_包裝數量, ";
                    sql += " salebom.itrec AS salebom_itrec, salebom.itprice AS salebom_產品單價, salebom.itprs AS salebom_產品折數, ";
                    sql += " salebom.itmny AS salebom_產品稅前金額, salebom.itnote AS salebom_產品說明, salebom.ItSource AS salebom_itsource, ";
                    sql += " salebom.ItBuyPri AS salebom_itbuypri, salebom.ItBuyMny AS salebom_itbuymny, ";
                }
                sql += " item.pic AS item_pic,item.itpicture AS item_showpic,DATALENGTH(item.pic)長度,";
                sql += " item.ItUdf1 AS item_ItUdf1 ";
                sql += " FROM sale ";
                sql += " LEFT OUTER JOIN scrit ON sale.appscno = scrit.scname";
                sql += " LEFT OUTER JOIN saled ON sale.sano = saled.sano";
                sql += " LEFT OUTER JOIN cust ON sale.cuno = cust.cuno";
                sql += " LEFT OUTER JOIN item ON saled.itno = item.itno";
                sql += " LEFT OUTER JOIN kind ON item.kino = kind.kino";
                if (radio2.Checked || radio3.Checked || radio7.Checked)
                {
                    sql += " LEFT OUTER JOIN salebom ON saled.bomid = salebom.BomID";
                }

                sql += " where '0'='0'";
                if (SaNo.Text != "")
                    sql += " and sale.sano >=@sano";

                if (SaNo1.Text.Trim() != "")
                    sql += " and sale.sano <=@sano1";

                sql += " Order by saled_said ";

                cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                cmd.Parameters.AddWithValue("sano1", SaNo1.Text.Trim());
                cmd.CommandText = sql;
                dd.Fill(printTB);
            }

            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                //報表參數

                //公司抬頭
                var txtstart = "";
                if (rdHeader1.Checked) txtstart = Common.dtstart.Rows[0]["pnname"].ToString();
                else if (rdHeader2.Checked) txtstart = Common.dtstart.Rows[1]["pnname"].ToString();
                else if (rdHeader3.Checked) txtstart = Common.dtstart.Rows[2]["pnname"].ToString();
                else if (rdHeader4.Checked) txtstart = Common.dtstart.Rows[3]["pnname"].ToString();
                else if (rdHeader5.Checked) txtstart = Common.dtstart.Rows[4]["pnname"].ToString();
                else txtstart = "";
                FastReport.dy.Add("txtstart", txtstart);
                //銷貨單標題
                var title = Common.Sys_SaleHead;
                FastReport.dy.Add("title", title);
                //三行註腳
                var txtend ="";
                if (rdFooter1.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
                else if (rdFooter2.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
                else if (rdFooter3.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
                else if (rdFooter4.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
                else if (rdFooter5.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
                else txtend = "";
                FastReport.dy.Add("txtend", txtend);
                //表頭-公司住址
                var txtadress = "";
                if (rdHeader1.Checked) txtadress = "    " + Common.dtstart.Rows[0]["pnaddr"].ToString();
                else if (rdHeader2.Checked) txtadress = "    " + Common.dtstart.Rows[1]["pnaddr"].ToString();
                else if (rdHeader3.Checked) txtadress = "    " + Common.dtstart.Rows[2]["pnaddr"].ToString();
                else if (rdHeader4.Checked) txtadress = "    " + Common.dtstart.Rows[3]["pnaddr"].ToString();
                else if (rdHeader5.Checked) txtadress = "    " + Common.dtstart.Rows[4]["pnaddr"].ToString();
                else txtadress = "";
                FastReport.dy.Add("txtadress", txtadress);
                //表頭-電話、傳真
                var txttel = "";
                if (rdHeader1.Checked) txttel = "    TEL：" + Common.dtstart.Rows[0]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[0]["pnfax"].ToString();
                else if (rdHeader2.Checked) txttel = "    TEL：" + Common.dtstart.Rows[1]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[1]["pnfax"].ToString();
                else if (rdHeader3.Checked) txttel = "    TEL：" + Common.dtstart.Rows[2]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[2]["pnfax"].ToString();
                else if (rdHeader4.Checked) txttel = "    TEL：" + Common.dtstart.Rows[3]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[3]["pnfax"].ToString();
                else if (rdHeader5.Checked) txttel = "    TEL：" + Common.dtstart.Rows[4]["pntel"].ToString() + " FAX：" + Common.dtstart.Rows[4]["pnfax"].ToString();
                else txttel = "";
                FastReport.dy.Add("txttel", txttel);

                bool showprice = false;
                if (Common.User_SalePrice)
                    showprice = (radio12.Checked) ? true : false;
                else
                    showprice = false;

                if (FastReport.dy.ContainsKey("是否顯示金額"))
                    FastReport.dy.Remove("是否顯示金額");
                FastReport.dy.Add("是否顯示金額", showprice);


                FastReport.PreView(path, printTB, "SALE_", null, null, mode, ReportFileName);
            }
        }

        private void 二聯式發票_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            try
            {
                if (SaNo.TrimTextLenth() == 0 || SaNo1.TrimTextLenth() == 0) return;

                if (SaNo.Text.Trim() != SaNo1.Text.Trim())
                {
                    MessageBox.Show("只能列印一張銷貨單");
                    SaNo.Focus();
                    return;
                }

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("sano", SaNo.Text.Trim());
                    cmd.CommandText = "select * from "
                        + "("
                        + "select * from saled where sano=@sano"
                        + ")saled left join sale on saled.sano=sale.sano";
                    dd.Fill(table);
                }

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("查無銷貨單號:" + SaNo.Text.Trim() + "！");
                    return;
                }
                else
                {
                    二聯式發票.Enabled = false;
                    InvoPrint inv = new InvoPrint();
                    inv.dt = table.Copy();
                    inv.doPrint();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                table.Clear();
                二聯式發票.Enabled = true;
            }
           
        }

        

        
    }

}
