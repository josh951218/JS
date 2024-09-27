namespace S_61.subMenuFm_3
{
    partial class FrmDate_Payabld
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDate_Payabld));
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.PaDate = new JE.MyControl.TextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.PaDate1 = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.SpNo = new JE.MyControl.TextBoxT();
            this.SpName = new JE.MyControl.TextBoxT();
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelT1.SuspendLayout();
            this.groupBoxT1.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(140, 119);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(88, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "請輸入列印";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(281, 73);
            this.lblT2.Margin = new System.Windows.Forms.Padding(15);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起始付款日期";
            // 
            // PaDate
            // 
            this.PaDate.AllowGrayBackColor = false;
            this.PaDate.AllowResize = true;
            this.PaDate.Font = new System.Drawing.Font("細明體", 12F);
            this.PaDate.Location = new System.Drawing.Point(396, 68);
            this.PaDate.MaxLength = 10;
            this.PaDate.Name = "PaDate";
            this.PaDate.oLen = 0;
            this.PaDate.Size = new System.Drawing.Size(87, 27);
            this.PaDate.TabIndex = 1;
            this.PaDate.Validating += new System.ComponentModel.CancelEventHandler(this.PaDate_Validating);
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(281, 119);
            this.lblT3.Margin = new System.Windows.Forms.Padding(15);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "終止付款日期";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(289, 164);
            this.lblT4.Margin = new System.Windows.Forms.Padding(15);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(96, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "專 案 編 號";
            // 
            // PaDate1
            // 
            this.PaDate1.AllowGrayBackColor = false;
            this.PaDate1.AllowResize = true;
            this.PaDate1.Font = new System.Drawing.Font("細明體", 12F);
            this.PaDate1.Location = new System.Drawing.Point(396, 114);
            this.PaDate1.MaxLength = 10;
            this.PaDate1.Name = "PaDate1";
            this.PaDate1.oLen = 0;
            this.PaDate1.Size = new System.Drawing.Size(87, 27);
            this.PaDate1.TabIndex = 2;
            this.PaDate1.Validating += new System.ComponentModel.CancelEventHandler(this.PaDate1_Validating);
            // 
            // panelT1
            // 
            this.panelT1.AutoSize = true;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(431, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 82);
            this.panelT1.TabIndex = 2;
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
            // SpNo
            // 
            this.SpNo.AllowGrayBackColor = false;
            this.SpNo.AllowResize = true;
            this.SpNo.Font = new System.Drawing.Font("細明體", 12F);
            this.SpNo.Location = new System.Drawing.Point(395, 159);
            this.SpNo.MaxLength = 4;
            this.SpNo.Name = "SpNo";
            this.SpNo.oLen = 0;
            this.SpNo.Size = new System.Drawing.Size(39, 27);
            this.SpNo.TabIndex = 3;
            this.SpNo.DoubleClick += new System.EventHandler(this.SpNo_DoubleClick);
            // 
            // SpName
            // 
            this.SpName.AllowGrayBackColor = true;
            this.SpName.AllowResize = true;
            this.SpName.BackColor = System.Drawing.Color.Silver;
            this.SpName.Font = new System.Drawing.Font("細明體", 12F);
            this.SpName.Location = new System.Drawing.Point(440, 159);
            this.SpName.MaxLength = 12;
            this.SpName.Name = "SpName";
            this.SpName.oLen = 0;
            this.SpName.ReadOnly = true;
            this.SpName.Size = new System.Drawing.Size(103, 27);
            this.SpName.TabIndex = 0;
            this.SpName.TabStop = false;
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radioT1);
            this.groupBoxT1.Controls.Add(this.radioT2);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(121, 86);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(769, 74);
            this.groupBoxT1.TabIndex = 3;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(157, 34);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(138, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.Tag = "明細表、簡要表";
            this.radioT1.Text = "明細表、簡要表";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.Transparent;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(538, 34);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(74, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Tag = "總額表";
            this.radioT2.Text = "總額表";
            this.radioT2.UseVisualStyleBackColor = true;
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.SpNo);
            this.pnlBoxT1.Controls.Add(this.PaDate);
            this.pnlBoxT1.Controls.Add(this.SpName);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.PaDate1);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Location = new System.Drawing.Point(121, 181);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(769, 254);
            this.pnlBoxT1.TabIndex = 1;
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
            // FrmDate_Payabld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmDate_Payabld";
            this.Tag = "日期別-已付帳款";
            this.Text = "日期別-已付帳款";
            this.Load += new System.EventHandler(this.FrmDate_Payabld_Load);
            this.panelT1.ResumeLayout(false);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT PaDate;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT PaDate1;
        private JE.MyControl.TextBoxT SpNo;
        private JE.MyControl.TextBoxT SpName; 
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}