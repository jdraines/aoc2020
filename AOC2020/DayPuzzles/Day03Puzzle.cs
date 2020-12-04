using System;
using AOC2020.AOCInput;
using AOC2020.DayLibs.Day03Lib;
using CoordTuple = System.Tuple<int, int>;

namespace AOC2020.DayPuzzles
{
    public class Day03Puzzle : IDayPuzzle
    {
        public Day03Puzzle()
        {
        }

        public string Day { get; } = "03";

        public void SolvePart1()
        {
            string[] map = InputParser.ReadToStringArr(Day, "1");

            CoordTuple slope = new CoordTuple (3, 1);

            string[] mapExpanded = MapExpander.ExpandMap(map, slope);
            int treeCount = TreePlotter.NumTrees(mapExpanded, slope);
            Console.WriteLine(treeCount.ToString());
        }

        public void SolvePart2()
        {
            long answer = 1;
            string[] map = InputParser.ReadToStringArr(Day, "1");

            CoordTuple[] slopes =
            {
                new CoordTuple(1, 1),
                new CoordTuple(3, 1),
                new CoordTuple(5, 1),
                new CoordTuple(7, 1),
                new CoordTuple(1, 2),
            };

            int[] treeCounts = new int[slopes.Length];

            int i = 0;
            foreach (CoordTuple slope in slopes)
            {
                string[] mapExpanded = MapExpander.ExpandMap(map, slope);
                int treeCount = TreePlotter.NumTrees(mapExpanded, slope, false);
                treeCounts[i] = treeCount;
                Console.WriteLine($"Tree Count for slope {slope}: {treeCount}");
                i++;
            }
            foreach (int count in treeCounts)
            {
                answer *= (long)count;
            }

            Console.WriteLine($"Answer: {answer}");
        }

        public void SolveTest()
        {
            long answer = 1;
            string[] map = InputParser.ReadToStringArr(Day, "Test");

            CoordTuple[] slopes =
            {
                new CoordTuple(1, 1),
                new CoordTuple(3, 1),
                new CoordTuple(5, 1),
                new CoordTuple(7, 1),
                new CoordTuple(1, 2),
            };

            int[] treeCounts = new int[slopes.Length];

            int i = 0;
            foreach (CoordTuple slope in slopes)
            {
                string[] mapExpanded = MapExpander.ExpandMap(map, slope);
                int treeCount = TreePlotter.NumTrees(mapExpanded, slope, false);
                treeCounts[i] = treeCount;
                Console.WriteLine($"Tree Count for slope {slope}: {treeCount}");
                i++;
            }
            foreach (int count in treeCounts)
            {
                answer *= (long)count;
            }

            Console.WriteLine($"Answer: {answer}");
        }
    }
}