namespace AdventOfCode2024.Day_11;

public class Day11 : IDay
{
    private Dictionary<long, long> GetInput()
    {
        return File.ReadAllText(@"Day 11\Day11Input.txt").Trim().Split().Select(long.Parse).ToDictionary(s => s, s => 1l);
    }
    
    public void Task1()
    {
        Console.WriteLine($"Number of stones {Blink(25)}");
    }

    private long Blink(int times)
    {
        var stones = GetInput();
        var splits = new Dictionary<long, (long, long)>();
        
        for (var i = 0; i < times; i++)
        {
            stones = Blink(stones, splits);
        }
        
        return stones.Values.Sum();
    }
    
    private Dictionary<long, long> Blink(Dictionary<long, long> stones, Dictionary<long, (long, long)> splits)
    {
        var newStones = new Dictionary<long, long>();
        
        if (stones.TryGetValue(0, out var zeroCount))
        {
            AddOrIncreaseStone(1, zeroCount, newStones);
        }
        
        foreach(var stone in stones.Where(s => s.Key != 0))
        {
            if(splits.TryGetValue(stone.Key, out var split))
            {
                AddOrIncreaseStone(split.Item1, stone.Value, newStones);
                AddOrIncreaseStone(split.Item2, stone.Value, newStones);
            }
            else if (stone.Key.ToString().Length % 2 == 0)
            {
                var strStone = stone.Key.ToString();
                var chunks = strStone.Chunk(strStone.Length / 2).Select(c => new string(c)).ToArray();
                var newSplits = (long.Parse(chunks.First()), long.Parse(chunks.Last()));
                
                AddOrIncreaseStone(newSplits.Item1, stone.Value, newStones);
                AddOrIncreaseStone(newSplits.Item2, stone.Value, newStones);
                splits[stone.Key] = newSplits;
            }
            else
            {
                AddOrIncreaseStone(stone.Key * 2024, stone.Value, newStones);
            }
        }

        return newStones;
    }

    private void AddOrIncreaseStone(long stone, long addToCount, Dictionary<long, long> newStones)
    {
        var count = newStones.TryGetValue(stone, out var currentCount);
        newStones[stone] = currentCount + addToCount;
    }

    public void Task2()
    {
        Console.WriteLine($"Number of stones {Blink(75)}");
    }
}