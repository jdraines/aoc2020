using System;
using CubeUtils = AOC2020.DayLibs.Day17Lib.CubeUtils;


namespace AOC2020.DayPuzzles
{
    public class Day17Puzzle : IDayPuzzle
    {
        public string Day { get; } = "17";

        public void SolvePart1()
        {
            char[,,] map = CubeUtils.GetInputMap3D();

            for (int i = 0; i < 6; i++)
            {
                map = CubeUtils.UpdateMap(map);
            }
            Console.WriteLine($"Number of active cubes: {CubeUtils.CountActive(map)}");
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        public void SolveTest()
        {
            char[,,] map = CubeUtils.GetInputMap3D(part: "Test");

            for (int i = 0; i < 6; i++)
            {
                map = CubeUtils.UpdateMap(map);
            }
            Console.WriteLine($"Number of active cubes: {CubeUtils.CountActive(map)}");
        }
    }
}
