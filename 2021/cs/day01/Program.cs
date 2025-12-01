using Common;

Console.WriteLine("Day 01: Sonar Sweep");

List<int> depths = [.. InputReader.ReadInts("input.txt")];

// get the difference between successive depths
List<int> changes = [.. depths.Zip(depths.Skip(1), (a, b) => b - a)];

// count the number of positive changes
int answerPt1 = changes.Where(change => change > 0).Count();

// ----------------------------------------------------------------------------

// create a list of 3-measurement sliding window sums
List<int> sums3;
sums3 = [.. depths.Zip(depths.Skip(1), (a, b) => a + b)];
sums3 = [.. sums3.Zip(depths.Skip(2), (a, b) => a + b)];

// get the difference between successive window sums
changes = [.. sums3.Zip(sums3.Skip(1), (a, b) => b - a)];

int answerPt2 = changes.Where(change => change > 0).Count();

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
