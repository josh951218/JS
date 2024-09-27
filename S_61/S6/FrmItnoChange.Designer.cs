namespace JBS.S6
{
    partial class FrmItnoChange
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
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.BItNo = new JE.MyControl.TextBoxT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.ItUnitP = new JE.MyControl.TextBoxT();
            this.ItUnit = new JE.MyControl.TextBoxT();
            this.ItName = new JE.MyControl.TextBoxT();
            this.pnlBoxT2 = new JE.MyControl.PanelT();
            this.NItNo = new JE.MyControl.TextBoxT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.btnCancel = new JE.MyControl.ButtonSmallT();
            this.btnSave = new JE.MyControl.ButtonSmallT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.pnlBoxT1.SuspendLayout();
            this.pnlBoxT2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.BItNo);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.lblT4);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.ItUnitP);
            this.pnlBoxT1.Controls.Add(this.ItUnit);
            this.pnlBoxT1.Controls.Add(this.ItName);
            this.pnlBoxT1.Location = new System.Drawing.Point(163, 79);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(684, 254);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(154, 47);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(72, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "產品編號";
            // 
            // BItNo
            // 
            this.BItNo.AllowGrayBackColor = false;
            this.BItNo.AllowResize = true;
            this.BItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.BItNo.Location = new System.Drawing.Point(284, 42);
            this.BItNo.MaxLength = 20;
            this.BItNo.Name = "BItNo";
            this.BItNo.oLen = 0;
            this.BItNo.Size = new System.Drawing.Size(167, 27);
            this.BItNo.TabIndex = 0;
            this.BItNo.DoubleClick += new System.EventHandler(this.BItNo_DoubleClick);
            this.BItNo.Validating += new System.ComponentModel.CancelEventHandler(this.BItNo_Validating);
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(154, 95);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(72, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "品名規格";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(154, 191);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(72, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "包裝單位";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(154, 143);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(72, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "單    位";
            // 
            // ItUnitP
            // 
            this.ItUnitP.AllowGrayBackColor = true;
            this.ItUnitP.AllowResize = true;
            this.ItUnitP.BackColor = System.Drawing.Color.Silver;
            this.ItUnitP.Font = new System.Drawing.Font("細明體", 12F);
            this.ItUnitP.Location = new System.Drawing.Point(284, 186);
            this.ItUnitP.MaxLength = 4;
            this.ItUnitP.Name = "ItUnitP";
            this.ItUnitP.oLen = 0;
            this.ItUnitP.ReadOnly = true;
            this.ItUnitP.Size = new System.Drawing.Size(39, 27);
            this.ItUnitP.TabIndex = 3;
            this.ItUnitP.TabStop = false;
            // 
            // ItUnit
            // 
            this.ItUnit.AllowGrayBackColor = true;
            this.ItUnit.AllowResize = true;
            this.ItUnit.BackColor = System.Drawing.Color.Silver;
            this.ItUnit.Font = new System.Drawing.Font("細明體", 12F);
            this.ItUnit.Location = new System.Drawing.Point(284, 138);
            this.ItUnit.MaxLength = 4;
            this.ItUnit.Name = "ItUnit";
            this.ItUnit.oLen = 0;
            this.ItUnit.ReadOnly = true;
            this.ItUnit.Size = new System.Drawing.Size(39, 27);
            this.ItUnit.TabIndex = 2;
            this.ItUnit.TabStop = false;
            // 
            // ItName
            // 
            this.ItName.AllowGrayBackColor = true;
            this.ItName.AllowResize = true;
            this.ItName.BackColor = System.Drawing.Color.Silver;
            this.ItName.Font = new System.Drawing.Font("細明體", 12F);
            this.ItName.Location = new System.Drawing.Point(284, 90);
            this.ItName.MaxLength = 30;
            this.ItName.Name = "ItName";
            this.ItName.oLen = 0;
            this.ItName.ReadOnly = true;
            this.ItName.Size = new System.Drawing.Size(247, 27);
            this.ItName.TabIndex = 1;
            this.ItName.TabStop = false;
            // 
            // pnlBoxT2
            // 
            this.pnlBoxT2.Controls.Add(this.NItNo);
            this.pnlBoxT2.Controls.Add(this.lblT5);
            this.pnlBoxT2.Location = new System.Drawing.Point(163, 344);
            this.pnlBoxT2.Name = "pnlBoxT2";
            this.pnlBoxT2.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT2.Size = new System.Drawing.Size(684, 98);
            this.pnlBoxT2.TabIndex = 2;
            // 
            // NItNo
            // 
            this.NItNo.AllowGrayBackColor = false;
            this.NItNo.AllowResize = true;
            this.NItNo.Font = new System.Drawing.Font("細明體", 12F);
            this.NItNo.Location = new System.Drawing.Point(284, 30);
            this.NItNo.MaxLength = 20;
            this.NItNo.Name = "NItNo";
            this.NItNo.oLen = 0;
            this.NItNo.Size = new System.Drawing.Size(167, 27);
            this.NItNo.TabIndex = 0;
            this.NItNo.Validating += new System.ComponentModel.CancelEventHandler(this.NItNo_Validating);
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(138, 35);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(88, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "新產品編號";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("細明體", 12F);
            this.btnCancel.Location = new System.Drawing.Point(544, 546);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(177, 47);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "放棄";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("細明體", 12F);
            this.btnSave.Location = new System.Drawing.Point(290, 546);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(195, 47);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // FrmItnoChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.pnlBoxT2);
            this.Name = "FrmItnoChange";
            this.Tag = "產品編號修改";
            this.Text = "產品編號修改作業";
            this.Load += new System.EventHandler(this.FrmItnoChange_Load);
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.pnlBoxT2.ResumeLayout(false);
            this.pnlBoxT2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.PanelT pnlBoxT2;
        private JE.MyControl.ButtonSmallT btnCancel;
        private JE.MyControl.ButtonSmallT btnSave;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT BItNo;
        private JE.MyControl.TextBoxT ItName;
        private JE.MyControl.TextBoxT ItUnit;
        private JE.MyControl.TextBoxT ItUnitP;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.TextBoxT NItNo;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.StatusStripT statusStripT1;
    }
}