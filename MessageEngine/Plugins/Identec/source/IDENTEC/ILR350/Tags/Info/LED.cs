namespace IDENTEC.ILR350.Tags.Info
{
    using System;

    public class LED : TagFeature
    {
        private int LEDCount;

        public LED()
        {
            base.name = "LED";
        }

        public LED(int nbLED)
        {
            base.name = "LED";
            this.LEDCount = nbLED;
        }

        public int Count
        {
            get
            {
                return this.LEDCount;
            }
        }
    }
}

