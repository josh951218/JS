using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmItemTempPrice : JE.MyControl.Formbase
    {
        DataTable dt = new DataTable();

        public FrmItemTempPrice()
        {
            InitializeComponent();
            this.Controls
                .OfType<JE.MyControl.TextBoxNumberT>()
                .AsParallel()
                .ForAll(t =>
                {
                    t.FirstNum = 19 - Common.MS;
                    t.LastNum = Common.MS;
                });
        }

        private void FrmItemTempPrice_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = "Select * from itemtempprice";
                da.Fill(dt);
            }

            dataGridViewT1.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                dataGridViewT1.FirstDisplayedScrollingRowIndex = 0;
                dataGridViewT1.CurrentCell = dataGridViewT1[0, 0];
                dataGridViewT1.Rows[0].Selected = true;
            }
        }

        void WriteToText(int index)
        {
            textBoxT1.Text = dt.Rows[index]["itno"].ToString().Trim();
            textBoxT2.Text = dt.Rows[index]["itname"].ToString().Trim();
            textBoxT3.Text = dt.Rows[index]["itunitp"].ToString().Trim();
            textBoxT4.Text = dt.Rows[index]["itunit"].ToString().Trim();

            itprice.Text = dt.Rows[index]["itprice"].ToDecimal().ToString("f" + Common.MS);
            itprice1.Text = dt.Rows[index]["itprice1"].ToDecimal().ToString("f" + Common.MS);
            itprice2.Text = dt.Rows[index]["itprice2"].ToDecimal().ToString("f" + Common.MS);
            itprice3.Text = dt.Rows[index]["itprice3"].ToDecimal().ToString("f" + Common.MS);
            itprice4.Text = dt.Rows[index]["itprice4"].ToDecimal().ToString("f" + Common.MS);
            itprice5.Text = dt.Rows[index]["itprice5"].ToDecimal().ToString("f" + Common.MS);

            itpricep.Text = dt.Rows[index]["itpricep"].ToDecimal().ToString("f" + Common.MS);
            itpricep1.Text = dt.Rows[index]["itpricep1"].ToDecimal().ToString("f" + Common.MS);
            itpricep2.Text = dt.Rows[index]["itpricep2"].ToDecimal().ToString("f" + Common.MS);
            itpricep3.Text = dt.Rows[index]["itpricep3"].ToDecimal().ToString("f" + Common.MS);
            itpricep4.Text = dt.Rows[index]["itpricep4"].ToDecimal().ToString("f" + Common.MS);
            itpricep5.Text = dt.Rows[index]["itpricep5"].ToDecimal().ToString("f" + Common.MS);
        }

        void Clear()
        {
            textBoxT1.Text = "";
            textBoxT2.Text = "";
            textBoxT3.Text = "";
            textBoxT4.Text = "";

            itprice.Text = (0M).ToString("f" + Common.MS);
            itprice1.Text = (0M).ToString("f" + Common.MS);
            itprice2.Text = (0M).ToString("f" + Common.MS);
            itprice3.Text = (0M).ToString("f" + Common.MS);
            itprice4.Text = (0M).ToString("f" + Common.MS);
            itprice5.Text = (0M).ToString("f" + Common.MS);

            itpricep.Text = (0M).ToString("f" + Common.MS);
            itpricep1.Text = (0M).ToString("f" + Common.MS);
            itpricep2.Text = (0M).ToString("f" + Common.MS);
            itpricep3.Text = (0M).ToString("f" + Common.MS);
            itpricep4.Text = (0M).ToString("f" + Common.MS);
            itpricep5.Text = (0M).ToString("f" + Common.MS);
        }

        private void ItNo_TextChanged(object sender, EventArgs e)
        {
            if (ItNo.TrimTextLenth() == 0
                && ItName.TrimTextLenth() == 0)
            {
                dt.DefaultView.RowFilter = "";
                return;
            }

            if (ItNo.TrimTextLenth() > 0)
            {
                dt.DefaultView.RowFilter = "";
                dataGridViewT1.Search("產品編號", ItNo.Text.Trim());
            }
            else if (ItName.TrimTextLenth() > 0)
            {
                dt.DefaultView.RowFilter = "itname like '" + ItName.Text.Trim().Replace("'", "") + "%'";
                if (dt.DefaultView.Count > 0)
                {
                    var itno = dt.DefaultView[0]["itno"].ToString().Trim(); 
                    var index = -1;
                    System.Threading.Tasks.Parallel.For(0, dt.Rows.Count, (i, state) =>
                    {
                        if (dt.Rows[i]["itno"].ToString().Trim() == itno)
                        {
                            index = i;
                            state.Break();
                        }
                    });

                    dt.DefaultView.RowFilter = "";
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                    dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
                    dataGridViewT1.Rows[index].Selected = true;
                } 
            }

            //if (dataGridViewT1.Rows.Count == 0)
            //    dt.DefaultView.RowFilter = "";

            //var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            //if (index == -1)
            //    this.Clear();
            //else
            //    WriteToText(index);
        }

        private void buttonSmallT1_Click(object sender, EventArgs e)
        {
            var dl = MessageBox.Show("請確定要重新產生底稿!!!", "", MessageBoxButtons.YesNo);
            if (dl != DialogResult.Yes)
                return;

            dt.Clear();
            this.Clear();

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                cmd.CommandText = "Delete from itemtempprice";
                cmd.ExecuteNonQuery();

                var columns = @"itno,itname,itunit,itunitp,itprice,itprice1,itprice2,itprice3,itprice4,itprice5
                                ,itpricep,itpricep1,itpricep2,itpricep3,itpricep4,itpricep5 ";

                cmd.CommandText = @"INSERT INTO itemtempprice (" + columns + @") SELECT " + columns + @" FROM item;";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Select * from itemtempprice";
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
                WriteToText(0);

            MessageBox.Show("完成!");
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
            {
                this.Clear();
                return;
            }

            if (dataGridViewT1.SelectedRows != null && dataGridViewT1.SelectedRows[0].Index == index)
            {
                WriteToText(index);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void xNumberT_Validating(object sender, CancelEventArgs e)
        {
            var itno = textBoxT1.Text.Trim();
            if (itno.Length == 0)
                return;

            var tb = sender as JE.MyControl.TextBoxNumberT;
            using (var db = new JBS.xSQL())
            {
                db.ExecuteNonQuery("Update itemtempprice set " + tb.Name + " = @p where itno = @itno ", spc =>
                {
                    spc.AddWithValue("p", tb.Text.ToDecimal());
                    spc.AddWithValue("itno", itno);
                });
            }

            var index = -1;
            System.Threading.Tasks.Parallel.For(0, dt.Rows.Count, (i, state) =>
            {
                if (dt.Rows[i]["itno"].ToString().Trim() == itno)
                {
                    index = i;
                    state.Break();
                }
            });

            if (index >= 0)
            {
                dt.Rows[index][tb.Name] = tb.Text.ToDecimal();
            }
        }

        private void buttonSmallT2_Click(object sender, EventArgs e)
        {
            var dl = MessageBox.Show("請確定是否批次更新!!!", "", MessageBoxButtons.YesNo);
            if (dl != DialogResult.Yes)
                return;

            using (var db = new JBS.xSQL())
            {
                db.ExecuteNonQuery(@"
                Update item set
                 item.itprice   = itemtempprice.itprice
                ,item.itprice1  = itemtempprice.itprice1
                ,item.itprice2  = itemtempprice.itprice2
                ,item.itprice3  = itemtempprice.itprice3
                ,item.itprice4  = itemtempprice.itprice4
                ,item.itprice5  = itemtempprice.itprice5
                ,item.itpricep  = itemtempprice.itpricep
                ,item.itpricep1 = itemtempprice.itpricep1
                ,item.itpricep2 = itemtempprice.itpricep2
                ,item.itpricep3 = itemtempprice.itpricep3
                ,item.itpricep4 = itemtempprice.itpricep4
                ,item.itpricep5 = itemtempprice.itpricep5
                from item 
                inner join itemtempprice on item.itno = itemtempprice.itno ", spc => spc.AddWithValue("x", "x"));
            }

            MessageBox.Show("完成!");
        }
    }
}
