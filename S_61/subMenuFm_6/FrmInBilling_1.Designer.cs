namespace S_61.subMenuFm_6
{
    partial class FrmInBilling_1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInBilling_1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.ItNo = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.StNo = new JE.MyControl.TextBoxT();
            this.StName = new JE.MyControl.TextBoxT();
            this.totcount = new JE.MyControl.TextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.期初庫存量 = new JE.MyControl.DataGridViewTextNumberT();
            this.期初單位成本 = new JE.MyControl.DataGridViewTextNumberT();
            this.產品組成 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelT1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Location = new System.Drawing.Point(397, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(217, 79);
            this.panelT1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 9F);
            this.btnExit.Location = new System.Drawing.Point(138, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 2;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("細明體", 9F);
            this.btnSave.Location = new System.Drawing.Point(69, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 79);
            this.btnSave.TabIndex = 1;
            this.btnSave.UseDefaultSettings = false;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.SystemColors.Control;
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModify.Font = new System.Drawing.Font("細明體", 9F);
            this.btnModify.Location = new System.Drawing.Point(0, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(69, 79);
            this.btnModify.TabIndex = 0;
            this.btnModify.UseDefaultSettings = false;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(96, 577);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "產品編號";
            // 
            // ItNo
            // 
            this.ItNo.AllowGrayBackColor = false;
            this.ItNo.AllowResize = true;
            this.ItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo.Location = new System.Drawing.Point(174, 572);
            this.ItNo.MaxLength = 20;
            this.ItNo.Name = "ItNo";
            this.ItNo.oLen = 0;
            this.ItNo.Size = new System.Drawing.Size(167, 27);
            this.ItNo.TabIndex = 1;
            this.ItNo.TextChanged += new System.EventHandler(this.ItNo_TextChanged);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(15, 13);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "倉庫編號";
            // 
            // StNo
            // 
            this.StNo.AllowGrayBackColor = true;
            this.StNo.AllowResize = true;
            this.StNo.BackColor = System.Drawing.Color.Silver;
            this.StNo.Font = new System.Drawing.Font("細明體", 12F);
            this.StNo.Location = new System.Drawing.Point(93, 8);
            this.StNo.MaxLength = 10;
            this.StNo.Name = "StNo";
            this.StNo.oLen = 0;
            this.StNo.ReadOnly = true;
            this.StNo.Size = new System.Drawing.Size(87, 27);
            this.StNo.TabIndex = 0;
            this.StNo.TabStop = false;
            // 
            // StName
            // 
            this.StName.AllowGrayBackColor = true;
            this.StName.AllowResize = true;
            this.StName.BackColor = System.Drawing.Color.Silver;
            this.StName.Font = new System.Drawing.Font("細明體", 12F);
            this.StName.Location = new System.Drawing.Point(186, 8);
            this.StName.MaxLength = 20;
            this.StName.Name = "StName";
            this.StName.oLen = 0;
            this.StName.ReadOnly = true;
            this.StName.Size = new System.Drawing.Size(167, 27);
            this.StName.TabIndex = 0;
            this.StName.TabStop = false;
            // 
            // totcount
            // 
            this.totcount.AllowGrayBackColor = true;
            this.totcount.AllowResize = true;
            this.totcount.BackColor = System.Drawing.Color.Silver;
            this.totcount.Font = new System.Drawing.Font("細明體", 12F);
            this.totcount.Location = new System.Drawing.Point(652, 8);
            this.totcount.MaxLength = 20;
            this.totcount.Name = "totcount";
            this.totcount.oLen = 0;
            this.totcount.ReadOnly = true;
            this.totcount.Size = new System.Drawing.Size(167, 27);
            this.totcount.TabIndex = 1;
            this.totcount.TabStop = false;
            this.totcount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(558, 13);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(88, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "庫存總金額";
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
            this.期初庫存量,
            this.期初單位成本,
            this.產品組成,
            this.StNo1});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(1, 41);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1009, 498);
            this.dataGridViewT1.TabIndex = 3;
            this.dataGridViewT1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewT1_CellBeginEdit);
            this.dataGridViewT1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewT1_CellValidating);
            this.dataGridViewT1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewT1_CurrentCellDirtyStateChanged);
            this.dataGridViewT1.SelectionChanged += new System.EventHandler(this.dataGridViewT1_SelectionChanged);
            // 
            // 產品編號
            // 
            this.產品編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品編號.DataPropertyName = "產品編號";
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
            this.品名規格.DataPropertyName = "品名規格";
            this.品名規格.HeaderText = "品名規格";
            this.品名規格.MaxInputLength = 30;
            this.品名規格.Name = "品名規格";
            this.品名規格.ReadOnly = true;
            this.品名規格.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.品名規格.Width = 253;
            // 
            // 單位
            // 
            this.單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位.DataPropertyName = "單位";
            this.單位.HeaderText = "單位";
            this.單位.MaxInputLength = 4;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 45;
            // 
            // 期初庫存量
            // 
            this.期初庫存量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.期初庫存量.DataPropertyName = "期初庫存量";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.期初庫存量.DefaultCellStyle = dataGridViewCellStyle2;
            this.期初庫存量.FirstNum = 0;
            this.期初庫存量.HeaderText = "期初庫存量";
            this.期初庫存量.LastNum = 0;
            this.期初庫存量.MarkThousand = false;
            this.期初庫存量.MaxInputLength = 16;
            this.期初庫存量.Name = "期初庫存量";
            this.期初庫存量.NullInput = false;
            this.期初庫存量.NullValue = "0";
            this.期初庫存量.ReadOnly = true;
            this.期初庫存量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.期初庫存量.Width = 141;
            // 
            // 期初單位成本
            // 
            this.期初單位成本.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.期初單位成本.DataPropertyName = "期初單位成本";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.期初單位成本.DefaultCellStyle = dataGridViewCellStyle3;
            this.期初單位成本.FirstNum = 0;
            this.期初單位成本.HeaderText = "期初單位成本";
            this.期初單位成本.LastNum = 0;
            this.期初單位成本.MarkThousand = false;
            this.期初單位成本.MaxInputLength = 16;
            this.期初單位成本.Name = "期初單位成本";
            this.期初單位成本.NullInput = false;
            this.期初單位成本.NullValue = "0";
            this.期初單位成本.ReadOnly = true;
            this.期初單位成本.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.期初單位成本.Width = 141;
            // 
            // 產品組成
            // 
            this.產品組成.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品組成.DataPropertyName = "產品組成";
            this.產品組成.FillWeight = 50F;
            this.產品組成.HeaderText = "產品組成";
            this.產品組成.MaxInputLength = 10;
            this.產品組成.Name = "產品組成";
            this.產品組成.ReadOnly = true;
            this.產品組成.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.產品組成.Width = 93;
            // 
            // StNo1
            // 
            this.StNo1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.StNo1.DataPropertyName = "StNo";
            this.StNo1.HeaderText = "StNo";
            this.StNo1.MaxInputLength = 10;
            this.StNo1.Name = "StNo1";
            this.StNo1.ReadOnly = true;
            this.StNo1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StNo1.Visible = false;
            this.StNo1.Width = 93;
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
            // FrmInBilling_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.StNo);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.StName);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.totcount);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.ItNo);
            this.MinimumSize = new System.Drawing.Size(200, 38);
            this.Name = "FrmInBilling_1";
            this.Text = "庫存期初開帳作業";
            this.Load += new System.EventHandler(this.FrmInBilling_1_Load);
            this.panelT1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.TextBoxT StNo;
        private JE.MyControl.TextBoxT StName;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnSave;
        private JE.MyControl.ButtonT btnModify;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT ItNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 期初庫存量;
        private JE.MyControl.DataGridViewTextNumberT 期初單位成本;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品組成;
        private System.Windows.Forms.DataGridViewTextBoxColumn StNo1;
        private JE.MyControl.TextBoxT totcount;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}