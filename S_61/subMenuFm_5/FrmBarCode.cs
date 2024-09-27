using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmBarCode : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmBarCode()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmBarCode_Load(object sender, EventArgs e)
        {
            radioT3.Checked = true;
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
            string sql = "select 列印份數=0,張數=1,kiname as 類別名稱,item.* from Item left join kind on item.kino = kind.kino where 0=0 ";

            DataTable t = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ItNo", ItNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItNo1", ItNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItNoUdf", ItNoUdf.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItNoUdf1", ItNoUdf1.Text.Trim());
                    cmd.Parameters.AddWithValue("@KiNo", KiNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@KiNo1", KiNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItName", ItName.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItDesp", ItDesp.Text.Trim());


                    if (ItNo.Text.Trim().Length > 0) sql += " and ItNo >= @ItNo";
                    if (ItNo1.Text.Trim().Length > 0) sql += " and ItNo <= @ItNo1";
                    if (ItNoUdf.Text.Trim().Length > 0) sql += " and ItNoUdf >= @ItNoUdf";
                    if (ItNoUdf1.Text.Trim().Length > 0) sql += " and ItNoUdf <= @ItNoUdf1";
                    if (KiNo.Text.Trim().Length > 0) sql += " and item.KiNo >= @KiNo";
                    if (KiNo1.Text.Trim().Length > 0) sql += " and item.KiNo <= @KiNo1";

                    if (ItName.Text.Trim().Length > 0) sql += " and ItName like @ItName +'%'";
                    if (ItDesp.Text.Trim().Length > 0) sql += " and ItDesp1 like @ItDesp + '%'";

                    sql += " order by itno";
                    cmd.CommandText = sql;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(t);
                    }
                    cmd.Dispose();
                }
                if (t.Rows.Count > 0)
                {
                    using (var frm = new FrmBarCodeBrow())
                    { 
                        frm.dt = t.Copy();
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }
    }
}
