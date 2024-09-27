namespace JBS.S6
{
    partial class FrmFixSQLIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFixSQLIndex));
            this.btnFix = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.panelBtnT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFix
            // 
            this.btnFix.Font = new System.Drawing.Font("細明體", 12F);
            this.btnFix.Location = new System.Drawing.Point(306, 294);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(398, 61);
            this.btnFix.TabIndex = 0;
            this.btnFix.Text = "資料重整作業";
            this.btnFix.UseVisualStyleBackColor = true;
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
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
            this.panelBtnT1.Location = new System.Drawing.Point(466, 544);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(79, 79);
            this.panelBtnT1.TabIndex = 1;
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
            // FrmFixSQLIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.btnFix);
            this.Name = "FrmFixSQLIndex";
            this.Text = "資料重整作業";
            this.Load += new System.EventHandler(this.FrmFixSQLIndex_Load);
            this.panelBtnT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.ButtonSmallT btnFix;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
    }
}