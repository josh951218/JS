using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class 批次修改作業 : Formbase
    {
        JBS.JS.xEvents xe;
        DataTable dt = new DataTable();
        List<ButtonSmallT> query;

        public 批次修改作業()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            this.進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.包裝進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.售價.DefaultCellStyle.Format = "f" + Common.MF;
            this.包裝售價.DefaultCellStyle.Format = "f" + Common.MF;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            query = new List<ButtonSmallT>() { query2, query3, query4 };
            tn1.FirstNum = 15;
            tn1.LastNum = 4;
        }

        private void 批次修改作業_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            tn1.ReadOnly = false;
            tn1.Text = "0";
            load();
        }

        void load()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "select 點選='',* from item order by itno";
                using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                {
                    dt.Clear();
                    dd.Fill(dt);
                }
                cmd.Dispose();
            }
            dataGridViewT1.SuspendLayout();
            dataGridViewT1.DataSource = null;
            dataGridViewT1.DataSource = dt;
            dataGridViewT1.ResumeLayout();
            query.ForEach(r => r.ForeColor = SystemColors.ControlText);
            query2.ForeColor = Color.Red;
        }

        void SetColor(string order)
        {
            query.ForEach(r => r.ForeColor = SystemColors.ControlText);
            dataGridViewT1.SuspendLayout();
            dataGridViewT1.DataSource = null;
            dt = dt.AsEnumerable().OrderBy(r => r[order].ToString()).CopyToDataTable();
            dataGridViewT1.DataSource = dt;
            dataGridViewT1.ResumeLayout();
        }

        private void query2_Click(object sender, EventArgs e)
        {
            SetColor("itno");
            query2.ForeColor = Color.Red;
        }

        private void query3_Click(object sender, EventArgs e)
        {
            SetColor("itname");
            query3.ForeColor = Color.Red;
        }

        private void query4_Click(object sender, EventArgs e)
        {
            SetColor("kino");
            query4.ForeColor = Color.Red;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            dt.AsEnumerable().ToList().ForEach(r => r["點選"] = "V");
        }

        private void btnAllCancel_Click(object sender, EventArgs e)
        {
            dt.AsEnumerable().ToList().ForEach(r => r["點選"] = "");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (qItNo.Text != "")
            {
                SetColor("itno");
                query2.ForeColor = Color.Red;
                dataGridViewT1.Search("產品編號", qItNo.Text.Trim());
            }
            else if (qItName.Text != "")
            {
                query.ForEach(r => r.ForeColor = SystemColors.ControlText);
                query3.ForeColor = Color.Red;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Parameters.AddWithValue("itname", qItName.Text.Trim());
                    cmd.CommandText = "select 點選='',* from item where itname like '%'+@itname+'%' order by itname";
                    using (SqlDataAdapter dd = new SqlDataAdapter(cmd))
                    {
                        dataGridViewT1.SuspendLayout();
                        dataGridViewT1.DataSource = null;
                        dt.Clear();
                        dd.Fill(dt);
                        dataGridViewT1.DataSource = dt;
                        dataGridViewT1.ResumeLayout();
                    }
                }
            }
            else if (qItNo.Text.Trim() == "" && qItName.Text.Trim() == "")
            {
                load();
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dt.AsEnumerable().Count(r => r["點選"].ToString() == "V") == 0)
            {
                MessageBox.Show("尚未點選更改產品", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable temp = dt.AsEnumerable().Where(r => r["點選"].ToString() == "V").CopyToDataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = tran;
                string kind = "";
                try
                {
                    //類別編號 = 0
                    //進價     = 1  
                    //售價     = 2
                    //包裝進價 = 3
                    //包裝售價 = 4
                    //單位     = 5
                    //包裝單位 = 6
                    //包裝數量 = 7
                    //經銷商   = 8
                    //注音速查 = 9
                    int index = comboBox1.SelectedIndex;
                    if (index == 0)
                    {
                        kind = "kino";
                        cmd.Parameters.AddWithValue("kind", tb1.Text.GetUTF8(4).Trim());
                    }
                    else if (index == 1)
                    {
                        kind = "itbuypri";
                        cmd.Parameters.AddWithValue("kind", tn1.Text.ToDecimal("f" + Common.M));
                    }
                    else if (index == 2)
                    {
                        kind = "itprice";
                        cmd.Parameters.AddWithValue("kind", tn1.Text.ToDecimal("f" + Common.MS));
                    }
                    else if (index == 3)
                    {
                        kind = "itbuyprip";
                        cmd.Parameters.AddWithValue("kind", tn1.Text.ToDecimal("f" + Common.M));
                    }
                    else if (index == 4)
                    {
                        kind = "itpricep";
                        cmd.Parameters.AddWithValue("kind", tn1.Text.ToDecimal("f" + Common.MS));
                    }
                    else if (index == 5)
                    {
                        kind = "itunit";
                        cmd.Parameters.AddWithValue("kind", tb1.Text.GetUTF8(4).Trim());
                    }
                    else if (index == 6)
                    {
                        kind = "itunitp";
                        cmd.Parameters.AddWithValue("kind", tb1.Text.GetUTF8(4).Trim());
                    }
                    else if (index == 7)
                    {
                        kind = "itpkgqty";
                        cmd.Parameters.AddWithValue("kind", tn1.Text.ToDecimal("f" + Common.Q));
                    }
                    else if (index == 8)
                    {
                        kind = "fano";
                        cmd.Parameters.AddWithValue("kind", tb1.Text.GetUTF8(10).Trim());
                    }
                    else if (index == 9)
                    {
                        kind = "ItIme";
                        cmd.Parameters.AddWithValue("kind", tb1.Text.GetUTF8(20).Trim());
                    }

                    cmd.Parameters.Add("itno", SqlDbType.NVarChar);
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        cmd.Parameters["itno"].Value = temp.Rows[i]["itno"].ToString().Trim();
                        cmd.CommandText = "update item set " + kind + "=@kind where itno=@itno";
                        cmd.ExecuteNonQuery();
                    }


                    tran.Commit();
                    cmd.Dispose();
                    tran.Dispose();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.ToString());
                }
            }
            load();
            tb1.Text = tb2.Text = "";
            tn1.Text = "0";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBoxT1_DoubleClick(object sender, EventArgs e)
        {
            //類別編號 = 0
            //進價     = 1  
            //售價     = 2
            //包裝進價 = 3
            //包裝售價 = 4
            //單位     = 5
            //包裝單位 = 6
            //包裝數量 = 7
            //經銷商   = 8
            int index = comboBox1.SelectedIndex;
            if (index == 0)
            {
                xe.Open<JBS.JS.Kind>(tb1, row =>
                {
                    tb1.Text = row["kino"].ToString().Trim();
                    tb2.Text = row["kiname"].ToString().Trim();
                });
            }
            else if (index == 8)
            {
                xe.Open<JBS.JS.Fact>(tb1, row =>
                {
                    tb1.Text = row["FaNo"].ToString();
                    tb2.Text = row["FaName1"].ToString();
                });
            }
        }

        private void textBoxT1_Validating(object sender, CancelEventArgs e)
        {
            if (btnCancel.Focused) return;
            //類別編號 = 0
            //進價     = 1  
            //售價     = 2
            //包裝進價 = 3
            //包裝售價 = 4
            //單位     = 5
            //包裝單位 = 6
            //包裝數量 = 7
            //經銷商   = 8
            int index = comboBox1.SelectedIndex;
            if (tb1.Text.Trim() == "")
            {
                tb1.Text = "";
                tb2.Text = "";
                return;
            }


            if (index == 0)
            {
                xe.ValidateOpen<JBS.JS.Kind>(tb1, e, row =>
                {
                    tb1.Text = row["kino"].ToString().Trim();
                    tb2.Text = row["kiname"].ToString().Trim();
                });
            }
            else if (index == 8)
            {
                xe.ValidateOpen<JBS.JS.Fact>(tb1, e, row =>
                {
                    tb1.Text = row["FaNo"].ToString();
                    tb2.Text = row["FaName1"].ToString();
                });
            }
        }

        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            if (dataGridViewT1.Columns[e.ColumnIndex].Name != "點選") return;
            DataGridViewCell cell = dataGridViewT1["點選", e.RowIndex];
            cell.Value = (cell.Value.ToString() == "V") ? "" : "V";
            dataGridViewT1.InvalidateRow(e.RowIndex);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.ToString().StartsWith("F2") && keyData.ToString().EndsWith("Shift"))
            {
                query2.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F3") && keyData.ToString().EndsWith("Shift"))
            {
                query3.PerformClick();
            }
            else if (keyData.ToString().StartsWith("F4") && keyData.ToString().EndsWith("Shift"))
            {
                query4.PerformClick();
            }
            else if (keyData == Keys.F6)
            {
                btnQuery.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
         
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //類別編號 = 0
            //進價     = 1  
            //售價     = 2
            //包裝進價 = 3
            //包裝售價 = 4
            //單位     = 5
            //包裝單位 = 6
            //包裝數量 = 7
            //經銷商   = 8
            //注音速查 = 9
            tb1.MaxLength = 10;

            int index = comboBox1.SelectedIndex;
            if (index == 0)
            {
                tb1.Visible = tb2.Visible = true;
                tn1.Visible = false;
                tb1.Focus();
            }
            else if (index == 1)
            {
                tb1.Visible = tb2.Visible = false;
                tn1.Visible = true;
                tn1.Focus();
            }
            else if (index == 2)
            {
                tb1.Visible = tb2.Visible = false;
                tn1.Visible = true;
                tn1.Focus();
            }
            else if (index == 3)
            {
                tb1.Visible = tb2.Visible = false;
                tn1.Visible = true;
                tn1.Focus();
            }
            else if (index == 4)
            {
                tb1.Visible = tb2.Visible = false;
                tn1.Visible = true;
                tn1.Focus();
            }
            else if (index == 5)
            {
                tb1.Visible = true;
                tb2.Visible = false;
                tn1.Visible = false;
                tb1.Focus();
            }
            else if (index == 6)
            {
                tb1.Visible = true;
                tb2.Visible = false;
                tn1.Visible = false;
                tb1.Focus();
            }
            else if (index == 7)
            {
                tb1.Visible = tb2.Visible = false;
                tn1.Visible = true;
                tn1.Focus();
            }
            else if (index == 8)
            {
                tb1.Visible = tb2.Visible = true;
                tn1.Visible = false;
                tb1.Focus();
            }
            else if (index == 9)
            {
                tb1.MaxLength = 20;
                tb1.Visible = true;
                tb2.Visible = false;
                tn1.Visible = false;
                tb1.Focus();
            }
        }



    }
}
