using Common;

Console.WriteLine("Day 01: Secret Entrance");

string[] input = InputReader.ReadLines("input.txt");

int position = 50;
int zeros = 0;
int crossings = 0;

foreach (string line in input)
{
    int startPos = position;
    bool left = line[0] == 'L' ? true : false;

    int clicks = Convert.ToInt32(line.Substring(1));
    int fullRotations = clicks / 100;
    clicks %= 100;

    if (left)
    {
        position -= clicks;

        if (position < 0)
        {
            position += 100;

            if (startPos != 0)
                crossings++;
        }
    }
    else
    {
        position += clicks;

        if (position > 100)
            crossings += 1;

        position %= 100;
    }

    if (position == 0)
        zeros += 1;
    crossings += fullRotations;

    //Console.WriteLine($"{line,-6}: val:{clicks,-4} rotations:{fullRotations,-4} crossings:{crossings,-4} zero:{zeros,-4} pos:{position,-4}");
}

int answerPt1 = zeros;

// ----------------------------------------------------------------------------

int answerPt2 = zeros + crossings;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
