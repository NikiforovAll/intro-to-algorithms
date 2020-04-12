using System;

namespace Sorting
{
    static class InsertionSortExample
    {
        public static int InsertionSortOne()
        {
            #region InsertionSort
            int[] data =  {5, 2, 4, 6, 1, 3};
            new InsertionSort().Sort<int>(data);
            #endregion
            ShowOutput();
            void ShowOutput() => Console.WriteLine(string.Join(',', data));
            return 0;
        }

        private class InsertionSort : ISorter
        {
            public void Sort<T>(Span<T> arr) where T : IComparable<T>
            {
            }
        }
    }
}
