using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;

namespace JE.MyControl
{
    public class TextBoxTdesp : TextBoxbase
    {
        int maxLen = 0;
        int byteLen = 0;
        int selectByteLen = 0;

        string temps = "";
        string tempc = "";
        string tempe = "";

        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        public TextBoxTdesp()
        {
        }

        public void DesignPattern()
        {
            var frm = this.Parent;
            if (frm != null)
            {
                w = this.Parent.Width;
                h = this.Parent.Height;
                y = (decimal)Location.Y / h;
                x = (decimal)Location.X / w;
            }
        }

        public void ResetLocation()
        {
            var frm = this.Parent;
            if (frm != null)
            {
                this.Location = new Point((int)(x * frm.Width), (int)(y * frm.Height));
            }
        }

        protected override void OnMaxLengthChanged(EventArgs e)
        {
            maxLen = this.MaxLength;
            base.OnMaxLengthChanged(e);

            if (maxLen > 100) return;
            this.Width = (maxLen * S_61.Basic.JEInitialize.CharWidth) + 7;
        }

        protected override void OnResize(EventArgs e)
        {
            var w = (maxLen * S_61.Basic.JEInitialize.CharWidth) + 7;
            if (this.Width != w)
            {
                this.Width = w;
                return;
            }
            else
            {
                base.OnResize(e);
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            this.oLen = this.MaxLength;
            base.OnEnter(e);

            if (this.ReadOnly) return;
            else
            {
                this.BackColor = Color.GreenYellow;
                this.MaxLength = 200;
                this.SelectionStart = 0;
            }
        }

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            base.OnValidating(e);

            if (this.ReadOnly) return;
            else
            {
                this.BackColor = Color.White;
                this.MaxLength = this.oLen;
                this.Text = this.Text.GetUTF8(this.oLen);
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (ReadOnly) return;
            if (maxLen == 0) return;
            if (char.IsControl(e.KeyChar))
            {
                base.OnKeyPress(e);
                return;
            }
            temps = tempc = tempe = "";

            byteLen = Encoding.GetEncoding(950).GetByteCount(Text + e.KeyChar.ToString());
            selectByteLen = Encoding.GetEncoding(950).GetByteCount(SelectedText);

            if (this.Text.Length > 0)
            {
                temps = this.Text.Substring(0, this.SelectionStart);
            }
            tempc = e.KeyChar.ToString();
            tempe = this.Text.Substring(this.SelectionStart + this.SelectionLength);


            e.Handled = true;
            var index = this.SelectionStart + 1;
            //this.Clear();
            this.Text = (temps + tempc + tempe).GetUTF8(this.oLen);
            this.SelectionStart = index;
            base.OnKeyPress(e);

            temps = new string(this.Text.Take(index).ToArray());
            index = Encoding.GetEncoding(950).GetByteCount(temps);
            if (index == this.oLen)
            {
                var p = FindForm();
                if (p != null) p.SelectNextControl(this, true, true, true, true);
            }
            return;
        }

        int WM_PASTEDATA = 0x0302; //貼上資料的訊息

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // tip
            // 
            this.tip.Popup += new System.Windows.Forms.PopupEventHandler(this.tip_Popup);
            this.ResumeLayout(false);

        }

        private void tip_Popup(object sender, PopupEventArgs e)
        {

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTEDATA)
            {
                string str = "";
                var len = 0;
                if (this.SelectionLength > 0)
                {
                    str = new string(this.Text.Take(this.SelectionStart).ToArray());
                    str += Clipboard.GetText();
                    len = str.Length;
                    str += new string(this.Text.Skip(this.SelectionStart + this.SelectionLength).ToArray());
                }
                else
                {
                    str = Clipboard.GetText();
                    len = this.SelectionStart + str.Length;
                    str = this.Text.Insert(this.SelectionStart, str);
                }
                this.Text = str.GetUTF8(this.MaxLength);
                this.SelectionStart = len;
                return;
            }
            base.WndProc(ref m);
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
