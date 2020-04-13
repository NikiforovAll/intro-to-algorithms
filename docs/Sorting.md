# Sorting algorithms

## Insertion sort

Insertion sort is efficient algorithms for small number of elements. The index `j` indicates current element being inserted. At the beginning of eat iteration of the **for** loop, which is indexed by `j`, the subarray consisting of elements `A[1..j-1]` constitutes the current sorted subarray. As a result, by the `j = n + 1, where n = A.length` original array is sorted. This is an example of incremental approach: having sorted the subarray `A[1..j-1]`, we inserted the single element `A[j]` into proper place, yielding the sorted subarray `A[1..j]`.

<!-- ![](https://user-images.githubusercontent.com/8037439/79077454-8a338a80-7d0a-11ea-9bdb-a51eb896c8c8.png =250x250) -->

<img src="https://user-images.githubusercontent.com/8037439/79077454-8a338a80-7d0a-11ea-9bdb-a51eb896c8c8.png" alt="insertion-sort-pseudocode" width="30%">

Let's define sorter interface for convenience:

```csharp
interface ISorter<T> where T : IComparable<T> {
    void Sort(Span<T> arr);
}
```

Implementation:

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

Merge Sort is an example of algorithms that closely follows divide-and-conquer:

* **Divide**: Divide n-element sequence to be sorted into two subsequences of n/2 element each.
* **Conquer**: Sort the two subsequences recursively using merge sort.
* **Combine**: Merge two sorted subsequences to produce the sorted answer.

The recursion "bottom out" when the sequence to be sorted has length *1*, since sequence of 1 is already sorted.

<img src="https://user-images.githubusercontent.com/8037439/79137084-bad2fd00-7dba-11ea-8de6-7a68d1c00359.png
" alt="merge-sort-pseudocode" width="20%">

Implementation:

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

## Useful links

* See full code listings: [NikiforovAll/intro-to-algorithms](https://github.com/NikiforovAll/intro-to-algorithms/tree/master/src/Sorting)

<div style="display: flex; justify-content: space-between">
  <a href="../README.md"> ⬅ Back To Home </a>
  <a href="../README.md"> ➡ Next </a>
</div>
