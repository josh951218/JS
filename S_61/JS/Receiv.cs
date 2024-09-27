using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    class Receiv : xDocuments
    {
        protected override string MasterName
        {
            get { return "Receiv"; }
        }

        protected override string KeyName
        {
            get { return "reno"; }
        }
    }
}
