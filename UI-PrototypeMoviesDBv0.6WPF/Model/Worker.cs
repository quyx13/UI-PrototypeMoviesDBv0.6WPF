﻿using System;
using System.Threading;

namespace UI_PrototypeMoviesDBv0._6WPF.Model
{
    class Worker
    {
        private int _counter = 0;
        private int _total = 0;
        private int _wait = 0;
        private WorkerState _workerState = WorkerState.ready;

        public event EventHandler CounterChanged;
        public event EventHandler WorkerDone;

        #region Getter and setter
        public int GetCounter()
        {
            return _counter;
        }

        public void SetTotal(int total)
        {
            this._total = total;
        }

        public void SetWait(int wait)
        {
            this._wait = wait;
        }

        public WorkerState GetState()
        {
            return _workerState;
        }

        public void SetState(WorkerState workerState)
        {
            _workerState = workerState;
        }
        #endregion

        public void DoWork()
        {
            System.Diagnostics.Trace.WriteLine("started...");

            for (; _counter < _total;)
            {
                if (_workerState == WorkerState.running)
                {
                    #region work
                    _counter++;
                    Thread.Sleep(_wait);

                    CounterChanged?.Invoke(this, EventArgs.Empty);
                    #endregion
                }
                else
                {
                    return;
                }
            }

            WorkerDone?.Invoke(this, EventArgs.Empty);
        }
    }
}