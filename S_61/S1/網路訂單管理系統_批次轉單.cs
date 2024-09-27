using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using System.Data.SqlClient;
using S_61.Basic;
using System.Threading.Tasks;

namespace S_61.S1
{
    public partial class 網路訂單管理系統_批次轉單 : Formbase
    {
        DataTable Dt_WebOrder = new DataTable();
        DataTable Dt_WebOrderD = new DataTable();

        public 網路訂單管理系統_批次轉單()
        {
            InitializeComponent();
        }

        private void 網路訂單管理系統_批次轉單_Load(object sender, EventArgs e)
        {
            訂單日期.MaxInputLength = (Common.User_DateTime == 1) ? 9 :10;
            交貨日期.MaxInputLength = (Common.User_DateTime == 1) ? 9 :10;
            this.交貨日期.DataPropertyName = (Common.User_DateTime == 1) ? "交貨日期" : "交貨日期1";
            this.訂單日期.DataPropertyName = (Common.User_DateTime == 1) ? "訂單日期" : "訂單日期1";
          // string Sql_網路訂單 = @"select 序號 = ROW_NUMBER() OVER(order by orderState asc,ordate desc,esdate desc),* from weborder where orderState =0 order by orderState asc,ordate desc,esdate desc";
            string Sql_網路訂單 = @"
            select 轉正式訂單 ='',序號 = ROW_NUMBER() OVER(order by orderState asc,ordate desc,esdate desc),
            LEFT(ordate,3)+'/'+SUBSTRING(ordate,4,2)+'/'+RIGHT(ordate,2) 訂單日期,
            LEFT(ordate1,4)+'/'+SUBSTRING(ordate1,5,2)+'/'+RIGHT(ordate1,2) 訂單日期1,
            LEFT(esdate,3)+'/'+SUBSTRING(esdate,4,2)+'/'+RIGHT(esdate,2) 交貨日期,
            LEFT(esdate1,4)+'/'+SUBSTRING(esdate1,5,2)+'/'+RIGHT(esdate1,2) 交貨日期1,
            * from weborder where orderState =0 order by orderState asc,ordate desc,esdate desc";
            LoadDt(Dt_WebOrder, Sql_網路訂單);
            dataGridViewT1.DataSource = Dt_WebOrder;
            if (Dt_WebOrder.Rows.Count > 0)
            {
                Dt_WebOrderD.Clear();
                string OrNo = Dt_WebOrder.Rows[0]["orno"].ToString();
                string Sql_網路訂單明細 = "select 序號 =  ROW_NUMBER() OVER(ORDER BY orno),* from weborderd where orno = @OrNo";
                LoadDt(Dt_WebOrderD, Sql_網路訂單明細, OrNo);
                dataGridViewT2.DataSource = Dt_WebOrderD;
            }
        }

