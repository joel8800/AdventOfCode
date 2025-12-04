using Common;

Console.WriteLine("Day 03: Lobby");

string[] input = InputReader.ReadLines("inputSample.txt");

long answerPt1 = GetMaxJoltage(input, 2); ;

// ----------------------------------------------------------------------------

long answerPt2 = GetMaxJoltage(input, 12); ;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

long GetMaxJoltage(string[] allBanks, int numBatteries)
{
    long totalJoltage = 0;

    foreach (string bank in allBanks)
    {
        List<int> batteries = [.. bank.Select(c => int.Parse(c.ToString()))];
        List<int> indexes = [];

        int windowStart = 0;
        int windowEnd = bank.Length - numBatteries;
        int searchValue = 9;
        long bankJoltage = 0;

        while (indexes.Count < numBatteries)
        {
            // find max battery in current window
            List<int> window = [.. batteries.Skip(windowStart).Take(windowEnd - windowStart + 1)];
            int index = window.FindIndex(i => i == searchValue);

            if (index >= 0)
            {
                // found stronger battery, add to indexes
                indexes.Add(windowStart + index);
                bankJoltage = bankJoltage * 10 + batteries[windowStart + index];

                // adjust window and reset search value
                windowStart += index + 1;
                windowEnd = bank.Length - (numBatteries - indexes.Count);
                searchValue = 9;
            }
            else
            {
                // not found, decrease search value
                searchValue--;
            }
        }
        totalJoltage += bankJoltage;
    }

    return totalJoltage;
}
