using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class XX05 : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "XX05"; }
        }

        public string ValiKey
        {
            get { return "X5No"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new JBS.JS.FrmXxBrow<JBS.JS.XX05>();
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
            get { return "X5No"; }
        }

        public string xNameID
        {
            get { return "X5Name"; }
        }

        public string gridNoText
        {
            get { return "發票類別編號"; }
        }

        public string gridNameText
        {
            get { return "發票類別名稱"; }
        }

        public string ShowDialog()
        {
            return "";
        }


        public string xTitle
        {
            get { return "發票類別建檔"; }
        }
    }
}
