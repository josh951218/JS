namespace S_61.subMenuFm_3
{
    partial class FrmPayabldb
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.PaNo = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.qq = new JE.MyControl.LabelT();
            this.FaNo = new JE.MyControl.TextBoxT();
            this.dataGridViewT2 = new JE.MyControl.DataGridViewT();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.帳款日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單據 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單據號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.幣別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.折讓金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.沖帳金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.沖款單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.沖款日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.廠商編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.廠商簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.沖抵帳款 = new JE.MyControl.DataGridViewTextNumberT();
            this.累入預付 = new JE.MyControl.DataGridViewTextNumberT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(549, 575);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(165, 44);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "F9:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // PaNo
            // 
            this.PaNo.AllowGrayBackColor = false;
            this.PaNo.AllowResize = true;
            this.PaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.PaNo.Location = new System.Drawing.Point(206, 584);
            this.PaNo.MaxLength = 10;
            this.PaNo.Name = "PaNo";
            this.PaNo.oLen = 0;
            this.PaNo.Size = new System.Drawing.Size(87, 27);
            this.PaNo.TabIndex = 1;
            this.PaNo.TextChanged += new System.EventHandler(this.PaNo_TextChanged);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(128, 589);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "付款單號";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(718, 575);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(165, 44);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "F11:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // qq
            // 
            this.qq.AutoSize = true;
            this.qq.BackColor = System.Drawing.Color.Transparent;
            this.qq.Font = new System.Drawing.Font("細明體", 12F);
            this.qq.Location = new System.Drawing.Point(348, 589);
            this.qq.Name = "qq";
            this.qq.Size = new System.Drawing.Size(72, 16);
            this.qq.TabIndex = 0;
            this.qq.Text = "廠商編號";
            // 
            // FaNo
            // 
            this.FaNo.AllowGrayBackColor = false;
            this.FaNo.AllowResize = true;
            this.FaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.FaNo.Location = new System.Drawing.Point(426, 584);
            this.FaNo.MaxLength = 10;
            this.FaNo.Name = "FaNo";
            this.FaNo.oLen = 0;
            this.FaNo.Size = new System.Drawing.Size(87, 27);
            this.FaNo.TabIndex = 2;
            this.FaNo.TextChanged += new System.EventHandler(this.PaNo_TextChanged);
            // 
            // dataGridViewT2
            // 
            this.dataGridViewT2.AllowUserToAddRows = false;
            this.dataGridViewT2.AllowUserToDeleteRows = false;
            this.dataGridViewT2.AllowUserToOrderColumns = true;
            this.dataGridViewT2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewT2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewT2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewT2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewT2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序號,
            this.帳款日期,
            this.單據,
            this.單據號碼,
            this.發票號碼,
            this.幣別,
            this.折讓金額,
            this.沖帳金額});
            this.dataGridViewT2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT2.EnableHeadersVisualStyles = false;
            this.dataGridViewT2.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT2.ISDocument = false;
            this.dataGridViewT2.Location = new System.Drawing.Point(0, 291);
            this.dataGridViewT2.MultiSelect = false;
            this.dataGridViewT2.Name = "dataGridViewT2";
            this.dataGridViewT2.ReadOnly = true;
            this.dataGridViewT2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT2.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT2.RowHeadersWidth = 20;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT2.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewT2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT2.ShowCellToolTips = false;
            this.dataGridViewT2.Size = new System.Drawing.Size(1010, 276);
            this.dataGridViewT2.TabIndex = 6;
            // 
            // 序號
            // 
            this.序號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.序號.DataPropertyName = "RecordNo";
            this.序號.HeaderText = "序號";
            this.序號.MaxInputLength = 6;
            this.序號.Name = "序號";
            this.序號.ReadOnly = true;
            this.序號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.序號.Width = 61;
            // 
            // 帳款日期
            // 
            this.帳款日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.帳款日期.DataPropertyName = "BsDateAc";
            this.帳款日期.HeaderText = "帳款日期";
            this.帳款日期.MaxInputLength = 10;
            this.帳款日期.Name = "帳款日期";
            this.帳款日期.ReadOnly = true;
            this.帳款日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.帳款日期.Width = 93;
            // 
            // 單據
            // 
            this.單據.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單據.DataPropertyName = "Bracket";
            this.單據.HeaderText = "單據";
            this.單據.MaxInputLength = 10;
            this.單據.Name = "單據";
            this.單據.ReadOnly = true;
            this.單據.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單據.Width = 93;
            // 
            // 單據號碼
            // 
            this.單據號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單據號碼.DataPropertyName = "BsNo";
            this.單據號碼.HeaderText = "單據號碼";
            this.單據號碼.MaxInputLength = 12;
            this.單據號碼.Name = "單據號碼";
            this.單據號碼.ReadOnly = true;
            this.單據號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單據號碼.Width = 109;
            // 
            // 發票號碼
            // 
            this.發票號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.發票號碼.DataPropertyName = "InvNo";
            this.發票號碼.HeaderText = "發票號碼";
            this.發票號碼.MaxInputLength = 10;
            this.發票號碼.Name = "發票號碼";
            this.發票號碼.ReadOnly = true;
            this.發票號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.發票號碼.Width = 93;
            // 
            // 幣別
            // 
            this.幣別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.幣別.DataPropertyName = "Xa1Name";
            this.幣別.HeaderText = "幣別";
            this.幣別.MaxInputLength = 8;
            this.幣別.Name = "幣別";
            this.幣別.ReadOnly = true;
            this.幣別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.幣別.Width = 77;
            // 
            // 折讓金額
            // 
            this.折讓金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折讓金額.DataPropertyName = "Discount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.折讓金額.DefaultCellStyle = dataGridViewCellStyle2;
            this.折讓金額.FirstNum = 0;
            this.折讓金額.HeaderText = "折讓金額";
            this.折讓金額.LastNum = 0;
            this.折讓金額.MarkThousand = false;
            this.折讓金額.MaxInputLength = 11;
            this.折讓金額.Name = "折讓金額";
            this.折讓金額.NullInput = false;
            this.折讓金額.NullValue = "0";
            this.折讓金額.ReadOnly = true;
            this.折讓金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.折讓金額.Width = 101;
            // 
            // 沖帳金額
            // 
            this.沖帳金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.沖帳金額.DataPropertyName = "Reverse";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.沖帳金額.DefaultCellStyle = dataGridViewCellStyle3;
            this.沖帳金額.FirstNum = 0;
            this.沖帳金額.HeaderText = "沖帳金額";
            this.沖帳金額.LastNum = 0;
            this.沖帳金額.MarkThousand = false;
            this.沖帳金額.MaxInputLength = 16;
            this.沖帳金額.Name = "沖帳金額";
            this.沖帳金額.NullInput = false;
            this.沖帳金額.NullValue = "0";
            this.沖帳金額.ReadOnly = true;
            this.沖帳金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.沖帳金額.Width = 141;
            // 
            // dataGridViewT1
            // 
            this.dataGridViewT1.AllowUserToAddRows = false;
            this.dataGridViewT1.AllowUserToDeleteRows = false;
            this.dataGridViewT1.AllowUserToOrderColumns = true;
            this.dataGridViewT1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewT1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewT1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewT1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewT1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.沖款單號,
            this.沖款日期,
            this.廠商編號,
            this.廠商簡稱,
            this.沖抵帳款,
            this.累入預付});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 285);
            this.dataGridViewT1.TabIndex = 5;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            this.dataGridViewT1.SelectionChanged += new System.EventHandler(this.dataGridViewT1_SelectionChanged);
            // 
            // 沖款單號
            // 
            this.沖款單號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.沖款單號.DataPropertyName = "PaNo";
            this.沖款單號.HeaderText = "沖款單號";
            this.沖款單號.MaxInputLength = 12;
            this.沖款單號.Name = "沖款單號";
            this.沖款單號.ReadOnly = true;
            this.沖款單號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.沖款單號.Width = 109;
            // 
            // 沖款日期
            // 
            this.沖款日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.沖款日期.DataPropertyName = "PaDate";
            this.沖款日期.HeaderText = "沖款日期";
            this.沖款日期.MaxInputLength = 10;
            this.沖款日期.Name = "沖款日期";
            this.沖款日期.ReadOnly = true;
            this.沖款日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.沖款日期.Width = 93;
            // 
            // 廠商編號
            // 
            this.廠商編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.廠商編號.DataPropertyName = "FaNo";
            this.廠商編號.HeaderText = "廠商編號";
            this.廠商編號.MaxInputLength = 10;
            this.廠商編號.Name = "廠商編號";
            this.廠商編號.ReadOnly = true;
            this.廠商編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.廠商編號.Width = 93;
            // 
            // 廠商簡稱
            // 
            this.廠商簡稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.廠商簡稱.DataPropertyName = "FaName1";
            this.廠商簡稱.HeaderText = "廠商簡稱";
            this.廠商簡稱.MaxInputLength = 10;
            this.廠商簡稱.Name = "廠商簡稱";
            this.廠商簡稱.ReadOnly = true;
            this.廠商簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.廠商簡稱.Width = 93;
            // 
            // 沖抵帳款
            // 
            this.沖抵帳款.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.沖抵帳款.DataPropertyName = "TotMny";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.沖抵帳款.DefaultCellStyle = dataGridViewCellStyle7;
            this.沖抵帳款.FirstNum = 0;
            this.沖抵帳款.HeaderText = "沖抵帳款";
            this.沖抵帳款.LastNum = 0;
            this.沖抵帳款.MarkThousand = false;
            this.沖抵帳款.MaxInputLength = 16;
            this.沖抵帳款.Name = "沖抵帳款";
            this.沖抵帳款.NullInput = false;
            this.沖抵帳款.NullValue = "0";
            this.沖抵帳款.ReadOnly = true;
            this.沖抵帳款.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.沖抵帳款.Width = 141;
            // 
            // 累入預付
            // 
            this.累入預付.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.累入預付.DataPropertyName = "AddPrvAcc";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.累入預付.DefaultCellStyle = dataGridViewCellStyle8;
            this.累入預付.FirstNum = 0;
            this.累入預付.HeaderText = "累入預付";
            this.累入預付.LastNum = 0;
            this.累入預付.MarkThousand = false;
            this.累入預付.MaxInputLength = 16;
            this.累入預付.Name = "累入預付";
            this.累入預付.NullInput = false;
            this.累入預付.NullValue = "0";
            this.累入預付.ReadOnly = true;
            this.累入預付.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.累入預付.Width = 141;
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
            // FrmPayabldb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.dataGridViewT2);
            this.Controls.Add(this.PaNo);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.qq);
            this.Controls.Add(this.FaNo);
            this.Controls.Add(this.btnExit);
            this.Name = "FrmPayabldb";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmPayabldb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.TextBoxT PaNo;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.DataGridViewT dataGridViewT2;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.LabelT qq;
        private JE.MyControl.TextBoxT FaNo;
        private JE.MyControl.ButtonSmallT btnGet;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 帳款日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單據;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單據號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 幣別;
        private JE.MyControl.DataGridViewTextNumberT 折讓金額;
        private JE.MyControl.DataGridViewTextNumberT 沖帳金額;
        private System.Windows.Forms.DataGridViewTextBoxColumn 沖款單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 沖款日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商簡稱;
        private JE.MyControl.DataGridViewTextNumberT 沖抵帳款;
        private JE.MyControl.DataGridViewTextNumberT 累入預付;
        private JE.MyControl.StatusStripT statusStripT1;

    }
}