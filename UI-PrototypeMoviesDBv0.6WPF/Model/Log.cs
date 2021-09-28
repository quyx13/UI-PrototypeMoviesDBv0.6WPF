using System.Collections.Generic;

namespace UI_PrototypeMoviesDBv0._6WPF.Model
{
    class Log
    {
        private Dictionary<string, object> _log = new Dictionary<string, object>();

        public Dictionary<string, object> GetLog()
        {
            return _log;
        }

        public void AddLog(string s, object o)
        {
            _log.Add(s, o);
        }
    }
}