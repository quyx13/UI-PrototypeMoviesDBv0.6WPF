﻿using System;
using System.Threading.Tasks;
using System.Windows;
using UI_PrototypeMoviesDBv0._6WPF.Model;

namespace UI_PrototypeMoviesDBv0._6WPF
{
    public class Controller
    {
        private View.MainWindow mainWindow;
        private Worker worker = new Worker();

        public Controller(View.MainWindow mainWindow)
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
            Task work = Task.Factory.StartNew(() => worker.DoWork(5200));
        }
    }
}