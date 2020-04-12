# Sorting algorithms

## Insertion sort

Insertion sort is efficient algorithms for small number of elements. The index `j` indicates current element being inserted. At the beginning of eat iteration of the **for** loop, which is indexed by `j`, the subarray consisting of elements `A[1..j-1]` constitutes the current sorted subarray. As a result, by the `j = n + 1, where n = A.length` original array is sorted and is loop invariant.

<!-- ![](https://user-images.githubusercontent.com/8037439/79077454-8a338a80-7d0a-11ea-9bdb-a51eb896c8c8.png =250x250) -->

<img src="https://user-images.githubusercontent.com/8037439/79077454-8a338a80-7d0a-11ea-9bdb-a51eb896c8c8.png" alt="insertion-sort-pseudocode" width="30%">

Let's define sorter interface for convenience:

```csharp
internal interface ISorter {
    void Sort<T>(Span<T> arr) where T : IComparable<T>;
}
```

```cs --project ../src/Sorting/Sorting.csproj --source-file ../src/Sorting/InsertionSort.cs --region InsertionSort
```


<div style="display: flex; justify-content: space-between">
  <a href="../README.md"> ⬅ Back To Home </a>
  <a href="../README.md"> ➡ Next </a>
</div>
