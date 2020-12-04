using System;
using CoordTuple = System.Tuple<int, int>;

namespace AOC2020.DayLibs.Day03Lib
{

    public static class TreePlotter
    {
        public static char Tree = '#';

        public static int NumTrees(string[] map, CoordTuple slope, bool printMap=true)
        {
            int height = map.Length;
            int width = map[0].Length;

            int posX = 0;
            int posY = 0;

            int treeCount = 0;

            while (posX < width && posY < height)
            {
                if (TreeAt(posX, posY, map))
                {
                    treeCount++;
                    char[] ch = map[posY].ToCharArray();
                    ch[posX] = 'X';
                    map[posY] = new string(ch);
                }
                else
                {
                    char[] ch = map[posY].ToCharArray();
                    ch[posX] = 'O';
                    map[posY] = new string(ch);
                    
                }

                Step(ref posX, ref posY, slope);
            }

            if (printMap)
            {
                Console.WriteLine(" ");
                foreach (string line in map)
                {
                    Console.WriteLine(line);
                }
            }
            
            return treeCount;
        }

        private static void Step(ref int posX, ref int posY, CoordTuple slope)
        {
            (int deltaX, int deltaY) = slope;
            posX = posX + deltaX;
            posY = posY + deltaY;
        }

        private static bool TreeAt(int posX, int posY, string[] map)
        {
            try
            {
                return map[posY][posX] == Tree;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

        }
    }
}
