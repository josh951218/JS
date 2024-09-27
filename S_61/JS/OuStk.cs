using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class OuStk : xDocuments
    {
        protected override string MasterName
        {
            get { return "OuStk"; }
        }

        protected override string KeyName
        {
            get { return "OuNo"; }
        }
    }
}
