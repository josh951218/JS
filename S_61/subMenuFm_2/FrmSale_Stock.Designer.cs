namespace S_61.subMenuFm_2
{
    partial class FrmSale_Stock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewT1 = new JE.MyControl.DataGridViewT();
            this.lblT7 = new JE.MyControl.LabelT();
            this.ItUnitP = new JE.MyControl.TextBoxT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.ItUnit = new JE.MyControl.TextBoxT();
            this.lblT8 = new JE.MyControl.LabelT();
            this._tUnit = new JE.MyControl.TextBoxT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.ItPkgQty = new JE.MyControl.TextBoxNumberT();
            this.B1 = new JE.MyControl.TextBoxNumberT();
            this.B2 = new JE.MyControl.TextBoxNumberT();
            this.B3 = new JE.MyControl.TextBoxNumberT();
            this.B4 = new JE.MyControl.TextBoxNumberT();
            this.BTotal = new JE.MyControl.TextBoxNumberT();
            this.C1 = new JE.MyControl.TextBoxNumberT();
            this.C2 = new JE.MyControl.TextBoxNumberT();
            this.C3 = new JE.MyControl.TextBoxNumberT();
            this.C4 = new JE.MyControl.TextBoxNumberT();
            this.CTotal = new JE.MyControl.TextBoxNumberT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.A1 = new JE.MyControl.TextBoxNumberT();
            this.A2 = new JE.MyControl.TextBoxNumberT();
            this.A3 = new JE.MyControl.TextBoxNumberT();
            this.A4 = new JE.MyControl.TextBoxNumberT();
            this.ATotal = new JE.MyControl.TextBoxNumberT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelNT1 = new JE.MyControl.PanelNT();
            this.倉庫編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.倉庫名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.倉庫類別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.包裝庫存數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位庫存數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.單位庫存總數量 = new JE.MyControl.DataGridViewTextNumberT();
            this.寄庫數量 = new JE.MyControl.DataGridViewTextNumberT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).BeginInit();
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
            this.倉庫編號,
            this.倉庫名稱,
            this.倉庫類別,
            this.包裝庫存數量,
            this.單位庫存數量,
            this.單位庫存總數量,
            this.寄庫數量});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewT1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewT1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewT1.EnableHeadersVisualStyles = false;
            this.dataGridViewT1.Font = new System.Drawing.Font("細明體", 12F);
            this.dataGridViewT1.ISDocument = false;
            this.dataGridViewT1.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewT1.MultiSelect = false;
            this.dataGridViewT1.Name = "dataGridViewT1";
            this.dataGridViewT1.ReadOnly = true;
            this.dataGridViewT1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("細明體", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewT1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewT1.RowHeadersWidth = 20;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewT1.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewT1.RowTemplate.Height = 24;
            this.dataGridViewT1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewT1.ShowCellToolTips = false;
            this.dataGridViewT1.Size = new System.Drawing.Size(1010, 367);
            this.dataGridViewT1.TabIndex = 1;
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(251, 411);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(88, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "包裝庫存量";
            // 
            // ItUnitP
            // 
            this.ItUnitP.AllowGrayBackColor = true;
            this.ItUnitP.AllowResize = true;
            this.ItUnitP.BackColor = System.Drawing.Color.Silver;
            this.ItUnitP.Font = new System.Drawing.Font("細明體", 12F);
            this.ItUnitP.Location = new System.Drawing.Point(339, 406);
            this.ItUnitP.MaxLength = 4;
            this.ItUnitP.Name = "ItUnitP";
            this.ItUnitP.oLen = 0;
            this.ItUnitP.ReadOnly = true;
            this.ItUnitP.Size = new System.Drawing.Size(39, 27);
            this.ItUnitP.TabIndex = 1;
            this.ItUnitP.TabStop = false;
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(466, 411);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(88, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "單位庫存量";
            // 
            // ItUnit
            // 
            this.ItUnit.AllowGrayBackColor = true;
            this.ItUnit.AllowResize = true;
            this.ItUnit.BackColor = System.Drawing.Color.Silver;
            this.ItUnit.Font = new System.Drawing.Font("細明體", 12F);
            this.ItUnit.Location = new System.Drawing.Point(554, 406);
            this.ItUnit.MaxLength = 4;
            this.ItUnit.Name = "ItUnit";
            this.ItUnit.oLen = 0;
            this.ItUnit.ReadOnly = true;
            this.ItUnit.Size = new System.Drawing.Size(39, 27);
            this.ItUnit.TabIndex = 6;
            this.ItUnit.TabStop = false;
            // 
            // lblT8
            // 
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(681, 411);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(88, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "總計庫存量";
            // 
            // _tUnit
            // 
            this._tUnit.AllowGrayBackColor = true;
            this._tUnit.AllowResize = true;
            this._tUnit.BackColor = System.Drawing.Color.Silver;
            this._tUnit.Font = new System.Drawing.Font("細明體", 12F);
            this._tUnit.Location = new System.Drawing.Point(769, 406);
            this._tUnit.MaxLength = 4;
            this._tUnit.Name = "_tUnit";
            this._tUnit.oLen = 0;
            this._tUnit.ReadOnly = true;
            this._tUnit.Size = new System.Drawing.Size(39, 27);
            this._tUnit.TabIndex = 11;
            this._tUnit.TabStop = false;
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(136, 378);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(72, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "包裝數量";
            // 
            // ItPkgQty
            // 
            this.ItPkgQty.AllowGrayBackColor = true;
            this.ItPkgQty.AllowResize = true;
            this.ItPkgQty.BackColor = System.Drawing.Color.Silver;
            this.ItPkgQty.FirstNum = 10;
            this.ItPkgQty.Font = new System.Drawing.Font("細明體", 12F);
            this.ItPkgQty.LastNum = 0;
            this.ItPkgQty.Location = new System.Drawing.Point(211, 373);
            this.ItPkgQty.MarkThousand = false;
            this.ItPkgQty.MaxLength = 20;
            this.ItPkgQty.Name = "ItPkgQty";
            this.ItPkgQty.NullInput = false;
            this.ItPkgQty.NullValue = "0";
            this.ItPkgQty.oLen = 0;
            this.ItPkgQty.ReadOnly = true;
            this.ItPkgQty.Size = new System.Drawing.Size(167, 27);
            this.ItPkgQty.TabIndex = 16;
            this.ItPkgQty.TabStop = false;
            this.ItPkgQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // B1
            // 
            this.B1.AllowGrayBackColor = true;
            this.B1.AllowResize = true;
            this.B1.BackColor = System.Drawing.Color.Silver;
            this.B1.FirstNum = 10;
            this.B1.Font = new System.Drawing.Font("細明體", 12F);
            this.B1.LastNum = 0;
            this.B1.Location = new System.Drawing.Point(426, 439);
            this.B1.MarkThousand = false;
            this.B1.MaxLength = 20;
            this.B1.Name = "B1";
            this.B1.NullInput = false;
            this.B1.NullValue = "0";
            this.B1.oLen = 0;
            this.B1.ReadOnly = true;
            this.B1.Size = new System.Drawing.Size(167, 27);
            this.B1.TabIndex = 7;
            this.B1.TabStop = false;
            this.B1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // B2
            // 
            this.B2.AllowGrayBackColor = true;
            this.B2.AllowResize = true;
            this.B2.BackColor = System.Drawing.Color.Silver;
            this.B2.FirstNum = 10;
            this.B2.Font = new System.Drawing.Font("細明體", 12F);
            this.B2.LastNum = 0;
            this.B2.Location = new System.Drawing.Point(426, 472);
            this.B2.MarkThousand = false;
            this.B2.MaxLength = 20;
            this.B2.Name = "B2";
            this.B2.NullInput = false;
            this.B2.NullValue = "0";
            this.B2.oLen = 0;
            this.B2.ReadOnly = true;
            this.B2.Size = new System.Drawing.Size(167, 27);
            this.B2.TabIndex = 8;
            this.B2.TabStop = false;
            this.B2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // B3
            // 
            this.B3.AllowGrayBackColor = true;
            this.B3.AllowResize = true;
            this.B3.BackColor = System.Drawing.Color.Silver;
            this.B3.FirstNum = 10;
            this.B3.Font = new System.Drawing.Font("細明體", 12F);
            this.B3.LastNum = 0;
            this.B3.Location = new System.Drawing.Point(426, 505);
            this.B3.MarkThousand = false;
            this.B3.MaxLength = 20;
            this.B3.Name = "B3";
            this.B3.NullInput = false;
            this.B3.NullValue = "0";
            this.B3.oLen = 0;
            this.B3.ReadOnly = true;
            this.B3.Size = new System.Drawing.Size(167, 27);
            this.B3.TabIndex = 9;
            this.B3.TabStop = false;
            this.B3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // B4
            // 
            this.B4.AllowGrayBackColor = true;
            this.B4.AllowResize = true;
            this.B4.BackColor = System.Drawing.Color.Silver;
            this.B4.FirstNum = 10;
            this.B4.Font = new System.Drawing.Font("細明體", 12F);
            this.B4.LastNum = 0;
            this.B4.Location = new System.Drawing.Point(426, 538);
            this.B4.MarkThousand = false;
            this.B4.MaxLength = 20;
            this.B4.Name = "B4";
            this.B4.NullInput = false;
            this.B4.NullValue = "0";
            this.B4.oLen = 0;
            this.B4.ReadOnly = true;
            this.B4.Size = new System.Drawing.Size(167, 27);
            this.B4.TabIndex = 10;
            this.B4.TabStop = false;
            this.B4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BTotal
            // 
            this.BTotal.AllowGrayBackColor = true;
            this.BTotal.AllowResize = true;
            this.BTotal.BackColor = System.Drawing.Color.Silver;
            this.BTotal.FirstNum = 10;
            this.BTotal.Font = new System.Drawing.Font("細明體", 12F);
            this.BTotal.LastNum = 0;
            this.BTotal.Location = new System.Drawing.Point(426, 596);
            this.BTotal.MarkThousand = false;
            this.BTotal.MaxLength = 20;
            this.BTotal.Name = "BTotal";
            this.BTotal.NullInput = false;
            this.BTotal.NullValue = "0";
            this.BTotal.oLen = 0;
            this.BTotal.ReadOnly = true;
            this.BTotal.Size = new System.Drawing.Size(167, 27);
            this.BTotal.TabIndex = 18;
            this.BTotal.TabStop = false;
            this.BTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // C1
            // 
            this.C1.AllowGrayBackColor = true;
            this.C1.AllowResize = true;
            this.C1.BackColor = System.Drawing.Color.Silver;
            this.C1.FirstNum = 10;
            this.C1.Font = new System.Drawing.Font("細明體", 12F);
            this.C1.LastNum = 0;
            this.C1.Location = new System.Drawing.Point(641, 439);
            this.C1.MarkThousand = false;
            this.C1.MaxLength = 20;
            this.C1.Name = "C1";
            this.C1.NullInput = false;
            this.C1.NullValue = "0";
            this.C1.oLen = 0;
            this.C1.ReadOnly = true;
            this.C1.Size = new System.Drawing.Size(167, 27);
            this.C1.TabIndex = 12;
            this.C1.TabStop = false;
            this.C1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // C2
            // 
            this.C2.AllowGrayBackColor = true;
            this.C2.AllowResize = true;
            this.C2.BackColor = System.Drawing.Color.Silver;
            this.C2.FirstNum = 10;
            this.C2.Font = new System.Drawing.Font("細明體", 12F);
            this.C2.LastNum = 0;
            this.C2.Location = new System.Drawing.Point(641, 472);
            this.C2.MarkThousand = false;
            this.C2.MaxLength = 20;
            this.C2.Name = "C2";
            this.C2.NullInput = false;
            this.C2.NullValue = "0";
            this.C2.oLen = 0;
            this.C2.ReadOnly = true;
            this.C2.Size = new System.Drawing.Size(167, 27);
            this.C2.TabIndex = 13;
            this.C2.TabStop = false;
            this.C2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // C3
            // 
            this.C3.AllowGrayBackColor = true;
            this.C3.AllowResize = true;
            this.C3.BackColor = System.Drawing.Color.Silver;
            this.C3.FirstNum = 10;
            this.C3.Font = new System.Drawing.Font("細明體", 12F);
            this.C3.LastNum = 0;
            this.C3.Location = new System.Drawing.Point(641, 505);
            this.C3.MarkThousand = false;
            this.C3.MaxLength = 20;
            this.C3.Name = "C3";
            this.C3.NullInput = false;
            this.C3.NullValue = "0";
            this.C3.oLen = 0;
            this.C3.ReadOnly = true;
            this.C3.Size = new System.Drawing.Size(167, 27);
            this.C3.TabIndex = 14;
            this.C3.TabStop = false;
            this.C3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // C4
            // 
            this.C4.AllowGrayBackColor = true;
            this.C4.AllowResize = true;
            this.C4.BackColor = System.Drawing.Color.Silver;
            this.C4.FirstNum = 10;
            this.C4.Font = new System.Drawing.Font("細明體", 12F);
            this.C4.LastNum = 0;
            this.C4.Location = new System.Drawing.Point(641, 538);
            this.C4.MarkThousand = false;
            this.C4.MaxLength = 20;
            this.C4.Name = "C4";
            this.C4.NullInput = false;
            this.C4.NullValue = "0";
            this.C4.oLen = 0;
            this.C4.ReadOnly = true;
            this.C4.Size = new System.Drawing.Size(167, 27);
            this.C4.TabIndex = 15;
            this.C4.TabStop = false;
            this.C4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CTotal
            // 
            this.CTotal.AllowGrayBackColor = true;
            this.CTotal.AllowResize = true;
            this.CTotal.BackColor = System.Drawing.Color.Silver;
            this.CTotal.FirstNum = 10;
            this.CTotal.Font = new System.Drawing.Font("細明體", 12F);
            this.CTotal.LastNum = 0;
            this.CTotal.Location = new System.Drawing.Point(641, 596);
            this.CTotal.MarkThousand = false;
            this.CTotal.MaxLength = 20;
            this.CTotal.Name = "CTotal";
            this.CTotal.NullInput = false;
            this.CTotal.NullValue = "0";
            this.CTotal.oLen = 0;
            this.CTotal.ReadOnly = true;
            this.CTotal.Size = new System.Drawing.Size(167, 27);
            this.CTotal.TabIndex = 19;
            this.CTotal.TabStop = false;
            this.CTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(136, 444);
            this.lblT1.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "庫 存 倉";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(136, 477);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "借 出 倉";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(136, 510);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "加 工 倉";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(136, 543);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "借 入 倉";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(136, 601);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(72, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "總庫存量";
            // 
            // A1
            // 
            this.A1.AllowGrayBackColor = true;
            this.A1.AllowResize = true;
            this.A1.BackColor = System.Drawing.Color.Silver;
            this.A1.FirstNum = 10;
            this.A1.Font = new System.Drawing.Font("細明體", 12F);
            this.A1.LastNum = 0;
            this.A1.Location = new System.Drawing.Point(211, 439);
            this.A1.MarkThousand = false;
            this.A1.MaxLength = 20;
            this.A1.Name = "A1";
            this.A1.NullInput = false;
            this.A1.NullValue = "0";
            this.A1.oLen = 0;
            this.A1.ReadOnly = true;
            this.A1.Size = new System.Drawing.Size(167, 27);
            this.A1.TabIndex = 2;
            this.A1.TabStop = false;
            this.A1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // A2
            // 
            this.A2.AllowGrayBackColor = true;
            this.A2.AllowResize = true;
            this.A2.BackColor = System.Drawing.Color.Silver;
            this.A2.FirstNum = 10;
            this.A2.Font = new System.Drawing.Font("細明體", 12F);
            this.A2.LastNum = 0;
            this.A2.Location = new System.Drawing.Point(211, 472);
            this.A2.MarkThousand = false;
            this.A2.MaxLength = 20;
            this.A2.Name = "A2";
            this.A2.NullInput = false;
            this.A2.NullValue = "0";
            this.A2.oLen = 0;
            this.A2.ReadOnly = true;
            this.A2.Size = new System.Drawing.Size(167, 27);
            this.A2.TabIndex = 3;
            this.A2.TabStop = false;
            this.A2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // A3
            // 
            this.A3.AllowGrayBackColor = true;
            this.A3.AllowResize = true;
            this.A3.BackColor = System.Drawing.Color.Silver;
            this.A3.FirstNum = 10;
            this.A3.Font = new System.Drawing.Font("細明體", 12F);
            this.A3.LastNum = 0;
            this.A3.Location = new System.Drawing.Point(211, 505);
            this.A3.MarkThousand = false;
            this.A3.MaxLength = 20;
            this.A3.Name = "A3";
            this.A3.NullInput = false;
            this.A3.NullValue = "0";
            this.A3.oLen = 0;
            this.A3.ReadOnly = true;
            this.A3.Size = new System.Drawing.Size(167, 27);
            this.A3.TabIndex = 4;
            this.A3.TabStop = false;
            this.A3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // A4
            // 
            this.A4.AllowGrayBackColor = true;
            this.A4.AllowResize = true;
            this.A4.BackColor = System.Drawing.Color.Silver;
            this.A4.FirstNum = 10;
            this.A4.Font = new System.Drawing.Font("細明體", 12F);
            this.A4.LastNum = 0;
            this.A4.Location = new System.Drawing.Point(211, 538);
            this.A4.MarkThousand = false;
            this.A4.MaxLength = 20;
            this.A4.Name = "A4";
            this.A4.NullInput = false;
            this.A4.NullValue = "0";
            this.A4.oLen = 0;
            this.A4.ReadOnly = true;
            this.A4.Size = new System.Drawing.Size(167, 27);
            this.A4.TabIndex = 5;
            this.A4.TabStop = false;
            this.A4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ATotal
            // 
            this.ATotal.AllowGrayBackColor = true;
            this.ATotal.AllowResize = true;
            this.ATotal.BackColor = System.Drawing.Color.Silver;
            this.ATotal.FirstNum = 10;
            this.ATotal.Font = new System.Drawing.Font("細明體", 12F);
            this.ATotal.LastNum = 0;
            this.ATotal.Location = new System.Drawing.Point(211, 596);
            this.ATotal.MarkThousand = false;
            this.ATotal.MaxLength = 20;
            this.ATotal.Name = "ATotal";
            this.ATotal.NullInput = false;
            this.ATotal.NullValue = "0";
            this.ATotal.oLen = 0;
            this.ATotal.ReadOnly = true;
            this.ATotal.Size = new System.Drawing.Size(167, 27);
            this.ATotal.TabIndex = 17;
            this.ATotal.TabStop = false;
            this.ATotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(825, 531);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(162, 41);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "F11:結束";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            // panelNT1
            // 
            this.panelNT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNT1.Location = new System.Drawing.Point(135, 583);
            this.panelNT1.Name = "panelNT1";
            this.panelNT1.Size = new System.Drawing.Size(673, 1);
            this.panelNT1.TabIndex = 21;
            // 
            // 倉庫編號
            // 
            this.倉庫編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.倉庫編號.DataPropertyName = "StNo";
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
            this.倉庫名稱.DataPropertyName = "StName";
            this.倉庫名稱.HeaderText = "倉庫名稱";
            this.倉庫名稱.MaxInputLength = 10;
            this.倉庫名稱.Name = "倉庫名稱";
            this.倉庫名稱.ReadOnly = true;
            this.倉庫名稱.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.倉庫名稱.Width = 93;
            // 
            // 倉庫類別
            // 
            this.倉庫類別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.倉庫類別.HeaderText = "倉庫類別";
            this.倉庫類別.MaxInputLength = 10;
            this.倉庫類別.Name = "倉庫類別";
            this.倉庫類別.ReadOnly = true;
            this.倉庫類別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.倉庫類別.Width = 93;
            // 
            // 包裝庫存數量
            // 
            this.包裝庫存數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = "0";
            this.包裝庫存數量.DefaultCellStyle = dataGridViewCellStyle2;
            this.包裝庫存數量.FirstNum = 0;
            this.包裝庫存數量.HeaderText = "包裝庫存數量 - ";
            this.包裝庫存數量.LastNum = 0;
            this.包裝庫存數量.MarkThousand = false;
            this.包裝庫存數量.MaxInputLength = 20;
            this.包裝庫存數量.Name = "包裝庫存數量";
            this.包裝庫存數量.NullInput = false;
            this.包裝庫存數量.NullValue = "0";
            this.包裝庫存數量.ReadOnly = true;
            this.包裝庫存數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.包裝庫存數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.包裝庫存數量.Width = 173;
            // 
            // 單位庫存數量
            // 
            this.單位庫存數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.NullValue = "0";
            this.單位庫存數量.DefaultCellStyle = dataGridViewCellStyle3;
            this.單位庫存數量.FirstNum = 0;
            this.單位庫存數量.HeaderText = "單位庫存數量 - ";
            this.單位庫存數量.LastNum = 0;
            this.單位庫存數量.MarkThousand = false;
            this.單位庫存數量.MaxInputLength = 20;
            this.單位庫存數量.Name = "單位庫存數量";
            this.單位庫存數量.NullInput = false;
            this.單位庫存數量.NullValue = "0";
            this.單位庫存數量.ReadOnly = true;
            this.單位庫存數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單位庫存數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位庫存數量.Width = 173;
            // 
            // 單位庫存總數量
            // 
            this.單位庫存總數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.單位庫存總數量.DataPropertyName = "ItQty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.NullValue = "0";
            this.單位庫存總數量.DefaultCellStyle = dataGridViewCellStyle4;
            this.單位庫存總數量.FirstNum = 0;
            this.單位庫存總數量.HeaderText = "單位庫存總數量";
            this.單位庫存總數量.LastNum = 0;
            this.單位庫存總數量.MarkThousand = false;
            this.單位庫存總數量.MaxInputLength = 20;
            this.單位庫存總數量.Name = "單位庫存總數量";
            this.單位庫存總數量.NullInput = false;
            this.單位庫存總數量.NullValue = "0";
            this.單位庫存總數量.ReadOnly = true;
            this.單位庫存總數量.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.單位庫存總數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.單位庫存總數量.Width = 173;
            // 
            // 寄庫數量
            // 
            this.寄庫數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.寄庫數量.DataPropertyName = "寄庫數量";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.寄庫數量.DefaultCellStyle = dataGridViewCellStyle5;
            this.寄庫數量.FirstNum = 0;
            this.寄庫數量.HeaderText = "含寄庫數量";
            this.寄庫數量.LastNum = 0;
            this.寄庫數量.MarkThousand = false;
            this.寄庫數量.MaxInputLength = 20;
            this.寄庫數量.Name = "寄庫數量";
            this.寄庫數量.NullInput = false;
            this.寄庫數量.NullValue = "0";
            this.寄庫數量.ReadOnly = true;
            this.寄庫數量.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.寄庫數量.Width = 173;
            // 
            // FrmSale_Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelNT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.BTotal);
            this.Controls.Add(this.B4);
            this.Controls.Add(this.CTotal);
            this.Controls.Add(this.C4);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.B3);
            this.Controls.Add(this.ATotal);
            this.Controls.Add(this.B2);
            this.Controls.Add(this.B1);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.A4);
            this.Controls.Add(this.lblT3);
            this.Controls.Add(this.C3);
            this.Controls.Add(this.C2);
            this.Controls.Add(this.ItPkgQty);
            this.Controls.Add(this.A3);
            this.Controls.Add(this.lblT9);
            this.Controls.Add(this._tUnit);
            this.Controls.Add(this.C1);
            this.Controls.Add(this.lblT8);
            this.Controls.Add(this.ItUnit);
            this.Controls.Add(this.lblT2);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.ItUnitP);
            this.Controls.Add(this.A2);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.dataGridViewT1);
            this.Controls.Add(this.lblT1);
            this.Controls.Add(this.A1);
            this.Name = "FrmSale_Stock";
            this.Text = "庫存量查詢";
            this.Load += new System.EventHandler(this.FrmSale_Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewT1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.DataGridViewT dataGridViewT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.TextBoxNumberT A1;
        private JE.MyControl.TextBoxNumberT A2;
        private JE.MyControl.TextBoxNumberT A3;
        private JE.MyControl.TextBoxNumberT A4;
        private JE.MyControl.TextBoxNumberT B1;
        private JE.MyControl.TextBoxNumberT B2;
        private JE.MyControl.TextBoxNumberT B3;
        private JE.MyControl.TextBoxNumberT B4;
        private JE.MyControl.TextBoxNumberT C1;
        private JE.MyControl.TextBoxNumberT C2;
        private JE.MyControl.TextBoxNumberT C3;
        private JE.MyControl.TextBoxNumberT C4;
        private JE.MyControl.TextBoxNumberT ItPkgQty;
        private JE.MyControl.TextBoxNumberT ATotal;
        private JE.MyControl.TextBoxNumberT BTotal;
        private JE.MyControl.TextBoxNumberT CTotal;
        private JE.MyControl.TextBoxT ItUnitP;
        private JE.MyControl.TextBoxT ItUnit;
        private JE.MyControl.TextBoxT _tUnit;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelNT panelNT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 倉庫類別;
        private JE.MyControl.DataGridViewTextNumberT 包裝庫存數量;
        private JE.MyControl.DataGridViewTextNumberT 單位庫存數量;
        private JE.MyControl.DataGridViewTextNumberT 單位庫存總數量;
        private JE.MyControl.DataGridViewTextNumberT 寄庫數量;
    }
}