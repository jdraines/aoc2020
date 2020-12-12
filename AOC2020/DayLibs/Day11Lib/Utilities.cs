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

        public static char[,] ReadTo2dCharArr(string day, string part="1")
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

        public static List<char> GetVisibles(Coord coord, char[,] seatMap)
        {
            List<char> visibles = new List<char>();
            int[] traversalDirs = { -1, 0, 1 };

            bool IsOnMap(Coord coord)
            {
                return (
                           (coord.Y >= 0 && coord.Y < seatMap.GetLength(0)) &&
                           (coord.X >= 0 && coord.X < seatMap.GetLength(1))
                       );
            }

            void GetNextVisible(int dx, int dy, Coord coord, char[,] seatMap, ref List<char> visibles)
            {
                Coord shifted = new Coord(coord.Y + dy, coord.X + dx);
                char status;

                if (IsOnMap(shifted))
                {
                    status = GetStatus(shifted, seatMap);
                    if (status != '.')
                    {
                        visibles.Add(status);
                    }
                    else
                    {
                        GetNextVisible(dx, dy, shifted, seatMap, ref visibles);
                    }
                }
            }


            foreach (int dy in traversalDirs)
            {
                foreach (int dx in traversalDirs)
                {

                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }

                    GetNextVisible(dx, dy, coord, seatMap, ref visibles);
                }
            }
            return visibles;
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

        public static char[,] UpdateMap(char[,] seatMap, string part)
        {
            char[,] updatedMap = new char[seatMap.GetLength(0), seatMap.GetLength(1)];

            Coord coord;
            List<char> surroundings = new List<char>();

            Dictionary<string, int> rule = new Dictionary<string, int>()
            {
                { "1", 4 }, {"2", 5}
            };

            for (int y = 0; y < seatMap.GetLength(0); y++)
            {
                for (int x = 0; x < seatMap.GetLength(1); x++)
                {

                    coord = new Coord(y, x);
                    if (part == "1")
                    {
                        surroundings = GetAdjacents(coord, seatMap);
                    }
                    else if (part == "2")
                    {
                        surroundings = GetVisibles(coord, seatMap);
                    }

                    // Empty with no occupied surroundings.
                    if (
                        GetStatus(coord, seatMap) == 'L' &&
                        CountAdjacentOccurence('#', surroundings) == 0
                        )
                    {
                        updatedMap[y, x] = '#';
                    }

                    // Occupied w/ >= 4 (or 5) adjacent surroundings
                    else if (
                                GetStatus(coord, seatMap) == '#' &&
                                CountAdjacentOccurence('#', surroundings) >= rule[part]
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
