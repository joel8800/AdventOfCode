using Common;

Console.WriteLine("Day 02: Rock Paper Scissors");

// Opponent always plays A, B, or C
// Player plays X, Y, or Z, but meaning differs between parts
//
// Opponent      Part 1           Part 2
// A = rock      X = rock         X = lose
// B = paper     Y = paper        Y = draw
// C = scissor   Z = scissors     Z = win
//

string[] input = InputReader.ReadLines("input.txt");

int scorePt1 = 0;
int scorePt2 = 0;

foreach (string line in input)
{
    string[] plays = line.Split(' ');

    scorePt1 += PlayRound(line);
    scorePt2 += PlayForResult(line);
}

int answerPt1 = scorePt1;

// ----------------------------------------------------------------------------

int answerPt2 = scorePt2;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

// player plays their shape
// X = rock, Y = paper, Z = scissor
// score = shape selected (1, 2, 3) + outcome (0, 3, 6)
int PlayRound(string line)  //(string opponent, string player)
{
    int score = 0;

    switch (line)
    {
        case "A X": score = 1 + 3; break;  // rock vs rock    : draw
        case "A Y": score = 2 + 6; break;  // rock vs paper   : win
        case "A Z": score = 3 + 0; break;  // rock vs scissor : lose
        case "B X": score = 1 + 0; break;  // paper vs rock    : lose
        case "B Y": score = 2 + 3; break;  // paper vs paper   : draw
        case "B Z": score = 3 + 6; break;  // paper vs scissor : win
        case "C X": score = 1 + 6; break;  // scissors vs rock    : win
        case "C Y": score = 2 + 0; break;  // scissors vs paper   : lose
        case "C Z": score = 3 + 3; break;  // scissors vs scissor : draw
    }
    return score;
}

// player plays shape to achieve result
// X = lose, Y = draw, Z = win
// score = shape selected (1, 2, 3) + outcome (0, 3, 6)
int PlayForResult(string line)  //(string opponent, string player)
{
    int score = 0;

    switch (line)
    {
        case "A X": score = 3 + 0; break;  // rock, must lose, play scissor
        case "A Y": score = 1 + 3; break;  // rock, must draw, play rock
        case "A Z": score = 2 + 6; break;  // rock, must win, play paper
        case "B X": score = 1 + 0; break;  // paper, must lose, play rock
        case "B Y": score = 2 + 3; break;  // paper, must draw, play paper
        case "B Z": score = 3 + 6; break;  // paper, must win, play scissor
        case "C X": score = 2 + 0; break;  // scissor, must lose, play paper
        case "C Y": score = 3 + 3; break;  // scissor, must draw, play scissor
        case "C Z": score = 1 + 6; break;  // scissor, must win, play rock
    }
    return score;
}
