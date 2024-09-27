using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.IO;

namespace S_61.SOther
{
    public partial class FrmPrint_Cb : Formbase
    {
        public DataTable table = new DataTable();
        DataTable dt = new DataTable();
        List<DataRow> list = new List<DataRow>();
        DataView dv = new DataView();
        bool flag = true;


        public FrmPrint_Cb()
        {
            InitializeComponent();
            Msg3.SetUserDefineRpt("客戶郵遞標籤_自定一.rpt");
            Msg4.SetUserDefineRpt("客戶郵遞標籤_自定二.rpt");
            SetRdUdf();
        }

        private void FrmPrint_Cb_Load(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.SetToolTip(Msg, "客戶郵遞標籤_信封");
            tip.SetToolTip(Msg1, "客戶郵遞標籤_1");
            tip.SetToolTip(Msg2, "客戶郵遞標籤_1_5");

            Print_c.Text = "1";
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

        private void CuPerCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (CuPerCheck.Checked)
            {
                CuPerSet.ReadOnly = false;
                CuPerSet.Focus();
                CuPerSet.SelectAll();
            }
            else
                CuPerSet.ReadOnly = true;
        }

        private void CuAddrCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (CuAddrCheck.Checked)
            {
                CuAddrSet.ReadOnly = false;
                CuAddrSet.Focus();
                CuAddrSet.SelectAll();
            }
            else
                CuAddrSet.ReadOnly = true;
        }
         
        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbSort.SelectedItem.ToString().Substring(0, 2))
            {
                case "01":
                    table.DefaultView.Sort = "CuNo ASC";
                    dataGridViewT1.DataSource = table;
                    break;
                case "02":
                    table.DefaultView.Sort = "CuName1 ASC";
                    dataGridViewT1.DataSource = table;
                    break;
                case "03":
                    table.DefaultView.Sort = "CuIme ASC";
                    dataGridViewT1.DataSource = table;
                    break;
                case "04":
                    table.DefaultView.Sort = "CuAddr1 ASC";
                    dataGridViewT1.DataSource = table;
                    break;
                case "05":
                    table.DefaultView.Sort = "CuX1No ASC";
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

        void GetCustList()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "select * from cust where cuno=''";

                        for (int i = 0; i < dataGridViewT1.RowCount; i++)
                        {
                            if (dataGridViewT1.Rows[i].Cells["Column1"].Value.ToString() == "Y")
                            {
                                cmd.Parameters.AddWithValue(i.ToString(), dataGridViewT1.Rows[i].Cells["客戶編號"].Value.ToString());
                                cmd.CommandText += " or cuno=@" + i.ToString();
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
            GetCustList();
            var path = "";
            if (Msg.Checked) path = Common.reportaddress + "Report\\客戶郵遞標籤_信封.rpt";
            else if (Msg1.Checked) path = Common.reportaddress + "Report\\客戶郵遞標籤_1.rpt";
            else if (Msg2.Checked) path = Common.reportaddress + "Report\\客戶郵遞標籤_1_5.rpt";
            else if (Msg3.Checked) path = Common.reportaddress + "Report\\客戶郵遞標籤_自定一.rpt";
            else if (Msg4.Checked) path = Common.reportaddress + "Report\\客戶郵遞標籤_自定二.rpt";
            else if (Msg5.Checked) path = Common.reportaddress + "ReportF\\客戶條碼列印Code128.frx";
            else if (Msg6.Checked) path = Common.reportaddress + "ReportF\\客戶條碼列印EAN13.frx";
            if (!Msg5.Checked && !Msg6.Checked)
            {
                #region 郵遞標籤列印
                RPT rp = new RPT(dt, path);

                rp.lval.Add(new string[] { "font", (PrintCn.Checked) ? "直書" : "橫書" });

                var people = "";
                if (CuPer1.Checked) people = "聯絡";
                else if (CuPer.Checked) people = "負責";
                else if (CuPerCheck.Checked) people = "自定";
                rp.lval.Add(new string[] { "people", people });
                rp.lval.Add(new string[] { "people1", CuPerSet.Text });

                var address = "";
                if (CuAddr1.Checked) address = "公司";
                else if (CuAddr2.Checked) address = "發票";
                else if (CuAddr3.Checked) address = "送貨";
                else if (CuAddrCheck.Checked) address = "自定";
                rp.lval.Add(new string[] { "address", address });
                rp.lval.Add(new string[] { "address1", CuAddrSet.Text });

                short pcs = 1;
                short.TryParse(Print_c.Text, out pcs);
                rp.PCS = pcs;

                if (mode == RptMode.Print) rp.Print();
                else if (mode == RptMode.PreView) rp.PreView();

                #endregion
            }
            else
            {
                #region 條碼列印
                using (JBS.FastReport_Wei FastReport = new JBS.FastReport_Wei())
                {
                    if (File.Exists(path) == false)
                    {
                        MessageBox.Show("報表檔案不存在\n" + path, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else 
                    {
                        FastReport.PreView(path, dt, "Cust", null, null, mode, "");
                    }
                }
                #endregion
            }

            

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
            pVar.SaveRadioUdf(pnlist, "Print_C");
        }

        void SetRdUdf()
        {
            List<Control> pnlist = new List<Control>();
            pnlist.Add(groupBox1);
            pnlist.Add(groupBox2);
            pnlist.Add(groupBox3);
            pnlist.Add(groupBox4);
            pVar.SetRadioUdf(pnlist, "Print_C");
        }

        private void CuNo_TextChanged(object sender, EventArgs e)
        {
            if (list.Count < 1) return;//如果完全沒有客戶，就不用查了
            table = table.DefaultView.ToTable();
            list.Clear();
            list = table.AsEnumerable().ToList();
            var v = from dr in list
                    where dr["cuno"].ToString().StartsWith(CuNo.Text.Trim())
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
