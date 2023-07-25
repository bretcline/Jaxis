﻿using System;
using BeverageMetrics.BeaconParser.BeaconMessages;

namespace BeverageMetrics.BeaconParser
{
    public class BeaconMessageParser
    {
        public BeaconMessage ParseBeaconMessage(byte[] beacon)
        {
            if (beacon.Length < 15)
            {
                throw new Exception("Beacon message is to short!");
            }

            byte beaconType = beacon[0];

            switch (beaconType)
            {
                // Type 1 and 2 (Inventory Message)
                case 1:
                case 2:
                    InventoryMessage invMessage = new InventoryMessage(beaconType, beacon);
                    return invMessage; 

                // Type 3 and 4 (Attach / Detach Message)
                case 3:
                case 4:
                    AttachMessage attachMessage = new AttachMessage(beaconType, beacon);
                    return attachMessage;

                // Type 5 (Pour Message)
                case 5:
                    if (beacon.Length < 21)
                    {
                        throw new Exception("Beacon message is to short!");
                    }

                    PourMessage pourMessage = new PourMessage(beaconType, beacon);
                    return pourMessage;

                // Type 6 (Dormant Message)
                case 6:
                    DormantMessage dormantMessage = new DormantMessage(beaconType, beacon);
                    return dormantMessage; 

                //default:
                    //throw new Exception("Beacon Type not supported!");
            }

            //throw new Exception("Beacon Type not supported!");
            return null;
        }
    }
}
