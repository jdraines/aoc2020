using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;

namespace AOC2020.DayLibs.Day19Lib
{
    public static class D19Util
    {

        public static string[] GetMessages(string day="19", string part="1")
        {
            string messagesRaw = InputParser.ReadChunksToStringArr(day, part)[1];
            return messagesRaw.Split('\n');
        }

        public static Dictionary<int, string> GetRules(string day="19", string part="1")
        {
            Dictionary<int, string> rules = new Dictionary<int, string>();
            string rulesRaw = InputParser.ReadChunksToStringArr(day, part)[0];
            string[] ruleSplit;

            foreach (string rule in rulesRaw.Split('\n'))
            {
                if (String.IsNullOrEmpty(rule) || String.IsNullOrWhiteSpace(rule))
                { continue;  }

                ruleSplit = rule.Split(':');
                try
                {
                    rules.Add(Int32.Parse(ruleSplit[0].Trim()), ruleSplit[1].Trim());
                }
                catch
                {
                    Console.WriteLine($"'{rule}'");
                    throw;
                }
            }

            return rules;
        }

        public static string GenRegexRule(int ruleNo, Dictionary<int, string> rules)
        {
            string rule = rules[ruleNo];
            string regexRule = "(";
            string[] rSeq = rule.Split('|');

            if (rule.StartsWith("\""))
            {
                return GetRuleVal(rule);
            }

            for (int i = 0; i < rSeq.Length; i++)
            {
                foreach(string rNo in rSeq[i].Trim().Split(" "))
                {
                    regexRule += GenRegexRule(Int32.Parse(rNo), rules);
                }
                if (i < rSeq.Length - 1)
                {
                    regexRule += "|";
                }
            }

            regexRule += ")";

            return regexRule;
        }

        public static string GetRuleVal(string rule)
        {
            return rule.Split("\"")[1];
        }

    }
}
