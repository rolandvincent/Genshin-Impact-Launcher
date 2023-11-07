using Genshin_Impact_Launcher.Common.DekstopWallpaper;
using Genshin_Impact_Launcher.Common.DesktopWallpaper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Genshin_Impact_Launcher.Utils
{
    internal class DesktopWallpaper
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessageTimeout(
              IntPtr windowHandle,
              uint Msg,
              IntPtr wParam,
              IntPtr lParam,
              SendMessageTimeoutFlags flags,
              uint timeout,
              out IntPtr result);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(
             IntPtr parentHandle,
             IntPtr childAfter,
             string className,
             IntPtr windowTitle);

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);

        private IntPtr ChildHandle = IntPtr.Zero;

        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0,
            SMTO_BLOCK = 1,
            SMTO_ABORTIFHUNG = 2,
            SMTO_NOTIMEOUTIFNOTHUNG = 8,
            SMTO_ERRORONEXIT = 32, // 0x00000020
        }

        public static uint WS_OVERLAPPED = 0;
        public static UInt32 WS_POPUP = 0x80000000;
        public static uint WS_CHILD = 0x40000000;
        public static uint WS_MINIMIZE = 0x20000000;
        public static uint WS_VISIBLE = 0x10000000;
        public static uint WS_DISABLED = 0x8000000;
        public static uint WS_CLIPSIBLINGS = 0x4000000;
        public static uint WS_CLIPCHILDREN = 0x2000000;
        public static uint WS_MAXIMIZE = 0x1000000;
        public static uint WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
        public static uint WS_BORDER = 0x800000;
        public static uint WS_DLGFRAME = 0x400000;
        public static uint WS_VSCROLL = 0x200000;
        public static uint WS_HSCROLL = 0x100000;
        public static uint WS_SYSMENU = 0x80000;
        public static uint WS_THICKFRAME = 0x40000;
        public static uint WS_GROUP = 0x20000;
        public static uint WS_TABSTOP = 0x10000;
        public static uint WS_MINIMIZEBOX = 0x20000;
        public static uint WS_MAXIMIZEBOX = 0x10000;
        public static uint WS_TILED = WS_OVERLAPPED;
        public static uint WS_ICONIC = WS_MINIMIZE;
        public static uint WS_SIZEBOX = WS_THICKFRAME;

        public static uint WS_EX_DLGMODALFRAME = 0x0001;
        public static uint WS_EX_NOPARENTNOTIFY = 0x0004;
        public static uint WS_EX_TOPMOST = 0x0008;
        public static uint WS_EX_ACCEPTFILES = 0x0010;
        public static uint WS_EX_TRANSPARENT = 0x0020;
        public static uint WS_EX_MDICHILD = 0x0040;
        public static uint WS_EX_TOOLWINDOW = 0x0080;
        public static uint WS_EX_WINDOWEDGE = 0x0100;
        public static uint WS_EX_CLIENTEDGE = 0x0200;
        public static uint WS_EX_CONTEXTHELP = 0x0400;
        public static uint WS_EX_RIGHT = 0x1000;
        public static uint WS_EX_LEFT = 0x0000;
        public static uint WS_EX_RTLREADING = 0x2000;
        public static uint WS_EX_LTRREADING = 0x0000;
        public static uint WS_EX_LEFTSCROLLBAR = 0x4000;
        public static uint WS_EX_RIGHTSCROLLBAR = 0x0000;
        public static uint WS_EX_CONTROLPARENT = 0x10000;
        public static uint WS_EX_STATICEDGE = 0x20000;
        public static uint WS_EX_APPWINDOW = 0x40000;
        public static uint WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        public static uint WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
        public static uint WS_EX_LAYERED = 0x00080000;
        public static uint WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
        public static uint WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
        public static uint WS_EX_COMPOSITED = 0x02000000;
        public static uint WS_EX_NOACTIVATE = 0x08000000;

        public static IntPtr workerw = IntPtr.Zero;
        public static Window? BackgroundWindow = null;

        public static void FindWorker()
        {
            IntPtr window = FindWindow("Progman", (string)null);
            IntPtr result = IntPtr.Zero;
            SendMessageTimeout(window, 1324U, new IntPtr(0), IntPtr.Zero, SendMessageTimeoutFlags.SMTO_NORMAL, 1000U, out result);
            workerw = IntPtr.Zero;
            EnumWindows((EnumWindowsProc)((tophandle, topparamhandle) =>
            {
                if (FindWindowEx(tophandle, IntPtr.Zero, "SHELLDLL_DefView", IntPtr.Zero) != IntPtr.Zero)
                    workerw = FindWindowEx(IntPtr.Zero, tophandle, "WorkerW", IntPtr.Zero);
                return true;
            }), IntPtr.Zero);

        }

        public static void RunLiveWallpaper(string FileName)
        {
            if (workerw == IntPtr.Zero) FindWorker();
            if (workerw != IntPtr.Zero)
            {
                if (BackgroundWindow != null)
                {
                    BackgroundWindow.Close();
                    BackgroundWindow = null;
                }
                BackgroundWindow = new Window()
                {
                    WindowStyle = WindowStyle.None,
                    ShowInTaskbar = false,
                };
                if (FileName.ToLower().EndsWith(".mp4") || FileName.ToLower().EndsWith(".avi") || FileName.ToLower().EndsWith(".mkv"))
                {
                    MediaElement mediaPlayer = new MediaElement() { 
                        Stretch = Stretch.UniformToFill,
                        LoadedBehavior = MediaState.Manual,
                        UnloadedBehavior = MediaState.Manual,
                    };
                    mediaPlayer.Source = new Uri(FileName);
                    mediaPlayer.IsMuted = true;
                    mediaPlayer.Play();
                    mediaPlayer.MediaEnded += delegate {
                        mediaPlayer.Position = new TimeSpan(0, 0, 0);
                        mediaPlayer.Play();
                    };
                    BackgroundWindow.Content = mediaPlayer;
                }
                else
                {
                    Image wallpaper = new Image()
                    {
                        Stretch = Stretch.UniformToFill
                    };
                    wallpaper.Source = new BitmapImage(new Uri(FileName));
                    BackgroundWindow.Content = wallpaper;
                }

                BackgroundWindow.Show();

                const int GWL_STYLE = (-16);
                const int GWL_EXSTYLE = (-20);
                const int WS_CHILD = 0x40000000;

                var IH = new WindowInteropHelper(BackgroundWindow);

                SetWindowLong(new WindowInteropHelper(BackgroundWindow).Handle, GWL_STYLE, WS_CHILD);
                SetWindowLong(IH.Handle,GWL_STYLE, WS_VISIBLE | WS_POPUP | WS_DISABLED | WS_CLIPSIBLINGS | WS_CLIPCHILDREN | WS_OVERLAPPED | WS_MAXIMIZE | 0x80);
                SetWindowLong(IH.Handle,GWL_EXSTYLE, WS_EX_LTRREADING | WS_EX_RIGHTSCROLLBAR| WS_EX_CONTEXTHELP | WS_EX_LAYERED);

                SetParent(new WindowInteropHelper(BackgroundWindow).Handle,workerw);

                BackgroundWindow.Width = SystemParameters.PrimaryScreenWidth;
                BackgroundWindow.Height = SystemParameters.PrimaryScreenHeight;
                BackgroundWindow.Top = BackgroundWindow.Left = 0;

            }
        }
    }
}
