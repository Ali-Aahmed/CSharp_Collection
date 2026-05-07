// ============================================================
//  By Dr. Ali A. Ahmed
//  C# Collections — Complete Code Examples
//  Ordered by Interface Inheritance Hierarchy
// ============================================================
//
//  HIERARCHY:
//
//  .   IEnumerable<T>
//  A    ICollection<T>
//  A.1    IList<T>
//  A.1.1    List<T>
//  A.1.2    T[]  Array
//  A.1.3    LinkedList<T>
//  A.1.4    ReadOnlyCollection<T>
//  A.2    IDictionary<TKey, TValue>
//  A.2.1    Dictionary<TKey, TValue>
//  A.2.2    SortedDictionary<TKey, TValue>
//  A.2.3    SortedList<TKey, TValue>
//  A.2.4    ReadOnlyDictionary<TKey, TValue>
//  A.3    ISet<T>
//  A.3.1    HashSet<T>
//  A.3.2    SortedSet<T>
//
//  B.   Read-Only Interfaces (.NET 4.5+)
//  B.1    IReadOnlyCollection<T>
//  B.2    IReadOnlyList<T>
//  B.3    IReadOnlyDictionary<TKey, TValue>
//
//  C.   Queue & Stack
//         implements: IEnumerable<T>, ICollection (non-generic), IReadOnlyCollection<T>
//         ⚠ does NOT implement ICollection<T> — by design (protects FIFO/LIFO contract)
//  C.1    Queue<T>
//  C.2    Stack<T>
// ============================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using System.Linq;

namespace CSharpCollectionsGuide
{

    // ============================================================
    // A.  IEnumerable<T>
    // The root of all collections.
    // Only capability: iterate with foreach via GetEnumerator().
    // ============================================================

    class IEnumerableExamples
    {
        public static void Run()
        {
            Console.WriteLine("=== A.  IEnumerable<T> ===");

            // Any collection can be referenced as IEnumerable<T>
            IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // ✅ Only operation available: iterate
            foreach (var n in numbers)
                Console.Write(n + " ");
            Console.WriteLine();

            // LINQ works on IEnumerable<T>
            var evens = numbers.Where(n => n % 2 == 0);
            Console.WriteLine("Evens: " + string.Join(", ", evens)); // 2, 4

            // ❌ Cannot Add, Remove, or access by index through this interface
            // numbers.Add(6);     // Compile error
            // var x = numbers[0]; // Compile error
        }
    }


    // ============================================================
    // A.1  ICollection<T>
    // Extends IEnumerable<T>.
    // Adds: Count, Add, Remove, Clear, Contains, CopyTo.
    // ============================================================

    class ICollectionExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1  ICollection<T> ===");

            ICollection<string> names = new List<string>();

            names.Add("Ahmed");
            names.Add("Sara");
            names.Add("Omar");

            Console.WriteLine("Count: " + names.Count);                         // 3
            Console.WriteLine("Contains Ahmed: " + names.Contains("Ahmed"));    // True

            names.Remove("Sara");
            Console.WriteLine("After remove, Count: " + names.Count);           // 2

            // CopyTo — copy to array
            string[] arr = new string[names.Count];
            names.CopyTo(arr, 0);
            Console.WriteLine("Copied: " + string.Join(", ", arr));             // Ahmed, Omar

