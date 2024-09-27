using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmItemOutb : Formbase
    {
        public DataTable dt = new DataTable();
        DirectoryInfo dir;

        public FrmItemOutb()
        {
            InitializeComponent();
            this.總庫存量.DefaultCellStyle.Format = "f" + Common.Q;
        }

        private void FrmItemOutb_Load(object sender, EventArgs e)
        { 
            dataGridViewT1.DataSource = dt;
            foreach (DataRow rw in dt.Rows)
            {
                rw["點選"] = "V";
                if (rw["ittrait"].ToDecimal() == 1) rw["產品組成"] = "組合品";
                else if (rw["ittrait"].ToDecimal() == 2) rw["產品組成"] = "組裝品";
                else if (rw["ittrait"].ToDecimal() == 3) rw["產品組成"] = "單一商品";
            }
            dir = new DirectoryInfo(Application.StartupPath + @"\STK");
            var path = Application.StartupPath + @"\STK\ItemOut.Txt";
            if (File.Exists(path)) textBoxT1.Text = path;
        }

        private void dataGridViewT1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            if (dt.Rows.Count == 0) return;

            if (dataGridViewT1.Columns[e.ColumnIndex].Name == "點選")
            {
                dt.Rows[e.RowIndex]["點選"] = dt.Rows[e.RowIndex]["點選"].ToString().Trim() == "V" ? "" : "V";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog frm = new FolderBrowserDialog())
            {
                frm.SelectedPath = Application.StartupPath;
                frm.ShowDialog();
                dir = new DirectoryInfo(frm.SelectedPath + @"\STK");
                textBoxT1.Text = frm.SelectedPath + @"\STK\ItemOut.Txt";
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            if (textBoxT1.Text.Trim() == "")
            {
                textBoxT1.Clear();
                MessageBox.Show("轉出路徑不可以為空白，請選擇路徑！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                btnOutput.Enabled = false;
                string temp = "";
                string str = "";
                var path = textBoxT1.Text.Trim();

                if (!Directory.Exists(dir.FullName)) dir.Create();
                if (File.Exists(path)) File.Delete(path);

                foreach (DataRow rw in dt.Rows)
                {
                    if (rw["點選"].ToString().Trim() == "V")
                    {
                        temp = rw["itno"].ToString().Trim();
                        temp = AddSpace(temp, 20 - Encoding.GetEncoding(950).GetByteCount(temp));
                        str += temp;

                        temp = rw["itnoudf"].ToString().Trim();
                        temp = AddSpace(temp, 20 - Encoding.GetEncoding(950).GetByteCount(temp));
                        str += temp;

                        temp = rw["itname"].ToString().Trim();
                        temp = AddSpace(temp, 30 - Encoding.GetEncoding(950).GetByteCount(temp));
                        str += temp;

                        temp = rw["itunit"].ToString().Trim();
                        temp = AddSpace(temp, 4 - Encoding.GetEncoding(950).GetByteCount(temp));
                        str += temp;

                        str += Environment.NewLine;
                    }
                }
                File.WriteAllText(path, str, Encoding.Default);
                MessageBox.Show("轉出完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Dispose();
            }
            catch (Exception ex)
            {
                btnOutput.Enabled = true;
                MessageBox.Show(ex.ToString());
            }
        }

        string AddSpace(string str, int len)
        {
            for (int i = 0; i < len; i++)
            {
                str += " ";
            }
            return str;
        }
    }
}
