namespace JBS.S6
{
    partial class FrmCunoChange
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
            this.panelT1 = new JE.MyControl.PanelT();
            this.CuAddr1 = new JE.MyControl.TextBoxT();
            this.CuPer1 = new JE.MyControl.TextBoxT();
            this.CuTel1 = new JE.MyControl.TextBoxT();
            this.CuName2 = new JE.MyControl.TextBoxT();
            this.BCuNo = new JE.MyControl.TextBoxT();
            this.labelT5 = new JE.MyControl.LabelT();
            this.labelT4 = new JE.MyControl.LabelT();
            this.labelT3 = new JE.MyControl.LabelT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.panelT2 = new JE.MyControl.PanelT();
            this.NCuNo = new JE.MyControl.TextBoxT();
            this.labelT6 = new JE.MyControl.LabelT();
            this.btnCancel = new JE.MyControl.ButtonSmallT();
            this.btnSave = new JE.MyControl.ButtonSmallT();
            this.statusStrip1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.panelT2.SuspendLayout();
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
            this.panelT1.Controls.Add(this.CuAddr1);
            this.panelT1.Controls.Add(this.CuPer1);
            this.panelT1.Controls.Add(this.CuTel1);
            this.panelT1.Controls.Add(this.CuName2);
            this.panelT1.Controls.Add(this.BCuNo);
            this.panelT1.Controls.Add(this.labelT5);
            this.panelT1.Controls.Add(this.labelT4);
            this.panelT1.Controls.Add(this.labelT3);
            this.panelT1.Controls.Add(this.labelT2);
            this.panelT1.Controls.Add(this.labelT1);
            this.panelT1.Location = new System.Drawing.Point(172, 74);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(660, 235);
            this.panelT1.TabIndex = 0;
            // 
            // CuAddr1
            // 
            this.CuAddr1.AllowGrayBackColor = true;
            this.CuAddr1.AllowResize = true;
            this.CuAddr1.BackColor = System.Drawing.Color.Silver;
            this.CuAddr1.Enabled = false;
            this.CuAddr1.Font = new System.Drawing.Font("細明體", 12F);
            this.CuAddr1.Location = new System.Drawing.Point(149, 181);
            this.CuAddr1.MaxLength = 60;
            this.CuAddr1.Name = "CuAddr1";
            this.CuAddr1.oLen = 0;
            this.CuAddr1.ReadOnly = true;
            this.CuAddr1.Size = new System.Drawing.Size(487, 27);
            this.CuAddr1.TabIndex = 4;
            this.CuAddr1.TabStop = false;
            // 
            // CuPer1
            // 
            this.CuPer1.AllowGrayBackColor = true;
            this.CuPer1.AllowResize = true;
            this.CuPer1.BackColor = System.Drawing.Color.Silver;
            this.CuPer1.Enabled = false;
            this.CuPer1.Font = new System.Drawing.Font("細明體", 12F);
            this.CuPer1.Location = new System.Drawing.Point(149, 141);
            this.CuPer1.MaxLength = 20;
            this.CuPer1.Name = "CuPer1";
            this.CuPer1.oLen = 0;
            this.CuPer1.ReadOnly = true;
            this.CuPer1.Size = new System.Drawing.Size(167, 27);
            this.CuPer1.TabIndex = 3;
            this.CuPer1.TabStop = false;
            // 
            // CuTel1
            // 
            this.CuTel1.AllowGrayBackColor = true;
            this.CuTel1.AllowResize = true;
            this.CuTel1.BackColor = System.Drawing.Color.Silver;
            this.CuTel1.Enabled = false;
            this.CuTel1.Font = new System.Drawing.Font("細明體", 12F);
            this.CuTel1.Location = new System.Drawing.Point(149, 103);
            this.CuTel1.MaxLength = 20;
            this.CuTel1.Name = "CuTel1";
            this.CuTel1.oLen = 0;
            this.CuTel1.ReadOnly = true;
            this.CuTel1.Size = new System.Drawing.Size(167, 27);
            this.CuTel1.TabIndex = 2;
            this.CuTel1.TabStop = false;
            // 
            // CuName2
            // 
            this.CuName2.AllowGrayBackColor = true;
            this.CuName2.AllowResize = true;
            this.CuName2.BackColor = System.Drawing.Color.Silver;
            this.CuName2.Enabled = false;
            this.CuName2.Font = new System.Drawing.Font("細明體", 12F);
            this.CuName2.Location = new System.Drawing.Point(149, 66);
            this.CuName2.MaxLength = 50;
            this.CuName2.Name = "CuName2";
            this.CuName2.oLen = 0;
            this.CuName2.ReadOnly = true;
            this.CuName2.Size = new System.Drawing.Size(407, 27);
            this.CuName2.TabIndex = 1;
            this.CuName2.TabStop = false;
            // 
            // BCuNo
            // 
            this.BCuNo.AllowGrayBackColor = false;
            this.BCuNo.AllowResize = true;
            this.BCuNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BCuNo.Location = new System.Drawing.Point(149, 25);
            this.BCuNo.MaxLength = 10;
            this.BCuNo.Name = "BCuNo";
            this.BCuNo.oLen = 0;
            this.BCuNo.Size = new System.Drawing.Size(87, 27);
            this.BCuNo.TabIndex = 0;
            this.BCuNo.DoubleClick += new System.EventHandler(this.BCuNo_DoubleClick);
            this.BCuNo.Validating += new System.ComponentModel.CancelEventHandler(this.BCuNo_Validating);
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
            this.labelT2.Text = "客戶名稱";
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
            this.labelT1.Text = "客戶編號";
            // 
            // panelT2
            // 
            this.panelT2.Controls.Add(this.NCuNo);
            this.panelT2.Controls.Add(this.labelT6);
            this.panelT2.Location = new System.Drawing.Point(172, 377);
            this.panelT2.Name = "panelT2";
            this.panelT2.Padding = new System.Windows.Forms.Padding(15);
            this.panelT2.Size = new System.Drawing.Size(660, 73);
            this.panelT2.TabIndex = 1;
            // 
            // NCuNo
            // 
            this.NCuNo.AllowGrayBackColor = false;
            this.NCuNo.AllowResize = true;
            this.NCuNo.Font = new System.Drawing.Font("細明體", 12F);
            this.NCuNo.Location = new System.Drawing.Point(149, 22);
            this.NCuNo.MaxLength = 10;
            this.NCuNo.Name = "NCuNo";
            this.NCuNo.oLen = 0;
            this.NCuNo.Size = new System.Drawing.Size(87, 27);
            this.NCuNo.TabIndex = 5;
            this.NCuNo.Validating += new System.ComponentModel.CancelEventHandler(this.NCuNo_Validating);
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
            this.labelT6.Text = "新客戶編號";
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
            // FrmCunoChange
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
            this.Name = "FrmCunoChange";
            this.Tag = "客戶編號修改";
            this.Text = "客戶編號修改作業";
            this.Load += new System.EventHandler(this.FrmCunoChange_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.panelT1.PerformLayout();
            this.panelT2.ResumeLayout(false);
            this.panelT2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.LabelT labelT5;
        private JE.MyControl.LabelT labelT4;
        private JE.MyControl.LabelT labelT3;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.PanelT panelT2;
        private JE.MyControl.LabelT labelT6;
        private JE.MyControl.TextBoxT CuAddr1;
        private JE.MyControl.TextBoxT CuPer1;
        private JE.MyControl.TextBoxT CuTel1;
        private JE.MyControl.TextBoxT CuName2;
        private JE.MyControl.TextBoxT BCuNo;
        private JE.MyControl.TextBoxT NCuNo;
        private JE.MyControl.ButtonSmallT btnCancel;
        private JE.MyControl.ButtonSmallT btnSave;
    }
}