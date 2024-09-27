namespace S_61.subMenuFm_1
{
    partial class FrmOrderToFordbrow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrderToFordbrow));
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.點選 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.訂單編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.訂單日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.業務人員 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.幣別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.訂單總額 = new JE.MyControl.DataGridViewTextNumberT();
            this.本幣總額 = new JE.MyControl.DataGridViewTextNumberT();
            this.備註 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allok = new JE.MyControl.ButtonSmallT();
            this.allcancel = new JE.MyControl.ButtonSmallT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnOk = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
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
            this.點選,
            this.訂單編號,
            this.訂單日期,
            this.客戶編號,
            this.客戶簡稱,
            this.業務人員,
            this.幣別,
            this.訂單總額,
            this.本幣總額,
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1009, 499);
            this.dataGridViewT1.TabIndex = 2;
            this.dataGridViewT1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellClick);
            // 
            // 點選
            // 
            this.點選.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.點選.DataPropertyName = "點選";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.點選.DefaultCellStyle = dataGridViewCellStyle2;
            this.點選.HeaderText = "點選";
            this.點選.MaxInputLength = 4;
            this.點選.Name = "點選";
            this.點選.ReadOnly = true;
            this.點選.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.點選.Width = 45;
            // 
            // 訂單編號
            // 
            this.訂單編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.訂單編號.DataPropertyName = "訂單編號";
            this.訂單編號.HeaderText = "訂單編號";
            this.訂單編號.MaxInputLength = 16;
            this.訂單編號.Name = "訂單編號";
            this.訂單編號.ReadOnly = true;
            this.訂單編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.訂單編號.Width = 141;
            // 
            // 訂單日期
            // 
            this.訂單日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.訂單日期.DataPropertyName = "訂單日期";
            this.訂單日期.HeaderText = "訂單日期";
            this.訂單日期.MaxInputLength = 10;
            this.訂單日期.Name = "訂單日期";
            this.訂單日期.ReadOnly = true;
            this.訂單日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.訂單日期.Width = 93;
            // 
            // 客戶編號
            // 
            this.客戶編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶編號.DataPropertyName = "客戶編號";
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
            this.客戶簡稱.DataPropertyName = "客戶簡稱";
            this.客戶簡稱.HeaderText = "客戶簡稱";
            this.客戶簡稱.MaxInputLength = 10;
            this.客戶簡稱.Name = "客戶簡稱";
            this.客戶簡稱.ReadOnly = true;
            this.客戶簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶簡稱.Width = 93;
            // 
            // 業務人員
            // 
            this.業務人員.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.業務人員.DataPropertyName = "業務人員";
            this.業務人員.HeaderText = "業務人員";
            this.業務人員.MaxInputLength = 10;
            this.業務人員.Name = "業務人員";
            this.業務人員.ReadOnly = true;
            this.業務人員.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.業務人員.Width = 93;
            // 
            // 幣別
            // 
            this.幣別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.幣別.DataPropertyName = "幣別";
            this.幣別.HeaderText = "幣別";
            this.幣別.MaxInputLength = 8;
            this.幣別.Name = "幣別";
            this.幣別.ReadOnly = true;
            this.幣別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.幣別.Width = 77;
            // 
            // 訂單總額
            // 
            this.訂單總額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.訂單總額.DataPropertyName = "訂單總額";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.訂單總額.DefaultCellStyle = dataGridViewCellStyle3;
            this.訂單總額.FirstNum = 0;
            this.訂單總額.HeaderText = "訂單總額";
            this.訂單總額.LastNum = 0;
            this.訂單總額.MarkThousand = false;
            this.訂單總額.MaxInputLength = 16;
            this.訂單總額.Name = "訂單總額";
            this.訂單總額.NullInput = false;
            this.訂單總額.NullValue = "0";
            this.訂單總額.ReadOnly = true;
            this.訂單總額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.訂單總額.Width = 141;
            // 
            // 本幣總額
            // 
            this.本幣總額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.本幣總額.DataPropertyName = "本幣總額";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.本幣總額.DefaultCellStyle = dataGridViewCellStyle4;
            this.本幣總額.FirstNum = 0;
            this.本幣總額.HeaderText = "本幣總額";
            this.本幣總額.LastNum = 0;
            this.本幣總額.MarkThousand = false;
            this.本幣總額.MaxInputLength = 16;
            this.本幣總額.Name = "本幣總額";
            this.本幣總額.NullInput = false;
            this.本幣總額.NullValue = "0";
            this.本幣總額.ReadOnly = true;
            this.本幣總額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.本幣總額.Width = 141;
            // 
            // 備註
            // 
            this.備註.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.備註.DataPropertyName = "備註";
            this.備註.HeaderText = "備註";
            this.備註.MaxInputLength = 60;
            this.備註.Name = "備註";
            this.備註.ReadOnly = true;
            this.備註.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.備註.Width = 493;
            // 
            // allok
            // 
            this.allok.Font = new System.Drawing.Font("細明體", 12F);
            this.allok.Location = new System.Drawing.Point(1, 504);
            this.allok.Name = "allok";
            this.allok.Size = new System.Drawing.Size(504, 35);
            this.allok.TabIndex = 3;
            this.allok.Text = "F2:全選";
            this.allok.UseVisualStyleBackColor = true;
            this.allok.Click += new System.EventHandler(this.btnBrowT1_Click);
            // 
            // allcancel
            // 
            this.allcancel.Font = new System.Drawing.Font("細明體", 12F);
            this.allcancel.Location = new System.Drawing.Point(505, 504);
            this.allcancel.Name = "allcancel";
            this.allcancel.Size = new System.Drawing.Size(504, 35);
            this.allcancel.TabIndex = 4;
            this.allcancel.Text = "F3:取消";
            this.allcancel.UseVisualStyleBackColor = true;
            this.allcancel.Click += new System.EventHandler(this.btnBrowT2_Click);
            // 
            // panelT1
            // 
            this.panelT1.AutoSize = true;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnOk);
            this.panelT1.Location = new System.Drawing.Point(431, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 82);
            this.panelT1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(69, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 2;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(0, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(69, 79);
            this.btnOk.TabIndex = 1;
            this.btnOk.UseDefaultSettings = true;
            this.btnOk.UseVisualStyleBackColor = false;
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
            // FrmOrderToFordbrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.allok);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.allcancel);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmOrderToFordbrow";
            this.Text = "訂單轉採購單";
            this.Load += new System.EventHandler(this.FrmOrderToFordbrow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT allok;
        private JE.MyControl.ButtonSmallT allcancel;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnOk;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 點選;
        private System.Windows.Forms.DataGridViewTextBoxColumn 訂單編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 訂單日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 業務人員;
        private System.Windows.Forms.DataGridViewTextBoxColumn 幣別;
        private JE.MyControl.DataGridViewTextNumberT 訂單總額;
        private JE.MyControl.DataGridViewTextNumberT 本幣總額;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註;
    }
}