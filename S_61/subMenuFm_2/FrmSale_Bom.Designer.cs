namespace S_61.subMenuFm_2
{
    partial class FrmSale_Bom
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItRec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.標準用量 = new JE.MyControl.DataGridViewTextNumberT();
            this.母件比例 = new JE.MyControl.DataGridViewTextNumberT();
            this.單價 = new JE.MyControl.DataGridViewTextNumberT();
            this.折數 = new JE.MyControl.DataGridViewTextNumberT();
            this.金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoItNo = new JE.MyControl.TextBoxT();
            this.BoItName = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.BoTotQty = new JE.MyControl.TextBoxNumberT();
            this.BoTotMny = new JE.MyControl.TextBoxNumberT();
            this.gridAppend = new JE.MyControl.ButtonSmallT();
            this.gridDelete = new JE.MyControl.ButtonSmallT();
            this.gridPic = new JE.MyControl.ButtonSmallT();
            this.gridInsert = new JE.MyControl.ButtonSmallT();
            this.gridStk = new JE.MyControl.ButtonSmallT();
            this.gridGetMny = new JE.MyControl.ButtonSmallT();
            this.gridGetBomD = new JE.MyControl.ButtonSmallT();
            this.gridExit = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.gridbatch = new JE.MyControl.ButtonSmallT();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gridItDesp = new JE.MyControl.ButtonSmallT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewT1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序號,
            this.ItRec,
            this.產品編號,
            this.品名規格,
            this.單位,
            this.標準用量,
            this.母件比例,
            this.單價,
            this.折數,
            this.金額,
            this.包裝數量,
            this.說明});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 45);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 499);
            this.dataGridViewT1.TabIndex = 2;
            this.dataGridViewT1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewT1_CellBeginEdit);
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            this.dataGridViewT1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewT1_CellValidating);
            this.dataGridViewT1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewT1_RowsAdded);
            this.dataGridViewT1.Click += new System.EventHandler(this.dataGridViewT1_Click);
            // 
            // 序號
            // 
            this.序號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.序號.HeaderText = "序號";
            this.序號.MaxInputLength = 4;
            this.序號.Name = "序號";
            this.序號.ReadOnly = true;
            this.序號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.序號.Width = 45;
            // 
            // ItRec
            // 
            this.ItRec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ItRec.DataPropertyName = "ItRec";
            this.ItRec.HeaderText = "ItRec";
            this.ItRec.Name = "ItRec";
            this.ItRec.ReadOnly = true;
            this.ItRec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ItRec.Visible = false;
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
            // 標準用量
            // 
            this.標準用量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.標準用量.DataPropertyName = "ItQty";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.標準用量.DefaultCellStyle = dataGridViewCellStyle11;
            this.標準用量.FirstNum = 0;
            this.標準用量.HeaderText = "標準用量";
            this.標準用量.LastNum = 0;
            this.標準用量.MarkThousand = false;
            this.標準用量.MaxInputLength = 11;
            this.標準用量.Name = "標準用量";
            this.標準用量.NullInput = false;
            this.標準用量.NullValue = "0";
            this.標準用量.ReadOnly = true;
            this.標準用量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.標準用量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.標準用量.Width = 101;
            // 
            // 母件比例
            // 
            this.母件比例.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.母件比例.DataPropertyName = "ItParePrs";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.母件比例.DefaultCellStyle = dataGridViewCellStyle12;
            this.母件比例.FirstNum = 0;
            this.母件比例.HeaderText = "/ 母件比例";
            this.母件比例.LastNum = 0;
            this.母件比例.MarkThousand = false;
            this.母件比例.MaxInputLength = 11;
            this.母件比例.Name = "母件比例";
            this.母件比例.NullInput = false;
            this.母件比例.NullValue = "0";
            this.母件比例.ReadOnly = true;
            this.母件比例.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.母件比例.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.母件比例.Width = 101;
            // 
            // 單價
            // 
            this.單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單價.DataPropertyName = "ItPrice";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單價.DefaultCellStyle = dataGridViewCellStyle13;
            this.單價.FirstNum = 0;
            this.單價.HeaderText = "* 單價";
            this.單價.LastNum = 0;
            this.單價.MarkThousand = false;
            this.單價.MaxInputLength = 16;
            this.單價.Name = "單價";
            this.單價.NullInput = false;
            this.單價.NullValue = "0";
            this.單價.ReadOnly = true;
            this.單價.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單價.Width = 141;
            // 
            // 折數
            // 
            this.折數.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折數.DataPropertyName = "ItPrs";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.折數.DefaultCellStyle = dataGridViewCellStyle14;
            this.折數.FirstNum = 0;
            this.折數.HeaderText = "* 折數";
            this.折數.LastNum = 0;
            this.折數.MarkThousand = false;
            this.折數.MaxInputLength = 6;
            this.折數.Name = "折數";
            this.折數.NullInput = false;
            this.折數.NullValue = "0";
            this.折數.ReadOnly = true;
            this.折數.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.折數.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.折數.Width = 61;
            // 
            // 金額
            // 
            this.金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.金額.DataPropertyName = "ItMny";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.金額.DefaultCellStyle = dataGridViewCellStyle15;
            this.金額.FirstNum = 0;
            this.金額.HeaderText = "= 金額";
            this.金額.LastNum = 0;
            this.金額.MarkThousand = false;
            this.金額.MaxInputLength = 16;
            this.金額.Name = "金額";
            this.金額.NullInput = false;
            this.金額.NullValue = "0";
            this.金額.ReadOnly = true;
            this.金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.金額.Width = 141;
            // 
            // 包裝數量
            // 
            this.包裝數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量.DataPropertyName = "ItPkgQty";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝數量.DefaultCellStyle = dataGridViewCellStyle16;
            this.包裝數量.FirstNum = 0;
            this.包裝數量.HeaderText = "包裝數量";
            this.包裝數量.LastNum = 0;
            this.包裝數量.MarkThousand = false;
            this.包裝數量.MaxInputLength = 11;
            this.包裝數量.Name = "包裝數量";
            this.包裝數量.NullInput = false;
            this.包裝數量.NullValue = "0";
            this.包裝數量.ReadOnly = true;
            this.包裝數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.包裝數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝數量.Width = 101;
            // 
            // 說明
            // 
            this.說明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.說明.DataPropertyName = "ItNote";
            this.說明.HeaderText = "說明";
            this.說明.MaxInputLength = 20;
            this.說明.Name = "說明";
            this.說明.ReadOnly = true;
            this.說明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.說明.Width = 173;
            // 
            // BoItNo
            // 
            this.BoItNo.AllowGrayBackColor = true;
            this.BoItNo.AllowResize = true;
            this.BoItNo.BackColor = System.Drawing.Color.Silver;
            this.BoItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BoItNo.Location = new System.Drawing.Point(91, 12);
            this.BoItNo.MaxLength = 20;
            this.BoItNo.Name = "BoItNo";
            this.BoItNo.oLen = 0;
            this.BoItNo.ReadOnly = true;
            this.BoItNo.Size = new System.Drawing.Size(167, 27);
            this.BoItNo.TabIndex = 0;
            this.BoItNo.TabStop = false;
            // 
            // BoItName
            // 
            this.BoItName.AllowGrayBackColor = true;
            this.BoItName.AllowResize = true;
            this.BoItName.BackColor = System.Drawing.Color.Silver;
            this.BoItName.Font = new System.Drawing.Font("細明體", 12F);
            this.BoItName.Location = new System.Drawing.Point(346, 12);
            this.BoItName.MaxLength = 30;
            this.BoItName.Name = "BoItName";
            this.BoItName.oLen = 0;
            this.BoItName.ReadOnly = true;
            this.BoItName.Size = new System.Drawing.Size(247, 27);
            this.BoItName.TabIndex = 1;
            this.BoItName.TabStop = false;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(11, 17);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "組件編號";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(266, 17);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "組件名稱";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(20, 555);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "用量總計";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(348, 555);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "金額小計";
            // 
            // BoTotQty
            // 
            this.BoTotQty.AllowGrayBackColor = true;
            this.BoTotQty.AllowResize = true;
            this.BoTotQty.BackColor = System.Drawing.Color.Silver;
            this.BoTotQty.FirstNum = 10;
            this.BoTotQty.Font = new System.Drawing.Font("細明體", 12F);
            this.BoTotQty.LastNum = 0;
            this.BoTotQty.Location = new System.Drawing.Point(98, 550);
            this.BoTotQty.MarkThousand = false;
            this.BoTotQty.MaxLength = 20;
            this.BoTotQty.Name = "BoTotQty";
            this.BoTotQty.NullInput = false;
            this.BoTotQty.NullValue = "0";
            this.BoTotQty.oLen = 0;
            this.BoTotQty.ReadOnly = true;
            this.BoTotQty.Size = new System.Drawing.Size(167, 27);
            this.BoTotQty.TabIndex = 1;
            this.BoTotQty.TabStop = false;
            this.BoTotQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BoTotMny
            // 
            this.BoTotMny.AllowGrayBackColor = true;
            this.BoTotMny.AllowResize = true;
            this.BoTotMny.BackColor = System.Drawing.Color.Silver;
            this.BoTotMny.FirstNum = 10;
            this.BoTotMny.Font = new System.Drawing.Font("細明體", 12F);
            this.BoTotMny.LastNum = 0;
            this.BoTotMny.Location = new System.Drawing.Point(426, 550);
            this.BoTotMny.MarkThousand = false;
            this.BoTotMny.MaxLength = 20;
            this.BoTotMny.Name = "BoTotMny";
            this.BoTotMny.NullInput = false;
            this.BoTotMny.NullValue = "0";
            this.BoTotMny.oLen = 0;
            this.BoTotMny.ReadOnly = true;
            this.BoTotMny.Size = new System.Drawing.Size(167, 27);
            this.BoTotMny.TabIndex = 2;
            this.BoTotMny.TabStop = false;
            this.BoTotMny.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gridAppend
            // 
            this.gridAppend.Font = new System.Drawing.Font("細明體", 12F);
            this.gridAppend.Location = new System.Drawing.Point(6, 583);
            this.gridAppend.Name = "gridAppend";
            this.gridAppend.Size = new System.Drawing.Size(93, 40);
            this.gridAppend.TabIndex = 0;
            this.gridAppend.Text = "F2:新增";
            this.gridAppend.UseVisualStyleBackColor = true;
            this.gridAppend.Click += new System.EventHandler(this.gridAppend_Click);
            // 
            // gridDelete
            // 
            this.gridDelete.Font = new System.Drawing.Font("細明體", 12F);
            this.gridDelete.Location = new System.Drawing.Point(99, 583);
            this.gridDelete.Name = "gridDelete";
            this.gridDelete.Size = new System.Drawing.Size(93, 40);
            this.gridDelete.TabIndex = 1;
            this.gridDelete.Text = "F3:刪除";
            this.gridDelete.UseVisualStyleBackColor = true;
            this.gridDelete.Click += new System.EventHandler(this.gridDelete_Click);
            // 
            // gridPic
            // 
            this.gridPic.Font = new System.Drawing.Font("細明體", 12F);
            this.gridPic.Location = new System.Drawing.Point(192, 583);
            this.gridPic.Name = "gridPic";
            this.gridPic.Size = new System.Drawing.Size(93, 40);
            this.gridPic.TabIndex = 2;
            this.gridPic.Text = "看圖";
            this.gridPic.UseVisualStyleBackColor = true;
            this.gridPic.Click += new System.EventHandler(this.gridPic_Click);
            // 
            // gridInsert
            // 
            this.gridInsert.Font = new System.Drawing.Font("細明體", 12F);
            this.gridInsert.Location = new System.Drawing.Point(285, 583);
            this.gridInsert.Name = "gridInsert";
            this.gridInsert.Size = new System.Drawing.Size(93, 40);
            this.gridInsert.TabIndex = 3;
            this.gridInsert.Text = "F5:插入";
            this.gridInsert.UseVisualStyleBackColor = true;
            this.gridInsert.Click += new System.EventHandler(this.gridInsert_Click);
            // 
            // gridStk
            // 
            this.gridStk.Font = new System.Drawing.Font("細明體", 12F);
            this.gridStk.Location = new System.Drawing.Point(378, 583);
            this.gridStk.Name = "gridStk";
            this.gridStk.Size = new System.Drawing.Size(105, 40);
            this.gridStk.TabIndex = 4;
            this.gridStk.Text = "F6:庫存查詢";
            this.gridStk.UseVisualStyleBackColor = true;
            this.gridStk.Click += new System.EventHandler(this.gridStk_Click);
            // 
            // gridGetMny
            // 
            this.gridGetMny.Font = new System.Drawing.Font("細明體", 12F);
            this.gridGetMny.Location = new System.Drawing.Point(483, 583);
            this.gridGetMny.Name = "gridGetMny";
            this.gridGetMny.Size = new System.Drawing.Size(105, 40);
            this.gridGetMny.TabIndex = 5;
            this.gridGetMny.Text = "F7:金額取回";
            this.gridGetMny.UseVisualStyleBackColor = true;
            this.gridGetMny.Click += new System.EventHandler(this.gridGetMny_Click);
            // 
            // gridGetBomD
            // 
            this.gridGetBomD.Font = new System.Drawing.Font("細明體", 12F);
            this.gridGetBomD.Location = new System.Drawing.Point(588, 583);
            this.gridGetBomD.Name = "gridGetBomD";
            this.gridGetBomD.Size = new System.Drawing.Size(115, 40);
            this.gridGetBomD.TabIndex = 6;
            this.gridGetBomD.Text = "F8:結構取回";
            this.gridGetBomD.UseVisualStyleBackColor = true;
            this.gridGetBomD.Click += new System.EventHandler(this.gridGetBomD_Click);
            // 
            // gridExit
            // 
            this.gridExit.Font = new System.Drawing.Font("細明體", 12F);
            this.gridExit.Location = new System.Drawing.Point(808, 583);
            this.gridExit.Name = "gridExit";
            this.gridExit.Size = new System.Drawing.Size(93, 40);
            this.gridExit.TabIndex = 7;
            this.gridExit.Text = "F4:放棄";
            this.gridExit.UseVisualStyleBackColor = true;
            this.gridExit.Click += new System.EventHandler(this.gridExit_Click);
            // 
            // statusStripT1
            // 
            this.statusStripT1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStripT1.Location = new System.Drawing.Point(0, 625);
            this.statusStripT1.Name = "statusStripT1";
            this.statusStripT1.Size = new System.Drawing.Size(1010, 22);
            this.statusStripT1.TabIndex = 0;
            this.statusStripT1.Text = "statusStripT1";
            // 
            // gridbatch
            // 
            this.gridbatch.AutoSize = true;
            this.gridbatch.BackColor = System.Drawing.SystemColors.Control;
            this.gridbatch.Font = new System.Drawing.Font("細明體", 12F);
            this.gridbatch.Location = new System.Drawing.Point(901, 583);
            this.gridbatch.Name = "gridbatch";
            this.gridbatch.Size = new System.Drawing.Size(105, 40);
            this.gridbatch.TabIndex = 80;
            this.gridbatch.Text = "批號勾稽";
            this.gridbatch.UseVisualStyleBackColor = true;
            this.gridbatch.Visible = false;
            this.gridbatch.Click += new System.EventHandler(this.gridbatch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 251);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1010, 267);
            this.dataGridView1.TabIndex = 81;
            this.dataGridView1.Visible = false;
            // 
            // gridItDesp
            // 
            this.gridItDesp.AutoSize = true;
            this.gridItDesp.BackColor = System.Drawing.SystemColors.Control;
            this.gridItDesp.Font = new System.Drawing.Font("細明體", 12F);
            this.gridItDesp.Location = new System.Drawing.Point(703, 583);
            this.gridItDesp.Name = "gridItDesp";
            this.gridItDesp.Size = new System.Drawing.Size(105, 40);
            this.gridItDesp.TabIndex = 82;
            this.gridItDesp.Text = "規格說明";
            this.gridItDesp.UseVisualStyleBackColor = true;
            this.gridItDesp.Click += new System.EventHandler(this.gridItDesp_Click);
            // 
            // FrmSale_Bom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.gridExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.gridItDesp);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.gridbatch);
            this.Controls.Add(this.BoItNo);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.BoItName);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.gridAppend);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.BoTotQty);
            this.Controls.Add(this.BoTotMny);
            this.Controls.Add(this.gridDelete);
            this.Controls.Add(this.gridPic);
            this.Controls.Add(this.gridGetMny);
            this.Controls.Add(this.gridInsert);
            this.Controls.Add(this.gridExit);
            this.Controls.Add(this.gridStk);
            this.Controls.Add(this.gridGetBomD);
            this.Name = "FrmSale_Bom";
            this.Text = "組合組裝品建檔";
            this.Load += new System.EventHandler(this.FrmSale_Bom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.TextBoxT BoItNo;
        private JE.MyControl.TextBoxT BoItName;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.ButtonSmallT gridAppend;
        private JE.MyControl.ButtonSmallT gridDelete;
        private JE.MyControl.ButtonSmallT gridPic;
        private JE.MyControl.ButtonSmallT gridInsert;
        private JE.MyControl.ButtonSmallT gridStk;
        private JE.MyControl.ButtonSmallT gridGetMny;
        private JE.MyControl.ButtonSmallT gridGetBomD;
        private JE.MyControl.ButtonSmallT gridExit;
        //private JE.MyControl.textbox.StatusBar1 statusBar11;
        //private JE.MyControl.textbox.StatusBar2 statusBar21;
        private JE.MyControl.TextBoxNumberT BoTotQty;
        private JE.MyControl.TextBoxNumberT BoTotMny;
        public JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.ButtonSmallT gridbatch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private JE.MyControl.ButtonSmallT gridItDesp;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItRec;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 標準用量;
        private JE.MyControl.DataGridViewTextNumberT 母件比例;
        private JE.MyControl.DataGridViewTextNumberT 單價;
        private JE.MyControl.DataGridViewTextNumberT 折數;
        private JE.MyControl.DataGridViewTextNumberT 金額;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 說明;
    }
}