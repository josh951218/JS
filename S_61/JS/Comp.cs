using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Comp : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "Comp"; }
        }

        public string ValiKey
        {
            get { return "cono"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new JBS.JS.FrmXxBrow<JBS.JS.Comp>();
        }

        public int xNoLength
        {
            get { return 2; }
        }

        public int xNameLength
        {
            get { return 10; }
        }

        public string xNoID
        {
            get { return "cono"; }
        }

        public string xNameID
        {
            get { return "coname1"; }
        }

        public string gridNoText
        {
            get { return "公司編號"; }
        }

        public string gridNameText
        {
            get { return "公司名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new S_61.S2.FrmComp())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }

        public string xTitle
        {
            get { return "公司建檔作業"; }
        }
    }
}
