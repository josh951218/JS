using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Spec : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "Spec"; }
        }

        public string ValiKey
        {
            get { return "spno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<Spec>();
        }

        public int xNoLength
        {
            get { return 4; }
        }

        public int xNameLength
        {
            get { return 20; }
        }

        public string xNoID
        {
            get { return "spno"; }
        }

        public string xNameID
        {
            get { return "spname"; }
        }

        public string gridNoText
        {
            get { return "專案編號"; }
        }

        public string gridNameText
        {
            get { return "專案名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<Spec>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "專案建檔作業"; }
        }
    }
}
