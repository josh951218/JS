using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;

namespace JE.MyControl
{
    public class TextBoxNumberT : TextBoxbase
    {
        int maxLen = 0;
        decimal x = 1M, y = 1M, w = 1M, h = 1M;

        int lastn = 0;
        int firstn = 10;
        public int LastNum
        {
            get { return lastn; }
            set
            {
                if (value < 0) lastn = 0;
                else
                {
                    lastn = value;
                }
                if (firstn == 1 && lastn == 0) MaxLength = 1;
                else
                {
                    MaxLength = firstn + 1 + lastn;
                }
            }
        }
        public int FirstNum
        {
            get { return firstn; }
            set
            {
                if (value < 0) firstn = 1;
                else
                {
                    firstn = value;
                }
                if (firstn == 1 && lastn == 0) MaxLength = 1;
                else
                {
                    MaxLength = firstn + 1 + lastn;
                }
            }
        }
        public bool NullInput { get; set; }
        public string NullValue { get; set; }
        public bool MarkThousand { get; set; }
        decimal Value = 0M;

        public TextBoxNumberT()
        {
            NullInput = false;
            NullValue = "0";
            MarkThousand = false;

            this.TextAlign = HorizontalAlignment.Right;
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

        protected override void OnMaxLengthChanged(EventArgs e)
        {
            maxLen = this.MaxLength;
            base.OnMaxLengthChanged(e);

            if (maxLen > 100) return;
            this.Width = (maxLen * JEInitialize.CharWidth) + 7;
        }

        protected override void OnResize(EventArgs e)
        {
            var w = (maxLen * JEInitialize.CharWidth) + 7;
            if (this.Width != w)
            {
                this.Width = w;
                return;
            }
            else
            {
                base.OnResize(e);
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            this.oLen = this.MaxLength;
            base.OnEnter(e);

            if (this.ReadOnly) return;
            else
            {
                if (this.MarkThousand)
                {
                    this.Value = this.Text.ToDecimal();
                    this.Text = this.Value.ToString("f" + this.LastNum);
                }

                this.BackColor = Color.GreenYellow;
                this.MaxLength = 200;
                this.SelectAll();
                this.SelectionStart = 0;
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (this.ReadOnly == false && this.SelectionLength == 0) this.SelectAll();
            base.OnMouseUp(mevent);
        }

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            base.OnValidating(e);

            if (this.ReadOnly) return;
            else
            {
                this.BackColor = Color.White;
                this.MaxLength = this.oLen;

                if (this.MarkThousand)
                {
                    this.Value = this.Text.Trim().GetUTF8(this.oLen).ToDecimal();
                    this.Text = this.Value.ToString("N" + this.LastNum);
                }
                else
                {
                    this.Text = this.Text.Trim().GetUTF8(this.oLen).ToDecimal().ToString("f" + LastNum);
                }
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (ReadOnly) return;
            if (maxLen == 0) return;
            if (char.IsControl(e.KeyChar))
            {
                base.OnKeyPress(e);
                return;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == (char)46)
            {
                //不在是最前面輸入負號
                if (e.KeyChar == '-')
                {
                    if (this.SelectionStart != 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }

                //小數點的處理，LastNum=0，不可輸入小數點
                if (e.KeyChar == (char)46)
                {
                    if (LastNum == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }

                //測試把字元插入後是否還是數值型態
                string tChar = e.KeyChar.ToString();
                string tStr = this.Text;
                //沒有選取範圍的時候
                if (this.SelectionLength == 0)
                {
                    tStr = this.Text.Insert(this.SelectionStart, tChar);
                }
                else if (this.SelectionLength > 0)
                {
                    tStr = this.Text.Substring(0, this.SelectionStart);
                    tStr += this.Text.Substring(this.SelectionStart + this.SelectionLength, this.TextLength - this.SelectionStart - this.SelectionLength);
                    tStr = tStr.Insert(this.SelectionStart, tChar);
                }

                if (!IsNumberFormat(tStr))
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = true;
                    var index = this.SelectionStart + 1;
                    this.Text = new string(tStr.Take(this.oLen).ToArray());
                    this.SelectionStart = index;

                    tStr = new string(this.Text.Take(index).ToArray());
                    index = Encoding.GetEncoding(950).GetByteCount(tStr);
                    if (index == this.oLen)
                    {
                        var p = FindForm();
                        if (p != null) p.SelectNextControl(this, true, true, true, true);
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        bool IsNumberFormat(string str)
        {
            //if (str == "" || str == "-" || str == ".") return true;
            if (!IsNumeric(str)) return false;

            //開始驗證格式是否正確
            string tempStr;
            Boolean boolchar = false;//是否有打負數

            //if (tChar == "-") boolchar = true;

            //將負數拿掉
            tempStr = str;
            if (tempStr.StartsWith("-.")) return false;
            if (tempStr.StartsWith("-"))
            {
                boolchar = true;
                tempStr = new string(tempStr.Skip(1).ToArray());
            }
            //沒有負數後，改判小數點是否合法
            if (tempStr.Length > 0 && tempStr[0] == '0' && tempStr.Length > 1)
            {
                if (tempStr[1] != '.') return false;
            }
            //是否有小數點
            if (tempStr.Contains('.'))
            {
                if (tempStr.First() == '.') //小數點在第一位
                {
                    if (tempStr.Length - 1 > LastNum) return false;
                }
                else if (tempStr.Last() == '.') //小數點在最後一位
                {
                    if (boolchar)
                    {
                        if (tempStr.Length - 2 > FirstNum) return false;
                    }
                    else
                    {
                        if (tempStr.Length - 1 > FirstNum) return false;
                    }
                }
                else
                {
                    var p = tempStr.Split('.');
                    var len = boolchar ? p[0].Length - 1 : p[0].Length;
                    if (len > FirstNum) return false;
                    if (p[1].Length > LastNum) return false;
                }
            }
            else
            {
                if (tempStr.Length > FirstNum) return false;
            }
            return true;
        }

        bool IsNumeric(object obj)
        {
            if (obj is string)
            {
                string str = ((string)obj);
                if (str == "" || str == "-" || str == ".") return true;
            }

            decimal d;
            return Decimal.TryParse(Convert.ToString(obj), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out d);
        }

        int WM_PASTEDATA = 0x0302; //貼上資料的訊息
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTEDATA)
            {
                decimal d = 0M;
                var IsNumberic = decimal.TryParse(Clipboard.GetText().Trim(), out d);
                if (!IsNumberic)
                {
                    MessageBox.Show("只能貼上數字！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string str = "";
                var len = 0;
                if (this.SelectionLength > 0)
                {
                    str = new string(this.Text.Take(this.SelectionStart).ToArray());
                    str += Clipboard.GetText();
                    len = str.Length;
                    str += new string(this.Text.Skip(this.SelectionStart + this.SelectionLength).ToArray());
                }
                else
                {
                    str = Clipboard.GetText();
                    len = this.SelectionStart + str.Length;
                    str = this.Text.Insert(this.SelectionStart, str);
                }
                this.Text = str.GetUTF8(this.MaxLength);
                this.SelectionStart = len;
                return;
            }
            base.WndProc(ref m);
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
