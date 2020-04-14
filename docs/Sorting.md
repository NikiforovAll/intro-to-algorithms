# Sorting algorithms

In my opinion, understanding of classical algorithms is essential since it allows you to use them as reliable **building blocks**. Furthermore, it allows you to predict behavior and runtime complexity of solution if it is built based on well-known building blocks and techniques.

<img src="https://user-images.githubusercontent.com/8037439/79157842-f9c67a00-7ddd-11ea-8d29-94e2fdb7e43c.png" alt="alg-run-time" width="45%">

## Insertion sort

Insertion sort is efficient algorithms for small number of elements. The index `j` indicates current element being inserted. At the beginning of each iteration of the *for* loop, which is indexed by `j`, the subarray consisting of elements `A[1..j-1]` constitutes the current sorted subarray. As a result, by the `j = n + 1, where n = A.length` original array is sorted. This is an example of **incremental approach**: having sorted the subarray `A[1..j-1]`, we inserted the single element `A[j]` into proper place, yielding the sorted subarray `A[1..j]`.

<!-- ![](https://user-images.githubusercontent.com/8037439/79077454-8a338a80-7d0a-11ea-9bdb-a51eb896c8c8.png =250x250) -->

<img src="https://user-images.githubusercontent.com/8037439/79077454-8a338a80-7d0a-11ea-9bdb-a51eb896c8c8.png" alt="insertion-sort-pseudocode" width="30%">

Let's define sorter interface for convenience:

```csharp
interface ISorter<T> where T : IComparable<T> {
    void Sort(Span<T> arr);
}
```

### Implementation

```csharp
public void Sort(Span<TItem> arr)
{
    for (int i = 1; i < arr.Length; i++)
        Insert(arr[0..(i + 1)], item: arr[i]);
}

private void Insert(Span<TItem> arr, TItem item)
{
    int i = arr.Length - 1;
    // shift until element is in place
    for (; i > 0 && item.CompareTo(arr[i - 1]) <= 0; i--)
        arr[i] = arr[i - 1];
    arr[i] = item;
}
```

```cs --project ../src/Sorting/Sorting.csproj --source-file ../src/Sorting/InsertionSort.cs --region InsertionSort --session InsertionSort
```

## Merge Sort

Merge Sort is an example of algorithms that closely follows **divide-and-conquer**:

* **Divide**: Divide n-element sequence to be sorted into two subsequences of `n/2` element each.
* **Conquer**: Sort the two subsequences recursively using merge sort.
* **Combine**: Merge two sorted subsequences to produce the sorted answer.

The recursion "bottom out" when the sequence to be sorted has length `1`, since sequence of `1` is already sorted.

As you can see, MergeSort requires additional space of `n` to combine result of subroutines. Unlike InsertionSort, doesn't sort *in place* because it requires non-constant additional space.

<img src="https://user-images.githubusercontent.com/8037439/79137084-bad2fd00-7dba-11ea-8de6-7a68d1c00359.png
" alt="merge-sort-pseudocode" width="25%">

### Implementation

```csharp
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
    return res;
}
```

```cs --project ../src/Sorting/Sorting.csproj --source-file ../src/Sorting/MergeSort.cs --region MergeSort --session MergeSort
```

## Heap Sort

*Heap Sort* introduces another algorithm design technique: using **a data structure to manage information**. The (binary) heap data structure is an array object that we can view as a nearly complete binary tree. Each node of the tree corresponds to element in array. Node with given index `i` could have left (`2i`) and right (`2i+1`) child nodes. *Heap Sort* uses max-heap. Every node of max-heap (except root node) satisfies condition `A[Parent(i) >= A[i]]`. The trick is that most basic operations on heap run in time proportional to the height of the tree and thus takes `O(lg n)` time.

### Max-Heapify

This operation is intended to restore max-heap property for given node `i`. Obviously, it is possible to build max-heap by applying *max-heapify* operation to relevant nodes.

<img src="https://user-images.githubusercontent.com/8037439/79236516-b7e91280-7e75-11ea-958c-89838a0ab215.png
" alt="merge-sort-pseudocode" width="25%">

*Heap Sort* algorithm starts by building max-heap. Since the maximum element is stored in root element  `A[1]`. We can put it final position by exchanging it with `A[n]`. All we need to do it to restore max-heap property for `A[1]` element for `A[1..n-1]` heap. As result, we sort array by repeating this process down to heap of size `2`.

<img src="https://user-images.githubusercontent.com/8037439/79240753-db628c00-7e7a-11ea-8802-c81166038fce.png
" alt="merge-sort-pseudocode" width="25%">

### Implementation

```csharp
private void BuildMaxHeap(Span<TItem> arr)
{
    // Build max-heap
    for (int i = arr.Length / 2 - 1; i >= 0; i--)
    {
        MaxHeapify(arr, i);
    }
    // Sort
    for (int i = 1; i < arr.Length - 1; i++)
    {
        (arr[0], arr[^i]) = (arr[^i], arr[0]);
        MaxHeapify(arr[..^i], 0);
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
        MaxHeapify(arr, left); MaxHeapify(arr, right);
    }
}

private ref TItem LargestOf(ref TItem item1, ref TItem item2) =>
    ref item1.CompareTo(item2) > 0 ? ref item1 : ref item2;
```

```cs --project ../src/Sorting/Sorting.csproj --source-file ../src/Sorting/HeapSort.cs --region HeapSort --session HeapSort
```

## Useful links

* See full code listings: [NikiforovAll/intro-to-algorithms](https://github.com/NikiforovAll/intro-to-algorithms/tree/master/src/Sorting)

<div style="display: flex; justify-content: space-between">
  <a href="../README.md"> ⬅ Back To Home </a>
  <a href="../README.md"> ➡ Next </a>
</div>
