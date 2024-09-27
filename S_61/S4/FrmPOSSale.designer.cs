namespace S_61.S4
{
    partial class FrmPOSSale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPOSSale));
            this.panelT1 = new JE.MyControl.PanelT();
            this.StName = new JE.MyControl.TextBoxT();
            this.SeNo = new JE.MyControl.TextBoxT();
            this.StNo = new JE.MyControl.TextBoxT();
            this.labelT4 = new JE.MyControl.LabelT();
            this.labelT3 = new JE.MyControl.LabelT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.day1 = new JE.MyControl.TextBoxT();
            this.day = new JE.MyControl.TextBoxT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.labelT5 = new JE.MyControl.LabelT();
            this.panelT1.SuspendLayout();
            this.panelBtnT1.SuspendLayout();
            this.groupBoxT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.StName);
            this.panelT1.Controls.Add(this.SeNo);
            this.panelT1.Controls.Add(this.StNo);
            this.panelT1.Controls.Add(this.labelT5);
            this.panelT1.Controls.Add(this.labelT4);
            this.panelT1.Controls.Add(this.labelT3);
            this.panelT1.Controls.Add(this.labelT2);
            this.panelT1.Controls.Add(this.day1);
            this.panelT1.Controls.Add(this.day);
            this.panelT1.Controls.Add(this.labelT1);
            this.panelT1.Location = new System.Drawing.Point(191, 166);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(629, 262);
            this.panelT1.TabIndex = 0;
            // 
            // StName
            // 
            this.StName.AllowGrayBackColor = true;
            this.StName.AllowResize = true;
            this.StName.BackColor = System.Drawing.Color.Silver;
            this.StName.Font = new System.Drawing.Font("細明體", 12F);
            this.StName.Location = new System.Drawing.Point(371, 118);
            this.StName.MaxLength = 10;
            this.StName.Name = "StName";
            this.StName.oLen = 0;
            this.StName.ReadOnly = true;
            this.StName.Size = new System.Drawing.Size(87, 27);
            this.StName.TabIndex = 4;
            this.StName.TabStop = false;
            // 
            // SeNo
            // 
            this.SeNo.AllowGrayBackColor = false;
            this.SeNo.AllowResize = true;
            this.SeNo.Font = new System.Drawing.Font("細明體", 12F);
            this.SeNo.Location = new System.Drawing.Point(284, 168);
            this.SeNo.MaxLength = 1;
            this.SeNo.Name = "SeNo";
            this.SeNo.oLen = 0;
            this.SeNo.Size = new System.Drawing.Size(15, 27);
            this.SeNo.TabIndex = 5;
            // 
            // StNo
            // 
            this.StNo.AllowGrayBackColor = false;
            this.StNo.AllowResize = true;
            this.StNo.Font = new System.Drawing.Font("細明體", 12F);
            this.StNo.Location = new System.Drawing.Point(284, 118);
            this.StNo.MaxLength = 10;
            this.StNo.Name = "StNo";
            this.StNo.oLen = 0;
            this.StNo.Size = new System.Drawing.Size(87, 27);
            this.StNo.TabIndex = 3;
            this.StNo.DoubleClick += new System.EventHandler(this.StNo_DoubleClick);
            this.StNo.Validating += new System.ComponentModel.CancelEventHandler(this.StNo_Validating);
            // 
            // labelT4
            // 
            this.labelT4.AutoSize = true;
            this.labelT4.BackColor = System.Drawing.Color.Transparent;
            this.labelT4.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT4.Location = new System.Drawing.Point(179, 173);
            this.labelT4.Name = "labelT4";
            this.labelT4.Size = new System.Drawing.Size(96, 16);
            this.labelT4.TabIndex = 0;
            this.labelT4.Text = "機 台 號 碼";
            // 
            // labelT3
            // 
            this.labelT3.AutoSize = true;
            this.labelT3.BackColor = System.Drawing.Color.Transparent;
            this.labelT3.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT3.Location = new System.Drawing.Point(179, 123);
            this.labelT3.Name = "labelT3";
            this.labelT3.Size = new System.Drawing.Size(96, 16);
            this.labelT3.TabIndex = 0;
            this.labelT3.Text = "門 市 編 號";
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(355, 73);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(24, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "～";
            // 
            // day1
            // 
            this.day1.AllowGrayBackColor = false;
            this.day1.AllowResize = true;
            this.day1.Font = new System.Drawing.Font("細明體", 12F);
            this.day1.Location = new System.Drawing.Point(379, 68);
            this.day1.MaxLength = 8;
            this.day1.Name = "day1";
            this.day1.oLen = 0;
            this.day1.Size = new System.Drawing.Size(71, 27);
            this.day1.TabIndex = 2;
            this.day1.Validating += new System.ComponentModel.CancelEventHandler(this.day1_Validating);
            // 
            // day
            // 
            this.day.AllowGrayBackColor = false;
            this.day.AllowResize = true;
            this.day.Font = new System.Drawing.Font("細明體", 12F);
            this.day.Location = new System.Drawing.Point(284, 68);
            this.day.MaxLength = 8;
            this.day.Name = "day";
            this.day.oLen = 0;
            this.day.Size = new System.Drawing.Size(71, 27);
            this.day.TabIndex = 1;
            this.day.Validating += new System.ComponentModel.CancelEventHandler(this.day_Validating);
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(171, 73);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(104, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "起訖帳款日期";
            // 
            // panelBtnT1
            // 
            this.panelBtnT1.Controls.Add(this.btnExit);
            this.panelBtnT1.Controls.Add(this.btnBrow);
            this.panelBtnT1.Location = new System.Drawing.Point(431, 544);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(148, 79);
            this.panelBtnT1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(69, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 1;
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
            this.btnBrow.Location = new System.Drawing.Point(0, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 0;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
            // 
            // statusStripT1
            // 
            this.statusStripT1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStripT1.Location = new System.Drawing.Point(0, 626);
            this.statusStripT1.Name = "statusStripT1";
            this.statusStripT1.Size = new System.Drawing.Size(1010, 22);
            this.statusStripT1.TabIndex = 0;
            this.statusStripT1.Text = "statusStripT1";
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radioT2);
            this.groupBoxT1.Controls.Add(this.radioT1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(191, 81);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(629, 72);
            this.groupBoxT1.TabIndex = 2;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.Transparent;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(383, 33);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(74, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Text = "明細表";
            this.radioT2.UseVisualStyleBackColor = false;
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(171, 33);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(74, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.TabStop = true;
            this.radioT1.Text = "簡要表";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // labelT5
            // 
            this.labelT5.AutoSize = true;
            this.labelT5.BackColor = System.Drawing.Color.Transparent;
            this.labelT5.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT5.Location = new System.Drawing.Point(299, 173);
            this.labelT5.Name = "labelT5";
            this.labelT5.Size = new System.Drawing.Size(56, 16);
            this.labelT5.TabIndex = 0;
            this.labelT5.Text = "(號機)";
            // 
            // FrmPOSSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmPOSSale";
            this.Text = "前台銷貨報表";
            this.Load += new System.EventHandler(this.FrmPOSSale_Load);
            this.panelT1.ResumeLayout(false);
            this.panelT1.PerformLayout();
            this.panelBtnT1.ResumeLayout(false);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.TextBoxT day1;
        private JE.MyControl.TextBoxT day;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.TextBoxT StName;
        private JE.MyControl.TextBoxT StNo;
        private JE.MyControl.LabelT labelT3;
        private JE.MyControl.TextBoxT SeNo;
        private JE.MyControl.LabelT labelT4;
        private JE.MyControl.LabelT labelT5;
    }
}