﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmSaddr : Formbase
    {
        public string TAddr = "";
        public string TZip = "";
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        List<DataRow> list = new List<DataRow>();
        List<Button> ltbtn; 
        int cityLength = 0;
        int roadLength = 0;

        public FrmSaddr()
        {
            InitializeComponent();
            ltbtn = new List<Button>() { button0, button1, button2, button3, button4, button5, button6, button7, button8, button9, button10 };
            btnChange.Tag = 1;
        }

        private void FrmSaddr_Load(object sender, EventArgs e)
        {  
            //gridView欄位寬度設定
            dataGridViewT1.Columns["city"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewT1.Columns["city"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewT1.Columns["city"].Width = dataGridViewT1.Width - 30;
            //gridView欄位寬度設定
            dataGridViewT2.Columns["area"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewT2.Columns["area"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewT2.Columns["area"].Width = dataGridViewT2.Width / 2 + 20;
            dataGridViewT2.Columns["zip"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewT2.Columns["zip"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewT2.Columns["zip"].Width = dataGridViewT2.Width / 2 - 45;
            //gridView欄位寬度設定
            dataGridViewT3.Columns["road"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewT3.Columns["road"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewT3.Columns["road"].Width = dataGridViewT3.Width - 30;

            //載入DB,秀出第一筆資料
            loadDB();
            if (dt.Rows.Count > 0)
            {
                writeToTxt();
            }
        }

        private void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string str = "select city from Saddr1";
                    SqlDataAdapter da = new SqlDataAdapter(str, conn);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        list.Clear();
                        list = dt.AsEnumerable().ToList();
                    }
                    else
                    {
                        list.Clear();
                    }
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void writeToTxt()
        {
            if (dt.Rows.Count > 0)
            {
                dt = list.OrderBy(r => r["city"]).CopyToDataTable();
                dataGridViewT1.DataSource = dt;
            }
            else
            {
                dataGridViewT1.DataSource = null;
            }
        }

        private void fillAdd2()
        {
            if (dt2.Rows.Count > 0)
            {
                dataGridViewT2.DataSource = dt2;
            }
            else
            {
                dataGridViewT2.DataSource = null;
            }
        }

        private void fillAdd3()
        {
            if (dt3.Rows.Count > 0)
            {
                dataGridViewT3.DataSource = dt3;
            }
            else
            {
                dataGridViewT3.DataSource = null;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;//過慮header點擊事件
            string city = dataGridViewT1.Rows[e.RowIndex].Cells[0].Value.ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@city", city);
                    string str = "select area,zip from Saddr2 where city=@city order by zip";
                    cmd.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt2.Clear();
                    da.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        fillAdd2();
                        CuAdd.Text = city.Trim();
                        cityLength = CuAdd.Text.Length;
                    }
                    da.Dispose();
                }
                dt3.Clear();
                dataGridViewT3.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;//過慮header點擊事件
            string area = dataGridViewT2.Rows[e.RowIndex].Cells[0].Value.ToString();
            string zip = dataGridViewT2.Rows[e.RowIndex].Cells[1].Value.ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@zip", zip);

                    string str = "select road from Saddr3 where zip=@zip order by road";

                    cmd.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt3.Clear();
                    da.Fill(dt3);
                    if (dt3.Rows.Count > 0)
                    {
                        fillAdd3();
                        CuAdd.Text = CuAdd.Text.Substring(0, cityLength);
                        CuAdd.Text += area.Trim();
                        roadLength = CuAdd.Text.Length;
                        CuZip.Text = zip.Trim();
                    }
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;//過慮header點擊事件
            string road = dataGridViewT3.Rows[e.RowIndex].Cells[0].Value.ToString();
            CuAdd.Text = CuAdd.Text.Substring(0, roadLength);
            CuAdd.Text += road.Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CuAdd.Text += ((Button)sender).Text;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CuAdd.Text = "";
            CuZip.Text = "";
            cityLength = 0;
            roadLength = 0;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            TAddr = CuAdd.Text;
            TZip = CuZip.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string[] s1 = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            string[] s2 = new string[] { "０", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
            string[] s3 = new string[] { "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖", "拾" };
            if (btnChange.Tag.ToDecimal() == 1)
            {
                btnChange.Tag = 2;
                ltbtn.ForEach(t => t.Text = s2[(int)t.Name.skipString(6).ToDecimal()]);
                ltbtn.ForEach(t => t.ForeColor = Color.OrangeRed);
                btnChange.ForeColor = Color.OrangeRed;
            }
            else if (btnChange.Tag.ToDecimal() == 2)
            {
                btnChange.Tag = 3;
                ltbtn.ForEach(t => t.Text = s3[(int)t.Name.skipString(6).ToDecimal()]);
                ltbtn.ForEach(t => t.ForeColor = Color.Red);
                btnChange.ForeColor = Color.Red;
            }
            else if (btnChange.Tag.ToDecimal() == 3)
            {
                btnChange.Tag = 1;
                ltbtn.ForEach(t => t.Text = s1[(int)t.Name.skipString(6).ToDecimal()]);
                ltbtn.ForEach(t => t.ForeColor = SystemColors.HotTrack);
                btnChange.ForeColor = SystemColors.HotTrack;
            }
        }
    }
}
