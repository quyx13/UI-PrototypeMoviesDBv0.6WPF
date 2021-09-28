﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using UI_PrototypeMoviesDBv0._6WPF.Model;

namespace UI_PrototypeMoviesDBv0._6WPF
{
    class Controller
    {
        private View.MainWindow _mainWindow;
        private Stopwatch _timer = new Stopwatch();
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
            _worker.WorkerDone += OnWorkerDone;
        }

        public void MenuExit_Click()
        {
            Application.Current.Shutdown();
        }

        public void BtnStart_Click()
        {
            if (_worker.GetState() == WorkerState.ready)
            {
                _timer.Start();
                _worker.SetState(WorkerState.running);
                _mainWindow.SetStateRunning();
                Trace.WriteLine("started...");
                Task work = Task.Factory.StartNew(() => _worker.DoWork());
            }
        }

        public void BtnStop_Click()
        {
            if (_worker.GetState() == WorkerState.running)
            {
                _timer.Stop();
                _worker.SetState(WorkerState.stopped);
                _mainWindow.SetStateDone();
                Trace.WriteLine("...stopped...");
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
            _mainWindow.SetStateDone();
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