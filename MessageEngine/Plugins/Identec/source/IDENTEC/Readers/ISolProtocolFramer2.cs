namespace IDENTEC.Readers
{
    using IDENTEC;
    using System;
    using System.Threading;

    internal class ISolProtocolFramer2 : ISolProtocolFramer
    {
        private byte[] m_buffer = new byte[0x400];

        internal override int RecvMsg(byte[] msg, int timeout, int nPauseBeforeRead)
        {
            Thread.Sleep(nPauseBeforeRead);
            int nRead = 0;
            IC3ProtocolMessage message = new IC3ProtocolMessage();
            int tickCount = Environment.TickCount;
            do
            {
                if (!base.ReadPortCached(this.m_buffer, 1, ref nRead))
                {
                    return -1;
                }
                message.ParseMessage(this.m_buffer, nRead);
                if (message.FullMessage)
                {
                    base.m_nReceivePacketCount++;
                    base.SetPacketReceivedEvent();
                    bool isSOHAtStartOfBuffer = message.IsSOHAtStartOfBuffer;
                    message.UnpackAndCheckCRC();
                    Array.Copy(message.Buffer, 1, msg, 0, message.BufferLength - 1);
                    if (ISolProtocolFramer.log.IsDebugEnabled)
                    {
                        string str = "RX:";
                        for (int i = 0; i < message.BufferLength; i++)
                        {
                            str = str + message.Buffer[i].ToString("X") + ":";
                        }
                        ISolProtocolFramer.log.Debug(str);
                    }
                    return message.MessageBodyLength;
                }
            }
            while (!Reader.TimedOut(ref tickCount, timeout));
            throw new ReaderTimeoutException("Low level communications timeout. Timeout setting was " + timeout + "ms");
        }
    }
}

