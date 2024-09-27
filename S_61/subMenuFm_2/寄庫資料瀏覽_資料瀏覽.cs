using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class 寄庫資料瀏覽_資料瀏覽 : Formbase
    {
        public DataTable dt_明細 = new DataTable(); //明細
        public DataTable dt_數量 = new DataTable(); //數量
        public DataTable dt_產品 = new DataTable(); 
        public DataTable dt_客戶 = new DataTable(); 
        public DataTable dt_總數 = new DataTable();
        public string 明細組件SqlStr = "", WhereStr="";
        public SqlParameterCollection SqlParameterCollection;
        List<ButtonSmallT> ListButtonSmallT = new List<ButtonSmallT>();   

        public 寄庫資料瀏覽_資料瀏覽()
        {
            InitializeComponent();
            this.radioDefine1.SetUserDefineRpt("寄庫資料瀏覽自定一.rpt");
            this.數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.單據日期.DataPropertyName = Common.User_DateTime == 1 ? "單據日期" : "單據日期1";
        }

        private void 寄庫資料瀏覽_資料瀏覽_Load(object sender, EventArgs e)
        {
            dataGridViewT1.DataSource = dt_明細;
            textBoxT51.Text = dt_總數.Rows[0]["總客戶數"].ToString();
            textBoxT52.Text = dt_總數.Rows[0]["總產品數"].ToString();
            textBoxT53.Text = dt_總數.Rows[0]["總寄庫單數"].ToString();
            textBoxT54.Text = dt_總數.Rows[0]["總領庫單數"].ToString();

            ListButtonSmallT = new List<ButtonSmallT>() { sort1, sort3, sort4 };   
            for (int i = 0; i < dt_明細.Rows.Count; i++)
            {
                dt_明細.Rows[i]["序號"] = (i + 1).ToString();
            }
            sort1.PerformClick();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (e.Row.Index == dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected))
                {
                    var index = e.Row.Index;
                    var inno = dt_明細.DefaultView[index]["inno"].ToString();
                    var itno = dt_明細.DefaultView[index]["產品編號"].ToString();
                    var cuno = dt_明細.DefaultView[index]["客戶編號"].ToString();
                    string InQty_TB_ = "", OuQty_TB_ = "", InCount_TB_ = "", OuCount_TB_ = "", NonQty_TB_ = "", TotalCustInstk_TB = "", TotalCustOustk_TB = "", TotalItemIustk_TB = "", TotalItemOustk_TB = "";
                    
                    object lock_ = new object();
                    Parallel.For(0, dt_數量.Rows.Count , (i, loopState) =>
                    {
                        if (dt_數量.Rows[i]["inno"].ToString() == inno && dt_數量.Rows[i]["itno"].ToString() == itno)
                        {
                            lock (lock_)
                            {

                                InQty_TB_   = dt_數量.Rows[i]["寄庫總數"].ToDecimal().ToString("f0");
                                OuQty_TB_   = dt_數量.Rows[i]["領庫總數"].ToDecimal().ToString("f0");
                                InCount_TB_ = dt_數量.Rows[i]["寄庫筆數"].ToDecimal().ToString("f0");
                                OuCount_TB_ = dt_數量.Rows[i]["領庫筆數"].ToDecimal().ToString("f0");
                                NonQty_TB_  =(dt_數量.Rows[i]["寄庫總數"].ToDecimal() - dt_數量.Rows[i]["領庫總數"].ToDecimal()).ToString("f0");
                            }
                        }
                    });
                    Parallel.For(0, dt_客戶.Rows.Count, (i, loopState) =>
                    {
                        if (dt_客戶.Rows[i]["客戶編號"].ToString() == cuno)
                        {
                            lock (lock_)
                            {
                                TotalCustInstk_TB = dt_客戶.Rows[i]["寄庫筆數"].ToDecimal().ToString("f0"); 
                                TotalCustOustk_TB = dt_客戶.Rows[i]["領庫筆數"].ToDecimal().ToString("f0"); 
                            }
                        }
                    });
                    Parallel.For(0, dt_產品.Rows.Count, (i, loopState) =>
                    {
                        if (dt_產品.Rows[i]["產品編號"].ToString() == itno)
                        {
                            lock (lock_)
                            {
                                TotalItemIustk_TB = dt_產品.Rows[i]["寄庫總數"].ToDecimal().ToString("f0"); 
                                TotalItemOustk_TB = dt_產品.Rows[i]["領庫總數"].ToDecimal().ToString("f0"); 
                            }
                        }
                    });

                    ItNo_TB.Text = dt_明細.DefaultView[index]["產品編號"].ToString();
                    CuNo_TB.Text = dt_明細.DefaultView[index]["客戶編號"].ToString();
                    SaNo_TB.Text = dt_明細.DefaultView[index]["銷貨單號"].ToString();
                    OuNo_TB.Text = dt_明細.DefaultView[index]["ouno"].ToString();
                    InNo_TB.Text = dt_明細.DefaultView[index]["inno"].ToString();
                    textBoxT32.Text = TotalCustInstk_TB; 
                    textBoxT33.Text = TotalCustOustk_TB;
                    textBoxT42.Text = TotalItemIustk_TB;
                    textBoxT43.Text = TotalItemOustk_TB;
                    InQty_TB.Text   =InQty_TB_;
                    OuQty_TB.Text   = OuQty_TB_;
                    NonQty_TB.Text = NonQty_TB_;
                }
            }
        }

        private void sort1_Click(object sender, EventArgs e)
        {
            排序("單據日期 Desc,單據憑證 Asc,產品編號 Asc,客戶編號 Asc,單據 Asc");
            ListButtonSmallT.ForEach(t => t.ForeColor = Color.Black);
            ((Button)sender).ForeColor = Color.Red;
            firstDay = dt_明細.Rows[dt_明細.Rows.Count - 1][this.單據日期.DataPropertyName].ToString();
            LeastDay = dt_明細.Rows[0]                     [this.單據日期.DataPropertyName].ToString();
        }

        private void sort3_Click(object sender, EventArgs e)
        {
            排序("產品編號 Asc,單據日期 Asc,客戶編號 Asc,單據 Asc");
            ListButtonSmallT.ForEach(t => t.ForeColor = Color.Black);
            ((Button)sender).ForeColor = Color.Red;
        }

        private void sort4_Click(object sender, EventArgs e)
        {
            排序("客戶編號 Asc,單據日期 Asc,產品編號 Asc,單據 Asc");
            ListButtonSmallT.ForEach(t => t.ForeColor = Color.Black);
            ((Button)sender).ForeColor = Color.Red;
        }
        string firstDay = "", LeastDay = "";
        private void 排序(string SortStr)
        {
            var Index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            string 序號 = dt_明細.DefaultView[Index]["序號"].ToString();
            DataTable TempDt = dt_明細.Copy();
            var SortRowArray = TempDt.Select("", SortStr);
            dt_明細.Rows.Clear();

            for (int i = 0; i <  SortRowArray.Length ; i++)
			{
			    dt_明細.ImportRow(SortRowArray[i]);
                if (SortRowArray[i]["序號"].ToString() == 序號)
                    Index = i;
			}
            TempDt.Dispose();

            for (int i = 0; i < SortRowArray.Length ; i++)
            {
                dt_明細.Rows[i]["序號"] = (i + 1).ToString();
            }

            //Parallel.For(0, dt_數量.Rows.Count, (i, loopState) =>
            //{
            //    if (dt_數量.Rows[i]["inno"].ToString() == inno && dt_數量.Rows[i]["itno"].ToString() == itno)
            //    {
            //            InQty_TB_ = dt_數量.Rows[i]["寄庫總數"].ToDecimal().ToString();
            //            OuQty_TB_ = dt_數量.Rows[i]["領庫總數"].ToDecimal().ToString();
            //            InCount_TB_ = dt_數量.Rows[i]["寄庫筆數"].ToDecimal().ToString();
            //            OuCount_TB_ = dt_數量.Rows[i]["領庫筆數"].ToDecimal().ToString();
            //            NonQty_TB_ = (dt_數量.Rows[i]["寄庫總數"].ToDecimal() - dt_數量.Rows[i]["領庫總數"].ToDecimal()).ToString();
            //    }
            //});

            dataGridViewT1.FirstDisplayedScrollingRowIndex = Index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, Index];
            dataGridViewT1.Rows[Index].Selected = true;
        }

        private void buttonT9_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void buttonT10_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void buttonT7_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void buttonT6_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

        private void buttonT8_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void OutReport(RptMode mode)
        {   
            string path = "",Today = "",DateRange="",txtend="";
            DataTable dt = new DataTable();
            RPT rp = new RPT();

            if (radioDefine1.Checked)
            {
                path = Common.reportaddress + @"Report\寄庫資料瀏覽自定一.rpt";
            }
            else if (sort1.ForeColor == Color.Red)
            {
                if (RadioPrintMemo.Checked) path = Common.reportaddress + @"Report\寄庫資料瀏覽_資料瀏覽_備註說明(單據日期).rpt";
                else　　　　　　　　　　　　path = Common.reportaddress + @"Report\寄庫資料瀏覽_資料瀏覽(單據日期).rpt"; 
            }
            else if (sort3.ForeColor == Color.Red)
            {
                if (RadioPrintMemo.Checked) path = Common.reportaddress + @"Report\寄庫資料瀏覽_資料瀏覽_備註說明(產品編號).rpt";
                else                        path = Common.reportaddress + @"Report\寄庫資料瀏覽_資料瀏覽(產品編號).rpt";
            }
            else if (sort4.ForeColor == Color.Red)
            {
                if (RadioPrintMemo.Checked) path = Common.reportaddress + @"Report\寄庫資料瀏覽_資料瀏覽_備註說明(客戶編號).rpt";
                else                        path = Common.reportaddress + @"Report\寄庫資料瀏覽_資料瀏覽(客戶編號).rpt";

            }

            載入dt(dt);

            int RowCount = dt.Rows.Count;
            for (int i = 0; i < RowCount; i++)
            {
                if (dt.Rows[i]["單據"].ToString() == "領庫")
                    dt.Rows[i]["數量"] = (-1)*dt.Rows[i]["數量"].ToString().ToDecimal();
            }

            DateRange = "日期區間 : " + firstDay + " ~ " + LeastDay;
            if (Common.User_DateTime == 1) Today = "製表日期 : " + (int.Parse(DateTime.Now.Year.ToString()) - 1911).ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Day.ToString().PadLeft(2, '0');
            else                           Today = "製表日期 : " + (int.Parse(DateTime.Now.Year.ToString())).ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0').PadLeft(2, '0') + "/" + DateTime.Now.Day.ToString().PadLeft(2, '0');

            if (rdFooter1.Checked)      txtend = Common.dtEnd.Rows[5] ["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
            else if (rdFooter2.Checked) txtend = Common.dtEnd.Rows[8] ["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
            else if (rdFooter3.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
            else if (rdFooter4.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
            else if (rdFooter5.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
            else txtend = "";
            rp = new RPT(dt, path);     
            rp.lobj.Add(new string[] { "txtend"     , txtend });
            rp.lobj.Add(new string[] { "DateRange"  , DateRange });
            rp.lobj.Add(new string[] { "DateCreated", Today });
            if (mode == RptMode.Print) rp.Print();
            else if (mode == RptMode.PreView) rp.PreView();
            else if (mode == RptMode.Excel) rp.Excel();
            else if (mode == RptMode.Word) rp.Word();

        }

        private void 載入dt(DataTable dt)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                明細組件SqlStr = 載入SqlStr(RadioBomTrue.Checked);
                #region add Parameter
                for (int i = 0; i < SqlParameterCollection.Count; i++)
                {
                    cmd.Parameters.AddWithValue(SqlParameterCollection[i].ParameterName, SqlParameterCollection[i].Value);
                }
                #endregion
                cmd.CommandText = 明細組件SqlStr; //+ " order by 客戶編號 Asc,產品編號 Asc,單據日期 Desc,單據憑證 Asc,A.單據 Asc";
                da.Fill(dt);
            }
        }

        private string 載入SqlStr(bool 是否列印組件)
        {
            //單據日期=SUBSTRING(indate,1,3) + '/'+SUBSTRING(indate,4,2)+ '/'+SUBSTRING(indate,6,2),
            //單據日期1= SUBSTRING(indate1,1,4) + '/'+SUBSTRING(indate,4,2)+ '/'+SUBSTRING(indate,6,2)
            string str = "", indate單據日期="",oudate單據日期=""; 
            indate單據日期 = Common.User_DateTime == 1 ? 
                "單據日期=SUBSTRING(indate,1,3) + '/'+SUBSTRING(indate,4,2)+ '/'+SUBSTRING(indate,6,2)" 
                :
                "單據日期= SUBSTRING(indate1,1,4) + '/'+SUBSTRING(indate1,5,2)+ '/'+SUBSTRING(indate1,7,2)";
            oudate單據日期 = Common.User_DateTime == 1 ? 
                "單據日期=SUBSTRING(oudate,1,3) + '/'+SUBSTRING(oudate,4,2)+ '/'+SUBSTRING(oudate,6,2)" 
                :
                "單據日期= SUBSTRING(oudate1,1,4) + '/'+SUBSTRING(oudate1,5,2)+ '/'+SUBSTRING(oudate1,7,2)";

            if (是否列印組件) 
            {
                str = @"  
 select * from (
         select * from 
         (
             select 序號='',*,客戶簡稱=CuName1 from 
             (
                  select  itpkgqty,recordno,bomid,inno,ouno='',單據憑證=inno,單據='寄庫',indate as date,銷貨單號=sano," + indate單據日期 + @",客戶編號=cuno,產品編號=itno,品名規格=itname,數量=inqty,單位=itunit,倉庫編號=stno,倉庫名稱=stname,備註說明=memo, 
                  CASE ittrait
                  WHEN '1' THEN '組合品'
                  WHEN '2' THEN '組裝品'
                  ELSE '單一商品'
                  END as 產品組成 
                  from instkd 
                 UNION ALL
                  select  itpkgqty,recordno,bomid,inno,ouno  ,單據憑證=ouno,單據='領庫',oudate as date,銷貨單號=sano," + indate單據日期 + @",客戶編號=cuno,產品編號=itno,品名規格=itname,數量=ouqty,單位=itunit,倉庫編號=stno,倉庫名稱=stname,備註說明=memo,
                  CASE ittrait
                  WHEN '1' THEN '組合品'
                  WHEN '2' THEN '組裝品'
                  ELSE '單一商品'
                  END as 產品組成
                  from oustkd 
             )A 
             inner JOIN 
             (SELECT cuno,cuname1 from cust) 
             cust ON cuno = 客戶編號) A  where 0 = 0 " + WhereStr +
@"        )A
left join
     (
         select itpkgqty as BOM包裝數量,單據 = '寄庫' ,BomID,itno as BOM產品編號 ,itname as BOM品名規格,itqty as BOM數量,itunit as BOM單位 from instkbom
         UNION ALL
         select itpkgqty as BOM包裝數量,單據 = '領庫' ,BomID,itno as BOM產品編號 ,itname as BOM品名規格,itqty as BOM數量,itunit as BOM單位 from instkbom
     ) B 
on A.bomid = B.BomID and A.單據 = B.單據 ";
            }
            else 
            {
                str =
                    // select * from (
       @" select BomID,BOM產品編號='' ,BOM品名規格='',BOM數量='',BOM單位='',* from  
         (
            select 序號='',*,客戶簡稱=CuName1 from 
             (
                  select  itpkgqty,recordno,bomid,inno,ouno='',單據憑證=inno,單據='寄庫',indate as date,銷貨單號=sano," + indate單據日期 + @",客戶編號=cuno,產品編號=itno,品名規格=itname,數量=inqty,單位=itunit,倉庫編號=stno,倉庫名稱=stname,備註說明=memo, 
                  CASE ittrait
                  WHEN '1' THEN '組合品'
                  WHEN '2' THEN '組裝品'
                  ELSE '單一商品'
                  END as 產品組成 
                  from instkd 
                 UNION ALL
                  select  itpkgqty,recordno,bomid,inno,ouno  ,單據憑證=ouno,單據='領庫',oudate as date,銷貨單號=sano," + oudate單據日期 + @",客戶編號=cuno,產品編號=itno,品名規格=itname,數量=ouqty,單位=itunit,倉庫編號=stno,倉庫名稱=stname,備註說明=memo,
                  CASE ittrait
                  WHEN '1' THEN '組合品'
                  WHEN '2' THEN '組裝品'
                  ELSE '單一商品'
                  END as 產品組成
                  from oustkd 
             )A 
             inner JOIN 
             (SELECT cuno,cuname1 from cust) 
             cust ON cuno = 客戶編號) A  where 0 = 0 " + WhereStr;
                //               )A";
                //left join
                //     (
                //         select 單據 = '寄庫' ,BomID,itno as BOM產品編號 ,itname as BOM品名規格,itqty as BOM數量,itunit as BOM單位 from instkbom
                //         UNION ALL
                //         select 單據 = '領庫' ,BomID,itno as BOM產品編號 ,itname as BOM品名規格,itqty as BOM數量,itunit as BOM單位 from instkbom
                //     ) B 
                //on A.bomid = B.BomID and A.單據 = B.單據 ";
            }
            return str;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt_明細.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 

    }
}
