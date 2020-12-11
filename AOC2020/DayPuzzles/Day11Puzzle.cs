using System;
using AOC2020.DayLibs.Day11Lib;

namespace AOC2020.DayPuzzles
{
    public class Day11Puzzle : IDayPuzzle
    {
        public string Day { get; } = "11";

        public void SolvePart1()
        {
            char[,] seatMap = Utilities.ReadTo2dCharArr(Day, "1");
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        public void SolveTest()
        {
            throw new NotImplementedException();
        }
    }
}
