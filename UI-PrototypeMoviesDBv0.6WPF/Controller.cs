using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private List<int> _updates = new List<int>();

        public Controller(View.MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _mainWindow.SetState(WorkerState.ready);

            mainWindow.GetDispatcherTimer().Interval = TimeSpan.FromMilliseconds(100);
            mainWindow.GetDispatcherTimer().Tick += timer_Tick;
            mainWindow.GetDispatcherTimer().Start();

            _worker = new Worker();
            _worker.SetTotal(setupTotal);
            _worker.SetWait(setupWait);

            _worker.CounterChanged += OnCounterChanged;
            _worker.WorkerDone += OnWorkerDone;
        }

        public void MenuExit_Click()
        {
            Application.Current.Shutdown();
        }

        public void BtnStart_Click()
        {
            Trace.WriteLine($"workerState:{_worker.GetState()}");
            switch (_worker.GetState())
            {
                case WorkerState.ready:
                    _timer.Start();
                    _worker.SetState(WorkerState.running);
                    _mainWindow.SetState(WorkerState.running);
                    Trace.WriteLine("started...");
                    Task work = Task.Factory.StartNew(() => _worker.DoWork());
                    break;
                case WorkerState.stopped:
                    _timer.Start();
                    _worker.SetState(WorkerState.running);
                    _mainWindow.SetState(WorkerState.running);
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
                    _worker = new Worker();
                    _worker.SetTotal(setupTotal);
                    _worker.SetWait(setupWait);
                    goto case WorkerState.stopped;
                case WorkerState.stopped:
                    _timer.Reset();
                    _worker.SetState(WorkerState.ready);
                    _mainWindow.SetState(WorkerState.ready);
                    _updates.Clear();
                    Trace.WriteLine("reset");
                    break;
            }
        }

        public void BtnSettings_Click()
        {
            switch (_mainWindow.btnStartTxt.Text)
            {
                case "Start":
                    _mainWindow.UpdateBtnStartTxt("Resume");
                    break;
                case "Resume":
                    _mainWindow.UpdateBtnStartTxt("Start");
                    break;
            }
        }

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
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //_mainWindow.UpdateWindowTitle($"UI-PrototypeMoviesDBv0.6WPF [{DateTime.Now.ToString("HH:mm:ss")}]");
            _mainWindow.UpdateWindowTitle($"UI-PrototypeMoviesDBv0.6WPF [{_updates.Count}]");

            _mainWindow.UpdateStatusTextElapsed($"{_timer.Elapsed.Hours:D2}h:{_timer.Elapsed.Minutes:D2}m:{_timer.Elapsed.Seconds:D2}s");
        }
    }
}