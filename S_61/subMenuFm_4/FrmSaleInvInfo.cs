using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_4
{
    public partial class FrmSaleInvInfo : Formbase
    {
        DataTable table ;
        DataTable pos ;
        DataTable nul;
        DataTable printtb;//主檔
        DataTable printtbD;//明細檔
        SqlDataAdapter dd;

        public FrmSaleInvInfo()
        {
            InitializeComponent();
            //pVar.FrmSaleInvInfo = this;
            Date1.SetDateLength();
            Date2.SetDateLength();
        }

        private void FrmSaleInvInfo_Load(object sender, EventArgs e)
        {
            Date2.Text = Date.GetDateTime(Common.User_DateTime);

            if (Common.User_DateTime == 1)
                Date1.Text = Date.GetDateTime(1).Substring(0, 5) + "01";
            else
                Date1.Text = Date.GetDateTime(2).Substring(0, 6) + "01";
         
            Date1.Focus();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (Date1.Text.BigThen(Date2.Text))
            {
                MessageBox.Show("起始異動日期不可大於終止異動日期", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Date1.Focus();
                return;
            }

            table = new DataTable();
            pos = new DataTable();
            printtb = new DataTable();
            printtbD = new DataTable();
            nul = new DataTable();

            #region 銷貨
            if (radioT1.Checked || radioT2.Checked)
            {
                //先撈銷貨單發票編號
                string sql = "";
                sql = "select sano,sadate,sadate1,invdate,invdate1,invno,invtaxno,invname,cuno,cuname1,taxmnyb,taxb,totmnyb ,作廢='' from sale where '0'='0'"
                    + " and invno != ''"
                    + " and invdate >=@invdate"
                    + " and invdate <=@invdate1";
                if (InvNo1.Text.Trim() != "")
                    sql += " and invno >=@invno";
                if (InvNo2.Text.Trim() != "")
                    sql += " and invno <=@invno1";
                sql += " order by invno";
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("invdate", Date.ToTWDate(Date1.Text.Trim()));
                        cmd.Parameters.AddWithValue("invdate1", Date.ToTWDate(Date2.Text.Trim()));
                        if (InvNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("invno", InvNo1.Text.Trim());
                        if (InvNo2.Text.Trim() != "") cmd.Parameters.AddWithValue("invno1", InvNo2.Text.Trim());
                        cmd.CommandText = sql;

                        using (dd = new SqlDataAdapter(cmd))
                        {
                            table.Clear();
                            dd.Fill(table);
                        }
                    }
                }
                if (Common.Sys_StockKind == 2)//若有用POS 再去posinv撈連開發票
                {
                    sql = "select posinv.sano,posinv.invdate as sadate,posinv.invdate1 as sadate1,posinv.invdate,posinv.invdate1,posinv.invno,posinv.invtaxno,sale.invname,sale.cuno,sale.cuname1,"
                        + " taxmnyb=0.0,taxb=0.0,totmnyb=0.0,作廢=''"
                        + " from posinv left join sale on posinv.sano=sale.sano where '0'='0'"
                        + " and posinv.invno != ''"
                        + " and posinv.totmny = 0"
                        + " and posinv.invdate >= @invdate"
                        + " and posinv.invdate <= @invdate1";
                    if (InvNo1.Text.Trim() != "")
                        sql += " and posinv.invno >= @invno";
                    if (InvNo2.Text.Trim() != "")
                        sql += " and posinv.invno <= @invno1";
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("invdate", Date.ToTWDate(Date1.Text.Trim()));
                            cmd.Parameters.AddWithValue("invdate1", Date.ToTWDate(Date2.Text.Trim()));
                            if (InvNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("invno", InvNo1.Text.Trim());
                            if (InvNo2.Text.Trim() != "") cmd.Parameters.AddWithValue("invno1", InvNo2.Text.Trim());
                            cmd.CommandText = sql;
                            using (dd = new SqlDataAdapter(cmd))
                            {
                                pos.Clear();
                                dd.Fill(pos);
                                table.Merge(pos);
                            }
                        }
                    }
                }
                //撈出作廢發票
                sql = "select invno,invdate,invdate1 from nullify where '0'='0'"
                    + " and invdate >=@invdate"
                    + " and invdate <=@invdate1";
                if (InvNo1.Text.Trim() != "")
                    sql += " and invno >=@invno";
                if (InvNo2.Text.Trim() != "")
                    sql += " and invno <=@invno1";
                sql += " order by invno";
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("invdate", Date.ToTWDate(Date1.Text.Trim()));
                        cmd.Parameters.AddWithValue("invdate1", Date.ToTWDate(Date2.Text.Trim()));
                        if (InvNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("invno", InvNo1.Text.Trim());
                        if (InvNo2.Text.Trim() != "") cmd.Parameters.AddWithValue("invno1", InvNo2.Text.Trim());
                        cmd.CommandText = sql;
                        using (dd = new SqlDataAdapter(cmd))
                        {
                            nul.Clear();
                            dd.Fill(nul);
                        }


                        cmd.CommandText = "select sale.invno,sale.invdate,sale.invdate1 from rsale left join sale on rsale.fromsale=sale.sano where LEN(sale.sano)>0 and LEN(sale.invno)>0 and sale.bracket='前台' "
                             + " and sale.invdate >=@invdate"
                             + " and sale.invdate <=@invdate1";
                        if (InvNo1.Text.Trim() != "")
                            cmd.CommandText += " and sale.invno >=@invno";
                        if (InvNo2.Text.Trim() != "")
                            cmd.CommandText += " and sale.invno <=@invno1";
                        cmd.CommandText += " order by sale.invno";
                        using (dd = new SqlDataAdapter(cmd))
                        {
                            dd.Fill(nul);
                        }
                    }
                }
                for (int i = 0; i < nul.Rows.Count; i++)
                {
                    //先檢查銷貨單 或  pos 有沒有這張發票
                    var temp = table.AsEnumerable().Count(r => r["invno"].ToString().Trim() == nul.Rows[i]["invno"].ToString().Trim());
                    if (temp == 0)
                    {
                        DataRow dr = table.NewRow();
                        dr["invno"] = nul.Rows[i]["invno"].ToString().Trim();
                        dr["invdate"] = nul.Rows[i]["invdate"].ToString().Trim();
                        dr["invdate1"] = nul.Rows[i]["invdate1"].ToString().Trim();
                        dr["taxmnyb"] = 0;
                        dr["taxb"] = 0;
                        dr["totmnyb"] = 0;
                        dr["作廢"] = "作廢";
                        table.Rows.Add(dr);
                        table.AcceptChanges();
                    }
                    else
                    {
                        table.AsEnumerable().ToList().ForEach(r =>
                        {
                            if (r["invno"].ToString().Trim() == nul.Rows[i]["invno"].ToString())
                                r["作廢"] = "作廢";
                        });
                    }
                }

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //把同一張發票編號 銷貨金額加在一起 此時printtb 中的 invno 是唯一值
                printtb.Clear();
                printtb = table.Clone();
                if(!table.Columns.Contains("標註"))
                    table.Columns.Add("標註", typeof(String));
                string nostr = "";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["標註"].ToString() == "V") continue;
                    var t = table.AsEnumerable().Where(r => r["invno"].ToString().Trim() == table.Rows[i]["invno"].ToString().Trim());

                    table.Rows[i]["taxmnyb"] = t.Sum(r => r["taxmnyb"].ToDecimal("f"+Common.M));
                    table.Rows[i]["taxb"] = t.Sum(r => r["taxb"].ToDecimal("f" + Common.M));
                    table.Rows[i]["totmnyb"] = t.Sum(r => r["totmnyb"].ToDecimal("f" + Common.M));
                    table.Rows[i]["sadate"] = Date.AddLine(table.Rows[i]["sadate"].ToString());
                    table.Rows[i]["sadate1"] = Date.AddLine(table.Rows[i]["sadate1"].ToString());
                    table.Rows[i]["invdate"] = Date.AddLine(table.Rows[i]["invdate"].ToString());
                    table.Rows[i]["invdate1"] = Date.AddLine(table.Rows[i]["invdate1"].ToString());

                    printtb.ImportRow(table.Rows[i]);
                    printtb.AcceptChanges();
                    table.AsEnumerable().Where(r => r["invno"].ToString().Trim() == table.Rows[i]["invno"].ToString().Trim()).ToList().ForEach(r => r["標註"] = "V");

                    //下一段sql 需要用到 在這先跑
                    if (radioT2.Checked && table.Rows[i]["作廢"].ToString() == "作廢")
                        continue;
                    else
                        nostr += "'" + table.Rows[i]["invno"].ToString().Trim() + "',";
                }
                nostr = nostr.Substring(0, nostr.Length - 1);
                //撈銷貨明細
                if(radioT1.Checked)
                    sql = "select invno,itno,itname,itunit,ROUND(qty," + Common.Q + ") AS qty,ROUND(taxpriceb," + 6 + ") AS taxpriceb,ROUND(taxprice," + 6 + ") AS taxprice,ROUND(mnyb," + Common.M + ") AS mnyb,saled.recordno from saled left join sale on saled.sano = sale.sano where sale.invno in (" + nostr + ") order by invno,itno";
                else if(radioT2.Checked)
                    sql = "select itno,"
                        + "(select itname from item where saled.itno=item.itno) AS itname,itunit,SUM(ROUND(qty," + Common.Q + ")) AS qty,SUM(ROUND(mnyb," + Common.M + ")) AS mnyb,saled.recordno from saled left join sale on saled.sano = sale.sano "
                        + "where sale.invno in (" + nostr + ") group by itno,itunit order by itno";
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (dd = new SqlDataAdapter(sql, cn))
                    {
                        printtbD.Clear();
                        dd.Fill(printtbD);
                    }
                }
                if (radioT1.Checked)
                {
                    //using (var frm = new FrmSaleInvInfob())
                    //{
                    //    frm.printtb = printtb.AsEnumerable().OrderBy(r => r["invno"].ToString()).CopyToDataTable();
                    //    printtbD.Merge(printtb);
                    //    frm.printtbD = printtbD.AsEnumerable().OrderBy(r => r["invno"].ToString()).ThenBy(r=>r["itname"].ToString()).CopyToDataTable();
                    //    frm.Date1.Text = Date1.Text.Trim();
                    //    frm.Date2.Text = Date2.Text.Trim();
                    //    frm.ShowDialog();
                    //}
                    this.OpemInfoFrom<FrmSaleInvInfob>(() =>
                            {
                                FrmSaleInvInfob frm = new FrmSaleInvInfob();
                                frm.printtb = printtb.AsEnumerable().OrderBy(r => r["invno"].ToString()).CopyToDataTable();
                                printtbD.Merge(printtb);
                                frm.printtbD = printtbD.AsEnumerable().OrderBy(r => r["invno"].ToString()).ThenBy(r => r["itname"].ToString()).CopyToDataTable();
                                frm.Date1.Text = Date1.Text.Trim();
                                frm.Date2.Text = Date2.Text.Trim();
                                return frm;
                            });
                }
                if (radioT2.Checked)
                {
                    //using (var frm = new FrmSaleInvInfoc())
                    //{
                    //    frm.printtbD = printtbD.Copy();
                    //    frm.Date1.Text = Date1.Text.Trim();
                    //    frm.Date2.Text = Date2.Text.Trim();
                    //    frm.InvNo1.Text = printtb.AsEnumerable().Min(r => r["invno"].ToString());
                    //    frm.InvNo2.Text = printtb.AsEnumerable().Max(r => r["invno"].ToString());
                    
                    //    frm.ShowDialog();
                    //}
                    this.OpemInfoFrom<FrmSaleInvInfoc>(() =>
                            {
                                FrmSaleInvInfoc frm = new FrmSaleInvInfoc();
                                frm.printtbD = printtbD.Copy();
                                frm.Date1.Text = Date1.Text.Trim();
                                frm.Date2.Text = Date2.Text.Trim();
                                frm.InvNo1.Text = printtb.AsEnumerable().Min(r => r["invno"].ToString());
                                frm.InvNo2.Text = printtb.AsEnumerable().Max(r => r["invno"].ToString());
                                return frm;
                            });
                }
            }
            #endregion

            #region 進貨
            if (radioT3.Checked || radioT4.Checked)
            {
                string sql = "";
                sql = "select bsno,bsdate,bsdate1,invdate,invdate1,invno,invname,fano,faname1,taxmnyb,taxb,totmnyb from bshop where '0'='0'"
                    + " and invno != ''"
                    + " and invdate >=@invdate"
                    + " and invdate <=@invdate1";
                if (InvNo1.Text.Trim() != "")
                    sql += " and invno >=@invno";
                if (InvNo2.Text.Trim() != "")
                    sql += " and invno <=@invno1";
                sql += " order by invno";
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("invdate", Date.ToTWDate(Date1.Text.Trim()));
                        cmd.Parameters.AddWithValue("invdate1", Date.ToTWDate(Date2.Text.Trim()));
                        if (InvNo1.Text.Trim() != "") cmd.Parameters.AddWithValue("invno", InvNo1.Text.Trim());
                        if (InvNo2.Text.Trim() != "") cmd.Parameters.AddWithValue("invno1", InvNo2.Text.Trim());
                        cmd.CommandText = sql;

                        using (dd = new SqlDataAdapter(cmd))
                        {
                            table.Clear();
                            dd.Fill(table);
                        }
                    }
                }

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                printtb.Clear();
                printtb = table.Clone();
                if (!table.Columns.Contains("標註"))
                    table.Columns.Add("標註", typeof(String));
                string nostr = "";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["標註"].ToString() == "V") continue;
                    var t = table.AsEnumerable().Where(r => r["invno"].ToString().Trim() == table.Rows[i]["invno"].ToString().Trim());

                    table.Rows[i]["taxmnyb"] = t.Sum(r => r["taxmnyb"].ToDecimal("f" + Common.M));
                    table.Rows[i]["taxb"] = t.Sum(r => r["taxb"].ToDecimal("f" + Common.M));
                    table.Rows[i]["totmnyb"] = t.Sum(r => r["totmnyb"].ToDecimal("f" + Common.M));
                    table.Rows[i]["bsdate"] = Date.AddLine(table.Rows[i]["bsdate"].ToString());
                    table.Rows[i]["bsdate1"] = Date.AddLine(table.Rows[i]["bsdate1"].ToString());
                    table.Rows[i]["invdate"] = Date.AddLine(table.Rows[i]["invdate"].ToString());
                    table.Rows[i]["invdate1"] = Date.AddLine(table.Rows[i]["invdate1"].ToString());

                    printtb.ImportRow(table.Rows[i]);
                    printtb.AcceptChanges();
                    table.AsEnumerable().Where(r => r["invno"].ToString().Trim() == table.Rows[i]["invno"].ToString().Trim()).ToList().ForEach(r => r["標註"] = "V");

                    //下一段sql 需要用到 在這先跑
                    if (radioT2.Checked && table.Rows[i]["作廢"].ToString() == "作廢")
                        continue;
                    else
                        nostr += "'" + table.Rows[i]["invno"].ToString().Trim() + "',";
                }
                nostr = nostr.Substring(0, nostr.Length - 1);
                //撈進貨明細
                if (radioT3.Checked)
                    sql = "select invno,itno,itname,itunit,ROUND(qty," + Common.Q + ") AS qty,ROUND(mnyb," + Common.M + ") AS mnyb ,ROUND(taxprice," + 6 + ") AS taxprice ,ROUND(taxpriceb," + 6 + ") AS taxpriceb,bshopd.recordno from bshopd left join bshop on bshopd.bsno = bshop.bsno where bshop.invno in (" + nostr + ") order by invno,itno";
                else if (radioT4.Checked)
                    sql = "select itno,"
                        + "(select itname from item where bshopd.itno=item.itno) AS itname,itunit,SUM(ROUND(qty," + Common.Q + ")) AS qty,SUM(ROUND(mnyb," + Common.M + ")) AS mnyb,bshopd.recordno from bshopd left join bshop on bshopd.bsno = bshop.bsno "
                        + "where bshop.invno in (" + nostr + ") group by itno,itunit order by itno";
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                {
                    using (dd = new SqlDataAdapter(sql, cn))
                    {
                        printtbD.Clear();
                        dd.Fill(printtbD);
                    }
                }
                if (radioT3.Checked)
                {
                    //using (var frm = new FrmSaleInvInfod())
                    //{
                    //    frm.printtb = printtb.AsEnumerable().OrderBy(r => r["invno"].ToString()).CopyToDataTable();
                    //    printtbD.Merge(printtb);
                    //    frm.printtbD = printtbD.AsEnumerable().OrderByDescending(r => r["invdate"].ToString()).CopyToDataTable();
                    //    frm.Date1.Text = Date1.Text.Trim();
                    //    frm.Date2.Text = Date2.Text.Trim();
                      
                    //    frm.ShowDialog();
                    //}
                    this.OpemInfoFrom<FrmSaleInvInfod>(() =>
                            {
                                FrmSaleInvInfod frm = new FrmSaleInvInfod();
                                frm.printtb = printtb.AsEnumerable().OrderBy(r => r["invno"].ToString()).CopyToDataTable();
                                printtbD.Merge(printtb);
                                frm.printtbD = printtbD.AsEnumerable().OrderByDescending(r => r["invdate"].ToString()).CopyToDataTable();
                                frm.Date1.Text = Date1.Text.Trim();
                                frm.Date2.Text = Date2.Text.Trim();
                                return frm;
                            });
                }
                if (radioT4.Checked)
                {
                    //using (var frm = new FrmSaleInvInfoe())
                    //{
                    //    frm.printtbD = printtbD.Copy();
                    //    frm.Date1.Text = Date1.Text.Trim();
                    //    frm.Date2.Text = Date2.Text.Trim();
                    //    frm.InvNo1.Text = printtb.AsEnumerable().Min(r => r["invno"].ToString());
                    //    frm.InvNo2.Text = printtb.AsEnumerable().Max(r => r["invno"].ToString());
                     
                    //    frm.ShowDialog();
                    //}
                    this.OpemInfoFrom<FrmSaleInvInfoe>(() =>
                            {
                                FrmSaleInvInfoe frm = new FrmSaleInvInfoe();
                                frm.printtbD = printtbD.Copy();
                                frm.Date1.Text = Date1.Text.Trim();
                                frm.Date2.Text = Date2.Text.Trim();
                                frm.InvNo1.Text = printtb.AsEnumerable().Min(r => r["invno"].ToString());
                                frm.InvNo2.Text = printtb.AsEnumerable().Max(r => r["invno"].ToString());
                                return frm;
                            });
                }
            }

            #endregion

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Date1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "")
            {
                MessageBox.Show("發票日期不可為空", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            else
            {
                if (!tb.IsDateTime())
                {
                    MessageBox.Show("日期格式錯誤", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }

        private void InvNo1_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused) return;
            TextBox tb = sender as TextBox;
            if (tb.Text.Trim() == "") return;
            bool erorr = false;
            Char[] C = tb.Text.ToCharArray();
            if (tb.Text.Trim().Length < 10)
                erorr = true;
            for (int i = 0; i < C.Length; i++)
            {
                if (erorr) break;
                if (i < 2)
                {
                    if (!Char.IsLetter(C[i]))
                        erorr = true;
                }
                else
                {
                    if (!Char.IsDigit(C[i]))
                        erorr = true;
                }
            }
            if (erorr)
            {
                MessageBox.Show("發票格式錯誤", "訊息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            tb.Text = tb.Text.ToUpper();
        }
    }
}
