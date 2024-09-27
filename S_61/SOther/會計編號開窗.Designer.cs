namespace S_61.SOther
{
    partial class 會計編號開窗
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(會計編號開窗));
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelT1 = new JE.MyControl.PanelT();
            this.ACCoNo = new JE.MyControl.TextBoxT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.ACNO = new JE.MyControl.TextBoxT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.statusStrip1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.panelBtnT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(954, 20);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatusLabel3.Text = "插入";
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.ACCoNo);
            this.panelT1.Controls.Add(this.labelT2);
            this.panelT1.Controls.Add(this.ACNO);
            this.panelT1.Controls.Add(this.labelT1);
            this.panelT1.Location = new System.Drawing.Point(172, 247);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(667, 155);
            this.panelT1.TabIndex = 2;
            // 
            // ACCoNo
            // 
            this.ACCoNo.AllowGrayBackColor = true;
            this.ACCoNo.AllowResize = true;
            this.ACCoNo.BackColor = System.Drawing.Color.Silver;
            this.ACCoNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ACCoNo.Location = new System.Drawing.Point(278, 38);
            this.ACCoNo.MaxLength = 16;
            this.ACCoNo.Name = "ACCoNo";
            this.ACCoNo.oLen = 0;
            this.ACCoNo.ReadOnly = true;
            this.ACCoNo.Size = new System.Drawing.Size(135, 27);
            this.ACCoNo.TabIndex = 3;
            this.ACCoNo.TabStop = false;
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(174, 43);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(104, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "會計公司編號";
            // 
            // ACNO
            // 
            this.ACNO.AllowGrayBackColor = true;
            this.ACNO.AllowResize = true;
            this.ACNO.BackColor = System.Drawing.Color.Silver;
            this.ACNO.Font = new System.Drawing.Font("細明體", 12F);
            this.ACNO.Location = new System.Drawing.Point(278, 90);
            this.ACNO.MaxLength = 16;
            this.ACNO.Name = "ACNO";
            this.ACNO.oLen = 0;
            this.ACNO.ReadOnly = true;
            this.ACNO.Size = new System.Drawing.Size(135, 27);
            this.ACNO.TabIndex = 1;
            this.ACNO.TabStop = false;
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(174, 95);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(104, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "會計傳票編號";
            // 
            // panelBtnT1
            // 
            this.panelBtnT1.Controls.Add(this.btnExit);
            this.panelBtnT1.Location = new System.Drawing.Point(466, 541);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(79, 79);
            this.panelBtnT1.TabIndex = 13;
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
            this.btnExit.TabIndex = 4;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // 會計編號開窗
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "會計編號開窗";
            this.Text = "會計編號開窗";
            this.Load += new System.EventHandler(this.會計編號開窗_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.panelT1.PerformLayout();
            this.panelBtnT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.TextBoxT ACCoNo;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.TextBoxT ACNO;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
    }
}