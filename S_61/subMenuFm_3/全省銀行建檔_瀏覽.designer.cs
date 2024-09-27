namespace S_61.subMenuFm_3
{
    partial class 全省銀行建檔_瀏覽
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(全省銀行建檔_瀏覽));
            this.lblT1 = new JE.MyControl.LabelT();
            this.BaNo = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.BaName = new JE.MyControl.TextBoxT();
            this.Append = new JE.MyControl.ButtonSmallT();
            this.Get = new JE.MyControl.ButtonSmallT();
            this.Exit = new JE.MyControl.ButtonSmallT();
            this.btnBrowT1 = new JE.MyControl.ButtonSmallT();
            this.Query = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.銀行編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.銀行名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.電話 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.郵遞區號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.備註 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.qBaNo = new JE.MyControl.TextBoxT();
            this.qBaName = new JE.MyControl.TextBoxT();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.CN = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.Bank = new System.Data.SqlClient.SqlDataAdapter();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.tableLayoutPanelbase1 = new JE.MyControl.TableLayoutPanelbase();
            this.tableLayoutPanelbase2 = new JE.MyControl.TableLayoutPanelbase();
            this.tableLayoutPanelbase3 = new JE.MyControl.TableLayoutPanelbase();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.panelT1.SuspendLayout();
            this.tableLayoutPanelbase1.SuspendLayout();
            this.tableLayoutPanelbase2.SuspendLayout();
            this.tableLayoutPanelbase3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblT1
            // 
            this.lblT1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(217, 11);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "銀行編號";
            // 
            // BaNo
            // 
            this.BaNo.AllowGrayBackColor = false;
            this.BaNo.AllowResize = true;
            this.BaNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BaNo.Location = new System.Drawing.Point(295, 6);
            this.BaNo.MaxLength = 20;
            this.BaNo.Name = "BaNo";
            this.BaNo.oLen = 0;
            this.BaNo.Size = new System.Drawing.Size(167, 27);
            this.BaNo.TabIndex = 1;
            this.BaNo.TextChanged += new System.EventHandler(this.BaNo_TextChanged);
            // 
            // lblT2
            // 
            this.lblT2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(468, 11);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "銀行名稱";
            // 
            // BaName
            // 
            this.BaName.AllowGrayBackColor = false;
            this.BaName.AllowResize = true;
            this.BaName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BaName.Font = new System.Drawing.Font("細明體", 12F);
            this.BaName.Location = new System.Drawing.Point(546, 6);
            this.BaName.MaxLength = 30;
            this.BaName.Name = "BaName";
            this.BaName.oLen = 0;
            this.BaName.Size = new System.Drawing.Size(247, 27);
            this.BaName.TabIndex = 2;
            this.BaName.TextChanged += new System.EventHandler(this.BaName_TextChanged);
            // 
            // Append
            // 
            this.Append.Font = new System.Drawing.Font("細明體", 12F);
            this.Append.Location = new System.Drawing.Point(93, 3);
            this.Append.Name = "Append";
            this.Append.Size = new System.Drawing.Size(160, 44);
            this.Append.TabIndex = 1;
            this.Append.Text = "F2:新增";
            this.Append.UseVisualStyleBackColor = true;
            this.Append.Click += new System.EventHandler(this.Append_Click);
            // 
            // Get
            // 
            this.Get.Font = new System.Drawing.Font("細明體", 12F);
            this.Get.Location = new System.Drawing.Point(591, 3);
            this.Get.Name = "Get";
            this.Get.Size = new System.Drawing.Size(160, 44);
            this.Get.TabIndex = 4;
            this.Get.Text = "F9:取回";
            this.Get.UseVisualStyleBackColor = true;
            this.Get.Click += new System.EventHandler(this.Get_Click);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("細明體", 12F);
            this.Exit.Location = new System.Drawing.Point(757, 3);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(160, 44);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "F11:結束";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // btnBrowT1
            // 
            this.btnBrowT1.Font = new System.Drawing.Font("細明體", 12F);
            this.btnBrowT1.Location = new System.Drawing.Point(259, 3);
            this.btnBrowT1.Name = "btnBrowT1";
            this.btnBrowT1.Size = new System.Drawing.Size(160, 44);
            this.btnBrowT1.TabIndex = 2;
            this.btnBrowT1.Text = "F5:查詢";
            this.btnBrowT1.UseVisualStyleBackColor = true;
            this.btnBrowT1.Click += new System.EventHandler(this.btnBrowT1_Click);
            // 
            // Query
            // 
            this.Query.Font = new System.Drawing.Font("細明體", 12F);
            this.Query.Location = new System.Drawing.Point(425, 3);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(160, 44);
            this.Query.TabIndex = 3;
            this.Query.Text = "F6:字元查詢";
            this.Query.UseVisualStyleBackColor = true;
            this.Query.Click += new System.EventHandler(this.Query_Click);
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
            this.銀行編號,
            this.銀行名稱,
            this.電話,
            this.地址,
            this.郵遞區號,
            this.備註});
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
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 451);
            this.dataGridViewT1.TabIndex = 4;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            // 
            // 銀行編號
            // 
            this.銀行編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.銀行編號.DataPropertyName = "bano";
            this.銀行編號.HeaderText = "銀行編號";
            this.銀行編號.MaxInputLength = 20;
            this.銀行編號.Name = "銀行編號";
            this.銀行編號.ReadOnly = true;
            this.銀行編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.銀行編號.Width = 173;
            // 
            // 銀行名稱
            // 
            this.銀行名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.銀行名稱.DataPropertyName = "baname";
            this.銀行名稱.HeaderText = "銀行名稱";
            this.銀行名稱.MaxInputLength = 30;
            this.銀行名稱.Name = "銀行名稱";
            this.銀行名稱.ReadOnly = true;
            this.銀行名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.銀行名稱.Width = 253;
            // 
            // 電話
            // 
            this.電話.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.電話.DataPropertyName = "batel";
            this.電話.HeaderText = "電話";
            this.電話.MaxInputLength = 20;
            this.電話.Name = "電話";
            this.電話.ReadOnly = true;
            this.電話.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.電話.Width = 173;
            // 
            // 地址
            // 
            this.地址.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.地址.DataPropertyName = "baaddr1";
            this.地址.HeaderText = "地址";
            this.地址.MaxInputLength = 60;
            this.地址.Name = "地址";
            this.地址.ReadOnly = true;
            this.地址.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.地址.Width = 493;
            // 
            // 郵遞區號
            // 
            this.郵遞區號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.郵遞區號.DataPropertyName = "bar1";
            this.郵遞區號.HeaderText = "郵遞區號";
            this.郵遞區號.MaxInputLength = 5;
            this.郵遞區號.MinimumWidth = 100;
            this.郵遞區號.Name = "郵遞區號";
            this.郵遞區號.ReadOnly = true;
            this.郵遞區號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 備註
            // 
            this.備註.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.備註.DataPropertyName = "bamemo";
            this.備註.HeaderText = "備註";
            this.備註.MaxInputLength = 60;
            this.備註.Name = "備註";
            this.備註.ReadOnly = true;
            this.備註.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.備註.Width = 493;
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Location = new System.Drawing.Point(653, 2);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(286, 79);
            this.panelT1.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 9F);
            this.btnExit.Location = new System.Drawing.Point(207, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 12;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("細明體", 9F);
            this.btnCancel.Location = new System.Drawing.Point(138, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 79);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.UseDefaultSettings = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnSave.TabIndex = 10;
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
            this.btnModify.TabIndex = 6;
            this.btnModify.UseDefaultSettings = false;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // lblT3
            // 
            this.lblT3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(74, 35);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "銀行編號";
            // 
            // lblT4
            // 
            this.lblT4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(325, 35);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "銀行名稱";
            // 
            // qBaNo
            // 
            this.qBaNo.AllowGrayBackColor = false;
            this.qBaNo.AllowResize = true;
            this.qBaNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.qBaNo.Font = new System.Drawing.Font("細明體", 12F);
            this.qBaNo.Location = new System.Drawing.Point(152, 29);
            this.qBaNo.MaxLength = 20;
            this.qBaNo.Name = "qBaNo";
            this.qBaNo.oLen = 0;
            this.qBaNo.Size = new System.Drawing.Size(167, 27);
            this.qBaNo.TabIndex = 1;
            this.qBaNo.TextChanged += new System.EventHandler(this.qBaNo_TextChanged);
            // 
            // qBaName
            // 
            this.qBaName.AllowGrayBackColor = false;
            this.qBaName.AllowResize = true;
            this.qBaName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.qBaName.Font = new System.Drawing.Font("細明體", 12F);
            this.qBaName.Location = new System.Drawing.Point(403, 29);
            this.qBaName.MaxLength = 30;
            this.qBaName.Name = "qBaName";
            this.qBaName.oLen = 0;
            this.qBaName.Size = new System.Drawing.Size(247, 27);
            this.qBaName.TabIndex = 2;
            this.qBaName.TextChanged += new System.EventHandler(this.qBaName_TextChanged);
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "select * from  bank order by bano";
            this.sqlSelectCommand1.Connection = this.CN;
            // 
            // CN
            // 
            this.CN.ConnectionString = "Data Source=.;Initial Catalog=CHK;Integrated Security=True";
            this.CN.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.CN;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@bano", System.Data.SqlDbType.NVarChar, 0, "bano"),
            new System.Data.SqlClient.SqlParameter("@baname", System.Data.SqlDbType.NVarChar, 0, "baname"),
            new System.Data.SqlClient.SqlParameter("@batel", System.Data.SqlDbType.NVarChar, 0, "batel"),
            new System.Data.SqlClient.SqlParameter("@baaddr1", System.Data.SqlDbType.NVarChar, 0, "baaddr1"),
            new System.Data.SqlClient.SqlParameter("@bar1", System.Data.SqlDbType.NVarChar, 0, "bar1"),
            new System.Data.SqlClient.SqlParameter("@bamemo", System.Data.SqlDbType.NVarChar, 0, "bamemo")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.CN;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@bano", System.Data.SqlDbType.NVarChar, 0, "bano"),
            new System.Data.SqlClient.SqlParameter("@baname", System.Data.SqlDbType.NVarChar, 0, "baname"),
            new System.Data.SqlClient.SqlParameter("@batel", System.Data.SqlDbType.NVarChar, 0, "batel"),
            new System.Data.SqlClient.SqlParameter("@baaddr1", System.Data.SqlDbType.NVarChar, 0, "baaddr1"),
            new System.Data.SqlClient.SqlParameter("@bar1", System.Data.SqlDbType.NVarChar, 0, "bar1"),
            new System.Data.SqlClient.SqlParameter("@bamemo", System.Data.SqlDbType.NVarChar, 0, "bamemo"),
            new System.Data.SqlClient.SqlParameter("@Original_bano", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bano", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_baname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "baname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_baname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "baname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_batel", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "batel", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_batel", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "batel", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_baaddr1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "baaddr1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_baaddr1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "baaddr1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bar1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bar1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bar1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bar1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bamemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bamemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bamemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bamemo", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.CN;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_bano", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bano", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_baname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "baname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_baname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "baname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_batel", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "batel", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_batel", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "batel", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_baaddr1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "baaddr1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_baaddr1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "baaddr1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bar1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bar1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bar1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bar1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bamemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bamemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bamemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bamemo", System.Data.DataRowVersion.Original, null)});
            // 
            // Bank
            // 
            this.Bank.DeleteCommand = this.sqlDeleteCommand1;
            this.Bank.InsertCommand = this.sqlInsertCommand1;
            this.Bank.SelectCommand = this.sqlSelectCommand1;
            this.Bank.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "bank", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("bano", "bano"),
                        new System.Data.Common.DataColumnMapping("baname", "baname"),
                        new System.Data.Common.DataColumnMapping("batel", "batel"),
                        new System.Data.Common.DataColumnMapping("baaddr1", "baaddr1"),
                        new System.Data.Common.DataColumnMapping("bar1", "bar1"),
                        new System.Data.Common.DataColumnMapping("bamemo", "bamemo")})});
            this.Bank.UpdateCommand = this.sqlUpdateCommand1;
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
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelbase1.Controls.Add(this.Append, 1, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.btnBrowT1, 2, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.Query, 3, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.Get, 4, 0);
            this.tableLayoutPanelbase1.Controls.Add(this.Exit, 5, 0);
            this.tableLayoutPanelbase1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelbase1.Location = new System.Drawing.Point(0, 576);
            this.tableLayoutPanelbase1.Name = "tableLayoutPanelbase1";
            this.tableLayoutPanelbase1.RowCount = 1;
            this.tableLayoutPanelbase1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelbase1.Size = new System.Drawing.Size(1010, 50);
            this.tableLayoutPanelbase1.TabIndex = 3;
            // 
            // tableLayoutPanelbase2
            // 
            this.tableLayoutPanelbase2.ColumnCount = 6;
            this.tableLayoutPanelbase2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelbase2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelbase2.Controls.Add(this.BaNo, 2, 0);
            this.tableLayoutPanelbase2.Controls.Add(this.BaName, 4, 0);
            this.tableLayoutPanelbase2.Controls.Add(this.lblT1, 1, 0);
            this.tableLayoutPanelbase2.Controls.Add(this.lblT2, 3, 0);
            this.tableLayoutPanelbase2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelbase2.Location = new System.Drawing.Point(0, 537);
            this.tableLayoutPanelbase2.Name = "tableLayoutPanelbase2";
            this.tableLayoutPanelbase2.RowCount = 1;
            this.tableLayoutPanelbase2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelbase2.Size = new System.Drawing.Size(1010, 39);
            this.tableLayoutPanelbase2.TabIndex = 2;
            // 
            // tableLayoutPanelbase3
            // 
            this.tableLayoutPanelbase3.ColumnCount = 7;
            this.tableLayoutPanelbase3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelbase3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelbase3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelbase3.Controls.Add(this.lblT3, 1, 0);
            this.tableLayoutPanelbase3.Controls.Add(this.lblT4, 3, 0);
            this.tableLayoutPanelbase3.Controls.Add(this.qBaNo, 2, 0);
            this.tableLayoutPanelbase3.Controls.Add(this.panelT1, 5, 0);
            this.tableLayoutPanelbase3.Controls.Add(this.qBaName, 4, 0);
            this.tableLayoutPanelbase3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelbase3.Location = new System.Drawing.Point(0, 451);
            this.tableLayoutPanelbase3.Name = "tableLayoutPanelbase3";
            this.tableLayoutPanelbase3.RowCount = 1;
            this.tableLayoutPanelbase3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelbase3.Size = new System.Drawing.Size(1010, 86);
            this.tableLayoutPanelbase3.TabIndex = 1;
            // 
            // 全省銀行建檔_瀏覽
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.tableLayoutPanelbase3);
            this.Controls.Add(this.tableLayoutPanelbase2);
            this.Controls.Add(this.tableLayoutPanelbase1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "全省銀行建檔_瀏覽";
            this.Text = "全省銀行建檔[瀏覽]";
            this.Load += new System.EventHandler(this.全省銀行建檔_瀏覽_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.panelT1.ResumeLayout(false);
            this.tableLayoutPanelbase1.ResumeLayout(false);
            this.tableLayoutPanelbase2.ResumeLayout(false);
            this.tableLayoutPanelbase2.PerformLayout();
            this.tableLayoutPanelbase3.ResumeLayout(false);
            this.tableLayoutPanelbase3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.TextBoxT BaNo;
        private JE.MyControl.ButtonSmallT Append;
        private JE.MyControl.ButtonSmallT Get;
        private JE.MyControl.ButtonSmallT Exit;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT BaName;
        private JE.MyControl.ButtonSmallT btnBrowT1;
        private JE.MyControl.ButtonSmallT Query;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnCancel;
        private JE.MyControl.ButtonT btnSave;
        private JE.MyControl.ButtonT btnModify;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT qBaNo;
        private JE.MyControl.TextBoxT qBaName;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection CN;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter Bank;
        private System.Windows.Forms.DataGridViewTextBoxColumn 銀行編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 銀行名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 電話;
        private System.Windows.Forms.DataGridViewTextBoxColumn 地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 郵遞區號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPanelbase1;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPanelbase2;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPanelbase3;
    }
}