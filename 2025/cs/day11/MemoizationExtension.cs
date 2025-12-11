using System.Collections.Concurrent;

namespace day11
{
    /// <summary>
    /// Memoization extension methods for caching function results.
    /// Used Co-Pilot to help create this extension.  Started with one input version to test.
    /// Then expanded to four input version.
    /// </summary>
    public static class MemoizationExtension
    {
        // A thread-safe memoize function for methods with one input and one output.
        public static Func<TInput, TResult> Memoize<TInput, TResult>(this Func<TInput, TResult> func)
        {
            // Use ConcurrentDictionary for thread safety
            var cache = new ConcurrentDictionary<TInput, TResult>();

            // Return a new function that wraps the original logic with caching
            return input => cache.GetOrAdd(input, func);
        }

        // A thread-safe memoize function for methods with four inputs and one output.
        public static Func<TInput1, TInput2, TInput3, TInput4, TResult>
            Memoize<TInput1, TInput2, TInput3, TInput4, TResult>(
                this Func<TInput1, TInput2, TInput3, TInput4, TResult> func)
        {
            // Use a tuple of all four inputs as the cache key
            var cache = new ConcurrentDictionary<(TInput1, TInput2, TInput3, TInput4), TResult>();

            // Return a new function that wraps the original logic with caching
            return (input1, input2, input3, input4) =>
            {
                var key = (input1, input2, input3, input4);

                // GetOrAdd handles thread safety: gets value if it exists, otherwise calls 'func' and adds result
                return cache.GetOrAdd(key, t => func(t.Item1, t.Item2, t.Item3, t.Item4));
            };
        }
    }
}
