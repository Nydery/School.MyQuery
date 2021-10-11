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

        [TestMethod]
        public void CountElementsOfList1_10()
        {
            var intList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expected = 10;
            var actual = intList.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SortElementsOfList_RandomNumbers()
        {
            var scrambledIntList = new int[] { 4, 3, 12, 24, 5, 98, 8, 0, 2 };
            var expected = new int[] { 0, 2, 3, 4, 5, 8, 12, 24, 98};
            var actual = scrambledIntList.SortBy(i => i);
            
            CollectionAssert.AreEqual(expected, (ICollection)actual);
        }
    }
}
