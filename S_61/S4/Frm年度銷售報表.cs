using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.IO;
namespace S_61.S4
{
    public partial class Frm年度銷售報表 : Formbase
    {
        DataTable dt_RowData = new DataTable();//RowData for 1.dt_key and 2.dt_show and 3.dt_source
        List<string> dt_key = new List<string>();    //according to radioT_Cust is checked to Grouping Table (1. Cuno or 2. Itno) for Up or Down Page
        DataTable dt_show = new DataTable();   //for DataGridView
        DataTable dt_source = new DataTable(); //for Source
        int OldIndex = 0;
        string SQL結構字串 = "select 客戶編號='',客戶簡稱='',產品編號='',品名規格='','01'   = 0.0,'02'   = 0.0,'03'   = 0.0,'04'   = 0.0,'05'   = 0.0,'06'   = 0.0,'07'   = 0.0,'08'   = 0.0,'09'   = 0.0,'10'   = 0.0,'11' = 0.0,'12' = 0.0,總數   = 0.0 from  item where 1=0";
        private TextBoxT SaleYear;
        private TextBoxT CuNo;
        private TextBoxT CuNo1;
        private TextBoxT ItNo;
        private TextBoxT ItNo1;
        private bool CheckBox_Sale;
        private bool CheckBox_rSale;
        private RadioT radioT_Cust;

        parameters par = new parameters();

        public Frm年度銷售報表(TextBoxT SaleYear, TextBoxT CuNo, TextBoxT CuNo1, TextBoxT ItNo, TextBoxT ItNo1, bool CheckBox_Sale, bool CheckBox_rSale, RadioT radioT_Cust)
        {
            #region 初始值
            InitializeComponent();
            一月.Set庫存數量小數();
            二月.Set庫存數量小數();
            三月.Set庫存數量小數();
            四月.Set庫存數量小數();
            五月.Set庫存數量小數();
            六月.Set庫存數量小數();
            七月.Set庫存數量小數();
            八月.Set庫存數量小數();
            九月.Set庫存數量小數();
            十月.Set庫存數量小數();
            十一月.Set庫存數量小數();
            十二月.Set庫存數量小數();
            this.SaleYear = SaleYear;
            this.CuNo = CuNo;
            this.CuNo1 = CuNo1;
            this.ItNo = ItNo;
            this.ItNo1 = ItNo1;
            this.CheckBox_Sale = CheckBox_Sale;
            this.CheckBox_rSale = CheckBox_rSale;
            this.radioT_Cust = radioT_Cust;
            #endregion

            par.AddWithValue("ItNo", ItNo.Text.Trim());
            par.AddWithValue("ItNo1", ItNo1.Text.Trim());
            par.AddWithValue("CuNo", CuNo.Text.Trim());
            par.AddWithValue("CuNo1", CuNo1.Text.Trim());
            if (Common.User_DateTime == 1)
                par.AddWithValue("SaleYear", SaleYear.Text.Trim());
            else 
                par.AddWithValue("SaleYear", (SaleYear.Text.Trim().ToInteger() - 1911).ToString());
            string SqlString = GetSQL字串(CheckBox_Sale, CheckBox_rSale);
            SQL.ExecuteNonQuery(SqlString, par, dt_RowData);
            SQL.ExecuteNonQuery(SQL結構字串, null, dt_source);
            SQL.ExecuteNonQuery(SQL結構字串, null, dt_show);
            整理資料(dt_RowData, dt_source);
            if (radioT_Cust.Checked)
            {
                this.Text = "客戶-" + this.Text;
                labelT1.Text = "客戶編號";
                labelT2.Text = "客戶簡稱";
                客戶編號.Visible = false;
                客戶簡稱.Visible = false;
                dt_key = dt_source.AsEnumerable().OrderBy(r => r["客戶編號"].ToString()).Select(r => r["客戶編號"].ToString()).Distinct().ToList();
            }
            else
            {
                this.Text = "產品-" + this.Text;
                labelT1.Text = "產品編號";
                labelT2.Text = "品名規格";
                產品編號.Visible = false;
                品名規格.Visible = false;
                dt_key = dt_source.AsEnumerable().OrderBy(r => r["產品編號"].ToString()).Select(r => r["產品編號"].ToString()).Distinct().ToList();
            }
        }
        /// <summary>
        /// 將 dt_source 填入  dt_source
        /// </summary>
        /// <param name="dt_RowData"></param>
        /// <param name="dt_source"></param>
        private void 整理資料(DataTable dt_RowData, DataTable dt_source)
        {
            #region 填入
            int index;
            for (int i = dt_RowData.Rows.Count -1 ; i >= 0; i--)
            {
                DataRow row = dt_RowData.Rows[i];
                string 產品編號 = row["產品編號"].ToString();
                string 品名規格 = row["品名規格"].ToString();
                string 客戶編號 = row["客戶編號"].ToString();
                string 客戶簡稱 = row["客戶簡稱"].ToString();
                string 月份  = row["月份"].ToString();
                decimal 數量 = row["數量"].ToDecimal("f"+Common.Q);
                index = dt_source.MultiThreadFindIndex("產品編號", 產品編號, "客戶編號", 客戶編號);
                if (index == -1) //如果不存在，新增
                {
                    DataRow dr = dt_source.NewRow();
                    dr["產品編號"] = row["產品編號"].ToString();
                    dr["品名規格"] = row["品名規格"].ToString();
                    dr["客戶編號"] = row["客戶編號"].ToString();
                    dr["客戶簡稱"] = row["客戶簡稱"].ToString();
                    dr["01"] = "0".ToDecimal("f"+Common.Q);
                    dr["02"] = "0".ToDecimal("f" + Common.Q);
                    dr["03"] = "0".ToDecimal("f" + Common.Q);
                    dr["04"] = "0".ToDecimal("f" + Common.Q);
                    dr["05"] = "0".ToDecimal("f" + Common.Q);
                    dr["06"] = "0".ToDecimal("f" + Common.Q);
                    dr["07"] = "0".ToDecimal("f" + Common.Q);
                    dr["08"] = "0".ToDecimal("f" + Common.Q);
                    dr["09"] = "0".ToDecimal("f" + Common.Q);
                    dr["10"] = "0".ToDecimal("f" + Common.Q);
                    dr["11"] = "0".ToDecimal("f" + Common.Q);
                    dr["12"] = "0".ToDecimal("f" + Common.Q);
                    dr[月份] = 數量;
                    dr["總數"] = 數量;
                    dt_source.Rows.Add(dr);
                }
                else //如果存在，填入數量並累加總數
                {
                    dt_source.Rows[index][月份] = 數量;
                    dt_source.Rows[index]["總數"] = dt_source.Rows[index]["總數"].ToDecimal() + 數量;
                }
                dt_RowData.Rows.RemoveAt(i);
            }
            #endregion
        }

