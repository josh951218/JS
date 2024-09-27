using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
namespace S_61.S2
{
    public partial class Frm異動表 : Formbase
    {
        public DataTable dt = new DataTable(); //cuno and itno group
        public DataTable dt_AllDtad = new DataTable();//for print
        public string stno = "";
        public int indate1 = 0, indate2 = 0;
        int CurrentIndex = 0 , max =0 , min=0;
        System.Threading.Tasks.Task Task_;
        string indate異動日期="",oudate異動日期;

        public Frm異動表()
        {
            InitializeComponent();
            this.異動數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.結餘數量.DefaultCellStyle.Format = "f" + Common.Q;

            indate異動日期 = Common.User_DateTime == 1 ?
"異動日期=SUBSTRING(indate,1,3) + '/'+SUBSTRING(indate,4,2)+ '/'+SUBSTRING(indate,6,2)"
:
"異動日期= SUBSTRING(indate1,1,4) + '/'+SUBSTRING(indate1,5,2)+ '/'+SUBSTRING(indate1,7,2)";
            oudate異動日期 = Common.User_DateTime == 1 ?
"異動日期=SUBSTRING(oudate,1,3) + '/'+SUBSTRING(oudate,4,2)+ '/'+SUBSTRING(oudate,6,2)"
:
"異動日期= SUBSTRING(oudate1,1,4) + '/'+SUBSTRING(oudate1,5,2)+ '/'+SUBSTRING(oudate1,7,2)";
        }

        private void Frm異動表_Load(object sender, EventArgs e)
        {
            Action<object> action = (object obj) =>
            {
                LoadAlldata(dt_AllDtad);
            };
            Task_ = System.Threading.Tasks.Task.Factory.StartNew(action, "1");
            dataGridView_changeAndShow(CurrentIndex);
            max = dt.Rows.Count - 1;
            radioT2.SetUserDefineRpt("寄庫領庫異動自定一.rpt");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentIndex + 1 > max) return;
            dataGridView_changeAndShow(++CurrentIndex);
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            if (CurrentIndex - 1 < 0) return;
            dataGridView_changeAndShow(--CurrentIndex);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            CurrentIndex = max;
            dataGridView_changeAndShow(max);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            CurrentIndex = min;
            dataGridView_changeAndShow(min);
        }

