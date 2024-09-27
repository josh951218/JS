using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class XX06 : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "XX06"; }
        }

        public string ValiKey
        {
            get { return "X6No"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<XX06>();
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
            get { return "X6No"; }
        }

        public string xNameID
        {
            get { return "X6Name"; }
        }

        public string gridNoText
        {
            get { return "職謂編號"; }
        }

        public string gridNameText
        {
            get { return "職謂名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<XX06>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "職謂建檔作業"; }
        }
    }
}
