using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Jaxis.Interfaces
{
    [Flags]
    public enum MessageType : ulong
    {
        All = 0xffff,
        None = 0x0000,
        RawData = 0x0001,
        DBData = 0x0002,
        UIData = 0x0004,
        TagMessage = 0x0008,
        AlertMessage = 0x0010,
    }

    public enum DeliveryTypes : ulong
    {
        None = 0x0000,
        Email = 0x0001,
    }


    public interface IMessage
    {
        string Driver { get; }

        UInt64 Type { get; }

        DateTime ReadTime { get; set; }
    }


    public interface IMessageWrapper : IMessage
    {
        new string Driver { get; set; }

        new UInt64 Type { get; set; }

        new DateTime ReadTime { get; set; }
    }
    
    
    public interface IAlertMessage : IMessage
    {
//        DeliveryTypes DeliveryType { get; set; }
        AlertTypes AlertType { get; set; }
//        AlertClasses AlertClass { get; set; }
        string AlertMessage { get; set; }
    }

    public class BaseMessage : IMessage
    {
        public string Driver { get; set; }

        public UInt64 Type { get; internal set; }

        public DateTime ReadTime { get; set; }
    }

    public static class MessageManipulator
    {
        public static void SetMessageType( BaseMessage _message, UInt64 _type )
        {
            if( null != _message )
            {
                _message.Type = _type;
            }
        }

        public static void SetMessageType(IMessageWrapper _Message, UInt64 _Type)
        {
            if (null != _Message)
            {
                _Message.Type = _Type;
            }
        }

    }


    public interface IPour
    {
    }
}