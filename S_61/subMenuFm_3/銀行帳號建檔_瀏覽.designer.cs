namespace S_61.subMenuFm_3
{
    partial class 銀行帳號建檔_瀏覽
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblT1 = new JE.MyControl.LabelT();
            this.AcNo = new JE.MyControl.TextBoxT();
            this.Get = new JE.MyControl.ButtonSmallT();
            this.Exit = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.帳戶編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.帳戶名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.帳戶幣別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.公司簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.銀行帳號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.現行餘額 = new JE.MyControl.DataGridViewTextNumberT();
            this.帳戶簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.帳戶類別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.隸屬公司 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.公司名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.字軌 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.聯絡人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.電話 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.行動電話 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.傳真 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.郵遞區號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.電子郵件 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.備註 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(150, 591);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "帳戶編號";
            // 
            // AcNo
            // 
            this.AcNo.AllowGrayBackColor = false;
            this.AcNo.AllowResize = true;
            this.AcNo.Font = new System.Drawing.Font("細明體", 12F);
            this.AcNo.Location = new System.Drawing.Point(228, 586);
            this.AcNo.MaxLength = 20;
            this.AcNo.Name = "AcNo";
            this.AcNo.oLen = 0;
            this.AcNo.Size = new System.Drawing.Size(167, 27);
            this.AcNo.TabIndex = 1;
            this.AcNo.TextChanged += new System.EventHandler(this.AcNo_TextChanged); 
            // 
            // Get
            // 
            this.Get.Font = new System.Drawing.Font("細明體", 12F);
            this.Get.Location = new System.Drawing.Point(438, 576);
            this.Get.Name = "Get";
            this.Get.Size = new System.Drawing.Size(191, 47);
            this.Get.TabIndex = 2;
            this.Get.Text = "F9:取回";
            this.Get.UseVisualStyleBackColor = true;
            this.Get.Click += new System.EventHandler(this.Get_Click);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("細明體", 12F);
            this.Exit.Location = new System.Drawing.Point(635, 576);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(191, 47);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "F11:結束";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // dataGridViewT1
            // 
            this.dataGridViewT1.AllowUserToAddRows = false;
            this.dataGridViewT1.AllowUserToDeleteRows = false;
            this.dataGridViewT1.AllowUserToOrderColumns = true;
            this.dataGridViewT1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewT1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewT1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewT1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.帳戶編號,
            this.帳戶名稱,
            this.帳戶幣別,
            this.公司簡稱,
            this.銀行帳號,
            this.現行餘額,
            this.帳戶簡稱,
            this.帳戶類別,
            this.隸屬公司,
            this.公司名稱,
            this.字軌,
            this.聯絡人,
            this.電話,
            this.行動電話,
            this.傳真,
            this.地址,
            this.郵遞區號,
            this.電子郵件,
            this.備註});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 570);
            this.dataGridViewT1.TabIndex = 4;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            // 
            // 帳戶編號
            // 
            this.帳戶編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.帳戶編號.DataPropertyName = "ACNO";
            this.帳戶編號.HeaderText = "帳戶編號";
            this.帳戶編號.MaxInputLength = 20;
            this.帳戶編號.Name = "帳戶編號";
            this.帳戶編號.ReadOnly = true;
            this.帳戶編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.帳戶編號.Width = 173;
            // 
            // 帳戶名稱
            // 
            this.帳戶名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.帳戶名稱.DataPropertyName = "acname";
            this.帳戶名稱.HeaderText = "帳戶名稱";
            this.帳戶名稱.MaxInputLength = 30;
            this.帳戶名稱.Name = "帳戶名稱";
            this.帳戶名稱.ReadOnly = true;
            this.帳戶名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.帳戶名稱.Width = 253;
            // 
            // 帳戶幣別
            // 
            this.帳戶幣別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.帳戶幣別.DataPropertyName = "xa1name";
            this.帳戶幣別.HeaderText = "帳戶幣別";
            this.帳戶幣別.MaxInputLength = 8;
            this.帳戶幣別.Name = "帳戶幣別";
            this.帳戶幣別.ReadOnly = true;
            this.帳戶幣別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.帳戶幣別.Width = 77;
            // 
            // 公司簡稱
            // 
            this.公司簡稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.公司簡稱.DataPropertyName = "coname1";
            this.公司簡稱.HeaderText = "公司簡稱";
            this.公司簡稱.MaxInputLength = 10;
            this.公司簡稱.Name = "公司簡稱";
            this.公司簡稱.ReadOnly = true;
            this.公司簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.公司簡稱.Width = 93;
            // 
            // 銀行帳號
            // 
            this.銀行帳號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.銀行帳號.DataPropertyName = "acact";
            this.銀行帳號.HeaderText = "銀行帳號";
            this.銀行帳號.MaxInputLength = 20;
            this.銀行帳號.Name = "銀行帳號";
            this.銀行帳號.ReadOnly = true;
            this.銀行帳號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.銀行帳號.Width = 173;
            // 
            // 現行餘額
            // 
            this.現行餘額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.現行餘額.DataPropertyName = "acmny1";
            this.現行餘額.HeaderText = "現行餘額";
            this.現行餘額.MaxInputLength = 12;
            this.現行餘額.Name = "現行餘額";
            this.現行餘額.ReadOnly = true;
            this.現行餘額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.現行餘額.Width = 109;
            // 
            // 帳戶簡稱
            // 
            this.帳戶簡稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.帳戶簡稱.DataPropertyName = "acname1";
            this.帳戶簡稱.HeaderText = "帳戶簡稱";
            this.帳戶簡稱.MaxInputLength = 10;
            this.帳戶簡稱.Name = "帳戶簡稱";
            this.帳戶簡稱.ReadOnly = true;
            this.帳戶簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.帳戶簡稱.Width = 93;
            // 
            // 帳戶類別
            // 
            this.帳戶類別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.帳戶類別.DataPropertyName = "帳戶類別";
            this.帳戶類別.HeaderText = "帳戶類別";
            this.帳戶類別.MaxInputLength = 10;
            this.帳戶類別.Name = "帳戶類別";
            this.帳戶類別.ReadOnly = true;
            this.帳戶類別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.帳戶類別.Width = 93;
            // 
            // 隸屬公司
            // 
            this.隸屬公司.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.隸屬公司.DataPropertyName = "cono";
            this.隸屬公司.HeaderText = "隸屬公司";
            this.隸屬公司.MaxInputLength = 2;
            this.隸屬公司.MinimumWidth = 100;
            this.隸屬公司.Name = "隸屬公司";
            this.隸屬公司.ReadOnly = true;
            this.隸屬公司.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 公司名稱
            // 
            this.公司名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.公司名稱.DataPropertyName = "coname2";
            this.公司名稱.HeaderText = "公司名稱";
            this.公司名稱.MaxInputLength = 50;
            this.公司名稱.Name = "公司名稱";
            this.公司名稱.ReadOnly = true;
            this.公司名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.公司名稱.Width = 413;
            // 
            // 字軌
            // 
            this.字軌.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.字軌.DataPropertyName = "acword";
            this.字軌.HeaderText = "字軌";
            this.字軌.MaxInputLength = 4;
            this.字軌.MinimumWidth = 80;
            this.字軌.Name = "字軌";
            this.字軌.ReadOnly = true;
            this.字軌.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.字軌.Width = 80;
            // 
            // 聯絡人
            // 
            this.聯絡人.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.聯絡人.DataPropertyName = "aclia";
            this.聯絡人.HeaderText = "聯絡人";
            this.聯絡人.MaxInputLength = 20;
            this.聯絡人.Name = "聯絡人";
            this.聯絡人.ReadOnly = true;
            this.聯絡人.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.聯絡人.Width = 173;
            // 
            // 電話
            // 
            this.電話.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.電話.DataPropertyName = "actel";
            this.電話.HeaderText = "電話";
            this.電話.MaxInputLength = 20;
            this.電話.Name = "電話";
            this.電話.ReadOnly = true;
            this.電話.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.電話.Width = 173;
            // 
            // 行動電話
            // 
            this.行動電話.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.行動電話.DataPropertyName = "acacttel";
            this.行動電話.HeaderText = "行動電話";
            this.行動電話.MaxInputLength = 20;
            this.行動電話.Name = "行動電話";
            this.行動電話.ReadOnly = true;
            this.行動電話.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.行動電話.Width = 173;
            // 
            // 傳真
            // 
            this.傳真.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.傳真.DataPropertyName = "acfax";
            this.傳真.HeaderText = "傳真";
            this.傳真.MaxInputLength = 20;
            this.傳真.Name = "傳真";
            this.傳真.ReadOnly = true;
            this.傳真.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.傳真.Width = 173;
            // 
            // 地址
            // 
            this.地址.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.地址.DataPropertyName = "acaddr1";
            this.地址.HeaderText = "地址";
            this.地址.MaxInputLength = 60;
            this.地址.Name = "地址";
            this.地址.ReadOnly = true;
            this.地址.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.地址.Width = 493;
            // 
            // 郵遞區號
            // 
            this.郵遞區號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.郵遞區號.DataPropertyName = "acr1";
            this.郵遞區號.HeaderText = "郵遞區號";
            this.郵遞區號.MaxInputLength = 5;
            this.郵遞區號.MinimumWidth = 100;
            this.郵遞區號.Name = "郵遞區號";
            this.郵遞區號.ReadOnly = true;
            this.郵遞區號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 電子郵件
            // 
            this.電子郵件.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.電子郵件.DataPropertyName = "acemail";
            this.電子郵件.HeaderText = "電子郵件";
            this.電子郵件.MaxInputLength = 60;
            this.電子郵件.Name = "電子郵件";
            this.電子郵件.ReadOnly = true;
            this.電子郵件.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.電子郵件.Width = 493;
            // 
            // 備註
            // 
            this.備註.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.備註.DataPropertyName = "acmemo";
            this.備註.HeaderText = "備註";
            this.備註.MaxInputLength = 60;
            this.備註.Name = "備註";
            this.備註.ReadOnly = true;
            this.備註.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.備註.Width = 493;
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
            // 銀行帳號建檔_瀏覽
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Exit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.Get);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.AcNo);
            this.Name = "銀行帳號建檔_瀏覽";
            this.Text = "銀行帳號建檔[瀏覽]";
            this.Load += new System.EventHandler(this.銀行帳號建檔_瀏覽_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.TextBoxT AcNo;
        private JE.MyControl.ButtonSmallT Get;
        private JE.MyControl.ButtonSmallT Exit;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 帳戶編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 帳戶名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 帳戶幣別;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 銀行帳號;
        private JE.MyControl.DataGridViewTextNumberT 現行餘額;
        private System.Windows.Forms.DataGridViewTextBoxColumn 帳戶簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 帳戶類別;
        private System.Windows.Forms.DataGridViewTextBoxColumn 隸屬公司;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 字軌;
        private System.Windows.Forms.DataGridViewTextBoxColumn 聯絡人;
        private System.Windows.Forms.DataGridViewTextBoxColumn 電話;
        private System.Windows.Forms.DataGridViewTextBoxColumn 行動電話;
        private System.Windows.Forms.DataGridViewTextBoxColumn 傳真;
        private System.Windows.Forms.DataGridViewTextBoxColumn 地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 郵遞區號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 電子郵件;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}