        private string GetSQL字串(bool CheckBox_Sale, bool CheckBox_rSale)
        {
            string SQL字串 = "";
            string SqlWhereStr_單據 = "";
            string SqlWhereStr_Itno = "";// WHERE  ItNo >= @ItNo and  ItNo <=@ItNo1
            string SqlWhereStr_Cuno = "";// AND CuNo >=@CuNo and  CuNo <=@CuNo1
            if (CheckBox_Sale && CheckBox_rSale)
                SqlWhereStr_單據 = "";
            else if (CheckBox_Sale)
                SqlWhereStr_單據 = "where 單據 = '銷貨'";
            else if (CheckBox_rSale)
                SqlWhereStr_單據 = "where 單據 = '銷退'";
            else
                SqlWhereStr_單據 = "where 單據 = 'x'";

            if (ItNo.Text.Trim().Length > 0 && ItNo1.Text.Trim().Length > 0)
                SqlWhereStr_Itno = "   ItNo >= @ItNo and  ItNo <=@ItNo1 ";
            else if (ItNo.Text.Trim().Length > 0 )
                SqlWhereStr_Itno = "   ItNo >= @ItNo  ";
            else if (ItNo1.Text.Trim().Length > 0)
                SqlWhereStr_Itno = "   ItNo <=@ItNo1  ";
            else
                SqlWhereStr_Itno = "   1 = 1";

            if (CuNo.Text.Trim().Length > 0 && CuNo1.Text.Trim().Length > 0)
                SqlWhereStr_Cuno = "  CuNo >= @CuNo and  CuNo <=@CuNo1 ";
            else if (CuNo.Text.Trim().Length > 0)
                SqlWhereStr_Cuno = "  CuNo >= @CuNo ";
            else if (CuNo1.Text.Trim().Length > 0)
                SqlWhereStr_Cuno = "  CuNo <= @CuNo1 ";
            else
                SqlWhereStr_Cuno = " 1 = 1";

            #region SQL字串
            SQL字串 = @"

DECLARE @銷貨明細 TABLE (
    單據   Nvarchar(4), 
	西元年 Nvarchar(5),       
	民國年 Nvarchar(4),       
	月份   Nvarchar(3),       
    客戶編號 Nvarchar(10),                        
	產品編號 Nvarchar(20),
    數量 decimal(16,4),
    產品組成 Nvarchar(1),
	Bomid Nvarchar(30) COLLATE Chinese_Taiwan_Stroke_BIN)
INSERT INTO @銷貨明細
select 單據 = '銷貨',民國年 = substring(sadate,1,3),西元年 = substring(sadate1,1,4),月份 = substring(sadate,4,2),客戶編號 = cuno,產品編號=itno,數量=qty *itpkgqty,產品組成 = ittrait ,Bomid  
from SALED   WHERE SaDate like @SaleYear+'%'  AND " + SqlWhereStr_Cuno + @" and  " + SqlWhereStr_Itno + @" 


DECLARE @銷貨bom TABLE (
    單據   Nvarchar(4), 
	西元年 Nvarchar(5),       
	民國年 Nvarchar(4),       
	月份   Nvarchar(3),       
    客戶編號 Nvarchar(10),                        
	產品編號 Nvarchar(20),
    數量 decimal(16,4),
    產品組成 Nvarchar(1),
    Bomid Nvarchar(30) COLLATE Chinese_Taiwan_Stroke_BIN)
INSERT INTO @銷貨bom
select 單據 = '銷貨',民國年,西元年,月份,客戶編號,產品編號,數量 = ( 組合明細.數量 * bom.Bom數量 ),產品組成='',Bomid=''  from (
	(select bomid,產品編號 = itno,Bom數量 = (itqty * itpkgqty / itpareprs)  from salebom  WHERE  " +SqlWhereStr_Itno+@" ) bom
	INNER JOIN 
	(select bomid,民國年 = substring(sadate,1,3),西元年 = substring(sadate1,1,4),月份 = substring(sadate,4,2),客戶編號 = cuno,數量 = qty * itpkgqty from saled WHERE ittrait = 1 and SaDate like @SaleYear+'%' AND " + SqlWhereStr_Cuno + @" ) as 組合明細
	on
	組合明細.bomid = bom.bomid 
)


DECLARE @銷退明細 TABLE (
    單據   Nvarchar(4), 
	西元年 Nvarchar(5),       
	民國年 Nvarchar(4),       
	月份   Nvarchar(3),       
    客戶編號 Nvarchar(10),                        
	產品編號 Nvarchar(20),
    數量 decimal(16,4),
    產品組成 Nvarchar(1),
	Bomid Nvarchar(30) COLLATE Chinese_Taiwan_Stroke_BIN)
INSERT INTO @銷退明細
select 單據 = '銷退',民國年 = substring(sadate,1,3),西元年 = substring(sadate1,1,4),月份 = substring(sadate,4,2),客戶編號 = cuno,產品編號=itno,數量= -1 * qty *itpkgqty,產品組成 = ittrait ,Bomid  
from rSALED   WHERE SaDate like @SaleYear+'%'  AND " + SqlWhereStr_Cuno + @" and  " + SqlWhereStr_Itno + @"



DECLARE @銷退bom TABLE (
    單據   Nvarchar(4), 
	西元年 Nvarchar(5),       
	民國年 Nvarchar(4),       
	月份   Nvarchar(3),       
    客戶編號 Nvarchar(10),                        
	產品編號 Nvarchar(20),
    數量 decimal(16,4),
    產品組成 Nvarchar(1),
    Bomid Nvarchar(30) COLLATE Chinese_Taiwan_Stroke_BIN)
INSERT INTO @銷退bom
select 單據 = '銷退',民國年,西元年,月份,客戶編號,產品編號,數量 = (-1 * 組合明細.數量 * bom.Bom數量 ),產品組成='',Bomid=''  from (
	(select bomid,產品編號 = itno,Bom數量 = (itqty * itpkgqty / itpareprs)  from rsalebom  WHERE  " + SqlWhereStr_Itno + @" ) bom
	INNER JOIN 
	(select bomid,民國年 = substring(sadate,1,3),西元年 = substring(sadate1,1,4),月份 = substring(sadate,4,2),客戶編號 = cuno,數量 = qty * itpkgqty from rsaled WHERE ittrait = 1 and SaDate like @SaleYear+'%'  AND " + SqlWhereStr_Cuno + @" ) as 組合明細
	on
	組合明細.bomid = bom.bomid 
)

DECLARE @銷貨統計 TABLE (
    單據   Nvarchar(4), 
	西元年 Nvarchar(5),       
	民國年 Nvarchar(4),       
	月份   Nvarchar(3),       
    客戶編號 Nvarchar(10),                        
	產品編號 Nvarchar(20) COLLATE Chinese_Taiwan_Stroke_BIN,
    數量 decimal(16,4),
    產品組成 Nvarchar(1),
    Bomid Nvarchar(30) COLLATE Chinese_Taiwan_Stroke_BIN)
INSERT INTO @銷貨統計
select * from(
			select * from @銷貨明細 WHERE 產品組成 != '1'
			union all
			select * from @銷貨bom
			union all
			select * from @銷退明細 WHERE 產品組成 != '1'
			union all
			select * from @銷退bom
            ) as a
 "+ SqlWhereStr_單據+@"    
DECLARE @數量表 TABLE
(
    產品編號 Nvarchar(20), 
	品名規格 Nvarchar(50), 
    客戶編號 Nvarchar(10) COLLATE Chinese_Taiwan_Stroke_BIN, 
	客戶簡稱 Nvarchar(20), 
    月份     Nvarchar(4),
	數量    Nvarchar(20)
)
INSERT INTO @數量表
SELECT 產品編號,品名規格 = (select top 1 itname from item where ItNo =產品編號 ),客戶編號,客戶簡稱 = (select top 1 cuname1 from cust where cuno = 客戶編號  COLLATE Chinese_Taiwan_Stroke_BIN ),月份,數量 FROM
(
	select 產品編號,客戶編號,月份,數量 = SUM(數量)  from @銷貨統計  group by 產品編號,客戶編號,月份 
)Z
order by 產品編號,客戶編號,月份 

select * from @數量表
";
            #endregion
            return SQL字串;
        }

