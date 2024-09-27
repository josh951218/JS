namespace S_61.subMenuFm_1
{
    partial class FrmFord_Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFord_Info));
            this.lblT7 = new JE.MyControl.LabelT();
            this.lblT8 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.rd2 = new JE.MyControl.RadioT();
            this.rd1 = new JE.MyControl.RadioT();
            this.groupBoxT2 = new JE.MyControl.GroupBoxT();
            this.rd4 = new JE.MyControl.RadioT();
            this.rd3 = new JE.MyControl.RadioT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.lblT10 = new JE.MyControl.LabelT();
            this.lblT11 = new JE.MyControl.LabelT();
            this.fodate = new JE.MyControl.TextBoxT();
            this.itno1 = new JE.MyControl.TextBoxT();
            this.memo = new JE.MyControl.TextBoxT();
            this.esdate = new JE.MyControl.TextBoxT();
            this.fano = new JE.MyControl.TextBoxT();
            this.emno = new JE.MyControl.TextBoxT();
            this.itno = new JE.MyControl.TextBoxT();
            this.fodate1 = new JE.MyControl.TextBoxT();
            this.esdate1 = new JE.MyControl.TextBoxT();
            this.fano1 = new JE.MyControl.TextBoxT();
            this.emno1 = new JE.MyControl.TextBoxT();
            this.lblT13 = new JE.MyControl.LabelT();
            this.lblT14 = new JE.MyControl.LabelT();
            this.lblT16 = new JE.MyControl.LabelT();
            this.lblT17 = new JE.MyControl.LabelT();
            this.lblT12 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.spno = new JE.MyControl.TextBoxT();
            this.spno1 = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.groupBoxT1.SuspendLayout();
            this.groupBoxT2.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.BackColor = System.Drawing.Color.Transparent;
            this.lblT7.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT7.Location = new System.Drawing.Point(361, 492);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(288, 16);
            this.lblT7.TabIndex = 0;
            this.lblT7.Text = "※終止編號空白表示列印至最後一筆...";
            // 
            // lblT8
            // 
            this.lblT8.AutoSize = true;
            this.lblT8.BackColor = System.Drawing.Color.Transparent;
            this.lblT8.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT8.Location = new System.Drawing.Point(361, 519);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(288, 16);
            this.lblT8.TabIndex = 0;
            this.lblT8.Text = "※兩者皆空白表示全部列印...........";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(361, 465);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(288, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "※起始編號空白表示從第一筆列印.....";
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.rd2);
            this.groupBoxT1.Controls.Add(this.rd1);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(114, 20);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(389, 64);
            this.groupBoxT1.TabIndex = 3;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "未交量小於零是否列印";
            // 
            // rd2
            // 
            this.rd2.AutoSize = true;
            this.rd2.BackColor = System.Drawing.Color.LightBlue;
            this.rd2.Checked = true;
            this.rd2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd2.Font = new System.Drawing.Font("細明體", 12F);
            this.rd2.Location = new System.Drawing.Point(246, 29);
            this.rd2.Name = "rd2";
            this.rd2.Size = new System.Drawing.Size(42, 20);
            this.rd2.TabIndex = 1;
            this.rd2.Tag = "否";
            this.rd2.Text = "否";
            this.rd2.UseVisualStyleBackColor = true;
            // 
            // rd1
            // 
            this.rd1.AutoSize = true;
            this.rd1.BackColor = System.Drawing.Color.Transparent;
            this.rd1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd1.Font = new System.Drawing.Font("細明體", 12F);
            this.rd1.Location = new System.Drawing.Point(100, 29);
            this.rd1.Name = "rd1";
            this.rd1.Size = new System.Drawing.Size(42, 20);
            this.rd1.TabIndex = 0;
            this.rd1.Tag = "是";
            this.rd1.Text = "是";
            this.rd1.UseVisualStyleBackColor = true;
            // 
            // groupBoxT2
            // 
            this.groupBoxT2.Controls.Add(this.rd4);
            this.groupBoxT2.Controls.Add(this.rd3);
            this.groupBoxT2.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT2.Location = new System.Drawing.Point(507, 20);
            this.groupBoxT2.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT2.Name = "groupBoxT2";
            this.groupBoxT2.Size = new System.Drawing.Size(390, 64);
            this.groupBoxT2.TabIndex = 4;
            this.groupBoxT2.TabStop = false;
            this.groupBoxT2.Text = "採購單已結案是否顯示";
            // 
            // rd4
            // 
            this.rd4.AutoSize = true;
            this.rd4.BackColor = System.Drawing.Color.LightBlue;
            this.rd4.Checked = true;
            this.rd4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd4.Font = new System.Drawing.Font("細明體", 12F);
            this.rd4.Location = new System.Drawing.Point(245, 29);
            this.rd4.Name = "rd4";
            this.rd4.Size = new System.Drawing.Size(42, 20);
            this.rd4.TabIndex = 1;
            this.rd4.Tag = "否";
            this.rd4.Text = "否";
            this.rd4.UseVisualStyleBackColor = true;
            // 
            // rd3
            // 
            this.rd3.AutoSize = true;
            this.rd3.BackColor = System.Drawing.Color.Transparent;
            this.rd3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd3.Font = new System.Drawing.Font("細明體", 12F);
            this.rd3.Location = new System.Drawing.Point(103, 29);
            this.rd3.Name = "rd3";
            this.rd3.Size = new System.Drawing.Size(42, 20);
            this.rd3.TabIndex = 0;
            this.rd3.Tag = "是";
            this.rd3.Text = "是";
            this.rd3.UseVisualStyleBackColor = true;
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.labelT1);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.fodate);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.spno1);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.spno);
            this.pnlBoxT1.Controls.Add(this.lblT5);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.lblT9);
            this.pnlBoxT1.Controls.Add(this.lblT12);
            this.pnlBoxT1.Controls.Add(this.lblT10);
            this.pnlBoxT1.Controls.Add(this.lblT17);
            this.pnlBoxT1.Controls.Add(this.lblT11);
            this.pnlBoxT1.Controls.Add(this.lblT16);
            this.pnlBoxT1.Controls.Add(this.lblT14);
            this.pnlBoxT1.Controls.Add(this.itno1);
            this.pnlBoxT1.Controls.Add(this.lblT13);
            this.pnlBoxT1.Controls.Add(this.memo);
            this.pnlBoxT1.Controls.Add(this.emno1);
            this.pnlBoxT1.Controls.Add(this.esdate);
            this.pnlBoxT1.Controls.Add(this.fano1);
            this.pnlBoxT1.Controls.Add(this.fano);
            this.pnlBoxT1.Controls.Add(this.esdate1);
            this.pnlBoxT1.Controls.Add(this.emno);
            this.pnlBoxT1.Controls.Add(this.fodate1);
            this.pnlBoxT1.Controls.Add(this.itno);
            this.pnlBoxT1.Location = new System.Drawing.Point(114, 91);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(783, 359);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(223, 41);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(104, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "起迄採購日期";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(223, 78);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起迄交貨日期";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(223, 115);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(104, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "起迄廠商編號";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(223, 152);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(104, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "起迄採購人員";
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(223, 226);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(104, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "起迄產品編號";
            // 
            // lblT10
            // 
            this.lblT10.AutoSize = true;
            this.lblT10.BackColor = System.Drawing.Color.Transparent;
            this.lblT10.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT10.Location = new System.Drawing.Point(223, 263);
            this.lblT10.Name = "lblT10";
            this.lblT10.Size = new System.Drawing.Size(104, 16);
            this.lblT10.TabIndex = 0;
            this.lblT10.Text = "終止產品編號";
            // 
            // lblT11
            // 
            this.lblT11.AutoSize = true;
            this.lblT11.BackColor = System.Drawing.Color.Transparent;
            this.lblT11.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT11.Location = new System.Drawing.Point(223, 300);
            this.lblT11.Name = "lblT11";
            this.lblT11.Size = new System.Drawing.Size(72, 16);
            this.lblT11.TabIndex = 0;
            this.lblT11.Text = "備註說明";
            // 
            // fodate
            // 
            this.fodate.AllowGrayBackColor = false;
            this.fodate.AllowResize = true;
            this.fodate.Font = new System.Drawing.Font("細明體", 12F);
            this.fodate.Location = new System.Drawing.Point(340, 36);
            this.fodate.MaxLength = 8;
            this.fodate.Name = "fodate";
            this.fodate.oLen = 0;
            this.fodate.Size = new System.Drawing.Size(71, 27);
            this.fodate.TabIndex = 1;
            this.fodate.Tag = "採購日期";
            this.fodate.Validating += new System.ComponentModel.CancelEventHandler(this.fodate_Validating);
            // 
            // itno1
            // 
            this.itno1.AllowGrayBackColor = false;
            this.itno1.AllowResize = true;
            this.itno1.Font = new System.Drawing.Font("細明體", 12F);
            this.itno1.Location = new System.Drawing.Point(340, 258);
            this.itno1.MaxLength = 20;
            this.itno1.Name = "itno1";
            this.itno1.oLen = 0;
            this.itno1.Size = new System.Drawing.Size(167, 27);
            this.itno1.TabIndex = 14;
            this.itno1.Tag = "產品編號";
            this.itno1.DoubleClick += new System.EventHandler(this.itno_DoubleClick);
            this.itno1.Validating += new System.ComponentModel.CancelEventHandler(this.itno_Validating);
            // 
            // memo
            // 
            this.memo.AllowGrayBackColor = false;
            this.memo.AllowResize = true;
            this.memo.Font = new System.Drawing.Font("細明體", 12F);
            this.memo.Location = new System.Drawing.Point(340, 295);
            this.memo.MaxLength = 20;
            this.memo.Name = "memo";
            this.memo.oLen = 0;
            this.memo.Size = new System.Drawing.Size(167, 27);
            this.memo.TabIndex = 15;
            this.memo.DoubleClick += new System.EventHandler(this.memo_DoubleClick);
            // 
            // esdate
            // 
            this.esdate.AllowGrayBackColor = false;
            this.esdate.AllowResize = true;
            this.esdate.Font = new System.Drawing.Font("細明體", 12F);
            this.esdate.Location = new System.Drawing.Point(340, 73);
            this.esdate.MaxLength = 8;
            this.esdate.Name = "esdate";
            this.esdate.oLen = 0;
            this.esdate.Size = new System.Drawing.Size(71, 27);
            this.esdate.TabIndex = 3;
            this.esdate.Tag = "交貨日期";
            this.esdate.Validating += new System.ComponentModel.CancelEventHandler(this.esdate_Validating);
            // 
            // fano
            // 
            this.fano.AllowGrayBackColor = false;
            this.fano.AllowResize = true;
            this.fano.Font = new System.Drawing.Font("細明體", 12F);
            this.fano.Location = new System.Drawing.Point(340, 110);
            this.fano.MaxLength = 10;
            this.fano.Name = "fano";
            this.fano.oLen = 0;
            this.fano.Size = new System.Drawing.Size(87, 27);
            this.fano.TabIndex = 7;
            this.fano.Tag = "客戶編號";
            this.fano.DoubleClick += new System.EventHandler(this.fano_DoubleClick);
            this.fano.Validating += new System.ComponentModel.CancelEventHandler(this.fano_Validating);
            // 
            // emno
            // 
            this.emno.AllowGrayBackColor = false;
            this.emno.AllowResize = true;
            this.emno.Font = new System.Drawing.Font("細明體", 12F);
            this.emno.Location = new System.Drawing.Point(340, 147);
            this.emno.MaxLength = 4;
            this.emno.Name = "emno";
            this.emno.oLen = 0;
            this.emno.Size = new System.Drawing.Size(39, 27);
            this.emno.TabIndex = 9;
            this.emno.Tag = "業務人員";
            this.emno.DoubleClick += new System.EventHandler(this.emno_DoubleClick);
            this.emno.Validating += new System.ComponentModel.CancelEventHandler(this.emno_Validating);
            // 
            // itno
            // 
            this.itno.AllowGrayBackColor = false;
            this.itno.AllowResize = true;
            this.itno.Font = new System.Drawing.Font("細明體", 12F);
            this.itno.Location = new System.Drawing.Point(340, 221);
            this.itno.MaxLength = 20;
            this.itno.Name = "itno";
            this.itno.oLen = 0;
            this.itno.Size = new System.Drawing.Size(167, 27);
            this.itno.TabIndex = 13;
            this.itno.Tag = "產品編號";
            this.itno.DoubleClick += new System.EventHandler(this.itno_DoubleClick);
            this.itno.Validating += new System.ComponentModel.CancelEventHandler(this.itno_Validating);
            // 
            // fodate1
            // 
            this.fodate1.AllowGrayBackColor = false;
            this.fodate1.AllowResize = true;
            this.fodate1.Font = new System.Drawing.Font("細明體", 12F);
            this.fodate1.Location = new System.Drawing.Point(473, 36);
            this.fodate1.MaxLength = 8;
            this.fodate1.Name = "fodate1";
            this.fodate1.oLen = 0;
            this.fodate1.Size = new System.Drawing.Size(71, 27);
            this.fodate1.TabIndex = 2;
            this.fodate1.Tag = "採購日期";
            this.fodate1.Validating += new System.ComponentModel.CancelEventHandler(this.fodate_Validating);
            // 
            // esdate1
            // 
            this.esdate1.AllowGrayBackColor = false;
            this.esdate1.AllowResize = true;
            this.esdate1.Font = new System.Drawing.Font("細明體", 12F);
            this.esdate1.Location = new System.Drawing.Point(473, 73);
            this.esdate1.MaxLength = 8;
            this.esdate1.Name = "esdate1";
            this.esdate1.oLen = 0;
            this.esdate1.Size = new System.Drawing.Size(71, 27);
            this.esdate1.TabIndex = 4;
            this.esdate1.Tag = "交貨日期";
            this.esdate1.Validating += new System.ComponentModel.CancelEventHandler(this.esdate_Validating);
            // 
            // fano1
            // 
            this.fano1.AllowGrayBackColor = false;
            this.fano1.AllowResize = true;
            this.fano1.Font = new System.Drawing.Font("細明體", 12F);
            this.fano1.Location = new System.Drawing.Point(473, 110);
            this.fano1.MaxLength = 10;
            this.fano1.Name = "fano1";
            this.fano1.oLen = 0;
            this.fano1.Size = new System.Drawing.Size(87, 27);
            this.fano1.TabIndex = 8;
            this.fano1.Tag = "客戶編號";
            this.fano1.DoubleClick += new System.EventHandler(this.fano_DoubleClick);
            this.fano1.Validating += new System.ComponentModel.CancelEventHandler(this.fano_Validating);
            // 
            // emno1
            // 
            this.emno1.AllowGrayBackColor = false;
            this.emno1.AllowResize = true;
            this.emno1.Font = new System.Drawing.Font("細明體", 12F);
            this.emno1.Location = new System.Drawing.Point(473, 147);
            this.emno1.MaxLength = 4;
            this.emno1.Name = "emno1";
            this.emno1.oLen = 0;
            this.emno1.Size = new System.Drawing.Size(39, 27);
            this.emno1.TabIndex = 10;
            this.emno1.Tag = "業務人員";
            this.emno1.DoubleClick += new System.EventHandler(this.emno_DoubleClick);
            this.emno1.Validating += new System.ComponentModel.CancelEventHandler(this.emno_Validating);
            // 
            // lblT13
            // 
            this.lblT13.AutoSize = true;
            this.lblT13.BackColor = System.Drawing.Color.Transparent;
            this.lblT13.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT13.Location = new System.Drawing.Point(440, 41);
            this.lblT13.Name = "lblT13";
            this.lblT13.Size = new System.Drawing.Size(24, 16);
            this.lblT13.TabIndex = 0;
            this.lblT13.Text = "～";
            // 
            // lblT14
            // 
            this.lblT14.AutoSize = true;
            this.lblT14.BackColor = System.Drawing.Color.Transparent;
            this.lblT14.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT14.Location = new System.Drawing.Point(440, 78);
            this.lblT14.Name = "lblT14";
            this.lblT14.Size = new System.Drawing.Size(24, 16);
            this.lblT14.TabIndex = 0;
            this.lblT14.Text = "～";
            // 
            // lblT16
            // 
            this.lblT16.AutoSize = true;
            this.lblT16.BackColor = System.Drawing.Color.Transparent;
            this.lblT16.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT16.Location = new System.Drawing.Point(440, 115);
            this.lblT16.Name = "lblT16";
            this.lblT16.Size = new System.Drawing.Size(24, 16);
            this.lblT16.TabIndex = 0;
            this.lblT16.Text = "～";
            // 
            // lblT17
            // 
            this.lblT17.AutoSize = true;
            this.lblT17.BackColor = System.Drawing.Color.Transparent;
            this.lblT17.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT17.Location = new System.Drawing.Point(440, 152);
            this.lblT17.Name = "lblT17";
            this.lblT17.Size = new System.Drawing.Size(24, 16);
            this.lblT17.TabIndex = 0;
            this.lblT17.Text = "～";
            // 
            // lblT12
            // 
            this.lblT12.AutoSize = true;
            this.lblT12.BackColor = System.Drawing.Color.Transparent;
            this.lblT12.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT12.Location = new System.Drawing.Point(79, 171);
            this.lblT12.Name = "lblT12";
            this.lblT12.Size = new System.Drawing.Size(88, 16);
            this.lblT12.TabIndex = 0;
            this.lblT12.Text = "請輸入列印";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(223, 189);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "起迄專案編號";
            // 
            // spno
            // 
            this.spno.AllowGrayBackColor = false;
            this.spno.AllowResize = true;
            this.spno.Font = new System.Drawing.Font("細明體", 12F);
            this.spno.Location = new System.Drawing.Point(340, 184);
            this.spno.MaxLength = 4;
            this.spno.Name = "spno";
            this.spno.oLen = 0;
            this.spno.Size = new System.Drawing.Size(39, 27);
            this.spno.TabIndex = 11;
            this.spno.Tag = "專案編號";
            this.spno.DoubleClick += new System.EventHandler(this.spno_DoubleClick);
            this.spno.Validating += new System.ComponentModel.CancelEventHandler(this.spno_Validating);
            // 
            // spno1
            // 
            this.spno1.AllowGrayBackColor = false;
            this.spno1.AllowResize = true;
            this.spno1.Font = new System.Drawing.Font("細明體", 12F);
            this.spno1.Location = new System.Drawing.Point(473, 184);
            this.spno1.MaxLength = 4;
            this.spno1.Name = "spno1";
            this.spno1.oLen = 0;
            this.spno1.Size = new System.Drawing.Size(39, 27);
            this.spno1.TabIndex = 12;
            this.spno1.Tag = "專案編號";
            this.spno1.DoubleClick += new System.EventHandler(this.spno_DoubleClick);
            this.spno1.Validating += new System.ComponentModel.CancelEventHandler(this.spno_Validating);
            // 
            // panelT1
            // 
            this.panelT1.BackColor = System.Drawing.Color.Transparent;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(431, 545);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 79);
            this.panelT1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 5F);
            this.btnExit.Location = new System.Drawing.Point(69, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 15;
            this.btnExit.UseDefaultSettings = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnBrow
            // 
            this.btnBrow.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrow.BackgroundImage")));
            this.btnBrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrow.Font = new System.Drawing.Font("Times New Roman", 5F);
            this.btnBrow.Location = new System.Drawing.Point(0, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 14;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
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
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(440, 189);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(24, 16);
            this.labelT1.TabIndex = 16;
            this.labelT1.Text = "～";
            // 
            // FrmFord_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.groupBoxT2);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.lblT8);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmFord_Info";
            this.Tag = "採購資料瀏覽";
            this.Text = "採購資料瀏覽";
            this.Load += new System.EventHandler(this.FrmFord_Info_Load);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.groupBoxT2.ResumeLayout(false);
            this.groupBoxT2.PerformLayout();
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT rd2;
        private JE.MyControl.RadioT rd1;
        private JE.MyControl.GroupBoxT groupBoxT2;
        private JE.MyControl.RadioT rd4;
        private JE.MyControl.RadioT rd3;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.LabelT lblT10;
        private JE.MyControl.LabelT lblT11;
        private JE.MyControl.LabelT lblT12;
        private JE.MyControl.TextBoxT fodate;
        private JE.MyControl.TextBoxT itno1;
        private JE.MyControl.TextBoxT memo;
        private JE.MyControl.TextBoxT esdate;
        private JE.MyControl.TextBoxT fano;
        private JE.MyControl.TextBoxT emno;
        private JE.MyControl.TextBoxT itno;
        private JE.MyControl.TextBoxT fodate1;
        private JE.MyControl.TextBoxT esdate1;
        private JE.MyControl.TextBoxT fano1;
        private JE.MyControl.TextBoxT emno1;
        private JE.MyControl.LabelT lblT13;
        private JE.MyControl.LabelT lblT14;
        private JE.MyControl.LabelT lblT16;
        private JE.MyControl.LabelT lblT17;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT spno;
        private JE.MyControl.TextBoxT spno1;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.StatusStripT statusStripT1; 
    }
}