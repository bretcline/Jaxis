using System;
using System.Collections.Generic;

namespace Jaxis.Interfaces
{
    public enum DeviceState
    {
        Started = 0,
        Stopped = 1,
        NotReporting = 2,
        Offline = 3,
        Running = 4, 
    }

    public interface IDeviceConfigOption
    {
        string Name { get; set; }
        string AvailableChoices { get; set; }
        string Value { get; set; }
    }

    public class DeviceConfigOption : IDeviceConfigOption
    {
        public string Name { get; set; }
        public string AvailableChoices { get; set; }
        public string Value { get; set; }
    }

    public interface IDeviceConfig
    {
        DeviceState State { get; set; }

        string AssemblyName { get; set; }

        string AssemblyType { get; set; }

        string AssemblyVersion { get; set; }

        string ID { get; set; }

        string Name { get; set; }

        DeviceType Type { get; set; }

        UInt64 ConsumerMessageType { get; set; }

        UInt64 ProducerMessageType { get; set; }

        List<DeviceConfigOption> Options { get; set; }

        string GetOptionData(int _Index);
        string GetOptionData(int _Index, string _Default);
        T GetOptionData<T>(int _Index, Func<string, T> _Parser, T _Default);
    }


    public interface IDeviceConfigView
    {
        DeviceState State { get; }

        string AssemblyName { get; }

        string AssemblyType { get; }

        string AssemblyVersion { get; }

        string ID { get; }

        string Name { get; }

        DeviceType Type { get; }

        UInt64 ConsumerMessageType { get; }

        UInt64 ProducerMessageType { get; }

        List<DeviceConfigOption> Options { get; }

        string GetOptionData(int _Index);
        string GetOptionData(int _Index, string _Default);
        T GetOptionData<T>(int _Index, Func<string, T> _Parser, T _Default);
    }

    public interface IFilterConfig
    {
        string Name { get; set; }

        FilterType Type { get; set; }

        List<DeviceConfigOption> Options { get; set; }
    }
}