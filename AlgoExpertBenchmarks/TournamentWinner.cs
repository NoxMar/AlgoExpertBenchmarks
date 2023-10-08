using System.Text;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AlgoExpertBenchmarks;

public class TournamentWinner
{
    private static Random _random = new Random(12345);

    private const string CharacterSource = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private const int StringLengthMin = 3;

    private const int StringLengthMax = 7;

    private const int TeamsTotal = 2_500;

    private const int WinnersTotal = 25_000;

    private readonly List<string> _teams = GenerateUniqueStrings(CharacterSource, StringLengthMin, StringLengthMax, TeamsTotal);

    private readonly List<string> _winners;
        
    public TournamentWinner()
    {
        _winners = Enumerable.Range(1, WinnersTotal)
            .Select(_ => _teams[_random.Next(_teams.Count)])
            .ToList();
    }

    private static List<string> GenerateUniqueStrings(string characterSource, int lenghtMinInclusive, int lenghtMaxInclusive, int resultCount)
    {
        StringBuilder currentString = new();
        HashSet<string> uniqueStrings = new();
        while (uniqueStrings.Count < resultCount)
        {
            currentString.Clear();
            for (int _ = 0; _ < _random.Next(lenghtMinInclusive, lenghtMaxInclusive + 1); _++)
            {
                currentString.Append(characterSource[_random.Next(characterSource.Length)]);
            }
            uniqueStrings.Add(currentString.ToString());
        }

        return uniqueStrings.ToList();
    }

    [Benchmark]
    public string UsingDictionary()
    {
        Dictionary<string, int> seenCounts = new();
        foreach (var winner in _winners)
        {
            seenCounts[winner] = seenCounts.GetValueOrDefault(winner, 0) + 1;
        }

        return seenCounts
            .Aggregate((kv1, kv2) => kv2.Value > kv1.Value ? kv2 : kv1)
            .Key;
    }
    
    [Benchmark]
    public string UsingGroupBy() => _winners
        .GroupBy(x => x)
        .Aggregate((g1, g2) => g2.Count() > g1.Count() ? g2 : g1)
        .Key;
}
