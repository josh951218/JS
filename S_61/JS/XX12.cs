using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class XX12 : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "XX12"; }
        }

        public string ValiKey
        {
            get { return "X12No"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<XX12>();
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
            get { return "X12No"; }
        }

        public string xNameID
        {
            get { return "X12Name"; }
        }

        public string gridNoText
        {
            get { return "廠商類別編號"; }
        }

        public string gridNameText
        {
            get { return "廠商類別名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<XX12>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "廠商類別建檔"; }
        }
    }
}