        private void GridViewShow(List<string> dt_key, int index)
        {
            dataGridViewT1.DataSource = null;
            #region 維護索引正確性  變數:index
            if(index < 0) index =0;
            else if (index >= dt_key.Count) index = dt_key.Count -1;
            OldIndex = index;
            #endregion
            string ColumnName = labelT1.Text; // 客戶編號 or 產品編號
            string OrderByColumnName = labelT1.Text == "客戶編號" ? "產品編號" : "客戶編號";
            string ColumnName1 = labelT2.Text;// 客戶簡稱 or 品名規格
            string Value = dt_key[index];
            #region 填入 dt_show
            //1.先加回 dt_source = dt_source + dt_show
            for (int j = dt_show.Rows.Count - 1; j >= 0; j--)
            {
                dt_source.ImportRow(dt_show.Rows[j]);
                dt_show.Rows.RemoveAt(j);
            }
            //2.再加入 dt_show = dt_source(符合之條件) Then 清除 dt_source(符合之條件)
            int i = dt_source.MultiThreadFindIndex(ColumnName,Value);
            while (i > -1)
            {
                dt_show.ImportRow(dt_source.Rows[i]);
                dt_source.Rows.RemoveAt(i);
                i = dt_source.MultiThreadFindIndex(ColumnName, Value);
            }
            //3.排序
            dt_show = dt_show.AsEnumerable().OrderBy(r => r[OrderByColumnName].ToString()).CopyToDataTable();
            #endregion
            if(dt_show.Rows.Count > 0)
            {
                textBoxT1.Text = dt_show.Rows[0][ColumnName].ToString();
                textBoxT2.Text = dt_show.Rows[0][ColumnName1].ToString();
                dataGridViewT1.DataSource = dt_show;
            }
        }

