﻿namespace S_61.subMenuFm_4
{
    partial class FrmSaleInvInfod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSaleInvInfod));
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.rdFooter6 = new JE.MyControl.RadioT();
            this.rdFooter5 = new JE.MyControl.RadioT();
            this.rdFooter4 = new JE.MyControl.RadioT();
            this.rdFooter3 = new JE.MyControl.RadioT();
            this.rdFooter2 = new JE.MyControl.RadioT();
            this.rdFooter1 = new JE.MyControl.RadioT();
            this.Date1 = new JE.MyControl.TextBoxT();
            this.Date2 = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.發票日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.開立人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.進貨合計 = new JE.MyControl.DataGridViewTextNumberT();
            this.營業稅 = new JE.MyControl.DataGridViewTextNumberT();
            this.總計 = new JE.MyControl.DataGridViewTextNumberT();
            this.廠商簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.進貨日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxT3 = new JE.MyControl.GroupBoxT();
            this.rd3 = new JE.MyControl.RadioT();
            this.rd2 = new JE.MyControl.RadioT();
            this.rd1 = new JE.MyControl.RadioT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnExcel = new JE.MyControl.ButtonT();
            this.btnWord = new JE.MyControl.ButtonT();
            this.btnPreview = new JE.MyControl.ButtonT();
            this.btnPrint = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.groupBoxT1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.groupBoxT3.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.rdFooter6);
            this.groupBoxT1.Controls.Add(this.rdFooter5);
            this.groupBoxT1.Controls.Add(this.rdFooter4);
            this.groupBoxT1.Controls.Add(this.rdFooter3);
            this.groupBoxT1.Controls.Add(this.rdFooter2);
            this.groupBoxT1.Controls.Add(this.rdFooter1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(455, 466);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(555, 72);
            this.groupBoxT1.TabIndex = 4;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "單行註腳";
            // 
            // rdFooter6
            // 
            this.rdFooter6.AutoSize = true;
            this.rdFooter6.BackColor = System.Drawing.Color.Transparent;
            this.rdFooter6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdFooter6.Font = new System.Drawing.Font("細明體", 12F);
            this.rdFooter6.Location = new System.Drawing.Point(465, 33);
            this.rdFooter6.Name = "rdFooter6";
            this.rdFooter6.Size = new System.Drawing.Size(74, 20);
            this.rdFooter6.TabIndex = 6;
            this.rdFooter6.Tag = "不列印";
            this.rdFooter6.Text = "不列印";
            this.rdFooter6.UseVisualStyleBackColor = true;
            // 
            // rdFooter5
            // 
            this.rdFooter5.AutoSize = true;
            this.rdFooter5.BackColor = System.Drawing.Color.Transparent;
            this.rdFooter5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdFooter5.Font = new System.Drawing.Font("細明體", 12F);
            this.rdFooter5.Location = new System.Drawing.Point(375, 33);
            this.rdFooter5.Name = "rdFooter5";
            this.rdFooter5.Size = new System.Drawing.Size(74, 20);
            this.rdFooter5.TabIndex = 5;
            this.rdFooter5.Tag = "第五組";
            this.rdFooter5.Text = "第五組";
            this.rdFooter5.UseVisualStyleBackColor = true;
            // 
            // rdFooter4
            // 
            this.rdFooter4.AutoSize = true;
            this.rdFooter4.BackColor = System.Drawing.Color.Transparent;
            this.rdFooter4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdFooter4.Font = new System.Drawing.Font("細明體", 12F);
            this.rdFooter4.Location = new System.Drawing.Point(285, 33);
            this.rdFooter4.Name = "rdFooter4";
            this.rdFooter4.Size = new System.Drawing.Size(74, 20);
            this.rdFooter4.TabIndex = 4;
            this.rdFooter4.Tag = "第四組";
            this.rdFooter4.Text = "第四組";
            this.rdFooter4.UseVisualStyleBackColor = true;
            // 
            // rdFooter3
            // 
            this.rdFooter3.AutoSize = true;
            this.rdFooter3.BackColor = System.Drawing.Color.Transparent;
            this.rdFooter3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdFooter3.Font = new System.Drawing.Font("細明體", 12F);
            this.rdFooter3.Location = new System.Drawing.Point(195, 33);
            this.rdFooter3.Name = "rdFooter3";
            this.rdFooter3.Size = new System.Drawing.Size(74, 20);
            this.rdFooter3.TabIndex = 3;
            this.rdFooter3.Tag = "第三組";
            this.rdFooter3.Text = "第三組";
            this.rdFooter3.UseVisualStyleBackColor = true;
            // 
            // rdFooter2
            // 
            this.rdFooter2.AutoSize = true;
            this.rdFooter2.BackColor = System.Drawing.Color.Transparent;
            this.rdFooter2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdFooter2.Font = new System.Drawing.Font("細明體", 12F);
            this.rdFooter2.Location = new System.Drawing.Point(105, 33);
            this.rdFooter2.Name = "rdFooter2";
            this.rdFooter2.Size = new System.Drawing.Size(74, 20);
            this.rdFooter2.TabIndex = 2;
            this.rdFooter2.Tag = "第二組";
            this.rdFooter2.Text = "第二組";
            this.rdFooter2.UseVisualStyleBackColor = true;
            // 
            // rdFooter1
            // 
            this.rdFooter1.AutoSize = true;
            this.rdFooter1.BackColor = System.Drawing.Color.LightBlue;
            this.rdFooter1.Checked = true;
            this.rdFooter1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdFooter1.Font = new System.Drawing.Font("細明體", 12F);
            this.rdFooter1.Location = new System.Drawing.Point(15, 33);
            this.rdFooter1.Name = "rdFooter1";
            this.rdFooter1.Size = new System.Drawing.Size(74, 20);
            this.rdFooter1.TabIndex = 1;
            this.rdFooter1.Tag = "第一組";
            this.rdFooter1.Text = "第一組";
            this.rdFooter1.UseVisualStyleBackColor = false;
            // 
            // Date1
            // 
            this.Date1.AllowGrayBackColor = true;
            this.Date1.AllowResize = true;
            this.Date1.BackColor = System.Drawing.Color.Silver;
            this.Date1.Font = new System.Drawing.Font("細明體", 12F);
            this.Date1.Location = new System.Drawing.Point(85, 12);
            this.Date1.MaxLength = 8;
            this.Date1.Name = "Date1";
            this.Date1.oLen = 0;
            this.Date1.ReadOnly = true;
            this.Date1.Size = new System.Drawing.Size(71, 27);
            this.Date1.TabIndex = 0;
            this.Date1.TabStop = false;
            // 
            // Date2
            // 
            this.Date2.AllowGrayBackColor = true;
            this.Date2.AllowResize = true;
            this.Date2.BackColor = System.Drawing.Color.Silver;
            this.Date2.Font = new System.Drawing.Font("細明體", 12F);
            this.Date2.Location = new System.Drawing.Point(192, 12);
            this.Date2.MaxLength = 8;
            this.Date2.Name = "Date2";
            this.Date2.oLen = 0;
            this.Date2.ReadOnly = true;
            this.Date2.Size = new System.Drawing.Size(71, 27);
            this.Date2.TabIndex = 1;
            this.Date2.TabStop = false;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(7, 17);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "發票日期";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(162, 17);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(24, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "～";
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
            this.發票日,
            this.發票號碼,
            this.開立人,
            this.進貨合計,
            this.營業稅,
            this.總計,
            this.廠商簡稱,
            this.進貨日期});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 45);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 413);
            this.dataGridViewT1.TabIndex = 2;
            // 
            // 發票日
            // 
            this.發票日.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.發票日.HeaderText = "發票日";
            this.發票日.MaxInputLength = 10;
            this.發票日.Name = "發票日";
            this.發票日.ReadOnly = true;
            this.發票日.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.發票日.Width = 93;
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
            // 開立人
            // 
            this.開立人.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.開立人.DataPropertyName = "invname";
            this.開立人.HeaderText = "開立人";
            this.開立人.MaxInputLength = 50;
            this.開立人.Name = "開立人";
            this.開立人.ReadOnly = true;
            this.開立人.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.開立人.Width = 413;
            // 
            // 進貨合計
            // 
            this.進貨合計.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.進貨合計.DataPropertyName = "taxmnyb";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.進貨合計.DefaultCellStyle = dataGridViewCellStyle2;
            this.進貨合計.FirstNum = 0;
            this.進貨合計.HeaderText = "進貨合計";
            this.進貨合計.LastNum = 0;
            this.進貨合計.MarkThousand = false;
            this.進貨合計.MaxInputLength = 16;
            this.進貨合計.Name = "進貨合計";
            this.進貨合計.NullInput = false;
            this.進貨合計.NullValue = "0";
            this.進貨合計.ReadOnly = true;
            this.進貨合計.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.進貨合計.Width = 141;
            // 
            // 營業稅
            // 
            this.營業稅.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.營業稅.DataPropertyName = "taxb";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.營業稅.DefaultCellStyle = dataGridViewCellStyle3;
            this.營業稅.FirstNum = 0;
            this.營業稅.HeaderText = "營業稅";
            this.營業稅.LastNum = 0;
            this.營業稅.MarkThousand = false;
            this.營業稅.MaxInputLength = 16;
            this.營業稅.Name = "營業稅";
            this.營業稅.NullInput = false;
            this.營業稅.NullValue = "0";
            this.營業稅.ReadOnly = true;
            this.營業稅.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.營業稅.Width = 141;
            // 
            // 總計
            // 
            this.總計.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.總計.DataPropertyName = "totmnyb";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.總計.DefaultCellStyle = dataGridViewCellStyle4;
            this.總計.FirstNum = 0;
            this.總計.HeaderText = "總計";
            this.總計.LastNum = 0;
            this.總計.MarkThousand = false;
            this.總計.MaxInputLength = 16;
            this.總計.Name = "總計";
            this.總計.NullInput = false;
            this.總計.NullValue = "0";
            this.總計.ReadOnly = true;
            this.總計.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.總計.Width = 141;
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
            // groupBoxT3
            // 
            this.groupBoxT3.Controls.Add(this.rd3);
            this.groupBoxT3.Controls.Add(this.rd2);
            this.groupBoxT3.Controls.Add(this.rd1);
            this.groupBoxT3.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT3.Location = new System.Drawing.Point(0, 466);
            this.groupBoxT3.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT3.Name = "groupBoxT3";
            this.groupBoxT3.Size = new System.Drawing.Size(447, 72);
            this.groupBoxT3.TabIndex = 3;
            this.groupBoxT3.TabStop = false;
            this.groupBoxT3.Text = "報表內容";
            // 
            // rd3
            // 
            this.rd3.AutoSize = true;
            this.rd3.BackColor = System.Drawing.Color.Transparent;
            this.rd3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd3.Font = new System.Drawing.Font("細明體", 12F);
            this.rd3.Location = new System.Drawing.Point(316, 33);
            this.rd3.Name = "rd3";
            this.rd3.Size = new System.Drawing.Size(74, 20);
            this.rd3.TabIndex = 3;
            this.rd3.Tag = "自定二";
            this.rd3.Text = "自定二";
            this.rd3.UseVisualStyleBackColor = true;
            // 
            // rd2
            // 
            this.rd2.AutoSize = true;
            this.rd2.BackColor = System.Drawing.Color.Transparent;
            this.rd2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd2.Font = new System.Drawing.Font("細明體", 12F);
            this.rd2.Location = new System.Drawing.Point(194, 33);
            this.rd2.Name = "rd2";
            this.rd2.Size = new System.Drawing.Size(74, 20);
            this.rd2.TabIndex = 2;
            this.rd2.Tag = "自定一";
            this.rd2.Text = "自定一";
            this.rd2.UseVisualStyleBackColor = true;
            // 
            // rd1
            // 
            this.rd1.AutoSize = true;
            this.rd1.BackColor = System.Drawing.Color.LightBlue;
            this.rd1.Checked = true;
            this.rd1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd1.Font = new System.Drawing.Font("細明體", 12F);
            this.rd1.Location = new System.Drawing.Point(56, 33);
            this.rd1.Name = "rd1";
            this.rd1.Size = new System.Drawing.Size(90, 20);
            this.rd1.TabIndex = 1;
            this.rd1.Tag = "標準報表";
            this.rd1.Text = "標準報表";
            this.rd1.UseVisualStyleBackColor = false;
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnExcel);
            this.panelT1.Controls.Add(this.btnWord);
            this.panelT1.Controls.Add(this.btnPreview);
            this.panelT1.Controls.Add(this.btnPrint);
            this.panelT1.Location = new System.Drawing.Point(328, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(355, 79);
            this.panelT1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 9F);
            this.btnExit.Location = new System.Drawing.Point(276, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 12;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.SystemColors.Control;
            this.btnExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExcel.BackgroundImage")));
            this.btnExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("細明體", 9F);
            this.btnExcel.Location = new System.Drawing.Point(207, 0);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(69, 79);
            this.btnExcel.TabIndex = 11;
            this.btnExcel.UseDefaultSettings = false;
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnWord
            // 
            this.btnWord.BackColor = System.Drawing.SystemColors.Control;
            this.btnWord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWord.BackgroundImage")));
            this.btnWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWord.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWord.Font = new System.Drawing.Font("細明體", 9F);
            this.btnWord.Location = new System.Drawing.Point(138, 0);
            this.btnWord.Name = "btnWord";
            this.btnWord.Size = new System.Drawing.Size(69, 79);
            this.btnWord.TabIndex = 10;
            this.btnWord.UseDefaultSettings = false;
            this.btnWord.UseVisualStyleBackColor = false;
            this.btnWord.Click += new System.EventHandler(this.btnWord_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.SystemColors.Control;
            this.btnPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreview.BackgroundImage")));
            this.btnPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPreview.Font = new System.Drawing.Font("細明體", 9F);
            this.btnPreview.Location = new System.Drawing.Point(69, 0);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(69, 79);
            this.btnPreview.TabIndex = 9;
            this.btnPreview.UseDefaultSettings = false;
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Font = new System.Drawing.Font("細明體", 9F);
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(69, 79);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.UseDefaultSettings = false;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            // FrmSaleInvInfod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.Date1);
            this.Controls.Add(this.Date2);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.groupBoxT3);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmSaleInvInfod";
            this.Text = "進項發票瀏覽明細表";
            this.Load += new System.EventHandler(this.FrmSaleInvInfod_Load);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.groupBoxT3.ResumeLayout(false);
            this.groupBoxT3.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT rdFooter6;
        private JE.MyControl.RadioT rdFooter5;
        private JE.MyControl.RadioT rdFooter4;
        private JE.MyControl.RadioT rdFooter3;
        private JE.MyControl.RadioT rdFooter2;
        private JE.MyControl.RadioT rdFooter1;
        public JE.MyControl.TextBoxT Date1;
        public JE.MyControl.TextBoxT Date2;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.GroupBoxT groupBoxT3;
        private JE.MyControl.RadioT rd3;
        private JE.MyControl.RadioT rd2;
        private JE.MyControl.RadioT rd1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnExcel;
        private JE.MyControl.ButtonT btnWord;
        private JE.MyControl.ButtonT btnPreview;
        private JE.MyControl.ButtonT btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票日;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 開立人;
        private JE.MyControl.DataGridViewTextNumberT 進貨合計;
        private JE.MyControl.DataGridViewTextNumberT 營業稅;
        private JE.MyControl.DataGridViewTextNumberT 總計;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 進貨日期;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}