using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JBS.JM
{
    public partial class Msg : Form
    {
        public Msg()
        {
            InitializeComponent();
        }

        public Msg(string msg)
        {
            InitializeComponent();
            labelT1.Text = msg;
        }

        private void mBox_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public static int Show(string msg)
        {
            using (Msg frm = new Msg())
            {
                frm.labelT1.Text = msg;
                frm.ShowDialog();
            }
            return 0;
        }

        delegate void myDelegate();
        int Count = 1000;
        bool Stop = false;
        object lockobj = new object();
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Enter)
            {
                this.Button1.Enabled = false;

                lock (this.lockobj)
                {
                    //停掉其它執行序
                    this.Stop = true; 
                }

                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(10);

                    //開始新的執行序
                    lock (lockobj)
                    {
                        this.Stop = false;
                        this.Count = 1000;
                    }
                    this.CountDown();
                }); 
            } 
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void CountDown()
        {
            while (true)
            {
                lock (this.lockobj)
                {
                    Count -= 1;

                    if (this.Stop)
                    { 
                        break;
                    }

                    if (Count <= 0)
                    { 
                        this.Invoke(new myDelegate(() => Button1.Enabled = true));
                        break;
                    }
                }
                System.Threading.Thread.Sleep(1);
            }
        }
    }
}
