namespace S_61.SOther
{
    partial class FrmSpecialGroup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單件售價 = new JE.MyControl.DataGridViewTextNumberT();
            this.數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.售價 = new JE.MyControl.DataGridViewTextNumberT();
            this.群組代碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridAppend = new JE.MyControl.ButtonSmallT();
            this.gridDelete = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelT1 = new JE.MyControl.PanelT();
            this.gridSave = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.panelT1.SuspendLayout();
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
            this.序號,
            this.單件售價,
            this.數量,
            this.售價,
            this.群組代碼});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.Location = new System.Drawing.Point(18, 18);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(761, 397);
            this.dataGridViewT1.TabIndex = 0;
            this.dataGridViewT1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewT1_CellValidating);
            // 
            // 序號
            // 
            this.序號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.序號.DataPropertyName = "序號";
            this.序號.HeaderText = "群組序號";
            this.序號.MaxInputLength = 10;
            this.序號.Name = "序號";
            this.序號.ReadOnly = true;
            this.序號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.序號.Width = 93;
            // 
            // 單件售價
            // 
            this.單件售價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單件售價.DataPropertyName = "singleprice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單件售價.DefaultCellStyle = dataGridViewCellStyle2;
            this.單件售價.FirstNum = 0;
            this.單件售價.HeaderText = "單件售價";
            this.單件售價.LastNum = 0;
            this.單件售價.MarkThousand = false;
            this.單件售價.MaxInputLength = 16;
            this.單件售價.Name = "單件售價";
            this.單件售價.NullInput = false;
            this.單件售價.NullValue = "0";
            this.單件售價.ReadOnly = true;
            this.單件售價.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單件售價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單件售價.Width = 141;
            // 
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.數量.DataPropertyName = "qty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.數量.DefaultCellStyle = dataGridViewCellStyle3;
            this.數量.FirstNum = 0;
            this.數量.HeaderText = "數量";
            this.數量.LastNum = 0;
            this.數量.MarkThousand = false;
            this.數量.MaxInputLength = 16;
            this.數量.Name = "數量";
            this.數量.NullInput = false;
            this.數量.NullValue = "0";
            this.數量.ReadOnly = true;
            this.數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.數量.Width = 141;
            // 
            // 售價
            // 
            this.售價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.售價.DataPropertyName = "price";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.售價.DefaultCellStyle = dataGridViewCellStyle4;
            this.售價.FirstNum = 0;
            this.售價.HeaderText = "售價";
            this.售價.LastNum = 0;
            this.售價.MarkThousand = false;
            this.售價.MaxInputLength = 16;
            this.售價.Name = "售價";
            this.售價.NullInput = false;
            this.售價.NullValue = "0";
            this.售價.ReadOnly = true;
            this.售價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.售價.Width = 141;
            // 
            // 群組代碼
            // 
            this.群組代碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.群組代碼.DataPropertyName = "groupid";
            this.群組代碼.HeaderText = "群組代碼";
            this.群組代碼.MaxInputLength = 10;
            this.群組代碼.Name = "群組代碼";
            this.群組代碼.ReadOnly = true;
            this.群組代碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.群組代碼.Width = 93;
            // 
            // gridAppend
            // 
            this.gridAppend.Font = new System.Drawing.Font("細明體", 12F);
            this.gridAppend.Location = new System.Drawing.Point(18, 421);
            this.gridAppend.Name = "gridAppend";
            this.gridAppend.Size = new System.Drawing.Size(148, 39);
            this.gridAppend.TabIndex = 1;
            this.gridAppend.Text = "新增";
            this.gridAppend.UseVisualStyleBackColor = true;
            this.gridAppend.Click += new System.EventHandler(this.gridAppend_Click);
            // 
            // gridDelete
            // 
            this.gridDelete.Font = new System.Drawing.Font("細明體", 12F);
            this.gridDelete.Location = new System.Drawing.Point(166, 421);
            this.gridDelete.Name = "gridDelete";
            this.gridDelete.Size = new System.Drawing.Size(148, 39);
            this.gridDelete.TabIndex = 2;
            this.gridDelete.Text = "刪除";
            this.gridDelete.UseVisualStyleBackColor = true;
            this.gridDelete.Click += new System.EventHandler(this.gridDelete_Click);
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
            // panelT1
            // 
            this.panelT1.Controls.Add(this.gridSave);
            this.panelT1.Controls.Add(this.dataGridViewT1);
            this.panelT1.Controls.Add(this.gridDelete);
            this.panelT1.Controls.Add(this.gridAppend);
            this.panelT1.Location = new System.Drawing.Point(110, 37);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(791, 478);
            this.panelT1.TabIndex = 0;
            // 
            // gridSave
            // 
            this.gridSave.Font = new System.Drawing.Font("細明體", 12F);
            this.gridSave.Location = new System.Drawing.Point(314, 421);
            this.gridSave.Name = "gridSave";
            this.gridSave.Size = new System.Drawing.Size(148, 39);
            this.gridSave.TabIndex = 3;
            this.gridSave.Text = "儲存";
            this.gridSave.UseVisualStyleBackColor = true;
            this.gridSave.Click += new System.EventHandler(this.gridSave_Click);
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(354, 584);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(148, 39);
            this.btnGet.TabIndex = 1;
            this.btnGet.Text = "F9:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(508, 584);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(148, 39);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "F4:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmSpecialGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "FrmSpecialGroup";
            this.Text = "混搭商品群組設定作業";
            this.Load += new System.EventHandler(this.FrmSpecialGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT gridAppend;
        private JE.MyControl.ButtonSmallT gridDelete;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.ButtonSmallT gridSave;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private JE.MyControl.DataGridViewTextNumberT 單件售價;
        private JE.MyControl.DataGridViewTextNumberT 數量;
        private JE.MyControl.DataGridViewTextNumberT 售價;
        private System.Windows.Forms.DataGridViewTextBoxColumn 群組代碼;
    }
}