using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class XX04 : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "XX04"; }
        }

        public string ValiKey
        {
            get { return "X4No"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<XX04>();
        }

        public int xNoLength
        {
            get { return 1; }
        }

        public int xNameLength
        {
            get { return 10; }
        }

        public string xNoID
        {
            get { return "X4No"; }
        }

        public string xNameID
        {
            get { return "X4Name"; }
        }

        public string gridNoText
        {
            get { return "結帳類別編號"; }
        }

        public string gridNameText
        {
            get { return "結帳類別名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<XX04>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "結帳類別建檔"; }
        }
    }
}
