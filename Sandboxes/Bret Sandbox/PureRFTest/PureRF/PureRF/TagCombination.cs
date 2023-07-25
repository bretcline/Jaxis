namespace PureRF
{
    using System;
    using System.Globalization;

    public class TagCombination
    {
        public string FullName = "";
        public string PrintName = "";
        public int SNCode;

        public TagCombination(string Data)
        {
            this.Parce(Data);
        }

        public void Parce(string Data)
        {
            string[] strArray = Data.Split(new char[] { '|' });
            if (strArray.Length >= 3)
            {
                this.SNCode = int.Parse(strArray[0].Substring(2, 2), NumberStyles.AllowHexSpecifier);
                this.FullName = strArray[1];
                this.PrintName = strArray[2];
            }
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }
}

