using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class 寄庫作業 : Formbase
    {

        JBS.JS.Instk Instk = new JBS.JS.Instk();
        DataTable instkD_dt = new DataTable();
        DataTable instkBom_dt = new DataTable();

        DataTable instkD_dt_扣庫存 = new DataTable();//副本，用在於修改編輯後，儲存時扣庫存用。
        DataTable instkBom_dt_扣庫存 = new DataTable();//副本，用在於修改編輯後，儲存時扣庫存用。

        decimal BomRec = 0;
        string CurrentInno = "", TextBefore = "", inmemo1 = "", appdate = "", appscno = "", edtdate = "", edtscno = "", 存檔狀態 = "";
        public 寄庫作業()
        {
            InitializeComponent();
            this.寄庫數量.Set庫存數量小數();
            this.包裝數量.Set庫存數量小數();
            InDate_TB.SetDateLength();
            this.品名規格.MaxInputLength = Common.Sys_ItNameLenth;
            Common.CheckGridViewUdf(this.Name, ref dataGridViewT1);
        }

        private void 寄庫作業_Load(object sender, EventArgs e)
        {
            InNo_TB.Focus();
            #region 載入  結構
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                cm.CommandText = "select 序號=0.0,產品組成='',結構編號='',ItNoUdf = '',* from instkD where 1=0";
                da.Fill(instkD_dt);
                cm.CommandText = "select * from instkBom where 1=0";
                da.Fill(instkBom_dt);
            }
            #endregion
            dataGridViewT1.DataSource = instkD_dt;
            dataGridView_changeAndShow("Last");
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            StnoDoubleOrValidatiog帶入倉庫資料("新增");
            新增或修改或複製按鈕最後處理("新增");
            StnoDoubleOrValidatiog帶入倉庫資料(Common.User_StkNo);//Common.User_StkNo = 記錄使用者預設倉庫
            InDate_TB.Text = Date.GetDateTime(Common.User_DateTime);
            CuNo_TB.Text = "";
            CuName1_TB.Text = "";
            CuPer1_TB.Text = "";
            CuTel1_TB.Text = "";
            InNo_TB.Text = "";
            inmemo1 = "";
            InMemo.Text = "";
            SaNo.Text = "";
            InDate_TB.Focus();
            序號.ReadOnly = true;
            倉庫名稱.ReadOnly = true;
            產品組成.ReadOnly = true;
        }

        private void gridAppend_Click(object sender, EventArgs e)
        {
            if (CuNo_TB.Text.Trim() == "")
            {
                CuNo_TB.Focus();
                MessageBox.Show("請輸入客戶編號!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dataGridViewT1.FirstDisplayedScrollingColumnIndex = 0;
            gridAppend.Focus();
            if (!dataGridViewT1.Rows.OfType<DataGridViewRow>().Any(r => r.Cells["產品編號"].Value.IsNullOrEmpty()))
            {
                GridInstkDAddRows(instkD_dt);
                //string str = (Common.Series == "74" || Common.Series == "72") ? "產品編號" : "訂單憑證";
                dataGridViewT1.CurrentCell = dataGridViewT1.Rows[dataGridViewT1.Rows.Count - 1].Cells["產品編號"];
                dataGridViewT1.CurrentRow.Selected = true;
            }
            //dataGridViewT1.FirstDisplayedScrollingRowIndex = instkD_dt.Rows.Count - 1;
            dataGridViewT1.Focus();
        }

        private void GridInstkDAddRows(DataTable instkD_dt)
        {
            DataRow dr = instkD_dt.NewRow();
            dr["inno"] = "";
            dr["sano"] = "寄庫開帳";
            dr["indate"] = "";
            dr["indate1"] = "";
            dr["cuno"] = "";
            dr["stno"] = StNo_TB.Text;
            dr["stname"] = StName.Text;
            dr["itno"] = "";
            dr["itname"] = "";
            dr["ittrait"] = 0;
            dr["itunit"] = "";
            dr["itpkgqty"] = 1;
            dr["inqty"] = 0;
            dr["qty"] = 0;
            dr["bomid"] = "";
            dr["bomrec"] = GetBomRec();
            dr["結構編號"] = dr["bomrec"].ToString();
            dr["recordno"] = 0;
            dr["nonqty"] = 0;
            instkD_dt.Rows.Add(dr);
            instkD_dt.AcceptChanges();
            get序號(instkD_dt);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Date.GetDateTime(Common.User_DateTime);
            string inno = "";
            string cuno = CuNo_TB.Text.Trim();
            string cuname1 = CuName1_TB.Text.Trim();
            string cuper1 = CuPer1_TB.Text.Trim();
            string cutel1 = CuTel1_TB.Text.Trim();
            string 民國年 = InDate_TB.Text.Substring(0, 3);
            string 西元年 = "";
            #region 選擇日期格式
            if (Common.User_DateTime == 1) //記錄使用者日期格式：1民國.2西元.
            {
                民國年 = InDate_TB.Text;
                西元年 = (int.Parse(InDate_TB.Text) + 19110000).ToString();
            }
            else
            {
                民國年 = (int.Parse(InDate_TB.Text) - 19110000).ToString();
                西元年 = InDate_TB.Text;
            }
            if (CuNo_TB.Text.Trim() == "")
            {
                CuNo_TB.Focus();
                MessageBox.Show("請輸入客戶編號!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (是否超過進銷存年度())
            { InDate_TB.Focus(); return; }
            #endregion
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            {
                cn.Open();

                #region 產生扣庫存用之暫存檔1.instkD_dt_扣庫存、2.instkBom_dt_扣庫存(編輯時發生)
                if (存檔狀態 == "修改")
                {
                    cm.Parameters.Clear();
                    instkD_dt_扣庫存.Clear();
                    instkBom_dt_扣庫存.Clear();
                    cm.Parameters.AddWithValue("inno", CurrentInno);
                    cm.CommandText = "select * from instkd where inno = @inno";
                    da.Fill(instkD_dt_扣庫存);
                    cm.CommandText = "select * from instkBom where inno = @inno";
                    da.Fill(instkBom_dt_扣庫存);
                }
                #endregion
                SqlTransaction transaction;
                transaction = cn.BeginTransaction("Transaction");
                cm.Transaction = transaction;//開始綁交易
                try
                {
                    #region  產生inno
                    if (InNo_TB.Text.Trim() != "")
                    {
                        inno = InNo_TB.Text.Trim();
                        if (存檔狀態 != "修改")
                        {
                            if (寄庫單號是否重複(inno))
                            {
                                InNo_TB.Focus();
                                MessageBox.Show("寄庫單號重複!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Instk.GetPkNumber<JBS.JS.Instk>(cm, InDate_TB.Text, ref InNo_TB);
                        inno = InNo_TB.Text;
                    }
                    #endregion
                    #region 先刪除 sql
                    cm.Parameters.Clear();
                    cm.Parameters.AddWithValue("inno", inno);
                    cm.CommandText = "delete InStk where  inno = @inno";
                    cm.ExecuteNonQuery();
                    cm.CommandText = "delete instkd where  inno = @inno";
                    cm.ExecuteNonQuery();
                    cm.CommandText = "delete InStkBOM where  inno = @inno";
                    cm.ExecuteNonQuery();
                    #endregion
                    #region 刪除記憶體DataTable，產品編號為空之資料
                    #region   刪除 instkD_dt
                    for (int i = instkD_dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (instkD_dt.Rows[i]["itno"].ToString().Trim() == "")
                        {
                            instkD_dt.Rows.RemoveAt(i);
                        }
                    }
                    #endregion
                    #region   刪除 instkBom_dt
                    for (int i = instkBom_dt.Rows.Count - 1; i >= 0; i--)
                    {
                        bool delete = true;
                        for (int j = 0; j < instkD_dt.Rows.Count; j++)
                        {
                            if (instkBom_dt.Rows[i]["bomrec"].ToString().Trim() == instkD_dt.Rows[j]["bomrec"].ToString().Trim())
                            {
                                delete = false;
                                break;
                            }
                        }
                        if (delete)
                        {
                            instkBom_dt.Rows.RemoveAt(i);
                        }
                    }
                    #endregion
                    #endregion
                    #region  新增 sql
                    #region insert into instk
                    cm.Parameters.Clear();
                    cm.Parameters.AddWithValue("inno", inno);
                    cm.Parameters.AddWithValue("indate", 民國年);
                    cm.Parameters.AddWithValue("indate1", 西元年);
                    cm.Parameters.AddWithValue("cuno", cuno);
                    cm.Parameters.AddWithValue("cuname1", cuname1);
                    cm.Parameters.AddWithValue("cutel1", cutel1);
                    cm.Parameters.AddWithValue("cuper1", cuper1);
                    cm.Parameters.AddWithValue("xa1no", "");
                    cm.Parameters.AddWithValue("Xa1Name", "");
                    cm.Parameters.AddWithValue("xa1par", 0);
                    cm.Parameters.AddWithValue("recordno", instkD_dt.Rows.Count.ToDecimal());
                    cm.Parameters.AddWithValue("invno", "");
                    cm.Parameters.AddWithValue("IsTrans", "");
                    cm.Parameters.AddWithValue("stno", StNo_TB.Text);
                    cm.Parameters.AddWithValue("stname", StName.Text);
                    cm.Parameters.AddWithValue("inmemo", InMemo.Text.Trim());
                    cm.Parameters.AddWithValue("inmemo1", inmemo1);
                    if (存檔狀態 != "修改")//新增或複製
                    {
                        cm.Parameters.AddWithValue("appdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cm.Parameters.AddWithValue("appscno", Common.User_Name);
                        cm.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cm.Parameters.AddWithValue("edtscno", Common.User_Name);
                    }
                    else //修改時
                    {
                        cm.Parameters.AddWithValue("appdate", appdate);
                        cm.Parameters.AddWithValue("appscno", appscno);
                        cm.Parameters.AddWithValue("edtdate", Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss"));
                        cm.Parameters.AddWithValue("edtscno", Common.User_Name);
                    }
                    cm.CommandText =
    @"insert into InStk(inno,sano,indate,indate1,cuno,cuname1,cutel1,cuper1,xa1no,Xa1Name,xa1par,recordno,invno,IsTrans,stno,stname,inmemo,inmemo1,appdate,appscno,edtdate,edtscno) 
values(@inno,'寄庫開帳',@indate,@indate1,@cuno,@cuname1,@cutel1,@cuper1,@xa1no,@Xa1Name,@xa1par,@recordno,@invno,@IsTrans,@stno,@stname,@inmemo,@inmemo1,@appdate,@appscno,@edtdate,@edtscno)";
                    cm.ExecuteNonQuery();
                    #endregion
                    #region intert into instkD
                    for (int i = 0; i < instkD_dt.Rows.Count; i++)
                    {
                        cm.Parameters.Clear();
                        cm.Parameters.AddWithValue("inno", inno);
                        cm.Parameters.AddWithValue("indate", 民國年);
                        cm.Parameters.AddWithValue("indate1", 西元年);
                        cm.Parameters.AddWithValue("cuno", cuno);
                        cm.Parameters.AddWithValue("sano", "寄庫開帳");
                        cm.Parameters.AddWithValue("stno", instkD_dt.Rows[i]["stno"].ToString().Trim());
                        cm.Parameters.AddWithValue("stname", instkD_dt.Rows[i]["stname"].ToString().Trim());
                        cm.Parameters.AddWithValue("itno", instkD_dt.Rows[i]["itno"].ToString().Trim());
                        cm.Parameters.AddWithValue("itname", instkD_dt.Rows[i]["itname"].ToString());
                        cm.Parameters.AddWithValue("ittrait", instkD_dt.Rows[i]["ittrait"].ToString().ToDecimal());
                        cm.Parameters.AddWithValue("itunit", instkD_dt.Rows[i]["itunit"].ToString().Trim());
                        cm.Parameters.AddWithValue("itpkgqty", instkD_dt.Rows[i]["itpkgqty"].ToString().ToDecimal());
                        cm.Parameters.AddWithValue("inqty", instkD_dt.Rows[i]["inqty"].ToString().ToDecimal());
                        cm.Parameters.AddWithValue("qty", instkD_dt.Rows[i]["inqty"].ToString().ToDecimal());
                        cm.Parameters.AddWithValue("bomid", inno + "1" + instkD_dt.Rows[i]["bomrec"].ToString().Trim().PadLeft(9, '0'));
                        cm.Parameters.AddWithValue("bomrec", instkD_dt.Rows[i]["bomrec"].ToString().Trim());
                        cm.Parameters.AddWithValue("recordno", (i + 1).ToDecimal());
                        cm.Parameters.AddWithValue("IsTrans", instkD_dt.Rows[i]["IsTrans"].ToString().Trim());
                        cm.Parameters.AddWithValue("nonqty", instkD_dt.Rows[i]["nonqty"].ToString().Trim());
                        cm.Parameters.AddWithValue("memo", instkD_dt.Rows[i]["memo"].ToString().Trim());
                        cm.CommandText = @"insert into InStkD(inno,sano,indate,indate1,cuno,stno,stname,itno,itname,ittrait,itunit,itpkgqty,inqty,qty,bomid,bomrec,recordno,IsTrans,nonqty,memo)
values (@inno,@sano,@indate,@indate1,@cuno,@stno,@stname,@itno,@itname,@ittrait,@itunit,@itpkgqty,@inqty,@qty,@bomid,@bomrec,@recordno,@IsTrans,@nonqty,@memo)";
                        cm.ExecuteNonQuery();
                    }
                    #endregion
                    #region intert into instkBom
                    for (int i = 0; i < instkBom_dt.Rows.Count; i++)
                    {
                        #region 補 DataGridView Validating Event 時填寫的值
                        instkBom_dt.Rows[i]["inno"] = inno;
                        instkBom_dt.Rows[i]["BomId"] = inno + "1" + instkBom_dt.Rows[i]["BomRec"].ToString().PadLeft(9, '0');
                        #endregion
                        cm.Parameters.Clear();
                        cm.Parameters.AddWithValue("InNo", instkBom_dt.Rows[i]["InNo"].ToString());
                        cm.Parameters.AddWithValue("BomID", instkBom_dt.Rows[i]["BomID"].ToString());
                        cm.Parameters.AddWithValue("BomRec", instkBom_dt.Rows[i]["BomRec"].ToString());
                        cm.Parameters.AddWithValue("itno", instkBom_dt.Rows[i]["itno"].ToString());
                        cm.Parameters.AddWithValue("itname", instkBom_dt.Rows[i]["itname"].ToString());
                        cm.Parameters.AddWithValue("itqty", instkBom_dt.Rows[i]["itqty"].ToString());
                        cm.Parameters.AddWithValue("itpareprs", instkBom_dt.Rows[i]["itpareprs"].ToString());
                        cm.Parameters.AddWithValue("itpkgqty", instkBom_dt.Rows[i]["itpkgqty"].ToString());
                        cm.Parameters.AddWithValue("itrec", instkBom_dt.Rows[i]["itrec"].ToString());
                        cm.Parameters.AddWithValue("itprice", instkBom_dt.Rows[i]["itprice"].ToString());
                        cm.Parameters.AddWithValue("itprs", instkBom_dt.Rows[i]["itprs"].ToString());
                        cm.Parameters.AddWithValue("itmny", instkBom_dt.Rows[i]["itmny"].ToString());
                        cm.Parameters.AddWithValue("itnote", instkBom_dt.Rows[i]["itnote"].ToString());
                        cm.Parameters.AddWithValue("IsTrans", instkBom_dt.Rows[i]["IsTrans"].ToString());
                        cm.Parameters.AddWithValue("itunit", instkBom_dt.Rows[i]["itunit"].ToString());
                        cm.CommandText =
@"insert into InStkBOM(InNo,BomID,BomRec,itno,itname,itqty,itpareprs,itpkgqty,itrec,itprice,itprs,itmny,itnote,IsTrans,itunit)   
  Values(@InNo,@BomID,@BomRec,@itno,@itname,@itqty,@itpareprs,@itpkgqty,@itrec,@itprice,@itprs,@itmny,@itnote,@IsTrans,@itunit) ";
                        cm.ExecuteNonQuery();
                    }
                    #endregion
                    #endregion
                    #region 庫存處理
                    //處理庫存Stock
                    if (存檔狀態 == "修改")
                    {
                        Instk.扣庫存(cm, instkD_dt_扣庫存, instkBom_dt_扣庫存);
                    }
                    Instk.加庫存(cm, instkD_dt, instkBom_dt);
                    transaction.Commit();
                    //更新產品檔庫存量Item
                    Instk.UpdateItemItStockQty(instkD_dt, instkBom_dt);
                    #endregion

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
            }
            放棄或儲存按鈕最後處理("儲存");
            //處理庫存
        }

        private bool 是否超過進銷存年度()
        {
            string 民國年, 西元年;
            if (Common.User_DateTime == 1) //記錄使用者日期格式：1民國.2西元.
            {
                民國年 = InDate_TB.Text;
                西元年 = (int.Parse(InDate_TB.Text) + 19110000).ToString();
            }
            else
            {
                民國年 = (int.Parse(InDate_TB.Text) - 19110000).ToString();
                西元年 = InDate_TB.Text;
            }
            var 月 = int.Parse(西元年.Substring(4, 2));
            var 日 = int.Parse(西元年.Substring(6, 2));

            if (int.Parse(西元年.Substring(0, 4)) > (Common.Sys_StkYear2 + 1) || int.Parse(西元年.Substring(0, 4)) < (Common.Sys_StkYear2))
            {
                MessageBox.Show("單據日期不超過進銷存年度兩年或小於進銷存年度!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (月 > 12 || 月 < 1 || 日 > 31 || 日 < 1)
            {
                MessageBox.Show("日期格式不正確!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            return false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            放棄或儲存按鈕最後處理("放棄");
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (SaNo.Text.IndexOf("寄庫") == -1) 
            {
                MessageBox.Show("此單據由銷貨轉寄庫產生，因此無法修改或刪除!");
                return;
            }
            if (Instk.IsExistDocument<JBS.JS.Instk>(InNo_TB.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBottom_Click(null, null);
                return;
            }
            
            if (InNo_TB.Text == "") return;//11

            if (Instk.IsModify<JBS.JS.Instk>(InNo_TB.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Instk.upModify1<JBS.JS.Instk>(InNo_TB.Text.Trim());//更新修改狀態1
                //刷新資料
                dataGridView_changeAndShow("this");
            }

            CuNo_TB.Focus();
            InNo_TB.Enabled = false;//修改時不能編輯inno

            新增或修改或複製按鈕最後處理("修改");

            序號.ReadOnly = true;
            倉庫名稱.ReadOnly = true;
            產品組成.ReadOnly = true;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            dataGridView_changeAndShow("next");
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            dataGridView_changeAndShow("previous");
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            dataGridView_changeAndShow("last");
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            dataGridView_changeAndShow("top");
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.ReadOnly) return;
            if (gridDelete.Focused || btnCancel.Focused) return;
            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;

            if (CurrentColumnName == "產品編號")
            {
                if (dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim() == "") return;//明細:客戶編號打錯才鎖欄位，如沒打則不鎖。 5
                #region 判斷是否存在此產品
                if (!是否存在產品編號(dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim()))
                {
                    using (var frm = new S_61.SOther.FrmItemb())
                    {
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK)
                        {
                            string itno = frm.TResult;
                            填入產品明細與產品組件(itno, e.RowIndex);
                        }
                    }
                    e.Cancel = true;
                    return;
                }
                #endregion
                if (TextBefore != dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim())
                    填入產品明細與產品組件(dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim(), e.RowIndex);
            }
            else if (CurrentColumnName == "倉庫編號")
            {
                #region 倉庫編號
                if (dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim() == "")
                    e.Cancel = true;

                if (TextBefore != dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim())
                {
                    if (填入倉庫明細(dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim(), e.RowIndex))
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = instkD_dt.Rows[e.RowIndex]["stno"].ToString();
                    }
                    else
                    {
                        using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Stkroom>())
                        {
                            frm.ShowDialog();
                            if (frm.DialogResult == DialogResult.OK)
                            {
                                string stno = frm.TResult;
                                填入倉庫明細(stno, e.RowIndex);
                            }
                            else
                                e.Cancel = true;
                        }
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "包裝數量") //12
            {
                #region 包裝數量
                try
                {
                    if (dataGridViewT1.EditingControl.Text.ToDecimal() == 0)
                    {
                        MessageBox.Show("包裝數量不得為0", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
                catch (Exception)
                {
                }
                #endregion
            }
            else if (CurrentColumnName == "寄庫數量")
            {
                #region 寄庫數量
                if (TextBefore != dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Trim())
                    instkD_dt.Rows[e.RowIndex]["inqty"] = dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToDecimal();
                instkD_dt.Rows[e.RowIndex]["nonqty"] = dataGridViewT1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToDecimal();
                #endregion
            }
        }

        private void dataGridViewT1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "倉庫編號")
            {
                TextBefore = dataGridViewT1["倉庫編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
            else if (dataGridViewT1.Columns[e.ColumnIndex].Name == "產品編號")
            {
                TextBefore = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (SaNo.Text.IndexOf("寄庫") == -1)
            {
                MessageBox.Show("此單據由銷貨轉寄庫產生，因此無法修改或刪除!");
                return;
            }
            if (Instk.IsExistDocument<JBS.JS.Instk>(InNo_TB.Text.Trim()) == false)
            {
                MessageBox.Show("此筆資料已被刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBottom_Click(null, null);
                return;
            }
            if (Instk.IsModify<JBS.JS.Instk>(InNo_TB.Text.Trim()) != false)
            {
                MessageBox.Show("此筆資料被其他使用者修改中", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            {
                cn.Open();
                SqlTransaction transaction;
                transaction = cn.BeginTransaction("Transaction");
                cm.Transaction = transaction;
                try
                {
                    if (是否存有領出紀錄(InNo_TB.Text)) return;
                    #region 寄庫刪除
                    cm.Parameters.AddWithValue("inno", InNo_TB.Text);
                    cm.CommandText = @"delete from instk where inno = @inno";
                    cm.ExecuteNonQuery();
                    cm.CommandText = @"delete from instkD where inno = @inno";
                    cm.ExecuteNonQuery();
                    cm.CommandText = @"delete from instkBom where inno = @inno";
                    cm.ExecuteNonQuery();
                    #endregion
                    #region 庫存處理
                    //處理庫存Stock
                    Instk.扣庫存(cm, instkD_dt, instkBom_dt);
                    transaction.Commit();
                    //更新產品檔庫存量Item
                    Instk.UpdateItemItStockQty(instkD_dt, instkBom_dt);
                    #endregion
                    if (!dataGridView_changeAndShow("previous"))
                        btnCancel_Click(null, null);//如果跳下一筆後，沒資料就視同放棄按鈕事件。
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
            }
        }

        private bool 是否存有領出紀錄(string inno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            {
                cm.Parameters.Clear();
                cm.Parameters.AddWithValue("inno", inno);
                cm.CommandText = "select count(*) from oustkD  where inno =@inno";
                cn.Open();
                if (cm.ExecuteScalar().ToInteger() > 0)
                {
                    MessageBox.Show("此寄入單號有存在領出紀錄，不得刪除", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
                else
                    return false;
            }
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (InNo_TB.Text == "") return;//11
            if (是否超過進銷存年度()) return;
            存檔狀態 = "複製";
            dataGridView_changeAndShow("this");
            新增或修改或複製按鈕最後處理("複製");
            InDate_TB.Text = InDate_TB.Text;
            CuNo_TB.Focus();
            InNo_TB.Clear();
            SaNo.Clear();
            SaNo.Text = "寄庫開帳";
            序號.ReadOnly = true;
            倉庫名稱.ReadOnly = true;
            產品組成.ReadOnly = true;
        }

        private void gridInsert_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridInsert.Focus();
                if (instkD_dt.Rows.Count > 0)
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    GridInstkD_Insert(instkD_dt, index);
                    //string str = (Common.Series == "74" || Common.Series == "72") ? "產品編號" : "訂單憑證";
                    dataGridViewT1.CurrentCell = dataGridViewT1.Rows[index].Cells["產品編號"];
                    dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
            }
        }

        private void GridInstkD_Insert(DataTable instkD_dt, int index)
        {
            DataRow dr = instkD_dt.NewRow();
            dr["inno"] = "";
            dr["sano"] = "寄庫開帳";
            dr["indate"] = "";
            dr["indate1"] = "";
            dr["cuno"] = "";
            dr["stno"] = StNo_TB.Text;
            dr["stname"] = StName.Text;
            dr["itno"] = "";
            dr["itname"] = "";
            dr["ittrait"] = 0;
            dr["itunit"] = "";
            dr["itpkgqty"] = 0;
            dr["inqty"] = 0;
            dr["qty"] = 0;
            dr["bomid"] = "";
            dr["bomrec"] = GetBomRec();
            dr["結構編號"] = dr["bomrec"].ToString();
            dr["recordno"] = 0;
            dr["nonqty"] = 0;
            dr["序號"] = index + 1;
            instkD_dt.Rows.InsertAt(dr, index);
            instkD_dt.AcceptChanges();
            get序號(instkD_dt);
        }

        private void gridDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridInsert.Focus();
                if (instkD_dt.Rows.Count > 0)
                {
                    int index = dataGridViewT1.SelectedRows[0].Index;
                    string inno_ = instkD_dt.Rows[index]["inno"].ToString();
                    decimal BomRec_ = instkD_dt.Rows[index]["bomrec"].ToString().ToDecimal();
                    if (是否存有領出紀錄(InNo_TB.Text)) return;
                    #region 刪除該筆Index之明細
                    for (int i = instkD_dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (BomRec_ == instkD_dt.Rows[i]["BomRec"].ToString().ToDecimal())
                        {
                            instkD_dt.Rows.RemoveAt(i);
                        }
                    }
                    #endregion
                    #region 刪除該筆Index之Bom
                    for (int i = instkBom_dt.Rows.Count - 1; i >= 0; i--)//由後往前減，因為RemoveAt 會刪除索引，造成DataRowsCount是動態縮減，因此要由後往前相減。
                    {
                        if (BomRec_ == instkBom_dt.Rows[i]["BomRec"].ToString().ToDecimal())
                            instkBom_dt.Rows.RemoveAt(i);
                    }
                    #endregion
                    if (instkD_dt.Rows.Count > 0)
                        dataGridViewT1.CurrentRow.Selected = true;
                }
                dataGridViewT1.Focus();
                //@@
            }
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (StNo_TB.ReadOnly == true || btnCancel.Focused) return;

            if (StNo_TB.TrimTextLenth() == 0)
            {
                StNo_TB.Clear();
                StName.Clear();
                e.Cancel = true;
                MessageBox.Show("倉庫編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!StnoDoubleOrValidatiog帶入倉庫資料(StNo_TB.Text.Trim())) //如果找不到該筆倉庫資料，就無法離開此textbox
            {
                using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Stkroom>())
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        string stno = frm.TResult;
                        StnoDoubleOrValidatiog帶入倉庫資料(stno);
                        單據單倉or多倉設定(Common.Sys_StNoMode);//單據單倉/多倉設定：1單倉 2多倉
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void 單據單倉or多倉設定(int Sys_StNoMode)//1單倉 2多倉
        {
            if (Common.Sys_StNoMode == 1)
            {
                for (int i = 0; i < instkD_dt.Rows.Count; i++) //刷成預設倉庫
                {
                    instkD_dt.Rows[i]["stname"] = StName.Text;
                    instkD_dt.Rows[i]["stno"] = StNo_TB.Text;
                }
            }
        }

        private void StNo_TB_DoubleClick(object sender, EventArgs e)
        {
            if (StNo_TB.ReadOnly == true) return;
            using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Stkroom>())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    string stno = frm.TResult;
                    StnoDoubleOrValidatiog帶入倉庫資料(stno);
                    單據單倉or多倉設定(Common.Sys_StNoMode);//單據單倉/多倉設定：1單倉 2多倉
                }
            }
        }

        private void gridStock_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                gridStock.Focus();
                using (S_61.subMenuFm_2.FrmSale_Stock frm = new S_61.subMenuFm_2.FrmSale_Stock())
                {
                    frm.ItNo = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                    frm.ShowDialog();
                    dataGridViewT1.Focus();
                }
            }
        }

        private void gridItDesp_Click(object sender, EventArgs e)
        {
            gridItDesp.Focus();
            using (JE.SOther.FrmDesp frm = new JE.SOther.FrmDesp(true, FormStyle.Mini))
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1)
                {
                    dataGridViewT1.Focus();
                    return;
                }
                frm.dr = instkD_dt.Rows[index];
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }

        private void gridPicture_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                if (dataGridViewT1.SelectedRows.Count > 0)
                {
                    pVar.PictureOpenForm(dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString());
                    dataGridViewT1.Focus();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (InNo_TB.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (寄庫作業列印 frm = new 寄庫作業列印())
            {
                frm.InNo = InNo_TB.Text.Trim();
                frm.CuNo = CuNo_TB.Text.Trim();
                frm.ShowDialog();
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (InNo_TB.Text.Trim() == "")
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new 寄庫作業瀏覽())
            {
                frm.SeekInno = InNo_TB.Text.Trim();
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    string inno_ = frm.ResultInno;
                    dataGridView_changeAndShow("this");
                }
            }
        }

        private void gridBomD_Click(object sender, EventArgs e)
        {

            if (dataGridViewT1.Rows.Count > 0)
            {
                gridBomD.Focus();
                string _trait = dataGridViewT1["產品組成", dataGridViewT1.CurrentRow.Index].Value.ToString();
                if (_trait != "組合品" && _trait != "組裝品")
                {
                    MessageBox.Show("只有組合品或組裝品可以編修組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridViewT1.Focus();
                    return;
                }

                using (FrmSale_Bom frm = new FrmSale_Bom())
                {
                    string rec = dataGridViewT1.SelectedRows[0].Cells["結構編號"].Value.ToString();
                    DataTable table = instkBom_dt.Clone();

                    for (int i = 0; i < instkBom_dt.Rows.Count; i++)
                    {
                        if (instkBom_dt.Rows[i]["bomrec"].ToString().Trim() == rec)
                        {
                            table.ImportRow(instkBom_dt.Rows[i]);
                            instkBom_dt.Rows.RemoveAt(i--);
                        }
                    }

                    table.AcceptChanges();
                    instkBom_dt.AcceptChanges();

                    frm.dtD = table.Copy();
                    frm.BomRec = rec;
                    frm.BoItNo1 = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString();
                    frm.BoItName1 = dataGridViewT1.SelectedRows[0].Cells["品名規格"].Value.ToString();
                    frm.grid = dataGridViewT1;
                    frm.上層Row = instkD_dt.Rows[dataGridViewT1.CurrentCell.RowIndex];
                    switch (frm.ShowDialog())
                    {
                        case DialogResult.OK:
                            if (frm.CallBack == "Money")
                            {
                                instkBom_dt.Merge(frm.dtD);
                                instkBom_dt.Rows[dataGridViewT1.SelectedRows[0].Index]["price"] = frm.Money.ToDecimal("f" + Common.MS);
                                dataGridViewT1.InvalidateRow(dataGridViewT1.SelectedRows[0].Index);
                                dataGridViewT1.Focus();
                                // SetRow_TaxPrice(instkBom_dt.Rows[datagridViewT1.SelectedRows[0].Index]);
                                // SetRow_Mny(instkBom_dt.Rows[datagridViewT1.SelectedRows[0].Index]);
                                // SetAllMny();
                                break;
                            }
                            else
                            {
                                instkBom_dt.Merge(frm.dtD);
                                instkBom_dt.AcceptChanges();
                                dataGridViewT1.Focus();
                                break;
                            }
                        case DialogResult.Cancel:
                            instkBom_dt.Merge(table);
                            instkBom_dt.AcceptChanges();
                            dataGridViewT1.Focus();
                            break;
                    }
                }
            }
        }

        private bool 是否存在產品編號(string itno_)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            {
                cn.Open();
                cm.Parameters.Clear();
                cm.Parameters.AddWithValue("itno", itno_);
                cm.CommandText = "select count(*) from item where itno =@itno or itnoudf  =@itno";
                if (cm.ExecuteScalar().ToInteger() > 0)
                    return true;
                else
                    return false;
            }
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnSave.Enabled == false) return;

            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= dataGridViewT1.Rows.Count) return;
            if (dataGridViewT1.Rows.Count == 0) return;

            var CurrentColumnName = dataGridViewT1.Columns[e.ColumnIndex].Name;

            if (CurrentColumnName == "產品編號")
            {
                #region 產品編號
                using (var frm = new S_61.SOther.FrmItemb())
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        string itno = frm.TResult;
                        填入產品明細與產品組件(itno, e.RowIndex);
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "倉庫編號")
            {
                #region 倉庫編號
                using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Stkroom>())
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        string stno = frm.TResult;
                        填入倉庫明細(stno, e.RowIndex);
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "單位")
            {
                #region 單位
                string 單位 = dataGridViewT1["單位", e.RowIndex].EditedFormattedValue.ToString();
                string itno_ = dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString();
                if (itno_ == "") return;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cn.Open();
                    cm.Parameters.Clear();
                    cm.Parameters.AddWithValue("itno", itno_);
                    cm.CommandText = "select itunit,itunitp,ItPkgQty from Item where itno = @itno";
                    var dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr["itunit"].ToString() == 單位)
                        {
                            instkD_dt.Rows[e.RowIndex]["itunit"] = dr["ItUnitp"].ToString(); //　ItUnitp　= 包裝單位
                            if (dr["ItPkgQty"].ToString().ToDecimal() == 0) //如果包裝數量是0
                            {
                                instkD_dt.Rows[e.RowIndex]["ItPkgQty"] = 1;//包裝數量
                            }
                            else
                                instkD_dt.Rows[e.RowIndex]["ItPkgQty"] = dr["ItPkgQty"].ToString().ToDecimal("f" + Common.Q);
                        }
                        else
                        {
                            instkD_dt.Rows[e.RowIndex]["itunit"] = dr["itunit"].ToString(); //itunit = 單位
                            instkD_dt.Rows[e.RowIndex]["ItPkgQty"] = 1;　//包裝數量
                        }
                    }
                }
                #endregion
            }
            else if (CurrentColumnName == "備註說明")
            {
                #region 備註說明
                using (var frm = new FrmSale_Memo())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (dataGridViewT1.EditingControl != null)
                            dataGridViewT1.EditingControl.Text = frm.Memo.GetUTF8(20);

                        instkD_dt.Rows[e.RowIndex]["memo"] = frm.Memo.GetUTF8(20);
                    }
                }
                dataGridViewT1.InvalidateRow(e.RowIndex);
                #endregion
            }

        }

        private void 填入產品明細與產品組件(string itno, int RowIndex)
        {
            TextBefore = itno;
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                #region 載入明細與挑整畫面
                #region 載入明細
                cm.Parameters.AddWithValue("itno", itno);
                cm.CommandText = "select itno,itname,ittrait,itpkgqty,ItSalUnit,ItUnitp,itunit,itnoudf  from item where itno =@itno or Itnoudf =@Itno ";
                da.Fill(dt_);
                if (dt_.Rows.Count == 0) return;
                instkD_dt.Rows[RowIndex]["itno"] = dt_.Rows[0]["itno"].ToString();
                instkD_dt.Rows[RowIndex]["itname"] = dt_.Rows[0]["itname"].ToString();
                instkD_dt.Rows[RowIndex]["產品組成"] = getIttrait(dt_.Rows[0]["ittrait"].ToString());
                instkD_dt.Rows[RowIndex]["ittrait"] = dt_.Rows[0]["ittrait"].ToString();
                instkD_dt.Rows[RowIndex]["itpkgqty"] = dt_.Rows[0]["itpkgqty"].ToString();
                instkD_dt.Rows[RowIndex]["ItNoUdf"] = dt_.Rows[0]["ItNoUdf"].ToString();
                #region 單位判斷　ItSalUnit
                if (dt_.Rows[0]["ItSalUnit"].ToString() == "1")//ItSalUnit = 銷貨常用單位 1=包裝單位  2=單位
                {
                    instkD_dt.Rows[RowIndex]["itunit"] = dt_.Rows[0]["ItUnitp"].ToString(); //　ItUnitp　= 包裝單位
                    if (dt_.Rows[0]["ItPkgQty"].ToString().ToDecimal() == 0) //如果包裝數量是0
                    {
                        instkD_dt.Rows[RowIndex]["ItPkgQty"] = 1;//包裝數量
                    }
                    else
                        instkD_dt.Rows[RowIndex]["ItPkgQty"] = dt_.Rows[0]["ItPkgQty"].ToString().ToDecimal("f" + Common.Q);
                }
                else
                {
                    instkD_dt.Rows[RowIndex]["itunit"] = dt_.Rows[0]["itunit"].ToString(); //itunit = 單位
                    instkD_dt.Rows[RowIndex]["ItPkgQty"] = 1;　//包裝數量
                }
                #endregion
                #endregion
                #region 調整明細之畫面(DataGridView)
                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = dt_.Rows[0]["itno"].ToString();
                dataGridViewT1.InvalidateRow(RowIndex);
                #endregion
                #endregion
                #region 清除該筆Index之組件 與 重新載入該筆Index之組件
                string BomRec = instkD_dt.Rows[RowIndex]["BomRec"].ToString();
                #region 清除該筆Index之組件
                for (int i = instkBom_dt.Rows.Count - 1; i >= 0; i--)//由後往前減，因為RemoveAt 會刪除索引，造成DataRowsCount是動態縮減，因此要由後往前相減。
                {
                    // string BomRec = (RowIndex + 1).ToString();
                    if (instkBom_dt.Rows[i]["BomRec"].ToString() == BomRec)
                        instkBom_dt.Rows.RemoveAt(i);
                }
                #endregion
                #region 重新載入該筆Index之組件
                dt_.Clear();
                cm.Parameters.Clear();
                cm.Parameters.AddWithValue("@boitno", itno);
                cm.CommandText = "select * from bomD where boitno = @boitno";
                da.Fill(dt_);
                for (int i = 0; i < dt_.Rows.Count; i++)
                {
                    string itrec = (i + 1).ToString();
                    // string BomRec = (RowIndex + 1).ToString();
                    DataRow dr = instkBom_dt.NewRow();
                    dr["InNo"] = "";    //存檔時會填入
                    dr["BomID"] = "";   //存檔時會填入 InNo + 1 + (9碼包含BomRec)
                    dr["BomRec"] = BomRec;
                    dr["itno"] = dt_.Rows[i]["itno"].ToString();
                    dr["itname"] = dt_.Rows[i]["itname"].ToString();
                    dr["itqty"] = dt_.Rows[i]["itqty"].ToDecimal();
                    dr["itpareprs"] = dt_.Rows[i]["itpareprs"].ToDecimal();
                    dr["itpkgqty"] = dt_.Rows[i]["itpkgqty"].ToDecimal();
                    dr["itrec"] = itrec;
                    dr["itprice"] = dt_.Rows[i]["itprs"].ToDecimal();
                    dr["itprs"] = dt_.Rows[i]["itprs"].ToDecimal();
                    dr["itmny"] = dt_.Rows[i]["itmny"].ToDecimal();
                    dr["itnote"] = dt_.Rows[i]["itnote"].ToString();
                    dr["itunit"] = dt_.Rows[i]["itunit"].ToString();
                    dr["IsTrans"] = "";
                    instkBom_dt.Rows.Add(dr);
                    instkBom_dt.AcceptChanges();
                }
                #endregion
                #endregion
            }
        }

        private bool 填入倉庫明細(string stno, int RowIndex)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                cm.Parameters.AddWithValue("stno", stno);
                cm.CommandText = "select stno,stname,stname,sttrait from StkRoom  where stno =@stno";
                da.Fill(dt_);
                if (dt_.Rows.Count == 0) return false;
                instkD_dt.Rows[RowIndex]["stno"] = dt_.Rows[0]["stno"].ToString();
                instkD_dt.Rows[RowIndex]["stname"] = dt_.Rows[0]["stname"].ToString();
                if (dataGridViewT1.EditingControl != null)
                    dataGridViewT1.EditingControl.Text = dt_.Rows[0]["stno"].ToString();
                dataGridViewT1.InvalidateRow(RowIndex);
                return true;
            }
        }

        private bool 寄庫單號是否重複(string inno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            {
                cn.Open();
                cm.Parameters.Clear();
                cm.Parameters.AddWithValue("inno", inno);
                cm.CommandText = "select count(*) from InStk where inno =@inno";
                if (cm.ExecuteScalar().ToInteger() > 0)
                    return true;
                else
                    return false;
            }
        }

        private void 放棄或儲存按鈕最後處理(string str)
        {
            instkD_dt.Clear();
            instkBom_dt.Clear();

            #region 版面控制
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name.Contains("grid"))
                    this.Controls[i].Enabled = false;
                else if (this.Controls[i].Name.Contains("TB"))
                {
                    ((JE.MyControl.TextBoxT)Controls[i]).ReadOnly = true;
                    ((JE.MyControl.TextBoxT)Controls[i]).Text = "";
                }
            }
            for (int i = 0; i < panelT1.Controls.Count; i++)
            {
                if (panelT1.Controls[i].Name.Contains("btnSave") || panelT1.Controls[i].Name.Contains("btnCancel"))
                    panelT1.Controls[i].Enabled = false;
                else
                    panelT1.Controls[i].Enabled = true;
            }
            if (str == "放棄") StName.Text = "";
            dataGridViewT1.ReadOnly = true;
            gridStock.Enabled = true;
            InNo_TB.Enabled = true; //修改時會不能編輯，在此恢復。
            #endregion
            BomRec = 0;
            #region 資料呈現
            if ((存檔狀態 == "新增" || 存檔狀態 == "複製") && str == "儲存")
            {
                dataGridView_changeAndShow("last");
            }
            else
            {
                dataGridView_changeAndShow("this");
                Instk.upModify0<JBS.JS.Instk>(InNo_TB.Text.Trim());//更新修改狀態0
            }
            InMemo.ReadOnly = true;
            #endregion
        }

        private void 新增或修改或複製按鈕最後處理(string str)
        {
            存檔狀態 = str;
            if (str == "新增")
            {
                instkD_dt.Clear();
                instkBom_dt.Clear();
                InNo_TB.Text = "";
                BomRec = 0;
            }
            else if (str == "修改" || str == "複製")
            {
                if (instkD_dt.Rows.Count == 0)
                    BomRec = 1;
                else if (instkD_dt.Rows.Count > 0)
                {
                    BomRec = GetMaxRec(instkD_dt);
                }
            }
            #region 版面控制
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name.Contains("grid"))
                    this.Controls[i].Enabled = true;
                else if (this.Controls[i].Name.Contains("TB"))
                    ((JE.MyControl.TextBoxT)Controls[i]).ReadOnly = false;
            }
            for (int i = 0; i < panelT1.Controls.Count; i++)
            {
                if (panelT1.Controls[i].Name.Contains("btnSave") || panelT1.Controls[i].Name.Contains("btnCancel"))
                    panelT1.Controls[i].Enabled = true;
                else
                    panelT1.Controls[i].Enabled = false;
            }
            InMemo.ReadOnly = false;
            dataGridViewT1.ReadOnly = false;
            this.自定編號.ReadOnly = true;
            #endregion
        }

        private decimal GetMaxRec(DataTable instkD_dt)
        {
            decimal big = 0;
            for (int i = 0; i < instkD_dt.Rows.Count; i++)
            {
                if (big < instkD_dt.Rows[i]["bomrec"].ToString().ToDecimal())
                    big = instkD_dt.Rows[i]["bomrec"].ToString().ToDecimal();
            }
            return big;
        }

        private object getIttrait(string ittrait)
        {
            switch (ittrait)
            {
                case "1": ittrait = "組合品"; break;
                case "2": ittrait = "組裝品"; break;
                case "3": ittrait = "單一商品"; break;
            }
            return ittrait;
        }

        private void textBoxT1_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new S_61.SOther.FrmCustb())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    CunoDoubleOrValidatiog帶入顧客資料(frm.TResult);
            }
        }

        private bool StnoDoubleOrValidatiog帶入倉庫資料(string stno_)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            {
                cn.Open();
                cm.Parameters.Clear();
                cm.Parameters.AddWithValue("stno", stno_);
                cm.CommandText = "select * from StkRoom  where stno =@stno";
                if (stno_ == "新增")
                    cm.CommandText = "select TOP  1 * from StkRoom ";
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    StNo_TB.Text = dr["stno"].ToString();
                    StName.Text = dr["stname"].ToString();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool CunoDoubleOrValidatiog帶入顧客資料(string cuno_)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                cn.Open();
                cm.Parameters.AddWithValue("cuno", cuno_);
                cm.CommandText = "select * from cust where cuno = @cuno";
                da.Fill(dt_);
                if (dt_.Rows.Count == 0)
                    return false;
                CuNo_TB.Text = dt_.Rows[0]["cuno"].ToString().GetUTF8(10);
                CuName1_TB.Text = dt_.Rows[0]["cuname1"].ToString();
                CuPer1_TB.Text = dt_.Rows[0]["cuper1"].ToString().GetUTF8(10);
                CuTel1_TB.Text = dt_.Rows[0]["cutel1"].ToString();
                return true;
            }
        }
        
        private bool dataGridView_changeAndShow(string str)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                string sql = "";
                cm.Parameters.AddWithValue("inno", CurrentInno);
                //cm.Parameters.AddWithValue("sano", "寄庫開帳");
                switch (str.ToLower())
                {
                    case "last": sql = "    select top 1 * from InStk order by inno desc"; break;
                    case "top": sql = "     select top 1 * from InStk order by inno asc";  break;
                    case "next": sql = "    select top 1 * from InStk where inno > @inno order by inno asc"; break;
                    case "previous": sql = "select top 1 * from InStk where inno < @inno order by inno desc"; break;
                    case "this": sql = "    select top 1 * from InStk where inno = @inno order by inno desc"; break;
                }
                cm.CommandText = sql;
                cn.Open();
                SqlDataReader reader = cm.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    var date = (Common.User_DateTime == 1) ? "" : "1";
                    InNo_TB.Text = reader["inno"].ToString();
                    SaNo.Text = reader["sano"].ToString();
                    InDate_TB.Text = reader["indate" + date].ToString();
                    CuNo_TB.Text = reader["cuno"].ToString();
                    CuPer1_TB.Text = reader["cuper1"].ToString();
                    CuTel1_TB.Text = reader["cutel1"].ToString();
                    CuName1_TB.Text = reader["cuname1"].ToString();
                    StNo_TB.Text = reader["stno"].ToString();
                    StName.Text = reader["stname"].ToString();
                    CurrentInno = reader["inno"].ToString(); //修改為新的INNO
                    InMemo.Text = reader["inmemo"].ToString();
                    inmemo1 = reader["inmemo1"].ToString();
                    appdate = reader["appdate"].ToString();//新增日期
                    appscno = reader["appscno"].ToString();//新增人員
                    edtdate = reader["edtdate"].ToString();//修改日期
                    edtscno = reader["edtscno"].ToString();//修改人員
                }
                else
                {
                    return false;
                }
                cn.Close();
                #region 載入 instk_dt (寄庫明細)
                cm.Parameters.Clear();
                cm.Parameters.AddWithValue("inno", CurrentInno);
                instkD_dt.Clear();
                instkBom_dt.Clear();
                cm.CommandText = @"
 select ( 
 case(ittrait)  
 when 1 then '組合品'  
 when 2 then '組裝品' 
 when 3 then '單一商品' 
 end) as 產品組成,
 BomRec as 結構編號 ,
 序號 = 0.0,
 ItNoUdf = (select top 1 itnoudf from item where item.itno = instkd.itno) ,
 * from instkd where inno = @inno order by recordno asc";
                da.Fill(instkD_dt);
                get序號(instkD_dt);
                cm.CommandText =
