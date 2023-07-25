using System;

namespace Jaxis.DrinkInventory.Reporting.Test
{
    public class TestHelpers
    {
        public static string TestString(int _length = 50)
        {
            var testString = string.Empty;
            
            while (testString.Length < _length)
            {
                testString += Guid.NewGuid().ToString();
            }

            testString = testString.Substring(0, _length);
            return testString;
        }
    }
}
