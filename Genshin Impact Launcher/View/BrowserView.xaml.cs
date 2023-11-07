using Genshin_Impact_Launcher.Common.DekstopWallpaper;
using Genshin_Impact_Launcher.Components;
using Genshin_Impact_Launcher.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace Genshin_Impact_Launcher.View
{
    /// <summary>
    /// Interaction logic for BrowserView.xaml
    /// </summary>
    public partial class BrowserView : UserControl
    {
        public BrowserView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WebView1.CoreWebView2InitializationCompleted += WebView1_CoreWebView2InitializationCompleted;
        }

        private async void CoreWebView2_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
//            await WebView1.CoreWebView2.ExecuteScriptAsync(@"
//var home = document. getElementsByClassName(""home"")
//home[0].style.backgroundImage = ""url(https://launcher-webstatic.hoyoverse.com/launcher-public/2023/09/26/cde91234e665ffee2f6346a6734228d4_3824286141505530716.png)""
//");
        }

        private void WebView1_CoreWebView2InitializationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            WebView1.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
            WebView1.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = false;
            WebView1.CoreWebView2.Settings.IsStatusBarEnabled = false;
            WebView1.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
            WebView1.CoreWebView2.Settings.IsWebMessageEnabled = false;
            WebView1.CoreWebView2.Settings.IsZoomControlEnabled = false;
            WebView1.CoreWebView2.Settings.AreDevToolsEnabled = false;
            WebView1.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;

            WebView1.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;

            WebView1.CoreWebView2.ContextMenuRequested += CoreWebView2_ContextMenuRequested;
        }

        private void CoreWebView2_ContextMenuRequested(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2ContextMenuRequestedEventArgs e)
        {
            defaultContextMenu.IsOpen = true;
            e.Handled = true;
        }

        private void ChangeWallpaper(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oplg = new OpenFileDialog()
            {
                Title = "Open Media",
                Filter = "All Media|*.mp4;*.jpg;*.png;*.bmp;*.avi;*.mkv"
            };
            if (oplg.ShowDialog() == true)
            {
                Config.CurrentWallpaper = oplg.FileName;
                var Wallpaper = (((FlatWindow)GetWindow.MainWindow).WindowContent as MainView).LiveWallpaper;
                var StaticWallpaper = (((FlatWindow)GetWindow.MainWindow).WindowContent as MainView).StaticBackground;
                if (oplg.FileName.ToLower().EndsWith(".mp4") || oplg.FileName.ToLower().EndsWith(".avi") || oplg.FileName.ToLower().EndsWith(".mkv"))
                {
                    StaticWallpaper.Visibility = Visibility.Collapsed;
                    Wallpaper.Visibility = Visibility.Visible;
                    Wallpaper.Source = new Uri(oplg.FileName);
                    Wallpaper.Play();
                }
                else
                {
                    StaticWallpaper.Visibility = Visibility.Visible;
                    StaticWallpaper.Source = new BitmapImage(new Uri(oplg.FileName));
                    Wallpaper.Visibility = Visibility.Collapsed;
                    Wallpaper.Stop();
                }
            }
        }

        private void ApplyToDesktopWallpaper(object sender, RoutedEventArgs e)
        {
            DesktopWallpaper.RunLiveWallpaper(Config.CurrentWallpaper);
        }
    }
}
