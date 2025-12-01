# Common Project

This project contains utility classes and functions used across multiple Advent of Code solutions. The main components are `InputReader` and `MathHelper`.

## InputReader
Provides methods for reading and parsing input files:

- **ReadLines(path)**: Reads all lines from a file and returns them as a string array.
- **ReadAllText(path)**: Reads the entire content of a file as a single string.
- **ReadInts(path)**: Reads all lines from a file and parses each line as an integer, returning an enumerable of integers.
- **ReadBlocks(path)**: Splits the file content into blocks separated by double newlines, returning an array of strings.
- **ReadFileToCharGrid(fileName)**: Reads a file and converts each line into a list of characters, returning a grid (list of lists of chars).
- **ReadFileToIntGrid(fileName)**: Reads a file and converts each character in each line to an integer, returning a grid (list of lists of ints).

## MathHelper
Provides mathematical helper functions:

- **ManhattanDistance(x1, y1, x2, y2)**: Calculates the Manhattan distance between two points.
- **LCM(a, b)**: Calculates the least common multiple of two integers or longs.
- **LCM(IEnumerable<long> numbers)**: Calculates the least common multiple of a list of longs.
- **GCD(a, b)**: Calculates the greatest common divisor of two integers or longs.

---

These utilities are designed to simplify input handling and common mathematical operations for Advent of Code problems. Feel free to extend them as needed for new challenges.