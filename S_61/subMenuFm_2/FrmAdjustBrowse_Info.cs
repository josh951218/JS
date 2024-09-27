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

namespace S_61.subMenuFm_2
{
    public partial class FrmAdjustBrowse_Info : Formbase
    {
        public FrmAdjustBrowse_Info()
        {
            InitializeComponent();
        }

        private void FrmAdjustBrowse_Info_Load(object sender, EventArgs e)
        {
            if (Common.User_DateTime == 1)
            {
                AdDate.MaxLength = AdDate1.MaxLength = 7;
                AdDate.Text = Date.GetDateTime(1, false);
                AdDate.Text = AdDate.Text.Remove(5) + "01";
                AdDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                AdDate.MaxLength = AdDate1.MaxLength = 8;
                AdDate.Text = Date.GetDateTime(2, false);
                AdDate.Text = AdDate.Text.Remove(6) + "01";
                AdDate1.Text = Date.GetDateTime(2, false);
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            string AdDate_ = AdDate.Text.Trim();
            string AdDate_1 = AdDate1.Text.Trim();
            string stno_ = StNo.Text.Trim();
            string stno_1 = StNo1.Text.Trim();
            string emno_ = EmNo.Text.Trim();
            string emno_1 = EmNo1.Text.Trim();
            string itno_ = ItNo.Text.Trim();
            string itno_1 = ItNo1.Text.Trim();
            string memo = Memo.Text.Trim();
            string WhereStr = "";
            SqlParameterCollection SqlParameterCollection ;
            if(檢查參數1是否大於參數2()) return;
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {         
                string SqlStr = @"select  序號 = 0.0, 調整日期=SUBSTRING(addate,1,3) + '/'+SUBSTRING(addate,4,2)+ '/'+SUBSTRING(addate,6,2),調整日期1= SUBSTRING(addate1,1,4) + '/'+SUBSTRING(addate1,5,2)+ '/'+SUBSTRING(addate1,7,2), empl.emno as 人員編號,empl.emname as 人員姓名 ,* from adjustd left join empl on adjustd.emno = empl.emno where 0=0 ";
                #region set cm.patamater
                string 西元年 ="";
                if (Common.User_DateTime == 2) 西元年="1";
                if (AdDate_.Length > 0)
                {
                    cmd.Parameters.AddWithValue("AdDate", AdDate_);
                    WhereStr += " and adjustd.AdDate" + 西元年.ToString() + " >= @AdDate ";
                }
                if (AdDate_1.Length > 0)
                {
                    cmd.Parameters.AddWithValue("AdDate1", AdDate_1);
                    WhereStr += " and adjustd.AdDate" + 西元年.ToString() + " <= @AdDate1 ";
                }
                if (stno_.Length > 0)
                {
                    cmd.Parameters.AddWithValue("stno", stno_);
                    WhereStr += " and adjustd.stno >= @stno ";
                }
                if (stno_.Length > 0)
                {
                    cmd.Parameters.AddWithValue("stno1", stno_1);
                    WhereStr += " and adjustd.stno <= @stno ";
                }
                if (itno_.Length > 0)
                {
                    cmd.Parameters.AddWithValue("itno", itno_);
                    WhereStr += " and adjustd.itno >= @itno ";
                }
                if (itno_1.Length > 0)
                {
                    cmd.Parameters.AddWithValue("itno1", itno_1);
                    WhereStr += " and adjustd.itno < =@itno1 ";
                }
                if (memo.Length > 0)
                {
                    cmd.Parameters.AddWithValue("memo", memo);
                    WhereStr += " and adjustd.memo = @memo ";
                }
                SqlParameterCollection = cmd.Parameters;
                #endregion
                cmd.CommandText = SqlStr + WhereStr+ " order by addate,adno";
                da.Fill(dt);
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("資料為空！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.OpemInfoFrom<FrmAdjustBrowse_Infob>(() =>
                            {
                                FrmAdjustBrowse_Infob frm = new FrmAdjustBrowse_Infob();
                                frm.dt_明細 = dt;
                                frm.SqlParameterCollection = SqlParameterCollection;
                                frm.WhereStr = WhereStr;
                                frm.firstDay = Date.AddLine(AdDate.Text.Trim());
                                frm.LeastDay = Date.AddLine(AdDate1.Text.Trim());
                                return frm;
                            });
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new S_61.SOther.FrmItemb())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    ((Control)sender).Text = frm.TResult;
            }
        }

