using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class SaleClass : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "SaleClass"; }
        }

        public string ValiKey
        {
            get { return "scno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<SaleClass>();
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
            get { return "ScNo"; }
        }

        public string xNameID
        {
            get { return "ScName"; }
        }

        public string gridNoText
        {
            get { return "銷售類別編號"; }
        }

        public string gridNameText
        {
            get { return "銷售類別名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<SaleClass>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "銷售類別建檔"; }
        }
    }
}
