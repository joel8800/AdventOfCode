using Common;

Console.WriteLine("Day 02: Was Told There Would Be No Math");

string[] input = InputReader.ReadLines("input.txt");

int paper = 0;
int ribbon = 0;

foreach (string line in input)
{
    // split the dimensions and calculate face areas
    List<int> lwh = [.. line.Split('x').Select(s => Convert.ToInt32(s))];
    List<int> areas = [lwh[0] * lwh[1], lwh[1] * lwh[2], lwh[0] * lwh[2]];

    // l * w * h cubic feet
    int cuft = lwh.Aggregate(1, (acc, val) => acc * val);

    // get the two smallest dimensions    
    lwh.Sort();
    List<int> mins = [.. lwh.Take(2)];

    paper += areas.Sum() * 2 + (mins[0] * mins[1]);
    ribbon += (mins[0] * 2) + (mins[1] * 2) + cuft;
}

int answerPt1 = paper;

// ----------------------------------------------------------------------------

int answerPt2 = ribbon;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
