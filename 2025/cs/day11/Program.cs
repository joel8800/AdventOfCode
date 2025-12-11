using Common;
using static day11.MemoizationExtension;

Console.WriteLine("Day 11: Reactor");

string[] input = InputReader.ReadLines("input.txt");

Dictionary<string, List<string>> devices = [];
foreach (string line in input)
{
    string[] parts = line.Split(' ');
    string deviceName = parts[0][0..^1];
    List<string> connections = [.. parts[1..]];
    devices[deviceName] = connections;
}

HashSet<string> visited = [];
List<List<string>> part1Paths = [];

// part 1 is small enough to brute force all paths and get a count
part1Paths = FindAllPaths(devices, "you", visited, [], part1Paths);

int answerPt1 = part1Paths.Count;

// ----------------------------------------------------------------------------

// Must cache results for part 2 due to exponential growth of paths
// Used Co-Pilot to help create memoization extension method

// 1. Declare the Func variable that will hold the memoized function.
Func<string, Dictionary<string, List<string>>, bool, bool, long>? Memoized = null;

// 2. Define the raw recursive logic. Crucially, the internal calls 
//    *must* use the 'memoizedRecursiveFunc' variable name.
Func<string, Dictionary<string, List<string>>, bool, bool, long> rawRecursiveLogic = (x, graph, dacSeen, fftSeen) =>
{
    if (x == "out")
    {
        return dacSeen && fftSeen ? 1 : 0;
    }
    else
    {
        long total = 0;

        foreach (string neighbor in graph[x])
        {
            bool localDacSeen = dacSeen || neighbor == "dac";
            bool localFftSeen = fftSeen || neighbor == "fft";
            total += Memoized(neighbor, graph, localDacSeen, localFftSeen);
        }
        return total;
    }
};

// 3. Apply the Memoize extension method to the raw logic and assign 
//    the resulting cached function back to the original variable name.
Memoized = rawRecursiveLogic.Memoize();

// 4. Now you can call the memoized function as needed.
long answerPt2 = Memoized("svr", devices, false, false);

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

int part1(string x, Dictionary<string, List<string>> graph)
{
    if (x == "out")
        return 1;
    else
    {
        int total = 0;
        foreach (string neighbor in graph[x])
            total += part1(neighbor, graph);

        return total;
    }
}


// breadth-first search to find all paths from start to end
List<List<string>> FindAllPaths(Dictionary<string, List<string>> graph, string start,
    HashSet<string> visited, List<string> path, List<List<string>> paths)
{
    visited.Add(start);
    path.Add(start);
    if (start == "out")
    {
        paths.Add(path);
    }
    else
    {
        foreach (string neighbor in graph[start])
        {
            if (!visited.Contains(neighbor))
            {
                FindAllPaths(graph, neighbor, visited, path, paths);
            }
        }
    }
    path.RemoveAt(path.Count - 1);
    visited.Remove(start);
    return paths;
}
