namespace AdventOfCode2024.Day_2;

public class Day2 : IDay
{
    private int[][] GetInput()
        => File.ReadAllLines(@"Day 2\Day2Input.txt")
                   .Select(l => l.Split().Select(int.Parse).ToArray())
                   .ToArray();
    
    public void Task1()
    {
        var reports = GetInput();

        var safeReports = reports.Count(IsSafeReport);
        Console.WriteLine($"Number of safe reports: {safeReports}");
    }

    public void Task2()
    {
        var reports = GetInput();
        
        var safeReports = reports.Count(WithDampening);
        Console.WriteLine($"Number of safe reports: {safeReports}");
    }

    private bool WithDampening(int[] levels)
    {
        if (IsSafeReport(levels))
            return true;

        for (var i = 0; i < levels.Length; i++)
        {
            var levelsDampened = levels.Take(i).Concat(levels.Skip(i + 1)).ToArray();
            if (IsSafeReport(levelsDampened))
                return true;
        }
        
        return false;
    }

    private bool IsSafeReport(int[] levels)
    {
        var last = levels.Length - 1;
    
        var ascendingValid = true;
        var descendingValid = true;
        
        for (var i = 0; i < last; i++)
        {
            var currentFromStart = levels[i];
            var nextFromStart = levels[i + 1];
            
            if(ascendingValid &&  nextFromStart < currentFromStart)
                ascendingValid = false;

            var currentFromEnd = levels[last - i];
            var nextFromEnd = levels[last - i - 1];
            if (descendingValid &&  nextFromEnd < currentFromEnd)
                descendingValid = false;
            
            if (Math.Abs(currentFromStart - nextFromStart) is < 1 or > 3 || (!ascendingValid && !descendingValid))
                return false;
        }
        
        return true;
    }
}