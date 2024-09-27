namespace JBS.S6
{
    partial class FrmFanoChange
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
            this.NFaNo = new JE.MyControl.TextBoxT();
            this.labelT6 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelT();
            this.FaAddr1 = new JE.MyControl.TextBoxT();
            this.FaPer1 = new JE.MyControl.TextBoxT();
            this.FaTel1 = new JE.MyControl.TextBoxT();
            this.FaName2 = new JE.MyControl.TextBoxT();
            this.BFaNo = new JE.MyControl.TextBoxT();
            this.labelT5 = new JE.MyControl.LabelT();
            this.labelT4 = new JE.MyControl.LabelT();
            this.labelT3 = new JE.MyControl.LabelT();
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
            this.btnCancel.Location = new System.Drawing.Point(521, 549);
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
            this.panelT2.Controls.Add(this.NFaNo);
            this.panelT2.Controls.Add(this.labelT6);
            this.panelT2.Location = new System.Drawing.Point(175, 353);
            this.panelT2.Name = "panelT2";
            this.panelT2.Padding = new System.Windows.Forms.Padding(15);
            this.panelT2.Size = new System.Drawing.Size(660, 73);
            this.panelT2.TabIndex = 1;
            // 
            // NFaNo
            // 
            this.NFaNo.AllowGrayBackColor = false;
            this.NFaNo.AllowResize = true;
            this.NFaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.NFaNo.Location = new System.Drawing.Point(149, 22);
            this.NFaNo.MaxLength = 10;
            this.NFaNo.Name = "NFaNo";
            this.NFaNo.oLen = 0;
            this.NFaNo.Size = new System.Drawing.Size(87, 27);
            this.NFaNo.TabIndex = 5;
            this.NFaNo.Validating += new System.ComponentModel.CancelEventHandler(this.NFaNo_Validating);
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
            this.labelT6.Text = "新廠商編號";
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.FaAddr1);
            this.panelT1.Controls.Add(this.FaPer1);
            this.panelT1.Controls.Add(this.FaTel1);
            this.panelT1.Controls.Add(this.FaName2);
            this.panelT1.Controls.Add(this.BFaNo);
            this.panelT1.Controls.Add(this.labelT5);
            this.panelT1.Controls.Add(this.labelT4);
            this.panelT1.Controls.Add(this.labelT3);
            this.panelT1.Controls.Add(this.labelT2);
            this.panelT1.Controls.Add(this.labelT1);
            this.panelT1.Location = new System.Drawing.Point(175, 50);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(660, 235);
            this.panelT1.TabIndex = 0;
            // 
            // FaAddr1
            // 
            this.FaAddr1.AllowGrayBackColor = true;
            this.FaAddr1.AllowResize = true;
            this.FaAddr1.BackColor = System.Drawing.Color.Silver;
            this.FaAddr1.Enabled = false;
            this.FaAddr1.Font = new System.Drawing.Font("細明體", 12F);
            this.FaAddr1.Location = new System.Drawing.Point(149, 181);
            this.FaAddr1.MaxLength = 60;
            this.FaAddr1.Name = "FaAddr1";
            this.FaAddr1.oLen = 0;
            this.FaAddr1.ReadOnly = true;
            this.FaAddr1.Size = new System.Drawing.Size(487, 27);
            this.FaAddr1.TabIndex = 4;
            this.FaAddr1.TabStop = false;
            // 
            // FaPer1
            // 
            this.FaPer1.AllowGrayBackColor = true;
            this.FaPer1.AllowResize = true;
            this.FaPer1.BackColor = System.Drawing.Color.Silver;
            this.FaPer1.Enabled = false;
            this.FaPer1.Font = new System.Drawing.Font("細明體", 12F);
            this.FaPer1.Location = new System.Drawing.Point(149, 141);
            this.FaPer1.MaxLength = 20;
            this.FaPer1.Name = "FaPer1";
            this.FaPer1.oLen = 0;
            this.FaPer1.ReadOnly = true;
            this.FaPer1.Size = new System.Drawing.Size(167, 27);
            this.FaPer1.TabIndex = 3;
            this.FaPer1.TabStop = false;
            // 
            // FaTel1
            // 
            this.FaTel1.AllowGrayBackColor = true;
            this.FaTel1.AllowResize = true;
            this.FaTel1.BackColor = System.Drawing.Color.Silver;
            this.FaTel1.Enabled = false;
            this.FaTel1.Font = new System.Drawing.Font("細明體", 12F);
            this.FaTel1.Location = new System.Drawing.Point(149, 103);
            this.FaTel1.MaxLength = 20;
            this.FaTel1.Name = "FaTel1";
            this.FaTel1.oLen = 0;
            this.FaTel1.ReadOnly = true;
            this.FaTel1.Size = new System.Drawing.Size(167, 27);
            this.FaTel1.TabIndex = 2;
            this.FaTel1.TabStop = false;
            // 
            // FaName2
            // 
            this.FaName2.AllowGrayBackColor = true;
            this.FaName2.AllowResize = true;
            this.FaName2.BackColor = System.Drawing.Color.Silver;
            this.FaName2.Enabled = false;
            this.FaName2.Font = new System.Drawing.Font("細明體", 12F);
            this.FaName2.Location = new System.Drawing.Point(149, 66);
            this.FaName2.MaxLength = 50;
            this.FaName2.Name = "FaName2";
            this.FaName2.oLen = 0;
            this.FaName2.ReadOnly = true;
            this.FaName2.Size = new System.Drawing.Size(407, 27);
            this.FaName2.TabIndex = 1;
            this.FaName2.TabStop = false;
            // 
            // BFaNo
            // 
            this.BFaNo.AllowGrayBackColor = false;
            this.BFaNo.AllowResize = true;
            this.BFaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BFaNo.Location = new System.Drawing.Point(149, 25);
            this.BFaNo.MaxLength = 10;
            this.BFaNo.Name = "BFaNo";
            this.BFaNo.oLen = 0;
            this.BFaNo.Size = new System.Drawing.Size(87, 27);
            this.BFaNo.TabIndex = 0;
            this.BFaNo.DoubleClick += new System.EventHandler(this.BFaNo_DoubleClick);
            this.BFaNo.Validating += new System.ComponentModel.CancelEventHandler(this.BFaNo_Validating);
            // 
            // labelT5
            // 
            this.labelT5.AutoSize = true;
            this.labelT5.BackColor = System.Drawing.Color.Transparent;
            this.labelT5.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT5.Location = new System.Drawing.Point(51, 184);
            this.labelT5.Name = "labelT5";
            this.labelT5.Size = new System.Drawing.Size(72, 16);
            this.labelT5.TabIndex = 0;
            this.labelT5.Text = "公司地址";
            // 
            // labelT4
            // 
            this.labelT4.AutoSize = true;
            this.labelT4.BackColor = System.Drawing.Color.Transparent;
            this.labelT4.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT4.Location = new System.Drawing.Point(51, 144);
            this.labelT4.Name = "labelT4";
            this.labelT4.Size = new System.Drawing.Size(72, 16);
            this.labelT4.TabIndex = 0;
            this.labelT4.Text = "連絡人 1";
            // 
            // labelT3
            // 
            this.labelT3.AutoSize = true;
            this.labelT3.BackColor = System.Drawing.Color.Transparent;
            this.labelT3.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT3.Location = new System.Drawing.Point(51, 106);
            this.labelT3.Name = "labelT3";
            this.labelT3.Size = new System.Drawing.Size(56, 16);
            this.labelT3.TabIndex = 0;
            this.labelT3.Text = "電話 1";
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(51, 69);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(72, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "廠商名稱";
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(51, 28);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "廠商編號";
            // 
            // FrmFanoChange
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
            this.Name = "FrmFanoChange";
            this.Tag = "廠商編號修改";
            this.Text = "廠商編號修改作業";
            this.Load += new System.EventHandler(this.FrmFanoChange_Load);
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
        private JE.MyControl.TextBoxT NFaNo;
        private JE.MyControl.LabelT labelT6;
        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.TextBoxT FaAddr1;
        private JE.MyControl.TextBoxT FaPer1;
        private JE.MyControl.TextBoxT FaTel1;
        private JE.MyControl.TextBoxT FaName2;
        private JE.MyControl.TextBoxT BFaNo;
        private JE.MyControl.LabelT labelT5;
        private JE.MyControl.LabelT labelT4;
        private JE.MyControl.LabelT labelT3;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.LabelT labelT1;
    }
}