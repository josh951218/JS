using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class RLend : xDocuments, IxValidate
    {
        protected override string MasterName
        {
            get { return "RLend"; }
        }

        protected override string KeyName
        {
            get { return "leno"; }
        }

        public string ValiTable
        {
            get { return "RLend"; }
        }

        public string ValiKey
        {
            get { return "leno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new S_61.S2.FrmRLend_Print_LeNo();
        }
    }
}
