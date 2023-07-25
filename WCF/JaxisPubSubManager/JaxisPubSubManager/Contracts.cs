using System;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace JaxisPubSubManager
{
    /// <summary>
    /// Data contract for persistent subscribers
    /// </summary>
    [DataContract]
    public struct PersistentSubscription
    {
        [DataMember]
        string m_Address;

        [DataMember]
        string m_Contract;

        [DataMember]
        string m_Operation;

        public string Address
        {
            get
            {
                return m_Address;
            }
            set
            {
                m_Address = value;
            }
        }
        public string Contract
        {
            get
            {
                return m_Contract;
            }
            set
            {
                m_Contract = value;
            }
        }
        public string Operation
        {
            get
            {
                return m_Operation;
            }
            set
            {
                m_Operation = value;
            }
        }
    }
}