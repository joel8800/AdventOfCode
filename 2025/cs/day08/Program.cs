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

// calculate distances between each pair of junction boxes
// store the distances in a sorted list of tuples (i, j, distance)
List<(int i, int j, double d)> distList = [];
for (int i = 0; i < jBoxes.Count; i++)
{
    for (int j = i + 1; j < jBoxes.Count; j++)
    {
        double distance = GetDistance(jBoxes[i], jBoxes[j]);
        distList.Add((i, j, distance));
    }
}
List<(int i, int j, double d)> sortedByDist = [.. distList.OrderBy(b => b.d)];

// create a list of sets to hold connected junction boxes
List<HashSet<int>> circuits = [];

int boxesProcessed = 0;
for (int x = 0; x < sortedByDist.Count; x++)
{
    boxesProcessed++;

    int box1 = sortedByDist[x].i;
    int box2 = sortedByDist[x].j;
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

    // both boxes are unconnected, create new set and add to circuits
    if (box1set == -1 && box2set == -1)
    {
        HashSet<int> tmp = [box1, box2];
        circuits.Add(tmp);
    }

    // box1 not connected, box2 connected
    if (box1set == -1 && box2set != -1)
        circuits[box2set].Add(box1);

    // box1 connected, box2 not connected
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
    // if using inputSample, there are 20 junction boxes, and we check after 10 iterations
    if (boxesProcessed == 1000 || (jBoxes.Count == 20 && boxesProcessed == 10))
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