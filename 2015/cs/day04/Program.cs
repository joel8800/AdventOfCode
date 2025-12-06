using Common;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Day 04: The Ideal Stocking Stuffer");

string key = InputReader.ReadAllText("input.txt");

MD5 hasher = MD5.Create();
int answerPt1 = 0;

for (int i = 0; i < Int32.MaxValue; i++)
{
    string input = $"{key}{i}";

    string hash = GetMd5Hash(hasher, input);
    if (hash.StartsWith("00000"))
    {
        answerPt1 = i;
        break;
    }
}

// ----------------------------------------------------------------------------

int answerPt2 = 0;

for (int i = answerPt1; i < Int32.MaxValue; i++)
{
    string input = $"{key}{i}";

    string hash = GetMd5Hash(hasher, input);
    if (hash.StartsWith("000000"))
    {
        answerPt2 = i;
        break;
    }
}

Console.WriteLine($"Part 1: {answerPt1}");
Console.WriteLine($"Part 2: {answerPt2}");

// ============================================================================

static string GetMd5Hash(MD5 hasher, string input)
{
    // convert the input string an MD5 hash - returns as a byte array
    byte[] hashData = hasher.ComputeHash(Encoding.Default.GetBytes(input));

    // convert to hex values
    List<string> hashes = [.. hashData.Select(b => b.ToString("x2"))];

    return string.Join("", hashes);
}
