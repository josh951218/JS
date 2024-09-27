using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace JBS.JS
{
    public partial class FrmXxBasic<T> : Formbase where T : JBS.JS.IxBrow, JBS.JS.IxValidate, new()
    {
        T t;
        xEvents xe;

        List<TextBoxbase> list;
        string tempNo;

        public FrmXxBasic()
        {
            InitializeComponent();
            this.t = new T();
            this.xe = new xEvents();
            this.list = this.getEnumMember();

            this.XNo.MaxLength = t.xNoLength;
            this.XNo.Name = t.xNoID;
            this.XNo.Tag = "";

            this.XName.MaxLength = t.xNameLength;
            this.XName.Name = t.xNameID;

            this.lblNo.Text = t.gridNoText;
            this.lblName.Text = t.gridNameText;

            this.Text = t.xTitle;

            if (XName.MaxLength > 50)
            {
                this.pnlBoxT1.Location = new System.Drawing.Point(177, 163); 
                this.pnlBoxT1.Size = new System.Drawing.Size(656, 218);
            }
            else
            {
                this.pnlBoxT1.Location = new System.Drawing.Point(236, 163); 
                this.pnlBoxT1.Size = new System.Drawing.Size(538, 218);
            }
        }

        private void FrmXxBasic_Load(object sender, EventArgs e)
        {
            using (var db = new JBS.xSQL())
            {
                var key = db.Top(t.ValiTable, t.ValiKey);
                writeToText(key);
            }
        }

        private void FrmXxBasic_Shown(object sender, EventArgs e)
        {
            btnAppend.Focus();
        }

        private void writeToText(string key)
        {
            XNo.Text = key.Trim();
            this.tempNo = key.Trim();

            XName.Clear();
            using (var db = new JBS.xSQL())
            {
                var tsql = "Select * from " + t.ValiTable + " where " + XNo.Name + " = @key";
                db.ExecuteReader(tsql, spc => spc.AddWithValue("key", XNo.Text), row =>
                {
                    XName.Text = row[XName.Name].ToString();
                });
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            using (var db = new JBS.xSQL())
            {
                var key = db.Top(t.ValiTable, t.ValiKey);
                writeToText(key);
            }
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            using (var db = new JBS.xSQL())
            {
                var key = db.Prior(t.ValiTable, t.ValiKey, this.tempNo);

                if (key.Length == 0)
                    key = db.CPrior(t.ValiTable, t.ValiKey, this.tempNo);

                writeToText(key);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            using (var db = new JBS.xSQL())
            {
                var key = db.Next(t.ValiTable, t.ValiKey, this.tempNo);

                if (key.Length == 0)
                    key = db.CNext(t.ValiTable, t.ValiKey, this.tempNo);

                writeToText(key);
            }
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            using (var db = new JBS.xSQL())
            {
                var key = db.Bottom(t.ValiTable, t.ValiKey);
                writeToText(key);
            }
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.Append, ref list);
            XNo.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (XNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Common.SetTextState(FormState = FormEditState.Modify, ref list);
            XNo.Focus();
            XNo.SelectAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (XNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new JBS.xSQL())
            {
                var tsql = "Delete from " + t.ValiTable + " where " + t.ValiKey + " = @key";
                db.ExecuteNonQuery(tsql, spc => spc.AddWithValue("key", this.tempNo));
            }
            btnNext_Click(null, null);

            MessageBox.Show("刪除完成!");
        }

        private void btnBrow_Click(object sender, EventArgs e)
        { 
            if (XNo.TrimTextLenth() == 0)
            {
                MessageBox.Show("空資料庫，請先新增", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new JBS.JS.FrmXxBrow<T>())
            {
                frm.TSeekNo = XNo.Text.Trim();
                var dl = frm.ShowDialog(this);
                if (dl == DialogResult.OK)
                {
                    writeToText(frm.TResult); 
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (XNo.Text.Trim() == "")
            {
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XNo.Focus();
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (this.IsExist(XNo.Text.Trim()))
                {
                    MessageBox.Show("此編號已經重複，請重新輸入!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    XNo.Clear();
                    XName.Clear();
                    XNo.Focus();
                    return;
                }

                using (var db = new JBS.xSQL())
                {
                    var tsql = "Insert into " + t.ValiTable + " (" + XNo.Name + "," + XName.Name + ") values (@p1,@p2)";
                    db.ExecuteNonQuery(tsql, spc =>
                    {
                        spc.AddWithValue("p1", XNo.Text.Trim());
                        spc.AddWithValue("p2", XName.Text.Trim());
                    });
                }

                this.tempNo = XNo.Text.Trim();
                Common.SetTextState(FormState = FormEditState.Append, ref list);
                XNo.Focus();

                return;
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (!this.IsExist(XNo.Text.Trim()))
                {
                    MessageBox.Show("此筆資料已被刪除!", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnCancel_Click(null, null);
                    return;
                }

                using (var db = new JBS.xSQL())
                {
                    var tsql = "Update " + t.ValiTable + " set " + XName.Name + " = @p2 where " + XNo.Name + " = @p1";
                    db.ExecuteNonQuery(tsql, spc =>
                    {
                        spc.AddWithValue("p1", XNo.Text.Trim());
                        spc.AddWithValue("p2", XName.Text.Trim());
                    });
                }

                this.tempNo = XNo.Text.Trim();
                Common.SetTextState(FormState = FormEditState.Append, ref list);
                XNo.Focus();

                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.SetTextState(FormState = FormEditState.None, ref list);
            using (var db = new JBS.xSQL())
            {
                var key = db.Cancel(t.ValiTable, t.ValiKey, this.tempNo);

                if (key.Length == 0)
                    btnNext_Click(null, null);
                else
                    writeToText(key);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    if (btnAppend.Enabled)
                        btnAppend.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    if (btnModify.Enabled)
                        btnModify.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (btnDelete.Enabled)
                        btnDelete.PerformClick();
                    break;
                case Keys.D4:
                    if (btnBrow.Enabled)
                        btnBrow.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.F11:
                    if (btnExit.Enabled)
                        btnExit.PerformClick();
                    break;
                case Keys.Home:
                    if (btnTop.Enabled)
                        btnTop.PerformClick();
                    break;
                case Keys.PageUp:
                    if (btnPrior.Enabled)
                        btnPrior.PerformClick();
                    break;
                case Keys.PageDown:
                    if (btnNext.Enabled)
                        btnNext.PerformClick();
                    break;
                case Keys.End:
                    if (btnBottom.Enabled)
                        btnBottom.PerformClick();
                    break;
                case Keys.F9:
                    if (btnSave.Enabled)
                        btnSave.PerformClick();
                    break;
                case Keys.F4:
                    if (btnCancel.Enabled)
                        btnCancel.Focus();
                    btnCancel.PerformClick();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void tNo_Enter(object sender, EventArgs e)
        {
            XNo.Tag = XNo.Text;
        }

        private void tNo_Validating(object sender, CancelEventArgs e)
        {
            if (XNo.ReadOnly) return;
            if (btnCancel.Focused) return;

            if (XNo.TrimTextLenth() == 0)
            {
                e.Cancel = true;
                XNo.Clear();
                MessageBox.Show("資料不可為空白，請確定後重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.FormState == FormEditState.Append)
            {
                if (this.IsExist(XNo.Text.Trim()))
                {
                    e.Cancel = true;
                    XNo.Text = "";
                    MessageBox.Show("此編號已經重複，請重新輸入", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.FormState == FormEditState.Modify)
            {
                if (this.IsExist(XNo.Text.Trim()))
                {
                    if (XNo.Tag.ToString().Trim() == XNo.Text.Trim())
                        return;

                    writeToText(XNo.Text.Trim());
                }
                else
                {
                    e.Cancel = true;

                    xe.Open<T>(sender, row =>
                    {
                        XNo.Text = row[XNo.Name].ToString().Trim();
                        writeToText(XNo.Text);
                    });

                    XNo.SelectAll();
                }
            }
        }

        bool IsExist(string key)
        {
            using (var db = new JBS.xSQL())
            {
                var tsql = "Select Count(*) from " + t.ValiTable + " where " + t.ValiKey + " = @key";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("key", key));
                if (count.ToDecimal() > 0)
                    return true;
            }
            return false;
        }

        public DialogResult ShowDialog(out string key)
        {
            var dl = this.ShowDialog();

            key = this.tempNo ?? "";
            return dl;
        }
    }
}






