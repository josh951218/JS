namespace S_61.S4
{
    partial class FrmCustLastDay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustLastDay));
            this.panelT1 = new JE.MyControl.PanelT();
            this.labelT6 = new JE.MyControl.LabelT();
            this.EmNo = new JE.MyControl.TextBoxT();
            this.CuDays = new JE.MyControl.TextBoxNumberT();
            this.SaDate = new JE.MyControl.TextBoxT();
            this.CuNo1 = new JE.MyControl.TextBoxT();
            this.CuNo = new JE.MyControl.TextBoxT();
            this.labelT5 = new JE.MyControl.LabelT();
            this.labelT4 = new JE.MyControl.LabelT();
            this.labelT3 = new JE.MyControl.LabelT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelT1.SuspendLayout();
            this.panelBtnT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.labelT6);
            this.panelT1.Controls.Add(this.EmNo);
            this.panelT1.Controls.Add(this.CuDays);
            this.panelT1.Controls.Add(this.SaDate);
            this.panelT1.Controls.Add(this.CuNo1);
            this.panelT1.Controls.Add(this.CuNo);
            this.panelT1.Controls.Add(this.labelT5);
            this.panelT1.Controls.Add(this.labelT4);
            this.panelT1.Controls.Add(this.labelT3);
            this.panelT1.Controls.Add(this.labelT2);
            this.panelT1.Controls.Add(this.labelT1);
            this.panelT1.Location = new System.Drawing.Point(222, 100);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(567, 259);
            this.panelT1.TabIndex = 0;
            // 
            // labelT6
            // 
            this.labelT6.AutoSize = true;
            this.labelT6.BackColor = System.Drawing.Color.Transparent;
            this.labelT6.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT6.Location = new System.Drawing.Point(156, 101);
            this.labelT6.Name = "labelT6";
            this.labelT6.Size = new System.Drawing.Size(72, 16);
            this.labelT6.TabIndex = 0;
            this.labelT6.Text = "業務編號";
            // 
            // EmNo
            // 
            this.EmNo.AllowGrayBackColor = false;
            this.EmNo.AllowResize = true;
            this.EmNo.Font = new System.Drawing.Font("細明體", 12F);
            this.EmNo.Location = new System.Drawing.Point(228, 96);
            this.EmNo.MaxLength = 4;
            this.EmNo.Name = "EmNo";
            this.EmNo.oLen = 0;
            this.EmNo.Size = new System.Drawing.Size(39, 27);
            this.EmNo.TabIndex = 3;
            this.EmNo.DoubleClick += new System.EventHandler(this.EmNo_DoubleClick);
            // 
            // CuDays
            // 
            this.CuDays.AllowGrayBackColor = false;
            this.CuDays.AllowResize = true;
            this.CuDays.FirstNum = 10;
            this.CuDays.Font = new System.Drawing.Font("細明體", 12F);
            this.CuDays.LastNum = 0;
            this.CuDays.Location = new System.Drawing.Point(228, 176);
            this.CuDays.MarkThousand = false;
            this.CuDays.MaxLength = 7;
            this.CuDays.Name = "CuDays";
            this.CuDays.NullInput = true;
            this.CuDays.NullValue = "";
            this.CuDays.oLen = 0;
            this.CuDays.Size = new System.Drawing.Size(63, 27);
            this.CuDays.TabIndex = 5;
            this.CuDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CuDays.Validating += new System.ComponentModel.CancelEventHandler(this.CuDays_Validating);
            // 
            // SaDate
            // 
            this.SaDate.AllowGrayBackColor = false;
            this.SaDate.AllowResize = true;
            this.SaDate.Font = new System.Drawing.Font("細明體", 12F);
            this.SaDate.Location = new System.Drawing.Point(228, 136);
            this.SaDate.MaxLength = 10;
            this.SaDate.Name = "SaDate";
            this.SaDate.oLen = 0;
            this.SaDate.Size = new System.Drawing.Size(87, 27);
            this.SaDate.TabIndex = 4;
            this.SaDate.Validating += new System.ComponentModel.CancelEventHandler(this.CuEDate_Validating);
            // 
            // CuNo1
            // 
            this.CuNo1.AllowGrayBackColor = false;
            this.CuNo1.AllowResize = true;
            this.CuNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.CuNo1.Location = new System.Drawing.Point(339, 56);
            this.CuNo1.MaxLength = 10;
            this.CuNo1.Name = "CuNo1";
            this.CuNo1.oLen = 0;
            this.CuNo1.Size = new System.Drawing.Size(87, 27);
            this.CuNo1.TabIndex = 2;
            this.CuNo1.DoubleClick += new System.EventHandler(this.CuNo_DoubleClick);
            // 
            // CuNo
            // 
            this.CuNo.AllowGrayBackColor = false;
            this.CuNo.AllowResize = true;
            this.CuNo.Font = new System.Drawing.Font("細明體", 12F);
            this.CuNo.Location = new System.Drawing.Point(228, 56);
            this.CuNo.MaxLength = 10;
            this.CuNo.Name = "CuNo";
            this.CuNo.oLen = 0;
            this.CuNo.Size = new System.Drawing.Size(87, 27);
            this.CuNo.TabIndex = 1;
            this.CuNo.DoubleClick += new System.EventHandler(this.CuNo_DoubleClick);
            // 
            // labelT5
            // 
            this.labelT5.AutoSize = true;
            this.labelT5.BackColor = System.Drawing.Color.Transparent;
            this.labelT5.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT5.Location = new System.Drawing.Point(291, 181);
            this.labelT5.Name = "labelT5";
            this.labelT5.Size = new System.Drawing.Size(48, 16);
            this.labelT5.TabIndex = 0;
            this.labelT5.Text = " 以上";
            // 
            // labelT4
            // 
            this.labelT4.AutoSize = true;
            this.labelT4.BackColor = System.Drawing.Color.Transparent;
            this.labelT4.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT4.Location = new System.Drawing.Point(315, 61);
            this.labelT4.Name = "labelT4";
            this.labelT4.Size = new System.Drawing.Size(24, 16);
            this.labelT4.TabIndex = 0;
            this.labelT4.Text = "～";
            // 
            // labelT3
            // 
            this.labelT3.AutoSize = true;
            this.labelT3.BackColor = System.Drawing.Color.Transparent;
            this.labelT3.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT3.Location = new System.Drawing.Point(140, 183);
            this.labelT3.Name = "labelT3";
            this.labelT3.Size = new System.Drawing.Size(88, 16);
            this.labelT3.TabIndex = 0;
            this.labelT3.Text = "未交易天數";
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(156, 141);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(72, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "終止日期";
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(156, 61);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "客戶編號";
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
            // FrmCustLastDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmCustLastDay";
            this.Tag = "久未交易客戶分析";
            this.Text = "久未交易客戶分析";
            this.Load += new System.EventHandler(this.FrmCustLastDay_Load);
            this.panelT1.ResumeLayout(false);
            this.panelT1.PerformLayout();
            this.panelBtnT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.TextBoxNumberT CuDays;
        private JE.MyControl.TextBoxT SaDate;
        private JE.MyControl.TextBoxT CuNo1;
        private JE.MyControl.TextBoxT CuNo;
        private JE.MyControl.LabelT labelT5;
        private JE.MyControl.LabelT labelT4;
        private JE.MyControl.LabelT labelT3;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.LabelT labelT6;
        private JE.MyControl.TextBoxT EmNo;
    }
}