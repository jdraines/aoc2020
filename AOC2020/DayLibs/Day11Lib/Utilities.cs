using System;
using AOC2020.AOCInput;

namespace AOC2020.DayLibs.Day11Lib
{
    public static class Utilities
    {

        public static char[,] ReadTo2dCharArr(string day, string part)
        {
            string[] input = InputParser.ReadToStringArr(day, part);
            char[,] seatMap = new char[input.Length, input[0].Length];
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                { 
                    seatMap[y, x] = input[y][x];
                }
            }

            return seatMap;
        }

    }
}
