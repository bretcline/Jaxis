namespace Jaxis.Readers.Trish
{
    public interface ICommunicationManager
    {
        string Data { get; set; }
        bool Open();
        bool Close();
        void WriteData( string _data );
    }
}