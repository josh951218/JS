using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_1
{
    public partial class FrmFordBshopd : Formbase
    {
        DataTable Dt = new DataTable();
        bool Isflag;

        public FrmFordBshopd()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FoNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmFord_Print_FoNo())
            {
                frm.TSeekNo = ((TextBox)sender).Text;

                switch (frm.ShowDialog())
                {
                    case DialogResult.OK:
                        ((TextBox)sender).Text = frm.TResult;
                        break;
                }
            }
        }

        bool Compare(TextBox tx, TextBox tx1)
        {
            Isflag = true;
            if (tx.Text != "" && tx1.Text != "")
            {
                if (string.CompareOrdinal(tx.Text, tx1.Text) > 0)
                {
                    Isflag = false;
                    MessageBox.Show("起始" + tx.Tag + "不可大於終止" + tx1.Tag + "，請確定", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx.Focus();
                }
            }
            return Isflag;
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (!Compare(FoNo, FoNo1))
            {
                MessageBox.Show("起始編號不能大於終止編號", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (radioT2.Checked)//已交未交
            {
                try
                {
                    Dt.Clear();
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    using (SqlCommand cmd = cn.CreateCommand())
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cn.Open();
                        string sql = "select fdd.pqty,fdd.punit,fdd.fono,fdd.fodate,fdd.fodate1,fdd.xa1no,fd.xa1name,fd.xa1par,fdd.fano,fd.faname1,fdd.itname,fdd.qty,fdd.itunit,產品類別=''"
                                   + ",fdd.itpkgqty,fdd.ittrait,fdd.qtyin,fdd.qtynotin,fdd.itno from fordd fdd left join ford fd on fdd.fono=fd.fono where 0=0 and fooverflag <> 1 ";

                        if (FoNo.Text.ToString().Trim().Length > 0) sql += " and fdd.fono >=@fono ";
                        if (FoNo1.Text.ToString().Trim().Length > 0) sql += " and fdd.fono <=@fono1 ";
                        
                        cmd.Parameters.AddWithValue("fono", FoNo.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("fono1", FoNo1.Text.ToString().Trim());

                        cmd.CommandText = sql;
                        da.Fill(Dt);
                    }

                    if (Dt.Rows.Count > 0)
                    {
                        //using (var frm = new FrmFordBshopdb())
                        //{
                        //    frm.Dt = Dt.Copy();
                        //    frm.ShowDialog();
                        //}
                        this.OpemInfoFrom<FrmFordBshopdb>(() =>
                            {
                                FrmFordBshopdb frm = new FrmFordBshopdb();
                                frm.Dt = Dt.Copy();
                                return frm;
                            });
                    }
                    else
                    {
                        MessageBox.Show("查詢不到資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                
            }
            else
            {
                try
                {
                    Dt.Clear();
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        string sql = "SELECT bsd.pqty,bsd.punit,bsd.fono, bsd.fano, bs.faname1, bsd.itno,bsd.itname,bsd.itunit,bsd.xa1no,bs.xa1name,bsd.xa1par,bsd.bsdate,bsd.bsdate1,單據='進貨'"
                            + ",bsd.bsno,bsd.qty,bsd.price,bsd.prs,bsd.taxprice,bsd.mny,bsd.itpkgqty,bsd.ittrait,產品類別='',序號='',單據日期='',採購數量=fdd.qty,fdd.qtyin,fdd.qtyNotIn,bs.faname2,fc.faaddr1,bs.fatel1,fc.fafax1,fc.faper1 "
                            + "FROM bshopd bsd left join bshop bs on bsd.bsno=bs.bsno left join fordd fdd on fdd.bomid=bsd.foid left join fact fc on bsd.fano=fc.fano where bsd.fono <> '' ";
                        if (FoNo.Text.ToString().Trim().Length > 0) sql += " and bsd.fono >=@fono ";
                        if (FoNo1.Text.ToString().Trim().Length > 0) sql += " and bsd.fono <=@fono1 ";
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("fono", FoNo.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("fono1", FoNo1.Text.ToString().Trim());
                        cmd.CommandText = sql;
                        SqlDataAdapter dd;
                        dd = new SqlDataAdapter(cmd);
                        dd.Fill(Dt);

                        DataTable temp = new DataTable();
                        sql = "SELECT rsd.pqty,rsd.punit,rsd.fono, rsd.fano, rs.faname1, rsd.itno,rsd.itname,rsd.itunit,rsd.xa1no,rs.xa1name,rsd.xa1par,rsd.bsdate,rsd.bsdate1,單據='退貨'"
                            + ",rsd.bsno,rsd.qty,rsd.price,rsd.prs,rsd.taxprice,rsd.mny,rsd.itpkgqty,rsd.ittrait,產品類別='',序號='',單據日期='',採購數量=fdd.qty,fdd.qtyin,fdd.qtyNotIn,rs.faname2,fc.faaddr1,rs.fatel1,fc.fafax1,fc.faper1 "
                            + "FROM rshopd rsd left join rshop rs on rsd.bsno=rs.bsno left join fordd fdd on fdd.bomid=rsd.foid left join fact fc on rsd.fano=fc.fano where rsd.fono <> '' ";
                        if (FoNo.Text.ToString().Trim().Length > 0) sql += " and rsd.fono >=@fono ";
                        if (FoNo1.Text.ToString().Trim().Length > 0) sql += " and rsd.fono <=@fono1 ";
                        cmd.CommandText = sql;
                        dd = new SqlDataAdapter(cmd);
                        dd.Fill(temp);

                        Dt.Merge(temp);

                        foreach (DataRow i in Dt.Rows)
                        {
                            if (Common.User_DateTime == 1) i["單據日期"] = Date.AddLine(i["bsdate"].ToString().Trim());
                            else i["單據日期"] = Date.AddLine(i["bsdate1"].ToString().Trim());
                        }
                        if (Dt.Rows.Count > 0)
                        {
                            //using (var frm = new FrmFordBshopda())
                            //{
                            //    frm.Dt = Dt.Copy();
                            //    frm.ShowDialog();
                            //}
                            this.OpemInfoFrom<FrmFordBshopda>(() =>
                            {
                                FrmFordBshopda frm = new FrmFordBshopda();
                                frm.Dt = Dt.Copy();
                                return frm;
                            });
                        }
                        else
                        {
                            MessageBox.Show("查詢不到資料", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void FrmFordBshopd_Shown(object sender, EventArgs e)
        {
            FoNo.Focus();
        }


    }
}