        private void dataGridView_changeAndShow(int index)
        {
            
            textBoxT1.Text = dt.Rows[index]["cuname1"].ToString();
            textBoxT2.Text = dt.Rows[index]["itname"].ToString();
            ItNo.Text = dt.Rows[index]["itno"].ToString();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                #region new dt_DataGrdiViewSource
                DataTable dt_DataGrdiViewSource = new DataTable();
                dataGridViewT1.DataSource = null;
                dataGridViewT1.DataSource = dt_DataGrdiViewSource;
                DataColumn 異動日期 = new DataColumn("異動日期", typeof(string));
                DataColumn 異動單號 = new DataColumn("異動單號", typeof(string));
                DataColumn 單據類別 = new DataColumn("單據類別", typeof(string));
                DataColumn 異動數量 = new DataColumn("異動數量", typeof(decimal));
                DataColumn 結餘數量 = new DataColumn("結餘數量", typeof(decimal));
                DataColumn 倉庫名稱 = new DataColumn("倉庫名稱", typeof(string));
                DataColumn 成本 = new DataColumn("成本", typeof(string));
                dt_DataGrdiViewSource.Columns.Add(異動日期);
                dt_DataGrdiViewSource.Columns.Add(異動單號);
                dt_DataGrdiViewSource.Columns.Add(單據類別);
                dt_DataGrdiViewSource.Columns.Add(異動數量);
                dt_DataGrdiViewSource.Columns.Add(結餘數量);
                dt_DataGrdiViewSource.Columns.Add(倉庫名稱);
                dt_DataGrdiViewSource.Columns.Add(成本);
                #endregion
                DataTable dt_InStkD = new DataTable();//寄入
                DataTable dt_OuStkD = new DataTable();//領出
                #region 載入寄庫表
                //if (stno != "")
                //    cmd.CommandText = @"select indate as 異動日期,inno as 異動單號,單據類別='寄庫+',inqty as 異動數量,stname as 倉庫名稱,cuno,itno,indate,inno from InStkD where cuno =@cuno and itno =@itno and stno = @stno order by inno,itno,cuno ";
                //else
                cmd.CommandText = @"select " + indate異動日期 + @",inno as 異動單號,單據類別='寄庫+',inqty as 異動數量,stname as 倉庫名稱,cuno,itno,indate,inno from InStkD where cuno =@cuno and itno =@itno order by inno,itno,cuno ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("cuno", dt.Rows[index]["cuno"].ToString());
                cmd.Parameters.AddWithValue("itno", dt.Rows[index]["itno"].ToString());
                cmd.Parameters.AddWithValue("stno", stno);
                da.Fill(dt_InStkD);
                #endregion
                decimal 累計結餘數量 = 0m;
                for (int i = 0; i < dt_InStkD.Rows.Count; i++)
                {
                    累計結餘數量 += dt_InStkD.Rows[i]["異動數量"].ToString().ToDecimal();
                    #region 將寄庫填入  dt_DataGrdiViewSource
                    DataRow dr = dt_DataGrdiViewSource.NewRow();
                    dr["異動日期"] = dt_InStkD.Rows[i]["異動日期"].ToString();
                    dr["異動單號"] = dt_InStkD.Rows[i]["異動單號"].ToString();
                    dr["單據類別"] = dt_InStkD.Rows[i]["單據類別"].ToString();
                    dr["異動數量"] = dt_InStkD.Rows[i]["異動數量"].ToString().ToDecimal();
                    dr["結餘數量"] = 累計結餘數量;
                    dr["倉庫名稱"] = dt_InStkD.Rows[i]["倉庫名稱"].ToString();
                    dr["成本"] = "0";
                    dt_DataGrdiViewSource.Rows.Add(dr);
                    #endregion
                    #region 避免重複_同銷貨單號、寄貨單號、產品、客戶重複撈，只用一筆撈。
                    if (i < dt_InStkD.Rows.Count - 1)
                    {
                        if (dt_InStkD.Rows[i]["cuno"].ToString() == dt_InStkD.Rows[i + 1]["cuno"].ToString() &&
                           dt_InStkD.Rows[i]["itno"].ToString() == dt_InStkD.Rows[i + 1]["itno"].ToString() &&
                           dt_InStkD.Rows[i]["inno"].ToString() == dt_InStkD.Rows[i + 1]["inno"].ToString())
                            continue;
                    }
                    #endregion
                    #region 載入領庫表
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("cuno", dt_InStkD.Rows[i]["cuno"].ToString());
                    cmd.Parameters.AddWithValue("itno", dt_InStkD.Rows[i]["itno"].ToString());
                    cmd.Parameters.AddWithValue("inno", dt_InStkD.Rows[i]["inno"].ToString());
                    cmd.Parameters.AddWithValue("stno", stno);
                    cmd.CommandText =
@"select "+oudate異動日期+@",ouno as 異動單號,單據類別='領庫-',ouqty as 異動數量,結餘數量='',stname as 倉庫名稱
    from OuStkD,cust
    where OuStkD.cuno = cust.cuno 
          and OuStkD.cuno = @cuno
	      and OuStkD.itno = @itno
	      and OuStkD.inno = @inno";
                    if (stno != "") cmd.CommandText += " and OuStkD.stno = @stno";
                    dt_OuStkD.Clear();
                    da.Fill(dt_OuStkD);
                    #endregion
                    #region 將領庫填入  dt_DataGrdiViewSource
                    for (int k = 0; k < dt_OuStkD.Rows.Count; k++)
                    {
                        累計結餘數量 = 累計結餘數量 - dt_OuStkD.Rows[k]["異動數量"].ToString().ToDecimal();
                        DataRow dr_ = dt_DataGrdiViewSource.NewRow();
                        dr_["異動日期"] = dt_OuStkD.Rows[k]["異動日期"].ToString();
                        dr_["異動單號"] = dt_OuStkD.Rows[k]["異動單號"].ToString();
                        dr_["單據類別"] = dt_OuStkD.Rows[k]["單據類別"].ToString();
                        dr_["異動數量"] = dt_OuStkD.Rows[k]["異動數量"].ToString().ToDecimal();
                        dr_["結餘數量"] = 累計結餘數量;
                        dr_["倉庫名稱"] = dt_OuStkD.Rows[k]["倉庫名稱"].ToString();
                        dr_["成本"] = "0";
                        dt_DataGrdiViewSource.Rows.Add(dr_);
                    }
                    #endregion
                    #region  過濾顯示之資料(condition:異動日期)
                    if (indate1 > 0 && indate2 > 0)
                    {
                        for (int l = dt_DataGrdiViewSource.Rows.Count - 1; l >= 0; l--)
                        {
                            var data_ = dt_DataGrdiViewSource.Rows[l]["異動日期"].ToString().Split(new char[1] { '/' });
                            int date = int.Parse(data_[0] + data_[1] + data_[2]);
                            if (!(indate1 <= date && date <= indate2))////////////////
                                dt_DataGrdiViewSource.Rows.RemoveAt(l);
                        }
                    }
                    else if (indate1 > 0)
                        for (int l = dt_DataGrdiViewSource.Rows.Count - 1; l >= 0; l--)
                        {
                            var data_ = dt_DataGrdiViewSource.Rows[l]["異動日期"].ToString().Split(new char[1] { '/' });
                            int date = int.Parse(data_[0] + data_[1] + data_[2]);
                            if (indate1 > date)
                                dt_DataGrdiViewSource.Rows.RemoveAt(l);
                        }
                    else if (indate2 > 0)
                        for (int l = dt_AllDtad.Rows.Count - 1; l >= 0; l--)
                        {
                            var data_ = dt_AllDtad.Rows[l]["異動日期"].ToString().Split(new char[1] { '/' });
                            int date = int.Parse(data_[0] + data_[1] + data_[2]);
                            if (indate2 < date)
                                dt_AllDtad.Rows.RemoveAt(l);
                        }
                    #endregion
                }
            }
        }

        private void LoadAlldata(DataTable dt_AllDtad)
        {
            #region new schema
            //dataGridViewT1.DataSource = dt_AllDtad;
            if (dt_AllDtad.Columns.Count == 0)
            {
                DataColumn 產品編號 = new DataColumn("產品編號", typeof(string));
                DataColumn 客戶簡稱 = new DataColumn("客戶簡稱", typeof(string));
                DataColumn 客戶編號 = new DataColumn("客戶編號", typeof(string));
                DataColumn 產品規格 = new DataColumn("產品規格", typeof(string));
                DataColumn 異動日期 = new DataColumn("異動日期", typeof(string));
                DataColumn 異動單號 = new DataColumn("異動單號", typeof(string));
                DataColumn 單據類別 = new DataColumn("單據類別", typeof(string));
                DataColumn 異動數量 = new DataColumn("異動數量", typeof(decimal));
                DataColumn 結餘數量 = new DataColumn("結餘數量", typeof(decimal));
                DataColumn 倉庫名稱 = new DataColumn("倉庫名稱", typeof(string));
                DataColumn 成本 = new DataColumn("成本", typeof(string));
                dt_AllDtad.Columns.Add(產品編號);
                dt_AllDtad.Columns.Add(客戶簡稱);
                dt_AllDtad.Columns.Add(客戶編號);
                dt_AllDtad.Columns.Add(產品規格);
                dt_AllDtad.Columns.Add(異動日期);
                dt_AllDtad.Columns.Add(異動單號);
                dt_AllDtad.Columns.Add(單據類別);
                dt_AllDtad.Columns.Add(異動數量);
                dt_AllDtad.Columns.Add(結餘數量);
                dt_AllDtad.Columns.Add(倉庫名稱);
                dt_AllDtad.Columns.Add(成本);
            }
            #endregion
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string 客戶編號_ = dt.Rows[j]["cuno"].ToString();
                string 客戶簡稱_ = dt.Rows[j]["cuname1"].ToString();
                string 產品編號_ = dt.Rows[j]["itno"].ToString();
                string 產品規格_ = dt.Rows[j]["itname"].ToString();
                #region fill dt_AllDtad
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt_InStkD = new DataTable();//寄入
                    DataTable dt_OuStkD = new DataTable();//領出
                    #region 載入寄庫表
                    //if(stno!="")
                    //    cmd.CommandText = @"select indate as 異動日期,inno as 異動單號,單據類別='寄庫+',inqty as 異動數量,stname as 倉庫名稱,cuno,itno,indate,inno from InStkD where cuno =@cuno and itno =@itno and stno = @stno order by inno,itno,cuno ";
                    //else
                    cmd.CommandText = @"select "+indate異動日期+",inno as 異動單號,單據類別='寄庫+',inqty as 異動數量,stname as 倉庫名稱,cuno,itno,indate,inno from InStkD where cuno =@cuno and itno =@itno order by inno,itno,cuno ";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("cuno", dt.Rows[j]["cuno"].ToString());
                    cmd.Parameters.AddWithValue("itno", dt.Rows[j]["itno"].ToString());
                    cmd.Parameters.AddWithValue("stno", stno);
                    da.Fill(dt_InStkD);
                    #endregion
                    decimal 累計結餘數量 = 0m;
                    for (int i = 0; i < dt_InStkD.Rows.Count; i++)
                    {
                        累計結餘數量 += dt_InStkD.Rows[i]["異動數量"].ToString().ToDecimal();
                        #region 將寄庫填入  dt_AllDtad
                        DataRow dr = dt_AllDtad.NewRow();

                        dr["客戶編號"] = 客戶編號_;
                        dr["客戶簡稱"] = 客戶簡稱_;
                        dr["產品編號"] = 產品編號_;
                        dr["產品規格"] = 產品規格_;
                        dr["異動日期"] = dt_InStkD.Rows[i]["異動日期"].ToString();
                        dr["異動單號"] = dt_InStkD.Rows[i]["異動單號"].ToString();
                        dr["單據類別"] = dt_InStkD.Rows[i]["單據類別"].ToString();
                        dr["異動數量"] = dt_InStkD.Rows[i]["異動數量"].ToString().ToDecimal();
                        dr["結餘數量"] = 累計結餘數量;
                        dr["倉庫名稱"] = dt_InStkD.Rows[i]["倉庫名稱"].ToString();
                        dr["成本"] = "0";
                        dt_OuStkD.Clear();
                        dt_AllDtad.Rows.Add(dr);
                        #endregion
                        #region 避免重複_同銷貨單號、寄貨單號、產品、客戶重複撈，只用一筆撈。
                        if (i < dt_InStkD.Rows.Count - 1)
                        {
                            if (dt_InStkD.Rows[i]["cuno"].ToString() == dt_InStkD.Rows[i + 1]["cuno"].ToString() &&
                               dt_InStkD.Rows[i]["itno"].ToString() == dt_InStkD.Rows[i + 1]["itno"].ToString() &&
                               dt_InStkD.Rows[i]["inno"].ToString() == dt_InStkD.Rows[i + 1]["inno"].ToString())
                                continue;
                        }
                        #endregion
                        #region 載入領庫表
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("cuno", dt_InStkD.Rows[i]["cuno"].ToString());
                        cmd.Parameters.AddWithValue("itno", dt_InStkD.Rows[i]["itno"].ToString());
                        cmd.Parameters.AddWithValue("inno", dt_InStkD.Rows[i]["inno"].ToString());
                        cmd.Parameters.AddWithValue("stno", stno);
                        cmd.CommandText =
    @"select "+oudate異動日期+@",ouno as 異動單號,單據類別='領庫-',ouqty as 異動數量,結餘數量='',stname as 倉庫名稱
    from OuStkD,cust
    where OuStkD.cuno = cust.cuno 
          and OuStkD.cuno = @cuno
	      and OuStkD.itno = @itno
	      and OuStkD.inno = @inno";
                        if(stno!="")cmd.CommandText += " and OuStkD.stno = @stno";
                        da.Fill(dt_OuStkD);
                        #endregion
                        #region 將領庫填入  dt_AllDtad
                        for (int k = 0; k < dt_OuStkD.Rows.Count; k++)
                        {
                            累計結餘數量 = 累計結餘數量 - dt_OuStkD.Rows[k]["異動數量"].ToString().ToDecimal();
                            DataRow dr_ = dt_AllDtad.NewRow();
                            dr_["客戶編號"] = 客戶編號_;
                            dr_["客戶簡稱"] = 客戶簡稱_;
                            dr_["產品編號"] = 產品編號_;
                            dr_["產品規格"] = 產品規格_;
                            dr_["異動日期"] = dt_OuStkD.Rows[k]["異動日期"].ToString();
                            dr_["異動單號"] = dt_OuStkD.Rows[k]["異動單號"].ToString();
                            dr_["單據類別"] = dt_OuStkD.Rows[k]["單據類別"].ToString();
                            dr_["異動數量"] = dt_OuStkD.Rows[k]["異動數量"].ToString().ToDecimal();
                            dr_["結餘數量"] = 累計結餘數量;
                            dr_["倉庫名稱"] = dt_OuStkD.Rows[k]["倉庫名稱"].ToString();
                            dr_["成本"] = "0";
                            dt_AllDtad.Rows.Add(dr_);
                        }
                        #endregion
                        #region  過濾顯示之資料(condition:異動日期)
                        if (indate1 > 0 && indate2 > 0)
                        {
                            for (int l = dt_AllDtad.Rows.Count - 1; l >= 0; l--)
                            {
                                var data_ = dt_AllDtad.Rows[l]["異動日期"].ToString().Split(new char[1] { '/' });
                                int data = int.Parse(data_[0] + data_[1] + data_[2]);
                                if (!(indate1 <= data && data <= indate2))////////////////
                                    dt_AllDtad.Rows.RemoveAt(l);
                            }
                        }
                        else if (indate1 > 0)
                            for (int l = dt_AllDtad.Rows.Count - 1; l >= 0; l--)
                            {
                                var data_ = dt_AllDtad.Rows[l]["異動日期"].ToString().Split(new char[1] { '/' });
                                int data = int.Parse(data_[0] + data_[1] + data_[2]);
                                if (indate1 > data)
                                    dt_AllDtad.Rows.RemoveAt(l);
                            }
                        else if (indate2 > 0)
                            for (int l = dt_AllDtad.Rows.Count - 1; l >= 0; l--)
                            {
                                var data_ = dt_AllDtad.Rows[l]["異動日期"].ToString().Split(new char[1] { '/' });
                                int data = int.Parse(data_[0] + data_[1] + data_[2]);
                                if (indate2 < data)
                                    dt_AllDtad.Rows.RemoveAt(l);
                            }
                        #endregion
                    }
                }
                #endregion
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Task_.Wait();
            OutReport(RptMode.Print);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Task_.Wait();
            OutReport(RptMode.PreView);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            Task_.Wait();
            OutReport(RptMode.Word);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Task_.Wait();
            OutReport(RptMode.Excel);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void OutReport(RptMode mode) 
        {
            var path="";
            RPT rp =new RPT();
            if (radioT1.Checked == true)
            {
                path = Common.reportaddress + @"Report\寄庫領庫異動簡要表.rpt";
            }
            else if (radioT2.Checked == true)
            {
                path = Common.reportaddress + @"Report\寄庫領庫異動自定一.rpt";
            }
            rp = new RPT(dt_AllDtad, path);
            var 製表日期 = "製表日期:" + Date.GetToday();
            rp.lobj.Add(new string[] { "DateCreated", 製表日期 });
            if      (mode == RptMode.Print)   rp.Print();
            else if (mode == RptMode.PreView) rp.PreView();
            else if (mode == RptMode.Excel)   rp.Excel();
            else if (mode == RptMode.Word)    rp.Word();

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var keyValue = keyData.ToString();

            if (keyData.ToString().StartsWith("Z") && keyData.ToString().EndsWith("Shift"))
            {
                dt_AllDtad.Excel匯出並開啟(this.Text);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 

    }
}
