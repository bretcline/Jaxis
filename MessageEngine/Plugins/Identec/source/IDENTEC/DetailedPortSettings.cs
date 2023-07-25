namespace IDENTEC
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal class DetailedPortSettings
    {
        public BasicPortSettings BasicSettings = new BasicPortSettings();
        public bool OutCTS;
        public bool OutDSR;
        public DTRControlFlows DTRControl;
        public bool DSRSensitive;
        public bool TxContinueOnXOff = true;
        public bool OutX;
        public bool InX;
        public bool ReplaceErrorChar;
        public RTSControlFlows RTSControl;
        public bool DiscardNulls;
        public bool AbortOnError;
        public char XonChar = '\x0011';
        public char XoffChar = '\x0013';
        public char ErrorChar = '\x0015';
        public char EOFChar = '\x0004';
        public char EVTChar;
        public DetailedPortSettings()
        {
            this.Init();
        }

        protected virtual void Init()
        {
            this.BasicSettings.BaudRate = BaudRates.CBR_19200;
            this.BasicSettings.ByteSize = 8;
            this.BasicSettings.Parity = Parity.none;
            this.BasicSettings.StopBits = StopBits.one;
            this.OutCTS = false;
            this.OutDSR = false;
            this.DTRControl = DTRControlFlows.disable;
            this.DSRSensitive = false;
            this.TxContinueOnXOff = true;
            this.OutX = false;
            this.InX = false;
            this.ReplaceErrorChar = false;
            this.RTSControl = RTSControlFlows.disable;
            this.DiscardNulls = false;
            this.AbortOnError = true;
            this.XonChar = '\x0011';
            this.XoffChar = '\x0013';
            this.ErrorChar = '\x0015';
            this.EOFChar = '\x0004';
            this.EVTChar = '\0';
        }
    }
}

