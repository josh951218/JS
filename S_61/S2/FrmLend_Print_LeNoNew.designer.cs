namespace S_61.S2
{
    partial class FrmLend_Print_LeNoNew
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.借出單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.借出日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.倉庫編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.倉庫名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.借出人員 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewT2 = new JE.MyControl.DataGridViewT();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單價 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelT1 = new JE.MyControl.LabelT();
            this.LeNo = new JE.MyControl.TextBoxT();
            this.btnQuery = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).BeginInit();
            this.SuspendLayout();
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
            this.借出單號,
            this.借出日期,
            this.客戶編號,
            this.客戶名稱,
            this.倉庫編號,
            this.倉庫名稱,
            this.借出人員});
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 294);
            this.dataGridViewT1.TabIndex = 0;
            this.dataGridViewT1.SelectionChanged += new System.EventHandler(this.dataGridViewT1_SelectionChanged);
            this.dataGridViewT1.DoubleClick += new System.EventHandler(this.dataGridViewT1_DoubleClick);
            // 
            // 借出單號
            // 
            this.借出單號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.借出單號.DataPropertyName = "leno";
            this.借出單號.HeaderText = "借出單號";
            this.借出單號.MaxInputLength = 12;
            this.借出單號.Name = "借出單號";
            this.借出單號.ReadOnly = true;
            this.借出單號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.借出單號.Width = 109;
            // 
            // 借出日期
            // 
            this.借出日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.借出日期.HeaderText = "借出日期";
            this.借出日期.MaxInputLength = 11;
            this.借出日期.Name = "借出日期";
            this.借出日期.ReadOnly = true;
            this.借出日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.借出日期.Width = 101;
            // 
            // 客戶編號
            // 
            this.客戶編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶編號.DataPropertyName = "cuno";
            this.客戶編號.HeaderText = "客戶編號";
            this.客戶編號.MaxInputLength = 10;
            this.客戶編號.Name = "客戶編號";
            this.客戶編號.ReadOnly = true;
            this.客戶編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶編號.Width = 93;
            // 
            // 客戶名稱
            // 
            this.客戶名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶名稱.DataPropertyName = "cuname1";
            this.客戶名稱.HeaderText = "客戶名稱";
            this.客戶名稱.MaxInputLength = 10;
            this.客戶名稱.Name = "客戶名稱";
            this.客戶名稱.ReadOnly = true;
            this.客戶名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶名稱.Width = 93;
            // 
            // 倉庫編號
            // 
            this.倉庫編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.倉庫編號.DataPropertyName = "stno";
            this.倉庫編號.HeaderText = "倉庫編號";
            this.倉庫編號.MaxInputLength = 10;
            this.倉庫編號.Name = "倉庫編號";
            this.倉庫編號.ReadOnly = true;
            this.倉庫編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.倉庫編號.Width = 93;
            // 
            // 倉庫名稱
            // 
            this.倉庫名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.倉庫名稱.DataPropertyName = "stname";
            this.倉庫名稱.HeaderText = "倉庫名稱";
            this.倉庫名稱.MaxInputLength = 10;
            this.倉庫名稱.Name = "倉庫名稱";
            this.倉庫名稱.ReadOnly = true;
            this.倉庫名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.倉庫名稱.Width = 93;
            // 
            // 借出人員
            // 
            this.借出人員.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.借出人員.DataPropertyName = "emname";
            this.借出人員.HeaderText = "借出人員";
            this.借出人員.MaxInputLength = 10;
            this.借出人員.Name = "借出人員";
            this.借出人員.ReadOnly = true;
            this.借出人員.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.借出人員.Width = 93;
            // 
            // dataGridViewT2
            // 
            this.dataGridViewT2.AllowUserToAddRows = false;
            this.dataGridViewT2.AllowUserToDeleteRows = false;
            this.dataGridViewT2.AllowUserToOrderColumns = true;
            this.dataGridViewT2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewT2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewT2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序號,
            this.產品編號,
            this.品名規格,
            this.單位,
            this.數量,
            this.單價});
            this.dataGridViewT2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT2.EnableHeadersVisualStyles = false;
            this.dataGridViewT2.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT2.ISDocument = false;
            this.dataGridViewT2.Location = new System.Drawing.Point(0, 298);
            this.dataGridViewT2.MultiSelect = false;
            this.dataGridViewT2.Name = "dataGridViewT2";
            this.dataGridViewT2.ReadOnly = true;
            this.dataGridViewT2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT2.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewT2.RowHeadersWidth = 20;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT2.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewT2.RowTemplate.Height = 24;
            this.dataGridViewT2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT2.ShowCellToolTips = false;
            this.dataGridViewT2.Size = new System.Drawing.Size(1010, 276);
            this.dataGridViewT2.TabIndex = 1;
            this.dataGridViewT2.DoubleClick += new System.EventHandler(this.dataGridViewT2_DoubleClick);
            // 
            // 序號
            // 
            this.序號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.序號.DataPropertyName = "序號";
            this.序號.HeaderText = "序號";
            this.序號.MaxInputLength = 6;
            this.序號.Name = "序號";
            this.序號.ReadOnly = true;
            this.序號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.序號.Width = 61;
            // 
            // 產品編號
            // 
            this.產品編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品編號.DataPropertyName = "itno";
            this.產品編號.HeaderText = "產品編號";
            this.產品編號.MaxInputLength = 20;
            this.產品編號.Name = "產品編號";
            this.產品編號.ReadOnly = true;
            this.產品編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.產品編號.Width = 173;
            // 
            // 品名規格
            // 
            this.品名規格.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.品名規格.DataPropertyName = "itname";
            this.品名規格.HeaderText = "品名規格";
            this.品名規格.MaxInputLength = 30;
            this.品名規格.Name = "品名規格";
            this.品名規格.ReadOnly = true;
            this.品名規格.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.品名規格.Width = 253;
            // 
            // 單位
            // 
            this.單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位.DataPropertyName = "itunit";
            this.單位.HeaderText = "單位";
            this.單位.MaxInputLength = 4;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 45;
            // 
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.數量.DataPropertyName = "qty";
            this.數量.HeaderText = "數量";
            this.數量.MaxInputLength = 11;
            this.數量.Name = "數量";
            this.數量.ReadOnly = true;
            this.數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.數量.Width = 101;
            // 
            // 單價
            // 
            this.單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單價.DataPropertyName = "price";
            this.單價.HeaderText = "單價";
            this.單價.MaxInputLength = 11;
            this.單價.Name = "單價";
            this.單價.ReadOnly = true;
            this.單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單價.Width = 101;
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(144, 592);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "借出單號";
            // 
            // LeNo
            // 
            this.LeNo.AllowGrayBackColor = false;
            this.LeNo.AllowResize = true;
            this.LeNo.Font = new System.Drawing.Font("細明體", 12F);
            this.LeNo.Location = new System.Drawing.Point(222, 587);
            this.LeNo.MaxLength = 12;
            this.LeNo.Name = "LeNo";
            this.LeNo.oLen = 0;
            this.LeNo.Size = new System.Drawing.Size(103, 27);
            this.LeNo.TabIndex = 2;
            this.LeNo.TextChanged += new System.EventHandler(this.LeNo_TextChanged); 
            // 
            // btnQuery
            // 
            this.btnQuery.AutoSize = true;
            this.btnQuery.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(353, 579);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(167, 42);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "F6：字元查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnGet
            // 
            this.btnGet.AutoSize = true;
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(526, 579);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(167, 42);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "F9：取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(699, 579);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(167, 42);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "F11：結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            // FrmLend_Print_LeNoNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.LeNo);
            this.Controls.Add(this.labelT1);
            this.Controls.Add(this.dataGridViewT2);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "FrmLend_Print_LeNoNew";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmLend_Print_LeNoNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.DataGridViewT dataGridViewT2;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.TextBoxT LeNo;
        private JE.MyControl.ButtonSmallT btnQuery;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 借出單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 借出日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 借出人員;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單價;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}