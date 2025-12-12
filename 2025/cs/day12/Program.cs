using Common;

Console.WriteLine("Day 12: Christmas Tree Farm");

string[] input = InputReader.ReadBlocks("input.txt");

List<int> shapeSizes = [];

// first six blocks are shapes
// only need to count number of '#' in each shape
for (int i = 0; i < 6; i++)
    shapeSizes.Add(input[i].Count(c => c == '#'));

// last block is regions
List<List<int>> regions = [];
string[] regionLines = input[^1].Split(Environment.NewLine);

for (int i = 0; i < regionLines.Length; i++)
{
    regionLines[i] = regionLines[i].Replace('x', ' ').Replace(":", "");
    regions.Add([.. regionLines[i].Split().Select(s => int.Parse(s))]);
}

// don't know how to fit pieces together to solve this
// input shows that some regions aren't big enough to fit all shapes
// while the rest have a lot of extra space
int fittable = 0;
foreach (var region in regions)
{
    int regionArea = region[0] * region[1];
    int shapeArea = 0;

    for (int i = 0; i < shapeSizes.Count; i++)
        shapeArea += shapeSizes[i] * region[i + 2];
    
    if (regionArea - shapeArea > 0)
        fittable++;
}

Console.WriteLine($"Part 1: {fittable}");

// ============================================================================
