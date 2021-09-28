using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using UI_PrototypeMoviesDBv0._6WPF.Model;

namespace UI_PrototypeMoviesDBv0._6WPF
{
    class Controller
    {
        private View.MainWindow _mainWindow;
        private Worker _worker;

        private List<int> _updates = new List<int>();

        public Controller(View.MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _mainWindow.SetStateReady();

            mainWindow.GetDispatcherTimer().Interval = TimeSpan.FromMilliseconds(100);
            mainWindow.GetDispatcherTimer().Tick += timer_Tick;
            mainWindow.GetDispatcherTimer().Start();

            _worker = new Worker();
            _worker.SetTotal(5200);
            _worker.SetWait(1);

            _worker.CounterChanged += OnCounterChanged;
        }

        public void MenuExit_Click()
        {
            Application.Current.Shutdown();
        }

        public void BtnStart_Click()
        {
            Task work = Task.Factory.StartNew(() => _worker.DoWork());
        }

        public void OnCounterChanged(object sender, EventArgs e)
        {
            _updates.Add(_worker.GetCounter());
        }

        public void OnPrimeFound(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine($"{_worker.GetCounter()}");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //_mainWindow.UpdateWindowTitle($"UI-PrototypeMoviesDBv0.6WPF [{DateTime.Now.ToString("HH:mm:ss")}]");
            _mainWindow.UpdateWindowTitle($"UI-PrototypeMoviesDBv0.6WPF [{_updates.Count}]");

            _mainWindow.UpdateStatusTextElapsed($"{_mainWindow.GetTimer().Elapsed.Hours:D2}h:" +
                $"{_mainWindow.GetTimer().Elapsed.Minutes:D2}m:{_mainWindow.GetTimer().Elapsed.Seconds:D2}s");
        }
    }
}