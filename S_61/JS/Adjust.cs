using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Adjust : xDocuments, IxValidate
    {
        protected override string MasterName
        {
            get { return "Adjust"; }
        }

        protected override string KeyName
        {
            get { return "adno"; }
        }
         
        public string ValiTable
        {
            get { return "Adjust"; }
        }

        public string ValiKey
        {
            get { return "adno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new S_61.subMenuFm_2.FrmAdjust_Print_AdNo();
        }
    }
}
