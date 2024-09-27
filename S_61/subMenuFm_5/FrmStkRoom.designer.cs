namespace S_61.SOther
{
    partial class FrmStkRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStkRoom));
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.btnDelete = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.btnAppend = new JE.MyControl.ButtonT();
            this.btnBottom = new JE.MyControl.ButtonT();
            this.btnNext = new JE.MyControl.ButtonT();
            this.btnPrior = new JE.MyControl.ButtonT();
            this.btnTop = new JE.MyControl.ButtonT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.panelNT1 = new JE.MyControl.PanelNT();
            this.StTrait1 = new JE.MyControl.RadioT();
            this.StTrait2 = new JE.MyControl.RadioT();
            this.StTrait4 = new JE.MyControl.RadioT();
            this.StTrait3 = new JE.MyControl.RadioT();
            this.StName = new JE.MyControl.TextBoxT();
            this.lblStNo = new JE.MyControl.LabelT();
            this.lblStTrait = new JE.MyControl.LabelT();
            this.lblStName = new JE.MyControl.LabelT();
            this.StNo = new JE.MyControl.TextBoxT();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.da1 = new System.Data.SqlClient.SqlDataAdapter();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelT1.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            this.panelNT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Controls.Add(this.btnDelete);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Controls.Add(this.btnAppend);
            this.panelT1.Controls.Add(this.btnBottom);
            this.panelT1.Controls.Add(this.btnNext);
            this.panelT1.Controls.Add(this.btnPrior);
            this.panelT1.Controls.Add(this.btnTop);
            this.panelT1.Location = new System.Drawing.Point(121, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(769, 79);
            this.panelT1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("細明體", 9F);
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(690, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 10;
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
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Location = new System.Drawing.Point(621, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 79);
            this.btnCancel.TabIndex = 9;
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
            this.btnSave.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(552, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 79);
            this.btnSave.TabIndex = 8;
            this.btnSave.UseDefaultSettings = false;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBrow
            // 
            this.btnBrow.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrow.BackgroundImage")));
            this.btnBrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrow.Font = new System.Drawing.Font("細明體", 9F);
            this.btnBrow.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBrow.Location = new System.Drawing.Point(483, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 7;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("細明體", 9F);
            this.btnDelete.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Location = new System.Drawing.Point(414, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 79);
            this.btnDelete.TabIndex = 6;
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
            this.btnModify.Font = new System.Drawing.Font("細明體", 9F);
            this.btnModify.ForeColor = System.Drawing.SystemColors.Control;
            this.btnModify.Location = new System.Drawing.Point(345, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(69, 79);
            this.btnModify.TabIndex = 5;
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
            this.btnAppend.Font = new System.Drawing.Font("細明體", 9F);
            this.btnAppend.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAppend.Location = new System.Drawing.Point(276, 0);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(69, 79);
            this.btnAppend.TabIndex = 4;
            this.btnAppend.UseDefaultSettings = false;
            this.btnAppend.UseVisualStyleBackColor = false;
            this.btnAppend.EnabledChanged += new System.EventHandler(this.btnAppend_EnabledChanged);
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // btnBottom
            // 
            this.btnBottom.BackColor = System.Drawing.SystemColors.Control;
            this.btnBottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBottom.BackgroundImage")));
            this.btnBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBottom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBottom.Font = new System.Drawing.Font("細明體", 9F);
            this.btnBottom.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBottom.Location = new System.Drawing.Point(207, 0);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(69, 79);
            this.btnBottom.TabIndex = 3;
            this.btnBottom.UseDefaultSettings = false;
            this.btnBottom.UseVisualStyleBackColor = false;
            this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Font = new System.Drawing.Font("細明體", 9F);
            this.btnNext.ForeColor = System.Drawing.SystemColors.Control;
            this.btnNext.Location = new System.Drawing.Point(138, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(69, 79);
            this.btnNext.TabIndex = 2;
            this.btnNext.UseDefaultSettings = false;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrior
            // 
            this.btnPrior.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrior.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrior.BackgroundImage")));
            this.btnPrior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrior.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrior.Font = new System.Drawing.Font("細明體", 9F);
            this.btnPrior.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPrior.Location = new System.Drawing.Point(69, 0);
            this.btnPrior.Name = "btnPrior";
            this.btnPrior.Size = new System.Drawing.Size(69, 79);
            this.btnPrior.TabIndex = 1;
            this.btnPrior.UseDefaultSettings = false;
            this.btnPrior.UseVisualStyleBackColor = false;
            this.btnPrior.Click += new System.EventHandler(this.btnPrior_Click);
            // 
            // btnTop
            // 
            this.btnTop.BackColor = System.Drawing.SystemColors.Control;
            this.btnTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTop.BackgroundImage")));
            this.btnTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTop.Font = new System.Drawing.Font("細明體", 9F);
            this.btnTop.ForeColor = System.Drawing.SystemColors.Control;
            this.btnTop.Location = new System.Drawing.Point(0, 0);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(69, 79);
            this.btnTop.TabIndex = 0;
            this.btnTop.UseDefaultSettings = false;
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.panelNT1);
            this.pnlBoxT1.Controls.Add(this.StName);
            this.pnlBoxT1.Controls.Add(this.lblStNo);
            this.pnlBoxT1.Controls.Add(this.lblStTrait);
            this.pnlBoxT1.Controls.Add(this.lblStName);
            this.pnlBoxT1.Controls.Add(this.StNo);
            this.pnlBoxT1.Location = new System.Drawing.Point(170, 162);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(670, 221);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Controls.Add(this.StTrait1);
            this.panelNT1.Controls.Add(this.StTrait2);
            this.panelNT1.Controls.Add(this.StTrait4);
            this.panelNT1.Controls.Add(this.StTrait3);
            this.panelNT1.Enabled = false;
            this.panelNT1.Location = new System.Drawing.Point(169, 142);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(410, 27);
            this.panelNT1.TabIndex = 3;
            // 
            // StTrait1
            // 
            this.StTrait1.AutoSize = true;
            this.StTrait1.BackColor = System.Drawing.Color.LightBlue;
            this.StTrait1.Checked = true;
            this.StTrait1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StTrait1.Font = new System.Drawing.Font("細明體", 12F);
            this.StTrait1.Location = new System.Drawing.Point(32, 2);
            this.StTrait1.Name = "StTrait1";
            this.StTrait1.Size = new System.Drawing.Size(74, 20);
            this.StTrait1.TabIndex = 1;
            this.StTrait1.Tag = "庫存倉";
            this.StTrait1.Text = "庫存倉";
            this.StTrait1.UseVisualStyleBackColor = false;
            // 
            // StTrait2
            // 
            this.StTrait2.AutoSize = true;
            this.StTrait2.BackColor = System.Drawing.Color.Transparent;
            this.StTrait2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StTrait2.Font = new System.Drawing.Font("細明體", 12F);
            this.StTrait2.Location = new System.Drawing.Point(122, 2);
            this.StTrait2.Name = "StTrait2";
            this.StTrait2.Size = new System.Drawing.Size(74, 20);
            this.StTrait2.TabIndex = 2;
            this.StTrait2.Tag = "借出倉";
            this.StTrait2.Text = "借出倉";
            this.StTrait2.UseVisualStyleBackColor = true;
            // 
            // StTrait4
            // 
            this.StTrait4.AutoSize = true;
            this.StTrait4.BackColor = System.Drawing.Color.Transparent;
            this.StTrait4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StTrait4.Font = new System.Drawing.Font("細明體", 12F);
            this.StTrait4.Location = new System.Drawing.Point(302, 2);
            this.StTrait4.Name = "StTrait4";
            this.StTrait4.Size = new System.Drawing.Size(74, 20);
            this.StTrait4.TabIndex = 4;
            this.StTrait4.Tag = "借入倉";
            this.StTrait4.Text = "借入倉";
            this.StTrait4.UseVisualStyleBackColor = true;
            // 
            // StTrait3
            // 
            this.StTrait3.AutoSize = true;
            this.StTrait3.BackColor = System.Drawing.Color.Transparent;
            this.StTrait3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StTrait3.Font = new System.Drawing.Font("細明體", 12F);
            this.StTrait3.Location = new System.Drawing.Point(212, 2);
            this.StTrait3.Name = "StTrait3";
            this.StTrait3.Size = new System.Drawing.Size(74, 20);
            this.StTrait3.TabIndex = 3;
            this.StTrait3.Tag = "加工倉";
            this.StTrait3.Text = "加工倉";
            this.StTrait3.UseVisualStyleBackColor = true;
            // 
            // StName
            // 
            this.StName.AllowGrayBackColor = false;
            this.StName.AllowResize = true;
            this.StName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.StName.Font = new System.Drawing.Font("細明體", 12F);
            this.StName.Location = new System.Drawing.Point(169, 99);
            this.StName.MaxLength = 10;
            this.StName.Name = "StName";
            this.StName.oLen = 0;
            this.StName.ReadOnly = true;
            this.StName.Size = new System.Drawing.Size(87, 27);
            this.StName.TabIndex = 2;
            this.StName.TabStop = false;
            // 
            // lblStNo
            // 
            this.lblStNo.AutoSize = true;
            this.lblStNo.BackColor = System.Drawing.Color.Transparent;
            this.lblStNo.Font = new System.Drawing.Font("細明體", 12F);
            this.lblStNo.Location = new System.Drawing.Point(91, 57);
            this.lblStNo.Name = "lblStNo";
            this.lblStNo.Size = new System.Drawing.Size(72, 16);
            this.lblStNo.TabIndex = 0;
            this.lblStNo.Text = "倉庫編號";
            // 
            // lblStTrait
            // 
            this.lblStTrait.AutoSize = true;
            this.lblStTrait.BackColor = System.Drawing.Color.Transparent;
            this.lblStTrait.Font = new System.Drawing.Font("細明體", 12F);
            this.lblStTrait.Location = new System.Drawing.Point(91, 147);
            this.lblStTrait.Name = "lblStTrait";
            this.lblStTrait.Size = new System.Drawing.Size(72, 16);
            this.lblStTrait.TabIndex = 0;
            this.lblStTrait.Text = "倉庫類別";
            // 
            // lblStName
            // 
            this.lblStName.AutoSize = true;
            this.lblStName.BackColor = System.Drawing.Color.Transparent;
            this.lblStName.Font = new System.Drawing.Font("細明體", 12F);
            this.lblStName.Location = new System.Drawing.Point(91, 104);
            this.lblStName.Name = "lblStName";
            this.lblStName.Size = new System.Drawing.Size(72, 16);
            this.lblStName.TabIndex = 0;
            this.lblStName.Text = "倉庫名稱";
            // 
            // StNo
            // 
            this.StNo.AllowGrayBackColor = false;
            this.StNo.AllowResize = true;
            this.StNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.StNo.Font = new System.Drawing.Font("細明體", 12F);
            this.StNo.Location = new System.Drawing.Point(169, 52);
            this.StNo.MaxLength = 10;
            this.StNo.Name = "StNo";
            this.StNo.oLen = 0;
            this.StNo.ReadOnly = true;
            this.StNo.Size = new System.Drawing.Size(87, 27);
            this.StNo.TabIndex = 1;
            this.StNo.TabStop = false;
            this.StNo.Enter += new System.EventHandler(this.StNo_Enter);
            this.StNo.Validating += new System.ComponentModel.CancelEventHandler(this.StNo_Validating);
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT          *\r\nFROM              stkroom";
            this.sqlSelectCommand1.Connection = this.cn;
            // 
            // cn
            // 
            this.cn.ConnectionString = "Data Source=.;Initial Catalog=web;Integrated Security=True";
            this.cn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [stkroom] ([stno], [stname], [sttrait]) VALUES (@stno, @stname, @sttr" +
    "ait);\r\nSELECT stno, stname, sttrait FROM stkroom WHERE (stno = @stno)";
            this.sqlInsertCommand1.Connection = this.cn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@stno", System.Data.SqlDbType.NVarChar, 0, "stno"),
            new System.Data.SqlClient.SqlParameter("@stname", System.Data.SqlDbType.NVarChar, 0, "stname"),
            new System.Data.SqlClient.SqlParameter("@sttrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Current, null)});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.cn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@stno", System.Data.SqlDbType.NVarChar, 0, "stno"),
            new System.Data.SqlClient.SqlParameter("@stname", System.Data.SqlDbType.NVarChar, 0, "stname"),
            new System.Data.SqlClient.SqlParameter("@sttrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Original_stno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sttrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sttrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.cn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_stno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sttrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sttrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Original, null)});
            // 
            // da1
            // 
            this.da1.DeleteCommand = this.sqlDeleteCommand1;
            this.da1.InsertCommand = this.sqlInsertCommand1;
            this.da1.SelectCommand = this.sqlSelectCommand1;
            this.da1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "stkroom", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("stno", "stno"),
                        new System.Data.Common.DataColumnMapping("stname", "stname"),
                        new System.Data.Common.DataColumnMapping("sttrait", "sttrait")})});
            this.da1.UpdateCommand = this.sqlUpdateCommand1;
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
            // FrmStkRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "FrmStkRoom";
            this.Tag = "倉庫建檔作業";
            this.Text = "倉庫建檔作業";
            this.Load += new System.EventHandler(this.FrmStkRoom_Load);
            this.panelT1.ResumeLayout(false);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelNT1.ResumeLayout(false);
            this.panelNT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnCancel;
        private JE.MyControl.ButtonT btnSave;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.ButtonT btnDelete;
        private JE.MyControl.ButtonT btnModify;
        private JE.MyControl.ButtonT btnAppend;
        private JE.MyControl.ButtonT btnBottom;
        private JE.MyControl.ButtonT btnNext;
        private JE.MyControl.ButtonT btnPrior;
        private JE.MyControl.ButtonT btnTop;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblStNo;
        private JE.MyControl.LabelT lblStName;
        private JE.MyControl.LabelT lblStTrait;
        private JE.MyControl.RadioT StTrait1;
        private JE.MyControl.RadioT StTrait2;
        private JE.MyControl.RadioT StTrait3;
        private JE.MyControl.RadioT StTrait4;
        public JE.MyControl.TextBoxT StNo;
        public JE.MyControl.TextBoxT StName;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection cn;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter da1;
        private JE.MyControl.PanelNT panelNT1;
        private JE.MyControl.StatusStripT statusStripT1; 
    }
}