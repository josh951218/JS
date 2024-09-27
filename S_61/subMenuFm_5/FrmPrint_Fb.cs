using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmPrint_Fb : Formbase
    {
        public DataTable table = new DataTable();
        DataTable dt = new DataTable();
        List<DataRow> list = new List<DataRow>();
        DataView dv = new DataView();
        bool flag = true;


        public FrmPrint_Fb()
        {
            InitializeComponent();
            Msg3.SetUserDefineRpt("廠商郵遞標籤_自定一.rpt");
            Msg4.SetUserDefineRpt("廠商郵遞標籤_自定二.rpt");
            SetRdUdf();
        }

        private void FrmPrint_Cb_Load(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.SetToolTip(Msg, "廠商郵遞標籤_信封");
            tip.SetToolTip(Msg1, "廠商郵遞標籤_1");
            tip.SetToolTip(Msg2, "廠商郵遞標籤_1_5");

            Print_F.Text = "1";
            if (table.Rows.Count > 0)
            {
                list = table.AsEnumerable().ToList();
                if (flag)
                {
                    table.Columns.Add();
                    flag = false;
                }
                dataGridViewT1.DataSource = table;
            }
            if (cbSort.Items.Count > 0) cbSort.SelectedIndex = 0;
        }

        private void FaPerCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (FaPerCheck.Checked)
            {
                FaPerSet.ReadOnly = false;
                FaPerSet.Focus();
                FaPerSet.SelectAll();
            }
            else
                FaPerSet.ReadOnly = true;
        }

        private void FaAddrCheck_CheckedChanged(object sender, EventArgs e)
        { 
            if (FaAddrCheck.Checked)
            {
                FaAddrSet.ReadOnly = false;
                FaAddrSet.Focus();
                FaAddrSet.SelectAll();
            }
            else
                FaAddrSet.ReadOnly = true;
        }
         
        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbSort.SelectedItem.ToString().Substring(0, 2))
            {
                case "01":
                    table.DefaultView.Sort = "FaNo ASC";
                    dataGridViewT1.DataSource = table;
                    break;
                case "02":
                    table.DefaultView.Sort = "FaName1 ASC";
                    dataGridViewT1.DataSource = table;
                    break;
                case "03":
                    table.DefaultView.Sort = "FaIme ASC";
                    dataGridViewT1.DataSource = table;
                    break;
                case "04":
                    table.DefaultView.Sort = "FaAddr1 ASC";
                    dataGridViewT1.DataSource = table;
                    break;
                case "05":
                    table.DefaultView.Sort = "FaX12No ASC";
                    dataGridViewT1.DataSource = table;
                    break;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridViewT1.Rows[i].Cells["Column1"].Value = "Y";
            }
        }

        private void btnSelectD_Click(object sender, EventArgs e)
        {
            for (int i = dataGridViewT1.CurrentCell.OwningRow.Index; i < table.Rows.Count; i++)
            {
                dataGridViewT1.Rows[i].Cells["Column1"].Value = "Y";
            }
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridViewT1.Rows[i].Cells["Column1"].Value = "N";
            }
        }

        private void btnCancelD_Click(object sender, EventArgs e)
        {
            for (int i = dataGridViewT1.CurrentCell.OwningRow.Index; i < table.Rows.Count; i++)
            {
                dataGridViewT1.Rows[i].Cells["Column1"].Value = "N";
            }
        }

        void GetFastList()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "select * from Fact where Fano=''";

                        for (int i = 0; i < dataGridViewT1.RowCount; i++)
                        {
                            if (dataGridViewT1.Rows[i].Cells["Column1"].Value.ToString() == "Y")
                            {
                                cmd.Parameters.AddWithValue(i.ToString(), dataGridViewT1.Rows[i].Cells["廠商編號"].Value.ToString());
                                cmd.CommandText += " or Fano=@" + i.ToString();
                            }
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadDBError:\n" + ex.ToString());
            }

        }

        void OutReport(RptMode mode)
        {
            GetFastList();

            var path = "";
            if (Msg.Checked) path = Common.reportaddress + "Report\\廠商郵遞標籤_信封.rpt";
            else if (Msg1.Checked) path = Common.reportaddress + "Report\\廠商郵遞標籤_1.rpt";
            else if (Msg2.Checked) path = Common.reportaddress + "Report\\廠商郵遞標籤_1_5.rpt";
            else if (Msg3.Checked) path = Common.reportaddress + "Report\\廠商郵遞標籤_自定一.rpt";
            else if (Msg4.Checked) path = Common.reportaddress + "Report\\廠商郵遞標籤_自定二.rpt";

            RPT rp = new RPT(dt, path);

            rp.lval.Add(new string[] { "font", (PrintCn.Checked) ? "直書" : "橫書" });

            var people = "";
            if (FaPer1.Checked) people = "聯絡";
            else if (FaPer.Checked) people = "負責";
            else if (FaPerCheck.Checked) people = "自定";
            rp.lval.Add(new string[] { "people", people });
            rp.lval.Add(new string[] { "people1", FaPerSet.Text });

            var address = "";
            if (FaAddr1.Checked) address = "公司";
            else if (FaAddr2.Checked) address = "發票";
            else if (FaAddr3.Checked) address = "工廠";
            else if (FaAddrCheck.Checked) address = "自定";
            rp.lval.Add(new string[] { "address", address });
            rp.lval.Add(new string[] { "address1", FaAddrSet.Text });

            short pcs = 1;
            short.TryParse(Print_F.Text, out pcs);
            rp.PCS = pcs;

            if (mode == RptMode.Print) rp.Print();
            else if (mode == RptMode.PreView) rp.PreView();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.Columns[e.ColumnIndex].Name != "Column1") return;
            if (dataGridViewT1["Column1", e.RowIndex].Value.ToString() == "Y")
            {
                dataGridViewT1["Column1", e.RowIndex].Value = "N";
            }
            else
            {
                dataGridViewT1["Column1", e.RowIndex].Value = "Y";
            }
        }

        private void btnUdf_Click(object sender, EventArgs e)
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox2);
            pnlist.Add(groupBox3);
            pnlist.Add(groupBox4);
            pVar.SaveRadioUdf(pnlist, "Print_F");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox2);
            pnlist.Add(groupBox3);
            pnlist.Add(groupBox4);
            pVar.SetRadioUdf(pnlist, "Print_F");
        }

        private void FaNo_TextChanged(object sender, EventArgs e)
        {
            if (list.Count < 1) return;//如果完全沒有廠商，就不用查了
            table = table.DefaultView.ToTable();
            list.Clear();
            list = table.AsEnumerable().ToList();
            var v = from dr in list
                    where dr["Fano"].ToString().StartsWith(FaNo.Text.Trim())
                    select dr;
            if (v.Count() > 0)
            {
                int i = list.IndexOf(v.First());
                if (i != -1)
                {
                    dataGridViewT1.FirstDisplayedScrollingRowIndex = i;
                    dataGridViewT1.Rows[i].Cells[0].Selected = true;
                }
            }
            else
            {
                dataGridViewT1.Rows[list.Count - 1].Cells[0].Selected = true;
            }
        }
    }
}
