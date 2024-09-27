using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class XX02 : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "XX02"; }
        }

        public string ValiKey
        {
            get { return "X2No"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<XX02>();
        }

        public int xNoLength
        {
            get { return 8; }
        }

        public int xNameLength
        {
            get { return 15; }
        }

        public string xNoID
        {
            get { return "X2No"; }
        }

        public string xNameID
        {
            get { return "X2Name"; }
        }

        public string gridNoText
        {
            get { return "區域編號"; }
        }

        public string gridNameText
        {
            get { return "區域名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<XX02>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "區域建檔作業"; }
        }
    }
}
