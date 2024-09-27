using System.Data;
using System.Linq;
using System.Threading.Tasks;
using S_61.Basic;

namespace JBS.JS
{
    public class Cust : xDocuments, IxValidate
    {
        public string ValiTable
        {
            get { return "Cust"; }
        }

        public string ValiKey
        {
            get { return "cuno"; }
        }

        protected override string MasterName
        {
            get { return "Cust"; }
        }

        protected override string KeyName
        {
            get { return "cuno"; }
        }

        public System.Windows.Forms.Form TOpen()
        {
            return new S_61.SOther.FrmCustb();
        }

        public bool IsExist(string cuno)
        {
            if (cuno.Trim().Length == 0)
                return false;

            using (var db = new xSQL())
            {
                var tsql = "Select Count(*) from cust where cuno = @cuno ";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("cuno", cuno));
                return count.ToDecimal() > 0;
            }
        }

        private bool IsEmpty()
        {
            using (var db = new xSQL())
            {
                var tsql = "Select Count(*) from cust";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("cuno", "1"));
                return count.ToDecimal() == 0;
            }
        }

        private string[] FistLast(string cuno)
        {
            var index = -1;
            var seek = S_61.MainForm.main.dyCust.FirstOrDefault(dy => string.CompareOrdinal(dy.Value, cuno) >= 0);

            if (seek.Key == null)
                index = S_61.MainForm.main.dyCust.Count - 1;
            else
                index = seek.Key.ToInteger();

            var first = index - (S_61.Basic.Common.SearchCount / 2);
            var last = index + (S_61.Basic.Common.SearchCount / 2);

            if (first <= 0)
                first = 0;

            if (last >= S_61.MainForm.main.dyCust.Count - 1)
                last = S_61.MainForm.main.dyCust.Count - 1;

            //var omin = S_61.MainForm.main.dyCust.ElementAt(first).Value;
            //var omax = S_61.MainForm.main.dyCust.ElementAt(last).Value;
            var omin = "";
            var omax = "";
            if (S_61.MainForm.main.dyCust.Count > 0)
            {
                omin = S_61.MainForm.main.dyCust.ElementAt(first).Value;
                omax = S_61.MainForm.main.dyCust.ElementAt(last).Value;
            }
            var min = "";
            if (first == 0)
            {
                using (var db = new xSQL())
                {
                    var tsql = "Select top 1 cuno from cust order by cuno";
                    var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("cuno", "1"));
                    if (obj != null)
                        min = obj.ToString().Trim();
                }
            }

            var max = "";
            if (last == S_61.MainForm.main.dyCust.Count - 1)
            {
                using (var db = new xSQL())
                {
                    var tsql = "Select top 1 cuno from cust order by cuno desc";
                    var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("cuno", "1"));
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

        public int Search(string cuno, ref System.Data.DataTable dt)
        {
            dt.Clear();

            if (this.IsEmpty())
            {
                System.Windows.Forms.MessageBox.Show("查無資料!");
                return 0;
            }

            var fl = FistLast(cuno);

            using (var db = new xSQL())
            {
                var tsql = "Select 點選='',* from Cust where cuno >= @cuno and cuno <= @cuno1 order by cuno";
                db.Fill(tsql, spc =>
                {
                    spc.AddWithValue("cuno", fl.First());
                    spc.AddWithValue("cuno1", fl.Last());
                }, ref dt);
            }

            return dt.Rows.Count;
        }

        public void SeekCurrent(string val, System.Data.DataTable dt, JE.MyControl.DataGridViewT dataGridViewT1, string column = "cuno")
        {
            if (dt.Rows.Count == 0)
                return;

            var index = -1;
            Parallel.For(0, dt.Rows.Count, (i, loopstate) =>
            {
                if (dt.Rows[i][column].ToString() == val)
                {
                    index = i;
                    loopstate.Stop();
                }
            });

            if (index == -1)
            {
                index = dt.AsEnumerable().ToList().FindIndex(r => string.CompareOrdinal(r[column].ToString().Trim(), val) > 0);

                if (index == -1)
                    index = dt.Rows.Count - 1;
            }

            if (index <= 0)
                index = 0;

            if (index >= dt.Rows.Count - 1)
                index = dt.Rows.Count - 1;

            dataGridViewT1.FirstDisplayedScrollingRowIndex = index;
            dataGridViewT1.CurrentCell = dataGridViewT1[0, index];
            dataGridViewT1.Rows[index].Selected = true;
        }

    }
}
