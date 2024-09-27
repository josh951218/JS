using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class Frm該客戶此產品交易 : Formbase
    {
        public string cuno = "";
        public string itno = "";
        DataTable dtD = new DataTable();

        public Frm該客戶此產品交易()
        {
            InitializeComponent();
            this.Style = FormStyle.Mini;
            var day = Common.User_DateTime == 1 ? "sadate" : "sadate1";
            this.銷貨日期.DataPropertyName = day;

            pVar.SetMemoUdf(this.備註說明);

            this.數量.Set庫存數量小數();
            this.單價.Set銷貨單價小數();

            this.折數.FirstNum = 1;
            this.折數.LastNum = 3;
            this.折數.DefaultCellStyle.Format = "f3";
            this.稅前單價.FirstNum = 9;
            this.稅前單價.LastNum = 6;
            this.稅前單價.DefaultCellStyle.Format = "f6";

            //權限設定
            this.單價.Visible = Common.User_SalePrice;
            this.稅前單價.Visible = Common.User_SalePrice;
        }

        private void Frm該客戶此產品交易_Load(object sender, EventArgs e)
        {
            dtD.Clear();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("CuNo", cuno);
                cmd.Parameters.AddWithValue("ItNo", itno);
                cmd.CommandText = "Select 單據='銷貨',saled.*,sale.cuname1 from saled left join sale on saled.sano=sale.sano where saled.cuno=(@cuno) And itno=(@itno)";
                da.Fill(dtD);
                cmd.CommandText = "Select 單據='銷退',rsaled.*,rsale.cuname1 from rsaled left join rsale on rsaled.sano=rsale.sano where rsaled.cuno=(@cuno) And itno=(@itno)";
                da.Fill(dtD);
            }
            dtD.DefaultView.Sort = "sadate DESC,sano ASC";
            if (dtD.Rows.Count > 0)
            {
                dataGridViewT1.DataSource = dtD.DefaultView;
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
