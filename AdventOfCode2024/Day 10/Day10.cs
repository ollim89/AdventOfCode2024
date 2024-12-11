namespace AdventOfCode2024.Day_10;

public class Day10 : IDay
{
    private (int[] positions, int rowLength) GetInput()
    {
        var rows = File.ReadAllLines(@"Day 10\Day10Input.txt");
        var rowLength = rows[0].Length;

        return (rows.SelectMany(r => r.Select(c => (int)char.GetNumericValue(c))).ToArray(), rowLength);
    }
    
    public void Task1()
    {
        var (positions, rowLength) = GetInput();

        var trailheadScores = positions
            .Select((p, i) => p != 0 ? 0 : GetTrailEndIndexes(i, rowLength, positions).Distinct().Count())
            .Sum();
        
        Console.WriteLine($"The sum of all trailhead scores is {trailheadScores}");
    }

    private int[] GetTrailEndIndexes(int index, int rowLength, int[] positions)
    {
        var position = positions[index];
        if (position == 9)
            return [index];
        
        var (left, above, right, below) = GetAdjacentIndexes(index, rowLength);

        return new int[][]
        {
            [left, TryGetPosition(positions, left)],
            [above, TryGetPosition(positions, above)],
            [right, TryGetPosition(positions, right)],
            [below, TryGetPosition(positions, below)]
        }
        .Where(p => p[1] == position + 1)
        .SelectMany(p => GetTrailEndIndexes(p[0], rowLength, positions))
        .ToArray();
    }
    
    private static int TryGetPosition(int[] array, int index) => index >= 0 && index < array.Length ? array[index] : -1;

    private (int left, int above, int right, int below) GetAdjacentIndexes(int index, int rowLength)
    {
        var onEdgeRemainder = index % rowLength;
        
        var left = onEdgeRemainder == 0 ? -1 : index - 1;
        var above = index - rowLength;
        var right = onEdgeRemainder == rowLength - 1 ? -1 : index + 1;
        var below = index + rowLength;

        return (left, above, right, below);
    }

    public void Task2()
    {
        var (positions, rowLength) = GetInput();

        var trailheadScores = positions
            .Select((p, i) => p != 0 ? 0 : GetTrailEndIndexes(i, rowLength, positions).Length)
            .Sum();
        
        Console.WriteLine($"The sum of all trailhead scores is {trailheadScores}");
    }
};