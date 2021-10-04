using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace MyQuery.Logic.UnitTest
{
    [TestClass]
    public class IEnumerableExtensionUnitTests
    {
        [TestMethod]
        public void ApplyFilter_EvenNumbersFromList1_10()
        {
            var intList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expectedResult = new int[] {2, 4, 6, 8, 10 };

            var actualResult = intList.Filter(x => x % 2 == 0);

            CollectionAssert.AreEqual(expectedResult, (ICollection)actualResult);
        }
    }
}
