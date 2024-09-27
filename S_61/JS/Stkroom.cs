using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBS.JS
{
    public class Stkroom : IxValidate, IxBrow
    {
        public string ValiTable
        {
            get { return "Stkroom"; }
        }

        public string ValiKey
        {
            get { return "stno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new JBS.JS.FrmXxBrow<JBS.JS.Stkroom>(
                3,
                @" stno,stname,倉庫類別 = 
                   case 
	                   when sttrait=1 then '庫存倉'
	                   when sttrait=2 then '借出倉'
	                   when sttrait=3 then '加工倉'
	                   when sttrait=4 then '借入倉'
                   end ",
                new string[] { "倉庫類別" },
                new string[] { "倉庫類別" });
        }

        public int xNoLength
        {
            get { return 10; }
        }

        public int xNameLength
        {
            get { return 10; }
        }

        public string xNoID
        {
            get { return "stno"; }
        }

        public string xNameID
        {
            get { return "stname"; }
        }

        public string gridNoText
        {
            get { return "倉庫編號"; }
        }

        public string gridNameText
        {
            get { return "倉庫名稱"; }
        }

        public string ShowDialog()
        {
            using (var frm = new S_61.SOther.FrmStkRoom())
            {
                var no = "";
                frm.ShowDialog(out no);

                return no;
            }
        }


        public string xTitle
        {
            get { return "倉庫建檔作業"; }
        }
    }
}
