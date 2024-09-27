using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Data.SqlClient;
using JBS.JS;

namespace S_61
{
    // 批次資料對應單據的動作函式
    class BatchProcess 
    {
        internal void 建立結構(DataTable dt = null, DataTable dt1 = null, DataTable dt2 = null, DataTable dt3 = null) 
        {
            try
            {
                SQL.ExecuteNonQuery("select 序號 = '',Bno ='',bomid ='',rec ='',Qty =0.0,Stno ='',stname ='', Fano ='',faname ='',Itno='',Itname='',Batchno ='',Date ='',Date1 ='',edit='',cuno='',cuname='',BomRec='',ItUnit='',StNoQty= '',修改批號 = '刪除' from BatchProcess_Saled where 1 = 0", null, dt);
                if(dt1 != null)
                    dt1 = dt.Clone();
                if (dt2 != null)
                    dt2 = dt.Clone();
                if (dt3 != null)
                    dt3 = dt.Clone();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 刪除dt之中相等於bomrec的Row
        /// </summary>
        internal void 刪除批次異動(DataTable dt, string bomrec)
        {
            //防止出錯，因為沒有批次功能的進來的dt有可是null
            if(dt != null) 
            {
                //找到需刪除的index
                //int index = dt.MultiThreadFindIndex("rec", bomrec);
                ////如果有需要刪除row
                //while (index > -1)
                //{
                //    //刪除index
                //    dt.Rows.RemoveAt(index);
                //    //繼續找需刪除的index
                //    index = dt.MultiThreadFindIndex("rec", bomrec);
                //}

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["rec"].ToString().Trim() == bomrec)
                        dt.Rows[i].Delete();
                }
                dt.AcceptChanges();
            }
        }
        /// <summary>
        /// 刪除dt_BatchBom之中相等於bomrec的Row
        /// </summary>
        internal void BOM刪除批次異動(DataTable dt_BatchBom,string bomrec)
        {
            //防止出錯，因為沒有批次功能的進來的 dt_BatchBom 有可是 null
            if (dt_BatchBom != null) 
            {
                //找到需刪除的index
                int index = dt_BatchBom.MultiThreadFindIndex("BomRec", bomrec);
                //如果有需要刪除row
                while (index > -1)
                {
                    //刪除index
                    dt_BatchBom.Rows.RemoveAt(index);
                    //繼續找需刪除的index
                    index = dt_BatchBom.MultiThreadFindIndex("BomRec", bomrec);
                }
            }
        }
        /// <summary>
        /// 單據明細上的倉庫變動時，會呼叫此函數去修改[Bom批次資料]
        /// </summary>                    //bom批次資料[目的] //單據的明細[來源]//單據的子階[來源]  //修改的Index   
        internal void BOM同步批次異動倉庫(DataTable dt_BatchBom, DataTable dtD, DataTable dt_Bom, int index_source)
        {
            //取得異動的倉庫資料
            string 上層Stno   = dtD.Rows[index_source]["stno"].ToString();
            string 上層Stname = dtD.Rows[index_source]["Stname"].ToString();
            //取的改編的BomRec
            string BomRec = dtD.Rows[index_source]["BomRec"].ToString();

            //明細改倉庫後，批號不管是依附哪個子件，都需要改倉庫
            for (int i = 0; i < dt_BatchBom.Rows.Count; i++)
            {
                if (dt_BatchBom.Rows[i]["BomRec"].ToString() == BomRec)
                {
                    dt_BatchBom.Rows[i]["Stno"] = 上層Stno;
                    dt_BatchBom.Rows[i]["Stname"] = 上層Stname;
                }
            }



            //單據子階跑迴圈
            //for (int i = 0; i < dt_Bom.Rows.Count; i++)
            //{
            //    //透過BomRec找出需要修改的row
            //    if (dt_Bom.Rows[i]["BomRec"].ToString() == BomRec)
            //    {
            //        //以 BomRec 與 rec 為key，找出[bom批次資料]的Index
            //        int index = dt_BatchBom.MultiThreadFindIndex("BomRec", BomRec, "rec", dt_Bom.Rows[i]["itrec"].ToString());
            //        //如果有對應的資料
            //        if (index > -1)
            //        {
            //            //修改資料
            //            dt_BatchBom.Rows[index]["Stno"]   = 上層Stno;
            //            dt_BatchBom.Rows[index]["Stname"] = 上層Stname;
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 單據明細上的倉庫變動時，會呼叫此函數去修改[明細批次資料]
        /// </summary>              //明細批次資料[目的] //單據的明細[來源] //修改的Index //倉庫編號[修改後] //倉庫名稱[修改後]
        internal void 同步批次異動倉庫(DataTable dt, DataTable dt_srouce, int index_source, string StNo, string StName)
        {
            //取的改變的BomRec
            string bomrec = dt_srouce.Rows[index_source]["BomRec"].ToString();
            //找出需修改的Index
            int index = dt.MultiThreadFindIndex("rec", bomrec);
            //如果有需修改的批次資料
            if (index > -1)
            {
                //明細批次資料[目的]跑迴圈
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //如果是符合bomrec
                    if (dt.Rows[i]["rec"].ToString() == bomrec)
                    {
                        //修改倉庫資料
                        dt.Rows[i]["stno"] = StNo;
                        dt.Rows[i]["stname"] = StName;
                    }
                }
            }
        }
        /// <summary>
        /// 單據上下頁變更單key時觸發，使批次資料與單據的資料同步[進貨、進退、銷貨、銷退]
        /// </summary>                //單據名稱                   //單據編號            //傳回批次資料       //傳回批次資料temp 描述:往後修改儲存時，會透過temp去(加、扣)上次的庫存
        internal void 上下頁dt資料修改(string FormName = "bShopd", string KeyValue = "", DataTable dt = null, DataTable dt1 = null) 
        {
            try
            {
                dt.Clear();
                dt1.Clear();
                //SQL Table 描述
                //BatchProcess_XXXXX   :批次異動過程
                //BatchInformation     :批次資訊
                //                      單據明細
                string str =
@"
    select a.itno,a.fano,a.batchNo,a.bno,a.[Date],a.Date1,b.bomid,b.rec,b.qty,c.stno  from 
	    (SELECT * FROM BatchProcess_" + FormName + " WHERE bomid like '" + KeyValue + @"'+'__________')   as b
    Left join 
	    BatchInformation as a
    on a.bno = b.bno
    Left join  "
	    + FormName +@" as c
    on b.bomid = c.bomid
";
                dt.Clear();
                SQL.ExecuteNonQuery(str, null, dt);
                if (dt1 != null)
                    SQL.ExecuteNonQuery(str, null, dt1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 單據上下頁變更單key時觸發，使Bom批次資料與單據的資料同步[銷貨、銷退]，因為進貨跟進退的組合子階的數量沒有效果
        /// </summary>                    //單據名稱                 //單據編號                      //單據編號           //傳回Bom批次資料     //傳回Bom批次資料temp 描述:往後修改儲存時，會透過temp去(加、扣)上次的庫存
        internal void BOM上下頁dt資料修改(string FormName = "Saled", string FormBomName = "SaleBom", string KeyValue = "", DataTable dt = null, DataTable dt1 = null)
        {
            try
            {
                dt.Clear();
                dt1.Clear();
                //SQL Table 描述
                //BatchProcess_XXXXX   :批次異動過程
                //BatchInformation     :批次資訊
                //                      單據Bom
                string str =
@"                                                                                    
    select a.itno,a.fano,a.batchNo,a.bno,a.[Date],a.Date1,b.bomid,b.rec,b.qty,c.stno, BomRec = substring( right(c.Bomid,10), patindex('%[^0]%',right(c.Bomid,10)),10)  from 
	    (SELECT * FROM BatchProcess_" + FormBomName + " WHERE bomid like '" + KeyValue + @"'+'__________')   as b
    Left join 
	    BatchInformation as a
    on a.bno = b.bno
    Left join  "
        + FormName + @" as c
    on b.bomid = c.bomid" ;
                dt.Clear();
                SQL.ExecuteNonQuery(str, null, dt);
                if (dt1 != null)
                    SQL.ExecuteNonQuery(str, null, dt1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        ///  [雙倉]的跟[單倉]只有在倉庫得差異，雙倉代表[領料、入庫]的作業，他們是不需要載入倉庫的，因為加、扣倉庫時，是根據單據作業上的舊倉庫邊號old_StNoI與新old_StNoI去做動作的。
        /// </summary>
        internal void 雙倉上下頁dt資料修改(string FormName = "Drawd", string KeyValue = "", DataTable dt = null, DataTable dt1 = null)
        {
            try
            {
                dt.Clear();
                dt1.Clear();
                string str =
@"
    select a.itno,a.fano,a.batchNo,a.bno,a.[Date],a.Date1,b.bomid,b.rec,b.qty,Stno=''  from 
	    (SELECT * FROM BatchProcess_" + FormName + " WHERE bomid like '" + KeyValue + @"'+'__________')   as b
    Left join 
	    BatchInformation as a
    on a.bno = b.bno
    Left join  "
        + FormName + @" as c
    on b.bomid = c.bomid
";
                dt.Clear();
                SQL.ExecuteNonQuery(str, null, dt);
                if (dt1 != null)
                    SQL.ExecuteNonQuery(str, null, dt1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        ///  [雙倉]的跟[單倉]只有在倉庫得差異，雙倉代表[領料、入庫]的作業，他們是不需要載入倉庫的，因為加、扣倉庫時，是根據單據作業上的舊倉庫邊號old_StNoI與新old_StNoI去做動作的。
        /// </summary>
        internal void BOM雙倉上下頁dt資料修改(string FormName = "Drawd", string FormBomName = "DrawBom", string KeyValue = "", DataTable dt = null, DataTable dt1 = null)
        {
            try
            {
                dt.Clear();
                dt1.Clear();
                string str =
@"                                                                                    
    select a.itno,a.fano,a.batchNo,a.bno,a.[Date],a.Date1,b.bomid,b.rec,b.qty, Stno='', BomRec = substring( right(c.Bomid,10), patindex('%[^0]%',right(c.Bomid,10)),10)  from 
	    (SELECT * FROM BatchProcess_" + FormBomName + " WHERE bomid like '" + KeyValue + @"'+'__________')   as b
    Left join 
	    BatchInformation as a
    on a.bno = b.bno
    Left join  "
        + FormName + @" as c
    on b.bomid = c.bomid";
                dt.Clear();
                SQL.ExecuteNonQuery(str, null, dt);
                if (dt1 != null)
                    SQL.ExecuteNonQuery(str, null, dt1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 當單據作業按下新增/複製時觸發
        /// </summary>
        internal void WhenAppendOrDuplicate(DataTable dt, DataTable dt1, DataTable dt_Bom = null, DataTable dt_Bom1 = null)
        {
            dt.Clear();
            dt1.Clear();
            if (dt_Bom != null)
                dt_Bom.Clear();
            if (dt_Bom1 != null)
                dt_Bom1.Clear();
        }                      
        /// <summary>
        /// 當單據作業按下批次勾稽按鈕時觸發
        /// </summary>
        ///                               //作業名稱       //該作業所對應的gGridView     //明細Table    //批次過程Table            //廠商編號     //廠商名稱       //客戶編號     //客戶名稱      //扣料倉庫編號             
        internal void WhenGridBadch_click(string FormName, DataGridViewT dataGridViewT1, DataTable dtD, DataTable dt_BatchProcess, TextBoxT FaNo, TextBoxT FaName, TextBoxT CuNo, TextBoxT CuName,TextBoxT Stnoo = null,bool isbom =false,bool 可以取回 = true)
        {
            //取得目前Focus的Index
            int Index_gv = dataGridViewT1.CurrentCell.RowIndex;
            //如果明細沒有產品編號，不得開窗
            if (dataGridViewT1.Rows[Index_gv].Cells["產品編號"].Value.ToString().Trim().Length == 0) return;
            //如果明細是組合品，不得開窗，因為明細的組合品是不管數量的
            if (dataGridViewT1.Rows[Index_gv].Cells["產品組成"].Value.ToString().Trim() == "組合品") return;
            //取得該明細的bomrec
            string bomrec = dtD.Rows[Index_gv]["bomrec"].ToString();
            //根據明細的bomrec關聯出相對應的批次資料
            var list = dt_BatchProcess.FindIndexToList("rec", bomrec);
            //temp為傳入批次勾稽作業的table
            DataTable temp = dt_BatchProcess.Clone();
            //根據相對應的批次資料跑回圈
            for (int k = 0; k < list.Count; k++)
            {
                //取的目前index
                int Index = list[k];
                //新建立一個row
                DataRow dr = temp.NewRow();
                //將資料填入row
                for (int i = 0; i < dt_BatchProcess.Columns.Count; i++)
                {
                    string column = dt_BatchProcess.Columns[i].ColumnName.ToString();
                    string value = dt_BatchProcess.Rows[Index][column].ToString();
                    dr[column] = value;
                }
                //倉庫編號
                string StNo = "";

                if (FormName != "FrmDraw" && FormName != "FrmGarner") 
                {
                    //如果不是領料、入庫作業開窗，填入相對應明細的倉庫編號
                    StNo = dtD.Rows[Index_gv]["stno"].ToString();
                }
                else
                {
                    //如果是領料、入庫作業開窗，倉庫是填入扣料倉
                    StNo = Stnoo.Text;
                } 
                dr["序號"] = (k + 1).ToString();
                dr["修改批號"] = "刪除";
                //關聯出倉庫名稱
                dr["StName"] = SQL.ExecuteScalar("SELECT TOP 1 stname from StkRoom where StNo = '" + dt_BatchProcess.Rows[Index]["StNo"].ToString() + "'");
                //填入產品名稱
                dr["ItName"] = dtD.Rows[Index_gv]["ItName"].ToString();
                //填入明細單位
                dr["ItUnit"] = dtD.Rows[Index_gv]["ItUnit"].ToString();
                //關聯出倉商名稱
                dr["FaName"] = SQL.ExecuteScalar("select top 1 faname1 from fact where fano = '" + dt_BatchProcess.Rows[Index]["fano"].ToString() + "'");
                //關聯出現有批次庫存量
                string 庫存量 = SQL.ExecuteScalar("SELECT top 1 StNoQty FROM BatchStock where stno ='" + StNo + "' and bno ='" + dt_BatchProcess.Rows[Index]["Bno"].ToString() + "'");
                if (庫存量 == "") 庫存量 = "0";
                //填入現有庫存量
                dr["StNoQty"] = 庫存量;
                //填入bomrec
                dr["rec"] = bomrec;
                //將row加入
                temp.Rows.Add(dr);
                temp.AcceptChanges();
            }
            //批次作業開窗
            using (Batch frm = new Batch(FormName, temp, isbom, 可以取回))
            {
                //外層廠商編號 [進貨、進退]
                if (FaNo != null)
                {
                    frm._Fano = FaNo.Text;
                    frm._Faneme = FaName.Text;
                }
                //外層客戶編號 [銷貨、銷退]
                if (CuNo != null)
                {
                    frm._cuno = CuNo.Text.Trim();
                    frm._cuname = CuName.Text.Trim();
                }
                //外層數量傳入
                frm._Qty    = GetQty(FormName, dtD.Rows[Index_gv]);  
                //帶入對應明細的資料
                frm._itno   = dtD.Rows[Index_gv]["itno"].ToString();
                frm._itname = dtD.Rows[Index_gv]["itname"].ToString();
                frm._bomid  = dtD.Rows[Index_gv]["bomid"].ToString();
                frm._rec    = dtD.Rows[Index_gv]["BomRec"].ToString();
                //帶入所對應明細的倉庫 [進貨、進退、銷貨、銷退]
                if (FormName != "FrmDraw" && FormName != "FrmGarner") 
                {
                    frm._stno   = dtD.Rows[Index_gv]["stno"].ToString();
                    frm._stname = dtD.Rows[Index_gv]["stname"].ToString();
                }
                else //因為[領料、入庫]之明細沒有倉庫，所以帶外層扣料倉庫
                {
                    frm._stno = Stnoo.Text;
                }
                //進貨時，要直接帶入進貨廠商資料，為了操作方便
                if (FormName == "FrmBShop")
                {
                    frm._Fano = FaNo.Text;
                    frm._Faneme = FaName.Text;
                }
                //開窗
                frm.ShowDialog();
                //批次作業開窗取回時
                if (frm.DialogResult == DialogResult.Yes)
                {
                    //更新批次資料
                    //1.先刪除之前的ROW
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        int Index = list[i];//list包含"單據明細"與"批次資料"相對應的Index
                        dt_BatchProcess.Rows.RemoveAt(Index);
                    }
                    //2.再加入新的資料
                    dt_BatchProcess.Merge(temp);
                    dt_BatchProcess.AcceptChanges();
                }
                else
                {
                    //不動作
                }
            }
        }
        /// <summary>
        /// 獲得外層的數量
        /// </summary>
        private string GetQty(string FormName, DataRow row)
        {
            string Qty = "";
            if (FormName == "FrmDraw" || FormName == "FrmGarner") //領料入庫沒有包裝數量之欄位
            {
                Qty = (row["Qty"].ToDecimal() * row["itpkgqty"].ToDecimal()).ToDecimal("f"+Common.Q).ToString();
            }
            else
            {
                Qty = (row["pqty"].ToDecimal() * row["itpkgqty"].ToDecimal()).ToDecimal("f" + Common.Q).ToString();
            }
            return Qty;
        }
        /// <summary>
        /// 刪除Bom批次異動過程資料
        /// </summary>                     //修改的批次異動資料           //刪除的序號
        internal void WhenFormBomDeleteRow(DataTable dt_Bom_BatchProcess, int 序號)
        {
            //先，刪除該筆Bom明細的批次異動資料
            刪除批次異動(dt_Bom_BatchProcess, 序號.ToString());
            //後，重新編輯序號，因為Bom的rec就是序號欄位的值，所以如有一筆序號被刪除時，則大於該筆序號的值都必須減1
            for (int i = 0; i < dt_Bom_BatchProcess.Rows.Count; i++)
            {
                int rec = dt_Bom_BatchProcess.Rows[i]["rec"].ToInteger();
                if (rec > 序號)
                {
                    dt_Bom_BatchProcess.Rows[i]["rec"] = rec - 1;
                }
            }

        }
        /// <summary>
        /// 插入Bom批次異動過程資料
        /// </summary>                    //修改的批次異動資料           //插入的序號
        internal void WhenFormBomInsertRow(DataTable dt_Bom_BatchProcess, int 序號)
        {
            //重新編輯序號，因為Bom的rec就是序號欄位的值，所以如插入一筆序號時，則小於等於該筆序號的值都必須加1
            for (int i = 0; i < dt_Bom_BatchProcess.Rows.Count; i++)
            {
                int rec = dt_Bom_BatchProcess.Rows[i]["rec"].ToInteger();
                if (rec >= 序號)
                {
                    dt_Bom_BatchProcess.Rows[i]["rec"] = rec + 1;
                }
            }
        }
        /// <summary>
        /// 刪除無明細對應之批號資料
        /// </summary>      
        internal void 刪除無明細對應之批號資料(DataTable dt, DataTable dt_BatchProcess)
        {
            for (int i = 0; i < dt_BatchProcess.Rows.Count; i++)
            {
                if (dt.AsEnumerable().Any(r => r["bomrec"].ToDecimal() == dt_BatchProcess.Rows[i]["rec"].ToDecimal())) continue;
                dt_BatchProcess.Rows[i].Delete();
            }
            dt_BatchProcess.AcceptChanges();
        }
        internal void 刪除無明細對應之bom批號資料(DataTable dt, DataTable dt_Bom_BatchProcess)
        {
            for (int i = 0; i < dt_Bom_BatchProcess.Rows.Count; i++)
            {
                if (dt.AsEnumerable().Any(r => r["bomrec"].ToDecimal() == dt_Bom_BatchProcess.Rows[i]["BomRec"].ToDecimal())) continue;
                dt_Bom_BatchProcess.Rows[i].Delete();
            }
            dt_Bom_BatchProcess.AcceptChanges();
        }
    }
    /// 批次資料，對應存檔時的函式
    class BatchFunction
    {
        /// <summary>
        /// 1.判斷 dt 裡的批次資料是否都存在，如不存在，就新增
        /// 2.找出 dt 裡的批次編號(bno)位控的row，如不存在，就填入  
        /// </summary>
        internal void Bno_get(DataTable dt, SqlCommand cmd, string FormName)
        {
            try
            {
                string str_新增批次資訊;
                //入庫時，才會填入製造數量  
                if(FormName == "Garnerd")  
                    str_新增批次資訊 = @"insert into BatchInformation(Itno,Fano,Batchno,[Date],Date1,Qty) values(@itno,@fano,@BatchNo,@Date,@Date1,@Qty) ";
                else                        //非入庫作業時，製造數量皆為0
                    str_新增批次資訊 = @"insert into BatchInformation(Itno,Fano,Batchno,[Date],Date1,Qty) values(@itno,@fano,@BatchNo,@Date,@Date1,0) ";
                
                string str_查詢批次資訊Bno = @"select bno from BatchInformation WHERE Itno = @itno and Fano = @fano and Batchno = @BatchNo";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["bno"].ToString().Trim().Length > 0) continue;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", dt.Rows[i]["Itno"].ToString());
                    cmd.Parameters.AddWithValue("BatchNo", dt.Rows[i]["BatchNo"].ToString());
                    cmd.Parameters.AddWithValue("fano", dt.Rows[i]["fano"].ToString());
                    string bno = SQL.ExecuteScalar(str_查詢批次資訊Bno, null, cmd);
                    //如果無該筆批次資料，則新增一筆批次資料
                    if (bno == "") 
                    {
                        cmd.Parameters.AddWithValue("Date", dt.Rows[i]["Date"].ToString());
                        cmd.Parameters.AddWithValue("Date1", dt.Rows[i]["Date1"].ToString());
                        cmd.Parameters.AddWithValue("Qty", dt.Rows[i]["Qty"].ToString());
                        //新增批次資料
                        SQL.ExecuteNonQuery(str_新增批次資訊, null,null, cmd);
                        //取的該筆批次資料編號
                        bno = SQL.ExecuteScalar(str_查詢批次資訊Bno, null, cmd);
                        #region 填入批次編號(Bno)
                        int index;
                        while(true)
                        {
                            // 批次資料為雙主key  key 1 = itno、batchNo、fano ； key 2 bno
                            // 找出該筆批次資料(條件為key 1)且批次編號為空的row
                            index = dt.MultiThreadFindIndex("itno",    dt.Rows[i]["itno"].ToString(),
                                                            "BatchNo", dt.Rows[i]["BatchNo"].ToString(), 
                                                            "fano",    dt.Rows[i]["fano"].ToString(),
                                                            "bno",     "");
                            //如果有找到空的批次批號row
                            if (index > -1)
                            { 
                                //填入bno
                                dt.Rows[index]["bno"] = bno;
                            }
                            else
                                break; // index==-1  表示此批次資料已經全部填入批次編號(bno)了
                        }
                        #endregion
                    }
                    else            //有資料
                    {
                        #region 填入批次編號(Bno)
                        int index;
                        while (true)
                        {
                            index = dt.MultiThreadFindIndex("itno", dt.Rows[i]["itno"].ToString(),
                                                            "BatchNo", dt.Rows[i]["BatchNo"].ToString(),
                                                            "fano", dt.Rows[i]["fano"].ToString(),
                                                            "bno", "");
                            if (index > -1)
                            {  //填入bno
                                dt.Rows[index]["bno"] = bno;
                            }
                            else break;// index==-1  表示此批次資料已經都填入bno了
                        }
                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 將批次異動過程(dt)的資料存入資料庫，步驟:1.先刪除2.後新增
        /// </summary>
        internal void BatchProcess_Insert(DataTable dt, SqlCommand cmd, string FormName = "bShopd", string KeyNo = "BsNo", string Cuno = "") 
        {
            string TableName = "BatchProcess_"+FormName;
            string str_Delete = "Delete " + TableName + " where bomid = @bomid ";
            string str_Insert = "insert " + TableName + " (bomid,rec,qty,Bno,Cuno) values  (@bomid,@rec,@qty,@Bno,@Cuno) ";
            //先刪除
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("bomid", KeyNo + dt.Rows[i]["rec"].ToString().PadLeft(10, '0'));
                SQL.ExecuteNonQuery(str_Delete, null, null, cmd);
            }
            //再新增
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("bomid", KeyNo + dt.Rows[i]["rec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("rec", dt.Rows[i]["rec"].ToString());
                cmd.Parameters.AddWithValue("qty", dt.Rows[i]["qty"].ToString());
                cmd.Parameters.AddWithValue("Bno", dt.Rows[i]["Bno"].ToString());
                //[銷貨/銷貨]才會有客戶編號(Cuno)
                if (Cuno != "")
                    cmd.Parameters.AddWithValue("Cuno", Cuno);
                else
                    cmd.Parameters.AddWithValue("Cuno", dt.Rows[i]["Cuno"].ToString());
                SQL.ExecuteNonQuery(str_Insert, null, null, cmd);
            }

        }
        /// <summary>
        /// 根據dt刪除批次異動資料
        /// </summary>
        internal void BatchProcess_Delete(DataTable dt, SqlCommand cmd, string FormName = "bShopd") 
        {
            string TableName = "BatchProcess_" + FormName;
            string str_Delete = "delete " + TableName + " where bomid = @bomid and rec = @rec";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("bomid", dt.Rows[i]["bomid"].ToString());
                cmd.Parameters.AddWithValue("rec", dt.Rows[i]["rec"].ToString());
                SQL.ExecuteNonQuery(str_Delete, null, null, cmd); 
            }

        }
        /// <summary>
        /// 根據dt增加批次庫存
        /// </summary>
        internal void BatchStock_Add(DataTable dt, SqlCommand cmd)
        {
            //宣告批次庫存TABLE
            string TableName = "BatchStock";
            //判斷是否存在該筆批次庫存資料
            string str_Being = "select count (*) from " + TableName + " where Bno = @Bno and Stno = @Stno";
            //更新庫存資料(數量)
            string str_Update = "update " + TableName + " set StnoQty =  StnoQty + (@Qty) where Bno = @Bno and Stno = @Stno";
            //新增一筆庫存資料
            string str_Insert = "insert into " + TableName + "(Bno,Stno,Itno,StnoQty) values(@Bno,@Stno,@Itno,@Qty) ";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Bno", dt.Rows[i]["Bno"].ToString());
                cmd.Parameters.AddWithValue("Stno", dt.Rows[i]["Stno"].ToString());
                cmd.Parameters.AddWithValue("Itno", dt.Rows[i]["Itno"].ToString());
                decimal Qty = dt.Rows[i]["Qty"].ToDecimal("f" + Common.Q);
                cmd.Parameters.AddWithValue("Qty", Qty);
                if (dt.Rows[i]["Stno"].ToString().Trim().Length == 0) continue;
                //如果不存在此批次庫存Row
                if (SQL.ExecuteScalar(str_Being,null,cmd) == "0")
                {
                    //新增一筆
                    SQL.ExecuteNonQuery(str_Insert,null,null,cmd);
                }
                else
                {
                    //更新批次庫存庫量
                    SQL.ExecuteNonQuery(str_Update, null, null, cmd);
                }
            }
        }
        /// <summary>
        /// 根據dt增扣批次庫存
        /// </summary>
        internal void BatchStock_Subtract(DataTable dt, SqlCommand cmd)
        {
            //宣告批次庫存TABLE
            string TableName = "BatchStock";
            //判斷是否存在該筆批次庫存資料
            string str_Being = "select count (*) from " + TableName + " where Bno = @Bno and Stno = @Stno";
            //更新庫存資料(數量)
            string str_Update = "update " + TableName + " set StnoQty =  StnoQty - @Qty where Bno = @Bno and Stno = @Stno";
            //新增一筆庫存資料
            string str_Insert = "insert into " + TableName + "(Bno,Stno,Itno,StnoQty) values(@Bno,@Stno,@Itno,-@Qty) ";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Bno", dt.Rows[i]["Bno"].ToString());
                cmd.Parameters.AddWithValue("Stno", dt.Rows[i]["Stno"].ToString());
                if (dt.Rows[i]["Stno"].ToString().Trim().Length == 0) continue;
                cmd.Parameters.AddWithValue("Itno", dt.Rows[i]["Itno"].ToString());
                decimal Qty = dt.Rows[i]["Qty"].ToDecimal("f" + Common.Q);
                cmd.Parameters.AddWithValue("Qty", Qty);
                //如果不存在此批次庫存Row
                if (SQL.ExecuteScalar(str_Being, null, cmd) == "0")
                {
                    //新增一筆
                    SQL.ExecuteNonQuery(str_Insert, null, null, cmd);
                }
                else
                {
                    //更新批次庫存庫量
                    SQL.ExecuteNonQuery(str_Update, null, null, cmd);
                }
            }
        }
        /// <summary>
        /// 1. dt用來新增本次的資料
        /// 2. Temp_dt用來刪除修改的資料
        /// </summary>                      //暫存的批次資料    //新的 
        internal void BatchBomProcess_Insert(DataTable Temp_dt, DataTable dt, SqlCommand cmd, string FormName = "bShopd", string KeyNo = "BsNo")
        {
            string TableName = "BatchProcess_" + FormName;
            string str_Being = "select count (*) from " + TableName + " where bomid = @bomid and rec = @rec";
            string str_Delete = "delete " + TableName + " where bomid = @bomid ";
            string str_Insert = "insert " + TableName + " (bomid,rec,qty,Bno,Cuno) values  (@bomid,@rec,@qty,@Bno,@Cuno) ";
            //判斷是否刪除修改前的bom批次資料[只有單據狀態為修改時，暫存的批次資料]
            if (Temp_dt != null)
            {
                for (int i = 0; i < Temp_dt.Rows.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("bomid", Temp_dt.Rows[i]["bomid"].ToString());
                    SQL.ExecuteNonQuery(str_Delete, null, null, cmd);
                }
            }
            //刪除本次批次資料
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("bomid", KeyNo + dt.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                SQL.ExecuteNonQuery(str_Delete, null, null, cmd);
            }
            //新增本次批次資料
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("bomid", KeyNo + dt.Rows[i]["BomRec"].ToString().PadLeft(10, '0'));
                cmd.Parameters.AddWithValue("rec", dt.Rows[i]["rec"].ToString());
                cmd.Parameters.AddWithValue("qty", dt.Rows[i]["qty"].ToString());
                cmd.Parameters.AddWithValue("Bno", dt.Rows[i]["Bno"].ToString());
                cmd.Parameters.AddWithValue("Cuno", dt.Rows[i]["Cuno"].ToString());
                SQL.ExecuteNonQuery(str_Insert, null, null, cmd);
            }
        }
        /// <summary>
        /// 過濾BatchNo = '' or Fano =''之row
        /// </summary>
        internal void 過濾(DataTable dt)  
        {
            for (int i = dt.Rows.Count - 1 ; i >= 0 ; i--)
            {
                if (dt.Rows[i]["BatchNo"].ToString().Trim().Length == 0 || dt.Rows[i]["fano"].ToString().Trim().Length == 0 )
                {
                    dt.Rows.RemoveAt(i);
                }
            }
            dt.AcceptChanges();
        }
    }
    /// 批次資料，存檔時的函式
    class BatchSave 
    {
        BatchFunction F = new BatchFunction();
        /// <summary>
        /// 1.新增批次 (+庫存)
        /// 2.單據作業存檔事件觸發，狀態為[新增/複製] 
        /// </summary>           //批次異動dt(來源) //cmd(sqltrancetion)  //單據名稱        //單據編號            //是否是bom         //客戶編號[銷貨、銷退]  //+倉庫編號[領料、入庫]
        internal void 進貨_Append(DataTable dt, SqlCommand cmd, string FormName = "bShopd", string KeyValue = "", bool isBom = false, string Cuno = null, string Stno = null) 
        {
            //清除批號與廠商編號為空的Row
            F.過濾(dt);
            //如果為[領料/入庫]作業會帶入單據的倉庫編號
            if (Stno != null) 強制填入(dt, "stno", Stno);
            //如果為[銷貨、銷退]作業會帶入單據的客戶編號
            if (Cuno != null) 強制填入(dt, "cuno", Cuno);
            //將[批次異動dt]都填入批次編號(bno)
            F.Bno_get(dt, cmd, FormName);
            //如果 [dt] 是 Bom
            if (isBom)
                F.BatchBomProcess_Insert(null, dt, cmd, FormName, KeyValue);
            else//如果是 [批次異動dt] 明細
                F.BatchProcess_Insert(dt, cmd, FormName, KeyValue);
            //增加批次庫存數量
            F.BatchStock_Add(dt, cmd);
            //清空dt
            dt.Clear();
        }
        /// <summary>
        /// 1.修改批次 (+庫存)
        /// 2.單據作業存檔事件觸發，狀態為[修改] 
        /// </summary>           //修改前的批次資料暫存//批次異動dt(來源) //cmd(sqltrancetion)  //單據名稱        //單據編號            //是否是bom         //客戶編號[銷貨、銷退]  //+倉庫編號[領料、入庫] //修改前的+倉庫編號[領料、入庫]
        internal void 進貨_Modify(DataTable temp_dt, DataTable dt, SqlCommand cmd, string FormName = "bShopd", string KeyValue = "", bool isBom = false, string Cuno = null, string Stno = null, string temp_Stno = null) 
        {
            //清除批號與廠商編號為空的Row
            F.過濾(dt);
            //如果為[領料、入庫]作業會帶入修改前的倉庫變號至[批次資料暫存(temp_dt)]
            if (temp_Stno != null) 強制填入(temp_dt, "stno", temp_Stno);
            //如果為[領料/入庫]作業會帶入單據的倉庫編號
            if (Stno != null) 強制填入(dt, "stno", Stno);
            //如果為[銷貨、銷退]作業會帶入單據的客戶編號
            if (Cuno != null) 強制填入(dt, "cuno", Cuno);
            //將[批次異動dt]都填入批次編號(bno)
            F.Bno_get(dt, cmd, FormName); 
            //先扣掉修改前的數量
            F.BatchStock_Subtract(temp_dt,cmd);
            //刪除掉修改前的批次異動資料
            F.BatchProcess_Delete(temp_dt, cmd, FormName);
            //如果 [dt] 是 Bom
            if (isBom)
                F.BatchBomProcess_Insert(temp_dt, dt, cmd, FormName, KeyValue);
            else//如果是 [批次異動dt] 明細
                F.BatchProcess_Insert(dt, cmd, FormName, KeyValue);
            //增加批次庫存數量
            F.BatchStock_Add(dt, cmd);
            //清空temp_dt
            temp_dt.Clear();
            //清空dt
            dt.Clear();
        }
        /// <summary>
        /// 1.刪除批次 (-庫存)
        /// 2.單據作業刪除事件觸發 
        /// </summary>            //批次資料暫存(來源) //cmd(sqltrancetion)  //單據名稱         //單據編號           //修改前的+倉庫編號[領料、入庫]
        internal void 進貨_Delete(DataTable temp_dt, SqlCommand cmd, string FormName = "bShopd", string KeyValue = "", string temp_Stno =null)
        {
            //如果為[領料、入庫]作業會帶入對應的倉庫變號至[批次資料暫存(temp_dt)]
            if (temp_Stno != null) 強制填入(temp_dt, "stno", temp_Stno);
            //刪除掉對應的批次異動資料
            F.BatchProcess_Delete(temp_dt, cmd, FormName);
            //減少批次庫存數量
            F.BatchStock_Subtract(temp_dt, cmd);

            temp_dt.Clear();
        }
        /// <summary>
        /// 與進貨_Append相反動作
        /// </summary>
        internal void 進退_Append(DataTable dt, SqlCommand cmd, string FormName = "rShopD", string KeyValue = "", bool isBom = false, string Cuno = null, string Stno = null) 
        {
            F.過濾(dt);
            if (Stno != null) 強制填入(dt, "stno", Stno);
            if (Cuno != null) 強制填入(dt, "cuno", Cuno);
            F.Bno_get(dt, cmd, FormName);
            if (isBom)
                F.BatchBomProcess_Insert(null, dt, cmd, FormName, KeyValue);
            else
                F.BatchProcess_Insert(dt, cmd, FormName, KeyValue);
            F.BatchStock_Subtract(dt, cmd);

            dt.Clear();

        }
        /// <summary>
        /// 與進貨_Modify相反動作
        /// </summary>
        internal void 進退_Modify(DataTable temp_dt, DataTable dt, SqlCommand cmd, string FormName = "rShopD", string KeyValue = "", bool isBom = false,string Cuno = null,string Stno=null,string temp_Stno=null) 
        {
            F.過濾(dt);
            if (temp_Stno != null) 強制填入(temp_dt, "stno", temp_Stno);
            if (Stno != null) 強制填入(dt, "stno", Stno);
            if (Cuno != null) 強制填入(dt, "cuno", Cuno);
            F.Bno_get(dt, cmd, FormName); 
            F.BatchStock_Add(temp_dt, cmd);
            F.BatchProcess_Delete(temp_dt, cmd, FormName);
            if (isBom)
                F.BatchBomProcess_Insert(temp_dt, dt, cmd, FormName, KeyValue);
            else
                F.BatchProcess_Insert(dt, cmd, FormName, KeyValue);
            F.BatchStock_Subtract(dt, cmd);

            temp_dt.Clear();
            dt.Clear();
        }
        /// <summary>
        /// 與進貨_Delete相反動作
        /// </summary>
        internal void 進退_Delete(DataTable temp_dt, SqlCommand cmd, string FormName = "rShopD", string KeyValue = "", string temp_Stno = null)
        {
            if (temp_Stno != null) 強制填入(temp_dt, "stno", temp_Stno);
            F.BatchProcess_Delete(temp_dt, cmd, FormName);
            F.BatchStock_Add(temp_dt, cmd);

            temp_dt.Clear();
        }
        internal void 強制填入(DataTable dt, string ColumnName, string values)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][ColumnName] = values;
            }
        }
        /// <summary>
        /// 入庫作業，組裝品是扣子階，所以存檔時要把組合品的明細拿掉，否則往後會扣到本體。
        /// </summary>                           //單據明細      //批次異動過程資料  //暫存批次異動過程資料
        internal DataTable 刪除相對應之組合品明細(DataTable GaD, DataTable dt, DataTable dtTemp = null)
        {
            string bomrec = "";
            int index1 = -1, index2 = -1;
            //宣告，準備過濾組裝品的單據明細
            DataTable ReturnDt = dt.Copy();
            //根據單據跑迴圈
            for (int i = 0; i < GaD.Rows.Count; i++)
            {
                //如果該筆單據明細為組裝品
                if (GaD.Rows[i]["ItTrait"].ToString() == "2")
                {
                    //取得該明細bomrec
                    bomrec = GaD.Rows[i]["bomrec"].ToString();
                    for (int k = 0; k <= ReturnDt.Rows.Count-1; k++)
                    {
                        //ReturnDt，刪除組裝品的單據明細
                        if (ReturnDt.Rows[k]["rec"].ToString() == bomrec)
                        {
                            ReturnDt.Rows.RemoveAt(k);
                            k = -1;
                        }
                    }
                    //ReturnDt，刪除組裝品的單據明細
                    //index1 = ReturnDt.MultiThreadFindIndex("rec", bomrec);
                    //if (index1 > -1)
                    //    ReturnDt.Rows.RemoveAt(index1);
                    ////dtTemp，刪除組裝品的單據明細    Note:暫存的批次異動過程也是要過濾組裝品，不然在修改時也是會扣錯數量
                    //if (dtTemp != null)
                    //{
                    //    index2 = dtTemp.MultiThreadFindIndex("rec", bomrec);
                    //    if (index2 > -1)
                    //        dtTemp.Rows.RemoveAt(index1);
                    //}

                }
            }
            //傳回沒組裝品的單據明細
            return ReturnDt.Copy();
        }
    }

    class BatchDocument : xDocuments 
    {
        protected override string MasterName
        {
            get { return ""; }
        }

        protected override string KeyName
        {
            get { return ""; }
        }
    }
}
