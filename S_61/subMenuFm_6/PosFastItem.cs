using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class PosFastItem : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable fastItem = new DataTable();
        DataTable posPlu = new DataTable();
        DataTable posKind = new DataTable();
        TextBox tno = new TextBox();
        TextBox tname = new TextBox();

        public PosFastItem()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            cn1.ConnectionString = Common.sqlConnString;
             
        }

        private void PosFastItem_Load(object sender, EventArgs e)
        {
            table1();
            table2();
            table3();

            dataGridViewT1.ReadOnly = false;
            dataGridViewT1.Columns["序號"].ReadOnly = true;
            dataGridViewT2.ReadOnly = false;
            dataGridViewT2.Columns["序號2"].ReadOnly = true;
            dataGridViewT3.ReadOnly = false;
            dataGridViewT3.Columns["序號3"].ReadOnly = true;
        }

        void table1()
        {
            dataGridViewT1.DataSource = null;
            fastItem.Clear();
            da1.Fill(fastItem);
            if (fastItem.Rows.Count < 7)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (fastItem.Rows.Count > i) continue;
                    DataRow row = fastItem.NewRow();
                    row["pno"] = i + 1;
                    fastItem.Rows.Add(row);
                }
            }
            fastItem = fastItem.AsEnumerable().OrderBy(r => r["pno"].ToDecimal()).CopyToDataTable();
            dataGridViewT1.DataSource = fastItem;
        }

        void table2()
        {
            dataGridViewT2.DataSource = null;
            posPlu.Clear();
            da2.Fill(posPlu);
            if (posPlu.Rows.Count < 41)
            {
                for (int i = 0; i < 41; i++)
                {
                    if (posPlu.Rows.Count > i) continue;
                    DataRow row = posPlu.NewRow();
                    row["pno"] = i + 1;
                    posPlu.Rows.Add(row);
                }
            }
            posPlu = posPlu.AsEnumerable().OrderBy(r => r["pno"].ToDecimal()).CopyToDataTable();
            dataGridViewT2.DataSource = posPlu;
        }

        void table3()
        {
            dataGridViewT3.DataSource = null;
            posKind.Clear();
            da3.Fill(posKind);
            if (posKind.Rows.Count < 41)
            {
                for (int i = 0; i < 41; i++)
                {
                    if (posKind.Rows.Count > i) continue;
                    DataRow row = posKind.NewRow();
                    row["pno"] = i + 1;
                    posKind.Rows.Add(row);
                }
            }
            posKind = posKind.AsEnumerable().OrderBy(r => r["pno"].ToDecimal()).CopyToDataTable();
            dataGridViewT3.DataSource = posKind;
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "itno")
            {
                xe.DataGridViewOpen<JBS.JS.Item>(sender, e, fastItem, row =>
                {
                    fastItem.Rows[e.RowIndex]["itno"] = row["ItNo"].ToString();
                    fastItem.Rows[e.RowIndex]["itname"] = row["ItName"].ToString();
                    fastItem.Rows[e.RowIndex]["pname"] = row["ItName"].ToString();
                });
            }
        }

        private void dataGridViewT2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT2.Columns[e.ColumnIndex].Name == "itno2")
            {
                xe.DataGridViewOpen<JBS.JS.Item>(sender, e, posPlu, row =>
                {
                    posPlu.Rows[e.RowIndex]["ItNo"] = row["ItNo"].ToString();
                    posPlu.Rows[e.RowIndex]["ItName"] = row["ItName"].ToString();
                    posPlu.Rows[e.RowIndex]["pname"] = row["ItName"].ToString();
                });
            }
        }

        private void dataGridViewT3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT3.Columns[e.ColumnIndex].Name == "kino")
            {
                xe.DataGridViewOpen<JBS.JS.Kind>(sender, e, posKind, row =>
                {
                    posKind.Rows[e.RowIndex]["kino"] = row["kino"].ToString();
                    posKind.Rows[e.RowIndex]["kiname"] = row["kiname"].ToString();
                    posKind.Rows[e.RowIndex]["pname"] = row["kiname"].ToString();
                });
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cn1.State != ConnectionState.Open) cn1.Open();
                using (SqlCommand cmd = cn1.CreateCommand())
                {
                    cmd.CommandText = "delete from PosFastItem";
                    cmd.ExecuteNonQuery();
                }
                fastItem.AcceptChanges();
                for (int i = 0; i < fastItem.Rows.Count; i++)
                {
                    fastItem.Rows[i].SetAdded();
                }
                da1.Update(fastItem);
                table1();
                MessageBox.Show("儲存完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt2Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (cn1.State != ConnectionState.Open) cn1.Open();
                using (SqlCommand cmd = cn1.CreateCommand())
                {
                    cmd.CommandText = "delete from posplu";
                    cmd.ExecuteNonQuery();
                }
                posPlu.AcceptChanges();
                for (int i = 0; i < posPlu.Rows.Count; i++)
                {
                    posPlu.Rows[i].SetAdded();
                }
                da2.Update(posPlu);
                table2();
                MessageBox.Show("儲存完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt3Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (cn1.State != ConnectionState.Open) cn1.Open();
                using (SqlCommand cmd = cn1.CreateCommand())
                {
                    cmd.CommandText = "delete from posKind";
                    cmd.ExecuteNonQuery();
                }
                posKind.AcceptChanges();
                for (int i = 0; i < posKind.Rows.Count; i++)
                {
                    posKind.Rows[i].SetAdded();
                }
                da3.Update(posKind);
                table3();
                MessageBox.Show("儲存完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            { 
                cn1.Close();
                this.Dispose();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewT1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "itno")
            {
                tno.Text = dataGridViewT1["itno", e.RowIndex].EditedFormattedValue.ToString();

                xe.Validate<JBS.JS.Item>(tno.Text, r =>
                {
                    dataGridViewT1["itno", e.RowIndex].Value = r["itno"];
                    dataGridViewT1["itname", e.RowIndex].Value = r["itname"];
                    dataGridViewT1["pname", e.RowIndex].Value = r["itname"];
                }, () =>
                {
                    dataGridViewT1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    dataGridViewT1["itno", e.RowIndex].Value = "";
                    dataGridViewT1["itname", e.RowIndex].Value = "";
                    dataGridViewT1["pname", e.RowIndex].Value = "";
                });

            }
        }

        private void dataGridViewT2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT2.Columns[e.ColumnIndex].Name == "itno2")
            {
                tno.Text = dataGridViewT2["itno2", e.RowIndex].EditedFormattedValue.ToString();

                xe.Validate<JBS.JS.Item>(tno.Text, r =>
                {
                    dataGridViewT2["itno2", e.RowIndex].Value = r["itno"];
                    dataGridViewT2["itname2", e.RowIndex].Value = r["itname"];
                    dataGridViewT2["pname2", e.RowIndex].Value = r["itname"];
                }, () =>
                {
                    dataGridViewT2.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    dataGridViewT2["itno2", e.RowIndex].Value = "";
                    dataGridViewT2["itname2", e.RowIndex].Value = "";
                    dataGridViewT2["pname2", e.RowIndex].Value = "";
                });

            }
        }

        private void dataGridViewT3_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewT3.Columns[e.ColumnIndex].Name == "kino")
            {
                tno.Text = dataGridViewT3["kino", e.RowIndex].EditedFormattedValue.ToString();
                tname.Text = dataGridViewT3["kiname", e.RowIndex].EditedFormattedValue.ToString();
                if (!pVar.KindValidate(tno.Text, tno, tname))
                {
                    dataGridViewT3["kino", e.RowIndex].Value = "";
                    dataGridViewT3["kiname", e.RowIndex].Value = "";
                    dataGridViewT3["pname3", e.RowIndex].Value = "";
                    dataGridViewT3.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
                else
                {
                    dataGridViewT3["kino", e.RowIndex].Value = tno.Text;
                    dataGridViewT3["kiname", e.RowIndex].Value = tname.Text;
                    dataGridViewT3["pname3", e.RowIndex].Value = tname.Text;
                    dataGridViewT3.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void PosFastItem_Shown(object sender, EventArgs e)
        {
            btnExit.Focus();
        }






































    }
}
