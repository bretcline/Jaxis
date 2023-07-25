using System;
using System.Collections.Generic;
using System.ServiceModel;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;

namespace EngineConfigWCF
{
    [ServiceContract]
    public interface IEngineConfigService
    {
        [OperationContract]
        List<DeviceConfig> GetDeviceList( );

        [OperationContract]
        DeviceState GetDeviceState( DeviceConfig Device );

        [OperationContract]
        void StartDevice( DeviceConfig Device );

        [OperationContract]
        void StopDevice( DeviceConfig Device );

        [OperationContract]
        string GetEventLog( );

        [OperationContract]
        EngineState GetEngineState( );

        [OperationContract]
        void StopEngine( );

        [OperationContract]
        void StartEngine( );

        [OperationContract]
        string GetEngineRev( );

        [OperationContract]
        List<DeviceConfig> GetLocalDevices();

        [OperationContract]
        void AddDevice(DeviceConfig _Config);

        [OperationContract]
        List<FilterConfig> GetLocalFilters();

        [OperationContract]
        void AddFilter(DeviceConfig _Device, FilterConfig _Config);

        [OperationContract]
        string DownloadDevicePlugin( DeviceConfig _Config, byte[] Device, List<string> DependentNames, List<byte[]> Dependents, byte[] _iv );

        [OperationContract]
        string DownloadFilterPlugin( DeviceConfig _Device, FilterConfig _Config, byte[] Device, byte[] _iv );

        [OperationContract]
        void UpdateOptions( DeviceConfig _Config );

        [OperationContract]
        void UpdateFilterOptions( DeviceConfig _Config, FilterConfig _FilterConfig );

        [OperationContract]
        void UnLoadDevice( DeviceConfig _Config );

        [OperationContract]
        void UnLoadFilter( DeviceConfig _Config, FilterConfig _FilterConfig );
    }
}