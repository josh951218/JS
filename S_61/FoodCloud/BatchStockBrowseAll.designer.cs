namespace S_61.FoodCloud
{
    partial class BatchStockBrowseAll
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchStockBrowseAll));
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.批次編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.庫存倉數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.借出倉數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.加工倉數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.借入倉數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.庫存總數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.廠商編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.廠商名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.製造日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.有效日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnExcel = new JE.MyControl.ButtonT();
            this.btnWord = new JE.MyControl.ButtonT();
            this.btnPreView = new JE.MyControl.ButtonT();
            this.btnPrint = new JE.MyControl.ButtonT();
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.rd1 = new JE.MyControl.RadioT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.orderbybatchno = new JE.MyControl.ButtonSmallT();
            this.orderbyitno = new JE.MyControl.ButtonSmallT();
            this.groupBoxT3 = new JE.MyControl.GroupBoxT();
            this.rd11 = new JE.MyControl.RadioT();
            this.rd10 = new JE.MyControl.RadioT();
            this.rd9 = new JE.MyControl.RadioT();
            this.rd8 = new JE.MyControl.RadioT();
            this.rd7 = new JE.MyControl.RadioT();
            this.rd6 = new JE.MyControl.RadioT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.panelT1.SuspendLayout();
            this.groupBoxT1.SuspendLayout();
            this.groupBoxT3.SuspendLayout();
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
            this.批次編號,
            this.產品編號,
            this.品名規格,
            this.單位,
            this.庫存倉數量,
            this.借出倉數量,
            this.加工倉數量,
            this.借入倉數量,
            this.庫存總數量,
            this.廠商編號,
            this.廠商名稱,
            this.製造日期,
            this.有效日期});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(1, 1);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(997, 420);
            this.dataGridViewT1.TabIndex = 1;
            // 
            // 批次編號
            // 
            this.批次編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.批次編號.DataPropertyName = "Batchno";
            this.批次編號.HeaderText = "批次編號";
            this.批次編號.Name = "批次編號";
            this.批次編號.ReadOnly = true;
            this.批次編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.批次編號.Width = 173;
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
            // 庫存倉數量
            // 
            this.庫存倉數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.庫存倉數量.DataPropertyName = "庫存倉";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.庫存倉數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.庫存倉數量.FirstNum = 0;
            this.庫存倉數量.HeaderText = "庫存倉數量";
            this.庫存倉數量.LastNum = 0;
            this.庫存倉數量.MarkThousand = false;
            this.庫存倉數量.MaxInputLength = 13;
            this.庫存倉數量.Name = "庫存倉數量";
            this.庫存倉數量.NullInput = false;
            this.庫存倉數量.NullValue = "0";
            this.庫存倉數量.ReadOnly = true;
            this.庫存倉數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.庫存倉數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.庫存倉數量.Width = 117;
            // 
            // 借出倉數量
            // 
            this.借出倉數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.借出倉數量.DataPropertyName = "借出倉";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.借出倉數量.DefaultCellStyle = dataGridViewCellStyle3;
            this.借出倉數量.FirstNum = 0;
            this.借出倉數量.HeaderText = "借出倉數量";
            this.借出倉數量.LastNum = 0;
            this.借出倉數量.MarkThousand = false;
            this.借出倉數量.MaxInputLength = 13;
            this.借出倉數量.Name = "借出倉數量";
            this.借出倉數量.NullInput = false;
            this.借出倉數量.NullValue = "0";
            this.借出倉數量.ReadOnly = true;
            this.借出倉數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.借出倉數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.借出倉數量.Width = 117;
            // 
            // 加工倉數量
            // 
            this.加工倉數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.加工倉數量.DataPropertyName = "加工倉";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.加工倉數量.DefaultCellStyle = dataGridViewCellStyle4;
            this.加工倉數量.FirstNum = 0;
            this.加工倉數量.HeaderText = "加工倉數量";
            this.加工倉數量.LastNum = 0;
            this.加工倉數量.MarkThousand = false;
            this.加工倉數量.MaxInputLength = 13;
            this.加工倉數量.Name = "加工倉數量";
            this.加工倉數量.NullInput = false;
            this.加工倉數量.NullValue = "0";
            this.加工倉數量.ReadOnly = true;
            this.加工倉數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.加工倉數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.加工倉數量.Width = 117;
            // 
            // 借入倉數量
            // 
            this.借入倉數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.借入倉數量.DataPropertyName = "借入倉";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.借入倉數量.DefaultCellStyle = dataGridViewCellStyle5;
            this.借入倉數量.FirstNum = 0;
            this.借入倉數量.HeaderText = "借入倉數量";
            this.借入倉數量.LastNum = 0;
            this.借入倉數量.MarkThousand = false;
            this.借入倉數量.MaxInputLength = 13;
            this.借入倉數量.Name = "借入倉數量";
            this.借入倉數量.NullInput = false;
            this.借入倉數量.NullValue = "0";
            this.借入倉數量.ReadOnly = true;
            this.借入倉數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.借入倉數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.借入倉數量.Width = 117;
            // 
            // 庫存總數量
            // 
            this.庫存總數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.庫存總數量.DataPropertyName = "StnoQty";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.庫存總數量.DefaultCellStyle = dataGridViewCellStyle6;
            this.庫存總數量.FirstNum = 0;
            this.庫存總數量.HeaderText = "庫存總數量";
            this.庫存總數量.LastNum = 0;
            this.庫存總數量.MarkThousand = false;
            this.庫存總數量.MaxInputLength = 13;
            this.庫存總數量.Name = "庫存總數量";
            this.庫存總數量.NullInput = false;
            this.庫存總數量.NullValue = "0";
            this.庫存總數量.ReadOnly = true;
            this.庫存總數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.庫存總數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.庫存總數量.Width = 117;
            // 
            // 廠商編號
            // 
            this.廠商編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.廠商編號.DataPropertyName = "fano";
            this.廠商編號.HeaderText = "廠商編號";
            this.廠商編號.Name = "廠商編號";
            this.廠商編號.ReadOnly = true;
            this.廠商編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.廠商編號.Width = 173;
            // 
            // 廠商名稱
            // 
            this.廠商名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.廠商名稱.DataPropertyName = "faname1";
            this.廠商名稱.HeaderText = "廠商名稱";
            this.廠商名稱.Name = "廠商名稱";
            this.廠商名稱.ReadOnly = true;
            this.廠商名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.廠商名稱.Width = 173;
            // 
            // 製造日期
            // 
            this.製造日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.製造日期.DataPropertyName = "Date";
            this.製造日期.HeaderText = "製造日期";
            this.製造日期.Name = "製造日期";
            this.製造日期.ReadOnly = true;
            this.製造日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.製造日期.Width = 173;
            // 
            // 有效日期
            // 
            this.有效日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.有效日期.DataPropertyName = "Date1";
            this.有效日期.HeaderText = "有效日期";
            this.有效日期.Name = "有效日期";
            this.有效日期.ReadOnly = true;
            this.有效日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.有效日期.Width = 173;
            // 
            // panelT1
            // 
            this.panelT1.AutoSize = true;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnExcel);
            this.panelT1.Controls.Add(this.btnWord);
            this.panelT1.Controls.Add(this.btnPreView);
            this.panelT1.Controls.Add(this.btnPrint);
            this.panelT1.Location = new System.Drawing.Point(328, 554);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(355, 82);
            this.panelT1.TabIndex = 24;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 5F);
            this.btnExit.Location = new System.Drawing.Point(276, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 4;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.SystemColors.Control;
            this.btnExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExcel.BackgroundImage")));
            this.btnExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("細明體", 5F);
            this.btnExcel.Location = new System.Drawing.Point(207, 0);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(69, 79);
            this.btnExcel.TabIndex = 3;
            this.btnExcel.UseDefaultSettings = false;
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnWord
            // 
            this.btnWord.BackColor = System.Drawing.SystemColors.Control;
            this.btnWord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWord.BackgroundImage")));
            this.btnWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWord.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWord.Font = new System.Drawing.Font("細明體", 5F);
            this.btnWord.Location = new System.Drawing.Point(138, 0);
            this.btnWord.Name = "btnWord";
            this.btnWord.Size = new System.Drawing.Size(69, 79);
            this.btnWord.TabIndex = 2;
            this.btnWord.UseDefaultSettings = false;
            this.btnWord.UseVisualStyleBackColor = false;
            this.btnWord.Click += new System.EventHandler(this.btnWord_Click);
            // 
            // btnPreView
            // 
            this.btnPreView.BackColor = System.Drawing.SystemColors.Control;
            this.btnPreView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreView.BackgroundImage")));
            this.btnPreView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPreView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPreView.Font = new System.Drawing.Font("細明體", 5F);
            this.btnPreView.Location = new System.Drawing.Point(69, 0);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(69, 79);
            this.btnPreView.TabIndex = 1;
            this.btnPreView.UseDefaultSettings = false;
            this.btnPreView.UseVisualStyleBackColor = false;
            this.btnPreView.Click += new System.EventHandler(this.btnPreView_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Font = new System.Drawing.Font("細明體", 5F);
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(69, 79);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.UseDefaultSettings = false;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            this.btnPrint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPrint_MouseDown);
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radioT1);
            this.groupBoxT1.Controls.Add(this.rd1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(1, 465);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(387, 84);
            this.groupBoxT1.TabIndex = 23;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.Transparent;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(256, 39);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(74, 20);
            this.radioT1.TabIndex = 1;
            this.radioT1.Tag = "自定一";
            this.radioT1.Text = "自定一";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // rd1
            // 
            this.rd1.AutoSize = true;
            this.rd1.BackColor = System.Drawing.Color.LightBlue;
            this.rd1.Checked = true;
            this.rd1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd1.Font = new System.Drawing.Font("細明體", 12F);
            this.rd1.Location = new System.Drawing.Point(41, 39);
            this.rd1.Name = "rd1";
            this.rd1.Size = new System.Drawing.Size(90, 20);
            this.rd1.TabIndex = 0;
            this.rd1.Tag = "內定報表";
            this.rd1.Text = "內定報表";
            this.rd1.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Location = new System.Drawing.Point(0, 625);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // orderbybatchno
            // 
            this.orderbybatchno.Font = new System.Drawing.Font("細明體", 12F);
            this.orderbybatchno.Location = new System.Drawing.Point(499, 427);
            this.orderbybatchno.Name = "orderbybatchno";
            this.orderbybatchno.Size = new System.Drawing.Size(499, 37);
            this.orderbybatchno.TabIndex = 26;
            this.orderbybatchno.Text = "批次編號排序";
            this.orderbybatchno.UseVisualStyleBackColor = true;
            this.orderbybatchno.Click += new System.EventHandler(this.orderbybatchno_Click);
            // 
            // orderbyitno
            // 
            this.orderbyitno.Font = new System.Drawing.Font("細明體", 12F);
            this.orderbyitno.Location = new System.Drawing.Point(1, 427);
            this.orderbyitno.Name = "orderbyitno";
            this.orderbyitno.Size = new System.Drawing.Size(499, 37);
            this.orderbyitno.TabIndex = 25;
            this.orderbyitno.Text = "產品編號排序";
            this.orderbyitno.UseVisualStyleBackColor = true;
            this.orderbyitno.Click += new System.EventHandler(this.orderbyitno_Click);
            // 
            // groupBoxT3
            // 
            this.groupBoxT3.Controls.Add(this.rd11);
            this.groupBoxT3.Controls.Add(this.rd10);
            this.groupBoxT3.Controls.Add(this.rd9);
            this.groupBoxT3.Controls.Add(this.rd8);
            this.groupBoxT3.Controls.Add(this.rd7);
            this.groupBoxT3.Controls.Add(this.rd6);
            this.groupBoxT3.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT3.Location = new System.Drawing.Point(392, 465);
            this.groupBoxT3.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT3.Name = "groupBoxT3";
            this.groupBoxT3.Size = new System.Drawing.Size(606, 84);
            this.groupBoxT3.TabIndex = 27;
            this.groupBoxT3.TabStop = false;
            this.groupBoxT3.Text = "單行註腳";
            // 
            // rd11
            // 
            this.rd11.AutoSize = true;
            this.rd11.BackColor = System.Drawing.Color.Transparent;
            this.rd11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd11.Font = new System.Drawing.Font("細明體", 12F);
            this.rd11.Location = new System.Drawing.Point(517, 36);
            this.rd11.Name = "rd11";
            this.rd11.Size = new System.Drawing.Size(74, 20);
            this.rd11.TabIndex = 5;
            this.rd11.Tag = "不列印";
            this.rd11.Text = "不列印";
            this.rd11.UseVisualStyleBackColor = true;
            // 
            // rd10
            // 
            this.rd10.AutoSize = true;
            this.rd10.BackColor = System.Drawing.Color.Transparent;
            this.rd10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd10.Font = new System.Drawing.Font("細明體", 12F);
            this.rd10.Location = new System.Drawing.Point(420, 36);
            this.rd10.Name = "rd10";
            this.rd10.Size = new System.Drawing.Size(74, 20);
            this.rd10.TabIndex = 4;
            this.rd10.Tag = "第五組";
            this.rd10.Text = "第五組";
            this.rd10.UseVisualStyleBackColor = true;
            // 
            // rd9
            // 
            this.rd9.AutoSize = true;
            this.rd9.BackColor = System.Drawing.Color.Transparent;
            this.rd9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd9.Font = new System.Drawing.Font("細明體", 12F);
            this.rd9.Location = new System.Drawing.Point(316, 36);
            this.rd9.Name = "rd9";
            this.rd9.Size = new System.Drawing.Size(74, 20);
            this.rd9.TabIndex = 3;
            this.rd9.Tag = "第四組";
            this.rd9.Text = "第四組";
            this.rd9.UseVisualStyleBackColor = true;
            // 
            // rd8
            // 
            this.rd8.AutoSize = true;
            this.rd8.BackColor = System.Drawing.Color.Transparent;
            this.rd8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd8.Font = new System.Drawing.Font("細明體", 12F);
            this.rd8.Location = new System.Drawing.Point(216, 36);
            this.rd8.Name = "rd8";
            this.rd8.Size = new System.Drawing.Size(74, 20);
            this.rd8.TabIndex = 2;
            this.rd8.Tag = "第三組";
            this.rd8.Text = "第三組";
            this.rd8.UseVisualStyleBackColor = true;
            // 
            // rd7
            // 
            this.rd7.AutoSize = true;
            this.rd7.BackColor = System.Drawing.Color.Transparent;
            this.rd7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd7.Font = new System.Drawing.Font("細明體", 12F);
            this.rd7.Location = new System.Drawing.Point(117, 36);
            this.rd7.Name = "rd7";
            this.rd7.Size = new System.Drawing.Size(74, 20);
            this.rd7.TabIndex = 1;
            this.rd7.Tag = "第二組";
            this.rd7.Text = "第二組";
            this.rd7.UseVisualStyleBackColor = true;
            // 
            // rd6
            // 
            this.rd6.AutoSize = true;
            this.rd6.BackColor = System.Drawing.Color.LightBlue;
            this.rd6.Checked = true;
            this.rd6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd6.Font = new System.Drawing.Font("細明體", 12F);
            this.rd6.Location = new System.Drawing.Point(15, 36);
            this.rd6.Name = "rd6";
            this.rd6.Size = new System.Drawing.Size(74, 20);
            this.rd6.TabIndex = 0;
            this.rd6.Tag = "第一組";
            this.rd6.Text = "第一組";
            this.rd6.UseVisualStyleBackColor = true;
            // 
            // BatchStockBrowseAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.groupBoxT3);
            this.Controls.Add(this.orderbybatchno);
            this.Controls.Add(this.orderbyitno);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "BatchStockBrowseAll";
            this.Text = "現有總表";
            this.Load += new System.EventHandler(this.BatchStockBrowseAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.panelT1.ResumeLayout(false);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.groupBoxT3.ResumeLayout(false);
            this.groupBoxT3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnExcel;
        private JE.MyControl.ButtonT btnWord;
        private JE.MyControl.ButtonT btnPreView;
        private JE.MyControl.ButtonT btnPrint;
        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.RadioT rd1;
        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 庫存倉數量;
        private JE.MyControl.DataGridViewTextNumberT 借出倉數量;
        private JE.MyControl.DataGridViewTextNumberT 加工倉數量;
        private JE.MyControl.DataGridViewTextNumberT 借入倉數量;
        private JE.MyControl.DataGridViewTextNumberT 庫存總數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 製造日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 有效日期;
        private JE.MyControl.ButtonSmallT orderbybatchno;
        private JE.MyControl.ButtonSmallT orderbyitno;
        private JE.MyControl.GroupBoxT groupBoxT3;
        private JE.MyControl.RadioT rd11;
        private JE.MyControl.RadioT rd10;
        private JE.MyControl.RadioT rd9;
        private JE.MyControl.RadioT rd8;
        private JE.MyControl.RadioT rd7;
        private JE.MyControl.RadioT rd6;
    }
}