namespace PureRF
{
    using System;

    public enum ReceiverRetVal
    {
        ERROR,
        SUCCESS,
        LOOP_TIMEOUT,
        LOOP_COMM_ERROR,
        BAD_CRC,
        BAD_SYNC,
        PROTOCOL_ERROR,
        PACKET_TOO_SMALL,
        BAD_PARAMS,
        NO_BROADCAST,
        GOT_BROADCAST,
        MODE_BOOTLOADER,
        MODE_FIRMWARE,
        FLASH_EMPTY,
        FLASH_DAMAGED,
        FLASH_OK
    }
}

