using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Linq;

namespace S_61.subMenuFm_2
{
    public partial class FrmSale_Stock : Formbase
    {
        DataTable dt = new DataTable();
        List<TextBoxNumberT> li;
        public string ItNo = "";
        string _Unit, _UnitP;

        public FrmSale_Stock()
        {
            InitializeComponent();
            li = new List<TextBoxNumberT> { A1, A2, A3, A4, ATotal, B1, B2, B3, B4, BTotal, C1, C2, C3, C4, CTotal, ItPkgQty };

            foreach (TextBoxNumberT item in li)
            {
                item.FirstNum = 17;
                item.LastNum = Common.Q;
            }

            this.包裝庫存數量.Set庫存數量小數();
            this.單位庫存數量.Set庫存數量小數();
            this.單位庫存總數量.Set庫存數量小數();
            this.寄庫數量.Set庫存數量小數();
        }

        private void FrmSale_Stock_Load(object sender, EventArgs e)
        {
            if (ItNo != "") LoadItem();
            btnExit.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void LoadItem()
        {
            try
            {
                DataTable dtInStk = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("itno", ItNo);
                    cmd.CommandText = "select ItUnit,ItUnitP,ItPkgQty from Item where ItNo=@itno ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            _Unit = reader["ItUnit"].ToString();
                            _UnitP = reader["ItUnitP"].ToString();

                            dataGridViewT1.Columns["單位庫存數量"].HeaderText += _Unit;
                            dataGridViewT1.Columns["包裝庫存數量"].HeaderText += _UnitP;
                            ItUnit.Text = _Unit;
                            ItUnitP.Text = _UnitP;
                            _tUnit.Text = _Unit;


                            var itpkgqty = reader["ItPkgQty"].ToDecimal();
                            if (itpkgqty == 0) itpkgqty = 1;
                            ItPkgQty.Text = itpkgqty.ToString("f" + Common.Q);
                        }
                        else
                        {
                            ItPkgQty.Text = (1M).ToString("F" + Common.Q);
                        }
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("itno", ItNo);
                        cmd.CommandText = " Select 寄庫數量=0.0,q.stno,s.stname,s.sttrait,q.itno,ISNULL(q.itqty,0)itqty,ISNULL(q.itqtyf,0)itqtyf,i.itname,i.itunit "
                               + " from stock as q "
                               + " left join stkroom as s on q.stno = s.stno "
                               + " left join item as i on q.itno = i.itno "
                               + " where i.itno=@itno";

                        dt.Clear();
                        da.Fill(dt);
                        dataGridViewT1.DataSource = dt;
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandText = @"
                        Select stno,寄庫數量=SUM(ISNULL(A.tal,0)) from (
	                        Select stno,tal=SUM(nonqty) from instkd where ittrait <> 1 and itno = @itno group by stno
	                        union all	
	                        Select stno,tal=SUM((d.nonqty*d.itpkgqty*b.itqty*b.itpkgqty)/b.itpareprs) 
	                        from InStkBOM b left join InStkD d on b.BomID=d.bomid where ittrait = 1 and b.itno = @itno group by stno
                        )A group by A.stno ";
                        da.Fill(dtInStk);
                    }

                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var stno = dt.Rows[i]["stno"].ToString().Trim();
                    var row = dtInStk.AsEnumerable().FirstOrDefault(r => r["stno"].ToString().Trim() == stno);
                    if (row == null)
                    {
                        dt.Rows[i]["寄庫數量"] = (0M).ToDecimal("f" + Common.Q);
                    }
                    else
                    {
                        dt.Rows[i]["寄庫數量"] = row["寄庫數量"].ToDecimal("f" + Common.Q);
                    }

                    if (dt.Rows[i]["StTrait"].ToString() == "1")
                        dataGridViewT1["倉庫類別", i].Value = "庫存倉";
                    else if (dt.Rows[i]["StTrait"].ToString() == "2")
                        dataGridViewT1["倉庫類別", i].Value = "借出倉";
                    else if (dt.Rows[i]["StTrait"].ToString() == "3")
                        dataGridViewT1["倉庫類別", i].Value = "加工倉";
                    else if (dt.Rows[i]["StTrait"].ToString() == "4")
                        dataGridViewT1["倉庫類別", i].Value = "借入倉";

                    decimal _ItQty = 0;
                    decimal _PkgQty = 1;
                    decimal.TryParse(dt.Rows[i]["ItQty"].ToString(), out _ItQty);
                    decimal.TryParse(ItPkgQty.Text, out _PkgQty);

                    if (_UnitP.Trim().Length > 0)
                    {
                        if (_ItQty < 0)
                        {
                            _ItQty = _ItQty * (-1);
                            decimal d = Math.Floor(_ItQty / _PkgQty);
                            d = d * (-1);
                            dataGridViewT1["包裝庫存數量", i].Value = d.ToString("F" + Common.Q);
                            d = (_ItQty % _PkgQty);
                            d = d * (-1);
                            dataGridViewT1["單位庫存數量", i].Value = d.ToString("F" + Common.Q);
                        }
                        else
                        {
                            dataGridViewT1["包裝庫存數量", i].Value = Math.Floor(_ItQty / _PkgQty);
                            dataGridViewT1["單位庫存數量", i].Value = (decimal)(_ItQty % _PkgQty);
                        }
                    }
                    else
                    {
                        dataGridViewT1["包裝庫存數量", i].Value = 0;
                        dataGridViewT1["單位庫存數量", i].Value = _ItQty.ToString("F" + Common.Q);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            decimal _A1 = 0, _B1 = 0, _C1 = 0;
            decimal _A2 = 0, _B2 = 0, _C2 = 0;
            decimal _A3 = 0, _B3 = 0, _C3 = 0;
            decimal _A4 = 0, _B4 = 0, _C4 = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["StTrait"].ToString() == "1")
                {
                    _A1 += decimal.Parse(dataGridViewT1["包裝庫存數量", i].Value.ToString());
                    _B1 += decimal.Parse(dataGridViewT1["單位庫存數量", i].Value.ToString());
                    _C1 += decimal.Parse(dataGridViewT1["單位庫存總數量", i].Value.ToString());
                }
                else if (dt.Rows[i]["StTrait"].ToString() == "2")
                {
                    _A2 += decimal.Parse(dataGridViewT1["包裝庫存數量", i].Value.ToString());
                    _B2 += decimal.Parse(dataGridViewT1["單位庫存數量", i].Value.ToString());
                    _C2 += decimal.Parse(dataGridViewT1["單位庫存總數量", i].Value.ToString());
                }
                else if (dt.Rows[i]["StTrait"].ToString() == "3")
                {
                    _A3 += decimal.Parse(dataGridViewT1["包裝庫存數量", i].Value.ToString());
                    _B3 += decimal.Parse(dataGridViewT1["單位庫存數量", i].Value.ToString());
                    _C3 += decimal.Parse(dataGridViewT1["單位庫存總數量", i].Value.ToString());
                }
                else if (dt.Rows[i]["StTrait"].ToString() == "4")
                {
                    _A4 += decimal.Parse(dataGridViewT1["包裝庫存數量", i].Value.ToString());
                    _B4 += decimal.Parse(dataGridViewT1["單位庫存數量", i].Value.ToString());
                    _C4 += decimal.Parse(dataGridViewT1["單位庫存總數量", i].Value.ToString());
                }
            }
            A1.Text = _A1.ToString("f" + Common.Q);
            A2.Text = _A2.ToString("f" + Common.Q);
            A3.Text = _A3.ToString("f" + Common.Q);
            A4.Text = _A4.ToString("f" + Common.Q);

