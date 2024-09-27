using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace JE.MyControl
{
    public class CheckBoxT : CheckBox
    {
        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        public CheckBoxT()
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up) return true;
            if (keyData == Keys.Down) return true;
            return base.IsInputKey(keyData);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.Checked = !this.Checked;
                SendKeys.Send("{tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnEnter(EventArgs e)
        {
            if (this.Checked == false) this.BackColor = Color.LightGray;
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (this.Checked == false) this.BackColor = Color.Transparent;
            base.OnLeave(e);
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            this.BackColor = (this.Checked) ? Color.LightBlue : Color.Transparent;
            if (this.Checked)
            {
                this.BackColor = Color.LightBlue;
            }
            else
            {
                if (this.Focused)
                {
                    this.BackColor = Color.LightGray;
                }
                else
                {
                    this.BackColor = Color.Transparent;
                }
            }
            base.OnCheckedChanged(e);
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

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Font = S_61.Basic.JEInitialize.ControlFontSize;
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