        private void dataGridViewT1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Frm年度銷售報表_Load(object sender, EventArgs e)
        {

        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            GridViewShow(dt_key, 0);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            GridViewShow(dt_key, OldIndex - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GridViewShow(dt_key, OldIndex + 1);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            GridViewShow(dt_key, dt_key.Count - 1);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {

        }

        private void Frm年度銷售報表_Load_1(object sender, EventArgs e)
        {
            if (dt_key.Count == 0)
            {
                MessageBox.Show("查無資料!");
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
            else
            {
                GridViewShow(dt_key, OldIndex);
            }
        }



        private void dataintodocument(RptMode rptMode)
        {
            using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
            {
                //報表參數
                List<string> Paramaters = new List<string>() { "年度", SaleYear.Text };
                FastReport.Paramaters = Paramaters;
                //報表名稱
                string 報表名稱 = "";
                if (radioT_Cust.Checked)
                    報表名稱 = "年度銷售報表_客戶";
                else
                    報表名稱 = "年度銷售報表_產品";
                //路徑
                string ReportPath = Common.reportaddress + "ReportG\\" + 報表名稱 + ".frx";

                if (File.Exists(ReportPath) == false)
                {
                    MessageBox.Show("報表檔案不存在\n" + ReportPath, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (DataTable dt= dt_source.Copy())
                {
                    dt.Merge(dt_show);
                    FastReport.PreView(ReportPath, dt, "Data", null, null, rptMode, 報表名稱);
                }
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

        private void btnWord_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dataintodocument(RptMode.Excel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
               using (DataTable dt = dt_source.Copy())
                {
                    dt.Merge(dt_show);

                    if (radioT_Cust.Checked)
                    {
                        dt.AsEnumerable().OrderBy(r => r["客戶編號"].ToString()).CopyToDataTable().Excel匯出並開啟(this.Text);
                    }
                    else
                    {
                        dt.AsEnumerable().OrderBy(r => r["產品編號"].ToString()).CopyToDataTable().Excel匯出並開啟(this.Text);
                    }
                }
            }
            if (keyData == Keys.Escape) this.Dispose();
            if (keyData == Keys.F11) this.Dispose();

            return base.ProcessCmdKey(ref msg, keyData);
        }




    }
}
