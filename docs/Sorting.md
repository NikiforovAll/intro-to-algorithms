# Sorting algorithms

## Insertion sort

Insertion sort is efficient algorithms for small number of elements. The index `j` indicates current element being inserted. At the beginning of eat iteration of the **for** loop, which is indexed by `j`, the subarray consisting of elements `A[1..j-1]` constitutes the current sorted subarray. As a result, by the `j = n + 1, where n = A.length` original array is sorted and is loop invariant.

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
    {
        Insert(arr[0..(i + 1)], item: arr[i]);
    }
}

private void Insert(Span<TItem> arr, TItem item)
{
    int i = arr.Length - 1;
    // shift until element is in place
    for (; i > 0 && item.CompareTo(arr[i - 1]) <= 0; i--)
    {
        arr[i] = arr[i - 1];
    }
    arr[i] = item;
}
```

```cs --project ../src/Sorting/Sorting.csproj --source-file ../src/Sorting/InsertionSort.cs --region InsertionSort
```


<div style="display: flex; justify-content: space-between">
  <a href="../README.md"> ⬅ Back To Home </a>
  <a href="../README.md"> ➡ Next </a>
</div>
