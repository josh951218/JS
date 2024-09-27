using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmItemOut : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmItemOut()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmItemOut_Load(object sender, EventArgs e)
        {
            radioT4.Checked = true;
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void ItNo1_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void ItNoUdf_DoubleClick(object sender, EventArgs e)
        { 
            xe.Open<JBS.JS.Item>(sender, row =>
            {
                ItNoUdf.Text = row["ItNoUdf"].ToString().Trim();
            });
        }

        private void ItNoUdf1_DoubleClick(object sender, EventArgs e)
        {  
            xe.Open<JBS.JS.Item>(sender, row =>
            {
                ItNoUdf1.Text = row["ItNoUdf"].ToString().Trim();
            });
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        { 
            xe.Open<JBS.JS.Kind>(sender);
        }

        private void KiNo1_DoubleClick(object sender, EventArgs e)
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
                ItNo.Focus();
                return;
            }
            if (ItNoUdf.Text.Trim().BigThen(ItNoUdf1.Text.Trim()))
            {
                MessageBox.Show("起始自定編號大於終止自定編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNoUdf.Focus();
                return;
            }
            if (KiNo.Text.Trim().BigThen(KiNo1.Text.Trim()))
            {
                MessageBox.Show("起始類別編號大於終止類別編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                KiNo.Focus();
                return;
            }

            string str = "";
            if (ItNo.Text.Trim().Length > 0) str += " and ItNo >=@ItNo";
            if (ItNo1.Text.Trim().Length > 0) str += " and ItNo <=@ItNo1";
            if (ItNoUdf.Text.Trim().Length > 0) str += " and ItNoUdf >=@ItNoUdf";
            if (ItNoUdf1.Text.Trim().Length > 0) str += " and ItNoUdf <=@ItNoUdf1";
            if (KiNo.Text.Trim().Length > 0) str += " and KiNo >=@KiNo";
            if (KiNo1.Text.Trim().Length > 0) str += " and KiNo <=@KiNo1";
            if (radioT2.Checked) str += " and ittrait=2 ";
            if (radioT3.Checked) str += " and ittrait=3 ";
            if (radioT4.Checked) str += " and (ittrait=2 or ittrait=3) ";
            //組合品不盤點
            str += " and ittrait !=1 ";

            DataTable t = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select *,點選='',產品組成='' from item where 0=0 ";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        if (ItNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("ItNo", ItNo.Text.Trim());
                        if (ItNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("ItNo1", ItNo1.Text.Trim());
                        if (ItNoUdf.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("ItNoUdf", ItNoUdf.Text.Trim());
                        if (ItNoUdf1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("ItNoUdf1", ItNoUdf1.Text.Trim());
                        if (KiNo.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                        if (KiNo1.Text.Trim().Length > 0) cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());
                        cmd.CommandText = sql + str;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (t.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                using (var frm = new FrmItemOutb())
                { 
                    frm.dt = t.Copy();
                    frm.ShowDialog();
                }
            }
        }
    }
}
