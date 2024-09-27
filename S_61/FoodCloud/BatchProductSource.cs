using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61
{
    public partial class BatchProductSource : Formbase
    {
        public string _Bno { get; set; }
        public string _BatchNo { get; set; }
        public string _FaName { get; set; }
        public string _ItName { get; set; }
        public DataTable dt = new DataTable();

        public BatchProductSource()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void BatchProductSource_Load(object sender, EventArgs e)
        {
            數量.Set庫存數量小數();
            if (Common.User_DateTime == 1)
                西元年.Visible = false;
            else
                民國年.Visible = false;
            ItName.Text = _ItName;
            FaName.Text = _FaName;
            BatchNo.Text = _BatchNo;
            string Get入庫bomId_Sql =
@"
 SELECT 入庫bomid = a.bomid ,單據 = '+入庫明細'
  FROM 
 (select * from BatchProcess_Garnerd where bno = @bno) as a 															    
 inner join 																				    
 (select * from Garnerd where len(stnoi) > 0 and ittrait != '1' ) as b 											    
 on 																						    
 a.bomid = b.bomid	
";

            string bomid = SQL.ExecuteScalar(Get入庫bomId_Sql, new parameters("bno", _Bno));
            string Get入庫扣料子街_Sql =
@"
 SELECT 單據 = '-入庫子階',異動數量 = a.Qty,單據編號 = b.gano,異動倉庫 = b.stnoo,序=b.recordno, bom序=CONVERT(varchar(4),a.Rec),民國年 = b.gadate,西元年 = b.gadate1,a.Bno,a.Bomid,a.Rec,批次號碼 = c.BatchNo,廠商編號 =c.Fano,廠商簡稱=(select top 1 faname1 from fact where fano = c.fano),產品編號 = c.itno,品名規格 = (select itname from item where itno = c.itno)
 from
 (select * FROM BatchProcess_GarnerBom where bomid = @bomid ) as a															    
 inner join 																				    
 (select * from Garnerd where bomid = @bomid and  len(stnoo) > 0 and ittrait = '2') as b 											    
 on 																						    
 a.bomid = b.bomid	
 inner join 
 (select * from BatchInformation ) as c
 on a.bno = c.bno
";

            SQL.ExecuteNonQuery(Get入庫扣料子街_Sql, new parameters("bomid", bomid), dt);
            dataGridViewT1.DataSource = dt;
 
        }
    }
}
