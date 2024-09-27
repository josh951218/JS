using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmSpecial_Print_SpNo : Formbase
    {
        DataTable dtM = new DataTable();
        DataTable dtD = new DataTable();
        public string result = "";

        public FrmSpecial_Print_SpNo()
        {
            InitializeComponent();
        }

        private void FrmSpecial_Print_SpNo_Load(object sender, EventArgs e)
        {
            this.特價起始日.DataPropertyName = Common.User_DateTime == 1 ? "SDate" : "SDate1";
            this.特價終止日.DataPropertyName = Common.User_DateTime == 1 ? "EDate" : "EDate1";
             
            LoadM();
        }

        void LoadM()
        {
            dtM.Clear();
            dtD.Clear();
            dataGridViewT1.DataSource = null;
            dataGridViewT2.DataSource = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from special";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(dtM);
                        if (dtM.Rows.Count > 0) dataGridViewT1.DataSource = dtM;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void LoadD(string spno)
        {
            dtD.Clear();
            dataGridViewT2.DataSource = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from speciald where spno =@spno";
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("spno", spno);
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtD);
                            if (dtD.Rows.Count > 0)
                            {
                                dataGridViewT2.DataSource = dtD;
                                for (int i = 0; i < dataGridViewT2.Rows.Count; i++)
                                {
                                    dataGridViewT2["序號", i].Value = (i + 1).ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (index == -1) return;
                else
                {
                    var spno = dataGridViewT1["特價單號", index].Value.ToString().Trim();
                    LoadD(spno);
                }
            }
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            dataGridViewT1.Search("特價單號", textBoxT1.Text.Trim());
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            var p = dataGridViewT1.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            if (p.IsNotNull()) result = p.Cells["特價單號"].Value.ToString().Trim();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnBrowT3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnBrowT2_Click(null, null);
        }
    }
}
