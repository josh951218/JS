using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JBS.JS
{
    public interface IxBrow
    {
        int xNoLength { get; }
        int xNameLength { get; }
        string xNoID { get; }
        string xNameID { get; }
        string gridNoText { get; }
        string gridNameText { get; }

        string xTitle { get; }

        string ShowDialog();
    }
}
