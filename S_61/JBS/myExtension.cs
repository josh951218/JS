using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace S_61.Basic
{
    public static class myExtension
    {
        public static void ShowInit(this Formbase frm)
        {
            if (frm.Parent != null)
            {
                //選單與主要表單
                //frm.Size = MainForm.menu.Size;
                frm.Size = new Size(MainForm.menu.Size.Width, MainForm.menu.Size.Height - 25);
            }
            else
            {
                //瀏覽視窗
                if (MainForm.menu != null)
                {
                    if (frm.Style == FormStyle.Max)
                    {
                        frm.Width = MainForm.menu.ClientSize.Width - 5;
                        frm.Height = MainForm.menu.ClientSize.Height + 14;
                    }
                    else if (frm.Style == FormStyle.Mini)
                    {
                        frm.Width = MainForm.menu.ClientSize.Width - 5;
                        frm.Height = MainForm.menu.ClientSize.Height * 6 / 7;
                    }
                    else if (frm.Style == FormStyle.VeryMini)
                    {
                        frm.Width = MainForm.menu.ClientSize.Width / 2;
                        frm.Height = MainForm.menu.ClientSize.Height / 2;
                    }
                }
            }
        }

        public static void DesignPattern(this TabPage page)
        {
            decimal x = 1M, y = 1M, w = 1M, h = 1M;

            var p = page.Parent;
            if (p != null)
            {
                x = (decimal)page.Location.X / p.Width;
                y = (decimal)page.Location.Y / p.Height;
                w = (decimal)page.Width / p.Width;
                h = (decimal)page.Height / p.Height;
            }
            List<decimal> list = new List<decimal>();
            list.Add(x);
            list.Add(y);
            list.Add(w);
            list.Add(h);
            page.Tag = list;
        }

        public static void ResetLocation(this TabPage page)
        {
            List<decimal> list = (List<decimal>)page.Tag;
            decimal x = 1M, y = 1M, w = 1M, h = 1M;
            x = list.ElementAt(0);
            y = list.ElementAt(1);
            w = list.ElementAt(2);
            h = list.ElementAt(3);

            var p = page.Parent;
            if (p != null)
            {
                page.Location = new Point((int)(x * p.Width), (int)(y * p.Height));
                page.Size = new Size((int)(w * p.Width), (int)(h * p.Height));
            }
        }

        public static void Set銷貨單價小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.MS;
            column.DefaultCellStyle.Format = "f" + Common.MS;
        }

        public static void Set銷貨單據小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.MST;
            column.DefaultCellStyle.Format = "f" + Common.MST;
        }

        public static void Set銷項金額小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.TPS;
            column.DefaultCellStyle.Format = "f" + Common.TPS;
        }

        public static void Set銷項稅額小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.TS;
            column.DefaultCellStyle.Format = "f" + Common.TS;
        }

        public static void Set進貨單價小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.MF;
            column.DefaultCellStyle.Format = "f" + Common.MF;
        }

        public static void Set進貨單據小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.MFT;
            column.DefaultCellStyle.Format = "f" + Common.MFT;
        }

        public static void Set進項金額小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.TPF;
            column.DefaultCellStyle.Format = "f" + Common.TPF;
        }

        public static void Set進項稅額小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.TF;
            column.DefaultCellStyle.Format = "f" + Common.TF;
        }

        public static void Set本幣金額小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.M;
            column.DefaultCellStyle.Format = "f" + Common.M;
        }

        public static void Set庫存數量小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.Q;
            column.DefaultCellStyle.Format = "f" + Common.Q;
        }

        public static void Set單據數量小數(this DataGridViewTextNumberT column)
        {
            column.FirstNum = 12;
            column.LastNum = Common.DQ;
            column.DefaultCellStyle.Format = "f" + Common.DQ;
        }

        public static void Set銷貨單價小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 10;
            obj.LastNum = Common.MS;
        }

        public static void Set銷貨單據小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 12;
            obj.LastNum = Common.MST;
        }

        public static void Set銷項金額小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 10;
            obj.LastNum = Common.TPS;
        }

        public static void Set銷項稅額小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 12;
            obj.LastNum = Common.TS;
        }

        public static void Set進貨單價小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 10;
            obj.LastNum = Common.MF;
        }

        public static void Set進貨單據小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 10;
            obj.LastNum = Common.MFT;
        }

        public static void Set進項金額小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 10;
            obj.LastNum = Common.TPF;
        }

        public static void Set進項稅額小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 10;
            obj.LastNum = Common.TF;
        }

        public static void Set本幣金額小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 10;
            obj.LastNum = Common.M;
        }

        public static void Set庫存數量小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 10;
            obj.LastNum = Common.Q;
        }

        public static void Set單據數量小數(this TextBoxNumberT obj)
        {
            obj.FirstNum = 10;
            obj.LastNum = Common.DQ;
        }

        public static Control FindControl(this Form ctrl, string name)
        {
            Control c = null;
            var p = ctrl.Controls.Find(name, true);
            if (p.Length > 0) c = p[0];
            return c;
        }
        public static bool IsNotNull(this object obj)
        {
            if (obj != null) return true;
            return false;
        }

        public static bool IsDateTime(this TextBox tx)
        {
            if (tx.Text.Length > 0)
            {
                if (tx.Text.Any(c => char.IsDigit(c) == false)) return false;
            }
            TaiwanCalendar tw = new TaiwanCalendar();
            bool flag = false;
            string year = "";
            string month = "";
            string day = "";
            DateTime t;
            if (Common.User_DateTime == 1)
            {
                if (tx.Text.Length != 7) return false;
                year = new string(tx.Text.Take(3).ToArray());
                month = new string(tx.Text.Skip(3).Take(2).ToArray()) + "/";
                day = new string(tx.Text.Skip(5).Take(2).ToArray());
                if (DateTime.Now.Year == tw.GetYear(DateTime.Now))
                {
                    //系統為民國年
                    year += "/";
                }
                else
                {
                    //系統為西元年
                    year = year.ToDecimal() + 1911 + "/";
                }
            }
            else
            {
                if (tx.Text.Length != 8) return false;
                year = new string(tx.Text.Take(4).ToArray());
                month = new string(tx.Text.Skip(4).Take(2).ToArray()) + "/";
                day = new string(tx.Text.Skip(6).Take(2).ToArray());
                if (DateTime.Now.Year == tw.GetYear(DateTime.Now))
                {
                    //系統為民國年
                    year = year.ToDecimal() - 1911 + "/";
                }
                else
                {
                    //系統為西元年
                    year += "/";
                }
            }
            flag = DateTime.TryParse(year + month + day, out t);
            return flag;
        }
        public static int TrimTextLenth(this TextBox tx)
        {
            int len = 0;
            len = tx.Text.Trim().Length;
            return len;
        }
        public static void SetDateLength(this JE.MyControl.TextBoxT tx)
        {
            tx.MaxLength = Common.User_DateTime == 1 ? 7 : 8;
        }

        public static void Search(this DataGridView grid, string TColumnName, string TValue)
        {
            if (TColumnName.IsNullOrEmpty()) return;
            if (TValue.IsNullOrEmpty()) return;
            if (grid.Rows.Count == 0) return;

            int index = -1;
            List<DataGridViewRow> list = grid.Rows.OfType<DataGridViewRow>().ToList();
            index = list.FindIndex(r => r.Cells[TColumnName].Value.ToString() == TValue);
            if (index == -1)
            {
                index = list.FindLastIndex(r => string.CompareOrdinal(TValue, r.Cells[TColumnName].Value.ToString()) > 0) + 1;
                if (index <= 0) index = 0;
                else if (index >= grid.Rows.Count) index = grid.Rows.Count - 1;
            }
            grid.FirstDisplayedScrollingRowIndex = index;
            grid.CurrentCell = grid[0, index];
            grid.Rows[index].Selected = true;
        }
        public static void Search(this DataGridView grid, string Column1, string Value1, string Column2, string Value2)
        {
            if (Value1.Trim().Length > 0 && Value2.Trim().Length > 0)
            {
                List<DataGridViewRow> list = grid.Rows.OfType<DataGridViewRow>().ToList();
                int index = list.FindIndex(r => r.Cells[Column1].Value.ToString() == Value1 && r.Cells[Column2].Value.ToString() == Value2);
                if (index != -1)
                {
                    grid.FirstDisplayedScrollingRowIndex = index;
                    grid.CurrentCell = grid[0, index];
                    grid.Rows[index].Selected = true;
                }
                else
                {
                    index = list.FindIndex(r => r.Cells[Column1].Value.ToString() == Value1);
                    int index2 = list.FindIndex(r => r.Cells[Column2].Value.ToString() == Value2);
                    if (index != -1)
                    {
                        grid.Search(Column1, Value1);
                    }
                    else
                    {
                        if (index2 != -1)
                        {
                            grid.Search(Column2, Value2);
                        }
                        else
                        {
                            grid.Search(Column1, Value1);
                        }
                    }
                }
            }
            else if (Value1.Trim().Length > 0 && Value2.Trim().Length == 0)
            {
                grid.Search(Column1, Value1);
            }
            else if (Value1.Trim().Length == 0 && Value2.Trim().Length > 0)
            {
                grid.Search(Column2, Value2);
            }
            else
            {
                return;
            }
        }
        public static void SearchForDate(this DataGridView grid, string TColumnName, string TValue)
        {
            if (TColumnName.IsNullOrEmpty()) return;
            if (TValue.IsNullOrEmpty()) return;
            if (grid.Rows.Count == 0) return;

            int index = -1;
            List<DataGridViewRow> list = grid.Rows.OfType<DataGridViewRow>().ToList();
            index = list.FindIndex(r => r.Cells[TColumnName].Value.ToString() == TValue);
            if (index == -1)
            {
                index = list.FindLastIndex(r => string.CompareOrdinal(r.Cells[TColumnName].Value.ToString(), TValue) > 0);
                if (index <= 0) index = 0;
                else if (index >= grid.Rows.Count) index = grid.Rows.Count - 1;

            }
            grid.FirstDisplayedScrollingRowIndex = index;
            grid.CurrentCell = grid[0, index];
            grid.Rows[index].Selected = true;
        }
        public static void SearchForDate(this DataGridView grid, string Column1, string Value1, string Column2, string Value2)
        {
            if (Value1.Trim().Length > 0 && Value2.Trim().Length > 0)
            {
                List<DataGridViewRow> list = grid.Rows.OfType<DataGridViewRow>().ToList();
                int index = list.FindIndex(r => r.Cells[Column1].Value.ToString() == Value1 && r.Cells[Column2].Value.ToString() == Value2);
                if (index != -1)
                {
                    grid.FirstDisplayedScrollingRowIndex = index;
                    grid.CurrentCell = grid[0, index];
                    grid.Rows[index].Selected = true;
                }
                else
                {
                    index = list.FindIndex(r => r.Cells[Column1].Value.ToString() == Value1);
                    int index2 = list.FindIndex(r => r.Cells[Column2].Value.ToString() == Value2);
                    if (index != -1)
                    {
                        grid.Search(Column1, Value1);
                    }
                    else
                    {
                        if (index2 != -1)
                        {
                            grid.Search(Column2, Value2);
                        }
                        else
                        {
                            grid.Search(Column1, Value1);
                        }
                    }
                }
            }
            else if (Value1.Trim().Length > 0 && Value2.Trim().Length == 0)
            {
                grid.Search(Column1, Value1);
            }
            else if (Value1.Trim().Length == 0 && Value2.Trim().Length > 0)
            {
                grid.SearchForDate(Column2, Value2);
            }
            else
            {
                return;
            }
        }

        public static int FindIndex(this DataView dv, string Filter)
        {
            if (string.IsNullOrEmpty(Filter)) return -1;
            if (Filter.Contains("=") == false) return -1;

            bool IsNumber = false;
            var index = Filter.IndexOf('=');

            var column = new string(Filter.ToArray(), 0, index).Trim();
            var obj = new string(Filter.Skip(index + 1).ToArray()).Trim();
            var value = "";
            var number = 0M;

            if (obj.Contains("\'"))
            {
                IsNumber = false;
                value = obj.Replace("\'", string.Empty);

            }
            else
            {
                IsNumber = true;
                number = obj.ToDecimal();
            }

            if (IsNumber)
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    if (dv[i][column].ToDecimal() == number) return i;
                }
            }
            else
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    if (dv[i][column].ToString() == value) return i;
                }
            }
            return -1;
        }
        public static void Search(this DataView dv, ref DataGridViewT grid, string TColumn, string TValue)
        {
            if (String.IsNullOrEmpty(TValue)) return;
            if (!dv.Table.Columns.Contains(TColumn)) return;
            if (dv.Count == 0) return;

            var list = dv.OfType<DataRowView>().ToList();
            var index = list.FindIndex(r => r[TColumn].ToString() == TValue);
            if (index == -1)
            {
                //index = list.FindLastIndex(r => String.CompareOrdinal(TValue, r[TColumn].ToString()) > 0) + 1;
                for (int i = 0; i < list.Count; i++)
                {
                    if (String.CompareOrdinal(list[i][TColumn].ToString(), TValue) > 0)
                    {
                        index = i;
                        break;
                    }
                    if (i == list.Count - 1)
                        index = i;
                }
            }

            if (index == -1) index = 0;
            if (index >= dv.Count) index = dv.Count - 1;

            grid.FirstDisplayedScrollingRowIndex = index;
            grid.CurrentCell = grid[0, index];
            grid.Rows[index].Selected = true;
        }
        public static void Search(this DataView dv, ref DataGridViewT grid, string Column1, string Value1, string Column2, string Value2)
        {
            if (String.IsNullOrEmpty(Value1)) return;
            if (String.IsNullOrEmpty(Value2)) return;
            if (!dv.Table.Columns.Contains(Column1)) return;
            if (!dv.Table.Columns.Contains(Column2)) return;
            if (dv.Count == 0) return;

            if (Value1.Trim().Length > 0 && Value2.Trim().Length > 0)
            {
                int index = -1;
                var list = dv.OfType<DataRowView>().AsParallel().ToList();
                index = list.FindIndex(r => r[Column1].ToString() == Value1 && r[Column2].ToString() == Value2);

                if (index != -1)
                {
                    grid.FirstDisplayedScrollingRowIndex = index;
                    grid.CurrentCell = grid[0, index];
                    grid.Rows[index].Selected = true;
                }
                else
                {
                    index = list.FindIndex(r => r[Column1].ToString() == Value1);
                    if (index != -1)
                    {
                        for (int i = index; i < dv.Count; i++)
                        {
                            if (dv[i][Column1].ToString() == Value1)
                            {
                                index = i;
                                if (String.CompareOrdinal(dv[i][Column2].ToString(), Value2) > 0)
                                {
                                    index = i;
                                    break;
                                }
                            }
                            else
                            {
                                index = i - 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        index = list.FindLastIndex(r => String.CompareOrdinal(Value1, r[Column1].ToString()) > 0) + 1;
                    }

                    if (index <= 0) index = 0;
                    if (index >= dv.Count) index = dv.Count - 1;
                    grid.FirstDisplayedScrollingRowIndex = index;
                    grid.CurrentCell = grid[0, index];
                    grid.Rows[index].Selected = true;
                }
            }
            else if (Value1.Trim().Length > 0 && Value2.Trim().Length == 0)
            {
                dv.Search(ref grid, Column1, Value1);
            }
            else if (Value1.Trim().Length == 0 && Value2.Trim().Length > 0)
            {
                dv.Search(ref grid, Column2, Value2);
            }
            else
            {
                return;
            }
        }

        public static void SearchForDate(this DataView dv, ref DataGridViewT grid, string TColumn, string TValue)
        {
            if (String.IsNullOrEmpty(TValue)) return;
            if (!dv.Table.Columns.Contains(TColumn)) return;
            if (dv.Count == 0) return;

            var list = dv.OfType<DataRowView>().AsParallel().ToList();
            var index = list.FindIndex(r => r[TColumn].ToString() == TValue);
            if (index == -1)
            {
                index = list.FindLastIndex(r => String.CompareOrdinal(r[TColumn].ToString(), TValue) > 0);
            }

            if (index == -1) index = 0;
            if (index >= dv.Count) index = dv.Count - 1;

            grid.FirstDisplayedScrollingRowIndex = index;
            grid.CurrentCell = grid[0, index];
            grid.Rows[index].Selected = true;
        }
        public static void SearchForDate(this DataView dv, ref DataGridViewT grid, string Column1, string Value1, string Column2, string Value2)
        {
            if (String.IsNullOrEmpty(Value1)) return;
            if (String.IsNullOrEmpty(Value2)) return;
            if (!dv.Table.Columns.Contains(Column1)) return;
            if (!dv.Table.Columns.Contains(Column2)) return;
            if (dv.Count == 0) return;

            if (Value1.Trim().Length > 0 && Value2.Trim().Length > 0)
            {
                int index = -1;
                var list = dv.OfType<DataRowView>().AsParallel().ToList();
                index = list.FindIndex(r => r[Column1].ToString() == Value1 && r[Column2].ToString() == Value2);

                if (index != -1)
                {
                    grid.FirstDisplayedScrollingRowIndex = index;
                    grid.CurrentCell = grid[0, index];
                    grid.Rows[index].Selected = true;
                }
                else
                {
                    index = list.FindIndex(r => r[Column1].ToString() == Value1);
                    var oi = index;
                    if (index != -1)
                    {
                        for (int i = index; i < dv.Count; i++)
                        {
                            if (dv[i][Column1].ToString() == Value1)
                            {
                                index = i;
                                if (String.CompareOrdinal(Value2, dv[i][Column2].ToString()) > 0 && i > oi)
                                {
                                    index = i - 1;
                                    break;
                                }
                            }
                            else
                            {
                                index = i - 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        index = list.FindLastIndex(r => String.CompareOrdinal(Value1, r[Column1].ToString()) > 0) + 1;
                    }
                    if (index <= 0) index = 0;
                    if (index >= dv.Count) index = dv.Count - 1;
                    grid.FirstDisplayedScrollingRowIndex = index;
                    grid.CurrentCell = grid[0, index];
                    grid.Rows[index].Selected = true;
                }
            }
            else if (Value1.Trim().Length > 0 && Value2.Trim().Length == 0)
            {
                dv.Search(ref grid, Column1, Value1);
            }
            else if (Value1.Trim().Length == 0 && Value2.Trim().Length > 0)
            {
                dv.Search(ref grid, Column2, Value2);
            }
            else
            {
                return;
            }
        }

        //Radio
        public static void SetUserDefineRpt(this RadioButton rd, string ReportName, string report = @"Report\")
        {
            var IsExists = File.Exists(Common.reportaddress + report + ReportName);
            rd.ForeColor = Color.Black;
            rd.Controls.Clear();
            if (!IsExists)
            {
                Panel pnl = new Panel();
                pnl.BackColor = Color.Transparent;
                pnl.Dock = DockStyle.Fill;
                rd.Controls.Add(pnl);
                new ToolTip().SetToolTip(pnl, ReportName);
                rd.ForeColor = Color.Gray;
            }
        }

        public static void 判斷有無CF或RF(this RadioButton rd, string ReportName, string report = @"Report\")
        {

            bool hasReport = false;
            //先判斷有無FastRepot
            var testPath = Common.reportaddress + report.Replace("Report", "ReportNew") + ReportName + ".frx";
            hasReport = File.Exists(testPath);
            if (hasReport == false)
            {
                //在判斷有無水晶報表
                testPath = Common.reportaddress + report + ReportName + ".rpt";
                hasReport = File.Exists(testPath);
            }

            if (hasReport)
            {
                rd.ForeColor = Color.Black;
                rd.Controls.Clear();
            }
            else
            {
                Panel pnl = new Panel();
                pnl.BackColor = Color.Transparent;
                pnl.Dock = DockStyle.Fill;
                rd.Controls.Add(pnl);
                new ToolTip().SetToolTip(pnl, ReportName);
                rd.ForeColor = Color.Gray;
            }
        }
        

        //JE Picturebox
        public static void LoadImage(this PictureBoxT pic, byte[] buffer)
        {
            if (buffer != null && buffer.Length > 0)
            {
                using (Stream stream = new MemoryStream(buffer))
                {
                    pic.Tag = buffer;
                    pic.Image = Image.FromStream(stream);
                }
            }
            else
            {
                pic.Clear();
            }
        }
        public static void LoadImage(this PictureBoxT pic)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.FileName = "";
            file.Filter = "圖片檔 (*.jpg;*.bmp)|*.jpg;*.bmp";

            if (file.ShowDialog() == DialogResult.OK)
            {
                var img = Image.FromFile(file.FileName);
                int max = 200;
                int w = img.Width;
                int h = img.Height;

                if (img.Width > img.Height)
                {
                    if (img.Width > max)
                    {
                        w = max;
                        h = Convert.ToInt32((Convert.ToDouble(max) / Convert.ToDouble(img.Width)) * Convert.ToDouble(img.Height));
                    }
                }
                else
                {
                    if (img.Height > max)
                    {
                        w = Convert.ToInt32((Convert.ToDouble(max) / Convert.ToDouble(img.Height)) * Convert.ToDouble(img.Width));
                        h = max;
                    }
                }

                pic.Image = new Bitmap(img, w, h);
                pic.IsImageChange = true;
            }
            else
            {
                pic.Clear();
            }
        }
        public static void Clear(this PictureBoxT pic)
        {
            if (pic.Image.IsNotNull())
            {
                pic.Image.Dispose();
                pic.Image = null;
                pic.Tag = null;
            }
        }
        public static byte[] ImageToByte(this PictureBoxT pic)
        {
            if (pic.Image == null) return new byte[0];
            if (pic.IsImageChange)
            {
                pic.IsImageChange = false;
                using (MemoryStream stream = new MemoryStream())
                {
                    pic.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return stream.GetBuffer();
                }
            }
            else
            {
                return (byte[])pic.Tag;
            }
        }

        public static bool IsZeroOrEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str)) return true;
            try
            {
                decimal d = Convert.ToDecimal(str);
                if (d == 0)
                    return true;
                else
                    return false;
            }
            catch { return true; }
        }
        public static string GetUTF8(this string str, int Length)
        {
            if (Length < 0) return "";
            int len = 0;
            string s = "";
            for (int i = 1; i <= str.Length; i++)
            {
                s = new string(str.Take(i).ToArray());
                if (Encoding.GetEncoding(950).GetByteCount(s) > Length)
                {
                    break;
                }
                else if (Encoding.GetEncoding(950).GetByteCount(s) == Length)
                {
                    return s;
                }
                else
                {
                    len = i;
                }
            }
            s = new string(str.Take(len).ToArray());
            return s;
        }


        /// <summary>
        /// 隨機找出一筆相對應的值之index，如果全都不相符則傳回-1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static int MultiThreadFindIndex(this DataTable dt, string column1, string value1, string column2 = null, string value2 = null, string column3 = null, string value3 = null, string column4 = null, string value4 = null)
        {
            int index = -1;
            if (column2 == null && column3 == null && column4 == null)
            {
                #region mapping 1
                object lock_ = new object();
                Parallel.For(0, dt.Rows.Count, (i, loopState) =>
                {
                    if (dt.Rows[i][column1].ToString() == value1)
                    {
                        lock (lock_)
                        {
                            index = i;
                            loopState.Stop();
                        }
                    }
                });
                #endregion
            }
            else if (column3 == null && column4 == null)
            {
                #region mapping 2
                object lock_ = new object();
                Parallel.For(0, dt.Rows.Count, (i, loopState) =>
                {
                    if (dt.Rows[i][column1].ToString() == value1 && dt.Rows[i][column2].ToString() == value2)
                    {
                        lock (lock_)
                        {
                            index = i;
                            loopState.Stop();
                        }
                    }
                });
                #endregion
            }
            else if (column4 == null)
            {
                #region mapping 3
                object lock_ = new object();
                Parallel.For(0, dt.Rows.Count, (i, loopState) =>
                {
                    if (dt.Rows[i][column1].ToString() == value1 && dt.Rows[i][column2].ToString() == value2 && dt.Rows[i][column3].ToString() == value3)
                    {
                        lock (lock_)
                        {
                            index = i;
                            loopState.Stop();
                        }
                    }
                });
                #endregion
            }
            else
            {
                #region mapping 4
                object lock_ = new object();
                Parallel.For(0, dt.Rows.Count, (i, loopState) =>
                {
                    if (dt.Rows[i][column1].ToString() == value1 && dt.Rows[i][column2].ToString() == value2 && dt.Rows[i][column3].ToString() == value3 && dt.Rows[i][column4].ToString() == value4)
                    {
                        lock (lock_)
                        {
                            index = i;
                            loopState.Stop();
                        }
                    }
                });
                #endregion
            }
            return index;
        }
        /// <summary>
        /// 傳回符合的資料之List<int>
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="column1"></param>
        /// <param name="value1"></param>
        /// <param name="column2"></param>
        /// <param name="value2"></param>
        /// <param name="column3"></param>
        /// <param name="value3"></param>
        /// <param name="column4"></param>
        /// <param name="value4"></param>
        /// <returns></returns>
        public static List<int> FindIndexToList(this DataTable dt, string column1, string value1, string column2 = null, string value2 = null, string column3 = null, string value3 = null, string column4 = null, string value4 = null)
        {
            List<int> ListIndex = new List<int>();
            if (column2 == null && column3 == null && column4 == null)
            {
                #region mapping 1
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][column1].ToString() == value1)
                    {
                        ListIndex.Add(i);
                    }
                }
                #endregion
            }
            else if (column3 == null && column4 == null)
            {
                #region mapping 2
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][column1].ToString() == value1 && dt.Rows[i][column2].ToString() == value2)
                    {
                        ListIndex.Add(i);
                    }
                }
                #endregion
            }
            else if (column4 == null)
            {
                #region mapping 3
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][column1].ToString() == value1 && dt.Rows[i][column2].ToString() == value2 && dt.Rows[i][column3].ToString() == value3)
                    {
                        ListIndex.Add(i);
                    }
                }
                #endregion
            }
            else
            {
                #region mapping 4
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][column1].ToString() == value1 && dt.Rows[i][column2].ToString() == value2 && dt.Rows[i][column3].ToString() == value3 && dt.Rows[i][column4].ToString() == value4)
                    {
                        ListIndex.Add(i);
                    }
                }
                #endregion
            }
            return ListIndex;
        }


        public static string takeString(this string str, int Length)
        {
            if (str == null) str = "";
            return new string(str.Take(Length).ToArray());
        }
        public static string skipString(this string str, int Length)
        {
            if (str == null) str = "";
            return new string(str.Skip(Length).ToArray());
        }
        public static bool BigThen(this string str, string str1)
        {
            if (str.Trim().Length > 0 && str1.Trim().Length > 0)
            {
                return string.CompareOrdinal(str.Trim(), str1.Trim()) > 0;
            }
            else return false;
        }

        public static bool IsNullOrEmpty(this object obj)
        {
            bool IsNullorEmpty = false;
            if (obj == null)
                IsNullorEmpty = true;
            else
            {
                try
                {
                    if (obj.ToString().Trim() == "")
                    {
                        obj = "";
                        IsNullorEmpty = true;
                    }
                    else
                        IsNullorEmpty = false;
                }
                catch
                {
                    IsNullorEmpty = true;
                }
            }
            return IsNullorEmpty;
        }
        public static decimal ToDecimal(this object obj)
        {
            if (obj == null) return 0;
            decimal d = 0;
            decimal.TryParse(obj.ToString(), out d);
            return d;
        }
        public static decimal ToDecimal(this object obj, string fn)
        {
            decimal d = 0;
            if (obj == null) return d.ToString(fn).ToDecimal();
            else
            {
                decimal.TryParse(obj.ToString(), out d);
                return d.ToString(fn).ToDecimal();
            }
        }
        public static int ToInteger(this object obj)
        {
            return (int)obj.ToDecimal();
        }

        static List<TextBoxbase> list = new List<TextBoxbase>();
        static void EnumMember(this Form frm)
        {
            list.Clear();
            foreach (Control c in frm.Controls)
            {
                if (c is TextBoxbase) list.Add((TextBoxbase)c);
                if (c.HasChildren) EnumMember(c);
            }
        }
        static void EnumMember(Control c)
        {
            foreach (Control c1 in c.Controls)
            {
                if (c1 is TextBoxbase) list.Add((TextBoxbase)c1);
                if (c1.HasChildren) EnumMember(c1);
            }
        }
        public static List<TextBoxbase> getEnumMember(this Form frm)
        {
            frm.EnumMember();
            return list.ToArray().ToList();
        }

        public static void Excel匯出並開啟(this DataTable dt, string FileName)
        {
            try
            {
                string FilePath = Application.StartupPath + "\\temp\\" + FileName + ".xls";
                JBS.ResloveCrystalReportExcel excel解析 = new JBS.ResloveCrystalReportExcel();
                excel解析.ExportExcelDt(dt, FilePath, true);
                while (!System.IO.File.Exists(FilePath)) { }
                System.Diagnostics.Process.Start(FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
