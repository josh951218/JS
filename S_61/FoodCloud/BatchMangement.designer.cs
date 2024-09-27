namespace S_61
{
    partial class BatchMangement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchMangement));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ItNo = new JE.MyControl.TextBoxT();
            this.lblItName = new JE.MyControl.LabelT();
            this.lblItNo = new JE.MyControl.LabelT();
            this.ItName = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.btnBottom = new JE.MyControl.ButtonT();
            this.btnNext = new JE.MyControl.ButtonT();
            this.btnPrior = new JE.MyControl.ButtonT();
            this.btnTop = new JE.MyControl.ButtonT();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.批次唯一碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.廠商編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.製造廠商 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.批次號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.製造日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.有效日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.倉庫編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.倉庫名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.總庫存量 = new JE.MyControl.DataGridViewTextNumberT();
            this.分倉庫存 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.異動過程 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.原料來源 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.追蹤 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.追溯 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.刪除 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ItTrait = new JE.MyControl.TextBoxT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.panelT1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // ItNo
            // 
            this.ItNo.AllowGrayBackColor = false;
            this.ItNo.AllowResize = true;
            this.ItNo.BackColor = System.Drawing.Color.White;
            this.ItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo.Location = new System.Drawing.Point(96, 13);
            this.ItNo.MaxLength = 20;
            this.ItNo.Name = "ItNo";
            this.ItNo.oLen = 0;
            this.ItNo.Size = new System.Drawing.Size(167, 27);
            this.ItNo.TabIndex = 4;
            this.ItNo.DoubleClick += new System.EventHandler(this.ItNo_DoubleClick);
            this.ItNo.Validating += new System.ComponentModel.CancelEventHandler(this.ItNo_Validating);
            // 
            // lblItName
            // 
            this.lblItName.AutoSize = true;
            this.lblItName.BackColor = System.Drawing.Color.Transparent;
            this.lblItName.Font = new System.Drawing.Font("細明體", 12F);
            this.lblItName.Location = new System.Drawing.Point(280, 18);
            this.lblItName.Name = "lblItName";
            this.lblItName.Size = new System.Drawing.Size(72, 16);
            this.lblItName.TabIndex = 0;
            this.lblItName.Text = "品名規格";
            // 
            // lblItNo
            // 
            this.lblItNo.AutoSize = true;
            this.lblItNo.BackColor = System.Drawing.Color.Transparent;
            this.lblItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.lblItNo.Location = new System.Drawing.Point(24, 18);
            this.lblItNo.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblItNo.Name = "lblItNo";
            this.lblItNo.Size = new System.Drawing.Size(72, 16);
            this.lblItNo.TabIndex = 0;
            this.lblItNo.Text = "產品編號";
            // 
            // ItName
            // 
            this.ItName.AllowGrayBackColor = true;
            this.ItName.AllowResize = true;
            this.ItName.BackColor = System.Drawing.Color.Silver;
            this.ItName.Font = new System.Drawing.Font("細明體", 12F);
            this.ItName.Location = new System.Drawing.Point(352, 13);
            this.ItName.MaxLength = 30;
            this.ItName.Name = "ItName";
            this.ItName.oLen = 0;
            this.ItName.ReadOnly = true;
            this.ItName.Size = new System.Drawing.Size(247, 27);
            this.ItName.TabIndex = 10;
            this.ItName.TabStop = false;
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Controls.Add(this.btnBottom);
            this.panelT1.Controls.Add(this.btnNext);
            this.panelT1.Controls.Add(this.btnPrior);
            this.panelT1.Controls.Add(this.btnTop);
            this.panelT1.Location = new System.Drawing.Point(190, 544);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(631, 79);
            this.panelT1.TabIndex = 14;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 9F);
            this.btnExit.Location = new System.Drawing.Point(552, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 45;
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
            this.btnCancel.Location = new System.Drawing.Point(483, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 79);
            this.btnCancel.TabIndex = 44;
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
            this.btnSave.Font = new System.Drawing.Font("細明體", 9F);
            this.btnSave.Location = new System.Drawing.Point(414, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 79);
            this.btnSave.TabIndex = 43;
            this.btnSave.UseDefaultSettings = false;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.SystemColors.Control;
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModify.Font = new System.Drawing.Font("細明體", 9F);
            this.btnModify.Location = new System.Drawing.Point(345, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(69, 79);
            this.btnModify.TabIndex = 42;
            this.btnModify.UseDefaultSettings = false;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnBrow
            // 
            this.btnBrow.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrow.BackgroundImage")));
            this.btnBrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrow.Font = new System.Drawing.Font("細明體", 9F);
            this.btnBrow.Location = new System.Drawing.Point(276, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 9;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
            // 
            // btnBottom
            // 
            this.btnBottom.BackColor = System.Drawing.SystemColors.Control;
            this.btnBottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBottom.BackgroundImage")));
            this.btnBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBottom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBottom.Font = new System.Drawing.Font("細明體", 9F);
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
            this.btnNext.Font = new System.Drawing.Font("細明體", 9F);
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
            this.btnPrior.Font = new System.Drawing.Font("細明體", 9F);
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
            this.btnTop.Font = new System.Drawing.Font("細明體", 9F);
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
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 626);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(963, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel3.Text = "插入";
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
            this.批次唯一碼,
            this.序號,
            this.廠商編號,
            this.製造廠商,
            this.批次號碼,
            this.製造日期,
            this.有效日期,
            this.產品編號,
            this.品名規格,
            this.倉庫編號,
            this.倉庫名稱,
            this.總庫存量,
            this.分倉庫存,
            this.異動過程,
            this.原料來源,
            this.追蹤,
            this.追溯,
            this.刪除});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = true;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 49);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 490);
            this.dataGridViewT1.TabIndex = 31;
            this.dataGridViewT1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellClick);
            this.dataGridViewT1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewT1_CellValidating);
            // 
            // 批次唯一碼
            // 
            this.批次唯一碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.批次唯一碼.DataPropertyName = "Bno";
            this.批次唯一碼.HeaderText = "批次唯一碼";
            this.批次唯一碼.Name = "批次唯一碼";
            this.批次唯一碼.ReadOnly = true;
            this.批次唯一碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.批次唯一碼.Visible = false;
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
            this.序號.Width = 45;
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
            this.廠商編號.Visible = false;
            this.廠商編號.Width = 93;
            // 
            // 製造廠商
            // 
            this.製造廠商.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.製造廠商.DataPropertyName = "faname";
            this.製造廠商.HeaderText = "製造廠商";
            this.製造廠商.MaxInputLength = 10;
            this.製造廠商.Name = "製造廠商";
            this.製造廠商.ReadOnly = true;
            this.製造廠商.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.製造廠商.Width = 93;
            // 
            // 批次號碼
            // 
            this.批次號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.批次號碼.DataPropertyName = "Batchno";
            this.批次號碼.HeaderText = "批次號碼";
            this.批次號碼.MaxInputLength = 30;
            this.批次號碼.Name = "批次號碼";
            this.批次號碼.ReadOnly = true;
            this.批次號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.批次號碼.Width = 253;
            // 
            // 製造日期
            // 
            this.製造日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.製造日期.DataPropertyName = "Date";
            this.製造日期.HeaderText = "製造日";
            this.製造日期.MaxInputLength = 8;
            this.製造日期.Name = "製造日期";
            this.製造日期.ReadOnly = true;
            this.製造日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.製造日期.Width = 77;
            // 
            // 有效日期
            // 
            this.有效日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.有效日期.DataPropertyName = "Date1";
            this.有效日期.HeaderText = "有效日";
            this.有效日期.MaxInputLength = 8;
            this.有效日期.Name = "有效日期";
            this.有效日期.ReadOnly = true;
            this.有效日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.有效日期.Width = 77;
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
            this.產品編號.Visible = false;
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
            this.品名規格.Visible = false;
            this.品名規格.Width = 253;
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
            this.倉庫編號.Visible = false;
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
            this.倉庫名稱.Visible = false;
            this.倉庫名稱.Width = 93;
            // 
            // 總庫存量
            // 
            this.總庫存量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.總庫存量.DataPropertyName = "總庫存量";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.總庫存量.DefaultCellStyle = dataGridViewCellStyle2;
            this.總庫存量.FirstNum = 0;
            this.總庫存量.HeaderText = "總庫存量";
            this.總庫存量.LastNum = 0;
            this.總庫存量.MarkThousand = false;
            this.總庫存量.Name = "總庫存量";
            this.總庫存量.NullInput = false;
            this.總庫存量.NullValue = "0";
            this.總庫存量.ReadOnly = true;
            this.總庫存量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.總庫存量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.總庫存量.Width = 109;
            // 
            // 分倉庫存
            // 
            this.分倉庫存.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.分倉庫存.DataPropertyName = "分倉庫存";
            this.分倉庫存.FillWeight = 120F;
            this.分倉庫存.HeaderText = "分倉庫存";
            this.分倉庫存.Name = "分倉庫存";
            this.分倉庫存.ReadOnly = true;
            this.分倉庫存.Width = 120;
            // 
            // 異動過程
            // 
            this.異動過程.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.異動過程.DataPropertyName = "異動過程";
            this.異動過程.FillWeight = 120F;
            this.異動過程.HeaderText = "異動過程";
            this.異動過程.Name = "異動過程";
            this.異動過程.ReadOnly = true;
            this.異動過程.Width = 120;
            // 
            // 原料來源
            // 
            this.原料來源.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.原料來源.DataPropertyName = "原料來源";
            this.原料來源.FillWeight = 120F;
            this.原料來源.HeaderText = "原料來源";
            this.原料來源.Name = "原料來源";
            this.原料來源.ReadOnly = true;
            this.原料來源.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.原料來源.Width = 120;
            // 
            // 追蹤
            // 
            this.追蹤.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.追蹤.DataPropertyName = "追蹤";
            this.追蹤.HeaderText = "追蹤";
            this.追蹤.MinimumWidth = 4;
            this.追蹤.Name = "追蹤";
            this.追蹤.ReadOnly = true;
            this.追蹤.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.追蹤.Text = "追蹤";
            // 
            // 追溯
            // 
            this.追溯.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.追溯.DataPropertyName = "追溯";
            this.追溯.HeaderText = "追溯";
            this.追溯.MinimumWidth = 4;
            this.追溯.Name = "追溯";
            this.追溯.ReadOnly = true;
            this.追溯.Text = "追溯";
            // 
            // 刪除
            // 
            this.刪除.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.刪除.DataPropertyName = "刪除";
            this.刪除.HeaderText = "刪除";
            this.刪除.Name = "刪除";
            this.刪除.ReadOnly = true;
            this.刪除.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ItTrait
            // 
            this.ItTrait.AllowGrayBackColor = true;
            this.ItTrait.AllowResize = true;
            this.ItTrait.BackColor = System.Drawing.Color.Silver;
            this.ItTrait.Font = new System.Drawing.Font("細明體", 12F);
            this.ItTrait.Location = new System.Drawing.Point(710, 13);
            this.ItTrait.MaxLength = 8;
            this.ItTrait.Name = "ItTrait";
            this.ItTrait.oLen = 0;
            this.ItTrait.ReadOnly = true;
            this.ItTrait.Size = new System.Drawing.Size(71, 27);
            this.ItTrait.TabIndex = 32;
            this.ItTrait.TabStop = false;
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(628, 18);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "產品組成";
            // 
            // BatchMangement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.labelT1);
            this.Controls.Add(this.ItTrait);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.ItName);
            this.Controls.Add(this.ItNo);
            this.Controls.Add(this.lblItName);
            this.Controls.Add(this.lblItNo);
            this.Name = "BatchMangement";
            this.Text = "批號管理作業";
            this.Load += new System.EventHandler(this.BatchMangement_Load);
            this.panelT1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.TextBoxT ItNo;
        private JE.MyControl.LabelT lblItName;
        private JE.MyControl.LabelT lblItNo;
        private JE.MyControl.TextBoxT ItName;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnBottom;
        private JE.MyControl.ButtonT btnNext;
        private JE.MyControl.ButtonT btnPrior;
        private JE.MyControl.ButtonT btnTop;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.TextBoxT ItTrait;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnCancel;
        private JE.MyControl.ButtonT btnSave;
        private JE.MyControl.ButtonT btnModify;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次唯一碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 製造廠商;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 製造日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 有效日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫名稱;
        private JE.MyControl.DataGridViewTextNumberT 總庫存量;
        private System.Windows.Forms.DataGridViewButtonColumn 分倉庫存;
        private System.Windows.Forms.DataGridViewButtonColumn 異動過程;
        private System.Windows.Forms.DataGridViewButtonColumn 原料來源;
        private System.Windows.Forms.DataGridViewButtonColumn 追蹤;
        private System.Windows.Forms.DataGridViewButtonColumn 追溯;
        private System.Windows.Forms.DataGridViewButtonColumn 刪除;
    }
}