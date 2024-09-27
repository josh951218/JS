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
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace S_61.subMenuFm_2
{
    public partial class FrmAdjustBrowse_Infob : Formbase
    {
        public DataTable dt_明細 = new DataTable();
        List<ButtonSmallT> ListButtonSmallT = new List<ButtonSmallT>();
        public string  WhereStr = "";
        public SqlParameterCollection SqlParameterCollection;
        public string firstDay = "", LeastDay = "";
        string no = "1";

        public FrmAdjustBrowse_Infob()
        {
            InitializeComponent();
        }

        private void FrmAdjustBrowse_Infob_Load(object sender, EventArgs e)
        {
            ListButtonSmallT = new List<ButtonSmallT>() { qury2, qury3, qury4, qury5 };

            for (int i = 0; i < dt_明細.Rows.Count; i++)
            {
                dt_明細.Rows[i]["序號"] = (i + 1); ;
            }

            this.調整數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            this.調整日期.DataPropertyName = Common.User_DateTime == 1 ? "調整日期" : "調整日期1";

            this.rd2.SetUserDefineRpt("調整資料瀏覽_簡要自定.rpt");
            this.rd3.SetUserDefineRpt("調整資料瀏覽_組件自定.rpt");
            dataGridViewT1.DataSource = dt_明細.DefaultView;

            qury2.ForeColor = Color.Red;
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                var index = e.Row.Index;
                ItNo.Text = dt_明細.DefaultView[index]["itno"].ToString();
                StNo.Text = dt_明細.DefaultView[index]["stno"].ToString();
                StName.Text = dt_明細.DefaultView[index]["stname"].ToString();
                EmNo.Text = dt_明細.DefaultView[index]["emno"].ToString();
                EmName.Text = dt_明細.DefaultView[index]["emname"].ToString();
            }
        }

        private void qury2_Click(object sender, EventArgs e)
        {
            排序("調整日期,adno");
            ListButtonSmallT.ForEach(t => t.ForeColor = Color.Black);
            ((Button)sender).ForeColor = Color.Red;
            firstDay = dt_明細.Rows[dt_明細.Rows.Count - 1][this.調整日期.DataPropertyName].ToString();
            LeastDay = dt_明細.Rows[0][this.調整日期.DataPropertyName].ToString();
        }

        private void qury3_Click(object sender, EventArgs e)
        {
            排序("itno,調整日期");
            ListButtonSmallT.ForEach(t => t.ForeColor = Color.Black);
            ((Button)sender).ForeColor = Color.Red;
        }

        private void qury4_Click(object sender, EventArgs e)
        {
            排序("emno,調整日期");
            ListButtonSmallT.ForEach(t => t.ForeColor = Color.Black);
            ((Button)sender).ForeColor = Color.Red;
        }

        private void qury5_Click(object sender, EventArgs e)
        {
            排序("memo,調整日期");
            ListButtonSmallT.ForEach(t => t.ForeColor = Color.Black);
            ((Button)sender).ForeColor = Color.Red;
        }

        private void 排序(string SortStr)
        {
            string no = dataGridViewT1.SelectedRows[0].Cells["序號"].Value.ToString().Trim();
            dt_明細.DefaultView.Sort = SortStr;
            dt_明細.DefaultView.Search(ref dataGridViewT1,"序號", no);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Excel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void OutReport(RptMode mode)
        {
            string path = "", Today = "", DateRange = "", txtend = "";
            DataTable dt = new DataTable();
            RPT rp = new RPT();
            if (rd2.Checked)
            {
                path = Common.reportaddress + @"Report\調整資料瀏覽_簡要自定.rpt";
            }
            else if (rd3.Checked)
            {
                path = Common.reportaddress + @"Report\調整資料瀏覽_組件自定.rpt";
            }
            else if (qury2.ForeColor == Color.Red)
            {
                path = Common.reportaddress + @"Report\調整資料瀏覽(日期).rpt";
            }
            else if (qury3.ForeColor == Color.Red)
            {
                path = Common.reportaddress + @"Report\調整資料瀏覽(產品).rpt";
            }
            else if (qury4.ForeColor == Color.Red)
            {
                path = Common.reportaddress + @"Report\調整資料瀏覽(人員).rpt";
            }
            else if (qury5.ForeColor == Color.Red)
            {
                path = Common.reportaddress + @"Report\調整資料瀏覽(說明).rpt";
            }
            載入dt(dt);

            DateRange = "日期區間 : " + firstDay + " ~ " + LeastDay;
            if (Common.User_DateTime == 1) Today = "製表日期 : " + (int.Parse(DateTime.Now.Year.ToString()) - 1911).ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Day.ToString().PadLeft(2, '0');
            else Today = "製表日期 : " + (int.Parse(DateTime.Now.Year.ToString())).ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0').PadLeft(2, '0') + "/" + DateTime.Now.Day.ToString().PadLeft(2, '0');

            if (rd6.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[6]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[7]["tamemo"].ToString();
            else if (rd7.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[9]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[10]["tamemo"].ToString();
            else if (rd8.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[12]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[13]["tamemo"].ToString();
            else if (rd9.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[15]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[16]["tamemo"].ToString();
            else if (rd10.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[17]["tamemo"].ToString() + '\n' + Common.dtEnd.Rows[18]["tamemo"].ToString();
            else txtend = "";
            rp = new RPT(dt, path);
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateRange", DateRange });
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
                string 明細組件SqlStr  = 載入SqlStr(rd5.Checked);
                #region add Parameter
                for (int i = 0; i < SqlParameterCollection.Count; i++)
                {
                    cmd.Parameters.AddWithValue(SqlParameterCollection[i].ParameterName, SqlParameterCollection[i].Value);
                }
                #endregion
                cmd.CommandText = 明細組件SqlStr; 
                da.Fill(dt);
            }
        }

        private string 載入SqlStr(bool 是否列印組件)
        {
            string str = "", 調整日期 = "";
            調整日期 = Common.User_DateTime == 1 ?
                "調整日期=SUBSTRING(adjustd.addate,1,3) + '/'+SUBSTRING(adjustd.addate,4,2)+ '/'+SUBSTRING(adjustd.addate,6,2)"
                :
                "調整日期= SUBSTRING(adjustd.addate1,1,4) + '/'+SUBSTRING(adjustd.addate1,5,2)+ '/'+SUBSTRING(adjustd.addate1,7,2)";

            if (!是否列印組件)
            {
                str = @"  
                select " + 調整日期 + @" ,adjustd.adno as 調整憑證 ,adjustd.adno as 倉庫編號,adjustd.stname as 倉庫名稱,adjustd.itno as 產品編號 ,adjustd.itname as 品名規格,adjustd.itunit as 單位 ,adjustd.qty as 調整數量,adjustd.itpkgqty as 包裝數量,adjustd.emno as 人員編號,empl.emname as 人員姓名,adjustd.memo as 備註說明,adjustd.bomid as BomID,Bom產品編號='',Bom產品名稱='',Bom產品單位='',Bom調整數量=0.0,Bom包裝數量=0.0
                from adjustd left join empl on adjustd.emno = empl.emno where 0=0" + WhereStr;
            }
            else
            {
                str = @"
                select " + 調整日期 + @" ,adjustd.adno as 調整憑證 ,adjustd.adno as 倉庫編號,adjustd.stname as 倉庫名稱,adjustd.itno as 產品編號 ,adjustd.itname as 品名規格,adjustd.itunit as 單位 ,adjustd.qty as 調整數量,adjustd.itpkgqty as 包裝數量,adjustd.emno as 人員編號,empl.emname as 人員姓名,adjustd.memo as 備註說明,adjustd.bomid as BomID,adjustd.itno as Bom產品編號,ISNULL(AdjuBom.itname,'') as Bom產品名稱,AdjuBom.itunit as Bom產品單位,AdjuBom.itqty as Bom調整數量,AdjuBom.itpkgqty as Bom包裝數量 
                from  (adjustd left join adjubom on adjustd.bomid = adjubom.BomID) left join empl on adjustd.emno = empl.emno where 0=0" + WhereStr ;          
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
            if (keyData == Keys.F2 )
            {
                qury2.PerformClick();
            }
            else if (keyData == Keys.F3)
            {
                qury3.PerformClick();
            }
            else if (keyData == Keys.F4)
            {
                qury4.PerformClick();
            }
            else if (keyData == Keys.F5)
            {
                qury5.PerformClick();
            }
           
            return base.ProcessCmdKey(ref msg, keyData);
        }


    }
}
