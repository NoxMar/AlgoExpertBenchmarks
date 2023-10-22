namespace AlgoExpertBenchmarks;

[MemoryDiagnoser]
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
    
    private int StaircaseTraversalMemoize(int height, int maxSteps)
    {
        Dictionary<int, int> remainingToWaysCount = new()
        {
            [0] = 1,
            [1] = 1,
        };
        return StaircaseTraversalMemoizeHelper(height, maxSteps);
        int StaircaseTraversalMemoizeHelper(int staircaseHeight, int maxStepsAtATime)
        {
            if (remainingToWaysCount.TryGetValue(staircaseHeight, out var result))
            {
                return result;
            }
            int maxPossibleSteps = Math.Min(staircaseHeight, maxStepsAtATime);
            int totalWays = 0;
            for (int steps = 1; steps <= maxPossibleSteps; steps++)
            {
                totalWays += remainingToWaysCount.GetValueOrDefault(
                    staircaseHeight - steps,
                    StaircaseTraversalMemoizeHelper(staircaseHeight - steps, maxStepsAtATime)
                );
            }
            remainingToWaysCount[staircaseHeight] = totalWays;
            return totalWays;
        }
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
    
    [Benchmark]
    public int StaircaseTraversal_Memoize_6_2()
    {
        return StaircaseTraversalMemoize(6, 2);
    }
    [Benchmark]
    public int StaircaseTraversal_Memoize_15_5()
    {
        return StaircaseTraversalMemoize(15, 5);
    }
}