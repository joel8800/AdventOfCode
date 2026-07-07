using Common;

Console.WriteLine("Day 05: Cafeteria");

string[] input = InputReader.ReadBlocks("input.txt");

// parse ingredient ID ranges
List<(long start, long end)> ranges = [.. input[0].Split(Environment.NewLine).Select(line => {
    string[] parts = line.Split('-');
    return (long.Parse(parts[0]), long.Parse(parts[1]));
})];

// parse ingredient IDs
List<long> ingredients = [.. input[1].Split(Environment.NewLine).Select(line => long.Parse(line.Trim()))];

int fresh = 0;

// check if each ingredient is in any of the ranges
foreach (long ingredient in ingredients)
{
    if (ranges.Any(r => ingredient >= r.start && ingredient <= r.end))
        fresh++;
}

int answerPt1 = fresh;

// ----------------------------------------------------------------------------

// sort ranges by start value
List<(long start, long end)> sortedRanges = [.. ranges.OrderBy(r => r.start)];
List<(long start, long end)> mergedRanges = [];

// add ranges to mergedRanges list, if they overlap, extend the last range in mergedRanges
foreach ((long start, long end) range in sortedRanges)
{
    if (mergedRanges.Count == 0 || range.start > mergedRanges[^1].end)
        mergedRanges.Add(range);
    else
    {
        // set the end of range to the largest value of the two overlapping ranges
        var (start, end) = mergedRanges[^1];
        mergedRanges[^1] = (start, Math.Max(range.end, end));
    }
}

// get total number of IDs in all ranges
long answerPt2 = mergedRanges.Sum(r => r.end - r.start + 1);

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
