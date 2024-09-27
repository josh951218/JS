namespace S_61.subMenuFm_2
{
    partial class FrmOutStkBrow
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
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.領出單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.寄庫單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.領出日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.領出數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.寄庫未領數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.寄庫日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.包裝數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.產品組成 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.備註說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelT1 = new JE.MyControl.LabelT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.labelT3 = new JE.MyControl.LabelT();
            this.labelT4 = new JE.MyControl.LabelT();
            this.textBoxT1 = new JE.MyControl.TextBoxT();
            this.textBoxT2 = new JE.MyControl.TextBoxT();
            this.textBoxT3 = new JE.MyControl.TextBoxT();
            this.textBoxT4 = new JE.MyControl.TextBoxT();
            this.labelT5 = new JE.MyControl.LabelT();
            this.textBoxT5 = new JE.MyControl.TextBoxT();
            this.textBoxT6 = new JE.MyControl.TextBoxT();
            this.Q2 = new JE.MyControl.ButtonSmallT();
            this.Q3 = new JE.MyControl.ButtonSmallT();
            this.Q4 = new JE.MyControl.ButtonSmallT();
            this.Q5 = new JE.MyControl.ButtonSmallT();
            this.Q6 = new JE.MyControl.ButtonSmallT();
            this.Q7 = new JE.MyControl.ButtonSmallT();
            this.Q8 = new JE.MyControl.ButtonSmallT();
            this.labelT6 = new JE.MyControl.LabelT();
            this.labelT7 = new JE.MyControl.LabelT();
            this.labelT8 = new JE.MyControl.LabelT();
            this.labelT9 = new JE.MyControl.LabelT();
            this.labelT10 = new JE.MyControl.LabelT();
            this.labelT11 = new JE.MyControl.LabelT();
            this.touno = new JE.MyControl.TextBoxT();
            this.tinno = new JE.MyControl.TextBoxT();
            this.toudate = new JE.MyControl.TextBoxT();
            this.tcuno = new JE.MyControl.TextBoxT();
            this.titno = new JE.MyControl.TextBoxT();
            this.tstno = new JE.MyControl.TextBoxT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnStock = new JE.MyControl.ButtonSmallT();
            this.btnBom = new JE.MyControl.ButtonSmallT();
            this.btnDesp = new JE.MyControl.ButtonSmallT();
            this.btnPicture = new JE.MyControl.ButtonSmallT();
            this.btnQuery = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
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
            this.領出單號,
            this.寄庫單號,
            this.領出日期,
            this.客戶簡稱,
            this.品名規格,
            this.領出數量,
            this.寄庫未領數量,
            this.單位,
            this.寄庫日期,
            this.包裝數量,
            this.產品組成,
            this.備註說明});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 87);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(999, 370);
            this.dataGridViewT1.TabIndex = 6;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            this.dataGridViewT1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewT1_RowStateChanged);
            // 
            // 領出單號
            // 
            this.領出單號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.領出單號.DataPropertyName = "ouno";
            this.領出單號.HeaderText = "領出單號";
            this.領出單號.MaxInputLength = 14;
            this.領出單號.Name = "領出單號";
            this.領出單號.ReadOnly = true;
            this.領出單號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.領出單號.Width = 125;
            // 
            // 寄庫單號
            // 
            this.寄庫單號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.寄庫單號.DataPropertyName = "inno";
            this.寄庫單號.HeaderText = "寄庫單號";
            this.寄庫單號.MaxInputLength = 14;
            this.寄庫單號.Name = "寄庫單號";
            this.寄庫單號.ReadOnly = true;
            this.寄庫單號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.寄庫單號.Width = 125;
            // 
            // 領出日期
            // 
            this.領出日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.領出日期.HeaderText = "領出日期";
            this.領出日期.MaxInputLength = 10;
            this.領出日期.Name = "領出日期";
            this.領出日期.ReadOnly = true;
            this.領出日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.領出日期.Width = 93;
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
            // 領出數量
            // 
            this.領出數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.領出數量.DataPropertyName = "ouqty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.領出數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.領出數量.FirstNum = 0;
            this.領出數量.HeaderText = "領出數量";
            this.領出數量.LastNum = 0;
            this.領出數量.MarkThousand = false;
            this.領出數量.MaxInputLength = 16;
            this.領出數量.Name = "領出數量";
            this.領出數量.NullInput = false;
            this.領出數量.NullValue = "0";
            this.領出數量.ReadOnly = true;
            this.領出數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.領出數量.Width = 141;
            // 
            // 寄庫未領數量
            // 
            this.寄庫未領數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.寄庫未領數量.DataPropertyName = "nonqty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.寄庫未領數量.DefaultCellStyle = dataGridViewCellStyle3;
            this.寄庫未領數量.FirstNum = 0;
            this.寄庫未領數量.HeaderText = "寄庫未領數量";
            this.寄庫未領數量.LastNum = 0;
            this.寄庫未領數量.MarkThousand = false;
            this.寄庫未領數量.MaxInputLength = 16;
            this.寄庫未領數量.Name = "寄庫未領數量";
            this.寄庫未領數量.NullInput = false;
            this.寄庫未領數量.NullValue = "0";
            this.寄庫未領數量.ReadOnly = true;
            this.寄庫未領數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.寄庫未領數量.Width = 141;
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
            // 寄庫日期
            // 
            this.寄庫日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.寄庫日期.DataPropertyName = "寄庫日期";
            this.寄庫日期.HeaderText = "寄庫日期";
            this.寄庫日期.MaxInputLength = 10;
            this.寄庫日期.Name = "寄庫日期";
            this.寄庫日期.ReadOnly = true;
            this.寄庫日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.寄庫日期.Width = 93;
            // 
            // 包裝數量
            // 
            this.包裝數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量.DataPropertyName = "itpkgqty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝數量.DefaultCellStyle = dataGridViewCellStyle4;
            this.包裝數量.FirstNum = 0;
            this.包裝數量.HeaderText = "包裝數量";
            this.包裝數量.LastNum = 0;
            this.包裝數量.MarkThousand = false;
            this.包裝數量.MaxInputLength = 10;
            this.包裝數量.Name = "包裝數量";
            this.包裝數量.NullInput = false;
            this.包裝數量.NullValue = "0";
            this.包裝數量.ReadOnly = true;
            this.包裝數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝數量.Width = 93;
            // 
            // 產品組成
            // 
            this.產品組成.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品組成.DataPropertyName = "產品組成";
            this.產品組成.HeaderText = "產品組成";
            this.產品組成.MaxInputLength = 10;
            this.產品組成.Name = "產品組成";
            this.產品組成.ReadOnly = true;
            this.產品組成.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.產品組成.Width = 93;
            // 
            // 備註說明
            // 
            this.備註說明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.備註說明.DataPropertyName = "oumemo";
            this.備註說明.HeaderText = "備註說明";
            this.備註說明.MaxInputLength = 20;
            this.備註說明.Name = "備註說明";
            this.備註說明.ReadOnly = true;
            this.備註說明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.備註說明.Width = 173;
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(12, 18);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "產品編號";
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(303, 18);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(72, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "客戶編號";
            // 
            // labelT3
            // 
            this.labelT3.AutoSize = true;
            this.labelT3.BackColor = System.Drawing.Color.Transparent;
            this.labelT3.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT3.Location = new System.Drawing.Point(12, 54);
            this.labelT3.Name = "labelT3";
            this.labelT3.Size = new System.Drawing.Size(72, 16);
            this.labelT3.TabIndex = 0;
            this.labelT3.Text = "倉庫編號";
            // 
            // labelT4
            // 
            this.labelT4.AutoSize = true;
            this.labelT4.BackColor = System.Drawing.Color.Transparent;
            this.labelT4.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT4.Location = new System.Drawing.Point(537, 18);
            this.labelT4.Name = "labelT4";
            this.labelT4.Size = new System.Drawing.Size(72, 16);
            this.labelT4.TabIndex = 0;
            this.labelT4.Text = "聯 絡 人";
            // 
            // textBoxT1
            // 
            this.textBoxT1.AllowGrayBackColor = true;
            this.textBoxT1.AllowResize = true;
            this.textBoxT1.BackColor = System.Drawing.Color.Silver;
            this.textBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT1.Location = new System.Drawing.Point(90, 15);
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
            this.textBoxT2.BackColor = System.Drawing.Color.Silver;
            this.textBoxT2.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT2.Location = new System.Drawing.Point(90, 54);
            this.textBoxT2.MaxLength = 10;
            this.textBoxT2.Name = "textBoxT2";
            this.textBoxT2.oLen = 0;
            this.textBoxT2.ReadOnly = true;
            this.textBoxT2.Size = new System.Drawing.Size(87, 27);
            this.textBoxT2.TabIndex = 4;
            this.textBoxT2.TabStop = false;
            // 
            // textBoxT3
            // 
            this.textBoxT3.AllowGrayBackColor = true;
            this.textBoxT3.AllowResize = true;
            this.textBoxT3.BackColor = System.Drawing.Color.Silver;
            this.textBoxT3.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT3.Location = new System.Drawing.Point(183, 54);
            this.textBoxT3.MaxLength = 10;
            this.textBoxT3.Name = "textBoxT3";
            this.textBoxT3.oLen = 0;
            this.textBoxT3.ReadOnly = true;
            this.textBoxT3.Size = new System.Drawing.Size(87, 27);
            this.textBoxT3.TabIndex = 5;
            this.textBoxT3.TabStop = false;
            // 
            // textBoxT4
            // 
            this.textBoxT4.AllowGrayBackColor = true;
            this.textBoxT4.AllowResize = true;
            this.textBoxT4.BackColor = System.Drawing.Color.Silver;
            this.textBoxT4.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT4.Location = new System.Drawing.Point(381, 15);
            this.textBoxT4.MaxLength = 10;
            this.textBoxT4.Name = "textBoxT4";
            this.textBoxT4.oLen = 0;
            this.textBoxT4.ReadOnly = true;
            this.textBoxT4.Size = new System.Drawing.Size(87, 27);
            this.textBoxT4.TabIndex = 1;
            this.textBoxT4.TabStop = false;
            // 
            // labelT5
            // 
            this.labelT5.AutoSize = true;
            this.labelT5.BackColor = System.Drawing.Color.Transparent;
            this.labelT5.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT5.Location = new System.Drawing.Point(770, 18);
            this.labelT5.Name = "labelT5";
            this.labelT5.Size = new System.Drawing.Size(72, 16);
            this.labelT5.TabIndex = 0;
            this.labelT5.Text = "電    話";
            // 
            // textBoxT5
            // 
            this.textBoxT5.AllowGrayBackColor = true;
            this.textBoxT5.AllowResize = true;
            this.textBoxT5.BackColor = System.Drawing.Color.Silver;
            this.textBoxT5.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT5.Location = new System.Drawing.Point(615, 12);
            this.textBoxT5.MaxLength = 10;
            this.textBoxT5.Name = "textBoxT5";
            this.textBoxT5.oLen = 0;
            this.textBoxT5.ReadOnly = true;
            this.textBoxT5.Size = new System.Drawing.Size(87, 27);
            this.textBoxT5.TabIndex = 2;
            this.textBoxT5.TabStop = false;
            // 
            // textBoxT6
            // 
            this.textBoxT6.AllowGrayBackColor = true;
            this.textBoxT6.AllowResize = true;
            this.textBoxT6.BackColor = System.Drawing.Color.Silver;
            this.textBoxT6.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT6.Location = new System.Drawing.Point(848, 15);
            this.textBoxT6.MaxLength = 10;
            this.textBoxT6.Name = "textBoxT6";
            this.textBoxT6.oLen = 0;
            this.textBoxT6.ReadOnly = true;
            this.textBoxT6.Size = new System.Drawing.Size(87, 27);
            this.textBoxT6.TabIndex = 3;
            this.textBoxT6.TabStop = false;
            // 
            // Q2
            // 
            this.Q2.AutoSize = true;
            this.Q2.Font = new System.Drawing.Font("細明體", 12F);
            this.Q2.Location = new System.Drawing.Point(12, 463);
            this.Q2.Name = "Q2";
            this.Q2.Size = new System.Drawing.Size(130, 36);
            this.Q2.TabIndex = 7;
            this.Q2.Text = "f2:領出單號";
            this.Q2.UseVisualStyleBackColor = true;
            this.Q2.Click += new System.EventHandler(this.Q2_Click);
            // 
            // Q3
            // 
            this.Q3.AutoSize = true;
            this.Q3.Font = new System.Drawing.Font("細明體", 12F);
            this.Q3.Location = new System.Drawing.Point(147, 463);
            this.Q3.Name = "Q3";
            this.Q3.Size = new System.Drawing.Size(130, 36);
            this.Q3.TabIndex = 8;
            this.Q3.Text = "f3:領出日期";
            this.Q3.UseVisualStyleBackColor = true;
            this.Q3.Click += new System.EventHandler(this.Q3_Click);
            // 
            // Q4
            // 
            this.Q4.AutoSize = true;
            this.Q4.Font = new System.Drawing.Font("細明體", 12F);
            this.Q4.Location = new System.Drawing.Point(282, 463);
            this.Q4.Name = "Q4";
            this.Q4.Size = new System.Drawing.Size(130, 36);
            this.Q4.TabIndex = 9;
            this.Q4.Text = "f4:產品";
            this.Q4.UseVisualStyleBackColor = true;
            this.Q4.Click += new System.EventHandler(this.Q4_Click);
            // 
            // Q5
            // 
            this.Q5.AutoSize = true;
            this.Q5.Font = new System.Drawing.Font("細明體", 12F);
            this.Q5.Location = new System.Drawing.Point(417, 463);
            this.Q5.Name = "Q5";
            this.Q5.Size = new System.Drawing.Size(146, 36);
            this.Q5.TabIndex = 10;
            this.Q5.Text = "f5:客戶+領出日期";
            this.Q5.UseVisualStyleBackColor = true;
            this.Q5.Click += new System.EventHandler(this.Q5_Click);
            // 
            // Q6
            // 
            this.Q6.AutoSize = true;
            this.Q6.Font = new System.Drawing.Font("細明體", 12F);
            this.Q6.Location = new System.Drawing.Point(568, 463);
            this.Q6.Name = "Q6";
            this.Q6.Size = new System.Drawing.Size(146, 36);
            this.Q6.TabIndex = 11;
            this.Q6.Text = "f6:寄庫+領出日期";
            this.Q6.UseVisualStyleBackColor = true;
            this.Q6.Click += new System.EventHandler(this.Q6_Click);
            // 
            // Q7
            // 
            this.Q7.AutoSize = true;
            this.Q7.Font = new System.Drawing.Font("細明體", 12F);
            this.Q7.Location = new System.Drawing.Point(719, 463);
            this.Q7.Name = "Q7";
            this.Q7.Size = new System.Drawing.Size(130, 36);
            this.Q7.TabIndex = 12;
            this.Q7.Text = "f7:客戶+產品";
            this.Q7.UseVisualStyleBackColor = true;
            this.Q7.Click += new System.EventHandler(this.Q7_Click);
            // 
            // Q8
            // 
            this.Q8.AutoSize = true;
            this.Q8.Font = new System.Drawing.Font("細明體", 12F);
            this.Q8.Location = new System.Drawing.Point(854, 463);
            this.Q8.Name = "Q8";
            this.Q8.Size = new System.Drawing.Size(130, 36);
            this.Q8.TabIndex = 13;
            this.Q8.Text = "f8:倉庫+產品";
            this.Q8.UseVisualStyleBackColor = true;
            this.Q8.Click += new System.EventHandler(this.Q8_Click);
            // 
            // labelT6
            // 
            this.labelT6.AutoSize = true;
            this.labelT6.BackColor = System.Drawing.Color.Transparent;
            this.labelT6.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT6.Location = new System.Drawing.Point(20, 514);
            this.labelT6.Name = "labelT6";
            this.labelT6.Size = new System.Drawing.Size(72, 16);
            this.labelT6.TabIndex = 0;
            this.labelT6.Text = "領出單號";
            // 
            // labelT7
            // 
            this.labelT7.AutoSize = true;
            this.labelT7.BackColor = System.Drawing.Color.Transparent;
            this.labelT7.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT7.Location = new System.Drawing.Point(20, 553);
            this.labelT7.Name = "labelT7";
            this.labelT7.Size = new System.Drawing.Size(72, 16);
            this.labelT7.TabIndex = 0;
            this.labelT7.Text = "寄庫單號";
            // 
            // labelT8
            // 
            this.labelT8.AutoSize = true;
            this.labelT8.BackColor = System.Drawing.Color.Transparent;
            this.labelT8.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT8.Location = new System.Drawing.Point(378, 514);
            this.labelT8.Name = "labelT8";
            this.labelT8.Size = new System.Drawing.Size(72, 16);
            this.labelT8.TabIndex = 0;
            this.labelT8.Text = "領出日期";
            // 
            // labelT9
            // 
            this.labelT9.AutoSize = true;
            this.labelT9.BackColor = System.Drawing.Color.Transparent;
            this.labelT9.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT9.Location = new System.Drawing.Point(378, 553);
            this.labelT9.Name = "labelT9";
            this.labelT9.Size = new System.Drawing.Size(72, 16);
            this.labelT9.TabIndex = 0;
            this.labelT9.Text = "客戶編號";
            // 
            // labelT10
            // 
            this.labelT10.AutoSize = true;
            this.labelT10.BackColor = System.Drawing.Color.Transparent;
            this.labelT10.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT10.Location = new System.Drawing.Point(716, 514);
            this.labelT10.Name = "labelT10";
            this.labelT10.Size = new System.Drawing.Size(72, 16);
            this.labelT10.TabIndex = 0;
            this.labelT10.Text = "產品編號";
            // 
            // labelT11
            // 
            this.labelT11.AutoSize = true;
            this.labelT11.BackColor = System.Drawing.Color.Transparent;
            this.labelT11.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT11.Location = new System.Drawing.Point(716, 553);
            this.labelT11.Name = "labelT11";
            this.labelT11.Size = new System.Drawing.Size(72, 16);
            this.labelT11.TabIndex = 0;
            this.labelT11.Text = "倉庫編號";
            // 
            // touno
            // 
            this.touno.AllowGrayBackColor = false;
            this.touno.AllowResize = true;
            this.touno.Font = new System.Drawing.Font("細明體", 12F);
            this.touno.Location = new System.Drawing.Point(98, 511);
            this.touno.MaxLength = 16;
            this.touno.Name = "touno";
            this.touno.oLen = 0;
            this.touno.Size = new System.Drawing.Size(135, 27);
            this.touno.TabIndex = 14;
            // 
            // tinno
            // 
            this.tinno.AllowGrayBackColor = false;
            this.tinno.AllowResize = true;
            this.tinno.Font = new System.Drawing.Font("細明體", 12F);
            this.tinno.Location = new System.Drawing.Point(98, 550);
            this.tinno.MaxLength = 16;
            this.tinno.Name = "tinno";
            this.tinno.oLen = 0;
            this.tinno.Size = new System.Drawing.Size(135, 27);
            this.tinno.TabIndex = 15;
            // 
            // toudate
            // 
            this.toudate.AllowGrayBackColor = false;
            this.toudate.AllowResize = true;
            this.toudate.Font = new System.Drawing.Font("細明體", 12F);
            this.toudate.Location = new System.Drawing.Point(456, 511);
            this.toudate.MaxLength = 10;
            this.toudate.Name = "toudate";
            this.toudate.oLen = 0;
            this.toudate.Size = new System.Drawing.Size(87, 27);
            this.toudate.TabIndex = 16;
            this.toudate.Validating += new System.ComponentModel.CancelEventHandler(this.toudate_Validating);
            // 
            // tcuno
            // 
            this.tcuno.AllowGrayBackColor = false;
            this.tcuno.AllowResize = true;
            this.tcuno.Font = new System.Drawing.Font("細明體", 12F);
            this.tcuno.Location = new System.Drawing.Point(456, 550);
            this.tcuno.MaxLength = 10;
            this.tcuno.Name = "tcuno";
            this.tcuno.oLen = 0;
            this.tcuno.Size = new System.Drawing.Size(87, 27);
            this.tcuno.TabIndex = 17;
            this.tcuno.DoubleClick += new System.EventHandler(this.tcuno_DoubleClick);
            // 
            // titno
            // 
            this.titno.AllowGrayBackColor = false;
            this.titno.AllowResize = true;
            this.titno.Font = new System.Drawing.Font("細明體", 12F);
            this.titno.Location = new System.Drawing.Point(794, 511);
            this.titno.MaxLength = 20;
            this.titno.Name = "titno";
            this.titno.oLen = 0;
            this.titno.Size = new System.Drawing.Size(167, 27);
            this.titno.TabIndex = 18;
            this.titno.DoubleClick += new System.EventHandler(this.titno_DoubleClick);
            // 
            // tstno
            // 
            this.tstno.AllowGrayBackColor = false;
            this.tstno.AllowResize = true;
            this.tstno.Font = new System.Drawing.Font("細明體", 12F);
            this.tstno.Location = new System.Drawing.Point(794, 550);
            this.tstno.MaxLength = 10;
            this.tstno.Name = "tstno";
            this.tstno.oLen = 0;
            this.tstno.Size = new System.Drawing.Size(87, 27);
            this.tstno.TabIndex = 19;
            this.tstno.DoubleClick += new System.EventHandler(this.tstno_DoubleClick);
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(804, 586);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(142, 41);
            this.btnGet.TabIndex = 49;
            this.btnGet.Text = "F4:返回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnStock
            // 
            this.btnStock.Font = new System.Drawing.Font("細明體", 12F);
            this.btnStock.Location = new System.Drawing.Point(654, 586);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(142, 41);
            this.btnStock.TabIndex = 48;
            this.btnStock.Text = "F8:庫存查詢";
            this.btnStock.UseVisualStyleBackColor = true;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // btnBom
            // 
            this.btnBom.Font = new System.Drawing.Font("細明體", 12F);
            this.btnBom.Location = new System.Drawing.Point(504, 586);
            this.btnBom.Name = "btnBom";
            this.btnBom.Size = new System.Drawing.Size(142, 41);
            this.btnBom.TabIndex = 47;
            this.btnBom.Text = "F7:組件明細";
            this.btnBom.UseVisualStyleBackColor = true;
            this.btnBom.Click += new System.EventHandler(this.btnBom_Click);
            // 
            // btnDesp
            // 
            this.btnDesp.Font = new System.Drawing.Font("細明體", 12F);
            this.btnDesp.Location = new System.Drawing.Point(354, 586);
            this.btnDesp.Name = "btnDesp";
            this.btnDesp.Size = new System.Drawing.Size(142, 41);
            this.btnDesp.TabIndex = 46;
            this.btnDesp.Text = "F6:規格說明";
            this.btnDesp.UseVisualStyleBackColor = true;
            this.btnDesp.Click += new System.EventHandler(this.btnDesp_Click);
            // 
            // btnPicture
            // 
            this.btnPicture.Font = new System.Drawing.Font("細明體", 12F);
            this.btnPicture.Location = new System.Drawing.Point(204, 586);
            this.btnPicture.Name = "btnPicture";
            this.btnPicture.Size = new System.Drawing.Size(142, 41);
            this.btnPicture.TabIndex = 45;
            this.btnPicture.Text = "看圖";
            this.btnPicture.UseVisualStyleBackColor = true;
            this.btnPicture.Click += new System.EventHandler(this.btnPicture_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(54, 586);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(142, 41);
            this.btnQuery.TabIndex = 44;
            this.btnQuery.Text = "F3:查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
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
            // FrmOutStkBrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnGet;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.btnBom);
            this.Controls.Add(this.btnDesp);
            this.Controls.Add(this.btnPicture);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.tstno);
            this.Controls.Add(this.titno);
            this.Controls.Add(this.tcuno);
            this.Controls.Add(this.toudate);
            this.Controls.Add(this.tinno);
            this.Controls.Add(this.touno);
            this.Controls.Add(this.labelT11);
            this.Controls.Add(this.labelT10);
            this.Controls.Add(this.labelT9);
            this.Controls.Add(this.labelT8);
            this.Controls.Add(this.labelT7);
            this.Controls.Add(this.labelT6);
            this.Controls.Add(this.Q8);
            this.Controls.Add(this.Q7);
            this.Controls.Add(this.Q6);
            this.Controls.Add(this.Q5);
            this.Controls.Add(this.Q4);
            this.Controls.Add(this.Q3);
            this.Controls.Add(this.Q2);
            this.Controls.Add(this.textBoxT6);
            this.Controls.Add(this.textBoxT5);
            this.Controls.Add(this.textBoxT4);
            this.Controls.Add(this.textBoxT3);
            this.Controls.Add(this.textBoxT2);
            this.Controls.Add(this.textBoxT1);
            this.Controls.Add(this.labelT5);
            this.Controls.Add(this.labelT4);
            this.Controls.Add(this.labelT2);
            this.Controls.Add(this.labelT3);
            this.Controls.Add(this.labelT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Name = "FrmOutStkBrow";
            this.Text = "寄庫領出系統瀏覽";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOutStkBrow_FormClosing);
            this.Load += new System.EventHandler(this.FrmOutStkBrow_Load);
            this.Shown += new System.EventHandler(this.FrmOutStkBrow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.LabelT labelT3;
        private JE.MyControl.LabelT labelT4;
        private JE.MyControl.TextBoxT textBoxT1;
        private JE.MyControl.TextBoxT textBoxT2;
        private JE.MyControl.TextBoxT textBoxT3;
        private JE.MyControl.TextBoxT textBoxT4;
        private JE.MyControl.LabelT labelT5;
        private JE.MyControl.TextBoxT textBoxT5;
        private JE.MyControl.TextBoxT textBoxT6;
        private System.Windows.Forms.DataGridViewTextBoxColumn 領出單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 寄庫單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 領出日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private JE.MyControl.DataGridViewTextNumberT 領出數量;
        private JE.MyControl.DataGridViewTextNumberT 寄庫未領數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 寄庫日期;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品組成;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註說明;
        private JE.MyControl.ButtonSmallT Q2;
        private JE.MyControl.ButtonSmallT Q3;
        private JE.MyControl.ButtonSmallT Q4;
        private JE.MyControl.ButtonSmallT Q5;
        private JE.MyControl.ButtonSmallT Q6;
        private JE.MyControl.ButtonSmallT Q7;
        private JE.MyControl.ButtonSmallT Q8;
        private JE.MyControl.LabelT labelT6;
        private JE.MyControl.LabelT labelT7;
        private JE.MyControl.LabelT labelT8;
        private JE.MyControl.LabelT labelT9;
        private JE.MyControl.LabelT labelT10;
        private JE.MyControl.LabelT labelT11;
        private JE.MyControl.TextBoxT touno;
        private JE.MyControl.TextBoxT tinno;
        private JE.MyControl.TextBoxT toudate;
        private JE.MyControl.TextBoxT tcuno;
        private JE.MyControl.TextBoxT titno;
        private JE.MyControl.TextBoxT tstno;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnStock;
        private JE.MyControl.ButtonSmallT btnBom;
        private JE.MyControl.ButtonSmallT btnDesp;
        private JE.MyControl.ButtonSmallT btnPicture;
        private JE.MyControl.ButtonSmallT btnQuery;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}