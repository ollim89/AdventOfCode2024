namespace AdventOfCode2024.Day_11;

public class Day11 : IDay
{
    private string[] GetInput()
    {
        return File.ReadAllText(@"Day 11\Day11Input.txt").Trim().Split();
    }
    
    public void Task1()
    {
        Console.WriteLine($"Number of stones {Blink(25)}");
    }

    private int Blink(int times)
    {
        var stones = GetInput();
        for (var i = 0; i < times; i++)
        {
            Console.WriteLine($"Running blink {i}");
            stones = stones.SelectMany(Blink).ToArray();
        }
        
        return stones.Length;
    }
    
    private string[] Blink(string stone)
    {
        if (stone == "0") return ["1"];

        if (stone.Length % 2 == 0)
        {
            var chunks = stone.Chunk(stone.Length / 2).Select(c => new string(c)). ToArray();
            return [chunks.First(), int.Parse(chunks.Last()).ToString()];
        }

        return [(long.Parse(stone) * 2024).ToString()];
    }

    public void Task2()
    {
        Console.WriteLine($"Number of stones {Blink(75)}");
    }
}