using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.subMenuFm_2
{
    public partial class FrmIvToThis : Formbase
    {
        JBS.JS.xEvents xe;

        public DataTable dtM;
        public DataTable dtD;
        public string invno = "";

        public FrmIvToThis()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();

            btnInput.Font = lblT1.Font;
        }

        private void FrmIvToThis_Load(object sender, EventArgs e)
        {
            dtM.Clear();
            dtD.Clear();

            IvDate.Text = Date.GetDateTime(Common.User_DateTime);
            IvDate.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
        

            radioT1.Checked = true;

            StNo.Text = Common.User_StkNo;
            pVar.StkValidate(StNo.Text, StNo, StName);
        }

        private void IvDate_Validating(object sender, CancelEventArgs e)
        {
            if (IvDate.Text.Trim().Length == 0)
            {
                IvDate.Clear();
                e.Cancel = true;
                MessageBox.Show("盤點日期不可為空白！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IvDate.IsDateTime())
            {
                e.Cancel = true;
                MessageBox.Show("輸入日期格式錯誤", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void StNo_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Stkroom>(sender, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }

        private void StNo_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            if (StNo.Text.Trim().Length == 0)
            {
                e.Cancel = true;
                StNo.Text = StName.Text = "";
                MessageBox.Show("倉庫編號不可為空白", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            xe.ValidateOpen<JBS.JS.Stkroom>(sender, e, row =>
            {
                StNo.Text = row["StNo"].ToString().Trim();
                StName.Text = row["StName"].ToString().Trim();
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Multiselect = false;
                op.Filter = "txt files (*.txt)|*.txt";
                op.ShowDialog();
                IvPath.Text = op.FileName;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            var path = IvPath.Text.Trim();
            if (path.Length == 0)
            {
                MessageBox.Show("請先選擇匯入檔案來源路徑！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(path))
            {
                MessageBox.Show("匯入檔案不存在！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DataTable temp = new DataTable();
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    { 
                        string str;
                        str = ""
+ " select stkroom.stname,stkroom.sttrait,item.itno,item.itname,item.itunit,item.ittrait,ISNULL(S.stno,(@stno))stno,ISNULL(S.itqtyf,0)itqtyf,ISNULL(S.itqty,0)itqty "
+ " from item "
+ " left join stock AS S on item.itno = S.itno And (S.stno is null or S.stno=(@stno)) "
+ " left join stkroom on (stkroom.stno=(@stno)) ";
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("stno", StNo.Text.Trim());
                            cmd.CommandText = str;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(temp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }

                using (StreamReader rd = new StreamReader(path))
                {
                    decimal trait = 0, index = 0;
                    string line, itno;
                    var collection = temp.AsEnumerable().ToList();
                    while ((line = rd.ReadLine()) != null)
                    {
                        DataRow rw = dtD.NewRow();
                        rw["IvDate"] = Date.ToTWDate(IvDate.Text.Trim());
                        rw["IvDate1"] = Date.ToUSDate(IvDate.Text.Trim());
                        rw["stno"] = StNo.Text.Trim();
                        rw["stname"] = StName.Text.Trim();

                        itno = line.takeString(20).Trim();
                        rw["itno"] = itno;
                        rw["qty"] = line.skipString(20).Trim().ToDecimal();

                        var p = collection.Find(r => r["itno"].ToString().Trim() == itno);
                        if (p != null)
                        {
                            rw["itname"] = p["itname"].ToString().Trim();
                            rw["stkqty"] = p["itqty"].ToDecimal();
                            rw["qty"] = line.skipString(20).Trim().ToDecimal();
                            rw["invqty"] = rw["qty"].ToDecimal() - rw["stkqty"].ToDecimal();
                            rw["itunit"] = p["itunit"].ToString().Trim();
                            rw["itpkgqty"] = 1;
                            trait = p["ittrait"].ToDecimal();
                            if (trait == 1) continue;
                            else
                            {
                                rw["ittrait"] = trait;
                                if (trait == 2) rw["產品組成"] = "組裝品";
                                else if (trait == 3) rw["產品組成"] = "單一商品";
                                rw["序號"] = (++index).ToString();
                                dtD.Rows.Add(rw);
                            }
                        }
                        else { }
                    }
                    dtD.EndInit();
                }

                var machine = "";
                 

                SqlTransaction trans = null;
                DataTable t;
                string ip = "";
                string ivno = "";
                string sql = "";

                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        sql = "select ip from stkroom where stno=@stno";
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("stno", Common.User_StkNo);
                            cmd.CommandText = sql;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                t = new DataTable();
                                da.Fill(t);
                                if (t.Rows.Count > 0) ip = t.Rows[0]["ip"].ToString().Trim();
                            }
                        }

                        var date = "";
                        if (Common.Sys_NoAdd >= 3)
                        {
                            date = Date.ToUSDate(IvDate.Text.Trim());
                        }
                        else
                        {
                            date = Date.ToTWDate(IvDate.Text.Trim());
                        }

                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("ivno", ip + date);
                            cmd.CommandText = "select ivno from iv where ivno like @ivno + '%' order by ivno desc";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                t = new DataTable();
                                if (reader.HasRows) t.Load(reader);
                            }
                        }
                        decimal d = 1;
                        var collection = t.AsEnumerable();
                        if (Common.Sys_NoAdd == 1)
                        {
                            ivno = ip + date + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 2)
                        {
                            ivno = ip + date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                        }
                        else if (Common.Sys_NoAdd == 3)
                        {
                            ivno = ip + date + (d.ToString().PadLeft(4, '0'));
                        }
                        else if (Common.Sys_NoAdd == 4)
                        {
                            ivno = ip + date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                        }

                        while (collection.Count(r => r["ivno"].ToString().Trim() == ivno) > 0)
                        {
                            d++;
                            if (Common.Sys_NoAdd == 1)
                            {
                                ivno = ip + date + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 2)
                            {
                                ivno = ip + date.takeString(5) + (d.ToString().PadLeft(6, '0'));
                            }
                            else if (Common.Sys_NoAdd == 3)
                            {
                                ivno = ip + date + (d.ToString().PadLeft(4, '0'));
                            }
                            else if (Common.Sys_NoAdd == 4)
                            {
                                ivno = ip + date.takeString(6) + (d.ToString().PadLeft(6, '0'));
                            }
                        }

                        //主檔
                        dtM.Clear();
                        var row = dtM.NewRow();
                        row["ivno"] = ivno.Trim();
                        row["ivdate"] = Date.ToTWDate(IvDate.Text);
                        row["ivdate1"] = Date.ToUSDate(IvDate.Text);
                        row["stno"] = StNo.Text.Trim();
                        row["stname"] = StName.Text.Trim();
                        row["sum"] = 0;
                        row["bracket"] = "盤點";
                        row["appscno"] = Common.User_Name;
                        row["appdate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                        row["edtscno"] = Common.User_Name;
                        row["edtdate"] = Date.GetDateTime(2, false) + " " + DateTime.Now.ToString("HH:mm:ss");
                        row["IsTrans"] = machine;
                        dtM.Rows.Add(row);

                        trans = cn.BeginTransaction();
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = "select * from iv where 1=0";
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                                {
                                    da.InsertCommand = builder.GetInsertCommand();
                                    da.InsertCommand.Transaction = trans;
                                    da.Update(dtM);
                                }
                            }
                        }
                        //明細
                        for (int i = 0; i < dtD.Rows.Count; i++)
                        {
                            dtD.Rows[i]["ivno"] = ivno.Trim();
                            dtD.Rows[i]["ivdate"] = Date.ToTWDate(IvDate.Text);
                            dtD.Rows[i]["ivdate1"] = Date.ToUSDate(IvDate.Text);
                            dtD.Rows[i]["bracket"] = "盤點";
                            dtD.Rows[i]["IsTrans"] = machine;
                            dtD.Rows[i].EndEdit();
                            dtD.Rows[i].AcceptChanges();
                            dtD.Rows[i].SetAdded();
                        }
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = "select * from IvD where 1=0";
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
                                {
                                    da.InsertCommand = builder.GetInsertCommand();
                                    da.InsertCommand.Transaction = trans;
                                    da.Update(dtD);
                                }
                            }
                        }
                        trans.Commit();
                    }
                    invno = ivno.Trim();
                    MessageBox.Show("匯入完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
