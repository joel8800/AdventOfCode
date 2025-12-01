using Common;

Console.WriteLine("Day 01: Calorie Counting");

string[] input = InputReader.ReadBlocks("input.txt");

// make a list of elves and their total calories
List<int> elves = [];
foreach (string cals in input)
{
    int elfCals = cals.Split(Environment.NewLine).Select(int.Parse).Sum();
    elves.Add(elfCals);
}

int answerPt1 = elves.Max();

// ----------------------------------------------------------------------------

// find the top 3 elves with the most calories
elves.Sort();
List<int> top3 = [.. elves.TakeLast(3)];

int answerPt2 = top3.Sum();

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
