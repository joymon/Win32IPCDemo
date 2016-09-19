using ChatClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        protected override void WndProc(ref Message m)
        {
            if(m.Msg == Win32.WM_USER)
            {

                string msg = "";
                msg = (string)m.GetLParam(msg.GetType());
                WriteToMessages(msg);
            }
            else if(m.Msg == Win32.WM_COPYDATA)
            {
                Win32.COPYDATASTRUCT data = (Win32.COPYDATASTRUCT)
                    m.GetLParam(typeof(Win32.COPYDATASTRUCT));

                string str =Marshal.PtrToStringUni(data.lpData);

                WriteToMessages(str);
            }
            base.WndProc(ref m);
        }

        private void WriteToMessages(string msg)
        {
            MessagesTextBox.Text += Environment.NewLine + "WPF Says: " + msg;
        }

        int a=0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((a++ & 0x10) != 0)
            {
                Foo();
                a = 1;
            }
            DoWork1();
        }

        private void Foo()
        {
            Console.WriteLine("Foo");
        }

        private void DoWork1()
        {
            Console.WriteLine("Do Work");
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            Process wpf = Process.GetProcesses().FirstOrDefault((proc) => proc.ProcessName.Contains("WPFChatClient"));
            if (wpf == null)
            {
                MessageBox.Show("WinFormsCharClient is not running");
            }
            else
            {
                new Win32Helper().SendStringViaCopyData(wpf.MainWindowHandle, MessageToSendTextBox.Text);
            }
        }
    }
}
