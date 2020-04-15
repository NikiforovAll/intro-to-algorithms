using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sorting
{
    static class HeapSortExample
    {
        public static int HeapSortOne()
        {
            #region HeapSort
            int[] data = { 5, 2, 4, 6, 1, 3 };
            new HeapSorter<int>().Sort(data);
            #endregion
            Print<int>(data);
            return 0;
        }

        private class HeapSorter<TItem> : ISorter<TItem> where TItem : IComparable<TItem>
        {
            public void Sort(Span<TItem> arr)
            {
                HeapSort(arr);
            }
            private void HeapSort(Span<TItem> arr)
            {
                // Build max-heap
                for (int i = arr.Length / 2 - 1; i >= 0; i--)
                {
                    MaxHeapify(arr, i);
                }
                Print(arr, "max-heap: ");
                // Sort
                for (int i = 1; i < arr.Length; i++)
                {
                    Console.Write($"[i = {i}]\t{string.Join(',', arr.ToArray())}\t->\t");
                    ref var current = ref arr[^i];
                    (arr[0], current) = (current, arr[0]);
                    MaxHeapify(arr[..^i], 0);
                    Console.WriteLine(string.Join(',', arr.ToArray()));
                    // TODO: uncomment this when https://github.com/dotnet/try/issues/821 will be fixed
                    // (arr[0], arr[^i]) = (arr[^i], arr[0]);
                    // MaxHeapify(arr[..^i], 0);
                }
            }

            private void MaxHeapify(Span<TItem> arr, int i)
            {
                var heapSize = arr.Length - 1;
                ref var node = ref arr[i];
                var (left, right) = (2 * i + 1, 2 * i + 2);
                ref var toSwap = ref LargestOf(
                    ref left < heapSize ? ref arr[left] : ref arr[i],
                    ref right < heapSize ? ref arr[right] : ref arr[i]);
                if (!node.Equals(LargestOf(ref node, ref toSwap)))
                {
                    (node, toSwap) = (toSwap, node);
                    MaxHeapify(arr, left);
                    MaxHeapify(arr, right);
                }
            }

            private ref TItem LargestOf(ref TItem item1, ref TItem item2) =>
                ref item1.CompareTo(item2) > 0 ? ref item1 : ref item2;
        }

        private static void Print<TItem>(Span<TItem> data, string message = "Result: ") =>
            Console.WriteLine($"{message}{string.Join(',', data.ToArray())}");
    }
}
