﻿namespace S_61.subMenuFm_1
{
    partial class FrmFactNotIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFactNotIn));
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.EsDate = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.EsDate1 = new JE.MyControl.TextBoxT();
            this.FaNo1 = new JE.MyControl.TextBoxT();
            this.FaNo = new JE.MyControl.TextBoxT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.lblT7 = new JE.MyControl.LabelT();
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
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.EsDate);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.EsDate1);
            this.pnlBoxT1.Controls.Add(this.FaNo1);
            this.pnlBoxT1.Controls.Add(this.FaNo);
            this.pnlBoxT1.Location = new System.Drawing.Point(195, 130);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(620, 201);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(102, 65);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(104, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "起迄交貨日期";
            // 
            // EsDate
            // 
            this.EsDate.AllowGrayBackColor = false;
            this.EsDate.AllowResize = true;
            this.EsDate.Font = new System.Drawing.Font("細明體", 12F);
            this.EsDate.Location = new System.Drawing.Point(212, 60);
            this.EsDate.MaxLength = 10;
            this.EsDate.Name = "EsDate";
            this.EsDate.oLen = 0;
            this.EsDate.Size = new System.Drawing.Size(87, 27);
            this.EsDate.TabIndex = 2;
            this.EsDate.Tag = "交貨日期";
            this.EsDate.Validating += new System.ComponentModel.CancelEventHandler(this.EsDate1_Validating);
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(102, 119);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起迄廠商編號";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(353, 119);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(24, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "～";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(353, 65);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(24, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "～";
            // 
            // EsDate1
            // 
            this.EsDate1.AllowGrayBackColor = false;
            this.EsDate1.AllowResize = true;
            this.EsDate1.Font = new System.Drawing.Font("細明體", 12F);
            this.EsDate1.Location = new System.Drawing.Point(383, 60);
            this.EsDate1.MaxLength = 10;
            this.EsDate1.Name = "EsDate1";
            this.EsDate1.oLen = 0;
            this.EsDate1.Size = new System.Drawing.Size(87, 27);
            this.EsDate1.TabIndex = 3;
            this.EsDate1.Tag = "交貨日期";
            this.EsDate1.Validating += new System.ComponentModel.CancelEventHandler(this.EsDate1_Validating);
            // 
            // FaNo1
            // 
            this.FaNo1.AllowGrayBackColor = false;
            this.FaNo1.AllowResize = true;
            this.FaNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.FaNo1.Location = new System.Drawing.Point(383, 114);
            this.FaNo1.MaxLength = 16;
            this.FaNo1.Name = "FaNo1";
            this.FaNo1.oLen = 0;
            this.FaNo1.Size = new System.Drawing.Size(135, 27);
            this.FaNo1.TabIndex = 5;
            this.FaNo1.Tag = "客戶編號";
            this.FaNo1.DoubleClick += new System.EventHandler(this.FaNo_DoubleClick);
            this.FaNo1.Validating += new System.ComponentModel.CancelEventHandler(this.FaNo_Validating);
            // 
            // FaNo
            // 
            this.FaNo.AllowGrayBackColor = false;
            this.FaNo.AllowResize = true;
            this.FaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.FaNo.Location = new System.Drawing.Point(212, 114);
            this.FaNo.MaxLength = 16;
            this.FaNo.Name = "FaNo";
            this.FaNo.oLen = 0;
            this.FaNo.Size = new System.Drawing.Size(135, 27);
            this.FaNo.TabIndex = 4;
            this.FaNo.Tag = "客戶編號";
            this.FaNo.DoubleClick += new System.EventHandler(this.FaNo_DoubleClick);
            this.FaNo.Validating += new System.ComponentModel.CancelEventHandler(this.FaNo_Validating);
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(365, 465);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(280, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "＊起始編號空白表示從第一筆開始列印";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(365, 489);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(248, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "＊終止編號空白表示印至最後一筆";
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(365, 513);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(200, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "＊二者皆空白表示全部列印";
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
            // statusStripT1
            // 
            this.statusStripT1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStripT1.Location = new System.Drawing.Point(0, 625);
            this.statusStripT1.Name = "statusStripT1";
            this.statusStripT1.Size = new System.Drawing.Size(1010, 22);
            this.statusStripT1.TabIndex = 0;
            this.statusStripT1.Text = "statusStripT1";
            // 
            // FrmFactNotIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmFactNotIn";
            this.Tag = "廠商別未到貨明細";
            this.Text = "廠商別未到貨明細";
            this.Load += new System.EventHandler(this.FrmFactNotIn_Load);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT EsDate;
        private JE.MyControl.TextBoxT EsDate1;
        private JE.MyControl.TextBoxT FaNo;
        private JE.MyControl.TextBoxT FaNo1;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.StatusStripT statusStripT1;

    }
}