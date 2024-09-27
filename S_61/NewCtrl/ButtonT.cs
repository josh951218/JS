using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using S_61.Basic;
using S_61.Properties;

namespace JE.MyControl
{
    public class ButtonT : Buttonbase
    {
        DialogResult result;

        public ButtonT()
        {
            this.FlatStyle = FlatStyle.Popup;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.BackColor = SystemColors.Control;
            this.DialogResult = DialogResult.None;
        }

        public bool UseDefaultSettings { get; set; }

        public override DialogResult DialogResult
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                if (result != DialogResult.None) result = DialogResult.None;
            }
        }

        public override string Text
        {
            get { return string.Empty; }
            set { value = string.Empty; }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            var width = Screen.PrimaryScreen.Bounds.Width;
            if (width <= 800)
            {
                var img800 = Resources.ResourceManager.GetObject(new string(this.Name.Skip(3).ToArray()).ToLower() + "800");
                if (img800 == null) return;
                this.BackgroundImage = (Bitmap)img800;
            }
            else
            {
                var img1024 = Resources.ResourceManager.GetObject(new string(this.Name.Skip(3).ToArray()).ToLower() + "1024");
                if (img1024 == null) return;
                this.BackgroundImage = (Bitmap)img1024;
            }

            if (!UseDefaultSettings)
            {
                if (this.Name.ToLower() == "btnsave") this.Enabled = false;
                if (this.Name.ToLower() == "btncancel") this.Enabled = false;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            var w800 = this.Enabled ? "800" : "800d";
            var w1024 = this.Enabled ? "1024" : "1024d";

            var width = Screen.PrimaryScreen.Bounds.Width;
            if (width <= 800)
            {
                var img800 = Resources.ResourceManager.GetObject(new string(this.Name.Skip(3).ToArray()).ToLower() + w800);
                if (img800 == null) return;
                this.BackgroundImage = (Bitmap)img800;
            }
            else
            {
                var img1024 = Resources.ResourceManager.GetObject(new string(this.Name.Skip(3).ToArray()).ToLower() + w1024);
                if (img1024 == null) return;
                this.BackgroundImage = (Bitmap)img1024;
            }
            base.OnEnabledChanged(e);
        }

        protected override void OnClick(EventArgs e)
        {
            var tag = FindForm().Tag;
            if (tag.IsNullOrEmpty() == false)
            {
                if (!this.IsPowerful(this.Name.skipString(3).ToLower(), tag.ToString().Trim()))
                {
                    MessageBox.Show("無使用權限！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.Name.ToLower() == "btnappend")
            {

            }
            else if (this.Name.ToLower() == "btnduplicate")
            {

            }
            else if (this.Name.ToLower() == "btnmodify")
            {

            }
            else if (this.Name.ToLower() == "btndelete")
            {
                if (MessageBox.Show("請確定是否刪除此筆記錄?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
            }
            else if (this.Name.ToLower() == "btnbrow")
            {

            }
            else if (this.Name.ToLower() == "btnsave")
            {

            }
            else if (this.Name.ToLower() == "btncancel")
            {
                if (Common.User_CanCelPrompt == 1)
                {
                    if (MessageBox.Show("請確定是否放棄編輯此筆資料?", "確認視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
                }

                var frm = FindForm();
                if (frm != null && frm is Formbase) ((Formbase)frm).FormState = S_61.Basic.FormEditState.None;
            }
            else if (this.Name.ToLower() == "btnexit")
            {

            }
            base.OnClick(e);
        }

        bool IsPowerful(string name, string key)
        {
            bool ispower = false;
            string scno = "";
            if (name == "append") scno = "sc01";        //新增
            else if (name == "modify") scno = "sc02";   //修變
            else if (name == "delete") scno = "sc04";   //刪除
            else if (name == "preview") scno = "sc05";  //列印
            else if (name == "print") scno = "sc05";    //列印
            else if (name == "word") scno = "sc05";     //列印
            else if (name == "excel") scno = "sc05";    //列印
            else if (name == "duplicate") scno = "sc06";//複製
            else if (name == "brow") scno = "sc07";     //瀏覽
            else if (name == "***") scno = "sc08";      //全開放
            else if (name == "****") scno = "sc03";     //查詢
            else return true;
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = "select m.scname,d.* from scrit as m,scritd as d where '0'='0' "
                        + " and d.taname='" + key.Trim() + "'"
                        + " and m.scno=d.scno "
                        + " and m.scname='" + Common.User_Name + "' COLLATE Chinese_Taiwan_Stroke_BIN";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            if (reader[scno].ToString().Trim() != "") ispower = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return ispower;
        }

        #region ~dispose
        private bool disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Release managed resources.
                }
                // Release unmanaged resources.
                // Set large fields to null.
                // Call Dispose on your base class.
                disposed = true;
            }
            base.Dispose(disposing);
        }
        // The derived class does not have a Finalize method
        // or a Dispose method without parameters because it inherits
        // them from the base class.
        #endregion
    }
}
