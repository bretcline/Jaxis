namespace LogViewer
{
    class LogEntry
    {
        public LogEntry(string _fileName, string _dateTimeText, string _typeText, string _threadText, string _messageText)
        {
            FileName = _fileName;
            DateTime = _dateTimeText;
            Type = _typeText;
            Thread = _threadText;
            Message = _messageText;
        }

        public string FileName { get; private set; }
        public string DateTime { get; private set; }
        public string Type { get; private set; }
        public string Thread { get; private set; }
        public string Message { get; private set; }
    }
}
