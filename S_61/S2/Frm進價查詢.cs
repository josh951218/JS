using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class Frm進價查詢 : Formbase
    {
        public string itno = "";
        DataTable dtD = new DataTable();

        public Frm進價查詢()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
            var day = Common.User_DateTime == 1 ? "bsdate" : "bsdate1";
            this.進貨日期.DataPropertyName = day;

            pVar.SetMemoUdf(this.備註說明);

            this.數量.Set庫存數量小數();
            this.單價.Set進貨單價小數();
            this.實際成本.Set本幣金額小數();

            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前單價.FirstNum = 9;
            this.稅前單價.LastNum = 6;
            this.稅前單價.DefaultCellStyle.Format = "f6";


            //權限設定
            this.單價.Visible = Common.User_SalePrice && Common.User_ShopPrice;
            this.稅前單價.Visible = Common.User_SalePrice && Common.User_ShopPrice;
            this.實際成本.Visible = Common.User_SalePrice && Common.User_ShopPrice;
        }

        private void Frm進貨查詢_Load(object sender, EventArgs e)
        {
            dtD.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("ItNo", itno);
                cmd.CommandText = @"select Bshopd.*,Bshop.faname1 from
                (
                select * from Bshopd where itno =@ItNo
                )Bshopd left join Bshop on Bshopd.bsno = Bshop.bsno
                order by  Bshop.bsdate desc";
                da.Fill(dtD);
            }
            dtD.DefaultView.Sort = "itno ASC,bsdate DESC";
            if (dtD.Rows.Count > 0)
            {
                dataGridViewT1.DataSource = dtD.DefaultView;
                dtD.DefaultView.Search(ref dataGridViewT1, "itno", itno);
            }
            else
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Dispose();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Dispose(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dtD.Clear();
            base.OnFormClosing(e);
        }
    }
}
