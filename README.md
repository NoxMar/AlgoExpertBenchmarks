
# AgloExpert Benchmarks

This project is a collection of benchmarks of some of my solutions (or essential parts of them) to exercises on [AlgoExpert](https://www.algoexpert.io/) in C#. This **isn't** a repository of all solutions to those exercises, but rather collections of different approaches to exercises in which I was curious about performance implications of some design or implementation choice.

## Results

### Tournament Winner

It's rather easy exercise that can be boiled down to counting occurrences of all unique strings in a list and choosing the most common one. Following implementation choices were benchmarked:
- using **`System.Collection.Generic.Dictionary`** instance and iterating over the input list in an explicit loop *vs* using **`System.Linq.GroupBy`** extension method to count the occurrences
- using **`Aggregate` vs `MaxBy`** extension methods to determine the most common string from the groupings obtained in the previous step

| Method                      | Mean     | Error     | StdDev     | Gen0     | Gen1    | Gen2    | Allocated |
| --------------------------- | -------- | --------- | ---------- | -------- | ------- | ------- | --------- |
| UsingDictionaryAndAggregate | 1.775 ms | 0.0461 ms | 0.1353 ms  |  27.3438 | 27.3438 | 27.3438 | 210.66 KB |
| UsingDictionaryAndMaxBy     | 1.654 ms | 0.0310 ms | 0.0634 ms  |  29.2969 | 29.2969 | 29.2969 | 210.66 KB |
| UsingGroupByAndAggregate    | 2.921 ms | 0.0581 ms | 0.1391 ms  | 160.1563 | 54.6875 |       - | 988.96 KB |
| UsingGroupByAndMaxBy        | 3.086 ms | 0.0739 ms | 0.2156 ms  | 160.1563 | 54.6875 |       - | 988.96 KB |

### Staircase Traversal

Quite simple recursion exercise about counting the ways something (in this case climbing stairs jumping at most `x` steps at a time) can be done. This is useful exercise to try out optimizing recursion-based solutions. For now I'm benchmarking:
- Naive recursive solution as a baseline
- Recursive solution using memoization of sub-problem solutions

| Method                            | Staircase count  | Max step   |  Mean         | Error       | StdDev      |  Allocated |
| --------------------------------- | ---------------- | ---------- | ------------- | ----------- | ----------- | ---------- |
| BasicRecursiveImplementation      |  6               |  2         |      145.6 ns |     2.91 ns |     4.61 ns |          - |
| BasicRecursiveImplementation      |  15              |  5         |  132,985.4 ns | 2,539.87 ns | 3,302.55 ns |          - |
| StaircaseTraversal_Memoize        |  6               |  2         |      481.1 ns |     9.61 ns |    20.47 ns |      384 B |
| StaircaseTraversal_Memoize        |  15              |  5         |    2,068.8 ns |    40.73 ns |    51.51 ns |      776 B |

As the results clearly show memoization significantly reduces the execution time while not needing a lot of memory on the heap which is used to store previously calculated results.

### Group Anagrams

An exercise in which we have to, as the name states, group words which are anagrams of each other from a list given as a parameter.
For this exercise, I was interested in benchmarking if there is any significant difference between using a dictionary vs `GroupBy` LINQ method for implementing grouping anagrams with their sorted letters as the key.

| Method                        | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
| ----------------------------- | --------- | -------- | -------- | ------- | ------ | --------- |
| GroupAnagrams_UsingLinq       | 494.2 us  | 9.80 us  | 17.16 us | 36.1328 | 3.9063 | 226.14 KB |
| GroupAnagrams_UsingDictionary | 498.3 us  | 9.95 us  | 28.72 us | 31.2500 | 2.9297 | 196.96 KB |

As the results show, the difference is mostly visible in the amount of the allocated memory. That being said, ~13% improvement isn't insignificant, however it has to be weighted against code complexity and readability on a case by case basis.