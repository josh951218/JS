using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Quote : xDocuments, IxBom
    {
        protected override string MasterName
        {
            get { return "Quote"; }
        }

        protected override string KeyName
        {
            get { return "quno"; }
        }

        public string TBom
        {
            get { return "QuoteBom"; }
        }
    }
}
