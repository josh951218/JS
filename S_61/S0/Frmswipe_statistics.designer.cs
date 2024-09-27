namespace S_61.S0
{
    partial class Frmswipe_statistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmswipe_statistics));
            this.statusStripT1 = new JE.MyControl.StatusStripT();
            this.panelBtnT1 = new JE.MyControl.PanelBtnT();
            this.btnExit = new JE.MyControl.ButtonT();
            this.btnBrow = new JE.MyControl.ButtonT();
            this.panelT1 = new JE.MyControl.PanelT();
            this.cuno = new JE.MyControl.TextBoxT();
            this.cuno1 = new JE.MyControl.TextBoxT();
            this.lblT16 = new JE.MyControl.LabelT();
            this.labelT4 = new JE.MyControl.LabelT();
            this.machine = new JE.MyControl.TextBoxT();
            this.labelT3 = new JE.MyControl.LabelT();
            this.Day1 = new JE.MyControl.TextBoxT();
            this.Day = new JE.MyControl.TextBoxT();
            this.labelT2 = new JE.MyControl.LabelT();
            this.labelT1 = new JE.MyControl.LabelT();
            this.panelBtnT1.SuspendLayout();
            this.panelT1.SuspendLayout();
            this.SuspendLayout();
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
            // panelBtnT1
            // 
            this.panelBtnT1.Controls.Add(this.btnExit);
            this.panelBtnT1.Controls.Add(this.btnBrow);
            this.panelBtnT1.Location = new System.Drawing.Point(431, 541);
            this.panelBtnT1.Name = "panelBtnT1";
            this.panelBtnT1.Size = new System.Drawing.Size(148, 79);
            this.panelBtnT1.TabIndex = 1;
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
            // panelT1
            // 
            this.panelT1.Controls.Add(this.cuno);
            this.panelT1.Controls.Add(this.cuno1);
            this.panelT1.Controls.Add(this.lblT16);
            this.panelT1.Controls.Add(this.labelT4);
            this.panelT1.Controls.Add(this.machine);
            this.panelT1.Controls.Add(this.labelT3);
            this.panelT1.Controls.Add(this.Day1);
            this.panelT1.Controls.Add(this.Day);
            this.panelT1.Controls.Add(this.labelT2);
            this.panelT1.Controls.Add(this.labelT1);
            this.panelT1.Location = new System.Drawing.Point(163, 113);
            this.panelT1.Name = "panelT1";
            this.panelT1.Padding = new System.Windows.Forms.Padding(15);
            this.panelT1.Size = new System.Drawing.Size(684, 225);
            this.panelT1.TabIndex = 0;
            // 
            // cuno
            // 
            this.cuno.AllowGrayBackColor = false;
            this.cuno.AllowResize = true;
            this.cuno.Font = new System.Drawing.Font("細明體", 12F);
            this.cuno.Location = new System.Drawing.Point(240, 147);
            this.cuno.MaxLength = 10;
            this.cuno.Name = "cuno";
            this.cuno.oLen = 0;
            this.cuno.Size = new System.Drawing.Size(87, 27);
            this.cuno.TabIndex = 4;
            this.cuno.Tag = "客戶編號";
            this.cuno.DoubleClick += new System.EventHandler(this.cuno_DoubleClick);
            // 
            // cuno1
            // 
            this.cuno1.AllowGrayBackColor = false;
            this.cuno1.AllowResize = true;
            this.cuno1.Font = new System.Drawing.Font("細明體", 12F);
            this.cuno1.Location = new System.Drawing.Point(367, 147);
            this.cuno1.MaxLength = 10;
            this.cuno1.Name = "cuno1";
            this.cuno1.oLen = 0;
            this.cuno1.Size = new System.Drawing.Size(87, 27);
            this.cuno1.TabIndex = 5;
            this.cuno1.Tag = "客戶編號";
            this.cuno1.DoubleClick += new System.EventHandler(this.cuno_DoubleClick);
            // 
            // lblT16
            // 
            this.lblT16.AutoSize = true;
            this.lblT16.BackColor = System.Drawing.Color.Transparent;
            this.lblT16.Font = new System.Drawing.Font("細明體", 12F);
            this.lblT16.Location = new System.Drawing.Point(335, 152);
            this.lblT16.Name = "lblT16";
            this.lblT16.Size = new System.Drawing.Size(24, 16);
            this.lblT16.TabIndex = 0;
            this.lblT16.Text = "～";
            // 
            // labelT4
            // 
            this.labelT4.AutoSize = true;
            this.labelT4.BackColor = System.Drawing.Color.Transparent;
            this.labelT4.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT4.Location = new System.Drawing.Point(123, 152);
            this.labelT4.Name = "labelT4";
            this.labelT4.Size = new System.Drawing.Size(104, 16);
            this.labelT4.TabIndex = 0;
            this.labelT4.Text = "起訖客戶編號";
            // 
            // machine
            // 
            this.machine.AllowGrayBackColor = false;
            this.machine.AllowResize = true;
            this.machine.Font = new System.Drawing.Font("細明體", 12F);
            this.machine.Location = new System.Drawing.Point(240, 98);
            this.machine.MaxLength = 10;
            this.machine.Name = "machine";
            this.machine.oLen = 0;
            this.machine.Size = new System.Drawing.Size(87, 27);
            this.machine.TabIndex = 3;
            // 
            // labelT3
            // 
            this.labelT3.AutoSize = true;
            this.labelT3.BackColor = System.Drawing.Color.Transparent;
            this.labelT3.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT3.Location = new System.Drawing.Point(139, 103);
            this.labelT3.Name = "labelT3";
            this.labelT3.Size = new System.Drawing.Size(72, 16);
            this.labelT3.TabIndex = 0;
            this.labelT3.Text = "機台號碼";
            // 
            // Day1
            // 
            this.Day1.AllowGrayBackColor = false;
            this.Day1.AllowResize = true;
            this.Day1.Font = new System.Drawing.Font("細明體", 12F);
            this.Day1.Location = new System.Drawing.Point(367, 49);
            this.Day1.MaxLength = 10;
            this.Day1.Name = "Day1";
            this.Day1.oLen = 0;
            this.Day1.Size = new System.Drawing.Size(87, 27);
            this.Day1.TabIndex = 2;
            this.Day1.Validating += new System.ComponentModel.CancelEventHandler(this.Day_Validating);
            // 
            // Day
            // 
            this.Day.AllowGrayBackColor = false;
            this.Day.AllowResize = true;
            this.Day.Font = new System.Drawing.Font("細明體", 12F);
            this.Day.Location = new System.Drawing.Point(240, 49);
            this.Day.MaxLength = 10;
            this.Day.Name = "Day";
            this.Day.oLen = 0;
            this.Day.Size = new System.Drawing.Size(87, 27);
            this.Day.TabIndex = 1;
            this.Day.Validating += new System.ComponentModel.CancelEventHandler(this.Day_Validating);
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.BackColor = System.Drawing.Color.Transparent;
            this.labelT2.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT2.Location = new System.Drawing.Point(335, 54);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(24, 16);
            this.labelT2.TabIndex = 0;
            this.labelT2.Text = "～";
            // 
            // labelT1
            // 
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 12F);
            this.labelT1.Location = new System.Drawing.Point(139, 54);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(72, 16);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "起訖日期";
            // 
            // Frmswipe_statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.panelBtnT1);
            this.Controls.Add(this.panelT1);
            this.Controls.Add(this.statusStripT1);
            this.Name = "Frmswipe_statistics";
            this.Text = "機台刷卡統計表";
            this.panelBtnT1.ResumeLayout(false);
            this.panelT1.ResumeLayout(false);
            this.panelT1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStripT1;
        private JE.MyControl.PanelBtnT panelBtnT1;
        private JE.MyControl.ButtonT btnExit;
        private JE.MyControl.PanelT panelT1;
        private JE.MyControl.TextBoxT cuno;
        private JE.MyControl.TextBoxT cuno1;
        private JE.MyControl.LabelT lblT16;
        private JE.MyControl.LabelT labelT4;
        private JE.MyControl.TextBoxT machine;
        private JE.MyControl.LabelT labelT3;
        private JE.MyControl.TextBoxT Day1;
        private JE.MyControl.TextBoxT Day;
        private JE.MyControl.LabelT labelT2;
        private JE.MyControl.LabelT labelT1;
        private JE.MyControl.ButtonT btnBrow;
    }
}