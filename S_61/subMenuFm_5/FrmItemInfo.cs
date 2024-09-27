using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmItemInfo : Formbase
    {
        JBS.JS.xEvents xe;
        public DataTable dt = new DataTable();
        List<DataRow> list = new List<DataRow>();

        public FrmItemInfo()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void FrmItemInfo_Load(object sender, EventArgs e)
        {
            ItNo1.Focus();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (ItNo1.Text.Trim() != "" && ItNo2.Text.Trim() != "")
            {
                if (ItNo1.Text.CompareTo(ItNo2.Text) > 0)
                {
                    MessageBox.Show("起始產品編號不可大於終止編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ItNo1.Focus();
                    return;
                }
            }
            if (ItNoUdf1.Text.Trim() != "" && ItNoUdf2.Text.Trim() != "")
            {
                if (ItNoUdf1.Text.CompareTo(ItNoUdf2.Text) > 0)
                {
                    MessageBox.Show("起始產品自定編號不可大於終止編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ItNoUdf1.Focus();
                    return;
                }
            }
            if (KiNo1.Text.Trim() != "" && KiNo1.Text.Trim() != "")
            {
                if (KiNo1.Text.CompareTo(KiNo2.Text) > 0)
                {
                    MessageBox.Show("起始產品類別編號不可大於終止產品類別編號，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    KiNo1.Focus();
                    return;
                }
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(Common.sqlConnString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ItNo1", ItNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItNo2", ItNo2.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItNoUdf1", ItNoUdf1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItNoUdf2", ItNoUdf2.Text.Trim());
                    cmd.Parameters.AddWithValue("@KiNo1", KiNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("@KiNo2", KiNo2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Itname", Itname.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItUdf1", ItUdf1.Text.Trim());


                    string str = @"select *,FaName1 = (SELECT faname1 FROM fact where a.fano = fact.fano) from 
(SELECT * FROM item) as a 
left join (SELECT SUM(itqty) as itqty,itno FROM stock group by itno) as b on a.itno=b.itno where '0'='0'";
                   
                    
                    if (ItNo1.Text.Trim() != "")
                        str += " and a.ItNo >=@ItNo1";
                    if (ItNo2.Text.Trim() != "")
                        str += " and a.ItNo <=@ItNo2";
                    if (ItNoUdf1.Text.Trim() != "")
                        str += " and ItNoUdf >=@ItNoUdf1";
                    if (ItNoUdf2.Text.Trim() != "")
                        str += " and ItNoUdf <=@ItNoUdf2";
                    if (KiNo1.Text.Trim() != "")
                        str += " and KiNo >=@KiNo1";
                    if (KiNo2.Text.Trim() != "")
                        str += " and KiNo <=@KiNo2";
                    if (Itname.Text.Trim() != "")
                        str += " and Itname like @Itname+'%'";
                    if (ItUdf1.Text.Trim() != "")
                        str += " and ItUdf1 like @ItUdf1+'%'";
                    cmd.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        //using (var frm = new FrmItemInfob())
                        //{ 
                        //    frm.dt = dt;
                        //    frm.ShowDialog();
                        //}
                        this.OpemInfoFrom<FrmItemInfob>(() =>
                            {
                                FrmItemInfob frm = new FrmItemInfob();
                                frm.dt = dt;
                                return frm;
                            });
                    }
                    else
                    {
                        MessageBox.Show("找不到任何資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void ItNoUdf_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender, row =>
            {
                (sender as TextBox).Text = row["ItNoUdf"].ToString();
            });
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }
         
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F11) 
                this.Dispose();

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}