using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;

namespace JE.MyControl
{
    public class TextBoxbase : TextBox
    { 
        public delegate void MaxLengthChangeHandler(object sender, System.EventArgs e);
        public event MaxLengthChangeHandler MaxLengthChaged;

        protected ToolTip tip = new ToolTip();
        protected string tipString = "雙擊滑鼠左鍵二下或按[F12]開窗查詢";

        public TextBoxbase()
        {
            this.MaxLength = 10;
            this.AllowResize = true;
        }

        public bool AllowResize { get; set; }

        public bool AllowGrayBackColor { get; set; }

        public int oLen { get; set; }

        public new int MaxLength
        {
            get { return base.MaxLength; }
            set
            {
                if (base.MaxLength != value)
                {
                    base.MaxLength = value;
                    OnMaxLengthChanged(new EventArgs());
                }
            }
        }

        protected virtual void OnMaxLengthChanged(EventArgs e)
        {
            if (MaxLengthChaged != null)
            {
                MaxLengthChaged(this, new EventArgs());
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Font = S_61.Basic.JEInitialize.ControlFontSize;
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            if (this.ReadOnly)
            {
                this.TabStop = false;
                this.BackColor = AllowGrayBackColor ? Color.Silver : Color.FromArgb(243, 243, 243);
            }
            else
            {
                this.TabStop = true;
                this.BackColor = Color.White;
            }
            base.OnReadOnlyChanged(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.ReadOnly)
            {
                if (keyData.ToString() == "C, Control")
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else
                {
                    msg = new Message();
                    return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            if (keyData == Keys.Enter)
            {
                FindForm().SelectNextControl(this, true, true, true, true);
                return true;
            }
            else if (keyData == Keys.F12)
            {
                if (IsAnyDoubleClickandNotReadOnly()) base.OnDoubleClick(null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (IsAnyDoubleClickandNotReadOnly()) tip.SetToolTip(this, tipString);
            base.OnMouseEnter(e);
        }

        bool IsAnyDoubleClickandNotReadOnly()
        {
            if (ReadOnly) return false;
            BindingFlags mPropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
            Type t = typeof(System.Windows.Forms.Control);
            PropertyInfo propertyInfo = t.GetProperty("Events", mPropertyFlags);
            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(this, null);

            BindingFlags mFieldFlags = BindingFlags.Static | BindingFlags.NonPublic;
            FieldInfo fieldInfo = (typeof(Control)).GetField("EventDoubleClick", mFieldFlags);

            Delegate d = eventHandlerList[fieldInfo.GetValue(this)];
            if (d != null && d.GetInvocationList().Length > 0) return true;
            return false;
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
