using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class XX03 : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "XX03"; }
        }

        public string ValiKey
        {
            get { return "X3No"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new JBS.JS.FrmXxBrow<JBS.JS.XX03>(
                3,
                " x3no,x3name,稅率 = x3rate ",
                new string[] { "稅率" },
                new string[] { "稅率" });
        }

        public int xNoLength
        {
            get { return 1; }
        }

        public int xNameLength
        {
            get { return 8; }
        }

        public string xNoID
        {
            get { return "x3no"; }
        }

        public string xNameID
        {
            get { return "x3name"; }
        }

        public string gridNoText
        {
            get { return "稅別編號"; }
        }

        public string gridNameText
        {
            get { return "稅別名稱"; }
        }

        public string ShowDialog()
        {
            throw new NotImplementedException();
        }


        public string xTitle
        {
            get { return "稅別建檔作業"; }
        }
    }
}
