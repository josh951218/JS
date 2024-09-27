namespace S_61.S2
{
    partial class Frm成本與毛利分析
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.稅前金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.成本 = new JE.MyControl.DataGridViewTextNumberT();
            this.毛利 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ittrait = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bomid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.textBoxT1 = new JE.MyControl.TextBoxT();
            this.textBoxT2 = new JE.MyControl.TextBoxT();
            this.textBoxT3 = new JE.MyControl.TextBoxT();
            this.textBoxT4 = new JE.MyControl.TextBoxT();
            this.btnBrowT1 = new JE.MyControl.ButtonSmallT();
            this.btnBrowT2 = new JE.MyControl.ButtonSmallT();
            this.groupBox1 = new JE.MyControl.GroupBoxT();
            this.rdAvgByOneStk = new JE.MyControl.RadioT();
            this.rdAvgByAllStk = new JE.MyControl.RadioT();
            this.radioT4 = new JE.MyControl.RadioT();
            this.radioT3 = new JE.MyControl.RadioT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.品名規格,
            this.產品編號,
            this.數量,
            this.單位,
            this.稅前金額,
            this.成本,
            this.毛利,
            this.包裝數量,
            this.ittrait,
            this.bomid});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 484);
            this.dataGridViewT1.TabIndex = 0;
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
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.數量.DataPropertyName = "qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.數量.FirstNum = 0;
            this.數量.HeaderText = "數量";
            this.數量.LastNum = 0;
            this.數量.MarkThousand = false;
            this.數量.MaxInputLength = 16;
            this.數量.Name = "數量";
            this.數量.NullInput = false;
            this.數量.NullValue = "0";
            this.數量.ReadOnly = true;
            this.數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.數量.Width = 141;
            // 
            // 單位
            // 
            this.單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位.DataPropertyName = "itunit";
            this.單位.HeaderText = "單位";
            this.單位.MaxInputLength = 8;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 77;
            // 
            // 稅前金額
            // 
            this.稅前金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅前金額.DataPropertyName = "mnyb";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.稅前金額.DefaultCellStyle = dataGridViewCellStyle3;
            this.稅前金額.FirstNum = 0;
            this.稅前金額.HeaderText = "稅前金額(本幣)";
            this.稅前金額.LastNum = 0;
            this.稅前金額.MarkThousand = false;
            this.稅前金額.MaxInputLength = 16;
            this.稅前金額.Name = "稅前金額";
            this.稅前金額.NullInput = false;
            this.稅前金額.NullValue = "0";
            this.稅前金額.ReadOnly = true;
            this.稅前金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.稅前金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅前金額.Width = 141;
            // 
            // 成本
            // 
            this.成本.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.成本.DefaultCellStyle = dataGridViewCellStyle4;
            this.成本.FirstNum = 0;
            this.成本.HeaderText = "成本(本幣)";
            this.成本.LastNum = 0;
            this.成本.MarkThousand = false;
            this.成本.MaxInputLength = 16;
            this.成本.Name = "成本";
            this.成本.NullInput = false;
            this.成本.NullValue = "0";
            this.成本.ReadOnly = true;
            this.成本.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.成本.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.成本.Width = 141;
            // 
            // 毛利
            // 
            this.毛利.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.毛利.DefaultCellStyle = dataGridViewCellStyle5;
            this.毛利.FirstNum = 0;
            this.毛利.HeaderText = "毛利(本利)";
            this.毛利.LastNum = 0;
            this.毛利.MarkThousand = false;
            this.毛利.MaxInputLength = 16;
            this.毛利.Name = "毛利";
            this.毛利.NullInput = false;
            this.毛利.NullValue = "0";
            this.毛利.ReadOnly = true;
            this.毛利.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.毛利.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.毛利.Width = 141;
            // 
            // 包裝數量
            // 
            this.包裝數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量.DataPropertyName = "itpkgqty";
            this.包裝數量.HeaderText = "包裝數量";
            this.包裝數量.MaxInputLength = 10;
            this.包裝數量.Name = "包裝數量";
            this.包裝數量.ReadOnly = true;
            this.包裝數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝數量.Visible = false;
            this.包裝數量.Width = 93;
            // 
            // ittrait
            // 
            this.ittrait.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ittrait.DataPropertyName = "ittrait";
            this.ittrait.HeaderText = "ittrait";
            this.ittrait.MaxInputLength = 10;
            this.ittrait.Name = "ittrait";
            this.ittrait.ReadOnly = true;
            this.ittrait.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ittrait.Visible = false;
            this.ittrait.Width = 93;
            // 
            // bomid
            // 
            this.bomid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.bomid.DataPropertyName = "bomid";
            this.bomid.HeaderText = "bomid";
            this.bomid.MaxInputLength = 20;
            this.bomid.Name = "bomid";
            this.bomid.ReadOnly = true;
            this.bomid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.bomid.Visible = false;
            this.bomid.Width = 173;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(14, 507);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "稅前合計";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(272, 507);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(56, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "總成本";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(513, 507);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "毛利總額";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(766, 507);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(56, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "毛利率";
            // 
            // textBoxT1
            // 
            this.textBoxT1.AllowGrayBackColor = true;
            this.textBoxT1.AllowResize = true;
            this.textBoxT1.BackColor = System.Drawing.Color.Silver;
            this.textBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT1.Location = new System.Drawing.Point(86, 502);
            this.textBoxT1.MaxLength = 20;
            this.textBoxT1.Name = "textBoxT1";
            this.textBoxT1.oLen = 0;
            this.textBoxT1.ReadOnly = true;
            this.textBoxT1.Size = new System.Drawing.Size(167, 27);
            this.textBoxT1.TabIndex = 2;
            this.textBoxT1.TabStop = false;
            this.textBoxT1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxT2
            // 
            this.textBoxT2.AllowGrayBackColor = true;
            this.textBoxT2.AllowResize = true;
            this.textBoxT2.BackColor = System.Drawing.Color.Silver;
            this.textBoxT2.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT2.Location = new System.Drawing.Point(328, 502);
            this.textBoxT2.MaxLength = 20;
            this.textBoxT2.Name = "textBoxT2";
            this.textBoxT2.oLen = 0;
            this.textBoxT2.ReadOnly = true;
            this.textBoxT2.Size = new System.Drawing.Size(167, 27);
            this.textBoxT2.TabIndex = 3;
            this.textBoxT2.TabStop = false;
            this.textBoxT2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxT3
            // 
            this.textBoxT3.AllowGrayBackColor = true;
            this.textBoxT3.AllowResize = true;
            this.textBoxT3.BackColor = System.Drawing.Color.Silver;
            this.textBoxT3.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT3.Location = new System.Drawing.Point(585, 502);
            this.textBoxT3.MaxLength = 20;
            this.textBoxT3.Name = "textBoxT3";
            this.textBoxT3.oLen = 0;
            this.textBoxT3.ReadOnly = true;
            this.textBoxT3.Size = new System.Drawing.Size(167, 27);
            this.textBoxT3.TabIndex = 4;
            this.textBoxT3.TabStop = false;
            this.textBoxT3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxT4
            // 
            this.textBoxT4.AllowGrayBackColor = true;
            this.textBoxT4.AllowResize = true;
            this.textBoxT4.BackColor = System.Drawing.Color.Silver;
            this.textBoxT4.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT4.Location = new System.Drawing.Point(822, 502);
            this.textBoxT4.MaxLength = 20;
            this.textBoxT4.Name = "textBoxT4";
            this.textBoxT4.oLen = 0;
            this.textBoxT4.ReadOnly = true;
            this.textBoxT4.Size = new System.Drawing.Size(167, 27);
            this.textBoxT4.TabIndex = 5;
            this.textBoxT4.TabStop = false;
            this.textBoxT4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnBrowT1
            // 
            this.btnBrowT1.Font = new System.Drawing.Font("細明體", 12F);
            this.btnBrowT1.Location = new System.Drawing.Point(705, 559);
            this.btnBrowT1.Margin = new System.Windows.Forms.Padding(3, 30, 3, 10);
            this.btnBrowT1.Name = "btnBrowT1";
            this.btnBrowT1.Size = new System.Drawing.Size(142, 44);
            this.btnBrowT1.TabIndex = 7;
            this.btnBrowT1.Text = "F3:查詢";
            this.btnBrowT1.UseVisualStyleBackColor = true;
            this.btnBrowT1.Click += new System.EventHandler(this.btnBrowT1_Click);
            // 
            // btnBrowT2
            // 
            this.btnBrowT2.Font = new System.Drawing.Font("細明體", 12F);
            this.btnBrowT2.Location = new System.Drawing.Point(861, 559);
            this.btnBrowT2.Margin = new System.Windows.Forms.Padding(3, 30, 3, 10);
            this.btnBrowT2.Name = "btnBrowT2";
            this.btnBrowT2.Size = new System.Drawing.Size(143, 44);
            this.btnBrowT2.TabIndex = 8;
            this.btnBrowT2.Text = "F9:返回";
            this.btnBrowT2.UseVisualStyleBackColor = true;
            this.btnBrowT2.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdAvgByOneStk);
            this.groupBox1.Controls.Add(this.rdAvgByAllStk);
            this.groupBox1.Controls.Add(this.radioT4);
            this.groupBox1.Controls.Add(this.radioT3);
            this.groupBox1.Controls.Add(this.radioT2);
            this.groupBox1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBox1.Location = new System.Drawing.Point(7, 542);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(684, 78);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "成本選擇";
            // 
            // rdAvgByOneStk
            // 
            this.rdAvgByOneStk.AutoSize = true;
            this.rdAvgByOneStk.BackColor = System.Drawing.Color.Transparent;
            this.rdAvgByOneStk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdAvgByOneStk.Font = new System.Drawing.Font("細明體", 12F);
            this.rdAvgByOneStk.Location = new System.Drawing.Point(540, 17);
            this.rdAvgByOneStk.Name = "rdAvgByOneStk";
            this.rdAvgByOneStk.Size = new System.Drawing.Size(138, 20);
            this.rdAvgByOneStk.TabIndex = 1;
            this.rdAvgByOneStk.Tag = "各倉月平均成本";
            this.rdAvgByOneStk.Text = "各倉月平均成本";
            this.rdAvgByOneStk.UseVisualStyleBackColor = false;
            this.rdAvgByOneStk.Visible = false;
            // 
            // rdAvgByAllStk
            // 
            this.rdAvgByAllStk.AutoSize = true;
            this.rdAvgByAllStk.BackColor = System.Drawing.Color.Transparent;
            this.rdAvgByAllStk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdAvgByAllStk.Font = new System.Drawing.Font("細明體", 12F);
            this.rdAvgByAllStk.Location = new System.Drawing.Point(10, 35);
            this.rdAvgByAllStk.Name = "rdAvgByAllStk";
            this.rdAvgByAllStk.Size = new System.Drawing.Size(138, 20);
            this.rdAvgByAllStk.TabIndex = 0;
            this.rdAvgByAllStk.Tag = "全倉月平均成本";
            this.rdAvgByAllStk.Text = "全倉月平均成本";
            this.rdAvgByAllStk.UseVisualStyleBackColor = false;
            // 
            // radioT4
            // 
            this.radioT4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioT4.AutoSize = true;
            this.radioT4.BackColor = System.Drawing.Color.Transparent;
            this.radioT4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT4.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT4.Location = new System.Drawing.Point(521, 35);
            this.radioT4.Margin = new System.Windows.Forms.Padding(40, 3, 3, 3);
            this.radioT4.Name = "radioT4";
            this.radioT4.Size = new System.Drawing.Size(58, 20);
            this.radioT4.TabIndex = 4;
            this.radioT4.Tag = "建檔";
            this.radioT4.Text = "建檔";
            this.radioT4.UseVisualStyleBackColor = true;
            // 
            // radioT3
            // 
            this.radioT3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioT3.AutoSize = true;
            this.radioT3.BackColor = System.Drawing.Color.Transparent;
            this.radioT3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT3.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT3.Location = new System.Drawing.Point(388, 35);
            this.radioT3.Margin = new System.Windows.Forms.Padding(40, 3, 3, 3);
            this.radioT3.Name = "radioT3";
            this.radioT3.Size = new System.Drawing.Size(90, 20);
            this.radioT3.TabIndex = 3;
            this.radioT3.Tag = "標準成本";
            this.radioT3.Text = "標準成本";
            this.radioT3.UseVisualStyleBackColor = true;
            // 
            // radioT2
            // 
            this.radioT2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.LightBlue;
            this.radioT2.Checked = true;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(191, 35);
            this.radioT2.Margin = new System.Windows.Forms.Padding(40, 3, 3, 3);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(154, 20);
            this.radioT2.TabIndex = 2;
            this.radioT2.Tag = "最近一次進貨成本";
            this.radioT2.Text = "最近一次進貨成本";
            this.radioT2.UseVisualStyleBackColor = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(954, 20);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatusLabel2.Text = "插入";
            // 
            // Frm成本與毛利分析
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnBrowT2;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.btnBrowT2);
            this.Controls.Add(this.btnBrowT1);
            this.Controls.Add(this.textBoxT4);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.textBoxT3);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.textBoxT2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBoxT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "Frm成本與毛利分析";
            this.Text = "成本與毛利分析";
            this.Load += new System.EventHandler(this.Frm成本與毛利分析_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.GroupBoxT groupBox1;
        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT textBoxT1;
        private JE.MyControl.TextBoxT textBoxT2;
        private JE.MyControl.TextBoxT textBoxT3;
        private JE.MyControl.TextBoxT textBoxT4;
        private JE.MyControl.ButtonSmallT btnBrowT1;
        private JE.MyControl.ButtonSmallT btnBrowT2;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.RadioT radioT3;
        private JE.MyControl.RadioT radioT4;
        private JE.MyControl.RadioT rdAvgByOneStk;
        private JE.MyControl.RadioT rdAvgByAllStk;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private JE.MyControl.DataGridViewTextNumberT 數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 稅前金額;
        private JE.MyControl.DataGridViewTextNumberT 成本;
        private JE.MyControl.DataGridViewTextNumberT 毛利;
        private System.Windows.Forms.DataGridViewTextBoxColumn 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn ittrait;
        private System.Windows.Forms.DataGridViewTextBoxColumn bomid;
    }
}