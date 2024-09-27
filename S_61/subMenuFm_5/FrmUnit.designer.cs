namespace S_61.SOther
{
    partial class FrmUnit
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
            this.Append = new JE.MyControl.ButtonSmallT();
            this.Delete = new JE.MyControl.ButtonSmallT();
            this.Get = new JE.MyControl.ButtonSmallT();
            this.itunit = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.Exit = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // Append
            // 
            this.Append.Font = new System.Drawing.Font("細明體", 12F);
            this.Append.Location = new System.Drawing.Point(180, 584);
            this.Append.Name = "Append";
            this.Append.Size = new System.Drawing.Size(158, 39);
            this.Append.TabIndex = 1;
            this.Append.Text = "F2:新增";
            this.Append.UseVisualStyleBackColor = true;
            this.Append.Click += new System.EventHandler(this.Append_Click);
            // 
            // Delete
            // 
            this.Delete.Font = new System.Drawing.Font("細明體", 12F);
            this.Delete.Location = new System.Drawing.Point(344, 584);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(158, 39);
            this.Delete.TabIndex = 2;
            this.Delete.Text = "F3:刪除";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Get
            // 
            this.Get.Font = new System.Drawing.Font("細明體", 12F);
            this.Get.Location = new System.Drawing.Point(508, 584);
            this.Get.Name = "Get";
            this.Get.Size = new System.Drawing.Size(158, 39);
            this.Get.TabIndex = 3;
            this.Get.Text = "F9:取回";
            this.Get.UseVisualStyleBackColor = true;
            this.Get.Click += new System.EventHandler(this.Get_Click);
            // 
            // itunit
            // 
            this.itunit.AllowGrayBackColor = false;
            this.itunit.AllowResize = true;
            this.itunit.Font = new System.Drawing.Font("細明體", 12F);
            this.itunit.Location = new System.Drawing.Point(368, 545);
            this.itunit.MaxLength = 4;
            this.itunit.Name = "itunit";
            this.itunit.oLen = 0;
            this.itunit.Size = new System.Drawing.Size(39, 27);
            this.itunit.TabIndex = 0;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(245, 550);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(32, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "...";
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("細明體", 12F);
            this.Exit.Location = new System.Drawing.Point(672, 584);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(158, 39);
            this.Exit.TabIndex = 4;
            this.Exit.Text = "F11:結束";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
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
            this.單位,
            this.unid});
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
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 529);
            this.dataGridViewT1.TabIndex = 1;
            this.dataGridViewT1.DoubleClick += new System.EventHandler(this.dataGridViewT1_DoubleClick);
            // 
            // 單位
            // 
            this.單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位.DataPropertyName = "itunit";
            this.單位.HeaderText = "單位";
            this.單位.MaxInputLength = 10;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 93;
            // 
            // unid
            // 
            this.unid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.unid.DataPropertyName = "unid";
            this.unid.HeaderText = "unid";
            this.unid.Name = "unid";
            this.unid.ReadOnly = true;
            this.unid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.unid.Visible = false;
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
            // FrmUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Exit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.itunit);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.Append);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Get);
            this.Controls.Add(this.Exit);
            this.Name = "FrmUnit";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmItUnit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.ButtonSmallT Append;
        private JE.MyControl.ButtonSmallT Delete;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT Get;
        private JE.MyControl.TextBoxT itunit;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.ButtonSmallT Exit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private System.Windows.Forms.DataGridViewTextBoxColumn unid;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}