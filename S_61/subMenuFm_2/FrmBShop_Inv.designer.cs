namespace S_61.subMenuFm_2
{
    partial class FrmBShop_Inv
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblT4 = new JE.MyControl.LabelT();
            this.InvKind = new JE.MyControl.ComboBoxT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.InvDate = new JE.MyControl.TextBoxT();
            this.備註 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblT3 = new JE.MyControl.LabelT();
            this.媒體申報 = new JE.MyControl.GroupBoxT();
            this.CustomsNo = new JE.MyControl.TextBoxT();
            this.海關lbl = new JE.MyControl.LabelT();
            this.Sub = new JE.MyControl.ComboBoxT();
            this.labelT7 = new JE.MyControl.LabelT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.InvName = new JE.MyControl.TextBoxT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.InvAddr1 = new JE.MyControl.TextBoxT();
            this.InvTaxNo = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.機台號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單據憑證 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.舊的發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.稅前金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.營業稅額 = new JE.MyControl.DataGridViewTextNumberT();
            this.應收總額 = new JE.MyControl.DataGridViewTextNumberT();
            this.btnSave = new JE.MyControl.ButtonSmallT();
            this.媒體申報.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(112, 131);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "發票地址";
            // 
            // InvKind
            // 
            this.InvKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InvKind.Font = new System.Drawing.Font("細明體", 12F);
            this.InvKind.FormattingEnabled = true;
            this.InvKind.Items.AddRange(new object[] {
            "21 進項三聯式、電子計算機統一發票",
            "22 進項二聯式收銀機統一發票、載有稅額之其他憑證",
            "25 進項三聯式收銀機統一發票及一般稅額計算之電子發票",
            "28 進項海關代徵營業稅繳納證"});
            this.InvKind.Location = new System.Drawing.Point(156, 26);
            this.InvKind.Name = "InvKind";
            this.InvKind.Size = new System.Drawing.Size(577, 24);
            this.InvKind.TabIndex = 113;
            this.InvKind.SelectedIndexChanged += new System.EventHandler(this.InvKind_SelectedIndexChanged);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(509, 576);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(149, 47);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "F4:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(112, 23);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "發票日期";
            // 
            // InvDate
            // 
            this.InvDate.AllowGrayBackColor = false;
            this.InvDate.AllowResize = true;
            this.InvDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.InvDate.Font = new System.Drawing.Font("細明體", 12F);
            this.InvDate.Location = new System.Drawing.Point(184, 18);
            this.InvDate.MaxLength = 10;
            this.InvDate.Name = "InvDate";
            this.InvDate.oLen = 0;
            this.InvDate.ReadOnly = true;
            this.InvDate.Size = new System.Drawing.Size(87, 27);
            this.InvDate.TabIndex = 1;
            this.InvDate.TabStop = false;
            this.InvDate.Validating += new System.ComponentModel.CancelEventHandler(this.InvDate_Validating);
            // 
            // 備註
            // 
            this.備註.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.備註.DataPropertyName = "memo";
            this.備註.HeaderText = "備註";
            this.備註.MaxInputLength = 20;
            this.備註.Name = "備註";
            this.備註.ReadOnly = true;
            this.備註.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.備註.Width = 173;
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(112, 95);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "統一編號";
            // 
            // 媒體申報
            // 
            this.媒體申報.Controls.Add(this.InvKind);
            this.媒體申報.Controls.Add(this.CustomsNo);
            this.媒體申報.Controls.Add(this.海關lbl);
            this.媒體申報.Controls.Add(this.Sub);
            this.媒體申報.Controls.Add(this.labelT7);
            this.媒體申報.Controls.Add(this.labelT2);
            this.媒體申報.Font = new System.Drawing.Font("細明體", 12F);
            this.媒體申報.Location = new System.Drawing.Point(114, 201);
            this.媒體申報.Name = "媒體申報";
            this.媒體申報.Size = new System.Drawing.Size(783, 180);
            this.媒體申報.TabIndex = 20;
            this.媒體申報.TabStop = false;
            this.媒體申報.Text = "媒體申報";
            // 
            // CustomsNo
            // 
            this.CustomsNo.AllowGrayBackColor = false;
            this.CustomsNo.AllowResize = true;
            this.CustomsNo.BackColor = System.Drawing.Color.White;
            this.CustomsNo.Font = new System.Drawing.Font("細明體", 12F);
            this.CustomsNo.Location = new System.Drawing.Point(156, 132);
            this.CustomsNo.MaxLength = 14;
            this.CustomsNo.Name = "CustomsNo";
            this.CustomsNo.oLen = 0;
            this.CustomsNo.Size = new System.Drawing.Size(119, 27);
            this.CustomsNo.TabIndex = 112;
            // 
            // 海關lbl
            // 
            this.海關lbl.AutoSize = true;
            this.海關lbl.BackColor = System.Drawing.Color.Transparent;
            this.海關lbl.Font = new System.Drawing.Font("細明體", 12F);
            this.海關lbl.Location = new System.Drawing.Point(78, 137);
            this.海關lbl.Name = "海關lbl";
            this.海關lbl.Size = new System.Drawing.Size(72, 16);
            this.海關lbl.TabIndex = 0;
            this.海關lbl.Text = "海關憑證";
            // 
            // Sub
            // 
            this.Sub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Sub.Font = new System.Drawing.Font("細明體", 12F);
            this.Sub.FormattingEnabled = true;
            this.Sub.Items.AddRange(new object[] {
            "1 進項可抵扣之進貨及費用",
            "2 進項可抵扣之固定資產",
            "3 進項不可抵扣之進貨及費用",
            "4 進項不可抵扣之固定資產"});
            this.Sub.Location = new System.Drawing.Point(156, 80);
            this.Sub.Name = "Sub";
            this.Sub.Size = new System.Drawing.Size(345, 24);
            this.Sub.TabIndex = 110;
            // 
            // labelT7
            // 
            this.labelT7.AutoSize = true;
            this.labelT7.BackColor = System.Drawing.Color.Transparent;
            this.labelT7.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT7.Location = new System.Drawing.Point(78, 84);
            this.labelT7.Name = "labelT7";
            this.labelT7.Size = new System.Drawing.Size(72, 16);
            this.labelT7.TabIndex = 0;
            this.labelT7.Text = "抵扣代碼";
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(78, 30);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(72, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "格式代碼";
            // 
            // InvName
            // 
            this.InvName.AllowGrayBackColor = false;
            this.InvName.AllowResize = true;
            this.InvName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.InvName.Font = new System.Drawing.Font("細明體", 12F);
            this.InvName.Location = new System.Drawing.Point(184, 54);
            this.InvName.MaxLength = 50;
            this.InvName.Name = "InvName";
            this.InvName.oLen = 0;
            this.InvName.ReadOnly = true;
            this.InvName.Size = new System.Drawing.Size(407, 27);
            this.InvName.TabIndex = 2;
            this.InvName.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Location = new System.Drawing.Point(0, 626);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.InvAddr1);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.InvDate);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.InvName);
            this.pnlBoxT1.Controls.Add(this.InvTaxNo);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Location = new System.Drawing.Point(114, 12);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(783, 183);
            this.pnlBoxT1.TabIndex = 16;
            // 
            // InvAddr1
            // 
            this.InvAddr1.AllowGrayBackColor = false;
            this.InvAddr1.AllowResize = true;
            this.InvAddr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.InvAddr1.Font = new System.Drawing.Font("細明體", 12F);
            this.InvAddr1.Location = new System.Drawing.Point(184, 126);
            this.InvAddr1.MaxLength = 60;
            this.InvAddr1.Name = "InvAddr1";
            this.InvAddr1.oLen = 0;
            this.InvAddr1.ReadOnly = true;
            this.InvAddr1.Size = new System.Drawing.Size(487, 27);
            this.InvAddr1.TabIndex = 4;
            this.InvAddr1.TabStop = false;
            // 
            // InvTaxNo
            // 
            this.InvTaxNo.AllowGrayBackColor = false;
            this.InvTaxNo.AllowResize = true;
            this.InvTaxNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.InvTaxNo.Font = new System.Drawing.Font("細明體", 12F);
            this.InvTaxNo.Location = new System.Drawing.Point(184, 90);
            this.InvTaxNo.MaxLength = 8;
            this.InvTaxNo.Name = "InvTaxNo";
            this.InvTaxNo.oLen = 0;
            this.InvTaxNo.ReadOnly = true;
            this.InvTaxNo.Size = new System.Drawing.Size(71, 27);
            this.InvTaxNo.TabIndex = 3;
            this.InvTaxNo.TabStop = false;
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(112, 59);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "發票抬頭";
            // 
            // 機台號碼
            // 
            this.機台號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.機台號碼.DataPropertyName = "seno";
            this.機台號碼.HeaderText = "機台號碼";
            this.機台號碼.MaxInputLength = 10;
            this.機台號碼.Name = "機台號碼";
            this.機台號碼.ReadOnly = true;
            this.機台號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.機台號碼.Width = 93;
            // 
            // 單據憑證
            // 
            this.單據憑證.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單據憑證.HeaderText = "單據憑證";
            this.單據憑證.MaxInputLength = 14;
            this.單據憑證.Name = "單據憑證";
            this.單據憑證.ReadOnly = true;
            this.單據憑證.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單據憑證.Width = 125;
            // 
            // 發票日期
            // 
            this.發票日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.發票日期.HeaderText = "發票日期";
            this.發票日期.MaxInputLength = 10;
            this.發票日期.Name = "發票日期";
            this.發票日期.ReadOnly = true;
            this.發票日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.發票日期.Width = 93;
            // 
            // dataGridViewT1
            // 
            this.dataGridViewT1.AllowUserToAddRows = false;
            this.dataGridViewT1.AllowUserToDeleteRows = false;
            this.dataGridViewT1.AllowUserToOrderColumns = true;
            this.dataGridViewT1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewT1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewT1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewT1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.單據憑證,
            this.發票日期,
            this.發票號碼,
            this.舊的發票號碼,
            this.稅前金額,
            this.營業稅額,
            this.應收總額,
            this.機台號碼,
            this.備註});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(114, 387);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(783, 183);
            this.dataGridViewT1.TabIndex = 17;
            this.dataGridViewT1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewT1_CellValidating);
            // 
            // 發票號碼
            // 
            this.發票號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.發票號碼.DataPropertyName = "invno";
            this.發票號碼.HeaderText = "發票號碼";
            this.發票號碼.MaxInputLength = 10;
            this.發票號碼.Name = "發票號碼";
            this.發票號碼.ReadOnly = true;
            this.發票號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.發票號碼.Width = 93;
            // 
            // 舊的發票號碼
            // 
            this.舊的發票號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.舊的發票號碼.DataPropertyName = "oldinvno";
            this.舊的發票號碼.HeaderText = "舊的發票號碼";
            this.舊的發票號碼.MaxInputLength = 16;
            this.舊的發票號碼.Name = "舊的發票號碼";
            this.舊的發票號碼.ReadOnly = true;
            this.舊的發票號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.舊的發票號碼.Visible = false;
            this.舊的發票號碼.Width = 141;
            // 
            // 稅前金額
            // 
            this.稅前金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅前金額.DataPropertyName = "taxmny";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.稅前金額.DefaultCellStyle = dataGridViewCellStyle9;
            this.稅前金額.FirstNum = 0;
            this.稅前金額.HeaderText = "稅前金額";
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
            // 營業稅額
            // 
            this.營業稅額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.營業稅額.DataPropertyName = "tax";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.營業稅額.DefaultCellStyle = dataGridViewCellStyle10;
            this.營業稅額.FirstNum = 0;
            this.營業稅額.HeaderText = "營業稅額";
            this.營業稅額.LastNum = 0;
            this.營業稅額.MarkThousand = false;
            this.營業稅額.MaxInputLength = 16;
            this.營業稅額.Name = "營業稅額";
            this.營業稅額.NullInput = false;
            this.營業稅額.NullValue = "0";
            this.營業稅額.ReadOnly = true;
            this.營業稅額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.營業稅額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.營業稅額.Width = 141;
            // 
            // 應收總額
            // 
            this.應收總額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.應收總額.DataPropertyName = "totmny";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.應收總額.DefaultCellStyle = dataGridViewCellStyle11;
            this.應收總額.FirstNum = 0;
            this.應收總額.HeaderText = "應收總額";
            this.應收總額.LastNum = 0;
            this.應收總額.MarkThousand = false;
            this.應收總額.MaxInputLength = 16;
            this.應收總額.Name = "應收總額";
            this.應收總額.NullInput = false;
            this.應收總額.NullValue = "0";
            this.應收總額.ReadOnly = true;
            this.應收總額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.應收總額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.應收總額.Width = 141;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("細明體", 12F);
            this.btnSave.Location = new System.Drawing.Point(352, 576);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(149, 47);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "F9:儲存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmBShop_Inv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.媒體申報);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.btnSave);
            this.Name = "FrmBShop_Inv";
            this.Text = "FrmBShop_Inv";
            this.Load += new System.EventHandler(this.FrmBShop_Inv_Load);
            this.媒體申報.ResumeLayout(false);
            this.媒體申報.PerformLayout();
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT4;
        public JE.MyControl.ComboBoxT InvKind;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.LabelT lblT1;
        public JE.MyControl.TextBoxT InvDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註;
        private JE.MyControl.LabelT lblT3;
        public JE.MyControl.TextBoxT CustomsNo;
        private JE.MyControl.LabelT 海關lbl;
        public JE.MyControl.ComboBoxT Sub;
        private JE.MyControl.LabelT labelT7;
        private JE.MyControl.LabelT labelT2;
        public JE.MyControl.TextBoxT InvName;
        private JE.MyControl.StatusStripT statusStrip1;
        private JE.MyControl.PanelT pnlBoxT1;
        public JE.MyControl.TextBoxT InvAddr1;
        public JE.MyControl.TextBoxT InvTaxNo;
        private JE.MyControl.LabelT lblT2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 機台號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單據憑證;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票日期;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 舊的發票號碼;
        private JE.MyControl.DataGridViewTextNumberT 稅前金額;
        private JE.MyControl.DataGridViewTextNumberT 營業稅額;
        private JE.MyControl.DataGridViewTextNumberT 應收總額;
        private JE.MyControl.ButtonSmallT btnSave;
        public JE.MyControl.GroupBoxT 媒體申報;
    }
}