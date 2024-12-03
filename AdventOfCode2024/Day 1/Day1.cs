namespace AdventOfCode2024.Day_1;

public class Day1 : IDay
{
    private (int[], int[]) GetInput()
    {
        var columns = File.ReadLines(@"Day 1\Day1Input.txt")
            .Select(l => l.Split()
                                .Where(s => !string.IsNullOrEmpty(s))
                                .Select(int.Parse)
                                .ToArray()
            )
            .Select(c => (c.First(), c.Last()))
            .ToArray();
        
        return (columns.Select(c => c.Item1).ToArray(), columns.Select(c => c.Item2).ToArray());
    }
    
    public void Task1()
    {
        var (list1, list2) = GetInput();
        var list1Ordered = list1.OrderBy(x => x).ToArray();
        var answer = list2.OrderBy(x => x)
                              .Select((x, i) => (item1: x, item2: list1Ordered[i]))
                              .Sum(pair => Math.Abs(pair.item1 - pair.item2));
        
        Console.WriteLine($"The answer is {answer}");
    }

    public void Task2()
    {
        var (list1, list2) = GetInput();
        var answer = list1.Sum(x => x * list2.Count(y => y == x));
        
        Console.WriteLine($"The answer is {answer}");
    }
}