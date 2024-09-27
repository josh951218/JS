using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmItemInvPrint : Formbase
    {
        public string No = "";
        DataTable dt = new DataTable();
        RPT rp;
        string str = "";

        public FrmItemInvPrint()
        {
            InitializeComponent();
        }

        private void FrmItemInvPrint_Load(object sender, EventArgs e)
        {
            IvNo.Text = IvNo1.Text = No;
            SetStr(); 

            radio3.SetUserDefineRpt("庫存盤點作業簡要自定一.rpt");
            radio4.SetUserDefineRpt("庫存盤點作業盤點自定一.rpt"); 
        }

        void LoadDB()
        {
            dt.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    string sql = str;
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        if (IvNo.Text.Trim().Length > 0)
                        {
                            cmd.Parameters.AddWithValue("ivno", IvNo.Text.Trim());
                            sql += " and ivd.ivno >= @ivno";
                        }
                        if (IvNo1.Text.Trim().Length > 0)
                        {
                            cmd.Parameters.AddWithValue("ivno1", IvNo1.Text.Trim());
                            sql += " and ivd.ivno <= @ivno1";
                        }
                        cmd.CommandText = sql + " order by ivd.ivno,ivd.recordno";
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
        }

        void paramsInit()
        {
            LoadDB();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = "";
            if (radio1.Checked)
            {
                if (radioT1.Checked)
                    path = Common.reportaddress + "Report\\庫存盤點作業簡要表B.rpt";
                else
                    path = Common.reportaddress + "Report\\庫存盤點作業簡要表A.rpt";
            }
            else if (radio2.Checked)
            {
                if (radioT1.Checked)
                    path = Common.reportaddress + "Report\\庫存盤點作業盤點空白表B.rpt";
                else
                    path = Common.reportaddress + "Report\\庫存盤點作業盤點空白表A.rpt";
            }
            else if (radio3.Checked)
            {
                path = Common.reportaddress + "Report\\庫存盤點作業簡要自定一.rpt";
            }
            else if (radio4.Checked)
            {
                path = Common.reportaddress + "Report\\庫存盤點作業盤點自定一.rpt";
            }

            string txtend = "";
            if (radioT3.Checked) txtend = Common.dtEnd.Rows[5]["tamemo"].ToString();
            else if (radioT4.Checked) txtend = Common.dtEnd.Rows[8]["tamemo"].ToString();
            else if (radioT5.Checked) txtend = Common.dtEnd.Rows[11]["tamemo"].ToString();
            else if (radioT6.Checked) txtend = Common.dtEnd.Rows[14]["tamemo"].ToString();
            else if (radioT7.Checked) txtend = Common.dtEnd.Rows[16]["tamemo"].ToString();
            else txtend = "";

            rp = new RPT(dt, path);
            rp.lobj.Add(new string[] { "txtend", txtend });
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.Print();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            paramsInit();
            rp.PreView();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void SetStr()
        {
            str = " SELECT  IvD.ivID, IvD.ivno, IvD.ivdate, IvD.ivdate1, IvD.ivdate2, IvD.emno, IvD.emname, IvD.stno, IvD.stname, IvD.itno, "
                + " IvD.itname, IvD.ittrait, IvD.itunit, IvD.itpkgqty, IvD.stkqty, IvD.qty, IvD.invqty, IvD.price, IvD.prs, IvD.rate, IvD.taxprice, "
                + " IvD.mny, IvD.priceb, IvD.taxpriceb, IvD.mnyb, IvD.memo, IvD.lowzero, IvD.bomid, IvD.bomrec, IvD.recordno, "
                + " IvD.sltflag, IvD.extflag, IvD.bracket, IvD.itdesp1, IvD.itdesp2, IvD.itdesp3, IvD.itdesp4, IvD.itdesp5, IvD.itdesp6, "
                + " IvD.itdesp7, IvD.itdesp8, IvD.itdesp9, IvD.itdesp10, IvD.IsTrans, Iv.ivno AS mIvno, Iv.ivdate AS mIvdate, "
                + " Iv.ivdate1 AS mIvdate1, Iv.ivdate2 AS mIvdate2, Iv.adno AS mAdno, Iv.stno AS mStno, Iv.stname AS mStname, "
                + " Iv.emno AS mEmno, Iv.emname AS mEmname, Iv.sum AS mSum, Iv.memo AS mMemo, Iv.rec AS mRec, "
                + " Iv.bracket AS mBracket, Iv.appscno AS mappscno, Iv.appdate AS mappdate, Iv.edtscno AS medtscno, "
                + " Iv.edtdate AS medtdate, Iv.dealflag AS mdealflag, Iv.adjflag AS madjflag, Iv.IsTrans AS mIstrans, Iv.recordno AS mrecordno "
                + " FROM  IvD LEFT OUTER JOIN "
                + " Iv ON IvD.ivno = Iv.ivno "
                + " WHERE  (0 = 0) ";
        }

        private void lb1_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            var p = lb.Parent.Controls.OfType<RadioButton>().Where(r => r.Name.Last() == lb.Name.Last());
            if (p.Count() > 0)
            {
                if (p.FirstOrDefault().Enabled) p.FirstOrDefault().Checked = true;
            }
        }

        private void radio1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            var p = rd.Parent.Controls.OfType<Label>();
            foreach (Label lb in p)
            {
                if (lb.Name.Last() == rd.Name.Last()) lb.BackColor = Color.LightBlue;
                else lb.BackColor = Color.Transparent;
            }
        }
    }
}
