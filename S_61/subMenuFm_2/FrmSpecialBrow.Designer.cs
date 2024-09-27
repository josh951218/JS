namespace S_61.subMenuFm_2
{
    partial class FrmSpecialBrow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpecialBrow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.da = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.da1 = new System.Data.SqlClient.SqlDataAdapter();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.ItNo = new JE.MyControl.TextBoxT();
            this.EmNo = new JE.MyControl.TextBoxT();
            this.EmName = new JE.MyControl.TextBoxT();
            this.Stock = new JE.MyControl.TextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.SpMemo = new JE.MyControl.TextBoxT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.特價單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.起始日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.終止日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.折數 = new JE.MyControl.DataGridViewTextNumberT();
            this.金額 = new JE.MyControl.DataGridViewTextNumberT();
            this.紅利 = new JE.MyControl.DataGridViewTextNumberT();
            this.特價取用方式 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.包裝數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.制單人員 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.備註說明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDate1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EDate1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Q2 = new JE.MyControl.ButtonSmallT();
            this.Q3 = new JE.MyControl.ButtonSmallT();
            this.Q4 = new JE.MyControl.ButtonSmallT();
            this.Q5 = new JE.MyControl.ButtonSmallT();
            this.Q6 = new JE.MyControl.ButtonSmallT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.lblT8 = new JE.MyControl.LabelT();
            this.tEDate = new JE.MyControl.TextBoxT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.tSpNo = new JE.MyControl.TextBoxT();
            this.tSDate = new JE.MyControl.TextBoxT();
            this.tItNo = new JE.MyControl.TextBoxT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.tMemo = new JE.MyControl.TextBoxT();
            this.lblT11 = new JE.MyControl.LabelT();
            this.btnQuery = new JE.MyControl.ButtonSmallT();
            this.btnPicture = new JE.MyControl.ButtonSmallT();
            this.btnDesp = new JE.MyControl.ButtonSmallT();
            this.btnStock = new JE.MyControl.ButtonSmallT();
            this.btnGet = new JE.MyControl.ButtonSmallT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Location = new System.Drawing.Point(0, 626);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "select * from special \r\nwhere (spno =@no)";
            this.sqlSelectCommand1.Connection = this.cn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@no", System.Data.SqlDbType.NVarChar, 20, "SpNo")});
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
            new System.Data.SqlClient.SqlParameter("@SpNo", System.Data.SqlDbType.NVarChar, 0, "SpNo"),
            new System.Data.SqlClient.SqlParameter("@SDate", System.Data.SqlDbType.NVarChar, 0, "SDate"),
            new System.Data.SqlClient.SqlParameter("@SDate1", System.Data.SqlDbType.NVarChar, 0, "SDate1"),
            new System.Data.SqlClient.SqlParameter("@SDate2", System.Data.SqlDbType.NVarChar, 0, "SDate2"),
            new System.Data.SqlClient.SqlParameter("@EDate", System.Data.SqlDbType.NVarChar, 0, "EDate"),
            new System.Data.SqlClient.SqlParameter("@EDate1", System.Data.SqlDbType.NVarChar, 0, "EDate1"),
            new System.Data.SqlClient.SqlParameter("@EDate2", System.Data.SqlDbType.NVarChar, 0, "EDate2"),
            new System.Data.SqlClient.SqlParameter("@EmNo", System.Data.SqlDbType.NVarChar, 0, "EmNo"),
            new System.Data.SqlClient.SqlParameter("@EmName", System.Data.SqlDbType.NVarChar, 0, "EmName"),
            new System.Data.SqlClient.SqlParameter("@SpTrait", System.Data.SqlDbType.NVarChar, 0, "SpTrait"),
            new System.Data.SqlClient.SqlParameter("@SpMemo", System.Data.SqlDbType.NVarChar, 0, "SpMemo"),
            new System.Data.SqlClient.SqlParameter("@RecordNo", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "RecordNo", System.Data.DataRowVersion.Current, null)});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.cn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SpNo", System.Data.SqlDbType.NVarChar, 0, "SpNo"),
            new System.Data.SqlClient.SqlParameter("@SDate", System.Data.SqlDbType.NVarChar, 0, "SDate"),
            new System.Data.SqlClient.SqlParameter("@SDate1", System.Data.SqlDbType.NVarChar, 0, "SDate1"),
            new System.Data.SqlClient.SqlParameter("@SDate2", System.Data.SqlDbType.NVarChar, 0, "SDate2"),
            new System.Data.SqlClient.SqlParameter("@EDate", System.Data.SqlDbType.NVarChar, 0, "EDate"),
            new System.Data.SqlClient.SqlParameter("@EDate1", System.Data.SqlDbType.NVarChar, 0, "EDate1"),
            new System.Data.SqlClient.SqlParameter("@EDate2", System.Data.SqlDbType.NVarChar, 0, "EDate2"),
            new System.Data.SqlClient.SqlParameter("@EmNo", System.Data.SqlDbType.NVarChar, 0, "EmNo"),
            new System.Data.SqlClient.SqlParameter("@EmName", System.Data.SqlDbType.NVarChar, 0, "EmName"),
            new System.Data.SqlClient.SqlParameter("@SpTrait", System.Data.SqlDbType.NVarChar, 0, "SpTrait"),
            new System.Data.SqlClient.SqlParameter("@SpMemo", System.Data.SqlDbType.NVarChar, 0, "SpMemo"),
            new System.Data.SqlClient.SqlParameter("@RecordNo", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "RecordNo", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Original_SpNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EmNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EmNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EmNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EmName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EmName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EmName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpTrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpTrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpTrait", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpTrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpMemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpMemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpMemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpMemo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_RecordNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RecordNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_RecordNo", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "RecordNo", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.cn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_SpNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EmNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EmNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EmNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EmName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EmName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EmName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpTrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpTrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpTrait", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpTrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpMemo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpMemo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpMemo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpMemo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_RecordNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RecordNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_RecordNo", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "RecordNo", System.Data.DataRowVersion.Original, null)});
            // 
            // da
            // 
            this.da.DeleteCommand = this.sqlDeleteCommand1;
            this.da.InsertCommand = this.sqlInsertCommand1;
            this.da.SelectCommand = this.sqlSelectCommand1;
            this.da.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Special", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SpNo", "SpNo"),
                        new System.Data.Common.DataColumnMapping("SDate", "SDate"),
                        new System.Data.Common.DataColumnMapping("SDate1", "SDate1"),
                        new System.Data.Common.DataColumnMapping("SDate2", "SDate2"),
                        new System.Data.Common.DataColumnMapping("EDate", "EDate"),
                        new System.Data.Common.DataColumnMapping("EDate1", "EDate1"),
                        new System.Data.Common.DataColumnMapping("EDate2", "EDate2"),
                        new System.Data.Common.DataColumnMapping("EmNo", "EmNo"),
                        new System.Data.Common.DataColumnMapping("EmName", "EmName"),
                        new System.Data.Common.DataColumnMapping("SpTrait", "SpTrait"),
                        new System.Data.Common.DataColumnMapping("SpMemo", "SpMemo"),
                        new System.Data.Common.DataColumnMapping("RecordNo", "RecordNo")})});
            this.da.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "select * from speciald";
            this.sqlSelectCommand2.Connection = this.cn;
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.cn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SpNo", System.Data.SqlDbType.NVarChar, 0, "SpNo"),
            new System.Data.SqlClient.SqlParameter("@SDate", System.Data.SqlDbType.NVarChar, 0, "SDate"),
            new System.Data.SqlClient.SqlParameter("@SDate1", System.Data.SqlDbType.NVarChar, 0, "SDate1"),
            new System.Data.SqlClient.SqlParameter("@SDate2", System.Data.SqlDbType.NVarChar, 0, "SDate2"),
            new System.Data.SqlClient.SqlParameter("@EDate", System.Data.SqlDbType.NVarChar, 0, "EDate"),
            new System.Data.SqlClient.SqlParameter("@EDate1", System.Data.SqlDbType.NVarChar, 0, "EDate1"),
            new System.Data.SqlClient.SqlParameter("@EDate2", System.Data.SqlDbType.NVarChar, 0, "EDate2"),
            new System.Data.SqlClient.SqlParameter("@EmNo", System.Data.SqlDbType.NVarChar, 0, "EmNo"),
            new System.Data.SqlClient.SqlParameter("@SpTrait", System.Data.SqlDbType.NVarChar, 0, "SpTrait"),
            new System.Data.SqlClient.SqlParameter("@SpTraitName", System.Data.SqlDbType.NVarChar, 0, "SpTraitName"),
            new System.Data.SqlClient.SqlParameter("@ItNo", System.Data.SqlDbType.NVarChar, 0, "ItNo"),
            new System.Data.SqlClient.SqlParameter("@ItName", System.Data.SqlDbType.NVarChar, 0, "ItName"),
            new System.Data.SqlClient.SqlParameter("@ItTrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItTrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ItUnit", System.Data.SqlDbType.NVarChar, 0, "ItUnit"),
            new System.Data.SqlClient.SqlParameter("@Itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Qty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "Price", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ItPrice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ItPrice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Point", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "Point", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "Prs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Memo", System.Data.SqlDbType.NVarChar, 0, "Memo"),
            new System.Data.SqlClient.SqlParameter("@BomID", System.Data.SqlDbType.NVarChar, 0, "BomID"),
            new System.Data.SqlClient.SqlParameter("@BomRec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ReCordNo", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "ReCordNo", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Sltflag", System.Data.SqlDbType.Bit, 0, "Sltflag"),
            new System.Data.SqlClient.SqlParameter("@Extflag", System.Data.SqlDbType.Bit, 0, "Extflag"),
            new System.Data.SqlClient.SqlParameter("@ItDesp1", System.Data.SqlDbType.NVarChar, 0, "ItDesp1"),
            new System.Data.SqlClient.SqlParameter("@ItDesp2", System.Data.SqlDbType.NVarChar, 0, "ItDesp2"),
            new System.Data.SqlClient.SqlParameter("@ItDesp3", System.Data.SqlDbType.NVarChar, 0, "ItDesp3"),
            new System.Data.SqlClient.SqlParameter("@ItDesp4", System.Data.SqlDbType.NVarChar, 0, "ItDesp4"),
            new System.Data.SqlClient.SqlParameter("@ItDesp5", System.Data.SqlDbType.NVarChar, 0, "ItDesp5"),
            new System.Data.SqlClient.SqlParameter("@ItDesp6", System.Data.SqlDbType.NVarChar, 0, "ItDesp6"),
            new System.Data.SqlClient.SqlParameter("@ItDesp7", System.Data.SqlDbType.NVarChar, 0, "ItDesp7"),
            new System.Data.SqlClient.SqlParameter("@ItDesp8", System.Data.SqlDbType.NVarChar, 0, "ItDesp8"),
            new System.Data.SqlClient.SqlParameter("@ItDesp9", System.Data.SqlDbType.NVarChar, 0, "ItDesp9"),
            new System.Data.SqlClient.SqlParameter("@ItDesp10", System.Data.SqlDbType.NVarChar, 0, "ItDesp10")});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.cn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SpNo", System.Data.SqlDbType.NVarChar, 0, "SpNo"),
            new System.Data.SqlClient.SqlParameter("@SDate", System.Data.SqlDbType.NVarChar, 0, "SDate"),
            new System.Data.SqlClient.SqlParameter("@SDate1", System.Data.SqlDbType.NVarChar, 0, "SDate1"),
            new System.Data.SqlClient.SqlParameter("@SDate2", System.Data.SqlDbType.NVarChar, 0, "SDate2"),
            new System.Data.SqlClient.SqlParameter("@EDate", System.Data.SqlDbType.NVarChar, 0, "EDate"),
            new System.Data.SqlClient.SqlParameter("@EDate1", System.Data.SqlDbType.NVarChar, 0, "EDate1"),
            new System.Data.SqlClient.SqlParameter("@EDate2", System.Data.SqlDbType.NVarChar, 0, "EDate2"),
            new System.Data.SqlClient.SqlParameter("@EmNo", System.Data.SqlDbType.NVarChar, 0, "EmNo"),
            new System.Data.SqlClient.SqlParameter("@SpTrait", System.Data.SqlDbType.NVarChar, 0, "SpTrait"),
            new System.Data.SqlClient.SqlParameter("@SpTraitName", System.Data.SqlDbType.NVarChar, 0, "SpTraitName"),
            new System.Data.SqlClient.SqlParameter("@ItNo", System.Data.SqlDbType.NVarChar, 0, "ItNo"),
            new System.Data.SqlClient.SqlParameter("@ItName", System.Data.SqlDbType.NVarChar, 0, "ItName"),
            new System.Data.SqlClient.SqlParameter("@ItTrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItTrait", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ItUnit", System.Data.SqlDbType.NVarChar, 0, "ItUnit"),
            new System.Data.SqlClient.SqlParameter("@Itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Itpkgqty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Qty", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "Price", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ItPrice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ItPrice", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Point", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "Point", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "Prs", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Memo", System.Data.SqlDbType.NVarChar, 0, "Memo"),
            new System.Data.SqlClient.SqlParameter("@BomID", System.Data.SqlDbType.NVarChar, 0, "BomID"),
            new System.Data.SqlClient.SqlParameter("@BomRec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@ReCordNo", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "ReCordNo", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Sltflag", System.Data.SqlDbType.Bit, 0, "Sltflag"),
            new System.Data.SqlClient.SqlParameter("@Extflag", System.Data.SqlDbType.Bit, 0, "Extflag"),
            new System.Data.SqlClient.SqlParameter("@ItDesp1", System.Data.SqlDbType.NVarChar, 0, "ItDesp1"),
            new System.Data.SqlClient.SqlParameter("@ItDesp2", System.Data.SqlDbType.NVarChar, 0, "ItDesp2"),
            new System.Data.SqlClient.SqlParameter("@ItDesp3", System.Data.SqlDbType.NVarChar, 0, "ItDesp3"),
            new System.Data.SqlClient.SqlParameter("@ItDesp4", System.Data.SqlDbType.NVarChar, 0, "ItDesp4"),
            new System.Data.SqlClient.SqlParameter("@ItDesp5", System.Data.SqlDbType.NVarChar, 0, "ItDesp5"),
            new System.Data.SqlClient.SqlParameter("@ItDesp6", System.Data.SqlDbType.NVarChar, 0, "ItDesp6"),
            new System.Data.SqlClient.SqlParameter("@ItDesp7", System.Data.SqlDbType.NVarChar, 0, "ItDesp7"),
            new System.Data.SqlClient.SqlParameter("@ItDesp8", System.Data.SqlDbType.NVarChar, 0, "ItDesp8"),
            new System.Data.SqlClient.SqlParameter("@ItDesp9", System.Data.SqlDbType.NVarChar, 0, "ItDesp9"),
            new System.Data.SqlClient.SqlParameter("@ItDesp10", System.Data.SqlDbType.NVarChar, 0, "ItDesp10"),
            new System.Data.SqlClient.SqlParameter("@Original_SpID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EmNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EmNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EmNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpTrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpTrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpTrait", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpTrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpTraitName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpTraitName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpTraitName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpTraitName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItTrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItTrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItTrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItTrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItUnit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItUnit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItUnit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItUnit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Itpkgqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Itpkgqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Itpkgqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Qty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Qty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Qty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Price", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Price", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "Price", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItPrice", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItPrice", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItPrice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ItPrice", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Point", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Point", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Point", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "Point", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Prs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Prs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "Prs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Memo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Memo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Memo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Memo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BomID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BomID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BomID", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BomID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BomRec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BomRec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ReCordNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReCordNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ReCordNo", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "ReCordNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Sltflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Sltflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Sltflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Sltflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Extflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Extflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Extflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Extflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp3", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp4", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp5", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp6", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp6", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp6", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp6", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp7", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp7", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp7", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp7", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp8", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp8", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp8", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp8", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp9", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp9", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp9", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp9", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp10", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp10", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp10", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp10", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@SpID", System.Data.SqlDbType.Int, 4, "SpID")});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.cn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_SpID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SDate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SDate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SDate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SDate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EDate2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EDate2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EDate2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EDate2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EmNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EmNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EmNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpTrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpTrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpTrait", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpTrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SpTraitName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SpTraitName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SpTraitName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SpTraitName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItName", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItName", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItName", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItTrait", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItTrait", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItTrait", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(1)), ((byte)(0)), "ItTrait", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItUnit", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItUnit", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItUnit", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItUnit", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Itpkgqty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Itpkgqty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Itpkgqty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Itpkgqty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Qty", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Qty", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Qty", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Qty", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Price", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Price", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Price", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "Price", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItPrice", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItPrice", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItPrice", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "ItPrice", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Point", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Point", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Point", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(6)), "Point", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Prs", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Prs", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Prs", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(4)), ((byte)(3)), "Prs", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Memo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Memo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Memo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Memo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BomID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BomID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BomID", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BomID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BomRec", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BomRec", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "BomRec", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ReCordNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReCordNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ReCordNo", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(10)), ((byte)(0)), "ReCordNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Sltflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Sltflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Sltflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Sltflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Extflag", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Extflag", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Extflag", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Extflag", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp1", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp2", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp3", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp4", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp5", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp6", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp6", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp6", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp6", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp7", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp7", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp7", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp7", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp8", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp8", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp8", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp8", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp9", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp9", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp9", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp9", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ItDesp10", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ItDesp10", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ItDesp10", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ItDesp10", System.Data.DataRowVersion.Original, null)});
            // 
            // da1
            // 
            this.da1.DeleteCommand = this.sqlDeleteCommand2;
            this.da1.InsertCommand = this.sqlInsertCommand2;
            this.da1.SelectCommand = this.sqlSelectCommand2;
            this.da1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Speciald", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SpID", "SpID"),
                        new System.Data.Common.DataColumnMapping("SpNo", "SpNo"),
                        new System.Data.Common.DataColumnMapping("SDate", "SDate"),
                        new System.Data.Common.DataColumnMapping("SDate1", "SDate1"),
                        new System.Data.Common.DataColumnMapping("SDate2", "SDate2"),
                        new System.Data.Common.DataColumnMapping("EDate", "EDate"),
                        new System.Data.Common.DataColumnMapping("EDate1", "EDate1"),
                        new System.Data.Common.DataColumnMapping("EDate2", "EDate2"),
                        new System.Data.Common.DataColumnMapping("EmNo", "EmNo"),
                        new System.Data.Common.DataColumnMapping("SpTrait", "SpTrait"),
                        new System.Data.Common.DataColumnMapping("SpTraitName", "SpTraitName"),
                        new System.Data.Common.DataColumnMapping("ItNo", "ItNo"),
                        new System.Data.Common.DataColumnMapping("ItName", "ItName"),
                        new System.Data.Common.DataColumnMapping("ItTrait", "ItTrait"),
                        new System.Data.Common.DataColumnMapping("ItUnit", "ItUnit"),
                        new System.Data.Common.DataColumnMapping("Itpkgqty", "Itpkgqty"),
                        new System.Data.Common.DataColumnMapping("Qty", "Qty"),
                        new System.Data.Common.DataColumnMapping("Price", "Price"),
                        new System.Data.Common.DataColumnMapping("ItPrice", "ItPrice"),
                        new System.Data.Common.DataColumnMapping("Point", "Point"),
                        new System.Data.Common.DataColumnMapping("Prs", "Prs"),
                        new System.Data.Common.DataColumnMapping("Memo", "Memo"),
                        new System.Data.Common.DataColumnMapping("BomID", "BomID"),
                        new System.Data.Common.DataColumnMapping("BomRec", "BomRec"),
                        new System.Data.Common.DataColumnMapping("ReCordNo", "ReCordNo"),
                        new System.Data.Common.DataColumnMapping("Sltflag", "Sltflag"),
                        new System.Data.Common.DataColumnMapping("Extflag", "Extflag"),
                        new System.Data.Common.DataColumnMapping("ItDesp1", "ItDesp1"),
                        new System.Data.Common.DataColumnMapping("ItDesp2", "ItDesp2"),
                        new System.Data.Common.DataColumnMapping("ItDesp3", "ItDesp3"),
                        new System.Data.Common.DataColumnMapping("ItDesp4", "ItDesp4"),
                        new System.Data.Common.DataColumnMapping("ItDesp5", "ItDesp5"),
                        new System.Data.Common.DataColumnMapping("ItDesp6", "ItDesp6"),
                        new System.Data.Common.DataColumnMapping("ItDesp7", "ItDesp7"),
                        new System.Data.Common.DataColumnMapping("ItDesp8", "ItDesp8"),
                        new System.Data.Common.DataColumnMapping("ItDesp9", "ItDesp9"),
                        new System.Data.Common.DataColumnMapping("ItDesp10", "ItDesp10")})});
            this.da1.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // lblT1
            // 
            this.lblT1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(6, 11);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "產品編號";
            // 
            // lblT2
            // 
            this.lblT2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(407, 11);
            this.lblT2.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "制單人員";
            // 
            // lblT3
            // 
            this.lblT3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(749, 11);
            this.lblT3.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "總庫存量";
            // 
            // ItNo
            // 
            this.ItNo.AllowGrayBackColor = true;
            this.ItNo.AllowResize = true;
            this.ItNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ItNo.BackColor = System.Drawing.Color.Silver;
            this.ItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo.Location = new System.Drawing.Point(78, 6);
            this.ItNo.MaxLength = 20;
            this.ItNo.Name = "ItNo";
            this.ItNo.oLen = 0;
            this.ItNo.ReadOnly = true;
            this.ItNo.Size = new System.Drawing.Size(167, 27);
            this.ItNo.TabIndex = 1;
            this.ItNo.TabStop = false;
            // 
            // EmNo
            // 
            this.EmNo.AllowGrayBackColor = true;
            this.EmNo.AllowResize = true;
            this.EmNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.EmNo.BackColor = System.Drawing.Color.Silver;
            this.EmNo.Font = new System.Drawing.Font("細明體", 12F);
            this.EmNo.Location = new System.Drawing.Point(479, 6);
            this.EmNo.MaxLength = 4;
            this.EmNo.Name = "EmNo";
            this.EmNo.oLen = 0;
            this.EmNo.ReadOnly = true;
            this.EmNo.Size = new System.Drawing.Size(39, 27);
            this.EmNo.TabIndex = 2;
            this.EmNo.TabStop = false;
            // 
            // EmName
            // 
            this.EmName.AllowGrayBackColor = true;
            this.EmName.AllowResize = true;
            this.EmName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.EmName.BackColor = System.Drawing.Color.Silver;
            this.EmName.Font = new System.Drawing.Font("細明體", 12F);
            this.EmName.Location = new System.Drawing.Point(524, 6);
            this.EmName.MaxLength = 10;
            this.EmName.Name = "EmName";
            this.EmName.oLen = 0;
            this.EmName.ReadOnly = true;
            this.EmName.Size = new System.Drawing.Size(87, 27);
            this.EmName.TabIndex = 3;
            this.EmName.TabStop = false;
            // 
            // Stock
            // 
            this.Stock.AllowGrayBackColor = true;
            this.Stock.AllowResize = true;
            this.Stock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Stock.BackColor = System.Drawing.Color.Silver;
            this.Stock.Font = new System.Drawing.Font("細明體", 12F);
            this.Stock.Location = new System.Drawing.Point(821, 6);
            this.Stock.MaxLength = 20;
            this.Stock.Name = "Stock";
            this.Stock.oLen = 0;
            this.Stock.ReadOnly = true;
            this.Stock.Size = new System.Drawing.Size(167, 27);
            this.Stock.TabIndex = 4;
            this.Stock.TabStop = false;
            this.Stock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblT4
            // 
            this.lblT4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(6, 44);
            this.lblT4.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "備    註";
            // 
            // Memo
            // 
            this.SpMemo.AllowGrayBackColor = false;
            this.SpMemo.AllowResize = true;
            this.SpMemo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SpMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.SpMemo.Font = new System.Drawing.Font("細明體", 12F);
            this.SpMemo.Location = new System.Drawing.Point(78, 39);
            this.SpMemo.MaxLength = 60;
            this.SpMemo.Name = "Memo";
            this.SpMemo.oLen = 0;
            this.SpMemo.ReadOnly = true;
            this.SpMemo.Size = new System.Drawing.Size(487, 27);
            this.SpMemo.TabIndex = 5;
            this.SpMemo.TabStop = false;
            this.SpMemo.Validating += new System.ComponentModel.CancelEventHandler(this.Memo_Validating);
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
            this.特價單號,
            this.起始日,
            this.終止日,
            this.品名規格,
            this.單位,
            this.數量,
            this.折數,
            this.金額,
            this.紅利,
            this.特價取用方式,
            this.包裝數量,
            this.制單人員,
            this.備註說明,
            this.產品編號,
            this.SPID,
            this.SDate,
            this.SDate1,
            this.EDate,
            this.EDate1});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 72);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 378);
            this.dataGridViewT1.TabIndex = 6;
            this.dataGridViewT1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewT1_RowStateChanged);
            // 
            // 特價單號
            // 
            this.特價單號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.特價單號.DataPropertyName = "spno";
            this.特價單號.HeaderText = "特價單號";
            this.特價單號.MaxInputLength = 14;
            this.特價單號.Name = "特價單號";
            this.特價單號.ReadOnly = true;
            this.特價單號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.特價單號.Width = 125;
            // 
            // 起始日
            // 
            this.起始日.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.起始日.HeaderText = "起始日";
            this.起始日.MaxInputLength = 10;
            this.起始日.Name = "起始日";
            this.起始日.ReadOnly = true;
            this.起始日.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.起始日.Width = 93;
            // 
            // 終止日
            // 
            this.終止日.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.終止日.HeaderText = "終止日";
            this.終止日.MaxInputLength = 10;
            this.終止日.Name = "終止日";
            this.終止日.ReadOnly = true;
            this.終止日.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.終止日.Width = 93;
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
            this.單位.MaxInputLength = 4;
            this.單位.Name = "單位";
            this.單位.ReadOnly = true;
            this.單位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位.Width = 45;
            // 
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.數量.DataPropertyName = "qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.數量.DefaultCellStyle = dataGridViewCellStyle2;
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
            // 折數
            // 
            this.折數.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.折數.DataPropertyName = "prs";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.折數.DefaultCellStyle = dataGridViewCellStyle3;
            this.折數.FirstNum = 0;
            this.折數.HeaderText = "折數";
            this.折數.LastNum = 0;
            this.折數.MarkThousand = false;
            this.折數.MaxInputLength = 5;
            this.折數.Name = "折數";
            this.折數.NullInput = false;
            this.折數.NullValue = "0";
            this.折數.ReadOnly = true;
            this.折數.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.折數.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.折數.Width = 53;
            // 
            // 金額
            // 
            this.金額.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.金額.DataPropertyName = "price";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.金額.DefaultCellStyle = dataGridViewCellStyle4;
            this.金額.FirstNum = 0;
            this.金額.HeaderText = "金額";
            this.金額.LastNum = 0;
            this.金額.MarkThousand = false;
            this.金額.MaxInputLength = 11;
            this.金額.Name = "金額";
            this.金額.NullInput = false;
            this.金額.NullValue = "0";
            this.金額.ReadOnly = true;
            this.金額.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.金額.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.金額.Width = 101;
            // 
            // 紅利
            // 
            this.紅利.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.紅利.DataPropertyName = "point";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.紅利.DefaultCellStyle = dataGridViewCellStyle5;
            this.紅利.FirstNum = 0;
            this.紅利.HeaderText = "紅利";
            this.紅利.LastNum = 0;
            this.紅利.MarkThousand = false;
            this.紅利.MaxInputLength = 11;
            this.紅利.Name = "紅利";
            this.紅利.NullInput = false;
            this.紅利.NullValue = "0";
            this.紅利.ReadOnly = true;
            this.紅利.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.紅利.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.紅利.Visible = false;
            this.紅利.Width = 101;
            // 
            // 特價取用方式
            // 
            this.特價取用方式.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.特價取用方式.DataPropertyName = "sptraitName";
            this.特價取用方式.HeaderText = "特價取用方式";
            this.特價取用方式.MaxInputLength = 20;
            this.特價取用方式.Name = "特價取用方式";
            this.特價取用方式.ReadOnly = true;
            this.特價取用方式.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.特價取用方式.Width = 173;
            // 
            // 包裝數量
            // 
            this.包裝數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量.DataPropertyName = "itpkgqty";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝數量.DefaultCellStyle = dataGridViewCellStyle6;
            this.包裝數量.FirstNum = 0;
            this.包裝數量.HeaderText = "包裝數量";
            this.包裝數量.LastNum = 0;
            this.包裝數量.MarkThousand = false;
            this.包裝數量.MaxInputLength = 11;
            this.包裝數量.Name = "包裝數量";
            this.包裝數量.NullInput = false;
            this.包裝數量.NullValue = "0";
            this.包裝數量.ReadOnly = true;
            this.包裝數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.包裝數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝數量.Width = 101;
            // 
            // 制單人員
            // 
            this.制單人員.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.制單人員.DataPropertyName = "emno";
            this.制單人員.HeaderText = "制單人員";
            this.制單人員.MaxInputLength = 10;
            this.制單人員.Name = "制單人員";
            this.制單人員.ReadOnly = true;
            this.制單人員.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.制單人員.Width = 93;
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
            // 產品編號
            // 
            this.產品編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品編號.DataPropertyName = "itno";
            this.產品編號.HeaderText = "產品編號";
            this.產品編號.MaxInputLength = 20;
            this.產品編號.Name = "產品編號";
            this.產品編號.ReadOnly = true;
            this.產品編號.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.產品編號.Visible = false;
            this.產品編號.Width = 173;
            // 
            // SPID
            // 
            this.SPID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SPID.DataPropertyName = "SPID";
            this.SPID.HeaderText = "SPID";
            this.SPID.MaxInputLength = 10;
            this.SPID.Name = "SPID";
            this.SPID.ReadOnly = true;
            this.SPID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SPID.Visible = false;
            this.SPID.Width = 93;
            // 
            // SDate
            // 
            this.SDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SDate.DataPropertyName = "SDate";
            this.SDate.HeaderText = "SDate";
            this.SDate.MaxInputLength = 10;
            this.SDate.Name = "SDate";
            this.SDate.ReadOnly = true;
            this.SDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SDate.Visible = false;
            this.SDate.Width = 93;
            // 
            // SDate1
            // 
            this.SDate1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SDate1.DataPropertyName = "SDate1";
            this.SDate1.HeaderText = "SDate1";
            this.SDate1.MaxInputLength = 10;
            this.SDate1.Name = "SDate1";
            this.SDate1.ReadOnly = true;
            this.SDate1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SDate1.Visible = false;
            this.SDate1.Width = 93;
            // 
            // EDate
            // 
            this.EDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EDate.DataPropertyName = "EDate";
            this.EDate.HeaderText = "EDate";
            this.EDate.MaxInputLength = 10;
            this.EDate.Name = "EDate";
            this.EDate.ReadOnly = true;
            this.EDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EDate.Visible = false;
            this.EDate.Width = 93;
            // 
            // EDate1
            // 
            this.EDate1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EDate1.DataPropertyName = "EDate1";
            this.EDate1.HeaderText = "EDate1";
            this.EDate1.MaxInputLength = 10;
            this.EDate1.Name = "EDate1";
            this.EDate1.ReadOnly = true;
            this.EDate1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EDate1.Visible = false;
            this.EDate1.Width = 93;
            // 
            // Q2
            // 
            this.Q2.AutoSize = true;
            this.Q2.Font = new System.Drawing.Font("細明體", 12F);
            this.Q2.Location = new System.Drawing.Point(2, 456);
            this.Q2.Name = "Q2";
            this.Q2.Size = new System.Drawing.Size(172, 33);
            this.Q2.TabIndex = 7;
            this.Q2.Text = "f2:特價單號";
            this.Q2.UseVisualStyleBackColor = true;
            this.Q2.Click += new System.EventHandler(this.Q2_Click);
            // 
            // Q3
            // 
            this.Q3.AutoSize = true;
            this.Q3.Font = new System.Drawing.Font("細明體", 12F);
            this.Q3.Location = new System.Drawing.Point(182, 456);
            this.Q3.Name = "Q3";
            this.Q3.Size = new System.Drawing.Size(172, 33);
            this.Q3.TabIndex = 8;
            this.Q3.Text = "f3:特價起始日";
            this.Q3.UseVisualStyleBackColor = true;
            this.Q3.Click += new System.EventHandler(this.Q3_Click);
            // 
            // Q4
            // 
            this.Q4.AutoSize = true;
            this.Q4.Font = new System.Drawing.Font("細明體", 12F);
            this.Q4.Location = new System.Drawing.Point(362, 456);
            this.Q4.Name = "Q4";
            this.Q4.Size = new System.Drawing.Size(172, 33);
            this.Q4.TabIndex = 9;
            this.Q4.Text = "f4:特價終止日";
            this.Q4.UseVisualStyleBackColor = true;
            this.Q4.Click += new System.EventHandler(this.Q4_Click);
            // 
            // Q5
            // 
            this.Q5.AutoSize = true;
            this.Q5.Font = new System.Drawing.Font("細明體", 12F);
            this.Q5.Location = new System.Drawing.Point(542, 456);
            this.Q5.Name = "Q5";
            this.Q5.Size = new System.Drawing.Size(172, 33);
            this.Q5.TabIndex = 10;
            this.Q5.Text = "f5:產品編號";
            this.Q5.UseVisualStyleBackColor = true;
            this.Q5.Click += new System.EventHandler(this.Q5_Click);
            // 
            // Q6
            // 
            this.Q6.AutoSize = true;
            this.Q6.Font = new System.Drawing.Font("細明體", 12F);
            this.Q6.Location = new System.Drawing.Point(722, 456);
            this.Q6.Name = "Q6";
            this.Q6.Size = new System.Drawing.Size(172, 33);
            this.Q6.TabIndex = 12;
            this.Q6.Text = "f6:備註說明";
            this.Q6.UseVisualStyleBackColor = true;
            this.Q6.Click += new System.EventHandler(this.Q6_Click);
            // 
            // lblT9
            // 
            this.lblT9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(383, 536);
            this.lblT9.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(72, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "終 止 日";
            // 
            // lblT8
            // 
            this.lblT8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(383, 500);
            this.lblT8.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(72, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "起 始 日";
            // 
            // tEDate
            // 
            this.tEDate.AllowGrayBackColor = false;
            this.tEDate.AllowResize = true;
            this.tEDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tEDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.tEDate.Font = new System.Drawing.Font("細明體", 12F);
            this.tEDate.Location = new System.Drawing.Point(455, 531);
            this.tEDate.MaxLength = 10;
            this.tEDate.Name = "tEDate";
            this.tEDate.oLen = 0;
            this.tEDate.ReadOnly = true;
            this.tEDate.Size = new System.Drawing.Size(87, 27);
            this.tEDate.TabIndex = 15;
            this.tEDate.TabStop = false;
            this.tEDate.Validating += new System.ComponentModel.CancelEventHandler(this.Sdate_Validating);
            // 
            // lblT5
            // 
            this.lblT5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(153, 502);
            this.lblT5.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(72, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "特價單號";
            // 
            // tSpNo
            // 
            this.tSpNo.AllowGrayBackColor = false;
            this.tSpNo.AllowResize = true;
            this.tSpNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tSpNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.tSpNo.Font = new System.Drawing.Font("細明體", 12F);
            this.tSpNo.Location = new System.Drawing.Point(225, 497);
            this.tSpNo.MaxLength = 12;
            this.tSpNo.Name = "tSpNo";
            this.tSpNo.oLen = 0;
            this.tSpNo.ReadOnly = true;
            this.tSpNo.Size = new System.Drawing.Size(103, 27);
            this.tSpNo.TabIndex = 13;
            this.tSpNo.TabStop = false;
            // 
            // tSDate
            // 
            this.tSDate.AllowGrayBackColor = false;
            this.tSDate.AllowResize = true;
            this.tSDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tSDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.tSDate.Font = new System.Drawing.Font("細明體", 12F);
            this.tSDate.Location = new System.Drawing.Point(455, 495);
            this.tSDate.MaxLength = 10;
            this.tSDate.Name = "tSDate";
            this.tSDate.oLen = 0;
            this.tSDate.ReadOnly = true;
            this.tSDate.Size = new System.Drawing.Size(87, 27);
            this.tSDate.TabIndex = 14;
            this.tSDate.TabStop = false;
            this.tSDate.Validating += new System.ComponentModel.CancelEventHandler(this.Sdate_Validating);
            // 
            // tItNo
            // 
            this.tItNo.AllowGrayBackColor = false;
            this.tItNo.AllowResize = true;
            this.tItNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tItNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.tItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.tItNo.Location = new System.Drawing.Point(690, 497);
            this.tItNo.MaxLength = 20;
            this.tItNo.Name = "tItNo";
            this.tItNo.oLen = 0;
            this.tItNo.ReadOnly = true;
            this.tItNo.Size = new System.Drawing.Size(167, 27);
            this.tItNo.TabIndex = 18;
            this.tItNo.TabStop = false;
            this.tItNo.DoubleClick += new System.EventHandler(this.tItNo_DoubleClick);
            // 
            // lblT6
            // 
            this.lblT6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(618, 502);
            this.lblT6.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(72, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "產品編號";
            // 
            // tMemo
            // 
            this.tMemo.AllowGrayBackColor = false;
            this.tMemo.AllowResize = true;
            this.tMemo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.tMemo.Font = new System.Drawing.Font("細明體", 12F);
            this.tMemo.Location = new System.Drawing.Point(690, 533);
            this.tMemo.MaxLength = 20;
            this.tMemo.Name = "tMemo";
            this.tMemo.oLen = 0;
            this.tMemo.ReadOnly = true;
            this.tMemo.Size = new System.Drawing.Size(167, 27);
            this.tMemo.TabIndex = 19;
            this.tMemo.TabStop = false;
            // 
            // lblT11
            // 
            this.lblT11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblT11.AutoSize = true;
            this.lblT11.BackColor = System.Drawing.Color.Transparent;
            this.lblT11.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT11.Location = new System.Drawing.Point(618, 538);
            this.lblT11.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT11.Name = "lblT11";
            this.lblT11.Size = new System.Drawing.Size(72, 16);
            this.lblT11.TabIndex = 0;
            this.lblT11.Text = "備註說明";
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(98, 571);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(163, 52);
            this.btnQuery.TabIndex = 20;
            this.btnQuery.Text = "F3:查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnPicture
            // 
            this.btnPicture.Font = new System.Drawing.Font("細明體", 12F);
            this.btnPicture.Location = new System.Drawing.Point(261, 571);
            this.btnPicture.Name = "btnPicture";
            this.btnPicture.Size = new System.Drawing.Size(163, 52);
            this.btnPicture.TabIndex = 21;
            this.btnPicture.Text = "看圖";
            this.btnPicture.UseVisualStyleBackColor = true;
            this.btnPicture.Click += new System.EventHandler(this.btnPicture_Click);
            // 
            // btnDesp
            // 
            this.btnDesp.Font = new System.Drawing.Font("細明體", 12F);
            this.btnDesp.Location = new System.Drawing.Point(424, 571);
            this.btnDesp.Name = "btnDesp";
            this.btnDesp.Size = new System.Drawing.Size(163, 52);
            this.btnDesp.TabIndex = 22;
            this.btnDesp.Text = "F6:規格說明";
            this.btnDesp.UseVisualStyleBackColor = true;
            this.btnDesp.Click += new System.EventHandler(this.btnDesp_Click);
            // 
            // btnStock
            // 
            this.btnStock.Font = new System.Drawing.Font("細明體", 12F);
            this.btnStock.Location = new System.Drawing.Point(587, 571);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(163, 52);
            this.btnStock.TabIndex = 23;
            this.btnStock.Text = "F8:庫存查詢";
            this.btnStock.UseVisualStyleBackColor = true;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("細明體", 12F);
            this.btnGet.Location = new System.Drawing.Point(750, 571);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(163, 52);
            this.btnGet.TabIndex = 24;
            this.btnGet.Text = "F9:取回";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // FrmSpecialBrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnGet;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnPicture);
            this.Controls.Add(this.tMemo);
            this.Controls.Add(this.btnDesp);
            this.Controls.Add(this.tItNo);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.lblT11);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.Q2);
            this.Controls.Add(this.tEDate);
            this.Controls.Add(this.lblT9);
            this.Controls.Add(this.Q3);
            this.Controls.Add(this.lblT8);
            this.Controls.Add(this.tSDate);
            this.Controls.Add(this.tSpNo);
            this.Controls.Add(this.SpMemo);
            this.Controls.Add(this.Q4);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.Q5);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.Stock);
            this.Controls.Add(this.Q6);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.EmName);
            this.Controls.Add(this.EmNo);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.ItNo);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmSpecialBrow";
            this.Text = "瀏覽視窗";
            this.Load += new System.EventHandler(this.FrmSpecialBrow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStrip1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT ItNo;
        private JE.MyControl.TextBoxT EmNo;
        private JE.MyControl.TextBoxT EmName;
        private JE.MyControl.TextBoxT Stock;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT SpMemo;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.ButtonSmallT Q2;
        private JE.MyControl.ButtonSmallT Q3;
        private JE.MyControl.ButtonSmallT Q4;
        private JE.MyControl.ButtonSmallT Q5;
        private JE.MyControl.ButtonSmallT Q6;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.TextBoxT tSpNo;
        private JE.MyControl.TextBoxT tItNo;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.TextBoxT tEDate;
        private JE.MyControl.TextBoxT tSDate;
        private JE.MyControl.TextBoxT tMemo;
        private JE.MyControl.LabelT lblT11;
        private JE.MyControl.ButtonSmallT btnQuery;
        private JE.MyControl.ButtonSmallT btnPicture;
        private JE.MyControl.ButtonSmallT btnDesp;
        private JE.MyControl.ButtonSmallT btnStock;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection cn;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter da;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlDataAdapter da1;
        private JE.MyControl.ButtonSmallT btnGet;
        private System.Windows.Forms.DataGridViewTextBoxColumn 特價單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 起始日;
        private System.Windows.Forms.DataGridViewTextBoxColumn 終止日;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位;
        private JE.MyControl.DataGridViewTextNumberT 數量;
        private JE.MyControl.DataGridViewTextNumberT 折數;
        private JE.MyControl.DataGridViewTextNumberT 金額;
        private JE.MyControl.DataGridViewTextNumberT 紅利;
        private System.Windows.Forms.DataGridViewTextBoxColumn 特價取用方式;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 制單人員;
        private System.Windows.Forms.DataGridViewTextBoxColumn 備註說明;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDate1;
        private System.Windows.Forms.DataGridViewTextBoxColumn EDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EDate1;
    }
}