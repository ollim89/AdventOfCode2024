using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day_3;

public class Day3 : IDay
{
    private string GetInput() => File.ReadAllText(@"Day 3\Day3Input.txt");

    private readonly Regex _multiplyRegex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
    private readonly Regex _disabledSectionsRegex = new Regex(@"don't\(\).*?(?=do\(\))", RegexOptions.Singleline);
    
    public void Task1()
    {
        var input = GetInput();
        
        Console.WriteLine($"Sum: {RunCalculationsAndSum(input)}");
    }

    private int RunCalculationsAndSum(string input)
        => _multiplyRegex.Matches(input)
            .Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));

    public void Task2()
    {
        var input = GetInput();
        
        var disabledSectionsRemoved = _disabledSectionsRegex.Replace(input, string.Empty);
        
        Console.WriteLine($"Sum: {RunCalculationsAndSum(disabledSectionsRemoved)}");
    }
}