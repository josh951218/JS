﻿namespace S_61.subMenuFm_3
{
    partial class FrmFord_Payabld
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFord_Payabld));
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.PaDate = new JE.MyControl.TextBoxT();
            this.EmNo1 = new JE.MyControl.TextBoxT();
            this.EmNo = new JE.MyControl.TextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.PaDate1 = new JE.MyControl.TextBoxT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(215, 92);
            this.lblT2.Margin = new System.Windows.Forms.Padding(15);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起訖收款日期";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(215, 151);
            this.lblT3.Margin = new System.Windows.Forms.Padding(15);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "起訖採購人員";
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(104, 122);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(88, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "請輸入列印";
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.PaDate);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.EmNo1);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.EmNo);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.PaDate1);
            this.pnlBoxT1.Controls.Add(this.lblT5);
            this.pnlBoxT1.Location = new System.Drawing.Point(163, 147);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(684, 261);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // PaDate
            // 
            this.PaDate.AllowGrayBackColor = false;
            this.PaDate.AllowResize = true;
            this.PaDate.Font = new System.Drawing.Font("細明體", 12F);
            this.PaDate.Location = new System.Drawing.Point(337, 87);
            this.PaDate.MaxLength = 10;
            this.PaDate.Name = "PaDate";
            this.PaDate.oLen = 0;
            this.PaDate.Size = new System.Drawing.Size(87, 27);
            this.PaDate.TabIndex = 1;
            this.PaDate.Validating += new System.ComponentModel.CancelEventHandler(this.PaDate_Validating);
            // 
            // EmNo1
            // 
            this.EmNo1.AllowGrayBackColor = false;
            this.EmNo1.AllowResize = true;
            this.EmNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.EmNo1.Location = new System.Drawing.Point(460, 146);
            this.EmNo1.MaxLength = 4;
            this.EmNo1.Name = "EmNo1";
            this.EmNo1.oLen = 0;
            this.EmNo1.Size = new System.Drawing.Size(39, 27);
            this.EmNo1.TabIndex = 4;
            this.EmNo1.DoubleClick += new System.EventHandler(this.EmNo1_DoubleClick);
            // 
            // EmNo
            // 
            this.EmNo.AllowGrayBackColor = false;
            this.EmNo.AllowResize = true;
            this.EmNo.Font = new System.Drawing.Font("細明體", 12F);
            this.EmNo.Location = new System.Drawing.Point(337, 146);
            this.EmNo.MaxLength = 4;
            this.EmNo.Name = "EmNo";
            this.EmNo.oLen = 0;
            this.EmNo.Size = new System.Drawing.Size(39, 27);
            this.EmNo.TabIndex = 3;
            this.EmNo.DoubleClick += new System.EventHandler(this.EmNo_DoubleClick);
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(430, 92);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(24, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "～";
            // 
            // PaDate1
            // 
            this.PaDate1.AllowGrayBackColor = false;
            this.PaDate1.AllowResize = true;
            this.PaDate1.Font = new System.Drawing.Font("細明體", 12F);
            this.PaDate1.Location = new System.Drawing.Point(460, 87);
            this.PaDate1.MaxLength = 10;
            this.PaDate1.Name = "PaDate1";
            this.PaDate1.oLen = 0;
            this.PaDate1.Size = new System.Drawing.Size(87, 27);
            this.PaDate1.TabIndex = 2;
            this.PaDate1.Validating += new System.ComponentModel.CancelEventHandler(this.PaDate1_Validating);
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(430, 151);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(24, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "～";
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
            // FrmFord_Payabld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmFord_Payabld";
            this.Tag = "採購員-已付帳款";
            this.Text = "採購員-已付帳款";
            this.Load += new System.EventHandler(this.FrmFord_Payabld_Load);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.TextBoxT PaDate;
        private JE.MyControl.TextBoxT PaDate1;
        private JE.MyControl.TextBoxT EmNo;
        private JE.MyControl.TextBoxT EmNo1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}