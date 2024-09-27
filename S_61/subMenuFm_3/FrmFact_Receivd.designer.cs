namespace S_61.subMenuFm_3
{
    partial class FrmFact_Receivd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFact_Receivd));
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.PaDateAcs = new JE.MyControl.TextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.FaNo = new JE.MyControl.TextBoxT();
            this.FaNo_1 = new JE.MyControl.TextBoxT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.PaDateAcs_1 = new JE.MyControl.TextBoxT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.groupBoxT2 = new JE.MyControl.GroupBoxT();
            this.radio4 = new JE.MyControl.RadioT();
            this.radio5 = new JE.MyControl.RadioT();
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radio1 = new JE.MyControl.RadioT();
            this.radio2 = new JE.MyControl.RadioT();
            this.radio3 = new JE.MyControl.RadioT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT8 = new JE.MyControl.LabelT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.groupBoxT2.SuspendLayout();
            this.groupBoxT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.PaDateAcs);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.FaNo);
            this.pnlBoxT1.Controls.Add(this.FaNo_1);
            this.pnlBoxT1.Controls.Add(this.lblT6);
            this.pnlBoxT1.Controls.Add(this.PaDateAcs_1);
            this.pnlBoxT1.Controls.Add(this.lblT7);
            this.pnlBoxT1.Location = new System.Drawing.Point(192, 206);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(626, 148);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(152, 41);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "起訖帳款日期";
            // 
            // PaDateAcs
            // 
            this.PaDateAcs.AllowGrayBackColor = false;
            this.PaDateAcs.AllowResize = true;
            this.PaDateAcs.BackColor = System.Drawing.Color.White;
            this.PaDateAcs.Font = new System.Drawing.Font("細明體", 12F);
            this.PaDateAcs.Location = new System.Drawing.Point(261, 36);
            this.PaDateAcs.MaxLength = 10;
            this.PaDateAcs.Name = "PaDateAcs";
            this.PaDateAcs.oLen = 0;
            this.PaDateAcs.Size = new System.Drawing.Size(87, 27);
            this.PaDateAcs.TabIndex = 1;
            this.PaDateAcs.Validating += new System.ComponentModel.CancelEventHandler(this.PaDateAcs_Validating);
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(152, 90);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(104, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "起訖廠商編號";
            // 
            // FaNo
            // 
            this.FaNo.AllowGrayBackColor = false;
            this.FaNo.AllowResize = true;
            this.FaNo.BackColor = System.Drawing.Color.White;
            this.FaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.FaNo.Location = new System.Drawing.Point(261, 85);
            this.FaNo.MaxLength = 10;
            this.FaNo.Name = "FaNo";
            this.FaNo.oLen = 0;
            this.FaNo.Size = new System.Drawing.Size(87, 27);
            this.FaNo.TabIndex = 3;
            this.FaNo.DoubleClick += new System.EventHandler(this.FaNo_Click);
            // 
            // FaNo_1
            // 
            this.FaNo_1.AllowGrayBackColor = false;
            this.FaNo_1.AllowResize = true;
            this.FaNo_1.BackColor = System.Drawing.Color.White;
            this.FaNo_1.Font = new System.Drawing.Font("細明體", 12F);
            this.FaNo_1.Location = new System.Drawing.Point(388, 85);
            this.FaNo_1.MaxLength = 10;
            this.FaNo_1.Name = "FaNo_1";
            this.FaNo_1.oLen = 0;
            this.FaNo_1.Size = new System.Drawing.Size(87, 27);
            this.FaNo_1.TabIndex = 4;
            this.FaNo_1.DoubleClick += new System.EventHandler(this.FaNo_Click);
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(356, 90);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(24, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "～";
            // 
            // PaDateAcs_1
            // 
            this.PaDateAcs_1.AllowGrayBackColor = false;
            this.PaDateAcs_1.AllowResize = true;
            this.PaDateAcs_1.BackColor = System.Drawing.Color.White;
            this.PaDateAcs_1.Font = new System.Drawing.Font("細明體", 12F);
            this.PaDateAcs_1.Location = new System.Drawing.Point(388, 36);
            this.PaDateAcs_1.MaxLength = 10;
            this.PaDateAcs_1.Name = "PaDateAcs_1";
            this.PaDateAcs_1.oLen = 0;
            this.PaDateAcs_1.Size = new System.Drawing.Size(87, 27);
            this.PaDateAcs_1.TabIndex = 2;
            this.PaDateAcs_1.Validating += new System.ComponentModel.CancelEventHandler(this.PaDateAcs_Validating);
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(356, 41);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(24, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "～";
            // 
            // panelT1
            // 
            this.panelT1.BackColor = System.Drawing.Color.Transparent;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(431, 546);
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
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 5F);
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
            this.btnBrow.Font = new System.Drawing.Font("Times New Roman", 5F);
            this.btnBrow.Location = new System.Drawing.Point(0, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 1;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
            // 
            // groupBoxT2
            // 
            this.groupBoxT2.Controls.Add(this.radio4);
            this.groupBoxT2.Controls.Add(this.radio5);
            this.groupBoxT2.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT2.Location = new System.Drawing.Point(39, 530);
            this.groupBoxT2.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT2.Name = "groupBoxT2";
            this.groupBoxT2.Size = new System.Drawing.Size(256, 80);
            this.groupBoxT2.TabIndex = 4;
            this.groupBoxT2.TabStop = false;
            this.groupBoxT2.Text = "沖帳金額為零";
            this.groupBoxT2.Visible = false;
            // 
            // radio4
            // 
            this.radio4.AutoSize = true;
            this.radio4.BackColor = System.Drawing.Color.Transparent;
            this.radio4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio4.Font = new System.Drawing.Font("細明體", 12F);
            this.radio4.Location = new System.Drawing.Point(43, 37);
            this.radio4.Name = "radio4";
            this.radio4.Size = new System.Drawing.Size(74, 20);
            this.radio4.TabIndex = 1;
            this.radio4.Tag = "不顯示";
            this.radio4.Text = "不顯示";
            this.radio4.UseVisualStyleBackColor = true;
            // 
            // radio5
            // 
            this.radio5.AutoSize = true;
            this.radio5.BackColor = System.Drawing.Color.LightBlue;
            this.radio5.Checked = true;
            this.radio5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio5.Font = new System.Drawing.Font("細明體", 12F);
            this.radio5.Location = new System.Drawing.Point(152, 37);
            this.radio5.Name = "radio5";
            this.radio5.Size = new System.Drawing.Size(58, 20);
            this.radio5.TabIndex = 0;
            this.radio5.Tag = "顯示";
            this.radio5.Text = "顯示";
            this.radio5.UseVisualStyleBackColor = true;
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radio1);
            this.groupBoxT1.Controls.Add(this.radio2);
            this.groupBoxT1.Controls.Add(this.radio3);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(192, 107);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(626, 80);
            this.groupBoxT1.TabIndex = 3;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radio1
            // 
            this.radio1.AutoSize = true;
            this.radio1.BackColor = System.Drawing.Color.LightBlue;
            this.radio1.Checked = true;
            this.radio1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio1.Font = new System.Drawing.Font("細明體", 12F);
            this.radio1.Location = new System.Drawing.Point(82, 37);
            this.radio1.Name = "radio1";
            this.radio1.Size = new System.Drawing.Size(138, 20);
            this.radio1.TabIndex = 0;
            this.radio1.Tag = "明細表、簡要表";
            this.radio1.Text = "明細表、簡要表";
            this.radio1.UseVisualStyleBackColor = false;
            // 
            // radio2
            // 
            this.radio2.AutoSize = true;
            this.radio2.BackColor = System.Drawing.Color.Transparent;
            this.radio2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio2.Font = new System.Drawing.Font("細明體", 12F);
            this.radio2.Location = new System.Drawing.Point(276, 37);
            this.radio2.Name = "radio2";
            this.radio2.Size = new System.Drawing.Size(106, 20);
            this.radio2.TabIndex = 1;
            this.radio2.Tag = "本幣總額表";
            this.radio2.Text = "本幣總額表";
            this.radio2.UseVisualStyleBackColor = true;
            // 
            // radio3
            // 
            this.radio3.AutoSize = true;
            this.radio3.BackColor = System.Drawing.Color.Transparent;
            this.radio3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio3.Font = new System.Drawing.Font("細明體", 12F);
            this.radio3.Location = new System.Drawing.Point(438, 37);
            this.radio3.Name = "radio3";
            this.radio3.Size = new System.Drawing.Size(106, 20);
            this.radio3.TabIndex = 2;
            this.radio3.Tag = "幣別總額表";
            this.radio3.Text = "幣別總額表";
            this.radio3.UseVisualStyleBackColor = true;
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(382, 437);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(288, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "※起始編號空白表示從第一筆列印.....";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(382, 465);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(288, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "※終止編號空白表示列印至最後一筆...";
            // 
            // lblT8
            // 
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(382, 493);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(288, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "※兩者皆空白表示全部列印...........";
            // 
            // statusStripT1
            // 
            this.statusStripT1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStripT1.Location = new System.Drawing.Point(0, 625);
            this.statusStripT1.Name = "statusStripT1";
            this.statusStripT1.Size = new System.Drawing.Size(1010, 22);
            this.statusStripT1.TabIndex = 0;
            this.statusStripT1.Text = "statusStripT1";
            // 
            // FrmFact_Receivd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.groupBoxT2);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.lblT8);
            this.Controls.Add(this.lblT5);
            this.Name = "FrmFact_Receivd";
            this.Tag = "廠商別-已付帳款";
            this.Text = "廠商別已付帳款";
            this.Load += new System.EventHandler(this.FrmFact_Receivd_Load);
            this.Shown += new System.EventHandler(this.FrmFact_Receivd_Shown);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.groupBoxT2.ResumeLayout(false);
            this.groupBoxT2.PerformLayout();
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        public JE.MyControl.TextBoxT PaDateAcs;
        public JE.MyControl.TextBoxT PaDateAcs_1;
        public JE.MyControl.TextBoxT FaNo;
        public JE.MyControl.TextBoxT FaNo_1;
        public JE.MyControl.RadioT radio3;
        public JE.MyControl.RadioT radio2;
        public JE.MyControl.RadioT radio1;
        private JE.MyControl.RadioT radio4;
        private JE.MyControl.RadioT radio5;
        private JE.MyControl.GroupBoxT groupBoxT2;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}