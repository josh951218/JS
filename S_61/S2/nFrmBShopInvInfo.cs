using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class nFrmBShopInvInfo : Formbase
    {
        JBS.JS.BShopInv jBShopInv;

        public nFrmBShopInvInfo()
        {
            InitializeComponent();
            this.jBShopInv = new JBS.JS.BShopInv();

            Day.SetDateLength();
            Day1.SetDateLength();
            PrDate.SetDateLength();
            PrDate1.SetDateLength();

            var i = Common.User_DateTime == 1 ? 1 : 2;
            //Day.Text = Date.GetDateTime(i).takeString(i + 4) + "01";
            //Day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmBShopInvInfo_Load(object sender, EventArgs e)
        {
            //X5No.Text = "1";
            //pVar.XX05Validate("1", X5No, X5Name);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void X5No_DoubleClick(object sender, EventArgs e)
        {
            jBShopInv.Open<JBS.JS.XX05>(sender);
        }

        private void X5No_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jBShopInv.ValidateOpen<JBS.JS.XX05>(sender, e, row =>
            {
                X5No.Text = row["X5No"].ToString().Trim();
                X5Name.Text = row["X5Name"].ToString().Trim();
            }, true);

            if (X5No.TrimTextLenth() == 0)
                X5Name.Clear();
        }

        private void CoNo_DoubleClick(object sender, EventArgs e)
        {
            jBShopInv.Open<JBS.JS.Comp>(sender);
        }

        private void CoNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if (CoNo.TrimTextLenth() == 0)
            {
                CoNo.Clear();
                CoName1.Clear();
                return;
            }

            jBShopInv.ValidateOpen<JBS.JS.Comp>(sender, e, reader =>
            {
                CoNo.Text = reader["CoNo"].ToString().Trim();
                CoName1.Text = reader["CoName1"].ToString().Trim();
            }, true);
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            jBShopInv.Open<JBS.JS.Fact>(sender);
        }

        private void Day_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jBShopInv.DateValidate(sender, e, true);
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            try
            {
                btnBrow.Enabled = false;

                DataTable dt = new DataTable();
                DataTable dtD = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("X5No", X5No.Text.Trim());
                    cmd.Parameters.AddWithValue("CoNo", CoNo.Text.Trim());
                    cmd.Parameters.AddWithValue("Day", Date.ToTWDate(Day.Text.Trim()));
                    cmd.Parameters.AddWithValue("Day1", Date.ToTWDate(Day1.Text.Trim()));
                    cmd.Parameters.AddWithValue("PrDate", Date.ToTWDate(PrDate.Text.Trim()));
                    cmd.Parameters.AddWithValue("PrDate1", Date.ToTWDate(PrDate1.Text.Trim()));
                    cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
                    cmd.Parameters.AddWithValue("FaNo1", FaNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("ItNo", ItNo.Text.Trim());
                    cmd.Parameters.AddWithValue("ItNo1", ItNo1.Text.Trim());

                    var qStr = "";
                    StringBuilder sb = new StringBuilder();
                    if (X5No.TrimTextLenth() > 0)
                        sb.Append(" And bshopinv.X5No = (@X5No) ");
                    if (CoNo.TrimTextLenth() > 0)
                        sb.Append(" And bshopinv.CoNo = (@CoNo) ");
                    if (Day.TrimTextLenth() > 0)
                        sb.Append(" And bshopinv.InDate >= (@Day) ");
                    if (Day1.TrimTextLenth() > 0)
                        sb.Append(" And bshopinv.InDate <= (@Day1) ");
                    if (PrDate.TrimTextLenth() > 0)
                        sb.Append(" And bshopinv.PrDate >= (@PrDate) ");
                    if (PrDate1.TrimTextLenth() > 0)
                        sb.Append(" And bshopinv.PrDate <= (@PrDate1) ");
                    if (FaNo.TrimTextLenth() > 0)
                        sb.Append(" And bshopinv.FaNo >= (@FaNo) ");
                    if (FaNo1.TrimTextLenth() > 0)
                        sb.Append(" And bshopinv.FaNo <= (@FaNo1) ");
                    if (ItNo.TrimTextLenth() > 0)
                        sb.Append(" And ItNo >= (@ItNo) ");
                    if (ItNo1.TrimTextLenth() > 0)
                        sb.Append(" And ItNo <= (@ItNo1) ");
                    qStr = sb.ToString();

                    cmd.CommandText = @"
                    Select 發票日期='',作廢='',bshopinv.* 
                    From bshopinvd
                    Left join bshopinv on bshopinvd.inno = bshopinv.inno
                    Where 0=0 " + qStr + " order by bshopinvd.inno";
                    da.Fill(dt);

                    sb.Clear();
                    if (X5No.TrimTextLenth() > 0)
                        sb.Append(" And bshopinvd.X5No = (@X5No) ");
                    if (CoNo.TrimTextLenth() > 0)
                        sb.Append(" And bshopinvd.CoNo = (@CoNo) ");
                    if (Day.TrimTextLenth() > 0)
                        sb.Append(" And bshopinvd.InDate >= (@Day) ");
                    if (Day1.TrimTextLenth() > 0)
                        sb.Append(" And bshopinvd.InDate <= (@Day1) ");
                    if (PrDate.TrimTextLenth() > 0)
                        sb.Append(" And bshopinvd.PrDate >= (@PrDate) ");
                    if (PrDate1.TrimTextLenth() > 0)
                        sb.Append(" And bshopinvd.PrDate <= (@PrDate1) ");
                    if (FaNo.TrimTextLenth() > 0)
                        sb.Append(" And bshopinvd.FaNo >= (@FaNo) ");
                    if (FaNo1.TrimTextLenth() > 0)
                        sb.Append(" And bshopinvd.FaNo <= (@FaNo1) ");
                    if (ItNo.TrimTextLenth() > 0)
                        sb.Append(" And ItNo >= (@ItNo) ");
                    if (ItNo1.TrimTextLenth() > 0)
                        sb.Append(" And ItNo <= (@ItNo1) ");
                    qStr = sb.ToString();

                    cmd.CommandText = " Select * from bshopinvd where 0=0 " + qStr + " order by bshopinvd.inno";
                    da.Fill(dtD);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                    "查無資料!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    return;
                }

                //發票金額(不含作廢金額)
                if (!dt.Columns.Contains("發票金額"))
                    dt.Columns.Add("發票金額", typeof(System.Decimal));
                if (!dt.Columns.Contains("發票稅前"))
                    dt.Columns.Add("發票稅前", typeof(System.Decimal));
                if (!dt.Columns.Contains("發票稅額"))
                    dt.Columns.Add("發票稅額", typeof(System.Decimal));
                 
                var day = string.Concat("indate", Common.User_DateTime == 1 ? "" : "1");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][day] = Date.AddLine(dt.Rows[i][day].ToString().Trim()); 

                    if (dt.Rows[i]["invalid"].ToDecimal() == 1)
                    {
                        dt.Rows[i]["作廢"] = "作廢";
                        dt.Rows[i]["發票稅前"] = 0;
                        dt.Rows[i]["發票稅額"] = 0;
                        dt.Rows[i]["發票金額"] = 0;
                    }
                    else
                    {
                        dt.Rows[i]["發票稅前"] = dt.Rows[i]["taxmny"];
                        dt.Rows[i]["發票稅額"] = dt.Rows[i]["tax"];
                        dt.Rows[i]["發票金額"] = dt.Rows[i]["totmny"];
                    }
                }

                using (nFrmBShopInvInfob frm = new nFrmBShopInvInfob())
                {
                    dt.TableName = "bshopinv";
                    dtD.TableName = "bshopinvd";

                    frm.X5Name = X5Name.Text;
                    frm.ds.Tables.Add(dt);
                    frm.ds.Tables.Add(dtD);
                    frm.ShowDialog();
                }
            }
            finally
            {
                btnBrow.Enabled = true;
            }
        }

        private void ItNo1_DoubleClick(object sender, EventArgs e)
        {
            jBShopInv.Open<JBS.JS.Item>(sender);
        }




    }
}
