using System;
using System.Windows.Forms;
using S_61.Basic;
using System.Threading;

namespace S_61
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            S_61.Basic.JEInitialize.IsRunTime = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
