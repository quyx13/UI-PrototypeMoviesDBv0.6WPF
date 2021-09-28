using System.Threading;

namespace UI_PrototypeMoviesDBv0._6WPF.Model
{
    class Worker
    {
        public void DoWork(int number)
        {
            for (int i = 0; i < number;)
            {
                #region work
                i++;
                Thread.Sleep(1);
                #endregion
            }

            System.Diagnostics.Trace.WriteLine("...done");
        }
    }
}