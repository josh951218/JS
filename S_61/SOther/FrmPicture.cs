using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;
using System.Data.SqlClient;

namespace S_61.SOther
{
    public partial class FrmPicture : Formbase
    {
        public string ItNo = "";
        public object image;

        public FrmPicture()
        {
            InitializeComponent();
             
        }

        private void FrmPicture_Load(object sender, EventArgs e)
        {
            if (ItNo.Length > 0)
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("ItNo", ItNo);
                    cmd.CommandText = "Select pic from item where itno = (@ItNo)";
                    image = cmd.ExecuteScalar();
                    if (image != null) pic.LoadImage((byte[])image);
                }
            }
            else
            {
                if (image != DBNull.Value) pic.LoadImage((byte[])image);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void checkBoxT1_CheckedChanged(object sender, EventArgs e)
        {
            pic.SizeMode = checkBoxT1.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.CenterImage;
        }
    }
}
