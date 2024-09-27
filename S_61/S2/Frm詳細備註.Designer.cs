namespace S_61.S1
{
    partial class Frm詳細備註
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm詳細備註));
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.RTB = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.panelNT1 = new JE.MyControl.PanelNT();
            this.panelT1.SuspendLayout();
            this.panelNT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelT1
            // 
            this.panelT1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panelT1.BackColor = System.Drawing.Color.Transparent;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Location = new System.Drawing.Point(466, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(79, 79);
            this.panelT1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 9F);
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 12;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // RTB
            // 
            this.RTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTB.Font = new System.Drawing.Font("細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RTB.Location = new System.Drawing.Point(0, 0);
            this.RTB.Name = "RTB";
            this.RTB.Size = new System.Drawing.Size(1008, 537);
            this.RTB.TabIndex = 26;
            this.RTB.Text = "";
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
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Controls.Add(this.RTB);
            this.panelNT1.Location = new System.Drawing.Point(0, 0);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(1010, 539);
            this.panelNT1.TabIndex = 1;
            // 
            // FrmOrder_Memob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelNT1);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmOrder_Memob";
            this.Text = "詳細備註";
            this.Load += new System.EventHandler(this.Frm詳細備註_Load);
            this.panelT1.ResumeLayout(false);
            this.panelNT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private System.Windows.Forms.RichTextBox RTB;
        private JE.MyControl.StatusStripT statusStrip1;
        private JE.MyControl.PanelNT panelNT1;
    }
}