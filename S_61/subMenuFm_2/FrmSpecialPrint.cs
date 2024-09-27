using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmSpecialPrint : Formbase
    {
        DataTable dt = new DataTable();
        RPT rp;

        public FrmSpecialPrint()
        {
            InitializeComponent();
        }

        private void FrmSpecialPrint_Load(object sender, EventArgs e)
        {
            radioT1.Checked = radioT4.Checked = radioT10.Checked = true;
            radioT2.Enabled = File.Exists(Common.reportaddress + "Report\\產品特價區間_簡要自定一.rpt");

            LoadLastSpecialNo();
        }

        void LoadLastSpecialNo()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = "select Top 1 * from special order by spno desc";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        DataTable t = new DataTable();
                        da.Fill(t);
                        if (t.Rows.Count > 0)
                        {
                            SpecialNo.Text = SpecialNo1.Text = t.Rows[0]["spno"].ToString().Trim();
                        }
                        else
                        {
                            SpecialNo.Text = SpecialNo1.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SpecialNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmSpecial_Print_SpNo())
            { 
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    (sender as TextBox).Text = frm.result;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Word();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Excel();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Print();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.PreView();
        }

        void paramsInit()
        {
            dt.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = getSQL();
                    if (SpecialNo.TrimTextLenth() > 0) sql += " and Special.SpNo  >=@spno";
                    if (SpecialNo1.TrimTextLenth() > 0) sql += " and Special.SpNo <=@spno1";

                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (SpecialNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("spno", SpecialNo.Text.Trim());
                        if (SpecialNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("spno1", SpecialNo1.Text.Trim());
                        cmd.CommandText = sql;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string path = "";
            if (radioT1.Checked)
            {
                path = Common.reportaddress + "Report\\產品特價區間_簡要表.rpt";
            }
            else if (radioT2.Checked)
            {
                path = Common.reportaddress + "Report\\產品特價區間_簡要自定一.rpt";
            }

            string txtend = "";
            if (radioT4.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString() + Common.dtEnd.Rows[6]["tamemo"].ToString() + Common.dtEnd.Rows[7]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString() + Common.dtEnd.Rows[9]["tamemo"].ToString() + Common.dtEnd.Rows[10]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString() + Common.dtEnd.Rows[12]["tamemo"].ToString() + Common.dtEnd.Rows[13]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString() + Common.dtEnd.Rows[15]["tamemo"].ToString() + Common.dtEnd.Rows[16]["tamemo"].ToString();
            else if (radioT8.Checked) txtend = Common.dtEnd.Rows[17]["tamemo"].ToString() + Common.dtEnd.Rows[18]["tamemo"].ToString() + Common.dtEnd.Rows[19]["tamemo"].ToString();
            else txtend = "";

            string txtstart = "";
            if (radioT10.Checked) txtstart = Common.dtstart.Rows[0]["pnname"].ToString();
            else if (radioT11.Checked) txtstart = Common.dtstart.Rows[1]["pnname"].ToString();
            else if (radioT12.Checked) txtstart = Common.dtstart.Rows[2]["pnname"].ToString();
            else if (radioT13.Checked) txtstart = Common.dtstart.Rows[3]["pnname"].ToString();
            else if (radioT14.Checked) txtstart = Common.dtstart.Rows[4]["pnname"].ToString();
            else txtstart = "";

            rp = new RPT(dt, path);
            rp.lobj.Add(new string[] { "txtend", txtend });
            rp.lobj.Add(new string[] { "txtstart", txtstart });
        }

        string getSQL()
        {
            return " SELECT          Speciald.SpID, Speciald.SpNo, Speciald.SDate, Speciald.SDate1, Speciald.SDate2, Speciald.EDate, Speciald.EDate1, "
                    + " Speciald.EDate2, Speciald.EmNo, Speciald.SpTrait, Speciald.SpTraitName, Speciald.ItNo, Speciald.ItName, "
                    + " Speciald.ItTrait, Speciald.ItUnit, Speciald.Itpkgqty, Speciald.Qty, Speciald.Price, Speciald.ItPrice, Speciald.Point, "
                    + " Speciald.Prs, Speciald.Memo, Speciald.BomID, Speciald.BomRec, Speciald.ReCordNo, Speciald.Sltflag, "
                    + " Speciald.Extflag, Speciald.ItDesp1, Speciald.ItDesp2, Speciald.ItDesp3, Speciald.ItDesp4, Speciald.ItDesp5, "
                    + " Speciald.ItDesp6, Speciald.ItDesp7, Speciald.ItDesp8, Speciald.ItDesp9, Speciald.ItDesp10, "
                    + " Special.SpNo AS SpecialSpNo, Special.SDate AS SpecialSDate, Special.SDate1 AS SpecialSDate1, "
                    + " Special.SDate2 AS SpecialSDate2, Special.EDate AS SpecialEDate, Special.EDate1 AS SpecialEDate1, "
                    + " Special.EDate2 AS SpecialEDate2, Special.EmNo AS SpecialEmNo, Special.EmName AS SpecialEmName, "
                    + " Special.SpTrait AS SpecialSpTrait, Special.SpMemo AS SpecialSpMemo, Special.RecordNo AS SpecialRecordNo "
                    + " FROM              Speciald LEFT OUTER JOIN "
                    + " Special ON Speciald.SpNo = Special.SpNo "
                    + " WHERE          (0 = 0) ";
        }
    }
}
