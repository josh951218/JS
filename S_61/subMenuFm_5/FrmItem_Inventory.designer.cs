namespace S_61.SOther
{
    partial class FrmItem_Inventory
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItem_Inventory));
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radio5 = new JE.MyControl.RadioT();
            this.radio4 = new JE.MyControl.RadioT();
            this.radio2 = new JE.MyControl.RadioT();
            this.radio3 = new JE.MyControl.RadioT();
            this.radio1 = new JE.MyControl.RadioT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.checkBoxT1_含寄庫 = new JE.MyControl.CheckBoxT();
            this.enddate = new JE.MyControl.TextBoxT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.KiNo1 = new JE.MyControl.TextBoxT();
            this.KiNo = new JE.MyControl.TextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.StNo = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.ItNo = new JE.MyControl.TextBoxT();
            this.ItNo1 = new JE.MyControl.TextBoxT();
            this.StNo1 = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.groupBoxT3 = new JE.MyControl.GroupBoxT();
            this.ch4 = new JE.MyControl.CheckBoxT();
            this.ch1 = new JE.MyControl.CheckBoxT();
            this.ch3 = new JE.MyControl.CheckBoxT();
            this.ch2 = new JE.MyControl.CheckBoxT();
            this.groupBox1 = new JE.MyControl.GroupBoxT();
            this.radio8 = new JE.MyControl.RadioT();
            this.radio9 = new JE.MyControl.RadioT();
            this.groupBoxT4 = new JE.MyControl.GroupBoxT();
            this.radio6 = new JE.MyControl.RadioT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.rdAvgByOneStk = new JE.MyControl.RadioT();
            this.rdAvgByAllStk = new JE.MyControl.RadioT();
            this.groupBoxT1.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.groupBoxT3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxT4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radio5);
            this.groupBoxT1.Controls.Add(this.radio4);
            this.groupBoxT1.Controls.Add(this.radio2);
            this.groupBoxT1.Controls.Add(this.radio3);
            this.groupBoxT1.Controls.Add(this.radio1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(176, 45);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(671, 63);
            this.groupBoxT1.TabIndex = 3;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radio5
            // 
            this.radio5.AutoSize = true;
            this.radio5.BackColor = System.Drawing.Color.Transparent;
            this.radio5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio5.Font = new System.Drawing.Font("細明體", 12F);
            this.radio5.Location = new System.Drawing.Point(486, 28);
            this.radio5.Margin = new System.Windows.Forms.Padding(0);
            this.radio5.Name = "radio5";
            this.radio5.Size = new System.Drawing.Size(154, 20);
            this.radio5.TabIndex = 4;
            this.radio5.Tag = "明細表(內含字元)";
            this.radio5.Text = "明細表(內含字元)";
            this.radio5.UseVisualStyleBackColor = true;
            this.radio5.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // radio4
            // 
            this.radio4.AutoSize = true;
            this.radio4.BackColor = System.Drawing.Color.Transparent;
            this.radio4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio4.Font = new System.Drawing.Font("細明體", 12F);
            this.radio4.Location = new System.Drawing.Point(372, 28);
            this.radio4.Margin = new System.Windows.Forms.Padding(0);
            this.radio4.Name = "radio4";
            this.radio4.Size = new System.Drawing.Size(90, 20);
            this.radio4.TabIndex = 3;
            this.radio4.Tag = "歷史總表";
            this.radio4.Text = "歷史總表";
            this.radio4.UseVisualStyleBackColor = true;
            this.radio4.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // radio2
            // 
            this.radio2.AutoSize = true;
            this.radio2.BackColor = System.Drawing.Color.Transparent;
            this.radio2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio2.Font = new System.Drawing.Font("細明體", 12F);
            this.radio2.Location = new System.Drawing.Point(128, 28);
            this.radio2.Margin = new System.Windows.Forms.Padding(0);
            this.radio2.Name = "radio2";
            this.radio2.Size = new System.Drawing.Size(90, 20);
            this.radio2.TabIndex = 1;
            this.radio2.Tag = "現有總表";
            this.radio2.Text = "現有總表";
            this.radio2.UseVisualStyleBackColor = true;
            this.radio2.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // radio3
            // 
            this.radio3.AutoSize = true;
            this.radio3.BackColor = System.Drawing.Color.Transparent;
            this.radio3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio3.Font = new System.Drawing.Font("細明體", 12F);
            this.radio3.Location = new System.Drawing.Point(242, 28);
            this.radio3.Margin = new System.Windows.Forms.Padding(0);
            this.radio3.Name = "radio3";
            this.radio3.Size = new System.Drawing.Size(106, 20);
            this.radio3.TabIndex = 2;
            this.radio3.Tag = "歷史明細表";
            this.radio3.Text = "歷史明細表";
            this.radio3.UseVisualStyleBackColor = true;
            this.radio3.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // radio1
            // 
            this.radio1.AutoSize = true;
            this.radio1.BackColor = System.Drawing.Color.LightBlue;
            this.radio1.Checked = true;
            this.radio1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio1.Font = new System.Drawing.Font("細明體", 12F);
            this.radio1.Location = new System.Drawing.Point(30, 28);
            this.radio1.Margin = new System.Windows.Forms.Padding(0);
            this.radio1.Name = "radio1";
            this.radio1.Size = new System.Drawing.Size(74, 20);
            this.radio1.TabIndex = 0;
            this.radio1.Tag = "明細表";
            this.radio1.Text = "明細表";
            this.radio1.UseVisualStyleBackColor = true;
            this.radio1.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.checkBoxT1_含寄庫);
            this.pnlBoxT1.Controls.Add(this.enddate);
            this.pnlBoxT1.Controls.Add(this.lblT5);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.lblT6);
            this.pnlBoxT1.Controls.Add(this.KiNo1);
            this.pnlBoxT1.Controls.Add(this.KiNo);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.StNo);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.lblT7);
            this.pnlBoxT1.Controls.Add(this.ItNo);
            this.pnlBoxT1.Controls.Add(this.ItNo1);
            this.pnlBoxT1.Controls.Add(this.StNo1);
            this.pnlBoxT1.Location = new System.Drawing.Point(176, 276);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(671, 250);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // checkBoxT1_含寄庫
            // 
            this.checkBoxT1_含寄庫.AutoSize = true;
            this.checkBoxT1_含寄庫.BackColor = System.Drawing.Color.LightBlue;
            this.checkBoxT1_含寄庫.Checked = true;
            this.checkBoxT1_含寄庫.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxT1_含寄庫.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxT1_含寄庫.Font = new System.Drawing.Font("細明體", 12F);
            this.checkBoxT1_含寄庫.Location = new System.Drawing.Point(160, 24);
            this.checkBoxT1_含寄庫.Name = "checkBoxT1_含寄庫";
            this.checkBoxT1_含寄庫.Size = new System.Drawing.Size(155, 20);
            this.checkBoxT1_含寄庫.TabIndex = 4;
            this.checkBoxT1_含寄庫.Text = "含寄庫與領庫數量";
            this.checkBoxT1_含寄庫.UseVisualStyleBackColor = true;
            // 
            // enddate
            // 
            this.enddate.AllowGrayBackColor = false;
            this.enddate.AllowResize = true;
            this.enddate.BackColor = System.Drawing.Color.White;
            this.enddate.Font = new System.Drawing.Font("細明體", 12F);
            this.enddate.Location = new System.Drawing.Point(295, 204);
            this.enddate.MaxLength = 10;
            this.enddate.Name = "enddate";
            this.enddate.oLen = 0;
            this.enddate.Size = new System.Drawing.Size(87, 27);
            this.enddate.TabIndex = 7;
            this.enddate.Visible = false;
            this.enddate.Layout += new System.Windows.Forms.LayoutEventHandler(this.enddate_Layout);
            this.enddate.Validating += new System.ComponentModel.CancelEventHandler(this.enddate_Validating);
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(168, 209);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(96, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "截 止 日 期";
            this.lblT5.Visible = false;
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(168, 172);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(96, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "產 品 類 別";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(385, 172);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(24, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "～";
            // 
            // KiNo1
            // 
            this.KiNo1.AllowGrayBackColor = false;
            this.KiNo1.AllowResize = true;
            this.KiNo1.BackColor = System.Drawing.Color.White;
            this.KiNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.KiNo1.Location = new System.Drawing.Point(421, 167);
            this.KiNo1.MaxLength = 4;
            this.KiNo1.Name = "KiNo1";
            this.KiNo1.oLen = 0;
            this.KiNo1.Size = new System.Drawing.Size(39, 27);
            this.KiNo1.TabIndex = 6;
            this.KiNo1.DoubleClick += new System.EventHandler(this.KiNo_Click);
            // 
            // KiNo
            // 
            this.KiNo.AllowGrayBackColor = false;
            this.KiNo.AllowResize = true;
            this.KiNo.BackColor = System.Drawing.Color.White;
            this.KiNo.Font = new System.Drawing.Font("細明體", 12F);
            this.KiNo.Location = new System.Drawing.Point(295, 167);
            this.KiNo.MaxLength = 4;
            this.KiNo.Name = "KiNo";
            this.KiNo.oLen = 0;
            this.KiNo.Size = new System.Drawing.Size(39, 27);
            this.KiNo.TabIndex = 5;
            this.KiNo.DoubleClick += new System.EventHandler(this.KiNo_Click);
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(160, 135);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "起訖倉庫編號";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(160, 98);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "結束產品編號";
            // 
            // StNo
            // 
            this.StNo.AllowGrayBackColor = false;
            this.StNo.AllowResize = true;
            this.StNo.BackColor = System.Drawing.Color.White;
            this.StNo.Font = new System.Drawing.Font("細明體", 12F);
            this.StNo.Location = new System.Drawing.Point(295, 130);
            this.StNo.MaxLength = 10;
            this.StNo.Name = "StNo";
            this.StNo.oLen = 0;
            this.StNo.Size = new System.Drawing.Size(87, 27);
            this.StNo.TabIndex = 3;
            this.StNo.DoubleClick += new System.EventHandler(this.StNo_Click);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(160, 61);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(104, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "開始產品編號";
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(385, 135);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(24, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "～";
            // 
            // ItNo
            // 
            this.ItNo.AllowGrayBackColor = false;
            this.ItNo.AllowResize = true;
            this.ItNo.BackColor = System.Drawing.Color.White;
            this.ItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo.Location = new System.Drawing.Point(295, 56);
            this.ItNo.MaxLength = 20;
            this.ItNo.Name = "ItNo";
            this.ItNo.oLen = 0;
            this.ItNo.Size = new System.Drawing.Size(167, 27);
            this.ItNo.TabIndex = 1;
            this.ItNo.DoubleClick += new System.EventHandler(this.ItNo_Click);
            // 
            // ItNo1
            // 
            this.ItNo1.AllowGrayBackColor = false;
            this.ItNo1.AllowResize = true;
            this.ItNo1.BackColor = System.Drawing.Color.White;
            this.ItNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo1.Location = new System.Drawing.Point(295, 93);
            this.ItNo1.MaxLength = 20;
            this.ItNo1.Name = "ItNo1";
            this.ItNo1.oLen = 0;
            this.ItNo1.Size = new System.Drawing.Size(167, 27);
            this.ItNo1.TabIndex = 2;
            this.ItNo1.DoubleClick += new System.EventHandler(this.ItNo_Click);
            // 
            // StNo1
            // 
            this.StNo1.AllowGrayBackColor = false;
            this.StNo1.AllowResize = true;
            this.StNo1.BackColor = System.Drawing.Color.White;
            this.StNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.StNo1.Location = new System.Drawing.Point(421, 130);
            this.StNo1.MaxLength = 10;
            this.StNo1.Name = "StNo1";
            this.StNo1.oLen = 0;
            this.StNo1.Size = new System.Drawing.Size(87, 27);
            this.StNo1.TabIndex = 4;
            this.StNo1.DoubleClick += new System.EventHandler(this.StNo_Click);
            // 
            // panelT1
            // 
            this.panelT1.BackColor = System.Drawing.Color.Transparent;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(431, 542);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 79);
            this.panelT1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 5F);
            this.btnExit.Location = new System.Drawing.Point(69, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 2;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnBrow
            // 
            this.btnBrow.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrow.BackgroundImage")));
            this.btnBrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrow.Font = new System.Drawing.Font("細明體", 5F);
            this.btnBrow.Location = new System.Drawing.Point(0, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 1;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
            // 
            // groupBoxT3
            // 
            this.groupBoxT3.Controls.Add(this.ch4);
            this.groupBoxT3.Controls.Add(this.ch1);
            this.groupBoxT3.Controls.Add(this.ch3);
            this.groupBoxT3.Controls.Add(this.ch2);
            this.groupBoxT3.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT3.Location = new System.Drawing.Point(176, 198);
            this.groupBoxT3.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT3.Name = "groupBoxT3";
            this.groupBoxT3.Size = new System.Drawing.Size(671, 63);
            this.groupBoxT3.TabIndex = 6;
            this.groupBoxT3.TabStop = false;
            this.groupBoxT3.Text = "倉庫類別";
            // 
            // ch4
            // 
            this.ch4.AutoSize = true;
            this.ch4.BackColor = System.Drawing.Color.LightBlue;
            this.ch4.Checked = true;
            this.ch4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ch4.Font = new System.Drawing.Font("細明體", 12F);
            this.ch4.Location = new System.Drawing.Point(494, 28);
            this.ch4.Name = "ch4";
            this.ch4.Size = new System.Drawing.Size(75, 20);
            this.ch4.TabIndex = 3;
            this.ch4.Text = "借入倉";
            this.ch4.UseVisualStyleBackColor = true;
            // 
            // ch1
            // 
            this.ch1.AutoSize = true;
            this.ch1.BackColor = System.Drawing.Color.LightBlue;
            this.ch1.Checked = true;
            this.ch1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ch1.Font = new System.Drawing.Font("細明體", 12F);
            this.ch1.Location = new System.Drawing.Point(101, 28);
            this.ch1.Name = "ch1";
            this.ch1.Size = new System.Drawing.Size(75, 20);
            this.ch1.TabIndex = 0;
            this.ch1.Text = "庫存倉";
            this.ch1.UseVisualStyleBackColor = true;
            // 
            // ch3
            // 
            this.ch3.AutoSize = true;
            this.ch3.BackColor = System.Drawing.Color.LightBlue;
            this.ch3.Checked = true;
            this.ch3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ch3.Font = new System.Drawing.Font("細明體", 12F);
            this.ch3.Location = new System.Drawing.Point(363, 28);
            this.ch3.Name = "ch3";
            this.ch3.Size = new System.Drawing.Size(75, 20);
            this.ch3.TabIndex = 2;
            this.ch3.Text = "加工倉";
            this.ch3.UseVisualStyleBackColor = true;
            // 
            // ch2
            // 
            this.ch2.AutoSize = true;
            this.ch2.BackColor = System.Drawing.Color.LightBlue;
            this.ch2.Checked = true;
            this.ch2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ch2.Font = new System.Drawing.Font("細明體", 12F);
            this.ch2.Location = new System.Drawing.Point(232, 28);
            this.ch2.Name = "ch2";
            this.ch2.Size = new System.Drawing.Size(75, 20);
            this.ch2.TabIndex = 1;
            this.ch2.Text = "借出倉";
            this.ch2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio8);
            this.groupBox1.Controls.Add(this.radio9);
            this.groupBox1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBox1.Location = new System.Drawing.Point(176, 118);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 63);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "庫存量為零";
            // 
            // radio8
            // 
            this.radio8.AutoSize = true;
            this.radio8.BackColor = System.Drawing.Color.Transparent;
            this.radio8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio8.Font = new System.Drawing.Font("細明體", 12F);
            this.radio8.Location = new System.Drawing.Point(128, 28);
            this.radio8.Margin = new System.Windows.Forms.Padding(0);
            this.radio8.Name = "radio8";
            this.radio8.Size = new System.Drawing.Size(58, 20);
            this.radio8.TabIndex = 0;
            this.radio8.Tag = "顯示";
            this.radio8.Text = "顯示";
            this.radio8.UseVisualStyleBackColor = true;
            this.radio8.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // radio9
            // 
            this.radio9.AutoSize = true;
            this.radio9.BackColor = System.Drawing.Color.LightBlue;
            this.radio9.Checked = true;
            this.radio9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio9.Font = new System.Drawing.Font("細明體", 12F);
            this.radio9.Location = new System.Drawing.Point(31, 28);
            this.radio9.Margin = new System.Windows.Forms.Padding(0);
            this.radio9.Name = "radio9";
            this.radio9.Size = new System.Drawing.Size(74, 20);
            this.radio9.TabIndex = 1;
            this.radio9.Tag = "不顯示";
            this.radio9.Text = "不顯示";
            this.radio9.UseVisualStyleBackColor = true;
            this.radio9.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // groupBoxT4
            // 
            this.groupBoxT4.Controls.Add(this.rdAvgByOneStk);
            this.groupBoxT4.Controls.Add(this.rdAvgByAllStk);
            this.groupBoxT4.Controls.Add(this.radio6);
            this.groupBoxT4.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT4.Location = new System.Drawing.Point(408, 118);
            this.groupBoxT4.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT4.Name = "groupBoxT4";
            this.groupBoxT4.Size = new System.Drawing.Size(439, 63);
            this.groupBoxT4.TabIndex = 5;
            this.groupBoxT4.TabStop = false;
            this.groupBoxT4.Text = "成本來源";
            // 
            // radio6
            // 
            this.radio6.AutoSize = true;
            this.radio6.BackColor = System.Drawing.Color.Transparent;
            this.radio6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio6.Enabled = false;
            this.radio6.Font = new System.Drawing.Font("細明體", 12F);
            this.radio6.Location = new System.Drawing.Point(23, 27);
            this.radio6.Name = "radio6";
            this.radio6.Size = new System.Drawing.Size(90, 20);
            this.radio6.TabIndex = 0;
            this.radio6.Tag = "標準成本";
            this.radio6.Text = "標準成本";
            this.radio6.UseVisualStyleBackColor = true;
            this.radio6.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(954, 20);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatusLabel3.Text = "插入";
            // 
            // rdAvgByOneStk
            // 
            this.rdAvgByOneStk.AutoSize = true;
            this.rdAvgByOneStk.BackColor = System.Drawing.Color.Transparent;
            this.rdAvgByOneStk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdAvgByOneStk.Font = new System.Drawing.Font("細明體", 12F);
            this.rdAvgByOneStk.Location = new System.Drawing.Point(288, 27);
            this.rdAvgByOneStk.Name = "rdAvgByOneStk";
            this.rdAvgByOneStk.Size = new System.Drawing.Size(138, 20);
            this.rdAvgByOneStk.TabIndex = 3;
            this.rdAvgByOneStk.Tag = "各倉月平均成本";
            this.rdAvgByOneStk.Text = "各倉月平均成本";
            this.rdAvgByOneStk.UseVisualStyleBackColor = false;
            // 
            // rdAvgByAllStk
            // 
            this.rdAvgByAllStk.AutoSize = true;
            this.rdAvgByAllStk.BackColor = System.Drawing.Color.LightBlue;
            this.rdAvgByAllStk.Checked = true;
            this.rdAvgByAllStk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdAvgByAllStk.Font = new System.Drawing.Font("細明體", 12F);
            this.rdAvgByAllStk.Location = new System.Drawing.Point(131, 27);
            this.rdAvgByAllStk.Name = "rdAvgByAllStk";
            this.rdAvgByAllStk.Size = new System.Drawing.Size(138, 20);
            this.rdAvgByAllStk.TabIndex = 2;
            this.rdAvgByAllStk.Tag = "全倉月平均成本";
            this.rdAvgByAllStk.Text = "全倉月平均成本";
            this.rdAvgByAllStk.UseVisualStyleBackColor = false;
            // 
            // FrmItem_Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.groupBoxT4);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.groupBoxT3);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmItem_Inventory";
            this.Tag = "現在-歷史庫存表";
            this.Text = "產品現有歷史庫存表查詢";
            this.Load += new System.EventHandler(this.FrmItem_Inventory_Load);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.groupBoxT3.ResumeLayout(false);
            this.groupBoxT3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxT4.ResumeLayout(false);
            this.groupBoxT4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.GroupBoxT groupBoxT3;
        private JE.MyControl.CheckBoxT ch3;
        private JE.MyControl.CheckBoxT ch2;
        private JE.MyControl.CheckBoxT ch1;
        private JE.MyControl.CheckBoxT ch4;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.TextBoxT enddate;
        public JE.MyControl.TextBoxT ItNo;
        public JE.MyControl.TextBoxT ItNo1;
        public JE.MyControl.TextBoxT StNo;
        public JE.MyControl.TextBoxT StNo1;
        public JE.MyControl.TextBoxT KiNo;
        public JE.MyControl.TextBoxT KiNo1;
        public JE.MyControl.RadioT radio5;
        public JE.MyControl.RadioT radio4;
        public JE.MyControl.RadioT radio3;
        public JE.MyControl.RadioT radio2;
        public JE.MyControl.RadioT radio1;
        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private JE.MyControl.GroupBoxT groupBox1;
        private JE.MyControl.RadioT radio9;
        private JE.MyControl.RadioT radio8;
        private JE.MyControl.GroupBoxT groupBoxT4;
        private JE.MyControl.RadioT radio6;
        private JE.MyControl.CheckBoxT checkBoxT1_含寄庫;
        private JE.MyControl.RadioT rdAvgByOneStk;
        private JE.MyControl.RadioT rdAvgByAllStk;
    }
}