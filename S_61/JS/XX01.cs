using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class XX01 : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "XX01"; }
        }

        public string ValiKey
        {
            get { return "X1No"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<XX01>();
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
            get { return "X1No"; }
        }

        public string xNameID
        {
            get { return "X1Name"; }
        }

        public string gridNoText
        {
            get { return "客戶類別編號"; }
        }

        public string gridNameText
        {
            get { return "客戶類別名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<XX01>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }

        public string xTitle
        {
            get { return "客戶類別建檔"; }
        }
    }
}
