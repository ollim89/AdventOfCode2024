using System.Diagnostics;

namespace AdventOfCode2024.Day_15;

public class Day15 : IDay
{
    private (char[][] map, char[] movements) GetInput()
    {
        var lines = File.ReadLines(@"Day 15\Day15Input.txt").ToArray();

        var map = lines.TakeWhile(l => !string.IsNullOrEmpty(l))
            .Select(l => l.ToCharArray()).ToArray();

        var commands = lines.Skip(map.Length + 1).SelectMany(l => l.ToCharArray()).ToArray();

        return (map, commands);
    }
    
    public void Task1()
    {
        var (map, movements) = GetInput();
        ExecuteMovements(map, movements);
        var sum = SumBoxPositions(map);
        
        PrintMap(map);
        
        Console.Write($"The score is {sum}");
    }

    private void PrintMap(char[][] map)
    {
        foreach (var line in map)
        {
            Console.WriteLine(line);
        }
    }
    
    private int SumBoxPositions(char[][] map)
        => map.SelectMany((l, y) => l.Select((c, x) => (c: c, s: 100 * y + x))).Where(s => s.c == 'O').Sum(s => s.s);
    
    private void ExecuteMovements(char[][] map, char[] movements)
    {
        var (robotX, robotY) = GetRobotStartPosition(map);

        foreach (var movement in movements)
        {
            var (nextX, nextY) = GetNextPosition(movement, robotX, robotY);
            var objectOnNextPosition = map[nextY][nextX];
            
            if(objectOnNextPosition == '#')
                continue;

            if (objectOnNextPosition == 'O')
            {
                if(!MoveBoxes(map, movement, (nextX, nextY)))
                    continue;
            }
            
                        
            map[robotY][robotX] = '.';
            map[nextY][nextX] = '@';

            robotX = nextX;
            robotY = nextY;
        }
    }

    private bool MoveBoxes(char[][] map, char movement, (int x, int y) box)
    {
        var positionsAhead = GetPositionsAhead(map, movement, box.x, box.y);
        
        if(positionsAhead.All(p => p.c != '.'))
            return false;
        
        foreach (var (c, x, y) in positionsAhead.Skip(1))
            map[y][x] = 'O';

        return true;
    }

    private (char c, int x, int y)[] GetPositionsAhead(char[][] map, char movement, int boxX, int boxY)
    {
        var moveablePositions = new List<(char o, int x, int y)>();
        
        var currentObject = map[boxY][boxX];
        while (currentObject != '#')
        {
            moveablePositions.Add((currentObject, boxX, boxY));
            
            if(currentObject == '.')
                break;
            
            (boxX, boxY) = GetNextPosition(movement, boxX, boxY);
            currentObject = map[boxY][boxX];
        }

        return moveablePositions.ToArray();
    }

    private (int x, int y) GetNextPosition(char movement, int currentX, int currentY)
        => movement switch
        {
            '>' => (currentX + 1, currentY),
            '<' => (currentX - 1, currentY),
            '^' => (currentX, currentY - 1),
            'v' => (currentX, currentY + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(movement), movement, null)
        };

    private (int x, int y) GetRobotStartPosition(char[][] map)
    {
        foreach (var line in map.Index())
        {
            var robot = line.Item.Index().FirstOrDefault(c => c.Item == '@');
            if (robot != default)
                return (robot.Index, line.Index);
        }

        throw new UnreachableException("Robot not found");
    } 

    public void Task2()
    {
        throw new NotImplementedException();
    }
}