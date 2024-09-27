using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using S_61.Basic;
using System.Data;

namespace S_61.S0
{
    public partial class FrmMachineSet : JE.MyControl.Formbase
    {
        JBS.JS.MachineSet jMachineSet;
        string pk;
        JBS.JS.xEvents xe = new JBS.JS.xEvents();
        public FrmMachineSet()
        {
            InitializeComponent();
            this.jMachineSet = new JBS.JS.MachineSet();
        }
        private void InitializeReadData(string maid)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("maid", maid);

                cn.Open();
                cmd.CommandText = "Select top 1 * from MachineSet left join Einvsetup on MachineSet.User_Einv=Einvsetup.Einvid collate Chinese_Taiwan_Stroke_BIN where maid = @maid";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        if (reader.Read())
                        {
                            machine.Text = reader["machine"].ToString().Trim();

                            TicketPort.Text = reader["TicketPort"].ToString().Trim();
                            InvPort.Text = reader["InvPort"].ToString().Trim();
                            MoneyPort.Text = reader["MoneyPort"].ToString().Trim();
                            VideoPort.Text = reader["VideoPort"].ToString().Trim();
                            OffLinePort.Text = reader["OffLinePort"].ToString().Trim();
                            HalfPort.Text = reader["HalfPort"].ToString().Trim();
                            PaperPort.Text = reader["PaperPort"].ToString().Trim();
                            cardPort.Text = reader["cardPort"].ToString().Trim();

                            var t = reader["TicketPortBox"].ToString().Trim();
                            if (t == "1") t1.Checked = true;
                            else t2.Checked = true;

                            var i = reader["InvPortBox"].ToString().Trim();
                            if (i == "1") i1.Checked = true;
                            else i2.Checked = true;

                            var x = reader["X5No"].ToString().Trim();
                            if (x == "3") x1.Checked = true;
                            else if (x == "4") x2.Checked = true;
                            else if (x == "7") x7.Checked = true;
                            x7_Click(null, null);

                            var c = reader["InvContentKind"].ToString().Trim();
                            if (c == "1") c1.Checked = true;
                            else c2.Checked = true;

                            var tS = reader["POSLayout"].ToString().Trim();
                            if (tS == "1") radioPOS.Checked = true;
                            else radioT1.Checked = true;

                            iZZ.Text = reader["InvNoS"].ToString().Trim().takeString(2);
                            iStart.Text = reader["InvNoS"].ToString().Trim().skipString(2);

                            X3No.Text = reader["X3No"].ToString().Trim();
                            X3Name.Clear();
                            jMachineSet.Validate<JBS.JS.XX03>(X3No.Text, r => X3Name.Text = r["X3Name"].ToString().Trim());

                            InvT1.Text = reader["InvT1"].ToString();
                            InvT2.Text = reader["InvT2"].ToString();
                            InvT3.Text = reader["InvT3"].ToString();
                            InvT4.Text = reader["InvT4"].ToString();
                            InvT5.Text = reader["InvT5"].ToString();

                            InvNoWarning.Text = reader["InvNoWarning"].ToDecimal().ToString("f0");
                            User_Einv.Text = reader["User_Einv"].ToString();
                            if (x7.Checked && User_Einv.Text.Trim().Length>0)
                            {
                                iStart.Visible= iZZ.Visible = labelT16.Visible = false;
                                InvT1.Text = reader["EinvTitle"].ToString().Trim(); 
                                InvT2.Text = reader["EinvTel"].ToString().Trim(); 
                                InvT3.Text = reader["EinvUnno"].ToString().Trim(); 
                                InvT4.Text = reader["EinvAddress"].ToString().Trim();
                                InvT5.Text = reader["EinvMemo1"].ToString().Trim(); 
                            }
                        }
                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show(EX.ToString());
                        throw;
                    }
                }
            }
        }
        private void FrmMachineSet_Load(object sender, EventArgs e)
        {
            pk = jMachineSet.Top();
            this.InitializeReadData(pk);
        }
        private void btnTop_Click(object sender, EventArgs e)
        {
            pk = jMachineSet.Top();
            this.InitializeReadData(pk);
        }
        private void btnPrior_Click(object sender, EventArgs e)
        {
            pk = jMachineSet.Prior();
            this.InitializeReadData(pk);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            pk = jMachineSet.Next();
            this.InitializeReadData(pk);
        }
        private void btnBottom_Click(object sender, EventArgs e)
        {
            pk = jMachineSet.Bottom();
            this.InitializeReadData(pk);
        }


        private string GetBoxNumber(JE.MyControl.PanelNT pn)
        {
            foreach (JE.MyControl.RadioT rd in pn.Controls.OfType<JE.MyControl.RadioT>())
            {
                if (rd.Checked)
                    return rd.Name.skipString(1).Trim();
            }
            return "2";
        }
        private void Save()
        {
            if (this.MoneyPort.TrimTextLenth() > 0)
            {
                t2.Checked = true;
                i2.Checked = true;
            }

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            {

                cn.Open();
                cmd.CommandText = @"
                    Update MachineSet Set
                    machine=@machine,TicketPort=@TicketPort,TicketPortBox=@TicketPortBox
                    ,InvPort=@InvPort,InvPortBox=@InvPortBox
                    ,MoneyPort=@MoneyPort
                    ,VideoPort=@VideoPort
                    ,OffLinePort=@OffLinePort 
                    ,HalfPort=@HalfPort
                    ,PaperPort=@PaperPort
                    ,X5No=@X5No,User_Einv=@User_Einv
                    ,InvNoS=@InvNoS
                    ,InvT1=@InvT1,InvT2=@InvT2,InvT3=@InvT3,InvT4=@InvT4,InvT5=@InvT5
                    ,InvNoWarning=@InvNoWarning,InvContentKind=@InvContentKind
                    ,X3No=@X3No,POSLayout=@POSLayout ,cardPort=@cardPort 
                    Where maid = @maid";
                //if (radioPOS.Checked)
                //    cmd.Parameters.AddWithValue("POSLayout", 1);
                //else
                cmd.Parameters.AddWithValue("POSLayout", 1);//版1
                cmd.Parameters.AddWithValue("maid", pk);
                cmd.Parameters.AddWithValue("machine", machine.Text.Trim());
                cmd.Parameters.AddWithValue("TicketPort", TicketPort.Text.Trim());
                cmd.Parameters.AddWithValue("TicketPortBox", GetBoxNumber(TicketPortBox));
                cmd.Parameters.AddWithValue("InvPort", InvPort.Text.Trim());
                cmd.Parameters.AddWithValue("InvPortBox", GetBoxNumber(InvPortBox));
                cmd.Parameters.AddWithValue("MoneyPort", MoneyPort.Text.Trim());
                cmd.Parameters.AddWithValue("VideoPort", VideoPort.Text.Trim());
                cmd.Parameters.AddWithValue("OffLinePort", OffLinePort.Text.Trim());
                cmd.Parameters.AddWithValue("HalfPort", HalfPort.Text.Trim());
                cmd.Parameters.AddWithValue("PaperPort", PaperPort.Text.Trim());
                var len = x1.Checked ? 24 : 32;
                //x7.checked = 25 媒體申報
                cmd.Parameters.AddWithValue("InvT1", InvT1.Text.GetUTF8(len));
                cmd.Parameters.AddWithValue("InvT2", InvT2.Text.GetUTF8(len));
                cmd.Parameters.AddWithValue("InvT3", InvT3.Text.GetUTF8(len));
                cmd.Parameters.AddWithValue("InvT4", InvT4.Text.GetUTF8(len));
                cmd.Parameters.AddWithValue("InvT5", InvT5.Text.GetUTF8(len));
                cmd.Parameters.AddWithValue("X3No", X3No.Text.Trim());
                cmd.Parameters.AddWithValue("User_Einv", User_Einv.Text.Trim());
                cmd.Parameters.AddWithValue("InvNoWarning", InvNoWarning.Text.ToInteger());
                cmd.Parameters.AddWithValue("InvContentKind", GetBoxNumber(InvContentKindBox) == "1" ? "1" : "2");
                cmd.Parameters.AddWithValue("cardPort", cardPort.Text.Trim()); 
                var x5no = "";
                if (GetBoxNumber(X5NoBox) == "1")
                {
                    x5no = "3";
                }
                else if (GetBoxNumber(X5NoBox) == "2")
                {
                    x5no = "4";
                }
                 else if (GetBoxNumber(X5NoBox) == "7")
                {
                    x5no = "7";
                }
                cmd.Parameters.AddWithValue("X5No", x5no);
                cmd.Parameters.AddWithValue("InvNoS", iZZ.Text.Trim() + iStart.Text.Trim());

                cmd.ExecuteNonQuery();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Save();
                MessageBox.Show("儲存完成!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void xPort_DoubleClick(object sender, EventArgs e)
        {
            using (var frm = new FrmPrintb())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    (sender as JE.MyControl.TextBoxT).Text = frm.TResult;
            }
        }
        private void btnTicket_Click(object sender, EventArgs e)
        {
            this.Save();
            new JBS.JM.Machine(this.machine.Text).TryTicket();
        }
        private void btnInv_Click(object sender, EventArgs e)
        {
            this.Save();
            new JBS.JM.Machine(this.machine.Text).TryInvNo();
        }
        private void btnVideo_Click(object sender, EventArgs e)
        {
            this.Save();
            new JBS.JM.Machine(this.machine.Text).TryVideo();
        }
        private void btnMoneyBox_Click(object sender, EventArgs e)
        {
            this.Save();
            new JBS.JM.Machine(this.machine.Text).TryMoneyBox();
        }
        private void btnOffLine_Click(object sender, EventArgs e)
        {
            this.Save();
            new JBS.JM.Machine(this.machine.Text).TryOffLine();
        }
        private void X3No_DoubleClick(object sender, EventArgs e)
        {
            jMachineSet.Open<JBS.JS.XX03>(sender);
        }
        private void X3No_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;

            X3Name.Clear();
            jMachineSet.ValidateOpen<JBS.JS.XX03>(sender, e, r => X3Name.Text = r["X3Name"].ToString().Trim());
        }
        private void x1_CheckedChanged(object sender, EventArgs e)
        {
            int max = x1.Checked ? 24 : 32;

            InvT1.MaxLength = max;
            InvT2.MaxLength = max;
            InvT3.MaxLength = max;
            InvT4.MaxLength = max;
            InvT5.MaxLength = max;
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmMachineCopy())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("machine", this.machine.Text.Trim());
                    cmd.Parameters.AddWithValue("TResult", frm.TResult.Trim());
                    cmd.CommandText = @"
                    UPDATE
                        MachineSet
                    SET
                    MachineSet.[TicketPort]=B.[TicketPort],
                    MachineSet.[TicketPortBox]=B.[TicketPortBox],
                    MachineSet.[InvPort]=B.[InvPort],
                    MachineSet.[InvPortBox]=B.[InvPortBox],
                    MachineSet.[MoneyPort]=B.[MoneyPort],
                    MachineSet.[VideoPort]=B.[VideoPort],
                    MachineSet.[X5No]=B.[X5No],
                    MachineSet.[InvT1]=B.[InvT1],
                    MachineSet.[InvT2]=B.[InvT2],
                    MachineSet.[InvT3]=B.[InvT3],
                    MachineSet.[InvT4]=B.[InvT4],
                    MachineSet.[InvT5]=B.[InvT5],
                    MachineSet.[OffLinePort]=B.[OffLinePort],
                    MachineSet.[InvNoWarning]=B.[InvNoWarning],
                    MachineSet.[InvContentKind]=B.[InvContentKind],
                    MachineSet.[X3No]=B.[X3No],
                    MachineSet.[HalfPort]=B.[HalfPort],
                    MachineSet.[PaperPort]=B.[PaperPort]
                    FROM
                        (Select * from MachineSet Where machine = @TResult) B
                    Where MachineSet.machine = @machine ";

                    cmd.ExecuteNonQuery();
                }
            }
            this.InitializeReadData(this.machine.Text.Trim());
            MessageBox.Show("複製完成!!!");
        }

        private void btnPic_Click(object sender, EventArgs e)
        {
            pictureBoxT1.LoadImage();
        }

        private void btnPicClear_Click(object sender, EventArgs e)
        {
            pictureBoxT1.Clear();
        }

        private void pictureBoxT2_Click(object sender, EventArgs e)
        {
            picshow frmS2 = new picshow();
            
            frmS2.pictureBoxT1.Image = pictureBoxT2.Image;
            frmS2.Text = "版一";
            frmS2.Show();
        }

        private void pictureBoxT1_Click(object sender, EventArgs e)
        {
            picshow frmS2 = new picshow();
            
            frmS2.pictureBoxT1.Image = pictureBoxT1.Image;
            frmS2.Text = "版二";
            frmS2.Show();
        }

        private void x7_Click(object sender, EventArgs e)
        {
            if (x7.Checked)
            {
                labelT10.Visible = false;
                labelT9.Visible = false;
                labelT11.Visible = false;
                labelT12.Visible = false;
                labelT13.Visible = false;
                labelT14.Visible = false;
                InvT1.Visible = false;
                InvT2.Visible = false;
                InvT3.Visible = false;
                InvT4.Visible = false;
                InvT5.Visible = false;
                labelT16.Visible = false;
                iZZ.Visible = false;
                iStart.Visible = false;
                labelT18.Visible = false;
                InvNoWarning.Visible = false;
                labelT19.Visible = false;
                labelT25.Visible = true;
                User_Einv.Visible = true;
            }
            else
            {
                labelT10.Visible = true;
                labelT9.Visible = true;
                labelT11.Visible = true;
                labelT12.Visible = true;
                labelT13.Visible = true;
                labelT14.Visible = true;
                InvT1.Visible = true;
                InvT2.Visible = true;
                InvT3.Visible = true;
                InvT4.Visible = true;
                InvT5.Visible = true;
                labelT16.Visible = true;
                iZZ.Visible = true;
                iStart.Visible = true;
                labelT18.Visible = true;
                InvNoWarning.Visible = true;
                labelT19.Visible = true;
                labelT25.Visible = false;
                User_Einv.Visible = false;
                User_Einv.Text = "";
            }

        }

        private void User_Einv_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.EINV>(sender, reader =>
            {
                User_Einv.Text = reader["Einvid"].ToString();
            });
        }

        private void User_Einv_Validating(object sender, CancelEventArgs e)
        {
            DataTable Einvdt = new DataTable();
            if (User_Einv.Text.Trim().Length > 0)
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.CommandText = "select * from Einvsetup where Einvid='" + User_Einv.Text + "'";
                    if (cmd.ExecuteScalar().IsNullOrEmpty())
                    {
                        User_Einv_DoubleClick(sender, null);
                        e.Cancel = true;
                    }
                    else
                    {
                        Einvdt.Clear();
                        da.Fill(Einvdt);
                        da.Dispose();
                    }
                }
            }
        }

        private void btntrycard_Click(object sender, EventArgs e)
        {
            this.Save();
            new JBS.JM.Machine(this.machine.Text).Trycard();
        }

        private void InvNoS_Validating(object sender, CancelEventArgs e)
        {
            if (btnExit.Focused)
                return;
            if (iZZ.TrimTextLenth() == 0 && iStart.TrimTextLenth() == 0)
                return;
            if (iZZ.TrimTextLenth() == 0 && iStart.TrimTextLenth() > 0)
                iZZ.Focus();
            if (iZZ.TrimTextLenth() > 0 && iStart.TrimTextLenth() == 0)
                iStart.Focus();

            iZZ.Text = iZZ.Text.ToUpper();
            var inno = iZZ.Text + iStart.Text;

            if (JBS.JM.NPOS.IsInvNoFormat(inno) == "iZZ")
            {
                iZZ.Focus();
                MessageBox.Show("發票字軌輸入錯誤!");
            }
            if (JBS.JM.NPOS.IsInvNoFormat(inno) == "iStart")
            {
                iStart.Focus();
                MessageBox.Show("發票號碼輸入錯誤!");
            }
        }

    }
}
