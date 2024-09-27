using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Data.SqlClient;

namespace S_61.S2
{
    public partial class FrmBShopDisPrint : Formbase
    {
        JBS.JS.BshopDis jBshopDis;
        public string TDefault = "";

        public FrmBShopDisPrint()
        {
            InitializeComponent();
            this.jBshopDis = new JBS.JS.BshopDis();
        }

        private void FrmBShopDisPrint_Load(object sender, EventArgs e)
        {
            this.DiNo.Text = this.TDefault;
            this.DiNo1.Text = this.TDefault;
        }

        private void InNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            jBshopDis.IsInNoFormat(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void OutReport(RptMode mode)
        {
            DataTable temp = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("DiNo", DiNo.Text);
                cmd.Parameters.AddWithValue("DiNo1", DiNo1.Text);

                cmd.CommandText = @"
                SELECT      bshopdisd.inid, bshopdisd.inno, bshopdisd.indate, bshopdisd.indate1, bshopdisd.itno, bshopdisd.itname, bshopdisd.itunit, 
                            bshopdisd.punit, bshopdisd.bomid, bshopdisd.bomrec, bshopdisd.ittrait, bshopdisd.itpkgqty, bshopdisd.pqty, bshopdisd.qty, 
                            bshopdisd.price, bshopdisd.prs, bshopdisd.taxprice, bshopdisd.mny, bshopdisd.priceb, bshopdisd.taxpriceb, bshopdisd.mnyb, 
                            bshopdisd.memo, bshopdisd.recordno, bshopdisd.didate, bshopdisd.didate1,
                            bshopdis.cono, bshopdis.coname1, bshopdis.fano, bshopdis.faname1, 
                            bshopdis.invtaxno, bshopdis.x5no, bshopdis.x3no, bshopdis.invname, bshopdis.invaddr1, bshopdis.invalid, bshopdis.tax, 
                            bshopdis.taxmny, bshopdis.totmny, bshopdis.rate, bshopdis.xa1par, bshopdis.inmemo, bshopdis.recordno AS rdno,bshopdis.dino
                FROM        bshopdisd 
                LEFT OUTER JOIN bshopdis ON bshopdisd.dino = bshopdis.dino
                Where bshopdisd.DiNo >= @DiNo And bshopdisd.DiNo1 <= @DiNo1 ";
                da.Fill(temp);
            }

            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            var path = Common.reportaddress + @"Report\進貨折讓作業簡要表.rpt";

            RPT rp = new RPT(temp, path);

            if (mode == RptMode.Print) rp.Print();
            else if (mode == RptMode.PreView) rp.PreView();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.Print);
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            OutReport(RptMode.PreView);
        }
    }
}
