using Common;

Console.WriteLine("Day 09: Movie Theater");

string[] input = InputReader.ReadLines("input.txt");

List<(int x, int y)> redTiles = [];
foreach (var line in input)
{
    List<int> xy = [.. line.Split(',').Select(i => int.Parse(i))];
    redTiles.Add((xy[0], xy[1]));
}

// find all combinations of pairs of red tiles and 
// calculate the area of a rectangle created by them
List<(int i, int j, long a)> areaList = [];
for (int i = 0; i < redTiles.Count; i++)
    for (int j = i + 1; j < redTiles.Count; j++)
        areaList.Add((i, j, GetArea(redTiles[i], redTiles[j])));

long answerPt1 = areaList.Select(t => t.a).Max();

// ----------------------------------------------------------------------------

// could not figure out how to do this one. Consulted Reddit solutions
// coordinate compression

// get x's and y's into sorted unique lists
List<int> xs = redTiles.Select(t => t.x).Distinct().OrderBy(x => x).ToList();
List<int> ys = redTiles.Select(t => t.y).Distinct().OrderBy(y => y).ToList();

// get indexes
var xIndex = new Dictionary<int, int>();
var yIndex = new Dictionary<int, int>();

for (int i = 0; i < xs.Count; i++)
    xIndex[xs[i]] = i;

for (int i = 0; i < ys.Count; i++)
    yIndex[ys[i]] = i;

int width = xs.Count + 2;
int height = ys.Count + 2;

bool[,] wall = new bool[width, height];

var redTileCells = new List<(int offsetX, int offsetY)>();

for (int i = 0; i < redTiles.Count; i++)
{
    var p = redTiles[i];
    int offsetX = xIndex[p.x] + 1;
    int offsetY = yIndex[p.y] + 1;

    wall[offsetX, offsetY] = true;
    redTileCells.Add((offsetX, offsetY));
}

for (int i = 0; i < redTiles.Count; i++)
{
    var a = redTiles[i];
    var b = redTiles[(i + 1) % redTiles.Count];

    int ax = xIndex[a.x] + 1;
    int ay = yIndex[a.y] + 1;
    int bx = xIndex[b.x] + 1;
    int by = yIndex[b.y] + 1;

    if (ax == bx)
    {
        int y1 = Math.Min(ay, by);
        int y2 = Math.Max(ay, by);

        for (int gy = y1; gy <= y2; gy++)
        {
            wall[ax, gy] = true;
        }
    }
    else
    {
        int x1 = Math.Min(ax, bx);
        int x2 = Math.Max(ax, bx);

        for (int gx = x1; gx <= x2; gx++)
        {
            wall[gx, ay] = true;
        }
    }
}

bool[,] outside = new bool[width, height];
var q = new Queue<(int x, int y)>();

q.Enqueue((0, 0));
outside[0, 0] = true;

int[] dx = { 1, -1, 0, 0 };
int[] dy = { 0, 0, 1, -1 };

while (q.Count > 0)
{
    var cell = q.Dequeue();

    for (int d = 0; d < 4; d++)
    {
        int nx = cell.x + dx[d];
        int ny = cell.y + dy[d];

        if (nx < 0 || nx >= width || ny < 0 || ny >= height)
            continue;

        if (outside[nx, ny])
            continue;

        if (wall[nx, ny])
            continue;

        outside[nx, ny] = true;
        q.Enqueue((nx, ny));
    }
}

bool[,] allowed = new bool[width, height];

for (int gx = 0; gx < width; gx++)
{
    for (int gy = 0; gy < height; gy++)
    {
        if (!outside[gx, gy])
        {
            allowed[gx, gy] = true;
        }
    }
}

long maxArea = 0;

for (int i = 0; i < redTileCells.Count; i++)
{
    var p1 = redTiles[i];
    var c1 = redTileCells[i];

    for (int j = i + 1; j < redTileCells.Count; j++)
    {
        var p2 = redTiles[j];
        var c2 = redTileCells[j];

        long dxOrig = Math.Abs((long)p1.x - p2.x);
        long dyOrig = Math.Abs((long)p1.y - p2.y);
        long area = (dxOrig + 1) * (dyOrig + 1);

        if (area < maxArea)
            continue;

        int x1 = Math.Min(c1.offsetX, c2.offsetX);
        int x2 = Math.Max(c1.offsetX, c2.offsetX);
        int y1 = Math.Min(c1.offsetY, c2.offsetY);
        int y2 = Math.Max(c1.offsetY, c2.offsetY);

        bool ok = true;

        for (long gx = x1; gx <= x2 && ok; gx++)
        {
            if (!allowed[gx, y1] || !allowed[gx, y2])
                ok = false;
        }

        for (long gy = y1; gy <= y2 && ok; gy++)
        {
            if (!allowed[x1, gy] || !allowed[x2, gy])
                ok = false;
        }

        if (ok)
            maxArea = area;
    }
}

long answerPt2 = maxArea;

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

long GetArea((int x, int y) p1, (int x, int y) p2)
{
    int dx = Math.Abs(p1.x - p2.x) + 1;
    int dy = Math.Abs(p1.y - p2.y) + 1;
    return Convert.ToInt64(dx) * Convert.ToInt64(dy);
}