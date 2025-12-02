using Common;

Console.WriteLine("Day 02: Gift Shop");

string input = InputReader.ReadAllText("input.txt");
string[] ranges = input.Split(',');
List<(long, long)> rangeList = [];

foreach (string range in ranges)
{
    string[] limits = range.Split('-');
    rangeList.Add((long.Parse(limits[0]), long.Parse(limits[1])));
}

long invalidIDsPt1 = 0;
long invalidIDsPt2 = 0;

foreach (var (loLim, hiLim) in rangeList)
{
    for (long idNum = loLim; idNum <= hiLim; idNum++)
    {
        if (CheckForRepeatingSequences(idNum, true))
            invalidIDsPt1 += idNum;
        if (CheckForRepeatingSequences(idNum, false))
            invalidIDsPt2 += idNum;
    }
}

long answerPt1 = invalidIDsPt1;

// ----------------------------------------------------------------------------

long answerPt2 = invalidIDsPt2;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

bool CheckForRepeatingSequences(long number, bool isPart1)
{
    string numStr = number.ToString();
    int maxSeqLen = numStr.Length / 2;

    if (isPart1)
    {
        // must be even length
        if (numStr.Length % 2 != 0)
            return false;

        string half1 = numStr[..(numStr.Length / 2)];
        string half2 = numStr[(numStr.Length / 2)..];

        return half1 == half2;
    }

    // start with largest possibile sequence and work down
    for (int i = maxSeqLen; i >= 1; i--)
    {
        // only process lengths that divide evenly
        if (numStr.Length % i != 0)
            continue;

        List<string> sequences = SplitStringIntoChunks(numStr, i);

        string firstSeq = sequences[0];
        if (sequences.All(s => s == firstSeq))
            return true;
    }

    return false;
}

// return a list of chunks of specified length
List<string> SplitStringIntoChunks(string str, int chunkSize)
{
    List<string> chunks = [];

    for (int i = 0; i < str.Length; i += chunkSize)
    {
        if (i + chunkSize <= str.Length)
            chunks.Add(str.Substring(i, chunkSize));
    }

    return chunks;
}