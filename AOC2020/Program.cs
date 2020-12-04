/* This program runs one part for one day. The input format to specify which day is
 * a 2-digit, zero-padded number. The input format to specify the part is a single
 * integer, either 1 or 2.
 */

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using IDayPuzzle = AOC2020.DayPuzzles.IDayPuzzle;

namespace AOC2020
{
    public static class Program
    {
        static Dictionary<string, string> partSolver = new Dictionary<string, string>
        {
            {"1", "SolvePart1"},
            {"2", "SolvePart2"},
            {"test", "SolveTest" }
        };

        public static void Main(string[] args)
        {

            Console.WriteLine("What day are we solving? (2 digits) ");
            string day = Console.ReadLine();

            Console.WriteLine("Which part? ");
            string part = Console.ReadLine();

            if (!String.IsNullOrEmpty(day))
            {
                Solve(day, part);
            }
            else
            {
                throw new ArgumentException("Must provide a day.");
            }
        }

        private static void Solve(string day, string part)
        {
            if (partSolver.TryGetValue(part, out string solverMethod))
            {
                InvokeDaySolver(GetDayPuzzClass(day), solverMethod);
            }

            else
            {
                throw new ArgumentOutOfRangeException($"Part should be 1 or 2, but not '{part}'.");
            }
        }

        private static void InvokeDaySolver(IDayPuzzle DayPuzzClass, string methodName)
        {
            _ = DayPuzzClass
                    .GetType()
                    .GetMethod(methodName)
                    .Invoke(DayPuzzClass, null);
        }

        private static IDayPuzzle GetDayPuzzClass(string day)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            IEnumerable<Type> puzzies = asm.GetTypes()
                            .Where(p =>
                                   p.Namespace == "AOC2020.DayPuzzles"
                                && p.Name == $"Day{day}Puzzle"
                                && typeof(IDayPuzzle).IsAssignableFrom(p)
                                );

            if (puzzies.Count() == 0)
            {
                throw new ArgumentOutOfRangeException($"Day {day} not found.");
            }
            else
            {
                return (IDayPuzzle)Activator.CreateInstance(puzzies.First());
            }

        }
    }
}



