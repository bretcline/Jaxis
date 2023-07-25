using Jaxis.Interfaces;

namespace SQLWatcher
{
    public class SQLWatcherAlert : BaseMessage, IAlertMessage
    {
        public AlertTypes AlertType{get; set;}

        public string AlertMessage { get; set; }
    }
}