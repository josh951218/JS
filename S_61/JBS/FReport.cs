using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FastReport.Editor;
using FastReport;
using FastReport.Utils;
using S_61.Basic;
using System.Windows.Forms;

namespace JBS
{
    public class FReport : IDisposable
    {
        #region 建構解構
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                dt.Clear();
                dt = null;
            }

            // Free any unmanaged objects here.
            //

            disposed = true;
        }

        ~FReport()
        {
            Dispose(false);
        }
        #endregion

        DataTable dt;
        string name;
        string path;
        public Dictionary<string, object> dy = new Dictionary<string, object>();
        public bool ShowDialog { get; set; }
        public string Printer { get; set; }

        public FReport()
        {
            this.ShowDialog = true;
            this.Printer = "";

            //報表參數設定
            dy.Add("txtstart", Common.Sys_StcPnName);
        }

        void SetParamaters(Report fs)
        {
            for (int i = 0; i < dy.Count; i++)
            {
                var p = fs.GetParameter(dy.ElementAt(i).Key);
                if (p != null)
                {
                    p.Value = dy.ElementAt(i).Value;
                }
            }
        }

        void PreView()
        {
            using (Report fs = new Report())
            {
                if (System.IO.File.Exists(path) == false)
                {
                    MessageBox.Show("找不到報表!\n" + path);
                    return;
                }
                fs.Load(path);
                fs.RegisterData(dt, name);
                SetParamaters(fs);

                fs.Prepare();
                fs.Show();
            }
        }

        void Print()
        {
            using (Report fs = new Report())
            {
                if (System.IO.File.Exists(path) == false)
                {
                    MessageBox.Show("找不到報表!\n" + path);
                    return;
                }

                fs.Load(path);
                fs.RegisterData(dt, name);
                SetParamaters(fs);

                fs.Prepare();
                fs.PrintSettings.ShowDialog = this.ShowDialog;
                if (this.Printer.Length > 0)
                {
                    fs.PrintSettings.Printer = this.Printer;
                }
                fs.Print();
            }
        }

        int Design()
        {
            try
            {
                using (Report fs = new Report())
                {
                    if (System.IO.File.Exists(path) == false)
                    {
                        MessageBox.Show("找不到報表!\n" + path);
                        return 0;
                    }

                    fs.Load(path);
                    fs.RegisterData(dt, name);
                    SetParamaters(fs);

                    fs.Design();
                }

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        internal int OutReport(RptMode mode, DataTable t, string n, string p)
        {
            this.dt = t.Copy();
            this.name = n;
            this.path = p;

            Res.LoadLocale(Application.StartupPath + @"\DLL\Localization\Chinese (Traditional).frl");

            if (mode == RptMode.Design)
                return this.Design();

            if (mode == RptMode.Print)
                this.Print();
            else if (mode == RptMode.PreView)
                this.PreView();

            return 0;
        }

        internal static void Design(string path)//1119修改報表
        {
           try
            {
                using (Report fs = new Report())
                {
                    if (System.IO.File.Exists(path) == false)
                    {
                        MessageBox.Show("找不到報表!\n" + path);
                    }

                    fs.Load(path);
                    fs.Design();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
