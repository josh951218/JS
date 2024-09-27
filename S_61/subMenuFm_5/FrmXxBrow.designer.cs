namespace JBS.JS
{
    partial class FrmXxBrow<T>
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
            this.XNo = new JE.MyControl.TextBoxT();
            this.XName = new JE.MyControl.TextBoxT();
            this.lblNo = new JE.MyControl.LabelT();
            this.lblName = new JE.MyControl.LabelT();
            this.btnQuery = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.btnAppend = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.tableLayoutPanelbase1 = new JE.MyControl.TableLayoutPanelbase();
            this.編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.三行 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.四行 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.五行 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.tableLayoutPanelbase1.SuspendLayout();
            this.SuspendLayout();
            // 
            // XNo
            // 
            this.XNo.AllowGrayBackColor = false;
            this.XNo.AllowResize = true;
            this.XNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.XNo.Font = new System.Drawing.Font("細明體", 12F);
            this.XNo.Location = new System.Drawing.Point(314, 24);
            this.XNo.MaxLength = 10;
            this.XNo.Name = "XNo";
            this.XNo.oLen = 0;
            this.XNo.Size = new System.Drawing.Size(87, 27);
            this.XNo.TabIndex = 1;
            this.XNo.TextChanged += new System.EventHandler(this.XNo_TextChanged);
            // 
            // XName
            // 
            this.XName.AllowGrayBackColor = false;
            this.XName.AllowResize = true;
            this.XName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.XName.Font = new System.Drawing.Font("細明體", 12F);
            this.XName.Location = new System.Drawing.Point(718, 24);
            this.XName.MaxLength = 10;
            this.XName.Name = "XName";
            this.XName.oLen = 0;
            this.XName.Size = new System.Drawing.Size(87, 27);
            this.XName.TabIndex = 2;
            this.XName.TextChanged += new System.EventHandler(this.XNo_TextChanged);
            // 
            // lblNo
            // 
            this.lblNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNo.AutoSize = true;
            this.lblNo.BackColor = System.Drawing.Color.Transparent;
            this.lblNo.Font = new System.Drawing.Font("細明體", 12F);
            this.lblNo.Location = new System.Drawing.Point(204, 29);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(104, 16);
            this.lblNo.TabIndex = 0;
            this.lblNo.Text = "廠商類別編號";
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("細明體", 12F);
            this.lblName.Location = new System.Drawing.Point(608, 29);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "廠商類別名稱";
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(347, 574);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(158, 49);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "F6:字元查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(505, 574);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(158, 49);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "F9:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(663, 574);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(158, 49);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "F11:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAppend
            // 
            this.btnAppend.Font = new System.Drawing.Font("細明體", 12F);
            this.btnAppend.Location = new System.Drawing.Point(189, 574);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(158, 49);
            this.btnAppend.TabIndex = 2;
            this.btnAppend.Text = "F2:新增";
            this.btnAppend.UseVisualStyleBackColor = true;
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
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
            this.編號,
            this.名稱,
            this.三行,
            this.四行,
            this.五行});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewT1.Dock = System.Windows.Forms.DockStyle.Top;
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 493);
            this.dataGridViewT1.TabIndex = 10;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
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
            // tableLayoutPanelbase1
            // 
            this.tableLayoutPanelbase1.ColumnCount = 7;
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelbase1.Controls.Add(this.lblNo, 1, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.XNo, 2, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.lblName, 4, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.XName, 5, 0);
            this.tableLayoutPanelbase1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelbase1.Location = new System.Drawing.Point(0, 493);
            this.tableLayoutPanelbase1.Name = "tableLayoutPanelbase1";
            this.tableLayoutPanelbase1.RowCount = 1;
            this.tableLayoutPanelbase1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelbase1.Size = new System.Drawing.Size(1010, 75);
            this.tableLayoutPanelbase1.TabIndex = 1;
            // 
            // 編號
            // 
            this.編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.編號.HeaderText = "編號";
            this.編號.MaxInputLength = 20;
            this.編號.Name = "編號";
            this.編號.ReadOnly = true;
            this.編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.編號.Width = 173;
            // 
            // 名稱
            // 
            this.名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.名稱.HeaderText = "名稱";
            this.名稱.MaxInputLength = 20;
            this.名稱.Name = "名稱";
            this.名稱.ReadOnly = true;
            this.名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.名稱.Width = 173;
            // 
            // 三行
            // 
            this.三行.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.三行.HeaderText = "三行";
            this.三行.MaxInputLength = 10;
            this.三行.Name = "三行";
            this.三行.ReadOnly = true;
            this.三行.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.三行.Visible = false;
            this.三行.Width = 93;
            // 
            // 四行
            // 
            this.四行.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.四行.HeaderText = "四行";
            this.四行.MaxInputLength = 10;
            this.四行.Name = "四行";
            this.四行.ReadOnly = true;
            this.四行.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.四行.Visible = false;
            this.四行.Width = 93;
            // 
            // 五行
            // 
            this.五行.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.五行.HeaderText = "五行";
            this.五行.MaxInputLength = 10;
            this.五行.Name = "五行";
            this.五行.ReadOnly = true;
            this.五行.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.五行.Visible = false;
            this.五行.Width = 93;
            // 
            // FrmXxBrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.tableLayoutPanelbase1);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAppend);
            this.Name = "FrmXxBrow";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmXXBorw_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.tableLayoutPanelbase1.ResumeLayout(false);
            this.tableLayoutPanelbase1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.TextBoxT XNo;
        private JE.MyControl.TextBoxT XName;
        private JE.MyControl.LabelT lblNo;
        private JE.MyControl.LabelT lblName;
        private JE.MyControl.ButtonSmallT btnAppend;
        private JE.MyControl.ButtonSmallT btnQuery;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPanelbase1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 三行;
        private System.Windows.Forms.DataGridViewTextBoxColumn 四行;
        private System.Windows.Forms.DataGridViewTextBoxColumn 五行; 

    }
}