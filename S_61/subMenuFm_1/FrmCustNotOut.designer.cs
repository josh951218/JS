namespace S_61.subMenuFm_1
{
    partial class FrmCustNotOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustNotOut));
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.CuNo = new JE.MyControl.TextBoxT();
            this.EsDate1 = new JE.MyControl.TextBoxT();
            this.CuNo1 = new JE.MyControl.TextBoxT();
            this.EsDate = new JE.MyControl.TextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.CuNo);
            this.pnlBoxT1.Controls.Add(this.EsDate1);
            this.pnlBoxT1.Controls.Add(this.CuNo1);
            this.pnlBoxT1.Controls.Add(this.EsDate);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Location = new System.Drawing.Point(200, 208);
            this.pnlBoxT1.Margin = new System.Windows.Forms.Padding(4);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(20, 19, 20, 19);
            this.pnlBoxT1.Size = new System.Drawing.Size(947, 229);
            this.pnlBoxT1.TabIndex = 0;
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(269, 136);
            this.lblT2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(129, 20);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起迄客戶編號";
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(269, 70);
            this.lblT1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(129, 20);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "起迄交貨日期";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(531, 136);
            this.lblT4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(29, 20);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "～";
            // 
            // CuNo
            // 
            this.CuNo.AllowGrayBackColor = false;
            this.CuNo.AllowResize = true;
            this.CuNo.Font = new System.Drawing.Font("細明體", 12F);
            this.CuNo.Location = new System.Drawing.Point(415, 130);
            this.CuNo.Margin = new System.Windows.Forms.Padding(4);
            this.CuNo.MaxLength = 10;
            this.CuNo.Name = "CuNo";
            this.CuNo.oLen = 0;
            this.CuNo.Size = new System.Drawing.Size(87, 31);
            this.CuNo.TabIndex = 4;
            this.CuNo.Tag = "客戶編號";
            this.CuNo.DoubleClick += new System.EventHandler(this.CuNo_DoubleClick);
            this.CuNo.Validating += new System.ComponentModel.CancelEventHandler(this.CuNo_Validating);
            // 
            // EsDate1
            // 
            this.EsDate1.AllowGrayBackColor = false;
            this.EsDate1.AllowResize = true;
            this.EsDate1.Font = new System.Drawing.Font("細明體", 12F);
            this.EsDate1.Location = new System.Drawing.Point(563, 64);
            this.EsDate1.Margin = new System.Windows.Forms.Padding(4);
            this.EsDate1.MaxLength = 10;
            this.EsDate1.Name = "EsDate1";
            this.EsDate1.oLen = 0;
            this.EsDate1.Size = new System.Drawing.Size(87, 31);
            this.EsDate1.TabIndex = 3;
            this.EsDate1.Tag = "交貨日期";
            this.EsDate1.Validating += new System.ComponentModel.CancelEventHandler(this.Date_Validating);
            // 
            // CuNo1
            // 
            this.CuNo1.AllowGrayBackColor = false;
            this.CuNo1.AllowResize = true;
            this.CuNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.CuNo1.Location = new System.Drawing.Point(563, 130);
            this.CuNo1.Margin = new System.Windows.Forms.Padding(4);
            this.CuNo1.MaxLength = 10;
            this.CuNo1.Name = "CuNo1";
            this.CuNo1.oLen = 0;
            this.CuNo1.Size = new System.Drawing.Size(87, 31);
            this.CuNo1.TabIndex = 5;
            this.CuNo1.Tag = "客戶編號";
            this.CuNo1.DoubleClick += new System.EventHandler(this.CuNo_DoubleClick);
            this.CuNo1.Validating += new System.ComponentModel.CancelEventHandler(this.CuNo_Validating);
            // 
            // EsDate
            // 
            this.EsDate.AllowGrayBackColor = false;
            this.EsDate.AllowResize = true;
            this.EsDate.Font = new System.Drawing.Font("細明體", 12F);
            this.EsDate.Location = new System.Drawing.Point(415, 64);
            this.EsDate.Margin = new System.Windows.Forms.Padding(4);
            this.EsDate.MaxLength = 10;
            this.EsDate.Name = "EsDate";
            this.EsDate.oLen = 0;
            this.EsDate.Size = new System.Drawing.Size(87, 31);
            this.EsDate.TabIndex = 2;
            this.EsDate.Tag = "交貨日期";
            this.EsDate.Validating += new System.ComponentModel.CancelEventHandler(this.Date_Validating);
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(531, 70);
            this.lblT3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(29, 20);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "～";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(487, 501);
            this.lblT5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(349, 20);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "＊起始編號空白表示從第一筆開始列印";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(487, 534);
            this.lblT6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(309, 20);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "＊終止編號空白表示印至最後一筆";
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(487, 566);
            this.lblT7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(249, 20);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "＊二者皆空白表示全部列印";
            // 
            // panelT1
            // 
            this.panelT1.AutoSize = true;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(575, 681);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 83);
            this.panelT1.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(69, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
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
            this.btnBrow.Location = new System.Drawing.Point(0, 0);
            this.btnBrow.Margin = new System.Windows.Forms.Padding(4);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 1023);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1914, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(1894, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // FrmCustNotOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1914, 1045);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.panelT1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCustNotOut";
            this.Tag = "客戶別未交貸訂單";
            this.Text = "客戶別未交貨訂單";
            this.Load += new System.EventHandler(this.FrmCustNotOut_Load);
            this.Shown += new System.EventHandler(this.FrmCustNotOut_Shown);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT EsDate;
        private JE.MyControl.TextBoxT EsDate1;
        private JE.MyControl.TextBoxT CuNo;
        private JE.MyControl.TextBoxT CuNo1;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    }
}