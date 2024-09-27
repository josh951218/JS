namespace S_61.SOther
{
    partial class FrmEmplb
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.EmName = new JE.MyControl.TextBoxT();
            this.EmIdno = new JE.MyControl.TextBoxT();
            this.Em111 = new JE.MyControl.LabelT();
            this.EmSex = new JE.MyControl.TextBoxT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.EmTel = new JE.MyControl.TextBoxT();
            this.EmNo = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.EmReg = new JE.MyControl.TextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.ff3 = new JE.MyControl.LabelT();
            this.EmAtel1 = new JE.MyControl.TextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnQuery = new JE.MyControl.ButtonSmallT();
            this.button1 = new JE.MyControl.ButtonSmallT();
            this.btnAppend = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.員工編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.員工姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.身份證字號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.電話 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.行動電話 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.生日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.性別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.血型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.婚姻 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.籍貫 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.戶籍地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(729, 579);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(149, 41);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "F11:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(291, 490);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "員工性名";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(275, 539);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(88, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "身分證字號";
            // 
            // EmName
            // 
            this.EmName.AllowGrayBackColor = false;
            this.EmName.AllowResize = true;
            this.EmName.Font = new System.Drawing.Font("細明體", 12F);
            this.EmName.Location = new System.Drawing.Point(369, 485);
            this.EmName.MaxLength = 10;
            this.EmName.Name = "EmName";
            this.EmName.oLen = 0;
            this.EmName.Size = new System.Drawing.Size(87, 27);
            this.EmName.TabIndex = 3;
            this.EmName.TextChanged += new System.EventHandler(this.EmNo_TextChanged);
            // 
            // EmIdno
            // 
            this.EmIdno.AllowGrayBackColor = false;
            this.EmIdno.AllowResize = true;
            this.EmIdno.Font = new System.Drawing.Font("細明體", 12F);
            this.EmIdno.Location = new System.Drawing.Point(369, 534);
            this.EmIdno.MaxLength = 10;
            this.EmIdno.Name = "EmIdno";
            this.EmIdno.oLen = 0;
            this.EmIdno.Size = new System.Drawing.Size(87, 27);
            this.EmIdno.TabIndex = 6;
            this.EmIdno.TextChanged += new System.EventHandler(this.EmNo_TextChanged);
            // 
            // Em111
            // 
            this.Em111.AutoSize = true;
            this.Em111.BackColor = System.Drawing.Color.Transparent;
            this.Em111.Font = new System.Drawing.Font("細明體", 12F);
            this.Em111.Location = new System.Drawing.Point(482, 490);
            this.Em111.Name = "Em111";
            this.Em111.Size = new System.Drawing.Size(40, 16);
            this.Em111.TabIndex = 0;
            this.Em111.Text = "性別";
            // 
            // EmSex
            // 
            this.EmSex.AllowGrayBackColor = false;
            this.EmSex.AllowResize = true;
            this.EmSex.Font = new System.Drawing.Font("細明體", 12F);
            this.EmSex.Location = new System.Drawing.Point(531, 485);
            this.EmSex.MaxLength = 16;
            this.EmSex.Name = "EmSex";
            this.EmSex.oLen = 0;
            this.EmSex.Size = new System.Drawing.Size(135, 27);
            this.EmSex.TabIndex = 4;
            this.EmSex.TextChanged += new System.EventHandler(this.EmNo_TextChanged);
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(482, 539);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(40, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "電話";
            // 
            // EmTel
            // 
            this.EmTel.AllowGrayBackColor = false;
            this.EmTel.AllowResize = true;
            this.EmTel.Font = new System.Drawing.Font("細明體", 12F);
            this.EmTel.Location = new System.Drawing.Point(531, 534);
            this.EmTel.MaxLength = 16;
            this.EmTel.Name = "EmTel";
            this.EmTel.oLen = 0;
            this.EmTel.Size = new System.Drawing.Size(135, 27);
            this.EmTel.TabIndex = 7;
            this.EmTel.TextChanged += new System.EventHandler(this.EmNo_TextChanged);
            // 
            // EmNo
            // 
            this.EmNo.AllowGrayBackColor = false;
            this.EmNo.AllowResize = true;
            this.EmNo.Font = new System.Drawing.Font("細明體", 12F);
            this.EmNo.Location = new System.Drawing.Point(155, 485);
            this.EmNo.MaxLength = 4;
            this.EmNo.Name = "EmNo";
            this.EmNo.oLen = 0;
            this.EmNo.Size = new System.Drawing.Size(39, 27);
            this.EmNo.TabIndex = 2;
            this.EmNo.TextChanged += new System.EventHandler(this.EmNo_TextChanged);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(67, 490);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "員工編號";
            // 
            // EmReg
            // 
            this.EmReg.AllowGrayBackColor = false;
            this.EmReg.AllowResize = true;
            this.EmReg.Font = new System.Drawing.Font("細明體", 12F);
            this.EmReg.Location = new System.Drawing.Point(155, 534);
            this.EmReg.MaxLength = 10;
            this.EmReg.Name = "EmReg";
            this.EmReg.oLen = 0;
            this.EmReg.Size = new System.Drawing.Size(87, 27);
            this.EmReg.TabIndex = 5;
            this.EmReg.TextChanged += new System.EventHandler(this.EmNo_TextChanged);
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(67, 539);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "籍    貫";
            // 
            // ff3
            // 
            this.ff3.AutoSize = true;
            this.ff3.BackColor = System.Drawing.Color.Transparent;
            this.ff3.Font = new System.Drawing.Font("細明體", 12F);
            this.ff3.Location = new System.Drawing.Point(699, 539);
            this.ff3.Name = "ff3";
            this.ff3.Size = new System.Drawing.Size(72, 16);
            this.ff3.TabIndex = 0;
            this.ff3.Text = "行動電話";
            // 
            // EmAtel1
            // 
            this.EmAtel1.AllowGrayBackColor = false;
            this.EmAtel1.AllowResize = true;
            this.EmAtel1.Font = new System.Drawing.Font("細明體", 12F);
            this.EmAtel1.Location = new System.Drawing.Point(777, 534);
            this.EmAtel1.MaxLength = 20;
            this.EmAtel1.Name = "EmAtel1";
            this.EmAtel1.oLen = 0;
            this.EmAtel1.Size = new System.Drawing.Size(167, 27);
            this.EmAtel1.TabIndex = 8;
            this.EmAtel1.TextChanged += new System.EventHandler(this.EmNo_TextChanged);
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(723, 490);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(48, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "lblT3";
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(580, 579);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(149, 41);
            this.btnGet.TabIndex = 12;
            this.btnGet.Text = "F9:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(431, 579);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(149, 41);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "F6:字元查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("細明體", 12F);
            this.button1.Location = new System.Drawing.Point(282, 579);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 41);
            this.button1.TabIndex = 10;
            this.button1.Text = "F5:查詢";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAppend
            // 
            this.btnAppend.Font = new System.Drawing.Font("細明體", 12F);
            this.btnAppend.Location = new System.Drawing.Point(133, 579);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(149, 41);
            this.btnAppend.TabIndex = 9;
            this.btnAppend.Text = "F2:新增";
            this.btnAppend.UseVisualStyleBackColor = true;
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
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
            this.員工編號,
            this.員工姓名,
            this.身份證字號,
            this.電話,
            this.行動電話,
            this.生日,
            this.性別,
            this.血型,
            this.婚姻,
            this.籍貫,
            this.戶籍地址});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 467);
            this.dataGridViewT1.TabIndex = 1;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            // 
            // 員工編號
            // 
            this.員工編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.員工編號.DataPropertyName = "EmNo";
            this.員工編號.HeaderText = "員工編號";
            this.員工編號.MaxInputLength = 10;
            this.員工編號.Name = "員工編號";
            this.員工編號.ReadOnly = true;
            this.員工編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.員工編號.Width = 93;
            // 
            // 員工姓名
            // 
            this.員工姓名.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.員工姓名.DataPropertyName = "EmName";
            this.員工姓名.HeaderText = "員工姓名";
            this.員工姓名.MaxInputLength = 10;
            this.員工姓名.Name = "員工姓名";
            this.員工姓名.ReadOnly = true;
            this.員工姓名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.員工姓名.Width = 93;
            // 
            // 身份證字號
            // 
            this.身份證字號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.身份證字號.DataPropertyName = "EmIdNo";
            this.身份證字號.HeaderText = "身份證字號";
            this.身份證字號.MaxInputLength = 15;
            this.身份證字號.Name = "身份證字號";
            this.身份證字號.ReadOnly = true;
            this.身份證字號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.身份證字號.Visible = false;
            this.身份證字號.Width = 133;
            // 
            // 電話
            // 
            this.電話.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.電話.DataPropertyName = "EmTel";
            this.電話.HeaderText = "電話";
            this.電話.MaxInputLength = 20;
            this.電話.Name = "電話";
            this.電話.ReadOnly = true;
            this.電話.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.電話.Visible = false;
            this.電話.Width = 173;
            // 
            // 行動電話
            // 
            this.行動電話.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.行動電話.DataPropertyName = "EmAtel1";
            this.行動電話.HeaderText = "行動電話";
            this.行動電話.MaxInputLength = 20;
            this.行動電話.Name = "行動電話";
            this.行動電話.ReadOnly = true;
            this.行動電話.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.行動電話.Visible = false;
            this.行動電話.Width = 173;
            // 
            // 生日
            // 
            this.生日.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.生日.DataPropertyName = "EmBirth";
            this.生日.HeaderText = "生日";
            this.生日.MaxInputLength = 10;
            this.生日.Name = "生日";
            this.生日.ReadOnly = true;
            this.生日.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.生日.Visible = false;
            this.生日.Width = 93;
            // 
            // 性別
            // 
            this.性別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.性別.DataPropertyName = "EmSex";
            this.性別.HeaderText = "性別";
            this.性別.MaxInputLength = 8;
            this.性別.Name = "性別";
            this.性別.ReadOnly = true;
            this.性別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.性別.Visible = false;
            this.性別.Width = 77;
            // 
            // 血型
            // 
            this.血型.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.血型.DataPropertyName = "EmBlood";
            this.血型.HeaderText = "血型";
            this.血型.MaxInputLength = 8;
            this.血型.Name = "血型";
            this.血型.ReadOnly = true;
            this.血型.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.血型.Visible = false;
            this.血型.Width = 77;
            // 
            // 婚姻
            // 
            this.婚姻.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.婚姻.DataPropertyName = "EmMarr";
            this.婚姻.HeaderText = "婚姻";
            this.婚姻.MaxInputLength = 8;
            this.婚姻.Name = "婚姻";
            this.婚姻.ReadOnly = true;
            this.婚姻.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.婚姻.Visible = false;
            this.婚姻.Width = 77;
            // 
            // 籍貫
            // 
            this.籍貫.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.籍貫.DataPropertyName = "EmReg";
            this.籍貫.HeaderText = "籍貫";
            this.籍貫.MaxInputLength = 12;
            this.籍貫.Name = "籍貫";
            this.籍貫.ReadOnly = true;
            this.籍貫.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.籍貫.Visible = false;
            this.籍貫.Width = 109;
            // 
            // 戶籍地址
            // 
            this.戶籍地址.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.戶籍地址.DataPropertyName = "EmAddr1";
            this.戶籍地址.HeaderText = "戶籍地址";
            this.戶籍地址.MaxInputLength = 60;
            this.戶籍地址.Name = "戶籍地址";
            this.戶籍地址.ReadOnly = true;
            this.戶籍地址.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.戶籍地址.Visible = false;
            this.戶籍地址.Width = 493;
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
            // FrmEmplb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.EmAtel1);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.ff3);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.EmTel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.btnAppend);
            this.Controls.Add(this.EmIdno);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.EmName);
            this.Controls.Add(this.EmSex);
            this.Controls.Add(this.Em111);
            this.Controls.Add(this.EmReg);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.EmNo);
            this.Name = "FrmEmplb";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmEmplb_Load);
            this.Shown += new System.EventHandler(this.FrmEmplb_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT Em111;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT ff3;
        private JE.MyControl.ButtonSmallT btnQuery;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.TextBoxT EmAtel1;
        private JE.MyControl.TextBoxT EmReg;
        private JE.MyControl.TextBoxT EmNo;
        private JE.MyControl.TextBoxT EmName;
        private JE.MyControl.TextBoxT EmIdno;
        private JE.MyControl.TextBoxT EmTel;
        private JE.MyControl.TextBoxT EmSex;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT button1;
        private JE.MyControl.ButtonSmallT btnAppend;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 員工編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 員工姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 身份證字號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 電話;
        private System.Windows.Forms.DataGridViewTextBoxColumn 行動電話;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生日;
        private System.Windows.Forms.DataGridViewTextBoxColumn 性別;
        private System.Windows.Forms.DataGridViewTextBoxColumn 血型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 婚姻;
        private System.Windows.Forms.DataGridViewTextBoxColumn 籍貫;
        private System.Windows.Forms.DataGridViewTextBoxColumn 戶籍地址;
    }
}