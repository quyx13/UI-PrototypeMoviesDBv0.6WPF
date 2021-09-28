using System;
using System.Threading.Tasks;
using System.Windows;

namespace UI_PrototypeMoviesDBv0._6WPF
{
    public class Controller
    {
        private MainWindow mainWindow;

        public Controller(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            mainWindow.GetDispatcherTimer().Interval = TimeSpan.FromMilliseconds(100);
            mainWindow.GetDispatcherTimer().Tick += timer_Tick;
            mainWindow.GetDispatcherTimer().Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }

        public void MenuExit_Click()
        {
            Application.Current.Shutdown();
        }

        public void BtnStart_Click()
        {
            Task work = Task.Factory.StartNew(() => Worker.DoWork(5200));
        }
    }
}