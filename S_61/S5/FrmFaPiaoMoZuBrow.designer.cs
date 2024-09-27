namespace S_61.S5
{
    partial class FrmFaPiaoMoZuBrow
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
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.lblNo = new JE.MyControl.LabelT();
            this.lblName = new JE.MyControl.LabelT();
            this.ImNo = new JE.MyControl.TextBoxT();
            this.ImName = new JE.MyControl.TextBoxT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.btnQuery = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.發票模組編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票模組名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.起始發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.終止發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.目前發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
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
            this.發票模組編號,
            this.發票模組名稱,
            this.起始發票號碼,
            this.終止發票號碼,
            this.目前發票號碼});
            this.dataGridViewT1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
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
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 518);
            this.dataGridViewT1.TabIndex = 0;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            // 
            // lblNo
            // 
            this.lblNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNo.AutoSize = true;
            this.lblNo.BackColor = System.Drawing.Color.Transparent;
            this.lblNo.Font = new System.Drawing.Font("細明體", 12F);
            this.lblNo.Location = new System.Drawing.Point(270, 537);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(104, 16);
            this.lblNo.TabIndex = 0;
            this.lblNo.Text = "發票模組編號";
            this.lblNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("細明體", 12F);
            this.lblName.Location = new System.Drawing.Point(549, 537);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "發票模組名稱";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ImNo
            // 
            this.ImNo.AllowGrayBackColor = false;
            this.ImNo.AllowResize = true;
            this.ImNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ImNo.Location = new System.Drawing.Point(374, 534);
            this.ImNo.MaxLength = 10;
            this.ImNo.Name = "ImNo";
            this.ImNo.oLen = 0;
            this.ImNo.Size = new System.Drawing.Size(87, 27);
            this.ImNo.TabIndex = 1;
            this.ImNo.TextChanged += new System.EventHandler(this.StNo_TextChanged);
            // 
            // ImName
            // 
            this.ImName.AllowGrayBackColor = false;
            this.ImName.AllowResize = true;
            this.ImName.Font = new System.Drawing.Font("細明體", 12F);
            this.ImName.Location = new System.Drawing.Point(653, 534);
            this.ImName.MaxLength = 10;
            this.ImName.Name = "ImName";
            this.ImName.oLen = 0;
            this.ImName.Size = new System.Drawing.Size(87, 27);
            this.ImName.TabIndex = 2;
            this.ImName.TextChanged += new System.EventHandler(this.StNo_TextChanged);
            // 
            // statusStripT1
            // 
            this.statusStripT1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStripT1.Location = new System.Drawing.Point(0, 626);
            this.statusStripT1.Name = "statusStripT1";
            this.statusStripT1.Size = new System.Drawing.Size(1010, 22);
            this.statusStripT1.TabIndex = 0;
            this.statusStripT1.Text = "statusStrip1";
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(303, 579);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(135, 44);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "F6:字元查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(438, 579);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(135, 44);
            this.btnGet.TabIndex = 5;
            this.btnGet.Text = "F9:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(573, 579);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(135, 44);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "F11:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // 發票模組編號
            // 
            this.發票模組編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.發票模組編號.DataPropertyName = "ImNo";
            this.發票模組編號.HeaderText = "發票模組編號";
            this.發票模組編號.MaxInputLength = 15;
            this.發票模組編號.Name = "發票模組編號";
            this.發票模組編號.ReadOnly = true;
            this.發票模組編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.發票模組編號.Width = 133;
            // 
            // 發票模組名稱
            // 
            this.發票模組名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.發票模組名稱.DataPropertyName = "ImName";
            this.發票模組名稱.HeaderText = "發票模組名稱";
            this.發票模組名稱.MaxInputLength = 15;
            this.發票模組名稱.Name = "發票模組名稱";
            this.發票模組名稱.ReadOnly = true;
            this.發票模組名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.發票模組名稱.Width = 133;
            // 
            // 起始發票號碼
            // 
            this.起始發票號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.起始發票號碼.DataPropertyName = "invnoS";
            this.起始發票號碼.HeaderText = "起始發票號碼";
            this.起始發票號碼.MaxInputLength = 15;
            this.起始發票號碼.Name = "起始發票號碼";
            this.起始發票號碼.ReadOnly = true;
            this.起始發票號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.起始發票號碼.Width = 133;
            // 
            // 終止發票號碼
            // 
            this.終止發票號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.終止發票號碼.DataPropertyName = "invnoE";
            this.終止發票號碼.HeaderText = "終止發票號碼";
            this.終止發票號碼.MaxInputLength = 15;
            this.終止發票號碼.Name = "終止發票號碼";
            this.終止發票號碼.ReadOnly = true;
            this.終止發票號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.終止發票號碼.Width = 133;
            // 
            // 目前發票號碼
            // 
            this.目前發票號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.目前發票號碼.DataPropertyName = "invnoC";
            this.目前發票號碼.HeaderText = "目前發票號碼";
            this.目前發票號碼.MaxInputLength = 15;
            this.目前發票號碼.Name = "目前發票號碼";
            this.目前發票號碼.ReadOnly = true;
            this.目前發票號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.目前發票號碼.Width = 133;
            // 
            // FrmFaPiaoMoZuBrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.ImName);
            this.Controls.Add(this.ImNo);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "FrmFaPiaoMoZuBrow";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmStkRoomBrow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.LabelT lblNo;
        private JE.MyControl.LabelT lblName;
        private JE.MyControl.TextBoxT ImNo;
        private JE.MyControl.TextBoxT ImName;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.ButtonSmallT btnQuery;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票模組編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票模組名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 起始發票號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 終止發票號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 目前發票號碼;
    }
}