namespace S_61.S2
{
    partial class FrmLendNew_InfobT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLendNew_InfobT));
            this.labelT1 = new JE.MyControl.LabelT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.CuNo = new JE.MyControl.TextBoxT();
            this.CuName1 = new JE.MyControl.TextBoxT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.前期數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.本期借出數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.本期還入數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.結餘數 = new JE.MyControl.DataGridViewTextNumberT();
            this.總結餘量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.倉庫名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelBtnT2 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnExcel = new JE.MyControl.ButtonT();
            this.btnWord = new JE.MyControl.ButtonT();
            this.btnPreView = new JE.MyControl.ButtonT();
            this.btnPrint = new JE.MyControl.ButtonT();
            this.btnBottom = new JE.MyControl.ButtonT();
            this.btnNext = new JE.MyControl.ButtonT();
            this.btnPrior = new JE.MyControl.ButtonT();
            this.btnTop = new JE.MyControl.ButtonT();
            this.btnDefault = new JE.MyControl.ButtonT();
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radioT3 = new JE.MyControl.RadioT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.panelNT1 = new JE.MyControl.PanelNT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.panelBtnT2.SuspendLayout();
            this.groupBoxT1.SuspendLayout();
            this.panelNT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(30, 14);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "客戶編號";
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(213, 14);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(72, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "客戶名稱";
            // 
            // CuNo
            // 
            this.CuNo.AllowGrayBackColor = true;
            this.CuNo.AllowResize = true;
            this.CuNo.BackColor = System.Drawing.Color.Silver;
            this.CuNo.Font = new System.Drawing.Font("細明體", 12F);
            this.CuNo.Location = new System.Drawing.Point(109, 11);
            this.CuNo.MaxLength = 10;
            this.CuNo.Name = "CuNo";
            this.CuNo.oLen = 0;
            this.CuNo.ReadOnly = true;
            this.CuNo.Size = new System.Drawing.Size(87, 27);
            this.CuNo.TabIndex = 0;
            this.CuNo.TabStop = false;
            // 
            // CuName1
            // 
            this.CuName1.AllowGrayBackColor = true;
            this.CuName1.AllowResize = true;
            this.CuName1.BackColor = System.Drawing.Color.Silver;
            this.CuName1.Font = new System.Drawing.Font("細明體", 12F);
            this.CuName1.Location = new System.Drawing.Point(291, 11);
            this.CuName1.MaxLength = 20;
            this.CuName1.Name = "CuName1";
            this.CuName1.oLen = 0;
            this.CuName1.ReadOnly = true;
            this.CuName1.Size = new System.Drawing.Size(167, 27);
            this.CuName1.TabIndex = 1;
            this.CuName1.TabStop = false;
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
            this.產品編號,
            this.品名規格,
            this.前期數量,
            this.本期借出數量,
            this.本期還入數量,
            this.結餘數,
            this.總結餘量,
            this.單位,
            this.倉庫名稱});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 44);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 494);
            this.dataGridViewT1.TabIndex = 2;
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
            // 前期數量
            // 
            this.前期數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.前期數量.DataPropertyName = "期初數量";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.前期數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.前期數量.FirstNum = 0;
            this.前期數量.HeaderText = "前期數量";
            this.前期數量.LastNum = 0;
            this.前期數量.MarkThousand = false;
            this.前期數量.MaxInputLength = 16;
            this.前期數量.Name = "前期數量";
            this.前期數量.NullInput = false;
            this.前期數量.NullValue = "0";
            this.前期數量.ReadOnly = true;
            this.前期數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.前期數量.Width = 141;
            // 
            // 本期借出數量
            // 
            this.本期借出數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.本期借出數量.DataPropertyName = "leqty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.本期借出數量.DefaultCellStyle = dataGridViewCellStyle3;
            this.本期借出數量.FirstNum = 0;
            this.本期借出數量.HeaderText = "本期借出數量";
            this.本期借出數量.LastNum = 0;
            this.本期借出數量.MarkThousand = false;
            this.本期借出數量.MaxInputLength = 16;
            this.本期借出數量.Name = "本期借出數量";
            this.本期借出數量.NullInput = false;
            this.本期借出數量.NullValue = "0";
            this.本期借出數量.ReadOnly = true;
            this.本期借出數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.本期借出數量.Width = 141;
            // 
            // 本期還入數量
            // 
            this.本期還入數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.本期還入數量.DataPropertyName = "rleqty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.本期還入數量.DefaultCellStyle = dataGridViewCellStyle4;
            this.本期還入數量.FirstNum = 0;
            this.本期還入數量.HeaderText = "本期還入數量";
            this.本期還入數量.LastNum = 0;
            this.本期還入數量.MarkThousand = false;
            this.本期還入數量.MaxInputLength = 16;
            this.本期還入數量.Name = "本期還入數量";
            this.本期還入數量.NullInput = false;
            this.本期還入數量.NullValue = "0";
            this.本期還入數量.ReadOnly = true;
            this.本期還入數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.本期還入數量.Width = 141;
            // 
            // 結餘數
            // 
            this.結餘數.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.結餘數.DataPropertyName = "qty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.結餘數.DefaultCellStyle = dataGridViewCellStyle5;
            this.結餘數.FirstNum = 0;
            this.結餘數.HeaderText = "本期結餘量";
            this.結餘數.LastNum = 0;
            this.結餘數.MarkThousand = false;
            this.結餘數.MaxInputLength = 16;
            this.結餘數.Name = "結餘數";
            this.結餘數.NullInput = false;
            this.結餘數.NullValue = "0";
            this.結餘數.ReadOnly = true;
            this.結餘數.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.結餘數.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.結餘數.Width = 141;
            // 
            // 總結餘量
            // 
            this.總結餘量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.總結餘量.DataPropertyName = "前期加本期";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.總結餘量.DefaultCellStyle = dataGridViewCellStyle6;
            this.總結餘量.FirstNum = 0;
            this.總結餘量.HeaderText = "總結餘量";
            this.總結餘量.LastNum = 0;
            this.總結餘量.MarkThousand = false;
            this.總結餘量.MaxInputLength = 16;
            this.總結餘量.Name = "總結餘量";
            this.總結餘量.NullInput = false;
            this.總結餘量.NullValue = "0";
            this.總結餘量.ReadOnly = true;
            this.總結餘量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.總結餘量.Width = 141;
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
            // 倉庫名稱
            // 
            this.倉庫名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.倉庫名稱.DataPropertyName = "StName";
            this.倉庫名稱.HeaderText = "倉庫名稱";
            this.倉庫名稱.MaxInputLength = 10;
            this.倉庫名稱.Name = "倉庫名稱";
            this.倉庫名稱.ReadOnly = true;
            this.倉庫名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.倉庫名稱.Visible = false;
            this.倉庫名稱.Width = 93;
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
            // panelBtnT2
            // 
            this.panelBtnT2.Controls.Add(this.btnExit);
            this.panelBtnT2.Controls.Add(this.btnExcel);
            this.panelBtnT2.Controls.Add(this.btnWord);
            this.panelBtnT2.Controls.Add(this.btnPreView);
            this.panelBtnT2.Controls.Add(this.btnPrint);
            this.panelBtnT2.Controls.Add(this.btnBottom);
            this.panelBtnT2.Controls.Add(this.btnNext);
            this.panelBtnT2.Controls.Add(this.btnPrior);
            this.panelBtnT2.Controls.Add(this.btnTop);
            this.panelBtnT2.Controls.Add(this.btnDefault);
            this.panelBtnT2.Location = new System.Drawing.Point(-1, -1);
            this.panelBtnT2.Name = "panelBtnT2";
            this.panelBtnT2.Size = new System.Drawing.Size(700, 79);
            this.panelBtnT2.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(621, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 9;
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
            this.btnExcel.Location = new System.Drawing.Point(552, 0);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(69, 79);
            this.btnExcel.TabIndex = 8;
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
            this.btnWord.Location = new System.Drawing.Point(483, 0);
            this.btnWord.Name = "btnWord";
            this.btnWord.Size = new System.Drawing.Size(69, 79);
            this.btnWord.TabIndex = 7;
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
            this.btnPreView.Location = new System.Drawing.Point(414, 0);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(69, 79);
            this.btnPreView.TabIndex = 6;
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
            this.btnPrint.Location = new System.Drawing.Point(345, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(69, 79);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.UseDefaultSettings = false;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnBottom
            // 
            this.btnBottom.BackColor = System.Drawing.SystemColors.Control;
            this.btnBottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBottom.BackgroundImage")));
            this.btnBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBottom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBottom.Location = new System.Drawing.Point(276, 0);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(69, 79);
            this.btnBottom.TabIndex = 4;
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
            this.btnNext.Location = new System.Drawing.Point(207, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(69, 79);
            this.btnNext.TabIndex = 3;
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
            this.btnPrior.Location = new System.Drawing.Point(138, 0);
            this.btnPrior.Name = "btnPrior";
            this.btnPrior.Size = new System.Drawing.Size(69, 79);
            this.btnPrior.TabIndex = 2;
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
            this.btnTop.Location = new System.Drawing.Point(69, 0);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(69, 79);
            this.btnTop.TabIndex = 1;
            this.btnTop.UseDefaultSettings = false;
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.BackColor = System.Drawing.SystemColors.Control;
            this.btnDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDefault.BackgroundImage")));
            this.btnDefault.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDefault.Location = new System.Drawing.Point(0, 0);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(69, 79);
            this.btnDefault.TabIndex = 0;
            this.btnDefault.UseDefaultSettings = false;
            this.btnDefault.UseVisualStyleBackColor = false;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radioT3);
            this.groupBoxT1.Controls.Add(this.radioT2);
            this.groupBoxT1.Controls.Add(this.radioT1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(13, 544);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(278, 79);
            this.groupBoxT1.TabIndex = 3;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radioT3
            // 
            this.radioT3.AutoSize = true;
            this.radioT3.BackColor = System.Drawing.Color.Transparent;
            this.radioT3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT3.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT3.Location = new System.Drawing.Point(184, 36);
            this.radioT3.Name = "radioT3";
            this.radioT3.Size = new System.Drawing.Size(74, 20);
            this.radioT3.TabIndex = 2;
            this.radioT3.Tag = "自定一";
            this.radioT3.Text = "自定一";
            this.radioT3.UseVisualStyleBackColor = false;
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.Transparent;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(102, 36);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(74, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Tag = "明細表";
            this.radioT2.Text = "明細表";
            this.radioT2.UseVisualStyleBackColor = false;
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(20, 36);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(74, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.Tag = "標準表";
            this.radioT1.Text = "標準表";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Controls.Add(this.panelBtnT2);
            this.panelNT1.Location = new System.Drawing.Point(297, 546);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(700, 79);
            this.panelNT1.TabIndex = 4;
            // 
            // FrmLendNew_InfobT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelNT1);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.CuName1);
            this.Controls.Add(this.CuNo);
            this.Controls.Add(this.labelT2);
            this.Controls.Add(this.labelT1);
            this.Name = "FrmLendNew_InfobT";
            this.Text = "倉庫借出還入瀏覽-總表";
            this.Load += new System.EventHandler(this.FrmLendNew_InfobT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.panelBtnT2.ResumeLayout(false);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.panelNT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.TextBoxT CuNo;
        private JE.MyControl.TextBoxT CuName1;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelBtnT panelBtnT2;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnExcel;
        private JE.MyControl.ButtonT btnWord;
        private JE.MyControl.ButtonT btnPreView;
        private JE.MyControl.ButtonT btnPrint;
        private JE.MyControl.ButtonT btnBottom;
        private JE.MyControl.ButtonT btnNext;
        private JE.MyControl.ButtonT btnPrior;
        private JE.MyControl.ButtonT btnTop;
        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.PanelNT panelNT1;
        private JE.MyControl.RadioT radioT3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private JE.MyControl.DataGridViewTextNumberT 前期數量;
        private JE.MyControl.DataGridViewTextNumberT 本期借出數量;
        private JE.MyControl.DataGridViewTextNumberT 本期還入數量;
        private JE.MyControl.DataGridViewTextNumberT 結餘數;
        private JE.MyControl.DataGridViewTextNumberT 總結餘量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫名稱;
        private JE.MyControl.ButtonT btnDefault;
    }
}