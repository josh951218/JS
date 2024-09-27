using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace JE.MyControl
{
    public class LabelMenuT : Labelbase
    {
        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        public LabelMenuT() { }

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

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.TabIndex = 0;
            this.Padding = new Padding(0);
            this.Font = S_61.Basic.JEInitialize.LabelMenyFontSize;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.ForeColor = Color.Blue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            base.OnMouseHover(e);
        }
         
        protected override void OnMouseLeave(EventArgs e)
        {
            this.ForeColor = Color.Black;
            this.Cursor = Cursors.Default;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;

            base.OnMouseLeave(e);
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
