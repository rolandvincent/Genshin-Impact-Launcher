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
using Genshin_Impact_Launcher.Common.DekstopWallpaper;
using Genshin_Impact_Launcher.Utils;
using Microsoft.Web.WebView2;
using Microsoft.Web.WebView2.Wpf;

namespace Genshin_Impact_Launcher.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private Window browserWindow = null;
        private Window ControlsWindow = null;
        public MainView()
        {
            InitializeComponent();

            browserWindow = new Window()
            {
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                Background = new SolidColorBrush(Color.FromArgb(1,255,255,255)),
            };
            ControlsWindow = new Window()
            {
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                Background = new SolidColorBrush(Colors.Transparent),
            };
            WebView2 WB = new WebView2();

            //WB.Initialized += new EventHandler((object sender, EventArgs e) => {
            //    WB.NavigateToString("<html></html>");
            //});


            browserWindow.Show();
            ControlsWindow.Show();
            browserWindow.Content = new BrowserView();
            ControlsWindow.Content = new ControlView();

            var Broswerhandle = new WindowInteropHelper(browserWindow).Handle;
            var Controlhandle = new WindowInteropHelper(ControlsWindow).Handle;

            if (Broswerhandle != IntPtr.Zero && Controlhandle != IntPtr.Zero)
            {
                var host = new HwndHostEx(Broswerhandle);
                var controlhost = new HwndHostEx(Controlhandle);

                hwndDisplay.Children.Add(host);
                hwndDisplay.Children.Add(controlhost);
            }

            LiveWallpaper.Source = new Uri(Config.CurrentWallpaper);
            LiveWallpaper.Play();
            LiveWallpaper.MediaEnded += LiveWallpaper_MediaEnded;

        }

        private void LiveWallpaper_MediaEnded(object sender, RoutedEventArgs e)
        {
            LiveWallpaper.Position = new TimeSpan(0, 0, 0);
            LiveWallpaper.Play();
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);


        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            browserWindow.Close();
            ControlsWindow.Close();
            DesktopWallpaper.BackgroundWindow.Close();
        }
    }
}
