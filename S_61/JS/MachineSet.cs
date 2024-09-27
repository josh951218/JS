using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    class MachineSet : xDocuments
    {
        protected override string MasterName
        {
            get { return "machineset"; }
        }

        protected override string KeyName
        {
            get { return "maid"; }
        }
    }
}
