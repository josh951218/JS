﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace JE.MyControl
{
    public class ComboBoxT : ComboBox
    {
        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        public ComboBoxT()
        {

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