namespace S_61.電子發票
{
    partial class ErrorInvoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.buttonSmallT1 = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.公司名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.公司統編 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.買方統編 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.稅別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.稅前小計 = new JE.MyControl.DataGridViewTextNumberT();
            this.營業稅 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.總金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.媒體申報類別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票隨機碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.statusStripT1.SuspendLayout();
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
            this.公司名稱,
            this.公司統編,
            this.發票號碼,
            this.發票日期,
            this.買方統編,
            this.稅別,
            this.稅前小計,
            this.營業稅,
            this.總金額,
            this.媒體申報類別,
            this.發票隨機碼});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 1);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 571);
            this.dataGridViewT1.TabIndex = 4;
            // 
            // buttonSmallT1
            // 
            this.buttonSmallT1.Font = new System.Drawing.Font("細明體", 12F);
            this.buttonSmallT1.Location = new System.Drawing.Point(444, 578);
            this.buttonSmallT1.Name = "buttonSmallT1";
            this.buttonSmallT1.Size = new System.Drawing.Size(122, 45);
            this.buttonSmallT1.TabIndex = 5;
            this.buttonSmallT1.Text = "結束";
            this.buttonSmallT1.UseVisualStyleBackColor = true;
            this.buttonSmallT1.Click += new System.EventHandler(this.buttonSmallT1_Click);
            // 
            // statusStripT1
            // 
            this.statusStripT1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStripT1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStripT1.Location = new System.Drawing.Point(0, 626);
            this.statusStripT1.Name = "statusStripT1";
            this.statusStripT1.Size = new System.Drawing.Size(1010, 22);
            this.statusStripT1.TabIndex = 0;
            this.statusStripT1.Text = "statusStripT1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // 公司名稱
            // 
            this.公司名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.公司名稱.DataPropertyName = "coname1";
            this.公司名稱.HeaderText = "公司名稱";
            this.公司名稱.MaxInputLength = 20;
            this.公司名稱.Name = "公司名稱";
            this.公司名稱.ReadOnly = true;
            this.公司名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.公司名稱.Width = 173;
            // 
            // 公司統編
            // 
            this.公司統編.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.公司統編.DataPropertyName = "couno";
            this.公司統編.HeaderText = "公司統編";
            this.公司統編.MaxInputLength = 8;
            this.公司統編.Name = "公司統編";
            this.公司統編.ReadOnly = true;
            this.公司統編.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.公司統編.Width = 77;
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
            // 發票日期
            // 
            this.發票日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.發票日期.DataPropertyName = "invdate1";
            this.發票日期.HeaderText = "發票日期";
            this.發票日期.MaxInputLength = 10;
            this.發票日期.Name = "發票日期";
            this.發票日期.ReadOnly = true;
            this.發票日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.發票日期.Width = 93;
            // 
            // 買方統編
            // 
            this.買方統編.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.買方統編.DataPropertyName = "invtaxno";
            this.買方統編.HeaderText = "買方統編";
            this.買方統編.MaxInputLength = 8;
            this.買方統編.Name = "買方統編";
            this.買方統編.ReadOnly = true;
            this.買方統編.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.買方統編.Width = 77;
            // 
            // 稅別
            // 
            this.稅別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅別.DataPropertyName = "x3no";
            this.稅別.HeaderText = "稅別";
            this.稅別.MaxInputLength = 6;
            this.稅別.Name = "稅別";
            this.稅別.ReadOnly = true;
            this.稅別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅別.Width = 61;
            // 
            // 稅前小計
            // 
            this.稅前小計.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅前小計.DataPropertyName = "taxmnyb";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.稅前小計.DefaultCellStyle = dataGridViewCellStyle2;
            this.稅前小計.FirstNum = 0;
            this.稅前小計.HeaderText = "稅前小計";
            this.稅前小計.LastNum = 0;
            this.稅前小計.MarkThousand = false;
            this.稅前小計.MaxInputLength = 16;
            this.稅前小計.Name = "稅前小計";
            this.稅前小計.NullInput = false;
            this.稅前小計.NullValue = "0";
            this.稅前小計.ReadOnly = true;
            this.稅前小計.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅前小計.Width = 141;
            // 
            // 營業稅
            // 
            this.營業稅.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.營業稅.DataPropertyName = "taxb";
            this.營業稅.HeaderText = "營業稅";
            this.營業稅.MaxInputLength = 16;
            this.營業稅.Name = "營業稅";
            this.營業稅.ReadOnly = true;
            this.營業稅.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.營業稅.Width = 141;
            // 
            // 總金額
            // 
            this.總金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.總金額.DataPropertyName = "totmnyb";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.總金額.DefaultCellStyle = dataGridViewCellStyle3;
            this.總金額.FirstNum = 0;
            this.總金額.HeaderText = "總金額";
            this.總金額.LastNum = 0;
            this.總金額.MarkThousand = false;
            this.總金額.MaxInputLength = 16;
            this.總金額.Name = "總金額";
            this.總金額.NullInput = false;
            this.總金額.NullValue = "0";
            this.總金額.ReadOnly = true;
            this.總金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.總金額.Width = 141;
            // 
            // 媒體申報類別
            // 
            this.媒體申報類別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.媒體申報類別.DataPropertyName = "invkind";
            this.媒體申報類別.HeaderText = "媒體申報類別";
            this.媒體申報類別.MaxInputLength = 20;
            this.媒體申報類別.Name = "媒體申報類別";
            this.媒體申報類別.ReadOnly = true;
            this.媒體申報類別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.媒體申報類別.Width = 173;
            // 
            // 發票隨機碼
            // 
            this.發票隨機碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.發票隨機碼.DataPropertyName = "invrandom";
            this.發票隨機碼.HeaderText = "發票隨機碼";
            this.發票隨機碼.MaxInputLength = 10;
            this.發票隨機碼.Name = "發票隨機碼";
            this.發票隨機碼.ReadOnly = true;
            this.發票隨機碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.發票隨機碼.Width = 93;
            // 
            // ErrorInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonSmallT1;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.buttonSmallT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "ErrorInvoice";
            this.Text = "錯誤發票明細";
            this.Load += new System.EventHandler(this.ErrorInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.statusStripT1.ResumeLayout(false);
            this.statusStripT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT buttonSmallT1;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司統編;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 買方統編;
        private System.Windows.Forms.DataGridViewTextBoxColumn 稅別;
        private JE.MyControl.DataGridViewTextNumberT 稅前小計;
        private System.Windows.Forms.DataGridViewTextBoxColumn 營業稅;
        private JE.MyControl.DataGridViewTextNumberT 總金額;
        private System.Windows.Forms.DataGridViewTextBoxColumn 媒體申報類別;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票隨機碼;

    }
}