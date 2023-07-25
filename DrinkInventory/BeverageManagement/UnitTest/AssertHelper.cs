using NUnit.Framework;

namespace Jaxis.DrinkInventory.BeverageManagement.UnitTest
{
    public class AssertHelper
    {
        public static void HasValueIsTrue(bool? _condition)
        {
            Assert.IsTrue(_condition.HasValue);
            // ReSharper disable PossibleInvalidOperationException
            Assert.IsTrue(_condition.Value);
            // ReSharper restore PossibleInvalidOperationException
        }

        public static void HasValueIsFalse(bool? _condition)
        {
            Assert.IsTrue(_condition.HasValue);
            // ReSharper disable PossibleInvalidOperationException
            Assert.IsFalse(_condition.Value);
            // ReSharper restore PossibleInvalidOperationException
        }
    }
}
