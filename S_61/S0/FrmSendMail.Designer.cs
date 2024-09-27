namespace S_61.S0
{
    partial class FrmSendMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSendMail));
            this.labelT1 = new JE.MyControl.LabelT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.labelT3 = new JE.MyControl.LabelT();
            this.SendID = new JE.MyControl.TextBoxT();
            this.SendPW = new JE.MyControl.TextBoxT();
            this.Geter = new JE.MyControl.RichTextBoxT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.panelBtnT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(102, 29);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(472, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "寄件者信箱(Yahoo.com, Hotmail.com, Outlook.com, Gmail.com)";
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(102, 92);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(88, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "寄件者密碼";
            // 
            // labelT3
            // 
            this.labelT3.AutoSize = true;
            this.labelT3.BackColor = System.Drawing.Color.Transparent;
            this.labelT3.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT3.Location = new System.Drawing.Point(102, 155);
            this.labelT3.Name = "labelT3";
            this.labelT3.Size = new System.Drawing.Size(376, 16);
            this.labelT3.TabIndex = 0;
            this.labelT3.Text = "收件者郵件信箱 (收件者多筆時，請按 Enter 換行)";
            // 
            // SendID
            // 
            this.SendID.AllowGrayBackColor = false;
            this.SendID.AllowResize = true;
            this.SendID.Font = new System.Drawing.Font("細明體", 12F);
            this.SendID.Location = new System.Drawing.Point(102, 55);
            this.SendID.MaxLength = 100;
            this.SendID.Name = "SendID";
            this.SendID.oLen = 0;
            this.SendID.Size = new System.Drawing.Size(807, 27);
            this.SendID.TabIndex = 3;
            // 
            // SendPW
            // 
            this.SendPW.AllowGrayBackColor = false;
            this.SendPW.AllowResize = true;
            this.SendPW.Font = new System.Drawing.Font("細明體", 12F);
            this.SendPW.Location = new System.Drawing.Point(102, 118);
            this.SendPW.MaxLength = 100;
            this.SendPW.Name = "SendPW";
            this.SendPW.oLen = 0;
            this.SendPW.PasswordChar = '●';
            this.SendPW.Size = new System.Drawing.Size(807, 27);
            this.SendPW.TabIndex = 4;
            // 
            // Geter
            // 
            this.Geter.Font = new System.Drawing.Font("細明體", 12F);
            this.Geter.Location = new System.Drawing.Point(102, 181);
            this.Geter.Name = "Geter";
            this.Geter.Size = new System.Drawing.Size(804, 319);
            this.Geter.TabIndex = 5;
            this.Geter.Text = "";
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
            this.panelBtnT1.TabIndex = 6;
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
            // FrmSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.Geter);
            this.Controls.Add(this.SendPW);
            this.Controls.Add(this.SendID);
            this.Controls.Add(this.labelT3);
            this.Controls.Add(this.labelT2);
            this.Controls.Add(this.labelT1);
            this.Name = "FrmSendMail";
            this.Text = "郵件設定作業";
            this.Load += new System.EventHandler(this.FrmSendMail_Load);
            this.panelBtnT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.LabelT labelT3;
        private JE.MyControl.TextBoxT SendID;
        private JE.MyControl.TextBoxT SendPW;
        private JE.MyControl.RichTextBoxT Geter;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
    }
}