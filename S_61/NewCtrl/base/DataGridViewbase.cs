﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using S_61.Basic;
using System.ComponentModel;

namespace JE.MyControl
{
    public class DataGridViewbase : DataGridView
    {
        DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
        TextBox input;
        TextBox textp;
        int NumFirst = 0;
        int NumLast = 0;
        int VisibleColumnsCount = 0;

        int oLen = 0;
        int byteLen = 0;
        int selectByteLen = 0;
        string temps = "";
        string tempc = "";
        string tempe = "";

        public DataGridViewbase()
        {
            ISDocument = false;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.AutoGenerateColumns = false;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.BackgroundColor = Color.White;
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.MultiSelect = false;
            this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ReadOnly = true;
            //Header
            this.EnableHeadersVisualStyles = false;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RowHeadersWidth = 20;
            //Cell
            this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.ShowCellToolTips = false;
            //Row
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(210, 214, 217);
            this.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //模式
            this.EditMode = DataGridViewEditMode.EditOnEnter;
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            //
            CellStyle.SelectionBackColor = Color.Yellow;
            CellStyle.SelectionForeColor = Color.Black;
            CellStyle.BackColor = Color.White;
            this.RowsDefaultCellStyle = CellStyle;
        }

        public bool ISDocument { get; set; }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Font = JEInitialize.ControlFontSize;
            this.ColumnHeadersDefaultCellStyle.Font = JEInitialize.ControlFontSize;
            this.DefaultCellStyle.Font = JEInitialize.ControlFontSize;
        }

