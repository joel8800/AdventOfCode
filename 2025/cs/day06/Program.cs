using Common;
using System.Text.RegularExpressions;

Console.WriteLine("Day 06: Trash Compactor");

string[] input = InputReader.ReadLines("input.txt");

int OPERAND_COUNT = input.Length - 1;   // rows in input with numbers
int OPERATOR_INDEX = input.Length - 1;  // last row has the operators: + or *

// get operands, convert each row into a list of numbers
List<List<long>> operandLists = [];
//List<List<string>> problemText = [];
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

// calculate each set of numbers
for (int i = 0; i < operators.Count; i++)
{
    List<long> operands = [.. operandLists.Select(r => r[i])];

    if (operators[i] == '+')
        sumTotalPt1 += operands.Aggregate((a, b) => a + b);
    else
        sumTotalPt1 += operands.Aggregate((a, b) => a * b);
}

// ----------------------------------------------------------------------------

long sumTotalPt2 = 0;

// for each index, transpose numbers and calculate
for (int i = 0; i < indexes.Count; i++)
{
    // get the index of the current set and the width of the longest number
    int offset = indexes[i];
    int width = operandLists.Max(s => s[i].ToString().Length);

    // transpose
    List<long> operands = [];
    for (int col = 0; col < width; col++)
    {
        List<char> op = [];

        // build operand from a single column
        for (int row = 0; row < OPERAND_COUNT; row++)
            op.Add(input[row][col + offset]);

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
