using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Order : xDocuments, IxBom
    {
        protected override string MasterName
        {
            get { return "[Order]"; }
        }

        protected override string KeyName
        {
            get { return "orno"; }
        }

        public string TBom
        {
            get { return "OrderBom"; }
        }
    }
}
