using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S_61.Basic;

namespace JBS.JS
{
    class BatchInformation : xDocuments, IxValidate
    {
        protected override string MasterName
        {
            get { return "BatchInformation"; }
        }

        protected override string KeyName
        {
            get { return "Batchno"; }
        }

        public string ValiTable
        {
            get { return "BatchInformation"; }
        }

        public string ValiKey
        {
            get { return "Batchno"; }
        }

        public Form TOpen()
        {
            return new S_61.subMenuFm_2.批號開窗();
        }
    }
}
