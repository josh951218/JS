using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Draw : xDocuments
    {

        protected override string MasterName
        {
            get { return "Draw"; }
        }

        protected override string KeyName
        {
            get { return "drno"; }
        }
    }
}
