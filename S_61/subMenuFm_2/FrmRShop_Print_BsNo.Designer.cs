namespace S_61.subMenuFm_2
{
    partial class FrmRShop_Print_BsNo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BsNo = new JE.MyControl.TextBoxT();
            this.FaNo = new JE.MyControl.TextBoxT();
            this.lblX1No = new JE.MyControl.LabelT();
            this.lblX1Name = new JE.MyControl.LabelT();
            this.btnQuery = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.進貨單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.進貨日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.廠商編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.廠商簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.稅別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.進貨總額 = new JE.MyControl.DataGridViewTextNumberT();
            this.業務 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewT2 = new JE.MyControl.DataGridViewT();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.計價數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.計位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單價 = new JE.MyControl.DataGridViewTextNumberT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).BeginInit();
            this.SuspendLayout();
            // 
            // BsNo
            // 
            this.BsNo.AllowGrayBackColor = false;
            this.BsNo.AllowResize = true;
            this.BsNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BsNo.Location = new System.Drawing.Point(129, 589);
            this.BsNo.MaxLength = 12;
            this.BsNo.Name = "BsNo";
            this.BsNo.oLen = 0;
            this.BsNo.Size = new System.Drawing.Size(103, 27);
            this.BsNo.TabIndex = 1;
            this.BsNo.TextChanged += new System.EventHandler(this.BsNo_TextChanged);
            // 
            // FaNo
            // 
            this.FaNo.AllowGrayBackColor = false;
            this.FaNo.AllowResize = true;
            this.FaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.FaNo.Location = new System.Drawing.Point(318, 589);
            this.FaNo.MaxLength = 10;
            this.FaNo.Name = "FaNo";
            this.FaNo.oLen = 0;
            this.FaNo.Size = new System.Drawing.Size(87, 27);
            this.FaNo.TabIndex = 2;
            this.FaNo.TextChanged += new System.EventHandler(this.BsNo_TextChanged);
            // 
            // lblX1No
            // 
            this.lblX1No.AutoSize = true;
            this.lblX1No.BackColor = System.Drawing.Color.Transparent;
            this.lblX1No.Font = new System.Drawing.Font("細明體", 12F);
            this.lblX1No.Location = new System.Drawing.Point(57, 594);
            this.lblX1No.Name = "lblX1No";
            this.lblX1No.Size = new System.Drawing.Size(72, 16);
            this.lblX1No.TabIndex = 0;
            this.lblX1No.Text = "進貨單號";
            // 
            // lblX1Name
            // 
            this.lblX1Name.AutoSize = true;
            this.lblX1Name.BackColor = System.Drawing.Color.Transparent;
            this.lblX1Name.Font = new System.Drawing.Font("細明體", 12F);
            this.lblX1Name.Location = new System.Drawing.Point(246, 594);
            this.lblX1Name.Name = "lblX1Name";
            this.lblX1Name.Size = new System.Drawing.Size(72, 16);
            this.lblX1Name.TabIndex = 0;
            this.lblX1Name.Text = "廠商編號";
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(435, 581);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(173, 42);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "F6:字元查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(608, 581);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(173, 42);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "F9:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(781, 581);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(173, 42);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "F11:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.進貨單號,
            this.進貨日期,
            this.廠商編號,
            this.廠商簡稱,
            this.稅別,
            this.進貨總額,
            this.業務});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 300);
            this.dataGridViewT1.TabIndex = 11;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            this.dataGridViewT1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewT1_RowStateChanged);
            // 
            // 進貨單號
            // 
            this.進貨單號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.進貨單號.DataPropertyName = "bsno";
            this.進貨單號.HeaderText = "退貨單號";
            this.進貨單號.MaxInputLength = 12;
            this.進貨單號.Name = "進貨單號";
            this.進貨單號.ReadOnly = true;
            this.進貨單號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.進貨單號.Width = 109;
            // 
            // 進貨日期
            // 
            this.進貨日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.進貨日期.HeaderText = "退貨日期";
            this.進貨日期.MaxInputLength = 10;
            this.進貨日期.Name = "進貨日期";
            this.進貨日期.ReadOnly = true;
            this.進貨日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.進貨日期.Width = 93;
            // 
            // 廠商編號
            // 
            this.廠商編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.廠商編號.DataPropertyName = "fano";
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
            this.廠商簡稱.DataPropertyName = "faname1";
            this.廠商簡稱.HeaderText = "廠商簡稱";
            this.廠商簡稱.MaxInputLength = 10;
            this.廠商簡稱.Name = "廠商簡稱";
            this.廠商簡稱.ReadOnly = true;
            this.廠商簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.廠商簡稱.Width = 93;
            // 
            // 稅別
            // 
            this.稅別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅別.DataPropertyName = "X3Name";
            this.稅別.HeaderText = "稅別";
            this.稅別.MaxInputLength = 8;
            this.稅別.Name = "稅別";
            this.稅別.ReadOnly = true;
            this.稅別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅別.Width = 77;
            // 
            // 進貨總額
            // 
            this.進貨總額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.進貨總額.DataPropertyName = "TotMny";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.進貨總額.DefaultCellStyle = dataGridViewCellStyle2;
            this.進貨總額.FirstNum = 0;
            this.進貨總額.HeaderText = "退貨總額";
            this.進貨總額.LastNum = 0;
            this.進貨總額.MarkThousand = false;
            this.進貨總額.MaxInputLength = 16;
            this.進貨總額.Name = "進貨總額";
            this.進貨總額.NullInput = false;
            this.進貨總額.NullValue = "0";
            this.進貨總額.ReadOnly = true;
            this.進貨總額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.進貨總額.Width = 141;
            // 
            // 業務
            // 
            this.業務.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.業務.DataPropertyName = "EmName";
            this.業務.HeaderText = "業務";
            this.業務.MaxInputLength = 10;
            this.業務.Name = "業務";
            this.業務.ReadOnly = true;
            this.業務.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.業務.Width = 93;
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewT2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序號,
            this.產品編號,
            this.品名規格,
            this.數量,
            this.單位,
            this.計價數量,
            this.計位,
            this.單價});
            this.dataGridViewT2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT2.EnableHeadersVisualStyles = false;
            this.dataGridViewT2.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT2.ISDocument = false;
            this.dataGridViewT2.Location = new System.Drawing.Point(0, 305);
            this.dataGridViewT2.MultiSelect = false;
            this.dataGridViewT2.Name = "dataGridViewT2";
            this.dataGridViewT2.ReadOnly = true;
            this.dataGridViewT2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT2.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewT2.RowHeadersWidth = 20;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT2.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewT2.RowTemplate.Height = 24;
            this.dataGridViewT2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT2.ShowCellToolTips = false;
            this.dataGridViewT2.Size = new System.Drawing.Size(1010, 268);
            this.dataGridViewT2.TabIndex = 12;
            this.dataGridViewT2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT2_CellDoubleClick);
            // 
            // 序號
            // 
            this.序號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.序號.DataPropertyName = "ReCordNo";
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
            this.產品編號.DataPropertyName = "ItNo";
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
            this.品名規格.DataPropertyName = "ItName";
            this.品名規格.HeaderText = "品名規格";
            this.品名規格.MaxInputLength = 30;
            this.品名規格.Name = "品名規格";
            this.品名規格.ReadOnly = true;
            this.品名規格.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.品名規格.Width = 253;
            // 
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.數量.DataPropertyName = "Qty";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.數量.DefaultCellStyle = dataGridViewCellStyle6;
            this.數量.FirstNum = 0;
            this.數量.HeaderText = "退貨數量";
            this.數量.LastNum = 0;
            this.數量.MarkThousand = false;
            this.數量.MaxInputLength = 11;
            this.數量.Name = "數量";
            this.數量.NullInput = false;
            this.數量.NullValue = "0";
            this.數量.ReadOnly = true;
            this.數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.數量.Width = 101;
            // 
            // 單位
            // 
            this.單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位.DataPropertyName = "itUnit";
            this.單位.HeaderText = "單位";
            this.單位.MaxInputLength = 4;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 45;
            // 
            // 計價數量
            // 
            this.計價數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.計價數量.DataPropertyName = "pqty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.計價數量.DefaultCellStyle = dataGridViewCellStyle7;
            this.計價數量.FirstNum = 0;
            this.計價數量.HeaderText = "計價數量";
            this.計價數量.LastNum = 0;
            this.計價數量.MarkThousand = false;
            this.計價數量.MaxInputLength = 11;
            this.計價數量.Name = "計價數量";
            this.計價數量.NullInput = false;
            this.計價數量.NullValue = "0";
            this.計價數量.ReadOnly = true;
            this.計價數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.計價數量.Width = 101;
            // 
            // 計位
            // 
            this.計位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.計位.DataPropertyName = "punit";
            this.計位.HeaderText = "計位";
            this.計位.MaxInputLength = 4;
            this.計位.Name = "計位";
            this.計位.ReadOnly = true;
            this.計位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.計位.Width = 45;
            // 
            // 單價
            // 
            this.單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單價.DataPropertyName = "Price";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單價.DefaultCellStyle = dataGridViewCellStyle8;
            this.單價.FirstNum = 0;
            this.單價.HeaderText = "單價";
            this.單價.LastNum = 0;
            this.單價.MarkThousand = false;
            this.單價.MaxInputLength = 16;
            this.單價.Name = "單價";
            this.單價.NullInput = false;
            this.單價.NullValue = "0";
            this.單價.ReadOnly = true;
            this.單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單價.Width = 141;
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
            // FrmRShop_Print_BsNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.BsNo);
            this.Controls.Add(this.dataGridViewT2);
            this.Controls.Add(this.FaNo);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.lblX1No);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.lblX1Name);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnExit);
            this.Name = "FrmRShop_Print_BsNo";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmRShop_Print_BsNo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.TextBoxT BsNo;
        private JE.MyControl.TextBoxT FaNo;
        private JE.MyControl.LabelT lblX1No;
        private JE.MyControl.LabelT lblX1Name;
        private JE.MyControl.ButtonSmallT btnQuery;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.DataGridViewT dataGridViewT2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 進貨單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 進貨日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 稅別;
        private JE.MyControl.DataGridViewTextNumberT 進貨總額;
        private System.Windows.Forms.DataGridViewTextBoxColumn 業務;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private JE.MyControl.DataGridViewTextNumberT 數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 計價數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 計位;
        private JE.MyControl.DataGridViewTextNumberT 單價;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}