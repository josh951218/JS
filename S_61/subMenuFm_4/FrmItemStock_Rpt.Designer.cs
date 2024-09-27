namespace S_61.subMenuFm_4
{
    partial class FrmItemStock_Rpt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItemStock_Rpt));
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.panelNT1 = new JE.MyControl.PanelNT();
            this.rdAvgByAllStk = new JE.MyControl.RadioT();
            this.rdAvgByOneStk = new JE.MyControl.RadioT();
            this.StName = new JE.MyControl.TextBoxT();
            this.StNo = new JE.MyControl.TextBoxT();
            this.ItNo1 = new JE.MyControl.TextBoxT();
            this.ItNo = new JE.MyControl.TextBoxT();
            this.date1 = new JE.MyControl.TextBoxT();
            this.lblT10 = new JE.MyControl.LabelT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT12 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.date = new JE.MyControl.TextBoxT();
            this.lblT11 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.lblT8 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlBoxT1.SuspendLayout();
            this.panelNT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.panelNT1);
            this.pnlBoxT1.Controls.Add(this.StName);
            this.pnlBoxT1.Controls.Add(this.StNo);
            this.pnlBoxT1.Controls.Add(this.ItNo1);
            this.pnlBoxT1.Controls.Add(this.ItNo);
            this.pnlBoxT1.Controls.Add(this.date1);
            this.pnlBoxT1.Controls.Add(this.lblT10);
            this.pnlBoxT1.Controls.Add(this.lblT9);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.lblT12);
            this.pnlBoxT1.Controls.Add(this.lblT5);
            this.pnlBoxT1.Controls.Add(this.date);
            this.pnlBoxT1.Controls.Add(this.lblT11);
            this.pnlBoxT1.Location = new System.Drawing.Point(133, 53);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(745, 328);
            this.pnlBoxT1.TabIndex = 0;
            // 
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Controls.Add(this.rdAvgByAllStk);
            this.panelNT1.Controls.Add(this.rdAvgByOneStk);
            this.panelNT1.Location = new System.Drawing.Point(282, 253);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(319, 40);
            this.panelNT1.TabIndex = 8;
            // 
            // rdAvgByAllStk
            // 
            this.rdAvgByAllStk.AutoSize = true;
            this.rdAvgByAllStk.BackColor = System.Drawing.Color.LightBlue;
            this.rdAvgByAllStk.Checked = true;
            this.rdAvgByAllStk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdAvgByAllStk.Font = new System.Drawing.Font("細明體", 12F);
            this.rdAvgByAllStk.Location = new System.Drawing.Point(8, 10);
            this.rdAvgByAllStk.Name = "rdAvgByAllStk";
            this.rdAvgByAllStk.Size = new System.Drawing.Size(138, 20);
            this.rdAvgByAllStk.TabIndex = 6;
            this.rdAvgByAllStk.Tag = "全倉月平均成本";
            this.rdAvgByAllStk.Text = "全倉月平均成本";
            this.rdAvgByAllStk.UseVisualStyleBackColor = false;
            // 
            // rdAvgByOneStk
            // 
            this.rdAvgByOneStk.AutoSize = true;
            this.rdAvgByOneStk.BackColor = System.Drawing.Color.Transparent;
            this.rdAvgByOneStk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdAvgByOneStk.Font = new System.Drawing.Font("細明體", 12F);
            this.rdAvgByOneStk.Location = new System.Drawing.Point(165, 10);
            this.rdAvgByOneStk.Name = "rdAvgByOneStk";
            this.rdAvgByOneStk.Size = new System.Drawing.Size(138, 20);
            this.rdAvgByOneStk.TabIndex = 7;
            this.rdAvgByOneStk.Tag = "各倉月平均成本";
            this.rdAvgByOneStk.Text = "各倉月平均成本";
            this.rdAvgByOneStk.UseVisualStyleBackColor = false;
            // 
            // StName
            // 
            this.StName.AllowGrayBackColor = true;
            this.StName.AllowResize = true;
            this.StName.BackColor = System.Drawing.Color.Silver;
            this.StName.Font = new System.Drawing.Font("細明體", 12F);
            this.StName.Location = new System.Drawing.Point(478, 208);
            this.StName.MaxLength = 10;
            this.StName.Name = "StName";
            this.StName.oLen = 0;
            this.StName.ReadOnly = true;
            this.StName.Size = new System.Drawing.Size(87, 27);
            this.StName.TabIndex = 5;
            this.StName.TabStop = false;
            this.StName.Tag = "";
            // 
            // StNo
            // 
            this.StNo.AllowGrayBackColor = false;
            this.StNo.AllowResize = true;
            this.StNo.Font = new System.Drawing.Font("細明體", 12F);
            this.StNo.Location = new System.Drawing.Point(391, 208);
            this.StNo.MaxLength = 10;
            this.StNo.Name = "StNo";
            this.StNo.oLen = 0;
            this.StNo.Size = new System.Drawing.Size(87, 27);
            this.StNo.TabIndex = 4;
            this.StNo.Tag = "";
            this.StNo.DoubleClick += new System.EventHandler(this.StNo_DoubleClick);
            this.StNo.Validating += new System.ComponentModel.CancelEventHandler(this.StNo_Validating);
            // 
            // ItNo1
            // 
            this.ItNo1.AllowGrayBackColor = false;
            this.ItNo1.AllowResize = true;
            this.ItNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo1.Location = new System.Drawing.Point(392, 154);
            this.ItNo1.MaxLength = 20;
            this.ItNo1.Name = "ItNo1";
            this.ItNo1.oLen = 0;
            this.ItNo1.Size = new System.Drawing.Size(167, 27);
            this.ItNo1.TabIndex = 3;
            this.ItNo1.Tag = "產品編號";
            this.ItNo1.DoubleClick += new System.EventHandler(this.ItNo_DoubleClick);
            this.ItNo1.Validating += new System.ComponentModel.CancelEventHandler(this.ItNo_Validating);
            // 
            // ItNo
            // 
            this.ItNo.AllowGrayBackColor = false;
            this.ItNo.AllowResize = true;
            this.ItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo.Location = new System.Drawing.Point(392, 100);
            this.ItNo.MaxLength = 20;
            this.ItNo.Name = "ItNo";
            this.ItNo.oLen = 0;
            this.ItNo.Size = new System.Drawing.Size(167, 27);
            this.ItNo.TabIndex = 2;
            this.ItNo.Tag = "產品編號";
            this.ItNo.DoubleClick += new System.EventHandler(this.ItNo_DoubleClick);
            this.ItNo.Validating += new System.ComponentModel.CancelEventHandler(this.ItNo_Validating);
            // 
            // date1
            // 
            this.date1.AllowGrayBackColor = false;
            this.date1.AllowResize = true;
            this.date1.Font = new System.Drawing.Font("細明體", 12F);
            this.date1.Location = new System.Drawing.Point(487, 46);
            this.date1.MaxLength = 8;
            this.date1.Name = "date1";
            this.date1.oLen = 0;
            this.date1.Size = new System.Drawing.Size(71, 27);
            this.date1.TabIndex = 1;
            this.date1.Tag = "異動日期";
            this.date1.Validating += new System.ComponentModel.CancelEventHandler(this.date_Validating);
            // 
            // lblT10
            // 
            this.lblT10.AutoSize = true;
            this.lblT10.BackColor = System.Drawing.Color.Transparent;
            this.lblT10.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT10.Location = new System.Drawing.Point(283, 213);
            this.lblT10.Name = "lblT10";
            this.lblT10.Size = new System.Drawing.Size(104, 16);
            this.lblT10.TabIndex = 0;
            this.lblT10.Text = "倉        庫";
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(283, 159);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(104, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "終止產品編號";
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(283, 51);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(104, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "起迄異動日期";
            // 
            // lblT12
            // 
            this.lblT12.AutoSize = true;
            this.lblT12.BackColor = System.Drawing.Color.Transparent;
            this.lblT12.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT12.Location = new System.Drawing.Point(463, 51);
            this.lblT12.Name = "lblT12";
            this.lblT12.Size = new System.Drawing.Size(24, 16);
            this.lblT12.TabIndex = 0;
            this.lblT12.Text = "～";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(283, 105);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(104, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "起迄產品編號";
            // 
            // date
            // 
            this.date.AllowGrayBackColor = false;
            this.date.AllowResize = true;
            this.date.Font = new System.Drawing.Font("細明體", 12F);
            this.date.Location = new System.Drawing.Point(392, 46);
            this.date.MaxLength = 8;
            this.date.Name = "date";
            this.date.oLen = 0;
            this.date.Size = new System.Drawing.Size(71, 27);
            this.date.TabIndex = 0;
            this.date.Tag = "異動日期";
            this.date.Validating += new System.ComponentModel.CancelEventHandler(this.date_Validating);
            // 
            // lblT11
            // 
            this.lblT11.AutoSize = true;
            this.lblT11.BackColor = System.Drawing.Color.Transparent;
            this.lblT11.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT11.Location = new System.Drawing.Point(144, 132);
            this.lblT11.Name = "lblT11";
            this.lblT11.Size = new System.Drawing.Size(88, 16);
            this.lblT11.TabIndex = 0;
            this.lblT11.Text = "請輸入列印";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(361, 406);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(288, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "※起始編號空白表示從第一筆列印.....";
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(361, 430);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(288, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "※終止編號空白表示列印至最後一筆...";
            // 
            // lblT8
            // 
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(361, 454);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(288, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "※兩者皆空白表示全部列印...........";
            // 
            // panelT1
            // 
            this.panelT1.AutoSize = true;
            this.panelT1.BackColor = System.Drawing.Color.Transparent;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(431, 539);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 82);
            this.panelT1.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 5F);
            this.btnExit.Location = new System.Drawing.Point(69, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 1;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnBrow
            // 
            this.btnBrow.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrow.BackgroundImage")));
            this.btnBrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrow.Font = new System.Drawing.Font("Times New Roman", 5F);
            this.btnBrow.Location = new System.Drawing.Point(0, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 0;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
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
            // FrmItemStock_Rpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.lblT8);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmItemStock_Rpt";
            this.Tag = "產品庫存明細查詢";
            this.Text = "產品庫存明細表查詢";
            this.Load += new System.EventHandler(this.FrmItemStock_Rpt_Load);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelNT1.ResumeLayout(false);
            this.panelNT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.LabelT lblT10;
        private JE.MyControl.LabelT lblT11;
        private JE.MyControl.TextBoxT date1;
        private JE.MyControl.TextBoxT ItNo;
        private JE.MyControl.TextBoxT ItNo1;
        private JE.MyControl.LabelT lblT12;
        private JE.MyControl.TextBoxT date;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.TextBoxT StNo;
        private JE.MyControl.TextBoxT StName;
        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private JE.MyControl.PanelNT panelNT1;
        private JE.MyControl.RadioT rdAvgByAllStk;
        private JE.MyControl.RadioT rdAvgByOneStk;
    }
}