namespace S_61.subMenuFm_6
{
    partial class PosMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PosMessage));
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.起始日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.終止日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.標題 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.內文 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mgID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.Msg = new JE.MyControl.RichTextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.MgTitle = new JE.MyControl.TextBoxT();
            this.MgsDate = new JE.MyControl.TextBoxT();
            this.MgeDate = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnDelete = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.btnAppend = new JE.MyControl.ButtonT();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn1 = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.da1 = new System.Data.SqlClient.SqlDataAdapter();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
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
            this.起始日期,
            this.終止日期,
            this.標題,
            this.內文,
            this.mgID});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewT1.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
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
            this.dataGridViewT1.Size = new System.Drawing.Size(499, 539);
            this.dataGridViewT1.TabIndex = 3;
            this.dataGridViewT1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewT1_RowStateChanged);
            // 
            // 起始日期
            // 
            this.起始日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.起始日期.HeaderText = "起始日期";
            this.起始日期.MaxInputLength = 12;
            this.起始日期.Name = "起始日期";
            this.起始日期.ReadOnly = true;
            this.起始日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.起始日期.Width = 109;
            // 
            // 終止日期
            // 
            this.終止日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.終止日期.HeaderText = "起始日期";
            this.終止日期.MaxInputLength = 12;
            this.終止日期.Name = "終止日期";
            this.終止日期.ReadOnly = true;
            this.終止日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.終止日期.Width = 109;
            // 
            // 標題
            // 
            this.標題.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.標題.DataPropertyName = "mgTitle";
            this.標題.HeaderText = "標題";
            this.標題.MaxInputLength = 30;
            this.標題.Name = "標題";
            this.標題.ReadOnly = true;
            this.標題.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.標題.Width = 253;
            // 
            // 內文
            // 
            this.內文.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.內文.DataPropertyName = "message";
            this.內文.HeaderText = "內文";
            this.內文.MaxInputLength = 300;
            this.內文.Name = "內文";
            this.內文.ReadOnly = true;
            this.內文.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.內文.Visible = false;
            // 
            // mgID
            // 
            this.mgID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.mgID.DataPropertyName = "mgID";
            this.mgID.HeaderText = "mgID";
            this.mgID.MaxInputLength = 10;
            this.mgID.Name = "mgID";
            this.mgID.ReadOnly = true;
            this.mgID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mgID.Visible = false;
            this.mgID.Width = 93;
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.Msg);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.MgTitle);
            this.pnlBoxT1.Controls.Add(this.MgsDate);
            this.pnlBoxT1.Controls.Add(this.MgeDate);
            this.pnlBoxT1.Location = new System.Drawing.Point(500, 0);
            this.pnlBoxT1.Margin = new System.Windows.Forms.Padding(10);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(510, 540);
            this.pnlBoxT1.TabIndex = 2;
            // 
            // Msg
            // 
            this.Msg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Msg.Font = new System.Drawing.Font("細明體", 12F);
            this.Msg.Location = new System.Drawing.Point(33, 107);
            this.Msg.MaxLength = 300;
            this.Msg.Name = "Msg";
            this.Msg.ReadOnly = true;
            this.Msg.Size = new System.Drawing.Size(450, 415);
            this.Msg.TabIndex = 4;
            this.Msg.TabStop = false;
            this.Msg.Text = "";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(30, 85);
            this.lblT3.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "內  文：";
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(30, 23);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "起始日期";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(30, 54);
            this.lblT4.Margin = new System.Windows.Forms.Padding(15);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "標    題";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(218, 23);
            this.lblT2.Margin = new System.Windows.Forms.Padding(15);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "終止日期";
            // 
            // MgTitle
            // 
            this.MgTitle.AllowGrayBackColor = false;
            this.MgTitle.AllowResize = true;
            this.MgTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.MgTitle.Font = new System.Drawing.Font("細明體", 12F);
            this.MgTitle.Location = new System.Drawing.Point(108, 51);
            this.MgTitle.MaxLength = 30;
            this.MgTitle.Name = "MgTitle";
            this.MgTitle.oLen = 0;
            this.MgTitle.ReadOnly = true;
            this.MgTitle.Size = new System.Drawing.Size(247, 27);
            this.MgTitle.TabIndex = 3;
            this.MgTitle.TabStop = false;
            // 
            // MgsDate
            // 
            this.MgsDate.AllowGrayBackColor = false;
            this.MgsDate.AllowResize = true;
            this.MgsDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.MgsDate.Font = new System.Drawing.Font("細明體", 12F);
            this.MgsDate.Location = new System.Drawing.Point(108, 18);
            this.MgsDate.MaxLength = 10;
            this.MgsDate.Name = "MgsDate";
            this.MgsDate.oLen = 0;
            this.MgsDate.ReadOnly = true;
            this.MgsDate.Size = new System.Drawing.Size(87, 27);
            this.MgsDate.TabIndex = 1;
            this.MgsDate.TabStop = false;
            this.MgsDate.Validating += new System.ComponentModel.CancelEventHandler(this.MgsDate_Validating);
            // 
            // MgeDate
            // 
            this.MgeDate.AllowGrayBackColor = false;
            this.MgeDate.AllowResize = true;
            this.MgeDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.MgeDate.Font = new System.Drawing.Font("細明體", 12F);
            this.MgeDate.Location = new System.Drawing.Point(294, 18);
            this.MgeDate.MaxLength = 10;
            this.MgeDate.Name = "MgeDate";
            this.MgeDate.oLen = 0;
            this.MgeDate.ReadOnly = true;
            this.MgeDate.Size = new System.Drawing.Size(87, 27);
            this.MgeDate.TabIndex = 2;
            this.MgeDate.TabStop = false;
            this.MgeDate.Validating += new System.ComponentModel.CancelEventHandler(this.MgeDate_Validating);
            // 
            // panelT1
            // 
            this.panelT1.AutoSize = true;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnDelete);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Controls.Add(this.btnAppend);
            this.panelT1.Location = new System.Drawing.Point(293, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(424, 82);
            this.panelT1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(345, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 5;
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
            this.btnCancel.Location = new System.Drawing.Point(276, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 79);
            this.btnCancel.TabIndex = 4;
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
            this.btnSave.Location = new System.Drawing.Point(207, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 79);
            this.btnSave.TabIndex = 3;
            this.btnSave.UseDefaultSettings = false;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(138, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 79);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.UseDefaultSettings = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.SystemColors.Control;
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModify.Location = new System.Drawing.Point(69, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(69, 79);
            this.btnModify.TabIndex = 1;
            this.btnModify.UseDefaultSettings = false;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAppend
            // 
            this.btnAppend.BackColor = System.Drawing.SystemColors.Control;
            this.btnAppend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAppend.BackgroundImage")));
            this.btnAppend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAppend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAppend.Location = new System.Drawing.Point(0, 0);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(69, 79);
            this.btnAppend.TabIndex = 0;
            this.btnAppend.UseDefaultSettings = false;
            this.btnAppend.UseVisualStyleBackColor = false;
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "select * from posmsg";
            this.sqlSelectCommand1.Connection = this.cn1;
            // 
            // cn1
            // 
            this.cn1.ConnectionString = "Data Source=.;Initial Catalog=74;Integrated Security=True";
            this.cn1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.cn1;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@mgsdate", System.Data.SqlDbType.NVarChar, 0, "mgsdate"),
            new System.Data.SqlClient.SqlParameter("@mgsdate1", System.Data.SqlDbType.NVarChar, 0, "mgsdate1"),
            new System.Data.SqlClient.SqlParameter("@mgsdate2", System.Data.SqlDbType.NVarChar, 0, "mgsdate2"),
            new System.Data.SqlClient.SqlParameter("@mgedate", System.Data.SqlDbType.NVarChar, 0, "mgedate"),
            new System.Data.SqlClient.SqlParameter("@mgedate1", System.Data.SqlDbType.NVarChar, 0, "mgedate1"),
            new System.Data.SqlClient.SqlParameter("@mgedate2", System.Data.SqlDbType.NVarChar, 0, "mgedate2"),
            new System.Data.SqlClient.SqlParameter("@mgTitle", System.Data.SqlDbType.NVarChar, 0, "mgTitle"),
            new System.Data.SqlClient.SqlParameter("@message", System.Data.SqlDbType.NVarChar, 0, "message"),
            new System.Data.SqlClient.SqlParameter("@AppNo", System.Data.SqlDbType.NVarChar, 0, "AppNo")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.cn1;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@mgsdate", System.Data.SqlDbType.NVarChar, 0, "mgsdate"),
            new System.Data.SqlClient.SqlParameter("@mgsdate1", System.Data.SqlDbType.NVarChar, 0, "mgsdate1"),
            new System.Data.SqlClient.SqlParameter("@mgsdate2", System.Data.SqlDbType.NVarChar, 0, "mgsdate2"),
            new System.Data.SqlClient.SqlParameter("@mgedate", System.Data.SqlDbType.NVarChar, 0, "mgedate"),
            new System.Data.SqlClient.SqlParameter("@mgedate1", System.Data.SqlDbType.NVarChar, 0, "mgedate1"),
            new System.Data.SqlClient.SqlParameter("@mgedate2", System.Data.SqlDbType.NVarChar, 0, "mgedate2"),
            new System.Data.SqlClient.SqlParameter("@mgTitle", System.Data.SqlDbType.NVarChar, 0, "mgTitle"),
            new System.Data.SqlClient.SqlParameter("@message", System.Data.SqlDbType.NVarChar, 0, "message"),
            new System.Data.SqlClient.SqlParameter("@AppNo", System.Data.SqlDbType.NVarChar, 0, "AppNo"),
            new System.Data.SqlClient.SqlParameter("@Original_mgID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgsdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgsdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgsdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgsdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgsdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgsdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgsdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgsdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgsdate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgsdate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgsdate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgsdate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgedate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgedate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgedate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgedate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgedate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgedate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgedate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgedate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgedate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgedate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgedate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgedate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgTitle", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgTitle", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgTitle", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgTitle", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_message", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "message", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_message", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "message", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AppNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AppNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AppNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AppNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@mgID", System.Data.SqlDbType.Int, 4, "mgID")});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.cn1;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_mgID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgsdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgsdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgsdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgsdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgsdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgsdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgsdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgsdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgsdate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgsdate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgsdate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgsdate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgedate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgedate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgedate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgedate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgedate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgedate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgedate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgedate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgedate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgedate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgedate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgedate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mgTitle", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mgTitle", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mgTitle", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "mgTitle", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_message", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "message", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_message", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "message", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AppNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AppNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AppNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AppNo", System.Data.DataRowVersion.Original, null)});
            // 
            // da1
            // 
            this.da1.DeleteCommand = this.sqlDeleteCommand1;
            this.da1.InsertCommand = this.sqlInsertCommand1;
            this.da1.SelectCommand = this.sqlSelectCommand1;
            this.da1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PosMsg", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("mgID", "mgID"),
                        new System.Data.Common.DataColumnMapping("mgsdate", "mgsdate"),
                        new System.Data.Common.DataColumnMapping("mgsdate1", "mgsdate1"),
                        new System.Data.Common.DataColumnMapping("mgsdate2", "mgsdate2"),
                        new System.Data.Common.DataColumnMapping("mgedate", "mgedate"),
                        new System.Data.Common.DataColumnMapping("mgedate1", "mgedate1"),
                        new System.Data.Common.DataColumnMapping("mgedate2", "mgedate2"),
                        new System.Data.Common.DataColumnMapping("mgTitle", "mgTitle"),
                        new System.Data.Common.DataColumnMapping("message", "message"),
                        new System.Data.Common.DataColumnMapping("AppNo", "AppNo")})});
            this.da1.UpdateCommand = this.sqlUpdateCommand1;
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
            // PosMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelT1);
            this.Name = "PosMessage";
            this.Text = "最新消息建檔作業";
            this.Load += new System.EventHandler(this.PosMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnCancel;
        private JE.MyControl.ButtonT btnSave;
        private JE.MyControl.ButtonT btnModify;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT MgsDate;
        private JE.MyControl.TextBoxT MgeDate;
        private JE.MyControl.RichTextBoxT Msg;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection cn1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter da1;
        private JE.MyControl.ButtonT btnAppend;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT MgTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn 起始日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 終止日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 標題;
        private System.Windows.Forms.DataGridViewTextBoxColumn 內文;
        private System.Windows.Forms.DataGridViewTextBoxColumn mgID;
        private JE.MyControl.ButtonT btnDelete;
        private JE.MyControl.StatusStripT statusStripT1; 
    }
}