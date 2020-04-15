using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorting
{
    static class BucketSortExample
    {
        public static int BucketSortOne()
        {
            #region BucketSort
            int[] data = { 5, 2, 4, 6, 1, 3, 9, 14 };
            new BucketSorter<int>(4).Sort(data);
            #endregion

            Console.WriteLine($"{"Result:", -10} {string.Join(',', data)}");
            return 0;
        }

        private class BucketSorter<TItem> : IConvertibleSorter<TItem> where TItem : IConvertible
            // : IIdentifiableSorter<TItem> where TItem : IIdentifiable<int>
        {
            public int NumberOfBuckets { get; }

            public BucketSorter(int numberOfBuckets)
            {
                this.NumberOfBuckets = numberOfBuckets;
            }

            public void Sort(Span<TItem> arr)
            {
                var buckets = new List<TItem>[NumberOfBuckets];
                var length = arr.Length;
                //init buckets
                for (int i = 0; i < NumberOfBuckets; i++)
                    buckets[i] = new List<TItem>();
                // fill buckets (normal distribution)
                for (int i = 0; i < length; i++)
                    buckets[arr[i].ToInt32(default) / NumberOfBuckets].Add(arr[i]);
                Console.Write($"{"buckets:", -10} ");
                foreach (var item in buckets.Select((items, k) => (items, k)))
                {
                    Console.Write($"[{item.k}] - {string.Join(',', item.items)}\t");
                }
                Console.WriteLine("");
                //sort buckets
                for (int j = 0; j < NumberOfBuckets; j++)
                    buckets[j].Sort();
                Console.Write($"{"sort:", -10} ");
                foreach (var item in buckets.Select((items, k) => (items, k)))
                {
                    Console.Write($"[{item.k}] - {string.Join(',', item.items)}\t");
                }
                Console.WriteLine("");
                var sorted = buckets.SelectMany(b => b)
                                    .Select((item, i) => (item, i));
                // fill original array
                foreach (var (item, i) in sorted)
                    arr[i] = item;
            }
        }
    }
}
