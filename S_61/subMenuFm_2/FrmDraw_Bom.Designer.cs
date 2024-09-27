namespace S_61.subMenuFm_2
{
    partial class FrmDraw_Bom
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridAppend = new JE.MyControl.ButtonSmallT();
            this.gridDelete = new JE.MyControl.ButtonSmallT();
            this.gridPic = new JE.MyControl.ButtonSmallT();
            this.gridInsert = new JE.MyControl.ButtonSmallT();
            this.gridStk = new JE.MyControl.ButtonSmallT();
            this.gridGetBomD = new JE.MyControl.ButtonSmallT();
            this.gridExit = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.標準用量 = new JE.MyControl.DataGridViewTextNumberT();
            this.母件比例 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.BoItNo = new JE.MyControl.TextBoxT();
            this.BoItName = new JE.MyControl.TextBoxT();
            this.BoTotQty = new JE.MyControl.TextBoxNumberT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.gridItDesp = new JE.MyControl.ButtonSmallT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridAppend
            // 
            this.gridAppend.Font = new System.Drawing.Font("細明體", 12F);
            this.gridAppend.Location = new System.Drawing.Point(11, 580);
            this.gridAppend.Name = "gridAppend";
            this.gridAppend.Size = new System.Drawing.Size(123, 43);
            this.gridAppend.TabIndex = 1;
            this.gridAppend.Text = "F2:新增";
            this.gridAppend.UseVisualStyleBackColor = true;
            this.gridAppend.Click += new System.EventHandler(this.gridAppend_Click);
            // 
            // gridDelete
            // 
            this.gridDelete.Font = new System.Drawing.Font("細明體", 12F);
            this.gridDelete.Location = new System.Drawing.Point(134, 580);
            this.gridDelete.Name = "gridDelete";
            this.gridDelete.Size = new System.Drawing.Size(123, 43);
            this.gridDelete.TabIndex = 2;
            this.gridDelete.Text = "F3:刪除";
            this.gridDelete.UseVisualStyleBackColor = true;
            this.gridDelete.Click += new System.EventHandler(this.gridDelete_Click);
            // 
            // gridPic
            // 
            this.gridPic.Font = new System.Drawing.Font("細明體", 12F);
            this.gridPic.Location = new System.Drawing.Point(257, 580);
            this.gridPic.Name = "gridPic";
            this.gridPic.Size = new System.Drawing.Size(123, 43);
            this.gridPic.TabIndex = 3;
            this.gridPic.Text = "看圖";
            this.gridPic.UseVisualStyleBackColor = true;
            this.gridPic.Click += new System.EventHandler(this.gridPic_Click);
            // 
            // gridInsert
            // 
            this.gridInsert.Font = new System.Drawing.Font("細明體", 12F);
            this.gridInsert.Location = new System.Drawing.Point(380, 580);
            this.gridInsert.Name = "gridInsert";
            this.gridInsert.Size = new System.Drawing.Size(123, 43);
            this.gridInsert.TabIndex = 4;
            this.gridInsert.Text = "F5:插入";
            this.gridInsert.UseVisualStyleBackColor = true;
            this.gridInsert.Click += new System.EventHandler(this.gridInsert_Click);
            // 
            // gridStk
            // 
            this.gridStk.Font = new System.Drawing.Font("細明體", 12F);
            this.gridStk.Location = new System.Drawing.Point(503, 580);
            this.gridStk.Name = "gridStk";
            this.gridStk.Size = new System.Drawing.Size(123, 43);
            this.gridStk.TabIndex = 5;
            this.gridStk.Text = "F6:庫存查詢";
            this.gridStk.UseVisualStyleBackColor = true;
            this.gridStk.Click += new System.EventHandler(this.gridStk_Click);
            // 
            // gridGetBomD
            // 
            this.gridGetBomD.Font = new System.Drawing.Font("細明體", 12F);
            this.gridGetBomD.Location = new System.Drawing.Point(749, 580);
            this.gridGetBomD.Name = "gridGetBomD";
            this.gridGetBomD.Size = new System.Drawing.Size(123, 43);
            this.gridGetBomD.TabIndex = 6;
            this.gridGetBomD.Text = "F9:取回\r\n";
            this.gridGetBomD.UseVisualStyleBackColor = true;
            this.gridGetBomD.Click += new System.EventHandler(this.gridGetBomD_Click);
            // 
            // gridExit
            // 
            this.gridExit.Font = new System.Drawing.Font("細明體", 12F);
            this.gridExit.Location = new System.Drawing.Point(872, 580);
            this.gridExit.Name = "gridExit";
            this.gridExit.Size = new System.Drawing.Size(130, 43);
            this.gridExit.TabIndex = 7;
            this.gridExit.Text = "F11:放棄";
            this.gridExit.UseVisualStyleBackColor = true;
            this.gridExit.Click += new System.EventHandler(this.gridExit_Click);
            // 
            // dataGridViewT1
            // 
            this.dataGridViewT1.AllowUserToAddRows = false;
            this.dataGridViewT1.AllowUserToDeleteRows = false;
            this.dataGridViewT1.AllowUserToOrderColumns = true;
            this.dataGridViewT1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewT1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewT1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewT1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序號,
            this.產品編號,
            this.品名規格,
            this.單位,
            this.標準用量,
            this.母件比例,
            this.包裝數量,
            this.說明});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 43);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 531);
            this.dataGridViewT1.TabIndex = 8;
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
            // 單位
            // 
            this.單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位.DataPropertyName = "itunit";
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
            this.標準用量.DataPropertyName = "itqty";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.標準用量.DefaultCellStyle = dataGridViewCellStyle8;
            this.標準用量.FirstNum = 0;
            this.標準用量.HeaderText = "標準用量";
            this.標準用量.LastNum = 0;
            this.標準用量.MarkThousand = false;
            this.標準用量.MaxInputLength = 11;
            this.標準用量.Name = "標準用量";
            this.標準用量.NullInput = false;
            this.標準用量.NullValue = "0";
            this.標準用量.ReadOnly = true;
            this.標準用量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.標準用量.Width = 101;
            // 
            // 母件比例
            // 
            this.母件比例.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.母件比例.DataPropertyName = "ItParePrs";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.母件比例.DefaultCellStyle = dataGridViewCellStyle9;
            this.母件比例.FirstNum = 0;
            this.母件比例.HeaderText = "/ 母件比例";
            this.母件比例.LastNum = 0;
            this.母件比例.MarkThousand = false;
            this.母件比例.MaxInputLength = 11;
            this.母件比例.Name = "母件比例";
            this.母件比例.NullInput = false;
            this.母件比例.NullValue = "0";
            this.母件比例.ReadOnly = true;
            this.母件比例.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.母件比例.Width = 101;
            // 
            // 包裝數量
            // 
            this.包裝數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量.DataPropertyName = "itpkgqty";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝數量.DefaultCellStyle = dataGridViewCellStyle10;
            this.包裝數量.FirstNum = 0;
            this.包裝數量.HeaderText = "* 包裝數量";
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
            // 說明
            // 
            this.說明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.說明.DataPropertyName = "itnote";
            this.說明.HeaderText = "說明";
            this.說明.MaxInputLength = 20;
            this.說明.Name = "說明";
            this.說明.ReadOnly = true;
            this.說明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.說明.Width = 173;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(7, 15);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(80, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "組件編號:";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(266, 15);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(80, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "組件名稱:";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(605, 15);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(80, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "用量總計:";
            // 
            // BoItNo
            // 
            this.BoItNo.AllowGrayBackColor = true;
            this.BoItNo.AllowResize = true;
            this.BoItNo.BackColor = System.Drawing.Color.Silver;
            this.BoItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BoItNo.Location = new System.Drawing.Point(93, 10);
            this.BoItNo.MaxLength = 20;
            this.BoItNo.Name = "BoItNo";
            this.BoItNo.oLen = 0;
            this.BoItNo.ReadOnly = true;
            this.BoItNo.Size = new System.Drawing.Size(167, 27);
            this.BoItNo.TabIndex = 3;
            this.BoItNo.TabStop = false;
            // 
            // BoItName
            // 
            this.BoItName.AllowGrayBackColor = true;
            this.BoItName.AllowResize = true;
            this.BoItName.BackColor = System.Drawing.Color.Silver;
            this.BoItName.Font = new System.Drawing.Font("細明體", 12F);
            this.BoItName.Location = new System.Drawing.Point(352, 10);
            this.BoItName.MaxLength = 30;
            this.BoItName.Name = "BoItName";
            this.BoItName.oLen = 0;
            this.BoItName.ReadOnly = true;
            this.BoItName.Size = new System.Drawing.Size(247, 27);
            this.BoItName.TabIndex = 4;
            this.BoItName.TabStop = false;
            // 
            // BoTotQty
            // 
            this.BoTotQty.AllowGrayBackColor = true;
            this.BoTotQty.AllowResize = true;
            this.BoTotQty.BackColor = System.Drawing.Color.Silver;
            this.BoTotQty.FirstNum = 10;
            this.BoTotQty.Font = new System.Drawing.Font("細明體", 12F);
            this.BoTotQty.LastNum = 0;
            this.BoTotQty.Location = new System.Drawing.Point(691, 10);
            this.BoTotQty.MarkThousand = false;
            this.BoTotQty.MaxLength = 14;
            this.BoTotQty.Name = "BoTotQty";
            this.BoTotQty.NullInput = false;
            this.BoTotQty.NullValue = "0";
            this.BoTotQty.oLen = 0;
            this.BoTotQty.ReadOnly = true;
            this.BoTotQty.Size = new System.Drawing.Size(119, 27);
            this.BoTotQty.TabIndex = 5;
            this.BoTotQty.TabStop = false;
            this.BoTotQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // gridItDesp
            // 
            this.gridItDesp.AutoSize = true;
            this.gridItDesp.BackColor = System.Drawing.SystemColors.Control;
            this.gridItDesp.Font = new System.Drawing.Font("細明體", 12F);
            this.gridItDesp.Location = new System.Drawing.Point(626, 580);
            this.gridItDesp.Name = "gridItDesp";
            this.gridItDesp.Size = new System.Drawing.Size(123, 43);
            this.gridItDesp.TabIndex = 83;
            this.gridItDesp.Text = "規格說明";
            this.gridItDesp.UseVisualStyleBackColor = true;
            this.gridItDesp.Click += new System.EventHandler(this.gridItDesp_Click);
            // 
            // FrmDraw_Bom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.gridExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.gridItDesp);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.gridAppend);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.BoItNo);
            this.Controls.Add(this.gridDelete);
            this.Controls.Add(this.BoItName);
            this.Controls.Add(this.BoTotQty);
            this.Controls.Add(this.gridPic);
            this.Controls.Add(this.gridStk);
            this.Controls.Add(this.gridInsert);
            this.Controls.Add(this.gridExit);
            this.Controls.Add(this.gridGetBomD);
            this.Name = "FrmDraw_Bom";
            this.Text = "組合組裝品建檔";
            this.Load += new System.EventHandler(this.FrmDraw_Bom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT BoItNo;
        private JE.MyControl.TextBoxT BoItName;
        private JE.MyControl.TextBoxNumberT BoTotQty;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT gridAppend;
        private JE.MyControl.ButtonSmallT gridDelete;
        private JE.MyControl.ButtonSmallT gridPic;
        private JE.MyControl.ButtonSmallT gridInsert;
        private JE.MyControl.ButtonSmallT gridStk;
        private JE.MyControl.ButtonSmallT gridGetBomD;
        private JE.MyControl.ButtonSmallT gridExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 標準用量;
        private JE.MyControl.DataGridViewTextNumberT 母件比例;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 說明;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.ButtonSmallT gridItDesp; 
    }
}