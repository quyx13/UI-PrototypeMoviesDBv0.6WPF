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
        public event EventHandler PrimeFound;

        public void DoWork()
        {
            System.Diagnostics.Trace.WriteLine("started...");

            for (; _counter < _total;)
            {
                #region work
                _counter++;
                Thread.Sleep(1);

                CounterChanged?.Invoke(this, EventArgs.Empty);

                if (IsPrime())
                    PrimeFound?.Invoke(this, EventArgs.Empty);
                #endregion
            }

            System.Diagnostics.Trace.WriteLine("...done");
        }

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

        private bool IsPrime()
        {
            if (_counter <= 1) return false;
            if (_counter == 2) return true;
            if (_counter % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(_counter));

            for (int i = 3; i <= boundary; i += 2)
                if (_counter % i == 0)
                    return false;

            return true;
        }
    }
}