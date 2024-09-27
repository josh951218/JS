namespace S_61.subMenuFm_1
{
    partial class FrmOrder_Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrder_Info));
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
            this.memo = new JE.MyControl.TextBoxT();
            this.lblT11 = new JE.MyControl.LabelT();
            this.lblT10 = new JE.MyControl.LabelT();
            this.itno1 = new JE.MyControl.TextBoxT();
            this.lblT9 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.itno = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.orno = new JE.MyControl.TextBoxT();
            this.spno1 = new JE.MyControl.TextBoxT();
            this.spno = new JE.MyControl.TextBoxT();
            this.lblT18 = new JE.MyControl.LabelT();
            this.lblT12 = new JE.MyControl.LabelT();
            this.emno = new JE.MyControl.TextBoxT();
            this.cuno = new JE.MyControl.TextBoxT();
            this.ordate = new JE.MyControl.TextBoxT();
            this.emno1 = new JE.MyControl.TextBoxT();
            this.cuno1 = new JE.MyControl.TextBoxT();
            this.lblT17 = new JE.MyControl.LabelT();
            this.orno1 = new JE.MyControl.TextBoxT();
            this.lblT13 = new JE.MyControl.LabelT();
            this.lblT16 = new JE.MyControl.LabelT();
            this.ordate1 = new JE.MyControl.TextBoxT();
            this.esdate = new JE.MyControl.TextBoxT();
            this.lblT15 = new JE.MyControl.LabelT();
            this.lblT14 = new JE.MyControl.LabelT();
            this.esdate1 = new JE.MyControl.TextBoxT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStrip1 = new JE.MyControl.StatusStripT();
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
            this.lblT7.Location = new System.Drawing.Point(361, 501);
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
            this.lblT8.Location = new System.Drawing.Point(361, 523);
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
            this.lblT6.Location = new System.Drawing.Point(361, 479);
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
            this.groupBoxT1.Location = new System.Drawing.Point(92, 20);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(403, 72);
            this.groupBoxT1.TabIndex = 1;
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
            this.rd2.Location = new System.Drawing.Point(233, 34);
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
            this.rd1.Location = new System.Drawing.Point(127, 34);
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
            this.groupBoxT2.Location = new System.Drawing.Point(515, 20);
            this.groupBoxT2.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT2.Name = "groupBoxT2";
            this.groupBoxT2.Size = new System.Drawing.Size(403, 72);
            this.groupBoxT2.TabIndex = 2;
            this.groupBoxT2.TabStop = false;
            this.groupBoxT2.Text = "已結案訂單是否顯示";
            // 
            // rd4
            // 
            this.rd4.AutoSize = true;
            this.rd4.BackColor = System.Drawing.Color.LightBlue;
            this.rd4.Checked = true;
            this.rd4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rd4.Font = new System.Drawing.Font("細明體", 12F);
            this.rd4.Location = new System.Drawing.Point(233, 34);
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
            this.rd3.Location = new System.Drawing.Point(127, 34);
            this.rd3.Name = "rd3";
            this.rd3.Size = new System.Drawing.Size(42, 20);
            this.rd3.TabIndex = 0;
            this.rd3.Tag = "是";
            this.rd3.Text = "是";
            this.rd3.UseVisualStyleBackColor = true;
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.memo);
            this.pnlBoxT1.Controls.Add(this.lblT11);
            this.pnlBoxT1.Controls.Add(this.lblT10);
            this.pnlBoxT1.Controls.Add(this.itno1);
            this.pnlBoxT1.Controls.Add(this.lblT9);
            this.pnlBoxT1.Controls.Add(this.lblT5);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.itno);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.orno);
            this.pnlBoxT1.Controls.Add(this.spno1);
            this.pnlBoxT1.Controls.Add(this.spno);
            this.pnlBoxT1.Controls.Add(this.lblT18);
            this.pnlBoxT1.Controls.Add(this.lblT12);
            this.pnlBoxT1.Controls.Add(this.emno);
            this.pnlBoxT1.Controls.Add(this.cuno);
            this.pnlBoxT1.Controls.Add(this.ordate);
            this.pnlBoxT1.Controls.Add(this.emno1);
            this.pnlBoxT1.Controls.Add(this.cuno1);
            this.pnlBoxT1.Controls.Add(this.lblT17);
            this.pnlBoxT1.Controls.Add(this.orno1);
            this.pnlBoxT1.Controls.Add(this.lblT13);
            this.pnlBoxT1.Controls.Add(this.lblT16);
            this.pnlBoxT1.Controls.Add(this.ordate1);
            this.pnlBoxT1.Controls.Add(this.esdate);
            this.pnlBoxT1.Controls.Add(this.lblT15);
            this.pnlBoxT1.Controls.Add(this.lblT14);
            this.pnlBoxT1.Controls.Add(this.esdate1);
            this.pnlBoxT1.Location = new System.Drawing.Point(92, 100);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(826, 373);
            this.pnlBoxT1.TabIndex = 3;
            // 
            // memo
            // 
            this.memo.AllowGrayBackColor = false;
            this.memo.AllowResize = true;
            this.memo.Font = new System.Drawing.Font("細明體", 12F);
            this.memo.Location = new System.Drawing.Point(364, 313);
            this.memo.MaxLength = 20;
            this.memo.Name = "memo";
            this.memo.oLen = 0;
            this.memo.Size = new System.Drawing.Size(167, 27);
            this.memo.TabIndex = 15;
            this.memo.DoubleClick += new System.EventHandler(this.ormemo_DoubleClick);
            // 
            // lblT11
            // 
            this.lblT11.AutoSize = true;
            this.lblT11.BackColor = System.Drawing.Color.Transparent;
            this.lblT11.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT11.Location = new System.Drawing.Point(250, 318);
            this.lblT11.Name = "lblT11";
            this.lblT11.Size = new System.Drawing.Size(72, 16);
            this.lblT11.TabIndex = 0;
            this.lblT11.Text = "備註說明";
            // 
            // lblT10
            // 
            this.lblT10.AutoSize = true;
            this.lblT10.BackColor = System.Drawing.Color.Transparent;
            this.lblT10.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT10.Location = new System.Drawing.Point(250, 283);
            this.lblT10.Name = "lblT10";
            this.lblT10.Size = new System.Drawing.Size(104, 16);
            this.lblT10.TabIndex = 0;
            this.lblT10.Text = "終止產品編號";
            // 
            // itno1
            // 
            this.itno1.AllowGrayBackColor = false;
            this.itno1.AllowResize = true;
            this.itno1.Font = new System.Drawing.Font("細明體", 12F);
            this.itno1.Location = new System.Drawing.Point(364, 278);
            this.itno1.MaxLength = 20;
            this.itno1.Name = "itno1";
            this.itno1.oLen = 0;
            this.itno1.Size = new System.Drawing.Size(167, 27);
            this.itno1.TabIndex = 14;
            this.itno1.Tag = "產品編號";
            this.itno1.DoubleClick += new System.EventHandler(this.itno_DoubleClick);
            this.itno1.Validating += new System.ComponentModel.CancelEventHandler(this.itno_Validating);
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.BackColor = System.Drawing.Color.Transparent;
            this.lblT9.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT9.Location = new System.Drawing.Point(250, 248);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(104, 16);
            this.lblT9.TabIndex = 0;
            this.lblT9.Text = "起迄產品編號";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(250, 178);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(104, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "起迄業務人員";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(250, 143);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(104, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "起迄客戶編號";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(250, 108);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "起迄訂單憑證";
            // 
            // itno
            // 
            this.itno.AllowGrayBackColor = false;
            this.itno.AllowResize = true;
            this.itno.Font = new System.Drawing.Font("細明體", 12F);
            this.itno.Location = new System.Drawing.Point(364, 243);
            this.itno.MaxLength = 20;
            this.itno.Name = "itno";
            this.itno.oLen = 0;
            this.itno.Size = new System.Drawing.Size(167, 27);
            this.itno.TabIndex = 13;
            this.itno.Tag = "產品編號";
            this.itno.DoubleClick += new System.EventHandler(this.itno_DoubleClick);
            this.itno.Validating += new System.ComponentModel.CancelEventHandler(this.itno_Validating);
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(250, 73);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起迄交貨日期";
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(250, 38);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(104, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "起迄訂單日期";
            // 
            // orno
            // 
            this.orno.AllowGrayBackColor = false;
            this.orno.AllowResize = true;
            this.orno.Font = new System.Drawing.Font("細明體", 12F);
            this.orno.Location = new System.Drawing.Point(364, 103);
            this.orno.MaxLength = 16;
            this.orno.Name = "orno";
            this.orno.oLen = 0;
            this.orno.Size = new System.Drawing.Size(135, 27);
            this.orno.TabIndex = 5;
            this.orno.Tag = "訂單憑證";
            this.orno.DoubleClick += new System.EventHandler(this.orno_DoubleClick);
            this.orno.Validating += new System.ComponentModel.CancelEventHandler(this.orno_Validating);
            // 
            // spno1
            // 
            this.spno1.AllowGrayBackColor = false;
            this.spno1.AllowResize = true;
            this.spno1.Font = new System.Drawing.Font("細明體", 12F);
            this.spno1.Location = new System.Drawing.Point(567, 208);
            this.spno1.MaxLength = 4;
            this.spno1.Name = "spno1";
            this.spno1.oLen = 0;
            this.spno1.Size = new System.Drawing.Size(39, 27);
            this.spno1.TabIndex = 12;
            this.spno1.Tag = "專案編號";
            this.spno1.DoubleClick += new System.EventHandler(this.spno_DoubleClick);
            this.spno1.Validating += new System.ComponentModel.CancelEventHandler(this.spno_Validating);
            // 
            // spno
            // 
            this.spno.AllowGrayBackColor = false;
            this.spno.AllowResize = true;
            this.spno.Font = new System.Drawing.Font("細明體", 12F);
            this.spno.Location = new System.Drawing.Point(364, 208);
            this.spno.MaxLength = 4;
            this.spno.Name = "spno";
            this.spno.oLen = 0;
            this.spno.Size = new System.Drawing.Size(39, 27);
            this.spno.TabIndex = 11;
            this.spno.Tag = "專案編號";
            this.spno.DoubleClick += new System.EventHandler(this.spno_DoubleClick);
            this.spno.Validating += new System.ComponentModel.CancelEventHandler(this.spno_Validating);
            // 
            // lblT18
            // 
            this.lblT18.AutoSize = true;
            this.lblT18.BackColor = System.Drawing.Color.Transparent;
            this.lblT18.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT18.Location = new System.Drawing.Point(250, 213);
            this.lblT18.Name = "lblT18";
            this.lblT18.Size = new System.Drawing.Size(104, 16);
            this.lblT18.TabIndex = 0;
            this.lblT18.Text = "起迄專案編號";
            // 
            // lblT12
            // 
            this.lblT12.AutoSize = true;
            this.lblT12.BackColor = System.Drawing.Color.Transparent;
            this.lblT12.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT12.Location = new System.Drawing.Point(124, 178);
            this.lblT12.Name = "lblT12";
            this.lblT12.Size = new System.Drawing.Size(88, 16);
            this.lblT12.TabIndex = 0;
            this.lblT12.Text = "請輸入列印";
            // 
            // emno
            // 
            this.emno.AllowGrayBackColor = false;
            this.emno.AllowResize = true;
            this.emno.Font = new System.Drawing.Font("細明體", 12F);
            this.emno.Location = new System.Drawing.Point(364, 173);
            this.emno.MaxLength = 4;
            this.emno.Name = "emno";
            this.emno.oLen = 0;
            this.emno.Size = new System.Drawing.Size(39, 27);
            this.emno.TabIndex = 9;
            this.emno.Tag = "業務人員";
            this.emno.DoubleClick += new System.EventHandler(this.emno_DoubleClick);
            this.emno.Validating += new System.ComponentModel.CancelEventHandler(this.emno_Validating);
            // 
            // cuno
            // 
            this.cuno.AllowGrayBackColor = false;
            this.cuno.AllowResize = true;
            this.cuno.Font = new System.Drawing.Font("細明體", 12F);
            this.cuno.Location = new System.Drawing.Point(364, 138);
            this.cuno.MaxLength = 10;
            this.cuno.Name = "cuno";
            this.cuno.oLen = 0;
            this.cuno.Size = new System.Drawing.Size(87, 27);
            this.cuno.TabIndex = 7;
            this.cuno.Tag = "客戶編號";
            this.cuno.DoubleClick += new System.EventHandler(this.cuno_DoubleClick);
            this.cuno.Validating += new System.ComponentModel.CancelEventHandler(this.cuno_Validating);
            // 
            // ordate
            // 
            this.ordate.AllowGrayBackColor = false;
            this.ordate.AllowResize = true;
            this.ordate.Font = new System.Drawing.Font("細明體", 12F);
            this.ordate.Location = new System.Drawing.Point(364, 33);
            this.ordate.MaxLength = 8;
            this.ordate.Name = "ordate";
            this.ordate.oLen = 0;
            this.ordate.Size = new System.Drawing.Size(71, 27);
            this.ordate.TabIndex = 1;
            this.ordate.Tag = "訂單日期";
            this.ordate.Validating += new System.ComponentModel.CancelEventHandler(this.ordate_Validating);
            // 
            // emno1
            // 
            this.emno1.AllowGrayBackColor = false;
            this.emno1.AllowResize = true;
            this.emno1.Font = new System.Drawing.Font("細明體", 12F);
            this.emno1.Location = new System.Drawing.Point(567, 173);
            this.emno1.MaxLength = 4;
            this.emno1.Name = "emno1";
            this.emno1.oLen = 0;
            this.emno1.Size = new System.Drawing.Size(39, 27);
            this.emno1.TabIndex = 10;
            this.emno1.Tag = "業務人員";
            this.emno1.DoubleClick += new System.EventHandler(this.emno_DoubleClick);
            this.emno1.Validating += new System.ComponentModel.CancelEventHandler(this.emno_Validating);
            // 
            // cuno1
            // 
            this.cuno1.AllowGrayBackColor = false;
            this.cuno1.AllowResize = true;
            this.cuno1.Font = new System.Drawing.Font("細明體", 12F);
            this.cuno1.Location = new System.Drawing.Point(567, 138);
            this.cuno1.MaxLength = 10;
            this.cuno1.Name = "cuno1";
            this.cuno1.oLen = 0;
            this.cuno1.Size = new System.Drawing.Size(87, 27);
            this.cuno1.TabIndex = 8;
            this.cuno1.Tag = "客戶編號";
            this.cuno1.DoubleClick += new System.EventHandler(this.cuno_DoubleClick);
            this.cuno1.Validating += new System.ComponentModel.CancelEventHandler(this.cuno_Validating);
            // 
            // lblT17
            // 
            this.lblT17.AutoSize = true;
            this.lblT17.BackColor = System.Drawing.Color.Transparent;
            this.lblT17.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT17.Location = new System.Drawing.Point(510, 178);
            this.lblT17.Name = "lblT17";
            this.lblT17.Size = new System.Drawing.Size(24, 16);
            this.lblT17.TabIndex = 0;
            this.lblT17.Text = "～";
            // 
            // orno1
            // 
            this.orno1.AllowGrayBackColor = false;
            this.orno1.AllowResize = true;
            this.orno1.Font = new System.Drawing.Font("細明體", 12F);
            this.orno1.Location = new System.Drawing.Point(567, 103);
            this.orno1.MaxLength = 16;
            this.orno1.Name = "orno1";
            this.orno1.oLen = 0;
            this.orno1.Size = new System.Drawing.Size(135, 27);
            this.orno1.TabIndex = 6;
            this.orno1.Tag = "訂單憑證";
            this.orno1.DoubleClick += new System.EventHandler(this.orno_DoubleClick);
            this.orno1.Validating += new System.ComponentModel.CancelEventHandler(this.orno_Validating);
            // 
            // lblT13
            // 
            this.lblT13.AutoSize = true;
            this.lblT13.BackColor = System.Drawing.Color.Transparent;
            this.lblT13.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT13.Location = new System.Drawing.Point(510, 38);
            this.lblT13.Name = "lblT13";
            this.lblT13.Size = new System.Drawing.Size(24, 16);
            this.lblT13.TabIndex = 0;
            this.lblT13.Text = "～";
            // 
            // lblT16
            // 
            this.lblT16.AutoSize = true;
            this.lblT16.BackColor = System.Drawing.Color.Transparent;
            this.lblT16.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT16.Location = new System.Drawing.Point(510, 143);
            this.lblT16.Name = "lblT16";
            this.lblT16.Size = new System.Drawing.Size(24, 16);
            this.lblT16.TabIndex = 0;
            this.lblT16.Text = "～";
            // 
            // ordate1
            // 
            this.ordate1.AllowGrayBackColor = false;
            this.ordate1.AllowResize = true;
            this.ordate1.Font = new System.Drawing.Font("細明體", 12F);
            this.ordate1.Location = new System.Drawing.Point(567, 33);
            this.ordate1.MaxLength = 8;
            this.ordate1.Name = "ordate1";
            this.ordate1.oLen = 0;
            this.ordate1.Size = new System.Drawing.Size(71, 27);
            this.ordate1.TabIndex = 2;
            this.ordate1.Tag = "訂單日期";
            this.ordate1.Validating += new System.ComponentModel.CancelEventHandler(this.ordate_Validating);
            // 
            // esdate
            // 
            this.esdate.AllowGrayBackColor = false;
            this.esdate.AllowResize = true;
            this.esdate.Font = new System.Drawing.Font("細明體", 12F);
            this.esdate.Location = new System.Drawing.Point(364, 68);
            this.esdate.MaxLength = 8;
            this.esdate.Name = "esdate";
            this.esdate.oLen = 0;
            this.esdate.Size = new System.Drawing.Size(71, 27);
            this.esdate.TabIndex = 3;
            this.esdate.Tag = "交貨日期";
            this.esdate.Validating += new System.ComponentModel.CancelEventHandler(this.esdate_Validating);
            // 
            // lblT15
            // 
            this.lblT15.AutoSize = true;
            this.lblT15.BackColor = System.Drawing.Color.Transparent;
            this.lblT15.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT15.Location = new System.Drawing.Point(510, 108);
            this.lblT15.Name = "lblT15";
            this.lblT15.Size = new System.Drawing.Size(24, 16);
            this.lblT15.TabIndex = 0;
            this.lblT15.Text = "～";
            // 
            // lblT14
            // 
            this.lblT14.AutoSize = true;
            this.lblT14.BackColor = System.Drawing.Color.Transparent;
            this.lblT14.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT14.Location = new System.Drawing.Point(510, 73);
            this.lblT14.Name = "lblT14";
            this.lblT14.Size = new System.Drawing.Size(24, 16);
            this.lblT14.TabIndex = 0;
            this.lblT14.Text = "～";
            // 
            // esdate1
            // 
            this.esdate1.AllowGrayBackColor = false;
            this.esdate1.AllowResize = true;
            this.esdate1.Font = new System.Drawing.Font("細明體", 12F);
            this.esdate1.Location = new System.Drawing.Point(567, 68);
            this.esdate1.MaxLength = 8;
            this.esdate1.Name = "esdate1";
            this.esdate1.oLen = 0;
            this.esdate1.Size = new System.Drawing.Size(71, 27);
            this.esdate1.TabIndex = 4;
            this.esdate1.Tag = "交貨日期";
            this.esdate1.Validating += new System.ComponentModel.CancelEventHandler(this.esdate_Validating);
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
            this.panelT1.TabIndex = 4;
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
            this.btnExit.TabIndex = 2;
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
            this.btnBrow.TabIndex = 1;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
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
            // FrmOrder_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.lblT8);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.lblT7);
            this.Controls.Add(this.groupBoxT2);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmOrder_Info";
            this.Tag = "訂單資料瀏覽";
            this.Text = "訂單資料瀏覽";
            this.Load += new System.EventHandler(this.FrmOrderInfo_Load);
            this.Shown += new System.EventHandler(this.FrmOrder_Info_Shown);
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

        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT rd2;
        private JE.MyControl.RadioT rd1;
        private JE.MyControl.GroupBoxT groupBoxT2;
        private JE.MyControl.RadioT rd4;
        private JE.MyControl.RadioT rd3;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.LabelT lblT7;
        private JE.MyControl.LabelT lblT8;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT9;
        private JE.MyControl.LabelT lblT10;
        private JE.MyControl.LabelT lblT11;
        private JE.MyControl.LabelT lblT12;
        private JE.MyControl.TextBoxT ordate;
        private JE.MyControl.TextBoxT orno;
        private JE.MyControl.TextBoxT emno;
        private JE.MyControl.TextBoxT cuno;
        private JE.MyControl.TextBoxT itno;
        private JE.MyControl.TextBoxT itno1;
        private JE.MyControl.TextBoxT memo;
        private JE.MyControl.TextBoxT esdate;
        private JE.MyControl.TextBoxT ordate1;
        private JE.MyControl.TextBoxT esdate1;
        private JE.MyControl.TextBoxT orno1;
        private JE.MyControl.TextBoxT cuno1;
        private JE.MyControl.TextBoxT emno1;
        private JE.MyControl.LabelT lblT13;
        private JE.MyControl.LabelT lblT14;
        private JE.MyControl.LabelT lblT15;
        private JE.MyControl.LabelT lblT16;
        private JE.MyControl.LabelT lblT17;
        private JE.MyControl.LabelT lblT18;
        private JE.MyControl.TextBoxT spno;
        private JE.MyControl.TextBoxT spno1;
        private JE.MyControl.StatusStripT statusStrip1;
    }
}