using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JE.MyControl;

namespace S_61.subMenuFm_1
{
    public partial class FrmOrder_Memo : Formbase
    {
        public string ormemo1;
        public string orno = "";
        public string quno = "";
        public string ordate = "";
        public string cuno = "";
        public string cuname1 = "";
        public string cuper1 = "";
        public string cutel1 = "";
        public string emno = "";
        public string emname = "";
        public string xa1no = "";
        public string xa1name = "";
        public string xa1par = "";
        public string trno = "";
        public string MeMain = "";
        public string MeOther = "";
        public string MePrint = "";
        public string MeSize = "";
        public string MeSize2 = "";

        List<TextBoxT> tb = new List<TextBoxT>();
        TextBox TB = new TextBox();

        public FrmOrder_Memo()
        {
            InitializeComponent();
        }

        private void FrmOrder_Memo_Load(object sender, EventArgs e)
        {
            OrNo.Text = orno;
            QuNo.Text = quno;
            OrDate.Text = ordate;
            CuNo.Text = cuno;
            CuName1.Text = cuname1;
            CuPer1.Text = cuper1;
            CuTel1.Text = cutel1;
            EmNo.Text = emno;
            EmName.Text = emname;
            Xa1No.Text = xa1no;
            Xa1Name.Text = xa1name;
            Xa1Par.Text = xa1par;
            TrNo.Text = trno;

            TB.Multiline = true;
            WriteToTxt();
        }

        void WriteToTxt()
        {
            try
            {
                RTB1.Text = MeMain;
                RTB2.Text = MeOther;
                RTB3.Text = MePrint;
                RTB4.Text = MeSize;
                RTB5.Text = MeSize2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MeMain = RTB1.Text;
                MeOther =RTB2.Text;
                MePrint =RTB3.Text;
                MeSize = RTB4.Text;
                MeSize2 =RTB5.Text;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

    }
}
