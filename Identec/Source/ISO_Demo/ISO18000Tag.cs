using System;
using System.Collections.Generic;
using System.Text;

namespace IDENTEC
{
    namespace Tags
    {
        class ISO18000Tag : IComparable
        {
            private System.UInt32 m_dwTagID;
            private System.UInt16 m_ManufacturerID;
            public byte[] m_RoutingCode;
            public byte m_RoutingCodeType;
            public byte[] m_UserID;
            public byte m_UserIDType;
            internal System.DateTime _lastTimeSeenAwake = DateTime.Now; // used to keep the last time tag was woken up
            private System.Collections.Generic.Dictionary<int, int> m_SignalStrength = new Dictionary<int, int>();

            /// <summary>
            /// The invalid signal strength value. If tag communications fails, the tag signal strength will be this value.
            /// </summary>
            public const int InvalidSignal = -128;

            public UInt32 ID
            {
                get
                {
                    return m_dwTagID;
                }
                set
                {
                    if (value != 0)
                        m_dwTagID = value;
                    else
                        throw new ArgumentOutOfRangeException("value");
                }
            }
            public void setRSSI(int antenna, int level)
            {
                if (m_SignalStrength.ContainsKey(antenna))
                {
                    m_SignalStrength[antenna] = level;
                }
                else
                    m_SignalStrength.Add(antenna, level);
            }
            public UInt16 ManufacturerID
            {
                get
                {
                    return m_ManufacturerID;
                }
                set
                {
                    if (value != 0)
                        m_ManufacturerID = value;
                    else
                        throw new ArgumentOutOfRangeException("value");
                }
            }

            #region IComparable Members

            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int CompareTo(object obj)
            {
                ISO18000Tag tag = obj as ISO18000Tag;
                if (null != tag)
                {
                    return m_dwTagID.CompareTo(tag.ID);
                }
                else
                    throw new ArgumentException("The tag can only be compared to a tag object");

                //return 0;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                ISO18000Tag tag = obj as ISO18000Tag;
                if (null != tag)
                {
                    return m_dwTagID.Equals(tag.ID);
                }
                else
                    throw new ArgumentException("The tag can only be compared to a tag object");
            }

            // Omitting getHashCode violates rule: OverrideGetHashCodeOnOverridingEquals.
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return this.m_dwTagID.GetHashCode();
            }


            #endregion

            /// <summary>
            /// A method to get the signal strength from the specified antenna.
            /// </summary>
            /// <param name="antenna">The antenna number for the specified signal.</param>
            /// <returns>The relative signal strength as calculated by the reader.</returns>
            /// <remarks>This method is only valid for tags detected on readers with multiple antennas.</remarks>
            public int GetSignalStrength(int antenna)
            {
                int value;
                //Check to see if the tag was detected on a reader with multiple antennas:
                if (null == m_SignalStrength)
                    return InvalidSignal;

                if (!m_SignalStrength.TryGetValue(antenna, out value))
                    throw new ArgumentOutOfRangeException("antenna");
                return value;
            }	


        }
    }
}

