using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace JE.MyControl
{
    public class GroupBoxbase : GroupBox
    {
        public GroupBoxbase() { }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Font = S_61.Basic.JEInitialize.ControlFontSize;
            this.TabStop = false;
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
