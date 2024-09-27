using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmInStkInfob : Formbase
    {
        public DataTable dt = new DataTable();
        IEnumerable<IGrouping<string, DataRow>> GroupbyDate = null;
        IGrouping<string, DataRow> g = null;
        RPT rp;
        string No = "";

        public FrmInStkInfob()
        {
            InitializeComponent();
        }

        private void FrmInStkInfob_Load(object sender, EventArgs e)
        {
            radioT1.Checked = radioT3.Checked = true;
            radioT2.SetUserDefineRpt("寄庫資料瀏覽_自定一.rpt"); 

            if (!dt.Columns.Contains("寄庫日期")) dt.Columns.Add("寄庫日期", typeof(System.String));
            if (!dt.Columns.Contains("產品組成")) dt.Columns.Add("產品組成", typeof(System.String));
            if (!dt.Columns.Contains("No")) dt.Columns.Add("No", typeof(System.String));
            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["No"] = (i + 1).ToString();

                if (dt.Rows[i]["ittrait"].ToDecimal() == 1) dt.Rows[i]["產品組成"] = "組合品";
                else if (dt.Rows[i]["ittrait"].ToDecimal() == 2) dt.Rows[i]["產品組成"] = "組裝品";
                else if (dt.Rows[i]["ittrait"].ToDecimal() == 3) dt.Rows[i]["產品組成"] = "單一商品";

                var d = Common.User_DateTime == 1 ? dt.Rows[i]["indate"].ToString() : dt.Rows[i]["indate1"].ToString();
                dt.Rows[i]["寄庫日期"] = Date.AddLine(d.Trim());
            }

            GroupbyDate = from r in dt.AsEnumerable()
                          group r by r["CuNo"].ToString();

            g = GroupbyDate.FirstOrDefault();
            No = g.FirstOrDefault().Field<string>("No");
            btnInNo_Click(null, null);
        }

        void SetDataSource(IOrderedEnumerable<DataRow> g)
        {
            if (g != null)
            {
                var temp = g.CopyToDataTable();
                dataGridViewT1.DataSource = temp;
                for (int i = 0; i < dataGridViewT1.Rows.Count; i++)
                {
                    dataGridViewT1["序號", i].Value = (i + 1).ToString();
                }
            }
            else
            {
                dataGridViewT1.DataSource = null;
                foreach (var item in this.Controls.OfType<TextBoxT>())
                {
                    item.Clear();
                }
            }
        }

        void WriteToTitle(int index)
        {
            var temp = (DataTable)dataGridViewT1.DataSource;
            textBoxT1.Text = temp.Rows[index]["CuNo"].ToString().Trim();
            textBoxT2.Text = temp.Rows[index]["CuName1"].ToString().Trim();
            textBoxT3.Text = temp.Rows[index]["StNo"].ToString().Trim();
            textBoxT4.Text = temp.Rows[index]["StName"].ToString().Trim();
            var itno = temp.Rows[index]["ItNo"].ToString().Trim();
            textBoxT6.Text = itno;
            decimal d = g.Where(r => r["itno"].ToString().Trim() == itno).Sum(r => r["nonqty"].ToDecimal());
            textBoxT5.Text = d.ToString("f" + Common.Q);
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (e.StateChanged == DataGridViewElementStates.Selected)
                {
                    WriteToTitle(e.Row.Index);
                }
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            g = GroupbyDate.FirstOrDefault();
            SetDataSource(g.OrderBy(r => r["InNo"].ToString()));
            btnInNo_Click(null, null);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var index = GroupbyDate.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == 0)
            {
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupbyDate.ToList()[--index];
                SetDataSource(g.OrderBy(r => r["InNo"].ToString()));
                btnInNo_Click(null, null);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var index = GroupbyDate.ToList().FindIndex(r => r.Key.Trim() == g.Key.Trim());
            if (index == GroupbyDate.Count() - 1)
            {
                MessageBox.Show("已最下一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g = GroupbyDate.ToList()[++index];
                SetDataSource(g.OrderBy(r => r["InNo"].ToString()));
                btnInNo_Click(null, null);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            g = GroupbyDate.LastOrDefault();
            SetDataSource(g.OrderBy(r => r["InNo"].ToString()));
            btnInNo_Click(null, null);
        }

        void SetBtnColor(Button btn)
        {
            foreach (var item in this.Controls.OfType<Button>())
            {
                item.ForeColor = Color.Black;
            }
            btn.ForeColor = Color.Red;

            var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            if (p == null) return;
            else
            {
                No = p.Cells["定序"].Value.ToString();
            }
        }

        private void btnInNo_Click(object sender, EventArgs e)
        {
            //SetBtnColor(btnInNo);
            SetDataSource(g.OrderBy(r => r["InNo"].ToString()));

            dataGridViewT1.Search("定序", No.Trim());
            var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            var index = p.Index == -1 ? 0 : p.Index;
            WriteToTitle(index);
        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            //SetBtnColor(btnDate);
            SetDataSource(g.OrderBy(r => r["indate"].ToString()));

            dataGridViewT1.Search("定序", No.Trim());
            var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            var index = p.Index == -1 ? 0 : p.Index;
            WriteToTitle(index);
        }

        private void btnItNo_Click(object sender, EventArgs e)
        {
           // SetBtnColor(btnItNo);
            SetDataSource(g.OrderBy(r => r["itno"].ToString()));

            dataGridViewT1.Search("定序", No.Trim());
            var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            var index = p.Index == -1 ? 0 : p.Index;
            WriteToTitle(index);
        }

        private void btnMemo_Click(object sender, EventArgs e)
        {
           // SetBtnColor(btnMemo);
            SetDataSource(g.OrderBy(r => r["memo"].ToString()));

            dataGridViewT1.Search("定序", No.Trim());
            var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            var index = p.Index == -1 ? 0 : p.Index;
            WriteToTitle(index);
        }

        void paramsInit()
        {
            string path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\寄庫資料瀏覽_明細表.rpt";
            }
            else if (radioT9.Checked)
            {
                path = Common.reportaddress + "Report\\寄庫資料瀏覽_產品總表.rpt";
            }
            else if (radioT10.Checked)
            {
                path = Common.reportaddress + "Report\\寄庫庫存明細表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\寄庫資料瀏覽_自定一.rpt";
            }
            else if (radioT11.Checked)
            {
                path = Common.reportaddress + "Report\\寄庫總表.rpt";
            }
            


            string txtend = "";
            if (radioT3.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + Common.dtEnd.Rows[6]["tamemo"].ToString() + Common.dtEnd.Rows[7]["tamemo"].ToString();
            else if (radioT4.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + Common.dtEnd.Rows[9]["tamemo"].ToString() + Common.dtEnd.Rows[10]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + Common.dtEnd.Rows[12]["tamemo"].ToString() + Common.dtEnd.Rows[13]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + Common.dtEnd.Rows[15]["tamemo"].ToString() + Common.dtEnd.Rows[16]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[17]["tamemo"].ToString() + Common.dtEnd.Rows[18]["tamemo"].ToString() + Common.dtEnd.Rows[19]["tamemo"].ToString();
            else txtend = "";

            if (radioT9.Checked)
            {
                //目前產品總表，相同客戶未歸屬到同一筆，僅以產品別-明細方式印出
                rp = new RPT(dt, path);
            }
            else if (radioT10.Checked)
            {
                #region 將產生"客戶寄庫庫存" 之  DT_。
                DataTable DT_ = new DataTable();
                DataColumn 產品編號 = new DataColumn("產品編號", typeof(string));
                DataColumn 客戶簡稱 = new DataColumn("客戶簡稱", typeof(string));
                DataColumn 客戶編號 = new DataColumn("客戶編號", typeof(string));
                DataColumn 產品規格 = new DataColumn("產品規格", typeof(string));
                DataColumn 結餘數量 = new DataColumn("結餘數量", typeof(decimal));
                DT_.Columns.Add(產品編號);
                DT_.Columns.Add(客戶簡稱);
                DT_.Columns.Add(客戶編號);
                DT_.Columns.Add(產品規格);
                DT_.Columns.Add(結餘數量);

                DataRow[] foundRows;

               // foundRows = dt.Select("cuno,cuname1,itno,itname,nonqty", "cuno ASC, itno ASC");
                foundRows = dt.Select("", "cuno ASC, itno ASC");
                string OldCuno = "", OldItno = "";
                decimal sum = 0m;

                if (foundRows.Length == 1)
                {
                    DataRow dr = DT_.NewRow();
                    dr["客戶編號"] = foundRows[0]["cuno"].ToString();
                    dr["客戶簡稱"] = foundRows[0]["cuname1"].ToString();
                    dr["產品編號"] = foundRows[0]["itno"].ToString();
                    dr["產品規格"] = foundRows[0]["itname"].ToString();
                    dr["結餘數量"] = foundRows[0]["nonqty"].ToString().ToDecimal();
                    DT_.Rows.Add(dr);
                }
                else
                {
                    bool first = true;
                    for (int i = 0; i < foundRows.Length ; i++)
                    {
                        if (first)
                        {
                            OldCuno = foundRows[i]["cuno"].ToString();
                            OldItno = foundRows[i]["itno"].ToString();
                            sum += foundRows[i]["nonqty"].ToString().ToDecimal();
                            first = false;
                            continue;
                        }

                        if (OldCuno != foundRows[i]["cuno"].ToString() || OldItno != foundRows[i]["itno"].ToString() || (i == foundRows.Length-1))
                        {

                            DataRow dr = DT_.NewRow();
                            dr["客戶編號"] = foundRows[i - 1]["cuno"].ToString();
                            dr["客戶簡稱"] = foundRows[i - 1]["cuname1"].ToString();
                            dr["產品編號"] = foundRows[i - 1]["itno"].ToString();
                            dr["產品規格"] = foundRows[i - 1]["itname"].ToString();
                            dr["結餘數量"] = sum;
                            DT_.Rows.Add(dr);
                            sum = 0m;
                            
                            OldCuno = foundRows[i]["cuno"].ToString();
                            OldItno = foundRows[i]["itno"].ToString();
                        }
                        sum += foundRows[i]["nonqty"].ToString().ToDecimal();
                    }
                    if (DT_.Rows[DT_.Rows.Count - 1]["客戶編號"].ToString() == foundRows[foundRows.Length - 1]["cuno"].ToString() && DT_.Rows[DT_.Rows.Count - 1]["產品編號"].ToString() == foundRows[foundRows.Length - 1]["itno"].ToString())
                    {
                        DT_.Rows[DT_.Rows.Count - 1]["結餘數量"] = DT_.Rows[DT_.Rows.Count - 1]["結餘數量"].ToDecimal() + foundRows[foundRows.Length - 1]["nonqty"].ToString().ToDecimal();
                    }
                    else 
                    {
                        DataRow dr = DT_.NewRow();
                        dr["客戶編號"] = foundRows[foundRows.Length-1]["cuno"].ToString();
                        dr["客戶簡稱"] = foundRows[foundRows.Length - 1]["cuname1"].ToString();
                        dr["產品編號"] = foundRows[foundRows.Length - 1]["itno"].ToString();
                        dr["產品規格"] = foundRows[foundRows.Length - 1]["itname"].ToString();
                        dr["結餘數量"] = foundRows[foundRows.Length - 1]["nonqty"].ToString();
                        DT_.Rows.Add(dr);
                    }
                }
                #endregion 
                rp = new RPT(DT_, path);

            }
            else if (radioT11.Checked)
            {
                #region 將產生"寄庫總表" 之  DT_。
                DataTable DT_ = new DataTable();
                DataColumn 產品編號 = new DataColumn("產品編號", typeof(string));
                DataColumn 客戶簡稱 = new DataColumn("客戶簡稱", typeof(string));
                DataColumn 客戶編號 = new DataColumn("客戶編號", typeof(string));
                DataColumn 產品規格 = new DataColumn("產品規格", typeof(string));
                DataColumn 結餘數量 = new DataColumn("結餘數量", typeof(decimal));
                DT_.Columns.Add(產品編號);
                DT_.Columns.Add(客戶簡稱);
                DT_.Columns.Add(客戶編號);
                DT_.Columns.Add(產品規格);
                DT_.Columns.Add(結餘數量);

                DataRow[] foundRows;

                // foundRows = dt.Select("cuno,cuname1,itno,itname,nonqty", "cuno ASC, itno ASC");
                foundRows = dt.Select("", "itno ASC");
                string  OldItno = "";
                decimal sum = 0m;

                if (foundRows.Length == 1)
                {
                    DataRow dr = DT_.NewRow();
                    dr["客戶編號"] = foundRows[0]["cuno"].ToString();
                    dr["客戶簡稱"] = foundRows[0]["cuname1"].ToString();
                    dr["產品編號"] = foundRows[0]["itno"].ToString();
                    dr["產品規格"] = foundRows[0]["itname"].ToString();
                    dr["結餘數量"] = foundRows[0]["nonqty"].ToString().ToDecimal();
                    DT_.Rows.Add(dr);
                }
                else
                {
                    bool first = true;
                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        if (first)
                        {
                            OldItno = foundRows[i]["itno"].ToString();
                            sum += foundRows[i]["nonqty"].ToString().ToDecimal();
                            first = false;
                            continue;
                        }

                        if (OldItno != foundRows[i]["itno"].ToString() || (i == foundRows.Length - 1))
                        {

                            DataRow dr = DT_.NewRow();
                            dr["客戶編號"] = foundRows[i - 1]["cuno"].ToString();
                            dr["客戶簡稱"] = foundRows[i - 1]["cuname1"].ToString();
                            dr["產品編號"] = foundRows[i - 1]["itno"].ToString();
                            dr["產品規格"] = foundRows[i - 1]["itname"].ToString();
                            dr["結餘數量"] = sum;
                            DT_.Rows.Add(dr);
                            sum = 0m;
                            OldItno = foundRows[i]["itno"].ToString();
                        }
                        sum += foundRows[i]["nonqty"].ToString().ToDecimal();
                    }
                    if (DT_.Rows[DT_.Rows.Count - 1]["產品編號"].ToString() == foundRows[foundRows.Length - 1]["itno"].ToString())
                    {
                        DT_.Rows[DT_.Rows.Count - 1]["結餘數量"] = DT_.Rows[DT_.Rows.Count - 1]["結餘數量"].ToDecimal() + foundRows[foundRows.Length - 1]["nonqty"].ToString().ToDecimal();
                    }
                    else
                    {
                        DataRow dr = DT_.NewRow();
                        dr["客戶編號"] = foundRows[foundRows.Length - 1]["cuno"].ToString();
                        dr["客戶簡稱"] = foundRows[foundRows.Length - 1]["cuname1"].ToString();
                        dr["產品編號"] = foundRows[foundRows.Length - 1]["itno"].ToString();
                        dr["產品規格"] = foundRows[foundRows.Length - 1]["itname"].ToString();
                        dr["結餘數量"] = foundRows[foundRows.Length - 1]["nonqty"].ToString();
                        DT_.Rows.Add(dr);
                    }
                }
                #endregion
                rp = new RPT(DT_, path);
            }
            else
            {
                rp = new RPT(dt, path);
            }
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "DateCreated", "製表日期:"+Date.GetToday()});
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Print();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.PreView();
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
                dt.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 

    }
}
