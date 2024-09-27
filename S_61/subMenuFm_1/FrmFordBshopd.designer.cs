namespace S_61.subMenuFm_1
{
    partial class FrmFordBshopd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFordBshopd));
            this.groupBoxT1 = new JE.MyControl.GroupBoxT();
            this.radioT1 = new JE.MyControl.RadioT();
            this.radioT2 = new JE.MyControl.RadioT();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.lblT1 = new JE.MyControl.LabelT();
            this.lblT2 = new JE.MyControl.LabelT();
            this.FoNo1 = new JE.MyControl.TextBoxT();
            this.lblT3 = new JE.MyControl.LabelT();
            this.FoNo = new JE.MyControl.TextBoxT();
            this.lblT4 = new JE.MyControl.LabelT();
            this.lblT5 = new JE.MyControl.LabelT();
            this.lblT6 = new JE.MyControl.LabelT();
            this.panelT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.groupBoxT1.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxT1
            // 
            this.groupBoxT1.Controls.Add(this.radioT1);
            this.groupBoxT1.Controls.Add(this.radioT2);
            this.groupBoxT1.Font = new System.Drawing.Font("細明體", 12F);
            this.groupBoxT1.Location = new System.Drawing.Point(219, 79);
            this.groupBoxT1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.groupBoxT1.Name = "groupBoxT1";
            this.groupBoxT1.Size = new System.Drawing.Size(572, 84);
            this.groupBoxT1.TabIndex = 3;
            this.groupBoxT1.TabStop = false;
            this.groupBoxT1.Text = "報表內容";
            // 
            // radioT1
            // 
            this.radioT1.AutoSize = true;
            this.radioT1.BackColor = System.Drawing.Color.LightBlue;
            this.radioT1.Checked = true;
            this.radioT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT1.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT1.Location = new System.Drawing.Point(76, 39);
            this.radioT1.Name = "radioT1";
            this.radioT1.Size = new System.Drawing.Size(138, 20);
            this.radioT1.TabIndex = 0;
            this.radioT1.Tag = "進(退)貨明細表";
            this.radioT1.Text = "進(退)貨明細表";
            this.radioT1.UseVisualStyleBackColor = false;
            // 
            // radioT2
            // 
            this.radioT2.AutoSize = true;
            this.radioT2.BackColor = System.Drawing.Color.Transparent;
            this.radioT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioT2.Font = new System.Drawing.Font("細明體", 12F);
            this.radioT2.Location = new System.Drawing.Point(359, 39);
            this.radioT2.Name = "radioT2";
            this.radioT2.Size = new System.Drawing.Size(138, 20);
            this.radioT2.TabIndex = 1;
            this.radioT2.Tag = "已交、未交總表";
            this.radioT2.Text = "已交、未交總表";
            this.radioT2.UseVisualStyleBackColor = true;
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.lblT1);
            this.pnlBoxT1.Controls.Add(this.lblT2);
            this.pnlBoxT1.Controls.Add(this.FoNo1);
            this.pnlBoxT1.Controls.Add(this.lblT3);
            this.pnlBoxT1.Controls.Add(this.FoNo);
            this.pnlBoxT1.Location = new System.Drawing.Point(219, 179);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(572, 197);
            this.pnlBoxT1.TabIndex = 1;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.BackColor = System.Drawing.Color.Transparent;
            this.lblT1.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT1.Location = new System.Drawing.Point(59, 90);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(88, 16);
            this.lblT1.TabIndex = 0;
            this.lblT1.Text = "請輸入列印";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.BackColor = System.Drawing.Color.Transparent;
            this.lblT2.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT2.Location = new System.Drawing.Point(201, 63);
            this.lblT2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(104, 16);
            this.lblT2.TabIndex = 0;
            this.lblT2.Text = "起始採購憑證";
            // 
            // FoNo1
            // 
            this.FoNo1.AllowGrayBackColor = false;
            this.FoNo1.AllowResize = true;
            this.FoNo1.Font = new System.Drawing.Font("細明體", 12F);
            this.FoNo1.Location = new System.Drawing.Point(312, 124);
            this.FoNo1.MaxLength = 16;
            this.FoNo1.Name = "FoNo1";
            this.FoNo1.oLen = 0;
            this.FoNo1.Size = new System.Drawing.Size(135, 27);
            this.FoNo1.TabIndex = 4;
            this.FoNo1.DoubleClick += new System.EventHandler(this.FoNo_DoubleClick);
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.BackColor = System.Drawing.Color.Transparent;
            this.lblT3.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT3.Location = new System.Drawing.Point(201, 129);
            this.lblT3.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(104, 16);
            this.lblT3.TabIndex = 0;
            this.lblT3.Text = "終止採購憑證";
            // 
            // FoNo
            // 
            this.FoNo.AllowGrayBackColor = false;
            this.FoNo.AllowResize = true;
            this.FoNo.Font = new System.Drawing.Font("細明體", 12F);
            this.FoNo.Location = new System.Drawing.Point(312, 58);
            this.FoNo.MaxLength = 16;
            this.FoNo.Name = "FoNo";
            this.FoNo.oLen = 0;
            this.FoNo.Size = new System.Drawing.Size(135, 27);
            this.FoNo.TabIndex = 3;
            this.FoNo.DoubleClick += new System.EventHandler(this.FoNo_DoubleClick);
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.BackColor = System.Drawing.Color.Transparent;
            this.lblT4.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT4.Location = new System.Drawing.Point(365, 447);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(280, 16);
            this.lblT4.TabIndex = 0;
            this.lblT4.Text = "＊起始編號空白表示從第一筆開始列印";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.BackColor = System.Drawing.Color.Transparent;
            this.lblT5.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT5.Location = new System.Drawing.Point(365, 471);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(248, 16);
            this.lblT5.TabIndex = 0;
            this.lblT5.Text = "＊終止編號空白表示印至最後一筆";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.BackColor = System.Drawing.Color.Transparent;
            this.lblT6.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT6.Location = new System.Drawing.Point(365, 495);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(200, 16);
            this.lblT6.TabIndex = 0;
            this.lblT6.Text = "＊二者皆空白表示全部列印";
            // 
            // panelT1
            // 
            this.panelT1.AutoSize = true;
            this.panelT1.Controls.Add(this.btnExit);
            this.panelT1.Controls.Add(this.btnBrow);
            this.panelT1.Location = new System.Drawing.Point(431, 542);
            this.panelT1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelT1.Name = "panelT1";
            this.panelT1.Size = new System.Drawing.Size(148, 82);
            this.panelT1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(69, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 79);
            this.btnExit.TabIndex = 1;
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
            this.btnBrow.Location = new System.Drawing.Point(0, 0);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(69, 79);
            this.btnBrow.TabIndex = 0;
            this.btnBrow.UseDefaultSettings = false;
            this.btnBrow.UseVisualStyleBackColor = false;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
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
            // FrmFordBshopd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 647);
            this.Controls.Add(this.groupBoxT1);
            this.Controls.Add(this.lblT4);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.statusStripT1);
            this.Controls.Add(this.lblT5);
            this.Controls.Add(this.lblT6);
            this.Controls.Add(this.panelT1);
            this.Name = "FrmFordBshopd";
            this.Tag = "採購別進貨明細表";
            this.Text = "採購單進貨明細表";
            this.Shown += new System.EventHandler(this.FrmFordBshopd_Shown);
            this.groupBoxT1.ResumeLayout(false);
            this.groupBoxT1.PerformLayout();
            this.pnlBoxT1.ResumeLayout(false);
            this.pnlBoxT1.PerformLayout();
            this.panelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.GroupBoxT groupBoxT1;
        private JE.MyControl.RadioT radioT1;
        private JE.MyControl.RadioT radioT2;
        private JE.MyControl.PanelT pnlBoxT1;
        private JE.MyControl.LabelT lblT1;
        private JE.MyControl.LabelT lblT2;
        private JE.MyControl.LabelT lblT3;
        private JE.MyControl.TextBoxT FoNo;
        private JE.MyControl.TextBoxT FoNo1;
        private JE.MyControl.LabelT lblT4;
        private JE.MyControl.LabelT lblT5;
        private JE.MyControl.LabelT lblT6;
        private JE.MyControl.PanelBtnT panelT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.ButtonT btnBrow;
        private JE.MyControl.StatusStripT statusStripT1; 

    }
}