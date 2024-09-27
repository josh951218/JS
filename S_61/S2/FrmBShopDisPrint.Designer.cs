namespace S_61.S2
{
    partial class FrmBShopDisPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBShopDisPrint));
            this.panelT1 = new JE.MyControl.PanelT();
            this.DiNo1 = new JE.MyControl.TextBoxT();
            this.DiNo = new JE.MyControl.TextBoxT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnPreView = new JE.MyControl.ButtonT();
            this.btnPrint = new JE.MyControl.ButtonT();
            this.panelT1.SuspendLayout();
            this.panelBtnT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.DiNo1);
            this.panelT1.Controls.Add(this.DiNo);
            this.panelT1.Controls.Add(this.labelT2);
            this.panelT1.Controls.Add(this.labelT1);
            this.panelT1.Location = new System.Drawing.Point(267, 170);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(477, 185);
            this.panelT1.TabIndex = 1;
            // 
            // InNo1
            // 
            this.DiNo1.AllowGrayBackColor = false;
            this.DiNo1.AllowResize = true;
            this.DiNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.DiNo1.Location = new System.Drawing.Point(227, 110);
            this.DiNo1.MaxLength = 16;
            this.DiNo1.Name = "InNo1";
            this.DiNo1.oLen = 0;
            this.DiNo1.Size = new System.Drawing.Size(135, 27);
            this.DiNo1.TabIndex = 2;
            this.DiNo1.Validating += new System.ComponentModel.CancelEventHandler(this.InNo_Validating);
            // 
            // InNo
            // 
            this.DiNo.AllowGrayBackColor = false;
            this.DiNo.AllowResize = true;
            this.DiNo.Font = new System.Drawing.Font("細明體", 12F);
            this.DiNo.Location = new System.Drawing.Point(227, 48);
            this.DiNo.MaxLength = 16;
            this.DiNo.Name = "InNo";
            this.DiNo.oLen = 0;
            this.DiNo.Size = new System.Drawing.Size(135, 27);
            this.DiNo.TabIndex = 1;
            this.DiNo.Validating += new System.ComponentModel.CancelEventHandler(this.InNo_Validating);
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(115, 115);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(104, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "終止折讓編號";
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(115, 53);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(104, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "起始折讓編號";
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
            // panelBtnT1
            // 
            this.panelBtnT1.Controls.Add(this.btnExit);
            this.panelBtnT1.Controls.Add(this.btnPreView);
            this.panelBtnT1.Controls.Add(this.btnPrint);
            this.panelBtnT1.Location = new System.Drawing.Point(397, 544);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(217, 79);
            this.panelBtnT1.TabIndex = 2;
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
            this.btnExit.TabIndex = 3;
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
            this.btnPreView.TabIndex = 2;
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
            this.btnPrint.TabIndex = 1;
            this.btnPrint.UseDefaultSettings = false;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmBShopDisPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelBtnT1);
            this.Name = "FrmBShopDisPrint";
            this.Text = "折讓列印";
            this.Load += new System.EventHandler(this.FrmBShopDisPrint_Load);
            this.panelT1.ResumeLayout(false);
            this.panelT1.PerformLayout();
            this.panelBtnT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.TextBoxT DiNo1;
        private JE.MyControl.TextBoxT DiNo;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnPreView;
        private JE.MyControl.ButtonT btnPrint;
    }
}