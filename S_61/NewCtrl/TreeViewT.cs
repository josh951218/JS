using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JE.MyControl;
using System.Windows.Forms;
using System.Drawing;
using S_61.Basic;

namespace JE.MyControl
{
    public class TreeViewT : TreeView
    {
        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        public TreeViewT()
        {
            this.Cursor = Cursors.Hand;
            this.PathSeparator = "";
            this.FullRowSelect = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up) keyData = Keys.Left;
            if (keyData == Keys.Down) keyData = Keys.Right;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Font = JEInitialize.ControlFontSize;
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
