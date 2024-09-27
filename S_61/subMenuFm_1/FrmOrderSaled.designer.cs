namespace S_61.subMenuFm_1
{
    partial class FrmOrderSaled
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrderSaled));
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.OrNo1 = new JE.MyControl.TextBoxT();
            this.OrNo = new JE.MyControl.TextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.groupBoxT1.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radioT2);
            this.groupBoxT1.Controls.Add(this.radioT1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(212, 77);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(586, 79);
            this.groupBoxT1.TabIndex = 3;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.Transparent;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(333, 36);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(138, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Tag = "已交、未交總表";
            this.radioT2.Text = "已交、未交總表";
            this.radioT2.UseVisualStyleBackColor = true;
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(115, 36);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(138, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.Tag = "銷(退)貨明細表";
            this.radioT1.Text = "銷(退)貨明細表";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.OrNo1);
            this.pnlBoxT1.Controls.Add(this.OrNo);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Location = new System.Drawing.Point(212, 171);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(586, 215);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // OrNo1
            // 
            this.OrNo1.AllowGrayBackColor = false;
            this.OrNo1.AllowResize = true;
            this.OrNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.OrNo1.Location = new System.Drawing.Point(321, 121);
            this.OrNo1.MaxLength = 16;
            this.OrNo1.Name = "OrNo1";
            this.OrNo1.oLen = 0;
            this.OrNo1.Size = new System.Drawing.Size(135, 27);
            this.OrNo1.TabIndex = 1;
            this.OrNo1.DoubleClick += new System.EventHandler(this.OrNo_DoubleClick);
            // 
            // OrNo
            // 
            this.OrNo.AllowGrayBackColor = false;
            this.OrNo.AllowResize = true;
            this.OrNo.Font = new System.Drawing.Font("細明體", 12F);
            this.OrNo.Location = new System.Drawing.Point(321, 67);
            this.OrNo.MaxLength = 16;
            this.OrNo.Name = "OrNo";
            this.OrNo.oLen = 0;
            this.OrNo.Size = new System.Drawing.Size(135, 27);
            this.OrNo.TabIndex = 0;
            this.OrNo.DoubleClick += new System.EventHandler(this.OrNo_DoubleClick);
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(214, 126);
            this.lblT3.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "終止訂單憑證";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(214, 72);
            this.lblT2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起始訂單憑證";
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(114, 99);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(88, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "請輸入列印";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(365, 424);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(280, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "＊起始編號空白表示從第一筆開始列印";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(365, 449);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(248, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "＊終止編號空白表示印至最後一筆";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(365, 474);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(200, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "＊二者皆空白表示全部列印";
            // 
            // panelT1
            // 
            this.panelT1.AutoSize = true;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(431, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 82);
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
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Location = new System.Drawing.Point(0, 625);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FrmOrderSaled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmOrderSaled";
            this.Tag = "訂單別出貨明細表";
            this.Text = "訂單別出貨明細表";
            this.Load += new System.EventHandler(this.FrmOrderSaled_Load);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT OrNo;
        private JE.MyControl.TextBoxT OrNo1;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.StatusStripT statusStrip1;
    }
}