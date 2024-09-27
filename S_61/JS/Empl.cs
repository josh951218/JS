using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Empl : IxValidate
    {
        public string ValiTable
        {
            get { return "Empl"; }
        }

        public string ValiKey
        {
            get { return "emno"; }
        } 

        public System.Windows.Forms.Form TOpen() 
        {
            return new S_61.SOther.FrmEmplb();
        }
    }
}
