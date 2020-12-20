using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Util = AOC2020.DayLibs.Day19Lib.D19Util;

namespace AOC2020.DayPuzzles
{
    public class Day19Puzzle : IDayPuzzle
    {
        public string Day { get; } = "19";

        public void SolvePart1()
        {

            Dictionary<int, string> rules = Util.GetRules();
            string[] messages = Util.GetMessages();
            string regexRule = "^" + Util.GenRegexRule(0, rules) + "$";
            int matches = 0;
            Match match;
            Regex rx = new Regex(regexRule);

            foreach (string msg in messages)
            {
                match = rx.Match(msg);
                if (match.Success)
                {
                    matches++;
                }
            }
            Console.WriteLine($"Number of matches to rule 0: {matches}");
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        public void SolveTest()
        {
            Dictionary<int, string> rules = Util.GetRules(part: "Test");
            Console.WriteLine($"'{Util.GenRegexRule(0, rules)}'");
        }
    }
}
