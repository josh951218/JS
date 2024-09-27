using System;
using System.Data;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace S_61.S5
{
    public partial class FrmItemCostbNew : Formbase
    {
        delegate void mydelegate();
        public DataTable iCost = new DataTable();
        public DataTable sCost = new DataTable();

        DataTable dt = new DataTable();

        public FrmItemCostbNew()
        {
            InitializeComponent();
            dataGridViewT1.AutoGenerateColumns = true;
            dataGridViewT2.AutoGenerateColumns = true;
        }

        private void FrmItemCostb_Load(object sender, EventArgs e)
        {
            //dataGridViewT1.SuspendLayout();
            //this.SuspendLayout();
            //dataGridViewT1.columns
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Application.DoEvents();

            Action tw = () =>
            {
                dataGridViewT1.DataSource = iCost;
                dataGridViewT2.DataSource = sCost;
                dataGridViewT2.Columns["itno"].Visible = false;
            };
            this.Invoke(tw);

            System.Threading.Tasks.Task.Factory.StartNew(a);
            System.Threading.Tasks.Task.Factory.StartNew(b);
            System.Threading.Tasks.Task.Factory.StartNew(c);
        }

        void a()
        {
            var j = 1;
            var w = (int)((20 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            var w1 = (int)((30 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            var w2 = (int)((10 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            var name1 = Common.User_DateTime == 1 ? Common.Sys_StkYear1.ToString() : Common.Sys_StkYear2.ToString();

            for (int i = 2; i <= 12; i += 5)
            {
                //var name = Common.User_DateTime == 1 ? Common.Sys_StkYear1.ToString() : Common.Sys_StkYear2.ToString();
                var name = name1 + (j).ToString().PadLeft(2, '0');
                j++;

                dataGridViewT1.Columns[i].HeaderText = name + "期初存量";
                dataGridViewT1.Columns[i + 1].HeaderText = name + "平均成本(初)";
                dataGridViewT1.Columns[i + 2].HeaderText = name + "累進數量";
                dataGridViewT1.Columns[i + 3].HeaderText = name + "累進成本";
                dataGridViewT1.Columns[i + 4].HeaderText = name + "平均成本";


                dataGridViewT2.Columns[i + 1].HeaderText = name + "期初存量";
                dataGridViewT2.Columns[i + 2].HeaderText = name + "平均成本(初)";
                dataGridViewT2.Columns[i + 3].HeaderText = name + "累進數量";
                dataGridViewT2.Columns[i + 4].HeaderText = name + "累進成本";
                dataGridViewT2.Columns[i + 5].HeaderText = name + "平均成本";

                Action a1 = () =>
                {
                    dataGridViewT1.Columns[i].Width = w;
                    dataGridViewT1.Columns[i + 1].Width = w;
                    dataGridViewT1.Columns[i + 2].Width = w;
                    dataGridViewT1.Columns[i + 3].Width = w;
                    dataGridViewT1.Columns[i + 4].Width = w;

                    dataGridViewT2.Columns[i + 1].Width = w;
                    dataGridViewT2.Columns[i + 2].Width = w;
                    dataGridViewT2.Columns[i + 3].Width = w;
                    dataGridViewT2.Columns[i + 4].Width = w;
                    dataGridViewT2.Columns[i + 5].Width = w;
                };
                this.Invoke(a1);
            }

            Action tw = () =>
            {
                dataGridViewT1.Columns["itno"].Width = w;
                dataGridViewT1.Columns["itname"].Width = w1;
                dataGridViewT2.Columns["stno"].Width = w2;
                dataGridViewT2.Columns["stname"].Width = w2;
            };
            this.Invoke(tw);

            dataGridViewT1.Columns["itno"].HeaderText = "產品編號";
            dataGridViewT1.Columns["itname"].HeaderText = "品名規格";
            dataGridViewT2.Columns["stno"].HeaderText = "倉庫編號";
            dataGridViewT2.Columns["stname"].HeaderText = "倉庫名稱";

            dataGridViewT1.DefaultCellStyle.Format = "F2";
            dataGridViewT1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewT2.DefaultCellStyle.Format = "F2";
            dataGridViewT2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewT1.Columns["itno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewT1.Columns["itname"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewT2.Columns["stno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewT2.Columns["stname"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        void b()
        {
            var j = 3;
            var w = (int)((20 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            var w1 = (int)((30 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            var w2 = (int)((10 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            var name1 = Common.User_DateTime == 1 ? Common.Sys_StkYear1.ToString() : Common.Sys_StkYear2.ToString();

            for (int i = 12; i <= 57; i += 5)
            {
                //var name = Common.User_DateTime == 1 ? Common.Sys_StkYear1.ToString() : Common.Sys_StkYear2.ToString();
                var name = name1 + (j).ToString().PadLeft(2, '0');
                j++;

                dataGridViewT1.Columns[i].HeaderText = name + "期初存量";
                dataGridViewT1.Columns[i + 1].HeaderText = name + "平均成本(初)";
                dataGridViewT1.Columns[i + 2].HeaderText = name + "累進數量";
                dataGridViewT1.Columns[i + 3].HeaderText = name + "累進成本";
                dataGridViewT1.Columns[i + 4].HeaderText = name + "平均成本";


                dataGridViewT2.Columns[i + 1].HeaderText = name + "期初存量";
                dataGridViewT2.Columns[i + 2].HeaderText = name + "平均成本(初)";
                dataGridViewT2.Columns[i + 3].HeaderText = name + "累進數量";
                dataGridViewT2.Columns[i + 4].HeaderText = name + "累進成本";
                dataGridViewT2.Columns[i + 5].HeaderText = name + "平均成本";

                Action b1 = () =>
                {
                    dataGridViewT1.Columns[i].Width = w;
                    dataGridViewT1.Columns[i + 1].Width = w;
                    dataGridViewT1.Columns[i + 2].Width = w;
                    dataGridViewT1.Columns[i + 3].Width = w;
                    dataGridViewT1.Columns[i + 4].Width = w;

                    dataGridViewT2.Columns[i + 1].Width = w;
                    dataGridViewT2.Columns[i + 2].Width = w;
                    dataGridViewT2.Columns[i + 3].Width = w;
                    dataGridViewT2.Columns[i + 4].Width = w;
                    dataGridViewT2.Columns[i + 5].Width = w;
                };
                this.Invoke(b1);
            }
        }

        void c()
        {
            var j = 1;
            var w = (int)((20 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            var w1 = (int)((30 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            var w2 = (int)((10 * JEInitialize.CharWidth) + 7 + (double)JEInitialize.CharWidth * ((double)14 / (double)18));
            var name1 = Common.User_DateTime == 1 ? Common.Sys_StkYear1 : Common.Sys_StkYear2;
            name1++;

            for (int i = 62; i <= 117; i += 5)
            {
                //var name = Common.User_DateTime == 1 ? Common.Sys_StkYear1.ToString() : Common.Sys_StkYear2.ToString();
                var name = name1 + (j).ToString().PadLeft(2, '0');
                j++;

                dataGridViewT1.Columns[i].HeaderText = name + "期初存量";
                dataGridViewT1.Columns[i + 1].HeaderText = name + "平均成本(初)";
                dataGridViewT1.Columns[i + 2].HeaderText = name + "累進數量";
                dataGridViewT1.Columns[i + 3].HeaderText = name + "累進成本";
                dataGridViewT1.Columns[i + 4].HeaderText = name + "平均成本";


                dataGridViewT2.Columns[i + 1].HeaderText = name + "期初存量";
                dataGridViewT2.Columns[i + 2].HeaderText = name + "平均成本(初)";
                dataGridViewT2.Columns[i + 3].HeaderText = name + "累進數量";
                dataGridViewT2.Columns[i + 4].HeaderText = name + "累進成本";
                dataGridViewT2.Columns[i + 5].HeaderText = name + "平均成本";

                Action c1 = () =>
                {
                    dataGridViewT1.Columns[i].Width = w;
                    dataGridViewT1.Columns[i + 1].Width = w;
                    dataGridViewT1.Columns[i + 2].Width = w;
                    dataGridViewT1.Columns[i + 3].Width = w;
                    dataGridViewT1.Columns[i + 4].Width = w;

                    dataGridViewT2.Columns[i + 1].Width = w;
                    dataGridViewT2.Columns[i + 2].Width = w;
                    dataGridViewT2.Columns[i + 3].Width = w;
                    dataGridViewT2.Columns[i + 4].Width = w;
                    dataGridViewT2.Columns[i + 5].Width = w;
                };
                this.Invoke(c1);
            }
        }

        private void btnQurry_Click(object sender, EventArgs e)
        {
            dataGridViewT1.Search("itno", qItno.Text.Trim());
        }

        private void dataGridViewT1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            var index = dataGridViewT1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            if (dataGridViewT1.SelectedRows == null)
                return;

            if (dataGridViewT1.SelectedRows[0].Index != index)
                return;

            ItNo.Text = dataGridViewT1["itno", index].Value.ToString();
            ItName.Text = dataGridViewT1["itname", index].Value.ToString();

            sCost.DefaultView.RowFilter = "ItNo='" + ItNo.Text.Trim() + "'";
        }

        private void dataGridViewT2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            var index = dataGridViewT2.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index == -1)
                return;

            if (dataGridViewT2.SelectedRows == null)
                return;

            if (dataGridViewT2.SelectedRows[0].Index != index)
                return;

            StNo.Text = dataGridViewT2["stno", index].Value.ToString();
            StName.Text = dataGridViewT2["stname", index].Value.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }




























    }
}
