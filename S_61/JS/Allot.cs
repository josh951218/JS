using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Allot : xDocuments
    {
        protected override string MasterName
        {
            get { return "Allot"; }
        }

        protected override string KeyName
        {
            get { return "alno"; }
        }
    }
}
