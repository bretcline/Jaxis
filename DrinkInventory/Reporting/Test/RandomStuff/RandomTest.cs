using NUnit.Framework;

namespace Jaxis.DrinkInventory.Reporting.Test.RandomStuff
{
    [TestFixture]
    class RandomTest
    {
        [Test]
        public void StringCompare()
        {
            var str1 = "123";
            var str2 = "123456";

            var c = string.Compare(str1, 0, str2, 0, str1.Length);
            Assert.AreEqual(0, c, "The first characters of str2 should equal to str1");

            c = string.Compare(str2, 0, str1, 0, str2.Length);
            Assert.AreNotEqual(0, c, "The first characters of str1 should not equal to str2.  This may blow up, that's why i'm testing it");
        }
    }
}
