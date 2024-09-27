namespace S_61.subMenuFm_3
{
    partial class FrmReceivdQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReceivdQuery));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daM = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daD = new System.Data.SqlClient.SqlDataAdapter();
            this.labelT1 = new JE.MyControl.LabelT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.qReno = new JE.MyControl.TextBoxT();
            this.qCuno = new JE.MyControl.TextBoxT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnCancel = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.收款憑證 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.收款日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.沖抵帳款 = new JE.MyControl.DataGridViewTextNumberT();
            this.累入預收 = new JE.MyControl.DataGridViewTextNumberT();
            this.dataGridViewT2 = new JE.MyControl.DataGridViewT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.btnQoo = new JE.MyControl.ButtonSmallT();
            this.點選 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.帳款日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單據 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單據號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.發票號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.幣別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.沖帳金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.折讓金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.未收金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.單據總計 = new JE.MyControl.DataGridViewTextNumberT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).BeginInit();
            this.SuspendLayout();
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "select \r\n*\r\nfrom receiv\r\norder by reno desc";
            this.sqlSelectCommand1.Connection = this.cn;
            // 
            // cn
            // 
            this.cn.ConnectionString = "Data Source=.;Initial Catalog=stock;Persist Security Info=True;User ID=BMIDP;Pass" +
    "word=BMIDP";
            this.cn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.cn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@reno", System.Data.SqlDbType.NVarChar, 0, "reno"),
            new System.Data.SqlClient.SqlParameter("@redate", System.Data.SqlDbType.NVarChar, 0, "redate"),
            new System.Data.SqlClient.SqlParameter("@redate1", System.Data.SqlDbType.NVarChar, 0, "redate1"),
            new System.Data.SqlClient.SqlParameter("@redate2", System.Data.SqlDbType.NVarChar, 0, "redate2"),
            new System.Data.SqlClient.SqlParameter("@redateacs", System.Data.SqlDbType.NVarChar, 0, "redateacs"),
            new System.Data.SqlClient.SqlParameter("@redateacs1", System.Data.SqlDbType.NVarChar, 0, "redateacs1"),
            new System.Data.SqlClient.SqlParameter("@redateacs2", System.Data.SqlDbType.NVarChar, 0, "redateacs2"),
            new System.Data.SqlClient.SqlParameter("@redateace", System.Data.SqlDbType.NVarChar, 0, "redateace"),
            new System.Data.SqlClient.SqlParameter("@redateace1", System.Data.SqlDbType.NVarChar, 0, "redateace1"),
            new System.Data.SqlClient.SqlParameter("@redateace2", System.Data.SqlDbType.NVarChar, 0, "redateace2"),
            new System.Data.SqlClient.SqlParameter("@cono", System.Data.SqlDbType.NVarChar, 0, "cono"),
            new System.Data.SqlClient.SqlParameter("@coname1", System.Data.SqlDbType.NVarChar, 0, "coname1"),
            new System.Data.SqlClient.SqlParameter("@coname2", System.Data.SqlDbType.NVarChar, 0, "coname2"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@cuname2", System.Data.SqlDbType.NVarChar, 0, "cuname2"),
            new System.Data.SqlClient.SqlParameter("@cuname1", System.Data.SqlDbType.NVarChar, 0, "cuname1"),
            new System.Data.SqlClient.SqlParameter("@cutel1", System.Data.SqlDbType.NVarChar, 0, "cutel1"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@emname", System.Data.SqlDbType.NVarChar, 0, "emname"),
            new System.Data.SqlClient.SqlParameter("@cashmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cashmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cardmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cardmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cardno", System.Data.SqlDbType.NVarChar, 0, "cardno"),
            new System.Data.SqlClient.SqlParameter("@ticket", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ticket", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@checkmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "checkmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@remitmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "remitmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@othermny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "othermny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@getprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "getprvacc", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@addprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "addprvacc", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@actslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "actslt", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totdisc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totdisc", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totreve", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totreve", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totexgdiff", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totexgdiff", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@memo1", System.Data.SqlDbType.NVarChar, 0, "memo1"),
            new System.Data.SqlClient.SqlParameter("@memo2", System.Data.SqlDbType.NVarChar, 0, "memo2"),
            new System.Data.SqlClient.SqlParameter("@acno", System.Data.SqlDbType.NVarChar, 0, "acno"),
            new System.Data.SqlClient.SqlParameter("@sano", System.Data.SqlDbType.NVarChar, 0, "sano"),
            new System.Data.SqlClient.SqlParameter("@bracket", System.Data.SqlDbType.NVarChar, 0, "bracket"),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cloflag", System.Data.SqlDbType.Bit, 0, "cloflag"),
            new System.Data.SqlClient.SqlParameter("@ExtFlag", System.Data.SqlDbType.NVarChar, 0, "ExtFlag"),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans"),
            new System.Data.SqlClient.SqlParameter("@spno", System.Data.SqlDbType.NVarChar, 0, "spno"),
            new System.Data.SqlClient.SqlParameter("@seno", System.Data.SqlDbType.NVarChar, 0, "seno"),
            new System.Data.SqlClient.SqlParameter("@DeNo", System.Data.SqlDbType.NVarChar, 0, "DeNo"),
            new System.Data.SqlClient.SqlParameter("@DeName", System.Data.SqlDbType.NVarChar, 0, "DeName"),
            new System.Data.SqlClient.SqlParameter("@accono", System.Data.SqlDbType.NVarChar, 0, "accono"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null)});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.cn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@reno", System.Data.SqlDbType.NVarChar, 0, "reno"),
            new System.Data.SqlClient.SqlParameter("@redate", System.Data.SqlDbType.NVarChar, 0, "redate"),
            new System.Data.SqlClient.SqlParameter("@redate1", System.Data.SqlDbType.NVarChar, 0, "redate1"),
            new System.Data.SqlClient.SqlParameter("@redate2", System.Data.SqlDbType.NVarChar, 0, "redate2"),
            new System.Data.SqlClient.SqlParameter("@redateacs", System.Data.SqlDbType.NVarChar, 0, "redateacs"),
            new System.Data.SqlClient.SqlParameter("@redateacs1", System.Data.SqlDbType.NVarChar, 0, "redateacs1"),
            new System.Data.SqlClient.SqlParameter("@redateacs2", System.Data.SqlDbType.NVarChar, 0, "redateacs2"),
            new System.Data.SqlClient.SqlParameter("@redateace", System.Data.SqlDbType.NVarChar, 0, "redateace"),
            new System.Data.SqlClient.SqlParameter("@redateace1", System.Data.SqlDbType.NVarChar, 0, "redateace1"),
            new System.Data.SqlClient.SqlParameter("@redateace2", System.Data.SqlDbType.NVarChar, 0, "redateace2"),
            new System.Data.SqlClient.SqlParameter("@cono", System.Data.SqlDbType.NVarChar, 0, "cono"),
            new System.Data.SqlClient.SqlParameter("@coname1", System.Data.SqlDbType.NVarChar, 0, "coname1"),
            new System.Data.SqlClient.SqlParameter("@coname2", System.Data.SqlDbType.NVarChar, 0, "coname2"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@cuname2", System.Data.SqlDbType.NVarChar, 0, "cuname2"),
            new System.Data.SqlClient.SqlParameter("@cuname1", System.Data.SqlDbType.NVarChar, 0, "cuname1"),
            new System.Data.SqlClient.SqlParameter("@cutel1", System.Data.SqlDbType.NVarChar, 0, "cutel1"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@emname", System.Data.SqlDbType.NVarChar, 0, "emname"),
            new System.Data.SqlClient.SqlParameter("@cashmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cashmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cardmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cardmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cardno", System.Data.SqlDbType.NVarChar, 0, "cardno"),
            new System.Data.SqlClient.SqlParameter("@ticket", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ticket", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@checkmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "checkmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@remitmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "remitmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@othermny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "othermny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@getprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "getprvacc", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@addprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "addprvacc", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@actslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "actslt", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totdisc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totdisc", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totreve", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totreve", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totexgdiff", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totexgdiff", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@memo1", System.Data.SqlDbType.NVarChar, 0, "memo1"),
            new System.Data.SqlClient.SqlParameter("@memo2", System.Data.SqlDbType.NVarChar, 0, "memo2"),
            new System.Data.SqlClient.SqlParameter("@acno", System.Data.SqlDbType.NVarChar, 0, "acno"),
            new System.Data.SqlClient.SqlParameter("@sano", System.Data.SqlDbType.NVarChar, 0, "sano"),
            new System.Data.SqlClient.SqlParameter("@bracket", System.Data.SqlDbType.NVarChar, 0, "bracket"),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@cloflag", System.Data.SqlDbType.Bit, 0, "cloflag"),
            new System.Data.SqlClient.SqlParameter("@ExtFlag", System.Data.SqlDbType.NVarChar, 0, "ExtFlag"),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans"),
            new System.Data.SqlClient.SqlParameter("@spno", System.Data.SqlDbType.NVarChar, 0, "spno"),
            new System.Data.SqlClient.SqlParameter("@seno", System.Data.SqlDbType.NVarChar, 0, "seno"),
            new System.Data.SqlClient.SqlParameter("@DeNo", System.Data.SqlDbType.NVarChar, 0, "DeNo"),
            new System.Data.SqlClient.SqlParameter("@DeName", System.Data.SqlDbType.NVarChar, 0, "DeName"),
            new System.Data.SqlClient.SqlParameter("@accono", System.Data.SqlDbType.NVarChar, 0, "accono"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Original_reno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "reno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace2", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cashmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cashmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cashmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cashmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cardmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cardmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cardmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cardmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cardno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cardno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cardno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cardno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ticket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ticket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ticket", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ticket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_checkmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "checkmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_checkmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "checkmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_remitmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "remitmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_remitmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "remitmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_othermny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "othermny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_othermny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "othermny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_getprvacc", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "getprvacc", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_getprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "getprvacc", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_addprvacc", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "addprvacc", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_addprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "addprvacc", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_actslt", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "actslt", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_actslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "actslt", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totdisc", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totdisc", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totdisc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totdisc", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totreve", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totreve", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totreve", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totreve", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totexgdiff", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totexgdiff", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totexgdiff", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totexgdiff", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_memo1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "memo1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_memo1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "memo1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_memo2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "memo2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_memo2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "memo2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_acno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "acno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_acno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "acno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sano", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sano", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bracket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bracket", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cloflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cloflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cloflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cloflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ExtFlag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ExtFlag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ExtFlag", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ExtFlag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_seno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_seno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DeNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DeNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DeNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DeNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DeName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DeName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DeName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DeName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_accono", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "accono", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_accono", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "accono", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.cn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_reno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "reno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace2", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cashmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cashmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cashmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cashmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cardmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cardmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cardmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "cardmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cardno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cardno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cardno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cardno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ticket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ticket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ticket", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ticket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_checkmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "checkmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_checkmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "checkmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_remitmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "remitmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_remitmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "remitmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_othermny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "othermny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_othermny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "othermny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_getprvacc", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "getprvacc", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_getprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "getprvacc", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_addprvacc", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "addprvacc", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_addprvacc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "addprvacc", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_actslt", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "actslt", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_actslt", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "actslt", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totdisc", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totdisc", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totdisc", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totdisc", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totreve", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totreve", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totreve", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totreve", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totexgdiff", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totexgdiff", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totexgdiff", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totexgdiff", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_memo1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "memo1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_memo1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "memo1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_memo2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "memo2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_memo2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "memo2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_acno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "acno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_acno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "acno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sano", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sano", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bracket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bracket", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cloflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cloflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cloflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cloflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ExtFlag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ExtFlag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ExtFlag", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ExtFlag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_seno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_seno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "seno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DeNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DeNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DeNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DeNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DeName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DeName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DeName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DeName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_accono", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "accono", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_accono", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "accono", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null)});
            // 
            // daM
            // 
            this.daM.DeleteCommand = this.sqlDeleteCommand1;
            this.daM.InsertCommand = this.sqlInsertCommand1;
            this.daM.SelectCommand = this.sqlSelectCommand1;
            this.daM.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "receiv", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("reno", "reno"),
                        new System.Data.Common.DataColumnMapping("redate", "redate"),
                        new System.Data.Common.DataColumnMapping("redate1", "redate1"),
                        new System.Data.Common.DataColumnMapping("redate2", "redate2"),
                        new System.Data.Common.DataColumnMapping("redateacs", "redateacs"),
                        new System.Data.Common.DataColumnMapping("redateacs1", "redateacs1"),
                        new System.Data.Common.DataColumnMapping("redateacs2", "redateacs2"),
                        new System.Data.Common.DataColumnMapping("redateace", "redateace"),
                        new System.Data.Common.DataColumnMapping("redateace1", "redateace1"),
                        new System.Data.Common.DataColumnMapping("redateace2", "redateace2"),
                        new System.Data.Common.DataColumnMapping("cono", "cono"),
                        new System.Data.Common.DataColumnMapping("coname1", "coname1"),
                        new System.Data.Common.DataColumnMapping("coname2", "coname2"),
                        new System.Data.Common.DataColumnMapping("cuno", "cuno"),
                        new System.Data.Common.DataColumnMapping("cuname2", "cuname2"),
                        new System.Data.Common.DataColumnMapping("cuname1", "cuname1"),
                        new System.Data.Common.DataColumnMapping("cutel1", "cutel1"),
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
                        new System.Data.Common.DataColumnMapping("sano", "sano"),
                        new System.Data.Common.DataColumnMapping("bracket", "bracket"),
                        new System.Data.Common.DataColumnMapping("recordno", "recordno"),
                        new System.Data.Common.DataColumnMapping("cloflag", "cloflag"),
                        new System.Data.Common.DataColumnMapping("ExtFlag", "ExtFlag"),
                        new System.Data.Common.DataColumnMapping("IsTrans", "IsTrans"),
                        new System.Data.Common.DataColumnMapping("spno", "spno"),
                        new System.Data.Common.DataColumnMapping("seno", "seno"),
                        new System.Data.Common.DataColumnMapping("DeNo", "DeNo"),
                        new System.Data.Common.DataColumnMapping("DeName", "DeName"),
                        new System.Data.Common.DataColumnMapping("accono", "accono"),
                        new System.Data.Common.DataColumnMapping("xa1par", "xa1par")})});
            this.daM.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "select * from receivd where (reno=@No)";
            this.sqlSelectCommand2.Connection = this.cn;
            this.sqlSelectCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@No", System.Data.SqlDbType.NVarChar, 20, "reno")});
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.cn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@reno", System.Data.SqlDbType.NVarChar, 0, "reno"),
            new System.Data.SqlClient.SqlParameter("@redate", System.Data.SqlDbType.NVarChar, 0, "redate"),
            new System.Data.SqlClient.SqlParameter("@redate1", System.Data.SqlDbType.NVarChar, 0, "redate1"),
            new System.Data.SqlClient.SqlParameter("@redate2", System.Data.SqlDbType.NVarChar, 0, "redate2"),
            new System.Data.SqlClient.SqlParameter("@redateacs", System.Data.SqlDbType.NVarChar, 0, "redateacs"),
            new System.Data.SqlClient.SqlParameter("@redateacs1", System.Data.SqlDbType.NVarChar, 0, "redateacs1"),
            new System.Data.SqlClient.SqlParameter("@redateacs2", System.Data.SqlDbType.NVarChar, 0, "redateacs2"),
            new System.Data.SqlClient.SqlParameter("@redateace", System.Data.SqlDbType.NVarChar, 0, "redateace"),
            new System.Data.SqlClient.SqlParameter("@redateace1", System.Data.SqlDbType.NVarChar, 0, "redateace1"),
            new System.Data.SqlClient.SqlParameter("@redateace2", System.Data.SqlDbType.NVarChar, 0, "redateace2"),
            new System.Data.SqlClient.SqlParameter("@cono", System.Data.SqlDbType.NVarChar, 0, "cono"),
            new System.Data.SqlClient.SqlParameter("@coname1", System.Data.SqlDbType.NVarChar, 0, "coname1"),
            new System.Data.SqlClient.SqlParameter("@coname2", System.Data.SqlDbType.NVarChar, 0, "coname2"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@cuname2", System.Data.SqlDbType.NVarChar, 0, "cuname2"),
            new System.Data.SqlClient.SqlParameter("@cuname1", System.Data.SqlDbType.NVarChar, 0, "cuname1"),
            new System.Data.SqlClient.SqlParameter("@cutel1", System.Data.SqlDbType.NVarChar, 0, "cutel1"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@emname", System.Data.SqlDbType.NVarChar, 0, "emname"),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@sadateac", System.Data.SqlDbType.NVarChar, 0, "sadateac"),
            new System.Data.SqlClient.SqlParameter("@sadateac1", System.Data.SqlDbType.NVarChar, 0, "sadateac1"),
            new System.Data.SqlClient.SqlParameter("@sadateac2", System.Data.SqlDbType.NVarChar, 0, "sadateac2"),
            new System.Data.SqlClient.SqlParameter("@sano", System.Data.SqlDbType.NVarChar, 0, "sano"),
            new System.Data.SqlClient.SqlParameter("@bracket", System.Data.SqlDbType.NVarChar, 0, "bracket"),
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@xa1name", System.Data.SqlDbType.NVarChar, 0, "xa1name"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@acctmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "acctmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@invno", System.Data.SqlDbType.NVarChar, 0, "invno"),
            new System.Data.SqlClient.SqlParameter("@discount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "discount", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@reverse", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "reverse", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@xa1par1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@reverseb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "reverseb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@exgstat", System.Data.SqlDbType.NVarChar, 0, "exgstat"),
            new System.Data.SqlClient.SqlParameter("@exgdiff", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "exgdiff", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@memo1", System.Data.SqlDbType.NVarChar, 0, "memo1"),
            new System.Data.SqlClient.SqlParameter("@extflag", System.Data.SqlDbType.NVarChar, 0, "extflag"),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans")});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.cn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@reno", System.Data.SqlDbType.NVarChar, 0, "reno"),
            new System.Data.SqlClient.SqlParameter("@redate", System.Data.SqlDbType.NVarChar, 0, "redate"),
            new System.Data.SqlClient.SqlParameter("@redate1", System.Data.SqlDbType.NVarChar, 0, "redate1"),
            new System.Data.SqlClient.SqlParameter("@redate2", System.Data.SqlDbType.NVarChar, 0, "redate2"),
            new System.Data.SqlClient.SqlParameter("@redateacs", System.Data.SqlDbType.NVarChar, 0, "redateacs"),
            new System.Data.SqlClient.SqlParameter("@redateacs1", System.Data.SqlDbType.NVarChar, 0, "redateacs1"),
            new System.Data.SqlClient.SqlParameter("@redateacs2", System.Data.SqlDbType.NVarChar, 0, "redateacs2"),
            new System.Data.SqlClient.SqlParameter("@redateace", System.Data.SqlDbType.NVarChar, 0, "redateace"),
            new System.Data.SqlClient.SqlParameter("@redateace1", System.Data.SqlDbType.NVarChar, 0, "redateace1"),
            new System.Data.SqlClient.SqlParameter("@redateace2", System.Data.SqlDbType.NVarChar, 0, "redateace2"),
            new System.Data.SqlClient.SqlParameter("@cono", System.Data.SqlDbType.NVarChar, 0, "cono"),
            new System.Data.SqlClient.SqlParameter("@coname1", System.Data.SqlDbType.NVarChar, 0, "coname1"),
            new System.Data.SqlClient.SqlParameter("@coname2", System.Data.SqlDbType.NVarChar, 0, "coname2"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@cuname2", System.Data.SqlDbType.NVarChar, 0, "cuname2"),
            new System.Data.SqlClient.SqlParameter("@cuname1", System.Data.SqlDbType.NVarChar, 0, "cuname1"),
            new System.Data.SqlClient.SqlParameter("@cutel1", System.Data.SqlDbType.NVarChar, 0, "cutel1"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@emname", System.Data.SqlDbType.NVarChar, 0, "emname"),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@sadateac", System.Data.SqlDbType.NVarChar, 0, "sadateac"),
            new System.Data.SqlClient.SqlParameter("@sadateac1", System.Data.SqlDbType.NVarChar, 0, "sadateac1"),
            new System.Data.SqlClient.SqlParameter("@sadateac2", System.Data.SqlDbType.NVarChar, 0, "sadateac2"),
            new System.Data.SqlClient.SqlParameter("@sano", System.Data.SqlDbType.NVarChar, 0, "sano"),
            new System.Data.SqlClient.SqlParameter("@bracket", System.Data.SqlDbType.NVarChar, 0, "bracket"),
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@xa1name", System.Data.SqlDbType.NVarChar, 0, "xa1name"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@acctmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "acctmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@invno", System.Data.SqlDbType.NVarChar, 0, "invno"),
            new System.Data.SqlClient.SqlParameter("@discount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "discount", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@reverse", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "reverse", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@xa1par1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@reverseb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "reverseb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@exgstat", System.Data.SqlDbType.NVarChar, 0, "exgstat"),
            new System.Data.SqlClient.SqlParameter("@exgdiff", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "exgdiff", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@memo1", System.Data.SqlDbType.NVarChar, 0, "memo1"),
            new System.Data.SqlClient.SqlParameter("@extflag", System.Data.SqlDbType.NVarChar, 0, "extflag"),
            new System.Data.SqlClient.SqlParameter("@IsTrans", System.Data.SqlDbType.NVarChar, 0, "IsTrans"),
            new System.Data.SqlClient.SqlParameter("@IsNull_reno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "reno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_reno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "reno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace2", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sadateac", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sadateac", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sadateac", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sadateac", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sadateac1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sadateac1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sadateac1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sadateac1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sadateac2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sadateac2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sadateac2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sadateac2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sano", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sano", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bracket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bracket", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_acctmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "acctmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_acctmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "acctmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_discount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "discount", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_discount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "discount", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_reverse", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "reverse", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_reverse", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "reverse", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_reverseb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "reverseb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_reverseb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "reverseb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_exgstat", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "exgstat", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_exgstat", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "exgstat", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_exgdiff", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "exgdiff", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_exgdiff", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "exgdiff", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_memo1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "memo1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_memo1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "memo1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_extflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "extflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_extflag", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "extflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_ReID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ReID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@ReID", System.Data.SqlDbType.Int, 4, "ReID")});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.cn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@IsNull_reno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "reno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_reno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "reno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateacs2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateacs2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateacs2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateacs2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_redateace2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "redateace2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_redateace2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "redateace2", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sadateac", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sadateac", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sadateac", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sadateac", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sadateac1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sadateac1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sadateac1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sadateac1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sadateac2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sadateac2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sadateac2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sadateac2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_sano", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_sano", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "sano", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_bracket", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_bracket", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "bracket", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_acctmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "acctmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_acctmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "acctmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_invno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "invno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_invno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "invno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_discount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "discount", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_discount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "discount", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_reverse", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "reverse", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_reverse", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "reverse", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_reverseb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "reverseb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_reverseb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "reverseb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_exgstat", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "exgstat", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_exgstat", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "exgstat", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_exgdiff", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "exgdiff", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_exgdiff", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "exgdiff", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_memo1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "memo1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_memo1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "memo1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_extflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "extflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_extflag", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "extflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IsTrans", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IsTrans", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IsTrans", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_ReID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ReID", System.Data.DataRowVersion.Original, null)});
            // 
            // daD
            // 
            this.daD.DeleteCommand = this.sqlDeleteCommand2;
            this.daD.InsertCommand = this.sqlInsertCommand2;
            this.daD.SelectCommand = this.sqlSelectCommand2;
            this.daD.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "receivd", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("reno", "reno"),
                        new System.Data.Common.DataColumnMapping("redate", "redate"),
                        new System.Data.Common.DataColumnMapping("redate1", "redate1"),
                        new System.Data.Common.DataColumnMapping("redate2", "redate2"),
                        new System.Data.Common.DataColumnMapping("redateacs", "redateacs"),
                        new System.Data.Common.DataColumnMapping("redateacs1", "redateacs1"),
                        new System.Data.Common.DataColumnMapping("redateacs2", "redateacs2"),
                        new System.Data.Common.DataColumnMapping("redateace", "redateace"),
                        new System.Data.Common.DataColumnMapping("redateace1", "redateace1"),
                        new System.Data.Common.DataColumnMapping("redateace2", "redateace2"),
                        new System.Data.Common.DataColumnMapping("cono", "cono"),
                        new System.Data.Common.DataColumnMapping("coname1", "coname1"),
                        new System.Data.Common.DataColumnMapping("coname2", "coname2"),
                        new System.Data.Common.DataColumnMapping("cuno", "cuno"),
                        new System.Data.Common.DataColumnMapping("cuname2", "cuname2"),
                        new System.Data.Common.DataColumnMapping("cuname1", "cuname1"),
                        new System.Data.Common.DataColumnMapping("cutel1", "cutel1"),
                        new System.Data.Common.DataColumnMapping("emno", "emno"),
                        new System.Data.Common.DataColumnMapping("emname", "emname"),
                        new System.Data.Common.DataColumnMapping("recordno", "recordno"),
                        new System.Data.Common.DataColumnMapping("sadateac", "sadateac"),
                        new System.Data.Common.DataColumnMapping("sadateac1", "sadateac1"),
                        new System.Data.Common.DataColumnMapping("sadateac2", "sadateac2"),
                        new System.Data.Common.DataColumnMapping("sano", "sano"),
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
                        new System.Data.Common.DataColumnMapping("extflag", "extflag"),
                        new System.Data.Common.DataColumnMapping("IsTrans", "IsTrans"),
                        new System.Data.Common.DataColumnMapping("ReID", "ReID")})});
            this.daD.UpdateCommand = this.sqlUpdateCommand2;
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
            this.labelT1.Text = "收款憑證";
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
            this.labelT2.Text = "客戶編號";
            // 
            // qReno
            // 
            this.qReno.AllowGrayBackColor = false;
            this.qReno.AllowResize = true;
            this.qReno.Font = new System.Drawing.Font("細明體", 12F);
            this.qReno.Location = new System.Drawing.Point(79, 16);
            this.qReno.MaxLength = 16;
            this.qReno.Name = "qReno";
            this.qReno.oLen = 0;
            this.qReno.Size = new System.Drawing.Size(135, 27);
            this.qReno.TabIndex = 1;
            this.qReno.TextChanged += new System.EventHandler(this.qReno_TextChanged);
            this.qReno.Validating += new System.ComponentModel.CancelEventHandler(this.qCuno_Validating);
            // 
            // qCuno
            // 
            this.qCuno.AllowGrayBackColor = false;
            this.qCuno.AllowResize = true;
            this.qCuno.Font = new System.Drawing.Font("細明體", 12F);
            this.qCuno.Location = new System.Drawing.Point(286, 16);
            this.qCuno.MaxLength = 10;
            this.qCuno.Name = "qCuno";
            this.qCuno.oLen = 0;
            this.qCuno.Size = new System.Drawing.Size(87, 27);
            this.qCuno.TabIndex = 2;
            this.qCuno.TextChanged += new System.EventHandler(this.qCuno_TextChanged);
            this.qCuno.DoubleClick += new System.EventHandler(this.qCuno_DoubleClick);
            this.qCuno.Validating += new System.ComponentModel.CancelEventHandler(this.qCuno_Validating);
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
            this.收款憑證,
            this.收款日期,
            this.客戶編號,
            this.客戶簡稱,
            this.沖抵帳款,
            this.累入預收});
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
            // 收款憑證
            // 
            this.收款憑證.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.收款憑證.DataPropertyName = "ReNo";
            this.收款憑證.HeaderText = "收款憑證";
            this.收款憑證.MaxInputLength = 16;
            this.收款憑證.Name = "收款憑證";
            this.收款憑證.ReadOnly = true;
            this.收款憑證.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.收款憑證.Width = 141;
            // 
            // 收款日期
            // 
            this.收款日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.收款日期.HeaderText = "收款日期";
            this.收款日期.MaxInputLength = 10;
            this.收款日期.Name = "收款日期";
            this.收款日期.ReadOnly = true;
            this.收款日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.收款日期.Width = 93;
            // 
            // 客戶編號
            // 
            this.客戶編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶編號.DataPropertyName = "cuno";
            this.客戶編號.HeaderText = "客戶編號";
            this.客戶編號.MaxInputLength = 10;
            this.客戶編號.Name = "客戶編號";
            this.客戶編號.ReadOnly = true;
            this.客戶編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶編號.Width = 93;
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
            // 累入預收
            // 
            this.累入預收.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.累入預收.DataPropertyName = "AddPrvAcc";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.累入預收.DefaultCellStyle = dataGridViewCellStyle3;
            this.累入預收.FirstNum = 0;
            this.累入預收.HeaderText = "累入預收";
            this.累入預收.LastNum = 0;
            this.累入預收.MarkThousand = false;
            this.累入預收.MaxInputLength = 16;
            this.累入預收.Name = "累入預收";
            this.累入預收.NullInput = false;
            this.累入預收.NullValue = "0";
            this.累入預收.ReadOnly = true;
            this.累入預收.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.累入預收.Width = 141;
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
            this.沖帳金額,
            this.折讓金額,
            this.未收金額,
            this.單據總計});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT2.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewT2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT2.EnableHeadersVisualStyles = false;
            this.dataGridViewT2.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT2.ISDocument = false;
            this.dataGridViewT2.Location = new System.Drawing.Point(0, 348);
            this.dataGridViewT2.MultiSelect = false;
            this.dataGridViewT2.Name = "dataGridViewT2";
            this.dataGridViewT2.ReadOnly = true;
            this.dataGridViewT2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT2.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewT2.RowHeadersWidth = 20;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT2.RowsDefaultCellStyle = dataGridViewCellStyle12;
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
            // 沖帳金額
            // 
            this.沖帳金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.沖帳金額.DataPropertyName = "Reverse";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.沖帳金額.DefaultCellStyle = dataGridViewCellStyle8;
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
            // 折讓金額
            // 
            this.折讓金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折讓金額.DataPropertyName = "Discount";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.折讓金額.DefaultCellStyle = dataGridViewCellStyle9;
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
            // 未收金額
            // 
            this.未收金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.未收金額.DataPropertyName = "AcctMny";
            this.未收金額.FirstNum = 0;
            this.未收金額.HeaderText = "未收金額";
            this.未收金額.LastNum = 0;
            this.未收金額.MarkThousand = false;
            this.未收金額.MaxInputLength = 16;
            this.未收金額.Name = "未收金額";
            this.未收金額.NullInput = false;
            this.未收金額.NullValue = "0";
            this.未收金額.ReadOnly = true;
            this.未收金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.未收金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.未收金額.Width = 141;
            // 
            // 單據總計
            // 
            this.單據總計.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單據總計.DataPropertyName = "TotMny";
            this.單據總計.FirstNum = 0;
            this.單據總計.HeaderText = "單據總計";
            this.單據總計.LastNum = 0;
            this.單據總計.MarkThousand = false;
            this.單據總計.MaxInputLength = 16;
            this.單據總計.Name = "單據總計";
            this.單據總計.NullInput = false;
            this.單據總計.NullValue = "0";
            this.單據總計.ReadOnly = true;
            this.單據總計.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單據總計.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單據總計.Width = 141;
            // 
            // FrmReceivdQuery
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
            this.Controls.Add(this.qCuno);
            this.Controls.Add(this.qReno);
            this.Controls.Add(this.labelT2);
            this.Controls.Add(this.labelT1);
            this.Name = "FrmReceivdQuery";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmReceivdQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection cn;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daM;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlDataAdapter daD;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.TextBoxT qReno;
        private JE.MyControl.TextBoxT qCuno;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnCancel;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.DataGridViewT dataGridViewT2;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 收款憑證;
        private System.Windows.Forms.DataGridViewTextBoxColumn 收款日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶簡稱;
        private JE.MyControl.DataGridViewTextNumberT 沖抵帳款;
        private JE.MyControl.DataGridViewTextNumberT 累入預收;
        private JE.MyControl.ButtonSmallT btnQoo;
        private System.Windows.Forms.DataGridViewTextBoxColumn 點選;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 帳款日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單據;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單據號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 發票號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 幣別;
        private JE.MyControl.DataGridViewTextNumberT 沖帳金額;
        private JE.MyControl.DataGridViewTextNumberT 折讓金額;
        private JE.MyControl.DataGridViewTextNumberT 未收金額;
        private JE.MyControl.DataGridViewTextNumberT 單據總計;
    }
}