using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day_1;

public class Day1 : IDay
{
    private (int[], int[]) GetInput()
    {
        var regeExSplit = new Regex(@"(\s+)");
        
        var list1 = new List<int>();
        var list2 = new List<int>();

        foreach (var line in File.ReadLines(@"Day 1\Day1Input.txt"))
        {
            if(string.IsNullOrEmpty(line))
                continue;
            
            var values = regeExSplit.Split(line);
            
            list1.Add(int.Parse(values[0]));
            list2.Add(int.Parse(values[2]));
        }
        
        return (list1.ToArray(), list2.ToArray());
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