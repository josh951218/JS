using System.Data;
using System.Linq;
using System.Threading.Tasks;
using S_61.Basic;

namespace JBS.JS
{
    public class Item : xDocuments, IxValidate
    {
        public string TSQL = "";

        public string ValiTable
        {
            get { return "Item"; }
        }

        public string ValiKey
        {
            get { return "itno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new S_61.SOther.FrmItemb();
        }

        public bool IsExist(string itno)
        {
            if (itno.Trim().Length == 0)
                return false;

            using (var db = new xSQL())
            {
                var tsql = "Select Count(*) from item where itno = @itno ";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("itno", itno));
                return count.ToDecimal() > 0;
            }
        }

        private bool IsEmpty()
        {
            using (var db = new xSQL())
            {
                var tsql = "Select Count(*) from item";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("itno", "1"));
                return count.ToDecimal() == 0;
            }
        }

        private string[] FistLast(string itno)
        {
            var index = -1;
            var seek = S_61.MainForm.main.dyItem.FirstOrDefault(dy => string.CompareOrdinal(dy.Value, itno) >= 0);

            if (seek.Key == null)
                index = S_61.MainForm.main.dyItem.Count - 1;
            else
                index = seek.Key.ToInteger();

            var first = index - (S_61.Basic.Common.SearchCount / 2);
            var last = index + (S_61.Basic.Common.SearchCount / 2);

            if (first <= 0)
                first = 0;

            if (last >= S_61.MainForm.main.dyItem.Count - 1)
                last = S_61.MainForm.main.dyItem.Count - 1;

            //var omin = S_61.MainForm.main.dyItem.ElementAt(first).Value;
            //var omax = S_61.MainForm.main.dyItem.ElementAt(last).Value;
            var omin = "";
            var omax = "";
            if (S_61.MainForm.main.dyItem.Count > 0)
            {
                omin = S_61.MainForm.main.dyItem.ElementAt(first).Value;
                omax = S_61.MainForm.main.dyItem.ElementAt(last).Value;
            }
            var min = "";
            if (first == 0)
            {
                using (var db = new xSQL())
                {
                    var tsql = "Select top 1 itno from item order by itno";
                    var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("itno", "1"));
                    if (obj != null)
                        min = obj.ToString().Trim();
                }
            }

            var max = "";
            if (last == S_61.MainForm.main.dyItem.Count - 1)
            {
                using (var db = new xSQL())
                {
                    var tsql = "Select top 1 itno from item order by itno desc";
                    var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("itno", "1"));
                    if (obj != null)
                        max = obj.ToString().Trim();
                }
            }

            if (min.Length > 0 && min != omin)
                omin = min;

            if (max.Length > 0 && max != omax)
                omax = max;

            return new string[] { omin, omax };
        }

        public int Search(string itno, ref System.Data.DataTable dt)
        {
            dt.Clear();

            if (this.IsEmpty())
            {
                System.Windows.Forms.MessageBox.Show("查無資料!");
                return 0;
            }

            var fl = FistLast(itno);

            using (var db = new xSQL())
            {
                var tsql = this.TSQL + " and itno >= @itno and itno <= @itno1 order by itno";
                string a = fl.First().ToString();
                string b = fl.Last().ToString();
                db.Fill(tsql, spc =>
                {
                    spc.AddWithValue("itno", fl.First());
                    spc.AddWithValue("itno1", fl.Last());
                }, ref dt);
            }

            return dt.Rows.Count;
        }

        public void SeekCurrent(string val, System.Data.DataTable dt, JE.MyControl.DataGridViewT dataGridViewT1, string column = "itno")
        {
            if (dt.Rows.Count == 0)
                return;

            var index = -1;
            Parallel.For(0, dt.Rows.Count, (i, loopstate) =>
            {
                if (dt.DefaultView[i][column].ToString() == val)
                {
                    index = i;
                    loopstate.Stop();
                }
            });

            if (index == -1)
            {
                using (var db = new xSQL())
                {
                    var tsql = "select top(1)itno from item where itno >= @itno";
                    var itno= db.ExecuteScalar(tsql, spc =>spc.AddWithValue("itno", val));
                    Parallel.For(0, dt.Rows.Count, (i, loopstate) =>
                    {
                        if (dt.Rows[i][column].ToString() == itno.ToString())
                        {
                            index = i;
                            loopstate.Stop();
                        }
                    });
                }
            }

            if (index <= 0)
                index = 0;

            if (index >= dt.Rows.Count - 1)
                index = dt.Rows.Count - 1;

            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }


        protected override string MasterName
        {
            get { return "Item"; }
        }

        protected override string KeyName
        {
            get { return "ItNo"; }
        }
    }
}
