namespace S_61.subMenuFm_2
{
    partial class FrmIvToThis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIvToThis));
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.IvDate = new JE.MyControl.TextBoxT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.radioT3 = new JE.MyControl.RadioT();
            this.StNo = new JE.MyControl.TextBoxT();
            this.StName = new JE.MyControl.TextBoxT();
            this.IvPath = new JE.MyControl.TextBoxT();
            this.button1 = new JE.MyControl.ButtonSmallT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnInput = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelNT1 = new JE.MyControl.PanelNT();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.panelNT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.panelNT1);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.button1);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.IvPath);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.StName);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.StNo);
            this.pnlBoxT1.Controls.Add(this.IvDate);
            this.pnlBoxT1.Location = new System.Drawing.Point(99, 115);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(813, 302);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(123, 73);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "盤點日期";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(123, 120);
            this.lblT2.Margin = new System.Windows.Forms.Padding(15);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "成本來源";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(123, 165);
            this.lblT3.Margin = new System.Windows.Forms.Padding(15);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "盤點倉庫";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(123, 212);
            this.lblT4.Margin = new System.Windows.Forms.Padding(15);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "資料轉入";
            // 
            // IvDate
            // 
            this.IvDate.AllowGrayBackColor = false;
            this.IvDate.AllowResize = true;
            this.IvDate.Font = new System.Drawing.Font("細明體", 12F);
            this.IvDate.Location = new System.Drawing.Point(202, 68);
            this.IvDate.MaxLength = 10;
            this.IvDate.Name = "IvDate";
            this.IvDate.oLen = 0;
            this.IvDate.Size = new System.Drawing.Size(87, 27);
            this.IvDate.TabIndex = 1;
            this.IvDate.Validating += new System.ComponentModel.CancelEventHandler(this.IvDate_Validating);
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(9, 3);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(90, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.Tag = "平均成本";
            this.radioT1.Text = "平均成本";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.Transparent;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Enabled = false;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(121, 3);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(58, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Tag = "進價";
            this.radioT2.Text = "進價";
            this.radioT2.UseVisualStyleBackColor = true;
            // 
            // radioT3
            // 
            this.radioT3.AutoSize = true;
            this.radioT3.BackColor = System.Drawing.Color.Transparent;
            this.radioT3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT3.Enabled = false;
            this.radioT3.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT3.Location = new System.Drawing.Point(201, 3);
            this.radioT3.Name = "radioT3";
            this.radioT3.Size = new System.Drawing.Size(90, 20);
            this.radioT3.TabIndex = 2;
            this.radioT3.Tag = "標準成本";
            this.radioT3.Text = "標準成本";
            this.radioT3.UseVisualStyleBackColor = true;
            // 
            // StNo
            // 
            this.StNo.AllowGrayBackColor = false;
            this.StNo.AllowResize = true;
            this.StNo.Font = new System.Drawing.Font("細明體", 12F);
            this.StNo.Location = new System.Drawing.Point(202, 160);
            this.StNo.MaxLength = 10;
            this.StNo.Name = "StNo";
            this.StNo.oLen = 0;
            this.StNo.Size = new System.Drawing.Size(87, 27);
            this.StNo.TabIndex = 3;
            this.StNo.DoubleClick += new System.EventHandler(this.StNo_DoubleClick);
            this.StNo.Validating += new System.ComponentModel.CancelEventHandler(this.StNo_Validating);
            // 
            // StName
            // 
            this.StName.AllowGrayBackColor = true;
            this.StName.AllowResize = true;
            this.StName.BackColor = System.Drawing.Color.Silver;
            this.StName.Font = new System.Drawing.Font("細明體", 12F);
            this.StName.Location = new System.Drawing.Point(297, 160);
            this.StName.MaxLength = 10;
            this.StName.Name = "StName";
            this.StName.oLen = 0;
            this.StName.ReadOnly = true;
            this.StName.Size = new System.Drawing.Size(87, 27);
            this.StName.TabIndex = 4;
            this.StName.TabStop = false;
            // 
            // IvPath
            // 
            this.IvPath.AllowGrayBackColor = true;
            this.IvPath.AllowResize = true;
            this.IvPath.BackColor = System.Drawing.Color.Silver;
            this.IvPath.Font = new System.Drawing.Font("細明體", 12F);
            this.IvPath.Location = new System.Drawing.Point(202, 206);
            this.IvPath.MaxLength = 60;
            this.IvPath.Multiline = true;
            this.IvPath.Name = "IvPath";
            this.IvPath.oLen = 0;
            this.IvPath.ReadOnly = true;
            this.IvPath.Size = new System.Drawing.Size(487, 28);
            this.IvPath.TabIndex = 5;
            this.IvPath.TabStop = false;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("細明體", 12F);
            this.button1.Location = new System.Drawing.Point(689, 205);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 29);
            this.button1.TabIndex = 6;
            this.button1.TabStop = false;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnInput);
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
            // btnInput
            // 
            this.btnInput.BackColor = System.Drawing.SystemColors.Control;
            this.btnInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnInput.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInput.Location = new System.Drawing.Point(0, 0);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(69, 79);
            this.btnInput.TabIndex = 0;
            this.btnInput.UseDefaultSettings = false;
            this.btnInput.UseVisualStyleBackColor = false;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
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
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Controls.Add(this.radioT1);
            this.panelNT1.Controls.Add(this.radioT3);
            this.panelNT1.Controls.Add(this.radioT2);
            this.panelNT1.Location = new System.Drawing.Point(202, 115);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(303, 27);
            this.panelNT1.TabIndex = 2;
            // 
            // FrmIvToThis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmIvToThis";
            this.Text = "庫存盤點匯入作業";
            this.Load += new System.EventHandler(this.FrmIvToThis_Load);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.panelNT1.ResumeLayout(false);
            this.panelNT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnInput;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT IvDate;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.RadioT radioT3;
        private JE.MyControl.TextBoxT StNo;
        private JE.MyControl.TextBoxT StName;
        private JE.MyControl.TextBoxT IvPath;
        private JE.MyControl.ButtonSmallT button1;
        private JE.MyControl.PanelNT panelNT1;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}