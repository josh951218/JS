using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data.SqlClient;
using S_61.Basic;

namespace JE.MyControl
{
    public class RadioT : Radiobase
    {
        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        public RadioT()
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
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

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mevent)
        {
            if (mevent.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TextBoxT tb = new TextBoxT();
                this.Controls.Clear();
                this.Controls.Add(tb);


                tb.Dock = System.Windows.Forms.DockStyle.Fill;
                tb.Name = "tb";
                tb.MaxLength = 20;
                tb.Text = this.Text;
                tb.Focus();
                tb.SelectionStart = 0;
                tb.SelectionLength = 0;
                tb.ImeMode = System.Windows.Forms.ImeMode.OnHalf;

            }
            base.OnMouseDown(mevent);
        }

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            if (this.Controls.ContainsKey("tb"))
            {
                var tb = this.Controls.Find("tb", false).FirstOrDefault();
                if (tb != null)
                {
                    if (tb.Text.Trim().Length == 0) this.Text = this.Tag.ToString();
                    else
                    {
                        this.Text = tb.Text;
                    }
                    tb.Dispose();
                    this.Controls.Clear();
                }
            }
            base.OnValidating(e);
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
