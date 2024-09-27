namespace S_61.subMenuFm_6
{
    partial class FrmSystemSetmore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemSetmore));
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.panelNT1 = new JE.MyControl.PanelNT();
            this.X3Forward2 = new JE.MyControl.RadioT();
            this.X3Forward1 = new JE.MyControl.RadioT();
            this.panelNT2 = new JE.MyControl.PanelNT();
            this.InvUsed2 = new JE.MyControl.RadioT();
            this.InvUsed1 = new JE.MyControl.RadioT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.btnResetReportFormat = new JE.MyControl.ButtonSmallT();
            this.btnResetAdmin = new JE.MyControl.ButtonSmallT();
            this.labelT3 = new JE.MyControl.LabelT();
            this.labelT4 = new JE.MyControl.LabelT();
            this.labelT6 = new JE.MyControl.LabelT();
            this.DabaseAddress = new JE.MyControl.TextBoxT();
            this.RestoreAddress = new JE.MyControl.TextBoxT();
            this.Browse_btn = new JE.MyControl.ButtonSmallT();
            this.labelT5 = new JE.MyControl.LabelT();
            this.buttonSmallT1 = new JE.MyControl.ButtonSmallT();
            this.buttonSmallT2 = new JE.MyControl.ButtonSmallT();
            this.panelBtnT1.SuspendLayout();
            this.panelNT1.SuspendLayout();
            this.panelNT2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBtnT1
            // 
            this.panelBtnT1.Controls.Add(this.btnExit);
            this.panelBtnT1.Location = new System.Drawing.Point(466, 544);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(79, 79);
            this.panelBtnT1.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 0;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(14, 27);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(104, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "內含稅改稅額";
            // 
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Controls.Add(this.X3Forward2);
            this.panelNT1.Controls.Add(this.X3Forward1);
            this.panelNT1.Location = new System.Drawing.Point(118, 14);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(181, 42);
            this.panelNT1.TabIndex = 1;
            // 
            // X3Forward2
            // 
            this.X3Forward2.AutoSize = true;
            this.X3Forward2.BackColor = System.Drawing.Color.LightBlue;
            this.X3Forward2.Checked = true;
            this.X3Forward2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.X3Forward2.Font = new System.Drawing.Font("細明體", 12F);
            this.X3Forward2.Location = new System.Drawing.Point(97, 10);
            this.X3Forward2.Name = "X3Forward2";
            this.X3Forward2.Size = new System.Drawing.Size(58, 20);
            this.X3Forward2.TabIndex = 1;
            this.X3Forward2.Tag = "向後";
            this.X3Forward2.Text = "向後";
            this.X3Forward2.UseVisualStyleBackColor = false;
            // 
            // X3Forward1
            // 
            this.X3Forward1.AutoSize = true;
            this.X3Forward1.BackColor = System.Drawing.Color.Transparent;
            this.X3Forward1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.X3Forward1.Font = new System.Drawing.Font("細明體", 12F);
            this.X3Forward1.Location = new System.Drawing.Point(23, 10);
            this.X3Forward1.Name = "X3Forward1";
            this.X3Forward1.Size = new System.Drawing.Size(58, 20);
            this.X3Forward1.TabIndex = 0;
            this.X3Forward1.Tag = "向前";
            this.X3Forward1.Text = "向前";
            this.X3Forward1.UseVisualStyleBackColor = false;
            // 
            // panelNT2
            // 
            this.panelNT2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT2.Controls.Add(this.InvUsed2);
            this.panelNT2.Controls.Add(this.InvUsed1);
            this.panelNT2.Location = new System.Drawing.Point(118, 91);
            this.panelNT2.Name = "panelNT2";
            this.panelNT2.Size = new System.Drawing.Size(181, 42);
            this.panelNT2.TabIndex = 3;
            // 
            // InvUsed2
            // 
            this.InvUsed2.AutoSize = true;
            this.InvUsed2.BackColor = System.Drawing.Color.LightBlue;
            this.InvUsed2.Checked = true;
            this.InvUsed2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InvUsed2.Font = new System.Drawing.Font("細明體", 12F);
            this.InvUsed2.Location = new System.Drawing.Point(97, 10);
            this.InvUsed2.Name = "InvUsed2";
            this.InvUsed2.Size = new System.Drawing.Size(74, 20);
            this.InvUsed2.TabIndex = 1;
            this.InvUsed2.Tag = "不使用";
            this.InvUsed2.Text = "不使用";
            this.InvUsed2.UseVisualStyleBackColor = false;
            // 
            // InvUsed1
            // 
            this.InvUsed1.AutoSize = true;
            this.InvUsed1.BackColor = System.Drawing.Color.Transparent;
            this.InvUsed1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InvUsed1.Font = new System.Drawing.Font("細明體", 12F);
            this.InvUsed1.Location = new System.Drawing.Point(23, 10);
            this.InvUsed1.Name = "InvUsed1";
            this.InvUsed1.Size = new System.Drawing.Size(58, 20);
            this.InvUsed1.TabIndex = 0;
            this.InvUsed1.Tag = "使用";
            this.InvUsed1.Text = "使用";
            this.InvUsed1.UseVisualStyleBackColor = false;
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(14, 104);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(104, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "進銷發票外掛";
            // 
            // btnResetReportFormat
            // 
            this.btnResetReportFormat.AutoSize = true;
            this.btnResetReportFormat.Font = new System.Drawing.Font("細明體", 12F);
            this.btnResetReportFormat.Location = new System.Drawing.Point(12, 581);
            this.btnResetReportFormat.Name = "btnResetReportFormat";
            this.btnResetReportFormat.Size = new System.Drawing.Size(170, 26);
            this.btnResetReportFormat.TabIndex = 4;
            this.btnResetReportFormat.Text = "Reset Report Format";
            this.btnResetReportFormat.UseVisualStyleBackColor = true;
            this.btnResetReportFormat.Click += new System.EventHandler(this.btnResetReportFormat_Click);
            // 
            // btnResetAdmin
            // 
            this.btnResetAdmin.AutoSize = true;
            this.btnResetAdmin.Font = new System.Drawing.Font("細明體", 12F);
            this.btnResetAdmin.Location = new System.Drawing.Point(12, 502);
            this.btnResetAdmin.Name = "btnResetAdmin";
            this.btnResetAdmin.Size = new System.Drawing.Size(170, 26);
            this.btnResetAdmin.TabIndex = 5;
            this.btnResetAdmin.Text = "Reset System  Admin";
            this.btnResetAdmin.UseVisualStyleBackColor = true;
            this.btnResetAdmin.Click += new System.EventHandler(this.btnResetAdmin_Click);
            // 
            // labelT3
            // 
            this.labelT3.AutoSize = true;
            this.labelT3.BackColor = System.Drawing.Color.Transparent;
            this.labelT3.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT3.Location = new System.Drawing.Point(12, 483);
            this.labelT3.Name = "labelT3";
            this.labelT3.Size = new System.Drawing.Size(168, 16);
            this.labelT3.TabIndex = 0;
            this.labelT3.Text = "系統管理員(BM)初始化";
            // 
            // labelT4
            // 
            this.labelT4.AutoSize = true;
            this.labelT4.BackColor = System.Drawing.Color.Transparent;
            this.labelT4.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT4.Location = new System.Drawing.Point(12, 562);
            this.labelT4.Name = "labelT4";
            this.labelT4.Size = new System.Drawing.Size(160, 16);
            this.labelT4.TabIndex = 0;
            this.labelT4.Text = "重設報表紙格式(122)";
            // 
            // labelT6
            // 
            this.labelT6.AutoSize = true;
            this.labelT6.BackColor = System.Drawing.Color.Transparent;
            this.labelT6.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT6.Location = new System.Drawing.Point(14, 156);
            this.labelT6.Margin = new System.Windows.Forms.Padding(20, 0, 5, 0);
            this.labelT6.Name = "labelT6";
            this.labelT6.Size = new System.Drawing.Size(176, 16);
            this.labelT6.TabIndex = 0;
            this.labelT6.Text = "資料庫位址-回復(.bak)";
            // 
            // DabaseAddress
            // 
            this.DabaseAddress.AllowGrayBackColor = true;
            this.DabaseAddress.AllowResize = true;
            this.DabaseAddress.BackColor = System.Drawing.Color.Silver;
            this.DabaseAddress.Font = new System.Drawing.Font("細明體", 12F);
            this.DabaseAddress.Location = new System.Drawing.Point(17, 394);
            this.DabaseAddress.MaxLength = 100;
            this.DabaseAddress.Name = "DabaseAddress";
            this.DabaseAddress.oLen = 0;
            this.DabaseAddress.ReadOnly = true;
            this.DabaseAddress.Size = new System.Drawing.Size(807, 27);
            this.DabaseAddress.TabIndex = 54;
            this.DabaseAddress.TabStop = false;
            this.DabaseAddress.Visible = false;
            // 
            // RestoreAddress
            // 
            this.RestoreAddress.AllowGrayBackColor = false;
            this.RestoreAddress.AllowResize = true;
            this.RestoreAddress.Font = new System.Drawing.Font("細明體", 12F);
            this.RestoreAddress.Location = new System.Drawing.Point(15, 182);
            this.RestoreAddress.MaxLength = 100;
            this.RestoreAddress.Name = "RestoreAddress";
            this.RestoreAddress.oLen = 0;
            this.RestoreAddress.Size = new System.Drawing.Size(807, 27);
            this.RestoreAddress.TabIndex = 55;
            // 
            // Browse_btn
            // 
            this.Browse_btn.AutoSize = true;
            this.Browse_btn.Font = new System.Drawing.Font("細明體", 12F);
            this.Browse_btn.ForeColor = System.Drawing.Color.Black;
            this.Browse_btn.Location = new System.Drawing.Point(828, 183);
            this.Browse_btn.Name = "Browse_btn";
            this.Browse_btn.Size = new System.Drawing.Size(75, 26);
            this.Browse_btn.TabIndex = 57;
            this.Browse_btn.Text = "瀏覽";
            this.Browse_btn.UseVisualStyleBackColor = true;
            this.Browse_btn.Click += new System.EventHandler(this.Browse_btn_Click);
            // 
            // labelT5
            // 
            this.labelT5.AutoSize = true;
            this.labelT5.BackColor = System.Drawing.Color.Transparent;
            this.labelT5.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT5.Location = new System.Drawing.Point(30, 400);
            this.labelT5.Name = "labelT5";
            this.labelT5.Size = new System.Drawing.Size(88, 16);
            this.labelT5.TabIndex = 0;
            this.labelT5.Text = "資料庫位址";
            this.labelT5.Visible = false;
            // 
            // buttonSmallT1
            // 
            this.buttonSmallT1.AutoSize = true;
            this.buttonSmallT1.Font = new System.Drawing.Font("細明體", 12F);
            this.buttonSmallT1.ForeColor = System.Drawing.Color.Black;
            this.buttonSmallT1.Location = new System.Drawing.Point(131, 395);
            this.buttonSmallT1.Name = "buttonSmallT1";
            this.buttonSmallT1.Size = new System.Drawing.Size(159, 26);
            this.buttonSmallT1.TabIndex = 59;
            this.buttonSmallT1.Text = "資料庫-備份";
            this.buttonSmallT1.UseVisualStyleBackColor = true;
            this.buttonSmallT1.Visible = false;
            this.buttonSmallT1.Click += new System.EventHandler(this.buttonSmallT1_Click);
            // 
            // buttonSmallT2
            // 
            this.buttonSmallT2.AutoSize = true;
            this.buttonSmallT2.Font = new System.Drawing.Font("細明體", 12F);
            this.buttonSmallT2.ForeColor = System.Drawing.Color.Black;
            this.buttonSmallT2.Location = new System.Drawing.Point(15, 227);
            this.buttonSmallT2.Name = "buttonSmallT2";
            this.buttonSmallT2.Size = new System.Drawing.Size(106, 26);
            this.buttonSmallT2.TabIndex = 60;
            this.buttonSmallT2.Text = "資料庫-恢復";
            this.buttonSmallT2.UseVisualStyleBackColor = true;
            this.buttonSmallT2.Click += new System.EventHandler(this.buttonSmallT2_Click);
            // 
            // FrmSystemSetmore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.buttonSmallT2);
            this.Controls.Add(this.buttonSmallT1);
            this.Controls.Add(this.labelT5);
            this.Controls.Add(this.Browse_btn);
            this.Controls.Add(this.RestoreAddress);
            this.Controls.Add(this.labelT6);
            this.Controls.Add(this.DabaseAddress);
            this.Controls.Add(this.labelT4);
            this.Controls.Add(this.labelT3);
            this.Controls.Add(this.btnResetAdmin);
            this.Controls.Add(this.btnResetReportFormat);
            this.Controls.Add(this.panelNT2);
            this.Controls.Add(this.labelT2);
            this.Controls.Add(this.panelNT1);
            this.Controls.Add(this.labelT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelBtnT1);
            this.Name = "FrmSystemSetmore";
            this.Text = "系統參數設定";
            this.Load += new System.EventHandler(this.FrmSystemSetmore_Load);
            this.panelBtnT1.ResumeLayout(false);
            this.panelNT1.ResumeLayout(false);
            this.panelNT1.PerformLayout();
            this.panelNT2.ResumeLayout(false);
            this.panelNT2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.PanelNT panelNT1;
        private JE.MyControl.RadioT X3Forward2;
        private JE.MyControl.RadioT X3Forward1;
        private JE.MyControl.PanelNT panelNT2;
        private JE.MyControl.RadioT InvUsed2;
        private JE.MyControl.RadioT InvUsed1;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.ButtonSmallT btnResetReportFormat;
        private JE.MyControl.ButtonSmallT btnResetAdmin;
        private JE.MyControl.LabelT labelT3;
        private JE.MyControl.LabelT labelT4;
        private JE.MyControl.LabelT labelT6;
        private JE.MyControl.TextBoxT DabaseAddress;
        private JE.MyControl.TextBoxT RestoreAddress;
        private JE.MyControl.ButtonSmallT Browse_btn;
        private JE.MyControl.LabelT labelT5;
        private JE.MyControl.ButtonSmallT buttonSmallT1;
        private JE.MyControl.ButtonSmallT buttonSmallT2;
    }
}