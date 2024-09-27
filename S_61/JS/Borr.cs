using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    class Borr : xDocuments, IxBom
    {
        protected override string MasterName
        {
            get { return "Borr"; }
        }

        protected override string KeyName
        {
            get { return "bono"; }
        }

        public string TBom
        {
            get { return "borrbom"; }
        }
    }
}
