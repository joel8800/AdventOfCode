using Common;

Console.WriteLine("Day 07: Laboratories");

List<List<char>> input = InputReader.ReadFileToCharGrid("input.txt");

int splits = 0;
int rowCount = input.Count;
int colCount = input[0].Count;
int start = input[0].IndexOf('S');

HashSet<int> beams = [start];
List<int> beamColumns = [.. beams];

for (int row = 0; row < rowCount; row++)
{
    foreach (int beam in beamColumns)
    {
        if (input[row][beam] == '^')
        {
            splits++;
            beams.Add(beam - 1);    // split left
            beams.Add(beam + 1);    // split right
            beams.Remove(beam);     // remove the original beam
        }
    }
    beamColumns = [.. beams];
}

int answerPt1 = splits;

// ----------------------------------------------------------------------------

List<long> timeLines = [.. Enumerable.Repeat(0, colCount)];
for (int row = 0; row < rowCount; row++)
{
    // create temporary time lines list to handle splits
    List<long> tmpTimeLines = [.. Enumerable.Repeat(0, colCount)];
    for (int col = 0; col < colCount; col++)
    {
        // add left and right timelines at splitters
        // otherwise beam continues
        if (input[row][col] == '^')
        {
            if (col > 0)
                tmpTimeLines[col - 1] += timeLines[col];

            if (col < colCount - 1)
                tmpTimeLines[col + 1] += timeLines[col];
        }
        else
            tmpTimeLines[col] += timeLines[col];
    }
    if (row == 0)
        tmpTimeLines[start] = 1;

    // update main timeLines list
    timeLines = tmpTimeLines;
}

long answerPt2 = timeLines.Sum();

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
