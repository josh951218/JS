using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using S_61;

namespace JBS.S6
{
    public partial class FrmCompare : Formbase
    {
        DataTable table = new DataTable();
        DataRow dr;
      
        public FrmCompare()
        {
            InitializeComponent();
        }

        class dtDStNo
        {
            public string dtD { get; set; }
            public string dtBom { get; set; }
            public string StNo { get; set; }
        }
        private void FrmCompare_Load(object sender, EventArgs e)
        {
            if (Common.Sys_UsingBatch == 2) btnBatchStock.Visible = true;
            else btnBatchStock.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        void CkeckDraw(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (tb.Rows[i]["stnoo"].ToString() != "" && tb.Rows[i]["stnoi"].ToString() != "")
                {
                    dr = table.NewRow();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                    dr["stno"] = tb.Rows[i]["stnoo"].ToString();
                    table.Rows.Add(dr);

                    dr = table.NewRow();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["數量"] = tb.Rows[i]["數量"].ToString();
                    dr["stno"] = tb.Rows[i]["stnoi"].ToString();
                    table.Rows.Add(dr);
                    table.AcceptChanges();
                }
                else
                {
                    dr = table.NewRow();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                    dr["stno"] = tb.Rows[i]["stnoo"].ToString();
                    table.Rows.Add(dr);
                    table.AcceptChanges();
                }
            }
        }
        void CkeckGarner(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (tb.Rows[i]["stnoo"].ToString() != "" && tb.Rows[i]["stnoi"].ToString() != "")
                {
                    dr = table.NewRow();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["數量"] = tb.Rows[i]["數量"].ToString();
                    dr["stno"] = tb.Rows[i]["stnoi"].ToString();
                    table.Rows.Add(dr);

                    if (tb.Rows[i]["ittrait"].ToString() == "3")
                    {
                        dr = table.NewRow();
                        dr["itno"] = tb.Rows[i]["itno"].ToString();
                        dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                        dr["stno"] = tb.Rows[i]["stnoo"].ToString();
                        table.Rows.Add(dr);
                    }
                }
                else
                {
                    dr = table.NewRow();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["數量"] = tb.Rows[i]["數量"].ToString();
                    dr["stno"] = tb.Rows[i]["stnoi"].ToString();
                    table.Rows.Add(dr);
                }
                table.AcceptChanges();
            }
        }
        void CkeckGarnbom(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (tb.Rows[i]["stnoo"].ToString() != "")
                {
                    dr = table.NewRow();
                    dr["itno"] = tb.Rows[i]["itno"].ToString();
                    dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                    dr["stno"] = tb.Rows[i]["stnoo"].ToString();
                    table.Rows.Add(dr);
                    table.AcceptChanges();
                }
            }
        }
        void CkeckAllot(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                dr["stno"] = tb.Rows[i]["stnoo"].ToString();
                table.Rows.Add(dr);

                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = tb.Rows[i]["數量"].ToString();
                dr["stno"] = tb.Rows[i]["stnoi"].ToString();
                table.Rows.Add(dr);
                table.AcceptChanges();
            }
        }
        void CkeckBorr(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                dr["stno"] = tb.Rows[i]["stnoo"].ToString();
                table.Rows.Add(dr);

                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = tb.Rows[i]["數量"].ToString();
                dr["stno"] = tb.Rows[i]["stno"].ToString();
                table.Rows.Add(dr);
                table.AcceptChanges();
            }
        }
        void CkeckRBorr(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = tb.Rows[i]["數量"].ToString();
                dr["stno"] = tb.Rows[i]["stnoo"].ToString();
                table.Rows.Add(dr);

                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                dr["stno"] = tb.Rows[i]["stno"].ToString();
                table.Rows.Add(dr);
                table.AcceptChanges();
            }
        }
        void CkeckLend(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = tb.Rows[i]["數量"].ToString();
                dr["stno"] = tb.Rows[i]["stnoi"].ToString();
                table.Rows.Add(dr);

                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                dr["stno"] = tb.Rows[i]["stno"].ToString();
                table.Rows.Add(dr);
                table.AcceptChanges();
            }
        }
        void CkeckRLend(DataTable tb)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = (tb.Rows[i]["數量"].ToDecimal() * (-1)).ToString();
                dr["stno"] = tb.Rows[i]["stnoi"].ToString();
                table.Rows.Add(dr);

                dr = table.NewRow();
                dr["itno"] = tb.Rows[i]["itno"].ToString();
                dr["數量"] = tb.Rows[i]["數量"].ToString();
                dr["stno"] = tb.Rows[i]["stno"].ToString();
                table.Rows.Add(dr);
                table.AcceptChanges();
            }
        }

        int GetConnectionningCount()
        {
            DataTable tCount = new DataTable();
            DataTable tTemp = new DataTable();

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = " SELECT TOP(1)database_id cndbid FROM master.sys.databases where name = '" + Common.DatabaseName + "'";
                var cndbid = cmd.ExecuteScalar().ToInteger();


                cmd.CommandText = " Select hostprocess 主機,net_address 位址 from master..sysprocesses where program_name = 'JBS' and dbid=" + cndbid;
                da.Fill(tCount);

                tTemp = tCount.Clone();
                for (int i = 0; i < tCount.Rows.Count; i++)
                {
                    var row = tTemp.AsEnumerable().Where(r => r["主機"].ToString() == tCount.Rows[i]["主機"].ToString() && r["位址"].ToString() == tCount.Rows[i]["位址"].ToString());
                    if (row.Count() == 0)
                        tTemp.ImportRow(tCount.Rows[i]);
                }
                tTemp.AcceptChanges();
            }

            var count = tTemp.Rows.Count;
            tTemp.Clear();
            tCount.Clear();

            return count;
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            #region 取得目前工作站數 若資料庫連線數>1 不可執行
            var ct = GetConnectionningCount();
            if (ct > 1)
            {
                MessageBox.Show("目前有 " + ct + " 台連線數，請確定其他工作站是否關閉!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            if (MessageBox.Show("請確定是否比對", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Cancel)
                return;

            JBS.JS.Stock jStock = new JS.Stock(Common.Sys_StkYear1, Common.Sys_StkYear1 + 1);
            try
            {
                PanelT1.Enabled = false;

                jStock.ReSetAllStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                PanelT1.Enabled = true;
            }
        }

        private void btnMny_Click(object sender, EventArgs e)
        {
            #region 取得目前工作站數 若資料庫連線數>1 不可執行
            var count = GetConnectionningCount();
            if (count > 1)
            {
                MessageBox.Show("目前有 " + count + " 台連線數，請確定其他工作站是否關閉!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            if (MessageBox.Show("請確定是否比對", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Cancel)
                return;

            SqlTransaction tn = null;
            SqlTransaction tn1 = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    PanelT1.Enabled = false;
                    cn.Open();

                    #region 客戶重新計算
                    try
                    {
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = @"
                        UPDATE cust
                        SET    cust.cusparercv =  cufirreceiv, cust.cureceiv =   cust.cufirreceiv,  cust.cuadvamt = cust.cufiradvamt";
                        //          期初帳款餘額  期初帳款金額      現有帳款餘額      期初帳款金額       現有預收餘額    期初預收金額
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                        UPDATE cust
                        SET    cust.cuadvamt = cust.cufiradvamt+A.預收 
                        FROM   cust 
                        INNER JOIN (Select cuno,SUM(addprvacc-getprvacc)預收 from receiv group by cuno)A ON cust.cuno = A.cuno";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                        UPDATE cust
                        SET    cust.cusparercv = cust.cufirreceiv-A.期初已沖款
                        FROM   cust 
                        INNER JOIN (Select cuno,SUM(receivd.discount+receivd.reverse)期初已沖款 from receivd where bracket='期初' group by cuno)A ON cust.cuno = A.cuno";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                        UPDATE cust
                        SET    cust.cureceiv = cust.cusparercv+ISNULL(A.帳款,0)
                        FROM   cust 
                        LEFT JOIN  
                        (
	                        Select payerno,SUM(f)帳款 from 
	                        (
		                        Select payerno,SUM(acctmny)f from sale group by payerno
		                        union all
		                        Select payerno,((-1)*SUM(acctmny))f from rsale group by payerno
	                        )B group by B.payerno
                        )A 
                        ON cust.cuno = A.payerno";
                        cmd.ExecuteNonQuery();
                        //
                        tn.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        if (tn != null) tn.Rollback();
                    }
                    #endregion

                    #region 廠商重新計算
                    try
                    {
                        tn1 = cn.BeginTransaction();
                        cmd.Transaction = tn1;

                        cmd.CommandText = @"
                        UPDATE fact
                        SET    fact.FaSparePay = FaFirPayabl,fact.FaPayable = fact.FaFirPayabl,fact.FaPayAmt = fact.FaFirPayAmt";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                        UPDATE fact
                        SET    fact.FaPayAmt = fact.FaFirPayAmt+A.預收 
                        FROM   fact 
                        INNER JOIN (Select fano,SUM(addprvacc-getprvacc)預收 from payabl group by fano)A ON fact.fano = A.fano";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                        UPDATE fact
                        SET    fact.FaSparePay = fact.FaFirPayabl-A.期初已沖款
                        FROM   fact 
                        INNER JOIN (Select fano,SUM(payabld.discount+payabld.reverse)期初已沖款 from payabld where bracket='期初' group by fano)A ON fact.fano = A.fano";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                        UPDATE fact
                        SET    fact.FaPayable = fact.FaFirPayabl+A.帳款
                        FROM   fact 
                        INNER JOIN 
                        (
	                        Select fano,SUM(f)帳款 from 
	                        (
		                        Select fano,SUM(acctmny)f from bshop group by fano
		                        union all
		                        Select fano,((-1)*SUM(acctmny))f from rshop group by fano
	                        )B group by B.fano
                        )A 
                        ON fact.fano = A.fano";
                        cmd.ExecuteNonQuery();
                        //
                        tn1.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        if (tn1 != null) tn.Rollback();
                    }
                    #endregion

                    MessageBox.Show("資料比對完成!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    PanelT1.Enabled = true;
                }
            }
        }

        private void buttonSmallT1_Click(object sender, EventArgs e)
        {
            #region 取得目前工作站數 若資料庫連線數>1 不可執行
            var count = GetConnectionningCount();
            if (count > 1)
            {
                MessageBox.Show("目前有 " + count + " 台連線數，請確定其他工作站是否關閉!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            if (MessageBox.Show("請確定是否比對", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Cancel)
                return;

            PanelT1.Enabled = false;
            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cmd.CommandText = @"
                    update
                    lendd
                    set
                    qtynotout = (ISNULL(lendd.qty,0)*ISNULL(lendd.itpkgqty,0)) - rQtys
                    from
                    (
	                    select lenoid,SUM(ISNULL(qty,0)*ISNULL(itpkgqty,0))rQtys
	                    from rlendd 
	                    where len(rlendd.lenoid) > 0 and len(rlendd.lendno) > 0
	                    group by lenoid
                    )B
                    left join lendd on B.lenoid = lendd.bomid;

                    update 
                    lend
                    set lend.leoverflag = 0
                    from lend 
                    inner join 
                    (
	                    select lend.leno,C.flag from lend 
	                    left join(
	                    select leno,COUNT(*)flag
	                    from lendd where ISNULL(qtynotout,0)<> 0
	                    group by leno)C on lend.leno = C.leno
	
                    )B on lend.leno = B.leno
                    where B.flag > 0;
                    
                    update 
                    lend
                    set lend.leoverflag = 1
                    from lend 
                    inner join 
                    (
	                    select lend.leno,C.flag from lend 
	                    left join(
	                    select leno,COUNT(*)flag
	                    from lendd where ISNULL(qtynotout,0)<> 0
	                    group by leno)C on lend.leno = C.leno
	
                    )B on lend.leno = B.leno
                    where B.flag is null;";

                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.ExecuteNonQuery();
                    tn.Commit();

                    MessageBox.Show("資料比對完成!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    if (tn != null)
                        tn.Rollback();

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    PanelT1.Enabled = true;
                }
            }
        }

        private void btnBrowT3_Click(object sender, EventArgs e)
        {
            #region 取得目前工作站數 若資料庫連線數>1 不可執行
            var count = GetConnectionningCount();
            if (count > 1)
            {
                MessageBox.Show("目前有 " + count + " 台連線數，請確定其他工作站是否關閉!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            if (MessageBox.Show("請確定是否比對", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Cancel)
                return;

            PanelT1.Enabled = false;
            SqlTransaction tn = null;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cmd.CommandText = @"
                    Update
                    orderd
                    set 
                    qtyNotOut = ISNULL(qty,0)-B.tQty
                    from
                    orderd od
                    inner join
                    (
                        select orno,orid,tQty = SUM(ISNULL(qty,0))
                        from saled 
                        where LEN(orno)>0 and LEN(orid)>0
                        group by orno,orid
                    )B on od.orno = B.orno and od.bomid = B.orid;

                    update 
                    dbo.[order]
                    set dbo.[order].oroverflag = 0
                    from dbo.[order] 
                    inner join 
                    (
                        select dbo.[order].orno,C.flag from dbo.[order] 
                        left join(
                        select orno,COUNT(*)flag
                        from orderd where ISNULL(qtynotout,0) > 0
                        group by orno)C on dbo.[order].orno = C.orno

                    )B on dbo.[order].orno = B.orno
                    where B.flag > 0;

                    update 
                    dbo.[order]
                    set dbo.[order].oroverflag = 1
                    from dbo.[order] 
                    inner join 
                    (
                        select dbo.[order].orno,C.flag from dbo.[order] 
                        left join(
                        select orno,COUNT(*)flag
                        from orderd where ISNULL(qtynotout,0) > 0
                        group by orno)C on dbo.[order].orno = C.orno

                    )B on dbo.[order].orno = B.orno
                    where B.flag is null;";

                    cn.Open();
                    tn = cn.BeginTransaction();
                    cmd.Transaction = tn;

                    cmd.ExecuteNonQuery();
                    tn.Commit();

                    #region 重寫訂單入庫數量(補)
                    cmd.CommandText = @" update  orderd set  qtyin = 
                        (select sum(qty) from garnerd where garnerd.orno=orderd.orno and garnerd.orid=orderd.bomid),
                        qtyNotInStk = 
                        orderd.qty-(select sum(qty) from garnerd where garnerd.orno=orderd.orno and garnerd.orid=orderd.bomid)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = " SELECT * FROM orderd where qtyin is null and qtyNotInStk is null ";
                    DataTable orderdskt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(orderdskt);
                        cmd.Parameters.AddWithValue("qtyin", "");
                        cmd.Parameters.AddWithValue("qtyNotInStk", "");
                        cmd.Parameters.AddWithValue("orno", "");
                        cmd.Parameters.AddWithValue("bomid", "");
                        for (int i = 0; i < orderdskt.Rows.Count; i++)
                        {
                            cmd.Parameters["qtyNotInStk"].Value = orderdskt.Rows[i]["qty"];
                            cmd.Parameters["orno"].Value = orderdskt.Rows[i]["orno"];
                            cmd.Parameters["bomid"].Value = orderdskt.Rows[i]["bomid"];
                            cmd.Parameters["qtyin"].Value = 0;
                            cmd.CommandText = "update orderd set qtyin=(@qtyin) , qtyNotInStk=(@qtyNotInStk) where orno=@orno and bomid = @bomid";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion

                    MessageBox.Show("資料比對完成!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    if (tn != null)
                        tn.Rollback();

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    PanelT1.Enabled = true;
                }
            }
        }

        private void btnBarCode_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("請確定是否重置!?", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Cancel)
                return;

            try
            {
                PanelT1.Enabled = false;

                DataTable temp = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("itno", "");
                    cmd.Parameters.AddWithValue("bar1", "");
                    cmd.Parameters.AddWithValue("bar2", "");

                    cn.Open();
                    cmd.CommandText = " Select itno,itname from item ";
                    da.Fill(temp);

                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        var itno = temp.Rows[i]["itno"].ToString().Trim();
                        var itname = temp.Rows[i]["itname"].ToString().Trim();

                        var bar1 = itname.GetUTF8(15);
                        var bar2 = new string(itname.Skip(bar1.Length).ToArray());

                        cmd.Parameters["itno"].Value = itno;
                        cmd.Parameters["bar1"].Value = bar1;
                        cmd.Parameters["bar2"].Value = bar2;
                        cmd.CommandText = " Update item set ItBarName1= @bar1 ,ItBarName2= @bar2 where itno = @itno";
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("重置完成!");
            }
            finally
            {
                PanelT1.Enabled = true;
            }
        }

        private void btnTransDay_Click(object sender, EventArgs e)
        {
            try
            {
                PanelT1.Enabled = false;
                 
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();

                    cmd.CommandText = "Update item Set itsaldate ='',itsaldate1='',itbuydate='',itbuydate1=''";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                    Update item 
                    Set itsaldate = S.md, itsaldate1=S.md1
                    From item inner join ( 
						                    select A.itno,md,md1
						                    from(
							                    select itno,MAX(sadate)md from saled group by itno
						                    )A 
						                    left join (
									                    select itno,MAX(sadate1)md1 from saled group by itno
						                    )B on A.itno = B.itno
                    )S on item.itno = S.itno ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                    Update item 
                    Set itbuydate = S.md, itbuydate1=S.md1
                    From item inner join ( 
						                    select A.itno,md,md1
						                    from(
							                    select itno,MAX(bsdate)md from bshopd group by itno
						                    )A 
						                    left join (
									                    select itno,MAX(bsdate1)md1 from bshopd group by itno
						                    )B on A.itno = B.itno
                    )S on item.itno = S.itno ";
                    cmd.ExecuteNonQuery(); 
                }
                MessageBox.Show("檢查完成!");
            }
            finally
            {
                PanelT1.Enabled = true;
            }
        }

        private void buttonSmallT2_Click(object sender, EventArgs e)
        {
            try
            {
                PanelT1.Enabled = false;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open(); 

                    cmd.CommandText = @"
                        Update weborder Set orderstate = 0,sysorno=''
                        from weborder 
                        left join dbo.[order] on weborder.orno=dbo.[order].netno
                        where dbo.[order].netno is null ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                        Update weborder Set orderstate = 1,sysorno=dbo.[order].orno
                        from weborder 
                        left join dbo.[order] on weborder.orno=dbo.[order].netno
                        where len(dbo.[order].netno) > 0 ";
                    cmd.ExecuteNonQuery();
                     
                    cmd.CommandText = @"
                        Update weborder Set orderstate = 2
                        from saled 
                        inner join weborder on saled.netno=weborder.orno";
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("檢查完成!");
            }
            finally
            {
                PanelT1.Enabled = true;
            }
        }

        private void btnBatchStock_Click(object sender, EventArgs e)
        {
            //#region 取得目前工作站數 若資料庫連線數>1 不可執行
            //var ct = GetConnectionningCount();
            //if (ct > 1)
            //{
            //    MessageBox.Show("目前有 " + ct + " 台連線數，請確定其他工作站是否關閉!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //#endregion

            if (MessageBox.Show("請確定是否比對", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Cancel)
                return;

            try
            {
                PanelT1.Enabled = false;
                if (批次庫存Update() == true)
                    MessageBox.Show("比對完成!");
                else
                    MessageBox.Show("比對失敗!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                PanelT1.Enabled = true;
            }
        }




        public bool 批次庫存Update()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter ad = new SqlDataAdapter())
            {
                cn.Open();
                using (SqlTransaction tran = cn.BeginTransaction("Transaction"))
                {
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    try
                    {
                        //1.刪除delete BatchStock
                        cmd.CommandText = "delete BatchStock";
                        cmd.ExecuteNonQuery();
                        //2.刪除幽靈勾稽 [正常來說，是不會有幽靈勾稽的部分，如果有的話，就是程式有瑕疵]
                        #region str刪除幽靈勾稽
                        string str刪除幽靈勾稽 = @"
delete BatchProcess_Saled     from 
(
	SELECT a.bomid FROM  saled right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_Saled.Bno) 
	from BatchProcess_Saled ) a 
	 ON a.bomid = saled.bomid and a.itno = saled.itno  WHERE saled.bomid is null
 )b
 where BatchProcess_Saled.bomid = b.bomid

delete BatchProcess_rSaleD    from 
(
	SELECT a.bomid FROM  rSaleD right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_rSaleD.Bno) 
	from BatchProcess_rSaleD ) a 
	 ON a.bomid = rSaleD.bomid and a.itno = rSaleD.itno  WHERE rSaleD.bomid is null
 )b
 where BatchProcess_rSaleD.bomid = b.bomid

delete BatchProcess_Drawd     from 
(
	SELECT a.bomid FROM  Drawd right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_Drawd.Bno) 
	from BatchProcess_Drawd ) a 
	 ON a.bomid = Drawd.bomid and a.itno = Drawd.itno  WHERE Drawd.bomid is null
 )b
 where BatchProcess_Drawd.bomid = b.bomid

delete BatchProcess_Drawbom   from 
(
	SELECT a.bomid FROM  Drawbom right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_Drawbom.Bno) 
	from BatchProcess_Drawbom ) a 
	 ON a.bomid = Drawbom.bomid and a.itno = Drawbom.itno  WHERE Drawbom.bomid is null
 )b
 where BatchProcess_Drawbom.bomid = b.bomid

