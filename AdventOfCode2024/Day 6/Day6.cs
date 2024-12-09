using System.Collections;

namespace AdventOfCode2024.Day_6;

public class Day6 : IDay
{
    private char[][] GetInput()
    {
        return File.ReadAllLines(@"Day 6\Day6Input.txt")
            .Where(l => !string.IsNullOrEmpty(l))
            .Select(l => l.ToCharArray())
            .ToArray();
    }

    private readonly char[] _guard = ['v', '<', '^', '>'];

    public void Task1()
    {
        var grid = GetInput();

        var distinctPositionsOfGuard = GetDistinctGuardPositions(grid).Count();

        Console.WriteLine($"Positions visited {distinctPositionsOfGuard}");
    }

    private (int x, int y, bool loopDetected)[] GetDistinctGuardPositions(char[][] grid)
    {
        var width = grid[0].Length;
        var height = grid.Length;

        var (gX, gY, dir) = GetGuardStartPosition(grid);

        var positionsVisited = new List<(int x, int y, char dir)>
        {
            (gX, gY, dir)
        };

        int nX, nY;
        var loopDetected = false;
        while (((nX, nY) = MoveGuard(gX, gY, dir)).nX >= 0 && nY >= 0 && nX < width && nY < height)
        {
            if (IsObstacle(nX, nY, grid))
            {
                dir = NextDirection(dir);
                continue;
            }

            gX = nX;
            gY = nY;

            if (positionsVisited.Any(p => p.x == gX && p.y == gY && p.dir == dir))
            {
                loopDetected = true;
                break;
            }

            positionsVisited.Add((gX, gY, dir));
        }

        return positionsVisited
            .Select(p => (p.x, p.y, loopDetected && p.x == gX && p.y == gY)).Distinct().ToArray();
    }

    private (int x, int y, char dir) GetGuardStartPosition(char[][] grid)
    {
        return grid
            .SelectMany((l, y) => l.Select((c, x) => (x, y, guard: _guard.FirstOrDefault(g => g == c))))
            .Single(c => c.guard != default);
    }

    private char NextDirection(char currentDirection)
    {
        var index = Array.IndexOf(_guard, currentDirection);
        return index == _guard.Length - 1 ? _guard[0] : _guard[index + 1];
    }

    private bool IsObstacle(int x, int y, char[][] grid)
    {
        return grid[y][x] == '#';
    }

    private (int x, int y) MoveGuard(int x, int y, char dir)
    {
        return dir switch
        {
            '^' => (x, y - 1),
            'v' => (x, y + 1),
            '<' => (x - 1, y),
            '>' => (x + 1, y),
            _ => throw new NotImplementedException()
        };
    }

    public void Task2()
    {
        var grid = GetInput();

        var (sX, sY, sDir) = GetGuardStartPosition(grid);
        var guardPositionsExcludingStart = GetDistinctGuardPositions(grid).Skip(1).ToArray();
        var numberOfLoops = 0;
        var totalCount = guardPositionsExcludingStart.Length;
        for (var i = 0; i < totalCount; i++)
        {
            var (pX, pY, pDir) = guardPositionsExcludingStart[i];
            grid[pY][pX] = '#';
            var newPositions = GetDistinctGuardPositions(grid);
            if (newPositions.Any(p => p.loopDetected))
                numberOfLoops++;
            grid[pY][pX] = '.';
            Console.Write("\rProcessed {0}/{1} positions and found {2} loops   ", i + 1, totalCount, numberOfLoops);
        }
    }
}