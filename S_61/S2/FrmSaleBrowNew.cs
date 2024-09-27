using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmSaleBrowNew : Formbase, JBS.JS.IxOpen
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }
        [Obsolete("Don't use this", true)]
        public new string SeekNo;

        JBS.JS.Sale jSale;
        DataTable dtD = new DataTable();
        List<ButtonSmallT> list;
        string SQL = "";
        decimal TotalCount = -1;

        static object obj = new object();

        public FrmSaleBrowNew()
        {
            InitializeComponent();
            this.jSale = new JBS.JS.Sale();
            list = new List<ButtonSmallT>() { Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11 };

            this.備註說明.HeaderText = Common.Sys_MemoUdf;
            labelT21.Text = Common.Sys_MemoUdf;

            this.包裝數量.Set庫存數量小數();
            this.本幣單價.Set本幣金額小數();
            this.本幣稅前金額.Set本幣金額小數();
            this.本幣稅前單價.Set本幣金額小數();
            this.售價.Set銷貨單價小數();
            this.稅前金額.Set銷項金額小數();
            this.銷貨數量.Set庫存數量小數();
            this.銷貨日期.DataPropertyName = Common.User_DateTime == 1 ? "銷貨日期" : "銷貨日期1";
            this.tSadate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;

            dataGridViewT1.ReadOnly = false;
            for (int i = 0; i < dataGridViewT1.Columns.Count; i++)
            {
                if (dataGridViewT1.Columns[i].Name != this.備註說明.Name) dataGridViewT1.Columns[i].ReadOnly = true;
            }

            dataGridViewT1.DataSource = dtD.DefaultView;
            this.本幣單價.Visible = Common.User_SalePrice;
            this.本幣稅前金額.Visible = Common.User_SalePrice;
            this.本幣稅前單價.Visible = Common.User_SalePrice;
            this.售價.Visible = Common.User_SalePrice;
            this.稅前金額.Visible = Common.User_SalePrice;
            this.稅前單價.Visible = Common.User_SalePrice;

            this.textBoxT1.Visible = Common.User_SalePrice;
            this.textBoxT2.Visible = Common.User_SalePrice;
            this.textBoxT5.Visible = Common.User_SalePrice;
            this.textBoxT6.Visible = Common.User_SalePrice;
        }
        private void FrmSaleBrowNew_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            SQL = @"
            LEFT(saled.Sadate,3)+'/'+SUBSTRING(saled.Sadate,4,2)+'/'+RIGHT(saled.Sadate,2) 銷貨日期,
            LEFT(saled.sadate1,4)+'/'+SUBSTRING(saled.sadate1,5,2)+'/'+RIGHT(saled.sadate1,2) 銷貨日期1,
            產品組成= case when saled.ittrait=1 then '組合品' when saled.ittrait=2 then '組裝品' when saled.ittrait=3 then '單一商品' end,

            saled.saID, saled.sano, saled.Sadate, saled.sadate1,saled.stno,saled.orno, saled.itno, saled.itname, saled.ittrait, 
            saled.itunit, saled.itpkgqty, saled.qty, saled.price, saled.prs,saled.taxprice, saled.mny, saled.priceb, saled.taxpriceb, 
            saled.mnyb, saled.memo, saled.Bomid, saled.stName, saled.Punit, saled.Pqty, saled.cuno, saled.emno
            
            ,sale.cuname1,sale.invno,saled.standard 
            FROM saled 
            left join sale on saled.sano = sale.sano
            WHERE (0 = 0) ";



            tSaNo.Text = this.TSeekNo ?? "";
            btnQuery_Click(null, null);
            tSaNo.Clear();

            Thread t = new Thread(new ThreadStart(GetTotalCount));
            t.Start();
        }
        private void FrmSaleBrowNew_Shown(object sender, EventArgs e)
        {
            tSaNo.Focus();
        }
        void GetTotalCount()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cn.Open();
                cmd.CommandText = " Select Count(*) from saled ";
                TotalCount = cmd.ExecuteScalar().ToDecimal();
            }
        }


        private void Sort(string sorter, Button Qn)
        {
            dtD.DefaultView.Sort = sorter;

            list.ForEach(t => t.ForeColor = Color.Black);
            Qn.ForeColor = Color.Red;

            dataGridViewT1.DataSource = dtD.DefaultView;
        }
        private void SortCustDay(Button Qn)
        {
            if (dtD.Rows.Count == 0)
                return;

            DataTable temp = new DataTable();

            temp = dtD.AsEnumerable()
                .OrderBy(r => r["cuno"].ToString(), StringComparer.Ordinal)
                .ThenByDescending(r => r["Sadate"].ToString())
                .ThenBy(r => r["sano"].ToString())
                .CopyToDataTable();

            dtD.Clear();
            dtD = null;
            dtD = temp.Copy();

            temp.Clear();
            temp = null;

            list.ForEach(t => t.ForeColor = Color.Black);
            Qn.ForeColor = Color.Red;

            dataGridViewT1.DataSource = dtD.DefaultView;
        }
        private void SortItemDay(Button Qn)
        {
            if (dtD.Rows.Count == 0)
                return;

            DataTable temp = new DataTable();

            temp = dtD.AsEnumerable()
                .OrderBy(r => r["itno"].ToString(), StringComparer.Ordinal)
                .ThenByDescending(r => r["Sadate"].ToString())
                .CopyToDataTable();

            dtD.Clear();
            dtD = null;
            dtD = temp.Copy();

            temp.Clear();
            temp = null;

            list.ForEach(t => t.ForeColor = Color.Black);
            Qn.ForeColor = Color.Red;

            dataGridViewT1.DataSource = dtD.DefaultView;
        }
        private void SortCustItem(string ci1, string ci2, Button Qn)
        {
            if (dtD.Rows.Count == 0)
                return;

            DataTable temp = new DataTable();

            temp = dtD.AsEnumerable()
                .OrderBy(r => r[ci1].ToString(), StringComparer.Ordinal)
                .ThenBy(r => r[ci2].ToString(), StringComparer.Ordinal)
                .CopyToDataTable();

            dtD.Clear();
            dtD = null;
            dtD = temp.Copy();

            temp.Clear();
            temp = null;

            list.ForEach(t => t.ForeColor = Color.Black);
            Qn.ForeColor = Color.Red;

            dataGridViewT1.DataSource = dtD.DefaultView;
        }
        private void SeekCurrent(string column, string val)
        {
            if (dtD.DefaultView.Count == 0)
                return;

            var index = -1;
            Parallel.For(0, dtD.DefaultView.Count, (i, loopstate) =>
            {
                if (dtD.DefaultView[i][column].ToString() == val)
                {
                    lock (obj)
                    {
                        if (index == -1)
                            index = i;

                        if (i < index)
                            index = i;
                    }
                }
            });

            if (index == -1)//若找不到全部符合的在比對字元
            {
                for (int i = 0; i < dtD.DefaultView.Count; i++)
                {
                    if (string.CompareOrdinal(dtD.DefaultView[i][column].ToString().Trim(), val) > 0)
                    {
                        index = i;
                        break;
                    }
                }

                if (index == -1)
                    index = dtD.DefaultView.Count - 1;
            }

            if (index <= 0)
                index = 0;

            if (index >= dtD.DefaultView.Count - 1)
                index = dtD.DefaultView.Count - 1;

            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
        }
        private void SeekCurrent(string c1, string v1, string c2, string v2)
        {
            if (dtD.DefaultView.Count == 0)
                return;

            var index = -1;//Parallel 平行-迴圈
            Parallel.For(0, dtD.DefaultView.Count, (i, loopstate) =>
            {
                if (dtD.DefaultView[i][c1].ToString() == v1
                    && dtD.DefaultView[i][c2].ToString() == v2)
                {
                    lock (obj)
                    {
                        if (index == -1)
                            index = i;

                        if (i < index)
                            index = i;
                    }
                }
            });

            if (index == -1)
            {
                Parallel.For(0, dtD.DefaultView.Count, (i, loopstate) =>
                {
                    if (dtD.DefaultView[i][c1].ToString() == v1)
                    {
                        lock (obj)
                        {
                            if (index == -1)
                                index = i;

                            if (i < index)
                                index = i;
                        }
                    }
                });

                if (index != -1)
                {
                    for (int i = index; i < dtD.DefaultView.Count; i++)
                    {
                        if (dtD.DefaultView[i][c1].ToString() == v1)
                        {
                            index = i;
                            if (String.CompareOrdinal(dtD.DefaultView[i][c2].ToString(), v2) > 0)
                            {
                                index = i;
                                break;
                            }
                        }
                        else
                        {
                            index = i - 1;
                            break;
                        }
                    }
                }
                else
                {
                    this.SeekCurrent(c1, v1);
                    return;
                }
            }

            if (index <= 0)
                index = 0;

            if (index >= dtD.DefaultView.Count - 1)
                index = dtD.DefaultView.Count - 1;

            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
        }
        private void SeekCurrentDay(string c1, string v1)
        {
            if (dtD.DefaultView.Count == 0)
                return;

            var index = -1;
            Parallel.For(0, dtD.DefaultView.Count, (i, loopstate) =>
            {
                if (dtD.DefaultView[i][c1].ToString() == v1)
                {
                    lock (obj)
                    {
                        if (index == -1)
                            index = i;

                        if (i < index)
                            index = i;
                    }
                }
            });

            if (index == -1)
            {
                for (int i = 0; i < dtD.DefaultView.Count; i++)
                {
                    if (string.CompareOrdinal(dtD.DefaultView[i][c1].ToString().Trim(), v1) > 0)
                        index = i;
                    else
                        break;
                }
            }

            if (index <= 0)
                index = 0;

            if (index >= dtD.DefaultView.Count - 1)
                index = dtD.DefaultView.Count - 1;

            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
        }
        private void SeekCurrentDay(string c1, string v1, string c2, string v2)
        {
            if (dtD.DefaultView.Count == 0)
                return;

            var index = -1;
            Parallel.For(0, dtD.DefaultView.Count, (i, loopstate) =>
            {
                if (dtD.DefaultView[i][c1].ToString() == v1
                    && dtD.DefaultView[i][c2].ToString() == v2)
                {
                    lock (obj)
                    {
                        if (index == -1)
                            index = i;

                        if (i < index)
                            index = i;
                    }
                }
            });

            if (index == -1)
            {
                Parallel.For(0, dtD.DefaultView.Count, (i, loopstate) =>
                {
                    if (dtD.DefaultView[i][c1].ToString() == v1)
                    {
                        lock (obj)
                        {
                            if (index == -1)
                                index = i;

                            if (i < index)
                                index = i;
                        }
                    }
                });

                var oIndex = index;

                if (index != -1)
                {
                    for (int i = index; i < dtD.DefaultView.Count; i++)
                    {
                        if (dtD.DefaultView[i][c1].ToString() == v1)
                        {
                            index = i;
                            if (String.CompareOrdinal(v2, dtD.DefaultView[i][c2].ToString()) > 0 && i > oIndex)
                            {
                                index = i - 1;
                                break;
                            }
                        }
                        else
                        {
                            index = i - 1;
                            break;
                        }
                    }
                }
                else
                {
                    this.SeekCurrent(c1, v1);
                    return;
                }
            }

            if (index <= 0)
                index = 0;

            if (index >= dtD.DefaultView.Count - 1)
                index = dtD.DefaultView.Count - 1;

            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
        }
        private void Q2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var said = dtD.DefaultView[index]["SaID"].ToString();
                var sano = dtD.DefaultView[index]["SaNo"].ToString();
                Eload("Saled.SaNo", sano);
                Sort("SaNo ASC", Q2);
                SeekCurrent("SaID", said);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void Q3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var said = dtD.DefaultView[index]["SaID"].ToString();
                var day = dtD.DefaultView[index]["Sadate"].ToString();
                Dload("Saled.Sadate", day);
                Sort("Sadate DESC,SaNo ASC", Q3);
                SeekCurrent("SaID", said);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void Q4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var said = dtD.DefaultView[index]["SaID"].ToString();
                var itno = dtD.DefaultView[index]["ItNo"].ToString();
                var day = dtD.DefaultView[index]["Sadate"].ToString();

                Eload("Saled.ItNo", itno);
                SortItemDay(Q4);
                SeekCurrent("SaID", said);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void Q5_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var said = dtD.DefaultView[index]["SaID"].ToString();
                var cuno = dtD.DefaultView[index]["CuNo"].ToString();
                var day = dtD.DefaultView[index]["Sadate"].ToString();
                Eload("Saled.CuNo", cuno);
                SortCustDay(Q5);
                SeekCurrent("SaID", said);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void Q6_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var said = dtD.DefaultView[index]["SaID"].ToString();
                var orno = dtD.DefaultView[index]["OrNo"].ToString();
                var day = dtD.DefaultView[index]["Sadate"].ToString();

                if (orno.Length > 0)
                    Eload("Saled.OrNo", orno);

                Sort("OrNo ASC,Sadate ASC", Q6);
                SeekCurrent("SaID", said);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void Q7_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var said = dtD.DefaultView[index]["SaID"].ToString();
                var cuno = dtD.DefaultView[index]["CuNo"].ToString();
                var itno = dtD.DefaultView[index]["ItNo"].ToString();
                Fload("Saled.CuNo", cuno, "Saled.ItNo", itno);
                SortCustItem("CuNo", "ItNo", Q7);
                SeekCurrent("SaID", said);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void Q8_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var said = dtD.DefaultView[index]["SaID"].ToString();
                var orno = dtD.DefaultView[index]["OrNo"].ToString();
                var itno = dtD.DefaultView[index]["ItNo"].ToString();

                if (orno.Length > 0)
                    Eload("Saled.OrNo", orno);

                SortCustItem("OrNo", "ItNo", Q8);
                SeekCurrent("SaID", said);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void Q9_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var stno = dtD.DefaultView[index]["StNo"].ToString();
                var itno = dtD.DefaultView[index]["ItNo"].ToString();
                Eload("Saled.StNo", stno);
                SortCustItem("StNo", "ItNo", Q9);
                SeekCurrent("StNo", stno, "ItNo", itno);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void Q10_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var said = dtD.DefaultView[index]["SaID"].ToString();
                var memo = dtD.DefaultView[index]["Memo"].ToString();

                if (memo.Length > 0)
                    Eload("Saled.Memo", memo);

                Sort("Memo ASC", Q10);
                SeekCurrent("SaID", said);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void Q11_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;

                var emno = dtD.DefaultView[index]["EmNo"].ToString();
                var cuno = dtD.DefaultView[index]["CuNo"].ToString();

                Eload("Saled.EmNo", emno);
                SortCustItem("EmNo", "CuNo", Q11);
                SeekCurrent("EmNo", emno, "CuNo", cuno);
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);

                if (tCuNo.TrimTextLenth() > 0 && tSadate.TrimTextLenth() > 0)
                {
                    var date = Date.ToTWDate(tSadate.Text.Trim());

                    Eload("Saled.CuNo", tCuNo.Text.Trim());
                    SortCustDay(Q5);
                    SeekCurrentDay("CuNo", tCuNo.Text.Trim(), "Sadate", date);
                }
                else if (tOrNo.TrimTextLenth() > 0 && tSadate.TrimTextLenth() > 0)
                {
                    var date = Date.ToTWDate(tSadate.Text.Trim());

                    Eload("Saled.OrNo", tOrNo.Text.Trim());
                    Sort("OrNo ASC,Sadate ASC", Q6);
                    SeekCurrentDay("OrNo", tOrNo.Text.Trim(), "Sadate", date);
                }
                else if (tItNo.TrimTextLenth() > 0 && tSadate.TrimTextLenth() > 0)
                {
                    var date = Date.ToTWDate(tSadate.Text.Trim());

                    Eload("Saled.ItNo", tItNo.Text.Trim());
                    SortItemDay(Q4);
                    SeekCurrentDay("ItNo", tItNo.Text.Trim(), "Sadate", date);
                }
                else if (tSadate.TrimTextLenth() > 0)
                {
                    var date = Date.ToTWDate(tSadate.Text.Trim());

                    Dload("Saled.Sadate", date);
                    Sort("Sadate DESC,SaNo ASC", Q3);
                    SeekCurrentDay("Sadate", date);
                }
                else if (tCuNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    Fload("Saled.CuNo", tCuNo.Text.Trim(), "Saled.ItNo", tItNo.Text.Trim());
                    SortCustItem("CuNo", "ItNo", Q7);
                    SeekCurrent("CuNo", tCuNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tOrNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    Eload("Saled.OrNo", tOrNo.Text.Trim());
                    SortCustItem("OrNo", "ItNo", Q8);
                    SeekCurrent("OrNo", tOrNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tStNo.TrimTextLenth() > 0 && tItNo.TrimTextLenth() > 0)
                {
                    Eload("Saled.StNo", tStNo.Text.Trim());
                    SortCustItem("StNo", "ItNo", Q9);
                    SeekCurrent("StNo", tStNo.Text.Trim(), "ItNo", tItNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0 && tCuNo.TrimTextLenth() > 0)
                {
                    Eload("Saled.EmNo", tEmNo.Text.Trim());
                    SortCustItem("EmNo", "CuNo", Q11);
                    SeekCurrent("EmNo", tEmNo.Text.Trim(), "CuNo", tCuNo.Text.Trim());
                }
                else if (tSaNo.TrimTextLenth() > 0)
                {
                    Eload("Saled.SaNo", tSaNo.Text.Trim());
                    Sort("SaNo ASC", Q2);
                    SeekCurrent("SaNo", tSaNo.Text.Trim());
                }
                else if (tOrNo.TrimTextLenth() > 0)
                {
                    Eload("Saled.OrNo", tOrNo.Text.Trim());
                    Sort("OrNo ASC,Sadate ASC", Q6);
                    SeekCurrent("OrNo", tOrNo.Text.Trim());
                }
                else if (tItNo.TrimTextLenth() > 0)
                {
                    Eload("Saled.ItNo", tItNo.Text.Trim());
                    SortItemDay(Q4);
                    SeekCurrent("ItNo", tItNo.Text.Trim());
                }
                else if (tMemo.TrimTextLenth() > 0)
                {
                    Eload("Saled.Memo", tMemo.Text.Trim());
                    Sort("Memo ASC", Q10);
                    SeekCurrent("Memo", tMemo.Text.Trim());
                }
                else if (tStNo.TrimTextLenth() > 0)
                {
                    Eload("Saled.StNo", tStNo.Text.Trim());
                    SortCustItem("StNo", "ItNo", Q9);
                    SeekCurrent("StNo", tStNo.Text.Trim());
                }
                else if (tEmNo.TrimTextLenth() > 0)
                {
                    Eload("Saled.EmNo", tEmNo.Text.Trim());
                    SortCustItem("EmNo", "CuNo", Q11);
                    SeekCurrent("EmNo", tEmNo.Text.Trim());
                }
                else if (tCuNo.TrimTextLenth() > 0)
                {
                    Eload("Saled.CuNo", tCuNo.Text.Trim());
                    SortCustDay(Q5);
                    SeekCurrent("CuNo", tCuNo.Text.Trim());
                }
                else if (tstandard.TrimTextLenth() > 0)
                {
                    Eload("Saled.standard", tstandard.Text.Trim());
                   
                    dtD.DefaultView.Sort = "standard ASC,Sadate DESC";
                    list.ForEach(t => t.ForeColor = Color.Black);
                    dataGridViewT1.DataSource = dtD.DefaultView;
       
                    SeekCurrent("standard", tstandard.Text.Trim());
                }
                else if (tInvNo.TrimTextLenth() > 0)
                {
                    dataGridViewT1.DataSource = null;

                    if (dtD.Rows.Count < TotalCount)
                    {
                        dtD.Clear();
                        using (SqlConnection cn = new SqlConnection(JBS.xSQL.xSqlConnectionString))
                        using (SqlCommand cmd = cn.CreateCommand())
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("invno", tInvNo.Text.Trim());

                            cmd.CommandText = "Select top " + Common.SearchCount / 2 + " " + SQL + " And invno >= (@invno) order by invno;";
                            da.Fill(dtD);

                            cmd.CommandText = "Select top " + Common.SearchCount / 2 + " " + SQL + "  And invno <  (@invno) order by invno DESC;";
                            da.Fill(dtD);
                        }
                    }

                    Sort("InvNo ASC", Q2);
                    list.ForEach(t => t.ForeColor = Color.Black);

                    SeekCurrent("InvNo", tInvNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                FillHeadText();
                dataGridViewT1.SelectionChanged -= new EventHandler(dataGridViewT1_SelectionChanged);
                dataGridViewT1.SelectionChanged += new EventHandler(dataGridViewT1_SelectionChanged);
            }
        }


        private void Dload(string column, string date)
        {
            dataGridViewT1.DataSource = null;

            if (dtD.Rows.Count < TotalCount)
            {
                Common.Dload(dtD, SQL, column, date);
            }
        }
        private void Eload(string column, string key)
        {
            dataGridViewT1.DataSource = null;

            if (TotalCount == -1 || dtD.Rows.Count < TotalCount)
            {
                Common.Eload(dtD, SQL, column, key, "Saled.Sadate");
            }
        }
        private void Fload(string column, string key, string c2, string k2)
        {
            dataGridViewT1.DataSource = null;

            if (TotalCount == -1 || dtD.Rows.Count < TotalCount)
            {
                Common.Fload(dtD, SQL, column, key, c2, k2, "Saled.Sadate");
            }
        }
        private void dataGridViewT1_SelectionChanged(object sender, EventArgs e)
        {
            FillHeadText();
        }
        private void FillHeadText()
        {
            if (dataGridViewT1.SelectedRows == null)
                return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            if (index != dataGridViewT1.SelectedRows[0].Index)
                return;

            //if (this.header == dtD.DefaultView[index]["SaID"].ToDecimal())
            //    return;

            //this.Text = (Query++).ToString();
            //this.header = dtD.DefaultView[index]["SaID"].ToDecimal();

            textBoxT15.Text = dtD.DefaultView[index]["StNo"].ToString();
            textBoxT16.Text = dtD.DefaultView[index]["StName"].ToString();

            var sano = dtD.DefaultView[index]["SaNo"].ToString().Trim();
            jSale.Validate<JBS.JS.Sale>(sano, row =>
            {
                textBoxT1.Text = row["TaxMny"].ToDecimal().ToString("f" + Common.MST);
                textBoxT2.Text = row["Tax"].ToDecimal().ToString("f" + Common.TS);
                textBoxT3.Text = row["SaMemo"].ToString();
                textBoxT4.Text = row["InvNo"].ToString();
                textBoxT6.Text = row["TotMny"].ToDecimal().ToString("f" + Common.MST);

                textBoxT7.Text = row["Xa1No"].ToString();
                textBoxT8.Text = row["Xa1Name"].ToString();
                textBoxT11.Text = row["Xa1Par"].ToDecimal().ToString("f4");

                textBoxT12.Text = row["CuNo"].ToString();
                textBoxT13.Text = row["CuPer1"].ToString();
                textBoxT14.Text = row["CuTel1"].ToString();

                jSale.Validate<JBS.JS.XX03>(row["X3No"].ToString(), rw =>
                {
                    textBoxT5.Text = rw["X3Name"].ToString();
                }, () => textBoxT5.Clear());
            });

            var itno = dtD.DefaultView[index]["ItNo"].ToString().Trim();
            jSale.Validate<JBS.JS.Item>(itno, row =>
            {
                textBoxT9.Text = row["ItNo"].ToString();
                textBoxT10.Text = row["ItNoUdf"].ToString();
                textBoxT17.Text = row["ItStockQty"].ToDecimal().ToString("f" + Common.Q);

            });
        }


        private void btnPicture_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            using (var frm = new SOther.FrmPicture())
            {
                var itno = dtD.DefaultView[index]["ItNo"].ToString().Trim();

                object obj = null;
                jSale.Validate<JBS.JS.Item>(itno, row => obj = row["pic"]);

                frm.image = obj;
                frm.ShowDialog();
            }
        }
        private void btnDesp_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            using (var frm = new JE.SOther.FrmDesp(false, FormStyle.Mini))
            {
                DataTable temp = new DataTable();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    var said = dtD.DefaultView[index]["SaID"].ToString().Trim();

                    cmd.Parameters.AddWithValue("SaID", said);
                    cmd.CommandText = "Select * from saled where SaID = @SaID";
                    da.Fill(temp);
                }

                frm.dr = temp.AsEnumerable().FirstOrDefault();
                frm.ShowDialog();

                temp.Clear();
                temp = null;
            }
        }
        private void btnBom_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            var trait = dtD.DefaultView[index]["ItTrait"].ToDecimal();
            if (trait != 1 && trait != 2)
            {
                MessageBox.Show("只有組合品或組裝品有組件明細", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridViewT1.Focus();
                return;
            }

            using (subMenuFm_2.FrmSale_Bom frm = new subMenuFm_2.FrmSale_Bom())
            {
                var itno = dtD.DefaultView[index]["ItNo"].ToString().Trim();
                var itname = "";
                jSale.Validate<JBS.JS.Item>(itno, row => itname = row["itname"].ToString().Trim());

                frm.BoItNo1 = itno;
                frm.BoItName1 = itname;
                frm.JustForBrow = true;
                frm.TTable = "SaleBom";
                frm.TKey = dtD.DefaultView[index]["BomID"].ToString().Trim();
                frm.ShowDialog();
            }
        }
        private void btnStock_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            btnStock.Focus();
            using (var frm = new subMenuFm_2.FrmSale_Stock())
            {
                var itno = dtD.DefaultView[index]["ItNo"].ToString().Trim();

                frm.ItNo = itno;
                frm.ShowDialog();
            }
            dataGridViewT1.Focus();
        }
        private void btnInv_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            var sano = dtD.DefaultView[index]["SaNo"].ToString().Trim();
            bool IsPOS = false;
            jSale.Validate<JBS.JS.Sale>(sano, row =>
            {
                IsPOS = row["Bracket"].ToString().Trim() == "前台" ? true : false;
            });

            using (var frm = new subMenuFm_2.FrmSale_Inv("Sale", sano, IsPOS))
            {
                jSale.Validate<JBS.JS.Sale>(sano, row =>
                {
                    frm.Result[0] = Common.User_DateTime == 1 ? row["InvDate"].ToString() : row["InvDate1"].ToString();
                    frm.Result[1] = row["InvName"].ToString();
                    frm.Result[2] = row["InvTaxNo"].ToString();
                    frm.Result[3] = row["InvAddr1"].ToString();

                    //媒體申報
                    if(row["invkind"].ToString() !="")
                        frm.InvKind.SelectedIndex = frm.InvKind.FindString(row["invkind"].ToString());
                    if(row["specialtax"].ToString() != "")
                        frm.SpecialTax.SelectedIndex = frm.SpecialTax.FindString(row["specialtax"].ToString());
                    if (row["passmode"].ToString() != "")
                        frm.PassMode.SelectedIndex = frm.PassMode.FindString(row["passmode"].ToString());
                });

                //媒體申報
                frm.媒體申報.Enabled = false;

                if (frm.ShowDialog() == DialogResult.OK)
                    textBoxT4.Text = frm.ivno;
            }
        }


        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name != this.備註說明.Name) return;
            try
            {
                var bomid = dtD.DefaultView[e.RowIndex]["Bomid"].ToString().Trim();
                var memo = dataGridViewT1[this.備註說明.Name, e.RowIndex].EditedFormattedValue.ToString();
                if (memo.Length == 0) return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@memo", memo);
                    cmd.Parameters.AddWithValue("@Bomid", bomid);

                    cmd.CommandText = "Update Saled Set memo = @memo where Bomid = @Bomid";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void textBoxT3_Validating(object sender, CancelEventArgs e)
        {
            if (dtD.Rows.Count == 0) return;

            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1) return;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SaMemo", textBoxT3.Text.Trim());
                    cmd.Parameters.AddWithValue("@SaNo", dtD.DefaultView[index]["SaNo"].ToString().Trim());

                    cmd.CommandText = "Update Sale Set SaMemo=@SaMemo where SaNo=@SaNo ";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void tCuNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Cust>(sender);
        }
        private void tItNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Item>(sender);
        }
        private void tStNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Stkroom>(sender);
        }
        private void tEmNo_DoubleClick(object sender, EventArgs e)
        {
            jSale.Open<JBS.JS.Empl>(sender);
        }
        private void tSadate_Validating(object sender, CancelEventArgs e)
        {
            if (btnGet.Focused)
                return;

            jSale.DateValidate(tSadate, e, true);
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                btnQuery_Click(null, null);
            }
            else if (keyData == Keys.F6)
            {
                btnDesp_Click(null, null);
            }
            else if (keyData == Keys.F8)
            {
                btnStock_Click(null, null);
            }
            else if (keyData == Keys.F7)
            {
                btnBom_Click(null, null);
            }
            else if (keyData == Keys.F4)
            {
                btnGet.Focus();
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
            else if (keyData.ToString().StartsWith("F8") && keyData.ToString().EndsWith("Shift"))
            {
                Q8_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F9") && keyData.ToString().EndsWith("Shift"))
            {
                Q9_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F10") && keyData.ToString().EndsWith("Shift"))
            {
                Q10_Click(null, null);
            }
            else if (keyData.ToString().StartsWith("F11") && keyData.ToString().EndsWith("Shift"))
            {
                Q11_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void FrmSaleBrowNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.TResult != null && this.TResult.Length > 0)
                return;

            if (dtD.Rows.Count == 0)
            {
                this.TResult = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) this.TResult = "";
                else
                {
                    this.TResult = dtD.DefaultView[index]["SaNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
            dtD.Clear();
        }
        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
                return;

            if (dtD.Rows.Count == 0)
                return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name != this.備註說明.Name)
                btnGet_Click(null, null);
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dtD.Rows.Count == 0)
            {
                this.TResult = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) this.TResult = "";
                else
                {
                    this.TResult = dtD.DefaultView[index]["SaNo"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }
            dtD.Clear();
        }

        private void labelT1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            var row = Common.load("Check", "sale", "sano",dtD.DefaultView[index]["SaNo"].ToString());
            if (row != null)
            {
                using (SOther.會計編號開窗 frm = new SOther.會計編號開窗())
                {
                    frm.acno = row["acno"].ToString();
                    frm.accono = row["accono"].ToString();
                    frm.ShowDialog();
                }
            }
        }
    }
}
