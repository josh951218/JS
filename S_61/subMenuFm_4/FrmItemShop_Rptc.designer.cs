namespace S_61.subMenuFm_4
{
    partial class FrmItemShop_Rptc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItemShop_Rptc));
            this.lblT3 = new JE.MyControl.LabelT();
            this.TotMnyb = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.TotB = new JE.MyControl.TextBoxT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.進貨數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.進貨淨額 = new JE.MyControl.DataGridViewTextNumberT();
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.rdFooter6 = new JE.MyControl.RadioT();
            this.rdFooter5 = new JE.MyControl.RadioT();
            this.rdFooter4 = new JE.MyControl.RadioT();
            this.rdFooter3 = new JE.MyControl.RadioT();
            this.rdFooter2 = new JE.MyControl.RadioT();
            this.rdFooter1 = new JE.MyControl.RadioT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnExcel = new JE.MyControl.ButtonT();
            this.btnWord = new JE.MyControl.ButtonT();
            this.btnPreView = new JE.MyControl.ButtonT();
            this.btnPrint = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelNT1 = new JE.MyControl.PanelNT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.groupBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.panelNT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(733, 11);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(88, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "進貨總淨額";
            // 
            // TotMnyb
            // 
            this.TotMnyb.AllowGrayBackColor = true;
            this.TotMnyb.AllowResize = true;
            this.TotMnyb.BackColor = System.Drawing.Color.Silver;
            this.TotMnyb.Font = new System.Drawing.Font("細明體", 12F);
            this.TotMnyb.Location = new System.Drawing.Point(829, 6);
            this.TotMnyb.MaxLength = 20;
            this.TotMnyb.Name = "TotMnyb";
            this.TotMnyb.oLen = 0;
            this.TotMnyb.ReadOnly = true;
            this.TotMnyb.Size = new System.Drawing.Size(167, 27);
            this.TotMnyb.TabIndex = 5;
            this.TotMnyb.TabStop = false;
            this.TotMnyb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(15, 11);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(88, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "進貨總數量";
            // 
            // TotB
            // 
            this.TotB.AllowGrayBackColor = true;
            this.TotB.AllowResize = true;
            this.TotB.BackColor = System.Drawing.Color.Silver;
            this.TotB.Font = new System.Drawing.Font("細明體", 12F);
            this.TotB.Location = new System.Drawing.Point(111, 6);
            this.TotB.MaxLength = 20;
            this.TotB.Name = "TotB";
            this.TotB.oLen = 0;
            this.TotB.ReadOnly = true;
            this.TotB.Size = new System.Drawing.Size(167, 27);
            this.TotB.TabIndex = 4;
            this.TotB.TabStop = false;
            this.TotB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.產品編號,
            this.品名規格,
            this.單位,
            this.進貨數量,
            this.進貨淨額});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(1, 39);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1008, 497);
            this.dataGridViewT1.TabIndex = 2;
            // 
            // 產品編號
            // 
            this.產品編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品編號.DataPropertyName = "itno";
            this.產品編號.HeaderText = "產品編號";
            this.產品編號.MaxInputLength = 20;
            this.產品編號.MinimumWidth = 10;
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
            this.品名規格.MinimumWidth = 10;
            this.品名規格.Name = "品名規格";
            this.品名規格.ReadOnly = true;
            this.品名規格.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.品名規格.Width = 253;
            // 
            // 單位
            // 
            this.單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位.DataPropertyName = "itunit";
            this.單位.HeaderText = "單位";
            this.單位.MaxInputLength = 4;
            this.單位.MinimumWidth = 16;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 45;
            // 
            // 進貨數量
            // 
            this.進貨數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.進貨數量.DataPropertyName = "進貨數量";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = "0";
            this.進貨數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.進貨數量.FirstNum = 0;
            this.進貨數量.HeaderText = "進貨數量";
            this.進貨數量.LastNum = 0;
            this.進貨數量.MarkThousand = false;
            this.進貨數量.MaxInputLength = 16;
            this.進貨數量.MinimumWidth = 16;
            this.進貨數量.Name = "進貨數量";
            this.進貨數量.NullInput = false;
            this.進貨數量.NullValue = "0";
            this.進貨數量.ReadOnly = true;
            this.進貨數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.進貨數量.Width = 141;
            // 
            // 進貨淨額
            // 
            this.進貨淨額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.進貨淨額.DataPropertyName = "進貨淨額";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.NullValue = "0.00";
            this.進貨淨額.DefaultCellStyle = dataGridViewCellStyle3;
            this.進貨淨額.FirstNum = 0;
            this.進貨淨額.HeaderText = "進貨淨額";
            this.進貨淨額.LastNum = 0;
            this.進貨淨額.MarkThousand = false;
            this.進貨淨額.MaxInputLength = 16;
            this.進貨淨額.MinimumWidth = 16;
            this.進貨淨額.Name = "進貨淨額";
            this.進貨淨額.NullInput = false;
            this.進貨淨額.NullValue = "0";
            this.進貨淨額.ReadOnly = true;
            this.進貨淨額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.進貨淨額.Width = 141;
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
            this.groupBoxT1.Location = new System.Drawing.Point(12, 544);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(619, 79);
            this.groupBoxT1.TabIndex = 3;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "單行註腳";
            // 
            // rdFooter6
            // 
            this.rdFooter6.AutoSize = true;
            this.rdFooter6.BackColor = System.Drawing.Color.Transparent;
            this.rdFooter6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdFooter6.Font = new System.Drawing.Font("細明體", 12F);
            this.rdFooter6.Location = new System.Drawing.Point(517, 36);
            this.rdFooter6.Name = "rdFooter6";
            this.rdFooter6.Size = new System.Drawing.Size(74, 20);
            this.rdFooter6.TabIndex = 5;
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
            this.rdFooter5.Location = new System.Drawing.Point(419, 36);
            this.rdFooter5.Name = "rdFooter5";
            this.rdFooter5.Size = new System.Drawing.Size(74, 20);
            this.rdFooter5.TabIndex = 4;
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
            this.rdFooter4.Location = new System.Drawing.Point(321, 36);
            this.rdFooter4.Name = "rdFooter4";
            this.rdFooter4.Size = new System.Drawing.Size(74, 20);
            this.rdFooter4.TabIndex = 3;
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
            this.rdFooter3.Location = new System.Drawing.Point(223, 36);
            this.rdFooter3.Name = "rdFooter3";
            this.rdFooter3.Size = new System.Drawing.Size(74, 20);
            this.rdFooter3.TabIndex = 2;
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
            this.rdFooter2.Location = new System.Drawing.Point(125, 36);
            this.rdFooter2.Name = "rdFooter2";
            this.rdFooter2.Size = new System.Drawing.Size(74, 20);
            this.rdFooter2.TabIndex = 1;
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
            this.rdFooter1.Location = new System.Drawing.Point(27, 36);
            this.rdFooter1.Name = "rdFooter1";
            this.rdFooter1.Size = new System.Drawing.Size(74, 20);
            this.rdFooter1.TabIndex = 0;
            this.rdFooter1.Tag = "第一組";
            this.rdFooter1.Text = "第一組";
            this.rdFooter1.UseVisualStyleBackColor = false;
            // 
            // panelT1
            // 
            this.panelT1.AutoSize = true;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnExcel);
            this.panelT1.Controls.Add(this.btnWord);
            this.panelT1.Controls.Add(this.btnPreView);
            this.panelT1.Controls.Add(this.btnPrint);
            this.panelT1.Location = new System.Drawing.Point(-1, -1);
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
            // btnPreView
            // 
            this.btnPreView.BackColor = System.Drawing.SystemColors.Control;
            this.btnPreView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreView.BackgroundImage")));
            this.btnPreView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPreView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPreView.Font = new System.Drawing.Font("細明體", 9F);
            this.btnPreView.Location = new System.Drawing.Point(69, 0);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(69, 79);
            this.btnPreView.TabIndex = 9;
            this.btnPreView.UseDefaultSettings = false;
            this.btnPreView.UseVisualStyleBackColor = false;
            this.btnPreView.Click += new System.EventHandler(this.btnPreView_Click);
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
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Controls.Add(this.panelT1);
            this.panelNT1.Location = new System.Drawing.Point(644, 544);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(355, 79);
            this.panelNT1.TabIndex = 1;
            // 
            // FrmItemShop_Rptc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.panelNT1);
            this.Controls.Add(this.TotMnyb);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.TotB);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "FrmItemShop_Rptc";
            this.Tag = "產品進貨報表";
            this.Text = "產品進貨報表-總額表";
            this.Load += new System.EventHandler(this.FrmItemShop_Rptc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.panelNT1.ResumeLayout(false);
            this.panelNT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT TotMnyb;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT TotB;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT rdFooter6;
        private JE.MyControl.RadioT rdFooter5;
        private JE.MyControl.RadioT rdFooter4;
        private JE.MyControl.RadioT rdFooter3;
        private JE.MyControl.RadioT rdFooter2;
        private JE.MyControl.RadioT rdFooter1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnExcel;
        private JE.MyControl.ButtonT btnWord;
        private JE.MyControl.ButtonT btnPreView;
        private JE.MyControl.ButtonT btnPrint;
    
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 進貨數量;
        private JE.MyControl.DataGridViewTextNumberT 進貨淨額;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelNT panelNT1;
     
    }
}