using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Genshin_Impact_Launcher.Extensions
{
    public static class ThinknessExtensions
    {
        public static Thickness Multiply(this Thickness thickness, double value)
        {
            return new Thickness(thickness.Left * value, thickness.Top * value, thickness.Right * value, thickness.Bottom * value);
        }
    }
}
