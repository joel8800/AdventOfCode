using Common;

Console.WriteLine("Day 10: Factory");

string[] input = InputReader.ReadLines("input.txt");

List<int> lights = [];
List<int> counters = [];
List<List<int>> buttons = [];

int minPressesPt1 = 0;
int minPressesPt2 = 0;

// TODO: My part1 solution seems needlessly complex.
// Try converting light panel and button press combinations to single integers as bit fields
// this should make comparisons easier

foreach (string line in input)
{
    string[] parts = line.Split(' ');

    // Process light panel
    string lightPanel = parts[0];
    lights = [.. lightPanel[1..^1].Select(c => c == '.' ? 0 : 1)];

    buttons.Clear();
    // Process each button
    for (int i = 1; i < parts.Length - 1; i++)
    {

        string buttonStr = parts[i][1..^1];
        List<int> button = [.. buttonStr.Split(',').Select(int.Parse)];
        buttons.Add(button);
    }

    // Process counters
    string counterPanel = parts[^1];
    counters = [.. counterPanel[1..^1].Split(',').Select(c => int.Parse(c.ToString()))];

    //PrintPanel(counters);

    // brute force all combinations of button presses for part 1
    int minPress = int.MaxValue;
    for (int i = 0; i < Convert.ToInt32(Math.Pow(2, buttons.Count)); i++)
    {
        int retVal = Math.Min(minPress, PressButtonSequence(lights, buttons, i));
        minPress = Math.Min(retVal, minPress);
    }
    minPressesPt1 += minPress;

    // part 2
    
}

int answerPt1 = minPressesPt1;

// ----------------------------------------------------------------------------

int answerPt2 = minPressesPt2;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

void PrintPanel(List<int> panel)
{
    Console.WriteLine(string.Join(",", panel.Select(n => n)));
}

List<int> PressButton(List<int> lights, List<int> button)
{
    List<int> newLights = [.. lights];
    foreach (int index in button)
    {
        newLights[index] = 1 - newLights[index]; // Toggle light
    }
    return newLights;
}

int PressButtonSequence(List<int> lights, List<List<int>> buttons, int value)
{
    List<int> bitField = [];
    List<int> newLights = [.. Enumerable.Repeat(0, lights.Count)];

    // create bit field
    for (int i = 0; i < buttons.Count; i++)
    {
        bitField.Add((value & (1 << i)) != 0 ? 1 : 0);
    }

    // walk bit field
    int numPresses = 0;
    for (int i = 0; i < bitField.Count; i++)
    {
        if (bitField[i] == 1)
        {
            newLights = PressButton(newLights, buttons[i]);
            numPresses++;
        }
    }

    if (lights.SequenceEqual(newLights))
    {
        return numPresses;
    }

    return buttons.Count * 100;
}