            names.Clear();
            Console.WriteLine("After clear, Count: " + names.Count);            // 0
        }
    }


    // ============================================================
    // A.1.1  IList<T>
    // Extends ICollection<T>.
    // Adds: indexer [i], Insert, RemoveAt, IndexOf.
    // ============================================================

    class IListExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.1  IList<T> ===");

            IList<string> fruits = new List<string> { "Apple", "Banana", "Cherry" };

            // Index access
            Console.WriteLine("Index 1: " + fruits[1]);   // Banana

            // Insert at position
            fruits.Insert(1, "Avocado");
            Console.WriteLine("After Insert: " + string.Join(", ", fruits));
            // Apple, Avocado, Banana, Cherry

            // Find by value
            Console.WriteLine("IndexOf Banana: " + fruits.IndexOf("Banana")); // 2

            // Remove at index
            fruits.RemoveAt(0);
            Console.WriteLine("After RemoveAt(0): " + string.Join(", ", fruits));
            // Avocado, Banana, Cherry
        }
    }


    // ============================================================
    // A.1.1.1  List<T>
    // Dynamic array. Implements IList<T>, ICollection<T>, IEnumerable<T>.
    // Best default choice for ordered mutable collections.
    // Time: Add O(1) amortized, Insert/Remove O(n), Access O(1)
    // ============================================================

    class ListExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.1.1  List<T> ===");

            var students = new List<string>();
            students.Add("Ahmed");
            students.Add("Sara");
            students.Add("Omar");
            students.Add("Ali");

            // Access by index
            Console.WriteLine("First: " + students[0]);  // Ahmed

            // Remove by value
            students.Remove("Sara");
            Console.WriteLine("After remove: " + string.Join(", ", students));

            // Search with predicate
            var aNames = students.FindAll(s => s.StartsWith("A"));
            Console.WriteLine("Names starting with A: " + string.Join(", ", aNames));

            // Sort
            students.Sort();
            Console.WriteLine("Sorted: " + string.Join(", ", students));

            // Range operations
            students.AddRange(new[] { "Ziad", "Nour" });
            Console.WriteLine("After AddRange: " + string.Join(", ", students));

            // LINQ on List<T>
            var longNames = students.Where(s => s.Length > 3).ToList();
            Console.WriteLine("Names > 3 chars: " + string.Join(", ", longNames));
        }
    }


    // ============================================================
    // A.1.1.2  T[]  Array
    // Fixed-size, contiguous memory. Fastest random access.
    // Implements IList<T> (read-only indexer only — Add/Remove throw).
    // Time: Access O(1), Search O(n), Sort O(n log n)
    // ============================================================

    class ArrayExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.1.2  T[] Array ===");

            // Fixed at creation — size cannot change
            int[] grades = new int[5] { 90, 85, 78, 92, 88 };

            Console.WriteLine("Grade at [2]: " + grades[2]);  // 78
            Console.WriteLine("Length: " + grades.Length);    // 5

            // Sort in-place
            Array.Sort(grades);
            Console.WriteLine("Sorted: " + string.Join(", ", grades));
            // 78, 85, 88, 90, 92

            // Binary search (array must be sorted first)
            int idx = Array.BinarySearch(grades, 88);
            Console.WriteLine("BinarySearch(88) at index: " + idx); // 2

            // Reverse
            Array.Reverse(grades);
            Console.WriteLine("Reversed: " + string.Join(", ", grades));

            // Multi-dimensional
            int[,] matrix = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
            Console.WriteLine("matrix[1,2]: " + matrix[1, 2]);  // 6

            // Array to List conversion
            var list = new List<int>(grades);
            list.Add(100); // Now we can grow
            Console.WriteLine("List count: " + list.Count);
        }
    }


    // ============================================================
    // A.1.1.3  LinkedList<T>
    // Doubly-linked list. No index access.
    // Best for frequent insert/delete at arbitrary positions.
    // Time: AddFirst/AddLast O(1), Insert after node O(1), Search O(n)
    // ============================================================

    class LinkedListExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.1.3  LinkedList<T> ===");

            var playlist = new LinkedList<string>();

            // Add to ends — O(1)
            playlist.AddLast("Song A");
            playlist.AddLast("Song C");
            playlist.AddFirst("Song X");  // Insert at front in O(1)

            Console.WriteLine("Playlist: " + string.Join(" -> ", playlist));
            // Song X -> Song A -> Song C

            // Insert in the middle — O(1) once node is found
            LinkedListNode<string> nodeC = playlist.Find("Song C");
            playlist.AddBefore(nodeC, "Song B");

            Console.WriteLine("After insert: " + string.Join(" -> ", playlist));
            // Song X -> Song A -> Song B -> Song C

            // Remove specific node — O(1)
            playlist.Remove("Song X");
            Console.WriteLine("After remove: " + string.Join(" -> ", playlist));

            // Navigate with nodes
            var first = playlist.First;
            Console.WriteLine("First: " + first.Value);
            Console.WriteLine("First.Next: " + first.Next?.Value);
        }
    }


    // ============================================================
    // A.1.1.4  ReadOnlyCollection<T>
    // Wraps an IList<T> and exposes it as read-only.
    // Implements IReadOnlyList<T> and IList<T> (throws on mutation).
    // ============================================================

    class ReadOnlyCollectionExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.1.4  ReadOnlyCollection<T> ===");

            var source = new List<string> { "C#", "Java", "Python" };
            var readOnly = new ReadOnlyCollection<string>(source);

            Console.WriteLine("Count: " + readOnly.Count);   // 3
            Console.WriteLine("Index 0: " + readOnly[0]);    // C#

            // Live view — changes to source are reflected here
            source.Add("Go");
            Console.WriteLine("After source.Add: " + readOnly.Count); // 4

            // readOnly.Add("Rust");  // ❌ NotSupportedException at runtime
        }
    }


    // ============================================================
    // A.1.2  IDictionary<TKey, TValue>
    // Extends ICollection<KeyValuePair<K,V>>.
    // Key-to-value mapping. Keys must be unique.
    // ============================================================

    class IDictionaryExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.2  IDictionary<TKey, TValue> ===");

            IDictionary<string, int> ages = new Dictionary<string, int>();

            ages["Ahmed"] = 30;
            ages["Sara"] = 25;
            ages.Add("Omar", 28);

            // Safe lookup — never throws KeyNotFoundException
            if (ages.TryGetValue("Ali", out int val))
                Console.WriteLine("Ali: " + val);
            else
                Console.WriteLine("Ali not found");

            // Check key existence
            Console.WriteLine("ContainsKey Ahmed: " + ages.ContainsKey("Ahmed")); // True

            // Iterate keys and values
            foreach (var kvp in ages)
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");

            Console.WriteLine("Keys: " + string.Join(", ", ages.Keys));
            Console.WriteLine("Values: " + string.Join(", ", ages.Values));
        }
    }


    // ============================================================
    // A.1.2.1  Dictionary<TKey, TValue>
    // Hash table. Best general-purpose key-value store.
    // Time: Add/Remove/Lookup O(1) average, O(n) worst case (hash collision)
    // ============================================================

    class DictionaryExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.2.1  Dictionary<TKey, TValue> ===");

            var salaries = new Dictionary<string, decimal>
            {
                ["Ahmed"] = 5000m,
                ["Sara"] = 7000m,
                ["Omar"] = 6500m
            };

            // Direct access (throws if key missing)
            Console.WriteLine("Sara: " + salaries["Sara"]);  // 7000

            // Safe access
            salaries.TryGetValue("Ali", out decimal s);
            Console.WriteLine("Ali salary: " + s);  // 0 (default)

            // Update
            salaries["Ahmed"] = 5500m;

            // Add or update pattern
            string key = "Nour";
            if (salaries.ContainsKey(key))
                salaries[key] += 100;
            else
                salaries[key] = 4000m;

            // Remove
            salaries.Remove("Omar");
            Console.WriteLine("Count: " + salaries.Count);

            // LINQ on dictionary
            var highEarners = salaries.Where(kv => kv.Value > 5000)
                                      .OrderByDescending(kv => kv.Value);
            foreach (var kv in highEarners)
                Console.WriteLine($"  {kv.Key}: {kv.Value:C}");
        }
    }


    // ============================================================
    // A.1.2.2  SortedDictionary<TKey, TValue>
    // Red-Black Tree. Keys always sorted.
    // Time: Add/Remove/Lookup O(log n)
    // Use when: sorted iteration AND frequent add/remove.
    // ============================================================

    class SortedDictionaryExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.2.2  SortedDictionary<TKey, TValue> ===");

            var scores = new SortedDictionary<string, int>();
            scores["Zara"] = 88;
            scores["Ahmed"] = 95;
            scores["Sara"] = 91;
            scores["Nour"] = 78;

            // Always iterates in sorted key order
            Console.WriteLine("Sorted by name:");
            foreach (var kv in scores)
                Console.WriteLine($"  {kv.Key}: {kv.Value}");
            // Ahmed:95, Nour:78, Sara:91, Zara:88

            // Custom comparer — case-insensitive
            var ci = new SortedDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            ci["beta"] = 2;
            ci["Alpha"] = 1;
            foreach (var kv in ci)
                Console.WriteLine($"  {kv.Key}: {kv.Value}");
            // Alpha:1, beta:2
        }
    }


    // ============================================================
    // A.1.2.3  SortedList<TKey, TValue>
    // Sorted array of key-value pairs.
    // Time: Add/Remove O(n), Lookup O(log n), Index access O(1)
    // Use when: mostly reads, need sorted order AND index access.
    // More memory-efficient than SortedDictionary.
    // ============================================================

    class SortedListExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.2.3  SortedList<TKey, TValue> ===");

            var population = new SortedList<string, long>();
            population["Cairo"] = 21_000_000;
            population["Alexandria"] = 5_200_000;
            population["Giza"] = 4_400_000;

            // Keys and Values accessible by index — unique to SortedList
            Console.WriteLine("Key at 0: " + population.Keys[0]);    // Alexandria
            Console.WriteLine("Value at 0: " + population.Values[0]);  // 5200000

            // Lookup still O(log n)
            Console.WriteLine("Cairo: " + population["Cairo"]);

            // Index of key
            int idx = population.IndexOfKey("Giza");
            Console.WriteLine("Giza index: " + idx);  // 1
        }
    }


    // ============================================================
    // A.1.2.4  ReadOnlyDictionary<TKey, TValue>
    // Wraps an IDictionary and exposes it as read-only.
    // Implements IReadOnlyDictionary<K,V>.
    // ============================================================

    class ReadOnlyDictionaryExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.2.4  ReadOnlyDictionary<TKey, TValue> ===");

            var source = new Dictionary<string, int> { ["one"] = 1, ["two"] = 2 };
            var readOnly = new ReadOnlyDictionary<string, int>(source);

            Console.WriteLine("Count: " + readOnly.Count);                          // 2
            Console.WriteLine("[\"two\"]: " + readOnly["two"]);                     // 2
            Console.WriteLine("ContainsKey one: " + readOnly.ContainsKey("one"));   // True

            // Live view — source changes are reflected here
            source["three"] = 3;
            Console.WriteLine("After source mutation, Count: " + readOnly.Count);   // 3

            // readOnly.Add("four", 4);  // ❌ NotSupportedException at runtime
        }
    }


    // ============================================================
    // A.1.3  ISet<T>
    // Extends ICollection<T>.
    // Guarantees uniqueness. Supports mathematical set operations.
    // ============================================================

    class ISetExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.3  ISet<T> ===");

            ISet<int> setA = new HashSet<int> { 1, 2, 3, 4, 5 };
            ISet<int> setB = new HashSet<int> { 3, 4, 5, 6, 7 };

            // Union — elements in A or B
            var union = new HashSet<int>(setA);
            union.UnionWith(setB);
            Console.WriteLine("Union: " + string.Join(", ", union.OrderBy(x => x)));
            // 1,2,3,4,5,6,7

            // Intersection — elements in both
            var intersect = new HashSet<int>(setA);
            intersect.IntersectWith(setB);
            Console.WriteLine("Intersect: " + string.Join(", ", intersect));
            // 3,4,5

            // Difference — in A but not B
            var except = new HashSet<int>(setA);
            except.ExceptWith(setB);
            Console.WriteLine("Except: " + string.Join(", ", except));
            // 1,2

            // Subset / superset checks
            ISet<int> small = new HashSet<int> { 3, 4 };
            Console.WriteLine("small subset of setA: " + small.IsSubsetOf(setA));    // True
            Console.WriteLine("setA superset of small: " + setA.IsSupersetOf(small)); // True
        }
    }


    // ============================================================
    // A.1.3.1  HashSet<T>
    // Hash table. No guaranteed order. Fastest set operations.
    // Time: Add/Remove/Contains O(1) average
    // ============================================================

    class HashSetExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.3.1  HashSet<T> ===");

            var visitedPages = new HashSet<string>();

            visitedPages.Add("/home");
            visitedPages.Add("/about");
            visitedPages.Add("/contact");
            visitedPages.Add("/home");   // Duplicate — silently ignored
            visitedPages.Add("/about");  // Duplicate — silently ignored

            Console.WriteLine("Unique pages: " + visitedPages.Count);  // 3

            // Fast membership test — O(1)
            Console.WriteLine("Visited /home: " + visitedPages.Contains("/home"));  // True
            Console.WriteLine("Visited /shop: " + visitedPages.Contains("/shop"));  // False

            // Real-world: deduplicate a list
            var rawTags = new List<string> { "c#", "dotnet", "c#", "linq", "dotnet" };
            var uniqueTags = new HashSet<string>(rawTags);
            Console.WriteLine("Unique tags: " + string.Join(", ", uniqueTags));
        }
    }


    // ============================================================
    // A.1.3.2  SortedSet<T>
    // Red-Black Tree. Elements always sorted. No duplicates.
    // Time: Add/Remove/Contains O(log n)
    // ============================================================

    class SortedSetExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== A.1.3.2  SortedSet<T> ===");

            var leaderboard = new SortedSet<int> { 85, 92, 78, 100, 88, 92 };
            // Duplicates removed, always sorted

            Console.WriteLine("Scores: " + string.Join(", ", leaderboard));
            // 78, 85, 88, 92, 100

            Console.WriteLine("Min: " + leaderboard.Min);  // 78
            Console.WriteLine("Max: " + leaderboard.Max);  // 100

            // Range queries — very efficient
            var topScores = leaderboard.GetViewBetween(90, 100);
            Console.WriteLine("90-100: " + string.Join(", ", topScores));
            // 92, 100

            // Reverse iteration
            foreach (var score in leaderboard.Reverse())
                Console.Write(score + " ");
            Console.WriteLine();
        }
    }


    // ============================================================
    // B.   Read-Only Interfaces (.NET 4.5+)
    // Expose collections without allowing callers to mutate them.
    // ⚠ They hide mutation — they do NOT make the object immutable.
    //
    // B.1  IReadOnlyCollection<T>  — Count + iterate only
    // B.2  IReadOnlyList<T>        — + indexer [ ]
    // B.3  IReadOnlyDictionary<K,V>— read-only key lookup
    // ============================================================

    class ReadOnlyInterfaceExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== B. Read-Only Interfaces ===");

            // ── B.1  IReadOnlyCollection<T> ─────────────────────
            IReadOnlyCollection<string> roc = new List<string> { "A", "B", "C" };
            Console.WriteLine("\nB.1 IReadOnlyCollection<T>");
            Console.WriteLine("Count: " + roc.Count);   // 3
            foreach (var item in roc) Console.Write(item + " ");
            Console.WriteLine();
            // roc.Add("D");  // ❌ Compile error

            // ── B.2  IReadOnlyList<T> ────────────────────────────
            IReadOnlyList<string> rol = new List<string> { "X", "Y", "Z" };
            Console.WriteLine("\nB.2 IReadOnlyList<T>");
            Console.WriteLine("rol[1]: " + rol[1]);     // Y
            // rol.Add("W");  // ❌ Compile error

            // ── B.3  IReadOnlyDictionary<K,V> ───────────────────
            var source = new Dictionary<string, int> { ["one"] = 1, ["two"] = 2 };
            IReadOnlyDictionary<string, int> rod = source;
            Console.WriteLine("\nB.3 IReadOnlyDictionary<K,V>");
            Console.WriteLine("rod[\"two\"]: " + rod["two"]);                // 2
            Console.WriteLine("ContainsKey one: " + rod.ContainsKey("one")); // True
            // rod.Add("three", 3);  // ❌ Compile error

            // ⚠ Mutability  — original can still be modified:
            source["three"] = 3;
            Console.WriteLine("After source mutation, rod.Count: " + rod.Count); // 3
        }

        // Good pattern: expose IReadOnly* from a public API property
        private List<string> _items = new List<string> { "item1", "item2" };
        public IReadOnlyList<string> Items => _items;  // Caller cannot modify
    }


    // ============================================================
    // C.   Queue & Stack
    //      Implements:
    //        IEnumerable<T>           — foreach iteration
    //        ICollection              — non-generic: Count, CopyTo, SyncRoot
    //        IReadOnlyCollection<T>   — generic read-only Count
    //
    //      ⚠ Does NOT implement ICollection<T> (generic) — intentional.
    //        Exposing Add() would break the FIFO / LIFO contract.
    // ============================================================

    // ============================================================
    // C.1  Queue<T>  — First In, First Out (FIFO)
    // Time: Enqueue/Dequeue/Peek O(1)
    // Use: job queues, print queues, BFS, message processing
    // ============================================================

    class QueueExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== C.1  Queue<T> — FIFO ===");

            var supportQueue = new Queue<string>();
            supportQueue.Enqueue("Ticket #1 - Ahmed: Login issue");
            supportQueue.Enqueue("Ticket #2 - Sara: Payment failed");
            supportQueue.Enqueue("Ticket #3 - Omar: UI bug");

            Console.WriteLine("Queue size: " + supportQueue.Count);   // 3

            // Peek — view without removing
            Console.WriteLine("Next up: " + supportQueue.Peek());     // Ticket #1 

            // Process in arrival order
            while (supportQueue.Count > 0)
            {
                string ticket = supportQueue.Dequeue();
                Console.WriteLine("Processing: " + ticket);
            }
            Console.WriteLine("Queue size after: " + supportQueue.Count);   // 3

        }
    }


    // ============================================================
    // C.2  Stack<T>  — Last In, First Out (LIFO)
    // Time: Push/Pop/Peek O(1)
    // Use: undo/redo, expression parsing, DFS, call stack simulation
    // ============================================================

    class StackExamples
    {
        public static void Run()
        {
            Console.WriteLine("\n=== C.2  Stack<T> — LIFO ===");

            var undoStack = new Stack<string>();
            undoStack.Push("Type 'Hello'");
            undoStack.Push("Type ' World'");
            undoStack.Push("Bold selection");
            undoStack.Push("Delete word");

            Console.WriteLine("Stack size: " + undoStack.Count);    // 4

            // Peek — view without removing
            Console.WriteLine("Last action: " + undoStack.Peek());  // Delete word

            // Undo last 2 actions
            Console.WriteLine("Undo: " + undoStack.Pop()); // Delete word
            Console.WriteLine("Undo: " + undoStack.Pop()); // Bold selection
             
        } 
    }
     
    // ============================================================
    // QUICK COMPARISON — Performance at a glance
    // ============================================================

    class PerformanceComparison
    {
        /*
         Ref        Collection               Add       Remove    Search    Index   Memory
         ─────────────────────────────────────────────────────────────────────────────────
         A.1.1.2    T[] Array                N/A       N/A       O(n)      O(1)    *****
         A.1.1.1    List<T>                  O(1)*     O(n)      O(n)      O(1)    ****
         A.1.1.3    LinkedList<T>            O(1)      O(1)**    O(n)      x       ***
         A.1.2.1    Dictionary<K,V>          O(1)      O(1)      O(1)      x       ***
         A.1.2.2    SortedDictionary<K,V>    O(logn)   O(logn)   O(logn)   x       ***
         A.1.2.3    SortedList<K,V>          O(n)      O(n)      O(logn)   O(1)    ****
         A.1.3.1    HashSet<T>               O(1)      O(1)      O(1)      x       ***
         A.1.3.2    SortedSet<T>             O(logn)   O(logn)   O(logn)   x       ***
         C.1        Queue<T>                 O(1)      O(1)      x         x       ****
         C.2        Stack<T>                 O(1)      O(1)      x         x       ****

         *  O(1) amortized — O(n) when internal array resizes
         ** O(1) only with a node reference; O(n) to find the node first
        */
    }


    // ============================================================
    // ENTRY POINT
    // ============================================================

    class Program
    {
        static void Main(string[] args)
        {
            // A.  IEnumerable hierarchy
            IEnumerableExamples.Run();          // A
            ICollectionExamples.Run();          // A.1
          
            IListExamples.Run();                // A.1.1
            ListExamples.Run();                 // A.1.1.1
            ArrayExamples.Run();                // A.1.1.2
            LinkedListExamples.Run();           // A.1.1.3
            ReadOnlyCollectionExamples.Run();   // A.1.1.4
          
            IDictionaryExamples.Run();          // A.1.2
            DictionaryExamples.Run();           // A.1.2.1
            SortedDictionaryExamples.Run();     // A.1.2.2
            SortedListExamples.Run();           // A.1.2.3
            ReadOnlyDictionaryExamples.Run();   // A.1.2.4
          
            ISetExamples.Run();                 // A.1.3
            HashSetExamples.Run();              // A.1.3.1
            SortedSetExamples.Run();            // A.1.3.2

            // B.  Read-Only Interfaces
            ReadOnlyInterfaceExamples.Run();    // B.1, B.2, B.3

            // C.  Queue & Stack
            QueueExamples.Run();                // C.1
            StackExamples.Run();                // C.2
             

            Console.WriteLine("\n\nAll examples completed.");
            Console.ReadKey();
        }
    }

}  