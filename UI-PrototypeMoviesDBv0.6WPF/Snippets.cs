namespace UI_PrototypeMoviesDBv0._6WPF
{
    class Snippets
    {
        // in Support:
        //public class EventArgsList : EventArgs
        //{
        //    public List<string> Data { get; set; }
        //    public EventArgsList(List<string> data)
        //    {
        //        Data = data;
        //    }
        //}

        // in Worker:
        //public event EventHandler<EventArgsList> OnLog;
        //...
        //OnLog?.Invoke(this, new EventArgsList(new List<string>() { "DoWork() gerade gestartet" }));

        // in Controller:
        //_worker.OnLog += OnLog;
        //...
        //public void OnLog(object sender, EventArgsList e)
        //{
        //    foreach (string s in e.Data)
        //    {
        //        Trace.WriteLine(s);
        //    }
        //}
    }
}