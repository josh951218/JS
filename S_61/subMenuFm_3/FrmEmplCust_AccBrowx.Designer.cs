namespace S_61.subMenuFm_3
{
    partial class FrmEmplCust_AccBrowx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmplCust_AccBrowx));
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radio1 = new JE.MyControl.RadioT();
            this.radio2 = new JE.MyControl.RadioT();
            this.radio3 = new JE.MyControl.RadioT();
            this.radio4 = new JE.MyControl.RadioT();
            this.radio5 = new JE.MyControl.RadioT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.textBoxT1 = new JE.MyControl.TextBoxT();
            this.textBoxT2 = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.textBoxT3 = new JE.MyControl.TextBoxT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.客戶編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.幣別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.前期帳款 = new JE.MyControl.DataGridViewTextNumberT();
            this.交易筆數 = new JE.MyControl.DataGridViewTextNumberT();
            this.稅前金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.營業稅額 = new JE.MyControl.DataGridViewTextNumberT();
            this.應收總計 = new JE.MyControl.DataGridViewTextNumberT();
            this.折扣金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.已收加預收 = new JE.MyControl.DataGridViewTextNumberT();
            this.本期應收 = new JE.MyControl.DataGridViewTextNumberT();
            this.前期加本期 = new JE.MyControl.DataGridViewTextNumberT();
            this.groupBoxT2 = new JE.MyControl.GroupBoxT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.radioT3 = new JE.MyControl.RadioT();
            this.radioT4 = new JE.MyControl.RadioT();
            this.radioT5 = new JE.MyControl.RadioT();
            this.radioT6 = new JE.MyControl.RadioT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnExcel = new JE.MyControl.ButtonT();
            this.btnWord = new JE.MyControl.ButtonT();
            this.btnPreView = new JE.MyControl.ButtonT();
            this.btnPrint = new JE.MyControl.ButtonT();
            this.btnBottom = new JE.MyControl.ButtonT();
            this.btnNext = new JE.MyControl.ButtonT();
            this.btnPrior = new JE.MyControl.ButtonT();
            this.btnTop = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.groupBoxT1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.groupBoxT2.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radio5);
            this.groupBoxT1.Controls.Add(this.radio4);
            this.groupBoxT1.Controls.Add(this.radio3);
            this.groupBoxT1.Controls.Add(this.radio2);
            this.groupBoxT1.Controls.Add(this.radio1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(1, 459);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(484, 79);
            this.groupBoxT1.TabIndex = 3;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radio1
            // 
            this.radio1.AutoSize = true;
            this.radio1.BackColor = System.Drawing.Color.LightBlue;
            this.radio1.Checked = true;
            this.radio1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio1.Font = new System.Drawing.Font("細明體", 12F);
            this.radio1.Location = new System.Drawing.Point(8, 38);
            this.radio1.Margin = new System.Windows.Forms.Padding(0);
            this.radio1.Name = "radio1";
            this.radio1.Size = new System.Drawing.Size(90, 20);
            this.radio1.TabIndex = 1;
            this.radio1.Tag = "";
            this.radio1.Text = "稅前報表";
            this.radio1.UseVisualStyleBackColor = false; 
            // 
            // radio2
            // 
            this.radio2.AutoSize = true;
            this.radio2.BackColor = System.Drawing.Color.Transparent;
            this.radio2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio2.Font = new System.Drawing.Font("細明體", 12F);
            this.radio2.Location = new System.Drawing.Point(106, 38);
            this.radio2.Margin = new System.Windows.Forms.Padding(0);
            this.radio2.Name = "radio2";
            this.radio2.Size = new System.Drawing.Size(90, 20);
            this.radio2.TabIndex = 2;
            this.radio2.Tag = "";
            this.radio2.Text = "稅後報表";
            this.radio2.UseVisualStyleBackColor = true; 
            // 
            // radio3
            // 
            this.radio3.AutoSize = true;
            this.radio3.BackColor = System.Drawing.Color.Transparent;
            this.radio3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio3.Font = new System.Drawing.Font("細明體", 12F);
            this.radio3.Location = new System.Drawing.Point(204, 38);
            this.radio3.Margin = new System.Windows.Forms.Padding(0);
            this.radio3.Name = "radio3";
            this.radio3.Size = new System.Drawing.Size(74, 20);
            this.radio3.TabIndex = 3;
            this.radio3.Tag = "";
            this.radio3.Text = "自定一";
            this.radio3.UseVisualStyleBackColor = true; 
            // 
            // radio4
            // 
            this.radio4.AutoSize = true;
            this.radio4.BackColor = System.Drawing.Color.Transparent;
            this.radio4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio4.Font = new System.Drawing.Font("細明體", 12F);
            this.radio4.Location = new System.Drawing.Point(286, 38);
            this.radio4.Margin = new System.Windows.Forms.Padding(0);
            this.radio4.Name = "radio4";
            this.radio4.Size = new System.Drawing.Size(74, 20);
            this.radio4.TabIndex = 4;
            this.radio4.Tag = "";
            this.radio4.Text = "自定二";
            this.radio4.UseVisualStyleBackColor = true; 
            // 
            // radio5
            // 
            this.radio5.AutoSize = true;
            this.radio5.BackColor = System.Drawing.Color.Transparent;
            this.radio5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio5.Font = new System.Drawing.Font("細明體", 12F);
            this.radio5.Location = new System.Drawing.Point(368, 38);
            this.radio5.Margin = new System.Windows.Forms.Padding(0);
            this.radio5.Name = "radio5";
            this.radio5.Size = new System.Drawing.Size(106, 20);
            this.radio5.TabIndex = 5;
            this.radio5.Tag = "";
            this.radio5.Text = "標籤自定一";
            this.radio5.UseVisualStyleBackColor = true; 
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(9, 13);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "業務編號";
            // 
            // textBoxT1
            // 
            this.textBoxT1.AllowGrayBackColor = true;
            this.textBoxT1.AllowResize = true;
            this.textBoxT1.BackColor = System.Drawing.Color.Silver;
            this.textBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT1.Location = new System.Drawing.Point(83, 8);
            this.textBoxT1.MaxLength = 4;
            this.textBoxT1.Name = "textBoxT1";
            this.textBoxT1.oLen = 0;
            this.textBoxT1.ReadOnly = true;
            this.textBoxT1.Size = new System.Drawing.Size(39, 27);
            this.textBoxT1.TabIndex = 1;
            this.textBoxT1.TabStop = false;
            // 
            // textBoxT2
            // 
            this.textBoxT2.AllowGrayBackColor = true;
            this.textBoxT2.AllowResize = true;
            this.textBoxT2.BackColor = System.Drawing.Color.Silver;
            this.textBoxT2.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT2.Location = new System.Drawing.Point(128, 8);
            this.textBoxT2.MaxLength = 10;
            this.textBoxT2.Name = "textBoxT2";
            this.textBoxT2.oLen = 0;
            this.textBoxT2.ReadOnly = true;
            this.textBoxT2.Size = new System.Drawing.Size(87, 27);
            this.textBoxT2.TabIndex = 2;
            this.textBoxT2.TabStop = false;
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(251, 13);
            this.lblT2.Margin = new System.Windows.Forms.Padding(15);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(88, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "交易總筆數";
            // 
            // textBoxT3
            // 
            this.textBoxT3.AllowGrayBackColor = true;
            this.textBoxT3.AllowResize = true;
            this.textBoxT3.BackColor = System.Drawing.Color.Silver;
            this.textBoxT3.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT3.Location = new System.Drawing.Point(339, 8);
            this.textBoxT3.MaxLength = 20;
            this.textBoxT3.Name = "textBoxT3";
            this.textBoxT3.oLen = 0;
            this.textBoxT3.ReadOnly = true;
            this.textBoxT3.Size = new System.Drawing.Size(167, 27);
            this.textBoxT3.TabIndex = 4;
            this.textBoxT3.TabStop = false;
            this.textBoxT3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.幣別,
            this.前期帳款,
            this.交易筆數,
            this.稅前金額,
            this.營業稅額,
            this.應收總計,
            this.折扣金額,
            this.已收加預收,
            this.本期應收,
            this.前期加本期});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 41);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1009, 410);
            this.dataGridViewT1.TabIndex = 2;
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
            // 客戶簡稱
            // 
            this.客戶簡稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶簡稱.DataPropertyName = "cuname1";
            this.客戶簡稱.HeaderText = "客戶簡稱";
            this.客戶簡稱.MaxInputLength = 10;
            this.客戶簡稱.Name = "客戶簡稱";
            this.客戶簡稱.ReadOnly = true;
            this.客戶簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶簡稱.Width = 93;
            // 
            // 幣別
            // 
            this.幣別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.幣別.DataPropertyName = "xa1name";
            this.幣別.HeaderText = "幣別";
            this.幣別.MaxInputLength = 10;
            this.幣別.Name = "幣別";
            this.幣別.ReadOnly = true;
            this.幣別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.幣別.Width = 93;
            // 
            // 前期帳款
            // 
            this.前期帳款.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.前期帳款.DataPropertyName = "前期總金額";
            this.前期帳款.HeaderText = "前期帳款";
            this.前期帳款.MaxInputLength = 16;
            this.前期帳款.Name = "前期帳款";
            this.前期帳款.ReadOnly = true;
            this.前期帳款.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.前期帳款.Width = 141;
            // 
            // 交易筆數
            // 
            this.交易筆數.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.交易筆數.DataPropertyName = "筆數";
            this.交易筆數.HeaderText = "交易筆數";
            this.交易筆數.MaxInputLength = 10;
            this.交易筆數.Name = "交易筆數";
            this.交易筆數.ReadOnly = true;
            this.交易筆數.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.交易筆數.Width = 93;
            // 
            // 稅前金額
            // 
            this.稅前金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅前金額.DataPropertyName = "稅前總金額";
            this.稅前金額.HeaderText = "稅前金額";
            this.稅前金額.MaxInputLength = 16;
            this.稅前金額.Name = "稅前金額";
            this.稅前金額.ReadOnly = true;
            this.稅前金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅前金額.Width = 141;
            // 
            // 營業稅額
            // 
            this.營業稅額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.營業稅額.DataPropertyName = "營業稅總額";
            this.營業稅額.HeaderText = "營業稅額";
            this.營業稅額.MaxInputLength = 16;
            this.營業稅額.Name = "營業稅額";
            this.營業稅額.ReadOnly = true;
            this.營業稅額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.營業稅額.Width = 141;
            // 
            // 應收總計
            // 
            this.應收總計.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.應收總計.DataPropertyName = "應收總金額";
            this.應收總計.HeaderText = "應收總計";
            this.應收總計.MaxInputLength = 16;
            this.應收總計.Name = "應收總計";
            this.應收總計.ReadOnly = true;
            this.應收總計.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.應收總計.Width = 141;
            // 
            // 折扣金額
            // 
            this.折扣金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折扣金額.DataPropertyName = "折扣總金額";
            this.折扣金額.HeaderText = "折扣金額";
            this.折扣金額.MaxInputLength = 16;
            this.折扣金額.Name = "折扣金額";
            this.折扣金額.ReadOnly = true;
            this.折扣金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.折扣金額.Width = 141;
            // 
            // 已收加預收
            // 
            this.已收加預收.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.已收加預收.DataPropertyName = "已收加預收";
            this.已收加預收.HeaderText = "已收加預收";
            this.已收加預收.MaxInputLength = 16;
            this.已收加預收.Name = "已收加預收";
            this.已收加預收.ReadOnly = true;
            this.已收加預收.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.已收加預收.Width = 141;
            // 
            // 本期應收
            // 
            this.本期應收.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.本期應收.DataPropertyName = "本期總金額";
            this.本期應收.HeaderText = "本期應收";
            this.本期應收.MaxInputLength = 16;
            this.本期應收.Name = "本期應收";
            this.本期應收.ReadOnly = true;
            this.本期應收.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.本期應收.Width = 141;
            // 
            // 前期加本期
            // 
            this.前期加本期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.前期加本期.DataPropertyName = "前期加本期";
            this.前期加本期.HeaderText = "前期加本期";
            this.前期加本期.MaxInputLength = 16;
            this.前期加本期.Name = "前期加本期";
            this.前期加本期.ReadOnly = true;
            this.前期加本期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.前期加本期.Width = 141;
            // 
            // groupBoxT2
            // 
            this.groupBoxT2.Controls.Add(this.radioT1);
            this.groupBoxT2.Controls.Add(this.radioT2);
            this.groupBoxT2.Controls.Add(this.radioT6);
            this.groupBoxT2.Controls.Add(this.radioT3);
            this.groupBoxT2.Controls.Add(this.radioT5);
            this.groupBoxT2.Controls.Add(this.radioT4);
            this.groupBoxT2.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT2.Location = new System.Drawing.Point(487, 458);
            this.groupBoxT2.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT2.Name = "groupBoxT2";
            this.groupBoxT2.Size = new System.Drawing.Size(522, 80);
            this.groupBoxT2.TabIndex = 4;
            this.groupBoxT2.TabStop = false;
            this.groupBoxT2.Text = "單行註腳";
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(12, 37);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(74, 20);
            this.radioT1.TabIndex = 1;
            this.radioT1.Tag = "第一組";
            this.radioT1.Text = "第一組";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.Transparent;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(97, 37);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(74, 20);
            this.radioT2.TabIndex = 2;
            this.radioT2.Tag = "第二組";
            this.radioT2.Text = "第二組";
            this.radioT2.UseVisualStyleBackColor = true;
            // 
            // radioT3
            // 
            this.radioT3.AutoSize = true;
            this.radioT3.BackColor = System.Drawing.Color.Transparent;
            this.radioT3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT3.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT3.Location = new System.Drawing.Point(182, 37);
            this.radioT3.Name = "radioT3";
            this.radioT3.Size = new System.Drawing.Size(74, 20);
            this.radioT3.TabIndex = 3;
            this.radioT3.Tag = "第三組";
            this.radioT3.Text = "第三組";
            this.radioT3.UseVisualStyleBackColor = true;
            // 
            // radioT4
            // 
            this.radioT4.AutoSize = true;
            this.radioT4.BackColor = System.Drawing.Color.Transparent;
            this.radioT4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT4.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT4.Location = new System.Drawing.Point(267, 37);
            this.radioT4.Name = "radioT4";
            this.radioT4.Size = new System.Drawing.Size(74, 20);
            this.radioT4.TabIndex = 4;
            this.radioT4.Tag = "第四組";
            this.radioT4.Text = "第四組";
            this.radioT4.UseVisualStyleBackColor = true;
            // 
            // radioT5
            // 
            this.radioT5.AutoSize = true;
            this.radioT5.BackColor = System.Drawing.Color.Transparent;
            this.radioT5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT5.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT5.Location = new System.Drawing.Point(352, 37);
            this.radioT5.Name = "radioT5";
            this.radioT5.Size = new System.Drawing.Size(74, 20);
            this.radioT5.TabIndex = 5;
            this.radioT5.Tag = "第五組";
            this.radioT5.Text = "第五組";
            this.radioT5.UseVisualStyleBackColor = true;
            // 
            // radioT6
            // 
            this.radioT6.AutoSize = true;
            this.radioT6.BackColor = System.Drawing.Color.Transparent;
            this.radioT6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT6.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT6.Location = new System.Drawing.Point(437, 37);
            this.radioT6.Name = "radioT6";
            this.radioT6.Size = new System.Drawing.Size(74, 20);
            this.radioT6.TabIndex = 6;
            this.radioT6.Tag = "不列印";
            this.radioT6.Text = "不列印";
            this.radioT6.UseVisualStyleBackColor = true;
            // 
            // panelT1
            // 
            this.panelT1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnExcel);
            this.panelT1.Controls.Add(this.btnWord);
            this.panelT1.Controls.Add(this.btnPreView);
            this.panelT1.Controls.Add(this.btnPrint);
            this.panelT1.Controls.Add(this.btnBottom);
            this.panelT1.Controls.Add(this.btnNext);
            this.panelT1.Controls.Add(this.btnPrior);
            this.panelT1.Controls.Add(this.btnTop);
            this.panelT1.Location = new System.Drawing.Point(190, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(631, 79);
            this.panelT1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(552, 0);
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
            this.btnExcel.Location = new System.Drawing.Point(483, 0);
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
            this.btnWord.Location = new System.Drawing.Point(414, 0);
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
            this.btnPreView.Location = new System.Drawing.Point(345, 0);
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
            this.btnPrint.Location = new System.Drawing.Point(276, 0);
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
            this.btnBottom.Location = new System.Drawing.Point(207, 0);
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
            this.btnNext.Location = new System.Drawing.Point(138, 0);
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
            this.btnPrior.Location = new System.Drawing.Point(69, 0);
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
            this.btnTop.Location = new System.Drawing.Point(0, 0);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(69, 79);
            this.btnTop.TabIndex = 1;
            this.btnTop.UseDefaultSettings = false;
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
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
            // FrmEmplCust_AccBrowx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.textBoxT1);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.textBoxT2);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.groupBoxT2);
            this.Controls.Add(this.textBoxT3);
            this.Controls.Add(this.statusStripT1);
            this.Name = "FrmEmplCust_AccBrowx";
            this.Text = "業務別-應收帳款(幣別總額表)";
            this.Load += new System.EventHandler(this.FrmEmplCust_AccBrowx_Load);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.groupBoxT2.ResumeLayout(false);
            this.groupBoxT2.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT radio1;
        private JE.MyControl.RadioT radio2;
        private JE.MyControl.RadioT radio3;
        private JE.MyControl.RadioT radio4;
        private JE.MyControl.RadioT radio5;
        private JE.MyControl.GroupBoxT groupBoxT2;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.RadioT radioT3;
        private JE.MyControl.RadioT radioT4;
        private JE.MyControl.RadioT radioT5;
        private JE.MyControl.RadioT radioT6;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnExcel;
        private JE.MyControl.ButtonT btnWord;
        private JE.MyControl.ButtonT btnPreView;
        private JE.MyControl.ButtonT btnPrint;
        private JE.MyControl.ButtonT btnBottom;
        private JE.MyControl.ButtonT btnNext;
        private JE.MyControl.ButtonT btnPrior;
        private JE.MyControl.ButtonT btnTop;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.TextBoxT textBoxT1;
        private JE.MyControl.TextBoxT textBoxT2;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT textBoxT3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 幣別;
        private JE.MyControl.DataGridViewTextNumberT 前期帳款;
        private JE.MyControl.DataGridViewTextNumberT 交易筆數;
        private JE.MyControl.DataGridViewTextNumberT 稅前金額;
        private JE.MyControl.DataGridViewTextNumberT 營業稅額;
        private JE.MyControl.DataGridViewTextNumberT 應收總計;
        private JE.MyControl.DataGridViewTextNumberT 折扣金額;
        private JE.MyControl.DataGridViewTextNumberT 已收加預收;
        private JE.MyControl.DataGridViewTextNumberT 本期應收;
        private JE.MyControl.DataGridViewTextNumberT 前期加本期;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}