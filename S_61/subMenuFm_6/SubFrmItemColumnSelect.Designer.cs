﻿namespace S_61.subMenuFm_6
{
    partial class SubFrmItemColumnSelect
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
            this.欄位名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.欄位說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewT1
            // 
            this.dataGridViewT1.AllowUserToAddRows = false;
            this.dataGridViewT1.AllowUserToDeleteRows = false;
            this.dataGridViewT1.AllowUserToOrderColumns = true;
            this.dataGridViewT1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
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
            this.欄位名稱,
            this.欄位說明});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(2, 1);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1008, 584);
            this.dataGridViewT1.TabIndex = 12;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            // 
            // 欄位名稱
            // 
            this.欄位名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.欄位名稱.DataPropertyName = "欄位名稱";
            this.欄位名稱.HeaderText = "欄位名稱";
            this.欄位名稱.MaxInputLength = 61;
            this.欄位名稱.Name = "欄位名稱";
            this.欄位名稱.ReadOnly = true;
            this.欄位名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.欄位名稱.Width = 501;
            // 
            // 欄位說明
            // 
            this.欄位說明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.欄位說明.DataPropertyName = "欄位說明";
            this.欄位說明.HeaderText = "欄位說明";
            this.欄位說明.MaxInputLength = 61;
            this.欄位說明.Name = "欄位說明";
            this.欄位說明.ReadOnly = true;
            this.欄位說明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.欄位說明.Width = 501;
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(326, 596);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(178, 40);
            this.btnGet.TabIndex = 13;
            this.btnGet.Text = "F9:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(507, 596);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(178, 40);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "F11:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // SubFrmItemColumnSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "SubFrmItemColumnSelect";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.SubFrmItemColumnSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 欄位名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 欄位說明;
    }
}