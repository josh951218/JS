using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using System.Data.SqlClient;
using S_61.Basic;

namespace S_61.subMenuFm_6
{
    public partial class SubFrmItemColumnSelect : Formbase, JBS.JS.IxOpen
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; } 
        public SubFrmItemColumnSelect()
        {
            InitializeComponent();
        }
        DataTable dt;
        private void SubFrmItemColumnSelect_Load(object sender, EventArgs e)
        {

            dt = new DataTable();
            dt.Columns.Add("欄位名稱",typeof(System.String));
            dt.Columns.Add("欄位說明", typeof(System.String));
            string str = @"itno;產品編號;itnoudf;自訂編號;itname;品名規格;kino;產品類別;itime;注音速查;ittrait;產品組成 1:組合品2:組裝品3:單一商品;itunit;單位;itunitp;包裝單位;itpkgqty;包裝數量;itbuypri;進價;itprice;售價;itprice1;售價1;itprice2;售價2;itprice3;售價3;itprice4;售價4;itprice5;售價5;itcost;標準成本;itbuyprip;包裝進價;itpricep;包裝售價;itpricep1;包裝售價1;itpricep2;包裝售價2;itpricep3;包裝售價3;itpricep4;包裝售價4;itpricep5;包裝售價5;itcostp;包裝標準成本;itsafeqty;期初單位成本;itlastqty;安全存量;itnw;淨重;itnwunit;重量單位;itdesp1;規格說明1;itdesp2;規格說明2;itdesp3;規格說明3;itdesp4;規格說明4;itdesp5;規格說明5;itdesp6;規格說明6;itdesp7;規格說明7;itdesp8;規格說明8;itdesp9;規格說明9;itdesp10;規格說明10;itdate;建檔日期;itdate1;建檔日期(西元);itbuydate;最近進貨日;itbuydate1;最近進貨日(西元);itsaldate;最近銷貨;itsaldate1;最近銷貨日(西元);itfircost;期初單位成本;itfirtqty;期初總數量;itfirtcost;期初總成本;itstockqty;總庫存量;itnote;備註;itudf1;自訂1;itudf2;自訂2;itudf3;自訂3;itudf4;自訂4;itudf5;自訂5;fano;供應商編號;Punit;計價單位;ScNo;銷售類別編號;ItBarName1;條碼品名1;ItBarName2;條碼品名2";
            string[] 雙數索引為欄位名稱_單數索引為欄位說明 = str.Split(';');
            for (int i = 0; i < 雙數索引為欄位名稱_單數索引為欄位說明.Length ; i+=2)
            {
                DataRow dr = dt.NewRow();
                dr["欄位名稱"] = 雙數索引為欄位名稱_單數索引為欄位說明[i];
                dr["欄位說明"] = 雙數索引為欄位名稱_單數索引為欄位說明[i + 1];
                dt.Rows.Add(dr);
            }
            dataGridViewT1.DataSource = dt;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;
            var 欄位名稱 = dt.Rows[index]["欄位名稱"].ToString();
            this.TResult = 欄位名稱.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
                btnGet_Click(null, null);

            return base.ProcessCmdKey(ref msg, keyData);
        }

//itno;產品編號
//;itnoudf;自訂編號
//;itname;品名規格
//;kino;產品類別
//;itime;注音速查
//;ittrait;產品組成 1:組合品2:組裝品3:單一商品
//;itunit;品單位
//;itunitp;包裝單位
//;itpkgqty;包裝數量
//;itbuypri;進價
//;itprice;售價
//;itprice1;售價1
//;itprice2;售價2
//;itprice3;售價3
//;itprice4;售價4
//;itprice5;售價5
//;itcost;標準成本
//;itbuyprip;包裝進價
//;itpricep;包裝售價
//;itpricep1;包裝售價1
//;itpricep2;包裝售價2
//;itpricep3;包裝售價3
//;itpricep4;包裝售價4
//;itpricep5;包裝售價5
//;itcostp;包裝標準成本
//;itsafeqty;期初單位成本
//;itlastqty;安全存量
//;itnw;淨重
//;itnwunit;重量單位
//;itdesp1;規格說明1
//;itdesp2;規格說明2
//;itdesp3;規格說明3
//;itdesp4;規格說明4
//;itdesp5;規格說明5
//;itdesp6;規格說明6
//;itdesp7;規格說明7
//;itdesp8;規格說明8
//;itdesp9;規格說明9
//;itdesp10;規格說明10
//;itdate;建檔日期
//;itdate1;建檔日期(西元)
//;itbuydate;最近進貨日
//;itbuydate1;最近進貨日(西元)
//;itsaldate;最近銷貨
//;itsaldate1;最近銷貨日(西元)
//;itfircost;期初單位成本
//;itfirtqty;期初總數量
//;itfirtcost;期初總成本
//;itstockqty;總庫存量
//;itnote;備註
//;itudf1;自訂1
//;itudf2;自訂2
//;itudf3;自訂3
//;itudf4;自訂4
//;itudf5;自訂5
//;fano;供應商編號
//;Punit;計價單位
//;ScNo;銷售類別編號
//;ItBarName1;條碼品名1
//;ItBarName2;條碼品名2

    }
}
