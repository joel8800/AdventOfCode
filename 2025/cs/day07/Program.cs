using Common;

Console.WriteLine("Day 07: Laboratories");

List<List<char>> input = InputReader.ReadFileToCharGrid("input.txt");

int splits = 0;
int rowCount = input.Count;
int colCount = input[0].Count;
(int r, int c) start = (0, input[0].IndexOf('S'));

HashSet<int> beams = [start.c];
List<int> beamColumns = [.. beams];

for (int row = 0; row < rowCount; row++)
{
    foreach (int beam in beamColumns)
    {
        if (input[row][beam] == '^')
        {
            splits++;
            beams.Add(beam - 1);
            beams.Add(beam + 1);
            beams.Remove(beam);
        }
    }
    beamColumns = [.. beams];
}

int answerPt1 = splits;

// ----------------------------------------------------------------------------

var timeLines = new long[colCount];
for (int row = 0; row < rowCount; row++)
{
    var tmpTimeLines = new long[colCount];
    for (int col = 0; col < colCount; col++)
    {
        // add timeLines for splits
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
        tmpTimeLines[start.c] = 1;
    
    timeLines = tmpTimeLines;
}

long answerPt2 = timeLines.Sum();

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
