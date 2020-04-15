using System;

namespace Sorting
{
    static class QuickSortExample
    {
        public static int QuickSortOne()
        {
            #region QuickSort
            int[] data = { 5, 2, 4, 6, 1, 3 };
            new QuickSorter<int>().Sort(data);
            #endregion

            Console.WriteLine($"Result:\t{string.Join(',', data)}");
            return 0;
        }

        private class QuickSorter<TItem> : ISorter<TItem> where TItem : IComparable<TItem>
        {
            public void Sort(Span<TItem> arr) => QuickSort(arr);
            private void QuickSort(Span<TItem> arr)
            {
                if (arr.IsEmpty || arr.Length == 1)
                    return;

                Console.Write($"{string.Join(',', arr.ToArray()),-15}");
                var q = Partition(arr);
                Console.WriteLine($"-[q={q}]->{string.Join(',', arr.ToArray()),15}");
                QuickSort(arr[..q++]);
                if (q < arr.Length - 1) // not already sorted
                    QuickSort(arr[q..]);
            }
            private int Partition(Span<TItem> arr)
            {
                ref var pivot = ref arr[^1];
                var i = -1; // current end of lessThan array part
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j].CompareTo(pivot) <= 0)
                    {
                        i++;
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                    }
                }
                var q = i + 1; //pivotPosition
                (arr[q], pivot) = (pivot, arr[q]);
                return q;
            }
        }
    }
}
