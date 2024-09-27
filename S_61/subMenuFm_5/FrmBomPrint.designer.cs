namespace S_61.SOther
{
    partial class FrmBomPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBomPrint));
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.BoItNo = new JE.MyControl.TextBoxT();
            this.BoItNo_1 = new JE.MyControl.TextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnexcel = new JE.MyControl.ButtonT();
            this.btnword = new JE.MyControl.ButtonT();
            this.btnpreview = new JE.MyControl.ButtonT();
            this.btnprint = new JE.MyControl.ButtonT();
            this.groupBox1 = new JE.MyControl.GroupBoxT();
            this.radio6 = new JE.MyControl.RadioT();
            this.radio7 = new JE.MyControl.RadioT();
            this.radio9 = new JE.MyControl.RadioT();
            this.radio8 = new JE.MyControl.RadioT();
            this.radio10 = new JE.MyControl.RadioT();
            this.radio16 = new JE.MyControl.RadioT();
            this.group1 = new JE.MyControl.GroupBoxT();
            this.radio1 = new JE.MyControl.RadioT();
            this.radio2 = new JE.MyControl.RadioT();
            this.radio4 = new JE.MyControl.RadioT();
            this.radio3 = new JE.MyControl.RadioT();
            this.radio5 = new JE.MyControl.RadioT();
            this.radio15 = new JE.MyControl.RadioT();
            this.groupBox2 = new JE.MyControl.GroupBoxT();
            this.radio11 = new JE.MyControl.RadioT();
            this.radio12 = new JE.MyControl.RadioT();
            this.radio13 = new JE.MyControl.RadioT();
            this.radio14 = new JE.MyControl.RadioT();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.da = new System.Data.SqlClient.SqlDataAdapter();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.group1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.BoItNo);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.BoItNo_1);
            this.pnlBoxT1.Location = new System.Drawing.Point(165, 315);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(680, 156);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(235, 42);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起始組件單號";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(235, 97);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "結止組件單號";
            // 
            // BoItNo
            // 
            this.BoItNo.AllowGrayBackColor = false;
            this.BoItNo.AllowResize = true;
            this.BoItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BoItNo.Location = new System.Drawing.Point(345, 37);
            this.BoItNo.MaxLength = 20;
            this.BoItNo.Name = "BoItNo";
            this.BoItNo.oLen = 0;
            this.BoItNo.Size = new System.Drawing.Size(167, 27);
            this.BoItNo.TabIndex = 1;
            this.BoItNo.DoubleClick += new System.EventHandler(this.BoItNo_DoubleClick);
            this.BoItNo.Validating += new System.ComponentModel.CancelEventHandler(this.BoItNo_Validating);
            // 
            // BoItNo_1
            // 
            this.BoItNo_1.AllowGrayBackColor = false;
            this.BoItNo_1.AllowResize = true;
            this.BoItNo_1.Font = new System.Drawing.Font("細明體", 12F);
            this.BoItNo_1.Location = new System.Drawing.Point(345, 92);
            this.BoItNo_1.MaxLength = 20;
            this.BoItNo_1.Name = "BoItNo_1";
            this.BoItNo_1.oLen = 0;
            this.BoItNo_1.Size = new System.Drawing.Size(167, 27);
            this.BoItNo_1.TabIndex = 2;
            this.BoItNo_1.DoubleClick += new System.EventHandler(this.BoItNo_DoubleClick);
            this.BoItNo_1.Validating += new System.ComponentModel.CancelEventHandler(this.BoItNo_Validating);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(55, 70);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(88, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "請輸入列印";
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnexcel);
            this.panelT1.Controls.Add(this.btnword);
            this.panelT1.Controls.Add(this.btnpreview);
            this.panelT1.Controls.Add(this.btnprint);
            this.panelT1.Location = new System.Drawing.Point(328, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(355, 79);
            this.panelT1.TabIndex = 2;
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
            this.btnExit.TabIndex = 4;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnexcel
            // 
            this.btnexcel.BackColor = System.Drawing.SystemColors.Control;
            this.btnexcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnexcel.BackgroundImage")));
            this.btnexcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnexcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnexcel.Font = new System.Drawing.Font("細明體", 9F);
            this.btnexcel.Location = new System.Drawing.Point(207, 0);
            this.btnexcel.Name = "btnexcel";
            this.btnexcel.Size = new System.Drawing.Size(69, 79);
            this.btnexcel.TabIndex = 3;
            this.btnexcel.UseDefaultSettings = false;
            this.btnexcel.UseVisualStyleBackColor = false;
            this.btnexcel.Click += new System.EventHandler(this.btnexcel_Click);
            // 
            // btnword
            // 
            this.btnword.BackColor = System.Drawing.SystemColors.Control;
            this.btnword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnword.BackgroundImage")));
            this.btnword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnword.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnword.Font = new System.Drawing.Font("細明體", 9F);
            this.btnword.Location = new System.Drawing.Point(138, 0);
            this.btnword.Name = "btnword";
            this.btnword.Size = new System.Drawing.Size(69, 79);
            this.btnword.TabIndex = 2;
            this.btnword.UseDefaultSettings = false;
            this.btnword.UseVisualStyleBackColor = false;
            this.btnword.Click += new System.EventHandler(this.btnword_Click);
            // 
            // btnpreview
            // 
            this.btnpreview.BackColor = System.Drawing.SystemColors.Control;
            this.btnpreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnpreview.BackgroundImage")));
            this.btnpreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnpreview.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnpreview.Font = new System.Drawing.Font("細明體", 9F);
            this.btnpreview.Location = new System.Drawing.Point(69, 0);
            this.btnpreview.Name = "btnpreview";
            this.btnpreview.Size = new System.Drawing.Size(69, 79);
            this.btnpreview.TabIndex = 1;
            this.btnpreview.UseDefaultSettings = false;
            this.btnpreview.UseVisualStyleBackColor = false;
            this.btnpreview.Click += new System.EventHandler(this.btnpreview_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.Control;
            this.btnprint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnprint.BackgroundImage")));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnprint.Font = new System.Drawing.Font("細明體", 9F);
            this.btnprint.Location = new System.Drawing.Point(0, 0);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(69, 79);
            this.btnprint.TabIndex = 0;
            this.btnprint.UseDefaultSettings = false;
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio6);
            this.groupBox1.Controls.Add(this.radio10);
            this.groupBox1.Controls.Add(this.radio7);
            this.groupBox1.Controls.Add(this.radio16);
            this.groupBox1.Controls.Add(this.radio9);
            this.groupBox1.Controls.Add(this.radio8);
            this.groupBox1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBox1.Location = new System.Drawing.Point(165, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 75);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "公司抬頭";
            // 
            // radio6
            // 
            this.radio6.AutoSize = true;
            this.radio6.BackColor = System.Drawing.Color.LightBlue;
            this.radio6.Checked = true;
            this.radio6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio6.Font = new System.Drawing.Font("細明體", 12F);
            this.radio6.Location = new System.Drawing.Point(63, 34);
            this.radio6.Name = "radio6";
            this.radio6.Size = new System.Drawing.Size(74, 20);
            this.radio6.TabIndex = 0;
            this.radio6.Tag = "";
            this.radio6.Text = "第一組";
            this.radio6.UseVisualStyleBackColor = true; 
            // 
            // radio7
            // 
            this.radio7.AutoSize = true;
            this.radio7.BackColor = System.Drawing.Color.Transparent;
            this.radio7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio7.Font = new System.Drawing.Font("細明體", 12F);
            this.radio7.Location = new System.Drawing.Point(159, 34);
            this.radio7.Name = "radio7";
            this.radio7.Size = new System.Drawing.Size(74, 20);
            this.radio7.TabIndex = 1;
            this.radio7.Tag = "";
            this.radio7.Text = "第二組";
            this.radio7.UseVisualStyleBackColor = true; 
            // 
            // radio9
            // 
            this.radio9.AutoSize = true;
            this.radio9.BackColor = System.Drawing.Color.Transparent;
            this.radio9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio9.Font = new System.Drawing.Font("細明體", 12F);
            this.radio9.Location = new System.Drawing.Point(351, 34);
            this.radio9.Name = "radio9";
            this.radio9.Size = new System.Drawing.Size(74, 20);
            this.radio9.TabIndex = 3;
            this.radio9.Tag = "";
            this.radio9.Text = "第四組";
            this.radio9.UseVisualStyleBackColor = true; 
            // 
            // radio8
            // 
            this.radio8.AutoSize = true;
            this.radio8.BackColor = System.Drawing.Color.Transparent;
            this.radio8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio8.Font = new System.Drawing.Font("細明體", 12F);
            this.radio8.Location = new System.Drawing.Point(255, 34);
            this.radio8.Name = "radio8";
            this.radio8.Size = new System.Drawing.Size(74, 20);
            this.radio8.TabIndex = 2;
            this.radio8.Tag = "";
            this.radio8.Text = "第三組";
            this.radio8.UseVisualStyleBackColor = true; 
            // 
            // radio10
            // 
            this.radio10.AutoSize = true;
            this.radio10.BackColor = System.Drawing.Color.Transparent;
            this.radio10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio10.Font = new System.Drawing.Font("細明體", 12F);
            this.radio10.Location = new System.Drawing.Point(447, 34);
            this.radio10.Name = "radio10";
            this.radio10.Size = new System.Drawing.Size(74, 20);
            this.radio10.TabIndex = 4;
            this.radio10.Tag = "";
            this.radio10.Text = "第五組";
            this.radio10.UseVisualStyleBackColor = true; 
            // 
            // radio16
            // 
            this.radio16.AutoSize = true;
            this.radio16.BackColor = System.Drawing.Color.Transparent;
            this.radio16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio16.Font = new System.Drawing.Font("細明體", 12F);
            this.radio16.Location = new System.Drawing.Point(543, 34);
            this.radio16.Name = "radio16";
            this.radio16.Size = new System.Drawing.Size(74, 20);
            this.radio16.TabIndex = 4;
            this.radio16.Tag = "";
            this.radio16.Text = "不列印";
            this.radio16.UseVisualStyleBackColor = true; 
            // 
            // group1
            // 
            this.group1.Controls.Add(this.radio1);
            this.group1.Controls.Add(this.radio5);
            this.group1.Controls.Add(this.radio2);
            this.group1.Controls.Add(this.radio15);
            this.group1.Controls.Add(this.radio4);
            this.group1.Controls.Add(this.radio3);
            this.group1.Font = new System.Drawing.Font("細明體", 12F);
            this.group1.Location = new System.Drawing.Point(165, 147);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(680, 75);
            this.group1.TabIndex = 4;
            this.group1.TabStop = false;
            this.group1.Text = "三行註腳";
            // 
            // radio1
            // 
            this.radio1.AutoSize = true;
            this.radio1.BackColor = System.Drawing.Color.LightBlue;
            this.radio1.Checked = true;
            this.radio1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio1.Font = new System.Drawing.Font("細明體", 12F);
            this.radio1.Location = new System.Drawing.Point(63, 34);
            this.radio1.Name = "radio1";
            this.radio1.Size = new System.Drawing.Size(74, 20);
            this.radio1.TabIndex = 0;
            this.radio1.Tag = "";
            this.radio1.Text = "第一組";
            this.radio1.UseVisualStyleBackColor = true; 
            // 
            // radio2
            // 
            this.radio2.AutoSize = true;
            this.radio2.BackColor = System.Drawing.Color.Transparent;
            this.radio2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio2.Font = new System.Drawing.Font("細明體", 12F);
            this.radio2.Location = new System.Drawing.Point(159, 34);
            this.radio2.Name = "radio2";
            this.radio2.Size = new System.Drawing.Size(74, 20);
            this.radio2.TabIndex = 1;
            this.radio2.Tag = "";
            this.radio2.Text = "第二組";
            this.radio2.UseVisualStyleBackColor = true; 
            // 
            // radio4
            // 
            this.radio4.AutoSize = true;
            this.radio4.BackColor = System.Drawing.Color.Transparent;
            this.radio4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio4.Font = new System.Drawing.Font("細明體", 12F);
            this.radio4.Location = new System.Drawing.Point(351, 34);
            this.radio4.Name = "radio4";
            this.radio4.Size = new System.Drawing.Size(74, 20);
            this.radio4.TabIndex = 3;
            this.radio4.Tag = "";
            this.radio4.Text = "第四組";
            this.radio4.UseVisualStyleBackColor = true; 
            // 
            // radio3
            // 
            this.radio3.AutoSize = true;
            this.radio3.BackColor = System.Drawing.Color.Transparent;
            this.radio3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio3.Font = new System.Drawing.Font("細明體", 12F);
            this.radio3.Location = new System.Drawing.Point(255, 34);
            this.radio3.Name = "radio3";
            this.radio3.Size = new System.Drawing.Size(74, 20);
            this.radio3.TabIndex = 2;
            this.radio3.Tag = "";
            this.radio3.Text = "第三組";
            this.radio3.UseVisualStyleBackColor = true; 
            // 
            // radio5
            // 
            this.radio5.AutoSize = true;
            this.radio5.BackColor = System.Drawing.Color.Transparent;
            this.radio5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio5.Font = new System.Drawing.Font("細明體", 12F);
            this.radio5.Location = new System.Drawing.Point(447, 34);
            this.radio5.Name = "radio5";
            this.radio5.Size = new System.Drawing.Size(74, 20);
            this.radio5.TabIndex = 4;
            this.radio5.Tag = "";
            this.radio5.Text = "第五組";
            this.radio5.UseVisualStyleBackColor = true; 
            // 
            // radio15
            // 
            this.radio15.AutoSize = true;
            this.radio15.BackColor = System.Drawing.Color.Transparent;
            this.radio15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio15.Font = new System.Drawing.Font("細明體", 12F);
            this.radio15.Location = new System.Drawing.Point(543, 34);
            this.radio15.Name = "radio15";
            this.radio15.Size = new System.Drawing.Size(74, 20);
            this.radio15.TabIndex = 4;
            this.radio15.Tag = "";
            this.radio15.Text = "不列印";
            this.radio15.UseVisualStyleBackColor = true; 
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radio11);
            this.groupBox2.Controls.Add(this.radio13);
            this.groupBox2.Controls.Add(this.radio12);
            this.groupBox2.Controls.Add(this.radio14);
            this.groupBox2.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBox2.Location = new System.Drawing.Point(165, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(680, 75);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "報表內容";
            // 
            // radio11
            // 
            this.radio11.AutoSize = true;
            this.radio11.BackColor = System.Drawing.Color.LightBlue;
            this.radio11.Checked = true;
            this.radio11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio11.Font = new System.Drawing.Font("細明體", 12F);
            this.radio11.Location = new System.Drawing.Point(135, 34);
            this.radio11.Name = "radio11";
            this.radio11.Size = new System.Drawing.Size(74, 20);
            this.radio11.TabIndex = 0;
            this.radio11.Tag = "";
            this.radio11.Text = "標準表";
            this.radio11.UseVisualStyleBackColor = true; 
            // 
            // radio12
            // 
            this.radio12.AutoSize = true;
            this.radio12.BackColor = System.Drawing.Color.Transparent;
            this.radio12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio12.Font = new System.Drawing.Font("細明體", 12F);
            this.radio12.Location = new System.Drawing.Point(247, 34);
            this.radio12.Name = "radio12";
            this.radio12.Size = new System.Drawing.Size(74, 20);
            this.radio12.TabIndex = 1;
            this.radio12.Tag = "";
            this.radio12.Text = "成本表";
            this.radio12.UseVisualStyleBackColor = true; 
            // 
            // radio13
            // 
            this.radio13.AutoSize = true;
            this.radio13.BackColor = System.Drawing.Color.Transparent;
            this.radio13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio13.Font = new System.Drawing.Font("細明體", 12F);
            this.radio13.Location = new System.Drawing.Point(359, 34);
            this.radio13.Name = "radio13";
            this.radio13.Size = new System.Drawing.Size(74, 20);
            this.radio13.TabIndex = 2;
            this.radio13.Tag = "";
            this.radio13.Text = "自訂一";
            this.radio13.UseVisualStyleBackColor = true; 
            // 
            // radio14
            // 
            this.radio14.AutoSize = true;
            this.radio14.BackColor = System.Drawing.Color.Transparent;
            this.radio14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio14.Font = new System.Drawing.Font("細明體", 12F);
            this.radio14.Location = new System.Drawing.Point(471, 34);
            this.radio14.Name = "radio14";
            this.radio14.Size = new System.Drawing.Size(74, 20);
            this.radio14.TabIndex = 2;
            this.radio14.Tag = "";
            this.radio14.Text = "自訂二";
            this.radio14.UseVisualStyleBackColor = true;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT         *\r\nFROM              bom\r\nWHERE          (boitno = @no)";
            this.sqlSelectCommand1.Connection = this.cn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@no", System.Data.SqlDbType.NVarChar, 20, "boitno")});
            // 
            // cn
            // 
            this.cn.ConnectionString = "Data Source=.;Initial Catalog=web;Integrated Security=True";
            this.cn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.cn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@boitno", System.Data.SqlDbType.NVarChar, 0, "boitno"),
            new System.Data.SqlClient.SqlParameter("@boitname", System.Data.SqlDbType.NVarChar, 0, "boitname"),
            new System.Data.SqlClient.SqlParameter("@bototmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "bototmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@bototqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "bototqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@bomemo", System.Data.SqlDbType.NVarChar, 0, "bomemo"),
            new System.Data.SqlClient.SqlParameter("@borec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(0)), "borec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@boexecflag", System.Data.SqlDbType.Bit, 0, "boexecflag"),
            new System.Data.SqlClient.SqlParameter("@BoTotBuyMny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "BoTotBuyMny", System.Data.DataRowVersion.Current, null)});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.cn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@boitno", System.Data.SqlDbType.NVarChar, 0, "boitno"),
            new System.Data.SqlClient.SqlParameter("@boitname", System.Data.SqlDbType.NVarChar, 0, "boitname"),
            new System.Data.SqlClient.SqlParameter("@bototmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "bototmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@bototqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "bototqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@bomemo", System.Data.SqlDbType.NVarChar, 0, "bomemo"),
            new System.Data.SqlClient.SqlParameter("@borec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(0)), "borec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@boexecflag", System.Data.SqlDbType.Bit, 0, "boexecflag"),
            new System.Data.SqlClient.SqlParameter("@BoTotBuyMny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "BoTotBuyMny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Original_boitno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "boitno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_boitname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "boitname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_boitname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "boitname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bototmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bototmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bototmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "bototmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bototqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bototqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bototqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "bototqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bomemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bomemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bomemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bomemo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_borec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "borec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_borec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(0)), "borec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_boexecflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "boexecflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_boexecflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "boexecflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BoTotBuyMny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BoTotBuyMny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BoTotBuyMny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "BoTotBuyMny", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.cn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_boitno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "boitno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_boitname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "boitname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_boitname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "boitname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bototmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bototmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bototmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "bototmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bototqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bototqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bototqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "bototqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bomemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bomemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bomemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bomemo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_borec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "borec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_borec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(0)), "borec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_boexecflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "boexecflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_boexecflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "boexecflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BoTotBuyMny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BoTotBuyMny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BoTotBuyMny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "BoTotBuyMny", System.Data.DataRowVersion.Original, null)});
            // 
            // da
            // 
            this.da.DeleteCommand = this.sqlDeleteCommand1;
            this.da.InsertCommand = this.sqlInsertCommand1;
            this.da.SelectCommand = this.sqlSelectCommand1;
            this.da.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "bom", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("boitno", "boitno"),
                        new System.Data.Common.DataColumnMapping("boitname", "boitname"),
                        new System.Data.Common.DataColumnMapping("bototmny", "bototmny"),
                        new System.Data.Common.DataColumnMapping("bototqty", "bototqty"),
                        new System.Data.Common.DataColumnMapping("bomemo", "bomemo"),
                        new System.Data.Common.DataColumnMapping("borec", "borec"),
                        new System.Data.Common.DataColumnMapping("boexecflag", "boexecflag"),
                        new System.Data.Common.DataColumnMapping("BoTotBuyMny", "BoTotBuyMny")})});
            this.da.UpdateCommand = this.sqlUpdateCommand1;
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
            // FrmBomPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.group1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmBomPrint";
            this.Text = "組合組裝品建檔列印";
            this.Load += new System.EventHandler(this.FrmBomPrint_Load);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.GroupBoxT groupBox1;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.ButtonT btnprint;
        private JE.MyControl.ButtonT btnpreview;
        private JE.MyControl.ButtonT btnword;
        private JE.MyControl.ButtonT btnexcel;
        private JE.MyControl.ButtonT btnExit;
        public JE.MyControl.TextBoxT BoItNo;
        public JE.MyControl.TextBoxT BoItNo_1; 
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.GroupBoxT group1;
        public JE.MyControl.RadioT radio1;
        public JE.MyControl.RadioT radio2;
        public JE.MyControl.RadioT radio4;
        public JE.MyControl.RadioT radio3;
        public JE.MyControl.RadioT radio5;
        public JE.MyControl.RadioT radio6;
        public JE.MyControl.RadioT radio7;
        public JE.MyControl.RadioT radio9;
        public JE.MyControl.RadioT radio8;
        public JE.MyControl.RadioT radio10;
        private JE.MyControl.GroupBoxT groupBox2;
        public JE.MyControl.RadioT radio11;
        public JE.MyControl.RadioT radio12;
        public JE.MyControl.RadioT radio13;
        public JE.MyControl.RadioT radio14;
        public JE.MyControl.RadioT radio15;
        public JE.MyControl.RadioT radio16;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection cn;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter da;
        private JE.MyControl.StatusStripT statusStripT1; 


    }
}