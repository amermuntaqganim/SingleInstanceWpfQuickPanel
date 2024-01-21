using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfSingleInstance
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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Handle messages...
            if (msg == User32API.WM_SHOWME)
            {
                Console.WriteLine("Existing Window");
                if(this.WindowState == WindowState.Minimized )
                {
                    WindowState = WindowState.Normal;
                }
                if(this.Visibility == Visibility.Hidden || this.Visibility == Visibility.Collapsed) 
                {
                    Show();
                }
            }

            return IntPtr.Zero;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = System.Windows.SystemParameters.WorkArea.Right - this.Width;
            this.Top = System.Windows.SystemParameters.WorkArea.Bottom - this.Height;
        }


    }

    


    
}
