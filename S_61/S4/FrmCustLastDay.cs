using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S4
{
    public partial class FrmCustLastDay : Formbase
    {
        JBS.JS.xEvents xe;

        public FrmCustLastDay()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            SaDate.SetDateLength();
            SaDate.Text = Date.GetDateTime(Common.User_DateTime);

            CuDays.FirstNum = 7;
            CuDays.LastNum = 0;
        }

        private void FrmCustLastDay_Load(object sender, EventArgs e)
        {

        }

        private void CuNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Cust>(sender);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void CuDays_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if (CuDays.TrimTextLenth() == 0)
            {
                CuDays.Clear();
                return;
            }

            if (CuDays.Text.ToDecimal() < 0)
            {
                e.Cancel = true;

                MessageBox.Show(
                    "為交易天數不可小於零!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CuDays.Text = "0";
            }
        }

        private void CuEDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (CuNo.TrimTextLenth() > 0 && CuNo1.TrimTextLenth() > 0 && CuNo.Text.BigThen(CuNo1.Text))
            {
                MessageBox.Show(
                    "起始客戶編號不可大於終止客戶編號!",
                    "訊息視窗",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dt = new DataTable();
                DataTable temp = new DataTable();

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("SaDate1", Date.ToUSDate(SaDate.Text.Trim()));

                    if (CuNo.TrimTextLenth() > 0)
                        cmd.Parameters.AddWithValue("CuNo", CuNo.Text.Trim());
                    if (CuNo1.TrimTextLenth() > 0)
                        cmd.Parameters.AddWithValue("CuNo1", CuNo1.Text.Trim());
                    if (EmNo.TrimTextLenth() > 0)
                        cmd.Parameters.AddWithValue("EmNo", EmNo.Text.Trim());

                    StringBuilder sb = new StringBuilder();
                    //沒交易的客戶也會撈出來
                    //var emno = "";
                    //if (EmNo.TrimTextLenth() > 0)
                    //    emno = " And EmNo = (@EmNo) ";

                    if (CuDays.TrimTextLenth() == 0)
                    {
                        sb.Append(@"
                            Select A.salelastday,日數=0,cust.*
                            from cust
                            Left join
	                            (Select cuno,MAX(sadate1)salelastday from sale where SaDate1<=(@SaDate1) group by cuno)A 
                                ON cust.cuno = A.cuno
                            Where 0=0 ");

                        if (CuNo.TrimTextLenth() > 0)
                            sb.Append(" And cust.CuNo >= @CuNo");
                        if (CuNo1.TrimTextLenth() > 0)
                            sb.Append(" And cust.CuNo <= @CuNo1");
                        if (EmNo.TrimTextLenth() > 0)
                            sb.Append(" And cust.cuemno1 = @EmNo");

                        cmd.CommandText = sb.ToString();
                        da.Fill(dt);

                        var day = 0;
                        var days = CuDays.Text.ToDecimal();
                        DateTime t1, t2;

                        DateTime.TryParseExact(
                            Date.ToUSDate(SaDate.Text.Trim()),
                            "yyyyMMdd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out t1);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["salelastday"].ToString().Trim().Length == 0)
                                continue;

                            DateTime.TryParseExact(
                                dt.Rows[i]["salelastday"].ToString().Trim(),
                                "yyyyMMdd",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out t2);

                            dt.Rows[i]["日數"] = day = new TimeSpan(t1.Ticks - t2.Ticks).Days;

                            if (Common.User_DateTime == 1)
                                dt.Rows[i]["salelastday"] = Date.AddLine(Date.ToTWDate(dt.Rows[i]["salelastday"].ToString().Trim()));
                            else
                                dt.Rows[i]["salelastday"] = Date.AddLine(Date.ToUSDate(dt.Rows[i]["salelastday"].ToString().Trim()));
                        }
                    }
                    //只撈有交易的客戶
                    else
                    {
                        sb.Append(@"
                            Select A.salelastday,日數=0,cust.*
                            from cust
                            Inner join
	                            (Select cuno,MAX(sadate1)salelastday from sale where SaDate1<=(@SaDate1) group by cuno)A
	                            ON cust.cuno=A.cuno
                            Where 0=0");

                        if (CuNo.TrimTextLenth() > 0)
                            sb.Append(" And cust.CuNo >= @CuNo");
                        if (CuNo1.TrimTextLenth() > 0)
                            sb.Append(" And cust.CuNo <= @CuNo1");
                        if (EmNo.TrimTextLenth() > 0)
                            sb.Append(" And cust.cuemno1 = @EmNo");

                        cmd.CommandText = sb.ToString();
                        da.Fill(temp);
                        dt = temp.Clone();

                        var day = 0;
                        var days = CuDays.Text.ToDecimal();
                        DateTime t1, t2;

                        DateTime.TryParseExact(
                            Date.ToUSDate(SaDate.Text.Trim()),
                            "yyyyMMdd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out t1);

                        for (int i = 0; i < temp.Rows.Count; i++)
                        {
                            if (temp.Rows[i]["salelastday"].ToString().Trim().Length == 0)
                                continue;

                            DateTime.TryParseExact(
                                temp.Rows[i]["salelastday"].ToString().Trim(),
                                "yyyyMMdd",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out t2);

                            temp.Rows[i]["日數"] = day = new TimeSpan(t1.Ticks - t2.Ticks).Days;

                            if (day < days)
                                continue;

                            if (Common.User_DateTime == 1)
                                temp.Rows[i]["salelastday"] = Date.AddLine(Date.ToTWDate(temp.Rows[i]["salelastday"].ToString().Trim()));
                            else
                                temp.Rows[i]["salelastday"] = Date.AddLine(Date.ToUSDate(temp.Rows[i]["salelastday"].ToString().Trim()));

                            dt.ImportRow(temp.Rows[i]);
                        }
                    }
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
                //using (FrmCustLastDayb frm = new FrmCustLastDayb())
                //{
                //    frm.dt = dt;
                //    frm.ShowDialog();
                //}
                this.OpemInfoFrom<FrmCustLastDayb>(() =>
                            {
                                FrmCustLastDayb frm = new FrmCustLastDayb();
                                frm.dt = dt;
                                return frm;
                            });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
