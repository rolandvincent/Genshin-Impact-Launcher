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
    /// Interaction logic for ControlView.xaml
    /// </summary>
    public partial class ControlView : UserControl
    {
        public ControlView()
        {
            InitializeComponent();
        }

        private void MenuBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            myPopup.IsOpen = true;
        }

        private void MenuBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            myPopup.IsOpen = false;
        }
    }
}
