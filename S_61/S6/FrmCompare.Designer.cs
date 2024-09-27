namespace JBS.S6
{
    partial class FrmCompare
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
            this.statusStrip1 = new JE.MyControl.StatusStripT();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlBoxT1 = new JE.MyControl.PanelT();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMny = new JE.MyControl.ButtonSmallT();
            this.btnBrowT3 = new JE.MyControl.ButtonSmallT();
            this.btnBrowT4 = new JE.MyControl.ButtonSmallT();
            this.btnExit = new JE.MyControl.ButtonSmallT();
            this.btnStock = new JE.MyControl.ButtonSmallT();
            this.buttonSmallT1 = new JE.MyControl.ButtonSmallT();
            this.btnBarCode = new JE.MyControl.ButtonSmallT();
            this.btnTransDay = new JE.MyControl.ButtonSmallT();
            this.PanelT1 = new JE.MyControl.PanelNT();
            this.buttonSmallT3 = new JE.MyControl.ButtonSmallT();
            this.btnBatchStock = new JE.MyControl.ButtonSmallT();
            this.buttonSmallT2 = new JE.MyControl.ButtonSmallT();
            this.statusStrip1.SuspendLayout();
            this.pnlBoxT1.SuspendLayout();
            this.PanelT1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(954, 20);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(41, 20);
            this.toolStripStatusLabel3.Text = "插入";
            // 
            // pnlBoxT1
            // 
            this.pnlBoxT1.Controls.Add(this.label1);
            this.pnlBoxT1.Location = new System.Drawing.Point(0, 0);
            this.pnlBoxT1.Name = "pnlBoxT1";
            this.pnlBoxT1.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBoxT1.Size = new System.Drawing.Size(1010, 94);
            this.pnlBoxT1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(980, 64);
            this.label1.TabIndex = 0;
            this.label1.Text = "此作業將重新計算庫存數量與帳款資料，請先將其他工作站關閉後再執行\r\n，否則將無法順利完成此作業";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMny
            // 
            this.btnMny.Font = new System.Drawing.Font("細明體", 12F);
            this.btnMny.Location = new System.Drawing.Point(115, 103);
            this.btnMny.Name = "btnMny";
            this.btnMny.Size = new System.Drawing.Size(371, 54);
            this.btnMny.TabIndex = 2;
            this.btnMny.Text = "帳款比對";
            this.btnMny.UseVisualStyleBackColor = true;
            this.btnMny.Click += new System.EventHandler(this.btnMny_Click);
            // 
            // btnBrowT3
            // 
            this.btnBrowT3.Font = new System.Drawing.Font("細明體", 12F);
            this.btnBrowT3.Location = new System.Drawing.Point(115, 247);
            this.btnBrowT3.Name = "btnBrowT3";
            this.btnBrowT3.Size = new System.Drawing.Size(371, 54);
            this.btnBrowT3.TabIndex = 4;
            this.btnBrowT3.Text = "訂單已交量及入庫量比對";
            this.btnBrowT3.UseVisualStyleBackColor = true;
            this.btnBrowT3.Click += new System.EventHandler(this.btnBrowT3_Click);
            // 
            // btnBrowT4
            // 
            this.btnBrowT4.Font = new System.Drawing.Font("細明體", 12F);
            this.btnBrowT4.Location = new System.Drawing.Point(115, 319);
            this.btnBrowT4.Name = "btnBrowT4";
            this.btnBrowT4.Size = new System.Drawing.Size(371, 54);
            this.btnBrowT4.TabIndex = 5;
            this.btnBrowT4.Text = "採購已交量比對";
            this.btnBrowT4.UseVisualStyleBackColor = true;
            this.btnBrowT4.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("細明體", 12F);
            this.btnExit.Location = new System.Drawing.Point(318, 437);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(371, 54);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "結束作業";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStock
            // 
            this.btnStock.Font = new System.Drawing.Font("細明體", 12F);
            this.btnStock.Location = new System.Drawing.Point(115, 31);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(371, 54);
            this.btnStock.TabIndex = 1;
            this.btnStock.Text = "庫存比對";
            this.btnStock.UseVisualStyleBackColor = true;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // buttonSmallT1
            // 
            this.buttonSmallT1.Font = new System.Drawing.Font("細明體", 12F);
            this.buttonSmallT1.Location = new System.Drawing.Point(115, 175);
            this.buttonSmallT1.Name = "buttonSmallT1";
            this.buttonSmallT1.Size = new System.Drawing.Size(371, 54);
            this.buttonSmallT1.TabIndex = 3;
            this.buttonSmallT1.Text = "借出單據結案檢查";
            this.buttonSmallT1.UseVisualStyleBackColor = true;
            this.buttonSmallT1.Click += new System.EventHandler(this.buttonSmallT1_Click);
            // 
            // btnBarCode
            // 
            this.btnBarCode.Font = new System.Drawing.Font("細明體", 12F);
            this.btnBarCode.Location = new System.Drawing.Point(522, 247);
            this.btnBarCode.Name = "btnBarCode";
            this.btnBarCode.Size = new System.Drawing.Size(371, 54);
            this.btnBarCode.TabIndex = 7;
            this.btnBarCode.Text = "品名規格轉條碼欄位";
            this.btnBarCode.UseVisualStyleBackColor = true;
            this.btnBarCode.Click += new System.EventHandler(this.btnBarCode_Click);
            // 
            // btnTransDay
            // 
            this.btnTransDay.Font = new System.Drawing.Font("細明體", 12F);
            this.btnTransDay.Location = new System.Drawing.Point(522, 319);
            this.btnTransDay.Name = "btnTransDay";
            this.btnTransDay.Size = new System.Drawing.Size(371, 54);
            this.btnTransDay.TabIndex = 7;
            this.btnTransDay.Text = "產品最新交易日檢查";
            this.btnTransDay.UseVisualStyleBackColor = true;
            this.btnTransDay.Click += new System.EventHandler(this.btnTransDay_Click);
            // 
            // PanelT1
            // 
            this.PanelT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelT1.Controls.Add(this.buttonSmallT3);
            this.PanelT1.Controls.Add(this.btnBatchStock);
            this.PanelT1.Controls.Add(this.buttonSmallT2);
            this.PanelT1.Controls.Add(this.btnStock);
            this.PanelT1.Controls.Add(this.btnTransDay);
            this.PanelT1.Controls.Add(this.btnBrowT4);
            this.PanelT1.Controls.Add(this.btnExit);
            this.PanelT1.Controls.Add(this.btnBarCode);
            this.PanelT1.Controls.Add(this.btnBrowT3);
            this.PanelT1.Controls.Add(this.buttonSmallT1);
            this.PanelT1.Controls.Add(this.btnMny);
            this.PanelT1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelT1.Location = new System.Drawing.Point(0, 100);
            this.PanelT1.Name = "PanelT1";
            this.PanelT1.Size = new System.Drawing.Size(1010, 523);
            this.PanelT1.TabIndex = 8;
            // 
            // buttonSmallT3
            // 
            this.buttonSmallT3.Font = new System.Drawing.Font("細明體", 12F);
            this.buttonSmallT3.Location = new System.Drawing.Point(522, 103);
            this.buttonSmallT3.Name = "buttonSmallT3";
            this.buttonSmallT3.Size = new System.Drawing.Size(371, 54);
            this.buttonSmallT3.TabIndex = 11;
            this.buttonSmallT3.Text = "釋放所有單據的修改狀態";
            this.buttonSmallT3.UseVisualStyleBackColor = true;
            this.buttonSmallT3.Click += new System.EventHandler(this.buttonSmallT3_Click);
            // 
            // btnBatchStock
            // 
            this.btnBatchStock.Font = new System.Drawing.Font("細明體", 12F);
            this.btnBatchStock.Location = new System.Drawing.Point(522, 31);
            this.btnBatchStock.Name = "btnBatchStock";
            this.btnBatchStock.Size = new System.Drawing.Size(371, 54);
            this.btnBatchStock.TabIndex = 9;
            this.btnBatchStock.Text = "批次勾稽庫存比對";
            this.btnBatchStock.UseVisualStyleBackColor = true;
            this.btnBatchStock.Click += new System.EventHandler(this.btnBatchStock_Click);
            // 
            // buttonSmallT2
            // 
            this.buttonSmallT2.Font = new System.Drawing.Font("細明體", 12F);
            this.buttonSmallT2.Location = new System.Drawing.Point(522, 175);
            this.buttonSmallT2.Name = "buttonSmallT2";
            this.buttonSmallT2.Size = new System.Drawing.Size(371, 54);
            this.buttonSmallT2.TabIndex = 8;
            this.buttonSmallT2.Text = "網路訂單狀態檢查";
            this.buttonSmallT2.UseVisualStyleBackColor = true;
            this.buttonSmallT2.Click += new System.EventHandler(this.buttonSmallT2_Click);
            // 
            // FrmCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.PanelT1);
            this.Controls.Add(this.pnlBoxT1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmCompare";
            this.Tag = "資料比對作業";
            this.Text = "資料比對作業";
            this.Load += new System.EventHandler(this.FrmCompare_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlBoxT1.ResumeLayout(false);
            this.PanelT1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JE.MyControl.StatusStripT statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private JE.MyControl.PanelT pnlBoxT1;
        private System.Windows.Forms.Label label1;
        private JE.MyControl.ButtonSmallT btnStock;
        private JE.MyControl.ButtonSmallT btnMny;
        private JE.MyControl.ButtonSmallT btnBrowT3;
        private JE.MyControl.ButtonSmallT btnBrowT4;
        private JE.MyControl.ButtonSmallT btnExit;
        private JE.MyControl.ButtonSmallT buttonSmallT1;
        private JE.MyControl.ButtonSmallT btnBarCode;
        private JE.MyControl.ButtonSmallT btnTransDay;
        private JE.MyControl.PanelNT PanelT1;
        private JE.MyControl.ButtonSmallT buttonSmallT2;
        private JE.MyControl.ButtonSmallT btnBatchStock;
        private JE.MyControl.ButtonSmallT buttonSmallT3;
    }
}