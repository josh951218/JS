using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Special : xDocuments
    {
        protected override string MasterName
        {
            get { return "Special"; }
        }

        protected override string KeyName
        {
            get { return "spno"; }
        }
    }
}
