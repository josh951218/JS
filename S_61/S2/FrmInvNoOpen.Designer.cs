namespace S_61.S2
{
    partial class FrmInvNoOpen
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
            this.發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.公司編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.公司名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.備註 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.ttt = new System.Windows.Forms.CheckBox();
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
            this.發票號碼,
            this.發票日期,
            this.公司編號,
            this.公司名稱,
            this.客戶編號,
            this.客戶簡稱,
            this.備註});
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 547);
            this.dataGridViewT1.TabIndex = 0;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            // 
            // 發票號碼
            // 
            this.發票號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.發票號碼.DataPropertyName = "inno";
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
            this.發票日期.HeaderText = "發票日期";
            this.發票日期.MaxInputLength = 8;
            this.發票日期.Name = "發票日期";
            this.發票日期.ReadOnly = true;
            this.發票日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.發票日期.Width = 77;
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
            // 公司名稱
            // 
            this.公司名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.公司名稱.DataPropertyName = "coname1";
            this.公司名稱.HeaderText = "公司名稱";
            this.公司名稱.MaxInputLength = 10;
            this.公司名稱.Name = "公司名稱";
            this.公司名稱.ReadOnly = true;
            this.公司名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.公司名稱.Width = 93;
            // 
            // 客戶編號
            // 
            this.客戶編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶編號.DataPropertyName = "cuno";
            this.客戶編號.HeaderText = "客戶編號";
            this.客戶編號.MaxInputLength = 10;
            this.客戶編號.Name = "客戶編號";
            this.客戶編號.ReadOnly = true;
            this.客戶編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶編號.Width = 93;
            // 
            // 客戶簡稱
            // 
            this.客戶簡稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶簡稱.DataPropertyName = "cuname1";
            this.客戶簡稱.HeaderText = "客戶簡稱";
            this.客戶簡稱.MaxInputLength = 10;
            this.客戶簡稱.Name = "客戶簡稱";
            this.客戶簡稱.ReadOnly = true;
            this.客戶簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶簡稱.Width = 93;
            // 
            // 備註
            // 
            this.備註.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.備註.DataPropertyName = "inmemo";
            this.備註.HeaderText = "備註";
            this.備註.MaxInputLength = 60;
            this.備註.Name = "備註";
            this.備註.ReadOnly = true;
            this.備註.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.備註.Width = 493;
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(344, 568);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(157, 46);
            this.btnGet.TabIndex = 1;
            this.btnGet.Text = "取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(509, 568);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(157, 46);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "取消";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            // ttt
            // 
            this.ttt.Location = new System.Drawing.Point(0, 0);
            this.ttt.Name = "ttt";
            this.ttt.Size = new System.Drawing.Size(104, 24);
            this.ttt.TabIndex = 0;
            this.ttt.Text = "dfjdsklfj;ds";
            // 
            // FrmInvNoOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "FrmInvNoOpen";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmInvNoOpen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 公司名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註;
        private System.Windows.Forms.CheckBox ttt;
    }
}