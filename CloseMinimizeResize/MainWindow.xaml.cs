using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CloseMinimizeResize
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Activated += MainWindow_OnActivated;
        }

        bool AlreadyFaded;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseDown += delegate { DragMove(); };
            AlreadyFaded = false;
        }
        private void MainWindow_OnActivated(object sender, EventArgs e)
        {
            //change the WindowStyle back to None, but only after the Window has been activated
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => WindowStyle = WindowStyle.None));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //WindowStyle = WindowStyle.SingleBorderWindow;

            //if (!AlreadyFaded)
            //{
            //    AlreadyFaded = true;
            //    var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
            //    anim.Completed += new EventHandler(anim_Completed);
            //    BeginAnimation(OpacityProperty, anim);
            //}

            var hide = FindResource("HideWindow") as Storyboard;
            if (hide != null)
            {
                Double CurrentHeight = Height;
                Double CurrentWidth = Width;

                hide.Completed += (s, ea) => {

                    Close();

                };

                hide.Begin(this, true);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //WindowStyle = WindowStyle.SingleBorderWindow;
            //WindowState = WindowState.Minimized;


            //if (!AlreadyFaded)
            //{
            //    AlreadyFaded = true;
            //    var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
            //    anim.FillBehavior = FillBehavior.Stop;
            //    anim.Completed += new EventHandler(minimizeCompleted);
            //    this.BeginAnimation(UIElement.OpacityProperty, anim);
            //}


            var hide = FindResource("HideWindow") as Storyboard;
            if (hide != null)
            {
                Double CurrentHeight = Height;
                Double CurrentWidth = Width;

                hide.Completed += (s, ea) => {

                    WindowState = WindowState.Minimized;

                    Application.Current.MainWindow.Height = CurrentHeight;
                    Application.Current.MainWindow.Width = CurrentWidth;

                    };

                hide.Begin(this, true);
            }
        }

        void anim_Completed(object sender, EventArgs e)
        {
            Close();
        }
        void minimizeCompleted(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            Opacity = 1;
            AlreadyFaded = false;
        }
        void minimizeReset(object sender, EventArgs e)
        {
            Opacity = 1;
            AlreadyFaded = false;
        }



        //protected override void OnActivated(EventArgs e)
        //{
        //    base.OnActivated(e);
        //    if (WindowStyle != WindowStyle.None)
        //    {
        //        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (DispatcherOperationCallback)delegate (object unused)
        //        {
        //            WindowStyle = WindowStyle.None;
        //            return null;
        //        }
        //        , null);
        //    }
        //}
    }
}
