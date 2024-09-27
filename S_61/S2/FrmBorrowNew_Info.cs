using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S2
{
    public partial class FrmBorrowNew_Info : Formbase
    {
        JBS.JS.xEvents xe;

        DataTable dtBorrD = new DataTable();
        DataTable dtRBorrD = new DataTable();

        DataTable Alltb1 = new DataTable();
        DataTable Alltb2 = new DataTable();
        DataTable tbtemp = new DataTable();
        DataTable total = new DataTable();

        public FrmBorrowNew_Info()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            BoDate.SetDateLength();
            BoDate1.SetDateLength();

            if (Common.User_DateTime == 1)
            {
                BoDate.Text = Date.GetDateTime(1, false).Remove(5) + "01";
                BoDate1.Text = Date.GetDateTime(1, false);
            }
            else
            {
                BoDate.Text = Date.GetDateTime(2, false).Remove(6) + "01";
                BoDate1.Text = Date.GetDateTime(2, false);
            }

            BoDate.Tag = "日期";
            FaNo.Tag = "廠商編號";
            EmNo.Tag = "人員編號";
            StNo.Tag = "倉庫編號";
            KiNo.Tag = "類別編號";
            ItNo.Tag = "產品編號";
        }

        private void FrmBorrowNew_Info_Load(object sender, EventArgs e)
        {
            dtBorrD.Clear();
            dtRBorrD.Clear();
        }

        void GetData()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    if (checkBoxT1.Checked)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "Select 序號='',單據日期='',單據='借入',TotQty=(ISNULL(BorrD.Qty,0)*ISNULL(BorrD.ItPkgQty,0)),Faname1,Emname,KiNo,BorrD.* from BorrD Left JOIN Borr on BorrD.BoNo = Borr.BoNo Left JOIN Item on BorrD.ItNo = Item.ItNo Where 0=0 ";
                        if (BoDate.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.BoDate >= (@Bodate)";
                        if (BoDate1.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.BoDate <= (@Bodate1)";
                        if (FaNo.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.FaNo >= (@FaNo)";
                        if (FaNo1.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.FaNo <= (@FaNo1)";
                        if (EmNo.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.EmNo >= (@EmNo)";
                        if (EmNo1.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.EmNo <= (@EmNo1)";
                        if (StNo.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.StNo >= (@StNo)";
                        if (StNo1.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.StNo <= (@StNo1)";
                        if (KiNo.TrimTextLenth() > 0) cmd.CommandText += " And KiNo >= (@KiNo)";
                        if (KiNo1.TrimTextLenth() > 0) cmd.CommandText += " And KiNo <= (@KiNo1)";
                        if (ItNo.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.ItNo >= (@ItNo)";
                        if (ItNo1.TrimTextLenth() > 0) cmd.CommandText += " And BorrD.ItNo <= (@ItNo1)";

                        if (BoDate.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("BoDate", Date.ToTWDate(BoDate.Text.Trim()));
                        if (BoDate1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("BoDate1", Date.ToTWDate(BoDate1.Text.Trim()));
                        if (FaNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
                        if (FaNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("FaNo1", FaNo1.Text.Trim());
                        if (EmNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("EmNo", EmNo.Text.Trim());
                        if (EmNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("EmNo1", EmNo1.Text.Trim());
                        if (StNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                        if (StNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                        if (KiNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                        if (KiNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());
                        if (ItNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("ItNo", ItNo.Text.Trim());
                        if (ItNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("ItNo1", ItNo1.Text.Trim());

                        da.Fill(dtBorrD);
                    }


                    if (checkBoxT2.Checked)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "Select 序號='',單據日期='',單據='還出',TotQty=(-1)*(ISNULL(RBorrD.Qty,0)*ISNULL(RBorrD.ItPkgQty,0)),Faname1,Emname,KiNo,RBorrD.* from RBorrD Left JOIN RBorr ON RBorrD.BoNo = RBorr.BoNo Left JOIN Item on RBorrD.ItNo = Item.ItNo Where 0=0 ";
                        if (BoDate.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.BoDate >= (@Bodate)";
                        if (BoDate1.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.BoDate <= (@Bodate1)";
                        if (FaNo.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.FaNo >= (@FaNo)";
                        if (FaNo1.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.FaNo <= (@FaNo1)";
                        if (EmNo.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.EmNo >= (@EmNo)";
                        if (EmNo1.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.EmNo <= (@EmNo1)";
                        if (StNo.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.StNo >= (@StNo)";
                        if (StNo1.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.StNo <= (@StNo1)";
                        if (KiNo.TrimTextLenth() > 0) cmd.CommandText += " And KiNo >= (@KiNo)";
                        if (KiNo1.TrimTextLenth() > 0) cmd.CommandText += " And KiNo <= (@KiNo1)";
                        if (ItNo.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.ItNo >= (@ItNo)";
                        if (ItNo1.TrimTextLenth() > 0) cmd.CommandText += " And RBorrD.ItNo <= (@ItNo1)";

                        if (BoDate.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("BoDate", Date.ToTWDate(BoDate.Text.Trim()));
                        if (BoDate1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("BoDate1", Date.ToTWDate(BoDate1.Text.Trim()));
                        if (FaNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("FaNo", FaNo.Text.Trim());
                        if (FaNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("FaNo1", FaNo1.Text.Trim());
                        if (EmNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("EmNo", EmNo.Text.Trim());
                        if (EmNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("EmNo1", EmNo1.Text.Trim());
                        if (StNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("StNo", StNo.Text.Trim());
                        if (StNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("StNo1", StNo1.Text.Trim());
                        if (KiNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("KiNo", KiNo.Text.Trim());
                        if (KiNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("KiNo1", KiNo1.Text.Trim());
                        if (ItNo.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("ItNo", ItNo.Text.Trim());
                        if (ItNo1.TrimTextLenth() > 0) cmd.Parameters.AddWithValue("ItNo1", ItNo1.Text.Trim());

                        da.Fill(dtRBorrD);

                        for (int i = 0; i < dtRBorrD.Rows.Count; i++)
                        {
                            dtRBorrD.Rows[i]["qty"] = (-1) * dtRBorrD.Rows[i]["qty"].ToDecimal();
                            dtRBorrD.Rows[i]["mny"] = (-1) * dtRBorrD.Rows[i]["mny"].ToDecimal();
                            dtRBorrD.Rows[i]["mnyb"] = (-1) * dtRBorrD.Rows[i]["mnyb"].ToDecimal();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            dtBorrD.Clear();
            dtRBorrD.Clear();

            var list = panelT1.Controls.OfType<TextBox>().ToList();
            foreach (var t in list)
            {
                if (t.Name.EndsWith("1")) continue;
                if (t.TrimTextLenth() == 0) continue;

                var t1 = list.Find(tb => tb.Name == t.Name + "1");
                if (t1.TrimTextLenth() == 0) continue;
                if (string.CompareOrdinal(t.Text.Trim(), t1.Text.Trim()) > 0)
                {
                    MessageBox.Show("起迄" + t.Tag + "不可大於終止" + t.Tag + "", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            GetData();
            if (dtBorrD.Rows.Count == 0 && dtRBorrD.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //using (FrmBorrowNew_Infob frm = new FrmBorrowNew_Infob())
            //{
            //    frm.DateRange = BoDate.Text + " ～ " + BoDate1.Text;
            //    frm.dtD.Clear();
            //    frm.dtD = dtBorrD.Clone();
            //    frm.dtD.Merge(dtBorrD);
            //    frm.dtD.Merge(dtRBorrD);
            //    var date = Common.User_DateTime == 1 ? "bodate" : "bodate1";
            //    for (int i = 0; i < frm.dtD.Rows.Count; i++)
            //    {
            //        frm.dtD.Rows[i]["序號"] = (i + 1).ToString();
            //        frm.dtD.Rows[i]["單據日期"] = Date.AddLine(frm.dtD.Rows[i][date].ToString().Trim());
            //    }
            //    frm.ShowDialog();
            //}
            this.OpemInfoFrom<FrmBorrowNew_Infob>(() =>
                            {
                                FrmBorrowNew_Infob frm = new FrmBorrowNew_Infob();
                                frm.DateRange = BoDate.Text + " ～ " + BoDate1.Text;
                                frm.dtD.Clear();
                                frm.dtD = dtBorrD.Clone();
                                frm.dtD.Merge(dtBorrD);
                                frm.dtD.Merge(dtRBorrD);
                                var date = Common.User_DateTime == 1 ? "bodate" : "bodate1";
                                for (int i = 0; i < frm.dtD.Rows.Count; i++)
                                {
                                    frm.dtD.Rows[i]["序號"] = (i + 1).ToString();
                                    frm.dtD.Rows[i]["單據日期"] = Date.AddLine(frm.dtD.Rows[i][date].ToString().Trim());
                                }
                                return frm;
                            });
        }

        private void btnTotal_Click(object sender, EventArgs e)
        {
            dtBorrD.Clear();
            dtRBorrD.Clear();
            total.Clear();

            var list = panelT1.Controls.OfType<TextBox>().ToList();
            foreach (var t in list)
            {
                if (t.Name.EndsWith("1")) continue;
                if (t.TrimTextLenth() == 0) continue;

                var t1 = list.Find(tb => tb.Name == t.Name + "1");
                if (t1.TrimTextLenth() == 0) continue;
                if (string.CompareOrdinal(t.Text.Trim(), t1.Text.Trim()) > 0)
                {
                    MessageBox.Show("起迄" + t.Tag + "不可大於終止" + t.Tag + "", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            GetData();

            if (checkBoxT1.Checked)
            {
                Alltb1 = dtBorrD.Clone();
                decimal qty;
                bool Er = false;
                for (int i = 0; i < dtBorrD.Rows.Count; i++)
                {
                    DataRow da = Alltb1.NewRow();
                    qty = 0;

                    //統計數量
                    for (int j = 0; j < dtBorrD.Rows.Count; j++)
                    {
                        if (string.Equals(dtBorrD.Rows[i]["fano"].ToString().Trim(), dtBorrD.Rows[j]["fano"].ToString().Trim()) && string.Equals(dtBorrD.Rows[i]["itno"].ToString().Trim(), dtBorrD.Rows[j]["itno"].ToString().Trim()))
                        {
                            for (int x = 0; x < i && Er == false; x++)
                            {
                                if (string.Equals(dtBorrD.Rows[x]["fano"].ToString().Trim(), dtBorrD.Rows[i]["fano"].ToString().Trim()) && string.Equals(dtBorrD.Rows[x]["itno"].ToString().Trim(), dtBorrD.Rows[i]["itno"].ToString().Trim()))
                                {
                                    Er = true;
                                    break;
                                }
                            }
                            qty += dtBorrD.Rows[j]["TotQty"].ToDecimal();
                        }
                    }
                    if (!Er)
                    {
                        var itunit = Common.load("Check", "item", "itno", dtBorrD.Rows[i]["itno"].ToString().Trim());

                        da["itno"] = dtBorrD.Rows[i]["itno"].ToString();
                        da["itname"] = dtBorrD.Rows[i]["itname"].ToString();
                        da["fano"] = dtBorrD.Rows[i]["fano"].ToString();
                        da["faname1"] = dtBorrD.Rows[i]["faname1"].ToString();
                        da["itunit"] = itunit["itunit"].ToString();


                        da["TotQty"] = qty;

                        Alltb1.Rows.Add(da);
                        Alltb1.AcceptChanges();

                    }

                    Er = false;

                }
                if (total.Rows.Count == 0)
                    total = Alltb1.Copy();
                else
                    total.Merge(Alltb1);
            }
            if (checkBoxT2.Checked)
            {
                Alltb2 = dtRBorrD.Clone();
                decimal qty;
                bool Er = false;
                for (int i = 0; i < dtRBorrD.Rows.Count; i++)
                {
                    DataRow da = Alltb2.NewRow();
                    qty = 0;

                    //統計數量
                    for (int j = 0; j < dtRBorrD.Rows.Count; j++)
                    {
                        if (string.Equals(dtRBorrD.Rows[i]["fano"].ToString().Trim(), dtRBorrD.Rows[j]["fano"].ToString().Trim()) && string.Equals(dtRBorrD.Rows[i]["itno"].ToString().Trim(), dtRBorrD.Rows[j]["itno"].ToString().Trim()))
                        {
                            for (int x = 0; x < i && Er == false; x++)
                            {
                                if (string.Equals(dtRBorrD.Rows[x]["fano"].ToString().Trim(), dtRBorrD.Rows[i]["fano"].ToString().Trim()) && string.Equals(dtRBorrD.Rows[x]["itno"].ToString().Trim(), dtRBorrD.Rows[i]["itno"].ToString().Trim()))
                                {
                                    Er = true;
                                    break;
                                }
                            }
                            qty += dtRBorrD.Rows[j]["TotQty"].ToDecimal();
                        }
                    }
                    if (!Er)
                    {
                        var itunit = Common.load("Check", "item", "itno", dtRBorrD.Rows[i]["itno"].ToString().Trim());

                        da["itno"] = dtRBorrD.Rows[i]["itno"].ToString();
                        da["itname"] = dtRBorrD.Rows[i]["itname"].ToString();
                        da["fano"] = dtRBorrD.Rows[i]["fano"].ToString();
                        da["faname1"] = dtRBorrD.Rows[i]["faname1"].ToString();
                        da["itunit"] = itunit["itunit"].ToString();


                        da["TotQty"] = qty;

                        Alltb2.Rows.Add(da);
                        Alltb2.AcceptChanges();

                    }

                    Er = false;

                }
                if (total.Rows.Count == 0)
                    total = Alltb2.Copy();
                else
                    total.Merge(Alltb2);
            }
            if (checkBoxT1.Checked && checkBoxT2.Checked)
            {
                tbtemp = total.Clone();
                decimal qty;
                bool Er = false;
                for (int i = 0; i < total.Rows.Count; i++)
                {
                    DataRow da = tbtemp.NewRow();
                    qty = 0;

                    //統計數量
                    for (int j = 0; j < total.Rows.Count; j++)
                    {
                        if (string.Equals(total.Rows[i]["fano"].ToString().Trim(), total.Rows[j]["fano"].ToString().Trim()) && string.Equals(total.Rows[i]["itno"].ToString().Trim(), total.Rows[j]["itno"].ToString().Trim()))
                        {
                            for (int x = 0; x < i && Er == false; x++)
                            {
                                if (string.Equals(total.Rows[x]["fano"].ToString().Trim(), total.Rows[i]["fano"].ToString().Trim()) && string.Equals(total.Rows[x]["itno"].ToString().Trim(), total.Rows[i]["itno"].ToString().Trim()))
                                {
                                    Er = true;
                                    break;
                                }
                            }
                            qty += total.Rows[j]["TotQty"].ToDecimal();
                        }
                    }
                    if (!Er)
                    {
                        var itunit = Common.load("Check", "item", "itno", total.Rows[i]["itno"].ToString().Trim());

                        da["itno"] = total.Rows[i]["itno"].ToString();
                        da["itname"] = total.Rows[i]["itname"].ToString();
                        da["fano"] = total.Rows[i]["fano"].ToString();
                        da["faname1"] = total.Rows[i]["faname1"].ToString();
                        da["itunit"] = itunit["itunit"].ToString();


                        da["TotQty"] = qty;

                        tbtemp.Rows.Add(da);
                        tbtemp.AcceptChanges();

                    }

                    Er = false;

                }

                total = tbtemp.Copy();

            }
            if (total.Rows.Count == 0)
            {
                MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //using (FrmBorrowNew_InfoT frm = new FrmBorrowNew_InfoT())
            //{
            //    frm.table = total.AsEnumerable().OrderBy(r => r["fano"].ToString().Trim()).ThenBy(r => r["itno"].ToString().Trim()).CopyToDataTable();
            //    frm.DateRange = "日期區間:" + Date.AddLine(BoDate.Text.ToString()) + "～" + Date.AddLine(BoDate1.Text.ToString());
            //    frm.ShowDialog();
            //}
            this.OpemInfoFrom<FrmBorrowNew_InfoT>(() =>
                            {
                                FrmBorrowNew_InfoT frm = new FrmBorrowNew_InfoT();
                                frm.table = total.AsEnumerable().OrderBy(r => r["fano"].ToString().Trim()).ThenBy(r => r["itno"].ToString().Trim()).CopyToDataTable();
                                frm.DateRange = "日期區間:" + Date.AddLine(BoDate.Text.ToString()) + "～" + Date.AddLine(BoDate1.Text.ToString());
                                return frm;
                            });
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FaNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Empl>(sender);
        }

        private void KiNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Kind>(sender);
        }

        private void BoDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.DateValidate(sender, e);
        }
    }
}
