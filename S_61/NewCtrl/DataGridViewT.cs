using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using S_61.Basic;
using JE.MyControl;

namespace JE.MyControl
{
    public class DataGridViewT : DataGridViewbase
    {
        decimal x = 1M, y = 1M, w = 1M, h = 1M;
        public string tableName = "";

        public DataGridViewT() { }

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
            if (JEInitialize.IsRunTime)
            {
                this.DefaultCellStyle.Font = JEInitialize.ControlFontSize;
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    this.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    if (this.Columns[i].GetType() == typeof(DataGridViewTextBoxColumn) || this.Columns[i].GetType() == typeof(DataGridViewTextNumberT))
                    {
                        var maxlen = ((DataGridViewTextBoxColumn)this.Columns[i]).MaxInputLength;
                        if (maxlen <= 150)
                        {
                            this.Columns[i].Width = (int)((maxlen * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
                        }
                    }
                }
            }
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (this.ReadOnly && e.ColumnIndex == -1 && e.RowIndex == -1)
            {
                using (S_61.SOther.FrmZidingGrid frm = new S_61.SOther.FrmZidingGrid(this))
                {
                    if (this.DataSource == null) return;
                    else
                    {
                        var p = this.FindForm();
                        if (p == null) return;
                        frm.frmName = p.Name;
                        frm.tableName = this.tableName;
                        frm.dtD = ((System.Data.DataTable)this.DataSource);

                        if (DialogResult.OK == frm.ShowDialog())
                        {
                            FindForm().Dispose();
                        }
                    }
                }
            }
            base.OnCellDoubleClick(e);
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
