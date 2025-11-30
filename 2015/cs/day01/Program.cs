using Common;

Console.WriteLine("Day 01: Not Quite Lisp");

string input = InputReader.ReadAllText("input.txt");

int up = input.Count(c => c == '(');
int down = input.Count(c => c == ')');

int answerPt1 = up - down;

// ----------------------------------------------------------------------------

int floor = 0;
int position = 0;
foreach (char c in input)
{
    position++;
    if (c == '(')
        floor++;
    else
        floor--;

    if (floor < 0)
        break;
}

int answerPt2 = position;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================
