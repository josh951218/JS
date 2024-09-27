namespace S_61.S6
{
    partial class FrmInvoSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInvoSet));
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn1 = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.da1 = new System.Data.SqlClient.SqlDataAdapter();
            this.tableLayoutPnl1 = new JE.MyControl.TableLayoutPanelbase();
            this.TextBox1 = new JE.MyControl.RichTextBoxT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.tableLayoutPnl3 = new JE.MyControl.TableLayoutPanelbase();
            this.lblMsg = new JE.MyControl.LabelT();
            this.panel1 = new JE.MyControl.PanelNT();
            this.lblMsg1 = new JE.MyControl.LabelT();
            this.panel2 = new JE.MyControl.PanelNT();
            this.lblMsg2 = new JE.MyControl.LabelT();
            this.TextBox4 = new JE.MyControl.RichTextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.Detail2 = new JE.MyControl.RichTextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.Detail1 = new JE.MyControl.RichTextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.tableLayoutPnl4 = new JE.MyControl.TableLayoutPanelbase();
            this.tableLayoutPnl7 = new JE.MyControl.TableLayoutPanelbase();
            this.radio10 = new JE.MyControl.RadioT();
            this.radio11 = new JE.MyControl.RadioT();
            this.radio12 = new JE.MyControl.RadioT();
            this.lbl10 = new JE.MyControl.LabelT();
            this.lbl11 = new JE.MyControl.LabelT();
            this.lbl12 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.lblT8 = new JE.MyControl.LabelT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.lblT10 = new JE.MyControl.LabelT();
            this.lblT11 = new JE.MyControl.LabelT();
            this.t1 = new JE.MyControl.TextBoxT();
            this.t2 = new JE.MyControl.TextBoxT();
            this.t4 = new JE.MyControl.TextBoxT();
            this.t3 = new JE.MyControl.TextBoxT();
            this.tableLayoutPnl5 = new JE.MyControl.TableLayoutPanelbase();
            this.radio1 = new JE.MyControl.RadioT();
            this.radio2 = new JE.MyControl.RadioT();
            this.lbl1 = new JE.MyControl.LabelT();
            this.lbl2 = new JE.MyControl.LabelT();
            this.tableLayoutPnl6 = new JE.MyControl.TableLayoutPanelbase();
            this.radio6 = new JE.MyControl.RadioT();
            this.radio7 = new JE.MyControl.RadioT();
            this.radio8 = new JE.MyControl.RadioT();
            this.lbl6 = new JE.MyControl.LabelT();
            this.lbl7 = new JE.MyControl.LabelT();
            this.lbl8 = new JE.MyControl.LabelT();
            this.lbl9 = new JE.MyControl.LabelT();
            this.radio9 = new JE.MyControl.RadioT();
            this.lblT20 = new JE.MyControl.LabelT();
            this.Recover1 = new System.Windows.Forms.Button();
            this.Recover2 = new System.Windows.Forms.Button();
            this.tableLayoutPnl2 = new JE.MyControl.TableLayoutPanelbase();
            this.radio3 = new JE.MyControl.RadioT();
            this.radio4 = new JE.MyControl.RadioT();
            this.radio5 = new JE.MyControl.RadioT();
            this.lbl3 = new JE.MyControl.LabelT();
            this.lbl4 = new JE.MyControl.LabelT();
            this.lbl5 = new JE.MyControl.LabelT();
            this.lblT24 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.btntPrnt = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPnl1.SuspendLayout();
            this.tableLayoutPnl3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            this.tableLayoutPnl4.SuspendLayout();
            this.tableLayoutPnl7.SuspendLayout();
            this.tableLayoutPnl5.SuspendLayout();
            this.tableLayoutPnl6.SuspendLayout();
            this.tableLayoutPnl2.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.statusStripT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "select * from invoiceset";
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
            new System.Data.SqlClient.SqlParameter("@inno", System.Data.SqlDbType.NVarChar, 0, "inno"),
            new System.Data.SqlClient.SqlParameter("@inheadnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "inheadnum", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@inditalnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "inditalnum", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@infootnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "infootnum", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@intotnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "intotnum", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@insignet", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "insignet", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@inhead", System.Data.SqlDbType.Text, 0, "inhead"),
            new System.Data.SqlClient.SqlParameter("@indital1", System.Data.SqlDbType.Text, 0, "indital1"),
            new System.Data.SqlClient.SqlParameter("@indital21", System.Data.SqlDbType.Text, 0, "indital21"),
            new System.Data.SqlClient.SqlParameter("@indital22", System.Data.SqlDbType.Text, 0, "indital22"),
            new System.Data.SqlClient.SqlParameter("@infoot", System.Data.SqlDbType.Text, 0, "infoot"),
            new System.Data.SqlClient.SqlParameter("@Jump", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "Jump", System.Data.DataRowVersion.Current, null)});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.cn1;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@inno", System.Data.SqlDbType.NVarChar, 0, "inno"),
            new System.Data.SqlClient.SqlParameter("@inheadnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "inheadnum", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@inditalnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "inditalnum", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@infootnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "infootnum", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@intotnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "intotnum", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@insignet", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "insignet", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@inhead", System.Data.SqlDbType.Text, 0, "inhead"),
            new System.Data.SqlClient.SqlParameter("@indital1", System.Data.SqlDbType.Text, 0, "indital1"),
            new System.Data.SqlClient.SqlParameter("@indital21", System.Data.SqlDbType.Text, 0, "indital21"),
            new System.Data.SqlClient.SqlParameter("@indital22", System.Data.SqlDbType.Text, 0, "indital22"),
            new System.Data.SqlClient.SqlParameter("@infoot", System.Data.SqlDbType.Text, 0, "infoot"),
            new System.Data.SqlClient.SqlParameter("@Jump", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "Jump", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Original_inno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inheadnum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inheadnum", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inheadnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "inheadnum", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inditalnum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inditalnum", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inditalnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "inditalnum", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_infootnum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "infootnum", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_infootnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "infootnum", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_intotnum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "intotnum", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_intotnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "intotnum", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_insignet", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "insignet", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_insignet", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "insignet", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Jump", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Jump", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Jump", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "Jump", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.cn1;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_inno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inheadnum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inheadnum", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inheadnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "inheadnum", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inditalnum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inditalnum", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inditalnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "inditalnum", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_infootnum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "infootnum", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_infootnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "infootnum", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_intotnum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "intotnum", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_intotnum", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "intotnum", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_insignet", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "insignet", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_insignet", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "insignet", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Jump", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Jump", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Jump", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(2)), ((byte)(0)), "Jump", System.Data.DataRowVersion.Original, null)});
            // 
            // da1
            // 
            this.da1.DeleteCommand = this.sqlDeleteCommand1;
            this.da1.InsertCommand = this.sqlInsertCommand1;
            this.da1.SelectCommand = this.sqlSelectCommand1;
            this.da1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "invoiceset", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("inno", "inno"),
                        new System.Data.Common.DataColumnMapping("inheadnum", "inheadnum"),
                        new System.Data.Common.DataColumnMapping("inditalnum", "inditalnum"),
                        new System.Data.Common.DataColumnMapping("infootnum", "infootnum"),
                        new System.Data.Common.DataColumnMapping("intotnum", "intotnum"),
                        new System.Data.Common.DataColumnMapping("insignet", "insignet"),
                        new System.Data.Common.DataColumnMapping("inhead", "inhead"),
                        new System.Data.Common.DataColumnMapping("indital1", "indital1"),
                        new System.Data.Common.DataColumnMapping("indital21", "indital21"),
                        new System.Data.Common.DataColumnMapping("indital22", "indital22"),
                        new System.Data.Common.DataColumnMapping("infoot", "infoot"),
                        new System.Data.Common.DataColumnMapping("Jump", "Jump")})});
            this.da1.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // tableLayoutPnl1
            // 
            this.tableLayoutPnl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.tableLayoutPnl1.ColumnCount = 2;
            this.tableLayoutPnl1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPnl1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPnl1.Controls.Add(this.TextBox1, 0, 1);
            this.tableLayoutPnl1.Controls.Add(this.lblT1, 0, 0);
            this.tableLayoutPnl1.Controls.Add(this.tableLayoutPnl3, 1, 1);
            this.tableLayoutPnl1.Controls.Add(this.TextBox4, 0, 7);
            this.tableLayoutPnl1.Controls.Add(this.lblT4, 0, 6);
            this.tableLayoutPnl1.Controls.Add(this.Detail2, 0, 5);
            this.tableLayoutPnl1.Controls.Add(this.lblT3, 0, 4);
            this.tableLayoutPnl1.Controls.Add(this.Detail1, 0, 3);
            this.tableLayoutPnl1.Controls.Add(this.lblT2, 0, 2);
            this.tableLayoutPnl1.Controls.Add(this.pnlBoxT1, 1, 2);
            this.tableLayoutPnl1.Controls.Add(this.panelT1, 1, 8);
            this.tableLayoutPnl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPnl1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPnl1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPnl1.Name = "tableLayoutPnl1";
            this.tableLayoutPnl1.RowCount = 9;
            this.tableLayoutPnl1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPnl1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPnl1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPnl1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPnl1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPnl1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPnl1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPnl1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPnl1.Size = new System.Drawing.Size(1010, 623);
            this.tableLayoutPnl1.TabIndex = 0;
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TextBox1.Font = new System.Drawing.Font("細明體", 12F);
            this.TextBox1.Location = new System.Drawing.Point(3, 24);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.TextBox1.Size = new System.Drawing.Size(499, 266);
            this.TextBox1.TabIndex = 1;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "";
            this.TextBox1.ReadOnlyChanged += new System.EventHandler(this.TextBox1_ReadOnlyChanged);
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            this.TextBox1.Enter += new System.EventHandler(this.TextBox1_Enter);
            this.TextBox1.Leave += new System.EventHandler(this.TextBox1_Leave);
            // 
            // lblT1
            // 
            this.lblT1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(3, 5);
            this.lblT1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "發票抬頭";
            // 
            // tableLayoutPnl3
            // 
            this.tableLayoutPnl3.ColumnCount = 2;
            this.tableLayoutPnl3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPnl3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPnl3.Controls.Add(this.lblMsg, 0, 0);
            this.tableLayoutPnl3.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPnl3.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPnl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPnl3.Location = new System.Drawing.Point(505, 21);
            this.tableLayoutPnl3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPnl3.Name = "tableLayoutPnl3";
            this.tableLayoutPnl3.RowCount = 2;
            this.tableLayoutPnl3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPnl3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl3.Size = new System.Drawing.Size(505, 272);
            this.tableLayoutPnl3.TabIndex = 23;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPnl3.SetColumnSpan(this.lblMsg, 2);
            this.lblMsg.Font = new System.Drawing.Font("細明體", 12F);
            this.lblMsg.Location = new System.Drawing.Point(3, 3);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(3);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(144, 16);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "發票列印設定事項:";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblMsg1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 244);
            this.panel1.TabIndex = 24;
            // 
            // lblMsg1
            // 
            this.lblMsg1.AutoSize = true;
            this.lblMsg1.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblMsg1.Location = new System.Drawing.Point(3, 3);
            this.lblMsg1.Margin = new System.Windows.Forms.Padding(3);
            this.lblMsg1.Name = "lblMsg1";
            this.lblMsg1.Size = new System.Drawing.Size(80, 16);
            this.lblMsg1.TabIndex = 0;
            this.lblMsg1.Text = "函式說明:";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblMsg2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(255, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(247, 244);
            this.panel2.TabIndex = 25;
            // 
            // lblMsg2
            // 
            this.lblMsg2.AutoSize = true;
            this.lblMsg2.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblMsg2.Location = new System.Drawing.Point(3, 3);
            this.lblMsg2.Name = "lblMsg2";
            this.lblMsg2.Size = new System.Drawing.Size(0, 16);
            this.lblMsg2.TabIndex = 0;
            // 
            // TextBox4
            // 
            this.TextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TextBox4.Font = new System.Drawing.Font("細明體", 12F);
            this.TextBox4.Location = new System.Drawing.Point(3, 470);
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.ReadOnly = true;
            this.tableLayoutPnl1.SetRowSpan(this.TextBox4, 2);
            this.TextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.TextBox4.Size = new System.Drawing.Size(499, 150);
            this.TextBox4.TabIndex = 4;
            this.TextBox4.TabStop = false;
            this.TextBox4.Text = "";
            this.TextBox4.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            this.TextBox4.Enter += new System.EventHandler(this.TextBox1_Enter);
            this.TextBox4.Leave += new System.EventHandler(this.TextBox1_Leave);
            // 
            // lblT4
            // 
            this.lblT4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(3, 451);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "發票註腳";
            // 
            // Detail2
            // 
            this.Detail2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Detail2.Font = new System.Drawing.Font("細明體", 12F);
            this.Detail2.Location = new System.Drawing.Point(3, 391);
            this.Detail2.Name = "Detail2";
            this.Detail2.ReadOnly = true;
            this.Detail2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.Detail2.Size = new System.Drawing.Size(487, 57);
            this.Detail2.TabIndex = 3;
            this.Detail2.TabStop = false;
            this.Detail2.Text = "";
            this.Detail2.Enter += new System.EventHandler(this.TextBox1_Enter);
            this.Detail2.Leave += new System.EventHandler(this.TextBox1_Leave);
            // 
            // lblT3
            // 
            this.lblT3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(3, 372);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(184, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "銷售數量>1時，列印內容";
            // 
            // Detail1
            // 
            this.Detail1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Detail1.Font = new System.Drawing.Font("細明體", 12F);
            this.Detail1.Location = new System.Drawing.Point(3, 312);
            this.Detail1.Name = "Detail1";
            this.Detail1.ReadOnly = true;
            this.Detail1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.Detail1.Size = new System.Drawing.Size(487, 57);
            this.Detail1.TabIndex = 2;
            this.Detail1.TabStop = false;
            this.Detail1.Text = "";
            this.Detail1.Enter += new System.EventHandler(this.TextBox1_Enter);
            this.Detail1.Leave += new System.EventHandler(this.TextBox1_Leave);
            // 
            // lblT2
            // 
            this.lblT2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(3, 293);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(192, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "銷售數量為1時，列印內容";
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBoxT1.Controls.Add(this.tableLayoutPnl4);
            this.pnlBoxT1.Location = new System.Drawing.Point(508, 296);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.tableLayoutPnl1.SetRowSpan(this.pnlBoxT1, 6);
            this.pnlBoxT1.Size = new System.Drawing.Size(499, 241);
            this.pnlBoxT1.TabIndex = 5;
            // 
            // tableLayoutPnl4
            // 
            this.tableLayoutPnl4.ColumnCount = 4;
            this.tableLayoutPnl4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPnl4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPnl4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPnl4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPnl4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPnl4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPnl4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPnl4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPnl4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPnl4.Controls.Add(this.tableLayoutPnl7, 1, 5);
            this.tableLayoutPnl4.Controls.Add(this.lblT5, 0, 0);
            this.tableLayoutPnl4.Controls.Add(this.lblT6, 2, 0);
            this.tableLayoutPnl4.Controls.Add(this.lblT7, 0, 1);
            this.tableLayoutPnl4.Controls.Add(this.lblT8, 2, 1);
            this.tableLayoutPnl4.Controls.Add(this.lblT9, 0, 2);
            this.tableLayoutPnl4.Controls.Add(this.lblT10, 0, 4);
            this.tableLayoutPnl4.Controls.Add(this.lblT11, 0, 5);
            this.tableLayoutPnl4.Controls.Add(this.t1, 1, 0);
            this.tableLayoutPnl4.Controls.Add(this.t2, 3, 0);
            this.tableLayoutPnl4.Controls.Add(this.t4, 3, 1);
            this.tableLayoutPnl4.Controls.Add(this.t3, 1, 1);
            this.tableLayoutPnl4.Controls.Add(this.tableLayoutPnl5, 1, 2);
            this.tableLayoutPnl4.Controls.Add(this.tableLayoutPnl6, 1, 4);
            this.tableLayoutPnl4.Controls.Add(this.lblT20, 0, 6);
            this.tableLayoutPnl4.Controls.Add(this.Recover1, 1, 6);
            this.tableLayoutPnl4.Controls.Add(this.Recover2, 2, 6);
            this.tableLayoutPnl4.Controls.Add(this.tableLayoutPnl2, 1, 3);
            this.tableLayoutPnl4.Controls.Add(this.lblT24, 0, 3);
            this.tableLayoutPnl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPnl4.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPnl4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPnl4.Name = "tableLayoutPnl4";
            this.tableLayoutPnl4.RowCount = 7;
            this.tableLayoutPnl4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPnl4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPnl4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPnl4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPnl4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPnl4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPnl4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPnl4.Size = new System.Drawing.Size(469, 211);
            this.tableLayoutPnl4.TabIndex = 9;
            // 
            // tableLayoutPnl7
            // 
            this.tableLayoutPnl7.ColumnCount = 7;
            this.tableLayoutPnl4.SetColumnSpan(this.tableLayoutPnl7, 3);
            this.tableLayoutPnl7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPnl7.Controls.Add(this.radio10, 0, 0);
            this.tableLayoutPnl7.Controls.Add(this.radio11, 2, 0);
            this.tableLayoutPnl7.Controls.Add(this.radio12, 4, 0);
            this.tableLayoutPnl7.Controls.Add(this.lbl10, 1, 0);
            this.tableLayoutPnl7.Controls.Add(this.lbl11, 3, 0);
            this.tableLayoutPnl7.Controls.Add(this.lbl12, 5, 0);
            this.tableLayoutPnl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPnl7.Location = new System.Drawing.Point(117, 150);
            this.tableLayoutPnl7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPnl7.Name = "tableLayoutPnl7";
            this.tableLayoutPnl7.RowCount = 1;
            this.tableLayoutPnl7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl7.Size = new System.Drawing.Size(352, 30);
            this.tableLayoutPnl7.TabIndex = 8;
            // 
            // radio10
            // 
            this.radio10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio10.AutoSize = true;
            this.radio10.BackColor = System.Drawing.Color.Transparent;
            this.radio10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio10.Enabled = false;
            this.radio10.Font = new System.Drawing.Font("細明體", 12F);
            this.radio10.Location = new System.Drawing.Point(3, 8);
            this.radio10.Name = "radio10";
            this.radio10.Size = new System.Drawing.Size(14, 13);
            this.radio10.TabIndex = 0;
            this.radio10.Tag = "";
            this.radio10.UseVisualStyleBackColor = true;
            this.radio10.Visible = false;
            this.radio10.CheckedChanged += new System.EventHandler(this.radio10_CheckedChanged);
            // 
            // radio11
            // 
            this.radio11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio11.AutoSize = true;
            this.radio11.BackColor = System.Drawing.Color.Transparent;
            this.radio11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio11.Enabled = false;
            this.radio11.Font = new System.Drawing.Font("細明體", 12F);
            this.radio11.Location = new System.Drawing.Point(69, 8);
            this.radio11.Name = "radio11";
            this.radio11.Size = new System.Drawing.Size(14, 13);
            this.radio11.TabIndex = 1;
            this.radio11.Tag = "";
            this.radio11.UseVisualStyleBackColor = true;
            this.radio11.Visible = false;
            this.radio11.CheckedChanged += new System.EventHandler(this.radio10_CheckedChanged);
            // 
            // radio12
            // 
            this.radio12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio12.AutoSize = true;
            this.radio12.BackColor = System.Drawing.Color.Transparent;
            this.radio12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio12.Enabled = false;
            this.radio12.Font = new System.Drawing.Font("細明體", 12F);
            this.radio12.Location = new System.Drawing.Point(135, 8);
            this.radio12.Name = "radio12";
            this.radio12.Size = new System.Drawing.Size(14, 13);
            this.radio12.TabIndex = 2;
            this.radio12.Tag = "";
            this.radio12.UseVisualStyleBackColor = true;
            this.radio12.Visible = false;
            this.radio12.CheckedChanged += new System.EventHandler(this.radio10_CheckedChanged);
            // 
            // lbl10
            // 
            this.lbl10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl10.AutoSize = true;
            this.lbl10.BackColor = System.Drawing.Color.Transparent;
            this.lbl10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl10.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl10.Location = new System.Drawing.Point(23, 7);
            this.lbl10.Name = "lbl10";
            this.lbl10.Size = new System.Drawing.Size(40, 16);
            this.lbl10.TabIndex = 0;
            this.lbl10.Text = "Com1";
            this.lbl10.Visible = false;
            this.lbl10.Click += new System.EventHandler(this.lbl10_Click);
            // 
            // lbl11
            // 
            this.lbl11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl11.AutoSize = true;
            this.lbl11.BackColor = System.Drawing.Color.Transparent;
            this.lbl11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl11.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl11.Location = new System.Drawing.Point(89, 7);
            this.lbl11.Name = "lbl11";
            this.lbl11.Size = new System.Drawing.Size(40, 16);
            this.lbl11.TabIndex = 0;
            this.lbl11.Text = "Com2";
            this.lbl11.Visible = false;
            this.lbl11.Click += new System.EventHandler(this.lbl10_Click);
            // 
            // lbl12
            // 
            this.lbl12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl12.AutoSize = true;
            this.lbl12.BackColor = System.Drawing.Color.Transparent;
            this.lbl12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl12.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl12.Location = new System.Drawing.Point(155, 7);
            this.lbl12.Name = "lbl12";
            this.lbl12.Size = new System.Drawing.Size(24, 16);
            this.lbl12.TabIndex = 0;
            this.lbl12.Text = "無";
            this.lbl12.Visible = false;
            this.lbl12.Click += new System.EventHandler(this.lbl10_Click);
            // 
            // lblT5
            // 
            this.lblT5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(18, 7);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(96, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "抬 頭 筆 數";
            // 
            // lblT6
            // 
            this.lblT6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(256, 7);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(72, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "抬頭跳行";
            // 
            // lblT7
            // 
            this.lblT7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(18, 37);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(96, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "明 細 筆 數";
            // 
            // lblT8
            // 
            this.lblT8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(256, 37);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(72, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "註腳筆數";
            // 
            // lblT9
            // 
            this.lblT9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(26, 67);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(88, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "蓋  電  章";
            this.lblT9.Visible = false;
            // 
            // lblT10
            // 
            this.lblT10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblT10.AutoSize = true;
            this.lblT10.BackColor = System.Drawing.Color.Transparent;
            this.lblT10.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT10.Location = new System.Drawing.Point(26, 127);
            this.lblT10.Name = "lblT10";
            this.lblT10.Size = new System.Drawing.Size(88, 16);
            this.lblT10.TabIndex = 0;
            this.lblT10.Text = "錢櫃連接埠";
            this.lblT10.Visible = false;
            // 
            // lblT11
            // 
            this.lblT11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblT11.AutoSize = true;
            this.lblT11.BackColor = System.Drawing.Color.Transparent;
            this.lblT11.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT11.Location = new System.Drawing.Point(26, 157);
            this.lblT11.Name = "lblT11";
            this.lblT11.Size = new System.Drawing.Size(88, 16);
            this.lblT11.TabIndex = 0;
            this.lblT11.Text = "客顯連接埠";
            this.lblT11.Visible = false;
            // 
            // t1
            // 
            this.t1.AllowGrayBackColor = false;
            this.t1.AllowResize = true;
            this.t1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.t1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.t1.Font = new System.Drawing.Font("細明體", 12F);
            this.t1.Location = new System.Drawing.Point(120, 3);
            this.t1.MaxLength = 2;
            this.t1.Name = "t1";
            this.t1.oLen = 0;
            this.t1.ReadOnly = true;
            this.t1.Size = new System.Drawing.Size(23, 27);
            this.t1.TabIndex = 1;
            this.t1.TabStop = false;
            this.t1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // t2
            // 
            this.t2.AllowGrayBackColor = false;
            this.t2.AllowResize = true;
            this.t2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.t2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.t2.Font = new System.Drawing.Font("細明體", 12F);
            this.t2.Location = new System.Drawing.Point(354, 3);
            this.t2.MaxLength = 2;
            this.t2.Name = "t2";
            this.t2.oLen = 0;
            this.t2.ReadOnly = true;
            this.t2.Size = new System.Drawing.Size(23, 27);
            this.t2.TabIndex = 2;
            this.t2.TabStop = false;
            this.t2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.t2.Validating += new System.ComponentModel.CancelEventHandler(this.t2_Validating);
            // 
            // t4
            // 
            this.t4.AllowGrayBackColor = false;
            this.t4.AllowResize = true;
            this.t4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.t4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.t4.Font = new System.Drawing.Font("細明體", 12F);
            this.t4.Location = new System.Drawing.Point(354, 33);
            this.t4.MaxLength = 2;
            this.t4.Name = "t4";
            this.t4.oLen = 0;
            this.t4.ReadOnly = true;
            this.t4.Size = new System.Drawing.Size(23, 27);
            this.t4.TabIndex = 4;
            this.t4.TabStop = false;
            this.t4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // t3
            // 
            this.t3.AllowGrayBackColor = false;
            this.t3.AllowResize = true;
            this.t3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.t3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.t3.Font = new System.Drawing.Font("細明體", 12F);
            this.t3.Location = new System.Drawing.Point(120, 33);
            this.t3.MaxLength = 2;
            this.t3.Name = "t3";
            this.t3.oLen = 0;
            this.t3.ReadOnly = true;
            this.t3.Size = new System.Drawing.Size(23, 27);
            this.t3.TabIndex = 3;
            this.t3.TabStop = false;
            this.t3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.t3.Validating += new System.ComponentModel.CancelEventHandler(this.t2_Validating);
            // 
            // tableLayoutPnl5
            // 
            this.tableLayoutPnl5.ColumnCount = 5;
            this.tableLayoutPnl4.SetColumnSpan(this.tableLayoutPnl5, 2);
            this.tableLayoutPnl5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPnl5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPnl5.Controls.Add(this.radio1, 0, 0);
            this.tableLayoutPnl5.Controls.Add(this.radio2, 2, 0);
            this.tableLayoutPnl5.Controls.Add(this.lbl1, 1, 0);
            this.tableLayoutPnl5.Controls.Add(this.lbl2, 3, 0);
            this.tableLayoutPnl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPnl5.Location = new System.Drawing.Point(117, 60);
            this.tableLayoutPnl5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPnl5.Name = "tableLayoutPnl5";
            this.tableLayoutPnl5.RowCount = 1;
            this.tableLayoutPnl5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl5.Size = new System.Drawing.Size(234, 30);
            this.tableLayoutPnl5.TabIndex = 5;
            // 
            // radio1
            // 
            this.radio1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio1.AutoSize = true;
            this.radio1.BackColor = System.Drawing.Color.Transparent;
            this.radio1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio1.Enabled = false;
            this.radio1.Font = new System.Drawing.Font("細明體", 12F);
            this.radio1.Location = new System.Drawing.Point(3, 8);
            this.radio1.Name = "radio1";
            this.radio1.Size = new System.Drawing.Size(14, 13);
            this.radio1.TabIndex = 0;
            this.radio1.Tag = "";
            this.radio1.UseVisualStyleBackColor = true;
            this.radio1.Visible = false;
            this.radio1.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // radio2
            // 
            this.radio2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio2.AutoSize = true;
            this.radio2.BackColor = System.Drawing.Color.Transparent;
            this.radio2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio2.Enabled = false;
            this.radio2.Font = new System.Drawing.Font("細明體", 12F);
            this.radio2.Location = new System.Drawing.Point(53, 8);
            this.radio2.Name = "radio2";
            this.radio2.Size = new System.Drawing.Size(14, 13);
            this.radio2.TabIndex = 1;
            this.radio2.Tag = "";
            this.radio2.UseVisualStyleBackColor = true;
            this.radio2.Visible = false;
            this.radio2.CheckedChanged += new System.EventHandler(this.radio1_CheckedChanged);
            // 
            // lbl1
            // 
            this.lbl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl1.AutoSize = true;
            this.lbl1.BackColor = System.Drawing.Color.Transparent;
            this.lbl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl1.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl1.Location = new System.Drawing.Point(23, 7);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(24, 16);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "是";
            this.lbl1.Visible = false;
            this.lbl1.Click += new System.EventHandler(this.lbl1_Click);
            // 
            // lbl2
            // 
            this.lbl2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl2.AutoSize = true;
            this.lbl2.BackColor = System.Drawing.Color.Transparent;
            this.lbl2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl2.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl2.Location = new System.Drawing.Point(73, 7);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(24, 16);
            this.lbl2.TabIndex = 0;
            this.lbl2.Text = "否";
            this.lbl2.Visible = false;
            this.lbl2.Click += new System.EventHandler(this.lbl1_Click);
            // 
            // tableLayoutPnl6
            // 
            this.tableLayoutPnl6.ColumnCount = 9;
            this.tableLayoutPnl4.SetColumnSpan(this.tableLayoutPnl6, 3);
            this.tableLayoutPnl6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl6.Controls.Add(this.radio6, 0, 0);
            this.tableLayoutPnl6.Controls.Add(this.radio7, 2, 0);
            this.tableLayoutPnl6.Controls.Add(this.radio8, 4, 0);
            this.tableLayoutPnl6.Controls.Add(this.lbl6, 1, 0);
            this.tableLayoutPnl6.Controls.Add(this.lbl7, 3, 0);
            this.tableLayoutPnl6.Controls.Add(this.lbl8, 5, 0);
            this.tableLayoutPnl6.Controls.Add(this.lbl9, 7, 0);
            this.tableLayoutPnl6.Controls.Add(this.radio9, 6, 0);
            this.tableLayoutPnl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPnl6.Location = new System.Drawing.Point(117, 120);
            this.tableLayoutPnl6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPnl6.Name = "tableLayoutPnl6";
            this.tableLayoutPnl6.RowCount = 1;
            this.tableLayoutPnl6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPnl6.Size = new System.Drawing.Size(352, 30);
            this.tableLayoutPnl6.TabIndex = 7;
            // 
            // radio6
            // 
            this.radio6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio6.AutoSize = true;
            this.radio6.BackColor = System.Drawing.Color.Transparent;
            this.radio6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio6.Enabled = false;
            this.radio6.Font = new System.Drawing.Font("細明體", 12F);
            this.radio6.Location = new System.Drawing.Point(3, 8);
            this.radio6.Name = "radio6";
            this.radio6.Size = new System.Drawing.Size(14, 13);
            this.radio6.TabIndex = 0;
            this.radio6.Tag = "";
            this.radio6.UseVisualStyleBackColor = true;
            this.radio6.Visible = false;
            this.radio6.CheckedChanged += new System.EventHandler(this.radio6_CheckedChanged);
            // 
            // radio7
            // 
            this.radio7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio7.AutoSize = true;
            this.radio7.BackColor = System.Drawing.Color.Transparent;
            this.radio7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio7.Enabled = false;
            this.radio7.Font = new System.Drawing.Font("細明體", 12F);
            this.radio7.Location = new System.Drawing.Point(69, 8);
            this.radio7.Name = "radio7";
            this.radio7.Size = new System.Drawing.Size(14, 13);
            this.radio7.TabIndex = 1;
            this.radio7.Tag = "";
            this.radio7.UseVisualStyleBackColor = true;
            this.radio7.Visible = false;
            this.radio7.CheckedChanged += new System.EventHandler(this.radio6_CheckedChanged);
            // 
            // radio8
            // 
            this.radio8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio8.AutoSize = true;
            this.radio8.BackColor = System.Drawing.Color.Transparent;
            this.radio8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio8.Enabled = false;
            this.radio8.Font = new System.Drawing.Font("細明體", 12F);
            this.radio8.Location = new System.Drawing.Point(135, 8);
            this.radio8.Name = "radio8";
            this.radio8.Size = new System.Drawing.Size(14, 13);
            this.radio8.TabIndex = 2;
            this.radio8.Tag = "";
            this.radio8.UseVisualStyleBackColor = true;
            this.radio8.Visible = false;
            this.radio8.CheckedChanged += new System.EventHandler(this.radio6_CheckedChanged);
            // 
            // lbl6
            // 
            this.lbl6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl6.AutoSize = true;
            this.lbl6.BackColor = System.Drawing.Color.Transparent;
            this.lbl6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl6.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl6.Location = new System.Drawing.Point(23, 7);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(40, 16);
            this.lbl6.TabIndex = 0;
            this.lbl6.Text = "Com1";
            this.lbl6.Visible = false;
            this.lbl6.Click += new System.EventHandler(this.lbl6_Click);
            // 
            // lbl7
            // 
            this.lbl7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl7.AutoSize = true;
            this.lbl7.BackColor = System.Drawing.Color.Transparent;
            this.lbl7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl7.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl7.Location = new System.Drawing.Point(89, 7);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(40, 16);
            this.lbl7.TabIndex = 0;
            this.lbl7.Text = "Com2";
            this.lbl7.Visible = false;
            this.lbl7.Click += new System.EventHandler(this.lbl6_Click);
            // 
            // lbl8
            // 
            this.lbl8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl8.AutoSize = true;
            this.lbl8.BackColor = System.Drawing.Color.Transparent;
            this.lbl8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl8.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl8.Location = new System.Drawing.Point(155, 7);
            this.lbl8.Name = "lbl8";
            this.lbl8.Size = new System.Drawing.Size(72, 16);
            this.lbl8.TabIndex = 0;
            this.lbl8.Text = "連發票機";
            this.lbl8.Visible = false;
            this.lbl8.Click += new System.EventHandler(this.lbl6_Click);
            // 
            // lbl9
            // 
            this.lbl9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl9.AutoSize = true;
            this.lbl9.BackColor = System.Drawing.Color.Transparent;
            this.lbl9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl9.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl9.Location = new System.Drawing.Point(253, 7);
            this.lbl9.Name = "lbl9";
            this.lbl9.Size = new System.Drawing.Size(24, 16);
            this.lbl9.TabIndex = 0;
            this.lbl9.Text = "無";
            this.lbl9.Visible = false;
            this.lbl9.Click += new System.EventHandler(this.lbl6_Click);
            // 
            // radio9
            // 
            this.radio9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio9.AutoSize = true;
            this.radio9.BackColor = System.Drawing.Color.Transparent;
            this.radio9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio9.Enabled = false;
            this.radio9.Font = new System.Drawing.Font("細明體", 12F);
            this.radio9.Location = new System.Drawing.Point(233, 8);
            this.radio9.Name = "radio9";
            this.radio9.Size = new System.Drawing.Size(14, 13);
            this.radio9.TabIndex = 3;
            this.radio9.Tag = "";
            this.radio9.UseVisualStyleBackColor = true;
            this.radio9.Visible = false;
            this.radio9.CheckedChanged += new System.EventHandler(this.radio6_CheckedChanged);
            // 
            // lblT20
            // 
            this.lblT20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblT20.AutoSize = true;
            this.lblT20.BackColor = System.Drawing.Color.Transparent;
            this.lblT20.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT20.Location = new System.Drawing.Point(10, 187);
            this.lblT20.Name = "lblT20";
            this.lblT20.Size = new System.Drawing.Size(104, 16);
            this.lblT20.TabIndex = 0;
            this.lblT20.Text = "還原成預設值";
            // 
            // Recover1
            // 
            this.Recover1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Recover1.AutoSize = true;
            this.Recover1.Enabled = false;
            this.Recover1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Recover1.Location = new System.Drawing.Point(120, 183);
            this.Recover1.Name = "Recover1";
            this.Recover1.Size = new System.Drawing.Size(98, 25);
            this.Recover1.TabIndex = 11;
            this.Recover1.Text = "二聯式發票";
            this.Recover1.UseVisualStyleBackColor = true;
            this.Recover1.Click += new System.EventHandler(this.Recover1_Click);
            // 
            // Recover2
            // 
            this.Recover2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Recover2.AutoSize = true;
            this.Recover2.Enabled = false;
            this.Recover2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Recover2.Location = new System.Drawing.Point(237, 183);
            this.Recover2.Name = "Recover2";
            this.Recover2.Size = new System.Drawing.Size(98, 25);
            this.Recover2.TabIndex = 12;
            this.Recover2.Text = "三聯式發票";
            this.Recover2.UseVisualStyleBackColor = true;
            this.Recover2.Click += new System.EventHandler(this.Recover2_Click);
            // 
            // tableLayoutPnl2
            // 
            this.tableLayoutPnl2.ColumnCount = 7;
            this.tableLayoutPnl4.SetColumnSpan(this.tableLayoutPnl2, 3);
            this.tableLayoutPnl2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPnl2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl2.Controls.Add(this.radio3, 0, 0);
            this.tableLayoutPnl2.Controls.Add(this.radio4, 2, 0);
            this.tableLayoutPnl2.Controls.Add(this.radio5, 4, 0);
            this.tableLayoutPnl2.Controls.Add(this.lbl3, 1, 0);
            this.tableLayoutPnl2.Controls.Add(this.lbl4, 3, 0);
            this.tableLayoutPnl2.Controls.Add(this.lbl5, 5, 0);
            this.tableLayoutPnl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPnl2.Location = new System.Drawing.Point(117, 90);
            this.tableLayoutPnl2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPnl2.Name = "tableLayoutPnl2";
            this.tableLayoutPnl2.RowCount = 1;
            this.tableLayoutPnl2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnl2.Size = new System.Drawing.Size(352, 30);
            this.tableLayoutPnl2.TabIndex = 6;
            // 
            // radio3
            // 
            this.radio3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio3.AutoSize = true;
            this.radio3.BackColor = System.Drawing.Color.Transparent;
            this.radio3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio3.Enabled = false;
            this.radio3.Font = new System.Drawing.Font("細明體", 12F);
            this.radio3.Location = new System.Drawing.Point(3, 8);
            this.radio3.Name = "radio3";
            this.radio3.Size = new System.Drawing.Size(14, 13);
            this.radio3.TabIndex = 0;
            this.radio3.Tag = "";
            this.radio3.UseVisualStyleBackColor = true;
            this.radio3.Visible = false;
            this.radio3.CheckedChanged += new System.EventHandler(this.radio3_CheckedChanged);
            // 
            // radio4
            // 
            this.radio4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio4.AutoSize = true;
            this.radio4.BackColor = System.Drawing.Color.Transparent;
            this.radio4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio4.Enabled = false;
            this.radio4.Font = new System.Drawing.Font("細明體", 12F);
            this.radio4.Location = new System.Drawing.Point(69, 8);
            this.radio4.Name = "radio4";
            this.radio4.Size = new System.Drawing.Size(14, 13);
            this.radio4.TabIndex = 1;
            this.radio4.Tag = "";
            this.radio4.UseVisualStyleBackColor = true;
            this.radio4.Visible = false;
            this.radio4.CheckedChanged += new System.EventHandler(this.radio3_CheckedChanged);
            // 
            // radio5
            // 
            this.radio5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radio5.AutoSize = true;
            this.radio5.BackColor = System.Drawing.Color.Transparent;
            this.radio5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio5.Enabled = false;
            this.radio5.Font = new System.Drawing.Font("細明體", 12F);
            this.radio5.Location = new System.Drawing.Point(135, 8);
            this.radio5.Name = "radio5";
            this.radio5.Size = new System.Drawing.Size(14, 13);
            this.radio5.TabIndex = 2;
            this.radio5.Tag = "";
            this.radio5.UseVisualStyleBackColor = true;
            this.radio5.Visible = false;
            this.radio5.CheckedChanged += new System.EventHandler(this.radio3_CheckedChanged);
            // 
            // lbl3
            // 
            this.lbl3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl3.AutoSize = true;
            this.lbl3.BackColor = System.Drawing.Color.Transparent;
            this.lbl3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl3.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl3.Location = new System.Drawing.Point(23, 7);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(40, 16);
            this.lbl3.TabIndex = 0;
            this.lbl3.Text = "Com1";
            this.lbl3.Visible = false;
            this.lbl3.Click += new System.EventHandler(this.lbl3_Click);
            // 
            // lbl4
            // 
            this.lbl4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl4.AutoSize = true;
            this.lbl4.BackColor = System.Drawing.Color.Transparent;
            this.lbl4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl4.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl4.Location = new System.Drawing.Point(89, 7);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(40, 16);
            this.lbl4.TabIndex = 0;
            this.lbl4.Text = "Com2";
            this.lbl4.Visible = false;
            this.lbl4.Click += new System.EventHandler(this.lbl3_Click);
            // 
            // lbl5
            // 
            this.lbl5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl5.AutoSize = true;
            this.lbl5.BackColor = System.Drawing.Color.Transparent;
            this.lbl5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl5.Font = new System.Drawing.Font("細明體", 12F);
            this.lbl5.Location = new System.Drawing.Point(155, 7);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(24, 16);
            this.lbl5.TabIndex = 0;
            this.lbl5.Text = "無";
            this.lbl5.Visible = false;
            this.lbl5.Click += new System.EventHandler(this.lbl3_Click);
            // 
            // lblT24
            // 
            this.lblT24.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblT24.AutoSize = true;
            this.lblT24.BackColor = System.Drawing.Color.Transparent;
            this.lblT24.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT24.Location = new System.Drawing.Point(18, 97);
            this.lblT24.Name = "lblT24";
            this.lblT24.Size = new System.Drawing.Size(96, 16);
            this.lblT24.TabIndex = 0;
            this.lblT24.Text = "發 票 機 埠";
            this.lblT24.Visible = false;
            // 
            // panelT1
            // 
            this.panelT1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Controls.Add(this.btntPrnt);
            this.panelT1.Location = new System.Drawing.Point(580, 542);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(355, 79);
            this.panelT1.TabIndex = 6;
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
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("細明體", 9F);
            this.btnCancel.Location = new System.Drawing.Point(207, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 79);
            this.btnCancel.TabIndex = 3;
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
            this.btnSave.Location = new System.Drawing.Point(138, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 79);
            this.btnSave.TabIndex = 2;
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
            this.btnModify.Location = new System.Drawing.Point(69, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(69, 79);
            this.btnModify.TabIndex = 1;
            this.btnModify.UseDefaultSettings = false;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btntPrnt
            // 
            this.btntPrnt.BackColor = System.Drawing.SystemColors.Control;
            this.btntPrnt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btntPrnt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btntPrnt.Font = new System.Drawing.Font("細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btntPrnt.Location = new System.Drawing.Point(0, 0);
            this.btntPrnt.Name = "btntPrnt";
            this.btntPrnt.Size = new System.Drawing.Size(69, 79);
            this.btntPrnt.TabIndex = 0;
            this.btntPrnt.UseDefaultSettings = false;
            this.btntPrnt.UseVisualStyleBackColor = false;
            this.btntPrnt.Visible = false;
            this.btntPrnt.Click += new System.EventHandler(this.btntPrnt_Click);
            // 
            // statusStripT1
            // 
            this.statusStripT1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStripT1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStripT1.Location = new System.Drawing.Point(0, 623);
            this.statusStripT1.Name = "statusStripT1";
            this.statusStripT1.Size = new System.Drawing.Size(1010, 25);
            this.statusStripT1.TabIndex = 0;
            this.statusStripT1.Text = "statusStripT1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1310, 20);
            this.toolStripStatusLabel1.Text = "此功能為設定進銷存列印[2/3聯發票版面]，若僅需列印[一家公司發票]，可直接於發票抬頭打入公司資訊(如:華越資訊);若[依使用者列印多家公司發票]，發票抬頭請用" +
    "公式設定(如:iTitle())";
            // 
            // FrmInvoSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.tableLayoutPnl1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "FrmInvoSet";
            this.Text = "發票機版面設定";
            this.Load += new System.EventHandler(this.FrmInvoSet_Load);
            this.tableLayoutPnl1.ResumeLayout(false);
            this.tableLayoutPnl1.PerformLayout();
            this.tableLayoutPnl3.ResumeLayout(false);
            this.tableLayoutPnl3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlBoxT1.ResumeLayout(false);
            this.tableLayoutPnl4.ResumeLayout(false);
            this.tableLayoutPnl4.PerformLayout();
            this.tableLayoutPnl7.ResumeLayout(false);
            this.tableLayoutPnl7.PerformLayout();
            this.tableLayoutPnl5.ResumeLayout(false);
            this.tableLayoutPnl5.PerformLayout();
            this.tableLayoutPnl6.ResumeLayout(false);
            this.tableLayoutPnl6.PerformLayout();
            this.tableLayoutPnl2.ResumeLayout(false);
            this.tableLayoutPnl2.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.statusStripT1.ResumeLayout(false);
            this.statusStripT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.TableLayoutPanelbase tableLayoutPnl1;
        private JE.MyControl.RichTextBoxT TextBox4;
        private JE.MyControl.RichTextBoxT Detail2;
        private JE.MyControl.RichTextBoxT Detail1;
        private JE.MyControl.RichTextBoxT TextBox1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnCancel;
        private JE.MyControl.ButtonT btnSave;
        private JE.MyControl.ButtonT btnModify;
        private JE.MyControl.ButtonT btntPrnt;
        private JE.MyControl.LabelT lblMsg;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPnl3;
        private JE.MyControl.LabelT lblMsg2;
        private JE.MyControl.LabelT lblMsg1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection cn1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter da1; 
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPnl4;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPnl7;
        private JE.MyControl.RadioT radio10;
        private JE.MyControl.RadioT radio11;
        private JE.MyControl.RadioT radio12;
        private JE.MyControl.LabelT lbl10;
        private JE.MyControl.LabelT lbl11;
        private JE.MyControl.LabelT lbl12;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.LabelT lblT10;
        private JE.MyControl.LabelT lblT11;
        private JE.MyControl.TextBoxT t1;
        private JE.MyControl.TextBoxT t2;
        private JE.MyControl.TextBoxT t4;
        private JE.MyControl.TextBoxT t3;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPnl5;
        private JE.MyControl.RadioT radio1;
        private JE.MyControl.RadioT radio2;
        private JE.MyControl.LabelT lbl1;
        private JE.MyControl.LabelT lbl2;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPnl6;
        private JE.MyControl.RadioT radio6;
        private JE.MyControl.RadioT radio7;
        private JE.MyControl.RadioT radio8;
        private JE.MyControl.LabelT lbl6;
        private JE.MyControl.LabelT lbl7;
        private JE.MyControl.LabelT lbl8;
        private JE.MyControl.LabelT lbl9;
        private JE.MyControl.RadioT radio9;
        private JE.MyControl.LabelT lblT20;
        private System.Windows.Forms.Button Recover1;
        private System.Windows.Forms.Button Recover2;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPnl2;
        private JE.MyControl.RadioT radio3;
        private JE.MyControl.RadioT radio4;
        private JE.MyControl.RadioT radio5;
        private JE.MyControl.LabelT lbl3;
        private JE.MyControl.LabelT lbl4;
        private JE.MyControl.LabelT lbl5;
        private JE.MyControl.LabelT lblT24;
        private JE.MyControl.PanelNT panel1;
        private JE.MyControl.PanelNT panel2;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}