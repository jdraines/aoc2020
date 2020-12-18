using System;
using AOC2020.AOCInput;
using System.Collections.Generic;
using System.Linq;


namespace AOC2020.DayLibs.Day17Lib
{
    
    public static class CubeUtils
    {
        public static char[,,] GetInputMap3D(string day="17", string part="1")
        {
            string[] rawIn = InputParser.ReadToStringArr(day, part);
            char[,,] map = new char[rawIn[0].Length, rawIn.Length, 1];

            for (int i = 0; i < rawIn[0].Length; i++)
            {
                for (int j = 0; j < rawIn.Length; j++)
                {
                    map[j, i, 0] = rawIn[i][j];
                }
            }
            return map;
        }

        public static char[,,] EnlargeMap(char[,,] map)
        {
            char[,,] enlarged = new char[map.GetLength(0) + 2, map.GetLength(1) + 2, map.GetLength(2) + 2];

            for (int i = 0; i < enlarged.GetLength(0); i++)
            {
                for (int j = 0; j < enlarged.GetLength(1); j++)
                {
                    for (int k = 0; k < enlarged.GetLength(2); k++)
                    {
                        enlarged[i, j, k] = '.';
                    }
                }
            }

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    for (int k = 0; k < map.GetLength(2); k++)
                    {
                        enlarged[i + 1, j + 1, k + 1] = map[i, j, k];
                    }
                }
            }

            return enlarged;
        }

        public static char[,,] UpdateMap(char[,,] map)
        {
            char[,,] updated = EnlargeMap(map);
            int activeNeighbors = 0;

            for (int i = 0; i < updated.GetLength(0); i++)
            {
                for (int j = 0; j < updated.GetLength(1); j++)
                {
                    for (int k = 0; k < updated.GetLength(2); k++)
                    {
                        activeNeighbors = CountActiveNeighbors(map, i-1, j-1, k-1);

                        if (IsActive(updated, i, j, k))
                        {
                            if (activeNeighbors != 2 && activeNeighbors != 3)
                            {
                                updated[i, j, k] = '.';
                            }
                        }
                        else
                        {
                            if (activeNeighbors == 3)
                            {
                                updated[i, j, k] = '#';
                            }
                        }
                    }
                }
            }

            return updated;
        }


        public static int CountActiveNeighbors(char[,,] map, int x, int y, int z)
        {
            int count = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    for (int k = z - 1; k < z + 2; k++)
                    {
                        if (i == x && j == y && k == z)
                        { }
                        else if (
                            (i >= 0 && j >= 0 && k >= 0) &&
                            (i < map.GetLength(0) && j < map.GetLength(1) && k < map.GetLength(2))
                           )
                        {
                            if (map[i, j, k] == '#')
                            {
                                count++;
                            }
                        }
                    }

                }
            }
            return count;
        }

        public static void PrintNeighbors(char[,,] map, int x, int y, int z)
        {
            char[,,] nMap = new char[map.GetLength(0), map.GetLength(1), map.GetLength(2)];

            for (int i = 0; i < nMap.GetLength(0); i++)
            {
                for (int j = 0; j < nMap.GetLength(1); j++)
                {
                    for (int k = 0; k < nMap.GetLength(2); k++)
                    {
                        nMap[i, j, k] = '.';
                    }
                }
            }

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    for (int k = z - 1; k < z + 2; k++)
                    {
                        if (i == x && j == y && k == z)
                        {
                            nMap[i, j, k] = 'X';
                        }
                        else if (
                            (i >= 0 && j >= 0 && k >= 0) &&
                            (i < map.GetLength(0) && j < map.GetLength(1) && k < map.GetLength(2))
                           )
                        {
                            nMap[i, j, k] = 'N';
                        }
                    }

                }
            }

            PrintMap(nMap);
        }

        public static int CountActive(char[,,] map)
        {
            int count = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    for (int k = 0; k < map.GetLength(2); k++)
                    {
                        if (map[i, j, k] == '#')
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public static void PrintMap(char[,,] map)
        {
            for (int z = 0; z < map.GetLength(2); z++)
            {
                Console.WriteLine($"Z: {z}");
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        Console.Write(map[x, y, z]);
                    }
                    Console.Write('\n');
                }
                Console.WriteLine('\n');
            }
        }


        private static bool IsActive(char[,,] map, int x, int y, int z)
        {
            return map[x, y, z] == '#';
        }

    }
}
