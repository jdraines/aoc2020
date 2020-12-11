using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;


namespace AOC2020.DayLibs.Day11Lib
{
    
    public struct Coord
    {
        public int Y;
        public int X;
        public Coord(int y, int x) { Y = y; X = x; }
    }

    public static class SeatUtils
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

        public static char GetStatus(Coord coord, char[,] seatMap)
        {
            return seatMap[coord.Y, coord.X];
        }

        public static void SetStatus(Coord coord, ref char[,] seatMap, char status)
        {
            seatMap[coord.Y, coord.X] = status;
        }

        public static List<char> GetAdjacents(Coord coord, char[,] seatMap)
        {
            Coord shifted;
            List<char> adjacents = new List<char>();
            int[] traversals = { -1, 0, 1 };
            foreach (int dy in traversals)
            {
                foreach (int dx in traversals)
                {
                    
                    if (dx == 0 && dy ==0)
                    {
                        continue;
                    }
                    
                    shifted = new Coord(coord.Y + dy, coord.X + dx);
                    
                    if (
                        (shifted.Y >= 0 && shifted.Y < seatMap.GetLength(0)) &&
                        (shifted.X >= 0 && shifted.X < seatMap.GetLength(1))
                        )
                    {
                        adjacents.Add(GetStatus(shifted, seatMap));
                    }
                }
            }
            return adjacents;
        }

        public static int CountAdjacentOccurence(char status, List<char> adjacents)
        {
            int i = 0;

            foreach (char c in adjacents)
            {
                if (c == status)
                {
                    i++;
                }
            }
            return i;
        }

        public static int CountMapOccurence(char status, char[,] seatMap)
        {
            int i = 0;

            for (int y = 0; y < seatMap.GetLength(0); y++)
            {
                for (int x = 0; x < seatMap.GetLength(1); x++)
                {

                    if (seatMap[y,x] == status)
                    {
                        i++;
                    }
                }
            }
            return i;
        }

        public static bool MapsAreEqual(char[,] mapA, char[,] mapB)
        {
            bool equal = true;
            char[] lineA;
            char[] lineB;

            for (int i = 0; i < mapA.GetLength(0); i++)
            {
                lineA = Enumerable.Range(0, mapA.GetLength(1)).Select(x => mapA[i, x]).ToArray();
                lineB = Enumerable.Range(0, mapB.GetLength(1)).Select(x => mapB[i, x]).ToArray();
                if (!Enumerable.SequenceEqual(lineA, lineB))
                {
                    equal = false;
                    break;
                }
            }

            return equal;
        }

        public static char[,] UpdateMap(char[,] seatMap)
        {
            char[,] updatedMap = new char[seatMap.GetLength(0), seatMap.GetLength(1)];

            Coord coord;
            List<char> adjacents;

            for (int y = 0; y < seatMap.GetLength(0); y++)
            {
                for (int x = 0; x < seatMap.GetLength(1); x++)
                {

                    coord = new Coord(y, x);
                    adjacents = GetAdjacents(coord, seatMap);

                    // Empty with no occupied adjacents.
                    if (
                        GetStatus(coord, seatMap) == 'L' &&
                        CountAdjacentOccurence('#', adjacents) == 0
                        )
                    {
                        updatedMap[y, x] = '#';
                    }

                    // Occupied w/ >= 4 adjacent occupieds
                    else if (
                                GetStatus(coord, seatMap) == '#' &&
                                CountAdjacentOccurence('#', adjacents) >= 4
                            )   
                    {
                        updatedMap[y, x] = 'L';
                    }

                    // All other cases
                    else
                    {
                        updatedMap[y, x] = seatMap[y, x];
                    }
                }
            }
            return updatedMap;
        }

    }
}
