using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;
using Coord = AOC2020.DayLibs.Day11Lib.Coord;

namespace AOC2020.DayPuzzles
{

    public struct CardinalDir
    {
        public int dx;
        public int dy;

        public CardinalDir(int dx, int dy)
        {
            this.dx = dx;
            this.dy = dy;
        }
    }

    public static class Dir
    {
        public static CardinalDir North = new CardinalDir(dx: 0, dy: 1);
        public static CardinalDir South = new CardinalDir(dx: 0, dy: -1);
        public static CardinalDir East = new CardinalDir(dx: 1, dy: 0);
        public static CardinalDir West = new CardinalDir(dx: -1, dy: 0);
    }

    public static class DirUtils
    {
        public static Dictionary<char, CardinalDir> dirLkup = new Dictionary<char, CardinalDir>()
        {
            { 'N', Dir.North },
            { 'S', Dir.South },
            { 'E', Dir.East },
            { 'W', Dir.West }
        };

        public static char[] CardinalKeys = { 'N', 'S', 'E', 'W' };

        public static char[] Turns = { 'L', 'R' };

        public static Dictionary<char, char> LeftTurn90 = new Dictionary<char, char>()
        {
            { 'N', 'W' },
            { 'W', 'S' },
            { 'S', 'E' },
            { 'E', 'N' }
        };

        public static Dictionary<char, char> RightTurn90 = new Dictionary<char, char>()
        {
            { 'N', 'E' },
            { 'E', 'S' },
            { 'S', 'W' },
            { 'W', 'N' }
        };

        public static Dictionary<char, Dictionary<char, char>> turnLkp = new Dictionary<char, Dictionary<char, char>>()
        {
            { 'L', LeftTurn90 }, {'R', RightTurn90 }
        };

        public static Tuple<char, int> ParseDir(string dir)
        {
            return new Tuple<char, int>(dir[0], Int32.Parse(dir[1..]));
        }


    }

    public class Day12Puzzle : IDayPuzzle
    {
        public string Day { get; } = "12";

        public void SolvePart1()
        {
            Coord coord = new Coord(0, 0);
            string[] directions = InputParser.ReadToStringArr(Day);
            char orientation = 'E';

            foreach(string direction in directions)
            {
                (char dir, int val) = DirUtils.ParseDir(direction);

                if(DirUtils.CardinalKeys.Contains(dir))
                {
                    CardinalDir step = DirUtils.dirLkup[dir];
                    coord.X += (val * step.dx);
                    coord.Y += (val * step.dy);
                }
                else if (DirUtils.Turns.Contains(dir))
                {
                    for (int i = 0; i < (val / 90); i++)
                    {
                        orientation = DirUtils.turnLkp[dir][orientation];
                    }
                }
                else if (dir == 'F')
                {
                    CardinalDir step = DirUtils.dirLkup[orientation];
                    coord.X += (val * step.dx);
                    coord.Y += (val * step.dy);
                }
            }
            int solution = Math.Abs(coord.X) + Math.Abs(coord.Y);
            Console.WriteLine($"Manhattan Distance: {solution}");
        }

        public void SolvePart2()
        {
            Coord shipLoc = new Coord(0, 0);
            Coord waypoint = new Coord(1, 10);

            string[] directions = InputParser.ReadToStringArr(Day);
            char orientation = 'E';

            foreach (string direction in directions)
            {
                (char dir, int val) = DirUtils.ParseDir(direction);

                if (DirUtils.CardinalKeys.Contains(dir))
                {
                    CardinalDir step = DirUtils.dirLkup[dir];
                    waypoint.X += (val * step.dx);
                    waypoint.Y += (val * step.dy);
                }
                else if (DirUtils.Turns.Contains(dir))
                {
                    for (int i = 0; i < (val / 90); i++)
                    {
                        if(dir == 'R')
                        {
                            int x = waypoint.X;
                            int y = waypoint.Y;
                            waypoint.X = y;
                            waypoint.Y = -x;
                        }
                        if (dir == 'L')
                        {
                            int x = waypoint.X;
                            int y = waypoint.Y;
                            waypoint.X = -y;
                            waypoint.Y = x;
                        }
                    }
                }
                else if (dir == 'F')
                {
                    CardinalDir step = DirUtils.dirLkup[orientation];
                    for (int i = 0; i < val; i++)
                    {
                        shipLoc.X += waypoint.X;
                        shipLoc.Y += waypoint.Y;
                    }
                }
            }
            int solution = Math.Abs(shipLoc.X) + Math.Abs(shipLoc.Y);
            Console.WriteLine($"Manhattan Distance: {solution}");
        }

        public void SolveTest()
        {

            Coord shipLoc = new Coord(0, 0);
            Coord waypoint = new Coord(1, 10);

            string[] directions = InputParser.ReadToStringArr(Day, "Test");
            char orientation = 'E';

            foreach (string direction in directions)
            {
                (char dir, int val) = DirUtils.ParseDir(direction);

                if (DirUtils.CardinalKeys.Contains(dir))
                {
                    CardinalDir step = DirUtils.dirLkup[dir];
                    waypoint.X += (val * step.dx);
                    waypoint.Y += (val * step.dy);
                }
                else if (DirUtils.Turns.Contains(dir))
                {
                    for (int i = 0; i < (val / 90); i++)
                    {
                        if (dir == 'R')
                        {
                            int x = waypoint.X;
                            int y = waypoint.Y;
                            waypoint.X = y;
                            waypoint.Y = -x;
                        }
                        if (dir == 'L')
                        {
                            int x = waypoint.X;
                            int y = waypoint.Y;
                            waypoint.X = -y;
                            waypoint.Y = x;
                        }
                    }
                }
                else if (dir == 'F')
                {
                    CardinalDir step = DirUtils.dirLkup[orientation];
                    for (int i = 0; i < val; i++)
                    {
                        shipLoc.X += waypoint.X;
                        shipLoc.Y += waypoint.Y;
                    }
                }
            }
            int solution = Math.Abs(shipLoc.X) + Math.Abs(shipLoc.Y);
            Console.WriteLine($"Manhattan Distance: {solution}");
        }
    }
}
