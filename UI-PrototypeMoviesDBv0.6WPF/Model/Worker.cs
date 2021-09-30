using System;
using System.Threading;

namespace UI_PrototypeMoviesDBv0._6WPF.Model
{
    internal class Worker
    {
        #region Initialization
        private int _counter = 0;
        private int _total = 0;
        private int _wait = 0;
        private WorkerState _workerState = WorkerState.ready;

        public event EventHandler OnWorkStep;
        public event EventHandler OnWorkDone;
        public event EventHandler OnWorkAbort;

        public event Action<string> OnLog;
        public event Action<string, string> OnCatLog;
        #endregion

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
            OnLog?.Invoke("DoWork() gerade gestartet");

            for (; _counter < _total;)
            {
                if (_workerState == WorkerState.running)
                {
                    #region Work
                    _counter++;
                    Thread.Sleep(_wait);

                    OnCatLog?.Invoke("Step", _counter.ToString());
                    if (_counter % 100 == 0)
                    {
                        OnCatLog?.Invoke("Modulo100", _counter.ToString());
                    }

                    if (_counter % 13 == 0)
                    {
                        OnCatLog?.Invoke("Modulo13", _counter.ToString());
                    }

                    if (_counter % 2 == 0)
                    {
                        OnCatLog?.Invoke("Even", _counter.ToString());
                    }
                    else
                    {
                        OnCatLog?.Invoke("Odd", _counter.ToString());
                    }

                    OnWorkStep?.Invoke(this, EventArgs.Empty);
                    #endregion
                }
                if (_workerState == WorkerState.abort)
                {
                    OnWorkAbort?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

            OnWorkDone?.Invoke(this, EventArgs.Empty);
        }
    }
}