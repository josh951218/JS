namespace S_61.SOther
{
    partial class FrmBuyGrad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBuyGrad));
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.KiNo = new JE.MyControl.TextBoxT();
            this.KiName = new JE.MyControl.TextBoxT();
            this.X12No = new JE.MyControl.TextBoxT();
            this.PK = new JE.MyControl.LabelT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.X12Name = new JE.MyControl.TextBoxT();
            this.lblT8 = new JE.MyControl.LabelT();
            this.BuPrs = new JE.MyControl.TextBoxNumberT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.btnDelete = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.btnAppend = new JE.MyControl.ButtonT();
            this.btnBottom = new JE.MyControl.ButtonT();
            this.btnNext = new JE.MyControl.ButtonT();
            this.btnPrior = new JE.MyControl.ButtonT();
            this.btnTop = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.KiNo);
            this.pnlBoxT1.Controls.Add(this.KiName);
            this.pnlBoxT1.Controls.Add(this.X12No);
            this.pnlBoxT1.Controls.Add(this.PK);
            this.pnlBoxT1.Controls.Add(this.lblT9);
            this.pnlBoxT1.Controls.Add(this.X12Name);
            this.pnlBoxT1.Controls.Add(this.lblT8);
            this.pnlBoxT1.Controls.Add(this.BuPrs);
            this.pnlBoxT1.Controls.Add(this.lblT7);
            this.pnlBoxT1.Location = new System.Drawing.Point(230, 109);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(550, 321);
            this.pnlBoxT1.TabIndex = 2;
            // 
            // KiNo
            // 
            this.KiNo.AllowGrayBackColor = false;
            this.KiNo.AllowResize = true;
            this.KiNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.KiNo.Font = new System.Drawing.Font("細明體", 12F);
            this.KiNo.Location = new System.Drawing.Point(209, 100);
            this.KiNo.MaxLength = 4;
            this.KiNo.Name = "KiNo";
            this.KiNo.oLen = 0;
            this.KiNo.ReadOnly = true;
            this.KiNo.Size = new System.Drawing.Size(39, 27);
            this.KiNo.TabIndex = 1;
            this.KiNo.TabStop = false;
            this.KiNo.DoubleClick += new System.EventHandler(this.KiNo_DoubleClick);
            this.KiNo.Validating += new System.ComponentModel.CancelEventHandler(this.KiNo_Validating);
            // 
            // KiName
            // 
            this.KiName.AllowGrayBackColor = true;
            this.KiName.AllowResize = true;
            this.KiName.BackColor = System.Drawing.Color.Silver;
            this.KiName.Font = new System.Drawing.Font("細明體", 12F);
            this.KiName.Location = new System.Drawing.Point(253, 100);
            this.KiName.MaxLength = 20;
            this.KiName.Name = "KiName";
            this.KiName.oLen = 0;
            this.KiName.ReadOnly = true;
            this.KiName.Size = new System.Drawing.Size(167, 27);
            this.KiName.TabIndex = 2;
            this.KiName.TabStop = false;
            // 
            // X12No
            // 
            this.X12No.AllowGrayBackColor = false;
            this.X12No.AllowResize = true;
            this.X12No.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.X12No.Font = new System.Drawing.Font("細明體", 12F);
            this.X12No.Location = new System.Drawing.Point(209, 147);
            this.X12No.MaxLength = 2;
            this.X12No.Name = "X12No";
            this.X12No.oLen = 0;
            this.X12No.ReadOnly = true;
            this.X12No.Size = new System.Drawing.Size(23, 27);
            this.X12No.TabIndex = 3;
            this.X12No.TabStop = false;
            this.X12No.DoubleClick += new System.EventHandler(this.X12No_DoubleClick);
            this.X12No.Validating += new System.ComponentModel.CancelEventHandler(this.X12No_Validating);
            // 
            // PK
            // 
            this.PK.AutoSize = true;
            this.PK.BackColor = System.Drawing.Color.Transparent;
            this.PK.Font = new System.Drawing.Font("細明體", 12F);
            this.PK.Location = new System.Drawing.Point(138, 55);
            this.PK.Name = "PK";
            this.PK.Size = new System.Drawing.Size(40, 16);
            this.PK.TabIndex = 0;
            this.PK.Text = "pk值";
            this.PK.Visible = false;
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(131, 198);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(72, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "折    數";
            // 
            // X12Name
            // 
            this.X12Name.AllowGrayBackColor = true;
            this.X12Name.AllowResize = true;
            this.X12Name.BackColor = System.Drawing.Color.Silver;
            this.X12Name.Font = new System.Drawing.Font("細明體", 12F);
            this.X12Name.Location = new System.Drawing.Point(236, 147);
            this.X12Name.MaxLength = 10;
            this.X12Name.Name = "X12Name";
            this.X12Name.oLen = 0;
            this.X12Name.ReadOnly = true;
            this.X12Name.Size = new System.Drawing.Size(87, 27);
            this.X12Name.TabIndex = 4;
            this.X12Name.TabStop = false;
            // 
            // lblT8
            // 
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(131, 152);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(72, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "廠商類別";
            // 
            // BuPrs
            // 
            this.BuPrs.AllowGrayBackColor = false;
            this.BuPrs.AllowResize = true;
            this.BuPrs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BuPrs.FirstNum = 10;
            this.BuPrs.Font = new System.Drawing.Font("細明體", 12F);
            this.BuPrs.LastNum = 0;
            this.BuPrs.Location = new System.Drawing.Point(209, 193);
            this.BuPrs.MarkThousand = false;
            this.BuPrs.MaxLength = 5;
            this.BuPrs.Name = "BuPrs";
            this.BuPrs.NullInput = false;
            this.BuPrs.NullValue = "0";
            this.BuPrs.oLen = 0;
            this.BuPrs.ReadOnly = true;
            this.BuPrs.Size = new System.Drawing.Size(47, 27);
            this.BuPrs.TabIndex = 5;
            this.BuPrs.TabStop = false;
            this.BuPrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BuPrs.Validating += new System.ComponentModel.CancelEventHandler(this.BuPrs_Validating);
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(131, 105);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(72, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "產品類別";
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Controls.Add(this.btnDelete);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Controls.Add(this.btnAppend);
            this.panelT1.Controls.Add(this.btnBottom);
            this.panelT1.Controls.Add(this.btnNext);
            this.panelT1.Controls.Add(this.btnPrior);
            this.panelT1.Controls.Add(this.btnTop);
            this.panelT1.Location = new System.Drawing.Point(121, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(769, 79);
            this.panelT1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 9F);
            this.btnExit.Location = new System.Drawing.Point(690, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 10;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("細明體", 9F);
            this.btnCancel.Location = new System.Drawing.Point(621, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 79);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.UseDefaultSettings = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("細明體", 9F);
            this.btnSave.Location = new System.Drawing.Point(552, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 79);
            this.btnSave.TabIndex = 8;
            this.btnSave.UseDefaultSettings = false;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBrow
            // 
            this.btnBrow.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrow.BackgroundImage")));
            this.btnBrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrow.Font = new System.Drawing.Font("細明體", 9F);
            this.btnBrow.Location = new System.Drawing.Point(483, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 7;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("細明體", 9F);
            this.btnDelete.Location = new System.Drawing.Point(414, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 79);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.UseDefaultSettings = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.SystemColors.Control;
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModify.Font = new System.Drawing.Font("細明體", 9F);
            this.btnModify.Location = new System.Drawing.Point(345, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(69, 79);
            this.btnModify.TabIndex = 5;
            this.btnModify.UseDefaultSettings = false;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAppend
            // 
            this.btnAppend.BackColor = System.Drawing.SystemColors.Control;
            this.btnAppend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAppend.BackgroundImage")));
            this.btnAppend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAppend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAppend.Font = new System.Drawing.Font("細明體", 9F);
            this.btnAppend.Location = new System.Drawing.Point(276, 0);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(69, 79);
            this.btnAppend.TabIndex = 4;
            this.btnAppend.UseDefaultSettings = false;
            this.btnAppend.UseVisualStyleBackColor = false;
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // btnBottom
            // 
            this.btnBottom.BackColor = System.Drawing.SystemColors.Control;
            this.btnBottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBottom.BackgroundImage")));
            this.btnBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBottom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBottom.Font = new System.Drawing.Font("細明體", 9F);
            this.btnBottom.Location = new System.Drawing.Point(207, 0);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(69, 79);
            this.btnBottom.TabIndex = 3;
            this.btnBottom.UseDefaultSettings = false;
            this.btnBottom.UseVisualStyleBackColor = false;
            this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Font = new System.Drawing.Font("細明體", 9F);
            this.btnNext.Location = new System.Drawing.Point(138, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(69, 79);
            this.btnNext.TabIndex = 2;
            this.btnNext.UseDefaultSettings = false;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrior
            // 
            this.btnPrior.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrior.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrior.BackgroundImage")));
            this.btnPrior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrior.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrior.Font = new System.Drawing.Font("細明體", 9F);
            this.btnPrior.Location = new System.Drawing.Point(69, 0);
            this.btnPrior.Name = "btnPrior";
            this.btnPrior.Size = new System.Drawing.Size(69, 79);
            this.btnPrior.TabIndex = 1;
            this.btnPrior.UseDefaultSettings = false;
            this.btnPrior.UseVisualStyleBackColor = false;
            this.btnPrior.Click += new System.EventHandler(this.btnPrior_Click);
            // 
            // btnTop
            // 
            this.btnTop.BackColor = System.Drawing.SystemColors.Control;
            this.btnTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTop.BackgroundImage")));
            this.btnTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTop.Font = new System.Drawing.Font("細明體", 9F);
            this.btnTop.Location = new System.Drawing.Point(0, 0);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(69, 79);
            this.btnTop.TabIndex = 0;
            this.btnTop.UseDefaultSettings = false;
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
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
            // FrmBuyGrad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmBuyGrad";
            this.Tag = "銷售採購策略建檔";
            this.Text = "採購策略建檔";
            this.Load += new System.EventHandler(this.FrmBuyGrad_Load);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT pnlBoxT1;
        public JE.MyControl.TextBoxT KiNo;
        public JE.MyControl.TextBoxT X12No;
        public JE.MyControl.TextBoxT KiName;
        public JE.MyControl.TextBoxT X12Name;
        private JE.MyControl.TextBoxNumberT BuPrs;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.LabelT PK;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnCancel;
        private JE.MyControl.ButtonT btnSave;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.ButtonT btnDelete;
        private JE.MyControl.ButtonT btnModify;
        private JE.MyControl.ButtonT btnAppend;
        private JE.MyControl.ButtonT btnBottom;
        private JE.MyControl.ButtonT btnNext;
        private JE.MyControl.ButtonT btnPrior;
        private JE.MyControl.ButtonT btnTop;
        private JE.MyControl.StatusStripT statusStripT1; 
    }
}