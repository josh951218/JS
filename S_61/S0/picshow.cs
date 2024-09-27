using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace S_61.S0
{
    public partial class picshow : JE.MyControl.Formbase
    {
        public picshow()
        {
            InitializeComponent();
        }

        private void picshow_Load(object sender, EventArgs e)
        {
            pictureBoxT1.Location = new Point(0, 0);//廣告圖的位置
            pictureBoxT1.Height = Screen.AllScreens[0].Bounds.Height;//廣告圖的高
            pictureBoxT1.Width = Screen.AllScreens[0].Bounds.Width;//廣告圖的寬
            pictureBoxT1.BorderStyle = BorderStyle.None;
        }

    }
}
