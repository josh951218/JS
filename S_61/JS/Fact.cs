using System.Data;
using System.Linq;
using System.Threading.Tasks;
using S_61.Basic;

namespace JBS.JS
{
    public class Fact : IxValidate
    {
        public string ValiTable
        {
            get { return "Fact"; }
        }

        public string ValiKey
        {
            get { return "fano"; }
        }
         
        public System.Windows.Forms.Form TOpen()
        {
            return new S_61.SOther.FrmFactb();
        }

        public bool IsExist(string fano)
        {
            if (fano.Trim().Length == 0)
                return false;

            using (var db = new xSQL())
            {
                var tsql = "Select Count(*) from fact where fano = @fano ";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("fano", fano));
                return count.ToDecimal() > 0;
            }
        }

        private bool IsEmpty()
        {
            using (var db = new xSQL())
            {
                var tsql = "Select Count(*) from fact";
                var count = db.ExecuteScalar(tsql, spc => spc.AddWithValue("fano", "1"));
                return count.ToDecimal() == 0;
            }
        }

        private string[] FistLast(string fano)
        {
            var index = -1;
            var seek = S_61.MainForm.main.dyFact.FirstOrDefault(dy => string.CompareOrdinal(dy.Value, fano) >= 0);

            if (seek.Key == null)
                index = S_61.MainForm.main.dyFact.Count - 1;
            else
                index = seek.Key.ToInteger();

            var first = index - (S_61.Basic.Common.SearchCount / 2);
            var last = index + (S_61.Basic.Common.SearchCount / 2);

            if (first <= 0)
                first = 0;

            if (last >= S_61.MainForm.main.dyFact.Count - 1)
                last = S_61.MainForm.main.dyFact.Count - 1;

            var omin = S_61.MainForm.main.dyFact.ElementAt(first).Value;
            var omax = S_61.MainForm.main.dyFact.ElementAt(last).Value;

            var min = "";
            if (first == 0)
            {
                using (var db = new xSQL())
                {
                    var tsql = "Select top 1 fano from fact order by fano";
                    var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("fano", "1"));
                    if (obj != null)
                        min = obj.ToString().Trim();
                }
            }

            var max = "";
            if (last == S_61.MainForm.main.dyFact.Count - 1)
            {
                using (var db = new xSQL())
                {
                    var tsql = "Select top 1 fano from fact order by fano desc";
                    var obj = db.ExecuteScalar(tsql, spc => spc.AddWithValue("fano", "1"));
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

        public int Search(string fano, ref System.Data.DataTable dt)
        {
            dt.Clear();

            if (this.IsEmpty())
            {
                System.Windows.Forms.MessageBox.Show("查無資料!");
                return 0;
            }

            var fl = FistLast(fano);

            using (var db = new xSQL())
            {
                var tsql = "Select 點選='',* from Fact where fano >= @fano and fano <= @fano1 order by fano";
                db.Fill(tsql, spc =>
                {
                    spc.AddWithValue("fano", fl.First());
                    spc.AddWithValue("fano1", fl.Last());
                }, ref dt);
            }

            return dt.Rows.Count;
        }

        public void SeekCurrent(string val, System.Data.DataTable dt, JE.MyControl.DataGridViewT dataGridViewT1, string column = "fano")
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