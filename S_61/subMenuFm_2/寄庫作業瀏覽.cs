using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class 寄庫作業瀏覽 : Formbase
    {
        JBS.JS.xEvents xe;
        public string ResultInno { get; private set; }
        public string SeekInno { private get; set; }
        DataTable dt = new DataTable();
        List<ButtonSmallT> list;
        int 瀏覽筆數 = Common.SearchCount;//後補
        public 寄庫作業瀏覽()
        {
            InitializeComponent();
        }

        private void 寄庫作業瀏覽_Load(object sender, EventArgs e)
        {
           this.xe = new JBS.JS.xEvents();
           list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7 };
           dataGridViewT1.DataSource = dt;
           dataGridViewT1ViewChange("inno",SeekInno,"","","initial");
           this.備註說明.HeaderText = Common.Sys_MemoUdf;
           this.包裝數量.Set庫存數量小數();
           this.寄庫未領數量.Set庫存數量小數();
           this.寄庫日期.DataPropertyName = Common.User_DateTime == 1 ? "寄庫日期" : "寄庫日期1";
           this.indate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
           this.dataGridViewT1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
             
        }

        private void dataGridViewT1ViewChange(string key1, string value1, string key2, string OrderBy變數, string inID)//1.不管順逆，因為是取中間值，所以不需考慮順逆排 2.兩參數排序共用此函式
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable DownDt = new DataTable())
            using (DataTable UpDt = new DataTable())
            {
                #region  條件設定
                dt.Clear();
                string 下Top數 = "", 上Top數="";
                string 下WhereStr1 = "and 0=0", 上WhereStr1 = " and 0=0 ", 下OrderBy1 = "", 上OrderBy1 = "", 下OrderBy2 = "", 上OrderBy2 = "";//,SortStr1="",SortStr2="";
                if (瀏覽筆數 % 2 == 0)
                    下Top數 = 上Top數 = (瀏覽筆數 / 2).ToString();
                else
                {
                    下Top數 = ((瀏覽筆數 / 2)).ToString();
                    上Top數 = (((瀏覽筆數 / 2) +1)).ToString();
                }
                if (key1 != "")
                {
                    cm.Parameters.AddWithValue(過濾(key1), value1);
                    上WhereStr1 = " and " + key1 + " <= @" + 過濾(key1);
                    下WhereStr1 = " and " + key1 + " > @" + 過濾(key1);
                    上OrderBy1 = " order by " + key1 + " desc";
                    下OrderBy1 = " order by " + key1 + " asc ";
                }
                if (key2 != "") 
                {
                    上OrderBy2 = "," + key2 + " " + OrderBy變數;
                    下OrderBy2 = "," + key2 + " " + OrderBy變數;
                }
                #endregion
                #region SQL語法，撈出上與下(UpDt,DownDt)
                //上 35 34 33
                cm.CommandText =
"  select top " + 上Top數 + " InStkD.stno as 倉庫編號 , cust.cuno as 客戶編號 , InStkD.itno as 產品編號 ,"
+ " SUBSTRING(InStkD.indate,1,3) + '/'+SUBSTRING(InStkD.indate,4,2)+ '/'+SUBSTRING(InStkD.indate,6,2) 寄庫日期, "
+ " SUBSTRING(InStkD.indate1,1,4) + '/'+SUBSTRING(InStkD.indate1,5,2)+ '/'+SUBSTRING(InStkD.indate1,7,2) 寄庫日期1, "
+ " 產品組成 = case when InStkD.ittrait=1 then '組合品' when InStkD.ittrait=2 then '組裝品' when InStkD.ittrait=3 then '單一商品' end "
+ " ,instkD.bomid as 'Bomid_',* "
+ " from  InStkD,cust "
+ " where InStkD.sano='寄庫開帳' and InStkD.cuno = cust.cuno "
+ 上WhereStr1 + " "
+ 上OrderBy1 + 上OrderBy2;
                da.Fill(UpDt);
                //下 36 37 38
                cm.CommandText =
"  select top " + 下Top數 + " InStkD.stno as 倉庫編號 , cust.cuno as 客戶編號 , InStkD.itno as 產品編號  ,"
+ " SUBSTRING(InStkD.indate,1,3) + '/'+SUBSTRING(InStkD.indate,4,2)+ '/'+SUBSTRING(InStkD.indate,6,2) 寄庫日期, "
+ " SUBSTRING(InStkD.indate1,1,4) + '/'+SUBSTRING(InStkD.indate1,5,2)+ '/'+SUBSTRING(InStkD.indate1,7,2) 寄庫日期1, "
+ " 產品組成 = case when InStkD.ittrait=1 then '組合品' when InStkD.ittrait=2 then '組裝品' when InStkD.ittrait=3 then '單一商品' end "
+ " ,instkD.bomid as 'Bomid_',* "
+ " from  InStkD,cust "
+ " where InStkD.sano='寄庫開帳' and InStkD.cuno = cust.cuno "
+ 下WhereStr1
+ 下OrderBy1 + 上OrderBy2;
                da.Fill(DownDt);
                dt = UpDt.Clone();
                #endregion
                string QueryInid = "";
                if (UpDt.Rows.Count > 0)
                {
                     QueryInid = UpDt.Rows[0]["inid"].ToString();
                    #region 將(UpDt,DownDt)合併，填入dt
                    for (int i = (UpDt.Rows.Count-1); i >= 0; i--)
                    {
                        dt.ImportRow(UpDt.Rows[i]);
                    }
                    //dt = UpDt.Copy();
                    dt.Merge(DownDt);
                    //DatagridView
                    //dt.DefaultView.Sort = key1 + " asc";
                    dataGridViewT1.DataSource = dt;
                    #endregion
                    #region 1.找出index 2.調整DatagridViewIndex 
                    int index = 0;
                    if (inID == "initial")
                        index = FindIndex(dt, key1, value1);
                    else if (inID == "query")
                        index = FindIndex(dt, QueryInid);
                    else
                        index = FindIndex(dt, inID);
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                    dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
                    dataGridViewT1.Rows[index].Selected = true;
                    #endregion 
                }

            }
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (e.Row.Index == dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected))
                {
                    var index = e.Row.Index;

                    textBoxT1.Text = dt.DefaultView[index]["itno"].ToString();
                    textBoxT2.Text = dt.DefaultView[index]["stno"].ToString();
                    textBoxT3.Text = dt.DefaultView[index]["stname"].ToString();
                    textBoxT4.Text = dt.DefaultView[index]["cuno"].ToString();
                    textBoxT5.Text = dt.DefaultView[index]["cutel1"].ToString();
                    textBoxT6.Text = dt.DefaultView[index]["cuper1"].ToString();
                }
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dt.Rows.Count == 0)
            {
                this.ResultInno = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) this.ResultInno = "";
                else
                {
                    this.ResultInno = dt.DefaultView[index]["inno"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Search(string key1,string key2, string OrderBy變數)//1.以key1為主排序，不管順逆排物理上都一樣結果 2.key2可排序
        {
            int SelectIndex = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            string value1 = "", inID = dt.DefaultView[SelectIndex]["inID"].ToString();
            if (SelectIndex == -1) return;
            value1 = dt.Rows[SelectIndex][過濾(key1)].ToString();
            dataGridViewT1ViewChange(key1, value1, key2, OrderBy變數, inID);
        }

        private void Q2_Click(object sender, EventArgs e)
        {
            //顯示 asc
            Search("inno", "", "");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q2.ForeColor = Color.Red;
        }

        private void Q3_Click(object sender, EventArgs e)
        {
            //顯示 indate desc, asc
            Search("InStkD.indate", "inno", "asc");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q3.ForeColor = Color.Red;
        }
        
        private void Q4_Click(object sender, EventArgs e)
        {
            //顯示 asc, asc
            Search("itno", "InStkD.indate", "asc");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q4.ForeColor = Color.Red;
        }

        private void Q5_Click(object sender, EventArgs e)
        {
            //顯示 indate desc, cust asc
            Search("InStkD.indate", "cust.cuno", "asc");//查詢
            For_DefaultViewSortAndIndex("cuno asc,indate desc");//排序
            list.ForEach(t => t.ForeColor = Color.Black);
            Q5.ForeColor = Color.Red;
        }

        private void Q6_Click(object sender, EventArgs e)
        {
            //顯示asc,asc
            Search("cust.cuno", "itno", "asc");
            For_DefaultViewSortAndIndex("cuno asc,itno asc");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q6.ForeColor = Color.Red;
        }

        private void Q7_Click(object sender, EventArgs e)
        {
            //顯示asc,asc
            Search("InStkD.stno", "itno", "asc");
            For_DefaultViewSortAndIndex("stno asc,itno asc");
            list.ForEach(t => t.ForeColor = Color.Black);
            Q7.ForeColor = Color.Red;
        }


        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Enabled = false;
                if (cuno.TrimTextLenth() > 0 && indate.TrimTextLenth() > 0)
                {
                    //Q5
                    dataGridViewT1ViewChange("InStkD.indate", indate.Text.Trim(), "cust.cuno", "asc", "query");//查詢
                    //排序
                    dt.DefaultView.Sort = "cuno asc,indate desc";
                    var SortedDt = For_DefaultViewSortAndIndex("cuno asc,indate desc");
                    DatagridViewIndex_符合條件第一筆(SortedDt, "indate", indate.Text.Trim(), "cuno", cuno.Text.Trim());
                }
                else if (cuno.TrimTextLenth() > 0 && itno.TrimTextLenth() > 0)
                {
                    //Q6
                    dataGridViewT1ViewChange("cust.cuno", cuno.Text.Trim(), "itno", "asc", "query");
                    dt.DefaultView.Sort = "cuno asc,itno asc";
                    var SortedDt = For_DefaultViewSortAndIndex("cuno asc,itno asc");
                    DatagridViewIndex_符合條件第一筆(SortedDt, "cuno", cuno.Text.Trim(), "itno", itno.Text.Trim());
                }
                else if (stno.TrimTextLenth() > 0 && itno.TrimTextLenth() > 0)
                {
                    //Q7
                    dataGridViewT1ViewChange("InStkD.stno", stno.Text.Trim(), "itno", "asc", "query");
                    dt.DefaultView.Sort = "stno asc,itno asc";
                    var SortedDt = For_DefaultViewSortAndIndex("sano asc,itno asc");
                    DatagridViewIndex_符合條件第一筆(SortedDt, "stno", stno.Text.Trim(), "itno", itno.Text.Trim());
                }
                else if (inno.TrimTextLenth() > 0)
                {
                    //Q2
                    dataGridViewT1ViewChange("inno", inno.Text.Trim(), "", "", "query");
                    DatagridViewIndex_符合條件第一筆(dt, "inno", inno.Text.Trim(), "", "");

                }
                else if (indate.TrimTextLenth() > 0)
                {
                    //Q3
                    dataGridViewT1ViewChange("indate", indate.Text.Trim(), "inno", "asc", "query");
                    DatagridViewIndex_符合條件第一筆(dt, "indate", indate.Text.Trim(), "", "");
                }
                else if (itno.TrimTextLenth() > 0)
                {
                    //Q4
                    dataGridViewT1ViewChange("itno", itno.Text.Trim(), "InStkD.indate", "asc", "query");
                    DatagridViewIndex_符合條件第一筆(dt, "itno", itno.Text.Trim(), "", "");
                }
                if (cuno.TrimTextLenth() > 0)
                {
                    //Q5
                    dataGridViewT1ViewChange("InStkD.indate", DateTime.Now.ToString("yyyyMMdd"), "cust.cuno", "asc", "query");
                    dt.DefaultView.Sort = "cuno asc,indate desc";
                    var SortedDt = For_DefaultViewSortAndIndex("cuno asc,indate desc");
                    DatagridViewIndex_符合條件第一筆(SortedDt, "cuno", cuno.Text.Trim(), "", "");
                }
                else if (cuno.TrimTextLenth() > 0)
                {
                    //Q6
                    dataGridViewT1ViewChange("cust.cuno", cuno.Text.Trim(), "itno", "asc", "query");
                    dt.DefaultView.Sort = "cuno asc,itno asc";
                    var SortedDt = For_DefaultViewSortAndIndex("cuno asc,itno asc");
                    DatagridViewIndex_符合條件第一筆(SortedDt, "cuno", cuno.Text.Trim(), "", "");
                }
                else if (stno.TrimTextLenth() > 0)
                {
                    //Q7
                    dataGridViewT1ViewChange("InStkD.stno", stno.Text.Trim(), "itno", "asc", "query");
                    dt.DefaultView.Sort = "stno asc,itno asc";
                    var SortedDt = For_DefaultViewSortAndIndex("stno asc,itno asc");
                    DatagridViewIndex_符合條件第一筆(SortedDt, "stno", stno.Text.Trim(), "", "");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnQuery.Enabled = true;
            }
        }

        private DataRow[] For_DefaultViewSortAndIndex(string SortStr)
        {
            var SortedDt = dt.Select("", SortStr);
            int SelectIndex = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            string inID = dt.DefaultView[SelectIndex]["inID"].ToString();
            if (SelectIndex == -1) return SortedDt;
            int index = FindIndex(SortedDt, inID);
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
            dt.DefaultView.Sort = SortStr;
            return SortedDt;
        }
        private int FindIndex(DataRow[] SortedDt, string inID)
        {
            for (int i = 0; i < SortedDt.Length - 1; i++)
            {
                if (SortedDt[i]["inID"].ToString() == inID)
                    return (i);
            }
            return (SortedDt.Length-1);
        }

        private void DatagridViewIndex_符合條件第一筆(DataRow[] SortedDt, string key1, string value1, string key2, string value2)
        {
            int index = 0;
            index = FindIndex(SortedDt, key1, value1, key2, value2);
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }

        private int FindIndex(DataRow[] SortedDt, string key1, string value1, string key2, string value2)
        {
            if (key2 != "")
            {
                for (int i = 0; i < SortedDt.Length - 1; i++)
                {
                    if (SortedDt[i][key1].ToString() == value1 && SortedDt[i][key2].ToString() == value2)
                        return (i);
                }
            }
            else
            {
                for (int i = 0; i < SortedDt.Length - 1; i++)
                {
                    if (SortedDt[i][key1].ToString() == value1 )
                        return (i);
                }
            }
            return (dt.Rows.Count - 1);
        }

        private void DatagridViewIndex_符合條件第一筆(DataTable dt, string key1, string value1, string key2, string value2)
        {
            int index = 0;
            index = FindIndex(dt, key1, value1);
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }

        private string 過濾(string key)
        {
            string str = key;
            switch (str.ToLower())
            {
                case "cust.cuno":
                    str = "cuno";
                    break;
                case "instkd.indate":
                    str = "indate";
                    break;
                case "instkd.stno":
                    str = "stno";
                    break;
            }
            return str;
        }

        private void GridSort(string SortStr, string key1)
        {
            dt.DefaultView.Sort = SortStr;

            int SelectIndex = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            string value1 = "";
            if (SelectIndex == -1) return;
            else value1 = dt.Rows[SelectIndex][key1].ToString();

            int index = FindIndex(dt, key1, value1);
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }

        private int FindIndex(DataTable dt, string inID)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i]["inID"].ToString() == inID)
                    return (i);
            }
            return (dt.Rows.Count - 1);
        }

        private int FindIndex(DataTable dt, string key1, string value1)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i][key1].ToString() == value1)
                    return (i);
            }
            return (dt.Rows.Count - 1);
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            using (var frm = new SOther.FrmPicture())
            {
                var row = Common.load("Check", "item", "itno", dt.DefaultView[index]["ItNo"].ToString().Trim());
                frm.image = row["pic"];
                frm.ShowDialog();
            }
        }

        private void btnDesp_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            using (var frm = new JE.SOther.FrmDesp(false, FormStyle.Mini))
            {
                var inid = dt.DefaultView[index]["inid"].ToString();
                frm.dr = Common.load("Check", "Instkd", "inid", inid);
                frm.SeekNo = inid;
                frm.ShowDialog();
            }
        }

        private void btnBom_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            var trait = dt.DefaultView[index]["ittrait"].ToDecimal();
            var Bomid = dt.DefaultView[index]["Bomid_"].ToString().Trim();
            if (trait != 1 && trait != 2)
            {
                MessageBox.Show("只有組合品或組裝品可編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridViewT1.Focus();
                return;
            }

            using (var frm = new subMenuFm_2.FrmSale_Bom())
            {
                frm.BoItNo1 = dt.DefaultView[index]["itno"].ToString().Trim();
                frm.BoItName1 = dt.DefaultView[index]["itname"].ToString().Trim();
                frm.JustForBrow = true;
                frm.TTable = "instkBom";
                frm.TKey = Bomid;
                frm.ShowDialog();
            }
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                btnStock.Focus();
                subMenuFm_2.FrmSale_Stock frm = new subMenuFm_2.FrmSale_Stock();
                frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                frm.ShowDialog();
                dataGridViewT1.Focus();
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0)
            {
                this.ResultInno = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) this.ResultInno = "";
                else
                {
                    this.ResultInno = dt.DefaultView[index]["InNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cuno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void stno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void itno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                btnQuery_Click(null, null);
            }
            else if (keyData == Keys.F5)
            {
                btnPicture_Click(null, null);
            }
            else if (keyData == Keys.F6)
            {
               btnDesp_Click(null, null);
            }
            else if (keyData == Keys.F7)
            {
                btnBom_Click(null, null);
            }
            else if (keyData == Keys.F8) 
            {
                btnStock_Click(null, null);
            }
            else if (keyData == Keys.F4)
            {
                btnGet_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F2") && keyData.ToString().EndsWith("Shift"))
            {
                Q2_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F3") && keyData.ToString().EndsWith("Shift"))
            {
                Q3_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                Q4_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F5") && keyData.ToString().EndsWith("Shift"))
            {
                Q5_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F6") && keyData.ToString().EndsWith("Shift"))
            {
                Q6_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F7") && keyData.ToString().EndsWith("Shift"))
            {
                Q7_Click(null, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
