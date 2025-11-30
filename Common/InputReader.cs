namespace Common
{
    public static class InputReader
    {
        public static string[] ReadLines(string path) =>
            File.ReadAllLines(path);

        public static string ReadAllText(string path) =>
            File.ReadAllText(path);

        public static IEnumerable<int> ReadInts(string path) =>
            File.ReadAllLines(path).Select(int.Parse);

        public static string[] ReadBlocks(string path)
        {
            string nl2x = $"{Environment.NewLine}{Environment.NewLine}";
            string text = File.ReadAllText(path);

            string[] blocks = text.Split(nl2x, StringSplitOptions.RemoveEmptyEntries);

            return blocks;
        }

        public static List<List<char>> ReadFileToCharGrid(string fileName)
        {
            string[] input = File.ReadAllLines(fileName);

            List<List<char>> grid = [];

            int colSize = input.Length;

            // fill forest
            for (int y = 0; y < colSize; y++)
            {
                List<char> row = [.. input[y]];

                grid.Add(row);
            }

            return grid;
        }

        public static List<List<int>> ReadFileToIntGrid(string fileName)
        {
            string[] input = File.ReadAllLines(fileName);

            List<List<int>> grid = new();

            int colSize = input.Length;

            // fill forest
            for (int y = 0; y < colSize; y++)
            {
                List<int> row = [];

                foreach (char c in input[y])
                    row.Add(c - '0');

                grid.Add(row);
            }

            return grid;
        }

    }

}
