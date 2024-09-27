using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Kind : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "Kind"; }
        }

        public string ValiKey
        {
            get { return "kino"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new JBS.JS.FrmXxBrow<JBS.JS.Kind>(
                3,
                @" kino,kiname,稅別 = 
                   case 
	                   when kitax=1 then '應稅'
	                   when kitax=2 then '免稅'
                   end ",
                new string[] { "稅別" },
                new string[] { "稅別" });
        }

        public int xNoLength
        {
            get { return 4; }
        }

        public int xNameLength
        {
            get { return 20; }
        }

        public string xNoID
        {
            get { return "kino"; }
        }

        public string xNameID
        {
            get { return "kiname"; }
        }

        public string gridNoText
        {
            get { return "產品類別編號"; }
        }

        public string gridNameText
        {
            get { return "產品類別名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new S_61.SOther.FrmKind())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "產品類別建檔"; }
        }
    }
}
