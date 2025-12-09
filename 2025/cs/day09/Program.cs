using Common;

Console.WriteLine("Day 09: Movie Theater");

string[] input = InputReader.ReadLines("input.txt");

List<(int x, int y)> redTiles = [];
foreach (var line in input)
{
    List<int> xy = [.. line.Split(',').Select(i => int.Parse(i))];
    redTiles.Add((xy[0], xy[1]));
}

// find all combinations of pairs of red tiles and 
// calculate the area of a rectangle created by them
List<(int i, int j, long a)> areaList = [];
for (int i = 0; i < redTiles.Count; i++)
    for (int j = i + 1; j < redTiles.Count; j++)
        areaList.Add((i, j, GetArea(redTiles[i], redTiles[j])));

long answerPt1 = areaList.Select(t => t.a).Max();

// ----------------------------------------------------------------------------

int answerPt2 = 0;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

long GetArea((int x, int y) p1, (int x, int y) p2)
{
    int dx = Math.Abs(p1.x - p2.x) + 1;
    int dy = Math.Abs(p1.y - p2.y) + 1;
    return Convert.ToInt64(dx) * Convert.ToInt64(dy);
}