"select * from InStkBOM where inno = @inno order by bomrec";
                da.Fill(instkBom_dt);
                #endregion
                return true;
            }
        }

        private void get序號(DataTable instkD_dt)
        {
            for (int i = 0; i < instkD_dt.Rows.Count; i++)
            {
                instkD_dt.Rows[i]["序號"] = (i + 1);
            }
        }

        private void CuNo_TB_Validating(object sender, CancelEventArgs e)
        {
            if (CuNo_TB.ReadOnly || btnCancel.Focused)
                return;
            if (!CunoDoubleOrValidatiog帶入顧客資料(CuNo_TB.Text.Trim())) //如果找不到該筆客戶資料，就無法離開此textbox
            {
                using (var frm = new S_61.SOther.FrmCustb())
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        CunoDoubleOrValidatiog帶入顧客資料(frm.TResult);
                }
                e.Cancel = true;
            }
        }

        decimal GetBomRec()
        {
            BomRec++;
            return BomRec;
        }

        private void InNo_TB_Validating(object sender, CancelEventArgs e)
        {
            if (InNo_TB.ReadOnly == false && 寄庫單號是否重複(InNo_TB.Text.Trim()))
            {
                MessageBox.Show("此寄庫單號重複!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down && !dataGridViewT1.ReadOnly)
            {
                if (dataGridViewT1.Rows.Count == 0 && CuTel1_TB.Focused == true)
                {
                    gridAppend.PerformClick();
                    return true;
                }
                else if ((dataGridViewT1.Rows.Count > 0 && dataGridViewT1["產品編號", (dataGridViewT1.Rows.Count - 1)].EditedFormattedValue.ToString().Trim() != ""))
                    gridAppend.PerformClick();
            }
            else if (keyData == Keys.D1 | keyData == Keys.NumPad1 && btnAppend.Enabled)
            {
                btnAppend.PerformClick();
                return true;
            }
            else if (keyData == Keys.D2 | keyData == Keys.NumPad2 && btnModify.Enabled)
            {
                btnModify.PerformClick();
            }
            else if (keyData == Keys.D3 | keyData == Keys.NumPad3 && btnDelete.Enabled)
            {
                btnDelete.PerformClick();
            }
            else if (keyData == Keys.D4 | keyData == Keys.NumPad4 && btnBrow.Enabled)
            {
                btnBrow.PerformClick();
            }
            else if (keyData == Keys.D0 | keyData == Keys.NumPad0 | keyData == Keys.F11 && btnBrow.Enabled)
            {
                btnExit.PerformClick();
            }
            else if (keyData == Keys.Home && btnTop.Enabled)
            {
                btnTop.PerformClick();
            }
            else if (keyData == Keys.PageUp && btnPrior.Enabled)
            {
                btnPrior.PerformClick();
            }
            else if (keyData == Keys.PageDown && btnNext.Enabled)
            {
                btnNext.PerformClick();
            }
            else if (keyData == Keys.End && btnBottom.Enabled)
            {
                btnBottom.PerformClick();
            }
            else if (keyData == Keys.F9 && btnSave.Enabled)
            {
                btnSave.PerformClick();
            }
            else if (keyData == Keys.F4 && btnCancel.Enabled)
            {
                btnCancel.Focus();
                btnCancel.PerformClick();
            }
            else if (keyData == Keys.F2 && gridAppend.Enabled)
            {
                gridAppend_Click(null, null);
            }
            else if (keyData == Keys.F3 && gridDelete.Enabled)
            {
                gridDelete_Click(null, null);
            }
            else if (keyData == Keys.F5 && gridInsert.Enabled)
            {
                gridInsert_Click(null, null);
            }
            else if (keyData == Keys.F6 && gridItDesp.Enabled)
            {
                gridItDesp_Click(null, null);
            }
            else if (keyData == Keys.F7 && gridBomD.Enabled)
            {
                gridBomD_Click(null, null);
            }
            else if (keyData == Keys.F8 && gridStock.Enabled)
            {
                gridStock_Click(null, null);
            }
            //else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift") && gridTran.Enabled)
            //{
            //    gridTran_Click(null, null);
            //}
            //else if (keyData.ToString().StartsWith("F10") && keyData.ToString().EndsWith("Shift") && gridAllTrans.Enabled)
            //{
            //    gridAllTrans_Click(null, null);
            //}
            //else if (keyData.ToString().StartsWith("F11") && keyData.ToString().EndsWith("Shift") && gridItBuyPrice.Enabled)
            //{
            //    gridItBuyPrice_Click(null, null);
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CuTel1_TB_Validating(object sender, CancelEventArgs e)
        {
            if (存檔狀態 == "新增" && instkD_dt.Rows.Count == 0)
            {
                gridAppend.PerformClick();
            }
        }

        private void dataGridViewT1_MouseClick(object sender, MouseEventArgs e)
        {
            if (存檔狀態 == "新增" && instkD_dt.Rows.Count == 0)
            {
                gridAppend_Click(null, null);
            }
        }

        private void InDate_TB_Validating(object sender, CancelEventArgs e)//3
        {
            if (btnCancel.Focused) return;
            if (!InDate_TB.IsDateTime())
            {
                MessageBox.Show("您輸入的日期格是不正確!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void dataGridViewT1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.ReadOnly == true || btnCancel.Focused) return;

            if (dataGridViewT1["產品編號", e.RowIndex].EditedFormattedValue.ToString().Trim() == "") //7
            {
                int index = dataGridViewT1.SelectedRows[0].Index;
                if (instkD_dt.Rows.Count > 0 && instkD_dt.Rows[e.RowIndex]["產品組成"].ToString().Trim() != "")
                {
                    string inno_ = instkD_dt.Rows[index]["inno"].ToString();
                    decimal BomRec_ = instkD_dt.Rows[index]["bomrec"].ToString().ToDecimal();
                    if (是否存有領出紀錄(InNo_TB.Text)) return;
                    #region 刪除該筆Index之明細
                    for (int i = instkD_dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (BomRec_ == instkD_dt.Rows[i]["BomRec"].ToString().ToDecimal())
                        {
                            instkD_dt.Rows[i]["inno"] = "";
                            instkD_dt.Rows[i]["sano"] = "寄庫開帳";
                            instkD_dt.Rows[i]["indate"] = "";
                            instkD_dt.Rows[i]["indate1"] = "";
                            instkD_dt.Rows[i]["cuno"] = "";
                            instkD_dt.Rows[i]["stno"] = StNo_TB.Text;
                            instkD_dt.Rows[i]["stname"] = StName.Text;
                            instkD_dt.Rows[i]["itno"] = "";
                            instkD_dt.Rows[i]["itname"] = "";
                            instkD_dt.Rows[i]["ittrait"] = 1;
                            instkD_dt.Rows[i]["itunit"] = "";
                            instkD_dt.Rows[i]["itpkgqty"] = 1;
                            instkD_dt.Rows[i]["inqty"] = 0;
                            instkD_dt.Rows[i]["qty"] = 0;
                            instkD_dt.Rows[i]["bomid"] = "";
                            // instkD_dt.Rows[i]["bomrec"] = GetBomRec();
                            // instkD_dt.Rows[i]["結構編號"] = ;
                            // instkD_dt.Rows[i]["recordno"] = 0;
                            instkD_dt.Rows[i]["nonqty"] = 0;
                        }
                    }
                    #endregion
                    #region 刪除該筆Index之Bom
                    for (int i = instkBom_dt.Rows.Count - 1; i >= 0; i--)//由後往前減，因為RemoveAt 會刪除索引，造成DataRowsCount是動態縮減，因此要由後往前相減。
                    {
                        if (BomRec_ == instkBom_dt.Rows[i]["BomRec"].ToString().ToDecimal())
                            instkBom_dt.Rows.RemoveAt(i);
                    }
                    #endregion
                    if (instkD_dt.Rows.Count > 0)
                        dataGridViewT1.CurrentRow.Selected = true;
                }
            }
        }

        private void DetailMemo_Click(object sender, EventArgs e)
        {
            using (S1.Frm詳細備註 frm = new S1.Frm詳細備註())
            {
                frm.CanEdt = CuNo_TB.ReadOnly ? false : true;
                frm.memo1 = inmemo1;

                if (frm.ShowDialog() == DialogResult.OK) inmemo1 = frm.memo1;
            }
        }

        private void gridKeyMan_Click(object sender, EventArgs e)
        {
            if (InNo_TB.Text.Trim() == "")
                return;

            using (FrmSale_AppScNo frm = new FrmSale_AppScNo())
            {
                //新增人員
                frm.AName = appscno;
                frm.ATime = appdate;
                //修改人員
                frm.EName = edtscno;
                frm.ETime = edtdate;
                frm.ShowDialog();
            }
        }

        private void gridAddress_Click(object sender, EventArgs e)
        {
            Instk.Open<JBS.JS.Cust>(InNo_TB, reader =>
            {
                InMemo.Text = reader["CuNo"].ToString() + " ";
                InMemo.Text += reader["CuName1"].ToString() + " ";
                InMemo.Text += reader["CuAddr3"].ToString();
            });
        }

        private void SaMemo_DoubleClick(object sender, EventArgs e)
        {
            if (InMemo.ReadOnly)
                return;

            using (var frm = new FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    InMemo.Text = frm.Memo.GetUTF8(60);
                InMemo.SelectAll();
            }
        }



    }
}
