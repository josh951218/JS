﻿namespace S_61.subMenuFm_3
{
    partial class FrmReceivd_Saleb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReceivd_Saleb));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExit = new JE.MyControl.ButtonT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.計價數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.計位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.稅前單價 = new JE.MyControl.DataGridViewTextNumberT();
            this.稅前金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.備註說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.panelBtnT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 1;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.產品編號,
            this.品名規格,
            this.數量,
            this.單位,
            this.計價數量,
            this.計位,
            this.稅前單價,
            this.稅前金額,
            this.備註說明});
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 538);
            this.dataGridViewT1.TabIndex = 2;
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
            // panelBtnT1
            // 
            this.panelBtnT1.Controls.Add(this.btnExit);
            this.panelBtnT1.Location = new System.Drawing.Point(466, 544);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(79, 79);
            this.panelBtnT1.TabIndex = 1;
            // 
            // 序號
            // 
            this.序號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.序號.DataPropertyName = "RecordNo";
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
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.數量.DataPropertyName = "Qty";
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
            this.單位.DataPropertyName = "ItUnit";
            this.單位.HeaderText = "單位";
            this.單位.MaxInputLength = 4;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 45;
            // 
            // 計價數量
            // 
            this.計價數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.計價數量.DataPropertyName = "pqty";
            this.計價數量.FirstNum = 0;
            this.計價數量.HeaderText = "計價數量";
            this.計價數量.LastNum = 0;
            this.計價數量.MarkThousand = false;
            this.計價數量.MaxInputLength = 11;
            this.計價數量.Name = "計價數量";
            this.計價數量.NullInput = false;
            this.計價數量.NullValue = "0";
            this.計價數量.ReadOnly = true;
            this.計價數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.計價數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.計價數量.Width = 101;
            // 
            // 計位
            // 
            this.計位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.計位.DataPropertyName = "punit";
            this.計位.HeaderText = "計位";
            this.計位.MaxInputLength = 4;
            this.計位.Name = "計位";
            this.計位.ReadOnly = true;
            this.計位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.計位.Width = 45;
            // 
            // 稅前單價
            // 
            this.稅前單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅前單價.DataPropertyName = "TaxPrice";
            this.稅前單價.FirstNum = 0;
            this.稅前單價.HeaderText = "稅前單價";
            this.稅前單價.LastNum = 0;
            this.稅前單價.MarkThousand = false;
            this.稅前單價.MaxInputLength = 11;
            this.稅前單價.Name = "稅前單價";
            this.稅前單價.NullInput = false;
            this.稅前單價.NullValue = "0";
            this.稅前單價.ReadOnly = true;
            this.稅前單價.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.稅前單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅前單價.Width = 101;
            // 
            // 稅前金額
            // 
            this.稅前金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅前金額.DataPropertyName = "Mny";
            this.稅前金額.FirstNum = 0;
            this.稅前金額.HeaderText = "稅前金額";
            this.稅前金額.LastNum = 0;
            this.稅前金額.MarkThousand = false;
            this.稅前金額.MaxInputLength = 16;
            this.稅前金額.Name = "稅前金額";
            this.稅前金額.NullInput = false;
            this.稅前金額.NullValue = "0";
            this.稅前金額.ReadOnly = true;
            this.稅前金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.稅前金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅前金額.Width = 141;
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
            // FrmReceivd_Saleb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "FrmReceivd_Saleb";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmReceivd_Saleb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.panelBtnT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private JE.MyControl.DataGridViewTextNumberT 數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 計價數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 計位;
        private JE.MyControl.DataGridViewTextNumberT 稅前單價;
        private JE.MyControl.DataGridViewTextNumberT 稅前金額;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註說明;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelBtnT panelBtnT1;
    }
}