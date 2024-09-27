namespace S_61.S0
{
    partial class FrmNFactCMS_Rpt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNFactCMS_Rpt));
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelT1 = new JE.MyControl.PanelT();
            this.FaName1 = new JE.MyControl.TextBoxT();
            this.FaNo = new JE.MyControl.TextBoxT();
            this.labelT3 = new JE.MyControl.LabelT();
            this.Day1 = new JE.MyControl.TextBoxT();
            this.Day = new JE.MyControl.TextBoxT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnPreView = new JE.MyControl.ButtonT();
            this.btnPrint = new JE.MyControl.ButtonT();
            this.panelT1.SuspendLayout();
            this.panelBtnT1.SuspendLayout();
            this.SuspendLayout();
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
            // panelT1
            // 
            this.panelT1.Controls.Add(this.FaName1);
            this.panelT1.Controls.Add(this.FaNo);
            this.panelT1.Controls.Add(this.labelT3);
            this.panelT1.Controls.Add(this.Day1);
            this.panelT1.Controls.Add(this.Day);
            this.panelT1.Controls.Add(this.labelT2);
            this.panelT1.Controls.Add(this.labelT1);
            this.panelT1.Location = new System.Drawing.Point(258, 161);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(494, 225);
            this.panelT1.TabIndex = 2;
            // 
            // FaName1
            // 
            this.FaName1.AllowGrayBackColor = true;
            this.FaName1.AllowResize = true;
            this.FaName1.BackColor = System.Drawing.Color.Silver;
            this.FaName1.Font = new System.Drawing.Font("細明體", 12F);
            this.FaName1.Location = new System.Drawing.Point(273, 122);
            this.FaName1.MaxLength = 10;
            this.FaName1.Name = "FaName1";
            this.FaName1.oLen = 0;
            this.FaName1.ReadOnly = true;
            this.FaName1.Size = new System.Drawing.Size(87, 27);
            this.FaName1.TabIndex = 4;
            this.FaName1.TabStop = false;
            // 
            // FaNo
            // 
            this.FaNo.AllowGrayBackColor = false;
            this.FaNo.AllowResize = true;
            this.FaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.FaNo.Location = new System.Drawing.Point(180, 122);
            this.FaNo.MaxLength = 10;
            this.FaNo.Name = "FaNo";
            this.FaNo.oLen = 0;
            this.FaNo.Size = new System.Drawing.Size(87, 27);
            this.FaNo.TabIndex = 3;
            this.FaNo.DoubleClick += new System.EventHandler(this.FaNo_DoubleClick);
            this.FaNo.Validating += new System.ComponentModel.CancelEventHandler(this.FaNo_Validating);
            // 
            // labelT3
            // 
            this.labelT3.AutoSize = true;
            this.labelT3.BackColor = System.Drawing.Color.Transparent;
            this.labelT3.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT3.Location = new System.Drawing.Point(101, 127);
            this.labelT3.Name = "labelT3";
            this.labelT3.Size = new System.Drawing.Size(72, 16);
            this.labelT3.TabIndex = 0;
            this.labelT3.Text = "廠商編號";
            // 
            // Day1
            // 
            this.Day1.AllowGrayBackColor = false;
            this.Day1.AllowResize = true;
            this.Day1.Font = new System.Drawing.Font("細明體", 12F);
            this.Day1.Location = new System.Drawing.Point(307, 76);
            this.Day1.MaxLength = 10;
            this.Day1.Name = "Day1";
            this.Day1.oLen = 0;
            this.Day1.Size = new System.Drawing.Size(87, 27);
            this.Day1.TabIndex = 2;
            this.Day1.Validating += new System.ComponentModel.CancelEventHandler(this.Day_Validating);
            // 
            // Day
            // 
            this.Day.AllowGrayBackColor = false;
            this.Day.AllowResize = true;
            this.Day.Font = new System.Drawing.Font("細明體", 12F);
            this.Day.Location = new System.Drawing.Point(180, 76);
            this.Day.MaxLength = 10;
            this.Day.Name = "Day";
            this.Day.oLen = 0;
            this.Day.Size = new System.Drawing.Size(87, 27);
            this.Day.TabIndex = 1;
            this.Day.Validating += new System.ComponentModel.CancelEventHandler(this.Day_Validating);
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(275, 81);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(24, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "～";
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(101, 81);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "起訖日期";
            // 
            // panelBtnT1
            // 
            this.panelBtnT1.Controls.Add(this.btnExit);
            this.panelBtnT1.Controls.Add(this.btnPreView);
            this.panelBtnT1.Controls.Add(this.btnPrint);
            this.panelBtnT1.Location = new System.Drawing.Point(397, 544);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(217, 79);
            this.panelBtnT1.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(138, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 2;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPreView
            // 
            this.btnPreView.BackColor = System.Drawing.SystemColors.Control;
            this.btnPreView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreView.BackgroundImage")));
            this.btnPreView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPreView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPreView.Location = new System.Drawing.Point(69, 0);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(69, 79);
            this.btnPreView.TabIndex = 1;
            this.btnPreView.UseDefaultSettings = false;
            this.btnPreView.UseVisualStyleBackColor = false;
            this.btnPreView.Click += new System.EventHandler(this.btnPreView_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(69, 79);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.UseDefaultSettings = false;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            this.btnPrint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPrint_MouseDown);
            // 
            // FrmNFactCMS_Rpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "FrmNFactCMS_Rpt";
            this.Text = "寄售廠商對帳表";
            this.Load += new System.EventHandler(this.FrmNFactCMS_Rpt_Load);
            this.panelT1.ResumeLayout(false);
            this.panelT1.PerformLayout();
            this.panelBtnT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.TextBoxT FaNo;
        private JE.MyControl.LabelT labelT3;
        private JE.MyControl.TextBoxT Day1;
        private JE.MyControl.TextBoxT Day;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnPreView;
        private JE.MyControl.ButtonT btnPrint;
        private JE.MyControl.TextBoxT FaName1;
    }
}