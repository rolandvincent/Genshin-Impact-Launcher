using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Genshin_Impact_Launcher.Common.DekstopWallpaper
{
    public static class DesktopUtils
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);

        public static UInt32 SPI_SETDESKWALLPAPER = 20;
        public static UInt32 SPIF_UPDATEINIFILE = 0x1;

        public static void RefreshDesktop()
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, null, SPIF_UPDATEINIFILE);
        }
    }
}
