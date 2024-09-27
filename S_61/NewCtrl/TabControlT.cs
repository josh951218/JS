using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace JE.MyControl
{
    public class TabControlT : TabControlbase
    {
        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        public TabControlT()
        {
            this.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
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
                var p = (this.Font.Size / 12);
                this.ItemSize = new Size(this.ItemSize.Width, (int)(p * this.ItemSize.Height));
            }
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
