using AdventOfCode2024;
using AdventOfCode2024.Day_1;
using AdventOfCode2024.Day_10;
using AdventOfCode2024.Day_2;
using AdventOfCode2024.Day_3;
using AdventOfCode2024.Day_4;
using AdventOfCode2024.Day_5;
using AdventOfCode2024.Day_6;

IDay[] days = [
    new Day1(),
    new Day2(),
    new Day3(),
    new Day4(),
    new Day5(),
    new Day6(),
    new Day10()
];

Console.WriteLine($"Which day would you like to run? Enter a number representing the day:");
for (var i = 0; i < days.Length; i++)
{
    Console.WriteLine($"{i+1}. {days[i].GetType().Name}");
}

var selectedKey = Console.ReadKey();
if (!char.IsNumber(selectedKey.KeyChar) || !int.TryParse(selectedKey.KeyChar.ToString(), out var selectedDay))
{
    Console.WriteLine("Invalid input, exiting...");
    return;
}

var day = days[selectedDay - 1];

Console.WriteLine($"Day {selectedDay} - Which task would you like to run? Enter a number representing the task:");
Console.WriteLine("1. Task 1");
Console.WriteLine("2. Task 2");

var selectedTaskKey = Console.ReadKey();
Console.WriteLine();

switch (selectedTaskKey.Key)
{
    case ConsoleKey.D1:
        day.Task1();
        break;
    case ConsoleKey.D2:
        day.Task2();
        break;
    default:
        Console.WriteLine("Invalid input, exiting...");
        break;
}