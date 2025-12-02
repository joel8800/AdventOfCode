using Common;

Console.WriteLine("Day 01: Secret Entrance");

string[] input = InputReader.ReadLines("input.txt");

int position = 50;
int zerosSeen = 0;
int endAtZero = 0;

foreach (string line in input)
{
    int dir = line[0] == 'L' ? -1 : 1;
    int clicks = Convert.ToInt32(line[1..]);

    // simulate dial turning
    for (int i = 0; i < clicks; i++)
    {
        position = (position + dir + 100) % 100;

        if (position == 0)
            zerosSeen++;
    }

    if (position == 0)
        endAtZero += 1;

    //Console.WriteLine($"{line,-6}: clicks:{clicks * dir,4}: position:{position, 4}");
}

int answerPt1 = endAtZero;

// ----------------------------------------------------------------------------

int answerPt2 = zerosSeen;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
