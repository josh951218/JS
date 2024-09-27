using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Trade : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "Trade"; }
        }

        public string ValiKey
        {
            get { return "TrNo"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new FrmXxBrow<Trade>();
        }

        public int xNoLength
        {
            get { return 4; }
        }

        public int xNameLength
        {
            get { return 60; }
        }

        public string xNoID
        {
            get { return "TrNo"; }
        }

        public string xNameID
        {
            get { return "TrName"; }
        }

        public string gridNoText
        {
            get { return "報價類別編號"; }
        }

        public string gridNameText
        {
            get { return "報價類別名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new FrmXxBasic<Trade>())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "報價類別建檔"; }
        }
    }
}
