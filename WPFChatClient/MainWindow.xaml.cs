using ChatClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Process winforms = Process.GetProcesses().FirstOrDefault((proc) => proc.ProcessName.Contains("WinFormsChatClient"));
            if (winforms == null)
            {
                MessageBox.Show("WinFormsCharClient is not running");
            }
            else
            {
                new Win32Helper().SendStringViaCopyData(winforms.MainWindowHandle, MessageToSendTextBox.Text);
                //Win32.SendMessage(winforms.MainWindowHandle, Win32.WM_USER, IntPtr.Zero, MessageToSendTextBox.Text);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == Win32.WM_COPYDATA)
            {
                Win32.COPYDATASTRUCT data = Marshal.PtrToStructure<Win32.COPYDATASTRUCT>(lParam);

                string str = Marshal.PtrToStringUni(data.lpData);

                WriteToMessages(str);
            }
            return IntPtr.Zero;
        }

        private void WriteToMessages(string str)
        {
            MessagesTextBox.Text += Environment.NewLine + "WinForms Says: " + str;
        }
    }
}
