using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day_5;

public class Day5 : IDay
{
    private (Dictionary<int, List<int>> rules, int[][] updates) GetInput()
    {
        var lines = File.ReadAllLines(@"Day 5\Day5Input.txt");
        var rules = lines
            .TakeWhile(l => !string.IsNullOrEmpty(l))
            .Select(l => l.Split("|").Select(int.Parse).ToArray())
            .Aggregate(new Dictionary<int, List<int>>(), (acc, r) =>
            {
                if (!acc.TryGetValue(r[0], out var successors))
                {
                    successors = new List<int>();
                    acc.Add(r[0], successors);
                }

                successors.Add(r[1]);

                return acc;
            });

        var updates = lines
            .SkipWhile(l => !string.IsNullOrEmpty(l))
            .Skip(1)
            .TakeWhile(l => !string.IsNullOrEmpty(l))
            .Select(l => l.Split(",").Select(int.Parse).ToArray())
            .ToArray();

        return (rules, updates);
    }

    public void Task1()
    {
        var (rules, updates) = GetInput();

        var sumOfValidMiddlePages =
            updates.Where(u => IsValidUpdate(u, rules)).Sum(GetMiddlePage);

        Console.WriteLine($"Sum of valid middle pages: {sumOfValidMiddlePages}");
    }

    private int GetMiddlePage(int[] update)
    {
        return update[(int)Math.Floor(update.Length / 2f)];
    }

    private bool IsValidUpdate(int[] update, Dictionary<int, List<int>> rules)
    {
        for (var i = 0; i < update.Length; i++)
        {
            var page = update[i];
            if (!rules.TryGetValue(page, out var pageRules))
                continue;

            if (update.Take(i).Any(p => pageRules.Contains(p))) return false;
        }

        return true;
    }

    public void Task2()
    {
        var (rules, updates) = GetInput();

        var sumOfValidMiddlePages = updates.Where(u => !IsValidUpdate(u, rules))
            .Select(u => ReOrderUpdate(u, rules))
            .Sum(GetMiddlePage);

        Console.WriteLine($"Sum of corrected middle pages: {sumOfValidMiddlePages}");
    }

    private int[] ReOrderUpdate(int[] update, Dictionary<int, List<int>> rules)
    {
        for (var i = 0; i < update.Length - 1; i++)
        for (var j = 0; j < update.Length - i - 1; j++)
        {
            var page = update[j];
            var pageCompared = update[j + 1];
            if (!rules.TryGetValue(pageCompared, out var comparedPageRules))
                continue;

            if (comparedPageRules.Contains(page))
                (update[j], update[j + 1]) = (update[j + 1], update[j]);
        }

        return update;
    }
}