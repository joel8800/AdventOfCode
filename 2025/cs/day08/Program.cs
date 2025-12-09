using Common;

Console.WriteLine("Day 08: Playground");

string[] input = InputReader.ReadLines("input.txt");

int answerPt1 = 0;
long answerPt2 = 0;

List<(int x, int y, int z)> jBoxes = [];
foreach (string line in input)
{
    List<int> coords = [.. line.Split(',').Select(int.Parse)];
    jBoxes.Add((coords[0], coords[1], coords[2]));
}

//// get distances between every junction box
//List<(int i, int j, double d)> distList = [];
//for (int i = 0; i < jBoxes.Count; i++)
//{
//    for (int j = i + 1; j < jBoxes.Count; j++)
//    {
//        double distance = GetDistance(jBoxes[i], jBoxes[j]);
//        distList.Add((i, j, distance));
//    }
//}
//List<(int i, int j, double d)> sorted = distList.OrderBy(b => b.d).ToList();


// get distances between every junction box
List<int> indexes = [.. Enumerable.Range(0, jBoxes.Count)];
var combinations = indexes
    .SelectMany((first, i) => indexes
    .Skip(i + 1)                            // avoid duplicates and self-pairing
    .Select(next => new {i = first, j = next, d = GetDistance(jBoxes[first], jBoxes[next])}))
    .OrderBy(x => x.d)
    .ToList();

List<HashSet<int>> circuits = [];

int linesProcessed = 0;
for (int x = 0; x < combinations.Count; x++)    //.Count; x++)
{
    linesProcessed++;

    int box1 = combinations[x].i;   // sorted[x].i;
    int box2 = combinations[x].j;   // sorted[x].j;
    int box1set = -1;
    int box2set = -1;

    // search sets for indexes
    for (int i = 0; i < circuits.Count; i++)
    {
        if (circuits[i].Contains(box1))
            box1set = i;
        if (circuits[i].Contains(box2))
            box2set = i;
    }

    // both boxes open, create new set and add to connected
    if (box1set == -1 && box2set == -1)
    {
        HashSet<int> tmp = [box1, box2];
        circuits.Add(tmp);
    }

    // box1 open, box2 connected
    if (box1set == -1 && box2set != -1)
        circuits[box2set].Add(box1);

    // box1 connected, box2 open
    if (box1set != -1 && box2set == -1)
        circuits[box1set].Add(box2);

    // both boxes connected, merge sets
    if (box1set != -1 && box2set != -1)
    {
        if (box1set == box2set)
            continue;

        int smaller = Math.Min(box1set, box2set);
        if (smaller == box1set)
        {
            circuits[box1set].UnionWith(circuits[box2set]);
            circuits.RemoveAt(box2set);
        }
        else
        {
            circuits[box2set].UnionWith(circuits[box1set]);
            circuits.RemoveAt(box1set);
        }
    }

    // part 1: get junction box count of top 3 circuits after 1000 iterations
    if (linesProcessed == 1000)
    {
        List<int> counts = [.. circuits.Select(c => c.Count)];        
        counts.Sort();
        answerPt1 = counts.TakeLast(3).Aggregate((a, b) => a * b);
    }

    // part 2: get x coordinates of the last pair connection that results in one circuit
    if (circuits[0].Count == jBoxes.Count)
    {
        long xOfBox1 = Convert.ToInt64(jBoxes[box1].x);
        long xOfBox2 = Convert.ToInt64(jBoxes[box2].x);
        answerPt2 = xOfBox1 * xOfBox2;
        break;
    }
}

// ----------------------------------------------------------------------------

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

static double GetDistance((int x, int y, int z) box1, (int x, int y, int z) box2)
{
    return Math.Sqrt(Math.Pow(box1.x - box2.x, 2) + Math.Pow(box1.y - box2.y, 2) + Math.Pow(box1.z - box2.z, 2));
}