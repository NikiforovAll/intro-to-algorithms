using System;
using System.Collections.Generic;
using System.Globalization;

namespace Sorting
{
    static class MergeSortExample
    {
        public static int MergeSortOne()
        {
            #region MergeSort
            int[] data = { 5, 2, 4, 6, 1, 3 };
            new MergeSorter<int>().Sort(data);
            #endregion
            Console.WriteLine($"Result:\t{string.Join(',', data)}");
            return 0;
        }

        private class MergeSorter<TItem> : ISorter<TItem> where TItem : IComparable<TItem>
        {
            public void Sort(Span<TItem> arr) => MergeSort(arr).CopyTo(arr);

            private Span<TItem> MergeSort(Span<TItem> arr)
            {
                int mid = arr.Length / 2;
                return mid > 0 ? Merge(MergeSort(arr[..mid]), MergeSort(arr[mid..])) : arr;
            }
            private Span<TItem> Merge(ReadOnlySpan<TItem> arr1, ReadOnlySpan<TItem> arr2)
            {
                var res = new TItem[arr1.Length + arr2.Length];
                int i = 0, j = 0, k = 0;
                while (k < res.Length)
                {
                    res[k++] = (i, j) switch
                    {
                        _ when i == arr1.Length => arr2[j++],
                        _ when j == arr2.Length => arr1[i++],
                        _ => arr1[i].CompareTo(arr2[j]) > -1 ? arr2[j++] : arr1[i++]
                    };
                }
                Console.WriteLine($"[{string.Join(',', arr1.ToArray()),5}] + [{string.Join(',', arr2.ToArray()),5}] = {string.Join(',', res)}");
                return res;
            }
        }
    }
}
