namespace S_61.subMenuFm_3
{
    partial class 公司開窗
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
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.公司編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.公司簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.公司名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblT1 = new JE.MyControl.LabelT();
            this.CoNo = new JE.MyControl.TextBoxT();
            this.Get = new JE.MyControl.ButtonSmallT();
            this.Exit = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
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
            this.公司編號,
            this.公司簡稱,
            this.公司名稱});
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 553);
            this.dataGridViewT1.TabIndex = 4;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            // 
            // 公司編號
            // 
            this.公司編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.公司編號.DataPropertyName = "cono";
            this.公司編號.HeaderText = "公司編號";
            this.公司編號.MaxInputLength = 10;
            this.公司編號.Name = "公司編號";
            this.公司編號.ReadOnly = true;
            this.公司編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.公司編號.Width = 93;
            // 
            // 公司簡稱
            // 
            this.公司簡稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.公司簡稱.DataPropertyName = "coname1";
            this.公司簡稱.HeaderText = "公司簡稱";
            this.公司簡稱.MaxInputLength = 10;
            this.公司簡稱.Name = "公司簡稱";
            this.公司簡稱.ReadOnly = true;
            this.公司簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.公司簡稱.Width = 93;
            // 
            // 公司名稱
            // 
            this.公司名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.公司名稱.DataPropertyName = "coname2";
            this.公司名稱.HeaderText = "公司名稱";
            this.公司名稱.MaxInputLength = 50;
            this.公司名稱.Name = "公司名稱";
            this.公司名稱.ReadOnly = true;
            this.公司名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.公司名稱.Width = 413;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(237, 582);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "公司編號";
            // 
            // CoNo
            // 
            this.CoNo.AllowGrayBackColor = false;
            this.CoNo.AllowResize = true;
            this.CoNo.Font = new System.Drawing.Font("細明體", 12F);
            this.CoNo.Location = new System.Drawing.Point(315, 577);
            this.CoNo.MaxLength = 2;
            this.CoNo.Name = "CoNo";
            this.CoNo.oLen = 0;
            this.CoNo.Size = new System.Drawing.Size(23, 27);
            this.CoNo.TabIndex = 1;
            this.CoNo.TextChanged += new System.EventHandler(this.CoNo_TextChanged);
            // 
            // Get
            // 
            this.Get.Font = new System.Drawing.Font("細明體", 12F);
            this.Get.Location = new System.Drawing.Point(386, 567);
            this.Get.Name = "Get";
            this.Get.Size = new System.Drawing.Size(191, 47);
            this.Get.TabIndex = 2;
            this.Get.Text = "F9:取回";
            this.Get.UseVisualStyleBackColor = true;
            this.Get.Click += new System.EventHandler(this.Get_Click);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("細明體", 12F);
            this.Exit.Location = new System.Drawing.Point(583, 567);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(191, 47);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "F11:結束";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
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
            // 公司開窗
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Exit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.Get);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.CoNo);
            this.Controls.Add(this.statusStripT1);
            this.Name = "公司開窗";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.公司開窗_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.TextBoxT CoNo;
        private JE.MyControl.ButtonSmallT Get;
        private JE.MyControl.ButtonSmallT Exit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司名稱;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}