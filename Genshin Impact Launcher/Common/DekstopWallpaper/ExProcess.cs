using Genshin_Impact_Launcher.Common.DekstopWallpaper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Genshin_Impact_Launcher.Common.DesktopWallpaper
{

    public class ExProcess
    {
        public static Process proccess;
        
        public static void ProcessStart()
        {
            Process proc = Process.GetCurrentProcess();
            proc.Exited += Proc_Exited;
            Task.Run(async() =>
            {
                await proc.WaitForExitAsync();
            });
        }

        private static void Proc_Exited(object? sender, EventArgs e)
        {
            DesktopUtils.RefreshDesktop();
        }
    }
}
