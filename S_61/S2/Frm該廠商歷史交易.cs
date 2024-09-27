using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class Frm該廠商歷史交易 : Formbase
    {
        JBS.JS.xEvents xe;
        public string fano = "";
        public string itno = "";
        DataTable dtD = new DataTable();

        public Frm該廠商歷史交易()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            this.Style = FormStyle.Mini;
            var day = Common.User_DateTime == 1 ? "bsdate" : "bsdate1";
            this.單據日期.DataPropertyName = day;

            pVar.SetMemoUdf(this.備註說明);

            this.數量.Set庫存數量小數();
            this.單價.Set進貨單價小數();
            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前單價.FirstNum = 9;
            this.稅前單價.LastNum = 6;
            this.稅前單價.DefaultCellStyle.Format = "f6";
            this.稅前金額.FirstNum = 9;
            this.稅前金額.LastNum = 6;
            this.稅前金額.DefaultCellStyle.Format = "f6";

            //權限設定
            this.單價.Visible = Common.User_ShopPrice;
            this.稅前單價.Visible = Common.User_ShopPrice;
            this.稅前金額.Visible = Common.User_ShopPrice;

            textBoxT3.Visible = textBoxT4.Visible = Common.User_ShopPrice;
        }

        private void Frm該廠商歷史交易_Load(object sender, EventArgs e)
        {
            //該廠商交易，只定位在廠商
            textBoxT1.Text = fano;
 
            LoadDB();
        }

        void LoadDB()
        {
            dtD.Clear();

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("fano", textBoxT1.Text.Trim());
                cmd.CommandText = "select 單據='退貨',* from rshopd where fano=(@fano)";
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtD);
                }
                textBoxT4.Text = dtD.AsEnumerable().Sum(r => r["Mny"].ToDecimal()).ToString("f4");

                for (int i = 0; i < dtD.Rows.Count; i++)
                {
                    dtD.Rows[i]["qty"] = -1 * (dtD.Rows[i]["qty"].ToDecimal());
                    dtD.Rows[i]["Mny"] = -1 * (dtD.Rows[i]["Mny"].ToDecimal());
                }

                cmd.CommandText = "select 單據='進貨',* from bshopd where fano=(@fano)";
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtD);
                }
                textBoxT3.Text = (dtD.AsEnumerable().Sum(r => r["Mny"].ToDecimal()) + textBoxT4.Text.ToDecimal()).ToString("f4");

                dtD.DefaultView.Sort = "bsdate DESC";
                dataGridViewT1.DataSource = dtD.DefaultView;

                if (dtD.Rows.Count > 0)
                {
                    dtD.DefaultView.Search(ref dataGridViewT1, "itno", textBoxT2.Text.Trim());
                }
            }
        }

        private void buttonGridT1_Click(object sender, EventArgs e)
        {
            if (textBoxT1.TrimTextLenth() > 0)
            {
                dataGridViewT1.DataSource = null;
                LoadDB();
            }
        }

        private void textBoxT1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void textBoxT2_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Dispose(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
