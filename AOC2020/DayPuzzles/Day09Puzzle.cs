using System;
using System.Collections.Generic;
using System.Linq;
using AOC2020.AOCInput;
using AOC2020.XMASHack;

namespace AOC2020.DayPuzzles
{
    public class Day09Puzzle : IDayPuzzle
    {
        public string Day { get; set; } = "09";

        public void SolvePart1()
        {
            long[] encryptedData = InputParser.ReadToLongArr(Day, "1");
            XMASHacker xmasHacker = new XMASHacker(encryptedData);
            long exceptionVal = xmasHacker.SearchFirstExceptionVal();

            if (exceptionVal == -1)
            {
                Console.WriteLine("Exception to the XMAS encryption was not found.");
            }
            else
            {
                Console.WriteLine($"First exception to the XMAS algorithm: {exceptionVal}");
            }
        }

        public void SolvePart2()
        {
            long[] encryptedData = InputParser.ReadToLongArr(Day, "1");
            XMASHacker xmasHacker = new XMASHacker(encryptedData);
            List<long> weaknessList = xmasHacker.EncryptionWeaknessList();

            if (weaknessList.Count == 0)
            {
                Console.WriteLine($"No weakness list was identified.");
            }

            else
            {
                long weakness = weaknessList.Min() + weaknessList.Max();
                Console.WriteLine($"Encryption Weakness: {weakness}");
            }
        }

        public void SolveTest()
        {
            throw new NotImplementedException();
        }
    }
}
