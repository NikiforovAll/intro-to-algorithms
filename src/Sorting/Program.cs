using System;
using System.Threading.Tasks;

namespace Sorting {
    public class Program {
        public static int Main (string[] ? args = null) {
            return RunAll ();
        }

        public static int RunAll () {
            InsertionSortExample.InsertionSortOne ();
            return 0;
        }
    }

    internal interface ISorter {
        void Sort<T>(Span<T> arr) where T : IComparable<T>;
    }
}
