namespace S_61.S2
{
    partial class Frm進價查詢
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
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.廠商簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.進貨日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單價 = new JE.MyControl.DataGridViewTextNumberT();
            this.折數 = new JE.MyControl.DataGridViewTextNumberT();
            this.稅前單價 = new JE.MyControl.DataGridViewTextNumberT();
            this.實際成本 = new JE.MyControl.DataGridViewTextNumberT();
            this.備註說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStripT1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStripT1
            // 
            this.statusStripT1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStripT1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStripT1.Location = new System.Drawing.Point(0, 623);
            this.statusStripT1.Name = "statusStripT1";
            this.statusStripT1.Size = new System.Drawing.Size(1010, 25);
            this.statusStripT1.TabIndex = 0;
            this.statusStripT1.Text = "statusStripT1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(105, 20);
            this.toolStripStatusLabel1.Text = "產品進價查詢";
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
            this.廠商簡稱,
            this.進貨日期,
            this.產品編號,
            this.品名規格,
            this.數量,
            this.單位,
            this.單價,
            this.折數,
            this.稅前單價,
            this.實際成本,
            this.備註說明});
            this.dataGridViewT1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 623);
            this.dataGridViewT1.TabIndex = 1;
            // 
            // 廠商簡稱
            // 
            this.廠商簡稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.廠商簡稱.DataPropertyName = "faname1";
            this.廠商簡稱.HeaderText = "廠商簡稱";
            this.廠商簡稱.MaxInputLength = 10;
            this.廠商簡稱.Name = "廠商簡稱";
            this.廠商簡稱.ReadOnly = true;
            this.廠商簡稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.廠商簡稱.Width = 93;
            // 
            // 進貨日期
            // 
            this.進貨日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.進貨日期.HeaderText = "進貨日期";
            this.進貨日期.MaxInputLength = 10;
            this.進貨日期.Name = "進貨日期";
            this.進貨日期.ReadOnly = true;
            this.進貨日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.進貨日期.Width = 93;
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
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.數量.DataPropertyName = "qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.數量.FirstNum = 0;
            this.數量.HeaderText = "數量";
            this.數量.LastNum = 0;
            this.數量.MarkThousand = false;
            this.數量.MaxInputLength = 11;
            this.數量.Name = "數量";
            this.數量.NullInput = false;
            this.數量.NullValue = "0";
            this.數量.ReadOnly = true;
            this.數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.數量.Width = 101;
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
            // 單價
            // 
            this.單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單價.DataPropertyName = "price";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單價.DefaultCellStyle = dataGridViewCellStyle3;
            this.單價.FirstNum = 0;
            this.單價.HeaderText = "單價";
            this.單價.LastNum = 0;
            this.單價.MarkThousand = false;
            this.單價.MaxInputLength = 11;
            this.單價.Name = "單價";
            this.單價.NullInput = false;
            this.單價.NullValue = "0";
            this.單價.ReadOnly = true;
            this.單價.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單價.Width = 101;
            // 
            // 折數
            // 
            this.折數.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折數.DataPropertyName = "prs";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.折數.DefaultCellStyle = dataGridViewCellStyle4;
            this.折數.FirstNum = 0;
            this.折數.HeaderText = "折數";
            this.折數.LastNum = 0;
            this.折數.MarkThousand = false;
            this.折數.MaxInputLength = 5;
            this.折數.Name = "折數";
            this.折數.NullInput = false;
            this.折數.NullValue = "0";
            this.折數.ReadOnly = true;
            this.折數.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.折數.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.折數.Width = 53;
            // 
            // 稅前單價
            // 
            this.稅前單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅前單價.DataPropertyName = "taxprice";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.稅前單價.DefaultCellStyle = dataGridViewCellStyle5;
            this.稅前單價.FirstNum = 0;
            this.稅前單價.HeaderText = "稅前單價";
            this.稅前單價.LastNum = 0;
            this.稅前單價.MarkThousand = false;
            this.稅前單價.MaxInputLength = 16;
            this.稅前單價.Name = "稅前單價";
            this.稅前單價.NullInput = false;
            this.稅前單價.NullValue = "0";
            this.稅前單價.ReadOnly = true;
            this.稅前單價.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.稅前單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅前單價.Width = 141;
            // 
            // 實際成本
            // 
            this.實際成本.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.實際成本.DataPropertyName = "realcost";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.實際成本.DefaultCellStyle = dataGridViewCellStyle6;
            this.實際成本.FirstNum = 0;
            this.實際成本.HeaderText = "實際成本";
            this.實際成本.LastNum = 0;
            this.實際成本.MarkThousand = false;
            this.實際成本.MaxInputLength = 11;
            this.實際成本.Name = "實際成本";
            this.實際成本.NullInput = false;
            this.實際成本.NullValue = "0";
            this.實際成本.ReadOnly = true;
            this.實際成本.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.實際成本.Width = 101;
            // 
            // 備註說明
            // 
            this.備註說明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.備註說明.DataPropertyName = "memo";
            this.備註說明.HeaderText = "備註說明";
            this.備註說明.MaxInputLength = 20;
            this.備註說明.Name = "備註說明";
            this.備註說明.ReadOnly = true;
            this.備註說明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.備註說明.Width = 173;
            // 
            // Frm進價查詢
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "Frm進價查詢";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.Frm進貨查詢_Load);
            this.statusStripT1.ResumeLayout(false);
            this.statusStripT1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 進貨日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private JE.MyControl.DataGridViewTextNumberT 數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 單價;
        private JE.MyControl.DataGridViewTextNumberT 折數;
        private JE.MyControl.DataGridViewTextNumberT 稅前單價;
        private JE.MyControl.DataGridViewTextNumberT 實際成本;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註說明;
    }
}