namespace S_61.subMenuFm_2
{
    partial class FrmInStkb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInStkb));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn1 = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.da1 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.da2 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand3 = new System.Data.SqlClient.SqlCommand();
            this.da3 = new System.Data.SqlClient.SqlDataAdapter();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.textBoxT1 = new JE.MyControl.TextBoxT();
            this.textBoxT3 = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.Sadate = new JE.MyControl.TextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.textBoxT4 = new JE.MyControl.TextBoxT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.textBoxT5 = new JE.MyControl.TextBoxT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.寄庫數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.銷貨數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.倉庫編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.倉庫名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.包裝數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.ItTrait = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品組成 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.未領數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.備註說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnDelete = new JE.MyControl.ButtonT();
            this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand4 = new System.Data.SqlClient.SqlCommand();
            this.da4 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand5 = new System.Data.SqlClient.SqlCommand();
            this.da5 = new System.Data.SqlClient.SqlDataAdapter();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT         *\r\nFROM              InStk\r\nWHERE          (sano = @sano)";
            this.sqlSelectCommand1.Connection = this.cn1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@sano", System.Data.SqlDbType.NVarChar, 20, "sano")});
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
            new System.Data.SqlClient.SqlParameter("@sano", System.Data.SqlDbType.NVarChar, 0, "sano"),
            new System.Data.SqlClient.SqlParameter("@indate", System.Data.SqlDbType.NVarChar, 0, "indate"),
            new System.Data.SqlClient.SqlParameter("@indateac", System.Data.SqlDbType.NVarChar, 0, "indateac"),
            new System.Data.SqlClient.SqlParameter("@indate1", System.Data.SqlDbType.NVarChar, 0, "indate1"),
            new System.Data.SqlClient.SqlParameter("@indate2", System.Data.SqlDbType.NVarChar, 0, "indate2"),
            new System.Data.SqlClient.SqlParameter("@indateac1", System.Data.SqlDbType.NVarChar, 0, "indateac1"),
            new System.Data.SqlClient.SqlParameter("@indateac2", System.Data.SqlDbType.NVarChar, 0, "indateac2"),
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@cono", System.Data.SqlDbType.NVarChar, 0, "cono"),
            new System.Data.SqlClient.SqlParameter("@coname1", System.Data.SqlDbType.NVarChar, 0, "coname1"),
            new System.Data.SqlClient.SqlParameter("@coname2", System.Data.SqlDbType.NVarChar, 0, "coname2"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@cuname2", System.Data.SqlDbType.NVarChar, 0, "cuname2"),
            new System.Data.SqlClient.SqlParameter("@cuname1", System.Data.SqlDbType.NVarChar, 0, "cuname1"),
            new System.Data.SqlClient.SqlParameter("@cutel1", System.Data.SqlDbType.NVarChar, 0, "cutel1"),
            new System.Data.SqlClient.SqlParameter("@cuper1", System.Data.SqlDbType.NVarChar, 0, "cuper1"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@emname", System.Data.SqlDbType.NVarChar, 0, "emname"),
            new System.Data.SqlClient.SqlParameter("@spno", System.Data.SqlDbType.NVarChar, 0, "spno"),
            new System.Data.SqlClient.SqlParameter("@spname", System.Data.SqlDbType.NVarChar, 0, "spname"),
            new System.Data.SqlClient.SqlParameter("@stno", System.Data.SqlDbType.NVarChar, 0, "stno"),
            new System.Data.SqlClient.SqlParameter("@stname", System.Data.SqlDbType.NVarChar, 0, "stname"),
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@Xa1Name", System.Data.SqlDbType.NVarChar, 0, "Xa1Name"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmnyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyf", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@x3no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x3no", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@x5no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x5no", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@seno", System.Data.SqlDbType.NVarChar, 0, "seno"),
            new System.Data.SqlClient.SqlParameter("@sename", System.Data.SqlDbType.NVarChar, 0, "sename"),
            new System.Data.SqlClient.SqlParameter("@x4no", System.Data.SqlDbType.NVarChar, 0, "x4no"),
            new System.Data.SqlClient.SqlParameter("@x4name", System.Data.SqlDbType.NVarChar, 0, "x4name"),
            new System.Data.SqlClient.SqlParameter("@tax", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "tax", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@discount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "discount", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cashmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cashmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cardmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cardmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cardno", System.Data.SqlDbType.NVarChar, 0, "cardno"),
            new System.Data.SqlClient.SqlParameter("@ticket", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ticket", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@collectmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "collectmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@getprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "getprvacc", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@acctmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "acctmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@inmemo", System.Data.SqlDbType.NVarChar, 0, "inmemo"),
            new System.Data.SqlClient.SqlParameter("@inmemo1", System.Data.SqlDbType.Text, 0, "inmemo1"),
            new System.Data.SqlClient.SqlParameter("@bracket", System.Data.SqlDbType.NVarChar, 0, "bracket"),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@invno", System.Data.SqlDbType.NVarChar, 0, "invno"),
            new System.Data.SqlClient.SqlParameter("@invdate", System.Data.SqlDbType.NVarChar, 0, "invdate"),
            new System.Data.SqlClient.SqlParameter("@invdate1", System.Data.SqlDbType.NVarChar, 0, "invdate1"),
            new System.Data.SqlClient.SqlParameter("@invname", System.Data.SqlDbType.NVarChar, 0, "invname"),
            new System.Data.SqlClient.SqlParameter("@invtaxno", System.Data.SqlDbType.NVarChar, 0, "invtaxno"),
            new System.Data.SqlClient.SqlParameter("@invaddr1", System.Data.SqlDbType.NVarChar, 0, "invaddr1"),
            new System.Data.SqlClient.SqlParameter("@invbatch", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "invbatch", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@invbatflg", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "invbatflg", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@appdate", System.Data.SqlDbType.NVarChar, 0, "appdate"),
            new System.Data.SqlClient.SqlParameter("@edtdate", System.Data.SqlDbType.NVarChar, 0, "edtdate"),
            new System.Data.SqlClient.SqlParameter("@appscno", System.Data.SqlDbType.NVarChar, 0, "appscno"),
            new System.Data.SqlClient.SqlParameter("@edtscno", System.Data.SqlDbType.NVarChar, 0, "edtscno"),
            new System.Data.SqlClient.SqlParameter("@acno", System.Data.SqlDbType.NVarChar, 0, "acno"),
            new System.Data.SqlClient.SqlParameter("@cloflag", System.Data.SqlDbType.Bit, 0, "cloflag"),
            new System.Data.SqlClient.SqlParameter("@SendAddr", System.Data.SqlDbType.NVarChar, 0, "SendAddr"),
            new System.Data.SqlClient.SqlParameter("@UsrNo", System.Data.SqlDbType.NVarChar, 0, "UsrNo"),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans"),
            new System.Data.SqlClient.SqlParameter("@DeNo", System.Data.SqlDbType.NVarChar, 0, "DeNo"),
            new System.Data.SqlClient.SqlParameter("@DeName", System.Data.SqlDbType.NVarChar, 0, "DeName")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.cn1;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@inno", System.Data.SqlDbType.NVarChar, 0, "inno"),
            new System.Data.SqlClient.SqlParameter("@sano", System.Data.SqlDbType.NVarChar, 0, "sano"),
            new System.Data.SqlClient.SqlParameter("@indate", System.Data.SqlDbType.NVarChar, 0, "indate"),
            new System.Data.SqlClient.SqlParameter("@indateac", System.Data.SqlDbType.NVarChar, 0, "indateac"),
            new System.Data.SqlClient.SqlParameter("@indate1", System.Data.SqlDbType.NVarChar, 0, "indate1"),
            new System.Data.SqlClient.SqlParameter("@indate2", System.Data.SqlDbType.NVarChar, 0, "indate2"),
            new System.Data.SqlClient.SqlParameter("@indateac1", System.Data.SqlDbType.NVarChar, 0, "indateac1"),
            new System.Data.SqlClient.SqlParameter("@indateac2", System.Data.SqlDbType.NVarChar, 0, "indateac2"),
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@cono", System.Data.SqlDbType.NVarChar, 0, "cono"),
            new System.Data.SqlClient.SqlParameter("@coname1", System.Data.SqlDbType.NVarChar, 0, "coname1"),
            new System.Data.SqlClient.SqlParameter("@coname2", System.Data.SqlDbType.NVarChar, 0, "coname2"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@cuname2", System.Data.SqlDbType.NVarChar, 0, "cuname2"),
            new System.Data.SqlClient.SqlParameter("@cuname1", System.Data.SqlDbType.NVarChar, 0, "cuname1"),
            new System.Data.SqlClient.SqlParameter("@cutel1", System.Data.SqlDbType.NVarChar, 0, "cutel1"),
            new System.Data.SqlClient.SqlParameter("@cuper1", System.Data.SqlDbType.NVarChar, 0, "cuper1"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@emname", System.Data.SqlDbType.NVarChar, 0, "emname"),
            new System.Data.SqlClient.SqlParameter("@spno", System.Data.SqlDbType.NVarChar, 0, "spno"),
            new System.Data.SqlClient.SqlParameter("@spname", System.Data.SqlDbType.NVarChar, 0, "spname"),
            new System.Data.SqlClient.SqlParameter("@stno", System.Data.SqlDbType.NVarChar, 0, "stno"),
            new System.Data.SqlClient.SqlParameter("@stname", System.Data.SqlDbType.NVarChar, 0, "stname"),
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@Xa1Name", System.Data.SqlDbType.NVarChar, 0, "Xa1Name"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmnyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyf", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@x3no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x3no", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@x5no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x5no", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@seno", System.Data.SqlDbType.NVarChar, 0, "seno"),
            new System.Data.SqlClient.SqlParameter("@sename", System.Data.SqlDbType.NVarChar, 0, "sename"),
            new System.Data.SqlClient.SqlParameter("@x4no", System.Data.SqlDbType.NVarChar, 0, "x4no"),
            new System.Data.SqlClient.SqlParameter("@x4name", System.Data.SqlDbType.NVarChar, 0, "x4name"),
            new System.Data.SqlClient.SqlParameter("@tax", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "tax", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@discount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "discount", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cashmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cashmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cardmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cardmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cardno", System.Data.SqlDbType.NVarChar, 0, "cardno"),
            new System.Data.SqlClient.SqlParameter("@ticket", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ticket", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@collectmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "collectmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@getprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "getprvacc", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@acctmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "acctmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@inmemo", System.Data.SqlDbType.NVarChar, 0, "inmemo"),
            new System.Data.SqlClient.SqlParameter("@inmemo1", System.Data.SqlDbType.Text, 0, "inmemo1"),
            new System.Data.SqlClient.SqlParameter("@bracket", System.Data.SqlDbType.NVarChar, 0, "bracket"),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@invno", System.Data.SqlDbType.NVarChar, 0, "invno"),
            new System.Data.SqlClient.SqlParameter("@invdate", System.Data.SqlDbType.NVarChar, 0, "invdate"),
            new System.Data.SqlClient.SqlParameter("@invdate1", System.Data.SqlDbType.NVarChar, 0, "invdate1"),
            new System.Data.SqlClient.SqlParameter("@invname", System.Data.SqlDbType.NVarChar, 0, "invname"),
            new System.Data.SqlClient.SqlParameter("@invtaxno", System.Data.SqlDbType.NVarChar, 0, "invtaxno"),
            new System.Data.SqlClient.SqlParameter("@invaddr1", System.Data.SqlDbType.NVarChar, 0, "invaddr1"),
            new System.Data.SqlClient.SqlParameter("@invbatch", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "invbatch", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@invbatflg", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "invbatflg", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@appdate", System.Data.SqlDbType.NVarChar, 0, "appdate"),
            new System.Data.SqlClient.SqlParameter("@edtdate", System.Data.SqlDbType.NVarChar, 0, "edtdate"),
            new System.Data.SqlClient.SqlParameter("@appscno", System.Data.SqlDbType.NVarChar, 0, "appscno"),
            new System.Data.SqlClient.SqlParameter("@edtscno", System.Data.SqlDbType.NVarChar, 0, "edtscno"),
            new System.Data.SqlClient.SqlParameter("@acno", System.Data.SqlDbType.NVarChar, 0, "acno"),
            new System.Data.SqlClient.SqlParameter("@cloflag", System.Data.SqlDbType.Bit, 0, "cloflag"),
            new System.Data.SqlClient.SqlParameter("@SendAddr", System.Data.SqlDbType.NVarChar, 0, "SendAddr"),
            new System.Data.SqlClient.SqlParameter("@UsrNo", System.Data.SqlDbType.NVarChar, 0, "UsrNo"),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans"),
            new System.Data.SqlClient.SqlParameter("@DeNo", System.Data.SqlDbType.NVarChar, 0, "DeNo"),
            new System.Data.SqlClient.SqlParameter("@DeName", System.Data.SqlDbType.NVarChar, 0, "DeName"),
            new System.Data.SqlClient.SqlParameter("@Original_inno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sano", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sano", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cono", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cono", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cono", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cono", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_coname1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "coname1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_coname1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "coname1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_coname2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "coname2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_coname2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "coname2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuname2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuname2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuname2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuname2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuname1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuname1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuname1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuname1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cutel1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cutel1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cutel1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cutel1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuper1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuper1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuper1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuper1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Xa1Name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Xa1Name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Xa1Name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Xa1Name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxmnyf", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxmnyf", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxmnyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyf", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxmnyb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxmnyb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x3no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x3no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x3no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x3no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_rate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "rate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x5no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x5no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x5no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x5no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_seno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_seno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sename", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sename", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sename", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sename", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x4no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x4no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x4no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "x4no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x4name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x4name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x4name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "x4name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_tax", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "tax", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_tax", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "tax", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmnyb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmnyb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmnyb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_discount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "discount", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_discount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "discount", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cashmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cashmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cashmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cashmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cardmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cardmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cardmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cardmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cardno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cardno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cardno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cardno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ticket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ticket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ticket", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ticket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_collectmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "collectmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_collectmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "collectmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_getprvacc", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "getprvacc", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_getprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "getprvacc", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_acctmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "acctmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_acctmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "acctmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inmemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inmemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inmemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inmemo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bracket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bracket", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invtaxno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invtaxno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invtaxno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invtaxno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invaddr1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invaddr1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invaddr1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invaddr1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invbatch", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invbatch", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invbatch", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "invbatch", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invbatflg", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invbatflg", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invbatflg", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "invbatflg", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_appdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "appdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_appdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "appdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_edtdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "edtdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_edtdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "edtdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_appscno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "appscno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_appscno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "appscno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_edtscno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "edtscno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_edtscno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "edtscno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_acno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "acno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_acno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "acno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cloflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cloflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cloflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cloflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SendAddr", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SendAddr", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SendAddr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SendAddr", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_UsrNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UsrNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_UsrNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "UsrNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DeNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DeNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DeNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DeNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DeName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DeName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DeName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DeName", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.cn1;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_inno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sano", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sano", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cono", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cono", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cono", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cono", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_coname1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "coname1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_coname1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "coname1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_coname2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "coname2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_coname2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "coname2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuname2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuname2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuname2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuname2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuname1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuname1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuname1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuname1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cutel1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cutel1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cutel1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cutel1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuper1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuper1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuper1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuper1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Xa1Name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Xa1Name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Xa1Name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Xa1Name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxmnyf", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxmnyf", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxmnyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyf", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxmnyb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxmnyb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x3no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x3no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x3no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x3no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_rate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "rate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x5no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x5no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x5no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x5no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_seno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_seno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sename", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sename", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sename", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sename", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x4no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x4no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x4no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "x4no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x4name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x4name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x4name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "x4name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_tax", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "tax", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_tax", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "tax", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmnyb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmnyb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmnyb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_discount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "discount", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_discount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "discount", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cashmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cashmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cashmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cashmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cardmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cardmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cardmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cardmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cardno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cardno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cardno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cardno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ticket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ticket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ticket", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ticket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_collectmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "collectmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_collectmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "collectmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_getprvacc", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "getprvacc", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_getprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "getprvacc", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_acctmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "acctmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_acctmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "acctmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inmemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inmemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inmemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inmemo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bracket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bracket", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invtaxno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invtaxno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invtaxno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invtaxno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invaddr1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invaddr1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invaddr1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invaddr1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invbatch", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invbatch", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invbatch", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "invbatch", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invbatflg", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invbatflg", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invbatflg", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "invbatflg", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_appdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "appdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_appdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "appdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_edtdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "edtdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_edtdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "edtdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_appscno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "appscno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_appscno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "appscno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_edtscno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "edtscno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_edtscno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "edtscno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_acno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "acno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_acno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "acno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cloflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cloflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cloflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cloflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SendAddr", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SendAddr", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SendAddr", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SendAddr", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_UsrNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UsrNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_UsrNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "UsrNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DeNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DeNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DeNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DeNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DeName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DeName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DeName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DeName", System.Data.DataRowVersion.Original, null)});
            // 
            // da1
            // 
            this.da1.DeleteCommand = this.sqlDeleteCommand1;
            this.da1.InsertCommand = this.sqlInsertCommand1;
            this.da1.SelectCommand = this.sqlSelectCommand1;
            this.da1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "InStk", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("inno", "inno"),
                        new System.Data.Common.DataColumnMapping("sano", "sano"),
                        new System.Data.Common.DataColumnMapping("indate", "indate"),
                        new System.Data.Common.DataColumnMapping("indateac", "indateac"),
                        new System.Data.Common.DataColumnMapping("indate1", "indate1"),
                        new System.Data.Common.DataColumnMapping("indate2", "indate2"),
                        new System.Data.Common.DataColumnMapping("indateac1", "indateac1"),
                        new System.Data.Common.DataColumnMapping("indateac2", "indateac2"),
                        new System.Data.Common.DataColumnMapping("quno", "quno"),
                        new System.Data.Common.DataColumnMapping("cono", "cono"),
                        new System.Data.Common.DataColumnMapping("coname1", "coname1"),
                        new System.Data.Common.DataColumnMapping("coname2", "coname2"),
                        new System.Data.Common.DataColumnMapping("cuno", "cuno"),
                        new System.Data.Common.DataColumnMapping("cuname2", "cuname2"),
                        new System.Data.Common.DataColumnMapping("cuname1", "cuname1"),
                        new System.Data.Common.DataColumnMapping("cutel1", "cutel1"),
                        new System.Data.Common.DataColumnMapping("cuper1", "cuper1"),
                        new System.Data.Common.DataColumnMapping("emno", "emno"),
                        new System.Data.Common.DataColumnMapping("emname", "emname"),
                        new System.Data.Common.DataColumnMapping("spno", "spno"),
                        new System.Data.Common.DataColumnMapping("spname", "spname"),
                        new System.Data.Common.DataColumnMapping("stno", "stno"),
                        new System.Data.Common.DataColumnMapping("stname", "stname"),
                        new System.Data.Common.DataColumnMapping("xa1no", "xa1no"),
                        new System.Data.Common.DataColumnMapping("Xa1Name", "Xa1Name"),
                        new System.Data.Common.DataColumnMapping("xa1par", "xa1par"),
                        new System.Data.Common.DataColumnMapping("taxmnyf", "taxmnyf"),
                        new System.Data.Common.DataColumnMapping("taxmnyb", "taxmnyb"),
                        new System.Data.Common.DataColumnMapping("taxmny", "taxmny"),
                        new System.Data.Common.DataColumnMapping("x3no", "x3no"),
                        new System.Data.Common.DataColumnMapping("rate", "rate"),
                        new System.Data.Common.DataColumnMapping("x5no", "x5no"),
                        new System.Data.Common.DataColumnMapping("seno", "seno"),
                        new System.Data.Common.DataColumnMapping("sename", "sename"),
                        new System.Data.Common.DataColumnMapping("x4no", "x4no"),
                        new System.Data.Common.DataColumnMapping("x4name", "x4name"),
                        new System.Data.Common.DataColumnMapping("tax", "tax"),
                        new System.Data.Common.DataColumnMapping("totmny", "totmny"),
                        new System.Data.Common.DataColumnMapping("taxb", "taxb"),
                        new System.Data.Common.DataColumnMapping("totmnyb", "totmnyb"),
                        new System.Data.Common.DataColumnMapping("discount", "discount"),
                        new System.Data.Common.DataColumnMapping("cashmny", "cashmny"),
                        new System.Data.Common.DataColumnMapping("cardmny", "cardmny"),
                        new System.Data.Common.DataColumnMapping("cardno", "cardno"),
                        new System.Data.Common.DataColumnMapping("ticket", "ticket"),
                        new System.Data.Common.DataColumnMapping("collectmny", "collectmny"),
                        new System.Data.Common.DataColumnMapping("getprvacc", "getprvacc"),
                        new System.Data.Common.DataColumnMapping("acctmny", "acctmny"),
                        new System.Data.Common.DataColumnMapping("inmemo", "inmemo"),
                        new System.Data.Common.DataColumnMapping("inmemo1", "inmemo1"),
                        new System.Data.Common.DataColumnMapping("bracket", "bracket"),
                        new System.Data.Common.DataColumnMapping("recordno", "recordno"),
                        new System.Data.Common.DataColumnMapping("invno", "invno"),
                        new System.Data.Common.DataColumnMapping("invdate", "invdate"),
                        new System.Data.Common.DataColumnMapping("invdate1", "invdate1"),
                        new System.Data.Common.DataColumnMapping("invname", "invname"),
                        new System.Data.Common.DataColumnMapping("invtaxno", "invtaxno"),
                        new System.Data.Common.DataColumnMapping("invaddr1", "invaddr1"),
                        new System.Data.Common.DataColumnMapping("invbatch", "invbatch"),
                        new System.Data.Common.DataColumnMapping("invbatflg", "invbatflg"),
                        new System.Data.Common.DataColumnMapping("appdate", "appdate"),
                        new System.Data.Common.DataColumnMapping("edtdate", "edtdate"),
                        new System.Data.Common.DataColumnMapping("appscno", "appscno"),
                        new System.Data.Common.DataColumnMapping("edtscno", "edtscno"),
                        new System.Data.Common.DataColumnMapping("acno", "acno"),
                        new System.Data.Common.DataColumnMapping("cloflag", "cloflag"),
                        new System.Data.Common.DataColumnMapping("SendAddr", "SendAddr"),
                        new System.Data.Common.DataColumnMapping("UsrNo", "UsrNo"),
                        new System.Data.Common.DataColumnMapping("IsTrans", "IsTrans"),
                        new System.Data.Common.DataColumnMapping("DeNo", "DeNo"),
                        new System.Data.Common.DataColumnMapping("DeName", "DeName")})});
            this.da1.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "SELECT        *\r\nFROM              InStkD\r\nWHERE          (inno = @inno)";
            this.sqlSelectCommand2.Connection = this.cn1;
            this.sqlSelectCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@inno", System.Data.SqlDbType.NVarChar, 20, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inno", System.Data.DataRowVersion.Current, "1")});
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.cn1;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@inno", System.Data.SqlDbType.NVarChar, 0, "inno"),
            new System.Data.SqlClient.SqlParameter("@indate", System.Data.SqlDbType.NVarChar, 0, "indate"),
            new System.Data.SqlClient.SqlParameter("@indate1", System.Data.SqlDbType.NVarChar, 0, "indate1"),
            new System.Data.SqlClient.SqlParameter("@indate2", System.Data.SqlDbType.NVarChar, 0, "indate2"),
            new System.Data.SqlClient.SqlParameter("@indateac", System.Data.SqlDbType.NVarChar, 0, "indateac"),
            new System.Data.SqlClient.SqlParameter("@indateac1", System.Data.SqlDbType.NVarChar, 0, "indateac1"),
            new System.Data.SqlClient.SqlParameter("@indateac2", System.Data.SqlDbType.NVarChar, 0, "indateac2"),
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@cono", System.Data.SqlDbType.NVarChar, 0, "cono"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@spno", System.Data.SqlDbType.NVarChar, 0, "spno"),
            new System.Data.SqlClient.SqlParameter("@stno", System.Data.SqlDbType.NVarChar, 0, "stno"),
            new System.Data.SqlClient.SqlParameter("@stname", System.Data.SqlDbType.NVarChar, 0, "stname"),
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@seno", System.Data.SqlDbType.NVarChar, 0, "seno"),
            new System.Data.SqlClient.SqlParameter("@sename", System.Data.SqlDbType.NVarChar, 0, "sename"),
            new System.Data.SqlClient.SqlParameter("@x4no", System.Data.SqlDbType.NVarChar, 0, "x4no"),
            new System.Data.SqlClient.SqlParameter("@x4name", System.Data.SqlDbType.NVarChar, 0, "x4name"),
            new System.Data.SqlClient.SqlParameter("@orno", System.Data.SqlDbType.NVarChar, 0, "orno"),
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itunit", System.Data.SqlDbType.NVarChar, 0, "itunit"),
            new System.Data.SqlClient.SqlParameter("@itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@inqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "inqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "price", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "prs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxprice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@priceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "priceb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxpriceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxpriceb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@memo", System.Data.SqlDbType.NVarChar, 0, "memo"),
            new System.Data.SqlClient.SqlParameter("@lowzero", System.Data.SqlDbType.NVarChar, 0, "lowzero"),
            new System.Data.SqlClient.SqlParameter("@bomid", System.Data.SqlDbType.NVarChar, 0, "bomid"),
            new System.Data.SqlClient.SqlParameter("@bomrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "bomrec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@sltflag", System.Data.SqlDbType.Bit, 0, "sltflag"),
            new System.Data.SqlClient.SqlParameter("@extflag", System.Data.SqlDbType.Bit, 0, "extflag"),
            new System.Data.SqlClient.SqlParameter("@bracket", System.Data.SqlDbType.NVarChar, 0, "bracket"),
            new System.Data.SqlClient.SqlParameter("@itdesp1", System.Data.SqlDbType.NVarChar, 0, "itdesp1"),
            new System.Data.SqlClient.SqlParameter("@itdesp2", System.Data.SqlDbType.NVarChar, 0, "itdesp2"),
            new System.Data.SqlClient.SqlParameter("@itdesp3", System.Data.SqlDbType.NVarChar, 0, "itdesp3"),
            new System.Data.SqlClient.SqlParameter("@itdesp4", System.Data.SqlDbType.NVarChar, 0, "itdesp4"),
            new System.Data.SqlClient.SqlParameter("@itdesp5", System.Data.SqlDbType.NVarChar, 0, "itdesp5"),
            new System.Data.SqlClient.SqlParameter("@itdesp6", System.Data.SqlDbType.NVarChar, 0, "itdesp6"),
            new System.Data.SqlClient.SqlParameter("@itdesp7", System.Data.SqlDbType.NVarChar, 0, "itdesp7"),
            new System.Data.SqlClient.SqlParameter("@itdesp8", System.Data.SqlDbType.NVarChar, 0, "itdesp8"),
            new System.Data.SqlClient.SqlParameter("@itdesp9", System.Data.SqlDbType.NVarChar, 0, "itdesp9"),
            new System.Data.SqlClient.SqlParameter("@itdesp10", System.Data.SqlDbType.NVarChar, 0, "itdesp10"),
            new System.Data.SqlClient.SqlParameter("@RecordNo_D", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "RecordNo_D", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans"),
            new System.Data.SqlClient.SqlParameter("@SaNo", System.Data.SqlDbType.NVarChar, 0, "SaNo"),
            new System.Data.SqlClient.SqlParameter("@nonqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "nonqty", System.Data.DataRowVersion.Current, null)});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.cn1;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@inno", System.Data.SqlDbType.NVarChar, 0, "inno"),
            new System.Data.SqlClient.SqlParameter("@indate", System.Data.SqlDbType.NVarChar, 0, "indate"),
            new System.Data.SqlClient.SqlParameter("@indate1", System.Data.SqlDbType.NVarChar, 0, "indate1"),
            new System.Data.SqlClient.SqlParameter("@indate2", System.Data.SqlDbType.NVarChar, 0, "indate2"),
            new System.Data.SqlClient.SqlParameter("@indateac", System.Data.SqlDbType.NVarChar, 0, "indateac"),
            new System.Data.SqlClient.SqlParameter("@indateac1", System.Data.SqlDbType.NVarChar, 0, "indateac1"),
            new System.Data.SqlClient.SqlParameter("@indateac2", System.Data.SqlDbType.NVarChar, 0, "indateac2"),
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@cono", System.Data.SqlDbType.NVarChar, 0, "cono"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@spno", System.Data.SqlDbType.NVarChar, 0, "spno"),
            new System.Data.SqlClient.SqlParameter("@stno", System.Data.SqlDbType.NVarChar, 0, "stno"),
            new System.Data.SqlClient.SqlParameter("@stname", System.Data.SqlDbType.NVarChar, 0, "stname"),
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@seno", System.Data.SqlDbType.NVarChar, 0, "seno"),
            new System.Data.SqlClient.SqlParameter("@sename", System.Data.SqlDbType.NVarChar, 0, "sename"),
            new System.Data.SqlClient.SqlParameter("@x4no", System.Data.SqlDbType.NVarChar, 0, "x4no"),
            new System.Data.SqlClient.SqlParameter("@x4name", System.Data.SqlDbType.NVarChar, 0, "x4name"),
            new System.Data.SqlClient.SqlParameter("@orno", System.Data.SqlDbType.NVarChar, 0, "orno"),
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itunit", System.Data.SqlDbType.NVarChar, 0, "itunit"),
            new System.Data.SqlClient.SqlParameter("@itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@inqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "inqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "price", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "prs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxprice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@priceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "priceb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxpriceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxpriceb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@memo", System.Data.SqlDbType.NVarChar, 0, "memo"),
            new System.Data.SqlClient.SqlParameter("@lowzero", System.Data.SqlDbType.NVarChar, 0, "lowzero"),
            new System.Data.SqlClient.SqlParameter("@bomid", System.Data.SqlDbType.NVarChar, 0, "bomid"),
            new System.Data.SqlClient.SqlParameter("@bomrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "bomrec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@sltflag", System.Data.SqlDbType.Bit, 0, "sltflag"),
            new System.Data.SqlClient.SqlParameter("@extflag", System.Data.SqlDbType.Bit, 0, "extflag"),
            new System.Data.SqlClient.SqlParameter("@bracket", System.Data.SqlDbType.NVarChar, 0, "bracket"),
            new System.Data.SqlClient.SqlParameter("@itdesp1", System.Data.SqlDbType.NVarChar, 0, "itdesp1"),
            new System.Data.SqlClient.SqlParameter("@itdesp2", System.Data.SqlDbType.NVarChar, 0, "itdesp2"),
            new System.Data.SqlClient.SqlParameter("@itdesp3", System.Data.SqlDbType.NVarChar, 0, "itdesp3"),
            new System.Data.SqlClient.SqlParameter("@itdesp4", System.Data.SqlDbType.NVarChar, 0, "itdesp4"),
            new System.Data.SqlClient.SqlParameter("@itdesp5", System.Data.SqlDbType.NVarChar, 0, "itdesp5"),
            new System.Data.SqlClient.SqlParameter("@itdesp6", System.Data.SqlDbType.NVarChar, 0, "itdesp6"),
            new System.Data.SqlClient.SqlParameter("@itdesp7", System.Data.SqlDbType.NVarChar, 0, "itdesp7"),
            new System.Data.SqlClient.SqlParameter("@itdesp8", System.Data.SqlDbType.NVarChar, 0, "itdesp8"),
            new System.Data.SqlClient.SqlParameter("@itdesp9", System.Data.SqlDbType.NVarChar, 0, "itdesp9"),
            new System.Data.SqlClient.SqlParameter("@itdesp10", System.Data.SqlDbType.NVarChar, 0, "itdesp10"),
            new System.Data.SqlClient.SqlParameter("@RecordNo_D", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "RecordNo_D", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans"),
            new System.Data.SqlClient.SqlParameter("@SaNo", System.Data.SqlDbType.NVarChar, 0, "SaNo"),
            new System.Data.SqlClient.SqlParameter("@nonqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "nonqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Original_inID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cono", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cono", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cono", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cono", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_seno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_seno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sename", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sename", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sename", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sename", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x4no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x4no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x4no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "x4no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x4name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x4name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x4name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "x4name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_orno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_orno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ittrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itunit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpkgqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpkgqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "inqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_price", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "price", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "price", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_prs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "prs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "prs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_rate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "rate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxprice", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxprice", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxprice", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_priceb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "priceb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_priceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "priceb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxpriceb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxpriceb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxpriceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxpriceb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mnyb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mnyb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mnyb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_memo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "memo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_memo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "memo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lowzero", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lowzero", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lowzero", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lowzero", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bomid", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bomid", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bomid", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bomid", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bomrec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bomrec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bomrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "bomrec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sltflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sltflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sltflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sltflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_extflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "extflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_extflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "extflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bracket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bracket", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp3", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp4", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp5", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp6", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp6", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp6", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp6", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp7", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp7", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp7", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp7", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp8", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp8", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp8", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp8", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp9", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp9", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp9", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp9", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp10", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp10", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp10", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp10", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_RecordNo_D", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RecordNo_D", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_RecordNo_D", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "RecordNo_D", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SaNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SaNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SaNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SaNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_nonqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "nonqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_nonqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "nonqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@inID", System.Data.SqlDbType.Int, 4, "inID")});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.cn1;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_inID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "inno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_indateac2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "indateac2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_indateac2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "indateac2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cono", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cono", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cono", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cono", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_seno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_seno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sename", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sename", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sename", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sename", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x4no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x4no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x4no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "x4no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_x4name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "x4name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_x4name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "x4name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_orno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_orno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ittrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itunit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpkgqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpkgqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_inqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "inqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_inqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "inqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_price", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "price", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "price", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_prs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "prs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "prs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_rate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "rate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxprice", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxprice", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxprice", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_priceb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "priceb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_priceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "priceb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxpriceb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxpriceb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxpriceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxpriceb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mnyb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mnyb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mnyb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_memo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "memo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_memo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "memo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lowzero", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lowzero", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lowzero", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lowzero", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bomid", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bomid", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bomid", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bomid", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bomrec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bomrec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bomrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "bomrec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sltflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sltflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sltflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sltflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_extflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "extflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_extflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "extflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bracket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bracket", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp3", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp4", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp5", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp6", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp6", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp6", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp6", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp7", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp7", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp7", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp7", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp8", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp8", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp8", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp8", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp9", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp9", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp9", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp9", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp10", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp10", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp10", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp10", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_RecordNo_D", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RecordNo_D", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_RecordNo_D", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "RecordNo_D", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SaNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SaNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SaNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SaNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_nonqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "nonqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_nonqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "nonqty", System.Data.DataRowVersion.Original, null)});
            // 
            // da2
            // 
            this.da2.DeleteCommand = this.sqlDeleteCommand2;
            this.da2.InsertCommand = this.sqlInsertCommand2;
            this.da2.SelectCommand = this.sqlSelectCommand2;
            this.da2.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "InStkD", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("inID", "inID"),
                        new System.Data.Common.DataColumnMapping("inno", "inno"),
                        new System.Data.Common.DataColumnMapping("indate", "indate"),
                        new System.Data.Common.DataColumnMapping("indate1", "indate1"),
                        new System.Data.Common.DataColumnMapping("indate2", "indate2"),
                        new System.Data.Common.DataColumnMapping("indateac", "indateac"),
                        new System.Data.Common.DataColumnMapping("indateac1", "indateac1"),
                        new System.Data.Common.DataColumnMapping("indateac2", "indateac2"),
                        new System.Data.Common.DataColumnMapping("quno", "quno"),
                        new System.Data.Common.DataColumnMapping("cono", "cono"),
                        new System.Data.Common.DataColumnMapping("cuno", "cuno"),
                        new System.Data.Common.DataColumnMapping("emno", "emno"),
                        new System.Data.Common.DataColumnMapping("spno", "spno"),
                        new System.Data.Common.DataColumnMapping("stno", "stno"),
                        new System.Data.Common.DataColumnMapping("stname", "stname"),
                        new System.Data.Common.DataColumnMapping("xa1no", "xa1no"),
                        new System.Data.Common.DataColumnMapping("xa1par", "xa1par"),
                        new System.Data.Common.DataColumnMapping("seno", "seno"),
                        new System.Data.Common.DataColumnMapping("sename", "sename"),
                        new System.Data.Common.DataColumnMapping("x4no", "x4no"),
                        new System.Data.Common.DataColumnMapping("x4name", "x4name"),
                        new System.Data.Common.DataColumnMapping("orno", "orno"),
                        new System.Data.Common.DataColumnMapping("itno", "itno"),
                        new System.Data.Common.DataColumnMapping("itname", "itname"),
                        new System.Data.Common.DataColumnMapping("ittrait", "ittrait"),
                        new System.Data.Common.DataColumnMapping("itunit", "itunit"),
                        new System.Data.Common.DataColumnMapping("itpkgqty", "itpkgqty"),
                        new System.Data.Common.DataColumnMapping("inqty", "inqty"),
                        new System.Data.Common.DataColumnMapping("qty", "qty"),
                        new System.Data.Common.DataColumnMapping("price", "price"),
                        new System.Data.Common.DataColumnMapping("prs", "prs"),
                        new System.Data.Common.DataColumnMapping("rate", "rate"),
                        new System.Data.Common.DataColumnMapping("taxprice", "taxprice"),
                        new System.Data.Common.DataColumnMapping("mny", "mny"),
                        new System.Data.Common.DataColumnMapping("priceb", "priceb"),
                        new System.Data.Common.DataColumnMapping("taxpriceb", "taxpriceb"),
                        new System.Data.Common.DataColumnMapping("mnyb", "mnyb"),
                        new System.Data.Common.DataColumnMapping("memo", "memo"),
                        new System.Data.Common.DataColumnMapping("lowzero", "lowzero"),
                        new System.Data.Common.DataColumnMapping("bomid", "bomid"),
                        new System.Data.Common.DataColumnMapping("bomrec", "bomrec"),
                        new System.Data.Common.DataColumnMapping("recordno", "recordno"),
                        new System.Data.Common.DataColumnMapping("sltflag", "sltflag"),
                        new System.Data.Common.DataColumnMapping("extflag", "extflag"),
                        new System.Data.Common.DataColumnMapping("bracket", "bracket"),
                        new System.Data.Common.DataColumnMapping("itdesp1", "itdesp1"),
                        new System.Data.Common.DataColumnMapping("itdesp2", "itdesp2"),
                        new System.Data.Common.DataColumnMapping("itdesp3", "itdesp3"),
                        new System.Data.Common.DataColumnMapping("itdesp4", "itdesp4"),
                        new System.Data.Common.DataColumnMapping("itdesp5", "itdesp5"),
                        new System.Data.Common.DataColumnMapping("itdesp6", "itdesp6"),
                        new System.Data.Common.DataColumnMapping("itdesp7", "itdesp7"),
                        new System.Data.Common.DataColumnMapping("itdesp8", "itdesp8"),
                        new System.Data.Common.DataColumnMapping("itdesp9", "itdesp9"),
                        new System.Data.Common.DataColumnMapping("itdesp10", "itdesp10"),
                        new System.Data.Common.DataColumnMapping("RecordNo_D", "RecordNo_D"),
                        new System.Data.Common.DataColumnMapping("IsTrans", "IsTrans"),
                        new System.Data.Common.DataColumnMapping("SaNo", "SaNo"),
                        new System.Data.Common.DataColumnMapping("nonqty", "nonqty")})});
            this.da2.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // sqlSelectCommand3
            // 
            this.sqlSelectCommand3.CommandText = "SELECT         *\r\nFROM              instkbom\r\nWHERE          (InNo = @inno)";
            this.sqlSelectCommand3.Connection = this.cn1;
            this.sqlSelectCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@inno", System.Data.SqlDbType.NVarChar, 20, "InNo")});
            // 
            // sqlInsertCommand3
            // 
            this.sqlInsertCommand3.CommandText = resources.GetString("sqlInsertCommand3.CommandText");
            this.sqlInsertCommand3.Connection = this.cn1;
            this.sqlInsertCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@InNo", System.Data.SqlDbType.NVarChar, 0, "InNo"),
            new System.Data.SqlClient.SqlParameter("@BomID", System.Data.SqlDbType.NVarChar, 0, "BomID"),
            new System.Data.SqlClient.SqlParameter("@BomRec", System.Data.SqlDbType.NVarChar, 0, "BomRec"),
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@itunit", System.Data.SqlDbType.NVarChar, 0, "itunit"),
            new System.Data.SqlClient.SqlParameter("@itqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpareprs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpareprs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(6)), ((byte)(0)), "itrec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "itprs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itnote", System.Data.SqlDbType.NVarChar, 0, "itnote"),
            new System.Data.SqlClient.SqlParameter("@ItSource", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ItBuyPri", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "ItBuyPri", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ItBuyMny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "ItBuyMny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans")});
            // 
            // sqlUpdateCommand3
            // 
            this.sqlUpdateCommand3.CommandText = resources.GetString("sqlUpdateCommand3.CommandText");
            this.sqlUpdateCommand3.Connection = this.cn1;
            this.sqlUpdateCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@InNo", System.Data.SqlDbType.NVarChar, 0, "InNo"),
            new System.Data.SqlClient.SqlParameter("@BomID", System.Data.SqlDbType.NVarChar, 0, "BomID"),
            new System.Data.SqlClient.SqlParameter("@BomRec", System.Data.SqlDbType.NVarChar, 0, "BomRec"),
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@itunit", System.Data.SqlDbType.NVarChar, 0, "itunit"),
            new System.Data.SqlClient.SqlParameter("@itqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpareprs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpareprs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(6)), ((byte)(0)), "itrec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "itprs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itnote", System.Data.SqlDbType.NVarChar, 0, "itnote"),
            new System.Data.SqlClient.SqlParameter("@ItSource", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ItBuyPri", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "ItBuyPri", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ItBuyMny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "ItBuyMny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans"),
            new System.Data.SqlClient.SqlParameter("@Original_Inbom", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Inbom", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_InNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "InNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_InNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "InNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_BomID", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BomID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BomRec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BomRec", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itunit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpareprs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpareprs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpareprs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpareprs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpkgqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpkgqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itrec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itrec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(6)), ((byte)(0)), "itrec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "itprs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itnote", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itnote", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itnote", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itnote", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItSource", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItSource", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItBuyPri", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItBuyPri", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItBuyPri", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "ItBuyPri", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItBuyMny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItBuyMny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItBuyMny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "ItBuyMny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Inbom", System.Data.SqlDbType.Int, 4, "Inbom")});
            // 
            // sqlDeleteCommand3
            // 
            this.sqlDeleteCommand3.CommandText = resources.GetString("sqlDeleteCommand3.CommandText");
            this.sqlDeleteCommand3.Connection = this.cn1;
            this.sqlDeleteCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_Inbom", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Inbom", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_InNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "InNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_InNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "InNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_BomID", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BomID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BomRec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BomRec", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itunit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpareprs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpareprs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpareprs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpareprs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpkgqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpkgqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itrec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itrec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(6)), ((byte)(0)), "itrec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "itprs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itnote", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itnote", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itnote", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itnote", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItSource", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItSource", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItBuyPri", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItBuyPri", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItBuyPri", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "ItBuyPri", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItBuyMny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItBuyMny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItBuyMny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "ItBuyMny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null)});
            // 
            // da3
            // 
            this.da3.DeleteCommand = this.sqlDeleteCommand3;
            this.da3.InsertCommand = this.sqlInsertCommand3;
            this.da3.SelectCommand = this.sqlSelectCommand3;
            this.da3.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "InStkBOM", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Inbom", "Inbom"),
                        new System.Data.Common.DataColumnMapping("InNo", "InNo"),
                        new System.Data.Common.DataColumnMapping("BomID", "BomID"),
                        new System.Data.Common.DataColumnMapping("BomRec", "BomRec"),
                        new System.Data.Common.DataColumnMapping("itno", "itno"),
                        new System.Data.Common.DataColumnMapping("itname", "itname"),
                        new System.Data.Common.DataColumnMapping("itunit", "itunit"),
                        new System.Data.Common.DataColumnMapping("itqty", "itqty"),
                        new System.Data.Common.DataColumnMapping("itpareprs", "itpareprs"),
                        new System.Data.Common.DataColumnMapping("itpkgqty", "itpkgqty"),
                        new System.Data.Common.DataColumnMapping("itrec", "itrec"),
                        new System.Data.Common.DataColumnMapping("itprice", "itprice"),
                        new System.Data.Common.DataColumnMapping("itprs", "itprs"),
                        new System.Data.Common.DataColumnMapping("itmny", "itmny"),
                        new System.Data.Common.DataColumnMapping("itnote", "itnote"),
                        new System.Data.Common.DataColumnMapping("ItSource", "ItSource"),
                        new System.Data.Common.DataColumnMapping("ItBuyPri", "ItBuyPri"),
                        new System.Data.Common.DataColumnMapping("ItBuyMny", "ItBuyMny"),
                        new System.Data.Common.DataColumnMapping("IsTrans", "IsTrans")})});
            this.da3.UpdateCommand = this.sqlUpdateCommand3;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(12, 14);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "銷貨單號";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(12, 47);
            this.lblT3.Margin = new System.Windows.Forms.Padding(15);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "客戶編號";
            // 
            // textBoxT1
            // 
            this.textBoxT1.AllowGrayBackColor = true;
            this.textBoxT1.AllowResize = true;
            this.textBoxT1.BackColor = System.Drawing.Color.Silver;
            this.textBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT1.Location = new System.Drawing.Point(89, 9);
            this.textBoxT1.MaxLength = 14;
            this.textBoxT1.Name = "textBoxT1";
            this.textBoxT1.oLen = 0;
            this.textBoxT1.ReadOnly = true;
            this.textBoxT1.Size = new System.Drawing.Size(119, 27);
            this.textBoxT1.TabIndex = 0;
            this.textBoxT1.TabStop = false;
            // 
            // textBoxT3
            // 
            this.textBoxT3.AllowGrayBackColor = true;
            this.textBoxT3.AllowResize = true;
            this.textBoxT3.BackColor = System.Drawing.Color.Silver;
            this.textBoxT3.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT3.Location = new System.Drawing.Point(89, 42);
            this.textBoxT3.MaxLength = 10;
            this.textBoxT3.Name = "textBoxT3";
            this.textBoxT3.oLen = 0;
            this.textBoxT3.ReadOnly = true;
            this.textBoxT3.Size = new System.Drawing.Size(87, 27);
            this.textBoxT3.TabIndex = 0;
            this.textBoxT3.TabStop = false;
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(236, 14);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "銷貨日期";
            // 
            // Sadate
            // 
            this.Sadate.AllowGrayBackColor = true;
            this.Sadate.AllowResize = true;
            this.Sadate.BackColor = System.Drawing.Color.Silver;
            this.Sadate.Font = new System.Drawing.Font("細明體", 12F);
            this.Sadate.Location = new System.Drawing.Point(316, 9);
            this.Sadate.MaxLength = 10;
            this.Sadate.Name = "Sadate";
            this.Sadate.oLen = 0;
            this.Sadate.ReadOnly = true;
            this.Sadate.Size = new System.Drawing.Size(87, 27);
            this.Sadate.TabIndex = 0;
            this.Sadate.TabStop = false;
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(236, 47);
            this.lblT4.Margin = new System.Windows.Forms.Padding(15);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "客戶簡稱";
            // 
            // textBoxT4
            // 
            this.textBoxT4.AllowGrayBackColor = true;
            this.textBoxT4.AllowResize = true;
            this.textBoxT4.BackColor = System.Drawing.Color.Silver;
            this.textBoxT4.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT4.Location = new System.Drawing.Point(316, 42);
            this.textBoxT4.MaxLength = 10;
            this.textBoxT4.Name = "textBoxT4";
            this.textBoxT4.oLen = 0;
            this.textBoxT4.ReadOnly = true;
            this.textBoxT4.Size = new System.Drawing.Size(87, 27);
            this.textBoxT4.TabIndex = 0;
            this.textBoxT4.TabStop = false;
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(431, 47);
            this.lblT5.Margin = new System.Windows.Forms.Padding(15);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(56, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "連絡人";
            // 
            // textBoxT5
            // 
            this.textBoxT5.AllowGrayBackColor = true;
            this.textBoxT5.AllowResize = true;
            this.textBoxT5.BackColor = System.Drawing.Color.Silver;
            this.textBoxT5.Font = new System.Drawing.Font("細明體", 12F);
            this.textBoxT5.Location = new System.Drawing.Point(495, 42);
            this.textBoxT5.MaxLength = 10;
            this.textBoxT5.Name = "textBoxT5";
            this.textBoxT5.oLen = 0;
            this.textBoxT5.ReadOnly = true;
            this.textBoxT5.Size = new System.Drawing.Size(87, 27);
            this.textBoxT5.TabIndex = 0;
            this.textBoxT5.TabStop = false;
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
            this.序號,
            this.產品編號,
            this.品名規格,
            this.寄庫數量,
            this.銷貨數量,
            this.單位,
            this.倉庫編號,
            this.倉庫名稱,
            this.包裝數量,
            this.ItTrait,
            this.產品組成,
            this.未領數量,
            this.備註說明});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(1, 74);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1009, 465);
            this.dataGridViewT1.TabIndex = 2;
            this.dataGridViewT1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewT1_CellValidating);
            this.dataGridViewT1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewT1_RowsAdded);
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
            // 產品編號
            // 
            this.產品編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品編號.DataPropertyName = "itno";
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
            this.品名規格.DataPropertyName = "itname";
            this.品名規格.HeaderText = "品名規格";
            this.品名規格.MaxInputLength = 30;
            this.品名規格.Name = "品名規格";
            this.品名規格.ReadOnly = true;
            this.品名規格.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.品名規格.Width = 253;
            // 
            // 寄庫數量
            // 
            this.寄庫數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.寄庫數量.DataPropertyName = "inqty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.寄庫數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.寄庫數量.FirstNum = 0;
            this.寄庫數量.HeaderText = "寄庫數量";
            this.寄庫數量.LastNum = 0;
            this.寄庫數量.MarkThousand = false;
            this.寄庫數量.MaxInputLength = 16;
            this.寄庫數量.Name = "寄庫數量";
            this.寄庫數量.NullInput = false;
            this.寄庫數量.NullValue = "0";
            this.寄庫數量.ReadOnly = true;
            this.寄庫數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.寄庫數量.Width = 141;
            // 
            // 銷貨數量
            // 
            this.銷貨數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.銷貨數量.DataPropertyName = "qty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.銷貨數量.DefaultCellStyle = dataGridViewCellStyle3;
            this.銷貨數量.FirstNum = 0;
            this.銷貨數量.HeaderText = "銷貨數量";
            this.銷貨數量.LastNum = 0;
            this.銷貨數量.MarkThousand = false;
            this.銷貨數量.MaxInputLength = 16;
            this.銷貨數量.Name = "銷貨數量";
            this.銷貨數量.NullInput = false;
            this.銷貨數量.NullValue = "0";
            this.銷貨數量.ReadOnly = true;
            this.銷貨數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.銷貨數量.Width = 141;
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
            // 倉庫編號
            // 
            this.倉庫編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.倉庫編號.DataPropertyName = "stno";
            this.倉庫編號.HeaderText = "倉庫編號";
            this.倉庫編號.MaxInputLength = 10;
            this.倉庫編號.Name = "倉庫編號";
            this.倉庫編號.ReadOnly = true;
            this.倉庫編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.倉庫編號.Width = 93;
            // 
            // 倉庫名稱
            // 
            this.倉庫名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.倉庫名稱.DataPropertyName = "stname";
            this.倉庫名稱.HeaderText = "倉庫名稱";
            this.倉庫名稱.MaxInputLength = 10;
            this.倉庫名稱.Name = "倉庫名稱";
            this.倉庫名稱.ReadOnly = true;
            this.倉庫名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.倉庫名稱.Width = 93;
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
            this.包裝數量.MaxInputLength = 16;
            this.包裝數量.Name = "包裝數量";
            this.包裝數量.NullInput = false;
            this.包裝數量.NullValue = "0";
            this.包裝數量.ReadOnly = true;
            this.包裝數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝數量.Width = 141;
            // 
            // ItTrait
            // 
            this.ItTrait.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ItTrait.DataPropertyName = "ItTrait";
            this.ItTrait.HeaderText = "ItTrait";
            this.ItTrait.MaxInputLength = 10;
            this.ItTrait.Name = "ItTrait";
            this.ItTrait.ReadOnly = true;
            this.ItTrait.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ItTrait.Visible = false;
            this.ItTrait.Width = 93;
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
            // 未領數量
            // 
            this.未領數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.未領數量.DataPropertyName = "nonqty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.未領數量.DefaultCellStyle = dataGridViewCellStyle5;
            this.未領數量.FirstNum = 0;
            this.未領數量.HeaderText = "未領數量";
            this.未領數量.LastNum = 0;
            this.未領數量.MarkThousand = false;
            this.未領數量.MaxInputLength = 16;
            this.未領數量.Name = "未領數量";
            this.未領數量.NullInput = false;
            this.未領數量.NullValue = "0";
            this.未領數量.ReadOnly = true;
            this.未領數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.未領數量.Width = 141;
            // 
            // 備註說明
            // 
            this.備註說明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.備註說明.DataPropertyName = "memo";
            this.備註說明.HeaderText = "備註說明";
            this.備註說明.MaxInputLength = 20;
            this.備註說明.Name = "備註說明";
            this.備註說明.ReadOnly = true;
            this.備註說明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.備註說明.Width = 173;
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnDelete);
            this.panelT1.Location = new System.Drawing.Point(397, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(217, 79);
            this.panelT1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(138, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 79);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.UseDefaultSettings = true;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(69, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 79);
            this.btnSave.TabIndex = 2;
            this.btnSave.UseDefaultSettings = true;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 79);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.UseDefaultSettings = true;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // sqlSelectCommand4
            // 
            this.sqlSelectCommand4.CommandText = "select *,IsTrans=\'\' \r\n from stock";
            this.sqlSelectCommand4.Connection = this.cn1;
            // 
            // sqlInsertCommand4
            // 
            this.sqlInsertCommand4.CommandText = resources.GetString("sqlInsertCommand4.CommandText");
            this.sqlInsertCommand4.Connection = this.cn1;
            this.sqlInsertCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@stno", System.Data.SqlDbType.NVarChar, 0, "stno"),
            new System.Data.SqlClient.SqlParameter("@stname", System.Data.SqlDbType.NVarChar, 0, "stname"),
            new System.Data.SqlClient.SqlParameter("@sttrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@unit", System.Data.SqlDbType.NVarChar, 0, "unit"),
            new System.Data.SqlClient.SqlParameter("@itqtyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itqtyf", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itqty", System.Data.DataRowVersion.Current, null)});
            // 
            // sqlUpdateCommand4
            // 
            this.sqlUpdateCommand4.CommandText = resources.GetString("sqlUpdateCommand4.CommandText");
            this.sqlUpdateCommand4.Connection = this.cn1;
            this.sqlUpdateCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@stno", System.Data.SqlDbType.NVarChar, 0, "stno"),
            new System.Data.SqlClient.SqlParameter("@stname", System.Data.SqlDbType.NVarChar, 0, "stname"),
            new System.Data.SqlClient.SqlParameter("@sttrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@unit", System.Data.SqlDbType.NVarChar, 0, "unit"),
            new System.Data.SqlClient.SqlParameter("@itqtyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itqtyf", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Original_stno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sttrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sttrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_itno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_unit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "unit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_unit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "unit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itqtyf", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itqtyf", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itqtyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itqtyf", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itqty", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand4
            // 
            this.sqlDeleteCommand4.CommandText = resources.GetString("sqlDeleteCommand4.CommandText");
            this.sqlDeleteCommand4.Connection = this.cn1;
            this.sqlDeleteCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_stno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sttrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sttrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "sttrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_itno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_unit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "unit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_unit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "unit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itqtyf", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itqtyf", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itqtyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itqtyf", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itqty", System.Data.DataRowVersion.Original, null)});
            // 
            // da4
            // 
            this.da4.DeleteCommand = this.sqlDeleteCommand4;
            this.da4.InsertCommand = this.sqlInsertCommand4;
            this.da4.SelectCommand = this.sqlSelectCommand4;
            this.da4.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "stock", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("stno", "stno"),
                        new System.Data.Common.DataColumnMapping("stname", "stname"),
                        new System.Data.Common.DataColumnMapping("sttrait", "sttrait"),
                        new System.Data.Common.DataColumnMapping("itno", "itno"),
                        new System.Data.Common.DataColumnMapping("itname", "itname"),
                        new System.Data.Common.DataColumnMapping("unit", "unit"),
                        new System.Data.Common.DataColumnMapping("itqtyf", "itqtyf"),
                        new System.Data.Common.DataColumnMapping("itqty", "itqty"),
                        new System.Data.Common.DataColumnMapping("IsTrans", "IsTrans")})});
            this.da4.UpdateCommand = this.sqlUpdateCommand4;
            // 
            // sqlSelectCommand5
            // 
            this.sqlSelectCommand5.CommandText = "select * from item";
            this.sqlSelectCommand5.Connection = this.cn1;
            // 
            // sqlInsertCommand5
            // 
            this.sqlInsertCommand5.CommandText = resources.GetString("sqlInsertCommand5.CommandText");
            this.sqlInsertCommand5.Connection = this.cn1;
            this.sqlInsertCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itnoudf", System.Data.SqlDbType.NVarChar, 0, "itnoudf"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@kino", System.Data.SqlDbType.NVarChar, 0, "kino"),
            new System.Data.SqlClient.SqlParameter("@itime", System.Data.SqlDbType.NVarChar, 0, "itime"),
            new System.Data.SqlClient.SqlParameter("@ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itunit", System.Data.SqlDbType.NVarChar, 0, "itunit"),
            new System.Data.SqlClient.SqlParameter("@itunitp", System.Data.SqlDbType.NVarChar, 0, "itunitp"),
            new System.Data.SqlClient.SqlParameter("@itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itbuypri", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itbuypri", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice2", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice3", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice4", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice5", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice5", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itcost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itcost", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itbuyprip", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itbuyprip", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep2", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep3", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep4", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep5", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep5", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itcostp", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itcostp", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itbuyunit", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itbuyunit", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itsalunit", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itsalunit", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itsafeqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itsafeqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itlastqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itlastqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itnw", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itnw", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itnwunit", System.Data.SqlDbType.NVarChar, 0, "itnwunit"),
            new System.Data.SqlClient.SqlParameter("@itcostslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcostslt", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itcodeslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcodeslt", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itcodeno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcodeno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itdesp1", System.Data.SqlDbType.NVarChar, 0, "itdesp1"),
            new System.Data.SqlClient.SqlParameter("@itdesp2", System.Data.SqlDbType.NVarChar, 0, "itdesp2"),
            new System.Data.SqlClient.SqlParameter("@itdesp3", System.Data.SqlDbType.NVarChar, 0, "itdesp3"),
            new System.Data.SqlClient.SqlParameter("@itdesp4", System.Data.SqlDbType.NVarChar, 0, "itdesp4"),
            new System.Data.SqlClient.SqlParameter("@itdesp5", System.Data.SqlDbType.NVarChar, 0, "itdesp5"),
            new System.Data.SqlClient.SqlParameter("@itdesp6", System.Data.SqlDbType.NVarChar, 0, "itdesp6"),
            new System.Data.SqlClient.SqlParameter("@itdesp7", System.Data.SqlDbType.NVarChar, 0, "itdesp7"),
            new System.Data.SqlClient.SqlParameter("@itdesp8", System.Data.SqlDbType.NVarChar, 0, "itdesp8"),
            new System.Data.SqlClient.SqlParameter("@itdesp9", System.Data.SqlDbType.NVarChar, 0, "itdesp9"),
            new System.Data.SqlClient.SqlParameter("@itdesp10", System.Data.SqlDbType.NVarChar, 0, "itdesp10"),
            new System.Data.SqlClient.SqlParameter("@itdate", System.Data.SqlDbType.NVarChar, 0, "itdate"),
            new System.Data.SqlClient.SqlParameter("@itdate1", System.Data.SqlDbType.NVarChar, 0, "itdate1"),
            new System.Data.SqlClient.SqlParameter("@itdate2", System.Data.SqlDbType.NVarChar, 0, "itdate2"),
            new System.Data.SqlClient.SqlParameter("@itbuydate", System.Data.SqlDbType.NVarChar, 0, "itbuydate"),
            new System.Data.SqlClient.SqlParameter("@itbuydate1", System.Data.SqlDbType.NVarChar, 0, "itbuydate1"),
            new System.Data.SqlClient.SqlParameter("@itbuydate2", System.Data.SqlDbType.NVarChar, 0, "itbuydate2"),
            new System.Data.SqlClient.SqlParameter("@itsaldate", System.Data.SqlDbType.NVarChar, 0, "itsaldate"),
            new System.Data.SqlClient.SqlParameter("@itsaldate1", System.Data.SqlDbType.NVarChar, 0, "itsaldate1"),
            new System.Data.SqlClient.SqlParameter("@itSaldate2", System.Data.SqlDbType.NVarChar, 0, "itSaldate2"),
            new System.Data.SqlClient.SqlParameter("@itfircost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itfircost", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itfirtqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itfirtqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itfirtcost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itfirtcost", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itstockqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itstockqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itnote", System.Data.SqlDbType.NVarChar, 0, "itnote"),
            new System.Data.SqlClient.SqlParameter("@itudf1", System.Data.SqlDbType.NVarChar, 0, "itudf1"),
            new System.Data.SqlClient.SqlParameter("@itudf2", System.Data.SqlDbType.NVarChar, 0, "itudf2"),
            new System.Data.SqlClient.SqlParameter("@itudf3", System.Data.SqlDbType.NVarChar, 0, "itudf3"),
            new System.Data.SqlClient.SqlParameter("@itudf4", System.Data.SqlDbType.NVarChar, 0, "itudf4"),
            new System.Data.SqlClient.SqlParameter("@itudf5", System.Data.SqlDbType.NVarChar, 0, "itudf5"),
            new System.Data.SqlClient.SqlParameter("@itweblist", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itweblist", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itwebpic", System.Data.SqlDbType.NVarChar, 0, "itwebpic"),
            new System.Data.SqlClient.SqlParameter("@itwebctl1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itwebctl1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itwebctl2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itwebctl2", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@IsUse", System.Data.SqlDbType.Bit, 0, "IsUse"),
            new System.Data.SqlClient.SqlParameter("@ItSource", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@pic", System.Data.SqlDbType.Image, 0, "pic"),
            new System.Data.SqlClient.SqlParameter("@itpicture", System.Data.SqlDbType.NVarChar, 0, "itpicture"),
            new System.Data.SqlClient.SqlParameter("@IsEnable", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "IsEnable", System.Data.DataRowVersion.Current, null)});
            // 
            // sqlUpdateCommand5
            // 
            this.sqlUpdateCommand5.CommandText = resources.GetString("sqlUpdateCommand5.CommandText");
            this.sqlUpdateCommand5.Connection = this.cn1;
            this.sqlUpdateCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itnoudf", System.Data.SqlDbType.NVarChar, 0, "itnoudf"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@kino", System.Data.SqlDbType.NVarChar, 0, "kino"),
            new System.Data.SqlClient.SqlParameter("@itime", System.Data.SqlDbType.NVarChar, 0, "itime"),
            new System.Data.SqlClient.SqlParameter("@ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itunit", System.Data.SqlDbType.NVarChar, 0, "itunit"),
            new System.Data.SqlClient.SqlParameter("@itunitp", System.Data.SqlDbType.NVarChar, 0, "itunitp"),
            new System.Data.SqlClient.SqlParameter("@itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itbuypri", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itbuypri", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice2", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice3", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice4", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itprice5", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice5", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itcost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itcost", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itbuyprip", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itbuyprip", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep2", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep3", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep4", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itpricep5", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep5", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itcostp", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itcostp", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itbuyunit", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itbuyunit", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itsalunit", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itsalunit", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itsafeqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itsafeqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itlastqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itlastqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itnw", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itnw", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itnwunit", System.Data.SqlDbType.NVarChar, 0, "itnwunit"),
            new System.Data.SqlClient.SqlParameter("@itcostslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcostslt", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itcodeslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcodeslt", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itcodeno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcodeno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itdesp1", System.Data.SqlDbType.NVarChar, 0, "itdesp1"),
            new System.Data.SqlClient.SqlParameter("@itdesp2", System.Data.SqlDbType.NVarChar, 0, "itdesp2"),
            new System.Data.SqlClient.SqlParameter("@itdesp3", System.Data.SqlDbType.NVarChar, 0, "itdesp3"),
            new System.Data.SqlClient.SqlParameter("@itdesp4", System.Data.SqlDbType.NVarChar, 0, "itdesp4"),
            new System.Data.SqlClient.SqlParameter("@itdesp5", System.Data.SqlDbType.NVarChar, 0, "itdesp5"),
            new System.Data.SqlClient.SqlParameter("@itdesp6", System.Data.SqlDbType.NVarChar, 0, "itdesp6"),
            new System.Data.SqlClient.SqlParameter("@itdesp7", System.Data.SqlDbType.NVarChar, 0, "itdesp7"),
            new System.Data.SqlClient.SqlParameter("@itdesp8", System.Data.SqlDbType.NVarChar, 0, "itdesp8"),
            new System.Data.SqlClient.SqlParameter("@itdesp9", System.Data.SqlDbType.NVarChar, 0, "itdesp9"),
            new System.Data.SqlClient.SqlParameter("@itdesp10", System.Data.SqlDbType.NVarChar, 0, "itdesp10"),
            new System.Data.SqlClient.SqlParameter("@itdate", System.Data.SqlDbType.NVarChar, 0, "itdate"),
            new System.Data.SqlClient.SqlParameter("@itdate1", System.Data.SqlDbType.NVarChar, 0, "itdate1"),
            new System.Data.SqlClient.SqlParameter("@itdate2", System.Data.SqlDbType.NVarChar, 0, "itdate2"),
            new System.Data.SqlClient.SqlParameter("@itbuydate", System.Data.SqlDbType.NVarChar, 0, "itbuydate"),
            new System.Data.SqlClient.SqlParameter("@itbuydate1", System.Data.SqlDbType.NVarChar, 0, "itbuydate1"),
            new System.Data.SqlClient.SqlParameter("@itbuydate2", System.Data.SqlDbType.NVarChar, 0, "itbuydate2"),
            new System.Data.SqlClient.SqlParameter("@itsaldate", System.Data.SqlDbType.NVarChar, 0, "itsaldate"),
            new System.Data.SqlClient.SqlParameter("@itsaldate1", System.Data.SqlDbType.NVarChar, 0, "itsaldate1"),
            new System.Data.SqlClient.SqlParameter("@itSaldate2", System.Data.SqlDbType.NVarChar, 0, "itSaldate2"),
            new System.Data.SqlClient.SqlParameter("@itfircost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itfircost", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itfirtqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itfirtqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itfirtcost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itfirtcost", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itstockqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itstockqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itnote", System.Data.SqlDbType.NVarChar, 0, "itnote"),
            new System.Data.SqlClient.SqlParameter("@itudf1", System.Data.SqlDbType.NVarChar, 0, "itudf1"),
            new System.Data.SqlClient.SqlParameter("@itudf2", System.Data.SqlDbType.NVarChar, 0, "itudf2"),
            new System.Data.SqlClient.SqlParameter("@itudf3", System.Data.SqlDbType.NVarChar, 0, "itudf3"),
            new System.Data.SqlClient.SqlParameter("@itudf4", System.Data.SqlDbType.NVarChar, 0, "itudf4"),
            new System.Data.SqlClient.SqlParameter("@itudf5", System.Data.SqlDbType.NVarChar, 0, "itudf5"),
            new System.Data.SqlClient.SqlParameter("@itweblist", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itweblist", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itwebpic", System.Data.SqlDbType.NVarChar, 0, "itwebpic"),
            new System.Data.SqlClient.SqlParameter("@itwebctl1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itwebctl1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itwebctl2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itwebctl2", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@IsUse", System.Data.SqlDbType.Bit, 0, "IsUse"),
            new System.Data.SqlClient.SqlParameter("@ItSource", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@pic", System.Data.SqlDbType.Image, 0, "pic"),
            new System.Data.SqlClient.SqlParameter("@itpicture", System.Data.SqlDbType.NVarChar, 0, "itpicture"),
            new System.Data.SqlClient.SqlParameter("@IsEnable", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "IsEnable", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Original_itno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itnoudf", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itnoudf", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itnoudf", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itnoudf", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_kino", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "kino", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_kino", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "kino", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itime", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itime", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itime", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itime", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ittrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itunit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itunitp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itunitp", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itunitp", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itunitp", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpkgqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpkgqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuypri", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuypri", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuypri", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itbuypri", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice5", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcost", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcost", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itcost", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuyprip", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuyprip", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuyprip", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itbuyprip", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep5", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcostp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcostp", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcostp", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itcostp", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuyunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuyunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuyunit", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itbuyunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itsalunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itsalunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itsalunit", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itsalunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itsafeqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itsafeqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itsafeqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itsafeqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itlastqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itlastqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itlastqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itlastqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itnw", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itnw", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itnw", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itnw", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itnwunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itnwunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itnwunit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itnwunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcostslt", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcostslt", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcostslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcostslt", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcodeslt", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcodeslt", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcodeslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcodeslt", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcodeno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcodeno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcodeno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcodeno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp3", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp4", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp5", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp6", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp6", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp6", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp6", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp7", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp7", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp7", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp7", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp8", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp8", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp8", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp8", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp9", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp9", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp9", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp9", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp10", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp10", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp10", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp10", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuydate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuydate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuydate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itbuydate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuydate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuydate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuydate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itbuydate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuydate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuydate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuydate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itbuydate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itsaldate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itsaldate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itsaldate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itsaldate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itsaldate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itsaldate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itsaldate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itsaldate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itSaldate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itSaldate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itSaldate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itSaldate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itfircost", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itfircost", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itfircost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itfircost", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itfirtqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itfirtqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itfirtqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itfirtqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itfirtcost", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itfirtcost", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itfirtcost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itfirtcost", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itstockqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itstockqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itstockqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itstockqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf3", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf4", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf5", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itweblist", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itweblist", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itweblist", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itweblist", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itwebpic", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itwebpic", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itwebpic", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itwebpic", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itwebctl1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itwebctl1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itwebctl1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itwebctl1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itwebctl2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itwebctl2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itwebctl2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itwebctl2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsUse", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsUse", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsUse", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsUse", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItSource", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItSource", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpicture", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpicture", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpicture", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itpicture", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsEnable", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsEnable", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsEnable", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "IsEnable", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand5
            // 
            this.sqlDeleteCommand5.CommandText = resources.GetString("sqlDeleteCommand5.CommandText");
            this.sqlDeleteCommand5.Connection = this.cn1;
            this.sqlDeleteCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_itno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itnoudf", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itnoudf", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itnoudf", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itnoudf", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_kino", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "kino", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_kino", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "kino", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itime", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itime", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itime", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itime", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ittrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itunit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itunitp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itunitp", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itunitp", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itunitp", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpkgqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpkgqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuypri", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuypri", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuypri", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itbuypri", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itprice5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itprice5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itprice5", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itprice5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcost", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcost", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itcost", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuyprip", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuyprip", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuyprip", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itbuyprip", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpricep5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpricep5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpricep5", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itpricep5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcostp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcostp", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcostp", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itcostp", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuyunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuyunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuyunit", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itbuyunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itsalunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itsalunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itsalunit", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itsalunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itsafeqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itsafeqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itsafeqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itsafeqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itlastqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itlastqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itlastqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itlastqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itnw", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itnw", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itnw", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itnw", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itnwunit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itnwunit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itnwunit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itnwunit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcostslt", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcostslt", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcostslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcostslt", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcodeslt", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcodeslt", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcodeslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcodeslt", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itcodeno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itcodeno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itcodeno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itcodeno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp3", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp4", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp5", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp6", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp6", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp6", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp6", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp7", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp7", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp7", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp7", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp8", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp8", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp8", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp8", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp9", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp9", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp9", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp9", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdesp10", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdesp10", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdesp10", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdesp10", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itdate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itdate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itdate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itdate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuydate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuydate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuydate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itbuydate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuydate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuydate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuydate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itbuydate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itbuydate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itbuydate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itbuydate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itbuydate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itsaldate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itsaldate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itsaldate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itsaldate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itsaldate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itsaldate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itsaldate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itsaldate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itSaldate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itSaldate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itSaldate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itSaldate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itfircost", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itfircost", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itfircost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itfircost", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itfirtqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itfirtqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itfirtqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itfirtqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itfirtcost", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itfirtcost", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itfirtcost", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(6)), "itfirtcost", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itstockqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itstockqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itstockqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(20)), ((byte)(4)), "itstockqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf3", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf4", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itudf5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itudf5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itudf5", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itudf5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itweblist", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itweblist", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itweblist", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itweblist", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itwebpic", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itwebpic", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itwebpic", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itwebpic", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itwebctl1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itwebctl1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itwebctl1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itwebctl1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itwebctl2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itwebctl2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itwebctl2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "itwebctl2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsUse", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsUse", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsUse", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsUse", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItSource", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItSource", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItSource", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_itpicture", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "itpicture", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_itpicture", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "itpicture", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsEnable", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsEnable", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsEnable", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "IsEnable", System.Data.DataRowVersion.Original, null)});
            // 
            // da5
            // 
            this.da5.DeleteCommand = this.sqlDeleteCommand5;
            this.da5.InsertCommand = this.sqlInsertCommand5;
            this.da5.SelectCommand = this.sqlSelectCommand5;
            this.da5.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "item", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("itno", "itno"),
                        new System.Data.Common.DataColumnMapping("itnoudf", "itnoudf"),
                        new System.Data.Common.DataColumnMapping("itname", "itname"),
                        new System.Data.Common.DataColumnMapping("kino", "kino"),
                        new System.Data.Common.DataColumnMapping("itime", "itime"),
                        new System.Data.Common.DataColumnMapping("ittrait", "ittrait"),
                        new System.Data.Common.DataColumnMapping("itunit", "itunit"),
                        new System.Data.Common.DataColumnMapping("itunitp", "itunitp"),
                        new System.Data.Common.DataColumnMapping("itpkgqty", "itpkgqty"),
                        new System.Data.Common.DataColumnMapping("itbuypri", "itbuypri"),
                        new System.Data.Common.DataColumnMapping("itprice", "itprice"),
                        new System.Data.Common.DataColumnMapping("itprice1", "itprice1"),
                        new System.Data.Common.DataColumnMapping("itprice2", "itprice2"),
                        new System.Data.Common.DataColumnMapping("itprice3", "itprice3"),
                        new System.Data.Common.DataColumnMapping("itprice4", "itprice4"),
                        new System.Data.Common.DataColumnMapping("itprice5", "itprice5"),
                        new System.Data.Common.DataColumnMapping("itcost", "itcost"),
                        new System.Data.Common.DataColumnMapping("itbuyprip", "itbuyprip"),
                        new System.Data.Common.DataColumnMapping("itpricep", "itpricep"),
                        new System.Data.Common.DataColumnMapping("itpricep1", "itpricep1"),
                        new System.Data.Common.DataColumnMapping("itpricep2", "itpricep2"),
                        new System.Data.Common.DataColumnMapping("itpricep3", "itpricep3"),
                        new System.Data.Common.DataColumnMapping("itpricep4", "itpricep4"),
                        new System.Data.Common.DataColumnMapping("itpricep5", "itpricep5"),
                        new System.Data.Common.DataColumnMapping("itcostp", "itcostp"),
                        new System.Data.Common.DataColumnMapping("itbuyunit", "itbuyunit"),
                        new System.Data.Common.DataColumnMapping("itsalunit", "itsalunit"),
                        new System.Data.Common.DataColumnMapping("itsafeqty", "itsafeqty"),
                        new System.Data.Common.DataColumnMapping("itlastqty", "itlastqty"),
                        new System.Data.Common.DataColumnMapping("itnw", "itnw"),
                        new System.Data.Common.DataColumnMapping("itnwunit", "itnwunit"),
                        new System.Data.Common.DataColumnMapping("itcostslt", "itcostslt"),
                        new System.Data.Common.DataColumnMapping("itcodeslt", "itcodeslt"),
                        new System.Data.Common.DataColumnMapping("itcodeno", "itcodeno"),
                        new System.Data.Common.DataColumnMapping("itdesp1", "itdesp1"),
                        new System.Data.Common.DataColumnMapping("itdesp2", "itdesp2"),
                        new System.Data.Common.DataColumnMapping("itdesp3", "itdesp3"),
                        new System.Data.Common.DataColumnMapping("itdesp4", "itdesp4"),
                        new System.Data.Common.DataColumnMapping("itdesp5", "itdesp5"),
                        new System.Data.Common.DataColumnMapping("itdesp6", "itdesp6"),
                        new System.Data.Common.DataColumnMapping("itdesp7", "itdesp7"),
                        new System.Data.Common.DataColumnMapping("itdesp8", "itdesp8"),
                        new System.Data.Common.DataColumnMapping("itdesp9", "itdesp9"),
                        new System.Data.Common.DataColumnMapping("itdesp10", "itdesp10"),
                        new System.Data.Common.DataColumnMapping("itdate", "itdate"),
                        new System.Data.Common.DataColumnMapping("itdate1", "itdate1"),
                        new System.Data.Common.DataColumnMapping("itdate2", "itdate2"),
                        new System.Data.Common.DataColumnMapping("itbuydate", "itbuydate"),
                        new System.Data.Common.DataColumnMapping("itbuydate1", "itbuydate1"),
                        new System.Data.Common.DataColumnMapping("itbuydate2", "itbuydate2"),
                        new System.Data.Common.DataColumnMapping("itsaldate", "itsaldate"),
                        new System.Data.Common.DataColumnMapping("itsaldate1", "itsaldate1"),
                        new System.Data.Common.DataColumnMapping("itSaldate2", "itSaldate2"),
                        new System.Data.Common.DataColumnMapping("itfircost", "itfircost"),
                        new System.Data.Common.DataColumnMapping("itfirtqty", "itfirtqty"),
                        new System.Data.Common.DataColumnMapping("itfirtcost", "itfirtcost"),
                        new System.Data.Common.DataColumnMapping("itstockqty", "itstockqty"),
                        new System.Data.Common.DataColumnMapping("itnote", "itnote"),
                        new System.Data.Common.DataColumnMapping("itudf1", "itudf1"),
                        new System.Data.Common.DataColumnMapping("itudf2", "itudf2"),
                        new System.Data.Common.DataColumnMapping("itudf3", "itudf3"),
                        new System.Data.Common.DataColumnMapping("itudf4", "itudf4"),
                        new System.Data.Common.DataColumnMapping("itudf5", "itudf5"),
                        new System.Data.Common.DataColumnMapping("itweblist", "itweblist"),
                        new System.Data.Common.DataColumnMapping("itwebpic", "itwebpic"),
                        new System.Data.Common.DataColumnMapping("itwebctl1", "itwebctl1"),
                        new System.Data.Common.DataColumnMapping("itwebctl2", "itwebctl2"),
                        new System.Data.Common.DataColumnMapping("IsUse", "IsUse"),
                        new System.Data.Common.DataColumnMapping("ItSource", "ItSource"),
                        new System.Data.Common.DataColumnMapping("pic", "pic"),
                        new System.Data.Common.DataColumnMapping("itpicture", "itpicture"),
                        new System.Data.Common.DataColumnMapping("IsEnable", "IsEnable")})});
            this.da5.UpdateCommand = this.sqlUpdateCommand5;
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
            // FrmInStkb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.textBoxT1);
            this.Controls.Add(this.textBoxT3);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.Sadate);
            this.Controls.Add(this.textBoxT5);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.textBoxT4);
            this.Name = "FrmInStkb";
            this.Text = "寄庫修改作業";
            this.Load += new System.EventHandler(this.FrmInStkb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection cn1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter da1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlDataAdapter da2;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand3;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand3;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand3;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand3;
        private System.Data.SqlClient.SqlDataAdapter da3;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnCancel;
        private JE.MyControl.ButtonT btnSave; 
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT textBoxT1;
        private JE.MyControl.TextBoxT textBoxT3;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT Sadate;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT textBoxT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.TextBoxT textBoxT5;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand4;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand4;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand4;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand4;
        private System.Data.SqlClient.SqlDataAdapter da4;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand5;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand5;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand5;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand5;
        private System.Data.SqlClient.SqlDataAdapter da5;
        private JE.MyControl.ButtonT btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private JE.MyControl.DataGridViewTextNumberT 寄庫數量;
        private JE.MyControl.DataGridViewTextNumberT 銷貨數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫名稱;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItTrait;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品組成;
        private JE.MyControl.DataGridViewTextNumberT 未領數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註說明;
        private JE.MyControl.StatusStripT statusStripT1; 
    }
}