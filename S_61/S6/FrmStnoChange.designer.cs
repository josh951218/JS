namespace JBS.S6
{
    partial class FrmStnoChange
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
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCancel = new JE.MyControl.ButtonSmallT();
            this.btnSave = new JE.MyControl.ButtonSmallT();
            this.panelT2 = new JE.MyControl.PanelT();
            this.NStNo = new JE.MyControl.TextBoxT();
            this.labelT6 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelT();
            this.StName = new JE.MyControl.TextBoxT();
            this.BStNo = new JE.MyControl.TextBoxT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.statusStrip1.SuspendLayout();
            this.panelT2.SuspendLayout();
            this.panelT1.SuspendLayout();
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
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCancel.Location = new System.Drawing.Point(517, 549);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(167, 42);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "放棄";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("細明體", 12F);
            this.btnSave.Location = new System.Drawing.Point(326, 549);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(167, 42);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "儲存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelT2
            // 
            this.panelT2.Controls.Add(this.NStNo);
            this.panelT2.Controls.Add(this.labelT6);
            this.panelT2.Location = new System.Drawing.Point(175, 379);
            this.panelT2.Name = "panelT2";
            this.panelT2.Padding = new System.Windows.Forms.Padding(15);
            this.panelT2.Size = new System.Drawing.Size(660, 73);
            this.panelT2.TabIndex = 1;
            // 
            // NStNo
            // 
            this.NStNo.AllowGrayBackColor = false;
            this.NStNo.AllowResize = true;
            this.NStNo.Font = new System.Drawing.Font("細明體", 12F);
            this.NStNo.Location = new System.Drawing.Point(149, 22);
            this.NStNo.MaxLength = 10;
            this.NStNo.Name = "NStNo";
            this.NStNo.oLen = 0;
            this.NStNo.Size = new System.Drawing.Size(87, 27);
            this.NStNo.TabIndex = 2;
            this.NStNo.Validating += new System.ComponentModel.CancelEventHandler(this.NStNo_Validating);
            // 
            // labelT6
            // 
            this.labelT6.AutoSize = true;
            this.labelT6.BackColor = System.Drawing.Color.Transparent;
            this.labelT6.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT6.Location = new System.Drawing.Point(51, 25);
            this.labelT6.Name = "labelT6";
            this.labelT6.Size = new System.Drawing.Size(88, 16);
            this.labelT6.TabIndex = 0;
            this.labelT6.Text = "新倉庫編號";
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.StName);
            this.panelT1.Controls.Add(this.BStNo);
            this.panelT1.Controls.Add(this.labelT2);
            this.panelT1.Controls.Add(this.labelT1);
            this.panelT1.Location = new System.Drawing.Point(175, 76);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(660, 235);
            this.panelT1.TabIndex = 0;
            // 
            // StName
            // 
            this.StName.AllowGrayBackColor = true;
            this.StName.AllowResize = true;
            this.StName.BackColor = System.Drawing.Color.Silver;
            this.StName.Enabled = false;
            this.StName.Font = new System.Drawing.Font("細明體", 12F);
            this.StName.Location = new System.Drawing.Point(149, 150);
            this.StName.MaxLength = 20;
            this.StName.Name = "StName";
            this.StName.oLen = 0;
            this.StName.ReadOnly = true;
            this.StName.Size = new System.Drawing.Size(167, 27);
            this.StName.TabIndex = 1;
            this.StName.TabStop = false;
            // 
            // BStNo
            // 
            this.BStNo.AllowGrayBackColor = false;
            this.BStNo.AllowResize = true;
            this.BStNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BStNo.Location = new System.Drawing.Point(149, 60);
            this.BStNo.MaxLength = 10;
            this.BStNo.Name = "BStNo";
            this.BStNo.oLen = 0;
            this.BStNo.Size = new System.Drawing.Size(87, 27);
            this.BStNo.TabIndex = 0;
            this.BStNo.DoubleClick += new System.EventHandler(this.BStNo_DoubleClick);
            this.BStNo.Validating += new System.ComponentModel.CancelEventHandler(this.BStNo_Validating);
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(51, 153);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(72, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "倉庫名稱";
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(51, 63);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "倉庫編號";
            // 
            // FrmStnoChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelT2);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmStnoChange";
            this.Tag = "倉庫編號修改";
            this.Text = "倉庫編號修改作業";
            this.Load += new System.EventHandler(this.FrmStnoChange_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelT2.ResumeLayout(false);
            this.panelT2.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.panelT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private JE.MyControl.ButtonSmallT btnCancel;
        private JE.MyControl.ButtonSmallT btnSave;
        private JE.MyControl.PanelT panelT2;
        private JE.MyControl.TextBoxT NStNo;
        private JE.MyControl.LabelT labelT6;
        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.TextBoxT StName;
        private JE.MyControl.TextBoxT BStNo;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.LabelT labelT1;

    }
}