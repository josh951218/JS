namespace S_61.S4
{
    partial class FrmPOSReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPOSReport));
            this.radioT1 = new JE.MyControl.RadioT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.panelT1 = new JE.MyControl.PanelT();
            this.btnNext = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.labelMenuT1 = new JE.MyControl.LabelMenuT();
            this.labelMenuT2 = new JE.MyControl.LabelMenuT();
            this.labelMenuT3 = new JE.MyControl.LabelMenuT();
            this.labelMenuT4 = new JE.MyControl.LabelMenuT();
            this.labelMenuT5 = new JE.MyControl.LabelMenuT();
            this.labelMenuT6 = new JE.MyControl.LabelMenuT();
            this.labelMenuT7 = new JE.MyControl.LabelMenuT();
            this.labelMenuT8 = new JE.MyControl.LabelMenuT();
            this.labelMenuT9 = new JE.MyControl.LabelMenuT();
            this.labelMenuT10 = new JE.MyControl.LabelMenuT();
            this.tabControlT1 = new JE.MyControl.TabControlT();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelT1.SuspendLayout();
            this.panelBtnT1.SuspendLayout();
            this.tabControlT1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(123, 54);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(122, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.Tag = "每日銷售報表";
            this.radioT1.Text = "每日銷售報表";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.Transparent;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(123, 89);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(122, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Tag = "產品折數報表";
            this.radioT2.Text = "產品折數報表";
            this.radioT2.UseVisualStyleBackColor = false;
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.radioT1);
            this.panelT1.Controls.Add(this.btnNext);
            this.panelT1.Controls.Add(this.radioT2);
            this.panelT1.Location = new System.Drawing.Point(233, 171);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(538, 162);
            this.panelT1.TabIndex = 2;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("細明體", 12F);
            this.btnNext.Location = new System.Drawing.Point(293, 66);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(122, 31);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "下一步";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
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
            this.panelBtnT1.TabIndex = 4;
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
            // labelMenuT1
            // 
            this.labelMenuT1.AutoSize = true;
            this.labelMenuT1.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT1.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT1.Location = new System.Drawing.Point(211, 81);
            this.labelMenuT1.Name = "labelMenuT1";
            this.labelMenuT1.Size = new System.Drawing.Size(206, 22);
            this.labelMenuT1.TabIndex = 0;
            this.labelMenuT1.Text = "■ 錢櫃開啟記錄表";
            this.labelMenuT1.Click += new System.EventHandler(this.labelMenuT1_Click);
            // 
            // labelMenuT2
            // 
            this.labelMenuT2.AutoSize = true;
            this.labelMenuT2.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT2.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT2.Location = new System.Drawing.Point(211, 145);
            this.labelMenuT2.Name = "labelMenuT2";
            this.labelMenuT2.Size = new System.Drawing.Size(206, 22);
            this.labelMenuT2.TabIndex = 0;
            this.labelMenuT2.Text = "■ 收銀人員業績表";
            this.labelMenuT2.Click += new System.EventHandler(this.labelMenuT2_Click);
            // 
            // labelMenuT3
            // 
            this.labelMenuT3.AutoSize = true;
            this.labelMenuT3.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT3.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT3.Location = new System.Drawing.Point(211, 209);
            this.labelMenuT3.Name = "labelMenuT3";
            this.labelMenuT3.Size = new System.Drawing.Size(206, 22);
            this.labelMenuT3.TabIndex = 0;
            this.labelMenuT3.Text = "■ 機台業績統計表";
            this.labelMenuT3.Click += new System.EventHandler(this.labelMenuT3_Click);
            // 
            // labelMenuT4
            // 
            this.labelMenuT4.AutoSize = true;
            this.labelMenuT4.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT4.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT4.Location = new System.Drawing.Point(211, 273);
            this.labelMenuT4.Name = "labelMenuT4";
            this.labelMenuT4.Size = new System.Drawing.Size(206, 22);
            this.labelMenuT4.TabIndex = 0;
            this.labelMenuT4.Text = "■ 寄售廠商對帳表";
            this.labelMenuT4.Click += new System.EventHandler(this.labelMenuT4_Click);
            // 
            // labelMenuT5
            // 
            this.labelMenuT5.AutoSize = true;
            this.labelMenuT5.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT5.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT5.Location = new System.Drawing.Point(541, 81);
            this.labelMenuT5.Name = "labelMenuT5";
            this.labelMenuT5.Size = new System.Drawing.Size(206, 22);
            this.labelMenuT5.TabIndex = 0;
            this.labelMenuT5.Text = "■ 會員銷售排行表";
            this.labelMenuT5.Click += new System.EventHandler(this.labelMenuT5_Click);
            // 
            // labelMenuT6
            // 
            this.labelMenuT6.AutoSize = true;
            this.labelMenuT6.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT6.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT6.Location = new System.Drawing.Point(541, 145);
            this.labelMenuT6.Name = "labelMenuT6";
            this.labelMenuT6.Size = new System.Drawing.Size(206, 22);
            this.labelMenuT6.TabIndex = 0;
            this.labelMenuT6.Text = "■ 產品銷售排行表";
            this.labelMenuT6.Click += new System.EventHandler(this.labelMenuT6_Click);
            // 
            // labelMenuT7
            // 
            this.labelMenuT7.AutoSize = true;
            this.labelMenuT7.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT7.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT7.Location = new System.Drawing.Point(541, 209);
            this.labelMenuT7.Name = "labelMenuT7";
            this.labelMenuT7.Size = new System.Drawing.Size(206, 22);
            this.labelMenuT7.TabIndex = 0;
            this.labelMenuT7.Text = "■ 類別銷售排行表";
            this.labelMenuT7.Click += new System.EventHandler(this.labelMenuT7_Click);
            // 
            // labelMenuT8
            // 
            this.labelMenuT8.AutoSize = true;
            this.labelMenuT8.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT8.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT8.Location = new System.Drawing.Point(541, 273);
            this.labelMenuT8.Name = "labelMenuT8";
            this.labelMenuT8.Size = new System.Drawing.Size(206, 22);
            this.labelMenuT8.TabIndex = 0;
            this.labelMenuT8.Text = "■ 時段銷售排行表";
            this.labelMenuT8.Click += new System.EventHandler(this.labelMenuT8_Click);
            // 
            // labelMenuT9
            // 
            this.labelMenuT9.AutoSize = true;
            this.labelMenuT9.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT9.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT9.Location = new System.Drawing.Point(541, 337);
            this.labelMenuT9.Name = "labelMenuT9";
            this.labelMenuT9.Size = new System.Drawing.Size(206, 22);
            this.labelMenuT9.TabIndex = 0;
            this.labelMenuT9.Text = "■ 客戶單價分析表";
            this.labelMenuT9.Click += new System.EventHandler(this.labelMenuT9_Click);
            // 
            // labelMenuT10
            // 
            this.labelMenuT10.AutoSize = true;
            this.labelMenuT10.BackColor = System.Drawing.Color.Transparent;
            this.labelMenuT10.Font = new System.Drawing.Font("細明體", 16F, System.Drawing.FontStyle.Bold);
            this.labelMenuT10.Location = new System.Drawing.Point(541, 401);
            this.labelMenuT10.Name = "labelMenuT10";
            this.labelMenuT10.Size = new System.Drawing.Size(252, 22);
            this.labelMenuT10.TabIndex = 0;
            this.labelMenuT10.Text = "■ 二聯收銀發票明細表";
            this.labelMenuT10.Click += new System.EventHandler(this.labelMenuT10_Click);
            // 
            // tabControlT1
            // 
            this.tabControlT1.Controls.Add(this.tabPage1);
            this.tabControlT1.Controls.Add(this.tabPage2);
            this.tabControlT1.Font = new System.Drawing.Font("細明體", 12F);
            this.tabControlT1.ItemSize = new System.Drawing.Size(150, 25);
            this.tabControlT1.Location = new System.Drawing.Point(0, 0);
            this.tabControlT1.Name = "tabControlT1";
            this.tabControlT1.SelectedIndex = 0;
            this.tabControlT1.Size = new System.Drawing.Size(1012, 538);
            this.tabControlT1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlT1.TabIndex = 5;
            this.tabControlT1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.labelMenuT1);
            this.tabPage1.Controls.Add(this.labelMenuT10);
            this.tabPage1.Controls.Add(this.labelMenuT2);
            this.tabPage1.Controls.Add(this.labelMenuT9);
            this.tabPage1.Controls.Add(this.labelMenuT3);
            this.tabPage1.Controls.Add(this.labelMenuT8);
            this.tabPage1.Controls.Add(this.labelMenuT4);
            this.tabPage1.Controls.Add(this.labelMenuT7);
            this.tabPage1.Controls.Add(this.labelMenuT5);
            this.tabPage1.Controls.Add(this.labelMenuT6);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1004, 505);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "百貨版";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.panelT1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1004, 505);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "POS舊版";
            // 
            // FrmPOSReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.tabControlT1);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "FrmPOSReport";
            this.Tag = "POS前台銷售報表";
            this.Text = "前台銷貨報表";
            this.Load += new System.EventHandler(this.FrmPOSReport_Load);
            this.panelT1.ResumeLayout(false);
            this.panelT1.PerformLayout();
            this.panelBtnT1.ResumeLayout(false);
            this.tabControlT1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.ButtonSmallT btnNext;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.LabelMenuT labelMenuT1;
        private JE.MyControl.LabelMenuT labelMenuT2;
        private JE.MyControl.LabelMenuT labelMenuT3;
        private JE.MyControl.LabelMenuT labelMenuT4;
        private JE.MyControl.LabelMenuT labelMenuT5;
        private JE.MyControl.LabelMenuT labelMenuT6;
        private JE.MyControl.LabelMenuT labelMenuT7;
        private JE.MyControl.LabelMenuT labelMenuT8;
        private JE.MyControl.LabelMenuT labelMenuT9;
        private JE.MyControl.LabelMenuT labelMenuT10;
        private JE.MyControl.TabControlT tabControlT1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}