using System;
using AOC2020.AOCInput;
using AOC2020.DayLibs.Day01Lib;

namespace AOC2020.DayPuzzles
{
    public class Day01Puzzle : IDayPuzzle
    {
        public Day01Puzzle() { }

        public string Day { get; } = "01";

        public void SolvePart1()
        {
            int[] input = InputParser.ReadToIntArr(Day, "1");
            (int A, int B) = Day01EntryBalancer.PairsSumToVal(input, 2020);
            Console.WriteLine((A * B).ToString());
        }

        public void SolvePart2()
        {
            int[] input = InputParser.ReadToIntArr(Day, "1");
            (int A, int B, int C) = Day01EntryBalancer.TriosSumToVal(input, 2020);
            Console.WriteLine((A * B * C).ToString());
        }

        public void SolveTest() { }
    }
}