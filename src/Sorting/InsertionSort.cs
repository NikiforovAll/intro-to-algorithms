using System;

namespace Sorting
{
    static class InsertionSortExample
    {
        public static int InsertionSortOne()
        {
            #region InsertionSort
            int[] data = { 5, 2, 4, 6, 1, 3 };
            new InsertionSort<int>().Sort(data);
            #endregion

            Console.WriteLine($"Result:\t{string.Join(',', data)}");
            return 0;
        }

        private class InsertionSort<TItem> : ISorter<TItem> where TItem : IComparable<TItem>
        {
            public void Sort(Span<TItem> arr)
            {
                for (int i = 1; i < arr.Length; i++)
                {
                    Console.Write($"[i = {i}]\t{string.Join(',', arr.ToArray())}\t->\t");
                    Insert(arr[0..(i + 1)], item: arr[i]);
                    Console.WriteLine(string.Join(',', arr.ToArray()));
                }
            }

            private void Insert(Span<TItem> arr, TItem item)
            {
                int i = arr.Length - 1;
                for (; i > 0 && item.CompareTo(arr[i - 1]) <= 0; i--)
                {
                    arr[i] = arr[i - 1];
                }
                arr[i] = item;
            }
        }
    }
}