        private void Memo_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmSale_Memo())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    Memo.Text = frm.Memo.GetUTF8(60);
            }
        }

        private void AdDate_Validating(object sender, CancelEventArgs e)
        {
            var TextBox = ((TextBox)sender);
            if (TextBox.Text.Trim() == "") return;
            if (!((TextBox)sender).IsDateTime())
            {
                MessageBox.Show("您輸入的日期格是不正確!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBox.SelectAll();
                e.Cancel = true;
            }
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            var TextBox = ((TextBox)sender);
            using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Stkroom>())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    ((Control)sender).Text = frm.TResult;
                }
            }
            TextBox.SelectAll();
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            var TextBox = ((TextBox)sender);
            if (TextBox.Text.Trim() == "") return;

            if (!BeingStno(((Control)sender).Text.Trim()))
            {
                using (var frm = new JBS.JS.FrmXxBrow<JBS.JS.Stkroom>())
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        ((Control)sender).Text = frm.TResult;
                    }
                }
                TextBox.SelectAll();
                e.Cancel = true;
            }
        }

        private bool BeingStno(string stno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                cn.Open();
                cm.Parameters.AddWithValue("stno", stno);
                cm.CommandText = "select count(*) from Stkroom where stno = @stno";
                if (int.Parse(cm.ExecuteScalar().ToString()) == 0)
                    return false;
                return true;
            }
        }

        private bool BeingItno(string itno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                cn.Open();
                cm.Parameters.AddWithValue("itno", itno);
                cm.CommandText = "select count(*) from item where itno = @itno";
                if (int.Parse(cm.ExecuteScalar().ToString()) == 0)
                    return false;
                return true;
            }
        }

        private bool BeingEmno(string emno)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cm = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cm))
            using (DataTable dt_ = new DataTable())
            {
                cn.Open();
                cm.Parameters.AddWithValue("emno", emno);
                cm.CommandText = "select count(*) from empl where emno = @emno";
                if (int.Parse(cm.ExecuteScalar().ToString()) == 0)
                    return false;
                return true;
            }
        }

        private void ItNo_Validating(object sender, CancelEventArgs e)
        {
            var TextBox = ((TextBox)sender);
            if (TextBox.Text.Trim() == "") return;
            if (!BeingItno(((Control)sender).Text.Trim()))
            {
                using (var frm = new S_61.SOther.FrmItemb())
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        ((Control)sender).Text = frm.TResult;
                }
                TextBox.SelectAll();
                e.Cancel = true;
            }
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            var TextBox = ((TextBox)sender);
            using (var frm = new  S_61.SOther.FrmEmplb())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    ((Control)sender).Text = frm.TResult;
            }
            TextBox.SelectAll();
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            var TextBox = ((TextBox)sender);
            if (TextBox.Text.Trim() == "") return;
            if (!BeingEmno(((Control)sender).Text.Trim()))
            {
                using (var frm = new S_61.SOther.FrmEmplb())
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        ((Control)sender).Text = frm.TResult;
                }
                TextBox.SelectAll();
                e.Cancel = true;
            }
        }

        private bool 檢查參數1是否大於參數2()
        {
            if (StNo.Text.Length > 0 && StNo1.Text.Length > 0)
            {
                if (StNo.Text.BigThen(StNo1.Text))
                {
                    MessageBox.Show("起始倉庫編號不得大於終止倉庫編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }

            if (EmNo.Text.Length > 0 && EmNo1.Text.Length > 0)
            {
                if (EmNo.Text.BigThen(EmNo1.Text))
                {
                    MessageBox.Show("起始人員編號不得大於終止人員編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }

            if (ItNo.Text.Length > 0 && ItNo1.Text.Length > 0)
            {
                if (ItNo.Text.BigThen(ItNo1.Text))
                {
                    MessageBox.Show("起始產品編號不得大於終止產品編號！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }

            if (AdDate.Text.Length > 0 && AdDate1.Text.Length > 0)
            {
                if (AdDate.Text.BigThen(AdDate1.Text))
                {
                    MessageBox.Show("起始寄庫日期不得大於終止寄庫日期！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            return false;
        }


     
    }
}
