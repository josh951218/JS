using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Bom : xDocuments, IxValidate
    {
        public string ValiTable
        {
            get { return "item"; }
        }

        public string ValiKey
        {
            get { return "itno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new S_61.SOther.FrmItemb_Bom();
        }

        protected override string MasterName
        {
            get { return "Bom"; }
        }

        protected override string KeyName
        {
            get { return "boitno"; }
        } 
    }
}
