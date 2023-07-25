namespace PureRF
{
    using System;

    public enum InitializerRetVal
    {
        ERROR,
        SUCCESS,
        INVALID_OR_NO_TAG,
        LOOP_TIMEOUT,
        LOOP_COMM_ERROR,
        BAD_CRC,
        BAD_SYNC,
        PACKET_TOO_SMALL,
        PROTOCOL_ERROR,
        BAD_PARAMS,
        MODE_BOOTLOADER,
        MODE_FIRMWARE,
        FLASH_OK,
        FLASH_DAMAGED,
        FLASH_NO_FIRMWARE,
        TAG_TIMEOUT,
        TAG_NAC
    }
}

