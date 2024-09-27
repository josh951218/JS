using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Xa01 : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "Xa01"; }
        }

        public string ValiKey
        {
            get { return "Xa1No"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new JBS.JS.FrmXxBrow<JBS.JS.Xa01>(
                5,
                " * ",
                new string[] { "使用國家", "國家簡稱", "單位" },
                new string[] { "Xa1Coun2", "Xa1Coun1", "Xa1Cent" });
        }

        public int xNoLength
        {
            get { return 3; }
        }

        public int xNameLength
        {
            get { return 12; }
        }

        public string xNoID
        {
            get { return "Xa1No"; }
        }

        public string xNameID
        {
            get { return "Xa1Name"; }
        }

        public string gridNoText
        {
            get { return "幣別編號"; }
        }

        public string gridNameText
        {
            get { return "幣別名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new S_61.SOther.FrmXa01())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "貨幣建檔作業"; }
        }
    }
}
