using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using S_61.Basic;

namespace S_61.S0
{
    public partial class FrmPrintBarCode : JE.MyControl.Formbase
    {
        JBS.JS.xEvents xe;

        public FrmPrintBarCode()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmPrintBarCode_Load(object sender, EventArgs e)
        {

        }
         
        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (ItNo.Text.Trim().BigThen(ItNo1.Text.Trim()))
            {
                MessageBox.Show("起始產品編號大於終止產品編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ItNoUdf.Text.Trim().BigThen(ItNoUdf1.Text.Trim()))
            {
                MessageBox.Show("起始自訂編號大於終止自訂編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (KiNo.Text.Trim().BigThen(KiNo1.Text.Trim()))
            {
                MessageBox.Show("起始產品類別編號大於終止產品類別編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable temp = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ItNo", ItNo.Text.Trim());
                cmd.Parameters.AddWithValue("@ItNo1", ItNo1.Text.Trim());
                cmd.Parameters.AddWithValue("@ItNoUdf", ItNoUdf.Text.Trim());
                cmd.Parameters.AddWithValue("@ItNoUdf1", ItNoUdf1.Text.Trim());
                cmd.Parameters.AddWithValue("@KiNo", KiNo.Text.Trim());
                cmd.Parameters.AddWithValue("@KiNo1", KiNo1.Text.Trim());

                string sql = @"  Select 列印份數=0,張數=1,kind.kiname as 類別名稱,item.*,fact.faname1,fact.faname2
                               , shopd_bsdate1='',shopd_bsdate2='', shopd_qty='',shopd_itunit='',shopd_memo=''
                               From Item left join kind on item.kino = kind.kino 
                               left join fact on item.fano = fact.fano
                               Where 0=0 ";

                if (ItNo.Text.Trim().Length > 0) sql += " and ItNo >= @ItNo";
                if (ItNo1.Text.Trim().Length > 0) sql += " and ItNo <= @ItNo1";
                if (ItNoUdf.Text.Trim().Length > 0) sql += " and ItNoUdf >= @ItNoUdf";
                if (ItNoUdf1.Text.Trim().Length > 0) sql += " and ItNoUdf <= @ItNoUdf1";
                if (KiNo.Text.Trim().Length > 0) sql += " and item.KiNo >= @KiNo";
                if (KiNo1.Text.Trim().Length > 0) sql += " and item.KiNo <= @KiNo1";

                sql += " order by itno";
                cmd.CommandText = sql;

                da.Fill(temp);
            }

            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            using (var frm = new FrmPrintBarCodeb())
            {
                frm.dt = temp.Copy();
                frm.ShowDialog();
            }
        } 
    }
}
