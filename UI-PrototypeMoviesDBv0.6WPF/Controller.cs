using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private Dictionary<string, List<string>> _logs = new Dictionary<string, List<string>>() { { "Output", new List<string>() } };
        private Dictionary<string, int> _lastIndex = new Dictionary<string, int>() { { "Output", 0 } };

        public Controller(View.MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _mainWindow.SetState(WorkerState.ready);

            mainWindow.GetDispatcherTimer().Interval = TimeSpan.FromMilliseconds(40);
            mainWindow.GetDispatcherTimer().Tick += timer_Tick;
            mainWindow.GetDispatcherTimer().Start();

            SetupWorker();
        }

        private void SetupWorker()
        {
            _worker = new Worker();
            _worker.SetTotal(setupTotal);
            _worker.SetWait(setupWait);

            _worker.OnWorkStep += OnWorkStep;
            _worker.OnWorkDone += OnWorkDone;
            _worker.OnWorkAbort += OnWorkAbort;

            _worker.OnLog += OnLog;
            _worker.OnCatLog += OnCatLog;
        }

        #region Commands
        public void MenuExit_Click()
        {
            Application.Current.Shutdown();
        }

        public void MenuInfo_Click()
        {

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
                    Log("started...");
                    Task work = Task.Factory.StartNew(() => _worker.DoWork());
                    break;
                case WorkerState.stopped:
                    _timer.Start();
                    _worker.SetState(WorkerState.running);
                    _mainWindow.SetState(WorkerState.running);
                    Log("...continued...");
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
                    Log("...stopped...");
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
                    _mainWindow.ClearComboBoxItems();
                    _updates.Clear();
                    Log("reset");
                    SaveLogToFile();
                    _mainWindow.ClearTextBox();
                    break;
            }
        }

        public void BtnSettings_Click()
        {

        }

        public void ComboBox_SelectionChanged()
        {

        }
        #endregion

        #region Work events
        public void OnWorkStep(object sender, EventArgs e)
        {
            _updates.Add(_worker.GetCounter());
        }

        public void OnWorkDone(object sender, EventArgs e)
        {
            _timer.Stop();
            _worker.SetState(WorkerState.done);
            _mainWindow.SetState(WorkerState.done);
            Log("...done");
        }

        public void OnWorkAbort(object sender, EventArgs e)
        {
            Log("...aborting...");
        }

        public void OnLog(string text)
        {
            Log(text);
        }

        public void OnCatLog(string category, string text)
        {
            Log(category, text);
        }
        #endregion

        #region Log
        private void ClearLog()
        {
            _logs = new Dictionary<string, List<string>>() { { "Output", new List<string>() } };
            _lastIndex = new Dictionary<string, int>() { { "Output", 0 } };
        }

        private void Log(string text)
        {
            Log("Output", text);
        }

        private void Log(string category, string text)
        {
            if (!_logs.ContainsKey(category))
            {
                _logs.Add(category, new List<string>());
            }

            _logs[category].Add(text);
        }

        private void SaveLogToFile()
        {
            foreach (string key in _logs.Keys)
            {
                Trace.WriteLine($"{key}:\t{_logs[key].Count}");
                File.WriteAllLines($@"C:\Users\Anwender\Downloads\_{key}.log", _logs[key]);
            }

            ClearLog();
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
                    Log(ex.ToString());
                }
                _mainWindow.UpdateStatusTextRemaining($"(remaining: {timeRemaing.Hours:D2}h:{timeRemaing.Minutes:D2}m:{timeRemaing.Seconds:D2}s)");

                _mainWindow.UpdateStatusTextTask($"{string.Format("{0:0,0}", _updates[_updates.Count - 1])} of {string.Format("{0:0,0}", setupTotal)}");
                _mainWindow.UpdateStatusProgressBar(_updates[_updates.Count - 1]);
                _mainWindow.UpdateStatusTextPercentage($"{((_updates[_updates.Count - 1]) / (double)setupTotal * 100):F2}%");

                _updates.Clear();
            }

            ShowLog("Output");
        }

        private void ShowLog(string category)
        {
            if (_logs[category].Count > 0)
            {
                string[] logUpdates = _logs[category].GetRange(_lastIndex[category], _logs[category].Count - _lastIndex[category]).ToArray();
                _lastIndex[category] += _logs[category].Count;

                foreach (string logUpdate in logUpdates)
                {
                    _mainWindow.UpdateTextBox(logUpdate);
                }
                _mainWindow.ScrollToEnd();
            }
        }
    }
}