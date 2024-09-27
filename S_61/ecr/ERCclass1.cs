using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using JE.MyControl;
namespace S_61.ecr
{
    public partial class ERCclass1 : Formbase
    {
        public ERCclass1()
        {
            InitializeComponent();
        }
        int times=0;
       public string dir = System.Windows.Forms.Application.StartupPath;
        class RawCARD
        {
            [DllImport( "Config.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        }
      //  [DllImport("Config.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        
        

        public void ERCclass1_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(dir + "//ecr//out.txt")) //刪除out.txt
                    File.Delete(dir + "//ecr//out.txt");
                string code = "";  // in.txt格式
                using (StreamWriter sw = new StreamWriter(dir + "//ecr//in.txt"))   //小寫TXT     
                    sw.Write(code);
                IntPtr PDC = RawCARD.FindWindow(null, "ecr");  //開啟PosDataCom
                if (PDC == (IntPtr)0)
                {
                    try
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = dir + "//ecr//ecr.exe";
                        p.StartInfo.WorkingDirectory = dir;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }


                this.timer1.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (times == 5)
            {
                times = 0;
                times++;
                if (times > 4)
                {
                    //times = 0;
                    try
                    {
                        if (File.Exists(dir + "//ecr//out.txt"))
                        {

                            using (StreamReader sr = new StreamReader(dir + "//ecr//out.txt"))     //小寫TXT
                            {
                                String line;

                                if ((line = sr.ReadLine()) != null)
                                {
                                    // 解析
                                    MessageBox.Show(line.ToString());
                                }
                                //else
                                    //return false;
                                //return true;
                            }
                        }
                        //return false;
                    }
                    catch (Exception ex)
                    {
                        //return false;
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            times++;
        }
    }
}
