﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace JE.MyControl
{
	public class StatusStripT :StatusStrip
	{
        public StatusStripT()
        {
            this.Font = new Font("微軟正黑體", 12F);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.TabIndex = 0;
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