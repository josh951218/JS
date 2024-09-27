namespace S_61.SOther
{
    partial class FrmPicture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPicture));
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.pic = new JE.MyControl.PictureBoxT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.panelNT1 = new JE.MyControl.PanelNT();
            this.checkBoxT1 = new JE.MyControl.CheckBoxT();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.panelBtnT1.SuspendLayout();
            this.panelNT1.SuspendLayout();
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
            // pic
            // 
            this.pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Enabled = false;
            this.pic.IsImageChange = false;
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(598, 448);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic.TabIndex = 1;
            this.pic.TabStop = false;
            // 
            // panelBtnT1
            // 
            this.panelBtnT1.Controls.Add(this.btnExit);
            this.panelBtnT1.Location = new System.Drawing.Point(466, 544);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(79, 79);
            this.panelBtnT1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 0;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Controls.Add(this.pic);
            this.panelNT1.Location = new System.Drawing.Point(205, 44);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(600, 450);
            this.panelNT1.TabIndex = 3;
            // 
            // checkBoxT1
            // 
            this.checkBoxT1.AutoSize = true;
            this.checkBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.checkBoxT1.Location = new System.Drawing.Point(206, 500);
            this.checkBoxT1.Name = "checkBoxT1";
            this.checkBoxT1.Size = new System.Drawing.Size(123, 20);
            this.checkBoxT1.TabIndex = 4;
            this.checkBoxT1.Text = "圖片是否縮放";
            this.checkBoxT1.UseVisualStyleBackColor = true;
            this.checkBoxT1.CheckedChanged += new System.EventHandler(this.checkBoxT1_CheckedChanged);
            // 
            // FrmPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.checkBoxT1);
            this.Controls.Add(this.panelNT1);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "FrmPicture";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmPicture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.panelBtnT1.ResumeLayout(false);
            this.panelNT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PictureBoxT pic;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.PanelNT panelNT1;
        private JE.MyControl.CheckBoxT checkBoxT1;
    }
}