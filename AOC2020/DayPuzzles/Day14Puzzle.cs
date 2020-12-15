using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;
using Util = AOC2020.DayLibs.Day14Lib.BitMemUtil;

namespace AOC2020.DayPuzzles 
{
    public class Day14Puzzle : IDayPuzzle
    {

        public string Day { get; } = "14";

        public void SolvePart1()
        {
            string[] lines = InputParser.ReadToStringArr(Day);
            var memory = new Dictionary<long, string>();
            string mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            long memLoc;
            string bitStr;
            long solution = 0;

            foreach (string line in lines)
            {
                if(line.StartsWith("mask"))
                {
                    mask = Util.GetMask(line);
                }
                else if (line.StartsWith("mem"))
                {
                    (memLoc, bitStr) = Util.ParseMemSet(line);
                    if (memory.ContainsKey(memLoc))
                    {
                        memory[memLoc] = Util.ApplyMaskP1(bitStr, mask);
                    }
                    else
                    {
                        memory.Add(memLoc, Util.ApplyMaskP1(bitStr, mask));
                    }
                }
            }

            foreach (KeyValuePair<long, string> entry in memory)
            {
                try
                {
                    solution += Convert.ToInt64(entry.Value.Trim(), 2);
                }
                catch (Exception e)
                {
                    Console.WriteLine(entry.Value);
                    throw e;
                }
                
            }
            Console.WriteLine($"Solution: {solution}");
        }

        public void SolvePart2()
        {
            string[] lines = InputParser.ReadToStringArr(Day);
            var memory = new Dictionary<long, long>();
            string mask = "000000000000000000000000000000000000";
            List<string> memLocs;
            long val;
            long solution = 0;

            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = Util.GetMask(line);
                }
                else if (line.StartsWith("mem"))
                {
                    (memLocs, val) = Util.ParseMemSet(line, mask);
                    foreach(string memLoc in memLocs)
                    {
                        long loc = Convert.ToInt64(memLoc, 2);

                        if (memory.ContainsKey(loc))
                        {
                            memory[loc] = val;
                        }
                        else
                        {
                            memory.Add(loc, val);
                        }
                    }

                }
            }

            foreach (KeyValuePair<long, long> entry in memory)
            {
                solution += entry.Value;
            }
            Console.WriteLine($"Solution: {solution}");
        }

        public void SolveTest()
        {
            string[] lines = InputParser.ReadToStringArr(Day, "Test");
            var memory = new Dictionary<long, long>();
            string mask = "000000000000000000000000000000000000";
            List<string> memLocs;
            long val;
            long solution = 0;

            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = Util.GetMask(line);
                }
                else if (line.StartsWith("mem"))
                {
                    (memLocs, val) = Util.ParseMemSet(line, mask);
                    foreach (string memLoc in memLocs)
                    {
                        long loc = Convert.ToInt64(memLoc, 2);

                        if (memory.ContainsKey(loc))
                        {
                            memory[loc] = val;
                        }
                        else
                        {
                            memory.Add(loc, val);
                        }
                    }

                }
            }

            foreach (KeyValuePair<long, long> entry in memory)
            {
                Console.WriteLine($"{entry.Key}={entry.Value}");
                solution += entry.Value;
            }
            Console.WriteLine($"Solution: {solution}");
        }
    }
}
