using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace JE.MyControl
{
    public class PanelT : Panelbase
    {
        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        public PanelT()
        {
            this.Padding = new Padding(15);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.TabStop = false;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            //BevelOuter (bvLowered)
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), 0, 0, 0, this.Height - 0);
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), 0, 0, this.Width - 0, 0);
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), this.Width - 1, this.Height - 1, 0, this.Height - 1);
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), this.Width - 1, this.Height - 1, this.Width - 1, 0);

            //BevelInner (bvRaised)
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), 10, 10, 10, this.Height - 10);
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), 10, 10, this.Width - 10, 10);
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), this.Width - (10 + 1), this.Height - (10 + 1), 10, this.Height - (10 + 1));
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), this.Width - (10 + 1), this.Height - (10 + 1), this.Width - (10 + 1), 10);
            base.OnPaint(e);
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
