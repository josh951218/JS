namespace S_61.subMenuFm_6
{
    partial class 批次修改作業
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
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.點選 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.注音速查 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.進價 = new JE.MyControl.DataGridViewTextNumberT();
            this.售價 = new JE.MyControl.DataGridViewTextNumberT();
            this.產品類別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.包裝單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.包裝售價 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝進價 = new JE.MyControl.DataGridViewTextNumberT();
            this.產品組成 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.包裝數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.供應商 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb1 = new JE.MyControl.TextBoxT();
            this.tb2 = new JE.MyControl.TextBoxT();
            this.comboBox1 = new JE.MyControl.ComboBoxT();
            this.tn1 = new JE.MyControl.TextBoxNumberT();
            this.query2 = new JE.MyControl.ButtonSmallT();
            this.query3 = new JE.MyControl.ButtonSmallT();
            this.query4 = new JE.MyControl.ButtonSmallT();
            this.btnAllCancel = new JE.MyControl.ButtonSmallT();
            this.btnQuery = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnCancel = new JE.MyControl.ButtonSmallT();
            this.btnAll = new JE.MyControl.ButtonSmallT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.qItNo = new JE.MyControl.TextBoxT();
            this.qItName = new JE.MyControl.TextBoxT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.tableLayoutPanelbase1 = new JE.MyControl.TableLayoutPanelbase();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.tableLayoutPanelbase1.SuspendLayout();
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
            this.點選,
            this.產品編號,
            this.品名規格,
            this.注音速查,
            this.進價,
            this.售價,
            this.產品類別,
            this.單位,
            this.包裝單位,
            this.包裝售價,
            this.包裝進價,
            this.產品組成,
            this.包裝數量,
            this.供應商});
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1007, 462);
            this.dataGridViewT1.TabIndex = 2;
            this.dataGridViewT1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellClick);
            // 
            // 點選
            // 
            this.點選.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.點選.DataPropertyName = "點選";
            this.點選.HeaderText = "點選";
            this.點選.MaxInputLength = 4;
            this.點選.Name = "點選";
            this.點選.ReadOnly = true;
            this.點選.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.點選.Width = 45;
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
            // 注音速查
            // 
            this.注音速查.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.注音速查.DataPropertyName = "ItIme";
            this.注音速查.HeaderText = "注音速查";
            this.注音速查.MaxInputLength = 20;
            this.注音速查.Name = "注音速查";
            this.注音速查.ReadOnly = true;
            this.注音速查.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.注音速查.Width = 173;
            // 
            // 進價
            // 
            this.進價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.進價.DataPropertyName = "itbuypri";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.進價.DefaultCellStyle = dataGridViewCellStyle2;
            this.進價.FirstNum = 0;
            this.進價.HeaderText = "進價";
            this.進價.LastNum = 0;
            this.進價.MarkThousand = false;
            this.進價.MaxInputLength = 11;
            this.進價.Name = "進價";
            this.進價.NullInput = false;
            this.進價.NullValue = "0";
            this.進價.ReadOnly = true;
            this.進價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.進價.Width = 101;
            // 
            // 售價
            // 
            this.售價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.售價.DataPropertyName = "ItPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.售價.DefaultCellStyle = dataGridViewCellStyle3;
            this.售價.FirstNum = 0;
            this.售價.HeaderText = "售價";
            this.售價.LastNum = 0;
            this.售價.MarkThousand = false;
            this.售價.MaxInputLength = 11;
            this.售價.Name = "售價";
            this.售價.NullInput = false;
            this.售價.NullValue = "0";
            this.售價.ReadOnly = true;
            this.售價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.售價.Width = 101;
            // 
            // 產品類別
            // 
            this.產品類別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品類別.DataPropertyName = "KiNo";
            this.產品類別.HeaderText = "產品類別";
            this.產品類別.MaxInputLength = 8;
            this.產品類別.Name = "產品類別";
            this.產品類別.ReadOnly = true;
            this.產品類別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.產品類別.Width = 77;
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
            // 包裝單位
            // 
            this.包裝單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝單位.DataPropertyName = "ItUnitP";
            this.包裝單位.HeaderText = "包裝單位";
            this.包裝單位.MaxInputLength = 8;
            this.包裝單位.Name = "包裝單位";
            this.包裝單位.ReadOnly = true;
            this.包裝單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝單位.Width = 77;
            // 
            // 包裝售價
            // 
            this.包裝售價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝售價.DataPropertyName = "ItPriceP";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝售價.DefaultCellStyle = dataGridViewCellStyle4;
            this.包裝售價.FirstNum = 0;
            this.包裝售價.HeaderText = "包裝售價";
            this.包裝售價.LastNum = 0;
            this.包裝售價.MarkThousand = false;
            this.包裝售價.MaxInputLength = 11;
            this.包裝售價.Name = "包裝售價";
            this.包裝售價.NullInput = false;
            this.包裝售價.NullValue = "0";
            this.包裝售價.ReadOnly = true;
            this.包裝售價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝售價.Width = 101;
            // 
            // 包裝進價
            // 
            this.包裝進價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝進價.DataPropertyName = "itbuyprip";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝進價.DefaultCellStyle = dataGridViewCellStyle5;
            this.包裝進價.FirstNum = 0;
            this.包裝進價.HeaderText = "包裝進價";
            this.包裝進價.LastNum = 0;
            this.包裝進價.MarkThousand = false;
            this.包裝進價.MaxInputLength = 11;
            this.包裝進價.Name = "包裝進價";
            this.包裝進價.NullInput = false;
            this.包裝進價.NullValue = "0";
            this.包裝進價.ReadOnly = true;
            this.包裝進價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝進價.Width = 101;
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
            this.產品組成.Visible = false;
            this.產品組成.Width = 93;
            // 
            // 包裝數量
            // 
            this.包裝數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量.DataPropertyName = "ItPkgQty";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝數量.DefaultCellStyle = dataGridViewCellStyle6;
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
            // 供應商
            // 
            this.供應商.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.供應商.DataPropertyName = "FaNo";
            this.供應商.HeaderText = "供應商";
            this.供應商.MaxInputLength = 10;
            this.供應商.Name = "供應商";
            this.供應商.ReadOnly = true;
            this.供應商.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.供應商.Width = 93;
            // 
            // tb1
            // 
            this.tb1.AllowGrayBackColor = false;
            this.tb1.AllowResize = true;
            this.tb1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tb1.Font = new System.Drawing.Font("細明體", 12F);
            this.tb1.Location = new System.Drawing.Point(357, 8);
            this.tb1.MaxLength = 10;
            this.tb1.Name = "tb1";
            this.tb1.oLen = 0;
            this.tb1.Size = new System.Drawing.Size(87, 27);
            this.tb1.TabIndex = 1;
            this.tb1.DoubleClick += new System.EventHandler(this.textBoxT1_DoubleClick);
            this.tb1.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxT1_Validating);
            // 
            // tb2
            // 
            this.tb2.AllowGrayBackColor = true;
            this.tb2.AllowResize = true;
            this.tb2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tb2.BackColor = System.Drawing.Color.Silver;
            this.tb2.Font = new System.Drawing.Font("細明體", 12F);
            this.tb2.Location = new System.Drawing.Point(450, 8);
            this.tb2.MaxLength = 20;
            this.tb2.Name = "tb2";
            this.tb2.oLen = 0;
            this.tb2.ReadOnly = true;
            this.tb2.Size = new System.Drawing.Size(167, 27);
            this.tb2.TabIndex = 2;
            this.tb2.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("細明體", 12F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "類別編號",
            "進價",
            "售價",
            "包裝進價",
            "包裝售價",
            "單位",
            "包裝單位",
            "包裝數量",
            "經銷商",
            "注音速查"});
            this.comboBox1.Location = new System.Drawing.Point(217, 9);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(137, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.TabStop = false;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tn1
            // 
            this.tn1.AllowGrayBackColor = true;
            this.tn1.AllowResize = true;
            this.tn1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tn1.BackColor = System.Drawing.Color.Silver;
            this.tn1.FirstNum = 10;
            this.tn1.Font = new System.Drawing.Font("細明體", 12F);
            this.tn1.LastNum = 0;
            this.tn1.Location = new System.Drawing.Point(623, 8);
            this.tn1.MarkThousand = false;
            this.tn1.MaxLength = 20;
            this.tn1.Name = "tn1";
            this.tn1.NullInput = false;
            this.tn1.NullValue = "0";
            this.tn1.oLen = 0;
            this.tn1.ReadOnly = true;
            this.tn1.Size = new System.Drawing.Size(167, 27);
            this.tn1.TabIndex = 3;
            this.tn1.TabStop = false;
            this.tn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // query2
            // 
            this.query2.Font = new System.Drawing.Font("細明體", 12F);
            this.query2.Location = new System.Drawing.Point(7, 512);
            this.query2.Name = "query2";
            this.query2.Size = new System.Drawing.Size(191, 32);
            this.query2.TabIndex = 3;
            this.query2.Text = "f2:產品編號";
            this.query2.UseVisualStyleBackColor = true;
            this.query2.Click += new System.EventHandler(this.query2_Click);
            // 
            // query3
            // 
            this.query3.Font = new System.Drawing.Font("細明體", 12F);
            this.query3.Location = new System.Drawing.Point(204, 512);
            this.query3.Name = "query3";
            this.query3.Size = new System.Drawing.Size(191, 32);
            this.query3.TabIndex = 4;
            this.query3.Text = "f3:品名規格";
            this.query3.UseVisualStyleBackColor = true;
            this.query3.Click += new System.EventHandler(this.query3_Click);
            // 
            // query4
            // 
            this.query4.Font = new System.Drawing.Font("細明體", 12F);
            this.query4.Location = new System.Drawing.Point(401, 512);
            this.query4.Name = "query4";
            this.query4.Size = new System.Drawing.Size(191, 32);
            this.query4.TabIndex = 5;
            this.query4.Text = "f4:產品類別";
            this.query4.UseVisualStyleBackColor = true;
            this.query4.Click += new System.EventHandler(this.query4_Click);
            // 
            // btnAllCancel
            // 
            this.btnAllCancel.Font = new System.Drawing.Font("細明體", 12F);
            this.btnAllCancel.Location = new System.Drawing.Point(298, 583);
            this.btnAllCancel.Name = "btnAllCancel";
            this.btnAllCancel.Size = new System.Drawing.Size(134, 40);
            this.btnAllCancel.TabIndex = 14;
            this.btnAllCancel.Text = "全部取消";
            this.btnAllCancel.UseVisualStyleBackColor = true;
            this.btnAllCancel.Click += new System.EventHandler(this.btnAllCancel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(438, 583);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(134, 40);
            this.btnQuery.TabIndex = 15;
            this.btnQuery.Text = "F6:查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(578, 583);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(134, 40);
            this.btnGet.TabIndex = 16;
            this.btnGet.Text = "確定";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCancel.Location = new System.Drawing.Point(718, 583);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(134, 40);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "離開";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAll
            // 
            this.btnAll.Font = new System.Drawing.Font("細明體", 12F);
            this.btnAll.Location = new System.Drawing.Point(158, 583);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(134, 40);
            this.btnAll.TabIndex = 13;
            this.btnAll.Text = "全部選擇";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(150, 555);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "產品編號";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(536, 555);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "品名規格";
            // 
            // qItNo
            // 
            this.qItNo.AllowGrayBackColor = false;
            this.qItNo.AllowResize = true;
            this.qItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.qItNo.Location = new System.Drawing.Point(228, 550);
            this.qItNo.MaxLength = 20;
            this.qItNo.Name = "qItNo";
            this.qItNo.oLen = 0;
            this.qItNo.Size = new System.Drawing.Size(167, 27);
            this.qItNo.TabIndex = 11;
            // 
            // qItName
            // 
            this.qItName.AllowGrayBackColor = false;
            this.qItName.AllowResize = true;
            this.qItName.Font = new System.Drawing.Font("細明體", 12F);
            this.qItName.Location = new System.Drawing.Point(614, 550);
            this.qItName.MaxLength = 30;
            this.qItName.Name = "qItName";
            this.qItName.oLen = 0;
            this.qItName.Size = new System.Drawing.Size(247, 27);
            this.qItName.TabIndex = 12;
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
            // tableLayoutPanelbase1
            // 
            this.tableLayoutPanelbase1.ColumnCount = 6;
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelbase1.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.tb1, 2, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.tn1, 4, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.tb2, 3, 0);
            this.tableLayoutPanelbase1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelbase1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelbase1.Name = "tableLayoutPanelbase1";
            this.tableLayoutPanelbase1.RowCount = 1;
            this.tableLayoutPanelbase1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelbase1.Size = new System.Drawing.Size(1010, 43);
            this.tableLayoutPanelbase1.TabIndex = 1;
            // 
            // 批次修改作業
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.tableLayoutPanelbase1);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.query2);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.query3);
            this.Controls.Add(this.query4);
            this.Controls.Add(this.btnAllCancel);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.qItNo);
            this.Controls.Add(this.qItName);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAll);
            this.Name = "批次修改作業";
            this.Text = "批次修改作業";
            this.Load += new System.EventHandler(this.批次修改作業_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.tableLayoutPanelbase1.ResumeLayout(false);
            this.tableLayoutPanelbase1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.TextBoxT tb1;
        private JE.MyControl.TextBoxT tb2;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT btnAllCancel;
        private JE.MyControl.ButtonSmallT btnQuery;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnCancel;
        private JE.MyControl.ButtonSmallT query2;
        private JE.MyControl.ButtonSmallT query3;
        private JE.MyControl.ButtonSmallT query4;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT qItNo;
        private JE.MyControl.TextBoxT qItName;
        private JE.MyControl.ButtonSmallT btnAll;
        private JE.MyControl.ComboBoxT comboBox1;
        private JE.MyControl.TextBoxNumberT tn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 點選;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 注音速查;
        private JE.MyControl.DataGridViewTextNumberT 進價;
        private JE.MyControl.DataGridViewTextNumberT 售價;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品類別;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 包裝單位;
        private JE.MyControl.DataGridViewTextNumberT 包裝售價;
        private JE.MyControl.DataGridViewTextNumberT 包裝進價;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品組成;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 供應商;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPanelbase1;
    }
}