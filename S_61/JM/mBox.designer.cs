namespace JBS.JM
{
    partial class Msg
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
            this.Button1 = new System.Windows.Forms.Button();
            this.labelT1 = new System.Windows.Forms.Label();
            this.tableLayoutPanelbase1 = new JE.MyControl.TableLayoutPanelbase();
            this.tableLayoutPanelbase1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button1.AutoSize = true;
            this.Button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button1.Font = new System.Drawing.Font("細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Button1.Location = new System.Drawing.Point(277, 215);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(280, 110);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "確認";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelT1
            // 
            this.labelT1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelT1.AutoSize = true;
            this.labelT1.BackColor = System.Drawing.Color.Transparent;
            this.labelT1.Font = new System.Drawing.Font("細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelT1.ForeColor = System.Drawing.Color.Red;
            this.labelT1.Location = new System.Drawing.Point(323, 92);
            this.labelT1.Name = "labelT1";
            this.labelT1.Size = new System.Drawing.Size(188, 48);
            this.labelT1.TabIndex = 0;
            this.labelT1.Text = "labelT1";
            // 
            // tableLayoutPanelbase1
            // 
            this.tableLayoutPanelbase1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanelbase1.ColumnCount = 1;
            this.tableLayoutPanelbase1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelbase1.Controls.Add(this.Button1, 0, 2);
            this.tableLayoutPanelbase1.Controls.Add(this.labelT1, 0, 1);
            this.tableLayoutPanelbase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelbase1.Location = new System.Drawing.Point(5, 15);
            this.tableLayoutPanelbase1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelbase1.Name = "tableLayoutPanelbase1";
            this.tableLayoutPanelbase1.RowCount = 4;
            this.tableLayoutPanelbase1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelbase1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelbase1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelbase1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelbase1.Size = new System.Drawing.Size(834, 348);
            this.tableLayoutPanelbase1.TabIndex = 2;
            // 
            // Msg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(844, 368);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanelbase1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Msg";
            this.Padding = new System.Windows.Forms.Padding(5, 15, 5, 5);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.mBox_Load);
            this.tableLayoutPanelbase1.ResumeLayout(false);
            this.tableLayoutPanelbase1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Label labelT1;
        private JE.MyControl.TableLayoutPanelbase tableLayoutPanelbase1;
    }
}