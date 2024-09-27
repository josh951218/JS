namespace S_61.SOther
{
    partial class FrmBarCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBarCode));
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.radioT3 = new JE.MyControl.RadioT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT8 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.ItNo = new JE.MyControl.TextBoxT();
            this.ItNoUdf = new JE.MyControl.TextBoxT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.ItName = new JE.MyControl.TextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.KiNo = new JE.MyControl.TextBoxT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.ItDesp = new JE.MyControl.TextBoxT();
            this.KiNo1 = new JE.MyControl.TextBoxT();
            this.ItNo1 = new JE.MyControl.TextBoxT();
            this.ItNoUdf1 = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.radioT3);
            this.pnlBoxT1.Controls.Add(this.radioT2);
            this.pnlBoxT1.Controls.Add(this.radioT1);
            this.pnlBoxT1.Controls.Add(this.lblT7);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.lblT8);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.lblT9);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.ItNo);
            this.pnlBoxT1.Controls.Add(this.ItNoUdf);
            this.pnlBoxT1.Controls.Add(this.lblT6);
            this.pnlBoxT1.Controls.Add(this.ItName);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.KiNo);
            this.pnlBoxT1.Controls.Add(this.lblT5);
            this.pnlBoxT1.Controls.Add(this.ItDesp);
            this.pnlBoxT1.Controls.Add(this.KiNo1);
            this.pnlBoxT1.Controls.Add(this.ItNo1);
            this.pnlBoxT1.Controls.Add(this.ItNoUdf1);
            this.pnlBoxT1.Location = new System.Drawing.Point(169, 68);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(673, 419);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // radioT3
            // 
            this.radioT3.AutoSize = true;
            this.radioT3.BackColor = System.Drawing.Color.Transparent;
            this.radioT3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT3.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT3.Location = new System.Drawing.Point(373, 318);
            this.radioT3.Name = "radioT3";
            this.radioT3.Size = new System.Drawing.Size(218, 20);
            this.radioT3.TabIndex = 2;
            this.radioT3.Tag = "不限制(預設類型CODE-128)";
            this.radioT3.Text = "不限制(預設類型CODE-128)";
            this.radioT3.UseVisualStyleBackColor = true;
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.LightBlue;
            this.radioT2.Checked = true;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Enabled = false;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(267, 318);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(90, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Tag = "CODE-128";
            this.radioT2.Text = "CODE-128";
            this.radioT2.UseVisualStyleBackColor = false;
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.Transparent;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Enabled = false;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(177, 318);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(74, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.Tag = "EAN-13";
            this.radioT1.Text = "EAN-13";
            this.radioT1.UseVisualStyleBackColor = true;
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(350, 85);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(24, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "～";
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(97, 85);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "產品編號";
            // 
            // lblT8
            // 
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(350, 133);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(24, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "～";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(97, 229);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "產品類別";
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(350, 229);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(24, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "～";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(97, 133);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "自訂編號";
            // 
            // ItNo
            // 
            this.ItNo.AllowGrayBackColor = false;
            this.ItNo.AllowResize = true;
            this.ItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo.Location = new System.Drawing.Point(177, 80);
            this.ItNo.MaxLength = 20;
            this.ItNo.Name = "ItNo";
            this.ItNo.oLen = 0;
            this.ItNo.Size = new System.Drawing.Size(167, 27);
            this.ItNo.TabIndex = 1;
            this.ItNo.DoubleClick += new System.EventHandler(this.ItNo_DoubleClick);
            // 
            // ItNoUdf
            // 
            this.ItNoUdf.AllowGrayBackColor = false;
            this.ItNoUdf.AllowResize = true;
            this.ItNoUdf.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNoUdf.Location = new System.Drawing.Point(177, 128);
            this.ItNoUdf.MaxLength = 20;
            this.ItNoUdf.Name = "ItNoUdf";
            this.ItNoUdf.oLen = 0;
            this.ItNoUdf.Size = new System.Drawing.Size(167, 27);
            this.ItNoUdf.TabIndex = 3;
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(97, 320);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(72, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "條碼類型";
            // 
            // ItName
            // 
            this.ItName.AllowGrayBackColor = false;
            this.ItName.AllowResize = true;
            this.ItName.Font = new System.Drawing.Font("細明體", 12F);
            this.ItName.Location = new System.Drawing.Point(177, 176);
            this.ItName.MaxLength = 30;
            this.ItName.Name = "ItName";
            this.ItName.oLen = 0;
            this.ItName.Size = new System.Drawing.Size(247, 27);
            this.ItName.TabIndex = 5;
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(97, 181);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "品名規格";
            // 
            // KiNo
            // 
            this.KiNo.AllowGrayBackColor = false;
            this.KiNo.AllowResize = true;
            this.KiNo.Font = new System.Drawing.Font("細明體", 12F);
            this.KiNo.Location = new System.Drawing.Point(177, 224);
            this.KiNo.MaxLength = 4;
            this.KiNo.Name = "KiNo";
            this.KiNo.oLen = 0;
            this.KiNo.Size = new System.Drawing.Size(39, 27);
            this.KiNo.TabIndex = 6;
            this.KiNo.DoubleClick += new System.EventHandler(this.KiNo_DoubleClick);
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(81, 277);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(88, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "規格說明一";
            // 
            // ItDesp
            // 
            this.ItDesp.AllowGrayBackColor = false;
            this.ItDesp.AllowResize = true;
            this.ItDesp.Font = new System.Drawing.Font("細明體", 12F);
            this.ItDesp.Location = new System.Drawing.Point(177, 272);
            this.ItDesp.MaxLength = 50;
            this.ItDesp.Name = "ItDesp";
            this.ItDesp.oLen = 0;
            this.ItDesp.Size = new System.Drawing.Size(407, 27);
            this.ItDesp.TabIndex = 8;
            // 
            // KiNo1
            // 
            this.KiNo1.AllowGrayBackColor = false;
            this.KiNo1.AllowResize = true;
            this.KiNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.KiNo1.Location = new System.Drawing.Point(380, 224);
            this.KiNo1.MaxLength = 4;
            this.KiNo1.Name = "KiNo1";
            this.KiNo1.oLen = 0;
            this.KiNo1.Size = new System.Drawing.Size(39, 27);
            this.KiNo1.TabIndex = 7;
            this.KiNo1.DoubleClick += new System.EventHandler(this.KiNo_DoubleClick);
            // 
            // ItNo1
            // 
            this.ItNo1.AllowGrayBackColor = false;
            this.ItNo1.AllowResize = true;
            this.ItNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo1.Location = new System.Drawing.Point(380, 80);
            this.ItNo1.MaxLength = 20;
            this.ItNo1.Name = "ItNo1";
            this.ItNo1.oLen = 0;
            this.ItNo1.Size = new System.Drawing.Size(167, 27);
            this.ItNo1.TabIndex = 2;
            this.ItNo1.DoubleClick += new System.EventHandler(this.ItNo_DoubleClick);
            // 
            // ItNoUdf1
            // 
            this.ItNoUdf1.AllowGrayBackColor = false;
            this.ItNoUdf1.AllowResize = true;
            this.ItNoUdf1.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNoUdf1.Location = new System.Drawing.Point(380, 128);
            this.ItNoUdf1.MaxLength = 20;
            this.ItNoUdf1.Name = "ItNoUdf1";
            this.ItNoUdf1.oLen = 0;
            this.ItNoUdf1.Size = new System.Drawing.Size(167, 27);
            this.ItNoUdf1.TabIndex = 4;
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(431, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 79);
            this.panelT1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.btnBrow.Location = new System.Drawing.Point(0, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 0;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
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
            // FrmBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmBarCode";
            this.Tag = "產品標簽條碼";
            this.Text = "產品標籤條碼列印";
            this.Load += new System.EventHandler(this.FrmBarCode_Load);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.TextBoxT ItNo;
        private JE.MyControl.TextBoxT ItNoUdf;
        private JE.MyControl.TextBoxT ItName;
        private JE.MyControl.TextBoxT KiNo;
        private JE.MyControl.TextBoxT ItDesp;
        private JE.MyControl.TextBoxT ItNo1;
        private JE.MyControl.TextBoxT ItNoUdf1;
        private JE.MyControl.TextBoxT KiNo1;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.RadioT radioT3;
        private JE.MyControl.StatusStripT statusStripT1; 
    }
}