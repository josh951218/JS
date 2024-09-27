using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Dept : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "Dept"; }
        }

        public string ValiKey
        {
            get { return "deno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<Dept>();
        }

        public int xNoLength
        {
            get { return 10; }
        }

        public int xNameLength
        {
            get { return 20; }
        }

        public string xNoID
        {
            get { return "DeNo"; }
        }

        public string xNameID
        {
            get { return "DeName1"; }
        }

        public string gridNoText
        {
            get { return "部門編號"; }
        }

        public string gridNameText
        {
            get { return "部門名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<Dept>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "部門建檔作業"; }
        }
    }
}