delete BatchProcess_Garnerd   from 
(
	SELECT a.bomid FROM  Garnerd right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_Garnerd.Bno) 
	from BatchProcess_Garnerd ) a 
	 ON a.bomid = Garnerd.bomid and a.itno = Garnerd.itno  WHERE Garnerd.bomid is null
 )b
 where BatchProcess_Garnerd.bomid = b.bomid

delete BatchProcess_GarnerBom from 
(
	SELECT a.bomid FROM  GarnBom right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_GarnerBom.Bno) 
	from BatchProcess_GarnerBom ) a 
	 ON a.bomid = GarnBom.bomid and a.itno = GarnBom.itno  WHERE GarnBom.bomid is null
 )b
 where BatchProcess_GarnerBom.bomid = b.bomid

delete BatchProcess_bShopd    from 
(
	SELECT a.bomid FROM  bShopd right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_bShopd.Bno) 
	from BatchProcess_bShopd ) a 
	 ON a.bomid = bShopd.bomid and a.itno = bShopd.itno  WHERE bShopd.bomid is null
 )b
 where BatchProcess_bShopd.bomid = b.bomid

delete BatchProcess_RShopd    from 
(
	SELECT a.bomid FROM  RShopd right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_RShopd.Bno) 
	from BatchProcess_RShopd ) a 
	 ON a.bomid = RShopd.bomid and a.itno = RShopd.itno  WHERE RShopd.bomid is null
 )b
