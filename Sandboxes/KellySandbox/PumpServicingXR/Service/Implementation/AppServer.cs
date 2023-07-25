using LFI.Sync.DataManager;

namespace WFT.PSService.Service
{
    public partial class AppServer
    {
        private DataManager dataMgr;
        public static PersistenceManager PersistenceMgr { get; set; }
        protected static RecordProcessor m_recordProcessor;

        public AppServer( )
        {
            if( PersistenceMgr == null )
            {
                // CAUTION: In order to share memory with the InfoServer, the PersistenceManager should
                // be initialized in the ServiceHost and passed off to the the PersistenceMgr property
                PersistenceMgr = new PersistenceManager( );
                PersistenceMgr.Init( );
            }

            dataMgr = PersistenceMgr.GetWSDataManager( );

            m_recordProcessor = new RecordProcessor( PersistenceMgr );
        }
    }
}
