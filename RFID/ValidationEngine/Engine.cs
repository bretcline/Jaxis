using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Utilities.Database;
using System.Data;
using System.Data.SqlServerCe;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using ValidationEngine.Readers;

namespace ValidationEngine
{
    public class Engine: IEngine
    {
        protected Thread m_Processor = null;
        protected Validator m_Validator = new Validator( );

        public Action<IValidationResults> DefaultProcessValidation { get; set; }

        public Engine( Action<IValidationResults> _Processor ) : this( )
        {
            DefaultProcessValidation = _Processor;
        }

        public Engine( )
        {
            ThreadPool.SetMaxThreads( 10, 10 );

            StartReaders( );
        }
        #region IEngine Members

        public void SubmitForProcessing( IValidationKey _Key )
        {
            if( null == _Key.ProcessValidation )
            {
                _Key.ProcessValidation = DefaultProcessValidation;
            }
            ThreadPool.QueueUserWorkItem( Validate, _Key );
        }
        #endregion

        protected void Validate( Object _Key )
        {
            m_Validator.Validate( _Key as IValidationKey );
        }

        protected void StartReaders( )
        {
            AlienConfig Config = new AlienConfig { TcpPort = 4000, UdpPort = 3988, TimeoutInterval = 20 };

            AlienReader Reader = new AlienReader( Config ) { SubmitForProcessing = SubmitForProcessing };
            Reader.Start( );
        }
    }
}