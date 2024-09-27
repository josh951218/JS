using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.SOther
{
    public partial class FrmEmpl : Formbase
    {
        JBS.JS.xEvents xe;
        List<TextBoxbase> list;
        DataTable dt = new DataTable();
        string tempNo = string.Empty;
        string btnState = string.Empty;
        string BeforeText = string.Empty;

        public FrmEmpl()
        {
            InitializeComponent();
            this.xe = new JBS.JS.xEvents();
            this.list = this.getEnumMember();

            if (Common.Sys_StockKind == 2)
            {
                lblPass.Visible = true;
                posPass.Visible = true;
            }
        }

        private void FrmEmpl_Load(object sender, EventArgs e)
        {
            EmBirth.SetDateLength();
            Emdate.SetDateLength();
            EmInday.SetDateLength();
            EmLoutday.SetDateLength();
            EmLinday.SetDateLength();
            EmOutday.SetDateLength();
            EmHoutday.SetDateLength();
            EmHinday.SetDateLength();

            loadDB();
            writeToTxt(Common.load("Top", "empl", "emno"));
            btnAppend.Focus();
        }

        public void loadDB()
        {
            dt.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from Empl", cn))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void setEmplDate(TextBox tx, DataRow dr)
        {
            if (Common.User_DateTime == 1)
            {
                tx.Text = dr[tx.Name].ToString();
            }
            else
            {
                tx.Text = dr[tx.Name + "1"].ToString();
            }
        }

        private void writeToTxt(DataRow row)
        {
            this.tempNo = "";
            if (row != null)
            {
                setEmplDate(Emdate, row);
                setEmplDate(EmBirth, row);
                EmNo.Text = row["EmNo"].ToString();
                EmMarr.Text = row["EmMarr"].ToString();
                EmName.Text = row["EmName"].ToString();
                EmReg.Text = row["EmReg"].ToString();

                EmDeno.Text = row["EmDeno"].ToString();
                pVar.DeptValidate(EmDeno.Text, EmDeno, EmDename);
                EmIdno.Text = row["EmIdno"].ToString();
                EmX6No.Text = row["EmX6no"].ToString();
                pVar.XX06Validate(EmX6No.Text, EmX6No, EmX6Name);
                posPass.Text = row["posPass"].ToString();
                //
                //分頁一
                //
                setEmplDate(EmHinday, row);
                EmMemo1.Text = row["EmMemo1"].ToString();
                EmAtel1.Text = row["EmAtel1"].ToString();
                EmBbc.Text = row["EmBbc"].ToString();
                EmPer1.Text = row["EmPer1"].ToString();
                EmAtel2.Text = row["EmAtel2"].ToString();
                setEmplDate(EmInday, row);
                setEmplDate(EmOutday, row);
                EmAccno.Text = row["EmAccno"].ToString();
                Emworkyears.Text = row["Emworkyears"].ToString();
                EmAddr1.Text = row["EmAddr1"].ToString();
                EmR1.Text = row["EmR1"].ToString();
                EmAddr2.Text = row["EmAddr2"].ToString();
                EmR2.Text = row["EmR2"].ToString();
                EmPath.Text = "";
                EmEmail.Text = row["EmEmail"].ToString();
                EmEdu.Text = row["EmEdu"].ToString();
                EmSpec.Text = row["EmSpec"].ToString();
                EmTel.Text = row["EmTel"].ToString();
                EmTel2.Text = row["EmTel2"].ToString();
                EmSex.Text = row["EmSex"].ToString();
                //
                //分頁二
                //
                EmEngname.Text = row["EmEngname"].ToString();
                EmBlood.Text = row["EmBlood"].ToString();
                setEmplDate(EmLinday, row);
                EmLamt.Text = row["EmLamt"].ToString();
                setEmplDate(EmLoutday, row);
                EmUnde.Text = row["EmUnde"].ToString();
                setEmplDate(EmHoutday, row);
                //區域編號不是空值,帶出區域名稱
                EmHpay.Text = row["EmHpay"].ToString();
                EmSupp.Text = row["EmSupp"].ToString();
                EmIspay.Text = row["EmIspay"].ToString();
                EmUdf1.Text = row["EmUdf1"].ToString();
                EmUdf2.Text = row["EmUdf2"].ToString();
                EmAccno.Text = row["EmAccno"].ToString();
                EmLpay.Text = row["EmLpay"].ToString();
                EmHamt.Text = row["EmHamt"].ToString();

                if (row["Empath"] != DBNull.Value) pictureBoxT1.LoadImage((byte[])row["Empath"]);
            }
            else
            {
                Common.SetTextState(FormState = FormEditState.Clear, ref list);
                pictureBoxT1.Clear();
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Top", "empl", "emno"));
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            var row = Common.load("Prior", "empl", "emno", EmNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CPrior", "empl", "emno", EmNo.Text.Trim());
                MessageBox.Show("已最上一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            writeToTxt(row);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var row = Common.load("Next", "empl", "emno", EmNo.Text.Trim());
            if (row == null)
            {
                row = Common.load("CNext", "empl", "emno", EmNo.Text.Trim());
                MessageBox.Show("已至最後一筆", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            writeToTxt(row);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            writeToTxt(Common.load("Bottom", "empl", "emno"));
        }

        private void btnAppend_EnabledChanged(object sender, EventArgs e)
        {
            btnPic.Enabled = btnPicClr.Enabled = !btnAppend.Enabled;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            tempNo = EmNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            btnState = ((Button)sender).Name.Substring(3);

            pictureBoxT1.Clear();
            EmNo.Focus();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            tempNo = EmNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Duplicate, ref list);
            btnState = ((Button)sender).Name.Substring(3);

            //複製時，更新日期
            Emdate.Text = Date.GetDateTime(Common.User_DateTime);
            EmNo.Text = "";
            EmNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (EmNo.TrimTextLenth() == 0) return;
            tempNo = EmNo.Text.Trim();
            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            btnState = ((Button)sender).Name.Substring(3);

            EmNo.Focus();
            EmNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (EmNo.TrimTextLenth() == 0) return;
            btnState = ((Button)sender).Name.Substring(3);

            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@EmNo", EmNo.Text.Trim());
                    cmd.CommandText = "delete from Empl where EmNo=@EmNo COLLATE Chinese_Taiwan_Stroke_BIN";
                    cmd.ExecuteNonQuery();
                }
                btnBottom_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FrmEmplPrint frm = new FrmEmplPrint();
            frm.ShowDialog();
        }

        private void btnBrow_Click(object sender, EventArgs e)
        {
            if (EmNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new FrmEmplb())
            {
                frm.TSeekNo = EmNo.Text.Trim();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    var row = Common.load("Check", "empl", "emno", frm.TResult);
                    writeToTxt(row);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (xe.IsRegisted("empl") == false)
            {
                string msg = "目前使用版權為『教育版』，超過筆數限制無法存檔！\n";
                msg += "若要解除筆數限制，請升級為『正式版』。";
                MessageBox.Show(msg, "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (EmNo.Text == string.Empty)//儲存或修改時，EmNo不能為空值
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EmNo.Focus();
                return;
            }

            if (posPass.TrimTextLenth() > 0)
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("emno", EmNo.Text.Trim());
                    cmd.Parameters.AddWithValue("pass", posPass.Text.Trim());

                    cmd.CommandText = "Select Count(*) from empl where pospass = @pass and emno <> @emno";
                    var obj = cmd.ExecuteScalar().ToDecimal();
                    if (obj > 0)
                    {
                        MessageBox.Show("POS密碼已被使用，請重新設定!");
                        return;
                    }
                }
            }

            if (btnState == "Append")
            {
                var row = Common.load("Check", "empl", "emno", EmNo.Text.Trim());
                if (row != null)
                {
                    MessageBox.Show("此員工編號已經重複，請重新輸入！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EmNo.Focus();
                    return;
                }

                try
                {

                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@EmNo", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmReg", EmReg.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmName", EmName.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmUnde", EmUnde.Text.Trim());
                        cmd.Parameters.AddWithValue("@Emdeno", EmDeno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmBlood", EmBlood.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmIdno", EmIdno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmMarr", EmMarr.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmBirth", Date.ToTWDate(EmBirth.Text));
                        cmd.Parameters.AddWithValue("@EmBirth1", Date.ToUSDate(EmBirth.Text));
                        cmd.Parameters.AddWithValue("@EmX6no", EmX6No.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmMemo1", EmMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAtel1", EmAtel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmBbc", EmBbc.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAtel2", EmAtel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmInday", Date.ToTWDate(EmInday.Text));
                        cmd.Parameters.AddWithValue("@EmInday1", Date.ToUSDate(EmInday.Text));
                        cmd.Parameters.AddWithValue("@EmOutday", Date.ToTWDate(EmOutday.Text));
                        cmd.Parameters.AddWithValue("@EmOutday1", Date.ToUSDate(EmOutday.Text));
                        cmd.Parameters.AddWithValue("@Emworkyears", Emworkyears.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAddr1", EmAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmR1", EmR1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAddr2", EmAddr2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmR2", EmR2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmEmail", EmEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmPer1", EmPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmLoutday", Date.ToTWDate(EmLoutday.Text));
                        cmd.Parameters.AddWithValue("@EmLoutday1", Date.ToUSDate(EmLoutday.Text));
                        cmd.Parameters.AddWithValue("@EmSex", EmSex.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmEdu", EmEdu.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHpay", EmHpay.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmSpec", EmSpec.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmTel", EmTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmPath", pictureBoxT1.ImageToByte());
                        cmd.Parameters.AddWithValue("@EmEngname", EmEngname.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmLinday", Date.ToTWDate(EmLinday.Text));
                        cmd.Parameters.AddWithValue("@EmLinday1", Date.ToUSDate(EmLinday.Text));
                        cmd.Parameters.AddWithValue("@EmLamt", EmLamt.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmTel2", EmTel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHoutday", Date.ToTWDate(EmHoutday.Text));
                        cmd.Parameters.AddWithValue("@EmHoutday1", Date.ToUSDate(EmHoutday.Text));
                        cmd.Parameters.AddWithValue("@EmSupp", EmSupp.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmIspay", EmIspay.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmUdf1", EmUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmUdf2", EmUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAccno", EmAccno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHamt", EmHamt.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmLpay", EmLpay.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHinday", Date.ToTWDate(EmHinday.Text));
                        cmd.Parameters.AddWithValue("@EmHinday1", Date.ToUSDate(EmHinday.Text));
                        cmd.Parameters.AddWithValue("@Emdate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@Emdate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@posPass", posPass.Text.Trim());

                        cmd.CommandText = "INSERT INTO Empl"
                            + "(EmNo,EmReg,EmName,EmUnde"
                            + ",Emdeno,EmBlood,EmIdno,EmMarr,EmBirth,EmBirth1"
                            + ",EmX6no,EmMemo1,EmAtel1,EmBbc"
                            + ",EmAtel2,EmInday,EmInday1,EmOutday,EmOutday1,Emworkyears"
                            + ",EmAddr1,EmR1,EmAddr2,EmR2"
                            + ",EmEmail,EmPer1,EmLoutday,EmLoutday1,EmSex"
                            + ",EmEdu,EmHpay,EmSpec,EmTel"//
                            + ",EmPath,EmEngname,EmLinday,EmLinday1,EmLamt"
                            + ",EmTel2,EmHoutday,EmHoutday1,EmSupp,EmIspay"
                            + ",EmUdf1,EmUdf2,EmAccno,EmHamt"
                            + ",EmLpay,EmHinday,EmHinday1,Emdate,Emdate1"
                            + ",posPass"
                            + ") VALUES ("

                            + "(@EmNo),(@EmReg),(@EmName),(@EmUnde),(@Emdeno),(@EmBlood),(@EmIdno),(@EmMarr),(@EmBirth),(@EmBirth1)"
                            + ",(@EmX6no),(@EmMemo1),(@EmAtel1),(@EmBbc),(@EmAtel2),(@EmInday),(@EmInday1),(@EmOutday),(@EmOutday1)"
                            + ",(@Emworkyears),(@EmAddr1),(@EmR1),(@EmAddr2),(@EmR2),(@EmEmail),(@EmPer1),(@EmLoutday),(@EmLoutday1)"
                            + ",(@EmSex),(@EmEdu),(@EmHpay),(@EmSpec),(@EmTel),(@EmPath),(@EmEngname),(@EmLinday),(@EmLinday1),(@EmLamt)"
                            + ",(@EmTel2),(@EmHoutday),(@EmHoutday1),(@EmSupp),(@EmIspay),(@EmUdf1),(@EmUdf2),(@EmAccno),(@EmHamt),(@EmLpay)"
                            + ",(@EmHinday),(@EmHinday1),(@Emdate),(@Emdate1),(@posPass)"
                            + ")";


                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

                    tempNo = EmNo.Text.Trim();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);
                    FormState = FormEditState.Append;

                    pictureBoxT1.Clear();
                    EmNo.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (btnState == "Duplicate")
            {
                var row = Common.load("Check", "empl", "emno", EmNo.Text.Trim());
                if (row != null)
                {
                    MessageBox.Show("此員工編號已經重複，請重新輸入！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EmNo.Focus();
                    return;
                }

                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@EmNo", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmReg", EmReg.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmName", EmName.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmUnde", EmUnde.Text.Trim());
                        cmd.Parameters.AddWithValue("@Emdeno", EmDeno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmBlood", EmBlood.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmIdno", EmIdno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmMarr", EmMarr.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmBirth", Date.ToTWDate(EmBirth.Text));
                        cmd.Parameters.AddWithValue("@EmBirth1", Date.ToUSDate(EmBirth.Text));
                        cmd.Parameters.AddWithValue("@EmX6no", EmX6No.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmMemo1", EmMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAtel1", EmAtel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmBbc", EmBbc.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAtel2", EmAtel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmInday", Date.ToTWDate(EmInday.Text));
                        cmd.Parameters.AddWithValue("@EmInday1", Date.ToUSDate(EmInday.Text));
                        cmd.Parameters.AddWithValue("@EmOutday", Date.ToTWDate(EmOutday.Text));
                        cmd.Parameters.AddWithValue("@EmOutday1", Date.ToUSDate(EmOutday.Text));
                        cmd.Parameters.AddWithValue("@Emworkyears", Emworkyears.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAddr1", EmAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmR1", EmR1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAddr2", EmAddr2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmR2", EmR2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmEmail", EmEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmPer1", EmPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmLoutday", Date.ToTWDate(EmLoutday.Text));
                        cmd.Parameters.AddWithValue("@EmLoutday1", Date.ToUSDate(EmLoutday.Text));
                        cmd.Parameters.AddWithValue("@EmSex", EmSex.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmEdu", EmEdu.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHpay", EmHpay.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmSpec", EmSpec.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmTel", EmTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmPath", pictureBoxT1.ImageToByte());
                        cmd.Parameters.AddWithValue("@EmEngname", EmEngname.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmLinday", Date.ToTWDate(EmLinday.Text));
                        cmd.Parameters.AddWithValue("@EmLinday1", Date.ToUSDate(EmLinday.Text));
                        cmd.Parameters.AddWithValue("@EmLamt", EmLamt.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmTel2", EmTel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHoutday", Date.ToTWDate(EmHoutday.Text));
                        cmd.Parameters.AddWithValue("@EmHoutday1", Date.ToUSDate(EmHoutday.Text));
                        cmd.Parameters.AddWithValue("@EmSupp", EmSupp.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmIspay", EmIspay.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmUdf1", EmUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmUdf2", EmUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAccno", EmAccno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHamt", EmHamt.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmLpay", EmLpay.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHinday", Date.ToTWDate(EmHinday.Text));
                        cmd.Parameters.AddWithValue("@EmHinday1", Date.ToUSDate(EmHinday.Text));
                        cmd.Parameters.AddWithValue("@Emdate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@Emdate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@posPass", posPass.Text.Trim());

                        cmd.CommandText = "INSERT INTO Empl"
                            + "(EmNo,EmReg,EmName,EmUnde"
                            + ",Emdeno,EmBlood,EmIdno,EmMarr,EmBirth,EmBirth1"
                            + ",EmX6no,EmMemo1,EmAtel1,EmBbc"
                            + ",EmAtel2,EmInday,EmInday1,EmOutday,EmOutday1,Emworkyears"
                            + ",EmAddr1,EmR1,EmAddr2,EmR2"
                            + ",EmEmail,EmPer1,EmLoutday,EmLoutday1,EmSex"
                            + ",EmEdu,EmHpay,EmSpec,EmTel"
                            + ",EmPath,EmEngname,EmLinday,EmLinday1,EmLamt"
                            + ",EmTel2,EmHoutday,EmHoutday1,EmSupp,EmIspay"
                            + ",EmUdf1,EmUdf2,EmAccno,EmHamt"
                            + ",EmLpay,EmHinday,EmHinday1,Emdate,Emdate1"
                            + ",posPass"
                            + ") VALUES ("
                            + "(@EmNo),(@EmReg),(@EmName),(@EmUnde),(@Emdeno),(@EmBlood),(@EmIdno),(@EmMarr),(@EmBirth),(@EmBirth1)"
                            + ",(@EmX6no),(@EmMemo1),(@EmAtel1),(@EmBbc),(@EmAtel2),(@EmInday),(@EmInday1),(@EmOutday),(@EmOutday1)"
                            + ",(@Emworkyears),(@EmAddr1),(@EmR1),(@EmAddr2),(@EmR2),(@EmEmail),(@EmPer1),(@EmLoutday),(@EmLoutday1)"
                            + ",(@EmSex),(@EmEdu),(@EmHpay),(@EmSpec),(@EmTel),(@EmPath),(@EmEngname),(@EmLinday),(@EmLinday1),(@EmLamt)"
                            + ",(@EmTel2),(@EmHoutday),(@EmHoutday1),(@EmSupp),(@EmIspay),(@EmUdf1),(@EmUdf2),(@EmAccno),(@EmHamt),(@EmLpay)"
                            + ",(@EmHinday),(@EmHinday1),(@Emdate),(@Emdate1),(@posPass)"
                            + ")";

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

                    tempNo = EmNo.Text.Trim();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);
                    FormState = FormEditState.Append;

                    pictureBoxT1.Clear();
                    EmNo.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (btnState == "Modify")
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                    {
                        cn.Open();
                        SqlCommand cmd = cn.CreateCommand();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@EmNo", EmNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmReg", EmReg.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmName", EmName.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmUnde", EmUnde.Text.Trim());
                        cmd.Parameters.AddWithValue("@Emdeno", EmDeno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmBlood", EmBlood.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmIdno", EmIdno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmMarr", EmMarr.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmBirth", Date.ToTWDate(EmBirth.Text));
                        cmd.Parameters.AddWithValue("@EmBirth1", Date.ToUSDate(EmBirth.Text));
                        cmd.Parameters.AddWithValue("@EmX6no", EmX6No.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmMemo1", EmMemo1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAtel1", EmAtel1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmBbc", EmBbc.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAtel2", EmAtel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmInday", Date.ToTWDate(EmInday.Text));
                        cmd.Parameters.AddWithValue("@EmInday1", Date.ToUSDate(EmInday.Text));
                        cmd.Parameters.AddWithValue("@EmOutday", Date.ToTWDate(EmOutday.Text));
                        cmd.Parameters.AddWithValue("@EmOutday1", Date.ToUSDate(EmOutday.Text));
                        cmd.Parameters.AddWithValue("@Emworkyears", Emworkyears.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAddr1", EmAddr1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmR1", EmR1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAddr2", EmAddr2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmR2", EmR2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmEmail", EmEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmPer1", EmPer1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmLoutday", Date.ToTWDate(EmLoutday.Text));
                        cmd.Parameters.AddWithValue("@EmLoutday1", Date.ToUSDate(EmLoutday.Text));
                        cmd.Parameters.AddWithValue("@EmSex", EmSex.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmEdu", EmEdu.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHpay", EmHpay.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmSpec", EmSpec.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmTel", EmTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmPath", pictureBoxT1.ImageToByte());
                        cmd.Parameters.AddWithValue("@EmEngname", EmEngname.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmLinday", Date.ToTWDate(EmLinday.Text));
                        cmd.Parameters.AddWithValue("@EmLinday1", Date.ToUSDate(EmLinday.Text));
                        cmd.Parameters.AddWithValue("@EmLamt", EmLamt.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmTel2", EmTel2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHoutday", Date.ToTWDate(EmHoutday.Text));
                        cmd.Parameters.AddWithValue("@EmHoutday1", Date.ToUSDate(EmHoutday.Text));
                        cmd.Parameters.AddWithValue("@EmSupp", EmSupp.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmIspay", EmIspay.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmUdf1", EmUdf1.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmUdf2", EmUdf2.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmAccno", EmAccno.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHamt", EmHamt.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmLpay", EmLpay.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmHinday", Date.ToTWDate(EmHinday.Text));
                        cmd.Parameters.AddWithValue("@EmHinday1", Date.ToUSDate(EmHinday.Text));
                        cmd.Parameters.AddWithValue("@Emdate", Date.GetDateTime(1, false));
                        cmd.Parameters.AddWithValue("@Emdate1", Date.GetDateTime(2, false));
                        cmd.Parameters.AddWithValue("@posPass", posPass.Text.Trim());

                        cmd.CommandText = "Update Empl set "
                          + "EmName =@EmName"
                          + ",EmUnde =@EmUnde"
                          + ",EmBlood =@EmBlood"
                          + ",EmDeno =@EmDeno"
                          + ",EmBirth =@EmBirth"
                          + ",EmBirth1 =@EmBirth1"
                          + ",EmMarr =@EmMarr"
                          + ",EmIdno =@EmIdno"
                          + ",EmX6no =@EmX6no"
                          + ",EmAtel1 =@EmAtel1"
                          + ",EmBbc =@EmBbc"
                          + ",EmPer1 =@EmPer1"
                          + ",EmMemo1 =@EmMemo1"
                          + ",EmAtel2 =@EmAtel2"
                          + ",EmInday =@EmInday"
                          + ",EmInday1 =@EmInday1"
                          + ",EmOutday =@EmOutday"
                          + ",EmOutday1 =@EmOutday1"
                          + ",Emworkyears =@Emworkyears"
                          + ",EmAddr1 =@EmAddr1"
                          + ",EmR1 =@EmR1"
                          + ",EmAddr2 =@EmAddr2"
                          + ",EmR2 =@EmR2"
                          + ",EmEmail =@EmEmail"
                          + ",EmEdu =@EmEdu"
                          + ",EmHpay =@EmHpay"
                          + ",EmSpec =@EmSpec"
                          + ",EmTel =@EmTel"
                          + ",EmSex =@EmSex"
                          + ",EmPath = @Empath"
                          + ",EmTel2 =@EmTel2"
                          + ",EmEngname =@EmEngname"
                          + ",EmLinday =@EmLinday"
                          + ",EmLinday1 =@EmLinday1"
                          + ",EmLamt =@EmLamt"
                          + ",EmLoutday =@EmLoutday"
                          + ",EmLoutday1 =@EmLoutday1"
                          + ",EmHoutday =@EmHoutday"
                          + ",EmHoutday1 =@EmHoutday1"
                          + ",EmSupp =@EmSupp"
                          + ",EmIspay =@EmIspay"
                          + ",EmUdf1 =@EmUdf1"
                          + ",EmLpay =@EmLpay"
                          + ",EmHinday =@EmHinday"
                          + ",EmHinday1 =@EmHinday1"
                          + ",EmReg =@EmReg"
                          + ",EmUdf2 =@EmUdf2"
                          + ",EmHamt =@EmHamt"
                          + ",EmAccno =@EmAccno"
                          + ",posPass =@posPass"
                          + " where EmNo =@EmNo"
                          + " COLLATE Chinese_Taiwan_Stroke_BIN";

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }

                    tempNo = EmNo.Text.Trim();
                    Common.SetTextState(FormState = FormEditState.Clear, ref list);
                    FormState = FormEditState.Append;

                    pictureBoxT1.Clear();
                    EmNo.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnState = string.Empty;
            writeToTxt(Common.load("Cancel", "empl", "emno", tempNo));
            Common.SetTextState(FormState = FormEditState.None, ref list);
            btnAppend.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void EmNo_DoubleClick(object sender, EventArgs e)
        {
            if (EmNo.ReadOnly) return;
            using (var frm = new FrmEmplb())
            {
                frm.TSeekNo = EmNo.Text.Trim();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var row = Common.load("Check", "empl", "emno", frm.TResult);
                    writeToTxt(row);
                }
            }
        }

        private void Deno_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.Dept>(sender, row =>
            {
                EmDeno.Text = row["deno"].ToString().Trim();
                EmDename.Text = row["dename1"].ToString().Trim();
            });
        }

        private void EmDeno_Validating(object sender, CancelEventArgs e)
        {
            if (EmDeno.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (EmDeno.Text.Trim() == "")
            {
                EmDeno.Text = "";
                EmDename.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.Dept>(sender, e, row =>
            {
                EmDeno.Text = row["deno"].ToString().Trim();
                EmDename.Text = row["dename1"].ToString().Trim();
            });
        }


        private void X6no_DoubleClick(object sender, EventArgs e)
        {
            xe.Open<JBS.JS.XX06>(sender, row =>
            {
                EmX6No.Text = row["X6No"].ToString().Trim();
                EmX6Name.Text = row["X6Name"].ToString().Trim();
            });
        }



        //分頁一
        private void EmAddr1_Leave(object sender, EventArgs e)
        {
            if (EmAddr2.Text.Trim() == "") EmAddr2.Text = EmAddr1.Text;
        }

        private void EmAddr1_DoubleClick(object sender, EventArgs e)
        {
            if (EmAddr1.ReadOnly != true)
            {
                using (var frm = new FrmSaddr())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (sender.Equals(EmAddr1))
                        {
                            this.EmAddr1.Text = frm.TAddr;
                            if (this.EmAddr2.Text.Trim() == "")
                                this.EmAddr2.Text = frm.TAddr;

                            this.EmR1.Text = frm.TZip;
                            if (this.EmR2.Text.Trim() == "")
                                this.EmR2.Text = frm.TZip;
                        }

                        if (sender.Equals(EmAddr2))
                        {
                            this.EmAddr2.Text = frm.TAddr;
                            this.EmR2.Text = frm.TZip;
                        }
                    }
                }
            }
        }

        private void EmR1_Leave(object sender, EventArgs e)
        {
            if (EmR2.Text.Trim() == "") EmR2.Text = EmR1.Text;
        }

        private void EmSex_DoubleClick(object sender, EventArgs e)
        {
            if (btnState == "Append" || btnState == "Modify" || btnState == "Duplicate")
            {
                (sender as TextBox).Text = (sender as TextBox).Text == "男" ? "女" : "男";
            }
        }


        private void EmMarr_DoubleClick(object sender, EventArgs e)
        {
            if (btnState == "Append" || btnState == "Modify" || btnState == "Duplicate")
            {
                (sender as TextBox).Text = (sender as TextBox).Text == "已" ? "未" : "已";
            }
        }

        private void btnPic_Click(object sender, EventArgs e)
        {
            pictureBoxT1.LoadImage();
        }

        private void btnPicClr_Click(object sender, EventArgs e)
        {
            pictureBoxT1.Clear();
        }

        private void EmIspay_DoubleClick(object sender, EventArgs e)
        {
            if (btnState == "Append" || btnState == "Modify" || btnState == "Duplicate")
            {
                (sender as TextBox).Text = (sender as TextBox).Text == "Y" ? "N" : "Y";
            }
        }

        private void EmBirth_Leave(object sender, EventArgs e)
        {
            TextBox datetb = (TextBox)sender;
            if (datetb.ReadOnly) return;

            if (datetb.Text.Trim() == "")
            {
                datetb.Clear();
                return;
            }
            if (!datetb.IsDateTime())
            {
                MessageBox.Show("日期格式錯誤！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                datetb.SelectAll();
                datetb.Focus();
            }
        }


        private void EmNo_Enter(object sender, EventArgs e)
        {
            BeforeText = EmNo.Text;
        }

        private void EmNo_Validating(object sender, CancelEventArgs e)
        {
            if (EmNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (EmNo.Text.Trim() == "")
            {
                e.Cancel = true;
                EmNo.Text = "";
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnState == "Append")
            {
                var row = Common.load("Check", "empl", "emno", EmNo.Text.Trim());
                if (row != null)
                {
                    e.Cancel = true;
                    MessageBox.Show("此員工編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EmNo.Text = "";
                    return;
                }
            }

            if (btnState == "Duplicate")
            {
                var row = Common.load("Check", "empl", "emno", EmNo.Text.Trim());
                if (row != null)
                {
                    e.Cancel = true;
                    MessageBox.Show("此員工編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EmNo.Text = "";
                    return;
                }
            }

            if (btnState == "Modify")
            {
                var row = Common.load("Check", "empl", "emno", EmNo.Text.Trim());
                if (row != null)
                {
                    if (EmNo.Text.Trim() != BeforeText) writeToTxt(row);
                }
                else
                {
                    e.Cancel = true;
                    EmNo.SelectAll();
                    using (var frm = new FrmEmplb())
                    {
                        frm.TSeekNo = EmNo.Text.Trim();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            row = Common.load("Check", "empl", "emno", frm.TResult);
                            writeToTxt(row);
                        }
                    }
                }
            }

        }


        private void EmX6no_Validating(object sender, CancelEventArgs e)
        {
            if (EmX6No.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (EmX6No.Text.Trim() == "")
            {
                EmX6No.Text = "";
                EmX6Name.Text = "";
                return;
            }

            xe.ValidateOpen<JBS.JS.XX06>(sender, e, row =>
            {
                EmX6No.Text = row["X6No"].ToString().Trim();
                EmX6Name.Text = row["X6Name"].ToString().Trim();
            });
        }

        private void ck1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxT1.SizeMode = ck1.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.CenterImage;
        }

        private void EmUnde_Validating(object sender, CancelEventArgs e)
        {
            if (EmUnde.ReadOnly) return;
            if (btnCancel.Focused) return;

            TextBox tx = (TextBox)sender;
            tx.Text = tx.Text.Trim();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            EmMemo1.MaxLength = 35;
            EmUnde.MaxLength = 45;
            EmSpec.MaxLength = 45;
            EmMemo1.Width = (35 * S_61.Basic.JEInitialize.CharWidth) + 7;
            EmUnde.Width = (60 * S_61.Basic.JEInitialize.CharWidth) + 7;
            EmSpec.Width = (60 * S_61.Basic.JEInitialize.CharWidth) + 7;
            EmMemo1.MaxLength = 32767;
            EmUnde.MaxLength = 32767;
            EmSpec.MaxLength = 32767;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    if (btnAppend.Enabled) btnAppend.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    if (btnModify.Enabled) btnModify.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (btnDelete.Enabled) btnDelete.PerformClick();
                    break;
                case Keys.D4:
                    if (btnBrow.Enabled) btnBrow.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    if (btnExit.Enabled) btnExit.PerformClick();
                    break;
                case Keys.Home:
                    if (btnTop.Enabled) btnTop.PerformClick();
                    break;
                case Keys.PageUp:
                    if (btnPrior.Enabled) btnPrior.PerformClick();
                    break;
                case Keys.PageDown:
                    if (btnNext.Enabled) btnNext.PerformClick();
                    break;
                case Keys.End:
                    if (btnBottom.Enabled) btnBottom.PerformClick();
                    break;
                case Keys.F9:
                    if (btnSave.Enabled) btnSave.PerformClick();
                    break;
                case Keys.F4:
                    if (btnCancel.Enabled)
                    {
                        btnCancel.Focus();
                        btnCancel.PerformClick();
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public DialogResult ShowDialog(out string itno)
        {
            var dl = this.ShowDialog();

            itno = this.tempNo;
            return dl;
        }
    }
}