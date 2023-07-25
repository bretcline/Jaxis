using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

namespace Jaxis.BeverageManagement.Plugin
{
    public class BeverageManagementAPI : IBeverageManagementAPI
    {
        public BeverageManagementAPI()
        {
            
        }

        public List<PourInformation> GetPourInformation(DateTime _startTime, DateTime _endTime)
        {
            var rc = new List<PourInformation>();

            var db = new BeverageMonitorDB();

            var data = db.GetPourData(_startTime, _endTime);
            if (null != data)
            {
                using (var reader = data.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pour = new PourInformation();
                        pour.BatteryVoltage = reader.GetDouble(reader.GetOrdinal("BatteryVoltage"));
                        pour.DeviceName = reader.GetString(reader.GetOrdinal("DeviceName"));
                        pour.PourAmount = reader.GetDouble(reader.GetOrdinal("Volume"));
                        pour.PourTime = reader.GetDateTime(reader.GetOrdinal("PourTime"));
                        pour.TagNumber = reader.GetString(reader.GetOrdinal("TagNumber"));
                        pour.Temperature = reader.GetDouble(reader.GetOrdinal("Temperature"));
                        pour.AmountLeft = reader.GetDouble(reader.GetOrdinal("AmountLeft"));
                        pour.LocationName = reader.GetString(reader.GetOrdinal("LocationName"));
                        pour.UPCName = reader.GetString(reader.GetOrdinal("UPCName"));
                        pour.UPCSize = reader.GetInt32(reader.GetOrdinal("UPCSize"));
                        var position = reader.GetOrdinal("RootCategory");
                        if( !reader.IsDBNull( position ) )
                        {
                            pour.RootCategory = reader.GetString( position );
                        }
                        pour.Category = reader.GetString(reader.GetOrdinal("Category"));

                        rc.Add(pour);
                    }
                }
            }
            return rc;
        }


        public bool BrandBottle(string _tagNumber, string _upc, Guid _nozzle )
        {
            return Log.Wrap( "BevManAPI::BrandBottle",LogType.Debug, false, () =>
            {
                var nozzleMan = BLManagerFactory.Get().ManageStandardNozzles();
                var nozzle = nozzleMan.Get(_nozzle) ?? BLManagerFactory.Get().GetDefaultNozzle();
                var tagMan = BLManagerFactory.Get( ).ManageTags( );
                //return tagMan.BrandBottle(_tagNumber, _upc, nozzle);
                ///TODO: Uncomment this...
                return true;
            });
        }
    }
}
