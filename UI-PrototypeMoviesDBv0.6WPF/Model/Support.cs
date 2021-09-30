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

    public class EventArgsList : EventArgs
    {
        public List<string> Data { get; set; }
        public EventArgsList(List<string> data)
        {
            Data = data;
        }
    }

    public class EventArgsString : EventArgs
    {
        public string Data { get; set; }
        public EventArgsString(string data)
        {
            Data = data;
        }
    }
}