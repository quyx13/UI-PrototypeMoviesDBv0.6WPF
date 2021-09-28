using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace UI_PrototypeMoviesDBv0._6WPF.View
{
    public partial class MainWindow : Window
    {
        private Controller _controller;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private Stopwatch _timer = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();

            _controller = new Controller(this);
        }

        public DispatcherTimer GetDispatcherTimer()
        {
            return _dispatcherTimer;
        }

        public Stopwatch GetTimer()
        {
            return _timer;
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

        public void UpdateWindowTitle(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this.Title = text;
            }), DispatcherPriority.Background);
        }

        public void UpdateBtnStart(bool isEnabled)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnStart.IsEnabled = isEnabled;
            }), DispatcherPriority.Background);
        }

        public void UpdateBtnStartImg(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnStartImg.Source = new BitmapImage(new Uri(text, UriKind.Relative));
            }), DispatcherPriority.Background);
        }

        public void UpdateBtnStartTxt(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnStartTxt.Text = text;
            }), DispatcherPriority.Background);
        }

        public void UpdateBtnStop(bool isEnabled)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnStop.IsEnabled = isEnabled;
            }), DispatcherPriority.Background);
        }

        public void UpdateBtnStopImg(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnStopImg.Source = new BitmapImage(new Uri(text, UriKind.Relative));
            }), DispatcherPriority.Background);
        }

        public void UpdateBtnStopTxt(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnStopTxt.Text = text;
            }), DispatcherPriority.Background);
        }

        public void UpdateBtnSettings(bool isEnabled)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnSettings.IsEnabled = isEnabled;
            }), DispatcherPriority.Background);
        }

        public void UpdateBtnSettingsImg(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnSettingsImg.Source = new BitmapImage(new Uri(text, UriKind.Relative));
            }), DispatcherPriority.Background);
        }

        public void UpdateBtnSettingsTxt(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnSettingsTxt.Text = text;
            }), DispatcherPriority.Background);
        }

        public void UpdateTextBoxText(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                textBox.Text = text;
            }), DispatcherPriority.Background);
        }

        public void UpdateTextBox(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                textBox.AppendText(text + Environment.NewLine);
            }), DispatcherPriority.Background);
        }

        public void ScrollToEnd()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                textBox.ScrollToEnd();
            }), DispatcherPriority.Background);
        }

        //public void UpdateStatusTextTime(string text)
        //{
        //    Dispatcher.Invoke(new Action(() =>
        //    {
        //        statusTextTime.Text = text;
        //    }), DispatcherPriority.Background);
        //}

        public void UpdateStatusTextTask(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                statusTextTask.Text = text;
            }), DispatcherPriority.Background);
        }

        public void SetupStatusProgressBar(int min, int max, int value)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                statusProgressBar.Minimum = min;
                statusProgressBar.Maximum = max;
                statusProgressBar.Value = value;
            }), DispatcherPriority.Background);
        }

        public void UpdateStatusProgressBar(int value)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                statusProgressBar.Value = value;
            }), DispatcherPriority.Background);
        }

        public void UpdateStatusTextPercentage(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                statusTextPercentage.Text = text;
            }), DispatcherPriority.Background);
        }

        public void UpdateStatusTextInfo(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                statusTextInfo.Text = text;
            }), DispatcherPriority.Background);
        }

        public void SetStateReady()
        {
            _timer.Reset();
        }

        public void SetStateRun()
        {

        }

        public void SetStateStop()
        {

        }

        public void SetStateDone()
        {

        }
    }
}