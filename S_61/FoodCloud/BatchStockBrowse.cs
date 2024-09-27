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

namespace S_61.FoodCloud
{
    public partial class BatchStockBrowse : Formbase
    {
        JBS.JS.xEvents xe;
        decimal batchcount = 0;
        public BatchStockBrowse()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
        }

        private void BatchStockBrowse_Load(object sender, EventArgs e)
        {
            Loadbatchinformation();
            if (Common.User_DateTime == 1)
            {
                Manufacturingdate.MaxLength = 7;
                Manufacturingdate1.MaxLength = 7;
                ExpiryDate.MaxLength = 7;
                ExpiryDate1.MaxLength = 7;
            }
            if (Common.User_DateTime == 2)
            {
                Manufacturingdate.MaxLength = 8;
                Manufacturingdate1.MaxLength = 8;
                ExpiryDate.MaxLength = 8;
                ExpiryDate1.MaxLength = 8;
            }
            ItNo.Focus();
        }

        void Loadbatchinformation()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = " Select COUNT(*) from BatchInformation";
                    batchcount = cmd.ExecuteScalar().ToDecimal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (radio1.Checked) 
            {
                Report1();
                return;
            }
            if (radio2.Checked) 
            {
                Report2();
                return;
            }
        }

        void Report1() 
        {
            if (ItNo.Text.BigThen(ItNo1.Text))
            {
                MessageBox.Show("起始產品編號不可大於終止產品編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }
            if (fano.Text.BigThen(fano1.Text))
            {
                MessageBox.Show("起始廠商編號不可大於終止產品編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fano.Focus();
                return;
            }

            if (Batchno.Text.BigThen(Batchno1.Text))
            {
                MessageBox.Show("起始批次編號不可大於終止批次編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Batchno.Focus();
                return;
            }
            if (Manufacturingdate.Text.BigThen(Manufacturingdate1.Text))
            {
                MessageBox.Show("起始製造日期不可大於終止製造日期，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Manufacturingdate.Focus();
                return;
            }
            if (ExpiryDate.Text.BigThen(ExpiryDate1.Text))
            {
                MessageBox.Show("起始有效日期不可大於終止有效日期，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ExpiryDate.Focus();
                return;
            }
            if (StNo.Text.BigThen(StNo1.Text))
            {
                MessageBox.Show("起始倉庫編號不可大於終止倉庫編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNo.Focus();
                return;
            }

            try
            {
                btnBrow.Enabled = false;
                DataTable temp = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = ""
                                      + " select ROW_NUMBER() OVER(ORDER BY BatchInformation.Bno) AS 序號,BatchStock.Stno,stname = (select top 1 stname from stkroom where stno = BatchStock.stno ),BatchStock.itno,BatchStock.StnoQty,BatchInformation.Batchno,item.itname , item.itunit,item.itpkgqty,item.itunitp,item.itnoudf,包裝整數=0.0,包裝餘數=0.0,倉庫類別='',stkroom.sttrait,BatchInformation.fano,fact.faname1,BatchInformation.Date,BatchInformation.Date1 "
                                      + " from BatchInformation "
                                      + " left join BatchStock on BatchStock.Bno = BatchInformation.Bno"
                                      + " left join item on item.itno = BatchInformation.itno"
                                      + " left join fact on fact.fano = BatchInformation.fano"
                                      + " left join stkroom on BatchStock.stno = stkroom.stno "
                                      + " where 0=0 ";

                    if (ch1.Checked == false) cmd.CommandText += " And stkroom.sttrait != 1";
                    if (ch2.Checked == false) cmd.CommandText += " And stkroom.sttrait != 2";
                    if (ch3.Checked == false) cmd.CommandText += " And stkroom.sttrait != 3";
                    if (ch4.Checked == false) cmd.CommandText += " And stkroom.sttrait != 4";
                    if (ItNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ItNo", ItNo.Text.Trim());
                        cmd.CommandText += " And Item.ItNo >= (@ItNo)";
                    }
                    if (ItNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ItNo1", ItNo1.Text.Trim());
                        cmd.CommandText += " And Item.ItNo <= (@ItNo1)";
                    }
                    if (fano.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@fano", fano.Text.Trim());
                        cmd.CommandText += " And BatchInformation.fano >= (@fano)";
                    }
                    if (fano1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@fano1", fano1.Text.Trim());
                        cmd.CommandText += " And BatchInformation.fano <= (@fano1)";
                    }
                    if (Batchno.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@Batchno", Batchno.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Batchno >= (@Batchno)";
                    }
                    if (Batchno1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@Batchno1", Batchno1.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Batchno <= (@Batchno1)";
                    }

                    if (Manufacturingdate.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@Manufacturingdate", Manufacturingdate.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Date >= (@Manufacturingdate)";
                    }
                    if (Manufacturingdate1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@Manufacturingdate1", Manufacturingdate1.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Date <= (@Manufacturingdate1)";
                    }

                    if (ExpiryDate.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ExpiryDate", ExpiryDate.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Date1 >= (@ExpiryDate)";
                    }
                    if (ExpiryDate1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ExpiryDate1", ExpiryDate1.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Date1 <= (@ExpiryDate1)";
                    }

                    if (StNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());
                        cmd.CommandText += " And BatchStock.StNo >= (@StNo)";
                    }
                    if (StNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@StNo1", StNo1.Text.Trim());
                        cmd.CommandText += " And BatchStock.StNo <= (@StNo1)";
                    }
                    
                    if (radio9.Checked)
                    {
                        cmd.CommandText += " And BatchStock.StnoQty != 0";
                    }
                    
                    

                    da.Fill(temp);  
                }
                if (temp.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    var itpkgqty = temp.Rows[i]["itpkgqty"].ToDecimal();
                    var sttrait = temp.Rows[i]["sttrait"].ToDecimal();
                    if (itpkgqty > 0)
                    {
                        temp.Rows[i]["包裝整數"] = (int)(temp.Rows[i]["StnoQty"].ToDecimal() / itpkgqty);
                        temp.Rows[i]["包裝餘數"] = (int)(temp.Rows[i]["StnoQty"].ToDecimal() % itpkgqty);
                    }
                    if (sttrait == 1) temp.Rows[i]["倉庫類別"] = "庫存倉";
                    else if (sttrait == 2) temp.Rows[i]["倉庫類別"] = "借出倉";
                    else if (sttrait == 3) temp.Rows[i]["倉庫類別"] = "加工倉";
                    else if (sttrait == 4) temp.Rows[i]["倉庫類別"] = "借入倉";
                }

                this.OpemInfoFrom<FoodCloud.BatchStockBrowseDetail>(() =>
                {
                    FoodCloud.BatchStockBrowseDetail frm = new FoodCloud.BatchStockBrowseDetail();
                    frm.dt = temp.Copy();
                    return frm;
                });
            }
            catch (Exception)
            {
               throw;
            }
            finally
            {
                btnBrow.Enabled = true;
            }
        }

        void Report2() 
        {
            if (ItNo.Text.BigThen(ItNo1.Text))
            {
                MessageBox.Show("起始產品編號不可大於終止產品編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ItNo.Focus();
                return;
            }
            if (Batchno.Text.BigThen(Batchno1.Text))
            {
                MessageBox.Show("起始批次編號不可大於終止批次編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Batchno.Focus();
                return;
            }
            if (StNo.Text.BigThen(StNo1.Text))
            {
                MessageBox.Show("起始倉庫編號不可大於終止倉庫編號，請確定！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StNo.Focus();
                return;
            }

            try
            {
                btnBrow.Enabled = false;
                DataTable temp = new DataTable();
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandText = ""
                    + " select ROW_NUMBER() OVER(ORDER BY BatchInformation.Bno) AS 序號,庫存倉=0.0,借出倉=0.0,加工倉=0.0,借入倉=0.0,BatchStock.stno,stkroom.stname,stkroom.sttrait,BatchStock.itno,item.itname,item.itunit,ISNULL(StnoQty,0)StnoQty,BatchInformation.Batchno,BatchInformation.fano,fact.faname1,BatchInformation.Date,BatchInformation.Date1 "
                    + " from BatchInformation"
                    + " left join BatchStock on BatchStock.Bno = BatchInformation.Bno"
                    + " left join item on BatchStock.itno = item.itno"
                    + " left join fact on fact.fano = BatchInformation.fano"
                    + " left join stkroom on BatchStock.stno = stkroom.stno"
                    + " where 0=0";

                    if (ItNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ItNo", ItNo.Text.Trim());
                        cmd.CommandText += " And Item.ItNo >= (@ItNo)";
                    }
                    if (ItNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ItNo1", ItNo1.Text.Trim());
                        cmd.CommandText += " And Item.ItNo <= (@ItNo1)";
                    }

                    if (fano.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@fano", fano.Text.Trim());
                        cmd.CommandText += " And BatchInformation.fano >= (@fano)";
                    }
                    if (fano1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@fano1", fano1.Text.Trim());
                        cmd.CommandText += " And BatchInformation.fano <= (@fano1)";
                    }

                    if (Batchno.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@Batchno", Batchno.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Batchno >= (@Batchno)";
                    }
                    if (Batchno1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@Batchno1", Batchno1.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Batchno <= (@Batchno1)";
                    }

                    if (Manufacturingdate.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@Manufacturingdate", Manufacturingdate.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Date >= (@Manufacturingdate)";
                    }
                    if (Manufacturingdate1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@Manufacturingdate1", Manufacturingdate1.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Date <= (@Manufacturingdate1)";
                    }

                    if (ExpiryDate.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ExpiryDate", ExpiryDate.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Date1 >= (@ExpiryDate)";
                    }
                    if (ExpiryDate1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@ExpiryDate1", ExpiryDate1.Text.Trim());
                        cmd.CommandText += " And BatchInformation.Date1 <= (@ExpiryDate1)";
                    }

                    if (StNo.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@StNo", StNo.Text.Trim());
                        cmd.CommandText += " And Stock.StNo >= (@StNo)";
                    }
                    if (StNo1.TrimTextLenth() > 0)
                    {
                        cmd.Parameters.AddWithValue("@StNo1", StNo1.Text.Trim());
                        cmd.CommandText += " And Stock.StNo <= (@StNo1)";
                    }
                    
                    if (radio9.Checked)
                    {
                        cmd.CommandText += " And BatchStock.StnoQty != 0";
                    }
                    
                    da.Fill(temp);
                }

                if (temp.Rows.Count == 0)
                {
                    MessageBox.Show("查無資料！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dt = temp.AsEnumerable().GroupBy(r => r["Batchno"].ToString()).Select(g => g.First() as DataRow).CopyToDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var itno = dt.Rows[i]["Batchno"].ToString().Trim();
                    dt.Rows[i]["庫存倉"] = temp.Select("Batchno = '" + itno + "'").Where(r => r["StTrait"].ToDecimal() == 1).Sum(r => r["StnoQty"].ToDecimal());
                    dt.Rows[i]["借出倉"] = temp.Select("Batchno = '" + itno + "'").Where(r => r["StTrait"].ToDecimal() == 2).Sum(r => r["StnoQty"].ToDecimal());
                    dt.Rows[i]["加工倉"] = temp.Select("Batchno = '" + itno + "'").Where(r => r["StTrait"].ToDecimal() == 3).Sum(r => r["StnoQty"].ToDecimal());
                    dt.Rows[i]["借入倉"] = temp.Select("Batchno = '" + itno + "'").Where(r => r["StTrait"].ToDecimal() == 4).Sum(r => r["StnoQty"].ToDecimal());
                    dt.Rows[i]["StnoQty"] = temp.Select("Batchno = '" + itno + "'").Sum(r => r["StnoQty"].ToDecimal());
                }

                
                this.OpemInfoFrom<FoodCloud.BatchStockBrowseAll>(() =>
                {
                    FoodCloud.BatchStockBrowseAll frm = new FoodCloud.BatchStockBrowseAll();
                    frm.dt = dt.Copy();
                    return frm;
                });
            }
            catch (Exception)
            {  
                throw;
            }
            finally
            {
                btnBrow.Enabled = true;
            }
        }

        private void ItNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Item>(sender);
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender);
        }

        private void Batchno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.BatchInformation>(sender);
        }

        private void radio2_CheckedChanged(object sender, EventArgs e)
        {
            if (radio2.Checked == true)
            {
                groupBoxT3.Visible = false;
                lblT3.Visible = false;
                StNo.Visible = false;
                StNo1.Visible = false;
                lblT7.Visible = false;
            }
            else
            { 
                groupBoxT3.Visible = true;
                lblT3.Visible = true;
                StNo.Visible = true;
                StNo1.Visible = true;
                lblT7.Visible = true;
            }
        }

        private void fano_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Fact>(sender);
        }

        private void ItNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Item>(sender, e, row =>
            {
                (sender as TextBox).Text = row["itno"].ToString().Trim();
            }, true);
        }

        private void fano_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Fact>(sender, e, row =>
            {
                (sender as TextBox).Text = row["fano"].ToString().Trim();
            }, true);
        }

        private void Batchno_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.BatchInformation>(sender, e, row =>
            {
                (sender as TextBox).Text = row["Batchno"].ToString().Trim();
            }, true);
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                (sender as TextBox).Text = row["stno"].ToString().Trim();
            }, true);
        }

        private void Manufacturingdate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if ((sender as TextBox).Text != "")
                xe.DateValidate(sender, e);
        }

        private void ExpiryDate_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if ((sender as TextBox).Text != "")
                xe.DateValidate(sender, e);
        }

        

        

        
   
    }
}
