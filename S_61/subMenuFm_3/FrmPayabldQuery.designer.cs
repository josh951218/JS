namespace S_61.subMenuFm_3
{
    partial class FrmPayabldQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cn = new System.Data.SqlClient.SqlConnection();
            this.labelT1 = new JE.MyControl.LabelT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.qPano = new JE.MyControl.TextBoxT();
            this.qFano = new JE.MyControl.TextBoxT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnCancel = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.付款憑證 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.付款日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.廠商編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.廠商簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.沖抵帳款 = new JE.MyControl.DataGridViewTextNumberT();
            this.累入預付 = new JE.MyControl.DataGridViewTextNumberT();
            this.dataGridViewT2 = new JE.MyControl.DataGridViewT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.btnQoo = new JE.MyControl.ButtonSmallT();
            this.daM = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlCommand3 = new System.Data.SqlClient.SqlCommand();
            this.daD = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlCommand2 = new System.Data.SqlClient.SqlCommand();
            this.點選 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.帳款日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單據 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單據號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.幣別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.折讓金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.沖帳金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.未付金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.單據總計 = new JE.MyControl.DataGridViewTextNumberT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).BeginInit();
            this.SuspendLayout();
            // 
            // cn
            // 
            this.cn.ConnectionString = "Data Source=.;Initial Catalog=LONG;Integrated Security=True";
            this.cn.FireInfoMessageEventOnUserErrors = false;
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(7, 21);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "付款憑證";
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(214, 21);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(72, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "廠商編號";
            // 
            // qPano
            // 
            this.qPano.AllowGrayBackColor = false;
            this.qPano.AllowResize = true;
            this.qPano.Font = new System.Drawing.Font("細明體", 12F);
            this.qPano.Location = new System.Drawing.Point(79, 16);
            this.qPano.MaxLength = 16;
            this.qPano.Name = "qPano";
            this.qPano.oLen = 0;
            this.qPano.Size = new System.Drawing.Size(135, 27);
            this.qPano.TabIndex = 1;
            this.qPano.TextChanged += new System.EventHandler(this.qReno_TextChanged);
            this.qPano.Validating += new System.ComponentModel.CancelEventHandler(this.qFano_Validating);
            // 
            // qFano
            // 
            this.qFano.AllowGrayBackColor = false;
            this.qFano.AllowResize = true;
            this.qFano.Font = new System.Drawing.Font("細明體", 12F);
            this.qFano.Location = new System.Drawing.Point(286, 16);
            this.qFano.MaxLength = 10;
            this.qFano.Name = "qFano";
            this.qFano.oLen = 0;
            this.qFano.Size = new System.Drawing.Size(87, 27);
            this.qFano.TabIndex = 2;
            this.qFano.TextChanged += new System.EventHandler(this.qFano_TextChanged);
            this.qFano.DoubleClick += new System.EventHandler(this.qFano_DoubleClick);
            this.qFano.Validating += new System.ComponentModel.CancelEventHandler(this.qFano_Validating);
            // 
            // btnGet
            // 
            this.btnGet.AutoSize = true;
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(587, 11);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(191, 36);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "F9：取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCancel.Location = new System.Drawing.Point(778, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(191, 36);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "F4：放棄";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.付款憑證,
            this.付款日期,
            this.廠商編號,
            this.廠商簡稱,
            this.沖抵帳款,
            this.累入預付});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 57);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1009, 285);
            this.dataGridViewT1.TabIndex = 4;
            this.dataGridViewT1.TabStop = false;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            this.dataGridViewT1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewT1_RowStateChanged);
            // 
            // 付款憑證
            // 
            this.付款憑證.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.付款憑證.DataPropertyName = "PaNo";
            this.付款憑證.HeaderText = "付款憑證";
            this.付款憑證.MaxInputLength = 16;
            this.付款憑證.Name = "付款憑證";
            this.付款憑證.ReadOnly = true;
            this.付款憑證.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.付款憑證.Width = 141;
            // 
            // 付款日期
            // 
            this.付款日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.付款日期.HeaderText = "付款日期";
            this.付款日期.MaxInputLength = 10;
            this.付款日期.Name = "付款日期";
            this.付款日期.ReadOnly = true;
            this.付款日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.付款日期.Width = 93;
            // 
            // 廠商編號
            // 
            this.廠商編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.廠商編號.DataPropertyName = "fano";
            this.廠商編號.HeaderText = "廠商編號";
            this.廠商編號.MaxInputLength = 10;
            this.廠商編號.Name = "廠商編號";
            this.廠商編號.ReadOnly = true;
            this.廠商編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.廠商編號.Width = 93;
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
            // 沖抵帳款
            // 
            this.沖抵帳款.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.沖抵帳款.DataPropertyName = "TotMny";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.沖抵帳款.DefaultCellStyle = dataGridViewCellStyle2;
            this.沖抵帳款.FirstNum = 0;
            this.沖抵帳款.HeaderText = "沖抵帳款";
            this.沖抵帳款.LastNum = 0;
            this.沖抵帳款.MarkThousand = false;
            this.沖抵帳款.MaxInputLength = 16;
            this.沖抵帳款.Name = "沖抵帳款";
            this.沖抵帳款.NullInput = false;
            this.沖抵帳款.NullValue = "0";
            this.沖抵帳款.ReadOnly = true;
            this.沖抵帳款.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.沖抵帳款.Width = 141;
            // 
            // 累入預付
            // 
            this.累入預付.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.累入預付.DataPropertyName = "AddPrvAcc";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.累入預付.DefaultCellStyle = dataGridViewCellStyle3;
            this.累入預付.FirstNum = 0;
            this.累入預付.HeaderText = "累入預付";
            this.累入預付.LastNum = 0;
            this.累入預付.MarkThousand = false;
            this.累入預付.MaxInputLength = 16;
            this.累入預付.Name = "累入預付";
            this.累入預付.NullInput = false;
            this.累入預付.NullValue = "0";
            this.累入預付.ReadOnly = true;
            this.累入預付.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.累入預付.Width = 141;
            // 
            // dataGridViewT2
            // 
            this.dataGridViewT2.AllowUserToAddRows = false;
            this.dataGridViewT2.AllowUserToDeleteRows = false;
            this.dataGridViewT2.AllowUserToOrderColumns = true;
            this.dataGridViewT2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewT2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewT2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewT2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.點選,
            this.序號,
            this.帳款日期,
            this.單據,
            this.單據號碼,
            this.發票號碼,
            this.幣別,
            this.折讓金額,
            this.沖帳金額,
            this.未付金額,
            this.單據總計});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewT2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT2.EnableHeadersVisualStyles = false;
            this.dataGridViewT2.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT2.ISDocument = false;
            this.dataGridViewT2.Location = new System.Drawing.Point(0, 348);
            this.dataGridViewT2.MultiSelect = false;
            this.dataGridViewT2.Name = "dataGridViewT2";
            this.dataGridViewT2.ReadOnly = true;
            this.dataGridViewT2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT2.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewT2.RowHeadersWidth = 20;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT2.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewT2.RowTemplate.Height = 24;
            this.dataGridViewT2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT2.ShowCellToolTips = false;
            this.dataGridViewT2.Size = new System.Drawing.Size(1009, 275);
            this.dataGridViewT2.TabIndex = 5;
            this.dataGridViewT2.TabStop = false;
            this.dataGridViewT2.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewT2_RowPrePaint);
            // 
            // statusStripT1
            // 
            this.statusStripT1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStripT1.Location = new System.Drawing.Point(0, 625);
            this.statusStripT1.Name = "statusStripT1";
            this.statusStripT1.Size = new System.Drawing.Size(1010, 22);
            this.statusStripT1.TabIndex = 0;
            this.statusStripT1.Text = "statusStripT1";
            // 
            // btnQoo
            // 
            this.btnQoo.AutoSize = true;
            this.btnQoo.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQoo.Location = new System.Drawing.Point(396, 11);
            this.btnQoo.Name = "btnQoo";
            this.btnQoo.Size = new System.Drawing.Size(191, 36);
            this.btnQoo.TabIndex = 3;
            this.btnQoo.Text = "F3：查詢";
            this.btnQoo.UseVisualStyleBackColor = true;
            this.btnQoo.Visible = false;
            // 
            // daM
            // 
            this.daM.SelectCommand = this.sqlCommand3;
            this.daM.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "payabl", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("pano", "pano"),
                        new System.Data.Common.DataColumnMapping("padate", "padate"),
                        new System.Data.Common.DataColumnMapping("padate1", "padate1"),
                        new System.Data.Common.DataColumnMapping("padate2", "padate2"),
                        new System.Data.Common.DataColumnMapping("padateacs", "padateacs"),
                        new System.Data.Common.DataColumnMapping("padateacs1", "padateacs1"),
                        new System.Data.Common.DataColumnMapping("padateacs2", "padateacs2"),
                        new System.Data.Common.DataColumnMapping("padateace", "padateace"),
                        new System.Data.Common.DataColumnMapping("padateace1", "padateace1"),
                        new System.Data.Common.DataColumnMapping("padateace2", "padateace2"),
                        new System.Data.Common.DataColumnMapping("cono", "cono"),
                        new System.Data.Common.DataColumnMapping("coname1", "coname1"),
                        new System.Data.Common.DataColumnMapping("coname2", "coname2"),
                        new System.Data.Common.DataColumnMapping("fano", "fano"),
                        new System.Data.Common.DataColumnMapping("faname2", "faname2"),
                        new System.Data.Common.DataColumnMapping("faname1", "faname1"),
                        new System.Data.Common.DataColumnMapping("fatel1", "fatel1"),
                        new System.Data.Common.DataColumnMapping("emno", "emno"),
                        new System.Data.Common.DataColumnMapping("emname", "emname"),
                        new System.Data.Common.DataColumnMapping("cashmny", "cashmny"),
                        new System.Data.Common.DataColumnMapping("cardmny", "cardmny"),
                        new System.Data.Common.DataColumnMapping("cardno", "cardno"),
                        new System.Data.Common.DataColumnMapping("ticket", "ticket"),
                        new System.Data.Common.DataColumnMapping("checkmny", "checkmny"),
                        new System.Data.Common.DataColumnMapping("remitmny", "remitmny"),
                        new System.Data.Common.DataColumnMapping("othermny", "othermny"),
                        new System.Data.Common.DataColumnMapping("getprvacc", "getprvacc"),
                        new System.Data.Common.DataColumnMapping("addprvacc", "addprvacc"),
                        new System.Data.Common.DataColumnMapping("totmny", "totmny"),
                        new System.Data.Common.DataColumnMapping("actslt", "actslt"),
                        new System.Data.Common.DataColumnMapping("totdisc", "totdisc"),
                        new System.Data.Common.DataColumnMapping("totreve", "totreve"),
                        new System.Data.Common.DataColumnMapping("totmny1", "totmny1"),
                        new System.Data.Common.DataColumnMapping("totexgdiff", "totexgdiff"),
                        new System.Data.Common.DataColumnMapping("memo1", "memo1"),
                        new System.Data.Common.DataColumnMapping("memo2", "memo2"),
                        new System.Data.Common.DataColumnMapping("acno", "acno"),
                        new System.Data.Common.DataColumnMapping("bsno", "bsno"),
                        new System.Data.Common.DataColumnMapping("bracket", "bracket"),
                        new System.Data.Common.DataColumnMapping("recordno", "recordno"),
                        new System.Data.Common.DataColumnMapping("cloflag", "cloflag"),
                        new System.Data.Common.DataColumnMapping("ExtFlag", "ExtFlag"),
                        new System.Data.Common.DataColumnMapping("DeNo", "DeNo"),
                        new System.Data.Common.DataColumnMapping("DeName", "DeName"),
                        new System.Data.Common.DataColumnMapping("SpNo", "SpNo"),
                        new System.Data.Common.DataColumnMapping("accono", "accono"),
                        new System.Data.Common.DataColumnMapping("xa1par", "xa1par")})});
            // 
            // sqlCommand3
            // 
            this.sqlCommand3.CommandText = "select * from payabl order by pano desc";
            this.sqlCommand3.Connection = this.cn;
            // 
            // daD
            // 
            this.daD.SelectCommand = this.sqlCommand2;
            this.daD.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "payabld", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("pano", "pano"),
                        new System.Data.Common.DataColumnMapping("padate", "padate"),
                        new System.Data.Common.DataColumnMapping("padate1", "padate1"),
                        new System.Data.Common.DataColumnMapping("padate2", "padate2"),
                        new System.Data.Common.DataColumnMapping("padateacs", "padateacs"),
                        new System.Data.Common.DataColumnMapping("padateacs1", "padateacs1"),
                        new System.Data.Common.DataColumnMapping("padateacs2", "padateacs2"),
                        new System.Data.Common.DataColumnMapping("padateace", "padateace"),
                        new System.Data.Common.DataColumnMapping("padateace1", "padateace1"),
                        new System.Data.Common.DataColumnMapping("padateace2", "padateace2"),
                        new System.Data.Common.DataColumnMapping("cono", "cono"),
                        new System.Data.Common.DataColumnMapping("coname1", "coname1"),
                        new System.Data.Common.DataColumnMapping("coname2", "coname2"),
                        new System.Data.Common.DataColumnMapping("fano", "fano"),
                        new System.Data.Common.DataColumnMapping("faname2", "faname2"),
                        new System.Data.Common.DataColumnMapping("faname1", "faname1"),
                        new System.Data.Common.DataColumnMapping("fatel1", "fatel1"),
                        new System.Data.Common.DataColumnMapping("emno", "emno"),
                        new System.Data.Common.DataColumnMapping("emname", "emname"),
                        new System.Data.Common.DataColumnMapping("recordno", "recordno"),
                        new System.Data.Common.DataColumnMapping("bsdateac", "bsdateac"),
                        new System.Data.Common.DataColumnMapping("bsdateac1", "bsdateac1"),
                        new System.Data.Common.DataColumnMapping("bsdateac2", "bsdateac2"),
                        new System.Data.Common.DataColumnMapping("bsno", "bsno"),
                        new System.Data.Common.DataColumnMapping("bracket", "bracket"),
                        new System.Data.Common.DataColumnMapping("xa1no", "xa1no"),
                        new System.Data.Common.DataColumnMapping("xa1name", "xa1name"),
                        new System.Data.Common.DataColumnMapping("xa1par", "xa1par"),
                        new System.Data.Common.DataColumnMapping("totmny", "totmny"),
                        new System.Data.Common.DataColumnMapping("acctmny", "acctmny"),
                        new System.Data.Common.DataColumnMapping("invno", "invno"),
                        new System.Data.Common.DataColumnMapping("discount", "discount"),
                        new System.Data.Common.DataColumnMapping("reverse", "reverse"),
                        new System.Data.Common.DataColumnMapping("xa1par1", "xa1par1"),
                        new System.Data.Common.DataColumnMapping("reverseb", "reverseb"),
                        new System.Data.Common.DataColumnMapping("exgstat", "exgstat"),
                        new System.Data.Common.DataColumnMapping("exgdiff", "exgdiff"),
                        new System.Data.Common.DataColumnMapping("memo1", "memo1"),
                        new System.Data.Common.DataColumnMapping("extflag", "extflag")})});
            // 
            // sqlCommand2
            // 
            this.sqlCommand2.CommandText = "select * from payabld where (pano=@No)";
            this.sqlCommand2.Connection = this.cn;
            this.sqlCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@No", System.Data.SqlDbType.NVarChar, 12, "pano")});
            // 
            // 點選
            // 
            this.點選.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.點選.HeaderText = "點選";
            this.點選.Name = "點選";
            this.點選.ReadOnly = true;
            this.點選.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.點選.Visible = false;
            // 
            // 序號
            // 
            this.序號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.序號.DataPropertyName = "序號";
            this.序號.HeaderText = "序號";
            this.序號.MaxInputLength = 4;
            this.序號.Name = "序號";
            this.序號.ReadOnly = true;
            this.序號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.序號.Width = 45;
            // 
            // 帳款日期
            // 
            this.帳款日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.帳款日期.DataPropertyName = "帳款日期";
            this.帳款日期.HeaderText = "帳款日期";
            this.帳款日期.MaxInputLength = 10;
            this.帳款日期.Name = "帳款日期";
            this.帳款日期.ReadOnly = true;
            this.帳款日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.帳款日期.Width = 93;
            // 
            // 單據
            // 
            this.單據.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單據.DataPropertyName = "bracket";
            this.單據.HeaderText = "單據";
            this.單據.MaxInputLength = 4;
            this.單據.Name = "單據";
            this.單據.ReadOnly = true;
            this.單據.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單據.Width = 45;
            // 
            // 單據號碼
            // 
            this.單據號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單據號碼.DataPropertyName = "sano";
            this.單據號碼.HeaderText = "單據號碼";
            this.單據號碼.MaxInputLength = 14;
            this.單據號碼.Name = "單據號碼";
            this.單據號碼.ReadOnly = true;
            this.單據號碼.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單據號碼.Width = 125;
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
            // 幣別
            // 
            this.幣別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.幣別.DataPropertyName = "xa1name";
            this.幣別.HeaderText = "幣別";
            this.幣別.MaxInputLength = 12;
            this.幣別.Name = "幣別";
            this.幣別.ReadOnly = true;
            this.幣別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.幣別.Width = 109;
            // 
            // 折讓金額
            // 
            this.折讓金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折讓金額.DataPropertyName = "Discount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.折讓金額.DefaultCellStyle = dataGridViewCellStyle8;
            this.折讓金額.FirstNum = 0;
            this.折讓金額.HeaderText = "折讓金額";
            this.折讓金額.LastNum = 0;
            this.折讓金額.MarkThousand = false;
            this.折讓金額.MaxInputLength = 16;
            this.折讓金額.Name = "折讓金額";
            this.折讓金額.NullInput = false;
            this.折讓金額.NullValue = "0";
            this.折讓金額.ReadOnly = true;
            this.折讓金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.折讓金額.Width = 141;
            // 
            // 沖帳金額
            // 
            this.沖帳金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.沖帳金額.DataPropertyName = "Reverse";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.沖帳金額.DefaultCellStyle = dataGridViewCellStyle9;
            this.沖帳金額.FirstNum = 0;
            this.沖帳金額.HeaderText = "沖帳金額";
            this.沖帳金額.LastNum = 0;
            this.沖帳金額.MarkThousand = false;
            this.沖帳金額.MaxInputLength = 16;
            this.沖帳金額.Name = "沖帳金額";
            this.沖帳金額.NullInput = false;
            this.沖帳金額.NullValue = "0";
            this.沖帳金額.ReadOnly = true;
            this.沖帳金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.沖帳金額.Width = 141;
            // 
            // 未付金額
            // 
            this.未付金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.未付金額.DataPropertyName = "AcctMny";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.未付金額.DefaultCellStyle = dataGridViewCellStyle10;
            this.未付金額.FirstNum = 0;
            this.未付金額.HeaderText = "未付金額";
            this.未付金額.LastNum = 0;
            this.未付金額.MarkThousand = false;
            this.未付金額.MaxInputLength = 16;
            this.未付金額.Name = "未付金額";
            this.未付金額.NullInput = false;
            this.未付金額.NullValue = "0";
            this.未付金額.ReadOnly = true;
            this.未付金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.未付金額.Width = 141;
            // 
            // 單據總計
            // 
            this.單據總計.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單據總計.DataPropertyName = "TotMny";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單據總計.DefaultCellStyle = dataGridViewCellStyle11;
            this.單據總計.FirstNum = 0;
            this.單據總計.HeaderText = "單據總計";
            this.單據總計.LastNum = 0;
            this.單據總計.MarkThousand = false;
            this.單據總計.MaxInputLength = 16;
            this.單據總計.Name = "單據總計";
            this.單據總計.NullInput = false;
            this.單據總計.NullValue = "0";
            this.單據總計.ReadOnly = true;
            this.單據總計.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單據總計.Width = 141;
            // 
            // FrmPayabldQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.btnQoo);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.dataGridViewT2);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.qFano);
            this.Controls.Add(this.qPano);
            this.Controls.Add(this.labelT2);
            this.Controls.Add(this.labelT1);
            this.Name = "FrmPayabldQuery";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmReceivdQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Data.SqlClient.SqlConnection cn;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.TextBoxT qPano;
        private JE.MyControl.TextBoxT qFano;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnCancel;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.DataGridViewT dataGridViewT2;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 付款憑證;
        private System.Windows.Forms.DataGridViewTextBoxColumn 付款日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 廠商簡稱;
        private JE.MyControl.DataGridViewTextNumberT 沖抵帳款;
        private JE.MyControl.DataGridViewTextNumberT 累入預付;
        private JE.MyControl.ButtonSmallT btnQoo;
        private System.Data.SqlClient.SqlDataAdapter daM;
        private System.Data.SqlClient.SqlCommand sqlCommand3;
        private System.Data.SqlClient.SqlDataAdapter daD;
        private System.Data.SqlClient.SqlCommand sqlCommand2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 點選;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 帳款日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單據;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單據號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 幣別;
        private JE.MyControl.DataGridViewTextNumberT 折讓金額;
        private JE.MyControl.DataGridViewTextNumberT 沖帳金額;
        private JE.MyControl.DataGridViewTextNumberT 未付金額;
        private JE.MyControl.DataGridViewTextNumberT 單據總計;
    }
}