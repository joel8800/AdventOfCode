using Common;
using System.Text.RegularExpressions;

Console.WriteLine("Day 05: Doesn't He Have Intern-Elves For This?");

string[] input = InputReader.ReadLines("input.txt");
int nicePt1 = 0;
foreach (string line in input)
{
    if (HasIllegalCombo(line))
        continue;

    if (Has3Vowels(line) && HasDoubleLetter(line))
        nicePt1++;
}

int answerPt1 = nicePt1;

// ----------------------------------------------------------------------------

int nicePt2 = 0;
foreach (string line in input)
{
    if (HasPairSplitByOneChar(line) && HasTwoPair(line))
        nicePt2++;
}

int answerPt2 = nicePt2;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

bool HasIllegalCombo(string line)
{
    MatchCollection mc = Regex.Matches(line, @"ab|cd|pq|xy");
    return mc.Count > 0;
}

bool Has3Vowels(string line)
{
    MatchCollection mc = Regex.Matches(line, @"[aeiou]");
    return mc.Count >= 3;
}

bool HasDoubleLetter(string line)
{
    MatchCollection mc = Regex.Matches(line, @"(.)\1");
    return mc.Count > 0;
}

bool HasTwoPair(string line)
{
    MatchCollection mc = Regex.Matches(line, @"(.)(.).*\1\2");
    return mc.Count > 0;
}

bool HasPairSplitByOneChar(string line)
{
    MatchCollection mc = Regex.Matches(line, @"(.).\1");
    return mc.Count > 0;
}