namespace S_61.subMenuFm_3
{
    partial class FrmCust_DateRecievd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCust_DateRecievd));
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.SpNo = new JE.MyControl.TextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.SpName = new JE.MyControl.TextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.ReDate1 = new JE.MyControl.TextBoxT();
            this.ReDate = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.groupBoxT1.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Location = new System.Drawing.Point(0, 626);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radioT2);
            this.groupBoxT1.Controls.Add(this.radioT1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(101, 90);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(783, 78);
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
            this.radioT2.Location = new System.Drawing.Point(547, 36);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(74, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Tag = "總額表";
            this.radioT2.Text = "總額表";
            this.radioT2.UseVisualStyleBackColor = true;
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(161, 36);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(138, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.Tag = "明細表、簡要表";
            this.radioT1.Text = "明細表、簡要表";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.SpNo);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.SpName);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.ReDate1);
            this.pnlBoxT1.Controls.Add(this.ReDate);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Location = new System.Drawing.Point(101, 198);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(783, 235);
            this.pnlBoxT1.TabIndex = 0;
            // 
            // SpNo
            // 
            this.SpNo.AllowGrayBackColor = false;
            this.SpNo.AllowResize = true;
            this.SpNo.Font = new System.Drawing.Font("細明體", 12F);
            this.SpNo.Location = new System.Drawing.Point(397, 150);
            this.SpNo.MaxLength = 4;
            this.SpNo.Name = "SpNo";
            this.SpNo.oLen = 0;
            this.SpNo.Size = new System.Drawing.Size(39, 27);
            this.SpNo.TabIndex = 3;
            this.SpNo.DoubleClick += new System.EventHandler(this.SpNo_DoubleClick);
            this.SpNo.Validating += new System.ComponentModel.CancelEventHandler(this.SpNo_Validating);
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(287, 155);
            this.lblT4.Margin = new System.Windows.Forms.Padding(15);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(96, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "專 案 編 號";
            // 
            // SpName
            // 
            this.SpName.AllowGrayBackColor = true;
            this.SpName.AllowResize = true;
            this.SpName.BackColor = System.Drawing.Color.Silver;
            this.SpName.Font = new System.Drawing.Font("細明體", 12F);
            this.SpName.Location = new System.Drawing.Point(442, 150);
            this.SpName.MaxLength = 12;
            this.SpName.Name = "SpName";
            this.SpName.oLen = 0;
            this.SpName.ReadOnly = true;
            this.SpName.Size = new System.Drawing.Size(103, 27);
            this.SpName.TabIndex = 0;
            this.SpName.TabStop = false;
            this.SpName.Validating += new System.ComponentModel.CancelEventHandler(this.ReDate1_Validating);
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(287, 109);
            this.lblT3.Margin = new System.Windows.Forms.Padding(15);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "終止收款日期";
            // 
            // ReDate1
            // 
            this.ReDate1.AllowGrayBackColor = false;
            this.ReDate1.AllowResize = true;
            this.ReDate1.Font = new System.Drawing.Font("細明體", 12F);
            this.ReDate1.Location = new System.Drawing.Point(397, 104);
            this.ReDate1.MaxLength = 10;
            this.ReDate1.Name = "ReDate1";
            this.ReDate1.oLen = 0;
            this.ReDate1.Size = new System.Drawing.Size(87, 27);
            this.ReDate1.TabIndex = 2;
            this.ReDate1.Validating += new System.ComponentModel.CancelEventHandler(this.ReDate1_Validating);
            // 
            // ReDate
            // 
            this.ReDate.AllowGrayBackColor = false;
            this.ReDate.AllowResize = true;
            this.ReDate.Font = new System.Drawing.Font("細明體", 12F);
            this.ReDate.Location = new System.Drawing.Point(397, 58);
            this.ReDate.MaxLength = 10;
            this.ReDate.Name = "ReDate";
            this.ReDate.oLen = 0;
            this.ReDate.Size = new System.Drawing.Size(87, 27);
            this.ReDate.TabIndex = 1;
            this.ReDate.Validating += new System.ComponentModel.CancelEventHandler(this.ReDate_Validating);
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(287, 63);
            this.lblT2.Margin = new System.Windows.Forms.Padding(15);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起始收款日期";
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(129, 109);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(88, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "請輸入列印";
            // 
            // panelT1
            // 
            this.panelT1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panelT1.AutoSize = true;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(431, 542);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 82);
            this.panelT1.TabIndex = 1;
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
            // FrmCust_DateRecievd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmCust_DateRecievd";
            this.Tag = "日期別-已收帳款";
            this.Text = "日期別-已收帳款";
            this.Load += new System.EventHandler(this.FrmCust_DateRecievd_Load);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStrip1;
        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT ReDate;
        private JE.MyControl.TextBoxT ReDate1;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT SpNo;
        private JE.MyControl.TextBoxT SpName;
    }
}