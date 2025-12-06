using Common;
using System.Numerics;

Console.WriteLine("Day 03: Perfectly Spherical Houses in a Vacuum");

string input = File.ReadAllText("input.txt");

(int x, int y) santa = (0, 0);

HashSet<(int, int)> houses = [santa];

foreach (char c in input)
{
    if (c == '^') santa.y++;
    if (c == 'v') santa.y--;
    if (c == '>') santa.x++;
    if (c == '<') santa.x--;

    houses.Add(santa);
}

int answerPt1 = houses.Count;

// ----------------------------------------------------------------------------

santa = (0, 0);
(int x, int y) robot = (0, 0);

HashSet<(int, int)> sHouses = [santa];
HashSet<(int, int)> rHouses = [robot];

int turn = 0;
foreach (char c in input)
{
    if (turn % 2 == 0)
    {
        if (c == '^') santa.y++;
        if (c == 'v') santa.y--;
        if (c == '>') santa.x++;
        if (c == '<') santa.x--;
        sHouses.Add(santa);
    }
    else
    {
        if (c == '^') robot.y++;
        if (c == 'v') robot.y--;
        if (c == '>') robot.x++;
        if (c == '<') robot.x--;
        rHouses.Add(robot);
    }

    turn++;
}

// merge the two sets
sHouses.UnionWith(rHouses);

int answerPt2 = sHouses.Count;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
