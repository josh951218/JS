using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmItemb_Bom : Formbase, JBS.JS.IxOpen
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }

        DataTable dt = new DataTable(); 
        List<TextBoxbase> list;

        public bool CanAppend { get; set; }
        //public DataRow Result { get; set; }

        public FrmItemb_Bom()
        {
            InitializeComponent();
            list = new List<TextBoxbase>() { ItNo, ItName };
            this.現有存量.DefaultCellStyle.Format = "f" + Common.Q;
            this.包裝數量.DefaultCellStyle.Format = "f" + Common.Q;
            this.進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.包裝進價.DefaultCellStyle.Format = "f" + Common.MF;
            this.售價.DefaultCellStyle.Format = "f" + Common.MS;
            this.包裝售價.DefaultCellStyle.Format = "f" + Common.MS;
        }

        private void FrmItemb_Bom_Load(object sender, EventArgs e)
        {
            this.TResult = "";

            dataGridViewT1.DataSource = dt.DefaultView;
            loadDB();

            dt.DefaultView.Search(ref dataGridViewT1, "ItNo", this.TSeekNo ?? "");
            ItNo.Focus();
        }

        private void loadDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select *,產品組成='' from item where (ItTrait=1 or ItTrait=2) order by itno COLLATE Chinese_Taiwan_Stroke_BIN";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        dt.Clear();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["ittrait"].ToDecimal() == 1)
                                    dt.Rows[i]["產品組成"] = "組合品";
                                else if (dt.Rows[i]["ittrait"].ToDecimal() == 2)
                                    dt.Rows[i]["產品組成"] = "組裝品";

                                dataGridViewT1.InvalidateRow(i);
                            }
                        }
                        lblT1.Text = "";
                    }
                }
                writeToTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void writeToTxt()
        {
            dt.DefaultView.Sort = "ItNo ASC";

        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (dataGridViewT1.Rows.Count > 0)
            {
                string TempID = dataGridViewT1.SelectedRows[0].Cells["產品編號"].Value.ToString().Trim();
                //this.Result = Common.load("Check", "item", "itno", TempID);

                this.TResult = TempID;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6)
            {
                btnQuery.PerformClick();
                return true;
            }
            else if (keyData == Keys.F9)
            {
                btnGet.PerformClick();
                return true;
            }
            else if (keyData == Keys.F11)
            {
                btnExit.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ItNo_TextChanged(object sender, EventArgs e)
        {
            if (ItNo.Text.Trim() == ""
                && ItName.Text.Trim() == "")
                return;

            if (ItName.Text.Trim() != "")
            {
                NewQuery_1();
            }
            else
            {
                NewQuery();
            }
        }

        void NewQuery()
        {
            if (ItNo.Text.Trim().Length > 0)
            {
                var itno = "";
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@itno", ItNo.Text.Trim());
                    cmd.CommandText = " SELECT top 1 itno FROM ITEM WHERE (ItTrait=1 or ItTrait=2) and ITNO >= @itno ORDER BY ITNO";
                    var obj = cmd.ExecuteScalar();
                    if (obj != null) itno = obj.ToString().Trim();
                    else
                    {
                        cmd.CommandText = " Select top 1 itno from item WHERE (ItTrait=1 or ItTrait=2) and order by itno desc";
                        obj = cmd.ExecuteScalar();
                        if (obj != null) itno = obj.ToString().Trim();
                        else itno = ItNo.Text.Trim();
                    }
                }

                dt.DefaultView.Sort = "ItNo ASC";
                dt.DefaultView.Search(ref dataGridViewT1, "ItNo", itno);
            }

        }

        void NewQuery_1()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("itname", ItName.Text.Trim());
                cmd.CommandText = "select *,產品組成='' from item where (ItTrait=1 or ItTrait=2) and itname like @itname+'%' order by itname";

                dt.Clear();
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ittrait"].ToDecimal() == 1)
                        dt.Rows[i]["產品組成"] = "組合品";
                    else if (dt.Rows[i]["ittrait"].ToDecimal() == 2)
                        dt.Rows[i]["產品組成"] = "組裝品";

                    dataGridViewT1.InvalidateRow(i);
                }
            }

            dt.DefaultView.Sort = "ItName ASC";
            dt.DefaultView.Search(ref dataGridViewT1, "ItName", ItName.Text.Trim());
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (ItNo.Text.Trim() == "" && ItName.Text.Trim() == "") return;

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = " select * from item where (ItTrait=1 or ItTrait=2) ";
                    foreach (Control tb in list)
                    {
                        if (tb is TextBox)
                        {
                            if (tb.Text.Trim() != "")
                            {
                                cmd.Parameters.AddWithValue(tb.Name, tb.Text);
                                cmd.CommandText += " and " + tb.Name + " like '%'+@" + tb.Name + "+'%'";
                            }
                            (tb as TextBox).Enter += new EventHandler(Text_OnEnter);
                        }
                    }
                    cmd.CommandText += " order by ItNo COLLATE Chinese_Taiwan_Stroke_BIN";

                    dt.Clear();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["ittrait"].ToDecimal() == 1)
                                dt.Rows[i]["產品組成"] = "組合品";
                            else if (dt.Rows[i]["ittrait"].ToDecimal() == 2)
                                dt.Rows[i]["產品組成"] = "組裝品";

                            dataGridViewT1.InvalidateRow(i);
                        }
                    }


                    writeToTxt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Text_OnEnter(object sender, EventArgs e)
        {
            loadDB();
            writeToTxt();
            foreach (Control tb in list)
            {
                if (tb is TextBox)
                {
                    (tb as TextBox).Text = "";
                    (tb as TextBox).Enter -= Text_OnEnter;
                }
            }
        }

    }
}
