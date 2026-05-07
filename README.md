# C# Collections — Complete Code Examples

Ordered by interface inheritance hierarchy, with practical examples for each collection type.


---

## A. Generic Collections Hierarchy

```text
A. IEnumerable<T>
│
└── A ICollection<T>
│   │
│   ├── A.1 IList<T>
│       │   ├── A.1.1 List<T>
│       │   ├── A.1.2 T[] Array
│       │   └── A.1.3 LinkedList<T>
│       │
│       ├── A.2 IDictionary<TKey, TValue>
│       │   ├── A.2.1 Dictionary<TKey, TValue>
│       │   ├── A.2.2 SortedDictionary<TKey, TValue>
│       │   ├── A.2.3 SortedList<TKey, TValue>
│       │   └── A.2.4 ReadOnlyDictionary<TKey, TValue>
│       │
│       ├── A.3 ISet<T>
│       │   ├── A.3.1 HashSet<T>
│       │   └── A.3.2 SortedSet<T>
│       ├── A.4 Queue & Stack<T>
│           ├── A.4.1 Queue<T>
│           └── A.4.2 Stack<T>
│
└── B. IReadOnlyCollection<T>
    │
    ├── B.1 IReadOnlyList<T>
    └── B.2 IReadOnlyDictionary<TKey, TValue>
    └── B.3 IReadOnlySet<TKey, TValue>
```

Read-only interfaces are useful when you want to expose data without allowing the caller to modify it.

---
# Quick Selection Guide

| Use Case | Best Type |
|---|---|
| Only loop through items | `IEnumerable<T>` |
| Need `Count`, `Add`, `Remove` | `ICollection<T>` |
| Need index access | `IList<T>` |
| Need key-value lookup | `IDictionary<TKey, TValue>` |
| Need unique values | `ISet<T>` |
| Need sorted unique values | `SortedSet<T>` |
| Need FIFO behavior | `Queue<T>` |
| Need LIFO behavior | `Stack<T>` |
| Expose data safely | `IReadOnlyCollection<T>` / `IReadOnlyList<T>` / `IReadOnlyDictionary<TKey, TValue>` |

---


# Summary

| Collection | Ordered | Indexed | Unique | Sorted | Key-Value |
|---|---:|---:|---:|---:|---:|
| `List<T>` | Yes | Yes | No | No | No |
| `T[]` | Yes | Yes | No | No | No |
| `LinkedList<T>` | Yes | No | No | No | No |
| `Dictionary<TKey, TValue>` | No guarantee | No | Keys only | No | Yes |
| `SortedDictionary<TKey, TValue>` | Yes, by key | No | Keys only | Yes | Yes |
| `SortedList<TKey, TValue>` | Yes, by key | Yes, by key/value collections | Keys only | Yes | Yes |
| `HashSet<T>` | No guarantee | No | Yes | No | No |
| `SortedSet<T>` | Yes | No | Yes | Yes | No |
| `Queue<T>` | Yes | No | No | No | No |
| `Stack<T>` | Yes | No | No | No | No |
```

Final rule:

> Program to the most general interface that gives you the operations you need.
>
> Example: use `IEnumerable<T>` for reading, `ICollection<T>` for adding/removing, `IList<T>` for indexing, and `IDictionary<TKey, TValue>` for key-value access.
