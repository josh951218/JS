using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JBS.JS
{
    public interface IxValidate
    {
        string ValiTable { get; }
        string ValiKey { get; } 
        System.Windows.Forms.Form TOpen();
         
    }
}
