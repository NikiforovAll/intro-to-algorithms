using System;
using System.Threading.Tasks;

namespace Sorting
{
    public class Program
    {
        ///<param name="region">Takes in the --region option from the code fence options in markdown</param>
        ///<param name="session">Takes in the --session option from the code fence options in markdown</param>
        ///<param name="package">Takes in the --package option from the code fence options in markdown</param>
        ///<param name="project">Takes in the --project option from the code fence options in markdown</param>
        ///<param name="args">Takes in any additional arguments passed in the code fence options in markdown</param>
        ///<see>To learn more see <a href="https://aka.ms/learntdn">our documentation</a></see>
        static int Main(
            string region = null,
            string session = null,
            string package = null,
            string project = null,
            string[] args = null)
        {
            return region
            switch
            {
                "InsertionSort" => InsertionSortExample.InsertionSortOne(),
                "MergeSort" => MergeSortExample.MergeSortOne(),
                "HeapSort" => HeapSortExample.HeapSortOne(),
                "QuickSort" => QuickSortExample.QuickSortOne(),
                "BucketSort" => BucketSortExample.BucketSortOne(),
                _ => RunAll()
            };
        }

        public static int RunAll()
        {
            InsertionSortExample.InsertionSortOne();
            MergeSortExample.MergeSortOne();
            HeapSortExample.HeapSortOne();
            QuickSortExample.QuickSortOne();
            BucketSortExample.BucketSortOne();

            return 0;
        }
    }

    internal interface ISorter<T> where T : IComparable<T>
    {
        void Sort(Span<T> arr);
    }

    internal interface IIdentifiableSorter<T> where T: IIdentifiable<int>
    {
        void Sort(Span<T> arr);
    }

    internal interface IConvertibleSorter<T> where T : IConvertible
    {
        void Sort(Span<T> arr);
    }

    internal interface IIdentifiable<TKey>
    {
        TKey Id { get; set; }
    }
}
