using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Threading.Tasks;


namespace S_61.subMenuFm_1
{
    public partial class 訂單轉採購_修改 : Formbase
    {
        public 訂單轉採購_修改()
        {
            InitializeComponent();

            this.訂單總額.DefaultCellStyle.Format = "f" + Common.MST;
            this.本幣總額.DefaultCellStyle.Format = "f" + Common.MST;

            this.訂單總額.Visible = this.本幣總額.Visible = Common.User_SalePrice;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }


        //選擇訂單
        public DataTable ordertable = new DataTable();
        //明細暫存
        DataTable DtTemp = new DataTable();
        //bom暫存
        DataTable DtBomTemp = new DataTable();
        //傳入瀏覽的dt
        DataTable DtM = new DataTable();

        DataTable 現有庫存_安全存量_採購未交量_已採購訂單dt = new DataTable();

        private void FrmOrderToFord_Load(object sender, EventArgs e)
        {
            ck1.Checked = ck3.Checked = radioT1.Checked = true;
            refreshdata();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void allok_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
               i.Cells["點選"].Value = "V";
            }
        }

        private void allcancel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewT1.Rows)
            {
                i.Cells["點選"].Value = "";
            }
        }

        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dataGridViewT1.Rows.Count - 1) return;
            if (dataGridViewT1.CurrentCell.OwningColumn.Name != "點選") return;

            if (dataGridViewT1["點選", e.RowIndex].Value.ToString().Trim() == "V")
                dataGridViewT1["點選", e.RowIndex].Value = "";
            else
                dataGridViewT1["點選", e.RowIndex].Value = "V";
            dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {

            //明細暫存
            DtTemp.Clear();
            //bom暫存
            DtBomTemp.Clear();
            //傳入瀏覽的dt
            DtM.Clear();

            var p = ordertable.AsEnumerable().Where(r => r["點選"].ToString().Trim() == "V");
            
            if (p.Count() > 0)
            {
                #region 初始table
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString+";Connect Timeout=120"))
                {
                    cn.Open();
                    string sql;
                    SqlDataAdapter dd;
                    //DtM 傳入之Table
                    sql = "select 品名規格='',單位='',現有庫存量=0.0,訂單需求量=0.0,採購未交量=0.0,已採購訂單=0.0,入庫數量=0.0,"
                        +"不足數量=0.0,安全存量=0.0,採購建議數量=0.0,採購數量=0.0,廠商編號='',廠商簡稱='' ,產品編號='' from instk where 1=0";
                    dd = new SqlDataAdapter(sql, cn);
                    dd.Fill(DtM);
                    //orderbom
                    //sql="select 訂單編號=orno, 結構識別碼=bomid, 結構對應記錄碼=bomrec, 產品編號=itno, 品名規格=itname,"
                    //    + "單位=itunit,標準用量=itqty,母件比率=itpareprs,包裝數量=itpkgqty,記錄編號=itrec from orderbom";
                    //dd = new SqlDataAdapter(sql, cn);
                    //dd.Fill(DtBom);
                    sql = @"select * from
(
SELECT 現有庫存與安全存量表.產品編號,現有庫存與安全存量表.sttrait1現有庫存,現有庫存與安全存量表.sttrait2現有庫存,現有庫存與安全存量表.sttrait3現有庫存,現有庫存與安全存量表.sttrait4現有庫存,現有庫存與安全存量表.Total現有庫存,採購未交量表.採購未交量,現有庫存與安全存量表.安全存量 FROM
(
select 產品編號=itno,安全存量=itsafeqty
,(select sum(itqty) from stock where itno=item.itno and sttrait='1') as sttrait1現有庫存
,(select sum(itqty) from stock where itno=item.itno and sttrait='2') as sttrait2現有庫存
,(select sum(itqty) from stock where itno=item.itno and sttrait='3') as sttrait3現有庫存
,(select sum(itqty) from stock where itno=item.itno and sttrait='4') as sttrait4現有庫存
,(select sum(itqty) from stock where itno=item.itno ) as Total現有庫存
from item
) 現有庫存與安全存量表
LEFT JOIN 
(
select 產品編號 = fordd_.itno 
,(select sum(qtyNotIn * itpkgqty) from fordd where itno = fordd_.itno group by itno) as 採購未交量
from (select distinct(itno) from fordd) fordd_
) 採購未交量表
ON 現有庫存與安全存量表.產品編號 = 採購未交量表.產品編號
) a

left join 

(
select 產品編號,sum(已採購訂單) as 已採購訂單  from(
select * from
(
select 訂單明細.訂單編號,訂單明細.產品編號,(訂單明細.包裝數量*訂單明細.訂單未交) as 已採購訂單 from 
(select 訂單編號=orno from [order] where 0=0 and ortrnflag='1' and oroverflag='0')
 已轉銷貨之訂單編號
inner join
(select  訂單編號=orno,產品編號=itno,包裝數量=itpkgqty,存量預估=stkqtyflag,結構識別碼=bomid ,訂單未交=qtynotout from orderd where stkqtyflag='2')
 訂單明細
on 已轉銷貨之訂單編號.訂單編號 = 訂單明細.訂單編號
) 未拆封
UNION ALL
(
select 訂單編號=orno, 產品編號=itno,已採購訂單=明細數量*(itqty/(itpkgqty*itpareprs))
        from 
orderbom 
inner join
	(
	select 結構識別碼,產品編號,訂單未交,包裝數量,明細數量=訂單未交*包裝數量 from 
	(select 訂單編號=orno from [order] where 0=0 and ortrnflag='1' and oroverflag='0')
	 已轉銷貨之訂單編號
	inner join
	(select  訂單編號=orno,產品編號=itno,包裝數量=itpkgqty,存量預估=stkqtyflag,結構識別碼=bomid ,訂單未交=qtynotout from orderd where stkqtyflag='1')
	 訂單明細
	on 已轉銷貨之訂單編號.訂單編號 = 訂單明細.訂單編號
	) 已轉採購之明細
on 已轉採購之明細.結構識別碼 = orderbom.bomid
) 
) 已拆封與未拆封之合
group by 已拆封與未拆封之合.產品編號
) b

on a.產品編號 = b.產品編號";

                    dd = new SqlDataAdapter(sql, cn);
                    dd.Fill(現有庫存_安全存量_採購未交量_已採購訂單dt);
                }
                #endregion
                #region 訂單需求量
                for (int i = 0; i < p.Count(); i++)// p = V勾選之Row
                {
                    string orno=p.ElementAt(i)["訂單編號"].ToString().Trim();
                    Get訂單明細(orno, DtTemp);
                    foreach (DataRow row in DtTemp.Rows)//DtTemp  => 訂單明細，dd=>該筆Order之orno取得Orderd之Rows
                    {
                        if (row["存量預估"].ToString().Trim() =="2")//stkqtyflag = 2 不展開
                        {
                            var m = DtM.AsEnumerable().ToList().Find(r => r["產品編號"].ToString().Trim() == row["產品編號"].ToString().Trim());//此訂單明細之產品
                            if (m == null)
                            {
                                DataRow dr = DtM.NewRow();
                                dr["品名規格"]   = row["品名規格"].ToString().Trim();
                                dr["現有庫存量"] = 0;
                                dr["訂單需求量"] = (row["訂單未交"].ToDecimal() * row["包裝數量"].ToDecimal());
                                dr["採購未交量"] = 0;
                                dr["已採購訂單"] = 0;
                                dr["入庫數量"] = 0;
                                dr["不足數量"] = 0;
                                dr["安全存量"] = 0;
                                dr["採購建議數量"] = 0;
                                dr["採購數量"] = 0;                               
                                dr["產品編號"] = row["產品編號"].ToString().Trim();

                                DtM.Rows.Add(dr);
                                DtM.AcceptChanges();
                            }
                            else
                            {
                                m["訂單需求量"] = m["訂單需求量"].ToDecimal()  +(row["訂單未交"].ToDecimal() * row["包裝數量"].ToDecimal());
                            }
                        }
                        else//stkqtyflag = 1 展開
                        {
                           // var dbom = DtBom.AsEnumerable().Where(r => r["結構識別碼"].ToString().Trim() == row["結構識別碼"].ToString().Trim());//bomid(結構識別碼)
                            Get訂單Bom(row["結構識別碼"].ToString().Trim(), DtBomTemp);
                            if (DtBomTemp.Rows.Count > 0)
                            {
                                foreach (DataRow bomrow in DtBomTemp.Rows)
                                {
                                    var m = DtM.AsEnumerable().ToList().Find(r => r["產品編號"].ToString().Trim() == bomrow["產品編號"].ToString().Trim());
                                    if (m == null)
                                    {
                                        DataRow dr = DtM.NewRow();
                                        dr["品名規格"] = bomrow["品名規格"].ToString().Trim();
                                        dr["現有庫存量"] = 0;
                                        decimal dcount = (row["訂單未交"].ToDecimal() * row["包裝數量"].ToDecimal());
                                        dr["訂單需求量"] = dcount * (bomrow["標準用量"].ToDecimal() / bomrow["母件比率"].ToDecimal() * bomrow["包裝數量"].ToDecimal());
                                        dr["採購未交量"] = 0;
                                        dr["已採購訂單"] = 0;
                                        dr["入庫數量"] = 0;
                                        dr["不足數量"] = 0;
                                        dr["安全存量"] = 0;
                                        dr["採購建議數量"] = 0;
                                        dr["採購數量"] = 0;
                                        dr["產品編號"] = bomrow["產品編號"].ToString().Trim();

                                        DtM.Rows.Add(dr);
                                        DtM.AcceptChanges();
                                    }
                                    else
                                    {
                                        decimal dcount = (row["訂單未交"].ToDecimal() * row["包裝數量"].ToDecimal());
                                        m["訂單需求量"] = m["訂單需求量"].ToDecimal() + (dcount * bomrow["標準用量"].ToDecimal() / bomrow["母件比率"].ToDecimal() * bomrow["包裝數量"].ToDecimal());
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
                #region 現有庫存、安全庫存、採購未交、已採購訂單
                foreach (DataRow dr in DtM.Rows)
                {
                    int index = GetIndex現有庫存_安全存量_採購未交量_已採購訂單dt(dr["產品編號"].ToString().Trim());
                    if (index == -1) continue;  //等待清除
                    dr["安全存量"] = 現有庫存_安全存量_採購未交量_已採購訂單dt.Rows[index]["安全存量"].ToDecimal();
                    decimal num = 0;
                    if (ck1.Checked)
                    {
                        num = num + 現有庫存_安全存量_採購未交量_已採購訂單dt.Rows[index]["sttrait1現有庫存"].ToDecimal();
                    }
                    if (ck2.Checked)
                    {
                        num = num + 現有庫存_安全存量_採購未交量_已採購訂單dt.Rows[index]["sttrait2現有庫存"].ToDecimal();
                    }
                    if (ck3.Checked)
                    {
                        num = num + 現有庫存_安全存量_採購未交量_已採購訂單dt.Rows[index]["sttrait3現有庫存"].ToDecimal();
                    }
                    if (ck4.Checked)
                    {
                        num = num + 現有庫存_安全存量_採購未交量_已採購訂單dt.Rows[index]["sttrait4現有庫存"].ToDecimal();
                    }
                    dr["現有庫存量"] = num;

                    dr["採購未交量"] = 現有庫存_安全存量_採購未交量_已採購訂單dt.Rows[index]["採購未交量"].ToDecimal();
                    dr["已採購訂單"] = 現有庫存_安全存量_採購未交量_已採購訂單dt.Rows[index]["已採購訂單"].ToDecimal();
                }
                #endregion
                foreach (DataRow dr in DtM.Rows)
                {
                    dr["不足數量"] = dr["訂單需求量"].ToDecimal() + dr["已採購訂單"].ToDecimal() - dr["現有庫存量"].ToDecimal() - dr["採購未交量"].ToDecimal();
                    if (dr["不足數量"].ToDecimal() < 0) dr["不足數量"] = 0;
                    dr["採購建議數量"] = dr["訂單需求量"].ToDecimal() + dr["已採購訂單"].ToDecimal() + dr["安全存量"].ToDecimal() - dr["現有庫存量"].ToDecimal() - dr["採購未交量"].ToDecimal();
                    if (dr["採購建議數量"].ToDecimal() < 0) dr["採購建議數量"] = 0;
                    dr["採購數量"] = dr["採購建議數量"].ToDecimal();
                    fill廠商編號_廠商簡稱(dr);
                }
                using (FrmOrderToFordb frm = new FrmOrderToFordb())
                { 
                    if (radioT1.Checked)//不顯示
                    {
                        var zero = DtM.AsEnumerable().Where(r=>r["採購建議數量"].ToDecimal()!=0);
                        if (zero.Count()<=0)
                        {
                            MessageBox.Show("查詢不到資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        frm.table = zero.CopyToDataTable();
                    }
                    else//顯示
                    {
                        frm.table = DtM.Copy();
                    }
                    frm.ordertable = p.CopyToDataTable();
                    frm.ShowDialog();

                    switch (frm.DialogResult)
                    {
                        case DialogResult.Cancel:
                            refreshdata();
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("請點選訂單", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
        }

        private void fill廠商編號_廠商簡稱(DataRow dr)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                var itno = dr["產品編號"].ToString().Trim();
                cmd.Parameters.AddWithValue("itno", dr["產品編號"].ToString().Trim());
                cmd.CommandText = @"select  產品編號=IT.itno ,最小單位=IT.itunit ,廠商編號=IT.fano,廠商簡稱=FA.faname1 from item IT left join fact FA on IT.fano=FA.fano where IT.itno = @itno";
                SqlDataReader Reader = cmd.ExecuteReader();
                Reader.Read();
                var 最小單位 = Reader["最小單位"].ToString().Trim();
                dr["單位"] = Reader["最小單位"].ToString().Trim();
                if (Reader["廠商編號"].ToString().Trim().Length>0) 
                {
                    dr["廠商編號"] = Reader["廠商編號"].ToString().Trim();
                    dr["廠商簡稱"] = Reader["廠商簡稱"].ToString().Trim(); 
                }
                else
                {
                    Reader.Close();
                    cmd.CommandText = @"select top 1 進貨日期=bsdate,產品編號=BS.itno,廠商編號=ISNULL(BS.fano,''),廠商簡稱=ISNULL(FA.faname1,'') from bshopd BS left join fact FA on BS.fano=FA.fano where itno = @itno order by bsdate desc";
                    Reader = cmd.ExecuteReader();
                    if (Reader.Read())
                    {
                        if (Reader.IsNotNull())
                        {
                            dr["廠商編號"] = Reader["廠商編號"].ToString().Trim();
                            dr["廠商簡稱"] = Reader["廠商簡稱"].ToString().Trim();
                        }
                    }
                }
            }
        }

        private int GetIndex現有庫存_安全存量_採購未交量_已採購訂單dt(string itno)
        {
            object lock_ = new object();
            int index = -1;
            Parallel.For(0, 現有庫存_安全存量_採購未交量_已採購訂單dt.Rows.Count, (i, loopState) =>
            {
                if (現有庫存_安全存量_採購未交量_已採購訂單dt.Rows[i]["產品編號"].ToString() == itno)
                {
                    lock (lock_)
                    {
                        index=i;
                    }
                }
            });
            return index;
        }

        private void Get訂單Bom(string bomid, DataTable dt)
        {
            dt.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("bomid", bomid);
                cmd.CommandText = @"select 訂單編號=orno, 結構識別碼=bomid, 結構對應記錄碼=bomrec, 產品編號=itno, 品名規格=itname,單位=itunit,標準用量=itqty,母件比率=itpareprs,包裝數量=itpkgqty,記錄編號=itrec from orderbom
	                                where bomid =@bomid";
                da.Fill(dt);
            }
        }

        private void Get訂單明細(string orno,DataTable dt)
        {
            dt.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("orno", orno);
                cmd.CommandText = @"select 訂單編號=orno,是否已轉採購=ortrnflag,產品編號=itno,品名規格=itname,單位=itunit,包裝數量=itpkgqty,數量=qty,訂單已交量=qtyout,已入庫數量=qtyin,存量預估=stkqtyflag,結構識別碼=bomid,結構對應記錄碼=bomrec ,訂單未交=qtynotout from orderd
                                    where orno =@orno";
                da.Fill(dt);
            }
        }
          
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    allok.Focus();
                    allok.PerformClick();
                    break;
                case Keys.F3:
                    allcancel.Focus();
                    allcancel.PerformClick();
                    break;
                case Keys.F9:
                    btnBrow.Focus();
                    btnBrow.PerformClick();
                    break;
                case Keys.F11:
                    btnExit.Focus();
                    btnExit.PerformClick();
                    break;
            } 
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void refreshdata()
        {
            try
            {
                ordertable.Clear();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    //ortrnflag 是否已轉採購單   oroverflag  =??  //1
                    string sql = "select 點選='V',訂單編號=OD.orno,訂單日期=OD.ordate,客戶編號=OD.cuno,客戶簡稱=OD.cuname1,"
                        + "業務編號=OD.emno,幣別=OD.xa1name,訂單總額=OD.totmny,本幣總額=OD.totmnyb,備註=OD.ormemo ,OD.* from [order] OD"
                        + " where ortrnflag=0 and oroverflag=0";
                    SqlDataAdapter dd;
                    dd = new SqlDataAdapter(sql, cn);
                    dd.Fill(ordertable);
                }
                dataGridViewT1.DataSource = ordertable;
             
                this.訂單總額.DefaultCellStyle.Format = "f" + Common.M;
                this.本幣總額.DefaultCellStyle.Format = "f" + Common.M;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
    }
}