where BatchProcess_RShopd.bomid = b.bomid

delete BatchProcess_SaleBom   from 
(
	SELECT a.bomid FROM  SaleBom right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_SaleBom.Bno) 
	from BatchProcess_SaleBom ) a 
	 ON a.bomid = SaleBom.bomid and a.itno = SaleBom.itno  WHERE SaleBom.bomid is null
 )b
 where BatchProcess_SaleBom.bomid = b.bomid

delete BatchProcess_rSaleBom  from 
(
	SELECT a.bomid FROM  rSaleBom right join 
	(SELECT bomid,itno = (select top 1 itno from batchinformation where batchinformation.Bno = BatchProcess_rSaleBom.Bno) 
	from BatchProcess_rSaleBom ) a 
	 ON a.bomid = rSaleBom.bomid and a.itno = rSaleBom.itno  WHERE rSaleBom.bomid is null
 )b
 where BatchProcess_rSaleBom.bomid = b.bomid

";
                        #endregion
                        cmd.CommandText = str刪除幽靈勾稽;
                        cmd.ExecuteNonQuery();
                        //後新增
                        #region str批次庫存更新
                        string str批次庫存更新 = @"
INSERT INTO [dbo].[BatchStock]   ([Bno],[Stno],[itno] ,[StnoQty])
SELECT                            bno , stno,itno,StnoQty 
 FROM
	(
		SELECT bno,異動倉庫 as stno,SUM(異動數量) as StnoQty,itno = (select top 1 itno from BatchInformation where BatchInformation.bno = A.Bno) FROM
			(
					SELECT * FROM 
					(
					 SELECT 單據 = '+入庫明細',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoi,序=b.recordno, bom序='',民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
					 FROM 
					 (select * from BatchProcess_Garnerd ) as a																    
					 inner join 																				    
					 (select * from Garnerd where len(stnoi) > 0 and ittrait != '1') as b 	--									    
					 on 																						    
					 a.bomid = b.bomid																			    
					UNION ALL																					    
					 SELECT 單據 = '-入庫明細',異動數量 = -1 * a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序='',民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
					 FROM 
					 (select * from BatchProcess_Garnerd ) as a	
					 inner join 
					 (select * from Garnerd where len(stnoo) > 0 and ittrait = '3') as b   --
					 on 
					 a.bomid = b.bomid
					UNION ALL																						    		
					 SELECT 單據 = '+領料明細',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoi,序=b.recordno, bom序='',民國年  = b.drdate ,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
					 FROM 
					 (select * from  BatchProcess_Drawd ) as a	
					 inner join 
					 (select * from Drawd where len(stnoi) > 0 and ittrait != '1') as b  --
					 on 
					 a.bomid = b.bomid
					UNION ALL																					    		
					 SELECT 單據 = '-領料明細',異動數量 = -1*a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoo,序=b.recordno, bom序='',民國年  = b.drdate ,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
					 FROM 
					 (select * from  BatchProcess_Drawd ) as a	
					 inner join 
					 (select * from Drawd where len(stnoo) > 0 and ittrait != '1' ) as b -- 
					 on 
					 a.bomid = b.bomid
		
					UNION ALL																					    		
					 SELECT 單據 = '+進貨明細',異動數量  = a.Qty,單據編號 = b.bsno,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.bsdate ,西元年 = b.bsdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 = bShop.fano,廠商簡稱=bShop.faname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
					 FROM 
					 (select * from BatchProcess_bShopd ) as a	
					 inner join 
					 (select * from bShopd where  ittrait != '1' ) as b  --
					 on 
					 a.bomid = b.bomid
					 left join bShop
					 on
					 b.bsno = bShop.bsno
		
					UNION ALL																					    		
					 SELECT 單據 = '-銷貨明細',異動數量  = -1*a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = Sale.cuno,客戶簡稱= Sale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
					 FROM 
					 (select * from BatchProcess_Saled ) as a	
					 inner join 
					 (select * from Saled where ittrait != '1') as b 
					 on 
					 a.bomid = b.bomid
					  left join Sale
					 on
					 b.sano = Sale.sano
					UNION ALL																						    		
					 SELECT 單據 = '+銷退明細',異動數量  = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.sadate ,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = rSale.cuno,客戶簡稱 = rSale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
					 FROM 
					 (select * from BatchProcess_rSaleD ) as a	
					 inner join 
					 (select * from rSaleD where ittrait != '1') as b 
					 on 
					 a.bomid = b.bomid
					  left join rSale
					 on
					 b.sano = rSale.sano
					UNION ALL																						    		
					 SELECT 單據 = '-進退明細',異動數量  = -1*a.Qty,單據編號 = b.bsno,異動倉庫 = b.stno,序=b.recordno, bom序='',民國年  = b.bsdate ,西元年 = b.bsdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 = rShop.fano,廠商簡稱=rShop.faname1,倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
					 FROM 
					 (select * from BatchProcess_rShopD ) as a	
					 inner join 
					 (select * from rShopD where  ittrait != '1' ) as b  --
					 on 
					 a.bomid = b.bomid
					  left join rShop
					 on
					 b.bsno = rShop.bsno
					UNION ALL
					-- SELECT 單據 = '+入庫子階',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoi,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
					--  FROM BatchProcess_GarnerBom as a															    
					-- inner join 																				    
					-- (select * from Garnerd where len(stnoi) > 0) as b 													
					-- on 																						    
					-- a.bomid = b.bomid	
					--UNION ALL	
					 SELECT 單據 = '-入庫子階',異動數量 = -1*a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
					 FROM 
					 (select * from BatchProcess_GarnerBom  ) as a															    
					 inner join 																				    
					 (select * from Garnerd where len(stnoo) > 0 and ittrait = '2') as b 											    
					 on 																						    
					 a.bomid = b.bomid		
					UNION ALL
					 SELECT 單據 = '+領料子階',異動數量 = a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoi,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.drdate,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoi )
					 FROM 
					 (select * from BatchProcess_Drawbom ) as a															    
					 inner join 																				    
					 (select * from Drawd where len(stnoi) > 0 and ittrait = '1') as b 		--									    
					 on 																						    
					 a.bomid = b.bomid		
					UNION ALL
					 SELECT 單據 = '-領料子階',異動數量 = -1*a.Qty,單據編號 = b.drno,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.drdate,西元年 = b.drdate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱='',廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stnoo )
					 FROM 
					  (select * from BatchProcess_Drawbom ) as a																    
					 inner join 																				    
					 (select * from Drawd where len(stnoo) > 0 and ittrait = '1') as b 		--									    
					 on 																						    
					 a.bomid = b.bomid		
					UNION ALL
					 SELECT 單據 = '+銷退子階',異動數量 = a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = rSale.cuno,客戶簡稱 = rSale.cuname1,廠商編號 ='',廠商簡稱='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
					 FROM  
					 (select * from BatchProcess_rSaleBom  ) as a																    
					 inner join 																				    
					 (select * from rSaled  where ittrait = '1') as b 											    
					 on 																						    
					 a.bomid = b.bomid	
					 left join rSale
					 on
					 b.sano = rSale.sano
					  UNION ALL
					 SELECT 單據 = '-銷貨子階',異動數量 = -1*a.Qty,單據編號 = b.sano,異動倉庫 = b.stno,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.sadate,西元年 = b.sadate1,a.Bno,a.Bomid,a.Rec,客戶編號 = sale.cuno,客戶簡稱 = sale.cuname1,廠商編號 ='',廠商簡稱 ='',倉庫名稱 = (select top 1 stname from stkroom where stno =  b.stno )
					 FROM 
					 (select * from BatchProcess_SaleBom  ) as a															    
					 inner join 																				    
					 (select * from Saled where ittrait = '1') as b 											    
					 on 																						    
					 a.bomid = b.bomid	
					 left join sale
					  on
					 b.sano = sale.sano


                    UNION ALL
                    SELECT 單據 = '+調整',異動數量 = a.Qty,單據編號 = a.adno,異動倉庫 = a.stno,序=a.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = a.addate,西元年 = a.addate1,a.Bno,a.Bomid,a.Rec,客戶編號 = '',客戶簡稱 = '',廠商編號 ='',廠商簡稱 ='',倉庫名稱 = (select top 1 stname from stkroom where stno =  a.stno )
                    FROM 
                    BatchProcess_adjustd as a
                    

					 ) Z
			) A
			GROUP BY BNO,異動倉庫
		) 更新資料";
                        #endregion
                        cmd.CommandText = str批次庫存更新;
                        cmd.ExecuteNonQuery();

                        // commit
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }

        private void buttonSmallT3_Click(object sender, EventArgs e)
        {
            #region 取得目前工作站數 若資料庫連線數>1 不可執行
            //var ct = GetConnectionningCount();
            //if (ct > 1)
            //{
            //    MessageBox.Show("目前有 " + ct + " 台連線數，請確定其他工作站是否關閉!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            #endregion
            if (MessageBox.Show("請確定是否要釋放單據的修改狀態", "訊息視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Cancel)
                return;

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter ad = new SqlDataAdapter())
            {
                cn.Open();
                using (SqlTransaction tran = cn.BeginTransaction("Transaction"))
                {
                    cmd.Connection = cn;
                    cmd.Transaction = tran;
                    try
                    {

                        cmd.CommandText = @"update sale set IsModify ='0' 
                        update rsale set IsModify ='0' 
                        update bshop set IsModify ='0' 
                        update rshop set IsModify ='0'
                        update draw set IsModify ='0' 
                        update allot set IsModify ='0' 
                        update adjust set IsModify ='0' 
                        update garner set IsModify ='0'
                        update InStk set IsModify ='0' 
                        update rlend set IsModify ='0' 
                        update lend set IsModify ='0' 
                        update Borr set IsModify ='0'
                        update RBorr set IsModify ='0'
                        update quote set IsModify ='0' 
                        update ford set IsModify ='0' 
                        update fquot set IsModify ='0' 
                        update [order] set IsModify ='0'
                        update Iv set IsModify ='0'  
                        update BatchProcess_adjust set IsModify ='0' 
                        ";
                        cmd.ExecuteNonQuery();
                        // commit
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show(ex.ToString());
                    }
                    MessageBox.Show("釋放完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }















    }
}
