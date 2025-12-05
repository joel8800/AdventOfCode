using Common;

Console.WriteLine("Day 04: Printing Department");

List<List<char>> input = InputReader.ReadFileToCharGrid("input.txt");

int accessible = 0;
List<(int row, int col)> accessiblePositions = [];

for (int row = 0; row < input.Count; row++)
{
    for (int col = 0; col < input[row].Count; col++)
    {
        if (IsAccessible(input, row, col))
        {
            accessiblePositions.Add((row, col));
            accessible++;
        }
    }
}


int answerPt1 = accessible;

// ----------------------------------------------------------------------------

int removedRolls = 0;

accessible = 1;
while (accessible > 0)
{
    accessible = 0;
    accessiblePositions.Clear();

    for (int row = 0; row < input.Count; row++)
    {
        for (int col = 0; col < input[row].Count; col++)
        {
            if (IsAccessible(input, row, col))
            {
                accessiblePositions.Add((row, col));
                accessible++;
            }
        }
    }
    //Console.WriteLine($"Found {accessible} accessible rolls this round.");

    foreach (var (row, col) in accessiblePositions)
    {
        input[row][col] = '.';
        removedRolls++;
    }
    //Console.WriteLine($"Removed {removedRolls} rolls so far.");
}

int answerPt2 = removedRolls;


Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

bool IsAccessible(List<List<char>> grid, int row, int col)
{
    int adjacentRolls = 0;

    if (IsInbounds(grid, row, col) == false)
        return false;

    // skip if not a roll
    if (grid[row][col] != '@')
        return false;

    // directions to check: [NW, N, NE, W, E, SW, S, SE]
    List<(int r, int c)> directions = 
        [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1),  (1, 0),  (1, 1)];
    
    // check all directions
    foreach (var (dr, dc) in directions)
    {
        int newRow = row + dr;
        int newCol = col + dc;
        if (IsInbounds(grid, newRow, newCol))
        {
            if (grid[newRow][newCol] == '@')
                adjacentRolls++;
        }
    }   

    if (adjacentRolls < 4)
        return true;
    else
        return false;
}

bool IsInbounds(List<List<char>> grid, int row, int col)
{
    return row >= 0 && row < grid.Count && col >= 0 && col < grid[0].Count;
}