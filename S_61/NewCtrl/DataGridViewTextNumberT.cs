using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JE.MyControl
{
    public class DataGridViewTextNumberT : DataGridViewTextBoxColumn
    {
        public int LastNum { get; set; }
        public int FirstNum { get; set; }
        public bool NullInput { get; set; }
        public string NullValue { get; set; }
        public bool MarkThousand { get; set; }

        public DataGridViewTextNumberT()
        {
            NullInput = false;
            NullValue = "0";
            MarkThousand = false;

            this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
