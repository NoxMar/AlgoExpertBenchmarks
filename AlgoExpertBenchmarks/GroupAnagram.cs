namespace AlgoExpertBenchmarks;

[MemoryDiagnoser]
public class GroupAnagrams
{
    private const int UniqueAnagramCount = 100;
    private const int WordLengthMin = 5;
    private const int WordLengthMax = 25;
    private const int MaxAnagramRepeats = 25;

    private static readonly Random Random = new(12345);
    private const string CharacterSource = "abcdefghijklmnopqrstuvwxyz";

    private static readonly List<string> Words = GenerateWords();

    private static List<string> GenerateWords()
    {
        var anagrams = Enumerable.Range(0, UniqueAnagramCount)
            .Select(_ => Random.Next(WordLengthMin, WordLengthMax))
            .Select(length =>
            {
                var wordChars = Enumerable
                    .Range(0, length)
                    .Select(_ => CharacterSource[Random.Next(CharacterSource.Length)])
                    .ToArray();
                return new string(wordChars);
            }).ToHashSet();
        var result = new List<string>();
        foreach (var anagram in anagrams)
        {
            for (var repeat = Random.Next(1, MaxAnagramRepeats); repeat >= 0; repeat--)
            {
                result.Add(new string(
                    anagram
                        .OrderBy(_ => Random.Next())
                        .ToArray())
                );
            }
        }
        return result.OrderBy(_ => Random.Next()).ToList();
    }
    
    [Benchmark]
#pragma warning disable CA1822
    public List<List<string>> GroupAnagrams_UsingLinq() 
        => Words.GroupBy(SortString,
            (_, anagrams) => anagrams.ToList())
        .ToList();
#pragma warning restore CA1822
    
    private static string SortString(string str)
    {
        var sortedChars = str.ToCharArray();
        Array.Sort(sortedChars);
        return new string(sortedChars);
    }
}

