namespace S_61.S5
{
    partial class FrmStockInventory1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStockInventory1));
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.庫存倉數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.借出倉數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.加工倉數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.借入倉數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.庫存總數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radioT3 = new JE.MyControl.RadioT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.groupBoxT2 = new JE.MyControl.GroupBoxT();
            this.radioT9 = new JE.MyControl.RadioT();
            this.radioT8 = new JE.MyControl.RadioT();
            this.radioT7 = new JE.MyControl.RadioT();
            this.radioT6 = new JE.MyControl.RadioT();
            this.radioT5 = new JE.MyControl.RadioT();
            this.radioT4 = new JE.MyControl.RadioT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnExcel = new JE.MyControl.ButtonT();
            this.btnWord = new JE.MyControl.ButtonT();
            this.btnPreView = new JE.MyControl.ButtonT();
            this.btnPrint = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.groupBoxT1.SuspendLayout();
            this.groupBoxT2.SuspendLayout();
            this.panelBtnT1.SuspendLayout();
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
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewT1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.產品編號,
            this.品名規格,
            this.單位,
            this.庫存倉數量,
            this.借出倉數量,
            this.加工倉數量,
            this.借入倉數量,
            this.庫存總數量});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 460);
            this.dataGridViewT1.TabIndex = 0;
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.庫存倉數量.DefaultCellStyle = dataGridViewCellStyle10;
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
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.借出倉數量.DefaultCellStyle = dataGridViewCellStyle11;
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
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.加工倉數量.DefaultCellStyle = dataGridViewCellStyle12;
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
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.借入倉數量.DefaultCellStyle = dataGridViewCellStyle13;
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
            this.庫存總數量.DataPropertyName = "itqty";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.庫存總數量.DefaultCellStyle = dataGridViewCellStyle14;
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
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radioT3);
            this.groupBoxT1.Controls.Add(this.radioT2);
            this.groupBoxT1.Controls.Add(this.radioT1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(0, 466);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(377, 72);
            this.groupBoxT1.TabIndex = 1;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radioT3
            // 
            this.radioT3.AutoSize = true;
            this.radioT3.BackColor = System.Drawing.Color.Transparent;
            this.radioT3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT3.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT3.Location = new System.Drawing.Point(257, 33);
            this.radioT3.Name = "radioT3";
            this.radioT3.Size = new System.Drawing.Size(74, 20);
            this.radioT3.TabIndex = 2;
            this.radioT3.Text = "自定二";
            this.radioT3.UseVisualStyleBackColor = false;
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.Transparent;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(159, 33);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(74, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Text = "自定一";
            this.radioT2.UseVisualStyleBackColor = false;
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(45, 33);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(90, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.TabStop = true;
            this.radioT1.Text = "標準報表";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // groupBoxT2
            // 
            this.groupBoxT2.Controls.Add(this.radioT9);
            this.groupBoxT2.Controls.Add(this.radioT8);
            this.groupBoxT2.Controls.Add(this.radioT7);
            this.groupBoxT2.Controls.Add(this.radioT6);
            this.groupBoxT2.Controls.Add(this.radioT5);
            this.groupBoxT2.Controls.Add(this.radioT4);
            this.groupBoxT2.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT2.Location = new System.Drawing.Point(383, 466);
            this.groupBoxT2.Name = "groupBoxT2";
            this.groupBoxT2.Size = new System.Drawing.Size(627, 72);
            this.groupBoxT2.TabIndex = 2;
            this.groupBoxT2.TabStop = false;
            this.groupBoxT2.Text = "單行註腳";
            // 
            // radioT9
            // 
            this.radioT9.AutoSize = true;
            this.radioT9.BackColor = System.Drawing.Color.Transparent;
            this.radioT9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT9.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT9.Location = new System.Drawing.Point(521, 33);
            this.radioT9.Name = "radioT9";
            this.radioT9.Size = new System.Drawing.Size(74, 20);
            this.radioT9.TabIndex = 5;
            this.radioT9.Text = "不列印";
            this.radioT9.UseVisualStyleBackColor = false;
            // 
            // radioT8
            // 
            this.radioT8.AutoSize = true;
            this.radioT8.BackColor = System.Drawing.Color.Transparent;
            this.radioT8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT8.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT8.Location = new System.Drawing.Point(423, 33);
            this.radioT8.Name = "radioT8";
            this.radioT8.Size = new System.Drawing.Size(74, 20);
            this.radioT8.TabIndex = 4;
            this.radioT8.Text = "第一組";
            this.radioT8.UseVisualStyleBackColor = false;
            // 
            // radioT7
            // 
            this.radioT7.AutoSize = true;
            this.radioT7.BackColor = System.Drawing.Color.Transparent;
            this.radioT7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT7.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT7.Location = new System.Drawing.Point(325, 33);
            this.radioT7.Name = "radioT7";
            this.radioT7.Size = new System.Drawing.Size(74, 20);
            this.radioT7.TabIndex = 3;
            this.radioT7.Text = "第一組";
            this.radioT7.UseVisualStyleBackColor = false;
            // 
            // radioT6
            // 
            this.radioT6.AutoSize = true;
            this.radioT6.BackColor = System.Drawing.Color.Transparent;
            this.radioT6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT6.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT6.Location = new System.Drawing.Point(227, 33);
            this.radioT6.Name = "radioT6";
            this.radioT6.Size = new System.Drawing.Size(74, 20);
            this.radioT6.TabIndex = 2;
            this.radioT6.Text = "第一組";
            this.radioT6.UseVisualStyleBackColor = false;
            // 
            // radioT5
            // 
            this.radioT5.AutoSize = true;
            this.radioT5.BackColor = System.Drawing.Color.Transparent;
            this.radioT5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT5.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT5.Location = new System.Drawing.Point(129, 33);
            this.radioT5.Name = "radioT5";
            this.radioT5.Size = new System.Drawing.Size(74, 20);
            this.radioT5.TabIndex = 1;
            this.radioT5.Text = "第一組";
            this.radioT5.UseVisualStyleBackColor = false;
            // 
            // radioT4
            // 
            this.radioT4.AutoSize = true;
            this.radioT4.BackColor = System.Drawing.Color.LightBlue;
            this.radioT4.Checked = true;
            this.radioT4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT4.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT4.Location = new System.Drawing.Point(31, 33);
            this.radioT4.Name = "radioT4";
            this.radioT4.Size = new System.Drawing.Size(74, 20);
            this.radioT4.TabIndex = 0;
            this.radioT4.TabStop = true;
            this.radioT4.Text = "第一組";
            this.radioT4.UseVisualStyleBackColor = false;
            // 
            // panelBtnT1
            // 
            this.panelBtnT1.Controls.Add(this.btnExit);
            this.panelBtnT1.Controls.Add(this.btnExcel);
            this.panelBtnT1.Controls.Add(this.btnWord);
            this.panelBtnT1.Controls.Add(this.btnPreView);
            this.panelBtnT1.Controls.Add(this.btnPrint);
            this.panelBtnT1.Location = new System.Drawing.Point(314, 544);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(355, 79);
            this.panelBtnT1.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(69, 79);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.UseDefaultSettings = false;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            // FrmStockInventory1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.groupBoxT2);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "FrmStockInventory1";
            this.Text = "產品現有庫存表-現有總表";
            this.Load += new System.EventHandler(this.FrmStockInventory1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.groupBoxT2.ResumeLayout(false);
            this.groupBoxT2.PerformLayout();
            this.panelBtnT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT radioT3;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.GroupBoxT groupBoxT2;
        private JE.MyControl.RadioT radioT9;
        private JE.MyControl.RadioT radioT8;
        private JE.MyControl.RadioT radioT7;
        private JE.MyControl.RadioT radioT6;
        private JE.MyControl.RadioT radioT5;
        private JE.MyControl.RadioT radioT4;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnExcel;
        private JE.MyControl.ButtonT btnWord;
        private JE.MyControl.ButtonT btnPreView;
        private JE.MyControl.ButtonT btnPrint;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 庫存倉數量;
        private JE.MyControl.DataGridViewTextNumberT 借出倉數量;
        private JE.MyControl.DataGridViewTextNumberT 加工倉數量;
        private JE.MyControl.DataGridViewTextNumberT 借入倉數量;
        private JE.MyControl.DataGridViewTextNumberT 庫存總數量;
    }
}