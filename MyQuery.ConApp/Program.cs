using MyQuery.Logic;
using System;

namespace MyQuery.ConApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demo MyQuery!");

            var intList = new int[] { 1, 2, 3, 4, 5, 6};
            var strList = intList.Map(i => i.ToString());

            var scrambledIntList = new int[] { 4, 3, 12, 24, 5, 98, 8, 0, 2};

        }
    }
}
