using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WpfSingleInstance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {

        static Mutex mutex = new Mutex(true, "WPF_SINGLE_INSTANCE");
        NotifyIcon notifyIcon = null;
        public static bool isQuickPanelToggle = true;
        App()
        {

            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                // Send Win32 message to shown currently existing instance window
                Console.WriteLine("Send Win32 message Synchronously or Aysnchronously");

                //If Main Window ShowInTaskbar == false, use SendMessage and if ShowInTaskbar == true then use Postmessage
               User32API.SendMessage((IntPtr)User32API.HWND_BROADCAST, User32API.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);

                //Console.WriteLine("Send Win32 message Asynchronously");
               //User32API.PostMessage((IntPtr)User32API.HWND_BROADCAST, User32API.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);

                App.Current.Shutdown();

            }
            else
            {

                // First Time Entry, Created a Tray Icon.

                notifyIcon = new NotifyIcon();

            }
            
        }
        
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            notifyIcon.Icon = new System.Drawing.Icon("icon.ico");
            notifyIcon.Text = "My Quick Panel";
            notifyIcon.Click += NotifyIcon_Click;
            notifyIcon.MouseClick += NotifyIcon_MouseClick;


            notifyIcon.Visible = true;

            MainWindow = new MainWindow();
            
            MainWindow.Show();
        }

        //For Closing the Application User needs to right click on the Tray Icon
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                notifyIcon.Dispose();
                App.Current.Shutdown();
            }
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {

            

            if (isQuickPanelToggle || MainWindow.Visibility == Visibility.Visible)
            {
                Console.WriteLine("Hide Window");
                MainWindow.Hide();
                isQuickPanelToggle = false;
            }
            else
            {
                Console.WriteLine("Show Window");
                MainWindow.Show();
                isQuickPanelToggle = true;
            }

        }

    }
}
