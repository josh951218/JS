namespace S_61.subMenuFm_2
{
    partial class FrmOrderToLend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrderToLend));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.orno = new JE.MyControl.TextBoxT();
            this.qcuno = new JE.MyControl.TextBoxT();
            this.btnCKall = new JE.MyControl.ButtonSmallT();
            this.btnCKnull = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnCancel = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.訂單憑證 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.訂單日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.稅別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.訂單總額 = new JE.MyControl.DataGridViewTextNumberT();
            this.訂單人員 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewT2 = new JE.MyControl.DataGridViewT();
            this.點選 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單價 = new JE.MyControl.DataGridViewTextNumberT();
            this.折數 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.稅前售價 = new JE.MyControl.DataGridViewTextNumberT();
            this.稅前金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).BeginInit();
            this.SuspendLayout();
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "select \r\nx3name =  \r\n case \r\n when x3no=1 then \'外加稅\'\r\nwhen x3no=2 then \'內含稅\'\r\nwhe" +
    "n x3no=3 then \'零稅\'\r\n when x3no=4 then \'免稅\'\r\n end,* \r\nfrom [order] \r\nwhere  (cuno" +
    "=@cuno)\r\n order by orno desc";
            this.sqlSelectCommand1.Connection = this.cn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 10, "cuno")});
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
            new System.Data.SqlClient.SqlParameter("@orno", System.Data.SqlDbType.NVarChar, 0, "orno"),
            new System.Data.SqlClient.SqlParameter("@ordate", System.Data.SqlDbType.NVarChar, 0, "ordate"),
            new System.Data.SqlClient.SqlParameter("@ordate1", System.Data.SqlDbType.NVarChar, 0, "ordate1"),
            new System.Data.SqlClient.SqlParameter("@ordate2", System.Data.SqlDbType.NVarChar, 0, "ordate2"),
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@ortrnflag", System.Data.SqlDbType.Bit, 0, "ortrnflag"),
            new System.Data.SqlClient.SqlParameter("@oroverflag", System.Data.SqlDbType.Bit, 0, "oroverflag"),
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
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@xa1name", System.Data.SqlDbType.NVarChar, 0, "xa1name"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@trno", System.Data.SqlDbType.NVarChar, 0, "trno"),
            new System.Data.SqlClient.SqlParameter("@trname", System.Data.SqlDbType.NVarChar, 0, "trname"),
            new System.Data.SqlClient.SqlParameter("@taxmnyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyf", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@x3no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x3no", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@tax", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "tax", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@orpayment", System.Data.SqlDbType.NVarChar, 0, "orpayment"),
            new System.Data.SqlClient.SqlParameter("@orperiod", System.Data.SqlDbType.NVarChar, 0, "orperiod"),
            new System.Data.SqlClient.SqlParameter("@ormemo", System.Data.SqlDbType.NVarChar, 0, "ormemo"),
            new System.Data.SqlClient.SqlParameter("@ormemo1", System.Data.SqlDbType.Text, 0, "ormemo1"),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@RevMoney", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "RevMoney", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@UsrNo", System.Data.SqlDbType.NVarChar, 0, "UsrNo"),
            new System.Data.SqlClient.SqlParameter("@MeMain", System.Data.SqlDbType.Text, 0, "MeMain"),
            new System.Data.SqlClient.SqlParameter("@MeOther", System.Data.SqlDbType.Text, 0, "MeOther"),
            new System.Data.SqlClient.SqlParameter("@MePrint", System.Data.SqlDbType.Text, 0, "MePrint"),
            new System.Data.SqlClient.SqlParameter("@MeSize", System.Data.SqlDbType.NVarChar, 0, "MeSize"),
            new System.Data.SqlClient.SqlParameter("@MeSize2", System.Data.SqlDbType.NVarChar, 0, "MeSize2"),
            new System.Data.SqlClient.SqlParameter("@AppDate", System.Data.SqlDbType.NVarChar, 0, "AppDate"),
            new System.Data.SqlClient.SqlParameter("@EdtDate", System.Data.SqlDbType.NVarChar, 0, "EdtDate"),
            new System.Data.SqlClient.SqlParameter("@AppScNo", System.Data.SqlDbType.NVarChar, 0, "AppScNo"),
            new System.Data.SqlClient.SqlParameter("@EdtScNo", System.Data.SqlDbType.NVarChar, 0, "EdtScNo"),
            new System.Data.SqlClient.SqlParameter("@spno", System.Data.SqlDbType.NVarChar, 0, "spno"),
            new System.Data.SqlClient.SqlParameter("@spname", System.Data.SqlDbType.NVarChar, 0, "spname"),
            new System.Data.SqlClient.SqlParameter("@esdate", System.Data.SqlDbType.NVarChar, 0, "esdate"),
            new System.Data.SqlClient.SqlParameter("@esdate1", System.Data.SqlDbType.NVarChar, 0, "esdate1"),
            new System.Data.SqlClient.SqlParameter("@X2No", System.Data.SqlDbType.NVarChar, 0, "X2No"),
            new System.Data.SqlClient.SqlParameter("@NetNo", System.Data.SqlDbType.NVarChar, 0, "NetNo")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.cn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@orno", System.Data.SqlDbType.NVarChar, 0, "orno"),
            new System.Data.SqlClient.SqlParameter("@ordate", System.Data.SqlDbType.NVarChar, 0, "ordate"),
            new System.Data.SqlClient.SqlParameter("@ordate1", System.Data.SqlDbType.NVarChar, 0, "ordate1"),
            new System.Data.SqlClient.SqlParameter("@ordate2", System.Data.SqlDbType.NVarChar, 0, "ordate2"),
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@ortrnflag", System.Data.SqlDbType.Bit, 0, "ortrnflag"),
            new System.Data.SqlClient.SqlParameter("@oroverflag", System.Data.SqlDbType.Bit, 0, "oroverflag"),
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
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@xa1name", System.Data.SqlDbType.NVarChar, 0, "xa1name"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@trno", System.Data.SqlDbType.NVarChar, 0, "trno"),
            new System.Data.SqlClient.SqlParameter("@trname", System.Data.SqlDbType.NVarChar, 0, "trname"),
            new System.Data.SqlClient.SqlParameter("@taxmnyf", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyf", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@x3no", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "x3no", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@tax", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "tax", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@totmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@orpayment", System.Data.SqlDbType.NVarChar, 0, "orpayment"),
            new System.Data.SqlClient.SqlParameter("@orperiod", System.Data.SqlDbType.NVarChar, 0, "orperiod"),
            new System.Data.SqlClient.SqlParameter("@ormemo", System.Data.SqlDbType.NVarChar, 0, "ormemo"),
            new System.Data.SqlClient.SqlParameter("@ormemo1", System.Data.SqlDbType.Text, 0, "ormemo1"),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@RevMoney", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "RevMoney", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@UsrNo", System.Data.SqlDbType.NVarChar, 0, "UsrNo"),
            new System.Data.SqlClient.SqlParameter("@MeMain", System.Data.SqlDbType.Text, 0, "MeMain"),
            new System.Data.SqlClient.SqlParameter("@MeOther", System.Data.SqlDbType.Text, 0, "MeOther"),
            new System.Data.SqlClient.SqlParameter("@MePrint", System.Data.SqlDbType.Text, 0, "MePrint"),
            new System.Data.SqlClient.SqlParameter("@MeSize", System.Data.SqlDbType.NVarChar, 0, "MeSize"),
            new System.Data.SqlClient.SqlParameter("@MeSize2", System.Data.SqlDbType.NVarChar, 0, "MeSize2"),
            new System.Data.SqlClient.SqlParameter("@AppDate", System.Data.SqlDbType.NVarChar, 0, "AppDate"),
            new System.Data.SqlClient.SqlParameter("@EdtDate", System.Data.SqlDbType.NVarChar, 0, "EdtDate"),
            new System.Data.SqlClient.SqlParameter("@AppScNo", System.Data.SqlDbType.NVarChar, 0, "AppScNo"),
            new System.Data.SqlClient.SqlParameter("@EdtScNo", System.Data.SqlDbType.NVarChar, 0, "EdtScNo"),
            new System.Data.SqlClient.SqlParameter("@spno", System.Data.SqlDbType.NVarChar, 0, "spno"),
            new System.Data.SqlClient.SqlParameter("@spname", System.Data.SqlDbType.NVarChar, 0, "spname"),
            new System.Data.SqlClient.SqlParameter("@esdate", System.Data.SqlDbType.NVarChar, 0, "esdate"),
            new System.Data.SqlClient.SqlParameter("@esdate1", System.Data.SqlDbType.NVarChar, 0, "esdate1"),
            new System.Data.SqlClient.SqlParameter("@X2No", System.Data.SqlDbType.NVarChar, 0, "X2No"),
            new System.Data.SqlClient.SqlParameter("@NetNo", System.Data.SqlDbType.NVarChar, 0, "NetNo"),
            new System.Data.SqlClient.SqlParameter("@Original_orno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ortrnflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ortrnflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ortrnflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ortrnflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_oroverflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "oroverflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_oroverflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "oroverflag", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_trno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "trno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_trno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "trno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_trname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "trname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_trname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "trname", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_tax", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "tax", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_tax", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "tax", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmnyb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmnyb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmnyb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_orpayment", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "orpayment", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_orpayment", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orpayment", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_orperiod", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "orperiod", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_orperiod", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orperiod", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ormemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ormemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ormemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ormemo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_RevMoney", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RevMoney", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_RevMoney", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "RevMoney", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_UsrNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UsrNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_UsrNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "UsrNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MeSize", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MeSize", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MeSize", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MeSize", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MeSize2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MeSize2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MeSize2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MeSize2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AppDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AppDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AppDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AppDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EdtDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EdtDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EdtDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EdtDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AppScNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AppScNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AppScNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AppScNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EdtScNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EdtScNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EdtScNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EdtScNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_X2No", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "X2No", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_X2No", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "X2No", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NetNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NetNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NetNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NetNo", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.cn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_orno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ortrnflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ortrnflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ortrnflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ortrnflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_oroverflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "oroverflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_oroverflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "oroverflag", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1name", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_trno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "trno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_trno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "trno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_trname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "trname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_trname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "trname", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_tax", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "tax", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_tax", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "tax", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmny", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmny", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmny", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_taxb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "taxb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_taxb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_totmnyb", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "totmnyb", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_totmnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "totmnyb", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_orpayment", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "orpayment", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_orpayment", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orpayment", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_orperiod", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "orperiod", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_orperiod", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orperiod", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ormemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ormemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ormemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ormemo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_recordno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_RevMoney", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RevMoney", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_RevMoney", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "RevMoney", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_UsrNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UsrNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_UsrNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "UsrNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MeSize", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MeSize", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MeSize", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MeSize", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MeSize2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MeSize2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MeSize2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MeSize2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AppDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AppDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AppDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AppDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EdtDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EdtDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EdtDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EdtDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AppScNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AppScNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AppScNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AppScNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EdtScNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EdtScNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EdtScNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EdtScNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_spname", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "spname", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_spname", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "spname", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_X2No", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "X2No", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_X2No", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "X2No", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NetNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NetNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NetNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NetNo", System.Data.DataRowVersion.Original, null)});
            // 
            // daM
            // 
            this.daM.DeleteCommand = this.sqlDeleteCommand1;
            this.daM.InsertCommand = this.sqlInsertCommand1;
            this.daM.SelectCommand = this.sqlSelectCommand1;
            this.daM.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "order", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("x3name", "x3name"),
                        new System.Data.Common.DataColumnMapping("orno", "orno"),
                        new System.Data.Common.DataColumnMapping("ordate", "ordate"),
                        new System.Data.Common.DataColumnMapping("ordate1", "ordate1"),
                        new System.Data.Common.DataColumnMapping("ordate2", "ordate2"),
                        new System.Data.Common.DataColumnMapping("quno", "quno"),
                        new System.Data.Common.DataColumnMapping("ortrnflag", "ortrnflag"),
                        new System.Data.Common.DataColumnMapping("oroverflag", "oroverflag"),
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
                        new System.Data.Common.DataColumnMapping("xa1no", "xa1no"),
                        new System.Data.Common.DataColumnMapping("xa1name", "xa1name"),
                        new System.Data.Common.DataColumnMapping("xa1par", "xa1par"),
                        new System.Data.Common.DataColumnMapping("trno", "trno"),
                        new System.Data.Common.DataColumnMapping("trname", "trname"),
                        new System.Data.Common.DataColumnMapping("taxmnyf", "taxmnyf"),
                        new System.Data.Common.DataColumnMapping("taxmnyb", "taxmnyb"),
                        new System.Data.Common.DataColumnMapping("taxmny", "taxmny"),
                        new System.Data.Common.DataColumnMapping("x3no", "x3no"),
                        new System.Data.Common.DataColumnMapping("rate", "rate"),
                        new System.Data.Common.DataColumnMapping("tax", "tax"),
                        new System.Data.Common.DataColumnMapping("totmny", "totmny"),
                        new System.Data.Common.DataColumnMapping("taxb", "taxb"),
                        new System.Data.Common.DataColumnMapping("totmnyb", "totmnyb"),
                        new System.Data.Common.DataColumnMapping("orpayment", "orpayment"),
                        new System.Data.Common.DataColumnMapping("orperiod", "orperiod"),
                        new System.Data.Common.DataColumnMapping("ormemo", "ormemo"),
                        new System.Data.Common.DataColumnMapping("ormemo1", "ormemo1"),
                        new System.Data.Common.DataColumnMapping("recordno", "recordno"),
                        new System.Data.Common.DataColumnMapping("RevMoney", "RevMoney"),
                        new System.Data.Common.DataColumnMapping("UsrNo", "UsrNo"),
                        new System.Data.Common.DataColumnMapping("MeMain", "MeMain"),
                        new System.Data.Common.DataColumnMapping("MeOther", "MeOther"),
                        new System.Data.Common.DataColumnMapping("MePrint", "MePrint"),
                        new System.Data.Common.DataColumnMapping("MeSize", "MeSize"),
                        new System.Data.Common.DataColumnMapping("MeSize2", "MeSize2"),
                        new System.Data.Common.DataColumnMapping("AppDate", "AppDate"),
                        new System.Data.Common.DataColumnMapping("EdtDate", "EdtDate"),
                        new System.Data.Common.DataColumnMapping("AppScNo", "AppScNo"),
                        new System.Data.Common.DataColumnMapping("EdtScNo", "EdtScNo"),
                        new System.Data.Common.DataColumnMapping("spno", "spno"),
                        new System.Data.Common.DataColumnMapping("spname", "spname"),
                        new System.Data.Common.DataColumnMapping("esdate", "esdate"),
                        new System.Data.Common.DataColumnMapping("esdate1", "esdate1"),
                        new System.Data.Common.DataColumnMapping("X2No", "X2No"),
                        new System.Data.Common.DataColumnMapping("NetNo", "NetNo")})});
            this.daM.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "select * from orderd where (orno=@No)";
            this.sqlSelectCommand2.Connection = this.cn;
            this.sqlSelectCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@No", System.Data.SqlDbType.NVarChar, 16, "orno")});
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.cn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@orno", System.Data.SqlDbType.NVarChar, 0, "orno"),
            new System.Data.SqlClient.SqlParameter("@ordate", System.Data.SqlDbType.NVarChar, 0, "ordate"),
            new System.Data.SqlClient.SqlParameter("@ordate1", System.Data.SqlDbType.NVarChar, 0, "ordate1"),
            new System.Data.SqlClient.SqlParameter("@ordate2", System.Data.SqlDbType.NVarChar, 0, "ordate2"),
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@ortrnflag", System.Data.SqlDbType.Bit, 0, "ortrnflag"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@trno", System.Data.SqlDbType.NVarChar, 0, "trno"),
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itunit", System.Data.SqlDbType.NVarChar, 0, "itunit"),
            new System.Data.SqlClient.SqlParameter("@itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "price", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "prs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxprice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@priceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "priceb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxpriceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxpriceb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qtyout", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyout", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qtyin", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyin", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@esdate", System.Data.SqlDbType.NVarChar, 0, "esdate"),
            new System.Data.SqlClient.SqlParameter("@esdate1", System.Data.SqlDbType.NVarChar, 0, "esdate1"),
            new System.Data.SqlClient.SqlParameter("@esdate2", System.Data.SqlDbType.NVarChar, 0, "esdate2"),
            new System.Data.SqlClient.SqlParameter("@stkqtyflag", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "stkqtyflag", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@memo", System.Data.SqlDbType.NVarChar, 0, "memo"),
            new System.Data.SqlClient.SqlParameter("@lowzero", System.Data.SqlDbType.NVarChar, 0, "lowzero"),
            new System.Data.SqlClient.SqlParameter("@bomid", System.Data.SqlDbType.NVarChar, 0, "bomid"),
            new System.Data.SqlClient.SqlParameter("@bomrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "bomrec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@sltflag", System.Data.SqlDbType.Bit, 0, "sltflag"),
            new System.Data.SqlClient.SqlParameter("@extflag", System.Data.SqlDbType.Bit, 0, "extflag"),
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
            new System.Data.SqlClient.SqlParameter("@stName", System.Data.SqlDbType.NVarChar, 0, "stName"),
            new System.Data.SqlClient.SqlParameter("@qtyNotOut", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyNotOut", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qtyNotInStk", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyNotInStk", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Punit", System.Data.SqlDbType.NVarChar, 0, "Punit"),
            new System.Data.SqlClient.SqlParameter("@Pqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Pqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@munit", System.Data.SqlDbType.NVarChar, 0, "munit"),
            new System.Data.SqlClient.SqlParameter("@mlong", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mlong", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mwidth1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mwidth2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth2", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mwidth3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth3", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mwidth4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth4", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mformula", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "mformula", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Pformula", System.Data.SqlDbType.NVarChar, 0, "Pformula")});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.cn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@orno", System.Data.SqlDbType.NVarChar, 0, "orno"),
            new System.Data.SqlClient.SqlParameter("@ordate", System.Data.SqlDbType.NVarChar, 0, "ordate"),
            new System.Data.SqlClient.SqlParameter("@ordate1", System.Data.SqlDbType.NVarChar, 0, "ordate1"),
            new System.Data.SqlClient.SqlParameter("@ordate2", System.Data.SqlDbType.NVarChar, 0, "ordate2"),
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@ortrnflag", System.Data.SqlDbType.Bit, 0, "ortrnflag"),
            new System.Data.SqlClient.SqlParameter("@cuno", System.Data.SqlDbType.NVarChar, 0, "cuno"),
            new System.Data.SqlClient.SqlParameter("@emno", System.Data.SqlDbType.NVarChar, 0, "emno"),
            new System.Data.SqlClient.SqlParameter("@xa1no", System.Data.SqlDbType.NVarChar, 0, "xa1no"),
            new System.Data.SqlClient.SqlParameter("@xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@trno", System.Data.SqlDbType.NVarChar, 0, "trno"),
            new System.Data.SqlClient.SqlParameter("@itno", System.Data.SqlDbType.NVarChar, 0, "itno"),
            new System.Data.SqlClient.SqlParameter("@itname", System.Data.SqlDbType.NVarChar, 0, "itname"),
            new System.Data.SqlClient.SqlParameter("@ittrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ittrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@itunit", System.Data.SqlDbType.NVarChar, 0, "itunit"),
            new System.Data.SqlClient.SqlParameter("@itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "price", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "prs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@rate", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "rate", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxprice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxprice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mny", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mny", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@priceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "priceb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@taxpriceb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "taxpriceb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mnyb", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mnyb", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qtyout", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyout", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qtyin", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyin", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@esdate", System.Data.SqlDbType.NVarChar, 0, "esdate"),
            new System.Data.SqlClient.SqlParameter("@esdate1", System.Data.SqlDbType.NVarChar, 0, "esdate1"),
            new System.Data.SqlClient.SqlParameter("@esdate2", System.Data.SqlDbType.NVarChar, 0, "esdate2"),
            new System.Data.SqlClient.SqlParameter("@stkqtyflag", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "stkqtyflag", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@memo", System.Data.SqlDbType.NVarChar, 0, "memo"),
            new System.Data.SqlClient.SqlParameter("@lowzero", System.Data.SqlDbType.NVarChar, 0, "lowzero"),
            new System.Data.SqlClient.SqlParameter("@bomid", System.Data.SqlDbType.NVarChar, 0, "bomid"),
            new System.Data.SqlClient.SqlParameter("@bomrec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "bomrec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@recordno", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "recordno", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@sltflag", System.Data.SqlDbType.Bit, 0, "sltflag"),
            new System.Data.SqlClient.SqlParameter("@extflag", System.Data.SqlDbType.Bit, 0, "extflag"),
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
            new System.Data.SqlClient.SqlParameter("@stName", System.Data.SqlDbType.NVarChar, 0, "stName"),
            new System.Data.SqlClient.SqlParameter("@qtyNotOut", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyNotOut", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@qtyNotInStk", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyNotInStk", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Punit", System.Data.SqlDbType.NVarChar, 0, "Punit"),
            new System.Data.SqlClient.SqlParameter("@Pqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Pqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@munit", System.Data.SqlDbType.NVarChar, 0, "munit"),
            new System.Data.SqlClient.SqlParameter("@mlong", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mlong", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mwidth1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth1", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mwidth2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth2", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mwidth3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth3", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mwidth4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth4", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@mformula", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "mformula", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Pformula", System.Data.SqlDbType.NVarChar, 0, "Pformula"),
            new System.Data.SqlClient.SqlParameter("@Original_OrID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "OrID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_orno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_orno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ortrnflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ortrnflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ortrnflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ortrnflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_trno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "trno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_trno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "trno", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_qtyout", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qtyout", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qtyout", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyout", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qtyin", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qtyin", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qtyin", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyin", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stkqtyflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stkqtyflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stkqtyflag", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "stkqtyflag", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_stName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qtyNotOut", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qtyNotOut", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qtyNotOut", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyNotOut", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qtyNotInStk", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qtyNotInStk", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qtyNotInStk", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyNotInStk", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Punit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Punit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Punit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Punit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Pqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Pqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Pqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Pqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_munit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "munit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_munit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "munit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mlong", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mlong", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mlong", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mlong", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mwidth1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mwidth1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mwidth1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mwidth2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mwidth2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mwidth2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mwidth3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mwidth3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mwidth3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mwidth4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mwidth4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mwidth4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mformula", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mformula", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mformula", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "mformula", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Pformula", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Pformula", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Pformula", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Pformula", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@OrID", System.Data.SqlDbType.Int, 4, "OrID")});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.cn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_OrID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "OrID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_orno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_orno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "orno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ordate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ordate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ordate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ordate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ortrnflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ortrnflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ortrnflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ortrnflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_cuno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_cuno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "cuno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_emno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_emno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "emno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1no", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1no", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "xa1no", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_xa1par", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "xa1par", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_xa1par", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(11)), ((byte)(4)), "xa1par", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_trno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "trno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_trno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "trno", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_qtyout", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qtyout", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qtyout", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyout", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qtyin", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qtyin", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qtyin", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyin", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_esdate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "esdate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_esdate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "esdate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_stkqtyflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stkqtyflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stkqtyflag", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "stkqtyflag", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_stName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "stName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_stName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "stName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qtyNotOut", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qtyNotOut", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qtyNotOut", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyNotOut", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qtyNotInStk", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qtyNotInStk", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qtyNotInStk", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "qtyNotInStk", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Punit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Punit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Punit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Punit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Pqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Pqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Pqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Pqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_munit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "munit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_munit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "munit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mlong", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mlong", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mlong", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mlong", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mwidth1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mwidth1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mwidth1", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mwidth2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mwidth2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mwidth2", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mwidth3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mwidth3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mwidth3", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mwidth4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mwidth4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mwidth4", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "mwidth4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_mformula", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "mformula", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_mformula", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "mformula", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Pformula", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Pformula", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Pformula", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Pformula", System.Data.DataRowVersion.Original, null)});
            // 
            // daD
            // 
            this.daD.DeleteCommand = this.sqlDeleteCommand2;
            this.daD.InsertCommand = this.sqlInsertCommand2;
            this.daD.SelectCommand = this.sqlSelectCommand2;
            this.daD.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "orderd", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("OrID", "OrID"),
                        new System.Data.Common.DataColumnMapping("orno", "orno"),
                        new System.Data.Common.DataColumnMapping("ordate", "ordate"),
                        new System.Data.Common.DataColumnMapping("ordate1", "ordate1"),
                        new System.Data.Common.DataColumnMapping("ordate2", "ordate2"),
                        new System.Data.Common.DataColumnMapping("quno", "quno"),
                        new System.Data.Common.DataColumnMapping("ortrnflag", "ortrnflag"),
                        new System.Data.Common.DataColumnMapping("cuno", "cuno"),
                        new System.Data.Common.DataColumnMapping("emno", "emno"),
                        new System.Data.Common.DataColumnMapping("xa1no", "xa1no"),
                        new System.Data.Common.DataColumnMapping("xa1par", "xa1par"),
                        new System.Data.Common.DataColumnMapping("trno", "trno"),
                        new System.Data.Common.DataColumnMapping("itno", "itno"),
                        new System.Data.Common.DataColumnMapping("itname", "itname"),
                        new System.Data.Common.DataColumnMapping("ittrait", "ittrait"),
                        new System.Data.Common.DataColumnMapping("itunit", "itunit"),
                        new System.Data.Common.DataColumnMapping("itpkgqty", "itpkgqty"),
                        new System.Data.Common.DataColumnMapping("qty", "qty"),
                        new System.Data.Common.DataColumnMapping("price", "price"),
                        new System.Data.Common.DataColumnMapping("prs", "prs"),
                        new System.Data.Common.DataColumnMapping("rate", "rate"),
                        new System.Data.Common.DataColumnMapping("taxprice", "taxprice"),
                        new System.Data.Common.DataColumnMapping("mny", "mny"),
                        new System.Data.Common.DataColumnMapping("priceb", "priceb"),
                        new System.Data.Common.DataColumnMapping("taxpriceb", "taxpriceb"),
                        new System.Data.Common.DataColumnMapping("mnyb", "mnyb"),
                        new System.Data.Common.DataColumnMapping("qtyout", "qtyout"),
                        new System.Data.Common.DataColumnMapping("qtyin", "qtyin"),
                        new System.Data.Common.DataColumnMapping("esdate", "esdate"),
                        new System.Data.Common.DataColumnMapping("esdate1", "esdate1"),
                        new System.Data.Common.DataColumnMapping("esdate2", "esdate2"),
                        new System.Data.Common.DataColumnMapping("stkqtyflag", "stkqtyflag"),
                        new System.Data.Common.DataColumnMapping("memo", "memo"),
                        new System.Data.Common.DataColumnMapping("lowzero", "lowzero"),
                        new System.Data.Common.DataColumnMapping("bomid", "bomid"),
                        new System.Data.Common.DataColumnMapping("bomrec", "bomrec"),
                        new System.Data.Common.DataColumnMapping("recordno", "recordno"),
                        new System.Data.Common.DataColumnMapping("sltflag", "sltflag"),
                        new System.Data.Common.DataColumnMapping("extflag", "extflag"),
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
                        new System.Data.Common.DataColumnMapping("stName", "stName"),
                        new System.Data.Common.DataColumnMapping("qtyNotOut", "qtyNotOut"),
                        new System.Data.Common.DataColumnMapping("qtyNotInStk", "qtyNotInStk"),
                        new System.Data.Common.DataColumnMapping("Punit", "Punit"),
                        new System.Data.Common.DataColumnMapping("Pqty", "Pqty"),
                        new System.Data.Common.DataColumnMapping("mqty", "mqty"),
                        new System.Data.Common.DataColumnMapping("munit", "munit"),
                        new System.Data.Common.DataColumnMapping("mlong", "mlong"),
                        new System.Data.Common.DataColumnMapping("mwidth1", "mwidth1"),
                        new System.Data.Common.DataColumnMapping("mwidth2", "mwidth2"),
                        new System.Data.Common.DataColumnMapping("mwidth3", "mwidth3"),
                        new System.Data.Common.DataColumnMapping("mwidth4", "mwidth4"),
                        new System.Data.Common.DataColumnMapping("mformula", "mformula"),
                        new System.Data.Common.DataColumnMapping("Pformula", "Pformula")})});
            this.daD.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(7, 23);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "訂單憑證";
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(243, 23);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(72, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "客戶編號";
            // 
            // orno
            // 
            this.orno.AllowGrayBackColor = false;
            this.orno.AllowResize = true;
            this.orno.Font = new System.Drawing.Font("細明體", 12F);
            this.orno.Location = new System.Drawing.Point(85, 20);
            this.orno.MaxLength = 16;
            this.orno.Name = "orno";
            this.orno.oLen = 0;
            this.orno.Size = new System.Drawing.Size(135, 27);
            this.orno.TabIndex = 2;
            this.orno.TextChanged += new System.EventHandler(this.orno_TextChanged); 
            // 
            // qcuno
            // 
            this.qcuno.AllowGrayBackColor = false;
            this.qcuno.AllowResize = true;
            this.qcuno.Font = new System.Drawing.Font("細明體", 12F);
            this.qcuno.Location = new System.Drawing.Point(321, 20);
            this.qcuno.MaxLength = 10;
            this.qcuno.Name = "qcuno";
            this.qcuno.oLen = 0;
            this.qcuno.Size = new System.Drawing.Size(87, 27);
            this.qcuno.TabIndex = 2;
            this.qcuno.TextChanged += new System.EventHandler(this.qcuno_TextChanged); 
            // 
            // btnCKall
            // 
            this.btnCKall.AutoSize = true;
            this.btnCKall.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCKall.Location = new System.Drawing.Point(422, 15);
            this.btnCKall.Name = "btnCKall";
            this.btnCKall.Size = new System.Drawing.Size(137, 36);
            this.btnCKall.TabIndex = 3;
            this.btnCKall.Text = "F2：全選";
            this.btnCKall.UseVisualStyleBackColor = true;
            this.btnCKall.Click += new System.EventHandler(this.btnCKall_Click);
            // 
            // btnCKnull
            // 
            this.btnCKnull.AutoSize = true;
            this.btnCKnull.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCKnull.Location = new System.Drawing.Point(561, 15);
            this.btnCKnull.Name = "btnCKnull";
            this.btnCKnull.Size = new System.Drawing.Size(137, 36);
            this.btnCKnull.TabIndex = 3;
            this.btnCKnull.Text = "F3：取消";
            this.btnCKnull.UseVisualStyleBackColor = true;
            this.btnCKnull.Click += new System.EventHandler(this.btnCKnull_Click);
            // 
            // btnGet
            // 
            this.btnGet.AutoSize = true;
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(700, 15);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(137, 36);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "F9：取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCancel.Location = new System.Drawing.Point(839, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 36);
            this.btnCancel.TabIndex = 3;
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
            this.訂單憑證,
            this.訂單日期,
            this.客戶編號,
            this.客戶名稱,
            this.稅別,
            this.訂單總額,
            this.訂單人員});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(1, 57);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(985, 285);
            this.dataGridViewT1.TabIndex = 4;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            this.dataGridViewT1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewT1_RowStateChanged);
            // 
            // 訂單憑證
            // 
            this.訂單憑證.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.訂單憑證.DataPropertyName = "orno";
            this.訂單憑證.HeaderText = "訂單憑證";
            this.訂單憑證.MaxInputLength = 16;
            this.訂單憑證.Name = "訂單憑證";
            this.訂單憑證.ReadOnly = true;
            this.訂單憑證.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.訂單憑證.Width = 141;
            // 
            // 訂單日期
            // 
            this.訂單日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.訂單日期.HeaderText = "訂單日期";
            this.訂單日期.MaxInputLength = 10;
            this.訂單日期.Name = "訂單日期";
            this.訂單日期.ReadOnly = true;
            this.訂單日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.訂單日期.Width = 93;
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
            // 客戶名稱
            // 
            this.客戶名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.客戶名稱.DataPropertyName = "cuname1";
            this.客戶名稱.HeaderText = "客戶名稱";
            this.客戶名稱.MaxInputLength = 10;
            this.客戶名稱.Name = "客戶名稱";
            this.客戶名稱.ReadOnly = true;
            this.客戶名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.客戶名稱.Width = 93;
            // 
            // 稅別
            // 
            this.稅別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅別.DataPropertyName = "x3name";
            this.稅別.HeaderText = "稅別";
            this.稅別.MaxInputLength = 10;
            this.稅別.Name = "稅別";
            this.稅別.ReadOnly = true;
            this.稅別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅別.Width = 93;
            // 
            // 訂單總額
            // 
            this.訂單總額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.訂單總額.DataPropertyName = "totmny";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.訂單總額.DefaultCellStyle = dataGridViewCellStyle2;
            this.訂單總額.FirstNum = 0;
            this.訂單總額.HeaderText = "訂單總額";
            this.訂單總額.LastNum = 0;
            this.訂單總額.MarkThousand = false;
            this.訂單總額.MaxInputLength = 16;
            this.訂單總額.Name = "訂單總額";
            this.訂單總額.NullInput = false;
            this.訂單總額.NullValue = "0";
            this.訂單總額.ReadOnly = true;
            this.訂單總額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.訂單總額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.訂單總額.Width = 141;
            // 
            // 訂單人員
            // 
            this.訂單人員.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.訂單人員.DataPropertyName = "emname";
            this.訂單人員.HeaderText = "訂單人員";
            this.訂單人員.MaxInputLength = 10;
            this.訂單人員.Name = "訂單人員";
            this.訂單人員.ReadOnly = true;
            this.訂單人員.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.訂單人員.Width = 93;
            // 
            // dataGridViewT2
            // 
            this.dataGridViewT2.AllowUserToAddRows = false;
            this.dataGridViewT2.AllowUserToDeleteRows = false;
            this.dataGridViewT2.AllowUserToOrderColumns = true;
            this.dataGridViewT2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewT2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewT2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewT2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.點選,
            this.產品編號,
            this.品名規格,
            this.數量,
            this.單位,
            this.單價,
            this.折數,
            this.稅前售價,
            this.稅前金額});
            this.dataGridViewT2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT2.EnableHeadersVisualStyles = false;
            this.dataGridViewT2.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT2.ISDocument = false;
            this.dataGridViewT2.Location = new System.Drawing.Point(1, 348);
            this.dataGridViewT2.MultiSelect = false;
            this.dataGridViewT2.Name = "dataGridViewT2";
            this.dataGridViewT2.ReadOnly = true;
            this.dataGridViewT2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT2.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewT2.RowHeadersWidth = 20;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT2.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewT2.RowTemplate.Height = 24;
            this.dataGridViewT2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT2.ShowCellToolTips = false;
            this.dataGridViewT2.Size = new System.Drawing.Size(985, 276);
            this.dataGridViewT2.TabIndex = 5;
            this.dataGridViewT2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT2_CellClick);
            // 
            // 點選
            // 
            this.點選.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.點選.HeaderText = "點選";
            this.點選.Name = "點選";
            this.點選.ReadOnly = true;
            this.點選.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.數量.DataPropertyName = "qty";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.數量.DefaultCellStyle = dataGridViewCellStyle6;
            this.數量.FirstNum = 0;
            this.數量.HeaderText = "數量";
            this.數量.LastNum = 0;
            this.數量.MarkThousand = false;
            this.數量.MaxInputLength = 11;
            this.數量.Name = "數量";
            this.數量.NullInput = false;
            this.數量.NullValue = "0";
            this.數量.ReadOnly = true;
            this.數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.數量.Width = 101;
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
            // 單價
            // 
            this.單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單價.DataPropertyName = "price";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單價.DefaultCellStyle = dataGridViewCellStyle7;
            this.單價.FirstNum = 0;
            this.單價.HeaderText = "單價";
            this.單價.LastNum = 0;
            this.單價.MarkThousand = false;
            this.單價.MaxInputLength = 12;
            this.單價.Name = "單價";
            this.單價.NullInput = false;
            this.單價.NullValue = "0";
            this.單價.ReadOnly = true;
            this.單價.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單價.Width = 109;
            // 
            // 折數
            // 
            this.折數.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折數.DataPropertyName = "prs";
            this.折數.HeaderText = "折數";
            this.折數.MaxInputLength = 5;
            this.折數.Name = "折數";
            this.折數.ReadOnly = true;
            this.折數.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.折數.Width = 53;
            // 
            // 稅前售價
            // 
            this.稅前售價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅前售價.DataPropertyName = "taxprice";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.稅前售價.DefaultCellStyle = dataGridViewCellStyle8;
            this.稅前售價.FirstNum = 0;
            this.稅前售價.HeaderText = "稅前售價";
            this.稅前售價.LastNum = 0;
            this.稅前售價.MarkThousand = false;
            this.稅前售價.MaxInputLength = 16;
            this.稅前售價.Name = "稅前售價";
            this.稅前售價.NullInput = false;
            this.稅前售價.NullValue = "0";
            this.稅前售價.ReadOnly = true;
            this.稅前售價.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.稅前售價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅前售價.Width = 141;
            // 
            // 稅前金額
            // 
            this.稅前金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.稅前金額.DataPropertyName = "mny";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.稅前金額.DefaultCellStyle = dataGridViewCellStyle9;
            this.稅前金額.FirstNum = 0;
            this.稅前金額.HeaderText = "稅前金額";
            this.稅前金額.LastNum = 0;
            this.稅前金額.MarkThousand = false;
            this.稅前金額.MaxInputLength = 16;
            this.稅前金額.Name = "稅前金額";
            this.稅前金額.NullInput = false;
            this.稅前金額.NullValue = "0";
            this.稅前金額.ReadOnly = true;
            this.稅前金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.稅前金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.稅前金額.Width = 141;
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
            // FrmOrderToLend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.dataGridViewT2);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnCKnull);
            this.Controls.Add(this.btnCKall);
            this.Controls.Add(this.qcuno);
            this.Controls.Add(this.orno);
            this.Controls.Add(this.labelT2);
            this.Controls.Add(this.labelT1);
            this.Name = "FrmOrderToLend";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmOrderToLend_Load);
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
        private JE.MyControl.TextBoxT orno;
        private JE.MyControl.TextBoxT qcuno;
        private JE.MyControl.ButtonSmallT btnCKall;
        private JE.MyControl.ButtonSmallT btnCKnull;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnCancel;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.DataGridViewT dataGridViewT2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 點選;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private JE.MyControl.DataGridViewTextNumberT 數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 單價;
        private System.Windows.Forms.DataGridViewTextBoxColumn 折數;
        private JE.MyControl.DataGridViewTextNumberT 稅前售價;
        private JE.MyControl.DataGridViewTextNumberT 稅前金額;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 訂單憑證;
        private System.Windows.Forms.DataGridViewTextBoxColumn 訂單日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 稅別;
        private JE.MyControl.DataGridViewTextNumberT 訂單總額;
        private System.Windows.Forms.DataGridViewTextBoxColumn 訂單人員;
    }
}