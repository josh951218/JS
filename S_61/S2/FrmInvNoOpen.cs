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
using System.Data.SqlClient;

namespace S_61.S2
{
    public partial class FrmInvNoOpen : Formbase, JBS.JS.IxOpen
    {
        public string TResult { get; private set; }

        public string TSeekNo { set; private get; }

        DataTable dttemp;
        int owner = 1;

        public FrmInvNoOpen(int i)
        {
            InitializeComponent();
            this.dttemp = new DataTable();
            this.owner = i;
             
            var no = (this.owner == 1) ? "客戶編號" : "廠商編號";
            this.客戶編號.HeaderText = no;

            var name = (this.owner == 1) ? "客戶簡稱" : "廠商簡稱";
            this.客戶簡稱.HeaderText = name;

            var pno = (this.owner == 1) ? "cuno" : "fano";
            this.客戶編號.DataPropertyName = pno;

            var pname = (this.owner == 1) ? "cuname1" : "faname1";
            this.客戶簡稱.DataPropertyName = pname;

            var date = (Common.User_DateTime == 1) ? "" : "1";
            this.發票日期.DataPropertyName = "indate" + date;
        }

        private void FrmInvNoOpen_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            using (SqlCommand cmd = cn.CreateCommand())
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                switch (this.owner)
                {
                    case 1:
                        cmd.CommandText = "Select inno,indate,indate1,cono,coname1,cuno,cuname1,inmemo from saleinv  where invalid = 0 order by indate desc,cuno asc";
                        break;
                    default:
                        cmd.CommandText = "Select inno,indate,indate1,cono,coname1,fano,faname1,inmemo from bshopinv where invalid = 0 order by indate desc,fano asc";
                        break;
                }
                da.Fill(dttemp);
            }

            dataGridViewT1.DataSource = dttemp;

            if (dttemp.Rows.Count > 0)
            {
                var li = dttemp.AsEnumerable().ToList();
                int index = li.FindLastIndex(r => string.CompareOrdinal(this.TSeekNo ?? "", r["inno"].ToString()) > 0) + 1;
                if (index >= li.Count)
                    index = li.Count - 1;

                dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
                dataGridViewT1.Rows[index].Selected = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dttemp.Clear();
            this.Dispose();
        }

        private void dataGridViewT1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGet_Click(null, null);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            if (dataGridViewT1.SelectedRows == null)
                return;

            if (dataGridViewT1.SelectedRows[0].Index != index)
                return;

            this.TResult = dttemp.Rows[index]["inno"].ToString().Trim();

            this.DialogResult = DialogResult.OK;
        }
    }
}
