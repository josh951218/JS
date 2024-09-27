using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Ford : xDocuments, IxBom
    { 
        protected override string MasterName
        {
            get { return "Ford"; }
        }

        protected override string KeyName
        {
            get { return "fono"; }
        }

        public string TBom
        {
            get { return "FOrdBom"; }
        }
    }
}
