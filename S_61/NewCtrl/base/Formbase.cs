using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using S_61.Basic;

namespace JE.MyControl
{
    public enum FormStyle : int
    {
        Max, Mini, VeryMini
    }
    public class Formbase : Form
    {
        public FormStyle Style;
        public bool CanChangeSize { get; set; }
        //[Obsolete("Don't use this", true)]
        public string SeekNo = "";
        FormEditState state = FormEditState.None;

        List<Control> AllControl = new List<Control>();
        List<Control> AllCtrlTemp = new List<Control>();

        public Formbase()
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormState = FormEditState.None;
            this.StartPosition = FormStartPosition.Manual;
            //限制要None的狀態下才可以按右上角的X關閉
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.Formbase_FormClosing);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Formbase_FormClosing);
        }

        public new Boolean KeyPreview
        {
            get { return true; }
        }

        public FormEditState FormState
        {
            get
            {
                return this.state;
            }
            set
            {
                state = value;
                if (state == FormEditState.Append)
                {
                    var btnAppend = this.FindControl("btnAppend");
                    if (btnAppend == null) return;
                    var p = btnAppend.Parent;
                    foreach (Button btn in p.Controls.OfType<Button>())
                    {
                        if (btn.Name.ToLower() == "btnsave") btn.Enabled = true;
                        else if (btn.Name.ToLower() == "btncancel") btn.Enabled = true;
                        else btn.Enabled = false;
                    }
                }
                else if (state == FormEditState.Duplicate)
                {
                    var btnAppend = this.FindControl("btnAppend");
                    if (btnAppend == null) return;
                    var p = btnAppend.Parent;
                    foreach (Button btn in p.Controls.OfType<Button>())
                    {
                        if (btn.Name.ToLower() == "btnsave") btn.Enabled = true;
                        else if (btn.Name.ToLower() == "btncancel") btn.Enabled = true;
                        else btn.Enabled = false;
                    }
                }
                else if (state == FormEditState.Modify)
                {
                    var btnModify = this.FindControl("btnModify");
                    if (btnModify == null) return;
                    var p = btnModify.Parent;
                    foreach (Button btn in p.Controls.OfType<Button>())
                    {
                        if (btn.Name.ToLower() == "btnsave") btn.Enabled = true;
                        else if (btn.Name.ToLower() == "btncancel") btn.Enabled = true;
                        else btn.Enabled = false;
                    }
                }
                else if (state == FormEditState.None)
                {
                    var btnModify = this.FindControl("btnCancel");
                    if (btnModify == null) return;
                    var p = btnModify.Parent;
                    foreach (Button btn in p.Controls.OfType<Button>())
                    {
                        if (btn.Name.ToLower() == "btnsave") btn.Enabled = false;
                        else if (btn.Name.ToLower() == "btncancel") btn.Enabled = false;
                        else btn.Enabled = true;
                    }
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (JEInitialize.IsRunTime)
            {
                this.Location = new Point(1, 1);
                AllControl = this.Controls.OfType<Control>().ToList();
                GetDesignPattern(AllControl.ToArray(), AllControl.Count);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (!JEInitialize.IsRunTime)
            {
                //190125 lillian mark
                //if (this.Width != 1016 || this.Height != 676)
                //{
                //   // this.Size = new Size(1016, 676);lillian

                    this.Size = new Size(1920, 1080);//190125 illian 
                //}
            }

        }

        public void GetDesignPattern(Control[] lt, int count)
        {
            AllCtrlTemp.AddRange(lt);
            AllControl.Clear();
            int i = 0;
            foreach (Control c in lt)
            {
                c.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                if (c is LabelT) ((LabelT)c).DesignPattern();
                else if (c is TextBoxT_MaxlenthNotEqualWidth)    
                    ((TextBoxT_MaxlenthNotEqualWidth)c).DesignPattern();
                else if (c is LabelMenuT) ((LabelMenuT)c).DesignPattern();
                else if (c is GroupBoxT) ((GroupBoxT)c).DesignPattern();
                else if (c is ButtonSmallT) ((ButtonSmallT)c).DesignPattern();
                else if (c is ButtonGridT) ((ButtonGridT)c).DesignPattern();
                else if (c is RadioT) ((RadioT)c).DesignPattern();
                else if (c is PanelT) ((PanelT)c).DesignPattern();
                else if (c is PanelNT) ((PanelNT)c).DesignPattern();
                else if (c is PanelBtnT) ((PanelBtnT)c).DesignPattern();
                else if (c is TabControlT) ((TabControlT)c).DesignPattern();
                else if (c is TabPage) ((TabPage)c).DesignPattern();
                else if (c is TextBoxT) ((TextBoxT)c).DesignPattern();
                else if (c is TextBoxNumberT) ((TextBoxNumberT)c).DesignPattern();
                else if (c is DataGridViewT) ((DataGridViewT)c).DesignPattern();
                else if (c is DataGridViewbaseTwo) ((DataGridViewbaseTwo)c).DesignPattern();
                else if (c is TreeViewT) ((TreeViewT)c).DesignPattern();
                else if (c is RichTextBoxT) ((RichTextBoxT)c).DesignPattern();
                else if (c is PictureBoxT) ((PictureBoxT)c).DesignPattern();
                else if (c is CheckBoxT) ((CheckBoxT)c).DesignPattern();
                else if (c is ComboBoxT) ((ComboBoxT)c).DesignPattern();
                else if (c is TextBoxTdesp) ((TextBoxTdesp)c).DesignPattern();

                i++;
                if (c.HasChildren)
                {
                    AllControl.AddRange(c.Controls.OfType<Control>());
                }
                if (i == count) GetDesignPattern(AllControl.ToArray(), AllControl.Count);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (JEInitialize.IsRunTime)
            {
                this.SuspendLayout();
                this.ShowInit();
                foreach (Control c in AllCtrlTemp)
                {
                    if (c is LabelT) ((LabelT)c).ResetLocation();
                    else if (c is TextBoxT_MaxlenthNotEqualWidth)
                        ((TextBoxT_MaxlenthNotEqualWidth)c).ResetLocation();
                    else if (c is LabelMenuT) ((LabelMenuT)c).ResetLocation();
                    else if (c is GroupBoxT) ((GroupBoxT)c).ResetLocation();
                    else if (c is ButtonSmallT) ((ButtonSmallT)c).ResetLocation();
                    else if (c is ButtonGridT) ((ButtonGridT)c).ResetLocation();
                    else if (c is RadioT) ((RadioT)c).ResetLocation();
                    else if (c is PanelT) ((PanelT)c).ResetLocation();
                    else if (c is PanelNT) ((PanelNT)c).ResetLocation();
                    else if (c is PanelBtnT) ((PanelBtnT)c).ResetLocation();
                    else if (c is TabControlT) ((TabControlT)c).ResetLocation();
                    else if (c is TabPage) ((TabPage)c).ResetLocation();
                    else if (c is TextBoxT) ((TextBoxT)c).ResetLocation();
                    else if (c is TextBoxNumberT) ((TextBoxNumberT)c).ResetLocation();
                    else if (c is DataGridViewT) ((DataGridViewT)c).ResetLocation();
                    else if (c is DataGridViewbaseTwo) ((DataGridViewbaseTwo)c).ResetLocation();
                    else if (c is TreeViewT) ((TreeViewT)c).ResetLocation();
                    else if (c is RichTextBoxT) ((RichTextBoxT)c).ResetLocation();
                    else if (c is PictureBoxT) ((PictureBoxT)c).ResetLocation();
                    else if (c is CheckBoxT) ((CheckBoxT)c).ResetLocation();
                    else if (c is ComboBoxT) ((ComboBoxT)c).ResetLocation();
                    else if (c is TextBoxTdesp) ((TextBoxTdesp)c).ResetLocation();
                    
                }

                if (this.Parent != null)
                {
                    this.Size = S_61.MainForm.menu.Size;
                }


                if (CanChangeSize)
                {
                    this.MinimizeBox = true;
                    this.MaximizeBox = false;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.AutoScroll = true;
                }


                if (!this.Modal) this.Location = new Point(1, 1);
                else
                {
                    this.Location = new Point(5, (S_61.MainForm.main.Height - this.Height) / 2);
                }
                this.ResumeLayout();
                if (CanChangeSize)
                {
                    this.VerticalScroll.Value = 0;
                    this.VerticalScroll.Value = 0;
                }

            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible == false)
            {
                try
                {
                    ToolStripMenuItem menu7 = (ToolStripMenuItem)S_61.MainForm.main.MainMenuStrip.Items["menu7"];
                    for (int i = 0; i < menu7.DropDownItems.Count; i++)
                    {
                        var name = menu7.DropDownItems[i].Name;
                        if (S_61.MainForm.main.MdiChildren.Any(fm => fm.Name == name) == false)
                        {
                            menu7.DropDownItems.RemoveAt(i);
                        }
                    }
                }
                catch { }
            }
            base.OnVisibleChanged(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Common.keyData = keyData;
            if (keyData == Keys.Up)
            {
                var p = ActiveControl.Parent.Parent;
                if (p.IsNotNull() && p is DataGridViewT)
                {
                    DataGridViewT t = ((DataGridViewT)p);
                    var index = t.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (index == 0)
                    {
                        SelectNextControl(t, false, true, true, true);
                        ActiveControl.Focus();
                        if (ActiveControl is TextBox) ((TextBox)ActiveControl).SelectAll();
                    }
                }
                else if (ActiveControl is DataGridViewT)
                {
                    var index = ((DataGridViewT)ActiveControl).Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (index == 0)
                    {
                        SelectNextControl(ActiveControl, false, true, true, true);
                        ActiveControl.Focus();
                        if (ActiveControl is TextBox) ((TextBox)ActiveControl).SelectAll();
                    }
                    else if (((DataGridViewT)ActiveControl).Rows.Count == 0)
                    {
                        SelectNextControl(ActiveControl, false, true, true, true);
                        ActiveControl.Focus();
                        if (ActiveControl is TextBox) ((TextBox)ActiveControl).SelectAll();
                    }
                }
                else
                {
                    SelectNextControl(ActiveControl, false, true, true, true);
                }
            }
            else if (keyData == Keys.Down)
            {
                var p = ActiveControl.Parent.Parent;
                if (p.IsNotNull() && p is DataGridViewT)
                {
                    DataGridViewT t = ((DataGridViewT)p);
                    var index = t.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (index == t.Rows.Count - 1)
                    {
                        SelectNextControl(t, true, true, true, true);
                        ActiveControl.Focus();
                        if (ActiveControl is TextBox) ((TextBox)ActiveControl).SelectAll();
                    }
                }
                else if (ActiveControl is DataGridViewT)
                {
                    var index = ((DataGridViewT)ActiveControl).Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    if (index == ((DataGridViewT)ActiveControl).Rows.Count - 1)
                    {
                        SelectNextControl(ActiveControl, true, true, true, true);
                        ActiveControl.Focus();
                        if (ActiveControl is TextBox) ((TextBox)ActiveControl).SelectAll();
                    }
                }
                else
                {
                    SelectNextControl(ActiveControl, true, true, true, true);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
          
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void OpemInfoFrom<T>(Func<T> init) where T : Formbase
        {
            //先檢查是否有此窗
            var name = typeof(T).Name;
            var from = S_61.MainForm.main.MdiChildren.FirstOrDefault(r => r.GetType().Name == name);
            if (from != null)
            {
                from.Dispose();
            }

            T t = init();
            t.CanChangeSize = true;
            t.Name = name;
            t.MdiParent = S_61.MainForm.main;
            t.Show();

            //加入[視窗]list,並設定click事件
            AddOption(t.Text, name);
            
            //關閉from後，移除[視窗]list
            t.VisibleChanged -= new EventHandler(ThisClosing);
            t.VisibleChanged += new EventHandler(ThisClosing);   
        }

        private void AddOption(string text, string name)
        {
            var it7 = (ToolStripMenuItem)S_61.MainForm.main.MainMenuStrip.Items["menu7"];
            var items = it7.DropDownItems;
            if (!items.ContainsKey(name))
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = name;
                item.Text = text + "[報表]";

                item.Click -= new EventHandler(item_Click);
                item.Click += new EventHandler(item_Click);

                items.Add(item);
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            var name = ((ToolStripMenuItem)sender).Name;
            var form = S_61.MainForm.main.MdiChildren.FirstOrDefault(fm => fm.GetType().Name == name);

            if (form == null)
                return;

            //form.Location = new Point(1, 1);
            form.WindowState = FormWindowState.Normal;
            form.BringToFront();
        }

        void ThisClosing(object sender, EventArgs e)
        {
            this.RemoveOption(((Form)sender).Name);
        }

        void RemoveOption(string name)
        {
            if (S_61.MainForm.main == null)
                return;

            var it7 = (ToolStripMenuItem)S_61.MainForm.main.MainMenuStrip.Items["menu7"];
            var items = it7.DropDownItems;
            if (items.ContainsKey(name))
            {
                items.RemoveByKey(name);
            }
        }
        public string ShowDialogCallBack()
        {
            this.ShowDialog();
            return SeekNo;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Formbase
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "Formbase";
            this.ResumeLayout(false);
        }
      
        public void Formbase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormState == FormEditState.None)
             {
                 e.Cancel = false;
             }
             else
             {
                 e.Cancel = true;
             }
        }
    }
}
