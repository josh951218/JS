namespace S_61.subMenuFm_2
{
    partial class FrmDraw_Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDraw_Info));
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.DrDate1 = new JE.MyControl.TextBoxT();
            this.StNoO1 = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.EmNo1 = new JE.MyControl.TextBoxT();
            this.lblT11 = new JE.MyControl.LabelT();
            this.lblT12 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT13 = new JE.MyControl.LabelT();
            this.ItNo = new JE.MyControl.TextBoxT();
            this.lblT14 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT15 = new JE.MyControl.LabelT();
            this.DrDate = new JE.MyControl.TextBoxT();
            this.StNoI1 = new JE.MyControl.TextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.Memo = new JE.MyControl.TextBoxT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.ItNo1 = new JE.MyControl.TextBoxT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.EmNo = new JE.MyControl.TextBoxT();
            this.lblT10 = new JE.MyControl.LabelT();
            this.StNoI = new JE.MyControl.TextBoxT();
            this.StNoO = new JE.MyControl.TextBoxT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.lblT8 = new JE.MyControl.LabelT();
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
            this.pnlBoxT1.Controls.Add(this.DrDate1);
            this.pnlBoxT1.Controls.Add(this.StNoO1);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.EmNo1);
            this.pnlBoxT1.Controls.Add(this.lblT11);
            this.pnlBoxT1.Controls.Add(this.lblT12);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.lblT13);
            this.pnlBoxT1.Controls.Add(this.ItNo);
            this.pnlBoxT1.Controls.Add(this.lblT14);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.lblT15);
            this.pnlBoxT1.Controls.Add(this.DrDate);
            this.pnlBoxT1.Controls.Add(this.StNoI1);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.Memo);
            this.pnlBoxT1.Controls.Add(this.lblT5);
            this.pnlBoxT1.Controls.Add(this.ItNo1);
            this.pnlBoxT1.Controls.Add(this.lblT9);
            this.pnlBoxT1.Controls.Add(this.EmNo);
            this.pnlBoxT1.Controls.Add(this.lblT10);
            this.pnlBoxT1.Controls.Add(this.StNoI);
            this.pnlBoxT1.Controls.Add(this.StNoO);
            this.pnlBoxT1.Location = new System.Drawing.Point(163, 63);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(684, 336);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // DrDate1
            // 
            this.DrDate1.AllowGrayBackColor = false;
            this.DrDate1.AllowResize = true;
            this.DrDate1.Font = new System.Drawing.Font("細明體", 12F);
            this.DrDate1.Location = new System.Drawing.Point(423, 23);
            this.DrDate1.MaxLength = 8;
            this.DrDate1.Name = "DrDate1";
            this.DrDate1.oLen = 0;
            this.DrDate1.Size = new System.Drawing.Size(71, 27);
            this.DrDate1.TabIndex = 2;
            this.DrDate1.Tag = "領料日期";
            this.DrDate1.Validating += new System.ComponentModel.CancelEventHandler(this.DrNo_Validating);
            // 
            // StNoO1
            // 
            this.StNoO1.AllowGrayBackColor = false;
            this.StNoO1.AllowResize = true;
            this.StNoO1.Font = new System.Drawing.Font("細明體", 12F);
            this.StNoO1.Location = new System.Drawing.Point(423, 66);
            this.StNoO1.MaxLength = 10;
            this.StNoO1.Name = "StNoO1";
            this.StNoO1.oLen = 0;
            this.StNoO1.Size = new System.Drawing.Size(87, 27);
            this.StNoO1.TabIndex = 4;
            this.StNoO1.Tag = "撥出倉庫";
            this.StNoO1.DoubleClick += new System.EventHandler(this.StNoO_DoubleClick);
            this.StNoO1.Validating += new System.ComponentModel.CancelEventHandler(this.StNoO_Validating);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(175, 28);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(104, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "起迄領料日期";
            // 
            // EmNo1
            // 
            this.EmNo1.AllowGrayBackColor = false;
            this.EmNo1.AllowResize = true;
            this.EmNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.EmNo1.Location = new System.Drawing.Point(423, 152);
            this.EmNo1.MaxLength = 4;
            this.EmNo1.Name = "EmNo1";
            this.EmNo1.oLen = 0;
            this.EmNo1.Size = new System.Drawing.Size(39, 27);
            this.EmNo1.TabIndex = 12;
            this.EmNo1.Tag = "領料人員";
            this.EmNo1.DoubleClick += new System.EventHandler(this.EmNo_DoubleClick);
            this.EmNo1.Validating += new System.ComponentModel.CancelEventHandler(this.EmNo_Validating);
            // 
            // lblT11
            // 
            this.lblT11.AutoSize = true;
            this.lblT11.BackColor = System.Drawing.Color.Transparent;
            this.lblT11.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT11.Location = new System.Drawing.Point(30, 160);
            this.lblT11.Name = "lblT11";
            this.lblT11.Size = new System.Drawing.Size(88, 16);
            this.lblT11.TabIndex = 0;
            this.lblT11.Text = "請輸入列印";
            // 
            // lblT12
            // 
            this.lblT12.AutoSize = true;
            this.lblT12.BackColor = System.Drawing.Color.Transparent;
            this.lblT12.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT12.Location = new System.Drawing.Point(393, 28);
            this.lblT12.Name = "lblT12";
            this.lblT12.Size = new System.Drawing.Size(24, 16);
            this.lblT12.TabIndex = 0;
            this.lblT12.Text = "～";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(175, 71);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起迄撥出倉庫";
            // 
            // lblT13
            // 
            this.lblT13.AutoSize = true;
            this.lblT13.BackColor = System.Drawing.Color.Transparent;
            this.lblT13.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT13.Location = new System.Drawing.Point(393, 71);
            this.lblT13.Name = "lblT13";
            this.lblT13.Size = new System.Drawing.Size(24, 16);
            this.lblT13.TabIndex = 0;
            this.lblT13.Text = "～";
            // 
            // ItNo
            // 
            this.ItNo.AllowGrayBackColor = false;
            this.ItNo.AllowResize = true;
            this.ItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo.Location = new System.Drawing.Point(285, 195);
            this.ItNo.MaxLength = 20;
            this.ItNo.Name = "ItNo";
            this.ItNo.oLen = 0;
            this.ItNo.Size = new System.Drawing.Size(167, 27);
            this.ItNo.TabIndex = 13;
            this.ItNo.Tag = "產品編號";
            this.ItNo.DoubleClick += new System.EventHandler(this.ItNo_DoubleClick);
            this.ItNo.Validating += new System.ComponentModel.CancelEventHandler(this.ItNo_Validating);
            // 
            // lblT14
            // 
            this.lblT14.AutoSize = true;
            this.lblT14.BackColor = System.Drawing.Color.Transparent;
            this.lblT14.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT14.Location = new System.Drawing.Point(393, 114);
            this.lblT14.Name = "lblT14";
            this.lblT14.Size = new System.Drawing.Size(24, 16);
            this.lblT14.TabIndex = 0;
            this.lblT14.Text = "～";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(175, 114);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "起迄領入倉庫";
            // 
            // lblT15
            // 
            this.lblT15.AutoSize = true;
            this.lblT15.BackColor = System.Drawing.Color.Transparent;
            this.lblT15.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT15.Location = new System.Drawing.Point(393, 157);
            this.lblT15.Name = "lblT15";
            this.lblT15.Size = new System.Drawing.Size(24, 16);
            this.lblT15.TabIndex = 0;
            this.lblT15.Text = "～";
            // 
            // DrDate
            // 
            this.DrDate.AllowGrayBackColor = false;
            this.DrDate.AllowResize = true;
            this.DrDate.Font = new System.Drawing.Font("細明體", 12F);
            this.DrDate.Location = new System.Drawing.Point(285, 23);
            this.DrDate.MaxLength = 8;
            this.DrDate.Name = "DrDate";
            this.DrDate.oLen = 0;
            this.DrDate.Size = new System.Drawing.Size(71, 27);
            this.DrDate.TabIndex = 1;
            this.DrDate.Tag = "領料日期";
            this.DrDate.Validating += new System.ComponentModel.CancelEventHandler(this.DrNo_Validating);
            // 
            // StNoI1
            // 
            this.StNoI1.AllowGrayBackColor = false;
            this.StNoI1.AllowResize = true;
            this.StNoI1.Font = new System.Drawing.Font("細明體", 12F);
            this.StNoI1.Location = new System.Drawing.Point(423, 109);
            this.StNoI1.MaxLength = 10;
            this.StNoI1.Name = "StNoI1";
            this.StNoI1.oLen = 0;
            this.StNoI1.Size = new System.Drawing.Size(87, 27);
            this.StNoI1.TabIndex = 6;
            this.StNoI1.Tag = "領入倉庫";
            this.StNoI1.DoubleClick += new System.EventHandler(this.StNoO_DoubleClick);
            this.StNoI1.Validating += new System.ComponentModel.CancelEventHandler(this.StNoO_Validating);
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(175, 157);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(104, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "起迄領料人員";
            // 
            // Memo
            // 
            this.Memo.AllowGrayBackColor = false;
            this.Memo.AllowResize = true;
            this.Memo.Font = new System.Drawing.Font("細明體", 12F);
            this.Memo.Location = new System.Drawing.Point(285, 281);
            this.Memo.MaxLength = 20;
            this.Memo.Name = "Memo";
            this.Memo.oLen = 0;
            this.Memo.Size = new System.Drawing.Size(167, 27);
            this.Memo.TabIndex = 15;
            this.Memo.DoubleClick += new System.EventHandler(this.Memo_DoubleClick);
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(175, 200);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(104, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "起迄產品編號";
            // 
            // ItNo1
            // 
            this.ItNo1.AllowGrayBackColor = false;
            this.ItNo1.AllowResize = true;
            this.ItNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo1.Location = new System.Drawing.Point(285, 238);
            this.ItNo1.MaxLength = 20;
            this.ItNo1.Name = "ItNo1";
            this.ItNo1.oLen = 0;
            this.ItNo1.Size = new System.Drawing.Size(167, 27);
            this.ItNo1.TabIndex = 14;
            this.ItNo1.Tag = "產品編號";
            this.ItNo1.DoubleClick += new System.EventHandler(this.ItNo_DoubleClick);
            this.ItNo1.Validating += new System.ComponentModel.CancelEventHandler(this.ItNo_Validating);
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(175, 243);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(104, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "終止產品編號";
            // 
            // EmNo
            // 
            this.EmNo.AllowGrayBackColor = false;
            this.EmNo.AllowResize = true;
            this.EmNo.Font = new System.Drawing.Font("細明體", 12F);
            this.EmNo.Location = new System.Drawing.Point(285, 152);
            this.EmNo.MaxLength = 4;
            this.EmNo.Name = "EmNo";
            this.EmNo.oLen = 0;
            this.EmNo.Size = new System.Drawing.Size(39, 27);
            this.EmNo.TabIndex = 11;
            this.EmNo.Tag = "領料人員";
            this.EmNo.DoubleClick += new System.EventHandler(this.EmNo_DoubleClick);
            this.EmNo.Validating += new System.ComponentModel.CancelEventHandler(this.EmNo_Validating);
            // 
            // lblT10
            // 
            this.lblT10.AutoSize = true;
            this.lblT10.BackColor = System.Drawing.Color.Transparent;
            this.lblT10.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT10.Location = new System.Drawing.Point(175, 286);
            this.lblT10.Name = "lblT10";
            this.lblT10.Size = new System.Drawing.Size(88, 16);
            this.lblT10.TabIndex = 0;
            this.lblT10.Text = "說      明";
            // 
            // StNoI
            // 
            this.StNoI.AllowGrayBackColor = false;
            this.StNoI.AllowResize = true;
            this.StNoI.Font = new System.Drawing.Font("細明體", 12F);
            this.StNoI.Location = new System.Drawing.Point(285, 109);
            this.StNoI.MaxLength = 10;
            this.StNoI.Name = "StNoI";
            this.StNoI.oLen = 0;
            this.StNoI.Size = new System.Drawing.Size(87, 27);
            this.StNoI.TabIndex = 5;
            this.StNoI.Tag = "領入倉庫";
            this.StNoI.DoubleClick += new System.EventHandler(this.StNoO_DoubleClick);
            this.StNoI.Validating += new System.ComponentModel.CancelEventHandler(this.StNoO_Validating);
            // 
            // StNoO
            // 
            this.StNoO.AllowGrayBackColor = false;
            this.StNoO.AllowResize = true;
            this.StNoO.Font = new System.Drawing.Font("細明體", 12F);
            this.StNoO.Location = new System.Drawing.Point(285, 66);
            this.StNoO.MaxLength = 10;
            this.StNoO.Name = "StNoO";
            this.StNoO.oLen = 0;
            this.StNoO.Size = new System.Drawing.Size(87, 27);
            this.StNoO.TabIndex = 3;
            this.StNoO.Tag = "撥出倉庫";
            this.StNoO.DoubleClick += new System.EventHandler(this.StNoO_DoubleClick);
            this.StNoO.Validating += new System.ComponentModel.CancelEventHandler(this.StNoO_Validating);
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(361, 437);
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
            this.lblT7.Location = new System.Drawing.Point(361, 464);
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
            this.lblT8.Location = new System.Drawing.Point(361, 491);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(288, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "※兩者皆空白表示全部列印...........";
            // 
            // panelT1
            // 
            this.panelT1.BackColor = System.Drawing.Color.Transparent;
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
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 5F);
            this.btnExit.Location = new System.Drawing.Point(69, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 2;
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
            this.btnBrow.TabIndex = 1;
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
            // FrmDraw_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.lblT8);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmDraw_Info";
            this.Tag = "領料資料瀏覽";
            this.Text = "領料資料瀏覽";
            this.Load += new System.EventHandler(this.FrmDraw_Info_Load);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.LabelT lblT10;
        private JE.MyControl.LabelT lblT11;
        private JE.MyControl.TextBoxT DrDate;
        private JE.MyControl.TextBoxT DrDate1;
        private JE.MyControl.TextBoxT StNoO;
        private JE.MyControl.TextBoxT StNoO1;
        private JE.MyControl.TextBoxT StNoI;
        private JE.MyControl.TextBoxT StNoI1;
        private JE.MyControl.TextBoxT EmNo;
        private JE.MyControl.TextBoxT EmNo1;
        private JE.MyControl.TextBoxT ItNo;
        private JE.MyControl.TextBoxT ItNo1;
        private JE.MyControl.TextBoxT Memo;
        private JE.MyControl.LabelT lblT12;
        private JE.MyControl.LabelT lblT13;
        private JE.MyControl.LabelT lblT14;
        private JE.MyControl.LabelT lblT15;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}