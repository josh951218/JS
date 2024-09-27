namespace S_61.SOther
{
    partial class FrmBom
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBom));
            this.BoItNo = new JE.MyControl.TextBoxT();
            this.BoItName = new JE.MyControl.TextBoxT();
            this.lblBoItNo = new JE.MyControl.LabelT();
            this.lblBoItName = new JE.MyControl.LabelT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.標準用量 = new JE.MyControl.DataGridViewTextNumberT();
            this.母件比例 = new JE.MyControl.DataGridViewTextNumberT();
            this.單價 = new JE.MyControl.DataGridViewTextNumberT();
            this.折數 = new JE.MyControl.DataGridViewTextNumberT();
            this.金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridAppend = new JE.MyControl.ButtonGridT();
            this.gridDelete = new JE.MyControl.ButtonGridT();
            this.gridPicture = new JE.MyControl.ButtonGridT();
            this.gridInsert = new JE.MyControl.ButtonGridT();
            this.gridStockQuery = new JE.MyControl.ButtonGridT();
            this.gridItBuyPrice = new JE.MyControl.ButtonGridT();
            this.lblBoTotQty = new JE.MyControl.LabelT();
            this.lblBoTotMny = new JE.MyControl.LabelT();
            this.lblBoMemo = new JE.MyControl.LabelT();
            this.BoTotQty = new JE.MyControl.TextBoxNumberT();
            this.BoTotMny = new JE.MyControl.TextBoxNumberT();
            this.BoMemo = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.btnPrint = new JE.MyControl.ButtonT();
            this.btnDelete = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.btnDuplicate = new JE.MyControl.ButtonT();
            this.btnAppend = new JE.MyControl.ButtonT();
            this.btnBottom = new JE.MyControl.ButtonT();
            this.btnNext = new JE.MyControl.ButtonT();
            this.btnPrior = new JE.MyControl.ButtonT();
            this.btnTop = new JE.MyControl.ButtonT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gridItDesp = new JE.MyControl.ButtonGridT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.panelT1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BoItNo
            // 
            this.BoItNo.AllowGrayBackColor = false;
            this.BoItNo.AllowResize = true;
            this.BoItNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BoItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BoItNo.Location = new System.Drawing.Point(96, 6);
            this.BoItNo.MaxLength = 20;
            this.BoItNo.Name = "BoItNo";
            this.BoItNo.oLen = 0;
            this.BoItNo.ReadOnly = true;
            this.BoItNo.Size = new System.Drawing.Size(167, 27);
            this.BoItNo.TabIndex = 0;
            this.BoItNo.TabStop = false;
            this.BoItNo.ReadOnlyChanged += new System.EventHandler(this.BoItNo_ReadOnlyChanged);
            this.BoItNo.DoubleClick += new System.EventHandler(this.BoItNo_DoubleClick);
            this.BoItNo.Enter += new System.EventHandler(this.BoItNo_Enter);
            this.BoItNo.Validating += new System.ComponentModel.CancelEventHandler(this.BoItNo_Validating);
            // 
            // BoItName
            // 
            this.BoItName.AllowGrayBackColor = false;
            this.BoItName.AllowResize = true;
            this.BoItName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BoItName.Font = new System.Drawing.Font("細明體", 12F);
            this.BoItName.Location = new System.Drawing.Point(477, 6);
            this.BoItName.MaxLength = 30;
            this.BoItName.Name = "BoItName";
            this.BoItName.oLen = 0;
            this.BoItName.ReadOnly = true;
            this.BoItName.Size = new System.Drawing.Size(247, 27);
            this.BoItName.TabIndex = 1;
            this.BoItName.TabStop = false;
            this.BoItName.Leave += new System.EventHandler(this.BoItName_Leave);
            // 
            // lblBoItNo
            // 
            this.lblBoItNo.AutoSize = true;
            this.lblBoItNo.BackColor = System.Drawing.Color.Transparent;
            this.lblBoItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.lblBoItNo.Location = new System.Drawing.Point(15, 11);
            this.lblBoItNo.Name = "lblBoItNo";
            this.lblBoItNo.Size = new System.Drawing.Size(72, 16);
            this.lblBoItNo.TabIndex = 0;
            this.lblBoItNo.Text = "組件編號";
            // 
            // lblBoItName
            // 
            this.lblBoItName.AutoSize = true;
            this.lblBoItName.BackColor = System.Drawing.Color.Transparent;
            this.lblBoItName.Font = new System.Drawing.Font("細明體", 12F);
            this.lblBoItName.Location = new System.Drawing.Point(399, 11);
            this.lblBoItName.Name = "lblBoItName";
            this.lblBoItName.Size = new System.Drawing.Size(72, 16);
            this.lblBoItName.TabIndex = 0;
            this.lblBoItName.Text = "組件名稱";
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
            this.序號,
            this.產品編號,
            this.品名規格,
            this.單位,
            this.標準用量,
            this.母件比例,
            this.單價,
            this.折數,
            this.金額,
            this.包裝數量,
            this.說明});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = true;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 39);
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
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 392);
            this.dataGridViewT1.TabIndex = 2;
            this.dataGridViewT1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewT1_CellBeginEdit);
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            this.dataGridViewT1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewT1_CellValidating);
            this.dataGridViewT1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewT1_EditingControlShowing);
            this.dataGridViewT1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewT1_RowsAdded);
            this.dataGridViewT1.Click += new System.EventHandler(this.dataGridViewT1_Click);
            // 
            // 序號
            // 
            this.序號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.序號.HeaderText = "序號";
            this.序號.MaxInputLength = 4;
            this.序號.Name = "序號";
            this.序號.ReadOnly = true;
            this.序號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.序號.Width = 45;
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
            // 單位
            // 
            this.單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位.DataPropertyName = "ItUnit";
            this.單位.HeaderText = "單位";
            this.單位.MaxInputLength = 4;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 45;
            // 
            // 標準用量
            // 
            this.標準用量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.標準用量.DataPropertyName = "ItQty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.標準用量.DefaultCellStyle = dataGridViewCellStyle2;
            this.標準用量.FirstNum = 0;
            this.標準用量.HeaderText = "標準用量";
            this.標準用量.LastNum = 0;
            this.標準用量.MarkThousand = false;
            this.標準用量.MaxInputLength = 16;
            this.標準用量.Name = "標準用量";
            this.標準用量.NullInput = false;
            this.標準用量.NullValue = "0";
            this.標準用量.ReadOnly = true;
            this.標準用量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.標準用量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.標準用量.Width = 141;
            // 
            // 母件比例
            // 
            this.母件比例.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.母件比例.DataPropertyName = "ItParePrs";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.母件比例.DefaultCellStyle = dataGridViewCellStyle3;
            this.母件比例.FirstNum = 0;
            this.母件比例.HeaderText = "/ 母件比例";
            this.母件比例.LastNum = 0;
            this.母件比例.MarkThousand = false;
            this.母件比例.MaxInputLength = 16;
            this.母件比例.Name = "母件比例";
            this.母件比例.NullInput = false;
            this.母件比例.NullValue = "0";
            this.母件比例.ReadOnly = true;
            this.母件比例.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.母件比例.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.母件比例.Width = 141;
            // 
            // 單價
            // 
            this.單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單價.DataPropertyName = "ItPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單價.DefaultCellStyle = dataGridViewCellStyle4;
            this.單價.FirstNum = 0;
            this.單價.HeaderText = "* 單價";
            this.單價.LastNum = 0;
            this.單價.MarkThousand = false;
            this.單價.MaxInputLength = 16;
            this.單價.Name = "單價";
            this.單價.NullInput = false;
            this.單價.NullValue = "0";
            this.單價.ReadOnly = true;
            this.單價.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單價.Width = 141;
            // 
            // 折數
            // 
            this.折數.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折數.DataPropertyName = "ItPrs";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.折數.DefaultCellStyle = dataGridViewCellStyle5;
            this.折數.FirstNum = 0;
            this.折數.HeaderText = "* 折數";
            this.折數.LastNum = 0;
            this.折數.MarkThousand = false;
            this.折數.MaxInputLength = 8;
            this.折數.Name = "折數";
            this.折數.NullInput = false;
            this.折數.NullValue = "0";
            this.折數.ReadOnly = true;
            this.折數.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.折數.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.折數.Width = 77;
            // 
            // 金額
            // 
            this.金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.金額.DataPropertyName = "ItMny";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.金額.DefaultCellStyle = dataGridViewCellStyle6;
            this.金額.FirstNum = 0;
            this.金額.HeaderText = "= 金額";
            this.金額.LastNum = 0;
            this.金額.MarkThousand = false;
            this.金額.MaxInputLength = 16;
            this.金額.Name = "金額";
            this.金額.NullInput = false;
            this.金額.NullValue = "0";
            this.金額.ReadOnly = true;
            this.金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.金額.Width = 141;
            // 
            // 包裝數量
            // 
            this.包裝數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量.DataPropertyName = "ItPkgQty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝數量.DefaultCellStyle = dataGridViewCellStyle7;
            this.包裝數量.FirstNum = 0;
            this.包裝數量.HeaderText = "包裝數量";
            this.包裝數量.LastNum = 0;
            this.包裝數量.MarkThousand = false;
            this.包裝數量.MaxInputLength = 10;
            this.包裝數量.Name = "包裝數量";
            this.包裝數量.NullInput = false;
            this.包裝數量.NullValue = "0";
            this.包裝數量.ReadOnly = true;
            this.包裝數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.包裝數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝數量.Width = 93;
            // 
            // 說明
            // 
            this.說明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.說明.DataPropertyName = "ItNote";
            this.說明.HeaderText = "說明";
            this.說明.MaxInputLength = 20;
            this.說明.Name = "說明";
            this.說明.ReadOnly = true;
            this.說明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.說明.Width = 173;
            // 
            // gridAppend
            // 
            this.gridAppend.BackColor = System.Drawing.SystemColors.Control;
            this.gridAppend.Enabled = false;
            this.gridAppend.Font = new System.Drawing.Font("新細明體", 12F);
            this.gridAppend.Location = new System.Drawing.Point(1, 437);
            this.gridAppend.Name = "gridAppend";
            this.gridAppend.Size = new System.Drawing.Size(144, 31);
            this.gridAppend.TabIndex = 3;
            this.gridAppend.Text = "F2:新增";
            this.gridAppend.UseVisualStyleBackColor = false;
            this.gridAppend.Click += new System.EventHandler(this.gridAppend_Click);
            // 
            // gridDelete
            // 
            this.gridDelete.BackColor = System.Drawing.SystemColors.Control;
            this.gridDelete.Enabled = false;
            this.gridDelete.Font = new System.Drawing.Font("新細明體", 12F);
            this.gridDelete.Location = new System.Drawing.Point(145, 437);
            this.gridDelete.Name = "gridDelete";
            this.gridDelete.Size = new System.Drawing.Size(144, 31);
            this.gridDelete.TabIndex = 4;
            this.gridDelete.Text = "F3:刪除";
            this.gridDelete.UseVisualStyleBackColor = false;
            this.gridDelete.Click += new System.EventHandler(this.gridDelete_Click);
            // 
            // gridPicture
            // 
            this.gridPicture.BackColor = System.Drawing.SystemColors.Control;
            this.gridPicture.Enabled = false;
            this.gridPicture.Font = new System.Drawing.Font("新細明體", 12F);
            this.gridPicture.Location = new System.Drawing.Point(289, 437);
            this.gridPicture.Name = "gridPicture";
            this.gridPicture.Size = new System.Drawing.Size(144, 31);
            this.gridPicture.TabIndex = 5;
            this.gridPicture.Text = "看圖";
            this.gridPicture.UseVisualStyleBackColor = false;
            this.gridPicture.Click += new System.EventHandler(this.gridPicture_Click);
            // 
            // gridInsert
            // 
            this.gridInsert.BackColor = System.Drawing.SystemColors.Control;
            this.gridInsert.Enabled = false;
            this.gridInsert.Font = new System.Drawing.Font("新細明體", 12F);
            this.gridInsert.Location = new System.Drawing.Point(433, 437);
            this.gridInsert.Name = "gridInsert";
            this.gridInsert.Size = new System.Drawing.Size(144, 31);
            this.gridInsert.TabIndex = 6;
            this.gridInsert.Text = "F5:插入";
            this.gridInsert.UseVisualStyleBackColor = false;
            this.gridInsert.Click += new System.EventHandler(this.gridInsert_Click);
            // 
            // gridStockQuery
            // 
            this.gridStockQuery.BackColor = System.Drawing.SystemColors.Control;
            this.gridStockQuery.Enabled = false;
            this.gridStockQuery.Font = new System.Drawing.Font("新細明體", 12F);
            this.gridStockQuery.Location = new System.Drawing.Point(577, 437);
            this.gridStockQuery.Name = "gridStockQuery";
            this.gridStockQuery.Size = new System.Drawing.Size(144, 31);
            this.gridStockQuery.TabIndex = 7;
            this.gridStockQuery.Text = "F6:庫存查詢";
            this.gridStockQuery.UseVisualStyleBackColor = false;
            this.gridStockQuery.Click += new System.EventHandler(this.gridStockQuery_Click);
            // 
            // gridItBuyPrice
            // 
            this.gridItBuyPrice.BackColor = System.Drawing.SystemColors.Control;
            this.gridItBuyPrice.Enabled = false;
            this.gridItBuyPrice.Font = new System.Drawing.Font("新細明體", 12F);
            this.gridItBuyPrice.Location = new System.Drawing.Point(865, 437);
            this.gridItBuyPrice.Name = "gridItBuyPrice";
            this.gridItBuyPrice.Size = new System.Drawing.Size(144, 31);
            this.gridItBuyPrice.TabIndex = 9;
            this.gridItBuyPrice.Text = "F8:進價查詢";
            this.gridItBuyPrice.UseVisualStyleBackColor = false;
            this.gridItBuyPrice.Click += new System.EventHandler(this.gridBuyQuery_Click);
            // 
            // lblBoTotQty
            // 
            this.lblBoTotQty.AutoSize = true;
            this.lblBoTotQty.BackColor = System.Drawing.Color.Transparent;
            this.lblBoTotQty.Font = new System.Drawing.Font("細明體", 12F);
            this.lblBoTotQty.Location = new System.Drawing.Point(15, 479);
            this.lblBoTotQty.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblBoTotQty.Name = "lblBoTotQty";
            this.lblBoTotQty.Size = new System.Drawing.Size(72, 16);
            this.lblBoTotQty.TabIndex = 0;
            this.lblBoTotQty.Text = "用量總計";
            // 
            // lblBoTotMny
            // 
            this.lblBoTotMny.AutoSize = true;
            this.lblBoTotMny.BackColor = System.Drawing.Color.Transparent;
            this.lblBoTotMny.Font = new System.Drawing.Font("細明體", 12F);
            this.lblBoTotMny.Location = new System.Drawing.Point(327, 479);
            this.lblBoTotMny.Name = "lblBoTotMny";
            this.lblBoTotMny.Size = new System.Drawing.Size(72, 16);
            this.lblBoTotMny.TabIndex = 0;
            this.lblBoTotMny.Text = "金額合計";
            // 
            // lblBoMemo
            // 
            this.lblBoMemo.AutoSize = true;
            this.lblBoMemo.BackColor = System.Drawing.Color.Transparent;
            this.lblBoMemo.Font = new System.Drawing.Font("細明體", 12F);
            this.lblBoMemo.Location = new System.Drawing.Point(15, 512);
            this.lblBoMemo.Name = "lblBoMemo";
            this.lblBoMemo.Size = new System.Drawing.Size(72, 16);
            this.lblBoMemo.TabIndex = 0;
            this.lblBoMemo.Text = "備    註";
            // 
            // BoTotQty
            // 
            this.BoTotQty.AllowGrayBackColor = true;
            this.BoTotQty.AllowResize = true;
            this.BoTotQty.BackColor = System.Drawing.Color.Silver;
            this.BoTotQty.FirstNum = 10;
            this.BoTotQty.Font = new System.Drawing.Font("細明體", 12F);
            this.BoTotQty.LastNum = 0;
            this.BoTotQty.Location = new System.Drawing.Point(92, 474);
            this.BoTotQty.MarkThousand = false;
            this.BoTotQty.MaxLength = 20;
            this.BoTotQty.Name = "BoTotQty";
            this.BoTotQty.NullInput = false;
            this.BoTotQty.NullValue = "0";
            this.BoTotQty.oLen = 0;
            this.BoTotQty.ReadOnly = true;
            this.BoTotQty.Size = new System.Drawing.Size(167, 27);
            this.BoTotQty.TabIndex = 10;
            this.BoTotQty.TabStop = false;
            this.BoTotQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BoTotMny
            // 
            this.BoTotMny.AllowGrayBackColor = true;
            this.BoTotMny.AllowResize = true;
            this.BoTotMny.BackColor = System.Drawing.Color.Silver;
            this.BoTotMny.FirstNum = 10;
            this.BoTotMny.Font = new System.Drawing.Font("細明體", 12F);
            this.BoTotMny.LastNum = 0;
            this.BoTotMny.Location = new System.Drawing.Point(405, 474);
            this.BoTotMny.MarkThousand = false;
            this.BoTotMny.MaxLength = 20;
            this.BoTotMny.Name = "BoTotMny";
            this.BoTotMny.NullInput = false;
            this.BoTotMny.NullValue = "0";
            this.BoTotMny.oLen = 0;
            this.BoTotMny.ReadOnly = true;
            this.BoTotMny.Size = new System.Drawing.Size(167, 27);
            this.BoTotMny.TabIndex = 11;
            this.BoTotMny.TabStop = false;
            this.BoTotMny.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BoMemo
            // 
            this.BoMemo.AllowGrayBackColor = false;
            this.BoMemo.AllowResize = true;
            this.BoMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BoMemo.Font = new System.Drawing.Font("細明體", 12F);
            this.BoMemo.Location = new System.Drawing.Point(93, 507);
            this.BoMemo.MaxLength = 80;
            this.BoMemo.Name = "BoMemo";
            this.BoMemo.oLen = 0;
            this.BoMemo.ReadOnly = true;
            this.BoMemo.Size = new System.Drawing.Size(647, 27);
            this.BoMemo.TabIndex = 12;
            this.BoMemo.TabStop = false;
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Controls.Add(this.btnPrint);
            this.panelT1.Controls.Add(this.btnDelete);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Controls.Add(this.btnDuplicate);
            this.panelT1.Controls.Add(this.btnAppend);
            this.panelT1.Controls.Add(this.btnBottom);
            this.panelT1.Controls.Add(this.btnNext);
            this.panelT1.Controls.Add(this.btnPrior);
            this.panelT1.Controls.Add(this.btnTop);
            this.panelT1.Location = new System.Drawing.Point(52, 542);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(907, 79);
            this.panelT1.TabIndex = 13;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnExit.Location = new System.Drawing.Point(828, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 12;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnCancel.Location = new System.Drawing.Point(759, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 79);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.UseDefaultSettings = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnSave.Location = new System.Drawing.Point(690, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 79);
            this.btnSave.TabIndex = 10;
            this.btnSave.UseDefaultSettings = false;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBrow
            // 
            this.btnBrow.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrow.BackgroundImage")));
            this.btnBrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrow.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnBrow.Location = new System.Drawing.Point(621, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 9;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnPrint.Location = new System.Drawing.Point(552, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(69, 79);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.UseDefaultSettings = false;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnDelete.Location = new System.Drawing.Point(483, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 79);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.UseDefaultSettings = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.SystemColors.Control;
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModify.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnModify.Location = new System.Drawing.Point(414, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(69, 79);
            this.btnModify.TabIndex = 6;
            this.btnModify.UseDefaultSettings = false;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.BackColor = System.Drawing.SystemColors.Control;
            this.btnDuplicate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDuplicate.BackgroundImage")));
            this.btnDuplicate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDuplicate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDuplicate.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnDuplicate.Location = new System.Drawing.Point(345, 0);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(69, 79);
            this.btnDuplicate.TabIndex = 5;
            this.btnDuplicate.UseDefaultSettings = false;
            this.btnDuplicate.UseVisualStyleBackColor = false;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // btnAppend
            // 
            this.btnAppend.BackColor = System.Drawing.SystemColors.Control;
            this.btnAppend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAppend.BackgroundImage")));
            this.btnAppend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAppend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAppend.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnAppend.Location = new System.Drawing.Point(276, 0);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(69, 79);
            this.btnAppend.TabIndex = 4;
            this.btnAppend.UseDefaultSettings = false;
            this.btnAppend.UseVisualStyleBackColor = false;
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // btnBottom
            // 
            this.btnBottom.BackColor = System.Drawing.SystemColors.Control;
            this.btnBottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBottom.BackgroundImage")));
            this.btnBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBottom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBottom.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnBottom.Location = new System.Drawing.Point(207, 0);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(69, 79);
            this.btnBottom.TabIndex = 3;
            this.btnBottom.UseDefaultSettings = false;
            this.btnBottom.UseVisualStyleBackColor = false;
            this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnNext.Location = new System.Drawing.Point(138, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(69, 79);
            this.btnNext.TabIndex = 2;
            this.btnNext.UseDefaultSettings = false;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrior
            // 
            this.btnPrior.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrior.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrior.BackgroundImage")));
            this.btnPrior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrior.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrior.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnPrior.Location = new System.Drawing.Point(69, 0);
            this.btnPrior.Name = "btnPrior";
            this.btnPrior.Size = new System.Drawing.Size(69, 79);
            this.btnPrior.TabIndex = 1;
            this.btnPrior.UseDefaultSettings = false;
            this.btnPrior.UseVisualStyleBackColor = false;
            this.btnPrior.Click += new System.EventHandler(this.btnPrior_Click);
            // 
            // btnTop
            // 
            this.btnTop.BackColor = System.Drawing.SystemColors.Control;
            this.btnTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTop.BackgroundImage")));
            this.btnTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTop.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnTop.Location = new System.Drawing.Point(0, 0);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(69, 79);
            this.btnTop.TabIndex = 0;
            this.btnTop.UseDefaultSettings = false;
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 622);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(214, 20);
            this.toolStripStatusLabel1.Text = "1.新增 2.修改 3.刪除 4.0.結束";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(740, 20);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatusLabel3.Text = "插入";
            // 
            // gridItDesp
            // 
            this.gridItDesp.BackColor = System.Drawing.SystemColors.Control;
            this.gridItDesp.Enabled = false;
            this.gridItDesp.Font = new System.Drawing.Font("新細明體", 12F);
            this.gridItDesp.Location = new System.Drawing.Point(721, 437);
            this.gridItDesp.Name = "gridItDesp";
            this.gridItDesp.Size = new System.Drawing.Size(144, 31);
            this.gridItDesp.TabIndex = 22;
            this.gridItDesp.Text = "F7:規格說明";
            this.gridItDesp.UseVisualStyleBackColor = false;
            this.gridItDesp.Click += new System.EventHandler(this.gridItDesp_Click);
            // 
            // FrmBom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.gridItDesp);
            this.Controls.Add(this.BoMemo);
            this.Controls.Add(this.lblBoMemo);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.lblBoTotMny);
            this.Controls.Add(this.BoTotMny);
            this.Controls.Add(this.lblBoTotQty);
            this.Controls.Add(this.gridAppend);
            this.Controls.Add(this.BoTotQty);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.gridDelete);
            this.Controls.Add(this.BoItName);
            this.Controls.Add(this.gridPicture);
            this.Controls.Add(this.BoItNo);
            this.Controls.Add(this.gridInsert);
            this.Controls.Add(this.lblBoItName);
            this.Controls.Add(this.gridStockQuery);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gridItBuyPrice);
            this.Controls.Add(this.lblBoItNo);
            this.Name = "FrmBom";
            this.Tag = "組合品組裝品建檔";
            this.Text = "組合品組裝品建檔";
            this.Load += new System.EventHandler(this.FrmBom_Load);
            this.Shown += new System.EventHandler(this.FrmBom_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.panelT1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblBoItNo;
        private JE.MyControl.LabelT lblBoItName;
        private JE.MyControl.ButtonGridT gridPicture;
        private JE.MyControl.ButtonGridT gridInsert;
        private JE.MyControl.ButtonGridT gridStockQuery;
        private JE.MyControl.ButtonGridT gridItBuyPrice;
        private JE.MyControl.LabelT lblBoTotQty;
        private JE.MyControl.LabelT lblBoTotMny;
        private JE.MyControl.LabelT lblBoMemo;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnCancel;
        private JE.MyControl.ButtonT btnSave;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.ButtonT btnPrint;
        private JE.MyControl.ButtonT btnDelete;
        private JE.MyControl.ButtonT btnModify;
        private JE.MyControl.ButtonT btnDuplicate;
        private JE.MyControl.ButtonT btnAppend;
        private JE.MyControl.ButtonT btnBottom;
        private JE.MyControl.ButtonT btnNext;
        private JE.MyControl.ButtonT btnPrior;
        private JE.MyControl.ButtonT btnTop;
        private JE.MyControl.TextBoxNumberT BoTotQty;
        private JE.MyControl.TextBoxNumberT BoTotMny;
        private JE.MyControl.TextBoxT BoMemo;
        public JE.MyControl.TextBoxT BoItNo;
        public JE.MyControl.TextBoxT BoItName;
        public JE.MyControl.DataGridViewT dataGridViewT1;
        public JE.MyControl.ButtonGridT gridAppend;
        public JE.MyControl.ButtonGridT gridDelete;
        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 標準用量;
        private JE.MyControl.DataGridViewTextNumberT 母件比例;
        private JE.MyControl.DataGridViewTextNumberT 單價;
        private JE.MyControl.DataGridViewTextNumberT 折數;
        private JE.MyControl.DataGridViewTextNumberT 金額;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 說明;
        private JE.MyControl.ButtonGridT gridItDesp;
    }
}