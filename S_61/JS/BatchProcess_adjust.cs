using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    class BatchProcess_adjust : xDocuments, IxValidate
    {
        protected override string MasterName
        {
            get { return "BatchProcess_adjust"; }
        }

        protected override string KeyName
        {
            get { return "adno"; }
        }

        public string ValiTable
        {
            get { return "BatchProcess_adjust"; }
        }

        public string ValiKey
        {
            get { return "adno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new S_61.subMenuFm_2.批號調整作業();
        }
    }
}
