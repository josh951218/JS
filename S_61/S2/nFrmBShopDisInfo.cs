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
    public partial class nFrmBShopDisInfo : Formbase
    {
        JBS.JS.BshopDis jBshopDis;

        public nFrmBShopDisInfo()
        {
            InitializeComponent();
            this.jBshopDis = new JBS.JS.BshopDis();

            Day.SetDateLength();
            Day1.SetDateLength();
            PrDate.SetDateLength();
            PrDate1.SetDateLength();

            var i = Common.User_DateTime == 1 ? 1 : 2;
            //Day.Text = Date.GetDateTime(i).takeString(i + 4) + "01";
            //Day1.Text = Date.GetDateTime(Common.User_DateTime);
        }

        private void FrmBshopDisInfo_Load(object sender, EventArgs e)
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
            jBshopDis.Open<JBS.JS.XX05>(sender);
        }

        private void X5No_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jBshopDis.ValidateOpen<JBS.JS.XX05>(sender, e, row =>
            {
                X5No.Text = row["X5No"].ToString().Trim();
                X5Name.Text = row["X5Name"].ToString().Trim();
            }, true);

            if (X5No.TrimTextLenth() == 0)
                X5Name.Clear();
        }

        private void CoNo_DoubleClick(object sender, EventArgs e)
        {
            jBshopDis.Open<JBS.JS.Comp>(sender);
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

            jBshopDis.ValidateOpen<JBS.JS.Comp>(sender, e, reader =>
            {
                CoNo.Text = reader["CoNo"].ToString().Trim();
                CoName1.Text = reader["CoName1"].ToString().Trim();
            }, true);
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            jBshopDis.Open<JBS.JS.Fact>(sender);
        }

        private void Day_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jBshopDis.DateValidate(sender, e, true);
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
                        sb.Append(" And bshopdis.X5No = (@X5No) ");
                    if (CoNo.TrimTextLenth() > 0)
                        sb.Append(" And bshopdis.CoNo = (@CoNo) ");
                    if (Day.TrimTextLenth() > 0)
                        sb.Append(" And bshopdis.DiDate >= (@Day) ");
                    if (Day1.TrimTextLenth() > 0)
                        sb.Append(" And bshopdis.DiDate <= (@Day1) ");
                    if (PrDate.TrimTextLenth() > 0)
                        sb.Append(" And bshopdis.PrDate >= (@PrDate) ");
                    if (PrDate1.TrimTextLenth() > 0)
                        sb.Append(" And bshopdis.PrDate <= (@PrDate1) ");
                    if (FaNo.TrimTextLenth() > 0)
                        sb.Append(" And bshopdis.FaNo >= (@FaNo) ");
                    if (FaNo1.TrimTextLenth() > 0)
                        sb.Append(" And bshopdis.FaNo <= (@FaNo1) ");
                    if (ItNo.TrimTextLenth() > 0)
                        sb.Append(" And ItNo >= (@ItNo) ");
                    if (ItNo1.TrimTextLenth() > 0)
                        sb.Append(" And ItNo <= (@ItNo1) ");
                    qStr = sb.ToString();

                    cmd.CommandText = @"
                    Select 發票日期='',作廢='',bshopdis.* 
                    From bshopdisd 
                    Left join bshopdis on bshopdisd.dino = bshopdis.dino
                    Where 0=0 " + qStr + " order by bshopdisd.inno";
                    da.Fill(dt);

                    sb.Clear();
                    if (X5No.TrimTextLenth() > 0)
                        sb.Append(" And bshopdisd.X5No = (@X5No) ");
                    if (CoNo.TrimTextLenth() > 0)
                        sb.Append(" And bshopdisd.CoNo = (@CoNo) ");
                    if (Day.TrimTextLenth() > 0)
                        sb.Append(" And bshopdisd.DiDate >= (@Day) ");
                    if (Day1.TrimTextLenth() > 0)
                        sb.Append(" And bshopdisd.DiDate <= (@Day1) ");
                    if (PrDate.TrimTextLenth() > 0)
                        sb.Append(" And bshopdisd.PrDate >= (@PrDate) ");
                    if (PrDate1.TrimTextLenth() > 0)
                        sb.Append(" And bshopdisd.PrDate <= (@PrDate1) ");
                    if (FaNo.TrimTextLenth() > 0)
                        sb.Append(" And bshopdisd.FaNo >= (@FaNo) ");
                    if (FaNo1.TrimTextLenth() > 0)
                        sb.Append(" And bshopdisd.FaNo <= (@FaNo1) ");
                    if (ItNo.TrimTextLenth() > 0)
                        sb.Append(" And ItNo >= (@ItNo) ");
                    if (ItNo1.TrimTextLenth() > 0)
                        sb.Append(" And ItNo <= (@ItNo1) ");
                    qStr = sb.ToString();

                    cmd.CommandText = " Select * from bshopdisd where 0=0 " + qStr + " order by bshopdisd.inno";
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

                var daz = string.Concat("indate", Common.User_DateTime == 1 ? "" : "1");
                var day = string.Concat("didate", Common.User_DateTime == 1 ? "" : "1");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][day] = Date.AddLine(dt.Rows[i][day].ToString().Trim());
                    dt.Rows[i][daz] = Date.AddLine(dt.Rows[i][daz].ToString().Trim());

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

                using (nFrmBShopDisInfob frm = new nFrmBShopDisInfob())
                {
                    dt.TableName = "bshopdis123";
                    dtD.TableName = "bshopdisd123";

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
            jBshopDis.Open<JBS.JS.Item>(sender);
        }





    }
}
