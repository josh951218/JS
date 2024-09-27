using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Threading.Tasks;

namespace S_61.SOther
{
    public partial class FrmItem_Inventory : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable temp = new DataTable();
        DataTable dt = new DataTable();
        decimal itemCount = 0;
        string 明細區_頭value = "產品編號", 明細區_尾value = "(以下空白)", 明細區_欄位對應value = "產品編號"; //Excel解析資料

        public FrmItem_Inventory()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            pVar.FrmItem_Inventory = this;
            enddate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
        }

        private void FrmItem_Inventory_Load(object sender, EventArgs e)
        {
            LoadItem();
            ItNo.Focus(); 
        }

        void LoadItem()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = " Select COUNT(*) from item ";
                    itemCount = cmd.ExecuteScalar().ToDecimal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        string GetStockKind(object obj)
        {
            if (obj.ToDecimal() == 1) return "庫存倉";
            else if (obj.ToDecimal() == 2) return "借出倉";
            else if (obj.ToDecimal() == 3) return "加工倉";
            else if (obj.ToDecimal() == 4) return "借入倉";
            else return "";
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (radio1.Checked)
            {
                Report1();
                return;
            }

            if (radio2.Checked)
            {
                Report2();
                return;
            }

            dt = new DataTable();
            temp = new DataTable();
            if (itemCount == 0)
            {
                MessageBox.Show("找不到任何資料，請重新輸入！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ItNo.Text.BigThen(ItNo1.Text))
            {
                MessageBox.Show("起始產品編號不可大於終止產品編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }
            if (StNo.Text.BigThen(StNo1.Text))
            {
                MessageBox.Show("起始倉庫編號不可大於終止倉庫編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }
            if (KiNo.Text.BigThen(KiNo1.Text))
            {
                MessageBox.Show("起始產品類別不可大於終止產品類別，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }

            #region 作誠 - 歷史明細表&歷史總表
            if (radio3.Checked || radio4.Checked || radio5.Checked)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();
                        SqlDataAdapter dd;
                        string 倉庫類別 = " in(";
                        if (ch1.Checked) 倉庫類別 += "1,";
                        if (ch2.Checked) 倉庫類別 += "2,";
                        if (ch3.Checked) 倉庫類別 += "3,";
                        if (ch4.Checked) 倉庫類別 += "4,";
                        倉庫類別 = 倉庫類別.Substring(0, 倉庫類別.Length - 1) + ")";

                        cmd.Parameters.AddWithValue("ItNo", ItNo.Text.Trim());
                        cmd.Parameters.AddWithValue("ItNo1", ItNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                        cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                        cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());

                        #region 撈期初
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.itqtyf,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                                    + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                                    + " from stock as b left join item as i on b.itno=i.itno left join stkroom as s on b.stno=s.stno where '0'='0'"
                                    + " and b.itqtyf != 0";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        dd.Fill(dt);
                        #endregion

                        #region 進貨
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from bshopd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.bsdate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bsdate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait != 1 ";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 進退
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rshopd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.bsdate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bsdate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait != 1 ";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 銷貨-組裝品 & 單一商品
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from saled as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.sadate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.sadate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 銷貨-組合品
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from saled as b left join salebom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.sadate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.sadate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion




                        #region 寄庫+組裝品 & 單一商品
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.inqty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from instkd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.indate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.indate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 寄庫+組合品
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.inqty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from instkd as b left join instkbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.indate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.indate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion





                        #region 領庫-組裝品 & 單一商品
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.ouqty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from oustkd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.oudate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.oudate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 領庫-組合品
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.ouqty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from oustkd as b left join oustkbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.oudate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.oudate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion







                        #region 銷退-組裝品 & 單一商品
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rsaled as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.sadate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.sadate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 銷退-組合品
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rsaled as b left join rsalebom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.sadate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.sadate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 領料 - 組裝品 & 單一商品 & 發料倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from drawd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.drdate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.drdate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 領料 - 組裝品 & 單一商品 & 入料倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoi,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from drawd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoi where '0'='0'"
                            + " and b.drdate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.drdate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.stnoi != ''"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 領料 - 組合品子件 & 發料倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from drawd as b left join drawbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.drdate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.drdate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 領料 - 組合品子件 & 入料倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoi,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from drawd as b left join drawbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stnoi where '0'='0'"
                            + " and b.drdate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.drdate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.stnoi != ''"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion





                        #region 入庫 - 單一商品&組裝品 入庫部分
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoi,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from garnerd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoi where '0'='0'"
                            + " and b.gadate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.gadate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 入庫 - 單一商品 扣料部分
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from garnerd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.gadate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.gadate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.stnoo != ''"
                            + " and b.ittrait = 3 ";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 入庫 - 組裝子件 扣料部分
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from garnerd as b left join garnbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.gadate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.gadate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.stnoo != ''"
                            + " and b.ittrait =2";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion





                        #region 調撥 - 單一商品&組裝品 撥入部分
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoi,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from allotd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoi where '0'='0'"
                            + " and b.aldate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.aldate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 調撥 - 組合品子件 撥入部分
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoi,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from allotd as b left join AlloBom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stnoi where '0'='0'"
                            + " and b.aldate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.aldate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 調撥 - 單一商品&組裝品 撥出部分
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from allotd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.aldate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.aldate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 調撥 - 組合品子件 撥出部分
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from allotd as b left join AlloBom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.aldate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.aldate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion




                        #region 調整 - 單一商品&組裝品
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from adjustd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.addate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.addate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借出 - 組裝品 & 單一商品 & 借出倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoi,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from lendd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoi where '0'='0'"
                            + " and b.ledate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.ledate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借出 - 組裝品 & 單一商品 & 庫存倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from lendd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.ledate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.ledate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借出 - 組合品子件 & 借出倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoi,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from lendd as b left join lendbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stnoi where '0'='0'"
                            + " and b.ledate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.ledate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借出 - 組合品子件 & 庫存倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from lendd as b left join lendbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.ledate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.ledate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion




                        //
                        #region 借出還入 - 組裝品 & 單一商品 & 借出倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoi,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rlendd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoi where '0'='0'"
                            + " and b.ledate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.ledate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借出還入 - 組裝品 & 單一商品 & 庫存倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rlendd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.ledate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.ledate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借出還入 - 組合品子件 & 借出倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoi,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rlendd as b left join rlendbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stnoi where '0'='0'"
                            + " and b.ledate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.ledate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoi <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借出還入 - 組合品子件 & 庫存倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rlendd as b left join rlendbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.ledate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.ledate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion
                        //




                        #region 借入 - 組裝品 & 單一商品 & 借入倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from borrd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.bodate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bodate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借入 - 組裝品 & 單一商品 & 庫存倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from borrd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.bodate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bodate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借入 - 組合品子件 & 借入倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from borrd as b left join borrbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.bodate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bodate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借入 - 組合品子件 & 庫存倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from borrd as b left join borrbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.bodate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bodate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion




                        //
                        #region 借入還出 - 組裝品 & 單一商品 & 借入倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rborrd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.bodate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bodate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借入還出 - 組裝品 & 單一商品 & 庫存倉庫
                        cmd.CommandText = " select 產品編號=b.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rborrd as b left join item as i on b.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.bodate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bodate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(2,3)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and b.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and b.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借入還出 - 組合品子件 & 借入倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stnoo,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rborrd as b left join rborrbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stnoo where '0'='0'"
                            + " and b.bodate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bodate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stnoo <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion

                        #region 借入還出 - 組合品子件 & 庫存倉庫
                        cmd.CommandText = " select 產品編號=bom.itno,品名規格=i.itname,單位=i.itunit,倉庫編號=b.stno,倉庫名稱=s.stname,倉庫類別=s.sttrait,庫存數量=(-1)*(bom.itqty/bom.itpareprs*bom.itpkgqty)*b.qty*b.itpkgqty,單位成本=i.itcost,成本=0.0,倉庫類別名稱='', "
                            + " 包裝數量=i.itpkgqty,包裝單位=i.itunitp,小單位=0.0,大單位=0.0"
                            + " from rborrd as b left join rborrbom as bom on b.bomid=bom.bomid left join item as i on bom.itno=i.itno left join stkroom as s on s.stno=b.stno where '0'='0'"
                            + " and b.bodate >= '" + Common.Sys_StkYear1 + "0101" + "'"
                            + " and b.bodate <= '" + Date.ToTWDate(enddate.Text.Trim()) + "'"
                            + " and b.ittrait in(1)";
                        if (ItNo.Text.Trim() != "")
                        {
                            if (radio5.Checked)
                                cmd.CommandText += " and bom.itno like '%'+@ItNo+'%'";
                            else
                                cmd.CommandText += " and bom.itno >=@ItNo";
                        }
                        if (ItNo1.Text.Trim() != "")
                            cmd.CommandText += " and bom.itno <=@ItNo1";
                        if (StNo.Text.Trim() != "")
                            cmd.CommandText += " and b.stno >=@StNo";
                        if (StNo1.Text.Trim() != "")
                            cmd.CommandText += " and b.stno <=@StNo1";
                        if (KiNo.Text.Trim() != "")
                            cmd.CommandText += " and i.kino >=@KiNo";
                        if (KiNo1.Text.Trim() != "")
                            cmd.CommandText += " and i.kino <=@KiNo1";
                        if (倉庫類別 != " in(")
                            cmd.CommandText += " and s.sttrait" + 倉庫類別;
                        dd = new SqlDataAdapter(cmd);
                        temp.Clear();
                        dd.Fill(temp);
                        dt.Merge(temp);
                        dt.AcceptChanges();
                        #endregion
                        //

                        //撈月平均成本
                        //cmd.CommandText = "select * from Itemcost";
                        //dd = new SqlDataAdapter(cmd);
                        //DataTable ItemCost = new DataTable();
                        //dd.Fill(ItemCost);////////////
                        string 月份 = "";
                        if (Date.ToTWDate(enddate.Text.Trim()).Substring(0, 3).ToDecimal() > Common.Sys_StkYear1)
                            月份 = (Date.ToTWDate(enddate.Text.Trim()).Substring(3, 2).ToDecimal() + 12).ToString().PadLeft(2, '0');
                        else
                            月份 = (Date.ToTWDate(enddate.Text.Trim()).Substring(3, 2).ToDecimal()).ToString().PadLeft(2, '0');

                        List<DataRow> item = dt.AsEnumerable().ToList();
                        List<String> itno_list = dt.AsEnumerable().OrderBy(r => r["產品編號"].ToString()).Select(r => r["產品編號"].ToString()).Distinct().ToList();
                        List<String> stno_list = dt.AsEnumerable().OrderBy(r => r["倉庫編號"].ToString()).Select(r => r["倉庫編號"].ToString()).Distinct().ToList();
                        DataRow dr, itrow, strow;
                        temp.Clear();

                        #region 歷史明細表
                        if (radio3.Checked)
                        {
                            itno_list.ForEach(r =>
                            {
                                itrow = item.Find(k => k["產品編號"].ToString() == r);
                                stno_list.ForEach(t =>
                                {
                                    strow = item.Find(k => k["倉庫編號"].ToString() == t);
                                    dr = temp.NewRow();
                                    dr["產品編號"] = r;
                                    dr["品名規格"] = itrow["品名規格"].ToString();
                                    dr["單位"] = itrow["單位"].ToString();
                                    dr["倉庫編號"] = t;
                                    dr["倉庫名稱"] = strow["倉庫名稱"].ToString();
                                    switch (strow["倉庫類別"].ToString())
                                    {
                                        case "1":
                                            dr["倉庫類別名稱"] = "庫存倉";
                                            break;
                                        case "2":
                                            dr["倉庫類別名稱"] = "借出倉";
                                            break;
                                        case "3":
                                            dr["倉庫類別名稱"] = "加工倉";
                                            break;
                                        case "4":
                                            dr["倉庫類別名稱"] = "借入倉";
                                            break;
                                    }
                                    dr["庫存數量"] = dt.AsEnumerable().ToList().Where(k => k["產品編號"].ToString() == r && k["倉庫編號"].ToString() == t).Sum(k => k["庫存數量"].ToDecimal());
                                    dr["包裝數量"] = itrow["包裝數量"].ToDecimal();
                                    dr["包裝單位"] = itrow["包裝單位"].ToString();
                                    if (dr["包裝數量"].ToDecimal() != 0)
                                    {
                                        dr["大單位"] = (int)(dr["庫存數量"].ToDecimal() / dr["包裝數量"].ToDecimal());
                                        dr["小單位"] = Math.Round(dr["庫存數量"].ToDecimal() % dr["包裝數量"].ToDecimal(), Common.Q, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        dr["小單位"] = dr["庫存數量"].ToDecimal();
                                    }

                                    if (rdAvgByAllStk.Checked)
                                    {
                                        cmd.Parameters["itno"].Value = r;
                                        cmd.CommandText = "select avgcost" + 月份 + " from itemcost where itno=@itno";
                                        dr["單位成本"] = cmd.ExecuteScalar().ToDecimal();
                                        //ItemCost.AsEnumerable().ToList().Find(k => k["itno"].ToString() == r) == null ? 0 : ItemCost.AsEnumerable().ToList().Find(k => k["itno"].ToString() == r)["avgcost" + 月份].ToDecimal();
                                    }
                                    else if (rdAvgByOneStk.Checked)
                                    {
                                        cmd.Parameters["itno"].Value = r;
                                        cmd.Parameters["stno"].Value = t;
                                        cmd.CommandText = "select avgcost" + 月份 + " from stkcost where itno=@itno and stno=@stno";
                                        dr["單位成本"] = cmd.ExecuteScalar().ToDecimal();
                                    }
                                    else
                                    {
                                        dr["單位成本"] = itrow["單位成本"].ToDecimal();
                                    }
                                    dr["成本"] = Math.Round(dr["庫存數量"].ToDecimal() * dr["單位成本"].ToDecimal(), Common.M, MidpointRounding.AwayFromZero);
                                    if (radio9.Checked)
                                    {
                                        if (dr["庫存數量"].ToDecimal() != 0)
                                        {
                                            temp.Rows.Add(dr);
                                            temp.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        temp.Rows.Add(dr);
                                        temp.AcceptChanges();
                                    }
                                });
                            });
                        }
                        #endregion
                        #region 歷史總表
                        if (radio4.Checked)
                        {
                            dt.Columns.Add("庫存倉數量", typeof(Decimal));
                            dt.Columns.Add("借出倉數量", typeof(Decimal));
                            dt.Columns.Add("加工倉數量", typeof(Decimal));
                            dt.Columns.Add("借入倉數量", typeof(Decimal));
                            temp = dt.Copy();
                            temp.Clear();
                            itno_list.ForEach(r =>
                            {
                                itrow = item.Find(k => k["產品編號"].ToString() == r);
                                dr = temp.NewRow();
                                dr["產品編號"] = r;
                                dr["品名規格"] = itrow["品名規格"].ToString();
                                dr["單位"] = itrow["單位"].ToString();
                                dr["庫存倉數量"] = dt.AsEnumerable().ToList().Where(k => k["倉庫類別"].ToDecimal() == 1 && k["產品編號"].ToString() == r).Sum(k => k["庫存數量"].ToDecimal());
                                dr["借出倉數量"] = dt.AsEnumerable().ToList().Where(k => k["倉庫類別"].ToDecimal() == 2 && k["產品編號"].ToString() == r).Sum(k => k["庫存數量"].ToDecimal());
                                dr["加工倉數量"] = dt.AsEnumerable().ToList().Where(k => k["倉庫類別"].ToDecimal() == 3 && k["產品編號"].ToString() == r).Sum(k => k["庫存數量"].ToDecimal());
                                dr["借入倉數量"] = dt.AsEnumerable().ToList().Where(k => k["倉庫類別"].ToDecimal() == 4 && k["產品編號"].ToString() == r).Sum(k => k["庫存數量"].ToDecimal());
                                dr["庫存數量"] = dr["庫存倉數量"].ToDecimal() + dr["借出倉數量"].ToDecimal() + dr["加工倉數量"].ToDecimal() + dr["借入倉數量"].ToDecimal();
                                if (rdAvgByAllStk.Checked)
                                {
                                    cmd.Parameters["itno"].Value = r;
                                    cmd.CommandText = "select avgcost" + 月份 + " from itemcost where itno=@itno";
                                    dr["單位成本"] = cmd.ExecuteScalar().ToDecimal();
                                    //ItemCost.AsEnumerable().ToList().Find(k => k["itno"].ToString() == r) == null ? 0 : ItemCost.AsEnumerable().ToList().Find(k => k["itno"].ToString() == r)["avgcost" + 月份].ToDecimal();
                                }
                                else
                                {
                                    dr["單位成本"] = itrow["單位成本"].ToDecimal();
                                }
                                dr["成本"] = Math.Round(dr["庫存數量"].ToDecimal() * dr["單位成本"].ToDecimal(), Common.M, MidpointRounding.AwayFromZero);
                                if (radio9.Checked)
                                {
                                    if (dr["庫存數量"].ToDecimal() != 0)
                                    {
                                        temp.Rows.Add(dr);
                                        temp.AcceptChanges();
                                    }
                                }
                                else
                                {
                                    temp.Rows.Add(dr);
                                    temp.AcceptChanges();
                                }
                            });
                        }
                        #endregion
                        #region 明細表(內含字元)
                        if (radio5.Checked)
                        {
                            cmd.CommandText = "select * from item where itno like '%'+@ItNo+'%'";
                            dd = new SqlDataAdapter(cmd);
                            DataTable likeitno = new DataTable();
                            dd.Fill(likeitno);
                            item = likeitno.AsEnumerable().ToList();
                            item.ForEach(r =>
                            {
                                var itno = itno_list.Find(k => k == r["itno"].ToString().Trim());
                                stno_list.ForEach(t =>
                                {
                                    strow = dt.AsEnumerable().ToList().Find(k => k["倉庫編號"].ToString() == t);
                                    if (itno == null)
                                    {
                                        if (radio8.Checked)
                                        {
                                            dr = temp.NewRow();
                                            dr["產品編號"] = r["itno"].ToString().Trim();
                                            dr["品名規格"] = r["itname"].ToString();
                                            dr["單位"] = r["itunit"].ToString();
                                            dr["倉庫編號"] = strow["倉庫編號"].ToString();
                                            dr["倉庫名稱"] = strow["倉庫名稱"].ToString();
                                            switch (dr["倉庫類別"].ToString())
                                            {
                                                case "1":
                                                    dr["倉庫類別名稱"] = "庫存倉";
                                                    break;
                                                case "2":
                                                    dr["倉庫類別名稱"] = "借出倉";
                                                    break;
                                                case "3":
                                                    dr["倉庫類別名稱"] = "加工倉";
                                                    break;
                                                case "4":
                                                    dr["倉庫類別名稱"] = "借入倉";
                                                    break;
                                            }
                                            dr["庫存數量"] = 0;
                                            dr["包裝數量"] = r["itpkgqty"].ToDecimal();
                                            dr["包裝單位"] = r["itunitp"].ToString();
                                            dr["大單位"] = 0;
                                            dr["小單位"] = 0;
                                            if (rdAvgByAllStk.Checked)
                                            {
                                                cmd.Parameters["itno"].Value = r["itno"].ToString().Trim();
                                                cmd.CommandText = "select avgcost" + 月份 + " from itemcost where itno=@itno";
                                                dr["單位成本"] = cmd.ExecuteScalar().ToDecimal();
                                                //dr["單位成本"] = ItemCost.AsEnumerable().ToList().Find(k => k["itno"].ToString() == r["itno"].ToString()) == null ? 0 : ItemCost.AsEnumerable().ToList().Find(k => k["itno"].ToString() == r["itno"].ToString())["avgcost" + 月份].ToDecimal();
                                            }
                                            else if (rdAvgByOneStk.Checked)
                                            {
                                                cmd.Parameters["itno"].Value = r["itno"].ToString().Trim();
                                                cmd.Parameters["stno"].Value = t;
                                                cmd.CommandText = "select avgcost" + 月份 + " from stkcost where itno=@itno and stno=@stno";
                                                dr["單位成本"] = cmd.ExecuteScalar().ToDecimal();
                                            }
                                            else
                                            {
                                                dr["單位成本"] = r["itcost"].ToDecimal();
                                            }
                                            dr["成本"] = Math.Round(dr["庫存數量"].ToDecimal() * dr["單位成本"].ToDecimal(), Common.M, MidpointRounding.AwayFromZero);
                                            temp.Rows.Add(dr);
                                            temp.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        dr = temp.NewRow();
                                        dr["產品編號"] = r["itno"].ToString().Trim();
                                        dr["品名規格"] = r["itname"].ToString();
                                        dr["單位"] = r["itunit"].ToString();
                                        dr["倉庫編號"] = strow["倉庫編號"].ToString();
                                        dr["倉庫名稱"] = strow["倉庫名稱"].ToString();
                                        switch (dr["倉庫類別"].ToString())
                                        {
                                            case "1":
                                                dr["倉庫類別名稱"] = "庫存倉";
                                                break;
                                            case "2":
                                                dr["倉庫類別名稱"] = "借出倉";
                                                break;
                                            case "3":
                                                dr["倉庫類別名稱"] = "加工倉";
                                                break;
                                            case "4":
                                                dr["倉庫類別名稱"] = "借入倉";
                                                break;
                                        }
                                        dr["庫存數量"] = dt.AsEnumerable().ToList().Where(k => k["產品編號"].ToString() == r["itno"].ToString() && k["倉庫編號"].ToString() == t).Sum(k => k["庫存數量"].ToDecimal());
                                        dr["包裝數量"] = r["itpkgqty"].ToDecimal();
                                        dr["包裝單位"] = r["itunitp"].ToString();
                                        if (dr["包裝數量"].ToDecimal() != 0)
                                        {
                                            dr["大單位"] = (int)(dr["庫存數量"].ToDecimal() / dr["包裝數量"].ToDecimal());
                                            dr["小單位"] = Math.Round(dr["庫存數量"].ToDecimal() % dr["包裝數量"].ToDecimal(), Common.Q, MidpointRounding.AwayFromZero);
                                        }
                                        else
                                        {
                                            dr["小單位"] = dr["庫存數量"].ToDecimal();
                                        }

                                        if (rdAvgByAllStk.Checked)
                                        {
                                            cmd.Parameters["itno"].Value = r["itno"].ToString().Trim();
                                            cmd.CommandText = "select avgcost" + 月份 + " from itemcost where itno=@itno";
                                            dr["單位成本"] = cmd.ExecuteScalar().ToDecimal();
                                            //dr["單位成本"] = ItemCost.AsEnumerable().ToList().Find(k => k["itno"].ToString() == r["itno"].ToString()) == null ? 0 : ItemCost.AsEnumerable().ToList().Find(k => k["itno"].ToString() == r["itno"].ToString())["avgcost" + 月份].ToDecimal();
                                        }
                                        else if (rdAvgByOneStk.Checked)
                                        {
                                            cmd.Parameters["itno"].Value = r["itno"].ToString().Trim();
                                            cmd.Parameters["stno"].Value = t;
                                            cmd.CommandText = "select avgcost" + 月份 + " from stkcost where itno=@itno and stno=@stno";
                                            dr["單位成本"] = cmd.ExecuteScalar().ToDecimal();
                                        }
                                        else
                                        {
                                            dr["單位成本"] = r["itcost"].ToDecimal();
                                        }
                                        dr["成本"] = Math.Round(dr["庫存數量"].ToDecimal() * dr["單位成本"].ToDecimal(), Common.M, MidpointRounding.AwayFromZero);
                                        temp.Rows.Add(dr);
                                        temp.AcceptChanges();
                                    }
                                });
                            });
                        }
                        #endregion
                        dt = temp.Copy();
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("找不到任何資料，請重新輸入！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        dd.Dispose();
                        cmd.Dispose();
                    }
                    this.OpemInfoFrom<FrmItem_Inventoryb>(() =>
                            {
                                FrmItem_Inventoryb frm = new FrmItem_Inventoryb();
                                frm.明細區_頭value = 明細區_頭value;
                                frm.明細區_尾value = 明細區_尾value;
                                frm.明細區_欄位對應value = 明細區_欄位對應value;
                                if (radio3.Checked)
                                {
                                    if (!checkBoxT1_含寄庫.Checked) 減去寄庫與領庫數量(dt);
                                    frm.dt = dt.Copy();
                                    frm.date3.Text = enddate.Text.Trim();
                                    frm.tabcontrol.SelectTab(2);
                                    frm.Text = "產品現有歷史庫存表(明細表)";
                                }
                                else if (radio4.Checked)
                                {
                                    if (!checkBoxT1_含寄庫.Checked) 減去寄庫與領庫數量(dt);
                                    frm.dt = dt.Copy();
                                    frm.date4.Text = enddate.Text.Trim();
                                    frm.tabcontrol.SelectTab(3);
                                    frm.Text = "產品現有歷史庫存表(總表)";
                                }
                                else if (radio5.Checked)
                                {
                                    if (!checkBoxT1_含寄庫.Checked) 減去寄庫與領庫數量(dt);
                                    if (radio9.Checked)
                                    {
                                        var rows = dt.AsEnumerable().Where(r => r["庫存數量"].ToDecimal() != 0);
                                        if (rows.Count() > 0) frm.dt = rows.CopyToDataTable();
                                        else
                                        {
                                            MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return frm;
                                        }
                                    }
                                    else
                                    {
                                        frm.dt = dt.Copy();
                                    }

                                    frm.date5.Text = enddate.Text.Trim();
                                    frm.tabcontrol.SelectTab(4);
                                    frm.Text = "產品現有歷史庫存表(明細表)";
                                }
                                return frm;
                            });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

            #endregion
        }

        private void enddate_Layout(object sender, LayoutEventArgs e)
        {
            enddate.Text = Date.GetDateTime(Common.User_DateTime, false);
        }

        private void ItNo_Click(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void StNo_Click(object sender, EventArgs e)
        {
           xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void KiNo_Click(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }

        private void radio1_CheckedChanged(object sender, EventArgs e)
        {
            if (radio3.Checked || radio4.Checked || radio5.Checked)
                radio6.Enabled = rdAvgByAllStk.Enabled = rdAvgByOneStk.Enabled = true;
            else
                radio6.Enabled = rdAvgByAllStk.Enabled = rdAvgByOneStk.Enabled = false;

            

            if (radio5.Checked)
            {
                lblT1.Text = "產品編號內含字元";
                lblT2.Text = "";
                ItNo1.Text = "";
                ItNo1.ReadOnly = true;
                KiNo.Text = KiNo1.Text = "";
                KiNo.ReadOnly = KiNo1.ReadOnly = true;
            }
            else
            {
                lblT1.Text = "開始產品編號";
                lblT2.Text = "結束產品編號";
                ItNo1.ReadOnly = false;
                KiNo.ReadOnly = KiNo1.ReadOnly = false;
            }

            lblT5.Visible = false;
            enddate.Visible = false;

            if (radio3.Checked)
            {
                //radio9.Checked = true;
                lblT5.Visible = enddate.Visible = true;
            }
            else if (radio4.Checked)
            {
                //radio9.Checked = true;
                lblT5.Visible = enddate.Visible = true;
                rdAvgByOneStk.Enabled = false;
                if (rdAvgByOneStk.Checked)
                    radio6.Checked = true;
            }
            else if (radio5.Checked)
            {
                lblT5.Visible = enddate.Visible = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void enddate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (enddate.Text.Trim() == "") return;
            if (!enddate.IsDateTime())
            {
                MessageBox.Show("日期格式錯誤，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                enddate.Focus();
                e.Cancel = true;
            }
        }

        void Report1()
        {
            if (ItNo.Text.BigThen(ItNo1.Text))
            {
                MessageBox.Show("起始產品編號不可大於終止產品編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }
            if (StNo.Text.BigThen(StNo1.Text))
            {
                MessageBox.Show("起始倉庫編號不可大於終止倉庫編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }
            if (KiNo.Text.BigThen(KiNo1.Text))
            {
                MessageBox.Show("起始產品類別不可大於終止產品類別，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }

            try
            {
                btnBrow.Enabled = false;
                DataTable temp = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = ""
                                    + " select stock.stno,stkroom.stname,stkroom.sttrait,stock.itno,item.itname,item.itunit,stock.itqty,item.itpkgqty,item.itunitp,item.itnoudf,包裝整數=0.0,包裝餘數=0.0,倉庫類別='' "
                                    + " from stock "
                                    + " left join item on stock.itno = item.itno "
                                    + " left join stkroom on stock.stno = stkroom.stno "
                                    + " where 0=0 ";

                    if (ch1.Checked == false) cmd.CommandText += " And stkroom.sttrait != 1";
                    if (ch2.Checked == false) cmd.CommandText += " And stkroom.sttrait != 2";
                    if (ch3.Checked == false) cmd.CommandText += " And stkroom.sttrait != 3";
                    if (ch4.Checked == false) cmd.CommandText += " And stkroom.sttrait != 4";
                    if (ItNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ItNo", ItNo.Text.Trim());
                        cmd.CommandText += " And Item.ItNo >= (@ItNo)";
                    }
                    if (ItNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ItNo1", ItNo1.Text.Trim());
                        cmd.CommandText += " And Item.ItNo <= (@ItNo1)";
                    }
                    if (StNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());
                        cmd.CommandText += " And Stock.StNo >= (@StNo)";
                    }
                    if (StNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@StNo1", StNo1.Text.Trim());
                        cmd.CommandText += " And Stock.StNo <= (@StNo1)";
                    }
                    if (KiNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@KiNo", KiNo.Text.Trim());
                        cmd.CommandText += " And Item.KiNo >= (@KiNo)";
                    }
                    if (KiNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@KiNo1", KiNo1.Text.Trim());
                        cmd.CommandText += " And Item.KiNo <= (@KiNo1)";
                    }
                    if (radio9.Checked)
                    {
                        cmd.CommandText += " And Stock.ItQty != 0";
                    }
                    da.Fill(temp);
                }

                if (temp.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    var itpkgqty = temp.Rows[i]["itpkgqty"].ToDecimal();
                    var sttrait = temp.Rows[i]["sttrait"].ToDecimal();
                    if (itpkgqty > 0)
                    {
                        temp.Rows[i]["包裝整數"] = (int)(temp.Rows[i]["itqty"].ToDecimal() / itpkgqty);
                        temp.Rows[i]["包裝餘數"] = (int)(temp.Rows[i]["itqty"].ToDecimal() % itpkgqty);
                    }
                    if (sttrait == 1) temp.Rows[i]["倉庫類別"] = "庫存倉";
                    else if (sttrait == 2) temp.Rows[i]["倉庫類別"] = "借出倉";
                    else if (sttrait == 3) temp.Rows[i]["倉庫類別"] = "加工倉";
                    else if (sttrait == 4) temp.Rows[i]["倉庫類別"] = "借入倉";
                }

                //using (S5.FrmStockInventory frm = new S5.FrmStockInventory())
                //{
                //    if (!checkBoxT1_含寄庫.Checked) 減去寄庫與領庫數量(temp);
                //    frm.明細區_頭value = 明細區_頭value;
                //    frm.明細區_尾value = 明細區_尾value;
                //    frm.明細區_欄位對應value = 明細區_欄位對應value;
                //    frm.dt = temp.Copy();
                //    frm.ShowDialog();
                //}
                this.OpemInfoFrom<S5.FrmStockInventory>(() =>
                            {
                                S5.FrmStockInventory frm = new S5.FrmStockInventory();
                                if (!checkBoxT1_含寄庫.Checked) 減去寄庫與領庫數量(temp);
                                frm.明細區_頭value = 明細區_頭value;
                                frm.明細區_尾value = 明細區_尾value;
                                frm.明細區_欄位對應value = 明細區_欄位對應value;
                                frm.dt = temp.Copy();
                                return frm;
                            });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnBrow.Enabled = true;
            }
        }

        void Report2()
        {
            if (ItNo.Text.BigThen(ItNo1.Text))
            {
                MessageBox.Show("起始產品編號不可大於終止產品編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }
            if (StNo.Text.BigThen(StNo1.Text))
            {
                MessageBox.Show("起始倉庫編號不可大於終止倉庫編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }
            if (KiNo.Text.BigThen(KiNo1.Text))
            {
                MessageBox.Show("起始產品類別不可大於終止產品類別，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }

            try
            {
                btnBrow.Enabled = false;
                DataTable temp = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = ""
                    + " select 庫存倉=0.0,借出倉=0.0,加工倉=0.0,借入倉=0.0,stock.stno,stkroom.stname,stkroom.sttrait,stock.itno,item.itname,item.itunit,ISNULL(itqtyf,0)itqtyf,ISNULL(itqty,0)itqty "
                    + " from stock"
                    + " left join item on stock.itno = item.itno"
                    + " left join stkroom on stock.stno = stkroom.stno"
                    + " where 0=0";

                    if (ch1.Checked == false) cmd.CommandText += " And stkroom.sttrait != 1";
                    if (ch2.Checked == false) cmd.CommandText += " And stkroom.sttrait != 2";
                    if (ch3.Checked == false) cmd.CommandText += " And stkroom.sttrait != 3";
                    if (ch4.Checked == false) cmd.CommandText += " And stkroom.sttrait != 4";

                    if (ItNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ItNo", ItNo.Text.Trim());
                        cmd.CommandText += " And Item.ItNo >= (@ItNo)";
                    }
                    if (ItNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ItNo1", ItNo1.Text.Trim());
                        cmd.CommandText += " And Item.ItNo <= (@ItNo1)";
                    }
                    if (StNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());
                        cmd.CommandText += " And Stock.StNo >= (@StNo)";
                    }
                    if (StNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@StNo1", StNo1.Text.Trim());
                        cmd.CommandText += " And Stock.StNo <= (@StNo1)";
                    }
                    if (KiNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@KiNo", KiNo.Text.Trim());
                        cmd.CommandText += " And Item.KiNo >= (@KiNo)";
                    }
                    if (KiNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@KiNo1", KiNo1.Text.Trim());
                        cmd.CommandText += " And Item.KiNo <= (@KiNo1)";
                    }
                    if (radio9.Checked)
                    {
                        cmd.CommandText += " And Stock.ItQty != 0";
                    }
                    da.Fill(temp);

                }

                if (temp.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dt = temp.AsEnumerable().GroupBy(r => r["itno"].ToString()).Select(g => g.First() as DataRow).CopyToDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var itno = dt.Rows[i]["ItNo"].ToString().Trim();
                    dt.Rows[i]["庫存倉"] = temp.Select("ItNo = '" + itno + "'").Where(r => r["StTrait"].ToDecimal() == 1).Sum(r => r["itqty"].ToDecimal());
                    dt.Rows[i]["借出倉"] = temp.Select("ItNo = '" + itno + "'").Where(r => r["StTrait"].ToDecimal() == 2).Sum(r => r["itqty"].ToDecimal());
                    dt.Rows[i]["加工倉"] = temp.Select("ItNo = '" + itno + "'").Where(r => r["StTrait"].ToDecimal() == 3).Sum(r => r["itqty"].ToDecimal());
                    dt.Rows[i]["借入倉"] = temp.Select("ItNo = '" + itno + "'").Where(r => r["StTrait"].ToDecimal() == 4).Sum(r => r["itqty"].ToDecimal());
                    dt.Rows[i]["itqty"] = temp.Select("ItNo = '" + itno + "'").Sum(r => r["itqty"].ToDecimal());
                }

                //using (S5.FrmStockInventory1 frm = new S5.FrmStockInventory1())
                //{
                //    if(!checkBoxT1_含寄庫.Checked) 減去寄庫與領庫數量(dt);
                //    frm.明細區_頭value = 明細區_頭value;
                //    frm.明細區_尾value = 明細區_尾value;
                //    frm.明細區_欄位對應value = 明細區_欄位對應value;
                //    frm.dt = dt.Copy();
                //    frm.ShowDialog();
                //}
                this.OpemInfoFrom<S5.FrmStockInventory1>(() =>
                            {
                                S5.FrmStockInventory1 frm = new S5.FrmStockInventory1();
                                if (!checkBoxT1_含寄庫.Checked) 減去寄庫與領庫數量(dt);
                                frm.明細區_頭value = 明細區_頭value;
                                frm.明細區_尾value = 明細區_尾value;
                                frm.明細區_欄位對應value = 明細區_欄位對應value;
                                frm.dt = dt.Copy();
                                return frm;
                            });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnBrow.Enabled = true;
            }
        }

        private void 減去寄庫與領庫數量(DataTable dt)
        {
            string sqlStr="";
            if (radio1.Checked) 
            {
                sqlStr = @"select * from (
select itno,stno,itqty=sum(itqty) from (
select itno,stno,itqty = sum(inqty*itpkgqty) from instkd where ittrait !=1 group by stno,itno
union all
select itno,stno,itqty = (-1) * sum(ouqty*itpkgqty) from OuStkD where ittrait !=1 group by stno,itno
union all
select InStkBOM.itno,InStkD.stno,itqty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where InStkD.ittrait = 1 group by  InStkD.stno,InStkBOM.itno
union all
select OuStkBOM.itno,OuStkD.stno,itqty = (-1) * sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where OuStkD.ittrait = 1 group by  OuStkD.stno,OuStkBOM.itno
)a
group by stno,itno
)b
where itqty!=0";
                using (DataTable ReduceDt = new DataTable())
                {
                    產生ReduceDt(ReduceDt, sqlStr);
                    int index = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        index = 獲得相減index(ReduceDt, dt.Rows[i]["itno"].ToString(), dt.Rows[i]["stno"].ToString());
                        if (index == -1) continue;
                        dt.Rows[i]["itqty"] = dt.Rows[i]["itqty"].ToDecimal() - ReduceDt.Rows[index]["itqty"].ToDecimal();
                    }
                }
            }
            else if (radio2.Checked)
            {
                sqlStr = @"
select itno,庫存倉=sum(庫存倉),借出倉=sum(借出倉),加工倉=sum(加工倉),借入倉=sum(借入倉) from (
select itno
,庫存倉 = (select sum(inqty*itpkgqty) from instkd inner join stkroom on InStkD.stno  = stkroom.stno where stkroom.sttrait ='1'  and instkd.itno = item.itno  )
,借出倉 = (select sum(inqty*itpkgqty) from instkd inner join stkroom on InStkD.stno  = stkroom.stno where stkroom.sttrait ='2'  and instkd.itno = item.itno  )
,加工倉 = (select sum(inqty*itpkgqty) from instkd inner join stkroom on InStkD.stno  = stkroom.stno where stkroom.sttrait ='3'  and instkd.itno = item.itno  )
,借入倉 = (select sum(inqty*itpkgqty) from instkd inner join stkroom on InStkD.stno  = stkroom.stno where stkroom.sttrait ='4'  and instkd.itno = item.itno  )
from item where ittrait >1 
union all
select itno
,庫存倉 = (-1) *(select sum(ouqty*itpkgqty) from OuStkD inner join stkroom on OuStkD.stno  = stkroom.stno where stkroom.sttrait ='1'  and OuStkD.itno = item.itno )
,借出倉 = (-1) *(select sum(ouqty*itpkgqty) from OuStkD inner join stkroom on OuStkD.stno  = stkroom.stno where stkroom.sttrait ='2'  and OuStkD.itno = item.itno )
,加工倉 = (-1) *(select sum(ouqty*itpkgqty) from OuStkD inner join stkroom on OuStkD.stno  = stkroom.stno where stkroom.sttrait ='3'  and OuStkD.itno = item.itno )
,借入倉 = (-1) *(select sum(ouqty*itpkgqty) from OuStkD inner join stkroom on OuStkD.stno  = stkroom.stno where stkroom.sttrait ='4'  and OuStkD.itno = item.itno )
from item where ittrait >1 
union all
select itno
,庫存倉 = (select qty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where stkroom.sttrait ='1' and InStkD.ittrait = 1 and InStkBOM.itno = item.itno)
,借出倉 = (select qty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where stkroom.sttrait ='2' and InStkD.ittrait = 1 and InStkBOM.itno = item.itno)
,加工倉 = (select qty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where stkroom.sttrait ='3' and InStkD.ittrait = 1 and InStkBOM.itno = item.itno)
,借入倉 = (select qty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where stkroom.sttrait ='4' and InStkD.ittrait = 1 and InStkBOM.itno = item.itno)
from item  
union all
select itno
,庫存倉 = (-1) * (select qty = sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where stkroom.sttrait ='1' and OuStkD.ittrait = 1 and OuStkBOM.itno = item.itno)
,借出倉 = (-1) *(select qty = sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where stkroom.sttrait ='2' and OuStkD.ittrait = 1 and OuStkBOM.itno = item.itno)
,加工倉 = (-1) *(select qty = sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where stkroom.sttrait ='3' and OuStkD.ittrait = 1 and OuStkBOM.itno = item.itno)
,借入倉 = (-1) *(select qty = sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where stkroom.sttrait ='4' and OuStkD.ittrait = 1 and OuStkBOM.itno = item.itno)
from item  
)a
where ( 庫存倉 is not null or 借出倉 is not null or 加工倉 is not null or 借入倉 is not null ) 
group by itno";
                using (DataTable ReduceDt = new DataTable())
                {
                    產生ReduceDt(ReduceDt, sqlStr);
                    int index=0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        index = 獲得相減index(ReduceDt,dt.Rows[i]["itno"].ToString());
                        if (index == -1) continue;
                        dt.Rows[i]["庫存倉"] = dt.Rows[i]["庫存倉"].ToDecimal() - ReduceDt.Rows[index]["庫存倉"].ToDecimal();
                        dt.Rows[i]["借出倉"] = dt.Rows[i]["借出倉"].ToDecimal() - ReduceDt.Rows[index]["借出倉"].ToDecimal();
                        dt.Rows[i]["加工倉"] = dt.Rows[i]["加工倉"].ToDecimal() - ReduceDt.Rows[index]["加工倉"].ToDecimal();
                        dt.Rows[i]["借入倉"] = dt.Rows[i]["借入倉"].ToDecimal() - ReduceDt.Rows[index]["借入倉"].ToDecimal();
                        dt.Rows[i]["itqty"] = dt.Rows[i]["庫存倉"].ToDecimal() + dt.Rows[i]["借出倉"].ToDecimal() + dt.Rows[i]["加工倉"].ToDecimal() + dt.Rows[i]["借入倉"].ToDecimal();
                    }
                }
            }
            else if (radio3.Checked) 
            {
                sqlStr = @"select * from (
select 產品編號 = itno,倉庫編號 = stno,庫存數量=sum(itqty) from (
select itno,stno,itqty = sum(inqty*itpkgqty) from instkd where ittrait !=1 and indate <=@截止日期 group by stno,itno
union all
select itno,stno,itqty = (-1) * sum(ouqty*itpkgqty) from OuStkD where ittrait !=1 and oudate <=@截止日期 group by stno,itno
union all
select InStkBOM.itno,InStkD.stno,itqty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where InStkD.ittrait = 1 and InStkD.indate <=@截止日期  group by  InStkD.stno,InStkBOM.itno
union all
select OuStkBOM.itno,OuStkD.stno,itqty = (-1) * sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where OuStkD.ittrait = 1 and OuStkD.oudate <=@截止日期 group by  OuStkD.stno,OuStkBOM.itno
)a
group by stno,itno
)b
where 庫存數量!=0";
                using (DataTable ReduceDt = new DataTable())
                {
                    產生ReduceDt(ReduceDt, sqlStr,enddate.Text.Trim());
                    int index = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        index = 獲得相減index(ReduceDt, dt.Rows[i]["產品編號"].ToString(), dt.Rows[i]["倉庫編號"].ToString());
                        if (index == -1) continue;
                        dt.Rows[i]["庫存數量"] = dt.Rows[i]["庫存數量"].ToDecimal() - ReduceDt.Rows[index]["庫存數量"].ToDecimal();
                        dt.Rows[i]["成本"] = Math.Round(dt.Rows[i]["庫存數量"].ToDecimal() * dt.Rows[i]["單位成本"].ToDecimal(), Common.M, MidpointRounding.AwayFromZero);
                    }
                }
            }
            else if (radio4.Checked)
            {
                sqlStr = @"select 產品編號=itno,庫存倉數量=sum(庫存倉),借出倉數量=sum(借出倉),加工倉數量=sum(加工倉),借入倉數量=sum(借入倉) from (
select itno
,庫存倉 = (select sum(inqty*itpkgqty) from instkd inner join stkroom on InStkD.stno  = stkroom.stno where stkroom.sttrait ='1'  and instkd.itno = item.itno and InStkD.indate <=@截止日期 )
,借出倉 = (select sum(inqty*itpkgqty) from instkd inner join stkroom on InStkD.stno  = stkroom.stno where stkroom.sttrait ='2'  and instkd.itno = item.itno and InStkD.indate <=@截止日期 )
,加工倉 = (select sum(inqty*itpkgqty) from instkd inner join stkroom on InStkD.stno  = stkroom.stno where stkroom.sttrait ='3'  and instkd.itno = item.itno and InStkD.indate <=@截止日期 )
,借入倉 = (select sum(inqty*itpkgqty) from instkd inner join stkroom on InStkD.stno  = stkroom.stno where stkroom.sttrait ='4'  and instkd.itno = item.itno and InStkD.indate <=@截止日期 )
from item where ittrait >1 
union all
select itno
,庫存倉 = (-1) *(select sum(ouqty*itpkgqty) from OuStkD inner join stkroom on OuStkD.stno  = stkroom.stno where stkroom.sttrait ='1'  and OuStkD.itno = item.itno and OuStkD.oudate <=@截止日期 )
,借出倉 = (-1) *(select sum(ouqty*itpkgqty) from OuStkD inner join stkroom on OuStkD.stno  = stkroom.stno where stkroom.sttrait ='2'  and OuStkD.itno = item.itno and OuStkD.oudate <=@截止日期 )
,加工倉 = (-1) *(select sum(ouqty*itpkgqty) from OuStkD inner join stkroom on OuStkD.stno  = stkroom.stno where stkroom.sttrait ='3'  and OuStkD.itno = item.itno and OuStkD.oudate <=@截止日期 )
,借入倉 = (-1) *(select sum(ouqty*itpkgqty) from OuStkD inner join stkroom on OuStkD.stno  = stkroom.stno where stkroom.sttrait ='4'  and OuStkD.itno = item.itno and OuStkD.oudate <=@截止日期 )
from item where ittrait >1 
union all
select itno
,庫存倉 = (select qty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where stkroom.sttrait ='1' and InStkD.ittrait = 1 and InStkBOM.itno = item.itno and InStkD.indate <=@截止日期 )
,借出倉 = (select qty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where stkroom.sttrait ='2' and InStkD.ittrait = 1 and InStkBOM.itno = item.itno and InStkD.indate <=@截止日期 )
,加工倉 = (select qty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where stkroom.sttrait ='3' and InStkD.ittrait = 1 and InStkBOM.itno = item.itno and InStkD.indate <=@截止日期 )
,借入倉 = (select qty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where stkroom.sttrait ='4' and InStkD.ittrait = 1 and InStkBOM.itno = item.itno and InStkD.indate <=@截止日期 )
from item  
union all
select itno
,庫存倉 = (-1) * (select qty = sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where stkroom.sttrait ='1' and OuStkD.ittrait = 1 and OuStkBOM.itno = item.itno and OuStkD.oudate <=@截止日期 )
,借出倉 = (-1) *(select qty = sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where stkroom.sttrait ='2' and OuStkD.ittrait = 1 and OuStkBOM.itno = item.itno  and OuStkD.oudate <=@截止日期 )
,加工倉 = (-1) *(select qty = sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where stkroom.sttrait ='3' and OuStkD.ittrait = 1 and OuStkBOM.itno = item.itno  and OuStkD.oudate <=@截止日期 )
,借入倉 = (-1) *(select qty = sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where stkroom.sttrait ='4' and OuStkD.ittrait = 1 and OuStkBOM.itno = item.itno  and OuStkD.oudate <=@截止日期 )
from item  
)a
where ( 庫存倉 is not null or 借出倉 is not null or 加工倉 is not null or 借入倉 is not null ) 
group by itno";
                using (DataTable ReduceDt = new DataTable())
                {
                    產生ReduceDt(ReduceDt, sqlStr, enddate.Text.Trim());
                    int index = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        index = 獲得相減index(ReduceDt, dt.Rows[i]["產品編號"].ToString());
                        if (index == -1) continue;
                        dt.Rows[i]["庫存倉數量"] = dt.Rows[i]["庫存倉數量"].ToDecimal() - ReduceDt.Rows[index]["庫存倉數量"].ToDecimal();
                        dt.Rows[i]["借出倉數量"] = dt.Rows[i]["借出倉數量"].ToDecimal() - ReduceDt.Rows[index]["借出倉數量"].ToDecimal();
                        dt.Rows[i]["加工倉數量"] = dt.Rows[i]["加工倉數量"].ToDecimal() - ReduceDt.Rows[index]["加工倉數量"].ToDecimal();
                        dt.Rows[i]["借入倉數量"] = dt.Rows[i]["借入倉數量"].ToDecimal() - ReduceDt.Rows[index]["借入倉數量"].ToDecimal();
                        dt.Rows[i]["庫存數量"] = dt.Rows[i]["庫存倉數量"].ToDecimal() + dt.Rows[i]["借出倉數量"].ToDecimal() + dt.Rows[i]["加工倉數量"].ToDecimal() + dt.Rows[i]["借入倉數量"].ToDecimal();
                        dt.Rows[i]["成本"] = Math.Round(dt.Rows[i]["庫存數量"].ToDecimal() * dt.Rows[i]["單位成本"].ToDecimal(), Common.M, MidpointRounding.AwayFromZero);
                    }
                }
            }
            else if (radio5.Checked)
            {
                sqlStr = @"select * from (
select 產品編號 = itno,倉庫編號 = stno,庫存數量=sum(itqty) from (
select itno,stno,itqty = sum(inqty*itpkgqty) from instkd where ittrait !=1 and indate <=@截止日期 and itno like '%'+@內含字元+'%' group by stno,itno
union all
select itno,stno,itqty = (-1) * sum(ouqty*itpkgqty) from OuStkD where ittrait !=1 and oudate <=@截止日期 and itno like '%'+@內含字元+'%' group by stno,itno
union all
select InStkBOM.itno,InStkD.stno,itqty = sum(instkd.inqty*instkd.itpkgqty*(InStkBOM.itqty*InStkBOM.itpkgqty/InStkBOM.itpareprs)) from InStkbom left join instkd on InStkBOM.BomID=InStkD.bomid inner join stkroom on InStkD.stno = stkroom.stno where InStkD.ittrait = 1 and InStkD.indate <=@截止日期 and InStkBOM.itno like '%'+@內含字元+'%'  group by  InStkD.stno,InStkBOM.itno
union all
select OuStkBOM.itno,OuStkD.stno,itqty = (-1) * sum(OuStkD.ouqty*Oustkd.itpkgqty*(OuStkBOM.itqty*OuStkBOM.itpkgqty/OuStkBOM.itpareprs)) from OuStkBOM left join OuStkD on OuStkBOM.BomID=OuStkD.bomid inner join stkroom on OuStkD.stno = stkroom.stno where OuStkD.ittrait = 1 and OuStkD.oudate <=@截止日期 and OuStkBOM.itno like '%'+@內含字元+'%' group by  OuStkD.stno,OuStkBOM.itno
)a
group by stno,itno
)b
where 庫存數量!=0";
                using (DataTable ReduceDt = new DataTable())
                {
                    產生ReduceDt(ReduceDt, sqlStr, enddate.Text.Trim(), ItNo.Text.Trim());
                    int index = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        index = 獲得相減index(ReduceDt, dt.Rows[i]["產品編號"].ToString(), dt.Rows[i]["倉庫編號"].ToString());
                        if (index == -1) continue;
                        var num1 = dt.Rows[i]["庫存數量"].ToDecimal();
                        var num2 = ReduceDt.Rows[index]["庫存數量"].ToDecimal();
                        var sum = num1 - num2;
                        dt.Rows[i]["庫存數量"] = dt.Rows[i]["庫存數量"].ToDecimal() - ReduceDt.Rows[index]["庫存數量"].ToDecimal();
                        dt.Rows[i]["成本"] = Math.Round(dt.Rows[i]["庫存數量"].ToDecimal() * dt.Rows[i]["單位成本"].ToDecimal(), Common.M, MidpointRounding.AwayFromZero);
                    }
                }
            }


        }

        private int 獲得相減index(DataTable SearchDt, string itno = "", string stno = "")
        {
            object lock_ = new object();
            int index = -1;
            if(radio1.Checked)
            {
                Parallel.For(0, SearchDt.Rows.Count, (i, loopState) =>
                {
                    if (SearchDt.Rows[i]["itno"].ToString() == itno && SearchDt.Rows[i]["stno"].ToString() == stno)
                    {
                        lock (lock_)
                        {
                            index = i;
                            loopState.Break();
                        }
                    }
                });
            }
            else if (radio2.Checked)
            {
                Parallel.For(0, SearchDt.Rows.Count, (i, loopState) =>
                {
                    if (SearchDt.Rows[i]["itno"].ToString() == itno)
                    {
                        lock (lock_)
                        {
                            index = i;
                            loopState.Break();
                        }
                    }
                });
            }
            if (radio3.Checked || radio5.Checked)
            {
                Parallel.For(0, SearchDt.Rows.Count, (i, loopState) =>
                {
                    if (SearchDt.Rows[i]["產品編號"].ToString() == itno && SearchDt.Rows[i]["倉庫編號"].ToString() == stno)
                    {
                        lock (lock_)
                        {
                            index = i;
                            loopState.Break();
                        }
                    }
                });
            }
            else if (radio4.Checked)
            {
                Parallel.For(0, SearchDt.Rows.Count, (i, loopState) =>
                {
                    if (SearchDt.Rows[i]["產品編號"].ToString() == itno)
                    {
                        lock (lock_)
                        {
                            index = i;
                            loopState.Break();
                        }
                    }
                });
            }
            return index;
        }

        private void 產生ReduceDt(DataTable ReduceDt, string SqlStr, string 截止日期 = "", string 內含字元 = "")
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                if (截止日期 != "")
                    cmd.Parameters.AddWithValue("截止日期", 日期統一轉民國年(截止日期));
                //if (內含字元 != "")
                    cmd.Parameters.AddWithValue("內含字元", 內含字元);
                cmd.CommandText = SqlStr;
                da.Fill(ReduceDt);
            }
        }

        private string 日期統一轉民國年(string str)
        {
            if (str == "") return "";
            if (Common.User_DateTime == 2) //記錄使用者日期格式：1民國.2西元.
            {
                return (int.Parse(str) - 19110000).ToString();
            }
            return str;
        }
    }
}
