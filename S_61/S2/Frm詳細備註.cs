using System;
using System.Windows.Forms;
using JE.MyControl;
  
namespace S_61.S1
{
    public partial class Frm詳細備註 : Formbase
    {
        public string memo1;
        public bool CanEdt = false;

        public Frm詳細備註()
        {
            InitializeComponent();
        }

        private void Frm詳細備註_Load(object sender, EventArgs e)
        {
            if (!CanEdt) RTB.ReadOnly = true;
            if (memo1 != "")
            {
                RTB.Text = memo1;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            string str = "";
            string empty = "";
            int count = RTB.Lines.Length;
            try
            {

                for (int i = 0; i < count; i++)
                {
                    str += RTB.Lines[i].PadRight(80, ' ') + "\n";
                }

                for (int i = 0; i < 18 - count; i++)
                {
                    str += empty.PadRight(80, ' ') + "\n";
                }
                memo1 = str;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
