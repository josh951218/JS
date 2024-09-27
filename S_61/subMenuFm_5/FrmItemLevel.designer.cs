using System.Data.SqlClient;
namespace S_61.SOther
{
    partial class FrmItemLevel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItemLevel));
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.ItNo = new JE.MyControl.TextBoxT();
            this.ItNoUdf = new JE.MyControl.TextBoxT();
            this.ItUnit = new JE.MyControl.TextBoxT();
            this.ItUnitP = new JE.MyControl.TextBoxT();
            this.ItPkgqty = new JE.MyControl.TextBoxNumberT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.lblT8 = new JE.MyControl.LabelT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.lblT10 = new JE.MyControl.LabelT();
            this.lblT11 = new JE.MyControl.LabelT();
            this.lblT12 = new JE.MyControl.LabelT();
            this.lblT13 = new JE.MyControl.LabelT();
            this.lblT14 = new JE.MyControl.LabelT();
            this.lblT15 = new JE.MyControl.LabelT();
            this.lblT16 = new JE.MyControl.LabelT();
            this.lblT17 = new JE.MyControl.LabelT();
            this.lblT18 = new JE.MyControl.LabelT();
            this.ItPrice = new JE.MyControl.TextBoxNumberT();
            this.ItPrice1 = new JE.MyControl.TextBoxNumberT();
            this.ItPrice2 = new JE.MyControl.TextBoxNumberT();
            this.ItPrice3 = new JE.MyControl.TextBoxNumberT();
            this.ItPrice4 = new JE.MyControl.TextBoxNumberT();
            this.ItPrice5 = new JE.MyControl.TextBoxNumberT();
            this.ItPriceP = new JE.MyControl.TextBoxNumberT();
            this.ItPriceP1 = new JE.MyControl.TextBoxNumberT();
            this.ItPriceP2 = new JE.MyControl.TextBoxNumberT();
            this.ItPriceP3 = new JE.MyControl.TextBoxNumberT();
            this.ItPriceP4 = new JE.MyControl.TextBoxNumberT();
            this.ItPriceP5 = new JE.MyControl.TextBoxNumberT();
            this.ItName = new JE.MyControl.TextBoxT();
            this.btnUpdateBat = new JE.MyControl.ButtonSmallT();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.產品編號1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.自訂編號1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品名規格1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品類別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單位1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.售價1 = new JE.MyControl.DataGridViewTextNumberT();
            this.售價一1 = new JE.MyControl.DataGridViewTextNumberT();
            this.售價二1 = new JE.MyControl.DataGridViewTextNumberT();
            this.售價三1 = new JE.MyControl.DataGridViewTextNumberT();
            this.售價四1 = new JE.MyControl.DataGridViewTextNumberT();
            this.售價五1 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝單位1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.包裝數量1 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝售價1 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝售價一1 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝售價二1 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝售價三1 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝售價四1 = new JE.MyControl.DataGridViewTextNumberT();
            this.包裝售價五1 = new JE.MyControl.DataGridViewTextNumberT();
            this.lblT19 = new JE.MyControl.LabelT();
            this.ItNos = new JE.MyControl.TextBoxT();
            this.lblT20 = new JE.MyControl.LabelT();
            this.ItNoUdfs = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnCancel = new JE.MyControl.ButtonT();
            this.btnSave = new JE.MyControl.ButtonT();
            this.btnModify = new JE.MyControl.ButtonT();
            this.KiNos = new JE.MyControl.TextBoxT();
            this.lblT21 = new JE.MyControl.LabelT();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cn1 = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.da1 = new System.Data.SqlClient.SqlDataAdapter();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.btnTempPrice = new JE.MyControl.ButtonSmallT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(13, 17);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "產品編號";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(13, 48);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "自訂編號";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(13, 79);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "品名規格";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(13, 110);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "單    位";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(13, 141);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(72, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "包裝單位";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(13, 172);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(72, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "包裝數量";
            // 
            // ItNo
            // 
            this.ItNo.AllowGrayBackColor = true;
            this.ItNo.AllowResize = true;
            this.ItNo.BackColor = System.Drawing.Color.Silver;
            this.ItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNo.Location = new System.Drawing.Point(91, 12);
            this.ItNo.MaxLength = 20;
            this.ItNo.Name = "ItNo";
            this.ItNo.oLen = 0;
            this.ItNo.ReadOnly = true;
            this.ItNo.Size = new System.Drawing.Size(167, 27);
            this.ItNo.TabIndex = 6;
            this.ItNo.TabStop = false;
            this.ItNo.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItNoUdf
            // 
            this.ItNoUdf.AllowGrayBackColor = true;
            this.ItNoUdf.AllowResize = true;
            this.ItNoUdf.BackColor = System.Drawing.Color.Silver;
            this.ItNoUdf.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNoUdf.Location = new System.Drawing.Point(91, 43);
            this.ItNoUdf.MaxLength = 20;
            this.ItNoUdf.Name = "ItNoUdf";
            this.ItNoUdf.oLen = 0;
            this.ItNoUdf.ReadOnly = true;
            this.ItNoUdf.Size = new System.Drawing.Size(167, 27);
            this.ItNoUdf.TabIndex = 7;
            this.ItNoUdf.TabStop = false;
            this.ItNoUdf.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItUnit
            // 
            this.ItUnit.AllowGrayBackColor = false;
            this.ItUnit.AllowResize = true;
            this.ItUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItUnit.Font = new System.Drawing.Font("細明體", 12F);
            this.ItUnit.Location = new System.Drawing.Point(91, 105);
            this.ItUnit.MaxLength = 4;
            this.ItUnit.Name = "ItUnit";
            this.ItUnit.oLen = 0;
            this.ItUnit.ReadOnly = true;
            this.ItUnit.Size = new System.Drawing.Size(39, 27);
            this.ItUnit.TabIndex = 2;
            this.ItUnit.TabStop = false;
            this.ItUnit.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItUnitP
            // 
            this.ItUnitP.AllowGrayBackColor = false;
            this.ItUnitP.AllowResize = true;
            this.ItUnitP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItUnitP.Font = new System.Drawing.Font("細明體", 12F);
            this.ItUnitP.Location = new System.Drawing.Point(91, 136);
            this.ItUnitP.MaxLength = 4;
            this.ItUnitP.Name = "ItUnitP";
            this.ItUnitP.oLen = 0;
            this.ItUnitP.ReadOnly = true;
            this.ItUnitP.Size = new System.Drawing.Size(39, 27);
            this.ItUnitP.TabIndex = 3;
            this.ItUnitP.TabStop = false;
            this.ItUnitP.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPkgqty
            // 
            this.ItPkgqty.AllowGrayBackColor = false;
            this.ItPkgqty.AllowResize = true;
            this.ItPkgqty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPkgqty.FirstNum = 10;
            this.ItPkgqty.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPkgqty.LastNum = 0;
            this.ItPkgqty.Location = new System.Drawing.Point(91, 167);
            this.ItPkgqty.MarkThousand = false;
            this.ItPkgqty.MaxLength = 20;
            this.ItPkgqty.Name = "ItPkgqty";
            this.ItPkgqty.NullInput = false;
            this.ItPkgqty.NullValue = "0";
            this.ItPkgqty.oLen = 0;
            this.ItPkgqty.ReadOnly = true;
            this.ItPkgqty.Size = new System.Drawing.Size(167, 27);
            this.ItPkgqty.TabIndex = 4;
            this.ItPkgqty.TabStop = false;
            this.ItPkgqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPkgqty.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(483, 17);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(56, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "售  價";
            // 
            // lblT8
            // 
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(483, 48);
            this.lblT8.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(56, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "售價一";
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(483, 79);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(56, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "售價二";
            // 
            // lblT10
            // 
            this.lblT10.AutoSize = true;
            this.lblT10.BackColor = System.Drawing.Color.Transparent;
            this.lblT10.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT10.Location = new System.Drawing.Point(483, 110);
            this.lblT10.Name = "lblT10";
            this.lblT10.Size = new System.Drawing.Size(56, 16);
            this.lblT10.TabIndex = 0;
            this.lblT10.Text = "售價三";
            // 
            // lblT11
            // 
            this.lblT11.AutoSize = true;
            this.lblT11.BackColor = System.Drawing.Color.Transparent;
            this.lblT11.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT11.Location = new System.Drawing.Point(483, 141);
            this.lblT11.Name = "lblT11";
            this.lblT11.Size = new System.Drawing.Size(56, 16);
            this.lblT11.TabIndex = 0;
            this.lblT11.Text = "售價四";
            // 
            // lblT12
            // 
            this.lblT12.AutoSize = true;
            this.lblT12.BackColor = System.Drawing.Color.Transparent;
            this.lblT12.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT12.Location = new System.Drawing.Point(483, 172);
            this.lblT12.Name = "lblT12";
            this.lblT12.Size = new System.Drawing.Size(56, 16);
            this.lblT12.TabIndex = 0;
            this.lblT12.Text = "售價五";
            // 
            // lblT13
            // 
            this.lblT13.AutoSize = true;
            this.lblT13.BackColor = System.Drawing.Color.Transparent;
            this.lblT13.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT13.Location = new System.Drawing.Point(752, 17);
            this.lblT13.Name = "lblT13";
            this.lblT13.Size = new System.Drawing.Size(72, 16);
            this.lblT13.TabIndex = 0;
            this.lblT13.Text = "包裝售價";
            // 
            // lblT14
            // 
            this.lblT14.AutoSize = true;
            this.lblT14.BackColor = System.Drawing.Color.Transparent;
            this.lblT14.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT14.Location = new System.Drawing.Point(736, 48);
            this.lblT14.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT14.Name = "lblT14";
            this.lblT14.Size = new System.Drawing.Size(88, 16);
            this.lblT14.TabIndex = 0;
            this.lblT14.Text = "包裝售價一";
            // 
            // lblT15
            // 
            this.lblT15.AutoSize = true;
            this.lblT15.BackColor = System.Drawing.Color.Transparent;
            this.lblT15.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT15.Location = new System.Drawing.Point(736, 79);
            this.lblT15.Name = "lblT15";
            this.lblT15.Size = new System.Drawing.Size(88, 16);
            this.lblT15.TabIndex = 0;
            this.lblT15.Text = "包裝售價二";
            // 
            // lblT16
            // 
            this.lblT16.AutoSize = true;
            this.lblT16.BackColor = System.Drawing.Color.Transparent;
            this.lblT16.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT16.Location = new System.Drawing.Point(736, 110);
            this.lblT16.Name = "lblT16";
            this.lblT16.Size = new System.Drawing.Size(88, 16);
            this.lblT16.TabIndex = 0;
            this.lblT16.Text = "包裝售價三";
            // 
            // lblT17
            // 
            this.lblT17.AutoSize = true;
            this.lblT17.BackColor = System.Drawing.Color.Transparent;
            this.lblT17.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT17.Location = new System.Drawing.Point(736, 141);
            this.lblT17.Name = "lblT17";
            this.lblT17.Size = new System.Drawing.Size(88, 16);
            this.lblT17.TabIndex = 0;
            this.lblT17.Text = "包裝售價四";
            // 
            // lblT18
            // 
            this.lblT18.AutoSize = true;
            this.lblT18.BackColor = System.Drawing.Color.Transparent;
            this.lblT18.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT18.Location = new System.Drawing.Point(736, 172);
            this.lblT18.Name = "lblT18";
            this.lblT18.Size = new System.Drawing.Size(88, 16);
            this.lblT18.TabIndex = 0;
            this.lblT18.Text = "包裝售價五";
            // 
            // ItPrice
            // 
            this.ItPrice.AllowGrayBackColor = false;
            this.ItPrice.AllowResize = true;
            this.ItPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPrice.FirstNum = 10;
            this.ItPrice.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPrice.LastNum = 0;
            this.ItPrice.Location = new System.Drawing.Point(547, 12);
            this.ItPrice.MarkThousand = false;
            this.ItPrice.MaxLength = 20;
            this.ItPrice.Name = "ItPrice";
            this.ItPrice.NullInput = false;
            this.ItPrice.NullValue = "0";
            this.ItPrice.oLen = 0;
            this.ItPrice.ReadOnly = true;
            this.ItPrice.Size = new System.Drawing.Size(167, 27);
            this.ItPrice.TabIndex = 11;
            this.ItPrice.TabStop = false;
            this.ItPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPrice.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPrice1
            // 
            this.ItPrice1.AllowGrayBackColor = false;
            this.ItPrice1.AllowResize = true;
            this.ItPrice1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPrice1.FirstNum = 10;
            this.ItPrice1.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPrice1.LastNum = 0;
            this.ItPrice1.Location = new System.Drawing.Point(547, 43);
            this.ItPrice1.MarkThousand = false;
            this.ItPrice1.MaxLength = 20;
            this.ItPrice1.Name = "ItPrice1";
            this.ItPrice1.NullInput = false;
            this.ItPrice1.NullValue = "0";
            this.ItPrice1.oLen = 0;
            this.ItPrice1.ReadOnly = true;
            this.ItPrice1.Size = new System.Drawing.Size(167, 27);
            this.ItPrice1.TabIndex = 12;
            this.ItPrice1.TabStop = false;
            this.ItPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPrice1.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPrice2
            // 
            this.ItPrice2.AllowGrayBackColor = false;
            this.ItPrice2.AllowResize = true;
            this.ItPrice2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPrice2.FirstNum = 10;
            this.ItPrice2.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPrice2.LastNum = 0;
            this.ItPrice2.Location = new System.Drawing.Point(547, 74);
            this.ItPrice2.MarkThousand = false;
            this.ItPrice2.MaxLength = 20;
            this.ItPrice2.Name = "ItPrice2";
            this.ItPrice2.NullInput = false;
            this.ItPrice2.NullValue = "0";
            this.ItPrice2.oLen = 0;
            this.ItPrice2.ReadOnly = true;
            this.ItPrice2.Size = new System.Drawing.Size(167, 27);
            this.ItPrice2.TabIndex = 13;
            this.ItPrice2.TabStop = false;
            this.ItPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPrice2.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPrice3
            // 
            this.ItPrice3.AllowGrayBackColor = false;
            this.ItPrice3.AllowResize = true;
            this.ItPrice3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPrice3.FirstNum = 10;
            this.ItPrice3.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPrice3.LastNum = 0;
            this.ItPrice3.Location = new System.Drawing.Point(547, 105);
            this.ItPrice3.MarkThousand = false;
            this.ItPrice3.MaxLength = 20;
            this.ItPrice3.Name = "ItPrice3";
            this.ItPrice3.NullInput = false;
            this.ItPrice3.NullValue = "0";
            this.ItPrice3.oLen = 0;
            this.ItPrice3.ReadOnly = true;
            this.ItPrice3.Size = new System.Drawing.Size(167, 27);
            this.ItPrice3.TabIndex = 14;
            this.ItPrice3.TabStop = false;
            this.ItPrice3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPrice3.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPrice4
            // 
            this.ItPrice4.AllowGrayBackColor = false;
            this.ItPrice4.AllowResize = true;
            this.ItPrice4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPrice4.FirstNum = 10;
            this.ItPrice4.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPrice4.LastNum = 0;
            this.ItPrice4.Location = new System.Drawing.Point(547, 136);
            this.ItPrice4.MarkThousand = false;
            this.ItPrice4.MaxLength = 20;
            this.ItPrice4.Name = "ItPrice4";
            this.ItPrice4.NullInput = false;
            this.ItPrice4.NullValue = "0";
            this.ItPrice4.oLen = 0;
            this.ItPrice4.ReadOnly = true;
            this.ItPrice4.Size = new System.Drawing.Size(167, 27);
            this.ItPrice4.TabIndex = 15;
            this.ItPrice4.TabStop = false;
            this.ItPrice4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPrice4.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPrice5
            // 
            this.ItPrice5.AllowGrayBackColor = false;
            this.ItPrice5.AllowResize = true;
            this.ItPrice5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPrice5.FirstNum = 10;
            this.ItPrice5.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPrice5.LastNum = 0;
            this.ItPrice5.Location = new System.Drawing.Point(547, 167);
            this.ItPrice5.MarkThousand = false;
            this.ItPrice5.MaxLength = 20;
            this.ItPrice5.Name = "ItPrice5";
            this.ItPrice5.NullInput = false;
            this.ItPrice5.NullValue = "0";
            this.ItPrice5.oLen = 0;
            this.ItPrice5.ReadOnly = true;
            this.ItPrice5.Size = new System.Drawing.Size(167, 27);
            this.ItPrice5.TabIndex = 16;
            this.ItPrice5.TabStop = false;
            this.ItPrice5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPrice5.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPriceP
            // 
            this.ItPriceP.AllowGrayBackColor = false;
            this.ItPriceP.AllowResize = true;
            this.ItPriceP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPriceP.FirstNum = 10;
            this.ItPriceP.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPriceP.LastNum = 0;
            this.ItPriceP.Location = new System.Drawing.Point(831, 12);
            this.ItPriceP.MarkThousand = false;
            this.ItPriceP.MaxLength = 20;
            this.ItPriceP.Name = "ItPriceP";
            this.ItPriceP.NullInput = false;
            this.ItPriceP.NullValue = "0";
            this.ItPriceP.oLen = 0;
            this.ItPriceP.ReadOnly = true;
            this.ItPriceP.Size = new System.Drawing.Size(167, 27);
            this.ItPriceP.TabIndex = 21;
            this.ItPriceP.TabStop = false;
            this.ItPriceP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPriceP.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPriceP1
            // 
            this.ItPriceP1.AllowGrayBackColor = false;
            this.ItPriceP1.AllowResize = true;
            this.ItPriceP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPriceP1.FirstNum = 10;
            this.ItPriceP1.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPriceP1.LastNum = 0;
            this.ItPriceP1.Location = new System.Drawing.Point(831, 43);
            this.ItPriceP1.MarkThousand = false;
            this.ItPriceP1.MaxLength = 20;
            this.ItPriceP1.Name = "ItPriceP1";
            this.ItPriceP1.NullInput = false;
            this.ItPriceP1.NullValue = "0";
            this.ItPriceP1.oLen = 0;
            this.ItPriceP1.ReadOnly = true;
            this.ItPriceP1.Size = new System.Drawing.Size(167, 27);
            this.ItPriceP1.TabIndex = 22;
            this.ItPriceP1.TabStop = false;
            this.ItPriceP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPriceP1.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPriceP2
            // 
            this.ItPriceP2.AllowGrayBackColor = false;
            this.ItPriceP2.AllowResize = true;
            this.ItPriceP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPriceP2.FirstNum = 10;
            this.ItPriceP2.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPriceP2.LastNum = 0;
            this.ItPriceP2.Location = new System.Drawing.Point(831, 74);
            this.ItPriceP2.MarkThousand = false;
            this.ItPriceP2.MaxLength = 20;
            this.ItPriceP2.Name = "ItPriceP2";
            this.ItPriceP2.NullInput = false;
            this.ItPriceP2.NullValue = "0";
            this.ItPriceP2.oLen = 0;
            this.ItPriceP2.ReadOnly = true;
            this.ItPriceP2.Size = new System.Drawing.Size(167, 27);
            this.ItPriceP2.TabIndex = 23;
            this.ItPriceP2.TabStop = false;
            this.ItPriceP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPriceP2.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPriceP3
            // 
            this.ItPriceP3.AllowGrayBackColor = false;
            this.ItPriceP3.AllowResize = true;
            this.ItPriceP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPriceP3.FirstNum = 10;
            this.ItPriceP3.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPriceP3.LastNum = 0;
            this.ItPriceP3.Location = new System.Drawing.Point(831, 105);
            this.ItPriceP3.MarkThousand = false;
            this.ItPriceP3.MaxLength = 20;
            this.ItPriceP3.Name = "ItPriceP3";
            this.ItPriceP3.NullInput = false;
            this.ItPriceP3.NullValue = "0";
            this.ItPriceP3.oLen = 0;
            this.ItPriceP3.ReadOnly = true;
            this.ItPriceP3.Size = new System.Drawing.Size(167, 27);
            this.ItPriceP3.TabIndex = 24;
            this.ItPriceP3.TabStop = false;
            this.ItPriceP3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPriceP3.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPriceP4
            // 
            this.ItPriceP4.AllowGrayBackColor = false;
            this.ItPriceP4.AllowResize = true;
            this.ItPriceP4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPriceP4.FirstNum = 10;
            this.ItPriceP4.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPriceP4.LastNum = 0;
            this.ItPriceP4.Location = new System.Drawing.Point(831, 136);
            this.ItPriceP4.MarkThousand = false;
            this.ItPriceP4.MaxLength = 20;
            this.ItPriceP4.Name = "ItPriceP4";
            this.ItPriceP4.NullInput = false;
            this.ItPriceP4.NullValue = "0";
            this.ItPriceP4.oLen = 0;
            this.ItPriceP4.ReadOnly = true;
            this.ItPriceP4.Size = new System.Drawing.Size(167, 27);
            this.ItPriceP4.TabIndex = 25;
            this.ItPriceP4.TabStop = false;
            this.ItPriceP4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPriceP4.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItPriceP5
            // 
            this.ItPriceP5.AllowGrayBackColor = false;
            this.ItPriceP5.AllowResize = true;
            this.ItPriceP5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItPriceP5.FirstNum = 10;
            this.ItPriceP5.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPriceP5.LastNum = 0;
            this.ItPriceP5.Location = new System.Drawing.Point(831, 167);
            this.ItPriceP5.MarkThousand = false;
            this.ItPriceP5.MaxLength = 20;
            this.ItPriceP5.Name = "ItPriceP5";
            this.ItPriceP5.NullInput = false;
            this.ItPriceP5.NullValue = "0";
            this.ItPriceP5.oLen = 0;
            this.ItPriceP5.ReadOnly = true;
            this.ItPriceP5.Size = new System.Drawing.Size(167, 27);
            this.ItPriceP5.TabIndex = 26;
            this.ItPriceP5.TabStop = false;
            this.ItPriceP5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItPriceP5.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // ItName
            // 
            this.ItName.AllowGrayBackColor = false;
            this.ItName.AllowResize = true;
            this.ItName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItName.Font = new System.Drawing.Font("細明體", 12F);
            this.ItName.Location = new System.Drawing.Point(91, 74);
            this.ItName.MaxLength = 30;
            this.ItName.Name = "ItName";
            this.ItName.oLen = 0;
            this.ItName.ReadOnly = true;
            this.ItName.Size = new System.Drawing.Size(247, 27);
            this.ItName.TabIndex = 1;
            this.ItName.TabStop = false;
            this.ItName.Leave += new System.EventHandler(this.表頭欄位_Leave);
            // 
            // btnUpdateBat
            // 
            this.btnUpdateBat.AutoSize = true;
            this.btnUpdateBat.Enabled = false;
            this.btnUpdateBat.Font = new System.Drawing.Font("細明體", 12F);
            this.btnUpdateBat.Location = new System.Drawing.Point(278, 161);
            this.btnUpdateBat.Name = "btnUpdateBat";
            this.btnUpdateBat.Size = new System.Drawing.Size(127, 33);
            this.btnUpdateBat.TabIndex = 36;
            this.btnUpdateBat.TabStop = false;
            this.btnUpdateBat.Text = "批次修改";
            this.btnUpdateBat.UseVisualStyleBackColor = true;
            this.btnUpdateBat.Click += new System.EventHandler(this.btnUpdateBat_Click);
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
            this.產品編號1,
            this.自訂編號1,
            this.品名規格1,
            this.產品類別,
            this.單位1,
            this.售價1,
            this.售價一1,
            this.售價二1,
            this.售價三1,
            this.售價四1,
            this.售價五1,
            this.包裝單位1,
            this.包裝數量1,
            this.包裝售價1,
            this.包裝售價一1,
            this.包裝售價二1,
            this.包裝售價三1,
            this.包裝售價四1,
            this.包裝售價五1});
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 200);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 339);
            this.dataGridViewT1.TabIndex = 27;
            this.dataGridViewT1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewT1_CellEndEdit);
            this.dataGridViewT1.SelectionChanged += new System.EventHandler(this.dataGridViewT1_SelectionChanged);
            // 
            // 產品編號1
            // 
            this.產品編號1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品編號1.DataPropertyName = "itno";
            this.產品編號1.HeaderText = "產品編號";
            this.產品編號1.MaxInputLength = 20;
            this.產品編號1.Name = "產品編號1";
            this.產品編號1.ReadOnly = true;
            this.產品編號1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.產品編號1.Width = 173;
            // 
            // 自訂編號1
            // 
            this.自訂編號1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.自訂編號1.DataPropertyName = "itnoudf";
            this.自訂編號1.HeaderText = "自訂編號";
            this.自訂編號1.MaxInputLength = 20;
            this.自訂編號1.Name = "自訂編號1";
            this.自訂編號1.ReadOnly = true;
            this.自訂編號1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.自訂編號1.Width = 173;
            // 
            // 品名規格1
            // 
            this.品名規格1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.品名規格1.DataPropertyName = "itname";
            this.品名規格1.HeaderText = "品名規格";
            this.品名規格1.MaxInputLength = 30;
            this.品名規格1.Name = "品名規格1";
            this.品名規格1.ReadOnly = true;
            this.品名規格1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.品名規格1.Width = 253;
            // 
            // 產品類別
            // 
            this.產品類別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.產品類別.DataPropertyName = "kino";
            this.產品類別.HeaderText = "產品類別";
            this.產品類別.MaxInputLength = 10;
            this.產品類別.Name = "產品類別";
            this.產品類別.ReadOnly = true;
            this.產品類別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.產品類別.Width = 93;
            // 
            // 單位1
            // 
            this.單位1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位1.DataPropertyName = "itunit";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.單位1.DefaultCellStyle = dataGridViewCellStyle2;
            this.單位1.HeaderText = "單位";
            this.單位1.MaxInputLength = 4;
            this.單位1.Name = "單位1";
            this.單位1.ReadOnly = true;
            this.單位1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單位1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位1.Width = 45;
            // 
            // 售價1
            // 
            this.售價1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.售價1.DataPropertyName = "itprice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.售價1.DefaultCellStyle = dataGridViewCellStyle3;
            this.售價1.FirstNum = 0;
            this.售價1.HeaderText = "售價";
            this.售價1.LastNum = 0;
            this.售價1.MarkThousand = false;
            this.售價1.MaxInputLength = 16;
            this.售價1.Name = "售價1";
            this.售價1.NullInput = false;
            this.售價1.NullValue = "0";
            this.售價1.ReadOnly = true;
            this.售價1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.售價1.Width = 141;
            // 
            // 售價一1
            // 
            this.售價一1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.售價一1.DataPropertyName = "itprice1";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.售價一1.DefaultCellStyle = dataGridViewCellStyle4;
            this.售價一1.FirstNum = 0;
            this.售價一1.HeaderText = "售價一";
            this.售價一1.LastNum = 0;
            this.售價一1.MarkThousand = false;
            this.售價一1.MaxInputLength = 16;
            this.售價一1.Name = "售價一1";
            this.售價一1.NullInput = false;
            this.售價一1.NullValue = "0";
            this.售價一1.ReadOnly = true;
            this.售價一1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.售價一1.Width = 141;
            // 
            // 售價二1
            // 
            this.售價二1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.售價二1.DataPropertyName = "itprice2";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.售價二1.DefaultCellStyle = dataGridViewCellStyle5;
            this.售價二1.FirstNum = 0;
            this.售價二1.HeaderText = "售價二";
            this.售價二1.LastNum = 0;
            this.售價二1.MarkThousand = false;
            this.售價二1.MaxInputLength = 16;
            this.售價二1.Name = "售價二1";
            this.售價二1.NullInput = false;
            this.售價二1.NullValue = "0";
            this.售價二1.ReadOnly = true;
            this.售價二1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.售價二1.Width = 141;
            // 
            // 售價三1
            // 
            this.售價三1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.售價三1.DataPropertyName = "itprice3";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.售價三1.DefaultCellStyle = dataGridViewCellStyle6;
            this.售價三1.FirstNum = 0;
            this.售價三1.HeaderText = "售價三";
            this.售價三1.LastNum = 0;
            this.售價三1.MarkThousand = false;
            this.售價三1.MaxInputLength = 16;
            this.售價三1.Name = "售價三1";
            this.售價三1.NullInput = false;
            this.售價三1.NullValue = "0";
            this.售價三1.ReadOnly = true;
            this.售價三1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.售價三1.Width = 141;
            // 
            // 售價四1
            // 
            this.售價四1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.售價四1.DataPropertyName = "itprice4";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.售價四1.DefaultCellStyle = dataGridViewCellStyle7;
            this.售價四1.FirstNum = 0;
            this.售價四1.HeaderText = "售價四";
            this.售價四1.LastNum = 0;
            this.售價四1.MarkThousand = false;
            this.售價四1.MaxInputLength = 16;
            this.售價四1.Name = "售價四1";
            this.售價四1.NullInput = false;
            this.售價四1.NullValue = "0";
            this.售價四1.ReadOnly = true;
            this.售價四1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.售價四1.Width = 141;
            // 
            // 售價五1
            // 
            this.售價五1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.售價五1.DataPropertyName = "itprice5";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.售價五1.DefaultCellStyle = dataGridViewCellStyle8;
            this.售價五1.FirstNum = 0;
            this.售價五1.HeaderText = "售價五";
            this.售價五1.LastNum = 0;
            this.售價五1.MarkThousand = false;
            this.售價五1.MaxInputLength = 16;
            this.售價五1.Name = "售價五1";
            this.售價五1.NullInput = false;
            this.售價五1.NullValue = "0";
            this.售價五1.ReadOnly = true;
            this.售價五1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.售價五1.Width = 141;
            // 
            // 包裝單位1
            // 
            this.包裝單位1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝單位1.DataPropertyName = "ItUnitP";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝單位1.DefaultCellStyle = dataGridViewCellStyle9;
            this.包裝單位1.HeaderText = "包裝單位";
            this.包裝單位1.MaxInputLength = 4;
            this.包裝單位1.Name = "包裝單位1";
            this.包裝單位1.ReadOnly = true;
            this.包裝單位1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.包裝單位1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝單位1.Width = 45;
            // 
            // 包裝數量1
            // 
            this.包裝數量1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝數量1.DataPropertyName = "ItPkgQty";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝數量1.DefaultCellStyle = dataGridViewCellStyle10;
            this.包裝數量1.FirstNum = 0;
            this.包裝數量1.HeaderText = "包裝數量";
            this.包裝數量1.LastNum = 0;
            this.包裝數量1.MarkThousand = false;
            this.包裝數量1.MaxInputLength = 11;
            this.包裝數量1.Name = "包裝數量1";
            this.包裝數量1.NullInput = false;
            this.包裝數量1.NullValue = "0";
            this.包裝數量1.ReadOnly = true;
            this.包裝數量1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝數量1.Width = 101;
            // 
            // 包裝售價1
            // 
            this.包裝售價1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝售價1.DataPropertyName = "ItPriceP";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝售價1.DefaultCellStyle = dataGridViewCellStyle11;
            this.包裝售價1.FirstNum = 0;
            this.包裝售價1.HeaderText = "包裝售價";
            this.包裝售價1.LastNum = 0;
            this.包裝售價1.MarkThousand = false;
            this.包裝售價1.MaxInputLength = 16;
            this.包裝售價1.Name = "包裝售價1";
            this.包裝售價1.NullInput = false;
            this.包裝售價1.NullValue = "0";
            this.包裝售價1.ReadOnly = true;
            this.包裝售價1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝售價1.Width = 141;
            // 
            // 包裝售價一1
            // 
            this.包裝售價一1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝售價一1.DataPropertyName = "ItPriceP1";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝售價一1.DefaultCellStyle = dataGridViewCellStyle12;
            this.包裝售價一1.FirstNum = 0;
            this.包裝售價一1.HeaderText = "包裝售價一";
            this.包裝售價一1.LastNum = 0;
            this.包裝售價一1.MarkThousand = false;
            this.包裝售價一1.MaxInputLength = 16;
            this.包裝售價一1.Name = "包裝售價一1";
            this.包裝售價一1.NullInput = false;
            this.包裝售價一1.NullValue = "0";
            this.包裝售價一1.ReadOnly = true;
            this.包裝售價一1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝售價一1.Width = 141;
            // 
            // 包裝售價二1
            // 
            this.包裝售價二1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝售價二1.DataPropertyName = "ItPriceP2";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝售價二1.DefaultCellStyle = dataGridViewCellStyle13;
            this.包裝售價二1.FirstNum = 0;
            this.包裝售價二1.HeaderText = "包裝售價二";
            this.包裝售價二1.LastNum = 0;
            this.包裝售價二1.MarkThousand = false;
            this.包裝售價二1.MaxInputLength = 16;
            this.包裝售價二1.Name = "包裝售價二1";
            this.包裝售價二1.NullInput = false;
            this.包裝售價二1.NullValue = "0";
            this.包裝售價二1.ReadOnly = true;
            this.包裝售價二1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝售價二1.Width = 141;
            // 
            // 包裝售價三1
            // 
            this.包裝售價三1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝售價三1.DataPropertyName = "ItPriceP3";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝售價三1.DefaultCellStyle = dataGridViewCellStyle14;
            this.包裝售價三1.FirstNum = 0;
            this.包裝售價三1.HeaderText = "包裝售價三";
            this.包裝售價三1.LastNum = 0;
            this.包裝售價三1.MarkThousand = false;
            this.包裝售價三1.MaxInputLength = 16;
            this.包裝售價三1.Name = "包裝售價三1";
            this.包裝售價三1.NullInput = false;
            this.包裝售價三1.NullValue = "0";
            this.包裝售價三1.ReadOnly = true;
            this.包裝售價三1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝售價三1.Width = 141;
            // 
            // 包裝售價四1
            // 
            this.包裝售價四1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝售價四1.DataPropertyName = "ItPriceP4";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝售價四1.DefaultCellStyle = dataGridViewCellStyle15;
            this.包裝售價四1.FirstNum = 0;
            this.包裝售價四1.HeaderText = "包裝售價四";
            this.包裝售價四1.LastNum = 0;
            this.包裝售價四1.MarkThousand = false;
            this.包裝售價四1.MaxInputLength = 16;
            this.包裝售價四1.Name = "包裝售價四1";
            this.包裝售價四1.NullInput = false;
            this.包裝售價四1.NullValue = "0";
            this.包裝售價四1.ReadOnly = true;
            this.包裝售價四1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝售價四1.Width = 141;
            // 
            // 包裝售價五1
            // 
            this.包裝售價五1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.包裝售價五1.DataPropertyName = "ItPriceP5";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.包裝售價五1.DefaultCellStyle = dataGridViewCellStyle16;
            this.包裝售價五1.FirstNum = 0;
            this.包裝售價五1.HeaderText = "包裝售價五";
            this.包裝售價五1.LastNum = 0;
            this.包裝售價五1.MarkThousand = false;
            this.包裝售價五1.MaxInputLength = 16;
            this.包裝售價五1.Name = "包裝售價五1";
            this.包裝售價五1.NullInput = false;
            this.包裝售價五1.NullValue = "0";
            this.包裝售價五1.ReadOnly = true;
            this.包裝售價五1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝售價五1.Width = 141;
            // 
            // lblT19
            // 
            this.lblT19.AutoSize = true;
            this.lblT19.BackColor = System.Drawing.Color.Transparent;
            this.lblT19.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT19.Location = new System.Drawing.Point(20, 564);
            this.lblT19.Name = "lblT19";
            this.lblT19.Size = new System.Drawing.Size(72, 16);
            this.lblT19.TabIndex = 0;
            this.lblT19.Text = "產品編號";
            // 
            // ItNos
            // 
            this.ItNos.AllowGrayBackColor = false;
            this.ItNos.AllowResize = true;
            this.ItNos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItNos.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNos.Location = new System.Drawing.Point(96, 559);
            this.ItNos.MaxLength = 20;
            this.ItNos.Name = "ItNos";
            this.ItNos.oLen = 0;
            this.ItNos.ReadOnly = true;
            this.ItNos.Size = new System.Drawing.Size(167, 27);
            this.ItNos.TabIndex = 31;
            this.ItNos.TabStop = false;
            this.ItNos.TextChanged += new System.EventHandler(this.ItNo_TextChanged);
            // 
            // lblT20
            // 
            this.lblT20.AutoSize = true;
            this.lblT20.BackColor = System.Drawing.Color.Transparent;
            this.lblT20.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT20.Location = new System.Drawing.Point(20, 597);
            this.lblT20.Name = "lblT20";
            this.lblT20.Size = new System.Drawing.Size(72, 16);
            this.lblT20.TabIndex = 0;
            this.lblT20.Text = "自訂編號";
            // 
            // ItNoUdfs
            // 
            this.ItNoUdfs.AllowGrayBackColor = false;
            this.ItNoUdfs.AllowResize = true;
            this.ItNoUdfs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ItNoUdfs.Font = new System.Drawing.Font("細明體", 12F);
            this.ItNoUdfs.Location = new System.Drawing.Point(96, 592);
            this.ItNoUdfs.MaxLength = 20;
            this.ItNoUdfs.Name = "ItNoUdfs";
            this.ItNoUdfs.oLen = 0;
            this.ItNoUdfs.ReadOnly = true;
            this.ItNoUdfs.Size = new System.Drawing.Size(167, 27);
            this.ItNoUdfs.TabIndex = 32;
            this.ItNoUdfs.TabStop = false;
            this.ItNoUdfs.TextChanged += new System.EventHandler(this.ItNo_TextChanged);
            // 
            // panelT1
            // 
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnCancel);
            this.panelT1.Controls.Add(this.btnSave);
            this.panelT1.Controls.Add(this.btnModify);
            this.panelT1.Location = new System.Drawing.Point(439, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(286, 79);
            this.panelT1.TabIndex = 34;
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
            this.btnExit.TabIndex = 16;
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
            this.btnCancel.TabIndex = 15;
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
            this.btnSave.TabIndex = 14;
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
            this.btnModify.TabIndex = 13;
            this.btnModify.UseDefaultSettings = false;
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.EnabledChanged += new System.EventHandler(this.btnModify_EnabledChanged);
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // KiNos
            // 
            this.KiNos.AllowGrayBackColor = false;
            this.KiNos.AllowResize = true;
            this.KiNos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.KiNos.Font = new System.Drawing.Font("細明體", 12F);
            this.KiNos.Location = new System.Drawing.Point(347, 592);
            this.KiNos.MaxLength = 4;
            this.KiNos.Name = "KiNos";
            this.KiNos.oLen = 0;
            this.KiNos.ReadOnly = true;
            this.KiNos.Size = new System.Drawing.Size(39, 27);
            this.KiNos.TabIndex = 33;
            this.KiNos.TabStop = false;
            this.KiNos.TextChanged += new System.EventHandler(this.ItNo_TextChanged);
            // 
            // lblT21
            // 
            this.lblT21.AutoSize = true;
            this.lblT21.BackColor = System.Drawing.Color.Transparent;
            this.lblT21.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT21.Location = new System.Drawing.Point(275, 597);
            this.lblT21.Name = "lblT21";
            this.lblT21.Size = new System.Drawing.Size(72, 16);
            this.lblT21.TabIndex = 0;
            this.lblT21.Text = "類別編號";
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "select  * from item";
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
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.cn1;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
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
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.cn1;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
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
            // da1
            // 
            this.da1.DeleteCommand = this.sqlDeleteCommand1;
            this.da1.InsertCommand = this.sqlInsertCommand1;
            this.da1.SelectCommand = this.sqlSelectCommand1;
            this.da1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
            // btnTempPrice
            // 
            this.btnTempPrice.AutoSize = true;
            this.btnTempPrice.Font = new System.Drawing.Font("細明體", 12F);
            this.btnTempPrice.Location = new System.Drawing.Point(755, 559);
            this.btnTempPrice.Name = "btnTempPrice";
            this.btnTempPrice.Size = new System.Drawing.Size(127, 44);
            this.btnTempPrice.TabIndex = 37;
            this.btnTempPrice.TabStop = false;
            this.btnTempPrice.Text = "變價底稿";
            this.btnTempPrice.UseVisualStyleBackColor = true;
            this.btnTempPrice.Click += new System.EventHandler(this.btnTempPrice_Click);
            // 
            // FrmItemLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.btnTempPrice);
            this.Controls.Add(this.btnUpdateBat);
            this.Controls.Add(this.lblT13);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.lblT14);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.lblT15);
            this.Controls.Add(this.lblT8);
            this.Controls.Add(this.lblT16);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.lblT17);
            this.Controls.Add(this.lblT9);
            this.Controls.Add(this.lblT18);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.ItPriceP);
            this.Controls.Add(this.lblT10);
            this.Controls.Add(this.ItPriceP1);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.ItPriceP2);
            this.Controls.Add(this.ItPriceP3);
            this.Controls.Add(this.lblT11);
            this.Controls.Add(this.ItPriceP4);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.ItPriceP5);
            this.Controls.Add(this.lblT12);
            this.Controls.Add(this.lblT19);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.ItNos);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.ItPrice);
            this.Controls.Add(this.ItNo);
            this.Controls.Add(this.ItPrice1);
            this.Controls.Add(this.lblT20);
            this.Controls.Add(this.ItPrice2);
            this.Controls.Add(this.ItPrice3);
            this.Controls.Add(this.ItNoUdf);
            this.Controls.Add(this.ItPrice4);
            this.Controls.Add(this.ItNoUdfs);
            this.Controls.Add(this.ItPrice5);
            this.Controls.Add(this.ItUnit);
            this.Controls.Add(this.lblT21);
            this.Controls.Add(this.ItUnitP);
            this.Controls.Add(this.KiNos);
            this.Controls.Add(this.ItPkgqty);
            this.Controls.Add(this.ItName);
            this.Name = "FrmItemLevel";
            this.Tag = "產品銷售等級建檔";
            this.Text = "產品售價等級建檔";
            this.Load += new System.EventHandler(this.FrmItemLevel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.TextBoxT ItNo;
        private JE.MyControl.TextBoxT ItNoUdf;
        private JE.MyControl.TextBoxT ItName;
        private JE.MyControl.TextBoxT ItUnit;
        private JE.MyControl.TextBoxT ItUnitP;
        private JE.MyControl.TextBoxNumberT ItPkgqty;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.LabelT lblT10;
        private JE.MyControl.LabelT lblT11;
        private JE.MyControl.LabelT lblT12;
        private JE.MyControl.LabelT lblT13;
        private JE.MyControl.LabelT lblT14;
        private JE.MyControl.LabelT lblT15;
        private JE.MyControl.LabelT lblT16;
        private JE.MyControl.LabelT lblT17;
        private JE.MyControl.LabelT lblT18;
        private JE.MyControl.TextBoxNumberT ItPrice;
        private JE.MyControl.TextBoxNumberT ItPrice1;
        private JE.MyControl.TextBoxNumberT ItPrice2;
        private JE.MyControl.TextBoxNumberT ItPrice3;
        private JE.MyControl.TextBoxNumberT ItPrice4;
        private JE.MyControl.TextBoxNumberT ItPrice5;
        private JE.MyControl.TextBoxNumberT ItPriceP;
        private JE.MyControl.TextBoxNumberT ItPriceP1;
        private JE.MyControl.TextBoxNumberT ItPriceP2;
        private JE.MyControl.TextBoxNumberT ItPriceP3;
        private JE.MyControl.TextBoxNumberT ItPriceP4;
        private JE.MyControl.TextBoxNumberT ItPriceP5;
        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnCancel;
        private JE.MyControl.ButtonT btnSave;
        private JE.MyControl.ButtonT btnModify;
        private JE.MyControl.LabelT lblT19;
        private JE.MyControl.TextBoxT ItNos;
        private JE.MyControl.LabelT lblT20;
        private JE.MyControl.TextBoxT ItNoUdfs;
        private JE.MyControl.TextBoxT KiNos;
        private JE.MyControl.LabelT lblT21; 
        private SqlCommand sqlSelectCommand1;
        private SqlConnection cn1;
        private SqlCommand sqlInsertCommand1;
        private SqlCommand sqlUpdateCommand1;
        private SqlCommand sqlDeleteCommand1;
        private SqlDataAdapter da1;
        private JE.MyControl.ButtonSmallT btnUpdateBat;
        private JE.MyControl.StatusStripT statusStripT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品編號1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 自訂編號1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品名規格1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品類別;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單位1;
        private JE.MyControl.DataGridViewTextNumberT 售價1;
        private JE.MyControl.DataGridViewTextNumberT 售價一1;
        private JE.MyControl.DataGridViewTextNumberT 售價二1;
        private JE.MyControl.DataGridViewTextNumberT 售價三1;
        private JE.MyControl.DataGridViewTextNumberT 售價四1;
        private JE.MyControl.DataGridViewTextNumberT 售價五1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 包裝單位1;
        private JE.MyControl.DataGridViewTextNumberT 包裝數量1;
        private JE.MyControl.DataGridViewTextNumberT 包裝售價1;
        private JE.MyControl.DataGridViewTextNumberT 包裝售價一1;
        private JE.MyControl.DataGridViewTextNumberT 包裝售價二1;
        private JE.MyControl.DataGridViewTextNumberT 包裝售價三1;
        private JE.MyControl.DataGridViewTextNumberT 包裝售價四1;
        private JE.MyControl.DataGridViewTextNumberT 包裝售價五1;
        private JE.MyControl.ButtonSmallT btnTempPrice;
    }
}