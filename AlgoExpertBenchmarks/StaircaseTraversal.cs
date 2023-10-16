using BenchmarkDotNet.Attributes;

namespace AlgoExpertBenchmarks;

public class StaircaseTraversal
{
    private int StaircaseTraversalRecursive(int height, int maxSteps) {
        if (height == 0) return 1;
        int maxPossibleStep = Math.Min(maxSteps, height);
        int possibleCombinations = 0;
        for (int step = 1; step <= maxPossibleStep; step++)
        {
            possibleCombinations += StaircaseTraversalRecursive(height - step, maxSteps);
        }
        return possibleCombinations;
    }
    [Benchmark]
    public int BasicRecursiveImplementation_6_2()
    {
        return StaircaseTraversalRecursive(6, 2);
    }
    [Benchmark]
    public int BasicRecursiveImplementation_15_5()
    {
        return StaircaseTraversalRecursive(15, 5);
    }
}