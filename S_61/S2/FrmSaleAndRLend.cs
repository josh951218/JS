using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmSaleAndRLend : Formbase
    {
        public enum MyEnum { Sale, BShop }
        public MyEnum me;
        public string No { get; set; }
        public string RNo { get; set; }
        DataTable dtD = new DataTable();
        DataTable dtRD = new DataTable();

        public FrmSaleAndRLend()
        {
            InitializeComponent();

            this.數量.Set庫存數量小數();
            this.數量1.Set庫存數量小數();
        }

        private void FrmSaleAndRLend_Load(object sender, EventArgs e)
        {
            if (me == MyEnum.Sale)
                Sale();

            if (me == MyEnum.BShop)
                BShop();
        }

        private void BShop()
        {
            this.銷貨產品.HeaderText = "進貨產品";

            dtD.Clear();
            dtRD.Clear();

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("bsno", No);
                cmd.Parameters.AddWithValue("bono", RNo);

                cmd.CommandText = "Select 序號='',itno,itname,qty from bshopd where bsno = bsno and LEN(bono)>0 and cyno = @bono order by bsid ";
                da.Fill(dtD);

                cmd.CommandText = "Select 序號='',itno,itname,qty from rborrd where bono = @bono and LEN(borrno) > 0 order by boid ";
                da.Fill(dtRD);
            }

            for (int i = 0; i < dtD.Rows.Count; )
            {
                dtD.Rows[i]["序號"] = (++i).ToString();
            }
            for (int i = 0; i < dtRD.Rows.Count; )
            {
                dtRD.Rows[i]["序號"] = (++i).ToString();
            }

            dataGridViewT1.DataSource = dtD;
            dataGridViewT2.DataSource = dtRD;
        }

        private void Sale()
        {
            this.銷貨產品.HeaderText = "銷貨產品";

            dtD.Clear();
            dtRD.Clear();

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("sano", No);
                cmd.Parameters.AddWithValue("leno", RNo);

                cmd.CommandText = "Select 序號='',itno,itname,qty from saled where sano = sano and LEN(leno)>0 and cyno = @leno order by said ";
                da.Fill(dtD);

                cmd.CommandText = "Select 序號='',itno,itname,qty from rlendd where leno = @leno and LEN(lendno) > 0 order by leid ";
                da.Fill(dtRD);
            }

            for (int i = 0; i < dtD.Rows.Count; )
            {
                dtD.Rows[i]["序號"] = (++i).ToString();
            }
            for (int i = 0; i < dtRD.Rows.Count; )
            {
                dtRD.Rows[i]["序號"] = (++i).ToString();
            }

            dataGridViewT1.DataSource = dtD;
            dataGridViewT2.DataSource = dtRD;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dtD.Clear();
            dtRD.Clear();

            base.OnFormClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            { 
                dtD.Dispose();
                dtRD.Dispose();

                this.Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
