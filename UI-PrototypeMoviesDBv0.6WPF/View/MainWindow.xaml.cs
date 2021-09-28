using System.Windows;
using System.Windows.Threading;

namespace UI_PrototypeMoviesDBv0._6WPF.View
{
    public partial class MainWindow : Window
    {
        private Controller _controller;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            _controller = new Controller(this);
        }

        public DispatcherTimer GetDispatcherTimer()
        {
            return _dispatcherTimer;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            _controller.MenuExit_Click();
        }

        private void menuInfo_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _controller.BtnStart_Click();
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
    }
}