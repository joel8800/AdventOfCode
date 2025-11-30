namespace Common
{
    public static class MathHelper
    {
        public static int ManhattanDistance(int x1, int y1, int x2, int y2) =>
            Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

        /// <summary>
        /// Least common multiple
        /// ints, longs, list of longs
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int LCM(int a, int b) =>
            (a / GCD(a, b)) * b;
        public static long LCM(long a, long b) =>
            (a / GCD(a, b)) * b;
        public static long LCM(IEnumerable<long> numbers)
        {
            return numbers.Aggregate((x, y) => x * y / GCD(x, y));
        }

        /// <summary>
        /// greatest common divisor
        /// ints and longs
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                (a, b) = (b, a % b);
            }
            return a;
        }
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                (a, b) = (b, a % b);
            }
            return a;
        }

    }
}
