using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using S_61.Basic;

namespace JE.MyControl
{
    public class Radiobase : RadioButton
    {
        public Radiobase() 
        {
            this.BackColor = Color.Transparent;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up) return true;
            if (keyData == Keys.Down) return true;
            return base.IsInputKey(keyData);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Font = JEInitialize.ControlFontSize;
            this.TabStop = false;
            this.Tag = this.Text;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            this.BackColor = (this.Checked) ? Color.LightBlue : Color.Transparent;
            this.TabStop = false;
            base.OnCheckedChanged(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
