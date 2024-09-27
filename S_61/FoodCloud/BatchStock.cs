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
    public partial class BatchStock : Formbase
    {
        public string _Bno { get; set; }
        public string _BatchNo { get; set; }
        public string _FaName { get; set; }
        public string _ItName { get; set; }
        DataTable dt = new DataTable();

        public BatchStock()
        {
            InitializeComponent();
        }

        private void BatchStock_Load(object sender, EventArgs e)
        {
            ItName.Text = _ItName;
            FaName.Text = _FaName;
            BatchNo.Text = _BatchNo;
            數量.Set庫存數量小數();
            string SrtSql =
@"
    select *,stname = (select top 1 stname from stkroom where stno = BatchStock.stno ) from BatchStock where bno = @bno
";

            SQL.ExecuteNonQuery(SrtSql, new parameters("bno", _Bno), dt);
            dataGridViewT1.DataSource = dt;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
