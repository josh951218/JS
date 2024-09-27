namespace S_61.subMenuFm_6
{
    partial class FrmCustBilling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustBilling));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblT2 = new JE.MyControl.LabelT();
            this.cust = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.客戶編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.期初帳款金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.期初帳款餘額 = new JE.MyControl.DataGridViewTextNumberT();
            this.期初預收金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.預收餘額 = new JE.MyControl.DataGridViewTextNumberT();
            this.信用額度 = new JE.MyControl.DataGridViewTextNumberT();
            this.應收帳款餘額 = new JE.MyControl.DataGridViewTextNumberT();
            this.幣別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.帳款期初匯率 = new JE.MyControl.DataGridViewTextNumberT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelNT1 = new JE.MyControl.PanelNT();
            this.panelT1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panelNT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(274, 573);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "客戶編號";
            // 
            // cust
            // 
            this.cust.AllowGrayBackColor = false;
            this.cust.AllowResize = true;
            this.cust.Font = new System.Drawing.Font("細明體", 12F);
            this.cust.Location = new System.Drawing.Point(352, 568);
            this.cust.MaxLength = 10;
            this.cust.Name = "cust";
            this.cust.oLen = 0;
            this.cust.Size = new System.Drawing.Size(87, 27);
            this.cust.TabIndex = 1;
            this.cust.TextChanged += new System.EventHandler(this.cust_TextChanged);
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Location = new System.Drawing.Point(-1, -1);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(217, 79);
            this.panelT1.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 9F);
            this.btnExit.Location = new System.Drawing.Point(138, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 3;
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
            this.btnCancel.Font = new System.Drawing.Font("細明體", 9F);
            this.btnCancel.Location = new System.Drawing.Point(69, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 79);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.UseDefaultSettings = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.SystemColors.Control;
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModify.Font = new System.Drawing.Font("細明體", 9F);
            this.btnModify.Location = new System.Drawing.Point(0, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(69, 79);
            this.btnModify.TabIndex = 1;
            this.btnModify.UseDefaultSettings = false;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.EnabledChanged += new System.EventHandler(this.btnModify_EnabledChanged);
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
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
            this.客戶編號,
            this.客戶簡稱,
            this.期初帳款金額,
            this.期初帳款餘額,
            this.期初預收金額,
            this.預收餘額,
            this.信用額度,
            this.應收帳款餘額,
            this.幣別,
            this.帳款期初匯率});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 536);
            this.dataGridViewT1.TabIndex = 0;
            this.dataGridViewT1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewT1_CellBeginEdit);
            this.dataGridViewT1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellEndEdit);
            // 
            // 客戶編號
            // 
            this.客戶編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶編號.DataPropertyName = "客戶編號";
            this.客戶編號.HeaderText = "客戶編號";
            this.客戶編號.MaxInputLength = 10;
            this.客戶編號.Name = "客戶編號";
            this.客戶編號.ReadOnly = true;
            this.客戶編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶編號.Width = 93;
            // 
            // 客戶簡稱
            // 
            this.客戶簡稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶簡稱.DataPropertyName = "客戶簡稱";
            this.客戶簡稱.HeaderText = "客戶簡稱";
            this.客戶簡稱.MaxInputLength = 10;
            this.客戶簡稱.Name = "客戶簡稱";
            this.客戶簡稱.ReadOnly = true;
            this.客戶簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶簡稱.Width = 93;
            // 
            // 期初帳款金額
            // 
            this.期初帳款金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.期初帳款金額.DataPropertyName = "期初帳款金額";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = "0";
            this.期初帳款金額.DefaultCellStyle = dataGridViewCellStyle2;
            this.期初帳款金額.FirstNum = 0;
            this.期初帳款金額.HeaderText = "期初帳款金額";
            this.期初帳款金額.LastNum = 0;
            this.期初帳款金額.MarkThousand = false;
            this.期初帳款金額.MaxInputLength = 16;
            this.期初帳款金額.Name = "期初帳款金額";
            this.期初帳款金額.NullInput = false;
            this.期初帳款金額.NullValue = "0";
            this.期初帳款金額.ReadOnly = true;
            this.期初帳款金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.期初帳款金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.期初帳款金額.Width = 141;
            // 
            // 期初帳款餘額
            // 
            this.期初帳款餘額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.期初帳款餘額.DataPropertyName = "期初帳款餘額";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.NullValue = "0";
            this.期初帳款餘額.DefaultCellStyle = dataGridViewCellStyle3;
            this.期初帳款餘額.FirstNum = 0;
            this.期初帳款餘額.HeaderText = "期初帳款餘額";
            this.期初帳款餘額.LastNum = 0;
            this.期初帳款餘額.MarkThousand = false;
            this.期初帳款餘額.MaxInputLength = 16;
            this.期初帳款餘額.Name = "期初帳款餘額";
            this.期初帳款餘額.NullInput = false;
            this.期初帳款餘額.NullValue = "0";
            this.期初帳款餘額.ReadOnly = true;
            this.期初帳款餘額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.期初帳款餘額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.期初帳款餘額.Width = 141;
            // 
            // 期初預收金額
            // 
            this.期初預收金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.期初預收金額.DataPropertyName = "期初預收金額";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.NullValue = "0";
            this.期初預收金額.DefaultCellStyle = dataGridViewCellStyle4;
            this.期初預收金額.FirstNum = 0;
            this.期初預收金額.HeaderText = "期初預收金額";
            this.期初預收金額.LastNum = 0;
            this.期初預收金額.MarkThousand = false;
            this.期初預收金額.MaxInputLength = 16;
            this.期初預收金額.Name = "期初預收金額";
            this.期初預收金額.NullInput = false;
            this.期初預收金額.NullValue = "0";
            this.期初預收金額.ReadOnly = true;
            this.期初預收金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.期初預收金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.期初預收金額.Width = 141;
            // 
            // 預收餘額
            // 
            this.預收餘額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.預收餘額.DataPropertyName = "預收餘額";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.NullValue = "0";
            this.預收餘額.DefaultCellStyle = dataGridViewCellStyle5;
            this.預收餘額.FirstNum = 0;
            this.預收餘額.HeaderText = "預收餘額";
            this.預收餘額.LastNum = 0;
            this.預收餘額.MarkThousand = false;
            this.預收餘額.MaxInputLength = 16;
            this.預收餘額.Name = "預收餘額";
            this.預收餘額.NullInput = false;
            this.預收餘額.NullValue = "0";
            this.預收餘額.ReadOnly = true;
            this.預收餘額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.預收餘額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.預收餘額.Width = 141;
            // 
            // 信用額度
            // 
            this.信用額度.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.信用額度.DataPropertyName = "信用額度";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.NullValue = "0";
            this.信用額度.DefaultCellStyle = dataGridViewCellStyle6;
            this.信用額度.FirstNum = 0;
            this.信用額度.HeaderText = "信用額度";
            this.信用額度.LastNum = 0;
            this.信用額度.MarkThousand = false;
            this.信用額度.MaxInputLength = 16;
            this.信用額度.Name = "信用額度";
            this.信用額度.NullInput = false;
            this.信用額度.NullValue = "0";
            this.信用額度.ReadOnly = true;
            this.信用額度.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.信用額度.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.信用額度.Width = 141;
            // 
            // 應收帳款餘額
            // 
            this.應收帳款餘額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.應收帳款餘額.DataPropertyName = "應收帳款餘額";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.NullValue = "0";
            this.應收帳款餘額.DefaultCellStyle = dataGridViewCellStyle7;
            this.應收帳款餘額.FirstNum = 0;
            this.應收帳款餘額.HeaderText = "應收帳款餘額";
            this.應收帳款餘額.LastNum = 0;
            this.應收帳款餘額.MarkThousand = false;
            this.應收帳款餘額.MaxInputLength = 16;
            this.應收帳款餘額.Name = "應收帳款餘額";
            this.應收帳款餘額.NullInput = false;
            this.應收帳款餘額.NullValue = "0";
            this.應收帳款餘額.ReadOnly = true;
            this.應收帳款餘額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.應收帳款餘額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.應收帳款餘額.Width = 141;
            // 
            // 幣別
            // 
            this.幣別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.幣別.DataPropertyName = "幣別";
            this.幣別.HeaderText = "幣別";
            this.幣別.MaxInputLength = 8;
            this.幣別.Name = "幣別";
            this.幣別.ReadOnly = true;
            this.幣別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.幣別.Width = 77;
            // 
            // 帳款期初匯率
            // 
            this.帳款期初匯率.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.帳款期初匯率.DataPropertyName = "帳款期初匯率";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.NullValue = "0";
            this.帳款期初匯率.DefaultCellStyle = dataGridViewCellStyle8;
            this.帳款期初匯率.FirstNum = 0;
            this.帳款期初匯率.HeaderText = "帳款期初匯率";
            this.帳款期初匯率.LastNum = 0;
            this.帳款期初匯率.MarkThousand = false;
            this.帳款期初匯率.MaxInputLength = 16;
            this.帳款期初匯率.Name = "帳款期初匯率";
            this.帳款期初匯率.NullInput = false;
            this.帳款期初匯率.NullValue = "0";
            this.帳款期初匯率.ReadOnly = true;
            this.帳款期初匯率.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.帳款期初匯率.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.帳款期初匯率.Width = 141;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(954, 20);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatusLabel3.Text = "插入";
            // 
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Controls.Add(this.panelT1);
            this.panelNT1.Location = new System.Drawing.Point(520, 542);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(225, 79);
            this.panelNT1.TabIndex = 2;
            // 
            // FrmCustBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelNT1);
            this.Controls.Add(this.cust);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmCustBilling";
            this.Tag = "應收帳款開帳";
            this.Text = "應收帳款開帳作業";
            this.Load += new System.EventHandler(this.FrmCustBilling_Load);
            this.Shown += new System.EventHandler(this.FrmCustBilling_Shown);
            this.panelT1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelNT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnModify;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT cust;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶簡稱;
        private JE.MyControl.DataGridViewTextNumberT 期初帳款金額;
        private JE.MyControl.DataGridViewTextNumberT 期初帳款餘額;
        private JE.MyControl.DataGridViewTextNumberT 期初預收金額;
        private JE.MyControl.DataGridViewTextNumberT 預收餘額;
        private JE.MyControl.DataGridViewTextNumberT 信用額度;
        private JE.MyControl.DataGridViewTextNumberT 應收帳款餘額;
        private System.Windows.Forms.DataGridViewTextBoxColumn 幣別;
        private JE.MyControl.DataGridViewTextNumberT 帳款期初匯率;
        private JE.MyControl.PanelNT panelNT1;
        private JE.MyControl.ButtonT btnCancel;
    }
}