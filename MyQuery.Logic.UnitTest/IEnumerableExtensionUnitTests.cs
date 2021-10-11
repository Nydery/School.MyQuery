using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace MyQuery.Logic.UnitTest
{
    [TestClass]
    public class IEnumerableExtensionUnitTests
    {
        [TestMethod]
        public void CountElementsOfList1_10()
        {
            var intList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expected = 10;
            var actual = intList.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CountElementsOfStringList_5Elems()
        {
            var stringList = new string[] { "", "1", "austria", "döner", "jeton"};
            var expected = 5;
            var actual = stringList.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ApplyFilter_EvenNumbersFromList1_10()
        {
            var intList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expectedResult = new int[] { 2, 4, 6, 8, 10 };

            var actualResult = intList.Filter(x => x % 2 == 0);

            CollectionAssert.AreEqual(expectedResult, (ICollection)actualResult);
        }

        [TestMethod]
        public void ApplyFilter_StartsWithLowerLetter_Strings()
        {
            var stringList = new string[]
            {
                "Austria",
                "Deutschland",
                "österreich!",
                "austria",
                "AAAAAAAAAAAAAAAAAAAAAAAAA",
                "bBBBBBBBBBBBBBBBBBB"
            };
            var expected = new string[]
            {
                "österreich!",
                "austria",
                "bBBBBBBBBBBBBBBBBBB"
            };

            var actual = stringList.Filter(i => char.IsLower(i[0]));
            CollectionAssert.AreEqual(expected, (ICollection)actual);
        }

        [TestMethod]
        public void Map_IntArrayToStringArray()
        {
            var intArr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expected = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            var actual = intArr.Map(i => i.ToString());

            CollectionAssert.AreEqual(expected, (ICollection)actual);
        }

        [TestMethod]
        public void Map_StringArrayToDoubleArray()
        {
            var stringArr = new string[] { "1,5", "2,4", "3,3", "4,2", "5,1", "6,0", "7,9", "8,8", "9,7", "10,6" };
            var expected = new double[] { 1.5, 2.4, 3.3, 4.2, 5.1, 6.0, 7.9, 8.8, 9.7, 10.6 };
            var actual = stringArr.Map(i => double.Parse(i));

            CollectionAssert.AreEqual(expected, (ICollection)actual);
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