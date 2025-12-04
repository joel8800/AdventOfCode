using Common;

Console.WriteLine("Day 04: Printing Department");

List<List<char>> input = InputReader.ReadFileToCharGrid("input.txt");

int accessible = 0;
List<(int row, int col)> accessiblePositions = [];

for (int row = 0; row < input.Count; row++)
{
    for (int col = 0; col < input[row].Count; col++)
    {
        if (input[row][col] == '@')
        {
            if (IsAccessible(input, row, col))
            {
                accessiblePositions.Add((row, col));
                accessible++;
            }
        }
    }
}


int answerPt1 = accessible;

// ----------------------------------------------------------------------------


accessible = 1;
int removedRolls = 0;

while (accessible > 0)
{
    accessible = 0;
    accessiblePositions.Clear();

    for (int row = 0; row < input.Count; row++)
    {
        for (int col = 0; col < input[row].Count; col++)
        {
            if (input[row][col] == '@')
            {
                if (IsAccessible(input, row, col))
                {
                    accessiblePositions.Add((row, col));
                    accessible++;
                }
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
    int numRows = grid.Count;
    int numCols = grid[0].Count;

    if (IsInbounds(grid, row, col) == false)
        return false;

    // Check row above
    if (row > 0 && col - 1 >= 0)
        if (grid[row - 1][col - 1] == '@')
            adjacentRolls++;

    if (row > 0)
        if (grid[row - 1][col] == '@')
            adjacentRolls++;

    if (row > 0 && col + 1 < numCols)
        if (grid[row - 1][col + 1] == '@')
            adjacentRolls++;

    // Check same row
    if (col > 0)
        if (grid[row][col - 1] == '@')
            adjacentRolls++;
    
    if (col + 1 < numCols)
        if (grid[row][col + 1] == '@')
            adjacentRolls++;

    // check row below
    if (row + 1 < numRows && col - 1 >= 0)
        if (grid[row + 1][col - 1] == '@') 
            adjacentRolls++;
    
    if (row + 1 < numRows)
        if (grid[row + 1][col] == '@')
            adjacentRolls++;
    
    if (row + 1 < numRows && col + 1 < numCols)
        if (grid[row + 1][col + 1] == '@')
            adjacentRolls++;

    if (adjacentRolls < 4)
        return true;
    else
        return false;
}

bool IsInbounds(List<List<char>> grid, int row, int col)
{
    return row >= 0 && row < grid.Count && col >= 0 && col < grid[0].Count;
}