using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmBarCodeBrow : Formbase
    {
        public DataTable dt = new DataTable();
        DataTable temp = new DataTable();
        string ItWhat = "";

        public FrmBarCodeBrow()
        {
            InitializeComponent(); 
             
            this.包裝售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.包裝進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝標準成本.DefaultCellStyle.Format = "f" + Common.MS;
            this.安全存量.DefaultCellStyle.Format = "f" + Common.Q;
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.經濟批量.DefaultCellStyle.Format = "f" + Common.Q;
            this.標準成本.DefaultCellStyle.Format = "f" + Common.MS;

            this.張數下.DefaultCellStyle.Format = "f0";
            this.張數下.FirstNum = 10;
            this.張數下.LastNum = 0;

            this.售價.Visible = this.包裝售價.Visible = Common.User_SalePrice;
            this.進價.Visible = this.包裝進價.Visible = Common.User_ShopPrice;
            this.標準成本.Visible = this.包裝標準成本.Visible = Common.User_ShopPrice;
        }

        private void FrmBarCodeBrow_Load(object sender, EventArgs e)
        { 
            txtNumber1.FirstNum = 2;
            txtNumber1.LastNum = 0;
            txtNumber1.ReadOnly = false;

            dataGridViewT2.ReadOnly = false;
            for (int i = 0; i < dataGridViewT2.Columns.Count; i++)
            {
                if (dataGridViewT2.Columns[i].Name == "張數下") continue;
                else
                {
                    dataGridViewT2.Columns[i].ReadOnly = true;
                }
            }

            txtNumber1.Text = "1";
            SetKiName();
            temp = dt.Clone();

            dataGridViewT1.DataSource = dt;
            dataGridViewT2.DataSource = temp;
        }

        void SetKiName()
        {
            try
            {
                DataTable t = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select * from kind";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(t);
                    }
                }
                if (t.Rows.Count > 0)
                {
                    var p = t.AsEnumerable().ToList();
                    DataRow n = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        n = p.Find(r => r["KiNo"].ToString().Trim() == dt.Rows[i]["KiNo"].ToString().Trim());
                        if (n == null) continue;
                        else
                        {
                            dt.Rows[i]["類別名稱"] = n["KiName"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ItNo.Text.Trim().Length > 0 && ItName.Text.Trim().Length > 0) dataGridViewT1.Search("產品編號", ItNo.Text.Trim(), "品名規格", ItName.Text.Trim());
            else if (ItNo.Text.Trim().Length > 0) dataGridViewT1.Search("產品編號", ItNo.Text.Trim());
            else if (ItName.Text.Trim().Length > 0) dataGridViewT1.Search("品名規格", ItName.Text.Trim());
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewT1.Rows.Count == 0) return;
            if (e.RowIndex == -1) return;

            temp.ImportRow(dt.Rows[e.RowIndex]);
            temp.AcceptChanges();

            dataGridViewT2.FirstDisplayedScrollingRowIndex = temp.Rows.Count - 1;
            dataGridViewT2.Rows[temp.Rows.Count - 1].Selected = true;

            var index = dataGridViewT2.Rows.Count - 1;
            dataGridViewT2.CurrentCell = dataGridViewT2["張數下", index];
            dataGridViewT2.Rows[index].Selected = true;
            dataGridViewT2.Focus();
        }

        private void gridItDesp_Click(object sender, EventArgs e)
        {
            if (dataGridViewT2.Rows.Count > 0)
            {
                gridItDesp.Focus();
                using (JE.SOther.FrmDesp frm = new JE.SOther.FrmDesp(true, JE.MyControl.FormStyle.Mini))
                {
                    var index = dataGridViewT2.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (index == -1)
                    {
                        dataGridViewT1.Focus();
                        return;
                    }
                    frm.dr = temp.Rows[index];
                    frm.ShowDialog();
                }
                dataGridViewT2.Focus();
            }
        }

        private void btnBrowT1_Click(object sender, EventArgs e)
        {
            btnBrowT1.Focus();
            temp.Merge(dt.Copy());
            temp.AcceptChanges();
            dataGridViewT2.DataSource = temp;

            var index = dataGridViewT2.Rows.Count - 1;
            dataGridViewT2.CurrentCell = dataGridViewT2["張數下", index];
            dataGridViewT2.Rows[index].Selected = true;
            dataGridViewT2.Focus();
        }

        private void btnBrowT2_Click(object sender, EventArgs e)
        {
            btnBrowT2.Focus();
            temp.Clear();
            dataGridViewT2.DataSource = temp;
        }

        private void btnBrowT3_Click(object sender, EventArgs e)
        {
            btnBrowT3.Focus();
            if (dataGridViewT2.Rows.Count > 0)
            {
                var p = dataGridViewT2.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                var index = p == null ? -1 : p.Index;
                if (index == -1) return;
                temp.Rows[index].Delete();
                temp.AcceptChanges();

                if (dataGridViewT2.Rows.Count > 0)
                {
                    index = dataGridViewT2.Rows.Count - 1;
                    dataGridViewT2.CurrentCell = dataGridViewT2["張數下", index];
                    dataGridViewT2.Rows[index].Selected = true;
                }
                dataGridViewT2.DataSource = temp;
            }
            dataGridViewT2.Focus();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string AddSpace(string str, int len)
        {
            for (int i = 0; i < len; i++)
            {
                str += ' ';
            }
            return str;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (radioT3.Checked)
            {
                if (temp.Rows.Count == 0) return;

                DataTable tcode = new DataTable();
                DataTable t = new DataTable();
                DataTable template = new DataTable();
                string codestr = "";
                var count = (int)txtNumber1.Text.Trim().ToDecimal();

                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        string sql = "select * from systemcode where userno=N'T01'";
                        using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                        {
                            tcode.Clear();
                            da.Fill(tcode);
                        }

                        for (int i = 0; i < tcode.Rows.Count; i++)
                        {
                            codestr += "," + tcode.Rows[i]["columnName"].ToString().Trim() + " ";
                        }

                        if (radioT6.Checked)
                        {
                            sql = "select 列印份數=0,ItNo " + codestr + " from item where 0=0 ";
                            ItWhat = "ItNo";
                        }
                        else if (radioT7.Checked)
                        {
                            sql = "select 列印份數=0,ItNoUdf " + codestr + " from item where 0=0 ";
                            ItWhat = "ItNoUdf";
                        }
                        else if (radioT8.Checked)
                        {
                            sql = "select ItNo,ItCodeSlt from item";
                            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                            {
                                t.Clear();
                                da.Fill(t);
                            }
                            var list = t.AsEnumerable().ToList();
                            foreach (DataRow item in temp.Rows)
                            {
                                DataRow row = list.Find(r => r["itno"].ToString().Trim() == item["itno"].ToString().Trim());
                                if (row == null) continue;
                                else
                                {
                                    item["ItNo"] = row["ItCodeSlt"].ToDecimal() == 1 ? item["ItNo"] : item["ItNoUdf"];
                                }
                            }
                            sql = "select 列印份數=0,ItNo " + codestr + " from item where 0=0 ";
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                        {
                            da.Fill(template);
                        }
                        template.Clear();

                        var codelist = tcode.AsEnumerable().ToList();
                        string cName = "";
                        for (int i = 0; i < temp.Rows.Count; i++)
                        {
                            temp.Rows[i]["列印份數"] = temp.Rows[i]["張數"].ToDecimal() * count;
                            DataRow dr = template.NewRow();
                            for (int j = 0; j < template.Columns.Count; j++)
                            {
                                cName = template.Columns[j].ColumnName;
                                var code = codelist.Find(r => r["columnName"].ToString() == cName);
                                if (code == null)
                                {
                                    if (cName == "列印份數") dr[cName] = temp.Rows[i][cName];
                                    if (cName == "ItNo") dr[cName] = temp.Rows[i][cName];
                                    continue;
                                }

                                if (template.Columns[j].DataType == typeof(Decimal))
                                {
                                    dr[cName] = temp.Rows[i][cName].ToDecimal().ToString("f" + code["cdec"]);
                                }
                                else
                                {
                                    dr[cName] = temp.Rows[i][cName];
                                }
                            }
                            template.Rows.Add(dr);
                        }
                    }


                    if (template.Rows.Count > 0)
                    {
                        var codelist = tcode.AsEnumerable().ToList();
                        string path = "BarCode.Txt";

                        if (File.Exists(path)) File.Delete(path);

                        using (FileStream fs = File.Create(path))
                        {
                            for (int i = 0; i < template.Rows.Count; i++)
                            {
                                string str = template.Rows[i]["列印份數"].ToString().PadRight(10, ' ');
                                str += template.Rows[i][ItWhat].ToString().PadRight(20, ' ');

                                for (int j = 0; j < template.Columns.Count; j++)
                                {
                                    var name = template.Columns[j].ColumnName;
                                    var code = codelist.Find(r => r["columnName"].ToString().Trim() == name);
                                    if (code == null) continue;
                                    else
                                    {
                                        var val = code["frontStr"] + template.Rows[i][name].ToString() + code["backStr"];
                                        int len = Encoding.GetEncoding(950).GetByteCount(val);
                                        int max = (int)code["cLen"].ToDecimal();
                                        val = val.GetUTF8(max);
                                        str += AddSpace(val, max - len);
                                    }
                                }
                                if (i < template.Rows.Count - 1) str += Environment.NewLine;
                                byte[] info = Encoding.GetEncoding(950).GetBytes(str);
                                fs.Write(info, 0, info.Length);
                            }
                        }
                    }

                    if (!File.Exists("Project1.exe")) MessageBox.Show("找不到恆錩列印程式，無法列印！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        Process.Start(Application.StartupPath + @"\Project1.exe");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
            }
        }






























































    }
}
