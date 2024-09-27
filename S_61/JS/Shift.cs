using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Shift : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "Shift"; }
        }

        public string ValiKey
        {
            get { return "ShNo"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<Shift>();
        }

        public int xNoLength
        {
            get { return 4; }
        }

        public int xNameLength
        {
            get { return 10; }
        }

        public string xNoID
        {
            get { return "ShNo"; }
        }

        public string xNameID
        {
            get { return "ShName"; }
        }

        public string gridNoText
        {
            get { return "班別編號"; }
        }

        public string gridNameText
        {
            get { return "班別名稱"; }
        }

        public string xTitle
        {
            get { return "班別建檔作業"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<Shift>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }
    }
}
