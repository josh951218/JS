using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class 寄庫資料瀏覽 : Formbase
    {
        public 寄庫資料瀏覽()
        {
            InitializeComponent();
        }

        private void panelT2_Paint(object sender, PaintEventArgs e)
        {
           // S_61.subMenuFm_2.FrmInStkInfo
        }

        string indate1 = "", indate2 = "", itno1 = "", itno2 = "", cuno1 = "", cuno2 = "", Memo = "";
        SqlParameterCollection SqlParameterCollection;
        private void btnBrow_Click(object sender, EventArgs e)
        {
           indate1 = ""; indate2 = ""; itno1 = ""; itno2 = ""; cuno1 = ""; cuno2 = ""; Memo = "";

           if(tabControlT1.SelectedTab == tabPage1)
           {
               bool InCheck = false, Oucheck = false;
               indate1 = 日期統一轉民國年(InDate.Text.Trim()); //判斷用
               indate2 = 日期統一轉民國年(InDate1.Text.Trim());
               itno1 =   ItNo.Text.Trim();
               itno2 =   ItNo1.Text.Trim();
               cuno1 =   CuNo.Text.Trim();
               cuno2 =   CuNo1.Text.Trim();
               Memo =  memo.Text.Trim();
               InCheck = In_CheckBox.Checked;
               Oucheck = Ou_CheckBox.Checked;
               檢查參數1是否大於參數2();

               #region 資料瀏覽
               DataTable dt_明細 = new DataTable();
               DataTable dt_數量 = new DataTable();
               DataTable dt_總數 = new DataTable();
               DataTable dt_客戶 = new DataTable();
               DataTable dt_產品 = new DataTable();

               string WhereStr = "", WhereStr2 = "", WhereStr3 = "";
               try
               {
                   using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                   using (SqlCommand cmd = cn.CreateCommand())
                   using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                   {
                       cmd.Parameters.Clear();

                       if (cuno1.Length > 0)
                       {
                           cmd.Parameters.AddWithValue("CuNo1", cuno1);
                           WhereStr  += " and 客戶編號 >=@CuNo1 ";
                           WhereStr2 += " and CuNo >=@CuNo1 ";
                           WhereStr3 += " and CuNo >=@CuNo1 ";
                       }
                       if (cuno2.Length > 0)
                       {
                           cmd.Parameters.AddWithValue("CuNo2", cuno2);
                           WhereStr  += " and 客戶編號 <=@CuNo2 ";
                           WhereStr2 += " and CuNo <=@CuNo2 ";
                           WhereStr3 += " and CuNo <=@CuNo2 ";
                       }
                       if (itno1.Length > 0)
                       {
                           cmd.Parameters.AddWithValue("ItNo1", itno1);
                           WhereStr  += " and 產品編號 >=@ItNo1 ";
                           WhereStr2 += " and Itno >=@ItNo1 ";
                           WhereStr3 += " and Itno >=@ItNo1 ";
                       }
                       if (itno2.Length > 0)
                       {
                           cmd.Parameters.AddWithValue("ItNo2", itno2);
                           WhereStr  += " and 產品編號 <=@ItNo2 ";
                           WhereStr2 += " and Itno <=@ItNo2 ";
                           WhereStr3 += " and Itno <=@ItNo2 ";
                       }
                       if (indate1.Length > 0)
                       {
                           cmd.Parameters.AddWithValue("indate1", indate1);//民國
                           WhereStr  += "  and 單據日期 >= @indate1 ";
                           WhereStr2 += " and indate >= @indate1 ";
                           WhereStr3 += " and Oudate >= @indate1 ";

                       }
                       if (indate2.Length > 0)
                       {
                           cmd.Parameters.AddWithValue("indate2", indate2);//民國
                           WhereStr  += " and 單據日期 <= @indate2 ";
                           WhereStr2 += " and indate <= @indate2 ";
                           WhereStr3 += " and Oudate <= @indate2 ";
                       }
                       if (Memo.Length > 0)
                       {
                           cmd.Parameters.AddWithValue("memo", Memo);//民國
                           WhereStr  += " and 備註說明 = @memo ";
                           WhereStr2 +=" and memo = @memo ";
                           //WhereStr3 +=" and memo = @memo ";
                       }
                       if (!InCheck)
                       {
                           WhereStr  += " and 單據!='寄庫' ";
                           WhereStr2 += " and 1=0";
                       }
                       if (!Oucheck)
                       {
                           WhereStr  += " and 單據!='領庫' ";
                           WhereStr3 += " and 1=0";
                       }
                       string OrderByStr = " order by inno,產品編號,單據日期,客戶編號,A.單據 asc";
                       #region 載入 dt_明細
                       string str_ = @"select 序號='',*,客戶簡稱=CuName1 from 
 (
  select  inno,ouno='',itpkgqty,單據='寄庫',單據憑證=inno,銷貨單號=sano,單據日期=SUBSTRING(indate,1,3) + '/'+SUBSTRING(indate,4,2)+ '/'+SUBSTRING(indate,6,2),單據日期1= SUBSTRING(indate1,1,4) + '/'+SUBSTRING(indate1,5,2)+ '/'+SUBSTRING(indate1,7,2),客戶編號=cuno,產品編號=itno,品名規格=itname,數量=inqty,單位=itunit,倉庫編號=stno,倉庫名稱=stname,備註說明=memo, 
  CASE ittrait
  WHEN '1' THEN '組合品'
  WHEN '2' THEN '組裝品'
  ELSE '單一商品'
  END as 產品組成 
  from instkd 
 UNION ALL
 select  inno,ouno,itpkgqty,單據='領庫',單據憑證=ouno,銷貨單號=sano,單據日期= SUBSTRING(oudate,1,3) + '/'+SUBSTRING(oudate,4,2)+ '/'+SUBSTRING(oudate,6,2),單據日期1= SUBSTRING(oudate1,1,4) + '/'+SUBSTRING(oudate1,5,2)+ '/'+SUBSTRING(oudate1,7,2),客戶編號=cuno,產品編號=itno,品名規格=itname,數量=ouqty,單位=itunit,倉庫編號=stno,倉庫名稱=stname,備註說明=memo,
  CASE ittrait
  WHEN '1' THEN '組合品'
  WHEN '2' THEN '組裝品'
  ELSE '單一商品'
  END as 產品組成
  from oustkd
 ) A INNER JOIN (SELECT cuno,cuname1 from cust) cust ON cuno = 客戶編號 ";
                       str_ = str_ + WhereStr + OrderByStr;
                       cmd.CommandText = str_;
                       da.Fill(dt_明細);

                       #endregion
                       #region   載入 dt_數量
                               str_ = "select distinct(B.inno),B.itno"
        + "  , (select sum(inqty) from InStkD where inno = B.inno and itno=B.itno" + WhereStr2 + ") as 寄庫總數"
        + "  , (select count(*)   from InStkD where inno = B.inno and itno=B.itno" + WhereStr2 + ") as 寄庫筆數"
        + "  , (select sum(ouqty) from OuStkD where inno = B.inno and itno=B.itno" + WhereStr3 + ") as 領庫總數"
        + "  , (select count(*)   from OuStkD where inno = B.inno and itno=B.itno" + WhereStr3 + ") as 領庫筆數"
        + "    from"
                                   //+"    (select inno,itno from InStkD UNION ALL select inno,itno from OuStkD group by inno,itno) A";
        + " ( select * from (select inno,itno,itno as 產品編號,indate as 單據日期,cuno as 客戶編號 ,單據='寄庫',備註說明=memo from InStkD UNION ALL select inno,itno,itno as 產品編號,oudate as 單據日期,cuno as 客戶編號,單據 = '領庫',備註說明=memo  from OuStkD) A  where 0=0 " + WhereStr + ")B";
                        cmd.CommandText = str_;
                        da.Fill(dt_數量);
                        #endregion
                       #region   載入 dt_總數
                        str_ =
"    select  "
+ "  ( select count(distinct(客戶編號)) from (select inno,itno as 產品編號,indate as 單據日期,cuno as 客戶編號 ,單據='寄庫',備註說明=memo from InStkD UNION ALL select inno,itno as 產品編號,oudate as 單據日期,cuno as 客戶編號,單據 = '領庫',備註說明=memo  from OuStkD) A  where 0=0 " + WhereStr + ") as 總客戶數"
+ " ,( select count(distinct(產品編號)) from (select inno,itno as 產品編號,indate as 單據日期,cuno as 客戶編號 ,單據='寄庫',備註說明=memo from InStkD UNION ALL select inno,itno as 產品編號,oudate as 單據日期,cuno as 客戶編號,單據 = '領庫',備註說明=memo  from OuStkD) A  where 0=0 " + WhereStr + ") as 總產品數"
+ " ,( select count(distinct(inno))     from InStkD  where 0=0 " + WhereStr2 + ") as 總寄庫單數"
+ " ,( select count(distinct(ouno))     from OuStkD  where 0=0 " + WhereStr3 + ") as 總領庫單數"
+" , ( select sum(inqty)                from InStkD  where 0=0 " + WhereStr2 + ") as 總寄庫數"
+ " ,( select sum(ouqty)                from OuStkD  where 0=0 " + WhereStr3 + ") as 總領庫數";
                       cmd.CommandText = str_;
                       da.Fill(dt_總數);
                       #endregion
                       #region   dt_客戶
                       str_ = 
  "  select distinct(B.客戶編號) "
+ "  , (select sum(inqty) from InStkD where  cuno=B.客戶編號 " + WhereStr2 + ") as 寄庫筆數"
+ "  , (select sum(ouqty) from OuStkD where  cuno=B.客戶編號 " + WhereStr3 + ") as 領庫筆數"
//+"    from ( select distinct(cuno)  from (select cuno from InStkD UNION ALL select cuno from OuStkD)A )B";
+ "  from ( select * from (select inno,itno,itno as 產品編號,indate as 單據日期,cuno as 客戶編號 ,單據='寄庫',備註說明=memo from InStkD UNION ALL select inno,itno,itno as 產品編號,oudate as 單據日期,cuno as 客戶編號,單據 = '領庫',備註說明=memo  from OuStkD) A  where 0=0 " + WhereStr + ")B";
                       cmd.CommandText = str_;
                       da.Fill(dt_客戶);
                       #endregion
                       #region   dt_產品
                       str_ =  
 "   select distinct(B.產品編號)"
+" , (select  sum(inqty)           from InStkD where  itno=B.itno " + WhereStr2 + " ) as 寄庫總數"
+" , (select  sum(ouqty)           from OuStkD where  itno=B.itno " + WhereStr3 + " ) as 領庫總數"
//+" from  ( select distinct(itno)   from (select itno from InStkD UNION ALL select itno from OuStkD)A )B";
+"   from ( select * from (select inno,itno,itno as 產品編號,indate as 單據日期,cuno as 客戶編號 ,單據='寄庫',備註說明=memo from InStkD UNION ALL select inno,itno,itno as 產品編號,oudate as 單據日期,cuno as 客戶編號,單據 = '領庫',備註說明=memo  from OuStkD) A  where 0=0 " + WhereStr + ")B";
                       cmd.CommandText = str_;
                       da.Fill(dt_產品);
                       SqlParameterCollection = cmd.Parameters;
                       #endregion
                   }
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.ToString());
               }
               
                       if (dt_明細.Rows.Count == 0)
                       {
                           MessageBox.Show("資料為空！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                           return;
                       }
                       //using (var frm = new 寄庫資料瀏覽_資料瀏覽())
                       //{
                       //    frm.dt_明細 = dt_明細.Copy();
                       //    frm.dt_數量 = dt_數量.Copy();
                       //    frm.dt_產品 = dt_產品.Copy();
                       //    frm.dt_客戶 = dt_客戶.Copy();
                       //    frm.dt_總數 = dt_總數.Copy();
                       //    frm.SqlParameterCollection = SqlParameterCollection;
                       //    //frm.明細組件SqlStr = 明細組件SqlStr;
                       //    frm.WhereStr = WhereStr;
                       //    frm.ShowDialog();
                       //}
                       this.OpemInfoFrom<寄庫資料瀏覽_資料瀏覽>(() =>
                            {
                                寄庫資料瀏覽_資料瀏覽 frm = new 寄庫資料瀏覽_資料瀏覽();
                                frm.dt_明細 = dt_明細.Copy();
                                frm.dt_數量 = dt_數量.Copy();
                                frm.dt_產品 = dt_產品.Copy();
                                frm.dt_客戶 = dt_客戶.Copy();
                                frm.dt_總數 = dt_總數.Copy();
                                frm.SqlParameterCollection = SqlParameterCollection;
                                //frm.明細組件SqlStr = 明細組件SqlStr;
                                frm.WhereStr = WhereStr;
                                return frm;
                            });
               #endregion
           }
           else if(tabControlT1.SelectedTab == tabPage2)
           {
               bool VisiableNonqtyEqualZero = checkBoxT1.Checked;
               indate1 = 日期統一轉民國年(Indate2.Text.Trim());
               itno1 = ItNo2.Text.Trim();
               itno2 = ItNo3.Text.Trim();
               cuno1 = CuNo2.Text.Trim();
               cuno2 = CuNo3.Text.Trim();
               檢查參數1是否大於參數2();
               #region 結餘表
               DataTable dt_ = new DataTable();
               #region 載入dt_
               try
               {
                   using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                   {
                       string str_ = @"select InStkD.*,InStk.cuname1,inStk.recordno as rdno from InStkD left join InStk on InStkD.inno = InStk.inno where 0=0 ";
                       using (SqlCommand cmd = cn.CreateCommand())
                       {
                           cmd.Parameters.Clear();
                           string WhereStr = "";
                           //WhereStr += " and Isnull(InStkD.nonqty,0) > 0 ";
                           if (cuno1.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("CuNo1", cuno1);
                               WhereStr += " and InStkD.cuno >=@CuNo1";
                           }
                           if (cuno2.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("CuNo2", cuno2);
                               WhereStr += " and InStkD.cuno <=@CuNo2";
                           }
                           if (itno1.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("ItNo1", itno1);
                               WhereStr += " and InStkD.itno >=@ItNo1";
                           }
                           if (itno2.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("ItNo2", itno2);
                               WhereStr += " and InStkD.itno <=@ItNo2";
                           }
                           if (indate1.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("indate1", indate1);//民國
                               WhereStr += " and InStkD.indate <= @indate1";
                           }
                           if(!VisiableNonqtyEqualZero)
                               WhereStr += " and nonqty != 0";
                           cmd.CommandText = str_ + WhereStr ;
                           using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                           {
                               da.Fill(dt_);
                           }
                       }
                   }
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.ToString());
               }
               #endregion
               if (dt_.Rows.Count == 0)
               {
                   MessageBox.Show("資料為空！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   return;
               }
               //using (var frm = new FrmInStkInfob())
               //{
               //    frm.dt = dt_.Copy();
               //    frm.ShowDialog();
               //}
               this.OpemInfoFrom<FrmInStkInfob>(() =>
                            {
                                FrmInStkInfob frm = new FrmInStkInfob();
                                frm.dt = dt_.Copy();
                                return frm;
                            });
               #endregion
           }
           else if (tabControlT1.SelectedTab == tabPage3) //異動表
           {
               //indate1 = 日期統一轉民國年(InDate4.Text.Trim());
               //indate2 = 日期統一轉民國年(InDate5.Text.Trim());
               indate1 = InDate4.Text.Trim();
               indate2 = InDate5.Text.Trim();
               itno1 = ItNo4.Text.Trim();
               itno2 = ItNo5.Text.Trim();
               cuno1 = CuNo4.Text.Trim();
               cuno2 = CuNo5.Text.Trim();
               檢查參數1是否大於參數2();

               #region 異動表
               DataTable dt_ = new DataTable();
               #region 載入dt_
               try
               {
                   using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                   {
                       string str_ = @"select InStkD.cuno,cust.cuname1,InStkD.itno,itname from InStkD inner join cust on InStkD.cuno = cust.cuno where 0=0 ";
                       using (SqlCommand cmd = cn.CreateCommand())
                       {
                           cmd.Parameters.Clear();
                           string WhereStr = "";
                           //WhereStr += " and Isnull(InStkD.nonqty,0) > 0 ";
                           if (cuno1.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("CuNo1", cuno1);
                               WhereStr += " and InStkD.cuno >=@CuNo1";
                           }
                           if (cuno2.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("CuNo2", cuno2);
                               WhereStr += " and InStkD.cuno <=@CuNo2";
                           }
                           if (itno1.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("ItNo1", itno1);
                               WhereStr += " and InStkD.itno >=@ItNo1";
                           }
                           if (itno2.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("ItNo2", itno2);
                               WhereStr += " and InStkD.itno <=@ItNo2";
                           }
                           if (indate1.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("indate1", 日期統一轉民國年(indate1));//民國
                               WhereStr += " and InStkD.indate >= @indate1";
                           }
                           if (indate2.Length > 0)
                           {
                               cmd.Parameters.AddWithValue("indate2", 日期統一轉民國年(indate2));
                               WhereStr += " and InStkD.indate <= @indate2";
                           }
                           cmd.CommandText = str_ + WhereStr + " group by InStkD.cuno,InStkD.itno,itname,cust.cuname1";
                           using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                           {
                               da.Fill(dt_);
                           }
                       }
                   }
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.ToString());
               }
               #endregion
               if (dt_.Rows.Count == 0)
               {
                   MessageBox.Show("資料為空！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   return;
               }
               //using (var frm = new S_61.S2.Frm異動表())
               //{
               //    if (indate1!="")
               //    frm.indate1 = int.Parse(indate1);
               //    if (indate2 != "")
               //    frm.indate2 = int.Parse(indate2);
               //    frm.dt = dt_.Copy();
               //    frm.ShowDialog();
               //}
               this.OpemInfoFrom<S_61.S2.Frm異動表>(() =>
                            {
                                S_61.S2.Frm異動表 frm = new S_61.S2.Frm異動表();
                                if (indate1 != "")
                                    frm.indate1 = int.Parse(indate1);
                                if (indate2 != "")
                                    frm.indate2 = int.Parse(indate2);
                                frm.dt = dt_.Copy();
                                return frm;
                            });
               #endregion
           }
           
        }

        private string 日期統一轉民國年(string str)
        {
            if (str == "")  return "";
            if (Common.User_DateTime == 2) //記錄使用者日期格式：1民國.2西元.
            {
                return (int.Parse(str) - 19110000).ToString();
            }
            return str;
        }

        private void 檢查參數1是否大於參數2()
        {
            if (cuno1.Length > 0 && cuno2.Length > 0)
            {
                if (cuno1.BigThen(cuno2))
                {
                    MessageBox.Show("起始客戶編號不得大於終止客戶編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (itno1.Length > 0 && itno2.Length > 0)
            {
                if (itno1.BigThen(itno2))
                {
                    MessageBox.Show("起始產品編號不得大於終止產品編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (indate1.Length > 0 && indate2.Length > 0)
            {
                if (indate1.BigThen(indate2))
                {
                    MessageBox.Show("起始寄庫日期不得大於終止寄庫日期！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void InDate_Validating(object sender, CancelEventArgs e)
        {
            var TextBox = ((TextBox)sender);
            if (TextBox.Text.Trim() == "") return;
            if (!((TextBox)sender).IsDateTime())
            {
                MessageBox.Show("您輸入的日期格是不正確!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBox.SelectAll();
                e.Cancel = true;
            }
        }

        private void CuNo_Validating(object sender, CancelEventArgs e)
        {
            var TextBox = ((TextBox)sender);
            if (((TextBox)sender).Text.Trim() == "") return;
            if (!BeingCuno(((TextBox)sender).Text.Trim()))   //如果找不到該筆客戶資料，就無法離開此textbox
            {
                using (var frm = new S_61.SOther.FrmCustb())
                {
                    frm.TSeekNo = ((TextBox)sender).Text.Trim();
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        ((TextBox)sender).Text = frm.TResult;
                }
                TextBox.SelectAll();
                e.Cancel = true;
            }
        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new S_61.SOther.FrmCustb())
            {
                frm.TSeekNo = ((TextBox)sender).Text.Trim();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                   ((Control)sender).Text = frm.TResult;
            }
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new S_61.SOther.FrmItemb())
            {
                frm.TSeekNo = ((TextBox)sender).Text.Trim();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                     ((Control)sender).Text = frm.TResult;
            }
        }

        private void ItNo_Validating(object sender, CancelEventArgs e)
        {
            var TextBox = ((TextBox)sender);
            if (TextBox.Text.Trim() == "") return;
            if (!BeingItno(((Control)sender).Text.Trim()))  
            {
                using (var frm = new S_61.SOther.FrmItemb())
                {
                    frm.TSeekNo = ((TextBox)sender).Text.Trim();
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        ((Control)sender).Text = frm.TResult;
                }
                TextBox.SelectAll();
                e.Cancel = true;
            }
        }

        private void memo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    memo.Text = frm.Memo.GetUTF8(60);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool BeingCuno(string cuno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                cn.Open();
                cm.Parameters.AddWithValue("cuno", cuno);
                cm.CommandText = "select * from cust where cuno = @cuno";
                da.Fill(dt_);
                if (dt_.Rows.Count == 0)
                    return false;
                return true;
            }
        }

        private bool BeingItno(string itno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                cn.Open();
                cm.Parameters.AddWithValue("itno", itno);
                cm.CommandText = "select * from item where itno = @itno";
                da.Fill(dt_);
                if (dt_.Rows.Count == 0)
                    return false;
                return true;
            }
        }

        private void 寄庫資料瀏覽_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                InDate.MaxLength = InDate1.MaxLength = Indate2.MaxLength = InDate4.MaxLength = InDate5.MaxLength = 7;
            }
            else
            {
                InDate.MaxLength = InDate1.MaxLength = Indate2.MaxLength = InDate4.MaxLength = InDate5.MaxLength = 8;
            }
        }








    }
}
