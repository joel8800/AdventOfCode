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

    scorePt1 += PlayRound(plays[0], plays[1]);
    scorePt2 += PlayForResult(plays[0], plays[1]);
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
int PlayRound(string opponent, string player)
{
    int score = 0;

    switch (player)
    {
        case "X": score = 1; break;
        case "Y": score = 2; break;
        case "Z": score = 3; break;
    }

    switch (opponent)
    {
        case "A":   // rock
            switch (player)
            {
                case "X": score += 3; break;  // vs rock    : draw
                case "Y": score += 6; break;  // vs paper   : win
                case "Z": score += 0; break;  // vs scissor : lose
            }
            ; break;
        case "B":   // paper
            switch (player)
            {
                case "X": score += 0; break;  // vs rock    : loser
                case "Y": score += 3; break;  // vs paper   : draw
                case "Z": score += 6; break;  // vs scissor : win
            }; break;
        case "C":   // scissors
            switch (player)
            {
                case "X": score += 6; break;  // vs rock    : win
                case "Y": score += 0; break;  // vs paper   : lose
                case "Z": score += 3; break;  // vs scissor : draw
            }; break;
    }

    return score;
}

// player plays shape to achieve result
// X = lose, Y = draw, Z = win
// score = shape selected (1, 2, 3) + outcome (0, 3, 6)
int PlayForResult(string opponent, string player)
{
    int score = 0;

    switch (player)
    {
        case "X": score = 0; break;
        case "Y": score = 3; break;
        case "Z": score = 6; break;
    }

    switch (opponent)
    {
        case "A":
            switch (player)
            {
                case "X": score += 3; break;  // must lose, play scissor
                case "Y": score += 1; break;  // must draw, play rock
                case "Z": score += 2; break;  // must win, play paper
            }; break;
        case "B":
            switch (player)
            {
                case "X": score += 1; break;  // must lose, play rock
                case "Y": score += 2; break;  // must draw, play paper
                case "Z": score += 3; break;  // must win, play scissor
            }; break;
        case "C":
            switch (player)
            {
                case "X": score += 2; break;  // must lose, play paper
                case "Y": score += 3; break;  // must draw, play scissor
                case "Z": score += 1; break;  // must win, play rock
            }; break;
    }

    return score;
}
