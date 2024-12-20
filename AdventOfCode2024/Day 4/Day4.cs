﻿namespace AdventOfCode2024.Day_4;

public class Day4 : IDay
{
    private char[][] GetInput() =>
        File
            .ReadAllLines(@"Day 4\Day4Input.txt")
            .Where(l => !string.IsNullOrEmpty(l))
            .Select(l => l.ToCharArray())
            .ToArray();

    public void Task1()
    {
        var input = GetInput();

        var nrOfXmas = input
            .Select((l, y) => l.Select((c, x) => NumberOfXmasesOnCell(x, y, input)).Sum(c => c))
            .Sum(r => r);

        Console.WriteLine($"Found {nrOfXmas} Xmases");
    }

    private int NumberOfXmasesOnRow(char[] lettersOfRow, int y, char[][] input)
    {
        return lettersOfRow.Select((c, x) => NumberOfXmasesOnCell(x, y, input)).Sum(c => c);
    }

    private int NumberOfXmasesOnCell(int x, int y, char[][] input)
    {
        var columns = input[0].Length;
        var rows = input.Length;
        var withinBounds = GetPossibleEndOfWordCoordinates(x, y)
            .Where(c => c.x >= 0 && c.x < columns && c.y >= 0 && c.y < rows)
            .ToArray();

        var foundXmases = 0;
        foreach (var (xEnd, yEnd) in withinBounds)
        {
            var word = new char[4];

            var xDirection = xEnd == x ? 0 : xEnd > x ? 1 : -1;
            var yDirection = yEnd == y ? 0 : yEnd > y ? 1 : -1;

            for (int i = 0; i < 4; i++)
            {
                word[i] = input[y + (i * yDirection)][x + (i * xDirection)];
            }

            var wordString = new string(word);
            if (wordString == "XMAS") foundXmases++;
        }

        return foundXmases;
    }

    private (int x, int y)[] GetPossibleEndOfWordCoordinates(int x, int y) =>
    [
        (x: x - 3, y), //Left
        (x: x + 3, y), //Right
        (x, y: y - 3), //Up
        (x, y: y + 3), // Down
        (x: x - 3, y: y - 3), //Diagonal left up
        (x: x + 3, y: y + 3), //Diagonal right down
        (x: x - 3, y: y + 3), //Diagonal left down
        (x: x + 3, y: y - 3), //Diagonal right up
    ];

    public void Task2()
    {
        var input = GetInput();
        
        var nrOfCrossMases = NumberOfCrossMasesOnCell(input);

        Console.WriteLine($"Found {nrOfCrossMases} X-mases");
    }

    private int NumberOfCrossMasesOnCell(char[][] input)
    {
        var columns = input[0].Length;
        var rows = input.Length;

        var foundXmases = 0;
        for(var y = 1; y < rows - 1; y++)
        {
            for(var x = 1; x < columns - 1; x++)
            {
                var letter = input[y][x];
                
                if(letter != 'A') continue;
                
                var diagonalLeft = new string([input[y - 1][x - 1], input[y][x], input[y + 1][x + 1]]);
                var diagonalRight = new string([input[y + 1][x - 1], input[y][x], input[y - 1][x + 1]]);
                
                if(diagonalLeft is "MAS" or "SAM" && diagonalRight is "MAS" or "SAM")
                {
                    foundXmases++;
                }
            }
        }

        return foundXmases;
    }
}