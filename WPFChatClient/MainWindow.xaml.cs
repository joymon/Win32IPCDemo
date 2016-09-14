using ChatClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
            Process winforms = Process.GetProcesses().FirstOrDefault((proc)=>proc.ProcessName.Contains("WinFormsChatClient"));
            if (winforms == null)
            {
                MessageBox.Show("WinFormsCharClient is not running");
            }
            else
            {
                new Win32Helper().SendString(winforms.MainWindowHandle, MessageToSendTextBox.Text);
                //Win32.SendMessage(winforms.MainWindowHandle, Win32.WM_USER, IntPtr.Zero, MessageToSendTextBox.Text);
            }
        }
    }
}
