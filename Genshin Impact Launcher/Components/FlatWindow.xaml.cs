using Genshin_Impact_Launcher.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Genshin_Impact_Launcher.Components
{
    /// <summary>
    /// Interaction logic for FlatWindow.xaml
    /// </summary>
    public partial class FlatWindow : Window
    {
        private FrameworkElement _windowContent = new Grid();
        public FrameworkElement WindowContent
        {
            get
            {
                return _windowContent;
            }
            set
            {
                _windowContent = value;
                AppArea.Child = _windowContent;
            }
        }

        public FlatWindow(FrameworkElement View, string Title)
        {
            InitializeComponent();

            WindowContent = View;
            this.Title = Title;

            Style? captionTextStyle = FindResource("CaptionText") as Style;
            SetValue(CaptionTextStyleProperty, captionTextStyle);

            this.CaptionBackgroundBrush = FindResource("TitleBarBackgroundBrush") as Brush;

            StateChanged += MainWindowStateChangeRaised;
            SizeChanged += Window_SizeChanged;

            Genshin_Impact_Launcher.Utils.GetWindow.MainWindow = this;

        }

        public FlatWindow(FrameworkElement View, string Title, Size size)
        {
            InitializeComponent();

            WindowContent = View;
            this.Title = Title;
            this.Height = size.Height;
            this.Width = size.Width;


            Style? captionTextStyle = FindResource("CaptionText") as Style;
            SetValue(CaptionTextStyleProperty, captionTextStyle);

            this.CaptionBackgroundBrush = FindResource("TitleBarBackgroundBrush") as Brush;

            StateChanged += MainWindowStateChangeRaised;
            SizeChanged += Window_SizeChanged;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Minimize
        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        // Maximize
        private void CommandBinding_Executed_Maximize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        // Restore
        private void CommandBinding_Executed_Restore(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        // Close
        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        public int CaptionHeight
        {
            get { return (int)GetValue(CaptionHeightProperty); }
            set { SetValue(CaptionHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register("CaptionHeight", typeof(int), typeof(FlatWindow), new PropertyMetadata(40));

        public Brush WindowBorderBrush
        {
            get { return (Brush)GetValue(WindowBorderBrushProperty); }
            set { SetValue(WindowBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowBorderBrushProperty =
            DependencyProperty.Register("WindowBorderBrush", typeof(Brush), typeof(FlatWindow), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public int WindowBorderThickness
        {
            get { return (int)GetValue(WindowBorderThicknessProperty); }
            set { SetValue(WindowBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowBorderThicknessProperty =
            DependencyProperty.Register("WindowBorderThickness", typeof(int), typeof(FlatWindow), new PropertyMetadata(0));


        public Brush CaptionBackgroundBrush
        {
            get { return (Brush)GetValue(CaptionBackgroundBrushProperty); }
            set { SetValue(CaptionBackgroundBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaptionBackgroundBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionBackgroundBrushProperty =
            DependencyProperty.Register("CaptionBackgroundBrush", typeof(Brush), typeof(FlatWindow), new PropertyMetadata(new SolidColorBrush(Colors.Black)));



        public Style CaptionTextStyle
        {
            get { return (Style)GetValue(CaptionTextStyleProperty); }
            set { SetValue(CaptionTextStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaptionTextStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionTextStyleProperty =
            DependencyProperty.Register("CaptionTextStyle", typeof(Style), typeof(FlatWindow));

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FormattedText formattedText = new FormattedText(Title, Thread.CurrentThread.CurrentCulture, FlowDirection.LeftToRight, new Typeface(txtTitle.FontFamily, txtTitle.FontStyle, txtTitle.FontWeight, txtTitle.FontStretch), txtTitle.FontSize, txtTitle.Foreground);
            if ((this.Width / 2d - TitleButtons.ActualWidth) - (formattedText.Width + txtTitle.Margin.Left * 2d) / 2d < 0)
                txtTitle.Margin = new Thickness(3, 0, TitleButtons.ActualWidth, 0);
            else
                txtTitle.Margin = new Thickness(3, 0, 0, 0);
        }

        private void MainWindowStateChangeRaised(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                MainWindowBorder.BorderThickness = SystemParameters.WindowResizeBorderThickness.Multiply(2);
                RestoreButton.Visibility = Visibility.Visible;
            }
            else
            {
                MainWindowBorder.BorderThickness = new Thickness(0);
                RestoreButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Screenshot_Click(object sender, RoutedEventArgs e)
        {
            string AssemblyPath = Environment.CurrentDirectory;
            if (System.IO.Directory.Exists(System.IO.Path.Join(new string[] { AssemblyPath, "Genshin Impact game\\ScreenShot" })))
            {
                System.Diagnostics.Process.Start(System.IO.Path.Join(new string[] { AssemblyPath, "Genshin Impact game\\ScreenShot" }));
            }

        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
