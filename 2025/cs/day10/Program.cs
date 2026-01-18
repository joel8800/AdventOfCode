using Microsoft.Z3;
using Common;

Console.WriteLine("Day 10: Factory");

string[] input = InputReader.ReadLines("input.txt");

List<List<int>> counters = [];
List<List<List<int>>> buttons = [];

int minPressesPt1 = 0;
int minPressesPt2 = 0;

// TODO: My part1 solution seems needlessly complex.
// Try converting light panel and button press combinations to single integers as bit fields
// this should make comparisons easier

foreach (string line in input)
{
    string[] parts = line.Split(' ');

    // Process light panel
    List<int> lights = [.. parts[0][1..^1].Select(c => c == '.' ? 0 : 1)];

    // Process each button panel, save for part 2
    List<List<int>> buttonPanel = [];
    for (int i = 1; i < parts.Length - 1; i++)
    {
        List<int> button = [.. parts[i][1..^1].Split(',').Select(int.Parse)];
        buttonPanel.Add(button);
    }
    buttons.Add(buttonPanel);

    // Process counters for part 2
    List<int> counterPanel = [.. parts[^1][1..^1].Split(',').Select(c => int.Parse(c.ToString()))];
    counters.Add(counterPanel);

    // part 1: brute force all combinations of button presses
    int minPress = int.MaxValue;
    for (int i = 0; i < Convert.ToInt32(Math.Pow(2, buttonPanel.Count)); i++)
        minPress = Math.Min(minPress, Math.Min(minPress, PressButtonSequence(lights, buttonPanel, i)));
    
    minPressesPt1 += minPress;    
}

int answerPt1 = minPressesPt1;

// ----------------------------------------------------------------------------

// I couldn't solve this on my own. Consulted Reddit and found a Z3-based solution

for (int i = 0; i < counters.Count; i++)
{
    int[] target = [.. counters[i]];
    var joltageButtons = buttons[i];    //buttonsPt2[i];

    int counterCount = target.Length;
    int buttonCount = joltageButtons.Count;

    using (var ctx = new Context())
    {
        Optimize opt = ctx.MkOptimize();

        IntExpr[] x = new IntExpr[buttonCount];

        for (int b = 0; b < buttonCount; b++)
        {
            x[b] = (IntExpr)ctx.MkIntConst($"x_{i}_{b}");
            opt.Add(ctx.MkGe(x[b], ctx.MkInt(0)));
        }

        for (int r = 0; r < counterCount; r++)
        {
            var terms = new List<ArithExpr>();

            for (int b = 0; b < buttonCount; b++)
            {
                int[] button = [.. joltageButtons[b]];

                bool affects = false;
                for (int k = 0; k < button.Length; k++)
                {
                    if (button[k] == r)
                    {
                        affects = true;
                        break;
                    }
                }

                if (affects)
                    terms.Add(x[b]);
            }

            ArithExpr lhs;
            if (terms.Count == 0)
                lhs = ctx.MkInt(0);
            else if (terms.Count == 1)
                lhs = terms[0];
            else
                lhs = ctx.MkAdd([.. terms]);

            opt.Add(ctx.MkEq(lhs, ctx.MkInt(target[r])));
        }

        ArithExpr totalExpr;
        if (buttonCount == 1)
            totalExpr = x[0];
        else
            totalExpr = ctx.MkAdd(x);
        

        opt.MkMinimize(totalExpr);

        if (opt.Check() != Status.SATISFIABLE)
        {
            Console.WriteLine("Failed :)");
            return;
        }

        Model model = opt.Model;

        int bestForMachine = 0;
        for (int b = 0; b < buttonCount; b++)
        {
            IntNum val = (IntNum)model.Evaluate(x[b], true);
            bestForMachine += val.Int;
        }

        minPressesPt2 += bestForMachine;
    }
}
int answerPt2 = minPressesPt2;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

List<int> PressButton(List<int> lights, List<int> button)
{
    List<int> newLights = [.. lights];

    foreach (int index in button)
        newLights[index] = 1 - newLights[index]; // Toggle light
    
    return newLights;
}

int PressButtonSequence(List<int> lights, List<List<int>> buttons, int value)
{
    List<int> bitField = [];
    List<int> newLights = [.. Enumerable.Repeat(0, lights.Count)];

    // create bit field
    for (int i = 0; i < buttons.Count; i++)
        bitField.Add((value & (1 << i)) != 0 ? 1 : 0);

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

    // check for match
    if (lights.SequenceEqual(newLights))
        return numPresses;

    return buttons.Count * 100;
}