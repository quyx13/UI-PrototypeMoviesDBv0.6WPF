using System;
using System.Collections.Generic;

namespace UI_PrototypeMoviesDBv0._6WPF.Model
{
    public enum WorkerState
    {
        ready,
        running,
        stopped,
        done,
        abort
    }

    public class ListEventArgs : EventArgs
    {
        public List<string> Data { get; set; }
        public ListEventArgs(List<string> data)
        {
            Data = data;
        }
    }
}