namespace S_61.SOther
{
    partial class FrmBomBrow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBomBrow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn = new System.Data.SqlClient.SqlConnection();
            this.da = new System.Data.SqlClient.SqlDataAdapter();
            this.textBoxT1 = new JE.MyControl.TextBoxT();
            this.textBoxT2 = new JE.MyControl.TextBoxT();
            this.textBoxT3 = new JE.MyControl.TextBoxT();
            this.textBoxT4 = new JE.MyControl.TextBoxT();
            this.textBoxT5 = new JE.MyControl.TextBoxT();
            this.textBoxT6 = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.組件名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.標準使用量 = new JE.MyControl.DataGridViewTextNumberT();
            this.母件比例 = new JE.MyControl.DataGridViewTextNumberT();
            this.單價 = new JE.MyControl.DataGridViewTextNumberT();
            this.折數 = new JE.MyControl.DataGridViewTextNumberT();
            this.金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boitno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxT7 = new JE.MyControl.TextBoxT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.btnQurry = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.cn;
            // 
            // cn
            // 
            this.cn.ConnectionString = "Data Source=.;Initial Catalog=web;Integrated Security=True";
            this.cn.FireInfoMessageEventOnUserErrors = false;
            // 
            // da
            // 
            this.da.SelectCommand = this.sqlSelectCommand1;
            this.da.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "bom", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("boid", "boid"),
                        new System.Data.Common.DataColumnMapping("boitno", "boitno"),
                        new System.Data.Common.DataColumnMapping("itno", "itno"),
                        new System.Data.Common.DataColumnMapping("itname", "itname"),
                        new System.Data.Common.DataColumnMapping("itunit", "itunit"),
                        new System.Data.Common.DataColumnMapping("itqty", "itqty"),
                        new System.Data.Common.DataColumnMapping("itpareprs", "itpareprs"),
                        new System.Data.Common.DataColumnMapping("itprice", "itprice"),
                        new System.Data.Common.DataColumnMapping("itprs", "itprs"),
                        new System.Data.Common.DataColumnMapping("itmny", "itmny"),
                        new System.Data.Common.DataColumnMapping("itpkgqty", "itpkgqty"),
                        new System.Data.Common.DataColumnMapping("itnote", "itnote"),
                        new System.Data.Common.DataColumnMapping("itrec", "itrec"),
                        new System.Data.Common.DataColumnMapping("ItSource", "ItSource"),
                        new System.Data.Common.DataColumnMapping("ItBuyPri", "ItBuyPri"),
                        new System.Data.Common.DataColumnMapping("ItBuyMny", "ItBuyMny"),
                        new System.Data.Common.DataColumnMapping("boitno1", "boitno1"),
                        new System.Data.Common.DataColumnMapping("boitname", "boitname"),
                        new System.Data.Common.DataColumnMapping("bototmny", "bototmny"),
                        new System.Data.Common.DataColumnMapping("bototqty", "bototqty"),
                        new System.Data.Common.DataColumnMapping("bomemo", "bomemo"),
                        new System.Data.Common.DataColumnMapping("itstockqty", "itstockqty")})});
            // 
            // textBoxT1
            // 
            this.textBoxT1.AllowGrayBackColor = true;
            this.textBoxT1.AllowResize = true;
            this.textBoxT1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.textBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT1.Location = new System.Drawing.Point(82, 6);
            this.textBoxT1.MaxLength = 20;
            this.textBoxT1.Name = "textBoxT1";
            this.textBoxT1.oLen = 0;
            this.textBoxT1.ReadOnly = true;
            this.textBoxT1.Size = new System.Drawing.Size(167, 27);
            this.textBoxT1.TabIndex = 0;
            this.textBoxT1.TabStop = false;
            // 
            // textBoxT2
            // 
            this.textBoxT2.AllowGrayBackColor = true;
            this.textBoxT2.AllowResize = true;
            this.textBoxT2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.textBoxT2.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT2.Location = new System.Drawing.Point(82, 39);
            this.textBoxT2.MaxLength = 20;
            this.textBoxT2.Name = "textBoxT2";
            this.textBoxT2.oLen = 0;
            this.textBoxT2.ReadOnly = true;
            this.textBoxT2.Size = new System.Drawing.Size(167, 27);
            this.textBoxT2.TabIndex = 2;
            this.textBoxT2.TabStop = false;
            // 
            // textBoxT3
            // 
            this.textBoxT3.AllowGrayBackColor = true;
            this.textBoxT3.AllowResize = true;
            this.textBoxT3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.textBoxT3.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT3.Location = new System.Drawing.Point(827, 39);
            this.textBoxT3.MaxLength = 20;
            this.textBoxT3.Name = "textBoxT3";
            this.textBoxT3.oLen = 0;
            this.textBoxT3.ReadOnly = true;
            this.textBoxT3.Size = new System.Drawing.Size(167, 27);
            this.textBoxT3.TabIndex = 5;
            this.textBoxT3.TabStop = false;
            this.textBoxT3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxT4
            // 
            this.textBoxT4.AllowGrayBackColor = true;
            this.textBoxT4.AllowResize = true;
            this.textBoxT4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.textBoxT4.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT4.Location = new System.Drawing.Point(579, 39);
            this.textBoxT4.MaxLength = 20;
            this.textBoxT4.Name = "textBoxT4";
            this.textBoxT4.oLen = 0;
            this.textBoxT4.ReadOnly = true;
            this.textBoxT4.Size = new System.Drawing.Size(167, 27);
            this.textBoxT4.TabIndex = 4;
            this.textBoxT4.TabStop = false;
            this.textBoxT4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxT5
            // 
            this.textBoxT5.AllowGrayBackColor = true;
            this.textBoxT5.AllowResize = true;
            this.textBoxT5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.textBoxT5.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT5.Location = new System.Drawing.Point(333, 39);
            this.textBoxT5.MaxLength = 20;
            this.textBoxT5.Name = "textBoxT5";
            this.textBoxT5.oLen = 0;
            this.textBoxT5.ReadOnly = true;
            this.textBoxT5.Size = new System.Drawing.Size(167, 27);
            this.textBoxT5.TabIndex = 3;
            this.textBoxT5.TabStop = false;
            this.textBoxT5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxT6
            // 
            this.textBoxT6.AllowGrayBackColor = true;
            this.textBoxT6.AllowResize = true;
            this.textBoxT6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.textBoxT6.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT6.Location = new System.Drawing.Point(333, 6);
            this.textBoxT6.MaxLength = 70;
            this.textBoxT6.Name = "textBoxT6";
            this.textBoxT6.oLen = 0;
            this.textBoxT6.ReadOnly = true;
            this.textBoxT6.Size = new System.Drawing.Size(567, 27);
            this.textBoxT6.TabIndex = 1;
            this.textBoxT6.TabStop = false;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(8, 11);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "組件編號";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(8, 44);
            this.lblT2.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "產品編號";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(755, 44);
            this.lblT3.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "總庫存量";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(507, 44);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "用量總計";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(258, 11);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(72, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "備    註";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(258, 44);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(72, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "金額總計";
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
            this.組件名稱,
            this.品名規格,
            this.單位,
            this.標準使用量,
            this.母件比例,
            this.單價,
            this.折數,
            this.金額,
            this.包裝數量,
            this.說明,
            this.boitno});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 72);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 497);
            this.dataGridViewT1.TabIndex = 6;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            this.dataGridViewT1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewT1_RowStateChanged);
            // 
            // 組件名稱
            // 
            this.組件名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.組件名稱.DataPropertyName = "boitname";
            this.組件名稱.HeaderText = "組件名稱";
            this.組件名稱.MaxInputLength = 30;
            this.組件名稱.Name = "組件名稱";
            this.組件名稱.ReadOnly = true;
            this.組件名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.組件名稱.Width = 253;
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
            // 標準使用量
            // 
            this.標準使用量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.標準使用量.DataPropertyName = "itqty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.標準使用量.DefaultCellStyle = dataGridViewCellStyle2;
            this.標準使用量.FirstNum = 0;
            this.標準使用量.HeaderText = "標準使用量";
            this.標準使用量.LastNum = 0;
            this.標準使用量.MarkThousand = false;
            this.標準使用量.MaxInputLength = 10;
            this.標準使用量.Name = "標準使用量";
            this.標準使用量.NullInput = false;
            this.標準使用量.NullValue = "0";
            this.標準使用量.ReadOnly = true;
            this.標準使用量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.標準使用量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.標準使用量.Width = 93;
            // 
            // 母件比例
            // 
            this.母件比例.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.母件比例.DataPropertyName = "itpareprs";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.母件比例.DefaultCellStyle = dataGridViewCellStyle3;
            this.母件比例.FirstNum = 0;
            this.母件比例.HeaderText = "母件比例";
            this.母件比例.LastNum = 0;
            this.母件比例.MarkThousand = false;
            this.母件比例.MaxInputLength = 10;
            this.母件比例.Name = "母件比例";
            this.母件比例.NullInput = false;
            this.母件比例.NullValue = "0";
            this.母件比例.ReadOnly = true;
            this.母件比例.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.母件比例.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.母件比例.Width = 93;
            // 
            // 單價
            // 
            this.單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單價.DataPropertyName = "itprice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單價.DefaultCellStyle = dataGridViewCellStyle4;
            this.單價.FirstNum = 0;
            this.單價.HeaderText = "單價";
            this.單價.LastNum = 0;
            this.單價.MarkThousand = false;
            this.單價.MaxInputLength = 16;
            this.單價.Name = "單價";
            this.單價.NullInput = false;
            this.單價.NullValue = "0";
            this.單價.ReadOnly = true;
            this.單價.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單價.Width = 141;
            // 
            // 折數
            // 
            this.折數.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折數.DataPropertyName = "itprs";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.折數.DefaultCellStyle = dataGridViewCellStyle5;
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
            // 金額
            // 
            this.金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.金額.DataPropertyName = "itmny";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.金額.DefaultCellStyle = dataGridViewCellStyle6;
            this.金額.FirstNum = 0;
            this.金額.HeaderText = "金額";
            this.金額.LastNum = 0;
            this.金額.MarkThousand = false;
            this.金額.MaxInputLength = 16;
            this.金額.Name = "金額";
            this.金額.NullInput = false;
            this.金額.NullValue = "0";
            this.金額.ReadOnly = true;
            this.金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.金額.Width = 141;
            // 
            // 包裝數量
            // 
            this.包裝數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量.DataPropertyName = "itpkgqty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝數量.DefaultCellStyle = dataGridViewCellStyle7;
            this.包裝數量.FirstNum = 0;
            this.包裝數量.HeaderText = "包裝數量";
            this.包裝數量.LastNum = 0;
            this.包裝數量.MarkThousand = false;
            this.包裝數量.MaxInputLength = 10;
            this.包裝數量.Name = "包裝數量";
            this.包裝數量.NullInput = false;
            this.包裝數量.NullValue = "0";
            this.包裝數量.ReadOnly = true;
            this.包裝數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.包裝數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝數量.Width = 93;
            // 
            // 說明
            // 
            this.說明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.說明.DataPropertyName = "itnote";
            this.說明.HeaderText = "說明";
            this.說明.MaxInputLength = 20;
            this.說明.Name = "說明";
            this.說明.ReadOnly = true;
            this.說明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.說明.Width = 173;
            // 
            // boitno
            // 
            this.boitno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.boitno.DataPropertyName = "boitno";
            this.boitno.HeaderText = "boitno";
            this.boitno.MaxInputLength = 20;
            this.boitno.Name = "boitno";
            this.boitno.ReadOnly = true;
            this.boitno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.boitno.Visible = false;
            this.boitno.Width = 173;
            // 
            // textBoxT7
            // 
            this.textBoxT7.AllowGrayBackColor = false;
            this.textBoxT7.AllowResize = true;
            this.textBoxT7.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT7.Location = new System.Drawing.Point(217, 583);
            this.textBoxT7.MaxLength = 20;
            this.textBoxT7.Name = "textBoxT7";
            this.textBoxT7.oLen = 0;
            this.textBoxT7.Size = new System.Drawing.Size(167, 27);
            this.textBoxT7.TabIndex = 7;
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(145, 588);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(72, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "組件編號";
            // 
            // btnQurry
            // 
            this.btnQurry.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQurry.Location = new System.Drawing.Point(409, 575);
            this.btnQurry.Name = "btnQurry";
            this.btnQurry.Size = new System.Drawing.Size(152, 43);
            this.btnQurry.TabIndex = 8;
            this.btnQurry.Text = "F3:查詢";
            this.btnQurry.UseVisualStyleBackColor = true;
            this.btnQurry.Click += new System.EventHandler(this.btnQurry_Click);
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(561, 575);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(152, 43);
            this.btnGet.TabIndex = 9;
            this.btnGet.Text = "F4:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(713, 575);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(152, 43);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "F11:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(954, 20);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatusLabel3.Text = "插入";
            // 
            // FrmBomBrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.btnQurry);
            this.Controls.Add(this.textBoxT7);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.textBoxT5);
            this.Controls.Add(this.textBoxT3);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.textBoxT2);
            this.Controls.Add(this.textBoxT1);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.textBoxT4);
            this.Controls.Add(this.textBoxT6);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.lblT1);
            this.Name = "FrmBomBrow";
            this.Text = "組合組裝品建檔瀏覽";
            this.Load += new System.EventHandler(this.FrmBomBrow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection cn;
        private System.Data.SqlClient.SqlDataAdapter da;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private JE.MyControl.TextBoxT textBoxT1;
        private JE.MyControl.TextBoxT textBoxT2;
        private JE.MyControl.TextBoxT textBoxT3;
        private JE.MyControl.TextBoxT textBoxT4;
        private JE.MyControl.TextBoxT textBoxT5;
        private JE.MyControl.TextBoxT textBoxT6;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.TextBoxT textBoxT7;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.ButtonSmallT btnQurry;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 組件名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 標準使用量;
        private JE.MyControl.DataGridViewTextNumberT 母件比例;
        private JE.MyControl.DataGridViewTextNumberT 單價;
        private JE.MyControl.DataGridViewTextNumberT 折數;
        private JE.MyControl.DataGridViewTextNumberT 金額;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 說明;
        private System.Windows.Forms.DataGridViewTextBoxColumn boitno;

    }
}