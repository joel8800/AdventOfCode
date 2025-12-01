using Common;
using System.Diagnostics;

Console.WriteLine("Day 02: Dive!");

string[] input = InputReader.ReadLines("input.txt");

int depth = 0;
int distance = 0;

foreach (string line in input)
{
    string[] words = line.Split(' ');
    int units = Convert.ToInt32(words[1]);

    switch (words[0])
    {
        case "forward":
            distance += units;
            break;
        case "down":
            depth += units;
            break;
        case "up":
            depth -= units;
            break;
    }
}

int answerPt1 = distance * depth;

// ----------------------------------------------------------------------------

int aim = 0;
depth = 0;
distance = 0;

foreach (string line in input)
{
    string[] words = line.Split(' ');
    int units = Convert.ToInt32(words[1]);

    switch (words[0])
    {
        case "forward":
            distance += units;
            depth += (aim * units);
            break;
        case "down":
            aim += units;
            break;
        case "up":
            aim -= units;
            break;
    }
}

int answerPt2 = distance * depth;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
