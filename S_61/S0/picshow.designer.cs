namespace S_61.S0
{
    partial class picshow
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
            this.pictureBoxT1 = new JE.MyControl.PictureBoxT();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxT1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxT1
            // 
            this.pictureBoxT1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxT1.IsImageChange = false;
            this.pictureBoxT1.Location = new System.Drawing.Point(5, 6);
            this.pictureBoxT1.Name = "pictureBoxT1";
            this.pictureBoxT1.Size = new System.Drawing.Size(993, 630);
            this.pictureBoxT1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxT1.TabIndex = 0;
            this.pictureBoxT1.TabStop = false;
            // 
            // picshow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 648);
            this.Controls.Add(this.pictureBoxT1);
            this.Name = "picshow";
            this.Text = "picshow";
            this.Load += new System.EventHandler(this.picshow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxT1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public JE.MyControl.PictureBoxT pictureBoxT1;
    }
}