using System;
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

        public event EventHandler OnAbort;
        public event EventHandler OnAll;
        public event EventHandler OnEven;
        public event EventHandler OnUneven;
        public event EventHandler OnModulo13;
        public event EventHandler OnModulo100;
        public event EventHandler OnPrime;

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
            for (; _counter < _total;)
            {
                if (_workerState == WorkerState.running)
                {
                    #region work
                    _counter++;
                    Thread.Sleep(_wait);

                    CounterChanged?.Invoke(this, EventArgs.Empty);
                    OnAll?.Invoke(this, EventArgs.Empty);

                    if (_counter % 2 == 0)
                        OnEven?.Invoke(this, EventArgs.Empty);
                    else
                        OnUneven?.Invoke(this, EventArgs.Empty);

                    if (_counter % 13 == 0)
                        OnModulo13?.Invoke(this, EventArgs.Empty);

                    if (_counter % 100 == 0)
                        OnModulo100?.Invoke(this, EventArgs.Empty);

                    if (IsPrime(_counter))
                        OnPrime?.Invoke(this, EventArgs.Empty);
                    #endregion
                }
                if (_workerState == WorkerState.abort)
                {
                    OnAbort?.Invoke(this, EventArgs.Empty);// TODO:Log
                    return;
                }
            }

            WorkerDone?.Invoke(this, EventArgs.Empty);
        }

        private static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}