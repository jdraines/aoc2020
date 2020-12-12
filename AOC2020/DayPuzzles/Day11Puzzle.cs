using System;
using AOC2020.DayLibs.Day11Lib;

namespace AOC2020.DayPuzzles
{
    public class Day11Puzzle : IDayPuzzle
    {
        public string Day { get; } = "11";

        public void SolvePart1()
        {
            string part1 = "1";
            char[,] seatMap = SeatUtils.ReadTo2dCharArr(Day);
            _ = new char[seatMap.GetLength(0), seatMap.GetLength(1)];

            while (true)
            {
                char[,] updatedMap = SeatUtils.UpdateMap(seatMap, part1);
                if (SeatUtils.MapsAreEqual(updatedMap, seatMap))
                {
                    break;
                }
                else
                {
                    seatMap = updatedMap;
                }
            }

            int occupiedCount = SeatUtils.CountMapOccurence('#', seatMap);
            Console.WriteLine($"Number of occupied seats: {occupiedCount}");

        }

        public void SolvePart2()
        {
            string part2 = "2";
            char[,] seatMap = SeatUtils.ReadTo2dCharArr(Day);
            char[,] updatedMap = new char[seatMap.GetLength(0), seatMap.GetLength(1)];

            int stopAt = 10000;
            int i = 0;

            while (true)
            {
                updatedMap = SeatUtils.UpdateMap(seatMap, part2);
                if (SeatUtils.MapsAreEqual(updatedMap, seatMap))
                {
                    break;
                }
                else
                {
                    seatMap = updatedMap;
                }

                if (i >= stopAt)
                {
                    Console.WriteLine($"Stop iteration point reached ({stopAt}). Breaking while loop.");
                    break;
                }
                i++;
            }

            int occupiedCount = SeatUtils.CountMapOccurence('#', seatMap);
            Console.WriteLine($"Number of occupied seats: {occupiedCount}");
        }

        public void SolveTest()
        {
            char[,] seatMap = SeatUtils.ReadTo2dCharArr(Day, "Test");
            char[,] updatedMap = new char[seatMap.GetLength(0), seatMap.GetLength(1)];

            int stopAt = 1000;
            int i = 0;

            while ( true )
            {
                updatedMap = SeatUtils.UpdateMap(seatMap, "2");
                if (SeatUtils.MapsAreEqual(updatedMap, seatMap))
                {
                    break;
                }
                else
                {
                    seatMap = updatedMap;
                }

                if (i >= stopAt)
                {
                    Console.WriteLine($"Stop iteration point reached ({stopAt}). Breaking while loop.");
                    break;
                }
                i++;
            }

            int occupiedCount = SeatUtils.CountMapOccurence('#', seatMap);
            Console.WriteLine($"Number of occupied seats: {occupiedCount}");
        }
    }
}