        private void LoadDt(DataTable dt , string SqlStr,string OrNo="")
        {
            dt.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                if (OrNo != "")
                    cmd.Parameters.AddWithValue("OrNo", OrNo);
                cmd.CommandText = SqlStr;
                da.Fill(dt);
            }
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridViewT1.CurrentCell == null) return;
            dataGridViewT2.DataSource = null;
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                if (e.Row.Index == dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected))
                {
                    var index = e.Row.Index;
                    string OrNo = Dt_WebOrder.Rows[index]["orno"].ToString();
                    string Sql_網路訂單明細 = "select 序號 =  ROW_NUMBER() OVER(ORDER BY orno),* from weborderd where orno = @OrNo";
                    LoadDt(Dt_WebOrderD, Sql_網路訂單明細, OrNo);
                    dataGridViewT2.DataSource = Dt_WebOrderD;
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<string> List_已被轉正世訂單之網路單號 = new List<string>();
            using (SqlConnection connection = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandTimeout = 300;
                connection.Open();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction();
                cmd.Connection = connection;
                cmd.Transaction = transaction;
                try
                {
                    #region 批次轉正式訂單
                    for (int i = 0; i < Dt_WebOrder.Rows.Count; i++)
                    {
                        string orno = Dt_WebOrder.Rows[i]["orno"].ToString();
                        string cuaddr1 = Dt_WebOrder.Rows[i]["cuaddr1"].ToString();
                        string cuper1 = Dt_WebOrder.Rows[i]["cuper1"].ToString();
                        string cutel1 = Dt_WebOrder.Rows[i]["cutel1"].ToString();
                        if (Dt_WebOrder.Rows[i]["轉正式訂單"].ToString() != "V") continue;

                        if (判斷此網路訂單是否已經轉過正是訂單(orno,cmd))
                        {
                            List_已被轉正世訂單之網路單號.Add(orno);
                            continue;
                        }
                        轉正世訂單之網路單號(cmd, orno, cuaddr1, cuper1, cutel1);
                    }
                    transaction.Commit();
                    #endregion
                    #region MessageBox show 已轉正是訂單指訊息
                    if (List_已被轉正世訂單之網路單號.Count > 0)
	                {
                        string Str_已被傳正是訂單 = "";
                        for (int i = 0; i < List_已被轉正世訂單之網路單號.Count; i++)
                        {
                            Str_已被傳正是訂單 += List_已被轉正世訂單之網路單號[i] + "\n";
                        }
                        MessageBox.Show("以下網路訂單已被他人轉成正是訂單，在此批次作業略過。\n" + Str_已被傳正是訂單,"訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     } 
                    #endregion

                    //補印出撿貨表

                    MessageBox.Show("批次傳出完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    #region ReLoad 重刷版面
                  // string Sql_網路訂單 = "select 序號 = ROW_NUMBER() OVER(order by orderState asc,ordate desc,esdate desc),* from weborder where orderState =0 order by orderState asc,ordate desc,esdate desc";
                    string Sql_網路訂單 = @"select
            轉正式訂單 ='',
            序號 = ROW_NUMBER() OVER(order by orderState asc,ordate desc,esdate desc),
            LEFT(ordate,3)+'/'+SUBSTRING(ordate,4,2)+'/'+RIGHT(ordate,2) 訂單日期,
            LEFT(ordate1,4)+'/'+SUBSTRING(ordate1,5,2)+'/'+RIGHT(ordate1,2) 訂單日期1,
            LEFT(esdate,3)+'/'+SUBSTRING(esdate,4,2)+'/'+RIGHT(esdate,2) 交貨日期,
            LEFT(esdate1,4)+'/'+SUBSTRING(esdate1,5,2)+'/'+RIGHT(esdate1,2) 交貨日期1,
            * from weborder where orderState =0 order by orderState asc,ordate desc,esdate desc";
                    LoadDt(Dt_WebOrder, Sql_網路訂單);
                    dataGridViewT1.DataSource = Dt_WebOrder;
                    #endregion
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show(ex2.Message);
                    }
                }
            }


        }

        private void 轉正世訂單之網路單號(SqlCommand cmd, string NetOrNo, string cuaddr1, string cuper1, string cutel1)
        {
            TextBoxT OrDate = new TextBoxT();
            OrDate.Name = "";
            OrDate.Text = Date.GetDateTime(Common.User_DateTime);
            OrDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
            TextBoxT orno = new TextBoxT();
            orno.Name = "orno";
            orno.MaxLength = 16;

            if (Common.JESetSSID("dbo.[Order]", ref OrDate, ref orno,cmd) == false)
            {
                MessageBox.Show("單據號碼計算錯誤，轉單失敗！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
             
            DataTable dt = new DataTable();
            DataTable dtD = new DataTable();
            DataTable dtBom = new DataTable();
            DataTable temp = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = "Select * from dbo.[order] where 1 = 0";
                da.Fill(dt);
                cmd.CommandText = "Select * from dbo.[orderd] where 1 = 0";
                da.Fill(dtD);
                cmd.CommandText = "Select * from dbo.[orderbom] where 1 = 0";
                da.Fill(dtBom);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("OrNo", NetOrNo);
                cmd.CommandText = "Select * from weborder where OrNo = (@OrNo)";
                da.Fill(dt);
                cmd.CommandText = "Select * from weborderd where OrNo = (@OrNo)";
                da.Fill(dtD);
                cmd.CommandText = "Select * from weborderbom where OrNo = (@OrNo)";
                da.Fill(dtBom);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["OrNo"] = NetOrNo;  
                    dt.Rows[i]["NetNo"] = NetOrNo;
                    dt.Rows[i]["recordno"] = dtD.Rows.Count;
                    dt.Rows[i]["AppDate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                    dt.Rows[i]["EdtDate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                    dt.Rows[i]["AppScNo"] = Common.User_Name;
                    dt.Rows[i]["EdtScNo"] = Common.User_Name;
                    dt.Rows[i]["cuper1"] = cuper1;
                    dt.Rows[i]["AdAddr"] = cuaddr1;
                    dt.Rows[i]["cutel1"] = cutel1;
                    dt.Rows[i].AcceptChanges();
                    dt.Rows[i].SetAdded();
                }
                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    dtD.Rows[i]["OrNo"] = NetOrNo;
                    dtD.Rows[i]["bomid"] = NetOrNo + (i + 1).ToString().PadLeft(10, '0');
                    dtD.Rows[i]["bomrec"] = (i+1).ToString();
                    dtD.Rows[i]["qtynotout"] = dtD.Rows[i]["qty"].ToDecimal();
                    dtD.Rows[i]["qtynotinStk"] = dtD.Rows[i]["qty"].ToDecimal();
                    dtD.Rows[i]["PQty"] = dtD.Rows[i]["qty"].ToDecimal();
                    dtD.Rows[i]["mwidth1"] = 0;
                    dtD.Rows[i]["mwidth2"] = 0;
                    dtD.Rows[i]["mwidth3"] = 0;
                    dtD.Rows[i]["mwidth4"] = 0;
                    dtD.Rows[i]["pformula"] = "";
                    dtD.Rows[i]["AdPer1"] = cuper1;
                    dtD.Rows[i]["AdAddr"] = cuaddr1;
                    dtD.Rows[i]["AdTel"] = cutel1;
                    dtD.Rows[i].AcceptChanges();
                    dtD.Rows[i].SetAdded();
                }
                //for (int i = 0; i < dtBom.Rows.Count; i++)
                //{
                //    dtBom.Rows[i]["OrNo"] = orno.Text.Trim();
                //    dtBom.Rows[i]["bomid"] = orno.Text.Trim() + dtBom.Rows[i]["bomrec"].ToString().PadLeft(10, '0');
                //    dtBom.Rows[i].AcceptChanges();
                //    dtBom.Rows[i].SetAdded();
                //}

                try
                {
                    cmd.CommandText = "Select * from dbo.[order] where 1=0";
                    da.Fill(temp);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.InsertCommand = builder.GetInsertCommand();
                    da.Update(dt);

                    cmd.CommandText = "Select * from dbo.[orderd] where 1=0";
                    da.Fill(temp);
                    builder = new SqlCommandBuilder(da);
                    da.InsertCommand = builder.GetInsertCommand();
                    da.Update(dtD);

                    cmd.CommandText = "Select * from dbo.[orderbom] where 1=0";
                    da.Fill(temp);
                    builder = new SqlCommandBuilder(da);
                    da.InsertCommand = builder.GetInsertCommand();
                    da.Update(dtBom);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("OrNo", NetOrNo);
                    cmd.Parameters.AddWithValue("SysOrNo", NetOrNo);
                    cmd.CommandText = " Update weborder set SysOrNo = (@SysOrNo), orderState = '1' where OrNo = (@OrNo);";
                    cmd.ExecuteNonQuery();

                    //cmd.CommandText = "Select top 1 * from weborder where OrNo = (@OrNo)";
                    //da.Fill(dt);
                    //WriteToTxt(dt.AsEnumerable().FirstOrDefault());

                    //補印出訂單貼紙標籤

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool 判斷此網路訂單是否已經轉過正是訂單(string OrNo,SqlCommand cmd)
        {
            #region 判斷是否轉過訂單 如有轉過 return true
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("OrNo",OrNo);
            cmd.CommandText = "select * from weborder where orno =@OrNo";
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["orderState"].ToString() != "0")
                        return true;
                }
            }
            #endregion
            return false;
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Dt_WebOrder.Rows.Count == 0) return;
            string ColumnName = dataGridViewT1.Columns[dataGridViewT1.CurrentCell.ColumnIndex].Name;
            if (ColumnName == "轉正式訂單")
            {
                if (Dt_WebOrder.Rows[dataGridViewT1.CurrentCell.RowIndex]["轉正式訂單"].ToString() == "V")
                {
                    Dt_WebOrder.Rows[dataGridViewT1.CurrentCell.RowIndex]["轉正式訂單"] = "";
                }
                else
                    Dt_WebOrder.Rows[dataGridViewT1.CurrentCell.RowIndex]["轉正式訂單"] = "V";
            }
        }

        private void OrNo_TextChanged(object sender, EventArgs e)
        {
            string orno = OrNo.Text;
            int index = -1,SeartchLtnth = orno.Length;
            object lock_ = new object();
            Parallel.For(0, Dt_WebOrder.Rows.Count, (i, loopState) =>
            {
                if (Dt_WebOrder.Rows[i]["orno"].ToString().Length >= SeartchLtnth)
                {
                    if (Dt_WebOrder.Rows[i]["orno"].ToString().Substring(0, SeartchLtnth) == orno)
                    {
                        lock (lock_)
                        {
                            index = i;
                            loopState.Break();
                        }
                    }
                }
            });
            if (index == -1) return;
            dataGridViewT1.Rows[index].Selected = true;

        }

        private void buttonSmallT1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCKall_Click(object sender, EventArgs e)
        {
            是否全部選取("V");
        }

        private void btnCKnull_Click(object sender, EventArgs e)
        {
            是否全部選取("");
        }

        private void 是否全部選取(string str)
        {
            for (int i = 0; i < Dt_WebOrder.Rows.Count; i++)
            {
                Dt_WebOrder.Rows[i]["轉正式訂單"] = str;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    btnCKall_Click(null,null);      break;
                case Keys.F3:
                    btnCKnull_Click(null,null);     break;
                case Keys.F9:
                    btnConfirm_Click(null,null);    break;
                case Keys.F4:
                    buttonSmallT1_Click(null,null); break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
