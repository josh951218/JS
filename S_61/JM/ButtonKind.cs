using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;

namespace JE.MyControl
{
    public class ButtonKind : Buttonbase
    {
        public string KiNo { get; set; }

        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        public ButtonKind()
        {
            this.KiNo = "";
        }

        public new DialogResult DialogResult
        {
            get { return DialogResult.None; }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Font = S_61.Basic.JEInitialize.ControlFontSize;
        }

        public void DesignPattern()
        {
            var frm = this.Parent;
            if (frm != null)
            {
                x = (decimal)Location.X / frm.Width;
                y = (decimal)Location.Y / frm.Height;
                w = (decimal)Width / frm.Width;
                h = (decimal)Height / frm.Height;
            }
        }

        public void ResetLocation()
        {
            var frm = this.Parent;
            if (frm != null)
            {
                this.Location = new Point((int)(x * frm.Width), (int)(y * frm.Height));
                this.Size = new Size((int)(w * frm.Width), (int)(h * frm.Height));
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0007)
            {
                if (JBS.JM.NPOS.SuperInput != null)
                    JBS.JM.NPOS.SuperInput.Focus();

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

        //protected override void OnResize(EventArgs e)
        //{
        //    if (this.Text.Trim().Length > 0)
        //    {
        //        this.Font = S_61.Basic.JEInitialize.ControlFontSize;

        //        var str = this.Text.Replace(Environment.NewLine, "");
        //        var len = Math.Ceiling(Encoding.GetEncoding(950).GetByteCount(str.Trim()) / 2F).ToInteger();

        //        if (len % 2 != 0)
        //            len++;

        //        using (var g = this.CreateGraphics())
        //        {
        //            var t = str.GetUTF8(len); 
        //            if (g.MeasureString(t, this.Font).Width > this.ClientSize.Width)
        //            {
        //                AutoFont(t);
        //            }
        //        }
        //    }

        //    base.OnResize(e);
        //}

        //void AutoFont(string t)
        //{
        //    using (var g = this.CreateGraphics())
        //    {
        //        while (g.MeasureString(t, this.Font).Width + 10 > this.ClientSize.Width)
        //        {
        //            this.Font = new Font(this.Font.FontFamily, this.Font.Size - 1); 
        //        }
        //    }
        //}
    }
}
