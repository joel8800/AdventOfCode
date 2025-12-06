using Common;
using System.Text.RegularExpressions;

Console.WriteLine("Day 06: Trash Compactor");

string[] input = InputReader.ReadLines("input.txt");

int OPERAND_COUNT = input.Length - 1;
int OPERATOR_INDEX = input.Length - 1;

// get operands
List<List<long>> operandLists = [];
for (int i = 0; i < OPERAND_COUNT; i++)
{
    MatchCollection mc = Regex.Matches(input[i], @"\d+");
    operandLists.Add([.. mc.Select(m => long.Parse(m.Value))]);
}

// get operator indexes
MatchCollection matches = Regex.Matches(input[OPERATOR_INDEX], @"[+*]");
List<int> indexes = [.. matches.Select(m => m.Index)];
List<char> operators = [.. matches.Select(m => m.Value[0])];

long sumTotalPt1 = 0;

// calculate each column of numbers
for (int i = 0; i < operators.Count; i++)
{
    List<long> operands = [.. operandLists.Select(r => r[i])];

    if (operators[i] == '+')
        sumTotalPt1 += operands.Aggregate((a, b) => a + b);
    else
        sumTotalPt1 += operands.Aggregate((a, b) => a * b);    
}

// ----------------------------------------------------------------------------

// get widths of each operator section
List<int> widths = [];
for (int i = 1; i < indexes.Count; i++)
    widths.Add(indexes[i] - indexes[i - 1]);
widths.Add(input[OPERAND_COUNT - 1].Length - indexes.Last());

long sumTotalPt2 = 0;

// for each index, transpose numbers and calculate
for (int i = 0; i < indexes.Count; i++)
{
    // width offset
    int offset = indexes[i];

    // get operands from columns of numbers
    List<long> operands = [];    
    for (int col = 0; col < widths[i]; col++)
    {
        List<char> op = [];

        // build operand from a single column
        for (int row = 0; row < OPERAND_COUNT; row++)
            op.Add(input[row][col + offset]);

        // only process if not all spaces
        if (op.All(c => c == ' ') == false)
            operands.Add(long.Parse(string.Join("", op)));
    }

    // calculate based on operator
    if (operators[i] == '+')
        sumTotalPt2 += operands.Aggregate((a, b) => a + b);
    else
        sumTotalPt2 += operands.Aggregate((a, b) => a * b);
}

Console.WriteLine($"Part 1: {sumTotalPt1}");
Console.WriteLine($"Part 2: {sumTotalPt2}");

// ============================================================================

