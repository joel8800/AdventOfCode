using Common;

Console.WriteLine("Day 05: Cafeteria");

string[] input = InputReader.ReadBlocks("input.txt");
string[] rangeLines = input[0].Split(Environment.NewLine);

// parse ingredient ID ranges
List<(long start, long end)> ranges = [];
foreach (string line in rangeLines)
{
    string[] parts = line.Split('-');
    ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
}

// parse ingredient IDs
List<long> ingredients = [.. input[1].Split(Environment.NewLine).Select(line => long.Parse(line.Trim()))];

int fresh = 0;

// check if each ingredient is in any of the ranges
foreach (long ingredient in ingredients)
{
    foreach ((long start, long end) in ranges)
    {
        if (ingredient >= start && ingredient <= end)
        {
            fresh++;
            break;
        }
    }
}

int answerPt1 = fresh;

// ----------------------------------------------------------------------------


// sort ranges by start value
List<(long start, long end)> mergedRanges = [.. ranges.OrderBy(r => r.start)];

// merge overlapping ranges
for (int i  = 0; i < mergedRanges.Count - 1;  i++)
{
    for (int j = i + 1; j < mergedRanges.Count; j++)
    {
        (long start, long end) r1 = mergedRanges[i];
        (long start, long end) r2 = mergedRanges[j];
        
        if (r1.end >= r2.start)
        {
            long newStart = Math.Min(r1.start, r2.start);
            long newEnd = Math.Max(r1.end, r2.end);
            
            mergedRanges[i] = (newStart, newEnd);
            mergedRanges.RemoveAt(j);
            j--;
        }
    }
}

long consideredFresh = 0;

// get total number of IDs in all ranges
foreach ((long start, long end) in mergedRanges)
    consideredFresh += (end - start + 1);

long answerPt2 = consideredFresh;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
