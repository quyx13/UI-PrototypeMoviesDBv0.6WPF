using System;
using System.Threading;

namespace UI_PrototypeMoviesDBv0._6WPF.Model
{
    class Worker
    {
        private int _counter = 0;
        private int _total = 0;

        public event EventHandler CounterChanged;

        public void DoWork()
        {
            for (int i = 0; i < _total;)
            {
                #region work
                i++;
                Thread.Sleep(1);

                CounterChanged?.Invoke(this, EventArgs.Empty);
                #endregion
            }

            System.Diagnostics.Trace.WriteLine("...done");
        }

        public void SetTotal(int total)
        {
            this._total = total;
        }

        public int GetCounter()
        {
            return _counter;
        }
    }
}