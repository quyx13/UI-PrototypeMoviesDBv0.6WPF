using System;
using System.Windows;
using System.Windows.Threading;

namespace UI_PrototypeMoviesDBv0._6WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menuInfo_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            
        }
    }
}