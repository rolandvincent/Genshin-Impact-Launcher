using Genshin_Impact_Launcher.Components;
using Genshin_Impact_Launcher.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Genshin_Impact_Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            FlatWindow mainView = new FlatWindow(new MainView(), "Genshin Impact") { 
                Height = 740,
                Width = 1280,
            };
            mainView.Show();
            base.OnStartup(e);
        }
    }
}
