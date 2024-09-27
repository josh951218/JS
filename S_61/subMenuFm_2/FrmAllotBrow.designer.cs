namespace S_61.subMenuFm_2
{
    partial class FrmAllotBrow
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
            this.btnQuery = new JE.MyControl.ButtonSmallT();
            this.btnPicture = new JE.MyControl.ButtonSmallT();
            this.btnDesp = new JE.MyControl.ButtonSmallT();
            this.btnBom = new JE.MyControl.ButtonSmallT();
            this.btnStock = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.StNoO = new JE.MyControl.TextBoxT();
            this.EmName = new JE.MyControl.TextBoxT();
            this.EmNo = new JE.MyControl.TextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.StNameO = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.ItNo = new JE.MyControl.TextBoxT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.AlMemo = new JE.MyControl.TextBoxT();
            this.StNameI = new JE.MyControl.TextBoxT();
            this.StNoI = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.StockQty = new JE.MyControl.TextBoxT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.調撥單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.調撥日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.撥入倉庫 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.撥出倉庫 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.調撥數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.產品組成 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.調撥人員 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.備註說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aldate1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aldate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.query2 = new JE.MyControl.ButtonSmallT();
            this.query3 = new JE.MyControl.ButtonSmallT();
            this.query4 = new JE.MyControl.ButtonSmallT();
            this.query5 = new JE.MyControl.ButtonSmallT();
            this.query6 = new JE.MyControl.ButtonSmallT();
            this.query7 = new JE.MyControl.ButtonSmallT();
            this.query8 = new JE.MyControl.ButtonSmallT();
            this.qMemo = new JE.MyControl.TextBoxT();
            this.lblT14 = new JE.MyControl.LabelT();
            this.lblT12 = new JE.MyControl.LabelT();
            this.qUnit = new JE.MyControl.TextBoxT();
            this.qStNoO = new JE.MyControl.TextBoxT();
            this.lblT11 = new JE.MyControl.LabelT();
            this.lblT13 = new JE.MyControl.LabelT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.qDate = new JE.MyControl.TextBoxT();
            this.lblT8 = new JE.MyControl.LabelT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.lblT10 = new JE.MyControl.LabelT();
            this.qAlNo = new JE.MyControl.TextBoxT();
            this.qStNoI = new JE.MyControl.TextBoxT();
            this.qItNo = new JE.MyControl.TextBoxT();
            this.qEmNo = new JE.MyControl.TextBoxT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(12, 582);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(158, 41);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "F3:查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnPicture
            // 
            this.btnPicture.Font = new System.Drawing.Font("細明體", 12F);
            this.btnPicture.Location = new System.Drawing.Point(176, 582);
            this.btnPicture.Name = "btnPicture";
            this.btnPicture.Size = new System.Drawing.Size(158, 41);
            this.btnPicture.TabIndex = 12;
            this.btnPicture.Text = "F4:看圖";
            this.btnPicture.UseVisualStyleBackColor = true;
            this.btnPicture.Click += new System.EventHandler(this.btnPicture_Click);
            // 
            // btnDesp
            // 
            this.btnDesp.Font = new System.Drawing.Font("細明體", 12F);
            this.btnDesp.Location = new System.Drawing.Point(340, 582);
            this.btnDesp.Name = "btnDesp";
            this.btnDesp.Size = new System.Drawing.Size(158, 41);
            this.btnDesp.TabIndex = 13;
            this.btnDesp.Text = "F5:規格說明";
            this.btnDesp.UseVisualStyleBackColor = true;
            this.btnDesp.Click += new System.EventHandler(this.btnDesp_Click);
            // 
            // btnBom
            // 
            this.btnBom.Font = new System.Drawing.Font("細明體", 12F);
            this.btnBom.Location = new System.Drawing.Point(504, 582);
            this.btnBom.Name = "btnBom";
            this.btnBom.Size = new System.Drawing.Size(158, 41);
            this.btnBom.TabIndex = 14;
            this.btnBom.Text = "F6:組件明細";
            this.btnBom.UseVisualStyleBackColor = true;
            this.btnBom.Click += new System.EventHandler(this.btnBom_Click);
            // 
            // btnStock
            // 
            this.btnStock.Font = new System.Drawing.Font("細明體", 12F);
            this.btnStock.Location = new System.Drawing.Point(668, 582);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(158, 41);
            this.btnStock.TabIndex = 15;
            this.btnStock.Text = "F8:庫存查詢";
            this.btnStock.UseVisualStyleBackColor = true;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(832, 582);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(161, 41);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "F9:返回";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(723, 14);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "撥出倉庫";
            // 
            // StNoO
            // 
            this.StNoO.AllowGrayBackColor = true;
            this.StNoO.AllowResize = true;
            this.StNoO.BackColor = System.Drawing.Color.Silver;
            this.StNoO.Font = new System.Drawing.Font("細明體", 12F);
            this.StNoO.Location = new System.Drawing.Point(801, 9);
            this.StNoO.MaxLength = 10;
            this.StNoO.Name = "StNoO";
            this.StNoO.oLen = 0;
            this.StNoO.ReadOnly = true;
            this.StNoO.Size = new System.Drawing.Size(87, 27);
            this.StNoO.TabIndex = 6;
            this.StNoO.TabStop = false;
            // 
            // EmName
            // 
            this.EmName.AllowGrayBackColor = true;
            this.EmName.AllowResize = true;
            this.EmName.BackColor = System.Drawing.Color.Silver;
            this.EmName.Font = new System.Drawing.Font("細明體", 12F);
            this.EmName.Location = new System.Drawing.Point(144, 42);
            this.EmName.MaxLength = 10;
            this.EmName.Name = "EmName";
            this.EmName.oLen = 0;
            this.EmName.ReadOnly = true;
            this.EmName.Size = new System.Drawing.Size(87, 27);
            this.EmName.TabIndex = 3;
            this.EmName.TabStop = false;
            // 
            // EmNo
            // 
            this.EmNo.AllowGrayBackColor = true;
            this.EmNo.AllowResize = true;
            this.EmNo.BackColor = System.Drawing.Color.Silver;
            this.EmNo.Font = new System.Drawing.Font("細明體", 12F);
            this.EmNo.Location = new System.Drawing.Point(99, 42);
            this.EmNo.MaxLength = 4;
            this.EmNo.Name = "EmNo";
            this.EmNo.oLen = 0;
            this.EmNo.ReadOnly = true;
            this.EmNo.Size = new System.Drawing.Size(39, 27);
            this.EmNo.TabIndex = 2;
            this.EmNo.TabStop = false;
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(22, 47);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "調撥人員";
            // 
            // StNameO
            // 
            this.StNameO.AllowGrayBackColor = true;
            this.StNameO.AllowResize = true;
            this.StNameO.BackColor = System.Drawing.Color.Silver;
            this.StNameO.Font = new System.Drawing.Font("細明體", 12F);
            this.StNameO.Location = new System.Drawing.Point(894, 9);
            this.StNameO.MaxLength = 10;
            this.StNameO.Name = "StNameO";
            this.StNameO.oLen = 0;
            this.StNameO.ReadOnly = true;
            this.StNameO.Size = new System.Drawing.Size(87, 27);
            this.StNameO.TabIndex = 7;
            this.StNameO.TabStop = false;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(22, 14);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "產品編號";
            // 
            // ItNo
            // 
            this.ItNo.AllowGrayBackColor = true;
            this.ItNo.AllowResize = true;
            this.ItNo.BackColor = System.Drawing.Color.Silver;
            this.ItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo.Location = new System.Drawing.Point(100, 9);
            this.ItNo.MaxLength = 20;
            this.ItNo.Name = "ItNo";
            this.ItNo.oLen = 0;
            this.ItNo.ReadOnly = true;
            this.ItNo.Size = new System.Drawing.Size(167, 27);
            this.ItNo.TabIndex = 3;
            this.ItNo.TabStop = false;
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(22, 80);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(72, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "備    註";
            // 
            // AlMemo
            // 
            this.AlMemo.AllowGrayBackColor = false;
            this.AlMemo.AllowResize = true;
            this.AlMemo.Font = new System.Drawing.Font("細明體", 12F);
            this.AlMemo.Location = new System.Drawing.Point(99, 75);
            this.AlMemo.MaxLength = 60;
            this.AlMemo.Name = "AlMemo";
            this.AlMemo.oLen = 0;
            this.AlMemo.Size = new System.Drawing.Size(487, 27);
            this.AlMemo.TabIndex = 21;
            this.AlMemo.Validating += new System.ComponentModel.CancelEventHandler(this.AlMemo_Validating);
            // 
            // StNameI
            // 
            this.StNameI.AllowGrayBackColor = true;
            this.StNameI.AllowResize = true;
            this.StNameI.BackColor = System.Drawing.Color.Silver;
            this.StNameI.Font = new System.Drawing.Font("細明體", 12F);
            this.StNameI.Location = new System.Drawing.Point(518, 9);
            this.StNameI.MaxLength = 10;
            this.StNameI.Name = "StNameI";
            this.StNameI.oLen = 0;
            this.StNameI.ReadOnly = true;
            this.StNameI.Size = new System.Drawing.Size(87, 27);
            this.StNameI.TabIndex = 5;
            this.StNameI.TabStop = false;
            // 
            // StNoI
            // 
            this.StNoI.AllowGrayBackColor = true;
            this.StNoI.AllowResize = true;
            this.StNoI.BackColor = System.Drawing.Color.Silver;
            this.StNoI.Font = new System.Drawing.Font("細明體", 12F);
            this.StNoI.Location = new System.Drawing.Point(425, 9);
            this.StNoI.MaxLength = 10;
            this.StNoI.Name = "StNoI";
            this.StNoI.oLen = 0;
            this.StNoI.ReadOnly = true;
            this.StNoI.Size = new System.Drawing.Size(87, 27);
            this.StNoI.TabIndex = 4;
            this.StNoI.TabStop = false;
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(348, 14);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "撥入倉庫";
            // 
            // StockQty
            // 
            this.StockQty.AllowGrayBackColor = true;
            this.StockQty.AllowResize = true;
            this.StockQty.BackColor = System.Drawing.Color.Silver;
            this.StockQty.Font = new System.Drawing.Font("細明體", 12F);
            this.StockQty.Location = new System.Drawing.Point(801, 42);
            this.StockQty.MaxLength = 20;
            this.StockQty.Name = "StockQty";
            this.StockQty.oLen = 0;
            this.StockQty.ReadOnly = true;
            this.StockQty.Size = new System.Drawing.Size(167, 27);
            this.StockQty.TabIndex = 4;
            this.StockQty.TabStop = false;
            this.StockQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(723, 47);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(72, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "總庫存量";
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
            this.調撥單號,
            this.調撥日期,
            this.撥入倉庫,
            this.撥出倉庫,
            this.品名規格,
            this.單位,
            this.調撥數量,
            this.包裝數量,
            this.產品組成,
            this.調撥人員,
            this.備註說明,
            this.序號,
            this.產品編號,
            this.aldate1,
            this.aldate});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 108);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1009, 362);
            this.dataGridViewT1.TabIndex = 22;
            this.dataGridViewT1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewT1_CellValidating);
            this.dataGridViewT1.SelectionChanged += new System.EventHandler(this.dataGridViewT1_SelectionChanged);
            this.dataGridViewT1.DoubleClick += new System.EventHandler(this.dataGridViewT1_DoubleClick);
            // 
            // 調撥單號
            // 
            this.調撥單號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.調撥單號.DataPropertyName = "alno";
            this.調撥單號.HeaderText = "調撥單號";
            this.調撥單號.MaxInputLength = 16;
            this.調撥單號.Name = "調撥單號";
            this.調撥單號.ReadOnly = true;
            this.調撥單號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.調撥單號.Width = 141;
            // 
            // 調撥日期
            // 
            this.調撥日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.調撥日期.DataPropertyName = "調撥日期";
            this.調撥日期.HeaderText = "調撥日期";
            this.調撥日期.MaxInputLength = 10;
            this.調撥日期.Name = "調撥日期";
            this.調撥日期.ReadOnly = true;
            this.調撥日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.調撥日期.Width = 93;
            // 
            // 撥入倉庫
            // 
            this.撥入倉庫.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.撥入倉庫.DataPropertyName = "stnoi";
            this.撥入倉庫.HeaderText = "撥入倉庫";
            this.撥入倉庫.MaxInputLength = 10;
            this.撥入倉庫.Name = "撥入倉庫";
            this.撥入倉庫.ReadOnly = true;
            this.撥入倉庫.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.撥入倉庫.Width = 93;
            // 
            // 撥出倉庫
            // 
            this.撥出倉庫.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.撥出倉庫.DataPropertyName = "stnoo";
            this.撥出倉庫.HeaderText = "撥出倉庫";
            this.撥出倉庫.MaxInputLength = 10;
            this.撥出倉庫.Name = "撥出倉庫";
            this.撥出倉庫.ReadOnly = true;
            this.撥出倉庫.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.撥出倉庫.Width = 93;
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
            // 調撥數量
            // 
            this.調撥數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.調撥數量.DataPropertyName = "qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.調撥數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.調撥數量.FirstNum = 0;
            this.調撥數量.HeaderText = "調撥數量";
            this.調撥數量.LastNum = 0;
            this.調撥數量.MarkThousand = false;
            this.調撥數量.MaxInputLength = 11;
            this.調撥數量.Name = "調撥數量";
            this.調撥數量.NullInput = false;
            this.調撥數量.NullValue = "0";
            this.調撥數量.ReadOnly = true;
            this.調撥數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.調撥數量.Width = 101;
            // 
            // 包裝數量
            // 
            this.包裝數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量.DataPropertyName = "itpkgqty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝數量.DefaultCellStyle = dataGridViewCellStyle3;
            this.包裝數量.FirstNum = 0;
            this.包裝數量.HeaderText = "包裝數量";
            this.包裝數量.LastNum = 0;
            this.包裝數量.MarkThousand = false;
            this.包裝數量.MaxInputLength = 11;
            this.包裝數量.Name = "包裝數量";
            this.包裝數量.NullInput = false;
            this.包裝數量.NullValue = "0";
            this.包裝數量.ReadOnly = true;
            this.包裝數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝數量.Width = 101;
            // 
            // 產品組成
            // 
            this.產品組成.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品組成.DataPropertyName = "產品組成";
            this.產品組成.HeaderText = "產品組成";
            this.產品組成.MaxInputLength = 10;
            this.產品組成.Name = "產品組成";
            this.產品組成.ReadOnly = true;
            this.產品組成.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.產品組成.Width = 93;
            // 
            // 調撥人員
            // 
            this.調撥人員.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.調撥人員.DataPropertyName = "emno";
            this.調撥人員.HeaderText = "調撥人員";
            this.調撥人員.MaxInputLength = 10;
            this.調撥人員.MinimumWidth = 95;
            this.調撥人員.Name = "調撥人員";
            this.調撥人員.ReadOnly = true;
            this.調撥人員.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.調撥人員.Width = 95;
            // 
            // 備註說明
            // 
            this.備註說明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.備註說明.DataPropertyName = "memo";
            this.備註說明.HeaderText = "備註說明";
            this.備註說明.MaxInputLength = 20;
            this.備註說明.Name = "備註說明";
            this.備註說明.ReadOnly = true;
            this.備註說明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.備註說明.Width = 173;
            // 
            // 序號
            // 
            this.序號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.序號.DataPropertyName = "序號";
            this.序號.HeaderText = "序號";
            this.序號.MaxInputLength = 4;
            this.序號.Name = "序號";
            this.序號.ReadOnly = true;
            this.序號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.序號.Visible = false;
            this.序號.Width = 45;
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
            // aldate1
            // 
            this.aldate1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.aldate1.DataPropertyName = "aldate1";
            this.aldate1.HeaderText = "aldate1";
            this.aldate1.MaxInputLength = 10;
            this.aldate1.Name = "aldate1";
            this.aldate1.ReadOnly = true;
            this.aldate1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.aldate1.Visible = false;
            this.aldate1.Width = 93;
            // 
            // aldate
            // 
            this.aldate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.aldate.DataPropertyName = "aldate";
            this.aldate.HeaderText = "aldate";
            this.aldate.MaxInputLength = 10;
            this.aldate.Name = "aldate";
            this.aldate.ReadOnly = true;
            this.aldate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.aldate.Visible = false;
            this.aldate.Width = 93;
            // 
            // query2
            // 
            this.query2.Font = new System.Drawing.Font("細明體", 12F);
            this.query2.Location = new System.Drawing.Point(5, 474);
            this.query2.Name = "query2";
            this.query2.Size = new System.Drawing.Size(134, 37);
            this.query2.TabIndex = 23;
            this.query2.Text = "f2:調撥單號";
            this.query2.UseVisualStyleBackColor = true;
            this.query2.Click += new System.EventHandler(this.query2_Click);
            // 
            // query3
            // 
            this.query3.Font = new System.Drawing.Font("細明體", 12F);
            this.query3.Location = new System.Drawing.Point(139, 474);
            this.query3.Name = "query3";
            this.query3.Size = new System.Drawing.Size(134, 37);
            this.query3.TabIndex = 24;
            this.query3.Text = "f3:調撥日期";
            this.query3.UseVisualStyleBackColor = true;
            this.query3.Click += new System.EventHandler(this.query3_Click);
            // 
            // query4
            // 
            this.query4.Font = new System.Drawing.Font("細明體", 12F);
            this.query4.Location = new System.Drawing.Point(273, 474);
            this.query4.Name = "query4";
            this.query4.Size = new System.Drawing.Size(134, 37);
            this.query4.TabIndex = 25;
            this.query4.Text = "f4:產品";
            this.query4.UseVisualStyleBackColor = true;
            this.query4.Click += new System.EventHandler(this.query4_Click);
            // 
            // query5
            // 
            this.query5.Font = new System.Drawing.Font("細明體", 12F);
            this.query5.Location = new System.Drawing.Point(407, 474);
            this.query5.Name = "query5";
            this.query5.Size = new System.Drawing.Size(154, 37);
            this.query5.TabIndex = 26;
            this.query5.Text = "f5:撥出倉庫+產品";
            this.query5.UseVisualStyleBackColor = true;
            this.query5.Click += new System.EventHandler(this.query5_Click);
            // 
            // query6
            // 
            this.query6.Font = new System.Drawing.Font("細明體", 12F);
            this.query6.Location = new System.Drawing.Point(561, 474);
            this.query6.Name = "query6";
            this.query6.Size = new System.Drawing.Size(154, 37);
            this.query6.TabIndex = 27;
            this.query6.Text = "f6:撥入倉庫+產品";
            this.query6.UseVisualStyleBackColor = true;
            this.query6.Click += new System.EventHandler(this.query6_Click);
            // 
            // query7
            // 
            this.query7.Font = new System.Drawing.Font("細明體", 12F);
            this.query7.Location = new System.Drawing.Point(715, 474);
            this.query7.Name = "query7";
            this.query7.Size = new System.Drawing.Size(154, 37);
            this.query7.TabIndex = 28;
            this.query7.Text = "f7:調撥人員+日期";
            this.query7.UseVisualStyleBackColor = true;
            this.query7.Click += new System.EventHandler(this.query7_Click);
            // 
            // query8
            // 
            this.query8.Font = new System.Drawing.Font("細明體", 12F);
            this.query8.Location = new System.Drawing.Point(869, 474);
            this.query8.Name = "query8";
            this.query8.Size = new System.Drawing.Size(137, 37);
            this.query8.TabIndex = 29;
            this.query8.Text = "f8:備註說明";
            this.query8.UseVisualStyleBackColor = true;
            this.query8.Click += new System.EventHandler(this.query8_Click);
            // 
            // qMemo
            // 
            this.qMemo.AllowGrayBackColor = false;
            this.qMemo.AllowResize = true;
            this.qMemo.Font = new System.Drawing.Font("細明體", 12F);
            this.qMemo.Location = new System.Drawing.Point(568, 550);
            this.qMemo.MaxLength = 20;
            this.qMemo.Name = "qMemo";
            this.qMemo.oLen = 0;
            this.qMemo.Size = new System.Drawing.Size(167, 27);
            this.qMemo.TabIndex = 6;
            this.qMemo.DoubleClick += new System.EventHandler(this.qMemo_DoubleClick);
            // 
            // lblT14
            // 
            this.lblT14.AutoSize = true;
            this.lblT14.BackColor = System.Drawing.Color.Transparent;
            this.lblT14.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT14.Location = new System.Drawing.Point(478, 555);
            this.lblT14.Name = "lblT14";
            this.lblT14.Size = new System.Drawing.Size(72, 16);
            this.lblT14.TabIndex = 0;
            this.lblT14.Text = "備註說明";
            // 
            // lblT12
            // 
            this.lblT12.AutoSize = true;
            this.lblT12.BackColor = System.Drawing.Color.Transparent;
            this.lblT12.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT12.Location = new System.Drawing.Point(283, 555);
            this.lblT12.Name = "lblT12";
            this.lblT12.Size = new System.Drawing.Size(72, 16);
            this.lblT12.TabIndex = 0;
            this.lblT12.Text = "撥出倉庫";
            // 
            // qUnit
            // 
            this.qUnit.AllowGrayBackColor = false;
            this.qUnit.AllowResize = true;
            this.qUnit.Font = new System.Drawing.Font("細明體", 12F);
            this.qUnit.Location = new System.Drawing.Point(843, 550);
            this.qUnit.MaxLength = 4;
            this.qUnit.Name = "qUnit";
            this.qUnit.oLen = 0;
            this.qUnit.Size = new System.Drawing.Size(39, 27);
            this.qUnit.TabIndex = 8;
            // 
            // qStNoO
            // 
            this.qStNoO.AllowGrayBackColor = false;
            this.qStNoO.AllowResize = true;
            this.qStNoO.Font = new System.Drawing.Font("細明體", 12F);
            this.qStNoO.Location = new System.Drawing.Point(373, 550);
            this.qStNoO.MaxLength = 10;
            this.qStNoO.Name = "qStNoO";
            this.qStNoO.oLen = 0;
            this.qStNoO.Size = new System.Drawing.Size(87, 27);
            this.qStNoO.TabIndex = 4;
            this.qStNoO.DoubleClick += new System.EventHandler(this.qStNoI_DoubleClick);
            // 
            // lblT11
            // 
            this.lblT11.AutoSize = true;
            this.lblT11.BackColor = System.Drawing.Color.Transparent;
            this.lblT11.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT11.Location = new System.Drawing.Point(40, 555);
            this.lblT11.Name = "lblT11";
            this.lblT11.Size = new System.Drawing.Size(72, 16);
            this.lblT11.TabIndex = 0;
            this.lblT11.Text = "調撥日期";
            // 
            // lblT13
            // 
            this.lblT13.AutoSize = true;
            this.lblT13.BackColor = System.Drawing.Color.Transparent;
            this.lblT13.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT13.Location = new System.Drawing.Point(753, 555);
            this.lblT13.Name = "lblT13";
            this.lblT13.Size = new System.Drawing.Size(72, 16);
            this.lblT13.TabIndex = 0;
            this.lblT13.Text = "單    位";
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(40, 522);
            this.lblT7.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(72, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "調撥單號";
            // 
            // qDate
            // 
            this.qDate.AllowGrayBackColor = false;
            this.qDate.AllowResize = true;
            this.qDate.Font = new System.Drawing.Font("細明體", 12F);
            this.qDate.Location = new System.Drawing.Point(130, 550);
            this.qDate.MaxLength = 8;
            this.qDate.Name = "qDate";
            this.qDate.oLen = 0;
            this.qDate.Size = new System.Drawing.Size(71, 27);
            this.qDate.TabIndex = 2;
            this.qDate.Validating += new System.ComponentModel.CancelEventHandler(this.qDate_Validating);
            // 
            // lblT8
            // 
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(283, 522);
            this.lblT8.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(72, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "撥入倉庫";
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(478, 522);
            this.lblT9.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(72, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "產    品";
            // 
            // lblT10
            // 
            this.lblT10.AutoSize = true;
            this.lblT10.BackColor = System.Drawing.Color.Transparent;
            this.lblT10.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT10.Location = new System.Drawing.Point(753, 522);
            this.lblT10.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT10.Name = "lblT10";
            this.lblT10.Size = new System.Drawing.Size(72, 16);
            this.lblT10.TabIndex = 0;
            this.lblT10.Text = "調撥人員";
            // 
            // qAlNo
            // 
            this.qAlNo.AllowGrayBackColor = false;
            this.qAlNo.AllowResize = true;
            this.qAlNo.Font = new System.Drawing.Font("細明體", 12F);
            this.qAlNo.Location = new System.Drawing.Point(130, 517);
            this.qAlNo.MaxLength = 16;
            this.qAlNo.Name = "qAlNo";
            this.qAlNo.oLen = 0;
            this.qAlNo.Size = new System.Drawing.Size(135, 27);
            this.qAlNo.TabIndex = 1;
            // 
            // qStNoI
            // 
            this.qStNoI.AllowGrayBackColor = false;
            this.qStNoI.AllowResize = true;
            this.qStNoI.Font = new System.Drawing.Font("細明體", 12F);
            this.qStNoI.Location = new System.Drawing.Point(373, 517);
            this.qStNoI.MaxLength = 10;
            this.qStNoI.Name = "qStNoI";
            this.qStNoI.oLen = 0;
            this.qStNoI.Size = new System.Drawing.Size(87, 27);
            this.qStNoI.TabIndex = 3;
            this.qStNoI.DoubleClick += new System.EventHandler(this.qStNoI_DoubleClick);
            // 
            // qItNo
            // 
            this.qItNo.AllowGrayBackColor = false;
            this.qItNo.AllowResize = true;
            this.qItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.qItNo.Location = new System.Drawing.Point(568, 517);
            this.qItNo.MaxLength = 20;
            this.qItNo.Name = "qItNo";
            this.qItNo.oLen = 0;
            this.qItNo.Size = new System.Drawing.Size(167, 27);
            this.qItNo.TabIndex = 5;
            this.qItNo.DoubleClick += new System.EventHandler(this.qItNo_DoubleClick);
            // 
            // qEmNo
            // 
            this.qEmNo.AllowGrayBackColor = false;
            this.qEmNo.AllowResize = true;
            this.qEmNo.Font = new System.Drawing.Font("細明體", 12F);
            this.qEmNo.Location = new System.Drawing.Point(843, 517);
            this.qEmNo.MaxLength = 10;
            this.qEmNo.Name = "qEmNo";
            this.qEmNo.oLen = 0;
            this.qEmNo.Size = new System.Drawing.Size(87, 27);
            this.qEmNo.TabIndex = 7;
            this.qEmNo.DoubleClick += new System.EventHandler(this.qEmNo_DoubleClick);
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
            // FrmAllotBrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.query2);
            this.Controls.Add(this.qMemo);
            this.Controls.Add(this.query3);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.query4);
            this.Controls.Add(this.lblT14);
            this.Controls.Add(this.query5);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.query6);
            this.Controls.Add(this.query7);
            this.Controls.Add(this.lblT12);
            this.Controls.Add(this.query8);
            this.Controls.Add(this.btnPicture);
            this.Controls.Add(this.qUnit);
            this.Controls.Add(this.EmName);
            this.Controls.Add(this.qStNoO);
            this.Controls.Add(this.lblT11);
            this.Controls.Add(this.btnDesp);
            this.Controls.Add(this.lblT13);
            this.Controls.Add(this.AlMemo);
            this.Controls.Add(this.qDate);
            this.Controls.Add(this.btnBom);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.lblT8);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblT9);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.lblT10);
            this.Controls.Add(this.EmNo);
            this.Controls.Add(this.qAlNo);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.qStNoI);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.qItNo);
            this.Controls.Add(this.qEmNo);
            this.Controls.Add(this.StNoO);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.StockQty);
            this.Controls.Add(this.StNoI);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.StNameO);
            this.Controls.Add(this.StNameI);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.ItNo);
            this.Name = "FrmAllotBrow";
            this.Text = "倉庫調撥作業系統瀏覽";
            this.Load += new System.EventHandler(this.FrmAllotBrow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.ButtonSmallT btnQuery;
        private JE.MyControl.ButtonSmallT btnPicture;
        private JE.MyControl.ButtonSmallT btnDesp;
        private JE.MyControl.ButtonSmallT btnBom;
        private JE.MyControl.ButtonSmallT btnStock;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT StNoO;
        private JE.MyControl.TextBoxT EmName;
        private JE.MyControl.TextBoxT EmNo;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT StNameO;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.TextBoxT ItNo;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.TextBoxT AlMemo;
        private JE.MyControl.TextBoxT StNameI;
        private JE.MyControl.TextBoxT StNoI;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT StockQty;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT query2;
        private JE.MyControl.ButtonSmallT query3;
        private JE.MyControl.ButtonSmallT query4;
        private JE.MyControl.ButtonSmallT query5;
        private JE.MyControl.ButtonSmallT query6;
        private JE.MyControl.ButtonSmallT query7;
        private JE.MyControl.ButtonSmallT query8;
        private JE.MyControl.TextBoxT qMemo;
        private JE.MyControl.LabelT lblT14;
        private JE.MyControl.LabelT lblT12;
        private JE.MyControl.TextBoxT qUnit;
        private JE.MyControl.TextBoxT qStNoO;
        private JE.MyControl.LabelT lblT11;
        private JE.MyControl.LabelT lblT13;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.TextBoxT qDate;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.LabelT lblT10;
        private JE.MyControl.TextBoxT qAlNo;
        private JE.MyControl.TextBoxT qStNoI;
        private JE.MyControl.TextBoxT qItNo;
        private JE.MyControl.TextBoxT qEmNo;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 調撥單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 調撥日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 撥入倉庫;
        private System.Windows.Forms.DataGridViewTextBoxColumn 撥出倉庫;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 調撥數量;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品組成;
        private System.Windows.Forms.DataGridViewTextBoxColumn 調撥人員;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註說明;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn aldate1;
        private System.Windows.Forms.DataGridViewTextBoxColumn aldate;
    }
}