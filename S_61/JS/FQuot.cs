using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JBS.JS
{
    class FQuot : xDocuments, IxBom
    {
        protected override string MasterName
        {
            get { return "FQuot"; }
        }

        protected override string KeyName
        {
            get { return "fqno"; }
        }
         
        public string TBom
        {
            get { return "FQuotBom"; }
        }
    }
}
