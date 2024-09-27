using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Send : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "Send"; }
        }

        public string ValiKey
        {
            get { return "seno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<Send>();
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
            get { return "SeNo"; }
        }

        public string xNameID
        {
            get { return "SeName"; }
        }

        public string gridNoText
        {
            get { return "送貨編號"; }
        }

        public string gridNameText
        {
            get { return "送貨名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<Send>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "送貨方式建檔"; }
        }
    }
}
