using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace JE.MyControl
{
    public class PanelBtnT : Panelbase
    {
        Size Size800 = new Size(53, 60);
        Size Size1024 = new Size(69, 79);

        decimal y = 1M;

        public PanelBtnT() { }

        public void DesignPattern()
        {
            var frm = this.Parent;
            if (frm != null)
            {
                y = (decimal)Location.Y / frm.Height;
            }
        }

        public void ResetLocation()
        {
            var frm = this.Parent;
            if (frm != null)
            {
                if (frm.GetType() == typeof(PanelNT))
                {
                    ((PanelNT)frm).BorderStyle = BorderStyle.None;
                    this.Location = new Point((int)((frm.Width - this.Width) / 2), (int)(y * frm.Height + 10M));
                }
                else
                {
                    if (frm is Formbase)
                    {
                        if (((Formbase)frm).CanChangeSize)
                        {
                            this.Location = new Point((int)((frm.Width - this.Width) / 2), (int)(frm.ClientSize.Height - this.Height));
                        }
                        else
                        {
                            var strip = frm.Controls.OfType<Control>().FirstOrDefault(c => c is StatusStrip);
                            if (strip == null)
                            {
                                this.Location = new Point((int)((frm.Width - this.Width) / 2), (int)(frm.ClientSize.Height - this.Height - 3));
                            }
                            else
                            {
                                this.Location = new Point((int)((frm.Width - this.Width) / 2), (int)(frm.ClientSize.Height - (strip.Height + this.Height + 3)));
                            }
                        }

                    }
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Padding = new System.Windows.Forms.Padding(0);

            if (Screen.PrimaryScreen.Bounds.Height <= 600) this.Height = 60;
            else this.Height = 79;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.Height = (Screen.PrimaryScreen.Bounds.Height <= 600) ? 60 : 79;
            List<Buttonbase> list = this.Controls.Cast<Buttonbase>().ToList();
            list.Reverse();
            for (int i = 0; i < list.Count; i++)
            {
                if (Screen.PrimaryScreen.Bounds.Width == 800)
                {
                    list.ElementAt(i).Location = new Point(i * 53, 0);
                    list.ElementAt(i).Size = Size800;
                }
                else
                {
                    list.ElementAt(i).Location = new Point(i * 69, 0);
                    list.ElementAt(i).Size = Size1024;
                }
            }

            this.Height = Screen.PrimaryScreen.Bounds.Height <= 600 ? 60 : 79;
            var w = Screen.PrimaryScreen.Bounds.Width <= 800 ? 53 : 69;
            this.Width = list.Count == 0 ? 100 : w * list.Count + 10;

            base.OnLayout(levent);
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
