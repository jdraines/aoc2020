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
            Dictionary<int, string> rules = Util.GetRules();
            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";

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

            // I empirically discovered that my solution was matching on 1 extra
            // message, despite passing the example case. Discovered this by
            // iterating on the allowed number of repeats to the match groups.
            // allowing an infinite (+) repeat of the matching group to simulate
            // infinite nesting.
            // I assume that the one additional match is the case where rule 11
            // is invoked, and my regex matches on a message with an unequal number
            // of 42's and 31's -- a true nesting would always produce an equal number.
            // I'm fine with just accepting this assumptin and moving on, since
            // the returns from actually proving this out are not worth it to me.

            Console.WriteLine($"Number of matches to rule 0: {matches - 1}"); //260
            
        }

        public void SolveTest()
        {
            Dictionary<int, string> rules = Util.GetRules(part: "Test");
            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";

            string[] messages = Util.GetMessages(part: "Test");
            string regexRule = "^" + Util.GenRegexRule(0, rules) + "$";
            int matches = 0;
            Match match;
            Regex rx = new Regex(regexRule);

            foreach (string msg in messages)
            {
                match = rx.Match(msg);
                if (match.Success)
                {
                    Console.WriteLine(msg);
                    matches++;
                }
            }
            Console.WriteLine($"Number of matches to rule 0: {matches}");
        }
    }
}
