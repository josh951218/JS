namespace S_61.subMenuFm_1
{
    partial class FrmQuoteToSale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuoteToSale));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn = new System.Data.SqlClient.SqlConnection();
            this.daM = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daD = new System.Data.SqlClient.SqlDataAdapter();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.報價單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.報價日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶簡稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.稅別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.報價總額 = new JE.MyControl.DataGridViewTextNumberT();
            this.業務 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewT2 = new JE.MyControl.DataGridViewT();
            this.點選 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單價 = new JE.MyControl.DataGridViewTextNumberT();
            this.btnCKall = new JE.MyControl.ButtonSmallT();
            this.btnCKnull = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            this.btnCancel = new JE.MyControl.ButtonSmallT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.qNo = new JE.MyControl.TextBoxT();
            this.cNo = new JE.MyControl.TextBoxT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).BeginInit();
            this.SuspendLayout();
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.cn;
            // 
            // cn
            // 
            this.cn.ConnectionString = "Data Source=.;Initial Catalog=STOCK;Integrated Security=True";
            this.cn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daM
            // 
            this.daM.SelectCommand = this.sqlSelectCommand1;
            this.daM.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "XX03", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("quno", "quno"),
                        new System.Data.Common.DataColumnMapping("qudate1", "qudate1"),
                        new System.Data.Common.DataColumnMapping("qudate2", "qudate2"),
                        new System.Data.Common.DataColumnMapping("qudates1", "qudates1"),
                        new System.Data.Common.DataColumnMapping("qudates2", "qudates2"),
                        new System.Data.Common.DataColumnMapping("cono", "cono"),
                        new System.Data.Common.DataColumnMapping("coname1", "coname1"),
                        new System.Data.Common.DataColumnMapping("coname2", "coname2"),
                        new System.Data.Common.DataColumnMapping("cuno", "cuno"),
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
                        new System.Data.Common.DataColumnMapping("qupayment", "qupayment"),
                        new System.Data.Common.DataColumnMapping("quperiod", "quperiod"),
                        new System.Data.Common.DataColumnMapping("qumemo", "qumemo"),
                        new System.Data.Common.DataColumnMapping("recordno", "recordno"),
                        new System.Data.Common.DataColumnMapping("UsrNo", "UsrNo"),
                        new System.Data.Common.DataColumnMapping("QuDate", "QuDate"),
                        new System.Data.Common.DataColumnMapping("QuDates", "QuDates"),
                        new System.Data.Common.DataColumnMapping("AppScNo", "AppScNo"),
                        new System.Data.Common.DataColumnMapping("AppDate", "AppDate"),
                        new System.Data.Common.DataColumnMapping("EdtScNo", "EdtScNo"),
                        new System.Data.Common.DataColumnMapping("EdtDate", "EdtDate"),
                        new System.Data.Common.DataColumnMapping("qumemo1", "qumemo1"),
                        new System.Data.Common.DataColumnMapping("cuname2", "cuname2"),
                        new System.Data.Common.DataColumnMapping("cuname1", "cuname1"),
                        new System.Data.Common.DataColumnMapping("cuinvoname", "cuinvoname"),
                        new System.Data.Common.DataColumnMapping("cuchkname", "cuchkname"),
                        new System.Data.Common.DataColumnMapping("cuxa1no", "cuxa1no"),
                        new System.Data.Common.DataColumnMapping("cupareno", "cupareno"),
                        new System.Data.Common.DataColumnMapping("cucono", "cucono"),
                        new System.Data.Common.DataColumnMapping("cuime", "cuime"),
                        new System.Data.Common.DataColumnMapping("cux1no", "cux1no"),
                        new System.Data.Common.DataColumnMapping("cuemno1", "cuemno1"),
                        new System.Data.Common.DataColumnMapping("cuper1", "cuper1"),
                        new System.Data.Common.DataColumnMapping("cuper2", "cuper2"),
                        new System.Data.Common.DataColumnMapping("cuper", "cuper"),
                        new System.Data.Common.DataColumnMapping("cutel1", "cutel1"),
                        new System.Data.Common.DataColumnMapping("cutel2", "cutel2"),
                        new System.Data.Common.DataColumnMapping("cufax1", "cufax1"),
                        new System.Data.Common.DataColumnMapping("cuatel1", "cuatel1"),
                        new System.Data.Common.DataColumnMapping("cuatel2", "cuatel2"),
                        new System.Data.Common.DataColumnMapping("cuatel3", "cuatel3"),
                        new System.Data.Common.DataColumnMapping("cubbc", "cubbc"),
                        new System.Data.Common.DataColumnMapping("cuaddr1", "cuaddr1"),
                        new System.Data.Common.DataColumnMapping("cur1", "cur1"),
                        new System.Data.Common.DataColumnMapping("cuaddr2", "cuaddr2"),
                        new System.Data.Common.DataColumnMapping("cur2", "cur2"),
                        new System.Data.Common.DataColumnMapping("cuaddr3", "cuaddr3"),
                        new System.Data.Common.DataColumnMapping("cur3", "cur3"),
                        new System.Data.Common.DataColumnMapping("cuslevel", "cuslevel"),
                        new System.Data.Common.DataColumnMapping("cudisc", "cudisc"),
                        new System.Data.Common.DataColumnMapping("cuemail", "cuemail"),
                        new System.Data.Common.DataColumnMapping("cuwww", "cuwww"),
                        new System.Data.Common.DataColumnMapping("cux2no", "cux2no"),
                        new System.Data.Common.DataColumnMapping("cuuno", "cuuno"),
                        new System.Data.Common.DataColumnMapping("cux3no", "cux3no"),
                        new System.Data.Common.DataColumnMapping("cux4no", "cux4no"),
                        new System.Data.Common.DataColumnMapping("cucredit", "cucredit"),
                        new System.Data.Common.DataColumnMapping("cuengname", "cuengname"),
                        new System.Data.Common.DataColumnMapping("cuengaddr", "cuengaddr"),
                        new System.Data.Common.DataColumnMapping("cuengr1", "cuengr1"),
                        new System.Data.Common.DataColumnMapping("cumemo1", "cumemo1"),
                        new System.Data.Common.DataColumnMapping("cumemo2", "cumemo2"),
                        new System.Data.Common.DataColumnMapping("cux5no", "cux5no"),
                        new System.Data.Common.DataColumnMapping("cuarea", "cuarea"),
                        new System.Data.Common.DataColumnMapping("cuudf1", "cuudf1"),
                        new System.Data.Common.DataColumnMapping("cuudf2", "cuudf2"),
                        new System.Data.Common.DataColumnMapping("cuudf3", "cuudf3"),
                        new System.Data.Common.DataColumnMapping("cuudf4", "cuudf4"),
                        new System.Data.Common.DataColumnMapping("cuudf5", "cuudf5"),
                        new System.Data.Common.DataColumnMapping("cuudf6", "cuudf6"),
                        new System.Data.Common.DataColumnMapping("cudate", "cudate"),
                        new System.Data.Common.DataColumnMapping("cudate1", "cudate1"),
                        new System.Data.Common.DataColumnMapping("cudate2", "cudate2"),
                        new System.Data.Common.DataColumnMapping("culastday", "culastday"),
                        new System.Data.Common.DataColumnMapping("culastday1", "culastday1"),
                        new System.Data.Common.DataColumnMapping("culastday2", "culastday2"),
                        new System.Data.Common.DataColumnMapping("cufirreceiv", "cufirreceiv"),
                        new System.Data.Common.DataColumnMapping("cusparercv", "cusparercv"),
                        new System.Data.Common.DataColumnMapping("cureceiv", "cureceiv"),
                        new System.Data.Common.DataColumnMapping("cufirrcvpar", "cufirrcvpar"),
                        new System.Data.Common.DataColumnMapping("cufiradvamt", "cufiradvamt"),
                        new System.Data.Common.DataColumnMapping("cuadvamt", "cuadvamt"),
                        new System.Data.Common.DataColumnMapping("cunote", "cunote"),
                        new System.Data.Common.DataColumnMapping("cubirth", "cubirth"),
                        new System.Data.Common.DataColumnMapping("cubirth1", "cubirth1"),
                        new System.Data.Common.DataColumnMapping("cubirth2", "cubirth2"),
                        new System.Data.Common.DataColumnMapping("cusex", "cusex"),
                        new System.Data.Common.DataColumnMapping("cublood", "cublood"),
                        new System.Data.Common.DataColumnMapping("cuidno", "cuidno"),
                        new System.Data.Common.DataColumnMapping("ServDate", "ServDate"),
                        new System.Data.Common.DataColumnMapping("BetwDates", "BetwDates"),
                        new System.Data.Common.DataColumnMapping("MsgFlag", "MsgFlag"),
                        new System.Data.Common.DataColumnMapping("CuPause", "CuPause"),
                        new System.Data.Common.DataColumnMapping("CuDatep", "CuDatep"),
                        new System.Data.Common.DataColumnMapping("CuDatep1", "CuDatep1"),
                        new System.Data.Common.DataColumnMapping("CuDatep2", "CuDatep2"),
                        new System.Data.Common.DataColumnMapping("IsTrans", "IsTrans"),
                        new System.Data.Common.DataColumnMapping("CuPoint", "CuPoint"),
                        new System.Data.Common.DataColumnMapping("WebPassWord", "WebPassWord"),
                        new System.Data.Common.DataColumnMapping("SaleFrequency", "SaleFrequency"),
                        new System.Data.Common.DataColumnMapping("WebID", "WebID"),
                        new System.Data.Common.DataColumnMapping("X3Name", "X3Name")})});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "SELECT          *,ItNoUdf = (select  top  1  ItNoUdf  from item where item.itno =" +
    " quoted.itno )\r\nFROM             quoted\r\nWHERE          (quno = @quno)";
            this.sqlSelectCommand2.Connection = this.cn;
            this.sqlSelectCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 16, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Current, "1")});
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.cn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@qudate1", System.Data.SqlDbType.NVarChar, 0, "qudate1"),
            new System.Data.SqlClient.SqlParameter("@qudate2", System.Data.SqlDbType.NVarChar, 0, "qudate2"),
            new System.Data.SqlClient.SqlParameter("@qudates1", System.Data.SqlDbType.NVarChar, 0, "qudates1"),
            new System.Data.SqlClient.SqlParameter("@qudates2", System.Data.SqlDbType.NVarChar, 0, "qudates2"),
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
            new System.Data.SqlClient.SqlParameter("@QuDate", System.Data.SqlDbType.NVarChar, 0, "QuDate"),
            new System.Data.SqlClient.SqlParameter("@QuDates", System.Data.SqlDbType.NVarChar, 0, "QuDates")});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.cn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@quno", System.Data.SqlDbType.NVarChar, 0, "quno"),
            new System.Data.SqlClient.SqlParameter("@qudate1", System.Data.SqlDbType.NVarChar, 0, "qudate1"),
            new System.Data.SqlClient.SqlParameter("@qudate2", System.Data.SqlDbType.NVarChar, 0, "qudate2"),
            new System.Data.SqlClient.SqlParameter("@qudates1", System.Data.SqlDbType.NVarChar, 0, "qudates1"),
            new System.Data.SqlClient.SqlParameter("@qudates2", System.Data.SqlDbType.NVarChar, 0, "qudates2"),
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
            new System.Data.SqlClient.SqlParameter("@QuDate", System.Data.SqlDbType.NVarChar, 0, "QuDate"),
            new System.Data.SqlClient.SqlParameter("@QuDates", System.Data.SqlDbType.NVarChar, 0, "QuDates"),
            new System.Data.SqlClient.SqlParameter("@Original_quid", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quid", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qudate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qudate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qudate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "qudate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qudate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qudate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qudate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "qudate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qudates1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qudates1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qudates1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "qudates1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qudates2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qudates2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qudates2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "qudates2", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_QuDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_QuDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "QuDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_QuDates", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuDates", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_QuDates", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "QuDates", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@quid", System.Data.SqlDbType.Int, 4, "quid")});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.cn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_quid", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quid", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_quno", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_quno", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "quno", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qudate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qudate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qudate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "qudate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qudate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qudate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qudate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "qudate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qudates1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qudates1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qudates1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "qudates1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_qudates2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "qudates2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_qudates2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "qudates2", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_QuDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_QuDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "QuDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_QuDates", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuDates", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_QuDates", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "QuDates", System.Data.DataRowVersion.Original, null)});
            // 
            // daD
            // 
            this.daD.DeleteCommand = this.sqlDeleteCommand2;
            this.daD.InsertCommand = this.sqlInsertCommand2;
            this.daD.SelectCommand = this.sqlSelectCommand2;
            this.daD.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "quoted", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("quid", "quid"),
                        new System.Data.Common.DataColumnMapping("quno", "quno"),
                        new System.Data.Common.DataColumnMapping("qudate1", "qudate1"),
                        new System.Data.Common.DataColumnMapping("qudate2", "qudate2"),
                        new System.Data.Common.DataColumnMapping("qudates1", "qudates1"),
                        new System.Data.Common.DataColumnMapping("qudates2", "qudates2"),
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
                        new System.Data.Common.DataColumnMapping("QuDate", "QuDate"),
                        new System.Data.Common.DataColumnMapping("QuDates", "QuDates")})});
            this.daD.UpdateCommand = this.sqlUpdateCommand2;
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
            this.報價單號,
            this.報價日期,
            this.客戶編號,
            this.客戶簡稱,
            this.稅別,
            this.報價總額,
            this.業務});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(1, 47);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1008, 286);
            this.dataGridViewT1.TabIndex = 11;
            this.dataGridViewT1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellDoubleClick);
            this.dataGridViewT1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewT1_RowStateChanged);
            // 
            // 報價單號
            // 
            this.報價單號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.報價單號.DataPropertyName = "quno";
            this.報價單號.HeaderText = "報價單號";
            this.報價單號.MaxInputLength = 16;
            this.報價單號.Name = "報價單號";
            this.報價單號.ReadOnly = true;
            this.報價單號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.報價單號.Width = 141;
            // 
            // 報價日期
            // 
            this.報價日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.報價日期.HeaderText = "報價日期";
            this.報價日期.MaxInputLength = 10;
            this.報價日期.Name = "報價日期";
            this.報價日期.ReadOnly = true;
            this.報價日期.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.報價日期.Width = 93;
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
            // 報價總額
            // 
            this.報價總額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.報價總額.DataPropertyName = "totmny";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.報價總額.DefaultCellStyle = dataGridViewCellStyle2;
            this.報價總額.FirstNum = 0;
            this.報價總額.HeaderText = "報價總額";
            this.報價總額.LastNum = 0;
            this.報價總額.MarkThousand = false;
            this.報價總額.MaxInputLength = 16;
            this.報價總額.Name = "報價總額";
            this.報價總額.NullInput = false;
            this.報價總額.NullValue = "0";
            this.報價總額.ReadOnly = true;
            this.報價總額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.報價總額.Width = 141;
            // 
            // 業務
            // 
            this.業務.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.業務.DataPropertyName = "emname";
            this.業務.HeaderText = "業務";
            this.業務.MaxInputLength = 10;
            this.業務.Name = "業務";
            this.業務.ReadOnly = true;
            this.業務.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.業務.Width = 93;
            // 
            // dataGridViewT2
            // 
            this.dataGridViewT2.AllowUserToAddRows = false;
            this.dataGridViewT2.AllowUserToDeleteRows = false;
            this.dataGridViewT2.AllowUserToOrderColumns = true;
            this.dataGridViewT2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewT2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewT2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewT2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewT2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.點選,
            this.產品編號,
            this.品名規格,
            this.單位,
            this.數量,
            this.單價});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT2.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewT2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT2.EnableHeadersVisualStyles = false;
            this.dataGridViewT2.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT2.ISDocument = false;
            this.dataGridViewT2.Location = new System.Drawing.Point(1, 337);
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
            this.dataGridViewT2.Size = new System.Drawing.Size(1008, 286);
            this.dataGridViewT2.TabIndex = 12;
            this.dataGridViewT2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT2_CellClick);
            // 
            // 點選
            // 
            this.點選.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.點選.HeaderText = "點選";
            this.點選.MaxInputLength = 6;
            this.點選.Name = "點選";
            this.點選.ReadOnly = true;
            this.點選.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.點選.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.點選.Width = 61;
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
            // 單位
            // 
            this.單位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位.DataPropertyName = "itunit";
            this.單位.HeaderText = "單位";
            this.單位.MaxInputLength = 8;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 77;
            // 
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.數量.DataPropertyName = "qty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.數量.DefaultCellStyle = dataGridViewCellStyle7;
            this.數量.FirstNum = 0;
            this.數量.HeaderText = "數量";
            this.數量.LastNum = 0;
            this.數量.MarkThousand = false;
            this.數量.MaxInputLength = 16;
            this.數量.Name = "數量";
            this.數量.NullInput = false;
            this.數量.NullValue = "0";
            this.數量.ReadOnly = true;
            this.數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.數量.Width = 141;
            // 
            // 單價
            // 
            this.單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單價.DataPropertyName = "price";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單價.DefaultCellStyle = dataGridViewCellStyle8;
            this.單價.FirstNum = 0;
            this.單價.HeaderText = "單價";
            this.單價.LastNum = 0;
            this.單價.MarkThousand = false;
            this.單價.MaxInputLength = 16;
            this.單價.Name = "單價";
            this.單價.NullInput = false;
            this.單價.NullValue = "0";
            this.單價.ReadOnly = true;
            this.單價.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單價.Width = 141;
            // 
            // btnCKall
            // 
            this.btnCKall.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCKall.Location = new System.Drawing.Point(441, 7);
            this.btnCKall.Name = "btnCKall";
            this.btnCKall.Size = new System.Drawing.Size(131, 34);
            this.btnCKall.TabIndex = 3;
            this.btnCKall.Text = "F2:全選";
            this.btnCKall.UseVisualStyleBackColor = true;
            this.btnCKall.Click += new System.EventHandler(this.btnCKall_Click);
            // 
            // btnCKnull
            // 
            this.btnCKnull.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCKnull.Location = new System.Drawing.Point(578, 7);
            this.btnCKnull.Name = "btnCKnull";
            this.btnCKnull.Size = new System.Drawing.Size(131, 34);
            this.btnCKnull.TabIndex = 4;
            this.btnCKnull.Text = "F3:取消";
            this.btnCKnull.UseVisualStyleBackColor = true;
            this.btnCKnull.Click += new System.EventHandler(this.btnCKnull_Click);
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(715, 7);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(131, 34);
            this.btnGet.TabIndex = 5;
            this.btnGet.Text = "F9:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCancel.Location = new System.Drawing.Point(852, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(131, 34);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "F4:放棄";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(259, 16);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "客戶編號";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(28, 16);
            this.lblT2.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "報價單號";
            // 
            // qNo
            // 
            this.qNo.AllowGrayBackColor = false;
            this.qNo.AllowResize = true;
            this.qNo.Font = new System.Drawing.Font("細明體", 12F);
            this.qNo.Location = new System.Drawing.Point(105, 11);
            this.qNo.MaxLength = 16;
            this.qNo.Name = "qNo";
            this.qNo.oLen = 0;
            this.qNo.Size = new System.Drawing.Size(135, 27);
            this.qNo.TabIndex = 1;
            this.qNo.TextChanged += new System.EventHandler(this.qNo_TextChanged);
            // 
            // cNo
            // 
            this.cNo.AllowGrayBackColor = false;
            this.cNo.AllowResize = true;
            this.cNo.Font = new System.Drawing.Font("細明體", 12F);
            this.cNo.Location = new System.Drawing.Point(335, 11);
            this.cNo.MaxLength = 10;
            this.cNo.Name = "cNo";
            this.cNo.oLen = 0;
            this.cNo.Size = new System.Drawing.Size(87, 27);
            this.cNo.TabIndex = 2;
            this.cNo.TextChanged += new System.EventHandler(this.qNo_TextChanged);
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
            // FrmQuoteToSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.btnCKall);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.btnCKnull);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dataGridViewT2);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.cNo);
            this.Controls.Add(this.qNo);
            this.Name = "FrmQuoteToSale";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmQuoteToSale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection cn;
        private System.Data.SqlClient.SqlDataAdapter daM;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlDataAdapter daD;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.TextBoxT qNo;
        private JE.MyControl.ButtonSmallT btnCKall;
        private JE.MyControl.ButtonSmallT btnCKnull;
        private JE.MyControl.ButtonSmallT btnGet;
        private JE.MyControl.ButtonSmallT btnCancel;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.TextBoxT cNo;
        private JE.MyControl.DataGridViewT dataGridViewT2; 
        private System.Windows.Forms.DataGridViewTextBoxColumn 報價單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 報價日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶簡稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 稅別;
        private JE.MyControl.DataGridViewTextNumberT 報價總額;
        private System.Windows.Forms.DataGridViewTextBoxColumn 業務;
        private System.Windows.Forms.DataGridViewTextBoxColumn 點選;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 數量;
        private JE.MyControl.DataGridViewTextNumberT 單價;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}