using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using UI_PrototypeMoviesDBv0._6WPF.Model;

namespace UI_PrototypeMoviesDBv0._6WPF
{
    class Controller
    {
        private static readonly int setupTotal = 5200;
        private static readonly int setupWait = 1;

        private View.MainWindow _mainWindow;
        private Stopwatch _timer = new Stopwatch();
        private Worker _worker;

        private Dictionary<string, List<string>> log = new Dictionary<string, List<string>>();
        private List<int> _updates = new List<int>();

        public Controller(View.MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _mainWindow.SetState(WorkerState.ready);

            mainWindow.GetDispatcherTimer().Interval = TimeSpan.FromMilliseconds(100);
            mainWindow.GetDispatcherTimer().Tick += timer_Tick;
            mainWindow.GetDispatcherTimer().Start();

            SetupWorker();
        }

        private void SetupWorker()
        {
            _worker = new Worker();
            _worker.SetTotal(setupTotal);
            _worker.SetWait(setupWait);

            _worker.CounterChanged += OnCounterChanged;
            _worker.WorkerDone += OnWorkerDone;

            _worker.WorkerDone += OnAllCounter;
            _worker.WorkerDone += OnEvenCounter;
            _worker.WorkerDone += OnUnevenCounter;
            _worker.WorkerDone += OnModulo13Counter;
            _worker.WorkerDone += OnModulo100Counter;
            _worker.WorkerDone += OnPrimeCounter;
        }

        #region React on commands
        public void MenuExit_Click()
        {
            Application.Current.Shutdown();
        }

        public void BtnStart_Click()
        {
            switch (_worker.GetState())
            {
                case WorkerState.ready:
                    _timer.Start();
                    _worker.SetState(WorkerState.running);
                    _mainWindow.SetState(WorkerState.running);
                    _mainWindow.SetupStatusProgressBar(0, setupTotal, 0);
                    Trace.WriteLine("started...");
                    Task work = Task.Factory.StartNew(() => _worker.DoWork());
                    break;
                case WorkerState.stopped:
                    _timer.Start();
                    _worker.SetState(WorkerState.running);
                    _mainWindow.SetState(WorkerState.running);
                    Trace.WriteLine("...continued...");
                    break;
            }
        }

        public void BtnStop_Click()
        {
            switch (_worker.GetState())
            {
                case WorkerState.running:
                    _timer.Stop();
                    _worker.SetState(WorkerState.stopped);
                    _mainWindow.SetState(WorkerState.stopped);
                    Trace.WriteLine("...stopped...");
                    break;
                case WorkerState.done:
                    goto case WorkerState.stopped;
                case WorkerState.stopped:
                    _worker.SetState(WorkerState.abort);
                    SetupWorker();
                    _timer.Reset();
                    _worker.SetState(WorkerState.ready);
                    _mainWindow.SetState(WorkerState.ready);
                    _mainWindow.SetupStatusProgressBar(0, 1, 0);
                    _updates.Clear();
                    Trace.WriteLine("reset");
                    break;
            }
        }
        #endregion

        #region React on events
        public void OnCounterChanged(object sender, EventArgs e)
        {
            _updates.Add(_worker.GetCounter());
        }

        public void OnWorkerDone(object sender, EventArgs e)
        {
            _timer.Stop();
            _worker.SetState(WorkerState.done);
            _mainWindow.SetState(WorkerState.done);
            Trace.WriteLine("...done");

            foreach (var key in log.Keys)
                File.WriteAllLines("", log[key]);
        }

        public void OnAllCounter(object sender, EventArgs e)
        {
            if (!log.ContainsKey("All"))
                log.Add("All", new List<string>());
            log["All"].Add(_worker.GetCounter().ToString());
        }

        public void OnEvenCounter(object sender, EventArgs e)
        {
            if (!log.ContainsKey("Even"))
                log.Add("Even", new List<string>());
            log["Even"].Add(_worker.GetCounter().ToString());
        }

        public void OnUnevenCounter(object sender, EventArgs e)
        {
            if (!log.ContainsKey("Uneven"))
                log.Add("Uneven", new List<string>());
            log["Uneven"].Add(_worker.GetCounter().ToString());
        }

        public void OnModulo13Counter(object sender, EventArgs e)
        {
            if (!log.ContainsKey("Modulo13"))
                log.Add("Modulo13", new List<string>());
            log["Modulo13"].Add(_worker.GetCounter().ToString());
        }

        public void OnModulo100Counter(object sender, EventArgs e)
        {
            if (!log.ContainsKey("Modulo100"))
                log.Add("Modulo100", new List<string>());
            log["Modulo100"].Add(_worker.GetCounter().ToString());
        }

        public void OnPrimeCounter(object sender, EventArgs e)
        {
            if (!log.ContainsKey("Prime"))
                log.Add("Prime", new List<string>());
            log["Prime"].Add(_worker.GetCounter().ToString());
        }
        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            _mainWindow.UpdateWindowTitle($"UI-PrototypeMoviesDBv0.6WPF   [{DateTime.Now.ToString("HH:mm:ss")}]   [Updates/Tick:{_updates.Count}]");
            _mainWindow.UpdateStatusTextElapsed($"{_timer.Elapsed.Hours:D2}h:{_timer.Elapsed.Minutes:D2}m:{_timer.Elapsed.Seconds:D2}s");

            if (_updates.Count > 0)
            {
                TimeSpan timeRemaing = TimeSpan.FromMilliseconds(0);
                try
                {
                    if (_timer.Elapsed.TotalMilliseconds > 0 && _updates[_updates.Count - 1] > 0)
                    {
                        timeRemaing = TimeSpan.FromMilliseconds((setupTotal - _updates[_updates.Count - 1]) * (_timer.Elapsed.TotalMilliseconds / _updates[_updates.Count - 1]));
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
                _mainWindow.UpdateStatusTextRemaining($"(remaining: {timeRemaing.Hours:D2}h:{timeRemaing.Minutes:D2}m:{timeRemaing.Seconds:D2}s)");

                _mainWindow.UpdateStatusTextTask($"{string.Format("{0:0,0}", _updates[_updates.Count - 1])} of {string.Format("{0:0,0}", setupTotal)}");
                _mainWindow.UpdateStatusProgressBar(_updates[_updates.Count - 1]);
                _mainWindow.UpdateStatusTextPercentage($"{((_updates[_updates.Count - 1]) / (double)setupTotal * 100):F2}%");

                _updates.Clear();
            }
        }
    }
}