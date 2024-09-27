using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using System.Data.SqlClient;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmSaleDisPrint : Formbase
    {
        JBS.JS.SaleDis jSaleDis;
        public string TDefault = "";

        public FrmSaleDisPrint()
        {
            InitializeComponent();
            this.jSaleDis = new JBS.JS.SaleDis();
        }

        private void FrmSaleDisPrint_Load(object sender, EventArgs e)
        {
            this.DiNo.Text = this.TDefault;
            this.DiNo1.Text = this.TDefault;
        }

        private void InNo_Validating(object sender, CancelEventArgs e)
        {
            //if (btnExit.Focused)
            //    return;

            //jSaleDis.IsInNoFormat(sender, e);
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
                SELECT  saledisd.inid, saledisd.inno, saledisd.indate, saledisd.indate1, saledisd.itno, saledisd.itname, saledisd.itunit, 
                        saledisd.punit, saledisd.bomid, saledisd.bomrec, saledisd.ittrait, saledisd.itpkgqty, saledisd.pqty, saledisd.qty, 
                        saledisd.price, saledisd.prs, saledisd.taxprice, saledisd.mny, saledisd.priceb, saledisd.taxpriceb, saledisd.mnyb, 
                        saledisd.memo, saledisd.recordno, saledisd.didate, saledisd.didate1,
                        saledis.dino, saledis.cono, saledis.coname1, saledis.cuno, saledis.cuname1, 
                        saledis.invtaxno, saledis.x5no, saledis.x3no, saledis.invname, saledis.invaddr1, saledis.invalid, saledis.tax, 
                        saledis.taxmny, saledis.totmny, saledis.rate, saledis.xa1par, saledis.inmemo, saledis.recordno AS rdno
                FROM    saledisd 
                LEFT OUTER JOIN saledis ON saledisd.dino = saledis.dino
                Where saledisd.DiNo >= @DiNo And saledisd.DiNo <= @DiNo1 "; 
                da.Fill(temp);
            }

            if (temp.Rows.Count == 0)
            {
                MessageBox.Show("查無資料!");
                return;
            }

            var path = Common.reportaddress + @"Report\銷貨折讓作業簡要表.rpt";

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
 