        protected override void OnCurrentCellChanged(EventArgs e)
        {
            if (CurrentCell != null)
            {
                var index = CurrentCell.RowIndex;
                if (0 <= index && index <= this.Rows.Count - 1)
                {
                    this[0, index].Style = CellStyle;
                    if (CurrentCell.ReadOnly)
                    {
                        CurrentCell.Style.SelectionBackColor = Color.Red;
                        CurrentCell.Style.SelectionForeColor = Color.Black;
                    }
                }
            }
            base.OnCurrentCellChanged(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            if (CurrentCell != null)
            {
                var index = CurrentCell.RowIndex;
                if (0 <= index && index <= this.Rows.Count - 1)
                {
                    this[0, index].Style = CellStyle;
                    if (CurrentCell.ReadOnly)
                    {
                        CurrentCell.Style.SelectionBackColor = Color.Red;
                        CurrentCell.Style.SelectionForeColor = Color.Black;
                    }
                }
            }
            base.OnEnter(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.ColumnHeadersDefaultCellStyle.Font = S_61.Basic.JEInitialize.ControlFontSize;
            if (JEInitialize.ControlFontSize.Size >= 16) this.RowTemplate.Height = 26;
            base.OnLayout(e);
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);

            if (!JEInitialize.IsRunTime)
            {
                e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
                e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                if (e.Column.GetType() == typeof(DataGridViewTextBoxColumn) || e.Column.GetType() == typeof(DataGridViewTextNumberT))
                {
                    var maxlen = ((DataGridViewTextBoxColumn)e.Column).MaxInputLength;
                    if (maxlen <= 150)
                    {
                        e.Column.Width = (int)((maxlen * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
                    }
                }
            }
        }

        protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
        {

            this[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
            this[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Yellow;
            this[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Black;

            if (input != null && this.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewTextNumberT))
            {
                DataGridViewTextNumberT t = ((DataGridViewTextNumberT)this.Columns[e.ColumnIndex]);

                if (!t.NullInput)
                {
                    input.Text = (input.TrimTextLenth() == 0) ? t.NullValue : input.Text.Trim();
                }
                if (input.Text.Contains("-")) input.Text = input.Text.Replace(",", "");
                if (input.Text == "-") input.Text = t.NullValue;
                if (input.Text == ".") input.Text = t.NullValue;
                //
                if (!IsNumber(input.Text, null))
                {
                    input.Text = t.NullValue;
                }
                //格式化
                if (t.MarkThousand)
                {
                    input.Text = string.Format("{0:" + "N" + NumLast.ToString() + "}", input.Text.Trim().ToDecimal());
                }
                else if (input.Text != "")
                {
                    input.Text = string.Format("{0:" + "F" + NumLast.ToString() + "}", input.Text.Trim().ToDecimal());
                }
            }
            base.OnCellValidating(e);
        }

        protected override void OnCellValidated(DataGridViewCellEventArgs e)
        {
            if (input != null)
            {
                input.KeyPress -= keyPress;
                input.KeyDown -= txtKeyDown;
            }
            if (textp != null)
            {
                textp.KeyPress -= OnKeyPressChar;
                textp.Validating -= txtValidating;
            }
            base.OnCellValidated(e);
        }


        void OnKeyPressChar(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                base.OnKeyPress(e);
                return;
            }
            temps = tempc = tempe = "";

            this.byteLen = Encoding.GetEncoding(950).GetByteCount(textp.Text + e.KeyChar.ToString());
            this.selectByteLen = Encoding.GetEncoding(950).GetByteCount(textp.SelectedText);

            if (textp.Text.Length > 0)
            {
                temps = textp.Text.Substring(0, textp.SelectionStart);
            }
            tempc = e.KeyChar.ToString();
            tempe = textp.Text.Substring(textp.SelectionStart + textp.SelectionLength);


            e.Handled = true;
            var index = textp.SelectionStart + 1;

            textp.Text = (temps + tempc + tempe).GetUTF8(this.oLen);
            textp.SelectionStart = index;
            base.OnKeyPress(e);

            temps = new string(textp.Text.Take(index).ToArray());
            index = Encoding.GetEncoding(950).GetByteCount(temps);
            if (index == this.oLen)
            {
                SendKeys.Send("{TAB}");
                //var p = FindForm();
                //if (p != null) p.SelectNextControl(this, true, true, true, true);
            }
            return;
        }

        void txtValidating(object sender, CancelEventArgs e)
        {
            textp.Text = textp.Text.GetUTF8(this.oLen);
        }

        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            base.OnEditingControlShowing(e);

            e.CellStyle.BackColor = Color.Yellow;
            var name = this.CurrentCell.OwningColumn.Name;
            var p = this.Columns[name];
            if (p is DataGridViewTextNumberT)
            {
                input = ((TextBox)e.Control);
                DataGridViewTextNumberT t = ((DataGridViewTextNumberT)p);
                NumFirst = t.FirstNum;
                NumLast = t.LastNum;
           
                input.KeyPress -= keyPress;
                input.KeyPress += keyPress;
                input.KeyDown -= txtKeyDown;
                input.KeyDown += txtKeyDown;
            }
            else if (p is DataGridViewTextBoxColumn)
            {
                textp = ((TextBox)e.Control);

                this.oLen = ((DataGridViewTextBoxColumn)p).MaxInputLength;
                if (p.Tag != null && p.Tag is string[])
                {
                    this.oLen = ((string[])p.Tag)[1].ToInteger();
                }

                textp.MaxLength = 200;
                textp.KeyPress -= OnKeyPressChar;
                textp.KeyPress += OnKeyPressChar;
                textp.Validating -= new CancelEventHandler(txtValidating);
                textp.Validating += new CancelEventHandler(txtValidating);
            }
        }

        void keyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
            {
                base.OnKeyPress(e);
                return;
            }
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == (char)46)
            {
                //不在是最前面輸入負號
                if (e.KeyChar == '-')
                {
                    if (input.SelectionStart != 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }

                //小數點的處理，NumLast=0，不可輸入小數點
                if (e.KeyChar == (char)46)
                {
                    if (NumLast == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }

                //測試把字元插入後是否還是數值型態
                string tChar = e.KeyChar.ToString();
                string tStr = input.Text;
                //沒有選取範圍的時候
                if (input.SelectionLength == 0)
                {
                    tStr = input.Text.Insert(input.SelectionStart, tChar);
                }
                else if (input.SelectionLength > 0)
                {
                    tStr = input.Text.Substring(0, input.SelectionStart);
                    tStr += input.Text.Substring(input.SelectionStart + input.SelectionLength, input.TextLength - input.SelectionStart - input.SelectionLength);
                    tStr = tStr.Insert(input.SelectionStart, tChar);
                }
                if (!IsNumber(tStr, null))
                {
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                string TempNumStr = "";
                if (input.SelectionLength != input.TextLength && input.SelectionStart > 0)
                {
                    if (input.SelectionLength > 0)
                    {
                        TempNumStr = input.Text.Substring(0, input.SelectionStart);
                        TempNumStr += input.Text.Substring(input.SelectionStart + input.SelectionLength, input.TextLength - TempNumStr.Length - input.SelectionLength);
                    }
                    else
                    {
                        TempNumStr = input.Text.Substring(0, input.SelectionStart - 1);
                        TempNumStr += input.Text.Substring(input.SelectionStart, input.TextLength - input.SelectionStart);
                    }
                }
                else if (input.SelectionLength != input.TextLength && input.SelectionStart == 0)
                {
                    if (input.SelectionLength > 0)
                    {
                        TempNumStr = input.Text.Substring(input.SelectionLength, input.TextLength - input.SelectionLength);
                    }
                }
                if (!IsNumber(TempNumStr, null))
                {
                    e.Handled = true;
                    return;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        void txtKeyDown(object sender, KeyEventArgs e)
        {
            string str = input.Text;
            if (e.KeyData == Keys.Delete)
            {
                string TempNumStr = "";
                if (input.SelectionLength != input.TextLength && input.SelectionStart < input.TextLength)
                {
                    if (input.SelectionLength > 0)
                    {
                        TempNumStr = input.Text.Substring(0, input.SelectionStart);
                        TempNumStr += input.Text.Substring(input.SelectionStart + input.SelectionLength, input.TextLength - TempNumStr.Length - input.SelectionLength);
                    }
                    else
                    {
                        TempNumStr = input.Text.Substring(0, input.SelectionStart);
                        TempNumStr += input.Text.Substring(input.SelectionStart + 1, input.TextLength - input.SelectionStart - 1);
                    }
                }
                else if (input.SelectionLength != input.TextLength && input.SelectionStart == input.TextLength)
                {
                    if (input.SelectionLength > 0)
                    {
                        TempNumStr = input.Text.Substring(0, input.SelectionStart);
                    }
                }
                if (!IsNumber(TempNumStr, null))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        bool IsNumber(string str, string tChar)
        {
            if (!IsNumeric(str)) return false;
            if (str.Trim().Length == 0) return true;
            if (str == "-" || str == ".") return true;

            string tempStr;
            Boolean boolchar = false;//是否有打負數

            if (tChar == "-") boolchar = true;

            //將負數拿掉
            tempStr = str;
            if (tempStr.StartsWith("-.")) return false;
            if (tempStr.StartsWith("-"))
            {
                boolchar = true;
                tempStr = new string(tempStr.Skip(1).ToArray());
            }
            //沒有負數後，改判小數點是否合法
            if (tempStr[0] == '0' && tempStr.Length > 1)
            {
                if (tempStr[1] != '.') return false;
            }
            //是否有小數點
            if (tempStr.Contains('.'))
            {
                if (tempStr.First() == '.') //小數點在第一位
                {
                    if (tempStr.Length - 1 > NumLast) return false;
                }
                else if (tempStr.Last() == '.') //小數點在最後一位
                {
                    if (boolchar)
                    {
                        if (tempStr.Length - 2 > NumFirst) return false;
                    }
                    else
                    {
                        if (tempStr.Length - 1 > NumFirst) return false;
                    }
                }
                else
                {
                    var p = tempStr.Split('.');
                    var len = boolchar ? p[0].Length - 1 : p[0].Length;
                    if (len > NumFirst) return false;
                    if (p[1].Length > NumLast) return false;
                }
            }
            else
            {
                if (tempStr.Length > NumFirst) return false;
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

            double d;
            return Double.TryParse(Convert.ToString(obj), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out d);
        }





        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (VisibleColumnsCount == 0)
            {
                foreach (DataGridViewColumn column in this.Columns)
                {
                    if (column.Visible) VisibleColumnsCount++;
                }
            }
            if (keyData == Keys.Enter)
            {
                if (CurrentCell != null)
                {
                    if (this.Columns[VisibleColumnsCount - 1].ReadOnly)
                    {
                        if (CurrentCell.ColumnIndex == VisibleColumnsCount && CurrentCell.RowIndex == Rows.Count - 1)
                        {
                            var bt = FindForm().Controls.Find("gridAppend", true).FirstOrDefault();
                            if (bt != null && bt is Button)
                            {
                                CurrentCell = this[0, CurrentCell.RowIndex];
                                bt.Focus();
                                ((Button)bt).PerformClick();
                            }
                            return true;
                        }
                    }
                    else
                    {
                        if (CurrentCell.ColumnIndex == (VisibleColumnsCount - 1) && CurrentCell.RowIndex == Rows.Count - 1)
                        {
                            var bt = FindForm().Controls.Find("gridAppend", true).FirstOrDefault();
                            if (bt != null && bt is Button)
                            {
                                CurrentCell = this[0, CurrentCell.RowIndex];
                                bt.Focus();
                                ((Button)bt).PerformClick();
                            }
                            return true;
                        }
                    }
                }

                SendKeys.Send("{TAB}");
                return true;
            }
            else if (keyData == Keys.Left)
            {
                if (CurrentCell != null && CurrentCell.ColumnIndex > 0)
                {
                    if (this.EditingControl != null)
                    {
                        var t = ((TextBox)this.EditingControl);
                        if (t.SelectionLength == t.Text.Length || t.SelectionStart == 0)
                        {
                            t.SelectionLength = 0;
                            t.SelectionStart = 0;
                        }
                        else
                        {
                            t.SelectionStart = t.SelectionStart - 1;
                            t.SelectionLength = 0;
                            return true;
                        }
                        //var t = ((TextBox)this.EditingControl);
                        //t.SelectionLength = 0;
                        //t.SelectionStart = 0;
                    }
                }
            }
            else if (keyData == Keys.Right)
            {
                if (CurrentCell != null && CurrentCell.ColumnIndex < VisibleColumnsCount)
                {
                    if (EditingControl != null)
                    {
                        var t = ((TextBox)this.EditingControl);
                        if (t.SelectionLength == t.Text.Length || t.SelectionStart >= t.Text.Length)
                        {
                            t.SelectionLength = 0;
                            t.SelectionStart = t.Text.Length + 1;
                        }
                        else
                        {
                            t.SelectionStart = t.SelectionStart + t.SelectionLength + 1;
                            t.SelectionLength = 0;
                            return true;
                        }
                        //var t = ((TextBox)this.EditingControl);
                        //t.SelectionLength = 0;
                        //t.SelectionStart = t.Text.Length + 1;
                    }
                }
            }
            else if (keyData == Keys.Escape)
            {
                if (this.ReadOnly == false)
                {
                    SendKeys.Send("^Z");
                    return true;
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            else if (keyData == Keys.Down)
            {
                if (ISDocument)
                {
                    var index = this.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (index == this.Rows.Count - 1 && index >= 0)
                    {
                        if (FindForm().Validate())
                        {
                            if (this.Columns.Contains("產品編號"))
                            {
                                if (this["產品編號", index].EditedFormattedValue.ToString().Trim().Length == 0) { }
                                else
                                {
                                    var gridAppend = FindForm().Controls.Find("gridAppend", true).FirstOrDefault();
                                    if (gridAppend != null)
                                    {
                                        ((Button)gridAppend).PerformClick();
                                        return true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            else if (keyData.ToString().Trim().StartsWith("C, Control") && keyData.ToString().Trim().EndsWith("C, Control"))
            {
                try
                {
                    if (this.ReadOnly)
                    {
                        if (CurrentCell != null)
                        {
                            if (CurrentCell.Value != null)
                            {
                                Clipboard.Clear();
                                Clipboard.SetText(CurrentCell.Value.ToString().Trim());
                            }
                        }
                    }
                    else
                    {
                        if (CurrentCell != null)
                        {
                            if (this.EditingControl != null)
                            {
                                var t = ((TextBox)this.EditingControl);
                                if (t.SelectionLength > 0)
                                {
                                    Clipboard.Clear();
                                    Clipboard.SetText(t.SelectedText);
                                }
                            }
                        }
                        else
                        {
                            Clipboard.Clear();
                            Clipboard.SetText(CurrentCell.Value.ToString().Trim());
                        }
                    }

                }
                catch { }
            }
            else if (keyData.ToString().Trim().StartsWith("V") && keyData.ToString().Trim().EndsWith(", Control"))
            {
                try
                {
                    if (this.ReadOnly == false && CurrentCell != null)
                    {
                        if (this.EditingControl != null)
                        {
                            var name = this.CurrentCell.OwningColumn.Name;
                            var c = this.Columns[name];
                            if (c is DataGridViewTextBoxColumn)
                            {
                                var t = ((TextBox)this.EditingControl);
                                if (t.SelectionLength > 0)
                                {
                                    return base.ProcessCmdKey(ref msg, keyData);
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                catch { }
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