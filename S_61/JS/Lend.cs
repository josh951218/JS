using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Lend : xDocuments, IxValidate, IxBom
    {
        protected override string MasterName
        {
            get { return "Lend"; }
        }

        protected override string KeyName
        {
            get { return "leno"; }
        }

        public string ValiTable
        {
            get { return "Lend"; }
        }

        public string ValiKey
        {
            get { return "leno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new S_61.S2.FrmLend_Print_LeNoNew();
        }

        public string TBom
        {
            get { return "lendbom"; }
        }
    }
}
