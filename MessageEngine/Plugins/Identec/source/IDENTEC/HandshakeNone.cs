namespace IDENTEC
{
    using System;

    internal class HandshakeNone : DetailedPortSettings
    {
        protected override void Init()
        {
            base.Init();
            base.OutCTS = false;
            base.OutDSR = false;
            base.OutX = false;
            base.InX = false;
            base.RTSControl = RTSControlFlows.enable;
            base.DTRControl = DTRControlFlows.enable;
            base.TxContinueOnXOff = true;
            base.DSRSensitive = false;
        }
    }
}

