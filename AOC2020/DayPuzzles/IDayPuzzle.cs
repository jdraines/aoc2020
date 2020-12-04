using System;
namespace AOC2020.DayPuzzles
{
    public interface IDayPuzzle
    {
        string Day { get; }
        void SolvePart1();
        void SolvePart2();
        void SolveTest();
    }
}