            B1.Text = _B1.ToString("f" + Common.Q);
            B2.Text = _B2.ToString("f" + Common.Q);
            B3.Text = _B3.ToString("f" + Common.Q);
            B4.Text = _B4.ToString("f" + Common.Q);

            C1.Text = _C1.ToString("f" + Common.Q);
            C2.Text = _C2.ToString("f" + Common.Q);
            C3.Text = _C3.ToString("f" + Common.Q);
            C4.Text = _C4.ToString("f" + Common.Q);

            var _Sum = (_C1 + _C2 + _C3 + _C4);
            var _Qty = ItPkgQty.Text.ToDecimal();

            if (_UnitP.Trim().Length > 0)
            {
                if (_Sum < 0)
                {
                    CTotal.Text = _Sum.ToString("f" + Common.Q);
                    decimal t = ((-1) * _Sum);
                    t = Math.Floor(t / _Qty);
                    t = t * (-1);
                    ATotal.Text = t.ToString("f" + Common.Q);
                    BTotal.Text = (_Sum % _Qty).ToString("f" + Common.Q);
                }
                else
                {
                    CTotal.Text = _Sum.ToString("f" + Common.Q);
                    ATotal.Text = (Math.Floor(_Sum / _Qty)).ToString("f" + Common.Q);
                    BTotal.Text = (_Sum % _Qty).ToString("f" + Common.Q);
                }
            }
            else
            {
                CTotal.Text = _Sum.ToString("f" + Common.Q);
                BTotal.Text = CTotal.Text;
                ATotal.Text = (0M).ToString("f" + Common.Q);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F11) this.Dispose();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dt.Clear();
            base.OnFormClosing(e);
        }
    }
}
