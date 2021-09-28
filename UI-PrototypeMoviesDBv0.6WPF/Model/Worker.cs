using System;
using System.Threading;

namespace UI_PrototypeMoviesDBv0._6WPF.Model
{
    class Worker
    {
        private int _counter = 0;
        private int _total = 0;
        private int _wait = 0;

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
        #endregion

        public void DoWork()
        {
            System.Diagnostics.Trace.WriteLine("started...");

            for (; _counter < _total;)
            {
                #region work
                _counter++;
                Thread.Sleep(_wait);

                CounterChanged?.Invoke(this, EventArgs.Empty);
                #endregion
            }

            WorkerDone?.Invoke(this, EventArgs.Empty);
        }
    }
}