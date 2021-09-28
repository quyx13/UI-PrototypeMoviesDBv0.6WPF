using System;
using System.Threading.Tasks;
using System.Windows;
using UI_PrototypeMoviesDBv0._6WPF.Model;

namespace UI_PrototypeMoviesDBv0._6WPF
{
    public class Controller
    {
        private View.MainWindow _mainWindow;
        private Worker _worker;

        public Controller(View.MainWindow mainWindow)
        {
            this._mainWindow = mainWindow;

            mainWindow.GetDispatcherTimer().Interval = TimeSpan.FromMilliseconds(100);
            mainWindow.GetDispatcherTimer().Tick += timer_Tick;
            mainWindow.GetDispatcherTimer().Start();
        }

        public void MenuExit_Click()
        {
            Application.Current.Shutdown();
        }

        public void BtnStart_Click()
        {
            _worker = new Worker();
            _worker.CounterChanged += OnCounterChanged;
            _worker.PrimeFound += OnPrimeFound;

            _worker.SetTotal(5200);
            _worker.SetWait(0);
            Task work = Task.Factory.StartNew(() => _worker.DoWork());
        }

        public void OnCounterChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine($"{_worker.GetCounter()}");
        }

        public void OnPrimeFound(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine($"{_worker.GetCounter()} is prime!");
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }
    }
}