using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DataProvider
{
    [ServiceContract]
    public interface IDataProvider
    {
        [OperationContract]
        string TestMethod( );
    }

    public class DataProvider : IDataProvider
    {
        public string TestMethod( )
        {
            return ( "This is a test message" );
        }
    }
}